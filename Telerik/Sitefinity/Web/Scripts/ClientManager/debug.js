Type.registerNamespace("Telerik.Sitefinity.Data");
/* ClientManager class */

/* ClientManager class is a class that all binders and client components should work with when working with
* WCF REST services. It is the role of the ClientManager to prepare and invoke the requests, as well as to
* handle the incoming responses. ClientManager should also handle any problems or errors resulting from these
* calls. This layer of abstraction allows us more flexibility in regards to the asynchronious approach to data
* access (e.g. we may decide to implement SOAP later on, and this is the only place where the change should be 
* made - at least when the client side is concerned).
*/
Telerik.Sitefinity.Data.ClientManager = function (isAsynchronous) {

    if (!Telerik.Sitefinity.Data.ClientManager._requestQueue) {
        Telerik.Sitefinity.Data.ClientManager._requestQueue = new Telerik.Sitefinity.Data.RequestQueue();
    }

    this._serviceUrl = null;
    this._batchOperation = false;
    this._urlParams = null;
    this._isRequestInProgress = false;
    this._culture = null;
    this._isBackend = false;
    this._uiCulture = null;
    this._fallbackMode = null;
    this._isAsynchronous = isAsynchronous ? true : false; //whether to use the queue of requests or not
    this._serializePostData = true;
    this._dataSerializerType;
    //The name of the timestamp paremeter used to prevent caching GET requests. Should not be used anywhere, except when sending the request.
    this._timestampParameterName = "unv_tstmp_prm";
    this._siteIdCallsArr = null;    

    // delegates
    this.RequestEndDelegate = Function.createDelegate(this, this.RequestEndCallBack);
    this.NonBinderCallbackDelegate = Function.createDelegate(this, this._nonBinderCallback);

    Telerik.Sitefinity.Data.ClientManager.initializeBase(this);
}

// Key that specifies the required culture for the requests. Set in WebRequest header
Telerik.Sitefinity.Data.ClientManager.CultureHeaderKey = "SF_CULTURE";
// Key that specifies the required UI culture for the requests. Set in WebRequest header
Telerik.Sitefinity.Data.ClientManager.UiCultureHeaderKey = "SF_UI_CULTURE";
// Key that specifies the fallback mode valid for the current requests. Set in WebRequest header.
Telerik.Sitefinity.Data.ClientManager.RequestFallbackModeKey = "SF_FALLBACK_MODE";

