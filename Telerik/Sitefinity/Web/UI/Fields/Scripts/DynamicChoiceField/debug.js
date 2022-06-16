/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.prototype =
{
    // Gets the value of the field control.
    // If a single item is selected returns the value, otherwise of more that one is selected
    // returns an array with the selected values
    get_value: function () {
        var baseValue = Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.callBaseMethod(this, "get_value");
        var value = this.getDynamicChoiceValue(baseValue);
        return value;
    },

    getDynamicChoiceValue: function (baseValue) {
        var value = null;
        if (jQuery.isArray(baseValue)) {
            value = [];
            for (var i = 0; i < baseValue.length; i++) {
                var choice = this._get_choice(baseValue[i]);
                var option = { 'PersistedValue': baseValue[i], 'Text': choice.Text };
                value.push(option);
            }
        }
        else if (baseValue) {
            var choiceItem = this._get_choice(baseValue);
            value = { 'PersistedValue': baseValue, 'Text': choiceItem.Text };
        }
        return value;
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
        var currValue = Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.callBaseMethod(this, "get_value"),
            hasChanged = Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.callBaseMethod(this, "hasValueChanged", [currValue]);

        return hasChanged;
    },

    // Sets the value of the field control.
    set_value: function (value) {
        var baseValues = [];
        if (jQuery.isArray(value)) {
            for (var i = 0; i < value.length; i++) {
                var option = value[i];
                if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
                    if (option.hasOwnProperty("PersistedValue")) {
                        baseValues.push(option.PersistedValue);
                    }
                    else {
                        baseValues.push(option);
                    }
                }
                else {
                    if (option.hasOwnProperty("Text")) {
                        baseValues.push(option.Text);
                    }
                    else {
                        baseValues.push(option);
                    }
                }
            }
            Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.callBaseMethod(this, "set_value", [baseValues]);
        }
        else {
            if (value) {
                if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
                    if (value.hasOwnProperty("PersistedValue")) {
                        baseValues.push(value.PersistedValue);
                    }
                    else {
                        baseValues.push(value);
                    }
                }
                else {
                    if (value.hasOwnProperty("Text")) {
                        baseValues.push(value.Text);
                    }
                    else {
                        var choice = this._get_choice(value);
                        if (choice) {
                            baseValues.push(choice.Text);
                        }
                        else {
                            baseValues.push(value);
                        }
                    }
                }
            }
            else {
                baseValues = value;
            }
            Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.callBaseMethod(this, "set_value", baseValues);
        }
    },

    _get_choice: function (value) {
        var choices = this.get_choices();
        for (var i = 0; i < choices.length; i++) {
            if(this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write && choices[i].Value === value) {
                return choices[i];
            }
            else if(choices[i].Text.trim() === value.trim()){
                return choices[i];
            }
        }
    },

    get_returnValuesAlwaysInArray: function () {
        return this._returnValuesAlwaysInArray;
    },

    set_returnValuesAlwaysInArray: function (value) {
        this._returnValuesAlwaysInArray = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField.registerClass("Telerik.Sitefinity.Web.UI.Fields.DynamicChoiceField", Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
