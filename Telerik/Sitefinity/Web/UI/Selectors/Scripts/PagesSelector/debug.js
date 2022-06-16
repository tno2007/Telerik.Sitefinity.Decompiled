Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.PagesSelector = function (element) {
    this._element = element;
    this._tabstrip = null;
    this._pageSelector = null;
    this._extPagesSelector = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._bindOnLoad = null;

    this.__currentPanelDelegate = null;
    this._onloadDelegate = null;
    this._selectionChangedDelegate = null;
    this._doneSelectingDelegate = null;
    this._pageSelector_selectionAppliedDelegate = null;
    this._cancelDelegate = null;

    this.PANEL_THIS_SITE = 'panelThisSite';
    this.PANEL_OTHER_SITES = 'panelOtherSites';

    //The currently visible(selected) panel
    this._selectedPanel = this.PANEL_THIS_SITE;

    //Contains all selected pages - both internal and external
    this._selectedPages = null;
    //Contains all selected internal pages
    this._selectedInternalPages = null;
    //Contains all selected external pages
    this._selectedExternalPages = null;
    this._onTabSelectedDelegate = null;

    this._uiCulture = null;

    Telerik.Sitefinity.Web.UI.PagesSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PagesSelector.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.PagesSelector.callBaseMethod(this, "initialize");
        if (this._onloadDelegate == null) {
            this._onloadDelegate = Function.createDelegate(this, this._onload);
        }
        Sys.Application.add_load(this._onloadDelegate);

        this._doneSelectingDelegate = Function.createDelegate(this, this._onDoneSelecting);
        $addHandler(this.get_doneButton(), "click", this._doneSelectingDelegate);

        this._cancelDelegate = Function.createDelegate(this, this._onCancel);
        $addHandler(this.get_cancelButton(), "click", this._cancelDelegate);

        if (this._selectionChangedDelegate == null) {
            this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChanged);
        }

        if (this._pageSelector_selectionAppliedDelegate == null) {
            this._pageSelector_selectionAppliedDelegate = Function.createDelegate(this, this._pageSelector_selectionApplied);
        }

        var pageSelector = this.get_pageSelector();
        pageSelector.set_uiCulture(this.get_uiCulture());
        pageSelector.add_selectionChanged(this._selectionChangedDelegate);
        pageSelector.add_selectionApplied(this._pageSelector_selectionAppliedDelegate);
        pageSelector.set_bindOnLoad(false);

        if (this.get_tabstrip()) {
            this._onTabSelectedDelegate = Function.createDelegate(this, this._onTabSelected);
            this._tabstrip.add_tabSelected(this._onTabSelectedDelegate);
        }

        //hook radio buttons
        //this._currentPanelDelegate = Function.createDelegate(this, this._clickedCurrentPanel);
        //this._setRadioClickHandler("CurrentPanel", this._currentPanelDelegate);

    },

    dispose: function () {
        if (this._onloadDelegate) {
            Sys.Application.remove_load(this._onloadDelegate);
            delete this._onloadDelegate;
        }

        if (this._doneSelectingDelegate) {
            $removeHandler(this.get_doneButton(), "click", this._doneSelectingDelegate);
            delete this._doneSelectingDelegate;
        }
        if (this._cancelDelegate) {
            $removeHandler(this.get_cancelButton(), "click", this._cancelDelegate);
            delete this._cancelDelegate;
        }

        if (this._selectionChangedDelegate) {
            this.get_pageSelector().remove_selectionChanged(this._selectionChangedDelegate);
            delete this._selectionChangedDelegate;
        }

        if (this._pageSelector_selectionAppliedDelegate) {
            this.get_pageSelector().remove_selectionApplied(this._pageSelector_selectionAppliedDelegate);
            delete this._pageSelector_selectionAppliedDelegate;
        }

        if (this._currentPanelDelegate) {
            delete this._currentPanelDelegate;
        }

        if (this.get_tabstrip()) {
            this._tabstrip.remove_tabSelected(this._onTabSelectedDelegate);
            delete this._onTabSelectedDelegate;
        }

        Telerik.Sitefinity.Web.UI.PagesSelector.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // Binds the page selector to the data.
    dataBind: function (query) {
        this.get_pageSelector().dataBind(query);
    },

    // Clears all selected items.
    clearSelection: function () {

    },

    getSelectedItems: function () {
        //        if (this._selectedPanel == this.PANEL_THIS_SITE) {
        var items = this.get_pageSelector().get_selectedItems();
        var result = [];
        if (items) {
            for (var i = 0, l = items.length; i < l; i++) {
                var item = items[i];

                if (item.TitlesPath) {
                    result.push({ Id: item.Id, Title: item.TitlesPath });
                }
                else {
                    result.push({ Id: item.Id, Title: item.Title.Value });
                }
            }
        }
        //            return result;
        //        } else if (this._selectedPanel == this.PANEL_OTHER_SITES) {
        items = this.get_extPagesSelector().get_selectedItems();
        //            var result = [];
        if (items) {
            for (var i = 0, l = items.length; i < l; i++) {
                var item = items[i];
                result.push({ Id: item.Id, Title: item.Title, Url: item.Url });
            }
        }
        return result;
        //        } else {
        //            return null;
        //        }
    },
    setSelectedItems: function (items) {
        this._selectedPages = items;
        var selectedInternalPages = [];
        var selectedExternalPages = [];

        var hasSelectedExternalPages = false;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            if (item.Id) {
                selectedInternalPages.push(item);
            } else {
                selectedExternalPages.push(item);
                hasSelectedExternalPages = true;
            }
        }

        //        //Automatically add a default ext page if no pages are defined
        //        if (selectedExternalPages.length == 0) {
        //            selectedExternalPages.push({ Id: null, Title: "External Page", Url: "http://example.com" });
        //        }

        this._selectedInternalPages = selectedInternalPages;
        this._selectedExternalPages = selectedExternalPages;

        this.get_pageSelector().set_selectedItems(selectedInternalPages);
        this.get_pageSelector().dataBind("");
        this.get_extPagesSelector().set_selectedItems(selectedExternalPages);

        if (hasSelectedExternalPages == true) {
            var elm = jQuery(this.get_element()).find("input[value='" + this.PANEL_OTHER_SITES + "']");
            elm.attr("checked", "checked");
            this._showPanel(this.PANEL_OTHER_SITES);
        }
    },

    _onTabSelected: function () {
        dialogBase.resizeToContent();
    },

    /* -------------------- events -------------------- */

    // Happens when selection of the pages is changed (node is checked or unchecked)
    add_selectionChanged: function (delegate) {
        this.get_events().addHandler("selectionChanged", delegate);
    },

    // Happens when selection of the pages is changed (node is checked or unchecked)
    remove_selectionChanged: function (delegate) {
        this.get_events().removeHandler("selectionChanged", delegate);
    },

    add_doneClientSelection: function (delegate) {
        this.get_events().addHandler('doneClientSelection', delegate);
    },
    remove_doneClientSelection: function (delegate) {
        this.get_events().removeHandler('doneClientSelection', delegate);
    },

    add_selectionApplied: function (handler) {
        /// <summary>
        /// Raised when the pre-set selection was actually aplied after data is bound.
        /// </summary>
        this.get_events().addHandler("selectionApplied", handler);
    },

    remove_selectionApplied: function (handler) {
        this.get_events().removeHandler("selectionApplied", handler);
    },

    _raiseSelectionApplied: function (args) {
        var handler = this.get_events().getHandler("selectionApplied");
        if (handler) handler(this, args);
    },

    //utility method to set radio group click handler
    _setRadioClickHandler: function (groupName, delegate) {
        jQuery(this.get_element()).find("input[name='" + groupName + "']").click(delegate)
    },

    /* -------------------- event handlers ------------ */

    _onload: function () {
        var query = null;
        //        if (this.get_selectedItemId()) {
        //            this.get_gridBinder().add_onItemDataBound(this._gridBinderItemDataBoundDelegate);
        //            query = String.format("Id==({0})", this.get_selectedItemId());
        //        }
        if (this._bindOnLoad)
        this.dataBind(query);
    },


    _clickedCurrentPanel: function (sender) {
        this._hideAllPanels();
        this._selectedPanel = sender.target.value;
        this._showPanel(sender.target.value);
    },

    //hides all panels
    _hideAllPanels: function () {
        jQuery(this.get_element()).find("#Panels div[id^='panel']").hide();
    },
    //shows a specific panel
    _showPanel: function (panelId) {
        jQuery(this.get_element()).find("#Panels #" + panelId).show();
    },

    _selectionChanged: function (sender, args) {
        var selectedItems = args;
    },

    _pageSelector_selectionApplied: function (sender, args) {
        this._raiseSelectionApplied(args);
    },

    _doneSelecting: function (cancel) {
        var h = this.get_events().getHandler('doneClientSelection');
        if (h) {
            if (cancel) {
                h(null, true);
            } else {
                var selectedPages = this.getSelectedItems();
                h(selectedPages, false);
            }
        }
    },

    _onDoneSelecting: function () {
        this._doneSelecting(false);
    },

    _onCancel: function () {
        this._doneSelecting(true);
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    get_element: function () {
        return this._element;
    },
    set_element: function (value) {
        this._element = value;
    },
    get_tabstrip: function () {
        return this._tabstrip;
    },
    set_tabstrip: function (value) {
        this._tabstrip = value;
    },
    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },
    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_pageSelector: function () {
        return this._pageSelector;
    },
    set_pageSelector: function (value) {
        this._pageSelector = value;
    },
    get_extPagesSelector: function () {
        return this._extPagesSelector;
    },
    set_extPagesSelector: function (value) {
        this._extPagesSelector = value;
    },
    get_uiCulture: function () {
        return this._uiCulture;
    },
    set_uiCulture: function (value) {
        this._uiCulture = value;
    },

    get_selectedItem: function () {
        var selectedItems = null;
        //        if (this._isTreeMode) {
        //            selectedItems = this.get_itemsTree().get_selectedItems();
        //        }
        //        else {
        //            selectedItems = this.get_itemsGrid().get_selectedItems();
        //        }
        //        if (selectedItems) {
        //            return selectedItems[0];
        //        }

        return null;
    },

    set_selectedItemId: function (value) {
        this._selectedItemId = value;
    },
    get_selectedItemId: function () {
        var selectedItem = this.get_selectedItem();
        if (selectedItem) {
            return selectedItem.Id;
        }
        return this._selectedItemId;
    },
    get_selectedPanel: function () {
        return this._selectedPanel;
    }
};

Telerik.Sitefinity.Web.UI.PagesSelector.registerClass("Telerik.Sitefinity.Web.UI.PagesSelector", Sys.UI.Control);
