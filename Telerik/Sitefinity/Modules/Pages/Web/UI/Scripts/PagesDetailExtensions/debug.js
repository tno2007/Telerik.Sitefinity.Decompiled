// called by the DetailFormView when it is loaded
function OnDetailViewLoaded(sender, args) {
    // the sender here is DetailFormView
    var currentForm = sender;
    Sys.Application.add_init(function () {
        $create(Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension,
                                         { _detailFormView: currentForm },
                                         {},
                                         {},
                                         null);
    });
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension = function () {
    Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.initializeBase(this);
    // Main components
    this._detailFormView = {};
    this._binder = null;
    this._emptyGuid = "00000000-0000-0000-0000-000000000000";

    // Fields references
    this._groupPageField = null;
    this._externalPageField = null;
    this._parentPageField = null;
    this._urlField = null;
    this._languageField = null;
    this._showInNavField = null;
    this._redirectToDefaultField = null;
    this._additionalUrlsField = null;
    this._allowMultipleUrlsField = null;
    this._canonicalUrlSettingsField = null;
    this._siteSelectorField = null;
    this._includeInSearchIndexField = null;
    this._defultRedirectToDefaultText = null;
    this._isFromEditor = false;
    this._command = "";
    this._siteIdParamKey = "sf_site";

    this._dataItem = null;
    this._selectedSite = null;
    this._selectedLanguage = null;
    // Event delegates
    this._fieldControlValueChangedDelegate = null;
    this._onDataBindDelegate = null;
    this._windowShowDelegate = null;
    this._hideTemplateSection = false;
    this._formClosingDelegate = null;
    this._commandDelegate = null;
    this._targetSiteSelectedDelegate = null;
    this._targetSiteLanguageSelectedDelegate = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.callBaseMethod(this, "initialize");
        this._binder = this._detailFormView.get_binder();

        this._groupPageField = this._getFieldControlByDataFieldName("IsGroup");
        this._externalPageField = this._getFieldControlByDataFieldName("IsExternalGroup").get_isExternalPageChoiceField();
        this._fieldControlValueChangedDelegate = Function.createDelegate(this, this._fieldControlValueChanged);
        this._groupPageField.add_valueChanged(this._fieldControlValueChangedDelegate);
        this._externalPageField.add_valueChanged(this._fieldControlValueChangedDelegate);

        this._redirectToDefaultField = this._getFieldControlByDataFieldName("AdditionalUrlsRedirectToDefaultOne");

        this._additionalUrlsField = this._getFieldControlByDataFieldName("MultipleNavigationNodes");
        this._allowMultipleUrlsField = this._getFieldControlByDataFieldName("AllowMultipleUrls");

        this._parentPageField = this._getFieldControlByDataFieldName("Parent");
        if (this._parentPageField) {
            this._parentPageField.get_nodeSelector().set_bindOnLoad(false);
        }

        this._urlField = this._getFieldControlByDataFieldName("UrlName");

        this._showInNavField = this._getFieldControlByDataFieldName("ShowInNavigation");

        this._languageField = this._getFieldControlByDataFieldName("AvailableLanguages");
        if (this._languageField) {
            this._languageField.add_valueChanged(this._fieldControlValueChangedDelegate);
        }

        this._siteSelectorField = this._getFieldControlByDataFieldName("TargetSiteId");

        this._canonicalUrlSettingsField = this._binder.getFieldControlByFieldName("canonicalUrlSettingsFieldElement");
        this._includeInSearchIndexField = this._getFieldControlByDataFieldName("IncludeInSearchIndex");

        this._formClosingDelegate = Function.createDelegate(this, this._formClosingHandler);
        this._detailFormView.add_formClosing(this._formClosingDelegate);

        this._formCreatedDelegate = Function.createDelegate(this, this._formCreatedHandler);
        this._detailFormView.add_formCreated(this._formCreatedDelegate);

        this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        this._detailFormView.add_command(this._commandDelegate);


        if (this._siteSelectorField) {
            this._targetSiteSelectedDelegate = Function.createDelegate(this, this._targetSiteSelectedHandler);
            this._siteSelectorField.add_onSiteSelected(this._targetSiteSelectedDelegate);
            this._targetSiteLanguageSelectedDelegate = Function.createDelegate(this, this._targetSiteLanguageSelectedHandler);
            this._siteSelectorField.add_onSiteLanguageSelected(this._targetSiteLanguageSelectedDelegate);
        }
    },

    dispose: function () {
        if (this._fieldControlValueChangedDelegate) {
            if (this._groupPageField) {
                this._groupPageField.remove_valueChanged(this._fieldControlValueChangedDelegate);
            }
            if (this._externalPageField) {
                this._externalPageField.remove_valueChanged(this._fieldControlValueChangedDelegate);
            }
            delete this._fieldControlValueChangedDelegate;
        }

        if (this._onDataBindDelegate != null) {
            if (this._detailFormView) {
                this._detailFormView.remove_onDataBind(this._onDataBindDelegate);
            }
            delete this._onDataBindDelegate;
        }

        if (this._formCreatedDelegate) {
            if (this._detailFormView) {
                this._detailFormView.remove_formCreated(this._formCreatedDelegate);
            }
            delete this._formCreatedDelegate;
        }
        this._detailFormView.remove_formClosing(this._formClosingDelegate);
        delete this._formClosingDelegate;

        if (this._commandDelegate) {
            this._detailFormView.remove_command(this._commandDelegate);
            delete this._commandDelegate;
        }

        if (this._targetSiteSelectedDelegate) {
            if (this._siteSelectorField) {
                this._siteSelectorField.remove_onSiteSelected(this._targetSiteSelectedDelegate);
            }
        }

        Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
    _fieldControlValueChanged: function (sender, args) {
        var fieldName = sender.get_dataFieldName();

        switch (fieldName) {
            case "IsGroup":
                this._toggleElement(this._canonicalUrlSettingsField.get_element(), Boolean.parse(sender.get_value()));
            case "IsExternal":
                var sectionCount = detailFormView.get_sectionIds().length;
                var createAndReturnWidget = this._getWidgetByName("CreateAndReturnWidgetElement");
                var createAndReturnButtonElement = (createAndReturnWidget) ? createAndReturnWidget.get_element() : null;
                var isLeastOneChecked = (this._groupPageField.get_value() == "true" || this._externalPageField.get_value() == "true") ? true : false;

                for (var sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
                    var sectionId = detailFormView.get_sectionIds()[sectionIndex];
                    var section = $find(sectionId);
                    var sectionName = section.get_name();

                    if (sectionName == "AdvancedOptionsSection") {
                        this._showHideAdvancedSettingsNonGroupPage(!isLeastOneChecked);
                    }

                    if (isLeastOneChecked) {
                        jQuery(this._includeInSearchIndexField.get_element()).hide();
                        // hides 'Create and go to add content' button for Group Pages when creating a page
                        this._toggleWidgetByName("SaveChangesWidgetElement", true);
                        //chage the style of createAndReturnButton
                        if (createAndReturnButtonElement) {
                            jQuery(createAndReturnButtonElement).addClass("sfSave");
                            this._toggleWidgetByName("CreateAndReturnWidgetElement", false);
                        }
                    } else {
                        jQuery(this._includeInSearchIndexField.get_element()).show();
                        // shows 'Create and go to add content' button for Normal Pages when creating a page
                        this._toggleWidgetByName("SaveChangesWidgetElement", false);
                        //chage the style of createAndReturnButton
                        if (createAndReturnButtonElement) {
                            jQuery(createAndReturnButtonElement).removeClass("sfSave");
                            this._toggleWidgetByName("CreateAndReturnWidgetElement", this._isFromEditor);
                        }
                    }

                    if (isLeastOneChecked && sectionName != "MainSection" && sectionName != "AdvancedOptionsSection" && sectionName != "CustomFieldsSection" && sectionName != "RelatedDataSection" && sectionName != "RelatedMediaSection" && sectionName != "HeaderSection") {
                        jQuery(section.get_element()).hide();
                    }
                    else {
                        if (this._hideTemplateSection == false || sectionName != "TemplateSection") {
                            jQuery(section.get_element()).show();
                        }
                    }
                }

                //Set the default template when "Group page" and "External page" checkbox is unchecked
                if (this._groupPageField.get_value() == "false" && this._externalPageField.get_value() == "false" && this._hideTemplateSection == false) {
                    var templateField = this._binder.getFieldControlByDataFieldName('Template');
                    var templateValue = templateField.get_value();
                    var emptyGuid = this._binder.GetEmptyGuid();

                    //Only set default template if no template is set
                    if (templateValue == null || templateValue.Id == emptyGuid) {
                        var templateField = this._binder.getFieldControlByDataFieldName('Template');
                        templateField.selectCurrentUITemplate();
                    }
                }

                break;
            case "AvailableLanguages":
                if (this._languageField) {
                    var languageCode = this._languageField.get_value();
                    this._binder.set_uiCulture(languageCode);

                    this._updateParentFieldForLanguage(languageCode, true);
                }
                break;
            default:
                break;
        }
    },

    _showHideAdvancedSettingsNonGroupPage: function (show) {
        var hide = show == false;
        this._toggleField("RequireSsl", hide);
        this._toggleField("EnableViewState", hide);
        this._toggleField("Crawlable", hide);
        this._toggleField("IncludeScriptManager", hide);
        this._toggleField("HeadTagContent", hide);
        this._toggleField("cacheProfileElement", hide);
    },

    // Fired when a form is created. Args are of type 'Telerik.Sitefinity.FormCreatedEventArgs'
    _formCreatedHandler: function (sender, args) {
        var detailFormView = this._detailFormView;
        detailFormView.get_messageControl().hide();

        //This is needed because the binder dataItem is sometimes changed after that (on dataBind) and we need to preserve the source object and language
        if (!this._onDataBindDelegate) {
            this._onDataBindDelegate = Function.createDelegate(this, this._onDataBindHandler);
            detailFormView.add_onDataBind(this._onDataBindDelegate);
        }

        var dataItem = this._binder.get_dataItem();
        this._dataItem = dataItem;

        if (!args.get_isNew() && args.get_commandName() == "edit") {
            if (dataItem.IsGroup) {
                detailFormView.get_messageControl().showPositiveMessage("This is a group page and does not have its own content.This type of page can be used only to group other pages.");
            } else {
                detailFormView.get_messageControl().hide();
            }

            var params = args.get_params();
            if ((params['HideTemplate'] && params['HideTemplate'] == true) || args.get_commandName() == "edit") {
                this._hideTemplateSection = true;
                var sectionCount = detailFormView.get_sectionIds().length;
                for (var sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
                    var sectionId = detailFormView.get_sectionIds()[sectionIndex];
                    var section = $find(sectionId);
                    if (section.get_name() == "TemplateSection") {
                        jQuery(section.get_element()).hide();
                    }
                }
            } else {
                this._hideTemplateSection = false;
            }
        } else {
            if (args.get_commandName() === "duplicate") {
                this._urlField.set_ckeckConditionalMirroring(false);
                this._urlField.set_isToMirror(true);

                if (this._siteSelectorField && !this._siteSelectorField._visible) {
                    this._toggleField("TargetSiteId", true);
                }
            }
            var commandArgument = args.get_commandArgument();
            if (commandArgument && commandArgument.language && commandArgument.sourceObjectId != this._guidEmpty) {

                dataItem.Language = commandArgument.language;
                dataItem.SourceLanguagePageId = commandArgument.sourceObjectId;

                var isSplit = commandArgument.isSplit;
                var hideReturnToPages = commandArgument.isFromEditor;
                this._isFromEditor = hideReturnToPages;
                this._binder.set_uiCulture(dataItem.Language);

                if (this._languageField) {
                    this._languageField.selectListItemsByValue(dataItem.Language);
                    jQuery(this._languageField.get_choiceElement()).attr('disabled', 'disabled');
                }

                this._showHideLanguageInvariantSettings(true);
                this._showHideSplitPageSettings(!isSplit);
                this._toggleWidgetByName("CreateAndReturnWidgetElement", hideReturnToPages);
            } else {
                this._showHideLanguageInvariantSettings(false);
                this._showHideSplitPageSettings(false);

                //Set language for the template selector
                if (commandArgument && commandArgument.language) {
                    var templateSelectorField = this._getFieldControlByDataFieldName("Template");
                    templateSelectorField.set_language(commandArgument.language);
                }
            }
        }

        //If in multilingual mode, set language for parent selector field
        if (this._languageField) {
            var binder = this._detailFormView.get_binder();
            var lang = dataItem.Language;
            if (binder.get_uiCulture()) {
                lang = binder.get_uiCulture();
            } else if (this._detailFormView.get_languageSelector()) {
                lang = this._detailFormView.get_languageSelector().get_value();
            }

            if (lang) {
                this._updateParentFieldForLanguage(lang, false);
            }
        }

        var choice = this._redirectToDefaultField.get_choices()[0];
        if (choice) {
            if (!this._defultRedirectToDefaultText) {
                this._defultRedirectToDefaultText = choice.Text;
            }

            choice.Text = this._defultRedirectToDefaultText + " " + dataItem.PageLiveUrl;
            this._redirectToDefaultField.get_choiceElement().nextSibling.innerHTML = choice.Text;
        }

        // If the page is homepage - add some restrictions
        if (this._dataItem && (this._detailFormView._homePageId == this._dataItem.Id ||
            (detailFormView.get_isMultilingual() && this._dataItem.LocalizationStrategy == 1))) { //if this is a split page
            jQuery(this._groupPageField.get_element()).hide();
            jQuery(this._externalPageField.get_element()).hide();
        } else {
            jQuery(this._groupPageField.get_element()).show();
            jQuery(this._externalPageField.get_element()).show();
        }
    },

    _onDataBindHandler: function (sender, dataItem) {
        dataItem.Item.Language = this._dataItem.Language;
        dataItem.Item.SourceLanguagePageId = this._dataItem.SourceLanguagePageId;
        if (sender.get_createFormCommandName() == 'duplicate') {
            this._urlField._setInitialUrlMode();
            this._urlField.set_isToMirror(true);

            this._additionalUrlsField.set_value(null); // when duplicating pages we remove the additional urls
            this._allowMultipleUrlsField.reset(); // reset the expandable field
        }

        var uiCulture = this._binder.get_uiCulture();
        if (this._siteSelectorField)
            this._siteSelectorField.setSelectedCulture(uiCulture);
    },

    _commandHandler: function (sender, args) {
        this._command = args._commandName;
        if (args._commandName == "delete" && this._dataItem.IsHomePage) {
            this._detailFormView.showPromptDialogByName("cannotDeleteHomepageDialog");
            args.set_cancel(true);
            return;
        }

        if (args._commandName == "delete" && this._dataItem.HasChildren) {
            this._detailFormView.showPromptDialogByName("cannotDeleteParentPageDialog");
            args.set_cancel(true);
        }
    },

    _formClosingHandler: function (sender, args) {
        var form = sender;
        var commandArgument = args.get_commandArgument();

        var isNew = args.get_isNew();

        var commandName = args.get_commandName();
        var isDuplicate = commandName === "duplicate";

        if (form.get_isMultilingual() && commandArgument) {
            isNew = commandArgument.languageMode === "create" || isDuplicate;
        }
        if (isNew && args.get_isDirty()) {
            // If page is group or redirect - return to grid
            if (this._isFromEditor && (this._groupPageField.get_value() == "true" || this._externalPageField.get_value() == "true")) {
                window.top.zoneEditorShared.set_isPageRefreshControlled(true);
                form._checkForChanges = false;
                window.top.location = window.top.editorToolBar.get_cancelUrl();
            } else {
                var dataItem = args.get_dataItem();
                if (this._selectedSite) {
                    if (this._command == this._detailFormView._saveCommandName) {
                        dataItem.TargetSiteId = this._selectedSite.Id;
                        if (this._selectedLanguage)
                            dataItem.Language = this._selectedLanguage;
                    }
                    else {
                        if (dataItem.NavigateUrl.indexOf("?sf_site=") == -1)
                            dataItem.NavigateUrl += "?sf_site=" + this._selectedSite.Id;
                    }
                }

                if (isDuplicate) {
                    dialogBase.closeDuplicated(dataItem, form);
                } else {
                    dialogBase.closeCreated(dataItem, form);
                }
            }
            args.set_cancel(true);
        }
    },

    _updateParentFieldForLanguage: function (lang, rebind) {
        var binder = this._parentPageField.get_nodeSelector().get_treeBinder();
        binder.set_culture(lang);
        binder.set_uiCulture(lang);

        var clientManager = binder.get_manager();
        clientManager.set_culture(lang);
        clientManager.set_uiCulture(lang);

        if (rebind) {
            binder.DataBind();
        }
    },

    _showHideLanguageInvariantSettings: function (hide) {
        this._hideTemplateSection = (hide == true);
        this._toggleSectionByName("TemplateSection", hide);
        this._toggleElement(this._groupPageField.get_element(), hide);
        if (this._parentPageField) {
            this._toggleElement(this._parentPageField.get_element(), hide);
        }
    },
    //Shows/Hides settings available only when creating a language version of a page in split mode
    _showHideSplitPageSettings: function (hide) {
        this._toggleElement(this._showInNavField.get_element(), hide);
        //        this._toggleElement(this._urlField.get_element(), hide);
    },

    _targetSiteSelectedHandler: function (sender, site) {
        this._selectedSite = site;
        this._selectedLanguage = sender.get_selectedLanguage();
        this._binder._dataItem.ParentId = site.SiteMapRootNodeId;
        this._binder._dataItem.Parent.Id = site.SiteMapRootNodeId;
        this._binder._dataItem.RootId = site.SiteMapRootNodeId;
        this._binder._dataItem.Language = this._selectedLanguage;
        this._parentPageField.reset();

        var hasParent = jQuery(this._parentPageField.get_nodeSelector().get_nodesRadio()).is(":checked");

        var binder = this._parentPageField.get_nodeSelector().get_treeBinder();
        var urlParams = binder.get_urlParams();
        urlParams["siteId"] = site.Id;
        binder.set_urlParams(urlParams);
        binder.set_rootTaxonID(site.SiteMapRootNodeId);
        if (hasParent) {
            binder.DataBind();
        }
    },

    _targetSiteLanguageSelectedHandler: function (sender, lang) {
        this._selectedLanguage = lang;
        this._binder._dataItem.Language = this._selectedLanguage;
    },

    /* -------------------- private methods ----------- */
    _getFieldControlByDataFieldName: function (dataFieldName) {
        return this._binder.getFieldControlByDataFieldName(dataFieldName);
    },

    _toggleElement: function (elem, hide) {
        var elm = jQuery(elem);
        if (hide) {
            elm.hide();
        } else {
            elm.show();
        }
    },
    _toggleField: function (name, hide) {
        var field = this._getFieldControlByDataFieldName(name);
        if (!field)
            field = this._binder.getFieldControlByFieldName(name);
        if (field) {
            this._toggleElement(field.get_element(), hide);
        }
    },
    _toggleSectionByName: function (name, hide) {
        var sectionCount = detailFormView.get_sectionIds().length;
        for (var sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
            var sectionId = detailFormView.get_sectionIds()[sectionIndex];
            var section = $find(sectionId);
            if (section.get_name() == name) {
                var sectionElm = jQuery(section.get_element());
                if (hide) {
                    sectionElm.hide();
                } else {
                    sectionElm.show();
                }
                break;
            }
        }
    },

    _getWidgetByName: function (name) {
        var widget;
        var widgetBarIdsCount = detailFormView.get_widgetBarIds().length;
        while (widgetBarIdsCount--) {
            var widgetBarId = detailFormView.get_widgetBarIds()[widgetBarIdsCount];
            var widgetBar = $find(widgetBarId);
            if (widgetBar) {
                widget = widgetBar.getWidgetByName(name);
                break;
            }
        }
        return widget;
    },

    _toggleWidgetByName: function (name, hide) {
        //Why would that check be in this function, and not where it is called?
        if (detailFormView.get_createFormCommandName() == 'create' ||
            detailFormView.get_createFormCommandName() == 'createChild' ||
            detailFormView.get_createFormCommandName() == 'duplicate') {

            this._toggleWidgetByNameReal(name, hide);
        }
    },
    _toggleWidgetByNameReal: function (name, hide) {
        var widget = this._getWidgetByName(name);

        if (widget) {
            if (hide) {
                jQuery(widget.get_element()).hide();
            }
            else {
                jQuery(widget.get_element()).show();
            }
        }
    },

    /* -------------------- properties ---------------- */

}

Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension", Sys.Component, Sys.IDisposable);
