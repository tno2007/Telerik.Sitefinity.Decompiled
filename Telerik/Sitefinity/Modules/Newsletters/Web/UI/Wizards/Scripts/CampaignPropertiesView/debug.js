﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView.initializeBase(this, [element]);

    this._campaignDetailView = null;
    this._clientLabelManager = null;
    this._wrapper = null;

    this._campaign = null;

    this._campaignName = null;
    this._fromName = null;
    this._replyToEmail = null;
    this._fromScratchContainer = null;
    this._createFromTemplateRadio = null;
    this._createFromScratchRadio = null;
    this._templatesChoiceField = null;
    this._selectListsButton = null;
    this._selectedLists = null;
    this._googleTrackingCheckbox = null;
    this._htmlCampaignRadio = null;
    this._plainTextCampaignRadio = null;
    this._standardCampaignRadio = null;
    this._campaignTypeContainer = null;
    this._mailingListErrorMessage = null;

    this._propertiesViewButtons = null;
    this._propertiesCancelLink = null;
    this._btnCreateAndGoToAddFirstIssue = null;
    this._btnSaveChanges = null;

    this._selectListsButtonClickDelegate = null;
    this._btnCreateAndGoToAddFirstIssueClickDelegate = null;
    this._createFromScratchRadioChangeDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView.callBaseMethod(this, "initialize");

        this._selectListsButtonClickDelegate = Function.createDelegate(this, this._selectListsButtonClick);
        $addHandler(this.get_selectListsButton(), 'click', this._selectListsButtonClickDelegate);

        this._saveDraftDelegate = Function.createDelegate(this, this._saveDraftHandler);
        $addHandler(this.get_btnSaveChanges(), 'click', this._saveDraftDelegate);

        this._btnCreateAndGoToAddFirstIssueClickDelegate = Function.createDelegate(this, this._btnCreateAndGoToAddFirstIssueClick);
        $addHandler(this.get_btnCreateAndGoToAddFirstIssue(), 'click', this._btnCreateAndGoToAddFirstIssueClickDelegate);

        this._createFromScratchRadioChangeDelegate = Function.createDelegate(this, this._createFromScratchRadioChange);
        $addHandler(this.get_createFromScratchRadio(), 'click', this._createFromScratchRadioChangeDelegate);
        $addHandler(this.get_createFromTemplateRadio(), 'click', this._createFromScratchRadioChangeDelegate);
    },

    dispose: function () {
        this._unhookCampaignDetailDelegates();

        if (this._selectListsButtonClickDelegate) {
            if (this.get_selectListsButton()) {
                $removeHandler(this.get_selectListsButton(), 'click', this._selectListsButtonClickDelegate);
            }
            delete this._selectListsButtonClickDelegate;
        }

        if (this._saveDraftDelegate) {
            if (this.get_btnSaveChanges()) {
                $removeHandler(this.get_btnSaveChanges(), 'click', this._saveDraftDelegate);
            }
            delete this._saveDraftDelegate;
        }

        if (this._btnCreateAndGoToAddFirstIssueClickDelegate) {
            if (this.get_btnCreateAndGoToAddFirstIssue()) {
                $removeHandler(this.get_btnCreateAndGoToAddFirstIssue(), 'click', this._btnCreateAndGoToAddFirstIssueClickDelegate);
            }
            delete this._btnCreateAndGoToAddFirstIssueClickDelegate;
        }

        if (this._createFromScratchRadioChangeDelegate) {
            if (this.get_createFromScratchRadio())
                $removeHandler(this.get_createFromScratchRadio(), 'click', this._createFromScratchRadioChangeDelegate);
            if (this.get_createFromTemplateRadio())
                $removeHandler(this.get_createFromTemplateRadio(), 'click', this._createFromScratchRadioChangeDelegate);
            delete this._createFromScratchRadioChangeDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    show: function () {
        if (!this.get_campaignDetailView()) {
            throw "You need to set the parent dialog reference first: set_campaignDetailView(value).";
        }

        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._campaign = data;
            that._updateUi();
            jQuery(that.get_wrapper()).show();
        }

        if (this._campaign == null && this.get_campaignDetailView().get_isCreateMode() == false) {
            this.get_campaignDetailView().getCampaign(this.get_campaignDetailView().get_campaignId(), onSuccess);
        }
        else {
            onSuccess(this._campaign);
        }
    },

    hide: function () {
        jQuery(this.get_wrapper()).hide();
    },

    reset: function () {
        this._campaign = null;

        this.get_campaignName().reset();
        jQuery(this.get_selectedLists()).html("");
        this.get_fromName().reset();
        this.get_replyToEmail().reset();
        this.get_templatesChoiceField().reset();
        this.get_createFromScratchRadio().checked = true;
        jQuery(this.get_fromScratchContainer()).show();
        jQuery(this.get_templatesChoiceField().get_element()).hide();
        this.get_standardCampaignRadio().checked = true;
        this.get_googleTrackingCheckbox().checked = true;
        jQuery(this.get_selectListsButton()).find("span").html(this.get_clientLabelManager().getLabel("NewslettersResources", "SelectMailingList"));
        jQuery(this.get_mailingListErrorMessage()).hide();
    },

    showButtons: function () {
        jQuery(this.get_propertiesViewButtons()).show();
    },

    hideButtons: function () {
        jQuery(this.get_propertiesViewButtons()).hide();
    },

    mailingListSelectorCloseHandler: function (sender, args) {
        var selectedMailingList = args.get_data();
        if (selectedMailingList) {
            this._selectedMailingList = selectedMailingList;
            this._setSelectedMailListLabel(selectedMailingList.Title);
            this._fillMailListDefaults(selectedMailingList);
        }
    },

    /* *************************** private methods *************************** */

    _saveDraftHandler: function (sender, args) {
        if (this._isValid()) {
            var onSuccess;
            if (this.get_campaignDetailView().get_isCreateMode()) {
                onSuccess = function () { dialogBase.closeCreated(); };
            } else {
                onSuccess = function () { dialogBase.closeUpdated(); };
            }
            this._saveCampaign(onSuccess);
        }
    },

    _btnCreateAndGoToAddFirstIssueClick: function (sender, args) {
        if (this._isValid()) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                var campaign = data;
                that.get_campaignDetailView().set_cancelAction(function () { dialogBase.closeCreated(); });
                that.get_campaignDetailView().get_issuePropertiesView().set_rootCampaign(campaign);
                that.get_campaignDetailView().set_originCampaignName(campaign.Name);
                that.get_campaignDetailView().set_isCreateMode(true);
                that.get_campaignDetailView().set_currentViewName("issue");
            };
            this._saveCampaign(onSuccess);
        }
    },

    _saveCampaign: function (onSuccess) {
        this._updateCurrentCampaign();
        this.get_campaignDetailView().saveCampaign(onSuccess, this._getCampaignObject(this._campaign));
    },

    _updateCurrentCampaign: function () {
        if (!this._campaign) {
            this._campaign = {};

            var campaignType = this.get_campaignType();
            this._campaign.CampaignTemplateId = this.get_campaignTemplateId();
            this._campaign.MessageBody = {
                MessageBodyType: campaignType,
                BodyText: "",
                PlainTextVersion: null
            };
        }

        this._campaign.Name = this.get_campaignName().get_value();

        if (this._selectedMailingList) {
            this._campaign.ListId = this._selectedMailingList.Id;
        }

        this._campaign.FromName = this.get_fromName().get_value();
        this._campaign.ReplyToEmail = this.get_replyToEmail().get_value();
        this._campaign.UseGoogleTracking = this.get_googleTrackingCheckbox().checked;
    },

    _getCampaignObject: function () {
        if (!this._campaign) {
            return null;
        }
       
        var campaign = {
            Id: this._campaign.Id,
            Name: this._campaign.Name,
            FromName: this._campaign.FromName,
            ReplyToEmail: this._campaign.ReplyToEmail,
            MessageSubject: this._campaign.MessageSubject,
            UseGoogleTracking: this._campaign.UseGoogleTracking,
            ListId: this._campaign.ListId,
            MessageBody: this._campaign.MessageBody,
            CampaignTemplateId: this._campaign.CampaignTemplateId
        };
        return campaign;
    },

    _fillMailListDefaults: function (selectedMailingList) {
        if (selectedMailingList) {
            if (!this.get_fromName().get_value()) {
                this.get_fromName().set_value(selectedMailingList.DefaultFromName);
            }
            if (!this.get_replyToEmail().get_value()) {
                this.get_replyToEmail().set_value(selectedMailingList.DefaultReplyToEmail);
            }
        }
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

    _updateUi: function () {
        this.get_campaignDetailView().get_backLinkLabel().innerHTML = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToCampaigns");

        if (this._campaign) {
            this.get_campaignName().set_value(this._campaign.Name);
            this._setSelectedMailListLabel(this._campaign.ListTitle);
            this.get_fromName().set_value(this._campaign.FromName);
            this.get_replyToEmail().set_value(this._campaign.ReplyToEmail);
            this.get_googleTrackingCheckbox().checked = this._campaign.UseGoogleTracking;
        }
        if (!this.get_campaignDetailView().get_isCreateMode()) {
            // dialog title
            this.get_campaignDetailView().get_dialogTitleLabel().innerHTML = this.get_clientLabelManager().getLabel("Labels", "Edit") + " " + this._campaign.Name;
            // hide options for selecting a campaign type
            jQuery(this.get_campaignTypeContainer()).hide();
            // hide "Create and go to add the first issue" button
            jQuery(this.get_btnCreateAndGoToAddFirstIssue()).hide();
            // show "Save changes" button
            jQuery(this.get_btnSaveChanges()).show();
        }
        else {
            // dialog title
            this.get_campaignDetailView().get_dialogTitleLabel().innerHTML = this.get_clientLabelManager().getLabel("NewslettersResources", "CreateACampaign");
            // show options for selecting a campaign type
            jQuery(this.get_campaignTypeContainer()).show();
            // show "Create and go to add the first issue" button
            jQuery(this.get_btnCreateAndGoToAddFirstIssue()).show();
            // hide "Save changes" button
            jQuery(this.get_btnSaveChanges()).hide();
        }
    },

    _isValid: function () {
        var stepIsValid = true;

        if (this.get_campaignName().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_fromName().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_replyToEmail().validate() == false) {
            stepIsValid = false;
        }

        if ((!this.get_campaign() || !this.get_campaign().ListId || this.get_campaign().ListId == this.get_campaignDetailView().EMPTY_GUID) &&
            this.get_campaignDetailView().get_mailingListSelector().validate() == false) {
            jQuery(this.get_mailingListErrorMessage()).show();
            stepIsValid = false;
        }
        else {
            jQuery(this.get_mailingListErrorMessage()).hide();
        }

        return stepIsValid;
    },

    _unhookCampaignDetailDelegates: function () {
        if (this.get_campaignDetailView() && this.get_propertiesCancelLink()) {
            $removeHandler(this.get_propertiesCancelLink(), "click", this.get_campaignDetailView().get_cancelDelegate());
        }
    },

    _hookCampaignDetailDelegates: function () {
        if (this.get_campaignDetailView() && this.get_propertiesCancelLink()) {
            $addHandler(this.get_propertiesCancelLink(), "click", this.get_campaignDetailView().get_cancelDelegate());
        }
    },

    _selectListsButtonClick: function (sender, args) {
        if (this._campaign) {
            this.get_campaignDetailView().get_mailingListSelector().set_selectedMailingListId(this._campaign.ListId);
        }
        this.get_campaignDetailView().get_mailingListSelector().show();
    },

    _createFromScratchRadioChange: function (sender, args) {
        var createFromScratch = this.get_createFromScratchRadio().checked;
        if (createFromScratch) {
            jQuery(this.get_fromScratchContainer()).show();
            jQuery(this.get_templatesChoiceField().get_element()).hide();
        }
        else {
            jQuery(this.get_fromScratchContainer()).hide();
            jQuery(this.get_templatesChoiceField().get_element()).show();
        }
    },

    /* *************************** properties *************************** */

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
    get_wrapper: function () {
        return this._wrapper;
    },
    set_wrapper: function (value) {
        this._wrapper = value;
    },
    get_campaignType: function () {
        if (this.get_htmlCampaignRadio().checked) {
            return this.get_campaignDetailView().MESSAGE_BODY_TYPE.HTML;
        } else if (this.get_plainTextCampaignRadio().checked) {
            return this.get_campaignDetailView().MESSAGE_BODY_TYPE.PLAINTEXT;
        } else if (this.get_standardCampaignRadio().checked) {
            return this.get_campaignDetailView().MESSAGE_BODY_TYPE.STANDARD;
        }
        return this._HTML;
    },
    get_campaignTemplateId: function () {
        if (this.get_createFromTemplateRadio().checked) {
            campaignTemplateId = this.get_templatesChoiceField().get_value();
            if (campaignTemplateId.length < 1) {
                campaignTemplateId = this.get_campaignDetailView().EMPTY_GUID;
            }
        } else {
            campaignTemplateId = this.get_campaignDetailView().EMPTY_GUID;
        }
        return campaignTemplateId;
    },
    get_campaignName: function () {
        return this._campaignName;
    },
    set_campaignName: function (value) {
        this._campaignName = value;
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
    get_fromScratchContainer: function () {
        return this._fromScratchContainer;
    },
    set_fromScratchContainer: function (value) {
        this._fromScratchContainer = value;
    },
    get_createFromTemplateRadio: function () {
        return this._createFromTemplateRadio;
    },
    set_createFromTemplateRadio: function (value) {
        this._createFromTemplateRadio = value;
    },
    get_createFromScratchRadio: function () {
        return this._createFromScratchRadio;
    },
    set_createFromScratchRadio: function (value) {
        this._createFromScratchRadio = value;
    },
    get_templatesChoiceField: function () {
        return this._templatesChoiceField;
    },
    set_templatesChoiceField: function (value) {
        this._templatesChoiceField = value;
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
    get_googleTrackingCheckbox: function () {
        return this._googleTrackingCheckbox;
    },
    set_googleTrackingCheckbox: function (value) {
        this._googleTrackingCheckbox = value;
    },
    get_htmlCampaignRadio: function () {
        return this._htmlCampaignRadio;
    },
    set_htmlCampaignRadio: function (value) {
        this._htmlCampaignRadio = value;
    },
    get_plainTextCampaignRadio: function () {
        return this._plainTextCampaignRadio;
    },
    set_plainTextCampaignRadio: function (value) {
        this._plainTextCampaignRadio = value;
    },
    get_standardCampaignRadio: function () {
        return this._standardCampaignRadio;
    },
    set_standardCampaignRadio: function (value) {
        this._standardCampaignRadio = value;
    },
    get_propertiesViewButtons: function () {
        return this._propertiesViewButtons;
    },
    set_propertiesViewButtons: function (value) {
        this._propertiesViewButtons = value;
    },
    get_btnCreateAndGoToAddFirstIssue: function () {
        return this._btnCreateAndGoToAddFirstIssue;
    },
    set_btnCreateAndGoToAddFirstIssue: function (value) {
        this._btnCreateAndGoToAddFirstIssue = value;
    },
    get_btnSaveChanges: function () {
        return this._btnSaveChanges;
    },
    set_btnSaveChanges: function (value) {
        this._btnSaveChanges = value;
    },
    get_propertiesCancelLink: function () {
        return this._propertiesCancelLink;
    },
    set_propertiesCancelLink: function (value) {
        this._propertiesCancelLink = value;
    },
    get_campaignTypeContainer: function () {
        return this._campaignTypeContainer;
    },
    set_campaignTypeContainer: function (value) {
        this._campaignTypeContainer = value;
    },
    get_mailingListErrorMessage: function () {
        return this._mailingListErrorMessage;
    },
    set_mailingListErrorMessage: function (value) {
        this._mailingListErrorMessage = value;
    },
    get_campaign: function () {
        return this._campaign;
    },
    set_campaign: function (value) {
        this._campaign = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignPropertiesView', Sys.UI.Control);