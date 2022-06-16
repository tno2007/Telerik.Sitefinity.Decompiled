﻿// called by the DetailFormView when it is loaded
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
    this._detailFormView = {};
    this._binder = null;
    this._emptyGuid = "00000000-0000-0000-0000-000000000000";

    this._dataItem = null;
    this._translationSyncedWarningDialog = null;

    this._onDataBindDelegate = null;
    this._formCreatedDelegate = null;
    this._formClosingDelegate = null;
    this._continueToEditPageDelegate = null;
    this._copyLanguageChangedDelegate = null;
    this._isNewLanguageVersion = false;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.callBaseMethod(this, "initialize");

        this._binder = this._detailFormView.get_binder();

        if (this._detailFormView) {
            this._formCreatedDelegate = Function.createDelegate(this, this._formCreatedHandler);
            this._detailFormView.add_formCreated(this._formCreatedDelegate);

            this._formClosingDelegate = Function.createDelegate(this, this._formClosingHandler);
            this._detailFormView.add_formClosing(this._formClosingDelegate);

            this._translationSyncedWarningDialog = this._detailFormView.getPromptDialogByName("translationSyncedWarningDialog");
        }
    },

    dispose: function () {
        if (this._onDataBindDelegate) {
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

        if (this._formClosingDelegate) {
            if (this._detailFormView) {
                this._detailFormView.remove_formCreated(this._formClosingDelegate);
            }
            delete this._formClosingDelegate;
        }

        if (this._continueToEditPageDelegate) {
            delete this._continueToEditPageDelegate;
        }

        Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.callBaseMethod(this, "dispose");
    },

    _formCreatedHandler: function (sender, args) {
        var dataItem = this._binder.get_dataItem();
        this._dataItem = dataItem;
        var detailFormView = this._detailFormView;

        //This is needed because the binder dataItem is sometimes changed after that (on dataBind) and we need to preserve the source object and language
        if (!this._onDataBindDelegate) {
            this._onDataBindDelegate = Function.createDelegate(this, this._onDataBindHandler);
            detailFormView.add_onDataBind(this._onDataBindDelegate);
        }

        var commandArgument = args.get_commandArgument();
        if (!args.get_isNew() && args.get_commandName() == "edit") {
            this._toggleSectionByName("CopyLanguageSection", true);
        }
        else {
            if (commandArgument && commandArgument.language && commandArgument.sourceObjectId != this._guidEmpty) {
                dataItem.Language = commandArgument.language;
                dataItem.SourceLanguagePageId = commandArgument.sourceObjectId;

                var hideReturnToPages = commandArgument.isFromEditor;

                this._binder.set_uiCulture(dataItem.Language);

                this._toggleSectionByName("TemplateSection", true);
                this._toggleSectionByName("CopyLanguageSection", false);
                this._toggleWidgetByName("CreateAndReturnWidgetElement", hideReturnToPages);
                this._isNewLanguageVersion = true;

                var copyLanguageField = this._getFieldControlByDataFieldName("AvailableLanguages");
                dataItem.SourceLanguage = this._detailFormView._defaultLanguage;
                copyLanguageField.set_selectedLanguage(this._detailFormView._defaultLanguage, false);
                if (!this._copyLanguageChangedDelegate) {
                    this._copyLanguageChangedDelegate = Function.createDelegate(this, this._copyLanguageChanged);
                    copyLanguageField.add_valueChanged(this._copyLanguageChangedDelegate);
                }
            }
            else {
                this._toggleSectionByName("TemplateSection", false);
                this._toggleSectionByName("CopyLanguageSection", true);

                if (args.get_commandName() == "duplicate") {
                    // Do not allow the user to change the framework on duplicate.
                    this._toggleFrameworkOptions(true);
                }
                //Set language for the template selector
                if (commandArgument && commandArgument.language) {
                    var templateSelectorField = this._getFieldControlByDataFieldName("Template");
                    templateSelectorField.set_language(commandArgument.language);
                }
            }
        }
    },
    _toggleFrameworkOptions: function (bDisable) {
        var frameworkField = this._getFieldControlByDataFieldName("Framework");
        if (frameworkField)
            jQuery("input", frameworkField.get_choiceElement()).prop('disabled', bDisable);
    },
    _copyLanguageChanged: function (elm) {
        this._binder.get_dataItem().SourceLanguage = elm.get_value();
    },

    _onDataBindHandler: function (sender, dataItem) {
        dataItem.Item.Language = this._dataItem.Language;
        dataItem.Item.SourceLanguagePageId = this._dataItem.SourceLanguagePageId;

        var copyLanguageField = this._getFieldControlByDataFieldName("AvailableLanguages");
        if (copyLanguageField) {
            dataItem.Item.SourceLanguage = this._detailFormView._defaultLanguage;
            copyLanguageField.set_selectedLanguage(this._detailFormView._defaultLanguage, false);
        }
    },

    _formClosingHandler: function (sender, args) {
        var form = sender;
        var commandArgument = args.get_commandArgument();
        var isNew = args.get_isNew();

        if (form.get_isMultilingual() && commandArgument) {
            isNew = commandArgument.languageMode == "create";
        }

        if (isNew && args.get_isDirty()) {
            var widgetCommandName = form.get_widgetCommandName();

            if (widgetCommandName == form.get_createCommandName() && this._isNewLanguageVersion == true) {
                this._continueToEditPageDelegate = Function.createDelegate(this, this._continueToEditPage);
                this._translationSyncedWarningDialog.show_prompt('', '', this._continueToEditPageDelegate, { dataItem: args.get_dataItem(), context: form });

            }
            else {
                dialogBase.closeCreated(args.get_dataItem(), form);
            }
            args.set_cancel(true);
        }
    },

    _continueToEditPage: function (sender, args) {
        var dataItem = args.get_commandArgument().dataItem;
        var context = args.get_commandArgument().context;
        var cancelEdit = args.get_commandName() == 'cancel';

        if (cancelEdit) {
            dialogBase.closeCreated(dataItem, null);
        }
        else {
            dialogBase.closeCreated(dataItem, context);
        }
    },

    _getWidgetByName: function (name) {
        var widget;
        var widgetBarIdsCount = this._detailFormView.get_widgetBarIds().length;
        while (widgetBarIdsCount--) {
            var widgetBarId = this._detailFormView.get_widgetBarIds()[widgetBarIdsCount];
            var widgetBar = $find(widgetBarId);
            if (widgetBar) {
                widget = widgetBar.getWidgetByName(name);
                break;
            }
        }
        return widget;
    },

    _getFieldControlByDataFieldName: function (dataFieldName) {
        return this._binder.getFieldControlByDataFieldName(dataFieldName);
    },

    _toggleSectionByName: function (name, hide) {
        var sectionIds = this._detailFormView.get_sectionIds();
        var sectionCount = sectionIds.length;

        for (var sectionIndex = 0; sectionIndex < sectionCount; sectionIndex++) {
            var sectionId = sectionIds[sectionIndex];
            var section = $find(sectionId);
            if (section.get_name() == name) {
                var sectionElm = jQuery(section.get_element());
                if (hide) {
                    sectionElm.hide();
                }
                else {
                    sectionElm.show();
                }
                break;
            }
        }
    },

    _toggleWidgetByName: function (name, hide) {
        var widget = this._getWidgetByName(name);

        if (widget) {
            if (hide) {
                jQuery(widget.get_element()).hide();
            }
            else {
                jQuery(widget.get_element()).show();
            }
        }
    }
}

Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.DetailFormViewExtension", Sys.Component, Sys.IDisposable);