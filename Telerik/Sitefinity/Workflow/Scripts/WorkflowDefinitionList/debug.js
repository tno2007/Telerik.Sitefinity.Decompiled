Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList.initializeBase(this, [element]);

    this._workflowDefinitionsGrid = null;
    this._decisionScreenEmpty = null;
    this._binder = null;
    this._isDBPMode = null;

    this._onLoadDelegate = null;
    this._dataBoundDelegate = null;
    this._itemSelectedDelegate = null;
    this._decisionScreenEmptyDelegate = null;
}

Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList.callBaseMethod(this, 'initialize');

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        if (this._decisionScreenEmpty == null)
            this._decisionScreenEmpty = $find(this._decisionScreenEmpty);

        if (this._workflowDefinitionsGrid == null)
            this._workflowDefinitionsGrid = $find(this._workflowDefinitionsGrid);

        if (this._binder == null)
            this._binder = $find(this._workflowDefinitionsGrid.get_binderId());

        if (this.get_isDBPMode() != null) {
            if (this.get_isDBPMode())
                jQuery("#permissionsAction").hide();
        }
        this._binder.set_unescapeHtml(true);
        this._dataBoundDelegate = Function.createDelegate(this, this.workflowDefinitionsGridBinder_DataBound);
        this._binder.add_onDataBound(this._dataBoundDelegate);

        this._itemSelectedDelegate = Function.createDelegate(this, this.workflowDefinitionsGridBinder_ItemSelectCommand);
        this._binder.add_onItemSelectCommand(this._itemSelectedDelegate);

        this._decisionScreenEmptyDelegate = Function.createDelegate(this, this.decisionScreenEmpty_ActionCommand);

        this._onCommandHandlerDelegate = Function.createDelegate(this, this._onCommandHandler);

    },
    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList.callBaseMethod(this, 'dispose');


        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        if (this._binder) {
            this._binder.remove_onDataBound(this, this._dataBoundDelegate);
            this._binder.remove_onItemSelectCommand(this, this._itemSelectedDelegate);
        }

        if (this._dataBoundDelegate)
            delete this._dataBoundDelegate;

        if (this._itemSelectedDelegate)
            delete this._itemSelectedDelegate;

        if (this._decisionScreenEmptyDelegate)
            delete this._decisionScreenEmptyDelegate;
    },

    /* ------------------ Events --------------*/
    _onLoad: function () {
        jQuery("body").addClass("sfHasSidebar");
        this.get_workflowDefinitionsGrid().add_command(this._onCommandHandlerDelegate);
    },


    decisionScreenEmpty_ActionCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case "defineWorkflow":
                args.set_cancel(true);
                this._workflowDefinitionsGrid.openDialog("create", null, []);
                break;
            default:
                break;
        }
    },

    workflowDefinitionsGridBinder_DataBound: function (sender, args) {
        if (sender.get_hasNoData() && !sender.get_isFiltering()) {
            this._decisionScreenEmpty.show();
            this._decisionScreenEmpty.add_actionCommand(this._decisionScreenEmptyDelegate);

            jQuery("body").addClass("sfEmpty");
            this._workflowDefinitionsGrid.hide();
        }
        else {
            this._decisionScreenEmpty.hide();
            this._decisionScreenEmpty.remove_actionCommand(this._decisionScreenEmptyDelegate);

            jQuery("body").removeClass("sfEmpty");
            this._workflowDefinitionsGrid.show();

        }
        this._setToolbarButtonsEnabledState();
    },

    _onCommandHandler: function (sender, args) {
        var comandName = args.get_commandName();
        if (comandName == "sort") {
            var argument = args.get_commandArgument();
            this.get_workflowDefinitionsGrid().applySorting(argument);
        }
    },

    workflowDefinitionsGridBinder_ItemSelectCommand: function (sender, args) {
        this._setToolbarButtonsEnabledState();
    },

    /* --------------------  public methods ----------- */

    newWorkflow: function () {
        this._workflowForm.show();
        this._workflowForm.center();
        this._workflowForm.maximize();
    },

    openWorkflow: function (sender, args) {
        this._workflowForm.add_pageLoad(this.onWorkflowFormLoaded);
        this._workflowForm.show();
        this._workflowForm.center();
        this._workflowForm.maximize();
    },

    onWorkflowFormLoaded: function () {
        this._workflowForm.remove_pageLoad(this.onWorkflowFormLoaded);
        this._workflowForm.add_show(this._onWorkflowFormShow);
        this._onWorkflowFormShow();
    },

    onWorkflowFormShow: function () {
        this._workflowForm.get_contentFrame().contentWindow.loadWorkflow();
    },

    /* ------------------ Private methods --------------*/

    _setToolbarButtonsEnabledState: function () {
        var toEnable = this._binder.get_selectedItemsCount && (this._binder.get_selectedItemsCount() > 0);

        jQuery(".sfGroupBtn a").each(function () {
            if (toEnable) {
                jQuery(this).removeClass("sfDisabledLinkBtn");
                //restore the removed postback when link disabled
                jQuery(this).attr("href", "javascript: __doPostBack('" + this.id + "', '')");
            }
            else {
                jQuery(this).addClass("sfDisabledLinkBtn");
                //TODO: this fix removes the link with postback error at FF, but still a  new window is opened when "Middle" mouse button is pressed.
                //Better aproach would be link to be replaced with span with appropriate styles.
                jQuery(this).attr("href", "");
            }
        });
    },

    /* ------------------ Properies --------------*/
    get_workflowDefinitionsGrid: function () {
        return this._workflowDefinitionsGrid;
    },

    set_workflowDefinitionsGrid: function (value) {
        this._workflowDefinitionsGrid = value;
    }
    ,
    get_decisionScreenEmpty: function () {
        return this._decisionScreenEmpty;
    },

    set_decisionScreenEmpty: function (value) {
        this._decisionScreenEmpty = value;
    },

    get_isDBPMode: function () {
        return this._isDBPMode;
    },

    set_isDBPMode: function (value) {
        this._isDBPMode = value;
    }
}

Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowDefinitionList', Sys.UI.Control);
