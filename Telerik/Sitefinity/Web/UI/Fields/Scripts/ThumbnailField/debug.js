/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ThumbnailField = function (element) {

    this._thumbnailImage = null;
    this._providerName = null;
    this._thumbnailSelectorDialog = null;
    this._thumbnailDialogUrl = null;
    this._commandBar = null;
    this._serviceBaseUrl = null;
    this._mediaUrl = null;

    this._useSitefinityImage = false;
    this._thumbnailSitefinityImage = null;

    //delegates
    this._cancelUpdateDelegate = null;
    this._commandBarCommandDelegate = null;
    this._dialogClosedDelegate = null;
    this._imageSelectorCloseDelegate = null;

    this._urlVersionQueryParam = "tmstmp";

    Telerik.Sitefinity.Web.UI.Fields.ThumbnailField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.ThumbnailField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ThumbnailField.callBaseMethod(this, "initialize");
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {

            if (this._commandBarCommandDelegate == null) {
                this._commandBarCommandDelegate = Function.createDelegate(this, this._handleCommandBarCommand);
            }
            this._commandBar.add_command(this._commandBarCommandDelegate);

            if (this._dialogClosedDelegate == null) {
                this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosed);
            }

            if (this._imageSelectorCloseDelegate == null) {
                this._imageSelectorCloseDelegate = Function.createDelegate(this, this._imageSelectorCloseHandler);
            }

            this._thumbnailSelectorDialog.add_close(this._dialogClosedDelegate);
            this.get_asyncImageSelector().set_customInsertDelegate(this._imageSelectorCloseDelegate);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ThumbnailField.callBaseMethod(this, "dispose");

        if (this._commandBarCommandDelegate != null) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }

        if (this._imageSelectorCloseDelegate != null) {
            this.get_asyncImageSelector().set_customInsertDelegate(null);
            delete this._imageSelectorCloseDelegate;
        }

        if (this._dialogClosedDelegate != null) {
            this._thumbnailSelectorDialog.remove_close(this._dialogClosedDelegate);
            delete this._dialogClosedDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this._isChanged = false;
        this._useSitefinityImage = false;
        this._thumbnailSitefinityImage = null;

        Telerik.Sitefinity.Web.UI.Fields.ThumbnailField.callBaseMethod(this, "reset");
    },

    saveChanges: function (dataItem, successCallback, failureCallback, caller) {
        var requestData;
        if (this._useSitefinityImage && this._thumbnailSitefinityImage) {
            requestData = "contentId=" + dataItem.Item.Id +
                "&thumbnailImageId=" + this._thumbnailSitefinityImage.Id +
                "&useSFImage=" + true +
                "&provider=" + caller.get_providerName();
        }
        else {
            requestData = "contentId=" + dataItem.Item.Id +
                "&data=" + $get(this._thumbnailImage).src +
                "&provider=" + caller.get_providerName();
        }

        var uiCulture = caller._uiCulture;
        jQuery.ajax({
            type: "POST",
            url: this._serviceBaseUrl,
            data: requestData,
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            async: false,
            success: successCallback,
            error: failureCallback,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("IsBackendRequest", true);
                if (uiCulture) {
                    xhr.setRequestHeader("SF_UI_CULTURE", uiCulture);
                }
            }
        });
    },

    isChanged: function () {
        return this._isChanged;
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handleCommandBarCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case 'editThumbnail':
                {
                    var mediaUrl;
                    if (this._mediaUrl) {
                        mediaUrl = this._mediaUrl;
                    }
                    else if (this._dataItem) {
                        mediaUrl = this._dataItem.MediaUrl;
                    }
                    else {
                        var thumbnailUrl = this.get_value().toString();
                        mediaUrl = thumbnailUrl;
                        var idx = mediaUrl.indexOf(".tmb");
                        if (idx > -1) {
                            mediaUrl = mediaUrl.substr(0, idx) + mediaUrl.substr(idx + 4, mediaUrl.length - 1);
                        }
                    }
                    var url = Telerik.Sitefinity.setUrlParameter(this._thumbnailDialogUrl, "mediaUrl", escape(mediaUrl));
                    this._thumbnailSelectorDialog.SetUrl(url);
                    this._thumbnailSelectorDialog.show();
                    Telerik.Sitefinity.centerWindowHorizontally(this._thumbnailSelectorDialog);
                }
                break;
        }
    },

    /* -------------------- private methods ----------- */
    _dialogClosed: function (sender, args) {
        if (args && args.get_argument()) {
            var data = args.get_argument().Data;
            if (data != null) {
                var image = $get(this._thumbnailImage);
                image.src = data;
                $(image).show();

                this._useSitefinityImage = false;
                this._isChanged = true;
            }
        }
        else {
            this._isChanged = false;
        }
    },

    _imageSelectorCloseHandler: function (item) {
        if (item) {
            var image = $get(this._thumbnailImage);
            image.src = item.MediaUrl;
            $(image).show();

            this._useSitefinityImage = true;
            this._thumbnailSitefinityImage = item;
            this._isChanged = true;
        }
        else {
            this._isChanged = false;
        }

        this.get_asyncImageSelector().close();
    },

    _getAppender: function (value) {
        var appender = "?";
        if (value.indexOf('?') > 0) { appender = "&"; }
        return appender;
    },

    _appendTimeStampParam: function (value) {
        var appender = this._getAppender(value);
        value += appender + this._urlVersionQueryParam + "=" + new Date().getTime();
        return value;
    },

    /* -------------------- properties ---------------- */

    //Sets the value(thumbnial url) of the thumbnail control.
    set_value: function (value) {
        if (value) {
            var thumbnail = $get(this._thumbnailImage);            

            thumbnail.onload = function () {
                if (this.width == 0) {
                    $(this).hide();
                }
                else {
                    $(this).show();
                }
            }

            thumbnail.src = this._appendTimeStampParam(value);

            this.raisePropertyChanged("value");
            this._valueChangedHandler();
        }
    },

    // Gets the element of the thumbnail image
    get_thumbnailImage: function () {
        return this._thumbnailImage;
    },

    // Sets the element of the thumbnail image
    set_thumbnailImage: function (value) {
        this._thumbnailImage = value;
    },

    get_thumbnailSelectorDialog: function () {
        return this._thumbnailSelectorDialog;
    },
    set_thumbnailSelectorDialog: function (value) {
        this._thumbnailSelectorDialog = value;
    },

    get_thumbnailDialogUrl: function () {
        return this._thumbnailDialogUrl;
    },
    set_thumbnailDialogUrl: function (value) {
        this._thumbnailDialogUrl = value;
    },

    // Gets the reference to the command bar.
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    }
};
Telerik.Sitefinity.Web.UI.Fields.ThumbnailField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ThumbnailField", Telerik.Sitefinity.Web.UI.Fields.ImageField, Telerik.Sitefinity.Web.UI.ISelfExecutableField);
