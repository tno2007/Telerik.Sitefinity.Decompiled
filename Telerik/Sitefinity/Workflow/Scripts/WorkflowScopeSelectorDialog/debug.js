﻿Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

var workflowScopeDialog = null;

Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog.initializeBase(this, [element]);

    workflowScopeDialog = this;

    this._cancelSelectingLink = null;
    this._doneSelectingLink = null;
    this._workflowScopeTitleField = null;
    this._languageList = null;
    this._contentTypeList = null;
    this._pagesViewWrapper = null;
    this._contentViewWrapper = null;
    this._contentView = null;
    this._pagesView = null;

    this._selectContentDelegate = null;
    this._doneSelectingDelegate = null;
    this._cancelSelectingDelegate = null;
}

Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog.callBaseMethod(this, "initialize");

        this._selectContentDelegate = Function.createDelegate(this, this._selectContentHandler);
        $addHandler(this._contentTypeList, "change", this._selectContentDelegate);

        this._doneSelectingDelegate = Function.createDelegate(this, this._doneSelecting);
        $addHandler(this._doneSelectingLink, "click", this._doneSelectingDelegate);

        this._cancelSelectingDelegate = Function.createDelegate(this, this._cancelSelecting);
        $addHandler(this._cancelSelectingLink, "click", this._cancelSelectingDelegate);
    },

    // tear down
    dispose: function () {
        if (this._selectContentDelegate) {
            if (this._contentTypeList) {
                $removeHandler(this._contentTypeList, "change", this._selectContentDelegate);
            }
            delete this._selectContentDelegate;
        }
        if (this._doneSelectingDelegate) {
            if (this._doneSelectingLink) {
                $removeHandler(this._doneSelectingLink, "click", this._doneSelectingDelegate);
            }
            delete this._doneSelectingDelegate;
        }
        if (this._cancelSelectingDelegate) {
            if (this._cancelSelectingLink) {
                $removeHandler(this._cancelSelectingLink, "click", this._cancelSelectingDelegate);
            }
            delete this._cancelSelectingDelegate;
        }
        Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* ************** public methods **************** */

    reset: function () {
        this._workflowScopeTitleField.reset();
        this._languageList.selectedIndex = 0;
        this._contentTypeList.selectedIndex = 0;
        this._selectContent("ALL_CONTENT");
        this._pagesView.reset();
        this._contentView.reset();
    },

    /* ************** private methods **************** */

    _selectContentHandler: function (sender, args) {
        this._selectContent(sender.target.value);
    },

    _selectContent: function (value) {
        switch (value) {
            case "Telerik.Sitefinity.Pages.Model.PageNode":
                jQuery(this._contentViewWrapper).hide();
                jQuery(this._pagesViewWrapper).show();
                break;
            case "ALL_CONTENT":
                jQuery(this._contentViewWrapper).hide();
                jQuery(this._pagesViewWrapper).hide();
                this._contentView.set_contentType(value);
                break;
            default:
                jQuery(this._contentViewWrapper).show();
                jQuery(this._pagesViewWrapper).hide();
                this._contentView.set_contentType(value);
                break;
        }
        dialogBase.resizeToContent();
    },

    _doneSelecting: function (sender, args) {
        var language = this._languageList.value;
        var contentType = this._contentTypeList.value;
        var currentView = this._getCurrentView(contentType);
        var dataItem = null;
        if (currentView) {
            dataItem = currentView.getDataItem();
        }
        var argument = {
            Language: language,
            ContentType: contentType,
            DataItem: dataItem,
            Title: this.get_workflowScopeTitleField().get_value()
        };
        this.close(argument);
    },

    _cancelSelecting: function (sender, args) {
        this.close();
    },

    _getCurrentView: function (contentType) {
        if (contentType == "Telerik.Sitefinity.Pages.Model.PageNode") {
            return this._pagesView;
        }
        else if (contentType == "ALL_CONTENT") {
            return null;
        }
        else {
            return this._contentView;
        }
    },

    /* ************** properties ********************* */

    get_cancelSelectingLink: function () {
        return this._cancelSelectingLink;
    },
    set_cancelSelectingLink: function (value) {
        this._cancelSelectingLink = value;
    },

    get_doneSelectingLink: function () {
        return this._doneSelectingLink;
    },
    set_doneSelectingLink: function (value) {
        this._doneSelectingLink = value;
    },

    get_workflowScopeTitleField: function () {
        return this._workflowScopeTitleField;
    },
    set_workflowScopeTitleField: function (value) {
        this._workflowScopeTitleField = value;
    },

    get_languageList: function () {
        return this._languageList;
    },
    set_languageList: function (value) {
        this._languageList = value;
    },

    get_contentTypeList: function () {
        return this._contentTypeList;
    },
    set_contentTypeList: function (value) {
        this._contentTypeList = value;
    },

    get_contentViewWrapper: function () {
        return this._contentViewWrapper;
    },
    set_contentViewWrapper: function (value) {
        this._contentViewWrapper = value;
    },

    get_pagesViewWrapper: function () {
        return this._pagesViewWrapper;
    },
    set_pagesViewWrapper: function (value) {
        this._pagesViewWrapper = value;
    },

    get_contentView: function () {
        return this._contentView;
    },
    set_contentView: function (value) {
        this._contentView = value;
    },

    get_pagesView: function () {
        return this._pagesView;
    },
    set_pagesView: function (value) {
        this._pagesView = value;
    }
};

Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowScopeSelectorDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);