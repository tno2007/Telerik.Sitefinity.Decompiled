define(["EditableWindowField", "HierarchicalTaxonSelector"], function (EditableWindowField, HierarchicalTaxonSelector) {
    function HierarchicalTaxonField(options) {
        var that = this;
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.fieldType = "HierarchicalTaxon";
        this.taxonSelector = new HierarchicalTaxonSelector({ parentElement: null, culture: this.contentRepository.culture });
        this.editWindowContentTemplate = "";
        this.originalValue = null;

        return (this);
    }

    HierarchicalTaxonField.prototype = {

        success: function (data, textStatus, jqXHR) {
            this.originalValue = [].concat(data.Taxons);
            
            this.dialog.viewModel.set('titleText', "Select " + data.TaxonomyName);
            this.taxonSelector.taxonomyId = data.TaxonomyId;
            this.taxonSelector.siteBaseUrl = this.siteBaseUrl;
            this.taxonSelector.allowMultipleSelection = data.AllowMultipleSelection;
            this.taxonSelector.parentElement = this.dialog.getContentPlaceHolder();
            this.taxonSelector.init();
            
            this.taxonSelector.setSelectedDataItems(data.Taxons);
            this.viewModel.set("showEditButton", true);

            this.isInitialized = true;
        },

        onDone: function (event, sender) {
            var strippedDataItems = [];
            var dataItems = this.taxonSelector.getSelectedDataItems();
            this.value = [].concat(dataItems);
            for (var i = 0; i < dataItems.length; i++) {
                strippedDataItems.push({
                    Value: dataItems[i].Id,
                    Name: dataItems[i].Title
                });
            }

            this.saveTempParams.data = {
                Name: this.fieldName,
                ComplexValue: strippedDataItems
            }
            EditableWindowField.prototype.onDone.call(this, event, sender);
        },

        onClose: function (event, sender) {
            this.taxonSelector.setSelectedDataItems([].concat(this.originalValue));
            EditableWindowField.prototype.onClose.call(this, event, sender)
        },

        isChanged: function () {
            if (this.value) {
                return !this.value.compare(this.originalValue);
            }
            return false;
        },

        applyChanges: function () {
            this.originalValue = [].concat(this.value);
        }
    };

    HierarchicalTaxonField.prototype = $.extend(Object.create(EditableWindowField.prototype), HierarchicalTaxonField.prototype);
    return (HierarchicalTaxonField);
});

