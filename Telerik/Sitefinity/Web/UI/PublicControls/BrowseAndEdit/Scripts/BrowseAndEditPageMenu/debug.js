Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit");

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu = function (element)
{
    Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu.initializeBase(this, [element]);

    this._browseAndEditEnabled = false;
    this._toggleEditingTools = null;
    this._openMenu = null;
    this._menuContainer = null;
    this._menuOpened = false;
    this._clientLabelManager = null;
    this._browseAndEditCookieName = null;
    this._toggleBrowseAndEditCommandName = null;
    this._toggleBrowseAndEditDelegate = null;
    this._openMenuDelegate = null;
    this._checkMouseDelegate = null;
    this._loadDelegate = null;
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu.callBaseMethod(this, 'initialize');
        this._toggleBrowseAndEditDelegate = Function.createDelegate(this, this._toggleBrowseAndEditHandler);
        $addHandler(this._toggleEditingTools, "click", this._toggleBrowseAndEditDelegate);
        this._openMenuDelegate = Function.createDelegate(this, this._openMenuHandler);
        $addHandler(this._openMenu, "click", this._openMenuDelegate);
        this._checkMouseDelegate = Function.createDelegate(this, this._checkMouseHandler);
        this._loadDelegate = Function.createDelegate(this, this._checkBrowseAndEditState);
        Sys.Application.add_load(this._loadDelegate);
    },
    dispose: function () {
        if (this._toggleBrowseAndEditDelegate) {
            if (this._toggleEditingTools) {
                $removeHandler(this._toggleEditingTools, "click", this._toggleBrowseAndEditDelegate);
            }
            delete this._toggleBrowseAndEditDelegate;
        }
        if (this._openMenuDelegate) {
            if (this._openMenu) {
                $removeHandler(this._openMenu, "click", this._openMenuDelegate);
            }
            delete this._openMenuDelegate;
        }
        if (this._menuOpened)
            this._clean();
        if (this._checkMouseDelegate) {
            delete this._checkMouseDelegate;
        }
        if (this._loadDelegate) {
            delete this._loadDelegate;
        }

        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- events ---------------------------------- */
    add_command: function (delegate) {
        this.get_events().addHandler('command', delegate);
    },

    remove_command: function (delegate) {
        this.get_events().removeHandler('command', delegate);
    },

    /* --------------------------------- event handlers ---------------------------------- */
    _toggleBrowseAndEditHandler: function (e) {
        this._raiseCommandExecuted(this._toggleBrowseAndEditCommandName);
        this._browseAndEditEnabled = !this._browseAndEditEnabled;
        this._switchBrowseEnabled(this._browseAndEditEnabled);
    },
    _openMenuHandler: function () {
        this._menuOpened = !this._menuOpened;
        jQuery(this._menuContainer).toggle(this._menuOpened);
        if (this._menuOpened) {
            $(document).bind('mousedown', this._checkMouseDelegate);
        }
        else {
            this._clean();
        }
    },
    _checkMouseHandler: function (e) {
        this._checkMouse(e.target);
    },

    /* --------------------------------- private methods --------------------------------- */
    _raiseCommandExecuted: function (commandName, commandArgs) {
        var h = this.get_events().getHandler('command');
        if (h) {
            var eventArgs = new Telerik.Sitefinity.CommandEventArgs(commandName);
            h(this, eventArgs);
        }
    },
    _clean: function () {
        if (this._checkMouseDelegate) {
            $(document).unbind('mousedown', this._checkMouseDelegate);
        }
    },
    _checkMouse: function (elem) {
        var insideMenu = false;
        for (var pNode = elem; pNode; pNode = pNode.parentNode) {
            if (pNode == this._openMenu || pNode == this._menuContainer) {
                insideMenu = true;
                break;
            }
        }
        if (!insideMenu) {
            jQuery(this._menuContainer).toggle();
            this._menuOpened = !this._menuOpened;
            this._clean();
        }
    },
    _switchBrowseEnabled: function (enabled) {
        jQuery("body").toggleClass("sfPageEditToolsShown", enabled);
        jQuery(this._toggleEditingTools).parent().toggleClass("sfEditToolsShown", enabled);
        var resKey = enabled ? "HideEditingTools" : "ShowEditingTools";
        var text = this._clientLabelManager.getLabel("PublicControlsResources", resKey);
        jQuery(this._toggleEditingTools).text(text);
        var browseAndEditState = this._browseAndEditEnabled ? 'enabled' : 'disabled';
        jQuery.cookie("browseAndEditState", browseAndEditState, { path: '/' });
    },
    _checkBrowseAndEditState: function () {
        browseAndEditState = jQuery.cookie(this._browseAndEditCookieName);
        browseAndEditEnabled = false;
        if (browseAndEditState != null && browseAndEditState != "") {
            browseAndEditEnabled = (browseAndEditState == 'enabled' ? true : false);
        }
        if (browseAndEditEnabled) {
            this._browseAndEditEnabled = true;
            this._switchBrowseEnabled(true);
        }
    },
    /* --------------------------------- properties -------------------------------------- */
    get_toggleEditingTools: function () {
        return this._toggleEditingTools;
    },
    set_toggleEditingTools: function (value) {
        this._toggleEditingTools = value;
    },
    get_openMenu: function () {
        return this._openMenu;
    },
    set_openMenu: function (value) {
        this._openMenu = value;
    },
    get_menuContainer: function () {
        return this._menuContainer;
    },
    set_menuContainer: function (value) {
        this._menuContainer = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_toggleBrowseAndEditCommandName: function () {
        return this._toggleBrowseAndEditCommandName;
    },
    set_toggleBrowseAndEditCommandName: function (value) {
        this._toggleBrowseAndEditCommandName = value;
    },
    get_browseAndEditCookieName: function () {
        return this._browseAndEditCookieName;
    },
    set_browseAndEditCookieName: function (value) {
        this._browseAndEditCookieName = value;
    }
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu', Sys.UI.Control);
