﻿Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView = function (element) {
    Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView.initializeBase(this, [element]);
    this._contentType = null;
    this._singleContentName = null;
    this._pluralConentName = null;
    this._parentContentName = null;
    this._clientLabelManager = null;
    this._itemPluralNameMap = [];
    this._parentItemTypeMap = [];

    this._allContentRadioId = null;
    this._selectionOfContentRadioId = null;
    this._parentRadioId = null;
    this._selectedRadioId = null;
    this._advancedSelectionRadioId = null;
    this._filterSelector = null;
    this._viewTitleElement = null;
    this._parentContentSelector = null;
    this._selectParentContentLink = null;
    this._parentContentSelectorWrapper = null;
    this._parentContentSelectionContainer = null;

    this._pageLoadHandlerDelegate = null;
    this._radioButtonClickDelegate = null;
    this._showParentContentSelectorDelegate = null;
    this._parentContentSelectedDelegate = null;

    this._dataItem = null;
    this._selectedParent = null;
}

Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView.callBaseMethod(this, "initialize");

        this._itemPluralNameMap["Telerik.Sitefinity.Blogs.Model.BlogPost"] = this._clientLabelManager.getLabel("BlogResources", "BlogPluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.News.Model.NewsItem"] = this._clientLabelManager.getLabel("NewsResources", "NewsPluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.GenericContent.Model.ContentItem"] = this._clientLabelManager.getLabel("ContentResources", "ContentPluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.Libraries.Model.Video"] = this._clientLabelManager.getLabel("VideosResources", "VideoPluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.Libraries.Model.Image"] = this._clientLabelManager.getLabel("ImagesResources", "ImagePluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.Libraries.Model.Document"] = this._clientLabelManager.getLabel("DocumentsResources", "DocumentPluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.Forms.Model.Form"] = this._clientLabelManager.getLabel("FormsResources", "FormPluralItemName");
        this._itemPluralNameMap["Telerik.Sitefinity.Events.Model.Event"] = this._clientLabelManager.getLabel("EventsResources", "EventPluralItemName");

        this._parentItemTypeMap["Telerik.Sitefinity.Blogs.Model.BlogPost"] = "Telerik.Sitefinity.Blogs.Model.Blog";
        //this._contentItemNameMap["News"] = "Telerik.Sitefinity.News.Model.NewsItem";
        //this._contentItemNameMap["GenericContent"] = "Telerik.Sitefinity.GenericContent.Model.ContentItem";
        this._parentItemTypeMap["Telerik.Sitefinity.Libraries.Model.Video"] = "Telerik.Sitefinity.Libraries.Model.VideoLibrary";
        this._parentItemTypeMap["Telerik.Sitefinity.Libraries.Model.Image"] = "Telerik.Sitefinity.Libraries.Model.Album";
        this._parentItemTypeMap["Telerik.Sitefinity.Libraries.Model.Document"] = "Telerik.Sitefinity.Libraries.Model.DocumentLibrary";
        //this._contentItemNameMap["Forms"] = "Blog";
        //this._contentItemNameMap["Events"] = "Event";

        if (this._pageLoadHandlerDelegate == null) {
            this._pageLoadHandlerDelegate = Function.createDelegate(this, this._pageLoadHandler);
        }
        if (this._radioButtonClickDelegate == null) {
            this._radioButtonClickDelegate = Function.createDelegate(this, this._radioButtonClick);
        }
        $addHandler($get(this._selectionOfContentRadioId), "click", this._radioButtonClickDelegate);
        $addHandler($get(this._allContentRadioId), "click", this._radioButtonClickDelegate);
        $addHandler($get(this._parentRadioId), "click", this._radioButtonClickDelegate);
        $addHandler($get(this._advancedSelectionRadioId), "click", this._radioButtonClickDelegate);

        if (this._showParentContentSelectorDelegate == null) {
            this._showParentContentSelectorDelegate = Function.createDelegate(this, this._showParentContentSelector);
        }
        $addHandler(this._selectParentContentLink, "click", this._showParentContentSelectorDelegate);

        if (this._parentContentSelectedDelegate == null) {
            this._parentContentSelectedDelegate = Function.createDelegate(this, this._parentContentSelected);
        }
        this._parentContentSelector.add_doneClientSelection(this._parentContentSelectedDelegate);

        Sys.Application.add_load(this._pageLoadHandlerDelegate);
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView.callBaseMethod(this, "dispose");
        if (this._pageLoadHandlerDelegate)
            delete this._pageLoadHandlerDelegate;
        if (this._radioButtonClickDelegate)
            delete this._radioButtonClickDelegate;
        if (this._showParentContentSelectorDelegate)
            delete this._showParentContentSelectorDelegate;
    },

    /* ************** private methods **************** */

    _pageLoadHandler: function (sender, args) {
        if (this._selectedRadioId == null) {
            this._selectRadioButton(this._allContentRadioId);
        }
        if (this._selectedRadioId != this._selectionOfContentRadioId) {
            this._filterSelector.set_disabled(true);
        }
    },

    _radioButtonClick: function (sender, args) {
        switch (sender.target.id) {
            case this._selectionOfContentRadioId:
                this._filterSelector.set_disabled(false);
                jQuery(this._parentContentSelectionContainer).hide();
                break;
            case this._parentRadioId:
                this._filterSelector.set_disabled(true);
                this._showParentSelectorIfRelevant();
                break;
            case this._allContentRadioId:
                this._filterSelector.set_disabled(true);
                jQuery(this._parentContentSelectionContainer).hide();
                break;
            case this._advancedSelectionRadioId:
                this._filterSelector.set_disabled(true);
                jQuery(this._parentContentSelectionContainer).hide();
                break;
        }
        this._selectedRadioId = sender.target.id;
        dialogBase.resizeToContent();
    },

    _showParentSelectorIfRelevant: function () {
        if (this._parentItemTypeMap[this._contentType]) {
            jQuery(this._parentContentSelectionContainer).show();
        }
    },

    _selectRadioButton: function (radioButtonId) {
        jQuery("#" + radioButtonId).click();
        this._selectedRadioId = radioButtonId;
    },

    _showParentContentSelector: function () {
        jQuery(this._parentContentSelectorWrapper).show();
        dialogBase.resizeToContent();
    },

    _parentContentSelected: function (items) {
        jQuery(this._parentContentSelectorWrapper).hide();

        if (items == null)
            return;
        if (items.length != 1)
            throw "Please select only one parent.";
        this._selectedParent = items[0];
    },

    /* ************** public methods ***************** */

    refreshUI: function () {
        this._pluralContentName = this._itemPluralNameMap[this._contentType];

        this._filterSelector.set_queryData(null);
        if (this._contentType != null && this._parentItemTypeMap[this._contentType] != null) {
            this._parentContentSelector.set_itemType(this._parentItemTypeMap[this._contentType]);
            this._parentContentSelector.dataBind();
        }

        this._viewTitleElement.innerHTML = String.format(this._clientLabelManager.getLabel("WorkflowResources", "WhichContentToInclude"), this.get_pluralContentName());

        var allContentText = this._clientLabelManager.getLabel("WorkflowResources", "AllContent");
        var allContentLabelSelector = "label[for='" + this.get_allContentRadioId() + "']";
        var jAllContentTitle = jQuery(allContentLabelSelector).get(0);
        jAllContentTitle.innerHTML = String.format(allContentText, this.get_pluralContentName());

        var fromSelectedParentText = this._clientLabelManager.getLabel("WorkflowResources", "FromSelectedParent");
        var parentLabelSelector = "label[for='" + this.get_parentRadioId() + "']";
        var jParentRadioTitle = jQuery(parentLabelSelector).get(0);
        jParentRadioTitle.innerHTML = String.format(fromSelectedParentText, "parent");

        var selectionOfContentText = this._clientLabelManager.getLabel("WorkflowResources", "SelectionOfContent");
        var selectionOfContentLabelSelector = "label[for='" + this.get_selectionOfContentRadioId() + "']";
        var jSelectionOfContentTitle = jQuery(selectionOfContentLabelSelector).get(0);
        jSelectionOfContentTitle.innerHTML = String.format(selectionOfContentText, this.get_pluralContentName());

        dialogBase.resizeToContent();
    },

    reset: function () {
        this.set_contentType(null);
        this._selectRadioButton(this._allContentRadioId);
    },

    getDataItem: function () {
        this._dataItem = {};
        switch (this._selectedRadioId) {
            case this._parentRadioId:
                // TODO: Implement selecting the parent in a dialog
                this._dataItem.Parent = this._selectedParent;
                break;
            case this._selectionOfContentRadioId:
                this._filterSelector.applyChanges();
                var queryData = this._filterSelector.get_queryData();
                queryData = Telerik.Sitefinity.JSON.stringify(queryData);
                this._dataItem = queryData;
                break;
            case this._allContentRadioId:
                this._dataItem = null;
            default:
                break;
        }
        return this._dataItem;
    },

    /* ************** properties ********************* */
    get_contentType: function () {
        return this._contentType;
    },

    set_contentType: function (value) {
        this._contentType = value;
        this.refreshUI();
    },

    // Gets the name of a single item used in the user interface.
    get_singleContentName: function () {
        return this._singleContentName;
    },

    // Sets the name of a single item used in the user interface.
    set_singleContentName: function (value) {
        if (value != null) {
            this._singleContentName = value;
        }
    },

    // Gets the name for an item in plural. Used in the user interface.
    get_pluralContentName: function () {
        return this._pluralContentName;
    },

    // Setsthe name for an item in plural. Used in the user interface.
    set_pluralContentName: function (value) {
        if (value != null) {
            this._pluralContentName = value;
        }
    },

    // Gets the name of the parent item where applicable. Used in the user interface.
    get_parentContentName: function () {
        return this._parentContentName;
    },

    // Sets the name of the parent item where applicable. Used in the user interface.
    set_parentContentName: function (value) {
        if (value != null) {
            this._parentContentName = value;
        }
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        if (value != null) {
            this._clientLabelManager = value;
        }
    },

    get_allContentRadioId: function () {
        return this._allContentRadioId;
    },

    set_allContentRadioId: function (value) {
        if (value != null) {
            this._allContentRadioId = value;
        }
    },

    get_selectionOfContentRadioId: function () {
        return this._selectionOfContentRadioId;
    },

    set_selectionOfContentRadioId: function (value) {
        if (value != null) {
            this._selectionOfContentRadioId = value;
        }
    },

    get_parentRadioId: function () {
        return this._parentRadioId;
    },

    set_parentRadioId: function (value) {
        if (value != null) {
            this._parentRadioId = value;
        }
    },

    get_advancedSelectionRadioId: function () {
        return this._advancedSelectionRadioId;
    },

    set_advancedSelectionRadioId: function (value) {
        if (value != null) {
            this._advancedSelectionRadioId = value;
        }
    },

    get_filterSelector: function () {
        return this._filterSelector;
    },

    set_filterSelector: function (value) {
        if (value != null) {
            this._filterSelector = value;
        }
    },

    get_viewTitleElement: function () {
        return this._viewTitleElement;
    },

    set_viewTitleElement: function (value) {
        if (value != null) {
            this._viewTitleElement = value;
        }
    },

    get_parentContentSelector: function () {
        return this._parentContentSelector;
    },

    set_parentContentSelector: function (value) {
        if (value != null) {
            this._parentContentSelector = value;
        }
    },

    get_selectParentContentLink: function () {
        return this._selectParentContentLink;
    },

    set_selectParentContentLink: function (value) {
        if (value != null) {
            this._selectParentContentLink = value;
        }
    },

    get_parentContentSelectorWrapper: function () {
        return this._parentContentSelectorWrapper;
    },

    set_parentContentSelectorWrapper: function (value) {
        if (value != null) {
            this._parentContentSelectorWrapper = value;
        }
    },

    get_parentContentSelectionContainer: function () {
        return this._parentContentSelectionContainer;
    },

    set_parentContentSelectionContainer: function (value) {
        if (value != null) {
            this._parentContentSelectionContainer = value;
        }
    }
};

Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView.registerClass('Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView', Sys.UI.Control);
