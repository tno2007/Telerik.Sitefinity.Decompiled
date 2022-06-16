Type.registerNamespace("Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views");

Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView = function (element) {

    this._permissionsControlSelectorID = null;
    this._globalItemsActionPermissionsListID = null;
    this._globalUserPermissionsListID = null;
    this._globalUserPermissionsListRolesID = null;
    this._sidebarID = null;   
    this._moduleSelectorID = null;
    this._userSelectorID = null;
    this._roleSelectorID = null;
    this._dynamicModulePermissionSet = null;

    Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView.prototype = {

    // ------------------------------------- Initialization -------------------------------------

    initialize: function () {
        /// <summary>Initializes the global permissions view control</summary>
        Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView.callBaseMethod(this, "initialize");
        Sys.Application.add_load(Function.createDelegate(this, this.onload));
    },

    dispose: function () {
        /// <summary>Called upon disposal of the global permissions view control</summary>
        Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView.callBaseMethod(this, "dispose");
    },

    onload: function () {
        /// <summary>Invoked after the global permissions view control is loaded</summary>
        var tabStrip = $find(this._permissionsControlSelectorID);
        tabStrip.add_tabSelected(Function.createDelegate(this, this._tabStrip_TabSelected));       

        var moduleSelector = $find(this._moduleSelectorID);
        moduleSelector.add_itemSelected(Function.createDelegate(this, this._module_Selected));
        moduleSelector.add_clientLoaded(Function.createDelegate(this, this._module_SelectorLoaded));

        var userSelector = $find(this._userSelectorID);
        userSelector.add_itemSelected(Function.createDelegate(this, this._user_Selected));
        userSelector.add_selectorReady(Function.createDelegate(this, this._user_SelectorReady));
        ;

        var roleSelector = $find(this._roleSelectorID);
        roleSelector.add_itemSelected(Function.createDelegate(this, this._role_Selected));
        roleSelector.add_selectorReady(Function.createDelegate(this, this._role_SelectorReady));
    },

    // ------------------------------------- Public methods -------------------------------------

    bindSelectedModule: function (module) {
        /// <summary>Binds the global ItemsActionPermissionsList control to a specific module</summary>
        /// <param name="module">A client object representation of the ModuleProviderAssociation class.</param>
        /// <returns>void.</returns>    
        if (module != null) {
            var globalItemsActionPermissionsList = $find(this._globalItemsActionPermissionsListID);

            if (module.IsDynamicModule) {
                globalItemsActionPermissionsList.set_applyDynamicModulePermissions(true);
                globalItemsActionPermissionsList.set_permissionSetName(this._dynamicModulePermissionSet);
            }
            else {
                globalItemsActionPermissionsList.set_applyDynamicModulePermissions(false);
                globalItemsActionPermissionsList.set_permissionSetName("");
            }

            globalItemsActionPermissionsList.set_showPermissionSetName(false);
            globalItemsActionPermissionsList.bindToModule(module);
        }
    },

    // ------------------------------------- Control callbacks -------------------------------------
 
    _module_Selected: function (sender, args) {
        /// <summary>Invoked when a module is selected in the module selector control.</summary>
        /// <param name="sender">The module selector control.</param>
        /// <param name="args">The selected module data item.</param>
        /// <returns>void.</returns>
        this.bindSelectedModule(args);
    },

    _module_SelectorLoaded: function () {
        /// <summary>Invoked when the module selector is loaded and ready</summary>
        var moduleSelector = $find(this._moduleSelectorID);
        this.bindSelectedModule(moduleSelector.get_selectedModule());
    },

    _user_Selected: function (sender, selectedItem) {
        //User
        if (typeof (selectedItem.UserID) != "undefined") {
            var userId = selectedItem.UserID;

            var userTitle = selectedItem.FirstName + " " + selectedItem.LastName;
            if (((selectedItem.FirstName == "") && (selectedItem.LastNam == "")) || ((selectedItem.FirstName == null) && (selectedItem.LastNam == null)))
                userTitle = selectedItem.Email;

            userName = userTitle;

            this._bindByUser(userId, userName);
        }
    },

    _user_SelectorReady: function (sender, args) {
        sender.setFirstRowActive();
    },

    _role_Selected: function (sender, selectedItem) {
        //Role
        if (typeof (selectedItem.Id) != "undefined") {
            var roleId = selectedItem.Id;
            var roleName = selectedItem.Name;

            this._bindByRole(roleId, roleName);
        }
    },

    _role_SelectorReady: function (sender, args) {
        sender.setFirstRowActive();
    },   

    _tabStrip_TabSelected: function () {
        /// <summary>Invoked a tab selection is changed (by section/by user/by role)</summary>
        var tabStrip = $find(this._permissionsControlSelectorID);
        var rtSection = ((tabStrip == null) ? null : tabStrip.findTabByValue("bySection"));
        var rtUser = ((tabStrip == null) ? null : tabStrip.findTabByValue("byUser"));
        var rtRole = ((tabStrip == null) ? null : tabStrip.findTabByValue("byRole"));
        var globalItemsActionPermissionsList = $find(this._globalItemsActionPermissionsListID);       
        var globalUserPermissionsList = $find(this._globalUserPermissionsListID);
        var globalUserPermissionsListRoles = $find(this._globalUserPermissionsListRolesID);        

        if ((rtSection != null) && (rtSection.get_selected()) && (globalItemsActionPermissionsList != null)) {
            //Section
            var moduleSelector = $find(this._moduleSelectorID);
            this.bindSelectedModule(moduleSelector.get_selectedModule());            
        }
        else if ((rtUser != null) && (rtUser.get_selected()) && (globalUserPermissionsList != null)) {
            //User
            globalUserPermissionsList.dataBind();
        }
        else if ((rtRole != null) && (rtRole.get_selected()) && (globalUserPermissionsListRoles != null)) {
            //Role
            globalUserPermissionsListRoles.dataBind();
        }
    },

    // ------------------------------------- Private methods -------------------------------------

    _bindByUser: function (userId, userName) {
        if (userId != null) {
            var globalUserPermissionsList = $find(this._globalUserPermissionsListID);
            globalUserPermissionsList.set_principalID(userId);
            if (userName != null) {
                globalUserPermissionsList.set_principalName(userName);
                globalUserPermissionsList.set_showPrincipalName(true);
            }

            globalUserPermissionsList.dataBind();
        }
    },

    _bindByRole: function (roleId, roleName) {        
        if (roleId != null) {
            var globalUserPermissionsListRoles = $find(this._globalUserPermissionsListRolesID);
            globalUserPermissionsListRoles.set_principalID(roleId);
        if (roleName != null) {
            globalUserPermissionsListRoles.set_principalName(roleName);
            globalUserPermissionsListRoles.set_showPrincipalName(true);
        }

        globalUserPermissionsListRoles.dataBind();
    }
}
};
Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView.registerClass('Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView', Sys.UI.Control);