Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions");

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor = function(element) {
    //public:
    this._arrCheckedOnLoadClientIDs = null;
    this._arrDenyButtonsColumn = null;
    this._bShowDenyColumnOnStartup = null;
    this._showAdvancedOptionsLinkButtonID = null;
    this._hideAdvancedOptionsLinkButtonID = null;
    this._saveLinkButtonClientID = null;
    this._closeLinkButtonClientID = null;
    this._wcfPermissionArray = null;

    this._dataProviderName = null;
    this._managerClassName = null;
    this._principalType = null;
    this._permissionsSetName = null;

    this._permissionsObjectRootID = null;
    this._principalServiceURL = null;
    this._principalID = null;

    this._securedObjectID = null;
    this._securedObjectType = null;   

    //private:
    this.isDirty = false;

    Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor.prototype = {

    initialize: function () {

        // hack.
        // JQuery will crash IE7 if its ready event is called after we resize the iframe (radwindow)
        // more preciesely, it dies when trying to figure out the boxing model prior to invoking 
        // user-land document ready event subscribers.
        // It so happens that MS Ajax's application load event is fired before that of jquery.
        // Therefore, we 'ensure' that the resize is called after jquery does its internal document ready logic.
        var self = this;
        jQuery(document).ready(function () {
            self._autosizeMe();
        });

        this.isDirty = false;
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor.callBaseMethod(this, "initialize");
        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        var showAdvancedOptionsButton = $get(this._showAdvancedOptionsLinkButtonID);
        var hideAdvancedOptionsButton = $get(this._hideAdvancedOptionsLinkButtonID);
        var saveButton = $get(this._saveLinkButtonClientID);
        var cancelButton = $get(this._closeLinkButtonClientID);

        //check the boxes which need to be checked
        for (var i = 0; i < this._arrCheckedOnLoadClientIDs.length; i++) {
            var checkbox = $get(this._arrCheckedOnLoadClientIDs[i]);
            checkbox.checked = true;
        }
        this.toggleAdvancedOptions(this._bShowDenyColumnOnStartup);

        this._clickCheckbox = Function.createDelegate(this, this.checkbox_click);
        for (var curPermission = 0; curPermission < this._wcfPermissionArray.length; curPermission++) {
            var allowCheckbox = $get(this._wcfPermissionArray[curPermission].AllowControlClientID);
            var denyCheckbox = $get(this._wcfPermissionArray[curPermission].DenyControlClientID);

            $addHandler(allowCheckbox, "click", this._clickCheckbox);
            $addHandler(denyCheckbox, "click", this._clickCheckbox);
        }

        this._clickShowAdvancedDelegate = Function.createDelegate(this, this.clickShowAdvancedOptions);
        $addHandler(showAdvancedOptionsButton, "click", this._clickShowAdvancedDelegate);

        this._clickHideAdvancedDelegate = Function.createDelegate(this, this.clickHideAdvancedOptions);
        $addHandler(hideAdvancedOptionsButton, "click", this._clickHideAdvancedDelegate);

        this._clickSaveDelegate = Function.createDelegate(this, this.clickSave);
        $addHandler(saveButton, "click", this._clickSaveDelegate);

        this._clickCancelDelegate = Function.createDelegate(this, this.clickCancel);
        $addHandler(cancelButton, "click", this._clickCancelDelegate);
    },

    dispose: function () {
        var showAdvancedOptionsButton = $get(this._showAdvancedOptionsLinkButtonID);
        var hideAdvancedOptionsButton = $get(this._hideAdvancedOptionsLinkButtonID);
        var saveButton = $get(this._saveLinkButtonClientID);
        var cancelButton = $get(this._closeLinkButtonClientID);

        $removeHandler(showAdvancedOptionsButton, "click", this._clickShowAdvancedDelegate);
        $removeHandler(hideAdvancedOptionsButton, "click", this._clickHideAdvancedDelegate);
        $removeHandler(saveButton, "click", this._clickSaveDelegate);
        $removeHandler(cancelButton, "click", this._clickCancelDelegate);

        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor.callBaseMethod(this, "dispose");
    },

    onload: function () {
        jQuery("body").addClass("sfSelectorDialog sfOverflowHiddenX");
        this._autosizeMe();
    },

    //class
    wcfPermission: function (entityType, entityName, entityID, entityProviderName, isAllowed, actionName, actionTitle) {
        this.EntityType = entityType;
        this.EntityID = entityID;
        this.EntityName = entityName;
        this.EntityProviderName = entityProviderName;
        this.IsAllowed = isAllowed;
        this.IsDenied = !isAllowed;
        this.ActionName = actionName;
        this.ActionTitle = actionTitle;
    },

    toggleAdvancedOptions: function (bShow) {
        var showAdvancedOptionsButton = $get(this._showAdvancedOptionsLinkButtonID);
        var hideAdvancedOptionsButton = $get(this._hideAdvancedOptionsLinkButtonID);
        for (var i = 0; i < this._arrDenyButtonsColumn.length; i++) {
            var tdElement = $get(this._arrDenyButtonsColumn[i]).parentNode;
            this.setTableCellDisplay(tdElement, bShow);
        }
        showAdvancedOptionsButton.style.display = ((!bShow) ? "inline" : "none");
        hideAdvancedOptionsButton.style.display = ((bShow) ? "inline" : "none");
    },

    setTableCellDisplay: function (cellElement, bShow) {
        if (!bShow)
            $(cellElement).addClass("sfHiddenCell");
        else
            $(cellElement).removeClass("sfHiddenCell");
    },

    clickShowAdvancedOptions: function () {
        this.toggleAdvancedOptions(true);
        return false;
    },

    clickHideAdvancedOptions: function () {
        this.toggleAdvancedOptions(false);
        return false;
    },

    clickSave: function () {
        if (!this.isDirty) {
            this.closeMe(null);
        }
        else {
            //save data
            var keys = new Array();
            keys[keys.length] = this._permissionsSetName;
            keys[keys.length] = this._dataProviderName;

            var params = {
                'permissionObjectRootID': this._permissionsObjectRootID,
                'managerClassName': this._managerClassName,
                'principalID': this._principalID,
                'securedObjectID': this._securedObjectID,
                'securedObjectType': this._securedObjectType,
                'dynamicDataProviderName': this._dynamicDataProviderName
            };

            var saveData = new Array();
            for (var curPermission = 0; curPermission < this._wcfPermissionArray.length; curPermission++) {
                if (this._wcfPermissionArray[curPermission].IsAllowed) {
                    var perm = new this.wcfPermission(
                        this._principalType,
                        this._wcfPermissionArray[curPermission].Email,
                        this._wcfPermissionArray[curPermission].UserID,
                        this._dataProviderName,
                        true,
                        this._wcfPermissionArray[curPermission].ActionName,
                        this._wcfPermissionArray[curPermission].ActionTitle);
                    saveData[saveData.length] = perm;
                }

                if (this._wcfPermissionArray[curPermission].IsDenied) {
                    var perm = new this.wcfPermission(
                        this._principalType,
                        this._wcfPermissionArray[curPermission].Email,
                        this._wcfPermissionArray[curPermission].UserID,
                        this._dataProviderName,
                        false,
                        this._wcfPermissionArray[curPermission].ActionName,
                        this._wcfPermissionArray[curPermission].ActionTitle);
                    saveData[saveData.length] = perm;
                }
            }
            var dataSaver = new Telerik.Sitefinity.Data.ClientManager();

            dataSaver.InvokePut(
                    this._principalServiceURL,
                    params,     //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                    keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                    saveData,
                    this.saveSuccessful,
                    this.saveFailure,
                    this);
        }
    },

    clickCancel: function () {
        this.closeMe(null);
    },

    saveSuccessful: function (sender, data) {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        oWindow.close(Sys.Serialization.JavaScriptSerializer.serialize(sender._wcfPermissionArray));
    },

    saveFailure: function (error) {
        alert(error.Detail);
    },

    checkbox_click: function (e) {

        var counterCheckBox = null;

        for (var curPermission = 0; curPermission < this._wcfPermissionArray.length; curPermission++) {

            if (this._wcfPermissionArray[curPermission].AllowControlClientID == e.target.id) {
                counterCheckBox = $get(this._wcfPermissionArray[curPermission].DenyControlClientID);
                if (e.target.checked) {
                    counterCheckBox.checked = false;
                    this._wcfPermissionArray[curPermission].IsDenied = false;
                }
                this._wcfPermissionArray[curPermission].IsAllowed = e.target.checked;
            }
            if (this._wcfPermissionArray[curPermission].DenyControlClientID == e.target.id) {
                counterCheckBox = $get(this._wcfPermissionArray[curPermission].AllowControlClientID);
                if (e.target.checked) {
                    counterCheckBox.checked = false;
                    this._wcfPermissionArray[curPermission].IsAllowed = false;
                }
                this._wcfPermissionArray[curPermission].IsDenied = e.target.checked;
            }
        }
        this.isDirty = true;
    },

    _autosizeMe: function () {
        /// <summary>Resizes the dialog to fit the content.</summary>
        //var sWin = this.getRadWindow();
        var sWin = this.getRadWindow();
        sWin.autoSize();
        var add = 26;
        if (jQuery.browser.msie)
            add = 40;
        else if (jQuery.browser.safari)
            add = 40;
        sWin.set_height(sWin.get_height() + 26);
    },

    closeMe: function (closeArg) {
        var dialog = this.getRadWindow();
        dialog.close(closeArg);
    },

    getRadWindow: function () {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        return oWindow;
    },

    get_arrCheckedOnLoadClientIDs: function () {
        return this._arrCheckedOnLoadClientIDs;
    },

    set_arrCheckedOnLoadClientIDs: function (value) {
        if (this._arrCheckedOnLoadClientIDs != value) {
            this._arrCheckedOnLoadClientIDs = value;
            this.raisePropertyChanged('arrCheckedOnLoadClientIDs');
        }
    },

    get_arrDenyButtonsColumn: function () {
        return this._arrDenyButtonsColumn;
    },

    set_arrDenyButtonsColumn: function (value) {
        if (this._arrDenyButtonsColumn != value) {
            this._arrDenyButtonsColumn = value;
            this.raisePropertyChanged('arrDenyButtonsColumn');
        }
    },

    get_bShowDenyColumnOnStartup: function () {
        return this._bShowDenyColumnOnStartup;
    },

    set_bShowDenyColumnOnStartup: function (value) {
        if (this._bShowDenyColumnOnStartup != value) {
            this._bShowDenyColumnOnStartup = value;
            this.raisePropertyChanged('bShowDenyColumnOnStartup');
        }
    },

    get_showAdvancedOptionsLinkButtonID: function () {
        return this._showAdvancedOptionsLinkButtonID;
    },

    set_showAdvancedOptionsLinkButtonID: function (value) {
        if (this._showAdvancedOptionsLinkButtonID != value) {
            this._showAdvancedOptionsLinkButtonID = value;
            this.raisePropertyChanged('showAdvancedOptionsLinkButtonID');
        }
    },

    get_hideAdvancedOptionsLinkButtonID: function () {
        return this._hideAdvancedOptionsLinkButtonID;
    },

    set_hideAdvancedOptionsLinkButtonID: function (value) {
        if (this._hideAdvancedOptionsLinkButtonID != value) {
            this._hideAdvancedOptionsLinkButtonID = value;
            this.raisePropertyChanged('hideAdvancedOptionsLinkButtonID');
        }
    },

    get_saveLinkButtonClientID: function () {
        return this._saveLinkButtonClientID;
    },

    set_saveLinkButtonClientID: function (value) {
        if (this._saveLinkButtonClientID != value) {
            this._saveLinkButtonClientID = value;
            this.raisePropertyChanged('saveLinkButtonClientID');
        }
    },

    get_closeLinkButtonClientID: function () {
        return this._closeLinkButtonClientID;
    },

    set_closeLinkButtonClientID: function (value) {
        if (this._closeLinkButtonClientID != value) {
            this._closeLinkButtonClientID = value;
            this.raisePropertyChanged('closeLinkButtonClienID');
        }
    },

    get_wcfPermissionArray: function () {
        return this._wcfPermissionArray;
    },

    set_wcfPermissionArray: function (value) {
        if (this._wcfPermissionArray != value) {
            this._wcfPermissionArray = value;
            this.raisePropertyChanged('wcfPermissionArray');
        }
    },


    ////
    get_dataProviderName: function () {
        return this._dataProviderName;
    },

    set_dataProviderName: function (value) {
        if (this._dataProviderName != value) {
            this._dataProviderName = value;
            this.raisePropertyChanged('dataProviderName');
        }
    },

    get_dynamicDataProviderName: function () {
        return this._dynamicDataProviderName;
    },

    set_dynamicDataProviderName: function (value) {
        if (this._dynamicDataProviderName != value) {
            this._dynamicDataProviderName = value;
        }
    },

    get_managerClassName: function () {
        return this._managerClassName;
    },

    set_managerClassName: function (value) {
        if (this._managerClassName != value) {
            this._managerClassName = value;
            this.raisePropertyChanged('managerClassName');
        }
    },

    get_principalType: function () {
        return this._principalType;
    },

    set_principalType: function (value) {
        if (this._principalType != value) {
            this._principalType = value;
            this.raisePropertyChanged('principalType');
        }
    },

    get_permissionsSetName: function () {
        return this._permissionsSetName;
    },

    set_permissionsSetName: function (value) {
        if (this._permissionsSetName != value) {
            this._permissionsSetName = value;
            this.raisePropertyChanged('permissionsSetName');
        }
    },


    //
    get_permissionsObjectRootID: function () {
        return this._permissionsObjectRootID;
    },

    set_permissionsObjectRootID: function (value) {
        if (this._permissionsObjectRootID != value) {
            this._permissionsObjectRootID = value;
            this.raisePropertyChanged('permissionsObjectRootID');
        }
    },

    get_principalServiceURL: function () {
        return this._principalServiceURL;
    },

    set_principalServiceURL: function (value) {
        if (this._principalServiceURL != value) {
            this._principalServiceURL = value;
            this.raisePropertyChanged('principalServiceURL');
        }
    },

    get_principalID: function () {
        return this._principalID;
    },

    set_principalID: function (value) {
        if (this._principalID != value) {
            this._principalID = value;
            this.raisePropertyChanged('principalID');
        }
    },

    get_securedObjectID: function () {
        return this._securedObjectID;
    },

    set_securedObjectID: function (value) {
        if (this._securedObjectID != value) {
            this._securedObjectID = value;
            this.raisePropertyChanged('securedObjectID');
        }
    },

    get_securedObjectType: function () {
        return this._securedObjectType;
    },

    set_securedObjectType: function (value) {
        if (this._securedObjectType != value) {
            this._securedObjectType = value;
            this.raisePropertyChanged('securedObjectType');
        }
    }
};

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor', Sys.UI.Control);
