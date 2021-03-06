Type.registerNamespace("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend");

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase = function (element) {
    Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase.initializeBase(this, [element]);
    this._handlePageLoadDelegate = null;
    this._baseUrl = null;
    this._onLoadEvents = null;
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase.callBaseMethod(this, "initialize");

        if (this._onLoadEvents) {
            for (var i = 0, len = this._onLoadEvents.length; i < len; i++) {

                //Sys.Application.add_load(this._onLoadEvents[i]);
                var h = this._onLoadEvents[i];
                if (h && window[h]) {
                    this.add_viewLoaded(window[h]);
                }                
            }
        }

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase.callBaseMethod(this, "dispose");
        this._handleViewLoadedDelegate = null;
        Sys.Application.remove_load(this._handlePageLoadDelegate);
    },

    // -------------------------------------- Events -----------------------------------------------

    add_viewLoaded: function (handler) {
        this.get_events().addHandler('viewLoaded', handler);
    },

    remove_viewLoaded: function (handler) {
        this.get_events().removeHandler('viewLoaded', handler);
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _handlePageLoad: function (sender, args) {        
        var handler = this.get_events().getHandler('viewLoaded');
        if (handler) {
            handler(this);
        }
    },

    // ------------------------------------------ Properties -------------------------------------------

    get_onLoadEvents: function () {
        return this._onLoadEvents;
    },
    set_onLoadEvents: function (value) {
        this._onLoadEvents = value;
    },

    get_baseUrl: function () {
        return this._baseUrl;
    },

    set_baseUrl: function (value) {
        if (this._baseUrl != value) {
            this._baseUrl = value;
            this.raisePropertyChanged('baseUrl');
        }
    }
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase", Sys.UI.Control);