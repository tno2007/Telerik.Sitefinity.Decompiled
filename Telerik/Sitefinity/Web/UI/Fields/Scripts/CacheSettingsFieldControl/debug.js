Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl = function (element) {
    this._element = element;

    this._cacheStatusChoiceField = null;
    this._useDefaultChoiceField = null;
    this._cacheDurationTextField = null;
    this._contentPanelId = null;
    this._innerContentPanelId = null;
    this._defaultCacheDuration = null;
    this._defaultCacheSlidingExpiration = null;
    this._slidingExpirationChoiceField = null;

    this._onLoadDelegate = null;
    this._onChoiceChangeDelegate = null;
    this._defaultChoiceChangeDelegate = null;

    Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl.callBaseMethod(this, "initialize");

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
            Sys.Application.add_load(this._onLoadDelegate);
            this._onChoiceChangeDelegate = Function.createDelegate(this, this._onStatusChoiceChange);
            this._defaultChoiceChangeDelegate = Function.createDelegate(this, this._defaultChoiceChangeHandler);

            this._cacheStatusChoiceField.add_valueChanged(this._onChoiceChangeDelegate);
            this._useDefaultChoiceField.add_valueChanged(this._defaultChoiceChangeDelegate);

            if (this._cacheStatusChoiceField.get_value() == "false") {
                jQuery('#' + this._contentPanelId).hide();
            }
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl.callBaseMethod(this, "dispose");
        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
        if (this._onChoiceChangeDelegate) {
            this._cacheStatusChoiceField.remove_valueChanged(this._onChoiceChangeDelegate);
            delete this._onChoiceChangeDelegate;
        }
        if (this._defaultChoiceChangeDelegate) {
            this._useDefaultChoiceField.remove_valueChanged(this._defaultChoiceChangeDelegate);
            delete this._defaultChoiceChangeDelegate;
        }

        this._cacheStatusChoiceField = null;
        this._slidingExpirationChoiceField = null;
        this._cacheDurationTextField = null;
        this._useDefaultChoiceField = null;
    },

    /* --------------------  public methods ----------- */

    // This function allows other objects to subscribe to the statusChanged event of the field control.
    add_statusChanged: function (delegate) {
        this.get_events().addHandler('statusChanged', delegate);
    },

    // This function allows other objects to unsubscribe from the statusChanged event of the field control.
    remove_statusChanged: function (delegate) {
        this.get_events().removeHandler('statusChanged', delegate);
    },
    reset: function () {
        //do nothing. The contained field controls must be reset.
    },
    /* -------------------- events -------------------- */

    _statusChangedHandler: function () {
        var h = this.get_events().getHandler('statusChanged');
        if (h) h(this, Sys.EventArgs.Empty);
        return Sys.EventArgs.Empty;
    },

    /* -------------------- event handlers ------------ */

    _onLoad: function () {
    },

    //handles the choice selection
    _onStatusChoiceChange: function () {
        var value = this._cacheStatusChoiceField.get_value();

        if (value == "true") {
            jQuery('#' + this._contentPanelId).show();
        }
        else {
            jQuery('#' + this._contentPanelId).hide();
        }
    },

    //handles the choice selection
    _defaultChoiceChangeHandler: function () {
        if (this._useDefaultChoiceField.get_value() == "true") {
            jQuery('#' + this._innerContentPanelId).block({ message: null, overlayCSS: { cursor: 'arrow'} });
            this._cacheDurationTextField.set_value(this._defaultCacheDuration);
            this._slidingExpirationChoiceField.set_value(this._defaultCacheSlidingExpiration);
        }
        else {
            jQuery('#' + this._innerContentPanelId).unblock();
        }
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    // gets the reference to the cache satus ChoiceField component used to choose if caching is enabled
    get_cacheStatusChoiceField: function () {
        return this._cacheStatusChoiceField;
    },

    // sets the reference to the cache satus ChoiceField component used to choose if caching is enabled
    set_cacheStatusChoiceField: function (value) {
        this._cacheStatusChoiceField = value;
    },

    // gets the reference to the cache duration TextField component
    get_cacheDurationTextField: function () {
        return this._cacheDurationTextField;
    },

    // sets the reference to the cache duration TextField component
    set_cacheDurationTextField: function (value) {
        this._cacheDurationTextField = value;
    },

    // gets the reference to the sliding expiration ChoiceField component
    get_slidingExpirationChoiceField: function () {
        return this._slidingExpirationChoiceField;
    },

    // sets the reference to the sliding expiration ChoiceField component
    set_slidingExpirationChoiceField: function (value) {
        this._slidingExpirationChoiceField = value;
    },

    // gets the reference to the "use default" ChoiceField component
    get_useDefaultChoiceField: function () {
        return this._useDefaultChoiceField;
    },

    // sets the reference to the "use default" ChoiceField component
    set_useDefaultChoiceField: function (value) {
        this._useDefaultChoiceField = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl.registerClass("Telerik.Sitefinity.Web.UI.Fields.CacheSettingsFieldControl", Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl);
