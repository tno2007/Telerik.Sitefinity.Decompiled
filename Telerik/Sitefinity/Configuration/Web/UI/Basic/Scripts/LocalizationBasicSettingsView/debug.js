﻿Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.initializeBase(this, [element]);
    this._frontendLanguagesField = null;
    this._backendLanguagesField = null;
    this._backendLanguagesSelect = null;
    this._languagesSelectContainer = null;
    this._languagesListContainer = null;
    this._manageLanguagesList = null;
    this._closeLanguagesList = null;
    this._subdomainList = [];
    this._subdomainsListElement = null;
    this._subdomainListDataView = null;
    this._subdomainsContainer = null;
    this._directoriesButton = null;
    this._domainsButton = null;
    this._domainStrategyValue = null;
    this._radioButtonList = null;
    this._domainRegex = null;

    this._frontendLanguagesFieldValueChangedDelegate = null;
    this._backendLanguagesFieldValueChangedDelegate = null;
    this._backendLanguagesSelectValueChangedDelegate = null;
    this._manageLanguagesListDelegate = null;
    this._closeLanguagesListDelegate = null;
    this._strategyButtonClickedDelegate = null;
    this._onLoadDelegate = null;
    this._languagesSelectionDoneDelegate = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "initialize");

        this._frontendLanguagesFieldValueChangedDelegate = Function.createDelegate(this, this._frontendLanguagesFieldValueChanged);

        if (this._frontendLanguagesField != null) {
            this._frontendLanguagesField.add_valueChanged(this._frontendLanguagesFieldValueChangedDelegate);
        }

        this._backendLanguagesFieldValueChangedDelegate = Function.createDelegate(this, this._backendLanguagesFieldValueChanged);
        this._backendLanguagesField.add_valueChanged(this._backendLanguagesFieldValueChangedDelegate);
        this._backendLanguagesSelectValueChangedDelegate = Function.createDelegate(this, this._backendLanguagesSelectValueChanged);
        this._backendLanguagesSelect.add_valueChanged(this._backendLanguagesSelectValueChangedDelegate);
        this._languagesSelectionDoneDelegate = Function.createDelegate(this, this._languagesSelectionDoneHandler);

        if (this._frontendLanguagesField != null) {
            this._frontendLanguagesField.add_selectionDone(this._languagesSelectionDoneDelegate);
        }

        this._backendLanguagesField.add_selectionDone(this._languagesSelectionDoneDelegate);
        this._manageLanguagesListDelegate = Function.createDelegate(this, this._manageLanguagesListHandler);
        $addHandler(this._manageLanguagesList, "click", this._manageLanguagesListDelegate);
        this._closeLanguagesListDelegate = Function.createDelegate(this, this._closeLanguagesListHandler);
        $addHandler(this._closeLanguagesList, "click", this._closeLanguagesListDelegate);
        this._strategyButtonClickedDelegate = Function.createDelegate(this, this._strategyButtonClicked);
        if (this._directoriesButton) {
            $addHandler(this._directoriesButton, "click", this._strategyButtonClickedDelegate);
        }
        if (this._domainsButton) {
            $addHandler(this._domainsButton, "click", this._strategyButtonClickedDelegate);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "dispose");

        if (this._frontendLanguagesField != null) {
            this._frontendLanguagesField.remove_valueChanged(this._frontendLanguagesFieldValueChangedDelegate);
        }

        delete this._frontendLanguagesFieldValueChangedDelegate;
        this._backendLanguagesField.remove_valueChanged(this._backendLanguagesFieldValueChangedDelegate);
        delete this._backendLanguagesFieldValueChangedDelegate;
        this._backendLanguagesSelect.remove_valueChanged(this._backendLanguagesSelectValueChangedDelegate);
        delete this._backendLanguagesSelectValueChangedDelegate;

        if (this._frontendLanguagesField != null) {
            this._frontendLanguagesField.remove_selectionDone(this._languagesSelectionDoneDelegate);
        }

        this._backendLanguagesField.remove_selectionDone(this._languagesSelectionDoneDelegate);
        delete this._languagesSelectionDoneDelegate;
        $removeHandler(this._manageLanguagesList, "click", this._manageLanguagesListDelegate);
        delete this._manageLanguagesListDelegate;
        $removeHandler(this._closeLanguagesList, "click", this._closeLanguagesListDelegate);
        delete this._closeLanguagesListDelegate;
        if (this._domainsButton) {
            $removeHandler(this._domainsButton, "click", this._strategyButtonClickedDelegate);
        }
        if (this._directoriesButton) {
            $removeHandler(this._directoriesButton, "click", this._strategyButtonClickedDelegate);
        }
        delete this._strategyButtonClickedDelegate;
    },

    /* --------------------  public methods ----------- */

    saveChanges: function () {
        if (this._directoriesButton) {
            this._setStrategy(this._directoriesButton);
        }
        if (this._domainsButton) {
            this._setStrategy(this._domainsButton);
        }
        if (this._dataItem.DefaultLocalizationStrategy === this._domainStrategyValue) {
            this._dataItem.SubdomainStrategySettings = this._subdomainList;
        }
        Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "saveChanges");
    },

    binderEndProcessing: function () {
        if (this._loadingViewClone) {
            Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "dataBind");
            this._loadingViewClone.remove();
            this._loadingViewClone = null;
            $(this._buttonsArea).children().show();
        }
    },

    validate: function () {
        var isBaseValid = Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "validate");
        var isValid = this._validate();
        return isValid && isBaseValid;
    },

    /* --------------------  events handlers ----------- */
    _load: function (sender, args) {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "_load");
        this._updateLanguagesSelect();
        if (this._subdomainsListElement) {
            this._subdomainListDataView = $create(Sys.UI.DataView, {}, {}, {}, this._subdomainsListElement);
        }
    },

    _dataBindSuccess: function (sender, result) {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.callBaseMethod(this, "_dataBindSuccess", [sender, result]);
        if (this._dataItem.SubdomainStrategySettings.length > 0) {
            this._subdomainList = this._dataItem.SubdomainStrategySettings;
            Sys.Observer.makeObservable(this._subdomainList);
            if (this._subdomainListDataView) {
                this._subdomainListDataView.set_data(this._subdomainList);
            }
        }
        var strategyKey = this._dataItem.DefaultLocalizationStrategy;
        jQuery(this._directoriesButton).add(this._domainsButton).each(function (i, el) {
            var jel = jQuery(el);
            if (jel.val() === strategyKey)
                jel.click();
        });
    },

    _frontendLanguagesFieldValueChanged: function (sender, args) {
        var frontendLanguages = this._frontendLanguagesField.get_value();
        if (frontendLanguages) {
            jQuery(this._radioButtonList).toggleClass("sfDisplayNone", frontendLanguages.length <= 1);
            if (!this._subdomainList.beginUpdate) return; //if it is not observed
            if (this._subdomainList.length > frontendLanguages.length) { //if some language is removed
                for (var i = this._subdomainList.length - 1; i > -1; i--) {
                    var item = this._subdomainList[i];
                    var found = false;
                    jQuery.each(frontendLanguages, function (ind, el) {
                        if (el.Key === item.Key) {
                            found = true;
                            return false;
                        }
                    });
                    if (!found) {//the language is removed
                        this._subdomainList.beginUpdate();
                        this._subdomainList.remove(item);
                        this._subdomainList.endUpdate();
                    }
                }
            }
            else if (this._subdomainList.length < frontendLanguages.length) { //if some language is added
                for (var i = 0, l = frontendLanguages.length; i < l; i++) {
                    var lang = frontendLanguages[i];
                    var found = false;
                    jQuery.each(this._subdomainList, function (ind, el) {
                        if (el.Key === lang.Key) {
                            found = true;
                            return false;
                        }
                    });
                    if (!found) {//the language is added
                        this._subdomainList.beginUpdate();
                        this._subdomainList.add({ Key: lang.Key, Setting: "", IsDefault: lang.IsDefault, DisplayName: lang.DisplayName });
                        this._subdomainList.endUpdate();
                    }
                }
            }
        }
    },

    _backendLanguagesFieldValueChanged: function (sender, args) {
        this._updateLanguagesSelect();
    },

    _backendLanguagesSelectValueChanged: function (sender, args) {
        this._updateLanguagesList();
    },

    _manageLanguagesListHandler: function (sender, args) {
        jQuery(this._languagesSelectContainer).addClass("sfDisplayNone");
        jQuery(this._languagesListContainer).removeClass("sfDisplayNone");
    },

    _closeLanguagesListHandler: function (sender, args) {
        jQuery(this._languagesSelectContainer).removeClass("sfDisplayNone");
        jQuery(this._languagesListContainer).addClass("sfDisplayNone");
    },

    _strategyButtonClicked: function (sender, args) {
        jQuery(this._subdomainsContainer).toggleClass("sfDisplayNone", jQuery(sender.target).val() !== this._domainStrategyValue);
    },

    _languagesSelectionDoneHandler: function (sender, args) {
        var error = args.get_error();
        if (error) {
            this._scrollTop();
            this._message.showNegativeMessage(error.message);
        }
    },

    /* -------------------- events -------------------- */

    /* --------------------  private methods ----------- */
    _validate: function () {
        var isValid = true;
        if (this._dataItem.DefaultLocalizationStrategy === this._domainStrategyValue) {
            isValid = isValid && this._validateSubdomainSettings();
        }
        return isValid;
    },
    _validateSubdomainSettings: function () {
        var isValid = true;
        this._subdomainList.beginUpdate();
        for (var i = 0, l = this._subdomainList.length; i < l; i++) {
            var item = this._subdomainList[i];
            delete item.ErrorMsg;
            if (!item.Setting) {
                item.ErrorMsg = this._clientLabelManager.getLabel('Labels', 'Required');
                isValid = false;
            }
            //TODO change the domain regex or remove the validation
            //             else {
            //                 var domainRegex = new RegExp(this._domainRegex);
            //                 if (!domainRegex.test(item.Setting)) {
            //                     item.ErrorMsg = this._clientLabelManager.getLabel('Labels', 'InvalidDomain');
            //                     isValid = false;
            //                 }
            //             }
        }
        this._subdomainList.endUpdate();
        if (this._subdomainListDataView) {
            this._subdomainListDataView.refresh();
        }
        return isValid;
    },
    _updateLanguagesSelect: function () {
        var backendLanguages = this._backendLanguagesField.get_value();
        var selectField = this._backendLanguagesSelect;
        if (backendLanguages) {
            selectField.clearListItems();
            for (var i = 0, l = backendLanguages.length; i < l; i++) {
                var lang = backendLanguages[i];
                selectField.addListItem(lang.Key, lang.DisplayName);
                if (lang.IsDefault)
                    selectField.selectListItemsByValue(lang.Key);
            }
        }
    },
    _updateLanguagesList: function () {
        var selectedLanguage = this._backendLanguagesSelect.get_value();
        var defaultLanguage = this._backendLanguagesField.getDefaultCulture();
        if (defaultLanguage !== selectedLanguage) {
            this._backendLanguagesField.setDefaultCulture(selectedLanguage);
        }
    },
    _setStrategy: function (elem) {
        var el = jQuery(elem);
        if (el.attr("checked")) {
            this._dataItem.DefaultLocalizationStrategy = el.val();
        }
    },
    /* -------------------- properties ---------------- */

    get_frontendLanguagesField: function () {
        return this._frontendLanguagesField;
    },
    set_frontendLanguagesField: function (value) {
        this._frontendLanguagesField = value;
    },
    get_backendLanguagesField: function () {
        return this._backendLanguagesField;
    },
    set_backendLanguagesField: function (value) {
        this._backendLanguagesField = value;
    },
    get_backendLanguagesSelect: function () {
        return this._backendLanguagesSelect;
    },
    set_backendLanguagesSelect: function (value) {
        this._backendLanguagesSelect = value;
    },
    get_languagesSelectContainer: function () {
        return this._languagesSelectContainer;
    },
    set_languagesSelectContainer: function (value) {
        this._languagesSelectContainer = value;
    },
    get_languagesListContainer: function () {
        return this._languagesListContainer;
    },
    set_languagesListContainer: function (value) {
        this._languagesListContainer = value;
    },
    get_manageLanguagesList: function () {
        return this._manageLanguagesList;
    },
    set_manageLanguagesList: function (value) {
        this._manageLanguagesList = value;
    },
    get_closeLanguagesList: function () {
        return this._closeLanguagesList;
    },
    set_closeLanguagesList: function (value) {
        this._closeLanguagesList = value;
    },
    get_subdomainsListElement: function () {
        return this._subdomainsListElement;
    },
    set_subdomainsListElement: function (value) {
        this._subdomainsListElement = value;
    },
    get_subdomainsContainer: function () {
        return this._subdomainsContainer;
    },
    set_subdomainsContainer: function (value) {
        this._subdomainsContainer = value;
    },
    get_directoriesButton: function () {
        return this._directoriesButton;
    },
    set_directoriesButton: function (value) {
        this._directoriesButton = value;
    },
    get_domainsButton: function () {
        return this._domainsButton;
    },
    set_domainsButton: function (value) {
        this._domainsButton = value;
    },
    get_domainStrategyValue: function () {
        return this._domainStrategyValue;
    },
    set_domainStrategyValue: function (value) {
        this._domainStrategyValue = value;
    },
    get_radioButtonList: function () {
        return this._radioButtonList;
    },
    set_radioButtonList: function (value) {
        this._radioButtonList = value;
    },
    get_domainRegex: function () {
        return this._domainRegex;
    },
    set_domainRegex: function (value) {
        this._domainRegex = value;
    }
};

Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.LocalizationBasicSettingsView", Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView);