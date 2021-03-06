Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView.initializeBase(this, [element]);

    this._issuePropertiesWrapper = null;
    this._clientLabelManager = null;

    this._backLink = null;
    this._cancelLink = null;
    this._backLinkClickDelegate = null;

    this._loadingView = null;
    this._loadingCounter = 0;
    this._dialogTitle = null;
    this._buttonsPanel = null;
    this._messageControl = null;

    this._createAndCloseButton = null;
    this._createAndCloseButtonClickDelegate = null;
    this._createAndCloseLabel = null;

    this._goToContentButton = null;
    this._goToContentButtonClickDelegate = null;

    this._nameTextField = null;
    this._nameTextFieldRead = null;
    this._messageSubjectTextField = null;
    this._fromNameTextField = null;
    this._replyToEmailTextField = null;

    this._selectListsButton = null;
    this._selectListsButtonClickDelegate = null;
    this._selectedLists = null;
    this._selectedMailingList = null;
    this._mailingListErrorMessage = null;
    this._mailingListSelector = null;

    this._saveButton = null;
    this._saveButtonClickDelegate = null;

    this._rootUrl = null;
    this._providerName = null;
    this._manager = null;
    this._ajaxCompleteDelegate = null;
    this._ajaxFailDelegate = null;

    this._issueId = null;
    this._issue = null;
    this._isCreateMode = null;
    this._isFromScratch = null;
    this._isBIssue = null;
    this._issueAId = null;
    this._issueAName = null;
    this._campaignId = null;
    this._origin = null;

    this.HIDE_RESULT = {
        BACK: "back",
        GO_TO_MESSAGE: "go-to-message",
        SAVE: "save"
    };
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._backLinkClickDelegate = Function.createDelegate(this, this._backLinkClick);
        $addHandler(this.get_backLink(), "click", this._backLinkClickDelegate);
        $addHandler(this.get_cancelLink(), "click", this._backLinkClickDelegate);

        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);

        this._createAndCloseButtonClickDelegate = Function.createDelegate(this, this._createAndCloseButtonClickHandler);
        $addHandler(this.get_createAndCloseButton(), "click", this._createAndCloseButtonClickDelegate);

        this._goToContentClicDelegate = Function.createDelegate(this, this._goToContentClickHandler);
        $addHandler(this.get_goToContentButton(), "click", this._goToContentClicDelegate);

        this._selectListsButtonClickDelegate = Function.createDelegate(this, this._selectListsButtonClick);
        $addHandler(this.get_selectListsButton(), 'click', this._selectListsButtonClickDelegate);

        this._mailingListSelectorCloseDelegate = Function.createDelegate(this, this._mailingListSelectorCloseHandler);
        this.get_mailingListSelector().add_close(this._mailingListSelectorCloseDelegate);

        this._saveButtonClickDelegate = Function.createDelegate(this, this._saveButtonClickHandler);
        $addHandler(this.get_saveButton(), "click", this._saveButtonClickDelegate);
    },

    dispose: function () {
        if (this._backLinkClickDelegate) {
            if (this.get_backLink()) {
                $removeHandler(this.get_backLink(), "click", this._backLinkClickDelegate);
            }
            if (this.get_cancelLink()) {
                $removeHandler(this.get_cancelLink(), "click", this._backLinkClickDelegate);
            }
            delete this._backLinkClickDelegate;
        }

        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }
        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._createAndCloseButtonClickDelegate) {
            if (this.get_createAndCloseButton()) {
                $removeHandler(this.get_createAndCloseButton(), "click", this._createAndCloseButtonClickDelegate);
            }
            delete this._createAndCloseButtonClickDelegate;
        }

        if (this._goToContentClickDelegate) {
            if (this.get_goToContentButton()) {
                $removeHandler(this.get_goToContentButton(), "click", this._goToContentClickDelegate);
            }
            delete this._goToContentClickDelegate;
        }

        if (this._selectListsButtonClickDelegate) {
            if (this.get_selectListsButton()) {
                $removeHandler(this.get_selectListsButton(), 'click', this._selectListsButtonClickDelegate);
            }
            delete this._selectListsButtonClickDelegate;
        }

        if (this._mailingListSelectorCloseDelegate) {
            if (this.get_mailingListSelector()) {
                this.get_mailingListSelector().remove_close(this._mailingListSelectorCloseDelegate);
            }
            delete this._mailingListSelectorCloseDelegate;
        }

        if (this._saveButtonClickDelegate) {
            if (this.get_saveButton()) {
                $removeHandler(this.get_saveButton(), "click", this._saveButtonClickDelegate);
            }
            delete this._saveButtonClickDelegate;
        }

        this._manager.dispose();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView.callBaseMethod(this, "dispose");
    },

    reset: function () {
        this._loadingCounter = 0;
        this._setLoadingViewVisible(false);

        this._issueId = null;
        this._issue = null;
        this._isCreateMode = null;
        this._isFromScratch = null;
        this._isBIssue = null;
        this._issueAId = null;
        this._issueAName = null;
        this._campaignId = null;
        this._origin = null;

        this.get_nameTextField().reset();
        this.get_nameTextFieldRead().reset();
        this.get_messageSubjectTextField().reset();
        this.get_fromNameTextField().reset();
        this.get_replyToEmailTextField().reset();

        this.set_selectedMailingList(null);
        jQuery(this.get_selectListsButton()).find("span").html(this.get_clientLabelManager().getLabel("NewslettersResources", "SelectMailingList"));
        jQuery(this.get_mailingListErrorMessage()).hide();
    },

    show: function (viewParams) {
        this.reset();

        this._issueId = viewParams.issueId;
        this._issue = viewParams.issue;
        this._isCreateMode = viewParams.isCreateMode;
        this._isFromScratch = viewParams.isFromScratch;
        this._isBIssue = viewParams.isBIssue;
        this._issueAId = viewParams.issueAId;
        this._issueAName = viewParams.issueAName;
        this._campaignId = viewParams.campaignId;
        this._origin = viewParams.origin;

        var that = this;
        var continueShowing = function () {
            that._updateUi();
            jQuery(that.get_issuePropertiesWrapper()).show();
        }

        if (this._issue) {
            continueShowing();
        }
        else {
            if (this._isCreateMode) {
                if (this._isFromScratch) {
                    if (this._campaignId) {
                        var getCampaignSuccess = function (data, textStatus, jqXHR) {
                            that._copyFromViewModel(data);
                            that._issue.Name = that._issueAName;
                            that._issue.RootCampaignId = data.Id;
                            that._issue.RootCampaignName = data.Name;
                            continueShowing();
                        }
                        this._setLoadingViewVisible(true);
                        this._manager.getCampaign(this._campaignId, getCampaignSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
                    }
                    else {
                        throw "When creating A/B test from scratch you need to specify campaignId in the viewParams.";
                    }
                }
                else {
                    if (this._issueAId) {
                        var getIssueSuccess = function (data, textStatus, jqXHR) {
                            that._copyFromViewModel(data);
                            continueShowing();
                        }
                        this._setLoadingViewVisible(true);
                        this._manager.getIssue(this._issueAId, getIssueSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
                    }
                    else {
                        throw "When creating A/B test from issue you need to specify issueAId in the viewParams.";
                    }
                }
            }
            else { //edit mode
                if (this._issueId) {
                    var getIssueSuccess = function (data, textStatus, jqXHR) {
                        that._issue = data;
                        continueShowing();
                    }
                    this._setLoadingViewVisible(true);
                    this._manager.getIssue(this._issueId, getIssueSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
                }
                else {
                    throw "You need to pass issueId in the viewParams when isCreateMode is not true.";
                }
            }
        }
    },

    hide: function (result, data) {
        var args = this._raise_hide(result, data);

        if (args.get_cancel() == false) {
            jQuery(this.get_issuePropertiesWrapper()).hide();
        }
    },

    add_hide: function (delegate) {
        this.get_events().addHandler('hide', delegate);
    },

    remove_hide: function (delegate) {
        this.get_events().removeHandler('hide', delegate);
    },

    _raise_hide: function (result, data) {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('hide');
            var args = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs(result, data);
            if (h) h(this, args);
            return args;
        }
    },

    _setLoadingViewVisible: function (loading) {
        if (loading) {
            this._loadingCounter++;
        }
        else {
            if (this._loadingCounter > 0) {
                this._loadingCounter--;
            }
        }
        if (this._loadingCounter > 0) {
            jQuery(this.get_buttonsPanel()).hide();
            jQuery(this.get_loadingView()).show();
        }
        else {
            jQuery(this.get_loadingView()).hide();
            jQuery(this.get_buttonsPanel()).show();
        }
    },

    _updateUi: function () {
        if (this._isCreateMode) {
            jQuery(this.get_backLink()).html(String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "BackToFormat"),
                    '"' + this.get_clientLabelManager().getLabel("NewslettersResources", "CreateABCampaign") + '"'));

            jQuery(this.get_dialogTitle()).html(String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "CreateIssueFor"),
                this._issue.RootCampaignName));

            switch (this._origin) {
                case "overview":
                    jQuery(this.get_createAndCloseLabel()).html(String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "CreateAndGoToFormat"),
                        this._issue.RootCampaignName));
                    break;
                case "campaigns":
                    jQuery(this.get_createAndCloseLabel()).html(this.get_clientLabelManager().getLabel("NewslettersResources", "CreateAndGoEmailCampaigns"));
                    break;
            }

            jQuery(this.get_createAndCloseButton()).show();
            jQuery(this.get_goToContentButton()).show();
            jQuery(this.get_saveButton()).hide();
        }
        else {
            jQuery(this.get_backLink()).html(this.get_clientLabelManager().getLabel("NewslettersResources", "BackToContent"));
            jQuery(this.get_dialogTitle()).html(this.get_clientLabelManager().getLabel("Labels", "Edit") + " " + this._issue.Name);

            jQuery(this.get_createAndCloseButton()).hide();
            jQuery(this.get_goToContentButton()).hide();
            jQuery(this.get_saveButton()).show();
        }

        if (this._isBIssue) {
            jQuery(this.get_selectListsButton()).hide();
            jQuery(this.get_nameTextField().get_element()).hide();
            jQuery(this.get_nameTextFieldRead().get_element()).show();
            this.get_nameTextFieldRead().set_value(this._issue.Name);
        }
        else {
            jQuery(this.get_selectListsButton()).show();
            jQuery(this.get_nameTextField().get_element()).show();
            jQuery(this.get_nameTextFieldRead().get_element()).hide();
            this.get_nameTextField().set_value(this._issue.Name);
        }

        this.get_messageSubjectTextField().set_value(this._issue.MessageSubject);
        this.get_fromNameTextField().set_value(this._issue.FromName);
        this.get_replyToEmailTextField().set_value(this._issue.ReplyToEmail);
        this._setSelectedMailListLabel(this._issue.ListTitle);
    },

    _backLinkClick: function (sender, args) {
        this.hide(this.HIDE_RESULT.BACK);
    },

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    _copyFromViewModel: function (viewModel) {
        this._issue = {};

        for (var v in viewModel) {
            this._issue[v] = viewModel[v];
        }

        this._issue.Id = this._manager.EMPTY_GUID;
        this._issue.MessageBody = null;
    },

    _createAndCloseButtonClickHandler: function (sender, args) {
        if (!this._isCreateMode) {
            throw "This button is supposed to appear only for create mode.";
        }

        if (this._isValid()) {
            var onSuccess = function (data, textStatus, jqXHR) {
                dialogBase.closeCreated();
            };

            this._updateCurrentIssue();

            this._setLoadingViewVisible(true);
            this._manager.createAbTest(this._issueAId, this._isFromScratch, this._issue, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
    },

    _saveButtonClickHandler: function (sender, args) {
        if (this._isCreateMode) {
            throw "This button is not supposed to appear for create mode.";
        }

        if (this._isValid()) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                that.hide(that.HIDE_RESULT.SAVE, data);
            };

            this._updateCurrentIssue();

            this._setLoadingViewVisible(true);
            this._manager.saveIssue(this._issue, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
    },

    _goToContentClickHandler: function (sender, args) {
        if (!this._isCreateMode) {
            throw "This button is supposed to appear only for create mode.";
        }

        if (this._isValid()) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                that.hide(that.HIDE_RESULT.GO_TO_MESSAGE, data);
            };

            this._updateCurrentIssue();

            this._setLoadingViewVisible(true);
            this._manager.createAbTest(this._issueAId, this._isFromScratch, this._issue, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
    },

    _selectListsButtonClick: function (sender, args) {
        if (this._issue) {
            this.get_mailingListSelector().set_selectedMailingListId(this._issue.ListId);
        }
        this.get_mailingListSelector().show();
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

    _fillMailListDefaults: function () {
        if (this.get_selectedMailingList()) {
            if (!this.get_fromNameTextField().get_value()) {
                this.get_fromNameTextField().set_value(this.get_selectedMailingList().DefaultFromName);
            }
            if (!this.get_replyToEmailTextField().get_value()) {
                this.get_replyToEmailTextField().set_value(this.get_selectedMailingList().DefaultReplyToEmail);
            }
            if (!this.get_messageSubjectTextField().get_value()) {
                this.get_messageSubjectTextField().set_value(this.get_selectedMailingList().DefaultSubject);
            }
        }
    },

    _isValid: function () {
        var stepIsValid = true;

        if (!this._isBIssue && this.get_nameTextField().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_messageSubjectTextField().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_fromNameTextField().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_replyToEmailTextField().validate() == false) {
            stepIsValid = false;
        }

        if (!this._isBIssue && ((!this._issue || !this._issue.ListId || this._issue.ListId == this._manager.EMPTY_GUID) &&
                this.get_mailingListSelector().validate() == false)) {
            jQuery(this.get_mailingListErrorMessage()).show();
            stepIsValid = false;
        }
        else {
            jQuery(this.get_mailingListErrorMessage()).hide();
        }

        return stepIsValid;
    },

    _mailingListSelectorCloseHandler: function (sender, args) {
        var selectedMailingList = args.get_data();
        if (selectedMailingList) {
            this.set_selectedMailingList(selectedMailingList);
        }
    },

    _updateCurrentIssue: function () {
        this._issue.Name = this._isBIssue ? this.get_nameTextFieldRead().get_value() : this.get_nameTextField().get_value();

        if (this.get_selectedMailingList()) {
            this._issue.ListId = this.get_selectedMailingList().Id;
        }
        this._issue.MessageSubject = this.get_messageSubjectTextField().get_value();
        this._issue.FromName = this.get_fromNameTextField().get_value();
        this._issue.ReplyToEmail = this.get_replyToEmailTextField().get_value();
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },
    get_issuePropertiesWrapper: function () {
        return this._issuePropertiesWrapper;
    },
    set_issuePropertiesWrapper: function (value) {
        this._issuePropertiesWrapper = value;
    },
    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },
    get_dialogTitle: function () {
        return this._dialogTitle;
    },
    set_dialogTitle: function (value) {
        this._dialogTitle = value;
    },
    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },
    get_buttonsPanel: function () {
        return this._buttonsPanel;
    },
    set_buttonsPanel: function (value) {
        this._buttonsPanel = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_createAndCloseLabel: function () {
        return this._createAndCloseLabel;
    },
    set_createAndCloseLabel: function (value) {
        this._createAndCloseLabel = value;
    },
    get_createAndCloseButton: function () {
        return this._createAndCloseButton;
    },
    set_createAndCloseButton: function (value) {
        this._createAndCloseButton = value;
    },
    get_goToContentButton: function () {
        return this._goToContentButton;
    },
    set_goToContentButton: function (value) {
        this._goToContentButton = value;
    },
    get_nameTextField: function () {
        return this._nameTextField;
    },
    set_nameTextField: function (value) {
        this._nameTextField = value;
    },
    get_nameTextFieldRead: function () {
        return this._nameTextFieldRead;
    },
    set_nameTextFieldRead: function (value) {
        this._nameTextFieldRead = value;
    },
    get_messageSubjectTextField: function () {
        return this._messageSubjectTextField;
    },
    set_messageSubjectTextField: function (value) {
        this._messageSubjectTextField = value;
    },
    get_fromNameTextField: function () {
        return this._fromNameTextField;
    },
    set_fromNameTextField: function (value) {
        this._fromNameTextField = value;
    },
    get_replyToEmailTextField: function () {
        return this._replyToEmailTextField;
    },
    set_replyToEmailTextField: function (value) {
        this._replyToEmailTextField = value;
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
    get_mailingListErrorMessage: function () {
        return this._mailingListErrorMessage;
    },
    set_mailingListErrorMessage: function (value) {
        this._mailingListErrorMessage = value;
    },
    get_mailingListSelector: function () {
        return this._mailingListSelector;
    },
    set_mailingListSelector: function (value) {
        this._mailingListSelector = value;
    },
    get_saveButton: function () {
        return this._saveButton;
    },
    set_saveButton: function (value) {
        this._saveButton = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestIssuePropertiesView', Sys.UI.Control);
