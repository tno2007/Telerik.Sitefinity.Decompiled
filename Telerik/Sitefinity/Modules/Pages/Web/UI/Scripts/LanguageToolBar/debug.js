Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar = function (element) {

	this._mediaType = null;
	//This is true if the page is in SPLIT mode or mode has not been selected yet.
	this._isSplitByLanguage = false;
	//This is true only if the page is in SPLIT mode
	this._isInSplitMode = false;

	this._toolbarWrapper = null;
	this._languagesList = null;
	this._windowManager = null;
	this._radWindow = null;
	this._dialogUrl = null;
	this._baseEditUrl = null;
	this._editorToolbar = null;

	//This is the ID of the PageNode/FormDescription. It is used when adding another language version for the object.
	this._objectDataId = null;

	//This is the ID of the draft object. Used to unlock a split page when adding another language version.
	this._draftId = null;

	this._canSplitPage = null;

	this._buttonsHidden = true;

	this._editorToolbar = null;
	this._stopSyncButton = null;
	this._showTranslationsButton = null;
	this._stopSyncWarningDialog = null;

	this._clientLabelManager = null;

	this._clientManager = null;

	this._handlePageLoadDelegate = null;
	this._languageCommandDelegate = null;
	this._stopSyncDelegate = null;
	this._showTranslationsDelegate = null;
	this._newLanguageDialogClosedDelegate = null;
	this._addFormLanguageSuccessDelegate = null;
	this._stopSyncSuccessDelegate = null;
	this._stopSyncFailureDelegate = null;

	this._backLabelTextForAddLanguage = null;
	this._itemLanguageUrls = null;

	Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar.initializeBase(this, [element]);
}
Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar.callBaseMethod(this, "initialize");

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);

        this._languageCommandDelegate = Function.createDelegate(this, this._onLanguageCommand);
        this.get_languagesList().add_command(this._languageCommandDelegate);

        if (this._itemLanguageUrls) {
            this._itemLanguageUrls = Sys.Serialization.JavaScriptSerializer.deserialize(this._itemLanguageUrls);
        }


        if (this.get_isSplitByLanguage() == false) {
            this._stopSyncDelegate = Function.createDelegate(this, this._onStopSync);
            $addHandler(this.get_stopSyncButton(), "click", this._stopSyncDelegate);

            this._showTranslationsDelegate = Function.createDelegate(this, this._onShowTranslations);
            $addHandler(this.get_showTranslationsButton(), "click", this._showTranslationsDelegate);

            this._stopSyncWarningDialogCommandDelegate = Function.createDelegate(this, this._stopSyncWarningDialogCommand);
            var dialog = this.get_stopSyncWarningDialog();
            dialog.add_command(this._stopSyncWarningDialogCommandDelegate);

            this._stopSyncSuccessDelegate = Function.createDelegate(this, this._stopSyncSuccess);
            this._stopSyncFailureDelegate = Function.createDelegate(this, this._stopSyncFailure);

            this._addFormLanguageSuccessDelegate = Function.createDelegate(this, this._addFormLanguageSuccess);

        }

        this._newLanguageDialogClosedDelegate = Function.createDelegate(this, this._newLanguageDialogClosed);
        var dialog = this.get_radWindow();
        dialog.add_close(this._newLanguageDialogClosedDelegate);

        jQuery("body").addClass("sfPageHasLangVersions");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar.callBaseMethod(this, "dispose");

        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }

        if (this._languageCommandDelegate) {
            this.get_languagesList().remove_command(this._languageCommandDelegate);
            delete this._languageCommandDelegate;
        }

        if (this._stopSyncDelegate) {
            delete this._languageCommandDelegate;
        }
        if (this._showTranslationsDelegate) {
            delete this._showTranslationsDelegate;
        }

        if (this._stopSyncSuccessDelegate) {
            delete this._stopSyncSuccessDelegate;
        }
        if (this._stopSyncFailureDelegate) {
            delete this._stopSyncFailureDelegate;
        }

        if (this._newLanguageDialogClosedDelegate) {
            this.get_radWindow().remove_close(this._newLanguageDialogClosedDelegate);
            delete this._newLanguageDialogClosedDelegate;
        }

    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */


    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {

    },

    //Unlocks the page
    _cancelDraftEditing: function () {
        var url = this.get_serviceUrl();
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }
        url += "Page/" + this.get_draftId();
        this.get_clientManager().InvokeDelete(url, null, null, function () { }, function () { });
    },

    _addFormLanguageSuccess: function (caller, data, request, context) {
        this._editLanguageVersion(context.Language, false);
    },

    _stopSyncSuccess: function (caller, data, request, context) {
        if (zoneEditorShared)
            zoneEditorShared.set_isPageRefreshControlled(true);
        document.location.href = document.location.href;
    },

    _stopSyncFailure: function (error, caller, context) {
        alert(error.Detail);
    },

    _onLanguageCommand: function (sender, args) {
        var commandName = args.get_commandName();
        var arg = args.get_commandArgument();
        if (commandName == "create") {
            this._addLanguageVersion(arg.language);
        } else {
            this._editLanguageVersion(arg.language, true);
        }
    },

    _newLanguageDialogClosed: function (sender, args) {
        if (sender._sfArgs.get_cancel() == true) {
            return;
        }

        if (this.get_isInSplitMode() == true) {
            this._cancelDraftEditing();
        }

        var languageCode = sender._sfArgs.get_params()["LanguageCode"];
        var dataItemUrl = null;
        if (args.get_argument) {
            var argument = args.get_argument();
            if (argument && argument.hasOwnProperty('DataItem')) {
                var dataItem = argument.DataItem;
                if (dataItem && dataItem.hasOwnProperty('NavigateUrl')) {
                    dataItemUrl = dataItem.NavigateUrl;
                }
            }
        }
        this._editLanguageVersion(languageCode, true, dataItemUrl);
    },

    _stopSyncWarningDialogCommand: function (sender, args) {
        var command = args.get_commandName();
        if (command == "ok") {
            this._stopSync();
        }
    },

    _onShowTranslations: function () {
        if (this._buttonsHidden) {
            jQuery(this.get_languagesList().get_element()).show();
            this.get_showTranslationsButton().innerHTML = this.get_clientLabelManager().getLabel("Labels", "HideOtherTranslations");
            this._buttonsHidden = false;
        } else {
            jQuery(this.get_languagesList().get_element()).hide();
            this.get_showTranslationsButton().innerHTML = this.get_clientLabelManager().getLabel("Labels", "ShowOtherTranslations");
            this._buttonsHidden = true;
        }
    },

    _onStopSync: function () {
        this.get_stopSyncWarningDialog().show_prompt();
    },


    /* -------------------- methods  ---------------- */

    _stopSync: function () {
        var url = this.get_serviceUrl();
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        url += "Page/SplitPage/" + this.get_objectDataId() + "/";
        
        var context = null;
        this.get_clientManager().InvokePut(url, null, null, null, this._stopSyncSuccessDelegate, this._stopSyncFailureDelegate, this, false, null, context);
    },

    _editLanguageVersion: function (languageCode, unlockCurrent, url) {
        jQuery("body").addClass("sfLoadingTransition");

        if (url == null || url == "") {
            if (this._itemLanguageUrls) {
                url = this._itemLanguageUrls[languageCode];
            }
            if (url == null || url == "")
                url = this.get_baseEditUrl();
        }
        url += '/' + languageCode;

        if (unlockCurrent == true && this.get_isInSplitMode()) {
            this.get_editorToolbar().unlockCurrentPage(
                function () {
                    if (zoneEditorShared)
                        zoneEditorShared.set_isPageRefreshControlled(true);
                    document.location.href = url;
                }
            );
        } else {
            if (zoneEditorShared)
                zoneEditorShared.set_isPageRefreshControlled(true);
            document.location.href = url;
        }

    },


    _addFormLanguageVersion: function (languageCode) {
        var url = this.get_serviceUrl();
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }
        url += "AddLanguage/";
        url += languageCode

        var data = this.get_objectDataId();
        var context = { Language: languageCode };
        this.get_clientManager().InvokePut(url, null, null, data, this._addFormLanguageSuccessDelegate, this._stopSyncFailureDelegate, this, false, null, context);
    },

    _addLanguageVersion: function (languageCode) {
        var type = this.get_mediaType();
        if (type == 0 || type == 1) {//page or template
            var url = this.get_dialogUrl();
            var objectDataId = this.get_objectDataId();
            //url += '&language=' + languageCode + '&sourceObjectId=' + this.get_objectDataId();

            var params = new Array();
            params["LanguageCode"] = languageCode;
            if (this._backLabelTextForAddLanguage != null) {
                params["backLabelText"] = this._backLabelTextForAddLanguage + " " + this._editorToolbar.get_pageTitle();
            }

            this._openDialog(url, this._editorToolbar.get_dataItem(), params, [this._objectDataId], { language: languageCode, languageMode: 'create', sourceObjectId: objectDataId, isSplit: this.get_isSplitByLanguage(), isFromEditor: true });

        } else if (type == 2) {//Form
            this._addFormLanguageVersion(languageCode);
        }
    },

    _openDialog: function (url, dataItem, params, key, commandArgument) {
        var dialog = this.get_radWindow();
        dialog.SetUrl(url);

        if (dialog) {
            dialog._sfArgs = new Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs(
                "create", dataItem, this, dialog, params, key, commandArgument
                );

            if (!dialog._sfShowDialogExtension) {
                dialog._sfShowDialogExtension = this._showDialogExtension;
                dialog.add_show(dialog._sfShowDialogExtension);
            }
            if (!dialog._sfLoadDialogExtension || dialog.get_reloadOnShow()) {
                dialog._sfLoadDialogExtension = this._loadDialogExtension;
                dialog.add_pageLoad(dialog._sfLoadDialogExtension);
            }
        }

        dialog.show();
        //Telerik.Sitefinity.centerWindowHorizontally(dialog);
        dialog.maximize();
    },

    _loadDialogExtension: function (sender, e) {
        var args = sender._sfArgs;
        var dialog = args.get_dialog();

        dialog.remove_pageLoad(dialog._sfLoadDialogExtension);
        dialog._sfShowDialogExtension(sender, args, true);
    },

    _showDialogExtension: function (sender, e, isLoad) {
        var args = sender._sfArgs;
        var dialog = args.get_dialog();

        var dataItem = args.get_dataItem();
        var commandName = args.get_commandName();
        var params = args.get_params();
        var key = args.get_key();
        var commandArgument = args.get_commandArgument();
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog && (!dialog.get_reloadOnShow() || isLoad)) {
                frameHandle.createDialog(commandName, dataItem, null, dialog, params, key, commandArgument);
            }
        }
    },

    get_clientManager: function () {
        if (this._clientManager == null)
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        return this._clientManager;
    },

    /* -------------------- event firing  ---------------- */




    /* -------------------- properties ---------------- */



    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        if (this._clientLabelManager != value) {
            this._clientLabelManager = value;
            this.raisePropertyChanged('clientLabelManager');
        }
    },

    get_baseItemServiceUrl: function () {
        return this._baseItemServiceUrl;
    },

    set_baseItemServiceUrl: function (value) {
        this._baseItemServiceUrl = value;
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },

    set_serviceUrl: function (value) {
        if (this._serviceUrl != value) {
            this._serviceUrl = value;
            this.raisePropertyChanged('serviceUrl');
        }
    },

    get_baseEditUrl: function () {
        return this._baseEditUrl;
    },
    set_baseEditUrl: function (value) {
        if (this._baseEditUrl != value) {
            this._baseEditUrl = value;
            this.raisePropertyChanged('baseEditUrl');
        }
    },

    get_dialogUrl: function () {
        return this._dialogUrl;
    },
    set_dialogUrl: function (value) {
        if (this._dialogUrl != value) {
            this._dialogUrl = value;
            this.raisePropertyChanged('dialogUrl');
        }
    },

    get_editorToolbar: function () {
        return this._editorToolbar;
    },
    set_editorToolbar: function (value) {
        this._editorToolbar = value;
    },

    get_stopSyncWarningDialog: function () {
        return this._stopSyncWarningDialog;
    },

    set_stopSyncWarningDialog: function (value) {
        this._stopSyncWarningDialog = value;
    },

    get_windowManager: function () {
        return this._windowManager;
    },

    set_windowManager: function (value) {
        this._windowManager = value;
    },

    get_radWindow: function () {
        return this._radWindow;
    },

    set_radWindow: function (value) {
        this._radWindow = value;
    },

    get_languagesList: function () {
        return this._languagesList;
    },

    set_languagesList: function (value) {
        this._languagesList = value;
    },

    get_stopSyncButton: function () {
        return this._stopSyncButton;
    },

    set_stopSyncButton: function (value) {
        this._stopSyncButton = value;
    },

    get_showTranslationsButton: function () {
        return this._showTranslationsButton;
    },

    set_showTranslationsButton: function (value) {
        this._showTranslationsButton = value;
    },

    get_objectDataId: function () {
        return this._objectDataId;
    },

    set_objectDataId: function (value) {
        if (this._objectDataId != value) {
            this._objectDataId = value;
            this.raisePropertyChanged('objectDataId');
        }
    },
    get_draftId: function () {
        return this._draftId;
    },

    set_draftId: function (value) {
        if (this._draftId != value) {
            this._draftId = value;
            this.raisePropertyChanged('draftId');
        }
    },

    get_canSplitPage: function () {
        return this._canSplitPage;
    },
    set_canSplitPage: function (value) {
        if (this._canSplitPage != value) {
            this._canSplitPage = value;
            this.raisePropertyChanged('canSplitPage');
        }
    },

    get_isSplitByLanguage: function () {
        return this._isSplitByLanguage;
    },
    set_isSplitByLanguage: function (value) {
        if (this._isSplitByLanguage != value) {
            this._isSplitByLanguage = value;
            this.raisePropertyChanged('isSplitByLanguage');
        }
    },
    get_isInSplitMode: function () {
        return this._isInSplitMode;
    },

    set_isInSplitMode: function (value) {
        if (this._isInSplitMode != value) {
            this._isInSplitMode = value;
            this.raisePropertyChanged('isInSplitMode');
        }
    },

    get_mediaType: function () {
        return this._mediaType;
    },

    set_mediaType: function (value) {
        if (this._mediaType != value) {
            this._mediaType = value;
            this.raisePropertyChanged('mediaType');
        }
    },

    get_toolbarWrapper: function () {
        return this._toolbarWrapper;
    },

    set_toolbarWrapper: function (value) {
        this._toolbarWrapper = value;
    },

    get_itemLanguageUrls: function () {
        return this._itemLanguageUrls;
    },

    set_itemLanguageUrls: function (value) {
        this._itemLanguageUrls = value;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar', Sys.UI.Control);