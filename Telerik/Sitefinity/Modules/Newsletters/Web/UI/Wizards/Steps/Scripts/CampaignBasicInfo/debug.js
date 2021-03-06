Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo = function (element) {

    this._nameField = null;
    this._fromNameField = null;
    this._replyToEmailField = null;
    this._messageSubjectField = null;
    this._listWebServiceUrl = null;
    this._selectedListId = null;
    this._existingCampaign = null;
    this._useGoogleTrackingField = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo.initializeBase(this, [element]);
}
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    loadDefaults: function (list) {
        //if later we decide not to change the mailing list for a campaign - check should be added whether this._existingCampaign is null
        this.get_fromNameField().set_value(list.DefaultFromName);
        this.get_replyToEmailField().set_value(list.DefaultReplyToEmail);
        this.get_messageSubjectField().set_value(list.DefaultSubject);
        this._selectedListId = list.Id;
        this.get_nameField().focus();
    },

    isValid: function () {
        var stepIsValid = true;

        if (this.get_nameField().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_fromNameField().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_replyToEmailField().validate() == false) {
            stepIsValid = false;
        }
        if (this.get_messageSubjectField().validate() == false) {
            stepIsValid = false;
        }

        return stepIsValid;
    },

    reset: function () {
        this.get_nameField().set_value('');
        this.get_fromNameField().set_value('');
        this.get_replyToEmailField().set_value('');
        this.get_messageSubjectField().set_value('');
        this.get_nameField().focus();
        this._existingCampaign = null;
        this._selectedListId = null;
    },

    setBasicInfo: function (campaign) {
        this._existingCampaign = campaign;
        this.get_nameField().set_value(this._existingCampaign.Name);
        this.get_fromNameField().set_value(this._existingCampaign.FromName);
        this.get_replyToEmailField().set_value(this._existingCampaign.ReplyToEmail);
        this.get_messageSubjectField().set_value(this._existingCampaign.MessageSubject);
    },

    /* *************************** private methods *************************** */

    /* *************************** properties *************************** */
    get_nameField: function () {
        return this._nameField;
    },
    set_nameField: function (value) {
        this._nameField = value;
    },
    get_fromNameField: function () {
        return this._fromNameField;
    },
    set_fromNameField: function (value) {
        this._fromNameField = value;
    },
    get_replyToEmailField: function () {
        return this._replyToEmailField;
    },
    set_replyToEmailField: function (value) {
        this._replyToEmailField = value;
    },
    get_messageSubjectField: function () {
        return this._messageSubjectField;
    },
    set_messageSubjectField: function (value) {
        this._messageSubjectField = value;
    },
    get_name: function () {
        return this.get_nameField().get_value();
    },
    get_fromName: function () {
        return this.get_fromNameField().get_value();
    },
    get_replyToEmail: function () {
        return this.get_replyToEmailField().get_value();
    },
    get_messageSubject: function () {
        return this.get_messageSubjectField().get_value();
    },
    get_selectedListId: function () {
        return this._selectedListId;
    },
    get_listWebServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_listWebServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_useGoogleTracking: function () {
        return $(this.get_useGoogleTrackingField()).is(':checked');
    },
    get_useGoogleTrackingField: function () {
        return this._useGoogleTrackingField;
    },
    set_useGoogleTrackingField: function (value) {
        this._useGoogleTrackingField = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.CampaignBasicInfo', Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl);