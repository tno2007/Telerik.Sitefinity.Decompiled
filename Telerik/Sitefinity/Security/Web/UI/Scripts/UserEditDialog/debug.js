Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI.Principals");

Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog = function (element) {
    Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog.initializeBase(this, [element]);

    this._profilesData = null;
};

Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog.callBaseMethod(this, 'dispose');
    },



    get_profilesData: function () {
        return this._profilesData;
    },
    set_profilesData: function (value) {
        this._profilesData = value;
    }


};

Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog.registerClass('Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);