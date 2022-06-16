﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions");

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector.initializeBase(this, [element]);
    this._rolesSelector = null;
    this._usersSelector = null;
    this._principalSelector = null;
    this._wcfPrincipalType = null;

    this._isDirty = false;
    this._selectedPrincipals = [];

    this._selectionChangedDelegate = null;
}

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector.callBaseMethod(this, "initialize");
        this._wcfPrincipalType = Sys.Serialization.JavaScriptSerializer.deserialize(this._wcfPrincipalType);

        this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChanged);
        this._principalSelector.add_command(this._selectionChangedDelegate);
    },

    dispose: function () {
        if (this._selectionChangedDelegate) {
            if (this._principalSelector) {
                this._principalSelector.remove_command(this._selectionChangedDelegate);
            }
            delete this._selectionChangedDelegate;
        }
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector.callBaseMethod(this, "dispose");
    },

    // ------------------------------------- Public methods -------------------------------------

    getSelectedPrincipals: function () {
        if (this._isDirty) {
            this._selectedPrincipals = [];
            var selectedItems = this._principalSelector.getSelectedValuesFromAllSelectors();
            var principalId = null;
            var principalName = null;
            var principalType = null;
            var principal = null;

            for (var i = 0; i < selectedItems.length; i++) {
                var selectedItem = selectedItems[i];

                //User
                if (selectedItem.UserID) {
                    principalId = selectedItem.UserID;
                    var principalTitle = "";
                    if (selectedItem.DisplayName) {
                        principalTitle = selectedItem.DisplayName;
                    }
                    else {
                        principalTitle = selectedItem.Email;
                    }
                    principalName = principalTitle;
                    principalType = this._wcfPrincipalType.User;
                }
                //Role
                else if (selectedItem.Id) {
                    principalId = selectedItem.Id;
                    principalName = selectedItem.Name;
                    principalType = this._wcfPrincipalType.Role;
                }

                principal = { Id: principalId, Name: principalName, Type: principalType };
                this._selectedPrincipals.push(principal);
            }
            this._isDirty = false;
        }
        return this._selectedPrincipals;
    },

    // ------------------------------------- Event handlers -------------------------------------

    _selectionChanged: function (sender, args) {
        this._isDirty = true;
    },

    // ------------------------------------- Properties -------------------------------------

    get_rolesSelector: function () {
        return this._rolesSelector;
    },
    set_rolesSelector: function (value) {
        this._rolesSelector = value;
    },

    get_usersSelector: function () {
        return this._usersSelector;
    },
    set_usersSelector: function (value) {
        this._usersSelector = value;
    },

    get_principalSelector: function () {
        return this._principalSelector;
    },
    set_principalSelector: function (value) {
        this._principalSelector = value;
    },

    get_isDirty: function () {
        return this._isDirty;
    },
    set_isDirty: function (value) {
        this._isDirty = value;
    }
};

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SimpleUsersAndRolesSelector', Sys.UI.Control);