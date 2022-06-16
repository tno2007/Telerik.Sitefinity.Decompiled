Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog.initializeBase(this, [element]);

    this._approvalNoteField = null;
    this._sendForApprovalButton = null;

    this._commandName = null;
    this._operationName = null;

    //delegates
    this._approveClientSelectionDelegate = null;
    this._loadDelegate = null;

}

Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog.callBaseMethod(this, "initialize");

        if (this._loadDelegate == null) {
            this._loadDelegate = Function.createDelegate(this, this.loadDialogHandler);
        }
        Sys.Application.add_load(this._loadDelegate);

        if (this._sendForApprovalButton) {
            if (this._approveClientSelectionDelegate == null) {
                this._approveClientSelectionDelegate = Function.createDelegate(this, this._handleApproveNoteClientSelection);
            }
            $addHandler(this._sendForApprovalButton, 'click', this._approveClientSelectionDelegate);
        }
    },

    // tear down
    dispose: function () {

        if (this._loadDelegate != null) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        if (this._sendForApprovalButton) {
            $removeHandler(this._sendForApprovalButton, 'click', this._approveClientSelectionDelegate);
        }

        if (this._approveClientSelectionDelegate != null) {
            delete this._approveClientSelectionDelegate;
        }

        Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog.callBaseMethod(this, "dispose");
    },

    /* -------------------- public methods ------------ */

    /* -------------------- event handlers ------------ */

    loadDialogHandler: function () {
        jQuery("body").addClass("sfSelectorDialog");

        this._operationName = dialogBase.getQueryValue('operationName', true);
        this._commandName = dialogBase.getQueryValue('commandName', true);
        

        if (dialogBase._dialog._sfProperties && dialogBase._dialog._sfProperties.length) {
            var action = this._getDialogPropertyValue('DetailedTitle');
            if (action) {
                jQuery("#actionTitle").html(action);
                jQuery(".sfButtonArea .sfLinkBtn .sfLinkBtnIn").html(action)
            }
        }

        if (dialogBase._dialog._sfNoteValue) {
            this.get_approvalNoteField().set_value(dialogBase._dialog._sfNoteValue);
        }

        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }

        dialogBase.resizeToContent();
    },

    _handleApproveNoteClientSelection: function () {
        var data = this._approvalNoteField.get_value();
        // we need to encode uri, because data is not send correctly in IE
        if ($telerik.isIE && dialogBase._dialog._sfValidationEnabled) data = encodeURIComponent(data);

        var contextBag = new Array({ key: "Note", value: data });
        var eventArgs = new Telerik.Sitefinity.WorkflowDialogClosedEventArgs(this._commandName, this._operationName, contextBag, true);
        eventArgs._noteValue = data;

        this.close(eventArgs);

    },

    _getDialogPropertyValue: function (name) {
        var value = "";
        var elements = jQuery.grep(dialogBase._dialog._sfProperties, function (e) { return e.Key == name; });
        if (elements && elements.length > 0) {
            value = elements[0].Value;
        }

        return value;
    },

    /* -------------------- private methods ------------ */

    /* -------------------- properties ------------ */
    get_sendForApprovalButton: function () {
        return this._sendForApprovalButton;
    },
    set_sendForApprovalButton: function (value) {
        this._sendForApprovalButton = value;
    },

    get_approvalNoteField: function () {
        return this._approvalNoteField;
    },
    set_approvalNoteField: function (value) {
        this._approvalNoteField = value;
    },

    get_commandName: function () {
        return this._commandName;
    },
    set_commandName: function (value) {
        this._commandName = value;
    },

    get_operationName: function () {
        return this._operationName;
    },
    set_operationName: function (value) {
        this._operationName = value;
    }
};

Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowSendForApprovalDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);