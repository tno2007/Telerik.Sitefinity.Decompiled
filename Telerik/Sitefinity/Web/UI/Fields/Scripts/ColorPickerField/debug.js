Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ColorPickerField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ColorPickerField.initializeBase(this, [element]);

    this._colorPicker = null;
}

Telerik.Sitefinity.Web.UI.Fields.ColorPickerField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ColorPickerField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ColorPickerField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this.set_value("#C0C0C0");

        Telerik.Sitefinity.Web.UI.Fields.ColorPickerField.callBaseMethod(this, "reset");
    },

    get_value: function () {
        var value = this.get_colorPicker().get_selectedColor();
        return value;
    },

    set_value: function (value) {
        this._value = value;

        if (value) {
            this.get_colorPicker().set_selectedColor(value);
        }
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    /* -------------------- properties ---------------- */

    // Gets the reference to the color picker.
    get_colorPicker: function () {
        return this._colorPicker;
    },

    // Sets the reference to the color picker.
    set_colorPicker: function (value) {
        this._colorPicker = value;
    }
};
Telerik.Sitefinity.Web.UI.Fields.ColorPickerField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ColorPickerField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);