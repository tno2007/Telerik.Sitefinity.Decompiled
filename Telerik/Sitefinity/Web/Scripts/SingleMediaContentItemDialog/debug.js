﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog = function (element) {
    this._uploaderView = null;
    this._selectorView = null;
    this._saveLink = null;
    this._cancelLink = null;
    this._backLink = null;
    this._titleLabel = null;
    this._cancelLinkTitle = null;
    this._backLinkTitle = null;
    this._buttonArea = null;
    this._uploaderSection = null;
    this._selectorSection = null;
    this._filterSection = null;
    this._useOnlyUploadMode = null;
    this._useOnlySelectMode = null;
    this._sourceLibraryId = null;

    this._dialogMode = null;
    this._mediaDialogOpenMode = null;
    this._selectedDataItem = null;
    this._domElementToInsert = null;
    this._currentMode = null;

    this._closeDialogCancelDelegate = null;
    this._backLinkClickedDelegate = null;
    this._closeDialogSaveDelegate = null;
    this._beforeCloseDialogDelegate = null;
    this._itemSelectDelegate = null;
    this._fileChangedDelegate = null;
    this._fileUploadingDelegate = null;
    this._fileUploadedDelegate = null;
    this._valueChangedDelegate = null;
    this._loadDelegate = null;
    this._bodyCssClass = null;
    this._initialized = false;
    this._enableOneClickUpload = false;

    // delegate used the override the standart functionality
    this._customInsertDelegate = null;

    this._culture = null;
    this._uiCulture = null;
    this._clientLabelManager = null;

    this._maxFileSize = null;
    this._allowedExtensions = null;
    this._isFileUploading = null;

    this._permissions = null;
    this._isGrantedDictionary = null;
    this._provider = null;
    this._providersSelector = null;
    this._providerChangedDelegate = null;
    this._libraryNotSelectedErrorMessage = null;

    this._noLibrariesOkButton = null;
    this._selectorViewTitle = null;
    this._filterPanel = null;

    Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.prototype = {

    /* ************************* set up and tear down ************************* */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.callBaseMethod(this, 'initialize');

        this._closeDialogCancelDelegate = Function.createDelegate(this, this._closeDialogCancel);
        this._backLinkClickedDelegate = Function.createDelegate(this, this._backLinkClicked);
        this._closeDialogSaveDelegate = Function.createDelegate(this, this._closeDialogSave);
        this._itemSelectDelegate = Function.createDelegate(this, this._itemSelect);
        this._beforeCloseDialogDelegate = Function.createDelegate(this, this._beforeCloseDialogHandler);

        if (this.get_radWindow()) {
            this.get_radWindow().add_beforeClose(this._beforeCloseDialogDelegate);
        }

        this._fileChangedDelegate = Function.createDelegate(this, this._fileChanged);
        this._fileUploadingDelegate = Function.createDelegate(this, this._fileUploading);
        this._fileUploadedDelegate = Function.createDelegate(this, this._fileUploaded);

        //TODO: 1) finish the upload and select only modes
        //if (!(this._useOnlyUploadMode || this._useOnlySelectMode)) {
        //    this._valueChangedDelegate = Function.createDelegate(this, this._valueChanged);
        //    this.get_dialogModesSwitcher().add_valueChanged(this._valueChangedDelegate);
        //}

        this._loadDelegate = Function.createDelegate(this, this._load);

        if (this._providersSelector) {
            this._providerChangedDelegate = Function.createDelegate(this, this._handleProviderChanged);
            this._providersSelector.add_onProviderSelected(this._providerChangedDelegate);
        }

        if (this._bodyCssClass) {
            jQuery(this.get_element()).addClass(this._bodyCssClass);
        }

        if (this.get_noLibrariesOkButton()) {
            $addHandler(this.get_noLibrariesOkButton(), "click", this._closeDialogCancelDelegate);
        }

        if (this._filterPanel) {
            this._filterSelectedDelegate = Function.createDelegate(this, this._handleFilterSelected);
            this._filterPanel.add_onFilterSelectedCommand(this._filterSelectedDelegate);
        }

        jQuery(this.get_backLink()).hide();

        if (this._enableOneClickUpload) {
            this.set_mediaDialogOpenMode(0);
        }

        Sys.Application.add_load(this._loadDelegate);
        this._initialized = true;
    },

    dispose: function () {
        if (this._closeDialogCancelDelegate) {
            if (this.get_noLibrariesOkButton()) {
                $removeHandler(this.get_noLibrariesOkButton(), "click", this._closeDialogCancelDelegate);
            }
            delete this._closeDialogCancelDelegate;
        }
        if (this._backLinkClickedDelegate) {
            delete this._backLinkClickedDelegate;
        }
        if (this._closeDialogSaveDelegate) {
            delete this._closeDialogSaveDelegate;
        }
        if (this._itemSelectDelegate) {
            if (this.get_selectorView()) {
                this.get_selectorView().remove_onItemSelectCommand(this._itemSelectDelegate);
            }
            delete this._itemSelectDelegate;
        }
        if (this._beforeCloseDialogDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_beforeClose(this._beforeCloseDialogDelegate);
            }
            delete this._beforeCloseDialogDelegate;
        }
        if (this._fileChangedDelegate) {
            if (this.get_uploaderView()) {
                this.get_uploaderView().remove_onFileChanged(this._fileChangedDelegate);
            }
            delete this._fileChangedDelegate;
        }
        if (this._fileUploadingDelegate) {
            if (this.get_uploaderView()) {
                this.get_uploaderView().remove_onFileUploading(this._fileUploadingDelegate);
            }
            delete this._fileUploadingDelegate;
        }
        if (this._fileUploadedDelegate) {
            if (this.get_uploaderView()) {
                this.get_uploaderView().remove_onFileUploaded(this._fileUploadedDelegate);
            }
            delete this._fileUploadedDelegate;
        }
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._providersSelector) {
            this._providersSelector.remove_onProviderSelected(this._providerChangedDelegate);
            delete this._providerChangedDelegate;
        }
        if (this._filterPanel) {
            this._filterPanel.remove_onFilterSelectedCommand(this._filterSelectedDelegate);
            delete this._filterSelectedDelegate;
        }
        Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */

    showProvidersSelector: function () {
        if (this._providersSelector) {
            jQuery('#' + this._providersSelector.get_id()).show();
        }
    },

    hideProvidersSelector: function () {
        if (this._providersSelector) {
            jQuery('#' + this._providersSelector.get_id()).hide();
        }
    },

    rebind: function () {
        if (this.get_selectorView()) {
            this.get_selectorView().rebind();
        }
        if (this.get_uploaderView()) {
            this.get_uploaderView().rebind(this._provider);
        }
        if (this.get_filterPanel()) {
            this.get_filterPanel().rebind();
        }
    },

    reset: function () {
        jQuery(this.get_cancelLink()).show();
        jQuery(this.get_backLink()).hide();
        jQuery(this.get_buttonArea()).show();

        if (this._enableOneClickUpload)
            this.set_mediaDialogOpenMode(0);

        var panel = this.get_filterPanel();
        if (panel) {
            var commandsJElement = jQuery(panel.get_commandBar().get_element());
            commandsJElement.find("a.sfSelected").removeClass("sfSelected");
            commandsJElement.find("a.sfRecent").addClass("sfSelected");
            this._loadCurrentModeView(this.get_mediaDialogOpenMode());
            this.get_selectorView().rebind();
        }

        if (this.get_filterPanel())
            jQuery(this.get_filterPanel().get_element()).show();

        if (this.get_uploaderView()) {
            jQuery(this.get_uploaderView().get_fileUploadWrapper()).not(".sfLeftCol").addClass("sfLeftCol");
            this.get_uploaderView().reset();
        }

        if (this.get_filterPanel()) {
            this.get_filterPanel().get_librarySelector().dataBind();
        }

        this.set_selectedDataItem(null);
        this._configureButtonArea();

        this.resizeTopRadWindow();
    },

    setPreSelectedItems: function (ids) {
        this.get_selectorView().setPreSelectedItems(ids);
    },

    open: function () {
        var mode = this.get_mediaDialogOpenMode();
        this._loadCurrentModeView(mode);
    },

    /* ************************* events ************************* */

    /* ************************* event handlers ************************* */

    _load: function () {
        this._showPermittedViews(this._getProviderPermissions());

        var openMode = this.get_mediaDialogOpenMode();
        this._loadCurrentModeView(openMode);

        this._initializeParams();
        this._initializeHandlers();

        this._setAllowedExtensions();

        //resize the dialog according to the content
        this.resizeTopRadWindow();

        if (/chrome/.test(navigator.userAgent.toLowerCase()))
            jQuery("body").addClass("sfOverflowHiddenX");
    },

    _loadCurrentModeView: function (mediaDialogOpenMode) {
        var panel = this.get_filterPanel();
        if (panel) {
            var commandsJElement = jQuery(panel.get_commandBar().get_element());
            var upload = commandsJElement.find("a.sfUpload");

            if (this._enableOneClickUpload && upload && !upload.hasClass("sfOneClickUpload")) {
                upload.addClass("sfOneClickUpload");
            }

            var selected = commandsJElement.find("a.sfSelected");

            if (mediaDialogOpenMode == 1 && this._permissions.Upload) {
                //load upload mode                
                if (selected.hasClass("sfUpload")) {
                    this._switchToUploadMode();
                }
                else {
                    commandsJElement.find("a.sfSelected").removeClass("sfSelected");
                    panel.get_librarySelector().clearSelection();

                    upload.addClass("sfSelected");

                    this._handleFilterSelected(this, { get_commandName: function () { return "upload"; } });
                }
            }
            else {
                //load select mode
                if (selected.hasClass("sfRecent")) {
                    this._switchToSelectMode();
                }
                else {
                    commandsJElement.find("a.sfSelected").removeClass("sfSelected");
                    panel.get_librarySelector().clearSelection();

                    var recentItemsElement = commandsJElement.find("a.sfRecent");

                    //If the SourceLibraryId of the selector is set, it have to show items from the library with this id,
                    //the "RecentItems" filter should not be applied because it shows items from all libraries.
                    var sourceLibraryIsSet = this.get_sourceLibraryId() && this.get_sourceLibraryId() !== Telerik.Sitefinity.getEmptyGuid();
                    if (recentItemsElement && !sourceLibraryIsSet) {
                        recentItemsElement.addClass("sfSelected");

                        this._handleFilterSelected(this, { get_commandName: function () { return "showRecent"; } });
                    }
                    else {
                        this._switchToSelectMode();
                    }
                }
            }
        }

        if (this._useOnlySelectMode && this._currentMode != "2") {
            this._switchToSelectMode();
        }

        if (this._useOnlyUploadMode && this._currentMode != "1") {
            this._switchToUploadMode();
        }
    },

    _closeDialogCancel: function () {
        //if (!this._customInsertDelegate && this.get_dialogMode() == "1" && this.get_selectedDataItem() && this.get_editImageSection().style.display == "none") {
        //    this._switchToImageEditMode();
        //    return;
        //}
        this.close();
        this.resizeTopRadWindow();

        //IE FIX: Explorer calls window.onbeforeunload when hyperlink (<a>)  with href="javascript:..." is clicked
        //Returning false solves the issue.
        return false;
    },

    _backLinkClicked: function () {
        this.reset();
    },

    _closeDialogSave: function (sender, args) {
        var selectedDataItem;
        if (this._currentMode == "1") {
            if (!this.get_selectedDataItem()) {
                if (this.get_uploaderView()._targetLibraryId == Telerik.Sitefinity.getEmptyGuid() &&
                    (this.get_uploaderView().get_parentLibrarySelector().get_value() == null ||
                     this.get_uploaderView().get_parentLibrarySelector().get_value() == Telerik.Sitefinity.getEmptyGuid())) {
                    //we do not have set library
                    alert(this._libraryNotSelectedErrorMessage);
                    return;
                }
            }

            var uploadView = this.get_uploaderView();
            uploadView._saveMediaItems();
        }
        else {

            //The case where the file is selected from already uploaded files
            selectedDataItem = this.get_selectedDataItem();
            if (selectedDataItem) {

                //Validate file size
                //Get the size of the selected file in Bytes as the maxFileSize is in Bytes
                var selectedDataItemSize = selectedDataItem.TotalSize * 1024;
                if (this.get_maxFileSize() > 0 && selectedDataItemSize > this.get_maxFileSize()) {
                    var invalidSelectedFileSizeMessage = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "InvalidFileSizeAlertMessage"), (this.get_maxFileSize() / 1024));
                    alert(invalidSelectedFileSizeMessage);
                    return;
                }

                //Validate file extension
                var extension = selectedDataItem.Extension.substr(1);
                if (!this._validateFileExtension(extension)) {
                    var invalidFileFormatMessage = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "CantUploadFiles"), "." + extension, this.get_allowedExtensions());
                    alert(invalidFileFormatMessage);

                    return;
                }
            }

            this._insertItem();
            this.resizeTopRadWindow();
        }

        if (selectedDataItem) {
            this.close(selectedDataItem);
        }
    },

    _beforeCloseDialogHandler: function (sender, args) {
        if (this.get_isFileUploading()) {
            //if the file is uploading do not allow to close the dialog
            args.set_cancel(true);
        }
        else {
            args.set_cancel(false);
        }
    },

    _itemSelect: function (sender, args) {
        var dataItem = null;
        if (!args.get_dataItem().IsFolder) {
            dataItem = args.get_dataItem();
        }
        this.set_selectedDataItem(dataItem);
        this._configureButtonArea();
    },

    _fileChanged: function (sender, args) {
        if (args && args.files.length > 0) {
            // A file has been selected            

            // perform size validation
            if (this.get_maxFileSize() && this.get_maxFileSize() > 0) {
                var file = args.files[0];
                var selectedFileSize = file.size;
                if (selectedFileSize > this.get_maxFileSize()) {
                    var invalidUploadedFileSizeMessage = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "InvalidFileSizeAlertMessage"), (this.get_maxFileSize() / 1024));

                    alert(invalidUploadedFileSizeMessage);

                    args.preventDefault();
                    this.reset();
                    return;
                }
            }

            this._configureButtonArea(true);

            if (this.get_filterPanel())
                jQuery(this.get_filterPanel().get_element()).hide();

            jQuery(this.get_uploaderView().get_mediaItemFieldsControl().get_element()).show()
            jQuery(this.get_uploaderView().get_element()).find("span.sfFilesSelectedLbl").text("");
            jQuery(this.get_uploaderView().get_fileUploadWrapper()).removeClass("sfLeftCol");
            jQuery(this.get_cancelLink()).hide();
            jQuery(this.get_backLink()).show();
            this.resizeTopRadWindow();
        }
        else {
            // The selected file has been removed
            this.reset();
        }
    },

    _fileUploading: function (sender, args) {
        jQuery(this.get_uploaderView().get_mediaItemFieldsControl().get_element()).hide();
        jQuery(this.get_uploaderView().get_fileUploadWrapper()).not(".sfLeftCol").addClass("sfLeftCol");
        jQuery(this.get_buttonArea()).hide();
        jQuery(this._element).find(".k-upload-status").hide();
        this.set_isFileUploading(true);
        this.resizeTopRadWindow();
    },

    _fileUploaded: function (sender, args) {
        var uploadedItem = args;
        this.set_selectedDataItem(uploadedItem);
        this._insertItem();
        this.set_isFileUploading(false);
        this.reset();
    },

    resizeTopRadWindow: function () {
        var currentRadWindow = this.get_radWindow();
        if (currentRadWindow) {
            //var bounds = $telerik.getBounds(currentRadWindow.get_popupElement());

            var topRadWindow = this.get_radWindow().get_contentFrame().contentWindow.top.$find("PropertyEditorDialog");
            var isInMediaManager = this.get_radWindow().get_contentFrame().contentWindow.name === "imageSelector";
            var isInCustomFieldCreation = this.get_radWindow().get_contentFrame().contentWindow.name === "createCustomField";
            var isInBackendContentEditTextArea = this.get_radWindow().get_contentFrame().contentWindow.name === "Window";
            //when the dialog is hosted in designer, MediaContentManagerDialog, custom field creation or TextArea in the backend
            if (topRadWindow || isInMediaManager || isInCustomFieldCreation || isInBackendContentEditTextArea) {
                setTimeout(function () {
                    //currentRadWindow.set_modal(false);
                    currentRadWindow.AjaxDialog.resizeToContent();
                    if (currentRadWindow.isVisible()) {
                        //currentRadWindow.set_modal(true);
                        //currentRadWindow._afterShow();
                        //currentRadWindow.AjaxDialog.get_radWindow().center()
                        //currentRadWindow.moveTo(bounds.x, bounds.y);
                    }
                    if (topRadWindow) {
                        topRadWindow.AjaxDialog.resizeToContent();
                        //topRadWindow.AjaxDialog.get_radWindow().center();
                        //topRadWindow.AjaxDialog.get_radWindow()._popupElement.style.top = "50px";
                    }
                }, 0);
            }
        }
    },

    _handleProviderChanged: function (sender, args) {
        var providerName = args.ProviderName;
        if (this._provider != providerName) {
            this._provider = providerName;
            //refresh uploader view
            if (this.get_uploaderView()) {
                this.get_uploaderView().rebind(providerName);
            }
            //refresh selector view
            if (this.get_selectorView()) {
                this.get_selectorView()._provider = providerName;
                this.get_selectorView()._initializeLists();
            }
            //refresh filter panel
            if (this.get_filterPanel()) {
                this.get_filterPanel()._provider = providerName;
                this.get_filterPanel().rebind();
            }
            //refresh ui based on the permissions
            this._showPermittedViews(this._getProviderPermissions());

            this.resizeTopRadWindow();
        }
    },

    _handleFilterSelected: function (sender, args) {
        if (args && args.get_commandName && args.get_commandName()) {
            jQuery(this.get_filterPanel().get_librarySelector().clearSelection());
        }

        if (!args && this.get_filterPanel()) {
            jQuery(this.get_filterPanel().get_commandBar().get_element()).find(".sfShowAll").click();
            return;
        }
        else if (args.get_commandName && args.get_commandName() === "upload") {
            this._switchToUploadMode();
            return;
        }
        else {
            this._switchToSelectMode();
            if (this.get_selectorView()) {
                this.get_selectorView()._selectionChanged(sender, args);
            }
        }

        this.set_selectedDataItem(null);
        this._configureButtonArea();
    },

    /* ************************* event firing ************************* */


    /* ************************* private methods ************************* */

    _initializeParams: function () {
        var win = this.get_radWindow();
        if (win) {
            this._domElementToInsert = win.ClientParameters;
        }
    },

    _initializeHandlers: function () {
        jQuery(this.get_cancelLink()).click(this._closeDialogCancelDelegate);
        jQuery(this.get_backLink()).click(this._backLinkClickedDelegate);

        if (this.get_selectorView()) {
            this.get_selectorView().add_onItemSelectCommand(this._itemSelectDelegate);
        }
        if (this.get_uploaderView()) {
            this.get_uploaderView().add_onFileChanged(this._fileChangedDelegate);
            this.get_uploaderView().add_onFileUploading(this._fileUploadingDelegate);
            this.get_uploaderView().add_onFileUploaded(this._fileUploadedDelegate);
        }
    },

    _setAllowedExtensions: function () {
        if (this._allowedExtensions && this.get_uploaderView()) {
            this.get_uploaderView().set_allowedExtensions(this._allowedExtensions);
        }
    },

    _getSelectedDataItem: function () {
        var selectedDataItem = this.get_selectedDataItem();
        if (!selectedDataItem) {
            throw "No data item selected";
        }
        return selectedDataItem;
    },

    _switchToUploadMode: function () {
        this._currentMode = "1";

        this.showProvidersSelector();
        jQuery(this.get_uploaderSection()).show();
        jQuery(this.get_selectorSection()).hide();

        var lblManager = this.get_clientLabelManager();
        this.set_saveLinkTitle(String.format(lblManager.getLabel("Labels", "Upload"), ""));
        this._setTitleLabel();

        if (this._enableOneClickUpload && this._initialized) {
            var fileUploadCtrl = this.get_uploaderView().get_fileUpload();
            this._uploadToClick = jQuery('[type="file"]', fileUploadCtrl.get_element()).last();

            this._uploadToClick.click();
        }
    },

    _switchToSelectMode: function () {
        this.get_selectorView()._preSelectItems();

        this._currentMode = "2";

        this.showProvidersSelector();
        jQuery(this.get_uploaderSection()).hide();
        jQuery(this.get_selectorSection()).show();

        var lblManager = this.get_clientLabelManager();
        this.set_saveLinkTitle(lblManager.getLabel("Labels", "Done"));
        if (this._selectorViewTitle) {
            this.set_title(this._selectorViewTitle);
        } else {
            this._setTitleLabel();
        }
    },

    _setTitleLabel: function () {
        var label = "";
        var lblManager = this.get_clientLabelManager();
        if (this.get_dialogMode() === 1) {
            label = lblManager.getLabel("ImagesResources", "SelectAnImage")
        }
        else if (this.get_dialogMode() === 2) {
            label = lblManager.getLabel("DocumentsResources", "SelectDocument")
        }
        else if (this.get_dialogMode() === 3) {
            label = lblManager.getLabel("VideosResources", "SelectVideo")
        }

        this.set_title(label);
    },

    _configureButtonArea: function (enable) {
        var toEnable = enable || !!this.get_selectedDataItem();
        this._configureLinkButton(toEnable);
    },

    _configureLinkButton: function (enabled) {
        var saveLinkButton = jQuery(this.get_saveLink());
        saveLinkButton.unbind('click');
        if (enabled) {
            saveLinkButton.removeClass("sfDisabledLinkBtn");
            saveLinkButton.click(this._closeDialogSaveDelegate);
        }
        else {
            saveLinkButton.addClass("sfDisabledLinkBtn");
        }
    },

    _insertItem: function () {
        if (this._customInsertDelegate) {
            this._customInsertDelegate(this._getSelectedDataItem());
            return;
        }

        var selectedDataItem = this._getSelectedDataItem();
        this.set_provider(selectedDataItem.ProviderName);
        this.hideProvidersSelector();
    },

    _setResizingOptionsControl: function (resizingOptionsControl, width) {
        if (width && width != "") {
            resizingOptionsControl._resizeSettingsExpanderClickHandler();
            resizingOptionsControl.get_resizeChoiceField().set_value("resize");
            if (resizingOptionsControl.get_sizesChoiceField()._get_listItemByValue(width).length > 0) {
                resizingOptionsControl.get_sizesChoiceField().set_value(width);
            }
            else {
                resizingOptionsControl.get_sizesChoiceField().set_value("custom");
                resizingOptionsControl.get_customWidthTextField().set_value(width);
            }
        }
    },

    /* ************************* Image specific methods ************************* */

    _changeImageClick: function (sender, args) {
        this.showProvidersSelector();
        this.resizeToContent();
    },

    _validateFileExtension: function (extension) {
        //if file extension is not specified => do not perform validation for file extension
        var doNotValidate = !this._allowedExtensions;
        return doNotValidate || this._allowedExtensions.toLowerCase().indexOf(extension.toLowerCase()) >= 0;
    },
    //returns a bool value that indicates whether the Upload input containing the fileName is empty
    _isUploadFileNameTxtSet: function () {
        return this.get_uploaderView() && !!this.get_uploaderView().get_fileNameTextBox().value;
    },

    _showPermittedViews: function (permissions) {
        this._permissions = permissions;

        if (this.get_filterPanel()) {
            this.get_filterPanel().toggleUploadOption(permissions.Upload);
        }
        if (!permissions.Upload) {
            this._switchToSelectMode();
        }

        if (this.get_uploaderView()) {
            this.get_uploaderView().allowCreateNewLibrary(permissions.CreateLibrary);
        }
    },

    _getProviderPermissions: function () {
        var permissions = this._isGrantedDictionary[this._provider];
        return Sys.Serialization.JavaScriptSerializer.deserialize(permissions);
    },

    /* ************************* properties ************************* */

    get_uploaderView: function () {
        return this._uploaderView;
    },
    set_uploaderView: function (value) {
        this._uploaderView = value;
    },

    get_selectorView: function () {
        if (this._selectorView) {
            this._selectorView.set_uiCulture(this.get_uiCulture());
            this._selectorView.set_culture(this.get_culture());
        }
        return this._selectorView
    },
    set_selectorView: function (value) {
        this._selectorView = value;
    },

    get_saveLink: function () {
        return this._saveLink
    },
    set_saveLink: function (value) {
        this._saveLink = value;
    },
    set_saveLinkTitle: function (value) {
        jQuery(this.get_saveLink()).find(".sfLinkBtnIn").text(value);
    },

    get_cancelLink: function () {
        return this._cancelLink
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },

    get_backLink: function () {
        return this._backLink
    },
    set_backLink: function (value) {
        this._backLink = value;
    },

    get_buttonArea: function () {
        return this._buttonArea;
    },
    set_buttonArea: function (value) {
        this._buttonArea = value;
    },

    get_titleLabel: function () {
        return this._titleLabel
    },
    set_titleLabel: function (value) {
        this._titleLabel = value;
    },
    set_title: function (value) {
        if (value) this.get_titleLabel().innerHTML = value;
    },

    get_cancelLinkTitle: function () {
        return this._cancelLinkTitle
    },
    set_cancelLinkTitle: function (value) {
        this._cancelLinkTitle = value;
    },

    get_backLinkTitle: function () {
        return this._backLinkTitle
    },
    set_backLinkTitle: function (value) {
        this._backLinkTitle = value;
    },

    get_dialogMode: function () {
        return this._dialogMode
    },
    set_dialogMode: function (value) {
        this._dialogMode = value;
    },

    get_mediaDialogOpenMode: function () {
        return this._mediaDialogOpenMode
    },
    set_mediaDialogOpenMode: function (value) {
        this._mediaDialogOpenMode = value;
    },

    get_selectedDataItem: function () {
        return this._selectedDataItem;
    },
    set_selectedDataItem: function (value) {
        this._selectedDataItem = value;
    },

    get_uploaderSection: function () {
        return this._uploaderSection;
    },
    set_uploaderSection: function (value) {
        this._uploaderSection = value;
    },

    get_selectorSection: function () {
        return this._selectorSection;
    },
    set_selectorSection: function (value) {
        this._selectorSection = value;
    },

    set_customInsertDelegate: function (insertDelegate) {
        this._customInsertDelegate = insertDelegate;
    },

    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this._culture = culture;
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this._culture;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    },

    get_libraryNotSelectedErrorMessage: function () {
        return this._libraryNotSelectedErrorMessage;
    },
    set_libraryNotSelectedErrorMessage: function (value) {
        this._libraryNotSelectedErrorMessage = value;
    },

    //Client side localization control
    get_clientLabelManager: function () { return this._clientLabelManager; },
    set_clientLabelManager: function (value) { this._clientLabelManager = value; },

    //Image specific properties
    get_maxFileSize: function () { return this._maxFileSize; },
    set_maxFileSize: function (value) { this._maxFileSize = value; },

    get_allowedExtensions: function () { return this._allowedExtensions; },
    set_allowedExtensions: function (value) { this._allowedExtensions = value; },

    get_isFileUploading: function () { return this._isFileUploading; },
    set_isFileUploading: function (value) { this._isFileUploading = value; },

    get_provider: function () { return this._provider; },
    set_provider: function (value) { this._provider = value; },

    get_providersSelector: function () { return this._providersSelector; },
    set_providersSelector: function (value) { this._providersSelector = value },

    get_isGrantedDictionary: function () { return this._isGrantedDictionary; },
    set_isGrantedDictionary: function (value) { this._isGrantedDictionary = value; },

    get_noLibrariesOkButton: function () { return this._noLibrariesOkButton; },
    set_noLibrariesOkButton: function (value) { this._noLibrariesOkButton = value; },

    get_filterPanel: function () { return this._filterPanel; },
    set_filterPanel: function (value) { this._filterPanel = value; },

    get_filterSection: function () {
        return this._filterSection;
    },
    set_filterSection: function (value) {
        this._filterSection = value;
    },

    get_sourceLibraryId: function () {
        return this._sourceLibraryId;
    },
    set_sourceLibraryId: function (value) {
        this._sourceLibraryId = value;
    }
};
Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog.registerClass('Telerik.Sitefinity.Web.UI.SingleMediaContentItemDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
