﻿var workflowTypeSelectorDialog = null;

window.createDialog = function () {
    if (workflowTypeSelectorDialog) {
        workflowTypeSelectorDialog.createDialog();
    }
};

Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog.initializeBase(this, [element]);

    this._selectors = {
        body: "body",
        checkedRadioBtn: "input:checked"
    };

    this._levelsOfApprovalSelectorValues = {
        oneLevel: "1",
        twoLevels: "2",
        threeLevels: "4",
        noApprovalWorkflow: "0"
    };

    this._registeredHandlers = [];
    this._levelsOfApproval = [];

    this._labelManager = null;
    this._bodyCssClass = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._levelsOfApprovalSelector = null;
}

Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog.callBaseMethod(this, "initialize");
        workflowTypeSelectorDialog = this;

        $(this._selectors.body).addClass(this.get_bodyCssClass());

        this._addHandler(this.get_doneButton(), this._doneClickHandler, "click");
        this._addHandler(this.get_cancelButton(), this._closeButtonClickHandler, "click");
        this._addHandler(this.get_levelsOfApprovalSelector(), this._onLevelsOfApprovalSelectorChange, "change");
    
        this._onPageLoadDelegate = Function.createDelegate(this, this._onPageLoadHandler);
        Sys.Application.add_load(this._onPageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* **************** public methods **************** */

    createDialog: function () { },

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

    _changeDoneBtnLabel: function (inputValue) {
        var $doneButton = $(this.get_doneButton());

        if (inputValue === this._levelsOfApprovalSelectorValues.noApprovalWorkflow) {
            var buttonLabel = this.get_labelManager().getLabel("Labels", "Done");
            $doneButton.text(buttonLabel);
        } else {
            var nextLabel = this.get_labelManager().getLabel("Labels", "Next");
            var setApproversLabel = this.get_labelManager().getLabel("WorkflowResources", "SetApprovers");
            var buttonLabel = nextLabel + ": " + setApproversLabel;
            $doneButton.text(buttonLabel);
        }
    },

    _initializeUI: function () {
        var radioListItems = $(this.get_levelsOfApprovalSelector()).find("li");
        radioListItems.slice(0, 2).addClass("sfMBottom5");
        radioListItems.slice(2, 3).addClass("sfMBottom25");

        this._changeDoneBtnLabel(this._levelsOfApprovalSelectorValues.oneLevel);
    },

    /* **************** event handlers **************** */

    _onPageLoadHandler: function () {
        this._initializeUI();
    },

    _doneClickHandler: function () {
        this.close(this.get_selectedLevelOfApproval().value);
    },

    _closeButtonClickHandler: function () {
        this.close();
    },

    _onLevelsOfApprovalSelectorChange: function (sender) {
        this._changeDoneBtnLabel(sender.target.value);
    },

    /* **************** properties **************** */

    get_selectedLevelOfApproval: function () {
        return $(this.get_levelsOfApprovalSelector()).find(this._selectors.checkedRadioBtn)[0] || null;
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },

    get_bodyCssClass: function () {
        return this._bodyCssClass;
    },
    set_bodyCssClass: function (value) {
        this._bodyCssClass = value;
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

    get_levelsOfApprovalSelector: function () {
        return this._levelsOfApprovalSelector;
    },
    set_levelsOfApprovalSelector: function (value) {
        this._levelsOfApprovalSelector = value;
    }
}

Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowTypeSelectorDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);