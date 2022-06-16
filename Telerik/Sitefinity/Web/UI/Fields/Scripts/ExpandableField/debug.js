Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ExpandableField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ExpandableField.initializeBase(this, [element]);
    this._element = element;
    this._expandField = null;
    this._expandableTarget = null;

    this._toggleDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.ExpandableField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ExpandableField.callBaseMethod(this, "initialize");

        this._toggleDelegate = Function.createDelegate(this, this.toggle);

        if (this._expandField) {
            $addHandler(this._expandField.get_choiceElement(), 'click', this._toggleDelegate);
        }
    },

    dispose: function () {
        if (this._expandField) {
            $clearHandlers(this._expandField);
        }

        Telerik.Sitefinity.Web.UI.Fields.ExpandableField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // resets the control to the original expanded state
    reset: function () {
        this._expandField.reset();
        this.toggle();
        Telerik.Sitefinity.Web.UI.Fields.ExpandableField.callBaseMethod(this, "reset");
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
        if (this._expandField._value == null) this._expandField._value = "";
        var thisValue = this.get_value();

        if (thisValue == "true") thisValue = true;
        if (thisValue == "false") thisValue = false;
        if (thisValue == "1") thisValue = 1;
        if (thisValue == "0") thisValue = 0;

        var notChanged = (this._expandField._value == thisValue);
        if (notChanged) {
            return false;
        }
        else {
            return true;
        }
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    toggle: function () {
        var isChecked = this._expandField.get_value();
        if (isChecked == "true") {
            this._expandableTarget.style.display = '';
        }
        else {
            this._expandableTarget.style.display = 'none';
        }
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    get_value: function () {
        return this._expandField.get_value();
    },

    set_value: function (value) {
        if (this._expandField) {
            this._expandField.set_value(value);
            this.toggle();
        }

        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    get_expandField: function () {
        return this._expandField;
    },
    set_expandField: function (value) {
        this._expandField = value;
    },

    get_expandableTarget: function () {
        return this._expandableTarget;
    },
    set_expandableTarget: function (value) {
        this._expandableTarget = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.ExpandableField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ExpandableField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);