Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog.initializeBase(this, [element]);

    this._buttonDone = null;
    this._buttonCancel = null;

    this._buttonDoneDelegate = null;
    this._buttonCancelClickDelegate = null;
    this._onDialogShowDelegate = null;
    this._onDialogLoadDelegate = null;
    this._thumbnailProfileField = null;
    this._libraryDataItem = null;
    this._thumbnailServiceUrl = null;
    this._ajaxRunning = false;
    this._profileFieldRadioButtonClickedDelegate = null;
    this._profileSettingsDialogOpenDelegate = null;
    this._profileSettingsDialogCloseDelegate = null;
    this._labelManager = null;

    this._labelWarningMessage = null;
    this._secondaryButtonDone = null;
    this._secondaryButtonCancel = null;
    this._secondaryButtonDoneDelegate = null;
    this._secondaryButtonCancelDelegate = null;

}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog.callBaseMethod(this, 'initialize');

        this._initDelegates();
        this._addHandlers();
    },

    dispose: function () {

        this._removeHandlers();
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog.callBaseMethod(this, 'dispose');
    },

    /* Private Methods */

    _initDelegates: function () {
        this._buttonDoneDelegate = Function.createDelegate(this, this._buttonDoneHandler);
        this._buttonCancelClickDelegate = Function.createDelegate(this, this._buttonCancelClickHandler);
        this._onDialogShowDelegate = Function.createDelegate(this, this._onDialogShowHandler);
        this._onDialogLoadDelegate = Function.createDelegate(this, this._onDialogLoadHandler);
        this._profileFieldRadioButtonClickedDelegate = Function.createDelegate(this, this._profileFieldRadioButtonClickedHandler);
        this._profileSettingsDialogOpenDelegate = Function.createDelegate(this, this._profileSettingsDialogOpenHandler);
        this._profileSettingsDialogCloseDelegate = Function.createDelegate(this, this._profileSettingsDialogCloseHandler);
        this._secondaryButtonDoneDelegate = Function.createDelegate(this, this._secondaryButtonDoneHandler);
        this._secondaryButtonCancelDelegate = Function.createDelegate(this, this._secondaryButtonCancelHandler);
    },

    _addHandlers: function () {
        this.get_radWindow().add_show(this._onDialogShowDelegate);
        this.get_radWindow().add_pageLoad(this._onDialogLoadDelegate);
        this.get_thumbnailProfileField().add_radioButtonClicked(this._profileFieldRadioButtonClickedDelegate);
        $addHandler(this.get_buttonDone(), 'click', this._buttonDoneDelegate);
        $addHandler(this.get_buttonCancel(), 'click', this._buttonCancelClickDelegate);
        $addHandler(this.get_secondaryButtonDone(), 'click', this._secondaryButtonDoneDelegate);
        $addHandler(this.get_secondaryButtonCancel(), 'click', this._secondaryButtonCancelDelegate);
    },

    _removeHandlers: function () {

        if (this._onDialogShowDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_show(this._onDialogShowDelegate);
            }
            delete this._onDialogShowDelegate;
        }

        if (this._onDialogLoadDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_pageLoad(this._onDialogLoadDelegate);
            }
            delete this._onDialogLoadDelegate;
        }

        if (this._buttonDoneDelegate) {
            if (this.get_buttonDone()) {
                $removeHandler(this.get_buttonDone(), 'click', this._buttonDoneDelegate);
            }
            delete this.buttonDoneDelegate;
        }

        if (this._buttonCancelClickDelegate) {
            if (this.get_buttonCancel()) {
                $removeHandler(this.get_buttonCancel(), 'click', this._buttonDoneDelegate);
            }
            delete this.buttonCancelClickDelegate;
        }

        if (this._profileFieldRadioButtonClickedDelegate) {
            if (this.get_thumbnailProfileField()) {
                this.get_thumbnailProfileField().remove_radioButtonClicked(this._profileFieldRadioButtonClickedHandler);
            }
            delete this.profileFieldRadioButtonClickedDelegate;
        }

        var profileDialog = this.get_thumbnailProfileField().get_thumbnailProfileSelectorDialog().get_kendoWindow();
        if (this._profileSettingsDialogOpenDelegate) {
            if (profileDialog) {
                profileDialog.unbind("activate", this._profileSettingsDialogOpenDelegate);
            }
            delete this.profileSettingsDialogOpenDelegate;
        }

        if (this._profileSettingsDialogCloseDelegate) {
            if (profileDialog) {
                profileDialog.unbind("deactivate", this._profileSettingsDialogCloseDelegate);
            }
            delete this.profileSettingsDialogCloseDelegate;
        }

        if (this._secondaryButtonDoneDelegate) {
            if (this.get_secondaryButtonDone()) {
                $removeHandler(this.get_secondaryButtonDone(), 'click', this._secondaryButtonDoneDelegate);
            }
            delete this.secondaryButtonDoneDelegate;
        }

        if (this._secondaryButtonCancelDelegate) {
            if (this.get_secondaryButtonCancel()) {
                $removeHandler(this.get_secondaryButtonCancel(), 'click', this._secondaryButtonCancelDelegate);
            }
            delete this.secondaryButtonCancelDelegate;
        }
    },

    /* Private Methods */

    _getProfileNames: function () {
        var dummyParameter = new Date().getTime();
        var requestUrl = String.format("{0}/{1}/profiles?provider={2}&time={3}",
            this.get_thumbnailServiceUrl(), this.get_libraryDataItem().Id, this.get_libraryDataItem().ProviderName, dummyParameter);
        var that = this;
        this._ajaxRunning = true;
        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            processData: false,
            contentType: "application/json",
            success: function (data) {
                var profileNames = [];
                for (var i = 0; i < data.Items.length; i++) {
                    profileNames.push(data.Items[i].Id);
                }
                that.get_thumbnailProfileField().set_value(profileNames);
            },
            error: function (jqXHR) {
                alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
            },
            complete: function () {
                that._ajaxRunning = false;
                that.resizeToContent();
            }
        });
    },

    _formatProfilesToString: function (profiles) {
        var result = "";
        if (profiles.length > 1) {
            result = this._getProfileTitle(profiles[0]);
            if (profiles.length > 2) {
                for (var i = 1; i < profiles.length - 1; i++) {
                    result = result + String.format(", {0}", this._getProfileTitle(profiles[i]));
                }
            }
        }
        return result;
    },

    _getProfileTitle: function (profileName) {
        return String.format("<i>{0}</i>", this.get_thumbnailProfileField()._getTmbProfile(profileName).Title);
    },
    /* Private Methods */

    /* Event Handlers */

    _buttonDoneHandler: function (sender, args) {

        var removedProfiles = this.get_thumbnailProfileField()._getRemovedProfileIds();
        var addedProfiles = this.get_thumbnailProfileField()._getAddedProfileIds();
        var warningMessage = null;
        var buttonMessage = null;
        if (removedProfiles.length == 1 && addedProfiles.length == 1) {
            warningMessage =
                String.format(this.get_labelManager().getLabel("LibrariesResources", "SingleThumbnailAddedWarning"), this._getProfileTitle(addedProfiles[0])) +
                String.format(this.get_labelManager().getLabel("LibrariesResources", "SingleThumbnailDeletedWarning"), this._getProfileTitle(removedProfiles[0]));
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "DeleteAndRegenerate");
        } else if (removedProfiles.length > 1 && addedProfiles.length == 1) {
            warningMessage =
                String.format(this.get_labelManager().getLabel("LibrariesResources", "SingleThumbnailAddedWarning"), this._getProfileTitle(addedProfiles[0])) +
                String.format(this.get_labelManager().getLabel("LibrariesResources", "MultipleThumbnailsDeletedWarning"), this._formatProfilesToString(removedProfiles), this._getProfileTitle(removedProfiles[removedProfiles.length - 1]));
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "DeleteAndRegenerate");
        } else if (removedProfiles.length == 1 && addedProfiles.length > 1) {
            warningMessage =
                String.format(this.get_labelManager().getLabel("LibrariesResources", "SingleThumbnailDeletedWarning"), this._getProfileTitle(removedProfiles[0])) +
                String.format(this.get_labelManager().getLabel("LibrariesResources", "MultpleThumbnailsAddedWarning"), this._formatProfilesToString(addedProfiles), this._getProfileTitle(addedProfiles[addedProfiles.length - 1]));
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "DeleteAndRegenerate");
        } else if (removedProfiles.length > 1 && addedProfiles.length > 1) {
            warningMessage =
                String.format(this.get_labelManager().getLabel("LibrariesResources", "MultipleThumbnailsDeletedWarning"), this._formatProfilesToString(removedProfiles), this._getProfileTitle(removedProfiles[removedProfiles.length - 1])) +
                String.format(this.get_labelManager().getLabel("LibrariesResources", "MultpleThumbnailsAddedWarning"), this._formatProfilesToString(addedProfiles), this._getProfileTitle(addedProfiles[addedProfiles.length - 1]));
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "DeleteAndRegenerate");
        } else if (removedProfiles.length == 1) {
            warningMessage = String.format(this.get_labelManager().getLabel("LibrariesResources", "SingleThumbnailDeletedWarning"), this._getProfileTitle(removedProfiles[0]))
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "YesDeleteThumbnails");
        } else if (removedProfiles.length > 1) {
            warningMessage = String.format(this.get_labelManager().getLabel("LibrariesResources", "MultipleThumbnailsDeletedWarning"), this._formatProfilesToString(removedProfiles), this._getProfileTitle(removedProfiles[removedProfiles.length - 1]))
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "YesDeleteThumbnails");
        } else if (addedProfiles.length == 1) {
            warningMessage = String.format(this.get_labelManager().getLabel("LibrariesResources", "SingleThumbnailAddedWarning"), this._getProfileTitle(addedProfiles[0]))
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "YesRegenerate");
        } else if (addedProfiles.length > 1) {
            warningMessage = String.format(this.get_labelManager().getLabel("LibrariesResources", "MultpleThumbnailsAddedWarning"), this._formatProfilesToString(addedProfiles), this._getProfileTitle(addedProfiles[addedProfiles.length - 1]))
            buttonMessage = this.get_labelManager().getLabel("LibrariesResources", "YesRegenerate");
        } else {
            var context = { Data: this.get_thumbnailProfileField().get_value(), Command: "cancel" };
            this.close(context);
            return;
        }
        jQuery("#mainView").hide();
        jQuery(this.get_labelWarningMessage()).html(warningMessage);
        jQuery(this.get_secondaryButtonDone()).html("<span class='sfLinkBtnIn'>" + buttonMessage + "</span>");
        jQuery("#secondaryView").show();

        this.resizeToContent();
    },

    _buttonCancelClickHandler: function (sender, args) {
        var context = { Data: null, Command: "cancel" };
        this.close();
    },

    _secondaryButtonDoneHandler: function (sender, args) {
        var context = { Data: this.get_thumbnailProfileField().get_value(), Command: "submit" };
        this._secondaryButtonCancelHandler(sender, args);
        this.close(context);
    },

    _secondaryButtonCancelHandler: function (sender, args) {
        jQuery("#secondaryView").hide();
        jQuery("#mainView").show();
        this.resizeToContent();
    },

    _onDialogShowHandler: function (sender, args) {
        jQuery("body").addClass("sfSelectorDialog");

        if ((jQuery.browser.safari || jQuery.browser.chrome) && !dialogBase._dialog.isMaximized()) {
            jQuery("body").addClass("sfOverflowHiddenX");
        }

        var dialog = this.get_thumbnailProfileField().get_thumbnailProfileSelectorDialog();
        jQuery(function () {
            var profileDialog = dialog.get_kendoWindow();
            profileDialog.unbind("activate", this._profileSettingsDialogOpenDelegate);
            profileDialog.unbind("deactivate", this._profileSettingsDialogCloseDelegate);
            profileDialog.bind("activate", this._profileSettingsDialogOpenDelegate);
            profileDialog.bind("deactivate", this._profileSettingsDialogCloseDelegate);
        });
    },

    _onDialogLoadHandler: function (sender, args) {
        this._onDialogShowHandler(sender, args);
    },

    _profileFieldRadioButtonClickedHandler: function (sender, args) {
        this.resizeToContent();
    },

    _profileSettingsDialogOpenHandler: function (sender, args) {
        this.resizeToContent();
    },

    _profileSettingsDialogCloseHandler: function (sender, args) {
        this.resizeToContent();
    },

    /* Event Handlers */

    /* Properties */

    get_buttonDone: function () {
        return this._buttonDone;
    },
    set_buttonDone: function (value) {
        this._buttonDone = value;
    },

    get_buttonCancel: function () {
        return this._buttonCancel;
    },
    set_buttonCancel: function (value) {
        this._buttonCancel = value;
    },

    get_thumbnailProfileField: function () {
        return this._thumbnailProfileField;
    },
    set_thumbnailProfileField: function (value) {
        this._thumbnailProfileField = value;
    },

    get_libraryDataItem: function () {
        return this._libraryDataItem;
    },
    set_libraryDataItem: function (value) {
        this._libraryDataItem = value;
        if (!this._ajaxRunning) {
            this._getProfileNames();
        }
        else {
            this.resizeToContent();
        }
    },

    get_thumbnailServiceUrl: function () {
        return this._thumbnailServiceUrl;
    },
    set_thumbnailServiceUrl: function (value) {
        this._thumbnailServiceUrl = value;
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },

    get_secondaryButtonCancel: function () {
        return this._secondaryButtonCancel;
    },
    set_secondaryButtonCancel: function (value) {
        this._secondaryButtonCancel = value;
    },

    get_secondaryButtonDone: function () {
        return this._secondaryButtonDone;
    },
    set_secondaryButtonDone: function (value) {
        this._secondaryButtonDone = value;
    },

    get_labelWarningMessage: function () {
        return this._labelWarningMessage;
    },
    set_labelWarningMessage: function (value) {
        this._labelWarningMessage = value;
    }

    /* Properties */
}
Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailSettingsDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
