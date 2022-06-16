﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView.initializeBase(this, [element]);
    this._galleriaTemplateUrl = null;
    this._imagesContainerId = null;
    this._enablePrevNextLinks = true;
    this._domReadyDelegate = null;
    this._galleriaCssUrl = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView.callBaseMethod(this, "initialize");

        this._domReadyDelegate = Function.createDelegate(this, this._domReadyHandler);
        jQuery(document).ready(this._domReadyDelegate);

    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView.callBaseMethod(this, "dispose");
        if (this._domReadyDelegate) {
            delete this._domReadyDelegate;
        }
    },

    // ----------------------------------------------- Private functions ----------------------------------------------

    _domReadyHandler: function (e) {
        Galleria.ready(function () {
            function setAlternateText(imageTarget, originalData) {
                if (!imageTarget || !originalData) {
                    return;
                }

                var alternateText = originalData.alt;
                imageTarget.setAttribute("alt", alternateText);
            }

            function encodeTitleAndDescription(e) {
                if (e.galleriaData.title && e.galleriaData.description) {
                    e.galleriaData.title = e.galleriaData.title.htmlEncode();
                    e.galleriaData.description = e.galleriaData.description.htmlEncode();
                }
            }

            this.bind('loadfinish', function (e) {
                if (e.imageTarget && e.galleriaData) {
                    setAlternateText(e.imageTarget, e.galleriaData.original);
                    encodeTitleAndDescription(e);
                }
            });

            this.bind('thumbnail', function (e) {
                if (e.thumbTarget && e.galleriaData) {
                    setAlternateText(e.thumbTarget, e.galleriaData.original);
                    encodeTitleAndDescription(e);
                }
            });
        });

        // Replacing Galleria.loadTheme - fixes loading Galleria theme script for IE
        var oldGalleriaLoadThemeFunction = Galleria.loadTheme;
        var loadScript = function (url, callback) {
            $.getScript(url, function () {
                done = true;
                if (typeof callback === 'function') {
                    callback.call(this, this);
                }
            });
        };

        Galleria.loadTheme = function (src, options) {
            if ($('script').filter(function () { return $(this).attr('src') === src; }).length) {
                return;
            }

            var loaded = false,
                err;

            $(window).on("load", function () {
                if (!loaded) {
                    err = window.setTimeout(function () {
                        if (!loaded) {
                            Galleria.raise("Galleria had problems loading theme at " + src + ". Please check theme path or load manually.", true);
                        }
                    }, 20000);
                }
            });

            loadScript(src, function () {
                loaded = true;
                window.clearTimeout(err);
            });

            return Galleria;
        };

        Galleria.loadTheme(this._galleriaTemplateUrl);
        Galleria.loadTheme = oldGalleriaLoadThemeFunction;

        var enablePrevNextLinks = this._enablePrevNextLinks;
        jQuery("#" + this._imagesContainerId).galleria({
            height: 400,
            extend: function (options) {
                if (enablePrevNextLinks) {
                    jQuery(this.get('stage')).find(".galleria-image-nav").show();
                }
                else {
                    jQuery(this.get('stage')).find(".galleria-image-nav").hide();
                }
            }
        });
    }
    // ----------------------------------------------- Public functions -----------------------------------------------


    // ------------------------------------------------- Properties ----------------------------------------------------

}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailStripView", Sys.UI.Control);