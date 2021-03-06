Type.registerNamespace("Telerik.Sitefinity.Localization.Web");



Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog = function (element) {
    Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog.initializeBase(this, [element]);
    this._importLanguagePackButton = null;
    this._cancel = null;
    this._importWindowWrapper = null;
    this._shouldClose = false;

    this._blockUIDelegate = null;
    this._cancelImportDelegate = null;

    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog.callBaseMethod(this, 'initialize');

        

        this._blockUIDelegate = Function.createDelegate(this, this._blockUI);
        $addHandler(this._importLanguagePackButton, "click", this._blockUIDelegate, true);

        this._cancelImportDelegate = Function.createDelegate(this, this._cancelImport);
        $addHandler(this._cancel, "click", this._cancelImportDelegate, true);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);



    },
    dispose: function () {
        Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog.callBaseMethod(this, 'dispose');
        if (this._blockUIDelegate) {
            delete this._blockUIDelegate;
        }

        if (this._cancelImportDelegate) {
            delete this._cancelImportDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    /* ------------------ Events --------------*/
    _onLoad: function () {
        jQuery("body").addClass("sfSelectorDialog sfOverflowHiddenX");
        jQuery(this._importWindowWrapper).unblock();

        if (this._shouldClose) {
            dialogBase.closeAndRebind();
            return;
        }

        dialogBase.resizeToContent();
    },
    /* ------------------ Private methods --------------*/

    _blockUI: function (e) {

        jQuery(this._importWindowWrapper).block({ message: null, overlayCSS: { cursor: 'arrow'} });
        return true;
    },

    _cancelImport: function (e) {
        dialogBase.close();
        return false;
    },

    /* ------------------ Properies --------------*/
    get_importLanguagePackButton: function () {
        return this._importLanguagePackButton;
    },

    set_importLanguagePackButton: function (value) {
        this._importLanguagePackButton = value;
    }
    ,

    get_cancel: function () {
        return this._cancel;
    },

    set_cancel: function (value) {
        this._cancel = value;
    },

    get_importWindowWrapper: function () {
        return this._importWindowWrapper;
    },

    set_importWindowWrapper: function (value) {
        this._importWindowWrapper = value;
    }
}

Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog.registerClass('Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
