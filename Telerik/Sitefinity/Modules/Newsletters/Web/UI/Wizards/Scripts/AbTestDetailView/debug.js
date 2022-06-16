$.fn.extend({
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

var abTestDetailView;

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView.initializeBase(this, [element]);

    this._issueAId = null;
    this._issueAName = null;
    this._campaignId = null;
    this._origin = null;
    this._isBIssue = null;
    this._abTest = null;

    this._abTestMiddleStep = null;
    this._abTestMiddleStepHideDelegate = null;

    this._issuePropertiesView = null;
    this._issuePropertiesViewHideDelegate = null;
    this._issuePropertiesViewCurrentHideDelegate = null;
    this._issuePropertiesViewHideAfterMiddleStepDelegate = null;

    this._messageView = null;
    this._messageViewHideDelegate = null;
    this._messageViewCurrentHideDelegate = null;
    this._messageViewHideAfterPropertiesDelegate = null;
    this._messageViewHideAfterSendDelegate = null;

    this._sendView = null;
    this._sendViewHideDelegate = null;

    this._rootUrl = null;
    this._providerName = null;
    this._manager = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView.callBaseMethod(this, "initialize");

        abTestDetailView = this;

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._abTestMiddleStepHideDelegate = Function.createDelegate(this, this._abTestMiddleStepHideHandler);
        this.get_abTestMiddleStep().add_hide(this._abTestMiddleStepHideDelegate);

        this._issuePropertiesViewHideDelegate = Function.createDelegate(this, this._issuePropertiesViewHideHandler);
        this.get_issuePropertiesView().add_hide(this._issuePropertiesViewHideDelegate);
        this._issuePropertiesViewHideAfterMiddleStepDelegate = Function.createDelegate(this, this._issuePropertiesViewHideAfterMiddleStep);

        this._messageViewHideDelegate = Function.createDelegate(this, this._messageViewHideHandler);
        this.get_messageView().add_hide(this._messageViewHideDelegate);
        this._messageViewHideAfterPropertiesDelegate = Function.createDelegate(this, this._messageViewHideAfterProperties);
        this._messageViewHideAfterSendDelegate = Function.createDelegate(this, this._messageViewHideAfterSend);

        this._sendViewHideDelegate = Function.createDelegate(this, this._sendViewHideHandler);
        this.get_sendView().add_hide(this._sendViewHideDelegate);
    },

    dispose: function () {
        if (this._abTestMiddleStepHideDelegate) {
            if (this.get_abTestMiddleStep()) {
                this.get_abTestMiddleStep().remove_hide(this._abTestMiddleStepHideDelegate);
            }
            delete this._abTestMiddleStepHideDelegate;
        }

        if (this._issuePropertiesViewHideDelegate) {
            if (this.get_issuePropertiesView()) {
                this.get_issuePropertiesView().remove_hide(this._issuePropertiesViewHideDelegate);
            }
            delete this._issuePropertiesViewHideDelegate;
        }

        if (this._issuePropertiesViewHideAfterMiddleStepDelegate) {
            delete this._issuePropertiesViewHideAfterMiddleStepDelegate;
        }

        if (this._messageViewHideDelegate) {
            if (this.get_messageView()) {
                this.get_messageView().remove_hide(this._messageViewHideDelegate);
            }
            delete this._messageViewHideDelegate;
        }

        if (this._messageViewHideAfterPropertiesDelegate) {
            delete this._messageViewHideAfterPropertiesDelegate;
        }

        if (this._messageViewHideAfterSendDelegate) {
            delete this._messageViewHideAfterSendDelegate;
        }

        this._manager.dispose();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView.callBaseMethod(this, "dispose");
    },

    reset: function () {
        this._issueAId = null;
        this._issueAName = null;
        this._campaignId = null;
        this._origin = null;
        this._isBIssue = null;
        this._abTest = null;
        this._issuePropertiesViewCurrentHideDelegate = null;
        this._messageViewCurrentHideDelegate = null;

        this.get_abTestMiddleStep().hide();
        this.get_issuePropertiesView().hide();
        this.get_messageView().hide();
        this.get_sendView().hide();
    },

    setForm: function (view, viewParams) {
        this.reset();

        this._origin = viewParams.origin;
        this._isBIssue = viewParams.isBIssue;
        this._issueAId = viewParams.issueAId;
        this._issueAName = viewParams.issueAName;
        this._campaignId = viewParams.campaignId;

        var that = this;
        switch (view) {
            case "middle":
                this.get_abTestMiddleStep().show(viewParams);
                break;
            case "properties":
                this._issuePropertiesViewCurrentHideDelegate = function (sender, args) {
                    switch (args.get_result()) {
                        case that.get_issuePropertiesView().HIDE_RESULT.BACK:
                            dialogBase.close();
                            break;
                        case that.get_issuePropertiesView().HIDE_RESULT.SAVE:
                            dialogBase.closeUpdated();
                    }
                };
                this.get_issuePropertiesView().show(viewParams);
                break;
            case "send":
                this.get_sendView().show(viewParams);
                break;
        }
    },

    _abTestMiddleStepHideHandler: function (sender, args) {
        var hideResult = args.get_result();
        if (hideResult) {
            var isFromScratch = hideResult == this.get_abTestMiddleStep().HIDE_RESULT.START_FROM_SCRATCH;
            this._isBIssue = true;
            var viewParams = {
                isCreateMode: true,
                isFromScratch: isFromScratch,
                isBIssue: this._isBIssue,
                issueAId: this._issueAId,
                issueAName: this._issueAName,
                campaignId: this._campaignId,
                origin: this._origin
            };

            this._issuePropertiesViewCurrentHideDelegate = this._issuePropertiesViewHideAfterMiddleStepDelegate;
            this.get_issuePropertiesView().show(viewParams);
        }
    },

    _issuePropertiesViewHideAfterMiddleStep: function (sender, args) {
        var hideResult = args.get_result();
        this._abTest = args.get_data();
        switch (hideResult) {
            case this.get_issuePropertiesView().HIDE_RESULT.BACK:
                this.get_abTestMiddleStep().show({ issueAName: this._issueAName });
                break;
            case this.get_issuePropertiesView().HIDE_RESULT.GO_TO_MESSAGE:
                var that = this;
                var onSuccess = function (data, textStatus, jqXHR) {
                    var issue = data;
                    if (issue.MessageBody.MessageBodyType == that._manager.MESSAGE_BODY_TYPE.STANDARD) {
                        var search = window.top.location.search;
                        var queryStringParser = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(search.substring(1, search.length));
                        var returnUrl = queryStringParser.get("ReturnUrl");
                        if (!returnUrl) {
                            returnUrl = window.top.location.href;
                        }
                        window.top.location = that._manager.getZoneEditorUrl(issue.MessageBody.Id, issue.Id, returnUrl);
                    }
                    else {
                        var viewParams = {
                            issue: issue,
                            origin: that._origin
                        };
                        that._messageViewCurrentHideDelegate = that._messageViewHideAfterPropertiesDelegate;
                        that.get_messageView().show(viewParams);
                    }
                }

                this._manager.getIssue(this._abTest.CampaignBId, onSuccess);
                break;
        }
    },

    _messageViewHideAfterProperties: function (sender, args) {
        var that = this;
        switch (args.get_result()) {
            case this.get_messageView().HIDE_RESULT.BACK:
                dialogBase.close();
                break;
            case this.get_messageView().HIDE_RESULT.SAVE_DRAFT:
                dialogBase.closeUpdated();
                break;
            case this.get_messageView().HIDE_RESULT.SEND:
                this.get_sendView().show({ abTestId: this._abTest.Id });
                break;
            case this.get_messageView().HIDE_RESULT.TITLE_AND_PROPERTIES:
                this._showTitleAndPropertiesAfterMessage(args.get_data());
                break;
        }
    },

    _messageViewHideAfterSend: function (sender, args) {
        var that = this;
        var issueFromMessage = args.get_data();

        if (this._abTest && args.get_result() != this.get_messageView().HIDE_RESULT.TITLE_AND_PROPERTIES) {
            if (this._isBIssue) {
                this._abTest.CampaignBMessageSubject = issueFromMessage.MessageSubject;
                this._abTest.CampaignBFromName = issueFromMessage.FromName;
                this._abTest.CampaignBReplyToEmail = issueFromMessage.ReplyToEmail;
            }
            else {
                this._abTest.CampaignAName = issueFromMessage.Name;
                this._abTest.CampaignBName = issueFromMessage.Name;
                this._abTest.CampaignAMessageSubject = issueFromMessage.MessageSubject;
                this._abTest.CampaignAFromName = issueFromMessage.FromName;
                this._abTest.CampaignAReplyToEmail = issueFromMessage.ReplyToEmail;
                this._abTest.CampaignAList = issueFromMessage.ListTitle;
                this._abTest.CampaignBList = issueFromMessage.ListTitle;
                this._abTest.SubscribersCount = issueFromMessage.ListSubscriberCount;
                this.get_sendView().setTestingSamplePercentageLabel();
            }
        }

        switch (args.get_result()) {
            case this.get_messageView().HIDE_RESULT.BACK:
                this.get_sendView().show({ abTest: this._abTest });
                break;
            case this.get_messageView().HIDE_RESULT.SAVE_DRAFT:
            case this.get_messageView().HIDE_RESULT.SEND:
                this.get_sendView().show({ abTest: this._abTest });
                break;
            case this.get_messageView().HIDE_RESULT.TITLE_AND_PROPERTIES:
                this._showTitleAndPropertiesAfterMessage(issueFromMessage);
                break;
        }
    },

    _showTitleAndPropertiesAfterMessage: function (issueFromMessage) {
        var viewParams = {
            isCreateMode: false,
            issueId: issueFromMessage.Id,
            isBIssue: this._isBIssue,
            origin: "content"
        };
        var that = this;
        this._issuePropertiesViewCurrentHideDelegate = function (sender, args) {
            var issue = args.get_data();
            switch (args.get_result()) {
                case that.get_issuePropertiesView().HIDE_RESULT.BACK:
                    issue = issueFromMessage;
                    break;
                case that.get_issuePropertiesView().HIDE_RESULT.SAVE:
                    issue.MessageBody = issueFromMessage.MessageBody;
                    break;
            }
            var messageViewParams = {
                issue: issue,
                origin: that._origin
            };
            that.get_messageView().show(messageViewParams);
        };
        this.get_issuePropertiesView().show(viewParams);
    },

    _issuePropertiesViewHideHandler: function (sender, args) {
        if (args.get_result()) {
            if (this._issuePropertiesViewCurrentHideDelegate) {
                this._issuePropertiesViewCurrentHideDelegate(sender, args);
            }
            else {
                throw "AbTestIssuePropertiesView 'hide' is not handled!";
            }
        }
    },

    _messageViewHideHandler: function (sender, args) {
        if (args.get_result()) {
            if (this._messageViewCurrentHideDelegate) {
                this._messageViewCurrentHideDelegate(sender, args);
            }
            else {
                throw "AbTestMessageView 'hide' is not handled!";
            }
        }
    },

    _sendViewHideHandler: function (sender, args) {
        if (args.get_result()) {
            this._abTest = args.get_data();
            var viewParams = { origin: "send" };
            switch (args.get_result()) {
                case this.get_sendView().HIDE_RESULT.EDIT_ISSUE_A:
                    this._isBIssue = false;
                    viewParams.issueId = this._abTest.CampaignAId;
                    break;
                case this.get_sendView().HIDE_RESULT.EDIT_ISSUE_B:
                    this._isBIssue = true;
                    viewParams.issueId = this._abTest.CampaignBId;
                    break;
            }

            this._messageViewCurrentHideDelegate = this._messageViewHideAfterSendDelegate;
            this.get_messageView().show(viewParams);
        }
    },

    get_abTestMiddleStep: function () {
        return this._abTestMiddleStep;
    },
    set_abTestMiddleStep: function (value) {
        this._abTestMiddleStep = value;
    },
    get_issuePropertiesView: function () {
        return this._issuePropertiesView;
    },
    set_issuePropertiesView: function (value) {
        this._issuePropertiesView = value;
    },
    get_sendView: function () {
        return this._sendView;
    },
    set_sendView: function (value) {
        this._sendView = value;
    },
    get_messageView: function () {
        return this._messageView;
    },
    set_messageView: function (value) {
        this._messageView = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestDetailView', Sys.UI.Control);

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs = function (result, data) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs.initializeBase(this);

    this._result = result;
    this._data = data;
    this._cancel = false;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs.callBaseMethod(this, 'dispose');
    },

    get_result: function () {
        return this._result;
    },
    get_data: function () {
        return this._data;
    },
    get_cancel: function () {
        return this._cancel;
    },
    set_cancel: function (value) {
        this._cancel = value;
    }
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs', Sys.EventArgs);