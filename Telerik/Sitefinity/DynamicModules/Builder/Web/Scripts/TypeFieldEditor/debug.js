///<reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.4.js" />

$(document).ready(function () {

    var forbiddenPropertyNames = ["DynamicContent", "dynamicContent", "ParentItem"];
    var forbiddenFieldNames = ["base_id", "originalContentId", "lastModifiedBy", "transaction", "provider", "lastModified", "applicationName", "status", "languageData", "version", "visible", "publishedTranslations",
        "owner", "publicationDate", "expirationDate", "approvalTrackingRecordMap", "urlName", "approvalWorkflowState", "urls", "organizer", "lifecycle", "id", "statusDisplayName", "originalContentId", "lastModifiedBy",
        "approvalWorkflowState", "author", "AutoGenerateUniqueUrl", "systemParentType", "systemParentId", "systemParentItem", "systemLiveParentItemCache", "systemUrl", "MetaTitle", "MetaDescription", "OpenGraphTitle", 
        "OpenGraphDescription", "OpenGraphImage", "OpenGraphVideo", "true", "false", "null", "it", "if", "new", "Object", "Boolean", "Char", "String", "SByte", "Byte", "Int16", "UInt16", "Int32", "UInt32", "Int64", 
        "UInt64", "Single", "Double", "Decimal", "DateTime", "TimeSpan", "Guid", "Math", "Convert", "Translations", "Actions", "Children", "Keywords"];

    var emptyField = {
        "Name": "",
        "TypeName": "",
        "TypeUIName": "",
        "IsHiddenField": false,
        "WidgetTypeName": "",
        "FrontendWidgetTypeName": "",
        "FrontendWidgetLabel": "",
        "MinNumberRange": 0,
        "MaxNumberRange": 0,
        "DBType": "",
        "MediaType": "",
        "ClassificationId": "00000000-0000-0000-0000-000000000000",
        "DecimalPlacesCount": 0,
        "NumberUnit": "",
        "DBLength": "",
        "AllowNulls": false,
        "DisableLinkParser": false,
        "IncludeInIndexes": false,
        "ColumnName": "",
        "CanSelectMultipleItems": true,
        "CanCreateItemsWhileSelecting": true,
        "Title": "",
        "InstructionalText": "",
        "DefaultValue": "",
        "IsRequired": false,
        "IsLocalizable": false,
        "MinLength": 0,
        "MaxLength": 0,
        "LengthValidationMessage": "",
        "Choices": "",
        "CheckedByDefault": false,
        "InstructionalChoice": "",
        "ChoiceRenderType": "RadioButton",
        "IsRequiredToSelectDdlValue": false,
        "IsRequiredToSelectCheckbox": false,
        "RegularExpression": null,
        "AllowMultipleImages": true,
        "AllowMultipleVideos": true,
        "AllowMultipleFiles": true,
        "AllowImageLibrary": false,
        "ImageMaxSize": 0,
        "ImageExtensions": "",
        "FileMaxSize": 0,
        "FileExtensions": "",
        "VideoMaxSize": 0,
        "VideoExtensions": "",
        "AddressFieldMode": "FormOnly",
        "RelatedDataType": "",
        "RelatedDataProvider": ""
    };

    var fieldClassificationId;
    var isEditMode;
    var oldWidgetName;
    var emptyGuid = '00000000-0000-0000-0000-000000000000';
    var selectors = {
        allCancelButtons: ".sfCancel",
        step1: "#typeFieldEditorStep1",
        step2: "#typeFieldEditorStep2",
        continueStep1Button: "#continueStep1Button",
        backStep2Button: "#backStep2Button",
        doneStep2Button: "#doneStep2Button",
        fieldName: "#typeFieldEditor_fieldName",
        fieldType: "#typeFieldEditor_fieldType",
        fieldTypeTitle: "#typeFieldEditorFieldTitle",
        defaultTitle: "#typeFieldEditorTitle",

        fieldNameText: "#typeFieldEditor_fieldName",
        fieldLabelText: "#typeFieldEditor_fieldTitle",
        forbiddenNames: "#forbiddenNames",
        minRangeNumber: "#typeFieldEditor_minRange",
        maxRangeNumber: "#typeFieldEditor_maxRange",
        checkedByDefault: "#typeFieldEditor_checkedByDefault",
        defaultDateValue: "#typeFieldEditor_defaultDateValue",
        minDateValue: "#typeFieldEditor_minDate",
        maxDateValue: "#typeFieldEditor_maxDate",
        numberUnit: "#typeFieldEditor_numberUnit",
        usedPropertyNames: "#usedPropertyNames",
        openReservedNamesWindow: "#openReservedNamesWindow",
        reservedName: "#reservedName",
        reservedNamesErrorBox: "#reservedNamesErrorBox",
        reservedNamesListView: "#reservedNamesListView",
        reservedNamesWindow: "#reservedNamesWindow",
        editorSelectedFieldContainer: "#editorSelectedFieldContainer",
        // multiple choice editor fields
        isMultiple: "#typeFieldEditor_canSelectMultiple",
        canSelectMultipleBox: "#typeFieldEditor_CanSelectMultipleBox",
        singleChoiceSelectedItem: "#typeFieldEditor_widgetType_singleChoice option:selected",
        multipleChoiceSelectedItem: "#typeFieldEditor_widgetType_multipleChoice option:selected",
        selectedMultipleChoiceTypeLiteral: "#selectedMultipleChoiceTypeLiteral",
        instructionalChoice: "#typeFieldEditor_instructionalChoice",
        isntructionalChoiceBox: "#typeFieldEditor_instructionalChoiceBox",
        questionText: "#typeFieldEditor_fieldTitleMultipleChoice",
        dropDownListMakeRequiredCheckBox: "#typeFieldEditor_dropDownListMakeRequiredCheckbox",
        dropDownListMakeRequiredCheckBoxContainer: "#typeFieldEditor_dropDownListMakeRequiredBox",
        oneRequiredCheckBox: "#typeFieldEditor_CheckboxesOneRequiredCheckBox",
        oneRequiredCheckBoxContainer: "#typeFieldEditor_CheckboxesOneRequiredBox",

        // classification editor fields
        canSelectMultipleClassificationItems: "#typeFieldEditor_multipleClassificationItems",
        singleClassificationItem: "#typeFieldEditor_singleClassificationItem",
        canCreateClassificationItemsWhenSelecting: "#typeFieldEditor_canCreateItemsWhenSelecting",
        finishHiddenFieldButton: "#finishHiddenField",
        mediaRadioButtonsBox: "#typeFieldEditor_mediaTypeRadioButtons",
        mediaDropDown: "#typeFieldEditor_mediaDropDown",
        moduleTypesDropDown: "#typeFieldEditor_moduleTypesDropDown",
        limitationsTab: "#typeFieldEditor_limitationsTab",

        // save this values in hidden fields so that they are not cleared on save of the field
        parentSectionId: "#typeFieldEditor_hidden_parentSectionId",
        showInGrid: "#typeFieldEditor_hidden_showInGrid",
        gridColumnOrdinal: "#typeFieldEditor_hidden_gridColumnOrdinal",

        isHiddenField: "#typeFieldEditor_isHiddenField",
        isHiddenBox: "#typeFieldEditor_isHiddenBox",
        widgetType: {
            label: "#widgetTypeLabel",
            allTypes: ".sfTypeWidgets",
            shortText: "#typeFieldEditor_widgetType_shortText",
            longText: "#typeFieldEditor_widgetType_longText",
            singleChoice: "#typeFieldEditor_widgetType_singleChoice",
            multipleChoice: "#typeFieldEditor_widgetType_multipleChoice",
            yesNo: "#typeFieldEditor_widgetType_yesNo",
            currency: "#typeFieldEditor_widgetType_currency",
            dateTime: "#typeFieldEditor_widgetType_dateTime",
            number: "#typeFieldEditor_widgetType_number",
            hierarchicalClassification: "#typeFieldEditor_widgetType_hierarchicalClassification",
            flatClassification: "#typeFieldEditor_widgetType_flatClassification",
            unknown: "#typeFieldEditor_widgetType_unknown",
            imageMedia: "#typeFieldEditor_widgetType_imageMediaType",
            videoMedia: "#typeFieldEditor_widgetType_videoMediaType",
            fileMedia: "#typeFieldEditor_widgetType_fileMediaType",
            guid: "#typeFieldEditor_widgetType_guid",
            guidArray: "#typeFieldEditor_widgetType_guidArray",
            address: "#typeFieldEditor_widgetType_addressType"
        },
        mediaType: {
            canUploadSingleImage: "#typeFieldEditor_canUploadSingleImage",
            allowMultipleImages: "#typeFieldEditor_canUploadMultipleImages",
            canUploadSingleVideo: "#typeFieldEditor_canUploadSingleVideo",
            allowMultipleVideos: "#typeFieldEditor_canUploadMultipleVideos",
            canUploadSingleFile: "#typeFieldEditor_canUploadSingleFile",
            allowMultipleFiles: "#typeFieldEditor_canUploadMultipleFiles",
            allowImageLibrary: "#typeFieldEditor_canAttachImageLibrary",
            imageMaxSize: "#typeFieldEditor_maxImageFileSize",
            imageExtensions: "#typeFieldEditor_imageFileExtensions",
            videoMaxSize: "#typeFieldEditor_maxVideoFileSize",
            videoExtensions: "#typeFieldEditor_videoFileExtensions",
            fileMaxSize: "#typeFieldEditor_maxFileFileSize",
            fileExtensions: "#typeFieldEditor_fileFileExtensions"
        },
        addressFieldMode: {
            formOnly: "#typeFieldEditor_addressFieldFormOnly",
            mapOnly: "#typeFieldEditor_addressFieldMapOnly",
            formMap: "#typeFieldEditor_addressFieldFormMap"
        },
        addressModeBox: "#typeEditor_addressModeBox",
        customWidgetTypeBox: "#typeFieldEditor_customWidgetBox",
        customWidgetTypeName: "#typeFieldEditor_customWidgetType",
        decimalPlacesBox: "#typeFieldEditor_decimalPlacesBox",
        decimalPlacesCurrencyBox: "#typeFieldEditor_decimalPlacesCurrencyBox",
        classificationBox: "#typeFieldEditor_classificationBox",
        classificationDropdown: "#typeFieldEditor_classification",
        metaFieldBox: "#typeFieldEditor_SeoBox",
        metaFieldDropdown: "#typeFieldEditor_seoDropDown",
        openGraphFieldBox: "#typeFieldEditor_OpenGraphBox",
        openGraphFieldDropdown: "#typeFieldEditor_OpenGraphDropDown",
        dbPrecision: "#typeFieldEditor_dbPrecision",
        dbPrecisionBox: "#typeFieldEditor_dbPrecisionBox",
        dbScale: "#typeFieldEditor_dbScale",
        dbScaleBox: "#typeFieldEditor_dbScaleBox",
        dbLengthBox: "#typeFieldEditor_dbLengthBox",
        dbLength: "#typeFieldEditor_dbLength",
        dbTypeDropdown: "#typeFieldEditor_dbType",
        decimalPlacesCount: "#typeFieldEditor_decimalPlaces",
        currencyDecimalPlacesCount: "#typeFieldEditor_decimalPlacesCurrency",
        allowNulls: "#typeFieldEditor_allowNullValues",
        disableLinkParser: "#typeFieldEditor_disableLinkParser",
        includeInIndexes: "#typeFieldEditor_includeInIndexes",
        columnName: "#typeFieldEditor_columnName",
        advancedExpand: "#typeFieldEditor_advancedOptionsExpand",
        advancedSection: "#typeFieldEditor_advancedSection",
        advancedSectionBox: "#typeFieldEditor_advancedSectionBox",
        moreOptionsExpand: "#typeFieldEditor_moreOptionsExpand",
        moreOptionsSection: "#typeFieldEditor_moreOptionsSection",
        moreOptionsBox: "#typeFieldEditor_moreOptionsBox",
        fieldTitle: ".sfFieldTitle",
        instructionalText: "#typeFieldEditor_instructionalText",
        defaultValue: "#typeFieldEditor_defaultValue",
        isRequired: "#typeFieldEditor_isRequired",
        isLocalizable: "#typeFieldEditor_isLocalizable",
        minLength: "#typeFieldEditor_minLength",
        maxLength: "#typeFieldEditor_maxLength",
        recommendedCharactersCount: "#typeFieldEditor_recommendedCharactersCount",
        regExValidationValue: "#typeFieldEditor_regExValidationValue",
        choicesEditor: {
            choiceList: "#typeEditor_multipleChoiceList",
            choiceItem: ".sfChoiceItem",
            choiceItemTemplate: "#typeFieldEditor_choiceItemTemplate",
            choiceItemTemplateObsolete: "#typeFieldEditor_choiceItemTemplate_obsolete",
            choiceItemTemplateWithRadio: "#typeFieldEditor_choiceItemTemplateWithRadio",
            addChoice: ".sfAddChoice",
            removeChoice: ".sfRemoveChoice",
            choiceListHeader: "#typeEditor_multipleChoiceList_header",
            choiceLabelDetailsLink: "#choiceLabelDetailsLink",
            choiceLabelDetailsTooltip: "#choiceLabelDetailsTooltip",
            choiceValueDetailsLink: "#choiceValueDetailsLink",
            choiceValueDetailsTooltip: "#choiceValueDetailsTooltip",
            howToTranslateDetailsLink: "#howToTranslateDetailsLink",
            howToTranslateDetailsTooltip: "#howToTranslateDetailsTooltip"

        },
        addressField: {
            isGoogleMapApiKeyValid: "#isGoogleMapApiKeyValid",
            googleMapsApiDescription: "#googleMapsApiDescription",
            checkedAddressFieldMode: "input[name='addressFieldMode']:checked"
        }
    };

    var validation = new sitefinityValidation();
    var _settings;
    var _typeFieldEditor;
    var fieldEditor = {
        defaultWidgetTypeNameRelatedMedia: "default",
        defaultWidgetTypeNameRelatedData: "Telerik.Sitefinity.Web.UI.Fields.RelatedDataField",
        defaultFrontendWidgetTypeNameRelatedData: "Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView",
        pagesType: "Telerik.Sitefinity.Pages.Model.PageNode",
        inlineFrontendControl: 'inline',
        dataTypes: null,
        model: null,
        initialize: function (siteBaseUrl) {
            var that = this;

            this.siteBaseUrl = siteBaseUrl;
            this.configure(siteBaseUrl);

            require(["DataTypes"], function (DataTypes) {
                var dataTypes = new DataTypes();
                $(selectors.moduleTypesDropDown).html(dataTypes.template);
                dataTypes.initialize(siteBaseUrl);
                that.dataTypes = dataTypes;
            });
        },

        configure: function (siteBaseUrl) {
            requirejs.config({
                baseUrl: siteBaseUrl + "Res",
                paths: {
                    FieldTypeEditorFactory: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldTypeEditorFactory',
                    RelatedData: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.RelatedData',
                    RelatedMedia: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.RelatedMedia',
                    ContentRepository: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.',
                    DataTypes: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.DataTypes',

                    DataTypesTemplate: 'Telerik.Sitefinity.ModuleEditor.Web.Templates.DataTypes.sfhtml',
                    RelatedDataTemplate: 'Telerik.Sitefinity.ModuleEditor.Web.Templates.RelatedData.sfhtml',
                    RelatedMediaTemplate: 'Telerik.Sitefinity.ModuleEditor.Web.Templates.RelatedMedia.sfhtml'
                },
                map: {
                    '*': {
                        text: siteBaseUrl + "ExtRes/Telerik.Sitefinity.Resources.Scripts.RequireJS.text.js",
                    }
                },
                waitSeconds: 0
            });
        },

        open: function (model, isEditMode) {

            // if we are creating new field now, we do not have the definitions set
            var that = this,
                field = {};

            if (model.field) {
                field = model.field;
            }

            this.model = model;
            this.prepare(field, isEditMode);

            var moduleTypeName = that.getModuleTypeName();

            $(model.mainFormSelector).hide();
            options = {
                dataModel: {},
                mediaType: {
                    isImage: field.MediaType == "image",
                    isVideo: field.MediaType == "video",
                    isFile: field.MediaType == "file"
                },
                isNotOpenGraphMedia: model.isNotOpenGraphMedia,
                moduleTypeName: moduleTypeName,
                dataType: {
                    isPage: moduleTypeName === "Pages",
                    isContentType: moduleTypeName === "DynamicType",
                    isNews: moduleTypeName === "News",
                    isEvents: moduleTypeName === "Events",
                    isBlogs: moduleTypeName === "Blogs",
                    isBlogPosts: moduleTypeName === "Blog Posts",
                    isProductType: moduleTypeName === "ProductType"
                },
                isEditMode: isEditMode,
                field: field,
                siteBaseUrl: that.siteBaseUrl
            };

            // from now on - field definitions should be in the required for the field format (RelatedMedia): 
            // WidgetTypeName, Title, InstructionalText, IsRequired, FrontendWidgetTypeName, FrontendWidgetLabel, 
            // AllowMultipleVideos, AllowMultipleFiles, AllowMultipleFiles, AllowMultipleImages, MaxFileSize, FileExtensions

            require(["FieldTypeEditorFactory"], function (fieldTypeEditorFactory) {
                fieldTypeEditorFactory.createField(model.fieldType, options, function (designer) {
                    if (designer) {
                        $(model.container).html(designer.template);
                        designer.initialize();
                        $(model.container).show();
                        kendo.bind($(model.container), designer.viewModel);

                        //Handle onClose and onDone Triggered from the field type designer
                        $(designer).on('onFieldCancel', function (event, sender) {
                            that.cancel();
                        });

                        $(designer).on('onFieldDone', function (event, sender) {
                            that.done(designer.modifiedDataModel.field);
                        });

                        $(designer).on('onFieldBack', function (event, sender) {
                            if (designer.modifiedDataModel.field.Name === "OpenGraphImage" || designer.modifiedDataModel.field.Name === "OpenGraphVideo") {
                                $(selectors.fieldType).val("OpenGraph");
                            }

                            that.back();
                        });
                    }
                });
            });
        },

        cancel: function () {
            if (this.model) {
                $(this.model.mainFormSelector).show();
                $(this.model.container).hide();
            }
            methods["reset"].apply();
            _settings.onCancel(_typeFieldEditor);
        },
        back: function () {
            if (this.model) {
                $(this.model.mainFormSelector).show();
                $(this.model.container).hide();
            }
        },
        done: function (field) {

            // this function should format the definitions received from the designer so that they comply with the dynamic field context
            if (this.model) {
                $(this.model.mainFormSelector).show();
                $(this.model.container).hide();
            }
            if (isEditMode) {
                _settings.onEditDone(_typeFieldEditor, { "Field": field });
            }
            else {
                _settings.onDone(_typeFieldEditor, { "Field": field });
            }
            methods["reset"].apply();
        },
        prepare: function (field, isEditMode) {

            // this method should prepare the definitions received from the dynamic context in a format appropriate for the related media field
            if (!isEditMode) {
                $.extend(true, field, emptyField);

                field.Name = $(selectors.fieldName).val();
                field.TypeName = $(selectors.fieldType).val();
                field.TypeUIName = $(selectors.fieldType + " option:selected").text();
                field.IsHiddenField = $(selectors.isHiddenField).is(":checked");

                // we are creating new field, so set default values for type names:
                field.MediaType = (field.MediaType === '') && (this.model.fieldType === 'RelatedMedia') ?
                    $("#typeFieldEditor_mediaDropDown_select").val() : field.MediaType;

                field.RelatedDataType = (field.RelatedDataType === '') && (this.model.fieldType === 'RelatedData') ?
                    this.getModuleType() : field.RelatedDataType;

                var relatedDataTypeName = null;

                if (this.model.fieldType === 'RelatedData') {
                    field.WidgetTypeName = this.defaultWidgetTypeNameRelatedData;
                    field.FrontendWidgetTypeName = this.inlineFrontendControl;
                    relatedDataTypeName = this.dataTypes.dataTypesDropdown.text().replace(/\([^\)]*\)/g, '').trim();
                }

                if (this.model.fieldType === 'RelatedMedia') {
                    field.WidgetTypeName = this.defaultWidgetTypeNameRelatedMedia;
                    field.FrontendWidgetTypeName = this.inlineFrontendControl;
                    field.RelatedDataProvider = (field.RelatedDataProvider === '') ? $('#mediaDataProviders').val() : field.RelatedDataProvider;
                    relatedDataTypeName = $("#typeFieldEditor_mediaDropDown_select option:selected").text().trim();
                }

                if (field.Name === "OpenGraphImage") {
                    field.MediaType = "image";
                    relatedDataTypeName = "Images";
                    field.Title = "Image";
                    field.TypeUIName = "Social media (OpenGraph)";
                    field.AllowMultipleImages = false;
                    // Select allowed/recommended media extension for OpenGraph image
                    field.FileExtensions = ".jpg,.jpeg,.png,.gif";
                }

                if (field.Name === "OpenGraphVideo") {
                    field.MediaType = "video";
                    relatedDataTypeName = "Video";
                    field.Title = "Video";
                    field.TypeUIName = "Social media (OpenGraph)";
                    field.AllowMultipleVideos = false;
                    // Select allowed/recommended media extension for OpenGraph video
                    field.FileExtensions = ".mp4";
                }

                var fieldLabelText = relatedDataTypeName ? String.format("Related {0}", relatedDataTypeName.toLowerCase()) : field.Name;
                if (field.Title === "") {
                    field.Title = fieldLabelText;
                }
                if (field.FrontendWidgetLabel === "" && this.model.fieldType !== 'RelatedMedia') {
                    field.FrontendWidgetLabel = fieldLabelText;
                }
            }
        },

        getModuleType: function () {
            return this.dataTypes.dataTypesDropdown.value();
        },

        getModuleTypeName: function () {
            var ddDataTypeText = null;

            /* Products module type name */

            if (this.dataTypes && this.dataTypes.dataTypesDropdown && !isEditMode) {
                ddDataTypeText = this.dataTypes.dataTypesDropdown.value();
            } else {
                ddDataTypeText = this.model.field.RelatedDataType;
            }

            if (ddDataTypeText && ddDataTypeText.indexOf("Telerik.Sitefinity.DynamicTypes.Model.sf_ec_prdct_") === 0) {
                return "ProductType";
            }

            /* Products module type name */

            var moduleTypeNames = ["Pages", "News", "Blogs", "Blog Posts", "Events"],
                moduleTypes = [
                    "Telerik.Sitefinity.Pages.Model.PageNode",
                    "Telerik.Sitefinity.News.Model.NewsItem",
                    "Telerik.Sitefinity.Blogs.Model.Blog",
                    "Telerik.Sitefinity.Blogs.Model.BlogPost",
                    "Telerik.Sitefinity.Events.Model.Event"],
                index = null;

            if (this.dataTypes && this.dataTypes.dataTypesDropdown && !isEditMode) {
                ddDataTypeText = this.dataTypes.dataTypesDropdown.text();
                index = moduleTypeNames.indexOf(ddDataTypeText);
            }
            else {
                ddDataTypeText = this.model.field.RelatedDataType;
                index = moduleTypes.indexOf(ddDataTypeText);
            }

            if (index != -1) {
                return moduleTypeNames[index];
            }

            return "DynamicType";
        }
    };
    // methods of the typeFieldEditor plugin
    var methods = {

        // this function initializes the plugin in
        init: function (settings) {
            _settings = settings;
            _typeFieldEditor = this;

            $(".sfError").remove();
            // hide all type specific UI and show short text field ui (default)
            methods["hideTypeSpecificUI"].apply();
            methods["prepareShortTextUI"].apply();
            $(selectors.fieldTypeTitle).hide();

            $(selectors.openReservedNamesWindow).click(function () {
                $(selectors.reservedNamesWindow).toggle();
            });

            // wire up field type changed event
            $(selectors.fieldType).change(function () {

                methods["hideTypeSpecificUI"].apply();

                methods["showAppropriateUI"].apply(undefined, [$(this).val()]);
            });

            $(selectors.fieldName).on("input", function () {
                methods["validateLength"].apply(undefined, [$(this).val()]);
            });

            // wire up the changing of the dropdownlist value for the classification field
            $(selectors.classificationDropdown).change(function () {
                var classificationText = $(selectors.classificationDropdown + " option:selected").text();
                classificationText = classificationText.replace(/ /g, "");
                $(selectors.fieldName).val(classificationText);
                var classificationType = $(selectors.classificationDropdown + " option:selected").attr("data-Type");
                switch (classificationType) {
                    case "HierarchicalTaxonomy":
                        $(selectors.widgetType.flatClassification).hide();
                        $(selectors.widgetType.hierarchicalClassification).show();
                        break;
                    case "FlatTaxonomy":
                        $(selectors.widgetType.hierarchicalClassification).hide();
                        $(selectors.widgetType.flatClassification).show();
                        break;
                }
                fieldClassificationId = $(selectors.classificationDropdown).val();
            });

            // wire up radio buttons of the media type field
            $("input[name='mediaFieldType']").each(function () {
                $(this).click(function () {
                    methods["hideTypeSpecificUI"].apply();
                    methods["prepareMediaUI"].apply();
                });
            });

            // wire up the changing of the dropdownlist value for the Meta field
            $(selectors.metaFieldDropdown).change(function () {
                var metaFieldName = $(selectors.metaFieldDropdown + " option:selected").val();
                metaFieldName = metaFieldName.replace(/ /g, "");
                $(selectors.fieldName).val(metaFieldName);
            });

            // wire up the changing of the dropdownlist value for the OpenGraph field
            $(selectors.openGraphFieldDropdown).change(function () {
                var metaFieldName = $(selectors.openGraphFieldDropdown + " option:selected").val();
                metaFieldName = metaFieldName.replace(/ /g, "");
                $(selectors.fieldName).val(metaFieldName);

                if (metaFieldName === "OpenGraphImage" || metaFieldName === "OpenGraphVideo") {
                    // preaprae data sources
                    methods["prepareRelatedMediaSources"].apply(this, [true]);
                }
                else {
                    $(selectors.mediaDropDown).hide();
                }
            });

            // wire up widget type selectors
            $(selectors.widgetType.allTypes).change(function () {
                if ($(this).val() == "Custom") {
                    $(selectors.customWidgetTypeBox).show();
                }
                else {
                    $(selectors.customWidgetTypeBox).hide();
                }
            });

            // wire up expand and collapse advanced section buttons
            $(selectors.advancedExpand).click(function () {
                methods["expandAdvancedSection"].apply();
            });

            // wire up expand and collapse more options section buttons
            $(selectors.moreOptionsExpand).click(function () {
                methods["expandMoreOptionsSection"].apply();
            });

            // wire up cancel buttons
            $(selectors.step2).find(selectors.allCancelButtons).each(function () {
                $(this).bind("click", cancelClick);
            });

            $(selectors.step1).find(selectors.allCancelButtons).each(function () {
                $(this).bind("click", cancelClick);
            });

            $(selectors.choicesEditor.choiceLabelDetailsLink).click(function () {
                $(selectors.choicesEditor.choiceLabelDetailsTooltip).toggleClass("sfDisplayNone");
            });

            $(selectors.choicesEditor.choiceValueDetailsLink).click(function () {
                $(selectors.choicesEditor.choiceValueDetailsTooltip).toggleClass("sfDisplayNone");
            });

            $(selectors.choicesEditor.howToTranslateDetailsLink).click(function () {
                $(selectors.choicesEditor.howToTranslateDetailsTooltip).toggleClass("sfDisplayNone");
            });

            function cancelClick() {
                // reset the form to it's initial state
                methods["reset"].apply();
                // raise onCancel event
                settings.onCancel(_typeFieldEditor);
            }

            var usedPropNames = $(selectors.usedPropertyNames).val();
            if (usedPropNames) {
                usedPropNames = JSON.parse(usedPropNames);
                forbiddenPropertyNames = forbiddenPropertyNames.concat(usedPropNames);
            }

            $(selectors.reservedNamesListView).kendoListView({
                dataSource: {
                    data: forbiddenPropertyNames.concat(forbiddenFieldNames).sort()
                },
                template: "<li>#:data#</li>"
            });

            // wire up the done button, which is active for hidden fields
            $(selectors.finishHiddenFieldButton).click(function () {
                $(selectors.reservedNamesErrorBox).removeClass("sfError").hide();
                $(".sfError").remove();
                if (methods["validateStep1"].apply() && methods["validateFieldName"].apply(this, [settings.fields])) {
                    var fieldNameValue = $(selectors.fieldNameText).val();
                    for (var i = 0; i < forbiddenPropertyNames.length; i++) {
                        if (fieldNameValue == forbiddenPropertyNames[i]) {
                            $(selectors.reservedName).html(fieldNameValue);
                            $(selectors.reservedNamesErrorBox).addClass("sfError").show();
                            return;
                        }
                    }
                    for (var j = 0; j < forbiddenFieldNames.length; j++) {
                        if (fieldNameValue.toLowerCase() == forbiddenFieldNames[j].toLowerCase()) {
                            $(selectors.reservedName).html(fieldNameValue);
                            $(selectors.reservedNamesErrorBox).addClass("sfError").show();
                            return;
                        }
                    }

                    settings.onDone(_typeFieldEditor, { "Field": methods["getField"].apply() });
                    // reset the form to it's initial state
                    methods["reset"].apply();
                }
            });

            // wire up the IsHidden checkbox to show/hide the next step
            $(selectors.isHiddenField).click(function () {
                var checkBox = $(this);
                if (checkBox.is(':checked')) {
                    methods["hiddenFieldInterface"].apply(this, [false]);
                }
                else {
                    methods["hiddenFieldInterface"].apply(this, [true]);
                }
            });

            // wire up done button
            $(selectors.doneStep2Button).click(methods.doneStep2ButtonClickHandler);

            // wire up continue step 1 button
            $(selectors.continueStep1Button).click(function () {
                $(selectors.reservedNamesErrorBox).removeClass("sfError").hide();
                $(".sfError").remove();
                if (methods["validateStep1"].apply() && methods["validateFieldName"].apply(this, [settings.fields])) {
                    var fieldNameValue = $(selectors.fieldNameText).val();
                    for (var i = 0; i < forbiddenPropertyNames.length; i++) {
                        if (fieldNameValue == forbiddenPropertyNames[i]) {
                            $(selectors.reservedName).html(fieldNameValue);
                            $(selectors.reservedNamesErrorBox).addClass("sfError").show();
                            return;
                        }
                    }
                    for (var j = 0; j < forbiddenFieldNames.length; j++) {
                        if (fieldNameValue.toLowerCase() == forbiddenFieldNames[j].toLowerCase()) {
                            switch (fieldNameValue.toLowerCase()) {
                                case "metatitle":
                                case "metadescription":
                                    if ($(selectors.fieldType).val() === "Seo") {
                                        break;
                                    }
                                case "opengraphtitle":
                                case "opengraphdescription":
                                case "opengraphimage":
                                case "opengraphvideo":
                                    if ($(selectors.fieldType).val() === "OpenGraph") {
                                        break;
                                    }
                                default:
                                    $(selectors.reservedName).html(fieldNameValue);
                                    $(selectors.reservedNamesErrorBox).addClass("sfError").show();
                                    return;
                            }
                        }
                    }

                    var isNotOpenGraphMedia = true;
                    if ($(selectors.fieldName).val() === "OpenGraphImage" || $(selectors.fieldName).val() === "OpenGraphVideo") {
                        $(selectors.fieldType).val("RelatedMedia");
                        isNotOpenGraphMedia = false;

                        if ($(selectors.fieldName).val() === "OpenGraphImage") {
                            $(selectors.fieldTypeTitle).text("OpenGraph image");
                        }
                        else {
                            $(selectors.fieldTypeTitle).text("OpenGraph video");
                        }

                        $(selectors.defaultTitle).hide();
                        $(selectors.fieldTypeTitle).show();
                    }

                    var selectedType = $(selectors.fieldType).val();
                    var relatedTypes = ["RelatedMedia", "RelatedData"];
                    if (relatedTypes.indexOf(selectedType) !== -1) {
                        var model = {
                            mainFormSelector: selectors.step1,
                            container: selectors.editorSelectedFieldContainer,
                            fieldType: selectedType,
                            isNotOpenGraphMedia: isNotOpenGraphMedia
                        };

                        // open field editor on CREATE of dynamic module field
                        fieldEditor.open(model, false);

                        return;
                    }

                    methods["goToStep2"].apply(this, [fieldNameValue]);
                }
                else {
                    if ($(selectors.fieldName).siblings(".sfError").length > 1) {
                        $(selectors.fieldName).siblings(".sfError:first").hide();
                    }

                }
            });

            // wire up the Kendo DatePicker to the default Date Value
            $(selectors.defaultDateValue).kendoDatePicker({
                min: new Date(1950, 0, 1),
                max: new Date(2049, 11, 31),
                animation: false
            });

            // wire up the date picker range validation
            methods["setDateRange"].apply();

            // wire up back step 2 button
            $(selectors.backStep2Button).click(function () {

                methods["goToStep1"].apply();
                if ($(selectors.fieldType).val() == "MultipleChoice" ||
                    $(selectors.fieldType).val() == "Choices" ||
                    $(selectors.fieldType).val() == "Classification") {
                    $(selectors.isHiddenBox).hide();
                }
                $(selectors.fieldTypeTitle).hide();
                $(selectors.defaultTitle).show();
            });

            // wire up the single/multiple choice checkbox
            $(selectors.isMultiple).click(function () {
                var checkBox = $(this);
                if (checkBox.is(':checked')) {
                    $(selectors.widgetType.singleChoice).hide();
                    $(selectors.widgetType.multipleChoice).show();
                }
                else {
                    $(selectors.widgetType.multipleChoice).hide();
                    $(selectors.widgetType.singleChoice).show();
                }
            });

            // wireup taxonomies selector
            $.ajax({
                type: 'GET',
                url: settings.taxonomyWebServiceUrl,
                contentType: "application/json",
                success: function (result, args) {
                    // clear the taxonomies classification dropdown
                    $(selectors.classificationDropdown).find("option").remove();
                    // bind all taxonomies to classification dropdown
                    var classificationId;
                    for (var taxonomyIterator = 0; taxonomyIterator < result.Items.length; taxonomyIterator++) {
                        var classificationName = result.Items[taxonomyIterator].Name;
                        var escapedName = methods["escapeClassificationName"].apply(this, [classificationName]);
                        $(selectors.classificationDropdown).append($("<option></option>")
                            .attr("value", result.Items[taxonomyIterator].Id)
                            .attr("data-Type", result.Items[taxonomyIterator].Type)
                            .text(escapedName));
                        if (result.Items[taxonomyIterator].Name == "Tags") {
                            classificationId = result.Items[taxonomyIterator].Id;
                        }
                    }
                    if (classificationId != null) {
                        $(selectors.classificationDropdown).val(classificationId);
                        fieldClassificationId = classificationId;
                    }
                    else {
                        fieldClassificationId = emptyGuid;
                    }
                }
            });

            var inputAddressMode = $("input[name='addressFieldMode']");
            if (inputAddressMode) {
                $(inputAddressMode).click(function (sender) {
                    var isKeyValid = $(selectors.addressField.isGoogleMapApiKeyValid).val();
                    var addressFieldMode = methods["getAddressFieldMode"].apply();

                    if (addressFieldMode == "FormOnly" || isKeyValid == "true") {
                        // hide example div
                        $(selectors.addressField.googleMapsApiDescription).hide();
                        methods["enableDoneStep2Button"].apply();
                    } else {
                        // show example div
                        $(selectors.addressField.googleMapsApiDescription).show();
                        $(selectors.doneStep2Button).addClass("sfDisabledLinkBtn");
                        // remove all delegated click handlers 
                        $(selectors.doneStep2Button).off("click");
                    }
                });
            }

            fieldEditor.initialize(settings.siteBaseUrl);
        },

        enableDoneStep2Button: function () {
            $(selectors.doneStep2Button).removeClass("sfDisabledLinkBtn");
            // add the click handler back
            $(selectors.doneStep2Button).off("click");
            $(selectors.doneStep2Button).click(methods.doneStep2ButtonClickHandler);
        },

        validateLength: function (value) {
            var dbtype = $('#dbtype').val();
            var recommendedLength = 128;
            var warningMessage = "Most probably your database supports column names up to 128 chars. Value with greater length will corrupt your database.";
            if (dbtype === "Oracle") {
                recommendedLength = 30;
                warningMessage = "Oracle versions prior 12.2 support column names up to 30 chars. Value with greater length will corrupt your database.";
            }

            if (value.length > recommendedLength) {
                if ($("#dbMessage").length === 0) {
                    var errorMessage = document.createElement("div");
                    errorMessage.setAttribute("id", "dbMessage");
                    $(errorMessage).addClass("sfError");
                    $(errorMessage).html(warningMessage);
                    $(selectors.fieldName).after(errorMessage);
                }
            } else {
                $("#dbMessage").remove();
            }
        },

        showAppropriateUI: function (type) {
            $(".sfHide").show();
            methods["removeCustomWidgetTypeNameErrorMessage"].apply();
            switch (type) {
                case "ShortText":
                    methods["prepareShortTextUI"].apply();
                    break;
                case "LongText":
                    methods["prepareLongTextUI"].apply();
                    break;
                case "Choices":
                case "MultipleChoice":
                    methods["prepareMultipleChoiceUI"].apply();
                    break;
                case "YesNo":
                    methods["prepareYesNoUI"].apply();
                    break;
                case "Currency":
                    methods["prepareCurrencyUI"].apply();
                    break;
                case "DateTime":
                    methods["prepareDateTimeUI"].apply();
                    break;
                case "Number":
                    methods["prepareNumberUI"].apply();
                    break;
                case "Classification":
                    methods["prepareClassificationUI"].apply();
                    break;
                case "Seo":
                    methods["prepareMetaFiledUI"].apply();
                    break;
                case "OpenGraph":
                    methods["prepareOpenGraphFiledUI"].apply();
                    break;
                case "Unknown":
                    methods["prepareUnknownUI"].apply();
                    break;
                case "Media":
                    methods["prepareMediaUI"].apply();
                    break;
                case "Guid":
                    methods["prepareGuidUI"].apply();
                    break;
                case "GuidArray":
                    methods["prepareGuidArrayUI"].apply();
                    break;
                case "Address":
                    methods["prepareAddressUI"].apply();
                    break;
                case "RelatedMedia":
                    methods["prepareRelatedMediaUI"].apply();
                    break;
                case "RelatedData":
                    methods["prepareRelatedDataUI"].apply();
                    break;
            }
        },

        escapeClassificationName: function (name) {
            var escapedName = "";
            for (var i = 0; i < name.length; i++) {
                if (name[i] != "-") {
                    escapedName += name[i];
                }
            }
            return escapedName;
        },

        setDateRange: function () {
            function startChange() {
                var startDate = start.value();

                if (!startDate) {
                    startDate.setDate(startDate.getDate() + 1);
                }

                end.min(startDate);
            }

            function endChange() {
                var endDate = end.value();

                if (!endDate) {
                    endDate.setDate(endDate.getDate() - 1);
                }

                start.max(endDate);
            }

            var start = $(selectors.minDateValue).kendoDatePicker({
                change: startChange,
                animation: false
            }).data("kendoDatePicker");

            var end = $(selectors.maxDateValue).kendoDatePicker({
                change: endChange,
                animation: false
            }).data("kendoDatePicker");

            start.max(end.value());
            end.min(start.value());
        },

        removeCustomWidgetTypeNameErrorMessage: function () {
            // remove the error message from custom widget type name if any
            if ($(selectors.customWidgetTypeName).siblings(".sfError").length == 1) {
                $(selectors.customWidgetTypeName).siblings(".sfError:first").hide();
            }
        },

        hiddenFieldInterface: function (isHidden) {
            if (isHidden) {
                $(".sfHide").show();
                methods["removeCustomWidgetTypeNameErrorMessage"].apply();
                switch ($(selectors.fieldType).val()) {
                    case "ShortText":
                        methods["prepareShortTextUI"].apply();
                        break;
                    case "LongText":
                        methods["prepareLongTextUI"].apply();
                        break;
                    case "YesNo":
                        methods["prepareYesNoUI"].apply();
                        break;
                    case "DateTime":
                        methods["prepareDateTimeUI"].apply();
                        break;
                    case "Guid":
                        $(selectors.widgetType.guid).show();
                        $(selectors.widgetType.guid + " option:first").attr("selected", "selected");
                        methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.guid]);
                        $(selectors.fieldTypeTitle).text("Textbox");
                        break;
                    case "Currency":
                        methods["prepareCurrencyUI"].apply();
                        break;
                    case "Number":
                        methods["prepareNumberUI"].apply();
                        break;
                    case "Classification":
                        methods["prepareClassificationUI"].apply();
                        break;
                    case "Seo":
                        methods["prepareMetaFiledUI"].apply();
                        break;
                    case "OpenGraph":
                        methods["prepareOpenGraphFiledUI"].apply();
                        break;
                    case "GuidArray":
                        $(selectors.customWidgetTypeBox).show();
                        break;
                    case "Address":
                        methods["prepareAddressUI"].apply();
                        break;
                    default:
                        break;
                }
                $(selectors.finishHiddenFieldButton).hide();
                $(selectors.continueStep1Button).show();
            }
            else {
                $(".sfHide").hide();
                $(selectors.finishHiddenFieldButton).show();
                $(selectors.continueStep1Button).hide();
                $(selectors.decimalPlacesBox).hide();
                $(selectors.decimalPlacesCurrencyBox).hide();
                $(selectors.classificationBox).hide();
                $(selectors.metaFieldBox).hide();
                $(selectors.customWidgetTypeBox).hide();
            }
        },

        doneStep2ButtonClickHandler: function () {
            var isValidStep2 = true;
            $(".sfError").remove();
            if (methods["validateStep2"].apply()) {

                // if the containers of the min and max length of characters 
                // are not visible we dont need to validate them
                if ($("#typeFieldEditor_minLength").parent().css("display") != "none") {
                    if (validation.validateMinMaxInputs(selectors.minLength, selectors.maxLength) == false) {
                        isValidStep2 = false;
                    }
                }
                var fieldType = $("#typeFieldEditor_fieldType").val();

                // if we are creating a number field, we should validate the min and max range numbers
                if (fieldType == "Number") {
                    if (validation.validateMinMaxRanges(selectors.minRangeNumber, selectors.maxRangeNumber) == false) {
                        isValidStep2 = false;
                    }
                }

                // validate choice field 
                if (fieldType == "MultipleChoice" || fieldType == "Choices") {
                    if (methods["validateChoices"].apply(this, [fieldType]) == false) {
                        isValidStep2 = false;
                    }
                }
            }
            else {
                isValidStep2 = false;
            }
            if (isValidStep2) {
                if (isEditMode) {
                    _settings.onEditDone(_typeFieldEditor, { "Field": methods["getField"].apply(this, [oldWidgetName]) });
                }
                else {
                    _settings.onDone(_typeFieldEditor, { "Field": methods["getField"].apply() });
                }
                // reset the form to it's initial state
                methods["reset"].apply();
            }
        },

        // navigates the typeFieldEditor to the first step
        goToStep1: function () {
            $(selectors.step1).show();
            $(selectors.step2).hide();
        },

        // navigates the typeFieldEditor to the second step
        goToStep2: function (fieldName) {
            $(selectors.step1).hide();
            $(selectors.step2).show();

            // enable and rebind the done button on step 2, if it was disabled by address field
            if ($(selectors.fieldType).val() !== "Address") {
                methods["enableDoneStep2Button"].apply();
            }

            if (isEditMode) {
                // hide back button if in edit mode
                $(selectors.backStep2Button).hide();
            } else {
                $(selectors.backStep2Button).show();
            }

            $(selectors.disableLinkParser).parent().hide();

            // prepare UI for second step
            // hide all dependant elements
            $(selectors.step2).find(".sfDependant").hide();
            // show all dependant elements for the current field type
            var targetSelector = "";
            switch ($(selectors.fieldType).val()) {
                case "ShortText":
                    var fieldName = $(selectors.fieldName).val();
                    if (fieldName === "MetaTitle") {
                        targetSelector = ".sfSeo";
                        $(selectors.fieldTypeTitle).text("Meta title");
                    }
                    else if (fieldName === "OpenGraphTitle") {
                        targetSelector = ".sfOpenGraph";
                        $(selectors.fieldTypeTitle).text("OpenGraph title");
                    }
                    else {
                        targetSelector = ".sfShortText";
                    }
                    break;
                case "LongText":
                    {
                        var fieldName = $(selectors.fieldName).val();
                        if (fieldName === "MetaDescription") {
                            targetSelector = ".sfSeo";
                            $(selectors.fieldTypeTitle).text("Meta description");
                        }
                        else if (fieldName === "OpenGraphDescription") {
                            targetSelector = ".sfOpenGraph";
                            $(selectors.fieldTypeTitle).text("OpenGraph description");
                        }
                        else {
                            targetSelector = ".sfLongText";
                            if ($(selectors.widgetType.longText).val() == "Telerik.Sitefinity.Web.UI.Fields.TextField") {
                                $(selectors.fieldTypeTitle).text("Text area");
                                //show limitations tab
                                $(selectors.limitationsTab).removeClass("sfDisplayNoneImportant");
                            }
                            if ($(selectors.widgetType.longText).val() == "Telerik.Sitefinity.Web.UI.Fields.HtmlField") {
                                $(selectors.fieldTypeTitle).text("Rich text editor");
                                //hide limitations tab
                                $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                            }

                            $(selectors.disableLinkParser).parent().show();
                        }

                        break;
                    }
                case "Choices":
                    targetSelector = ".sfChoices";
                    if (!isEditMode) {
                        methods["choiceEditorDesign"].apply(this, [false]);
                    }
                    //hide limitations tab
                    $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                    break;
                case "MultipleChoice":
                    targetSelector = ".sfMultipleChoice";
                    if (!isEditMode) {
                        methods["choiceEditorDesign"].apply(this, [true]);
                    }
                    //hide limitations tab
                    $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                    break;
                case "YesNo":
                    targetSelector = ".sfYesNo";
                    //hide limitations tab
                    $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                    break;
                case "Currency":
                    targetSelector = ".sfCurrency";
                    // set the pattern for the Range input fields
                    var decimalPlacesCurrency = $("#typeFieldEditor_decimalPlacesCurrency").val();
                    methods["setMinMaxCurrencyRangeRegEx"].apply(this, [decimalPlacesCurrency]);
                    break;
                case "DateTime":
                    targetSelector = ".sfDateTime";
                    break;
                case "Number":
                    targetSelector = ".sfNumber";
                    // set the pattern for the Range input fields
                    var decimalPlacesNumber = $("#typeFieldEditor_decimalPlaces").val();
                    methods["setMinMaxNumberRangeRegEx"].apply(this, [decimalPlacesNumber]);
                    break;
                case "Classification":
                    targetSelector = ".sfClassification";
                    break;
                case "Unknown":
                    targetSelector = ".sfUnknown";
                    break;
                case "Media":
                    var mediaFieldTypeId = $("input[name='mediaFieldType']:checked").attr("id");
                    switch (mediaFieldTypeId) {
                        case "typeFieldEditor_imageMediaFieldType":
                            targetSelector = ".sfImageMedia";
                            $(selectors.fieldTypeTitle).text("Image selector");
                            break;
                        case "typeFieldEditor_videoMediaFieldType":
                            targetSelector = ".sfVideoMedia";
                            $(selectors.fieldTypeTitle).text("Video selector");
                            break;
                        case "typeFieldEditor_fileMediaFieldType":
                            targetSelector = ".sfFileMedia";
                            $(selectors.fieldTypeTitle).text("File selector");
                            break;
                    }
                    break;
                case "Guid":
                    targetSelector = ".sfGuid";
                    //hide limitations tab
                    $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                    break;
                case "GuidArray":
                    targetSelector = ".sfGuidArray";
                    //hide limitations tab
                    $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                    break;
                case "Address":
                    targetSelector = ".sfAddress";
                    //hide limitations tab
                    $(selectors.limitationsTab).addClass("sfDisplayNoneImportant");
                    if (!isEditMode) {
                        // set default address field mode
                        var defaultMode = $("input[name='addressFieldMode'][id='typeFieldEditor_addressFieldFormOnly']");
                        defaultMode.prop('checked', true);
                        defaultMode.click();
                    }
                    break;
                case "Seo":
                    targetSelector = ".sfSeo";

                    var fieldName = $(selectors.fieldName).val();
                    if (fieldName === "MetaTitle") {
                        $(selectors.fieldTypeTitle).text("Meta title");
                        $(selectors.fieldLabelText).val("Title for search engines");
                        $(selectors.instructionalText).val("Displayed in browser title bar and in search results. Less than 70 characters are recommended.");
                        $(selectors.recommendedCharactersCount).val(70);
                    }
                    if (fieldName === "MetaDescription") {
                        $(selectors.fieldTypeTitle).text("Meta description");
                        $(selectors.fieldLabelText).val("Description");
                        $(selectors.instructionalText).val("Less than 150 characters are recommended.");
                        $(selectors.recommendedCharactersCount).val(150);
                        $(selectors.widgetType.longText).val("Telerik.Sitefinity.Web.UI.Fields.TextField");
                    }
                    break;
                case "OpenGraph":
                    var openGraphFieldName = $(selectors.fieldName).val();
                    switch (openGraphFieldName) {
                        case "OpenGraphTitle":
                            targetSelector = ".sfOpenGraph";
                            $(selectors.fieldTypeTitle).text("OpenGraph title");
                            $(selectors.fieldLabelText).val("Title for social media");
                            $(selectors.instructionalText).val("Less than 90 characters are recommended.");
                            $(selectors.recommendedCharactersCount).val(90);
                            break;
                        case "OpenGraphDescription":
                            targetSelector = ".sfOpenGraph";
                            $(selectors.fieldTypeTitle).text("OpenGraph description");
                            $(selectors.fieldLabelText).val("Description");
                            $(selectors.instructionalText).val("Less than 200 characters are recommended.");
                            $(selectors.recommendedCharactersCount).val(200);
                            $(selectors.widgetType.longText).val("Telerik.Sitefinity.Web.UI.Fields.TextField");
                            break;
                    }
                    break;
            }
            if (!isEditMode) {
                // sets the label text to the field name from step 1
                var labelName = methods["transferFieldName"].apply(this, [fieldName]);
                if ($(selectors.fieldLabelText).val() === "") {
                    $(selectors.fieldLabelText).val(labelName);
                }
            }
            $(selectors.defaultTitle).hide();
            $(selectors.fieldTypeTitle).show();
            $(targetSelector).show();
            
            var tabstrip = $("#typeFieldEditor_Tabstrip").kendoTabStrip({ animation: false }).data("kendoTabStrip");
            tabstrip.select(tabstrip.tabGroup.children("li:first"));

        },

        // validates step 1 of the typeFieldEditor
        validateStep1: function () {
            return validation.validateContainer(selectors.step1);
        },

        validateFieldName: function (fields) {
            var fieldName = $(selectors.fieldName).val();
            var error;

            for (var i = 0; i < fields.length; i++) {
                if ((fieldName === fields[i].Name) ||
                    (fieldName && fields[i].Name && fieldName.toLowerCase() === fields[i].Name.toLowerCase())) {
                    error = "This field name is already used!";
                    break;
                }

                var classificationId = $(selectors.classificationDropdown).val();
                if (classificationId != null && classificationId === fields[i].ClassificationId &&
                    fields[i].TypeName === "Classification" && $(selectors.fieldType).val() === "Classification") {
                    error = "This classification is already used!";
                    break;
                }
            }

            if (error) {
                var errorMessage = document.createElement("div");
                $(errorMessage).addClass("sfError");
                $(errorMessage).html(error);
                $(selectors.fieldName).after(errorMessage);
                return false;
            }

            return true;
        },

        // validates step 2 of the typeFieldEditor
        validateStep2: function () {
            var isValid = validation.validateContainer(selectors.step2);

            if (!isValid) {
                $(selectors.choicesEditor.choiceList).find(selectors.choicesEditor.choiceItem + ' input.sfVal').each(function () {
                    if ($(this).siblings(".sfError").length > 1) {
                        $(this).siblings(".sfError:first").hide();
                    }
                });
            }

            return isValid;
        },

        // gets the definition of the field configured through this editor
        getField: function (oldWidgetName) {
            var widgetTypeName = "";
            var dependantClass = "";
            var decimalPlacesCount = 0;
            var minRange;
            var maxRange;
            var mediaType = "";
            var renderChoice = "";
            var addressFieldMode = "";
            var canSelectMultipleItems = false;
            switch ($(selectors.fieldType).val()) {
                case "ShortText":
                    widgetTypeName = $(selectors.widgetType.shortText).val();
                    dependantClass = "sfShortText";
                    break;
                case "LongText":
                    widgetTypeName = $(selectors.widgetType.longText).val();
                    dependantClass = "sfLongText";
                    break;
                case "Choices":
                case "MultipleChoice":
                    if ($(selectors.isMultiple).is(":checked")) {
                        renderChoice = $(selectors.multipleChoiceSelectedItem).attr("data-choice-type");
                        widgetTypeName = $(selectors.widgetType.multipleChoice).val();
                        canSelectMultipleItems = true;
                    }
                    else {
                        renderChoice = $(selectors.singleChoiceSelectedItem).attr("data-choice-type");
                        widgetTypeName = $(selectors.widgetType.singleChoice).val();
                        canSelectMultipleItems = false;
                    }

                    if (oldWidgetName) {
                        widgetTypeName = oldWidgetName;
                    }

                    dependantClass = "sfMultipleChoice";
                    break;
                case "YesNo":
                    widgetTypeName = $(selectors.widgetType.yesNo).val();
                    dependantClass = "sfYesNo";
                    break;
                case "Currency":
                    widgetTypeName = $(selectors.widgetType.currency).val();
                    dependantClass = "sfCurrency";
                    break;
                case "DateTime":
                    widgetTypeName = $(selectors.widgetType.dateTime).val();
                    dependantClass = "sfDateTime";
                    var minRangeDatePicker = $(selectors.minDateValue).data("kendoDatePicker");
                    var minRangeDate = minRangeDatePicker.value();
                    if (minRangeDate != null) {
                        minRange = minRangeDate.getDate() + "/" + (minRangeDate.getMonth() + 1) + "/" + minRangeDate.getFullYear();
                    }
                    var maxRangeDatePicker = $(selectors.maxDateValue).data("kendoDatePicker");
                    var maxRangeDate = maxRangeDatePicker.value();
                    if (maxRangeDate != null) {
                        maxRange = maxRangeDate.getDate() + "/" + (maxRangeDate.getMonth() + 1) + "/" + maxRangeDate.getFullYear();
                    }
                    break;
                case "Number":
                    widgetTypeName = $(selectors.widgetType.number).val();
                    dependantClass = "sfNumber";
                    decimalPlacesCount = parseInt($(selectors.decimalPlacesCount).val());
                    minRange = ($(selectors.minRangeNumber).val().length == 0) ? null : $(selectors.minRangeNumber).val();
                    maxRange = ($(selectors.maxRangeNumber).val().length == 0) ? null : $(selectors.maxRangeNumber).val();
                    break;
                case "Classification":
                    // The base value for all classification items is the TaxonField.
                    // In the actual backend definition, we decide wheter to user FlatTaxon or HierarchicalTaxon
                    widgetTypeName = $(selectors.widgetType.hierarchicalClassification).val();
                    canSelectMultipleItems = $(selectors.canSelectMultipleClassificationItems).is(":checked");
                    dependantClass = "sfClassification";
                    break;
                case "Unknown":
                    widgetTypeName = $(selectors.widgetType.unknown).val();
                    dependantClass = "sfUnknown";
                    break;
                case "Media":
                    // the widget type is the same for the three types of media -> AssetsField
                    widgetTypeName = $(selectors.widgetType.imageMedia).val();
                    var mediaFieldTypeId = $("input[name='mediaFieldType']:checked").attr("id");
                    switch (mediaFieldTypeId) {
                        case "typeFieldEditor_imageMediaFieldType":
                            mediaType = "image";
                            dependantClass = "sfImageMedia";
                            break;
                        case "typeFieldEditor_videoMediaFieldType":
                            mediaType = "video";
                            dependantClass = "sfVideoMedia";
                            break;
                        case "typeFieldEditor_fileMediaFieldType":
                            mediaType = "file";
                            dependantClass = "sfFileMedia";
                            break;
                    }
                    dependantClass = "sfImageMedia";
                    break;
                case "Guid":
                    widgetTypeName = $(selectors.widgetType.guid).val();
                    dependantClass = "sfGuid";
                    break;
                case "GuidArray":
                    widgetTypeName = $(selectors.widgetType.guidArray).val();
                    dependantClass = "sfGuidArray";
                    break;
                case "Address":
                    widgetTypeName = $(selectors.widgetType.address).val();
                    addressFieldMode = methods["getAddressFieldMode"].apply();
                    dependantClass = "sfAddress";
                    break;
                case "Seo":
                    var fieldName = $(selectors.fieldName).val();
                    switch (fieldName) {
                        case "MetaTitle":
                            widgetTypeName = $(selectors.widgetType.shortText).val();
                            dependantClass = "sfSeo";
                            break;
                        case "MetaDescription":
                            widgetTypeName = $(selectors.widgetType.longText).val();
                            dependantClass = "sfSeo";
                            break;
                    }
                    break;
                case "OpenGraph":
                    var fieldName = $(selectors.fieldName).val();
                    switch (fieldName) {
                        case "OpenGraphTitle":
                            widgetTypeName = $(selectors.widgetType.shortText).val();
                            dependantClass = "sfOpenGraph";
                            break;
                        case "OpenGraphDescription":
                            widgetTypeName = $(selectors.widgetType.longText).val();
                            dependantClass = "sfOpenGraph";
                            break;
                    }
                    break;
            }

            // if user selected custom widget type, take the value from the
            // custom widget type name input
            if (widgetTypeName == "Custom") {
                widgetTypeName = $(selectors.customWidgetTypeName).val();
            }

            // field title can have different elements that define it. Use the
            // one that has the class for the specified field type
            var fieldTitle = "";
            $(selectors.step2).find(selectors.fieldTitle).each(function () {
                if ($(this).hasClass(dependantClass)) {
                    fieldTitle = $(this).val();
                }
            });

            if ($(selectors.isHiddenField).is(":checked")) {
                var hiddenField = {
                    "Name": $(selectors.fieldName).val(),
                    "TypeName": $(selectors.fieldType).val(),
                    "TypeUIName": $(selectors.fieldType + " option:selected").text(),
                    "IsHiddenField": $(selectors.isHiddenField).is(":checked"),
                    "WidgetTypeName": widgetTypeName,
                    "DBType": $(selectors.dbTypeDropdown).val(),
                    "DBLength": $(selectors.dbLength).val(),
                    "AllowNulls": $(selectors.allowNulls).is(":checked"),
                    "IncludeInIndexes": $(selectors.includeInIndexes).is(":checked"),
                    "ColumnName": $(selectors.columnName).val()
                };
                return hiddenField;
            }

            // get choices
            var choices = "";

            if ($(selectors.fieldType).val() == 'MultipleChoice') {
                $(selectors.choicesEditor.choiceList).find(selectors.choicesEditor.choiceItem).each(function () {
                    $(this).find("input.sfTxt").each(function () {
                        if ($(this).attr("type") == "text") {
                            var encodedText = methods["encodeHTML"].apply(this, [$(this).val()]);
                            choices += encodedText + ",";
                        }
                    });
                });
                // remove the last comma
                choices = choices.substr(0, choices.length - 1);
            }
            else if ($(selectors.fieldType).val() == 'Choices') {
                var xml = $('<choices></choices>');
                $(selectors.choicesEditor.choiceList).find(selectors.choicesEditor.choiceItem).each(function () {
                    var value = $(this).find("input.sfVal").val();
                    var text = $(this).find("input.sfTxt").val();
                    var encodedText = methods["encodeHTML"].apply(this, [text]);

                    xml.append($('<choice />').attr('value', value).attr('text', encodedText));
                });
                choices = xml[0].outerHTML;
            }

            var fieldName = $(selectors.fieldName).val();
            var fieldType = $(selectors.fieldType).val();

            // Adjust filed type for Seo and OpenGraph title and description fields to be ShortText
            if ((fieldType === "Seo" && fieldName === "MetaTitle") || (fieldType === "OpenGraph" && fieldName === "OpenGraphTitle")) {
                fieldType = "ShortText";
            }
            else if ((fieldType === "Seo" && fieldName === "MetaDescription") || (fieldType === "OpenGraph" && fieldName === "OpenGraphDescription")) {
                fieldType = "LongText";
            }

            var typeUIName = $(selectors.fieldType + " option:selected").text();
            // Keep TypeUIName for Seo and OpenGraph not to be overriden from generic short text
            if (fieldType === "ShortText" || fieldType === "LongText") {
                if (fieldName === "OpenGraphTitle" || fieldName === "OpenGraphDescription") {
                    typeUIName = "Social media (OpenGraph)";
                }
                else if (fieldName === "MetaTitle" || fieldName === "MetaDescription") {
                    typeUIName = "Search engine optimization";
                }
            }

            var recommendedCharactersCount = null;
            if ($(selectors.recommendedCharactersCount).val()) {
                if ($(selectors.recommendedCharactersCount).val().length !== 0) {
                    recommendedCharactersCount = $(selectors.recommendedCharactersCount).val();
                }
            }

            var field = {
                "Name": $(selectors.fieldName).val(),
                "TypeName": fieldType,
                "TypeUIName": typeUIName,
                "IsHiddenField": $(selectors.isHiddenField).is(":checked"),
                "WidgetTypeName": widgetTypeName,
                // "MinNumberRange": ($(selectors.minRangeNumber).val().length == 0) ? null : $(selectors.minRangeNumber).val(),
                // "MaxNumberRange": ($(selectors.maxRangeNumber).val().length == 0) ? null : $(selectors.maxRangeNumber).val(),
                "MinNumberRange": minRange,
                "MaxNumberRange": maxRange,
                "DBType": $(selectors.dbTypeDropdown).val(),
                "MediaType": mediaType,
                "ClassificationId": fieldClassificationId,
                "DecimalPlacesCount": decimalPlacesCount,
                "NumberUnit": $.trim($(selectors.numberUnit).val()),
                "DBLength": $(selectors.dbLength).val(),
                "AllowNulls": $(selectors.allowNulls).is(":checked"),
                "DisableLinkParser": $(selectors.disableLinkParser).is(":checked"),
                "IncludeInIndexes": $(selectors.includeInIndexes).is(":checked"),
                "ColumnName": $(selectors.columnName).val(),
                "CanSelectMultipleItems": canSelectMultipleItems,
                "CanCreateItemsWhileSelecting": $(selectors.canCreateClassificationItemsWhenSelecting).is(":checked"),
                "Title": fieldTitle,
                "InstructionalText": $(selectors.instructionalText).val(),
                "DefaultValue": $(selectors.defaultValue).val(),
                "IsRequired": $(selectors.isRequired).is(":checked"),
                "IsLocalizable": $(selectors.isLocalizable).is(":checked"),
                "MinLength": ($(selectors.minLength).val().length == 0) ? 0 : $(selectors.minLength).val(),
                "MaxLength": ($(selectors.maxLength).val().length == 0) ? 0 : $(selectors.maxLength).val(),
                "LengthValidationMessage": $(selectors.regExValidationValue).val(),
                "Choices": choices,
                "CheckedByDefault": $(selectors.checkedByDefault).is(":checked"),
                "InstructionalChoice": $.trim($(selectors.instructionalChoice).val()),
                "ChoiceRenderType": renderChoice,
                "IsRequiredToSelectDdlValue": $(selectors.dropDownListMakeRequiredCheckBox).is(":checked"),
                "IsRequiredToSelectCheckbox": $(selectors.oneRequiredCheckBox).is(":checked"),
                "RegularExpression": (($.trim($(selectors.regExValidationValue).val())).length == 0) ? null : ($.trim($(selectors.regExValidationValue).val())),
                "AllowMultipleImages": $(selectors.mediaType.allowMultipleImages).is(":checked"),
                "AllowMultipleVideos": $(selectors.mediaType.allowMultipleVideos).is(":checked"),
                "AllowMultipleFiles": $(selectors.mediaType.allowMultipleFiles).is(":checked"),
                "AllowImageLibrary": $(selectors.mediaType.allowImageLibrary).is(":checked"),
                "ImageMaxSize": (($.trim($(selectors.mediaType.imageMaxSize).val())).length == 0) ? 0 : ($.trim($(selectors.mediaType.imageMaxSize).val())),
                "ImageExtensions": $.trim($(selectors.mediaType.imageExtensions).val()),
                "FileMaxSize": (($.trim($(selectors.mediaType.fileMaxSize).val())).length == 0) ? 0 : ($.trim($(selectors.mediaType.fileMaxSize).val())),
                "FileExtensions": $.trim($(selectors.mediaType.fileExtensions).val()),
                "VideoMaxSize": (($.trim($(selectors.mediaType.videoMaxSize).val())).length == 0) ? 0 : ($.trim($(selectors.mediaType.videoMaxSize).val())),
                "VideoExtensions": $.trim($(selectors.mediaType.videoExtensions).val()),
                "AddressFieldMode": addressFieldMode,
                "RecommendedCharactersCount": recommendedCharactersCount,
            };

            if ($(selectors.parentSectionId).val() != "") {
                field.ParentSectionId = $(selectors.parentSectionId).val();
            }
            if ($(selectors.showInGrid).val() != "") {
                field.ShowInGrid = $(selectors.showInGrid).val();
            }
            if ($(selectors.gridColumnOrdinal).val() != "") {
                field.GridColumnOrdinal = $(selectors.gridColumnOrdinal).val();
            }

            return field;
        },

        encodeHTML: function (value) {
            //create a in-memory div, set it's inner text(which jQuery automatically encodes)
            //then grab the encoded contents back out.  The div never exists on the page.
            var $div = $('<div/>');
            var encodedValue = $div.text(value).html();
            $div = null;
            return encodedValue;
        },

        decodeHTML: function (value) {
            var $div = $('<div/>');
            var decodedValue = $div.html(value).text();
            $div = null;
            return decodedValue;
        },

        // resets the editor and puts it in the initial state
        reset: function () {
            /* RESET THE STEP 1 */
            // go to the first step of the editor
            methods["goToStep1"].apply();
            // hide all type specific UI
            methods["hideTypeSpecificUI"].apply();
            // prepare the UI for short text, which is default
            methods["prepareShortTextUI"].apply();
            // reset custom widget type name
            $(selectors.customWidgetTypeName).val("");
            // reset the db precision ui for number type
            $(selectors.dbPrecision).val("");
            // reset hidden field
            $(".sfHide").show();
            $(selectors.finishHiddenFieldButton).hide();
            $(selectors.continueStep1Button).show();
            // remove the reserved names invalid message
            // This MUST be before the removing of all tags with class sfError!
            $(selectors.reservedNamesErrorBox).removeClass("sfError").hide();
            // clear the validation messages 
            $(".sfError").remove();
            // clear the field name
            $(selectors.fieldName).val("");
            // remove the disabled attribute of the field name text box
            $(selectors.fieldName).removeAttr("disabled");
            // set the field type to the first one
            $(selectors.fieldType).val($(selectors.fieldType + " option:first").val());
            // uncheck the is hidden field
            $(selectors.isHiddenField).prop("checked", false);
            // clear the DB length field
            $(selectors.dbLength).val("");
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // unchecheck allow nulls
            $(selectors.allowNulls).prop("checked", false);
            // unchecheck allow nulls
            $(selectors.disableLinkParser).prop("checked", false);
            // uncheck include in indexes
            $(selectors.includeInIndexes).prop("checked", false);
            // uncheck "checked by default" for yesno field
            $(selectors.checkedByDefault).prop("checked", false);
            // clear column name
            $(selectors.columnName).val("");
            // collapse the advanced selection
            $(selectors.advancedSection).hide().parent().removeClass("sfExpandedSection");
            //remove MultipleChoice option
            $(selectors.fieldType + ' option[value="MultipleChoice"]').remove();
            //remove Media option
            $(selectors.fieldType + ' option[value="Media"]').remove();
            /* RESET THE ADDITIONAL SETTINGS */
            // clear the field title (label)
            $(selectors.fieldTitle).val("");
            // set the classification dropdown type to the first value
            $(selectors.classificationDropdown).val($(selectors.classificationDropdown + " option:first").val());
            if ($(selectors.classificationDropdown).val() != null) {
                fieldClassificationId = $(selectors.classificationDropdown).val();
            }
            else {
                fieldClassificationId = emptyGuid;
            }

            // clear instructional text
            $(selectors.instructionalText).val("");
            // clear predefined text
            $(selectors.defaultValue).val("");
            // uncheck the "Make required"
            $(selectors.isRequired).prop("checked", false);
            // uncheck the "Display Currency"
            $(selectors.displayCurrency).prop("checked", false);
            // uncheck the "Can create classification items while selecting"
            $(selectors.canCreateClassificationItemsWhenSelecting).attr("checked", true);
            // clear min-length input value
            $(selectors.minLength).val("");
            // clear max-length input value
            $(selectors.maxLength).val("");
            // clearrecommended length input value
            $(selectors.recommendedCharactersCount).val("");
            // clear the regex text input
            $(selectors.regExValidationValue).val("");
            // clear the min range input
            $(selectors.minRangeNumber).val("");
            // clear the max range input
            $(selectors.maxRangeNumber).val("");
            // uncheck the checkbox for multiple choice editor, one required checkbox
            $(selectors.oneRequiredCheckBox).prop("checked", false);
            // uncheck the checkbox for ddl make required
            $(selectors.dropDownListMakeRequiredCheckBox).prop("checked", false);
            // uncheck the multiple selection option
            $(selectors.isMultiple).prop("checked", false);
            // select the first option from the drop down
            $(selectors.decimalPlacesCount).val("0");
            // remove the pattern attribute from the default value input box
            $(selectors.defaultValue).removeAttr("pattern");
            // show the limitations tabs
            $(selectors.limitationsTab).removeClass("sfDisplayNoneImportant");
            // clear the instructional choice text
            $(selectors.instructionalChoice).val("- Select -");
            // hide the instructional choice textbox
            $(selectors.isntructionalChoiceBox).hide();
            // clear the number unit
            $(selectors.numberUnit).val("");
            // reset the min range date calendar
            $(selectors.minDateValue).data("kendoDatePicker").value(null);
            // reset the min range text box value
            $(selectors.minDateValue).val("");
            // reset the max range date calendar
            $(selectors.maxDateValue).data("kendoDatePicker").value(null);
            // reset the max range text box value
            $(selectors.maxDateValue).val("");
            // reset the default date calendar
            $(selectors.defaultDateValue).data("kendoDatePicker").value(null);
            // reset the default date calendar text box value
            $(selectors.defaultDateValue).val("");
            // clear the image media inputs
            $(selectors.mediaType.imageExtensions).val("");
            $(selectors.mediaType.imageMaxSize).val("");
            // clear the video media inputs
            $(selectors.mediaType.videoExtensions).val("");
            $(selectors.mediaType.videoMaxSize).val("");
            // clear the file media inputs
            $(selectors.mediaType.fileExtensions).val("");
            $(selectors.mediaType.fileMaxSize).val("");
            $(selectors.mediaType.allowNulls).prop("checked", false);
            $(selectors.mediaType.disableLinkParser).prop("checked", false);
            $(selectors.mediaType.columnName).val("");

            // hide the field type title
            $(selectors.fieldTypeTitle).hide();
            // show the default title
            $(selectors.defaultTitle).show();
            // set the first media radio button to be checked
            $("#typeFieldEditor_imageMediaFieldType").attr("checked", "checked");
            // set edit mode to default value
            isEditMode = false;
            // hide tooltips
            $(selectors.step2 + ' .sfDetailsPopup').addClass('sfDisplayNone');
            // enable and rebind the done button on step 2, if it was disabled by address field
            methods["enableDoneStep2Button"].apply();
        },

        // hides all the user interface elements specific
        // to the particular type of field
        hideTypeSpecificUI: function () {
            // hide all the widget type selectors
            $(selectors.widgetType.allTypes).hide();
            $(selectors.widgetType.label).hide();
            // hide the custom widget box
            $(selectors.customWidgetTypeBox).hide();
            // remove the reserved names invalid message
            $(selectors.reservedNamesErrorBox).hide();
            // hide radio buttons for media file types
            $(selectors.mediaRadioButtonsBox).hide();
            // hide drop down for media file types
            $(selectors.mediaDropDown).hide();
            // hide drop down for data types
            $(selectors.moduleTypesDropDown).hide();
            // hide checkbox for selecting single/multiple choice
            $(selectors.canSelectMultipleBox).hide();
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // clear the instructional choice text
            $(selectors.instructionalChoice).val("- Select -");
            // hide the instructional choice textbox
            $(selectors.isntructionalChoiceBox).hide();
            // hide the decimal places box
            $(selectors.decimalPlacesBox).hide();
            // hide the decimal places box
            $(selectors.decimalPlacesCurrencyBox).hide();
            // hide the box with ui for selecting a classification
            $(selectors.classificationBox).hide();
            // hide the box with ui for selecting a Meta field
            $(selectors.metaFieldBox).hide();
            // hide the box with ui for selecting an OpenGraph field
            $(selectors.openGraphFieldBox).hide();
            // hide the db precision ui for number type
            $(selectors.dbPrecisionBox).hide();
            // hide the db scale ui for number type
            $(selectors.dbScaleBox).hide();
            // hide the db length box (used only for short text)
            $(selectors.dbLengthBox).hide();
            // remove all the db type options
            $(selectors.dbTypeDropdown).find("option").remove();
            // hide the checkbox for ddl make required
            $(selectors.dropDownListMakeRequiredCheckBoxContainer).hide();
            // hide the checkbox for multiple choice editor, one required checkbox
            $(selectors.oneRequiredCheckBoxContainer).hide();
            // enable the field name text box
            if ($(selectors.fieldName).attr("disabled") == "disabled") {
                $(selectors.fieldName).removeAttr("disabled");
            }
            //reset and enable is hidden feild checkbox
            if ($(selectors.isHiddenField).attr("checked") == "checked") {
                $(selectors.isHiddenField).prop("checked", false);
                $(selectors.continueStep1Button).show();
            }
            // show the limitations tabs
            $(selectors.limitationsTab).removeClass("sfDisplayNoneImportant");
            // remove the pattern attribute from the default value input box
            $(selectors.defaultValue).removeAttr("pattern");
            // hide done button for hidden field
            if (!($(selectors.isHiddenField).is(":checked"))) {
                $(selectors.finishHiddenFieldButton).hide();
            }
            $(selectors.isHiddenBox).show();
            //make localizable checkbox
            $(selectors.isLocalizable).parent().hide();
            $(selectors.moreOptionsBox).hide();
            $(selectors.isLocalizable).prop("checked", false);
            $(selectors.disableLinkParser).prop("checked", false);
            // hide address field mode
            $(selectors.addressModeBox).hide();
        },

        // shows only the user interface elements specific
        // to the short text type of field
        prepareShortTextUI: function () {
            // show the widget types for the short text field
            $(selectors.widgetType.shortText).show();
            $(selectors.widgetType.label).show();
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // select the first widget type
            $(selectors.widgetType.shortText + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.shortText]);
            // set the field type title
            $(selectors.fieldTypeTitle).text("Textbox");
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the db length box
            $(selectors.dbLengthBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // add 'varchar' dbtype
            var varcharType = "VARCHAR";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));
            //isLocalizable should be checked by default
            $(selectors.isLocalizable).prop("checked", true);

            $(selectors.moreOptionsBox).show();
        },

        prepareAddressUI: function () {
            // show the widget types for the short text field
            $(selectors.widgetType.address).show();
            $(selectors.widgetType.label).show();
            // select the first widget type
            $(selectors.widgetType.address + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.address]);
            // set the field type title
            $(selectors.fieldTypeTitle).text("Address");
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            $(selectors.addressModeBox).show();
        },

        // shows only the user interface elements specific
        // to the long text type of field
        prepareLongTextUI: function () {
            // show the widet types for the long text field
            $(selectors.widgetType.longText).show();
            $(selectors.widgetType.label).show();
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // select the first widget type
            $(selectors.widgetType.longText + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.longText]);
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // add 'CLOB' dbtype
            var varcharType = "CLOB";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));
            //isLocalizable should be checked by default
            $(selectors.isLocalizable).prop("checked", true);
            //disableLinkParser should not be checked by default
            $(selectors.disableLinkParser).prop("checked", false);
            //show more options setting
            $(selectors.moreOptionsBox).show();
        },

        // shows only the user interface elements specific
        // to the multiple choice type of field
        prepareMultipleChoiceUI: function () {
            if ($(selectors.isHiddenField).is(":checked")) {
                $(".sfHide").show();
                $(selectors.finishHiddenFieldButton).hide();
                $(selectors.continueStep1Button).show();
                $(selectors.isHiddenField).prop("checked", false);
            }
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // show the checkbox for single/multiple choice
            $(selectors.canSelectMultipleBox).show();
            // uncheck the can select multiple
            $(selectors.isMultiple).prop("checked", false);
            // show the widget types for the multiple choice field
            $(selectors.widgetType.singleChoice).show();
            $(selectors.widgetType.label).show();
            // select the first widget type
            $(selectors.widgetType.singleChoice + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.singleChoice]);
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.multipleChoice]);
            // hide the isHidden checkbox
            $(selectors.isHiddenBox).hide();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // add 'varchar' dbtype
            var varcharType = "VARCHAR";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));
        },

        // validate the multiple choice field
        validateChoices: function (fieldType) {
            var questionText = $.trim($(selectors.questionText).val());
            if (questionText == "") {
                var errorMessage = document.createElement("div");
                $(errorMessage).addClass("sfError");
                $(errorMessage).html("This field is required!");
                $(selectors.questionText).after(errorMessage);
                return false;
            }

            var values = [];
            var isValid = true;
            if (fieldType = 'Choices') {
                $(selectors.choicesEditor.choiceList).find(selectors.choicesEditor.choiceItem + ' input.sfVal').each(function () {
                    var val = $(this).val();
                    if (values.indexOf(val) < 0) {
                        values.push(val);
                    }
                    else {
                        errorMessage = document.createElement("div");
                        $(errorMessage).addClass("sfError");
                        $(errorMessage).html("You cannot have duplicate values!");
                        $(selectors.choicesEditor.choiceList).after(errorMessage);
                        isValid = false;
                        return false;
                    }
                });
            }
            if (!isValid) {
                return false;
            }

            var choiceCounter = 0;
            $(selectors.choicesEditor.choiceList).find(selectors.choicesEditor.choiceItem).each(function () {
                $(this).find("input.sfTxt").each(function () {
                    var choiceText = $.trim($(this).val());
                    if (choiceText) {
                        choiceCounter++;
                    }
                });
            });

            if (choiceCounter < 2) {
                errorMessage = document.createElement("div");
                $(errorMessage).addClass("sfError");
                $(errorMessage).html("You need at least 2 choices filled with text!");
                $(selectors.choicesEditor.choiceList).after(errorMessage);
                return false;
            }

            return true;
        },

        // create the design for the multiplechoice field
        choiceEditorDesign: function (useOldTemplate) {
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // clear all choices
            $(selectors.choicesEditor.choiceList).empty();
            //set default value for the question
            $(selectors.questionText).val("Select a choice");
            // create three sample choices
            var choiceText;
            var choiceValue;
            for (var i = 1; i < 4; i++) {
                switch (i) {
                    case 1:
                        choiceText = "First Choice";
                        choiceValue = "1"
                        break;
                    case 2:
                        choiceText = "Second Choice";
                        choiceValue = "2"
                        break;
                    case 3:
                        choiceText = "Third Choice";
                        choiceValue = "3"
                        break;
                }

                var newChoice;
                if (useOldTemplate) {
                    $(selectors.choicesEditor.choiceListHeader).hide();
                    newChoice = $(selectors.choicesEditor.choiceItemTemplateObsolete)
                        .clone()
                        .removeAttr("id")
                        .show();
                }
                else {
                    $(selectors.choicesEditor.choiceListHeader).show();
                    var newChoice = $(selectors.choicesEditor.choiceItemTemplate)
                        .clone()
                        .removeAttr("id")
                        .show();
                    newChoice.find('input.sfVal').val(choiceValue);
                }

                newChoice.find('input.sfTxt').val(choiceText);

                methods["wireUpChoiceItem"].apply(this, [newChoice]);
                var wrapperCheckbox = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
                $(selectors.choicesEditor.choiceList).append(wrapperCheckbox);
            }

            if ($(selectors.isMultiple).is(":checked")) {
                var multipleDataChoiceType = $(selectors.multipleChoiceSelectedItem).attr("data-choice-type").toLowerCase();

                switch (multipleDataChoiceType) {
                    case "checkbox":
                        {
                            $(selectors.dropDownListMakeRequiredCheckBoxContainer).hide();
                            $(selectors.oneRequiredCheckBoxContainer).hide();
                            $(selectors.isntructionalChoiceBox).hide();
                            $(selectors.fieldTypeTitle).text("Checkboxes");
                            break;
                        }
                    case "custom":
                        $(selectors.dropDownListMakeRequiredCheckBoxContainer).hide();
                        $(selectors.oneRequiredCheckBoxContainer).hide();
                        $(selectors.isntructionalChoiceBox).hide();
                        $(selectors.fieldTypeTitle).text("Custom");
                        break;
                }
            }
            else {
                var singleDataChoiceType = $(selectors.singleChoiceSelectedItem).attr("data-choice-type").toLowerCase();
                switch (singleDataChoiceType) {
                    case "radiobutton":
                        {
                            $(selectors.dropDownListMakeRequiredCheckBoxContainer).hide();
                            $(selectors.oneRequiredCheckBoxContainer).hide();
                            $(selectors.isntructionalChoiceBox).hide();
                            $(selectors.fieldTypeTitle).text("Radio buttons");
                            break;
                        }
                    case "dropdownlist":
                        {
                            $(selectors.dropDownListMakeRequiredCheckBoxContainer).hide();
                            $(selectors.oneRequiredCheckBoxContainer).hide();
                            $(selectors.isntructionalChoiceBox).hide();
                            $(selectors.fieldTypeTitle).text("Dropdown list");
                            break;
                        }
                    case "custom":
                        $(selectors.dropDownListMakeRequiredCheckBoxContainer).hide();
                        $(selectors.oneRequiredCheckBoxContainer).hide();
                        $(selectors.isntructionalChoiceBox).hide();
                        $(selectors.fieldTypeTitle).text("Custom");
                        break;
                }
            }
            // enable sorting of the choices
            $(selectors.choicesEditor.choiceList).sortable({ axis: 'y', containment: 'parent', cursor: 'crosshair', handle: '.sfSortHandle' });
            // show the advanced section
            $(selectors.advancedSectionBox).show();
        },

        wireUpChoiceItem: function (choiceItem) {
            // wire up add choice buttons
            $(choiceItem).find(selectors.choicesEditor.addChoice).each(function () {
                $(this).click(function () {
                    var newChoice = $(choiceItem).clone().removeAttr("id").show();
                    newChoice.find("input.sfTxt").val('');
                    newChoice.find("input.sfVal").val('');
                    methods["wireUpChoiceItem"].apply(this, [newChoice]);
                    var wrapper = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
                    $(wrapper).insertAfter($(choiceItem).closest(selectors.choicesEditor.choiceItem));
                });
            });
            // wire up remove choice buttons
            $(choiceItem).find(selectors.choicesEditor.removeChoice).each(function () {
                $(this).click(function () {
                    var choiceCounter = 0;
                    $(selectors.choicesEditor.choiceList).find(selectors.choicesEditor.choiceItem).each(function () {
                        $(this).find("input.sfTxt").each(function () {
                            choiceCounter++;
                        });
                    });
                    if (choiceCounter == 2) {
                        $(selectors.choicesEditor.choiceList).siblings(".sfError").remove();
                        errorMessage = document.createElement("div");
                        $(errorMessage).addClass("sfError");
                        $(errorMessage).html("You cannot have less than two options!");
                        $(selectors.choicesEditor.choiceList).after(errorMessage);
                        return;
                    }
                    $(choiceItem).parent().remove();
                });
            });
            //add validation 
            $(choiceItem).find("input.sfVal").attr('required', 'required').attr('pattern', '^[a-zA-Z_0-9]+\w*$');
        },

        // shows only the user interface elements specific
        // to the yes / no type of field
        prepareYesNoUI: function () {
            // show the widget types for the yes / no field
            $(selectors.widgetType.yesNo).show();
            $(selectors.widgetType.label).show();
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // select the first widget type
            $(selectors.widgetType.yesNo + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.yesNo]);
            $(selectors.fieldTypeTitle).text("Checkbox");
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // add 'BIT' dbtype
            var varcharType = "BIT";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));
        },

        // shows only the user interface elements specific
        // to the currency type of field
        prepareCurrencyUI: function () {
            if ($(selectors.isHiddenField).is(":checked")) {
                return;
            }
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the widget types for the currency field
            $(selectors.widgetType.currency).show();
            $(selectors.widgetType.label).show();
            // show the selector for number of decimal places
            $(selectors.decimalPlacesCurrencyBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // add 'DECIMAL' dbtype
            var varcharType = "DECIMAL";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));
        },

        // shows only the user interface elements specific
        // to the date time type of field
        prepareDateTimeUI: function () {
            // show the widget types for the date time field
            $(selectors.widgetType.dateTime).show();
            $(selectors.widgetType.label).show();
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // select the first widget type
            $(selectors.widgetType.dateTime + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.dateTime]);
            $(selectors.fieldTypeTitle).text("Date picker");
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // set allow nulls by default
            $(selectors.allowNulls).prop("checked", true);
            // add 'DATE' dbtype
            var varcharType = "DATE";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));

            $(selectors.moreOptionsBox).show();
        },

        // shows only the user interface elements specific
        // to the number type of field
        prepareNumberUI: function () {
            if ($(selectors.isHiddenField).is(":checked")) {
                return;
            }
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            $(selectors.fieldTypeTitle).text("Number box");
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // show the widget types for number field type
            $(selectors.widgetType.number).show();
            $(selectors.widgetType.label).show();
            // select the first widget type
            $(selectors.widgetType.number + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.number]);
            // show the selector for number of decimal places
            $(selectors.decimalPlacesBox).show();
            // select the first option from the drop down
            $(selectors.decimalPlacesCount).val("0");
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // set allow nulls by default
            $(selectors.allowNulls).prop("checked", true);
            // add 'NUMERIC' dbtype
            var varcharType = "NUMERIC";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));

            $(selectors.moreOptionsBox).show();
        },

        prepareMediaUI: function () {
            var mediaFieldTypeId = $("input[name='mediaFieldType']:checked").attr("id");
            switch (mediaFieldTypeId) {
                case "typeFieldEditor_imageMediaFieldType":
                    // show the widget types for image field type
                    $(selectors.widgetType.imageMedia).show();
                    $(selectors.widgetType.imageMedia).change();
                    break;
                case "typeFieldEditor_videoMediaFieldType":
                    // show the widget types for video field type
                    $(selectors.widgetType.videoMedia).show();
                    $(selectors.widgetType.videoMedia).change();
                    break;
                case "typeFieldEditor_fileMediaFieldType":
                    // show the widget types for file field type
                    $(selectors.widgetType.fileMedia).show();
                    $(selectors.widgetType.fileMedia).change();
                    break;
            }
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // hide the advanced information 
            $(selectors.advancedSectionBox).hide();
            // hide the is hidden checkbox
            $(selectors.isHiddenBox).hide();
        },

        prepareRelatedMediaUI: function () {
            //prepare the UI for the related contents
            methods["prepareRelatedMediaSources"].apply();

            $(selectors.widgetType.label).hide();
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // hide the advanced information 
            $(selectors.advancedSectionBox).hide();
            // hide the is hidden checkbox
            $(selectors.isHiddenField).prop("checked", false);
            $(selectors.isHiddenBox).hide();
        },

        prepareRelatedDataUI: function () {
            //prepare the UI for the related contents
            $(selectors.widgetType.label).hide();
            $(selectors.moduleTypesDropDown).show();
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // hide the advanced information 
            $(selectors.advancedSectionBox).hide();
            // hide the is hidden checkbox
            $(selectors.isHiddenField).prop("checked", false);
            $(selectors.isHiddenBox).hide();
        },

        hideSourceDropDown: function (dropDownSelector) {
            var $dropDown = $(dropDownSelector).data("kendoDropDownList");
            if ($dropDown && $dropDown.element) {
                $dropDown.element.closest(":has(h2)").find("h2").last().hide();
                $dropDown.element.closest(".k-widget").hide();
            }
        },
        showSourceDropDown: function (dropDownSelector) {
            var $dropDown = $(dropDownSelector).data("kendoDropDownList");
            if ($dropDown && $dropDown.element) {
                $dropDown.element.closest(":has(h2)").find("h2").last().show();
                $dropDown.element.closest(".k-widget").show();
            }
        },

        prepareGuidUI: function () {

            // show the widget types for the short text field
            $(selectors.widgetType.guid).show();
            // select the first widget type
            $(selectors.widgetType.guid + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.guid]);
            // set the field type title
            $(selectors.fieldTypeTitle).text("Textbox");

            $(selectors.isHiddenField).attr("checked", "checked");
            // $(selectors.isHiddenField).attr("disabled", "disabled");

            methods["hiddenFieldInterface"].apply(this, [false]);

            //set validation regular expression
            $(selectors.regExValidationValue).val("^{?([0-9a-fA-F]){8}(-([0-9a-fA-F]){4}){3}-([0-9a-fA-F]){12}}?$");

            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
            // add 'Guid' dbtype
            var type = "GUID";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", type).text(type));
        },

        prepareGuidArrayUI: function () {

            // show the widget types for the short text field
            $(selectors.widgetType.guidArray).show();
            // select the first widget type
            $(selectors.widgetType.guidArray + " option:first").attr("selected", "selected");
            // handle the change of the selected item
            methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.guidArray]);
            // set the field type title
            $(selectors.fieldTypeTitle).text("Custom");

            $(selectors.isHiddenField).attr("checked", "checked");

            methods["hiddenFieldInterface"].apply(this, [false]);

            //reset validation regular expression
            $(selectors.regExValidationValue).val("");

            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).hide();
        },

        // shows only the user interface elements specific
        // to the classification type of field
        prepareClassificationUI: function () {
            if ($(selectors.isHiddenField).is(":checked")) {
                $(".sfHide").show();
                $(selectors.finishHiddenFieldButton).hide();
                $(selectors.continueStep1Button).show();
                $(selectors.isHiddenField).prop("checked", false);
            }
            $(selectors.isHiddenBox).hide();
            // get the classification dropdown value
            var classificationName = $(selectors.classificationDropdown + " option:selected").text();
            classificationName = classificationName.replace(/ /g, "");
            // set the field name textbox value
            $(selectors.fieldName).val(classificationName);
            // decide the widget for the taxonomy
            var classificationType = $(selectors.classificationDropdown + " option:selected").attr("data-Type");
            switch (classificationType) {
                case "HierarchicalTaxonomy":
                    $(selectors.widgetType.flatClassification).hide();
                    $(selectors.widgetType.hierarchicalClassification).show();
                    // select the first widget type
                    $(selectors.widgetType.hierarchicalClassification + " option:first").attr("selected", "selected");
                    // handle the change of the selected item
                    methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.hierarchicalClassification]);
                    $(selectors.fieldTypeTitle).text("Tree-like selector( select from a tree)");
                    break;
                case "FlatTaxonomy":
                    $(selectors.widgetType.hierarchicalClassification).hide();
                    $(selectors.widgetType.flatClassification).show();
                    // select the first widget type
                    $(selectors.widgetType.flatClassification + " option:first").attr("selected", "selected");
                    // handle the change of the selected item
                    methods["changeWidgetTypeSelection"].apply(this, [selectors.widgetType.flatClassification]);
                    $(selectors.fieldTypeTitle).text("Textbox selector( select as you type)");
                    break;
            }
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // disable the field name textbox
            $(selectors.fieldName).attr("disabled", "disabled");
            // show the ui for choosing the classification
            $(selectors.classificationBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
        },

        // shows only the user interface elements specific
        // to the Meta field type of field
        prepareMetaFiledUI: function () {
            if ($(selectors.isHiddenField).is(":checked")) {
                $(".sfHide").show();
                $(selectors.finishHiddenFieldButton).hide();
                $(selectors.continueStep1Button).show();
                $(selectors.isHiddenField).prop("checked", false);
            }
            $(selectors.isHiddenBox).hide();
            // get the meta field type dropdown value
            var metaFieldName = $(selectors.metaFieldDropdown + " option:selected").val();
            metaFieldName = metaFieldName.replace(/ /g, "");
            // set the field name textbox value
            $(selectors.fieldName).val(metaFieldName);

            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // disable the field name textbox
            $(selectors.fieldName).attr("disabled", "disabled");
            // show the ui for choosing a meta field type
            $(selectors.metaFieldBox).show();
            // hide the advanced section
            $(selectors.advancedSectionBox).hide();
            // add 'varchar' dbtype
            var varcharType = "VARCHAR";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));
            //isLocalizable should be checked by default
            $(selectors.isLocalizable).prop("checked", true);

            $(selectors.moreOptionsBox).show();
        },

        // shows only the user interface elements specific
        // to the OpenGraph field type of field
        prepareOpenGraphFiledUI: function () {
            if ($(selectors.isHiddenField).is(":checked")) {
                $(".sfHide").show();
                $(selectors.finishHiddenFieldButton).hide();
                $(selectors.continueStep1Button).show();
                $(selectors.isHiddenField).prop("checked", false);
            }
            $(selectors.isHiddenBox).hide();
            // get the OpenGraph field type dropdown value
            var openGraphFieldName = $(selectors.openGraphFieldDropdown + " option:selected").val();
            openGraphFieldName = openGraphFieldName.replace(/ /g, "");
            // set the field name textbox value
            $(selectors.fieldName).val(openGraphFieldName);

            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            // disable the field name textbox
            $(selectors.fieldName).attr("disabled", "disabled");
            // show the ui for choosing an OpenGraph field type
            $(selectors.openGraphFieldBox).show();
            // hide the advanced section
            $(selectors.advancedSectionBox).hide();
            // add 'varchar' dbtype
            var varcharType = "VARCHAR";
            $(selectors.dbTypeDropdown).append($("<option></option>").attr("value", varcharType).text(varcharType));

            //isLocalizable should be checked by default
            $(selectors.isLocalizable).prop("checked", true);

            $(selectors.moreOptionsBox).show();

            if (openGraphFieldName === "OpenGraphImage" || openGraphFieldName === "OpenGraphVideo") {
                // preaprae data sources
                methods["prepareRelatedMediaSources"].apply(this, [true]);
            }
        },

        // shows only the user interface elements specific
        // to the unknown type of field
        prepareUnknownUI: function () {
            //reset validation regular expression
            $(selectors.regExValidationValue).val("");
            $(selectors.widgetType.unknown).show();
            // show the isHidden checkbox
            $(selectors.isHiddenBox).show();
            // show the advanced section
            $(selectors.advancedSectionBox).show();
        },

        // expands the advanced section for defining the data properties
        // of the field
        expandAdvancedSection: function () {
            $(selectors.advancedSection).toggle().parent().toggleClass("sfExpandedSection");
        },

        expandMoreOptionsSection: function () {
            $(selectors.moreOptionsSection).toggle().parent().toggleClass("sfExpandedSection");
        },

        getAddressFieldMode: function () {
            var addressFieldMode;
            var addressFieldModeId = $(selectors.addressField.checkedAddressFieldMode).attr("id");
            switch (addressFieldModeId) {
                case "typeFieldEditor_addressFieldFormOnly":
                    addressFieldMode = "FormOnly";
                    break;
                case "typeFieldEditor_addressFieldMapOnly":
                    addressFieldMode = "MapOnly";
                    break;
                case "typeFieldEditor_addressFieldFormMap":
                    addressFieldMode = "Hybrid";
                    break;
            }
            return addressFieldMode;
        },

        // Transfers and modifies the field name
        // to the label text field in the second step
        transferFieldName: function (fieldName) {
            var labelName = "";
            var counter = 0;
            for (var i = 0; i < fieldName.length; i++) {
                var currentCharacter = fieldName.charAt(i);
                if (currentCharacter == currentCharacter.toUpperCase() && counter == 0) {
                    labelName = labelName + currentCharacter;
                    counter++;
                }
                else if (currentCharacter == currentCharacter.toUpperCase() && counter > 0) {
                    labelName = labelName + " " + currentCharacter.toLowerCase();
                }
                else {
                    labelName = labelName + currentCharacter;
                }
            }
            return labelName;
        },

        // Sets the regular expression for the min and max range for numbers
        setMinMaxNumberRangeRegEx: function (decimalPlaces) {
            // TODO: Add a resolver for the current culture separator!
            if (decimalPlaces == 0) {
                // in case of "no decimal places" chosen, we use the 
                // regex pattern for integer value
                var regExPatternNoDecPlaces = "^((0|(-)?[1-9]{1}\\d*))?$";
                $("#typeFieldEditor_minRange").attr("pattern", regExPatternNoDecPlaces);
                $("#typeFieldEditor_maxRange").attr("pattern", regExPatternNoDecPlaces);
                $(selectors.defaultValue).attr("pattern", regExPatternNoDecPlaces);
            }
            else {
                // in case of using some decimal places, we set regex pattern
                // for real numbers
                var regExPattern = "^((0|(-)?[1-9]{1}\\d*)(((\\.|\\,)\\d{1," + decimalPlaces + "}){1})?)?$";
                $("#typeFieldEditor_minRange").attr("pattern", regExPattern);
                $("#typeFieldEditor_maxRange").attr("pattern", regExPattern);
                $(selectors.defaultValue).attr("pattern", regExPattern);
            }
        },

        // Sets the regular expression for the min and max range for currency
        setMinMaxCurrencyRangeRegEx: function (decimalPlaces) {
            // TODO: Add a resolver for the current culture separator!
            if (decimalPlaces == 0) {
                // in case of "no decimal places" chosen, we use the 
                // regex pattern for integer value
                var regExPatternNoDecPlaces = "^((0|([1-9]{1,3}(\\,\\d{3})*)|(\\d+)))?$";
                //var old = "^((0|(-)?[1-9]{1}\\d*))?$";
                $("#typeFieldEditor_minRange").attr("pattern", regExPatternNoDecPlaces);
                $("#typeFieldEditor_maxRange").attr("pattern", regExPatternNoDecPlaces);
                $(selectors.defaultValue).attr("pattern", regExPatternNoDecPlaces);
            }
            else {
                // in case of using some decimal places, we set regex pattern
                // for real numbers
                var regExPattern = "^((0|([1-9]{1,3}(\\,\\d{3})*|(\\d+)))(((\\.)\\d{1," + decimalPlaces + "}){1})?)?$";
                //var old2 = "^((0|[1-9]{1}\\d*)(((\\.|\\,)\\d{1," + decimalPlaces + "}){1})?)?$";
                $("#typeFieldEditor_minRange").attr("pattern", regExPattern);
                $("#typeFieldEditor_maxRange").attr("pattern", regExPattern);
                $(selectors.defaultValue).attr("pattern", regExPattern);
            }
        },

        // handle the change of the widget type for every field typ
        changeWidgetTypeSelection: function (fieldTypeWidgetName) {
            $(fieldTypeWidgetName).change(function () {
                var selectedValue = $(fieldTypeWidgetName + " option:selected").val();
                if (selectedValue != "Custom") {
                    $(selectors.customWidgetTypeBox).hide();
                }
            });
        },

        // prepares the type field editor for editing field
        prepareEditField: function (fields, editedFieldKey) {
            for (var i = 0; i < fields.length; i++) {
                if (editedFieldKey == fields[i].Name) {
                    if (fields[i].IsHiddenField) {
                        // we do not allow hidden fields to be edited
                        alert("Hidden fields cannot be edited!");
                        return false;
                    }

                    var isNotOpenGraphMedia = true;
                    if (fields[i].Name === "OpenGraphImage" || fields[i].Name === "OpenGraphVideo") {
                        isNotOpenGraphMedia = false;

                        if (fields[i].Name === "OpenGraphImage") {
                            $(selectors.fieldTypeTitle).text("OpenGraph image");
                        }
                        else {
                            $(selectors.fieldTypeTitle).text("OpenGraph video");
                        }

                        $(selectors.defaultTitle).hide();
                        $(selectors.fieldTypeTitle).show();
                    }

                    var relatedTypes = ["RelatedMedia", "RelatedData"];
                    if (relatedTypes.indexOf(fields[i].TypeName) !== -1) {
                        var model = {
                            mainFormSelector: selectors.step1,
                            container: selectors.editorSelectedFieldContainer,
                            fieldType: fields[i].TypeName,
                            field: fields[i],
                            isNotOpenGraphMedia: isNotOpenGraphMedia
                        };
                        isEditMode = true;

                        // open field editor on EDIT of dynamic module field
                        fieldEditor.open(model, true);

                        return true;
                    }
                    // MultipleChoice option is removed from the field type dropdown
                    // we need to add it manually in order to support old choice fields editing
                    if (fields[i].TypeName == "MultipleChoice" &&
                        $(selectors.fieldType + ' option[value="MultipleChoice"]').length == 0) {
                        $(selectors.fieldType).append($("<option></option>").attr("value", "MultipleChoice").text(fields[i].TypeUIName));
                    }
                    // Media field option is removed from the field type dropdown
                    // we need to add it manually in order to support old media fields editing
                    if (fields[i].TypeName == "Media" &&
                        $(selectors.fieldType + ' option[value="Media"]').length == 0) {
                        $(selectors.fieldType).append($("<option></option>").attr("value", "Media").text(fields[i].TypeUIName));
                    }
                    methods["prepareEditFieldUI"].call(this, fields[i]);
                    return true;
                }
            }
        },

        prepareEditFieldUI: function (field) {
            isEditMode = true;

            $(selectors.parentSectionId).val(field.ParentSectionId);
            $(selectors.showInGrid).val(field.ShowInGrid);
            $(selectors.gridColumnOrdinal).val(field.GridColumnOrdinal);

            methods["enableDoneStep2Button"].apply();
            $(selectors.fieldType).val(field.TypeName).change();

            // hack: simulate click event so that ui is refreshed
            $(selectors.isHiddenField).prop('checked', field.IsHiddenField);
            $(selectors.isHiddenField).click();
            $(selectors.isHiddenField).prop('checked', field.IsHiddenField);

            var widgetTypeSelect = null;
            switch (field.TypeName) {
                case "ShortText":
                    widgetTypeSelect = $(selectors.widgetType.shortText);
                    break;
                case "LongText":
                    widgetTypeSelect = $(selectors.widgetType.longText);
                    break;
                case "YesNo":
                    widgetTypeSelect = $(selectors.widgetType.yesNo);
                    break;
                case "Currency":
                    widgetTypeSelect = $(selectors.widgetType.currency);
                    break;
                case "Classification":
                    widgetTypeSelect = $(selectors.widgetType.hierarchicalClassification);
                    break;
                case "Unknown":
                    widgetTypeSelect = $(selectors.widgetType.unknown);
                    break;
                case "Guid":
                    widgetTypeSelect = $(selectors.widgetType.guid);
                    break;
                case "GuidArray":
                    widgetTypeSelect = $(selectors.widgetType.guidArray);
                    break;
                case "Choices":
                case "MultipleChoice":
                    if ($(selectors.widgetType.singleChoice).find('option[data-choice-type="' + field.ChoiceRenderType + '"]').length > 0) {
                        // we have single choice item selected
                        widgetTypeSelect = $(selectors.widgetType.singleChoice);
                    }
                    else if ($(selectors.widgetType.multipleChoice).find('option[data-choice-type="' + field.ChoiceRenderType + '"]').length > 0) {
                        // we have multiple choice item selected
                        widgetTypeSelect = $(selectors.widgetType.multipleChoice);
                        $(selectors.isMultiple).prop("checked", true).click().prop("checked", true);
                    }
                    else {
                        // set default
                        widgetTypeSelect = $(selectors.widgetType.singleChoice);
                    }

                    oldWidgetName = field.WidgetTypeName;
                    break;
                case "DateTime":
                    widgetTypeSelect = $(selectors.widgetType.dateTime);
                    if (field.MinNumberRange) {
                        var minRangeDatePicker = $(selectors.minDateValue).data("kendoDatePicker");
                        var minDateString = field.MinNumberRange.split("/");
                        var minDateValue = new Date(parseInt(minDateString[2]),
                            parseInt(minDateString[1]) - 1,
                            parseInt(minDateString[0]), 0, 0, 0, 0);
                        minRangeDatePicker.value(minDateValue);
                    }
                    if (field.MaxNumberRange) {
                        var maxRangeDatePicker = $(selectors.maxDateValue).data("kendoDatePicker");
                        var maxDateString = field.MaxNumberRange.split("/");
                        var maxDateValue = new Date(parseInt(maxDateString[2]),
                            parseInt(maxDateString[1]) - 1,
                            parseInt(maxDateString[0]), 0, 0, 0, 0);
                        maxRangeDatePicker.value(maxDateValue);
                    }
                    break;
                case "Number":
                    widgetTypeSelect = $(selectors.widgetType.number);
                    $(selectors.decimalPlacesCount).val(field.DecimalPlacesCount);
                    $(selectors.minRangeNumber).val(field.MinNumberRange);
                    $(selectors.maxRangeNumber).val(field.MaxNumberRange)
                    break;
                case "Media":
                    widgetTypeSelect = $(selectors.widgetType.imageMedia);
                    var mediaTypeIdToSelect = null;
                    switch (field.MediaType) {
                        case "image":
                            mediaTypeIdToSelect = "typeFieldEditor_imageMediaFieldType";
                            break;
                        case "video":
                            mediaTypeIdToSelect = "typeFieldEditor_videoMediaFieldType";
                            break;
                        case "file":
                            mediaTypeIdToSelect = "typeFieldEditor_fileMediaFieldType";
                            break;
                    }
                    var input = $("input[name='mediaFieldType'][id='" + mediaTypeIdToSelect + "']");
                    input.prop('checked', true);
                    input.click();
                    break;
                case "Address":
                    widgetTypeSelect = $(selectors.widgetType.address);
                    var addressFieldMode = null;
                    switch (field.AddressFieldMode) {
                        case "FormOnly":
                            addressFieldMode = "typeFieldEditor_addressFieldFormOnly";
                            break;
                        case "MapOnly":
                            addressFieldMode = "typeFieldEditor_addressFieldMapOnly";
                            break;
                        case "Hybrid":
                            addressFieldMode = "typeFieldEditor_addressFieldFormMap";
                            break;
                    }
                    var inputAddressMode = $("input[name='addressFieldMode'][id='" + addressFieldMode + "']");
                    inputAddressMode.prop('checked', true);
                    inputAddressMode.click();
                    break;
                case "RelatedData":
                    break;
                case "Seo":
                    var openGraphFieldName = $(selectors.fieldName).val();
                    switch (openGraphFieldName) {
                        case "MetaTitle":
                            widgetTypeSelect = $(selectors.widgetType.shortText);
                            break;
                        case "MetaDescription":
                            widgetTypeSelect = $(selectors.widgetType.longText);
                            break;
                    }
                    break;
                case "OpenGraph":
                    var openGraphFieldName = $(selectors.fieldName).val();
                    switch (openGraphFieldName) {
                        case "OpenGraphTitle":
                            widgetTypeSelect = $(selectors.widgetType.shortText);
                            break;
                        case "OpenGraphDescription":
                            widgetTypeSelect = $(selectors.widgetType.longText);
                            break;
                    }
                    break;
            }

            if (field.WidgetTypeName && field.WidgetTypeName != "") {

                // logic for multiple choice
                if (field.TypeName == "MultipleChoice" || field.TypeName == "Choices") {
                    if (widgetTypeSelect.find('option[data-choice-type="' + field.ChoiceRenderType + '"]').length > 0) {
                        var element = $(widgetTypeSelect.find('option[data-choice-type="' + field.ChoiceRenderType + '"]')[0]);
                        element.attr("selected", true);
                        element.change();
                        $(selectors.customWidgetTypeName).val("");
                    }
                    if (field.ChoiceRenderType == "custom") {
                        $(selectors.customWidgetTypeName).val(field.WidgetTypeName);
                    }
                }
                // logic for everything else selected by value
                else if (widgetTypeSelect.find('option[value="' + field.WidgetTypeName + '"]').length > 0) {
                    widgetTypeSelect.val(field.WidgetTypeName).change();
                    $(selectors.customWidgetTypeName).val("");
                }
                else {
                    widgetTypeSelect.val("Custom").change();
                    $(selectors.customWidgetTypeName).val(field.WidgetTypeName);
                }
            }

            $(selectors.dbTypeDropdown).val(field.DBType);

            if (field.TypeName == "Classification") {
                if (field.ClassificationId != null) {
                    $(selectors.classificationDropdown).val(field.ClassificationId).change();
                }
                else {
                    $(selectors.classificationDropdown).val(emptyGuid).change();
                }
            }

            $(selectors.numberUnit).val(field.NumberUnit);
            $(selectors.dbLength).val(field.DBLength);
            $(selectors.allowNulls).prop("checked", field.AllowNulls);
            $(selectors.disableLinkParser).prop("checked", field.disableLinkParser);
            $(selectors.includeInIndexes).prop("checked", field.IncludeInIndexes);
            $(selectors.columnName).val(field.ColumnName);
            if (field.CanSelectMultipleItems) {
                $(selectors.canSelectMultipleClassificationItems).prop("checked", true);
            }
            else {
                $(selectors.singleClassificationItem).prop("checked", true);
            }
            $(selectors.canCreateClassificationItemsWhenSelecting).prop("checked", field.CanCreateItemsWhileSelecting);
            $(selectors.fieldTitle).val(field.Title);
            $(selectors.fieldLabelText).val(field.Title);
            $(selectors.instructionalText).val(field.InstructionalText);
            $(selectors.defaultValue).val(field.DefaultValue);
            $(selectors.isRequired).prop("checked", field.IsRequired);
            $(selectors.isLocalizable).prop("checked", field.IsLocalizable);
            $(selectors.disableLinkParser).prop("checked", field.DisableLinkParser);
            $(selectors.regExValidationValue).val(field.LengthValidationMessage);

            if (field.TypeName == "MultipleChoice") {
                // clear all choices
                $(selectors.choicesEditor.choiceList).empty();
                // set default value for the question
                $(selectors.questionText).val(field.Title);
                // get all values from the field
                var choicesValues = field.Choices.split(',');
                for (var i = 0; i < choicesValues.length; i++) {
                    var newChoice = $(selectors.choicesEditor.choiceItemTemplateObsolete).clone().removeAttr("id").show();
                    var choiceFieldValue = methods["decodeHTML"].apply(this, [choicesValues[i]]);
                    $(newChoice).find("input.sfTxt").val(choiceFieldValue);
                    methods["wireUpChoiceItem"].apply(this, [newChoice]);
                    var wrapper = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
                    $(selectors.choicesEditor.choiceList).append(wrapper);
                }
                //hide header
                $(selectors.choicesEditor.choiceListHeader).hide();
                // enable sorting of the choices
                $(selectors.choicesEditor.choiceList).sortable({ axis: 'y', containment: 'parent', cursor: 'crosshair', handle: '.sfSortHandle' });
            }
            else if (field.TypeName == "Choices") {
                // clear all choices
                $(selectors.choicesEditor.choiceList).empty();
                // set default value for the question
                $(selectors.questionText).val(field.Title);
                // get all values from the field
                var choices = $($.parseXML(field.Choices));

                choices.find('choice').each(function (index) {
                    var value = $(this).attr('value');
                    var text = methods["decodeHTML"].apply(this, [$(this).attr('text')]);
                    var newChoice = $(selectors.choicesEditor.choiceItemTemplate).clone().removeAttr("id").show();
                    $(newChoice).find("input.sfTxt").val(text);
                    $(newChoice).find("input.sfVal").val(value);
                    methods["wireUpChoiceItem"].apply(this, [newChoice]);
                    var wrapper = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
                    $(selectors.choicesEditor.choiceList).append(wrapper);
                });
                //show header
                $(selectors.choicesEditor.choiceListHeader).show();
                // enable sorting of the choices
                $(selectors.choicesEditor.choiceList).sortable({ axis: 'y', containment: 'parent', cursor: 'crosshair', handle: '.sfSortHandle' });
            }

            $(selectors.checkedByDefault).prop("checked", field.CheckedByDefault);
            $(selectors.instructionalChoice).val(field.InstructionalChoice);
            $(selectors.dropDownListMakeRequiredCheckBox).prop("checked", field.IsRequiredToSelectDdlValue);
            $(selectors.oneRequiredCheckBox).prop("checked", field.IsRequiredToSelectCheckbox);

            if (field.AllowMultipleImages) {
                $(selectors.mediaType.allowMultipleImages).prop("checked", true);
            } else {
                $(selectors.mediaType.canUploadSingleImage).prop("checked", true);
            }
            if (field.AllowMultipleVideos) {
                $(selectors.mediaType.allowMultipleVideos).prop("checked", true);
            } else {
                $(selectors.mediaType.canUploadSingleVideo).prop("checked", true);
            }
            if (field.AllowMultipleFiles) {
                $(selectors.mediaType.allowMultipleFiles).prop("checked", true);
            } else {
                $(selectors.mediaType.canUploadSingleFile).prop("checked", true);
            }
            $(selectors.mediaType.allowImageLibrary).prop("checked", field.AllowImageLibrary);

            // prevent property validation if length is not set
            $(selectors.minLength).val(field.MinLength == 0 ? "" : field.MinLength);
            $(selectors.maxLength).val(field.MaxLength == 0 ? "" : field.MaxLength);

            $(selectors.recommendedCharactersCount).val(field.RecommendedCharactersCount == null ? "" : field.RecommendedCharactersCount);

            $(selectors.mediaType.imageMaxSize).val(field.ImageMaxSize == 0 ? "" : field.ImageMaxSize);
            $(selectors.mediaType.videoMaxSize).val(field.VideoMaxSize == 0 ? "" : field.VideoMaxSize);
            $(selectors.mediaType.fileMaxSize).val(field.FileMaxSize == 0 ? "" : field.FileMaxSize);
            $(selectors.regExValidationValue).val(field.RegularExpression == null ? "" : field.RegularExpression);

            $(selectors.mediaType.imageExtensions).val(field.ImageExtensions);
            $(selectors.mediaType.fileExtensions).val(field.FileExtensions);
            $(selectors.mediaType.videoExtensions).val(field.VideoExtensions);

            $(selectors.fieldName).val(field.Name);
            methods["goToStep2"].apply(this, [field.Name]);
        },

        prepareRelatedMediaSources: function (hideMediaTypeDropdown) {
            hideMediaTypeDropdown = hideMediaTypeDropdown || false;

            //prepare the UI for the related contents
            $(selectors.mediaDropDown).show();

            if (hideMediaTypeDropdown === true) {
                $(selectors.mediaDropDown).find("h2").first().hide();
                $(selectors.mediaDropDown).find(".sfDropdownList").first().hide();
            }
            else {
                $(selectors.mediaDropDown).find("h2").first().show();
                $(selectors.mediaDropDown).find(".sfDropdownList").first().show();
                $(selectors.mediaDropDown).find('select option:first-child').attr("selected", "selected");
            }
        },
    };

    // type field editor plugin. This plugin provides functionality
    // for creating or modifying dynamic fields.
    $.fn.typeFieldEditor = function (method) {

        // Method calling logic
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.typeFieldEditor');
        }

    };

});