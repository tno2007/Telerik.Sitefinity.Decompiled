Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public");

// ------------------------------------------------------------------------
// SubscribeFormDesigner class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner.callBaseMethod(this, 'dispose');
    }

    /* ************************* public methods ************************* */

    /* ************************* properties ************************* */
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscribeFormDesigner', Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase);