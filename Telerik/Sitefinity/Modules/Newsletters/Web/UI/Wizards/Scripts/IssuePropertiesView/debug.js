Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView.initializeBase(this, [element]);

    this._rootCampaign = null;
    this._issue = null;
    this._wrapper = null;
    this._campaignDetailView = null;
    this._clientLabelManager = null;
    this._cancelLink = null;
    this._selectListsButton = null;
    this._selectedLists = null;
    this._selectedMailingList = null;

    this._messageSubject = null;
    this._fromName = null;
    this._replyToEmail = null;
    this._issueName = null;
    this._mailingListErrorMessage = null;

    this._issuePropertiesViewButtons = null;
    this._btnCreateAndGoToAddContent = null;
    this._btnCreateAndGoToEmailCampaigns = null;

    this._selectListsButtonClickDelegate = null;
    this._btnCreateAndGoToAddContentClickDelegate = null;
    this._btnCreateAndGoToEmailCampaignsClickDelegate = null;

    this._backToMessageMode = null;
    this._oldOrigin = null;
    this._oldCancelAction = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView.callBaseMethod(this, "initialize");

        this._selectListsButtonClickDelegate = Function.createDelegate(this, this._selectListsButtonClick);
        $addHandler(this.get_selectListsButton(), 'click', this._selectListsButtonClickDelegate);

        this._btnCreateAndGoToAddContentClickDelegate = Function.createDelegate(this, this._btnCreateAndGoToAddContentClick);
        $addHandler(this.get_btnCreateAndGoToAddContent(), "click", this._btnCreateAndGoToAddContentClickDelegate);

        this._btnCreateAndGoToEmailCampaignsClickDelegate = Function.createDelegate(this, this._saveDraftHandler);
        $addHandler(this.get_btnCreateAndGoToEmailCampaigns(), "click", this._btnCreateAndGoToEmailCampaignsClickDelegate);
    },

    dispose: function () {
        this._unhookCampaignDetailDelegates();

        if (this._selectListsButtonClickDelegate) {
            if (this.get_selectListsButton()) {
                $removeHandler(this.get_selectListsButton(), 'click', this._selectListsButtonClickDelegate);
            }
            delete this._selectListsButtonClickDelegate;
        }

        if (this._btnCreateAndGoToAddContentClickDelegate) {
            if (this.get_btnCreateAndGoToAddContent()) {
                $removeHandler(this.get_btnCreateAndGoToAddContent(), "click", this._btnCreateAndGoToAddContentClickDelegate);
            }
            delete this._btnCreateAndGoToAddContentClickDelegate;
        }

        if (this._btnCreateAndGoToEmailCampaignsClickDelegate) {
            if (this.get_btnCreateAndGoToEmailCampaigns()) {
                $removeHandler(this.get_btnCreateAndGoToEmailCampaigns(), "click", this._btnCreateAndGoToEmailCampaignsClickDelegate);
            }
            delete this._btnCreateAndGoToEmailCampaignsClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    show: function () {
        if (!this.get_campaignDetailView()) {
            throw "You need to set the parent dialog reference first: set_campaignDetailView(value).";
        }

        var that = this;
        var getCampaignSuccess = function (data, textStatus, jqXHR) {
            that._rootCampaign = data;
            that._copyFromRootCampaign();
            that._updateUi();
            jQuery(that.get_wrapper()).show();
            that._makePropsForm(false);
        }
        var getIssueSuccess = function (data, textStatus, jqXHR) {
            that._issue = data;
            that._rootCampaign = { Id: that._issue.RootCampaignId };
            that._updateUi();
            jQuery(that.get_wrapper()).show();
            that._makePropsForm(false);
        }

        if (this.get_campaignDetailView().get_isCreateMode()) {
            if (this._rootCampaign) {
                getCampaignSuccess(this._rootCampaign);
            }
            else {
                this.get_campaignDetailView().getCampaign(this.get_campaignDetailView().get_campaignId(), getCampaignSuccess);
            }
        }
        else {
            if (this._issue) {
                getIssueSuccess(this._issue);
            }
            else {
                this.get_campaignDetailView().getIssue(this.get_campaignDetailView().get_campaignId(), getIssueSuccess);
            }
        }
    },

    hide: function () {
        jQuery(this.get_wrapper()).hide();
        this._makePropsForm(true);
    },

    hideButtons: function () {
        jQuery(this.get_issuePropertiesViewButtons()).hide();
    },

    showButtons: function () {
        jQuery(this.get_issuePropertiesViewButtons()).show();
    },

    reset: function () {
        this._issue = null;
        this._rootCampaign = null;
        this.set_selectedMailingList(null);
        this.get_messageSubject().reset();
        this.get_fromName().reset();
        this.get_replyToEmail().reset();
        this.get_issueName().reset();
        jQuery(this.get_selectListsButton()).find("span").html(this.get_clientLabelManager().getLabel("NewslettersResources", "SelectMailingList"));
        jQuery(this.get_issuePropertiesViewButtons()).show();
        jQuery(this.get_mailingListErrorMessage()).hide();
        this._backToMessageMode = false;
    },

    mailingListSelectorCloseHandler: function (sender, args) {
        var selectedMailingList = args.get_data();
        if (selectedMailingList) {
            this.set_selectedMailingList(selectedMailingList);
        }
    },

    setBackToMessageMode: function () {
        this._backToMessageMode = true;

        var that = this;
        this._oldCancelAction = this.get_campaignDetailView().get_cancelAction();
        this._oldOrigin = this.get_campaignDetailView().get_origin();
        var cancelAction = function (sender, args) {
            that._backToMessageMode = false;
            that.get_campaignDetailView().set_cancelAction(that._oldCancelAction);
            that.get_campaignDetailView().set_origin(that._oldOrigin);
            that.get_campaignDetailView().set_currentViewName("message");
        };

        this.get_campaignDetailView().set_cancelAction(cancelAction);
    },

    /* *************************** private methods *************************** */

    _makePropsForm: function (flag) {
        if (jQuery(this.get_wrapper()).is(":hidden"))
            return;

        if (flag) {
            jQuery("body").addClass("sfWidgetTmpDialog");
            jQuery(".sfNewItemForm").removeClass("sfNewItemForm").addClass("sfNewContentForm");

        } else {
            jQuery("body").removeClass("sfWidgetTmpDialog");
            jQuery(".sfNewContentForm").removeClass("sfNewContentForm").addClass("sfNewItemForm");
        }
    },

    _copyFromRootCampaign: function () {
        if (!this._rootCampaign) {
            throw "You need to set root campaign first.";
        }

        this._issue = {};

        for (var v in this._rootCampaign) {
            this._issue[v] = this._rootCampaign[v];
        }

        this._issue.Id = this.get_campaignDetailView().EMPTY_GUID;
        this._issue.Name = null;
        this._issue.RootCampaignId = this._rootCampaign.Id;
        this._issue.MessageBody = null;
    },

    _unhookCampaignDetailDelegates: function () {
        if (this.get_campaignDetailView() && this.get_cancelLink()) {
            $removeHandler(this.get_cancelLink(), "click", this.get_campaignDetailView().get_cancelDelegate());
        }
    },

    _hookCampaignDetailDelegates: function () {
        if (this.get_campaignDetailView() && this.get_cancelLink()) {
            $addHandler(this.get_cancelLink(), "click", this.get_campaignDetailView().get_cancelDelegate());
        }
    },

    _btnCreateAndGoToAddContentClick: function (sender, args) {
        if (this._isValid()) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                var issue = data;
                that.get_campaignDetailView().set_cancelAction(function () { dialogBase.closeCreated(); });
                that.get_campaignDetailView().get_issueMessageView().set_issue(issue);
                that.get_campaignDetailView().set_originCampaignName(that._rootCampaign.Name);
                that.get_campaignDetailView().set_isCreateMode(false);               
                that.get_campaignDetailView().set_currentViewName("message");
            };
            this._saveIssue(onSuccess);
        }
    },

    _saveDraftHandler: function (sender, args) {
        if (this._isValid()) {
            var onSuccess;
            if (this.get_campaignDetailView().get_isCreateMode()) {
                onSuccess = function () { dialogBase.closeCreated(); };
            } else {
                onSuccess = function () { dialogBase.closeUpdated(); };
            }
            if (this._backToMessageMode) {
                var that = this;
                onSuccess = function (data, textStatus, jqXHR) {
                    that.get_campaignDetailView().set_cancelAction(that._oldCancelAction);
                    that.get_campaignDetailView().set_origin(that._oldOrigin);
                    that.get_campaignDetailView().backToMessage(data);
                };
            }
            this._saveIssue(onSuccess);
        }
    },

    _isValid: function () {
        var stepIsValid = true;

        if (this.get_issueName().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_messageSubject().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_fromName().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_replyToEmail().validate() == false) {
            stepIsValid = false;
        }

        if ((!this._issue || !this._issue.ListId || this._issue.ListId == this.get_campaignDetailView().EMPTY_GUID) &&
                this.get_campaignDetailView().get_mailingListSelector().validate() == false) {
            jQuery(this.get_mailingListErrorMessage()).show();
            stepIsValid = false;
        }
        else {
            jQuery(this.get_mailingListErrorMessage()).hide();
        }

        return stepIsValid;
    },

    _updateUi: function () {
        var backLinkLabel;
        switch (this.get_campaignDetailView().get_origin()) {
            case 'campaigns':
                backLinkLabel = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToCampaigns");
                break;
            case 'content':
                backLinkLabel = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToContent");
                break;
            case 'overview':
                var rootCampaignName = (this._rootCampaign.Name.length < 29) ? this._rootCampaign.Name : this._rootCampaign.Name.substring(0, 26).concat("...");
                backLinkLabel = String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "BackToFormat"),
                    rootCampaignName.htmlEncode());
                break;
        }
        this.get_campaignDetailView().get_backLinkLabel().innerHTML = backLinkLabel;

        if (this.get_campaignDetailView().get_isCreateMode()) {
            this.get_campaignDetailView().get_dialogTitleLabel().innerHTML =
                String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "CreateIssueFor"), this._rootCampaign.Name.htmlEncode());
        }
        else if (this._issue) {
            this.get_campaignDetailView().get_dialogTitleLabel().innerHTML = this.get_clientLabelManager().getLabel("Labels", "Edit") + " " + this._issue.Name;
        }

        if (this._issue) {
            this.get_issueName().set_value(this._issue.Name);
            this._setSelectedMailListLabel(this._issue.ListTitle.htmlEncode());
            this.get_messageSubject().set_value(this._issue.MessageSubject);
            this.get_fromName().set_value(this._issue.FromName);
            this.get_replyToEmail().set_value(this._issue.ReplyToEmail);
        }

        if (this.get_campaignDetailView().get_isCreateMode()) {
            jQuery(this.get_btnCreateAndGoToAddContent()).show();
            if (this.get_campaignDetailView().get_origin() == "overview") {
                var rootCampaignName = (this._rootCampaign.Name.length < 29) ? this._rootCampaign.Name : this._rootCampaign.Name.substring(0, 26).concat("...");
                var createBtnLabel = String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "CreateAndGoToFormat"), rootCampaignName.htmlEncode());
                jQuery(this.get_btnCreateAndGoToEmailCampaigns()).find("span").html(createBtnLabel);
            }
            else {
                jQuery(this.get_btnCreateAndGoToEmailCampaigns()).find("span").html(this.get_clientLabelManager().getLabel("NewslettersResources", "CreateAndGoEmailCampaigns"));
            }
        }
        else {
            jQuery(this.get_btnCreateAndGoToAddContent()).hide();
            jQuery(this.get_btnCreateAndGoToEmailCampaigns()).find("span").html(this.get_clientLabelManager().getLabel("Labels", "SaveChanges"));
        }
    },

    _getIssueObject: function () {
        if (!this._issue) {
            return null;
        }
        var issue = {
            Id: this._issue.Id,
            Name: this._issue.Name,
            FromName: this._issue.FromName,
            ReplyToEmail: jQuery.trim(this._issue.ReplyToEmail),
            MessageSubject: this._issue.MessageSubject,
            UseGoogleTracking: this._issue.UseGoogleTracking,
            ListId: this._issue.ListId,
            MessageBody: this._issue.MessageBody,
            CampaignTemplateId: this.get_campaignDetailView().EMPTY_GUID,
            RootCampaignId: this._rootCampaign.Id
        };
        return issue;
    },

    _selectListsButtonClick: function (sender, args) {
        if (this._issue) {
            this.get_campaignDetailView().get_mailingListSelector().set_selectedMailingListId(this._issue.ListId);
        }
        this.get_campaignDetailView().get_mailingListSelector().show();
    },

    _setSelectedMailListLabel: function (text) {
        if (text) {
            this.get_selectedLists().innerHTML = "";

            var listItem = document.createElement("LI");
            var span = document.createElement("SPAN");
            span.innerHTML = text;
            listItem.appendChild(span);
            this.get_selectedLists().appendChild(listItem);

            jQuery(this.get_selectListsButton()).find("span").html(this.get_clientLabelManager().getLabel("Labels", "ChangeEllipsis"));
        }
    },

    _updateCurrentIssue: function () {
        if (!this._issue) {
            this._issue = {};

            this._issue.CampaignTemplateId = this.get_campaignDetailView().EMPTY_GUID;
            this._issue.MessageBody = {
                MessageBodyType: this._rootCampaign.MessageBody.MessageBodyType,
                BodyText: this._rootCampaign.MessageBody.BodyText,
                PlainTextVersion: null
            };
        }

        this._issue.Name = this.get_issueName().get_value();

        if (this.get_selectedMailingList()) {
            this._issue.ListId = this.get_selectedMailingList().Id;
        }
        this._issue.MessageSubject = this.get_messageSubject().get_value();
        this._issue.FromName = this.get_fromName().get_value();
        this._issue.ReplyToEmail = this.get_replyToEmail().get_value();
        this._issue.RootCamapignId = this._rootCampaign.Id;
    },

    _saveIssue: function (onSuccess) {
        this._updateCurrentIssue();
        this.get_campaignDetailView().saveIssue(onSuccess, this._getIssueObject());
    },

    _fillMailListDefaults: function () {
        if (this.get_selectedMailingList()) {
            if (!this.get_fromName().get_value()) {
                this.get_fromName().set_value(this.get_selectedMailingList().DefaultFromName);
            }
            if (!this.get_replyToEmail().get_value()) {
                this.get_replyToEmail().set_value(this.get_selectedMailingList().DefaultReplyToEmail);
            }
            if (!this.get_messageSubject().get_value()) {
                this.get_messageSubject().set_value(this.get_selectedMailingList().DefaultSubject);
            }
        }
    },

    /* *************************** properties *************************** */

    get_selectedMailingList: function () {
        return this._selectedMailingList;
    },
    set_selectedMailingList: function (value) {
        this._selectedMailingList = value;
        if (value) {
            this._setSelectedMailListLabel(value.Title);
            this._fillMailListDefaults();
        }
    },
    get_wrapper: function () {
        return this._wrapper;
    },
    set_wrapper: function (value) {
        this._wrapper = value;
    },
    get_campaignDetailView: function () {
        return this._campaignDetailView;
    },
    set_campaignDetailView: function (value) {
        this._unhookCampaignDetailDelegates();
        this._campaignDetailView = value;
        this._hookCampaignDetailDelegates();
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },
    get_selectListsButton: function () {
        return this._selectListsButton;
    },
    set_selectListsButton: function (value) {
        this._selectListsButton = value;
    },
    get_selectedLists: function () {
        return this._selectedLists;
    },
    set_selectedLists: function (value) {
        this._selectedLists = value;
    },
    get_messageSubject: function () {
        return this._messageSubject;
    },
    set_messageSubject: function (value) {
        this._messageSubject = value;
    },
    get_fromName: function () {
        return this._fromName;
    },
    set_fromName: function (value) {
        this._fromName = value;
    },
    get_replyToEmail: function () {
        return this._replyToEmail;
    },
    set_replyToEmail: function (value) {
        this._replyToEmail = value;
    },
    get_issueName: function () {
        return this._issueName;
    },
    set_issueName: function (value) {
        this._issueName = value;
    },
    get_issuePropertiesViewButtons: function () {
        return this._issuePropertiesViewButtons;
    },
    set_issuePropertiesViewButtons: function (value) {
        this._issuePropertiesViewButtons = value;
    },
    get_mailingListErrorMessage: function () {
        return this._mailingListErrorMessage;
    },
    set_mailingListErrorMessage: function (value) {
        this._mailingListErrorMessage = value;
    },
    get_btnCreateAndGoToAddContent: function () {
        return this._btnCreateAndGoToAddContent;
    },
    set_btnCreateAndGoToAddContent: function (value) {
        this._btnCreateAndGoToAddContent = value;
    },
    get_btnCreateAndGoToEmailCampaigns: function () {
        return this._btnCreateAndGoToEmailCampaigns;
    },
    set_btnCreateAndGoToEmailCampaigns: function (value) {
        this._btnCreateAndGoToEmailCampaigns = value;
    },
    set_rootCampaign: function (value) {
        this._rootCampaign = value;
    },
    set_issue: function (value) {
        this._issue = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssuePropertiesView', Sys.UI.Control);
