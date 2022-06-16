Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI");

Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector = function (element) {
    this._baseServiceUrl = null;
    this._taxonomyId = null;
    this._siteContextMode = null;

    Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector.prototype =
{
    // override
    _setServiceUrls: function () {
        var serviceBaseUrl = this._baseServiceUrl + this._taxonomyId + "/";

        if (this._siteContextMode)
            serviceBaseUrl += "?siteContextMode=" + this._siteContextMode

        this._treeBinder.set_orginalServiceBaseUrl(serviceBaseUrl);
        this._treeBinder.set_serviceChildItemsBaseUrl(this._baseServiceUrl + "subtaxa/");
        this._treeBinder.set_servicePredecessorBaseUrl(this._baseServiceUrl + "predecessor/");
    },

    // Gets the culture to use when visualizing a content.
    get_culture: function () {
        return this._treeBinder.get_culture();
    },

    // Sets the culture to use when visualizing a content.
    set_culture: function (culture) {
        this._treeBinder.set_culture(culture);
    },

    // Gets the UI culture to use when visualizing a content.
    get_uiCulture: function () {
        return this._treeBinder.get_uiCulture();
    },

    // Sets the UI culture to use when visualizing a content.
    set_uiCulture: function (culture) {
        this._treeBinder.set_uiCulture(culture);
    }
};

Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector.registerClass("Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonSelector", Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector);