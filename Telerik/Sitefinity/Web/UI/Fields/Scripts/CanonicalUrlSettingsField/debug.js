Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField.initializeBase(this, [element]);
    this._canonicalUrlSettingsSelect = null;
    this._detailsButton = null;
    this._detailsContainer = null;

    this._detailsClickedDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField.callBaseMethod(this, 'initialize');

        this._detailsClickedDelegate = Function.createDelegate(this, this._detailsClicked);
        $addHandler(this._detailsButton, "click", this._detailsClickedDelegate);
    },
    dispose: function () {
        this._canonicalUrlSettingsSelect = null;

        $removeHandler(this._detailsButton, "click", this._detailsClickedDelegate);
        delete this._detailsClickedDelegate;

        Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    _detailsClicked: function () {
        $(this._detailsContainer).toggleClass("sfDisplayNone");
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    get_canonicalUrlSettingsSelect: function () {
        return this._canonicalUrlSettingsSelect;
    },
    set_canonicalUrlSettingsSelect: function (value) {
        this._canonicalUrlSettingsSelect = value;
    },
    get_detailsButton: function () {
        return this._detailsButton;
    },
    set_detailsButton: function (value) {
        this._detailsButton = value;
    },
    get_detailsContainer: function () {
        return this._detailsContainer;
    },
    set_detailsContainer: function (value) {
        this._detailsContainer = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField.registerClass('Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField', Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl);