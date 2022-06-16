Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.Designers.Views");

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.initializeBase(this, [element]);
    this._viewsSelector = null;
}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var selectedViews = this.get_controlData().VisibleViews;
        var hidden = this.get_controlData().Hidden;
        if (selectedViews && selectedViews.length > 0) {
            this.get_viewsSelector().setSelectedViews(selectedViews);
        }
        else if (hidden) {
            this.get_viewsSelector().setSelectedViews("nowhere");
        }
        else {
            this.get_viewsSelector().setSelectedViews("allViews");
        }
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.callBaseMethod(this, 'refreshUI');
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var selectedViews = this.get_viewsSelector().getSelectedViews();
        this.get_controlData().VisibleViews = selectedViews;
        if (selectedViews === "allViews") {
            this.get_controlData().Hidden = "false";
        }
        else if (selectedViews === "nowhere") {
            this.get_controlData().Hidden = "true";
        }
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.callBaseMethod(this, 'applyChanges');
    },

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    get_viewsSelector: function () {
        return this._viewsSelector;
    },
    set_viewsSelector: function (value) {
        this._viewsSelector = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView.registerClass('Telerik.Sitefinity.Web.UI.Fields.Designers.Views.TextFieldAppearanceView', Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormTextBoxAppearanceView);
