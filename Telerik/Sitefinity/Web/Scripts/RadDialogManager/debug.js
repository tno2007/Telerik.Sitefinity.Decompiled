Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.RadDialogManager = function (element) {
    Telerik.Sitefinity.Web.UI.RadDialogManager.initializeBase(this, [element]);

    this._selectors = {
        MAXIMIZED_WINDOW: "sfMaximizedWindow"
    };

    this._windowManager = null;

    this._handlerMappings = {};
    this._registeredEventHandlers = {};
    this._handlerArgsPropertyName = "__$handlerArgs$__";
};
Telerik.Sitefinity.Web.UI.RadDialogManager.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.RadDialogManager.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.RadDialogManager.callBaseMethod(this, "dispose");

        var self = this;

        Object.keys(self._registeredEventHandlers).forEach(function (dialog) {
            Object.keys(self._registeredEventHandlers[dialog]).forEach(function (event) {
                self._registeredEventHandlers[dialog][event].forEach(function (handler) {
                    self.removeHandler(dialog, event, handler);
                });
            });
        });
    },

    getDialog: function(dialogName) {
        var dialog = this.get_windowManager().getWindowByName(dialogName);

        if (!dialog) {
            throw new Error("The given dialog does not exist.");
        };

        return dialog;
    },

    openDialog: function (dialog, dialogArgs, handlerArgs) {
        if (typeof dialog === "string") {
            this.openDialog(this.getDialog(dialog), dialogArgs, handlerArgs);
            return;
        }

        var onPageLoad = function (sender) {
            this.removeHandler(dialog, "pageLoad", onPageLoad);
            
            dialog[this._handlerArgsPropertyName] = handlerArgs;

            var dialogFrame = sender.get_contentFrame().contentWindow;

            if (dialogFrame && dialogFrame.createDialog) {
                dialogFrame.createDialog.apply(this, dialogArgs);
            }

            //window start clearing the dom elements on close (overlay in this case).
            if (sender && sender._modalExtender && sender.get_modal()) {
                sender._modalExtender.show();
            }

        }.bind(this);

        this.addHandler(dialog, "pageLoad", onPageLoad);

        dialog.show();

        if (dialogBase && dialogBase.resizeToContent) {
            dialogBase.resizeToContent();
        }

        if (dialog.isModal()) {
            Telerik.Sitefinity.centerWindowHorizontally(dialog);
        }

        if ($(dialog.get_element()).hasClass(this._selectors.MAXIMIZED_WINDOW)) {
            dialog.maximize();
        }
    },

    addHandler: function (dialog, event, handler) {
        var self = this;

        var addEventFnName = "add_" + event;
        if (!dialog[addEventFnName]) return;

        var interceptedHandler = function (sender, args) {
            var dialogArgs = (args.get_argument && args.get_argument()) || null;
            var handlerArgs = sender[self._handlerArgsPropertyName];

            delete sender[self._handlerArgsPropertyName];

            handler(sender, dialogArgs, handlerArgs);
        };

        this._handlerMappings[handler] = interceptedHandler;

        dialog[addEventFnName](interceptedHandler);
        this._registerEventHandler(dialog, event, interceptedHandler);
    },

    removeHandler: function (dialog, event, handler) {
        var removeEventFnName = "remove_" + event;
        if (!dialog[removeEventFnName]) return;

        var interceptedHandler = this._handlerMappings[handler];
        if (!interceptedHandler) return;

        dialog[removeEventFnName](interceptedHandler);
        this._unregisterEventHandler(dialog, event, interceptedHandler);
    },

    _registerEventHandler: function (dialog, event, handler) {
        var dialogEvents = this._registeredEventHandlers[dialog.get_name()] || {};
        this._registeredEventHandlers[dialog.get_name()] = dialogEvents;

        var dialogEventHandlers = dialogEvents[event] || [];
        dialogEvents[event] = dialogEventHandlers;

        dialogEventHandlers.push(handler);
    },

    _unregisterEventHandler: function (dialog, event, handler) {
        var dialogEvents = this._registeredEventHandlers[dialog.get_name()] || {};
        this._registeredEventHandlers[dialog.get_name()] = dialogEvents;

        var dialogEventHandlers = dialogEvents[event] || [];
        dialogEvents[event] = dialogEventHandlers;

        dialogEvents[event] = dialogEventHandlers.filter(function (x) {
            x !== handler;
        });

        if (!dialogEvents[event].length) {
            delete dialogEvents[event];
        }
    },

    get_windowManager: function () {
        if (!this._windowManager) {
            this._windowManager = window.GetRadWindowManager();
        }

        return this._windowManager;
    },
    set_windowManager: function (value) {
        this._windowManager = value;
    },
};

Telerik.Sitefinity.Web.UI.RadDialogManager.registerClass("Telerik.Sitefinity.Web.UI.RadDialogManager", Sys.UI.Control);