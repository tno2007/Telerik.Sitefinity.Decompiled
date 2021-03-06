Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

var $get_clientId;
if (typeof ($FormManager) === "undefined") {
    var $FormManager = null;
}

Telerik.Sitefinity.Web.UI.Fields.FormManager = function () {
    Telerik.Sitefinity.Web.UI.Fields.FormManager.initializeBase(this);
    this._controlIdMappings = {};
    this._validationGroupMappings = {}; // key is group name, value is array of field client IDs
};

Telerik.Sitefinity.Web.UI.Fields.FormManager.IsRegistered = false;

Telerik.Sitefinity.Web.UI.Fields.FormManager.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.FormManager.callBaseMethod(this, "initialize");
        if (Telerik.Sitefinity.Web.UI.Fields.FormManager.IsRegistered) {
            return;
        }
        Telerik.Sitefinity.Web.UI.Fields.FormManager._controlIdMappings = this._controlIdMappings;

        Telerik.Sitefinity.Web.UI.Fields.FormManager.getControlId = $get_clientId = this.getControlId;
        Telerik.Sitefinity.Web.UI.Fields.FormManager._validationGroupMappings = this._validationGroupMappings;
        Telerik.Sitefinity.Web.UI.Fields.FormManager.validateGroup = this.validateGroup;
        Telerik.Sitefinity.Web.UI.Fields.FormManager.validateAll = this.validateAll;
        $FormManager = this;
        Telerik.Sitefinity.Web.UI.Fields.FormManager.IsRegistered = true;
    },

    dispose: function () {

        Telerik.Sitefinity.Web.UI.Fields.FormManager.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    getControlId: function (serverId) {
        var controlId = $FormManager._controlIdMappings[serverId];
        if (!controlId) {
            //throw "No control with id: " + serverId + " was found. You must Register this control in the FormManger."
            alert('This form expects a control with ID "' + serverId + '" to be present and may not work properly as it is missing. You may see this message, because the corresponding default field was removed.');
            controlId = null;
        }
        return controlId;
    },

    // Validates all the fields by their validation groups
    validateGroup: function (validationGroup) {
        if (validationGroup === undefined || validationGroup == null) {
            validationGroup = ""; // Default group is emty string
        }

        var clientIDs = $FormManager._validationGroupMappings[validationGroup];
        var result = true;
        var violationMessages = [];
        if (clientIDs) {
            var iter = clientIDs.length;
            while (iter--) {
                var fieldControl = $find(clientIDs[iter]);
                var resultArgs = $FormManager._raiseFieldValidation(fieldControl, validationGroup);
                var isValid;

                if (resultArgs &&
                    typeof (resultArgs.get_validated) === "function" &&
                    typeof (resultArgs.get_isValid) === "function") {
                    if (resultArgs.get_validated()) {
                        isValid = resultArgs.get_isValid();
                    }
                }
                else {
                    isValid = fieldControl.validate();
                }

                if (!isValid) {
                    var msgs = fieldControl.get_violationMessages();
                    var count = msgs.length;
                    while (count--) {
                        violationMessages.push(msgs[count]);
                    }
                    result = false;
                }
            }
        }
        $FormManager._raiseFormValidationCompleted(violationMessages, validationGroup, result);
        return result;
    },

    // Validates all validation groups
    validateAll: function () {
        var result = true;
        var mappings = this._validationGroupMappings;
        for (var group in mappings) {
            var isValid = this.validateGroup(group);
            if (!isValid) {
                result = false;
            }
        }
        return result;
    },

    /* -------------------- events -------------------- */
    add_onFieldValidation: function (delegate) {
        ///<summary>Adds delegate to be invoked by when a field is being validated
        this.get_events().addHandler('onFieldValidation', delegate);
    },

    remove_onFieldValidation: function (delegate) {
        this.get_events().removeHandler('onFieldValidation', delegate);
    },


    add_onFormValidationCompleted: function (delegate) {
        ///<summary>Adds delegate to be invoked by when after the validation is completed
        this.get_events().addHandler('onFormValidationCompleted', delegate);
    },

    remove_onFormValidationCompleted: function (delegate) {
        this.get_events().removeHandler('onFormValidationCompleted', delegate);
    },

    /* -------------------- private methods ----------- */
    _raiseFieldValidation: function (fieldControl, validationGroup) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs(fieldControl, validationGroup);
        var h = this.get_events().getHandler('onFieldValidation');
        if (h) h(this, eventArgs);
    },

    _raiseFormValidationCompleted: function (violationMessages, validationGroup, validationResult) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs(violationMessages, validationGroup, validationResult);
        var h = this.get_events().getHandler('onFormValidationCompleted');
        if (h) h(this, eventArgs);
    }

    /* -------------------- properties ---------------- */

};
Telerik.Sitefinity.Web.UI.Fields.FormManager.registerClass("Telerik.Sitefinity.Web.UI.Fields.FormManager", Sys.Component);

