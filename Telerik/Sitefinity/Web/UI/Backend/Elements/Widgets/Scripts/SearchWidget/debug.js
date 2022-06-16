Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget.initializeBase(this, [element]);

    this._pageLoadDelegate = null;
    this._showSearchBoxDelegate = null;
    this._hideSearchBoxDelegate = null;

    this._searchBox = null;
    this._showSearchBoxLink = null;
    this._hideSearchBoxLink = null;
    this._searchPlaceHolder = null;

    this._searchCommandName = null;
    this._closeSearchCommandName = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget.callBaseMethod(this, 'initialize');

        if (this._pageLoadDelegate === null) {
            this._pageLoadDelegate = Function.createDelegate(this, this._pageLoad);
        }
        if (this._showSearchBoxDelegate === null) {
            this._showSearchBoxDelegate = Function.createDelegate(this, this._showSearchBoxHandler);
        }
        if (this._hideSearchBoxDelegate === null) {
            this._hideSearchBoxDelegate = Function.createDelegate(this, this._hideSearchBoxHandler);
        }

        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget.callBaseMethod(this, 'dispose');
        if (this._pageLoadDelegate) {
            delete this._pageLoadDelegate;
        }
        if (this._showSearchBoxDelegate) {
            delete this._showSearchBoxDelegate;
        }
        if (this._hideSearchBoxDelegate) {
            delete this._hideSearchBoxDelegate;
        }
    },

    /* ------------------------------------ Private functions --------------------------------- */

    _pageLoad: function () {
        this.get_searchBox().add_search(this._commandDelegate);
        this._hideSearchBox();
        $addHandler(this._showSearchBoxLink, 'click', this._showSearchBoxDelegate);
        $addHandler(this._hideSearchBoxLink, 'click', this._hideSearchBoxDelegate);
    },
    _showSearchBox: function () {
        $(this.get_searchPlaceHolder()).show();
        $(this.get_showSearchBoxLink()).hide();
        $(this.get_hideSearchBoxLink()).show();
        $('body').addClass("sfShowSearchPanel");
        this.get_searchBox().focusSearchBox();
    },
    _hideSearchBox: function () {
        $(this.get_searchPlaceHolder()).hide();
        $(this.get_showSearchBoxLink()).show();
        $(this.get_hideSearchBoxLink()).hide();
        this.get_searchBox().clearSearchBox();
        $('body').removeClass("sfShowSearchPanel");
    },
    _showSearchBoxHandler: function () {
        this._showSearchBox();
        this.set_commandName(this._searchCommandName);
    },
    _hideSearchBoxHandler: function () {
        this._hideSearchBox();
        this.set_commandName(this._closeSearchCommandName);
        this._commandHandler();
    },
    _commandHandler: function (sender, args) {
        if (!args) {
            args = null;
        }
        var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs(this.get_commandName(), args);
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
    },
    /* ------------------------------------ Public Methods ----------------------------------- */

    /* ------------------------------------- Properties --------------------------------------- */

    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        if (this._searchBox != value) {
            this._searchBox = value;
            this.raisePropertyChanged('searchBox');
        }
    },
    get_hideSearchBoxLink: function () {
        return this._hideSearchBoxLink;
    },
    set_hideSearchBoxLink: function (value) {
        if (this._hideSearchBoxLink != value) {
            this._hideSearchBoxLink = value;
            this.raisePropertyChanged('hideSearchBoxLink');
        }
    },
    get_showSearchBoxLink: function () {
        return this._showSearchBoxLink;
    },
    set_showSearchBoxLink: function (value) {
        if (this._showSearchBoxLink != value) {
            this._showSearchBoxLink = value;
            this.raisePropertyChanged('showSearchBoxLink');
        }
    },
    get_searchPlaceHolder: function () {
        return this._searchPlaceHolder;
    },
    set_searchPlaceHolder: function (value) {
        if (this._searchPlaceHolder != value) {
            this._searchPlaceHolder = value;
            this.raisePropertyChanged('searchPlaceHolder');
        }
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SearchWidget", Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget);