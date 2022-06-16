Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions");

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList = function (element) {
    //private properties (retrieved from server, inaccessible)
    this._permissionsSelectionDialogID = null;
    this._principalServiceURL = null;
    this._userOrRoleNameLabelID = null;
    this._mainPermissionsTablePanelID = null;
    this._tableBodyToUpdate = null;
    this._listedButtonPermissionSets = null;
    this._generalPermissionSetName = null;
    this._loadingProgressPanelID = null;

    //internal properties
    this.listedModulesArray = new Array();
    this.listedModuleUniqueId = 0;
    this._clientInitialized = false;
    this._isCurrentPrincipalAdministrator = false;
    this._loadingLastTable = false;

    this._multiSiteMode = null;
    this._sitesUsageRadWindowID = null;    
    this._sitesUsageSingleLabelText = null;
    this._sitesUsageMultipleLabelText = null;

    //publicly accessible attributes
    this._principalID = null;
    this._managerClassName = null;
    this._dataProviderName = null;
    this._permissionsSetName = null;
    this._showPrincipalName = null;
    this._securedObjectID = null;
    this._securedObjectType = null;
    this._principalName = null;
    this._bindOnLoad = null;
    this._showPermissionSetNameTitle = true;
    this._showGeneralPermissionSetTitles = true;

    // event delegates
    this._clientInitializedDelegate = null;

    Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList.prototype = {

    // ------------------------------------- Initialization -------------------------------------

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList.callBaseMethod(this, "initialize");
        this.listedModuleUniqueId = 0;
        this.listedModulesArray = new Array();
        this._listedButtonPermissionSets = new Array();

        //register events
        if (this._clientInitializedDelegate === null) {
            this._clientInitializedDelegate = Function.createDelegate(this, this._clientInitializedHandler);
        }

        Sys.Application.add_load(Function.createDelegate(this, this.onload));
    },

    dispose: function () {
        // Clean up events
        if (this._clientInitializedDelegate) {
            delete this._clientInitializedDelegate;
        }
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList.callBaseMethod(this, "dispose");
    },

    onload: function () {
        //if we need to bind on load, let the loading complete, then the "onInitialized" will be invoked by the callback
        if (this.get_bindOnLoad())
            this.dataBind();
        else
            this._clientInitializedHandler();
    },

    // ------------------------------------- Public methods -------------------------------------

    dataBind: function () {
        this._showLoadingPanel(true);
        this._setActivePrincipal(this._principalID, this._dataProviderName);
    },

    // ---------------------------------------- Events -----------------------------------------
    add_onClientInitialized: function (delegate) {
        this.get_events().addHandler('onClientInitialized', delegate);
    },

    remove_onClientInitialized: function (delegate) {
        this.get_events().removeHandler('onClientInitialized', delegate);
    },

    _clientInitializedHandler: function () {
        // this event can be fired only once per instance
        if (this._clientInitialized == false) {
            this._clientInitialized = true;
            var h = this.get_events().getHandler('onClientInitialized');
            if (h) h(this, Sys.EventArgs.Empty);
        }
    },

    // ------------------------------------- client-side classes -------------------------------------
    //class
    listedModule: function (uniqueModuleId, moduleClientId, selectboxClientId, moduleProvidersData, securedObjectId, moduleTitle) {
        this.UniqueModuleId = uniqueModuleId;
        this.ModuleClientId = moduleClientId;
        this.ModuleProvidersData = moduleProvidersData;
        this.SelectboxClientId = selectboxClientId;
        this.SecuredObjectId = securedObjectId;
        this.ModuleTitle = moduleTitle;
    },

    //class
    changeButtonPermissionSetName: function (btnClientId, permissionSetName, permissionSetModuleOrdinal) {
        this.BtnClientId = btnClientId;
        this.PermissionSetName = permissionSetName;
        this.PermissionSetModuleOrdinal = permissionSetModuleOrdinal;
    },

    // ------------------------------------- Internal utility functions -------------------------------------

    _showLoadingPanel: function (bShow) {
        this._toggleControlDisplayById(this._loadingProgressPanelID, bShow);
        this._toggleControlDisplayById(this._mainPermissionsTablePanelID, !bShow);
    },

    _toggleControlDisplayById: function (ctlElementID, bIsDisplayed) {
        var ctlElement = $get(ctlElementID);
        if (ctlElement != null)
            this._toggleControlDisplay(ctlElement, bIsDisplayed);
    },

    _createmoduleProviderSection: function (moduleTitle, providersData) {
        var displayModuleTitle = this._getModuleDisplayTitle(moduleTitle);

        var permissionsPanel = $get(this._mainPermissionsTablePanelID).getElementsByTagName("div")[0];
        var moduleProviderSectionTemplate = new Sys.UI.Template($('#moduleProviderSectionTemplate').get(0));
        var moduleProviderSectionInstance = moduleProviderSectionTemplate.instantiateIn(permissionsPanel, null, null, this.listedModuleUniqueId);

        var moduleTitleLabel = $(permissionsPanel).find("#moduleTitle" + moduleProviderSectionInstance.index).get(0);
        var slcProviderSelector = $(permissionsPanel).find("#slcProviderSelector" + moduleProviderSectionInstance.index).get(0);
        var pnlModuleSelector = $(permissionsPanel).find("#pnlModuleSelector" + moduleProviderSectionInstance.index).get(0);
        $(permissionsPanel).find("#pnlPermissionSetTables" + moduleProviderSectionInstance.index).get(0);
        var mainModuleArea = $(permissionsPanel).find("#mainModuleArea" + moduleProviderSectionInstance.index).get(0);
        this._setLabelText(moduleTitleLabel, displayModuleTitle);

        var securedObjectId = null;
        if (providersData.length > 0)
            securedObjectId = providersData[0].SecuredObjectId;

        this.listedModulesArray[this.listedModulesArray.length] = new this.listedModule(this.listedModuleUniqueId, mainModuleArea.id, slcProviderSelector.id, providersData, securedObjectId, displayModuleTitle);
        this.listedModuleUniqueId++;
        this._clearSelectBox(slcProviderSelector);
        for (var provider = 0; provider < providersData.length; provider++) {
            var curProvider = providersData[provider];
            this._addOptionToSelect(slcProviderSelector, curProvider.ProviderTitle, curProvider.ProviderId);
        }
        pnlModuleSelector.style.display = (providersData.length < 2) ? "none" : "inline-block";
    },

    _clearSelectBox: function (slcElement) {
        var options = slcElement.getElementsByTagName("option");
        for (var i = options.length - 1; i >= 0; i--) {
            slcElement.removeChild(options[i]);
        }
    },

    _addOptionToSelect: function (slcElement, text, value) {
        var optn = document.createElement("OPTION");
        optn.text = text;
        optn.value = value;
        slcElement.options.add(optn);
    },

    _providerChanged: function (e) {
        var slc = null;
        if ((typeof e != "undefined") && (typeof e.target != "undefined"))
            slc = e.target;
        else if (typeof event.srcElement != "undefined")
            slc = event.srcElement;

        var methodToCallWhenDone = this._getPermissionSetsSuccess;
        if (!this._clientInitialized)
            methodToCallWhenDone = this._getPermissionSetsSuccessAndInit;

        this._getProviderData(slc, methodToCallWhenDone);        
    },

    _getProviderData: function (providerSelectBox, doneMethodDelegate) {
        var module = null;
        var provider = null;
        for (var curModule = 0; curModule < this.listedModulesArray.length; curModule++) {
            if (this.listedModulesArray[curModule].SelectboxClientId == providerSelectBox.id) {
                module = this.listedModulesArray[curModule];
                break;
            }
        }
        if (module != null) {
            for (var curProvider = 0; curProvider < module.ModuleProvidersData.length; curProvider++) {
                if (providerSelectBox.value == module.ModuleProvidersData[curProvider].ProviderId) {
                    provider = module.ModuleProvidersData[curProvider];
                    break;
                }
            }
            if (provider != null) {
                var clientManager = new Telerik.Sitefinity.Data.ClientManager();

                var keys = new Array();
                keys[keys.length] = "GetPermissionSets";

                var urlParams = {
                    'permissionsSetName': provider.PermissionSetName, //""
                    'dataProviderName': provider.ProviderName,
                    'dynamicDataProviderName': provider.DynamicDataProviderName,
                    'managerClassName': provider.ManagerType,
                    'principalID': this._principalID,
                    'securedObjectID': provider.SecuredObjectId,
                    'securedObjectType': provider.SecuredObjectType
                };

                clientManager.InvokeGet(
                    this._principalServiceURL,
                    urlParams,  //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                    keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                    Function.createDelegate(this, doneMethodDelegate),
                    Function.createDelegate(this, this._getPermissionSetsFailure),
                    this);

                if (this._multiSiteMode && module.ModuleProvidersData.length > 0) {
                    var keys = new Array();
                    keys[keys.length] = "GetProviderUsage";                    

                    urlParams = {
                        'dataProviderName': provider.DynamicDataProviderName ? provider.DynamicDataProviderName : provider.ProviderName,
                        'managerClassName': provider.ManagerType,
                        'securedObjectId': provider.SecuredObjectId,
                        'securedObjectType': provider.SecuredObjectType, 
                        'dynamicModuleTitle': this._getModuleTitle(provider.SecuredObjectId)
                    };

                    if (urlParams != null) {
                        clientManager.InvokeGet(
                                    this._principalServiceURL,
                                    urlParams,  //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                                    keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                                    Function.createDelegate(this, this._getProviderUsageSuccess),
                                    Function.createDelegate(this, this._getProviderUsageFail),
                                    this);
                    }
                }
            }
        }
    },

    _getPermissionSetsSuccessAndInit: function (sender, commandArgs) {
        this._isCurrentPrincipalAdministrator = commandArgs.isCurrentPrincipalAdministrator;
        var permissionData = commandArgs.Items;
        this._buildPermissionSetsTable(sender, permissionData, commandArgs, commandArgs.SecuredObjectId);
        if (!this._clientInitialized)
            this._clientInitializedHandler();
        if (this._loadingLastTable)
            this._showLoadingPanel(false);
    },

    _getPermissionSetsSuccess: function (sender, commandArgs) {
        this._isCurrentPrincipalAdministrator = commandArgs.isCurrentPrincipalAdministrator;
        var permissionData = commandArgs.Items;
        this._buildPermissionSetsTable(sender, permissionData, commandArgs, commandArgs.SecuredObjectId);
        if (this._loadingLastTable)
            this._showLoadingPanel(false);
    },

    _getPermissionSetsFailure: function (error) {
        alert(error.Detail);
    },

    _getProviderUsageSuccess: function (sender, commandArgs) {
        var module = this._getModule(commandArgs.SecuredObjectId);        

        var permissionsPanel = $get(sender._mainPermissionsTablePanelID).getElementsByTagName("div")[0];
        var labelIdFormat = 'usedIn' + module.UniqueModuleId;
        var hyperLinkIdFormat = 'sitesUsageLink' + module.UniqueModuleId;
        var sitesUsageLabel = $(permissionsPanel).find("[id$=" + labelIdFormat + "]").get(0);
        var sitesUsageLink = $(permissionsPanel).find("[id$=" + hyperLinkIdFormat + "]").get(0);

        var sitesCount = commandArgs.Items.length; 
        if (sitesCount > 1) {
            sitesUsageLabel.style.display = '';
            sitesUsageLink.style.display = '';
            sitesUsageLink.innerText = sitesCount - 1 + " " + (sitesCount == 2 ? this._sitesUsageSingleLabelText : this._sitesUsageMultipleLabelText);
            $addHandler(sitesUsageLink, "click", Function.createDelegate(this, this._openSiteUsageWindow));
        }
        else {
            sitesUsageLabel.style.display = 'none';
            sitesUsageLink.style.display = 'none';
        }
    },

    _getProviderUsageFail: function () {
        alert("failed");
    },

    _clearActionListTables: function () {
        var permissionsPanel = $get(this._mainPermissionsTablePanelID).getElementsByTagName("div")[0];
        this._removeChildren(permissionsPanel);
    },


    _addPermissionRow: function (tblBody, actionName, isAllowed, isDenied) {
        var tabelRowTemplate = new Sys.UI.Template($('#sfPermissionsTableRowTemplate').find("tbody").get(0));
        var row = tabelRowTemplate.instantiateIn(tblBody, null, null, this.listedModuleUniqueId);
        this.listedModuleUniqueId++;
        var actNameLabel = $(tblBody).find('#' + "spanActionName" + row.index).get(0);
        this._setLabelText(actNameLabel, actionName);
        this._toggleControlVisibility($(tblBody).find('#' + "spanActionAllowed" + row.index).get(0), isAllowed);
        this._toggleControlVisibility($(tblBody).find('#' + "spanActionDenied" + row.index).get(0), isDenied);
    },

    _setActivePrincipal: function (principalID, providerName) {
        this._principalID = principalID;
        this._getPermissionModulesForPrincipal(principalID, providerName);
    },

    _setLabelText: function (LabelElement, newText) {
        if (typeof LabelElement.textContent != "undefined")
            LabelElement.textContent = newText;

        if (typeof LabelElement.innerText != "undefined")
            LabelElement.innerText = newText;
    },

    _getPermissionModulesForPrincipal: function (principalID, providerName) {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var keys = new Array();
        keys[keys.length] = "GetModules";

        var urlParams = {
            'dataProviderName': this._dataProviderName,
            'managerClassName': this._managerClassName,
            'securedObjectID': this._securedObjectID
        };

        clientManager.InvokeGet(
            this._principalServiceURL,
            urlParams,  //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
            keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
            Function.createDelegate(this, this._getPermissionModulesSuccess),
            Function.createDelegate(this, this._getPermissionModulesFailure),
            this);
    },

    _getPermissionModulesSuccess: function (sender, commandArgs) {
        this._reBuildPermissionModulesList(sender, commandArgs.Items, commandArgs);
    },

    _reBuildPermissionModulesList: function (sender, permissionsArray, args) {
        this.listedModulesArray = new Array();
        this._clearActionListTables();

        if ((!this._clientInitialized) && (permissionsArray.length == 0))
            this._clientInitializedHandler();
        this._loadingLastTable = false;
        for (var module = 0; module < permissionsArray.length; module++) {
            var curModule = permissionsArray[module];
            this._createmoduleProviderSection(curModule.ModuleTitle, curModule.Providers/*, methodToCallWhenDone*/);
        }
        for (var module = 0; module < this.listedModulesArray.length; module++) {
            var methodToCallWhenDone = this._getPermissionSetsSuccess;
            if ((!this._clientInitialized) && (module == permissionsArray.length - 1))
                methodToCallWhenDone = this._getPermissionSetsSuccessAndInit;
            if (module == permissionsArray.length - 1)
                this._loadingLastTable = true;

            var permissionsPanel = $get(sender._mainPermissionsTablePanelID).getElementsByTagName("div")[0];
            var moduleProviderBox = $(permissionsPanel).find('#' + this.listedModulesArray[module].SelectboxClientId).get(0)

            moduleProviderBox.onchange = Function.createDelegate(this, this._providerChanged);
            this._getProviderData(moduleProviderBox, methodToCallWhenDone);
        }
    },

    _buildPermissionSetsTable: function (sender, permissionSetsArray, args, securedObjectId) {
        var module = null;
        var provider = null;
        for (var curPermissionSet = 0; curPermissionSet < permissionSetsArray.length; curPermissionSet++) {
            for (var curModule = 0; curModule < this.listedModulesArray.length; curModule++) {
                for (var curProvider = 0; curProvider < this.listedModulesArray[curModule].ModuleProvidersData.length; curProvider++) {
                    if ((this.listedModulesArray[curModule].ModuleProvidersData[curProvider].ProviderId == permissionSetsArray[curPermissionSet].ProviderId)
                        && (this.listedModulesArray[curModule].ModuleProvidersData[curProvider].SecuredObjectId == securedObjectId))
                    {
                        module = this.listedModulesArray[curModule];
                        provider = this.listedModulesArray[curModule].ModuleProvidersData[curProvider];
                        break;
                    }
                }
            }

            if (module != null) {
                var permissionsPanel = $get(sender._mainPermissionsTablePanelID).getElementsByTagName("div")[0];
                var moduleClient = $(permissionsPanel).find('#' + module.ModuleClientId).get(0)
                var permissionsTableTemplate = new Sys.UI.Template($('#sfPermissionsTableTemplate').get(0));
                var tablesArea = $(moduleClient).find("#pnlPermissionSetTables" + module.UniqueModuleId).get(0);
                this._removeChildren(tablesArea);
                for (var curPermissionSet = 0; curPermissionSet < permissionSetsArray.length; curPermissionSet++) {
                    var moduleProviderSectionInstance = permissionsTableTemplate.instantiateIn(tablesArea, null, null, this.listedModuleUniqueId);
                    this.listedModuleUniqueId++;
                    var lblPermissionSetName = $(tablesArea).find("#lblPermissionSetName" + moduleProviderSectionInstance.index).get(0);
                    var lnkChange = $(tablesArea).find("#lnkChange" + moduleProviderSectionInstance.index).get(0);

                    {
                        var btnChange = $(tablesArea).find("#btnChange" + moduleProviderSectionInstance.index).get(0);
                        var showChangeBtn = Array.contains(args.EditablePermissionSets, permissionSetsArray[curPermissionSet].PermissionSetName);
                        Sys.UI.DomElement.setVisible(btnChange, showChangeBtn);
                    }

                    this._listedButtonPermissionSets[this._listedButtonPermissionSets.length] = new this.changeButtonPermissionSetName(lnkChange.id, permissionSetsArray[curPermissionSet].PermissionSetName, curPermissionSet);

                    this._clickDelegate = Function.createDelegate(this, this._openUserPermissionsEditor);
                    //administrators shouldn't change anything, they are always allowed everything and shouldn't be denied.
                    if (this._isCurrentPrincipalAdministrator)
                        lnkChange.parentNode.style.visibility = "hidden";
                    else {
                        lnkChange.parentNode.style.visibility = "visible";
                        $addHandler(lnkChange, "click", this._clickDelegate);
                    }

                    var showLabel = false;
                    var permSetName = permissionSetsArray[curPermissionSet].PermissionSetName;
                    if (this.get_showPermissionSetNameTitle()) {
                        if (permSetName == this._generalPermissionSetName) {
                            if (this.get_showGeneralPermissionSetTitles()) {
                                showLabel = true;
                            }
                        }
                        else showLabel = true;
                    }
                    else
                        showLabel = false;
                    this._setLabelText(lblPermissionSetName, permissionSetsArray[curPermissionSet].PermissionSetName);
                    this._toggleControlVisibility(lblPermissionSetName, showLabel);

                    for (var curAction = 0; curAction < permissionSetsArray[curPermissionSet].Permissions.length; curAction++) {
                        this._addPermissionRow($(tablesArea).find("tbody").get(curPermissionSet), permissionSetsArray[curPermissionSet].Permissions[curAction].ActionTitle, permissionSetsArray[curPermissionSet].Permissions[curAction].IsAllowed, permissionSetsArray[curPermissionSet].Permissions[curAction].IsDenied);
                    }
                    this._autoHideDeniedColumnIfEmpty($(tablesArea).find("tbody").get(curPermissionSet));
                }
            }
        }
    },

    _getPermissionModulesFailure: function (error) {
        alert(error.Detail);
    },

    _reBindCombo: function (comboID, binderID) {
        var Combo = $find(comboID);
        var Binder = $find(binderID);
        var providerName = (((Combo.get_value() == "") || (Combo.get_value() == null)) ? "" : Combo.get_text());
        var params = Binder.get_urlParams();
        if (params == null)
            params = new Object();
        params["forAllProviders"] = (providerName == "");
        params["skip"] = 0;
        params["take"] = 0;
        Binder.set_urlParams(params);
        Binder.set_provider(providerName);
        Binder.DataBind();
    },

    _toggleControlVisibility: function (ctlElement, bIsVisible) {
        if (ctlElement != null)
            ctlElement.style.visibility = ((bIsVisible) ? "visible" : "hidden");
    },

    _toggleControlDisplay: function (ctlElement, bIsDisplayed) {
        if (ctlElement != null)
            this._setTableCellDisplay(ctlElement, bIsDisplayed);
    },


    _openUserPermissionsEditor: function (e) {
        var module = null;
        var provider = null;
        var permissionSetName = "";
        var permissionSetOrdinal = 0;
        for (var curPermSet = 0; curPermSet < this._listedButtonPermissionSets.length; curPermSet++) {
            if (this._listedButtonPermissionSets[curPermSet].BtnClientId == e.target.id) {
                permissionSetName = this._listedButtonPermissionSets[curPermSet].PermissionSetName;
                permissionSetOrdinal = this._listedButtonPermissionSets[curPermSet].PermissionSetModuleOrdinal;
                break;
            }
        }

        for (var listedModule = 0; listedModule < this.listedModulesArray.length; listedModule++) {
            if (typeof $($get(this._mainPermissionsTablePanelID).getElementsByTagName("div")[0]).find("#" + this.listedModulesArray[listedModule].ModuleClientId).find("#" + e.target.id).get(0) != "undefined") {
                this._tableBodyToUpdate = $($get(this._mainPermissionsTablePanelID).getElementsByTagName("div")[0]).find("#" + this.listedModulesArray[listedModule].ModuleClientId).find("tbody").get(permissionSetOrdinal);
                module = this.listedModulesArray[listedModule];
                for (var providerIndex = 0; providerIndex < module.ModuleProvidersData.length; providerIndex++) {
                    var selectBoxValue = $get(module.SelectboxClientId).value;
                    if (module.ModuleProvidersData[providerIndex].ProviderId == selectBoxValue) {
                        provider = module.ModuleProvidersData[providerIndex];
                        break;
                    }
                }
                break;
            }
        }
        if ((module != null) && (provider != null)) {
            var oWnd = $find(this._permissionsSelectionDialogID);
            var url = oWnd.GetUrl();
            if (url.indexOf("?") > 0)
                url = url.substring(0, url.indexOf("?"))

            oWnd.SetUrl(url +
                "?principalID=" + this._principalID +
                "&managerClassName=" + provider.ManagerType +
                "&dataProviderName=" + provider.ProviderName +
                "&dynamicDataProviderName=" + provider.DynamicDataProviderName +
                "&permissionsSetName=" + permissionSetName +
                "&showPrincipalName=" + this._showPrincipalName +
                "&securedObjectID=" + provider.SecuredObjectId +
                "&securedObjectType=" + provider.SecuredObjectType);

            oWnd.argument = this;
            oWnd.add_close(this._permissionsSelection_ClientClose);
            oWnd.setSize(425, 250);
            oWnd.show();
            Telerik.Sitefinity.centerWindowHorizontally(oWnd);
        }
        return false;
    },


    _permissionsSelection_ClientClose: function (sender, eventArgs) {
        var newPermissions = eventArgs.get_argument();
        if (newPermissions != null) {
            var usersData = Sys.Serialization.JavaScriptSerializer.deserialize(newPermissions);
            var tbody = sender.argument._tableBodyToUpdate;
            sender.argument._removeChildren(tbody);
            for (var curPer = 0; curPer < usersData.length; curPer++) {
                sender.argument._addPermissionRow(tbody, usersData[curPer].ActionTitle, usersData[curPer].IsAllowed, usersData[curPer].IsDenied);
            }
            sender.argument._autoHideDeniedColumnIfEmpty(tbody);
        }
    },

    _removeChildren: function (element) {
        for (var child = element.childNodes.length - 1; child >= 0; child--)
            element.removeChild(element.childNodes[child]);
    },

    _autoHideDeniedColumnIfEmpty: function (tableBodyElement) {
        var hideColumn = true;
        jQuery.each($(tableBodyElement).find("[id^='spanActionDenied']"), function () {
            if (this.style.visibility == "visible")
                hideColumn = false;
        });
        jQuery.each($(tableBodyElement).find("[id^='tdActionDenied']"), function () {
            if (hideColumn)
                $(this).addClass("sfHiddenCell");
            else
                $(this).removeClass("sfHiddenCell");
        });
        this._setTableCellDisplay($(tableBodyElement.parentNode).find("[id^='lblDenyAction']").get(0), !hideColumn);
    },


    _setTableCellDisplay: function (cellElement, bShow) {
        if (!bShow)
            $(cellElement).addClass("sfHiddenCell");
        else
            $(cellElement).removeClass("sfHiddenCell");
    },

    _openSiteUsageWindow: function (e) {
        var providerSelectBox = $(e.target.parentElement).find("[id*=slcProviderSelector]").get(0);
        var module = null;
        var provider = null;

        for (var curModule = 0; curModule < this.listedModulesArray.length; curModule++) {
            if (this.listedModulesArray[curModule].SelectboxClientId == providerSelectBox.id) {
                module = this.listedModulesArray[curModule];
                break;
            }
        }

        if (module != null) {
            for (var curProvider = 0; curProvider < module.ModuleProvidersData.length; curProvider++) {
                if (providerSelectBox.value == module.ModuleProvidersData[curProvider].ProviderId) {
                    provider = module.ModuleProvidersData[curProvider];
                    break;
                }
            }

            if (provider != null) {
                var oWnd = $find(this._sitesUsageRadWindowID);
                var url = oWnd.GetUrl();
                if (url.indexOf("?") > 0)
                    url = url.substring(0, url.indexOf("?"))               

                var dataProviderName = provider.DynamicDataProviderName ? provider.DynamicDataProviderName : provider.ProviderName;
                windowUrl = url +
                    "?dataProviderName=" + dataProviderName +
                    "&dataProviderTitle=" + provider.ProviderTitle +
                    "&managerClassName=" + provider.ManagerType +                    
                    "&securedObjectType=" + provider.SecuredObjectType +
                    "&dynamicModuleTitle=" + this._getModuleTitle(provider.SecuredObjectId);

                oWnd.SetUrl(windowUrl);
                oWnd.argument = this;

                oWnd.show();
                Telerik.Sitefinity.centerWindowHorizontally(oWnd);
            }
        }
    },

    _closeSitesUsageWindow: function () {
        var oWnd = $find(this._sitesUsageRadWindowID);
        oWnd.Hide();
        oWnd.Close();
    },

    _getModule: function (securedObjectId) {
        var module = null;
        for (var curModule = 0; curModule < this.listedModulesArray.length; curModule++) {
            for (var curProvider = 0; curProvider < this.listedModulesArray[curModule].ModuleProvidersData.length; curProvider++) {
                if (this.listedModulesArray[curModule].ModuleProvidersData[curProvider].SecuredObjectId == securedObjectId) {
                    module = this.listedModulesArray[curModule];
                    return module;
                }
            }
        }
    },

    _getModuleTitle: function (securedObjectId) {
        var module = this._getModule(securedObjectId);
        var moduleTitle = '';
        if (module != null) {
            moduleTitle = module.ModuleTitle;
        }

        return moduleTitle;
    },

    _getModuleDisplayTitle: function (moduleTitle) {
        var displayModuleTitle = moduleTitle;
        if (displayModuleTitle == "Widget templates") {
            // change the name of the section because it includes permissions for Widget templates, pages and page templates
            displayModuleTitle = "Pages and templates";
        }

        return displayModuleTitle;
    },

    // ------------------------------------- Public accessors -------------------------------------

    get_dataProviderName: function () {
        return this._dataProviderName;
    },

    set_dataProviderName: function (value) {
        if (this._dataProviderName != value) {
            this._dataProviderName = value;
            this.raisePropertyChanged('dataProviderName');
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

    get_permissionsSetName: function () {
        return this._permissionsSetName;
    },

    set_permissionsSetName: function (value) {
        if (this._permissionsSetName != value) {
            this._permissionsSetName = value;
            this.raisePropertyChanged('permissionsSetName');
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

    get_principalName: function () {
        return this._principalName;
    },

    set_principalName: function (value) {
        if (this._principalName != value) {
            this._principalName = value;
            this._setLabelText($get(this._userOrRoleNameLabelID), value);
            this.raisePropertyChanged('principalName');
        }
    },

    get_userPermissionsEditorPerRoleWindowTitle: function () {
        return this._userPermissionsEditorPerRoleWindowTitle;
    },

    set_userPermissionsEditorPerRoleWindowTitle: function (value) {
        if (this._userPermissionsEditorPerRoleWindowTitle != value) {
            this._userPermissionsEditorPerRoleWindowTitle = value;
            this.raisePropertyChanged('userPermissionsEditorPerRoleWindowTitle');
        }
    },

    get_showPrincipalName: function () {
        return this._showPrincipalName;
    },

    set_showPrincipalName: function (value) {
        if (this._showPrincipalName != value) {
            this._showPrincipalName = (String(value).toUpperCase() == "TRUE");
            $get(this._userOrRoleNameLabelID).style.display = ((this._showPrincipalName) ? "block" : "none");
            this.raisePropertyChanged('showPrincipalName');
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
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },

    set_bindOnLoad: function (value) {
        if (this._bindOnLoad != value) {
            this._bindOnLoad = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('bindOnLoad');
        }
    },

    get_showPermissionSetNameTitle: function () {
        return this._showPermissionSetNameTitle;
    },

    set_showPermissionSetNameTitle: function (value) {
        if (this._showPermissionSetNameTitle != value) {
            this._showPermissionSetNameTitle = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('showPermissionSetNameTitle');
        }
    },

    get_showGeneralPermissionSetTitles: function () {
        return this._showGeneralPermissionSetTitles;
    },

    set_showGeneralPermissionSetTitles: function (value) {
        if (this._showGeneralPermissionSetTitles != value) {
            this._showGeneralPermissionSetTitles = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('showGeneralPermissionSetTitles');
        }
    }
};
Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList', Sys.UI.Control);