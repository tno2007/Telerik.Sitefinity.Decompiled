Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Type._registerScript("LanguageSelectorDesigner.js", ["IControlDesigner.js"]);
Type.registerNamespace("Telerik.Sitefinity.Localization.Web.UI.Designers");

// ------------------------------------------------------------------------
// LanguageSelectorDesigner class
// ------------------------------------------------------------------------

Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner = function (element) {

    Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner.initializeBase(this, [element]);
    this._propertyEditor = null;
    this._controlData = null;

    this._includeCurrentCheckbox = null;

    this._selectorTypeChangedDelegate = null;
    this._noTranslationActionChangedDelegate = null;
    this._includeCurrentChangedDelegate = null;
}

Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner.callBaseMethod(this, 'initialize');

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._selectorTypeChangedDelegate = Function.createDelegate(this, this._selectorTypeChanged);
        this._setRadioClickHandler("SelectorType", this._selectorTypeChangedDelegate);

        this._noTranslationActionChangedDelegate = Function.createDelegate(this, this._noTranslationActionChanged);
        this._setRadioClickHandler("ActionType", this._noTranslationActionChangedDelegate);

        this._includeCurrentChangedDelegate = Function.createDelegate(this, this._includeCurrentChanged);
        this._includeCurrentCheckbox.add_valueChanged(this._includeCurrentChangedDelegate);

    },

    dispose: function () {
        if (this._selectorTypeChangedDelegate) {
            delete this._selectorTypeChangedDelegate;
        }
        if (this._noTranslationActionChangedDelegate) {
            delete this._noTranslationActionChangedDelegate;
        }
        if (this._includeCurrentChangedDelegate) {
            this._includeCurrentCheckbox.remove_valueChanged(this._includeCurrentChangedDelegate);
            delete this._includeCurrentChangedDelegate;
        }

        Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // refereshed the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this desinger.
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (!controlData) {
            return;
        }

        this._clickRadioChoice("SelectorType", controlData.SelectorType);
        this._clickRadioChoice("ActionType", controlData.MissingTranslationAction);
        this.get_includeCurrentCheckbox().set_value(controlData.ShowCurrentLanguage);
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        //Changes are already applied
    },

    /* --------------------------------- event handlers --------------------------------- */

    _selectorTypeChanged: function (sender) {
        this.get_controlData().SelectorType = sender.target.value;
    },

    _noTranslationActionChanged: function (sender) {
        this.get_controlData().MissingTranslationAction = sender.target.value;
    },

    _includeCurrentChanged: function () {
        this.get_controlData().ShowCurrentLanguage = this.get_includeCurrentCheckbox().get_value();
    },

    /* --------------------------------- private methods --------------------------------- */

    //utility method to set radio group click handler
    _setRadioClickHandler: function (groupName, delegate) {
        jQuery(this.get_element()).find("input[name='" + groupName + "']").click(delegate)
    },

    //utility method to get a radio button option
    _getRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").get(0);
    },

    //utility method to to click a radio group option
    _clickRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").click();
    },

    /* --------------------------------- properties --------------------------------- */

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    // gets the reference to the content selector used to choose content item
    get_includeCurrentCheckbox: function () {
        return this._includeCurrentCheckbox;
    },

    // gets the reference to the content selector used to choose one or more content item
    set_includeCurrentCheckbox: function (value) {
        this._includeCurrentCheckbox = value;
    }
}
Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner.registerClass('Telerik.Sitefinity.Localization.Web.UI.Designers.LanguageSelectorDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
