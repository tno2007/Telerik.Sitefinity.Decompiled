Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* PromptDialog class */

Telerik.Sitefinity.Web.UI.PromptDialog = function (element) {
    this._commands = null;
    this._defaultInputText = null;
    this._textFieldExample = null;
    this._textFieldTitle = null;
    this._title = null;
    this._showOnLoad = null;
    this._wrapperElement = null;
    this._inputTextField = null;
    this._allowCloseButton = null;
    this._closeButtonElement = null;
    this._promptMessageElement = null;
    this._promptTitleElement = null;
    this._message = null;
    this._height = null;
    this._width = null;
    this._promptInnerContentElement = null;
    this._lastHandler = null;
    this._initialMessage = null;
    this._promptMode = null;
    this._checkRelatingDataCheckBox = null;

    Telerik.Sitefinity.Web.UI.PromptDialog.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.PromptDialog.prototype = {
    // set up 
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PromptDialog.callBaseMethod(this, "initialize");
        this._commands = Sys.Serialization.JavaScriptSerializer.deserialize(this._commands);

        if (this._showOnLoad == true) {
            this._positionOverlay();
            this.show_prompt(this.get_title(), this.get_message(), null);
        }

        if (this._defaultInputText) {
            this.set_defaultInputText(this._defaultInputText);
        }

        if (this._allowCloseButton != null) {
            this.set_allowCloseButton(this._allowCloseButton);
        }

        if (this._message) {
            this.set_message(this._message);
            //keeps initial message template(needed at cases when message is formated, but need to be reused)
            this.set_initialMessage(this._message);
        }

        Sys.Application.add_load(Function.createDelegate(this, this._load));
    },


    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.PromptDialog.callBaseMethod(this, "dispose");
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

            var menu = $find(commandButtonId);

            // menu.add_itemClicked(Function.createDelegate(this, this._menuItemClicked));
            $('#' + commandButtonId).click(Function.createDelegate(this, this._handlePredefinedCommands));

        }

        if (this._width) {
            this.set_width(this._width);
        }
    },


    // EVENTS BINDING AND UNBINDING
    _onCommand: function (commandName, commandArgument, checkRelatingData) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs(commandName, commandArgument, checkRelatingData);
        var handler = this.get_events().getHandler('command');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _onCommanding: function (commandName, commandArgument, checkRelatingData) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs(commandName, commandArgument, checkRelatingData);
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

    _handleProvidersListChange: function (sender, args) {
        var providerName = args.get_newProviderName();
        var commandName = this._getCommandName(sender.get_element().id);

        var checkRelatingData = false;

        if (this.get_checkRelatingDataCheckBox()) {
            checkRelatingData = this.get_checkRelatingDataCheckBox().checked
        }

        if (!this._onCommanding(commandName, providerName, checkRelatingData).get_cancel()) {
            this._onCommand(commandName, providerName, checkRelatingData);
        }
    },

    _handlePredefinedCommands: function (sender) {
        var commandName = this._getCommandName(sender.currentTarget.id);
        var isValid = true;
        for (var i = 0, len = this._commands.length; i < len; i++) {
            if (this._commands[i].CommandName == commandName &&
                this._commands[i].CausesValidation == true &&
                this.get_inputTextField().validate() == false) {
                isValid = false;
                break;
            }
        }
        var commandArgument = this._lastContext;
        if (this._promptMode == 1) {
            // mode is Input. Pass the text from the TextField as a command argument
            commandArgument = this.get_inputTextField().get_value();
        }
        if (isValid) {
            this._hidePrompt();
            var checkRelatingData = false;

            if (this.get_checkRelatingDataCheckBox()) {
                checkRelatingData = this.get_checkRelatingDataCheckBox().checked
            }

            this._raiseCommand(commandName, commandArgument, checkRelatingData);
        }
        return false; // fix for IE showing the warning for leaving the page
    },

    _raiseCommand: function (commandName, args, checkRelatingData) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs(commandName, args, checkRelatingData);
        var handler = this.get_events().getHandler('command');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _docHeight: function () {
        var D = document;

        if ((D == null) || (D == 'undefined')) return false;

        return Math.max(
            Math.max(D.body.scrollHeight, D.documentElement.scrollHeight),
            Math.max(D.body.offsetHeight, D.documentElement.offsetHeight),
            Math.max(D.body.clientHeight, D.documentElement.clientHeight)
        );

    },

    _docWidth: function () {
        var D = document;

        if ((D == null) || (D == 'undefined')) return false;

        return Math.max(
            Math.max(D.body.scrollWidth, D.documentElement.scrollWidth),
            Math.max(D.body.offsetWidth, D.documentElement.offsetWidth),
            Math.max(D.body.clientWidth, D.documentElement.clientWidth)
        );

    },

    _positionOverlay: function () {
        if (!$get('sfPromptDialogOverlayWrapper')) {
            var containerElement = document.getElementsByTagName("body")[0];
            var documentHeight = containerElement.clientHeight;
            var documentWidth = containerElement.clientWidth;
            var overlay = document.createElement('div');
            overlay.className = 'sfPromptDialogOverlay';
            overlay.id = 'sfPromptDialogOverlayWrapper';
            overlay.style.height = this._docHeight() + 'px';
            overlay.style.width = this._docWidth() + 'px';
            containerElement.appendChild(overlay);
        } else {
            $get('sfPromptDialogOverlayWrapper').style.display = "block";
        }
    },

    _recalculateOverlay: function () {
        var element = $get('sfPromptDialogOverlayWrapper');
        if (element) {
            element.style.height = this._docHeight() + 'px';
            element.style.width = this._docWidth() + 'px';
        }
    },

    _hidePrompt: function () {
        var overlay = $get('sfPromptDialogOverlayWrapper');
        var promptWindow = this.get_wrapperElement();
        overlay.style.display = "none";
        promptWindow.style.display = "none";
    },

    _hideOverlay: function() {
        var overlay = $get('sfPromptDialogOverlayWrapper');
        overlay.style.display = "none";
    },

    _changeCheckBoxText: function (checkboxId, text) {
        var allLabel = document.getElementsByTagName("label");
        for (i = 0; i < allLabel.length; i++) {

            if (allLabel[i].htmlFor == checkboxId) {
                allLabel[i].innerHTML = text;
                break;
            }
        }
    },

    /* PUBLIC METHODS */

    // Visualizes the PromptDialog on the screen absolutely centered.
    // All arguments are optional and disregarded if null; The third argument 'handler' should be a function with two arguments, e.g. myHandler(sender, args) 
    show_prompt: function (title, message, handler, context, checkRelatingDataText) {

        var newParent = document.getElementsByTagName('form')[0];
        if (!newParent) {
            if (document.forms && document.forms.length > 0) newParent = document.forms[0];
            else newParent = document.body;
        }

        var promptWindow = this.get_wrapperElement();

        if (title) this.set_title(title);
        if (message) this.set_message(message);

        if (this._lastHandler) this.remove_command(this._lastHandler);
        if (handler) {
            this.add_command(handler);
            this._lastHandler = handler;
        }

        this._lastContext = context;

        if (this.get_title() == "") {
            this.get_promptTitleElement().style.display = 'none';
        }

        if (this.get_message() == "") {
            this.get_promptMessageElement().style.display = 'none';
        }

        if (this.get_checkRelatingDataCheckBox() && checkRelatingDataText)
            this._changeCheckBoxText(this.get_checkRelatingDataCheckBox().id, checkRelatingDataText)

        newParent.insertBefore(promptWindow, newParent.firstChild);

        if (!$get('sfPromptDialogOverlayWrapper')) this._positionOverlay();
        promptWindow.style.display = "block";
        promptWindow.style.left = "50%";
        promptWindow.style.marginLeft = promptWindow.offsetWidth / 2 * -1 + 'px';
        promptWindow.style.display = "block";
        $get('sfPromptDialogOverlayWrapper').style.display = "block";
    },

    // Returns the width of the window 
    get_width: function () {
        var promptWindow = this.get_wrapperElement();
        promptWindow.offsetWidth;
    },

    // Sets the width of the window
    set_width: function (value) {
        this._width = value;
        var promptWindow = this.get_wrapperElement();
        if (promptWindow) {
            promptWindow.style.width = value + 'px';
            promptWindow.style.marginLeft = (value / (-2)) + 'px';
        }
    },

    // Returns the height of the window
    get_height: function () {
        var promptWindow = this.get_wrapperElement();
        promptWindow.offsetHeight;
    },

    // Sets the height of the window
    set_height: function (value) {
        this._height = value;
        var promptWindow = this.get_wrapperElement();
        if (promptWindow) {
            promptWindow.style.height = value + 'px';
            promptWindow.style.marginTop = (value / (-2)) + 'px';
        }
    },

    // Returns the most outer window html element
    get_wrapperElement: function () {
        return this._wrapperElement;
    },

    // Sets the most outer window html element
    set_wrapperElement: function (value) {
        if (this._wrapperElement != value) {
            this._wrapperElement = value;
            this.raisePropertyChanged('wrapperElement');
        }
    },

    // Returns the content html element that contains the TextField and the Message
    get_promptInnerContentElement: function () {
        return this._promptInnerContentElement;
    },

    // Sets the content html element that contains the TextField and the Message
    set_promptInnerContentElement: function (value) {
        if (this._promptInnerContentElement != value) {
            this._promptInnerContentElement = value;
            this.raisePropertyChanged('promptInnerContentElement');
        }
    },

    // Returns the close button html element
    get_closeButtonElement: function () {
        return this._closeButtonElement;
    },

    // Sets the close button html element
    set_closeButtonElement: function (value) {
        if (this._closeButtonElement != value) {
            this._closeButtonElement = value;
            this.raisePropertyChanged('closeButtonElement');
        }
    },

    // Returns the html element that contains the Message text only
    get_promptMessageElement: function () {
        return this._promptMessageElement;
    },

    // Sets the html element that contains the Message text only
    set_promptMessageElement: function (value) {
        if (this._promptMessageElement != value) {
            this._promptMessageElement = value;
            this.raisePropertyChanged('promptMessageElement');
        }
    },

    // Returns the html element of the title text
    get_promptTitleElement: function () {
        return this._promptTitleElement;
    },

    // Sets the html element of the title text
    set_promptTitleElement: function (value) {
        if (this._promptTitleElement != value) {
            this._promptTitleElement = value;
            this.raisePropertyChanged('promptTitleElement');
        }
    },

    // Sets title text
    set_title: function (value) {
        this.get_promptTitleElement().innerHTML = value;
    },

    // Gets title text
    get_title: function () {
        return this.get_promptTitleElement().innerHTML;
    },

    // Sets message text
    set_message: function (value) {
        this.get_promptMessageElement().innerHTML = value;
    },

    // Gets message text
    get_message: function () {
        return this.get_promptMessageElement().innerHTML;
    },

    // Sets initial message text(used to hold unchanged message)
    set_initialMessage: function (value) {
        this._initialMessage = value;
    },

    // Gets initial message text(used to hold unchanged message)
    get_initialMessage: function () {
        return this._initialMessage;
    },

    // Gets the input TextField component
    get_inputTextField: function () {
        return this._inputTextField;
    },

    // Sets the input TextField component
    set_inputTextField: function (value) {
        if (this._inputTextField != value) {
            this._inputTextField = value;
            this.raisePropertyChanged('inputTextField');
        }
    },

    // Gets the default input text from the TextField
    get_defaultInputText: function () {
        return this._inputTextField;
    },

    // Sets the default input text in the TextField
    set_defaultInputText: function (value) {
        this._inputTextField.set_value(value);
    },

    // Gets the value of the property AllowCloseButton (true/false)
    get_allowCloseButton: function () {
        return this._allowCloseButton;
    },

    // Sets the value of the property AllowCloseButton (true/false)
    set_allowCloseButton: function (value) {
        if (value == true) {
            var self = this;
            this._closeButtonElement.onclick = function () {
                self._hidePrompt();
            }
        } else {
            this._closeButtonElement.className += ' sfCloseDisabled';
        }
    },

    // Gets the value of the input TextField
    get_inputText: function () {
        return this.get_inputTextField().get_value();
    },

    // Sets the value of the input TextField
    set_inputText: function (value) {
        this.get_inputTextField().set_value(value);
    },

    // Sets the title of the input TextField
    set_textFieldTitle: function (value) {
        this.get_inputTextField().set_title(value);
    },

    // Gets the title of the input TextField
    get_textFieldTitle: function () {
        this.get_inputTextField().get_title();
    },

    // Sets the example text beneath the TextField control
    set_textFieldExample: function (value) {
        this.get_inputTextField().set_example(value);
    },

    // Gets the example text beneath the TextField control
    get_textFieldExample: function () {
        this.get_inputTextField().get_example();
    },

    get_checkRelatingDataCheckBox: function () {
        return this._checkRelatingDataCheckBox;
    },

    set_checkRelatingDataCheckBox: function (value) {
        this._checkRelatingDataCheckBox = value;
    },

    setButtonDisplay: function (commandName, bShow) {
        for (var i = 0; i < this._commands.length; i++) {
            if (this._commands[i].CommandName == commandName) {
                if (bShow)
                    $($get(this._commands[i].ButtonClientId)).show();
                else
                    $($get(this._commands[i].ButtonClientId)).hide();
            }
        }
    }

};

Telerik.Sitefinity.Web.UI.PromptDialog.registerClass('Telerik.Sitefinity.Web.UI.PromptDialog', Sys.UI.Control);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs = function (commandName, commandArgument, checkRelatingData) {
    this._commandName = commandName;
    this._checkRelatingData = checkRelatingData;
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    },
    get_checkRelatingData: function () {
        return this._checkRelatingData;
    },

};
Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs.registerClass('Telerik.Sitefinity.Web.UI.PromptDialog.CommandEventArgs', Sys.CancelEventArgs);
