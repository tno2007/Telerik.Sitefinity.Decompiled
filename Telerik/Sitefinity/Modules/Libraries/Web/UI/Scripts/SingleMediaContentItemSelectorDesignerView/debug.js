Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView.initializeBase(this, [element]);

    this._parentDesigner = null;
    this._imageDataView = null;
    this._imageId = null;
    this._providerName = null;
    this._mediaUrl = null;
    this._mediaContentDesignerCloseDelegate = null;
    this._thisDialog = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView.callBaseMethod(this, "initialize");

        var oWindow = null;
        if (window.radWindow) {
            oWindow = window.radWindow;//Will work in Moz in all cases, including clasic dialog
        }
        else if (window.frameElement.radWindow) {
            oWindow = window.frameElement.radWindow;//IE (and Moz az well)
        }

        this._mediaContentDesignerCloseDelegate = Function.createDelegate(this, this._mediaContentDesignerClose);
        this._thisDialog = oWindow;
        this._thisDialog.add_close(this._mediaContentDesignerCloseDelegate); 
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- events --------------------------------- */

    /* ---------------------------------- event handlers --------------------------------- */
    _mediaContentDesignerClose: function (sender, args) {
        var mediaView = this.get_imageDataView();
        if (mediaView.isVideo()) {
            if (mediaView.get_mediaField() && mediaView.get_mediaField().get_mediaPlayer()) {
                mediaView.get_mediaField().get_mediaPlayer().stopMedia();
            }
        }

        this._thisDialog.remove_close(this._mediaContentDesignerCloseDelegate);
    },

    /* --------------------------------- public methods --------------------------------- */
    applyChanges: function () {
        ///forces the designer view to apply the changes on UI to the control Data
        var view = this.get_imageDataView();
        var data = view.getData();
        if (data) {
            var controlData = this.get_controlData();
            if (data.DataItemId) {
                if (view.isImage()) {
                    controlData.ImageId = data.DataItemId;
                    controlData.ToolTip = data.Title;
                    controlData.ProviderName = data.ProviderName;
                }
                else if (view.isDocument()) {
                    controlData.DocumentId = data.DataItemId;
                    controlData.Text = data.Title;
                    controlData.ProviderName = data.ProviderName;
                }
                else if (view.isVideo()) {
                    controlData.MediaContentId = data.DataItemId;
                    controlData.MediaUrl = data.MediaUrl;
                    controlData.ProviderName = data.ProviderName;
                }
            }
        }
    },

    refreshUI: function (forceRefresh) {
        if (forceRefresh) {
            this.get_imageDataView().refreshUI();
        }
    },

    notifyViewSelected: function () {
        var parentDesigner = this.get_parentDesigner();
        //parentDesigner.set_saveButtonText(this._saveButtonText);
        //parentDesigner.set_saveButtonEnabled(this.isItemSelected());
    },

    rebind: function () {
        this.get_imageDataView().rebind();
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

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

    get_imageDataView: function () {
        return this._imageDataView;
    },
    set_imageDataView: function (value) {
        this._imageDataView = value;
    },

    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },

    get_dataItemId: function () {
        return this.get_imageDataView().get_dataItemId();
    },
    set_dataItemId: function (value, forceRefreshUI) {
        this._dataItemId = value;
        this.get_imageDataView().set_dataItemId(value, forceRefreshUI);
    },

    get_mediaUrl: function () {
        return this.get_imageDataView().get_mediaUrl();
    },
    set_mediaUrl: function (value, forceRefreshUI) {
        this._mediaUrl = value;
        this.get_imageDataView().set_mediaUrl(value, forceRefreshUI);
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);