Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog = function (element) {
	Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog.initializeBase(this, [element]);
	this._closeButton = null;
	this._closeDelegate = null;
	this._loadDelegate = null;
	this._txtSharedLink = "";
}

Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog.prototype = {
	/* --------------------------------- set up and tear down --------------------------------- */
	initialize: function () {
		this._closeDelegate = Function.createDelegate(this, this._closeDialogHandler);
       
		$addHandler(this._closeButton, 'click', this._closeDelegate);
      
		Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog.callBaseMethod(this, 'initialize');

		this._loadDelegate = Function.createDelegate(this, this._load);
		Sys.Application.add_load(this._loadDelegate);

		jQuery(".sfDialog").addClass("sfSelectorDialog");
		jQuery("#" + this.get_txtSharedLink()).click(this._txtSharedLinkClick);
	},

	dispose: function () {
		if (this._closeDelegate) {
			delete this._closeDelegate;
		}

		if (this._loadDelegate) {
			Sys.Application.remove_load(this._loadDelegate);
			delete this._loadDelegate;
		}
       
		Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog.callBaseMethod(this, 'dispose');
	},

	/* --------------------------------- event handlers ---------------------------------- */

	_load: function () {
		dialogBase.resizeToContent();
	},
	_closeDialogHandler: function () {
		dialogBase.close();
	},
	_txtSharedLinkClick: function () {
		this.focus();
		this.select();
	},

	/* --------------------------------- properties -------------------------------------- */
	get_closeButton: function () {
		return this._closeButton;
	},
	set_closeButton: function (value) {
		this._closeButton = value;
	},
	
	get_txtSharedLink: function () {
		return this._txtSharedLink;
	},
	set_txtSharedLink: function (value) {
		this._txtSharedLink = value;
	}
};

Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.ShareLinkDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);