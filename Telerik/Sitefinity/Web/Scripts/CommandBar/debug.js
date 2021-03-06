Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* CommandBar class */

Telerik.Sitefinity.Web.UI.CommandBar = function (element) {
    Telerik.Sitefinity.Web.UI.CommandBar.initializeBase(this, [element]);

    this._commands = null;
    this._loadDelegate = null;
}
Telerik.Sitefinity.Web.UI.CommandBar.prototype = {
    // set up 
    initialize: function () {
        Telerik.Sitefinity.Web.UI.CommandBar.callBaseMethod(this, "initialize");
        this._commands = Sys.Serialization.JavaScriptSerializer.deserialize(this._commands);

        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.CommandBar.callBaseMethod(this, "dispose");
        // Clean up events
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
    },

    // EVENTS BINDING AND UNBINDING
    add_command: function (delegate) {
        this.get_events().addHandler('command', delegate);
    },
    remove_command: function (delegate) {
        this.get_events().removeHandler('command', delegate);
    },
    add_commanding: function (delegate) {
        this.get_events().addHandler('commanding', delegate);
    },
    remove_commanding: function (delegate) {
        this.get_events().removeHandler('commanding', delegate);
    },

    _load: function () {
        var _commandsCount = this._commands.length;
        var self = this;
        var commandButtonId = '';
        for (var i = 0; i < _commandsCount; i++) {
            commandButtonId = this._commands[i].ButtonClientId;
            switch (this._commands[i].ItemType) {
                case "ProvidersListToolboxItem":
                    var providersList = $find(commandButtonId);
                    providersList.add_providerNameChanged(Function.createDelegate(this, this._handleProvidersListChange));
                    break;
                case "MenuToolboxItem":
                    var menu = $find(commandButtonId);
                    menu.add_itemClicked(Function.createDelegate(this, this._menuItemClicked));
                    break;
                default:
                    $('#' + commandButtonId).click(function () {
                        var commandName = self._getCommandName($(this).attr('id'));
                        if (!self._onCommanding(commandName, null).get_cancel()) {
                            self._onCommand(commandName, null);
                        }
                    });
                    break;
            }
        }
    },

    // EVENTS BINDING AND UNBINDING
    _onCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('command');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _onCommanding: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('commanding');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _getCommandName: function (buttonId) {
        var commandsCount = this._commands.length;
        while (commandsCount--) {
            if (this._commands[commandsCount].ButtonClientId == buttonId) {
                return this._commands[commandsCount].CommandName;
            }
        }
        return null;
    },

    _menuItemClicked: function (sender, args) {
        var commandName = args.get_item().get_value();
        if (!(commandName == null || commandName.length == 0)) {

            this._onCommand(commandName, null);

            args.get_item().get_menu().close();
        }
    },

    _handleProvidersListChange: function (sender, args) {
        var providerName = args.get_newProviderName();
        var commandName = this._getCommandName(sender.get_element().id);
        if (!this._onCommanding(commandName, providerName).get_cancel()) {
            this._onCommand(commandName, providerName);
        }
    },

    // PUBLIC METHODS
    setItemText: function (commandName, text) {
        var item = this.findItemByCommandName(commandName);
        if (item) {
            var itemText = $get(item.TextClientId);
            itemText.innerHTML = text;
        }
    },

    findItemByCommandName: function (commandName) {
        var commandCount = this._commands.length;
        while (commandCount--) {
            if (this._commands[commandCount].CommandName == commandName) {
                return this._commands[commandCount];
            }
        }
        return null;
    }
};

Telerik.Sitefinity.Web.UI.CommandBar.registerClass('Telerik.Sitefinity.Web.UI.CommandBar', Sys.UI.Control);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs = function(commandName, commandArgument) {
	this._commandName = commandName;
	this._commandArgument = commandArgument;
	Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs.prototype = {
	// ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs.callBaseMethod(this, 'dispose');
    },   
	get_commandName: function() {
		return this._commandName;
	},
	get_commandArgument: function() {
		return this._commandArgument;
	}
};
Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs.registerClass('Telerik.Sitefinity.Web.UI.CommandBar.CommandEventArgs', Sys.CancelEventArgs);