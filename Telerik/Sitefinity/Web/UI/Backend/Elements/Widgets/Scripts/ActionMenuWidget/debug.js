//Type._registerScript("ICommandWidget.js", ["ICommandWidget.js"]);
//Type._registerScript("IWidget.js", ["IWidget.js"]);

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget.initializeBase(this, [element]);
    this._menu = null;
    this._commandDelegate = null;
    this._menuClientId = null;
    this._spanClientId = null;
    this._enabled = null;
    // ------------------ IWidget members -----------------------
    this._name = null;
    this._cssClass = null;
    this._isSeparator = null;
    this._wrapperTagId = null;
    this._wrapperTagKey = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget.prototype = {

    initialize: function () {

        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget.callBaseMethod(this, 'initialize');
        // subscribe to load event to load widgets there
        Sys.Application.add_load(Function.createDelegate(this, this._loadHandler));

        if (this._commandDelegate === null) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget.callBaseMethod(this, 'dispose');
        if (this._commandDelegate) {
            if (this._menu) {
                this._menu.remove_itemClicked(this._commandDelegate);
            }
            delete this._commandDelegate;
        }
    },

    /* ------------------------------------ Private functions --------------------------------- */
    _commandHandler: function (sender, args) {
        var commandName = args.get_item().get_value();
        if (commandName !== null) {
            var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(commandName, null);

            //commandEventArgs.set_cancel(true);
            //close the manu, normally the menu does not close
            //sender.close();
            //fix problem with not closing menu in IE
            setTimeout(function () { sender.close(); }, 10);

            var h = this.get_events().getHandler('command');
            if (h) h(this, commandEventArgs);
        }
    },

    _loadHandler: function () {
        this._menu = $find(this._menuClientId);
        this._menu.add_itemClicked(this._commandDelegate);
    },

    /* ------------------------------------ Public Methods ----------------------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },


    // Hides menu item by specified name
    hideWidgetByName: function (name) {
        var item = this._menu.findItemByValue(name);
        if (item) {
            item.hide();
            $(item.get_element()).addClass("sfDisplayNoneImportant");
        }
        else {
            throw Error.argument("name", "No item found with the specified value.");
        }
    },

    // Shows menu item by specified name
    showWidgetByName: function (name) {
        var item = this._menu.findItemByValue(name);
        if (item) {
            item.show();
            $(item.get_element()).removeClass("sfDisplayNoneImportant");
        }
        else {
            throw Error.argument("name", "No item found with the specified value.");
        }
    },
    // Hides the seperators which has no items in their sections
    hideEmptySections: function () {
        var allItems = this._menu.get_allItems();

        var lastSeparator = null;
        var hasVisible = false;

        for (var i = 0, length = allItems.length; i < length; i++) {
            var item = allItems[i];
            if (item.get_isSeparator()) {
                if (!hasVisible) {
                    lastSeparator.hide();
                }
                lastSeparator = item;
                hasVisible = false;
            }
            else if (!hasVisible && item.get_visible()) {
                hasVisible = true;
            }
        }

        if (!hasVisible && lastSeparator) {
            lastSeparator.hide();
        }
    },

    /* ----------------------------------- Properties -------------------------------------------*/

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

    get_wrapperTagKey: function () {
        return this._wrapperTagKey;
    },
    set_wrapperTagKey: function (value) {
        if (this._wrapperTagKey != value) {
            this._wrapperTagKey = value;
            this.raisePropertyChanged('wrapperTagName');
        }
    },

    get_menu: function () {
        return this._menu;
    },

    get_enabled: function () {
        return this._enabled;
    },

    set_enabled: function (value) {
        this._enabled = value;

        if (value) {
            if (this._menu) {
                this._menu.enable();
                jQuery(this._menu.get_element()).find("a.rmDisabled").removeClass("sfDisabledLinkBtn");
            }
        }
        else {
            if (this._menu) {
                this._menu.close();
                this._menu.disable();
                jQuery(this._menu.get_element()).find("a.rmDisabled").addClass("sfDisabledLinkBtn");
            }
        }
    }
};

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget", Sys.UI.Control, Telerik.Sitefinity.UI.ICommandWidget, Telerik.Sitefinity.UI.IWidget);