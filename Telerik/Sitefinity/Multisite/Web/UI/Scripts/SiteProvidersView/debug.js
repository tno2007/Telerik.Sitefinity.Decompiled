Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView.initializeBase(this, [element]);

    this._siteDetailView = null;
    this._currentSiteId = null;
}

Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView.callBaseMethod(this, "initialize");

        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView.callBaseMethod(this, "dispose");
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _onLoadHandler: function () {
        this.get_siteDetailView().set_isInStandaloneMode(true);
        this.show(this._getSiteId());
    },

    _getSiteId: function () {
        var search = window.location.search;
        var queryStringParser = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(search.substring(1, search.length));

        var siteId = queryStringParser.get("sf_site");

        return (siteId) ? siteId : this.get_currentSiteId();
    },

    reset: function () {
    },

    show: function (siteId) {
        this.get_siteDetailView().show(false, siteId, true);
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
    }
};

Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView.registerClass('Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView', Sys.UI.Control);
