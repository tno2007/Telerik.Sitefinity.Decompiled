
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog = function(element){
    Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog.initializeBase(this, [element]);

    this._mediaPlayer = null;
    this._doneButton = null;
    this._cancelButton = null;

    //delegates
     this._loadDelegate = null;
     this._doneClientSelectionDelegate = null;
     this._cancelClientSelectionDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog.callBaseMethod(this, "initialize");

        if (this._loadDelegate == null) {
            this._loadDelegate = Function.createDelegate(this, this.loadDialog);
        }
        Sys.Application.add_load(this._loadDelegate);

        if (this._doneButton) {
            if (this._doneClientSelectionDelegate == null) {
                this._doneClientSelectionDelegate = Function.createDelegate(this, this._handleDoneClientSelection);
            }
            $addHandler(this._doneButton, 'click', this._doneClientSelectionDelegate);
        }

        if (this._cancelButton) {
            if (this._cancelClientSelectionDelegate == null) {
                this._cancelClientSelectionDelegate = Function.createDelegate(this, this._handleCancelClientSelection);
            }
            $addHandler(this._cancelButton, 'click', this._cancelClientSelectionDelegate);
        }
    },

    dispose: function () {
        if (this._loadDelegate != null) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._doneClientSelectionDelegate != null) {
            delete this._doneClientSelectionDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    // Loads the media player dialog for editing thumbnails
    loadDialog: function () {
        var url = dialogBase.getQueryValue('mediaUrl', true);
        var data = { url: unescape(url), title: '', description: '', autoPlay: 'true' };
        if (this._mediaPlayer) {
            this._mediaPlayer.setMediaParams(data);
        }
    },
    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
    _handleDoneClientSelection: function () {

        this._mediaPlayer._clearMediaItem();
        var data = this._mediaPlayer.getThumbnailData();
       
        if (data) {
            var arg = { "Data": data };
            this.close(arg);
        }
        else {
            this.close();
        }
    },

    _handleCancelClientSelection: function () {
        this._mediaPlayer._clearMediaItem();
        this.close();
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */
    // Gets the media player control that displays the media url
    get_mediaPlayer: function () {
        return this._mediaPlayer;
    },

    // Sets the media player control that displays the media url
    set_mediaPlayer: function (value) {
        this._mediaPlayer = value;
    },

    get_doneButton: function () {
        return this._doneButton;
    },

    set_doneButton: function (value) {
        this._doneButton = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },

    set_cancelButton: function (value) {
        this._cancelButton = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.ThumbnailMediaPlayerDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);