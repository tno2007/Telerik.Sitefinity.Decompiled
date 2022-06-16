Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart.initializeBase(this, [element]);

    this._chart = null;
    this._webServiceUrl = null;
    this._clientLabelManager = null;

    this._offsetXDiff = 110;
    this._offsetX = 0;

    this._documentReadyDelegate = null;
    this._changeDelegate = null;
    this._axisLabelClickDelegate = null;
    this._resizeDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart.callBaseMethod(this, "initialize");

        this._offsetX = jQuery(this.get_chart()).width() / 2 - this._offsetXDiff;

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);
        this._changeDelegate = Function.createDelegate(this, this._changeHandler);
        this._axisLabelClickDelegate = Function.createDelegate(this, this._axisLabelClickHandler);
        this._resizeDelegate = Function.createDelegate(this, this._resizeHandler);

        jQuery(document).ready(this._documentReadyDelegate);
        jQuery(window).resize(this._resizeDelegate);
    },

    dispose: function () {
        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

        if (this._changeDelegate) {
            delete this._changeDelegate;
        }

        if (this._axisLabelClickDelegate) {
            delete this._axisLabelClickDelegate;
        }

        if (this._resizeDelegate) {
            delete this._resizeDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart.callBaseMethod(this, "dispose");
    },

    _documentReadyHandler: function () {
        var that = this;
        setTimeout(function () {
            that._createAreaChart();
            // Initialize the chart with a delay to make sure
            // the initial animation is visible
        }, 100);
    },

    _resizeHandler: function () {
        var campaignChart = jQuery(this.get_chart()).data("kendoChart");
        if (campaignChart) {
            campaignChart.refresh();
            this._offsetX = jQuery(this.get_chart()).width() / 2 - this._offsetXDiff;
            campaignChart.options.legend.offsetX = this._offsetX;
        }
    },

    _createAreaChart: function () {
        var title = this.get_clientLabelManager().getLabel('NewslettersResources', 'CampaignOverviewReportTitle');
        var deliveredLabel = this.get_clientLabelManager().getLabel('NewslettersResources', 'Delivered');
        var openedLabel = this.get_clientLabelManager().getLabel('NewslettersResources', 'Opened');
        var clickedLabel = this.get_clientLabelManager().getLabel('NewslettersResources', 'Clicked');

        var defaultSeriesColors = ["#d6d6d6", "#98de76", "#50a3fe"];

        jQuery(this.get_chart()).kendoChart({
            seriesColors: defaultSeriesColors,
            dataSource: {
                transport: {
                    read: {
                        url: this.get_webServiceUrl(),
                        dataType: "json"
                    }
                },
                schema: {
                    data: "Items"
                },
                change: this._changeDelegate
            },
            title: {
                text: title,
                align: "left",
                color: "#000000",
                font: "18px Arial,Helvetica,sans-serif"
            },
            legend: {
                position: "top",
                offsetX: this._offsetX,
                offsetY: 0,
                markers: { visible: true, type: 'circle' }
            },
            chartArea: {
                background: ""
            },
            seriesDefaults: {
                type: "area",
                markers: { visible: true, size: 10 },
                opacity: 0.7
            },
            tooltip: {
                visible: true
            },
            series: [{
                    field: "DeliveredPercentage",
                    name: deliveredLabel,
                    tooltip:
                    {
                        template: "<strong>${value}%</strong> #= dataItem.Delivered # ${series.name}",
                        background: defaultSeriesColors[0],
                        color: "#fff",
                        font: "11px Arial,Helvetica,sans-serif",
                        opacity: 1
                    }
                },
                {
                    field: "OpenedPercentage",
                    name: openedLabel,
                    tooltip:
                    {
                        template: "<strong>${value}%</strong> #= dataItem.Opened # ${series.name}",
                        background: defaultSeriesColors[1],
                        color: "#fff",
                        font: "11px Arial,Helvetica,sans-serif",
                        opacity: 1
                    }
                },
                {
                    field: "ClickedPercentage",
                    name: clickedLabel,
                    tooltip:
                        {
                            template: "<strong>${value}%</strong> #= dataItem.Clicked # ${series.name}",
                            background: defaultSeriesColors[2],
                            color: "#fff",
                            font: "11px Arial,Helvetica,sans-serif",
                            opacity: 1
                        }
                }],
            categoryAxis: {
                field: "CategoryAxisName"
            },
            valueAxis: {
                labels: {
                    format: "{0}%"
                },
                majorUnit: 25,
                min: 0,
                max: 100
            },
            axisLabelClick: this._axisLabelClickDelegate
        });
    },

    _changeHandler: function (e) {
        var data = e.sender.data();

        for (var i = 0; i < data.length; i++) {
            var issueName = (data[i].Name.length < 29) ? data[i].Name : data[i].Name.substring(0, 26).concat("...");
            data[i].CategoryAxisName = issueName;
            data[i].ClickedPercentage = data[i].Sent > 0 ? ((data[i].Clicked / data[i].Sent) * 100).toFixed(2) : 0;
            data[i].OpenedPercentage = data[i].Sent > 0 ? ((data[i].Opened / data[i].Sent) * 100).toFixed(2) : 0;
            data[i].DeliveredPercentage = data[i].Sent > 0 ? ((data[i].Delivered / data[i].Sent) * 100).toFixed(2) : 0;
        }
    },

    _axisLabelClickHandler: function (e) {
        if (e.axis.type != "Category") {
            return;
        }

        var chart = e.sender;
        var chartContainer = e.sender.wrapper;
        var categoryIndex = e.index;

        var plotArea = chart._plotArea;
        var areaChart = plotArea.charts[0];
        var categoryPoints = areaChart.categoryPoints[categoryIndex];
        var categoryAxis = plotArea.categoryAxis;
        var slot = categoryAxis.getSlot(categoryIndex);

        jQuery(".sfChartCategoryHighlight, .sfChartPointDetail").remove();

        for (var i = 0; i < categoryPoints.length; i++) {
            var point = categoryPoints[i];
            var offset = jQuery("#" + point.options.id).offset();
            var detail = null;

            if (point.series.field.indexOf("Clicked") != -1) {
                var clickedTemplate = kendo.template(jQuery("#clickedTemplate").html());
                detail = jQuery(clickedTemplate(point)).appendTo(chartContainer);
            }
            else if (point.series.field.indexOf("Opened") != -1) {
                var openedTemplate = kendo.template(jQuery("#openedTemplate").html());
                detail = jQuery(openedTemplate(point)).appendTo(chartContainer);
            }
            else {
                var deliveredTemplate = kendo.template(jQuery("#deliveredTemplate").html());
                detail = jQuery(deliveredTemplate(point)).appendTo(chartContainer);
            }

            detail.css({
                top: offset.top - detail.height(),
                left: offset.left + 10
            });
        }

        jQuery("<div class='sfChartCategoryHighlight'></div>")
                            .prependTo(chartContainer)
                            .css({
                                top: slot.y1, left: slot.x1,
                                width: slot.width() - 1 // 1px border
                            });
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

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.CampaignOverviewChart', Sys.UI.Control);
