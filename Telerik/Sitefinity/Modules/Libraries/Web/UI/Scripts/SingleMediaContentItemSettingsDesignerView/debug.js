Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView.initializeBase(this, [element]);

    this._providerName = "";
    this._settingsView = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- events --------------------------------- */

    /* ---------------------------------- event handlers --------------------------------- */
    
    _beforeSaveHandler: function (sender, args) {
        if (this.get_parentDesigner().isViewSelected(this)) {
            var isValidImageData = this.get_settingsView().validateItemData();
            if (!isValidImageData)
                dialogBase.resizeToContent();

            var setCancel = (!isValidImageData);
            args.set_cancel(setCancel);
        }
    },
    
    /* --------------------------------- public methods --------------------------------- */

    // ----------------------------------------------- Private functions ----------------------------------------------
    
    // ----------------------------------------------- Public functions -----------------------------------------------
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (controlData) {
            var settingsView = this.get_settingsView();
            if (settingsView.isImage()) {
                settingsView.setMargins(controlData.MarginTop, controlData.MarginRight, controlData.MarginBottom, controlData.MarginLeft);
                settingsView.set_alignment(controlData.Alignment);
                settingsView.setOpenOriginalImage(controlData.OpenOriginalImageOnClick);
                //settingsView.setLimitations(controlData.MinWidth, controlData.MinHeight, controlData.MaxWidth, controlData.MaxHeight, controlData.UploadedFileMaxSize);
                settingsView.set_viewType(controlData.ViewType);
                if (controlData.ThumbnailName) {
                    settingsView.set_selectedThumbnailName(controlData.ThumbnailName);
                }
                else if (controlData.DisplayMode === "Custom") {
                    settingsView.selectSizeOptionCustom();
                }
                settingsView.setMethodControlsProperties(controlData.CustomSizeMethodProperties);
                settingsView.setImageProcessingMethod(controlData.Method);
            }
            else if (settingsView.isDocument()) {
                settingsView.setThumbnailType(controlData.ThumbnailType);
            }
            else if (settingsView.isVideo()) {
                settingsView.setVideoAspectRatio(controlData.Width, controlData.Height);
            }

        }

        if (!this._beforeSaveDelegate) {
            var pe = this.get_parentDesigner().get_propertyEditor();
            this._beforeSaveDelegate = Function.createDelegate(this, this._beforeSaveHandler);
            pe.add_beforeSaveChanges(this._beforeSaveDelegate);
        }
    },

    applyChanges: function () {
        var settingsView = this.get_settingsView();
        var itemData = settingsView.getItemData();
        var controlData = this.get_controlData();
        if (settingsView.isImage()) {
            if (itemData) {
                controlData.MarginRight = itemData.MarginRight;
                controlData.MarginBottom = itemData.MarginBottom;
                controlData.MarginLeft = itemData.MarginLeft;
                controlData.MarginTop = itemData.MarginTop;
                controlData.Alignment = itemData.Alignment;
                controlData.OpenOriginalImageOnClick = itemData.OpenOriginalImageOnClick;
                controlData.MinWidth = itemData.MinWidth;
                controlData.MinHeight = itemData.MinHeight;
                controlData.MaxWidth = itemData.MaxWidth;
                controlData.MaxHeight = itemData.MaxHeight;
                controlData.UploadedFileMaxSize = itemData.UploadedFileMaxSize;
                controlData.ThumbnailName = itemData.ThumbnailName;
                controlData.Method = itemData.Method;
                controlData.CustomSizeMethodProperties = itemData.CustomSizeMethodProperties;
                controlData.DisplayMode = itemData.DisplayMode;
            }
        }
        else if (settingsView.isDocument()) {
            controlData.ThumbnailType = itemData.ThumbnailType;
        }
        else if (settingsView.isVideo()) {
            controlData.Height = itemData.Height;
            controlData.Width = itemData.Width;
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

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

    get_providerName: function () { return this._providerName; },
    set_providerName: function (value) {
        this._providerName = value;
    },

    //gets a bool value that indicates whether an image has been uploaded
    get_isUploaded: function () { return this._isUploaded; },
    set_isUploaded: function (value) { this._isUploaded = value; },

    get_settingsView: function () {
        return this._settingsView;
    },
    set_settingsView: function (value) {
        this._settingsView = value;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
