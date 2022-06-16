define(function () {
    function FieldEditor() {
        this.model = null;
        this.defaultWidgetTypeNameRelatedMedia = "default";
        this.defaultWidgetTypeNameRelatedData = "Telerik.Sitefinity.Web.UI.Fields.RelatedDataField";
        this.pagesType = "Telerik.Sitefinity.Pages.Model.PageNode";
        this.inlineFrontendControl = 'inline';
        this.dataTypes = null;
    };
    FieldEditor.prototype = {
        initialize: function (siteBaseUrl) {
            var that = this;
            this.siteBaseUrl = siteBaseUrl;
            this.configure(siteBaseUrl);

            require(["DataTypes"], function (DataTypes) {
                var dataTypes = new DataTypes();
                $('#typeFieldEditor_moduleTypesDropDown').html(dataTypes.template);
                dataTypes.initialize(siteBaseUrl);
                that.dataTypes = dataTypes;
            });
        },

        configure: function (siteBaseUrl) {
            requirejs.config({
                baseUrl: siteBaseUrl + "Res",
                paths: {
                    FieldTypeEditorFactory: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldTypeEditorFactory',
                    RelatedMedia: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.RelatedMedia',
                    RelatedData: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.RelatedData',
                    DataTypes: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.DataTypes',

                    DataTypesTemplate: 'Telerik.Sitefinity.ModuleEditor.Web.Templates.DataTypes.sfhtml',
                    RelatedMediaTemplate: 'Telerik.Sitefinity.ModuleEditor.Web.Templates.RelatedMedia.sfhtml',
                    RelatedDataTemplate: 'Telerik.Sitefinity.ModuleEditor.Web.Templates.RelatedData.sfhtml'
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
            var that = this;
            that.model = model;

            // prepare received field definitions in format for media field, set defaults if there aren't any
            that.prepare(that.model.field, isEditMode);

            var mediaType = that.model.field.Value.Definition.MediaType;
            $(model.mainFormSelector).hide();

            var moduleTypeName = that.getModuleTypeName();

            options = {
                dataModel: {},
                mediaType: {
                    isImage: mediaType === "image",
                    isVideo: mediaType === "video",
                    isFile: mediaType === "file"
                },
                isNotOpenGraphMedia: model.isNotOpenGraphMedia,
                moduleTypeName: moduleTypeName,
                dataType: {
                    isPage: moduleTypeName === "Pages",
                    isContentType: moduleTypeName === "DynamicType",
                    isNews: moduleTypeName === "News",
                    isEvents: moduleTypeName === "Events",
                    isBlogPosts: moduleTypeName === "Blog Posts",
                	isProductType: moduleTypeName === "ProductType"
                },
                field: that.model.field.Value.Definition,
                isEditMode: isEditMode,
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
                        dialogBase.resizeToContent();

                        //Handle onClose and onDone Triggered from the field type designer
                        $(designer).on('onFieldCancel', function (event, sender) {
                            that.cancel();
                            dialogBase.resizeToContent();
                        });

                        $(designer).on('onFieldDone', function (event, sender) {

                            var mappings = [
                                { DesignerPropertyName: "WidgetTypeName", PersistedPropertyName: "FieldType" },
                                { DesignerPropertyName: "InstructionalText", PersistedPropertyName: "Description" },
                                { DesignerPropertyName: "FileMaxSize", PersistedPropertyName: "MaxFileSize" },
                                { DesignerPropertyName: "CanSelectMultipleItems", PersistedPropertyName: "AllowMultipleSelection" }
                            ];

                            // set values to current model definitions that are coming from the persisted definitions
                            for (var i = 0; i < mappings.length; i++) {
                                designer.modifiedDataModel.field[mappings[i].PersistedPropertyName] =
                                    designer.modifiedDataModel.field[mappings[i].DesignerPropertyName];
                                delete designer.modifiedDataModel.field[mappings[i].DesignerPropertyName];
                            }

                            //TODO/6: add multi level java script property setter
                            designer.modifiedDataModel.field.ValidatorDefinition.Required = designer.modifiedDataModel.field.IsRequired;

                            that.done(designer.modifiedDataModel.field);
                            dialogBase.resizeToContent();
                        });

                        $(designer).on('onFieldBack', function (event, sender) {
                            that.back();
                            dialogBase.resizeToContent();
                        });
                    }
                });
            });
        },

        cancel: function () {
            if (this.model) {
                this.model.commandName = 'cancel';
                $(this.model.mainFormSelector).show();
                $(this.model.container).hide();
                if (this.model.kendoWindow) {
                    this.model.kendoWindow.data("kendoWindow").close();
                    this.model.kendoWindow.data("kendoWindow").destroy();
                }
            }
        },
        back: function () {
            if (this.model) {
                this.model.commandName = 'back';
                $(this.model.mainFormSelector).show();
                $(this.model.container).hide();
            }
        },
        done: function (field) {

            // this function should format the definitions received from the designer so that they comply with the custom field context
            if (this.model) {
                this.model.commandName = 'saveCustomField';
                $(this.model.mainFormSelector).show();
                $(this.model.container).hide();
            }
            this.model.field.Value.Definition = field;

            // dialog base close extensions need to be called so that when field is edited, the new context replaces 
            // the one that will be send to the client
            if (this.model.kendoWindow) {
                this.model.kendoWindow.data("kendoWindow").close();
                this.model.kendoWindow.data("kendoWindow").destroy();
            } else {
                // called when custom field is created
                var context = this.model.field; dialogBase.close(context);
            }
        },

        validate: function (selector) {
            var isValid = $(selector).val().length > 0;
            var violationMessageElement = $('#dataTypesValidationMirrorFieldViolationMessage');
            if (isValid) {
                violationMessageElement.hide();
            }
            else {
                violationMessageElement.show();
            }
            return isValid;
        },

        prepare: function (context, isEditMode) {
            // this method should prepare the definitions received from the custom field context in a format appropriate for the related media field
            if (!isEditMode) {

                var fieldName = "";
                if (typeof fieldWizardDialog !== "undefined") {
                    fieldName = fieldWizardDialog._fieldName.get_value();
                }

                // Empty definitions that can be serialized to CustomFieldContext
                var emptyPersistedDefinition = {
                    'FieldName': fieldName,
                    'FieldType': '',
                    'Title': '',
                    'WidgetTypeName': '',
                    'FrontendWidgetLabel': '',
                    'FrontendWidgetTypeName': '',
                    'FieldVirtualPath': '',
                    'Example': '',
                    'MediaType': '',
                    'CssClass': '',
                    'DefaultValue': '',
                    'MutuallyExclusive': true,
                    'Choices': '',
                    'DefaultStringValue': '',
                    'DefaultValue': '',
                    'Description': '',
                    'SectionName': '',
                    'ResourceClassId': '',
                    'DefaultImageId': '00000000-0000-0000-0000-000000000000',
                    'ProviderNameForDefaultImage': null,
                    'DefaultItemTypeName': null,
                    'MaxWidth': null,
                    'MaxHeight': null,
                    'MaxFileSize': null,
                    'FileExtensions': "",
                    'DefaultSrc': null,
                    'IsLocalizable': false,
                    'ChoiceItemsTitles': '',
                    'AllowedExtensions': '',
                    'AllowMultipleImages': true,
                    'AllowMultipleVideos': true,
                    'AllowMultipleFiles': true,
                    'AllowMultipleSelection': true,
                    'RelatedDataType': '',
                    'RelatedDataProvider': '',
                    'ValidatorDefinition': {
                        'Required': false,
                        'MinLength': 0,
                        'MaxLength': 0,
                        'DefaultValue': '',
                        'MaxLengthViolationMessage': 0,
                        'MinLengthViolationMessage': 0
                    }
                }

                $.extend(true, emptyPersistedDefinition, context.Value.Definition);

                for (var propertyName in emptyPersistedDefinition) {
                    context.Value.Definition[propertyName] = emptyPersistedDefinition[propertyName];
                }


                // media type is not set, when adding new field, so set it
                context.Value.Definition.MediaType = (context.Value.Definition.MediaType === '') && (this.model.fieldType === 'RelatedMedia') ?
                    $("#typeFieldEditor_mediaDropDown_select").val() : context.Value.Definition.MediaType;
                context.Value.Definition.RelatedDataType = (context.Value.Definition.RelatedDataType === '') && (this.model.fieldType === 'RelatedData') ?
                    this.getModuleType() : context.Value.Definition.RelatedDataType;
                context.Value.Definition.RelatedDataProvider = (context.Value.Definition.RelatedDataProvider === '') && (this.model.fieldType === 'RelatedData') ?
                    $($('#typeFieldEditor_moduleTypesDropDown').find('#dataTypeProviders')).val() : context.Value.Definition.RelatedDataProvider;
                var relatedDataTypeName = null;
               
                if (this.model.fieldType === 'RelatedMedia') {
                    context.Value.Definition.FieldType = this.defaultWidgetTypeNameRelatedMedia;
                   context.Value.Definition.FrontendWidgetTypeName = this.inlineFrontendControl;
                    context.Value.Definition.WidgetTypeName = this.defaultWidgetTypeNameRelatedMedia;
                    context.Value.Definition.RelatedDataProvider = (context.Value.Definition.RelatedDataProvider === '') ?
                    $('#mediaDataProviders').val() : context.Value.Definition.RelatedDataProvider;
                    relatedDataTypeName = $("#typeFieldEditor_mediaDropDown_select option:selected").text().trim();
                }

                if (this.model.fieldType === 'RelatedData') {
                    context.Value.Definition.FieldType = this.defaultWidgetTypeNameRelatedData;
                    context.Value.Definition.FrontendWidgetTypeName = this.inlineFrontendControl;
                    context.Value.Definition.WidgetTypeName = this.defaultWidgetTypeNameRelatedData;
                    relatedDataTypeName = this.dataTypes.dataTypesDropdown.text().replace(/\([^\)]*\)/g, '').trim();
                }

                var fieldLabelText = relatedDataTypeName ? String.format("Related {0}", relatedDataTypeName.toLowerCase()) : context.Value.Definition.FieldName;

                // Select allowed/recommended media extension for OpenGraph image and video
                if (context.Value.Name === "OpenGraphImage") {
                    fieldLabelText = "Image";
                    context.Value.Definition.AllowMultipleImages = false;
                    context.Value.Definition.FileExtensions = ".jpg,.jpeg,.png,.gif";
                }
                else if (context.Value.Name === "OpenGraphVideo") {
                    fieldLabelText = "Video";
                    context.Value.Definition.AllowMultipleVideos = false;
                    context.Value.Definition.FileExtensions = ".mp4";
                }

                if (context.Value.Definition.Title === "") {
                    context.Value.Definition.Title = fieldLabelText;
                }

                if (context.Value.Definition.FrontendWidgetLabel === "" && this.model.fieldType !== 'RelatedMedia') {
                    context.Value.Definition.FrontendWidgetLabel = fieldLabelText;
                }
            }

            // set type specific field values
            var mappings = [
                { DesignerPropertyName: "WidgetTypeName", PersistedPropertyName: "FieldType" },
                { DesignerPropertyName: "InstructionalText", PersistedPropertyName: "Description" },
                { DesignerPropertyName: "FileMaxSize", PersistedPropertyName: "MaxFileSize" },
                { DesignerPropertyName: "CanSelectMultipleItems", PersistedPropertyName: "AllowMultipleSelection" }
            ];

            // set values to current model definitions that are coming from the persisted definitions
            for (var i = 0; i < mappings.length; i++) {
                context.Value.Definition[mappings[i].DesignerPropertyName] =
                    context.Value.Definition[mappings[i].PersistedPropertyName];
            }

            //TODO/6: add multi level java script property setter
            context.Value.Definition.IsRequired = context.Value.Definition.ValidatorDefinition.Required;
        },

        getModuleType: function () {
            return this.dataTypes.dataTypesDropdown.value();
        },

        getModuleTypeName: function () {
        	var ddDataTypeText = null;

        	/* Products module type name */

        	if (this.dataTypes && this.dataTypes.dataTypesDropdown) {
        		ddDataTypeText = this.dataTypes.dataTypesDropdown.value();
        	} else {
                ddDataTypeText = this.model.field.Value.Definition.RelatedDataType;
        	}

        	if (ddDataTypeText && ddDataTypeText.indexOf("Telerik.Sitefinity.DynamicTypes.Model.sf_ec_prdct_") === 0) {
        		return "ProductType";
        	}

        	/* Products module type name */

            var moduleTypeNames = ["Pages", "News", "Blog Posts", "Events"],
                moduleTypes = [
                "Telerik.Sitefinity.Pages.Model.PageNode",
                "Telerik.Sitefinity.News.Model.NewsItem",
                "Telerik.Sitefinity.Blogs.Model.BlogPost",
                "Telerik.Sitefinity.Events.Model.Event"],
                index = null;

            if (this.dataTypes && this.dataTypes.dataTypesDropdown) {
                ddDataTypeText = this.dataTypes.dataTypesDropdown.text();
                index = moduleTypeNames.indexOf(ddDataTypeText);
            }
            else {
                ddDataTypeText = this.model.field.Value.Definition.RelatedDataType;
                index = moduleTypes.indexOf(ddDataTypeText);
            }

            if (index != -1) {
                return moduleTypeNames[index];
            }

            return "DynamicType";
        }
    };

    return (FieldEditor);
});