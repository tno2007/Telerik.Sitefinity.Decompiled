Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid.initializeBase(this, [element]);

    this._eventNamespace = ".workflowScope";

    this._selectors = {
        workflowScopeWrapper: "#workflowScopeWrapper",
        workflowScopeGrid: "#workflowScopeGrid",
        gridRowTemplate: "#workflowGridRowTemplate",

        workflowScopeAddBtn: ".workflowScopeAddBtn",
        workflowScopeRemoveBtn: ".workflowScopeRemoveBtn",
        workflowChangeScopeBtn: ".workflowChangeScopeBtn",
        workflowPageSelectorBtn: ".workflowPageSelectorBtn",
        workflowScopeChangeBtn: ".workflowScopeChangeBtn"
    };

    this._contentScopeValues = null;
    this._langScopeValues = null;
    this._siteScopeValues = null;
    this._dialogManager = null;
    this._labelManager = null;
    this._addScopeBtnLabel = null;
    this._dataSource = null;
    this._selectAll = false;

    this._isMultisiteMode = null;
    this._isMultilingual = null;
    this._currentSite = null;

    this._workflowScope = null;
    this._scopesErrorMessage = null;

    this._gridDataBoundDelegate = null;
    this._dataSourceChangeDelegate = null;
}

Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid.callBaseMethod(this, "initialize");

        this._initializeEventHandlers();
        this._initializeDialogHandlers();

        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);
        this._dataSourceChangeDelegate = Function.createDelegate(this, this._dataSourceChangeHandler);

    },

    dispose: function () {
        this._disposeEventHandlers();
        // TODO: this._disposeDialogHandlers();

        Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid.callBaseMethod(this, "dispose");
    },

    /* **************** public methods **************** */

    create: function (dataItem) {
        this._initializeDataSource(dataItem);
        this._initializeGrid();
    },

    showScopesErrorMsg: function () {
        var hasScopes = this.get_scopes().length > 0;
        if (!hasScopes)
            jQuery(this.get_scopesErrorMessage()).show();
        else
            jQuery(this.get_scopesErrorMessage()).hide();

        return hasScopes;
    },

    /* **************** private methods **************** */

    _initializeDataSource: function (dataItem) {
        // datasource must be workflow scope object from WorkflowForm
        if (!dataItem) return;

        var scopes = dataItem.Scopes;
        this._setScopeBtnLabel(scopes.length);

        this._dataSource = new kendo.data.DataSource({
            data: scopes,
            sort: {
                field: "Language", dir: "asc", compare: function (a, b) {
                    if (!a.Language.length)
                        return -1;

                    if (!b.Language.length)
                        return 1;

                    return a.Language[0].Culture == b.Language[0].Culture ? 0 : (a.Language[0].Culture > b.Language[0].Culture) ? 1 : -1;
                },
                field: "Site", dir: "asc", compare: function (a, b) {
                    if (a.Site == null)
                        return -1;

                    if (b.Site == null)
                        return 1;

                    return a.Site.Name == b.Site.Name ? 0 : (a.Site.Name > b.Site.Name) ? 1 : -1;
                }
            }
        });
    },

    _initializeGrid: function () {
        var gridRowTemplate = jQuery(this.get_selectors().gridRowTemplate).html();
        var compileGridRowTemplate = kendo.template(gridRowTemplate);

        jQuery(this.get_selectors().workflowScopeGrid).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(compileGridRowTemplate, this),
            scrollable: false,
            editable: true,
            autobind: true,
            dataBound: this._gridDataBoundDelegate
        });
    },

    _initializeEventHandlers: function () {
        var selectors = this.get_selectors();

        this._addHandler("click", selectors.workflowScopeAddBtn, this._onAddScopeButtonClick);
        this._addHandler("click", selectors.workflowScopeRemoveBtn, this._onRemoveScopeButtonClick, true);
        this._addHandler("click", selectors.workflowChangeScopeBtn, this._onChangeScopeButtonCLick, true);
        this._addHandler("click", selectors.workflowPageSelectorBtn, this._onPageSelectorBtnClick, true);
        this._addHandler("click", selectors.workflowScopeChangeBtn, this._onChangeScopeButtonCLick, true);
    },

    _initializeDialogHandlers: function () {
        this.get_dialogManager().addHandler(this.get_scopeDefinitionDialog(), "close", this._onScopeDefinitionDialogClose.bind(this));
        this.get_dialogManager().addHandler(this.get_pageSelectorDialog(), "close", this._onPageSelectorDialogClose.bind(this));
    },

    _disposeEventHandlers: function () {
        var selectors = this.get_selectors();

        this._removeHandlers(selectors.workflowScopeWrapper);
        this._removeHandlers(selectors.workflowScopeAddBtn);
        this._removeHandlers(selectors.workflowScopeRemoveBtn);
        this._removeHandlers(selectors.workflowChangeScopeBtn);
        this._removeHandlers(selectors.workflowPageSelectorBtn);
        this._removeHandlers(selectors.workflowScopeChangeBtn);
    },

    _addHandler: function (eventName, selector, handler, shouldBindDynamically) {
        var boundHandler = handler.bind(this);
        var eventFullName = eventName + this._eventNamespace;

        if (shouldBindDynamically) {
            jQuery(this.get_selectors().workflowScopeWrapper).on(eventFullName, selector, boundHandler);
        } else {
            jQuery(selector).on(eventFullName, boundHandler);
        }
    },

    _removeHandlers: function (selector) {
        jQuery(selector).off(this._eventNamespace);
    },

    _getWorkflowScopeViewModel: function () {
        return {
            Site: {
                Name: null,
                SiteId: null,
                SiteMapRootNodeId: null
            },
            Language: [{
                Culture: null,
                Key: null
            }],
            TypeScopes: [{
                ContentFilter: [],
                IncludeChildren: false,
                ContentType: null,
                Title: null,
                Id: Telerik.Sitefinity.getEmptyGuid()
            }]
        };
    },

    _getScopeDialogObj: function (item, mode) {
        return {
            wfScopes: this.get_scopes(),
            wfScope: item,
            mode: mode
        };
    },

    _setScopeBtnLabel: function (scopeLength) {
        var addScopeBtnlabel = jQuery(this.get_addScopeBtnLabel());
        if (scopeLength > 0)
            addScopeBtnlabel.text(this.get_labelManager().getLabel("WorkflowResources", "AddScopeButtonLabel"));
        else
            addScopeBtnlabel.text(this.get_labelManager().getLabel("WorkflowResources", "DefineScopeButtonLabel"));
    },

    _updateGridColumns: function () {
        //TODO: rethink this
        this.get_kendoGrid().showColumn("language");
        this.get_kendoGrid().showColumn("site");

        if (!this.get_isMultilingual())
            this.get_kendoGrid().hideColumn("language");

        if (!this.get_isMultisiteMode())
            this.get_kendoGrid().hideColumn("site");
    },

    _getPageNodeItem: function (typeScopes) {
        return $workflow.getPageNodeItem(typeScopes);
    },
    /* **************** event handlers **************** */

    _gridDataBoundHandler: function (e) {
        var data = this.get_dataSource().data();
        if (data.length > 0) {
            jQuery(this.get_kendoGrid().element).show();
            jQuery(".sfActionsMenu").kendoMenu({
                animation: false,
                openOnClick: {
                    rootMenuItems: true
                }
            });

            this._updateGridColumns();
        } else {
            jQuery(this.get_kendoGrid().element).hide();
        }

        this.get_dataSource().bind("change", this._dataSourceChangeDelegate);
    },

    _dataSourceChangeHandler: function (e) {
        this.showScopesErrorMsg();
    },

    _onAddScopeButtonClick: function () {
        var scopeSelectorDialog = this.get_scopeDefinitionDialog();
        this.get_dialogManager().openDialog(scopeSelectorDialog, [this._getScopeDialogObj(this._getWorkflowScopeViewModel(), "create")]);
    },

    _onRemoveScopeButtonClick: function (ev) {
        var dataItem = $workflow.getDataItem(ev, this.get_dataSource());
        if (dataItem) {
            this.get_dataSource().remove(dataItem);
        }

        this._setScopeBtnLabel(this.get_scopes().length);
    },

    _onChangeScopeButtonCLick: function (ev) {
        var dataItem = $workflow.getDataItem(ev, this.get_dataSource());
        if (dataItem) {
            this.get_dialogManager().openDialog(this.get_scopeDefinitionDialog(), [this._getScopeDialogObj(dataItem.toJSON(), "edit")], dataItem.uid);
        }
    },

    _onPageSelectorBtnClick: function (ev) {
        var targetRow = jQuery(ev.target).closest("tr");
        var dataItemUid = targetRow.data("client-id");

        var workflowDataItem = this.get_dataSource().getByUid(dataItemUid);

        var workflowPageScope = this._getPageNodeItem(workflowDataItem.TypeScopes);

        var site = workflowDataItem.Site || this.get_currentSite();

        var siteId = site.SiteId || site.Id;
        var siteName = site.Name;
        var siteRootNodeId = site.SiteMapRootNodeId;

        var selectedPagesIds = workflowPageScope.ContentFilter.toJSON();
        var applyToChildPages = workflowPageScope.IncludeChildren;

        var siteContext = {
            id: siteId,
            name: siteName,
            rootNodeId: siteRootNodeId
        };

        var language = workflowDataItem.Language[0] || null;
        var culture = language && language.Culture;

        var pageSelectorDialog = this.get_pageSelectorDialog();
        var pageSelectorArgs = [selectedPagesIds, applyToChildPages, siteContext, culture];

        this.get_dialogManager().openDialog(pageSelectorDialog, pageSelectorArgs, dataItemUid);
    },

    /* **************** dialog handlers **************** */

    _onScopeDefinitionDialogClose: function (sender, args, dataItemUid) {
        if (!args) return;

        var currentScope = Telerik.Sitefinity.cloneObject(args.scope);
        var dataSource = this.get_dataSource();

        if (args.isNew) {
            dataSource.add(currentScope);
        } else {
            var hasBeenAdded = $workflow.hasBeenAdded(currentScope, this.get_scopes());
            if (hasBeenAdded) return;

            var existingScope = dataSource.getByUid(dataItemUid);
            dataSource.remove(existingScope);
            dataSource.add(currentScope);
        }

        this._setScopeBtnLabel(this.get_scopes().length);
    },

    _onPageSelectorDialogClose: function (sender, args, dataItemUid) {
        if (!args) return;

        var dataSource = this.get_dataSource();
        var dataItem = dataSource.getByUid(dataItemUid);

        var workflowPagesScope = this._getPageNodeItem(dataItem.TypeScopes);

        workflowPagesScope.set("ContentFilter", args.selectedPagesIds);
        workflowPagesScope.set("IncludeChildren", args.applyToChildPages);
    },

    /* **************** properties **************** */

    get_scopeDefinitionDialog: function () {
        return this.get_dialogManager().getDialog("scopeDefinitionDialog");
    },
   
    get_pageSelectorDialog: function () {
        return this.get_dialogManager().getDialog("scopePageSelectorDialog");
    },

    get_selectors: function () {
        return this._selectors;
    },

    get_kendoGrid: function () {
        return jQuery(this.get_selectors().workflowScopeGrid).data("kendoGrid");
    },

    get_dataSource: function () {
        return this.get_kendoGrid().dataSource;
    },

    get_scopes: function () {
        return this.get_dataSource().data().toJSON();
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },

    get_addScopeBtnLabel: function () {
        return this._addScopeBtnLabel;
    },
    set_addScopeBtnLabel: function (value) {
        this._addScopeBtnLabel = value;
    },

    get_dialogManager: function () {
        return this._dialogManager;
    },
    set_dialogManager: function (value) {
        this._dialogManager = value;
    },

    get_isMultisiteMode: function () {
        return this._isMultisiteMode;
    },
    set_isMultisiteMode: function (value) {
        this._isMultisiteMode = value;
    },

    get_isMultilingual: function () {
        return this._isMultilingual;
    },
    set_isMultilingual: function (value) {
        this._isMultilingual = value;
    },

    get_currentSite: function () {
        return this._currentSite;
    },
    set_currentSite: function (value) {
        this._currentSite = value;
    },

    get_scopesErrorMessage: function () {
        return this._scopesErrorMessage;
    },
    set_scopesErrorMessage: function (value) {
        this._scopesErrorMessage = value;
    }
};

Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid.registerClass("Telerik.Sitefinity.Workflow.UI.WorkflowScopesGrid", Telerik.Sitefinity.Web.UI.Fields.FieldControl);