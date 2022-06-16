Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields");

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField = function (element) {
    this._element = element;
    this._isBackend = false;
    Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField.callBaseMethod(this, "initialize");
    },

    _load: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField.callBaseMethod(this, "_load");
        if (this.get_isBackend())
            this.get_nodeSelector().get_treeBinder().get_urlParams()['root'] = 'backend';
    },
    
    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField.callBaseMethod(this, "dispose");
    },

    get_isBackend: function() {
        return this._isBackend;
    },

    set_isBackend: function(value) {
        this._isBackend = value;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageSelectorField", Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField);