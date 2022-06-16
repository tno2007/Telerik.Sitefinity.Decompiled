// ----------------------------------------------------------------------------
// Assign users to role dialog client-side component
// ----------------------------------------------------------------------------

Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ----------------------------------------------------------------------------
// Class: AssignUsersToRoleDialog
// ----------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog = function(id) {
    Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog.initializeBase(this, [id]);

    this._allUsersLabel = null;
    this._usersInRoleLabel = null;
    this._allUsersTitle = null;
    this._usersInRoleTitle = null;
    this._usersProviderName = null;
    this._rolesProviderName = null;
    this._roleId = null;
    this._tabSwithcer = null;
    this._allUsersBinder = null;
    this._inRoleBinder = null;
    this._pickRolesManager = null;
    this._rolesServiceUrl = null;
    this._usersServiceUrl = null;
    this._sortExpression = null;
    this._filterExpression = null;
    this._allUsersGrid = null;
    this._inRoleGrid = null;
    this._change = {};
    this._messageControl = null;
    this._allUserProviders = null;
    this._providersList = null;
    this._roleName = null;
    this._serviceSelectionEnabled = false;
    this._searchBox = null;
    this._title = null;
    this._titleLabel = null;
    this._saveButton = null;
    this._cancelButton = null;
    this._saveManager = null;
    this._showAllUsersCount = false;
    this._countManager = false;
    this._showRowNameInTitle = false;
}
Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog.prototype = {
    // ------------------------------------------------------------------------
    // Initialize & dispose
    // ------------------------------------------------------------------------
    "initialize": function () {
        Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog.callBaseMethod(this, "initialize");

        // deserialize server data
        this._allUserProviders = Sys.Serialization.JavaScriptSerializer.deserialize(this._allUserProviders);

        // create delegates
        this._allUsersDataBoundDelegate = Function.createDelegate(this, this._allUsersDataBound);
        this._inRoleDataBoundDelegate = Function.createDelegate(this, this._inRoleDataBound);
        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoad);
        this._binderErrorDelegate = Function.createDelegate(this, this._binderError);
        this._providersListChangeDelegate = Function.createDelegate(this, this._providersListChange);
        this._tabSelectedDelegate = Function.createDelegate(this, this._tabSelected);
        this._rowSelectingDelegate = Function.createDelegate(this, this._rowSelecting);
        this._rowSelectedDelegate = Function.createDelegate(this, this._rowSelected);
        this._rowDeselectingDelegate = Function.createDelegate(this, this._rowDeselecting);
        this._rowDeselectedDelegate = Function.createDelegate(this, this._rowDeselected);
        this._doSearchDelegate = Function.createDelegate(this, this._doSearch);
        this._saveChangesDelegate = Function.createDelegate(this, this._saveChanges);
        this._cancelChangesDelegate = Function.createDelegate(this, this._cancelChanges);

        Sys.Application.add_load(this._pageLoadDelegate);
    },
    "dispose": function () {
        Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog.callBaseMethod(this, "dispose");

        // unsubscribe from events
        this._allUsersGrid.remove_rowSelecting(this._rowSelectingDelegate);
        this._allUsersGrid.remove_rowSelected(this._rowSelectedDelegate);
        this._allUsersGrid.remove_rowDeselecting(this._rowDeselectingDelegate);
        this._allUsersGrid.remove_rowDeselected(this._rowDeselectedDelegate);
        this._inRoleGrid.remove_rowSelecting(this._rowSelectingDelegate);
        this._inRoleGrid.remove_rowSelected(this._rowSelectedDelegate);
        this._inRoleGrid.remove_rowDeselecting(this._rowDeselectingDelegate);
        this._inRoleGrid.remove_rowDeselected(this._rowDeselectedDelegate);
        $clearHandlers(this._providersList);
        Sys.Application.remove_load(this._pageLoadDelegate);
        this._tabSwithcer.remove_tabSelected(this._tabSelectedDelegate);
        this._searchBox.remove_manualSearch(this._doSearchDelegate);
        $clearHandlers(this._saveButton);
        $clearHandlers(this._cancelButton);

        // free DOM elements and components
        this._allUsersLabel = null;
        this._usersInRoleLabel = null;
        this._tabSwithcer = null;
        this._allUsersBinder = null;
        this._inRoleBinder = null;
        this._inRoleGrid = null;
        this._allUsersGrid = null;
        this._pickRolesManager = null;
        this._messageControl = null;
        this._providersList = null;
        this._searchBox = null;
        this._titleLabel = null;
        this._saveButton = null;
        this._cancelButton = null;
        if (this._pickRolesManager != null) {
            this._pickRolesManager.dispose();
            this._pickRolesManager = null;
        }
        if (this._saveManager != null) {
            this._saveManager.dispose();
            this._saveManager = null;
        }
        if (this._countManager != null) {
            this._countManager.dispose();
            this._countManager = null;
        }

        // free delegates
        this._rowSelectingDelegate = null;
        this._rowSelectedDelegate = null;
        this._allUsersDataBoundDelegate = null;
        this._inRoleDataBoundDelegate = null;
        this._tabSelectedDelegate = null;
        this._binderErrorDelegate = null;
        this._pageLoadDelegate = null;
        this._rowDeselectingDelegate = null;
        this._rowDeselectedDelegate = null;
        this._doSearchDelegate = null;
        this._saveChangesDelegate = null;
        this._cancelChangesDelegate = null;
    },
    "_pageLoad": function (sender, args) {
        // retrieve DOM elements and components
        this._tabSwithcer = $find(this._tabSwithcer);
        this._allUsersBinder = $find(this._allUsersBinder);
        this._inRoleBinder = $find(this._inRoleBinder);
        this._allUsersLabel = $get(this._allUsersLabel);
        this._usersInRoleLabel = $get(this._usersInRoleLabel);
        this._allUsersGrid = $find(this._allUsersGrid);
        this._inRoleGrid = $find(this._inRoleGrid);
        this._messageControl = $find(this._messageControl);
        this._providersList = $get(this._providersList);
        this._searchBox = $find(this._searchBox);
        this._titleLabel = $get(this._titleLabel);
        this._saveButton = $get(this._saveButton);
        this._cancelButton = $get(this._cancelButton);

        // subscribe to events
        this._allUsersBinder.add_onDataBound(this._allUsersDataBoundDelegate);
        this._inRoleBinder.add_onDataBound(this._inRoleDataBoundDelegate);
        this._allUsersBinder.add_onError(this._binderErrorDelegate);
        this._inRoleBinder.add_onError(this._binderErrorDelegate);
        $addHandler(this._providersList, "change", this._providersListChangeDelegate);
        this._tabSwithcer.add_tabSelected(this._tabSelectedDelegate);
        this._allUsersGrid.add_rowSelecting(this._rowSelectingDelegate);
        this._allUsersGrid.add_rowSelected(this._rowSelectedDelegate);
        this._inRoleGrid.add_rowSelecting(this._rowSelectingDelegate);
        this._inRoleGrid.add_rowSelected(this._rowSelectedDelegate);
        this._allUsersGrid.add_rowDeselecting(this._rowDeselectingDelegate);
        this._allUsersGrid.add_rowDeselected(this._rowDeselectedDelegate);
        this._inRoleGrid.add_rowDeselecting(this._rowDeselectingDelegate);
        this._inRoleGrid.add_rowDeselected(this._rowDeselectedDelegate);
        this._searchBox.add_manualSearch(this._doSearchDelegate);
        $addHandler(this._saveButton, "click", this._saveChangesDelegate);
        $addHandler(this._cancelButton, "click", this._cancelChangesDelegate);

        // set initial state
        this._setAllUsersTitle("");
        this._setSelectedUsersTitle("");
        this._allUsersBinder.set_clearSelectionOnRebind(false);
        this._inRoleBinder.set_clearSelectionOnRebind(false);
        this._titleLabel.innerHTML = String.format(this._title, this._roleName);
    },

    _initCountManager: function () {
        if (!this._countManager) {
            this._countManager = new Telerik.Sitefinity.Data.ClientManager();
        }
        this._updateUsersProviderName();
    },

    _initPickRolesManager: function () {
        if (!this._pickRolesManager) {
            this._pickRolesManager = new Telerik.Sitefinity.Data.ClientManager();
        }
        this._updateUsersProviderName();
    },

    _initSaveManager: function () {
        if (!this._saveManager) {
            this._saveManager = new Telerik.Sitefinity.Data.ClientManager();
        }
    },

    _initAllUsersBinder: function () {
        // /all/?provider={usersProvider}?forAllProviders={forAllUserProviders}&sort={sort}&take={take}&skip={skip}&roleProvider={roleProviderName}&roleId={roleIdString}
        this._updateUsersProviderName();
        var forAllProviders = this._usersProviderName == null;
        this._allUsersBinder.set_serviceBaseUrl(this._usersServiceUrl + "/all/");
        this._allUsersBinder.set_globalDataKeys([]);
        this._allUsersBinder.set_provider(this._usersProviderName != null ? this._usersProviderName : "");
        this._allUsersBinder.set_sortExpression(this._sortExpression);
        this._allUsersBinder.set_filterExpression(this._filterExpression);
        this._allUsersBinder.set_urlParams({
            "forAllProviders": forAllProviders,
            "roleProvider": this._rolesProviderName,
            "roleId": this._roleId
        });
    },

    _initUsersInRoleBinder: function () {
        // GetWcfUsersInRole/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&userProvider={userProvider}&forAllUserProviders={forAllUserProviders}      
        this._updateUsersProviderName();

        var forAllUserProviders = this._usersProviderName == null;

        this._inRoleBinder.set_serviceBaseUrl(this._rolesServiceUrl + "/GetWcfUsersInRole/" + this._roleId + "/");
        this._inRoleBinder.set_globalDataKeys([]);
        this._inRoleBinder.set_provider(this._rolesProviderName != null ? this._rolesProviderName : "");
        this._inRoleBinder.set_sortExpression(this._sortExpression);
        this._inRoleBinder.set_filterExpression(this._filterExpression);
        this._inRoleBinder.set_urlParams({
            "userProvider": this._usersProviderName,
            "forAllUserProviders": forAllUserProviders
        });

        var self = this;

        // TODO: find a better way to PUT GetCollection 
        // Not done through ClientManager.InvokePut, because of RadGrid's paging (i.e. RadGridBinder's DataBind and TargetCommand methods)
        // DIRTY HACK
        this._inRoleBinder.get_manager()._getItemCollection = function (successDelegate, binder, dataKeys, context) {
            // this is ClientManager
            if (binder.get_provider() != null) {
                this.get_urlParams()['provider'] = binder.get_provider();
            }

            if (binder.get_sortExpression() != null) {
                this.get_urlParams()['sortExpression'] = binder.get_sortExpression();
            }

            if (binder.get_skip() != null) {
                this.get_urlParams()['skip'] = binder.get_skip();
            }

            if (binder.get_take() != null) {
                this.get_urlParams()['take'] = binder.get_take();
            }

            if (binder.get_filterExpression() != null) {
                this.get_urlParams()['filter'] = binder.get_filterExpression();
            }

            var url = this._getServiceUrl(binder, dataKeys);

            this._invokeWebService(url, "PUT", self._getAllStoredUsers(), binder, successDelegate, context);
        }
    },

    // ------------------------------------------------------------------------
    // Public functions
    // ------------------------------------------------------------------------
    dataBind: function () {
        /// <summary>Data bind the current mode of the dialog</summary>
        if (typeof this._roleId == "string") {
            this._titleLabel.innerHTML = String.format(this._title, this._roleName);
            var mode = this.get_mode();
            this._messageControl.hide();
            this._setIsServiceSelection(true);
            if (mode == "allUsers") {
                this.binderAllUsers();
            }
            else if (mode == "usersInRole") {
                this.bindUsersInRole();
            }
            else {
                this._raiseError(Object.getTypeName(this) + ".dataBind(): Invalid mode : " + mode);
            }
        }
        else {
            this._raiseError("You must call set_roleId before calling dataBind");
        }
    },

    binderAllUsers: function () {
        /// <summary>Explicitly bind the "all users" mode. This does not make the mode visible, though.</summary>
        this._initAllUsersBinder();
        this._allUsersBinder.DataBind();
    },

    bindUsersInRole: function () {
        /// <summary>Explicitly bind the "users in a specific role" mode. This does not make the mode visible, though.</summary>
        this._initUsersInRoleBinder();
        this._inRoleBinder.DataBind();
    },

    pickSelectedUsersFromRange: function (usersToPickFrom) {
        /// <summary>Explicitly call the service for selecting users in the "all users" mode. This does not make the mode visible.</summary>
        this._initPickRolesManager();

        var iter = usersToPickFrom.length;
        var idsToPickFrom = [];
        while (iter--) {
            idsToPickFrom.push(usersToPickFrom[iter].UserID);
        }

        // url, urlParams, keys, data, successDelegate, failureDelegate, caller, disableValidation, validationGroup, context       
        this._pickRolesManager.InvokePut(
            this._rolesServiceUrl,
            { "provider": this._rolesProviderName != null ? this._rolesProviderName : "" },
            ["pick", this._roleId],
            idsToPickFrom,
            this._pickedRolesReceivedCallback,
            this._raiseError,
            this,
            false,
            "",
            usersToPickFrom
        );
    },

    getAllUsersCount: function () {
//        //countWithLocalChanges/?roleProvider={roleProvider}&roleId={roleId}&filter={filter}&userProvider={userProvider}&forAllProviders={forAllProviders}
//        this._initCountManager();
//        this._updateUsersProviderName();

//        this._countManager.InvokePut(
//            this._rolesServiceUrl,
//            {
//                "roleProvider": this._rolesProviderName,
//                "roleId": this._roleId,
//                "filter": this._filterExpression,
//                "userProvider": this._usersProvider,
//                "forAllProviders": (this._usersProvider == null) + ""
//            },
//            ["countWithLocalChanges"],
//            this._getAllStoredUsers(),
//            this._allUsersCountReceivedCallback,
//            this._raiseError,
//            this
//        );
    },

    // ------------------------------------------------------------------------
    // Changed users
    // ------------------------------------------------------------------------

    _isUserStored: function (id, providerName) {
        var key = this._getChangeKey(id, providerName);
        return this._change.hasOwnProperty(key);
    },

    _getStoredUser: function (id, providerName) {
        var key = this._getChangeKey(id, providerName);
        if (this._change.hasOwnProperty(key)) {
            return this._change[key];
        }
        else {
            return null;
        }
    },

    _getAllStoredUsers: function () {
        var storedUsers = [];
        for (var key in this._change) {
            if (this._change.hasOwnProperty(key)) {
                storedUsers.push(this._change[key]);
            }
        }
        return storedUsers;
    },

    _storeUserToRemove: function (id, providerName) {
        this._storeUserChange(id, providerName, true);
    },

    _storeUserToAdd: function (id, providerName) {
        this._storeUserChange(id, providerName, false);
    },

    _storeUserChange: function (id, providerName, remove) {
        var key = this._getChangeKey(id, providerName);
        this._changeSelectedUsersCount(remove ? -1 : 1);
        if (!this._change.hasOwnProperty(key)) {

            this._change[key] = {
                "UserId": id,
                "ProviderName": providerName,
                "Remove": remove
            };
        }
        else {
            this._change[key].Remove = remove;
        }
    },

    _getChangeKey: function (id, providerName) {
        return providerName + "/" + id;
    },

    _intersectSelectedIdsWithChange: function (visuallySelectedUsers) {
        var intersected = [];
        var iter = visuallySelectedUsers.length;
        while (iter--) {
            var user = visuallySelectedUsers[iter];
            var stored = this._getStoredUser(user.UserID, user.ProviderName);
            if (stored == null || !stored.Remove) {
                intersected.push(user.UserID);
            }
        }
        var storedUsers = this._getAllStoredUsers();
        var iter = storedUsers.length;
        while (iter--) {
            var stored = storedUsers[iter];
            var existingIter = intersected.length;
            var exists = false;
            while (existingIter--) {
                if (intersected[existingIter] == stored.UserId) {
                    exists = true;
                    break;
                }
            }
            if (exists) {
                continue;
            }
            if (stored.Remove == false &&
                (stored.ProviderName == this._usersProviderName || this._usersProviderName == null)) {
                intersected.push(stored.UserId);
            }
        }

        return intersected;
    },

    // ------------------------------------------------------------------------
    // Event handlers
    // ------------------------------------------------------------------------

    _saveChanges: function (sender, args) {
        var storedUsers = this._getAllStoredUsers();
        if (storedUsers.length > 0) {
            this._initSaveManager();
            // SaveRoleUser/{roleId}/?provider={provider}           
            this._saveManager.InvokePut(
                this._rolesServiceUrl,
                { "provider": this._rolesProviderName },
                ["SaveRoleUser", this._roleId],
                storedUsers,
                this._changesSavedCallback,
                this._raiseError,
                this
            );
        }
        else {
            this._change = new Object();
            dialogBase.close();
        }
    },

    _changesSavedCallback: function (caller, data, request) {
        caller._changesSaved.apply(caller, []);
    },

    _changesSaved: function () {
        this._change = new Object();
        dialogBase.closeAndRebind();
    },

    _cancelChanges: function (sender, args) {
        this._change = new Object();
        dialogBase.close();
    },

    _allUsersCountReceivedCallback: function (caller, data, request) {
        caller._allUsersCountReceived.apply(caller, [data]);
    },

    _allUsersCountReceived: function (data) {
        this._setSelectedUsersCount(parseInt(data));
    },

    _doSearch: function (sender, args) {
        var searchFilter = args.get_query();
        this.set_filterExpression(searchFilter);
        this.dataBind();
    },

    _rowSelecting: function (sender, args) {
        // if you need to stop some selection, do it here
    },

    _rowDeselecting: function (sender, args) {
        // if you need to stop deselecting, do it here
    },

    _rowSelected: function (sender, args) {
        if (!this._isServiceSelection()) {
            var gridItem = args.get_gridDataItem();
            var dataItem = gridItem.get_dataItem();
            this._storeUserToAdd(dataItem.UserID, dataItem.ProviderName);

        }
    },

    _rowDeselected: function (sender, args) {
        if (!this._isServiceSelection()) {
            var gridItem = args.get_gridDataItem();
            var dataItem = gridItem.get_dataItem();
            this._storeUserToRemove(dataItem.UserID, dataItem.ProviderName);
        }
    },

    _providersListChange: function (sender, args) {
        this._updateUsersProviderName();
        this.dataBind();
    },

    _updateUsersProviderName: function () {
        var idx = this._providersList.selectedIndex;
        if (idx == 0) {
            // all user providers
            this._usersProviderName = null;
        }
        else {
            this._usersProviderName = this._providersList.options[idx].value;
        }
        return this._usersProviderName;
    },

    _tabSelected: function (sender, args) {
        this.dataBind();
    },

    _allUsersDataBound: function (sender, args) {
        var collectionContext = args.get_dataItem();

        this._setAllUsersTitle(collectionContext.TotalCount);
        this._setSelectedUsersTitle(""); // clear        
        this.getAllUsersCount();


        this._setIsServiceSelection(true);
        this._allUsersGrid.get_masterTableView().clearSelectedItems();
        this._setIsServiceSelection(false);

        if (collectionContext.Items.length > 0) {
            this.pickSelectedUsersFromRange(collectionContext.Items);
        }
    },

    _inRoleDataBound: function (sender, args) {
        var collectionContext = args.get_dataItem();
        var usersInRolePerPage = collectionContext.Items;
        this._setSelectedUsersTitle(collectionContext.TotalCount);
        this._setAllUsersTitle(""); // clear

        this._setIsServiceSelection(true);
        var tableView = this._inRoleGrid.get_masterTableView();
        tableView.clearSelectedItems();
        var gridItems = tableView.get_dataItems();
        var gridItemIter = gridItems.length;
        while (gridItemIter--) {
            var gridItem = gridItems[gridItemIter];
            var dataItem;
            var desiredID;
            try {
                dataItem = gridItem.get_dataItem();
                desiredID = dataItem.UserID;
            }
            catch (error) {
                continue;
            }
            var userIter = usersInRolePerPage.length;
            while (userIter--) {
                if (desiredID == usersInRolePerPage[userIter].UserID) {
                    gridItem.set_selected(true);
                }
            }
        }
        this._setIsServiceSelection(false);
    },

    _pickedRolesReceivedCallback: function (caller, data, request) {
        caller._pickedRolesReveived.apply(caller, [data, request.get_userContext().Context]);
    },

    _pickedRolesReveived: function (data, usersInPage) {
        var ids = data.Items;
        {
            var selectedUsers = [];
            var idIter = ids.length;
            while (idIter--) {
                var neededID = ids[idIter];
                var userIter = usersInPage.length;
                while (userIter--) {
                    var user = usersInPage[userIter];
                    if (user.UserID == neededID) {
                        selectedUsers.push(user);
                        break;
                    }
                }
            }
            ids = this._intersectSelectedIdsWithChange(selectedUsers);
        }
        var tableView = this._allUsersGrid.get_masterTableView();
        var gridItems = tableView.get_dataItems();
        var gridItemIter = gridItems.length;
        this._setIsServiceSelection(true);
        while (gridItemIter--) {
            var gridItem = gridItems[gridItemIter];
            var desiredID;
            try {
                desiredID = gridItem.get_dataItem().UserID;
            }
            catch (error) {
                continue;
            }
            var idIter = ids.length;
            while (idIter--) {
                if (desiredID == ids[idIter]) {
                    gridItem.set_selected(true);
                }
            }
        }
        this._setIsServiceSelection(false);
    },

    _binderError: function (sender, args) {
        this._raiseError(args.get_error());
    },

    // ------------------------------------------------------------------------
    // Events
    // ------------------------------------------------------------------------
    add_error: function (delegate) {
        /// <summary>Subscribe a delegate to the event which is raised whenever an error occurs.</summary>
        /// <param name="delegate">Event handler to subscribe to the "error" event</param>
        /// <remarks>If you subscribe, the integrated message control won't be used, and you will be responsible for displaying the error.</remarks>
        this.get_events().addHandler("error", delegate);
    },
    remove_error: function (delegate) {
        this.get_events().removeHandler("error", delegate);
    },

    // ------------------------------------------------------------------------
    // Event raising
    // ------------------------------------------------------------------------

    _raiseError: function (error) {
        error = error ? error : "";
        error = error.Detail ? error.Detail : error;

        var self = this.Caller ? this.Caller : this;

        var handler = self.get_events().getHandler("error");
        if (handler != null) {
            var args = self._getErrorArgs(error);
            handler(self, args);
        }
        else {
            self._messageControl.showNegativeMessage(error);
        }
    },

    _getErrorArgs: function (message) {
        var args = new Sys.EventArgs();
        args.get_error = function () { return message };
        return args;
    },

    // ------------------------------------------------------------------------
    // Utility functions
    // ------------------------------------------------------------------------
    // if called with 2 arguments: propertyName, value
    // if called with 3 arguments: propertyName, fieldName, value
    _setPropertyIfChanged: function () {
        var propertyName = arguments[0];
        var value = arguments.length == 3 ? arguments[2] : arguments[1];
        var fieldName = arguments.length == 3 ? arguments[1] : null;
        if (!fieldName)
            fieldName = "_" + propertyName.charAt(0) + propertyName.slice(1);

        if (!fieldName || !this.hasOwnProperty(fieldName)) return;

        var currentValue = this[fieldName];
        if (currentValue != value) {
            this[fieldName] = value;
            this.raisePropertyChanged(propertyName);
        }
    },

    _setAllUsersTitle: function (count) {
        if (this._showAllUsersCount) {
            count = typeof count == typeof 1 ? "(" + count + ")" : "";
        }
        else {
            count = "";
        }
        this._allUsersLabel.innerHTML = String.format(this._allUsersTitle, count);
    },

    _setSelectedUsersCount: function (count) {
        this._selectedUsersCountInternal = count;
        this._setSelectedUsersTitle(this._selectedUsersCountInternal);
    },

    _changeSelectedUsersCount: function (changeBy) {
        this._selectedUsersCountInternal += changeBy;
        this._setSelectedUsersTitle(this._selectedUsersCountInternal);
    },

    _setSelectedUsersTitle: function (count) {
        count = typeof count == typeof 1 ? "(" + count + ")" : "";
        var name = this._showRowNameInTitle && typeof this._roleName == typeof "" && this._roleName.length > 0 ? "'" + this._roleName + "'" : "";
        this._usersInRoleLabel.innerHTML = String.format(this._usersInRoleTitle, name, count + "");
    },

    _setIsServiceSelection: function (enable) {
        this._serviceSelectionEnabled = enable;
    },

    _isServiceSelection: function () {
        return this._serviceSelectionEnabled;
    },

    // ------------------------------------------------------------------------
    // Properties
    // ------------------------------------------------------------------------
    get_mode: function () {
        /// <summary>Retrieve the current mode</summary>
        /// <returns>String: either "allUsers" or "usersInRole"</returns>
        if (this._tabSwithcer.get_selectedTab().get_index() == 0) {
            return "allUsers";
        }
        else {
            return "usersInRole";
        }
    },
    get_usersProviderName: function () { return this._usersProviderName; },
    set_usersProviderName: function (val) { this._setPropertyIfChanged("usersProviderName", val); },
    get_roleId: function () { return this._roleId; },
    set_roleId: function (val) { this._setPropertyIfChanged("roleId", val); },
    get_rolesProviderName: function () { return this._rolesProviderName; },
    set_rolesProviderName: function (val) { this._setPropertyIfChanged("rolesProviderName", val) },
    get_sortExpression: function () { return this._sortExpression; },
    set_sortExpression: function (val) { this._setPropertyIfChanged("sortExpression", val); },
    get_filterExpression: function () { return this._filterExpression; },
    set_filterExpression: function (val) { this._setPropertyIfChanged("filterExpression", val); },
    get_roleName: function () { return this._roleName; },
    set_roleName: function (val) { this._setPropertyIfChanged("roleName", val); },
    get_title: function () { return this._title; },
    set_title: function (val) { this._setPropertyIfChanged("title", val); }
}
Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog.registerClass("Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog", Sys.UI.Control);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded(); 