Telerik.Sitefinity.Data.ClientManager.prototype = {
    // ********************************************************************************
    // * Clean-up
    // ********************************************************************************
    dispose: function () {
        // clean-up delegates
        this.GetItemCollectionDelegate = null;
        this.GetItemDelegate = null;
        this.DeleteItemDelegate = null;
        this.SaveItemDelegate = null;
        this.RequestEndDelegate = null;
        this.NonBinderCallbackDelegate = null;

        Telerik.Sitefinity.Data.ClientManager.callBaseMethod(this, "dispose");
    },   

    // Use this function to get a collection of the items from the WCF REST service. The function accepts
    // a mandatory binder argument, which has to be an object derived from the ClientBinder base class. All
    // the information needed to retrieve the item collection is defined on the binder object.
    GetItemCollection: function (binder, serviceUrl, dataKey, serviceCallback, context, data) {
        if (serviceUrl) {
            this._serviceUrl = serviceUrl;
        }
        else {
            this._serviceUrl = binder.get_serviceBaseUrl();
        }
        if (serviceCallback) {
            binder._serviceCallback = serviceCallback;
        }
        else {
            binder._serviceCallback = binder.BindCollection;
        }
        if (this.GetItemCollectionDelegate) {
            delete this.GetItemCollectionDelegate;
        }
        this.GetItemCollectionDelegate = Function.createDelegate(this, this.GetItemCollectionCallback);
        this._getItemCollection(this.GetItemCollectionDelegate, binder, dataKey, context, data);
    },
    // This is a callback function fired after the collection of items has been retrieved. If the response is
    // available and valid, the BindCollection on the Binder that invoked the GetItemCollection method will be
    // called and deserialized CollectionContext (defined on the server side) object will be passed to it.
    GetItemCollectionCallback: function (executor, eventArgs) {
        var self = this;
        this._handleResponse(executor, function (customContext, completedExecutor) {
            var binder = completedExecutor._webRequest._userContext.binder;
            //var customContext = executor._webRequest._userContext.context;
            if (completedExecutor.get_responseAvailable()) {
                var rawData = completedExecutor.get_responseData();
                if (rawData != null && rawData.length > 0 && (rawData.charAt(0) == '{' || rawData.charAt(1) == '[')) {
                    var deserializedResponse = Sys.Serialization.JavaScriptSerializer.deserialize(rawData);
                    //var deserializedResponse = JSON.parse(rawData);
                    binder._serviceCallback(deserializedResponse, customContext);
                }
                else {
                    var data = { "Context": null, "IsGeneric": false, "Items": [], "TotalCount": 0 };
                    binder._serviceCallback(data, customContext);
                    self.RequestEndCallBack(completedExecutor, eventArgs);
                    binder._errorHandler("Could not deserialize service response, because it was empty or not a valid JSON.");
                }
            }
            else {
                var data = { "Context": null, "IsGeneric": false, "Items": [], "TotalCount": 0 };
                binder._serviceCallback(data, customContext);
                self.RequestEndCallBack(completedExecutor, eventArgs);
                binder._errorHandler("Could not deserialzie, because there was no response data from the web service.");
            }
        });
    },
    // Use this function to get a single item from the WCF REST service. The function accepts a mandatory
    // binder argument, which has to be an object derived from the ClientBinder base class. All the information
    // needed to retrieve the item is defined on the binder object.
    GetItem: function (binder, dataKey) {
        this._serviceUrl = binder.get_serviceBaseUrl();
        binder._serviceCallback = binder.BindItem;
        if (this.GetItemDelegate) {
            delete this.GetItemDelegate;
        }
        this.GetItemDelegate = Function.createDelegate(this, this.GetItemCallback);
        this._getItem(this.GetItemDelegate, binder, dataKey);
    },

    // This is a callback function fired after the item has been retrieved. If the response is available and valid,
    // the BindItem on the Binder that invoked the GetItem method will be called and deserialized item (defined on
    // the server side) object will be passed to it.
    GetItemCallback: function (executor, eventArgs) {
        var binder = executor._webRequest._userContext.binder;
        var context = executor._webRequest._userContext.context;
        executor._webRequest.remove_completed(this.GetItemDelegate);

        this._handleResponse(executor, function (customContext, completedExecutor) {
            var item = completedExecutor.get_responseData();
            if (item) item = Sys.Serialization.JavaScriptSerializer.deserialize(item);
            binder._serviceCallback(item, customContext);
        });
    },

    DeleteItem: function (binder, dataKey, lang) {
        this._batchOperation = false;
        this._serviceUrl = binder.get_serviceBaseUrl();
        if (this.DeleteItemDelegate) {
            delete this.DeleteItemDelegate;
        }
        this.DeleteItemDelegate = Function.createDelegate(this, this.DeleteItemCallback);
        this._deleteItem(this.DeleteItemDelegate, binder, dataKey, lang);
    },

    DeleteItemCallback: function (executor, eventArgs) {
        var context = executor._webRequest._userContext;
        if (context.callback) {
            var data = executor.get_responseData();
            if (data && typeof (data) == typeof ("") && data.length > 0 && (data.charAt(0) == "{" || data.charAt(0) == "["))
                data = Sys.Serialization.JavaScriptSerializer.deserialize(data);
            context.callback(context.Caller, data);
        }

        executor._webRequest.remove_completed(this.DeleteItemDelegate);
        this._handleResponse(executor);

        //raise the onDeleted event on the binder:
        var binder = executor._webRequest.get_userContext().binder;
        if (binder != null) {
            var binderEvents = binder.get_events();
            var binderOnDeletedHandler = null;
            var eventArgs = eventArgs = Sys.EventArgs.Empty;
            if (binderEvents != null)
                binderOnDeletedHandler = binderEvents.getHandler("onDeleted");
            if ((binderOnDeletedHandler != null) && (typeof (binderOnDeletedHandler) == "function")) {
                binderOnDeletedHandler(this, eventArgs);
            }
        }
    },

    deleteItems: function (binder, dataKeys, callback, caller, language, checkRelatingData) {
        this._batchOperation = true;
        this._serviceUrl = binder.get_serviceBaseUrl();

        if (this.DeleteItemDelegate)
            delete this.DeleteItemDelegate;
        this.DeleteItemDelegate = Function.createDelegate(this, this.DeleteItemCallback);

        this._deleteItems(this.DeleteItemDelegate, binder, null, dataKeys, callback, caller, language, checkRelatingData);
        this._batchOperation = false;
    },
    // Use this function to save an item through the WCF REST service. The function accepts following arguments:
    // *binder - object that invoked the method and that derives from the ClientBinder base class.
    // * keyFilter - dynamic LINQ filter expression used to find the item (and verify wheather it exists or not). Note
    // that the exception will be thrown on the server if the keyFilter expression returns more than one item.
    // * itemProperties - the property bag with all the item properties that should be persisted. The actual
    // creation of the object is responsibility of the server, so that the Factory pattern can be obeyed.
    // * culture - name of the culture for which the item shoud be saved (e.g. "en-US").
    SaveItem: function (binder, dataKeys, itemProperties, culture) {
        this._batchOperation = false;
        this._serviceUrl = binder.get_serviceBaseUrl();
        if (this.SaveItemDelegate) {
            delete this.SaveItemDelegate;
        }
        this.SaveItemDelegate = Function.createDelegate(this, this.SaveItemCallback);
        this._putItem(dataKeys, itemProperties, culture, binder, this.SaveItemDelegate);
    },

    SaveItems: function (binder, dataKeys, properties, culture) {
        this._batchOperation = true;
        this._serviceUrl = binder.get_serviceBaseUrl();
        if (this.SaveItemDelegate) {
            delete this.SaveItemDelegate;
        }
        this.SaveItemDelegate = Function.createDelegate(this, this.SaveItemCallback);
        this._putItem(dataKeys, properties, culture, binder, this.SaveItemDelegate);
        this._batchOperation = false;
    },

    // This is a callback function fired after the item has been saved. This method will raise the onSaved event
    // on the binder.
    SaveItemCallback: function (executor, eventArgs) {
        executor._webRequest.remove_completed(this.SaveItemDelegate);
        this._handleResponse(executor, function (customContext, completedExecutor) {
            var binder = completedExecutor._webRequest._userContext.binder;
            binder._savedHandler();
        });
    },

    // IVAN's note: This cannot be done like this, since manager is not to know about the binder. Work with
    // interfaces if needed, or even better have the manager be the received instead of the binder.
    // This is a callback function fired on request completion
    RequestEndCallBack: function (executor, eventArgs) {
        var binder = executor._webRequest._userContext.binder;
        executor._webRequest.remove_completed(this.RequestEndDelegate);
        binder._manager._isRequestInProgress = false;
        binder._endProcessingHandler();

        // clear url params
        this.set_urlParams([]);
    },

    GetEmptyGuid: function () {
        return '00000000-0000-0000-0000-000000000000';
    },

    // This method performs an actual web request initiated by one of the data
    // manipulation methods. The information about the request is received through
    // the parameters. Binder that initialized the request will be passed as User Context
    // and hence will be available on the callback.
    // *** The method is not meant to be consumed outside of the ClientManager class. ***
    _invokeWebService: function (url, verb, data, binder, successDelegate, context, callback, caller) {
        binder._startProcessingHandler();
        var wRequest = new Sys.Net.WebRequest();
        wRequest.set_url(url);
        wRequest.set_httpVerb(verb);
        this._applyLocalizationHeaders(wRequest);

        //TODO set the request timeout - wRequest.set_timeout('milliseconds');
        if (data) {

            var postData = data;
            // javascript-array-becomes-an-object-structure when it is passed to another document. Mind : BLOWN 
            if (this.get_serializePostData()) {
                if (this.get_dataSerializerType() != null && this.get_dataSerializerType() === Telerik.Sitefinity.Data.SerializationType.Sitefinity) {
                    postData = Telerik.Sitefinity.JSON.stringify(postData);
                }
                else {
                    postData = Sys.Serialization.JavaScriptSerializer.serialize(postData);
                }
            }

            wRequest.set_body(postData);

            // setting content-length is not allowed
            // http://www.w3.org/TR/XMLHttpRequest2/#the-setrequestheader-method
            //wRequest.get_headers()["Content-Length"] = postData.length;
            wRequest.get_headers()["Content-Type"] = "application/json";
        }
        
        var userContext = { "binder": binder, "context": context, "callback": callback, "Caller": caller };
        wRequest.set_userContext(userContext);
        // NOTE: If we change the places of the add_completed handlers, Users will not work. Investigate when nothing better
        // to do or if the problem manifest itself somewhere else.
        wRequest.add_completed(this.RequestEndDelegate);
        wRequest.add_completed(successDelegate);
        this._isRequestInProgress = true;

        Telerik.Sitefinity.Data.ClientManager._requestQueue.addAndExecute(wRequest);
    },
    // This method is used to retrieve a collection of items from the WCF REST service.
    // *** The method is not meant to be consumed outside of the ClientManager class. ***
    _getItemCollection: function (successDelegate, binder, dataKeys, context, data) {

        if (binder.get_provider() != null) {
            this.get_urlParams()['provider'] = binder.get_provider();
        }

        if (binder.get_sortExpression() != null) {
            this.get_urlParams()['sortExpression'] = binder.get_sortExpression();
        }

        if (binder.get_skip() != null) {
            this.get_urlParams()['skip'] = binder.get_skip();
        }

        if (binder.get_take() != null) {
            this.get_urlParams()['take'] = binder.get_take();
        }

        if (binder.get_filterExpression() != null) {
            this.get_urlParams()['filter'] = encodeURIComponent(binder.get_filterExpression());
        }

        if (binder.get_dataType() != null) {
            this.get_urlParams()['itemType'] = binder.get_dataType();
        }

        this._setSiteId(this.get_urlParams());

        var url = this._getServiceUrl(binder, dataKeys);

        var method;
        if (data !== null && data != undefined) {
            method = "POST";
        }
        else {
            method = "GET";
            data = null;
        }
        this._invokeWebService(url, method, data, binder, successDelegate, context);
    },
    // This method is used to retrieve a single item from the WCF REST service.
    // *** The method is not meant to be consumed outside of the ClientManager class. ***
    _getItem: function (successDelegate, binder, dataKey) {

        if (binder.get_provider() != null) {
            this.get_urlParams()['provider'] = binder.get_provider();
        }

        this._setSiteId(this.get_urlParams());

        var url = this._getServiceUrl(binder, dataKey);
        this._invokeWebService(url, "GET", null, binder, successDelegate);
    },
    _deleteItem: function (successDelegate, binder, dataKey, lang) {

        if (binder.get_provider() != null) {
            this.get_urlParams()['provider'] = binder.get_provider();
        }

        this._setSiteId(this.get_urlParams());

        var url = this._getServiceUrl(binder, dataKey);
        if (lang) {
            url += "&lang=" + lang;
        }
        this._invokeWebService(url, "DELETE", null, binder, successDelegate);
    },
    _deleteItems: function (successDelegate, binder, dataKey, dataKeys, callback, caller, language, checkRelatingData) {
        if (binder.get_provider() != null)
            this.get_urlParams()['provider'] = binder.get_provider();

        this._setSiteId(this.get_urlParams());

        var url = this._getServiceUrl(binder, dataKey);

        if (language) {
            url += "&language=" + language;
        }

        if (checkRelatingData) {
            url += "&checkRelatingData=" + checkRelatingData;
        }

        var dataKeyValues = [];
        if (dataKeys && !this._isArrayEmpty(dataKeys)) {
            if (typeof (dataKeys) == 'string') {
                dataKeyValues.push(dataKeys);
            }
            else {
                for (var keyName in dataKeys) {
                    if (typeof (dataKeys[keyName]) === "string") {
                        dataKeyValues.push(dataKeys[keyName]);
                    }
                }
            }
        }

        // Figure out the proper way to do this
        var datakeysCount = dataKeys.length;
        while (datakeysCount--) {
            if (dataKeyValues.indexOf(this._getDataKeyValue(dataKeys[datakeysCount])) < 0) {
                dataKeyValues.push(this._getDataKeyValue(dataKeys[datakeysCount]));
            }
        }
        this._invokeWebService(url, "POST", dataKeyValues, binder, successDelegate, null, callback, caller);
    },
    // This method is used to post (create or update) an item to the WCF REST service.
    // *** The method is not meant to be consumed outside of the ClientManager class. ***
    _putItem: function (dataKeys, item, culture, binder, successDelegate) {

        if (binder.get_provider() != null) {
            this.get_urlParams()['provider'] = binder.get_provider();
        }

        this._setSiteId(this.get_urlParams());

        var url = this._getServiceUrl(binder, dataKeys);
        this._invokeWebService(url, "PUT", item, binder, successDelegate);
    },
    _onFailure: function (message) {
        alert(message);
    },

    _logSiteId: function (method, siteIdValue) {
        if (this._siteIdCallsArr == null) {
            this._siteIdCallsArr = [];
        }

        var date = new Date();
        var now = date.toUTCString();

        if (siteIdValue == null) {
            var sfSiteId = this.get_urlParams()['sf_site'];
            this._siteIdCallsArr.push(sfSiteId + method + now);
        }
        else {
            this._siteIdCallsArr.push(siteIdValue + method + now);
        }

    },

    // processes the reponse from service, calls the error handler and raises calback for 
    // successful response.
    _handleResponse: function (executor, callback) {
        if (typeof callback != "function") {
            callback = executor._webRequest.__completedArg;
        }
        var statusCode = executor.get_statusCode();

        //The status code is zero when the request is aborted and we should not execute any of the response handling logic.
        if (statusCode === 0)
            return;

        if (statusCode < 200 || statusCode >= 300) {
            var self = this;
            executor.get_webRequest().__completedArg = callback;
            var reInvokeFn = function (executor2, callback2) {
                // callback is Object and not Function, because the second argument of a completed callback
                // is EventArgs.Empty => newObject if the executor is XMLHttpExecutor (and there is no other executor)                
                self._handleResponse.apply(self, [executor2, executor2._webRequest.__completedArg]);
            };

            if (!Telerik.Sitefinity.Data.ClientManager._handleSessionExpire(executor, reInvokeFn)) {
                //var binder = executor._webRequest._userContext.binder; 
                //binder._errorHandler({ "Detail": "Session expired" });
                this.RequestEndCallBack(executor, Sys.EventArgs.Empty);
                return;
            }

            // something thown by user intentionally.
            if (executor.get_responseAvailable()) {
                var binder = executor._webRequest._userContext.binder;
                binder._errorHandler(Telerik.Sitefinity.Data.ClientManager._parseErrorResponseObject(executor));
            }
            else {
                var errorData = { "Detail": "No respose available. Status text is " + executor.get_statusText() };
                binder._errorHandler(errorData);
            }
            this.RequestEndCallBack(executor, Sys.EventArgs.Empty);
        }
        else {
            if (callback != null) {
                callback(executor._webRequest._userContext.context, executor);
            }
        }
    },
    /* Helper methods */

    _getServiceUrl: function (binder, dataKey) {

        // remove trailing slash if it is in the query string
        if (this._serviceUrl.indexOf('?') > -1 && (this._serviceUrl.lastIndexOf('/') == this._serviceUrl.length - 1)) {
            this._serviceUrl = this._serviceUrl.substr(0, this._serviceUrl.length - 1);
        }

        var serviceUrl = new Sys.Uri(this._serviceUrl);

        if (this._batchOperation) {
            serviceUrl.get_pathSections().push("batch");
        }

        // append all global datakeys in proper order
        var globalDataKeys = binder.get_globalDataKeys();
        var dataKeyNames = binder.get_dataKeyNames();
        if (globalDataKeys && dataKeyNames) {
            for (dataKeyIndex = 0; dataKeyIndex < dataKeyNames.length; dataKeyIndex++) {
                var dataKeyName = dataKeyNames[dataKeyIndex].replace(' ', '');
                if (globalDataKeys[dataKeyName] == null) {
                    break;
                }
                if (globalDataKeys[dataKeyName].length == 0) {
                    break;
                }
                serviceUrl.get_pathSections().push(escape(globalDataKeys[dataKeyName]));
            }
        }

        // add key if it exists
        var keysUrlPart = '';
        if (dataKey && !this._isArrayEmpty(dataKey)) {
            if (typeof (dataKey) == 'string') {
                serviceUrl.get_pathSections().push(escape(dataKey));
            }
            else {
                for (var keyName in dataKey) {
                    if (typeof (dataKey[keyName]) == "string") {
                        serviceUrl.get_pathSections().push(escape(dataKey[keyName]));
                    }
                }
            }
        }

        // generate query string from the urlParams
        if (this._urlParams == null || this._urlParams.length == 0) {
            this._urlParams = binder.get_urlParams();
        }

        if (this._urlParams != null) {
            for (var paramName in this._urlParams) {
                if (!(this._urlParams[paramName] instanceof Function)) {
                    serviceUrl.get_query()[paramName] = this._urlParams[paramName];
                }
            }
        }

        return serviceUrl.toString();
    },

    _getQueryStringParams: function () {

        var params = [];
        var qs = location.search.substring(1, location.search.length);
        if (qs.length == 0) return params;

        // Turn <plus> back to <space>
        // See: http://www.w3.org/TR/REC-html40/interact/forms.html#h-17.13.4.1
        qs = qs.replace(/\+/g, ' ');
        var args = qs.split('&'); // parse out name/value pairs separated via &

        // split out each name=value pair
        for (var i = 0; i < args.length; i++) {
            var pair = args[i].split('=');
            var name = decodeURIComponent(pair[0]).toLowerCase();

            var value = (pair.length == 2)
			? decodeURIComponent(pair[1])
			: name;

            params[name] = value;
        }

        return params;
    },

    getSiteId: function () {
        if (this.siteId == null) {

            var queryStringParams = this._getQueryStringParams();
            if (queryStringParams[this.getSiteIdKey()] != null) {
                this.siteId = queryStringParams[this.getSiteIdKey()];
            }
        }
        return this.siteId;
    },

    getSiteIdKey: function () {
        return "sf_site";
    },

    _setSiteId: function (queryStringParams) {
        if (queryStringParams == null) {
            queryStringParams = [];
        }

        if (queryStringParams[this.getSiteIdKey()] == null && this.getSiteId() != null) {
            queryStringParams[this.getSiteIdKey()] = this.getSiteId();
        }
    },

    //This function is used to get the value of the dataKey in cases it can be string or dictionary
    _getDataKeyValue: function (dataKey) {
        if (typeof dataKey == typeof "") {
            return dataKey;
        }
        var dataKeyValue;
        for (var paramName in dataKey) {
            if (!(dataKey[paramName] instanceof Function)) {
                if (dataKeyValue) {
                    throw "Multiple dataKeys are not supported!";
                }
                dataKeyValue = dataKey[paramName];
            }
        }
        return dataKeyValue;
    },
    InvokePut: function (url, urlParams, keys, data, successDelegate, failureDelegate, caller, enableValidation, validationGroup, context, isSynchronous) {
        var validationResult = true;
        if (enableValidation) {
            if (typeof (Page_ClientValidate) == 'function') {
                validationResult = Page_ClientValidate(validationGroup);
            }
        }

        if (validationResult) {
            var wRequest = new Sys.Net.WebRequest();
            this._setSiteId(urlParams);
            wRequest.set_url(this._generateNonBinderUrl(url, urlParams, keys));
            wRequest.set_httpVerb('PUT');
            this._applyLocalizationHeaders(wRequest);

            if (data) {
                var postData = Sys.Serialization.JavaScriptSerializer.serialize(data);
                wRequest.set_body(postData);
                // setting content-length is not allowed
                // http://www.w3.org/TR/XMLHttpRequest2/#the-setrequestheader-method                
                //wRequest.get_headers()["Content-Length"] = postData.length;
                wRequest.get_headers()["Content-Type"] = "application/json";

            }

            var delegates = { Success: successDelegate, Failure: failureDelegate, Caller: caller, Context: context };
            wRequest.set_userContext(delegates);
            wRequest.add_completed(this.NonBinderCallbackDelegate);

            if (this._isAsynchronous) {
                //manager is not using queues at all
                wRequest.invoke();
            }
            else {
                if (isSynchronous) {
                    var executor = new Sys.Net.XMLHttpSyncExecutor();
                    wRequest.set_executor(executor);
                    wRequest.set_timeout(30 * 1000); //does not work
                    wRequest.invoke();
                    if (executor.get_responseAvailable()) {
                        if (successDelegate) {
                            successDelegate(caller, (executor.get_object()));
                        }
                        return (executor.get_object());
                    }
                    if (failureDelegate) {
                        failureDelegate((executor.get_object()));
                    }
                    return (false);
                }
                else {
                    Telerik.Sitefinity.Data.ClientManager._requestQueue.addAndExecute(wRequest);
                }
            }
        }
    },
    InvokePost: function (url, urlParams, keys, data, successDelegate, failureDelegate, caller, context) {
        var wRequest = new Sys.Net.WebRequest();
        this._setSiteId(urlParams);
        wRequest.set_url(this._generateNonBinderUrl(url, urlParams, keys));
        wRequest.set_httpVerb('POST');
        this._applyLocalizationHeaders(wRequest);

        if (data) {
            var postData = Sys.Serialization.JavaScriptSerializer.serialize(data);
            wRequest.set_body(postData);
            // setting content-length is not allowed
            // http://www.w3.org/TR/XMLHttpRequest2/#the-setrequestheader-method                
            //wRequest.get_headers()["Content-Length"] = postData.length;
            wRequest.get_headers()["Content-Type"] = "application/json";
        }
        else {
            alert('POST method cannot be invoked without the data');
            return;
        }

        var delegates = { Success: successDelegate, Failure: failureDelegate, Caller: caller, Context: context };
        wRequest.set_userContext(delegates);
        wRequest.add_completed(this.NonBinderCallbackDelegate);

        Telerik.Sitefinity.Data.ClientManager._requestQueue.addAndExecute(wRequest);
    },
    InvokeGet: function (url, urlParams, keys, successDelegate, failureDelegate, caller, context) {
        var wRequest = new Sys.Net.WebRequest();
        this._setSiteId(urlParams);
        var generatedUrl = this._generateNonBinderUrlInternal(url, urlParams, keys, true);
        wRequest.set_url(generatedUrl);
        wRequest.set_httpVerb('GET');
        this._applyLocalizationHeaders(wRequest);

        var delegates = { Success: successDelegate, Failure: failureDelegate, Caller: caller, Context: context };
        wRequest.set_userContext(delegates);
        wRequest.add_completed(this.NonBinderCallbackDelegate);
        if (this._isAsynchronous) {
            wRequest.invoke();
        }
        else {
            Telerik.Sitefinity.Data.ClientManager._requestQueue.addAndExecute(wRequest);
        }
    },

    InvokeDelete: function (url, urlParams, keys, successDelegate, failureDelegate, caller, context) {
        var wRequest = new Sys.Net.WebRequest()
        this._setSiteId(urlParams);
        wRequest.set_url(this._generateNonBinderUrl(url, urlParams, keys));
        wRequest.set_httpVerb('DELETE');
        this._applyLocalizationHeaders(wRequest);

        var delegates = { Success: successDelegate, Failure: failureDelegate, Caller: caller, Context: context };
        wRequest.set_userContext(delegates);
        if (this.NonBinderCallbackDelegate) {
            wRequest.add_completed(this.NonBinderCallbackDelegate);
        }

        if (this._isAsynchronous) {
            wRequest.invoke();
        }
        else {
            Telerik.Sitefinity.Data.ClientManager._requestQueue.addAndExecute(wRequest);
        }
    },

    //Method that submits the form to the specified path using the provided method(post, put)
    //Each entry from the params object is submitted to the server using different hidden field.
    //This is mostly used in RadWindow dialogs to post data to the server.
    SubmitForm: function (path, params, method) {
        method = method || "post";

        var body = $('body');
        var form = $('form');

        form.attr("method", method);
        form.attr("action", path);

        $.each(params, function (key, value) {
            var field = $('<input />');
            field.attr("type", "hidden");
            field.attr("name", key);
            field.attr("value", value);
            form.append(field);
        });

        body.append(form);
        form.submit();
    },

    JQueryPost: function (url, data, serializerType, doneHandler, failHandler) {
        var serializedData;
        if (serializerType == Telerik.Sitefinity.Data.SerializationType.Sitefinity) {
            serializedData = Telerik.Sitefinity.JSON.stringify(data);
        }
        else {           
            serializedData = Sys.Serialization.JavaScriptSerializer.serialize(data);
        }
        
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: url,
            data: serializedData,
            dataType: "json",
            async: false
        }).done(doneHandler).fail(failHandler);
    },

    _generateNonBinderUrl: function (url, urlParams, keys) {
        return this._generateNonBinderUrlInternal(url, urlParams, keys, false);
    },
    _generateNonBinderUrlInternal: function (url, urlParams, keys, addTimestamp) {
        var serviceUrl = url;

        // this ensures that the base url ends with the slash, which is required
        // by the WCF REST factory we use
        if (serviceUrl.charAt(serviceUrl.length - 1) != '/' && serviceUrl.indexOf('?') < 0) {
            serviceUrl += '/';
        }

        // add key if it exists
        var keysUrlPart = '';
        for (var keyName in keys) {
            if (typeof keys[keyName] == 'string') {
                keysUrlPart += escape(keys[keyName]) + '/';
            }
        }

        if (serviceUrl.indexOf('?') < 0) {
            serviceUrl += keysUrlPart;
        }
        else {
            var urlParts = serviceUrl.split('?');
            serviceUrl = urlParts[0] + keysUrlPart + '?' + urlParts[1];
        }

        // add url parameters if they exist
        if (urlParams != null) {
            if (serviceUrl.indexOf('?') > -1) {
                if (serviceUrl.charAt(serviceUrl.length - 1) == '/') {
                    serviceUrl = serviceUrl.substr(0, serviceUrl.length - 1);
                }
                serviceUrl = serviceUrl + '&';
            }
            else {
                serviceUrl = serviceUrl + '?';
            }
            for (var param in urlParams) {
                // Why is this filtering at all?
                if (typeof urlParams[param] == 'string' || typeof urlParams[param] == 'boolean' || typeof urlParams[param] == 'number') {
                    serviceUrl += param + '=' + urlParams[param] + '&';
                }
            }
            // remove the last ampersand
            serviceUrl = serviceUrl.substring(0, serviceUrl.length - 1);
        }

        if (addTimestamp == true) {
            if (serviceUrl.indexOf('?') < 0) {
                serviceUrl += "?";
            }
            else {
                serviceUrl += "&"
            }
            serviceUrl += this._timestampParameterName + "=" + new Date().getTime();
        }

        return serviceUrl;
    },

    _nonBinderCallback: function (executor, callback) {
        if (typeof callback != "function") {
            callback = executor._webRequest.__completedArg;
        }

        var callbacks = executor._webRequest._userContext;
        executor._webRequest.remove_completed(this.NonBinderCallbackDelegate);

        if (executor.get_statusCode() < 200 || executor.get_statusCode() >= 300) {
            var self = this;
            executor.get_webRequest().__completedArg = callback;
            var reInvokeFn = function (executor2, callback2) {
                // callback is Object and not Function, because the second argument of a completed callback
                // is EventArgs.Empty => newObject if the executor is XMLHttpExecutor (and there is no other executor)      
                self._nonBinderCallback(executor2, executor2._webRequest.__completedArg); // callback2 is Object and not Function for some reason
            };

            if (!Telerik.Sitefinity.Data.ClientManager._handleSessionExpire(executor, reInvokeFn)) {
                return;
            }

            // something thown by user intentionally.
            if (executor.get_responseAvailable()) {
                callbacks.Failure(Telerik.Sitefinity.Data.ClientManager._parseErrorResponseObject(executor), callbacks.Caller, callbacks.Context);
            }
            else {
                var error = new Object();
                error["Detail"] = "No response available for error code " + executor.get_statusCode();
                callbacks.Failure(error, callbacks.Caller, callbacks.Context);
            }
        }
        else {
            var data = executor.get_responseData();
            if (data) {
                if (typeof (data) == typeof ("") && data.length > 0 && (data.charAt(0) == "{" || data.charAt(0) == "[")) {
                    data = Sys.Serialization.JavaScriptSerializer.deserialize(data);
                }
            }
            if (callbacks.Success)
                callbacks.Success(callbacks.Caller, data, executor._webRequest, callbacks.Context);
        }
    },

    get_urlParams: function () {
        if (this._urlParams == null) {
            this._urlParams = [];
        }
        return this._urlParams;
    },

    set_urlParams: function (value) {
        if (this._urlParams != value) {
            this._urlParams = value;
            this.raisePropertyChanged('urlParams');
        }
    },

    _isArrayEmpty: function (arr) {
        var len = arr.length;
        if (len > 0) return false;

        for (var item in arr) len++;

        return len == 0;
    },

    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this._culture = culture;
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this._culture;
    },

    set_isBackend: function (isBackend) {
        this._isBackend = isBackend;
    },

    get_isBackend: function () {
        return this._isBackend;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    },

    // Sets the culture fallback mode for the requests.
    set_fallbackMode: function (culture) {
        this._fallbackMode = culture;
    },
    // Gets the culture fallback mode for the requests.
    get_fallbackMode: function () {
        return this._fallbackMode;
    },

    get_serializePostData: function() {
        return this._serializePostData;
    },

    set_serializePostData: function(value) {
        this._serializePostData = value;
    },
    get_dataSerializerType: function () {
        return this._dataSerializerType;
    },
    set_dataSerializerType: function (serializerType) {
        this._dataSerializerType = serializerType;
    },

    // Sets the custom headers required for the localization. They specify the current thread culture and ui culture that will be set 
    // for the service call
    _applyLocalizationHeaders: function (webRequest) {

        if (this._culture) {
            webRequest.get_headers()[Telerik.Sitefinity.Data.ClientManager.CultureHeaderKey] = this._culture;
        }

        if (this._uiCulture) {
            webRequest.get_headers()[Telerik.Sitefinity.Data.ClientManager.UiCultureHeaderKey] = this._uiCulture;
        }

        if (this._fallbackMode != null && this._fallbackMode !== "") {
            webRequest.get_headers()[Telerik.Sitefinity.Data.ClientManager.RequestFallbackModeKey] = this._fallbackMode;
        }

        if (this._isBackend) {
            webRequest.get_headers()["IsBackendRequest"] = true;
        }

        var currentMultisiteSelector = this._getCurrentMultisiteSelector();
        if (currentMultisiteSelector) {
            webRequest.get_headers()[currentMultisiteSelector.get_siteIdParamKey()] = currentMultisiteSelector.get_selectedSite();
        }
    },
    _getCurrentMultisiteSelector: function () {
        if (typeof GetCurrentMultisiteSelector !== "undefined") {
            return GetCurrentMultisiteSelector();
        }
        else if (window.top && typeof window.top.GetCurrentMultisiteSelector !== "undefined")
        {
            return window.top.GetCurrentMultisiteSelector();
        }
        return null;
    }
};
Telerik.Sitefinity.Data.ClientManager.registerClass('Telerik.Sitefinity.Data.ClientManager', Sys.Component);

