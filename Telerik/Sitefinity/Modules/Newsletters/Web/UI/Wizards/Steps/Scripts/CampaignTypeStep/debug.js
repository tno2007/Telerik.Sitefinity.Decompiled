Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep = function (element) {
    this._htmlCampaignRadio = null;
    this._plainTextCampaignRadio = null;
    this._standardCampaignRadio = null;
    this._standardCampaignOptions = null;
    this._standardCampaignInternalPageRadio = null;
    this._standardCampaignExternalPageRadio = null;
    this._createFromTemplateRadio = null;
    this._createFromScratchRadio = null;
    this._fromScratchContainer = null;
    this._templatesChoiceField = null;
    this._campaignTemplateId = null;

    this._campaignRadioClickDelegate = null;
    this._createTemplateRadioClickDelegate = null;
    this._createScratchRadioClickDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep.prototype = {

    // set up 
    initialize: function () {

        if (this._campaignRadioClickDelegate === null) {
            this._campaignRadioClickDelegate = Function.createDelegate(this, this._campaignRadioClickHandler);
        }
        if (this._createTemplateRadioClickDelegate === null) {
            this._createTemplateRadioClickDelegate = Function.createDelegate(this, this._createTemplateRadioClickHandler);
        }
        if (this._createScratchRadioClickDelegate === null) {
            this._createScratchRadioClickDelegate = Function.createDelegate(this, this._createScratchRadioClickHandler);
        }

        $addHandler(this._htmlCampaignRadio, 'click', this._campaignRadioClickDelegate);
        $addHandler(this._plainTextCampaignRadio, 'click', this._campaignRadioClickDelegate);
        $addHandler(this._standardCampaignRadio, 'click', this._campaignRadioClickDelegate);
        $addHandler(this._standardCampaignInternalPageRadio, 'click', this._campaignRadioClickDelegate);
        $addHandler(this._standardCampaignExternalPageRadio, 'click', this._campaignRadioClickDelegate);
        $addHandler(this._createFromTemplateRadio, 'click', this._createTemplateRadioClickDelegate);
        $addHandler(this._createFromScratchRadio, 'click', this._createScratchRadioClickDelegate);

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._campaignRadioClickDelegate) {
            delete this._campaignRadioClickDelegate;
        }
        else if (this._createScratchRadioClickDelegate) {
            delete this._createScratchRadioClickDelegate;
        }
        else if (this._createTemplateRadioClickDelegate) {
            delete this._createTemplateRadioClickDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    add_typeChanged: function (delegate) {
        this.get_events().addHandler('onTypeChanged', delegate);
    },
    remove_typeChanged: function (delegate) {
        this.get_events().removeHandler('onTypeChanged', delegate);
    },

    add_fromScratchRadioClick: function (delegate) {
        this.get_events().addHandler('fromScratchRadioClick', delegate);
    },
    remove_fromScratchRadioClick: function (delegate) {
        this.get_events().removeHandler('fromScratchRadioClick', delegate);
    },

    isValid: function () {
        if (this.get_autoSaved()) {
            return true;
        }

        return false;
    },

    reset: function () {
        this.get_templatesChoiceField().reset();
        $(this._htmlCampaignRadio).attr('checked', 'checked');
    },

    /* *************************** private methods *************************** */
    _campaignRadioClickHandler: function (sender, args) {
        // show / hide standard campaign options
        if ($(this._standardCampaignRadio).is(':checked')) {
            $(this._standardCampaignOptions).hide(); // THIS IS ONLY TEMPORARY!!!
        } else {
            $(this._standardCampaignOptions).hide();
        }

        // rise the type changed event
        if ($(this._plainTextCampaignRadio).is(':checked')) {
            this._typeChangedHandler(0);
        } else if ($(this._htmlCampaignRadio).is(':checked')) {
            this._typeChangedHandler(1);
        } else if ($(this._standardCampaignRadio).is(':checked')) {
            if ($(this._standardCampaignInternalPageRadio).is(':checked')) {
                this._typeChangedHandler(2);
            } else if ($(this._standardCampaignExternalPageRadio).is(':checked')) {
                this._typeChangedHandler(3);
            }
        }
    },

    _createTemplateRadioClickHandler: function (sender, args) {
        if ($(this._createFromTemplateRadio).is(':checked')) {
            $(this._fromScratchContainer).hide();
        } else {
            $(this._fromScratchContainer).show();
        }
    },

    _createScratchRadioClickHandler: function (sender, args) {
        if ($(this._createFromScratchRadio).is(':checked')) {
            $(this._fromScratchContainer).show();
        } else {
            $(this._fromScratchContainer).hide();
        }

        this._fromScratchRadioClickHandler(sender, args);
    },

    _fromScratchRadioClickHandler: function (sender, args) {
        var h = this.get_events().getHandler('fromScratchRadioClick');
        if (h) h(this, args);
        return args;
    },

    _typeChangedHandler: function (campaignType) {
        var eventArgs = { 'CampaignType': campaignType };
        var h = this.get_events().getHandler('onTypeChanged');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    _resetCampaignTemplate: function () {
        this._campaignTemplateId = '00000000-0000-0000-0000-000000000000';
    },

    /* *************************** properties *************************** */
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
    get_standardCampaignOptions: function () {
        return this._standardCampaignOptions;
    },
    set_standardCampaignOptions: function (value) {
        this._standardCampaignOptions = value;
    },
    get_standardCampaignInternalPageRadio: function () {
        return this._standardCampaignInternalPageRadio;
    },
    set_standardCampaignInternalPageRadio: function (value) {
        this._standardCampaignInternalPageRadio = value;
    },
    get_standardCampaignExternalPageRadio: function () {
        return this._standardCampaignExternalPageRadio;
    },
    set_standardCampaignExternalPageRadio: function (value) {
        this._standardCampaignExternalPageRadio = value;
    },
    get_campaignType: function () {
        var createFromTemplate = $(this.get_createFromTemplateRadio()).is(':checked');
        if (createFromTemplate) {
            return null;
        }
        else {
            if ($(this._htmlCampaignRadio).is(':checked')) {
                return 1;
            } else if ($(this._plainTextCampaignRadio).is(':checked')) {
                return 0;
            } else if ($(this._standardCampaignRadio).is(':checked')) {
                if ($(this._standardCampaignInternalPageRadio).is(':checked')) {
                    return 2;
                } else if ($(this._standardCampaignExternalPageRadio).is(':checked')) {
                    return 3;
                }
            }
        }
        return null;
    },
    set_campaignType: function (campaignType) {
        switch (campaignType) {
            case 0:
                $(this._plainTextCampaignRadio).attr('checked', 'checked');
                break;
            case 1:
                $(this._htmlCampaignRadio).attr('checked', 'checked');
                break;
            case 2:
                $(this._standardCampaignRadio).attr('checked', 'checked');
                break;
        }
        this._typeChangedHandler(campaignType);
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
    get_fromScratchContainer: function () {
        return this._fromScratchContainer;
    },
    set_fromScratchContainer: function (value) {
        this._fromScratchContainer = value;
    },
    get_templatesChoiceField: function () {
        return this._templatesChoiceField;
    },
    set_templatesChoiceField: function (value) {
        this._templatesChoiceField = value;
    },
    get_campaignTemplateId: function () {
        var createFromTemplate = $(this.get_createFromTemplateRadio()).is(':checked');
        if (createFromTemplate) {
            this._campaignTemplateId = this.get_templatesChoiceField().get_value();
            if (this._campaignTemplateId.length < 1) {
                this._campaignTemplateId = '00000000-0000-0000-0000-000000000000';
            }
        } else {
            this._resetCampaignTemplate();
        }
        return this._campaignTemplateId;
    },
    set_campaignTemplateId: function (value) {
        this._campaignTemplateId = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignTypeStep', Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl);