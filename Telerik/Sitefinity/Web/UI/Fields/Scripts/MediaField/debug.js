/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.MediaField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.MediaField.initializeBase(this, [element]);
    this._mediaPlayer = null;
}

Telerik.Sitefinity.Web.UI.Fields.MediaField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.MediaField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.MediaField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this._clearMediaItem();
        Telerik.Sitefinity.Web.UI.Fields.MediaField.callBaseMethod(this, "reset");
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    _clearMediaItem: function () {
        if (this._mediaPlayer) {
            this._mediaPlayer._clearMediaItem();
        }
    },
    
    // Gets the value(media url) of the video control.
    get_value: function () {
        if (this._mediaPlayer) {
            return this._mediaPlayer.get_url();
        }

        return "";
    },

    //Sets the value(media url) of the video control.
    set_value: function (value) {
        if (this._mediaPlayer) {
            var data = { url: this._appendRnd(value), title: '', description: '' };
            this._mediaPlayer.setMediaParams(data);
        }

        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    // Gets the media player control that displays the media url
    get_mediaPlayer: function () { return this._mediaPlayer; },
    // Sets the media player control that displays the media url
    set_mediaPlayer: function (value) { this._mediaPlayer = value; }

};
Telerik.Sitefinity.Web.UI.Fields.MediaField.registerClass("Telerik.Sitefinity.Web.UI.Fields.MediaField", Telerik.Sitefinity.Web.UI.Fields.FileField);
