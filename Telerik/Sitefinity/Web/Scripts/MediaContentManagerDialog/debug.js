Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.MediaContentManagerDialog = function (element) {
    this._buttonArea = null;
    this._saveLink = null;
    this._cancelLink = null;
    this._titleTextField = null;
    this._altTextField = null;
    this._videoTag = null;

    this._contentSelectorView = null;
    this._itemSettingsView = null;
    this._dialogMode = null;
    this._provider = null;
    this._thumbnailServiceUrl = null;

    this._domElementToInsert = null;

    this._loadDelegate = null;
    this._closeDialogSaveDelegate = null;
    this._closeDialogCancelDelegate = null;
    this._itemLoadedDelegate = null;
    this._itemSelectedDelegate = null;
    this._thumbnailExtensionPrefix = null;

    Telerik.Sitefinity.Web.UI.MediaContentManagerDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.MediaContentManagerDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.MediaContentManagerDialog.callBaseMethod(this, 'initialize');

        this._loadDelegate = Function.createDelegate(this, this._load);
        this._closeDialogCancelDelegate = Function.createDelegate(this, this._closeDialogCancel);
        this._closeDialogSaveDelegate = Function.createDelegate(this, this._closeDialogSave);
        this._itemLoadedDelegate = Function.createDelegate(this, this._itemLoaded);
        this._itemSelectedDelegate = Function.createDelegate(this, this._itemSelected);

        jQuery(this.get_cancelLink()).click(this._closeDialogCancelDelegate);

        this.get_contentSelectorView().add_onItemSelectedCommand(this._itemSelectedDelegate);
        this.get_contentSelectorView().add_onImageLoadedCommand(this._itemLoadedDelegate);

        Sys.Application.add_load(this._loadDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.MediaContentManagerDialog.callBaseMethod(this, 'dispose');

        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._closeDialogSaveDelegate) {
            delete this._closeDialogSaveDelegate;
        }
        if (this._closeDialogSaveDelegate) {
            delete this._closeDialogSaveDelegate;
        }
        if (this._itemSelectedDelegate) {
            this.get_contentSelectorView().remove_onItemSelectedCommand(this._itemSelectedDelegate);
            delete this._itemSelectedDelegate;
        }
        if (this._itemLoadedDelegate) {
            this.get_contentSelectorView().remove_onImageLoadedCommand(this._itemLoadedDelegate);
            delete this._itemLoadedDelegate;
        }
    },

    _load: function () {
        this._initializeParams();

        this._configureButtonArea();

        switch (this.get_dialogMode()) {
            case 1: //Image
                break;
            case 2: //Document
                $(this.get_altTextField().get_element()).toggle(false);
                jQuery(this.get_itemSettingsView().get_element()).hide();
                break
            case 3: //Media
                $(this.get_altTextField().get_element()).toggle(false);
                $(this.get_titleTextField().get_element()).toggle(false);
                break;
            default:
        }

        this._initializeFieldControlsFromElement();
        this.get_contentSelectorView().refreshUI(true);

        jQuery("body").addClass("sfSelectorDialog");
        if (/chrome/.test(navigator.userAgent.toLowerCase()))
            jQuery("body").addClass("sfOverflowHiddenX");
    },

    _itemLoaded: function (sender, args) {
        var item = args;

        var jElement = this._getJElementToInsert();
        var title = jElement.attr("title");
        var altText = jElement.attr("alt");

        //If there are no explicitly set title and alternative text, get them from the content item
        if (!title) {
            title = item.Title;
        }
        if (!altText && item.AlternativeText) {
            altText = item.AlternativeText;
        }

        this.get_titleTextField().set_value(title);
        this.get_altTextField().set_value(altText);

        this.resizeToContent();
        this.resizeTopRadWindow();
    },

    _itemSelected: function (sender, args) {
        var item = args;

        this.get_titleTextField().set_value(item.Title);
        this.get_altTextField().set_value(item.AlternativeText);

        this.resizeToContent();
        this.resizeTopRadWindow();
    },

    _configureButtonArea: function (disable) {
        jQuery(this.get_saveLink()).unbind('click');
        jQuery(this.get_saveLink()).click(this._closeDialogSaveDelegate);
        this._enableDisableLinkButton();
    },

    _enableDisableLinkButton: function (toDisable) {
        if (toDisable)
            jQuery(this.get_saveLink()).addClass("sfDisabledLinkBtn").removeClass("sfSave");
        else
            jQuery(this.get_saveLink()).removeClass("sfDisabledLinkBtn").addClass("sfSave");
    },

    _initializeParams: function () {
        var win = this.get_radWindow();
        if (win) {
            this._domElementToInsert = win.ClientParameters;
        }
    },

    _closeDialogSave: function (sender, args) {
        this._insertItem();
        this.resizeTopRadWindow();
    },

    _closeDialogCancel: function () {
        this.close();
        this.resizeTopRadWindow();

        //IE FIX: Explorer calls window.onbeforeunload when hyperlink (<a>)  with href="javascript:..." is clicked
        //Returning false solves the issue.
        return false;
    },

    _insertItem: function () {
        var selectedDataItem = this.get_contentSelectorView().get_selectedDataItem();

        this.set_provider(this.get_contentSelectorView().get_providerName());
        //this.hideProvidersSelector();

        // We validate the EditImageView data
        var isEditImageViewValid = this.get_itemSettingsView().validateItemData();
        if (!isEditImageViewValid) {
            this.resizeToContent();
            return;
        }

        var jElementToInsert = this._getJElementToInsert();
        if (jElementToInsert) {
            var elementAsHtml = null;

            jElementToInsert = this._setJElementProperties(jElementToInsert, selectedDataItem);
            var args = null;

            switch (this._dialogMode) {
                // Images                                                                          
                case 1:
                    this._imageCloseArgs = { element: jElementToInsert };
                    // if (this.get_editImageView().uploadImage()) return;

                    // It is possible the jElementToInsert to be null for images in the following cases:
                    // 1. We edit a image with OpenOriginalImageOnClick == false and we check the checkbox and make it true
                    // 2. We edit a image with OpenOriginalImageOnClick == true and we uncheck the checkbox and make it false
                    if (jElementToInsert)
                        elementAsHtml = this._getOuterHtml(jElementToInsert.get(0));
                    else
                        elementAsHtml = "";
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
        }
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

                    var itemSelectorView = this.get_contentSelectorView();
                    var itemSettingsView = this.get_itemSettingsView();

                    itemSelectorView.set_dataItemId(id);
                    itemSelectorView.set_providerName(provider);
                    this.set_provider(provider);

                    if (this._providersSelector) {
                        this._providersSelector.selectProvider(provider);
                    }

                    switch (this.get_dialogMode()) {
                        //NotSet                                                                                                                                                                                                                                                                 
                        case 0:
                            throw "Dialog mode not set.";
                            break;
                            //Image                                                                                                                                                                                                 
                        case 1:
                            this._isInsertImage = false;
                            url = jElementToInsert.attr("src");

                            var customSizeMethodProperties = jElementToInsert.attr("customSizeMethodProperties") || jElementToInsert.attr("data-customSizeMethodProperties");
                            itemSettingsView.setMethodControlsProperties(customSizeMethodProperties);

                            var method = jElementToInsert.attr("method") || jElementToInsert.attr("data-method");
                            itemSettingsView.setImageProcessingMethod(method);

                            var displayMode = jElementToInsert.attr("displayMode") || jElementToInsert.attr("data-displayMode");

                            if (thumbnailName) {
                                itemSettingsView.set_selectedThumbnailName(thumbnailName);
                            }
                            else if (displayMode == "Custom") {
                                itemSettingsView.selectSizeOptionCustom();
                            }

                            if (jElementToInsert.css('vertical-align') == "middle") {
                                itemSettingsView.set_alignment('Center');
                            }
                            else {
                                switch (jElementToInsert.css('float')) {
                                    case "left":
                                        itemSettingsView.set_alignment('Left');
                                        break;
                                    case "right":
                                        itemSettingsView.set_alignment('Right');
                                        break;
                                    default:
                                        itemSettingsView.set_alignment('None');
                                        break;
                                }
                            }

                            itemSettingsView.setMargins(this._domElementToInsert.style.marginTop, this._domElementToInsert.style.marginRight,
                                this._domElementToInsert.style.marginBottom, this._domElementToInsert.style.marginLeft);

                            if ((jElementToInsert.attr("openOriginalImageOnClick") || jElementToInsert.attr("data-openOriginalImageOnClick")) === "true") {
                                itemSettingsView.setOpenOriginalImage(true);
                            } else {
                                itemSettingsView.setOpenOriginalImage(false);
                            }

                            dataItem = {
                                "Id": id,
                                "MediaUrl": url,
                                //"AlternativeText": itemSettingsView.getImageData().AlternateText,
                                "ProviderName": provider,
                                "Extension": ".jpg"
                            };
                            break;
                            //Document                                                                                                                                                                                                                                                             
                        case 2:
                            url = jElementToInsert.attr("href");
                            var title = (jElementToInsert.children().length == 0) ? jElementToInsert.html() : null;

                            file = this._getFileFromUrl(url);
                            //jQuery(this.get_uploaderView().get_fileNameTextBox()).val(file);

                            if (title) {
                                this.get_titleTextField().set_value(title);
                            }
                            else {
                                jQuery(this.get_titleTextField().get_element()).hide();
                            }

                            dataItem = {
                                "Id": id,
                                "MediaUrl": url,
                                "Title": title,
                                "ProviderName": provider,
                                "Extension": ".pdf"
                            };
                            break;
                            //Media                                                                                                                                                                                                                                                                 
                        case 3:
                            var param = jElementToInsert.find("param");
                            if (param) {
                                url = param.attr("value");
                            }
                            else {
                                url = jElementToInsert.attr("src");
                            }

                            var width = jElementToInsert.find("[width]").attr("width");

                            file = this._getFileFromUrl(url);
                            this.get_titleTextField().set_value(file);
                            //jQuery(this.get_uploaderView().get_fileNameTextBox()).val(file);

                            this._setResizingOptionsControl(this.get_itemSettingsView().get_resizingOptionsControl(), width);

                            dataItem = {
                                "Id": id,
                                "MediaUrl": url,
                                "ProviderName": provider,
                                "Extension": ".wmv"
                            };
                            break;
                        default:
                            throw "Not supported.";
                    }

                    itemSelectorView.setData(dataItem);
                }
            }
            else {
                switch (this.get_dialogMode()) {
                    //NotSet                                                                                                                                                                                                                                                                 
                    case 0:
                        break;
                        //Image                                                                                                                                                                                                 
                    case 1:
                        var itemSettingsView = this.get_itemSettingsView();
                        if (itemSettingsView)
                            itemSettingsView.setOpenOriginalImage(false);
                        break;
                        //Document            
                    case 2:
                        //this.get_uploaderView().set_jElementToInsert(jElementToInsert);
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

    _getJElementToInsert: function () {
        if (this._domElementToInsert) {
            return jQuery(this._domElementToInsert);
        }
        return null;
    },

    _setJElementProperties: function (jElementToInsert, selectedDataItem) {
        return this._setElementSpecificAttributes(jElementToInsert, selectedDataItem);
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

    _setElementSpecificAttributes: function (jElementToInsert, selectedDataItem) {
        //TODO: register enum
        var width = null;

        var contentItemSettings = this.get_itemSettingsView().getItemData();

        switch (this.get_dialogMode()) {
            //NotSet                                                                                                                                                                                                                                                                
            case 0:
                throw "Dialog mode not set.";
                break;
                //Image                                                                                                                                                                                                
            case 1:
                var sfref = "";
                var src = "";
                if (contentItemSettings.DisplayMode == "Thumbnail") {
                    sfref = this._getSfrefAttribute('images', selectedDataItem.Id, contentItemSettings.ThumbnailName);
                    src = this._resolveThumbnailUrl(selectedDataItem.ThumbnailUrl, contentItemSettings.ThumbnailName);
                } else {
                    sfref = this._getSfrefAttribute('images', selectedDataItem.Id);
                    src = selectedDataItem.MediaUrl;
                }

                if (contentItemSettings.DisplayMode == "Custom") {
                    var customSizeMethodPropertiesDictionary = contentItemSettings.CustomSizeMethodPropertiesDictionary;
                    customSizeMethodPropertiesDictionary["Method"] = contentItemSettings.Method;
                    var customSizeMethodProperties = Sys.Serialization.JavaScriptSerializer.serialize(customSizeMethodPropertiesDictionary);

                    var requestUrl = String.format(
                        "{0}/custom-image-thumbnail/url?imageId={1}&customUrlParameters={2}&libraryProvider={3}",
                        this.get_thumbnailServiceUrl(),
                        selectedDataItem.Id,
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

                    if (selectedDataItem.IsVectorGraphics === true) {
                        if (customSizeMethodPropertiesDictionary["MaxWidth"]) {
                            jElementToInsert.attr("width", customSizeMethodPropertiesDictionary["MaxWidth"]);
                        }
                        if (customSizeMethodPropertiesDictionary["MaxHeight"]) {
                            jElementToInsert.attr("height", customSizeMethodPropertiesDictionary["MaxHeight"]);
                        }
                    }
                    else {
                        jElementToInsert.removeAttr("width");
                        jElementToInsert.removeAttr("height");
                    }
                }
                else {
                    jElementToInsert.removeAttr("width");
                    jElementToInsert.removeAttr("height");
                }

                var oldSfref = jElementToInsert.attr("sfref");
                var currentImageSfref = sfref;
                jElementToInsert.attr("sfref", sfref);
                jElementToInsert.attr("src", src);

                jElementToInsert.attr("alt", contentItemSettings.AlternateText);
                if (contentItemSettings.Title) jElementToInsert.attr("title", contentItemSettings.Title);

                jElementToInsert.removeAttr("method");
                if (contentItemSettings.Method) jElementToInsert.attr("data-method", contentItemSettings.Method);
                else jElementToInsert.removeAttr("data-method");

                jElementToInsert.removeAttr("customSizeMethodProperties");
                if (contentItemSettings.CustomSizeMethodProperties) jElementToInsert.attr("data-customSizeMethodProperties", contentItemSettings.CustomSizeMethodProperties);
                else jElementToInsert.removeAttr("data-customSizeMethodProperties");

                jElementToInsert.removeAttr("displayMode");
                if (contentItemSettings.DisplayMode) jElementToInsert.attr("data-displayMode", contentItemSettings.DisplayMode);
                else jElementToInsert.removeAttr("data-displayMode");

                jElementToInsert.attr("alt", this.get_altTextField().get_value());
                if (selectedDataItem.Title) jElementToInsert.attr("title", this.get_titleTextField().get_value());

                //Image alignment
                jElementToInsert.css("float", "");
                jElementToInsert.css("vertical-align", "");
                switch (contentItemSettings.Alignment) {
                    case "Left":
                        jElementToInsert.css("float", "left");
                        break;
                    case "Right":
                        jElementToInsert.css("float", "right");
                        break;
                    case "Center":
                        jElementToInsert.css("vertical-align", "middle");
                        break;
                    default:
                        break;
                }

                this._domElementToInsert.style.margin = "";
                if (contentItemSettings.MarginTop != null)
                    this._domElementToInsert.style.marginTop = contentItemSettings.MarginTop + "px";
                if (contentItemSettings.MarginBottom != null)
                    this._domElementToInsert.style.marginBottom = contentItemSettings.MarginBottom + "px";
                if (contentItemSettings.MarginLeft != null)
                    this._domElementToInsert.style.marginLeft = contentItemSettings.MarginLeft + "px";
                if (contentItemSettings.MarginRight != null)
                    this._domElementToInsert.style.marginRight = contentItemSettings.MarginRight + "px";

                var oldOpenOriginalImageOnClick = jElementToInsert.attr("openOriginalImageOnClick") || jElementToInsert.attr("data-openOriginalImageOnClick");
                jElementToInsert.removeAttr("openOriginalImageOnClick");

                if (contentItemSettings.OpenOriginalImageOnClick) {
                    jElementToInsert.attr("data-openOriginalImageOnClick", "true");
                } else if (jElementToInsert.attr("data-openOriginalImageOnClick")) {
                    jElementToInsert.removeAttr("data-openOriginalImageOnClick");
                }

                // We check if the image is wrapped in an anchor tag, we check if this anchor tag is related to the image and we update its 
                // sfref attribute if needed
                var imageLinkSfref = this._getImageOpenOriginalLink(jElementToInsert);
                var isWrappingLinkImpacted = (imageLinkSfref && oldOpenOriginalImageOnClick);
                if (isWrappingLinkImpacted) {
                    var jWrappingLink = jElementToInsert.parent();
                    var newOriginalImageSfref = this._getSfrefAttribute('images', selectedDataItem.Id);
                    jWrappingLink.attr("sfref", newOriginalImageSfref);
                }

                // If there is no wrapping anchor tag and we need to create one, we first remove the wrapping image span - this way we avoid the RadEditor
                // related problems.
                if (!imageLinkSfref && contentItemSettings.OpenOriginalImageOnClick) {
                    var originalImageSfref = this._getSfrefAttribute('images', selectedDataItem.Id);
                    jElementToInsert.wrap("<a></a>");
                    jElementToInsert = jElementToInsert.parent();
                    jElementToInsert.attr("sfref", originalImageSfref).attr("href", selectedDataItem.MediaUrl);
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
                    title = this.get_titleTextField().get_value();
                    if (title === "")
                        title = jElementToInsert.html();
                    if (title === "")
                        title = selectedDataItem.Title;

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
                width = contentItemSettings.ResizedWidth ? contentItemSettings.ResizedWidth : 150;

                var objectElement = this._createObjectElement(this._getSfrefAttribute('videos', selectedDataItem.Id), selectedDataItem.MediaUrl, width);
                jElementToInsert.html(objectElement);
                break;
            default:
                throw "Not supported.";
        }
        return jElementToInsert;
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

    _resolveThumbnailUrl: function (tmbDefaultUrl, tmbName) {
        if (tmbName) {
            var parts = tmbDefaultUrl.split(".");
            if (parts.length > 1) {
                var url = "";
                for (var i = 0; i < parts.length; i++) {
                    if (url.length > 0)
                        url = url + "."
                    if (parts[i].indexOf(this.get_thumbnailExtensionPrefix()) == 0)
                        url = url + this._thumbnailExtensionPrefix + tmbName;
                    else
                        url = url + parts[i];
                }
                return url;
            }
        }
        return tmbDefaultUrl;
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

    _createObjectElement: function (srefId, url, width) {
        var tag = this.get_videoTag();

        var sb = new Sys.StringBuilder("");

        if (tag === "Video") {
            sb.append("<video");
            sb.append(String.format(" sfref=\"{0}\"", srefId));
            sb.append(String.format(" src=\"{0}\"", url));
            sb.append(String.format(" width=\"{0}\"", width));
            sb.append(String.format(" height=\"{0}\"", width));
            sb.append(" controls=\"true\"");
            sb.append("></video>");
        }
        else {
            sb.append("<object");

            sb.append(String.format(" sfref=\"{0}\"", srefId));
            sb.append(" classid=\"clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6\"");
            sb.append(String.format(" width=\"{0}\"", width));
            sb.append(String.format(" height=\"{0}\"", width));
            sb.append(" type=\"application/x-oleobject\"");
            sb.append(">");

            //set object params
            sb.append(String.format("<param name=\"URL\" value=\"{0}\"", url));
            sb.append(String.format(" sfref=\"{0}\">", srefId));
            sb.append("<param name=\"autoStart\" value=\"false\">");

            //set embed attributes
            sb.append("<embed");
            sb.append(String.format(" src=\"{0}\"", url));
            sb.append(String.format(" width=\"{0}\"", width));
            sb.append(String.format(" height=\"{0}\"", width));
            sb.append(String.format(" sfref=\"{0}\"", srefId));
            sb.append(" type=\"application/x-mplayer2\"");
            sb.append(" pluginspage=\"http://www.microsoft.com/Windows/MediaPlayer\"");
            sb.append(" autoStart=\"false\"");
            sb.append("></embed>");
            sb.append("</object>");
        }
        return sb.toString();
    },

    _getOuterHtml: function (element) {
        if (element) {
            return $telerik.getOuterHtml(element);
        }
        return "";
    },

    _isImageWithOpenOriginalLink: function (jImage) {
        return jImage.parent("a[sfref='" + this._getEscapedSelectorString(jImage.attr("sfref")) + "']").length > 0;
    },

    _getImageOpenOriginalLink: function (jImage) {
        if (jImage.parent().prop('tagName') !== "A")
            return null;
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

    resizeTopRadWindow: function () {
        var currentRadWindow = this.get_radWindow();
        if (currentRadWindow) {
            var bounds = $telerik.getBounds(currentRadWindow.get_popupElement());
            var topRadWindow = this.get_radWindow().get_contentFrame().contentWindow.top.$find("PropertyEditorDialog");
            if (topRadWindow) {
                setTimeout(function () {
                    currentRadWindow.set_modal(false);
                    currentRadWindow.AjaxDialog.resizeToContent();
                    if (currentRadWindow.isVisible()) {
                        currentRadWindow.set_modal(true);
                        currentRadWindow._afterShow();
                        currentRadWindow.moveTo(bounds.x, bounds.y);
                    }
                }, 0);
            }
        }
    },

    /* Script descriptor properties */
    get_buttonArea: function () {
        return this._buttonArea;
    },
    set_buttonArea: function (value) {
        this._buttonArea = value;
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

    get_dialogMode: function () {
        return this._dialogMode
    },
    set_dialogMode: function (value) {
        this._dialogMode = value;
    },

    get_videoTag: function () {
        return this._videoTag;
    },
    set_videoTag: function (value) {
        this._videoTag = value;
    },

    //gets the input used for specifying the title
    get_titleTextField: function () { return this._titleTextField; },
    set_titleTextField: function (value) { this._titleTextField = value; },

    //gets the TextField used for setting the Alternate text of the image
    get_altTextField: function () { return this._altTextField; },
    set_altTextField: function (value) { this._altTextField = value; },

    get_contentSelectorView: function () { return this._contentSelectorView },
    set_contentSelectorView: function (value) { this._contentSelectorView = value },

    get_itemSettingsView: function () { return this._itemSettingsView },
    set_itemSettingsView: function (value) { this._itemSettingsView = value },

    get_provider: function () { return this._provider },
    set_provider: function (value) { this._provider = value },

    get_thumbnailServiceUrl: function () { return this._thumbnailServiceUrl; },
    set_thumbnailServiceUrl: function (value) { this._thumbnailServiceUrl = value; },

    get_thumbnailExtensionPrefix: function () { return this._thumbnailExtensionPrefix },
    set_thumbnailExtensionPrefix: function (value) { this._thumbnailExtensionPrefix = value }
}

Telerik.Sitefinity.Web.UI.MediaContentManagerDialog.registerClass('Telerik.Sitefinity.Web.UI.MediaContentManagerDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);