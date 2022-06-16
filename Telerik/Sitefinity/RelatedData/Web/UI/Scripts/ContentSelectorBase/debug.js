﻿define([], function () {
    function ContentSelectorBase(options) {
        var that = this;
        this.parentElement = options.parentElement;
        this.culture = options.culture;
        this.itemType = options.itemType;
        this.createItemUrl = options.createItemUrl;
        this.viewModel = null;
        this.isMultisite = null;
        this.enableMultilingualSearch = options.enableMultilingualSearch;

        //The extended view model
        this.viewModelObjExt = {};
        this.viewModelObj = {
            onSearch: function (e) {
                if (that._timerId != null) {
                    clearTimeout(that._timerId);
                }
                that._timerId = setTimeout(function () {
                    that.rebind();
                    if (that.isSearchRequest()) {
                        that.viewModel.set('clearFilterVisible', true);
                    } else {
                        that.viewModel.set('clearFilterVisible', false);
                    }
                }, that.inputDelay);
            },
            onClearFilter: function (e) {
                this.set('searchValue', '');
                this.set('clearFilterVisible', false);
                this.contentDataSource.read();
            },
            isMultilingual: false,
            languages: "",
            defaultLanguage: "",
            clearFilterVisible: false,
            searchValue: null,
            allowMultipleSelection: options.allowMultipleSelection || false,
            removeFromDataOnUnselect: options.removeFromDataOnUnselect || false
        };

        this.selectedDataItems = [];
        this.preselectedDataItemIds = [];
        this.dataTemplate = null;
        this.serviceUrl = null;
        this.siteBaseUrl = "";
        this.initialized = false;
        this.providerName = null;

        this.timerId = null;
        this.inputDelay = 500;
        return (this);
    }

    ContentSelectorBase.prototype = {

        init: function () {
            this.viewModelObjExt = $.extend(Object.create(this.viewModelObj), this.viewModelObjExt);
            this.viewModel = kendo.observable(this.viewModelObjExt);
            kendo.bind(this.parentElement, this.viewModel);
        },

        getSelectedDataItems: function () {
            return this.selectedDataItems;
        },

        setSelectedDataItems: function(value) {
            this.selectedDataItems = value;
        },

        getPreselectedDataItemIds: function () {
            return this.preselectedDataItemIds;
        },

        setPreselectedDataItemIds: function (value) {
            this.preselectedDataItemIds = value;
        },

        parmeterMapFunc: function () {
            return {};
        },

        resolveUrlFunc: function() {
            return "";
        },

        onRequestEnd: function (e) { },

        rebind: function() {
            this.viewModel.contentDataSource.page(1);
        },

        setProviderName: function(value) {
            this.providerName = value;
            this.rebind();
        },

        isSearchRequest: function () {
            var searchValue = this.viewModel.searchValue;
            if (searchValue && searchValue != "") {
                return true;
            }
            return false;
        },

        markDataItemsAsSelected: function (dataItems, isCreateMode) {
            if (dataItems && !isCreateMode) {
                for (var i = 0; i < dataItems.length; i++) {
                    if (dataItems[i].IsRelated) {
                        this.addDataItemToSelected(dataItems[i]);
                    } else {
                        this.removeDataItemFromSelected(dataItems[i]);
                    }
                }
            }
        },

        addDataItemToSelected: function (dataItem) {
            if (this.isDataItemSelected(dataItem.Id) == -1) {
                this.selectedDataItems.push(dataItem);
            }
        },

        removeDataItemFromSelected: function (dataItem) {
            var dataItemPosition = this.isDataItemSelected(dataItem.Id);
            if (dataItemPosition >= 0) {
                this.selectedDataItems.splice(dataItemPosition, 1);
            }
            var dataItemPreselectedPosition = this.isDataItemPreselected(dataItem.Id);
            if (dataItemPreselectedPosition >= 0) {
                this.preselectedDataItemIds.splice(dataItemPreselectedPosition, 1);
            }
        },

        isDataItemSelected: function (dataItemId) {
            for (var i = 0; i < this.selectedDataItems.length; i++) {
                if (this.selectedDataItems[i].Id == dataItemId) {
                    return i;
                }
            }
            return -1;
        },

        isDataItemPreselected: function (dataItemId) {
            for (var i = 0; i < this.preselectedDataItemIds.length; i++) {
                if (this.preselectedDataItemIds[i] == dataItemId) {
                    return i;
                }
            }
            return -1;
        },

        isDataItemTranslatedForCurrentCulture: function (dataItem) {
            if (this.isMultilingual()) {
                if (dataItem.AvailableLanguages) {
                    for (var j = 0, length = dataItem.AvailableLanguages.length; j < length; j++) {
                        if (dataItem.AvailableLanguages[j] == this.culture) {
                            return true;
                        }
                    }
                } else {
                    return true;
                }
                
            }
            return false;
        },

        markDataItemsTranslatedForCurrentCulture: function (dataItems) {
            if (dataItems && this.isMultilingual()) {
                for (var i = 0; i < dataItems.length; i++) {
                    if (this.isDataItemTranslatedForCurrentCulture(dataItems[i])) {
                        dataItems[i].IsTranslated = true;
                    } else {
                        dataItems[i].IsTranslated = false;
                    }
                }
            }
        },

        getServiceUrl: function () {
            return this.siteBaseUrl + this.serviceUrl;
        },

        isMultilingual: function () {
            return this.viewModel.isMultilingual;
        },

        getLanguages: function () {
            return this.viewModel.languages.split(';');
        },

        generateFilterExpression: function (field, query) {
            if (!this.isMultilingual() || !this.enableMultilingualSearch) {
                return String.format("{0}.ToUpper().Contains(\"{1}\".ToUpper())", field, query);
            }
            if (this.viewModel.defaultLanguage !== this.culture) {
                return String.format("{0}.ToUpper().Contains(\"{1}\".ToUpper())", field, query);
            }
            var filterExpression = String.format("({0}.ToUpper().Contains(\"{1}\".ToUpper()) OR (", field, query);
            var languages = this.getLanguages();
            for (var i = 0; i < languages.length; i++) {
                filterExpression += String.format("{0}[\"{1}\"] = null AND ", field, languages[i]);
            }
            filterExpression += String.format("{0}[\"\"] != null AND ", field);
            filterExpression += String.format("{0}[\"\"].ToUpper().Contains(\"{1}\".ToUpper())))", field, query);
            return filterExpression;
        }
    };

    return (ContentSelectorBase);
});