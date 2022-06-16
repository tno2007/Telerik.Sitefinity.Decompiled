﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog.initializeBase(this, [element]);

    this._buttonDone = null;
    this._buttonCancel = null;

    this._closeCommand = null;

    this._buttonDoneDelegate = null;
    this._buttonCancelDelegate = null;
    this._radWindowShowDelegate = null;
    this._radWindowPageLoadDelegate = null;

    this._labelWarningMessage = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog.callBaseMethod(this, 'initialize');

        this._initDelegates();
        this._addHandlers();
    },
    dispose: function () {

        this._removeHandlers();
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog.callBaseMethod(this, 'dispose');
    },

    /********** Private Methods **********/

    _initDelegates: function () {
        this._buttonDoneDelegate = Function.createDelegate(this, this._buttonDoneHandler);
        this._buttonCancelDelegate = Function.createDelegate(this, this._buttonCancelHandler);
        this._radWindowShowDelegate = Function.createDelegate(this, this._radWindowShowHandler);
        this._radWindowPageLoadDelegate = Function.createDelegate(this, this._radWindowPageLoadHandler);
    },

    _addHandlers: function () {
        $addHandler(this.get_buttonDone(), 'click', this._buttonDoneDelegate);
        $addHandler(this.get_buttonCancel(), 'click', this._buttonCancelDelegate);
        this.get_radWindow().add_show(this._radWindowShowDelegate);
        this.get_radWindow().add_pageLoad(this._radWindowPageLoadDelegate);
    },

    _removeHandlers: function () {
        if (this._buttonDoneDelegate) {
            if (this.get_buttonDone()) {
                $removeHandler(this.get_buttonDone(), 'click', this._buttonDoneDelegate);
            }
            delete this._buttonDoneDelegate;
        }

        if (this._buttonCancelDelegate) {
            if (this.get_buttonDone()) {
                $removeHandler(this.get_buttonCancel(), 'click', this._buttonCancelDelegate);
            }
            delete this._buttonCancelDelegate;
        }

        if (this._radWindowShowDelegate != null) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_show(this._radWindowShowDelegate);
            }

            delete this._radWindowShowDelegate;
        }

        if (this._radWindowPageLoadDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_pageLoad(this._radWindowPageLoadDelegate);
            }
            delete this.radWindowPageLoadDelegate;
        }
    },

    /********** Private Methods **********/

    /********** Event Handlers **********/

    _buttonDoneHandler: function () {
        var context = { Data: null, Command: this.get_closeCommand() };
        this.close(context);
    },

    _buttonCancelHandler: function () {
        var context = { Data: null, Command: "cancel" };
        this.close(context);
    },

    _radWindowShowHandler: function (sender, args) {
        jQuery("body").addClass("sfSelectorDialog");
        if (!dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }
        this.resizeToContent();
    },

    _radWindowPageLoadHandler: function (sender, args) {
        this._radWindowShowHandler(sender, args);
    },

    /********** Event Handlers **********/


    /********** Properties **********/

    get_buttonDone: function () {
        return this._buttonDone;
    },
    set_buttonDone: function (value) {
        this._buttonDone = value;
    },

    get_buttonCancel: function () {
        return this._buttonCancel;
    },
    set_buttonCancel: function (value) {
        this._buttonCancel = value;
    },

    get_closeCommand: function () {
        return this._closeCommand;
    },
    set_closeCommand: function (value) {
        this._closeCommand = value;
    },

    get_labelWarningMessage: function () {
        return this._labelWarningMessage;
    },
    set_labelWarningMessage: function (value) {
        this._labelWarningMessage = value;
    }

    /********** Properties **********/
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailPromptDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
