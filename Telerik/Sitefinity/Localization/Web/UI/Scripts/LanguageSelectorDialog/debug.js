Type.registerNamespace("Telerik.Sitefinity.Localization.Web.UI");
// ------------------------------------------------------------------------
// Class LanguageSelectorDialog
// ------------------------------------------------------------------------
Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog = function (element) {
    Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog.initializeBase(this, [element]);

    this._context = null;
    this._settingsOriginalData = null;
    this._doneEditingHandler = null;
    this._windowLoaded = false;
    this._languageSelector = null;
    this._doneClientSelectionDelegate = null;

    Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog.instance = this;
}

Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog.callBaseMethod(this, "initialize");

        window.settingsDialog = this;
        this._doneClientSelectionDelegate = Function.createDelegate(this, this._doneClientSelection);
        this._languageSelector.add_doneClientSelection(this._doneClientSelectionDelegate);
    },

    dispose: function () {
        this._languageSelector.remove_doneClientSelection(this._doneClientSelectionDelegate);
        delete this._doneClientSelectionDelegate;
        Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* -------------------- Public methods ------------ */

    initDialog: function (data, doneHandler, context) {
        this._doneEditingHandler = doneHandler;
        this._originalData = data;
        this._context = context;
        this._languageSelector.set_selectedItems(data);
        this.resizeToContent();
    },

    /* -------------------- Event handlers ------------ */

    add_doneClientSelection: function (delegate) {
        this._languageSelector.add_doneClientSelection(delegate);
    },

    remove_doneClientSelection: function (delegate) {
        this._languageSelector.remove_doneClientSelection(delegate);
    },

    _doneClientSelection: function (sender, args) {
        switch (args.get_commandName()) {
            case "done":
                var items = args.get_commandArgument();
                this._doneEditingHandler(items, this._context);
                this.close();
                break;
            case "cancel":
                this.close();
                break;
            default:
                break;
        }
    },

    /* -------------------------- Private methods ----------------------------------- */



    /* -------------------------- Properties ---------------------------------------- */

    get_languageSelector: function () {
        return this._languageSelector;
    },
    set_languageSelector: function (value) {
        this._languageSelector = value;
    }
}

Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog.registerClass("Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


