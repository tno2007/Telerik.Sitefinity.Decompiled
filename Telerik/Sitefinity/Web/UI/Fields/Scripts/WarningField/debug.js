/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.WarningField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.WarningField.initializeBase(this, [element]);
    this._element = element;
    this._dataItem = null;
    this._fieldWrapper = null;
    this._itemsBinder = null;
}

Telerik.Sitefinity.Web.UI.Fields.WarningField.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.WarningField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.WarningField.callBaseMethod(this, "dispose");
    },


    /* --------------------  public methods ----------- */

    reset: function () {
        this._dataItem = null;
    },


    /* -------------------- events -------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },


    /* -------------------- public methods ----------- */

    dataBind: function () {
        if (this._dataItem) {
            if (this._dataItem.Warnings && this._dataItem.Warnings.length > 0) {
                var wrapper = jQuery(this.get_fieldWrapper());
                var data = { 'Items': this._dataItem.Warnings };
                this._itemsBinder.BindCollection(data);
                wrapper.show();
            }
            else {
                jQuery(this.get_fieldWrapper()).hide();
            }
        }
    },


    /* -------------------- properties ---------------- */

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        this._dataItem = value;
        this.dataBind();
    },

    get_fieldWrapper: function () {
        return this._fieldWrapper;
    },
    set_fieldWrapper: function (value) {
        this._fieldWrapper = value;
    },

    get_itemsBinder: function () {
        return this._itemsBinder;
    },
    set_itemsBinder: function (value) {
        this._itemsBinder = value;
    }

};

Telerik.Sitefinity.Web.UI.Fields.WarningField.registerClass("Telerik.Sitefinity.Web.UI.Fields.WarningField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext, Telerik.Sitefinity.Web.UI.Fields.ICommandField, Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl, Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider);