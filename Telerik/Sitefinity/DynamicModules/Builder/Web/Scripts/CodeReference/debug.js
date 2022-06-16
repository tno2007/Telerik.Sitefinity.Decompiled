Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Builder.Web.UI");

Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference = function (element) {
    Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference.initializeBase(this, [element]);

    this._moduleTypeId = null;
}

Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference.callBaseMethod(this, "initialize");
        this._initTreeView();
    },

    dispose: function () {
        Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference.callBaseMethod(this, "dispose");
    },

    _initTreeView: function () {
        var moduleTypeId = this.get_moduleTypeId();

        var treeView = $("#contentTypesTree ul:first").kendoTreeView({
            select: function (e) {
                var value = $(e.node).attr('data-id');
                window.location.href = window.location.href.replace(moduleTypeId, value);
            },
            animation: {
                expand: {
                    duration: 0
                },
                collapse: {
                    duration: 0
                }
            }
        }).data("kendoTreeView");
        treeView.expand(".k-item");
        var node = $('li[data-id="'+ moduleTypeId + '"]');
        treeView.select(node);
    },

    /* properties */

    get_moduleTypeId: function () {
        return this._moduleTypeId;
    },
    set_moduleTypeId: function (value) {
        this._moduleTypeId = value;
    }
};

Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference.registerClass("Telerik.Sitefinity.DynamicModules.Builder.Web.UI.CodeReference", Sys.UI.Control);
