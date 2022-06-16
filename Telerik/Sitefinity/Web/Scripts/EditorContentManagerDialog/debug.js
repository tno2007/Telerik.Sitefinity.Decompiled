﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.EditorContentManagerDialog = function (element) {
    this._uploaderView = null;
    this._selectorView = null;
    this._dialogModesSwitcher = null;
    this._saveLink = null;
    this._cancelLink = null;
    this._titleLabel = null;
    this._cancelLinkTitle = null;
    this._uploaderSection = null;
    this._selectorSection = null;
    this._useOnlyUploadMode = null;
    this._useOnlySelectMode = null;

    this._dialogMode = null;
    this._selectedDataItem = null;
    this._itemWasSelected = false;
    this._domElementToInsert = null;
    this._currentMode = null;

    this._closeDialogCancelDelegate = null;
    this._closeDialogSaveDelegate = null;
    this._itemSelectDelegate = null;
    this._fileChangedDelegate = null;
    this._fileUploadedDelegate = null;
    this._valueChangedDelegate = null;
    this._loadDelegate = null;
    this._bodyCssClass = null;

    // delegate used the override the standart functionality
    this._customInsertDelegate = null;

    this._culture = null;
    this._uiCulture = null;
    this._clientLabelManager = null;

    //Image specific vars
    this._editImageView = null;
    this._editImageSection = null;
    this._isInsertImage = true;
    this._isMediaItemSelected = false;

    this._maxFileSize = null;
    this._allowedExtensions = null;

    this._isGranted = null;
    this._isGrantedDictionary = null;
    this._provider = null;
    this._providersSelector = null;
    this._providerChangedDelegate = null;
    this._libraryNotSelectedErrorMessage = null;

    this._noLibrariesOkButton = null;
    this._thumbnailServiceUrl = null;
    this._selectorViewTitle = null;

    Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.prototype = {

    /* ************************* set up and tear down ************************* */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.callBaseMethod(this, 'initialize');

        this._closeDialogCancelDelegate = Function.createDelegate(this, this._closeDialogCancel);
        this._closeDialogSaveDelegate = Function.createDelegate(this, this._closeDialogSave);
        this._itemSelectDelegate = Function.createDelegate(this, this._itemSelect);

        this._changeIsGranted(this.get_isGrantedForCurrentProvider());

        this._fileChangedDelegate = Function.createDelegate(this, this._fileChanged);
        this._fileUploadedDelegate = Function.createDelegate(this, this._fileUploaded);

        if (!(this._useOnlyUploadMode || this._useOnlySelectMode)) {
            this._valueChangedDelegate = Function.createDelegate(this, this._valueChanged);
            this.get_dialogModesSwitcher().add_valueChanged(this._valueChangedDelegate);
        }

        this._loadDelegate = Function.createDelegate(this, this._load);

        if (this._providersSelector) {
            this._providerChangedDelegate = Function.createDelegate(this, this._handleProviderChanged);
            this._providersSelector.add_onProviderSelected(this._providerChangedDelegate);
        }

        if (this._bodyCssClass) {
            jQuery("body").addClass("sfSelectorDialog");
        }

        if (this.get_noLibrariesOkButton()) {
            $addHandler(this.get_noLibrariesOkButton(), "click", this._closeDialogCancelDelegate);
        }

        Sys.Application.add_load(this._loadDelegate);
    },

    dispose: function () {
        if (this._closeDialogCancelDelegate) {
            if (this.get_noLibrariesOkButton()) {
                $removeHandler(this.get_noLibrariesOkButton(), "click", this._closeDialogCancelDelegate);
            }
            delete this._closeDialogCancelDelegate;
        }
        if (this._closeDialogSaveDelegate) {
            delete this._closeDialogSaveDelegate;
        }
        if (this._itemSelectDelegate) {
            if (this.get_selectorView()) {
                this.get_selectorView().remove_onItemSelectCommand(this._itemSelectDelegate);
            }
            delete this._itemSelectDelegate;
        }
        if (this._fileChangedDelegate) {
            if (this.get_uploaderView()) {
                this.get_uploaderView().remove_onFileChanged(this._fileChangedDelegate);
            }
            delete this._fileChangedDelegate;
        }
        if (this._fileUploadedDelegate) {
            if (this.get_uploaderView()) {
                this.get_uploaderView().remove_onFileUploaded(this._fileUploadedDelegate);
            }
            delete this._fileUploadedDelegate;
        }
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._providersSelector) {
            this._providersSelector.remove_onProviderSelected(this._providerChangedDelegate);
            delete this._providerChangedDelegate;
        }
        this._attachEditImageViewHandlers(false);
        Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    showProvidersSelector: function () {
        if (this._providersSelector) {
            $('#' + this._providersSelector.get_id()).show();
        }
    },

    hideProvidersSelector: function () {
        if (this._providersSelector) {
            $('#' + this._providersSelector.get_id()).hide();
        }
    },

    rebind: function () {
        this.get_selectorView().rebind();
        this.get_uploaderView().rebind(this._provider);
    },

    /* ************************* events ************************* */


    /* ************************* event handlers ************************* */

    _load: function () {
        if (this._isGranted) {
            this._currentMode = "1";
            this._switchToUploadMode();
        }
        else {
            this._currentMode = "2";
            this._switchToSelectMode();
        }

        if (this._useOnlySelectMode && this._currentMode != "2") {
            this._currentMode = "2";
            this._switchToSelectMode();
        }

        this._initializeParams();
        this._initializeHandlers();
        this._initializeFieldControlsFromElement();
        if (this.get_uploaderView()) {
            this.get_uploaderView().set_fileAllowedExtensions(this.get_allowedExtensions());
        }
        //resize the dialog according to the content
        this.resizeToContent();
        this.resizeTopRadWindow();

        if (/chrome/.test(navigator.userAgent.toLowerCase()))
            jQuery("body").addClass("sfOverflowHiddenX");
    },

    _closeDialogCancel: function () {
        if (!this._customInsertDelegate && this.get_dialogMode() == "1" && this.get_selectedDataItem() && this.get_editImageSection().style.display == "none") {
            this._switchToImageEditMode();
            return;
        }
        this.close();
        this.resizeTopRadWindow();

        //IE FIX: Explorer calls window.onbeforeunload when hyperlink (<a>)  with href="javascript:..." is clicked
        //Returning false solves the issue.
        return false;
    },

    _closeDialogSave: function (sender, args) {
        if (this._currentMode == "1") {
            var ajaxUpload = this.get_uploaderView().get_ajaxUpload();

            // perform size validation
            if (this.get_maxFileSize() && this.get_maxFileSize() > 0) {
                var selectedFileSize = ajaxUpload.get_input().files[0].size;
                if (selectedFileSize > this.get_maxFileSize()) {
                    var invalidUploadedFileSizeMessage = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "InvalidFileSizeAlertMessage"), (this.get_maxFileSize() / 1024));
                    alert(invalidUploadedFileSizeMessage);
                    return;
                }
            }

            if (this.get_selectedDataItem() == null) {
                if (this.get_uploaderView()._targetLibraryId == Telerik.Sitefinity.getEmptyGuid() &&
                    (this.get_uploaderView().get_parentLibrarySelector().get_value() == null ||
                     this.get_uploaderView().get_parentLibrarySelector().get_value() == Telerik.Sitefinity.getEmptyGuid())) {
                    //we do not have set library
                    alert(this._libraryNotSelectedErrorMessage);
                    return;
                }
            }

            var fileUploading = (ajaxUpload.get_input() && ajaxUpload.get_input().value != "");
            if (fileUploading) {
                ajaxUpload.submit();
                this._enableDisableLinkButton(true);
                jQuery(this.get_uploaderView().get_fileNameTextBox()).val("");
                this._itemWasSelected = false;
            }
            else {
                this._insertItem();
                this.resizeTopRadWindow();
            }
        }
        else {
            //The case where the file is selected from already uploaded files
            var selectedDataItem = this.get_selectedDataItem();
            if (selectedDataItem != null) {

                //Validate file size
                //Get the size of the selected file in Bytes as the maxFileSize is in Bytes
                if (this.get_maxFileSize() && this.get_maxFileSize() > 0) {
                    var selectedDataItemSize = selectedDataItem.TotalSize * 1024;
                    if (selectedDataItemSize > this.get_maxFileSize()) {
                        var invalidSelectedFileSizeMessage = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "InvalidFileSizeAlertMessage"), (this.get_maxFileSize() / 1024));
                        alert(invalidSelectedFileSizeMessage);
                        return;
                    }
                }

                //Validate file extension
                var extension = selectedDataItem.Extension.substr(1);
                if (!this._validateFileExtension(extension)) {
                    var invalidFileFormatMessage = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "CantUploadFiles"), "." + extension, this.get_allowedExtensions());
                    alert(invalidFileFormatMessage);

                    return;
                }
            }
            this._insertItem();
            this.resizeTopRadWindow();
        }
    },

    _itemSelect: function (sender, args) {
        if (!args.get_dataItem().IsFolder) {
            this._configureButtonArea();
            var dataItem = args.get_dataItem();
            this.set_selectedDataItem(dataItem);
            this._isMediaItemSelected = true;
        }
    },

    _fileChanged: function (sender, args) {
        this._configureButtonArea();
        this.set_selectedDataItem(null);
    },

    _fileUploaded: function (sender, args) {
        var uploadedItem = args;
        this.set_selectedDataItem(uploadedItem);
        var editView = this.get_editImageView();
        if (editView) editView.set_isUploaded(true);
        this._insertItem();
    },

    _valueChanged: function (sender, args) {
        var value = this.get_dialogModesSwitcher().get_value();
        switch (value) {
            case "1":
                this._switchToUploadMode();
                break;
            case "2":
                this._switchToSelectMode();
                break;
            default:
                break;
        }

        this._currentMode = value;
        this.resizeToContent();
        this.resizeTopRadWindow();
    },

    resizeTopRadWindow: function () {
        var currentRadWindow = this.get_radWindow();
        if (currentRadWindow) {
            var bounds = $telerik.getBounds(currentRadWindow.get_popupElement());
            var topRadWindow = this.get_radWindow().get_contentFrame().contentWindow.top.$find("PropertyEditorDialog");
            if (topRadWindow) {
                setTimeout(function () {
                    currentRadWindow.set_modal(false);
                    topRadWindow.AjaxDialog.resizeToContent();
                    if (currentRadWindow.isVisible()) {
                        currentRadWindow.set_modal(true);
                        currentRadWindow._afterShow();
                        currentRadWindow.moveTo(bounds.x, bounds.y);
                    }
                }, 0);
            }
        }
    },

    _handleProviderChanged: function (sender, args) {
        var providerName = args.ProviderName;
        if (this._provider != providerName) {
            this._provider = providerName;
            //refresh uploader view
            this._uploaderView.rebind(providerName);
            //refresh selector view
            this._selectorView._provider = providerName;
            this._selectorView._initializeLists();
            //refresh ui based on the permissions
            this._changeIsGranted(this.get_isGrantedForCurrentProvider());
        }
    },

    /* ************************* event firing ************************* */


    /* ************************* private methods ************************* */

    _initializeParams: function () {
        var win = this.get_radWindow();
        if (win) {
            this._domElementToInsert = win.ClientParameters;
        }
    },

    _initializeHandlers: function () {
        jQuery(this.get_cancelLink()).click(this._closeDialogCancelDelegate);

        this.get_selectorView().add_onItemSelectCommand(this._itemSelectDelegate);
        this.get_uploaderView().add_onFileChanged(this._fileChangedDelegate);
        this.get_uploaderView().add_onFileUploaded(this._fileUploadedDelegate);
        this._attachEditImageViewHandlers(true);
    },

    _initializeFieldControlsFromElement: function () {
        var jElementToInsert = this._getJElementToInsert();
        this._isInsertImage = !jElementToInsert;
        if (jElementToInsert) {
            var sfref = jElementToInsert.attr("sfref") ? jElementToInsert.attr("sfref") : jElementToInsert.children().attr("sfref");

            if (sfref) {
                var initialize = true;
                if (this._getDialogModeFromSfrefAttr(sfref) != this._getDialogModeAsString())
                    initialize = false;

                if (initialize) {
                    var id = this._getIdFromSfrefAttr(sfref);
                    var provider = this._getProviderFromSfrefAttr(sfref);
                    var thumbnailName = this._getThumbnailNameFromSfrefAttr(sfref);
                    var url = null;
                    var file = null;
                    var dataItem = null;

                    if (this._providersSelector) {
                        this._providersSelector.selectProvider(provider);
                    }

                    if (this._isGranted) {
                        jQuery(this.get_uploaderView().get_selectFileButton()).find("strong.sfLinkBtnIn").html(this.get_uploaderView().get_selectFileButtonText());
                        jQuery(this.get_uploaderView().get_settingsPanel()).show();
                    }
                    this._configureButtonArea();

                    switch (this.get_dialogMode()) {
                        //NotSet                                                                                                                                                                                                                                                                 
                        case 0:
                            throw "Dialog mode not set.";
                            break;
                            //Image                                                                                                                                                                                                 
                        case 1:
                            this._isInsertImage = false;
                            this._switchToImageEditMode();
                            url = jElementToInsert.attr("src");
                            var editImageView = this.get_editImageView();
                            editImageView.readDataFromHTML(jElementToInsert);
                            editImageView.set_imageId(id);
                            editImageView.set_providerName(provider);

                            editImageView.setMethodControlsProperties(jElementToInsert.attr("customSizeMethodProperties"));
                            editImageView.setImageProcessingMethod(jElementToInsert.attr("method"));

                            //var imageLink = this._getImageOpenOriginalLink(jElementToInsert);
                            //if (imageLink) {
                            //    var origImgId = this._getIdFromSfrefAttr(imageLink);
                            //    if (origImgId == id) {
                            //        editImageView.setOpenOriginalImage(true);
                            //    }
                            //}
                            editImageView.hideOpenOriginalImage(true);
                            if (thumbnailName)
                                editImageView.setThumbnailName(thumbnailName);
                            else if (jElementToInsert.attr("displayMode") == "Custom") {
                                editImageView.selectSizeOptionCustom();
                            }

                            if (jElementToInsert.css('vertical-align') == "middle") {
                                editImageView.set_alignment('Center');
                            }
                            else {
                                switch (jElementToInsert.css('float')) {
                                    case "left":
                                        editImageView.set_alignment('Left');
                                        break;
                                    case "right":
                                        editImageView.set_alignment('Right');
                                        break;
                                    default:
                                        editImageView.set_alignment('None');
                                        break;
                                }
                            }

                            editImageView.setMargins(this._domElementToInsert.style.marginTop, this._domElementToInsert.style.marginRight,
                                this._domElementToInsert.style.marginBottom, this._domElementToInsert.style.marginLeft);
                            
                            dataItem = {
                                "Id": editImageView.getImageData().ImageId,
                                "MediaUrl": url,
                                "AlternativeText": editImageView.getImageData().AlternateText,
                                "ProviderName": editImageView.get_providerName()
                            };
                            break;
                            //Document                                                                                                                                                                                                                                                             
                        case 2:
                            url = jElementToInsert.attr("href");
                            var title = (jElementToInsert.children().length == 0) ? jElementToInsert.html() : null;

                            if (this._isGranted) {
                                file = this._getFileFromUrl(url);
                                jQuery(this.get_uploaderView().get_fileNameTextBox()).val(file);

                                if (title) {
                                    this.get_uploaderView().get_titleTextField().set_value(title);
                                }
                                else {
                                    jQuery(this.get_uploaderView().get_titleTextField().get_element()).hide();
                                }
                            }

                            dataItem = { "Id": id, "MediaUrl": url, "Title": title };
                            break;
                            //Media                                                                                                                                                                                                                                                                 
                        case 3:
                            var param = jElementToInsert.find("param");
                            url = param.attr("value");
                            var width = jElementToInsert.attr("width");

                            if (this._isGranted) {
                                file = this._getFileFromUrl(url);
                                jQuery(this.get_uploaderView().get_fileNameTextBox()).val(file);

                                this._setResizingOptionsControl(this.get_uploaderView().get_resizingOptionsControl(), width);
                            }

                            this._setResizingOptionsControl(this.get_selectorView().get_resizingOptionsControl(), width);

                            dataItem = { "Id": id, "MediaUrl": url };
                            break;
                        default:
                            throw "Not supported.";
                    }

                    this.set_selectedDataItem(dataItem);
                }
            }
            else {
                switch (this.get_dialogMode()) {
                    //NotSet                                                                                                                                                                                                                                                                 
                    case 0:
                        break;
                        //Image                                                                                                                                                                                                 
                    case 1:
                        var editImageView = this.get_editImageView();
                        if (editImageView)
                            editImageView.hideOpenOriginalImage(false);
                        break;
                        //Document            
                    case 2:
                        this.get_uploaderView().set_jElementToInsert(jElementToInsert);
                        break;
                        //Media                                                                                                                                                                                                                                                                 
                    case 3:
                        break;
                    default:
                        throw "Not supported.";
                }
            }
        }
    },

    _setJElementProperties: function (jElementToInsert, selectedDataItem) {
        return this._setElementSpecificAttributes(jElementToInsert, selectedDataItem);
    },

    _setElementSpecificAttributes: function (jElementToInsert, selectedDataItem) {
        //TODO: register enum
        var width = null;

        switch (this.get_dialogMode()) {
            //NotSet                                                                                                                                                                                                                                                                
            case 0:
                throw "Dialog mode not set.";
                break;
                //Image                                                                                                                                                                                                
            case 1:
                var sfref = "";
                var src = "";
                if (selectedDataItem.DisplayMode == "Thumbnail") {
                    sfref = this._getSfrefAttribute('images', selectedDataItem.ImageId, selectedDataItem.ThumbnailName);
                    src = selectedDataItem.ThumbnailUrl;
                } else {
                    sfref = this._getSfrefAttribute('images', selectedDataItem.ImageId);
                    src = selectedDataItem.MediaUrl;
                }

                if (selectedDataItem.DisplayMode == "Custom") {
                    var customSizeMethodPropertiesDictionary = selectedDataItem.CustomSizeMethodPropertiesDictionary;
                    customSizeMethodPropertiesDictionary["Method"] = selectedDataItem.Method;
                    var customSizeMethodProperties = Sys.Serialization.JavaScriptSerializer.serialize(customSizeMethodPropertiesDictionary);

                    var requestUrl = String.format(
                        "{0}/custom-image-thumbnail/url?imageId={1}&customUrlParameters={2}&libraryProvider={3}",
                        this.get_thumbnailServiceUrl(),
                        selectedDataItem.ImageId,
                        customSizeMethodProperties,
                        this.get_provider());
                                        
                    jQuery.ajax({
                        type: 'GET',
                        url: requestUrl,
                        processData: false,
                        async: false,
                        contentType: "application/json",
                        success: function (resolvedUrl) {
                            src = resolvedUrl;
                        }
                    });
                }

                jElementToInsert.attr("sfref", sfref);
                jElementToInsert.attr("src", src);

                jElementToInsert.attr("alt", selectedDataItem.AlternateText);
                if (selectedDataItem.Title) jElementToInsert.attr("title", selectedDataItem.Title);

                if (selectedDataItem.Method) jElementToInsert.attr("method", selectedDataItem.Method);
                else jElementToInsert.get(0).removeAttribute("method");

                if (selectedDataItem.CustomSizeMethodProperties) jElementToInsert.attr("customSizeMethodProperties", selectedDataItem.CustomSizeMethodProperties);
                else jElementToInsert.get(0).removeAttribute("customSizeMethodProperties");

                if (selectedDataItem.DisplayMode) jElementToInsert.attr("displayMode", selectedDataItem.DisplayMode);
                else jElementToInsert.get(0).removeAttribute("displayMode");

                //Image alignment
                switch (selectedDataItem.Alignment) {
                    case "Left":
                        jElementToInsert.css("float", "left");
                        break;
                    case "Right":
                        jElementToInsert.css("float", "right");
                        break;
                    case "Center":
                        jElementToInsert.css("float", "");
                        jElementToInsert.css("vertical-align", "middle");
                        break;
                    default:
                        jElementToInsert.css("float", "");
                        if (jElementToInsert.css('vertical-align') == "middle") jElementToInsert[0].style.verticalAlign = "";
                        break;
                }

                this._domElementToInsert.style.margin = "";
                if (selectedDataItem.MarginTop != null)
                    this._domElementToInsert.style.marginTop = selectedDataItem.MarginTop + "px";
                if (selectedDataItem.MarginBottom != null)
                    this._domElementToInsert.style.marginBottom = selectedDataItem.MarginBottom + "px";
                if (selectedDataItem.MarginLeft != null)
                    this._domElementToInsert.style.marginLeft = selectedDataItem.MarginLeft + "px";
                if (selectedDataItem.MarginRight != null)
                    this._domElementToInsert.style.marginRight = selectedDataItem.MarginRight + "px";


                var imageLink = this._getImageOpenOriginalLink(jElementToInsert);
                //if (imageLink) {
                //    var origImgId = this._getIdFromSfrefAttr(imageLink);
                //    if (origImgId == selectedDataItem.ImageId) {
                //        jElementToInsert.unwrap();
                //        var newElement = jElementToInsert.clone();
                //        jElementToInsert.remove();
                //        jElementToInsert = newElement;
                //    }
                //}
                if (!imageLink && selectedDataItem.OpenOriginalImageOnClick) {
                    var originalImageUrl = this._getSfrefAttribute('images', selectedDataItem.ImageId);
                    var jLinkWrapper = jQuery(document.createElement("a"))
                                                        .attr("sfref", originalImageUrl)
                                                        .attr("href", selectedDataItem.MediaUrl);
                    jLinkWrapper.append(jElementToInsert);
                    jElementToInsert = jLinkWrapper;
                }
                break;
                //Document                                                                                                                                                                                                                                                                
            case 2:
                jElementToInsert.attr("sfref", this._getSfrefAttribute('documents', selectedDataItem.Id));
                jElementToInsert.attr("href", selectedDataItem.MediaUrl);

                var title = null;
                var html = null;
                if (jElementToInsert.children().length > 0) {
                    html = jElementToInsert.html();
                }
                else {
                    if (this._currentMode == "1") {
                        title = this.get_uploaderView().get_titleTextField().get_value();
                    }
                    else {
                        title = jElementToInsert.html();
                        if (title == "")
                            title = selectedDataItem.Title;
                    }

                    html = title;
                }

                if (title)
                    jElementToInsert.attr("title", title);

                var showText = true;
                if (jElementToInsert.children().length > 0) {
                    var childElement = jElementToInsert.children().get(0);
                    var tagName = (childElement && childElement.tagName) ? childElement.tagName : "";
                    if (tagName == "IMG") {
                        showText = false;
                    }
                }

                if (showText) {
                    jElementToInsert.html(html);
                }
                break;
                //Media                                                                                                                                                                                                                                                                
            case 3:
                if (this._currentMode == "1") {
                    width = this.get_uploaderView().get_resizingOptionsControl().get_resizedWidth();
                }
                else {
                    width = this.get_selectorView().get_resizingOptionsControl().get_resizedWidth();
                }
                width = width ? width : 150;

                var objectElement = this._createObjectElement(this._getSfrefAttribute('videos', selectedDataItem.Id), selectedDataItem.MediaUrl, width);
                jElementToInsert.html(objectElement);
                break;
            default:
                throw "Not supported.";
        }
        return jElementToInsert;
    },

    _getSfrefAttribute: function (mediaType, id, thumbnailName) {
        sfref = '[' + mediaType;
        if (this.get_provider()) {
            sfref += '|' + this.get_provider();
        }
        if (thumbnailName && thumbnailName != "") {
            sfref += '|tmb%3A' + thumbnailName;
        }
        sfref += ']' + id;
        return sfref;
    },

    _createObjectElement: function (id, url, width) {
        var sb = new Sys.StringBuilder("");
        sb.append("<object");

        sb.append(String.format(" sfref=\"{0}\"", id));
        sb.append(" classid=\"clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6\"");
        sb.append(String.format(" width=\"{0}\"", width));
        sb.append(String.format(" height=\"{0}\"", width));
        sb.append(" type=\"application/x-oleobject\"");
        sb.append(">");

        //set object params
        sb.append(String.format("<param name=\"URL\" value=\"{0}\"", url));
        sb.append(String.format(" sfref=\"{0}\">", id));
        sb.append("<param name=\"autoStart\" value=\"false\">");

        //set embed attributes
        sb.append("<embed");
        sb.append(String.format(" src=\"{0}\"", url));
        sb.append(String.format(" width=\"{0}\"", width));
        sb.append(String.format(" height=\"{0}\"", width));
        sb.append(String.format(" sfref=\"{0}\"", id));
        sb.append(" type=\"application/x-mplayer2\"");
        sb.append(" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer\"");
        sb.append(" autoStart=\"false\"");
        sb.append("></embed>");
        sb.append("</object>");
        return sb.toString();
    },

    _getJElementToInsert: function () {
        if (this._domElementToInsert) {
            return jQuery(this._domElementToInsert);
        }
        return null;
    },

    _getSelectedDataItem: function () {
        var selectedDataItem = this.get_selectedDataItem();
        if (!selectedDataItem) {
            throw "No data item selected";
        }
        return selectedDataItem;
    },

    _getOuterHtml: function (element) {
        if (element) {
            return $telerik.getOuterHtml(element);
        }
        return "";
    },

    _switchToUploadMode: function () {
        this.showProvidersSelector();
        jQuery(this.get_uploaderSection()).show();
        jQuery(this.get_selectorSection()).hide();
        if (this.get_dialogMode() == 1) {
            var lblManager = this.get_clientLabelManager();
            this.set_saveLinkTitle(String.format(lblManager.getLabel("Labels", "Upload"), ""));
            this.set_title(lblManager.getLabel("ImagesResources", "SelectAnImage"));
            this._configureButtonArea(!this._isUploadFileNameTxtSet());
        }
    },

    _switchToSelectMode: function () {
        this.showProvidersSelector();
        jQuery(this.get_uploaderSection()).hide();
        jQuery(this.get_selectorSection()).show();
        if (this.get_dialogMode() == 1) {
            var lblManager = this.get_clientLabelManager();
            this.set_saveLinkTitle(lblManager.getLabel("Labels", "Done"));
            if (this._selectorViewTitle) {
                this.set_title(this._selectorViewTitle);
            } else {
                this.set_title(lblManager.getLabel("ImagesResources", "SelectAnImage"));
            }
            this._configureButtonArea(!this._isItemSelected());
        }
    },

    _configureButtonArea: function (disable) {
        if (disable) {
            this._itemWasSelected = false;
            jQuery(this.get_saveLink()).unbind('click');
            this._enableDisableLinkButton(true);
        } else
            if (!this._itemWasSelected) {
                this._itemWasSelected = true;
                jQuery(this.get_saveLink()).unbind('click');
                jQuery(this.get_saveLink()).click(this._closeDialogSaveDelegate);
                //jQuery(this.get_saveLink()).removeClass("sfDisabledLinkBtn").addClass("sfSave");
                this._enableDisableLinkButton();
            }
    },

    _enableDisableLinkButton: function (toDisable) {
        if (toDisable)
            jQuery(this.get_saveLink()).addClass("sfDisabledLinkBtn").removeClass("sfSave");
        else
            jQuery(this.get_saveLink()).removeClass("sfDisabledLinkBtn").addClass("sfSave");
    },

    _insertItem: function () {
        if (this._customInsertDelegate) {
            this._customInsertDelegate(this._getSelectedDataItem());
            return;
        }
        var selectedDataItem = this._getSelectedDataItem();
        this.set_provider(selectedDataItem.ProviderName);
        this.hideProvidersSelector();

        // Images
        if (this._dialogMode == 1) {
            // If Insert Button (not Save), we stop processing
            if (this._processInsertButtonClick(selectedDataItem))
                return;

            // If Save, we validate the EditImageView data
            var isEditImageViewValid = this.get_editImageView().validateImageData();
            if (!isEditImageViewValid) {
                this.resizeToContent();
                return;
            }

            selectedDataItem = this.get_editImageView().getImageData();
        }

        var jElementToInsert = this._getJElementToInsert();
        var elementAsHtml = null;

        jElementToInsert = this._setJElementProperties(jElementToInsert, selectedDataItem);
        var args = null;

        switch (this._dialogMode) {
            // Images                                                                          
            case 1:
                this._imageCloseArgs = { element: jElementToInsert };
                if (this.get_editImageView().uploadImage()) return;

                elementAsHtml = this._getOuterHtml(jElementToInsert.get(0));
                args = new Telerik.Sitefinity.CommandEventArgs("pasteHtml", elementAsHtml);
                break;
                // Documents                                                                          
            case 2:
                args = new Telerik.Sitefinity.CommandEventArgs("pasteLink", jElementToInsert.get(0));
                break;
                // Media                                                                          
            case 3:
                elementAsHtml = jElementToInsert.html();
                args = new Telerik.Sitefinity.CommandEventArgs("pasteHtml", elementAsHtml);
                break;
        }

        this.close(args);
    },

    _getFileFromUrl: function (url) {
        if (url) {
            var idx = url.lastIndexOf("/");
            if (idx > -1) {
                var file = url.substring(idx + 1);
                idx = file.indexOf("?");
                if (idx > -1) {
                    file = file.substring(0, idx);
                }
                return file;
            }
        }
        return "";
    },

    _getIdFromSfrefAttr: function (sfref) {
        if (sfref) {
            var idx = sfref.indexOf("]");
            if (idx > -1) {
                return sfref.substring(idx + 1);
            }
        }
        return null;
    },

    _getDialogModeFromSfrefAttr: function (sfref) {
        if (sfref) {
            var startIdx = sfref.indexOf("[");
            var endIdx = sfref.indexOf("]");
            if (startIdx > -1 && endIdx > -1) {
                var parts = sfref.substring(startIdx + 1, endIdx).split(new RegExp(['%7C', '\\\|'].join('|'), 'ig'));
                if (parts.length > 0) {
                    return parts[0];
                }
            }
        }
        return null;
    },

    _getProviderFromSfrefAttr: function (sfref) {
        if (sfref) {
            var startIdx = sfref.indexOf("[");
            var endIdx = sfref.indexOf("]");
            if (startIdx > -1 && endIdx > -1) {
                var parts = sfref.substring(startIdx + 1, endIdx).split(new RegExp(['%7C', '\\\|'].join('|'), 'ig'));
                if (parts.length > 1) {
                    for (var i = 1; i < parts.length; i++) {
                        if (parts[i].indexOf(":") === -1 || parts[i].toLowerCase().indexOf("%3a") === -1)
                            return parts[i];
                    }
                }
            }
        }
        return null;
    },

    _getThumbnailNameFromSfrefAttr: function (sfref) {
        if (sfref) {
            var startIdx = sfref.indexOf("[");
            var endIdx = sfref.indexOf("]");
            if (startIdx > -1 && endIdx > -1) {
                var parts = sfref.substring(startIdx + 1, endIdx).split(new RegExp(['%7C', '\\\|'].join('|'), 'ig'));
                if (parts.length > 1) {
                    var thumbnail = "tmb:", encodedThumbnail = "tmb%3a";
                    for (var i = 1; i < parts.length; i++) {
                        var indx = parts[i].toLowerCase().indexOf(thumbnail);
                        if (indx === 0)
                            return parts[i].toLowerCase().substring(indx + thumbnail.length);
                        indx = parts[i].toLowerCase().indexOf(encodedThumbnail);
                        if (indx === 0)
                            return parts[i].toLowerCase().substring(indx + encodedThumbnail.length);
                    }
                }
            }
        }
        return null;
    },

    _getDialogModeAsString: function (sfref) {
        switch (this.get_dialogMode()) {
            //NotSet                                                                                                                                                                                                                                                                 
            case 0:
                throw "Dialog mode not set.";
            case 1:
                return "images";
            case 2:
                return "documents";
            case 3:
                return "videos";
            default:
                throw "Not supported.";
        }
    },

    _setResizingOptionsControl: function (resizingOptionsControl, width) {
        if (width && width != "") {
            resizingOptionsControl._resizeSettingsExpanderClickHandler();
            resizingOptionsControl.get_resizeChoiceField().set_value("resize");
            if (resizingOptionsControl.get_sizesChoiceField()._get_listItemByValue(width).length > 0) {
                resizingOptionsControl.get_sizesChoiceField().set_value(width);
            }
            else {
                resizingOptionsControl.get_sizesChoiceField().set_value("custom");
                resizingOptionsControl.get_customWidthTextField().set_value(width);
            }
        }
    },

    /* ************************* Image specific methods ************************* */
    _attachEditImageViewHandlers: function (toAttach) {
        var imageView = this.get_editImageView();
        if (imageView && toAttach) {
            this._changeImageDelegate = Function.createDelegate(this, this._changeImageClick);
            imageView.add_changeImageClick(this._changeImageDelegate);

            this._editImageDelegate = Function.createDelegate(this, this._editImageClick);
            imageView.add_editImageClick(this._editImageDelegate);

            this._updateParametersDelegate = Function.createDelegate(this, this._updateParameters);
            imageView.add_updateParameters(this._updateParametersDelegate);

            this._closeEditImageDialogDelegate = Function.createDelegate(this, this._closeEditImageDialog)
            imageView.add_closeEditImageDialog(this._closeEditImageDialogDelegate);
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

            if (this._closeEditImageDialogDelegate)
                imageView.remove_closeEditImageDialog(this._closeEditImageDialogDelegate)
            delete this._closeEditImageDialogDelegate;
        }
    },
    _changeImageClick: function (sender, args) {
        this.showProvidersSelector();
        this.get_dialogModesSwitcher().get_element().parentNode.style.display = "";
        jQuery(this.get_editImageSection()).hide();
        var value = this.get_dialogModesSwitcher().get_value();
        if (value == "1") {
            this.set_saveLinkTitle(String.format(this.get_clientLabelManager().getLabel("Labels", "Upload"), ""));
            var disableUpload = !this._isUploadFileNameTxtSet();
            if (disableUpload) this._configureButtonArea(disableUpload);
        }
        else if (value == "2") {
            this.set_saveLinkTitle(this.get_clientLabelManager().getLabel("Labels", "Done"));
            var disableSelect = !this._isItemSelected();
            if (disableSelect) this._configureButtonArea(disableSelect);
        }
        this.resizeToContent();
    },
    _editImageClick: function (sender, args) {
        var ownd = dialogBase.get_radWindow();
        if (ownd) {
            ownd.get_popupElement().className += " reImageEditor";
            var browserWindow = ownd.get_browserWindow();
            if (browserWindow) {
                var bwDialogBase = browserWindow.dialogBase;
                if (bwDialogBase) {
                    bwDialogBase.setWndWidth(1000);
                    bwDialogBase.setWndHeight(700);
                    var bOwnd = bwDialogBase.get_radWindow();
                    if (bOwnd) {
                        bOwnd.center();
                        if (parseInt(bOwnd._popupElement.style.top) != 0)
                            bOwnd._popupElement.style.top = "50px";
                    }
                }
            }
        }
    },
    _updateParameters: function (sender, args) {
        if (args.MediaUrl) {
            this._imageCloseArgs.element.attr("src", args.MediaUrl);
        }
        elementAsHtml = this._getOuterHtml(this._imageCloseArgs.element.get(0));
        args = new Telerik.Sitefinity.CommandEventArgs("pasteHtml", elementAsHtml);
        this.close(args);
    },
    _closeEditImageDialog: function (sender, args) {
        var ownd = dialogBase.get_radWindow();
        if (ownd) {
            ownd.get_popupElement().className = ownd.get_popupElement().className.replace(" reImageEditor", "");
            var browserWindow = ownd.get_browserWindow();
            if (browserWindow) {
                var bwDialogBase = browserWindow.dialogBase;
                if (bwDialogBase) {
                    bwDialogBase.setWndWidth(730);
                    bwDialogBase.setWndHeight(565);
                    this.setWndHeight(600);
                    var bOwnd = bwDialogBase.get_radWindow();
                    if (bOwnd) {
                        bOwnd.center();
                        if (parseInt(bOwnd._popupElement.style.top) != 0)
                            bOwnd._popupElement.style.top = "50px";
                    }
                }
            }
        }
    },
    _switchToImageEditMode: function () {
        this.hideProvidersSelector();
        this.get_dialogModesSwitcher().get_element().parentNode.style.display = "none";
        jQuery(this.get_editImageSection()).show();
        this._configureButtonArea();
        jQuery(window.parent.document).find("html").scrollTop(0);

        var lblManager = this.get_clientLabelManager();
        this.set_saveLinkTitle(lblManager.getLabel("Labels", "Save"));
        var title = this._isInsertImage ? String.format(lblManager.getLabel("LibrariesResources", "InsertAItem"), lblManager.getLabel("ImagesResources", "ImageWithArticle")) : lblManager.getLabel("ImagesResources", "EditImage");
        this.set_title(title);

        this.setWndWidth(450);
        this.setWndHeight(650);
    },
    _processInsertButtonClick: function (imageData) {
        if (this.get_editImageSection().style.display == "none") {
            this.get_editImageView().set_image(imageData);
            this._switchToImageEditMode();
            return true;
        }
        return false;
    },
    _isImageWithOpenOriginalLink: function (jImage) {
        return jImage.parent("a[sfref='" + this._getEscapedSelectorString(jImage.attr("sfref")) + "']").length > 0;
    },

    _getImageOpenOriginalLink: function (jImage) {
        return jImage.parent().attr("sfref") ? jImage.parent().attr("sfref") : null;
    },

    _getEscapedSelectorString: function (str) {
        var charsToEscape = ["[", "]", "|", ":"];
        var len = charsToEscape.length;
        var i = 0;
        while (i < len) {
            str = str.replace(charsToEscape[i], "\\" + charsToEscape[i]);
            i++;
        }
        return str;
    },
    //returns a bool value that indicates whether the Upload input containing the fileName is empty
    _isUploadFileNameTxtSet: function () {
        return !!this.get_uploaderView().get_fileNameTextBox().value;
    },
    //returns a bool value that indicates whether an item is selected in the media selector control
    _isItemSelected: function () {
        return this._isMediaItemSelected;
    },

    _changeIsGranted: function (isGranted) {
        if (this._isGranted != isGranted) {
            this._isGranted = isGranted;
            if (this.get_dialogModesSwitcher()) {
                var modesSwitcher = jQuery(this.get_dialogModesSwitcher().get_element());
                if (this._isGranted) {
                    modesSwitcher.show();
                    this.resizeToContent();
                    this.resizeTopRadWindow();
                }
                else {
                    modesSwitcher.hide();
                    // if we don't have permissions switch to select mode
                    this.get_dialogModesSwitcher().set_value("2");
                }
            }
        }
    },

    get_isGrantedForCurrentProvider: function () {
        return this._isGrantedDictionary[this._provider];
    },

    _validateFileExtension: function (extension) {
        //if file extension is not specified => do not perform validation for file extension
        var doNotValidate = this.get_allowedExtensions() == null || this.get_allowedExtensions() == "";
        return doNotValidate || this.get_allowedExtensions().toLowerCase().indexOf(extension.toLowerCase()) != -1;
    },

    /* ************************* properties ************************* */

    get_uploaderView: function () {
        return this._uploaderView;
    },
    set_uploaderView: function (value) {
        this._uploaderView = value;
    },

    get_selectorView: function () {
        this._selectorView.set_uiCulture(this.get_uiCulture());
        this._selectorView.set_culture(this.get_culture());
        return this._selectorView
    },
    set_selectorView: function (value) {
        this._selectorView = value;
    },

    get_dialogModesSwitcher: function () {
        return this._dialogModesSwitcher;
    },
    set_dialogModesSwitcher: function (value) {
        this._dialogModesSwitcher = value;
    },

    get_saveLink: function () {
        return this._saveLink
    },
    set_saveLink: function (value) {
        this._saveLink = value;
    },
    set_saveLinkTitle: function (value) {
        jQuery(this.get_saveLink()).find(".sfLinkBtnIn").text(value);
    },

    get_cancelLink: function () {
        return this._cancelLink
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },

    get_titleLabel: function () {
        return this._titleLabel
    },
    set_titleLabel: function (value) {
        this._titleLabel = value;
    },
    set_title: function (value) {
        if (value) this.get_titleLabel().innerHTML = value;
    },

    get_cancelLinkTitle: function () {
        return this._cancelLinkTitle
    },

    set_cancelLinkTitle: function (value) {
        this._cancelLinkTitle = value;
    },

    get_dialogMode: function () {
        return this._dialogMode
    },
    set_dialogMode: function (value) {
        this._dialogMode = value;
    },

    get_selectedDataItem: function () {
        return this._selectedDataItem;
    },
    set_selectedDataItem: function (value) {
        this._selectedDataItem = value;
    },

    get_uploaderSection: function () {
        return this._uploaderSection;
    },
    set_uploaderSection: function (value) {
        this._uploaderSection = value;
    },

    get_selectorSection: function () {
        return this._selectorSection;
    },
    set_selectorSection: function (value) {
        this._selectorSection = value;
    },

    set_customInsertDelegate: function (insertDelegate) {
        this._customInsertDelegate = insertDelegate;
    },

    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this._culture = culture;
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this._culture;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    },

    get_libraryNotSelectedErrorMessage: function () {
        return this._libraryNotSelectedErrorMessage;
    },
    set_libraryNotSelectedErrorMessage: function (value) {
        this._libraryNotSelectedErrorMessage = value;
    },

    //Client side localization control
    get_clientLabelManager: function () { return this._clientLabelManager; },
    set_clientLabelManager: function (value) { this._clientLabelManager = value; },

    //Image specific properties
    get_editImageView: function () { return this._editImageView; },
    set_editImageView: function (value) { this._editImageView = value; },

    get_editImageSection: function () { return this._editImageSection; },
    set_editImageSection: function (value) { this._editImageSection = value; },

    get_maxFileSize: function () { return this._maxFileSize; },
    set_maxFileSize: function (value) { this._maxFileSize = value },

    get_allowedExtensions: function () { return this._allowedExtensions; },
    set_allowedExtensions: function (value) { this._allowedExtensions = value },

    get_provider: function () { return this._provider; },
    set_provider: function (value) { this._provider = value },

    get_providersSelector: function () { return this._providersSelector; },
    set_providersSelector: function (value) { this._providersSelector = value },

    get_isGrantedDictionary: function () { return this._isGrantedDictionary; },
    set_isGrantedDictionary: function (value) { this._isGrantedDictionary = value },

    get_noLibrariesOkButton: function () { return this._noLibrariesOkButton; },
    set_noLibrariesOkButton: function (value) { this._noLibrariesOkButton = value; },

    get_thumbnailServiceUrl: function () { return this._thumbnailServiceUrl; },
    set_thumbnailServiceUrl: function (value) { this._thumbnailServiceUrl = value; }
    
};
Telerik.Sitefinity.Web.UI.EditorContentManagerDialog.registerClass('Telerik.Sitefinity.Web.UI.EditorContentManagerDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
