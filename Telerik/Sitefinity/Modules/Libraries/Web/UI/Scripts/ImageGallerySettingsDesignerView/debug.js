Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.initializeBase(this, [element]);
    this._showPrevAndNextLinksClientIDs = [];
    this._selectPrevAndNextLinksTypeClientIDs = [];
    this._valueChangedDelegate == null;

    this._templateLinkIdMap = [];
    this._editThumbnailListTemplateLink = null;
    this._editDetailTemplateLink = null;
    this._editThumbnailLightboxTemplateLink = null;
    this._editThumbnailStripTemplateLink = null;
    this._editDetailTemplateLink2 = null;
    this._editSimpleListTemplateLink = null;
    this._editThumbnailListTemplateDelegate = null;
    this._editDetailTemplateDelegate = null;
    this._editThumbnailLightboxTemplateDelegate = null;
    this._editThumbnailStripTemplateDelegate = null;
    this._editDetailTemplateLink2Delegate = null;
    this._editSimpleListTemplateDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.callBaseMethod(this, "initialize");

        if (this._valueChangedDelegate == null) {
            this._valueChangedDelegate = Function.createDelegate(this, this._valueChangedHandler);
        }
        if (this._editThumbnailListTemplateDelegate == null) {
            this._editThumbnailListTemplateDelegate = Function.createDelegate(this, this._editThumbnailListTemplateClicked);
        }
        $addHandler(this._editThumbnailListTemplateLink, "click", this._editThumbnailListTemplateDelegate);

        if (this._editDetailTemplateDelegate == null) {
            this._editDetailTemplateDelegate = Function.createDelegate(this, this._editDetailTemplateClicked);
        }
        $addHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateDelegate);

        if (this._editThumbnailLightboxTemplateDelegate == null) {
            this._editThumbnailLightboxTemplateDelegate = Function.createDelegate(this, this._editThumbnailLightboxTemplateClicked);
        }
        $addHandler(this._editThumbnailLightboxTemplateLink, "click", this._editThumbnailLightboxTemplateDelegate);

        if (this._editThumbnailStripTemplateDelegate == null) {
            this._editThumbnailStripTemplateDelegate = Function.createDelegate(this, this._editThumbnailStripTemplateClicked);
        }
        $addHandler(this._editThumbnailStripTemplateLink, "click", this._editThumbnailStripTemplateDelegate);

        if (this._editDetailTemplateLink2Delegate == null) {
            this._editDetailTemplateLink2Delegate = Function.createDelegate(this, this._editDetailTemplateLink2Clicked);
        }
        $addHandler(this._editDetailTemplateLink2, "click", this._editDetailTemplateLink2Delegate);

        if (this._editSimpleListTemplateDelegate == null) {
            this._editSimpleListTemplateDelegate = Function.createDelegate(this, this._editSimpleListTemplateClicked);
        }
        $addHandler(this._editSimpleListTemplateLink, "click", this._editSimpleListTemplateDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.callBaseMethod(this, "dispose");
        if (this._valueChangedDelegate != null) {
            delete this._valueChangedDelegate;
        }
        $removeHandler(this._editThumbnailListTemplateLink, "click", this._editThumbnailListTemplateDelegate);
        if (this._editThumbnailListTemplateDelegate) {
            delete this._editThumbnailListTemplateDelegate;
        }
        $removeHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateDelegate);
        if (this._editDetailTemplateDelegate) {
            delete this._editDetailTemplateDelegate;
        }
        $removeHandler(this._editThumbnailLightboxTemplateLink, "click", this._editThumbnailLightboxTemplateDelegate);
        if (this._editThumbnailLightboxTemplateDelegate) {
            delete this._editThumbnailLightboxTemplateDelegate;
        }
        $removeHandler(this._editThumbnailStripTemplateLink, "click", this._editThumbnailStripTemplateDelegate);
        if (this._editThumbnailStripTemplateDelegate) {
            delete this._editThumbnailStripTemplateDelegate;
        }
        $removeHandler(this._editDetailTemplateLink2, "click", this._editDetailTemplateLink2Delegate);
        if (this._editDetailTemplateLink2Delegate) {
            delete this._editDetailTemplateLink2Delegate;
        }
        $removeHandler(this._editSimpleListTemplateLink, "click", this._editSimpleListTemplateDelegate);
        if (this._editSimpleListTemplateDelegate) {
            delete this._editSimpleListTemplateDelegate;
        }
    },

    // ----------------------------------------------- Private functions ----------------------------------------------

    _valueChangedHandler: function (sender, args) {
        var index = this.get_currentMode() + 1;
        var selectPrevAndNextLinksType = this.get_currentSelectPrevAndNextLinksType(index);
        if (selectPrevAndNextLinksType) {
            $(selectPrevAndNextLinksType.get_element()).toggle();
            dialogBase.resizeToContent();
        }
    },

    _pageLoadHandler: function (sender, args) {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.callBaseMethod(this, "_pageLoadHandler", [sender, args]);

        for (var index in this._showPrevAndNextLinksClientIDs) {
            var showPrevAndNextLink = this.get_currentShowPrevAndNextLink(index);
            if (showPrevAndNextLink) {
                showPrevAndNextLink.add_valueChanged(this._valueChangedDelegate);
            }
        }
    },



    _editThumbnailListTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editThumbnailListTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editDetailTemplateClicked: function (sender, args) {
        this._selectedDetailTemplateId = this._templateLinkIdMap[this.get_editDetailTemplateLink().id];
        this._selectedMasterTemplateId = null;
        this._showWidgetEditor();
    },

    _editThumbnailLightboxTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editThumbnailLightboxTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editThumbnailStripTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editThumbnailStripTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editDetailTemplateLink2Clicked: function (sender, args) {
        this._selectedDetailTemplateId = this._templateLinkIdMap[this.get_editDetailTemplateLink2().id];
        this._selectedMasterTemplateId = null;
        this._showWidgetEditor();
    },

    _editSimpleListTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editSimpleListTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },


    // ----------------------------------------------- Public functions -----------------------------------------------

    refreshCustomUI: function (newMode) {
        var index = newMode + 1;
        var currentView = null;
        // Thumbnails + Detail page
        if (newMode == 0) {
            currentView = this.get_currentDetailView();
        }
        else {
            currentView = this.get_currentView();
        }

        if (currentView.EnablePrevNextLinks) {
            var showPrevAndNextLink = this.get_currentShowPrevAndNextLink(index);
            if (showPrevAndNextLink) {
                showPrevAndNextLink.set_selectedChoicesIndex(0);
            }
        }

        if (currentView.PrevNextLinksDisplayMode) {
            var selectPrevAndNextLinksType = this.get_currentSelectPrevAndNextLinksType(index);
            if (selectPrevAndNextLinksType) {
                if (currentView.EnablePrevNextLinks) {
                    $(selectPrevAndNextLinksType.get_element()).show();
                    selectPrevAndNextLinksType.set_value(currentView.PrevNextLinksDisplayMode);
                }
                else {
                    $(selectPrevAndNextLinksType.get_element()).hide();
                }
                dialogBase.resizeToContent();
            }
        }
    },

    applyChanges: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.callBaseMethod(this, "applyChanges");
        var index = this.get_currentMode() + 1;
        var currentView = null;
        // Thumbnails + Detail page
        if (this.get_currentMode() == 0) {
            currentView = this.get_currentDetailView();
        }
        else {
            currentView = this.get_currentView();
        }

        // set EnablePrevNextLinks property
        var showPrevAndNextLink = this.get_currentShowPrevAndNextLink(index);
        if (showPrevAndNextLink && showPrevAndNextLink.get_selectedChoicesIndex()) {
            currentView.EnablePrevNextLinks = true;
        }
        else {
            currentView.EnablePrevNextLinks = false;
        }

        // set PrevNextLinksDisplayMode property
        var selectPrevAndNextLinksType = this.get_currentSelectPrevAndNextLinksType(index);
        if (selectPrevAndNextLinksType) {
            currentView.PrevNextLinksDisplayMode = selectPrevAndNextLinksType.get_value();
        }
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    get_currentShowPrevAndNextLink: function (index) {
        return $find(this._showPrevAndNextLinksClientIDs[index]);
    },

    get_currentSelectPrevAndNextLinksType: function (index) {
        return $find(this._selectPrevAndNextLinksTypeClientIDs[index]);
    },

    get_editThumbnailListTemplateLink: function () {
        return this._editThumbnailListTemplateLink;
    },

    set_editThumbnailListTemplateLink: function (value) {
        if (this._editThumbnailListTemplateLink != value) {
            this._editThumbnailListTemplateLink = value;
        }
    },

    get_editDetailTemplateLink: function () {
        return this._editDetailTemplateLink;
    },

    set_editDetailTemplateLink: function (value) {
        if (this._editDetailTemplateLink != value) {
            this._editDetailTemplateLink = value;
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

    get_editThumbnailStripTemplateLink: function () {
        return this._editThumbnailStripTemplateLink;
    },

    set_editThumbnailStripTemplateLink: function (value) {
        if (this._editThumbnailStripTemplateLink != value) {
            this._editThumbnailStripTemplateLink = value;
        }
    },

    get_editDetailTemplateLink2: function () {
        return this._editDetailTemplateLink2;
    },

    set_editDetailTemplateLink2: function (value) {
        if (this._editDetailTemplateLink2 != value) {
            this._editDetailTemplateLink2 = value;
        }
    },

    get_editSimpleListTemplateLink: function () {
        return this._editSimpleListTemplateLink;
    },

    set_editSimpleListTemplateLink: function (value) {
        if (this._editSimpleListTemplateLink != value) {
            this._editSimpleListTemplateLink = value;
        }
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGallerySettingsDesignerView", Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView);

