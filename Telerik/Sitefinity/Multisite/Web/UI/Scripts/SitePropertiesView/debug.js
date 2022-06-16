Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView.initializeBase(this, [element]);

    this._site = null;
    this._isCreateMode = null;
    this._loadingCounter = null;

    this._clientLabelManager = null;
    this._dialogTitle = null;
    this._messageControl = null;
    this._propertiesViewWrapper = null;
    this._nameField = null;
    this._domainNameWrapper = null;
    this._languagesFieldWrapper = null;
    this._useSelectedLanguagesWrapper = null;
    this._useAllLanguagesWrapper = null;
    this._pageSettingsWrapper = null;
    this._pageSettingsList = null;
    this._sitesDropDown = null;
    this._genericBinder = null;
    this._languagesField = null;
    this._allLanguagesField = null;
    this._domainField = null;
    this._stagingDomainField = null;
    this._isSiteInDevelopmentField = null;
    this._detailsLink = null;
    this._detailsContent = null;
    this._isOfflineField = null;
    this._pageBehaviourField = null;
    this._domainAliasesField = null;
    this._defaultProtocolField = null;
    this._saveButton = null;
    this._continueButton = null;
    this._orLabel = null;
    this._cancelLink = null;
    this._backLink = null;
    this._expandLink = null;
    this._loadingView = null;
    this._buttonsPanel = null;
    this._webServiceUrl = null;
    this._localizationServiceUrl = null;
    this._siteDetailView = null;
    this._isOfflineSection = null;
    this._selectPageRadio = null;
    this._useAllLanguagesRadio = null;
    this._useSelectedLanguagesRadio = null;
    this._pageField = null;
    this._enterUrlRadio = null;
    this._loginPageUrlField = null;
    this._additionalDomainValidationError = null;
    this._additionalStagingDomainValidationError = null;

    this._cancelDelegate = null;
    this._continueDelegate = null;
    this._saveDelegate = null;
    this._toggleDelegate = null;
    this._pageSettingsClickDelegate = null;
    this._valueChangedDelegate = null;
    this._ajaxFailDelegate = null;
    this._ajaxCompletedDelegate = null;
    this._detailsClickDelegate = null;
    this._selectPageRadioClickDelegate = null;
    this._useAllLanguagesRadioClickDelegate = null;
    this._useSelectedLanguagesRadioClickDelegate = null;
    this._configureAllLanguagesFieldCommandsDelegate = null;
    this._enterUrlRadioClickDelegate = null;
    this._manuallyConfiguredModeContainer = null;
    this._configureByDeploymentBtn = null;
    this._configuredByDeploymentModeContainer = null;
    this._configureManuallyBtn = null;
    this._configureModulesBtn = null;
    this._configureModulesClickDelegate = null;
    this._isSingleSiteMode = null;
    this._enableLanguagesTooltip = null;
    this._addLanguagesLabel = null;
    this._selectLanguagesLabel = null;
    this._cultureValidationError = null;

    this.EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
    this.DUMMY_GUID = "68a2ce16-9f7e-47b1-a230-5dc37efecc4c";
}

Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView.callBaseMethod(this, "initialize");

        this._cancelDelegate = Function.createDelegate(this, this._cancel);
        $addHandler(this.get_cancelLink(), "click", this._cancelDelegate);
        $addHandler(this.get_backLink(), "click", this._cancelDelegate);

        this._saveDelegate = Function.createDelegate(this, this._save);
        $addHandler(this.get_saveButton(), "click", this._saveDelegate);

        this._continueDelegate = Function.createDelegate(this, this._continue);
        $addHandler(this.get_continueButton(), "click", this._continueDelegate);

        this._toggleDelegate = Function.createDelegate(this, this._toggle);
        $addHandler(this.get_expandLink(), "click", this._toggleDelegate);

        this._valueChangedDelegate = Function.createDelegate(this, this._valueChanged);
        this.get_isOfflineField().add_valueChanged(this._valueChangedDelegate);
        this.get_isSiteInDevelopmentField().add_valueChanged(this._valueChangedDelegate);

        this._pageSettingsClickDelegate = Function.createDelegate(this, this._pageSettingsClickHandler);
        $addHandler(this.get_pageSettingsList(), "click", this._pageSettingsClickDelegate);

        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);

        this._detailsClickDelegate = Function.createDelegate(this, this._detailsClickHandler);
        $addHandler(this.get_detailsLink(), "click", this._detailsClickDelegate);

        this._selectPageRadioClickDelegate = Function.createDelegate(this, this._selectPageRadioClickHandler);
        $addHandler(this.get_selectPageRadio(), "click", this._selectPageRadioClickDelegate);

        this._enterUrlRadioClickDelegate = Function.createDelegate(this, this._enterUrlRadioClickHandler);
        $addHandler(this.get_enterUrlRadio(), "click", this._enterUrlRadioClickDelegate);

        this._useAllLanguagesRadioClickDelegate = Function.createDelegate(this, this._useAllLanguagesRadioClickHandler);
        $addHandler(this.get_useAllLanguagesRadio(), "click", this._useAllLanguagesRadioClickDelegate);

        this._useSelectedLanguagesRadioClickDelegate = Function.createDelegate(this, this._useSelectedLanguagesRadioClickHandler);
        $addHandler(this.get_useSelectedLanguagesRadio(), "click", this._useSelectedLanguagesRadioClickDelegate);

        this._configureAllLanguagesFieldCommandsDelegate = Function.createDelegate(this, this._configureAllLanguagesFieldCommands);

        if (this.get_configureManuallyBtn()) {
            this._configureManuallyClickDelegate = Function.createDelegate(this, this._configureManuallyClickHandler);
            $addHandler(this.get_configureManuallyBtn(), "click", this._configureManuallyClickDelegate);
        }

        if (this.get_configureByDeploymentBtn()) {
            this._configureByDeploymentClickDelegate = Function.createDelegate(this, this._configureByDeploymentClickHandler);
            $addHandler(this.get_configureByDeploymentBtn(), "click", this._configureByDeploymentClickDelegate);
        }

        if (this.get_configureModulesBtn()) {
            this._configureModulesClickDelegate = Function.createDelegate(this, this._configureModulesClickHandler);
            $addHandler(this.get_configureModulesBtn(), "click", this._configureModulesClickDelegate);
        }

    },

    dispose: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView.callBaseMethod(this, "dispose");

        if (this._cancelDelegate) {
            if (this.get_cancelLink()) {
                $removeHandler(this.get_cancelLink(), "click", this._cancelDelegate);
            }
            if (this.get_backLink()) {
                $removeHandler(this.get_backLink(), "click", this._cancelDelegate);
            }
            delete this._cancelDelegate;
        }

        if (this._saveDelegate) {
            if (this.get_saveButton()) {
                $removeHandler(this.get_saveButton(), "click", this._saveDelegate);
            }
            delete this._saveDelegate;
        }

        if (this._continueDelegate) {
            if (this.get_continueButton()) {
                $removeHandler(this.get_continueButton(), "click", this._continueDelegate);
            }
            delete this._continueDelegate;
        }

        if (this._toggleDelegate) {
            if (this.get_expandLink()) {
                $removeHandler(this.get_expandLink(), "click", this._toggleDelegate);
            }
            delete this._toggleDelegate;
        }

        if (this._pageSettingsClickDelegate) {
            if (this.get_pageSettingsList()) {
                $removeHandler(this.get_pageSettingsList(), "click", this._pageSettingsClickDelegate);
            }
            delete this._pageSettingsClickDelegate;
        }

        if (this._valueChangedDelegate) {
            if (this.get_isOfflineField()) {
                this.get_isOfflineField().remove_valueChanged(this._valueChangedDelegate);
            }
            if (this.get_isSiteInDevelopmentField()) {
                this.get_isSiteInDevelopmentField().remove_valueChanged(this._valueChangedDelegate);
            }
            delete this._valueChangedDelegate;
        }

        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }

        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._detailsClickDelegate) {
            if (this.get_detailsLink()) {
                $removeHandler(this.get_detailsLink(), "click", this._detailsClickDelegate);
            }
            delete this._detailsClickDelegate;
        }

        if (this._selectPageRadioClickDelegate) {
            if (this.get_selectPageRadio()) {
                $removeHandler(this.get_selectPageRadio(), "click", this._selectPageRadioClickDelegate);
            }
            delete this._selectPageRadioClickDelegate;
        }

        if (this._enterUrlRadioClickDelegate) {
            if (this.get_enterUrlRadio()) {
                $removeHandler(this.get_enterUrlRadio(), "click", this._enterUrlRadioClickDelegate);
            }
            delete this._enterUrlRadioClickDelegate;
        }

        if (this._useAllLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useAllLanguagesRadioClickDelegate);
            }
            delete this._useAllLanguagesRadioClickDelegate;
        }

        if (this._useSelectedLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useSelectedLanguagesRadioClickDelegate);
            }
            delete this._useSelectedLanguagesRadioadioClickDelegate;
        }

        if (this._useAllLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useAllLanguagesRadioClickDelegate);
            }
            delete this._useAllLanguagesRadioClickDelegate;
        }

        if (this._useSelectedLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useSelectedLanguagesRadioClickDelegate);
            }
            delete this._useSelectedLanguagesRadioadioClickDelegate;
        }

        if (this._useAllLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useAllLanguagesRadioClickDelegate);
            }
            delete this._useAllLanguagesRadioClickDelegate;
        }

        if (this._useSelectedLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useSelectedLanguagesRadioClickDelegate);
            }
            delete this._useSelectedLanguagesRadioadioClickDelegate;
        }

        if (this._configureAllLanguagesFieldCommandsDelegate) {
            delete this._configureAllLanguagesFieldCommandsDelegate;
        }

        if (this._useAllLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useAllLanguagesRadioClickDelegate);
            }
            delete this._useAllLanguagesRadioClickDelegate;
        }

        if (this._useSelectedLanguagesRadioClickDelegate) {
            if (this.get_useAllLanguagesRadio()) {
                $removeHandler(this.get_useAllLanguagesRadio(), "click", this._useSelectedLanguagesRadioClickDelegate);
            }
            delete this._useSelectedLanguagesRadioadioClickDelegate;
        }

        if (this._configureManuallyClickDelegate) {
            delete this._configureManuallyClickDelegate;
        }

        if (this._configureByDeploymentClickDelegate) {
            delete this._configureByDeploymentClickDelegate;
        }

        if (this._configureModulesClickDelegate) {
            if (this.get_configureModulesBtn()) {
                $removeHandler(this.get_configureModulesBtn(), "click", this._configureModulesClickDelegate);
            }
            delete this._configureModulesClickDelegate;
        }
    },

    /* *************************** public methods *************************** */

    show: function (isCreateMode, siteId, isFromConfigureModules) {
        jQuery(this._element).closest(".sfFormDialog").addClass("sfLoadingTransition");
        if (!(isFromConfigureModules && isCreateMode)) {
            this.reset();
        }

        this._isCreateMode = isCreateMode;

        var that = this;
        var onSuccess = function (data, textStatus, jqXHR) {
            that._site = data;
            that._updateUi();
            that.showWrapper();
        }

        if (isCreateMode) {
            jQuery(this.get_configureModulesBtn()).hide();
            onSuccess();
        }
        else {
            this._getSite(siteId, onSuccess);
        }

        if (this.get_languagesField().add_valueChanged) {
            this.get_languagesField().add_valueChanged(this._configureAllLanguagesFieldCommandsDelegate)
        }
    },

    hide: function () {
        jQuery(this.get_propertiesViewWrapper()).hide();
    },

    showWrapper: function () {
        jQuery(this.get_propertiesViewWrapper()).show();
    },

    reset: function () {
        this._site = null;
        this.get_pageBehaviourField().reset(this.DUMMY_GUID);
        jQuery(this.get_configureModulesBtn()).show();
        this.get_nameField().reset();
        this.get_domainField().reset();
        this.get_stagingDomainField().reset();
        this.get_isSiteInDevelopmentField().reset();
        jQuery(this.get_detailsContent()).hide();
        this.get_languagesField().reset();
        this.get_allLanguagesField().reset();
        this.get_isOfflineField().reset();
        jQuery(this.get_isOfflineSection()).show();
        this.get_domainAliasesField().reset();
        this.get_defaultProtocolField().reset();
        jQuery(this.get_pageSettingsList()).find("input:radio").first().click();
        jQuery(this.get_sitesDropDown()).hide();
        jQuery(this.get_selectPageRadio()).click();
        this.get_pageField().reset();
        this.get_loginPageUrlField().reset();
        jQuery(this.get_additionalDomainValidationError()).hide();
        jQuery(this.get_additionalStagingDomainValidationError()).hide();
        jQuery(this.get_cultureValidationError()).hide();

        // Advanced section is collapsed by default
        this._collapseAdvancedSection();
    },

    arrangeUIForIntegrationMode: function () {
        jQuery(this.get_domainNameWrapper()).hide();
        jQuery(this.get_isOfflineSection()).hide();
        jQuery(this.get_domainAliasesField()._element).hide();
        jQuery(this.get_expandLink()).parent().next().removeClass("sfCollapsedTarget").addClass("sfExpandedTarget");
        jQuery(this.get_expandLink()).closest(".sfExpandableForm").removeClass("sfExpandableForm");
        jQuery(this.get_expandLink()).parent().hide();
        jQuery(this.get_orLabel()).hide();
        jQuery(this.get_cancelLink()).hide();
        jQuery(this.get_backLink()).hide();
        jQuery(".k-window-actions").hide();
    },

    /* *************************** private methods *************************** */

    _updateUi: function () {
        if (this._isCreateMode) {

            // Set write mode
            this._switchDisplayMode(false);
            this._hideConfigurationModeContainers();

            if (this._isAllowedToConfigureModules) {
                jQuery(this.get_continueButton()).show();
                jQuery(this.get_saveButton()).hide();
            }
            else {
                jQuery(this.get_saveButton()).children("span").text(this.get_clientLabelManager().getLabel("MultisiteResources", "CreateThisSite"));
                jQuery(this.get_continueButton()).hide();
                jQuery(this.get_saveButton()).show();
            }

            //show/ hide the isOffline section depending on the global permissions
            if (!this._isAllowedStartStop) {
                jQuery(this.get_isOfflineSection()).hide();
            }

            this.get_dialogTitle().innerHTML = this.get_clientLabelManager().getLabel("MultisiteResources", "CreateSite");

            jQuery(this.get_pageSettingsWrapper()).show();

            // set the default language from configurations
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                that.get_allLanguagesField().set_value(data.Cultures);
                jQuery(that.get_useAllLanguagesRadio()).click();
                that._configureAllLanguagesFieldCommands();
                that._configureAllLanguagesFieldHandlers();
            };

            this._getLocalizationBasicSettings(onSuccess);

            // set a dummy root node id, because the site is still not created
            this.get_pageField().get_pageSelector().set_rootNodeId(this.DUMMY_GUID);
        }
        else {
            jQuery(this.get_saveButton()).children("span").text(this.get_clientLabelManager().getLabel("Labels", "SaveChanges"));
            jQuery(this.get_saveButton()).show();
            jQuery(this.get_continueButton()).hide();

            this.get_dialogTitle().innerHTML = this.get_clientLabelManager().getLabel("MultisiteResources", "SiteProperties");
            jQuery(this.get_pageSettingsWrapper()).hide();
            this.get_nameField().set_value(this._site.Name);
            this.get_domainField().set_value(this._site.LiveUrl);
            if (this._site.StagingUrl)
                this.get_isSiteInDevelopmentField().set_value(true);
            this.get_stagingDomainField().set_value(this._site.StagingUrl);
            this.get_languagesField().set_value(this._site.PublicContentCultures);
            this.get_allLanguagesField().set_value(this._site.SystemCultures);
            this.get_isOfflineField().set_value(this._site.IsOffline);
            this.get_pageBehaviourField().set_value(this._site);
            this.get_domainAliasesField().set_value(this._site.DomainAliases);
            this.get_defaultProtocolField().set_value(this._site.RequiresSsl);

            if (!this._site.IsAllowedStartStop) {
                jQuery(this.get_isOfflineSection()).hide();
            }

            this.get_pageField().get_pageSelector().set_rootNodeId(this._site.SiteMapRootNodeId);
            if (this._site.FrontEndLoginPageId && this._site.FrontEndLoginPageId != this.EMPTY_GUID) {
                this.get_pageField().set_value(this._site.FrontEndLoginPageId);
                jQuery(this.get_selectPageRadio()).click();
            }
            else if (this._site.FrontEndLoginPageUrl) {
                this.get_loginPageUrlField().set_value(this._site.FrontEndLoginPageUrl)
                jQuery(this.get_enterUrlRadio()).click();
            }

            // expand Advanced section
            if ((this._site.DomainAliases && this._site.DomainAliases.length > 0) ||
                (this._site.FrontEndLoginPageId && this._site.FrontEndLoginPageId != this.EMPTY_GUID) ||
                (this._site.FrontEndLoginPageUrl && this._site.FrontEndLoginPageUrl != "")) {
                this._expandAdvancedSection();
            }

            var isReadMode = this._site.SiteConfigurationMode === Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureByDeployment;
            this._switchDisplayMode(isReadMode);

            this._configureLanguageFields();
        }

        jQuery(this._element).closest(".sfLoadingTransition").removeClass("sfLoadingTransition");

        if (this.get_siteDetailView().get_isInStandaloneMode()) {
            this.get_dialogTitle().innerHTML = this.get_clientLabelManager().getLabel("MultisiteResources", "GeneralSettings");
            if (jQuery(".body-inner").length === 1) {
                var dialogWindow = jQuery(".k-widget.k-window");
                dialogWindow.detach().appendTo(".body-inner");
            }
        }
    },
    _configureLanguageFields: function () {

        if (this._site.UseSystemCultures) {
            jQuery(this.get_useAllLanguagesRadio()).click();
        } else {
            jQuery(this.get_useSelectedLanguagesRadio()).click();
        }

        this._configureAllLanguagesFieldCommands();
        this._configureAllLanguagesFieldHandlers();
    },

    _configureAllLanguagesFieldHandlers: function () {
        if (this.get_allLanguagesField()
            && this.get_allLanguagesField().get_binder()
            && this.get_allLanguagesField().get_binder().add_onItemCommand) {
            this.get_allLanguagesField().get_binder().add_onItemCommand(this._configureAllLanguagesFieldCommandsDelegate);
        }
    },
    _configureAllLanguagesFieldCommands: function () {
        if (this.get_allLanguagesField()) {
            // Hide the add languages button
            $(this.get_allLanguagesField().get_openSelector()).hide();

            // Removes the delete option
            $(this.get_allLanguagesField().get_itemsList())
                .find("li > a.sfDeleteBtn")
                .each(function (index, element) {
                    $(element).hide();
                });

            // Disable the drag and drop
            var that = this;
            //setTimeout(function () {
            $(that.get_allLanguagesField().get_itemsList()).sortable("disable");
            //}, 0);
        }

        if (this.get_languagesField()) {
            if (this.get_languagesField().get_value().length === 0) {
                this.get_languagesField().get_openSelector().text = this.get_selectLanguagesLabel();
                jQuery(this.get_languagesField().get_element()).find('.sfDragNDropListWrp').hide();
            }
            else {
                this.get_languagesField().get_openSelector().text = this.get_addLanguagesLabel();
                jQuery(this.get_languagesField().get_element()).find('.sfDragNDropListWrp').show();
                jQuery(this.get_cultureValidationError()).hide();
            }
        }
    },

    _switchDisplayMode: function (isReadMode) {
        if (isReadMode) {
            // Hide language field elments
            jQuery(this.get_languagesField().get_openSelector()).hide();
            jQuery(this.get_languagesField().get_element()).find("a").hide();
            var sortable = jQuery(this.get_languagesField().get_element()).find("ul").data("sortable");
            if (sortable) {
                sortable.disable();
            }

            jQuery(this.get_allLanguagesField().get_openSelector()).hide();
            jQuery(this.get_allLanguagesField().get_element()).find("a").hide();
            var allLanguagesSortable = jQuery(this.get_allLanguagesField().get_element()).find("ul").data("sortable");
            if (allLanguagesSortable) {
                allLanguagesSortable.disable();
            }

            jQuery(this.get_isOfflineField().get_element()).find("input").prop('disabled', true);
            jQuery(this.get_isOfflineField().get_element()).addClass("sfDisabledFieldCtrl");
            jQuery(this.get_nameField().get_element()).find("input").prop('disabled', true);
            jQuery(this.get_manuallyConfiguredModeContainer()).hide();
            jQuery(this.get_configuredByDeploymentModeContainer()).show();
        }
        else {
            // Show language field elements
            jQuery(this.get_languagesField().get_openSelector()).show();
            jQuery(this.get_languagesField().get_element()).find("a").show();
            jQuery(this.get_languagesField().get_element()).find("a.sfDisplayNone").hide();
            var sortableList = jQuery(this.get_languagesField().get_element()).find("ul").data("sortable");
            if (sortableList) {
                sortableList.enable();
            }

            jQuery(this.get_isOfflineField().get_element()).find("input").prop('disabled', false);
            jQuery(this.get_isOfflineField().get_element()).removeClass("sfDisabledFieldCtrl");
            jQuery(this.get_nameField().get_element()).find("input").prop('disabled', false);
            jQuery(this.get_manuallyConfiguredModeContainer()).show();
            jQuery(this.get_configuredByDeploymentModeContainer()).hide();
            jQuery(this.get_allLanguagesField().get_itemsList()).find('.sfSetAsDefault').show();
        }
    },

    _hideConfigurationModeContainers: function () {
        jQuery(this.get_manuallyConfiguredModeContainer()).hide();
        jQuery(this.get_configuredByDeploymentModeContainer()).hide();
    },

    _getSite: function (siteId, onSuccess) {
        this._setLoadingViewVisible(true);
        jQuery.ajax({
            type: 'GET',
            url: this.get_webServiceUrl() + String.format('{0}/', siteId),
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: this._ajaxFailDelegate,
            complete: this._ajaxCompleteDelegate
        });
    },

    _validateCultures: function () {
        if (this.get_useSelectedLanguagesRadio().checked && this.get_languagesField().get_value().length === 0) {
            jQuery(this.get_cultureValidationError()).show();
            return false;
        }

        jQuery(this.get_cultureValidationError()).hide();
        return true;
    },

    _saveSite: function (onSuccess) {
        var siteId = (this._site.Id) ? this._site.Id : this.EMPTY_GUID;

        if (!this._validateCultures()) {
            return;
        }

        this._setLoadingViewVisible(true);

        jQuery.ajax({
            type: 'PUT',
            url: this.get_webServiceUrl() + String.format('{0}/', siteId),
            contentType: "application/json",
            processData: false,
            data: Telerik.Sitefinity.JSON.stringify(this._site),
            success: onSuccess,
            error: this._ajaxFailDelegate,
            complete: this._ajaxCompleteDelegate
        });
    },

    _updateCurrentSite: function () {
        if (this._site == null) {
            this._site = {};
        }

        this._site.Id = this._site.Id ? this._site.Id : this.EMPTY_GUID;
        this._site.Name = this.get_nameField().get_value();
        this._site.LiveUrl = this.get_domainField().get_value();
        this._site.StagingUrl = (this.get_isSiteInDevelopmentField().get_value() == "true") ? this.get_stagingDomainField().get_value() : "";
        this._site.PublicContentCultures = this.get_languagesField().get_value();
        this._site.SystemCultures = this.get_allLanguagesField().get_value();
        this._site.IsOffline = this.get_isOfflineField().get_value();
        this._site.OfflineSiteMessage = this.get_pageBehaviourField().get_value().Message;
        this._site.OfflinePageToRedirect = this.get_pageBehaviourField().get_value().PageId;
        this._site.DomainAliases = this.get_domainAliasesField().get_value();
        this._site.RequiresSsl = this.get_defaultProtocolField().get_value();
        this._site.UseSystemCultures = this.get_useAllLanguagesRadio().checked;
        if (this._isCreateMode && jQuery(this.get_pageSettingsList()).find("input:checked").val() == "1") {
            this._site.SourcePagesSiteId = jQuery(this.get_sitesDropDown()).val();
        }
        if (this.get_selectPageRadio().checked && this.get_pageField().get_value()) {
            this._site.FrontEndLoginPageId = this.get_pageField().get_value();
        }
        else if (this.get_enterUrlRadio().checked) {
            this._site.FrontEndLoginPageUrl = this.get_loginPageUrlField().get_value();
            this._site.FrontEndLoginPageId = this.EMPTY_GUID;
        }
    },

    _isValid: function () {
        var isValid = true;
        var alternateReqPattern = /.*\:\/\/\s*$/i;
        var pathValidationPattern = /^(.*\:\/\/)?[^\/]*\/?[^\/]*$/i;
        var domainCannotContainPathMessage = this.get_clientLabelManager().getLabel("MultisiteResources", "SiteDomainCannotContainPath");

        var additionalValidationFunc = function (field, jErrDiv) {
            var validator = field.get_validator();
            var fieldValue = field.get_value();
            if (fieldValue.match(alternateReqPattern)) {
                isValid = false;
                jErrDiv.html(validator.get_requiredViolationMessage());
                jErrDiv.show();
            }
            //else if (!fieldValue.match(pathValidationPattern)) {
            //    isValid = false;
            //    jErrDiv.html(domainCannotContainPathMessage);
            //    jErrDiv.show();
            //}
            else {
                jErrDiv.hide();
            }
        };

        if (this.get_nameField().validate() == false) {
            isValid = false;
        }

        if (this.get_domainField().validate() == false) {
            isValid = false;
        }
        else {
            additionalValidationFunc(this.get_domainField(), jQuery(this.get_additionalDomainValidationError()));
        }

        if (this.get_isSiteInDevelopmentField().get_value() === "true") {
            if (this.get_stagingDomainField().validate() == false) {
                isValid = false;
            }
            else {
                additionalValidationFunc(this.get_stagingDomainField(), jQuery(this.get_additionalStagingDomainValidationError()));
            }
        }

        return isValid;
    },

    _getLocalizationBasicSettings: function (onSuccess) {
        jQuery.ajax({
            type: 'GET',
            url: this._localizationServiceUrl,
            contentType: "application/json",
            processData: false,
            success: onSuccess,
            error: this._ajaxFailDelegate
        });
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

    _toggleAdvancedSection: function () {
        jQuery(this.get_expandLink()).closest(".sfExpandableForm").toggleClass("sfExpandedForm");
        jQuery(this.get_expandLink()).parent().next().toggleClass("sfCollapsedTarget").toggleClass("sfExpandedTarget");
    },

    _expandAdvancedSection: function () {
        jQuery(this.get_expandLink()).closest(".sfExpandableForm").toggleClass("sfExpandedForm");
        jQuery(this.get_expandLink()).parent().next().removeClass("sfCollapsedTarget").addClass("sfExpandedTarget");
    },

    _collapseAdvancedSection: function () {
        jQuery(this.get_expandLink()).closest(".sfExpandableForm").removeClass("sfExpandedForm");
        jQuery(this.get_expandLink()).parent().next().addClass("sfCollapsedTarget").removeClass("sfExpandedTarget");
    },

    /* *************************** event handlers *************************** */

    _save: function (sender, args) {
        if (this._isValid()) {
            var onSuccess = function () {
                siteDetailView.close();
            }
            this._updateCurrentSite();
            this._saveSite(onSuccess);
        }
    },

    _continue: function (sender, args) {
        if (this._isValid()) {
            if (!this._validateCultures()) {
                return;
            }

            this._updateCurrentSite();
            this.get_siteDetailView().get_configureModulesView().set_propertiesView(this);

            this.hide();
            this.get_siteDetailView().get_configureModulesView().show(null, this._site);
        }
    },

    _cancel: function (sender, args) {
        siteDetailView.close();
    },

    _toggle: function (sender, args) {
        this._toggleAdvancedSection();
    },

    _pageSettingsClickHandler: function (sender, args) {
        var selectedValue = jQuery(this.get_pageSettingsList()).find("input:checked").val();

        if (selectedValue == 0) {
            jQuery(this.get_sitesDropDown()).hide();
        }
        else if (selectedValue == 1) {
            jQuery(this.get_sitesDropDown()).show();
            this.get_genericBinder().DataBind();
        }
    },

    _valueChanged: function (sender, args) {
        switch (sender.get_value()) {
            case "true":
                if (sender == this.get_isOfflineField()) {
                    jQuery(this.get_pageBehaviourField().get_element()).show();
                } else if (sender == this.get_isSiteInDevelopmentField()) {
                    jQuery(this.get_stagingDomainField().get_element()).show();
                }
                break;
            case "false":
                if (sender == this.get_isOfflineField()) {
                    jQuery(this.get_pageBehaviourField().get_element()).hide();
                } else if (sender == this.get_isSiteInDevelopmentField()) {
                    jQuery(this.get_stagingDomainField().get_element()).hide();
                }
                break;
            default:
                break;
        }

        return false;
    },

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
    },

    _detailsClickHandler: function (sender, args) {
        jQuery(this.get_detailsContent()).toggle();
    },

    _selectPageRadioClickHandler: function (sender, args) {
        jQuery(this.get_loginPageUrlField().get_element()).hide();
    },

    _enterUrlRadioClickHandler: function (sender, args) {
        jQuery(this.get_loginPageUrlField().get_element()).show();
    },

    _useAllLanguagesRadioClickHandler: function (sender, args) {
        jQuery(this.get_useAllLanguagesWrapper()).show();
        jQuery(this.get_useSelectedLanguagesWrapper()).hide();
        this.get_enableLanguagesTooltip().hide();
    },

    _useSelectedLanguagesRadioClickHandler: function (sender, args) {
        jQuery(this.get_useAllLanguagesWrapper()).hide();
        jQuery(this.get_useSelectedLanguagesWrapper()).show();
        this.get_enableLanguagesTooltip().hide();
    },

    _configureManuallyClickHandler: function (sender, args) {
        this._changeConfigurationMode(Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureManually);
    },

    _configureByDeploymentClickHandler: function (sender, args) {
        this._changeConfigurationMode(Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureByDeployment);
    },

    _configureModulesClickHandler: function (sender, args) {
        this.get_siteDetailView().show(this._isCreateMode, this._site.Id, true, true);
    },
    _changeConfigurationMode: function (newMode) {
        if (this.get_site().SiteConfigurationMode !== newMode) {
            var that = this;
            var onSuccess = function (data) {
                that._site.SiteConfigurationMode = data.Mode;

                var isReadMode = that._site.SiteConfigurationMode === Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureByDeployment;
                that._switchDisplayMode(isReadMode);

                that.showWrapper();
            }

            var siteId = this._site.Id;
            this._setLoadingViewVisible(true);
            jQuery.ajax({
                type: 'PUT',
                url: this.get_webServiceUrl() + String.format('{0}/config/mode', siteId),
                contentType: "application/json",
                processData: false,
                data: Telerik.Sitefinity.JSON.stringify({ Mode: newMode }),
                success: onSuccess,
                error: this._ajaxFailDelegate,
                complete: this._ajaxCompleteDelegate
            });
        }
    },

    /* *************************** properties *************************** */

    get_site: function () {
        return this._site;
    },
    set_site: function (value) {
        this._site = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_dialogTitle: function () {
        return this._dialogTitle;
    },
    set_dialogTitle: function (value) {
        this._dialogTitle = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_propertiesViewWrapper: function () {
        return this._propertiesViewWrapper;
    },
    set_propertiesViewWrapper: function (value) {
        this._propertiesViewWrapper = value;
    },
    get_domainNameWrapper: function () {
        return this._domainNameWrapper;
    },
    set_domainNameWrapper: function (value) {
        this._domainNameWrapper = value;
    },
    get_languagesFieldWrapper: function () {
        return this._languagesFieldWrapper;
    },
    set_languagesFieldWrapper: function (value) {
        this._languagesFieldWrapper = value;
    },
    get_useSelectedLanguagesWrapper: function () {
        return this._useSelectedLanguagesWrapper;
    },
    set_useSelectedLanguagesWrapper: function (value) {
        this._useSelectedLanguagesWrapper = value;
    },
    get_useAllLanguagesWrapper: function () {
        return this._useAllLanguagesWrapper;
    },
    set_useAllLanguagesWrapper: function (value) {
        this._useAllLanguagesWrapper = value;
    },
    get_nameField: function () {
        return this._nameField;
    },
    set_nameField: function (value) {
        this._nameField = value;
    },
    get_pageSettingsWrapper: function () {
        return this._pageSettingsWrapper;
    },
    set_pageSettingsWrapper: function (value) {
        this._pageSettingsWrapper = value;
    },
    get_pageSettingsList: function () {
        return this._pageSettingsList;
    },
    set_pageSettingsList: function (value) {
        this._pageSettingsList = value;
    },
    get_sitesDropDown: function () {
        return this._sitesDropDown;
    },
    set_sitesDropDown: function (value) {
        this._sitesDropDown = value;
    },
    get_genericBinder: function () {
        return this._genericBinder;
    },
    set_genericBinder: function (value) {
        this._genericBinder = value;
    },
    get_languagesField: function () {
        return this._languagesField;
    },
    set_languagesField: function (value) {
        this._languagesField = value;
    },
    get_allLanguagesField: function () {
        return this._allLanguagesField;
    },
    set_allLanguagesField: function (value) {
        this._allLanguagesField = value;
    },
    get_domainField: function () {
        return this._domainField;
    },
    set_domainField: function (value) {
        this._domainField = value;
    },
    get_stagingDomainField: function () {
        return this._stagingDomainField;
    },
    set_stagingDomainField: function (value) {
        this._stagingDomainField = value;
    },
    get_isSiteInDevelopmentField: function () {
        return this._isSiteInDevelopmentField;
    },
    set_isSiteInDevelopmentField: function (value) {
        this._isSiteInDevelopmentField = value;
    },
    get_detailsLink: function () {
        return this._detailsLink;
    },
    set_detailsLink: function (value) {
        this._detailsLink = value;
    },
    get_detailsContent: function () {
        return this._detailsContent;
    },
    set_detailsContent: function (value) {
        this._detailsContent = value;
    },
    get_isOfflineField: function () {
        return this._isOfflineField;
    },
    set_isOfflineField: function (value) {
        this._isOfflineField = value;
    },
    get_pageBehaviourField: function () {
        return this._pageBehaviourField;
    },
    set_pageBehaviourField: function (value) {
        this._pageBehaviourField = value;
    },
    get_domainAliasesField: function () {
        return this._domainAliasesField;
    },
    set_domainAliasesField: function (value) {
        this._domainAliasesField = value;
    },
    get_defaultProtocolField: function () {
        return this._defaultProtocolField;
    },
    set_defaultProtocolField: function (value) {
        this._defaultProtocolField = value;
    },
    get_saveButton: function () {
        return this._saveButton;
    },
    set_saveButton: function (value) {
        this._saveButton = value;
    },
    get_continueButton: function () {
        return this._continueButton;
    },
    set_continueButton: function (value) {
        this._continueButton = value;
    },
    get_orLabel: function () {
        return this._orLabel;
    },
    set_orLabel: function (value) {
        this._orLabel = value;
    },
    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },
    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },
    get_expandLink: function () {
        return this._expandLink;
    },
    set_expandLink: function (value) {
        this._expandLink = value;
    },
    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },
    get_buttonsPanel: function () {
        return this._buttonsPanel;
    },
    set_buttonsPanel: function (value) {
        this._buttonsPanel = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_siteDetailView: function () {
        return this._siteDetailView;
    },
    set_siteDetailView: function (value) {
        this._siteDetailView = value;
    },
    get_isOfflineSection: function () {
        return this._isOfflineSection;
    },
    set_isOfflineSection: function (value) {
        this._isOfflineSection = value;
    },
    get_selectPageRadio: function () {
        return this._selectPageRadio;
    },
    set_selectPageRadio: function (value) {
        this._selectPageRadio = value;
    },
    get_useSelectedLanguagesRadio: function () {
        return this._useSelectedLanguagesRadio;
    },
    set_useSelectedLanguagesRadio: function (value) {
        this._useSelectedLanguagesRadio = value;
    },
    get_useAllLanguagesRadio: function () {
        return this._useAllLanguagesRadio;
    },
    set_useAllLanguagesRadio: function (value) {
        this._useAllLanguagesRadio = value;
    },
    get_pageField: function () {
        return this._pageField;
    },
    set_pageField: function (value) {
        this._pageField = value;
    },
    get_enterUrlRadio: function () {
        return this._enterUrlRadio;
    },
    set_enterUrlRadio: function (value) {
        this._enterUrlRadio = value;
    },
    get_loginPageUrlField: function () {
        return this._loginPageUrlField;
    },
    set_loginPageUrlField: function (value) {
        this._loginPageUrlField = value;
    },
    get_additionalDomainValidationError: function () {
        return this._additionalDomainValidationError;
    },
    set_additionalDomainValidationError: function (value) {
        this._additionalDomainValidationError = value;
    },
    get_additionalStagingDomainValidationError: function () {
        return this._additionalStagingDomainValidationError;
    },
    set_additionalStagingDomainValidationError: function (value) {
        this._additionalStagingDomainValidationError = value;
    },
    get_manuallyConfiguredModeContainer: function () {
        return this._manuallyConfiguredModeContainer;
    },
    set_manuallyConfiguredModeContainer: function (value) {
        this._manuallyConfiguredModeContainer = value;
    },
    get_configureByDeploymentBtn: function () {
        return this._configureByDeploymentBtn;
    },
    set_configureByDeploymentBtn: function (value) {
        this._configureByDeploymentBtn = value;
    },
    get_configuredByDeploymentModeContainer: function () {
        return this._configuredByDeploymentModeContainer;
    },
    set_configuredByDeploymentModeContainer: function (value) {
        this._configuredByDeploymentModeContainer = value;
    },
    get_configureManuallyBtn: function () {
        return this._configureManuallyBtn;
    },
    set_configureManuallyBtn: function (value) {
        this._configureManuallyBtn = value;
    },
    get_configureModulesBtn: function (value) {
        return this._configureModulesBtn;
    },
    set_configureModulesBtn: function (value) {
        this._configureModulesBtn = value;
    },
    get_isSingleSiteMode: function () {
        return this._isSingleSiteMode;
    },
    set_isSingleSiteMode: function (value) {
        this._isSingleSiteMode = value;
    },
    get_enableLanguagesTooltip: function () {
        return this._enableLanguagesTooltip;
    },
    set_enableLanguagesTooltip: function (value) {
        this._enableLanguagesTooltip = value;
    },
    get_addLanguagesLabel: function () {
        return this._addLanguagesLabel;
    },
    set_addLanguagesLabel: function (value) {
        this._addLanguagesLabel = value;
    },
    get_selectLanguagesLabel: function () {
        return this._selectLanguagesLabel;
    },
    set_selectLanguagesLabel: function (value) {
        this._selectLanguagesLabel = value;
    },
    get_cultureValidationError: function () {
        return this._cultureValidationError;
    },
    set_cultureValidationError: function (value) {
        this._cultureValidationError = value;
    }
};

Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView.registerClass('Telerik.Sitefinity.Multisite.Web.UI.SitePropertiesView', Sys.UI.Control);

Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.Services");

Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode = function () {
};
Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.prototype = {
    ConfigureManually: 0,
    ConfigureByDeployment: 1
};
Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.registerEnum("Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode");