﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields");

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField = function (element) {
    Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField.initializeBase(this, [element]);
    this._useAllUserProvidersRadio = null;
    this._useSpecificUserProvidersRadio = null;
    this._providersSelector = null;

    this._dataItem = null;

    this._showRadioButtons = null;
    this._lastClickedRadio = null;

    this._openTemplateClickDelegate = null;
    this._templateSelectorDialogCloseDelegate = null;
    this._templateSelectorDialogLoadDelegate = null;
    this._providersRadioClickDelegate = null;
    this._providersSelectionChangedDelegate == null;

    this.MPU_USE_ALL = 1;
    this.MPU_USE_SPECIFIED = 2;

    this._isChanged = false;

    this._updateDataItemOnChange = true;
}

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField.callBaseMethod(this, "initialize");

        if (this._useAllUserProvidersRadio && this._useSpecificUserProvidersRadio) {
            if (this._providersRadioClickDelegate == null) {
                this._providersRadioClickDelegate = Function.createDelegate(this, this._providersRadioClickHandler);
            }
            $addHandler(this._useAllUserProvidersRadio, "click", this._providersRadioClickDelegate, true);
            $addHandler(this._useSpecificUserProvidersRadio, "click", this._providersRadioClickDelegate, true);

            if (this._providersSelectionChangedDelegate == null) {
                this._providersSelectionChangedDelegate = Function.createDelegate(this, this._providersSelectionChangedHandler);
            }
            this.get_providersSelector().add_valueChanged(this._providersSelectionChangedDelegate);
            //            $addHandler(this.get_providersSelector(), "valueChanged", this._providersSelectionChangedDelegate, true);
        }
    },

    dispose: function () {

        if (this._providersRadioClickDelegate) {
            delete this._providersRadioClickDelegate;
        }
        if (this._providersSelectionChangedDelegate) {
            delete this._providersSelectionChangedDelegate;
        }

        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this.get_useAllUserProvidersRadio().select();
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
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    set_dataItem: function (value) {
        this._dataItem = value;
        this._refreshForDataItem(this.get_dataItem());
    },

    isChanged: function () {
        if (this.get_dataItem().Id == this._emptyGuid) {
            return false;
        } else {
            return this._isChanged;
        }

    },

    _refreshForDataItem: function (dataItem) {
        this._updateDataItemOnChange = false;

        try {
            if (dataItem.MembershipProvidersUsage == this.MPU_USE_ALL) {
                jQuery(this.get_useAllUserProvidersRadio()).attr('checked', true);
            } else {
                jQuery(this.get_useSpecificUserProvidersRadio()).attr('checked', true);
            }

            var providersSelector = this.get_providersSelector();
            if (dataItem.SelectedMembershipProviders) {
                var valuesArray = new Array();
                for (var i = 0; i < dataItem.SelectedMembershipProviders.length; i++) {
                    valuesArray.push(dataItem.SelectedMembershipProviders[i].ProviderName);
                }
                providersSelector.set_value(valuesArray);
            } else {
                providersSelector.set_value(new Array());
            }
        } finally {
            this._updateDataItemOnChange = true;
        }

        this._updateUI();
    },

    /* -------------------- events -------------------- */

    add_templateChanged: function (handler) {
        this.get_events().addHandler('templateChanged', handler);
    },

    remove_templateChanged: function (handler) {
        this.get_events().removeHandler('templateChanged', handler);
    },

    raiseTemplateChanged: function (args) {
        var handler = this.get_events().getHandler('templateChanged');
        if (handler) {
            handler(this, args);
        }
    },

    /* -------------------- event handlers ------------ */

    _providersRadioClickHandler: function (e) {
        if (this._lastClickedRadio == e.target) {
            return;
        }

        this._lastClickedRadio = e.target;

        this._updateUI();

        if (this._updateDataItemOnChange == true) {
            this._updateDataItem();
        }
    },

    _providersSelectionChangedHandler: function (e) {
        if (this._updateDataItemOnChange == true) {
            this._updateDataItem();
        }
    },

    /* -------------------- private methods ----------- */

    //Updates the UI based on the
    _updateUI: function () {
        var elm = this.get_providersSelector().get_element();
        if (jQuery(this._useAllUserProvidersRadio).is(":checked")) {
            jQuery(elm).hide();
        }
        else if (jQuery(this._useSpecificUserProvidersRadio).is(":checked")) {
            jQuery(elm).show();
        }
    },

    //Updates the data item based on the current selection in the UI
    _updateDataItem: function () {
        var dataItem = this.get_dataItem();
        if (jQuery(this._useAllUserProvidersRadio).is(":checked")) {
            dataItem.MembershipProvidersUsage = this.MPU_USE_ALL;
            dataItem.SelectedMembershipProviders = new Array();
        } else {
            dataItem.MembershipProvidersUsage = this.MPU_USE_SPECIFIED;
            dataItem.SelectedMembershipProviders = this._getSelectedProviders();
        }

        this._isChanged = true;
    },

    _getSelectedProviders: function () {
        var selectedValues = this.get_providersSelector().get_value();
        if (Array.prototype.isPrototypeOf(selectedValues) == false) {
            selectedValues = [selectedValues];
        }
        var result = new Array();

        for (var i = 0; i < selectedValues.length; i++) {
            var selectedValue = selectedValues[i];
            result.push({ ProviderName: selectedValue });
        }

        return result;
    },

    /* -------------------- properties ---------------- */

    get_dataItem: function () { return this._dataItem; },

    get_useAllUserProvidersRadio: function () { return this._useAllUserProvidersRadio; },
    set_useAllUserProvidersRadio: function (value) { this._useAllUserProvidersRadio = value; },

    get_useSpecificUserProvidersRadio: function () { return this._useSpecificUserProvidersRadio; },
    set_useSpecificUserProvidersRadio: function (value) { this._useSpecificUserProvidersRadio = value; },

    get_providersSelector: function () { return this._providersSelector; },
    set_providersSelector: function (value) { this._providersSelector = value; }

};
Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField.registerClass("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.Fields.UserProvidersField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
