Type.registerNamespace("Telerik.Sitefinity.ModuleEditor.Web.UI");

Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor = function (element) {
    this._backLink = null;
    this._definition = null;
    this._databaseMapping = null;
    this._backDelegate = null;
    this._defaultValueValidationMessage = null;
    this._onLoadDelegate = null;
    this._clientLabelManager = null;

    Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor.initializeBase(this, [element]);
}

Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor.prototype = {

    initialize: function () {
        Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor.callBaseMethod(this, "initialize");

        if (this._definition) {
            this._definition = Sys.Serialization.JavaScriptSerializer.deserialize(this._definition);
            this._normalizeDefinition();
        }

        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var command = this._getQueryStringValue(queryString, "command");
        var fieldName = this._getQueryStringValue(queryString, "fieldName");

        if (fieldName === "MetaTitle" || fieldName === "MetaDescription" || fieldName === "OpenGraphTitle" || fieldName === "OpenGraphDescription") {
            this._definition.FieldName = fieldName;

            if (command === "createCustomField") {
                this._definition.Title = this._getQueryStringValue(queryString, "fieldTitle");
                this._definition.Example = this._getQueryStringValue(queryString, "example");
                if (this._definition.ValidatorDefinition === null)
                    this._definition.ValidatorDefinition = {};

                this._definition.ValidatorDefinition.RecommendedCharactersCount = this._getQueryStringValue(queryString, "recommendedCharacters");
                this._definition.ValidatorDefinition.MaxLength = this._getQueryStringValue(queryString, "maxLength");
            }
        }

        this._backDelegate = Function.createDelegate(this, this._backHandler);
        $addHandler(this._backLink, "click", this._backDelegate);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        if (this._backDelegate) {
            if (this._backLink) {
                $removeHandler(this._backLink, "click", this._backDelegate);
            }
            delete this._backDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor.callBaseMethod(this, "dispose");
    },

    /* -------------------- Public methods ---------------- */
    /* ------------------ Events --------------*/
    _onLoad: function () {
        if (($telerik.isSafari || $telerik.isChrome) && !dialogBase._dialog.isMaximized())
            jQuery("body").addClass("sfOverflowHiddenX");

        var fieldName = this.get_definition().FieldName;
        if (fieldName === "MetaTitle" || fieldName === "MetaDescription" || fieldName === "OpenGraphTitle" || fieldName === "OpenGraphDescription") {
            if (this.get_clientLabelManager()) {
                var appearanceTitle = this.get_clientLabelManager().getLabel("FormsResources", "Appearance");
                var appearanceTab = this._designer._menuTabStrip.findTabByText(appearanceTitle);
                if (appearanceTab) {
                    jQuery(appearanceTab.get_element()).hide();
                }
            }
        }
    },
    /* -------------------- Private methods ---------------- */
    // overridden method
    _saveChanges: function (e) {
        var eventArgs = new Sys.CancelEventArgs();
        this._raiseBeforeSaveChanges(eventArgs);

        if (eventArgs.get_cancel()) {
            e.preventDefault();
            return;
        }

        this._designer.applyChanges();
        this._buildDefinitionProperties();

        if (!this._validate(e)) {
            return;
        }
        var context = this._buildContext();
        dialogBase.close(context);
    },
    _buildDefinitionProperties: function () {
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        this._definition = this.setDefinitionByFieldType(queryString);
        var taxonomyId = this._definition.TaxonomyId;
        if (!this._definition.TaxonomyId)
            this._definition.TaxonomyId = taxonomyId;
    },
    _buildContext: function () {
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var hidden = this._getQueryStringValue(queryString, "Hidden");
        if (hidden == null) {
            hidden = false;
        }
        var fieldName = this._getQueryStringValue(queryString, "FieldName");
        var isCustom = (this._getQueryStringValue(queryString, "isCustom") == null) ? true : this._getQueryStringValue(queryString, "isCustom");
        var context =
        {
            'Key': fieldName,
            'Value': {
                'Name': fieldName,
                'ContentType': this._getQueryStringValue(queryString, "contentType"),
                'FieldTypeKey': this._getQueryStringValue(queryString, "fieldTypeName"),
                'IsCustom': isCustom,
                'Definition': this._definition,
                'DatabaseMapping': this.get_databaseMapping()
                //'Hidden': hidden
            }
        };
        return context;
    },
    _validate: function (e) {
        var maxLength = this._definition.ValidatorDefinition.MaxLength;
        var defaultValue = this._definition.DefaultStringValue;
        if (!this._validateDefaultValue(maxLength, defaultValue)) {
            if (!e) var e = window.event;

            if (e.stopPropagation) {
                e.stopPropagation();
            }
            else {
                e.cancelBubble = true;
            }
            if (e.preventDefault) {
                e.preventDefault();
            }
            else {
                e.returnValue = false;
            }

            alert("The number of symbols at default value exceed max specified limitation length.");
            return false;
        }
        return true;
    },
    _validateDefaultValue: function (maxLength, defaultValue) {
        if (defaultValue && maxLength && 0 < maxLength && maxLength < defaultValue.length) {
            return false;
        }

        return true;
    },
    _getQueryStringValue: function (queryString, key) {
        if (!queryString) {
            queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        }

        if (queryString.contains(key)) {
            var stringVal = queryString.get(key);
            if (stringVal == 'true') {
                return true;
            }
            else if (stringVal == 'false') {
                return false;
            }
            return stringVal;
        }
        return null;
    },

    _getData: function (field, property) {
        var controlData = this._designer.get_controlData();
        if (controlData[field] != null) {
            if (property == null)
                return controlData[field];
            else {
                var value = controlData[field][property];
                if (value != "" && value != '')
                    return value;
            }
        }
        return null;
    },

    setDefinitionByFieldType: function (queryString) {
        var fieldName = this._getQueryStringValue(queryString, "fieldName") || this._getData("FieldName");
        var widgetTypeName = this._getQueryStringValue(queryString, "widgetTypeName");
        var fieldType = null;
        var fieldVirtualPath = null; 

        if (widgetTypeName) {
            if (widgetTypeName.startsWith("~")) {
                fieldVirtualPath = widgetTypeName;
                fieldType = null;
            } else {
                fieldVirtualPath = null;
                fieldType = widgetTypeName;
            }
        } else {
            fieldType = this._getData("FieldType");
            fieldVirtualPath = this._getData("FieldVirtualPath");
        }

        var fieldTypeName = this._getQueryStringValue(queryString, "fieldTypeName");
        var definition =
        {
            'FieldName': fieldName,
            'FieldType': fieldType,
            'FieldVirtualPath': fieldVirtualPath,
            'Title': this._getData("Title"),
            'Example': this._getData("Example"),
            'CssClass': this._getData("CssClass"),
            'DefaultValue': this._getData("DefaultStringValue"),
            'MutuallyExclusive': true,
            'Choices': '',
            'DefaultStringValue': this._getData("DefaultStringValue"),
            'DefaultValue': this._getData("DefaultStringValue"),
            'SectionName': this._getData("SectionName"),
            'ResourceClassId': this._getData("ResourceClassId"),

            // image field properties
            'DefaultImageId': this._getData("DefaultImageId"),
            'ProviderNameForDefaultImage': this._getData("ProviderNameForDefaultImage"),
            'DefaultItemTypeName': this._getData("DefaultItemTypeName"),
            'MaxWidth': this._getData("MaxWidth"),
            'MaxHeight': this._getData("MaxHeight"),
            'MaxFileSize': this._getData("MaxFileSize"),
            'DefaultSrc': this._getData("DefaultSrc"),
            'IsLocalizable': this._getData('IsLocalizable')
        };

        var visibleViews = this._getData("VisibleViews");
        definition.VisibleViews = visibleViews;

        definition.Hidden = this._getData("Hidden");
        definition.AllowMultipleSelection = this._getData("AllowMultipleSelection");

        definition.ValidatorDefinition = {};
        var validatorDefinition = this._getData("ValidatorDefinition");

        if (validatorDefinition != null) {
            for (var propertyName in validatorDefinition) {
                var propertyValue = this._getData("ValidatorDefinition", propertyName);

                if (propertyValue !== null) {
                    definition.ValidatorDefinition[propertyName] = propertyValue;
                }
            }
        }

        if (fieldTypeName == "MultipleChoice") {
            definition.Choices = this.getChoiceItemsOld(this._getData("ChoiceItemsTitles"));
            definition.SortAlphabetically = this._getData("SortAlphabetically");
        }
        else if (fieldTypeName == "Choices") {
            definition.RenderChoiceAs = this._getData("RenderChoiceAs");
            definition.Choices = this.getChoiceItems(this._getData("ChoiceItemsTitles"));
            definition.SortAlphabetically = this._getData("SortAlphabetically");
        }
        else if (fieldTypeName == "YesNo") {
            definition.RenderChoiceAs = Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox;
            definition.HideTitle = true;
            var choiceItems =
            [
                {
                    Text: definition.Title,
                    Value: 'true',
                    Selected: false
                }
            ];
            definition.Choices = Sys.Serialization.JavaScriptSerializer.serialize(choiceItems);
        }
        else if (fieldTypeName == "Currency") {
            definition.ValidatorDefinition.ExpectedFormat = Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.Currency;
            //CurrencyViolationMessage: 'Price should be a valid currency value.'
        }
        else if (fieldTypeName == "Number") {
            definition.ValidatorDefinition.ExpectedFormat = Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.Numeric;
        }
        else if (fieldTypeName == "Classification") {
            definition.TaxonomyId = this._getQueryStringValue(queryString, "taxonomyId");
        }

        if (!definition.DefaultImageId) {
            definition.DefaultImageId = '00000000-0000-0000-0000-000000000000';
        }

        if (!definition.AllowMultipleSelection) {
            definition.AllowMultipleSelection = false;
        }

        return definition;
    },
    getChoiceItems: function (value, defaultSelectedTitle) {
        choices = value.children();
        var choiceItems = [];
        for (var i = 0; i < choices.length; i++) {
            var choice = $(choices[i]);
            var text = choice.attr('text');
            var v = choice.attr('value');
            if (v != "" && text != "") {
                var choiceItem = {
                    Text: text,
                    Value: v,
                    Selected: false
                };
                choiceItems.push(choiceItem);
            }
        }

        return Sys.Serialization.JavaScriptSerializer.serialize(choiceItems);
    },
    getChoiceItemsOld: function (value, defaultSelectedTitle) {
        value = value.split(";");
        var choiceItems = [];
        for (var i = 0, length = value.length; i < length; i++) {
            var v = value[i];
            if (v != "") {
                var choiceItem = {
                    Text: v,
                    Value: v,
                    Selected: false
                };
                choiceItems.push(choiceItem);
            }
        }

        return Sys.Serialization.JavaScriptSerializer.serialize(choiceItems);
    },

    /* -------------------- Event handlers ---------------- */
    _backHandler: function (sender, args) {
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();

        var fieldTypeName = this._getQueryStringValue(queryString, "fieldTypeName");
        var fieldName = this._getQueryStringValue(queryString, "fieldName");
        var hidden = this._getQueryStringValue(queryString, "hidden");
        var renderChoiceAs = this._getQueryStringValue(queryString, "renderChoiceAs");
        var multiple = this._getQueryStringValue(queryString, "multiple");
        var widgetTypeName = this._getQueryStringValue(queryString, "widgetTypeName");
        var dbType = this._getQueryStringValue(queryString, "dbType");
        var dbSqlType = this._getQueryStringValue(queryString, "dbSqlType");
        var dbLength = this._getQueryStringValue(queryString, "dbLength");
        var allowEmptyValues = this._getQueryStringValue(queryString, "allowEmptyValues");
        var includeInIndexes = this._getQueryStringValue(queryString, "includeInIndexes");
        var columnName = this._getQueryStringValue(queryString, "columnName");
        var dbPrecision = this._getQueryStringValue(queryString, "dbPrecision");
        var dbScale = this._getQueryStringValue(queryString, "dbScale");
        var taxonomyId = this._getQueryStringValue(queryString, "taxonomyId");
        var clrType = this._getQueryStringValue(queryString, "clrType");
        var componentType = this._getQueryStringValue(queryString, "componentType");
        var contentType = this._getQueryStringValue(queryString, "contentType");

        var url = String.format(
            "FieldWizardDialog?fieldTypeName={0}&fieldName={1}&hidden={2}&renderChoiceAs={16}&multiple={17}&widgetTypeName={3}&dbType={4}&dbSqlType={5}&dbLength={6}&allowEmptyValues={7}&includeInIndexes={8}&columnName={9}&dbPrecision={10}&dbScale={11}&taxonomyId={12}&clrType={13}&componentType={14}&contentType={15}",
            fieldTypeName,
            fieldName,
            hidden,
            widgetTypeName,
            dbType,
            dbSqlType,
            dbLength,
            allowEmptyValues,
            includeInIndexes,
            columnName,
            dbPrecision,
            dbScale,
            taxonomyId,
            encodeURIComponent(clrType),
            componentType,
            contentType,
            renderChoiceAs,
            multiple);
        window.location.replace(url);
    },
    _normalizeDefinition: function (definition) {
        var def = definition || this._definition;

        if (!def.ChoiceItemsTitles && def.Choices) {
            if (typeof def.Choices === 'string') {
                def.Choices = Sys.Serialization.JavaScriptSerializer.deserialize(def.Choices);
            }

            var titles = [], len = def.Choices.length;
            for (var i = 0; i < len; i++) {
                titles[i] = def.Choices[i].Text;
            }

            if (def.SortAlphabetically) {
                titles.sort();
            }

            var choiceTitles = "";
            for (i = 0; i < len; i++) {
                choiceTitles += (i ? ";" : "") + titles[i];
            }

            def.ChoiceItemsTitles = choiceTitles;
        }
    },

    /* -------------------- Properties ---------------- */
    // overridden property
    get_control: function () {
        return this.get_definition();
    },
    get_definition: function () {
        if (!this._definition) {
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
            var widgetTypeName = this._getQueryStringValue(queryString, "widgetTypeName");
            var fieldVirtualPath = null;
            var fieldType = null;
            if (widgetTypeName && widgetTypeName.startsWith("~")) {
                fieldVirtualPath = widgetTypeName;
            }
            else {
                fieldType = widgetTypeName;
            }
            var fieldName = this._getQueryStringValue(queryString, "fieldName");

            this._definition =
                {
                    'FieldName': fieldName,
                    'FieldType': fieldType,
                    'FieldVirtualPath': fieldVirtualPath,
                    'Title': '',
                    'Example': '',
                    'CssClass': '',
                    'ChoiceItemsTitles': 'Choice one;Choice two;Choice three',
                    'ValidatorDefinition': {
                        'Required': '',
                        'MinLength': '',
                        'MaxLength': '',
                        'DefaultValue': '',
                        'MaxLengthViolationMessage': '',
                        'MinLengthViolationMessage': '',
                        'RecommendedCharactersCount': ''
                    },
                    'DefaultValue': '',
                    'TaxonomyId': '',
                    'VisibleViews': [],
                    'Hidden': false,
                    'AllowMultipleSelection': false,
                    'SectionName': '',
                    'ResourceClassId': '',
                    'IsLocalizable': false
                };
        }
        return this._definition;
    },
    set_definition: function (value) {
        this._definition = value;
        this._normalizeDefinition();
        this._designer.get_controlData().Hidden = this._definition.Hidden;

        var designer = this.get_designer();
        if (designer) {
            designer.refreshUI();
        }
    },
    get_databaseMapping: function () {
        if (!this._databaseMapping) {
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();

            var clrType = this._getQueryStringValue(queryString, "clrType");

            this._databaseMapping = {
                'ClrType': clrType && decodeURIComponent(clrType),
                'DbType': this._getQueryStringValue(queryString, "dbType"),
                'DbSqlType': this._getQueryStringValue(queryString, "dbSqlType"),
                'DbLength': this._getQueryStringValue(queryString, "dbLength"),
                'DbPrecision': this._getQueryStringValue(queryString, "dbPrecision"),
                'DbScale': this._getQueryStringValue(queryString, "dbScale"),
                'Nullable': this._getQueryStringValue(queryString, "allowEmptyValues") || true,
                'Indexed': this._getQueryStringValue(queryString, "includeInIndexes") || false,
                'ColumnName': this._getQueryStringValue(queryString, "columnName")
            };
        }
        return this._databaseMapping;
    },
    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },
    get_defaultValueValidationMessage: function () {
        return this._defaultValueValidationMessage;
    },
    set_defaultValueValidationMessage: function (value) {
        this._defaultValueValidationMessage = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};

Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor.registerClass("Telerik.Sitefinity.ModuleEditor.Web.UI.CustomFieldPropertyEditor", Telerik.Sitefinity.Web.UI.PropertyEditor);

