﻿//Type._registerScript("ICommandWidget.js", ["ICommandWidget.js"]);
//Type._registerScript("IWidget.js", ["IWidget.js"]);

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget.initializeBase(this, [element]);
    // ------------------ IWidget members -----------------------
    this._name = null;
    this._cssClass = null;
    this._isSeparator = null;
    this._wrapperTagId = null;
    this._wrapperTagName = null;
    this._binderId = null;
    this._binder = null;
    this._serviceBaseUrl = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget.callBaseMethod(this, 'dispose');
    },

    /* ------------------------------------ Public Methods ----------------------------------- */

    dataBind: function (dataItem) {
        this.get_binder().DataBind(dataItem);
    },

    clear: function () {
        var templateElement = $get("ciwTemplateHolder");
        templateElement.innerHTML = "";
    },

    /* ------------------------------------- Properties --------------------------------------- */

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

    get_binder: function () {
        if (this._binder == null) {
            this._binder = $find(this._binderId);
            this._binder.set_serviceBaseUrl(this._serviceBaseUrl);
        }
        return this._binder;
    },

    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;

        if (this._binder != null) {
            this._binder.set_serviceBaseUrl(this._serviceBaseUrl);
        }
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget", Sys.UI.Control, Telerik.Sitefinity.UI.IWidget);