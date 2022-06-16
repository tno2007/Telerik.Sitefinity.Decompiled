﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos");

mediaPlayerControl = null;

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl.initializeBase(this, [element]);

    this._title = null;
    this._description = null;
    this._url = null;
    this._mimeType = null;
    this._autoPlay = null;
    this._startVolume = null;
    this._startTime = null;
    this._fullScreen = null;
    this._isFrontend = null;
    this._domain = "http://" + document.domain;
    this._playerContainer = null;
    this._width = null;
    this._height = null;
    this._mediaPlayer = null;
    this._mediaPlayerError = null;
    this._mediaPlayerPlayedDelegate = null;
    this._useYouTubePlayer = null;
    this._useYouTubePlaylist = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
       
		var player = this.get_mediaPlayer();
		this._mediaPlayerPlayedDelegate = Function.createDelegate(this, this._mediaPlayerPlayedHandler)
		player.add_play(this._mediaPlayerPlayedDelegate);

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
		this._playMediaItem();
       
        mediaPlayerControl = this;
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._mediaPlayerPlayedDelegate) {
            delete this._mediaPlayerPlayedDelegate;
        }
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    setMediaParams: function (data) {
        this.set_url(data.url);
        this.set_title(data.title);
        this.set_description(data.description);
        this.set_autoPlay(data.autoPlay);
        this.set_startVolume(data.startVolume);
        this.set_startTime(data.startTime);
        this.set_fullScreen(data.fullScreen);
        this.set_isFrontend(data.isFrontend);
        this.set_mimeType(null);
        this._playMediaItem();
    },

    //Stops the player
    stopMedia: function () {
        this._clearMediaItem();
    },
    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _mediaPlayerPlayedHandler: function (sender, args) {
        var sentenceObject = this._getSentenceObject();

        if (window.DataIntelligenceSubmitScript && sentenceObject) {
            DataIntelligenceSubmitScript._client.sentenceClient.writeSentence({
                predicate: "Play video",
                object: sentenceObject,
                objectMetadata: [
											{
											    'K': 'PageTitle',
											    'V': document.title
											},
                                            {
                                                'K': 'PageUrl',
                                                'V': location.href
                                            }
                ]
            });
        }
    },

    /* -------------------- private methods ----------- */

    _getSentenceObject: function () {
        try {
            var player = this.get_mediaPlayer();
            var file = player.currentFile;

            return file.path;
        } catch (e) {
            return null;
        }           
    },

    _playMediaItem: function () {
        if (this.get_url()) {
            if (this.get_useYouTubePlayer() || this.get_mimeType()) {
                this._playMediaItemHtml5(this.get_mimeType());
            }
            else {
                var that = this;
                jQuery.ajax({
                    type: "HEAD",
                    url: this.get_url(),
                    async: true,
                    success: function (message, text, response) {
                        that.set_mimeType(response.getResponseHeader('Content-Type'));
                        that._playMediaItemHtml5();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                    }
                });
            }
        }
    },

    _playMediaItemHtml5: function () {
        jQuery(".rmpButtSet:first").hide();

        var player = this.get_mediaPlayer();

        var file = player.currentFile;
        if (file.path !== this.get_url()) {
            file.path = this.get_url();
            file.mimeType = this.get_mimeType();
            file.type = "video";

            var options = {
                autoPlay: false,
                duration: 100,
                fsActive: false,
                hdActive: false,
                muted: false,
                poster: "",
                sources: [],
                startTime: this.get_startTime(),
                startVolume: this.get_startVolume()
            };

            if (this.get_isFrontend()) {
                options.autoPlay = this.get_autoPlay();

                if (this.get_fullScreen()) {
                    player.enterFullScreen();
                }
            }

            file.options = options;

            var currentPlayer = player.currentPlayer;
            $telerik.$(currentPlayer.media).remove();
            currentPlayer.dispose();
            player.dispose();
            player._joverlay.off();
            delete player.supportedPlayers.Flash;
            if (player.toolbar._jprogressTooltip)
                player.toolbar._jprogressTooltip.remove();
            if (player.toolbar._jprogressBuffer)
                player.toolbar._jprogressBuffer.remove();

            player.initialize();
        }

        var flashPlayer = player.currentPlayer.get_playerType() === Telerik.Web.UI.MediaPlayerType.Flash ? player.supportedPlayers.Flash : null;
        var canPlay = !flashPlayer || flashPlayer.canPlay(null, file);

        var errorMessage = jQuery(this.get_mediaPlayerError());
        if (canPlay) {
            errorMessage.hide();
        }
        else {
            file.path = null;
            errorMessage.appendTo(player.get_element()).show();
        }
    },

    _clearMediaItem: function () {
        this._title = null;
        this._description = null;
        this._url = null;
      
        try {
            var player = this.get_mediaPlayer()
            player.seekTo(0);
            if (player.isPlaying())
                player.stop();
        } catch (e) {
        }
    },

    // At some point upgrading, set video width to 0px. To avoid hiding all videos, we consider 0 sized videos to have the default size.
    // To actully hide a video - set Visible to false
    _fixUpgradeSizeIssue: function () {
        if (this._width === "0px") {
            this._width = 0;
        }
        if (this._height === "0px") {
            this._height = 0;
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
    get_mimeType: function () {
        return this._mimeType;
    },
    set_mimeType: function (value) {
        this._mimeType = value;
    },
    get_autoPlay: function () {
        return this._autoPlay;
    },
    set_autoPlay: function (value) {
        if (value == null)
            value = false;
        this._autoPlay = value;
    },
    get_startVolume: function () {
        return this._startVolume;
    },
    set_startVolume: function (value) {
        if (value == null)
            value = 50;
        this._startVolume = value;
    },
    get_startTime: function () {
        return this._startTime;
    },
    set_startTime: function (value) {
        if (value == null)
            value = 0;
        this._startTime = value;
    },
    get_fullScreen: function () {
        return this._fullScreen;
    },
    set_fullScreen: function (value) {
        if (value == null)
            value = false;
        this._fullScreen = value;
    },
    get_isFrontend: function () {
        return this._isFrontend;
    },
    set_isFrontend: function (value) {
        if (value == null)
            value = false;
        this._isFrontend = value;
    },
    get_width: function () {
        return this._width;
    },
    set_width: function (value) {
        this._width = value;
    },
    get_height: function () {
        return this._height;
    },
    set_height: function (value) {
        this._height = value;
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

    get_useYouTubePlayer: function () {
        return this._useYouTubePlayer;
    },
    set_useYouTubePlayer: function (value) {
        this._useYouTubePlayer = value;
    },

    get_useYouTubePlaylist: function () {
        return this._useYouTubePlaylist;
    },
    set_useYouTubePlaylist: function (value) {
        this._useYouTubePlaylist = value;
    },

    // Gets the reference to the control which serves as a container for the player control.
    get_playerContainer: function () { return this._playerContainer; },

    // Sets the reference to the control which serves as a container for the player control.
    set_playerContainer: function (value) { this._playerContainer = value; }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl", Sys.UI.Control);

