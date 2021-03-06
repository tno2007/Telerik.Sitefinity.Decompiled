Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector.initializeBase(this, [element]);

    this._mediaPlayer = null;
    this._mediaPlayerError = null;
    this._currentFrameButton = null;
    this._selectedThumbnailContainer = null;

    this._value;
    this._videoUrl = "";
    this._videoAutoPlay = true;
    this._thumbnailWidth = null;
    this._thumbnailHeight = null;
    this._thumbnailStartX = null;
    this._thumbnailStartY = null;
    this._thumbnailFormat = "image/jpeg";
    this._urlVersionQueryParam = "sfvrsn";

    this._currentFrameClickedDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector.prototype =
{
    /* --------------------------------- set up and tear down --------------------------- */
    initialize: function () {
        if (this.get_currentFrameButton()) {
            this._currentFrameClickedDelegate = Function.createDelegate(this, this._currentFrameClicked);
            $addHandler(this.get_currentFrameButton(), "click", this._currentFrameClickedDelegate);
        }

        var player = this.get_mediaPlayer();
        if (player && player.toolbar && player.toolbar._jhdButton && player.toolbar._jprogressRail) {
            // Hide HD button since it is not used.
            var hdButton = player.toolbar._jhdButton;

            var hdButtonLeftMargin = parseInt(hdButton.css("margin-left"));
            if (isNaN(hdButtonLeftMargin))
                hdButtonLeftMargin = 0;

            var hdButtonRightMargin = parseInt(hdButton.css("margin-right"));
            if (isNaN(hdButtonRightMargin))
                hdButtonRightMargin = 0;

            var hdButtonWidth = hdButton.width() + hdButtonLeftMargin + hdButtonRightMargin;
            hdButton.hide();

            var progressRail = player.toolbar._jprogressRail.parent();
            var progressRightMargin = parseInt(progressRail.css("margin-right"));
            progressRail.css("margin-right", (progressRightMargin - hdButtonWidth) + "px");

            // IE8 fix for volume button when HD button is hidden.
            var volumeButton = player.toolbar._jvcButton.parent();
            var volumeRightMargin = parseInt(volumeButton.css("right"));
            if (isNaN(volumeRightMargin))
                volumeRightMargin = 0;
            if (volumeRightMargin > hdButtonWidth)
                volumeButton.css("right", (volumeRightMargin - hdButtonWidth) + "px");
        }

        if (this.get_videoUrl()) {
            this.loadVideo();
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._currentFrameClickedDelegate) {
            $removeHandler(this.get_currentFrameButton(), "click", this._currentFrameClickedDelegate);
            delete this._currentFrameClickedDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    loadVideo: function () {
        if (this.get_videoUrl()) {
            var that = this;
            jQuery.ajax({
                type: "HEAD",
                url: this.get_videoUrl(),
                async: true,
                success: function (message, text, response) {
                    that._loadVideo(response.getResponseHeader('Content-Type'));
                },
                error: function (jqXHR, textStatus, errorThrown) {
                }
            });
        }
    },

    resetPlayer: function () {
        var player = this.get_mediaPlayer();
        if (player) {
            try {
                player.seekTo(0);
                if (player.isPlaying()) {
                    player.stop();
                }
            } catch (e) {
            }
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _currentFrameClicked: function () {
        var thumbnail = this.get_mediaPlayer().getSnapshotDataUrl(this._thumbnailWidth, this._thumbnailHeight, this._thumbnailStartX, this._thumbnailStartY, this._thumbnailFormat);
        this._loadThumbnailPreview(thumbnail);
    },

    /* --------------------------------- private methods --------------------------------- */

    _loadVideo: function (mimeType) {
        var player = this.get_mediaPlayer();
        if (player && player.currentPlayer && player.currentFile) {
            var file = player.currentFile;
            if (file.path !== this.get_videoUrl()) {
                file.path = this.get_videoUrl();
                file.mimeType = mimeType
                file.type = "video";
                file.options = {
                    autoPlay: false,
                    duration: 100,
                    fsActive: false,
                    hdActive: false,
                    muted: false,
                    poster: "",
                    startTime: 0,
                    startVolume: 50,
                    sources: []
                }
                var currentPlayer = player.currentPlayer;
                $telerik.$(currentPlayer.media).remove();
                currentPlayer.dispose();
                player.dispose();
                delete player.supportedPlayers.Flash;
                if (player.toolbar._jprogressTooltip)
                    player.toolbar._jprogressTooltip.remove();
                player.initialize();                
            }
            
            if (player.currentPlayer.media && 
                player.currentPlayer.media.tagName &&
                player.currentPlayer.media.tagName.toLowerCase() === "video"){

                // Set crossOrigin attribute in order to be able to take snapshot from videos played from different origin
                player.currentPlayer.media.crossOrigin = "anonymous";
            }

            var html5Player = player.currentPlayer.get_playerType() === Telerik.Web.UI.MediaPlayerType.HTML5 ? player.supportedPlayers.HTML5 : null;
            var canPlay = !!html5Player;

            if (canPlay) {
                jQuery(player.get_element()).parent().show();
                jQuery(this.get_currentFrameButton()).show();
                jQuery(this.get_mediaPlayerError()).hide();
            }
            else {
                jQuery(player.get_element()).parent().hide();
                jQuery(this.get_currentFrameButton()).hide();
                jQuery(this.get_mediaPlayerError()).show();
            }

            dialogBase.resizeToContent();
        }
    },

    _loadThumbnailPreview: function (url) {
        var that = this;
        var img = jQuery('<li><span class="sfThumbnailWrp"><img alt="thumbnail"/><span class="imgSelect"></span></span></li>');
        img.find("img").attr("src", url);
        img.click(function (args) {
            that._selectThumbnailItem(this, args);
        });
        img.appendTo(this.get_selectedThumbnailContainer());
        var thumbnailContainer = jQuery(this.get_selectedThumbnailContainer());
        if (thumbnailContainer.children().length === 1) {
            img.addClass("sfSel");
            this.set_value(url);
            thumbnailContainer.show();
            dialogBase.resizeToContent();
        }
    },

    _selectThumbnailItem: function (sender, args) {
        jQuery(this.get_selectedThumbnailContainer()).children(".sfSel").removeClass("sfSel");
        this.set_value(jQuery(sender).addClass("sfSel").find("img").attr("src"));
    },

    // Chrome does not support 2 <video> tags with the same URL.
    _appendRnd: function (value) {
        var qIndex = value.lastIndexOf("?");
        if (qIndex > 0) {
            var query = value.substring(qIndex + 1);
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(query);
            if (queryString.contains(this._urlVersionQueryParam)) {
                queryString.set(this._urlVersionQueryParam, Math.random());
                return (value.substring(0, qIndex)) + queryString.toString(true);
            }
        }
        value = qIndex > 0 ? value + "&" : value + "?";
        return value + this._urlVersionQueryParam + "=" + Math.random();
    },

    /* --------------------------------- properties -------------------------------------- */
    get_value: function () {
        return this._value;
    },

    set_value: function (value) {
        this._value = value;
    },

    get_videoAutoPlay: function () {
        return this._videoAutoPlay;
    },
    set_videoAutoPlay: function (value) {
        this._videoAutoPlay = value && value !== "false" ? true : false;
    },

    get_videoUrl: function () {
        return this._videoUrl;
    },
    set_videoUrl: function (value) {
        this._videoUrl = value ? this._appendRnd(value) : value;
    },

    get_mediaPlayer: function () {
        return this._mediaPlayer;
    },
    set_mediaPlayer: function (value) {
        this._mediaPlayer = value;
    },

    get_mediaPlayerError: function () {
        return this._mediaPlayerError;
    },
    set_mediaPlayerError: function (value) {
        this._mediaPlayerError = value;
    },

    get_currentFrameButton: function () {
        return this._currentFrameButton;
    },
    set_currentFrameButton: function (value) {
        this._currentFrameButton = value;
    },

    get_selectedThumbnailContainer: function () {
        return this._selectedThumbnailContainer;
    },
    set_selectedThumbnailContainer: function (value) {
        this._selectedThumbnailContainer = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.ThumbnailSelector", Sys.UI.Control);
