Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");

/* ABCampaignForm class */

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm = function (element) {
    this._webServiceUrl = null;
    this._abTestFormTitle = null;
    this._campaignBMatrix = null;
    this._backLink = null;
    this._nameTextField = null;
    this._campaignAChoiceField = null;
    this._campaignBChoiceField = null;
    this._winningFactorChoiceField = null;
    this._testSampleSlider = null;
    this._testingSamplePercentageLabel = null;
    this._testingPeriodEndPicker = null;
    this._commandBar = null;
    this._clientLabelManager = null;

    this._editedCampaignId = null;
    this._campaignAChangedDelegate = null;
    this._commandBarCommandDelegate = null;
    this._testingSampleSliderValueChangedDelegate = null;
    this._getCampaignSuccessDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm.prototype = {

    // set up 
    initialize: function () {

        this._campaignBMatrix = Telerik.Sitefinity.JSON.parse(this._campaignBMatrix);

        $(this._backLink).click(function () {
            dialogBase.close();
        });

        if (this._campaignAChangedDelegate === null) {
            this._campaignAChangedDelegate = Function.createDelegate(this, this._handleCampaignAChanged)
        }
        if (this._campaignAChoiceField) {
            this._campaignAChoiceField.add_valueChanged(this._campaignAChangedDelegate);
        }

        if (this._commandBarCommandDelegate === null) {
            this._commandBarCommandDelegate = Function.createDelegate(this, this._handleCommandBarCommand);
        }
        if (this._commandBar != null) {
            this._commandBar.add_command(this._commandBarCommandDelegate);
        }

        if (this._testingSampleSliderValueChangedDelegate === null) {
            this._testingSampleSliderValueChangedDelegate = Function.createDelegate(this, this._handleTestingSampleSliderSliding);
        }
        if (this._testSampleSlider) {
            this._testSampleSlider.add_valueChanged(this._testingSampleSliderValueChangedDelegate);
        }

        this._getCampaignSuccessDelegate = Function.createDelegate(this, this._getCampaignSuccessHandler);

        this._handleCampaignAChanged();

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._campaignAChangedDelegate) {
            this._campaignAChoiceField.remove_valueChanged(this._campaignAChangedDelegate);
            delete this._campaignAChangedDelegate;
        }

        if (this._commandBarCommandDelegate) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }

        if (this._testingSampleSliderValueChangedDelegate) {
            this._testSampleSlider.remove_valueChanged(this._testingSampleSliderValueChangedDelegate);
            delete this._testingSampleSliderValueChangedDelegate;
        }

        if (this._getCampaignSuccessDelegate) {
            delete this._getCampaignSuccessDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    loadCampaign: function (campaign) {
        this._clearForm();
        this._updateTitle(campaign);
        if (campaign != null) {
            this._editedCampaignId = campaign.Id;
            this._getCampaign(this._editedCampaignId);
        }
    },

    /* *************************** private methods *************************** */

    _handleCampaignAChanged: function () {
        this._campaignBChoiceField.clearListItems();
        var campaignA = this._campaignAChoiceField.get_value();
        if (campaignA.length > 0) {
            var compatibleCampaigns = this._campaignBMatrix[campaignA];
            var compatibleCampaignsCount = compatibleCampaigns.length;
            while (compatibleCampaignsCount > 0) {
                compatibleCampaignsCount--;
                if (this._campaignAChoiceField.get_value() != compatibleCampaigns[compatibleCampaignsCount].Id) {
                    this._campaignBChoiceField.addListItem(compatibleCampaigns[compatibleCampaignsCount].Id, compatibleCampaigns[compatibleCampaignsCount].Name);
                }
            }
        }
    },

    _handleCommandBarCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case 'cancel':
                dialogBase.close();
                break;
            case 'save':
                this._saveCampaign();
                break;
            default:
                alert('Command "' + args.CommandName + '" is not supported.');
        }
    },

    _handleTestingSampleSliderSliding: function (sender, args) {
        $(this._testingSamplePercentageLabel).html(sender.get_value() + ' %');
    },

    _saveCampaign: function () {
        if (this._isFormValid()) {
            var campaignObject = this._getCampaignObject();
            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            var urlParams = [];
            var keys = [(this._editedCampaignId == null) ? clientManager.GetEmptyGuid() : this._editedCampaignId];

            clientManager.InvokePut(this._webServiceUrl, urlParams, keys, campaignObject, this._saveCampaign_Success, this._saveCampaign_Failure, this);
        }
    },

    _saveCampaign_Success: function (sender, result, args) {
        dialogBase.closeCreated();
    },

    _saveCampaign_Failure: function (sender, args) {
        alert('There was a problem saving A/B campaign.');
    },

    _getCampaignObject: function () {

        var campaignObject = {
            'Name': this._nameTextField.get_value(),
            'CampaignAId': this._campaignAChoiceField.get_value(),
            'CampaignBId': this._campaignBChoiceField.get_value(),
            'WinningCondition': this._winningFactorChoiceField.get_value(),
            'TestingSamplePercentage': this._testSampleSlider.get_value(),
            'TestingEnds': this._testingPeriodEndPicker.get_selectedDate()
        };

        return campaignObject;
    },

    _clearForm: function () {
        this._nameTextField.reset();
        this._editedCampaignId = null;
        this._campaignBChoiceField.clearListItems();
        this._campaignAChoiceField.set_selectedChoicesIndex(0);
        this._handleCampaignAChanged();
        this._winningFactorChoiceField.set_value(0);
        this._testSampleSlider.set_value(1);
        var currentDate = new Date();
        currentDate.setDate(currentDate.getDate() + 5);
        this._testingPeriodEndPicker.set_selectedDate(currentDate);
    },

    _updateTitle: function (campaign) {
        var title = null;
        if (campaign != null) {
            title = this.get_clientLabelManager().getLabel("NewslettersResources", "EditABCampaign");
        } else {
            title = this.get_clientLabelManager().getLabel("NewslettersResources", "CreateABCampaign");
        }
        this.get_abTestFormTitle().innerHTML = title;
    },

    _isFormValid: function () {
        return this.get_nameTextField().validate();
    },

    _getCampaign: function (campaignId) {
        jQuery.ajax({
            type: 'GET',
            url: this._webServiceUrl + '/' + campaignId + '/',
            contentType: "application/json",
            processData: false,
            success: this._getCampaignSuccessDelegate
        });
    },

    _getCampaignSuccessHandler: function (result, args) {
        var campaign = result;

        this._nameTextField.set_value(campaign.Name);
        this._campaignAChoiceField.set_value(campaign.CampaignAId);
        this._campaignBChoiceField.set_value(campaign.CampaignBId);
        this._winningFactorChoiceField.set_value(campaign.WinningCondition);
        this._testSampleSlider.set_value(campaign.TestingSamplePercentage);

        var dateRegExp = /^\/Date\((.*?)\)\/$/;
        var dateMatch = dateRegExp.exec(campaign.TestingEnds);
        if (dateMatch) {
            this._testingPeriodEndPicker.set_selectedDate(new Date(parseInt(dateMatch[1])));
        }
        else {
            this._testingPeriodEndPicker.set_selectedDate(campaign.TestingEnds);
        }
    },

    /* *************************** properties *************************** */

    // gets the reference to the back link
    get_backLink: function () {
        return this._backLink;
    },
    // sets the reference to the back link
    set_backLink: function (value) {
        this._backLink = value;
    },
    // gets the reference to the ab test form title label
    get_abTestFormTitle: function () {
        return this._abTestFormTitle;
    },
    // sets the reference to the ab test form title label
    set_abTestFormTitle: function (value) {
        this._abTestFormTitle = value;
    },
    // gets the reference to the name text field
    get_nameTextField: function () {
        return this._nameTextField;
    },
    // sets the reference to the name text field
    set_nameTextField: function (value) {
        this._nameTextField = value;
    },
    // gets the reference to the campaign A choice field
    get_campaignAChoiceField: function () {
        return this._campaignAChoiceField;
    },
    // sets the reference to the campaign A choice field
    set_campaignAChoiceField: function (value) {
        this._campaignAChoiceField = value;
    },
    // gets the reference to the campaign B choice field
    get_campaignBChoiceField: function () {
        return this._campaignBChoiceField;
    },
    // sets the reference to the campaign B choice field
    set_campaignBChoiceField: function (value) {
        this._campaignBChoiceField = value;
    },
    // gets the reference to the winning factor choice field
    get_winningFactorChoiceField: function () {
        return this._winningFactorChoiceField;
    },
    // sets the reference to the winning factor choice field
    set_winningFactorChoiceField: function (value) {
        this._winningFactorChoiceField = value;
    },
    // sets the reference to the test sample slider
    get_testSampleSlider: function () {
        return this._testSampleSlider;
    },
    // sets the reference to the test sample slider
    set_testSampleSlider: function (value) {
        this._testSampleSlider = value;
    },
    // gets the reference to the testing sample percentage label
    get_testingSamplePercentageLabel: function () {
        return this._testingSamplePercentageLabel;
    },
    // sets the reference to the testing sample percentage label
    set_testingSamplePercentageLabel: function (value) {
        this._testingSamplePercentageLabel = value;
    },
    // gets the reference to the test period end picker
    get_testingPeriodEndPicker: function () {
        return this._testingPeriodEndPicker;
    },
    // sets the reference to the test period end picker
    set_testingPeriodEndPicker: function (value) {
        this._testingPeriodEndPicker = value;
    },
    // gets the reference to the command bar
    get_commandBar: function () {
        return this._commandBar;
    },
    // sets the reference to the command bar
    set_commandBar: function (value) {
        this._commandBar = value;
    },
    // gets the reference to the client label manager
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    // sets the reference to the client label manager
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ABCampaignForm', Telerik.Sitefinity.Web.UI.AjaxDialogBase);