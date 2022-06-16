Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView.initializeBase(this, [element]);

    this._mediaContentSelector = null;
    this._selectedDataItem = null;

    this._itemSelectDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView.callBaseMethod(this, "initialize");

        this._itemSelectDelegate = Function.createDelegate(this, this._itemSelect);
        this.get_mediaContentSelector().add_onItemSelectCommand(this._itemSelectDelegate);

        // prevent memory leaks
        $(this).on("unload", function(e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView.callBaseMethod(this, "dispose");

        if (this._itemSelectDelegate) {
            if (this.get_mediaContentSelector()) {
                this.get_mediaContentSelector().remove_onItemSelectCommand(this._itemSelectDelegate);
            }
            delete this._itemSelectDelegate;
        }
    },

    /* -------------------- public methods -------------------- */

    refreshUI: function () {
        this.get_parentDesigner().set_saveButtonEnabled(false);
    },

    applyChanges: function () {
        if (this.get_selectedDataItem())
        {
            this.get_controlData().MediaUrl = this.get_selectedDataItem().MediaUrl;
            this.get_controlData().MediaContentId = this.get_selectedDataItem().Id;
        }
    },

    rebind: function (providerName) {
        this.get_mediaContentSelector()._provider = providerName;
        this.get_mediaContentSelector()._initializeLists();
        this.get_parentDesigner().set_saveButtonEnabled(false);
    },

    isItemSelected: function () {
        return !!this.get_selectedDataItem();
    },

    /* -------------------- event handlers -------------------- */

    _itemSelect: function (sender, args) {
        if (!args.get_dataItem().IsFolder) {
            this.set_selectedDataItem(args.get_dataItem());
            this.get_parentDesigner().set_saveButtonEnabled(this.isItemSelected());
        }
    },

    /* -------------------- properties -------------------- */

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

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSelectorDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);