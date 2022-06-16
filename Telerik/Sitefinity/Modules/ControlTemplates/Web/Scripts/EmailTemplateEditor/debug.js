Type.registerNamespace("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI");

var emailTemplateEditor = null;

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor = function (element) {
    Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
		Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.callBaseMethod(this, "initialize");

		emailTemplateEditor = this;
	},

    dispose: function () {
    	Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.callBaseMethod(this, "dispose");
	},

    // ------------------------------------- Public methods -------------------------------------

	reset: function () {
        this._otherPropertiesContainerExpanded = false;
        this._messageControl.hide();
    },

	saveChanges: function () {
		this._isDirty = true;
		this._binder.SaveChanges();
	},

	// ------------------------------------- Event handlers -------------------------------------

	// fired when page has been loaded
    _pageLoadHandler: function (sender, args) {
    	jQuery("body").addClass("sfFormDialog sfWidgetTmpDialog");

    	this._binder.set_fieldControlIds(this._fieldControlIds);
    },

	// ------------------------------------- Private methods -------------------------------------

	// called when data binding was successful
    _dataBindSuccess: function (sender, result) {
        this.Caller._binder.BindItem(result);
    }

	// ------------------------------------- Properties -------------------------------------
};

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.registerClass("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor", Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateEditor);