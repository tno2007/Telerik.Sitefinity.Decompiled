﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField.initializeBase(this, [element]);

    this._element = element;
    this._selectElement = null;
    this._binder = null;
    this._serviceBaseUrl = null;

    this._changed = false;
    this._selectedId = null;
    this._createHyperLink = null;
    this._exampleHyperLink = null;

    this._migrationLogDialog = null;
    this._dialog = null;
    this._closeMigrationDlgBtn = null;

    this._createNewItemDelegate = null;
    this._changeDelegate = null;

    this._createItemPrompt = null;
    this._createItemPromptCommandDelegate = null;

    this._createPromptTitle = null;
    this._createPromptTextFieldTitle = null;
    this._createPromptExampleText = null;
    this._createPromptCreateButtonTitle = null;
    this._createPromptCreateButtonTitle = null;
    this._createNewItemServiceUrl = null;
    this._binderDataBoundDelegate = null;
    this._newlyCreatedGroup = null;
    this._createPromptTextsSet = false;

    this._closeMigrationDlgBtnDelegate = null;

    this._newItemPromptSuccessHandler = null;
    this._newItemPromptFailureHandler = null;
    this._providerName = null;
}

Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField.prototype =
{
    initialize: function () {
        this._changeDelegate = Function.createDelegate(this, this._changeHandler);
        this._createNewItemDelegate = Function.createDelegate(this, this._createNew);
        this._exampleDelegate = Function.createDelegate(this, this._exampleHandler);
        this._createItemPromptCommandDelegate = Function.createDelegate(this, this._promptCommandHandler);

        this._newItemPromptSuccessHandler = Function.createDelegate(this, this._promptDialogSuccess);
        this._newItemPromptFailureHandler = Function.createDelegate(this, this._promptDialogFail);
        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        this._binder.add_onDataBound(this._binderDataBoundDelegate);

        $addHandler(this.get_selectElement(), "change", this._changeDelegate);
        $addHandler(this.get_createHyperLink(), "click", this._createNewItemDelegate);
        $addHandler(this.get_exampleHyperLink(), "click", this._exampleDelegate);

        this._createItemPrompt.add_command(this._createItemPromptCommandDelegate);

        //adds click handler to 'Close' button which closes the dialog which the log in written in
        this._closeMigrationDlgBtnDelegate = Function.createDelegate(this, this.closeMigrationDlg);
        $addHandler(this._closeMigrationDlgBtn, "click", this._closeMigrationDlgBtnDelegate, true);

        if (this._itemsGridItemCommandDelegate == null) {
            this._itemsGridItemCommandDelegate = Function.createDelegate(this, this._itemsGridItemCommand);
        }

        //initialize jquery UI dialod which the migration log is displayed in during the migration process
        this._dialog = jQuery(this._migrationLogDialog).dialog({
            autoOpen: false,
            modal: true,
            width: 510,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfSelectorDialog sfZIndexL"
            }
        });

        Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        this._selectElement = null;
        if (this._changeDelegate) {
            if (this.get_selectElement()) {
                $removeHandler(this.get_selectElement(), "change", this._changeDelegate);
            }
            delete this._changeDelegate;
        }
        if (this._createNewItemDelegate) {
            if (this.get_createHyperLink()) {
                $removeHandler(this.get_createHyperLink(), "click", this._createNewItemDelegate);
            }
            delete this._createNewItemDelegate;
        }
        if (this._exampleDelegate) {
            if (this.get_exampleHyperLink()) {
                $removeHandler(this.get_exampleHyperLink(), "click", this._exampleDelegate);
            }
            delete this._exampleDelegate;
        }
        if (this._createItemPromptCommandDelegate) {
            if (this.get_createItemPrompt()) {
                this.get_createItemPrompt().remove_command(this._createItemPromptCommandDelegate);
            }
            delete this._createItemPromptCommandDelegate;
        }
        if (this._binderDataBoundDelegate) {
            this._binder.remove_onDataBound(this._binderDataBoundDelegate);
            delete this._binderDataBoundDelegate;
        }

        if (this._closeMigrationDlgBtnDelegate) {
            if (this.get_closeMigrationDlgBtn()) {
                $removeHandler(this.get_closeMigrationDlgBtn(), "click", this._closeMigrationDlgBtnDelegate);
            }
            delete this._closeMigrationDlgBtnDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField.callBaseMethod(this, "dispose");
    },


    /* --------------------  public methods ----------- */

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField.callBaseMethod(this, "reset");

        this._newlyCreatedGroup = null;
        this._selectedId = null;
        this.set_value(null);
        this.dataBind();
    },

    dataBind: function () {
        if (this._binder) {
            this._binder.set_provider(this._providerName);
            this._binder.DataBind();
        }
    },

    // Gets the value of the field control.
    get_value: function () {
        if (this._value && this._value.Id) {
            return null;
        }
        else {
            return this._value;
        }
    },

    set_value: function (value) {
        this._value = value;
        if (value && value.Id) {
            this.setSelectedParentId(value.Id);
        }
        else {
            this.setSelectedParentId(value);
        }
    },

    isChanged: function () {
        return this._changed;
    },

    getSelectedParentId: function () {
        return this._selectedId;
    },

    //fired when 'Close' button of migration dialog is clicked
    closeMigrationDlg: function (e) {
        this._dialog.dialog("close");
    },

    setSelectedParentId: function (value) {
        if (this.get_selectElement()) {
            if (value)
                jQuery(this.get_selectElement()).val(value);
            else
                jQuery(this.get_selectElement()).prop('selectedIndex', 0);
        }

        this._changeHandler();
    },

    /* -------------------- event handlers ---------------- */

    _changeHandler: function () {
        this._selectedId = jQuery(this.get_selectElement()).val();

        if (this._value && this._value.Id) {
            this._changed = this._selectedId != this._value.Id;
        }
        else if (this._value) {
            this._changed = this._selectedId != this._value;
        }
        else {
            this._changed = true;
        }

        var handler = this.get_events().getHandler("valueChanged");
        if (handler) handler(this);
    },

    _createNew: function (resetText) {
        if ((typeof (resetText) == "undefined") || (resetText != false))
            this._createItemPrompt.set_inputText("");

        if (!this._createPromptTextsSet) {
            jQuery('#' + this._createItemPrompt._commands[0].ButtonClientId).find("span").text(this._createPromptCreateButtonTitle);
            this._createItemPrompt.set_textFieldExample(this._createPromptExampleText);
            this._createItemPrompt.set_title(this._createPromptTitle);
            this._createItemPrompt.set_message(this._createPromptTextFieldTitle);
            this._createPromptTextsSet = true;
        }
        this.get_createItemPrompt().show_prompt();
    },

    _exampleHandler: function () {
        this._dialog.dialog("open");
    },

    _promptCommandHandler: function (sender, args) {
        if (args.get_commandName() == "createItem") {
            var url = this._createNewItemServiceUrl;
            var urlParams = {};
            urlParams['provider'] = this._binder.get_provider();
            urlParams['title'] = sender.get_inputText();
            var manager = this._binder.get_manager();
            manager.InvokePut(url, urlParams, null, {}, this._newItemPromptSuccessHandler, this._newItemPromptFailureHandler, this._itemsGrid);
        }
    },

    _promptDialogSuccess: function (sender, args) {
        if (args && args.Item)
            this._newlyCreatedGroup = args.Item;
        this.dataBind();
    },

    _promptDialogFail: function (err) {
        alert(err.Detail);
        this._createNew(false);
    },

    _binderDataBound: function (sender, args) {
        if (this._newlyCreatedGroup != null) {            
            this.setSelectedParentId(this._newlyCreatedGroup.Id);
        }
        else {
            this._restoreSelection();
        }

        var handler = this.get_events().getHandler("binderDataBound");
        if (handler) handler(this, args);
    },

    _restoreSelection: function () {
        this.set_value(this._value);
    },

    /* -------------------- properties ---------------- */


    // Gets the reference to the prompt dialog for creating a new item.
    get_createItemPrompt: function () {
        return this._createItemPrompt;
    },

    // Sets the reference to the prompt dialog for creating a new item.
    set_createItemPrompt: function (value) {
        this._createItemPrompt = value;
    },

    // Gets the reference to the DOM element used to create new item of the text field control.
    get_createHyperLink: function () {
        return this._createHyperLink;
    },

    // Sets the reference to the DOM element used to create new item of the text field control.
    set_createHyperLink: function (value) {
        this._createHyperLink = value;
    },

    // Gets the reference to the DOM element used to open an example picture.
    get_exampleHyperLink: function () {
        return this._exampleHyperLink;
    },

    // Sets the reference to the DOM element used to open an example picture.
    set_exampleHyperLink: function (value) {
        this._exampleHyperLink = value;
    },

    // Gets the reference to the DOM element used to display the text box of the text field control.
    get_selectElement: function () {
        return this._selectElement;
    },

    // Sets the reference to the DOM element used to display the text box of the text field control.
    set_selectElement: function (value) {
        this._selectElement = value;
    },

    // Gets the reference to the migration dialog.
    get_migrationLogDialog: function () {
        return this._migrationLogDialog;
    },

    // Sets the reference to the migration dialog.
    set_migrationLogDialog: function (value) {
        this._migrationLogDialog = value;
    },

    // Gets the reference to the close button of the migration dialog.
    get_closeMigrationDlgBtn: function () {
        return this._closeMigrationDlgBtn;
    },

    // Sets the reference to the close button of the migration dialog.
    set_closeMigrationDlgBtn: function (value) {
        this._closeMigrationDlgBtn = value;
    },

    // gets the client binder
    get_binder: function () {
        return this._binder;
    },

    // sets the client binder
    set_binder: function (value) {
        this._binder = value;
    },

    // gets service Url
    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },

    // sets service Url
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },

    // Sets the position in the tabbing order
    // Overridden from field control
    set_tabIndex: function (value) {
        this._tabIndex = value;
        jQuery(this.get_selectElement()).attr("tabindex", value);
    },

    // Passes the provider to the control
    set_providerName: function (value) {
        this._providerName = value;

        var binder = this.get_binder();
        if (binder) {
            binder.set_provider(value);
        }
    },

    // Gets the provider from the control
    get_providerName: function () {
        return this._providerName;
    },

    add_binderDataBound: function (delegate) {
        this.get_events().addHandler("binderDataBound", delegate);
    },

    remove_binderDataBound: function (delegate) {
        this.get_events().removeHandler("binderDataBound", delegate);
    },

    add_valueChanged: function (delegate) {
        this.get_events().addHandler("valueChanged", delegate);
    },

    remove_valueChanged: function (delegate) {
        this.get_events().removeHandler("valueChanged", delegate);
    },

    get_uiCulture: function () {
        if (this._binder) {
            return this._binder.get_uiCulture();
        }
        else {
            return null;
        }
    },
    set_uiCulture: function (value) {
        if (this._binder) {
            this._binder.set_uiCulture(value);
        }
    }
};
Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField.registerClass("Telerik.Sitefinity.Web.UI.Fields.BindableChoiceField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, 
    Telerik.Sitefinity.Web.UI.IParentSelectorField, Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider, Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture);