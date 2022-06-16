Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions");

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection.initializeBase(this, [element]);

    //private properties (retrieved from server, inaccessible)
    this._onLoadPermissions = null;
    this._actionName = null;
    this._permissionSetName = null;
    this._dataProviderName = null;
    this._managerClassName = null;
    this._permissionsUrl = null;
    this._securedObjectType = null;
    this._dynamicDataProviderName = null;
    this._administratorRoleSelected = null;
    this._everyoneRoleSelected = null;
    this._backendRoleSelected = null;
    this._specificPrincipalsSelected = null;
    this._everyoneRoleName = null;
    this._backendRoleName = null;
    this._inheritsPermissions = null;

    //private enumerations (retrieved from server, inaccessible)
    this._usersSelectionMode = null;
    this._wcfPrincipalType = null;

    //private texts (retrieved from server, inaccessible)
    this._principalRemoveText = null;
    this._specificUsersRadioActiveHtml = null;
    this._specificUsersRadioInactiveHtml = null;
    this._SpecificDeniedUsersActiveHtml = null;
    this._SpecificDeniedUsersInactiveHtml = null;

    //private child-controls (retrieved from server, inaccessible)
    this._ctrlID = null;
    this._permissionObjectRootID = null;
    this._allowedSelectedUsersRadioButtonID = null;
    this._allowedUsersOrRolesListPanelID = null;
    this._saveLinkButtonID = null;
    this._closeLinkButtonID = null;
    this._addRolesOrUsersDeniedLinkButtonID = null;
    this._securedObjectID = null;
    this._actualSecuredObjectId = null;
    this._allowedEveryoneRadioButtonID = null;
    this._allowedBackendUsersRadioButtonID = null;
    this._allowedAdminsOnlyRadioButtonID = null;
    this._principalSelectionPanelID = null;
    this._everyoneRoleID = null;
    this._backendRoleID = null;
    this._explicitlyDeniedCheckboxID = null;
    this._deniedUsersOrRolesListPanelID = null;
    this._openUsersSelectionBoxLinkButtonID = null;
    this._doneSelectingLinkID = null;
    this._cancelSelectingLinkID = null;
    this._principalSelector = null;

    //internal properties
    this._selectedAllowedKeysArray = new Array();
    this._selectedDeniedKeysArray = new Array();
    this._deselectedPrincipals = new Array();
    this._adminsOnlyConstText = "AdminsOnly";
    this._isDirty = false;
    this._arrayActionEntities = new Array();
    this._activeUserSelectionMode = null;
    this._selectedAppRole = null;
    this._allowedUsersCounter = 0;
    this._deniedUsersCounter = 0;
    this._saveData = null;
}

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection.callBaseMethod(this, "initialize");
        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        //deserialize
        this._usersSelectionMode = Sys.Serialization.JavaScriptSerializer.deserialize(this._usersSelectionMode);
        this._wcfPrincipalType = Sys.Serialization.JavaScriptSerializer.deserialize(this._wcfPrincipalType);
        this._onLoadPermissions = Sys.Serialization.JavaScriptSerializer.deserialize(this._onLoadPermissions);
        this._specificPrincipalsSelected = Sys.Serialization.JavaScriptSerializer.deserialize(this._specificPrincipalsSelected);
        this._everyoneRoleSelected = Sys.Serialization.JavaScriptSerializer.deserialize(this._everyoneRoleSelected);
        this._administratorRoleSelected = Sys.Serialization.JavaScriptSerializer.deserialize(this._administratorRoleSelected);
        this._backendRoleSelected = Sys.Serialization.JavaScriptSerializer.deserialize(this._backendRoleSelected);
        this._inheritsPermissions = Sys.Serialization.JavaScriptSerializer.deserialize(this._inheritsPermissions);

        //initialize
        this._activeUserSelectionMode = this._usersSelectionMode.AllowedUsers;

        //ctrl references
        var OpenUsersSelectionBoxLinkButton = $get(this._openUsersSelectionBoxLinkButtonID);
        var addRolesOrUsersDeniedLinkButton = $get(this._addRolesOrUsersDeniedLinkButtonID);
        var explicitlyDeniedCheckbox = $get(this._explicitlyDeniedCheckboxID);
        var allowedSelectedUsersRadioButton = $get(this._allowedSelectedUsersRadioButtonID);
        var allowedEveryoneRadioButton = $get(this._allowedEveryoneRadioButtonID);
        var allowedBackendUsersRadioButton = $get(this._allowedBackendUsersRadioButtonID);
        var allowedAdminsOnlyRadioButton = $get(this._allowedAdminsOnlyRadioButtonID);
        var doneSelectingLink = $get(this._doneSelectingLinkID);
        var cancelSelectingLink = $get(this._cancelSelectingLinkID);
        var SaveLinkButton = $get(this._saveLinkButtonID);
        var CloseLinkButton = $get(this._closeLinkButtonID);

        //initial ui status
        allowedSelectedUsersRadioButton.checked = this._specificPrincipalsSelected;
        allowedEveryoneRadioButton.checked = this._everyoneRoleSelected;
        allowedBackendUsersRadioButton.checked = this._backendRoleSelected;
        allowedAdminsOnlyRadioButton.checked = this._administratorRoleSelected;
        this._radioButtonSelectionChanged();

        //delegates
        this._explicitlyDeniedCheckboxClickDelegate = Function.createDelegate(this, this._setDeniedArea);
        this._radioButtonClickDelegate = Function.createDelegate(this, this._radioButtonSelectionChanged);
        this._principalsDeselectedDelegate = Function.createDelegate(this, this._principalsDeselected);
        this._doneSelectingPrincipalskDelegate = Function.createDelegate(this, this._doneSelectingPrincipals);
        this._cancelSelectingDelegate = Function.createDelegate(this, this._cancelSelectingPrincipals);
        this._selectDeniedUsersLinkButtonClickDelegate = Function.createDelegate(this, this._openDeniedUsersSelectionBoxLink_Click);
        this._openUsersSelectionBoxLinkButtonClickDelegate = Function.createDelegate(this, this._openUsersSelectionBoxLink_Click);
        this._clickSaveDelegate = Function.createDelegate(this, this._saveAndClose);
        this._clickCancelDelegate = Function.createDelegate(this, this._closeWindow);

        //ui events
        $addHandler(allowedSelectedUsersRadioButton, "click", this._radioButtonClickDelegate);
        $addHandler(allowedEveryoneRadioButton, "click", this._radioButtonClickDelegate);
        $addHandler(allowedBackendUsersRadioButton, "click", this._radioButtonClickDelegate);
        $addHandler(allowedAdminsOnlyRadioButton, "click", this._radioButtonClickDelegate);
        $addHandler(explicitlyDeniedCheckbox, "click", this._explicitlyDeniedCheckboxClickDelegate);
        $addHandler(doneSelectingLink, "click", this._doneSelectingPrincipalskDelegate);
        $addHandler(cancelSelectingLink, "click", this._cancelSelectingDelegate);
        $addHandler(addRolesOrUsersDeniedLinkButton, "click", this._selectDeniedUsersLinkButtonClickDelegate);
        $addHandler(OpenUsersSelectionBoxLinkButton, "click", this._openUsersSelectionBoxLinkButtonClickDelegate);
        $addHandler(SaveLinkButton, "click", this._clickSaveDelegate);
        $addHandler(CloseLinkButton, "click", this._clickCancelDelegate);
        this._attachItemDeselectedHandler();
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection.callBaseMethod(this, "dispose");

        //ctrl references
        var addRolesOrUsersDeniedLinkButton = $get(this._addRolesOrUsersDeniedLinkButtonID);
        var OpenUsersSelectionBoxLinkButton = $get(this._openUsersSelectionBoxLinkButtonID);
        var SaveLinkButton = $get(this._saveLinkButtonID);
        var CloseLinkButton = $get(this._closeLinkButtonID);
        var doneSelectingLink = $get(this._doneSelectingLinkID);
        var cancelSelectingLink = $get(this._cancelSelectingLinkID);
        var explicitlyDeniedCheckbox = $get(this._explicitlyDeniedCheckboxID);
        var allowedSelectedUsersRadioButton = $get(this._allowedSelectedUsersRadioButtonID);
        var allowedEveryoneRadioButton = $get(this._allowedEveryoneRadioButtonID);
        var allowedAdminsOnlyRadioButton = $get(this._allowedAdminsOnlyRadioButtonID);
        var allowedBackendUsersRadioButton = $get(this._allowedBackendUsersRadioButtonID);

        //ui events
        $removeHandler(allowedSelectedUsersRadioButton, "click", this._radioButtonClickDelegate);
        $removeHandler(allowedBackendUsersRadioButton, "click", this._radioButtonClickDelegate);
        $removeHandler(allowedEveryoneRadioButton, "click", this._radioButtonClickDelegate);
        $removeHandler(allowedAdminsOnlyRadioButton, "click", this._radioButtonClickDelegate);
        $removeHandler(explicitlyDeniedCheckbox, "click", this._explicitlyDeniedCheckboxClickDelegate);
        $removeHandler(addRolesOrUsersDeniedLinkButton, "click", this._selectDeniedUsersLinkButtonClickDelegate);
        $removeHandler(OpenUsersSelectionBoxLinkButton, "click", this._openUsersSelectionBoxLinkButtonClickDelegate);
        $removeHandler(doneSelectingLink, "click", this._doneSelectingPrincipalskDelegate);
        $removeHandler(cancelSelectingLink, "click", this._cancelSelectingDelegate);
        $removeHandler(SaveLinkButton, "click", this._clickSaveDelegate);
        $removeHandler(CloseLinkButton, "click", this._clickCancelDelegate);
    },

    onload: function () {
        //ctrl references
        var addRolesOrUsersDeniedLinkButton = $get(this._addRolesOrUsersDeniedLinkButtonID);
        var allowedSelectedUsersRadioButton = $get(this._allowedSelectedUsersRadioButtonID);
        var OpenUsersSelectionBoxLinkButton = $get(this._openUsersSelectionBoxLinkButtonID);
        var explicitlyDeniedCheckbox = $get(this._explicitlyDeniedCheckboxID);
        var allowedBackendUsersRadioButton = $get(this._allowedBackendUsersRadioButtonID);
        var principalSelector = $find(this._principalSelector);
        this._rolesSelectorInitializedDelegate = Function.createDelegate(this, this._rolesSelector_Initialized);

        if ((this._inheritsPermissions) && (allowedBackendUsersRadioButton.checked)) {
            var roleSelector = this.get_principalSelector().getSelectorItemByName("rolesSelector");
            roleSelector.add_selectorInitialized(this._rolesSelectorInitializedDelegate);
        }

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var securedObjectId = this._securedObjectID;
        if (this._actualSecuredObjectId &&
            this._actualSecuredObjectId.toUpperCase().trim() != clientManager.GetEmptyGuid() &&
            (this._actualSecuredObjectId.toUpperCase().trim() != "")) {
            securedObjectId = this._actualSecuredObjectId;
        }

        //load initial permissions
        for (var i = 0; i < this._onLoadPermissions.length; i++) {
            if ((this._specificPrincipalsSelected) || (this._onLoadPermissions[i].IsDenied)) {
                var inheritedPermissionThereforeLocked = (this._onLoadPermissions[i].SecuredObjectId.toUpperCase().trim() != securedObjectId.toUpperCase().trim());

                this._addEntityToCollection(
	            this._onLoadPermissions[i].PrincipalType,
	            this._onLoadPermissions[i].PrincipalTitle,
	            this._onLoadPermissions[i].PrincipalID,
	            ((this._onLoadPermissions[i].IsAllowed) ? this._allowedUsersOrRolesListPanelID : this._deniedUsersOrRolesListPanelID),
	            this._onLoadPermissions[i].IsAllowed,
	            this._onLoadPermissions[i].IsDenied,
	            inheritedPermissionThereforeLocked);
            }
        }
        explicitlyDeniedCheckbox.checked = (this._deniedUsersCounter > 0);
        this._setDeniedArea();
        this._displayUserSelectionLink(allowedSelectedUsersRadioButton.checked);
        this._isDirty = false;

        jQuery("body").addClass("sfSelectorDialog sfOverflowHiddenX");
        this._autosizeMe();
    },

    // ------------------------------------- client-side classes -------------------------------------
    //class
    wcfPermission: function (entityType, entityName, entityID, isAllowed, isDenied, actionName, isInherited) {
        this.PrincipalType = entityType;
        this.PrincipalID = entityID;
        this.PrincipalName = entityName;
        this.IsAllowed = isAllowed;
        this.IsDenied = isDenied;
        this.ActionName = actionName;
        this.IsInherited = isInherited;
    },

    //class
    listedBoundPrincipal: function (principalListItemElement, principalId) {
        this.PrincipalListItemElement = principalListItemElement;
        this.PrincipalId = principalId;
    },

    // ------------------------------------- Internal utility functions -------------------------------------
    _attachItemDeselectedHandler: function () {
        var selectors = this.get_principalSelector()._selectors;
        for (var selectorName in selectors) {
            if (selectors.hasOwnProperty(selectorName)) {
                var selector = selectors[selectorName];
                selector.add_itemDeselected(this._principalsDeselectedDelegate);
            }
        }
    },

    _rolesSelector_Initialized: function () {
        /// <summary>Invoked when the role selector is initialized</summary>
        var allowedBackendUsersRadioButton = $get(this._allowedBackendUsersRadioButtonID);
        //If the "backend users" radio is selected and locked (i.e. inherited), it's impossible to deny "backend users",
        //thus it should not be displayed in the role selector.
        if ((this._inheritsPermissions) && (allowedBackendUsersRadioButton.checked)) {
            var roleSelector = this.get_principalSelector().findSelectorByName("rolesSelector");
            var rolesFilter = roleSelector.get_constantFilter();
            rolesFilter = ((rolesFilter == "") ? "(" : "((" + rolesFilter + ") and ");
            rolesFilter += "(Id != (" + this._backendRoleID + ")))";
            roleSelector.set_constantFilter(rolesFilter);
            roleSelector.bindSelector();
        }
    },

    _setRadioButtonTexts: function () {
        /// <summary>Sets the text on the radio buttons according to the current selection.</summary>
        var allowedSelectedUsersRadioButton = $get(this._allowedSelectedUsersRadioButtonID);
        this._setServerControlLabelHTML(this._allowedSelectedUsersRadioButtonID,
            (allowedSelectedUsersRadioButton.checked) ? this._specificUsersRadioActiveHtml : this._specificUsersRadioInactiveHtml);
    },

    _setDeniedArea: function () {
        /// <summary>Sets the text "denied" checkbox, and visibility of the denied area according to checkbox status.</summary>
        var explicitlyDeniedCheckbox = $get(this._explicitlyDeniedCheckboxID);
        var deniedUsersOrRolesListPanel = $get(this._deniedUsersOrRolesListPanelID);
        this._setServerControlLabelHTML(this._explicitlyDeniedCheckboxID,
            (explicitlyDeniedCheckbox.checked) ? this._SpecificDeniedUsersActiveHtml : this._SpecificDeniedUsersInactiveHtml);
        this._toggleControlDisplay(deniedUsersOrRolesListPanel, (explicitlyDeniedCheckbox.checked));
        this._isDirty = true;

        if (!explicitlyDeniedCheckbox.checked) {
            var deniedUsersOrRolesListItems = deniedUsersOrRolesListPanel.getElementsByTagName("span");
            for (var listScanner = 0; listScanner < deniedUsersOrRolesListItems.length; listScanner++) {
                var principalDetails = this._getObjectFromPrincipalListItemID(deniedUsersOrRolesListItems[listScanner].id);
                if (principalDetails != null) {
                    this._removeEntityFromCollection(
                    deniedUsersOrRolesListItems[listScanner],
                    principalDetails.EntityType,
                    principalDetails.EntityID,
                    principalDetails.IsAllowed,
                    principalDetails.IsDenied);
                }
            }
        }

        this._autosizeMe();
    },

    _setServerControlLabelHTML: function (elementId, HTMLContent) {
        /// <summary>Sets html of a label control, related to a server side control (e.g. checkbox, radio button).</summary>
        /// <param name="elementId">Cliend side Id of the server control related to the label</param>
        /// <param name="HTMLContent">HTML content to place in the label</param>
        var labelElements = $("*").find("label[for='" + elementId + "']");
        if (labelElements != null) {
            this._setLabelHTML(labelElements.get(0), HTMLContent);
        }
    },

    _setLabelHTML: function (labelElement, HTMLContent) {
        /// <summary>Sets html content of a label control.</summary>
        /// <param name="labelElement">Cliend side Id label</param>
        /// <param name="HTMLContent">HTML content to place in the label</param>
        labelElement.innerHTML = HTMLContent;
    },

    _setUsersSelectionBoxDisplay: function (bShow) {
        /// <summary>Shows or hides the user selecttion panel.</summary>
        /// <param name="bShow">Boolean- whether to show or hide the panel</param>
        var principalSelectionPanel = $get(this._principalSelectionPanelID);
        this._toggleControlDisplay(principalSelectionPanel, bShow);
        this._autosizeMe();
    },

    _toggleControlDisplay: function (ctlElement, bIsDisplayed) {
        /// <summary>Shows or hides an element.</summary>
        /// <param name="ctlElement">The element to show or hide</param>
        /// <param name="bIsDisplayed">Boolean- whether to show or hide the control</param>
        if (ctlElement != null)
            ctlElement.style.display = ((bIsDisplayed) ? "block" : "none");
    },

    _toggleLinkButtonDisplay: function (ctlElement, bIsDisplayed) {
        /// <summary>Sets the css class to show or hide a link button.</summary>
        /// <param name="ctlElement">The linkbutton to show or hide</param>
        /// <param name="bIsDisplayed">Boolean- whether to show or hide the control</param>    
        if (ctlElement != null)
            ctlElement.className = ((bIsDisplayed) ? "sfLinkBtn sfChange" : "sfDisplayNone");
    },

    _addEntityToCollection: function (entityType, entityName, entityID, entityUiList, isAllowed, isDenied, isLocked) {
        /// <summary>Adds a principal entity to the allowed/denied collection, and creates the corresponding control on the page.</summary>
        /// <param name="entityType">The type of the principal. Should be of the this._wcfPrincipalType enumeration</param>
        /// <param name="entityName">The name of the principal to display (user/role name)</param>
        /// <param name="entityID">The GUID ID of the principal.</param>
        /// <param name="entityUiList">A corresponding control whic contains the list to which the item is added</param>
        /// <param name="isAllowed">Boolean indicating if this principal is allowed to perform the current action</param>
        /// <param name="isDenied">Boolean indicating if this principal is denied to perform the current action</param>
        var entity = new this.wcfPermission(entityType, entityName, entityID, isAllowed, isDenied, this._actionName, isLocked);
        var alreadyExists = false;

        for (arrayCell = 0; arrayCell < this._arrayActionEntities.length; arrayCell++) {
            if ((this._arrayActionEntities[arrayCell].PrincipalID == entityID) &&
                (this._arrayActionEntities[arrayCell].IsAllowed == isAllowed) &&
                (this._arrayActionEntities[arrayCell].IsDenied == isDenied))
                alreadyExists = true;
        }

        var itemsList = $get(entityUiList).getElementsByTagName("ul")[0];
        if (!alreadyExists) {
            if (this._arrayActionEntities != null) {
                this._arrayActionEntities[this._arrayActionEntities.length] = entity;
            }
            if (isAllowed)
                this._selectedAllowedKeysArray[this._selectedAllowedKeysArray.length] = entityID;

            if (isDenied)
                this._selectedDeniedKeysArray[this._selectedDeniedKeysArray.length] = entityID;

            this._allowedUsersCounter += ((isAllowed) ? 1 : 0);
            this._deniedUsersCounter += ((isDenied) ? 1 : 0);
            var newItem = document.createElement("LI");

            if (isLocked) {
                $(newItem).addClass("sfNotToBeRemoved");
            }
            else {
                var removeSpan = document.createElement("SPAN");
                removeSpan.appendChild(document.createTextNode(this._principalRemoveText));

                removeSpan.setAttribute("id", this._ctrlID + "___" + entityID + "_" + entityType + "_" + isAllowed + "_" + isDenied);

                this._newItemClickDelegate = Function.createDelegate(this, this._selectedPrincipal_Click);
                $addHandler(removeSpan, "click", this._newItemClickDelegate);
            }
            newItem.appendChild(document.createTextNode(entityName));
            itemsList.appendChild(newItem);

            if (!isLocked) {
                newItem.appendChild(removeSpan);
            }
            this._isDirty = true;

            //if the principal is "backend users" to be added to the denied list, and the "backend users" radio is checked (and not locked)
            //move the radio selection to "administrators"
            var allowedBackendUsersRadioButton = $get(this._allowedBackendUsersRadioButtonID);
            var allowedAdminsOnlyRadioButton = $get(this._allowedAdminsOnlyRadioButtonID);
            if ((entityID == this._backendRoleID) &&
                (entityType == this._wcfPrincipalType.Role) &&
                (allowedBackendUsersRadioButton.checked) &&
                (!this._inheritsPermissions) &&
                (isDenied)) {
                allowedAdminsOnlyRadioButton.checked = true;
                this._radioButtonSelectionChanged();
            }

            //see if the item exists on the other already and remove it from there
            var theOtherList = $get(((isAllowed) ? this._deniedUsersOrRolesListPanelID : this._allowedUsersOrRolesListPanelID));
            var theOtherListItems = theOtherList.getElementsByTagName("span");
            for (var listScanner = 0; listScanner < theOtherListItems.length; listScanner++) {
                var principalDetails = this._getObjectFromPrincipalListItemID(theOtherListItems[listScanner].id);
                if (principalDetails != null) {
                    if ((principalDetails.EntityID == entityID) &&
	                (principalDetails.IsAllowed == !isAllowed) &&
	                (principalDetails.IsDenied == !isDenied)) {
                        this._removeEntityFromCollection(
	                    theOtherListItems[listScanner],
	                    principalDetails.EntityType,
	                    principalDetails.EntityID,
	                    principalDetails.IsAllowed,
	                    principalDetails.IsDenied);
                    }
                }
            }
            this._autosizeMe();
        }
    },

    _removeEntityFromCollection: function (liRemovalItem, entityType, entityID, isAllowed, isDenied) {
        /// <summary>Removes a principal entity from the allowed/denied collection, and removes the corresponding control on the page.</summary>
        /// <param name="liRemovalItem">The UI element representing the principal to remove</param>
        /// <param name="entityType">The type of the principal. Should be of the this._wcfPrincipalType enumeration</param>
        /// <param name="entityID">The GUID ID of the principal.</param>
        /// <param name="isAllowed">Boolean indicating if this principal is allowed to perform the current action</param>
        /// <param name="isDenied">Boolean indicating if this principal is denied to perform the current action</param>
        liRemovalItem.parentNode.parentNode.removeChild(liRemovalItem.parentNode);
        this._allowedUsersCounter -= ((isAllowed) ? 1 : 0);
        this._deniedUsersCounter -= ((isDenied) ? 1 : 0);
        this._isDirty = true;
        for (var arrayCell = 0; arrayCell < this._arrayActionEntities.length; arrayCell++) {
            if ((this._arrayActionEntities[arrayCell].PrincipalType == entityType) &&
	            (this._arrayActionEntities[arrayCell].PrincipalID == entityID) &&
	            (this._arrayActionEntities[arrayCell].IsAllowed == isAllowed) &&
	            (this._arrayActionEntities[arrayCell].IsDenied == isDenied)) {
                this._arrayActionEntities.splice(arrayCell, 1);
                break;
            }
        }
        if (isAllowed) {
            for (var arrayCell = 0; arrayCell < this._selectedAllowedKeysArray.length; arrayCell++) {
                if (this._selectedAllowedKeysArray[arrayCell] == entityID) {
                    this._selectedAllowedKeysArray.splice(arrayCell, 1);
                }
            }
        }
        if (isDenied) {
            for (var arrayCell = 0; arrayCell < this._selectedDeniedKeysArray.length; arrayCell++) {
                if (this._selectedDeniedKeysArray[arrayCell] == entityID) {
                    this._selectedDeniedKeysArray.splice(arrayCell, 1);
                }
            }
        }
        this._autosizeMe();
    },


    _displayUserSelectionLink: function (bShow) {
        /// <summary>Shows or hides the users selection link for allowed users.</summary>
        /// <param name="bShow">Boolean- whether to show or hide the control</param>    
        var UserSelectionLink = $get(this._openUsersSelectionBoxLinkButtonID);
        this._toggleLinkButtonDisplay(UserSelectionLink, bShow);
    },

    _displaySelectedAllowedUsers: function (bShow) {
        /// <summary>Shows or hides the allowed selected users panel.</summary>
        /// <param name="bShow">Boolean- whether to show or hide the control</param> 
        var AllowedUsersPanel = $get(this._allowedUsersOrRolesListPanelID);
        this._toggleControlDisplay(AllowedUsersPanel, bShow);
    },

    // ------------------------------------- Dialog functions -------------------------------------

    _autosizeMe: function () {
        /// <summary>Resizes the dialog to fit the content.</summary>
        //autosize function doesn't work properly so we add 26 pixels to have enough height for each browser
        var sWin = this._getRadWindow();
        sWin.autoSize();
        var add = 26;
        if (jQuery.browser.msie)
            add = 40;
        else if (jQuery.browser.safari)
            add = 40;
        sWin.set_height(sWin.get_height() + 26);
    },

    _setWindowWidth: function (newWidth) {
        /// <summary>Sets width to the dialog to.</summary>
        var sWin = this._getRadWindow();
        sWin.set_width(newWidth);
    },

    _closeWindow: function () {
        /// <summary>Closes the dialog, returning a null value.</summary>
        this._closeMe(null);
    },

    _closeMe: function (closeArgument) {
        /// <summary>Closes the dialog, returning a value.</summary>
        /// <param name="closeArgument">The data to return at closing.</param>
        var dialog = this._getRadWindow();
        dialog.close(closeArgument);
    },

    _getRadWindow: function () {
        /// <summary>Gets reference to the current RadWindow.</summary>
        /// <returns>A reference to the current window.</returns>
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        return oWindow;
    },

    // ------------------------------------- Event handlers -------------------------------------

    _radioButtonSelectionChanged: function () {
        /// <summary>Handles a change in the radio buttons selection: sets the display of relevant controls, and the current selection ID if applicable</summary>
        var entityID = "";
        var entityName = "";

        var allowedSelectedUsersRadioButton = $get(this._allowedSelectedUsersRadioButtonID);
        var allowedEveryoneRadioButton = $get(this._allowedEveryoneRadioButtonID);
        var allowedBackendUsersRadioButton = $get(this._allowedBackendUsersRadioButtonID);
        var allowedAdminsOnlyRadioButton = $get(this._allowedAdminsOnlyRadioButtonID);

        //specific users
        if (allowedSelectedUsersRadioButton.checked) {
            entityName = "";
            entityID = "";
        }
            //everyone
        else if (allowedEveryoneRadioButton.checked) {
            entityName = this._everyoneRoleName;
            entityID = this._everyoneRoleID;
        }
            //backend users
        else if (allowedBackendUsersRadioButton.checked) {
            entityName = this._backendRoleName;
            entityID = this._backendRoleID;
            //if "backend users" role is listed in the denied list, remove it.
            var deniedList = $get(this._deniedUsersOrRolesListPanelID).getElementsByTagName("ul")[0];
            var deniedPrincipals = deniedList.getElementsByTagName("span");
            for (var i = 0; i < deniedPrincipals.length; i++) {
                var details = this._getObjectFromPrincipalListItemID(deniedPrincipals[i].id);
                if (details.EntityID == this._backendRoleID) {
                    this._deniedUsersCounter--;
                    deniedPrincipals[i].parentNode.parentNode.removeChild(deniedPrincipals[i].parentNode);
                    for (var arrayCell = 0; arrayCell < this._selectedDeniedKeysArray.length; arrayCell++) {
                        if (this._selectedDeniedKeysArray[arrayCell] == this._backendRoleID) {
                            this._selectedDeniedKeysArray.splice(arrayCell, 1);
                        }
                    }
                    for (var arrayCell = 0; arrayCell < this._arrayActionEntities.length; arrayCell++) {
                        if ((this._arrayActionEntities[arrayCell].PrincipalType == this._wcfPrincipalType.Role) &&
	                        (this._arrayActionEntities[arrayCell].PrincipalID == this._backendRoleID) &&
	                        (this._arrayActionEntities[arrayCell].IsAllowed == false) &&
	                        (this._arrayActionEntities[arrayCell].IsDenied == true)) {
                            this._arrayActionEntities.splice(arrayCell, 1);
                            break;
                        }
                    }
                }
            }

        }
            //administrators
        else if (allowedAdminsOnlyRadioButton.checked) {
            entityName = this._adminsOnlyConstText;
            entityID = "";
        }

        this._setRadioButtonTexts();
        this._userSelectionChanged(entityID, entityName);
        this._autosizeMe();
    },

    _openUsersSelectionBoxLink_Click: function () {
        /// <summary>Handles clicking the user allowed selection linkbutton, opening the selection panel</summary>
        this.get_principalSelector().resetSelectorsPageIndexes();
        this._activeUserSelectionMode = this._usersSelectionMode.AllowedUsers;
        this.get_principalSelector().setSelectedKeys(this._selectedAllowedKeysArray);
        this._setUsersSelectionBoxDisplay(true);
    },

    _getIdFromPrincipalObject: function (principal) {
        var principalId;
        var principalType;
        var isAllowed = (this._activeUserSelectionMode == this._usersSelectionMode.AllowedUsers);

        // User
        if (typeof (principal.UserID) != "undefined") {
            principalId = principal.UserID;
            principalType = this._wcfPrincipalType.User;
        }
            // Role
        else if (typeof (principal.Id) != "undefined") {
            principalId = principal.Id;
            principalType = this._wcfPrincipalType.Role;
        }

        return this._ctrlID + "___" + principalId + "_" + principalType + "_" + isAllowed + "_" + !isAllowed;
    },

    _principalsDeselected: function (sender, args) {
        var item = this._getIdFromPrincipalObject(args);
        this._deselectedPrincipals.push(item);
    },

    _openDeniedUsersSelectionBoxLink_Click: function () {
        /// <summary>Handles clicking the user denied selection linkbutton, opening the selection panel</summary>
        this.get_principalSelector().resetSelectorsPageIndexes();
        this._activeUserSelectionMode = this._usersSelectionMode.DeniedUsers;
        this.get_principalSelector().setSelectedKeys(this._selectedDeniedKeysArray);
        this._setUsersSelectionBoxDisplay(true);
    },

    _doneSelectingPrincipals: function () {
        /// <summary>Handles clicking the "done" in the user selection panel- adding the selected users to their collections</summary>

        var principalId = "";
        var principalName = "";
        var principalType = "";
        var isAllowed = (this._activeUserSelectionMode == this._usersSelectionMode.AllowedUsers);
        var updatePanel = (isAllowed ? this._allowedUsersOrRolesListPanelID : this._deniedUsersOrRolesListPanelID);

        //// Clear deselected entries
        var selctionsPanel = $get(updatePanel).getElementsByTagName("ul")[0];
        if (selctionsPanel) {
            var children = selctionsPanel.children;
            var len = children.length;
            for (var i = len - 1; i >= 0; i--) {
                var child = children[i];
                for (var j = 0; j < this._deselectedPrincipals.length; j++) {
                    var deselectedPrincipal = this._deselectedPrincipals[j];
                    if (child.lastChild.id == deselectedPrincipal) {
                        var removedItem = this._getObjectFromPrincipalListItemID(deselectedPrincipal);
                        this._removeEntityFromCollection(child.lastChild, removedItem.EntityType, removedItem.EntityID, removedItem.IsAllowed, removedItem.IsDenied);
                        break;
                    }
                }
            }
        }
        this._deselectedPrincipals = new Array();

        var selections = this.get_principalSelector().getSelectedValuesFromAllSelectors();
        for (var curSelected = 0; curSelected < selections.length; curSelected++) {
            selectedItem = selections[curSelected];

            //user
            if (typeof (selectedItem.UserID) != "undefined") {
                principalId = selectedItem.UserID;
                var principalTitle = "";
                if (selectedItem.FirstName || selectedItem.LastName) {
                    principalTitle += selectedItem.FirstName ? selectedItem.FirstName : "";
                    if (selectedItem.LastName) {
                        principalTitle += selectedItem.FirstName ? " " : "";
                        principalTitle += selectedItem.LastName;
                    }
                }
                else {
                    principalTitle = selectedItem.Email;
                }
                principalName = principalTitle;
                principalType = this._wcfPrincipalType.User;
            }
                //Role
            else if (typeof (selectedItem.Id) != "undefined") {
                principalId = selectedItem.Id;
                principalName = selectedItem.Name;
                principalType = this._wcfPrincipalType.Role;
            }

            // Add selected entries
            this._addEntityToCollection(
                principalType,
                principalName,
                principalId,
                updatePanel,
                isAllowed,
                !isAllowed,
                false);
        }
        this._setUsersSelectionBoxDisplay(false);
    },

    _cancelSelectingPrincipals: function () {
        /// <summary>Handles clicking the "cancel" in the user selection panel- just hiding the panel</summary>
        this._setUsersSelectionBoxDisplay(false);
    },

    _selectedPrincipal_Click: function (sender, commandArgs) {
        /// <summary>Handles clicking the any of the already selected principals, in its list: removing it from the list</summary>
        /// <param name="sender">The click event.</param>
        /// <param name="commandArgs">The click event arguments.</param>
        var liRemovalItem = sender.target;
        var principalDetails = this._getObjectFromPrincipalListItemID(liRemovalItem.id);
        if (principalDetails != null) {
            this._removeEntityFromCollection(
	            liRemovalItem,
	            principalDetails.EntityType,
	            principalDetails.EntityID,
	            principalDetails.IsAllowed,
	            principalDetails.IsDenied);
        }
    },

    _getObjectFromPrincipalListItemID: function (liID) {
        /// <summary>Retrirves an object with the principal data, parsed from the element ID represeting it</summary>
        /// <param name="liID">ID of the principal element.</param>
        /// <returns>An object with the principal data: principal type, id, is it allowed or denied.</returns>
        var retObj = new Object();
        var idParts = liID.split("___");
        if (idParts.length < 2) {
            return null;
        }
        var detailParts = idParts[1].split("_");
        if (detailParts.length < 4) {
            return null;
        }
        retObj.EntityType = detailParts[1];
        retObj.EntityID = detailParts[0];
        retObj.IsAllowed = (detailParts[2].toUpperCase() == "TRUE");
        retObj.IsDenied = (detailParts[3].toUpperCase() == "TRUE");
        return retObj;
    },

    _userSelectionChanged: function (AppRoleID, EntityName) {
        /// <summary>Sets the correct display rules, when the radio buttons slection has changed.</summary>
        /// <param name="AppRoleID">Id of the currently selected entity, represented by the selected radio button. Empty if the list of specific principals is selected.</param>
        /// <param name="EntityName">Name of the currently selected entity, represented by the selected radio button. Empty if the list of specific principals is selected.</param>
        var rbSelectedUsers = $get(this._allowedSelectedUsersRadioButtonID);

        var OpenUsersSelectionBoxLinkButton = $get(this._openUsersSelectionBoxLinkButtonID);

        if (rbSelectedUsers != null) {
            if (rbSelectedUsers.checked) {
                this._selectedAppRole = null;
                this._displayUserSelectionLink(true);
                this._displaySelectedAllowedUsers(true);
            }
            else {
                this._selectedAppRole = new this.wcfPermission(this._wcfPrincipalType.Role, EntityName, AppRoleID, true, false, this._actionName, this._inheritsPermissions);
                this._displayUserSelectionLink(false);
                this._displaySelectedAllowedUsers(false);
            }
            this._isDirty = true;
        }
        this._autosizeMe();
    },

    _saveAndClose: function () {
        /// <summary>Handles the "Save" button click event. Saves the selected data and closes the dialog.</summary>
        if (!this._isDirty)
            this._closeMe(null);
        else {
            //save data
            var explicitlyDeniedCheckbox = $get(this._explicitlyDeniedCheckboxID);
            var keys = new Array();
            keys[keys.length] = this._permissionSetName;
            keys[keys.length] = this._dataProviderName;
            keys[keys.length] = this._actionName;

            var params = {
                'permissionObjectRootID': this._permissionObjectRootID,
                'managerClassName': this._managerClassName,
                'securedObjectID': this._securedObjectID,
                'securedObjectType': this._securedObjectType,
                'dynamicDataProviderName': this._dynamicDataProviderName
            };

            if (this._selectedAppRole == null)
                this._saveData = this._arrayActionEntities;
            else {
                this._saveData = new Array();
                if (this._selectedAppRole.PrincipalName != this._adminsOnlyConstText) {
                    this._saveData[this._saveData.length] = this._selectedAppRole;
                }
                for (var i = 0; i < this._arrayActionEntities.length; i++) {
                    if ((this._arrayActionEntities[i].IsDenied) && (explicitlyDeniedCheckbox.checked))
                        this._saveData[this._saveData.length] = this._arrayActionEntities[i];
                }
            }
            var dataSaver = new Telerik.Sitefinity.Data.ClientManager();
            dataSaver.InvokePut(
                this._permissionsUrl,
                params,     //An array. Constructing the querystring http://svc/?param1=params[param1]&param2=params[param2]...
                keys,       //An array. URL params are concatenated to the URL http://svc/key1/key2/key3...
                this._saveData,
                this._savedSuccessfully,
                this._saveFailure,
                this);
        }
    },

    _savedSuccessfully: function (sender, data) {
        /// <summary>Callback of the _saveAndClose method: after the data is saved, close the window</summary>
        /// <param name="sender">Reference to this JS extension.</param>
        /// <param name="data">Data to return.</param>
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;

        for (var i = 0; i < sender._saveData.length; i++) {
            if (sender._saveData[i].PrincipalName == this._adminsOnlyConstText)
                sender._saveData.splice(i, 1);
        }
        oWindow.close(Sys.Serialization.JavaScriptSerializer.serialize(sender._saveData));
    },

    _saveFailure: function (error) {
        /// <summary>Callback of the _saveAndClose method: Unsuccessful saving</summary>
        alert(error.Detail);
    },

    get_principalSelector: function () {
        return this._principalSelector;
    },

    set_principalSelector: function (value) {
        this._principalSelector = value;
    }
};

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection', Sys.UI.Control);
