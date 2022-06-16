/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
//Type._registerScript("UploadImageDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView.initializeBase(this, [element]);

	this._saveButtonText = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView.callBaseMethod(this, 'initialize');
        this._uploadTabName = "uploadImageDesignerView";
		this._fileUploadedDelegate = Function.createDelegate(this, this._fileUploadedHandler);
		this._ajaxUpload._settings.onComplete = this._fileUploadedDelegate;
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        if (this.get_parentDesigner().isViewSelected(this))
            this.notifyViewSelected();
    },

	notifyViewSelected: function ()
	{
		var parentDesigner = this.get_parentDesigner();
		parentDesigner.set_saveButtonText(this._saveButtonText);
		parentDesigner.set_saveButtonEnabled(this.isFileNameSet());
	},

    _setControlData: function (controlData) {
        if (this._contentId) {
            controlData.ImageId = this._contentId;
        }
    },

	//override of the upload handler method
	_fileUploadedHandler: function (file, response)
	{
		this._hideLoadingSign();

		var id = null;
		try
		{
			var responseMessage = eval("(" + response + ")");
			if (!responseMessage.UploadResult)
			{
				alert(responseMessage.ErrorMessage);
				jQuery(this._fileNameTextBox).val("");
			}
			else
			{
				this._contentId = responseMessage.ContentId;
				this._readUploadResponseData(responseMessage);
				this.get_parentDesigner().executeCommand({ CommandName: "UploadImage", ImageData: responseMessage.ContentItem });
			}
		}
		catch (e)
		{
			alert(response);
		}
	}

};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadImageDesignerView', Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();