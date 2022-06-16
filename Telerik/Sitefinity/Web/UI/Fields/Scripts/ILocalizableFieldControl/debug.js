Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

// An interface that provides common properties for all field controls that support visualizing data in a specific culture.
Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl = function() {
}

Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl.prototype = {
    // Gets the culture to use when visualizing a content.
    get_culture: function () {
    },
    // Sets the culture to use when visualizing a content.
    set_culture: function (culture) {
    },

    // Gets the UI culture to use when visualizing a content.
    get_uiCulture: function () {
    },
    // Sets the UI culture to use when visualizing a content.
    set_uiCulture: function (culture) {
    }
};

Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl.registerInterface("Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl");
