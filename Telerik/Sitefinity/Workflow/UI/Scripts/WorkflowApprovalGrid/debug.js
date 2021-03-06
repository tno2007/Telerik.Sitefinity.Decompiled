Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid.initializeBase(this, [element]);

    this._dialogManager = null;
    this._labelManager = null;
    this._changeLevelsBtn = null;
    this._approvalErrorMessage = null;
    this.selectors = null;

    this._workflowType = null;
    this._levelsOfApproval = [];

    this._adminRoleId = null;

    this._eventNamespace = ".workflowApprovers";

    this._selectors = {
        workflowApprovalWrapper: "#workflowApprovalWrapper",
        workflowApprovalGrid: "#workflowApprovalsGrid",
        gridRowTemplate: "#wfApprovalGridRowTemplate",
        noApprovalWorkflowPanel: "#wfNoApprovalWorkflowPanel",
        changeLevelsBtn: ".wfChangeLevelsBtn",
        changeApproversBtn: ".wfChangeApproversBtn",
        setNotificationsBtn: ".wfSetNotificationsBtn"
    };
}

Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid.callBaseMethod(this, "initialize");

        this._initializeEventHandlers();
        this._initializeDialogHandlers();

        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);
        this._dataSourceChangeDelegate = Function.createDelegate(this, this._dataSourceChangeHandler);
    },

    dispose: function () {
        this._disposeEventHandlers();

        Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid.callBaseMethod(this, "dispose");
    },

    /* **************** public methods **************** */

    create: function (dataItem, selectors) {
        this.selectors = selectors;

        if (dataItem) {
            this.set_workflowType(dataItem.WorkflowType);
        } 

        this.showHideSelectors();

        this._initializeDataSource(dataItem);
        this._initializeGrid();
    },

    fetchDataSource: function () {
        this.get_dataSource().fetch();
    },

    /* **************** private methods **************** */

    _initializeDataSource: function (dataItem) {
        var that = this;

        if (!dataItem) return;

        this._dataSource = new kendo.data.DataSource({
            data: dataItem.Levels
        });

        this._dataSource.sort({
            field: "Ordinal",
            dir: "asc"
        });
    },

    _initializeGrid: function () {
        var gridRowTemplate = jQuery(this.get_selectors().gridRowTemplate).html();
        var compileGridRowTemplate = kendo.template(gridRowTemplate);

        jQuery(this.get_selectors().workflowApprovalGrid).kendoGrid({
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

        this._addHandler("click", selectors.changeLevelsBtn, this._onChangeLevelButtonClick);
        this._addHandler("click", selectors.changeApproversBtn, this._onChangeApproverButtonClick, true);
        this._addHandler("click", selectors.setNotificationsBtn, this._onSetNotificationButtonClick, true);
    },

    _initializeDialogHandlers: function () {
        var dialogManager = this.get_dialogManager();

        dialogManager.addHandler(this.get_selectUsersAndRolesDialog(), "close", this._onChangeApproverDialogClose.bind(this));
        dialogManager.addHandler(this.get_setNotificationDialog(), "close", this._onSetNotificationDialogClose.bind(this));
        dialogManager.addHandler(this.get_typeSelectorDialog(), "close", this._onTypeSelectorDialogClose.bind(this));
    },

    _disposeEventHandlers: function () {
        var selectors = this.get_selectors();

        this._removeHandlers(selectors.changeLevelsBtn);
        this._removeHandlers(selectors.changeApproversBtn);
        this._removeHandlers(selectors.setNotificationsBtn);
    },

    _addHandler: function (eventName, selector, handler, shouldBindDynamically) {
        var boundHandler = handler.bind(this);
        var eventFullName = eventName + this._eventNamespace;

        if (shouldBindDynamically) {
            jQuery(this.get_selectors().workflowApprovalWrapper).on(eventFullName, selector, boundHandler);
        } else {
            jQuery(selector).on(eventFullName, boundHandler);
        }
    },

    _removeHandlers: function (selector) {
        jQuery(selector).off(this._eventNamespace);
    },

    _selectApprovers: function (actionsNames, successCb, cancelCb, index) {
        var self = this;

        var dialogManager = this.get_dialogManager();

        if (index < 0) {
            this._levelsOfApproval = [];
            dialogManager.openDialog(this.get_typeSelectorDialog());
            return;
        }

        if (!actionsNames || index >= actionsNames.length) {
            var levelsOfApprovalCopy = this._levelsOfApproval.slice();
            this._levelsOfApproval = [];

            if (successCb) successCb(levelsOfApprovalCopy);
            return;
        }

        var index = index == undefined ? 0 : index;
        var ordinal = index + 1;

        var currentActionName = actionsNames[index];

        var dialogHandler = function (_, args) {
            dialogManager.removeHandler(self.get_selectUsersAndRolesDialog(), "close", dialogHandler);

            if (!args) {
                self._levelsOfApproval = [];

                if (cancelCb) cancelCb();
                return;
            }

            if (args.command === "back") {
                self._selectApprovers(actionsNames, successCb, cancelCb, --index);
            } else {
                var currentAction = {
                    Ordinal: ordinal,
                    ActionName: currentActionName,
                    NotifyApprovers: true,
                    NotifyAdministrators: true,
                    CustomEmailRecipients: [],
                    Permissions: self._mapToLevelPermissions(args.SelectedPrincipals, currentActionName)
                };
    
                self._levelsOfApproval[index] = currentAction;
                self._selectApprovers(actionsNames, successCb, cancelCb, ++index);
            }
        };

        dialogManager.addHandler(this.get_selectUsersAndRolesDialog(), "close", dialogHandler);

        var approversLabel = '';
        var dialogHeaderIcon = '';

        if (actionsNames.length > 1) {
            var approversForLevelLabel = this.get_labelManager().getLabel("WorkflowResources", "SetApproversForLevel");
            approversLabel = String.format(approversForLevelLabel, ordinal);
            dialogHeaderIcon = jQuery("<span/>").text(ordinal);
            dialogHeaderIcon.addClass("sfBadge sfBadgeLarge sfBadgeDarkGrey sfBaseTxt sfMRight5")
        } else {
            approversLabel = this.get_labelManager().getLabel("WorkflowResources", "ApproversLabel");
        }
        
        var dialogHeaderText = jQuery("<span/>").text(approversLabel);
        dialogHeaderText.addClass("sfAlignMiddle");
        var dialogHeader = jQuery("<span/>").append(dialogHeaderIcon).append(dialogHeaderText);

        var dialogDoneBtnLabel = null;

        if (index < actionsNames.length - 1) {
            var continueLabel = this.get_labelManager().getLabel("Labels", "Next");
            dialogDoneBtnLabel = continueLabel + ": " + String.format(approversForLevelLabel, ordinal + 1);
        } else {
            dialogDoneBtnLabel = this.get_labelManager().getLabel("Labels", "Done");
        }

        var previousLevel = this._levelsOfApproval[index];
        var previousLevelPrincipalIds = previousLevel && previousLevel.Permissions.map(function (level) {
            return level.PrincipalId;
        });

        var dialogArgs = [previousLevelPrincipalIds || [], dialogHeader, dialogDoneBtnLabel, true];
        dialogManager.openDialog(this.get_selectUsersAndRolesDialog(), dialogArgs);
    },

    _mapToLevelPermissions: function (principals, actionName) {
        return principals.map(function (principal) {
            return {
                ActionName: actionName,
                PrincipalId: principal.Id,
                PrincipalName: principal.Name,
                PrincipalType: principal.Type === "User" ? 0 : 1,
            };
        });
    },

    _getActionMsg: function (action) {
        return this._getWorkflowRes("Action") + ": " + this._getWorkflowRes("SendFor") + " " + this._getActionName(action).toLocaleLowerCase();
    },

    _getStatusMsg: function (action) {
        return this._getWorkflowRes("Status") + ": " + this._getWorkflowRes("WaitingFor") + " " + this._getActionName(action).toLocaleLowerCase();
    },

    _getActionName: function (action) {
        var actionName = $workflow.getActionUIName(action, this.get_labelManager());
        return actionName;
    },

    _getWorkflowRes: function (key) {
        return this.get_labelManager().getLabel("WorkflowResources", key)
    },

    showApproversErrorMsg: function () {
        var isValid = true;

        if (!this._workflowType) {
            isValid = false;
            jQuery(this.get_approvalErrorMessage()).show();
        } else {
            jQuery(this.get_approvalErrorMessage()).hide();
        }

        return isValid;
    },

    showHideSelectors: function () {
        if (!this.selectors) return;

        for (var s in this.selectors) {
            if (!this._workflowType || this._workflowType === "0")
                $(this.selectors[s]).hide();
            else
                $(this.selectors[s]).show();
        }
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
        } else {
            jQuery(this.get_kendoGrid().element).hide();
        }

        this.get_dataSource().bind("change", this._dataSourceChangeDelegate);
    },

    _onChangeLevelButtonClick: function () {
        this.get_dialogManager().openDialog(this.get_typeSelectorDialog());
    },

    _onChangeApproverButtonClick: function (ev) {
        var dataItem = $workflow.getDataItem(ev, this.get_dataSource());
        if (!dataItem) return;

        var principalsIds = dataItem.Permissions.map(function (level) {
            return level.PrincipalId;
        });

        var dialogHeader = null;

        if (this.get_levels().length > 1) {
            var label = this.get_labelManager().getLabel("WorkflowResources", "SetApproversForLevel");
            dialogHeader = String.format(label, dataItem.Ordinal);
        } else {
            dialogHeader = this.get_labelManager().getLabel("WorkflowResources", "ApproversLabel");
        }

        var dialogDoneBtnLabel = this.get_labelManager().getLabel("Labels", "Done");

        this.get_dialogManager().openDialog(this.get_selectUsersAndRolesDialog(), [principalsIds, dialogHeader, dialogDoneBtnLabel], dataItem);
    },

    _onSetNotificationButtonClick: function (ev) {
        var dataItem = $workflow.getDataItem(ev, this.get_dataSource());
        if (dataItem) {
            this.get_dialogManager().openDialog(this.get_setNotificationDialog(), [dataItem.toJSON()], dataItem.uid);
        }
    },

    _dataSourceChangeHandler: function (e) {
        this.showApproversErrorMsg();
        this.showHideSelectors();
    },

    /* **************** dialog handlers **************** */

    _onChangeApproverDialogClose: function (sender, args, dataItem) {
        if (!args || !dataItem) return;

        var levelPermissions = this._mapToLevelPermissions(args.SelectedPrincipals, dataItem.ActionName);
        dataItem.set("Permissions", levelPermissions);
    },

    _onTypeSelectorDialogClose: function (sender, args) {
        var self = this;

        if (!args) return;

        var successCb = function (levelsOfApproval) {
            self.set_workflowType(args);
            self.get_dataSource().data(levelsOfApproval);
        };

        switch (args) {
            case "0":
                successCb([]);
                break;
            case "1":
                var actionsNames = [$workflow.actionNames.approve];
                this._selectApprovers([$workflow.actionNames.approve], successCb);
                break;
            case "2":
                var actionsNames = [$workflow.actionNames.approve, $workflow.actionNames.publish];
                this._selectApprovers(actionsNames, successCb);
                break;
            case "4":
                var actionsNames = [$workflow.actionNames.review, $workflow.actionNames.approve, $workflow.actionNames.publish];
                this._selectApprovers(actionsNames, successCb);
                break;
        }
    },

    _onSetNotificationDialogClose: function (sender, dataItem, uid) {
        if (!dataItem) return;

        var dataSource = this.get_dataSource();
        var item = dataSource.getByUid(uid);

        // TODO: Remove fetchDataSource
        item.NotifyApprovers = dataItem.NotifyApprovers;
        item.NotifyAdministrators = dataItem.NotifyAdministrators;
        item.CustomEmailRecipients = [];
        jQuery.each(dataItem.CustomEmailRecipients, function (key, val) {
            item.CustomEmailRecipients.push(val);
        });

        this.fetchDataSource();
    },

    /* **************** properties **************** */

    get_levels: function () {
        return this.get_dataSource().data().toJSON();
    },

    get_selectors: function () {
        return this._selectors;
    },

    get_selectUsersAndRolesDialog: function () {
        return this.get_dialogManager().getDialog("selectUsersAndRolesDialog");
    },

    get_setNotificationDialog: function () {
        return this.get_dialogManager().getDialog("setNotificationDialog");
    },

    get_typeSelectorDialog: function () {
        return this.get_dialogManager().getDialog("typeSelectorDialog");
    },

    get_kendoGrid: function () {
        return jQuery(this.get_selectors().workflowApprovalGrid).data("kendoGrid");
    },

    get_dataSource: function () {
        return this.get_kendoGrid().dataSource;
    },

    get_workflowType: function () {
        return this._workflowType;
    },
    set_workflowType: function (value) {
        this._workflowType = value.toString();

        var changeLevelsBtnLabel = null;

        if (this._workflowType === "0") {
            $(this._selectors.noApprovalWorkflowPanel).show();
            changeLevelsBtnLabel = this.get_labelManager().getLabel("Labels", "Change");
        } else {
            $(this._selectors.noApprovalWorkflowPanel).hide();
            changeLevelsBtnLabel = this.get_labelManager().getLabel("WorkflowResources", "ChangeLevelButtonLabel");
        }

        $(this.get_changeLevelsBtn()).text(changeLevelsBtnLabel);
    },

    get_dialogManager: function () {
        return this._dialogManager;
    },
    set_dialogManager: function (value) {
        this._dialogManager = value;
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },

    get_changeLevelsBtn: function () {
        return this._changeLevelsBtn;
    },
    set_changeLevelsBtn: function (value) {
        this._changeLevelsBtn = value;
    },

    get_approvalErrorMessage: function () {
        return this._approvalErrorMessage;
    },

    set_approvalErrorMessage: function (value) {
        this._approvalErrorMessage = value;
    },

    get_adminRoleId: function () {
        return this._adminRoleId;
    },
    set_adminRoleId: function (value) {
        this._adminRoleId = value;
    }
};

Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid.registerClass("Telerik.Sitefinity.Workflow.UI.WorkflowApprovalGrid", Telerik.Sitefinity.Web.UI.Fields.FieldControl);