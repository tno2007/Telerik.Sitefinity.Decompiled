Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView.initializeBase(this, [element]);

    this._campaignDetailView = null;
    this._clientLabelManager = null;
    this._wrapper = null;

    this._newslettersHandlerUrl = null;

    this._scheduleDeliveryWindow = null;
    this._campaignPreviewWindow = null;

    this._issue = null;

    this._sendTestPrompt = null;
    this._sendIssuePrompt = null;

    this._htmlTextPanel = null;
    this._plainTextPanel = null;
    this._htmlTextControl = null;
    this._plainTextControl = null;
    this._mergeTagSelector = null;
    this._htmlMergeTagSelector = null;
    this._automaticPlainTextRadio = null;
    this._manualPlainTextRadio = null;
    this._plainTextVersionHtmlPanel = null;
    this._plainTextVersionHtml = null;

    this._messageViewButtons = null;
    this._sendButton = null;
    this._saveDraftButton = null;
    this._actionsMenu = null;
    this._previewButton = null;
    this._messageCancelLink = null;

    this._commandMenuDelegate = null;
    this._scheduleDeliveryCloseDelegate = null;
    this._messageViewChangedDelegate = null;
    this._mergeTagSelectedDelegate = null;
    this._htmlMergeTagSelectedDelegate = null;
    this._sendDelegate = null;
    this._saveDraftDelegate = null;
    this._closeSuccessDelegate = null;
    this._sendTestCallbackDelegate = null;
    this._previewButtonClickDelegate = null;
    this._previewWindowCloseDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView.callBaseMethod(this, "initialize");

        this._commandMenuDelegate = Function.createDelegate(this, this._commandMenuHandler);
        this.get_actionsMenu().add_itemClicked(this._commandMenuDelegate);

        this._scheduleDeliveryCloseDelegate = Function.createDelegate(this, this._scheduleDeliveryCloseHandler);
        this.get_scheduleDeliveryWindow().add_close(this._scheduleDeliveryCloseDelegate);

        this._messageViewChangedDelegate = Function.createDelegate(this, this._messageViewChangedHandler);
        $addHandler(this.get_automaticPlainTextRadio(), "click", this._messageViewChangedDelegate);
        $addHandler(this.get_manualPlainTextRadio(), "click", this._messageViewChangedDelegate);

        this._mergeTagSelectedDelegate = Function.createDelegate(this, this._mergeTagSelectedHandler);
        this.get_mergeTagSelector().add_tagSelected(this._mergeTagSelectedDelegate);
        this._htmlMergeTagSelectedDelegate = Function.createDelegate(this, this._htmlMergeTagSelectedHandler);
        this.get_htmlMergeTagSelector().add_tagSelected(this._htmlMergeTagSelectedDelegate);

        this._sendDelegate = Function.createDelegate(this, this._sendHandler);
        $addHandler(this.get_sendButton(), 'click', this._sendDelegate);

        this._saveDraftDelegate = Function.createDelegate(this, this._saveDraftHandler);
        $addHandler(this.get_saveDraftButton(), 'click', this._saveDraftDelegate);

        this._closeSuccessDelegate = Function.createDelegate(this, this._closeSuccessHandler);
        this._sendTestCallbackDelegate = Function.createDelegate(this, this._sendTestCallback);

        this._previewButtonClickDelegate = Function.createDelegate(this, this._previewButtonClickHandler);
        $addHandler(this.get_previewButton(), 'click', this._previewButtonClickDelegate);

        this._previewWindowCloseDelegate = Function.createDelegate(this, this._previewWindowCloseHandler);
        this.get_campaignPreviewWindow().add_close(this._previewWindowCloseDelegate);
    },

    dispose: function () {
        this._unhookCampaignDetailDelegates();

        if (this._commandMenuDelegate) {
            if (this.get_actionsMenu()) {
                this.get_actionsMenu().remove_itemClicked(this._commandMenuDelegate);
            }
            delete this._commandMenuDelegate;
        }

        if (this._scheduleDeliveryCloseDelegate) {
            if (this.get_scheduleDeliveryWindow()) {
                this.get_scheduleDeliveryWindow().remove_close(this._scheduleDeliveryCloseDelegate);
            }
            delete this._scheduleDeliveryCloseDelegate;
        }

        if (this._mergeTagSelectedDelegate) {
            if (this.get_mergeTagSelector()) {
                this.get_mergeTagSelector().remove_tagSelected(this._mergeTagSelectedDelegate);
            }
            delete this._mergeTagSelectedDelegate;
        }

        if (this._htmlMergeTagSelectedDelegate) {
            if (this.get_htmlMergeTagSelector()) {
                this.get_htmlMergeTagSelector().remove_tagSelected(this._htmlMergeTagSelectedDelegate);
            }
            delete this._htmlMergeTagSelectedDelegate;
        }

        if (this._sendDelegate) {
            if (this.get_sendButton()) {
                $removeHandler(this.get_sendButton(), 'click', this._sendDelegate);
            }
            delete this._sendDelegate;
        }

        if (this._saveDraftDelegate) {
            if (this.get_saveDraftButton()) {
                $removeHandler(this.get_saveDraftButton(), 'click', this._saveDraftDelegate);
            }
            delete this._saveDraftDelegate;
        }

        if (this._closeSuccessDelegate) {
            delete this._closeSuccessDelegate;
        }

        if (this._sendTestCallbackDelegate) {
            delete this._sendTestCallbackDelegate;
        }

        if (this._previewButtonClickDelegate) {
            if (this.get_previewButton()) {
                $removeHandler(this.get_previewButton(), 'click', this._previewButtonClickDelegate);
            }
            delete this._previewButtonClickDelegate;
        }

        if (this._previewWindowCloseDelegate) {
            if (this.get_campaignPreviewWindow()) {
                this.get_campaignPreviewWindow().remove_close(this._previewWindowCloseDelegate);
            }
            delete this._previewWindowCloseDelegate;
        }

        if (this._messageViewChangedDelegate) {
            if (this.get_automaticPlainTextRadio()) {
                $removeHandler(this.get_automaticPlainTextRadio(), "click", this._messageViewChangedDelegate);
            }
            if (this.get_manualPlainTextRadio()) {
                $removeHandler(this.get_manualPlainTextRadio(), "click", this._messageViewChangedDelegate);
            }
            delete this._messageViewChangedDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    show: function () {
        if (!this.get_campaignDetailView()) {
            throw "You need to set the parent dialog reference first: set_campaignDetailView(value).";
        }

        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._issue = data;
            that._updateUi();
            jQuery(that.get_wrapper()).show();
            that._makeFormWider(true);
            //fixes an issue with #259996 in TWU 2014 Q1 SP1 
            that.get_htmlTextControl().get_editControl().repaint();
        };

        if (!this._issue) {
            this.get_campaignDetailView().getIssue(this.get_campaignDetailView().get_campaignId(), onSuccess);
        }
        else {
            onSuccess(this._issue);
        }
    },

    hide: function () {
        this._makeFormWider(false);
        jQuery(this.get_wrapper()).hide();
    },

    reset: function () {
        this._issue = null;

        this.get_htmlTextControl().reset();
        this.get_plainTextControl().value = "";
        this.get_plainTextVersionHtml().value = "";
        this.get_automaticPlainTextRadio().checked = true;
    },

    updateCurrentIssue: function () {
        this._issue.MessageBody.BodyText = this.get_bodyText();
        if (this._issue.MessageBody.MessageBodyType == this.get_campaignDetailView().MESSAGE_BODY_TYPE.HTML) {
            if (this.get_manualPlainTextRadio().checked) {
                this._issue.MessageBody.PlainTextVersion = this.get_plainTextVersionHtml().value;
            }
            else {
                this._issue.MessageBody.PlainTextVersion = null;
            }
        }
    },

    showButtons: function () {
        jQuery(this.get_messageViewButtons()).show();
    },

    hideButtons: function () {
        jQuery(this.get_messageViewButtons()).hide();
    },

    /* *************************** private methods *************************** */

    _messageViewChangedHandler: function (sender, args) {
        this.updateCurrentIssue();
        this._updateUi();
    },

    _updateUi: function () {
        this._hideAllEditors();

        var backLinkLabel;
        switch (this.get_campaignDetailView().get_origin()) {
            case 'campaigns':
                backLinkLabel = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToCampaigns");
                break;
            case 'content':
                backLinkLabel = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToContent");
                break;
            case 'overview':
                var originCampaignName = (this.get_campaignDetailView().get_originCampaignName().length < 29) ? 
                    this.get_campaignDetailView().get_originCampaignName() : this.get_campaignDetailView().get_originCampaignName().substring(0, 26).concat("...");
                backLinkLabel = String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "BackToFormat"), originCampaignName);
                break;
        }
        this.get_campaignDetailView().get_backLinkLabel().innerHTML = backLinkLabel;
        this.get_messageCancelLink().innerHTML = backLinkLabel;

        if (this._issue) {
            var issueName = (this._issue.Name.length < 29) ? this._issue.Name : this._issue.Name.substring(0, 26).concat("...");
            this.get_campaignDetailView().get_dialogTitleLabel().innerHTML = this.get_clientLabelManager().getLabel("Labels", "Edit") + " " + issueName;
        }

        if (this._issue && this._issue.MessageBody) {
            switch (this._issue.MessageBody.MessageBodyType) {
                case this.get_campaignDetailView().MESSAGE_BODY_TYPE.HTML:
                    this.get_htmlTextControl().set_value(this._issue.MessageBody.BodyText);
                    this.get_htmlMergeTagSelector().setMergeTags(this._issue.MergeTags);
                    if (this._issue.MessageBody.PlainTextVersion != null) {
                        this.get_manualPlainTextRadio().checked = true;
                        this.get_plainTextVersionHtml().value = this._issue.MessageBody.PlainTextVersion;
                        jQuery(this.get_plainTextVersionHtmlPanel()).show();
                    }
                    else {
                        this.get_automaticPlainTextRadio().checked = true;
                        jQuery(this.get_plainTextVersionHtmlPanel()).hide();
                    }
                    jQuery(this.get_htmlTextPanel()).show();
                    break;
                case this.get_campaignDetailView().MESSAGE_BODY_TYPE.PLAINTEXT:
                    this.get_plainTextControl().value = this._issue.MessageBody.BodyText;
                    this.get_mergeTagSelector().setMergeTags(this._issue.MergeTags);
                    jQuery(this.get_plainTextPanel()).show();
                    break;
                case this.get_campaignDetailView().MESSAGE_BODY_TYPE.STANDARD:
                    jQuery(window.top.document.body).addClass("sfLoadingTransition");
                    this.get_campaignDetailView().setLoadingViewVisible(true);

                    window.top.location = this.get_newslettersHandlerUrl() + this._issue.MessageBody.Id +
                        "?ReturnUrl=" + escape(window.top.location.pathname + window.top.location.search) +
                        "&CampaignId=" + this._issue.Id;
                    break;
            }
        }
    },

    _hideAllEditors: function () {
        jQuery(this.get_htmlTextPanel()).hide();
        jQuery(this.get_plainTextPanel()).hide();
    },

    _makeFormWider: function (flag) {
        if (flag) {
            jQuery("body").addClass("sfWidgetTmpDialog");
            jQuery(".sfNewItemForm").removeClass("sfNewItemForm").addClass("sfNewContentForm");

        } else {
            jQuery("body").removeClass("sfWidgetTmpDialog");
            jQuery(".sfNewContentForm").removeClass("sfNewContentForm").addClass("sfNewItemForm");
        }
    },

    _unhookCampaignDetailDelegates: function () {
        if (this.get_campaignDetailView() && this.get_messageCancelLink()) {
            $removeHandler(this.get_messageCancelLink(), "click", this.get_campaignDetailView().get_cancelDelegate());
        }
    },

    _hookCampaignDetailDelegates: function () {
        if (this.get_campaignDetailView() && this.get_messageCancelLink()) {
            $addHandler(this.get_messageCancelLink(), "click", this.get_campaignDetailView().get_cancelDelegate());
        }
    },

    _mergeTagSelectedHandler: function (sender, args) {
        jQuery(this.get_plainTextControl()).insertAtCaret(args.MergeTag);
    },

    _htmlMergeTagSelectedHandler: function (sender, args) {
        this.get_htmlTextControl()._editControl.pasteHtml(args.MergeTag);
    },

    _sendHandler: function (sender, args) {
        this.get_sendIssuePrompt().set_title(this.get_clientLabelManager().getLabel('NewslettersResources', 'SendIssueFor') + ' ' + this._issue.RootCampaignName);
        this.get_sendIssuePrompt().set_message(String.format(this.get_clientLabelManager().getLabel('NewslettersResources', 'SendIssuePromptText'), this._issue.ListSubscriberCount));

        var that = this;
        var onSaveSuccess = function (data, textStatus, jqXHR) {
            that._sendIssue(that._closeSuccessDelegate);
        };
        var promptCallback = function (sender, args) {
            if (args.get_commandName() == "send") {
                that._saveIssue(onSaveSuccess);
            }
        };

        this.get_sendIssuePrompt().show_prompt(null, null, promptCallback);
    },

    _sendIssue: function (onSuccess) {
        this.get_campaignDetailView().setLoadingViewVisible(true);

        jQuery.ajax({
            type: 'PUT',
            url: this.get_campaignDetailView().get_campaignServiceUrl() + '/send/',
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(this._issue.Id),
            success: onSuccess,
            error: this.get_campaignDetailView().get_ajaxFailDelegate(),
            complete: this.get_campaignDetailView().get_ajaxCompleteDelegate()
        });
    },

    _saveDraftHandler: function (sender, args) {
        this._saveIssue(this._closeSuccessDelegate);
    },

    _saveIssue: function (onSuccess) {
        this.updateCurrentIssue();
        this.get_campaignDetailView().saveIssue(onSuccess, this._getIssueObject());
    },

    _commandMenuHandler: function (sender, args) {
        var commandName = args.get_item().get_value();
        if (!commandName) {
            return;
        }

        //fix problem with not closing menu
        sender.close();

        switch (commandName) {
            case 'scheduleDelivery':
                this._scheduleCampaign();
                break;
            case 'sendTest':
                this.get_sendTestPrompt().set_inputText("");
                this.get_sendTestPrompt().show_prompt(null, null, this._sendTestCallbackDelegate);
                break;
            case 'editProperties':
                this.get_campaignDetailView().showIssueTitleAndProperties(this._issue);
                break;
            case 'delete':
                this._deleteCampaign();
                break;
            case 'createAbTest':
                this._createAbTest();
                break;
            default:
                alert('Command "' + commandName + '" is not supported.');
        }
    },

    _createAbTest: function () {
        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            if (window.top.showAbTestDetailDialog) {
                var viewParams = {
                    origin: that.get_campaignDetailView().get_origin(),
                    campaignId: that._issue.RootCampaignId,
                    issueAId: that._issue.Id,
                    issueAName: that._issue.Name,
                    rootCampaignName: that._issue.RootCampaignName
                };
                window.top.showAbTestDetailDialog("middle", viewParams);
            }
            that._getRadWindow().close();
        }

        this.updateCurrentIssue();
        this.get_campaignDetailView().saveIssue(onSuccess, this._getIssueObject());
    },

    _scheduleCampaign: function () {
        this.get_scheduleDeliveryWindow().reset();
        this.get_scheduleDeliveryWindow().show();
    },

    _getIssueObject: function () {
        if (!this._issue) {
            return null;
        }

        var issue = {
            Id: this._issue.Id,
            Name: this._issue.Name,
            FromName: this._issue.FromName,
            ReplyToEmail: this._issue.ReplyToEmail,
            MessageSubject: this._issue.MessageSubject,
            UseGoogleTracking: this._issue.UseGoogleTracking,
            ListId: this._issue.ListId,
            MessageBody: this._issue.MessageBody,
            CampaignTemplateId: this.get_campaignDetailView().EMPTY_GUID,
            RootCampaignId: this._issue.RootCampaignId
        };
        return issue;
    },

    _scheduleDeliveryCloseHandler: function (sender, args) {
        var deliveryDate = args.get_data();
        if (deliveryDate) {
            var that = this;
            var onSuccess = function () {
                that.get_campaignDetailView().setLoadingViewVisible(true);
                jQuery.ajax({
                    type: 'PUT',
                    url: that.get_campaignDetailView().get_campaignServiceUrl() +
                        String.format("/schedule/{0}/?provider={1}", that._issue.Id, that.get_campaignDetailView().get_providerName()),
                    contentType: "application/json",
                    processData: false,
                    data: Sys.Serialization.JavaScriptSerializer.serialize(deliveryDate),
                    success: that._closeSuccessDelegate,
                    error: that.get_campaignDetailView().get_ajaxFailDelegate(),
                    complete: that.get_campaignDetailView().get_ajaxCompleteDelegate()
                });
            };

            this.updateCurrentIssue();
            this.get_campaignDetailView().saveIssue(onSuccess, this._getIssueObject());
        }
    },

    _closeSuccessHandler: function (data, textStatus, jqXHR) {
        if (this.get_campaignDetailView().get_isCreateMode()) {
            dialogBase.closeCreated();
        } else {
            dialogBase.closeUpdated();
        }
    },

    _sendTestCallback: function (sender, args) {
        if (args.get_commandName() == "sendTest") {
            this._sendTestMessage(args.get_commandArgument());
        }
    },

    _sendTestMessage: function (emailString) {
        if (this._validateEmailAddresses(emailString)) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                that.get_campaignDetailView().get_messageControl().showPositiveMessage(that.get_clientLabelManager().getLabel("NewslettersResources", "TestMessageSentSuccessfully"));
            }

            this.updateCurrentIssue();
            this.get_campaignDetailView().setLoadingViewVisible(true);
            jQuery.ajax({
                type: 'PUT',
                url: this.get_campaignDetailView().get_campaignServiceUrl() + String.format('/sendtest/?provider={0}', this.get_campaignDetailView().get_providerName()),
                contentType: "application/json",
                processData: false,
                data: Telerik.Sitefinity.JSON.stringify({ campaign: this._getIssueObject(), testEmailAddresses: this._parseEmailString(emailString) }),
                success: onSuccess,
                error: this.get_campaignDetailView().get_ajaxFailDelegate(),
                complete: this.get_campaignDetailView().get_ajaxCompleteDelegate()
            });
        }
    },

    _parseEmailString: function (emailString) {
        return emailString.split(',');
    },

    _validateEmailAddresses: function (emailString) {
        if (!emailString.length > 0) {
            this.get_campaignDetailView().get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('NewslettersResources', 'EnterAtLeastOneTestEmailAddress'));
            return false;
        }
        var emailAddresses = this._parseEmailString(emailString);

        // Consider using <see cref="Telerik.Sitefinity.Constants.EmailAddressRegexPattern"/>
        var emailAddressRegexPattern = /[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}/i;

        for (var i = 0; i < emailAddresses.length; i++) {
            if (!emailAddressRegexPattern.test(emailAddresses[i])) {
                this.get_campaignDetailView().get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('ErrorMessages', 'EmailAddressViolationMessage'));
                return false;
            }
        }
        return true;
    },

    _previewButtonClickHandler: function (sender, args) {
        this.updateCurrentIssue();
        this._getRawMessageBodyAndOpenPreview();
    },

    _getRawMessageBodyAndOpenPreview: function () {
        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._issue.MessageBody = data;
            that.get_campaignPreviewWindow().show(that._issue);
            jQuery("body").removeClass("sfWidgetTmpDialog");
        };

        this.get_campaignDetailView().setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'POST',
            url: this.get_campaignDetailView().get_campaignServiceUrl() + "/rawmessage/",
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(this._issue.MessageBody),
            success: onSuccess,
            error: this.get_campaignDetailView().get_ajaxFailDelegate(),
            complete: this.get_campaignDetailView().get_ajaxCompleteDelegate()
        });
    },

    _previewWindowCloseHandler: function () {
        jQuery("body").addClass("sfWidgetTmpDialog");
    },

    _deleteCampaign: function (campaignId) {
        var message = this.get_clientLabelManager().getLabel("NewslettersResources", "AreYouSureDeleteIssue");

        if (confirm(message)) {
            this.get_campaignDetailView().setLoadingViewVisible(true);
            jQuery.ajax({
                type: 'DELETE',
                url: this.get_campaignDetailView().get_campaignServiceUrl() + String.format('/issue/{0}/?provider={1}', this._issue.Id, this.get_campaignDetailView().get_providerName()),
                contentType: "application/json",
                processData: false,
                success: this._closeSuccessDelegate,
                error: this.get_campaignDetailView().get_ajaxFailDelegate(),
                complete: this.get_campaignDetailView().get_ajaxCompleteDelegate()
            });
        }
    },

    _getRadWindow: function () {
        var oWindow = null;
        if (window.radWindow)
            oWindow = window.radWindow;
        else if (window.frameElement.radWindow)
            oWindow = window.frameElement.radWindow;
        return oWindow;
    },

    /* *************************** properties *************************** */

    get_bodyText: function () {
        if (this._issue.MessageBody.MessageBodyType == this.get_campaignDetailView().MESSAGE_BODY_TYPE.PLAINTEXT) {
            return this.get_plainTextControl().value;
        } else if (this._issue.MessageBody.MessageBodyType == this.get_campaignDetailView().MESSAGE_BODY_TYPE.HTML) {
            var htmlVal = this.get_htmlTextControl().get_value();
            if (htmlVal != null && htmlVal != '<br>') {
                return htmlVal;
            }
            return '';
        }
        else {
            return '';
        }
    },
    set_bodyText: function (value) {
        if (this._issue.MessageBody.MessageBodyType == this.get_campaignDetailView().MESSAGE_BODY_TYPE.PLAINTEXT) {
            this.get_plainTextControl().value = value;
        } else if (this._issue.MessageBody.MessageBodyType == this.get_campaignDetailView().MESSAGE_BODY_TYPE.HTML) {
            this.get_htmlTextControl().set_value(value);
        } else {
            alert('This campaign type is not supported!');
        }
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
    get_wrapper: function () {
        return this._wrapper;
    },
    set_wrapper: function (value) {
        this._wrapper = value;
    },
    set_issue: function (value) {
        this._issue = value;
    },
    get_newslettersHandlerUrl: function () {
        return this._newslettersHandlerUrl;
    },
    set_newslettersHandlerUrl: function (value) {
        this._newslettersHandlerUrl = value;
    },
    get_scheduleDeliveryWindow: function () {
        return this._scheduleDeliveryWindow;
    },
    set_scheduleDeliveryWindow: function (value) {
        this._scheduleDeliveryWindow = value;
    },
    get_campaignPreviewWindow: function () {
        return this._campaignPreviewWindow;
    },
    set_campaignPreviewWindow: function (value) {
        this._campaignPreviewWindow = value;
    },
    get_sendTestPrompt: function () {
        return this._sendTestPrompt;
    },
    set_sendTestPrompt: function (value) {
        this._sendTestPrompt = value;
    },
    get_sendIssuePrompt: function () {
        return this._sendIssuePrompt;
    },
    set_sendIssuePrompt: function (value) {
        this._sendIssuePrompt = value;
    },
    get_htmlTextPanel: function () {
        return this._htmlTextPanel;
    },
    set_htmlTextPanel: function (value) {
        this._htmlTextPanel = value;
    },
    get_plainTextPanel: function () {
        return this._plainTextPanel;
    },
    set_plainTextPanel: function (value) {
        this._plainTextPanel = value;
    },
    get_htmlTextControl: function () {
        return this._htmlTextControl;
    },
    set_htmlTextControl: function (value) {
        this._htmlTextControl = value;
    },
    get_plainTextControl: function () {
        return this._plainTextControl;
    },
    set_plainTextControl: function (value) {
        this._plainTextControl = value;
    },
    get_mergeTagSelector: function () {
        return this._mergeTagSelector;
    },
    set_mergeTagSelector: function (value) {
        this._mergeTagSelector = value;
    },
    get_htmlMergeTagSelector: function () {
        return this._htmlMergeTagSelector;
    },
    set_htmlMergeTagSelector: function (value) {
        this._htmlMergeTagSelector = value;
    },
    get_automaticPlainTextRadio: function () {
        return this._automaticPlainTextRadio;
    },
    set_automaticPlainTextRadio: function (value) {
        this._automaticPlainTextRadio = value;
    },
    get_manualPlainTextRadio: function () {
        return this._manualPlainTextRadio;
    },
    set_manualPlainTextRadio: function (value) {
        this._manualPlainTextRadio = value;
    },
    get_plainTextVersionHtmlPanel: function () {
        return this._plainTextVersionHtmlPanel;
    },
    set_plainTextVersionHtmlPanel: function (value) {
        this._plainTextVersionHtmlPanel = value;
    },
    get_plainTextVersionHtml: function () {
        return this._plainTextVersionHtml;
    },
    set_plainTextVersionHtml: function (value) {
        this._plainTextVersionHtml = value;
    },
    get_messageViewButtons: function () {
        return this._messageViewButtons;
    },
    set_messageViewButtons: function (value) {
        this._messageViewButtons = value;
    },
    get_sendButton: function () {
        return this._sendButton;
    },
    set_sendButton: function (value) {
        this._sendButton = value;
    },
    get_saveDraftButton: function () {
        return this._saveDraftButton;
    },
    set_saveDraftButton: function (value) {
        this._saveDraftButton = value;
    },
    get_actionsMenu: function () {
        return this._actionsMenu;
    },
    set_actionsMenu: function (value) {
        this._actionsMenu = value;
    },
    get_previewButton: function () {
        return this._previewButton;
    },
    set_previewButton: function (value) {
        this._previewButton = value;
    },
    get_messageCancelLink: function () {
        return this._messageCancelLink;
    },
    set_messageCancelLink: function (value) {
        this._messageCancelLink = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.IssueMessageView', Sys.UI.Control);
