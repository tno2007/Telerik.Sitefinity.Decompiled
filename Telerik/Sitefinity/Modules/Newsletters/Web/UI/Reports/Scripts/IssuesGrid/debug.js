Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid.initializeBase(this, [element]);

    this._documentReadyDelegate = null;
    this._detailInitDelegate = null;
    this._gridDataBoundDelegate = null;
    this._scheduledIssuesGridDataBoundDelegate = null;
    this._draftIssuesGridDataBoundDelegate = null;

    this._grid = null;
    this._scheduledIssuesGrid = null;
    this._draftIssuesGrid = null;
    this._issueGridViewModel = null;
    this._webServiceUrl = null;
    this._issueServiceUrl = null;
    this._clientLabelManager = null;
    this._campaignPreviewWindow = null;

    this._issuesTabStrip = null;
    this._issuesTabStripTabSelectedDelegate = null;

    this._campaignDetailView = null;
    this._campaignDetailViewLoadedDelegate = null;
    this._campaignDetailViewShowDelegate = null;
    this._campaignDetailViewCloseDelegate = null;

    this._issueIdToEdit = null;
    this._campaignName = null;

    this._manager = null;
    this._rootUrl = null;
    this._providerName = null;

    this._COLUMN_FULL_HEIGHT = 200;
    this._cookieName;
    this._cookiePath;

    this._COOKIE_NAME = "sf-issuesckie";
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);

        if (this.get_grid()) {
            this._detailInitDelegate = Function.createDelegate(this, this._detailInit);
            this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);
        }
        if (this.get_scheduledIssuesGrid()) {
            this._scheduledIssuesGridDataBoundDelegate = Function.createDelegate(this, this._scheduledIssuesGridDataBoundHandler);
        }
        if (this.get_draftIssuesGrid()) {
            this._draftIssuesGridDataBoundDelegate = Function.createDelegate(this, this._draftIssuesGridDataBoundHandler);
        }

        this._issuesTabStripTabSelectedDelegate = Function.createDelegate(this, this._issuesTabStripTabSelectedHandler);
        this.get_issuesTabStrip().add_tabSelected(this._issuesTabStripTabSelectedDelegate);

        this._campaignDetailViewLoadedDelegate = Function.createDelegate(this, this._campaignDetailViewLoadedHandler);
        this._campaignDetailViewShowDelegate = Function.createDelegate(this, this._campaignDetailViewShowHandler);
        this._campaignDetailViewCloseDelegate = Function.createDelegate(this, this._campaignDetailViewCloseHandler);

        this.get_campaignDetailView().add_pageLoad(this._campaignDetailViewLoadedDelegate);
        this.get_campaignDetailView().add_show(this._campaignDetailViewShowDelegate);
        this.get_campaignDetailView().add_close(this._campaignDetailViewCloseDelegate);

        this._issueGridViewModel = {
            id: "Id",
            fields: {
                Id: { type: "string" },
                RootCampaignId: { type: "string" },
                Name: { type: "string" },
                Clicked: { type: "number" },
                Delivered: { type: "number" },
                Sent: { type: "number" },
                DateSent: { type: "date" },
                LastModified: { type: "date" },
                MessageBodyType: { type: "number" },
                MessageBodyId: { type: "string" }
            },
            totalCount: "TotalCount"
        };

        this._cookieName;
        if (window.location.port != 80) {
            this._cookieName = window.location.port + this._COOKIE_NAME;
        }
        else {
            this._cookieName = this._COOKIE_NAME;
        }

        jQuery(document).ready(this._documentReadyDelegate);
        this._loadTabStrip();
    },

    dispose: function () {
        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

        if (this._detailInitDelegate) {
            delete this._detailInitDelegate;
        }

        if (this._gridDataBoundDelegate) {
            delete this._gridDataBoundDelegate;
        }

        if (this._scheduledIssuesGridDataBoundDelegate) {
            delete this._scheduledIssuesGridDataBoundDelegate;
        }

        if (this._draftIssuesGridDataBoundDelegate) {
            delete this._draftIssuesGridDataBoundDelegate;
        }

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

        if (this._issuesTabStripTabSelectedDelegate) {
            if (this.get_issuesTabStrip()) {
                this.get_issuesTabStrip().remove_tabSelected(this._issuesTabStripTabSelectedDelegate);
            }
        }

        if (this._manager) {
            this._manager.dispose();
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid.callBaseMethod(this, "dispose");
    },

    _detailInit: function (e) {
        var sentColumn = this._generateChartColumn(1, "rgb(215, 215, 215)", "");

        var deliveredValue = e.data.Sent != 0 ? e.data.Delivered / e.data.Sent : 0;
        var deliveredLabel = this._generateChartColumnLabel(deliveredValue, this.get_clientLabelManager().getLabel("NewslettersResources", "Delivered"));
        var deliveredColumn = this._generateChartColumn(deliveredValue, "rgb(215, 215, 215)", deliveredLabel);

        var openedValue = e.data.Sent != 0 ? e.data.Opened / e.data.Sent : 0;
        var openedLabel = this._generateChartColumnLabel(openedValue, this.get_clientLabelManager().getLabel("NewslettersResources", "Opened"));
        var openedColumn = this._generateChartColumn(openedValue, "rgb(101, 178, 101)", openedLabel);

        var clickedValue = e.data.Sent != 0 ? e.data.Clicked / e.data.Sent : 0;
        var clickedLabel = this._generateChartColumnLabel(clickedValue, this.get_clientLabelManager().getLabel("NewslettersResources", "Clicked"));
        var clickedColumn = this._generateChartColumn(clickedValue, "rgb(63, 178, 217)", clickedLabel);

        var detailRow = String.format("<td></td><td></td>{0}{1}{2}{3}<td></td><td></td><td></td>", sentColumn, deliveredColumn, openedColumn, clickedColumn);

        jQuery(e.detailRow).html(detailRow);
    },

    _gridDataBoundHandler: function (e) {
        var data = jQuery(this.get_grid()).data("kendoGrid").dataSource.data();

        var totalCount = jQuery(this.get_grid()).data("kendoGrid").dataSource.total();
        var newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "CampaignStateCompleted") + " (" + totalCount + ")";
        this.get_issuesTabStrip().findTabByValue("sent").set_text(newTabText);

        var that = this;
        for (var i = 0; i < data.length; i++) {
            var setClick = function (issueId) {
                var anchorId = that.get_id() + "_viewIssue_" + issueId;
                jQuery("#" + anchorId).click(function () { that._previewIssue(issueId); });
            };
            setClick(data[i].Id);
        }
        jQuery(this.get_element()).find("thead th").slice(2, 6).each(function () { jQuery(this).addClass("sfChartCountCol"); });
        var rows = jQuery(this.get_element()).find(".k-master-row");
        for (var i = 0; i < rows.length; i++) {
            var clickOriginal = function (original) {
                return function () {
                    original.click();
                }
            };
            var jqTitleSpan = jQuery(rows[i]).find("td span.sfItemTitle");
            jqTitleSpan.click(clickOriginal(jQuery(rows[i]).find(".k-hierarchy-cell a")));
            jQuery(rows[i]).find(".k-hierarchy-cell a").click(function () {
                if (jQuery(this).hasClass("k-i-collapse"))
                    jQuery(this).parents(".k-master-row").removeClass("sfRowNoBorder")
                else
                    jQuery(this).parents(".k-master-row").addClass("sfRowNoBorder")
            });
        }
    },

    _scheduledIssuesGridDataBoundHandler: function (e) {
        var data = jQuery(this.get_scheduledIssuesGrid()).data("kendoGrid").dataSource.data();

        var totalCount = jQuery(this.get_scheduledIssuesGrid()).data("kendoGrid").dataSource.total();
        var newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "Scheduled") + " (" + totalCount + ")";
        this.get_issuesTabStrip().findTabByValue("scheduled").set_text(newTabText);

        var that = this;
        for (var i = 0; i < data.length; i++) {
            var viewClick = function (issueId) {
                var anchorId = that.get_id() + "_scheduled_viewIssue_" + issueId;
                jQuery("#" + anchorId).click(function () { that._previewIssue(issueId); });
            };
            viewClick(data[i].Id);

            var editClick = function (issueId, issueType, issueBodyId) {
                var spanId = that.get_id() + "_scheduled_editIssue_" + issueId;
                jQuery("#" + spanId).click(function () {
                    //check if it is like a web page
                    if (2 == issueType) {
                        window.location = that._manager.getZoneEditorUrl(issueBodyId, issueId, window.location.href);
                    }
                    else {
                        that._issueIdToEdit = issueId;
                        that.get_campaignDetailView().show();
                        that.get_campaignDetailView().maximize();
                    }
                });
            };
            editClick(data[i].Id, data[i].MessageBodyType, data[i].MessageBodyId);

            var deleteClick = function (issueId) {
                var anchorId = that.get_id() + "_scheduled_delete_" + issueId;
                jQuery("#" + anchorId).click(function () {
                    if (confirm(that.get_clientLabelManager().getLabel("NewslettersResources", "AreYouSureDeleteIssue"))) {
                        that._manager.deleteIssue(issueId, function () { jQuery(that.get_scheduledIssuesGrid()).data("kendoGrid").dataSource.read(); },
                            function (jqXHR, textStatus, errorThrown) { alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail); });
                    }
                });
            };
            deleteClick(data[i].Id);
        }
    },

    _draftIssuesGridDataBoundHandler: function (e) {
        var data = jQuery(this.get_draftIssuesGrid()).data("kendoGrid").dataSource.data();

        var newTabText;
        if (data.length != 1) {
            var totalCount = jQuery(this.get_draftIssuesGrid()).data("kendoGrid").dataSource.total();
            newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "Drafts") + " (" + totalCount + ")";
        }
        else {
            newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "CampaignStateDraft") + " (1)";
        }
        this.get_issuesTabStrip().findTabByValue("draft").set_text(newTabText);

        var that = this;
        for (var i = 0; i < data.length; i++) {
            var setClick = function (issueId) {
                var anchorId = that.get_id() + "_draft_viewIssue_" + issueId;
                jQuery("#" + anchorId).click(function () { that._previewIssue(issueId); });
            };
            setClick(data[i].Id);

            var editClick = function (issueId, issueName, issueType, issueBodyId) {
                var spanId = that.get_id() + "_draft_editIssue_" + issueId;
                jQuery("#" + spanId).click(function () {
                    //check if it is like a web page
                    if (2 == issueType) {
                        window.location = that._manager.getZoneEditorUrl(issueBodyId, issueId, window.location.href);
                    }
                    else {
                        that._issueIdToEdit = issueId;
                        that._issueNameToEdit = issueName;
                        that.get_campaignDetailView().show();
                        that.get_campaignDetailView().maximize();
                    }
                });
            };
            editClick(data[i].Id, data[i].Name, data[i].MessageBodyType, data[i].MessageBodyId);

            var deleteClick = function (issueId) {
                var anchorId = that.get_id() + "_draft_delete_" + issueId;
                jQuery("#" + anchorId).click(function () {
                    if (confirm(that.get_clientLabelManager().getLabel("NewslettersResources", "AreYouSureDeleteIssue"))) {
                        that._manager.deleteIssue(issueId, function () { jQuery(that.get_draftIssuesGrid()).data("kendoGrid").dataSource.read(); },
                            function (jqXHR, textStatus, errorThrown) { alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail); });
                    }
                });
            };
            deleteClick(data[i].Id);
        }
    },

    _generateChartColumnLabel: function (value, title) {
        return String.format('<strong class="sfChartPercentage">{0}%</strong><span class="sfChartPercentageTitle">{1}</span>', (value * 100).toFixed(2), title);
    },

    _generateChartColumn: function (value, color, label) {
        var computedHeight = value * this._COLUMN_FULL_HEIGHT;

        return String.format('<td class="sfChartCol">{0}<div class="sfChartBar" style="height: {1}px; background-color: {2}">&nbsp;</div></td>', label, computedHeight, color);
    },

    _previewIssue: function (issueId) {
        if (issueId != "00000000-0000-0000-0000-000000000000") {
            var that = this;
            jQuery.ajax({
                type: 'GET',
                url: String.format(this.get_issueServiceUrl(), issueId),
                contentType: "application/json",
                processData: false,
                success: function (data, textStatus, jqXHR) {
                    that.get_campaignPreviewWindow().show(data);
                }
            });
        }
    },

    _initializeSentIssuesGrid: function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl() + "&filter=" + encodeURIComponent("CampaignState == Completed || CampaignState == Sending")
            },
            schema: {
                data: "Items",
                total: "TotalCount",
                model: this._issueGridViewModel
            },
            pageSize: 20,
            serverPaging: true
        });

        jQuery(this.get_grid()).kendoGrid({
            dataSource: dataSource,
            detailInit: this._detailInitDelegate,
            rowTemplate: jQuery.proxy(kendo.template($("#sentIssuesGridRowTemplate").html()), this),
            scrollable: false,
            pageable: {
                responsive: false
            },
            columns: [
                {
                    field: "Name",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "IssueName")
                },
                {
                    field: "Sent",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "SentTo")
                },
                {
                    field: "Delivered",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "Delivered")
                },
                {
                    field: "Opened",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "Opened")
                },
                {
                    field: "Clicked",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "Clicked")
                },
                {
                    field: "DateSent",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "DateSent")
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                }
            ],
            dataBound: this._gridDataBoundDelegate
        });
    },

    _initializeScheduledIssuesGrid: function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl() + "&filter=" + encodeURIComponent("CampaignState == Scheduled")
            },
            schema: {
                data: "Items",
                total: "TotalCount",
                model: this._issueGridViewModel
            },
            pageSize: 20,
            serverPaging: true
        });

        jQuery(this.get_scheduledIssuesGrid()).kendoGrid({
            dataSource: dataSource,
            rowTemplate: jQuery.proxy(kendo.template($("#scheduledIssuesGridRowTemplate").html()), this),
            scrollable: false,
            pageable: {
                responsive: false
            },
            columns: [
                {
                    field: "Name",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "IssueName")
                },
                {
                    field: "DateSent",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "SendingOn")
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                }
            ],
            dataBound: this._scheduledIssuesGridDataBoundDelegate
        });
    },

    _initializeDraftIssuesGrid: function () {
        var dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl() + "&filter=" + encodeURIComponent("CampaignState == Draft || CampaignState == PendingSending")
            },
            schema: {
                data: "Items",
                total: "TotalCount",
                model: this._issueGridViewModel
            },
            pageSize: 20,
            serverPaging: true
        });

        jQuery(this.get_draftIssuesGrid()).kendoGrid({
            dataSource: dataSource,
            rowTemplate: jQuery.proxy(kendo.template($("#draftIssuesGridRowTemplate").html()), this),
            scrollable: false,
            pageable: {
                responsive: false
            },
            columns: [
                {
                    field: "Name",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "IssueName")
                },
                {
                    field: "DateSent",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "LastModified")
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                }
            ],
            dataBound: this._draftIssuesGridDataBoundDelegate
        });
    },

    _documentReadyHandler: function () {
        if (this.get_grid()) {
            this._initializeSentIssuesGrid();
        }

        if (this.get_scheduledIssuesGrid()) {
            this._initializeScheduledIssuesGrid();
        }

        if (this.get_draftIssuesGrid()) {
            this._initializeDraftIssuesGrid();
        }
    },

    _campaignDetailViewLoadedHandler: function (sender, args) {
        this._campaignDetailViewShowHandler(sender, args);
    },

    _campaignDetailViewShowHandler: function (sender, args) {
        if (this.get_campaignDetailView().get_contentFrame() && this.get_campaignDetailView().get_contentFrame().contentWindow &&
            this.get_campaignDetailView().get_contentFrame().contentWindow.setForm) {
            this.get_campaignDetailView().get_contentFrame().contentWindow.setForm("overview", this._issueIdToEdit, "message", false, this.get_campaignName());
        }
    },

    _campaignDetailViewCloseHandler: function (sender, args) {
        var argument = args.get_argument();
        if (argument != null) {
            if (argument.IsCreated || argument.IsUpdated) {
                location.reload(true);
            }
        }
    },

    _issuesTabStripTabSelectedHandler: function (sender, args) {
        //Persist selected tab text
        var tabValue = args.get_tab().get_value();
        //Save text to a cookie using JavaScript
        this._setCookie(tabValue);
    },

    _setCookie: function (cookieValue) {
        //Call JS function to save cookie name, tab text,
        //and days before cookie should expire
        var expirationDays = 2 * 365; // 2 years
        jQuery.cookie(this._cookieName, cookieValue, { expires: expirationDays, path: this._cookiePath });
    },

    _loadTabStrip: function () {
        //Get cookie value
        var tabValue = this._getCookie();
        //If text from the cookie exists
        if (tabValue != "" || tabValue != null) {
            //Set tabstrip selected index
            var tab = this.get_issuesTabStrip().findTabByValue(tabValue); //Get tab object
            if (tab != null) {
                tab.select(); //Select tab
            }
        }
    },

    //Cookie helper function
    //Gets a cookie based on supplied name and returns value
    //as a string
    _getCookie: function () {
        try {
            return jQuery.cookie(this._cookieName);
        }
        catch (err) { }
        //If there is an error or no cookie
        //return an empty string
        return "";
    },

    get_grid: function () {
        return this._grid;
    },
    set_grid: function (value) {
        this._grid = value;
    },
    get_scheduledIssuesGrid: function () {
        return this._scheduledIssuesGrid;
    },
    set_scheduledIssuesGrid: function (value) {
        this._scheduledIssuesGrid = value;
    },
    get_draftIssuesGrid: function () {
        return this._draftIssuesGrid;
    },
    set_draftIssuesGrid: function (value) {
        this._draftIssuesGrid = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_issueServiceUrl: function () {
        return this._issueServiceUrl;
    },
    set_issueServiceUrl: function (value) {
        this._issueServiceUrl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_campaignPreviewWindow: function () {
        return this._campaignPreviewWindow;
    },
    set_campaignPreviewWindow: function (value) {
        this._campaignPreviewWindow = value;
    },
    get_issuesTabStrip: function () {
        return this._issuesTabStrip;
    },
    set_issuesTabStrip: function (value) {
        this._issuesTabStrip = value;
    },
    get_campaignDetailView: function () {
        return this._campaignDetailView;
    },
    set_campaignDetailView: function (value) {
        this._campaignDetailView = value;
    },
    get_campaignName: function () {
        return this._campaignName;
    },
    set_campaignName: function (value) {
        this._campaignName = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.IssuesGrid', Sys.UI.Control);
