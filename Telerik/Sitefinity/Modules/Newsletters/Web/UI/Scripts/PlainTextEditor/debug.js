Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

var plainTextEditor = null;

Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor.initializeBase(this, [element]);

    this._automaticPlainTextRadio = null;
    this._manualPlainTextRadio = null;
    this._plainTextVersionPanel = null;
    this._plainTextVersion = null;

    this._plainTextRadioClickDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor.callBaseMethod(this, "initialize");

        plainTextEditor = this;

        this._plainTextRadioClickDelegate = Function.createDelegate(this, this._plainTextRadioClickHandler);

        $addHandler(this.get_automaticPlainTextRadio(), "click", this._plainTextRadioClickDelegate);
        $addHandler(this.get_manualPlainTextRadio(), "click", this._plainTextRadioClickDelegate);
    },

    dispose: function () {
        if (this._plainTextRadioClickDelegate) {
            if (this.get_automaticPlainTextRadio()) {
                $removeHandler(this.get_automaticPlainTextRadio(), "click", this._plainTextRadioClickDelegate);
            }
            if (this.get_manualPlainTextRadio()) {
                $removeHandler(this.get_manualPlainTextRadio(), "click", this._plainTextRadioClickDelegate);
            }
            delete this._plainTextRadioClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor.callBaseMethod(this, "dispose");
    },

    dataBind: function (campaign) {
        if (campaign) {
            if (campaign.MessageBody.PlainTextVersion != null) {
                this.get_manualPlainTextRadio().checked = true;
                this.get_plainTextVersion().value = campaign.MessageBody.PlainTextVersion;
                jQuery(this.get_plainTextVersionPanel()).show();
            }
            else {
                jQuery(this.get_plainTextVersionPanel()).hide();
                this.get_automaticPlainTextRadio().checked = true;
            }
        }
    },

    updateCampaign: function (campaign) {
        if (this.get_automaticPlainTextRadio().checked) {
            campaign.MessageBody.PlainTextVersion = null;
        }
        else {
            campaign.MessageBody.PlainTextVersion = this.get_plainTextVersion().value;
        }
        return campaign;
    },

    _plainTextRadioClickHandler: function (sender, args) {
        if (this.get_automaticPlainTextRadio().checked) {
            jQuery(this.get_plainTextVersionPanel()).hide();
        }
        else {
            jQuery(this.get_plainTextVersionPanel()).show();
        }
    },

    get_automaticPlainTextRadio: function () {
        return this._automaticPlainTextRadio;
    },
    set_automaticPlainTextRadio: function (value) {
        this._automaticPlainTextRadio = value;
    },
    get_manualPlainTextRadio: function () {
        return this._manualPlainTextRadio;
    },
    set_manualPlainTextRadio: function (value) {
        this._manualPlainTextRadio = value;
    },
    get_plainTextVersionPanel: function () {
        return this._plainTextVersionPanel;
    },
    set_plainTextVersionPanel: function (value) {
        this._plainTextVersionPanel = value;
    },
    get_plainTextVersion: function () {
        return this._plainTextVersion;
    },
    set_plainTextVersion: function (value) {
        this._plainTextVersion = value;
    }
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.PlainTextEditor', Sys.UI.Control);