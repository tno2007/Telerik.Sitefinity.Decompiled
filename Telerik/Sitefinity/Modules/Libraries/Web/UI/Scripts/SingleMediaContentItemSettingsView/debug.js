Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView.initializeBase(this, [element]);

    this._sizesChoiceField = null;
    this._alignment = "None";
    this._openOriginalChoiceField = null;
    this._clientManager = null;
    this._marginTopField = null;
    this._marginBottomField = null;
    this._marginLeftField = null;
    this._marginRightField = null;
    this._customSizeOptionValue = "--custom--size--";
    this._customImageSizeViewControl = null;
    this._libraryType = null;
    this._thumbnailServiceUrl = null;
    this._blobStorageServiceUrl = null;
    this._viewType = null;
    this._sizesChoiceFieldValueChangedDelegate = null;
    this._selectedThumbnailName = null;
    this._onLoadDelegate = null;
    this._resizingOptionsControl = null;
    this._viewMode = null;
    this._iconSizesRadioButtonsClientIDs = [];
    this._bigIconsRadioButton = 0;
    this._smallIconsRadioButton = 1;
    this._noIconsRadioButton = 2;
    this._videoAspectRatio = null;

    this._btnMargin = null;
    this._marginSection = null;
    this._btnMarginClickDelegate = null;
    this._processingMethodFieldsGeneratedDelegate = null;

    this._isVectorGraphics = false;
    this._blobStorageProviderName = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView.callBaseMethod(this, "initialize");
        this.attachEventHandlers(true);
        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        this._bigIconsRadioButton = $get(this._iconSizesRadioButtonsClientIDs[0]);
        this._smallIconsRadioButton = $get(this._iconSizesRadioButtonsClientIDs[1]);
        this._noIconsRadioButton = $get(this._iconSizesRadioButtonsClientIDs[2]);

        this._processingMethodFieldsGeneratedDelegate = Function.createDelegate(this, this._processingMethodFieldsGeneratedHandler);
        if (this.get_customImageSizeViewControl()) {
            this.get_customImageSizeViewControl().add_processingMethodFieldsGenerated(this._processingMethodFieldsGeneratedDelegate);
        }
    },

    dispose: function () {
        this.attachEventHandlers(false);

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        if (this._processingMethodFieldsGeneratedDelegate) {
            if (this.get_customImageSizeViewControl()) {
                this.get_customImageSizeViewControl().remove_processingMethodFieldsGenerated(this._processingMethodFieldsGeneratedDelegate);
            }
            delete this._processingMethodFieldsGeneratedDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView.callBaseMethod(this, "dispose");
    },

    _onLoad: function () {
        if (this.get_viewMode() === "Image") {
            this.bindThumbnailSizesChoiceFields();
        }
    },

    attachEventHandlers: function (toAttach) {
        this._attachChooseSizeHandlers(toAttach);

        if (toAttach) {
            if (this._btnMargin) {
                this._btnMarginClickDelegate = Function.createDelegate(this, this._btnMarginClickHandler);
                $addHandler(this._btnMargin, "click", this._btnMarginClickDelegate);
            }
        }
        else {
            if (this._btnMargin) {
                $removeHandler(this._btnMargin, "click", this._btnMarginClickDelegate);
                delete this._btnMarginClickDelegate;
            }
        }
    },

    /* --------------------------------- events --------------------------------- */

    /* ---------------------------------- event handlers --------------------------------- */
    _btnMarginClickHandler: function () {
        jQuery(this._marginSection).toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    /* --------------------------------- public methods --------------------------------- */

    // ----------------------------------------------- Private functions ----------------------------------------------


    _attachChooseSizeHandlers: function (toAttach) {
        var sizesChoiceField = this.get_sizesChoiceField();

        if (!sizesChoiceField) return;

        if (toAttach) {
            this._sizesChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._sizesChoiceFieldValueChangedHandler);
            sizesChoiceField.add_valueChanged(this._sizesChoiceFieldValueChangedDelegate);
        }
        else {
            if (this._sizesChoiceFieldValueChangedDelegate) {
                sizesChoiceField.remove_valueChanged(this._sizesChoiceFieldValueChangedDelegate);
                delete this._sizesChoiceFieldValueChangedDelegate;
            }
        }
    },

    _sizesChoiceFieldValueChangedHandler: function (sender, args) {
        var selectedValue = sender.get_value();

        var customImageSizeViewControl = this.get_customImageSizeViewControl();
        if (selectedValue == this._customSizeOptionValue) {
            var method = customImageSizeViewControl.get_selectImageProcessingMethod().get_value();
            if (this.get_isVectorGraphics()) {
                customImageSizeViewControl.set_isVectorGraphics(true);
                //set default method 'ResizeFitToAreaArguments' for vector graphics
                if (method !== customImageSizeViewControl._selectImageProcessingMethod.get_defaultValue()) {
                    method = customImageSizeViewControl._selectImageProcessingMethod.get_defaultValue();
                }
            }
            else {
                customImageSizeViewControl.set_isVectorGraphics(false);
            }

            customImageSizeViewControl.get_selectImageProcessingMethod().set_value(method);
            jQuery(customImageSizeViewControl.get_customImgSizeWrp()).show();
        } else {
            jQuery(customImageSizeViewControl.get_customImgSizeWrp()).hide();
        }

        if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
            dialogBase.resizeToContent();
        }
    },

    _processingMethodFieldsGeneratedHandler: function (sender, args) {
        if (sender.get_isVectorGraphics() === true) {
            jQuery(sender.get_customImgSizeWrp()).find("div[id$='selectImageProcessingMethod']").hide();
            jQuery(sender.get_customImgSizeWrp()).find('div.sfCheckBox:has(input[name="ScaleUp"])').hide();
            jQuery(sender.get_customImgSizeWrp()).find('div.sfDropdownList:has(select[name="Quality"])').hide();

        }
        else {
            jQuery(sender.get_customImgSizeWrp()).find("div[id$='selectImageProcessingMethod']").show();
            jQuery(sender.get_customImgSizeWrp()).find('div.sfCheckBox:has(input[name="ScaleUp"])').show();
            jQuery(sender.get_customImgSizeWrp()).find('div.sfDropdownList:has(select[name="Quality"])').show();
        }

        if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
            dialogBase.resizeToContent();
        }
    },

    _getFieldValue: function (field) {
        var val = parseInt(field.get_value());
        return isNaN(val) ? null : val;
    },

    _getAvailableThumbnailProfilesAsync: function (callback) {
        var profiles = [];
        var requestUrl = String.format(
            "{0}/thumbnail-profiles/?libraryType={1}&viewType={2}",
            this.get_thumbnailServiceUrl(),
            this.get_libraryType(),
            this.get_viewType());

        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            processData: false,
            async: false,
            contentType: "application/json",
            success: function (data) {
                var result = [];
                for (var i = 0; i < data.Items.length; i++) {
                    var name = data.Items[i].Id;
                    var title = data.Items[i].Title;
                    result.push({ key: name, value: title });
                }
                profiles = result;
            },
            complete: function () {
                callback(profiles);
            }
        });
    },

    _getCustomImageSizeAllowed: function (callback) {
        var customImageSizeAllowed = false;
        var blobStorageProviderName = this.get_blobStorageProviderName();
        if (blobStorageProviderName){
            var requestUrl = String.format(
                "{0}/provider-settings/?blobStorageProviderName={1}",
                this.get_blobStorageServiceUrl(),
                blobStorageProviderName);

            jQuery.ajax({
                type: 'GET',
                url: requestUrl,
                processData: false,
                async: false,
                contentType: "application/json",
                success: function (data) {
                    if (data) {
                        customImageSizeAllowed = data.CustomImageSizeAllowed;
                    }
                },
                complete: function () {
                    callback(customImageSizeAllowed);
                }
            });
        } else {
            callback(customImageSizeAllowed);
        }
    },

    _getThumbnailChoices: function (profiles, customImageSizeAllowed) {
        var originalSizeChoice = { key: "", value: this.get_clientLabelManager().getLabel("LibrariesResources", "OriginalSize") };
        var customSizeChoice = { key: this._customSizeOptionValue, value: this.get_clientLabelManager().getLabel("ImagesResources", "CustomSize") };

        if (profiles[0] && profiles[0].key != originalSizeChoice.key && profiles[0].value != originalSizeChoice.value) {
            profiles.splice(0, 0, originalSizeChoice);
            if (customImageSizeAllowed) { 
                profiles.push(customSizeChoice); 
            }
        } else if (profiles.length < 1) {
            profiles.push(originalSizeChoice);
            if (customImageSizeAllowed) { 
                profiles.push(customSizeChoice); 
            }
        }

        return profiles;
    },

    // ----------------------------------------------- Public functions -----------------------------------------------
    getItemData: function () {
        switch (this.get_viewMode()) {
            case "Image":
                return this.getImageData();
            case "Media":
                return this.getVideoData();
            case "Document":
                return this.getDocumentData();
            default:
        }
    },

    isImage: function () {
        return this.get_viewMode() == "Image";
    },

    isDocument: function () {
        return this.get_viewMode() == "Document";
    },

    isVideo: function () {
        return this.get_viewMode() == "Media";
    },

    getImageData: function () {
        var thumbnailName = this.get_sizesChoiceField().get_value();

        // DisplayMode following the enum Telerik.Sitefinity.Web.UI.PublicControls.Enums.ImageDisplayMode
        var displayMode = "Original";
        var customSizeMethodProperties = "";
        var customSizeMethodPropertiesDictionary = {};
        var method = "";
        if (thumbnailName) {
            if (thumbnailName == this._customSizeOptionValue) {
                displayMode = "Custom";

                var customSizeMethodPropertiesData = this.get_customImageSizeViewControl().getCustomSizeMethodProperties();
                method = customSizeMethodPropertiesData.Method;
                customSizeMethodProperties = customSizeMethodPropertiesData.CustomSizeMethodProperties;
                customSizeMethodPropertiesDictionary = customSizeMethodPropertiesData.CustomSizeMethodPropertiesDictionary;
            }
            else
                displayMode = "Thumbnail";
        }

        if (thumbnailName && thumbnailName == this._customSizeOptionValue) {
            thumbnailName = "";
        }

        return {
            Alignment: jQuery('input:checked[name="radiosAlignment"]').attr('value'),
            VerticalAlign: "",
            DisplayMode: displayMode,
            CustomSizeMethodProperties: customSizeMethodProperties,
            CustomSizeMethodPropertiesDictionary: customSizeMethodPropertiesDictionary,
            Method: method,
            ThumbnailName: thumbnailName,
            OpenOriginalImageOnClick: this.get_openOriginalChoiceField() && this.get_openOriginalChoiceField().get_value() === "true",
            MarginTop: this._getFieldValue(this.get_marginTopField()),
            MarginLeft: this._getFieldValue(this.get_marginLeftField()),
            MarginRight: this._getFieldValue(this.get_marginRightField()),
            MarginBottom: this._getFieldValue(this.get_marginBottomField())
            //,Limitations
            //MinWidth: this._getFieldValue(this.get_minWidth()),
            //MinHeight: this._getFieldValue(this.get_minHeight()),
            //MaxWidth: this._getFieldValue(this.get_maxWidth()),
            //MaxHeight: this._getFieldValue(this.get_maxHeight()),
            //UploadedFileMaxSize: this._getFieldValue(this.get_uploadedFileMaxSize()),
        };
    },

    getVideoData: function () {
        var width = 0;
        if (this.get_resizingOptionsControl()) {
            width = this.get_resizingOptionsControl().get_resizedWidth();

            return {
                ResizedWidth: width
            };
        }
        else if (this.get_videoAspectRatio()) {
            return {
                Width: this.get_videoAspectRatio().getWidth(),
                Height: this.get_videoAspectRatio().getHeight()
            };
        }
    },

    getDocumentData: function () {
        // sets the thumbnail type if applicable
        var thumbnailType = "NoIcons";
        for (var i = 0, length = this._iconSizesRadioButtonsClientIDs.length; i < length; i++) {
            var radio = $get(this._iconSizesRadioButtonsClientIDs[i]);
            if (jQuery(radio).is(":checked")) {
                switch (i) {
                    case 0: thumbnailType = "BigIcons"; break;
                    case 1: thumbnailType = "SmallIcons"; break;
                    case 2: thumbnailType = "NoIcons"; break;
                }
                break;
            }
        }
        return {
            ThumbnailType: thumbnailType
        };
    },

    getData: function () {
        var mediaUrl = this.get_src();

        return {
            AlternateText: this.getAltText(),
            Title: this.getTitle(),
            DataItemId: this.get_dataItemId(),
            MediaUrl: mediaUrl,
            Extension: this.getExtension(),
            Library: this.getLibrary(),
            ProviderName: this.get_providerName()
        };
    },

    validateItemData: function () {
        switch (this.get_viewMode()) {
            case "Image":
                return this.vaidateImageData();
            case "Media":
                return true;
            case "Document":
                return true;
            default:
        }
    },

    setThumbnailType: function (thumbnailType) {
        switch (thumbnailType) {
            case "NoIcons":
                jQuery(this._noIconsRadioButton).attr('checked', true);
                break;
            case "SmallIcons":
                jQuery(this._smallIconsRadioButton).attr('checked', true);
                break;
            case "BigIcons":
                jQuery(this._bigIconsRadioButton).attr('checked', true);
                break;
            default:
                jQuery(this._bigIconsRadioButton).attr('checked', true);
                break;
        }
    },

    vaidateImageData: function () {
        var thumbnailName = this.get_sizesChoiceField().get_value();
        if (thumbnailName == this._customSizeOptionValue) {
            return this.get_customImageSizeViewControl().validateCustomSizeMethodProperties();
        }

        return true;
    },

    bindThumbnailSizesChoiceFields: function () {
        var that = this;

        this._getAvailableThumbnailProfilesAsync(function (profiles) {
            var selectThumbSizeField = that.get_sizesChoiceField();
            selectThumbSizeField.clearListItems();

            that._getCustomImageSizeAllowed(function (customImageSizeAllowed){
                var choices = that._getThumbnailChoices(profiles, customImageSizeAllowed);
                for (var i = 0; i < choices.length; i++) {
                    selectThumbSizeField.addListItem(choices[i].key, choices[i].value);
                }

                for (var j = 0; j < selectThumbSizeField._choiceElement.length; j++) {
                    if (that.get_isVectorGraphics() && j > 0 && j < selectThumbSizeField._choiceElement.length - 1) {
                        jQuery(selectThumbSizeField._choiceElement[j]).hide();
                    }
                    else {
                        jQuery(selectThumbSizeField._choiceElement[j]).show();
                    }
                }

                if (that.get_isVectorGraphics()) {
                    var selectedValue = that.get_selectedThumbnailName();
                    if (selectedValue == "" || selectedValue == that._customSizeOptionValue) {
                        selectThumbSizeField.set_value(selectedValue);
                    }
                    else {
                        selectThumbSizeField.set_value(that.get_selectedThumbnailName());
                    }
                }
                else {
                    selectThumbSizeField.set_value(that.get_selectedThumbnailName());
                }
            });
        });
    },

    setMargins: function (top, right, bottom, left) {
        top = parseInt(top);
        if (isNaN(top)) top = null;
        right = parseInt(right);
        if (isNaN(right)) right = null;
        bottom = parseInt(bottom);
        if (isNaN(bottom)) bottom = null;
        left = parseInt(left);
        if (isNaN(left)) left = null;

        if (this.get_marginTopField())
            this.get_marginTopField().set_value(top);
        if (this.get_marginRightField())
            this.get_marginRightField().set_value(right);
        if (this.get_marginBottomField())
            this.get_marginBottomField().set_value(bottom);
        if (this.get_marginLeftField())
            this.get_marginLeftField().set_value(left);
    },

    //setLimitations: function (minWidth, minHeight, maxWidth, maxHeight, maxFileSize) {
    //    minWidth = parseInt(minWidth);
    //    if (isNaN(minWidth)) minWidth = null;
    //    minHeight = parseInt(minHeight);
    //    if (isNaN(minHeight)) minHeight = null;
    //    maxWidth = parseInt(maxWidth);
    //    if (isNaN(maxWidth)) maxWidth = null;
    //    maxHeight = parseInt(maxHeight);
    //    if (isNaN(maxHeight)) maxHeight = null;
    //    maxFileSize = parseInt(maxFileSize);
    //    if (isNaN(maxFileSize)) maxFileSize = null;

    //    this.get_minWidth().set_value(minWidth);
    //    this.get_minHeight().set_value(minHeight);
    //    this.get_maxWidth().set_value(maxWidth);
    //    this.get_maxHeight().set_value(maxHeight);
    //    this.get_uploadedFileMaxSize().set_value(maxFileSize);
    //},

    setOpenOriginalImage: function (state) {
        if (this.get_openOriginalChoiceField())
            this.get_openOriginalChoiceField().set_value(state);
    },

    setImageProcessingMethod: function (value) {
        if (this.get_customImageSizeViewControl())
            this.get_customImageSizeViewControl().setImageProcessingMethod(value);
    },
    setMethodControlsProperties: function (methodControlsProperties) {
        if (this.get_customImageSizeViewControl())
            this.get_customImageSizeViewControl().setMethodControlsProperties(methodControlsProperties);
    },
    selectSizeOptionCustom: function () {
        this.set_selectedThumbnailName(this._customSizeOptionValue);
    },
    setVideoAspectRatio: function (width, height) {
        if (this.get_videoAspectRatio()) {
            this.get_videoAspectRatio().setWidthHeight(width, height);
        }
    },

    /* --------------------------------- properties --------------------------------- */
    //gets the TextField used for setting the top margin of the image
    get_marginTopField: function () { return this._marginTopField; },
    set_marginTopField: function (value) { this._marginTopField = value; },

    //gets the TextField used for setting the bottom margin of the image
    get_marginBottomField: function () { return this._marginBottomField; },
    set_marginBottomField: function (value) { this._marginBottomField = value; },

    //gets the TextField used for setting the left margin of the image
    get_marginLeftField: function () { return this._marginLeftField; },
    set_marginLeftField: function (value) { this._marginLeftField = value; },

    //gets the TextField used for setting the right margin of the image
    get_marginRightField: function () { return this._marginRightField; },
    set_marginRightField: function (value) { this._marginRightField = value; },

    //gets the ChoicesField used for choosing image size
    get_sizesChoiceField: function () { return this._sizesChoiceField; },
    set_sizesChoiceField: function (value) { this._sizesChoiceField = value; },

    //gets a reference to the CustomImageSizeView component
    get_customImageSizeViewControl: function () { return this._customImageSizeViewControl; },
    set_customImageSizeViewControl: function (value) { this._customImageSizeViewControl = value; },

    //gets the client label manager that manages the localization strings 
    get_clientLabelManager: function () { return this._clientLabelManager; },
    set_clientLabelManager: function (value) { this._clientLabelManager = value; },

    //gets the alignment of the image
    get_alignment: function () { return this._alignment; },
    set_alignment: function (value) {
        jQuery('input[value="' + value + '"]').attr("checked", "checked");
        this._alignment = value;
    },

    get_openOriginalChoiceField: function () { return this._openOriginalChoiceField; },
    set_openOriginalChoiceField: function (value) { this._openOriginalChoiceField = value; },

    //gets the client WebService manager
    get_clientManager: function () {
        if (this._clientManager == null) this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        return this._clientManager;
    },

    /*//gets the limitations section
    get_limitationsSection: function () {
        return this._limitationsSection;
    },
    set_limitationsSection: function (value) {
        this._limitationsSection = value;
    },

    //gets the limitations expander
    get_limitationsExpander: function () {
        return this._limitationsExpander;
    },
    set_limitationsExpander: function (value) {
        this._limitationsExpander = value;
    },

    //gets the min width
    get_minWidth: function () {
        return this._minWidth;
    },
    set_minWidth: function (value) {
        this._minWidth = value;
    },

    //gets the min height
    get_minHeight: function () {
        return this._minHeight;
    },
    set_minHeight: function (value) {
        this._minHeight = value;
    },

    //gets the max width
    get_maxWidth: function () {
        return this._maxWidth;
    },
    set_maxWidth: function (value) {
        this._maxWidth = value;
    },

    //gets the max height
    get_maxHeight: function () {
        return this._maxHeight;
    },
    set_maxHeight: function (value) {
        this._maxHeight = value;
    },

    //gets the max file size that can be uploaded
    get_uploadedFileMaxSize: function () {
        return this._uploadedFileMaxSize;
    },
    set_uploadedFileMaxSize: function (value) {
        this._uploadedFileMaxSize = value;
    },*/

    //gets the selected thumbnail name
    get_selectedThumbnailName: function () {
        return this._selectedThumbnailName;
    },
    set_selectedThumbnailName: function (value) {
        this._selectedThumbnailName = value;
    },

    // gets the control view type
    get_viewType: function () {
        return this._viewType;
    },
    // sets the control view type
    set_viewType: function (value) {
        this._viewType = value;
    },

    // gets the library type
    get_libraryType: function () {
        return this._libraryType;
    },
    set_libraryType: function (value) {
        this._libraryType = value;
    },

    // gets the thumbnail service Url
    get_thumbnailServiceUrl: function () {
        return this._thumbnailServiceUrl;
    },
    set_thumbnailServiceUrl: function (value) {
        this._thumbnailServiceUrl = value;
    },

    get_blobStorageServiceUrl: function () {
        return this._blobStorageServiceUrl;
    },
    set_blobStorageServiceUrl: function (value) {
        this._blobStorageServiceUrl = value;
    },

    get_viewMode: function () {
        return this._viewMode;
    },
    set_viewMode: function (value) {
        this._viewMode = value;
    },

    get_resizingOptionsControl: function () {
        return this._resizingOptionsControl;
    },
    set_resizingOptionsControl: function (value) {
        this._resizingOptionsControl = value;
    },

    get_btnMargin: function () {
        return this._btnMargin;
    },
    set_btnMargin: function (value) {
        this._btnMargin = value;
    },

    get_marginSection: function () {
        return this._marginSection;
    },
    set_marginSection: function (value) {
        this._marginSection = value;
    },

    get_videoAspectRatio: function () {
        return this._videoAspectRatio;
    },
    set_videoAspectRatio: function (value) {
        this._videoAspectRatio = value;
    },

    get_isVectorGraphics: function () {
        return this._isVectorGraphics;
    },
    set_isVectorGraphics: function (value) {
        this._isVectorGraphics = value;
    },
    
    get_blobStorageProviderName: function () {
        return this._blobStorageProviderName;
    },
    set_blobStorageProviderName: function (value) {
        this._blobStorageProviderName = value;
    },
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsView", Sys.UI.Control);
