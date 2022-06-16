﻿Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView.initializeBase(this, [element]);

    this._siteDetailView = null;
    this._currentSiteId = null;
    this._backLinkUrl = null;
}

Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView.callBaseMethod(this, "initialize");

        //this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        //Sys.Application.add_load(this._onLoadDelegate);

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);
        jQuery(document).ready(this._documentReadyDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView.callBaseMethod(this, "dispose");

        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _documentReadyHandler: function () {
        var that = this;

        this.get_siteDetailView().set_backLinkUrl(this.get_backLinkUrl());

        this.get_siteDetailView().set_kendoLoadedHandler(function () {
            that.show(that._getSiteId());
        });
    },

    //_onLoadHandler: function () {
    //    //this.get_siteDetailView().set_isInStandaloneMode(true);
    //    //this.show(this._getSiteId());
    //},

    _getSiteId: function () {
        var search = window.location.search;
        var queryStringParser = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(search.substring(1, search.length));

        var siteId = queryStringParser.get("sf_site");

        return (siteId) ? siteId : this.get_currentSiteId();
    },

    reset: function () {
    },

    show: function (siteId) {
        this.get_siteDetailView().show(false, siteId, false);
    },

    get_siteDetailView: function () {
        return this._siteDetailView;
    },
    set_siteDetailView: function (value) {
        this._siteDetailView = value;
    },
    get_currentSiteId: function () {
        return this._currentSiteId;
    },
    set_currentSiteId: function (value) {
        this._currentSiteId = value;
    },
    get_backLinkUrl: function () {
        return this._backLinkUrl;
    },
    set_backLinkUrl: function (value) {
        this._backLinkUrl = value;
    }
}

Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView.registerClass('Telerik.Sitefinity.Multisite.Web.UI.SiteSettingsView', Sys.UI.Control);
