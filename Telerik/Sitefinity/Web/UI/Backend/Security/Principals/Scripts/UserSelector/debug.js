//UserSelector
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Principals");

Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector = function(element) {
    Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector.initializeBase(this, [element]);
    this._hideAdminUsers = null;
    this._sortExpression = null;
}

Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector.callBaseMethod(this, "initialize");
    },

    dispose: function() {
        Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector.callBaseMethod(this, "dispose");
    },

    bindSelector: function () {
        var urlParams = this.get_binder().get_urlParams();
        urlParams['ignoreAdminUsers'] = this.get_hideAdminUsers();
        this.get_binder().set_sortExpression(this.get_sortExpression());
        this.get_binder().set_urlParams(urlParams);        
        Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector.callBaseMethod(this, "bindSelector");                
    },

    get_hideAdminUsers: function () {
        return this._hideAdminUsers;
    },
    set_hideAdminUsers: function (value) {        
        this._hideAdminUsers = value;
    },
    get_sortExpression: function () {
        return this._sortExpression;
    },
    set_sortExpression: function (value) {
        this._sortExpression = value;
    }
};

Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector', Telerik.Sitefinity.Web.UI.FlatSelector);