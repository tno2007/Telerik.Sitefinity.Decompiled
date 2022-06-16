﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField = function (element) {

    this._sites = null;
    this._selectedSite = null;
    this._currentSiteId = null;
    this._siteSelector = null;
    this._selectorPanel = null;
    this._changeButton = null;
    this._valuePanel = null;
    this._selectedSiteName = null;
    this._uiCulture = null;
    this._selectedLanguage = null;
    this._siteLanguageSelector = null;

    this._changeButtonClickDelegate = null;
    this._siteSelectedDelegate = null;
    this._siteLanguageSelectedDelegate = null;

    Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField.prototype =
{
    initialize: function () {

        if (this._changeButton) {
            this._changeButtonClickDelegate = Function.createDelegate(this, this._onChangeClick);
            $addHandler(this._changeButton, 'click', this._changeButtonClickDelegate);
        }

        if (this._siteSelector) {
            this._siteSelectedDelegate = Function.createDelegate(this, this._onSiteSelected);
            $addHandler(this._siteSelector, 'change', this._siteSelectedDelegate);
        }

        if (this._siteLanguageSelector) {
            this._siteLanguageSelectedDelegate = Function.createDelegate(this, this._onSiteLanguageSelected);
            $addHandler(this._siteLanguageSelector, 'change', this._siteLanguageSelectedDelegate);
        }

        if (this._selectorPanel) {
            this._toggleSelectorPanel(true);
        }

        Sys.Application.add_load(Function.createDelegate(this, this.onLoad));

        Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._changeButtonClickDelegate) {
            $removeHandler(this._changeButton, 'click', this._changeButtonClickDelegate);
        }

        if (this._siteSelectedDelegate) {
            $removeHandler(this._siteSelector, 'change', this._changeButtonClickDelegate);
        }

        Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    setSelectedCulture: function (culture) {
        if (!culture)
            return;
        this._uiCulture = this._selectedLanguage = culture;
        this._reloadLanguageSelector();
    },

    // Gets the value of the field control.
    get_value: function () {
        if (this._selectedSite) {
            return this._selectedSite.Id;
        }
    },

    // Sets the value of the field control.
    set_value: function (value) {
        if (this._siteSelector && value) {
            var selector = jQuery(this._siteSelector);
            this._selectedSite = this._sites[value];
            if (!this._selectedSite) {
                this._selectedSite = this._sites[this._currentSiteId];
            }
            selector.val(value);
            jQuery(this._selectedSiteName).text(this._selectedSite.Name);
        }
    },

    reset: function () {
        jQuery(this.get_valuePanel()).show();
        jQuery(this.get_selectorPanel()).hide();
        this._selectedLanguage = this._uiCulture;
        this._reloadLanguageSelector();
        Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField.callBaseMethod(this, "reset");
    },

    get_selectedLanguage: function () {
        return this._selectedLanguage;
    },

    /* -------------------- events -------------------- */

    onLoad: function () {
        if (this._sites) {
            this._sites = JSON.parse(this._sites);
        }
        this.set_value(this._currentSiteId);
        this._selectedLanguage = this._uiCulture;
        this._reloadLanguageSelector();
    },

    /* -------------------- event handlers ------------ */

    _onSiteLanguageSelected: function(){
        this._selectedLanguage = jQuery(this.get_siteLanguageSelector()).val();
        this._siteLanguageSelectedHandler(this._selectedSite, this._selectedLanguage);
    },

    _onChangeClick: function () {
        this._toggleSelectorPanel();
    },

    _onSiteSelected: function () {
        var selector = jQuery(this._siteSelector);
        this._selectedSite = this._sites[selector.val()];
        jQuery(this._selectedSiteName).text(this._selectedSite.Name);
        this._reloadLanguageSelector();
        this._siteSelectedHandler(this._selectedSite);
    },

    add_onSiteSelected: function (delegate) {
        this.get_events().addHandler('onSiteSelected', delegate);
    },

    remove_onSiteSelected: function (delegate) {
        this.get_events().removeHandler('onSiteSelected', delegate);
    },

    add_onSiteLanguageSelected: function (delegate) {
        this.get_events().addHandler('onSiteLanguageSelected', delegate);
    },

    remove_onSiteLanguageSelected: function (delegate) {
        this.get_events().removeHandler('onSiteLanguageSelected', delegate);
    },

    _siteSelectedHandler: function (site) {
        var h = this.get_events().getHandler('onSiteSelected');
        if (h) h(this, site);
    },

    _siteLanguageSelectedHandler: function(site, lang){
        var h = this.get_events().getHandler('onSiteLanguageSelected');
        if (h) h(this, lang);
    },

    /* -------------------- private methods ----------- */

    // Shows or hides the selector panel
    _toggleSelectorPanel: function (hide) {
        if (hide && hide === true) {
            jQuery(this._selectorPanel).hide();
            this._toggleValuePanel();
        }
        else {
            jQuery(this._selectorPanel).show();
            this._toggleValuePanel(true);
            this.set_value(this._currentSiteId);
        }
    },

    _toggleValuePanel: function (hide) {
        if (hide && hide === true) {
            jQuery(this._valuePanel).hide();
        }
        else {
            jQuery(this._valuePanel).show();
        }
    },

    _reloadLanguageSelector: function(){
        var ddlLanguages = jQuery(this.get_siteLanguageSelector());
        var firstCulture = "";

        ddlLanguages.html("");
        for (lang in this._selectedSite.PublicCultures) {
            // Get the first culture in the list in case we need it for the dropdown value, otherwise we'll have to enumerate object properties.
            if (firstCulture == "") 
                firstCulture = lang;
            ddlLanguages.append("<option value='" + lang + "'>" + this._selectedSite.PublicCultures[lang] + "</option>")
        }
        if (!this._selectedSite.PublicCultures[this._uiCulture])
            this._selectedLanguage = firstCulture;
        else
            this._selectedLanguage = this._uiCulture;

        jQuery(this.get_siteLanguageSelector()).val(this._selectedLanguage);
    },

    /* -------------------- properties ---------------- */

    get_siteSelector: function () {
        return this._siteSelector;
    },

    set_siteSelector: function (value) {
        this._siteSelector = value;
    },

    get_selectorPanel: function () {
        return this._selectorPanel;
    },

    set_selectorPanel: function (value) {
        this._selectorPanel = value;
    },

    get_changeButton: function () {
        return this._changeButton;
    },

    set_changeButton: function (value) {
        this._changeButton = value;
    },

    get_selectedSiteName: function () {
        return this._selectedSiteName;
    },

    set_selectedSiteName: function (value) {
        this._selectedSiteName = value;
    },

    get_valuePanel: function () {
        return this._valuePanel;
    },

    set_valuePanel: function (value) {
        this._valuePanel = value;
    },

    get_siteLanguageSelector: function () {
        return this._siteLanguageSelector;
    },

    set_siteLanguageSelector: function(value){
        this._siteLanguageSelector = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField.registerClass("Telerik.Sitefinity.Web.UI.Fields.SiteSelectorField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
