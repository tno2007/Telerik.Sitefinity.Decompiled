Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");

var siteSelectorDialog;

Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog.initializeBase(this, [element]);

    this._doneClientSelectionDelegate = null;
    this._cancelClientsSelectionDelegate = null;
    this._siteSelector = null;
    this._doneButton = null;
    this._cancelButton = null;
    this.formId = null;
    this.webServiceUrl = null;
    this.exisitngLinks = [];

    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        siteSelectorDialog = this;

        if (this._doneButton) {
            this._doneClientSelectionDelegate = Function.createDelegate(this, this._doneClientSelection);
            $addHandler(this._doneButton, "click", this._doneClientSelectionDelegate);
        }

        if (this._cancelButton) {
            this._cancelClientsSelectionDelegate = Function.createDelegate(this, this._cancelClientsSelection);
            $addHandler(this._cancelButton, "click", this._cancelClientsSelectionDelegate);
        }

        Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog.callBaseMethod(this, "initialize");

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        if (this._doneClientSelectionDelegate) {
            if (this._doneButton) {
                $removeHandler(this._doneButton, "click", this._doneClientSelectionDelegate);
            }
            delete this._allSitesDoneSelectingDelegate;
        }

        if (this._cancelClientsSelectionDelegate) {
            if (this._cancelButton) {
                $removeHandler(this._cancelButton, "click", this._cancelClientsSelectionDelegate);
            }
            delete this._cancelButton;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */
    _onLoad: function () {
        this.get_siteSelector().set_clearSelectionOnRebind(false);
        this.get_siteSelector().set_selectedKeys(this.exisitngLinks);

        jQuery("body").addClass("sfSelectorDialog");
        dialogBase.resizeToContent();
    },

    /* -------------------- event handlers ------------ */

    _doneClientSelection: function (items) {
        if (items) {
            var selectedKeys = this.get_siteSelector().get_selectedKeys();
            var deselectedKeys = this.get_siteSelector().get_deselectedKeys();
            var key = null;

            for (var i = 0; i < selectedKeys.length; i++) {
                key = selectedKeys[i];
                if (this.exisitngLinks.indexOf(key) === -1) {
                    this.exisitngLinks.push(key);
                }
            }

            for (var i = 0; i < deselectedKeys.length; i++) {
                key = deselectedKeys[i];
                var idx = this.exisitngLinks.indexOf(key);
                if (idx !== -1) {
                    this.exisitngLinks.splice(idx, 1);
                }
            }

            var data = { formId: this.formId, selectedSites: this.exisitngLinks };
            this._save(data);
        }
        else {
            this.close();
        }
    },
    _cancelClientsSelection: function () {
        this.close();
    },

    /* -------------------- private methods ----------- */

    _setTitle: function () {
        var titleElement = this._siteSelector.get_lblTitle();
        if (titleElement) {
            var itemsName = null;
            var selectedItemsCount = 0;
            var args = this.get_radWindow()._sfArgs;
            if (args) {
                var itemsList = args.get_itemsList();
                if (itemsList) {
                    selectedItemsCount = itemsList.get_selectedItems().length;
                }
            }
            var contentTypeName = this._siteSelector.get_contentTypeName();

            if (selectedItemsCount == 1) {
                itemsName = this._siteSelector.get_itemName();

            }
            else {
                itemsName = this._siteSelector.get_itemsName();
            }

            titleElement.innerHTML = String.format(this._siteSelector.get_titleText(), contentTypeName, selectedItemsCount, itemsName);
        }
    },
    _showLoadingSection: function () {
        $("#finishSection").hide();
        $("#loadingSection").show();
    },

    _hideLoadingSection: function () {
        $("#finishSection").show();
        $("#loadingSection").hide();
    },
    _save: function (data){
        var that = this;
        this._showLoadingSection();
        $.ajax({
            url: that.webServiceUrl,
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
    get_doneButton: function () {
        return this._doneButton;
    },

    set_doneButton: function (value) {
        this._doneButton = value;
    },
    get_cancelButton: function () {
        return this._cancelButton;
    },

    set_cancelButton: function (value) {
        this._cancelButton = value;
    }
};

Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);