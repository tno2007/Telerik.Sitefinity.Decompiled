/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.LinkField = function (element) {

    Telerik.Sitefinity.Web.UI.Fields.LinkField.initializeBase(this, [element]);
    this._element = element;
    this._linkFieldButton = null;
    this._value = null;
    this._commandName = null;
}

Telerik.Sitefinity.Web.UI.Fields.LinkField.prototype =
{
    initialize: function () {

        if (this._linkFieldButton) {
            this._onLinkClickedDelegate = Function.createDelegate(this, this._onLinkClicked);
            $addHandler(this._linkFieldButton, 'click', this._onLinkClickedDelegate);
        }

        Telerik.Sitefinity.Web.UI.Fields.LinkField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._onLinkClickedDelegate) {
            $removeHandler(this._linkFieldButton, 'click', this._onLinkClickedDelegate);
        }

        this._linkFieldButton = null;

        Telerik.Sitefinity.Web.UI.Fields.LinkField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.LinkField.callBaseMethod(this, "reset");
    },

    // Gets the value of the field control.
    get_value: function () {
        var val = this._value;
        return val;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        this._value = value;
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
        return false;
    },

    /* -------------------- events -------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    /* -------------------- event handlers ------------ */


    /* -------------------- private methods ----------- */


    /* -------------------- properties ---------------- */

    // Gets the reference to the DOM element used to display the link control.
    get_linkFieldButton: function () {
        return this._linkFieldButton;
    },

    // Sets the reference to the DOM element used to display the link control.
    set_linkFieldButton: function (value) {
        this._linkFieldButton = value;
    },

    _onLinkClicked: function (sender, args) {
        var eventArgs = new Telerik.Sitefinity.CommandEventArgs(this._commandName);
        this.onCommand(sender, eventArgs);
    },

    onCommand: function (sender, args) {
        var h = this.get_events().getHandler('command');
        if (h) h(this, args);
    },

    // Gets the reference to the DOM element used to display the link control.
    get_commandName: function () {
        return this._commandName;
    },

    // Sets the reference to the DOM element used to display the link control.
    set_commandName: function (value) {
        this._commandName = value;
    },


    // Sets the position in the tabbing order
    // Overridden from field control
    set_tabIndex: function (value) {
        this._tabIndex = value;
        jQuery(this.get_linkFieldButton()).attr("tabindex", value);
    },

    // Gets the position in the tabbing order
    // Overriden from field control
    get_tabIndex: function () {
        return jQuery(this.get_linkFieldButton()).attr("tabindex");
    },


    // Focuses the input element.
    // Overriden from field control
    focus: function () {
        var input = this.get_linkFieldButton();
        if (jQuery(input).is(":visible") && jQuery(input).is(":enabled")) {
            input.focus();
        }
    },

    // Blures the anchor element.
    // Overriden from field control
    blur: function () {
        //Not implemented
    },

    // Sets the default value of the field control.
    // overriden from FieldControl
    set_defaultValue: function (value) {
        // converting it to string to be comptible with the results returned by get_value
        if (value === null || value === undefined) {
            value = "";
        }
        this._defaultValue = value;
    },

    get_element: function () {
        return this._element;
    },
    set_element: function (value) {
        this._element = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.LinkField.registerClass("Telerik.Sitefinity.Web.UI.Fields.LinkField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext, Telerik.Sitefinity.Web.UI.Fields.ICommandField);
