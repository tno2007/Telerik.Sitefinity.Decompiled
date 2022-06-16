﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView.initializeBase(this, [element]);

    this._mediaContentSelector = null;
    this._parentDesigner = null;
    this._itemSelectDelegate = null;
    this._selectedDataItem = null;
	this._saveButtonText = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView.callBaseMethod(this, "initialize");

        this.attachEventHandlers();
    },

    dispose: function () {
        this.detachEventHandlers();

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView.callBaseMethod(this, "dispose");
    },

    attachEventHandlers: function () {
        this._itemSelectDelegate = Function.createDelegate(this, this._itemSelect);
        this.get_mediaContentSelector().add_onItemSelectCommand(this._itemSelectDelegate);

        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);
        Sys.Application.add_load(this._pageLoadDelegate);

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },
    detachEventHandlers: function () {
        Sys.Application.remove_load(this._pageLoadDelegate);
        delete this._pageLoadDelegate;

        if (this.get_mediaContentSelector()) {
            this.get_mediaContentSelector().remove_onItemSelectCommand(this._itemSelectDelegate);
        }
        delete this._itemSelectDelegate;
    },

    // ----------------------------------------------- Private functions ----------------------------------------------
    _pageLoadHandler: function () {
        this._beforeSaveDelegate = Function.createDelegate(this, this._selectedImageHandler);
        this.get_parentDesigner().get_propertyEditor().add_beforeSaveChanges(this._beforeSaveDelegate);
    },

    _selectedImageHandler: function (sender, args) {
        if (this.get_parentDesigner().isViewSelected(this)) {
            args.set_cancel(true);
            this.get_parentDesigner().executeCommand({ CommandName: "SelectImage", ImageData: this.get_selectedDataItem() });
        }
    },

    // ----------------------------------------------- Public functions -----------------------------------------------
    refreshUI: function () {

    },

    notifyViewSelected: function () {
        var parentDesigner = this.get_parentDesigner();
        parentDesigner.set_saveButtonText(this._saveButtonText);
        parentDesigner.set_saveButtonEnabled(this.isItemSelected());
    },

    applyChanges: function () {
    },

    isItemSelected: function () {
        return !!this.get_selectedDataItem();
    },

    rebind: function (providerName) {
        if (this._mediaContentSelector) {
            this._mediaContentSelector._provider = providerName;
            this._mediaContentSelector._initializeLists();
        }
    },

    _itemSelect: function (sender, args) {
        var dataItem = args.get_dataItem();
        if (!dataItem.IsFolder) {
            this.set_selectedDataItem(dataItem);
            this.get_parentDesigner().set_saveButtonEnabled(this.isItemSelected());
        }
    },

    // gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },
    // sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        if (this._parentDesigner != value) {
            this._parentDesigner = value;
            this.raisePropertyChanged("parentDesigner");
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    get_mediaContentSelector: function () {
        return this._mediaContentSelector;
    },
    set_mediaContentSelector: function (value) {
        this._mediaContentSelector = value;
    },

    get_selectedDataItem: function () {
        return this._selectedDataItem;
    },
    set_selectedDataItem: function (value) {
        this._selectedDataItem = value;
    }

}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSelectorDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);