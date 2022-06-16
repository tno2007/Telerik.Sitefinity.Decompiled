﻿$.fn.extend({
    insertAtCaret: function (myValue) {
        if (document.selection) {
            this.focus();
            sel = document.selection.createRange();
            sel.text = myValue;
            this.focus();
        }
        else if (this.selectionStart || this.selectionStart == '0') {
            var startPos = this.selectionStart;
            var endPos = this.selectionEnd;
            var scrollTop = this.scrollTop;
            this.val(this.val().substring(0, startPos) + myValue + this.val().substring(endPos, this.val().length));
            this.focus();
            this.selectionStart = startPos + myValue.length;
            this.selectionEnd = startPos + myValue.length;
            this.scrollTop = scrollTop;
        } else {
            this.val(this.val() + myValue);
            this.focus();
        }
    }
})

Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

var campaignDetailView = null;

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView.initializeBase(this, [element]);

    this._campaignServiceUrl = null;

    this._messageControl = null;
    this._backLinkLabel = null;
    this._dialogTitleLabel = null;

    this._campaignId = null;
    this._providerName = null;
    this._currentViewName = null;
    this._isCreateMode = null;
    this._cancelAction = null;
    this._origin = null;
    this._originCampaignName = null;

    this._loadingView = null;
    this._loadingCounter = 0;

    this._mailingListSelector = null;

    this._campaignPropertiesView = null;
    this._issuePropertiesView = null;
    this._issueMessageView = null;

    this._cancelDelegate = null;

    this._ajaxFailDelegate = null;
    this._ajaxCompleteDelegate = null;

    this.EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
    this.MESSAGE_BODY_TYPE = {
        PLAINTEXT: 0,
        HTML: 1,
        STANDARD: 2
    }
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView.callBaseMethod(this, "initialize");

        this._cancelDelegate = Function.createDelegate(this, this._cancelHandler);
        $addHandler(this.get_backLinkAnchor(), "click", this.get_cancelDelegate());

        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);
        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);

        this._mailingListSelectorCloseDelegate = Function.createDelegate(this, this._mailingListSelectorCloseHandler);
        this.get_mailingListSelector().add_close(this._mailingListSelectorCloseDelegate);

        campaignDetailView = this;
        this.get_campaignPropertiesView().set_campaignDetailView(this);
        this.get_issuePropertiesView().set_campaignDetailView(this);
        this.get_issueMessageView().set_campaignDetailView(this);
    },

    dispose: function () {
        if (this._cancelDelegate) {
            if (this.get_backLinkAnchor()) {
                $removeHandler(this.get_backLinkAnchor(), "click", this.get_cancelDelegate());
            }
            delete this._cancelDelegate;
        }

        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }
        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }

        if (this._mailingListSelectorCloseDelegate) {
            if (this.get_mailingListSelector()) {
                this.get_mailingListSelector().remove_close(this._mailingListSelectorCloseDelegate);
            }
            delete this._mailingListSelectorCloseDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    setForm: function (origin, campaignId, viewName, isCreateMode, originCampaignName) {
        this.reset();

        this._campaignId = campaignId;
        this._origin = origin;
        this._originCampaignName = originCampaignName;
        this.set_isCreateMode(isCreateMode);
        this.set_currentViewName(viewName);
    },

    reset: function () {
        this._loadingCounter = 0;
        this.setLoadingViewVisible(false);

        this._originCampaignName = null;
        this.get_dialogTitleLabel().innerHTML = "";
        this.set_cancelAction(function () { dialogBase.close(); });

        this.get_mailingListSelector().reset();

        this.get_campaignPropertiesView().reset();
        this.get_issuePropertiesView().reset();
        this.get_issueMessageView().reset();
    },

    setLoadingViewVisible: function (loading) {
        if (loading) {
            this._loadingCounter++;
        }
        else {
            if (this._loadingCounter > 0) {
                this._loadingCounter--;
            }
        }
        if (this._loadingCounter > 0) {
            jQuery(this.get_loadingView()).show();

            this.get_campaignPropertiesView().hideButtons();
            this.get_issuePropertiesView().hideButtons();
            this.get_issueMessageView().hideButtons();
        }
        else {
            jQuery(this.get_loadingView()).hide();

            this.get_campaignPropertiesView().showButtons();
            this.get_issuePropertiesView().showButtons();
            this.get_issueMessageView().showButtons();
        }
    },

    getCampaign: function (campaignId, onSuccess) {
        this.setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'GET',
            url: this.get_campaignServiceUrl() + String.format('/{0}/?provider={1}', campaignId, this.get_providerName()),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: this.get_ajaxFailDelegate(),
            complete: this._ajaxCompleteDelegate()
        });
    },

    saveCampaign: function (onSuccess, campaign) {
        var id = campaign.Id ? campaign.Id : this.EMPTY_GUID;

        this.setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'PUT',
            url: this.get_campaignServiceUrl() + String.format("/{0}/?provider={1}", id, this.get_providerName()),
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(campaign),
            success: onSuccess,
            error: this.get_ajaxFailDelegate(),
            complete: this._ajaxCompleteDelegate()
        });
    },

    getIssue: function (issueId, onSuccess) {
        this.setLoadingViewVisible(true);

        jQuery.ajax({
            type: 'GET',
            url: this.get_campaignServiceUrl() + String.format("/issue/{0}/?provider={1}", issueId, this.get_providerName()),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: this.get_ajaxFailDelegate(),
            complete: this.get_ajaxCompleteDelegate()
        });
    },

    saveIssue: function (onSuccess, issue) {
        var id = issue.Id ? issue.Id : this.EMPTY_GUID;

        this.setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'PUT',
            url: this.get_campaignServiceUrl() + String.format("/issue/{0}/?provider={1}", id, this.get_providerName()),
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(issue),
            success: onSuccess,
            error: this.get_ajaxFailDelegate(),
            complete: this.get_ajaxCompleteDelegate()
        });
    },

    showIssueTitleAndProperties: function (issue) {
        this.get_issuePropertiesView().set_issue(issue);
        this.get_issuePropertiesView().setBackToMessageMode();
        this.set_origin("content");
        this.set_currentViewName("issue");
    },

    backToMessage: function (issue) {
        this.get_issueMessageView().set_issue(issue);
        this.get_issueMessageView().updateCurrentIssue();
        this.set_currentViewName("message");
    },

    /* *************************** private methods *************************** */

    _updateView: function () {
        this._hideAllViews();

        switch (this.get_currentViewName()) {
            case "campaign":
                this.get_campaignPropertiesView().show();
                break;
            case "issue":
                this.get_issuePropertiesView().show();
                break;
            case "message":
                this.get_issueMessageView().show();
                break;
        }
    },

    _hideAllViews: function () {
        this.get_campaignPropertiesView().hide();
        this.get_issuePropertiesView().hide();
        this.get_issueMessageView().hide();
    },

    _cancelHandler: function (sender, args) {
        this._cancelAction();
    },

    _mailingListSelectorCloseHandler: function (sender, args) {
        switch (this.get_currentViewName()) {
            case "campaign":
                this.get_campaignPropertiesView().mailingListSelectorCloseHandler(sender, args);
                break;
            case "issue":
                this.get_issuePropertiesView().mailingListSelectorCloseHandler(sender, args);
                break;
        }
    },

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this.setLoadingViewVisible(false);
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    /* *************************** properties *************************** */

    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_backLinkLabel: function () {
        return this._backLinkLabel;
    },
    set_backLinkLabel: function (value) {
        this._backLinkLabel = value;
    },
    get_dialogTitleLabel: function () {
        return this._dialogTitleLabel;
    },
    set_dialogTitleLabel: function (value) {
        this._dialogTitleLabel = value;
    },
    get_currentViewName: function () {
        return this._currentViewName;
    },
    set_currentViewName: function (value) {
        this._currentViewName = value;
        this._updateView();
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },
    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },
    get_backLinkAnchor: function () {
        return this._backLinkAnchor;
    },
    set_backLinkAnchor: function (value) {
        this._backLinkAnchor = value;
    },
    get_isCreateMode: function () {
        return this._isCreateMode;
    },
    set_isCreateMode: function (value) {
        this._isCreateMode = value;
    },
    get_cancelAction: function () {
        return this._cancelAction;
    },
    set_cancelAction: function (value) {
        this._cancelAction = value;
    },
    get_campaignPropertiesView: function () {
        return this._campaignPropertiesView;
    },
    set_campaignPropertiesView: function (value) {
        this._campaignPropertiesView = value;
    },
    get_issuePropertiesView: function () {
        return this._issuePropertiesView;
    },
    set_issuePropertiesView: function (value) {
        this._issuePropertiesView = value;
    },
    get_issueMessageView: function () {
        return this._issueCampaignView;
    },
    set_issueMessageView: function (value) {
        this._issueCampaignView = value;
    },
    get_cancelDelegate: function () {
        return this._cancelDelegate;
    },
    get_campaignId: function () {
        return this._campaignId;
    },
    get_origin: function () {
        return this._origin;
    },
    set_origin: function (value) {
        this._origin = value;
    },
    get_originCampaignName: function () {
        return this._originCampaignName;
    },
    set_originCampaignName: function (value) {
        this._originCampaignName = value;
    },
    get_ajaxCompleteDelegate: function () {
        return this._ajaxCompleteDelegate;
    },
    get_ajaxFailDelegate: function () {
        return this._ajaxFailDelegate;
    },
    get_mailingListSelector: function () {
        return this._mailingListSelector;
    },
    set_mailingListSelector: function (value) {
        this._mailingListSelector = value;
    },
    get_campaignServiceUrl: function () {
        return this._campaignServiceUrl;
    },
    set_campaignServiceUrl: function (value) {
        this._campaignServiceUrl = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignDetailView', Sys.UI.Control);
