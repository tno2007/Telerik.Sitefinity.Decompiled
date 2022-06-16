﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Components");

var lockingScreenDialog;

Telerik.Sitefinity.Web.UI.Components.LockingDialog = function (element) {
    Telerik.Sitefinity.Web.UI.Components.LockingDialog.initializeBase(this, [element]);

    this._showUnlockButton = null;
    this._viewUrl = null;
    this._closeUrl = null;
    this._unlockUrl = null;

    this._unlockButtonId = null;
    this._closeButtonId = null;
    this._viewButtonId = null;
    this._backButtonId = null;
    this._titleLabelId = null;
    this._lockedByLabelId = null;

    this._useDefaultActions = null;
    this._tryToCloseWindow = null;
    this._lnkRefreshPage = null;

    //Delegates
    this._loadDelegate = null;
    this._dialogShowDelegate = null;
    this._unlockClickedDelegate = null;
    this._closeClickedDelegate = null;
    this._backClickedDelegate = null;
    this._unlockSuccessDelegate = null;
    this._unlockFailureDelegate = null;

    this._clientManager = null;
};

Telerik.Sitefinity.Web.UI.Components.LockingDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        lockingScreenDialog = this;

        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        this._unlockClickedDelegate = Function.createDelegate(this, this._unlockButtonClickedHandler);
        jQuery(this.getUnlockButton()).bind("click", this._unlockClickedDelegate);

        this._closeClickedDelegate = Function.createDelegate(this, this._closeButtonClickedHandler);
        jQuery(this.getCloseButton()).bind("click", this._closeClickedDelegate);

        this._viewClickedDelegate = Function.createDelegate(this, this._viewButtonClickedHandler);
        jQuery(this.getViewButton()).bind("click", this._viewClickedDelegate);

        this._backClickedDelegate = Function.createDelegate(this, this._backButtonClickedHandler);
        jQuery(this.getBackButton()).bind("click", this._backClickedDelegate);

        this._refreshClickedDelegate = Function.createDelegate(this, this._refreshButtonClickedHandler);
        jQuery(this.getRefreshButton()).bind("click", this._refreshClickedDelegate);

        this._unlockSuccessDelegate = Function.createDelegate(this, this._unlockSuccess);
        this._unlockFailureDelegate = Function.createDelegate(this, this._unlockFailure);

        Telerik.Sitefinity.Web.UI.Components.LockingDialog.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._dialogShowDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_show(this._dialogShowDelegate);
            }
        }

        if (this._unlockClickedDelegate) {
            jQuery(this.getUnlockButton()).unbind("click");
            delete this._unlockClickedDelegate;
        }

        if (this._closeClickedDelegate) {
            jQuery(this.getCloseButton()).unbind("click");
            delete this._closeClickedDelegate;
        }

        if (this._viewClickedDelegate) {
            jQuery(this.getViewButton()).unbind("click");
            delete this._viewClickedDelegate;
        }

        if (this._backClickedDelegate) {
            jQuery(this.getBackButton()).unbind("click");
            delete this._backClickedDelegate;
        }

        if (this._refreshClickedDelegate) {
            jQuery(this.getRefreshButton()).unbind("click");
            delete this._refreshClickedDelegate;
        }

        if (this._unlockSuccessDelegate) {
            delete this._unlockSuccessDelegate;
        }
        if (this._unlockFailureDelegate) {
            delete this._unlockFailureDelegate;
        }

        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        Telerik.Sitefinity.Web.UI.Components.LockingDialog.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */


    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _unlockButtonClickedHandler: function () {
        this._closeForm("unlock");
        return false;
    },
    _closeButtonClickedHandler: function () {
        this._closeForm("close");
    },
    _viewButtonClickedHandler: function () {
        this._closeForm("view");
    },
    _backButtonClickedHandler: function () {
        this._closeForm("back");
    },

    _refreshButtonClickedHandler: function () {
        top.location.href = top.location.href;
    },

    /* -------------------- private methods ----------- */

    _load: function () {

    },

    _closeForm: function (action) {
        var args = { Action: action, Handled: false };
        this._raiseCommandSelected(args);

        if (args.Handled == false) {
            var canCloseWindow = true;
            if (this.get_useDefaultActions() == true) {
                switch (args.Action) {
                    case "close":
                        this._closeWindow();
                        break;
                    case "view":
                        canCloseWindow = false;
                        this._previewItem();
                        break;
                    case "unlock":
                        canCloseWindow = this._unlockItem(args);
                        break;
                    case "back":
                        this._closeWindow();
                        break;
                }
            }

            if (canCloseWindow == true) {
                this._closeDialog(args);
            }
        }
    },

    _closeDialog: function (args) {
        if (this.get_tryToCloseWindow() == true && window.frameElement) {
            dialogBase.close(args);
        }
    },

    _unlockItem: function (args) {
        if (this.get_unlockServiceUrl()) {
            var url = this.get_unlockServiceUrl();
            this.get_clientManager().InvokeGet(url, null, null, this._unlockSuccessDelegate, this._unlockFailureDelegate, this, { Args: args });

            //Prevent closing window - it will be closed after service result
            return false;
        } else {
            this._redirectToUrl(this.get_unlockUrl());
            return true;
        }
    },
    _unlockSuccess: function (caller, data, request, context) {
        this._redirectToUrl(this.get_unlockUrl());
        if (this.get_unlockUrl() == null || this.get_unlockUrl().length == 0) {
            this._closeDialog(context.Args);
        }
    },
    _unlockFailure: function (error, caller, context) {
        alert(error.Detail);
        this._closeDialog(context.Args);
    },
    _previewItem: function () {
        window.open(this.get_viewUrl());
    },
    _closeWindow: function () {
        this._redirectToUrl(this.get_closeUrl());
    },

    _redirectToUrl: function (url) {
        if (url) {
            document.location.href = url;
        }
    },

    /* ----------------------- events ----------------------- */

    add_commandSelected: function (handler) {
        this.get_events().addHandler('commandSelected', handler);
    },
    remove_commandSelected: function (handler) {
        this.get_events().removeHandler('commandSelected', handler);
    },
    _raiseCommandSelected: function (args) {
        var handler = this.get_events().getHandler('commandSelected');
        if (handler) {
            handler(this, args);
        }
    },

    /* -------------------- properties ---------------- */

    getCloseButton: function () {
        if (this._closeButton == null) {
            this._closeButton = jQuery('#' + this.get_closeButtonId());
        }
        return this._closeButton;
    },
    getViewButton: function () {
        if (this._viewButton == null) {
            this._viewButton = jQuery('#' + this.get_viewButtonId());
        }
        return this._viewButton;
    },
    getUnlockButton: function () {
        if (this._unlockButton == null) {
            this._unlockButton = jQuery('#' + this.get_unlockButtonId());
        }
        return this._unlockButton;
    },
    getBackButton: function () {
        if (this._backButton == null) {
            this._backButton = jQuery('#' + this.get_backButtonId());
        }
        return this._backButton;
    },
    getRefreshButton: function () {
        if (this._refreshButton == null) {
            this._refreshButton = jQuery('#' + this._lnkRefreshPage);
        }
        return this._refreshButton;
    },

    get_clientManager: function () {
        if (this._clientManager == null)
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        return this._clientManager;
    },

    //Whether to show the Unlock button
    get_showUnlockButton: function () {
        return this._showUnlockButton;
    },
    set_showUnlockButton: function (value) {
        this._showUnlockButton = value;
    },

    get_viewUrl: function () {
        return this._viewUrl;
    },
    set_viewUrl: function (value) {
        this._viewUrl = value;
    },
    get_closeUrl: function () {
        return this._closeUrl;
    },
    set_closeUrl: function (value) {
        this._closeUrl = value;
    },
    get_unlockUrl: function () {
        return this._unlockUrl;
    },
    set_unlockUrl: function (value) {
        this._unlockUrl = value;
    },
    get_unlockServiceUrl: function () {
        return this._unlockServiceUrl;
    },
    set_unlockServiceUrl: function (value) {
        this._unlockServiceUrl = value;
    },


    get_useDefaultActions: function () {
        return this._useDefaultActions;
    },
    set_useDefaultActions: function (value) {
        this._useDefaultActions = value;
    },
    get_tryToCloseWindow: function () {
        return this._tryToCloseWindow;
    },
    set_tryToCloseWindow: function (value) {
        this._tryToCloseWindow = value;
    },

    get_titleLabelId: function () {
        return this._titleLabelId;
    },
    set_titleLabelId: function (value) {
        this._titleLabelId = value;
    },
    get_lockedByLabelId: function () {
        return this._lockedByLabelId;
    },
    set_lockedByLabelId: function (value) {
        this._lockedByLabelId = value;
    },
    get_viewButtonId: function () {
        return this._viewButtonId;
    },
    set_viewButtonId: function (value) {
        this._viewButtonId = value;
    },
    get_closeButtonId: function () {
        return this._closeButtonId;
    },
    set_closeButtonId: function (value) {
        this._closeButtonId = value;
    },
    get_unlockButtonId: function () {
        return this._unlockButtonId;
    },
    set_unlockButtonId: function (value) {
        this._unlockButtonId = value;
    },
    get_backButtonId: function () {
        return this._backButtonId;
    },
    set_backButtonId: function (value) {
        this._backButtonId = value;
    },

    // Determines whether to show the emtpty template selection
    get_showEmptyTemplate: function () {
        return this._showEmptyTemplate;
    },
    set_showEmptyTemplate: function (value) {
        this._showEmptyTemplate = value;
        if (this.get_isInitialized()) {
            jQuery(this._emptyTemplateElement).toggle(value);
        }
    }

};

Telerik.Sitefinity.Web.UI.Components.LockingDialog.registerClass("Telerik.Sitefinity.Web.UI.Components.LockingDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);