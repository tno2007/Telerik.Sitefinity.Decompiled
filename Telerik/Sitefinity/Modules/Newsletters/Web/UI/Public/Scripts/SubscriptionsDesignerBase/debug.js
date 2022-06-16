Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public");

// ------------------------------------------------------------------------
// SubscriptionsDesignerBase class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase = function (element) {
    this._propertyEditor = null;
    this._mailingListRadCombo = null;
    this._widgetTitleTextField = null;
    this._widgetDescriptionTextField = null;
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {
        var controlData = this.get_controlData();
        if (this._mailingListRadCombo != null) {
            controlData.ListId = this.get_mailingListRadCombo().get_value();
        }
        if (this._widgetTitleTextField != null) {
            controlData.WidgetTitle = this.get_widgetTitleTextField().get_value();
        }
        if (this._widgetDescriptionTextField != null) {
            controlData.WidgetDescription = this.get_widgetDescriptionTextField().get_value();
        }
    },

    refreshUI: function () {
        var controlData = this.get_controlData();
        if (this._widgetTitleTextField != null) {
            this.get_widgetTitleTextField().set_value(controlData.WidgetTitle);
        }
        if (this._widgetDescriptionTextField != null) {
            this.get_widgetDescriptionTextField().set_value(controlData.WidgetDescription);
        }
        if (this._mailingListRadCombo != null) {
            var selectedItem = this.get_mailingListRadCombo().findItemByValue(controlData.ListId);
            if (selectedItem) {
                selectedItem.select();
            }
        }
    },

    /* ************************* properties ************************* */
    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },
    // gets the reference to the maling list rad combo box
    get_mailingListRadCombo: function () {
        return this._mailingListRadCombo;
    },
    // sets the reference to the mailing list rad combo box
    set_mailingListRadCombo: function (value) {
        this._mailingListRadCombo = value;
    },
    // gets the reference to the widget title text field
    get_widgetTitleTextField: function () {
        return this._widgetTitleTextField;
    },
    // sets the reference to the widget title text field
    set_widgetTitleTextField: function (value) {
        this._widgetTitleTextField = value;
    },
    // gets the reference to the widget description text field
    get_widgetDescriptionTextField: function () {
        return this._widgetDescriptionTextField;
    },
    // sets the reference to the widget description text field
    set_widgetDescriptionTextField: function (value) {
        this._widgetDescriptionTextField = value;
    }
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);