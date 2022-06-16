Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

var workflowScheduleDialog = null;

Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog.initializeBase(this, [element]);

    workflowScheduleDialog = this;

    this._scheduleButton = null;
    this._publishedOnDateField = null;
    this._unpublishOnDateField = null;
    // this._serviceUrl = null;

    this._unpublishOnSection = null;
    this._unpublishOnExpander = null;
    this._commandName = null;
    this._operationName = null;
    this._provider = null;
    this._dataItem = null;

    //delegates
    this._doneClientSelectionDelegate = null;
    this._loadDelegate = null;
    this._radWindowShowDelegate = null;

    this._publishedOnDateFieldPopupOpeningDelegate = null;
    this._publishedOnDateFieldPopupClosingDelegate = null;
    this._unpublishDateFieldPopupOpeningDelegate = null;
    this._unpublishDateFieldPopupClosingDelegate = null;
}

Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog.callBaseMethod(this, "initialize");

        if (this._radWindowShowDelegate == null) {
            this._radWindowShowDelegate = Function.createDelegate(this, this._radWindowShowHandler);
        }

        this.get_radWindow().add_show(this._radWindowShowDelegate);

        if (this._loadDelegate == null) {
            this._loadDelegate = Function.createDelegate(this, this.loadDialogHandler);
        }
        Sys.Application.add_load(this._loadDelegate);

        if (this._scheduleButton != null) {
            if (this._doneClientSelectionDelegate == null) {
                this._doneClientSelectionDelegate = Function.createDelegate(this, this._saveClientSelectionHandler);
            }
            $addHandler(this._scheduleButton, 'click', this._doneClientSelectionDelegate);
        }

        if (this._unpublishOnExpander != null) {
            if (this._unpublishOnExpanderClickDelegate == null) {
                this._unpublishOnExpanderClickDelegate = Function.createDelegate(this, this._unpublishOnExpanderClickHandler);
            }
            $addHandler(this._unpublishOnExpander, "click", this._unpublishOnExpanderClickDelegate);
        }

        if (this._publishedOnDateField != null) {
            if (this._publishedOnDateFieldPopupOpeningDelegate == null)
                this._publishedOnDateFieldPopupOpeningDelegate = Function.createDelegate(this, this._pulishOnDateFieldPopupOpeningHandler);
            this._publishedOnDateField.add_datePickerOnPopupOpeningHandler(this._pulishOnDateFieldPopupOpeningHandler);

            if (this._publishedOnDateField == null)
                this._publishedOnDateField = Function.createDelegate(this, this._pulishOnDateFieldPopupClosingHandler);

            this._publishedOnDateField.add_datePickerOnPopupClosingHandler(this._pulishOnDateFieldPopupClosingHandler);
        }

        if (this._unpublishOnDateField != null) {
            if (this._unpublishDateFieldPopupOpeningDelegate == null)
                this._unpublishDateFieldPopupOpeningDelegate = Function.createDelegate(this, this._unpulishOnDateFieldPopupOpeningHandler);
            this._unpublishOnDateField.add_datePickerOnPopupOpeningHandler(this._unpublishDateFieldPopupOpeningDelegate);

            if (this._unpublishDateFieldPopupClosingDelegate == null)
                this._unpublishDateFieldPopupClosingDelegate = Function.createDelegate(this, this._unpulishOnDateFieldPopupClosingHandler);

            this._unpublishOnDateField.add_datePickerOnPopupClosingHandler(this._unpublishDateFieldPopupClosingDelegate);
        }
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog.callBaseMethod(this, "dispose");

        if (this._radWindowShowDelegate != null) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_show(this._radWindowShowDelegate);
            }

            delete this._radWindowShowDelegate;
        }

        if (this._loadDelegate != null) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        if (this._doneClientSelectionDelegate != null) {
            if (this._scheduleButton) {
                $removeHandler(this._scheduleButton, 'click', this._doneClientSelectionDelegate);
            }

            delete this._doneClientSelectionDelegate;
        }

        if (this._unpublishOnExpanderClickDelegate) {
            if (this._unpublishOnExpander) {
                $removeHandler(this._unpublishOnExpander, "click", this._unpublishOnExpanderClickDelegate);
            }
            delete this._unpublishOnExpanderClickDelegate;
        }

        if (this._unpublishOnDateField != null) {
            this._unpublishOnDateField.remove_datePickerOnPopupClosingHandler(this._unpublishDateFieldPopupClosingDelegate);
            this._unpublishOnDateField.remove_datePickerOnPopupOpeningHandler(this._unpublishDateFieldPopupOpeningDelegate);

            delete this._unpublishDateFieldPopupOpeningDelegate;
            delete this._unpublishDateFieldPopupClosingDelegate;
        }
    },

    /* -------------------- public methods -------------------- */

    // Returns an object that is collection of property name and property value.
    getContextBag: function (publishedOn, expiresOn) {
        var contextBag = new Array();
        contextBag.push({ key: "PublicationDate", value: publishedOn.toISOString() });
        if (expiresOn != null) {
            contextBag.push({ key: "ExpirationDate", value: expiresOn.toISOString() });
        }
        return contextBag;
    },

    // Expects as parameter an object that contains fields : PulicationDate and ExpirationDate
    setScheduleData: function (value) {
        var defaultDateValue = new Date()
        if (value) {
            var date = value.PublicationDate ? value.PublicationDate : null;
            this._setDateValue(this._publishedOnDateField, date, defaultDateValue);
            date = value.ExpirationDate ? value.ExpirationDate : null;
            this._setDateValue(this._unpublishOnDateField, date, null);
            if (date == null) {
                this._collapseSection();
            }
            else {
                this._expandSection();
            }
        }
        else {
            this._setDateValue(this._publishedOnDateField, null, defaultDateValue);
            this._setDateValue(this._unpublishOnDateField, null, null);
            this._collapseSection();
        }
    },

    validate: function () {
        var validFields = this._publishedOnDateField.validate() && this._unpublishOnDateField.validate();
        var publishDateFieldHasValue = this._publishedOnDateField.get_value();
        return validFields && publishDateFieldHasValue;
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _pulishOnDateFieldPopupOpeningHandler: function () {
        dialogBase.setWndHeight(570);
    },
    _pulishOnDateFieldPopupClosingHandler: function () {
        dialogBase.setWndHeight(330);
    },
    _unpulishOnDateFieldPopupOpeningHandler: function () {
        dialogBase.setWndHeight(650);
    },
    _unpulishOnDateFieldPopupClosingHandler: function () {
        jQuery(this.get_unpublishOnDateField().get_validator().get_violationMessageElement()).hide()
        dialogBase.setWndHeight(330);
    },

    loadDialogHandler: function () {
        jQuery("body").addClass("sfSelectorDialog");
        this._operationName = dialogBase.getQueryValue('operationName', true);
        this._commandName = dialogBase.getQueryValue('commandName', true);
        this._provider = dialogBase.getQueryValue('provider', true);

        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }

    },

    _radWindowShowHandler: function (sender, args) {
        dialogBase.resizeToContent();
    },

    _unpublishOnExpanderClickHandler: function (e) {
        this._toggleSection();
        dialogBase.resizeToContent();
    },

    _saveClientSelectionHandler: function () {
        var publishedOn = this._publishedOnDateField.get_value();
        var expiresOn = this._unpublishOnDateField.get_value();

        this._dataItem.ExpirationDate = expiresOn;

        var contextBag = this.getContextBag(publishedOn, expiresOn);

        if (contextBag) {
            if (this.validate()) {
                var eventArgs = new Telerik.Sitefinity.WorkflowDialogClosedEventArgs(this._commandName, this._operationName, contextBag, true);
                eventArgs.__workflowScheduleDialog = true;
                this.close(eventArgs);
            }
            else {
                dialogBase.resizeToContent();
            }
        }
        return;
    },

    /* -------------------- private methods -------------------- */
    _setDateValue: function (dateField, value, defaultDateValue) {
        if (value)
            dateField.set_value(value);
        else
            dateField.set_value(defaultDateValue);
    },

    _toggleSection: function () {
        jQuery(this._unpublishOnSection).toggleClass("sfExpandedSection");
    },

    _expandSection: function () {
        jQuery(this._unpublishOnSection).addClass("sfExpandedSection");
    },

    _collapseSection: function () {
        jQuery(this._unpublishOnSection).removeClass("sfExpandedSection");
    },

    /* -------------------- properties -------------------- */

    // Gets the date field for published on date
    get_publishedOnDateField: function () {
        return this._publishedOnDateField;
    },
    // sets the date field for published on date 
    set_publishedOnDateField: function (value) {
        this._publishedOnDateField = value;
    },

    // Gets the date field for expires on date
    get_unpublishOnDateField: function () {
        return this._unpublishOnDateField;
    },
    // sets the date field for expires on date
    set_unpublishOnDateField: function (value) {
        this._unpublishOnDateField = value;
    },

    get_scheduleButton: function () {
        return this._scheduleButton;
    },
    set_scheduleButton: function (value) {
        this._scheduleButton = value;
    },

    get_unpublishOnSection: function () {
        return this._unpublishOnSection;
    },
    set_unpublishOnSection: function (value) {
        this._unpublishOnSection = value;
    },

    get_unpublishOnExpander: function () {
        return this._unpublishOnExpander;
    },
    set_unpublishOnExpander: function (value) {
        this._unpublishOnExpander = value;
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
    },

    get_provider: function () {
        return this._provider;
    },
    set_provider: function (value) {
        this._provider = value;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    set_dataItem: function (value) {
        this._dataItem = value;
        this.setScheduleData(value);
    }
};

Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog.registerClass('Telerik.Sitefinity.Workflow.UI.WorkflowScheduleDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
