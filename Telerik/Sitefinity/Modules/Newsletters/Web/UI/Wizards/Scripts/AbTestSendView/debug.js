Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView.initializeBase(this, [element]);

    this._clientLabelManager = null;
    this._messageControl = null;
    this._previewWindow = null;
    this._sendViewWrapper = null;

    this._sendPrompt = null;

    this._issueATitle = null;
    this._viewIssueALink = null;
    this._issueAMessageSubject = null;
    this._issueAFromName = null;
    this._issueAReplyEmail = null;
    this._issueAMailingList = null;
    this._editIssueAButton = null;

    this._issueBTitle = null;
    this._viewIssueBLink = null;
    this._issueBMessageSubject = null;
    this._issueBFromName = null;
    this._issueBReplyEmail = null;
    this._issueBMailingList = null;
    this._editIssueBButton = null;

    this._nameTextField = null;
    this._testingNoteTextField = null;
    this._testingSampleDescriptionLabel = null;
    this._testingSampleSlider = null;
    this._testingSamplePercentageLabel = null;
    this._winningFactorChoiceField = null;
    this._sendingDecisionChoiceField = null;
    this._schedulerWrapper = null;
    this._scheduleABTestPicker = null;
    this._testingPeriodEndPicker = null;

    this._buttonsPanel = null;
    this._sendABTestButton = null;
    this._scheduleABTestButton = null;
    this._saveDraftButton = null;
    this._cancelLink = null;
    this._loadingView = null;

    this._abTest = null;
    this._rootUrl = null;
    this._providerName = null;
    this._manager = null;
    this._testingSample = 0;

    this._ajaxCompleteDelegate = null;
    this._ajaxFailDelegate = null;

    this._viewIssueADelegate = null;
    this._viewIssueBDelegate = null;
    this._editIssueADelegate = null;
    this._editIssueBDelegate = null;
    this._testingSampleSliderValueChangedDelegate = null;
    this._sendingDecisionFieldValueChangedDelegate = null;
    this._sendABTestDelegate = null;
    this._saveDraftAndScheduleDelegate = null;
    this._cancelDelegate = null;

    this.HIDE_RESULT = {
        EDIT_ISSUE_A: "edit-issue-a",
        EDIT_ISSUE_B: "edit-issue-b"
    };
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView.callBaseMethod(this, "initialize");

        this._manager = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersClientManager(this._rootUrl, this._providerName);

        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);

        this._viewIssueADelegate = Function.createDelegate(this, this._viewIssueA);
        $addHandler(this.get_viewIssueALink(), "click", this._viewIssueADelegate);

        this._viewIssueBDelegate = Function.createDelegate(this, this._viewIssueB);
        $addHandler(this.get_viewIssueBLink(), "click", this._viewIssueBDelegate);

        this._editIssueADelegate = Function.createDelegate(this, this._editIssueA);
        $addHandler(this.get_editIssueAButton(), "click", this._editIssueADelegate);

        this._editIssueBDelegate = Function.createDelegate(this, this._editIssueB);
        $addHandler(this.get_editIssueBButton(), "click", this._editIssueBDelegate);

        this._testingSampleSliderValueChangedDelegate = Function.createDelegate(this, this._testingSampleSliderValueChanged);
        this.get_testingSampleSlider().add_valueChanged(this._testingSampleSliderValueChangedDelegate);

        this._sendingDecisionFieldValueChangedDelegate = Function.createDelegate(this, this._sendingDecisionFieldValueChanged);
        this.get_sendingDecisionChoiceField().add_valueChanged(this._sendingDecisionFieldValueChangedDelegate);

        this._sendABTestDelegate = Function.createDelegate(this, this._sendABTest);
        $addHandler(this.get_sendABTestButton(), "click", this._sendABTestDelegate);

        this._saveDraftAndScheduleDelegate = Function.createDelegate(this, this._saveDraftAndSchedule);
        $addHandler(this.get_scheduleABTestButton(), "click", this._saveDraftAndScheduleDelegate);
        $addHandler(this.get_saveDraftButton(), "click", this._saveDraftAndScheduleDelegate);

        this._cancelDelegate = Function.createDelegate(this, this._cancel);
        $addHandler(this.get_cancelLink(), "click", this._cancelDelegate);
    },

    dispose: function () {
        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }
        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._viewIssueADelegate) {
            if (this.get_viewIssueALink()) {
                $removeHandler(this.get_viewIssueALink(), "click", this._viewIssueADelegate);
            }
            delete this._viewIssueADelegate;
        }

        if (this._viewIssueBDelegate) {
            if (this.get_viewIssueBLink()) {
                $removeHandler(this.get_viewIssueBLink(), "click", this._viewIssueBDelegate);
            }
            delete this._viewIssueBDelegate;
        }

        if (this._editIssueADelegate) {
            if (this.get_editIssueAButton()) {
                $removeHandler(this.get_editIssueAButton(), "click", this._editIssueADelegate);
            }
            delete this._editIssueADelegate;
        }

        if (this._editIssueBDelegate) {
            if (this.get_editIssueBButton()) {
                $removeHandler(this.get_editIssueBButton(), "click", this._editIssueBDelegate);
            }
            delete this._editIssueBDelegate;
        }

        if (this._testingSampleSliderValueChangedDelegate) {
            if (this.get_testingSampleSlider()) {
                this.get_testingSampleSlider().remove_valueChanged(this._testingSampleSliderValueChangedDelegate);
            }
            delete this._testingSampleSliderValueChangedDelegate;
        }

        if (this._sendingDecisionFieldValueChangedDelegate) {
            if (this.get_sendingDecisionChoiceField()) {
                this.get_sendingDecisionChoiceField().remove_valueChanged(this._sendingDecisionFieldValueChangedDelegate);
            }
            delete this._sendingDecisionFieldValueChangedDelegate;
        }

        if (this._sendABTestDelegate) {
            if (this.get_sendABTestButton()) {
                $removeHandler(this.get_sendABTestButton(), "click", this._sendABTestDelegate);
            }
            delete this._sendABTestDelegate;
        }

        if (this._saveDraftAndScheduleDelegate) {
            if (this.get_saveDraftButton()) {
                $removeHandler(this.get_saveDraftButton(), "click", this._saveDraftAndScheduleDelegate);
            }
            if (this.get_scheduleABTestButton()) {
                $removeHandler(this.get_scheduleABTestButton(), "click", this._saveDraftAndScheduleDelegate);
            }
            delete this._saveDraftAndScheduleDelegate;
        }

        if (this._cancelDelegate) {
            if (this.get_cancelLink()) {
                $removeHandler(this.get_cancelLink(), "click", this._cancelDelegate);
            }
            delete this._cancelDelegate;
        }

        this._manager.dispose();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    show: function (viewParams) {
        this.reset();

        this._abTest = viewParams.abTest;

        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._abTest = data;
            that._updateUi();
            jQuery(that.get_sendViewWrapper()).show();
            //fixes an issue with #259996 in TWU 2014 Q1 SP1 
            that.get_testingSampleSlider().repaint();
            if (jQuery.browser.webkit || jQuery.browser.msie)
                jQuery(".sfMediumNewContentForm").addClass("sfShiftLeft");
        };

        if (this._abTest == null) {
            this._setLoadingViewVisible(true);
            this._manager.getAbTest(viewParams.abTestId, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
        else {
            onSuccess(this._abTest);
        }
    },

    hide: function (result, data) {
        var args = this._raise_hide(result, data);

        if (args.get_cancel() == false) {
            jQuery(this.get_sendViewWrapper()).hide();
        }
    },

    add_hide: function (delegate) {
        this.get_events().addHandler('hide', delegate);
    },

    remove_hide: function (delegate) {
        this.get_events().removeHandler('hide', delegate);
    },

    reset: function () {
        this._abTest = null;

        this.get_nameTextField().reset();
        this.get_testingNoteTextField().reset();
        this.get_winningFactorChoiceField().set_value(0);
        this.get_sendingDecisionChoiceField().set_value(0);

        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate() + 7);
        this.get_scheduleABTestPicker().set_selectedDate(currentDate);
        this.get_testingPeriodEndPicker().set_selectedDate(currentDate);

        jQuery(this.get_sendABTestButton()).show();
        jQuery(this.get_saveDraftButton()).show();
        jQuery(this.get_scheduleABTestButton()).hide();
    },

    setTestingSamplePercentageLabel: function () {
        var testingSamplePercentageText = this.get_clientLabelManager().getLabel("NewslettersResources", "TestingSamplePercentage");
        var subscribersCount = (this._abTest) ? this._abTest.SubscribersCount : 0;
        var percentage = this.get_testingSampleSlider().get_value();
        this._testingSample = Math.floor(subscribersCount * percentage / 100);
        var textToDisplay = String.format(testingSamplePercentageText, this._testingSample, percentage);

        jQuery(this.get_testingSamplePercentageLabel()).html(textToDisplay);
    },

    /* *************************** private methods *************************** */

    _raise_hide: function (result, data) {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('hide');
            var args = new Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestViewHideEventArgs(result, data);
            if (h) h(this, args);
            return args;
        }
    },

    _updateUi: function () {
        // issue A
        var issueATitle = this.get_clientLabelManager().getLabel("NewslettersResources", "IssueATitle");
        this.get_issueATitle().innerHTML = String.format(issueATitle, this._abTest.CampaignAName);
        this.get_issueAMessageSubject().innerHTML = this._abTest.CampaignAMessageSubject;
        this.get_issueAFromName().innerHTML = this._abTest.CampaignAFromName;
        this.get_issueAReplyEmail().innerHTML = this._abTest.CampaignAReplyToEmail;
        this.get_issueAReplyEmail().innerHTML = this._abTest.CampaignAReplyToEmail;
        this.get_issueAMailingList().innerHTML = this._abTest.CampaignAList;

        // issue B
        var issueBTitle = this.get_clientLabelManager().getLabel("NewslettersResources", "IssueBTitle");
        this.get_issueBTitle().innerHTML = String.format(issueBTitle, this._abTest.CampaignBName);
        this.get_issueBMessageSubject().innerHTML = this._abTest.CampaignBMessageSubject;
        this.get_issueBFromName().innerHTML = this._abTest.CampaignBFromName;
        this.get_issueBReplyEmail().innerHTML = this._abTest.CampaignBReplyToEmail;
        this.get_issueBReplyEmail().innerHTML = this._abTest.CampaignBReplyToEmail;
        this.get_issueBMailingList().innerHTML = this._abTest.CampaignBList;

        this.get_nameTextField().set_value(this._abTest.Name);
        this.get_testingNoteTextField().set_value(this._abTest.TestingNote);

        // testing sample
        var testingSampleDescription = this.get_clientLabelManager().getLabel("NewslettersResources", "TestingSampleDescription");
        this.get_testingSampleDescriptionLabel().innerHTML = String.format(testingSampleDescription, this._abTest.CampaignAList);
        this.get_testingSampleSlider().set_value(this._abTest.TestingSamplePercentage);
        // winning criteria
        this.get_winningFactorChoiceField().set_value(this._abTest.WinningCondition);

        var dateRegExp = /^\/Date\((.*?)\)\/$/;
        var dateMatch = null;

        // schedule test
        if (this._abTest.ScheduleDate) {
            this._showSchedulerWrapper();
            this.get_sendingDecisionChoiceField().set_value(1);

            dateMatch = dateRegExp.exec(this._abTest.ScheduleDate);
            if (dateMatch) {
                this.get_scheduleABTestPicker().set_selectedDate(new Date(parseInt(dateMatch[1])));
            }
            else {
                this.get_scheduleABTestPicker().set_selectedDate(this._abTest.ScheduleDate);
            }
        }
        else {
            this._hideSchedulerWrapper();
            this.get_sendingDecisionChoiceField().set_value(0);
        }

        // A/B test end
        dateMatch = dateRegExp.exec(this._abTest.TestingEnds);
        if (dateMatch) {
            this.get_testingPeriodEndPicker().set_selectedDate(new Date(parseInt(dateMatch[1])));
        }
        else {
            this.get_testingPeriodEndPicker().set_selectedDate(this._abTest.TestingEnds);
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

    _isFormValid: function () {
        return this.get_nameTextField().validate();
    },

    _updateAbTest: function () {
        if (this._abTest) {
            this._abTest.Name = this.get_nameTextField().get_value();
            this._abTest.WinningCondition = this.get_winningFactorChoiceField().get_value();
            this._abTest.TestingSamplePercentage = this.get_testingSampleSlider().get_value();
            this._abTest.TestingEnds = this.get_testingPeriodEndPicker().get_selectedDate();
            this._abTest.TestingNote = this.get_testingNoteTextField().get_value();
            if (jQuery(this.get_schedulerWrapper()).is(":visible")) {
                this._abTest.ScheduleDate = this.get_scheduleABTestPicker().get_selectedDate();
            }
        }
    },

    _getAbTestObject: function () {
        var abTestObject = {
            'Id': this._abTest.Id,
            'RootCampaignId': this._abTest.RootCampaignId,
            'Name': this._abTest.Name,
            'CampaignAId': this._abTest.CampaignAId,
            'CampaignBId': this._abTest.CampaignBId,
            'WinningCondition': this._abTest.WinningCondition,
            'TestingSamplePercentage': this._abTest.TestingSamplePercentage,
            'TestingEnds': this._abTest.TestingEnds,
            'TestingNote': this._abTest.TestingNote,
            'ScheduleDate': this._abTest.ScheduleDate
        };

        return abTestObject;
    },

    _saveAbTest: function (onSuccess) {
        if (this._isFormValid()) {
            this._updateAbTest();
            this._setLoadingViewVisible(true);
            this._manager.saveAbTest(this._getAbTestObject(), onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
    },

    _showSchedulerWrapper: function () {
        jQuery(this.get_schedulerWrapper()).show();

        jQuery(this.get_sendABTestButton()).hide();
        jQuery(this.get_saveDraftButton()).hide();
        jQuery(this.get_scheduleABTestButton()).show();
    },

    _hideSchedulerWrapper: function () {
        jQuery(this.get_schedulerWrapper()).hide();

        jQuery(this.get_sendABTestButton()).show();
        jQuery(this.get_saveDraftButton()).show();
        jQuery(this.get_scheduleABTestButton()).hide();
    },

    /* *************************** event handlers *************************** */

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    _viewIssueA: function () {
        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that.get_previewWindow().show(data);
        };

        this._setLoadingViewVisible(true);
        this._manager.getIssue(this._abTest.CampaignAId, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
    },

    _viewIssueB: function () {
        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that.get_previewWindow().show(data);
        };

        this._setLoadingViewVisible(true);
        this._manager.getIssue(this._abTest.CampaignBId, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
    },

    _editIssueA: function (sender, args) {
        this._updateAbTest();
        this.hide(this.HIDE_RESULT.EDIT_ISSUE_A, this._abTest);
    },

    _editIssueB: function (sender, args) {
        this._updateAbTest();
        this.hide(this.HIDE_RESULT.EDIT_ISSUE_B, this._abTest);
    },

    _sendingDecisionFieldValueChanged: function (sender, args) {
        var selectedValue = sender.get_value();
        if (selectedValue == 1) {
            this._showSchedulerWrapper();
        }
        else {
            this._hideSchedulerWrapper();
        }
    },

    _testingSampleSliderValueChanged: function (sender, args) {
        this.setTestingSamplePercentageLabel();
    },

    _sendABTest: function () {
        this.get_sendPrompt().set_title(this.get_clientLabelManager().getLabel('NewslettersResources', 'Send') + ' ' + this.get_nameTextField().get_value());
        this.get_sendPrompt().set_message(String.format(this.get_clientLabelManager().getLabel('NewslettersResources', 'SendAbTestPromptText'), this._testingSample));

        var that = this;
        var onSaveSuccess = function () {
            var onTestingSuccess = function () { dialogBase.closeUpdated(); };
            that._setLoadingViewVisible(true);
            that._manager.startAbTesting(that._abTest.Id, onTestingSuccess, that._ajaxFailDelegate, that._ajaxCompleteDelegate);
        };
        var promptCallback = function (sender, args) {
            if (args.get_commandName() == "send") {
                that._saveAbTest(onSaveSuccess);
            }
        };

        this.get_sendPrompt().show_prompt(null, null, promptCallback);
    },

    _saveDraftAndSchedule: function () {
        var onSuccess = function () { dialogBase.closeUpdated(); };
        this._saveAbTest(onSuccess);
    },

    _cancel: function () {
        dialogBase.close();
    },

    /* *************************** properties *************************** */

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_previewWindow: function () {
        return this._previewWindow;
    },
    set_previewWindow: function (value) {
        this._previewWindow = value;
    },
    get_sendViewWrapper: function () {
        return this._sendViewWrapper;
    },
    set_sendViewWrapper: function (value) {
        this._sendViewWrapper = value;
    },
    get_issueATitle: function () {
        return this._issueATitle;
    },
    set_issueATitle: function (value) {
        this._issueATitle = value;
    },
    get_viewIssueALink: function () {
        return this._viewIssueALink;
    },
    set_viewIssueALink: function (value) {
        this._viewIssueALink = value;
    },
    get_issueAMessageSubject: function () {
        return this._issueAMessageSubject;
    },
    set_issueAMessageSubject: function (value) {
        this._issueAMessageSubject = value;
    },
    get_issueAFromName: function () {
        return this._issueAFromName;
    },
    set_issueAFromName: function (value) {
        this._issueAFromName = value;
    },
    get_issueAReplyEmail: function () {
        return this._issueAReplyEmail;
    },
    set_issueAReplyEmail: function (value) {
        this._issueAReplyEmail = value;
    },
    get_issueAMailingList: function () {
        return this._issueAMailingList;
    },
    set_issueAMailingList: function (value) {
        this._issueAMailingList = value;
    },
    get_editIssueAButton: function () {
        return this._editIssueAButton;
    },
    set_editIssueAButton: function (value) {
        this._editIssueAButton = value;
    },
    get_issueBTitle: function () {
        return this._issueBTitle;
    },
    set_issueBTitle: function (value) {
        this._issueBTitle = value;
    },
    get_viewIssueBLink: function () {
        return this._viewIssueBLink;
    },
    set_viewIssueBLink: function (value) {
        this._viewIssueBLink = value;
    },
    get_issueBMessageSubject: function () {
        return this._issueBMessageSubject;
    },
    set_issueBMessageSubject: function (value) {
        this._issueBMessageSubject = value;
    },
    get_issueBFromName: function () {
        return this._issueBFromName;
    },
    set_issueBFromName: function (value) {
        this._issueBFromName = value;
    },
    get_issueBReplyEmail: function () {
        return this._issueBReplyEmail;
    },
    set_issueBReplyEmail: function (value) {
        this._issueBReplyEmail = value;
    },
    get_issueBMailingList: function () {
        return this._issueBMailingList;
    },
    set_issueBMailingList: function (value) {
        this._issueBMailingList = value;
    },
    get_editIssueBButton: function () {
        return this._editIssueBButton;
    },
    set_editIssueBButton: function (value) {
        this._editIssueBButton = value;
    },
    get_nameTextField: function () {
        return this._nameTextField;
    },
    set_nameTextField: function (value) {
        this._nameTextField = value;
    },
    get_testingNoteTextField: function () {
        return this._testingNoteTextField;
    },
    set_testingNoteTextField: function (value) {
        this._testingNoteTextField = value;
    },
    get_testingSampleDescriptionLabel: function () {
        return this._testingSampleDescriptionLabel;
    },
    set_testingSampleDescriptionLabel: function (value) {
        this._testingSampleDescriptionLabel = value;
    },
    get_testingSampleSlider: function () {
        return this._testingSampleSlider;
    },
    set_testingSampleSlider: function (value) {
        this._testingSampleSlider = value;
    },
    get_testingSamplePercentageLabel: function () {
        return this._testingSamplePercentageLabel;
    },
    set_testingSamplePercentageLabel: function (value) {
        this._testingSamplePercentageLabel = value;
    },
    get_winningFactorChoiceField: function () {
        return this._winningFactorChoiceField;
    },
    set_winningFactorChoiceField: function (value) {
        this._winningFactorChoiceField = value;
    },
    get_sendingDecisionChoiceField: function () {
        return this._sendingDecisionChoiceField;
    },
    set_sendingDecisionChoiceField: function (value) {
        this._sendingDecisionChoiceField = value;
    },
    get_schedulerWrapper: function () {
        return this._schedulerWrapper;
    },
    set_schedulerWrapper: function (value) {
        this._schedulerWrapper = value;
    },
    get_scheduleABTestPicker: function () {
        return this._scheduleABTestPicker;
    },
    set_scheduleABTestPicker: function (value) {
        this._scheduleABTestPicker = value;
    },
    get_testingPeriodEndPicker: function () {
        return this._testingPeriodEndPicker;
    },
    set_testingPeriodEndPicker: function (value) {
        this._testingPeriodEndPicker = value;
    },
    get_buttonsPanel: function () {
        return this._buttonsPanel;
    },
    set_buttonsPanel: function (value) {
        this._buttonsPanel = value;
    },
    get_sendABTestButton: function () {
        return this._sendABTestButton;
    },
    set_sendABTestButton: function (value) {
        this._sendABTestButton = value;
    },
    get_scheduleABTestButton: function () {
        return this._scheduleABTestButton;
    },
    set_scheduleABTestButton: function (value) {
        this._scheduleABTestButton = value;
    },
    get_saveDraftButton: function () {
        return this._saveDraftButton;
    },
    set_saveDraftButton: function (value) {
        this._saveDraftButton = value;
    },
    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },
    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },
    get_sendPrompt: function () {
        return this._sendPrompt;
    },
    set_sendPrompt: function (value) {
        this._sendPrompt = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.AbTestSendView', Sys.UI.Control);
