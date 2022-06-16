Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager = function (rootUrl, providerName) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager.initializeBase(this);

    if (rootUrl[rootUrl.length - 1] === "/") {
        this._rootUrl = rootUrl;
    } else {
        this._rootUrl = rootUrl + "/";
    }
    this._providerName = providerName;
    if (!this._providerName) {
        this._providerName = "";
    }

    this._campaignServiceUrl = this._rootUrl + "Sitefinity/Services/Newsletters/Campaign.svc";
    this._abCampaignServiceUrl = this._rootUrl + "Sitefinity/Services/Newsletters/ABCampaign.svc";
    this._newslettersHandlerUrl = this._rootUrl + "Sitefinity/SFNwslttrs/";

    this.EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
    this.MESSAGE_BODY_TYPE = {
        PLAINTEXT: 0,
        HTML: 1,
        STANDARD: 2
    };
    // Consider using <see cref="Telerik.Sitefinity.Constants.EmailAddressRegexPattern"/>
    this.EMAIL_ADDRESS_REGEX = /[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}/i;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager.callBaseMethod(this, "dispose");
    },

    getCampaign: function (campaignId, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'GET',
            url: this._campaignServiceUrl + String.format('/{0}/?provider={1}', campaignId, this._providerName),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    getIssue: function (issueId, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'GET',
            url: this._campaignServiceUrl + String.format("/issue/{0}/?provider={1}", issueId, this._providerName),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    saveIssue: function (issue, onSuccess, onError, onComplete) {
        var id = issue.Id ? issue.Id : this.EMPTY_GUID;

        jQuery.ajax({
            type: 'PUT',
            url: this._campaignServiceUrl + String.format("/issue/{0}/?provider={1}", id, this._providerName),
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(issue),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    deleteIssue: function (issueId, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'DELETE',
            url: this._campaignServiceUrl + String.format('/issue/{0}/?provider={1}', issueId, this._providerName),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    getRawMessage: function (messageBody, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'POST',
            url: this._campaignServiceUrl + "/rawmessage/",
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(messageBody),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    createAbTest: function (issueAId, isFromScratch, issueB, onSuccess, onError, onComplete) {
        var issueBViewModel = this._getIssueViewModel(issueB);
        jQuery.ajax({
            type: 'PUT',
            url: this._abCampaignServiceUrl + String.format("/abtests/{0}/?provider={1}&isFromScratch={2}", issueAId, this._providerName, isFromScratch),
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(issueBViewModel),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    getAbTest: function (id, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'GET',
            url: this._abCampaignServiceUrl + String.format('/{0}/?provider={1}', id, this._providerName),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    saveAbTest: function (abTest, onSuccess, onError, onComplete) {
        var id = abTest.Id ? abTest.Id : this.EMPTY_GUID;

        jQuery.ajax({
            type: 'PUT',
            url: this._abCampaignServiceUrl + String.format('/{0}/?provider={1}', id, this._providerName),
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(abTest),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    startAbTesting: function (id, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'GET',
            url: this._abCampaignServiceUrl + String.format('/starttesting/{0}/?provider={1}', id, this._providerName),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    deleteAbTest: function (abTestId, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'DELETE',
            url: this._abCampaignServiceUrl + String.format('/{0}/?provider={1}', abTestId, this._providerName),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    getZoneEditorUrl: function (messageBodyId, campaignId, returnUrl) {
        var result = this._newslettersHandlerUrl + messageBodyId +
                        "?ReturnUrl=" + escape(returnUrl) +
                        "&CampaignId=" + campaignId;
        if (this._providerName) {
            result = result + "&providerName=" + this._providerName;
        }

        return result;
    },

    sendTestMessage: function (issue, emails, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'PUT',
            url: this._campaignServiceUrl + String.format('/sendtest/?provider={0}', this._providerName),
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify({ campaign: this._getIssueViewModel(issue), testEmailAddresses: emails }),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    _getIssueViewModel: function (issue) {
        var issueViewModel = {
            Id: issue.Id,
            Name: issue.Name,
            FromName: issue.FromName,
            ReplyToEmail: issue.ReplyToEmail,
            MessageSubject: issue.MessageSubject,
            UseGoogleTracking: issue.UseGoogleTracking,
            ListId: issue.ListId,
            MessageBody: issue.MessageBody,
            CampaignTemplateId: issue.CampaignTemplateId ? issue.CampaignTemplateId : this.EMPTY_GUID,
            RootCampaignId: issue.RootCampaignId
        };
        return issueViewModel;
    },

    setAbTestTestingNote: function (id, testingNote, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'PUT',
            url: this._abCampaignServiceUrl + String.format('/settestingnote/{0}/?provider={1}', id, this._providerName),
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(testingNote),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    setAbTestConclusion: function (id, conclusion, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'PUT',
            url: this._abCampaignServiceUrl + String.format('/setconclusion/{0}/?provider={1}', id, this._providerName),
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(conclusion),
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager', Sys.Component);
