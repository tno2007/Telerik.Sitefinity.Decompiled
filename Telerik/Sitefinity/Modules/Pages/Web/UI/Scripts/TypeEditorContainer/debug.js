Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer = function() {
    this._cancelButtonId = null;
    this._selectButtonId = null;
    Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer.initializeBase(this);
}

Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer.prototype = {

    initialize: function() {
        
        Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer.callBaseMethod(this, 'initialize');

        var cancelButton = $get(this._cancelButtonId);
        this._cancelClickDelegate = Function.createDelegate(this, this.CancelCommand);
        $addHandler(cancelButton, 'click', this._cancelClickDelegate);

        var selectButton = $get(this._selectButtonId);
        this._selectClickDelegate = Function.createDelegate(this, this.SelectCommand);
        $addHandler(selectButton, 'click', this._selectClickDelegate);
    },

    dispose: function() {
        var cancelButton = $get(this._cancelButtonId);
        $removeHandler(cancelButton, 'click', this._cancelClickDelegate);

        var selectButton = $get(this._selectButtonId);
        $removeHandler(selectButton, 'click', this._selectClickDelegate);

        Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer.callBaseMethod(this, 'dispose');
    },

    /* methods */
    CancelCommand: function() {
        alert('Cancel');
    },

    SelectCommand: function() {
        alert('Select');
    },

    /* properties */
    get_cancelButtonId: function() {
        return this._cancelButtonId;
    },
    set_cancelButtonId: function(value) {
        if (this._cancelButtonId != value) {
            this._cancelButtonId = value;
            this.raisePropertyChanged('cancelButtonId');
        }
    },
    get_selectButtonId: function() {
        return this._selectButtonId;
    },
    set_selectButtonId: function(value) {
        if (this._selectButtonId != value) {
            this._selectButtonId = value;
            this.raisePropertyChanged('selectButtonId');
        }
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.TypeEditorContainer", Sys.Component);