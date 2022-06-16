Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog.initializeBase(this, [element]);

    this._mirrorFieldProfileName = null;
    this._textFieldProfileTitle = null;
    this._buttonCancel = null;
    this._buttonDone = null;

    this._onDialogShowDelegate = null;
    this._onDialogLoadDelegate = null;
    this._onButtonCancelClickDelegate = null;
    this._buttonDoneClickDelegate = null;
    this._promptDialogCloseDelegate = null;
    this._promptDialogShowDelegate = null;
    this._promptDialogLoadDelegate = null;

    this._promptDialogUrl = null;
    this._fieldControlIds = null;

    this._promptDialog = null;
    this._regeneratePostBackRef = null;
    this._doneClickedPostBackRef = null;

    this._editMode = null;
    this._librariesCount = null;
    this._regenFieldIds = null;

    this._currentMethodChanged = false;
    this._originalRegenFieldValues = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog.callBaseMethod(this, 'initialize');

        this._onDialogShowDelegate = Function.createDelegate(this, this._onDialogShowHandler);
        this._onDialogLoadDelegate = Function.createDelegate(this, this._onDialogLoadHandler);
        this._onButtonCancelClickDelegate = Function.createDelegate(this, this._onButtonCancelClickHandler);
        this._buttonDoneClickDelegate = Function.createDelegate(this, this._buttonDoneClickHandler);
        this._promptDialogCloseDelegate = Function.createDelegate(this, this._promptDialogCloseHandler);
        this._promptDialogShowDelegate = Function.createDelegate(this, this._promptDialogShowHandler);
        this._promptDialogLoadDelegate = Function.createDelegate(this, this._promptDialogLoadHandler);

        this._attachHandlers();
        this.get_mirrorFieldProfileName().set_isToMirror(!this.get_editMode());
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog.callBaseMethod(this, 'dispose');

        if (this._onDialogShowDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_show(this._onDialogShowDelegate);
            }
            delete this._onDialogShowDelegate;
        }

        if (this._onDialogLoadDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_pageLoad(this._onDialogLoadDelegate);
            }
            delete this._onDialogLoadDelegate;
        }

        if (this._onButtonCancelClickDelegate) {
            if (this.get_buttonCancel()) {
                $removeHandler(this.get_buttonCancel(), 'click', this._onButtonCancelClickDelegate);
            }
            delete this._onButtonCancelClickDelegate;
        }

        if (this._buttonDoneClickDelegate) {
            if (this.get_buttonDone()) {
                $removeHandler(this.get_buttonDone(), 'click', this._buttonDoneClickDelegate);
            }
            delete this._buttonDoneClickDelegate;
        }

        if (this._promptDialogCloseDelegate) {
            if (this.get_promptDialog()) {
                this.get_promptDialog().remove_close(this._promptDialogCloseDelegate);
            }
            delete this._promptDialogCloseDelegate;
        }

        if (this._promptDialogShowDelegate) {
            if (this.get_promptDialog()) {
                this.get_promptDialog().remove_show(this._promptDialogShowDelegate);
            }
            delete this._promptDialogShowDelegate;
        }

        if (this._promptDialogLoadDelegate) {
            if (this.get_promptDialog()) {
                this.get_promptDialog().remove_pageLoad(this._promptDialogLoadDelegate);
            }
            delete this._promptDialogLoadDelegate;
        }

    },

    /* Begin Private Methods */

    _attachHandlers: function () {
        this.get_radWindow().add_show(this._onDialogShowDelegate);
        this.get_radWindow().add_pageLoad(this._onDialogLoadDelegate);
        this.get_promptDialog().add_close(this._promptDialogCloseDelegate);
        this.get_promptDialog().add_show(this._promptDialogShowDelegate);
        this.get_promptDialog().add_pageLoad(this._promptDialogLoadDelegate);
        $addHandler(this.get_buttonCancel(), 'click', this._onButtonCancelClickDelegate);
        $addHandler(this.get_buttonDone(), 'click', this._buttonDoneClickDelegate);

    },

    _validate: function () {
        var valid = true;
        for (var i = 0; i < this.get_fieldControlIds().length; i++) {
            var currentField = $find(this.get_fieldControlIds()[i]);
            valid = valid && currentField.validate();
        }
        return valid;
    },

    _haveFieldValesChanged: function () {
        var valuesChanged = false;
        for (var i = 0; i < this.get_regenFieldIds().length; i++) {
            var currentField = $find(this.get_regenFieldIds()[i]);

            var dataFieldName = currentField.get_dataFieldName();
            var currentValue = currentField.get_value();
            var originalValue = this.get_originalRegenFieldValues()[dataFieldName];
            if (originalValue.toLowerCase() != currentValue.toLowerCase()) {
                valuesChanged = true;
            }
        }
        return valuesChanged;
    },

    /* End Private Methods */

    /* Begin Event Handlers */

    _onDialogShowHandler: function (sender, args) {
        jQuery("body").addClass("sfSelectorDialog");
        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }
        this.resizeToContent();
    },

    _onDialogLoadHandler: function (sender, args) {
        this._onDialogShowHandler(sender, args);
    },

    _buttonDetailsClickHandler: function (sender, args) {
        $(this.get_detailsContainer()).toggleClass("sfDisplayNone");
    },

    _onButtonCancelClickHandler: function (sender, args) {
        this.close();
    },

    _buttonDoneClickHandler: function (sender, args) {
        if (this._validate()) {
            if (this.get_librariesCount() == parseInt("0")) {
                eval(this.get_doneClickedPostBackRef())
            }
            else {
                if (this._haveFieldValesChanged() || this.get_currentMethodChanged()) {
                    var url = this._promptDialogUrl;
                    url = url + String.format("?{0}={1}&{2}={3}&{4}={5}&{6}={7}&{8}={9}&{10}={11}&{12}={13}",
                        "messageRes", "LibrariesResources", "messageKey", "ThumbnailsForMultipleLibrariesNeedRegen", "messageArg", "images," + this.get_librariesCount(),
                        "buttonRes", "LibrariesResources", "buttonKey", "Regenerate", "buttonArg", "",
                        "closeCommand", "submit");
                    this.get_promptDialog().set_navigateUrl(url);
                    this.get_promptDialog().show();
                }
                else {
                    eval(this.get_doneClickedPostBackRef());
                }
            }
        }
        else {
            this.resizeToContent();
        }
    },

    _promptDialogCloseHandler: function (sender, args) {
        if (args.get_argument() && args.get_argument().Command == "submit") {
            eval(this.get_regeneratePostBackRef());
        }
    },

    _promptDialogShowHandler: function (sender, args) {
        if (sender.AjaxDialog) {
            sender.AjaxDialog.resizeToContent();
        }
    },

    _promptDialogLoadHandler: function (sender, args) {
        this._promptDialogShowHandler(sender, args);
    },

    /* End Event Handlers */

    /* Begin Properties */

    get_buttonDone: function () {
        return this._buttonDone;
    },
    set_buttonDone: function (value) {
        this._buttonDone = value;
    },

    get_mirrorFieldProfileName: function () {
        return this._mirrorFieldProfileName;
    },
    set_mirrorFieldProfileName: function (value) {
        this._mirrorFieldProfileName = value;
    },

    get_textFieldProfileTitle: function () {
        return this._textFieldProfileTitle;
    },
    set_textFieldProfileTitle: function (value) {
        this._textFieldProfileTitle = value;
    },

    get_buttonCancel: function () {
        return this._buttonCancel;
    },
    set_buttonCancel: function (value) {
        this._buttonCancel = value;
    },

    get_buttonDone: function () {
        return this._buttonDone;
    },
    set_buttonDone: function (value) {
        this._buttonDone = value;
    },

    get_promptDialog: function () {
        return this._promptDialog;
    },
    set_promptDialog: function (value) {
        this._promptDialog = value;
    },

    get_regeneratePostBackRef: function () {
        return this._regeneratePostBackRef;
    },
    set_regeneratePostBackRef: function (value) {
        this._regeneratePostBackRef = value;
    },

    get_doneClickedPostBackRef: function () {
        return this._doneClickedPostBackRef;
    },
    set_doneClickedPostBackRef: function (value) {
        this._doneClickedPostBackRef = value;
    },

    get_promptDialogUrl: function () {
        return this._promptDialogUrl;
    },
    set_promptDialogUrl: function (value) {
        this._promptDialogUrl = value;
    },

    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
    },

    get_editMode: function () {
        return this._editMode;
    },
    set_editMode: function (value) {
        this._editMode = value;
    },

    get_librariesCount: function () {
        return this._librariesCount;
    },
    set_librariesCount: function (value) {
        this._librariesCount = value;
    },

    get_regenFieldIds: function () {
        return this._regenFieldIds;
    },
    set_regenFieldIds: function (value) {
        this._regenFieldIds = value;
    },

    get_originalRegenFieldValues: function () {
        return this._originalRegenFieldValues;
    },
    set_originalRegenFieldValues: function (value) {
        this._originalRegenFieldValues = value;
    },

    get_currentMethodChanged: function () {
        return this._currentMethodChanged;
    },
    set_currentMethodChanged: function (value) {
        return this._currentMethodChanged = value;
    }

    /* End Properties */

}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
