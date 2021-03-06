//ItemActionPermissionsList
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions");

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList = function (element) {

    //private properties (retrieved from server, inaccessible)
    this._providersComboID = null;
    this._providerSelectionPanelID = null;
    this._permissionsUrl = null;
    this._permissionSetsBinderID = null;
    this._inheritsPermissionsPanelID = null;
    this._inheritanceBrokenPanelID = null;
    this._overrideInheritedPermissionsButtonID = null;
    this._inheritPermissionsButtonID = null;
    this._loadingProgressPanelID = null;
    this._loadingProgressPanelInheritanceID = null;
    this._mainPermissionsPanelID = null;
    this._mainPermissionInheritancePanelID = null;

    this._wcfPrincipalType = null;
    this._userCssClassName = "sfUserPerm";
    this._roleCssClassName = "sfRolePerm";
    this._userInheritedCssClassName = "sfInheritedUserPerm";
    this._roleInheritedCssClassName = "sfInheritedRolePerm";
    this._administratorsOnlyLabelText = null;
    this._usersSelectionRadWindowID = null;

    this._multiSiteMode = null;
    this._sitesUsageRadWindowID = null;
    this._sitesUsageLabelID = null;
    this._sitesUsageLinkID = null;
    this._sitesUsageSingleLabelText = null;
    this._sitesUsageMultipleLabelText = null;

    this._allowedUsersLabelText = null;
    this._deniedUsersLabelText = null;
    this._changeButtonLabelText = null;
    this._permissionTitleLabelID = null;
    this._actionDescriptionToolTip = null;
    this._confirmInheritPermissionsMessage = null;
    this._isGranularPermissionsEnabled = null;

    //internal properties
    this._allowedUsersListToUpdateClientID = "";
    this._deniedUsersListToUpdateClientID = "";
    this._allowedUsersToUpdateLabelID = "";
    this._deniedUsersToUpdateLabelID = "";
    this._clientInitialized = false;
    this._listedPermissionSets = null;
    this._listedControlSets = null;
    this._providersData = null;
    this._selectedProvider = null;
    this._selectedProviderName = null;
    this._permissionsAreInherited = false;
    this._permissionsCanBeInherited = false;
    this._providersArr = null;

    //publicly accessible attributes
    this._moduleName = null;
    this._permissionSetName = null;
    this._managerClassName = null;
    this._dataProviderName = null;
    this._securedObjectID = null;
    this._showPermissionSetName = null;
    this._securedObjectTypeName = null;
    this._title = null;
    this._bindOnLoad = null;
    this._requireSystemProviders = null;

    //dynamic modules
    this._applyDynamicModulePermissions = null;
    this._moduleBuilderClassName = null;
    this._moduleBuilderDefaultProvider = null;

    // event delegates
    this._clientInitializedDelegate = null;

    Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList.callBaseMethod(this, "initialize");
        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        this._dispatchDialogOpenedEvent();

        this._wcfPrincipalType = Sys.Serialization.JavaScriptSerializer.deserialize(this._wcfPrincipalType);

        this._listedPermissionSets = new Array();
        this._listedControlSets = new Array();

        this._allowedUsersListToUpdateClientID = "";
        this._deniedUsersListToUpdateClientID = "";
        this._allowedUsersToUpdateLabelID = "";
        this._deniedUsersToUpdateLabelID = "";


        var inheritsPermissionsPanel = $get(this._inheritsPermissionsPanelID);
        var inheritanceBrokenPanel = $get(this._inheritanceBrokenPanelID);
        var overrideInheritedPermissionsButton = $get(this._overrideInheritedPermissionsButtonID);
        var inheritPermissionsButton = $get(this._inheritPermissionsButtonID);
        var inheritsControlPanelLoading = $get(this._loadingProgressPanelInheritanceID);
        var mainPermissionInheritancePanel = $get(this._mainPermissionInheritancePanelID);

        this._toggleControlDisplay(inheritsPermissionsPanel, false);
        this._toggleControlDisplay(inheritanceBrokenPanel, false);
        this._toggleControlDisplay(inheritsControlPanelLoading, false);
        this._toggleControlDisplay(mainPermissionInheritancePanel, false);

        this._overridePermissionsDelegate = Function.createDelegate(this, this._overridePermissions_Click);
        this._inheritPermissionsDelegate = Function.createDelegate(this, this._inheritPermissions_Click);

        $addHandler(overrideInheritedPermissionsButton, "click", this._overridePermissionsDelegate);
        $addHandler(inheritPermissionsButton, "click", this._inheritPermissionsDelegate);

        //register events
        if (this._clientInitializedDelegate === null) {
            this._clientInitializedDelegate = Function.createDelegate(this, this._clientInitializedHandler);
        }
    },

    dispose: function () {
        var overrideInheritedPermissionsButton = $get(this._overrideInheritedPermissionsButtonID);
        var inheritPermissionsButton = $get(this._inheritPermissionsButtonID);
        $removeHandler(overrideInheritedPermissionsButton, "click", this._overridePermissionsDelegate);
        $removeHandler(inheritPermissionsButton, "click", this._inheritPermissionsDelegate);

        // Clean up events
        if (this._clientInitializedDelegate) {
            delete this._clientInitializedDelegate;
        }

        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList.callBaseMethod(this, "dispose");
    },

    onload: function () {
        var providersComboBox = $find(this._providersComboID);
        providersComboBox.add_selectedIndexChanged(Function.createDelegate(this, this._providerSelectionChanged));

        var PermissionSetsBinder = $find(this._permissionSetsBinderID);
        PermissionSetsBinder.add_onItemDataBound(Function.createDelegate(this, this._permissionSet_OnItemDataBound));
        PermissionSetsBinder.add_onDataBound(Function.createDelegate(this, this._permissionSet_OnDataBound));
        PermissionSetsBinder.add_onDataBinding(Function.createDelegate(this, this._permissionSet_OnDataBinding));

        //if we need to bind on load, let the loading complete, then the "onInitialized" will be invoked by the callback
        if (this.get_bindOnLoad())
            this.dataBind();
        else
            this._clientInitializedHandler();
    },

    // ------------------------------------- client-side classes -------------------------------------
    //class
    controlSet: function (allowedLabelId, deniedLabelId, allowedPrincipalListId, deniedPrincipalListId, lnkChangeId, actionName, permissionSetName) {
        this.AllowedLabelId = allowedLabelId;
        this.DeniedLabelId = deniedLabelId;
        this.AllowedPrincipalListId = allowedPrincipalListId;
        this.DeniedPrincipalListId = deniedPrincipalListId;
        this.LnkChangeId = lnkChangeId;
        this.ActionName = actionName;
        this.PermissionSetName = permissionSetName;
    },

    // ------------------------------------- Internal utility functions -------------------------------------

    _showLoadingPanel: function (bShow) {
        this._toggleControlDisplayById(this._loadingProgressPanelID, bShow);
        this._toggleControlDisplayById(this._mainPermissionsPanelID, !bShow);
    },

    _showLoadingPanelInheritance: function (bShow) {
        this._toggleControlDisplayById(this._loadingProgressPanelInheritanceID, bShow);
    },

    _overridePermissions_Click: function (sender, data) {
        /// <summary>Handler for clicking the "override permissions" button: break the inheritance</summary>
        this._changeInheritanceStatus(true, true);
    },


    _inheritPermissions_Click: function (sender, data) {
        /// <summary>Handler for clicking the "inherit permissions" button: unbreak the inheritance</summary>
        if (confirm(this._confirmInheritPermissionsMessage))
            //TODO: instead of standard confirmation, display a dialog with radio buttons to let the user choose if they want to lose their custom permissions or not,
            //and set the 2nd argument here
            this._changeInheritanceStatus(false, true);
    },

    _changeInheritanceStatus: function (bBreak, loseCustomChanges) {
        var providersComboBox = $find(this._providersComboID);

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var keys = new Array();
        var urlParams = null;

        keys[keys.length] = "ChangeInheritance";
        // the provider and manager of the dynamic module are passed as an extra parameters - dynamicDataProviderName and dynamicManagerClassName
        if (this._applyDynamicModulePermissions) {
            urlParams = {
                'dataProviderName': this._moduleBuilderDefaultProvider,
                'managerClassName': this._moduleBuilderClassName,
                'securedObjectID': this._securedObjectID,
                'securedObjectType': this._securedObjectTypeName,
                'break': String(bBreak),
                'loseCustomChanges': String(loseCustomChanges),
                'dynamicDataProviderName': providersComboBox.get_value()
            };
        }
        else {
            urlParams = {
                'dataProviderName': providersComboBox.get_value(),
                'managerClassName': this._managerClassName,
                'securedObjectID': this._securedObjectID,
                'securedObjectType': this._securedObjectTypeName,
                'break': String(bBreak),
                'loseCustomChanges': String(loseCustomChanges)
            };
        }

        if (urlParams != null) {
            this._toggleControlDisplayById(this._inheritanceBrokenPanelID, false);
            this._toggleControlDisplayById(this._inheritsPermissionsPanelID, false);
            this._showLoadingPanelInheritance(true);
            clientManager.InvokePut(
                                this._permissionsUrl,
                                urlParams,  //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                                keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                                [],
                                Function.createDelegate(this, this._changeInheritanceSuccess),
                                Function.createDelegate(this, this._changeInheritanceFailure),
                                this);
        }
    },

    _changeInheritanceSuccess: function (sender, args) {
        this._showLoadingPanelInheritance(false);
        var PermissionSetsBinder = $find(this._permissionSetsBinderID);
        PermissionSetsBinder.ClearTarget();
        PermissionSetsBinder.DataBind(args);
    },

    _changeInheritanceFailure: function (sender, args) {
        alert(sender.Detail);
    },

    _setLabelText: function (LabelElement, newText) {
        if (LabelElement != null) {
            if (typeof LabelElement.textContent != "undefined")
                LabelElement.textContent = newText;

            if (typeof LabelElement.innerText != "undefined")
                LabelElement.innerText = newText;
        }
    },

    _dispatchDialogOpenedEvent: function () {
        var currentDocument = document;

        if (window.frameElement) {
            currentDocument = window.parent.document;
        }

        if (typeof CustomEvent == "function") {
            var evt = new CustomEvent('sfModalDialogOpened');
            currentDocument.dispatchEvent(evt);
        }
    },

    _toggleControlDisplay: function (ctlElement, bIsDisplayed) {
        this._toggleControlDisplayWithStyle(ctlElement, bIsDisplayed, "block");
    },

    _toggleControlDisplayWithStyle: function (ctlElement, bIsDisplayed, style) {
        if (ctlElement != null)
            ctlElement.style.display = ((bIsDisplayed) ? style : "none");
    },

    _toggleControlVisibility: function (ctlElement, bIsDisplayed) {
        if (ctlElement != null)
            ctlElement.style.visibility = ((bIsDisplayed) ? "visible" : "hidden");
    },

    _toggleControlDisplayById: function (ctlElementID, bIsDisplayed) {
        var ctlElement = $get(ctlElementID);
        this._toggleControlDisplayWithStyle(ctlElement, bIsDisplayed, "block");
    },

    _toggleControlDisplayInlineById: function (ctlElementID, bIsDisplayed) {
        var ctlElement = $get(ctlElementID);
        this._toggleControlDisplayWithStyle(ctlElement, bIsDisplayed, "inline-block");
    },

    _clearUserOrRolePleaceList: function (listElement) {
        $(listElement).children().not(".sfInfoListItem").remove();
    },

    _addUserOrRoleToList: function (itemsList, UserOrRoleNameToAdd, isAllowed, relatedLabel, principalType, isInherited) {
        var newItem = document.createElement("LI");

        var userClass = (isInherited) ? this._userInheritedCssClassName : this._userCssClassName;
        var roleClass = (isInherited) ? this._roleInheritedCssClassName : this._roleCssClassName;

        newItem.className = ((principalType == this._wcfPrincipalType.User) ? userClass : roleClass);
        newItem.appendChild(document.createTextNode(UserOrRoleNameToAdd));
        itemsList.appendChild(newItem);
        this._toggleControlDisplay(relatedLabel, true);
    },

    // Information list item for some item action
    _addInformationListItem: function (itemsList, actionName) {
        var message;
        switch (actionName) {
            case "ViewBackendLink":
                message = 'As set in <i>Create</i>, <i>Modify</i> or <i>Delete</i> ';
                break;
            default:
                return;
        }

        var listItem = document.createElement("LI");
        listItem.className = this._roleCssClassName + " sfInfoListItem";
        listItem.innerHTML = message;
        itemsList.appendChild(listItem);
    },

    _bindProvidersList: function () {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var keys = new Array();
        var urlParams = null;

        // option 1: by manager type, get only system providers
        if (this._requireSystemProviders != null && this._requireSystemProviders == true)
        {
            keys[keys.length] = "GetManagerSystemProviders";

            urlParams = {
                'managerClassName': this._managerClassName
            };
        }
            //option 2: by module name
        else if ((this._moduleName != null) && (this._moduleName != "")) {
            keys[keys.length] = "GetModuleProviders";

            urlParams = {
                'moduleName': this._moduleName
            };
        }
        else {
            //option 3: by manager type:
            if ((this._managerClassName != null) && (this._managerClassName != "")) {
                keys[keys.length] = "GetManagerProviders";

                urlParams = {
                    'managerClassName': this._managerClassName
                };
            }
        }
        if (urlParams != null) {
            clientManager.InvokeGet(
                        this._permissionsUrl,
                        urlParams,  //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                        keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                        Function.createDelegate(this, this._getProvidersSuccess),
                        Function.createDelegate(this, this._getProvidersFail),
                        this);
        }
    },

    _getProvidersSuccess: function (sender, commandArgs) {
        this._providersData = commandArgs.Items;
        this._fillComboProviders(commandArgs.Items);
    },

    _getProvidersFail: function () {
        alert("failed");
    },

    _fillComboProviders: function (providersArray) {
        this._providersArr = providersArray;
        var providersComboBox = $find(this._providersComboID);
        this._managerClassName = "";
        if (providersComboBox != null) {
            providersComboBox.trackChanges();
            providersComboBox.clearItems();
            var items = providersComboBox.get_items();
            var selectedProviderIndex = -1;
            for (var curProvider = 0; curProvider < providersArray.length; curProvider++) {
                //if no provider is set, or set to the current provider -> add it
                if ((this._dataProviderName == null) || (this._dataProviderName == "") || (providersArray[curProvider].ProviderName == this._dataProviderName)) {
                    var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                    comboItem.set_text(providersArray[curProvider].ProviderTitle);
                    comboItem.set_value(providersArray[curProvider].ProviderName);
                    items.add(comboItem);
                }
            }

            var itemsCount = items.get_count();

            //locate the item to select, out of the providers which were actually added
            for (var curProvider = 0; curProvider < itemsCount; curProvider++) {
                if ((this._dataProviderName == null) || (this._dataProviderName == "") || (items.getItem(curProvider).get_value() == this._dataProviderName)) {
                    selectedProviderIndex = curProvider;
                    break;
                }
            }

            if (itemsCount > 0) {
                var selectedProviderName = this.get_selectedProviderName();
                var selectedComboItem = null;

                // The last condition is to ensuse that selectedProviderName is respected only on initialize of the modal dialog
                if (selectedProviderName && !this._selectedProvider) {
                    selectedComboItem = items._array.filter(function (el) { return el._properties._data.value === selectedProviderName; })[0];
                    if (selectedComboItem) {
                        selectedProviderIndex = items.indexOf(selectedComboItem);
                    }
                } else {
                    if (selectedProviderIndex == -1) {
                        selectedProviderIndex = 0;
                    }

                    selectedComboItem = items.getItem(selectedProviderIndex);
                }

                this._managerClassName = providersArray[selectedProviderIndex].ManagerTypeName;
                this._selectedProvider = providersArray[selectedProviderIndex];
                selectedComboItem.select();

                this._toggleControlDisplayInlineById(this._providerSelectionPanelID, (itemsCount > 1));
                providersComboBox.commitChanges();
            }
            else {
                var PermissionSetsBinder = $find(this._permissionSetsBinderID);
                PermissionSetsBinder.ClearTarget();
                this._toggleControlDisplayInlineById(this._providerSelectionPanelID, false);
            }
        }
    },

    _permissionSet_OnDataBound: function (sender, args) {
        if (this._clientInitialized == false) {
            this._clientInitializedHandler();
        }
        this._showLoadingPanel(false);
    },

    _permissionSet_OnDataBinding: function (sender, args) {
        var data = args._dataItem;
        var inheritsPermissionsPanel = $get(this._inheritsPermissionsPanelID);
        var mainPermissionInheritancePanel = $get(this._mainPermissionInheritancePanelID);
        var inheritanceBrokenPanel = $get(this._inheritanceBrokenPanelID);
        var inheritButton = $("*[id$='btnInheritPermissionsWrapper']").get(0);
        var breakInheritanceButton = $("*[id$='btnOverrideInheritedPermissionsWrapper']").get(0);

        var serverPermissionSetsCount = 0;
        {
            var iter = data.Items.length;
            var sets = {};
            while (iter--) {
                var cur = data.Items[iter].PermissionSetName;
                if (!sets.hasOwnProperty(cur)) {
                    sets[cur] = true;
                    serverPermissionSetsCount++;
                }
            }
        }

        if (data.EditablePermissionSets) {
            var hidePermButton = !this._isGranularPermissionsEnabled || data.EditablePermissionSets.length < serverPermissionSetsCount;
            if (hidePermButton) {
                this._toggleControlDisplay(inheritButton, !hidePermButton);
                this._toggleControlDisplay(breakInheritanceButton, !hidePermButton);
            }
        }

        if (data.CanInheritPermissions) {
            this._toggleControlDisplay(mainPermissionInheritancePanel, true);
            this._toggleControlDisplay(inheritsPermissionsPanel, data.InheritsPermissions);
            this._toggleControlDisplay(inheritanceBrokenPanel, !data.InheritsPermissions);
        }
        else {
            this._toggleControlDisplay(mainPermissionInheritancePanel, false);
        }

        this._permissionsAreInherited = data.InheritsPermissions;
        this._permissionsCanBeInherited = data.CanInheritPermissions;
        this._editablePermissionSets = data.EditablePermissionSets;
        this._actualSecuredObjectId = data.ActualSecuredObjectId;
    },

    _permissionSet_OnItemDataBound: function (sender, args) {
        var allowListIsEmpty = true;
        var dataItem = args.get_dataItem();
        var permissionSetNameTitleControl = $(args.get_itemElement()).find("*[id^='permissionSetNameTitle']").get(0);
        var ulAllowed = $(args.get_itemElement()).find("*[id^='ulAllowed']").get(0);
        var ulDenied = $(args.get_itemElement()).find("*[id^='ulDenied']").get(0);
        var lblDeniedUsers = $(args.get_itemElement()).find("*[id^='lblDeniedUsers']").get(0);
        var btnChange = $(args.get_itemElement()).find("*[id^='btnChange']").get(0);    //the whole button, with surroundings
        var lnkChange = $(args.get_itemElement()).find("*[id^='lnkChange']").get(0);    //the text link inside the button
        var actionDescriptionText = $(args.get_itemElement()).find("*[id^='hiddenActionDescription']").get(0);
        var actionDecriptionLink = $(args.get_itemElement()).find("*[id^='actionDecriptionLink']").get(0);

        var tooltipTemplate = $find(this._actionDescriptionToolTip);
        var curTooltip = tooltipTemplate.clone();
        curTooltip.set_text(actionDescriptionText.value);
        curTooltip.set_targetControl(actionDecriptionLink);

        var permSetVisible = true;
        if (this._permissionsAreInherited)
            permSetVisible = false;
        else
            permSetVisible = Array.contains(this._editablePermissionSets, dataItem.PermissionSetName);
        this._toggleControlVisibility(btnChange, permSetVisible);

        this._setLabelText(lblDeniedUsers, this._deniedUsersLabelText);
        this._setLabelText(lnkChange, this._changeButtonLabelText);

        this._toggleControlDisplay(lblDeniedUsers, false);

        var exists = false;
        for (var curset = 0; curset < this._listedPermissionSets.length; curset++) {
            if (this._listedPermissionSets[curset] == dataItem.PermissionSetName) {
                exists = true;
                break;
            }
        }
        if (!exists)
            this._listedPermissionSets[this._listedPermissionSets.length] = dataItem.PermissionSetName;

        if (this._showPermissionSetName)
            this._toggleControlDisplay(permissionSetNameTitleControl, !exists);
        else
            this._toggleControlDisplay(permissionSetNameTitleControl, false);

        this._clearUserOrRolePleaceList(ulAllowed);
        this._clearUserOrRolePleaceList(ulDenied);
        this._addInformationListItem(ulAllowed, dataItem.ActionName);
        var securedObjectId = this._securedObjectID;
        if (this._actualSecuredObjectId &&
            this._actualSecuredObjectId.toUpperCase().trim() != sender.GetEmptyGuid() &&
            (this._actualSecuredObjectId.toUpperCase().trim() != "")) {
            securedObjectId = this._actualSecuredObjectId;
        }

        for (var curPermission = 0; curPermission < dataItem.Permissions.length; curPermission++) {
            var isInherited = (
                (securedObjectId.toUpperCase().trim() != dataItem.Permissions[curPermission].SecuredObjectId.toUpperCase().trim()) &&
                    (
                        (securedObjectId.toUpperCase().trim() != sender.GetEmptyGuid()) &&
                        (securedObjectId.toUpperCase().trim() != null) &&
                        (securedObjectId.toUpperCase().trim() != "")
                    )
                );
            this._addUserOrRoleToList(
                ((dataItem.Permissions[curPermission].IsAllowed) ? ulAllowed : ulDenied),
                dataItem.Permissions[curPermission].PrincipalTitle,
                dataItem.Permissions[curPermission].IsAllowed,
                ((dataItem.Permissions[curPermission].IsAllowed) ? null /*lblAllowedUsers*/ : lblDeniedUsers),
                dataItem.Permissions[curPermission].PrincipalType,
                isInherited);
            if (dataItem.Permissions[curPermission].IsAllowed)
                allowListIsEmpty = false;
        }
        if (allowListIsEmpty) {
            this._addUserOrRoleToList(ulAllowed, this._administratorsOnlyLabelText, true, null/*lblAllowedUsers*/, this._wcfPrincipalType.Role, this._permissionsAreInherited);
        }
        this._listedControlSets[this._listedControlSets.length] = new this.controlSet(
            null/*lblAllowedUsers.id*/,
            lblDeniedUsers.id,
            ulAllowed.id,
            ulDenied.id,
            lnkChange.id,
            dataItem.ActionName,
            dataItem.PermissionSetName);

        $addHandler(lnkChange, "click", Function.createDelegate(this, this._openActions));
    },

    _providerSelectionChanged: function (sender, args) {
        var selectedValue = args.get_item().get_value();
        this._selectedProvider = this._providersArr[args.get_item().get_index()];
        var securedObjectType = this._securedObjectTypeName;
        var securedObjId = this._selectedProvider.SecuredObjectRootId;
        if ((typeof (securedObjectType) == "undefined") || (securedObjectType == null) || (securedObjectType == ""))
            securedObjectType = this._selectedProvider.SecuredObjectType;

        if ((this._securedObjectID != null) && (this._securedObjectID != ""))
            securedObjId = this._securedObjectID;
        if (selectedValue != null) {
            var PermissionSetsBinder = $find(this._permissionSetsBinderID);

            var selectedText = args.get_item().get_value();
            var urlParams = this._getUrlParams(selectedText, securedObjId, securedObjectType);

            this._listedPermissionSets = new Array();
            this._listedControlSets = new Array();
            PermissionSetsBinder.set_urlParams(urlParams);
            PermissionSetsBinder.DataBind();

            if (this._multiSiteMode && this._providersArr.length > 0) {
                var clientManager = new Telerik.Sitefinity.Data.ClientManager();
                var keys = new Array();
                keys[keys.length] = "GetProviderUsage";
                urlParams = this._getSitesUsageUrlParams(securedObjectType);

                if (urlParams != null) {
                    clientManager.InvokeGet(
                                this._permissionsUrl,
                                urlParams,  //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                                keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                                Function.createDelegate(this, this._getProviderUsageSuccess),
                                Function.createDelegate(this, this._getProviderUsageFail),
                                this);
                }
            }
        }
    },

    _getProviderUsageSuccess: function (sender, commandArgs) {
        var sitesCount = commandArgs.Items.length;
        var sitesUsageLabel = $get(this._sitesUsageLabelID);
        var sitesUsageLink = $get(this._sitesUsageLinkID);
        if (sitesCount > 1) {
            sitesUsageLabel.style.display = '';
            sitesUsageLink.style.display = '';
            sitesUsageLink.innerText = sitesCount-1 + " " + (sitesCount == 2 ? this._sitesUsageSingleLabelText : this._sitesUsageMultipleLabelText);
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

    _getUrlParams: function (selectedText, securedObjId, securedObjectType) {

        var urlParams;
        // the provider and manager of the dynamic module are passed as an extra parameters - dynamicDataProviderName and dynamicManagerClassName
        if (this._applyDynamicModulePermissions) {
            urlParams = {
                'permissionsSetName': this._permissionSetName,
                'securedObjectID': this._securedObjectID,
                'securedObjectType': securedObjectType,
                'dataProviderName': this._moduleBuilderDefaultProvider,
                'managerClassName': this._moduleBuilderClassName,
                'dynamicDataProviderName': selectedText
            };
        }
        else {
            urlParams = {
                'permissionsSetName': this._permissionSetName,
                'dataProviderName': selectedText,
                'managerClassName': this._selectedProvider.ManagerTypeName,
                'securedObjectID': securedObjId,
                'securedObjectType': securedObjectType
            };
        }

        return urlParams;
    },

    _getSitesUsageUrlParams: function (securedObjectType) {

        var urlParams;
        // in case of dynamic module add module title parameter
        if (this._applyDynamicModulePermissions) {
            urlParams = {
                'dataProviderName': this._selectedProvider.ProviderName,
                'managerClassName': this._selectedProvider.ManagerTypeName,
                'securedObjectType': securedObjectType,
                'dynamicModuleTitle': this.get_title()
            };
        }
        else {
            urlParams = {
                'dataProviderName': this._selectedProvider.ProviderName,
                'managerClassName': this._selectedProvider.ManagerTypeName
            };
        }

        return urlParams;
    },

    // ------------------------------------- Users selection window -------------------------------------
    _openActions: function (e) {
        var oWnd = $find(this._usersSelectionRadWindowID);
        var url = oWnd.GetUrl();
        if (url.indexOf("?") > 0)
            url = url.substring(0, url.indexOf("?"))

        var actionName = "";
        var allowedUsersListID = "";
        var deniedUsersListID = "";
        var allowedUsersLabelID = "";
        var deniedUsersLabelID = "";
        var permissionSetName = "";

        for (var curCtrl = 0; curCtrl < this._listedControlSets.length; curCtrl++) {
            if (e.target.id == this._listedControlSets[curCtrl].LnkChangeId) {
                actionName = this._listedControlSets[curCtrl].ActionName;
                permissionSetName = this._listedControlSets[curCtrl].PermissionSetName;
                allowedUsersListID = this._listedControlSets[curCtrl].AllowedPrincipalListId;
                deniedUsersListID = this._listedControlSets[curCtrl].DeniedPrincipalListId;
                allowedUsersLabelID = this._listedControlSets[curCtrl].AllowedLabelId;
                deniedUsersLabelID = this._listedControlSets[curCtrl].DeniedLabelId;
                break;
            }
        }

        this._allowedUsersListToUpdateClientID = allowedUsersListID;
        this._deniedUsersListToUpdateClientID = deniedUsersListID;
        this._allowedUsersToUpdateLabelID = allowedUsersLabelID;
        this._deniedUsersToUpdateLabelID = deniedUsersLabelID;

        var providersComboBox = $find(this._providersComboID);
        var securedObjId = this._selectedProvider.SecuredObjectRootId;
        var securedObjType = this._selectedProvider.SecuredObjectType;
        if ((this._securedObjectID != null) && (this._securedObjectID != ""))
            securedObjId = this._securedObjectID;
        if ((this._securedObjectTypeName != null) && (this._securedObjectTypeName != ""))
            securedObjType = this._securedObjectTypeName;

        // the provider and manager of the dynamic module are passed as an extra parameters - dynamicDataProviderName and dynamicManagerClassName
        var windowUrl;
        if (this._applyDynamicModulePermissions) {
            windowUrl = url +
                "?actionName=" + actionName +
                "&permissionSetName=" + permissionSetName +
                "&dataProviderName=" + this._moduleBuilderDefaultProvider +
                "&managerClassName=" + this._moduleBuilderClassName +
                "&dynamicDataProviderName=" + providersComboBox.get_value() +
                "&permissionObjectRootID=" + this._selectedProvider.SecuredObjectRootId +
                "&securedObjectID=" + securedObjId +
                "&actualSecuredObjectId=" + this._actualSecuredObjectId +
                "&securedObjectType=" + securedObjType +
                "&inheritsPermissions=" + this._permissionsAreInherited;
        }
        else {
            windowUrl = url +
                "?actionName=" + actionName +
                "&permissionSetName=" + permissionSetName +
                "&dataProviderName=" + providersComboBox.get_value() +
                "&permissionObjectRootID=" + this._selectedProvider.SecuredObjectRootId +
                "&managerClassName=" + this._selectedProvider.ManagerTypeName +
                "&securedObjectID=" + securedObjId +
                "&securedObjectType=" + securedObjType +
                "&inheritsPermissions=" + this._permissionsAreInherited;
        }

        oWnd.SetUrl(windowUrl);
        oWnd.argument = this;
        oWnd.add_close(this._UsersSelection_ClientClose);
        oWnd.setSize(424, 250);
        oWnd.show();
        Telerik.Sitefinity.centerWindowHorizontally(oWnd);
    },

    _UsersSelection_ClientClose: function (sender, eventArgs) {
        var newUsers = eventArgs.get_argument();
        sender.argument._isAllowedListEmpty = true;
        if (newUsers != null) {

            var ulAllowed = $("#" + sender.argument.get_id()).find("#" + sender.argument._allowedUsersListToUpdateClientID)[0]; //$get(sender.argument._allowedUsersListToUpdateClientID);
            var ulDenied = $("#" + sender.argument.get_id()).find("#" + sender.argument._deniedUsersListToUpdateClientID)[0]; //$get(sender.argument._deniedUsersListToUpdateClientID);
            var lblDeniedUsers = $("#" + sender.argument.get_id()).find("#" + sender.argument._deniedUsersToUpdateLabelID)[0]; // $get(sender.argument._deniedUsersToUpdateLabelID);

            sender.argument._toggleControlDisplay(lblDeniedUsers, false);
            sender.argument._clearUserOrRolePleaceList(ulAllowed);
            sender.argument._clearUserOrRolePleaceList(ulDenied);
            var allowListIsEmpty = true;

            var usersData = Sys.Serialization.JavaScriptSerializer.deserialize(newUsers);
            for (var arrayCell = 0; arrayCell < usersData.length; arrayCell++) {
                sender.argument._addUserOrRoleToList(
                ((usersData[arrayCell].IsAllowed) ? ulAllowed : ulDenied),
                usersData[arrayCell].PrincipalName,
                usersData[arrayCell].IsAllowed,
                ((usersData[arrayCell].IsAllowed) ? null/*lblAllowedUsers*/ : lblDeniedUsers),
                usersData[arrayCell].PrincipalType,
                usersData[arrayCell].IsInherited);
                if (usersData[arrayCell].IsAllowed)
                    allowListIsEmpty = false;
            }
            if (allowListIsEmpty) {
                sender.argument._addUserOrRoleToList(ulAllowed, sender.argument._administratorsOnlyLabelText, true, null/*lblAllowedUsers*/, sender.argument._wcfPrincipalType.User, false);
            }
        }
    },

    _closeSelectionWindow: function () {
        var oWnd = $find(this._usersSelectionRadWindowID);
        oWnd.Hide();
        oWnd.Close();
    },

    _openSiteUsageWindow: function (e) {
        var oWnd = $find(this._sitesUsageRadWindowID);
        var url = oWnd.GetUrl();
        if (url.indexOf("?") > 0)
            url = url.substring(0, url.indexOf("?"))

        var securedObjType = this._selectedProvider.SecuredObjectType;
        if ((this._securedObjectID != null) && (this._securedObjectID != ""))
            securedObjId = this._securedObjectID;
        if ((this._securedObjectTypeName != null) && (this._securedObjectTypeName != ""))
            securedObjType = this._securedObjectTypeName;

        if (this._applyDynamicModulePermissions) {
            windowUrl = url +
           "?dataProviderName=" + this._selectedProvider.ProviderName +
           "&dataProviderTitle=" + this._selectedProvider.ProviderTitle +
           "&managerClassName=" + this._selectedProvider.ManagerTypeName +
           "&securedObjectType=" + securedObjType +
           "&dynamicModuleTitle=" + this.get_title();
        }
        else {
            windowUrl = url +
            "?dataProviderName=" + this._selectedProvider.ProviderName +
            "&managerClassName=" + this._selectedProvider.ManagerTypeName;
        }

        oWnd.SetUrl(windowUrl);
        oWnd.argument = this;

        oWnd.show();
        Telerik.Sitefinity.centerWindowHorizontally(oWnd);
    },

    _closeSitesUsageWindow: function () {
        var oWnd = $find(this._sitesUsageRadWindowID);
        oWnd.Hide();
        oWnd.Close();
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

    // ------------------------------------- Public methods -------------------------------------
    dataBind: function () {
        this._showLoadingPanel(true);
        this._closeSelectionWindow();
        this._bindProvidersList();
    },

    bindToModule: function (moduleProviderAssociationObject) {
        this._showLoadingPanel(true);
        this._closeSelectionWindow();
        //optional: a specific manager class name specified
        if ((typeof (moduleProviderAssociationObject.ModuleManagerTypeName) != "undefined") &&
            (moduleProviderAssociationObject.ModuleManagerTypeName != null) &&
            (moduleProviderAssociationObject.ModuleManagerTypeName != "")) {
            this.set_managerClassName(moduleProviderAssociationObject.ModuleManagerTypeName);
        }
            //optional: a module name is specified
        else {
            this.set_moduleName(moduleProviderAssociationObject.ModuleName);
        }
        //optional: a specific secured object id + type is specified
        if ((typeof (moduleProviderAssociationObject.SecuredObjectId) != "undefined") &&
            (moduleProviderAssociationObject.SecuredObjectId != null) &&
            (moduleProviderAssociationObject.SecuredObjectId != "") &&
            (typeof (moduleProviderAssociationObject.SecuredObjectTypeName) != "undefined") &&
            (moduleProviderAssociationObject.SecuredObjectTypeName != null) &&
            (moduleProviderAssociationObject.SecuredObjectTypeName != "")) {
            this.set_securedObjectID(moduleProviderAssociationObject.SecuredObjectId);
            this.set_securedObjectTypeName(moduleProviderAssociationObject.SecuredObjectTypeName);
        }
        else {
            this.set_securedObjectID("");
            this.set_securedObjectTypeName("");
        }
        this.set_title(moduleProviderAssociationObject.ModuleTitle);
        this._fillComboProviders(moduleProviderAssociationObject.ModuleProviders);
    },

    // ------------------------------------- Public accessors -------------------------------------

    get_moduleName: function () {
        return this._moduleName;
    },

    set_moduleName: function (value) {
        if (this._moduleName != value) {
            this._moduleName = value;
            this.raisePropertyChanged('moduleName');
        }
    },

    get_selectedProviderName: function () {
        return this._selectedProviderName;
    },

    set_selectedProviderName: function (value) {
        if (this._selectedProviderName != value) {
            this._selectedProviderName = value;
        }
    },

    get_securedObjectTypeName: function () {
        return this._securedObjectTypeName;
    },

    set_securedObjectTypeName: function (value) {
        if (this._securedObjectTypeName != value) {
            this._securedObjectTypeName = value;
            this.raisePropertyChanged('securedObjectTypeName');
        }
    },

    get_permissionSetName: function () {
        return this._permissionSetName;
    },

    set_permissionSetName: function (value) {
        if (this._permissionSetName != value) {
            this._permissionSetName = value;
            this.raisePropertyChanged('permissionSetName');
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

    get_dataProviderName: function () {
        return this._dataProviderName;
    },

    set_dataProviderName: function (value) {
        if (this._dataProviderName != value) {
            this._dataProviderName = value;
            this.raisePropertyChanged('dataProviderName');
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

    get_showPermissionSetName: function () {
        return this._showPermissionSetName;
    },

    set_showPermissionSetName: function (value) {
        if (this._showPermissionSetName != value) {
            this._showPermissionSetName = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('showPermissionSetName');
        }
    },

    get_title: function () {
        return this._title;
    },

    set_title: function (value) {
        if (this._title != value) {
            this._title = value;
            this._setLabelText($get(this._permissionTitleLabelID), value);
            this.raisePropertyChanged('title');
        }
    },

    get_applyDynamicModulePermissions: function () {
        return this._applyDynamicModulePermissions;
    },

    set_applyDynamicModulePermissions: function (value) {
        this._applyDynamicModulePermissions = value;
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },

    set_bindOnLoad: function (value) {
        if (this._bindOnLoad != value) {
            this._bindOnLoad = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('bindOnLoad');
        }
    }
};

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList', Sys.UI.Control);