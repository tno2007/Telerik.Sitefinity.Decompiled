Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView.initializeBase(this, [element]);

    this._resources = null;
    this._controlData = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView.callBaseMethod(this, 'initialize');

    },
    dispose: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView.callBaseMethod(this, 'dispose');

    },

    validate: function () {
        return true;
    },

    refreshUI: function () {
    },

    applyChanges: function () {
        var data = this.get_controlData();
        if (data.settings.ContentLinks) {
            data.settings.ContentLinks.length = 0;
        }
    },
    get_selectedItemsTitles: function () {
        return "";
    },

    // generates the UIDescription of the pipe
    get_uiDescription: function () {
        var res = this._resources;
        var typeName;
        var contentType = this._controlData.settings.ContentTypeName;
        var contentRes = res[contentType];
        var defaultRes = res["default"];
        if (contentRes) {
            typeName = contentRes.ItemsName;
        }
        else {
            var namespaces = contentType.split('.');
            typeName = namespaces[namespaces.length - 1];
        }
        var typePrefix = String.format("<strong>{0}</strong>: ", typeName);
        return typePrefix + defaultRes.AllItems;
    },

    get_controlData: function () {
        return this._controlData;
    },
    set_controlData: function (value) {
        this._controlData = value;
    },

    get_resources: function () {
        return this._resources;
    },
    set_resources: function (value) {
        this._resources = value;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView.registerClass(
    'Telerik.Sitefinity.Publishing.Web.UI.Designers.DefaultContentOutPipeDesignerView', Sys.UI.Control);
