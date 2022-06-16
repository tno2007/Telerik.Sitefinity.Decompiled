Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
// If this interface is implemented the field controls binder will subscribe for the command event.
Telerik.Sitefinity.Web.UI.Fields.ICommandField = function() {
}

Telerik.Sitefinity.Web.UI.Fields.ICommandField.prototype = {
    add_command: function (handler) {
    },
    remove_command: function (handler) {
    }
};

Telerik.Sitefinity.Web.UI.Fields.ICommandField.registerInterface("Telerik.Sitefinity.Web.UI.Fields.ICommandField");
