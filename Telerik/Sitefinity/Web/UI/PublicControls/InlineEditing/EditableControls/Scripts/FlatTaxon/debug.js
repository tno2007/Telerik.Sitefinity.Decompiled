﻿
define(["EditableWindowField", "FlatTaxonSelector", "text!FlatTaxonTemplate!strip"], function (EditableWindowField, FlatTaxonSelector, html) {
    function FlatTaxonField(options) {
        var that = this;
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.fieldType = "FlatTaxon";
        this.editWindowContentTemplate = html;
        this.taxonServiceBaseUrl = that.siteBaseUrl + "Sitefinity/Services/Taxonomies/FlatTaxon.svc";
        this.taxonSelector = new FlatTaxonSelector({ parentElement: null, culture: this.contentRepository.culture });
        this.emptyGuid = "00000000-0000-0000-0000-000000000000";
        this.multiSelect = null;
        this.editWindowViewModel = kendo.observable({
            onAllTagsClick: function (e) {
                e.preventDefault();
                that.taxonSelector.setSelectedDataItems(this.selectedValues.slice(0));
                that.showTaxonSelectView();
                that.taxonSelector.rebind();
            },
            onItemSelect: function (e) {
                
                if (!that.taxonSelector.allowMultipleSelection && this.selectedValues.length >= 1) {
                    e.preventDefault();
                }
                var selectedItem = e.sender.dataItem(e.item.index());
                //newly typed item
                if (selectedItem.Id == that.emptyGuid) {
                    e.preventDefault();
                    e.sender.element.closest('.k-multiselect').find('input').val('');

                    var itemTitle = [selectedItem.Title];
                    that.contentRepository.ensureFlatTaxon(that.taxonomyId, itemTitle, function (data) {
                        var dataItem = data[0];
                        if (dataItem) {
                            that.editWindowViewModel.multiselectDataSource.remove(selectedItem);
                            that.editWindowViewModel.multiselectDataSource.add(dataItem);
                            that.editWindowViewModel.selectedValues.push(that.editWindowViewModel.multiselectDataSource.get(dataItem.Id));
                        }
                    });
                }
            },
            multiselectDataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        type: "GET",
                        dataType: "json",
                        url: function (options) {
                            return String.format("{0}/{1}/", that.taxonServiceBaseUrl, that.taxonomyId);
                        },
                        beforeSend: function(xhr) {
                            if (that.contentRepository.culture) {
                                xhr.setRequestHeader("SF_UI_CULTURE", that.contentRepository.culture);
                            }
                        }
                    },

                    parameterMap: function (options) {
                        var queryObj = {
                            skip: 0,
                            take: 10,
                            filter: function () {
                                var inputValue = that.getMultiselectValue();
                                if (inputValue) {
                                    return that.taxonSelector.generateFilterExpression("Title", that.getMultiselectValue());
                                }
                                return "";
                            },
                            sortExpression: "Title ASC"
                        }
                        return queryObj;
                    }
                },
                schema: {
                    data: "Items",
                    total: "TotalCount",
                    model: {
                        id: "Id"
                    }
                },
                requestEnd: function(e) {
                    var dataItems = e.response.Items;
                    var exists = function (id) {
                        var exists = false;
                        for (var i = 0; i < dataItems.length; i++) {
                            if (dataItems[i].Id == id) {
                                exists = true;
                                break;
                            }
                        }
                        return exists;
                    }
                    if (that.taxonSelector.isMultilingual()) {
                        that.taxonSelector.markDataItemsTranslatedForCurrentCulture(dataItems);
                        dataItems = jQuery.grep(dataItems, function (dataItem, index) {
                            return dataItem.IsTranslated;
                        });
                    }
                    var selectedValues = that.editWindowViewModel.selectedValues;
                    that.taxonSelector.markDataItemsTranslatedForCurrentCulture(selectedValues);
                    for (var i = 0; i < selectedValues.length; i++) {
                        if (!exists(selectedValues[i].Id)) {
                            dataItems.push(selectedValues[i]);
                        }
                    }
                    e.response.Items = dataItems;
                },
                change: function (e) {
                    //display first element on top of multiselect dropdown
                    var dataItems = e.items;
                    var inputValue = that.getMultiselectValue();
                    if (inputValue) {
                            
                        var found = false;
                        for (var i = 0; i < dataItems.length; i++) {
                            if (dataItems[i].Title == inputValue) {
                                found = true;
                                break;
                            }
                        }

                        if (!found) {
                            dataItems.splice(0, 0, { Id: that.emptyGuid, Title: inputValue });
                        }
                    }
                    
                },
                serverFiltering: true
            }),
            selectedValues: [],
            onMultiselectKeyUp: function (e) { },
            multiSelectKeyUp: null,
            taxonsSelectViewVisible: false,
            autoCompleteViewVisible: true,
            taxonomyName: null
        });

        return (this);
    }

    FlatTaxonField.prototype = {

        getMultiselectValue: function() {
            var event = this.editWindowViewModel.multiSelectKeyUp;
            if (event) {
                var sender = event.target || event.srcElement;
                if (sender) {
                    var value = $(sender).val();
                    if (value) {
                        return value;
                    } else {
                        return null;
                    }
                }
            }
            return null;
        },
        success: function (data, textStatus, jqXHR) {
            this.taxonomyId = data.TaxonomyId;
            this.editWindowViewModel.set('taxonomyName', data.TaxonomyName);
            this.originalValue = [].concat(data.Taxons);
            this.editWindowViewModel.set("selectedValues", [].concat(data.Taxons));
            
            kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
            this.dialog.viewModel.set('titleText', "Select " + data.TaxonomyName);
            this.taxonSelector.taxonomyId = data.TaxonomyId;
            this.taxonSelector.siteBaseUrl = this.siteBaseUrl;
            this.taxonSelector.allowMultipleSelection = data.AllowMultipleSelection;
            this.taxonSelector.parentElement = $(this.dialog.getContentPlaceHolder()).find('#taxonsSelectView').find(':first-child');
            //this.multiSelect = $(this.dialog.getContentPlaceHolder()).find('[data-role="multiselect"]');
            this.taxonSelector.init();

            this.taxonSelector.setSelectedDataItems(data.Taxons.slice(0));
            this.viewModel.set("showEditButton", true);
            this.isInitialized = true;
        },

        onDone: function (event, sender) {
            if (this.editWindowViewModel.taxonsSelectViewVisible) {
                this.hideTaxonSelectView();
                var selectedDataItems = this.taxonSelector.getSelectedDataItems();
                for (var i = 0; i < selectedDataItems.length; i++) {
                    var currentItem = selectedDataItems[i];
                    var exists = this.editWindowViewModel.multiselectDataSource.get(currentItem.Id);
                    if (!exists) {
                        this.editWindowViewModel.multiselectDataSource.add(currentItem);
                    }
                }
                this.editWindowViewModel.set("selectedValues", this.taxonSelector.getSelectedDataItems());
            } else {
                var strippedDataItems = [];
                var dataItems = this.editWindowViewModel.selectedValues;
                this.value = [];
                for (var i = 0; i < dataItems.length; i++) {
                    strippedDataItems.push({
                        Value: dataItems[i].Id,
                        Name: dataItems[i].Title
                    });
                    this.value.push({
                        Id: dataItems[i].Id,
                        Title: dataItems[i].Title
                    });
                }
                this.saveTempParams.data = {
                    Name: this.fieldName,
                    ComplexValue: strippedDataItems
                }
                this.editWindowSave(this);
                sender.close();
            }
        },

        onClose: function (event, sender) {
            if (this.editWindowViewModel.taxonsSelectViewVisible) {
                this.hideTaxonSelectView();
            } else {
                this.editWindowViewModel.set('selectedValues', [].concat(this.originalValue));
                sender.close();
            }
        },

        isChanged: function () {
            if (this.value) {
                return !this.value.compare(this.originalValue);
            }
            return false;
        },

        applyChanges: function () {
            this.originalValue = [].concat(this.value);
        },

        showTaxonSelectView: function () {
            this.editWindowViewModel.set("autoCompleteViewVisible", false);
            this.editWindowViewModel.set("taxonsSelectViewVisible", true);
        },

        hideTaxonSelectView: function () {
            this.editWindowViewModel.set("autoCompleteViewVisible", true);
            this.editWindowViewModel.set("taxonsSelectViewVisible", false);
        }
    };

    // workaround for an issue with https://github.com/telerik/kendo-ui-core/issues/812 in Kendo UI 2015 Q1 SP1 
    // http://dojo.telerik.com/iRoKO
    kendo.ui.MultiSelect.fn._preselect = function (data, value) {
        var that = this;

        if (!$.isArray(data) && !(data instanceof kendo.data.ObservableArray)) {
            data = [data];
        }

        if ($.isPlainObject(data[0]) || data[0] instanceof kendo.data.ObservableObject || !that.options.dataValueField) {
            that._retrieveData = true;
            that.dataSource.data(data);
            that.value(value || that._initialValues);
        }
    };

    FlatTaxonField.prototype = $.extend(Object.create(EditableWindowField.prototype), FlatTaxonField.prototype);
    return (FlatTaxonField);
});