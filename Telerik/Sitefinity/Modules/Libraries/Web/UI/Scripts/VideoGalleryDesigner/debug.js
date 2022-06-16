Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner.initializeBase(this, [element]);
    this._libraryType = null;

    this._selectorViewName = null;
    this._settingsViewName = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner.callBaseMethod(this, 'initialize');

        this._initDelegates();
        this._addHandlers();
        this._bindSettingsView();
    },

    dispose: function () {

        this._removeHandlers();
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner.callBaseMethod(this, 'dispose');
    },

    /* Private Methods */

    _initDelegates: function () {

    },

    _addHandlers: function () {

    },

    _removeHandlers: function () {

    },

    _clientTabSelectedHandler: function (sender, args) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner.callBaseMethod(this, '_clientTabSelectedHandler', [sender, args]);
        var viewName = sender.get_selectedTab().get_value();
        if (viewName == this._settingsViewName) {
            this._bindSettingsView();
        }
    },

    _bindSettingsView: function() {
        var selectorView = $find(this.get_designerViewsMap()[this._selectorViewName]);
        var settingsView = $find(this.get_designerViewsMap()[this._settingsViewName]);
        if (selectorView && settingsView) {
            settingsView._libraryType = this._libraryType;
            if (selectorView._selectedLibraryId != Telerik.Sitefinity.getEmptyGuid() && selectorView._libraryRadio.checked) {
                var providerName = "";
                if (selectorView.get_providersSelector() && selectorView.get_providersSelector()._selectedProvider) {
                    providerName = selectorView.get_providersSelector()._selectedProvider;
                }
                var selectedLibrary = { Id: selectorView._selectedLibraryId, ProviderName: providerName };
                settingsView.setSelectedLibrary(selectedLibrary);
            }
            else {
                settingsView.setSelectedLibrary(null);
            }
        }
    },

    /* Private Methods */

    /* Event Handlers */


    /* Event Handlers */

    /* Properties */

    get_libraryType: function() {
        return this._libraryType;
    },
    set_libraryType: function(value) {
        this._libraryType = value;
    },

    get_selectorViewName: function () {
        return this._selectorViewName;
    },
    set_selectorViewName: function (value) {
        this._selectorViewName = value;
    },

    get_settingsViewName: function () {
        return this._settingsViewName;
    },
    set_settingsViewName: function (value) {
        this._settingsViewName = value;
    }

    /* Properties */
}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner", Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase);
