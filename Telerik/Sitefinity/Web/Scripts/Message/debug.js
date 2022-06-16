Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// Constructor
Telerik.Sitefinity.Web.UI.Message = function (element) {

    Telerik.Sitefinity.Web.UI.Message.initializeBase(this, [element]);

    this._pageDelegate = null;
    this._messageLoadDelegate = null;
    this._removeAfterDelegate = null;

    this._startPositiveColor = null;
    this._endPositiveColor = null;
    this._startNeutralColor = null;
    this._endNeutralColor = null;
    this._startNegativeColor = null;
    this._endNegativeColor = null;
    this._startFadeColor = null;
    this._endFadeColor = null;

    this._fadeDuration = null;
    this._removeAfter = null;
    this._messageText = null;
    this._messageType = null;
    this._clientId = null;
    this._timerId = null;
    this._animate = false;
    this._commandButtons = [];

    //constants
    this._positiveType = "Positive";
    this._neutralType = "Neutral";
    this._negativeType = "Negative";
}

Telerik.Sitefinity.Web.UI.Message.prototype = {

    initialize: function () {
        if (this._messageLoadDelegate === null) {
            this._messageLoadDelegate = Function.createDelegate(this, this._messageLoadHandler);
        }
        this._pageDelegate = Function.createDelegate(this, this._pageReady);
        Sys.Application.add_load(this._pageDelegate);

        this._removeAfterDelegate = Function.createDelegate(this, this._removeMessageHandler);
        this._configurableCommandDelegate = Function.createDelegate(this, this._confugurableCommandHandler);
        if (this._commandButtons) {
            this._commandButtons = Sys.Serialization.JavaScriptSerializer.deserialize(this._commandButtons);
        }
        this._renderCommandButtons(this._commandButtons);

        Telerik.Sitefinity.Web.UI.Message.callBaseMethod(this, 'initialize');
    },

    // Release resources before control is disposed.
    dispose: function () {

        if (this._messageLoadDelegate) {
            delete this._messageLoadDelegate;
        }

        if (this._pageDelegate) {
            Sys.Application.remove_load(this._pageDelegate);
            delete this._pageDelegate;
        }

        if (this._removeAfterDelegate) {
            if (this._timerId != null) {
                clearTimeout(this._timerId);
                this._timerId = null;
            }
            delete this._removeAfterDelegate;
        }

        if (this._configurableCommandDelegate) {
            delete this._configurableCommandDelegate;
        }

        Telerik.Sitefinity.Web.UI.Message.callBaseMethod(this, 'dispose');
    },

    /* ************** public methods **************** */

    // show positive message
    showPositiveMessage: function (message) {
        this._messageText = message;
        this._messageType = this._positiveType;
        this._startFadeAnimation();
    },

    showNeutralMessage: function (message, commands) {
        this._messageText = message;
        this._messageType = this._neutralType;

        $(this.get_element()).empty();

        // Show the explicitly specified commands or the predefined ones.
        var commandsToShow = commands || this.get_commandButtons();
        this._renderCommandButtons(commandsToShow);

        this._startFadeAnimation();
    },

    showNegativeMessage: function (message) {
        this._messageText = message;
        this._messageType = this._negativeType;
        this._startFadeAnimation();
    },

    hide: function () {
        this.getJControl().css("display", "none");
        this.getJControl().addClass("sfMsgHidden").removeClass("sfMsgVisible");
    },
    getJControl: function () {
        if (this._jControl == null) {
            this._jControl = jQuery(this._clientId);
        }
        return this._jControl;
    },

    add_onClientLoad: function (delegate) {
        this.get_events().addHandler('onClientLoad', delegate);
    },
    remove_onClientLoad: function (delegate) {
        this.get_events().removeHandler('onClientLoad', delegate);
    },

    add_command: function (delegate) {
        this.get_events().addHandler('onCommand', delegate);
    },
    remove_command: function (delegate) {
        this.get_events().removeHandler('onCommand', delegate);
    },

    /* ************** events **************** */

    /* ************** event handlers **************** */

    _renderCommandButtons: function (commandButtons) {
        var itemElement = this.get_element();
        var length = commandButtons.length;
        for (var i = 0; i < length; i++) {
            var customCommand = commandButtons[i];
            if (customCommand.CommandName != null && customCommand.CommandName != "") {
                var anchorElement = this._generateAnchorElement(customCommand);
                if (anchorElement) {
                    $(itemElement).append(anchorElement);
                }
            }
        }
        if(length > 0)
            $(itemElement).show();
    },

    _commandHandler: function (commandName) {
        var eventArgs = new Telerik.Sitefinity.CommandEventArgs(commandName, null);
        var h = this.get_events().getHandler('onCommand');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    // Handles the command fired by a rendered anchor from custom command
    _confugurableCommandHandler: function (sender, args) {
        var commandName = $(sender.target.parentNode).attr('rel');
        this._commandHandler(commandName);
    },

    _pageReady: function () {
        this._clientId = "#" + this.get_element().id;
        this._startFadeAnimation();
        this._messageLoadHandler();
    },

    _messageLoadHandler: function () {
        var eventArgs = null;
        var h = this.get_events().getHandler('onClientLoad');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    _removeMessageHandler: function () {
        this.getJControl().css("display", "none");
        this.getJControl().addClass("sfMsgHidden").removeClass("sfMsgVisible");
    },

    /* ************** private methods **************** */

    _startFadeAnimation: function () {
        if (this._messageText != null && this._messageText.length > 0) {
            if (this._positiveType == this._messageType) {
                this._startFadeColor = this.get_startPositiveColor();
                this._endFadeColor = this.get_endPositiveColor();
                this.getJControl().addClass("sfMsgPositive").removeClass("sfMsgNeutral sfMsgNegative");
            }
            else if (this._neutralType == this._messageType) {
                this._startFadeColor = this.get_startNeutralColor();
                this._endFadeColor = this.get_endNeutralColor();
                this.getJControl().addClass("sfMsgNeutral").removeClass("sfMsgPositive sfMsgNegative");
            }
            else {
                this._startFadeColor = this.get_startNegativeColor();
                this._endFadeColor = this.get_endNegativeColor();
                this.getJControl().addClass("sfMsgNegative").removeClass("sfMsgNeutral sfMsgPositive");
            }

            this.getJControl().addClass("sfMsgVisible").removeClass("sfMsgHidden");

            if (this._timerId != null) {
                clearTimeout(this._timerId);
                this._timerId = null;
            }

            //Create the new message element
            var messageElement = document.createElement("span");
            $(messageElement).addClass("sfMessageElement");
            $(messageElement).html(this.get_messageText());

            //Remove the old element and insert the new one
            $(this.get_element()).find(".sfMessageElement").remove();
            $(this.get_element()).prepend(messageElement);

            this.getJControl()
            .css({ backgroundColor: this._startFadeColor })
            .show();

            if (this._animate) {
                this.getJControl()
                .stop()
                .stop()
                .animate({ backgroundColor: this._endFadeColor }, this._fadeDuration / 2)
                .animate({ backgroundColor: this._startFadeColor }, this._fadeDuration / 2);
            }

            if (this._removeAfter > 0) {
                this._timerId = setTimeout(this._removeAfterDelegate, this._removeAfter);
            }
            else if (this._removeAfter == 0) {
                this.getJControl().css("display", "none");
                this.getJControl().addClass("sfMsgHidden").removeClass("sfMsgVisible");
            }
        }
    },


    // Generates a anchor element representing a link that will fire custom command,
    // that is configuration object received from the server
    _generateAnchorElement: function (configurableCommand) {
        var anchorElement = document.createElement('a');
        $(anchorElement).addClass(configurableCommand.CssClass);
        $(anchorElement).attr('rel', configurableCommand.CommandName);
        $(anchorElement).attr("href", "javascript:void(0)");
        $addHandler(anchorElement, 'click', this._configurableCommandDelegate);

        var spanElement = document.createElement("span");
        $(spanElement).text(configurableCommand.Title);

        $(anchorElement).append(spanElement);        

        return anchorElement;
    },

    /* ************** properties ********************* */

    set_startPositiveColor: function (value) {
        this._startPositiveColor = value;
    },

    get_startPositiveColor: function () {
        return this._startPositiveColor;
    },

    get_animate: function () {
        return this._animate;
    },

    set_animate: function (value) {
        this._animate = value;
    },

    set_endPositiveColor: function (value) {
        this._endPositiveColor = value;
    },

    get_endPositiveColor: function () {
        return this._endPositiveColor;
    },

    set_startNeutralColor: function (value) {
        this._startNeutralColor = value;
    },

    get_startNeutralColor: function () {
        return this._startNeutralColor;
    },

    set_endNeutralColor: function (value) {
        this._endNeutralColor = value;
    },

    get_endNeutralColor: function () {
        return this._endNeutralColor;
    },

    set_startNegativeColor: function (value) {
        this._startNegativeColor = value;
    },

    get_startNegativeColor: function () {
        return this._startNegativeColor;
    },

    set_endNegativeColor: function (value) {
        this._endNegativeColor = value;
    },

    get_endNegativeColor: function () {
        return this._endNegativeColor;
    },

    set_fadeDuration: function (value) {
        this._fadeDuration = value;
    },

    get_fadeDuration: function () {
        return this._fadeDuration;
    },

    set_removeAfter: function (value) {
        this._removeAfter = value;
    },

    get_removeAfter: function () {
        return this._removeAfter;
    },

    set_messageText: function (value) {
        this._messageText = value;
    },

    get_messageText: function () {
        return this._messageText;
    },

    set_commandButtons: function (value) {
        this._commandButtons = value;
        this._renderCommandButtons(this._commandButtons);
    },

    get_commandButtons: function () {
        return this._commandButtons;
    }
}

Telerik.Sitefinity.Web.UI.Message.registerClass('Telerik.Sitefinity.Web.UI.Message', Sys.UI.Control);

