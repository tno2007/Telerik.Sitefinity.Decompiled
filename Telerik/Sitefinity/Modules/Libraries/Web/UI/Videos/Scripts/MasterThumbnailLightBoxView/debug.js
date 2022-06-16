﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView.initializeBase(this, [element]);

    this._videoAutoPlay = false;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView.callBaseMethod(this, "initialize");
        var self = this;
        $(document).ready(function () {
            $('#' + self.get_element().id + " a.sfVideoLightBox").fancybox({
                'width': 610,
                'height': 480,
                'arrows': false,
                'scrolling': 'no',
                beforeClose: function () {
                    var radMediaPlayer = jQuery(".fancybox-inner .RadMediaPlayer");
                    var playerId = radMediaPlayer.attr("id");
                    var player = playerId ? $find(playerId) : null;
                    if (player) {
                        player.loadingIndicator.hide(player.ClientID);
                        if (player.currentPlayer && player.currentPlayer.media) {
                            player.currentPlayer.media.src = '';
                        }
                    }
                },
                beforeShow: function () {
                    //In HTML5 mode, we need to redraw the progress rail to initialize it.
                    var radMediaPlayer = jQuery(".fancybox-inner .RadMediaPlayer");
                    var playerId = radMediaPlayer.attr("id");
                    var player = playerId ? $find(playerId) : null;
                    if (player && player.currentPlayer && player.currentFile) {
                        var mediaPath = radMediaPlayer.closest('[sf-media-url]').attr('sf-media-url');
                        var mimeType = radMediaPlayer.closest('[sf-mime-type]').attr('sf-mime-type');
                        player.currentFile.path = mediaPath || player.currentFile.path;
                        player.currentFile.mimeType = mimeType || player.currentFile.mimeType;

                        player.currentFile.options.autoPlay = self.get_videoAutoPlay();
                        var currentPlayer = player.currentPlayer;
                        $telerik.$(currentPlayer.media).remove();
                        currentPlayer.dispose();
                        player.dispose();
                        delete player.supportedPlayers.Flash;
                        if (player.toolbar._jprogressTooltip)
                            player.toolbar._jprogressTooltip.remove();
                        player.initialize();
                    }
                    if (player && player.toolbar && player.toolbar._jprogressRail) {
                        var progressRailId = player.toolbar._jprogressRail.attr("id");
                        var progressRail = progressRailId ? $find(progressRailId) : null;
                        if (progressRail && progressRail.redraw) {
                            progressRail.redraw();
                        }
                    }
                }
            });
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView.callBaseMethod(this, "dispose");
    },

    // ----------------------------------------------- Private functions ----------------------------------------------


    // ----------------------------------------------- Public functions -----------------------------------------------


    // ------------------------------------------------- Properties ----------------------------------------------------

    get_videoAutoPlay: function () {
        return this._videoAutoPlay;
    },
    set_videoAutoPlay: function (value) {
        this._videoAutoPlay = value && value !== "false" ? true : false;
    }

}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MasterThumbnailLightBoxView", Sys.UI.Control);