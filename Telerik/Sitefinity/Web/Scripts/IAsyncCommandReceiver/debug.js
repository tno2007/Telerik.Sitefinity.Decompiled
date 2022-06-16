// If this interface is implemented the receiver will be invoked asynchronously
// and at the end of the asynchronious command will be called endProcessingHandler on the corresponding sender.
Type._registerScript("IAsyncCommandReceiver.js");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ClientBinders");

Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandReceiver = function() { }
Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandReceiver.prototype = {

    // Adds a handler that is to be invoked before execution of asynchronous command
    add_onStartProcessing: function() { },

    // Removes the star processing handler
    remove_onStartProcessing: function() { },

    // Adds a handler that is invoked when an asynchronous command execution is complete
    add_onEndProcessing: function() { },

    // Removes the end processing handler
    remove_onEndProcessing: function() { },

    // This methods fires the command on the receiver
    _onCommand: function(commandName, commandArgument) { }

};

Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandReceiver.registerInterface("Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandReceiver");
