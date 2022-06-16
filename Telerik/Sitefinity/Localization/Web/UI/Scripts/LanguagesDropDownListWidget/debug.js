Type.registerNamespace("Telerik.Sitefinity.Localization.Web.UI");

// ------------------------------------------------------------------------
// Class: LanguagesDropDownListWidget
// ------------------------------------------------------------------------
Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget = function (element) {
    Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget.initializeBase(this, [element]);
    this._commandName = null;
    this._languagesList = null;
    this._languagesListId = null;
    this._loadDelegate = null;
    this._languageChangedDelegate = null;
}
Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget.prototype = {
    // ------------------------------------------------------------------------
    // Set-up and tear-down
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget.callBaseMethod(this, 'initialize');
        this._loadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._loadDelegate);
        this._languageChangedDelegate = Function.createDelegate(this, this._languageChanged);
    },
    dispose: function () {
        Sys.Application.remove_load(this._loadDelegate);
        delete this._loadDelegate;
        this._languagesList.remove_languageChanged(this._languageChangedDelegate);
        delete this._languageChangedDelegate;
        Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget.callBaseMethod(this, 'dispose');
    },

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    /* ------------------------------------- EventHandlers --------------------------------------- */
    _handlePageLoad: function (sender, args) {
        this._languagesList = $find(this._languagesListId);
        this._languagesList.add_languageChanged(this._languageChangedDelegate);
    },
    _languageChanged: function (sender, args) {
        var commandEventArgs = new Telerik.Sitefinity.CommandEventArgs(this._commandName, args.get_value());
        var h = this.get_events().getHandler('command');
        if (h) h(this, commandEventArgs);
    },

    /* ------------------------------------- Properties --------------------------------------- */
    get_commandName: function () {
        return this._commandName;
    },
    set_commandName: function (value) {
        this._commandName = value;
    },
    get_languagesListId: function () {
        return this._languagesListId;
    },
    set_languagesListId: function (value) {
        this._languagesListId = value;
    }
}
Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget.registerClass('Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownListWidget', Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget, Telerik.Sitefinity.UI.ICommandWidget);
