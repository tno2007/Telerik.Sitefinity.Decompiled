Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* BindeSearchBox class */

Telerik.Sitefinity.Web.UI.BinderSearchBox = function (element) {
    Telerik.Sitefinity.Web.UI.BinderSearchBox.initializeBase(this, [element]);
    this._searchBox = null;
    this._binderSearch = null;

    this._searchDelegate = null;
    this._pageLoadDelegate = null;
    this._enableManualSearch = false;
}
Telerik.Sitefinity.Web.UI.BinderSearchBox.prototype = {
    // set up 
    initialize: function () {
        Telerik.Sitefinity.Web.UI.BinderSearchBox.callBaseMethod(this, "initialize");
        if (!this._searchDelegate)
            this._searchDelegate = Function.createDelegate(this, this.search);
        if (!this._pageLoadDelegate)
            this._pageLoadDelegate = Function.createDelegate(this, this.pageLoad);
        Sys.Application.add_load(this._pageLoadDelegate);
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.BinderSearchBox.callBaseMethod(this, "dispose");
        if (this._searchDelegate)
            delete this._searchDelegate;
        if (this._pageLoadDelegate)
            delete this._pageLoadDelegate;
        Sys.Application.remove_load(this._pageLoadDelegate);
    },

    pageLoad: function () {
        this.get_searchBox().add_search(this._searchDelegate);
    },

    search: function (sender, args) {
        var query = args.get_query();
        var binderSearch = this.get_binderSearch();
        if (!this.get_enableManualSearch()) {
            binderSearch.search(query);
        }
        else {
            var filter = binderSearch.buildFilterExpression(query);
            this._raiseManualSearch(filter);
        }
    },

    /* ----------------------- events ----------------------- */
    add_manualSearch: function (delegate) {
        this.get_events().addHandler("manualSearch", delegate);
    },
    remove_manualSearch: function (delegate) {
        this.get_events().addHandler("manualSearch", delegate);
    },
    _raiseManualSearch: function (filter) {
        var handler = this.get_events().getHandler("manualSearch");
        if (handler) {
            var args = new Telerik.Sitefinity.Web.UI.SearchEventArgs(filter);
            handler(this, args);
        }
    },

    /* ----------------------- properties ----------------------- */
    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        this._searchBox = value;
    },

    get_binderSearch: function () {
        return this._binderSearch;
    },
    set_binderSearch: function (value) {
        this._binderSearch = value;
    },

    get_binder: function () {
        return this._binder;
    },
    set_binder: function (val) {
        if (val != this._binder) {
            this._binder = val;
            if (this._binderSearch) {
                this._binderSearch.set_binder(val);
            }
        }
    },

    get_searchProgressPanel: function () {
        return this.get_binderSearch().get_searchProgressPanel();
    },
    set_searchProgressPanel: function (value) {
        this.get_binderSearch().set_searchProgressPanel(value);
    },

    get_additionalFilterExpression: function () {
        return this.get_binderSearch().get_additionalFilterExpression();
    },
    set_additionalFilterExpression: function (value) {
        this.get_binderSearch().set_additionalFilterExpression(value);
    },

    get_enableManualSearch: function () { return this._enableManualSearch; },
    set_enableManualSearch: function (val) {
        if (val != this._enableManualSearch) {
            this._enableManualSearch = val;
            this.raisePropertyChanged("enableManualSearch");
        }
    },

    get_textBox: function () {
        return this.get_searchBox().get_textBox();
    },
    set_textBox: function (value) {
        this.get_searchBox().set_textBox(value);
    }
};

Telerik.Sitefinity.Web.UI.BinderSearchBox.registerClass('Telerik.Sitefinity.Web.UI.BinderSearchBox', Sys.UI.Control);

//-----------------------------------------------------------------------------
// Event arguments
//-----------------------------------------------------------------------------

Telerik.Sitefinity.Web.UI.SearchEventArgs = function(query) {
    /// <summary>Event arguments for the BinderSearchBox' search event</summary>
    Telerik.Sitefinity.Web.UI.SearchEventArgs.initializeBase(this);
    this._query = query;
}
Telerik.Sitefinity.Web.UI.SearchEventArgs.prototype = {
    get_query: function() { return this._query; }
}
Telerik.Sitefinity.Web.UI.SearchEventArgs.registerClass("Telerik.Sitefinity.Web.UI.SearchEventArgs", Sys.EventArgs);
