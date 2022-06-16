Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI.Designers");

Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner.initializeBase(this, [element]);

    this._siteSelectorViewChoiceField = null;
    this._urlTypeChoiceField = null;
    this._siteSelectorLabelTextBoxId = null;
    this._selectors = null;
    this._constants = null;

    this._languageSettingsChangeDelegate = null;
    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;
    this._createTemplateLinkClickDelegate = null;
    this._editTemplateLinkClickDelegate = null;
    this._templateValueChangedDelegate = null;
    this._modifyWidgetTemplatePermission = null;

    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._viewsList = null;
    this._dataFieldNameControlIdMap = null;
    this._currentViewName = null;
    this._radWindowManager = null;
    this._widgetEditorDialog = null;
    this._widgetEditorDialogUrl = null;
    this._widgetEditorShowDelegate = null;
    this._widgetEditorCloseDelegate = null;
    this._embeddedTemplateMap = [];
    this._defaultLayoutTemplateName = null;
    this._templateTitleElement = null;
}

Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner.prototype = {

    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner.callBaseMethod(this, 'initialize');

        this._selectors = {
            includeCurrentSiteCheckbox: "#includeCurrentSiteCheckbox",
            languageVersionSettingsCheckbox: "#languageVersionSettingsCheckbox",
            languageVersionSettingsContainer: "#languageVersionSettingsContainer",
            siteNamesAndLanguagesRationBtn: "#siteNamesAndLanguagesRationBtn",
            languagesOnlyRadioBtn: "#languagesOnlyRadioBtn"
        };

        this._constants = {
            siteSelectorViewName: "SiteSelectorControl"
        };

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        }
        if (this._onUnloadDelegate == null) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnloadHandler);
        }
        Sys.Application.add_load(this._onLoadDelegate);
        Sys.Application.add_unload(this._onUnloadDelegate);

        if (this._createTemplateLinkClickDelegate == null) {
            this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        }
        $addHandler(this.get_createTemplateLink(), "click", this._createTemplateLinkClickDelegate);

        if (this._editTemplateLinkClickDelegate == null) {
            this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        }
        $addHandler(this.get_editTemplateLink(), "click", this._editTemplateLinkClickDelegate);

        if (this._templateValueChangedDelegate == null) {
            this._templateValueChangedDelegate = Function.createDelegate(this, this._templateValueChanged);
        }

        if (this._widgetEditorShowDelegate == null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        }

        if (this._widgetEditorCloseDelegate == null) {
            this._widgetEditorCloseDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }

        this._languageSettingsChangeDelegate = Function.createDelegate(this, this._languageVersionSettingsValueChanged);
        $(this._selectors.languageVersionSettingsCheckbox).bind('change', this._languageSettingsChangeDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }

        if (this._onUnloadDelegate) {
            delete this._onUnloadDelegate;
        }

        $removeHandler(this._createTemplateLink, "click", this._createTemplateLinkClickDelegate);
        if (this._createTemplateLinkClickDelegate) {
            delete this._createTemplateLinkClickDelegate;
        }

        $removeHandler(this._editTemplateLink, "click", this._editTemplateLink);
        if (this._editTemplateLinkClickDelegate) {
            delete this._editTemplateLinkClickDelegate;
        }

        if (this._templateValueChangedDelegate) {
            delete this._templateValueChangedDelegate;
        }

        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }

        if (this._widgetEditorCloseDelegate) {
            delete this._widgetEditorCloseDelegate;
        }

        $(this._selectors.languageVersionSettingsCheckbox).unbind('change');
        if (this._languageSettingsChangeDelegate) {
            delete this._languageSettingsChangeDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    refreshUI: function () {
        var controlData = this._propertyEditor.get_control();

        this.get_siteSelectorViewChoiceField().set_value(controlData.SelectorType);
        this.get_urlTypeChoiceField().set_value(!controlData.UseLiveUrl);

        $(this.get_siteSelectorLabelTextBoxId()).val(controlData.Label);
        this.set_checked(this._selectors.includeCurrentSiteCheckbox, controlData.IncludeCurrentSite);

        var languageSettingsContainer = $(this._selectors.languageVersionSettingsContainer);
        if (controlData.ShowSiteNamesAndLanguages || controlData.ShowLanguagesOnly) {

            this.set_checked(this._selectors.languageVersionSettingsCheckbox, true);
            languageSettingsContainer.show();

            if (controlData.ShowSiteNamesAndLanguages) {
                this.set_checked(this._selectors.siteNamesAndLanguagesRationBtn, true);
            } else {
                this.set_checked(this._selectors.languagesOnlyRadioBtn, true);
            }
        } else {
            languageSettingsContainer.hide();
            $(this._selectors.languageVersionSettingsCheckbox).attr('checked', false);
        }

        // region edit/create templates
        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);

        if (controlData.TemplateKey) {
            c.set_value(controlData.TemplateKey);
        }
        else {
            templateKey = this._embeddedTemplateMap[this._defaultLayoutTemplateName];
            c.set_value(templateKey);
        }

        if (c.get_value() == "")
            jQuery(this.get_editTemplateLink()).hide();
        else {
            jQuery(this.get_editTemplateLink()).show();
        }
        // endregion
    },

    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        var displayAsValue = this.get_siteSelectorViewChoiceField().get_value();
        var labelValue = $(this.get_siteSelectorLabelTextBoxId()).val();
        var includeCurrentSite = $(this._selectors.includeCurrentSiteCheckbox).is(':checked');
        var useLiveUrl = (this.get_urlTypeChoiceField().get_value()) == "false" ? true : false;

        controlData.UseLiveUrl = useLiveUrl;
        controlData.SelectorType = displayAsValue;
        controlData.Label = labelValue;
        controlData.IncludeCurrentSite = includeCurrentSite;

        var showLanguageSettings = $(this._selectors.languageVersionSettingsCheckbox).is(':checked');
        if (showLanguageSettings) {
            controlData.ShowSiteNamesAndLanguages = $(this._selectors.siteNamesAndLanguagesRationBtn).is(':checked');
            controlData.ShowLanguagesOnly = $(this._selectors.languagesOnlyRadioBtn).is(':checked');
        }
        else {
            controlData.ShowSiteNamesAndLanguages = false;
            controlData.ShowLanguagesOnly = false;
        }

        // region edit/create templates
        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);

        var option = $(c._choiceElement).find("option:selected").get(0);
        if (option) {
            controlData.TemplateKey = option.value;
        }
        // endregion
    },

    resizeDialog: function () {
        dialogBase.resizeToContent();
    },

    set_checked: function (selector, checked) {
        $(selector).attr('checked', checked);
    },

    get_currentViewName: function () {
        return this._constants.siteSelectorViewName;
    },

    get_currentView: function () {
        return this._constants.siteSelectorViewName;
    },

    /* --------------------  events handlers ----------- */

    _onLoadHandler: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).add_valueChanged(this._templateValueChangedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _onUnloadHandler: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).remove_valueChanged(this._templateValueChangedDelegate);
        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            this._widgetEditorDialog.remove_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.remove_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _createTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = null;
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                $("body").removeClass("sfSelectorDialog");
                dialogBase.get_radWindow().maximize();
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();                
            }
        } else {
            alert("You don't have the permissions to create new widgets templates.");
        }
    },

    _editTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);
            var option = $(c._choiceElement).find("option:selected").get(0);
            this._selectedTemplateId = option.value;
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                $("body").removeClass("sfSelectorDialog");
                dialogBase.get_radWindow().maximize();
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
            }
        } else {
            alert("You don't have the permissions to edit widgets templates.");
        }
    },

    _templateValueChanged: function (sender) {
        var option = $(sender._choiceElement).find("option:selected").get(0);
        if (option) {
            jQuery(this.get_editTemplateLink()).show();
            this.get_currentView().TemplateKey = option.value;
        }
    },

    _languageVersionSettingsValueChanged: function (event) {
        var showLanguageSettings = event.target.checked;
        var languageSettingsContainer = $(this._selectors.languageVersionSettingsContainer);
        if (showLanguageSettings) {
            languageSettingsContainer.show();

            // set default selected option
            this.set_checked(this._selectors.siteNamesAndLanguagesRationBtn, true);

        } else {
            languageSettingsContainer.hide();
        }
        this.resizeDialog();
    },

    _onWidgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog) {
                var params =
                {
                    TemplateId: this._selectedTemplateId,
                    ControlType: "Telerik.Sitefinity.Multisite.Web.UI.SiteSelectorControl",
                    BlackListControlTemplateEditor: true
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    _onWidgetEditorClosed: function (sender, args) {
        dialogBase.get_radWindow().Restore();
        $("body").addClass("sfSelectorDialog");

        if (args && args.get_argument) {
            var arg = args.get_argument();
            if (arg) {
                if (arg.IsCreated) {
                    var widgetName = arg.DataItem.Name;
                    var widgetId = arg.DataItem.Id;

                    this.get_viewsList().addListItem(widgetId, widgetName);
                } else if (arg.IsUpdated) {
                    var selectedChoices = this.get_viewsList()._get_selectedListItemsElements();
                    if (selectedChoices) {
                        var selectedChoice = selectedChoices[0];
                        var newName = arg.DataItem.Name;

                        selectedChoice.text = newName;
                    }
                }
            }
        }
    },

    /* -------------------- properties ---------------- */

    get_siteSelectorViewChoiceField: function () {
        return this._siteSelectorViewChoiceField;
    },
    set_siteSelectorViewChoiceField: function (value) {
        this._siteSelectorViewChoiceField = value;
    },
    get_urlTypeChoiceField: function () {
        return this._urlTypeChoiceField;
    },
    set_urlTypeChoiceField: function (value) {
        this._urlTypeChoiceField = value;
    },
    get_siteSelectorLabelTextBoxId: function () {
        return this._siteSelectorLabelTextBoxId;
    },
    set_siteSelectorLabelTextBoxId: function (value) {
        this._siteSelectorLabelTextBoxId = value;
    },
    get_editTemplateLink: function () {
        return this._editTemplateLink;
    },
    set_editTemplateLink: function (value) {
        if (this._editTemplateLink != value) {
            this._editTemplateLink = value;
        }
    },
    get_createTemplateLink: function () {
        return this._createTemplateLink;
    },
    set_createTemplateLink: function (value) {
        if (this._createTemplateLink != value) {
            this._createTemplateLink = value;
        }
    },
    // gets the object which represents the map of field properties and respective controls
    // that are used to edit them
    set_dataFieldNameControlIdMap: function (value) {
        this._dataFieldNameControlIdMap = value;
    },
    // sets the object which represents the map of field properties and respective controls
    // that are used to edit them
    get_dataFieldNameControlIdMap: function () {
        return this._dataFieldNameControlIdMap;
    },
    get_viewsList: function () {
        return this._viewsList;
    },
    set_viewsList: function (value) {
        if (this._viewsList != value) {
            this._viewsList = value;
        }
    },
    get_radWindowManager: function () {
        return this._radWindowManager;
    },
    set_radWindowManager: function (value) {
        if (this._radWindowManager != value) {
            this._radWindowManager = value;
        }
    },
    get_templateTitleElement: function () {
        return this._templateTitleElement;
    },

    set_templateTitleElement: function (value) {
        if (this._templateTitleElement != value) {
            this._templateTitleElement = value;
        }
    }
}

Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner.registerClass('Telerik.Sitefinity.Multisite.Web.UI.Designers.SiteSelectorControlDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);