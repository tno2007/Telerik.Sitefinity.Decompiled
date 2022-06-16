﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.initializeBase(this, [element]);

    this._element = element;

    this._createLibraryDelegate = null;
    this._createLibraryCommandDelegate = null;

    this._createNewLibraryButton = null;    
    this._createLibraryContainer = null;
    this._createLibraryControl = null;
    this._createLibraryHiddenControls = null;
    this._itemsCount = null;

    this._radWindow = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.callBaseMethod(this, "initialize");
        if (this.get_createNewLibraryButton()) {
            this._createLibraryDelegate = Function.createDelegate(this, this._createLibraryLinkClicked);
            $addHandler(this.get_createNewLibraryButton(), "click", this._createLibraryDelegate);
        }

        if (this.get_createLibraryControl()) {
            this._createLibraryCommandDelegate = Function.createDelegate(this, this._createLibraryCommandHandler);
            this.get_createLibraryControl().add_commandExecuted(this._createLibraryCommandDelegate);
        }

        this._dialog = jQuery(this.get_createLibraryContainer()).dialog({
            autoOpen: false,
            modal: false,
            width: 355,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexXL"
            }
        });
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.callBaseMethod(this, "dispose");
        if (this._createLibraryDelegate)
        {
            if (this.get_createNewLibraryButton())                
                $removeHandler(this._createNewLibraryButton, "click", this._createLibraryDelegate);
            delete this._createLibraryDelegate;
        }

        if (this._createLibraryCommandDelegate) {
            if (this.get_createLibraryControl()) 
                this.get_createLibraryControl().remove_commandExecuting(this._createLibraryCommandDelegate);                            
            delete this._createLibraryCommandDelegate;
        }

    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */
    _createLibraryLinkClicked: function () {
        this._showCreateLibrary();
    },

    commandBar_Command: function (sender, args) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.callBaseMethod(this, "commandBar_Command", [sender, args]);
        if (this.get_radWindow()) {
            this.get_radWindow().AjaxDialog.resizeToContent();
        }
    },
    openSelectorCommand: function () {
        var urlParams =  this.get_pageSelector().get_treeBinder().get_urlParams();
        if (urlParams['take'] === undefined || urlParams['take'] === null) {
            urlParams['take'] = this.get_itemsCount();
        }
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.callBaseMethod(this, "openSelectorCommand");

        if (this.get_radWindow()) {
            this.get_radWindow().AjaxDialog.resizeToContent();
        }        
    },

    /* --------------------------------- private methods --------------------------------- */
    _showCreateLibrary: function () {
        this.get_createLibraryControl().set_provider(this.get_providerName());
        this.get_createLibraryControl().reset();        
        this._dialog.dialog("open");
        this._dialogScrollToTop(this._dialog);       
        jQuery(this.get_selectorWindowDialog()).parent().hide();        
        this._createLibraryHiddenControls = this.get_selectorWindowDialog().parent().siblings("form").children("#editorContentManagerDialog,#propertyEditor").filter(":visible");
        if (this._createLibraryHiddenControls)
            this._createLibraryHiddenControls.hide();
        jQuery(this._dialog).parent().addClass("sfSelectorDialog");
        if (this.get_radWindow()) {
            this.get_radWindow().AjaxDialog.resizeToContent();
        }
    },

    _hideCreateLibrary: function () {
        this._dialog.dialog("close");
        if (this._createLibraryHiddenControls)
            this._createLibraryHiddenControls.show();
        jQuery(this.get_selectorWindowDialog()).parent().show();
        if (this.get_radWindow()) {
            this.get_radWindow().AjaxDialog.resizeToContent();
        }
    },

    _createLibraryCommandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case "createLibrary":
                this._updateLibraryList(args.get_commandArgument());
                args.set_cancel(true);
                break;
        }
        this._hideCreateLibrary();
    },

    _updateLibraryList: function (selectedId) {
        this.get_pageSelector().set_selectedItemIds([selectedId]);
        this.get_pageSelector().dataBind();
    },

    rebind: function (providerName) {
        this._providerName = providerName;

        if (this.get_pageSelector()) {
            this.get_pageSelector().set_provider(providerName);
            this.get_pageSelector().dataBind();
        }
        if (this.get_createLibraryControl()) {
            this.get_createLibraryControl().set_provider(providerName);
            this.get_createLibraryControl().rebind(providerName);
        }
    },

    reset: function()
    {
        if (this.get_pageSelector().get_searchBox()) {
            this.get_pageSelector().get_searchBox().get_textBox().value = "";
        }
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.callBaseMethod(this, "reset");
    },

    //overrides the same method in PageField in order to show full path to the selected folder
    _setPageProps: function (item) {
        var selectedTitle = item.Path != null ? item.Path : item.Title;
        if (selectedTitle.hasOwnProperty('Value')) {
            jQuery(this._pageUrl).text(selectedTitle.Value).removeClass("sfDisplayNone");
        } else {
            jQuery(this._pageUrl).text(selectedTitle).removeClass("sfDisplayNone");
        }
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    /* --------------------------------- properties -------------------------------------- */
    get_createNewLibraryButton: function () {
        return this._createNewLibraryButton;
    },
    set_createNewLibraryButton: function (value) {
        this._createNewLibraryButton = value;
    },
    get_createLibraryContainer: function () {
        return this._createLibraryContainer;
    },
    set_createLibraryContainer: function (value) {
        this._createLibraryContainer = value;
    },
    get_createLibraryControl: function () {
        return this._createLibraryControl;
    },
    set_createLibraryControl: function (value) {
        this._createLibraryControl = value;
    },
    get_itemsCount: function () {
        return this._itemsCount;
    },
    set_itemsCount: function (value) {
        this._itemsCount = value;
    },
    // override the base property
    set_providerName: function (value) {
        if (this._providerName !== value) {
            this._providerName = value;
            this.rebind(value);
        }
    },

    get_radWindow: function () {
        if (!this._radWindow) {
            if (typeof window.radWindow !== "undefined") {
                this._radWindow = window.radWindow;
            }
            else if (window.frameElement != null && typeof window.frameElement.radWindow !== "undefined") {
                this._radWindow = window.frameElement.radWindow;
            }
        }
        return this._radWindow;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.FolderField", Telerik.Sitefinity.Web.UI.Fields.PageField, Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider);