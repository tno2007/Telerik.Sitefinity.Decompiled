/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField.initializeBase(this, [element]);
    this._languageSelectionEnum = Telerik.Sitefinity.Localization.Web.UI.LanguagesSelection;

    this._loadDelegate = null;
    this._enabled = true;

    this._languagesToShow = this._languageSelectionEnum.Unavailable;

    this._removedLanguages = {};
    this._itemsMap = {};

}

Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField.callBaseMethod(this, "initialize");
        this._loadDelegate = Function.createDelegate(this, this.loadHandler);
        Sys.Application.add_load(this._loadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField.callBaseMethod(this, "dispose");
        if (this._loadDelegate) {
            delete this._loadDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    //changing the culture of the caller's manager
    saveChanges: function (dataItem, successCallback, failureCallback, caller) {
        var binder = caller;
        if (binder && binder.get_manager) {
            if (this._enabled) {
                var manager = binder.get_manager();
                manager.set_uiCulture(this.get_value());
            }
            if (successCallback) {
                successCallback.apply(caller);
            }
        }
        else {
            throw "LanguageChoiceField's self executing functionality doesn't support this caller";
        }
    },

    isChanged: function () {
        return false;
    },

    set_value: function (value) {
        if (value === null) {
            this._clearItems();
        }

        if (this._enabled && jQuery.isArray(value)) {
            var availableLanguages = value;
            //We do not want to raise the value changed while binding the drop down.
            this._unsubscribeForValueChanged();
            //append all selector items which value is not contained in the available languages and remove those that are not.
            for (var itemValue in this._itemsMap) {
                var jItem = jQuery(this._itemsMap[itemValue]);
                //fixing an issue with jQuery 1.3.2 'losing' the value of the option element.
                jItem.val(itemValue);

                if (!itemValue) {
                    this._removeSelectorItem(jItem);
                    continue;
                }

                if (jQuery.inArray(jItem.val(), availableLanguages) < 0) {
                    if (this.get_languagesToShow() === this._languageSelectionEnum.Unavailable)
                        this._addSelectorItem(jItem);
                    else if (this.get_languagesToShow() === this._languageSelectionEnum.Available)
                        this._removeSelectorItem(jItem);
                    else
                        throw 'The value being set as languagesToShow is not supported';
                }
                else {
                    if (this.get_languagesToShow() === this._languageSelectionEnum.Unavailable)
                        this._removeSelectorItem(jItem);
                    else if (this.get_languagesToShow() === this._languageSelectionEnum.Available)
                        this._addSelectorItem(jItem);
                    else
                        throw 'The value being set as languagesToShow is not supported';
                }
            }
            this._subscribeForValueChanged();
            this._selectFirstItem();
        }
    },

    _addSelectorItem: function (jItem) {
        if (this._removedLanguages[jItem.val()]) {
            jItem.appendTo(jQuery(this._choiceElement));
            this._removedLanguages[jItem.val()] = false;
        }
    },

    _removeSelectorItem: function (jItem) {
        if (!this._removedLanguages[jItem.val()]) {
            jItem.remove();
            this._removedLanguages[jItem.val()] = true;
        }
    },

    set_selectedLanguage: function (language, throwIfNotFound) {
        if (throwIfNotFound === undefined || throwIfNotFound === null) {
            throwIfNotFound = this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
        }
        var selector = this._get_listItemSelector() + "[value='" + language + "']"
        var jDropDown = jQuery(this._choiceElement);
        if (this._choiceElement) {
            //            jDropDown.val(language);
            selectElm = jDropDown.find(selector);
            if (selectElm) {
                selectElm.attr('selected', 'selected');
            }
        }
        else {
            if (throwIfNotFound) {
                throw "The language selector doesn't have item with value: " + language;
            }
        }
    },

    enable: function () {
        this._enabled = true;
        this.show();
    },

    disable: function () {
        this._enabled = false;
        this.hide();
    },

    show: function () {
        jQuery(this._element).show();
    },

    hide: function () {
        jQuery(this._element).hide();
    },

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField.callBaseMethod(this, "reset");
        this.enable();
        this.set_value([]);
    },
    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    loadHandler: function () {
        //Getting all option items and removing them from the selector so that they can be assigned later.
        if (this._choices) {
            var itemsLength = this._choices.length;
            for (var i = 0; i < itemsLength; i++) {
                var jItem = this._get_listItemByValue(this._choices[i].Value);
                this._itemsMap[jItem.val()] = jItem;
            }
        }
    },

    /* -------------------- private methods ----------- */

    _clearItems: function () {
        if (this._choices) {
            for (var itemValue in this._itemsMap) {
                var jItem = jQuery(this._itemsMap[itemValue]);
                this._removeSelectorItem(jItem);
            }
        }
    },

    _selectFirstItem: function () {
        if (this._choices && this._choices.length > 0) {
            var jFirstItem = this._get_listItemByValue(this._choices[0].Value);
            if (jFirstItem && jFirstItem.val()) {
                this._selectItemByValue(jFirstItem.val());
            }
        }
    },

    _selectItemByValue: function (value) {
        var jDropDown = jQuery(this._choiceElement);
        jDropDown.val(value);
    },

    /* -------------------- properties ---------------- */

    get_enabled: function () {
        return this._enabled;
    },
    set_enabled: function () {
        if (this._enabled !== value) {
            this._enabled = value;
            if (this._enabled)
                this.enable();
            else
                this.disable();
        }
    },

    get_languagesToShow: function () {
        return this._languagesToShow;
    },
    set_languagesToShow: function (value) {
        this._languagesToShow = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField.registerClass("Telerik.Sitefinity.Web.UI.Fields.LanguageChoiceField", Telerik.Sitefinity.Web.UI.Fields.ChoiceField, Telerik.Sitefinity.Web.UI.ISelfExecutableField);
