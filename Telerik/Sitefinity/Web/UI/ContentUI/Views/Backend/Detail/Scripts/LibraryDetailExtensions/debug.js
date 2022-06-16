﻿// called by the DetailFormView when it is loaded
function OnDetailViewLoaded(sender, args) {
    // the sender here is DetailFormView
    var detailFormView = sender;

    Sys.Application.add_init(function () {
        $create(Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions,
        { _detailFormView: detailFormView },
        {},
        {},
        null);
    });
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions = function () {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions.initializeBase(this);
    // Main components
    this._detailFormView = {};
    this._binder = null;
    this._parentLibraryField = null;
    this._rootLibrarySettings = null;

    this._parentLibraryHasParentChangedDelegate = null;
    this._parentLibraryFieldDataBoundDelegate = null;
    this._saveDetailDelegate = null;
    this._saveChangesOriginal = null;
    this._saveChangesFinalDelegate = null;
    this._workflowOperations = null;    
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions.callBaseMethod(this, "initialize");

        this.set_parentLibraryField(this._getFieldControlByDataFieldName("ParentId"));
        this.set_rootLibrarySettings(this._getSectionByName("RootLibrarySection"));

        this._parentLibraryHasParentChangedDelegate = Function.createDelegate(this, this._parentLibraryHasParentChangedHandler);
        this.get_parentLibraryField().add_hasParentChanged(this._parentLibraryHasParentChangedDelegate);

        this._parentLibraryFieldDataBoundDelegate = Function.createDelegate(this, this._parentLibraryFieldDataBoundHandler);
        this.get_parentLibraryField().add_dataBound(this._parentLibraryFieldDataBoundDelegate);

        this._saveDetailDelegate = Function.createDelegate(this, this.saveChanges);
        this._saveChangesOriginal = Function.createDelegate(detailFormView, detailFormView.saveChanges);
        this._saveChangesFinalDelegate = Function.createDelegate(this, this._saveItemChangesFinal);
        detailFormView.saveChanges = this._saveDetailDelegate;
    },

    dispose: function () {
        if (this._parentLibraryHasParentChangedDelegate) {
            if (this.get_parentLibraryField()) {
                this.get_parentLibraryField().remove_hasParentChanged(this._parentLibraryHasParentChangedDelegate);
            }
            delete this._parentLibraryHasParentChangedDelegate;
        }

        if (this._parentLibraryFieldDataBoundDelegate) {
            if (this.get_parentLibraryField()) {
                this.get_parentLibraryField().remove_dataBound(this._parentLibraryFieldDataBoundDelegate);
            }
            delete this._parentLibraryFieldDataBoundDelegate;
        }

        if (this._saveDetailDelegate) 
            delete this._saveDetailDelegate;        

        if (this._saveChangesFinalDelegate)
            delete this._saveChangesFinalDelegate;

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions.callBaseMethod(this, "dispose");
    },


    /* --------------------  public methods ----------- */
    saveChanges: function (workflowOperation) {
        this._workflowOperations = workflowOperation;        

        
        var isParentIdInitialized = function (that) {
            return (!detailFormView._dataItem.Item.ParentId && that.get_parentLibraryField().get_value() && that.get_parentLibraryField().get_value() != Telerik.Sitefinity.getEmptyGuid());
        };

        var isParentIdUpdated = function (that) {
            return (detailFormView._dataItem.Item.ParentId && detailFormView._dataItem.Item.ParentId != that.get_parentLibraryField().get_value());
        };
        
        if (!detailFormView._isNew && (isParentIdInitialized(this) || isParentIdUpdated(this))) {
            var dialog = this.get_detailFormView().getPromptDialogByName("confirmMove");

            var selectedLibrary = this.get_parentLibraryField().get_parentLibrarySelector()._selectedPage;

            if (this.get_parentLibraryField().get_value() != Telerik.Sitefinity.getEmptyGuid() && selectedLibrary)
                dialog.set_message(String.format(this.get_detailFormView().get_clientLabelManager().getLabel("LibrariesResources", "MoveLibraryWarning"), selectedLibrary.Title));
            else
                dialog.set_message(this.get_detailFormView().get_clientLabelManager().getLabel("LibrariesResources", "MoveToRootLibraryWarning"));
            
            dialog.show_prompt('', '', this._saveChangesFinalDelegate);
        }
        else {
            this._saveChangesOriginal(this._workflowOperations);
        }

    },

    /* -------------------- event handlers ------------ */

    _parentLibraryHasParentChangedHandler: function (sender, args) {        
        if (args && this.get_rootLibrarySettings()) {
            jQuery(this.get_rootLibrarySettings().get_element()).toggle(!args.get_hasParent());
        }
    },

    _parentLibraryFieldDataBoundHandler: function (sender, args) {
        if (!detailFormView._isNew)
            return;

        var query = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var folderId = query.get("folderId", null);
        if (folderId && folderId != "undefined" && folderId != "null") {
            this.get_parentLibraryField().set_value(folderId);
        }
        else {
            var parentId = query.get("parentId", null);
            if (parentId) {
                this.get_parentLibraryField().set_value(parentId);
            }
        }
    },    

    /* -------------------- private methods ----------- */
    _saveItemChangesFinal: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName == 'cancel') {
            detailFormView.asyncEndProcessing();
            return;
        }

        this._saveChangesOriginal(this._workflowOperations);
    },

    _getFieldControlByDataFieldName: function (dataFieldName) {
        return this.get_detailFormView().get_binder().getFieldControlByDataFieldName(dataFieldName);
    },

    _getSectionByName: function (name) {
        var sectionIds = this.get_detailFormView().get_sectionIds();
        for (var i = 0; i < sectionIds.length; i++) {
            var currentSection = $find(sectionIds[i]);
            if (currentSection && currentSection.get_name() == name) {
                return currentSection;
            }
        }
    },

    /* -------------------- properties ---------------- */
    get_parentLibraryField: function () {
        return this._parentLibraryField;
    },
    set_parentLibraryField: function (value) {
        this._parentLibraryField = value;
    },
    get_detailFormView: function () {
        return this._detailFormView;
    },
    set_detailFormView: function (value) {
        this._detailFormView = value;
    },
    get_rootLibrarySettings: function () {
        return this._rootLibrarySettings;
    },
    set_rootLibrarySettings: function (value) {
        this._rootLibrarySettings = value;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.LibraryDetailExtensions", Sys.Component, Sys.IDisposable);