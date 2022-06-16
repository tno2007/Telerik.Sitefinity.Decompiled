/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
//Type._registerScript("UploadImageDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView.initializeBase(this, [element]);

    this._contentUrl = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView.callBaseMethod(this, 'initialize');
               
        this._uploadTabName = "uploadVideoDesignerView";
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
    },

    _setControlData: function (controlData) {
        if (this._contentUrl) {
            controlData.MediaUrl = this._contentUrl;
        }
        if (this._contentId) {
            controlData.MediaContentId = this._contentId;
        }
    },

    _readUploadResponseData: function (response) {
        this._contentUrl = response.ContentUrl;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadVideoDesignerView', Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();