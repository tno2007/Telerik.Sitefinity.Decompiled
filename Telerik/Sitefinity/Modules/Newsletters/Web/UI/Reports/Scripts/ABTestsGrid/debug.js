Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid.initializeBase(this, [element]);

    this._sentTestsGrid = null;
    this._sentTestsGridDataBoundDelegate = null;

    this._scheduledTestsGrid = null;
    this._scheduledTestsGridDataBoundDelegate = null;

    this._draftTestsGrid = null;
    this._draftTestsGridDataBoundDelegate = null;

    this._webServiceUrl = null;
    this._dataSource = null;
    this._clientLabelManager = null;
    this._reportUrl = null;

    this._documentReadyDelegate = null;

    this._abTestsGridViewModel = null;
    this._abTestsTabStrip = null;
    this._abTestsTabStripTabSelectedDelegate = null;

    this._cookieName;
    this._cookiePath;

    this._COOKIE_NAME = "sf-abissuesckie";
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);

        if (this.get_sentTestsGrid()) {
            this._sentTestsGridDataBoundDelegate = Function.createDelegate(this, this._sentTestsGridDataBoundHandler);
        }
        if (this.get_scheduledTestsGrid()) {
            this._scheduledTestsGridDataBoundDelegate = Function.createDelegate(this, this._scheduledTestsGridDataBoundHandler);
        }
        if (this.get_draftTestsGrid()) {
            this._draftTestsGridDataBoundDelegate = Function.createDelegate(this, this._draftTestsGridDataBoundHandler);
        }

        this._abTestsTabStripTabSelectedDelegate = Function.createDelegate(this, this._abTestsTabStripTabSelectedHandler);
        this.get_abTestsTabStrip().add_tabSelected(this._abTestsTabStripTabSelectedDelegate);

        this._abTestsGridViewModel = {
            fields: {
                Id: { type: "string" },
                Name: { type: "string" },
                SampleUsers: { type: "string" },
                Winner: { type: "string" },
                DateSent: { type: "date" },
                DateEnded: { type: "date" },
                LastModified: { type: "date" }
            }
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

        if (this._sentTestsGridDataBoundDelegate) {
            delete this._sentTestsGridDataBoundDelegate;
        }

        if (this._scheduledTestsGridDataBoundDelegate) {
            delete this._scheduledTestsGridDataBoundDelegate;
        }

        if (this._draftTestsGridDataBoundDelegate) {
            delete this._draftTestsGridDataBoundDelegate;
        }

        if (this._abTestsTabStripTabSelectedDelegate) {
            if (this.get_abTestsTabStrip()) {
                this.get_abTestsTabStrip().remove_tabSelected(this._abTestsTabStripTabSelectedDelegate);
            }
        }

        if (this._manager) {
            this._manager.dispose();
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid.callBaseMethod(this, "dispose");
    },

    _initializeSentTestsGrid: function () {
        var that = this;
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl() + encodeURIComponent("ABTestingStatus == InProgress || ABTestingStatus == Done")
            },
            schema: {
                data: "Items",
                model: this._abTestsGridViewModel
            },
            change: function (e) {
                var data = e.sender.data();
                for (var i = 0; i < data.length; i++) {
                    data[i].ReportUrl = String.format(that._reportUrl, data[i].Id);
                }
            }
        });

        jQuery(this.get_sentTestsGrid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(kendo.template($("#sentAbTestsGridRowTemplate").html()), this),
            scrollable: false,
            columns: [
                {
                    title: "&nbsp;"
                },
                {
                    field: "Name",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "ABTestNameColumn")
                },
                {
                    field: "SampleUsers",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "SampleUsers")
                },
                {
                    field: "Winner",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "Winner")
                },
                {
                    field: "DateSent",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "DateSent")
                },
                {
                    field: "DateEnded",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "DateEnded")
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                }
            ],
            dataBound: this._sentTestsGridDataBoundDelegate
        });
    },

    _initializeScheduledTestsGrid: function () {
        var that = this;
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl() + encodeURIComponent("ABTestingStatus == Scheduled")
            },
            schema: {
                data: "Items",
                model: this._abTestsGridViewModel
            }
        });

        jQuery(this.get_scheduledTestsGrid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(kendo.template($("#scheduledAbTestsGridRowTemplate").html()), this),
            scrollable: false,
            columns: [
                {
                    field: "Name",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "ABTestNameColumn")
                },
                {
                    field: "DateSent",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "SendingOn")
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                }
            ],
            dataBound: this._scheduledTestsGridDataBoundDelegate
        });
    },

    _initializeDraftTestsGrid: function () {
        var that = this;
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl() + encodeURIComponent("ABTestingStatus == Stopped")
            },
            schema: {
                data: "Items",
                model: this._abTestsGridViewModel
            }
        });

        jQuery(this.get_draftTestsGrid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(kendo.template($("#draftAbTestsGridRowTemplate").html()), this),
            scrollable: false,
            columns: [
                {
                    field: "Name",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "ABTestNameColumn")
                },
                {
                    field: "LastModified",
                    title: this.get_clientLabelManager().getLabel("NewslettersResources", "LastModified")
                },
                {
                    field: "Id",
                    title: "&nbsp;"
                }
            ],
            dataBound: this._draftTestsGridDataBoundDelegate
        });
    },

    _documentReadyHandler: function () {
        if (this.get_sentTestsGrid()) {
            this._initializeSentTestsGrid();
        }
        if (this.get_scheduledTestsGrid()) {
            this._initializeScheduledTestsGrid();
        }
        if (this.get_draftTestsGrid()) {
            this._initializeDraftTestsGrid();
        }
    },

    _sentTestsGridDataBoundHandler: function (e) {
        var data = jQuery(this.get_sentTestsGrid()).data("kendoGrid").dataSource.data();

        var newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "CampaignStateCompleted") + " (" + data.length + ")";
        this.get_abTestsTabStrip().findTabByValue("sent").set_text(newTabText);

        var rows = jQuery(this.get_element()).find(".k-master-row");
        for (var i = 0; i < rows.length; i++) {
            var clickOriginal = function (original) {
                return function () {
                    original.click();
                }
            };
            var jqTitleSpan = jQuery(rows[i]).find("td span.sfItemTitle");
            jqTitleSpan.click(clickOriginal(jQuery(rows[i]).find(".k-hierarchy-cell a")));
        }
    },

    _scheduledTestsGridDataBoundHandler: function (e) {
        var data = jQuery(this.get_scheduledTestsGrid()).data("kendoGrid").dataSource.data();

        var newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "Scheduled") + " (" + data.length + ")";
        this.get_abTestsTabStrip().findTabByValue("scheduled").set_text(newTabText);

        var that = this;
        for (var i = 0; i < data.length; i++) {
            var deleteClick = function (abTestId) {
                var anchorId = that.get_id() + "_scheduled_delete_" + abTestId;
                jQuery("#" + anchorId).click(function () {
                    if (confirm(that.get_clientLabelManager().getLabel("NewslettersResources", "AreYouSureDeleteABCampaign"))) {
                        that._manager.deleteAbTest(abTestId, function () { jQuery(that.get_scheduledTestsGrid()).data("kendoGrid").dataSource.read(); },
                            function (jqXHR, textStatus, errorThrown) { alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail); });
                    }
                });
            };
            deleteClick(data[i].Id);

            var editClick = function (abTestId) {
                var spanId = that.get_id() + "_scheduled_edit_" + abTestId;
                jQuery("#" + spanId).click(function () {
                    showAbTestDetailDialog("send", { abTestId: abTestId });
                });
            };
            editClick(data[i].Id);
        }
    },

    _draftTestsGridDataBoundHandler: function (e) {
        var data = jQuery(this.get_draftTestsGrid()).data("kendoGrid").dataSource.data();

        var newTabText;
        if (data.length != 1) {
            newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "Drafts") + " (" + data.length + ")";
        }
        else {
            newTabText = this.get_clientLabelManager().getLabel("NewslettersResources", "CampaignStateDraft") + " (1)";
        }
        this.get_abTestsTabStrip().findTabByValue("draft").set_text(newTabText);

        var that = this;
        for (var i = 0; i < data.length; i++) {
            var deleteClick = function (abTestId) {
                var anchorId = that.get_id() + "_draft_delete_" + abTestId;
                jQuery("#" + anchorId).click(function () {
                    if (confirm(that.get_clientLabelManager().getLabel("NewslettersResources", "AreYouSureDeleteABCampaign"))) {
                        that._manager.deleteAbTest(abTestId, function () { jQuery(that.get_draftTestsGrid()).data("kendoGrid").dataSource.read(); },
                            function (jqXHR, textStatus, errorThrown) { alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail); });
                    }
                });
            };
            deleteClick(data[i].Id);

            var editClick = function (abTestId) {
                var spanId = that.get_id() + "_draft_edit_" + abTestId;
                jQuery("#" + spanId).click(function () {
                    showAbTestDetailDialog("send", { abTestId: abTestId });
                });
            };
            editClick(data[i].Id);
        }
    },

    _abTestsTabStripTabSelectedHandler: function (sender, args) {
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
        var tabText = this._getCookie();
        //If text from the cookie exists
        if (tabText != "" || tabText != null) {
            //Set tabstrip selected index
            var tab = this.get_abTestsTabStrip().findTabByValue(tabText); //Get tab object
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

    get_sentTestsGrid: function () {
        return this._sentTestsGrid;
    },
    set_sentTestsGrid: function (value) {
        this._sentTestsGrid = value;
    },
    get_scheduledTestsGrid: function () {
        return this._scheduledTestsGrid;
    },
    set_scheduledTestsGrid: function (value) {
        this._scheduledTestsGrid = value;
    },
    get_draftTestsGrid: function () {
        return this._draftTestsGrid;
    },
    set_draftTestsGrid: function (value) {
        this._draftTestsGrid = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_abTestsTabStrip: function () {
        return this._abTestsTabStrip;
    },
    set_abTestsTabStrip: function (value) {
        this._abTestsTabStrip = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ABTestsGrid', Sys.UI.Control);

