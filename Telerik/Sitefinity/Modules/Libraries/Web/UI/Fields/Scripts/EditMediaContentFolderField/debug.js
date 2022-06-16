Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField.initializeBase(this, [element]);

    this._element = element;

    this._dataItem = null;
    this._selectedLibraryId = null;
    this._extendedValue = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */
    isChanged: function () {
        return this._value != this._originalValue;
    },

    getSelectedParentId: function () {
        return this._value;
    },
        
    set_value: function (value) {
        if (value && value.Id) {
            this._extendedValue = value;
            if (this._dataItem && this._dataItem.FolderId) {
                value = this._dataItem.FolderId;
            }
            else {
                value = value.Id;
            }
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField.callBaseMethod(this, "set_value", [value]);
    },

    get_value: function () {
        return this._extendedValue;
    },

    // IRequiresDataItem members
    set_dataItem: function (value) {
        this._dataItem = value;
    }
    // end IRequiresDataItem members
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.EditMediaContentFolderField", Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem, Telerik.Sitefinity.Web.UI.IParentSelectorField);