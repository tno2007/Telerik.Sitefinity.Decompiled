Type.registerNamespace("Telerik.Sitefinity.Web.UI.Images");

Telerik.Sitefinity.Web.UI.Images.MenuList = function (element) {
    Telerik.Sitefinity.Web.UI.Images.MenuList.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.Images.MenuList.prototype = {
    // set up 
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Images.MenuList.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Images.MenuList.callBaseMethod(this, "dispose");
    }
};

Telerik.Sitefinity.Web.UI.Images.MenuList.registerClass('Telerik.Sitefinity.Web.UI.Images.MenuList', Sys.UI.Control);
