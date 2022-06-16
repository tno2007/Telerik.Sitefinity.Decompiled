Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected = function (element) {
    Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected.callBaseMethod(this, 'dispose');
    }
}

Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected.registerClass('Telerik.Sitefinity.Web.UI.PageItemsBuilderSelected', Telerik.Sitefinity.Web.UI.PageItemsBuilder);