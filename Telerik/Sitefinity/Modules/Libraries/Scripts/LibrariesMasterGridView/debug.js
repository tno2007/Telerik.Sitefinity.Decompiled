Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries");

Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView.initializeBase(this, [element]);

    this._isParentEditable = null;
    this._isParentDeletable = null;
    this._parentUrlName = null;
    this._libraryType = null;
    this._libraryServiceUrl = null;
    this._runningTaskId = null;
}

Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView.callBaseMethod(this, "initialize");

    },

    dispose: function () {

        Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView.callBaseMethod(this, "dispose");
    },

    get_isParentEditable: function () {
        return this._isParentEditable;
    },
    set_isParentEditable: function (value) {
        this._isParentEditable = value;
    },

    get_isParentDeletable: function () {
        return this._isParentDeletable;
    },
    set_isParentDeletable: function (value) {
        this._isParentDeletable = value;
    },

    get_parentUrlName: function () {
        return this._parentUrlName;
    },
    set_parentUrlName: function (value) {
        this._parentUrlName = value;
    },

    get_libraryType: function () {
        return this._libraryType;
    },
    set_libraryType: function (value) {
        this._libraryType = value;
    },

    get_libraryServiceUrl: function () {
        return this._libraryServiceUrl;
    },
    set_libraryServiceUrl: function (value) {
        this._libraryServiceUrl = value;
    },

    get_runningTaskId: function () {
        return this._runningTaskId;
    },
    set_runningTaskId: function (value) {
        this._runningTaskId = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView.registerClass("Telerik.Sitefinity.Modules.Libraries.LibrariesMasterGridView", Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView);