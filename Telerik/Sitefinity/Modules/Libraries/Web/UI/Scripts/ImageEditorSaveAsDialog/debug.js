Type.registerNamespace("Telerik.Web.UI.ImageEditor");

(function ($, $T, $ImageEditor) {
    $(function () {
        $ImageEditor.SaveAs = function (imageEditor) {
		    $ImageEditor.SaveAs.initializeBase(this, [imageEditor]);
	    };
	    $ImageEditor.SaveAs.prototype = {
		    initialize: function() {
			    $ImageEditor.SaveAs.callBaseMethod(this, "initialize");

			    this._titleTxt = this._getControlFromParent("titleInput");
			    this._libraryDD = this._getControlFromParent("librariesDD");
			    this._saveAsButton = this._getControlFromParent("btnApply");
			    this._cancelButton = this._getControlFromParent("btnCancel");

			    this.attachEventHandlers();
		    },
		    attachEventHandlers: function() {
			    this._cancelButtonDelegate = Function.createDelegate(this, this._cancelButtonHandler);
			    this._saveAsButtonDelegate = Function.createDelegate(this, this._saveAsButtonHandler);

			    $addHandler(this._saveAsButton, "click", this._saveAsButtonDelegate);
			    $addHandler(this._cancelButton, "click", this._cancelButtonDelegate);
		    },
		    detachEventHandlers: function() {
			    $removeHandler(this._saveAsButton, "click", this._saveAsButtonDelegate);
			    delete this._saveAsButtonDelegate;
			    $removeHandler(this._cancelButton, "click", this._cancelButtonDelegate);
			    delete this._cancelButtonDelegate;
		    },
		    get_name: function() { return "SaveAs"; },
		    get_text: function() {
			    if(!this._text)
				    this._text = this._getTextFromTool();

			    return this._text;
		    },
		    get_imageTitle: function() { return $(this._titleTxt).val(); },
		    get_libraryId: function() {
			    return $(this._libraryDD).val();
		    },
		    onOpen: function() {
			    this.get_imageEditor()._isOurRequest = false;
		    },
		    onClose: function() {
			    this.get_imageEditor()._isOurRequest = false;
		    },
		    updateUI: function(){
		    },
		    dispose: function() {
			    this.detachEventHandlers();
			    $ImageEditor.SaveAs.callBaseMethod(this, "dispose");
		    },
		    /* specific event handlers */
		    _cancelButtonHandler: function() {
			    this.close();
		    },
		    _saveAsButtonHandler: function() {
			    this.get_imageEditor().saveImageOnServer(this.get_libraryId() + "/" + this.get_imageTitle());
			    this.close();
		    },
		    _getTextFromTool: function() {
			    var tools = this.get_imageEditor().get_tools();
			    var saveAsTool = tools[this.get_name()];
			    return saveAsTool.get_toolTip();
		    }
	    };
	    $ImageEditor.SaveAs.registerClass("Telerik.Web.UI.ImageEditor.SaveAs" , $ImageEditor.ToolWidget, $ImageEditor.IToolWidget);
    });
})(jQuery, Telerik.Web.UI, Telerik.Web.UI.ImageEditor);