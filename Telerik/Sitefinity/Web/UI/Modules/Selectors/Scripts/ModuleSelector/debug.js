﻿//ModuleSelector
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Modules.Selectors");

Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector = function(element) {

    //private properties (retrieved from server, inaccessible)
    this._moduleSelectionRepeaterID = null;
    this._moduleProviderAssociations = null;

    //public properties
    this._managerClassName = null;
    this._dataProviderName = null;
    this._securedObjectID = null;

    //internals
    this._preSelectedModule = null;
    this._selectedModule = null;

    //events
    this._itemSelected = null;
    this._itemSelectedDelegate = null;
    this._clientLoaded = null;
    this._clientLoadedDelegate = null;

    Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector.callBaseMethod(this, "initialize");
        Sys.Application.add_load(Function.createDelegate(this, this.onload));
        this._moduleProviderAssociations = eval(this._moduleProviderAssociations);

        if (this._itemSelectedDelegate == null) {
            this._itemSelectedDelegate = Function.createDelegate(this, this._itemSelectedHandler);
        }
        if (this._clientLoadedDelegate == null) {
            this._clientLoadedDelegate = Function.createDelegate(this, this._clientLoadedHandler);
        }
    },

    dispose: function() {
        for (var curModule = 0; curModule < this._moduleProviderAssociations.length; curModule++) {
            var moduleLink = $get(this._moduleProviderAssociations[curModule].LinkClientID);
            $removeHandler(moduleLink, "click", this._moduleLinkClick);
        }
        Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector.callBaseMethod(this, "dispose");

        if (this._itemSelectedHandler) {
            delete this._itemSelectedHandler;
        }
        if (this._clientLoadedHandler) {
            delete this._clientLoadedHandler;
        }
    },

    onload: function() {

        this._moduleLinkClick = Function.createDelegate(this, this._moduleLinkClick_Click)

        //set the module selection links
        for (var curModule = 0; curModule < this._moduleProviderAssociations.length; curModule++) {
            var moduleLink = $get(this._moduleProviderAssociations[curModule].LinkClientID);
            $addHandler(moduleLink, "click", this._moduleLinkClick);
            if (this._moduleProviderAssociations[curModule].IsSelectedModule)
                preSelectedModule = this._moduleProviderAssociations[curModule];
        }
        if (this._moduleProviderAssociations.length > 0) {
            if (this._preSelectedModule == null) {
                this._preSelectedModule = this._moduleProviderAssociations[0];
                this._preSelectedModule.IsSelectedModule = true;
            }
            this._highlightSelectedModule();
        }
        this._clientLoadedHandler();
    },

    get_selectedModule: function() {
        return this._selectedModule;
    },

    add_itemSelected: function(delegate) {
        this.get_events().addHandler('itemSelected', delegate);
    },

    remove_itemSelected: function(delegate) {
        this.get_events().removeHandler('itemSelected', delegate);
    },

    add_clientLoaded: function(delegate) {
        this.get_events().addHandler('clientLoaded', delegate);
    },

    remove_clientLoaded: function(delegate) {
        this.get_events().removeHandler('clientLoaded', delegate);
    },

    _itemSelectedHandler: function(dataItem, selectedElement) {
        var h = this.get_events().getHandler('itemSelected');
        if (h) h(this, dataItem, selectedElement);
    },

    _clientLoadedHandler: function() {
        var h = this.get_events().getHandler('clientLoaded');
        if (h) h(this);
    },

    _highlightSelectedModule: function() {
        for (var curModule = 0; curModule < this._moduleProviderAssociations.length; curModule++) {
            var moduleLink = $get(this._moduleProviderAssociations[curModule].LinkClientID);
            if (this._moduleProviderAssociations[curModule].IsSelectedModule) {
                moduleLink.parentNode.className = "sfSel";
                this._selectedModule = this._moduleProviderAssociations[curModule];
            }
            else
                moduleLink.parentNode.className = "";
        }
    },

    _moduleLinkClick_Click: function(e) {
        var module = null;
        for (var curModule = 0; curModule < this._moduleProviderAssociations.length; curModule++) {
            if (this._moduleProviderAssociations[curModule].LinkClientID == e.target.id) {
                module = this._moduleProviderAssociations[curModule];
                module.IsSelectedModule = true;
            }
            else
                this._moduleProviderAssociations[curModule].IsSelectedModule = false;
        }
        this._highlightSelectedModule();
        if (module != null) {
            this._itemSelectedHandler(module);
        }
    },

    //public properties
    get_managerClassName: function() {
        return this._managerClassName;
    },

    set_managerClassName: function(value) {
        if (this._managerClassName != value) {
            this._managerClassName = value;
            this.raisePropertyChanged('managerClassName');
        }
    },

    get_dataProviderName: function() {
        return this._dataProviderName;
    },

    set_dataProviderName: function(value) {
        if (this._dataProviderName != value) {
            this._dataProviderName = value;
            this.raisePropertyChanged('dataProviderName');
        }
    },

    get_securedObjectID: function() {
        return this._securedObjectID;
    },

    set_securedObjectID: function(value) {
        if (this._securedObjectID != value) {
            this._securedObjectID = value;
            this.raisePropertyChanged('securedObjectID');
        }
    }
};

Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector.registerClass('Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector', Sys.UI.Control);