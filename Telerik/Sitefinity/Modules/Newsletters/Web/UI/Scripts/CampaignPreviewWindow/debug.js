Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow.initializeBase(this, [element]);

    this._backLink = null;
    this._backLinkClickDelegate = null;

    this._messageSubject = null;
    this._fromName = null;
    this._replyToEmail = null;
    this._htmlSource = null;
    this._plainTextSource = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow.callBaseMethod(this, "initialize");

        if (this.get_backLink()) {
            this._backLinkClickDelegate = Function.createDelegate(this, this._backLinkClickHandler);
            $addHandler(this.get_backLink(), "click", this._backLinkClickDelegate);
        }
    },

    dispose: function () {
        if (this._backLinkClickDelegate) {
            if (this.get_backLink()) {
                $removeHandler(this.get_backLink(), "click", this._backLinkClickDelegate);
            }
            delete this._backLinkClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow.callBaseMethod(this, "dispose");
    },

    show: function (campaign) {
        if (this.get_messageSubject()) {
            this.get_messageSubject().set_value(campaign.MessageSubject);
        }
        if (this.get_fromName()) {
            this.get_fromName().set_value(campaign.FromName);
        }
        if (this.get_replyToEmail()) {
            this.get_replyToEmail().set_value(campaign.ReplyToEmail);
        }
        if (this.get_htmlSource()) {
            this._loadHtmlSource(campaign.MessageBody.RawSourceHtml);
        }
        if (this.get_plainTextSource()) {
            this.get_plainTextSource().value = campaign.MessageBody.RawSourcePlainText;
        }

        this.get_kendoWindow().element.parent().addClass("sfMaximizedWindow");
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow.callBaseMethod(this, "show");
        this.get_kendoWindow().maximize();
    },

    _loadHtmlSource: function (source) {
        var doc = this.get_htmlSource().contentDocument;
        if (doc == undefined || doc == null)
            doc = this.get_htmlSource().contentWindow.document;
        doc.open();
        doc.write(source);
        doc.close();
    },

    _backLinkClickHandler: function () {
        this.close();
    },

    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },
    get_messageSubject: function () {
        return this._messageSubject;
    },
    set_messageSubject: function (value) {
        this._messageSubject = value;
    },
    get_fromName: function () {
        return this._fromName;
    },
    set_fromName: function (value) {
        this._fromName = value;
    },
    get_replyToEmail: function () {
        return this._replyToEmail;
    },
    set_replyToEmail: function (value) {
        this._replyToEmail = value;
    },
    get_htmlSource: function () {
        return this._htmlSource;
    },
    set_htmlSource: function (value) {
        this._htmlSource = value;
    },
    get_plainTextSource: function () {
        return this._plainTextSource;
    },
    set_plainTextSource: function (value) {
        this._plainTextSource = value;
    }
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow',
    Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);