Telerik.Sitefinity.Data.SerializationType = function () {
};
Telerik.Sitefinity.Data.SerializationType.prototype = {
    Sitefinity: 0,
    Sys: 1,    
};
Telerik.Sitefinity.Data.SerializationType.registerEnum("Telerik.Sitefinity.Data.SerializationType");

Telerik.Sitefinity.Data.ClientManager._parseErrorResponseObject = function (executor) {
    var errorData = "";
    var binder = executor._webRequest._userContext.binder;
    errorData = executor.get_responseData();
    if (errorData != null) {
        if (typeof (errorData) == typeof ("") && errorData.length > 0) {
            if (errorData.indexOf("{") == 0 || errorData.indexOf("[") == 0)
                errorData = Sys.Serialization.JavaScriptSerializer.deserialize(errorData);
            else if (errorData.startsWith("<html>") || errorData.startsWith("<!DOCTYPE html")) {
                var pattern = new RegExp("\<title.*?\>(\n*\r*.+\n*\r*)\<\/title.*?\>", "gi");
                var parsed = pattern.exec(errorData);
                if ((parsed != null) && (parsed.length > 1))
                    parsed = parsed[1];
                errorData = { "Detail": parsed };
            }
            else {
                var matches = (/\<B\>.*<\/B>(.*)<\/p>\<!--.*x+-->/gi).exec(errorData);
                if (matches != null && matches.length == 2) {
                    errorData = { "Detail": matches[1] };
                }
            }
        }
    }

    if (!errorData) {
        errorData = { "Detail": "Return code: " + executor.get_statusCode() };
    }
    errorData.StatusCode = executor.get_statusCode();
    return errorData;
}

