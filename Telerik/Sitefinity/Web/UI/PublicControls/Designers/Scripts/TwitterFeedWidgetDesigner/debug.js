﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner = function (element) {

    this._pageSelector = null;
    this._pageSelectorBinder = null;

    this._publishingPointProvider = null;
    this._pipeName = null;

    this._publishingPointProvider = null;
    this._pipeName = null;

    this._loadDelegate = null;
    this._MaxItems = null;
    this._pageSelectorBinderIsDataBound = false;
    this._pageSelectorBinderDelegate = null;


    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._templateSelector = null;

    this._editTemplateLinkClickDelegate = null;
    this._createTemplateLinkClickDelegate = null;
    this._widgetEditorDialogUrl = null;
    this._widgetEditorDialog = null;

    this._editTemplateViewName = null;
    this._createTemplateViewName = null;
    this._modifyWidgetTemplatePermission = null;

    Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner.initializeBase(this, [element]);
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner.callBaseMethod(this, 'initialize');

        this._loadDelegate = Function.createDelegate(this, this._loadHandler);
        Sys.Application.add_load(this._loadDelegate);

        this._pageSelectorBinderDelegate = Function.createDelegate(this, this._pageSelectorBinder_DataBound);
        this._pageSelectorBinder.add_onDataBound(this._pageSelectorBinderDelegate);


        this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        jQuery(this.get_editTemplateLink()).click(this._editTemplateLinkClickDelegate);

        this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        jQuery(this.get_createTemplateLink()).click(this._createTemplateLinkClickDelegate);

    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner.callBaseMethod(this, 'dispose');

        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._pageSelectorBinderDelegate) {
            this._pageSelectorBinder.remove_onDataBound(this._pageSelectorBinderDelegate);
            delete this._pageSelectorBinderDelegate;
        }

    },

    /* ************************* public methods ************************* */
    // forces the designer to apply the changes on UI to the cotnrol data
    applyChanges: function () {
        var controlData = this.get_controlData();

        var selectedIndex = this.get_pipeSelectorBinder().get_selectedItem();
        if (selectedIndex) {
            controlData.TwitterPipeId = selectedIndex.ID;
        }

        controlData.MaxItems = this.get_MaxItems().get_value();

        controlData.TemplateKey = this.get_templateSelector().get_value();

    },
    _pageSelectorBinder_DataBound: function () {
        this._pageSelectorBinderIsDataBound = true;
        if (this.get_controlData().TwitterPipeId) {
            this.get_pipeSelectorBinder().set_selectedItem('ID', this.get_controlData().TwitterPipeId);

        }
    },


    _editTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = this.get_templateSelector().get_value();

            this._openEditTemplateDialog();
        } else {
            alert("You don't have the permissions to edit widgets templates.");
        }
    },

    _createTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = null;

            this._openCreateTemplateDialog();
        } else {
            alert("You don't have the permissions to create new widgets templates.");
        }
    },

    _openCreateTemplateDialog: function (condition) {
        if (this._widgetEditorDialog) {
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
            if (condition) {
                var url = new Sys.Uri(dialogUrl);
                url.get_query().Condition = condition;
                dialogUrl = url.toString();
            }
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            dialogBase.get_radWindow().maximize();
            this._widgetEditorDialog.show();
            this._widgetEditorDialog.maximize();
            $("body").removeClass("sfSelectorDialog");
        }
    },

    _openEditTemplateDialog: function () {
        if (this._widgetEditorDialog) {
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            dialogBase.get_radWindow().maximize();
            this._widgetEditorDialog.show();
            this._widgetEditorDialog.maximize();
            $("body").removeClass("sfSelectorDialog");
        }
    },

    _configureTemplateEditorDialog: function () {
        this._assignTemplateEditorDialogHandlers();

        var dialogUrl = this._widgetEditorDialogUrl;
        this.get_widgetEditorDialog().set_navigateUrl(dialogUrl);
        this.get_widgetEditorDialog().add_close(this._onWidgetEditorClosedDelegate);
        this.get_widgetEditorDialog().add_pageLoad(this._widgetEditorShowDelegate);
    },

    // forces the designer to refresh the UI from the cotnrol data
    refreshUI: function () {
        var controlData = this.get_controlData();

        if (controlData.TwitterPipeId) {
            if (this._pageSelectorBinderIsDataBound) {
                this.get_pipeSelectorBinder().set_selectedItem('ID', this.get_controlData().TwitterPipeId);
            }
        }

        if (controlData.MaxItems != 'undefined')
            this.get_MaxItems().set_value(controlData.MaxItems);

        if (controlData.TemplateKey) {
            this.get_templateSelector().set_value(controlData.TemplateKey);
        }


    },


    /* ************************* private methods ************************* */
    _loadHandler: function () {
        var urlParams = this._pageSelectorBinder.get_urlParams();
        urlParams['providerName'] = this._publishingPointProvider;
        urlParams['pipeTypeName'] = this._pipeName;
        this._pageSelectorBinder.DataBind();
    },

    /* ************************* properties ************************* */
    get_pipeSelector: function () {
        return this._pageSelector;
    },
    set_pipeSelector: function (value) {
        this._pageSelector = value;
    },
    get_pipeSelectorBinder: function () {
        return this._pageSelectorBinder;
    },
    set_pipeSelectorBinder: function (value) {
        this._pageSelectorBinder = value;
    },

    get_MaxItems: function () {
        return this._MaxItems;
    },
    set_MaxItems: function (value) {
        this._MaxItems = value;
    },


    get_editTemplateLink: function () {
        return this._editTemplateLink;
    },

    set_editTemplateLink: function (value) {
        this._editTemplateLink = value;
    },

    get_createTemplateLink: function () {
        return this._createTemplateLink;
    },
    set_createTemplateLink: function (value) {
        this._createTemplateLink = value;
    },
    get_templateSelector: function () {
        return this._templateSelector;
    },

    set_templateSelector: function (value) {
        this._templateSelector = value;
    },
    get_widgetEditorDialog: function () {
        return this._widgetEditorDialog;
    },

    set_widgetEditorDialog: function (value) {
        this._widgetEditorDialog = value;
    },


    get_selectedPageLabel: function () {
        return this._selectedPageLabel;
    },
    set_selectedPageLabel: function (value) {
        this._selectedPageLabel = value;
    },

    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    }
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
