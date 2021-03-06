Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit");

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar = function (element) {
    this._deleteOperationsContainerID = null;
    this._deleteOperationsTitleElementID = null;
    this._deleteItemWarningDialog = {};
    this._unpublishItemWarningDialog = {};
    this._deleteItemWarningDialogCommandDelegate = null;
    this._unpublishItemWarningDialogCommandDelegate = null;

    Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar.prototype = {

    /* ****************************** set up / tear down methods ****************************** */
    initialize: function () {
        var inlineEditingWrapper = $(this._element).closest('sfie');
        if (inlineEditingWrapper && inlineEditingWrapper.length > 0 && inlineEditingWrapper.find('[data-sf-type]').length === 0) {
            inlineEditingWrapper.attr('data-sf-id', this.get_itemId());
            inlineEditingWrapper.attr('data-sf-type', this.get_itemType());
            inlineEditingWrapper.attr('data-sf-provider', this.get_providerName());
            $(this._element).hide();
        }
    },

    _attach_clickmenu: function () {
        jQuery("#" + this._deleteOperationsContainerID).clickMenu();
        jQuery("#" + this._deleteOperationsTitleElementID).children(".outerbox").hide();
    },

    // tear down
    dispose: function () {
        this._unregisterEvents();
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar.callBaseMethod(this, "dispose");
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
            switch (args.CommandName) {
                case 'delete':
                    this._clearHandler(this._deleteItemWarningDialog, this._deleteItemWarningDialogCommandDelegate);
                    this._deleteItemWarningDialogCommandDelegate = Function.createDelegate({ self: this, arguments: args }, this._deleteItemWarningDialogCommand);
                    this._deleteItemWarningDialog.add_command(this._deleteItemWarningDialogCommandDelegate);
                    this._deleteItemWarningDialog.show_prompt();
                    break;
                case 'unpublish':
                    this._clearHandler(this._unpublishItemWarningDialog, this._unpublishItemWarningDialogCommandDelegate);
                    this._unpublishItemWarningDialogCommandDelegate = Function.createDelegate({ self: this, arguments: args }, this._unpublishItemWarningDialogCommand);
                    this._unpublishItemWarningDialog.add_command(this._unpublishItemWarningDialogCommandDelegate);
                    this._unpublishItemWarningDialog.show_prompt();
                    break;
                default:
                    this._raiseCommandExecuted(args);
            }
        }
    },

    _clearHandler: function (dialog, delegate) {
        if (delegate) {
            dialog.remove_command(delegate);
            delegate = null;
        }
    },

    // The function to be called when user tries to delete an item.
    _deleteItemWarningDialogCommand: function (sender, args) {
        var command = args.get_commandName();
        if (command == "ok") {
            this.self._raiseCommandExecuted(this.arguments);
        }
    },

    // The function to be called when user tries to unpublish an item.
    _unpublishItemWarningDialogCommand: function (sender, args) {
        var command = args.get_commandName();
        if (command == "ok") {
            this.self._raiseCommandExecuted(this.arguments);
        }
    },

    get_deleteItemWarningDialog: function () {
        return this._deleteItemWarningDialog;
    },

    set_deleteItemWarningDialog: function (value) {
        this._deleteItemWarningDialog = value;
    },

    get_unpublishItemWarningDialog: function () {
        return this._unpublishItemWarningDialog;
    },

    set_unpublishItemWarningDialog: function (value) {
        this._unpublishItemWarningDialog = value;
    },

    get_deleteOperationsContainerID: function () {
        return this._deleteOperationsContainerID;
    },

    set_deleteOperationsContainerID: function (value) {
        this._deleteOperationsContainerID = value;
    },

    get_deleteOperationsTitleElementID: function () {
        return this._deleteOperationsTitleElementID;
    },

    set_deleteOperationsTitleElementID: function (value) {
        this._deleteOperationsTitleElementID = value;
    }
};

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar', Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbar);


