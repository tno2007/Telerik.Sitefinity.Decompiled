﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid.initializeBase(this, [element]);

    this._grid = null;
    this._webServiceUrl = null;
    this._subscribersReportUrl = null;
    this._dataSource = null;
    this._clientLabelManager = null;

    this._documentReadyDelegate = null;
    this._changeDelegate = null;

    this._pixelsPerClick = 0;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid.callBaseMethod(this, "initialize");

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);
        this._changeDelegate = Function.createDelegate(this, this._changeHandler);

        jQuery(document).ready(this._documentReadyDelegate);
    },

    dispose: function () {
        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

        if (this._changeDelegate) {
            delete this._changeDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid.callBaseMethod(this, "dispose");
    },

    _documentReadyHandler: function () {
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl()
            },
            schema: {
                model: {
                    fields: {
                        Url: { type: "string" },
                        Clicks: { type: "number" }
                    }
                }
            },
            change: this._changeDelegate
        });

        jQuery(this.get_grid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(kendo.template(jQuery("#clickedLinksGridRowTemplate").html()), this),
            scrollable: false,
            columns: [{
                field: "Url",
                title: this.get_clientLabelManager().getLabel("Labels", "Url")
            },
            {
                field: "Clicks",
                title: this.get_clientLabelManager().getLabel("NewslettersResources", "UniqueClicks")
            },
            {
                title: this.get_clientLabelManager().getLabel("NewslettersResources", "ClickedBy")
            }]
        });
    },

    _changeHandler: function (e) {
        var data = e.sender.data();
        var maxClicks = 0;

        for (var i = 0; i < data.length; i++) {
            if (data[i].Clicks > maxClicks) {
                maxClicks = data[i].Clicks;
            }
        }
        this._pixelsPerClick = 200 / maxClicks;
    },

    get_grid: function () {
        return this._grid;
    },
    set_grid: function (value) {
        this._grid = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_subscribersReportUrl: function () {
        return this._subscribersReportUrl;
    },
    set_subscribersReportUrl: function (value) {
        this._subscribersReportUrl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClickedLinksGrid', Sys.UI.Control);