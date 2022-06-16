Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos");

mediaPlayerControl = null;

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl.initializeBase(this, [element]);

    this._title = null;
    this._description = null;
    this._url = null;
    this._autoPlay = null;
    this._domain = "http://" + document.domain;
    this._playerContainer = null;
    this._serviceBaseUrl = null;
    this._thumbnailSelector = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        mediaPlayerControl = this;
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    setMediaParams: function (data) {
        this.set_url(data.url);
        this.set_title(data.title);
        this.set_description(data.description);
        this.set_autoPlay(data.autoPlay);
        if (this.get_thumbnailSelector()) {
            this.get_thumbnailSelector().set_videoAutoPlay(this.get_autoPlay());
            this.get_thumbnailSelector().set_videoUrl(this.get_url());
            this.get_thumbnailSelector().loadVideo();
        }
    },

    thumbnailSelectionCanceled: function (sender, args) {
        this._thumbnailData = null;
        this._expandCollapse();

        this._thumbnailSelectionCanceledHandler();
    },

    getThumbnailData: function () {
        var data = null;
        if (this.get_thumbnailSelector()) {
            data = this.get_thumbnailSelector().get_value();
        }
        return data;
    },

    uploadThumbnail: function (handlerUrl, itemData) {
       if (this.get_thumbnailSelector()) {
            jQuery.ajax({
                type: "POST",
                url: handlerUrl,
                data: "contentId=" + itemData.Id + "&data=" + this.get_thumbnailSelector().get_value() + "&provider=" + itemData.ProviderName,
                dataType: "json",
                contentType: "application/x-www-form-urlencoded",
                async: false,
                success: function () {
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        }
    },

    // This function allows other objects to subscribe to the 'thumbnailSelectionCanceled' event.
    add_thumbnailSelectionCanceled: function (handler) {
        this.get_events().addHandler('thumbnailSelectionCanceled', handler);
    },

    // This function allows other objects to unsubscribe to the 'thumbnailSelectionCanceled' event.
    remove_thumbnailSelectionCanceled: function (handler) {
        this.get_events().removeHandler('thumbnailSelectionCanceled', handler);
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    // This function will raise 'thumbnailSelectionCanceled' event.
    _thumbnailSelectionCanceledHandler: function () {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('thumbnailSelectionCanceled');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },

    /* -------------------- private methods ----------- */
    _clearMediaItem: function () {
        this._title = null;
        this._description = null;
        this._url = null;

        if (this.get_thumbnailSelector()) {
            this.get_thumbnailSelector().resetPlayer();
        }
    },

    _expandCollapse: function (toExpand) {
        var player = $(this._playerContainer);
        if (toExpand) {
            player
            .removeClass('sfvideoWrp')
            .addClass('sftmbVideoWrp')
        }
        else {
            player
            .removeClass('sftmbVideoWrp')
            .addClass('sfvideoWrp')
        }
    },

    /* -------------------- properties ---------------- */

    get_title: function () {
        return this._title;
    },
    set_title: function (value) {
        this._title = value;
    },
    get_description: function () {
        return this._description;
    },
    set_description: function (value) {
        this._description = value;
    },
    get_url: function () {
        return this._url;
    },
    set_url: function (value) {
        var videoUrl = value;
        if (videoUrl != null) {
            //if url is relative it is converted to absolute
            if (value.indexOf('http://') == 0 || value.indexOf('https://') == 0 || value.indexOf('file://') == 0)
                videoUrl = value;
            else
                videoUrl = this._domain + value;
        }
        this._url = videoUrl;
    },
    get_autoPlay: function () {
        return this._autoPlay;
    },
    set_autoPlay: function (value) {
        if (value == null)
            value = "false";
        this._autoPlay = value;
    },

    // gets service Url
    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },

    // sets service Url
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },

    get_thumbnailData: function () {
        return this._thumbnailData;
    },
    set_thumbnailData: function (value) {
        this._thumbnailData = value;
    },

    // Gets the reference to the control which serves as a container for the player control.
    get_playerContainer: function () { return this._playerContainer; },

    // Sets the reference to the control which serves as a container for the player control.
    set_playerContainer: function (value) { this._playerContainer = value; },

    // Gets or sets the HTML5 thumbnail selector.
    get_thumbnailSelector: function () {
        return this._thumbnailSelector;
    },
    set_thumbnailSelector: function (value) {
        this._thumbnailSelector = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailMediaPlayerControl", Sys.UI.Control);