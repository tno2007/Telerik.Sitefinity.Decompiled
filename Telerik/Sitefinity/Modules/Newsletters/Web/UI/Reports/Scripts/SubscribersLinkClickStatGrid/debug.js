﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid.initializeBase(this, [element]);

    this._grid = null;
    this._webServiceUrl = null;
    this._dataSource = null;
    this._clientLabelManager = null;

    this._documentReadyDelegate = null;
    this._changeDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid.callBaseMethod(this, "initialize");

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

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid.callBaseMethod(this, "dispose");
    },

    _documentReadyHandler: function () {
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: this.get_webServiceUrl()
            },
            schema: {
                data: "Items",
                model: {
                    fields: {
                        SubscriberName: { type: "string" },
                        Url: { type: "string" },
                        DateTimeClicked: { type: "date" }
                    }
                }
            }
        });

        jQuery(this.get_grid()).kendoGrid({
            dataSource: this._dataSource,
            scrollable: false,
            columns: [{
                field: "SubscriberName",
                title: this.get_clientLabelManager().getLabel("NewslettersResources", "Name")
            },
            {
                field: "Url",
                title: this.get_clientLabelManager().getLabel("NewslettersResources", "ClickedUrl")
            },
            {
                field: "DateTimeClicked",
                template: '#= DateTimeClicked.sitefinityLocaleFormat("MMMM dd, hh:mm") #',
                title: this.get_clientLabelManager().getLabel("NewslettersResources", "DateClicked")
            }]
        });
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
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersLinkClickStatGrid', Sys.UI.Control);
