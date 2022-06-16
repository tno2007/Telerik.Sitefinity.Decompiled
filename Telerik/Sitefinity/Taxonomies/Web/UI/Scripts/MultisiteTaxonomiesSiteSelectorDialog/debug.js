Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog = function (element) {

    this._siteSelector = null;
    this._proceedButton = null;
    this._cancelButton = null;
    this._confirmationButtonDone = null;
    this._confirmationButtonCancel = null;

    this._confirmationMessageLabel = null;

    this._onLoadDelegate = null;
    this._proceedButtonDelegate = null;
    this._cancelButtonDelegate = null;
    this._confirmationButtonDoneDelegate = null;
    this._confirmationButtonCancelDelegate = null;

    this._webServiceUrl = null;

    Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog.prototype = {
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog.callBaseMethod(this, "initialize");

        if (this._proceedButton) {
            this._proceedButtonDelegate = Function.createDelegate(this, this._onProceedButtonClick);
            $addHandler(this._proceedButton, "click", this._proceedButtonDelegate);
        }

        if (this._cancelButton) {
            this._cancelButtonDelegate = Function.createDelegate(this, this._onCancelButtonClick);
            $addHandler(this._cancelButton, "click", this._cancelButtonDelegate);
        }

        if (this._confirmationButtonDone) {
            this._confirmationButtonDoneDelegate = Function.createDelegate(this, this._onConfirmationButtonDoneClick);
            $addHandler(this._confirmationButtonDone, "click", this._confirmationButtonDoneDelegate);
        }

        if (this._confirmationButtonCancel) {
            this._confirmationButtonCancelDelegate = Function.createDelegate(this, this._onConfirmationButtonCancelClick);
            $addHandler(this._confirmationButtonCancel, "click", this._confirmationButtonCancelDelegate);
        }

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        if (this._proceedButtonDelegate) {
            if (this._proceedButton) {
                $removeHandler(this._proceedButton, "click", this._proceedButtonDelegate);
            }
            delete this._proceedButtonDelegate;
        }

        if (this._cancelButtonDelegate) {
            if (this._cancelButton) {
                $removeHandler(this._cancelButton, "click", this._cancelButtonDelegate);
            }
            delete this._cancelButtonDelegate;
        }

        if (this._confirmationButtonDoneDelegate) {
            if (this._confirmationButtonDone) {
                $removeHandler(this._confirmationButtonDone, "click", this._confirmationButtonDoneDelegate);
            }
            delete this._confirmationButtonDoneDelegate;
        }

        if (this._confirmationButtonCancelDelegate) {
            if (this._confirmationButtonCancel) {
                $removeHandler(this._confirmationButtonCancel, "click", this._confirmationButtonCancelDelegate);
            }
            delete this._confirmationButtonCancelDelegate;
        }

        Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */
    _onLoad: function () {
        dialogBase.resizeToContent();
    },

    /* -------------------- event handlers ------------ */
    _onProceedButtonClick: function (items) {
        var dataItems = this._siteSelector.getSelectedItems();

        if (dataItems.length > 0) {
            jQuery("#mainView").hide();
            jQuery("#secondaryView").show();
        }
        else {
            this.close();
        }

        dialogBase.resizeToContent();
    },

    _onCancelButtonClick: function () {
        this.close();
    },

    _onConfirmationButtonDoneClick: function () {
        var dataItems = this._siteSelector.getSelectedItems(),
            selectedSites = [];
        if (dataItems.length > 0) {
            for (var i = 0; i < dataItems.length; i++) {
                selectedSites.push(dataItems[i].Id);
            }
        }
        this._save(selectedSites);
        dialogBase.closeUpdated();
    },

    _onConfirmationButtonCancelClick: function () {
        jQuery("#secondaryView").hide();
        jQuery("#mainView").show();
        dialogBase.resizeToContent();
    },

    /* -------------------- private methods ----------- */
    _showLoadingSection: function () {
        $("#finishSection").hide();
        $("#loadingSection").show();
    },

    _hideLoadingSection: function () {
        $("#finishSection").show();
        $("#loadingSection").hide();
    },

    _save: function (data) {
        var that = this;
        this._showLoadingSection();
        $.ajax({
            url: that.get_webServiceUrl(),
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (data) {
                that._hideLoadingSection();
                that.closeAndRebind();
            }, error: function (jqXHR) {
                alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
            }
        });
    },

    /* -------------------- properties ---------------- */
    get_siteSelector: function () {
        return this._siteSelector;
    },
    set_siteSelector: function (value) {
        this._siteSelector = value;
    },
    get_proceedButton: function () {
        return this._proceedButton;
    },
    set_proceedButton: function (value) {
        this._proceedButton = value;
    },
    get_cancelButton: function () {
        return this._doneButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_confirmationButtonDone: function () {
        return this._confirmationButtonDone;
    },
    set_confirmationButtonDone: function (value) {
        this._confirmationButtonDone = value;
    },
    get_confirmationButtonCancel: function () {
        return this._confirmationButtonCancel;
    },
    set_confirmationButtonCancel: function (value) {
        this._confirmationButtonCancel = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    }
};

Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog.registerClass("Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);