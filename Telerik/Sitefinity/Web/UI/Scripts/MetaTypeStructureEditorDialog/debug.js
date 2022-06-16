Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog = function (element) {
    //control IDs given from the server
    this._metaTypeEditorID = null;
    this._saveAndCloseButtonID = null;
    this._cancelButtonID = null;


    //private control references
    this._metaTypeEditor = null;
    this._saveAndCloseButton = null;
    this._cancelButton = null;

    Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog.prototype = {

    /* ****************************** set up / tear down methods ****************************** */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog.callBaseMethod(this, 'initialize');
        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        this._saveAndCloseButton = $get(this._saveAndCloseButtonID);
        this._cancelButton = $get(this._cancelButtonID);
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog.callBaseMethod(this, "dispose");
    },

    onload: function () {
        this._metaTypeEditor = $find(this._metaTypeEditorID);

        //delegates
        this._clickSaveDelegate = Function.createDelegate(this, this._saveAndClose);
        this._clickCancelDelegate = Function.createDelegate(this, this._closeWindow);

        //ui events
        $addHandler(this._saveAndCloseButton, "click", this._clickSaveDelegate);
        $addHandler(this._cancelButton, "click", this._clickCancelDelegate);
    },

    _saveAndClose: function () {
        try {
            var value = this._metaTypeEditor.get_dynamicTypeObject();
            this._closeMe(value);
        }
        catch (err) {
            //Do not close on exception (validation error on the client)
        }
    },

    _closeWindow: function () {
        this._closeMe(null);
    },

    _closeMe: function (closeArgument) {
        /// <summary>Closes the dialog, returning a value.</summary>
        /// <param name="closeArgument">The data to return at closing.</param>
        var dialog = dialogBase.get_radWindow();
        dialog.close(closeArgument);
    }

    //    // forces the designer to refresh the UI from the cotnrol Data
    //    refreshUI: function () {
    //        var controlData = this.get_controlData();
    //        this._metaTypeEditor.set_dynamicTypeObject(controlData);
    //    },

    //    // forces the designer to apply the changes on UI to the cotnrol Data
    //    applyChanges: function () {
    //        var controlData = this.get_controlData();
    //        controlData = this._metaTypeEditor.get_dynamicTypeObject();
    //    }
};

Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog.registerClass('Telerik.Sitefinity.Web.UI.MetaTypeStructureEditorDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);