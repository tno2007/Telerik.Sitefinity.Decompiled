Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector.initializeBase(this, [element]);

    this._siblingTypeSelector = null;
}

Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------

    initialize: function () {
        /// <summary>Initialization handler.</summary>
        Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector.callBaseMethod(this, "initialize");

        if (this._siblingTypeSelector) {
            jQuery(this._siblingTypeSelector).clickMenu();
            jQuery(this._siblingTypeSelector).closest(".sfBreadCrumb").addClass("sfBreadCrumbHasTypes");
        }
    },

    get_siblingTypeSelector: function () {
        return this._siblingTypeSelector;
    },

    set_siblingTypeSelector: function (value) {
        this._siblingTypeSelector = value;
    }
};

Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.SiblingTypeSelector', Sys.UI.Control);