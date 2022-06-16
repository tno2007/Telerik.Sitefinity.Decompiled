Type.registerNamespace("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI");

var insertTagsDialog = null;

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog = function (element) {
    Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog.initializeBase(this, [element]);
    this._dialogTitleLabel = null;
    this._commandBar = null;
    this._tagField = null;

    // supported commands
    this._insertTagsCommandName = null;
    this._cancelCommandName = null;

    this._textFieldTemplate = null;

    this._commandDelegate = null;
    this._pageLoadDelegate = null;
}

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog.callBaseMethod(this, "initialize");

        insertTagsDialog = this;

        this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        this._commandBar.add_command(this._commandDelegate);
    },

    dispose: function () {
        if (this._commandDelegate) {
            if (this._commandBar) {
                this._commandBar.remove_command(this._commandDelegate);
            }
            delete this._commandDelegate;
        }
        Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog.callBaseMethod(this, "dispose");
    },

    // ------------------------------------- Public methods -------------------------------------

    createDialog: function (commandName, dataItem, self, dialog, params, key) {
        var propertyName = dataItem.Name;
        this._dialogTitleLabel.innerHTML = propertyName;
        var tag = "";
        if (dataItem.ControlTag && dataItem.ControlTag !== "") {
            tag = dataItem.ControlTag;
        }
        else {
            tag = String.format(this._textFieldTemplate, propertyName);
        }
        this._tagField.set_value(tag);
    },

    // ------------------------------------- Event handlers -------------------------------------

    _commandHandler: function (sender, args) {
        var argument = null;
        switch (args.get_commandName()) {
            case this._insertTagsCommandName:
                argument = this._tagField.get_value();
                break;
            case this._cancelCommandName:
                break;
            default:
                break;
        }
        this.close(argument);
    },

    // ------------------------------------- Private methods -------------------------------------

    // ------------------------------------- Properties -------------------------------------

    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },

    get_tagField: function () {
        return this._tagField;
    },
    set_tagField: function (value) {
        this._tagField = value;
    },

    get_dialogTitleLabel: function () {
        return this._dialogTitleLabel;
    },
    set_dialogTitleLabel: function (value) {
        this._dialogTitleLabel = value;
    }
};

Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog.registerClass("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.InsertTagsDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);