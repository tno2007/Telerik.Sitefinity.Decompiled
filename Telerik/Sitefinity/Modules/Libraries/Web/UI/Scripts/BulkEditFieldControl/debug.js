
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl.initializeBase(this, [element]);
    this._webServiceUrl = null;
    this._contentType = null;
    this._parentType = null;
    this._itemsBinder = null;
    this._dataObjects = [];
    this._callbacks = null;
    this._persistedItemsCount = 0;
    this._itemsToUpdateCount = 0;
    this._provider = "";

    this._binderItemDataBoundDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl.callBaseMethod(this, "initialize");
        if (this._binderItemDataBoundDelegate == null) {
            this._binderItemDataBoundDelegate = Function.createDelegate(this, this._handleBinderItemDataBound);
        }
        this._itemsBinder.add_onItemDataBound(this._binderItemDataBoundDelegate);
    },

    dispose: function () {
        if (this._binderItemDataBoundDelegate) {
            this._itemsBinder.remove_onItemDataBound(this._binderItemDataBoundDelegate);
            delete this._binderItemDataBoundDelegate;
        }
        Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handleBinderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();
        var width = dataItem.Width;
        var height = dataItem.Height;

        if (width && height) {
            // resizing Images thumbnails
            var img = $(args.get_itemElement()).find(".sfBulkEditTmb img");
            if (img && img.length > 0) {
                this._resizeImage(img, width, height, 80);
            }
        }
    },

    /* -------------------- private methods ----------- */

    _loadDataObjects: function (selectedItems) {
        var clientManager = this._itemsBinder.get_manager();
        var urlParams = [];
        urlParams['itemType'] = this._contentType;
        var selectedItemsCount = selectedItems.length;

        while (selectedItemsCount--) {
            var key = selectedItems[selectedItemsCount].Id;
            clientManager.InvokeGet(this._webServiceUrl, urlParams, [key], this._loadDataObjectsSuccess, this._loadDataObjectsFailure, this);
        }
    },

    _updateFieldControls: function (tempData, dataObject, fieldControls) {
        var fieldControlsCount = fieldControls.length;

        while (fieldControlsCount--) {
            var fieldControl = fieldControls[fieldControlsCount];
            if (fieldControl) {
                if (!Object.getType(fieldControl).implementsInterface(Telerik.Sitefinity.Web.UI.ISelfExecutableField)) {
                    var propertyName = fieldControl.get_dataFieldName();
                    var propertyValue = tempData[propertyName];
                    if (propertyValue) {
                        var isArray = Array.prototype.isPrototypeOf(propertyValue);
                        if (isArray) {
                            var length = propertyValue.length;
                            while (length--) {
                                if (dataObject[propertyName].indexOf(propertyValue[length]) == -1) {
                                    dataObject[propertyName].push(propertyValue[length]);
                                }
                            }
                        }
                        else {
                            this._updatePropertyValue(dataObject, propertyName, propertyValue);
                        }
                    }
                }
            }
        }
    },

    _updatePropertyValue: function (dataObject, propertyName, propertyValue) {
        if (dataObject[propertyName] && dataObject[propertyName].hasOwnProperty('Value')) {
            dataObject[propertyName].Value = propertyValue;
            // Hack: Some LString properties is mapped as strings. There is a bug for this. When the bug is fixed this logick should be removed. Bug No. 115560
            if (dataObject[propertyName].hasOwnProperty('PersistedValue')) {
                dataObject[propertyName].PersistedValue = propertyValue;
            }
        } else {
            dataObject[propertyName] = propertyValue;
        }
    },

    _loadDataObjectsSuccess: function (caller, result) {
        caller._dataObjects.push(result);
    },

    _loadDataObjectsFailure: function (result) {
        alert(result.Detail);
    },

    _saveChangesSuccess: function (caller, result) {
        caller._persistedItemsCount++;
        if (caller._persistedItemsCount == caller._itemsToUpdateCount) {
            if (caller._callbacks) {
                caller._callbacks.Success(caller._callbacks.Caller, caller._callbacks.Data);
                caller._callbacks = null;
                caller._persistedItemsCount = 0;
                caller._dataObjects = [];
            }
        }
    },

    _saveChangesFailure: function (result) {
        if (this.Caller._callbacks) {
            this.Caller._callbacks.Failure(result);
            this.Caller._callbacks = null;
        }
        else {
            alert(result.Detail);
        }
    },

    _resizeImage: function (img, w, h, size) {
        if (h > size || w > size) {
            if (h == w) {
                img.attr("height", size);
                img.attr("width", size);
            }
            else if (h > w) {
                img.attr("width", size);
                // IE fix
                img.removeAttr("height");
            }
            else {
                img.attr("height", size);
                // IE fix
                img.removeAttr("width");
            }
        }
    },

    _getObjectById: function (array, id) {
        var i = array.length;

        while (i--) {
            var item = array[i].Item;
            if (item.Id == id) {
                return array[i];
            }
        }

        return null;
    },

    /* -------------------- properties ---------------- */

    // Gets the url of the webservice that is used to manage items asynchronously.
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },

    // Sets the url of the webservice that is used to manage items asynchronously.
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },

    // Gets the reference to the client binder that displays the item.
    get_itemsBinder: function () {
        return this._itemsBinder;
    },

    // Sets the reference to the client binder that displays the items.
    set_itemsBinder: function (value) {
        this._itemsBinder = value;
    },

    // Gets the provider name for the items to retrieve/edit.
    get_provider: function () {
        return this._provider;
    },

    // Sets the provider name for the items to retrieve/edit.
    set_provider: function (value) {
        this._provider = value;
    },

    /* ----------- IBulkEditFieldControl members ------- */

    dataBind: function (selectedItems) {
        this._itemsBinder.set_serviceBaseUrl(this._webServiceUrl);
        var urlParams = [];
        urlParams['itemType'] = this._contentType;
        var filterExpression = '';
        var selectedMediaItems = [];
        var selectedItemsCount = selectedItems.length;

        while (selectedItemsCount--) {
            var selectedItem = selectedItems[selectedItemsCount];
            if (!selectedItem.IsFolder) {
                filterExpression += 'Id == ' + selectedItem.Id + ' OR ';
                selectedMediaItems.push(selectedItem);
            }
        }
        filterExpression = filterExpression.substr(0, filterExpression.length - 4);

        urlParams['filter'] = filterExpression;
        urlParams['provider'] = this._provider;
        this._itemsBinder.set_urlParams(urlParams);
        this._itemsBinder.DataBind();

        this._dataObjects = [];
        this._itemsToUpdateCount = selectedMediaItems.length;
        this._loadDataObjects(selectedMediaItems);
    },

    saveChanges: function (urlParams, tempData, fieldControls, successCallback, failureCallback, caller) {
        var tableView = this._itemsBinder.GetTableView();
        var dataItems = tableView.get_dataItems();
        var clientManager = this._itemsBinder.get_manager();
        var serviceUrl = this._webServiceUrl;
        urlParams['itemType'] = this._contentType;
        if (urlParams['newParentId']) {
            urlParams["parentItemType"] = this._parentType;
            serviceUrl += 'parent/';
        }

        // for each row
        for (var dataItemIndex = 0, dataItemsLen = tableView.get_virtualItemCount(); dataItemIndex < dataItemsLen; dataItemIndex++) {
            var dataItem = dataItems[dataItemIndex].get_dataItem();
            if (dataItem != null) {
                var dataObject = this._getObjectById(this._dataObjects, dataItem.Id);
                if (dataObject) {
                    var oldValues = tableView.extractOldValuesFromItem(dataItemIndex);
                    // create a property bag
                    var propertyBag = this._itemsBinder.GetData(dataItems[dataItemIndex].get_element(), dataItemIndex);
                    for (var i = 0; i < propertyBag.length; i++) {
                        var propertyName = propertyBag[i][0];
                        var propertyValue = propertyBag[i][1];
                        if (propertyValue) {
                            if (oldValues[propertyName] != propertyValue) {
                                this._updatePropertyValue(dataObject.Item, propertyName, propertyValue);
                            }
                        }
                    }

                    this._updateFieldControls(tempData.Item, dataObject.Item, fieldControls);

                    var keys = [];
                    var key = this._itemsBinder.GetItemKey(dataItemIndex);
                    keys.push(key['Id']);
                    if (urlParams['newParentId']) {
                        keys.push(key['ParentId']);
                    }

                    this._callbacks = { Caller: caller, Success: successCallback, Failure: failureCallback, Data: dataObject };
                    clientManager.InvokePut(serviceUrl, urlParams, keys, dataObject, this._saveChangesSuccess, this._saveChangesFailure, this);
                }
            }
        }
    },

    getDataItems: function () {
        return this._dataObjects;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.BulkEditFieldControl", Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl, Telerik.Sitefinity.Web.UI.IBulkEditFieldControl);