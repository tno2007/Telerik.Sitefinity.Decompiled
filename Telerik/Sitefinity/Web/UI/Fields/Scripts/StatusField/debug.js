Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.StatusField = function(element) {
    this._element = element;

    Telerik.Sitefinity.Web.UI.Fields.StatusField.initializeBase(this, [element]);
    this._expirationDateComponent = null;
    this._publicationDateComponent = null;
    this._statusChoiseComponent = null;
    this._choiceDescriptions = null;
    this._readModeLabel = null;

    this._messageLabel = null;
    this._datesPanel = null;

    this._onLoadDelegate = null;
    this._onChoiceChangeDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.StatusField.prototype =
{
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Fields.StatusField.callBaseMethod(this, "initialize");
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._onLoadDelegate = Function.createDelegate(this, this._on_load);
            Sys.Application.add_load(this._onLoadDelegate);
            this._onChoiceChangeDelegate = Function.createDelegate(this, this._on_choiceChange);
        }
        else {

        }
    },

    dispose: function() {
        Telerik.Sitefinity.Web.UI.Fields.StatusField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // This function allows other objects to subscribe to the statusChanged event of the field control.
    add_statusChanged: function(delegate) {
        this.get_events().addHandler('statusChanged', delegate);
    },

    // This function allows other objects to unsubscribe from the statusChanged event of the field control.
    remove_statusChanged: function(delegate) {
        this.get_events().removeHandler('statusChanged', delegate);
    },
    reset: function() {
        //do nothing. The contained field controls must be reset.
    },
    /* -------------------- events -------------------- */

    _statusChangedHandler: function() {
        var h = this.get_events().getHandler('statusChanged');
        if (h) h(this, Sys.EventArgs.Empty);
        return Sys.EventArgs.Empty;
    },

    /* -------------------- event handlers ------------ */

    _on_load: function() {
        this._statusChoiseComponent.add_valueChanged(this._onChoiceChangeDelegate);
    },

    //handles the choice selection
    _on_choiceChange: function() {
        var value = this._statusChoiseComponent.get_value();
        var message = this._choiceDescriptions[value];
        jQuery(this._messageLabel).text(message);

        if (value == Telerik.Sitefinity.GenericContent.Model.ContentUIStatus.Scheduled) {
            //if (!this.get_publicationDateComponent().get_value())
            //this.get_publicationDateComponent().set_value(new Date());
            jQuery(this._datesPanel).show();
        }
        else {
            jQuery(this._datesPanel).hide();
        }
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    // gets the reference to the expiration date component used to choose content expiration date
    // and to be displayed by the view
    get_expirationDateComponent: function() {
        return this._expirationDateComponent;
    },

    // sets the reference to the expiration date component used to choose content expiration date
    // and to be displayed by the view
    set_expirationDateComponent: function(value) {
        this._expirationDateComponent = value;
    },

    // gets the reference to the publication date component used to choose content publication date
    // and to be displayed by the view
    get_publicationDateComponent: function() {
        return this._publicationDateComponent;
    },

    // sets the reference to the publication date component used to choose content publication date
    // and to be displayed by the view
    set_publicationDateComponent: function(value) {
        this._publicationDateComponent = value;
    },

    // gets the reference to the status choise component to be displayed by the view
    get_statusChoiseComponent: function() {
        return this._statusChoiseComponent;
    },

    // gets the reference to the status choise component to be displayed by the view
    set_statusChoiseComponent: function(value) {
        this._statusChoiseComponent = value;
    },

    // gets the reference to the status choise component to be displayed by the view
    get_choiceDescriptions: function() {
        return this._choiceDescriptions;
    },

    // gets the reference to the status choise component to be displayed by the view
    set_choiceDescriptions: function(value) {
        this._choiceDescriptions = value;
    },

    // gets the element that displays the message when switching the choice
    get_messageLabel: function() {
        return this._messageLabel;
    },

    // sets the element that displays the message when switching the choice
    set_messageLabel: function(value) {
        this._messageLabel = value;
    },

    // gets the panel containig the dates
    get_datesPanel: function() {
        return this._datesPanel;
    },

    // sets the panel containing
    set_datesPanel: function(value) {
        this._datesPanel = value;
    },

    // gets the label in read mode
    get_readModeLabel: function() {
        return this._readModeLabel;
    },

    // sets the label in read mode
    set_readModeLabel: function(value) {
        this._readModeLabel = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.StatusField.registerClass("Telerik.Sitefinity.Web.UI.Fields.StatusField", Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl);

Type.registerNamespace("Telerik.Sitefinity.GenericContent.Model");

Telerik.Sitefinity.GenericContent.Model.ContentUIStatus = function() {
};
Telerik.Sitefinity.GenericContent.Model.ContentUIStatus.prototype = {
    /// The content publidation default state.
    /// Invisible to the public side.
    Draft: 0,
    //autosave recovery record
    PrivateCopy :1,
    /// Visible to the public side.
    Published: 2,
    /// Scheduled to be visible for the public side.
    Scheduled: 3
};
Telerik.Sitefinity.GenericContent.Model.ContentUIStatus.registerEnum("Telerik.Sitefinity.GenericContent.Model.ContentUIStatus");
