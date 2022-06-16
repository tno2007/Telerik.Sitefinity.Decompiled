/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField.initializeBase(this, [element]);

    this._wasPublished = false;
    this._pagePublicationDate = false;
}

Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */


    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _toggle: function (sender, args) {
        jQuery(this._expandableTarget).toggleClass("sfDisplayNone");
    },

    /* -------------------- private methods ----------- */

    _setOnLabelText: function (status, createdToday) {
        var jOnLabel = jQuery(this._onLabel);
        switch (status) {
            case "Draft":
                if (createdToday)
                    jOnLabel.text(this.get_clientLabelManager().getLabel('WorkflowResources', 'SavedAt'));
                else
                    jOnLabel.text(this.get_clientLabelManager().getLabel('WorkflowResources', 'SavedOn'));
                break;
            case "Scheduled":
                if (createdToday)
                    jOnLabel.text(this.get_clientLabelManager().getLabel('WorkflowResources', 'ToBePublishedAt'));
                else
                    jOnLabel.text(this.get_clientLabelManager().getLabel('WorkflowResources', 'ToBePublishedOn'));
                break;
            case "Rejected":
            case "Published":
            default:
                if (createdToday)
                    jOnLabel.text(this.get_clientLabelManager().getLabel('WorkflowResources', 'At'));
                else
                    jOnLabel.text(this.get_clientLabelManager().getLabel('WorkflowResources', 'On'));
                break;
        }
    },

    /* -------------------- properties ---------------- */

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        var dataItem = value.LastApprovalTrackingRecord;
        if (dataItem) {
            jQuery(this.get_element()).show();
            var status = dataItem.Status;
            var createdToday = false;
            if (dataItem.DateCreated)
                createdToday = !!(dataItem.DateCreated.toDateString() === (new Date()).toDateString());

            var isDraftNewerThanPublished = status === "Draft" && this._wasPublished;
            //Assume the UI status to be draft if it is not set (UX decision).
            if (!status) {
                status = "Draft"
            }

            if (status) {
                if (isDraftNewerThanPublished) {
                    this._statusLabel.innerHTML = this.get_clientLabelManager().getLabel("PageResources", "PublishedAndDraft");
                    jQuery(this._onLabel).text("");
                }
                else {
                    this._statusLabel.innerHTML = dataItem.UIStatus;
                    this._setOnLabelText(status, createdToday);
                }
            }
            if (dataItem.DateCreated && !isDraftNewerThanPublished) {
                var dateToDisplay;
                if (status !== "Scheduled")
                    dateToDisplay = dataItem.DateCreated;
                else
                    dateToDisplay = this._pagePublicationDate;

                if (createdToday)
                    jQuery(this._dateLabel).show().text(dateToDisplay.sitefinityLocaleFormat("hh:mm tt"));
                else
                    jQuery(this._dateLabel).show().text(dateToDisplay.sitefinityLocaleFormat("dd MMMM yyyy"));
            }
            else {
                jQuery(this._dateLabel).hide();
            }
            if (dataItem.Note) {
                switch (dataItem.Status)
                {
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

                jQuery("#noteWrapper").removeClass("sfDisplayNone").addClass("sfInlineBlock");
                this._noteLabel.innerText = dataItem.Note;
            }
        }
    },

    //property used to determine the draft newer than published status.
    set_wasPublished: function (value) {
        this._wasPublished = value;
    },

    set_pagePublicationDate: function (value) {
        this._pagePublicationDate = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField.registerClass("Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField", Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField);