Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

var _currentMultisiteSelector = null;
function GetCurrentMultisiteSelector() {
    return _currentMultisiteSelector;
}

Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl = function () {
    Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl.initializeBase(this);
    this._siteIdParamKey = null;
    this._selectedSite = null;

    _currentMultisiteSelector = this;
}

Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl.callBaseMethod(this, 'initialize');
        this._windowFocusDelegate = Function.createDelegate(this, this._ensureSelectedSiteSession);
        window.onfocus = this._windowFocusDelegate;
    },

    dispose: function () {
        Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl.callBaseMethod(this, 'dispose');

        if (this._windowFocusDelegate) {
            delete this._windowFocusDelegate;
        }
    },

    _ensureSelectedSiteSession: function () {
        var cookieSiteId = jQuery.cookie(this._siteIdParamKey);
        if (cookieSiteId !== this._selectedSite) {
            var expirationDays = 2 * 365; // 2 years
            jQuery.cookie(this._siteIdParamKey, this._selectedSite, { expires: expirationDays, path: '/' });
        }
    },

    get_siteIdParamKey: function () {
        return this._siteIdParamKey;
    },
    set_siteIdParamKey: function (value) {
        this._siteIdParamKey = value;
    },
    get_selectedSite: function () {
        return this._selectedSite;
    },
    set_selectedSite: function (value) {
        this._selectedSite = value;
    }
}

Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl.registerClass('Telerik.Sitefinity.Multisite.Web.UI.BackendMultisiteSessionControl', Sys.Component);