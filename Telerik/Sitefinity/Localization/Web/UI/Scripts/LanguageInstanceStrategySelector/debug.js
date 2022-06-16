Type.registerNamespace("Telerik.Sitefinity.Localization.Web.UI");

Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector = function (element) {
    this._wrapper = null;
    this._loading = null;
    this._copyFromOtherElement = null;
    this._startFromScratchElement = null;
    this._radWindow = null;
    this._chooseSourceDialog = null;
    this._sourceLanguageCombo = null;
    this._syncedCheckbox = null;

    this._copyOtherClickDelegate = null;
    this._startFromScratchClickDelegate = null;
    this._handlePageLoadDelegate = null;
    this._localizationModeSuccessDelegate = null;

    this._pageNodeId = null;
    this._serviceUrl = null;
    this._isInSplitMode = null;
    this._currentLanguage = null;
    this._baseEditUrl = null;

    this._clientManager = null;

    this.LM_SYNCED = "Synced";
    this.LM_SPLIT = "Split";

    Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector.initializeBase(this, [element]);
}
Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector.callBaseMethod(this, "initialize");

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);

        this._localizationModeSuccessDelegate = Function.createDelegate(this, this._localizationModeSuccess);
        this._localizationModeFailureDelegate = Function.createDelegate(this, this._localizationModeFailure);

        this._copyOtherClickDelegate = Function.createDelegate(this, this._copyFromOtherButtonClicked);
        this._startFromScratchClickDelegate = Function.createDelegate(this, this._startFromScratchButtonClicked);

        var copyOtherButton = this.get_copyFromOtherElement();
        $addHandler(copyOtherButton, "click", this._copyOtherClickDelegate);

        var editButton = this.get_startFromScratchElement();
        $addHandler(editButton, "click", this._startFromScratchClickDelegate);

        this._sourceDialogCommandDelegate = Function.createDelegate(this, this._sourceDialogCommand);
        var dialog = this.get_chooseSourceDialog();
        dialog.add_command(this._sourceDialogCommandDelegate);


    },

    dispose: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector.callBaseMethod(this, "dispose");

        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }
        if (this._editClickDelegate) {
            delete this._editClickDelegate;
        }
        if (this._addClickDelegate) {
            delete this._addClickDelegate;
        }
        if (this._sourceDialogCommandDelegate) {
            this.get_chooseSourceDialog().remove_command(this._sourceDialogCommandDelegate);
            delete this._sourceDialogCommandDelegate;
        }
    },

    /* --------------------  public methods ----------- */


    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {

    },

    _localizationModeSuccess: function (caller, data, request, context) {
        if (zoneEditorShared)
            zoneEditorShared.set_isPageRefreshControlled(true);
        var lang = context.Language;
        var url = this.get_baseEditUrl() + "/" + lang;
        location.href = url;
    },

    _localizationModeFailure: function (error, caller, context) {
        alert(error.Detail);
    },

    _sourceDialogCommand: function (sender, args) {
        var command = args.get_commandName();
        if (command == "ok") {
            if (this.get_isInSplitMode() == true) {
                var selectedLanguage = this.get_sourceLanguageCombo().get_value();
                this._copyPageNodeData(selectedLanguage);
            } else { //We need to persist localization mode here
                var choice = this.get_syncedCheckbox().get_value();
                var localizationMode = (choice == "true") ? this.LM_SYNCED : this.LM_SPLIT;
                this._persistChoiceAndReload(localizationMode, true);
            }
        } else {
            //Cancel - nothing to do here
        }
    },

    _copyFromOtherButtonClicked: function (e) {
        this.get_chooseSourceDialog().show_prompt();
    },

    _startFromScratchButtonClicked: function (e) {
        if (this.get_isInSplitMode() == true) {
            this._copyPageNodeData("none");
        } else {
            this._persistChoiceAndReload(this.LM_SPLIT, false);
        }
    },

    _persistChoiceAndReload: function (localizationMode, copyData) {
        jQuery(this.get_wrapper()).hide();
        jQuery(this.get_loading()).show();

        var url = this.get_serviceUrl();

        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        url += "Page/LocalizationStrategy/" + this.get_pageNodeId() + "/";
        url += "?strategy=" + localizationMode + "&copyData=" + (copyData ? "true" : "false");

        var context = { Language: this.get_currentLanguage() };
        this.get_clientManager().InvokePut(url, null, null, null, this._localizationModeSuccessDelegate, this._localizationModeFailureDelegate, this, false, null, context);
    },

    _copyPageNodeData: function (sourceLanguage) {
        var url = this.get_serviceUrl();

        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        url += "Page/InitializeSplitPage/" + this.get_pageNodeId() + "/";
        url += "?sourceLanguage=" + sourceLanguage;
        url += "&targetLanguage=" + this.get_currentLanguage();

        var context = { Language: this.get_currentLanguage() };
        this.get_clientManager().InvokePut(url, null, null, null, this._localizationModeSuccessDelegate, this._localizationModeFailureDelegate, this, false, null, context);
    },

    get_clientManager: function () {
        if (this._clientManager == null)
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        return this._clientManager;
    },

    /* -------------------- event firing  ---------------- */


    /* -------------------- properties ---------------- */


    get_baseEditUrl: function () {
        return this._baseEditUrl;
    },
    set_baseEditUrl: function (value) {
        this._baseEditUrl = value;
    },

    get_currentLanguage: function () {
        return this._currentLanguage;
    },

    set_currentLanguage: function (value) {
        this._currentLanguage = value;
    },

    get_isInSplitMode: function () {
        return this._isInSplitMode;
    },

    set_isInSplitMode: function (value) {
        this._isInSplitMode = value;
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },

    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_pageNodeId: function () {
        return this._pageNodeId;
    },

    set_pageNodeId: function (value) {
        this._pageNodeId = value;
    },

    get_sourceLanguageCombo: function () {
        return this._sourceLanguageCombo;
    },

    set_sourceLanguageCombo: function (value) {
        this._sourceLanguageCombo = value;
    },

    get_syncedCheckbox: function () {
        return this._syncedCheckbox;
    },

    set_syncedCheckbox: function (value) {
        this._syncedCheckbox = value;
    },

    get_chooseSourceDialog: function () {
        return this._chooseSourceDialog;
    },

    set_chooseSourceDialog: function (value) {
        this._chooseSourceDialog = value;
    },

    get_radWindow: function () {
        return this._radWindow;
    },

    set_radWindow: function (value) {
        this._radWindow = value;
    },

    get_copyFromOtherElement: function () {
        return this._copyFromOtherElement;
    },

    set_copyFromOtherElement: function (value) {
        this._copyFromOtherElement = value;
    },

    get_startFromScratchElement: function () {
        return this._startFromScratchElement;
    },

    set_startFromScratchElement: function (value) {
        this._startFromScratchElement = value;
    },

    get_wrapper: function () {
        return this._wrapper;
    },

    set_wrapper: function (value) {
        this._wrapper = value;
    },

    get_loading: function () {
        return this._loading;
    },

    set_loading: function (value) {
        this._loading = value;
    }
};

Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector.registerClass('Telerik.Sitefinity.Localization.Web.UI.LanguageInstanceStrategySelector', Sys.UI.Control);