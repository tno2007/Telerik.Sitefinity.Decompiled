﻿Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog.initializeBase(this, [element]);

    this._reasonToRejectTextField = null;
    this._rejectButton = null;

    this._commandName = null;
    this._operationName = null;

    //delegates
    this._rejectClientSelectionDelegate = null;
    this._loadDelegate = null;
}

Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog.callBaseMethod(this, "initialize");

        if (this._loadDelegate == null) {
            this._loadDelegate = Function.createDelegate(this, this.loadDialogHandler);
        }
        Sys.Application.add_load(this._loadDelegate);

        if (this._rejectButton) {
            if (this._rejectClientSelectionDelegate == null) {
                this._rejectClientSelectionDelegate = Function.createDelegate(this, this._handleRejectClientSelection);
            }
            $addHandler(this._rejectButton, 'click', this._rejectClientSelectionDelegate);
        }

    },

    // tear down
    dispose: function () {

        if (this._loadDelegate != null) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        if (this._rejectButton) {
            $removeHandler(this._rejectButton, 'click', this._rejectClientSelectionDelegate);
        }

        if (this._rejectClientSelectionDelegate != null) {
            delete this._rejectClientSelectionDelegate;
        }

        Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog.callBaseMethod(this, "dispose");
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
            }

            var hint = this._getDialogPropertyValue('Hint');
            if (hint) {
                jQuery(".sfBasicDim .sfFormCtrl p").html(hint);
            }
        }

        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }

        dialogBase.resizeToContent();
    },

    _handleRejectClientSelection: function () {
        var data = this._reasonToRejectTextField.get_value();

        var contextBag = new Array({ key: "Note", value: data });
        var eventArgs = new Telerik.Sitefinity.WorkflowDialogClosedEventArgs(this._commandName, this._operationName, contextBag);
        this.close(eventArgs);

    },

    _getDialogPropertyValue: function (name) {
        var value = "";
        var elements = jQuery.grep(dialogBase._dialog._sfProperties, function(e){ return e.Key == name; });
        if (elements && elements.length > 0) {
            value = elements[0].Value;
        }

        return value;
    },

    /* -------------------- private methods ------------ */

    /* -------------------- properties ------------ */
    get_rejectButton: function () {
        return this._rejectButton;
    },
    set_rejectButton: function (value) {
        this._rejectButton = value;
    },

    get_reasonToRejectTextField: function () {
        return this._reasonToRejectTextField;
    },
    set_reasonToRejectTextField: function (value) {
        this._reasonToRejectTextField = value;
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

Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowRejectDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
