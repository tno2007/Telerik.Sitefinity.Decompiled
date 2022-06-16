/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.LanguageListField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.LanguageListField.initializeBase(this, [element]);
    this._languageListControl = null;
    this._languageListControlDelegate = null;
    this._loadDelegate = null;
    this._hiddenLanguages = [];
}

Telerik.Sitefinity.Web.UI.Fields.LanguageListField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguageListField.callBaseMethod(this, "initialize");
        this._loadDelegate = Function.createDelegate(this, this.loadHandler);
        Sys.Application.add_load(this._loadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguageListField.callBaseMethod(this, "dispose");
        if (this._languageListControlDelegate) {
            this._languageListControl.remove_command(this._languageListControlDelegate);
            delete this._languageListControlDelegate;
        }
        if (this._loadDelegate) {
            delete this._loadDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    //changing the culture of the caller's manager
    saveChanges: function (dataItem, successCallback, failureCallback, caller) {
        return;
    },

    //This should awlays return true in order for the saveChanges method to be executed. 
    isChanged: function () {
        return false;
    },

    get_value: function () {
        //This control doesn't support a state
        return null;
    },

    set_value: function (value) {
        if (value === null) {
            this._clearItems();
        }
        //If the supplied value is an array
        if (jQuery.isArray(value)) {
            this._showHiddenLanguages();
            var availableLanguages = value;
            var languagesCount = value.length;
            while (languagesCount--) {
                //Skipping the invariant culture a.k.a. the "" culture
                var language = availableLanguages[languagesCount];
                if (language && language !== "") {
                    this.get_languageListControl().setLanguageIsInUse(language, true);
                }
            }
        }
    },

    hideLanguage: function (languageCode) {
        var languageToHide = this.get_languageListControl().getLanguageElement(languageCode);
        if (languageToHide) {
            this._hiddenLanguages.push(languageCode);
            jQuery(languageToHide).hide();
        }
    },

    /* -------------------- events -------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    /* -------------------- event handlers ------------ */

    loadHandler: function () {
        this._languageListControlDelegate = Function.createDelegate(this, this._languageListControlCommandHandler);
        this._languageListControl.add_command(this._languageListControlDelegate);
    },

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguageListField.callBaseMethod(this, "reset");
        this.get_languageListControl().clearLanguagesInUse();
        this._showHiddenLanguages();
    },
    /* -------------------- private methods ----------- */

    _clearItems: function () {
    },

    //This is a handler for the LanguageListControl command, 
    //which is executing the handlers subscribed to the command of this control.
    //The proper agruments are constructed there.
    _languageListControlCommandHandler: function (sender, args) {
        var h = this.get_events().getHandler('command');
        if (h) h(this, args);
    },

    _showHiddenLanguages: function () {
        var hiddenLanguagesLength = this._hiddenLanguages.length;
        while (hiddenLanguagesLength--) {
            var languageToShow = this.get_languageListControl().getLanguageElement(this._hiddenLanguages[hiddenLanguagesLength]);
            if (languageToShow) {
                jQuery(languageToShow).show();
            }
        }
    },

    /* -------------------- properties ---------------- */

    get_languageListControl: function () {
        return this._languageListControl;
    },
    set_languageListControl: function (value) {
        this._languageListControl = value;
    }

};

Telerik.Sitefinity.Web.UI.Fields.LanguageListField.registerClass("Telerik.Sitefinity.Web.UI.Fields.LanguageListField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.ISelfExecutableField, Telerik.Sitefinity.Web.UI.Fields.ICommandField);
