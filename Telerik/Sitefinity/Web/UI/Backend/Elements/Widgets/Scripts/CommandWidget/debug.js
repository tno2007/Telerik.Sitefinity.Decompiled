Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget.initializeBase(this, [element]);
    this._commandDelegate = null;
    this._keyPressCommandDelegate = null;

    this._jLink = null;
    this._linkClientId = null;
    this.enabled = null;
    this._clickable = null;

    this._disabledCssClass = null;

    this._commandName = null;
    this._commandArgument = null;
    this._isFilterCommand = false;
    this._buttonTextElem = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget.callBaseMethod(this, 'initialize');

        if (this._commandDelegate === null) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        }

        this._keyPressCommandDelegate = Function.createDelegate(this, this._keyPressCommandHandler);


        // subscribe to the click event of the button
        if (this._clickable) {
            this._jLink = jQuery(this._escapeMetaCharactersFromId(this._linkClientId));
            this._jLink.bind("click", this._commandDelegate);
            this._jLink.bind("keypress", this._keyPressCommandDelegate);
        }

        if (this._commandArgument)
            this._commandArgument = Sys.Serialization.JavaScriptSerializer.deserialize(this._commandArgument);
        this._setEnabledStyle();
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget.callBaseMethod(this, 'dispose');
        if (this._commandDelegate) {
            if (this._jLink) {
                this._jLink.unbind("click", this._commandDelegate);
            }
            delete this._commandDelegate;
        }
        if (this._keyPressCommandDelegate) {
            if (this._jLink) {
                this._jLink.unbind("keypress", this._keyPressCommandDelegate);
            }
            delete this._keyPressCommandDelegate;
        }
        this._buttonTextElem = null;
    },
    /* ------------------------------------ Private functions --------------------------------- */

    _commandHandler: function (sender, args) {
        if (this.get_enabled()) {
            var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(this.get_commandName(), this.get_commandArgument());
            commandEventArgs.isFilterCommand = this.get_isFilterCommand();
            var h = this.get_events().getHandler('command');
            if (h) h(this, commandEventArgs);
        }
    },

    _keyPressCommandHandler: function (e) {
        if (e.keyCode == Sys.UI.Key.enter) {
            e.preventDefault();
            e.stopPropagation();
            this.get_commandArgument().enterKey = true;
            this._commandHandler(this);
        }
    },
    _setEnabledStyle: function () {
        if (this._jLink) {
            if (this.get_enabled()) {
                this._jLink.removeClass(this._disabledCssClass);
            }
            else {
                this._jLink.addClass(this._disabledCssClass);
            }
        }
    },
    
    //The colon (":") and period (".") are problematic within the context of a jQuery selector because they indicate a pseudo-class and class, respectively.
    //In order to tell jQuery to treat these characters literally rather than as CSS notation, they must be "escaped" by placing two backslashes in front of them.
    _escapeMetaCharactersFromId: function (id) {
        if (id != "undefined" && id != null)
            return "#" + id.replace(/(:|\.|\[|\]|,)/g, "\\$1");
    },
 
    /* ------------------------------------ Public Methods ----------------------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    enable: function () {
        this.set_enabled(true);
    },
    disable: function () {
        this.set_enabled(false);
    },

    /* ------------------------------------- Properties --------------------------------------- */
    get_commandName: function () {
        return this._commandName;
    },

    set_commandName: function (value) {
        if (this._commandName != value) {
            this._commandName = value;
            this.raisePropertyChanged('commandName');
        }
    },

    get_commandArgument: function () {
        return this._commandArgument;
    },

    set_commandArgument: function (value) {
        if (this._commandArgument != value) {
            this._commandArgument = value;
            this.raisePropertyChanged('commandArgument');
        }
    },

    get_linkElement: function () {
        //TODO: The method name should specify that it returns a jQuery instance.
        return this._jLink;
    },

    // Gets if the widget is enabled
    get_enabled: function () {
        return this.enabled;
    },
    // Sets if the widget is enabled
    set_enabled: function (value) {
        this.enabled = value;
        this._setEnabledStyle();
    },
    get_isFilterCommand: function () {
        return this._isFilterCommand;
    },
    set_isFilterCommand: function (val) {
        if (val != this._isFilterCommand) {
            this._isFilterCommand = val;
            this.raisePropertyChanged("isFilterCommand");
        }
    },
    get_buttonText: function () {
        if (typeof this._buttonTextElem != "undefined" && this._buttonTextElem != null) {
            return jQuery(this._buttonTextElem).text();
        }
    },
    set_buttonText: function (val) {
        if (typeof this._buttonTextElem != "undefined" && this._buttonTextElem != null) {
            jQuery(this._buttonTextElem).text(val);
        }
    },
    get_buttonTextElem: function () {
        return this._buttonTextElem;
    },
    set_buttonTextElem: function (val) {
        this._buttonTextElem = val;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget", Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget, Telerik.Sitefinity.UI.ICommandWidget);