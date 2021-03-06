/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.UserProfiles.Web.UI");

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl = function (element) {
    Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl.initializeBase(this, [element]);

    this._dataItem = null;
    this._profilesData = null;

    this._onLoadDelegate = null;

    this._fieldControlIds = [];
    this._requireDataItemControlIds = [];
    this._binder = null;

    this._isChanged = false;

    this._sectionControls = null;
    this._typeProviders = null;

    this._updateDataItemOnChange = true;
    this._externalProvidersMappings = null;
}

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl.callBaseMethod(this, "initialize");

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        }

        Sys.Application.add_load(this._onLoadDelegate);

    },

    dispose: function () {
        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this.get_binder().reset();
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._value;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        this._value = value;
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    isChanged: function () {
        return true;
    },

    validate: function () {
        return this.get_binder().validate();
    },

    //Updates the data item based on the current selection in the UI
    updateDataItem: function () {
        var jsonData = this.get_binder()._getJsonData();
        var item = jsonData.Item;

        for (var profileType in this._profilesData) {
            var profileData = this._profilesData[profileType];
            for (var prop in item) {
                if (prop == "__type") continue;
                if (profileData.hasOwnProperty(prop)) {
                    profileData[prop] = item[prop];
                }
            }
        }

        this._rebindProfileData("update");
        var dataItem = this.get_dataItem();
        dataItem.ProfileData = Sys.Serialization.JavaScriptSerializer.serialize(this._profilesData);

        this._isChanged = true;
    },

    showHideSection: function (sectionId, show) {
        var section = jQuery('#' + sectionId);
        if (show) {
            section.show();
        } else {
            section.hide();
        }
    },

    isProfileTypeActive: function (profileType) {
        var sectionControls = this.get_sectionControls();
        var sectionId = sectionControls[profileType];
        return $('#' + sectionId).is(':visible');
    },

    refreshForMembershipProvider: function (membershipProvider) {
        var sectionControls = this.get_sectionControls();
        var providersMapping = this.get_typeProviders();
        if (membershipProvider) {
            for (var profileType in sectionControls) {
                var providersList = providersMapping[profileType];
                var show = true;
                if (providersList) {
                    show = jQuery.inArray(membershipProvider, providersList) > -1;
                }

                var sectionId = sectionControls[profileType];
                this.showHideSection(sectionId, show);
            }
        }
    },

    /* -------------------- events -------------------- */

    //    add_templateChanged: function (handler) {
    //        this.get_events().addHandler('templateChanged', handler);
    //    },

    //    remove_templateChanged: function (handler) {
    //        this.get_events().removeHandler('templateChanged', handler);
    //    },

    //    raiseTemplateChanged: function (args) {
    //        var handler = this.get_events().getHandler('templateChanged');
    //        if (handler) {
    //            handler(this, args);
    //        }
    //    },

    /* -------------------- event handlers ------------ */

    // fired when page has been loaded
    _handlePageLoad: function (sender, args) {
        this.get_binder().set_fieldControlIds(this._fieldControlIds);
        this.get_binder().set_requireDataItemControlIds(this._requireDataItemControlIds);
        this.get_binder().set_validationFunction(Function.createDelegate(this, this._validateFieldControl));
    },

    /* -------------------- private methods ----------- */

    //Updates the UI based on the
    _updateUI: function () {

    },

    _validateFieldControl: function (fieldControl) {
        var fieldProfileType = null;
        for (var profileType in this._profilesData) {
            var dataItem = this._profilesData[profileType];
            if (dataItem.hasOwnProperty(fieldControl.get_dataFieldName())) {
                fieldProfileType = profileType;
                break;
            }
        }

        if (this._isFieldReadOnly(profileType, fieldControl.get_fieldName())) {
            return true;
        }

        var isActive = this.isProfileTypeActive(fieldProfileType);
        if (isActive == true) {
            return fieldControl.validate();
        } else {
            return true;
        }
    },

    _refreshForDataItem: function (dataItem) {
        this._profilesData = Sys.Serialization.JavaScriptSerializer.deserialize(dataItem.ProfileData);

        this._updateDataItemOnChange = false;

        try {
            if (dataItem.ProviderName) {
                this.refreshForMembershipProvider(dataItem.ProviderName);
            }

            for (var profileType in this._profilesData) {
                var dataItem = this._profilesData[profileType];
                this.get_binder().BindItem({ Item: dataItem }, false);
            }
        } finally {
            this._updateDataItemOnChange = true;
        }

        this._updateUI();
        this._rebindProfileData("refresh");
    },
    _rebindProfileData: function (command) {
        var hasReadOnlyField = false;
        for (var profileType in this._profilesData) {
	        var isActive = this.isProfileTypeActive(profileType);
	        if(isActive){
		        var profileData = this._profilesData[profileType]
		        var sectionId = this._sectionControls[profileType];
		        var section = $find(sectionId);
		        var controlIds = section.get_fieldControlIds();
                for (var i = 0; i < controlIds.length; i++) {
                    var fieldControl = $find(controlIds[i]);
                    if (fieldControl) {
                        var fieldName = fieldControl.get_fieldName();
                        var isFieldReadonly = this._isFieldReadOnly(profileType, fieldName);
                        
                        if ($.isFunction(fieldControl.get_textElement)) {
                            $(fieldControl.get_textElement()).attr('readonly', isFieldReadonly ? 'readonly' : null);
                            if (isFieldReadonly) {
                                hasReadOnlyField = true;
                            }
                        }

                        switch (command) {
                            case "update":
			                    profileData[fieldName] = fieldControl.get_value();
                                break;
                            case "refresh":
                                if (fieldControl.loadTaxa && fieldControl.reset) {
                                    this._rebindProfileDataTaxonField(fieldControl, profileData[fieldName]);
                                } else {
                                    if (profileData[fieldName] != null) {
                                        fieldControl.set_value(profileData[fieldName]);
                                    }
                                }
                                break;
                        }
                    }
                }
	        }
        }
        if (hasReadOnlyField) {
            $('#externalUserMessage').show();
        }
    },
    _rebindProfileDataTaxonField: function (taxonField, taxonIds) {
        var serviceUrl = taxonField.get_webServiceUrl();
        var urlParams = [];
        var keys = [];
        var data = taxonIds || [];

        if (serviceUrl.indexOf("/FlatTaxon.svc/") !== -1) {
            taxonField._clientManager.InvokePost(
                serviceUrl,
                urlParams,
                keys,
                data,
                this._rebindProfileDataTaxonFieldCallback,
                taxonField._loadTaxaFailure,
                taxonField);
        } else if (serviceUrl.indexOf("/HierarchicalTaxon.svc/") !== -1) {
            taxonField._clientManager.InvokePut(
                serviceUrl + "batchpath/",
                urlParams,
                keys,
                data,
                this._rebindProfileDataTaxonFieldCallback,
                taxonField._loadTaxaFailure,
                taxonField);
        } else {
            taxonField.set_value(data);
        }
    },
    _rebindProfileDataTaxonFieldCallback: function (caller, result, request) {
        var taxonIds = JSON.parse(request._body);
        caller.reset();
        caller.set_value(taxonIds);
    },
    _isFieldReadOnly: function (profileType, fieldName) {
        var maps = this._dataItem.ExternalProviderName ? this._externalProvidersMappings[this._dataItem.ExternalProviderName] : null;
        fieldName = fieldName.toLowerCase()

        if (maps && maps[profileType]) {
            var fields = maps[profileType];
            for (var i = 0; i < fields.length; i++) {
                if (fieldName === fields[i].toLowerCase()) {
                    return true;
                }
            }
        }

        return false;
    },
    /* -------------------- properties ---------------- */

    // gets the reference to the field controls binder
    get_binder: function () {
        return this._binder;
    },
    // sets the reference to the field controls binder
    set_binder: function (value) {
        this._binder = value;
    },

    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
    },

    get_sectionControls: function () {
        return this._sectionControls;
    },
    set_sectionControls: function (value) {
        this._sectionControls = value;
    },

    get_typeProviders: function () {
        return this._typeProviders;
    },
    set_typeProviders: function (value) {
        this._typeProviders = value;
    },

    get_requireDataItemControlIds: function () {
        return this._requireDataItemControlIds;
    },
    set_requireDataItemControlIds: function (value) {
        this._requireDataItemControlIds = value;
    },

    set_dataItem: function (value) {
        this._dataItem = value;
        this._refreshForDataItem(this.get_dataItem());
    },
    get_dataItem: function () { return this._dataItem; },

    set_externalProvidersMappings: function (value) {
        this._externalProvidersMappings = value;
    }

};
Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl.registerClass("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfilesControl", Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
