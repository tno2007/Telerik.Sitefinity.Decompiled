﻿Type.registerNamespace("Telerik.Sitefinity.Localization.Web.UI");

// ------------------------------------------------------------------------
// Class: LanguagesDropDownList
// ------------------------------------------------------------------------
Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList = function (element) {
    Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList.initializeBase(this, [element]);
    this._dropDownList = null;
    this._selectedIndex = 0;
    this._changeDelegate = null;
    this._loadDelegate = null;
}
Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList.prototype = {
    // ------------------------------------------------------------------------
    // Set-up and tear-down
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList.callBaseMethod(this, 'initialize');
        this._dropDownList.selectedIndex = this._selectedIndex;
        this._changeDelegate = Function.createDelegate(this, this._handleIndexChanged);
        this._loadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._loadDelegate);
        $addHandler(this._dropDownList, "change", this._changeDelegate);
    },
    dispose: function () {
        Sys.Application.remove_load(this._loadDelegate);
        delete this._loadDelegate;
        $removeHandler(this._dropDownList, "change", this._changeDelegate);
        delete this._changeDelegate;
        Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // Private functions
    // ------------------------------------------------------------------------
    _handlePageLoad: function (sender, args) {
    },
    _handleIndexChanged: function (e) {
        var handler = this.get_events().getHandler("languageChanged");
        if (handler) {
            var selectedOption = this._dropDownList.options[this._dropDownList.selectedIndex];
            var args = new Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs(
                this, this._dropDownList.selectedIndex, selectedOption.text, selectedOption.value, e);
            handler(this, args);
        }
    },

    // ------------------------------------------------------------------------
    // Events
    // ------------------------------------------------------------------------    
    add_languageChanged: function (handler) {
        this.get_events().addHandler("languageChanged", handler);
    },
    remove_languageChanged: function (handler) {
        this.get_events().removeHandler("languageChanged", handler);
    },
    /********************** Properties *****************************/
    get_dropDownList: function () {
        return this._dropDownList;
    },
    set_dropDownList: function (value) {
        this._dropDownList = value;
    },
    get_selectedIndex: function () {
        return this._selectedIndex;
    },
    set_selectedIndex: function (value) {
        this._selectedIndex = value;
    }
}
Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList.registerClass('Telerik.Sitefinity.Localization.Web.UI.LanguagesDropDownList', Sys.UI.Control);
// ------------------------------------------------------------------------
// Index changed event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs = function (source, selectedIndex, text, value, domevent) {
    Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs.initializeBase(this);
    this._source = source;
    this._selectedIndex = selectedIndex;
    this._text = text;
    this._value = value;
    this._domevent = domevent;
}

Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs.callBaseMethod(this, 'dispose');
    },

    get_source: function () {
        return this._source;
    },
    get_selectedIndex: function () {
        return this._selectedIndex;
    },
    get_text: function () {
        return this._text;
    },
    get_value: function () {
        return this._value;
    },
    get_domevent: function () {
        return this._domevent;
    }
};
Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs.registerClass('Telerik.Sitefinity.Localization.Web.UI.IndexChangedEventArgs', Sys.EventArgs);
