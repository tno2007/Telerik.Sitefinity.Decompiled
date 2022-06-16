﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget.initializeBase(this, [element]);
    this._commandName = null;

    this._pageLoadDelegate = null;
    this._commandDelegate = null;

    this._stateButtonsIds = null;
    // ------------------ IWidget members -----------------------
    this._name = null;
    this._cssClass = null;
    this._isSeparator = null;
    this._wrapperTagId = null;
    this._wrapperTagName = null;
    this._toolbarControl = null;
    this._toolbarControlId = null;
    this._toolbarClickedDelegate = null;
    this._commandArgument = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget.callBaseMethod(this, 'initialize');

        if (this._commandDelegate === null) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        }

        if (this._toolbarClickedDelegate == null) {
            this._toolbarClickedDelegate = Function.createDelegate(this, this._toolbarClicked)
        }

        if (this._pageLoadDelegate === null) {
            this._pageLoadDelegate = Function.createDelegate(this, this._pageLoad);
        }

        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget.callBaseMethod(this, 'dispose');
        if (this._commandDelegate) {
            delete this._commandDelegate;
        }
        if (this._pageLoadDelegate) {
            delete this._pageLoadDelegate;
        }
    },

    /* ------------------------------------ Private functions --------------------------------- */

    _pageLoad: function () {
        if (this._toolbarControlId) {
            this._toolbarControl = $find(this._toolbarControlId);
            if (this._toolbarControl) {
                this._toolbarControl.add_buttonClicking(this._toolbarClickedDelegate);
            }
        }
    },

    _commandHandler: function (sender, args) {
        if (!args) {
            args = null;
        }
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(this.get_commandName(), args);
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
    },

    _toolbarClicked: function (sender, args) {
        this._commandName = "stateButtonClicked";
        this._commandArgument = args.get_item().get_value();
        this._commandHandler(this, this._commandArgument);
    },

    /* ------------------------------------ Public Methods ----------------------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
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

    set_toolbarControlId: function (value) {
        if (this._toolbarControlId != value) {
            this._toolbarControlId = value;
        }
    },

    get_toolbarControlId: function () {
        return this._toolbarControlId;

    },

    get_name: function () {
        return this._name;
    },
    set_name: function (value) {
        if (this._name != value) {
            this._name = value;
            this.raisePropertyChanged('name');
        }
    },
    get_cssClass: function () {
        return this._cssClass;
    },
    set_cssClass: function (value) {
        if (this._cssClass != value) {
            this._cssClass = value;
            this.raisePropertyChanged('cssClass');
        }
    },

    get_isSeparator: function () {
        return this._isSeparator;
    },
    set_isSeparator: function (value) {
        if (this._isSeparator != value) {
            this._isSeparator = value;
            this.raisePropertyChanged('isSeparator');
        }
    },

    get_wrapperTagId: function () {
        return this._wrapperTagId;
    },
    set_wrapperTagId: function (value) {
        if (this._wrapperTagId != value) {
            this._wrapperTagId = value;
            this.raisePropertyChanged('wrapperTagId');
        }
    },

    get_wrapperTagName: function () {
        return this._wrapperTagName;
    },
    set_wrapperTagName: function (value) {
        if (this._wrapperTagName != value) {
            this._wrapperTagName = value;
            this.raisePropertyChanged('wrapperTagName');
        }
    },

    get_stateButtonsIds: function () {
        return this._stateButtonsIds;
    },
    set_stateButtonsIds: function (value) {
        if (this._stateButtonsIds != value) {
            this._stateButtonsIds = value;
            this.raisePropertyChanged('stateButtonsIds');
        }
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget", Sys.UI.Control, Telerik.Sitefinity.UI.ICommandWidget, Telerik.Sitefinity.UI.IWidget);