// LOGIN functionality
Telerik.Sitefinity.Data.ClientManager._handleSessionExpireIsUsed = null;
Telerik.Sitefinity.Data.ClientManager._handleSessionExpire = function (executor, reInvokeFn) {
    ///<summary>Handle the case when the session has expired</summary>
    ///<param name="executor">Sys.Net.WebRequestExecutor</param>
    ///<param name="reInvokeFn">Function</param>
    ///<result>false if redirect, true if there is no authentication failure.</result>    

    // ..::login|session|expired::..
        
    var loginUrl = "";
    var errorMsg = Telerik.Sitefinity.Data.ClientManager._parseErrorResponseObject(executor);

    //Handle session expire for Claims
    if (errorMsg && (401 == errorMsg.StatusCode || 403 == errorMsg.StatusCode)) {
        var authenticationType = executor.getResponseHeader("SF-AuthProtocol");
        if (authenticationType == "OpenId") {
            Telerik.Sitefinity.Data.ClientManager._handleSessionExpireIsUsed = true;
            return true;            
            }
        }
        else {
        var issuer = executor.getResponseHeader("Issuer");
        var realm = executor.getResponseHeader("Realm");
        if (issuer && realm) {
            loginUrl = issuer + "?UserLoggingReason=SessionExpired&deflate=true&realm=" + realm + "&redirect_uri=" + location.href;
            location.href = loginUrl;
            return false;
        }
    }

    if (errorMsg && errorMsg.Detail)
        errorMsg = errorMsg.Detail;
    else {
        // go on with regular error handling logic
        return true;
    }

    if (errorMsg.startsWith("..::login|session|expired::..")) {
        // this request fails because user's session has expired
        loginUrl = errorMsg.substr("..::login|session|expired::..".length);
    }
    else {
        // go on with regular error handling logic
        return true;
    }

    if (Telerik.Sitefinity.Data.ClientManager._handleSessionExpireIsUsed) {
        // add to waiting queue and skip regular error handling logic
        Telerik.Sitefinity.Data.ClientManager._requestQueue.add(executor.get_webRequest(), reInvokeFn);
        return false;
    }
    else {
        // block other requests
        Telerik.Sitefinity.Data.ClientManager._handleSessionExpireIsUsed = true;
        Telerik.Sitefinity.Data.ClientManager._requestQueue.suspend();
    }

    if (!loginUrl) {
        // try to deduce the login url
        // TODO: find a better way -> this won't work with configuration
        var current = location.href;
        loginUrl = current.toLowerCase();
        var idx = loginUrl.indexOf("/sitefinity/");
        loginUrl = current.substring(0, idx) + "/Sitefinity/Login/Ajax";
    }
    var returnUrl = "";
    {
        var url = location.href + "";
        var query = "";
        var idx = url.indexOf('?');
        if (idx != -1) {
            url = url.substring(0, idx);
            url = encodeURI(url);
            query = location.search + "";
            query = encodeURIComponent(query);
        }
        returnUrl = url + query;
    }
    var binder = executor._webRequest._userContext.binder;
    if (binder)
        binder._endProcessingHandler();
    if (typeof radopen != "undefined") {
        // open a RadWindow if RadWindowManager is defined on this page
        var wnd = radopen(loginUrl);
        wnd.set_skin("Default");
        wnd.set_showContentDuringLoad(false);
        wnd.set_modal(false);
        wnd.set_visibleStatusbar(false);
        wnd.set_visibleTitlebar(true);
        wnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.None);
        wnd.maximize();
        wnd.__executor = executor;
        wnd.__reinvokeFn = reInvokeFn;

        wnd.add_pageLoad(function (sender, args) {
            sender.__showed = true;
            sender.set_skin("Default");
            wnd.set_title("Session expired.");
            sender.center();
            if (sender.get_contentFrame().contentWindow.dialogLoaded) {
                sender.get_contentFrame().contentWindow.dialogLoaded();
            }
        });
        wnd.add_close(function (sender, closeArg) {
            Telerik.Sitefinity.Data.ClientManager._handleSessionExpireIsUsed = false;
            Telerik.Sitefinity.Data.ClientManager._requestQueue.addWithTopPriority(sender.__executor.get_webRequest(), sender.__reinvokeFn);
            Telerik.Sitefinity.Data.ClientManager._requestQueue.resume();
            sender.dispose();
        });
    }
    else {
        // redirect to another page if RadWindowManager is not defined on this page
        Telerik.Sitefinity.Data.ClientManager._requestQueue.resume();
        var redirectUrl = loginUrl + "?ReturnUrl=" + returnUrl;
        Telerik.Sitefinity.Data.ClientManager._handleSessionExpireIsUsed = false;
        location.href = redirectUrl;
    }

    // skip regular error handling logic
    return false;
}

