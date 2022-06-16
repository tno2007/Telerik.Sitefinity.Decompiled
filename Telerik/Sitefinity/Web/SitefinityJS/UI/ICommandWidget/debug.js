// ICommandWidget interface implemented by all command widgets
Type.registerNamespace("Telerik.Sitefinity.UI");

Telerik.Sitefinity.UI.ICommandWidget = function() {}

Telerik.Sitefinity.UI.ICommandWidget.prototype = {
    add_command: function(handler) { },
    remove_command: function(handler) { }
}

Telerik.Sitefinity.UI.ICommandWidget.registerInterface("Telerik.Sitefinity.UI.ICommandWidget");

Telerik.Sitefinity.UI.CommandEventArgs = function(commandName, commandArgument) {
    this._commandName = commandName;
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.UI.CommandEventArgs.initializeBase(this);
}

Telerik.Sitefinity.UI.CommandEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.UI.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.UI.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function() {
        return this._commandName;
    },
    get_commandArgument: function() {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.UI.CommandEventArgs.registerClass('Telerik.Sitefinity.UI.CommandEventArgs', Sys.CancelEventArgs);
