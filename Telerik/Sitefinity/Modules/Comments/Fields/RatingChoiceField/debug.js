Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Fields");

Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField = function (element) {
    Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.callBaseMethod(this, "dispose");
    },

    // Gets the value of the field control.
    get_value: function () {
        if (jQuery(this._element).is(":visible")) {
            return Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.callBaseMethod(this, "get_value");
        } else {
            return null;
        }
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.callBaseMethod(this, "set_value", [value]);

        var valueAsNumber = parseFloat(value);
        if (!valueAsNumber || valueAsNumber < 0 || valueAsNumber > 5) {
            jQuery(this._element).hide();
        } else {
            jQuery(this._element).show();
        }
    }
};

Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField.registerClass("Telerik.Sitefinity.Modules.Comments.Fields.RatingChoiceField", Telerik.Sitefinity.Web.UI.Fields.ChoiceField, Telerik.Sitefinity.Web.UI.ISelfExecutableField);
