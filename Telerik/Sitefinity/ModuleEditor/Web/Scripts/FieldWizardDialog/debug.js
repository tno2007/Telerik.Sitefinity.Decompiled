﻿Type.registerNamespace("Telerik.Sitefinity.ModuleEditor.Web.UI");
var fieldWizardDialog = null;

Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog = function (element) {
    Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog.initializeBase(this, [element]);

    this._forbiddenFieldNames = [];
    this._additionalForbiddenFieldNames = ["MetaTitle", "MetaDescription", "OpenGraphTitle", "OpenGraphDescription", "OpenGraphImage", "OpenGraphVideo"];
    this._componentType = null;
    this._fieldTypes = null;
    this._fieldName = null;
    this._hiddenField = null;
    this._multipleChoiceField = null;
    this._clrType = null;
    this._dbTypes = null;
    this._dbLengthField = null;
    this._dbPrecisionField = null;
    this._dbScaleField = null;
    this._emptyValuesField = null;
    this._includeInIndexesField = null;
    this._columnNameField = null;
    this._decimalPlacesField = null;
    this._taxonomySelector = null;
    this._taxonomySelectorBinder = null;
    this._interfaceWidgets = null;
    this._clientLabelManager = null;
    this._customWidgetField = null;
    this._continueLink = null;
    this._cancelLink = null;
    this._moreOptionsSection = null;
    this._moreOptionsExpander = null;
    this._classificationsWrapper = null;
    this._databaseMappings = null;
    this._customFieldTypes = null;

    this._cancelDelegate = null;
    this._continueDelegate = null;
    this._valueChangedDelegate = null;
    this._fieldNameChangedDelegate = null;
    this._multipleChoiceFieldChangedDelegate = null;
    this._moreOptionsExpandDelegate = null;
    this._loadDelegate = null;
    this._interfaceWidgetsValueChangedDelegate = null;
    this._decimalPlacesValueChangedDelegate = null;
    this._onTaxonomySelectorIndexChangedDelegate = null;
    this._siteBaseUrl = null;
    this._taxonomySelectorBinded = false;
    this._emptyField = {
        'Key': '',
        'Value': {
            'Name': '',
            'ContentType': '',
            'FieldTypeKey': '',
            'IsCustom': false,
            'Definition': {
                'FieldName': '',
                'FieldType': '',
                'FieldVirtualPath': '',
                'Title': '',
                'Example': '',
                'CssClass': '',
                'DefaultValue': '',
                'MutuallyExclusive': true,
                'Choices': '',
                'DefaultStringValue': '',
                'DefaultValue': '',
                'SectionName': '',
                'ResourceClassId': '',

                // image field properties
                'DefaultImageId': '',
                'ProviderNameForDefaultImage': '',
                'DefaultItemTypeName': '',
                'MaxWidth': '',
                'MaxHeight': '',
                'MaxFileSize': '',
                'DefaultSrc': '',
                'IsLocalizable': ''
            },
            'DatabaseMapping': {
                'ClrType': '',
                'DbType': '',
                'DbLength': '',
                'DbPrecision': '',
                'DbScale': null,
                'Nullable': '',
                'Indexed': '',
                'ColumnName': ''
            }
        }
    };
}

Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog.callBaseMethod(this, "initialize");

        fieldWizardDialog = this;

        $("#openReservedNamesWindow").click(function () {
            $("#reservedNamesWindow").toggle();
            fieldWizardDialog.resizeToContent();
        });

        this.set_forbiddenFieldNames(this.get_forbiddenFieldNames().concat(this._additionalForbiddenFieldNames));

        if (this._dialog._sfDefaultFields) {
            for (var i = 0; i < this._dialog._sfDefaultFields.length; i++) {
                this.set_forbiddenFieldNames(this.get_forbiddenFieldNames().concat(this._dialog._sfDefaultFields[i].Name));
            }
        }

        $("#reservedNamesListView").kendoListView({
            dataSource: {
                data: this.get_forbiddenFieldNames().sort()
            },
            template: "<li>#:data#</li>"
        });

        if (this._databaseMappings) {
            this._databaseMappings = Sys.Serialization.JavaScriptSerializer.deserialize(this._databaseMappings);
        }
        if (this._customFieldTypes) {
            this._customFieldTypes = Sys.Serialization.JavaScriptSerializer.deserialize(this._customFieldTypes);
        }

        this._loadDelegate = Function.createDelegate(this, this._loadHandler);
        Sys.Application.add_load(this._loadDelegate);

        this._cancelDelegate = Function.createDelegate(this, this._cancelHandler);
        $addHandler(this._cancelLink, "click", this._cancelDelegate);

        this._continueDelegate = Function.createDelegate(this, this._continueHandler);
        $addHandler(this._continueLink, "click", this._continueDelegate);

        this._valueChangedDelegate = Function.createDelegate(this, this._valueChangedHandler);
        $addHandler(this._fieldTypes.get_choiceElement(), "change", this._valueChangedDelegate);

        this._fieldNameChangedDelegate = Function.createDelegate(this, this._fieldNameChangedHandler);
        $addHandler(this._fieldName.get_element(), "input", this._fieldNameChangedDelegate);

        this._moreOptionsExpandDelegate = Function.createDelegate(this, this._moreOptionsExpandHandler);
        $addHandler(this._moreOptionsExpander, "click", this._moreOptionsExpandDelegate);

        this._interfaceWidgetsValueChangedDelegate = Function.createDelegate(this, this._interfaceWidgetsValueChangedHandler);
        $addHandler(this._interfaceWidgets.get_choiceElement(), "change", this._interfaceWidgetsValueChangedDelegate);

        this._decimalPlacesValueChangedDelegate = Function.createDelegate(this, this._decimalPlacesValueChangedHandler);
        $addHandler(this._decimalPlacesField.get_choiceElement(), "change", this._decimalPlacesValueChangedDelegate);

        this._multipleChoiceFieldChangedDelegate = Function.createDelegate(this, this._multipleChoiceFieldChangedHandler);
        $addHandler(this._multipleChoiceField.get_element(), "change", this._multipleChoiceFieldChangedDelegate);

        this._onTaxonomySelectorIndexChangedDelegate = Function.createDelegate(this, this._onTaxonomySelectorIndexChangedHandler);
        this._taxonomySelector.add_selectedIndexChanging(this._onTaxonomySelectorIndexChangedDelegate);

        requirejs.config({
            baseUrl: this._siteBaseUrl + "Res",
            paths: {
                FieldEditor: 'Telerik.Sitefinity.ModuleEditor.Web.Scripts.FieldEditor',
            },
            waitSeconds: 0
        });
        require(["FieldEditor"], function (FieldEditor) {
            fieldWizardDialog.fieldEditor = new FieldEditor();
            fieldWizardDialog.fieldEditor.initialize(fieldWizardDialog._siteBaseUrl);
        });

        // Hide SEO MetaTitle option for Pages
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        fieldWizardDialog._componentType = this._getQueryStringValue(queryString, "componentType");
        if (fieldWizardDialog._componentType === "Telerik.Sitefinity.Pages.Model.PageNode") {
            $('#typeFieldEditor_seoDropDown_select option[value=MetaTitle]').hide();
        }
    },

    dispose: function () {
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }

        if (this._cancelDelegate) {
            if (this._cancelLink) {
                $removeHandler(this._cancelLink, "click", this._cancelDelegate);
            }
            delete this._cancelDelegate;
        }

        if (this._continueDelegate) {
            if (this._continueLink) {
                $removeHandler(this._continueLink, "click", this._continueDelegate);
            }
            delete this._continueDelegate;
        }

        if (this._valueChangedDelegate) {
            if (this._fieldTypes) {
                $removeHandler(this._fieldTypes.get_choiceElement(), "change", this._valueChangedDelegate);
            }
            delete this._valueChangedDelegate;
        }

        if (this._fieldNameChangedDelegate) {
            if (this._fieldTypes) {
                $removeHandler(this._fieldName.get_element(), "input", this._fieldNameChangedDelegate);
            }
            delete this._fieldNameChangedDelegate;
        }

        if (this._multipleChoiceFieldChangedDelegate) {
            if (this._multipleChoiceField) {
                $removeHandler(this._multipleChoiceField.get_element(), "change", this._multipleChoiceFieldChangedDelegate);
            }
            delete this._multipleChoiceFieldChangedDelegate;
        }

        if (this._moreOptionsExpandDelegate) {
            if (this._moreOptionsExpander) {
                $removeHandler(this._moreOptionsExpander, "click", this._moreOptionsExpandDelegate);
            }
            delete this._moreOptionsExpandDelegate;
        }

        if (this._interfaceWidgetsValueChangedDelegate) {
            if (this._interfaceWidgets) {
                $removeHandler(this._interfaceWidgets.get_choiceElement(), "change", this._interfaceWidgetsValueChangedDelegate);
            }
            delete this._interfaceWidgetsValueChangedDelegate;
        }

        if (this._decimalPlacesValueChangedDelegate) {
            if (this._decimalPlacesField) {
                $removeHandler(this._decimalPlacesField.get_choiceElement(), "change", this._decimalPlacesValueChangedDelegate);
            }
            delete this._decimalPlacesValueChangedDelegate;
        }

        if(this._onTaxonomySelectorIndexChangedDelegate) {
            if(this.get_taxonomySelector()) {
                this._taxonomySelector.remove_selectedIndexChanging(this._onTaxonomySelectorIndexChangedDelegate);
            }
            delete this.onTaxonomySelectorIndexChangedDelegate;
        }

        Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog.callBaseMethod(this, "dispose");
    },

    /* -------------------- Public methods ---------------- */

    reset: function () {
        jQuery(this._classificationsWrapper).hide();
        jQuery(this._moreOptionsSection).removeClass("sfExpandedSection");
        this._taxonomySelectorBinded = false;

        this._updateClrType(this._fieldTypes.get_choices()[0].Value);
        this._fieldTypes.reset();
        this._fieldName.reset();
        this._hiddenField.reset();
        this._multipleChoiceField.reset();
        jQuery(this._multipleChoiceField.get_element()).hide();
        this._dbTypes.reset();
        this._dbLengthField.reset();
        //this._emptyValuesField.reset();
        this._emptyValuesField.set_value(true);
        this._includeInIndexesField.reset();
        this._columnNameField.reset();
        // clear decimal places field
        this._decimalPlacesField.reset();
        jQuery(this._decimalPlacesField.get_element()).hide();
        // clear DB precision field
        this._dbPrecisionField.reset();
        jQuery(this._dbPrecisionField.get_element()).hide();
        // clear DB scale field
        this._dbScaleField.reset();
        jQuery(this._dbScaleField.get_element()).hide();
        // clear custom widget field
        this._customWidgetField.reset();
        jQuery(this._customWidgetField.get_element()).hide();

        this._setDbLengthField("ShortText");
        this._bindInterfaceWidgets("ShortText");
        this._setInterfaceWidgetsTitle("InterfaceWidgetForEnteringData");

        jQuery('#typeFieldEditor_mediaDropDown').hide();
        jQuery('#typeFieldEditor_moduleTypesDropDown').hide();
        jQuery('#typeFieldEditor_seoDropDown').hide();
        jQuery('#typeFieldEditor_openGraphDropDown').hide();
        jQuery('#reservedNamesErrorBox').removeClass("sfError").hide();

        this.resizeToContent();
    },

    createDialog: function () {
        this.reset();
    },

    refreshUI: function () {
        jQuery('#reservedNamesErrorBox').removeClass("sfError").hide();

        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var queryStringValue = null;

        var fieldTypeName = this._getQueryStringValue(queryString, "fieldTypeName");
        this._fieldTypes.selectListItemsByValue(fieldTypeName);

        queryStringValue = this._getQueryStringValue(queryString, "fieldName");
        this._fieldName.set_value(queryStringValue);

        queryStringValue = this._getQueryStringValue(queryString, "hidden") == "true" ? true : false;
        this._hiddenField.selectListItemsByValue(queryStringValue);

        queryStringValue = this._getQueryStringValue(queryString, "multiple") == "true" ? true : false;
        this._multipleChoiceField.selectListItemsByValue(queryStringValue);

        queryStringValue = this._getQueryStringValue(queryString, "widgetTypeName");
        this._bindInterfaceWidgets(fieldTypeName);
        var item = this._interfaceWidgets._get_listItemByValue(queryStringValue);
        if (item.length) {
            this._interfaceWidgets.selectListItemsByValue(queryStringValue);
            jQuery(this._customWidgetField.get_element()).hide();
        }
        else {
            this._interfaceWidgets.selectListItemsByValue("custom");
            this._customWidgetField.set_value(queryStringValue);
        }

        queryStringValue = this._getQueryStringValue(queryString, "clrType");
        if (queryStringValue) {
            this._clrType = queryStringValue;
        } else {
            this._updateClrType(fieldTypeName);
        }

        this._populateDbType(this._fieldTypes.get_value());
        queryStringValue = this._getQueryStringValue(queryString, "dbType");
        this._dbTypes._get_listItemByText(queryStringValue).attr("selected", true);

        queryStringValue = this._getQueryStringValue(queryString, "dbLength");
        if (queryStringValue) {
            this._dbLengthField.set_value(queryStringValue);
        }
        else {
            jQuery(this._dbLengthField.get_element()).hide();
        }

        queryStringValue = this._getQueryStringValue(queryString, "allowEmptyValues") == "true" ? true : false;
        this._emptyValuesField.selectListItemsByValue(queryStringValue);

        queryStringValue = this._getQueryStringValue(queryString, "includeInIndexes") == "true" ? true : false;
        this._includeInIndexesField.selectListItemsByValue(queryStringValue);

        queryStringValue = this._getQueryStringValue(queryString, "columnName");
        this._columnNameField.set_value(queryStringValue);

        jQuery(this._dbPrecisionField.get_element()).hide();
        jQuery(this._dbScaleField.get_element()).hide();

        if (fieldTypeName === "Number") {
            queryStringValue = this._getQueryStringValue(queryString, "dbScale");
            this._dbScaleField.set_value(queryStringValue);
        }
        else {
            jQuery(this._decimalPlacesField.get_element()).hide();            
        }

        queryStringValue = this._getQueryStringValue(queryString, "taxonomyId");
        if (queryStringValue) {
            jQuery(this._classificationsWrapper).show();
            this._taxonomySelectorBinder.DataBind();
            this._taxonomySelectorBinded = true;
            this._selectItemByValue(this._taxonomySelector, queryStringValue);
            this._setInterfaceWidgetsTitle("InterfaceWidgetForSelectingClassificationItems");
        }

        if (this._fieldTypes.get_value() == 'Classification' || this._fieldTypes.get_value() == 'Seo' || this._fieldTypes.get_value() == 'OpenGraph') {
            jQuery(this._fieldName.get_textElement()).prop('disabled', true);
        }

        if (fieldTypeName && !(fieldTypeName.toLowerCase() === 'relateddata' || fieldTypeName.toLowerCase() === 'relatedmedia'))
        {
            jQuery('#typeFieldEditor_mediaDropDown').hide();
            jQuery('#typeFieldEditor_moduleTypesDropDown').hide();
        }

        if (fieldTypeName && !(fieldTypeName.toLowerCase() === 'seo' || fieldTypeName.toLowerCase() === 'opengraph')) {
            jQuery('#typeFieldEditor_seoDropDown').hide();
            jQuery('#typeFieldEditor_openGraphDropDown').hide();
        }

        // Prepare view for SEO and OpenGraph fields on "Back"
        if (this._fieldName.get_value() === "MetaTitle" || this._fieldName.get_value() === "MetaDescription" || this._fieldName.get_value() === "OpenGraphTitle" || this._fieldName.get_value() === "OpenGraphDescription" || this._fieldName.get_value() === "OpenGraphImage" || this._fieldName.get_value() === "OpenGraphVideo") {
            jQuery(this._fieldName.get_textElement()).prop('disabled', true);

            this._interfaceWidgets.set_visible(false);
            this._hiddenField.set_visible(false);
            jQuery(this._moreOptionsSection).hide();
        }

        if ((this._fieldTypes.get_value() === 'ShortText' || this._fieldTypes.get_value() === 'LongText') && (this._fieldName.get_value() === "MetaTitle" || this._fieldName.get_value() === "MetaDescription")) {
            jQuery('#typeFieldEditor_seoDropDown').show();
            $('#typeFieldEditor_seoDropDown_select option[value=' + this._fieldName.get_value() + ']').attr("selected", "selected");
            $('#typeFieldEditor_seoDropDown_select').change(function () {
                var selectedText = $('#typeFieldEditor_seoDropDown_select option:selected').val();
                selectedText = selectedText.replace(/[ -]/g, "");
                fieldWizardDialog.get_fieldName().set_value(selectedText);
            });

            this._fieldTypes.set_value("Seo");
        }

        var isOpenGraphText = (this._fieldTypes.get_value() === 'ShortText' || this._fieldTypes.get_value() === 'LongText') && (this._fieldName.get_value() === "OpenGraphTitle" || this._fieldName.get_value() === "OpenGraphDescription");
        var isOpenGraphRelated = this._fieldTypes.get_value() === 'RelatedMedia' && (this._fieldName.get_value() === "OpenGraphImage" || this._fieldName.get_value() === "OpenGraphVideo");
        if (isOpenGraphText || isOpenGraphRelated) {
            jQuery('#typeFieldEditor_openGraphDropDown').show();
            $('#typeFieldEditor_openGraphDropDown_select option[value=' + this._fieldName.get_value() + ']').attr("selected", "selected");
            $('#typeFieldEditor_openGraphDropDown_select').change(function () {
                var selectedText = $('#typeFieldEditor_openGraphDropDown_select option:selected').val();
                selectedText = selectedText.replace(/[ -]/g, ""); 
                fieldWizardDialog.get_fieldName().set_value(selectedText);

                if (selectedText === "OpenGraphImage" || selectedText === "OpenGraphVideo") {
                    // preaprae data sources
                    fieldWizardDialog.prepareRelatedMediaSources(true);
                }
                else {
                    jQuery('#typeFieldEditor_mediaDropDown').hide();
                    fieldWizardDialog.resizeToContent();
                }
            });

            this._fieldTypes.set_value("OpenGraph");
        }

        this._multipleChoiceField.set_visible(false);

        if (this._fieldTypes.get_value() == 'Choices') {
            this._includeInIndexesField.set_visible(false);
            this._hiddenField.set_visible(false);
            this._multipleChoiceField.set_visible(true);
        }
    },

    openSelectedFieldContainer: function (context) {
        var isNotOpenGraphMedia = true;
        var fieldTypeName = this.get_fieldTypes().get_value();

        if (fieldTypeName === "OpenGraph") {
            fieldTypeName = "RelatedMedia";
            isNotOpenGraphMedia = false;
        }

        var model = {
            field: context,
            mainFormSelector: "div.sfFormStepOne",
            container: "#editorSelectedFieldContainer",
            fieldType: fieldTypeName,
            isNotOpenGraphMedia: isNotOpenGraphMedia
        };
        
        // open field editor on CREATE of custom field
        isValid = ((fieldTypeName === "RelatedData") && fieldWizardDialog.fieldEditor.validate("#dataTypesValidationMirrorField")) || (fieldTypeName === "RelatedMedia");
        if (isValid) {
            fieldWizardDialog.fieldEditor.open(model, false);
        }
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

    /* -------------------- Event handlers ---------------- */

    _loadHandler: function (sender, args) {
        this.refreshUI();
    },

    _cancelHandler: function (sender, args) {
        this.close();
    },

    _getExtendedContext: function () {
        var fieldName = this._fieldName.get_value();

        var contentType = this._fieldTypes.get_selectedName();
        var fieldTypeName = this._fieldTypes.get_value();
        var dbType = this._dbTypes.get_value();

        var widgetTypeName = this._interfaceWidgets.get_value();
        if (widgetTypeName == "custom") {
            widgetTypeName = this._customWidgetField.get_value();
        }

        if (fieldTypeName === "Seo" || fieldTypeName === "OpenGraph") {
            if (fieldTypeName === "OpenGraph" && (fieldName === "OpenGraphImage" || fieldName === "OpenGraphVideo")) {
                fieldTypeName = "RelatedMedia";
                widgetTypeName = "Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField";
                this._clrType = "Telerik.Sitefinity.RelatedData.RelatedItems";

                if (fieldName === "OpenGraphImage") {
                    $('#typeFieldEditor_mediaDropDown_select option[value="image"]').attr("selected", "selected");
                }

                if (fieldName === "OpenGraphVideo") {
                    $('#typeFieldEditor_mediaDropDown_select option[value="video"]').attr("selected", "selected");
                }
            }
            else {
                if (fieldName === "MetaTitle" || fieldName === "OpenGraphTitle") {
                    fieldTypeName = "ShortText";
                    widgetTypeName = "Telerik.Sitefinity.Web.UI.Fields.TextField";
                }
                else if (fieldName === "MetaDescription" || fieldName === "OpenGraphDescription") {
                    fieldTypeName = "LongText";
                    dbType = "VARCHAR";
                    widgetTypeName = "Telerik.Sitefinity.Web.UI.Fields.TextField";
                }
            }
        }

        var renderChoiceAs;
        if (fieldTypeName == "Choices") {
            var choiceElement = this._interfaceWidgets._choiceElement;
            renderChoiceAs = choiceElement.options[choiceElement.selectedIndex].innerText;
            switch (renderChoiceAs) {
                case "Radio buttons":
                    renderChoiceAs = "RadioButtons";
                    break;
                case "Dropdown list":
                    renderChoiceAs = "DropDownList";
                    break;
                case "Checkboxes":
                    renderChoiceAs = "Checkboxes";
                    break;
                default:
                    renderChoiceAs = "";
                    break;
            }
        }

        return {
            'Key': fieldName,
            'Value': {
                'Name': fieldName,
                'ContentType': contentType,
                'FieldTypeKey': fieldTypeName,
                'IsCustom': true,
                'Definition': {
                    'FieldType': widgetTypeName,
                    'TaxonomyId': (fieldTypeName === "Classification") ? this._taxonomySelectorBinder.get_selectedItem().Id : null,
                    'Hidden': Boolean.parse(this._hiddenField.get_value()),
                    'RenderChoiceAs': renderChoiceAs
                },
                'DatabaseMapping': {
                    'ClrType': this._clrType,
                    'DbType': dbType,
                    'DbLength': (fieldTypeName === "ShortText") ? this._dbLengthField.get_value() : null,
                    'DbPrecision': (fieldTypeName === "Number") ? this._dbPrecisionField.get_value() : null,
                    'DbScale': (fieldTypeName === "Number") ? this._dbScaleField.get_value() : null,
                    'Nullable': this._emptyValuesField.get_value(),
                    'Indexed': this._includeInIndexesField.get_value(),
                    'ColumnName': this._columnNameField.get_value()
                }
                //'Hidden': Boolean.parse(this._hiddenField.get_value())
            }
        };
    },

    _continueHandler: function (sender, args) {

        // clear reserved field names validatin error
        jQuery('#reservedNamesErrorBox').removeClass("sfError").hide();

        var context = this._getExtendedContext();
        var field = context.Value;

        if (this._fieldName.validate()) {
            var fieldTypeLowerCase = fieldWizardDialog.get_fieldTypes().get_value().toLowerCase();

            // validate reserved field names
            var fieldNameValue = field.Name.toLowerCase().trim();
            var forbiddenFieldNames = fieldWizardDialog.get_forbiddenFieldNames();
            for (var i = 0; i < forbiddenFieldNames.length; i++) {
                if (fieldNameValue == forbiddenFieldNames[i].toLowerCase()) {
                    switch (fieldNameValue) {
                        case "metatitle":
                        case "metadescription":
                            if (fieldTypeLowerCase === "seo") {
                                break;
                            }
                        case "opengraphtitle":
                        case "opengraphdescription":
                        case "opengraphimage":
                        case "opengraphvideo":
                            if (fieldTypeLowerCase === "opengraph") {
                                break;
                            }
                        default:
                            // show reserved field names validatin error
                            var reservedFieldNameErrorMessage = kendo.format(this._clientLabelManager.getLabel("ModuleEditorResources", "ReservedFieldNameErrorMessage"), field.Name);
                            $("#reservedFieldNameErrorMessage").html(reservedFieldNameErrorMessage);
                            jQuery('#reservedNamesErrorBox').addClass("sfError").show();
                            fieldWizardDialog.resizeToContent();
                            return;
                    }
                }
            }            

            if (fieldTypeLowerCase === "opengraph" && (field.Name === "OpenGraphImage" || field.Name === "OpenGraphVideo")) {
                fieldTypeLowerCase = 'RelatedMedia'.toLowerCase();
            }

            if (fieldTypeLowerCase === 'RelatedData'.toLowerCase() || fieldTypeLowerCase === 'RelatedMedia'.toLowerCase()) {
                fieldWizardDialog.openSelectedFieldContainer(context);
                return;
            }

            if (fieldTypeLowerCase === 'Choices'.toLowerCase()) {
                var fieldDefinition = field.Definition;
                if (fieldDefinition)
                    fieldDefinition.AllowMultipleSelection = Boolean.parse(this._multipleChoiceField.get_value());
            }

            var exapmle;
            var fieldTitle;
            var recommendedCharacters;
            var maxRange;
            switch (field.Name) {
                case "MetaTitle":
                    fieldTitle = "Title for search engines";
                    exapmle = "Displayed in browser title bar and in search results. Less than 70 characters are recommended.";
                    recommendedCharacters = 70;
                    break;
                case "MetaDescription":
                    fieldTitle = "Description";
                    exapmle = "Less than 150 characters are recommended.";
                    recommendedCharacters = 150;
                    maxRange = 255;
                    break;
                case "OpenGraphTitle":
                    fieldTitle = "Title for social media";
                    exapmle = "Less than 90 characters are recommended.";
                    recommendedCharacters = 90;
                    break;
                case "OpenGraphDescription":
                    fieldTitle = "Description";
                    exapmle = "Less than 200 characters are recommended.";
                    recommendedCharacters = 200;
                    maxRange = 255;
                    break;
                default:
                    exapmle = null;
                    fieldTitle = null;
                    recommendedCharacters = null;
                    break;
            }

            if (field.Definition.Hidden) {
                if (!field.Definition.FieldName) {
                    field.Definition.FieldName = field.Name;
                }
                dialogBase.close(context);
            }
            else {

                var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                var componentType = this._getQueryStringValue(queryString, "componentType");
                var itemsName = this._getQueryStringValue(queryString, "itemsName");
                var url = String.format(
                "CustomFieldPropertyEditor?fieldTypeName={0}&fieldName={1}&hidden={2}&widgetTypeName={3}&dbType={4}&dbSqlType={5}&dbLength={6}&allowEmptyValues={7}&includeInIndexes={8}&columnName={9}&dbPrecision={10}&dbScale={11}&taxonomyId={12}&clrType={13}&componentType={14}&command={15}&contentType={16}&itemsName={17}&example={18}&fieldTitle={19}&recommendedCharacters={20}&renderChoiceAs={21}&multiple={22}&maxLength={23}",
                field.FieldTypeKey,
                field.Name,
                field.Hidden,
                field.Definition.FieldType,
                field.DatabaseMapping.DbType,
                field.DatabaseMapping.DbSqlType,
                field.DatabaseMapping.DbLength,
                field.DatabaseMapping.Nullable,
                field.DatabaseMapping.Indexed,
                field.DatabaseMapping.ColumnName,
                field.DatabaseMapping.DbPrecision,
                field.DatabaseMapping.DbScale,
                field.Definition.TaxonomyId,
                encodeURIComponent(field.DatabaseMapping.ClrType),
                componentType,
                "createCustomField",
                field.ContentType,
                itemsName,
                exapmle,
                fieldTitle,
                recommendedCharacters,
                field.Definition.RenderChoiceAs,
                field.Definition.AllowMultipleSelection,
                maxRange);
                window.location.replace(url);
            }
        }
    },

    _fieldNameChangedHandler: function (sender, args) {
        var value = sender.target.value;
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
                jQuery(this._fieldName.get_textElement()).after(errorMessage);
            }
        } else {
            $("#dbMessage").remove();
        }
    },

    _valueChangedHandler: function (sender, args) {
        var selectedValue = sender.target.value;
        this._interfaceWidgets.set_visible(true);
        jQuery('#typeFieldEditor_mediaDropDown').hide();
        jQuery('#typeFieldEditor_moduleTypesDropDown').hide();
        jQuery('#typeFieldEditor_seoDropDown').hide();
        jQuery('#typeFieldEditor_openGraphDropDown').hide();
        jQuery('#reservedNamesErrorBox').removeClass("sfError").hide();
        // interface widgets settings
        this._setInterfaceWidgetsTitle("InterfaceWidgetForEnteringData");
        this._bindInterfaceWidgets(selectedValue);
        jQuery(this._customWidgetField.get_element()).hide();
        this._customWidgetField.reset();
        // set the CLR type
        this._updateClrType(selectedValue);
        // set DB type
        this._populateDbType(selectedValue);
        jQuery(this._fieldName.get_textElement()).prop('disabled', false);
        this._hiddenField.set_value(false);
        this._hiddenField.set_visible(true);
        this._multipleChoiceField.set_value(false);
        this._multipleChoiceField.set_visible(false);
        switch (selectedValue) {
            case "ShortText":
                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();

                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                this._setDbLengthField(selectedValue);
                jQuery(this._moreOptionsSection).show();
                break;
            case "Choices":
                this._multipleChoiceField.set_visible(true);
                jQuery(this._hiddenField.get_element()).hide();
                jQuery(this._includeInIndexesField.get_element()).hide();
                this._emptyValuesField.set_value(false);
                
                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();
                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._dbLengthField.get_element()).hide();
                jQuery(this._moreOptionsSection).show();
                break;
            case "Number":
                jQuery(this._decimalPlacesField.get_element()).show();
                jQuery(this._classificationsWrapper).hide();

                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._dbLengthField.get_element()).hide();
                jQuery(this._moreOptionsSection).show();
                break;
            case "Classification":
                if (!this._taxonomySelectorBinded) {
                    this._taxonomySelectorBinder.DataBind();
                    this._taxonomySelectorBinded = true;
                }

                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).show();
                jQuery(this._fieldName.get_textElement()).prop('disabled', true);
                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._dbLengthField.get_element()).hide();
                jQuery(this._moreOptionsSection).show();

                this._setInterfaceWidgetsTitle("InterfaceWidgetForSelectingClassificationItems");
                break;
            case "Image":
                jQuery(this._moreOptionsSection).hide();
                break;
            case "RelatedMedia":
                this._interfaceWidgets.set_visible(false);
                this._hiddenField.set_visible(false);

                //Show related media providers drop down
                fieldWizardDialog.prepareRelatedMediaSources();

                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();
                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._moreOptionsSection).hide();
                break;
            case "RelatedData":
                this._interfaceWidgets.set_visible(false);
                this._hiddenField.set_visible(false);

                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();
                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._moreOptionsSection).hide();

                jQuery('#typeFieldEditor_moduleTypesDropDown').show();
                break;
            case "Seo":
                this._interfaceWidgets.set_visible(false);
                this._hiddenField.set_visible(false);
                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();
                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._moreOptionsSection).hide();

                jQuery('#typeFieldEditor_seoDropDown').show();

                if (fieldWizardDialog._componentType === "Telerik.Sitefinity.Pages.Model.PageNode") {
                    $('#typeFieldEditor_seoDropDown_select option:last-child').attr("selected", "selected");
                }
                else {
                    $('#typeFieldEditor_seoDropDown_select option:first-child').attr("selected", "selected");
                }

                $('#typeFieldEditor_seoDropDown_select').change(function () {
                    var selectedText = $('#typeFieldEditor_seoDropDown_select option:selected').val();
                    selectedText = selectedText.replace(/[ -]/g, "");
                    fieldWizardDialog.get_fieldName().set_value(selectedText);
                });

                var selectedText = $('#typeFieldEditor_seoDropDown_select option:selected').val();
                selectedText = selectedText.replace(/[ -]/g, "");
                this._fieldName.set_value(selectedText);
                jQuery(this._fieldName.get_textElement()).prop('disabled', true);
                
                break;
            case "OpenGraph":
                this._interfaceWidgets.set_visible(false);
                this._hiddenField.set_visible(false);
                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();
                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._moreOptionsSection).hide();

                jQuery('#typeFieldEditor_openGraphDropDown').show();
                $('#typeFieldEditor_openGraphDropDown_select option:first-child').attr("selected", "selected");
                $('#typeFieldEditor_openGraphDropDown_select').change(function () {
                    var selectedText = $('#typeFieldEditor_openGraphDropDown_select option:selected').val();
                    selectedText = selectedText.replace(/[ -]/g, "");
                    fieldWizardDialog.get_fieldName().set_value(selectedText);

                    if (selectedText === "OpenGraphImage" || selectedText === "OpenGraphVideo") {
                        // preaprae data sources
                        fieldWizardDialog.prepareRelatedMediaSources(true);
                    }
                    else {
                        jQuery('#typeFieldEditor_mediaDropDown').hide();
                        fieldWizardDialog.resizeToContent();
                    }
                });
                var selectedText = $('#typeFieldEditor_openGraphDropDown_select option:selected').val();
                selectedText = selectedText.replace(/[ -]/g, "");
                this._fieldName.set_value(selectedText);
                jQuery(this._fieldName.get_textElement()).prop('disabled', true);
                
                break;
            default:
                jQuery(this._decimalPlacesField.get_element()).hide();
                jQuery(this._classificationsWrapper).hide();

                jQuery(this._dbPrecisionField.get_element()).hide();
                jQuery(this._dbScaleField.get_element()).hide();
                jQuery(this._dbLengthField.get_element()).hide();
                jQuery(this._moreOptionsSection).show();
                break;
        }
        this.resizeToContent();
    },

    _moreOptionsExpandHandler: function (e) {
        jQuery(this._moreOptionsSection).toggleClass("sfExpandedSection");
        this.resizeToContent();
    },

    _interfaceWidgetsValueChangedHandler: function (sender, args) {
        var selectedValue = sender.target.value;
        if (selectedValue == "custom") {
            jQuery(this._customWidgetField.get_element()).show();
        }
        else {
            jQuery(this._customWidgetField.get_element()).hide();
            this._customWidgetField.reset();
        }
        this.resizeToContent();
    },

    _multipleChoiceFieldChangedHandler: function (sender, args) {
        var fieldTypeName = this._fieldTypes.get_value()
        this._bindInterfaceWidgets(fieldTypeName);
    },

    _decimalPlacesValueChangedHandler: function (sender, args) {
        var selectedValue = sender.target.value;
        this._dbScaleField.set_value(selectedValue);
    },

    _onTaxonomySelectorIndexChangedHandler: function (sender, args) {
        var selectedText = args._item._properties._data.value.Name;
        selectedText = selectedText.replace(/[ -]/g, "");
        if (selectedText === 'Categories')
            this._fieldName.set_value("Category");
        else
            this._fieldName.set_value(selectedText);
    },

    prepareRelatedMediaSources: function (hideMediaTypeDropdown) {
        hideMediaTypeDropdown = hideMediaTypeDropdown || false;
        jQuery('#typeFieldEditor_mediaDropDown').show();

        if (hideMediaTypeDropdown === true) {
            jQuery('#typeFieldEditor_mediaDropDown').find("h2").first().hide();
            jQuery('#typeFieldEditor_mediaDropDown').find(".sfDropdownList").first().hide();

            fieldWizardDialog.resizeToContent();
        }
        else {
            jQuery('#typeFieldEditor_mediaDropDown').find("h2").first().show();
            jQuery('#typeFieldEditor_mediaDropDown').find(".sfDropdownList").first().show();
            $('#typeFieldEditor_mediaDropDown_select option:first-child').attr("selected", "selected");
        }
    },

    /* -------------------- Private methods ---------------- */

    _findItemByKey: function (dictionary, key) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key)
                return item.Value;
        }
        return null;
    },

    _setDbLengthField: function (key) {
        var item = this._findItemByKey(this._databaseMappings, key);
        if (item) {
            jQuery(this._dbLengthField.get_element()).show();
            this._dbLengthField.set_value(item.DbLength);
        }
    },

    _updateClrType: function (key) {
        var item = this._findItemByKey(this._databaseMappings, key);
        if (item) {
            this._clrType = item.ClrType;
        }
    },

    _bindInterfaceWidgets: function (key) {
        this._interfaceWidgets.clearListItems();

        var controls = this._findItemByKey(this._customFieldTypes, key);
        if (controls) {
            for (var i = 0; i < controls.length; i++) {
                var ctrl = controls[i];
                var text = this._clientLabelManager.getLabel(ctrl.ResourceClassId, ctrl.Title);
                var value = ctrl.FieldTypeOrPath;

                if (key === "Choices") {
                    var isMultiple = Boolean.parse(this._multipleChoiceField.get_value());

                    if (isMultiple && text !== "Checkboxes") {
                        continue;
                    }

                    if (!isMultiple && text === "Checkboxes") {
                        continue;
                    }
                }
                this._interfaceWidgets.addListItem(value, text);
            }

            var text = "--------------------";
            var value = "";
            this._interfaceWidgets.addListItem(value, text)

            text = this._clientLabelManager.getLabel("ModuleEditorResources", "CustomDotDotDot");
            value = "custom";
            this._interfaceWidgets.addListItem(value, text)
        }
    },

    _getQueryStringValue: function (queryString, key) {
        if (queryString.contains(key)) {
            return queryString.get(key);
        }
        return null;
    },

    _selectItemByValue: function (comboBox, value) {
        var items = comboBox.get_items();
        for (var i = 0; i <= items.get_count(); i++) {
            var item = items.getItem(i);
            if (item && item.get_value().Type == value) {
                comboBox.set_selectedIndex(i);
                return;
            }
        }
    },

    _setInterfaceWidgetsTitle: function (key) {
        var title = this._clientLabelManager.getLabel("ModuleEditorResources", key);
        this._interfaceWidgets.set_title(title);
    },

    _populateDbType: function (selectedValue) {
        var selectedValueMappings = this._findItemByKey(this._databaseMappings, selectedValue);
        this._dbTypes.clearListItems();
        if (selectedValueMappings) {
            this._dbTypes.addListItem(selectedValueMappings.DbType, selectedValueMappings.DbType);
            if (selectedValueMappings.AdditionalDbTypeChoices) {
                var additionalChoices = selectedValueMappings.AdditionalDbTypeChoices.split(',');
                for (var i = 0; i < additionalChoices.length; i++) {
                    this._dbTypes.addListItem(additionalChoices[i], additionalChoices[i]);
                }
            }
            this._dbTypes.selectListItemsByValue(selectedValueMappings.DbType);
        }
    },

    /* -------------------- properties ---------------- */

    get_forbiddenFieldNames: function () {
        return this._forbiddenFieldNames;
    },

    set_forbiddenFieldNames: function (value) {
        this._forbiddenFieldNames = value;
    },

    get_fieldTypes: function () {
        return this._fieldTypes;
    },
    set_fieldTypes: function (value) {
        this._fieldTypes = value;
    },

    get_fieldName: function () {
        return this._fieldName;
    },
    set_fieldName: function (value) {
        this._fieldName = value;
    },

    get_hiddenField: function () {
        return this._hiddenField;
    },
    set_hiddenField: function (value) {
        this._hiddenField = value;
    },

    get_multipleChoiceField: function () {
        return this._multipleChoiceField;
    },
    set_multipleChoiceField: function (value) {
        this._multipleChoiceField = value;
    },

    get_continueLink: function () {
        return this._continueLink;
    },
    set_continueLink: function (value) {
        this._continueLink = value;
    },

    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },

    get_moreOptionsSection: function () {
        return this._moreOptionsSection;
    },
    set_moreOptionsSection: function (value) {
        this._moreOptionsSection = value;
    },

    get_moreOptionsExpander: function () {
        return this._moreOptionsExpander;
    },
    set_moreOptionsExpander: function (value) {
        this._moreOptionsExpander = value;
    },

    get_classificationsWrapper: function () {
        return this._classificationsWrapper;
    },
    set_classificationsWrapper: function (value) {
        this._classificationsWrapper = value;
    },

    get_dbTypes: function () {
        return this._dbTypes;
    },
    set_dbTypes: function (value) {
        this._dbTypes = value;
    },

    get_dbLengthField: function () {
        return this._dbLengthField;
    },
    set_dbLengthField: function (value) {
        this._dbLengthField = value;
    },

    get_dbPrecisionField: function () {
        return this._dbPrecisionField;
    },
    set_dbPrecisionField: function (value) {
        this._dbPrecisionField = value;
    },

    get_dbScaleField: function () {
        return this._dbScaleField;
    },
    set_dbScaleField: function (value) {
        this._dbScaleField = value;
    },

    get_emptyValuesField: function () {
        return this._emptyValuesField;
    },
    set_emptyValuesField: function (value) {
        this._emptyValuesField = value;
    },

    get_includeInIndexesField: function () {
        return this._includeInIndexesField;
    },
    set_includeInIndexesField: function (value) {
        this._includeInIndexesField = value;
    },

    get_columnNameField: function () {
        return this._columnNameField;
    },
    set_columnNameField: function (value) {
        this._columnNameField = value;
    },

    get_databaseMappings: function () {
        return this._databaseMappings;
    },
    set_databaseMappings: function (value) {
        this._databaseMappings = value;
    },

    get_decimalPlacesField: function () {
        return this._decimalPlacesField;
    },
    set_decimalPlacesField: function (value) {
        this._decimalPlacesField = value;
    },

    get_taxonomySelector: function () {
        return this._taxonomySelector;
    },
    set_taxonomySelector: function (value) {
        this._taxonomySelector = value;
    },

    get_taxonomySelectorBinder: function () {
        return this._taxonomySelectorBinder;
    },
    set_taxonomySelectorBinder: function (value) {
        this._taxonomySelectorBinder = value;
    },

    get_interfaceWidgets: function () {
        return this._interfaceWidgets;
    },
    set_interfaceWidgets: function (value) {
        this._interfaceWidgets = value;
    },

    get_customFieldTypes: function () {
        return this._customFieldTypes;
    },
    set_customFieldTypes: function (value) {
        this._customFieldTypes = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_customWidgetField: function () {
        return this._customWidgetField;
    },
    set_customWidgetField: function (value) {
        this._customWidgetField = value;
    }
};

Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog.registerClass("Telerik.Sitefinity.ModuleEditor.Web.UI.FieldWizardDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);

