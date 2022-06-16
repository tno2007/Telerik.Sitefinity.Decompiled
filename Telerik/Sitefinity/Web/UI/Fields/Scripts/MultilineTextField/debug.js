/// <reference name="MicrosoftAjax.js"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.MultilineTextField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.initializeBase(this, [element]);
    this._validatingDelegate = null;
    this._validatingRegex = null;
}

Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.callBaseMethod(this, "initialize");
        this._validatingDelegate = Function.createDelegate(this, this._validatingHandler);
        this.get_validator().add_onValidating(this._validatingDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.callBaseMethod(this, "dispose");
        if (this._validatingDelegate) {
            delete this._validatingDelegate;
        }
    },

    _validatingHandler: function (sender, args) {
        var toBeValidated = this.get_value();
        var toBeValidatedLength = toBeValidated.length;
        var isValid = true;
        for (var i = 0; i < toBeValidatedLength; i++) {
            isValid = isValid && this._getValidatingRegex().test(toBeValidated[i]);
            if (!isValid) {
                sender.set_violationMessages(["The url '" + toBeValidated[i] + "' is not valid."]);
                break;
            }
        }
        if (Object.getType(args).inheritsFrom(Sys.CancelEventArgs)) {
            args.set_cancel(true);
            args.set_isValid(isValid);
        }
    },

    set_value: function (value) {
        var textAreaText = "";
        if (value && jQuery.isArray(value)) {
            textAreaText = value.join("\n");
            if (value.length > 0) {
                textAreaText += "\n"; // User friendly - for easy adding new url.
            }
        }
        Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.callBaseMethod(this, "set_value", [textAreaText]);
    },

    get_value: function () {
        var textAreaText = Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.callBaseMethod(this, "get_value");
        var outputWithoutEmptyStrings = [];
        if (textAreaText) {
            var output = textAreaText.split('\n');
            for (var i = 0, outputLength = output.length; i < outputLength; i++) {
                if (jQuery.trim(output[i]) != "") {
                    outputWithoutEmptyStrings.push(jQuery.trim(output[i]));
                }
            }
        }
        return Telerik.Sitefinity.fixArray(outputWithoutEmptyStrings);
    },

    isChanged: function () {
        var currentValueArray = this.get_value();
        var value = currentValueArray.join("\n");
        if (currentValueArray.length > 0) {
            value += "\n";
        }
        var notChanged = (this._value == value);
        if (notChanged) {
            return false;
        } else {
            return true;
        }
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    _getValidatingRegex: function () {
        if (!this._validatingRegex)
            this._validatingRegex = new XRegExp(this.get_validator().get_regularExpression());
        return this._validatingRegex;
    }

    /* -------------------- properties ---------------- */

};

Telerik.Sitefinity.Web.UI.Fields.MultilineTextField.registerClass("Telerik.Sitefinity.Web.UI.Fields.MultilineTextField", Telerik.Sitefinity.Web.UI.Fields.TextField);
