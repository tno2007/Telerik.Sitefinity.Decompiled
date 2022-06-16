/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.AssetsField = function (element) {

    Telerik.Sitefinity.Web.UI.Fields.AssetsField.initializeBase(this, [element]);
    this._element = element;
    this._selectAssetsButton = null;
    this._selector = null;
    this._selectorDialog = null;
    this._assetsWorkMode = null;
    this._maxFileSize = null;
    this._allowedExtensions = null;
    this._imageTypeFullName = null;
    this._documentTypeFullName = null;
    this._videoTypeFullName = null;
    this._selectedAssetsList = null;
    this._preSelectItemInSelector = null;

    this._imageServiceUrl = null;
    this._documentServiceUrl = null;
    this._videoServiceUrl = null;

    this._value = null;

    this._actionsLabel = null;
    this._removeLabel = null;
    this._viewOriginalSizeLabel = null;
    this._editPropertiesLabel = null;
    this._playVideoLabel = null;
    this._setAsPrimaryImageLabel = null;
    this._setAsPrimaryVideoLabel = null;
    this._enabled = null;
    this._isBackendReadMode = null;
    this._isPreviewMode = false;
    this._isDataBound = false;
    this._uiCulture = null;
    this._culture = null;

    this._selectLabel = null;
    this._changeLabel = null;

    this._selectedAssets = [];

    this.SingleImage = "SingleImage";
    this.MultipleImages = "MultipleImages";
    this.SingleDocument = "SingleDocument";
    this.MultipleDocuments = "MultipleDocuments";
    this.SingleVideo = "SingleVideo";
    this.MultipleVideos = "MultipleVideos";
    this.thisElement = null;
    this.thumbnailWidth = 95;
}

