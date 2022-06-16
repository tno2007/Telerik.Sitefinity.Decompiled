//Type._registerScript("IAsyncCommandSender.js", ["IAsyncCommandSender.js"]);

Type.registerNamespace("Telerik.Sitefinity.Web.UI.ClientBinders");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");

Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar = function(element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar.prototype = {

    initialize: function() {
    Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar.callBaseMethod(this, 'initialize');
    
        this._startProcessingDelegate = Function.createDelegate(this, this._startProcessingHandler);
        this._endProcessingDelegate = Function.createDelegate(this, this._endProcessingHandler);
        
        //attaches startProcessing handler to 'command' event
        this.add_command(this._startProcessingDelegate);

    },

    dispose: function() {
        Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar.callBaseMethod(this, 'dispose');

        this.remove_command(this._startProcessingDelegate);

        if (this._startProcessingDelegate) {
            delete this._startProcessingDelegate;
        }

        if (this._endProcessingDelegate) {
            delete this._endProcessingDelegate;
        }
    },

    /* -------------------------- Public Methods ---------------------------------------- */

    // This function allows other objects to subscribe to the onStartProcessing event of the button bar control.
    add_onStartProcessing: function(handler) {
        this.get_events().addHandler('onStartProcessing', handler);
    },
  
    // This function allows other objects to subscribe to the onEndProcessing event of the button bar control.
    add_onEndProcessing: function(handler) {
        this.get_events().addHandler('onEndProcessing', handler);
    },
    
    // This function allows other objects to unsubscribe to the onStartProcessing event of the button bar control.
    remove_onStartProcessing: function(handler) {
        this.get_events().removeHandler('onStartProcessing', handler);
    },

    // This function allows other objects to unsubscribe to the onEndProcessing event of the button bar control.
    remove_onEndProcessing: function(handler) {
        this.get_events().removeHandler('onEndProcessing', handler);
    },
    
    /* -------------------------- Private functions ---------------------------------- */

    // This function will rise onStartProcessing event.
    _startProcessingHandler: function(sender, args) {

       var commandName = args.get_commandName();
       var commandEventArgs = this._getCommandEventArgs(commandName, null);

        var h = this.get_events().getHandler('onStartProcessing');
        if (h) h(this, commandEventArgs);
    },

    // This function will rise onEndProcessing event.
    _endProcessingHandler: function(sender, args) {
        //var commandName = args.get_commandName();
        //var commandEventArgs = this._getCommandEventArgs(commandName, null);
        var h = this.get_events().getHandler('onEndProcessing');
        if (h) h(this, args);
    },

    _getCommandEventArgs: function(commandName, commandArgument) {
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(commandName, commandArgument);
        return commandEventArgs;
    }

    /* -------------------------- Properties ---------------------------------------- */
}

Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.ButtonBar", Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar, Telerik.Sitefinity.Web.UI.ClientBinders.IAsyncCommandSender);
