Type.registerNamespace("Telerik.Sitefinity.Web.SitefinityJS.Utility");
Type.registerNamespace("Telerik.Sitefinity.Web.UI");

var _dialogManager = null;

function GetDialogManager() {
    if (!_dialogManager) {
        _dialogManager = $create(Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager);
    }
    return _dialogManager;
}

Sys.Application.add_load(function () {
    // make sure it exists
    GetDialogManager();
});

Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager = function () {
    Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.initializeBase(this);

    //public
    this._WindowTitle = null;
    this._AutoMaximize = null;
    this._window = window;

    this._registeredWindowManagers = {};
    this._blackListedManagers = {};
    this._blackListedWindows = {};
    this._windowNameGenerator = null;

    //private   
    var _onOpenAddHistoryPoint = false;
    var lastWindowOpened = null;

    this._defaultDialogSettings = { skin: "Default", showContentDuringLoad: false, resolveUrl: true, showLoadingWhileOpening: true, centerWindowHorizontally: true, maximize: null };

}

Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.prototype = {
    // --------------------------------------------------------------------------------------------
    // --- Initialize/deinitialize
    // --------------------------------------------------------------------------------------------
    //Global initialization
    initialize: function () {

        Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.callBaseMethod(this, "initialize");

        this._AutoMaximize = false;
        this._WindowTitle = document.title;
        _onOpenAddHistoryPoint = false;
        lastWindowOpened = null;
        this._skipAddingHistoryPoint = false;
        this._skipRevertHistoryOnWindowClosed = false;

        this._beforeCloseDelegate = Function.createDelegate(this, this._beforeClose);
        this._beforeShowDelegate = Function.createDelegate(this, this._beforeShow);
        this._afterShowDelegate = Function.createDelegate(this, this._afterShow);
        this._afterCloseDelegate = Function.createDelegate(this, this._afterClose);
        this._pageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        this._navigateDelegate = Function.createDelegate(this, this._navigate);

        Sys.Application.add_navigate(this._navigateDelegate);

        if (!Sys.Application._loaded)
            Sys.Application.add_load(this._pageLoadDelegate);
        else
            this._handlePageLoad();
    },

    //Disposal
    dispose: function () {
        for (var mgrId in this._registeredWindowManagers) {
            var mgr = this._registeredWindowManagers[mgrId];
            var allWindows = mgr.get_windows();
            if (allWindows != null && typeof allWindows.length == "number") {
                var curIdx = allWindows.length;
                while (curIdx--) {
                    var curWnd = allWindows[curIdx];
                    curWnd.remove_beforeShow(this._beforeShowDelegate);
                    curWnd.remove_show(this._afterShowDelegate);
                    curWnd.remove_beforeClose(this._beforeCloseDelegate);
                    curWnd.remove_close(this._afterCloseDelegate);
                }
            }
        }
        delete this._registeredWindowManagers;
        delete this._blackListedManagers;
        delete this._blackListedWindows;

        Sys.Application.remove_load(this._pageLoadDelegate);
        Sys.Application.remove_navigate(this._navigateDelegate);

        delete this._beforeCloseDelegate;
        delete this._afterCloseDelegate;
        delete this._beforeShowDelegate;
        delete this._afterShowDelegate;
        delete this._pageLoadDelegate;
        delete this._navigateDelegate;
        delete this._windowNameGenerator;

        Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.callBaseMethod(this, "dispose");
    },

    // --------------------------------------------------------------------------------------------
    // --- Public interface
    // --------------------------------------------------------------------------------------------   

    //Dialog handling methods

    getDialogById: function (dialogId, throwIfNotFound) {
        var dialogs = this._getWindowsById(dialogId);
        if (!dialogs || dialogs.length < 1) {
            if (throwIfNotFound)
                throw String.format("No dialog with id: {0} was found", dialogId);
            else
                return null;
        }
        if (dialogs.length > 1)
            throw String.format("More than one dialog with id: '{0}' was found", dialogId);

        return dialogs[0];
    },

    getDialogByName: function (dialogName, throwIfNotFound) {
        var dialogs = this._getWindowsByName(dialogName);
        if (!dialogs || dialogs.length < 1) {
            if (throwIfNotFound)
                throw String.format("No dialog with the name: {0} was found", dialogName);
            else
                return null;
        }
        if (dialogs.length > 1)
            throw String.format("More than one dialog named: '{0}' was found", dialogName);

        return dialogs[0];
    },

    getDialogsById: function (dialogId) {
        return this._getWindowsById(dialogId);
    },
    getDialogsByName: function (dialogName) {
        return this._getWindowsByName(dialogName);
    },

    openDialog: function (dialogName, sender, dialogContext, settings) {
        /// <summary>Opens a dialog and calls a <c>createDialog</c> function on it, if it is present</summary>
        /// <param name="dialogName">The name of the dialog.</param>
        /// <param name="sender">The component that invoked the dialog opening and initialization</param>
        /// <param name="dialogContext">The context that will be used to initialize the dialog</param>
        /// <param name="settings">The <c>Telerik.Sitefinity.Web.UI.DialogOpeningSettings</c> object
        /// containing the properties:
        /// skin: the skin that will be set to the dialog before its opening.
        /// showContentDuringLoad: 
        /// resolveUrl:
        /// showLoadingWhileOpening:
        /// centerWindowHorizontaly:
        /// </param>
        /// <remarks>
        /// The <c>createDialog</c> callback has the following signature:
        /// <c>createDialog(commandName, dataItem, sender, dialog, params, commandArgument)</c>
        /// </remarks>
        settings = jQuery.extend({}, this._defaultDialogSettings, settings);

        var dialog = this.getDialogByName(dialogName, true);

        this._preShowInitialization(dialog, settings, dialogContext);

        if (this._raiseDialogBeforeShow(dialog, dialogContext).get_cancel()) {
            // dialog opening was cancelled
            return;
        }
        //TODO find a better way to determine this (ask Team2).
        var isFirstLoad = (dialog.get_reloadOnShow() || !dialog._loaded);
        this._attachDialogEvents(dialog);
        dialog.show();

        this._afterShowInitialization(settings, dialog);
        this._raiseDialogAfterShow(dialog, dialogContext);

        //When the dialog is already loaded the load is raised here and not on show
        //so that the events sequence is constant - beforeShow, afterShow then load.
        if (!isFirstLoad) {
            this._raiseDialogLoaded(dialog, dialogContext, null);
        }
    },

    add_dialogBeforeShow: function (handler) {
        this.get_events().addHandler('dialogBeforeShow', handler);
    },
    remove_dialogBeforeShow: function (handler) {
        this.get_events().removeHandler('dialogBeforeShow', handler);
    },
    clear_dialogBeforeShow: function () {
        this.get_events()._removeHandlers('dialogBeforeShow');
    },

    add_dialogAfterShow: function (handler) {
        this.get_events().addHandler('dialogAfterShow', handler);
    },
    remove_dialogAfterShow: function (handler) {
        this.get_events().removeHandler('dialogAfterShow', handler);
    },
    clear_dialogAfterShow: function () {
        this.get_events()._removeHandlers('dialogAfterShow');
    },

    add_dialogLoaded: function (handler) {
        this.get_events().addHandler('dialogLoaded', handler);
    },
    remove_dialogLoaded: function (handler) {
        this.get_events().removeHandler('dialogLoaded', handler);
    },
    clear_dialogLoaded: function () {
        this.get_events()._removeHandlers('dialogLoaded');
    },

    add_dialogClosed: function (handler) {
        this.get_events().addHandler('dialogClosed', handler);
    },
    remove_dialogClosed: function (handler) {
        this.get_events().removeHandler('dialogClosed', handler);
    },
    clear_dialogClosed: function () {
        this.get_events()._removeHandlers('dialogClosed');
    },

    enableHistoryPointsForWindow: function (win) {
        if (this._isWindowEnabledForHistory(win)) {

            //1. Event handler: before a window is loaded
            win.add_beforeShow(this._beforeShowDelegate);
            //2. Event handler: after a window is loaded
            win.add_show(this._afterShowDelegate);
            //3. Event handler: before window is closed
            win.add_beforeClose(this._beforeCloseDelegate);
            win.add_close(this._afterCloseDelegate);
        }
    },

    blacklistWindowManager: function (windowManager) {
        this._blackListedManagers[windowManager.get_element().id] = windowManager;
    },

    blacklistWindow: function (radWindow) {
        this._blackListedWindows[radWindow.get_element().id] = radWindow;
    },

    // --------------------------------------------------------------------------------------------
    // --- Events
    // --------------------------------------------------------------------------------------------

    _raiseDialogBeforeShow: function (dialog, dialogContext) {
        eventArgs = this._getDialogArgs(dialog, dialogContext);
        var handler = this.get_events().getHandler("dialogBeforeShow");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _raiseDialogAfterShow: function (dialog, dialogContext) {
        eventArgs = this._getDialogArgs(dialog, dialogContext);
        var handler = this.get_events().getHandler("dialogAfterShow");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _raiseDialogLoaded: function (dialog, dialogContext, originalDialogEventArgument) {
        this._invokeDialogLoadCallback(dialog, dialogContext);
        eventArgs = this._getDialogArgs(dialog, dialogContext);
        var handler = this.get_events().getHandler("dialogLoaded");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    //TODO rebubling this event as it is most likely is not be the best idea since every other
    // event uses DialogManagerEventArgs. This is done because of the closeAndRebind argument that we still use.
    _raiseDialogClosed: function (dialog, originalDialogEventArgument) {
        eventArgs = this._getDialogArgs(dialog, dialog ? dialog._sfArgs : null, originalDialogEventArgument);
        var handler = this.get_events().getHandler("dialogClosed");
        if (handler) handler(dialog, eventArgs);
        return eventArgs;
    },

    // --------------------------------------------------------------------------------------------
    // --- Event handlers
    // --------------------------------------------------------------------------------------------

    _handlePageLoad: function () {
        var components = Sys.Application.getComponents();
        for (var i = 0, len = components.length; i < len; i++) {
            var current = components[i];
            if (Object.getTypeName(current) == "Telerik.Web.UI.RadWindowManager") {
                this._registerWindowManager(current);
            }
        }
        this._enableHistoryPointsForAllRadWindowsInternal();
    },

    //Global history navigation handler:
    _navigate: function (sender, e) {
        if (_onOpenAddHistoryPoint) {
            var val = e.get_state().event;
            var winId = e.get_state().winId;
            var autoMax = e.get_state().autoMax;
            var currentOpenedWindows = this._currentOpenWindows();
            var newWindows = this._getWindowsById(winId);
            for (var i = 0, newWindowsLen = newWindows.length; i < newWindowsLen; i++) {
                var newWindow = newWindows[i];
                if (this._isWindowEnabledForHistory(newWindow)) {
                    var alreadyOpend = false;
                    for (var p = 0; p < currentOpenedWindows.length; p++) {
                        if (this._isWindowEnabledForHistory(currentOpenedWindows[p])) {
                            if (currentOpenedWindows[p].get_name() == newWindow.get_name()) {
                                alreadyOpend = true;
                            }
                            else {
                                this._skipRevertHistoryOnWindowClosed = true;
                                currentOpenedWindows[p].close();
                            }
                        }
                    }
                    if (winId != "undefined" && !alreadyOpend) {
                        this._skipAddingHistoryPoint = true;
                        var windows = this._getWindowsById(winId);
                        for (var wI = 0; wI < windows.length; wI++) {
                            var current = windows[wI];
                            if (this._isWindowEnabledForHistory(current)) {
                                current.OpeningFromHistory = true;
                                current.show();
                                current.center();
                                current.maximize();
                            }
                        }
                    }
                }
            }
            if (newWindows.length == 0) {
                for (var p = 0; p < currentOpenedWindows.length; p++) {
                    if (this._isWindowEnabledForHistory(currentOpenedWindows[p])) {
                        this._skipRevertHistoryOnWindowClosed = true;
                        currentOpenedWindows[p].close();
                    }
                }
            }
        }
    },

    //Before a window is shown:
    _beforeShow: function (sender, args) {
        //newly opened window (going forward)
        if ((typeof (sender.OpeningFromHistory) == "undefined") || (sender.OpeningFromHistory == null) || (sender.OpeningFromHistory == false)) {
            //alert("window \"" + sender._name + "\" is being newly opened (going forward)");
			try{
            this.get_globalHistoryContextStack().push(jQuery.extend(true, {}, sender._sfArgs));
			}
			catch(e){}
        }
        //a re-open of historical window (going backwards)
        else {
            //alert("window \"" + sender._name + "\" is re-open of historical window (going backwards)");
            this.get_globalHistoryContextStack().pop();
            if (this.get_globalHistoryContextStack().length > 0)
                sender._sfArgs = jQuery.extend(true, {}, this.get_globalHistoryContextStack()[this.get_globalHistoryContextStack().length - 1]);
        }

        var mgr = sender.get_windowManager();
        var enabled = this._isManagerEnabledForHistory(mgr) && this._isWindowEnabledForHistory(sender);
        if (_onOpenAddHistoryPoint && enabled) {
            this.lastWindowOpened = mgr.getWindowById(sender.get_id());
            this._resetWindowNavigationEventHandler(this.lastWindowOpened);
        }
    },

    //After a window is shown:
    _afterShow: function (sender, args) {
        if (!this._isWindowEnabledForHistory(sender)) {
            return;
        }
        var mgr = sender.get_windowManager();
        if (_onOpenAddHistoryPoint) {
            //Set a history point (this causes a navigation implicitly).
            if (!this._skipAddingHistoryPoint) {
                var val = "showWindow";

                if (!(BrowserDetect.browser == "Chrome" && BrowserDetect.version == 50))
                    Sys.Application.addHistoryPoint({ event: val, winId: sender.get_id(), autoMax: this._AutoMaximize }, this._WindowTitle);
            }
            this._skipAddingHistoryPoint = false;
        }

        mgr.getWindowById(sender.get_id()).add_close(this._afterCloseDelegate);
    },

    //Before a window is closed- go back to the previous history point.
    //This is only if the window is closed explicitly by the user.
    //(if the user clicks the Back button, no need to navigate back, just close the window)
    _beforeClose: function (sender, eventArgs) {
        if (!this._isWindowEnabledForHistory(sender)) {
            return;
        }
        if (_onOpenAddHistoryPoint) {
            var arg = eventArgs.get_argument();
            var noHistoryClose = arg == "noHistory" || (typeof arg != "undefined" && arg != null && arg.DisableHistory == true);
            if (!this._skipRevertHistoryOnWindowClosed && !noHistoryClose) {

                if (!(BrowserDetect.browser == "Chrome" && BrowserDetect.version == 50))
                    history.back();

                this._skipRevertHistoryOnWindowClosed = false;
            }
            else {
                this._skipRevertHistoryOnWindowClosed = false;
            }
        }
    },

    _afterClose: function (sender, arg) {
        if (!this._isWindowEnabledForHistory(sender)) {
            return;
        }
        sender.OpeningFromHistory = false;
        sender.remove_close(this._afterCloseDelegate);
        if (arg.get_argument() == "ToMainScreen" || arg.get_argument() == "rebind") {
            var currentOpenedWindows = this._currentOpenWindows();
            for (var i = 0; i < currentOpenedWindows.length; i++) {
                this._skipRevertHistoryOnWindowClosed = true;
                currentOpenedWindows[i].close();
            }
            // Slavo: This is not needed as the current implementation calls history.back() when closing a window anyway
            //Sys.Application.addHistoryPoint({ event: "", winId: "", autoMax: "" }, null);
        }
    },

    _dialogLoadedHandler: function (dialog, originalDialogEventArgument) {
        dialog.remove_pageLoad(dialog._sfLoadDialogExtension);
        delete dialog._sfLoadDialogExtension;
        this._raiseDialogLoaded(dialog, dialog ? dialog._sfArgs : null, originalDialogEventArgument);
    },

    _dialogClosedHandler: function (dialog, args) {
        //        dialog.remove_close(dialog._sfCloseDialogExtension);
        //        delete dialog._sfCloseDialogExtension;
        this._raiseDialogClosed(dialog, args);
    },
    // --------------------------------------------------------------------------------------------
    // --- Private functions
    // --------------------------------------------------------------------------------------------

    _registerWindowManager: function (windowManager) {
        this._registeredWindowManagers[windowManager.get_element().id] = windowManager;
    },

    _isManagerBlacklisted: function (radWindowManager) {
        return this._blackListedManagers.hasOwnProperty(radWindowManager.get_element().id);
    },

    _isWindowBlacklisted: function (radWindow) {
        return this._blackListedWindows.hasOwnProperty(radWindow.get_element().id);
    },

    _isWindowEnabledForHistory: function (radWindow) {
        var globalHistoryEnabled = Sys.Application.get_enableHistory();
        return globalHistoryEnabled && !this._isWindowBlacklisted(radWindow) && this._isManagerEnabledForHistory(radWindow.get_windowManager());
    },

    _isManagerEnabledForHistory: function (radWindowManager) {
        var globalHistoryEnabled = Sys.Application.get_enableHistory();
        return globalHistoryEnabled && !this._isManagerBlacklisted(radWindowManager);
    },

    _enableHistoryPointsForAllRadWindowsInternal: function () {
        var historySupportedBrowser = true;
        if (historySupportedBrowser) {
            historySupportedBrowser = BrowserDetect.browser != 'Chrome';
        }
        if (historySupportedBrowser) {
            historySupportedBrowser = BrowserDetect.browser != 'Opera';
        }
        if (historySupportedBrowser) {
            historySupportedBrowser = BrowserDetect.browser != 'Safari';
        }

        for (var propName in this._registeredWindowManagers) {
            var mgr = this._registeredWindowManagers[propName];
            if (this._isManagerEnabledForHistory(mgr)) {
                _onOpenAddHistoryPoint = true;
                var allWindows = mgr.get_windows();
                for (var i = 0; i < allWindows.length; i++) {
                    var currentWindow = allWindows[i];
                    this.enableHistoryPointsForWindow(currentWindow);
                }
            }
        }
    },

    _currentOpenWindows: function () {
        var openedWindows = new Array();
        for (var propName in this._registeredWindowManagers) {
            var mgr = this._registeredWindowManagers[propName];
            var windows = mgr.get_windows();
            for (var i = 0; i < windows.length; i++) {
                if (!windows[i].isClosed()) {
                    openedWindows.push(windows[i]);
                }
            }
        }
        return openedWindows;
    },

    _getWindowsById: function (winId) {
        var windows = [];
        for (var propName in this._registeredWindowManagers) {
            var mgr = this._registeredWindowManagers[propName];
            var wnd = mgr.getWindowById(winId);
            if (wnd != null) {
                windows.push(wnd);
            }
        }
        return windows;
    },
    _getWindowsByName: function (name) {
        if (typeof this._windowNameGenerator == "function")
            name = this._windowNameGenerator(name);
        var windows = [];
        for (var propName in this._registeredWindowManagers) {
            var mgr = this._registeredWindowManagers[propName];
            var wnd = mgr.getWindowByName(name);
            if (wnd != null) {
                windows.push(wnd);
            }
        }
        return windows;
    },

    //Reassign the window closure event handler
    _resetWindowNavigationEventHandler: function (winObj) {
        if (!this._isWindowEnabledForHistory(winObj)) {
            return;
        }
        winObj.remove_beforeClose(this._beforeCloseDelegate);
        winObj.add_beforeClose(this._beforeCloseDelegate);
        winObj.remove_close(this._afterCloseDelegate);
        winObj.add_close(this._afterCloseDelegate);
    },

    _preShowInitialization: function (dialog, settings, dialogContext) {
        dialog.set_skin(settings.skin);
        dialog.set_showContentDuringLoad(settings.showContentDuringLoad);

        // have to set dialog url here, because it's too late on show!
        if (settings.resolveUrl)
            this._resolveUrl(dialog, dialogContext);

        // we extend the dialog object instead of using local variables
        // thus we are sure they are per-instance
        if (typeof (dialog._sfArgs) !== "undefined")
            delete dialog._sfArgs;
        dialog._sfArgs = dialogContext;

        // Ivan's note: RadWindow does not pass the units here, so we'll assume
        // that 100 x 100 is percents and maxize only in that case
        if (settings.showContentDuringLoad && dialog.get_width() == 100 && dialog.get_height() == 100)
            $("body").addClass("sfLoadingTransition");
    },

    _afterShowInitialization: function (settings, dialog) {
        if (settings.centerWindowHorizontally)
            Telerik.Sitefinity.centerWindowHorizontally(dialog);
        var isToMaximize = settings.maximize === true || (dialog.get_width() == 100 && dialog.get_height() == 100);
        if (isToMaximize)
            dialog.maximize();
        if (settings.showContentDuringLoad && isToMaximize)
            $("body").removeClass("sfLoadingTransition");
    },

    _resolveUrl: function (dialog, dialogContext) {
        var url = dialog.get_navigateUrl();
        if (dialogContext.dataItem) {
            url = this._replacePropertyValues(dialogContext.dataItem, url);
        }

        url = this._replaceQueryStringValues(url);
        dialog.set_navigateUrl(url);
    },

    _replacePropertyValues: function (dataItem, literal) {
        if (literal && dataItem) {
            var matches = literal.match(/{{\w+}}/g);
            if (matches) {
                var matchIndex = matches.length;
                var current = null;
                var propName = null;
                var propValue = null;
                while (matchIndex--) {
                    current = matches[matchIndex];
                    propName = current.slice(2, -2);
                    propValue = this._getPropertyValue(dataItem, propName);
                    literal = literal.replace(current, propValue);
                }
            }
        }
        return literal;
    },

    _getPropertyValue: function (dataItem, propertyName) {
        var dotIndex = propertyName.indexOf(".");
        var property = dataItem[propertyName];
        if (property && dotIndex != -1) {
            var itemName = propertyName.slice(0, dotIndex);
            var itemValueName = propertyName.substring(dotIndex);
            if (itemName) {
                var item = dataItem[itemName];
                return this._getPropertyValue(item, itemValueName);
            }
        }
        return property;
    },

    _replaceQueryStringValues: function (literal) {
        if (literal) {
            var matches = literal.match(/\[\[\w+\]\]/g);
            if (matches) {
                var matchIndex = matches.length;
                var name = "";
                var value = null;
                while (matchIndex--) {
                    current = matches[matchIndex];
                    name = current.slice(2, -2);
                    value = this._queryStringParts[name];
                    literal = literal.replace(current, value);
                }
            }
        }
        return literal;
    },

    _attachDialogEvents: function (dialog) {
        //TODO delete this.
        //        if (!dialog._sfShowDialogExtension) {
        //            dialog._sfShowDialogExtension = this._showDialogExtension;
        //            dialog.add_show(dialog._sfShowDialogExtension);
        //        }
        //check if the the dialog is set to reload on each showing.
        //If that's the case - the _loadDialogExtension handler should be reattached.
        if (!dialog._sfLoadDialogExtension || dialog.get_reloadOnShow()) {
            dialog._sfLoadDialogExtension = Function.createDelegate(this, this._dialogLoadedHandler);
            dialog.add_pageLoad(dialog._sfLoadDialogExtension);
        }
        if (!dialog._sfCloseDialogExtension) {
            dialog._sfCloseDialogExtension = Function.createDelegate(this, this._dialogClosedHandler);
            dialog.add_close(dialog._sfCloseDialogExtension);
        }
    },

    _getDialogArgs: function (dialog, dialogContext, originalDialogEventArgument) {
        return new Telerik.Sitefinity.Web.UI.DialogManagerEventArgs(dialog, dialogContext, originalDialogEventArgument);
    },

    _invokeDialogLoadCallback: function (dialog, dialogContext) {
        var frameHandle = dialog.get_contentFrame().contentWindow;
        if (frameHandle) {
            var itemsList = dialogContext.itemsList;
            var dataItem = dialogContext.dataItem;
            var commandName = dialogContext.commandName;
            var dialog = dialogContext.dialog;
            var params = dialogContext.params;
            var key = dialogContext.key;
            var commandArgument = dialogContext.commandArgument;
            //TODO this hack is no longer valid. Find a way to do this.
            //if (dialog.OpeningFromHistory && dialog.DetailFormView) {
            //    var detailFormView = dialog.DetailFormView;
            //    key = detailFormView.get_dataKey();
            //    commandName = detailFormView.get_createFormCommandName();
            //    dataItem = detailFormView.get_dataItem();
            //}
            //TODO this should be done inside the itemsList if he is the opener of the dialog.
            //if (itemsList.get_scrollOpenedDialogsToTop()) {
            //    frameHandle.scrollTo(0, 0);
            //}
            if (frameHandle.createDialog) {
                //HACK using the old createDialog method with its signiture for now.
                //TODO find a way to pass the actual sender a.k.a. the object that opened the dialog.
                frameHandle.createDialog(commandName, dataItem, itemsList, dialog, params, key, commandArgument);
            }
        }
        else {
            //TODO remove this. Only for test purpose.
            throw "dialog load event not raised properly.";
        }
    },

    // --------------------------------------------------------------------------------------------
    // --- Properties
    // --------------------------------------------------------------------------------------------

    // override
    // hack: so that Sys.Component.dispose works
    // fixes bug 78192
    get_id: function () {
        var undef;
        return undef;
    },

    //Property: this._WindowTitle
    //Get/Set this string, to change the title in the browser when the window is opened (and a histoty point is added).
    get_WindowTitle: function () {
        return this._WindowTitle;
    },

    set_WindowTitle: function (value) {
        if (this._WindowTitle != value) {
            this._WindowTitle = value;
            this.raisePropertyChanged('WindowTitle');
        }
    },

    //Property: this._AutoMaximize
    //Get/Set whether to force-maximize the window, when opened
    get_AutoMaximize: function () {
        return this._AutoMaximize;
    },

    set_AutoMaximize: function (value) {
        if (this._AutoMaximize != value) {
            this._AutoMaximize = value;
            this.raisePropertyChanged('AutoMaximize');
        }
    },

    get_globalHistoryContextStack: function () {
        if ((typeof (window.top.historyContextStack) == "undefined") || (window.top.historyContextStack == null))
            window.top.historyContextStack = new Array();

        return window.top.historyContextStack;
    },

    //Property: this._windowNameGenerator
    //Get/Set the function that is used for generating the name of the window being searched
    get_windowNameGenerator: function () {
        return this._windowNameGenerator;
    },
    set_windowNameGenerator: function (value) {
        this._windowNameGenerator = value;
    }
};
Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.registerClass('Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager', Sys.Component);

var BrowserDetect = {
    init: function () {
        this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
        this.version = this.searchVersion(navigator.userAgent)
                       || this.searchVersion(navigator.appVersion)
                       || "an unknown version";
        this.OS = this.searchString(this.dataOS) || "an unknown OS";
    },
    searchString: function (data) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            var dataProp = data[i].prop;
            this.versionSearchString = data[i].versionSearch || data[i].identity;
            if (dataString) {
                if (dataString.indexOf(data[i].subString) != -1)
                    return data[i].identity;
            }
            else if (dataProp)
                return data[i].identity;
        }
    },
    searchVersion: function (dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        if (index == -1) return;
        return parseFloat(dataString.substring(index + this.versionSearchString.length + 1));
    },
    dataBrowser: [
        {
            string: navigator.userAgent,
            subString: "Chrome",
            identity: "Chrome"
        },
        { string: navigator.userAgent,
            subString: "OmniWeb",
            versionSearch: "OmniWeb/",
            identity: "OmniWeb"
        },
        {
            string: navigator.vendor,
            subString: "Apple",
            identity: "Safari",
            versionSearch: "Version"
        },
        {
            prop: window.opera,
            identity: "Opera"
        },
        {
            string: navigator.vendor,
            subString: "iCab",
            identity: "iCab"
        },
        {
            string: navigator.vendor,
            subString: "KDE",
            identity: "Konqueror"
        },
        {
            string: navigator.userAgent,
            subString: "Firefox",
            identity: "Firefox"
        },
        {
            string: navigator.vendor,
            subString: "Camino",
            identity: "Camino"
        },
        {		// for newer Netscapes (6+)
            string: navigator.userAgent,
            subString: "Netscape",
            identity: "Netscape"
        },
        {
            string: navigator.userAgent,
            subString: "MSIE",
            identity: "Explorer",
            versionSearch: "MSIE"
        },
        {
            string: navigator.userAgent,
            subString: "Gecko",
            identity: "Mozilla",
            versionSearch: "rv"
        },
        { 		// for older Netscapes (4-)
            string: navigator.userAgent,
            subString: "Mozilla",
            identity: "Netscape",
            versionSearch: "Mozilla"
        }
    ],
    dataOS: [
        {
            string: navigator.platform,
            subString: "Win",
            identity: "Windows"
        },
        {
            string: navigator.platform,
            subString: "Mac",
            identity: "Mac"
        },
        {
            string: navigator.userAgent,
            subString: "iPhone",
            identity: "iPhone/iPod"
        },
        {
            string: navigator.platform,
            subString: "Linux",
            identity: "Linux"
        }
    ]

};

BrowserDetect.init();

// ------------------------------------------------------------------------
// Dialog Event Args
// ------------------------------------------------------------------------

Telerik.Sitefinity.Web.UI.DialogManagerEventArgs =
function (
    dialog,
    dialogContext,
    originalDialogEventArgument) {
    this._dialog = dialog;
    this._dialogContext = dialogContext;
    this._originalDialogEventArgument = originalDialogEventArgument;
    Telerik.Sitefinity.Web.UI.DialogManagerEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.DialogManagerEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.DialogManagerEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.DialogManagerEventArgs.callBaseMethod(this, 'dispose');
    },
    get_dialog: function () {
        return this._dialog;
    },
    get_dialogContext: function () {
        return this._dialogContext;
    },
    get_originalDialogEventArgument: function () {
        return this._originalDialogEventArgument;
    }
};
Telerik.Sitefinity.Web.UI.DialogManagerEventArgs.registerClass('Telerik.Sitefinity.Web.UI.DialogManagerEventArgs', Sys.CancelEventArgs);
//TODO
//Telerik.Sitefinity.Web.UI.DialogEventArgs =
//function (
//    commandName,
//    dataItem,
//    sender,
//    dialog,
//    params,
//    key,
//    commandArgument) {
//    this._commandName = commandName;
//    this._dataItem = dataItem;
//    this._sender = sender;
//    this._dialog = dialog;
//    this._params = params;
//    this._key = key;
//    this._commandArgument = commandArgument;
//    Telerik.Sitefinity.Web.UI.DialogEventArgs.initializeBase(this);
//}

//Telerik.Sitefinity.Web.UI.DialogEventArgs.prototype = {
//    // ------------------------------------------------------------------------
//    // Set-up
//    // ------------------------------------------------------------------------
//    initialize: function () {
//        Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.callBaseMethod(this, 'initialize');
//    },
//    dispose: function () {
//        Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.callBaseMethod(this, 'dispose');
//    },
//    get_commandName: function () {
//        return this._commandName;
//    },
//    get_dataItem: function () {
//        return this._dataItem;
//    },
//    get_sender: function () {
//        return this._sender;
//    },
//    get_params: function () {
//        return this._params;
//    },
//    get_key: function () {
//        return this._key;
//    },
//    get_dialog: function () {
//        return this._dialog;
//    },
//    get_commandArgument: function () {
//        return this._commandArgument;
//    }
//};
//Telerik.Sitefinity.Web.UI.DialogEventArgs.registerClass('Telerik.Sitefinity.Web.UI.DialogEventArgs', Sys.CancelEventArgs);