Telerik.Sitefinity.Web.UI.Fields.AssetsField.prototype =
{
    initialize: function () {
        this.thisElement = jQuery(this._element);
        if (this._selector) {
            var width = this._selector._useOnlyUploadMode ? 650 : 930;
            this._selectorDialog = jQuery(this._selector.get_element()).dialog({
                autoOpen: false,
                modal: true,
                width: width,
                height: "auto",
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                classes: {
                    "ui-dialog": "sfSelectorDialog sfZIndexL"
                }
            });

            this._selectorInsertDelegate = Function.createDelegate(this, this._selectorInsertHandler);
            this._selector.set_customInsertDelegate(this._selectorInsertDelegate);
            this._selector.set_maxFileSize(this._maxFileSize);
            this._selector.set_allowedExtensions(this._allowedExtensions);
        }
        if (this.get_selectAssetsButton()) {
            var me = this;
            jQuery(this.get_selectAssetsButton()).click(function () {
                if (me._enabled) {
                    if (me._selectedAssets && me.get_preSelectItemInSelector()) {
                        var ids = me._selectedAssets.map(function (currentValue) {
                            return currentValue.ChildItemId;
                        });
                        me._selector.setPreSelectedItems(ids);
                        me._selector.open();
                    }
                    me._selectorDialog.dialog("open");
                    me._dialogScrollToTop(me._selectorDialog);
                    if (!me._isDataBound) {
                        me.get_selector().rebind();
                        me._isDataBound = true;
                    }

                }
            });

            if (!this._enabled) {
                jQuery(this.get_selectAssetsButton()).addClass("sfDisabledLinkBtn");
            }
        }
        switch (this._assetsWorkMode) {
            case this.MultipleImages:
            case this.MultipleDocuments:
            case this.MultipleVideos:
                this._bindSortable(".sfGrippy");
                break;
        }

        Telerik.Sitefinity.Web.UI.Fields.AssetsField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.AssetsField.callBaseMethod(this, "dispose");
        this._selectedAssets = [];
        delete this._selectedAssets;
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.AssetsField.callBaseMethod(this, "reset");
        this._selectedAssets = [];
        $(this.get_selectedAssetsList()).empty();
        this._setActionVerb();
    },

    // Gets the value of the field control.
    get_value: function () {
        return this._selectedAssets;
    },

    // Sets the value of the text field control depending on DisplayMode.
    set_value: function (value) {
        if (value === this._selectedAssets) {
            return;
        }

        if (value) {
            value.sort(function (a, b) { return a.Ordinal - b.Ordinal });
        }

        //clear old values
        this._selectedAssets.length = 0;


        if (value) {

            for (var vIter = 0; vIter < value.length; vIter++) {

                //set unique ID to the asset element
                //The ID is used as identifier of the 'li' element
                //in ordert to sort, resize and remove the asset
                var containerId = this.generateGuid();
                this._selectedAssets.push(
                    {
                        "ChildItemId": value[vIter].ChildItemId,
                        "ChildItemType": value[vIter].ChildItemType,
                        "ChildItemProviderName": value[vIter].ChildItemProviderName,
                        "ChildItemAdditionalInfo": value[vIter].ChildItemAdditionalInfo,
                        "Ordinal": value[vIter].Ordinal,
                        "ContainerId": containerId
                    }
                );
            }

            this.loadItems(this._selectedAssets);
            this._setActionVerb(value.length);
        } else {
            this._setActionVerb();
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
    _selectorInsertHandler: function (selectedItem, containerId) {

        var childItemType = null;
        var additionalInfo = null;
        var singleModeRemovedAsset = null;

        if (!containerId) {
            containerId = this.generateGuid();
        }

        var isTranslated = this.isDataItemTranslatedForCurrentCulture(selectedItem);

        switch (this._assetsWorkMode) {
            case this.SingleImage:
                singleModeRemovedAsset = this._selectedAssets.length > 0 ? this._selectedAssets[0] : null;
                this._selectedAssets.length = 0;
                $(this.get_selectedAssetsList()).empty();
                this.addListElement(containerId, "sfAsset sfInlineBlock");                
                this.addImage(containerId, selectedItem.ThumbnailUrl, selectedItem.MediaUrl, true, isTranslated);
                childItemType = this._imageTypeFullName;
                additionalInfo = selectedItem.ThumbnailUrl;
                break;
            case this.MultipleImages:
                this.addListElement(containerId, "sfAsset sfInlineBlock");
                this.addImage(containerId, selectedItem.ThumbnailUrl, selectedItem.MediaUrl, false, isTranslated);
                childItemType = this._imageTypeFullName;
                additionalInfo = selectedItem.ThumbnailUrl;
                break;
            case this.SingleDocument:
                singleModeRemovedAsset = this._selectedAssets.length > 0 ? this._selectedAssets[0] : null;
                this._selectedAssets.length = 0;
                $(this.get_selectedAssetsList()).empty();
                this.addListElement(containerId, "sfAsset");
                this.addDocument(containerId, selectedItem.Title, selectedItem.Extension, selectedItem.TotalSize, true, isTranslated);
                childItemType = this._documentTypeFullName;
                additionalInfo = selectedItem.Title + "." + selectedItem.Extension + " (" + selectedItem.TotalSize + " KB )";
                break;
            case this.MultipleDocuments:
                this.addListElement(containerId, "sfAsset");
                this.addDocument(containerId, selectedItem.Title, selectedItem.Extension, selectedItem.TotalSize, false, isTranslated);
                childItemType = this._documentTypeFullName;
                additionalInfo = selectedItem.Title + "." + selectedItem.Extension + " (" + selectedItem.TotalSize + " KB )";
                break;
            case this.SingleVideo:
                singleModeRemovedAsset = this._selectedAssets.length > 0 ? this._selectedAssets[0] : null;
                this._selectedAssets.length = 0;
                $(this.get_selectedAssetsList()).empty();
                this.addListElement(containerId, "sfAsset sfInlineBlock");
                this.addVideo(containerId, selectedItem.SnapshotUrl, true, isTranslated, selectedItem.Title);
                childItemType = this._videoTypeFullName;
                additionalInfo = selectedItem.SnapshotUrl;
                break;
            case this.MultipleVideos:
                this.addListElement(containerId, "sfAsset sfInlineBlock");
                this.addVideo(containerId, selectedItem.SnapshotUrl, false, isTranslated, selectedItem.Title);
                childItemType = this._videoTypeFullName;
                additionalInfo = selectedItem.SnapshotUrl;
                break;
        }

        var ordinal = this.getOrdinal();

        var selectedAsset = {
            "ChildItemId": this.getChildItemId(selectedItem),
            "ChildItemType": childItemType,
            "ChildItemProviderName": selectedItem.ProviderName,
            "ChildItemAdditionalInfo": additionalInfo,
            "Ordinal": ordinal,
            "ContainerId": containerId
        };
        this._selectedAssets.push(selectedAsset);
        this._selectorDialog.dialog("close");
        this._setActionVerb();
        this.raiseEvent("actionExecuted", {
            Name: "Add",
            SingleModeRemovedAsset: singleModeRemovedAsset,
            Asset: selectedAsset
        });
    },

    /* -------------------- private methods ----------- */

    getOrdinal: function () {
        //Get the maximum Ordinal in collection. In case there are no other items
        //the ordinal of the item will be set to 0
        var maxOrdinal = -1;
        if (this._selectedAssets.length > 0) {
            maxOrdinal = this._selectedAssets[0].Ordinal;
            for (var i = 1, len = this._selectedAssets.length; i < len; i++) {
                if (this._selectedAssets[i].Ordinal > maxOrdinal) {
                    maxOrdinal = this._selectedAssets[i].Ordinal;
                }
            }
        }

        return maxOrdinal + 1;
    },

    getChildItemId: function (item) {
        return item.Id;
    },

    loadItems: function (contentLinks) {
        $(this.get_selectedAssetsList()).empty();

        if (contentLinks == null) {
            return;
        }

        if (contentLinks.length == 0) {
            return;
        }

        var me = this;
        var containerId = "";

        switch (this._assetsWorkMode) {
            case this.SingleImage:
                containerId = contentLinks[0].ContainerId;
                me.addListElement(containerId, "sfAsset sfInlineBlock");
                $.ajax({
                    type: 'GET',
                    url: this._imageServiceUrl + "live/" + contentLinks[0].ChildItemId + "/?provider=" + contentLinks[0].ChildItemProviderName,
                    contentType: "application/json",
                    processData: false,
                    success: me.onLoadImageSuccess(containerId, true),
                    error: me.onError(containerId)
                });
                break;
            case this.MultipleImages:
                for (i = 0; i < contentLinks.length; i++) {
                    containerId = contentLinks[i].ContainerId;
                    me.addListElement(containerId, "sfAsset sfInlineBlock");
                    $.ajax({
                        type: 'GET',
                        url: this._imageServiceUrl + "live/" + contentLinks[i].ChildItemId + "/?provider=" + contentLinks[i].ChildItemProviderName,
                        contentType: "application/json",
                        processData: false,
                        success: me.onLoadImageSuccess(containerId),
                        error: me.onError(containerId)
                    });
                }
                break;
            case this.SingleDocument:
                containerId = contentLinks[0].ContainerId;
                me.addListElement(containerId, "sfAsset");
                $.ajax({
                    type: 'GET',
                    url: this._documentServiceUrl + "live/" + contentLinks[0].ChildItemId + "/?provider=" + contentLinks[0].ChildItemProviderName,
                    contentType: "application/json",
                    processData: false,
                    success: me.onLoadDocumentSuccess(containerId, true),
                    error: me.onError(containerId)
                });
                break;
            case this.MultipleDocuments:
                for (i = 0; i < contentLinks.length; i++) {
                    containerId = contentLinks[i].ContainerId;
                    me.addListElement(containerId, "sfAsset");
                    $.ajax({
                        type: 'GET',
                        url: this._documentServiceUrl + "live/" + contentLinks[i].ChildItemId + "/?provider=" + contentLinks[i].ChildItemProviderName,
                        contentType: "application/json",
                        processData: false,
                        success: me.onLoadDocumentSuccess(containerId),
                        error: me.onError(containerId)
                    });
                }
                break;
            case this.SingleVideo:
                containerId = contentLinks[0].ContainerId;
                me.addListElement(containerId, "sfAsset sfInlineBlock");
                $.ajax({
                    type: 'GET',
                    url: this._videoServiceUrl + "live/" + contentLinks[0].ChildItemId + "/?provider=" + contentLinks[0].ChildItemProviderName,
                    contentType: "application/json",
                    processData: false,
                    success: me.onLoadVideoSuccess(containerId, true),
                    error: me.onError(containerId)
                });
                break;
            case this.MultipleVideos:
                for (i = 0; i < contentLinks.length; i++) {
                    containerId = contentLinks[i].ContainerId;
                    me.addListElement(containerId, "sfAsset sfInlineBlock");
                    $.ajax({
                        type: 'GET',
                        url: this._videoServiceUrl + "live/" + contentLinks[i].ChildItemId + "/?provider=" + contentLinks[i].ChildItemProviderName,
                        contentType: "application/json",
                        processData: false,
                        success: me.onLoadVideoSuccess(containerId),
                        error: me.onError(containerId)
                    });
                }
                break;
        }

    },

    addListElement: function (id, cssClass) {
        var imageMarkup = '<li id="asset' + id + '" class="' + cssClass + '"></li>';
        $(this.get_selectedAssetsList()).append(imageMarkup);
    },

    addImage: function (id, thumbnailUrl, originalUrl, isSingleImage, isTranslated) {
        var cssWrapperClass = "sfTmbWrp";
        var imageUrl = thumbnailUrl;
        var imageId = id;

        if (isSingleImage && this.get_isPreviewMode()) {
            cssWrapperClass = "";
            imageUrl = originalUrl;
            imageId += "Preview";
        }

        var imageMarkup = '<div class="' + cssWrapperClass + '">';

        if (!(this.get_isPreviewMode() && isSingleImage)) {
            imageMarkup += '<img id="' + imageId + '" src="' + imageUrl + '" width="' + this.thumbnailWidth + '" /></div>';
        }
        else {
            imageMarkup += '<img id="' + imageId + '" src="' + imageUrl + '" /></div>';
        }

        if (!this._isBackendReadMode) {
            imageMarkup += '<div class="sfActionWrp sfClearfix">';
            if (!isSingleImage) {
                imageMarkup += '<span class="sfGrippy"></span>';
            }

            imageMarkup += '<a class="sfActionMenuTrigger sfLinkBtn sfOptionsChange sfSecondaryBtn"><span class="sfLinkBtnIn">' + this._actionsLabel + '</span></a>';
            imageMarkup += '<div style="display:none;" class="outerbox inner">';
            imageMarkup += '<ul class="innerBox">';
            imageMarkup += '<li><a href="' + originalUrl + '" target="_BLANK" class="sfViewOrg">' + this._viewOriginalSizeLabel + '</a></li>';
            imageMarkup += '<li><a data-command="delete" data-id="' + id + '" class="sfRemoveItm">' + this._removeLabel + '</a></li>';
            imageMarkup += '</ul>';
            imageMarkup += '</div>';
            imageMarkup += '</div>';
        }
        var asset = this.thisElement.find("#asset" + id);
        this.addNotTranslatedLbl(asset, imageMarkup, isTranslated);
        this.bindAssetsFunctions();
    },

    addDocument: function (id, title, extension, size, isSingleDocument, isTranslated) {
        var documentMarkup = "";
        if (!isSingleDocument) {
            documentMarkup += '<span class="sfGrippy"></span>';
        }

        documentMarkup += '<span class="sfext sf' + extension.substring(1) + '">' + title.htmlEncode() + '</span>';
        documentMarkup += ' <span class="sfNote">(' + extension + ', ' + size + ' KB)</span>';

        if (isTranslated == false) {
            documentMarkup += String.format('<i class="sfNotTranslatedLbl">({0})</i>',
                                        this.get_notTranslatedLabel());
        }
        if (!this._isBackendReadMode) {
            documentMarkup += ' <a data-command="delete" data-id="' + id + '" class="sfDeleteItm sfInlineBlock">[DELETE]</a>';
        }

        var asset = this.thisElement.find("#asset" + id);
        asset.html(documentMarkup);
        if (isTranslated == false) {
            asset.addClass("sfNotTranslatedItem");
        }
        this.bindAssetsFunctions();
    },

    addVideo: function (id, snapshotUrl, isSingleVideo, isTranslated, videoTitle) {
        var cssWrapperClass = "sfTmbWrp";
        var imageId = id;
        if (isSingleVideo && this.get_isPreviewMode()) {
            cssWrapperClass = "";
            imageId += "Preview";
        }

        var videoMarkup = '<div class="' + cssWrapperClass + '">';

        if (!(this.get_isPreviewMode() && isSingleVideo)) {
            videoMarkup += '<img id="' + imageId + '" src="' + snapshotUrl + '" title="' + videoTitle + '" width="' + this.thumbnailWidth + '" /></div>';
        }
        else {
            videoMarkup += '<img id="' + imageId + '" src="' + snapshotUrl + '" title="' + videoTitle + '" /></div>';
        }

        if (!this._isBackendReadMode) {
            videoMarkup += '<div class="sfActionWrp sfClearfix">';
            if (!isSingleVideo) {
                videoMarkup += '<span class="sfGrippy"></span>';
            }

            videoMarkup += '<a class="sfActionMenuTrigger sfLinkBtn sfOptionsChange sfSecondaryBtn"><span class="sfLinkBtnIn">' + this._actionsLabel + '</span></a>';
            videoMarkup += '<div style="display:none;" class="outerbox inner">';
            videoMarkup += '<ul class="innerBox">';
            videoMarkup += '<li><a class="sfPlayItm">' + this._playVideoLabel + '</a></li>';
            videoMarkup += '<li><a data-command="delete" data-id="' + id + '" class="sfRemoveItm">' + this._removeLabel + '</a></li>';
            videoMarkup += '</ul>';
            videoMarkup += '</div>';
            videoMarkup += '</div>';
        }

        var asset = this.thisElement.find("#asset" + id);
        this.addNotTranslatedLbl(asset, videoMarkup, isTranslated);
        this.bindAssetsFunctions();
    },

    addNotTranslatedLbl: function(asset, markup, isTranslated)
    {
        asset.html(markup);
        if (isTranslated == false) {
            var element = String.format('<div><i class="sfNotTranslatedLbl">({0})</i></div>',
                                        this.get_notTranslatedLabel());
            asset.append(element);
            asset.addClass("sfNotTranslatedItem");
        }
    },

    bindAssetsFunctions: function () {
        var me = this;

        this.thisElement.find(".sfActionMenuTrigger").unbind();
        this.thisElement.find(".sfActionMenuTrigger").bind('click', function () {
            $(this).next().toggle();
        });

        this.thisElement.find(".sfAsset").find("[data-command=delete]").unbind();
        this.thisElement.find(".sfAsset").find("[data-command=delete]").bind('click', function () {
            me.removeItem($(this).attr("data-id"));
        });

        this.thisElement.find(".sfAsset").find("[data-command=viewImage]").unbind();
        this.thisElement.find(".sfAsset").find("[data-command=viewImage]").bind('click', function () {
            me.viewImageInOriginalSize($(this).attr("data-original-url"));
        });
    },

    viewImageInOriginalSize: function (originalUrl) {
        alert('Open in full size : ' + id);
    },

    removeItem: function (id) {
        this.thisElement.find("#asset" + id).remove();
        var deletedAsset = null;
        for (var i = this._selectedAssets.length - 1; i >= 0; i--) {
            if (this._selectedAssets[i].ContainerId == id) {
                deletedAsset = this._selectedAssets[i];
                this._selectedAssets.splice(i, 1);
            }
        }

        this._setActionVerb();
        this.raiseEvent("actionExecuted", { Name: "Remove", Asset: deletedAsset });
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    _setActionVerb: function (assetsCount) {

        switch (this._assetsWorkMode) {
            case this.MultipleImages:
            case this.MultipleDocuments:
            case this.MultipleVideos:
                return;
        }

        if (!assetsCount) {
            assetsCount = $(this.get_selectedAssetsList()).find('.sfAsset').length;
        }
        var _label = (assetsCount > 0) ? this._changeLabel : this._selectLabel;

        $(this._element).find('.sfActionVerb').each(function () {
            $(this).html(_label);
        });
    },

    _bindSortable: function (handle) {
        var options = {
            cursor: 'move',
            tolerance: 'pointer',
            helper: 'clone',
            delay: 60,
            forceHelperSize: true,
            forcePlaceholderSize: true,
            stop: jQuery.proxy(this._sortItems, this),
            handle: handle,
        };

        //Fix for jQuery sortable element offset issue in Chrome
        //Description: When window is scrolled down and an item is being dragged, the item isn't positioned under the mouse pointer.
        //TODO: This is a known bug in jQuery https://bugs.jqueryui.com/ticket/9315. This should be removed after jQuery had fixed it. 
        var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
        if (is_chrome) {
            options.sort = function (event, ui) {
                ui.helper.css({ 'top': ui.position.top + $(window).scrollTop() + 'px' });
            }
        }

        $(this.get_selectedAssetsList()).sortable(options);
    },

    _sortItems: function () {
        var itemOrder = {};
        $(this.get_selectedAssetsList()).children("li").each(function (i, e) {
            itemOrder[$(this).attr("id").substr(5)] = i;
        });

        $.each(this._selectedAssets, function () {
            this.Ordinal = itemOrder[this.ContainerId];
        });
    },

    generateGuid: function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },

    onLoadImageSuccess: function (id, isSingleImage) {
        var me = this;
        return function (result, args) {
            me.addImage(id, result.Item.ThumbnailUrl, result.Item.MediaUrl, isSingleImage);
        }
    },

    onLoadDocumentSuccess: function (id, isSingleDocument) {
        var me = this;
        return function (result, args) {
            me.addDocument(id, result.Item.Title.PersistedValue, result.Item.Extension, Math.round(result.Item.TotalSize / 1024), isSingleDocument);
        }
    },

    onLoadVideoSuccess: function (id, isSingleVideo) {
        var me = this;
        return function (result, args) {
            me.addVideo(id, result.Item.ThumbnailUrl, isSingleVideo, result.Item.Title);
        }
    },

    onError: function (id) {
        var me = this;
        return function () {
            me.thisElement.find("#asset" + id).remove();
        }
    },

    isDataItemTranslatedForCurrentCulture: function (dataItem) {
        if (this.get_isMultilingual()) {
            if (dataItem.AvailableLanguages) {
                for (var j = 0, length = dataItem.AvailableLanguages.length; j < length; j++) {
                    if (dataItem.AvailableLanguages[j] === this.get_uiCulture()) {
                        return true;
                    }
                }
                return false;
            } else {
                return true;
            }
        }
    },

    /* -------------------- events ---------------- */
    raiseEvent: function (eventName, eventArgs) {
        var handler = this.get_events().getHandler(eventName);
        if (handler) {
            if (!eventArgs) {
                eventArgs = Sys.EventArgs.Empty;
            }
            handler(this, eventArgs);
        }
    },
    add_actionExecuted: function (handler) {
        this.get_events().addHandler("actionExecuted", handler);
    },
    remove_actionExecuted: function (handler) {
        this.get_events().removeHandler("actionExecuted", handler);
    },

    /* -------------------- properties ---------------- */
    get_selectAssetsButton: function () {
        return this._selectAssetsButton;
    },
    set_selectAssetsButton: function (value) {
        this._selectAssetsButton = value;
    },
    get_selector: function () {
        return this._selector;
    },
    set_selector: function (value) {
        this._selector = value;
    },
    get_selectedAssetsList: function () {
        return this._selectedAssetsList;
    },
    set_selectedAssetsList: function (value) {
        this._selectedAssetsList = value;
    },
    get_isPreviewMode: function () {
        return this._isPreviewMode;
    },
    set_isPreviewMode: function (value) {
        this._isPreviewMode = value;
    },
    // Gets the culture to use when visualizing a content.
    get_culture: function () {
        return this._culture;
    },
    // Sets the culture to use when visualizing a content.
    set_culture: function (culture) {
        this._culture = culture;
        if (this._selector) {
            this._selector.set_culture(culture);
            if (this._selector._selectorView) {
                this._selector._selectorView.set_culture(culture);
            }
        }
    },
    // Gets the UI culture to use when visualizing a content.
    get_uiCulture: function () {
        return this._uiCulture;
    },
    // Sets the UI culture to use when visualizing a content.
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
        if (this._selector) {
            this._selector.set_uiCulture(culture);
            if (this._selector._selectorView) {
                this._selector._selectorView.set_uiCulture(culture);
            }
            if (this._selector._uploaderView) {
                this._selector._uploaderView.set_uiCulture(culture);
            }
        }
    },

    get_preSelectItemInSelector: function () {
        return this._preSelectItemInSelector;
    },
    set_preSelectItemInSelector: function (value) {
        this._preSelectItemInSelector = value;
    },

    get_isMultilingual: function () {
        return this._isMultilingual;
    },
    set_isMultilingual: function (value) {
        this._isMultilingual = value;
    },

    get_notTranslatedLabel: function () {
        return this._notTranslatedLabel;
    },
    set_notTranslatedLabel: function (value) {
        this._notTranslatedLabel = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.AssetsField.registerClass("Telerik.Sitefinity.Web.UI.Fields.AssetsField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);