Sys.Uri = function (uriAsString) {
    this._scheme = null;
    this._authority = null;
    this._pathSections = [];
    this._query = {};
    this._fragment = null;

    this._isValidUrl = true;

    if (uriAsString)
        this.parse(uriAsString);
}

//Sys.Uri class
Sys.Uri.prototype = {
    initialize: function () {
    },

    dispose: function () {
    },

    /* --------------------  public methods ----------- */

    //Parses a string to URI object.
    parse: function (uriAsString) {
        if (!uriAsString || typeof uriAsString != typeof "")
            throw "URI.parse works for strings only!";
        var regexParser = /^(?:([^:\/?\#]+):)?(?:\/\/([^\/?\#]*))?([^?\#]*)(?:\?([^\#]*))?(?:\#(.*))?/;
        var result = uriAsString.match(regexParser);
        this._scheme = result[1] || null;
        this._authority = result[2] || null;
        if (result[3])
            this._pathSections.push(result[3]);
        if (result[4])
            this._parseQueryString(result[4]);
        this._fragment = result[5] || null;
    },

    //Returns the string representation of the Uri object.
    toString: function () {
        var str = "";
        if (this._scheme) {
            str += this._scheme + ":";
        }
        if (this._authority) {
            str += "//" + this._authority;
        }
        if (this._pathSections) {
            str += this.get_path();
        }
        if (this._query) {
            var appendAmpersant = false;

            for (var queryParameterName in this._query) {
                var queryValue = this._query[queryParameterName];
                if (queryValue == null || queryValue == undefined)
                    queryValue = "";

                if (appendAmpersant) {
                    str += '&';
                }
                else {
                    str += "?";
                    appendAmpersant = true;
                }

                str += queryParameterName + '=' + queryValue;
            }
        }
        if (this._fragment) {
            str += "#" + this._fragment;
        }
        return str;
    },

    /* -------------------- events -------------------- */
    /* -------------------- event handlers ------------ */
    /* -------------------- private methods ----------- */
    _parseQueryString: function (queryString) {
        var queryStringNameValueParts = queryString.split('&');
        var queryLength = queryStringNameValueParts.length;
        while (queryLength--) {
            var nameValuePair = queryStringNameValueParts[queryLength].split('=');
            var name = nameValuePair[0];
            var value = nameValuePair[1];
            if (!name || name in this._query)
                this._isValidUrl = false;
            if (name)
                this._query[name] = value || "";
        }
    },
    /* -------------------- properties ---------------- */

    // Gets the scheme of the Uri
    get_scheme: function () {
        return this._scheme;
    },
    // Sets the scheme of the Uri
    set_scheme: function (value) {
        this._scheme = value;
    },

    // Gets the authority of the Uri
    get_authority: function () {
        return this._authority;
    },
    // Sets the authority of the Uri
    set_authority: function (value) {
        this._authority = value;
    },

    // Gets the path of the Uri
    get_pathSections: function () {
        return this._pathSections;
    },
    // Sets the path of the Uri
    set_pathSections: function (value) {
        this._pathSections = value;
    },
    // Gets the constructed path
    get_path: function () {
        //TODO the path should not start with / if the first _pathSection does not start with /
        var path = "/";
        var pathSectionsLength = this._pathSections.length;
        for (var i = 0; i < pathSectionsLength; i++) {
            path += this._pathSections[i].replace(/^\/+/, '').replace(/\/+$/, '') + '/';
        }
        return path;
    },
    // Gets the query of the Uri
    get_query: function () {
        return this._query;
    },
    // Sets the query of the Uri
    set_query: function (value) {
        this._query = value;
    },

    // Gets the fragment of the Uri
    get_fragment: function () {
        return this._fragment;
    },
    // Sets the fragment of the Uri
    set_fragment: function (value) {
        this._fragment = value;
    },

    // Gets whether the parsed url was valid
    get_isValidUrl: function () {
        return this._isValidUrl;
    },
    // Sets whether the parsed url was valid
    set_isValidUrl: function (value) {
        this._isValidUrl = value;
    }
};

Sys.Uri.registerClass('Sys.Uri');

///////////////////////////////////////////////////////////////////////////////
// Class: RequestQueue
// ----------------------------------------------------------------------------
// Purpose: 
// Stores a queue of Sys.Net.WebRequest-s and executes them in order
///////////////////////////////////////////////////////////////////////////////

Telerik.Sitefinity.Data.RequestQueue = function () {
    /// <summary>Stores a queue of Sys.Net.WebRequest-s and executes them in order</summary>

    // private variables
    this._queue = [];
    this._suspended = false;

    this._beforeInvokeDelegate = Function.createDelegate(this, this._beforeInvoke);
    this._requestCompletedDelegate = Function.createDelegate(this, this._requestCompleted);
    Sys.Net.WebRequestManager.add_invokingRequest(this._beforeInvokeDelegate);
    Sys.Net.WebRequestManager.add_completedRequest(this._requestCompletedDelegate);
    Sys.Application.registerDisposableObject(this);
};
Telerik.Sitefinity.Data.RequestQueue.prototype = {
    ///////////////////////////////////////////////////////////////////////////
    // Public methods
    ///////////////////////////////////////////////////////////////////////////    
    "isSuspended": function () {
        /// <summary>Returns true if the request queue is suspended</summary>
        /// <returns>Boolean</returns>
        return this._suspended;
    },
    "suspend": function () {
        /// <summary>Suspends execution of requests</summary>
        this._suspended = true;
    },
    "resume": function () {
        /// <summary>Executes all suspended requests in a batch and enables other requests to be executed.</summary>
        this._suspended = false;
        this.execute();
    },
    "add": function (request, reinvokeFn) {
        /// <summary>Add a web request to the end of the queue</summary>
        /// <param name="request">Instance of Sys.Net.WebRequest</param>
        /// <param name="reinvokeFn">Function</param>

        if (!request || Object.getTypeName(request) != "Sys.Net.WebRequest") {
            alert("RequestQueue.add: you can only add instances of Sys.Net.WebRequest!");
            return;
        }
        request.__reinvokeFn = reinvokeFn;
        this._queue.push(request);
    },
    "addWithTopPriority": function (request, reinvokeFn) {
        /// <summary>Add a web request to the beginning of the queue</summary>
        /// <param name="request">Instance of Sys.Net.WebRequest</param>
        /// <param name="reinvokeFn">Function</param>

        if (!request || Object.getTypeName(request) != "Sys.Net.WebRequest") {
            alert("RequestQueue.addWithTopPriority: you can only add instances of Sys.Net.WebRequest!");
            return;
        }
        request.__reinvokeFn = reinvokeFn;
        this._queue.unshift(request);
    },
    "execute": function () {
        /// <summary>Executes the first request in the queue</summary>
        /// <returns>Executed Sys.Net.WebRequest or null if the queue was empty (i.e. no request was executed)</returns>       

        if (!this._suspended) {
            var firstInQueue = this._queue.shift();
            if (typeof firstInQueue != "undefined" && firstInQueue != null) {
                if (firstInQueue._invokeCalled) {
                    // if the request has already been used, we create a new request with the same parameters, but clear state
                    var newRequest = new Sys.Net.WebRequest();
                    newRequest.set_body(firstInQueue.get_body());
                    for (var header in firstInQueue.get_headers()) {
                        newRequest.get_headers()[header] = firstInQueue.get_headers()[header];
                    }
                    newRequest.set_httpVerb(firstInQueue.get_httpVerb());
                    newRequest.set_timeout(firstInQueue.get_timeout());
                    newRequest.set_url(firstInQueue.get_url());
                    newRequest.set_userContext(firstInQueue.get_userContext());
                    // Sys.Net.WebRequest uses Sys.Observer and doesn't have its own Sys.Event
                    var completedSubscribers = Sys.Observer._getContext(firstInQueue).events._list.completed;
                    for (var i = 0; i < completedSubscribers.length; i++) {
                        newRequest.add_completed(completedSubscribers[i]);
                    }
                    // Custom attribute. Contains closure function that is to be executed when a request is completed.
                    if (firstInQueue.__reinvokeFn && completedSubscribers.length == 0) {
                        newRequest.add_completed(firstInQueue.__reinvokeFn);
                    }
                    // Custom attribute. Contains the second argument (instead of Sys.EventArgs.Empty) to be passed to the __reinvokeFn closure when the request is completed
                    if (firstInQueue.__completedArg) {
                        newRequest.__completedArg = firstInQueue.__completedArg;
                    }
                    newRequest.invoke();
                    return newRequest;
                }
                else {
                    // if this is the first time a request has to be executed (has clear state), we just invoke it
                    firstInQueue.invoke();
                    return firstInQueue;
                }
            }
        }
        return null;
    },
    "addAndExecute": function (request) {
        /// <summmary>Add a request to the end of the queue and execute the first request in the queue</summary>
        /// <param name="request">Instance of Sys.Net.WebRequest</param>
        /// <returns>Executed Sys.Net.WebRequest or null if no request was executed</returns>

        this.add(request);
        if (this._suspended == false) {
            return this.execute();
        }
        else {
            return null;
        }
    },
    "addAndExecuteImmediately": function (request) {
        /// <summary>Add a request to the top of the queue and execute it before the other pending requests</summary>
        /// <param name="request">Instance of Sys.Net.WebRequest</summary>
        /// <returns>Executed Sys.Net.WebRequest or null if no request was executed</returns>
        this.addWithTopPriority(request);
        if (this._suspended == false) {
            return this.execute();
        }
        else {
            return null;
        }
    },
    "clear": function () {
        /// <summary>Clears the queue of pending requests</summary>
        this._queue = [];
    },
    "dispose": function () {
        /// <summary>Releases resources held by RequestQueue</summary>
        Sys.Net.WebRequestManager.remove_invokingRequest(this._beforeInvokeDelegate);
        delete this._beforeInvokeDelegate;

        Sys.Net.WebRequestManager.remove_completedRequest(this._requestCompletedDelegate);
        delete this._requestCompletedDelegate;
    },
    ///////////////////////////////////////////////////////////////////////////
    // Private methods
    ///////////////////////////////////////////////////////////////////////////
    "_beforeInvoke": function (sender, args) {
        if (this._suspended) {
            args.set_cancel(true);
            var request = args.get_webRequest();
            this._queue.push(request);
        }
    },
    "_requestCompleted": function (sender, args) {
        this.execute();
    }
};
Telerik.Sitefinity.Data.RequestQueue.registerClass("Telerik.Sitefinity.Data.RequestQueue", null, Sys.IDisposable);

//A singleton object keeping track of all web requests.
Telerik.Sitefinity._WebRequestManager = function Telerik$Sitefinity$_WebRequestManager() {
    this._incompleteRequestsCount = 0;
    this._requestInvokedDelegate = Function.createDelegate(this, this._requestInvokedHandler);
    this._requestCompletedDelegate = Function.createDelegate(this, this._requestCompletedHandler);
    Sys.Net.WebRequestManager.add_invokingRequest(this._requestInvokedDelegate);
    Sys.Net.WebRequestManager.add_completedRequest(this._requestCompletedDelegate);
    Sys.Application.registerDisposableObject(this);
};
function Telerik$Sitefinity$_WebRequestManager$dispose() {
    Sys.Net.WebRequestManager.remove_invokingRequest(this._requestInvokedDelegate);
    delete this._requestInvokedDelegate;
    Sys.Net.WebRequestManager.remove_completedRequest(this._requestCompletedDelegate);
    delete this._requestCompletedDelegate;
}
function Telerik$Sitefinity$_WebRequestManager$get_incompleteRequestsCount() {
    return this._incompleteRequestsCount;
}
function Telerik$Sitefinity$_WebRequestManager$_requestInvokedHandler() {
    this._incompleteRequestsCount++;
}
function Telerik$Sitefinity$_WebRequestManager$_requestCompletedHandler() {
    this._incompleteRequestsCount--;
}

Telerik.Sitefinity._WebRequestManager.prototype = {
    dispose: Telerik$Sitefinity$_WebRequestManager$dispose,
    get_incompleteRequestsCount: Telerik$Sitefinity$_WebRequestManager$get_incompleteRequestsCount,
    _requestInvokedHandler: Telerik$Sitefinity$_WebRequestManager$_requestInvokedHandler,
    _requestCompletedHandler: Telerik$Sitefinity$_WebRequestManager$_requestCompletedHandler
};

Telerik.Sitefinity._WebRequestManager.registerClass('Telerik.Sitefinity._WebRequestManager', null, Sys.IDisposable);

if (typeof (Telerik.Sitefinity.WebRequestManager) === "undefined") {
    Telerik.Sitefinity.WebRequestManager = new Telerik.Sitefinity._WebRequestManager();
}
///////////////////////////////////////////////////////////////////////////////
// Array prototype extensions
///////////////////////////////////////////////////////////////////////////////

//This prototype is provided by the Mozilla foundation and
//is distributed under the MIT license.
//http://www.ibiblio.org/pub/Linux/LICENSES/mit.license

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0)
               ? Math.ceil(from)
               : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
                        this[from] === elt)
                return from;
        }
        return -1;
    };
}

