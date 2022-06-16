Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._editImageView = null;
    this._image = null;
    //by default we have enabled image uploading
    this._lastSelectedTabIndex = 0;
    this._clientLabelManager = null;
    this._blockSave = true;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView.callBaseMethod(this, "initialize");
        this._attachHandlers(true);
    },

    dispose: function () {
        this._attachHandlers(false);
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView.callBaseMethod(this, "dispose");
    },

    _attachHandlers: function (toAttach) {
        var imageView = this.get_editImageView();
        if (toAttach) {
            this._changeImageDelegate = Function.createDelegate(this, this._changeImageClick);
            imageView.add_changeImageClick(this._changeImageDelegate);

            this._editImageDelegate = Function.createDelegate(this, this._editImageClick);
            imageView.add_editImageClick(this._editImageDelegate);

            this._updateParametersDelegate = Function.createDelegate(this, this._updateParameters);
            imageView.add_updateParameters(this._updateParametersDelegate);
        }
        else {
            if (this._changeImageDelegate)
                imageView.remove_changeImageClick(this._changeImageDelegate);
            delete this._changeImageDelegate;

            if (this._editImageDelegate)
                imageView.remove_editImageClick(this._editImageDelegate);
            delete this._editImageDelegate;

            if (this._updateParametersDelegate)
                imageView.remove_updateParameters(this._updateParametersDelegate);
            delete this._updateParametersDelegate;

            //attached in refreshUI
            if (this._beforeSaveDelegate) {
                var pe = this.get_parentDesigner().get_propertyEditor();
                if (pe) pe.remove_beforeSaveChanges(this._beforeSaveDelegate);
            }
            delete this._beforeSaveDelegate;
        }
    },

    _changeImageClick: function (sender, args) {
        //select the last view
        this.get_parentDesigner().executeCommand({ CommandName: "ChangeImage" });
    },
    _editImageClick: function (sender, args) {
    },
    _updateParameters: function (sender, args) {
        var parentDesigner = this.get_parentDesigner();
        parentDesigner.get_propertyEditor()._saveChanges();
    },
    _beforeSaveHandler: function (sender, args) {
        if (this.get_parentDesigner().isViewSelected(this) && this._blockSave) {
            var isValidImageData = this.get_editImageView().validateImageData();
            if (!isValidImageData)
                dialogBase.resizeToContent();

            var setCancel = ((!isValidImageData) || (this.get_editImageView().uploadImage()));
            args.set_cancel(setCancel);
            this._blockSave = false;
        }
    },

    ///IDesignerViewControl members START

    // gets the reference for the propertyEditor control
    get_parentDesigner: function () { return this._parentDesigner; },
    // sets the reference for the propertyEditor control
    set_parentDesigner: function (value) {
        if (this._parentDesigner != value) {
            this._parentDesigner = value;
            this.raisePropertyChanged("parentDesigner");
        }
    },

    /// <summary>
    /// Base interface for all design controls
    /// </summary>
    set_controlData: function (value) {
        /// <summary>
        ///  Sets the control data to the component which is responsible for interpretation
        /// </summary>
        /// <param name="value">The controlData object</param>
    },
    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    refreshUI: function () {
        var controlData = this.get_controlData();
        if (controlData) {
            var editImageView = this.get_editImageView();
            editImageView.setAltText(controlData.AlternateText);
            editImageView.setMargins(controlData.MarginTop, controlData.MarginRight, controlData.MarginBottom, controlData.MarginLeft);
            editImageView.setTitle(controlData.ToolTip);
            editImageView.setOpenOriginalImage(controlData.OpenOriginalImageOnClick);
            editImageView.set_viewType(controlData.ViewType);

            if (controlData.ThumbnailName) {
                editImageView.set_selectedThumbnailName(controlData.ThumbnailName);
            } else if (controlData.DisplayMode === "Custom") {
                editImageView.selectSizeOptionCustom();
            }

            editImageView.setMethodControlsProperties(controlData.CustomSizeMethodProperties);
            editImageView.setImageProcessingMethod(controlData.Method);
        }

        if (!this._beforeSaveDelegate) {
            var pe = this.get_parentDesigner().get_propertyEditor();
            this._beforeSaveDelegate = Function.createDelegate(this, this._beforeSaveHandler);
            pe.add_beforeSaveChanges(this._beforeSaveDelegate);
        }
    },

    applyChanges: function () {
        ///forces the designer view to apply the changes on UI to the control Data
        var imageData = this.get_editImageView().getImageData();
        if (imageData) {
            var controlData = this.get_controlData();
            if (imageData.ImageId)
                controlData.ImageId = imageData.ImageId;
            controlData.AlternateText = imageData.AlternateText;
            controlData.ThumbnailName = this.get_editImageView().getThumbnailName();
            controlData.ToolTip = imageData.Title;
            controlData.DisplayMode = imageData.DisplayMode;
            controlData.CustomSizeMethodProperties = imageData.CustomSizeMethodProperties;
            controlData.Method = imageData.Method;
            controlData.Alignment = imageData.Alignment;
            controlData.OpenOriginalImageOnClick = imageData.OpenOriginalImageOnClick;
            controlData.MarginRight = imageData.MarginRight;
            controlData.MarginBottom = imageData.MarginBottom;
            controlData.MarginLeft = imageData.MarginLeft;
            controlData.MarginTop = imageData.MarginTop;
            //prevent image caching by setting safely a property that is not used
            controlData.BorderColor = controlData.BorderColor == "Transparent" ? "White" : "Transparent";
        }
    },

    notifyViewSelected: function () {
        this.get_parentDesigner().set_saveButtonText(this.get_clientLabelManager().getLabel("Labels", "Save"));
        dialogBase.resizeToContent();
    },
    ///IDesignerViewControl members END

    ///Properties START
    /// Gets a reference to the InsertEditImageView component
    get_editImageView: function () { return this._editImageView; },
    set_editImageView: function (value) { this._editImageView = value; },

    //gets the clientLabelManager
    get_clientLabelManager: function () { return this._clientLabelManager; },
    set_clientLabelManager: function (value) { this._clientLabelManager = value; },

    //gets the current image object
    get_image: function () { return this._image; },
    set_image: function (value) {
        this._image = value;
        this.get_editImageView().set_image(value);
    },

    set_isUploaded: function (value) {
        this.get_editImageView().set_isUploaded(value);
    }

    ///Properties END
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.InsertEditImageDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
