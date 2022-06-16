/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.8.8-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.MultiImageField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.MultiImageField.initializeBase(this, [element]);
    this._element = element;
    this._imagesHolder = null;
    this._viewOriginalSizeButtonElement = null;
    this._originalSizeValue = null;

    this._fieldMode = null;
    this._viewPanelID = null;
    this._uploadPanelID = null;
    this._replaceImageButtonElement = null;
    this._cancelUploadButtonElement = null;

    this._replaceImageDelegate = null;
    this._cancelUploadDelegate = null; // This is the delegate used InputField mode

    this._uploadDialog = null;
    this._imagePropertiesDialog = null;

    this._uploadServiceUrl = null;
    this._contentType = null;
    this._providerName = null;

    this._cancelDelegate = null; // This is the delegate used in Dialog mode

    this._imageBinder = null;

    this._firstItemText = null;
    this._scalableText = null;

    this._selectedImageKey = null;
    this._selectedImageElement = null;

    this._asyncImageSelector = null;
    this._imageProperties = null;
    this._selectedImageItem = null;
    this._dataFieldType = null;
    this._dataItem = null;
    this._contentLink = null;
    this._itemId = Telerik.Sitefinity.getEmptyGuid();
    this._imageEditorDialogUrl = null;
};

Telerik.Sitefinity.Web.UI.Fields.MultiImageField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.MultiImageField.callBaseMethod(this, "initialize");

        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
            // subscribe to the click event of the button
            if (this._viewOriginalSizeButtonElement) {
                $addHandler(this._viewOriginalSizeButtonElement, "click", this._commandDelegate);
            }
        }

        if (this._replaceImageButtonElement) {
            this._replaceImageDelegate = Function.createDelegate(this, this._replaceImageHandler);
            $addHandler(this._replaceImageButtonElement, "click", this._replaceImageDelegate);
        }

        if (this._cancelUploadButtonElement) {
            this._cancelUploadDelegate = Function.createDelegate(this, this._cancelUploadHandler);
            $addHandler(this._cancelUploadButtonElement, "click", this._cancelUploadDelegate);
        }

        this._uploadDialog = jQuery(this._asyncImageSelector.get_element()).dialog({
            autoOpen: false,
            modal: true,
            width: 930,
            height: "auto",
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfSelectorDialog sfZIndexL"
            }
        });

        this._asyncImageSelectorInsertDelegate = Function.createDelegate(this, this._asyncImageSelectorInsertHandler);
        this._asyncImageSelector.set_customInsertDelegate(this._asyncImageSelectorInsertDelegate);

        this._imagePropertiesDialog = jQuery(this._imageProperties.get_element()).dialog({
            autoOpen: false,
            modal: true,
            width: 930,
            height: "auto",
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfSelectorDialog sfZIndexL"
            }
        });

        this._editImagePropertiesDelegate = Function.createDelegate(this, this._editImagePropertiesHandler);
        this._imageProperties.set_saveImagePropertiesDelegate(this._editImagePropertiesDelegate);

        var imageList = $("ul.sfProductImagesHolder");
        imageList.sortable({
            tolerance: 'pointer',
            helper: 'clone',
            delay: 60,
            forceHelperSize: true,
            forcePlaceholderSize: true,
            stop: jQuery.proxy(this.sortImages, this),
            handle: '.sfGrippy'
        });
    },

    sortImages: function () {
        var imageList = $("ul.sfProductImagesHolder");
        var imageOrder = {};
        imageList.find("ul.innerBox a.sfMakePrimary").removeClass('sfDisabled').first().addClass('sfDisabled');
        imageList.children("li").each(function (i, e) {
            imageOrder[$(this).data("image-id")] = i;
        });

        $.each(this._dataItem.Images, function () {
            this.Ordinal = imageOrder[this.Id];
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.MultiImageField.callBaseMethod(this, "dispose");

        if (this._commandDelegate) {
            if (this._viewOriginalSizeButtonElement) {
                $removeHandler(this._viewOriginalSizeButtonElement, "click", this._commandDelegate);
            }
            delete this._commandDelegate;
        }

        if (this._replaceImageDelegate) {
            if (this._replaceImageButtonElement) {
                $removeHandler(this._replaceImageButtonElement, "click", this._replaceImageDelegate);
            }
            delete this._replaceImageDelegate;
        }

        if (this._cancelUploadDelegate) {
            if (this._cancelUploadButtonElement) {
                $removeHandler(this._cancelUploadButtonElement, "click", this._cancelUploadDelegate);
            }
            delete this._cancelUploadDelegate;
        }

        this._imagesHolder = null;
        this._viewOriginalSizeButtonElement = null;
        this._replaceImageButtonElement = null;
        this._cancelUploadButtonElement = null;
        this._ajaxUpload = null;
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        var imagePlaceholder = jQuery('ul.sfProductImagesHolder');
        imagePlaceholder.html("");

        Telerik.Sitefinity.Web.UI.Fields.MultiImageField.callBaseMethod(this, "reset");
    },

    /* -------------------- event handlers ------------ */

    _commandHandler: function (e) {
        if (this._originalSizeValue) {
            window.open(this._originalSizeValue, "_blank");
        }
    },

    _cancelUploadHandler: function (e) {
        jQuery("#" + this._viewPanelID).show();
        jQuery("#" + this._uploadPanelID).hide();
    },

    _replaceImageHandler: function (e) {
        this._uploadDialog.dialog("open");
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(this._uploadDialog).parent().css({ "top": scrollTop });
    },

    _imagePropertiesClickedHandler: function (e) {
        this._imagePropertiesDialog.dialog("open");
        this._imageProperties.get_clientManager().set_uiCulture(this.get_uiCulture());
        this._imageProperties.setImageProperties(e);
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(this._imagePropertiesDialog).parent().css({ "top": scrollTop });
    },

    _asyncImageSelectorInsertHandler: function (selectedItem) {
        if (jQuery.grep(this.get_dataItem().Images, function (item) { return item.Id == selectedItem.Id; }).length) {
            return;
        }
        if (selectedItem) {
            jQuery('.sfSingleImageWrp').show();
            var fileName = selectedItem.MediaUrl.substring(selectedItem.MediaUrl.lastIndexOf('/') + 1);
            var fileSize = selectedItem.TotalSize + ' kb';
            var imageDimensions = (selectedItem.IsVectorGraphics) ? this.get_scalableText() : selectedItem.Width + " x " + selectedItem.Height + "px";
            var realDate = (typeof (selectedItem.DateCreated) === 'string')
                ? new Date(selectedItem.DateCreated.match(/\d+/)[0] * 1)
                : selectedItem.DateCreated;
            var uploadedDate = realDate.toLocaleDateString();
            var thumbnailWidth = Math.round(this._calculateThumbnailWidth(selectedItem.Width, selectedItem.Height));
            var thumbnailHeight = Math.round(this._calculateThumbnailHeight(selectedItem.Width, selectedItem.Height));
            var image = {
                Id: selectedItem.Id,
                Url: selectedItem.MediaUrl,
                AlternativeText: { PersistedValue: selectedItem.AlternativeText, Value: selectedItem.AlternativeText },
                FileName: fileName,
                FileSize: fileSize,
                UploadedDate: uploadedDate,
                Album: selectedItem.LibraryTitle,
                AlbumId: selectedItem.ParentId,
                Title: selectedItem.Title,
                ImageDimensions: imageDimensions,
                ThumbnailWidth: thumbnailWidth,
                ThumbnailHeight: thumbnailHeight,
                Width: selectedItem.Width,
                Height: selectedItem.Height,
                ThumbnailUrl: selectedItem.ThumbnailUrl
            };
            this._showImage(image);

            this.get_dataItem().Images.push(image);
            this._uploadDialog.dialog("close");

            // Resets the ordinals on the product's images
            this.sortImages();
        }
    },

    _editImagePropertiesHandler: function (selectedItem) {
        this._imagePropertiesDialog.dialog("close");
        if (selectedItem) {
            this._imagePropertiesDialog.dialog("close");
        }
    },

    /* -------------------- private methods ----------- */

    _calculateThumbnailWidth: function (width, height) {
        if (width <= this.get_maxThumbnailWidth() && height <= this.get_maxThumbnailHeight()) {
            return width;
        }

        if (width > height) {
            return this.get_maxThumbnailWidth();
        }

        return width * this.get_maxThumbnailWidth() / height;
    },

    _calculateThumbnailHeight: function (width, height) {
        if (width <= this.get_maxThumbnailWidth() && height <= this.get_maxThumbnailHeight()) {
            return height;
        }

        if (height > width) {
            return this.get_maxThumbnailHeight();
        }

        return height * this.get_maxThumbnailHeight() / width;
    },

    _showImage: function (imageObject) {
        var imageUrl = imageObject.ThumbnailUrl;
        var image = this;
        var img = jQuery("<img />", { src: imageObject.Url });

        var imagePlaceholder = jQuery('ul.sfProductImagesHolder').addClass('actionsMenu').addClass('clickMenu');
        var imageWidth = imageObject.ThumbnailWidth > 0 ? imageObject.ThumbnailWidth * 95 / 85 : 85;
        var imageHeight = imageObject.ThumbnailHeight > 0 ? imageObject.ThumbnailHeight * 95 / 85 : 85;
        var format = '<li class="sfSingleImageWrp sfInlineBlock" data-image-id="{2}">\
            <span class="sfPrimaryProductImageMarker">Primary</span>\
            <span class="sfImgWrp"><img src="{0}" width="{3}" height="{4}" /></span>\
            <div class="sfActionWrp">\
            <span class="sfGrippy"></span>\
            <div class="sfActionWrp"><a class="sfLinkBtn sfOptionsChange sfSecondaryBtn"><span class="sfLinkBtnIn">{1}</span></a></div>\
            </li>';
        var actionButtonHtml = String.format(format, imageUrl, this.get_actionsMenuText(), imageObject.Id, imageWidth, imageHeight);
        imagePlaceholder.append(actionButtonHtml);

        //imagePlaceholder.find('img');

        var item = function (text, className) { return jQuery("<a />", { 'text': text, 'class': className }); }
        var listItems = [
            item(this.get_viewOriginalText(), 'sfViewOrg'),
            item(this.get_removeText(), 'sfRemoveItm'),
            item(this.get_editPropertiesText(), 'sfEditItm'),
            item(this.get_setAsPrimaryImageText(), 'sfMakePrimary')
        ];

        listItems[0].click(function () {
            var options = String.format('toolbar=0,status=0,menubar=0,location=0,directories=0,width={0},height={1}', imageObject.Width, imageObject.Height);
            window.open(img.attr("src"), 'ImageWindow', options);
        });

        listItems[1].click(function () {
            image._remove_dataItem(imageObject);
            var imageFrame = jQuery(this).closest("li.sfSingleImageWrp");
            imageFrame.fadeOut('slow', function () { imageFrame.remove(); image.sortImages(); });
        });

        listItems[2].click(function () {
            image._imagePropertiesClickedHandler(imageObject);
        });

        listItems[3].click(function () {
            var imageFrame = jQuery(this).closest("li.sfSingleImageWrp");
            imageFrame.prependTo(imagePlaceholder);
            var imageList = $("ul.sfProductImagesHolder");
            image.sortImages();
        });

        var actionsList = jQuery('<ul />', { "class": "innerBox" });

        jQuery.each(listItems, function () { var self = $(this); actionsList.append(jQuery("<li />", { 'class': self.attr("class") }).append(self)); });

        var actionsMenuButton = jQuery("a:last", imagePlaceholder);

        var actionsDiv = jQuery('<div />', { "class": "outerbox inner" }).css({
            "position": "absolute !important;"
        }).append(actionsList);

        var openMenu = function () {
            jQuery(this).addClass("open");
            jQuery("body").bind("click.productImageActionsMenu", function () { actionsMenuButton.click(); });
            actionsDiv.fadeIn().css("position", "absolute");
        };

        var closeMenu = function () {
            jQuery(this).removeClass("open");
            jQuery("body").unbind(".productImageActionsMenu");
            actionsDiv.fadeOut();
        };

        actionsMenuButton.after(actionsDiv).toggle(openMenu, closeMenu);
        imagePlaceholder.find("ul.innerBox a.sfMakePrimary").first().addClass("sfDisabled");
    },

    _remove_dataItem: function (dataItem) {
        var dataItems = this.get_dataItem().Images;
        var i = 0;
        for (; i < dataItems.length; i++) {
            if (dataItems[i].Id === dataItem.Id) {
                dataItems.splice(i, 1);
                return;
            }
        }
    },

    /* -------------------- properties ---------------- */

    get_maxThumbnailWidth: function () {
        if (!this._maxThumbnailWidth) {
            this.set_maxThumbnailWidth(85);
        }
        return this._maxThumbnailWidth;
    },

    set_maxThumbnailWidth: function (width) {
        this._maxThumbnailWidth = width;
    },

    get_maxThumbnailHeight: function () {
        if (!this._maxThumbnailHeight) {
            this.set_maxThumbnailHeight(85);
        }
        return this._maxThumbnailHeight;
    },

    set_maxThumbnailHeight: function (height) {
        return this._maxThumbnailHeight = height;
    },

    // Gets the reference to the DOM element used to display the view original size button.
    get_viewOriginalSizeButtonElement: function () {
        return this._viewOriginalSizeButtonElement;
    },
    // Sets the reference to the DOM element used to display the view original size button.
    set_viewOriginalSizeButtonElement: function (value) {
        this._viewOriginalSizeButtonElement = value;
    },

    get_viewPanelID: function () {
        return this._viewPanelID;
    },
    set_viewPanelID: function (value) {
        this._viewPanelID = value;
    },

    get_uploadPanelID: function () {
        return this._uploadPanelID;
    },
    set_uploadPanelID: function (value) {
        this._uploadPanelID = value;
    },

    // Gets the reference to the DOM element used to display the Replace image button.
    get_replaceImageButtonElement: function () {
        return this._replaceImageButtonElement;
    },
    // Sets the reference to the DOM element used to display the Replace image button.
    set_replaceImageButtonElement: function (value) {
        this._replaceImageButtonElement = value;
    },

    // Gets the reference to the DOM element used to display the Cancel upload button.
    get_cancelUploadButtonElement: function () {
        return this._cancelUploadButtonElement;
    },
    // Sets the reference to the DOM element used to display the Cancel upload button.
    set_cancelUploadButtonElement: function (value) {
        this._cancelUploadButtonElement = value;
    },

    get_firstItemText: function () {
        return this._firstItemText;
    },
    set_firstItemText: function (value) {
        this._firstItemText = value;
    },

    get_actionsMenuText: function () {
        return this._actionsMenuText;
    },
    set_actionsMenuText: function (value) {
        this._actionsMenuText = value;
    },

    get_removeText: function () {
        return this._removeText;
    },
    set_removeText: function (value) {
        this._removeText = value;
    },

    get_viewOriginalText: function () {
        return this._viewOriginalText;
    },
    set_viewOriginalText: function (value) {
        this._viewOriginalText = value;
    },

    get_editPropertiesText: function () {
        return this._editPropertiesText;
    },
    set_editPropertiesText: function (value) {
        this._editPropertiesText = value;
    },

    get_setAsPrimaryImageText: function () {
        return this._setAsPrimaryImageText;
    },
    set_setAsPrimaryImageText: function (value) {
        this._setAsPrimaryImageText = value;
    },

    get_asyncImageSelector: function () {
        return this._asyncImageSelector;
    },
    set_asyncImageSelector: function (value) {
        this._asyncImageSelector = value;
    },

    get_imageProperties: function () {
        return this._imageProperties;
    },
    set_imageProperties: function (value) {
        this._imageProperties = value;
    },

    get_uiCulture: function () {
        return this._uiCulture;
    },
    set_uiCulture: function (value) {
        this._uiCulture = value;
    },

    get_scalableText: function () {
        return this._scalableText;
    },
    set_scalableText: function (value) {
        this._scalableText = value;
    },

    get_dataItem: function () {
        return this._dataItem;
    },
    set_dataItem: function (value) {
        //reset the control before setting it as save as draft calls causes set_dataItem to be called and if not reset, items will appear twice
        this.reset();

        var detailFormView = null;
        try {
            detailFormView = dialogBase.get_radWindow().DetailFormView;
        }
        catch (ex) { }

        if (detailFormView !== null) {
            this.set_uiCulture(detailFormView.get_binder().get_uiCulture());
        }

        this._dataItem = value;
        if (this._dataItem && this._dataItem.Images) {
            for (var i = 0; i < this._dataItem.Images.length; i++) {
                this._showImage(this._dataItem.Images[i]);
            }
        }
    }
};
Telerik.Sitefinity.Web.UI.Fields.MultiImageField.registerClass("Telerik.Sitefinity.Web.UI.Fields.MultiImageField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
