﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks.initializeBase(this, [element]);

    this._searchBox = null;
    this._searchBoxSearchDelegate = null;

    this._clickedLinkFilters = null;
    this._clickedLinkFilterTemplate = null;
    this._clickedLinkFilterClickDelegate = null;

    this._webServiceUrl = null;
    this._dataSource = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks.callBaseMethod(this, "initialize");

        this._searchBoxSearchDelegate = Function.createDelegate(this, this._searchBoxSearch);
        this.get_searchBox().add_search(this._searchBoxSearchDelegate);

        this._clickedLinkFilterTemplate = kendo.template(jQuery("#clickedLinkFilterTemplate").html());
        this._clickedLinkFilterClickDelegate = Function.createDelegate(this, this._clickedLinkFilterClick);

        var that = this;
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: String.format(this._webServiceUrl, "")
            },
            schema: {
                model: {
                    fields: {
                        key: { type: "string" },
                        value: { type: "number" }
                    }
                }
            },
            change: function () {
                var jClickedLinkFilters = jQuery(that.get_clickedLinkFilters());
                jClickedLinkFilters.html(kendo.render(that._clickedLinkFilterTemplate, this.view()));
                var jAnchors = jClickedLinkFilters.find("a");
                for (var i = 0; i < jAnchors.length; i++) {
                    $addHandler(jAnchors[i], "click", that._clickedLinkFilterClickDelegate);
                }
            }
        });
        this._dataSource.read();
    },

    dispose: function () {
        if (this._searchBoxSearchDelegate) {
            if (this.get_searchBox()) {
                this.get_searchBox().remove_search(this._searchBoxSearchDelegate);
            }
            delete this._searchBoxSearchDelegate;
        }

        if (this._clickedLinkFilterClickDelegate) {
            delete this._clickedLinkFilterClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks.callBaseMethod(this, "dispose");
    },

    clear: function () {
        this.get_searchBox().clearSearchBox();
    },

    _searchBoxSearch: function (sender, args) {
        this._dataSource.transport.options.read.url = String.format(this._webServiceUrl, escape(args.get_query()));
        this._dataSource.read();
    },

    add_selected: function (delegate) {
        this.get_events().addHandler("selected", delegate);
    },
    remove_selected: function (delegate) {
        this.get_events().removeHandler("selected", delegate);
    },

    _raiseSelected: function (url) {
        var handler = this.get_events().getHandler("selected");
        if (handler) {
            handler(this, url);
        }
    },

    _clickedLinkFilterClick: function (sender, args) {
        this._raiseSelected(jQuery(sender.target).parent().find("input").val());
        jQuery(this._element).find("li").removeClass("sfSel");
        jQuery(sender.target).parent().addClass("sfSel");
    },

    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        this._searchBox = value;
    },
    get_clickedLinkFilters: function () {
        return this._clickedLinkFilters;
    },
    set_clickedLinkFilters: function (value) {
        this._clickedLinkFilters = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportClickedLinks', Sys.UI.Control);