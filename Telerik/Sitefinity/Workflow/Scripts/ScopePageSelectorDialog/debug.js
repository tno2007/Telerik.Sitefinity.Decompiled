var scopePageSelectorDialog = null;

window.createDialog = function (selectedPagesIds, applyToChildPages, site, culture) {
    if (scopePageSelectorDialog) {
        scopePageSelectorDialog.createDialog(selectedPagesIds, applyToChildPages, site, culture);
    }
};

Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog.initializeBase(this, [element]);

    this._selectors = {
        body: "body",
        siteNameHeader: "#siteNameHeader",
        checkedRadioBtn: "input:checked"
    };

    this._applyWorkflowSelectorValues = {
        allPages: "0",
        selectedPages: "1"
    };
    
    this._registeredHandlers = [];

    this._bodyCssClass = null;
    this._applyWorkflowSelector = null;
    this._pageSelector = null;
    this._selectedPagesPanel = null;
    this._applyToChildPagesCheckbox = null;
    this._doneButton = null;
    this._cancelButton = null;

    this._onPageLoadDelegate = null;
}
Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog.callBaseMethod(this, "initialize");
        scopePageSelectorDialog = this;

        $(this._selectors.body).addClass(this.get_bodyCssClass());

        this._onPageLoadDelegate = this.createDelegate(this._onPageLoadHandler);
        Sys.Application.add_load(this._onPageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog.callBaseMethod(this, "dispose");

        this._registeredHandlers.forEach(function (handler) {
            $removeHandler(handler.element, handler.event, handler.delegate);
        });

        Sys.Application.remove_load(this._onPageLoadDelegate);
    },

    /* **************** public methods **************** */

    createDialog: function (selectedPagesIds, applyToChildPages, site, culture) {
        $(this._selectors.siteNameHeader).text(site.name);

        var pageSelector = this.get_pageSelector();

        pageSelector.changeSite(site.id, site.rootNodeId);
        pageSelector.changeCulture(culture);

        if (selectedPagesIds && selectedPagesIds.length) {
            // "selectedPagesIds" might be array-like object
            var selectedPageIdsArray = Array.prototype.slice.call(selectedPagesIds);
            pageSelector.set_selectedItemIds(selectedPageIdsArray);

            var $applyWorkflowToSelectedPagesElement = $(this.getApplyWorkflowElementByValue(this._applyWorkflowSelectorValues.selectedPages));
            $applyWorkflowToSelectedPagesElement.prop("checked", true);
            $applyWorkflowToSelectedPagesElement.trigger("change");
        } else {
            this._toggleSelectedPagesPanelCheckboxes(true);
        }

        this.get_applyToChildPagesCheckbox().checked = !!applyToChildPages;

        pageSelector.dataBind();
    },

    createDelegate: function (handler) {
        return Function.createDelegate(this, handler);
    },

    /* **************** private methods **************** */

    _addHandler: function (element, handler, event) {
        var delegate = Function.createDelegate(this, handler);
        $addHandler(element, event, delegate);

        this._registeredHandlers.push({
            element: element,
            event: event,
            delegate: delegate
        });
    },

    getApplyWorkflowElementByValue: function (value) {
        return $(this.get_applyWorkflowSelector()).find("input[value=" + value + "]")[0];
    },

    /* **************** event handlers **************** */

    _onPageLoadHandler: function () {
        var applyWorkflowSelectorInputElements = $(this.get_applyWorkflowSelector()).find("input");
        applyWorkflowSelectorInputElements.on("change", this._onApplyWorkflowSelectorChangeHandler.bind(this));

        this._addHandler(this.get_doneButton(), this._onDoneHandler, "click");
        this._addHandler(this.get_cancelButton(), this._onCancelHandler, "click");
    },

    _onApplyWorkflowSelectorChangeHandler: function (ev) {
        if (ev.target.value === this._applyWorkflowSelectorValues.selectedPages) {
            $(this.get_selectedPagesPanel()).removeClass("sfSelectorDisabled");
            this._toggleSelectedPagesPanelCheckboxes(false);
        } else {
            this._toggleSelectedPagesPanelCheckboxes(true);
            $(this.get_selectedPagesPanel()).addClass("sfSelectorDisabled");
        }

        this.resizeToContent();
    },

    _toggleSelectedPagesPanelCheckboxes: function (isDisabled) {
        $(this.get_selectedPagesPanel()).find("input[type=checkbox]").each(function () {
            $(this).prop("disabled", isDisabled);
        });
    },

    _onDoneHandler: function () {
        var applyToChildPages = this.get_applyToChildPagesCheckbox().checked;
        var selectedPagesIds = [];

        if (this.get_selectedApplyWorkflowValue() === this._applyWorkflowSelectorValues.selectedPages) {
            var selectedItems = this.get_pageSelector().get_selectedItems();
            selectedPagesIds = selectedItems.map(function (item) {
                return item.Id;
            });
        }

        var data = {
            selectedPagesIds: selectedPagesIds,
            applyToChildPages: applyToChildPages
        };

        this.close(data);
    },

    _onCancelHandler: function () {
        this.close();
    },

    /* **************** properties **************** */

    get_selectedApplyWorkflowValue: function () {
        return $(this.get_applyWorkflowSelector()).find(this._selectors.checkedRadioBtn)[0].value;
    },

    get_bodyCssClass: function () {
        return this._bodyCssClass;
    },
    set_bodyCssClass: function (value) {
        this._bodyCssClass = value;
    },

    get_applyWorkflowSelector: function () {
        return this._applyWorkflowSelector;
    },
    set_applyWorkflowSelector: function (value) {
        this._applyWorkflowSelector = value;
    },

    get_pageSelector: function () {
        return this._pageSelector;
    },
    set_pageSelector: function (value) {
        this._pageSelector = value;
    },

    get_selectedPagesPanel: function () {
        return this._selectedPagesPanel;
    },
    set_selectedPagesPanel: function (value) {
        this._selectedPagesPanel = value;
    },

    get_applyToChildPagesCheckbox: function () {
        return this._applyToChildPagesCheckbox;
    },
    set_applyToChildPagesCheckbox: function (value) {
        this._applyToChildPagesCheckbox = value;
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
}

Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog.registerClass('Telerik.Sitefinity.Workflow.UI.ScopePageSelectorDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);