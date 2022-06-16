Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.CacheProfileField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.CacheProfileField.initializeBase(this, [element]);
    this._profileSelect = null;
    this._detailsButton = null;
    this._detailsContainer = null;
    this._detailsDateView = null;
    this._profileDetails = null;

    this._profileChangedDelegate = null;
    this._detailsClickedDelegate = null;
    this._loadDelegate = null;
    this._resetDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.CacheProfileField.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.CacheProfileField.callBaseMethod(this, 'initialize');
        this._profileChangedDelegate = Function.createDelegate(this, this._profileChanged);
        this._profileSelect.add_valueChanged(this._profileChangedDelegate);
        this._detailsClickedDelegate = Function.createDelegate(this, this._detailsClicked);
        $addHandler(this._detailsButton, "click", this._detailsClickedDelegate);
        this._resetDelegate = Function.createDelegate(this, this._reset);
        this._profileSelect.add_reset(this._resetDelegate);
        this._loadDelegate = Function.createDelegate(this, this._loadHandler);
        Sys.Application.add_load(this._loadDelegate);
    },
    dispose: function () {
        this._profileSelect.remove_valueChanged(this._profileChangedDelegate);
        delete this._profileChangedDelegate;
        this._profileSelect.remove_reset(this._resetDelegate);
        delete this._resetDelegate;
        $removeHandler(this._detailsButton, "click", this._detailsClickedDelegate);
        delete this._detailsClickedDelegate;
        this._profileSelect = null;
        Telerik.Sitefinity.Web.UI.Fields.CacheProfileField.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */

    _profileChanged: function (sender) {
        var selectedProfileIndex = this._profileSelect.get_selectedChoicesIndex();
        if (selectedProfileIndex)
            this._detailsDateView.set_data(this._profileDetails[selectedProfileIndex[0]]);
    },

    _detailsClicked: function () {
        $(this._detailsContainer).toggleClass("sfDisplayNone");
    },

    _reset: function () {
        $(this._detailsContainer).toggleClass("sfDisplayNone", true);
    },

    _loadHandler: function (e) {
        this._detailsDateView = $create(Sys.UI.DataView, {}, {}, {}, this._detailsContainer);
        this._detailsDateView.set_data(this._profileDetails[0]);
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */


    get_profileSelect: function () {
        return this._profileSelect;
    },
    set_profileSelect: function (value) {
        this._profileSelect = value;
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
    },
    get_profileDetails: function () {
        return this._profileDetails;
    },
    set_profileDetails: function (value) {
        this._profileDetails = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.CacheProfileField.registerClass('Telerik.Sitefinity.Web.UI.Fields.CacheProfileField', Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl);