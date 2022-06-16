Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner.initializeBase(this, [element]);
    this._providersSelectedDelegate = null;
    this._providersSelector = null;
    this._uploadVideoView = null;
    this._videoSelectorView = null;
    this._message = null;
    this._isProviderCorrect = null;
    this._tabSelectedDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner.callBaseMethod(this, "initialize");

        if (this._propertyEditor && this._propertyEditor._providersSelector) {
            this._providersSelector = this._propertyEditor._providersSelector;
            this._providersSelectedDelegate = Function.createDelegate(this, this._handleProvidersSelected);
            this._providersSelector.add_onProviderSelected(this._providersSelectedDelegate);
        }
    },

    dispose: function () {
        if (this._providersSelector) {
            this._providersSelector.remove_onProviderSelected(this._providersSelectedDelegate);
            delete this._providersSelectedDelegate;
        }

        if (this._menuTabStrip) {
            this._menuTabStrip.remove_tabSelected(this._tabSelectedDelegate);
            delete this._tabSelectedDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner.callBaseMethod(this, "dispose");
    },

    // ----------------------------------------------- Private functions ----------------------------------------------
    //rebind all controls with specified provider name
    _rebind: function (providerName) {
        if (this._uploadVideoView && this._uploadVideoView.rebind) {
            this._uploadVideoView.rebind(providerName);
        }

        if (this._videoSelectorView && this._videoSelectorView.rebind) {
            this._videoSelectorView.rebind(providerName);
        }
    },

    // --------------------------------- event handlers --------------------------------- 

    _onLoad: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner.callBaseMethod(this, "_onLoad");

        if (this._menuTabStrip) {
            this._tabSelectedDelegate = Function.createDelegate(this, this._tabSelectedHandler)
            this._menuTabStrip.add_tabSelected(this._tabSelectedDelegate);
        }
    },

    _handleProvidersSelected: function (sender, args) {
        var controlData = this.get_controlData();
        if (controlData.hasOwnProperty('ProviderName')) {
            controlData.ProviderName = args.ProviderName;
        }
        this._rebind(args.ProviderName);
    },

    _tabSelectedHandler: function (sender, args) {
        if (!this._isProviderCorrect && this._message) {
            //hide ProviderNotAvailable warning message
            this._message.hide();
            dialogBase.resizeToContent();
            this._isProviderCorrect = true;
        }
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    //gets the upload video view
    get_uploadVideoView: function () { return this._uploadVideoView; },
    set_uploadVideoView: function (value) { this._uploadVideoView = value; },

    //gets the video selector view
    get_videoSelectorView: function () { return this._videoSelectorView; },
    set_videoSelectorView: function (value) { this._videoSelectorView = value; },

    //gets boolean value indicating whether selected provider name is correct
    get_isProviderCorrect: function () { return this._isProviderCorrect; },
    set_isProviderCorrect: function (value) { this._isProviderCorrect = value; },

    //gets the message control
    get_message: function () { return this._message; },
    set_message: function (value) { this._message = value; }
}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner", Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase);