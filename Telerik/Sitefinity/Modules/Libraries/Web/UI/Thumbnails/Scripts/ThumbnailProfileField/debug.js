Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails")

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField.initializeBase(this, [element]);
    this._openSelector = null;
    this._thumbnailProfileSelectorDialog = null;
    this._generateThumbnail = null;
    this._doNotGenerateThumbnail = null;
    this._openSelectorDelegate = null;
    this._doneClientSelectionDelegate = null;
    this._selectedProfileIds = [];
    this._radioButtonsClickDelegate = null;
    this._getThmbProfilesSuccessDelegate = null;
    this._getThmbProfilesFailureDelegate = null;
    this._thumbnailProfiles = [];
    this._libType = null;
    this._thumbnailSettingsServiceUrl = null;
    this._initialSelectedProfileIds = [];
    this._clientLabelManager = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField.callBaseMethod(this, 'initialize');

        if (this._getThmbProfilesSuccessDelegate == null)
            this._getThmbProfilesSuccessDelegate = Function.createDelegate(this, this._getThmbProfilesSuccessHandler);

        if (this._getThmbProfilesFailureDelegate == null)
            this._getThmbProfilesFailureDelegate = Function.createDelegate(this, this._getThmbProfilesFailureHandler);

        if (this._displayMode == 1) {
            this._openSelectorDelegate = Function.createDelegate(this, this._openSelectorHandler);
            $addHandler(this._openSelector, "click", this._openSelectorDelegate);
            this._doneClientSelectionDelegate = Function.createDelegate(this, this._doneClientSelectionHandler);
            this._thumbnailProfileSelectorDialog.add_doneClientSelection(this._doneClientSelectionDelegate);

            this._radioButtonsClickDelegate = Function.createDelegate(this, this._radioButtonsClick);
            $addHandler(this.get_generateThumbnail(), "click", this._radioButtonsClickDelegate);
            $addHandler(this.get_doNotGenerateThumbnail(), "click", this._radioButtonsClickDelegate);
        }
    },

    dispose: function () {
        if (this._openSelectorDelegate) {
            if (this.get_openSelector())
                $removeHandler(this.get_openSelector(), "click", this._openSelectorDelegate);

            delete this._openSelectorDelegate;
        }

        if (this._doneClientSelectionDelegate) {
            this._thumbnailProfileSelectorDialog.remove_doneClientSelection(this._doneClientSelectionDelegate);
            delete this._doneClientSelectionDelegate;
        }

        if (this._radioButtonsClickDelegate) {
            if (this.get_doNotGenerateThumbnail()) {
                $removeHandler(this.get_doNotGenerateThumbnail(), "click", this._radioButtonsClickDelegate);
            }

            if (this.get_generateThumbnail()) {
                $removeHandler(this.get_generateThumbnail(), "click", this._radioButtonsClickDelegate);
            }

            delete this._radioButtonsClickDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField.callBaseMethod(this, 'dispose');
    },

    /* Events */

    add_radioButtonClicked: function (delegate) {
        this.get_events().addHandler('radioButtonClicked', delegate);
    },

    remove_radioButtonClicked: function (delegate) {
        this.get_events().removeHandler('radioButtonClicked', delegate);
    },

    _raiseRadioButtonClicked: function () {
        var eventArgs = null;
        var h = this.get_events().getHandler('radioButtonClicked');
        if (h) h(this, eventArgs);
    },

    _radioButtonsClick: function (sender, args) {
        var generateTmb = this.get_generateThumbnail().checked;
        this._updateVisibility(generateTmb);
        if (!generateTmb) {
            this._specifySelectedProfileIds([])
        }
        this._raiseRadioButtonClicked();
    },

    _doneClientSelectionHandler: function (sender, args) {
        this._specifySelectedProfileIds(args.get_commandArgument().slice(0));
    },

    _getThmbProfilesSuccessHandler: function (sender, result) {
        if (result && result.Items.length > 0) {
            this._thumbnailProfiles = result.Items;
            this._displayTmbProfiles(this._selectedProfileIds);
            dialogBase.resizeToContent();
        }
    },

    _getThmbProfilesFailureHandler: function (sender, result) {
        alert(sender.Detail);
    },

    _openSelectorHandler: function () {
        this._thumbnailProfileSelectorDialog._itemSelector._binder.get_urlParams().libraryType = this._libType;
        this._thumbnailProfileSelectorDialog.show(this._selectedProfileIds);
    },

    /* Private functions */

    _updateVisibility: function (showGenerate) {
        jQuery('.sfSelectProfilesWrapper').toggle(showGenerate);

        if (this.get_clientLabelManager()) {
            var labelText = null;

            if (this._selectedProfileIds && this._selectedProfileIds.length > 0) {
                labelText = this.get_clientLabelManager().getLabel('Labels', 'Change');
            }
            else {
                labelText = this.get_clientLabelManager().getLabel('Labels', 'Select');
            }
            var innerText = String.format("<span class='sfLinkBtnIn'>{0}</span>", labelText);
            jQuery(this.get_openSelector()).html(innerText);
        }
        if (this._displayMode == 0) {
            jQuery('.sfDoNotSelectProfilesWrapper').toggle(!showGenerate);
        }
    },

    _loadAllTmbProfiles: function () {
        if (this._thumbnailProfiles.length < 1) {
            var keys = {};
            var urlParams = {};
            urlParams["libraryType"] = this._libType;

            var clientManager = new Telerik.Sitefinity.Data.ClientManager(false);
            clientManager.InvokeGet(this._thumbnailSettingsServiceUrl, urlParams, keys, this._getThmbProfilesSuccessDelegate, this._getThmbProfilesFailureDelegate, this);
        }
    },

    _getTmbProfile: function (id) {
        for (var i = 0; i < this._thumbnailProfiles.length; i++) {
            if (this._thumbnailProfiles[i].Id == id) {
                return this._thumbnailProfiles[i];
            }
        }
    },

    _displayTmbProfiles: function (tmbProfilesIds) {
        $(".selectedThmbProfilesText").html('');

        for (i = 0; i < tmbProfilesIds.length; i++) {
            var profile = this._getTmbProfile(tmbProfilesIds[i]);
            if (profile) {
                $(".selectedThmbProfilesText").append("<span class='sfSelectedItem sfMRight10 sfInlineBlock'>" + profile.Title + " (" + profile.Size + ")</span>");
            }
        }

        var selectGenerateTmb = null;

        if (tmbProfilesIds.length > 0) {
            selectGenerateTmb = true;
        }
        else {
            selectGenerateTmb = false;
        }
        
        if (this._displayMode == 1) {
            this.get_generateThumbnail().checked = selectGenerateTmb;
            this.get_doNotGenerateThumbnail().checked = !selectGenerateTmb;
        }

        this._updateVisibility(selectGenerateTmb);
    },

    _getRemovedProfileIds: function () {
        return this._getDiffElements(this._initialSelectedProfileIds, this._selectedProfileIds);
    },

    _getAddedProfileIds: function () {
        return this._getDiffElements(this._selectedProfileIds, this._initialSelectedProfileIds);
    },

    _getDiffElements: function (mainArray, compareArray) {
        var diffElements = [];

        for (var i = 0; i < mainArray.length; i++) {
            if ($.inArray(mainArray[i], compareArray) == -1) {
                diffElements.push(mainArray[i]);
            }
        }

        return diffElements;
    },

    _specifySelectedProfileIds: function (value) {
        this._selectedProfileIds = value;
        this._loadAllTmbProfiles();
        this._displayTmbProfiles(this._selectedProfileIds);
    },

    /* Properties */
    get_value: function () {
        return Telerik.Sitefinity.fixArray(this._selectedProfileIds);
    },
    set_value: function (value) {
        if (!value) {
            value = [];
        }

        this._initialSelectedProfileIds = value;
        this._specifySelectedProfileIds(value);
    },

    get_openSelector: function () {
        return this._openSelector;
    },
    set_openSelector: function (value) {
        this._openSelector = value;
    },
    get_thumbnailProfileSelectorDialog: function () {
        return this._thumbnailProfileSelectorDialog;
    },
    set_thumbnailProfileSelectorDialog: function (value) {
        this._thumbnailProfileSelectorDialog = value;
    },
    get_generateThumbnail: function () {
        return this._generateThumbnail;
    },
    set_generateThumbnail: function (value) {
        this._generateThumbnail = value;
    },
    get_doNotGenerateThumbnail: function () {
        return this._doNotGenerateThumbnail;
    },
    set_doNotGenerateThumbnail: function (value) {
        this._doNotGenerateThumbnail = value;
    },
    get_thumbnailSettingsServiceUrl: function () {
        return this._thumbnailSettingsServiceUrl;
    },
    set_thumbnailSettingsServiceUrl: function (value) {
        this._thumbnailSettingsServiceUrl = value;
    },
    get_libType: function () {
        return this._libType;
    },
    set_libType: function (value) {
        this._libType = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileField', Telerik.Sitefinity.Web.UI.Fields.FieldControl);