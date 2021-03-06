Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");

/* DynamicMailingListSettings */

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings = function (element) {

    this._dynamicListProviderChoiceField = null;
    this._dynamicListsChoiceField = null;
    this._connectionNameField = null;
    this._firstNameMappedField = null;
    this._lastNameMappedField = null;
    this._emailMappedField = null;
    this._mappingsContainerId = null;
    this._dynamicListProviderChangedDelegate = null;
    this._dynamicListChangedDelegate = null;
    this._dynamicListServiceBaseUrl = null;
    this._availableDynamicLists = null;
    this._commandBar = null;
    this._commandBarCommandDelegate = null;
    this._selectedProvider = "";
    this._messageControl = null;
    this._clientLabelManager = null;
    this._onLoadDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.prototype = {

    // set up 
    initialize: function () {

        if (this._dynamicListProviderChangedDelegate === null) {
            this._dynamicListProviderChangedDelegate = Function.createDelegate(this, this._handleDynamicListProviderChanged);
        }
        this._dynamicListProviderChoiceField.add_valueChanged(this._dynamicListProviderChangedDelegate);

        if (this._dynamicListChangedDelegate === null) {
            this._dynamicListChangedDelegate = Function.createDelegate(this, this._handleDynamicListChanged);
        }
        this._dynamicListsChoiceField.add_valueChanged(this._dynamicListChangedDelegate);

        if (this._commandBarCommandDelegate === null) {
            this._commandBarCommandDelegate = Function.createDelegate(this, this._handleCommandBarCommand);
        }
        this._commandBar.add_command(this._commandBarCommandDelegate);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._dynamicListChangedDelegate) {
            this._dynamicListsChoiceField.remove_valueChanged(this._dynamicListChangedDelegate);
            delete this._dynamicListChangedDelegate;
        }

        if (this._dynamicListProviderChangedDelegate) {
            this._dynamicListProviderChoiceField.remove_valueChanged(this._dynamicListProviderChangedDelegate);
            delete this._dynamicListProviderChangedDelegate;
        }

        if (this._commandBarCommandDelegate) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    /* *************************** private methods *************************** */

    _onLoad: function () {
        jQuery("body").addClass("sfSelectorDialog"); 
        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }
        dialogBase.resizeToContent();
    },

    _handleDynamicListProviderChanged: function (sender, args) {
        this._selectedProvider = sender.get_value();

        if ((this._selectedProvider == null) || (this._selectedProvider.length == 0)) {
            jQuery(this.get_connectionNameField().get_element()).hide();
            jQuery(this.get_dynamicListsChoiceField().get_element()).hide();
            jQuery("#" + this._mappingsContainerId).hide();
        } else {
            jQuery(this.get_connectionNameField().get_element()).show();
            jQuery(this.get_dynamicListsChoiceField().get_element()).show();
            jQuery("#" + this._mappingsContainerId).show();
            this._getDynamicLists(this._selectedProvider);
        }

        dialogBase.resizeToContent();
    },

    _handleDynamicListChanged: function (sender, args) {
        var propertyChoices = new Array();
        var currentList = this.get_selectedDynamicList();
        if (currentList) {
            for (pIter = 0; pIter < currentList.AvailableProperties.length; pIter++) {
                propertyChoices.push({ 'Text': currentList.AvailableProperties[pIter].Title, 'Value': currentList.AvailableProperties[pIter].ComposedTag });
            }

            this.get_connectionNameField().set_value(currentList.Title + ' : ' + currentList.ProviderName);
        }

        this._populateMappingField(this._firstNameMappedField, propertyChoices);
        this._populateMappingField(this._lastNameMappedField, propertyChoices);
        this._populateMappingField(this._emailMappedField, propertyChoices);

        dialogBase.resizeToContent();
    },

    _getDynamicLists: function (selectedProvider) {
        var keys = [selectedProvider];
        var serviceUrl = this._dynamicListServiceBaseUrl + 'info/';
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        clientManager.InvokeGet(
            serviceUrl,
            null,
            keys,
            this._getDynamicLists_Success,
            this._getDynamicLists_Failure,
            this);
        dialogBase.resizeToContent();

    },

    _getDynamicLists_Success: function (sender, result, args) {
        sender._availableDynamicLists = result.Items;
        sender._dynamicListsChoiceField.clearListItems();
        for (lIter = 0; lIter < sender._availableDynamicLists.length; lIter++) {
            sender._dynamicListsChoiceField.addListItem(sender._availableDynamicLists[lIter].Key, sender._availableDynamicLists[lIter].Title);
        }

        jQuery('#' + sender._mappingsContainerId).show();

        sender._handleDynamicListChanged(sender.get_dynamicListsChoiceField(), null);
    },

    _getDynamicListProperties_Failure: function (result, arg) {
        alert(result.Detail);
    },

    _populateMappingField: function (mappedField, choices) {
        mappedField.clearListItems();
        var choicesCount = choices.length;
        while (choicesCount > 0) {
            choicesCount--;
            mappedField.addListItem(choices[choicesCount].Value, choices[choicesCount].Text);
        }
    },

    _getSelectedListKey: function () {
        var valuePair = this.get_dynamicListsChoiceField().get_value().split(':');
        return valuePair[0];
    },

    _getSelectedListProviderName: function () {
        var valuePair = this.get_dynamicListsChoiceField().get_value().split(':');
        return valuePair[1];
    },

    _handleCommandBarCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case 'cancel':
                dialogBase.close();
                break;
            case 'save':
                if (this._selectedProvider == "") {
                    this.get_messageControl().showNegativeMessage(this.get_clientLabelManager().getLabel('NewslettersResources', 'SelectConnectionSourceErrorMessage'));
                }
                else if (this._getSettingsObject() != null) {
                    dialogBase.close(this._getSettingsObject());
                }
                break;
        }
    },

    _getSettingsObject: function () {
        var settings = null;
        if (this.get_selectedDynamicList()) {
            settings = {
                'ConnectionName': this.get_connectionNameField().get_value(),
                'DynamicListProviderName': this.get_selectedDynamicList().ProviderName,
                'ListKey': this.get_selectedDynamicList().Key,
                'FirstNameMappedField': this.get_firstNameMappedField().get_value(),
                'LastNameMappedField': this.get_lastNameMappedField().get_value(),
                'EmailMappedField': this.get_emailMappedField().get_value()
            };
        }
        return settings;
    },

    /* *************************** properties *************************** */

    // gets the reference to the dynamics lists choice field control
    get_dynamicListsChoiceField: function () {
        return this._dynamicListsChoiceField;
    },

    // sets the reference to the dynamics lists choice field control
    set_dynamicListsChoiceField: function (value) {
        this._dynamicListsChoiceField = value;
    },
    // gets the reference to the connection name field
    get_connectionNameField: function () {
        return this._connectionNameField;
    },
    // sets the reference to the connection name field
    set_connectionNameField: function (value) {
        this._connectionNameField = value;
    },
    // gets the reference to the choice field representing the first name mapped field
    get_firstNameMappedField: function () {
        return this._firstNameMappedField;
    },
    // sets the reference to the choice field representing the first name mapped field
    set_firstNameMappedField: function (value) {
        this._firstNameMappedField = value;
    },
    // gets the reference to the choice field representing the last name mapped field
    get_lastNameMappedField: function () {
        return this._lastNameMappedField;
    },
    // sets the reference to the choice field representing the last name mapped field
    set_lastNameMappedField: function (value) {
        this._lastNameMappedField = value;
    },
    // gets the reference to the choice field representing the email mapped field
    get_emailMappedField: function () {
        return this._emailMappedField;
    },
    // sets the reference to the choice field representing the email mapped field
    set_emailMappedField: function (value) {
        this._emailMappedField = value;
    },
    // gets the reference to the choice field with available dynamic list providers
    get_dynamicListProviderChoiceField: function () {
        return this._dynamicListProviderChoiceField;
    },
    // sets the reference to the choice field with available dynamic list providers
    set_dynamicListProviderChoiceField: function (value) {
        this._dynamicListProviderChoiceField = value;
    },
    // gets the reference to the command bar component
    get_commandBar: function () {
        return this._commandBar;
    },
    // sets the reference to the command bar component
    set_commandBar: function (value) {
        this._commandBar = value;
    },
    get_selectedDynamicList: function () {
        for (sIter = 0; sIter < this._availableDynamicLists.length; sIter++) {
            if (this._availableDynamicLists[sIter].Key == this._dynamicListsChoiceField.get_value()) {
                return this._availableDynamicLists[sIter];
            }
        }
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

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.DynamicMailingListSettings', Telerik.Sitefinity.Web.UI.AjaxDialogBase);