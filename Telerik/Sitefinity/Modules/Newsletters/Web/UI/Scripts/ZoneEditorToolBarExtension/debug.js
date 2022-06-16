Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension = function (toolBar) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension.initializeBase(this);

    this._toolBar = null;
    this._campaign = null;
    this._campaignId = null;
    this._scheduledDeliveryDate = null;

    this._sendTestPrompt = null;
    this._sendIssuePrompt = null;
    this._scheduleDeliveryWindow = null;
    this._campaignServiceUrl = null;
    this._campaignWizardDialog = null;

    this._appLoadDelegate = null;
    this._sendCampaignDelegate = null;
    this._sendTestDelegate = null;
    this._sendTestCallbackDelegate = null;
    this._createAbTestDelegate = null;
    this._scheduleCampaignDialogClosedDelegate = null;
    this._scheduleCampaignDialogOpenDelegate = null;
    this._operationFailureDelegate = null;
    this._campaignWizardDialogShowDelegate = null;
    this._showTitleAndPropertiesDelegate = null;
    this._campaignWizardDialogLoadedDelegate = null;
    this._publishDraftDelegate = null;
    this._saveCampaignDraftDelegate = null;
    this._deleteCampaignClickDelegate = null;
    this._deleteCampaignDelegate = null;

    this._dataBoundDelegate = null;
    this._campaignWizardDialogClosedDelegate = null;
    this._saveAndSendCampaignDelegate = null;
    this._publishDraftAndSendCampaignDelegate = null;
    this._scheduleCampaignDelegate = null;
    this._publishDraftAndScheduleCampaignDelegate = null;
    this._clientLabelManager = null;
    this._deleteConfirmationDialog = null;

    this._abTestDetailViewDialog = null;
    this._showAbTestDetailDialog = null;

    this._isAbTestIssue = null;
    this._isBIssue = null;
    this._abTestId = null;
    this._sendAbTestIssueClickDelegate = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension.callBaseMethod(this, 'initialize');

        this._appLoadDelegate = Function.createDelegate(this, this._appLoadHandler);
        this._sendCampaignDelegate = Function.createDelegate(this, this._sendCampaign);
        this._sendTestDelegate = Function.createDelegate(this, this._sendTestHandler);
        this._sendTestCallbackDelegate = Function.createDelegate(this, this._sendTestCallback);
        this._createAbTestDelegate = Function.createDelegate(this, this._createAbTestHandler);
        this._scheduleCampaignDialogClosedDelegate = Function.createDelegate(this, this._scheduleCampaignDialogClosed);
        this._scheduleCampaignDialogOpenDelegate = Function.createDelegate(this, this._scheduleCampaignDialogOpen);
        this._operationFailureDelegate = Function.createDelegate(this, this._operationFailureHandler);
        this._campaignWizardDialogShowDelegate = Function.createDelegate(this, this._campaignWizardDialogShowHandler);
        this._showTitleAndPropertiesDelegate = Function.createDelegate(this, this._showTitleAndProperties);
        this._campaignWizardDialogLoadedDelegate = Function.createDelegate(this, this._campaignWizardDialogLoadedHandler);
        this._publishDraftDelegate = Function.createDelegate(this, this._publishDraft);
        this._saveCampaignDraftDelegate = Function.createDelegate(this, this._saveCampaignDraft);

        this._dataBoundDelegate = Function.createDelegate(this, this._dataBoundHandler);
        this._campaignWizardDialogClosedDelegate = Function.createDelegate(this, this._campaignWizardDialogClosedHandler);
        this._saveAndSendCampaignDelegate = Function.createDelegate(this, this._saveAndSendCampaign);
        this._publishDraftAndSendCampaignDelegate = Function.createDelegate(this, this._publishDraftAndSendCampaign);
        this._scheduleCampaignDelegate = Function.createDelegate(this, this._scheduleCampaign);
        this._publishDraftAndScheduleCampaignDelegate = Function.createDelegate(this, this._publishDraftAndScheduleCampaign);
        this._deleteCampaignClickDelegate = Function.createDelegate(this, this._deleteCampaignClick);
        this._deleteCampaignDelegate = Function.createDelegate(this, this._deleteCampaign);

        Sys.Application.add_load(this._appLoadDelegate);
        if (this.get_scheduleDeliveryWindow()) {
            this.get_scheduleDeliveryWindow().add_close(this._scheduleCampaignDialogClosedDelegate);
        }
        if (this.get_campaignWizardDialog()) {
            this.get_campaignWizardDialog().add_pageLoad(this._campaignWizardDialogLoadedDelegate);
            this.get_campaignWizardDialog().add_show(this._campaignWizardDialogShowDelegate);
            this.get_campaignWizardDialog().add_close(this._campaignWizardDialogClosedDelegate);
        }

        this._attachAbTestDetailViewEventHandlers();

        if (this._isAbTestIssue) {
            this._sendAbTestClickDelegate = Function.createDelegate(this, this._sendAbTestClickHandler);
        }
    },

    dispose: function () {
        if (this._appLoadDelegate) {
            Sys.Application.remove_load(this._appLoadDelegate);
            delete this._appLoadDelegate;
        }

        if (this._sendCampaignDelegate) {
            delete this._sendCampaignDelegate;
        }

        if (this._sendTestDelegate) {
            delete this._sendTestDelegate;
        }

        if (this._sendTestCallbackDelegate) {
            delete this._sendTestCallbackDelegate;
        }

        if (this._createAbTestDelegate) {
            delete this._createAbTestDelegate;
        }

        if (this._scheduleCampaignDialogClosedDelegate) {
            if (this.get_scheduleDeliveryWindow()) {
                this.get_scheduleDeliveryWindow().remove_close(this._scheduleCampaignDialogClosedDelegate);
            }
            delete this._scheduleCampaignDialogClosedDelegate;
        }

        if (this._scheduleCampaignDialogOpenDelegate) {
            delete this._scheduleCampaignDialogOpenDelegate;
        }

        if (this._campaignWizardDialogShowDelegate) {
            if (this.get_campaignWizardDialog()) {
                this.get_campaignWizardDialog().remove_show(this._campaignWizardDialogShowDelegate);
            }
            delete this._campaignWizardDialogShowDelegate;
        }

        if (this._campaignWizardDialogLoadedDelegate) {
            if (this.get_campaignWizardDialog()) {
                this.get_campaignWizardDialog().remove_pageLoad(this._campaignWizardDialogLoadedDelegate);
            }
            delete this._campaignWizardDialogLoadedDelegate;
        }

        if (this._publishDraftDelegate) {
            delete this._publishDraftDelegate;
        }

        if (this._saveCampaignDraftDelegate) {
            delete this._saveCampaignDraftDelegate;
        }

        if (this._dataBoundDelegate) {
            delete this._dataBoundDelegate;
        }

        if (this._campaignWizardDialogClosedDelegate) {
            if (this.get_campaignWizardDialog()) {
                this.get_campaignWizardDialog().remove_close(this._campaignWizardDialogClosedDelegate);
            }
            delete this._campaignWizardDialogClosedDelegate;
        }

        if (this._saveAndSendCampaignDelegate) {
            delete this._saveAndSendCampaignDelegate;
        }

        if (this._publishDraftAndSendCampaignDelegate) {
            delete this._publishDraftAndSendCampaignDelegate;
        }

        if (this._scheduleCampaignDelegate) {
            delete this._scheduleCampaignDelegate;
        }

        if (this._publishDraftAndScheduleCampaignDelegate) {
            delete this._publishDraftAndScheduleCampaignDelegate;
        }

        if (this._deleteCampaignClickDelegate) {
            delete this._deleteCampaignClickDelegate;
        }

        if (this._deleteCampaignDelegate) {
            delete this._deleteCampaignDelegate;
        }

        if (this._sendAbTestClickDelegate) {
            delete this._sendAbTestClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension.callBaseMethod(this, 'dispose');
    },

    _appLoadHandler: function () {
        this._getCurrentIssue(this._dataBoundDelegate);

        this._toolBar = editorToolBar;
        if (this._isAbTestIssue) {
            this._toolBar.sendCampaign = this._sendAbTestClickDelegate;
        }
        else {
            this._toolBar.sendCampaign = this._saveAndSendCampaignDelegate;
        }
        this._toolBar.sendTest = this._sendTestDelegate;
        this._toolBar.createAbTest = this._createAbTestDelegate;
        this._toolBar.scheduleCampaign = this._scheduleCampaignDialogOpenDelegate;
        this._toolBar.showTitleAndProperties = this._showTitleAndPropertiesDelegate;
        this._toolBar.saveCampaignDraft = this._saveCampaignDraftDelegate;
        this._toolBar.deleteCampaign = this._deleteCampaignClickDelegate;
    },

    _deleteCampaignClick: function (sender, args) {
        this.get_deleteConfirmationDialog().show_prompt(null, null, this._deleteCampaignDelegate, this);
    },

    _deleteCampaign: function (sender, eventargs) {
        if (eventargs.get_commandName() == "ok") {
            var that = this;
            jQuery.ajax({
                type: 'DELETE',
                url: this.get_campaignServiceUrl() + String.format("/{0}/", this._campaignId),
                contentType: "application/json",
                processData: false,
                success:
                        function (data, textStatus, jqXHR) {
                            that._toolBar._deleteItemSuccess();
                        },
                error: this._operationFailureDelegate
            });
        }
    },

    _campaignWizardDialogClosedHandler: function (sender, args) {
        this._getCurrentIssue();
    },

    _dataBoundHandler: function () {
        if (plainTextEditor) {
            plainTextEditor.dataBind(this._campaign);
        }
    },

    _campaignWizardDialogLoadedHandler: function (sender, args) {
        this._campaignWizardDialogShowHandler(sender, args);
    },

    _campaignWizardDialogShowHandler: function (sender, args) {
        if (this.get_campaignWizardDialog().get_contentFrame().contentWindow.setForm) {
            this.get_campaignWizardDialog().get_contentFrame().contentWindow.setForm('content', this._campaign.Id, "issue", false);
        }
    },

    _operationFailureHandler: function (jqXHR, textStatus, errorThrown) {
        this._toolBar.raiseNonParallelRequestCompleted();
        alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    _getCampaignObject: function () {
        if (!this._campaign) {
            return null;
        }

        var campaign = {
            Id: this._campaign.Id,
            Name: this._campaign.Name,
            CampaignState: this._campaign.CampaignState,
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

    _saveCampaignDraft: function () {
        this._saveCampaign(this._publishDraftDelegate);
    },

    _saveCampaign: function (onSuccess) {
        if (this._campaign) {
            if (plainTextEditor) {
                this._campaign = plainTextEditor.updateCampaign(this._campaign);
            }
            var id = (this._campaign.Id) ? (this._campaign.Id) : this.EMPTY_GUID;

            jQuery.ajax({
                type: 'PUT',
                url: this.get_campaignServiceUrl() + String.format("/{0}/", id),
                contentType: "application/json",
                processData: false,
                data: Telerik.Sitefinity.JSON.stringify(this._getCampaignObject()),
                success:
                    function (data, textStatus, jqXHR) {
                        if (typeof (onSuccess) == "function") {
                            onSuccess();
                        }
                    },
                error: this._operationFailureDelegate
            });
        }
    },

    _saveAndSendCampaign: function () {
        this.get_sendIssuePrompt().set_title(this.get_clientLabelManager().getLabel('NewslettersResources', 'SendIssueFor') + ' ' + this._campaign.RootCampaignName);
        this.get_sendIssuePrompt().set_message(String.format(this.get_clientLabelManager().getLabel('NewslettersResources', 'SendIssuePromptText'), this._campaign.ListSubscriberCount));

        var that = this;
        var promptCallback = function (sender, args) {
            if (args.get_commandName() == "send") {
                that._saveCampaign(that._publishDraftAndSendCampaignDelegate);
            }
        }

        this.get_sendIssuePrompt().show_prompt(null, null, promptCallback);
    },

    _saveAndScheduleCampaign: function () {
        this._saveCampaign(this._publishDraftAndScheduleCampaignDelegate);
    },

    _publishDraftAndSendCampaign: function () {
        this._publishDraft(this._sendCampaignDelegate);
    },

    _publishDraftAndScheduleCampaign: function () {
        this._publishDraft(this._scheduleCampaignDelegate);
    },

    _publishDraft: function (onSuccess) {
        if (typeof (onSuccess) != "function") {
            onSuccess = this._toolBar._publishDraftSuccessDelegate;
        }

        var url = this._toolBar.get_serviceUrl();

        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }
        
        url += "Page/Publish/" + this._toolBar.get_draftId() + "/";
        this._toolBar.raiseNonParallelRequestInvoked();
        this._toolBar.get_clientManager().InvokePut(url, null, null, null, onSuccess, this._toolBar._operationFailureDelegate);
    },

    _sendTestHandler: function () {
        this.get_sendTestPrompt().set_inputText("");
        this.get_sendTestPrompt().show_prompt(null, null, this._sendTestCallbackDelegate);
    },

    _sendTestCallback: function (sender, args) {
        if (args.get_commandName() == "sendTest") {
            if (plainTextEditor) {
                this._campaign = plainTextEditor.updateCampaign(this._campaign);
            }

            var emailString = sender.get_inputText().split(',');
            if (this._validateEmailAddresses(emailString)) {
                jQuery.ajax({
                    type: 'PUT',
                    url: this.get_campaignServiceUrl() + '/sendtest/',
                    contentType: "application/json",
                    processData: false,
                    data: Telerik.Sitefinity.JSON.stringify({ campaign: this._getCampaignObject(), testEmailAddresses: emailString }),
                    error: this._operationFailureDelegate
                });
            }
        }
    },

    _createAbTestHandler: function () {
        var that = this;
        var publishDraftAndCreateAbTest = function () {
            that._publishDraft(function () {
                that._createAbTest();
            });
        }
        this._saveCampaign(publishDraftAndCreateAbTest);
    },

    _createAbTest: function () {
        var viewParams = {
            origin: "campaigns",
            campaignId: this._campaign.RootCampaignId,
            issueAId: this._campaign.Id,
            issueAName: this._campaign.Name
        };
        var that = this;
        this._showAbTestDetailDialog("middle", viewParams, function (sender, args) { that._toolBar._publishDraftSuccess() });
    },

    _validateEmailAddresses: function (emailString) {
        if (!emailString.length > 0) {
            alert(this.get_clientLabelManager().getLabel('NewslettersResources', 'EnterAtLeastOneTestEmailAddress'));
            return false;
        }

        // Consider using <see cref="Telerik.Sitefinity.Constants.EmailAddressRegexPattern"/>
        var emailAddressRegexPattern = /[a-zA-Z0-9._%+-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}/i;

        for (var i = 0; i < emailString.length; i++) {
            if (!emailAddressRegexPattern.test(emailString[i])) {
                alert(this.get_clientLabelManager().getLabel('ErrorMessages', 'EmailAddressViolationMessage'));
                return false;
            }
        }
        return true;
    },

    _scheduleCampaignDialogClosed: function (sender, args) {
        var deliveryDate = args.get_data();
        if (deliveryDate) {
            this._scheduledDeliveryDate = new Date(deliveryDate);
            this._saveAndScheduleCampaign();
        }
    },

    _scheduleCampaign: function () {
        var that = this;
        jQuery.ajax({
            type: 'PUT',
            url: this.get_campaignServiceUrl() + String.format("/schedule/{0}/", this._campaignId),
            contentType: "application/json",
            processData: false,
            data: Sys.Serialization.JavaScriptSerializer.serialize(this._scheduledDeliveryDate),
            success:
                    function (data, textStatus, jqXHR) {
                        that._toolBar._publishDraftSuccess();
                    },
            error: this._operationFailureDelegate
        });
    },

    _scheduleCampaignDialogOpen: function () {
        this.get_scheduleDeliveryWindow().reset();
        this.get_scheduleDeliveryWindow().show();
    },

    _sendCampaign: function () {
        var that = this;
        jQuery.ajax({
            type: 'PUT',
            url: this.get_campaignServiceUrl() + '/send/',
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(this._campaignId),
            success:
                function (data, textStatus, jqXHR) {
                    that._toolBar._publishDraftSuccessDelegate();
                },
            error: this._operationFailureDelegate
        });
    },

    _showTitleAndProperties: function () {
        if (this._campaign) {
            if (!this._isAbTestIssue) {
                this.get_campaignWizardDialog().show();
                this.get_campaignWizardDialog().maximize();
            }
            else {
                var viewParams = {
                    isCreateMode: false,
                    isBIssue: this._isBIssue,
                    issueId: this.get_campaignId(),
                    origin: "content"
                };
                this._showAbTestDetailDialog("properties", viewParams, this._campaignWizardDialogClosedDelegate);
            }
        }
    },

    _getCurrentIssue: function (onSuccess) {
        var that = this;
        jQuery.ajax({
            type: 'GET',
            url: this.get_campaignServiceUrl() + String.format("/issue/{0}/", this._campaignId),
            contentType: "application/json",
            processData: false,
            success:
                function (data, textStatus, jqXHR) {
                    that._campaign = data;
                    jQuery(".sfPageTitle").html(that._campaign.Name.htmlEncode());
                    if (typeof (onSuccess) == "function") {
                        onSuccess();
                    }
                },
            error: this._operationFailureDelegate
        });
    },

    _attachAbTestDetailViewEventHandlers: function () {
        var persistedCall = {
        };

        var that = this;
        this._showAbTestDetailDialog = function (view, viewParams, onClose) {
            persistedCall.view = view;
            persistedCall.viewParams = viewParams;
            persistedCall.onClose = onClose;

            that.get_abTestDetailViewDialog().show();
            that.get_abTestDetailViewDialog().maximize();
        };

        this.get_abTestDetailViewDialog().add_close(function (sender, args) {
            persistedCall.onClose(sender, args);
        });

        var abTestDetailViewDialogShowHandler = function (sender, args) {
            if (that.get_abTestDetailViewDialog().get_contentFrame().contentWindow.setForm) {
                var zoneEditor = $find(that._toolBar._zoneEditorId);
                if (zoneEditor) {
                    zoneEditor.set_isChangeMade(false);
                    zoneEditor.set_isUnlockingDone(true);
                }
                that.get_abTestDetailViewDialog().get_contentFrame().contentWindow.setForm(persistedCall.view, persistedCall.viewParams);
            }
        };

        this.get_abTestDetailViewDialog().add_show(abTestDetailViewDialogShowHandler);
        this.get_abTestDetailViewDialog().add_pageLoad(abTestDetailViewDialogShowHandler);
    },

    _sendAbTestClickHandler: function (sender, args) {
        var that = this;
        var onSuccess = function () {
            var viewParams = {
                abTestId: that._abTestId
            };
            that._publishDraft(function () {
                that._showAbTestDetailDialog("send", viewParams, function (sender, args) {
                    var jBody = jQuery(document.body);
                    jBody.removeClass("sfPageEditor");
                    jBody.addClass("sfLoadingTransition");
                    that._toolBar._publishDraftSuccess();
                });
            });
        };
        this._saveCampaign(onSuccess);
    },

    get_campaignServiceUrl: function () {
        return this._campaignServiceUrl;
    },
    set_campaignServiceUrl: function (value) {
        this._campaignServiceUrl = value;
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
    get_campaignId: function () {
        return this._campaignId;
    },
    set_campaignId: function (value) {
        this._campaignId = value;
    },
    get_scheduleDeliveryWindow: function () {
        return this._scheduleDeliveryWindow;
    },
    set_scheduleDeliveryWindow: function (value) {
        this._scheduleDeliveryWindow = value;
    },
    get_campaignWizardDialog: function () {
        return this._campaignWizardDialog;
    },
    set_campaignWizardDialog: function (value) {
        this._campaignWizardDialog = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_deleteConfirmationDialog: function () {
        return this._deleteConfirmationDialog;
    },
    set_deleteConfirmationDialog: function (value) {
        this._deleteConfirmationDialog = value;
    },
    get_abTestDetailViewDialog: function () {
        return this._abTestDetailViewDialog;
    },
    set_abTestDetailViewDialog: function (value) {
        this._abTestDetailViewDialog = value;
    }
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension', Sys.Component);