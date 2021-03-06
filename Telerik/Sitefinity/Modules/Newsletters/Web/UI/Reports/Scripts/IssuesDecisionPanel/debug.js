Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel.initializeBase(this, [element]);

    this._campaignDetailView = null;
    this._campaignDetailViewLoadedDelegate = null;
    this._campaignDetailViewShowDelegate = null;
    this._campaignDetailViewCloseDelegate = null;

    this._createIssueButton = null;
    this._openIssueDelegate = null;

    this._campaignId = null;
    this._campaignName = null;
    this._campaignMessageBodyType = null;
    this._rootUrl = null;
    this._providerName = null;
    this._manager = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this.get_providerName());

        if (this.get_campaignDetailView()) {
            this._campaignDetailViewLoadedDelegate = Function.createDelegate(this, this._campaignDetailViewLoaded);
            this._campaignDetailViewShowDelegate = Function.createDelegate(this, this._campaignDetailViewShow);
            this._campaignDetailViewCloseDelegate = Function.createDelegate(this, this._campaignDetailViewClose);

            this.get_campaignDetailView().add_pageLoad(this._campaignDetailViewLoadedDelegate);
            this.get_campaignDetailView().add_show(this._campaignDetailViewShowDelegate);
            this.get_campaignDetailView().add_close(this._campaignDetailViewCloseDelegate);
        }

        if (this.get_createIssueButton()) {
            this._openIssueDelegate = Function.createDelegate(this, this._openIssueHandler);
            $addHandler(this.get_createIssueButton(), "click", this._openIssueDelegate);
        }
    },

    dispose: function () {
        if (this._campaignDetailViewLoadedDelegate) {
            if (this.get_campaignDetailView()) {
                this.get_campaignDetailView().remove_pageLoad(this._campaignDetailViewLoadedDelegate);
            }
            delete this._campaignDetailViewLoadedDelegate;
        }

        if (this._campaignDetailViewShowDelegate) {
            if (this.get_campaignDetailView()) {
                this.get_campaignDetailView().remove_show(this._campaignDetailViewShowDelegate);
            }
            delete this._campaignDetailViewShowDelegate;
        }

        if (this._campaignDetailViewCloseDelegate) {
            if (this.get_campaignDetailView()) {
                this.get_campaignDetailView().remove_close(this._campaignDetailViewCloseDelegate);
            }
            delete this._campaignDetailViewCloseDelegate;
        }

        if (this._openIssueDelegate) {
            if (this.get_createIssueButton()) {
                $removeHandler(this.get_createIssueButton(), "click", this._openIssueDelegate);
            }
            delete this._openIssueDelegate;
        }

        this._manager.dispose();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel.callBaseMethod(this, "dispose");
    },

    _campaignDetailViewLoaded: function (sender, args) {
        this._campaignDetailViewShow(sender, args);
    },

    _campaignDetailViewShow: function (sender, args) {
        if (this.get_campaignDetailView().get_contentFrame() && this.get_campaignDetailView().get_contentFrame().contentWindow &&
            this.get_campaignDetailView().get_contentFrame().contentWindow.setForm) {
            this.get_campaignDetailView().get_contentFrame().contentWindow.setForm("overview", this.get_campaignId(), "issue", true, this.get_campaignName());
        }
    },

    _campaignDetailViewClose: function (sender, args) {
        var argument = args.get_argument();
        if (argument != null) {
            if (argument.IsCreated || argument.IsUpdated) {
                location.reload(true);
            }
        }
    },

    _openIssueHandler: function (sender, args) {
        this.get_campaignDetailView().show();
        this.get_campaignDetailView().maximize();
    },

    get_campaignDetailView: function () {
        return this._campaignDetailView;
    },
    set_campaignDetailView: function (value) {
        this._campaignDetailView = value;
    },
    get_createIssueButton: function () {
        return this._createIssueButton;
    },
    set_createIssueButton: function (value) {
        this._createIssueButton = value;
    },
    get_campaignId: function () {
        return this._campaignId;
    },
    set_campaignId: function (value) {
        this._campaignId = value;
    },
    get_campaignMessageBodyType: function () {
        return this._campaignMessageBodyType;
    },
    set_campaignMessageBodyType: function (value) {
        this._campaignMessageBodyType = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },
    get_campaignName: function () {
        return this._campaignName;
    },
    set_campaignName: function (value) {
        this._campaignName = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesDecisionPanel', Sys.UI.Control);
