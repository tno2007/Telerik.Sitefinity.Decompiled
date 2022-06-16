Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._lastSelectedView = "uploadImageDesignerView";
    this._isInsertImage = true;
    this._stringInsertImage = "Insert Image";
    this._stringEditImage = "Edit Image";
    this._stringSelectImage = "Select Image";
    this._beforeDialogCloseDelegate = null;
    this._doNotCloseOnCancel = false;
    this._providersSelectedDelegate = null;
    this._providersSelector = null;
    this._uploadImageView = null;
    this._imageSelectorView = null;
    this._isProviderCorrect = null;
    this._message = null;
    this._tabSelectedDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.callBaseMethod(this, "initialize");

        if (this._propertyEditor && this._propertyEditor._providersSelector) {
            this._providersSelector = this._propertyEditor._providersSelector;
            this._providersSelectedDelegate = Function.createDelegate(this, this._handleProvidersSelected);
            this._providersSelector.add_onProviderSelected(this._providersSelectedDelegate);
        }
    },

    dispose: function () {
        var pe = this.get_propertyEditor();
        if (pe && this._beforeDialogCloseDelegate && pe.get_radWindow()) {
            pe.get_radWindow().remove_beforeClose(this._beforeDialogCloseDelegate);
        }
        delete this._beforeDialogCloseDelegate;

        if (this._providersSelector) {
            this._providersSelector.remove_onProviderSelected(this._providersSelectedDelegate);
            delete this._providersSelectedDelegate;
        }

        if (this._menuTabStrip) {
            this._menuTabStrip.remove_tabSelected(this._tabSelectedDelegate);
            delete this._tabSelectedDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.callBaseMethod(this, "dispose");
    },

    // ----------------------------------------------- Private functions ----------------------------------------------
    //fired before closing of the dialog
    _beforeDialogCloseHandler: function (sender, args) {
        // in advanced mode close switches to simple mode
        if (this.get_currentViewName() != "insertEditImageDesignerView" && this._doNotCloseOnCancel) {
            args.set_cancel(true);
            this.selectView("insertEditImageDesignerView");
            this.set_saveButtonEnabled(true);
        }
    },

    //rebind all controls with specified provider name
    _rebind: function (providerName) {

        if (this._uploadImageView && this._uploadImageView.rebind) {
            this._uploadImageView.rebind(providerName);
        }

        if (this._imageSelectorView && this._imageSelectorView.rebind) {
            this._imageSelectorView.rebind(providerName);
        }
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
    },

    // ----------------------------------------------- Public functions -----------------------------------------------
    _onLoad: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.callBaseMethod(this, "_onLoad");

        var controlData = this.get_controlData();
        var pe = this.get_propertyEditor();

        //if there is image select the correct
        if (controlData.ImageId && controlData.ImageId != Telerik.Sitefinity.getEmptyGuid()) {
            pe.set_title(this.get_stringEditImage());
            this._isInsertImage = false;
            this.selectView("insertEditImageDesignerView");
            this.getEditImageView().get_editImageView().set_imageId(controlData.ImageId);
        }
        else {
            pe.set_title(this.get_stringInsertImage());
        }

        //handle Cancel click
        if (!this._beforeDialogCloseDelegate) {
            this._beforeDialogCloseDelegate = Function.createDelegate(this, this._beforeDialogCloseHandler);
            pe.get_radWindow().add_beforeClose(this._beforeDialogCloseDelegate);
        }

        if (this._menuTabStrip) {
            this._tabSelectedDelegate = Function.createDelegate(this, this._tabSelectedHandler)
            this._menuTabStrip.add_tabSelected(this._tabSelectedDelegate);
        }
    },

    //fired by the child designer views 
    executeCommand: function (args) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.callBaseMethod(this, "executeCommand", [args]);

        switch (args.CommandName) {
            case "ChangeImage":
                this.selectView(this.get_lastSelectedView());
                this.get_propertyEditor().set_title(this.get_stringSelectImage());
                break;
            case "UploadImage":
                this.set_lastSelectedView("uploadImageDesignerView");
                this.setImage(args.ImageData, true);
                this.selectView("insertEditImageDesignerView");
                this.get_propertyEditor().set_title(this._isInsertImage ? this.get_stringInsertImage() : this.get_stringEditImage());
                break;
            case "SelectImage":
                this.set_lastSelectedView("imageSelectorDesignerView");
                this.setImage(args.ImageData);
                this.selectView("insertEditImageDesignerView");
                this.get_propertyEditor().set_title(this._isInsertImage ? this.get_stringInsertImage() : this.get_stringEditImage());
                break;
            default:
                break;
        }
    },

    setImage: function (imageData, isUploaded) {
        var editImageView = this.getEditImageView();
        editImageView.set_image(imageData);
        editImageView.set_isUploaded(isUploaded);
    },

    getEditImageView: function () {
        return $find(this.get_designerViewsMap()["insertEditImageDesignerView"]);
    },

    selectView: function (viewName) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.callBaseMethod(this, "selectView", [viewName]);
        if (viewName == "insertEditImageDesignerView") this._doNotCloseOnCancel = true;

        if (this._propertyEditor) {
            if (viewName == 'insertEditImageDesignerView') {
                this._propertyEditor.hideProvidersSelector();
            }
            else {
                this._propertyEditor.showProvidersSelector();
            }
        }
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    //gets the view (from the first two) that was selected the last
    get_lastSelectedView: function () { return this._lastSelectedView; },
    set_lastSelectedView: function (value) { this._lastSelectedView = value; },

    //gets the localized string for the InsertImage title of the property editor
    get_stringInsertImage: function () { return this._stringInsertImage; },
    set_stringInsertImage: function (value) { this._stringInsertImage = value; },

    //gets the localized string for the EditImage title of the property editor
    get_stringEditImage: function () { return this._stringEditImage; },
    set_stringEditImage: function (value) { this._stringEditImage = value; },

    //gets the localized string for the SelectImage title of the property editor
    get_stringSelectImage: function () { return this._stringSelectImage; },
    set_stringSelectImage: function (value) { this._stringSelectImage = value; },

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
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner", Telerik.Sitefinity.Web.UI.ControlDesign.ContentViewDesignerBase);