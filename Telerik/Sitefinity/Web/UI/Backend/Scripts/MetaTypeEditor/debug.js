Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor = function (element) {
    //control IDs given from the server
    this._addNewDataFieldButtonId = null;
    this._deleteDataFieldButtonId = null;
    this._fieldsListId = null;
    this._fieldTypeListBoxId = null;
    this._fieldDbTypeListBoxId = null;
    this._defaultValueCheckBoxId = null;
    this._taxonomyProviderListId = null;
    this._taxonomyListId = null;
    this._allowMultipleTaxonsCheckBoxId = null;
    this._precisionTextBoxId = null;
    this._messageControlId = null;
    this._taxonomySelectorBinderId = null;
    this._fieldSQLDbTypeListBoxId = null;
    this._fieldTitleTextBoxId = null;
    this._fieldNameTextBoxId = null;
    this._maxLengthTextBoxId = null;
    this._defaultValueTextBoxId = null;
    this._clientLabelManagerId = null;

    //panels
    this._maxLengthPanelId = null;
    this._defaultValuesPanelId = null;
    this._precisionPanelId = null;
    this._taxonomiesPanelId = null;
    this._dBTypePanelId = null;
    this._taxonomieProvidersPanelId = null;
    this._defaultTextFieldPanelId = null;
    this._chkDefaultValuePanelId = null;

    //server vars
    this._fieldDefinitions = null;

    //private control references
    this._addNewDataFieldButton = null;
    this._deleteDataFieldButton = null;
    this._fieldsList = null;
    this._fieldTypeListBox = null;
    this._fieldDbTypeListBox = null;
    this._defaultValueTextBox = null;
    this._defaultValueCheckBox = null;
    this._taxonomyProviderList = null;
    this._taxonomyList = null;
    this._allowMultipleTaxonsCheckBox = null;
    this._precisionTextBox = null;
    this._messageControl = null;
    this._taxonomySelectorBinder = null;
    this._fieldSQLDbTypeListBox = null;
    this._fieldTitleTextBox = null;
    this._fieldNameTextBox = null;
    this._maxLengthTextBox = null;
    this._defaultValueTextBox = null;
    this._clientLabelManager = null;

    //panels
    this._maxLengthPanel = null;
    this._defaultValuesPanel = null;
    this._precisionPanel = null;
    this._taxonomiesPanel = null;
    this._dBTypePanel = null;
    this._taxonomieProvidersPanel = null;
    this._defaultTextFieldPanel = null;
    this._chkDefaultValuePanel = null;

    //private vars
    this._loadedNonMetaFieldsObject = null;
    this._dynamicTypeObject = null;
    this._taxonomyToSelect = null;
    this._currentlyEditedField = null;
    this._previouslySelectedIndex = -1;
    this._bSuppressDataCollection = false;
    this._loaded = false;
    this._suppressValidationChecks = false;

    Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor.prototype = {

    /* ****************************** set up / tear down methods ****************************** */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor.callBaseMethod(this, 'initialize');

        //populate private control references
        this._addNewDataFieldButton = $get(this._addNewDataFieldButtonId);
        this._deleteDataFieldButton = $get(this._deleteDataFieldButtonId);
        this._fieldsList = $get(this._fieldsListId);
        this._defaultValueTextBox = $get(this._defaultValueTextBoxId);
        this._defaultValueCheckBox = $get(this._defaultValueCheckBoxId);
        this._allowMultipleTaxonsCheckBox = $get(this._allowMultipleTaxonsCheckBoxId);
        //panels
        this._maxLengthPanel = $get(this._maxLengthPanelId);
        this._defaultValuesPanel = $get(this._defaultValuesPanelId);
        this._precisionPanel = $get(this._precisionPanelId);
        this._taxonomiesPanel = $get(this._taxonomiesPanelId);
        this._dBTypePanel = $get(this._dBTypePanelId);
        this._taxonomieProvidersPanel = $get(this._taxonomieProvidersPanelId);
        this._defaultTextFieldPanel = $get(this._defaultTextFieldPanelId);
        this._chkDefaultValuePanel = $get(this._chkDefaultValuePanelId);

        //deserialize
        this._fieldDefinitions = Sys.Serialization.JavaScriptSerializer.deserialize(this._fieldDefinitions);

        //event handler delegates
        this._fieldTypeChangedDelegate = Function.createDelegate(this, this._fieldTypeChanged);
        this._addNewDataFieldButtonClickDelegate = Function.createDelegate(this, this._addNewDataFieldButtonClick);
        this._deleteDataFieldButtonClickDelegate = Function.createDelegate(this, this._deleteDataFieldButtonClick);
        this._taxonomyProviderChangedDelegate = Function.createDelegate(this, this._taxonomyProviderChanged);
        this._fieldsListClickDelegate = Function.createDelegate(this, this._fieldsListSelectionChanged);
        this._fieldsListSelectionChangedDelegate = Function.createDelegate(this, this._fieldsListSelectionChanged);

        this._fieldTitleKeyUpDelegate = Function.createDelegate(this, this._fieldTitleKeyUp);
        this._fieldNameTextBoxKeyUpDelegate = Function.createDelegate(this, this._fieldNameTextBoxKeyUp);
        this._validateNameFieldDelegate = Function.createDelegate(this, this._validateNameField);
        this._validateMaxLengthFieldDelegate = Function.createDelegate(this, this._validateMaxLengthField);
        this._validateDefaultValueTextFieldDelegate = Function.createDelegate(this, this._validateDefaultValueTextField);
        this._validatePrecisionDelegate = Function.createDelegate(this, this._validatePrecision);
        this._fieldNameTextBoxCutPasteDelegate = Function.createDelegate(this, this._fieldNameTextBoxCutPaste);
        this._fieldTitleTextBoxCutPasteDelegate = Function.createDelegate(this, this._fieldTitleTextBoxCutPaste);
        this._taxonomySelectorBinderDataBoundDelegate = Function.createDelegate(this, this._taxonomySelectorBinderDataBound);

        //event handlers
        $addHandler(this._addNewDataFieldButton, "click", this._addNewDataFieldButtonClickDelegate);
        $addHandler(this._deleteDataFieldButton, "click", this._deleteDataFieldButtonClickDelegate);
        $addHandler(this._fieldsList, "click", this._fieldsListClickDelegate);
        $addHandler(this._fieldsList, "change", this._fieldsListSelectionChangedDelegate);

        Sys.Application.add_load(Function.createDelegate(this, this.onload));
    },

    // tear down
    dispose: function () {
        // clear dom events
        $clearHandlers(this._addNewDataFieldButton);
        $clearHandlers(this._deleteDataFieldButton);
        $clearHandlers(this._fieldsList);

        $removeHandler(this._fieldNameTextBox.get_textBoxElement(), "keyup", this._fieldNameTextBoxKeyUpDelegate);
        $removeHandler(this._fieldTitleTextBox.get_textBoxElement(), "keyup", this._fieldTitleKeyUpDelegate);
        $removeHandler(this._fieldNameTextBox.get_textBoxElement(), "blur", this._validateNameFieldDelegate);
        $removeHandler(this._maxLengthTextBox.get_textBoxElement(), "blur", this._validateMaxLengthFieldDelegate);
        $removeHandler(this._defaultValueTextBox.get_textBoxElement(), "blur", this._validateDefaultValueTextFieldDelegate);
        $removeHandler(this._precisionTextBox.get_textBoxElement(), "blur", this._validatePrecisionDelegate);

        // clear jQuery events
        jQuery(this._fieldNameTextBox.get_textBoxElement()).unbind('cut paste', this._fieldNameTextBoxCutPasteDelegate);
        jQuery(this._fieldTitleTextBox.get_textBoxElement()).unbind('cut paste', this._fieldNameTitleBoxCutPasteDelegate);

        // clear component events
        this._taxonomySelectorBinder.remove_onDataBound(this._taxonomySelectorBinderDataBoundDelegate);

        // clear delegates
        delete this._fieldTitleKeyUpDelegate;
        delete this._fieldNameTextBoxKeyUpDelegate;
        delete this._fieldTypeChangedDelegate;
        delete this._addNewDataFieldButtonClickDelegate;
        delete this._deleteDataFieldButtonClickDelegate;
        delete this._taxonomyProviderChangedDelegate;
        delete this._fieldsListClickDelegate;
        delete this._fieldsListSelectionChangedDelegate;
        delete this._validateNameFieldDelegate;
        delete this._validateMaxLengthFieldDelegate;
        delete this._validateDefaultValueTextFieldDelegate;
        delete this._validatePrecisionDelegate;
        delete this._fieldNameTextBoxCutPasteDelegate;
        delete this._fieldTitleTextBoxCutPasteDelegate;
        delete this._taxonomySelectorBinderDataBoundDelegate

        // clear dom references
        delete this._addNewDataFieldButton;
        delete this._deleteDataFieldButton;
        delete this._fieldsList;
        delete this._defaultValueTextBox;
        delete this._defaultValueCheckBox;
        delete this._allowMultipleTaxonsCheckBox;
        delete this._maxLengthPanel;
        delete this._defaultValuesPanel;
        delete this._precisionPanel;
        delete this._taxonomiesPanel;
        delete this._dBTypePanel;
        delete this._taxonomieProvidersPanel;
        delete this._defaultTextFieldPanel;
        delete this._chkDefaultValuePanel;

        // clear component references
        delete this._fieldTypeListBox;
        delete this._fieldDbTypeListBox;
        delete this._taxonomyProviderList;
        delete this._taxonomyList;
        delete this._messageControl;
        delete this._taxonomySelectorBinder;
        delete this._fieldSQLDbTypeListBox;
        delete this._fieldNameTextBox;
        delete this._fieldTitleTextBox;
        delete this._maxLengthTextBox;
        delete this._defaultValueTextBox;
        delete this._precisionTextBox;
        delete this._clientLabelManager;

        Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor.callBaseMethod(this, 'dispose');
    },

    onload: function () {
        //populate private AJAX control references
        this._fieldTypeListBox = $find(this._fieldTypeListBoxId);
        this._fieldDbTypeListBox = $find(this._fieldDbTypeListBoxId);
        this._taxonomyProviderList = $find(this._taxonomyProviderListId);
        this._taxonomyList = $find(this._taxonomyListId);
        this._messageControl = $find(this._messageControlId);
        this._taxonomySelectorBinder = $find(this._taxonomySelectorBinderId);
        this._fieldSQLDbTypeListBox = $find(this._fieldSQLDbTypeListBoxId);
        this._fieldNameTextBox = $find(this._fieldNameTextBoxId);
        this._fieldTitleTextBox = $find(this._fieldTitleTextBoxId);
        this._maxLengthTextBox = $find(this._maxLengthTextBoxId);
        this._defaultValueTextBox = $find(this._defaultValueTextBoxId);
        this._precisionTextBox = $find(this._precisionTextBoxId);
        this._clientLabelManager = $find(this._clientLabelManagerId);

        this._fieldTypeListBox.add_selectedIndexChanged(this._fieldTypeChangedDelegate);
        this._taxonomyProviderList.add_selectedIndexChanged(this._taxonomyProviderChangedDelegate);

        $addHandler(this._fieldNameTextBox.get_textBoxElement(), "keyup", this._fieldNameTextBoxKeyUpDelegate);
        $addHandler(this._fieldTitleTextBox.get_textBoxElement(), "keyup", this._fieldTitleKeyUpDelegate);
        $addHandler(this._fieldNameTextBox.get_textBoxElement(), "blur", this._validateNameFieldDelegate);
        $addHandler(this._maxLengthTextBox.get_textBoxElement(), "blur", this._validateMaxLengthFieldDelegate);
        $addHandler(this._defaultValueTextBox.get_textBoxElement(), "blur", this._validateDefaultValueTextFieldDelegate);
        $addHandler(this._precisionTextBox.get_textBoxElement(), "blur", this._validatePrecisionDelegate);

        jQuery(this._fieldNameTextBox.get_textBoxElement()).bind('cut paste', this._fieldNameTextBoxCutPasteDelegate);
        jQuery(this._fieldTitleTextBox.get_textBoxElement()).bind('cut paste', this._fieldTitleTextBoxCutPasteDelegate);

        this._taxonomySelectorBinder.add_onDataBound(this._taxonomySelectorBinderDataBoundDelegate);

        this._fieldTypeChanged();
        this._rebindTaxonomies();
        dialogBase.resizeToContent();
        this._loaded = true;
        this._selectFirstOption();
    },

    // ------------------------------------- client-side classes -------------------------------------
    //Constructor for the SimpleDefinitionField class for representing a field of a dynamic type
    SimpleDefinitionField: function (fieldTitle, fieldName, fieldType, fieldDBType, fieldSQLDBType, fieldMaxLength, fieldDefaultValue, fieldPrecision, fieldTaxonomyProviderName, fieldTaxonomyId, allowMultipleTaxons, fieldDefinition, isMetaField) {
        this.Title = fieldTitle;
        this.Name = fieldName;
        this.ClrType = fieldType;
        this.DBType = fieldDBType;
        this.SQLDBType = fieldSQLDBType;
        this.MaxLength = fieldMaxLength;
        this.DefaultValue = fieldDefaultValue;
        this.Precision = fieldPrecision;
        this.TaxonomyProviderName = fieldTaxonomyProviderName;
        this.TaxonomyId = fieldTaxonomyId;
        this.AllowMultipleTaxons = allowMultipleTaxons;
        this.FieldDefinition = fieldDefinition;
        this.IsRequired = false;
        this.IsModified = false;
        this.IsMetaField = isMetaField;
    },

    _fieldNameTextBoxCutPaste: function () {
        //hack to catch the pasted text
        setTimeout(Function.createDelegate(this, this._fieldNameTextBoxChanged), 1);
    },

    _fieldNameTextBoxKeyUp: function () {
        this._fieldNameTextBoxChanged();
    },

    _fieldTitleKeyUp: function () {
        this._fieldTitleTextBoxChanged();
    },

    _fieldNameTextBoxChanged: function () {
        var curName = this._fieldNameTextBox.get_value();
        this._getFieldIfAlreadyExists(this._currentlyEditedField).Name = curName;
        this._currentlyEditedField = curName;
        this._fieldsList.options[this._fieldsList.selectedIndex].text = curName;
    },

    _fieldTitleTextBoxCutPaste: function () {
        //hack to catch the pasted text
        setTimeout(Function.createDelegate(this, this._fieldTitleTextBoxChanged), 1);
    },

    _fieldTitleTextBoxChanged: function () {
        this._getFieldIfAlreadyExists(this._currentlyEditedField).Title = this._fieldTitleTextBox.get_value();
    },

    get_dynamicTypeObject: function () {
        if (this._dynamicTypeObject.length > 0) {
            if (!this._tryCollectCurFieldChanges(false))
                throw ("Validation error");
        }

        for (var i = 0; i < this._loadedNonMetaFieldsObject.length; i++) {
            this._dynamicTypeObject[this._dynamicTypeObject.length] = this._loadedNonMetaFieldsObject[i];
        }
        for (var x = 0; x < this._dynamicTypeObject.length; x++) {
            var cur = this._dynamicTypeObject[x];
            if (typeof cur != "undefined") {
                delete this._dynamicTypeObject[x]["FieldDefinition"];
            }
        }
        return this._dynamicTypeObject;
    },

    set_dynamicTypeObject: function (dynObj) {
        /// <summary>Sets the currently edited dynamic type.</summary>
        /// <param name="dynObj">An array of SimpleDefinitionField objects, representing the dynamic type.</param>
        /// <returns>Void.</returns>            
        dynObj = this._fixArray(dynObj);
        if (dynObj == null) {
            alert(this._clientLabelManager.getLabel("ErrorMessages", "DynamicTypeDefinitionIsNotArray"));
            return;
        }
        else {
            this._clearFieldsList();
            this._dynamicTypeObject = new Array();
            this._loadedNonMetaFieldsObject = new Array();
            for (var i = 0; i < dynObj.length; i++) {
                if (dynObj[i].IsMetaField) {
                    this._dynamicTypeObject.push(new this.SimpleDefinitionField(
                        dynObj[i].Title,
                        dynObj[i].Name,
                        dynObj[i].ClrType,
                        dynObj[i].DBType,
                        dynObj[i].SQLDBType,
                        dynObj[i].MaxLength,
                        dynObj[i].DefaultValue,
                        dynObj[i].Precision,
                        dynObj[i].TaxonomyProviderName,
                        dynObj[i].TaxonomyId,
                        dynObj[i].AllowMultipleTaxons,
                        this._getFieldTypeDefinitionByClrType(dynObj[i].ClrType),
                        dynObj[i].IsMetaField));
                    var opt = new Option(dynObj[i].Name, "", false, false);
                    this._fieldsList.options.add(opt);
                }
                else
                    this._loadedNonMetaFieldsObject.push(new this.SimpleDefinitionField(
                        dynObj[i].Title,
                        dynObj[i].Name,
                        dynObj[i].ClrType,
                        dynObj[i].DBType,
                        dynObj[i].SQLDBType,
                        dynObj[i].MaxLength,
                        dynObj[i].DefaultValue,
                        dynObj[i].Precision,
                        dynObj[i].TaxonomyProviderName,
                        dynObj[i].TaxonomyId,
                        dynObj[i].AllowMultipleTaxons,
                        null,
                        dynObj[i].IsMetaField));
            }
            this._selectFirstOption();
        }
    },

    _generateNewFieldName: function () {
        var newFieldNamePattern = "NewField";
        var newFieldCounter = 0;

        for (var i = this._fieldsList.options.length - 1; i >= 0; i--) {
            var text = this._fieldsList.options[i].text;
            var num = parseInt(text.substring(newFieldNamePattern.length));
            if ((text.indexOf(newFieldNamePattern) == 0) && (!isNaN(num)) && (num >= newFieldCounter)) {
                newFieldCounter = num;
            }
        }
        newFieldCounter++;
        return newFieldNamePattern + newFieldCounter;
    },

    _clearFieldsList: function () {
        /// <summary>Clears the list of dynamic fields</summary>
        /// <returns>Void.</returns>    
        for (var i = this._fieldsList.options.length - 1; i >= 0; i--) {
            this._fieldsList.remove(i);
        }
    },

    _isArray: function (obj)
    /// <summary>Determines if a certin object is an array.</summary>
    /// <param name="obj">The object suspicious of being an array.</param>
    /// <returns>True if the object is an array, False otherwise.</returns>    
    {
        if (obj.constructor == Array || obj instanceof Array || Array.prototype.isPrototypeOf(obj)) {
            return true;
        }
        else {
            return false;
        }
    },

    _fixArray: function (obj) {
        return Telerik.Sitefinity.fixArray(obj);
    },

    _taxonomySelectorBinderDataBound: function () {
        /// <summary>Handler for the DataBound event of the taxonomy list. Selects a specific taxonomy after data is bound, if needed.</summary>
        /// <returns>Void.</returns>    
        if (this._taxonomyToSelect != null) {
            this._selectTaxonomyById(this._taxonomyToSelect);
            this._taxonomyToSelect = null;
        }
    },

    _selectTaxonomy: function (taxonomyProviderName, taxonomyId) {
        /// <summary>Selects a specific taxonomy provider and taxonomy (a rebind occurs when the taxonomy provider is changed, then _taxonomySelectorBinderDataBound will be invoked).</summary>
        /// <param name="taxonomyProviderID">ID of the taxonomy provider to select</param>
        /// <param name="taxonomyId">ID of the taxonomy to select</param>
        /// <returns>Void.</returns>    
        if (this._taxonomyProviderList.get_value() != taxonomyProviderName) {
            this._taxonomyProviderList.findItemByValue(taxonomyProviderName).select();
            this._taxonomyToSelect = taxonomyId;
        }
        else {
            this._selectTaxonomyById(taxonomyId);
        }
    },

    //TODO: use this._taxonomySelectorBinder.set_selectedItem('Id', taxonomyId)
    _selectTaxonomyById: function (taxonomyId) {
        /// <summary>Selects a specific taxonomy in the list by ID</summary>
        /// <param name="taxonomyId">ID of the taxonomy to select</param>
        /// <returns>Void.</returns>    
        for (var i = 0; i < this._taxonomyList.get_items().toArray().length; i++) {
            if (this._taxonomyList.get_items().toArray()[i].get_value().Id == taxonomyId) {
                this._taxonomyList.get_items().toArray()[i].select();
                break;
            }
        }
    },

    _validateNameField: function () {
        /// <summary>Invokes validation check on the name text field.</summary>
        /// <returns>True if the field contains valid data, False otherwise.</returns>    
        if (this._suppressValidationChecks)
            return true;

        var isValid = this._fieldNameTextBox.validate();
        return isValid;
    },

    _validateMaxLengthField: function () {
        /// <summary>Invokes validation check on the max length text field.</summary>
        /// <returns>True if the field contains valid data, False otherwise.</returns>    
        if (this._suppressValidationChecks)
            return true;

        var isValid = this._maxLengthTextBox.validate();
        return isValid;
    },

    _validateDefaultValueTextField: function () {
        /// <summary>Invokes validation check on the default value field.</summary>
        /// <returns>True if the field contains valid data, False otherwise.</returns>    
        if (this._suppressValidationChecks)
            return true;

        var isValid = this._defaultValueTextBox.validate();
        return isValid;
    },

    _validatePrecision: function () {
        /// <summary>Invokes validation check on the precision field.</summary>
        /// <returns>True if the field contains valid data, False otherwise.</returns>    
        if (this._suppressValidationChecks)
            return true;

        var isValid = this._precisionTextBox.validate();
        return isValid;
    },

    _fieldTypeChanged: function () {
        /// <summary>Invoked when the field type drop-down is changed. Sets display settings for relevant panels and controls.</summary>
        /// <returns>Void.</returns>    
        var fieldDef = this._getCurrSelectedFieldTypeDefinition();
        if (fieldDef != null) {
            this._synchComboBoxToValuesList(this._fieldDbTypeListBox, fieldDef.DBTypes, fieldDef.DefaultDBType);
            this._synchComboBoxToValuesList(this._fieldSQLDbTypeListBox, fieldDef.DBSQLTypes, fieldDef.DefaultDBSQLType);

            //show/hide panels
            this._toggleControlDisplay(this._defaultValuesPanel, (fieldDef.SupportsDefaultValueText || fieldDef.SupportsDefaultValueBool));
            this._toggleControlDisplay(this._defaultTextFieldPanel, fieldDef.SupportsDefaultValueText);
            this._toggleControlDisplay(this._chkDefaultValuePanel, fieldDef.SupportsDefaultValueBool);
            this._toggleControlDisplay(this._maxLengthPanel, fieldDef.SupportsMaxLength);
            this._toggleControlDisplay(this._taxonomiesPanel, fieldDef.SupportsTaxonomyFields);
            this._toggleControlDisplay(this._precisionPanel, fieldDef.SupportsPrecision);

            //show/hide panels with combo boxes according to num of items
            this._toggleControlDisplay(this._dBTypePanel, (fieldDef.DBTypes.length > 0 && fieldDef.DBSQLTypes.length > 0));
            this._toggleControlDisplay(this._taxonomieProvidersPanel, (this._taxonomyProviderList.get_items().toArray().length > 1));

            //set regular expression for the default text box
            if ((fieldDef.DefaultValueRegularExpression != null) && (fieldDef.DefaultValueRegularExpression != ""))
                this._defaultValueTextBox.get_validator().set_regularExpression(fieldDef.DefaultValueRegularExpression);

            dialogBase.resizeToContent();
        }
    },

    _toggleControlDisplay: function (ctlElement, bIsDisplayed) {
        /// <summary>Sets a display status (shoen/hidden) of a specific control.</summary>
        /// <param name="ctlElement">The element to show or hide.</param>
        /// <param name="bIsDisplayed">True if the element should be shown, False if the element should be hidden.</param>
        /// <returns>Void.</returns>    
        if (ctlElement != null)
            ctlElement.style.display = ((bIsDisplayed) ? "block" : "none");
    },

    get_clientLabelManager: function () {
        /// <summary>Gets the instance of te ClientLabelManager component on the page. In order to retrieve certain text resources from the server.</summary>
        /// <returns>The instance of te ClientLabelManager component on the page.</returns>    
        return this._clientLabelManager;
    },

    _getCurrSelectedFieldTypeDefinition: function () {
        /// <summary>Gets the definition matching the currently selected field type in the drop down.</summary>
        /// <returns>The definition matching the currently selected field type in the drop down.</returns>    
        return this._getFieldTypeDefinitionByClrType(this._fieldTypeListBox.get_value());
    },

    _getFieldTypeDefinitionByClrType: function (clrType) {
        var fieldDef = null;
        for (var i = 0; i < this._fieldDefinitions.length; i++) {
            if (this._fieldDefinitions[i].ClrType == clrType) {
                fieldDef = this._fieldDefinitions[i];
                break;
            }
        }
        return fieldDef;
    },

    _synchComboBoxToValuesList: function (comboElement, itemsList, valueToSelect) {
        /// <summary>Loads a list of strings into a RadComboBox element, and optionally selects a specific value.</summary>
        /// <param name="comboElement">The RadComboBox element to reload.</param>
        /// <param name="itemsList">A list of strings to load.</param>
        /// <param name="valueToSelect">The value to select once the values are loaded into the box.</param>
        /// <returns>Void.</returns>    
        if (comboElement != null) {
            comboElement.trackChanges();
            comboElement.clearSelection();
            comboElement.clearItems();
            var items = comboElement.get_items();
            var selectedItemIndex = -1;
            for (var curItem = 0; curItem < itemsList.length; curItem++) {
                //if no provider is set, or set to the current provider -> add it
                var comboItem = new Telerik.Web.UI.RadComboBoxItem();
                comboItem.set_text(itemsList[curItem]);
                comboItem.set_value(itemsList[curItem]);
                items.add(comboItem);
            }

            //locate the item to select, out of the providers which were actually added
            for (var curItem = 0; curItem < items.get_count(); curItem++) {
                if ((valueToSelect == null) || (valueToSelect == "") || (items.getItem(curItem).get_value() == valueToSelect)) {
                    selectedItemIndex = curItem;
                    break;
                }
            }
            if (items.get_count() > 0) {
                if (selectedItemIndex == -1) {
                    selectedItemIndex = 0;
                }
                items.getItem(selectedItemIndex).select();
            }
        }
        comboElement.commitChanges();
    },

    _taxonomyProviderChanged: function () {
        /// <summary>Event handler for the Changed selection of the taxonomy proviers drop down: invokes a rebind of the taxonomy list.</summary>
        /// <returns>Void.</returns>    
        this._rebindTaxonomies();
    },

    _rebindTaxonomies: function () {
        /// <summary>Rebinds the taxonomy list with the currently selected taxonomy provider.</summary>
        /// <returns>Void.</returns>    
        this._taxonomySelectorBinder.set_urlParams({
            "provider": this._taxonomyProviderList.get_text()
        });
        this._taxonomySelectorBinder.DataBind();
    },

    _validateFields: function (checkMaxLength, checkDefaultValueText, checkPrecision) {
        /// <summary>Invokes validation check on all validatable text fields.</summary>
        /// <returns>True if all fields are valid, False if some or all are not valid.</returns>    

        if (this._suppressValidationChecks)
            return true;

        var isValid = true;

        if (!this._validateNameField())
            isValid = false;

        if ((typeof (checkMaxLength) == "undefined" || checkMaxLength) && (!this._validateMaxLengthField()))
            isValid = false;

        if ((typeof (checkDefaultValueText) == "undefined" || checkDefaultValueText) && (!this._validateDefaultValueTextField()))
            isValid = false;

        if ((typeof (checkPrecision) == "undefined" || checkPrecision) && (!this._validatePrecision()))
            isValid = false;

        return isValid;
    },

    _clearValidationMessages: function () {
        this._fieldNameTextBox.clearViolationMessage();
        this._maxLengthTextBox.clearViolationMessage();
        this._defaultValueTextBox.clearViolationMessage();
        this._precisionTextBox.clearViolationMessage();
    },

    _getFieldIfAlreadyExists: function (fieldName) {
        /// <summary>Gets a dynamic field definition out of the currently dynamic type object, if exists. Null if the field doesn't exist.</summary>
        /// <param name="fieldName">The name of the field to retrieve.</param>
        /// <returns>A dynamic field definition out of the currently dynamic type object, if exists. Null if the field doesn't exist.</returns>     
        // for some reason index 0 is not present in the array
        for (var propName in this._dynamicTypeObject) {
            if (!isNaN(parseInt(propName))) {
                var fld = this._dynamicTypeObject[propName];
                if (fld.Name == fieldName)
                    return fld;
            }
        }
        return null;
    },

    _getFieldIndexIfAlreadyExists: function (fieldName) {
        for (var i = 0; i < this._dynamicTypeObject.length; i++) {
            if (this._dynamicTypeObject[i].Name == fieldName)
                return i;
        }
        return null;
    },

    _doesFieldNameAlreadyExist: function (fieldName) {
        /// <summary>Checks if a dynamic field definition exists in the currently dynamic type object.</summary>
        /// <param name="fieldName">The name of the field to check for.</param>
        /// <returns>True if the field by the given name exists. False otherwise.</returns> 
        for (var i = 0; i < this._dynamicTypeObject.length; i++) {
            if (this._dynamicTypeObject[i].Name == fieldName)
                return true;
        }
        return false;
    },

    _addNewDataFieldButtonClick: function () {
        /// <summary>Handles the click event on the Add New Data field linkbutton: Adds a new dynamic field.</summary>
        /// <returns>Void.</returns> 
        this._createNewField();
    },

    _createNewField: function () {
        if ((this._fieldsList.selectedIndex != -1) && (!this._tryCollectCurFieldChanges(false)))
            return;

        this._fieldsList.selectedIndex = -1;
        this._messageControl.hide();
        var fieldName = this._generateNewFieldName();
        var fieldIndex = this._fieldsList.options.length;

        this._suppressValidationChecks = true;
        this._clearFields();
        var opt = new Option(fieldName, "", false, false);
        this._fieldsList.options[fieldIndex] = opt;

        this._fieldsList.options[fieldIndex].selected = true;
        this._fieldNameTextBox.set_value(fieldName);
        this._fieldTitleTextBox.set_value(fieldName);

        var newFieldData = this._collectFieldInfo();
        newFieldData.IsModified = true;
        this._dynamicTypeObject.push(newFieldData);
        this._currentlyEditedField = fieldName;
        this._previouslySelectedIndex = fieldIndex;
        this._suppressValidationChecks = false;
    },

    _collectFieldInfo: function () {
        var fieldDef = this._getCurrSelectedFieldTypeDefinition();

        var defVal = "";
        if (fieldDef.SupportsDefaultValueText) {
            defVal = this._defaultValueTextBox.get_value();
        }
        else if (fieldDef.SupportsDefaultValueBool) {
            defVal = this._defaultValueCheckBox.checked;
        }

        var precision = 0;
        if (fieldDef.SupportsPrecision) {
            precision = (isNaN(parseInt(this._precisionTextBox.get_value())) ? 0 : this._precisionTextBox.get_value());
        }

        var curFieldInfo = new this.SimpleDefinitionField(
            this._fieldTitleTextBox.get_value(),
            this._fieldNameTextBox.get_value(),
            this._fieldTypeListBox.get_value(),
            this._fieldDbTypeListBox.get_value(),
            this._fieldSQLDbTypeListBox.get_value(),
            (isNaN(parseInt(this._maxLengthTextBox.get_value())) ? 0 : this._maxLengthTextBox.get_value()),
            defVal,
            precision,
            ((fieldDef.SupportsTaxonomyFields) ? this._taxonomyProviderList.get_text() : ""),
            ((fieldDef.SupportsTaxonomyFields) ? this._taxonomyList.get_value().Id : ""),
            ((fieldDef.SupportsTaxonomyFields) ? this._allowMultipleTaxonsCheckBox.checked : false),
            fieldDef,
            true
        );

        return curFieldInfo;
    },

    _tryCollectCurFieldChanges: function (byPrevioiusSelection) {
        if (this._bSuppressDataCollection) {
            this._bSuppressDataCollection = false;
            return true;
        }
        this._clearValidationMessages();
        var fieldDef = this._getCurrSelectedFieldTypeDefinition();
        //validate the data
        if (!this._validateFields(fieldDef.SupportsMaxLength, fieldDef.SupportsDefaultValueText, fieldDef.SupportsPrecision)) {
            return false;
        }
        var selectionIndexToCheck = (byPrevioiusSelection) ? this._previouslySelectedIndex : this._fieldsList.selectedIndex;
        //check that the name is not duplicate
        for (var i = 0; i < this._fieldsList.options.length; i++) {
            if ((this._fieldsList.options[i].text == this._currentlyEditedField) && (i != selectionIndexToCheck)) {
                this._messageControl.showNegativeMessage(this.get_clientLabelManager().getLabel("Labels", "MetaFieldDuplicateFieldName"));
                return false;
            }
        }
        var existingFieldDef = this._getFieldIfAlreadyExists(this._currentlyEditedField);
        if (existingFieldDef != null) {
            var fieldUpdated = false;

            //field title:
            var fieldTitle = this._fieldTitleTextBox.get_value();
            if (existingFieldDef.Title != fieldTitle) {
                existingFieldDef.Title = fieldTitle;
                fieldUpdated = true;
            }

            //field name:
            var fieldName = this._fieldNameTextBox.get_value();
            if (existingFieldDef.Name != fieldName) {
                existingFieldDef.Name = fieldName;
                fieldUpdated = true;
            }

            //field type:
            var fieldType = this._fieldTypeListBox.get_value();
            if (existingFieldDef.ClrType != fieldType) {
                existingFieldDef.ClrType = fieldType;
                fieldUpdated = true;
            }

            //field DB type:
            var fieldDBType = this._fieldDbTypeListBox.get_value();
            if (existingFieldDef.DBType != fieldDBType) {
                existingFieldDef.DBType = fieldDBType;
                fieldUpdated = true;
            }

            //field SQL DB type:
            var fieldSqlDBType = this._fieldSQLDbTypeListBox.get_value();
            if (existingFieldDef.SQLDBType != fieldSqlDBType) {
                existingFieldDef.SQLDBType = fieldSqlDBType;
                fieldUpdated = true;
            }

            //field MaxLength:
            var fieldMaxLen = this._maxLengthTextBox.get_value();
            if (existingFieldDef.MaxLength != fieldMaxLen) {
                existingFieldDef.MaxLength = fieldMaxLen;
                fieldUpdated = true;
            }
            if (isNaN(parseInt(existingFieldDef.MaxLength)))
                existingFieldDef.MaxLength = 0;

            //field DefaultValue (text):
            if (fieldDef.SupportsDefaultValueText) {
                var fieldDefVal = this._defaultValueTextBox.get_value();
                if (existingFieldDef.DefaultValue != fieldDefVal) {
                    existingFieldDef.DefaultValue = fieldDefVal;
                    fieldUpdated = true;
                }
            }

            //field DefaultValue (bool):
            if (fieldDef.SupportsDefaultValueBool) {
                var fieldDefVal = this._defaultValueCheckBox.checked;
                if (existingFieldDef.DefaultValue != fieldDefVal) {
                    existingFieldDef.DefaultValue = fieldDefVal;
                    fieldUpdated = true;
                }
            }

            //field precision:
            if (fieldDef.SupportsPrecision) {
                var fieldPrec = this._precisionTextBox.get_value();
                if (existingFieldDef.Precision != fieldPrec) {
                    existingFieldDef.Precision = fieldPrec;
                    fieldUpdated = true;
                }
            }
            if (isNaN(parseInt(existingFieldDef.Precision)))
                existingFieldDef.Precision = 0;

            //field taxonomy:
            if (fieldDef.SupportsTaxonomyFields) {
                var fieldTaxProviderName = this._taxonomyProviderList.get_text();
                var fieldTaxId = this._taxonomyList.get_value().Id;
                var fieldAllowMultiple = this._allowMultipleTaxonsCheckBox.checked;
                if (existingFieldDef.TaxonomyProviderName != fieldTaxProviderName) {
                    existingFieldDef.TaxonomyProviderName = fieldTaxProviderName;
                    fieldUpdated = true;
                }
                if (existingFieldDef.TaxonomyId != fieldTaxId) {
                    existingFieldDef.TaxonomyId = fieldTaxId;
                    fieldUpdated = true;
                }
                if (existingFieldDef.AllowMultipleTaxons != fieldAllowMultiple) {
                    existingFieldDef.AllowMultipleTaxons = fieldAllowMultiple;
                    fieldUpdated = true;
                }
            }
            if ((typeof (existingFieldDef.AllowMultipleTaxons) == "undefined") || (existingFieldDef.AllowMultipleTaxons == null))
                existingFieldDef.AllowMultipleTaxons = false;

            //field type definition
            var fieldTypeDef = this._fieldTypeListBox.get_value();
            if ((typeof (existingFieldDef.FieldDefinition) == "undefined") || (existingFieldDef.FieldDefinition == null) || (existingFieldDef.FieldDefinition.Id != fieldTypeDef)) {
                existingFieldDef.FieldDefinition = fieldDef;
                fieldUpdated = true;
            }
            existingFieldDef.IsModified = fieldUpdated;

            if (typeof (existingFieldDef.IsRequired) == "undefined")
                existingFieldDef.IsRequired = false;

            for (prop in existingFieldDef) {
                if (typeof (existingFieldDef[prop]) == "undefined")
                    existingFieldDef[prop] = null;
            }
        }
        return true;
    },

    _fieldsListSelectionChanged: function () {
        /// <summary>Handles a click event on the dynamic fields list: loads the selected field.</summary>
        /// <returns>Void.</returns>
        var collected = true;

        if (this._previouslySelectedIndex != -1)
            collected = this._tryCollectCurFieldChanges(true);

        if (collected) {
            this._previouslySelectedIndex = this._fieldsList.selectedIndex;
            var field = this._getFieldIfAlreadyExists(this._fieldsList.options[this._fieldsList.selectedIndex].text);
            if ((field != null) && (this._currentlyEditedField != field.Name)) {
                this._currentlyEditedField = field.Name;
                this._loadField(field);
            }
        }
        else {
            this._fieldsList.selectedIndex = this._previouslySelectedIndex;
        }
        this._fieldsList.focus();
    },

    _loadField: function (dynamicTypeField) {
        /// <summary>Loads a dynamic type field into the input fields.</summary>
        /// <param name="dynamicTypeField">A DynamicTypeField object to load.</param>
        /// <returns>Void.</returns> 
        this._suppressValidationChecks = true;
        this._clearFields();
        this._fieldTitleTextBox.set_value(dynamicTypeField.Title);
        this._fieldNameTextBox.set_value(dynamicTypeField.Name);
        this._fieldTypeListBox.findItemByValue(dynamicTypeField.ClrType).select();

        if ((dynamicTypeField.DBType != null) && (dynamicTypeField.DBType != ""))
            this._fieldDbTypeListBox.findItemByValue(dynamicTypeField.DBType).select();

        if ((dynamicTypeField.SQLDBType != null) && (dynamicTypeField.SQLDBType != ""))
            this._fieldSQLDbTypeListBox.findItemByValue(dynamicTypeField.SQLDBType).select();

        this._maxLengthTextBox.set_value(dynamicTypeField.MaxLength);

        if (dynamicTypeField.FieldDefinition.SupportsDefaultValueText) {
            this._defaultValueTextBox.set_value(dynamicTypeField.DefaultValue);
            this._defaultValueCheckBox.checked = false;
        }
        else if (dynamicTypeField.FieldDefinition.SupportsDefaultValueBool) {
            this._defaultValueTextBox.set_value("");
            this._defaultValueCheckBox.checked = dynamicTypeField.DefaultValue;
        }
        else {
            this._defaultValueTextBox.set_value("");
            this._defaultValueCheckBox.checked = false;
        }
        if (dynamicTypeField.FieldDefinition.SupportsPrecision) {
            this._precisionTextBox.set_value(dynamicTypeField.Precision);
        }
        if (dynamicTypeField.FieldDefinition.SupportsTaxonomyFields) {
            this._selectTaxonomy(dynamicTypeField.TaxonomyProviderName, dynamicTypeField.TaxonomyId);
            this._allowMultipleTaxonsCheckBox.checked = dynamicTypeField.AllowMultipleTaxons;
        }
        this._suppressValidationChecks = false;
    },

    _selectFirstOption: function () {
        if (this._loaded) {
            if (this._fieldsList.options.length > 0) {
                this._fieldsList.options[0].selected = true;
                this._bSuppressDataCollection = true;
                this._fieldsListSelectionChanged();
            }
        }
    },

    _clearFields: function () {
        /// <summary>Clears the input fields.</summary>
        /// <returns>Void.</returns> 
        this._fieldTitleTextBox.set_value("");
        this._fieldNameTextBox.set_value("");
        this._maxLengthTextBox.set_value("");
        this._defaultValueTextBox.set_value("");
        this._defaultValueCheckBox.checked = false;
        this._allowMultipleTaxonsCheckBox.checked = false;
        this._precisionTextBox.set_value("");
        this._fieldTypeListBox.set_selectedIndex(0);
        this._fieldTypeChanged();
    },

    _getFieldInListByName: function (fieldName) {
        /// <summary>Retrieves the option element matching to a specific field name.</summary>
        /// <param name="fieldName">The field name to retrieve.</param>
        /// <returns>The option element matching to a specific field name.</returns> 
        var option = null;
        for (var i = this._fieldsList.options.length - 1; i >= 0; i--) {
            if (this._fieldsList.options[i].text == fieldName) {
                option = this._fieldsList.options[i];
            }
        }
        return option;
    },

    _deleteDataFieldButtonClick: function () {
        /// <summary>Handles the click event on the Delete Data field linkbutton: Deletes a selected dynamic field.</summary>
        /// <returns>Void.</returns> 
        var fieldName = "";
        var reselect = -1;
        for (var i = this._fieldsList.options.length - 1; i >= 0; i--) {
            if (this._fieldsList.options[i].selected) {
                fieldName = this._fieldsList.options[i].text;
                this._fieldsList.remove(i);
                this._dynamicTypeObject.splice(this._getFieldIndexIfAlreadyExists(fieldName), 1);
                reselect = i - 1;
            }
        }
        if ((reselect == -1) && (this._fieldsList.options.length > 0))
            reselect = 0;
        if (this._fieldsList.options.length > 0) {
            if (reselect >= 0) {
                this._fieldsList.options[reselect].selected = true;
                this._bSuppressDataCollection = true;
                this._fieldsListSelectionChanged();
            }
        }
        else {
            this._clearFields();
        }
    }
};
Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor.registerClass('Telerik.Sitefinity.Web.UI.Backend.MetaTypeEditor', Sys.UI.Control);