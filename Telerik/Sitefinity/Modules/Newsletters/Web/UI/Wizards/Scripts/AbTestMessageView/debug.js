Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView.initializeBase(this, [element]);

    this._messageWrapper = null;
    this._clientLabelManager = null;

    this._backLink = null;
    this._cancelLink = null;
    this._backDelegate = null;

    this._loadingView = null;
    this._loadingCounter = 0;
    this._dialogTitle = null;
    this._messageControl = null;

    this._previewWindow = null;
    this._previewWindowCloseDelegate = null;
    this._sendTestPrompt = null;
    this._sendTestCallbackDelegate = null;

    this._htmlTextPanel = null;
    this._htmlTextControl = null;
    this._htmlMergeTagSelector = null;
    this._automaticPlainTextRadio = null;
    this._manualPlainTextRadio = null;
    this._plainTextVersionHtmlPanel = null;
    this._plainTextVersionHtml = null;

    this._plainTextPanel = null;
    this._plainTextControl = null;
    this._mergeTagSelector = null;

    this._buttonsPanel = null;
    this._actionsMenu = null;
    this._commandMenuDelegate = null;
    this._sendButton = null;
    this._sendButtonClickDelegate = null;
    this._saveDraftButton = null;
    this._saveDraftDelegate = null;
    this._previewButton = null;
    this._previewButtonClickDelegate = null;
    this._mergeTagSelectedDelegate = null;
    this._htmlMergeTagSelectedDelegate = null;

    this._rootUrl = null;
    this._providerName = null;
    this._manager = null;

    this._issue = null;
    this._origin = null;

    this._ajaxCompleteDelegate = null;
    this._ajaxFailDelegate = null;

    this._messageViewChangedDelegate = null;

    this.HIDE_RESULT = {
        BACK: "back",
        SAVE_DRAFT: "save-draft",
        SEND: "send",
        TITLE_AND_PROPERTIES: "title-and-properties"
    };
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);

        this._backDelegate = Function.createDelegate(this, this._backHandler);
        $addHandler(this.get_backLink(), "click", this._backDelegate);
        $addHandler(this.get_cancelLink(), "click", this._backDelegate);

        this._messageViewChangedDelegate = Function.createDelegate(this, this._messageViewChangedHandler);
        $addHandler(this.get_automaticPlainTextRadio(), "click", this._messageViewChangedDelegate);
        $addHandler(this.get_manualPlainTextRadio(), "click", this._messageViewChangedDelegate);

        this._commandMenuDelegate = Function.createDelegate(this, this._commandMenuHandler);
        this.get_actionsMenu().add_itemClicked(this._commandMenuDelegate);

        this._sendTestCallbackDelegate = Function.createDelegate(this, this._sendTestCallback);

        this._previewButtonClickDelegate = Function.createDelegate(this, this._previewButtonClickHandler);
        $addHandler(this.get_previewButton(), 'click', this._previewButtonClickDelegate);

        this._previewWindowCloseDelegate = Function.createDelegate(this, this._previewWindowCloseHandler);
        this.get_previewWindow().add_close(this._previewWindowCloseDelegate);

        this._saveDraftDelegate = Function.createDelegate(this, this._saveDraftHandler);
        $addHandler(this.get_saveDraftButton(), 'click', this._saveDraftDelegate);

        this._sendButtonClickDelegate = Function.createDelegate(this, this._sendButtonClickHandler);
        $addHandler(this.get_sendButton(), "click", this._sendButtonClickDelegate);

        this._mergeTagSelectedDelegate = Function.createDelegate(this, this._mergeTagSelectedHandler);
        this.get_mergeTagSelector().add_tagSelected(this._mergeTagSelectedDelegate);
        this._htmlMergeTagSelectedDelegate = Function.createDelegate(this, this._htmlMergeTagSelectedHandler);
        this.get_htmlMergeTagSelector().add_tagSelected(this._htmlMergeTagSelectedDelegate);

    },

    dispose: function () {
        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }
        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._backDelegate) {
            if (this.get_backLink()) {
                $removeHandler(this.get_backLink(), "click", this._backDelegate);
            }
            if (this.get_cancelLink()) {
                $removeHandler(this.get_cancelLink(), "click", this._backDelegate);
            }
            delete this._backDelegate;
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

        if (this._commandMenuDelegate) {
            if (this.get_actionsMenu()) {
                this.get_actionsMenu().remove_itemClicked(this._commandMenuDelegate);
            }
            delete this._commandMenuDelegate;
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
            if (this.get_previewWindow()) {
                this.get_previewWindow().remove_close(this._previewWindowCloseDelegate);
            }
            delete this._previewWindowCloseDelegate;
        }

        if (this._saveDraftDelegate) {
            if (this.get_saveDraftButton()) {
                $removeHandler(this.get_saveDraftButton(), 'click', this._saveDraftDelegate);
            }
            delete this._saveDraftDelegate;
        }

        if (this._sendButtonClickDelegate) {
            if (this.get_sendButton()) {
                $removeHandler(this.get_sendButton(), "click", this._sendButtonClickDelegate);
            }
            delete this._sendButtonClickDelegate;
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

        this._manager.dispose();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView.callBaseMethod(this, "dispose");
    },

    reset: function () {
        this._issue = null;
        this._origin = null;

        this._loadingCounter = 0;
        this._setLoadingViewVisible(false);

        this.get_htmlTextControl().reset();
        this.get_automaticPlainTextRadio().checked = true;
        this.get_plainTextVersionHtml().value = "";

        this.get_plainTextControl().value = "";

        jQuery(this.get_htmlTextPanel()).hide();
        jQuery(this.get_plainTextPanel()).hide();
    },

    show: function (viewParams) {
        this.reset();

        this._issue = viewParams.issue;
        this._issueId = viewParams.issueId;
        this._origin = viewParams.origin;

        var that = this;
        var continueShowing = function () {
            that._updateUi();
            jQuery(that.get_messageWrapper()).show();
            that._makeFormWider(true);
            //fixes an issue with #259996 in TWU 2014 Q1 SP1 
            that.get_htmlTextControl().get_editControl().repaint();
        };

        if (this._issue) {
            continueShowing();
        }
        else {
            if (this._issueId) {
                var getIssueSuccess = function (data, textStatus, jqXHR) {
                    that._issue = data;
                    continueShowing();
                }
                this._setLoadingViewVisible(true);
                this._manager.getIssue(this._issueId, getIssueSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
            }
            else {
                throw "You need to pass issueId in the viewParams when issue is not passed.";
            }
        }
    },

    hide: function (result, data) {
        var args = this._raise_hide(result, data);

        if (args.get_cancel() == false) {
            this._makeFormWider(false);
            jQuery(this.get_messageWrapper()).hide();
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

    _updateUi: function () {
        if (!this._issue) {
            throw "Get the issue first.";
        }

        var backText;
        switch (this._origin) {
            case "campaigns":
                backText = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToCampaigns");
                break;
            case "overview":
                backText = String.format(this.get_clientLabelManager().getLabel("NewslettersResources", "BackToFormat"), this._issue.RootCampaignName);
                break;
            case "content":
                backText = this.get_clientLabelManager().getLabel("NewslettersResources", "BackToContent");
                break;
            case "send":
                backText = this.get_clientLabelManager().getLabel("Labels", "Back");
                break;
        }
        jQuery(this.get_backLink()).html(backText);
        jQuery(this.get_cancelLink()).html(backText);

        jQuery(this.get_dialogTitle()).html(this.get_clientLabelManager().getLabel("Labels", "Edit") + " " + this._issue.Name);

        switch (this._issue.MessageBody.MessageBodyType) {
            case this._manager.MESSAGE_BODY_TYPE.HTML:
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
            case this._manager.MESSAGE_BODY_TYPE.PLAINTEXT:
                this.get_plainTextControl().value = this._issue.MessageBody.BodyText;
                this.get_mergeTagSelector().setMergeTags(this._issue.MergeTags);
                jQuery(this.get_plainTextPanel()).show();
                break;
            case this._manager.MESSAGE_BODY_TYPE.STANDARD:
                jQuery(window.top.document.body).addClass("sfLoadingTransition");

                var search = window.top.location.search;
                var queryStringParser = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(search.substring(1, search.length));
                var returnUrl = queryStringParser.get("ReturnUrl");
                if (!returnUrl) {
                    returnUrl = window.top.location.href;
                }

                window.top.location = this._manager.getZoneEditorUrl(this._issue.MessageBody.Id, this._issue.Id, returnUrl);
                break;
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

    _backHandler: function (sender, args) {
        this.hide(this.HIDE_RESULT.BACK, this._issue);
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

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    _messageViewChangedHandler: function (sender, args) {
        this._updateCurrentIssue();
        this._updateUi();
    },

    _updateCurrentIssue: function () {
        this._issue.MessageBody.BodyText = this.get_bodyText();
        if (this._issue.MessageBody.MessageBodyType == this._manager.MESSAGE_BODY_TYPE.HTML) {
            if (this.get_manualPlainTextRadio().checked) {
                this._issue.MessageBody.PlainTextVersion = this.get_plainTextVersionHtml().value;
            }
            else {
                this._issue.MessageBody.PlainTextVersion = null;
            }
        }
    },

    _commandMenuHandler: function (sender, args) {
        var commandName = args.get_item().get_value();
        if (!commandName) {
            return;
        }

        //fix problem with not closing menu
        sender.close();

        switch (commandName) {
            case 'sendTest':
                this.get_sendTestPrompt().set_inputText("");
                this.get_sendTestPrompt().show_prompt(null, null, this._sendTestCallbackDelegate);
                break;
            case 'editProperties':
                this._updateCurrentIssue();
                this.hide(this.HIDE_RESULT.TITLE_AND_PROPERTIES, this._issue);
                break;
            default:
                alert('Command "' + commandName + '" is not supported.');
        }
    },

    _sendTestCallback: function (sender, args) {
        if (args.get_commandName() == "sendTest") {
            this._sendTestMessage(args.get_commandArgument());
        }
    },

    _sendTestMessage: function (emailString) {
        var emails = this._parseEmailString(emailString);
        if (this._validateEmailAddresses(emails)) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                that.get_messageControl().showPositiveMessage(that.get_clientLabelManager().getLabel("NewslettersResources", "TestMessageSentSuccessfully"));
            }

            this._updateCurrentIssue();
            this._setLoadingViewVisible(true);
            this._manager.sendTestMessage(this._issue, emails, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
    },

    _parseEmailString: function (emailString) {
        return emailString.split(',');
    },

    _validateEmailAddresses: function (emails) {
        if (!emails.length > 0) {
            this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('NewslettersResources', 'EnterAtLeastOneTestEmailAddress'));
            return false;
        }

        for (var i = 0; i < emails.length; i++) {
            if (!this._manager.EMAIL_ADDRESS_REGEX.test(emails[i])) {
                this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('ErrorMessages', 'EmailAddressViolationMessage'));
                return false;
            }
        }
        return true;
    },

    _previewButtonClickHandler: function (sender, args) {
        this._updateCurrentIssue();
        this._getRawMessageBodyAndOpenPreview();
    },

    _getRawMessageBodyAndOpenPreview: function () {
        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._issue.MessageBody = data;
            that.get_previewWindow().show(that._issue);
            jQuery("body").removeClass("sfWidgetTmpDialog");
        };

        this._setLoadingViewVisible(true);
        this._manager.getRawMessage(this._issue.MessageBody, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
    },

    _previewWindowCloseHandler: function (sender, args) {
        jQuery("body").addClass("sfWidgetTmpDialog");
    },

    _saveDraftHandler: function (sender, args) {
        this._updateCurrentIssue();

        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._issue = data;
            that.hide(that.HIDE_RESULT.SAVE_DRAFT, data);
        };

        this._setLoadingViewVisible(true);
        this._manager.saveIssue(this._issue, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
    },

    _sendButtonClickHandler: function (sender, args) {
        this._updateCurrentIssue();

        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._issue = data;
            that.hide(that.HIDE_RESULT.SEND, data);
        };

        this._setLoadingViewVisible(true);
        this._manager.saveIssue(this._issue, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
    },

    _mergeTagSelectedHandler: function (sender, args) {
        jQuery(this.get_plainTextControl()).insertAtCaret(args.MergeTag);
    },

    _htmlMergeTagSelectedHandler: function (sender, args) {
        this.get_htmlTextControl()._editControl.pasteHtml(args.MergeTag);
    },

    get_bodyText: function () {
        if (this._issue.MessageBody.MessageBodyType == this._manager.MESSAGE_BODY_TYPE.PLAINTEXT) {
            return this.get_plainTextControl().value;
        } else if (this._issue.MessageBody.MessageBodyType == this._manager.MESSAGE_BODY_TYPE.HTML) {
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
        if (this._issue.MessageBody.MessageBodyType == this._manager.MESSAGE_BODY_TYPE.PLAINTEXT) {
            this.get_plainTextControl().value = value;
        } else if (this._issue.MessageBody.MessageBodyType == this._manager.MESSAGE_BODY_TYPE.HTML) {
            this.get_htmlTextControl().set_value(value);
        } else {
            alert('This campaign type is not supported!');
        }
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
    get_messageWrapper: function () {
        return this._messageWrapper;
    },
    set_messageWrapper: function (value) {
        this._messageWrapper = value;
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
    get_buttonsPanel: function () {
        return this._buttonsPanel;
    },
    set_buttonsPanel: function (value) {
        this._buttonsPanel = value;
    },
    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
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
    get_plainTextVersionHtml: function () {
        return this._plainTextVersionHtml;
    },
    set_plainTextVersionHtml: function (value) {
        this._plainTextVersionHtml = value;
    },
    get_previewWindow: function () {
        return this._previewWindow;
    },
    set_previewWindow: function (value) {
        this._previewWindow = value;
    },
    get_sendTestPrompt: function () {
        return this._sendTestPrompt;
    },
    set_sendTestPrompt: function (value) {
        this._sendTestPrompt = value;
    },
    get_actionsMenu: function () {
        return this._actionsMenu;
    },
    set_actionsMenu: function (value) {
        this._actionsMenu = value;
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
    get_previewButton: function () {
        return this._previewButton;
    },
    set_previewButton: function (value) {
        this._previewButton = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestMessageView', Sys.UI.Control);
