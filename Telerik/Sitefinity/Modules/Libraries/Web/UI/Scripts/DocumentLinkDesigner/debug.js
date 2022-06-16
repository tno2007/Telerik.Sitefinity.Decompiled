Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner.initializeBase(this, [element]);
    this._providersSelectedDelegate = null;
    this._providersSelector = null;
    this._uploadDocumentView = null;
    this._documentSelectorView = null;
    this._isProviderCorrect = null;
    this._message = null;
    this._tabSelectedDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner.callBaseMethod(this, "initialize");

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

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner.callBaseMethod(this, "dispose");
    },

    // ----------------------------------------------- Private functions ----------------------------------------------
    //rebind all controls with specified provider name
    _rebind: function (providerName) {
        if (this._uploadDocumentView && this._uploadDocumentView.rebind) {
            this._uploadDocumentView.rebind(providerName);
        }

        if (this._documentSelectorView && this._documentSelectorView.rebind) {
            this._documentSelectorView.rebind(providerName);
        }
    },

    // --------------------------------- event handlers --------------------------------- 
    _onLoad: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner.callBaseMethod(this, "_onLoad");

        if (this._menuTabStrip) {
            this._tabSelectedDelegate = Function.createDelegate(this, this._tabSelectedHandler);
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

    //gets the upload document view
    get_uploadDocumentView: function () { return this._uploadDocumentView; },
    set_uploadDocumentView: function (value) { this._uploadDocumentView = value; },

    //gets the document selector view
    get_documentSelectorView: function () { return this._documentSelectorView; },
    set_documentSelectorView: function (value) { this._documentSelectorView = value; },

    //gets boolean value indicating whether selected provider name is correct
    get_isProviderCorrect: function () { return this._isProviderCorrect; },
    set_isProviderCorrect: function (value) { this._isProviderCorrect = value; },

    //gets the message control
    get_message: function () { return this._message; },
    set_message: function (value) { this._message = value; }
}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner", Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase);