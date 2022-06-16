Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

var librarySelectorDialog;

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog.initializeBase(this, [element]);

    this._folderSelector = null;
    this._args = null;

    this._doneClientSelectionDelegate = null;
    this._onLoadDelegate = null;
    this._selectionChangedDelegate = null;
    this._moveConfirmationDialog = null;
    this._moveFinalDelegate = null;
    this._clientLabelManager = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        librarySelectorDialog = this;

        if (this._doneClientSelectionDelegate == null) {
            this._doneClientSelectionDelegate = Function.createDelegate(this, this._handleDoneClientSelection);
        }
        this.get_folderSelector().add_doneClientSelection(this._doneClientSelectionDelegate);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog.callBaseMethod(this, "initialize");

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        this._selectionChangedDelegate = Function.createDelegate(this, this._handleSelectionChanged);
        this._moveFinalDelegate = Function.createDelegate(this, this._moveFinal);
    },

    dispose: function () {
        if (this._doneClientSelectionDelegate != null) {
            if (this.get_folderSelector() != null) {
                this.get_folderSelector().remove_doneClientSelection(this._doneClientSelectionDelegate);
            }
            delete this._doneClientSelectionDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        if (this._selectionChangedDelegate) {
            if (this.get_folderSelector().get_foldersGenericSelector()) {
                this.get_folderSelector().get_foldersGenericSelector().remove_selectionChanged(this._selectionChangedDelegate);
            }
            delete this._selectionChangedDelegate;
        }

        if (this._moveFinalDelegate)
            delete this._moveFinalDelegate;

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    _onLoad: function () {
        jQuery("body").addClass("sfSelectorDialog sfMaxPromptDlg");
        this.get_folderSelector().get_foldersGenericSelector().add_selectionChanged(this._selectionChangedDelegate);
        dialogBase.resizeToContent();
    },
    // rebinds the library selector
    dataBind: function () {
        $(this.get_element()).show();
        $(this.get_moveConfirmationDialog().get_wrapperElement()).hide();
        this.get_folderSelector().dataBind();
        jQuery(this.get_folderSelector().get_lnkDoneSelecting()).addClass("sfDisabledLinkBtn");
        dialogBase.resizeToContent();
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handleSelectionChanged: function (sender, args) {
        var selectedItems = args;
        if (selectedItems && selectedItems.length > 0) {
            jQuery(this.get_folderSelector().get_lnkDoneSelecting()).removeClass("sfDisabledLinkBtn");
        }
        else {
            jQuery(this.get_folderSelector().get_lnkDoneSelecting()).addClass("sfDisabledLinkBtn");
        }
    },

    _handleDoneClientSelection: function (items) {
        if (items) {
            if (items.length > 0) {
                var dataItem = this.get_folderSelector().getSelectedItems()[0];
                var context = {
                    "WindowName": this.get_radWindow().get_name(),
                    "Id": items[0],
                    "ItemType": this.getQueryValue("itemType", false),
                    "ParentItemType": this.getQueryValue("parentType", false)
                };
                this._arg = { "IsCreated": false, "IsUpdated": false, "DataItem": dataItem, "Context": context };
                if (this.getQueryValue("showPrompt")) {
                    this.get_moveConfirmationDialog().set_message(String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "MoveLibraryWarning"), this.get_folderSelector().get_selectedItem().Title));
                    this.get_moveConfirmationDialog().show_prompt('', '', this._moveFinalDelegate);
                    $(this.get_element()).hide();
                    dialogBase.resizeToContent();
                    return;
                }
                this.close(this._arg);
            }
        }
        else {
            this.close();
        }
    },

    /* -------------------- private methods ----------- */
    _moveFinal: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName == 'cancel') {
            $(this.get_element()).show();
            dialogBase.resizeToContent();
            return;
        }

        this.close(this._arg);
    },

    /* -------------------- properties ---------------- */

    get_folderSelector: function () {
        return this._folderSelector;
    },

    set_folderSelector: function (value) {
        this._folderSelector = value;
    },

    get_moveConfirmationDialog: function () {
        return this._moveConfirmationDialog;
    },
    set_moveConfirmationDialog: function (value) {
        this._moveConfirmationDialog = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }

};

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelectorDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);