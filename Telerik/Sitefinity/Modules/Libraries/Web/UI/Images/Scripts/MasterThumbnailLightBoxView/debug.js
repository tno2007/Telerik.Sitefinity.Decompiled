﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView = function(element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView.initializeBase(this, [element]);
    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView.callBaseMethod(this, "initialize");
        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        if (this._onLoadDelegate !== null) {
            delete this._onLoadDelegate;
        }
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView.callBaseMethod(this, "dispose");
    },

    _onLoad: function () {
        $('#' + this.get_element().id + " a.sfLightBox").fancybox({
            beforeShow: function () {
                var width = $(this.element).data("width");
                if (width) {
                    this.width = width;
                }

                var height = $(this.element).data("height");
                if (height) {
                    this.height = height;
                }

                var title = $(this.element).attr("title");
                this.title = title.htmlEncode();
            }
        });
    }

    // ----------------------------------------------- Private functions ----------------------------------------------

    // ----------------------------------------------- Public functions -----------------------------------------------

    // ------------------------------------------------- Properties ----------------------------------------------------

};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailLightBoxView", Sys.UI.Control);