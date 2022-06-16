/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField.initializeBase(this, [element]);
    this._ipAddressLabelId = null;
    this._submittedOnLabelId = null;
    this._usernameLabelId = null;
    this._dataItemContext = null;

    this._emptyGuid = "00000000-0000-0000-0000-000000000000";

    this._baseBackendUrl = null;

    this._openTemplateClickDelegate = null;
    this._templateSelectorDialogCloseDelegate = null;
    this._templateRadioClickDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField.callBaseMethod(this, "initialize");


    },

    dispose: function () {


        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField.callBaseMethod(this, "reset");

        var ipAddressLabel = jQuery(this.get_ipAddressLabelId()).get(0);
        ipAddressLabel.innerHTML = "";

        var submittedOnLabel = jQuery(this.get_submittedOnLabelId()).get(0);
        submittedOnLabel.innerHTML = "";
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._value;
    },

    // Sets the value of the text field control depending on DisplayMode.
    // NOTE: When the value contains empty guid, this means that the field is from a "Create Page" dialog, not "Edit".
    //       If the value is null, this means that "No template" is selected for the page.
    set_value: function (value) {
        this._value = value;
    },


    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */



    /* -------------------- private methods ----------- */


    /* -------------------- properties ---------------- */

    get_ipAddressLabelId: function () { return this._ipAddresslabelId; },
    set_ipAddressLabelId: function (value) { this._ipAddresslabelId = value; },

    get_submittedOnLabelId: function () { return this._submittedOnLabelId; },
    set_submittedOnLabelId: function (value) { this._submittedOnLabelId = value; },

    get_usernameLabelId: function () { return this._usernameLabelId; },
    set_usernameLabelId: function (value) { this._usernameLabelId = value; },

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        this.reset();
        this._dataItemContext = value;

        var ipAddressLabel = jQuery(this.get_ipAddressLabelId()).get(0);
        ipAddressLabel.innerHTML = value.Item.IpAddress;

        var submittedOnLabel = jQuery(this.get_submittedOnLabelId()).get(0);
        submittedOnLabel.innerHTML = value.Item.SubmittedOn.sitefinityLocaleFormat("MMMM dd, yyyy | hh:mm tt");

        var usernameLabel = jQuery(this.get_usernameLabelId()).get(0);
        usernameLabel.innerHTML = value.Item.Username;
    }

};
Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormEntryDetailsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext);
