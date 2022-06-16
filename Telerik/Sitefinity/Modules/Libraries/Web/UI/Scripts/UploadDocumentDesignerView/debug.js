/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView.initializeBase(this, [element]);

    this._documentSettingsPanel = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView.callBaseMethod(this, 'initialize');

        this._uploadTabName = "uploadDocumentDesignerView";
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
    },

    _setControlData: function (controlData) {
        if (this._contentId) {
            controlData.DocumentId = this._contentId;
        }
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView', Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();