//This method let's you compare arrays, based on their contents. Also note, that this function handles nested arrays 
//just fine also.
Array.prototype.compareArrays = function (arr) {
    if (this.length != arr.length) return false;
    for (var i = 0; i < arr.length; i++) {
        if (this[i].compareArrays) { //likely nested array
            if (!this[i].compareArrays(arr[i])) return false;
            else continue;
        }
        if (this[i] != arr[i]) return false;
    }
    return true;
};



//http://weblogs.asp.net/ricardoperes/archive/2009/03/06/calling-web-service-methods-synchronously-in-asp-net-ajax.aspx
Type.registerNamespace('Sys.Net');

Sys.Net.XMLHttpSyncExecutor = function () {
    if (arguments.length !== 0) throw Error.parameterCount();
    Sys.Net.XMLHttpSyncExecutor.initializeBase(this);

    var _this = this;
    this._xmlHttpRequest = null;
    this._webRequest = null;
    this._responseAvailable = false;
    this._timedOut = false;
    this._timer = null;
    this._aborted = false;
    this._started = false;

    this._responseData = null;
    this._statusCode = null;
    this._statusText = null;
    this._headers = null;

    this._onReadyStateChange = function () {
        if (_this._xmlHttpRequest.readyState === 4) {
            _this._clearTimer();
            _this._responseAvailable = true;

            _this._responseData = _this._xmlHttpRequest.responseText;
            _this._statusCode = _this._xmlHttpRequest.status;
            _this._statusText = _this._xmlHttpRequest.statusText;
            _this._headers = _this._xmlHttpRequest.getAllResponseHeaders();

            _this._webRequest.completed(Sys.EventArgs.Empty);
            if (_this._xmlHttpRequest != null) {
                _this._xmlHttpRequest.onreadystatechange = Function.emptyMethod;
                _this._xmlHttpRequest = null;
            }
        }
    }

    this._clearTimer = function this$_clearTimer() {
        if (_this._timer != null) {
            window.clearTimeout(_this._timer);
            _this._timer = null;
        }
    }

    this._onTimeout = function this$_onTimeout() {
        if (!_this._responseAvailable) {
            _this._clearTimer();
            _this._timedOut = true;
            _this._xmlHttpRequest.onreadystatechange = Function.emptyMethod;
            _this._xmlHttpRequest.abort();
            _this._webRequest.completed(Sys.EventArgs.Empty);
            _this._xmlHttpRequest = null;
        }
    }
}

