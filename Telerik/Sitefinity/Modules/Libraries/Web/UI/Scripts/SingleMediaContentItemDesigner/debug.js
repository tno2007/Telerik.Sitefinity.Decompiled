Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._lastSelectedView = "uploadImageDesignerView";
    this._isInsertImage = true;
    this._stringImageTitle = "Image";
    this._doNotCloseOnCancel = false;
    this._providersSelectedDelegate = null;
    this._providersSelector = null;
    this._uploadImageView = null;
    this._imageSelectorView = null;
    this._isProviderCorrect = null;
    this._message = null;
    this._tabSelectedDelegate = null;

}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.callBaseMethod(this, "initialize");

        if (this._propertyEditor && this._propertyEditor._providersSelector) {
            this._providersSelector = this._propertyEditor._providersSelector;
            this._providersSelectedDelegate = Function.createDelegate(this, this._handleProvidersSelected);
            this._providersSelector.add_onProviderSelected(this._providersSelectedDelegate);
        }

        // This is temporary fix for chrome browser. If there is a video tag in the tabstrip control the browser does not re-render the
        // view correctly.
        if (this._stringImageTitle === "Video" && this._propertyEditor && this._propertyEditor._advancedModeButton && $telerik.isChrome) {
            this._advancedModeButtonDelegate = Function.createDelegate(this._propertyEditor, this._advancedModeButtonClicked);
            $addHandler(this._propertyEditor._advancedModeButton, 'click', this._advancedModeButtonDelegate);
        }

        if (this._uploadImageView && this._uploadImageView.get_imageDataView()) {
            this._imageLoadedDelegate = Function.createDelegate(this, this._handleImageLoaded);
            this._uploadImageView.get_imageDataView().add_onImageLoadedCommand(this._imageLoadedDelegate);
        }
    },

    dispose: function () {
        if (this._providersSelector) {
            this._providersSelector.remove_onProviderSelected(this._providersSelectedDelegate);
            delete this._providersSelectedDelegate;
        }

        if (this._menuTabStrip) {
            this._menuTabStrip.remove_tabSelected(this._tabSelectedDelegate);
            delete this._tabSelectedDelegate;
        }

        if (this._advancedModeButtonDelegate) {
            delete this._advancedModeButtonDelegate;
        }

        if (this._uploadImageView && this._uploadImageView.get_imageDataView()) {
            this._uploadImageView.get_imageDataView().remove_onImageLoadedCommand(this._imageLoadedDelegate);
            delete this._imageLoadedDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.callBaseMethod(this, "dispose");
    },

    // ----------------------------------------------- Private functions ----------------------------------------------

    //rebind all controls with specified provider name
    _rebind: function (providerName) {

        if (this._uploadImageView && this._uploadImageView.rebind) {
            this._uploadImageView.rebind(providerName);
        }

        if (this._imageSelectorView && this._imageSelectorView.rebind) {
            this._imageSelectorView.rebind(providerName);
        }
    },

    _updateImageSelectorView: function () {
        if (this._imageSelectorView._settingsView) {
            if (this._uploadImageView) {
                this._imageSelectorView._settingsView.set_isVectorGraphics(this._isVectorGraphics());
                this._imageSelectorView._settingsView.set_blobStorageProviderName(this._getBlobStorageProviderName());
                this._imageSelectorView._settingsView._onLoad();
            }
        }
    },
    
    _getBlobStorageProviderName: function (){
        if (this._uploadImageView && this._uploadImageView._imageDataView && this._uploadImageView._imageDataView._selectedDataItem) {
            return this._uploadImageView._imageDataView._selectedDataItem.BlobStorageProvider;
        }

        return null;
    },

    _isVectorGraphics: function () {
        if (this._uploadImageView &&
            this._uploadImageView._imageDataView &&
            this._uploadImageView._imageDataView._selectedDataItem &&
            this._uploadImageView._imageDataView._selectedDataItem.IsVectorGraphics === true) {
            return true;
        }
        return false;
    },

    // --------------------------------- event handlers --------------------------------- 
    _handleProvidersSelected: function (sender, args) {
        var controlData = this.get_controlData();
        if (controlData.hasOwnProperty('ProviderName')) {
            controlData.ProviderName = args.ProviderName;
        }
        this._rebind(args.ProviderName);
    },

    _tabSelectedHandler: function (sender, args) {
        if (!this._isProviderCorrect && this._message) {
            //hide ProviderNotAvailable warning message
            this._message.hide();
            dialogBase.resizeToContent();
            this._isProviderCorrect = true;
        }

        this._updateImageSelectorView();

        // This is temporary fix for chrome browser. If there is a video tag in the tabstrip control the browser does not re-render the
        // view correctly.
        if (this._stringImageTitle === "Video" && $telerik.isChrome) {
            setTimeout(function () {
                if (dialogBase)
                    dialogBase.resizeToContent();
            }, 100);
        }
    },

    _handleImageLoaded: function () {
        setTimeout(function () { dialogBase.resizeToContent(); }, 500);
    },

    // ----------------------------------------------- Public functions -----------------------------------------------
    _onLoad: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.callBaseMethod(this, "_onLoad");

        var controlData = this.get_controlData();
        var pe = this.get_propertyEditor();
        pe.set_title(this.get_stringImageTitle());

        var selectorDesignerView = this.getSelectorDesignerView();

        ////if there is image select the correct
        if (controlData.ImageId && controlData.ImageId != Telerik.Sitefinity.getEmptyGuid()) {
            selectorDesignerView.set_dataItemId(controlData.ImageId, true);
            selectorDesignerView.set_providerName(controlData.ProviderName);
        }

        else if (controlData.DocumentId && controlData.DocumentId != Telerik.Sitefinity.getEmptyGuid()) {
            selectorDesignerView.set_dataItemId(controlData.DocumentId, true);
            selectorDesignerView.set_providerName(controlData.ProviderName);
        }
        else if (controlData.MediaContentId && controlData.MediaContentId != Telerik.Sitefinity.getEmptyGuid()) {
            selectorDesignerView.set_dataItemId(controlData.MediaContentId, true);
            selectorDesignerView.set_mediaUrl(controlData.MediaUrl);
            selectorDesignerView.set_providerName(controlData.ProviderName);
        }

        if (this._menuTabStrip) {
            this._tabSelectedDelegate = Function.createDelegate(this, this._tabSelectedHandler)
            this._menuTabStrip.add_tabSelected(this._tabSelectedDelegate);
        }
    },

    //fired by the child designer views 
    executeCommand: function (args) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.callBaseMethod(this, "executeCommand", [args]);

        switch (args.CommandName) {
            case "ChangeImage":
                //this.selectView(this.get_lastSelectedView());
                //this.get_propertyEditor().set_title(this.get_stringImageTitle());
                break;
            case "UploadImage":
                //this.set_lastSelectedView("uploadImageDesignerView");
                //this.setImage(args.ImageData, true);
                //this.selectView("insertEditImageDesignerView");
                //this.get_propertyEditor().set_title(this.get_stringImageTitle());
                break;
            case "SelectImage":
                //this.set_lastSelectedView("imageSelectorDesignerView");
                //this.setImage(args.ImageData);
                //this.selectView("insertEditImageDesignerView");
                //this.get_propertyEditor().set_title(this.get_stringImageTitle());
                break;
            default:
                break;
        }
    },

    _advancedModeButtonClicked: function () {
        setTimeout(function () {
            if (dialogBase)
                dialogBase.resizeToContent();
        }, 100);
    },

    setImage: function (imageData, isUploaded) {
        var editImageView = this.getEditImageView();
        editImageView.set_image(imageData);
        editImageView.set_isUploaded(isUploaded);
    },

    getSelectorDesignerView: function () {
        return $find(this.get_designerViewsMap()["SingleMediaContentItemSelectorDesignerView"]);
    },

    selectView: function (viewName) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.callBaseMethod(this, "selectView", [viewName]);
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    //gets the view (from the first two) that was selected the last
    get_lastSelectedView: function () { return this._lastSelectedView; },
    set_lastSelectedView: function (value) { this._lastSelectedView = value; },

    //gets the localized string for the Image title of the property editor
    get_stringImageTitle: function () { return this._stringImageTitle; },
    set_stringImageTitle: function (value) { this._stringImageTitle = value; },

    //gets the upload image view
    get_uploadImageView: function () { return this._uploadImageView; },
    set_uploadImageView: function (value) { this._uploadImageView = value; },

    //gets the image selector view
    get_imageSelectorView: function () { return this._imageSelectorView; },
    set_imageSelectorView: function (value) { this._imageSelectorView = value; },

    //gets boolean value indicating whether selected provider name is correct
    get_isProviderCorrect: function () { return this._isProviderCorrect; },
    set_isProviderCorrect: function (value) { this._isProviderCorrect = value; },

    //gets the message control
    get_message: function () { return this._message; },
    set_message: function (value) { this._message = value; }

}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner", Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase);