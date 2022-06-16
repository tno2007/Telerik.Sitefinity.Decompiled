﻿// called by the DetailFormView when it is loaded
function OnDetailViewLoaded(sender, args) {
    // the sender here is DetailFormView
    var currentForm = sender;
    Sys.Application.add_init(function () {
        $create(Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension,
                { _detailFormView: currentForm },
                {},
                {},
                null);
    });
}

Type.registerNamespace("Telerik.Sitefinity.Modules.UserProfiles.Web.UI");

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension = function () {
    Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension.initializeBase(this);
    // Main components
    this._detailFormView = {};
    this._binder = null;
    this._devNameField = null;
    
    // Event delegates
    this._formClosingDelegate = null;
}

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension.callBaseMethod(this, "initialize");
        this._binder = this._detailFormView.get_binder();

        this._devNameField = this._getFieldControlByDataFieldName("Name");

        this._formClosingDelegate = Function.createDelegate(this, this._formClosingHandler);
        this._detailFormView.add_formClosing(this._formClosingDelegate);

        this._formCreatedDelegate = Function.createDelegate(this, this._formCreatedHandler);
        this._detailFormView.add_formCreated(this._formCreatedDelegate);
    },

    dispose: function () {
        this._detailFormView.remove_formClosing(this._formClosingDelegate);
        delete this._formClosingDelegate;

        if (this._formCreatedDelegate) {
            if (this._detailFormView) {
                this._detailFormView.remove_formCreated(this._formCreatedDelegate);
            }
            delete this._formCreatedDelegate;
        }

        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _formClosingHandler: function (sender, args) {
        var form = sender;
        var commandArgument = args.get_commandArgument();
        var isNew = args.get_isNew();
        if (form.get_isMultilingual() && commandArgument) {
            isNew = commandArgument.languageMode == "create";
        }
        if (isNew && args.get_isDirty()) {
            dialogBase.closeCreated(args.get_dataItem(), form);
            args.set_cancel(true);
        }
    },

    // Fired when a form is created. Args are of type 'Telerik.Sitefinity.FormCreatedEventArgs'
    _formCreatedHandler: function (sender, args) {
        var dataItem = this._binder.get_dataItem();
        this._dataItem = dataItem;

        if (!args.get_isNew() && args.get_commandName() == "edit") {
            this._devNameField.set_isToMirror(false);
            this._devNameField._set_isToShowAsLabel(true);
            jQuery(this._devNameField.get_changeControl()).hide();
        }
    },

    /* -------------------- properties ---------------- */


    _getFieldControlByDataFieldName: function (dataFieldName) {
        return this._binder.getFieldControlByDataFieldName(dataFieldName);
    }


}

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension.registerClass("Telerik.Sitefinity.Modules.UserProfiles.Web.UI.DetailFormViewExtension", Sys.Component, Sys.IDisposable);