function Sys$Net$XMLHttpSyncExecutor$get_timedOut() {
    /// <value type="Boolean"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    return this._timedOut;
}

function Sys$Net$XMLHttpSyncExecutor$get_started() {
    /// <value type="Boolean"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    return this._started;
}

function Sys$Net$XMLHttpSyncExecutor$get_responseAvailable() {
    /// <value type="Boolean"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    return this._responseAvailable;
}

function Sys$Net$XMLHttpSyncExecutor$get_aborted() {
    /// <value type="Boolean"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    return this._aborted;
}

function Sys$Net$XMLHttpSyncExecutor$executeRequest() {
    if (arguments.length !== 0) throw Error.parameterCount();
    this._webRequest = this.get_webRequest();

    if (this._started) {
        throw Error.invalidOperation(String.format(Sys.Res.cannotCallOnceStarted, 'executeRequest'));
    }
    if (this._webRequest === null) {
        throw Error.invalidOperation(Sys.Res.nullWebRequest);
    }

    var body = this._webRequest.get_body();
    var headers = this._webRequest.get_headers();
    this._xmlHttpRequest = new XMLHttpRequest();
    this._xmlHttpRequest.onreadystatechange = this._onReadyStateChange;
    var verb = this._webRequest.get_httpVerb();
    this._xmlHttpRequest.open(verb, this._webRequest.getResolvedUrl(), false); // False to call Synchronously
    if (headers) {
        for (var header in headers) {
            var val = headers[header];
            if (typeof (val) !== 'function')
                this._xmlHttpRequest.setRequestHeader(header, val);
        }
    }

    if (verb.toLowerCase() === 'post') {
        if ((headers === null) || !headers['Content-Type']) {
            this._xmlHttpRequest.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        }

        if (!body) {
            body = '';
        }
    }

    var timeout = this._webRequest.get_timeout();
    if (timeout > 0) {
        this._timer = window.setTimeout(Function.createDelegate(this, this._onTimeout), timeout);
    }
    this._xmlHttpRequest.send(body);
    this._started = true;

    // fix for firefox bug
    //if (/Firefox[\/\s](\d+\.\d+)/.test(navigator.userAgent)) {
    if (!this._responseAvailable && !this._timedOut && this._xmlHttpRequest.readyState === 4)
        this._onReadyStateChange();
    //}
}

