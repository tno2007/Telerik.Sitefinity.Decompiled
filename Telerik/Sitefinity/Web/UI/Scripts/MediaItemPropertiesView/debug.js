Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.MediaItemPropertiesView = function (element) {
    this._fieldControlIds = null;
    this._sectionsIds = null;
    this._requireDataItemFieldControlIds = null;
    this._binder = null;
    this._blankDataItem = null;
    this._isNew = true;
    this._providerName = null;
    this._itemType = null;
    this._uiCulture = null;

    this._parentSelectorFieldControl = null;
    this._targetLibraryId = null;

    this._dataBindSuccessDelegate = null;
    this._handlePageLoadDelegate = null;
    this._itemSavedDelegate = null;
    Telerik.Sitefinity.Web.UI.MediaItemPropertiesView.initializeBase(this, [element]);
};

Telerik.Sitefinity.Web.UI.MediaItemPropertiesView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.MediaItemPropertiesView.callBaseMethod(this, 'initialize');

        if (this._fieldControlIds)
            this._fieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._fieldControlIds);

        if (this._requireDataItemFieldControlIds)
            this._requireDataItemFieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._requireDataItemFieldControlIds);

        if (this._blankDataItem) {
            this._blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(this._blankDataItem);
            if (this._blankDataItem.hasOwnProperty('Id')) {
                delete this._blankDataItem.Id;
            }
        }

        if (this._sectionsIds) {
            this._sectionsIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._sectionsIds);
        }

        this._dataBindSuccessDelegate = Function.createDelegate(this, this._dataBindSuccess);
        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        this._itemSavedDelegate = Function.createDelegate(this, this._itemSavedHandler);
        this._toggleSectionDelegate = Function.createDelegate(this, function () {
            dialogBase.resizeToContent();
        });

        if (this.getParentSelectorFieldControl() && this._targetLibraryId && this._targetLibraryId !== Telerik.Sitefinity.getEmptyGuid()) {
            jQuery(this.getParentSelectorFieldControl().get_element()).hide();
        }

        for (var i = 0, length = this.get_sectionsIds().length; i < length; i++) {
            var section = $find(this.get_sectionsIds()[i]);
            if (section.add_doToggle) {
                section.add_doToggle(this._toggleSectionDelegate);
            }
        }

        Sys.Application.add_load(this._handlePageLoadDelegate);        
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.MediaItemPropertiesView.callBaseMethod(this, 'dispose');
        if (this._dataBindSuccessDelegate) {
            delete this._dataBindSuccessDelegate;
        }
        if (this._handlePageLoad) {
            delete this._handlePageLoad;
        }
        if (this._itemSavedDelegate) {
            delete this._itemSavedDelegate;
        }

        if (this._toggleSectionDelegate) {
            for (var i = 0, length = this.get_sectionsIds().length; i < length; i++) {
                var section = $find(this.get_sectionsIds()[i]);
                if (section.remove_doToggle) {
                    section.remove_doToggle(this._toggleSectionDelegate);
                }
            }

            delete this._toggleSectionDelegate;
        }        
    },

    /* --------------------------------- public methods ---------------------------------- */
    reset: function () {
        this.get_binder().reset();
        if (this.get_uiCulture())
            this.get_binder().set_uiCulture(this.get_uiCulture());
    },

    resetProviderControls: function () {
        for (var i = 0; i < this._fieldControlIds.length; i++) {
            var fieldControl = $find(this._fieldControlIds[i]);
            var fieldControlType = Object.getType(fieldControl);

            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.ISelfExecutableField) ||
                fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.IParentSelectorField) ||
                fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider)) {
                    fieldControl.reset();
            }
        }
    },

    validate: function () {
        var isValid = this._binder.validate();

        var parentId = this.getSelectedParentId();
        if (!parentId || parentId === Telerik.Sitefinity.getEmptyGuid()) {
            isValid = false;
            alert("You must select the library in which the files ought to be uploaded.");
        }

        return isValid;
    },

    getParentSelectorFieldControl: function () {
        if (!this._parentSelectorFieldControl) {
            for (var i = 0; i < this._fieldControlIds.length; i++) {
                var fieldControl = $find(this._fieldControlIds[i]);
                var fieldControlType = Object.getType(fieldControl);
                if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.IParentSelectorField)) {
                    this._parentSelectorFieldControl = fieldControl;
                    break;
                }
            }
        }

        return this._parentSelectorFieldControl;
    },

    getSelectedParentId: function () {
        if (this._targetLibraryId && this._targetLibraryId !== Telerik.Sitefinity.getEmptyGuid()) {
            return this._targetLibraryId;
        }
        return this.getParentSelectorFieldControl().getSelectedParentId();
    },

    rebind: function (providerName) {
        if (providerName) {
            this.set_providerName(providerName);
        }

        for(var i = 0; i < this._fieldControlIds.length; i++)
        {
            var fieldControl = $find(this._fieldControlIds[i]);
            var fieldControlType = Object.getType(fieldControl);

            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.ISelfExecutableField)) {
                if (fieldControl && typeof (fieldControl.set_provider) === 'function') {
                    fieldControl.set_provider(this.get_providerName());
                }
            }
            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.IParentSelectorField)) {
                if (fieldControl && typeof (fieldControl.set_provider) === 'function' && typeof (fieldControl.dataBind) === 'function') {
                    fieldControl.set_provider(this.get_providerName());
                    fieldControl.dataBind();
                }
            }
            if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider)) {
                if (fieldControl && typeof (fieldControl.set_providerName) === 'function') {
                    fieldControl.set_providerName(this.get_providerName());
                }
            }
        }
    },

    allowCreateNewLibrary: function (allow) {
        var selector = this.getParentSelectorFieldControl();
        if (selector) {
            var button = selector.get_createNewLibraryButton();
        }
        if (button) {
            $(button).toggle(allow);
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _dataBindFailure: function (sender, result) {
        alert(result.Detail);
    },
    
    _dataBindSuccess: function (sender, result) {
        this._binder.DataBind(result, {"Id": result.Item.Id});
    },

    _handlePageLoad: function () {
        this._binder.reset();
        this._binder.set_fieldControlIds(this._fieldControlIds);
        this._binder.set_requireDataItemControlIds(this._requireDataItemFieldControlIds);
        this._binder.add_onSaved(this._itemSavedDelegate);

        if (this.get_uiCulture())
            this._binder.set_uiCulture(this.get_uiCulture());
    },

    /* --------------------------------- private methods --------------------------------- */    

    _itemSavedHandler: function (sender, args) {
        var dataItem = this._binder.get_dataItem();
        dialogBase.closeCreated(dataItem);
    },

    _clone: function (obj) {
        var serialized = Sys.Serialization.JavaScriptSerializer.serialize(obj);
        return Sys.Serialization.JavaScriptSerializer.deserialize(serialized);
    },

    /* --------------------------------- properties -------------------------------------- */
    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
    },
    get_sectionsIds: function () {
        return this._sectionsIds;
    },
    set_sectionsIds: function (value) {
        this._sectionsIds = value;
    },
    get_binder: function () {
        return this._binder;
    },
    set_binder: function (value) {
        this._binder = value;
    },
    get_requireDataItemFieldControlIds: function () {
        return this._requireDataItemFieldControlIds;
    },
    set_requireDataItemFieldControlIds: function (value) {
        this._requireDataItemFieldControlIds = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
        if (this.get_binder()) {
            this.get_binder().set_providerName(value);
        }
    },
    get_blankDataItem: function () {
        return this._blankDataItem;
    },
    set_blankDataItem: function (value) {
        this._blankDataItem = value;
    },
    get_itemType: function () {
        return this._itemType;
    },
    set_itemType: function (value) {
        this._itemType = value;
    },
    get_dataItem: function () {
        return this.get_binder()._getJsonData();
    },
    set_dataItem: function (value) {
        this._binder.BindItem({
            Item: this._clone(value),
            ItemType: this.get_itemType()
        });
    },
    get_uiCulture: function () {
        return this._uiCulture;
    },
    set_uiCulture: function (value) {
        this._uiCulture = value;
        if (this.get_binder())
            this.get_binder().set_uiCulture(value);
    }
};

Telerik.Sitefinity.Web.UI.MediaItemPropertiesView.registerClass('Telerik.Sitefinity.Web.UI.MediaItemPropertiesView', Sys.UI.Control);
