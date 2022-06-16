define([], function () {
    function ContentSelectorBase(options, html) {
        var that = this;
        this.template = html;
        this.parentElement = options.parentElement;
        this.culture = options.culture;
        this.viewModel = null;
        this.isMultisite = null;

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
            isMultilingual: "False",
            languages: "",
            defaultLanguage: "",
            clearFilterVisible: false,
            searchValue: null
        };

        this.allowMultipleSelection = false;
        this.selectedDataItems = [];
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

        parmeterMapFunc: function () {
            return {};
        },

        resolveUrlFunc: function() {
            return "";
        },

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
        },

        isDataItemSelected: function (dataItemId) {
            for (var i = 0; i < this.selectedDataItems.length; i++) {
                if (this.selectedDataItems[i].Id == dataItemId) {
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
            return Boolean.parse(this.viewModel.isMultilingual);
        },

        getLanguages: function () {
            return this.viewModel.languages.split(';');
        },

        generateFilterExpression: function (field, query) {

            if (!this.isMultilingual()) {
                return String.format("{0}.Contains(\"{1}\")", field, query);
            }

            if (this.viewModel.defaultLanguage !== this.culture) {
                return String.format("{0}.Contains(\"{1}\")", field, query);
            }

            var filterExpression = String.format("({0}.Contains(\"{1}\") OR (", field, query);
            var languages = this.getLanguages();
            for (var i = 0; i < languages.length; i++) {
                filterExpression += String.format("{0}[\"{1}\"] = null AND ", field, languages[i]);
            }
            filterExpression += String.format("{0}[\"\"] != null AND ", field);
            filterExpression += String.format("{0}[\"\"].Contains(\"{1}\")))", field, query);
            return filterExpression;
        }
    };

    return (ContentSelectorBase);
});