Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart.initializeBase(this, [element]);

    this._chart = null;
    this._webServiceUrl = null;
    this._dataSource = null;
    this._clientLabelManager = null;

    this._documentReadyDelegate = null;

    this._resizeDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart.callBaseMethod(this, "initialize");

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);

        this._resizeDelegate = Function.createDelegate(this, this._resizeHandler);

        jQuery(document).ready(this._documentReadyDelegate);
        jQuery(window).resize(this._resizeDelegate);
    },

    dispose: function () {
        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

        if (this._resizeDelegate) {
            delete this._resizeDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart.callBaseMethod(this, "dispose");
    },

    _documentReadyHandler: function () {
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl()
            },
            schema: {
                model: {
                    fields: {
                        key: { type: "string" },
                        value: { type: "number" }
                    }
                }
            }
        });

        jQuery(this.get_chart()).kendoChart({
            dataSource: this._dataSource,
            theme: "metro",
            title: {
                visible: false,
                text: this.get_clientLabelManager().getLabel("NewslettersResources", "ClicksByHour")
            },
            seriesDefaults: {
                type: "column"
            },
            series: [{
                field: "value"
            }],
            categoryAxis: {
                field: "key"
            },
            valueAxis: {
                majorUnit: 20
            },
            tooltip: {
                visible: true,
                format: '<strong>{0}</strong><span>' + 
                    this.get_clientLabelManager().getLabel("NewslettersResources", "Clicks") + 
                    '</span>',
                background: "#25a0da",
                color: "#fff",
                font: "11px Arial,Helvetica,sans-serif"
            }
        });
    },

    _resizeHandler: function () {
        var clicksByHourChart = jQuery(this.get_chart()).data("kendoChart");
        if (clicksByHourChart) {
            clicksByHourChart.redraw();
        }
    },

    get_chart: function () {
        return this._chart;
    },
    set_chart: function (value) {
        this._chart = value;
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
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.ClicksByHourChart', Sys.UI.Control);
