﻿Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI");
/////////////////////////////////////////////////////////////////////////////////////////////////
// Mapping
/////////////////////////////////////////////////////////////////////////////////////////////////
Telerik.Sitefinity.Publishing.Web.UI.Mapping = function (id) {
    Telerik.Sitefinity.Publishing.Web.UI.Mapping.initializeBase(this, [id]);

    this._titleElem = null;
    this._templateElem = null;
    this._targetElem = null;
    this._sourceFields = [];
    this._destFields = [];
    this._domEvents = [];
    this._isDataboundAtLeastOnce = false;
};
Telerik.Sitefinity.Publishing.Web.UI.Mapping.prototype = {
    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Construction & destruction          
    ////////////////////////////////////////////////////////////////////////////////////////////////
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Mapping.callBaseMethod(this, "initialize");

        this._titleElem = $get(this._titleElem);
        this._templateElem = $get(this._templateElem);
        this._targetElem = $get(this._targetElem);

        this._addSourceFieldToDestinationFieldDelegate = Function.createDelegate(this, this._addSourceFieldToDestinationField);
        this._removeSourceFieldFromDestinationFieldDelegate = Function.createDelegate(this, this._removeSourceFieldFromDestinationField);
        this._addFirstSourceFieldToDestinationFieldDelegate = Function.createDelegate(this, this._addFirstSourceFieldToDestinationField);
        this._selectedSourceFieldIndexChangedDelegate = Function.createDelegate(this, this._selectedSourceFieldIndexChanged);
    },
    dispose: function () {
        this._clearEvents();
        this._clearDomElementContents(this._targetElem);

        delete this._addSourceFieldToDestinationFieldDelegate;
        delete this._removeSourceFieldFromDestinationFieldDelegate;
        delete this._addFirstSourceFieldToDestinationField;
        delete this._selectedSourceFieldIndexChangedDelegate;

        // Make certain there are no memory leaks
        delete this._titleElem;
        delete this._templateElem;
        delete this._targetElem;
        Telerik.Sitefinity.Publishing.Web.UI.Mapping.callBaseMethod(this, "dispose");
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // PUBLIC METHODS            
    ////////////////////////////////////////////////////////////////////////////////////////////////

    dataBind: function (destFields, sourceFields, mappingSettings) {
        if (typeof destFields == "undefined" || destFields == null
            || typeof sourceFields == "undefined" || sourceFields == null) {
            return;
        }
        this._sourceFields = this._addKeysToFieldsArray(this._getVisibleFields(sourceFields));
        this._destFields = this._addKeysToFieldsArray(this._getVisibleFields(destFields));
        //this._mergeMappingWithDestFields(this._destFields, mappingSettings);
        this._storeSettings(mappingSettings);
        this._originalMapping = mappingSettings;
        if (typeof mappingSettings != "undefined" && mappingSettings != null) {
            this._mappingSettings = Telerik.Sitefinity.cloneObject(mappingSettings);
        }
        else {
            this._mappingSettings = { Mappings: [] };
        }

        // Extract templates

        var optionTemplate = this._getOptionTemplate();
        var sourceFieldTemplate = this._getSourceFieldItemTemplate();

        // Remove any previous binding...   
        this._clearDomElementContents(this._targetElem);
        this._clearEvents();

        // Data bind
        var container = this._getDestinationFieldsContainerTemplate().cloneNode(true);
        this._targetElem.appendChild(container);
        this._clearDomElementContents(container);
        var destinationFieldTemplate = this._getDestinationFieldItemTemplate();

        // not Arrray and has no length
        for (var idx in this._destFields) {
            if (!isNaN(parseInt(idx))) {
                var destinationFieldElem = destinationFieldTemplate.cloneNode(true);
                this._bindDestinationField(this._destFields[idx], destinationFieldElem, optionTemplate, sourceFieldTemplate);
                container.appendChild(destinationFieldElem);
            }
        }

        // these are constantly copied, so we need to make sure there are no references to them
        delete optionTemplate;
        delete sourceFieldTemplate;

        this._isDataboundAtLeastOnce = true;
    },

    get_originalMappingSettings: function () {
        var restored = this._restoreSettings();
        return restored;
    },

    get_mappingSettings: function () {
        if (!this._isDataboundAtLeastOnce) {
            return null;
        }
        return this._mappingSettings;
    },

    get_isDataBoundAtLeastOnce: function () {
        return this._isDataboundAtLeastOnce;
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Binding helpers                
    ////////////////////////////////////////////////////////////////////////////////////////////////

    _bindDestinationField: function (destField, destinationFieldElem, optionTemplate, sourceFieldTemplate) {
        jQuery(destinationFieldElem).find(".sfDestinationFieldTitle").text(destField.Title || destField.Name);

        var sourceFieldsContainerElem = jQuery(destinationFieldElem).find(".sfSourceFieldsContainer").get(0);
        this._clearDomElementContents(sourceFieldsContainerElem);
        jQuery(sourceFieldsContainerElem).data("destField", destField);

        var noItemsElement = this._getNotItemsTemplate().cloneNode(true);
        this._addEventHandler(noItemsElement, "click", this._addFirstSourceFieldToDestinationFieldDelegate);
        sourceFieldsContainerElem.appendChild(noItemsElement);

        // not Arrray and has no length
        var totalSourceFields = 0;
        var sourceFieldNames = this._getDestinationSourceFields(destField.Name);
        var sourceFieldIndex;
        for (var relatedSourceFieldIdx in sourceFieldNames) {
            sourceFieldIndex = parseInt(relatedSourceFieldIdx);
            if (!isNaN(sourceFieldIndex)) {
                var sourceFieldElem = sourceFieldTemplate.cloneNode(true);
                var sourceFieldName = sourceFieldNames[relatedSourceFieldIdx];
                this._bindSourceField(destField, sourceFieldName, sourceFieldElem, optionTemplate, sourceFieldIndex);
                sourceFieldsContainerElem.appendChild(sourceFieldElem);
                totalSourceFields++;
            }
        }
        if (totalSourceFields == 0) {
            var jNoElements = jQuery(sourceFieldsContainerElem).find(".sfNoSourceFieldsTemplate");
            if (jNoElements.hasClass("sfDisplayNone"))
                jNoElements.removeClass("sfDisplayNone");
        }

        jQuery(destinationFieldElem).find(".sfTitleContainer").after(sourceFieldsContainerElem);
    },

    _bindSourceField: function (destField, sourceFieldName, sourceFieldElem, optionTemplate, sourceFieldIndex) {
        jQuery(sourceFieldElem).data("destField", destField);

        var addItemButton = jQuery(sourceFieldElem).find(".sfAddSourceField").get(0);
        var removeItemButton = jQuery(sourceFieldElem).find(".sfRemoveSourceField").get(0);

        this._addEventHandler(addItemButton, "click", this._addSourceFieldToDestinationFieldDelegate);
        this._addEventHandler(removeItemButton, "click", this._removeSourceFieldFromDestinationFieldDelegate);

        var selectTarget = jQuery(sourceFieldElem).find(".sfAllSourceFieldNames").get(0);
        while (selectTarget.length > 0)
            selectTarget.remove(0);

        this._addEventHandler(selectTarget, "change", this._selectedSourceFieldIndexChangedDelegate);
        //this._clearDomElementContents(selectTarget);

        this._bindSourceFieldsSelect(sourceFieldName, optionTemplate, selectTarget, sourceFieldIndex);
    },

    _bindSourceFieldsSelect: function (selectedFieldName, optionTemplate, selectElem, sourceFieldIndex) {

        var selectedIndex = -1;
        // not Arrray and has no length
        var idx;
        var jSelectElem = jQuery(selectElem);
        for (var i in this._sourceFields) {
            idx = parseInt(i);
            if (!isNaN(idx)) {
                var sourceField = this._sourceFields[i];
                if (selectedFieldName == sourceField.Name) {
                    selectedIndex = idx;
                    jSelectElem.data("sourceField", sourceField);
                    jSelectElem.data("sourceFieldIndex", sourceFieldIndex);
                }
                var option = this._createSourceFieldOption(sourceField, optionTemplate);
                selectElem.appendChild(option);
            }
        }

        selectElem.selectedIndex = selectedIndex;

    },

    _createSourceFieldOption: function (field, template) {
        var clone = template.cloneNode(true);
        var jClone = jQuery(clone);
        jClone.text(field.Title || field.Name);
        return clone;
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // DOM helpers                
    ////////////////////////////////////////////////////////////////////////////////////////////////

    _clearDomElementContents: function (domElement) {
        jQuery(domElement).empty();
    },

    _addEventHandler: function (domElement, domEventName, handler) {
        Sys.UI.DomEvent.addHandler(domElement, domEventName, handler);
        this._domEvents.push({ "Element": domElement, "EventName": domEventName, "Handler": handler });
    },

    _clearEvents: function () {
        var idx = this._domEvents.length;
        while (idx--) {
            var evt = this._domEvents[idx];
            Sys.UI.DomEvent.removeHandler(evt.Element, evt.EventName, evt.Handler);
        }
        this._domEvents = [];
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Templates and filters
    ////////////////////////////////////////////////////////////////////////////////////////////////

    _getDestinationFieldsContainerTemplate: function () {
        var template = jQuery(this._templateElem).find(".sfDestinationFieldsContainer").get(0);
        return template;
    },

    _getDestinationFieldItemTemplate: function () {
        var template = jQuery(this._templateElem).find(".sfDestinationFieldItem").get(0);
        return template;
    },

    _getNotItemsTemplate: function () {
        var template = jQuery(this._templateElem).find(".sfNoSourceFieldsTemplate").get(0);
        return template;
    },

    _getOptionTemplate: function () {
        // Get <option> template and remove it from the general template
        var selectTemplate = jQuery(this._templateElem).find(".sfAllSourceFieldNames").get(0);
        // clone node
        var optionTemplate = selectTemplate.children[0]; //.cloneNode(true);
        return optionTemplate;
    },

    _getSourceFieldItemTemplate: function () {
        // Get destination field template and remove it from the general template                                    
        var sourceFieldItem = jQuery(this._templateElem).find(".sfSourceFieldItem").get(0); //.cloneNode(true);
        return sourceFieldItem;
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Event handlers          
    ////////////////////////////////////////////////////////////////////////////////////////////////

    _addSourceFieldToDestinationField: function (domEvent) {
        var sourceFieldElem = jQuery(domEvent.target).parents(".sfSourceFieldItem").get(0);
        var destField = jQuery(sourceFieldElem).data("destField");
        var sourceField = this._getDefaultSourceField(destField.Name);
        var mapping = this._getDestinationMapping(destField.Name);
        var jSelect = jQuery(sourceFieldElem).find(".sfAllSourceFieldNames").eq(0)
        var idx = jSelect.data("sourceFieldIndex");
        if (sourceField == null || mapping == null)
            return;

        // add to data
        Array.insert(mapping.SourcePropertyNames, idx + 1, sourceField.Name);

        // add to UI
        var optionTemplate = this._getOptionTemplate();
        var newSourceFieldElement = this._getSourceFieldItemTemplate().cloneNode(true);

        this._bindSourceField(destField, sourceField.Name, newSourceFieldElement, optionTemplate, idx + 1);
        jQuery(sourceFieldElem).after(newSourceFieldElement);
        this._reorderAfter(jQuery(newSourceFieldElement).find(".sfAllSourceFieldNames").eq(0));
        dialogBase.resizeToContent();
    },

    _removeSourceFieldFromDestinationField: function (domEvent) {       
        var jSourceFieldsContainer = jQuery(domEvent.target).parents(".sfSourceFieldsContainer");
        var sourceFieldsContainerElem = jSourceFieldsContainer.get(0);
        var sourceFieldElem = jQuery(domEvent.target).parents(".sfSourceFieldItem").get(0);
        var jSelect = jQuery(sourceFieldElem).find(".sfAllSourceFieldNames").eq(0)
        var idx = jSelect.data("sourceFieldIndex");
        //var jPrevSelect = jSelect.prev().find(".sfAllSourceFieldNames").eq(0);
        var destField = jQuery(sourceFieldElem).data("destField");
        var mapping = this._getDestinationMapping(destField.Name);

        // remove from data
        mapping.SourcePropertyNames.splice(idx, 1);

        // remove from UI
        sourceFieldsContainerElem.removeChild(sourceFieldElem);
        if (sourceFieldsContainerElem.children.length == 1) {
            var jNoElements = jSourceFieldsContainer.find(".sfNoSourceFieldsTemplate");
            if (jNoElements.hasClass("sfDisplayNone"))
                jNoElements.removeClass("sfDisplayNone");
        }

        this._reorder(jSourceFieldsContainer);

        dialogBase.resizeToContent();
    },

    _addFirstSourceFieldToDestinationField: function (domEvent) {
        if (!Sys.UI.DomElement.containsCssClass(domEvent.target, "sfAddFirstSourceField")) {
            return;
        }
        var jSourceFieldsContainer = jQuery(domEvent.target);
        while (!jSourceFieldsContainer.hasClass("sfSourceFieldsContainer")) {
            jSourceFieldsContainer = jSourceFieldsContainer.parent();
        }
        var jDestinationFieldsContainer = jSourceFieldsContainer;
        while (!jDestinationFieldsContainer.hasClass("sfDestinationFieldsContainer")) {
            jDestinationFieldsContainer = jDestinationFieldsContainer.parent();
        }
        var sourceFieldContainerElem = jSourceFieldsContainer.get(0);
        var destField = jSourceFieldsContainer.data("destField");
        var mapping = this._getDestinationMapping(destField.Name);
        var sourceField = this._getDefaultSourceField(destField.Name);
        if (sourceField == null)
            return;

        if (mapping == null) {
            mapping = this._createDefaultMapping(destField.Name);
            this._mappingSettings.Mappings.push(mapping);
        }

        // add to data
        mapping.SourcePropertyNames.push(sourceField.Name);

        // add to UI
        var optionTemplate = this._getOptionTemplate();
        var newSourceFieldElement = this._getSourceFieldItemTemplate().cloneNode(true);
        this._bindSourceField(destField, sourceField.Name, newSourceFieldElement, optionTemplate, 0);
        sourceFieldContainerElem.appendChild(newSourceFieldElement);

        var jNoElements = jQuery(domEvent.target).parents(".sfNoSourceFieldsTemplate");
        if (!jNoElements.hasClass("sfDisplayNone"))
            jNoElements.addClass("sfDisplayNone");
        // no need to reorder since there is nothing after the first element
    },

    _selectedSourceFieldIndexChanged: function (domEvent) {
        var jSourceFieldsContainer = jQuery(domEvent.target).parents(".sfSourceFieldsContainer");
        var sourceFieldsSelectElem = domEvent.target;

        var destField = jSourceFieldsContainer.data("destField");
        var mapping = this._getDestinationMapping(destField.Name);
        var sourceField = this._sourceFields[sourceFieldsSelectElem.selectedIndex];

        // modify data
        var idx = jQuery(sourceFieldsSelectElem).data("sourceFieldIndex");
        mapping.SourcePropertyNames.splice(idx, 1, sourceField.Name)
        jQuery(sourceFieldsSelectElem).data("sourceField", sourceField);

        // UI is not modified
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Misc. helpers       
    ////////////////////////////////////////////////////////////////////////////////////////////////                    

    _reorder: function (jSourceFieldsContainer, startAfter) {
        if (!jSourceFieldsContainer.hasClass("sfSourceFieldsContainer")) {
            return;
        }
        if (typeof startAfter != "number") {
            startAfter = -1;
        }
        //insertAfter = Math.max(-1, Math.min(insertAfter, jSourceFieldsContainer.length - 1));
        var jSelects = jSourceFieldsContainer.find(".sfAllSourceFieldNames");
        var len = jSelects.length;
        for (var i = startAfter + 1; i < len; i++) {
            jQuery(jSelects[i]).data("sourceFieldIndex", i);
        }
    },

    _reorderAfter: function (jSelect) {
        if (!jSelect.hasClass("sfAllSourceFieldNames")) {
            return;
        }
        var jSourceFieldsContainer = jSelect;
        while (!jSourceFieldsContainer.hasClass("sfSourceFieldsContainer")) {
            jSourceFieldsContainer = jSourceFieldsContainer.parent();
        }
        var insertAfter = jSelect.data("sourceFieldIndex");
        this._reorder(jSourceFieldsContainer, insertAfter);
    },

    _storeSettings: function (settings) {
        if (typeof settings != "undefined" && settings != null) {
            this._storedSettings = Sys.Serialization.JavaScriptSerializer.serialize(settings);
        }
        else {
            this._storedSettings = "";
        }
    },

    _restoreSettings: function () {
        if (this._isDataboundAtLeastOnce && typeof this._storedSettings == "string") {
            var restored = Sys.Serialization.JavaScriptSerializer.deserialize(this._storedSettings);
            var mappings = restored.Mappings;
            mappings = Telerik.Sitefinity.fixArray(mappings);
            var iter = mappings.length;
            while (iter--) {
                var mapping = mappings[iter];
                mapping.SourcePropertyNames = Telerik.Sitefinity.fixArray(mapping.SourcePropertyNames);
                mapping.Translations = Telerik.Sitefinity.fixArray(mapping.Translations);
            }
            restored.Mappings = mappings;
            return restored;
        }
        else {
            var undef;
            return undef;
        }
    },

    _createDefaultMapping: function (destFieldName) {
        var mapping = new Object();
        mapping.DestinationPropertyName = destFieldName;
        mapping.SourcePropertyNames = [];
        mapping.Translations = [];
        mapping.DefaultValue = null;
        mapping.IsRequired = true;
        return mapping;
    },

    _getDestinationMapping: function (destFieldName) {
        for (var idx in this._mappingSettings.Mappings) {
            if (!isNaN(parseInt(idx))) {
                var mapping = this._mappingSettings.Mappings[idx];
                if (mapping.DestinationPropertyName == destFieldName) {
                    return mapping;
                }
            }
        }
        return null;
    },

    _getDestinationSourceFields: function (destFieldName) {
        var mapping = this._getDestinationMapping(destFieldName);
        if (mapping != null) {
            var namesToRemove = [];
            for (var idx in mapping.SourcePropertyNames) {
                if (!isNaN(parseInt(idx))) {
                    var name = mapping.SourcePropertyNames[idx];
                    if (this._getSourceFieldByName(name) == null) {
                        namesToRemove.push(name);
                    }
                }
            }
            for (var i = 0; i < namesToRemove.length; i++) {
                Array.remove(mapping.SourcePropertyNames, namesToRemove[i]);
            }
            return mapping.SourcePropertyNames;
        }
        else
            return [];
    },

    _getSourceFieldByName: function (name) {
        if (typeof this._sourceFields.AllKeys != "undefined") {
            return this._sourceFields[this._sourceFields.AllKeys[name]];
        }
        else {
            var idx = this._sourceFields.length;
            var currentField;
            while (idx--) {
                currentField = this._sourceFields[idx];
                if (currentField.Name == name) {
                    return currentField;
                }
            }
            return null;
        }
    },

    _getMappingForDestinationField: function (destFieldName, mappingSettings) {
        if (typeof mappingSettings == "undefined" || mappingSettings == null) {
            mappingSettings = Telerik.Sitefinity.cloneObject(this._originalMapping);
        }
        var idx = mappingSettings.Mappings.length;
        while (idx--) {
            var mapping = mappingSettings.Mappings[idx];
            if (mapping.DestinationPropertyName == destFieldName)
                return mapping;
        }
        return null;
    },

    _getDefaultSourceField: function (/*destinationFieldName*/) {
        // destinationFieldName is ignored for now
        // means: get default source field for this destination field
        if (this._sourceFields.length > 0)
            return this._sourceFields[0];
        else
            return null;
    },

    _addKeysToFieldsArray: function (arrayOfFields) {
        // arrayOfFields is not Arrray and has no length
        var allKeys = {};
        var length = 0;
        for (var idx in arrayOfFields) {
            if (!isNaN(parseInt(idx))) {
                allKeys[arrayOfFields[idx].Name] = idx;
                length++;
            }
        }
        arrayOfFields.AllKeys = allKeys;
        arrayOfFields.length = length;

        return arrayOfFields;
    },

    _getVisibleFields: function (fields) {
        var result = [];
        for (var idx in fields) {
            if (!isNaN(parseInt(idx))) {
                var fld = fields[idx];
                if (!this._fieldShouldBeHidden(fld)) {
                    result.push(Telerik.Sitefinity.cloneObject(fld));
                }
            }
        }
        return result;
    },

    _fieldShouldBeHidden: function (field) {
        var hideInUI = field.HideInUI;
        if (typeof hideInUI == "boolean") {
            return hideInUI;
        }
        else if (typeof hideInUI == "string") {
            try { return Boolean.parse(hideInUI); }
            catch (e) { return false; }
        }
        else {
            return false;
        }
    },

    ////////////////////////////////////////////////////////////////////////////////////////////////
    // Properties    
    ////////////////////////////////////////////////////////////////////////////////////////////////     

    get_title: function () {
        return jQuery(this._titleElem).text();
    },
    set_title: function (val) {
        jQuery(this._titleElem).text(val);
    }
};
Telerik.Sitefinity.Publishing.Web.UI.Mapping.registerClass("Telerik.Sitefinity.Publishing.Web.UI.Mapping", Sys.UI.Control);

////////////////////////////////////////////////////////////////////////////////////////////////
// Global Functions
////////////////////////////////////////////////////////////////////////////////////////////////

function Telerik$Sitefinity$Publishing$Web$UI$Mapping$fixMappingSettings(settings) {
    var mappings = settings.Mappings;
    mappings = Telerik.Sitefinity.fixArray(mappings);
    var iter = mappings.length;
    while (iter--) {
        var mapping = mappings[iter];
        mapping.SourcePropertyNames = Telerik.Sitefinity.fixArray(mapping.SourcePropertyNames);
        mapping.Translations = Telerik.Sitefinity.fixArray(mapping.Translations);
    }
    settings.Mappings = mappings;
    return settings;
}

Telerik.Sitefinity.Publishing.Web.UI.Mapping.fixMappingSettings = Telerik$Sitefinity$Publishing$Web$UI$Mapping$fixMappingSettings;