/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField.initializeBase(this, [element]);
    this._element = element;

    this._statusLabel = null;
    this._onLabel = null;
    this._dateLabel = null;
    this._authorLabel = null;
    this._expandButton = null;
    this._expandableTarget = null;
    this._noteLabel = null;
    this._openRevisionHistoryButton = null;
    this._isBoundToItem = false;
    this._clientLabelManager = null;

    this._historyCommandName = null;

    this._publicationDateLabel = null;
    this._toggleDelegate = null;
    this._openRevisionHistoryDelegate = null;

}

Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField.callBaseMethod(this, "initialize");

        if (this._expandButton) {
            this._toggleDelegate = Function.createDelegate(this, this._toggle);
            $addHandler(this._expandButton, 'click', this._toggleDelegate);
        }
        if (this._openRevisionHistoryButton) {
            this._openRevisionHistoryDelegate = Function.createDelegate(this, this._openRevisionHistory);
            $addHandler(this._openRevisionHistoryButton, 'click', this._openRevisionHistoryDelegate);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField.callBaseMethod(this, "dispose");

        if (this._toggleDelegate) {
            if (this._expandButton) {
                $removeHandler(this._expandButton, 'click', this._toggleDelegate);
            }
            delete this._toggleDelegate;
        }

        if (this._openRevisionHistoryDelegate) {
            if (this._openRevisionHistoryButton) {
                $removeHandler(this._openRevisionHistoryButton, 'click', this._openRevisionHistoryDelegate);
            }
            delete this._openRevisionHistoryDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this._statusLabel.innerHTML = "";
        this._dateLabel.innerHTML = "";
        this._authorLabel.innerHTML = "";
        this._noteLabel.innerHTML = "";
        this._publicationDateLabel.innerHTML = "";
        this._isBoundToItem = false;

        jQuery(this._element).hide();
        jQuery("#noteWrapper").hide();
        jQuery(this._expandableTarget).hide();
        jQuery("#wfStatusPubDateHolder").hide();
    },

    /* -------------------- events -------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    /* -------------------- event handlers ------------ */

    _toggle: function (sender, args) {
        // TODO: remove this function when jQuery toggle function is fixed.
        var jQueryElement = jQuery(this._expandableTarget);
        var state = jQueryElement.css("display");
        if (state == "none") {
            jQueryElement.show();
            jQuery("#noteWrapper").addClass("sfExpandedSection");
        }
        else {
            jQueryElement.hide();
            jQuery("#noteWrapper").removeClass("sfExpandedSection");
        }
    },

    _openRevisionHistory: function (sender, args) {
        var eventArgs = new Telerik.Sitefinity.CommandEventArgs(this._historyCommandName);
        this.onCommand(sender, eventArgs);
    },

    onCommand: function (sender, args) {
        var h = this.get_events().getHandler('command');
        if (h) h(this, args);
    },

    /* -------------------- private methods ----------- */

    // Returns the dynamic content's Lifecycle object if presented in the value parameter. 
    _isItemLifecycleAvailable: function (value){
        if (value && value.Item && !value.Item.isPopulatedWithDefaultValues && value.Item.Lifecycle) {
            return value.Item.Lifecycle;
        }
        else {
            return null;
        }
    },

    /* -------------------- properties ---------------- */

    // Sets the date item context that is used for populating the workflow menu fields information.
    set_dataItemContext: function (value) {
        var itemLifecycle = value.LifecycleStatus || this._isItemLifecycleAvailable(value);
        if (itemLifecycle) {
            this._isBoundToItem = true;

            jQuery(this._element).show();

            if (itemLifecycle.WorkflowStatus) {
                this._statusLabel.innerHTML = itemLifecycle.WorkflowStatus;
            }

            if (itemLifecycle.LastModified) {
                this._dateLabel.innerHTML = itemLifecycle.LastModified.sitefinityLocaleFormat("dd MMMM, yyyy, hh:mm tt");
            }
            if (itemLifecycle.LastModifiedBy) {
                this._authorLabel.innerHTML = itemLifecycle.LastModifiedBy;
            }
            if (itemLifecycle.IsPublished && itemLifecycle.PublicationDate) {
                jQuery("#wfStatusPubDateHolder").show();
                this._publicationDateLabel.innerHTML = itemLifecycle.PublicationDate.sitefinityLocaleFormat("dd MMMM, yyyy, hh:mm tt");
            }
        }
        var workflowItem = value.LastApprovalTrackingRecord;
        if (workflowItem) {
            jQuery(this._element).show();
            if (workflowItem.UIStatus) {
                this._statusLabel.innerHTML = workflowItem.UIStatus;
            }
            if (!this._isBoundToItem) {
                if (workflowItem.DateCreated) {
                    var dateModified = workflowItem.DateCreated;
                    this._dateLabel.innerHTML = dateModified.sitefinityLocaleFormat("dd MMMM, yyyy, hh:mm tt");
                }
                if (workflowItem.Email) {
                    this._authorLabel.innerHTML = workflowItem.Email;
                }
            }
            if (workflowItem.Note) {
                switch (workflowItem.Status) {
                    case "AwaitingApproval":
                    case "AwaitingReview":
                    case "AwaitingPublishing":
                    case "SaveAsAwaitingApproval":
                    case "SaveAsAwaitingReview":
                    case "SaveAsAwaitingPublishing":
                        this._expandButton.innerHTML = this.get_clientLabelManager().getLabel("WorkflowResources", "NotesForApprovers");
                        break;
                    case "Rejected":
                    case "RejectedForPublishing":
                    case "RejectedForReview":
                        this._expandButton.innerHTML = this.get_clientLabelManager().getLabel("WorkflowResources", "WhyRejected");
                        break;
                    default:
                        break;
                }
                jQuery("#noteWrapper").show();
                this._noteLabel.innerText = workflowItem.Note;
            }
        }
    },

get_statusLabel: function () {
    return this._statusLabel;
},
set_statusLabel: function (value) {
    this._statusLabel = value;
},

get_onLabel: function () {
    return this._onLabel;
},
set_onLabel: function (value) {
    this._onLabel = value;
},

get_dateLabel: function () {
    return this._dateLabel;
},
set_dateLabel: function (value) {
    this._dateLabel = value;
},

get_publicationDateLabel: function () {
    return this._publicationDateLabel;
},
set_publicationDateLabel: function (value) {
    this._publicationDateLabel = value;
},

get_authorLabel: function () {
    return this._authorLabel;
},
set_authorLabel: function (value) {
    this._authorLabel = value;
},

get_expandButton: function () {
    return this._expandButton;
},
set_expandButton: function (value) {
    this._expandButton = value;
},

get_expandableTarget: function () {
    return this._expandableTarget;
},
set_expandableTarget: function (value) {
    this._expandableTarget = value;
},

get_noteLabel: function () {
    return this._noteLabel;
},
set_noteLabel: function (value) {
    this._noteLabel = value;
},

get_clientLabelManager: function () {
    return this._clientLabelManager;
},
set_clientLabelManager: function (value) {
    this._clientLabelManager = value;
},

get_openRevisionHistoryButton: function () {
    return this._openRevisionHistoryButton;
},
set_openRevisionHistoryButton: function (value) {
    this._openRevisionHistoryButton = value;
},
};

Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext, Telerik.Sitefinity.Web.UI.Fields.ICommandField);