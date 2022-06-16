Type.registerNamespace("Telerik.Sitefinity.Web.UI.FieldControls");

/* Field Controls Binder class */
Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder = function () {
    Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder.initializeBase(this);
    this._serviceBaseUrl = null;
    this._providerName = null;
    this._contentId = null;
    this._fieldControlIds = [];
    this._fieldControls = [];
    this._localizableFieldControls = [];
    this._requireDataItemControlIds = [];
    this._requireDataItemFieldControls = [];
    this._bulkEditFieldControlIds = [];
    this._bulkEditFieldControls = [];
    this._compositeFieldControlIds = [];
    this._compositeFieldControls = [];
    this._dataItem = null;
    this._lastModifiedData = null;
    this._changedSelfExecutableFields = null;
    this._tempData = null;
    this._itemType = null;
    this._duplicate = false;
    this._blankDataItem = null;
    this._doNotUseContentItemContext = false;
    this._newParentId = null;
    this._lockedFieldMessage = null;
    this._fieldsInitialState = null;

    this._saveChangesThroughWebserviceDelegate = null;

    //Custom validation function
    this._validationFunction = null;

    this._saveChangesInternalSuccessDelegate = null;
    this._defaultValuesSet = false;
}

Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder.prototype = {

    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder.callBaseMethod(this, 'initialize');
        this._saveChangesInternalSuccessDelegate = Function.createDelegate(this, this._saveChangesInternalSuccess);

        if (this._saveChangesThroughWebserviceDelegate === null) {
            this._saveChangesThroughWebserviceDelegate = Function.createDelegate(this, this._saveChangesThroughWebserviceHandler);
        }

    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder.callBaseMethod(this, 'dispose');
        if (this._saveChangesInternalSuccessDelegate) {
            delete this._saveChangesInternalSuccessDelegate;
        }
        if (this._saveChangesThroughWebserviceDelegate) {
            delete this._saveChangesThroughWebserviceDelegate;
        }
    },

    /* *********************************** Public methods *********************************** */

    add_onSaveChangesThroughWebservice: function (delegate) {
        this.get_events().addHandler('saveChangesThroughWebservice', delegate);
    },
    remove_onSaveChangesThroughWebservice: function (delegate) {
        this.get_events().removeHandler('saveChangesThroughWebservice', delegate);
    },

    // function binds the to a data that will be retrieved from
    // the server
    DataBind: function () {
        alert('In process of implementation.');
    },

    // function binds the to the data item passed to it. No additional
    // calls to the web service are made.
    DataBind: function (dataItem, keys) {

        if (!keys) {
            alert('When binding to an offline item, keys argument must be supplied.');
            return;
        }

        if (keys.length > 1) {
            alert('Composite keys not supported at the moment.');
            return;
        }

        this._dataItem = dataItem;
        if (!keys.Id) {
            alert('Currently the name of the single key must be "Id".');
        }
        this._contentId = keys.Id;
        this.BindItem(this._dataItem);
    },

    // specific to FieldControlsBinder: returns false if validation failed and true if validation passed
    SaveChanges: function (delegates) {
        if (!this.validate()) {
            // TODO: remove these calls, once the IAsync thing is modified to work in proper context
            this._endProcessingHandler();
            return false;
        }

        this._tempData = this._getJsonData();
        this._saveChangesInternal(delegates);
        return true;
    },

    _saveChangesInternal: function (delegates, externalUrlParams) {
        if (this._tempData == null) {
            return;
        }

        var urlParams = new Object();
        for (var key in this.get_urlParams()) {
            urlParams[key] = this.get_urlParams()[key];
        }

        if (externalUrlParams) {
            for (var key in externalUrlParams) {
                urlParams[key] = externalUrlParams[key];
            }
        }

        if (this._changedSelfExecutableFields != null && this._changedSelfExecutableFields.length > 0) {
            var execitableFieldControl = this._changedSelfExecutableFields.shift();
            execitableFieldControl.saveChanges(this._tempData, this._saveChangesInternalSuccessDelegate, this._saveChangesFailure, this);
            return;
        }

        var clientManager = this.get_manager();
        var serviceUrl = this._serviceBaseUrl;
        if (this._providerName != null) {
            urlParams['provider'] = this._providerName;
        }
        if (this._newParentId != null) {
            urlParams['newParentId'] = this._newParentId;
        }

        if (this._duplicate) {
            urlParams['duplicate'] = this._duplicate;
        }
        if (this.get_workflowOperation() != null) {
            urlParams['workflowOperation'] = this.get_workflowOperation();
        }

        if (this.get_contextBag() != null) {
            urlParams['contextBag'] = Sys.Serialization.JavaScriptSerializer.serialize(Telerik.Sitefinity.fixArray(this.get_contextBag()));
        }

        if (delegates == null) {
            delegates = { Success: this._saveChangesSuccess, Failure: this._saveChangesFailure, Caller: this };
        }

        var bulkEditMode = this._bulkEditFieldControls.length > 0;
        if (bulkEditMode) {
            for (var i = 0, length = this._bulkEditFieldControls.length; i < length; i++) {
                var fieldControl = this._bulkEditFieldControls[i];
                if (fieldControl) {
                    fieldControl.get_itemsBinder().set_uiCulture(this._uiCulture);
                    fieldControl.saveChanges(urlParams, this._tempData, this._fieldControls, delegates.Success, delegates.Failure, delegates.Caller);
                }
            }
        }
        else {
            // add global data keys, if any
            var keys = [];
            {
                var globalKeys = this.get_globalDataKeys();
                var globalKeysLen = globalKeys.length;
                for (var i = 0; i < globalKeysLen; i++) {
                    keys.push(globalKeys[i]);
                }
            }
            var parentItemId = this.get_parentItemId();
            if (parentItemId != null) {
                keys.push(parentItemId);
            }
            var contentId = this.get_contentId();
            keys.push(contentId);

            this._tempData = $sitefinity.fixObjectForSerialization(this._tempData);
            if (this._saveChangesThroughWebserviceHandler(this, serviceUrl, keys, urlParams, this._tempData, delegates.Success, delegates.Failure, delegates.Caller).Cancel == false) {
                clientManager.InvokePut(serviceUrl, urlParams, keys, this._tempData, delegates.Success, delegates.Failure, delegates.Caller);
            }

            keys.pop();
        }

        this._tempData = null;
        this._changedSelfExecutableFields = null;
    },

    _saveChangesThroughWebserviceHandler: function (sender, serviceUrl, keys, urlParams, data, successDelegate, failureDelegate, caller) {
        var eventArgs = { 'Cancel': false, 'ServiceUrl': serviceUrl, 'Keys': keys, 'UrlParams': urlParams, 'Data': data, 'SuccessDelegate': successDelegate, 'FailureDelegate': failureDelegate, 'Caller': caller };
        var h = this.get_events().getHandler('saveChangesThroughWebservice');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    validate: function () {
        var fieldControlsCount = this._fieldControls.length;
        var isValid = true;

        //Resets this flag in order to give focus to the first element that has a validation error
        Telerik.Sitefinity.Web.UI.Fields.FieldControl.isValidationMessagedFocused = false;

        while (fieldControlsCount--) {
            var fieldControl = this._fieldControls[fieldControlsCount];
            var isCurrentValid = true;
            var vf = this.get_validationFunction();
            if (vf) {
                isCurrentValid = vf(fieldControl);
            } else {
                isCurrentValid = fieldControl.validate();
            }

            isValid = isCurrentValid && isValid;
        }
        return isValid;
    },

    // sets the javascript object that represents the blank data item (used as a template for JSON generation)
    setBlankDataItem: function (value) {
        this._dataItem = value;
        this._blankDataItem = new Object();
        if (value) {
            if (!value.hasOwnProperty('Item') && !this._doNotUseContentItemContext) {
                this._blankDataItem.Item = value;
            }
            else {
                this._blankDataItem = value;
            }
        }
    },

    _saveChangesInternalSuccess: function (data) {
        this._saveChangesInternal(null, data);
    },

    _saveChangesSuccess: function (target, data, webRequest) {
        this.Caller.set_lastModifiedData(data);
        this.Caller._savedHandler();
        // TODO: remove these calls, once the IAsync thing is modified to work in proper context
        this.Caller._endProcessingHandler();
    },

    _saveChangesFailure: function (result) {

        this.Caller._endProcessingHandler();
        this.Caller._errorHandler(result.Detail);

        alert(result.Detail);
    },

    reset: function (culture) {
        var fieldControlsCount = this._fieldControls.length;
        while (fieldControlsCount--) {
            this._fieldControls[fieldControlsCount].reset();
        }

        if ($.browser) {
            if ($.browser.msie) {
                $(window).scrollTop();
            }
            else {
                $('html, body').animate({ scrollTop: 0 }, 1);
            }
        }
        this.set_uiCulture(culture || null);
        this.set_culture(null);
        this.set_fallbackMode(null);
        this.set_duplicate(false);
    },

    DeleteItem: function (language, version) {
        var clientManager = this.get_manager();
        var serviceUrl = this._serviceBaseUrl;
        var urlParams = [];

        if (language) {
            urlParams['language'] = language;
        }

        if (this._providerName != null) {
            urlParams['provider'] = this._providerName;
        }
        if (version) {
            urlParams['version'] = version;
        }

        var keys = [];
        if (this._dataItem != null) {
            keys.push(this._dataItem.Id);
        }

        clientManager.InvokeDelete(serviceUrl, urlParams, keys, this._deleteSuccess, this._deleteFailure, this);

        keys.pop();
    },

    _deleteSuccess: function (result) {
        this.Caller._deletedHandler();
        // TODO: remove these calls, once the IAsync thing is modified to work in proper context
        this.Caller._endProcessingHandler();
    },

    _deleteFailure: function (result) {
        this.Caller._endProcessingHandler();
        this.Caller._errorHandler(result.Detail);
        this.Caller.DataBind(this.Caller.get_dataItem(), { "Id": "Id" });
        alert(result.Detail);
    },

    /* *********************************** Private methods *********************************** */

    _registerFieldControls: function () {
        this._fieldControls = [];
        this._localizableFieldControls = [];
        var fieldControlCount = this._fieldControlIds.length;
        while (fieldControlCount--) {
            var fieldControl = $find(this._fieldControlIds[fieldControlCount]);
            var fieldControlType = Object.getType(fieldControl);
            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl)) {
                this._localizableFieldControls.push(fieldControl);
            }
            if (typeof fieldControl.set_fieldControlBinder === 'function') {
                fieldControl.set_fieldControlBinder(this);
            }
            this._fieldControls.push(fieldControl);
        }
    },
    _registerRequireDataItemFieldControls: function () {
        this._requireDataItemFieldControls = [];
        var fieldControlCount = this._requireDataItemControlIds.length;
        while (fieldControlCount--) {
            var fieldControl = $find(this._requireDataItemControlIds[fieldControlCount]);
            this._requireDataItemFieldControls.push(fieldControl);
        }
    },

    _registerBulkEditFieldControls: function () {
        this._bulkEditFieldControls = [];
        var fieldControlCount = this._bulkEditFieldControlIds.length;
        while (fieldControlCount--) {
            var fieldControl = $find(this._bulkEditFieldControlIds[fieldControlCount]);
            this._bulkEditFieldControls.push(fieldControl);
        }
    },

    _registerCompositeFieldControls: function () {
        this._compositeFieldControls = [];
        var ids = this._compositeFieldControlIds;
        var fieldControl;
        for (var i = 0, l = ids.length; i < l; i++) {
            fieldControl = $find(ids[i]);
            this._compositeFieldControls.push(fieldControl);
        }
    },

    _clone: function (obj) {
        var clone = jQuery.extend(true, {}, obj); // {};
        this._fixClone(clone, obj);
        return clone;
    },

    _fixClone: function (obj, original) {
        for (var property in obj) {
            var val = original[property];
            if (val && (typeof val == 'object')) {
                if (val.constructor == Date) {
                    obj[property] = val;
                }
                else {
                    this._fixClone(obj[property], val);
                }
            }
        }
    },


    // NOTE: this works only for the first level properties for now
    _getJsonData: function () {
        var itemContext = new Object();
        var relatedDataContext = null;

        // loop through all the field controls

        var dataObject = this._clone(this._dataItem);

        var fieldControlsCount = this._fieldControls.length;
        if (fieldControlsCount == 0) {
            return null;
        }

        while (fieldControlsCount--) {
            var fieldControl = this._fieldControls[fieldControlsCount];
            var fieldControlType = Object.getType(fieldControl);
            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.IParentSelectorField)) {
                if (fieldControl.isChanged()) {
                    this._newParentId = fieldControl.getSelectedParentId();
                }
                else {
                    this._newParentId = null;
                }
            }
            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.ISelfExecutableField)) {
                if (fieldControl.isChanged()) {
                    if (this._changedSelfExecutableFields == null) {
                        this._changedSelfExecutableFields = [];
                    }
                    this._changedSelfExecutableFields.push(fieldControl);
                }
            } else if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.Fields.IRelatedDataField)) {
                if (fieldControl.isChanged()) {
                    var changedContentLinks = fieldControl.getChanges();
                    if (!relatedDataContext) relatedDataContext = [];
                    relatedDataContext.push.apply(relatedDataContext, changedContentLinks);
                }
            }
            else {
                var hasAttribute = fieldControl.hasAttribute('data-sf-serializedata');
                var serializeData = fieldControl.getAttributeValue('data-sf-serializedata');

                if (hasAttribute) {
                    if (serializeData == 'yes') {
                        var propertyNameForSerializeData = fieldControl.get_dataFieldName();
                        var propertyValueForSerializeData = fieldControl.get_value();
                        this._set_BinderPropertyValue(dataObject, propertyNameForSerializeData, propertyValueForSerializeData, itemContext);
                    }
                }
                else {
                    var setValue = true;
                    var propertyName = fieldControl.get_dataFieldName();
                    if (propertyName == "PublicationDate") {
                        // Do not set Publication date when not modified. Otherwise it breaks Schedule for publish.
                        setValue = fieldControl.isChanged();
                    }

                    if (setValue) {
                        var propertyValue = fieldControl.get_value();
                        if (typeof propertyValue === typeof "")
                            propertyValue = propertyValue.trim();
                        
                        this._set_BinderPropertyValue(dataObject, propertyName, propertyValue, itemContext);
                    }
                }
            }
        }

        // Prevent the value for the temp item to replace the value, generated in the upload handler
        var mediaFileUrlNameProp = "MediaFileUrlName";
        if (dataObject.hasOwnProperty(mediaFileUrlNameProp) && dataObject[mediaFileUrlNameProp].PersistedValue === this._dataItem[mediaFileUrlNameProp].PersistedValue) {
            delete dataObject["MediaFileUrlName"]
        }

        // ensure that you have last modified set
        if (dataObject['LastModified'] == null) {
            dataObject['LastModified'] = new Date();
        }
        if (this._doNotUseContentItemContext) {
            return dataObject;
        }
        else {
            itemContext['Item'] = dataObject;
            itemContext['ItemType'] = this._itemType;
            if (relatedDataContext !== null) {
                itemContext['ChangedRelatedData'] = relatedDataContext;
            }
            return itemContext;
        }
    },

    // ClientBinder overridden method
    BindCollection: function () {
        alert('Not supported.');
    },

    // ClientBinder overridden method
    BindItem: function (itemContext, setDefaultValues, isCreateMode) {
        // Bind the default values to the ones from the blank item if needed
        if (!this._defaultValuesSet && !setDefaultValues && this._blankDataItem) {
            this.BindItem(this._blankDataItem, true, isCreateMode);
            this._defaultValuesSet = true;
        }
        var dataItem;
        if (this._doNotUseContentItemContext) {
            dataItem = itemContext;
            this._itemType = null;
            if (typeof dataItem != "undefined" && dataItem != null && dataItem.hasOwnProperty('Id')) {
                this._contentId = dataItem.Id;
            }
        }
        else {
            dataItem = itemContext.Item;
            this._itemType = itemContext.ItemType;
        }

        if (dataItem) {
            if (!setDefaultValues) {
                this._dataItem = dataItem;
                this._fieldChangeNotifier = new Telerik.Sitefinity.Web.UI.FieldChangeNotifier(this, dataItem);
            }
            // Setting the data item to each if the controls that require it
            for (var i = 0, length = this._requireDataItemFieldControls.length; i < length; i++) {
                var fieldControl = this._requireDataItemFieldControls[i];
                if (fieldControl) {
                    if (Object.getType(fieldControl).implementsInterface(Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem)) {
                        // the second parameter is a boolean one specifying whether the data item is the default one.
                        // this can be used by some field controls to decide whether to ignore this method call and the passed data item.
                        fieldControl.set_dataItem(dataItem, setDefaultValues);
                    }
                    if (Object.getType(fieldControl).implementsInterface(Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext)) {
                        fieldControl.set_dataItemContext(itemContext);
                    }
                }
            }

            var fieldControlsCount = this._fieldControls.length;
            if (fieldControlsCount == 0) {
                return null;
            }

            var fieldsStateChanged = false;
            var fieldsNewState = null;

            if (this._fieldsInitialState == null && dataItem.Status == 8) {
                fieldsNewState = {};
                fieldsStateChanged = true;
            }
            else if (this._fieldsInitialState != null && dataItem.Status != 8) {
                fieldsStateChanged = true;
            }

            while (fieldControlsCount--) {
                var fieldControl = this._fieldControls[fieldControlsCount];
                fieldControl.set_isBinding(true);
                fieldControl.set_dataContext(this._dataItem);
                if (Object.getType(fieldControl).implementsInterface(Telerik.Sitefinity.Web.UI.IDataItemAccessField)
                    || typeof fieldControl.set_fieldChangeNotifier == "function") {
                    fieldControl.set_fieldChangeNotifier(this._fieldChangeNotifier);
                }
                if (Object.getType(fieldControl).implementsInterface(Telerik.Sitefinity.Web.UI.Fields.IRelatingDataField)) {
                    fieldControl.dataBind(dataItem, this._itemType, this._provider || this._providerName);
                }
                var propertyName = fieldControl.get_dataFieldName();
                if (propertyName) {
                    if (Object.getType(fieldControl).implementsInterface(Telerik.Sitefinity.Web.UI.Fields.IRelatedDataField)) {
                        //we need to remove CustomFields prefix that is used for pages
                        if (propertyName.startsWith('CustomFields.')) {
                            propertyName = propertyName.replace('CustomFields.', '');
                        }

                        // itemContext.ItemId is used when the item will be duplicated.
                        // itemContext.ChangedRelatedData are the links that the new item will have 
                        var duplicationData = {
                            parentItemId: itemContext.ItemId,
                            relatedDataLinks: itemContext.ChangedRelatedData
                        };

                        // the setDefaultValues parameter is a boolean one specifying whether the data item is the default one.
                        // this can be used by some field controls to decide whether to ignore this method call and the passed data item.
                        fieldControl.dataBind(dataItem, this._itemType, this._provider || this._providerName, propertyName, setDefaultValues, duplicationData);
                    } else {
                        var propertyValue;
                        var source = dataItem;
                        var contextPropertyName = propertyName;
                        //Handle also the case with field control binded to the ItemContext (not the dataItem)
                        //Handles case 1) field binded to a property in the context 2) field is binded to the whole context object
                        propertyValue = this._get_BinderPropertyValue(source, contextPropertyName, itemContext);

                        if (setDefaultValues && (propertyValue || propertyValue === false || propertyValue === 0)) {
                            // Initialize the default control values instead of the control values
                            fieldControl.set_defaultValue(propertyValue, this._dataItem);
                        }
                        else {
                            if (isCreateMode && // create mode
                                (fieldControl.get_defaultValue() || fieldControl.get_defaultValue() === false || fieldControl.get_defaultValue() === 0) && // default value is specified
                                (!propertyValue && propertyValue !== false && propertyValue !== 0)) { // no value is present in the model
                                // use default value in create mode if specified and no value is present in the model
                                fieldControl.set_value(fieldControl.get_defaultValue());
                            }
                            else if (propertyValue !== undefined) {
                                if (propertyValue !== null && propertyName.endsWith(".PersistedValue") && propertyValue.startsWith('$Resources:')) {
                                    fieldControl.set_value(fieldControl.get_defaultValue());
                                } else {
                                    fieldControl.set_value(propertyValue);
                                }
                            }
                        }

                        if (this.get_duplicate() && fieldControl.get_fieldName() === "UrlName" &&
                            Object.getType(fieldControl).inheritsFrom(Telerik.Sitefinity.Web.UI.Fields.MirrorTextField)) {
                            fieldControl.set_ckeckConditionalMirroring(false);
                            fieldControl.set_isToMirror(true);
                        }
                    }

                    if (fieldsStateChanged && !propertyName.endsWith(".PersistedValue") && propertyName != "AvailableLanguages") {
                        var isLocalizable = false;
                        if ((Telerik.Sitefinity.Web.UI.Fields.TextField && fieldControl instanceof Telerik.Sitefinity.Web.UI.Fields.TextField) ||
                            (Telerik.Sitefinity.Web.UI.Fields.DateField && fieldControl instanceof Telerik.Sitefinity.Web.UI.Fields.DateField)) {
                            isLocalizable = fieldControl.get_isLocalizable();
                        }

                        if (!isLocalizable) {
                            var fieldElement = fieldControl.get_element();
                            if (fieldElement) {
                                if (fieldsNewState) {
                                    var initialTitle = $(fieldElement).attr("title");
                                    if (initialTitle !== undefined && initialTitle != '') {
                                        fieldsNewState[propertyName] = $(fieldElement).attr("title");
                                    }
                                    $(fieldElement).addClass("sfDisabledField");
                                    $(fieldElement).find("input").attr("disabled", "disabled");
                                    $(fieldElement).attr("title", this._lockedFieldMessage);
                                }
                                else {
                                    $(fieldElement).removeClass("sfDisabledField");
                                    $(fieldElement).find("input").removeAttr("disabled", "disabled");
                                    var fieldState = this._fieldsInitialState[propertyName];
                                    if (fieldState) {
                                        $(fieldElement).attr("title", fieldState);
                                    }
                                    else {
                                        $(fieldElement).removeAttr("title");
                                    }
                                }
                            }
                        }
                    }

                    fieldControl.set_isBinding(false);
                    fieldControl.raise_dataBound();
                }
            }

            if (fieldsStateChanged) {
                this._fieldsInitialState = fieldsNewState;
            }
        }

        // TODO: remove these calls, once the IAsync thing is modified to work in proper context
        this._endProcessingHandler();
    },

    _get_BinderPropertyValue: function (source, propertyName, itemContext) {

        if (!source) return;
        if (!propertyName) return;

        var propertyValue = null;
        var contextPropertyName = propertyName;

        if (propertyName.startsWith('$Context')) {
            contextPropertyName = propertyName.slice(9);
            source = itemContext;
        }

        if (propertyName.startsWith('$SfAdditionalInfo')) {
            var additionalPropertyName = propertyName.slice(18);
            var additionalInfo = itemContext.SfAdditionalInfo;
            if (additionalInfo != 'undefined' && additionalInfo != null)
                return this._findValueByKey(additionalInfo, additionalPropertyName);

            return;
        }

        var dotIndex = contextPropertyName.indexOf(".");
        if (dotIndex != -1) {

            var currentProperty = propertyName.slice(0, dotIndex);
            var nextProperty = propertyName.slice(dotIndex + 1);
            var newSoruce = source[currentProperty];
            return this._get_BinderPropertyValue(newSoruce, nextProperty);
        }

        if (contextPropertyName) {
            propertyValue = source[contextPropertyName];
        }
        else {
            propertyValue = source;
        }

        if (propertyValue && propertyValue.hasOwnProperty('Value')) {
            propertyValue = source[propertyName].Value;
        }

        return propertyValue;
    },

    _set_BinderPropertyValue: function (source, propertyName, value, itemContext) {

        if (!source) return;
        if (!propertyName) return;

        var contextPropertyName = propertyName;

        if (propertyName && propertyName.indexOf("$Context") == 0) {
            contextPropertyName = propertyName.slice(9);
            if (contextPropertyName) {
                itemContext[contextPropertyName] = value;
            }
        }

        if (propertyName && propertyName.indexOf("$SfAdditionalInfo") == 0) {
            var additionalPropertyName = propertyName.slice(18);
            if (additionalPropertyName) {
                if (typeof (itemContext.SfAdditionalInfo) === 'undefined')
                    itemContext.SfAdditionalInfo = [];

                this._setValueByKey(itemContext.SfAdditionalInfo, additionalPropertyName, value);
                return;
            }
        }

        var dotIndex = contextPropertyName.indexOf(".");
        if (dotIndex != -1) {

            var currentProperty = propertyName.slice(0, dotIndex);
            var nextProperty = propertyName.slice(dotIndex + 1);
            if (typeof source[currentProperty] === 'undefined') {
                source[currentProperty] = {};
            }
            var newSource = source[currentProperty];
            if (newSource == null) {
                newSource = {};
                source[currentProperty] = newSource;
            }
            this._set_BinderPropertyValue(newSource, nextProperty, value, itemContext);
            return;
        }

        if (source[propertyName] && source[propertyName].hasOwnProperty('Value')) {
            source[propertyName].Value = value;
            return;
        }

        if (contextPropertyName) {
            source[contextPropertyName] = value;
        }
        else {
            source = value;
        }
    },

    getFieldControlByDataFieldName: function (dataFieldName) {
        if (dataFieldName && dataFieldName != '') {
            var fieldControlCount = this._fieldControls.length;
            for (var fieldIndex = 0; fieldIndex < fieldControlCount; fieldIndex++) {
                if (this._fieldControls[fieldIndex].get_dataFieldName() == dataFieldName)
                    return this._fieldControls[fieldIndex];
            }
        }
        return undefined;
    },

    getFieldControlByFieldName: function (name) {
        if (name) {
            var fields = this._fieldControls;
            var cFields = this._compositeFieldControls;
            var field, i, l;
            for (i = 0, l = cFields.length; i < l; i++) {
                field = cFields[i];
                if (field.get_fieldName() == name)
                    return field;
            }
            for (i = 0, l = fields.length; i < l; i++) {
                field = fields[i];
                if (field.get_fieldName() == name)
                    return field;
            }
        }
        return null;
    },

    // returns if any of the field controls has modified the original values it was bound with
    hasChangedFieldValues: function () {
        var fieldControlsCount = this._fieldControls.length;
        if (fieldControlsCount == 0) {
            return false;
        }
        while (fieldControlsCount--) {

            var fieldControl = this._fieldControls[fieldControlsCount];
            if (fieldControl.isChanged()) {
                return true;
            }
        }
        return false;
    },

    _findValueByKey: function (dictionary, key) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key)
                return item.Value;
        }

        return null;
    },

    _setValueByKey: function (dictionary, key, value) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key) {
                item.Value = value;
                return;
            }
        }

        dictionary.push({ Key: key, Value: value });
    },

    /* *********************************** Properties *********************************** */

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder.callBaseMethod(this, 'set_uiCulture', [culture]);
        var localizableControlsCount = this._localizableFieldControls.length;
        while (localizableControlsCount--) {
            var localizableControl = this._localizableFieldControls[localizableControlsCount];
            localizableControl.set_uiCulture(culture);
        }
    },

    // gets the base service url used for binding and saving
    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },
    // sets the base service url used for binding and saving
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },
    // gets the name of the provider that should be used for data related work
    get_providerName: function () {
        return this._providerName;
    },
    // sets the name of the provider that should be used for data related work
    set_providerName: function (value) {
        this._providerName = value;
    },
    // gets the id of the content to which the binder is bound
    get_contentId: function () {
        if (this._contentId == null) {
            this._contentId = this.GetEmptyGuid();
        }
        return this._contentId;
    },
    // sets the id of the content to which the binder is bound
    set_contentId: function (value) {
        this._contentId = value;
    },
    // gets the data item to which the binder is bound
    get_dataItem: function () {
        return this._dataItem;
    },
    // sets the data item to which the binder is bound
    set_dataItem: function (value) {
        this._dataItem = value;
    },
    // gets the array of the field control ids this binder is binding to
    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    // sets the array of the field control ids this binder is binding to
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
        this._registerFieldControls();
    },
    get_lastModifiedDataItem: function () {
        if (this.get_lastModifiedData())
            return (this.get_lastModifiedData().hasOwnProperty('Item')) ? this.get_lastModifiedData().Item : this.get_lastModifiedData();
        return null;
    },

    get_lastModifiedData: function () {
        return this._lastModifiedData;
    },
    set_lastModifiedData: function (value) {
        this._lastModifiedData = value;
    },

    get_validationFunction: function () {
        return this._validationFunction;
    },
    set_validationFunction: function (value) {
        this._validationFunction = value;
    },

    // IDs of the field controls which require the current data item when binding
    get_requireDataItemControlIds: function () {
        return this._requireDataItemControlIds;
    },
    set_requireDataItemControlIds: function (value) {
        this._requireDataItemControlIds = value;
        this._registerRequireDataItemFieldControls();
    },

    // Client IDs of the bulk edit field controls
    get_bulkEditFieldControlIds: function () {
        return this._bulkEditFieldControlIds;
    },

    set_bulkEditFieldControlIds: function (value) {
        this._bulkEditFieldControlIds = value;
        this._registerBulkEditFieldControls();
    },

    // Gets a value indicating if the dataItem will be duplicated.
    get_duplicate: function () {
        return this._duplicate;
    },

    // Sets a value indicating if the dataItem will be duplicated
    set_duplicate: function (value) {
        this._duplicate = value;
    },

    // Client IDs of the composite field controls
    get_compositeFieldControlIds: function () {
        return this._compositeFieldControlIds;
    },
    set_compositeFieldControlIds: function (value) {
        this._compositeFieldControlIds = value;
        this._registerCompositeFieldControls();
    }
};

Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder.registerClass('Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder', Telerik.Sitefinity.Web.UI.ClientBinder);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
