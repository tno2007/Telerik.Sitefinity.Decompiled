Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog.initializeBase(this, [element]);

    this._dialogTitle = null;
    this._clientLabelManager = null;

    this._providerName = null;

    this._cancelButton = null;
    this._cancelButtonClickDelegate = null;

    this._doneButton = null;
    this._doneButtonClickDelegate = null;

    this._errorMessageWrapper = null;
    this._errorMessageLabel = null;

    this._dataSource = null;
    this._siteDataSourceConfig = null;

    this.usersDataSourceMode = false;

    this._userGroupInformationText = null;
    this._sourceInformationText = null;
};

Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog.callBaseMethod(this, "initialize");

        this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClickHandler);
        $addHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);

        this._doneButtonClickDelegate = Function.createDelegate(this, this._doneButtonClickHandler);
        $addHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);
    },

    dispose: function () {
        if (this._cancelButtonClickDelegate) {
            if (this.get_cancelButton()) {
                $removeHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
            }
            delete this._cancelButtonClickDelegate;
        }

        if (this._doneButtonClickDelegate) {
            if (this.get_doneButton()) {
                $removeHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);
            }
            delete this._doneButtonClickDelegate;
        }

        Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    reset: function () {
        jQuery(this.get_providerName()).val("");
        this._hideError();
    },

    show: function (dataSource, dataSourceConfig) {
        this.reset();

        this.usersDataSourceMode = dataSourceConfig.Title == "Users";
        this._dataSource = dataSource;
        this._siteDataSourceConfig = dataSourceConfig;

        jQuery(this.get_kendoWindow().wrapper).width(510);

        this._updateLabelsAndMessages();

        Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog.callBaseMethod(this, "show");
    },

    /* *************************** private methods *************************** */

    _updateLabelsAndMessages: function () {
        var titleText = String.format(this.get_clientLabelManager().getLabel("MultisiteResources", "CreateNewSourceFor"), this._siteDataSourceConfig.Title);
        var sourceNameText = this.get_clientLabelManager().getLabel("MultisiteResources", "SourceName");
        var sourceNameExampleText = this.get_clientLabelManager().getLabel("MultisiteResources", "SourceNameExample");
        var sourceInformationLabelText = this.get_clientLabelManager().getLabel("MultisiteResources", "SourceInformation");
        var sourceInformationLink = this.get_clientLabelManager().getLabel("MultisiteResources", "ExternalLinkSourceInformation");

        if (this.usersDataSourceMode) {
            titleText = this.get_clientLabelManager().getLabel("MultisiteResources", "CreateNewUserGroup");
            sourceNameText = this.get_clientLabelManager().getLabel("MultisiteResources", "UserGroupName");
            sourceNameExampleText = this.get_clientLabelManager().getLabel("MultisiteResources", "UserGroupNameExample");
            sourceInformationLabelText = this.get_clientLabelManager().getLabel("MultisiteResources", "UserGroupInformation");
            sourceInformationLink = this.get_clientLabelManager().getLabel("MultisiteResources", "ExternalLinkUserGroupInformation");
        }

        jQuery(this.get_dialogTitle()).html(titleText);
        jQuery(this.get_outerDiv()).find("[id$='sourceInformation']").text(sourceInformationLabelText);
        jQuery(this.get_outerDiv()).find("[id$='sourceInformationLink']").attr("href", sourceInformationLink);
        jQuery(this.get_outerDiv()).find("[id$='providerNameLbl']").text(sourceNameText);
        jQuery(this.get_outerDiv()).find("[id$='sourceNameExampleLbl']").text(sourceNameExampleText);
    },

    _cancelButtonClickHandler: function (sender, args) {
        this.close();
    },

    _doneButtonClickHandler: function (sender, args) {
        var providerName = $(this.get_providerName()).val().trim();

        if (providerName === "") {
            this._showError(this.get_clientLabelManager().getLabel("MultisiteResources", "EmptySourceError"));
            return;
        }

        if (providerName.length > 255) {
            this._showError(this.get_clientLabelManager().getLabel("MultisiteResources", "SourceNameLengthMessage"));
            return;
        }

        if (this._validateIfProviderExist(providerName)) {
            this.close({ providerName: providerName });
        } else {
            if (this.usersDataSourceMode) {
                this._showError(this.get_clientLabelManager().getLabel("MultisiteResources", "UserGroupAlreadyExistsError"));
            } else {
                this._showError(this.get_clientLabelManager().getLabel("MultisiteResources", "SourceAlreadyExistsError"));
            }
        }
    },

    _validateIfProviderExist: function (providerName) {
        var valid = true;

        if (this._dataSource && this._dataSource.data() && this._dataSource.data().length) {
            for (var i = 0; i < this._dataSource.data().length; i++) {
                if (providerName.toLowerCase() === this._dataSource.data()[i].Link.ProviderTitle.toLowerCase()) {
                    valid = false;
                    break;
                }
            }
        }

        return valid;
    },

    _showError: function (error) {
        $(this.get_errorMessageWrapper()).show();
        $(this.get_errorMessageLabel()).text(error);
        this._resizeToContent();
    },

    _hideError: function () {
        $(this.get_errorMessageWrapper()).hide();
        $(this.get_errorMessageLabel()).text("");
        this._resizeToContent();
    },

    _resizeToContent: function () {
        if (window.dialogBase) {
            dialogBase.resizeToContent();
        }
    },

    /* *************************** properties *************************** */

    get_dialogTitle: function () {
        return this._dialogTitle;
    },
    set_dialogTitle: function (value) {
        this._dialogTitle = value;
    },

    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_errorMessageLabel: function () {
        return this._errorMessageLabel;
    },
    set_errorMessageLabel: function (value) {
        this._errorMessageLabel = value;
    },

    get_errorMessageWrapper: function () {
        return this._errorMessageWrapper;
    },
    set_errorMessageWrapper: function (value) {
        this._errorMessageWrapper = value;
    },

    get_sourceInformationText: function () {
        return this._sourceInformationText;
    },
    set_sourceInformationText: function (value) {
        this._sourceInformationText = value;
    },
    get_userGroupInformationText: function () {
        return this._userGroupInformationText;
    },
    set_userGroupInformationText: function (value) {
        this._userGroupInformationText = value;
    },
};

Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog.registerClass('Telerik.Sitefinity.Multisite.Web.UI.DataSourceCreateProviderDialog', Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);