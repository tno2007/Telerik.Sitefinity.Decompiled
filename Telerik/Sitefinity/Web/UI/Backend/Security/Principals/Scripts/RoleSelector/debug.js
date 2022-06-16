//RoleSelector
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Principals");

Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector = function(element) {
    Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector.initializeBase(this, [element]);
    this._adminRoleId = null;
    this._selectAdminRoleByDefault = null;
    this._hideAdminRole = null;
}

Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector.prototype = {
    // ------------------------------------- Initialization -------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector.callBaseMethod(this, "initialize");

    },

    dispose: function() {
        Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector.callBaseMethod(this, "dispose");
    },

    _rowDataBound: function (sender, args) {
        Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector.callBaseMethod(this, "_rowDataBound", [sender, args]);

        var gridItem = args.get_item();
        var dataItem = gridItem.get_dataItem();
        var key = dataItem[this._dataKeyNames];

        if (this.get_selectAdminRoleByDefault())
        {
            if (key == this.get_adminRoleId())
            {
                gridItem.set_selected(true);

                if (this.get_hideAdminRole())
                {
                    gridItem.set_visible(false)
                }
            }
        }
    },

    get_hideAdminRole: function () {
        return this._hideAdminRole;
    },
    set_hideAdminRole: function (value) {
        this._hideAdminRole = value;
    },

    get_adminRoleId: function () {
        return this._adminRoleId;
    },
    set_adminRoleId: function (value) {
        this._adminRoleId = value;
    },

    get_selectAdminRoleByDefault: function () {
        return this._selectAdminRoleByDefault;
    },
    set_selectAdminRoleByDefault: function (value) {
        this._selectAdminRoleByDefault = value;
    },
};

Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector', Telerik.Sitefinity.Web.UI.FlatSelector);