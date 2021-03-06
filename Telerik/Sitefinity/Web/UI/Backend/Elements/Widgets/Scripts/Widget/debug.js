
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget.initializeBase(this, [element]);

    // ------------------ IWidget members -----------------------
    this._name = null;
    this._cssClass = null;
    this._isSeparator = null;
    this._wrapperTagId = null;
    this._wrapperTagName = null;
    this._wrapperTagKey = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget.callBaseMethod(this, 'initialize');

    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget.callBaseMethod(this, 'dispose');
    },

    /* ------------------------------------ Private functions --------------------------------- */

    /* ------------------------------------ Public Methods ----------------------------------- */

    /* ------------------------------------- Properties --------------------------------------- */

    /* ------------------- IWidget members ------------------- */

    get_name: function () {
        return this._name;
    },
    set_name: function (value) {
        if (this._name != value) {
            this._name = value;
            this.raisePropertyChanged('name');
        }
    },
    get_cssClass: function () {
        return this._cssClass;
    },
    set_cssClass: function (value) {
        if (this._cssClass != value) {
            this._cssClass = value;
            this.raisePropertyChanged('cssClass');
        }
    },

    get_isSeparator: function () {
        return this._isSeparator;
    },
    set_isSeparator: function (value) {
        if (this._isSeparator != value) {
            this._isSeparator = value;
            this.raisePropertyChanged('isSeparator');
        }
    },

    get_wrapperTagId: function () {
        return this._wrapperTagId;
    },
    set_wrapperTagId: function (value) {
        if (this._wrapperTagId != value) {
            this._wrapperTagId = value;
            this.raisePropertyChanged('wrapperTagId');
        }
    },

    get_wrapperTagName: function () {
        return this._wrapperTagName;
    },
    set_wrapperTagName: function (value) {
        if (this._wrapperTagName != value) {
            this._wrapperTagName = value;
            this.raisePropertyChanged('wrapperTagName');
        }
    },
    get_wrapperTagKey: function () {
        return this._wrapperTagKey;
    },
    set_wrapperTagKey: function (value) {
        if (this._wrapperTagKey != value) {
            this._wrapperTagKey = value;
            this.raisePropertyChanged('wrapperTagKey');
        }
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget", Sys.UI.Control, Telerik.Sitefinity.UI.IWidget);
