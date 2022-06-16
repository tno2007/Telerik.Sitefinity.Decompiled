Type.registerNamespace("Telerik.Sitefinity.Modules.GenericContent.Web.UI");

Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector = function (element) {
    this._contentTypes = null;
    this._contentTypeSelectorViewName = null;
    this._linkDoneBtn = null;
    this._linkCancelBtn = null;

    Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector.callBaseMethod(this, "dispose");
    },

    /* -------------------- Public methods ---------------- */

    /* -------------------- Event handlers ---------------- */  

    /* -------------------- properties ---------------- */

    get_linkDoneBtn: function () {
        return this._linkDoneBtn;
    },

    set_linkDoneBtn: function (value) {
        this._linkDoneBtn = value;
    },

    get_linkCancelBtn: function () {
        return this._linkCancelBtn;
    },

    set_linkCancelBtn: function (value) {
        this._linkCancelBtn = value;
    },

    get_contentTypeSelectorViewName: function () {
        return this._contentTypeSelectorViewName;
    },

    set_contentTypeSelectorViewName: function (value) {
        this._contentTypeSelectorViewName = value;
    },

    get_contentTypes: function() {
        return this._contentTypes;
    },

    set_contentTypes: function(value) {
        this._contentTypes = value;    
    }
};

Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector.registerClass("Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentTypesSelector", Sys.UI.Control);

