/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web");

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView = function (element) {

    Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView.initializeBase(this, [element]);
    this._element = element;
    this._devicesSettings = null;
    this._inPortraitMode = true;
    this._currentDeviceName = null;
    this._previewPageUrl = null;
}

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView.prototype =
{
    initialize: function () {

        var me = this;
        this._devicesSettings = JSON.parse(this._devicesSettings);

        this._fillDevicesDropdown(this._devicesSettings, me);

        if (this._inPortraitMode) {
            $(this._selectors.portraitButton).addClass("sfSel");
            $(this._selectors.landscapeButton).removeClass("sfSel");
        } else {
            $(this._selectors.portraitButton).removeClass("sfSel");
            $(this._selectors.landscapeButton).addClass("sfSel");
        }

        // wire up portrait button
        $(this._selectors.portraitButton).click(function () {
            me._inPortraitMode = true;
            me._switchDevice(me._currentDeviceName);
            $(me._selectors.portraitButton).addClass("sfSel");
            $(me._selectors.landscapeButton).removeClass("sfSel");
        });

        // wire up landscape button
        $(this._selectors.landscapeButton).click(function () {
            me._inPortraitMode = false;
            me._switchDevice(me._currentDeviceName);
            $(me._selectors.portraitButton).removeClass("sfSel");
            $(me._selectors.landscapeButton).addClass("sfSel");
        });

        // wire up device buttons

        $(this._selectors.deviceSelect).change(function () {
            me._switchDevice($(this).val());
        });

        // load the first device
        this._switchDevice(this._devicesSettings[0].Name);
    },

    dispose: function () {
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    /* Fills the devices menu by groupping devices by DeviceCategory, each category is presented in optgroup */
    _fillDevicesDropdown: function (devices, model) {
        var categoryNames = new Array();
        $(devices).each(function (deviceIndex, deviceItem) {
            var deviceExists = $.grep(categoryNames, function (strItem, strIndex) {
                return strItem == deviceItem.DeviceCategory;
            }).length > 0;
            if (!deviceExists)
                categoryNames.push(deviceItem.DeviceCategory);
        });

        $(categoryNames).each(function (categoryIndex, categoryItem) {
            $(model._selectors.deviceSelect).append("<optgroup label=\"" + categoryItem + "\"></optgroup>");
            var allDevicesInThisCategory = $.grep(devices, function (deviceItem, deviceIndex) {
                return deviceItem.DeviceCategory == categoryItem;
            });

            $(allDevicesInThisCategory).each(function (deviceIndex, deviceItem) {
                $(model._selectors.deviceSelect).append("<option value=\"" + deviceItem.Name + "\">" + deviceItem.Title + "</option>");
            });
        });
    },
    /* switches the device in which the preview is being made */
    _switchDevice: function (newDeviceName) {

        // turn off all the device buttons

        var deviceSettings = this._getSettings(newDeviceName);

        // remove all device specific class
        var devicesCount = this._devicesSettings.length;
        while (devicesCount--) {
            $(this._selectors.devicePreviewContainer).removeClass(this._devicesSettings[devicesCount].CssClass);
        }

        $(this._selectors.devicePreviewContainer).removeClass();
        $(this._selectors.devicePreviewContainer).addClass("sfDevicePreviewContainer");

        // change the dimensions of the device preview container
        if (this._inPortraitMode) {

            $(this._selectors.devicePreviewContainer).width(deviceSettings.DeviceWidth);
            $(this._selectors.devicePreviewContainer).height(deviceSettings.DeviceHeight);

            $(this._selectors.viewport).width(deviceSettings.ViewportWidth);
            $(this._selectors.viewport).height(deviceSettings.ViewportHeight);
            $(this._selectors.viewport).css("left", deviceSettings.OffsetX + "px");
            $(this._selectors.viewport).css("top", deviceSettings.OffsetY + "px");

            // add the class for the currently selected device
            $(this._selectors.devicePreviewContainer).addClass(deviceSettings.CssClass);
        } else {

            $(this._selectors.devicePreviewContainer).height(deviceSettings.DeviceWidth);
            $(this._selectors.devicePreviewContainer).width(deviceSettings.DeviceHeight);

            $(this._selectors.viewport).width(deviceSettings.ViewportHeight);
            $(this._selectors.viewport).height(deviceSettings.ViewportWidth);
            $(this._selectors.viewport).css("left", deviceSettings.OffsetXLandscape + "px");
            $(this._selectors.viewport).css("top", deviceSettings.OffsetYLandscape + "px");

            // add the class for the currently selected device
            $(this._selectors.devicePreviewContainer).addClass(deviceSettings.CssClass + '_landscape');
        }

        this._currentDeviceName = newDeviceName;

        var me = this;
        if ($(this._selectors.viewport).attr("src") == null) {
            $(this._selectors.viewport).attr("src", this._previewPageUrl);
            $(this._selectors.viewport).on("load", function () {
                var _wheelDelegate = Function.createDelegate(me, me._wheel);
                var viewportFrame = $(me._selectors.viewport).get(0);
                var frameWindow = viewportFrame.contentWindow;
                if (frameWindow.addEventListener) {
                    /** DOMMouseScroll is for mozilla. */
                    frameWindow.addEventListener('DOMMouseScroll', _wheelDelegate, { passive: false });
                }
                /** IE/Opera. */
                frameWindow.addEventListener('mousewheel', _wheelDelegate, { passive: false });
                viewportFrame.contentDocument.addEventListener('mousewheel', _wheelDelegate, { passive: false });
            });
        }
    },


    /* Gets the settings for the specified device */
    _getSettings: function (deviceName) {
        var devicesCount = this._devicesSettings.length;
        while (devicesCount--) {
            if (this._devicesSettings[devicesCount].Name == deviceName) {
                return this._devicesSettings[devicesCount];
            }
        }
    },

    _selectors: {
        devicePreviewContainer: "#device-preview-container",
        portraitButton: "#mobile-preview-portrait-button",
        landscapeButton: "#mobile-preview-landscape-button",
        viewport: "#device-preview-viewport",
        deviceSelect: "#deviceSelect"
    },

    _wheel: function (event) {
        var delta = 0;
        if (!event) /* For IE. */
            event = window.event;
        if (event.wheelDelta) { /* IE/Opera. */
            delta = event.wheelDelta / 120;
        } else if (event.detail) { /** Mozilla case. */
            /** In Mozilla, sign of delta is different than in IE.
            * Also, delta is multiple of 3.
            */
            delta = -event.detail / 3;
        }
        /** If delta is nonzero, handle it.
        * Basically, delta is now positive if wheel was scrolled up,
        * and negative, if wheel was scrolled down.
        */
        if (delta)
            this._handle(delta);
        /** Prevent default actions caused by mouse wheel.
        * That might be ugly, but we handle scrolls somehow
        * anyway, so don't bother here..
        */
        if (event.preventDefault)
            event.preventDefault();
        event.returnValue = false;
    },

    /** This is high-level function.
    * It must react to delta being more/less than zero.
    */
    _handle: function (delta) {
        $(this._selectors.viewport).contents().scrollTop($(this._selectors.viewport).contents().scrollTop() - (delta * 10));
    }

    /* -------------------- properties ---------------- */
};

Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView", Sys.UI.Control);
