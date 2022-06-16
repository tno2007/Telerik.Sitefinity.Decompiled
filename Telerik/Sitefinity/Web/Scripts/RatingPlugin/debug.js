Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ------------------------------------------------------------------------
// Enum: RatingMode
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.RatingMode = function() { }
Telerik.Sitefinity.Web.UI.RatingMode.prototype = {
    Normal: 0,
    Edit: 1,
    Custom: 2
}
Telerik.Sitefinity.Web.UI.RatingMode.registerEnum("Telerik.Sitefinity.Web.UI.RatingMode");

// ------------------------------------------------------------------------
// Class: RatingPlugin
// ------------------------------------------------------------------------
// Constructor
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.RatingPlugin = function(element) {
    Telerik.Sitefinity.Web.UI.RatingPlugin.initializeBase(this, [element]);

    this._mode = null;
    this._resetId = null;
    this._radRatingId = null;
    this._subtitleId = null;
    this._manager = null;
    this._serviceBaseUrl = null;
    this._contentType = null;
    this._providerName = null;
    this._contentId = null;
    this._areYouSure = null;
};

Telerik.Sitefinity.Web.UI.RatingPlugin.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.RatingPlugin.callBaseMethod(this, 'initialize');

        Sys.Application.add_load(Function.createDelegate(this, this._onLoad));
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.RatingPlugin.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // public functions
    // ------------------------------------------------------------------------
    rate: function(rating) {
        //contentId}/rating/?itemType={itemType}
        if (!this._internalSetRating) {
            this.get_radRating().set_readOnly(true);
            var url = this._serviceBaseUrl + "/" + this._contentId + "/rating/";
            var queryString = [];
            if (this._contentType) {
                queryString["itemType"] = this._contentType;
            }
            if (this._providerName) {
                queryString["provider"] = this._providerName;
            }
            var self = this;
            this._manager.InvokePut(url, queryString, null, rating,
                this._onRateSuccess,
                function(errorData) { self._onError(errorData);  }, this);
        }
        else {
            this._internalSetRating = false;
        }
    },
    reset: function() {
        var url = this._serviceBaseUrl + "/" + this._contentId + "/rating/";
        var queryString = [];
        if (this._contentType) {
            queryString["itemType"] = this._contentType;
        }
        var self = this;
        this._manager.InvokeDelete(url, queryString, null,
            this._onResetSuccess, function(errorData) { self._onError(errorData); }, this);
    },

    // ------------------------------------------------------------------------
    // events
    // ------------------------------------------------------------------------
    add_error: function(handler) {
        this.get_events().addHandler(handler);
    },
    remove_error: function(handler) {
        this.get_events().removeHandler(handler);
    },

    // ------------------------------------------------------------------------
    // private functions
    // ------------------------------------------------------------------------
    _onLoad: function(sender, args) {
        this._manager = new Telerik.Sitefinity.Data.ClientManager();
        var radRating = this.get_radRating();
        if (radRating) {
            radRating.add_rating(Function.createDelegate(this, this._onRatingPreview));
        }
        var self = this;
        if (this._resetId) {
            $("#" + this._resetId).click(function(jEvent) {
                jEvent.preventDefault();
                if (confirm(self._areYouSure)) {
                    self.reset();
                }
            });
        }
    },
    _onRatingPreview: function(sender, args) {
        var rating = args.get_newValue();
        this.rate(rating);
    },
    _onRateSuccess: function(caller, successData) {
        var ratingResult = successData;
        if (ratingResult && typeof (ratingResult) == typeof ("")) {
            ratingResult = Sys.Serialization.JavaScriptSerializer.deserialize(successData);
        }
        var subtitleText = "";
        if (!caller._isError(ratingResult)) {
            subtitleText = caller._getSubtitleText(ratingResult);
        }
        else {
            caller._internalSetRating = true;
            caller.get_radRating().set_value(0);
        }
        var subtitleControl = caller.get_subtitle();
        $(subtitleControl).text(subtitleText);
    },
    _onResetSuccess: function(caller, successData) {
        var ratingResult = successData;
        if (ratingResult && typeof (ratingResult) == typeof ("")) {
            ratingResult = Sys.Serialization.JavaScriptSerializer.deserialize(successData);
        }
        var subtitleControl = caller.get_subtitle();

        if (subtitleControl) {
            var subtitleText = "";
            if (!caller._isError(ratingResult)) {
                subtitleText = caller._getSubtitleText(ratingResult);
            }
            $(subtitleControl).text(subtitleText);
        }

        var radRating = caller.get_radRating();
        if (radRating) {
            caller._internalSetRating = true;
            radRating.set_value(0);
        }
    },
    _getSubtitleText: function(rateResut) {
        return rateResut.SubtitleMessage;
    },
    _isError: function(rateResult) {
        return rateResult.Average == -1;
    },
    _onError: function(errorData) {
        args = new Telerik.Sitefinity.Web.UI.RatingErrorEventArgs(errorData);
        var h = this.get_events().getHandler("error");
        if (h) h(this, args);
        return args;
    },

    // ------------------------------------------------------------------------
    // properties
    // ------------------------------------------------------------------------
    get_radRatingId: function() {
        return this._radRatingId;
    },
    set_radRatingId: function(value) {
        this._radRatingId = value;
    },
    get_radRating: function() {
        var rating = $find(this._radRatingId);
        return rating;
    },
    get_subtitle: function() {
        var subtitle = $("#" + this._subtitleId);
        return subtitle;
    },
    get_manager: function() {
        return this._manager;
    },
    get_serviceBaseUrl: function() {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function(value) {
        this._serviceBaseUrl = value;
    },
    get_contentType: function() {
        return this._contentType;
    },
    set_contentType: function(value) {
        this._contentType = value;
    },
    get_providerName: function() {
        return this._providerName;
    },
    set_providerName: function(value) {
        this._providerName = value;
    },
    get_contentId: function() {
        return this._contentId;
    },
    set_contentId: function(value) {
        this._contentId = value;
    },
    get_subtitleId: function() {
        return this._subtitleId;
    },
    set_subtitleId: function(value) {
        this._subtitleId = value;
    },
    get_mode: function() {
        return this._mode;
    },
    set_mode: function(value) {
        this._mode = value;
    },
    get_resetId: function() {
        return this._resetId;
    },
    set_resetId: function(value) {
        this._resetId = value;
    },
    get_areYouSure: function() {
        return this._areYouSure;
    },
    set_areYouSure: function(value) {
        this._areYouSure = value;
    }
}
Telerik.Sitefinity.Web.UI.RatingPlugin.registerClass("Telerik.Sitefinity.Web.UI.RatingPlugin", Sys.UI.Control);

// ------------------------------------------------------------------------
// Class: ErrorEventArgs
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.RatingErrorEventArgs = function(error) {
    this._error = error;
    Telerik.Sitefinity.Web.UI.RatingErrorEventArgs.initializeBase();
}
Telerik.Sitefinity.Web.UI.RatingErrorEventArgs.prototype = {
    get_error: function() {
        return this._error;
    }
}
Telerik.Sitefinity.Web.UI.RatingErrorEventArgs.registerClass("Telerik.Sitefinity.Web.UI.RatingErrorEventArgs", Sys.EventArgs);