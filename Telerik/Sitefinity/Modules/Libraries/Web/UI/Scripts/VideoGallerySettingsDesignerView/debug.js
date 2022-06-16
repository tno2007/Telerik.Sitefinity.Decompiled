Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView.initializeBase(this, [element]);
    this._showEmbeddingOptionClientIDs = [];
    this._showRelatedVideosClientIDs = [];
    this._allowFullSizeClientIDs = [];
    this._editThumbnailListTemplateLink = null;
    this._editThumbnailLightboxTemplateLink = null;
    this._editDetailTemplateLink = null;
    this._editTemplateLinkDelegate = null;
    this._editThumbnailLightboxDelegate = null;
    this._editDetailTemplateLinkDelegate = null;
    // a dictionary to map between links for editing control templates and template names
    // KEY: ClientID of link that opens up a template for editing
    // VALUE: ID of the template which is opened for editing by the link
    this._templateLinkIdMap = [];
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView.callBaseMethod(this, "initialize");

        if (this._editTemplateLinkDelegate == null) {
            this._editTemplateLinkDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        }
        $addHandler(this._editThumbnailListTemplateLink, "click", this._editTemplateLinkDelegate);
        if (this._editThumbnailLightboxDelegate == null) {
            this._editThumbnailLightboxDelegate = Function.createDelegate(this, this._editLightboxTemplateLinkClicked);
        }
        $addHandler(this._editThumbnailLightboxTemplateLink, "click", this._editThumbnailLightboxDelegate);
        if (this._editDetailTemplateLinkDelegate == null) {
            this._editDetailTemplateLinkDelegate = Function.createDelegate(this, this._editDetailTemplateLinkClicked);
        }
        $addHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateLinkDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView.callBaseMethod(this, "dispose");

        $removeHandler(this._editThumbnailListTemplateLink, "click", this._editTemplateLinkDelegate);
        if (this._editTemplateLinkDelegate) {
            delete this._editTemplateLinkDelegate;
        }
        $removeHandler(this._editThumbnailLightboxTemplateLink, "click", this._editThumbnailLightboxDelegate);
        if (this._editThumbnailLightboxDelegate) {
            delete this._editThumbnailLightboxDelegate;
        }
        $removeHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateLinkDelegate);
        if (this._editDetailTemplateLinkDelegate) {
            delete this._editDetailTemplateLinkDelegate;
        }
    },

    // ----------------------------------------------- Private functions ----------------------------------------------

    _editLightboxTemplateLinkClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editThumbnailLightboxTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editDetailTemplateLinkClicked: function (sender, args) {
        this._selectedDetailTemplateId = this._templateLinkIdMap[this.get_editDetailTemplateLink().id];
        this._selectedMasterTemplateId = null;
        this._showWidgetEditor();
    },

    _editTemplateLinkClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editThumbnailListTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    // ----------------------------------------------- Public functions -----------------------------------------------

    refreshCustomUI: function (newMode) {
        var currentView = this.get_currentDetailView();
        var index = newMode + 1;

        if (currentView.ShowEmbeddingOption) {
            var showEmbeddingOption = $find(this._showEmbeddingOptionClientIDs[index]);
            if (showEmbeddingOption) {
                showEmbeddingOption.set_selectedChoicesIndex(0);
            }
        }

        if (currentView.ShowRelatedVideos) {
            var showRelatedVideos = $find(this._showRelatedVideosClientIDs[index]);
            if (showRelatedVideos) {
                showRelatedVideos.set_selectedChoicesIndex(0);
            }
        }

        if (currentView.AllowFullSize) {
            var allowFullSize = $find(this._allowFullSizeClientIDs[index]);
            if (allowFullSize) {
                allowFullSize.set_selectedChoicesIndex(0);
            }
        }
    },

    applyChanges: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView.callBaseMethod(this, "applyChanges");

        var index = this.get_currentMode() + 1;
        var currentView = this.get_currentDetailView();

        // set ShowEmbeddingOption property
        var showEmbeddingOption = $find(this._showEmbeddingOptionClientIDs[index]);
        if (showEmbeddingOption && showEmbeddingOption.get_selectedChoicesIndex()) {
            currentView.ShowEmbeddingOption = true;
        }
        else {
            currentView.ShowEmbeddingOption = false;
        }

        // set ShowRelatedVideos property
        var showRelatedVideos = $find(this._showRelatedVideosClientIDs[index]);
        if (showRelatedVideos && showRelatedVideos.get_selectedChoicesIndex()) {
            currentView.ShowRelatedVideos = true;
        }
        else {
            currentView.ShowRelatedVideos = false;
        }

        // set AllowFullSize property
        var allowFullSize = $find(this._allowFullSizeClientIDs[index]);
        if (allowFullSize && allowFullSize.get_selectedChoicesIndex()) {
            currentView.AllowFullSize = true;
        }
        else {
            currentView.AllowFullSize = false;
        }
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    get_editThumbnailListTemplateLink: function () {
        return this._editThumbnailListTemplateLink;
    },

    set_editThumbnailListTemplateLink: function (value) {
        if (this._editThumbnailListTemplateLink != value) {
            this._editThumbnailListTemplateLink = value;
        }
    },

    get_editThumbnailLightboxTemplateLink: function () {
        return this._editThumbnailLightboxTemplateLink;
    },

    set_editThumbnailLightboxTemplateLink: function (value) {
        if (this._editThumbnailLightboxTemplateLink != value) {
            this._editThumbnailLightboxTemplateLink = value;
        }
    },

    get_editDetailTemplateLink: function () {
        return this._editDetailTemplateLink;
    },

    set_editDetailTemplateLink: function (value) {
        if (this._editDetailTemplateLink != value) {
            this._editDetailTemplateLink = value;
        }
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGallerySettingsDesignerView", Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView);

