﻿var workflowFormDialog = null;

window.createDialog = function (commandName, dataItem, self, dialog, params, key) {
    if (workflowFormDialog) {
        workflowFormDialog.createDialog(commandName, dataItem, self, dialog, params, key);
    }
};

Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowForm = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowForm.initializeBase(this, [element]);

    this._selectors = {
        body: "body",
        notesWrapper: "#notesWrapper",
        skipWrapper: "#skipWrapper"
    };

    this._bodyCssClass = null;
    this._backToWorkflowLink = null;
    this._workflowTitleField = null;
    this._workflowTypeSelectorPanel = null;
    this._workflowPropertiesPanel = null;
    this._workflowPropertiesCommandBar = null;
    this._enableWorkflowField = null;
    this._allowNotesField = null;
    this._allowPublishersToSkipWorkflowField = null;
    this._allowAdministratorsToSkipWorkflowField = null;
    this._webServiceUrl = null;
    this._workflowDefinitionId = null;
    this._workflowScopesGrid = null;
    this._workflowApprovalGrid = null;
    this._isNew = false;
    this._adminRoleId = null;
    this._adminRoleName = null;
    this._workflowPropertiesDialogNewTitle = null;
    this._workflowPropertiesDialogEditTitle = null;

    this._workflowPropertiesCommandDelegate = null;
    this._backToWorkflowLinkDelegate = null;
}
Telerik.Sitefinity.Workflow.UI.WorkflowForm.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowForm.callBaseMethod(this, "initialize");
        workflowFormDialog = this;

        $(this._selectors.body).addClass(this._bodyCssClass);

        this._backToWorkflowLinkDelegate = this._addHandler(this._backToWorkflowLink, this.backToWorkflowLinkHandler, "click");

        if (this.get_workflowPropertiesCommandBar()) {
            this._workflowPropertiesCommandDelegate = this._addCommandBarHandler(
                this.get_workflowPropertiesCommandBar(),
                this.workflowPropertiesCommandHandler);
        }

        this._showPropertiesDialog();
    },

    dispose: function () {
        if (this._workflowPropertiesCommandDelegate) {
            if (this.get_workflowPropertiesCommandBar() != null) {
                this.get_workflowPropertiesCommandBar().remove_command(this._workflowPropertiesCommandDelegate);
            }
            delete this._workflowPropertiesCommandDelegate;
        }

        $clearHandlers(this._backToWorkflowLink);

        Telerik.Sitefinity.Workflow.UI.WorkflowForm.callBaseMethod(this, "dispose");
    },

    /* **************** public methods **************** */

    createDialog: function (commandName, dataItem, self, dialog, params, key) {
        this.get_workflowScopesGrid().create(dataItem);

        var selectors = [];
        selectors.push(this._selectors.notesWrapper);
        selectors.push(this._selectors.skipWrapper);
        this.get_workflowApprovalGrid().create(dataItem, selectors);

        if (dataItem) {
            this._initializeForm(dataItem);
            $(this.get_workflowPropertiesDialogNewTitle()).hide();
            $(this.get_workflowPropertiesDialogEditTitle()).show();
        }
        else {
            this._isNew = true;
            $(this.get_workflowPropertiesDialogNewTitle()).show();
            $(this.get_workflowPropertiesDialogEditTitle()).hide();
        }

        //do not add history point for this dialog, state is not cleared every time.
        window.top.GetDialogManager().blacklistWindow(dialog);
    },

    /* **************** private methods **************** */

    _addHandler: function (element, handler, event) {
        var delegate = Function.createDelegate(this, handler);
        $addHandler(element, event, delegate);

        return delegate;
    },

    _addCommandBarHandler: function (commandBar, handler) {
        var delegate = Function.createDelegate(this, handler);
        commandBar.add_command(delegate);

        return delegate;
    },

    _initializeForm: function (dataItem) {
        this._setValue(this.get_workflowTitleField(), dataItem);

        this._setValue(this.get_allowAdministratorsToSkipWorkflowField(), dataItem);

        this._initializeWhoCanSkipWorkflow(this.get_allowPublishersToSkipWorkflowField(), this.get_allowAdministratorsToSkipWorkflowField(), dataItem);

        this._setValue(this.get_allowNotesField(), dataItem);

        this._setValue(this.get_enableWorkflowField(), dataItem);

        this._workflowDefinitionId = dataItem.Id;
    },


    _setValue: function (field, dataItem) {
        var value = dataItem[field.get_dataFieldName()];
        field.set_value(value);
    },

    _initializeWhoCanSkipWorkflow: function (field, fieldParrent, dataItem) {
        if (dataItem) {
            this._setValue(field, dataItem);
            if (field._choiceElement.checked) {
                fieldParrent.set_value(true);
                $(fieldParrent._choiceElement).attr("disabled", "disabled");
            }
            else if (!field._choiceElement.checked) {
                $(fieldParrent._choiceElement).removeAttr("disabled");
            }
        }

        $(field._choiceElement).change(function () {
            if (!field._choiceElement.checked) {
                $(fieldParrent._choiceElement).removeAttr("disabled");
                fieldParrent.set_value(false);
            }
            else if (field._choiceElement.checked) {
                fieldParrent.set_value(true);
                $(fieldParrent._choiceElement).attr("disabled", "disabled");
            }
        });
    },

    _showPropertiesDialog: function () {
        $(this.get_workflowTypeSelectorPanel()).show();
        $(this.get_workflowPropertiesPanel()).show();
    },

    _getWorkflowDefinition: function () {
        var workflowDefinition = {
            "Id": this.get_workflowDefinitionId(),
            "Title": this.get_workflowTitleField().get_value(),
            "IsActive": this.get_enableWorkflowField().get_value(),
            "AllowNotes": this.get_allowNotesField().get_value(),
            "WorkflowType": this.get_workflowApprovalGrid().get_workflowType(),
            "Scopes": this.get_workflowScopesGrid().get_scopes(),
            "Levels": this.get_workflowApprovalGrid().get_levels(),
            "AllowAdministratorsToSkipWorkflow": this.get_allowAdministratorsToSkipWorkflowField().get_value(),
            "AllowPublishersToSkipWorkflow": this.get_allowPublishersToSkipWorkflowField().get_value()
        };

        return workflowDefinition;
    },

    _saveWorkflowDefinition: function () {
        if (!this.isFormValid()) return;

        var workflowDefinition = this._getWorkflowDefinition();

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();

        var provider = "";
        var urlParams = {
            provider: provider
        };

        var keys = [this.get_workflowDefinitionId()];

        var that = this;
        function onSuccessCallback(result, data) {
            if (that.isNew)
                dialogBase.closeCreated(data);
            else
                dialogBase.closeUpdated(data);
        }

        function onFailureCallback(sender, args) {
            alert(sender.Detail);
        }

        clientManager.InvokePut(this.get_webServiceUrl(), urlParams, keys, workflowDefinition, onSuccessCallback, onFailureCallback);
    },

    isFormValid: function () {
        var isValid = false;
        if (this.get_workflowTitleField().validate())
            isValid = true;

        isValid = isValid && this.get_workflowScopesGrid().showScopesErrorMsg();
        isValid = isValid && this.get_workflowApprovalGrid().showApproversErrorMsg();

        return isValid;
    },

    get_workflowDefinitionId: function () {
        var id = this._workflowDefinitionId;
        if (!id) return Telerik.Sitefinity.getEmptyGuid();

        return id;
    },

    /* **************** event handlers **************** */

    backToWorkflowLinkHandler: function () {
        dialogBase.close();
    },

    workflowPropertiesCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        switch (commandName) {
            case "save":
                this._saveWorkflowDefinition();
                break;
            default:
                dialogBase.close();
                break;
        }
    },

    /* **************** properties **************** */

    get_bodyCssClass: function () {
        return this._bodyCssClass;
    },
    set_bodyCssClass: function (value) {
        this._bodyCssClass = value;
    },

    get_backToWorkflowLink: function () {
        return this._backToWorkflowLink;
    },
    set_backToWorkflowLink: function (value) {
        this._backToWorkflowLink = value;
    },

    get_workflowTitleField: function () {
        return this._workflowTitleField;
    },
    set_workflowTitleField: function (value) {
        this._workflowTitleField = value;
    },

    get_allowPublishersToSkipWorkflowField: function () {
        return this._allowPublishersToSkipWorkflowField;
    },
    set_allowPublishersToSkipWorkflowField: function (value) {
        this._allowPublishersToSkipWorkflowField = value;
    },

    get_workflowTypeSelectorPanel: function () {
        return this._workflowTypeSelectorPanel;
    },
    set_workflowTypeSelectorPanel: function (value) {
        this._workflowTypeSelectorPanel = value;
    },

    get_workflowPropertiesPanel: function () {
        return this._workflowPropertiesPanel;
    },
    set_workflowPropertiesPanel: function (value) {
        this._workflowPropertiesPanel = value;
    },

    get_workflowPropertiesCommandBar: function () {
        return this._workflowPropertiesCommandBar;
    },
    set_workflowPropertiesCommandBar: function (value) {
        this._workflowPropertiesCommandBar = value;
    },

    get_enableWorkflowField: function () {
        return this._enableWorkflowField;
    },
    set_enableWorkflowField: function (value) {
        this._enableWorkflowField = value;
    },

    get_allowNotesField: function () {
        return this._allowNotesField;
    },
    set_allowNotesField: function (value) {
        this._allowNotesField = value;
    },

    get_allowPublishersToSkipWorkflowField: function () {
        return this._allowPublishersToSkipWorkflowField;
    },
    set_allowPublishersToSkipWorkflowField: function (value) {
        this._allowPublishersToSkipWorkflowField = value;
    },

    get_allowAdministratorsToSkipWorkflowField: function () {
        return this._allowAdministratorsToSkipWorkflowField;
    },
    set_allowAdministratorsToSkipWorkflowField: function (value) {
        this._allowAdministratorsToSkipWorkflowField = value;
    },

    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },

    get_workflowScopesGrid: function () {
        return this._workflowScopesGrid;
    },
    set_workflowScopesGrid: function (value) {
        this._workflowScopesGrid = value;
    },

    get_workflowApprovalGrid: function () {
        return this._workflowApprovalGrid;
    },
    set_workflowApprovalGrid: function (value) {
        this._workflowApprovalGrid = value;
    },

    get_adminRoleId: function () {
        return this._adminRoleId;
    },
    set_adminRoleId: function (value) {
        this._adminRoleId = value;
    },

    get_adminRoleName: function () {
        return this._adminRoleName;
    },
    set_adminRoleName: function (value) {
        this._adminRoleName = value;
    },

    get_workflowPropertiesDialogNewTitle: function () {
        return this._workflowPropertiesDialogNewTitle;
    },
    set_workflowPropertiesDialogNewTitle: function (value) {
        this._workflowPropertiesDialogNewTitle = value;
    },

    get_workflowPropertiesDialogEditTitle: function () {
        return this._workflowPropertiesDialogEditTitle;
    },
    set_workflowPropertiesDialogEditTitle: function (value) {
        this._workflowPropertiesDialogEditTitle = value;
    }
};

Telerik.Sitefinity.Workflow.UI.WorkflowForm.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowForm', Telerik.Sitefinity.Web.UI.AjaxDialogBase);