/// <reference name="MicrosoftAjax.js"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField.initializeBase(this, [element]);

    this._isOneSiteMode = null;
};

Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField.callBaseMethod(this, "initialize");

        if (this.get_isOneSiteMode()) {
            this.get_parent().set_visible(false);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField.callBaseMethod(this, "dispose");
    },

    get_isOneSiteMode: function () {
        return this._isOneSiteMode;
    },

    set_isOneSiteMode: function (value) {
        this._isOneSiteMode = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField.registerClass("Telerik.Sitefinity.Web.UI.Fields.MultisiteChoiceField", Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
