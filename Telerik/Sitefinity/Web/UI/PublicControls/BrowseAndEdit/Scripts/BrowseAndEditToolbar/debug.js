﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit");

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar = function (element) {
    this._toolbarContainerID = null;
    this._toolbarControls = {};
    this._itemId = null;
    this._itemType = null;
    this._providerName = null;
    this._parentId = null;
    this._serviceUrl = null;
    this._alwaysVisible = false;

    this._clickDelegate = null;
    this._documentReadyDelegate = null;

    Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar.prototype = {

    /* ****************************** set up / tear down methods ****************************** */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar.callBaseMethod(this, 'initialize');

        this._clickDelegate = Function.createDelegate(this, this._clickHandler);
        this._registerEvents();
    },


    // tear down
    dispose: function () {
        this._unregisterEvents();
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar.callBaseMethod(this, "dispose");
    },

    add_command: function (delegate) {
        this.get_events().addHandler('command', delegate);
    },

    remove_command: function (delegate) {
        this.get_events().removeHandler('command', delegate);
    },

    _registerEvents: function () {
        for (var key in this._toolbarControls) {
            jQuery("#" + key).click(this._clickDelegate);
        }
    },

    _unregisterEvents: function () {
        if (this._clickDelegate) {
            for (var key in this._toolbarControls) {
                jQuery("#" + key).unbind("click", this._clickDelegate);
            }
        }
    },

    _clickHandler: function (evt) {
        var item, found = false;
        for (var key in this._toolbarControls) {
            item = this._toolbarControls[key];
            if (key == evt.target.id) {
                found = true;
                break;
            }
        }
        if (!found)
            return;
        var args = item;
        if (args) {
            this._raiseCommandExecuted(args);
        }
    },

    // This function will rise the raiseCommandExecuted event.
    _raiseCommandExecuted: function (commandArgs) {
        var h = this.get_events().getHandler('command');
        if (h) {
            var itemId = commandArgs.Arguments.ItemId || this._itemId;
            var itemType = commandArgs.Arguments.ItemType || this._itemType;
            var providerName = commandArgs.Arguments.ProviderName || this._providerName;
            var eventArgs = new Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs(
                commandArgs.CommandName,
                providerName,
                itemId,
                itemType,
                this._parentId,
                commandArgs.Arguments.DialogName,
                this._serviceUrl,
                commandArgs.Arguments.DialogUrlParameters,
                commandArgs.Arguments.AdditionalProperties);
            h(this, eventArgs);
        }
    },
    /* --------------------------------- properties -------------------------------------- */
    get_alwaysVisible: function () {
        return this._alwaysVisible;
    },
    set_alwaysVisible: function (value) {
        this._alwaysVisible = value;
    },

    get_toolbarContainerID: function () {
        return this._toolbarContainerID;
    },
    set_toolbarContainerID: function (value) {
        this._toolbarContainerID = value;
    },

    get_toolbarControls: function () {
        return this._toolbarControls;
    },
    set_toolbarControls: function (value) {
        this._toolbarControls = value;
    },

    get_itemId: function () {
        return this._itemId;
    },
    set_itemId: function (value) {
        this._itemId = value;
    },

    get_itemType: function () {
        return this._itemType;
    },
    set_itemType: function (value) {
        this._itemType = value;
    },

    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },

    get_parentId: function () {
        return this._parentId;
    },
    set_parentId: function (value) {
        this._parentId = value;
    }
};

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar', Sys.UI.Control);

// ************************** Toolbar event args **************************
Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs = function (commandName, providerName, itemId, itemType, parentId, dialogName, serviceUrl, urlParameters, additionalProperties) {
    this._commandName = commandName;
    this._providerName = providerName;
    this._itemId = itemId;
    this._itemType = itemType;
    this._parentId = parentId;
    this._dialogName = dialogName;
    this._serviceUrl = serviceUrl;
    this._urlParameters = urlParameters;
    this._additionalProperties = additionalProperties;

    Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs.prototype = {

    // ************************** Set up and tear down **************************
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs.callBaseMethod(this, 'dispose');
    },
    // ************************** Properties **************************
    get_commandName: function () {
        return this._commandName;
    },
    get_providerName: function () {
        return this._providerName;
    },
    get_itemId: function () {
        return this._itemId;
    },
    get_itemType: function () {
        return this._itemType;
    },
    get_parentId: function () {
        return this._parentId;
    },
    get_dialogName: function () {
        return this._dialogName;
    },
    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    get_urlParameters: function () {
        return this._urlParameters;
    },
    get_additionalProperties: function () {
        return this._additionalProperties;
    }
};
Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ToolbarEventArgs', Sys.EventArgs);