function Sys$Net$XMLHttpSyncExecutor$getAllResponseHeaders() {
    /// <returns type="String"></returns>
    if (arguments.length !== 0) throw Error.parameterCount();
    if (!this._responseAvailable) {
        throw Error.invalidOperation(String.format(Sys.Res.cannotCallBeforeResponse, 'getAllResponseHeaders'));
    }

    return this._headers;
}

function Sys$Net$XMLHttpSyncExecutor$get_responseData() {
    /// <value type="String"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    if (!this._responseAvailable) {
        throw Error.invalidOperation(String.format(Sys.Res.cannotCallBeforeResponse, 'get_responseData'));
    }

    return this._responseData;
}

function Sys$Net$XMLHttpSyncExecutor$get_statusCode() {
    /// <value type="Number"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    if (!this._responseAvailable) {
        throw Error.invalidOperation(String.format(Sys.Res.cannotCallBeforeResponse, 'get_statusCode'));
    }

    return this._statusCode;
}

function Sys$Net$XMLHttpSyncExecutor$get_statusText() {
    /// <value type="String"></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    if (!this._responseAvailable) {
        throw Error.invalidOperation(String.format(Sys.Res.cannotCallBeforeResponse, 'get_statusText'));
    }

    return this._statusText;
}

function Sys$Net$XMLHttpSyncExecutor$get_xml() {
    /// <value></value>
    if (arguments.length !== 0) throw Error.parameterCount();
    if (!this._responseAvailable) {
        throw Error.invalidOperation(String.format(Sys.Res.cannotCallBeforeResponse, 'get_xml'));
    }

    var xml = this._responseData;
    if ((!xml) || (!xml.documentElement)) {
        xml = new XMLDOM(this._responseData);
        if ((!xml) || (!xml.documentElement)) {
            return null;
        }
    }
    else if (navigator.userAgent.indexOf('MSIE') !== -1) {
        xml.setProperty('SelectionLanguage', 'XPath');
    }

    if ((xml.documentElement.namespaceURI === 'http://www.mozilla.org/newlayout/xml/parsererror.xml') &&
        (xml.documentElement.tagName === 'parsererror')) {
        return null;
    }

    if (xml.documentElement.firstChild && xml.documentElement.firstChild.tagName === 'parsererror') {
        return null;
    }

    return xml;
}

function Sys$Net$XMLHttpSyncExecutor$abort() {
    if (arguments.length !== 0) throw Error.parameterCount();
    if (!this._started) {
        throw Error.invalidOperation(Sys.Res.cannotAbortBeforeStart);
    }

    if (this._aborted || this._responseAvailable || this._timedOut)
        return;

    this._aborted = true;

    this._clearTimer();

    if (this._xmlHttpRequest && !this._responseAvailable) {
        this._xmlHttpRequest.onreadystatechange = Function.emptyMethod;
        this._xmlHttpRequest.abort();

        this._xmlHttpRequest = null;
        var handler = this._webRequest._get_eventHandlerList().getHandler('completed');
        if (handler) {
            handler(this, Sys.EventArgs.Empty);
        }
    }
}

Sys.Net.XMLHttpSyncExecutor.prototype = {
    get_timedOut: Sys$Net$XMLHttpSyncExecutor$get_timedOut,
    get_started: Sys$Net$XMLHttpSyncExecutor$get_started,
    get_responseAvailable: Sys$Net$XMLHttpSyncExecutor$get_responseAvailable,
    get_aborted: Sys$Net$XMLHttpSyncExecutor$get_aborted,
    executeRequest: Sys$Net$XMLHttpSyncExecutor$executeRequest,
    getAllResponseHeaders: Sys$Net$XMLHttpSyncExecutor$getAllResponseHeaders,
    get_responseData: Sys$Net$XMLHttpSyncExecutor$get_responseData,
    get_statusCode: Sys$Net$XMLHttpSyncExecutor$get_statusCode,
    get_statusText: Sys$Net$XMLHttpSyncExecutor$get_statusText,
    get_xml: Sys$Net$XMLHttpSyncExecutor$get_xml,
    abort: Sys$Net$XMLHttpSyncExecutor$abort
}
Sys.Net.XMLHttpSyncExecutor.registerClass('Sys.Net.XMLHttpSyncExecutor', Sys.Net.WebRequestExecutor);

if (typeof (Sys) != 'undefined') {
    Sys.Application.notifyScriptLoaded();
}