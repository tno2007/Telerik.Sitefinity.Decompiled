﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards");

var campaignWizard = null;

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard = function (element) {

    this._wizardControl = null;
    this._wizardTitleLabel = null;
    this._wizardCreateTitle = null;
    this._wizardEditTitle = null;
    this._stepControls = null;

    this._listSelectorStep = null;
    this._campaignBasicInfoStep = null;
    this._campaignTypeStep = null;
    this._campaignMessageStep = null;
    this._campaignConfirmationStep = null;
    this._messageControl = null;
    this._clientLabelManager = null;
    this._testMessageSentSuccessfully = null;

    this._commandDelegate = null;
    this._pageChangedDelegate = null;
    this._campaignTypeChangedDelegate = null;

    this._campaignType = null;
    this._campaignServiceUrl = null;
    this._sendTestEmailsDialogUrl = null;

    this._STANDARDCAMPAIGN = 'standard';
    this._HTMLCAMPAIGN = 'html';
    this._PLAINTEXTCAMPAIGN = 'plaintext';
    this._ABCAMPAIGN = 'ab';

    this._currentCampaign = null;
    this._campaignLoaded = false;
    this._closeOnSave = false;
    this._isEdit = false;
    this._currentStep = null;

    this._currentWindow = null;
    this._campaignPreviewDialog = null;
    this._onCampaignPreviewDialogCloseDelegate = null;

    this._isFirstShow = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard.initializeBase(this, [element]);
}
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard.prototype = {

    // set up 
    initialize: function () {
        campaignWizard = this;

        this._initializeStepControls();

        if (this._commandDelegate === null) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        }

        if (this._pageChangedDelegate === null) {
            this._pageChangedDelegate = Function.createDelegate(this, this._pageChangedHandler);
        }

        if (this._campaignTypeChangedDelegate === null) {
            this._campaignTypeChangedDelegate = Function.createDelegate(this, this._campaignTypeChangedHandler);
        }

        if (this._onCampaignPreviewDialogCloseDelegate === null) {
            this._onCampaignPreviewDialogCloseDelegate = Function.createDelegate(this, this._onCampaignPreviewDialogClose);
        }

        this._wizardControl.add_pageChanged(this._pageChangedDelegate);
        this.get_campaignConfirmationStep().add_command(this._commandDelegate);
        this.get_campaignTypeStep().add_typeChanged(this._campaignTypeChangedDelegate);

        if (this._currentWindow == null) {
            this._currentWindow = this._getRadWindow();
        }

        if (this._campaignPreviewDialog == null) {
            var windowManager = this._currentWindow.get_windowManager();
            this._campaignPreviewDialog = windowManager.getWindowByName("campaignPreviewDialog");
        }

        this._campaignPreviewDialog.add_close(this._onCampaignPreviewDialogCloseDelegate);

        // by default, campaign message step should be in html mode
        this.get_campaignMessageStep().switchToHtmlMode();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._commandDelegate) {
            this.get_campaignConfirmationStep().remove_command(this._commandDelegate);
            delete this._commandDelegate;
        }

        if (this._pageChangedDelegate) {
            this._wizardControl.remove_pageChanged(this._pageChangedDelegate);
            delete this._pageChangedDelegate;
        }

        if (this._campaignTypeChangedDelegate) {
            this.get_campaignTypeStep().remove_typeChanged(this._campaignTypeChangedDelegate);
            delete this._campaignTypeChangedDelegate;
        }

        if (this._onCampaignPreviewDialogCloseDelegate) {
            if (this._campaignPreviewDialog) {
                this._campaignPreviewDialog.remove_close(this._onCampaignPreviewDialogCloseDelegate);
            }
            delete this._onCampaignPreviewDialogCloseDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */
    setWizard: function (campaign, buttonInvoked) {
        this._isFirstShow = this._isFirstShow == null;

        if (buttonInvoked && !this._isFirstShow) {
            this._resetWizard();
        }
        if (campaign == null) {
            this.get_wizardTitleLabel().innerHTML = this.get_wizardCreateTitle();
        } else {
            this.get_wizardTitleLabel().innerHTML = this.get_wizardEditTitle();
            this.loadCampaign(campaign);
        }
    },

    loadCampaign: function (campaign) {
        if (this._campaignLoaded == false) {
            // set the existing campaign id
            this._currentCampaign = campaign;
            // set the correct list
            this.get_listSelectorStep().selectList(campaign.ListId);
            // set basic info
            this.get_campaignBasicInfoStep().setBasicInfo(campaign);
            // set campaign type
            this.get_campaignTypeStep().set_campaignType(campaign.MessageBody.MessageBodyType);
            // set the message body
            switch (campaign.MessageBody.MessageBodyType) {
                case 0:
                case 1:
                    this.get_campaignMessageStep().set_bodyText(campaign.MessageBody.BodyText);
                    this.get_campaignMessageStep().set_htmlPlainTextVersion(campaign.MessageBody.PlainTextVersion);
                    break;
                case 2:
                    this.get_campaignMessageStep().set_pagePlainTextVersion(campaign.MessageBody.PlainTextVersion);
                    break;
                default:
                    alert('Campaign type not supported.');
            }
            this._isEdit = true;
            this._campaignLoaded = true;
        }
    },

    /* *************************** private methods *************************** */

    _resetWizard: function () {
        this.get_campaignBasicInfoStep().reset();
        this.get_campaignConfirmationStep().reset();
        this.get_campaignMessageStep().reset();
        this.get_campaignTypeStep().reset();
        this.get_listSelectorStep().reset();
        this._isEdit = false;
        this._campaignLoaded = false;
        this._currentCampaign = null;
        this._closeOnSave = false;
        this._wizardControl.goToFirstStep();
    },

    _initializeStepControls: function () {
        this._stepControls = [];
        var steps = this._wizardControl.get_steps();
        for (var stepIter = 0; stepIter < steps.length; stepIter++) {
            this._stepControls.push($find(steps[stepIter]));
        }
    },

    _getStepControl: function (stepType) {
        for (var si = 0; si < this._stepControls.length; si++) {
            if (this._stepControls[si] != null && Object.getTypeName(this._stepControls[si]) == stepType) {
                return this._stepControls[si];
            }
        }
        return null;
    },

    _commandHandler: function (sender, args) {
        switch (args.CommandName) {
            case 'saveCampaign':
                this._closeOnSave = true;
                this._saveCampaign(true);
                break;
            case 'previewCampaign':
                this._openCampaignPreview();
                break;
            case 'sendTest':
                this._sendTestMessage(this._currentCampaign.Id, args.CommandArgument);
                break;
            case 'send':
                this._sendCampaign(this._currentCampaign.Id);
                break;
            case 'scheduleDelivery':
                if (args.CommandArgument) {
                    this._scheduleCampaign(this._currentCampaign.Id, args.CommandArgument);
                }
                break;
            case 'discardCampaign':
                this._deleteCampaign(this._currentCampaign.Id);
                break;
            default:
                alert('Command "' + args.CommandName + '" is not supported.');
        }
    },

    _campaignTypeChangedHandler: function (sender, args) {
        if (args.CampaignType == 0) {
            this.get_campaignMessageStep().switchToPlainTextMode();
        } else if (args.CampaignType == 1) {
            this.get_campaignMessageStep().switchToHtmlMode();
        } else if (args.CampaignType == 2) {
            this.get_campaignMessageStep().switchToStandardInternalMode();
        } else if (args.CampaignType == 3) {
            this.get_campaignMessageStep().switchToStandardExternalMode();
        }
    },

    _saveCampaign: function (isReadyForSending) {
        var campaignObject = this._getCampaignObject(isReadyForSending);
        this.get_campaignTypeStep().get_templatesChoiceField().reset();
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this.get_campaignServiceUrl();
        var urlParams = [];
        var id = (this._currentCampaign === null) ? clientManager.GetEmptyGuid() : this._currentCampaign.Id;
        var keys = [id];

        this.get_campaignBasicInfoStep().set_autoSaved(false);
        this.get_campaignConfirmationStep().set_autoSaved(false);
        this.get_campaignMessageStep().set_autoSaved(false);
        this.get_campaignTypeStep().set_autoSaved(false);
        this.get_listSelectorStep().set_autoSaved(false);

        clientManager.InvokePut(serviceUrl, urlParams, keys, campaignObject, this._saveCampaign_Success, this._saveCampaign_Failure, this);
    },

    _saveCampaign_Success: function (caller, args) {
        caller._currentCampaign = args;
        caller.get_campaignMessageStep().loadMessageBody(caller._currentCampaign.MessageBody);

        caller.get_campaignBasicInfoStep().set_autoSaved(true);
        caller.get_campaignConfirmationStep().set_autoSaved(true);
        caller.get_campaignMessageStep().set_autoSaved(true);
        caller.get_campaignMessageStep().setMergeTags(caller._currentCampaign.MergeTags);
        caller.get_campaignTypeStep().set_autoSaved(true);
        caller.get_listSelectorStep().set_autoSaved(true);

        if (caller._closeOnSave && caller._isEdit) {
            dialogBase.closeUpdated();
        } else if (caller._closeOnSave) {
            dialogBase.closeCreated();
        }

        caller._currentStep.show();
    },

    _saveCampaign_Failure: function (sender, args) {
        alert('Campaign was not saved. There was a problem.');
    },

    _sendCampaign: function (campaignId) {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this.get_campaignServiceUrl() + '/send/';
        var urlParams = [];
        var keys = [];

        clientManager.InvokePut(serviceUrl, urlParams, keys, campaignId, this._sendCampaign_Success, this._sendCampaign_Failure, this);
    },

    _sendCampaign_Success: function (caller, args) {
        if (caller._isEdit) {
            dialogBase.closeUpdated();
        } else {
            dialogBase.closeCreated();
        }
    },

    _sendCampaign_Failure: function (error, caller) {
        caller.get_messageControl().showNegativeMessage(error.Detail);
    },

    _sendTestMessage: function (campaignId, emailString) {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this.get_campaignServiceUrl() + '/sendtest/';
        var urlParams = [];
        var keys = [campaignId];

        if (this.validateEmailAddresses(emailString)) {
            clientManager.InvokePut(serviceUrl, urlParams, keys, this._parseEmailString(emailString), this._sendTestMessage_Success, this._sendTestMessage_Failure, this);
        }
    },

    _sendTestMessage_Success: function (caller, args) {
        caller.get_messageControl().showPositiveMessage(caller._testMessageSentSuccessfully);
    },

    _sendTestMessage_Failure: function (error, caller) {
        caller.get_messageControl().showNegativeMessage(error.Detail);
    },

    validateEmailAddresses: function (emailString) {
        if (!emailString.length > 0) {
            this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('NewslettersResources', 'EnterAtLeastOneTestEmailAddress'));
            return false;
        }
        var emailAddresses = this._parseEmailString(emailString);

        // Consider using <see cref="Telerik.Sitefinity.Constants.EmailAddressRegexPattern"/>
        var emailAddressRegexPattern = /[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}/i;

        for (var i = 0; i < emailAddresses.length; i++) {
            if (!emailAddressRegexPattern.test(emailAddresses[i])) {
                this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('ErrorMessages', 'EmailAddressViolationMessage'));
                return false;
            }
        }
        return true;
    },

    /* ------------------- scheduling --------------------------- */

    _scheduleCampaign: function (campaignId, deliveryDate) {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this.get_campaignServiceUrl() + '/schedule/';
        var urlParams = [];
        var keys = [campaignId];

        var data = new Date(deliveryDate.getFullYear(), deliveryDate.getMonth(), deliveryDate.getDate(), deliveryDate.getHours(), deliveryDate.getMinutes(), deliveryDate.getSeconds());
        clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._scheduleCampaign_Success, this._scheduleCampaign_Failure, this);
    },

    _scheduleCampaign_Success: function (sender, result, args) {
        if (sender._closeOnSave && sender._isEdit) {
            dialogBase.closeUpdated();
        } else {
            dialogBase.closeCreated();
        }
    },

    _scheduleCampaign_Failure: function (sender, result, args) {
        alert('There was a problem with scheduling a campaign.');
    },

    /* ------------------- scheduling --------------------------- */

    _parseEmailString: function (emailString) {
        return emailString.split(',');
    },

    _deleteCampaign: function (campaignId) {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this.get_campaignServiceUrl();
        var urlParams = [];
        var keys = [campaignId];

        clientManager.InvokeDelete(serviceUrl, urlParams, keys, this._deleteCampaign_Success, this._deleteCampaign_Failure, this);
    },

    _deleteCampaign_Success: function (caller, args) {
        dialogBase.close();
    },

    _deleteCampaign_Failure: function (caller, args) {
        alert('There was a problem and campaign could not be deleted. Try deleting the campaign from the grid.');
    },

    _getCampaignObject: function (isReadyForSending) {
        var plainTextVersion = null;
        var campaignType = this.get_campaignTypeStep().get_campaignType();
        if (campaignType == null) {
            if (this._currentCampaign && this._currentCampaign.MessageBody) {
                campaignType = this._currentCampaign.MessageBody.MessageBodyType;
            }
            else {
                campaignType = 1;
            }
        }
        if (campaignType == 1) {
            plainTextVersion = this.get_campaignMessageStep().get_htmlPlainTextVersion();
        }
        if (campaignType == 2) {
            plainTextVersion = this.get_campaignMessageStep().get_pagePlainTextVersion();
        }

        var campaign = {
            'Name': this.get_campaignBasicInfoStep().get_name(),
            'FromName': this.get_campaignBasicInfoStep().get_fromName(),
            'ReplyToEmail': this.get_campaignBasicInfoStep().get_replyToEmail(),
            'MessageSubject': this.get_campaignBasicInfoStep().get_messageSubject(),
            'IsReadyForSending': isReadyForSending,
            'ListId': this.get_listSelectorStep().get_selectedList().Id,
            'CampaignTemplateId': this.get_campaignTypeStep().get_campaignTemplateId(),
            'UseGoogleTracking': this.get_campaignBasicInfoStep().get_useGoogleTracking(),
            'MessageBody': {
                'MessageBodyType': campaignType,
                'BodyText': this.get_campaignMessageStep().get_bodyText(),
                'PlainTextVersion': plainTextVersion
            }
        };

        return campaign;
    },

    _pageChangedHandler: function (sender, args) {
        var showCurrentStep = true;
        this._currentStep = $find(args.CurrentStepId);
        var previousStepComponent = $find(args.PreviousStepId);
        if (previousStepComponent !== null) {
            var previousStepType = Object.getTypeName(previousStepComponent);
            if (this._needsToSave(previousStepType)) {
                showCurrentStep = false;
                this._saveCampaign(false);
            }
            if (previousStepType == 'Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep' && this._currentCampaign == null) {
                var list = previousStepComponent.get_selectedList();
                if (list.Id != this._currentStep.get_selectedListId()) {
                    this._currentStep.loadDefaults(list);
                }
            }
        }
        if (showCurrentStep)
            this._currentStep.show();
    },

    _needsToSave: function (previousStepType) {
        if (previousStepType == 'Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep') {
            return false;
        }

        if (previousStepType == 'Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign') {
            return false;
        }

        return true;
    },

    // preview dialogs
    _openCampaignPreview: function () {
        this._campaignPreviewDialog._sfCurrentCampaign = this._currentCampaign;

        this._campaignPreviewDialog.show();
        this._campaignPreviewDialog.maximize();
        this._currentWindow.minimize();
    },

    _onCampaignPreviewDialogClose: function () {
        this._campaignPreviewDialog._sfCurrentCampaign = null;

        this._currentWindow.maximize();
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
    get_wizardControl: function () {
        return this._wizardControl;
    },
    set_wizardControl: function (value) {
        this._wizardControl = value;
    },
    get_wizardTitleLabel: function () {
        return this._wizardTitleLabel;
    },
    set_wizardTitleLabel: function (value) {
        this._wizardTitleLabel = value;
    },
    get_wizardCreateTitle: function () {
        return this._wizardCreateTitle;
    },
    set_wizardCreateTitle: function (value) {
        this._wizardCreateTitle = value;
    },
    get_wizardEditTitle: function () {
        return this._wizardEditTitle;
    },
    set_wizardEditTitle: function (value) {
        this._wizardEditTitle = value;
    },
    get_campaignType: function () {
        return this._campaignType;
    },
    set_campaignType: function (value) {
        this._campaignType = value;
    },
    get_campaignServiceUrl: function () {
        return this._campaignServiceUrl;
    },
    set_campaignServiceUrl: function (value) {
        this._campaignServiceUrl = value;
    },
    get_listSelectorStep: function () {
        if (this._listSelectorStep === null) {
            this._listSelectorStep = this._getStepControl('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep' || previousStepType == 'Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep');
        }
        return this._listSelectorStep;
    },
    get_campaignBasicInfoStep: function () {
        if (this._campaignBasicInfoStep === null) {
            this._campaignBasicInfoStep = this._getStepControl('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo');
        }
        return this._campaignBasicInfoStep;
    },
    get_campaignTypeStep: function () {
        if (this._campaignTypeStep === null) {
            this._campaignTypeStep = this._getStepControl('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep');
        }
        return this._campaignTypeStep;
    },
    get_campaignMessageStep: function () {
        if (this._campaignMessageStep === null) {
            this._campaignMessageStep = this._getStepControl('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignMessageStep');
        }
        return this._campaignMessageStep;
    },
    get_campaignConfirmationStep: function () {
        if (this._campaignConfirmationStep === null) {
            this._campaignConfirmationStep = this._getStepControl('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign');
        }
        return this._campaignConfirmationStep;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.CampaignWizard', Sys.UI.Control);