Type.registerNamespace("Telerik.Sitefinity.Web.UI.Components");

/* BindeSearchBox class */

Telerik.Sitefinity.Web.UI.Components.BinderSearch = function (element) {
    Telerik.Sitefinity.Web.UI.Components.BinderSearch.initializeBase(this, [element]);
    this._binderId = null;
    this._binder = null;
    this._searchFieldsString = null;
    this._searchFields = null;
    this._extendedSearchFieldsString = null;
    this._extendedSearchFields = null;
    this._defaultLanguage = null;
    this._definedLanguages = [];
    this._multilingual = false;
    this._searchType = null;
    this._disableHighlighthing = null;
    this._searchProgressPanel = null;
    this._additionalFilterExpression = null;
    this._enableManualSearch = null;
    this._enableProgressPanel = null;
    this._query = null;
    this._enableAllLanguagesSearch = null;
}
Telerik.Sitefinity.Web.UI.Components.BinderSearch.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Components.BinderSearch.callBaseMethod(this, "initialize");

        this._searchFields = this._deserializeCommaDelimitedList(this._searchFieldsString);
        this._extendedSearchFields = this._deserializeCommaDelimitedList(this._extendedSearchFieldsString);

        this._additionalFilterExpression = "";

        this._binderDataBoundDelegate = Function.createDelegate(this, this._searchFinished);
        this._highlightDelegate = Function.createDelegate(this, this._highlightItem);
        this._colorizeDelegate = Function.createDelegate(this, this._colorizeItem);
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Components.BinderSearch.callBaseMethod(this, "dispose");
    },

    search: function (query) {
        this._query = query;

        if (this.get_enableProgressPanel()) {
            this._showProgressPanel();
        }

        var filterExpression = this.buildFilterExpression(query);

        this.get_binder().set_isFiltering(true);
        this.get_binder().add_onDataBound(this._binderDataBoundDelegate);
        if (!this._disableHighlighthing) {
            this.get_binder().add_onItemDataBinding(this._highlightDelegate);
            this.get_binder().add_onItemDataBound(this._colorizeDelegate);
        }

        this.get_binder().set_filterExpression(filterExpression);
        this.get_binder().DataBind();
    },

    buildFilterExpression: function (query) {
        var filterExpression = "";
        if (query) {
            query = query.split('"').join('""'); // DynamicLinq will throw error if '"' is not escaped as '""'
            filterExpression = this._getFilterExpression(query);
            if (this._additionalFilterExpression)
                filterExpression += " AND " + this._additionalFilterExpression;
        }
        else {
            if (this._additionalFilterExpression) {
                filterExpression = this._additionalFilterExpression;
            }
        }
        return filterExpression;
    },

    // deserializes a comma delimited list into an array
    _deserializeCommaDelimitedList: function (stringValue) {
        if (stringValue == null || stringValue.length == 0) {
            return null;
        }
        var items = stringValue.split(',');
        for (i = 0, itemsLength = items.length; i < itemsLength; i++) {
            items[i] = jQuery.trim(items[i]);
        }
        return items;
    },

    // creates a dynamic linq filter expression
    _getFilterExpression: function (query) {
        var filterExpression = '';
        filterExpression += this._getFieldsExpression(this._searchFields, query);
        filterExpression += this._getFieldsExpression(this._extendedSearchFields, query, this._generateExtendedExpression);
        if (filterExpression.length > 0) {
            // remove the last OR join
            if (filterExpression.indexOf(' OR ', filterExpression.length - 4) !== -1)
                filterExpression = filterExpression.substring(0, filterExpression.length - 4);

            filterExpression = "(" + filterExpression + ")";
        }
        return filterExpression;
    },

    // creates a dynamic linq filter expression for the specified fields collection
    _getFieldsExpression: function (fields, query, operation) {
        var filterExpression = "";
        operation = operation || this._generateExpression;
        if (fields) {
            for (var i = 0, l = fields.length; i < l; i++) {
                //filterExpression += fields[i] + " != null AND ";
                filterExpression += operation.call(this, fields[i], query);
            }
        }
        return filterExpression;
    },

    //generates simple expression for the given field
    _generateExpressionClause: function (field, query) {
        if (this._searchType === 'Equals')
            return String.format("{0} = \"{1}\" OR ", field, query);
        return String.format("{0}.ToUpper().{1}(\"{2}\".ToUpper()) OR ", field, this._searchType, query);
    },

    //// generates simple expression for the given field
    _generateExpression: function (field, query) {

        if (this._multilingual && this._enableAllLanguagesSearch) {
            var languages = this._definedLanguages;
            var filterExpression = "";
            for (var i = 0; i < languages.length; i++) {
                //e.g. Title["fr"]
                var localizedField = String.format("{0}[\"{1}\"]", field, languages[i]);
                filterExpression += this._generateExpressionClause(localizedField, query);
            }
            return filterExpression;
        }
        else {
            return this._generateExpressionClause(field, query);
        }
    },

    // generates complex expression for the given Lstring field
    // an example expression: "(Title.Contains("news") OR (Title["en"] = null AND Title["fr"] = null AND Title[""].Contains("news")))"
    _generateExtendedExpression: function (field, query) {        
        if (!this._multilingual)
            return this._generateExpression(field, query);
        if (this.get_binder() != null) {
            var currentLanguage = this.get_binder().get_uiCulture();
            if (this._defaultLanguage !== currentLanguage)
                return this._generateExpression(field, query);
        }
        var indx = field.indexOf("[");
        if (indx != -1) {
            return this._generateExpression(field, query);
        }
        var filterExpression = "";
        if (this._searchType === 'Equals') {
            filterExpression += String.format("{0} = \"{1}\" OR {0}[\"\"] = \"{1}\"", field, query);
        }
        else {
            filterExpression += String.format("{0}.ToUpper().{1}(\"{2}\".ToUpper()) OR {0}[\"\"].ToUpper().{1}(\"{2}\".ToUpper())", field, this._searchType, query);
        }
        return filterExpression;
    },

    _searchFinished: function () {
        this._hideProgressPanel();
        this.get_binder().remove_onDataBound(this._binderDataBoundDelegate);
        if (!this._disableHighlighthing) {
            this.get_binder().remove_onItemDataBinding(this._highlightDelegate);
            this.get_binder().remove_onItemDataBound(this._colorizeDelegate);
        }
    },

    _highlightItem: function (sender, args) {
        if (this._query == ' ' || this._query.length == 0) {
            return;
        }
        var keywordRegEx = new RegExp("(" + this._query + ")", "i");
        for (i = 0, fieldsLength = this._searchFields.length; i < fieldsLength; i++) {
            args[this._searchFields[i]] = args[this._searchFields[i]].replace(keywordRegEx, "[highlight]$1[/highlight]");
        }
        delete keywordRegEx;
    },

    _colorizeItem: function (sender, args) {
        var innerDiv = $(args.get_itemElement()).find('.sys-container').get(0);
        var innerHtml = innerDiv.innerHTML;
        var keywordRegEx = /\[highlight\](.*)\[\/highlight\]/gi;
        innerHtml = innerHtml.replace(keywordRegEx, "<span class=\"sfHighlighted\" style=\"display:inline;\">$1</span>");
        innerDiv.innerHTML = innerHtml;
    },

    _showProgressPanel: function () {
        $(this._searchProgressPanel).show();
    },

    _hideProgressPanel: function () {
        $(this._searchProgressPanel).hide();
    },

    /* ----------------------- events ----------------------- */

    /* ----------------------- event raising ----------------------- */

    /* ----------------------- properties ----------------------- */

    // gets the reference to the client binder associated with the search box
    get_binder: function () {
        if (!this._binder) {
            this._binder = $find(this._binderId);
        }
        return this._binder;
    },
    set_binder: function (val) {
        if (val != this._binder) {
            this._binder = val;
            this.raisePropertyChanged("binder");
        }
    },

    get_searchProgressPanel: function () {
        return this._searchProgressPanel;

    },
    set_searchProgressPanel: function (value) {
        if (this._searchProgressPanel != value) {
            this._searchProgressPanel = value;
            this.raisePropertyChanged("searchProgressPanel");
        }
    },

    get_additionalFilterExpression: function () {
        return this._additionalFilterExpression;
    },
    set_additionalFilterExpression: function (value) {
        if (this._additionalFilterExpression != value) {
            this._additionalFilterExpression = value;
            this.raisePropertyChanged('additionalFilterExpression');
        }
    },

    get_binderId: function () {
        return this._binderId;
    },
    set_binderId: function (value) {
        if (this._binderId != value) {
            this._binderId = value;
            this._binder = null;
            this.raisePropertyChanged('binderId');
        }
    },

    get_enableProgressPanel: function () {
        return this._enableProgressPanel;
    },
    set_enableProgressPanel: function (value) {
        this._enableProgressPanel = value;
    }
};

Telerik.Sitefinity.Web.UI.Components.BinderSearch.registerClass('Telerik.Sitefinity.Web.UI.Components.BinderSearch', Sys.UI.Control);