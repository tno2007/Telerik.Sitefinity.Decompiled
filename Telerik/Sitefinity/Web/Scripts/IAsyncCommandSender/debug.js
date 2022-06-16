// If this interface is implemented the sender can invoke commands asynchronously
// and then expect that at the end of the async. command the endProcessingHandler will be called
Type._registerScript("IAsyncCommandSender.js");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ClientBinders");

Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandSender = function() { }

Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandSender.prototype = {

    // Adds command event listener. Called when a new command event is fired.
    add_command: function(handler) { },

    // Removes command event listener
    remove_command: function(handler) { },

    // Returns a handler that is to be invoked after the async. request is finished
    _endProcessingHandler: function() { }


};

Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandSender.registerInterface("Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandSender");
