Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep = function (element) {
    this._listSelector = null;
    this._messageControl = null;
    this._selectorPanel = null;
    this._noListsPanel = null;
    this._createYourFirstListButton = null;
    this._selectedList = null;
    this._selectedListId = null;
    this._listDialog = null;
    this._currentWindow = null;

    this._itemSelectedDelegate = null;
    this._itemSelectorDataBoundDelegate = null;
    this._selectorReadyDelegate = null;
    this._createYourFirstListDelegate = null;

    this._listDialogCloseDelegate = null;
    this._listDialogShowDelegate = null;
    this._listDialogLoadedDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep.initializeBase(this, [element]);
}
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep.prototype = {

    // set up 
    initialize: function () {

        this.show();

        if (this._itemSelectedDelegate === null) {
            this._itemSelectedDelegate = Function.createDelegate(this, this._itemSelectedHandler);
        }
        this._listSelector.add_itemSelected(this._itemSelectedDelegate);

        if (this._itemSelectorDataBoundDelegate === null) {
            this._itemSelectorDataBoundDelegate = Function.createDelegate(this, this._itemSelectorDataBoundHandler);
        }
        this._listSelector.add_binderDataBound(this._itemSelectorDataBoundDelegate);

        if (this._selectorReadyDelegate === null) {
            this._selectorReadyDelegate = Function.createDelegate(this, this._selectorReadyHandler);
        }
        this._listSelector.add_selectorReady(this._selectorReadyDelegate);

        if (this._createYourFirstListDelegate === null) {
            this._createYourFirstListDelegate = Function.createDelegate(this, this._createYourFirstListHandler);
        }
        $addHandler(this._createYourFirstListButton, 'click', this._createYourFirstListDelegate);

        if (this._listDialogCloseDelegate === null) {
            this._listDialogCloseDelegate = Function.createDelegate(this, this._listDialogClosed);
        }
        this.get_listDialog().add_close(this._listDialogCloseDelegate);

        if (this._listDialogShowDelegate === null) {
            this._listDialogShowDelegate = Function.createDelegate(this, this._listDialogShow);
        }

        if (this._listDialogLoadedDelegate === null) {
            this._listDialogLoadedDelegate = Function.createDelegate(this, this._listDialogLoaded);
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._itemSelectedDelegate) {
            this._listSelector.remove_itemSelected(this._itemSelectedDelegate);
            delete this._itemSelectedDelegate;
        }

        if (this._itemSelectorDataBoundDelegate) {
            this._listSelector.remove_binderDataBound(this._itemSelectorDataBoundDelegate);
            delete this._itemSelectorDataBoundDelegate;
        }

        if (this._selectorReadyDelegate) {
            this._listSelector.remove_selectorReady(this._selectorReadyDelegate);
            delete this._selectorReadyDelegate;
        }

        if (this._createYourFirstListDelegate) {
            delete this._createYourFirstListDelegate;
        }

        if (this._listDialogCloseDelegate) {
            this.get_listDialog().remove_close(this._listDialogCloseDelegate);
            delete this._listDialogCloseDelegate;
        }

        if (this._listDialogShowDelegate) {
            delete this._listDialogShowDelegate;
        }

        if (this._listDialogLoaded) {
            delete this._listDialogLoaded;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */
    isValid: function () {
        return this.get_selectedList() !== null;
    },

    selectList: function (listId) {
        this._selectedListId = listId;
        this._listSelector.set_selectedKeys([listId]);
    },

    reset: function () {
        this._listSelector.cleanUp();
    },

    /* *************************** private methods *************************** */
    _itemSelectedHandler: function (sender, args) {
        if (args != null) {
            this._selectedList = args;
        } else {
            this._selectedList = null;
        }
    },

    _itemSelectorDataBoundHandler: function (sender, args) {
        this._prepareUI();
    },

    _selectorReadyHandler: function (sender, args) {
        this._prepareUI();
    },

    _prepareUI: function () {
        if (this._listSelector._grid.get_masterTableView().get_virtualItemCount() > 0) {
            $(this._selectorPanel).show();
            $(this._noListsPanel).hide();
        } else {
            var isFiltering = this._listSelector.get_binder().get_isFiltering();
            if (isFiltering) {
                $(this._selectorPanel).show();
            } else {
                $(this._selectorPanel).hide();
                $(this._noListsPanel).show();
            }
        }
    },

    _createYourFirstListHandler: function (sender, agrs) {
        this._currentWindow = this._getRadWindow();
        this.get_listDialog().show();
        this.get_listDialog().maximize();
        this._currentWindow.minimize();

        this.get_listDialog().add_pageLoad(this._listDialogLoadedDelegate);
    },

    _listDialogLoaded: function (sender, args) {
        this.get_listDialog().remove_pageLoad(this._listDialogLoadedDelegate);
        this.get_listDialog().add_show(this._listDialogShowDelegate);
        this._listDialogShow();
    },

    _listDialogShow: function (sender, args) {
        this.get_listDialog().get_contentFrame().contentWindow.setForm(null, true);
    },

    _listDialogClosed: function (sender, args) {
        this._currentWindow.maximize();
        var argument = args.get_argument();
        if (argument != null) {
            if (argument.IsCreated || argument.IsUpdated) {
                this._listSelector.bindSelector();
            }
        }
    },

    _getRadWindow: function () {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        return oWindow;
    },

    /* *************************** properties *************************** */
    get_listSelector: function () {
        return this._listSelector;
    },
    set_listSelector: function (value) {
        this._listSelector = value;
    },
    get_selectorPanel: function () {
        return this._selectorPanel;
    },
    set_selectorPanel: function (value) {
        this._selectorPanel = value;
    },
    get_noListsPanel: function () {
        return this._noListsPanel;
    },
    set_noListsPanel: function (value) {
        this._noListsPanel = value;
    },
    get_createYourFirstListButton: function () {
        return this._createYourFirstListButton;
    },
    set_createYourFirstListButton: function (value) {
        this._createYourFirstListButton = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_selectedList: function () {
        return this._selectedList;
    },
    get_listDialog: function () {
        if (this._listDialog == null) {
            var currentWindow = this._getRadWindow();
            var windowManager = currentWindow.get_windowManager();
            this._listDialog = windowManager.getWindowByName("listDialog");
        }
        return this._listDialog;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep', Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl);