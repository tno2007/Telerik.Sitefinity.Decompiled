Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._allDocumentsRadioClientID = null;
    this._libraryRadioClientID = null;
    this._uploadRadioClientID = null;
    this._allDocumentsRadio = null;
    this._libraryRadio = null;
    this._uploadRadio = null;
    this._selectLibraryButtonClientID = null;
    this._selectLibraryButton = null;
    this._uploadButton = null;
    this._folderSelector = null;

    this._sortExpressionChoiceField = null;
    this._uploadDialogUrl = null;
    this._backPhrase = null;
    this._isInAdvancedMode = null;
    this._propertyEditor = null;
    this._uploadDialog = null;
    this._filterSelector = null;
    this._btnNarrowSelection = null;
    this._narrowSelection = null;

    this._selectedLibrary = null;
    this._selectedLibraryTitle = null;
    this._selectedLibraryId = null;
    this._haveLibraries = null;

    this._uploadButtonClickDelegate = null;
    this._uploadDialogPageLoadDelegate = null;
    this._uploadDialogCloseDelegate = null;
    this._doneSelectingLibraryDelegate = null;
    this._beforeDialogCloseDelegate = null;
    this._filterRadioClickDelegate = null;
    this._btnNarrowSelectionClickDelegate = null;

    this._selectorTag = null;
    this._dialog = null;

    this._providersSelector = null;
    this._providersSelectorClickedDelegate = null;
    this._noContentSelectedLabel = null;
    this._selectLibraryButtonWrapper = null;

    this._includesChildLibraryItemsLabel = null;
    this._includesChildLibraryItems = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView.callBaseMethod(this, "initialize");

        this._isInAdvancedMode = false;
        this._allDocumentsRadio = $get(this._allDocumentsRadioClientID);
        this._libraryRadio = $get(this._libraryRadioClientID);

        this._selectLibraryButtonClientID = "selectAlbumButton";
        this._selectLibraryButton = $get(this._selectLibraryButtonClientID);

        $addHandler(this._libraryRadio, 'click', Function.createDelegate(this, this._radioClicked));
        $addHandler(this._allDocumentsRadio, 'click', Function.createDelegate(this, this._radioClicked));

        $addHandler(this._selectLibraryButton, 'click', Function.createDelegate(this, this._openAlbumSelector));


        this._uploadRadio = $get(this._uploadRadioClientID);
        $addHandler(this._uploadRadio, 'click', Function.createDelegate(this, this._radioClicked));
        this._uploadButtonClickDelegate = Function.createDelegate(this, this._uploadButtonClickHandler);
        $addHandler(this._uploadButton, "click", this._uploadButtonClickDelegate);

        this._doneSelectingLibraryDelegate = Function.createDelegate(this, this._selectAlbum);
        this._folderSelector.add_doneClientSelection(this._doneSelectingLibraryDelegate);

        this._beforeDialogCloseDelegate = Function.createDelegate(this, this._beforeDialogCloseHandler);
        this._propertyEditor = this.get_parent().get_parent().get_parent().get_parent(); // Getting the property editor for this view.
        this._propertyEditor.get_radWindow().add_beforeClose(this._beforeDialogCloseDelegate);

        this._filterRadioClickDelegate = Function.createDelegate(this, this._setContentFilter);
        this.get_radioChoices().click(this._filterRadioClickDelegate);

        if (this._btnNarrowSelection) {
            this._btnNarrowSelectionClickDelegate = Function.createDelegate(this, this._btnNarrowSelectionClick);
            $addHandler(this._btnNarrowSelection, "click", this._btnNarrowSelectionClickDelegate);
        }
        if (this._providersSelector) {
            this._providersSelectorClickedDelegate = Function.createDelegate(this, this._handleProvidersChanged);
            this._providersSelector.add_onProviderSelected(this._providersSelectorClickedDelegate);
        }
        this._dialog = jQuery(this._selectorTag).dialog({
            autoOpen: false,
            modal: false,
            width: 355,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexL"
            }
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView.callBaseMethod(this, "dispose");
        if (this._doneSelectingLibraryDelegate) {
            this.get_folderSelector().remove_doneClientSelection(this._doneSelectingLibraryDelegate);
            delete this._doneSelectingLibraryDelegate;
        }

        if (this._uploadButtonClickDelegate) {
            if (this._uploadButton) {
                $removeHandler(this._uploadButton, "click", this._uploadButtonClickDelegate);
            }
            delete this._uploadButtonClickDelegate;
        }

        if (this._beforeDialogCloseDelegate) {
            this._propertyEditor.get_radWindow().remove_beforeClose(this._beforeDialogCloseDelegate);
            delete this._beforeDialogCloseDelegate;
        }

        if (this._uploadDialogPageLoadDelegate) {
            delete this._uploadDialogPageLoadDelegate;
        }

        if (this._uploadDialogCloseDelegate) {
            this._uploadDialog.remove_beforeClose(this._uploadDialogCloseDelegate);
            delete this._uploadDialogCloseDelegate;
        }
        if (this._filterRadioClickDelegate) {
            this.get_radioChoices().unbind("click", this._filterRadioClickDelegate);
            delete this._filterRadioClickDelegate;
        }
        if (this._btnNarrowSelection) {
            $removeHandler(this._btnNarrowSelection, "click", this._btnNarrowSelectionClickDelegate);
            delete this._btnNarrowSelectionClickDelegate;
        }
        if (this._providersSelector) {
            this._providersSelector.remove_onProviderSelected(this._providersSelectorClickedDelegate);
            delete this._providersSelectorClickedDelegate;
        }
    },

    // ----------------------------------------------- Private functions ----------------------------------------------

    _resolvePropertyPath: function (property) {
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        var propertyPath = viewPath + "." + property;
        return propertyPath;
    },

    _handleProvidersChanged: function (sender, args) {
        var controlData = this.get_controlData();
        var oldProviderName = controlData.ControlDefinition.ProviderName;
        if (controlData.ControlDefinition.hasOwnProperty('ProviderName')) {
            controlData.ControlDefinition.ProviderName = args.ProviderName;
        }
        this.get_folderSelector().set_providerName(args.ProviderName);
        this.get_folderSelector().dataBind();
        if (args.ProviderName != oldProviderName) {
            this._resetViewOnProvidersChanged();
        }
        dialogBase.resizeToContent();
    },

    _resetViewOnProvidersChanged: function () {
        // select show all items
        this._allDocumentsRadio.click();
        $(this._allDocumentsRadio).attr('checked', true);

        // set default title
        $(this.get_selectedLibrary()).text(this._noContentSelectedLabel);
        this._selectedLibraryTitle = null;

        //clear selected library id
        this.get_currentMasterView().ItemsParentId = Telerik.Sitefinity.getEmptyGuid();
        this._selectedLibraryId = Telerik.Sitefinity.getEmptyGuid();

        // show libliray selector button
        if (this.get_selectLibraryButtonWrapper()) {
            $(this.get_selectLibraryButtonWrapper()).show();
        }
    },

    /* --------------------------------- event handlers --------------------------------- */

    _uploadButtonClickHandler: function (e) {
        var dialogManager = GetDialogManager();
        var dialog = dialogManager.getDialogByName("uploadDialog");
        this._uploadDialog = dialog;
        var providerName = this.get_folderSelector().get_providerName();
        var params = { 'IsEditable': true, 'providerName': providerName };
        var dialogContext = this._getDialogContext("upload", null, null, dialog, params, "uploadDialog");
        dialog.SetUrl(this._uploadDialogUrl);
        if (this._uploadDialogPageLoadDelegate == null) {
            this._uploadDialogPageLoadDelegate = Function.createDelegate(this, this._uploadDialogPageLoad);
            dialog.add_pageLoad(this._uploadDialogPageLoadDelegate);
        }
        if (this._uploadDialogCloseDelegate == null) {
            this._uploadDialogCloseDelegate = Function.createDelegate(this, this._uploadDialogClose);
            dialog.add_beforeClose(this._uploadDialogCloseDelegate);
        }
        this._propertyEditor.set_forceReload(true);
        dialogManager.openDialog("uploadDialog", this, dialogContext);
        if (!dialog.isMaximized())
            dialog.maximize();
        this._isInAdvancedMode = true;
    },

    _uploadDialogPageLoad: function (sender, args) {
        //sender.AjaxDialog.get_backButton().textContent = this._backPhrase;
        //sender._iframe.contentWindow.jQuery("#" + sender.AjaxDialog.get_successCommandBar()._commands[0].TextClientId).html(this._backPhrase);
        sender._iframe.contentWindow.jQuery(".sfBack").html(this._backPhrase);
    },

    _uploadDialogClose: function (sender, args) {
        this._isInAdvancedMode = false;
    },

    //TODO remove when createDialog() arguments refactoring is done;
    _getDialogContext: function (commandName, dataItem, itemsList, dialog, params, key, commandArgument, additionalProperties) {
        if (additionalProperties) {
            //Merging the additionalProperties in the commandArgument object.
            commandArgument = jQuery.extend(true, commandArgument, additionalProperties);
        }
        return { commandName: commandName, dataItem: dataItem, itemsList: itemsList, dialog: dialog, params: params, key: key, commandArgument: commandArgument };
    },

    //fired before closing of the dialog
    _beforeDialogCloseHandler: function (sender, args) {
        // in advanced mode close switches to simple mode
        if (this._isInAdvancedMode) {
            args.set_cancel(true);
            var dialogManager = GetDialogManager();
            var dialog = dialogManager.getDialogByName("uploadDialog");
            dialog.Close();
            this._isInAdvancedMode = false;
        }
    },

    _openAlbumSelector: function () {
        var parentId = this.get_currentMasterView().ItemsParentId;
        if (parentId != Telerik.Sitefinity.getEmptyGuid()) {
            this.get_folderSelector().get_foldersGenericSelector().set_selectedItemIds([parentId]);
        }
        this.get_folderSelector().set_includesChildLibraryItems(this._includesChildLibraryItems);
        this.get_folderSelector().dataBind();
        this._dialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    // handles the content selected event of the content selector
    _selectAlbum: function (args) {
        this._dialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        //If cancel is clicked then args are null.
        if (args === null)
            return;

        var selectedItems = this.get_folderSelector().get_foldersGenericSelector().get_selectedItems();
        if (selectedItems != null) {
            if (selectedItems.length > 0) {
                var selectedTitle = selectedItems[0].Path != null ? selectedItems[0].Path : selectedItems[0].Title;
                if (selectedTitle.hasOwnProperty('Value')) {
                    this.get_selectedLibrary().innerHTML = selectedTitle.Value;
                } else {
                    this.get_selectedLibrary().innerHTML = selectedTitle;
                }
                jQuery(this.get_selectedLibrary()).show();
                this._selectedLibraryId = selectedItems[0].Id;
                this._includesChildLibraryItems = this.get_folderSelector().get_includesChildLibraryItems();
                if (this.get_includesChildLibraryItemsLabel()) {
                    jQuery(this.get_includesChildLibraryItemsLabel()).toggle(this._includesChildLibraryItems);
                }
            }
        }
    },

    _radioClicked: function (sender, args) {
        switch (sender.target) {
            case this._libraryRadio:
                $("#uploadWrapper").hide();
                $("#selectAlbumWrapper").show();
                if (this._selectedLibraryTitle) {
                    this.get_selectedLibrary().innerHTML = this._selectedLibraryTitle;
                }
                break;
            case this._allDocumentsRadio:
                $("#selectAlbumWrapper").hide();
                $("#uploadWrapper").hide();
                break;
            case this._uploadRadio:
                $("#selectAlbumWrapper").hide();
                $("#uploadWrapper").show();
                break;
        }
        dialogBase.resizeToContent();
    },

    _getCurrentView: function (parent) {
        var settingsDesignerViewId = parent.get_designerViewsMap()["DownloadListSettingsDesignerView"];
        var settingsDesignerView = $find(settingsDesignerViewId);
        if (settingsDesignerView) {
            return settingsDesignerView.get_currentView();
        }

        return null;
    },

    // ----------------------------------------------- Public functions -----------------------------------------------

    // forces the designer to refresh the UI from the control Data
    refreshUI: function () {
        this._refreshing = true;
        var controlData = this.get_controlData();
        if (!controlData) {
            return;
        }

        var currentView = this.get_currentMasterView();
        this._includesChildLibraryItems = currentView.IncludeDescendantItems === null || currentView.IncludeDescendantItems === true;
        if (currentView.ItemsParentId == Telerik.Sitefinity.getEmptyGuid()) {
            this._allDocumentsRadio.click();
        }
        else {
            this._libraryRadio.click();
            this.get_selectedLibrary().innerHTML = this._selectedLibraryTitle;
            jQuery(this.get_selectedLibrary()).show();
            if (this.get_includesChildLibraryItemsLabel()) {
                jQuery(this.get_includesChildLibraryItemsLabel()).toggle(this._includesChildLibraryItems);
            }
        }

        this._refreshFilterUI();

        if (this._sortExpressionChoiceField._get_listItemByValue(currentView.SortExpression).length == 0) {
            this._sortExpressionChoiceField.set_value("custom");
        }
        else {
            this._sortExpressionChoiceField.set_value(currentView.SortExpression);
        }

        if (this._haveLibraries == false) {
            jQuery(this._selectLibraryButton).hide();
        }

        this._refreshing = false;
    },

    // forces the designer to apply the changes on UI to the control Data
    applyChanges: function () {
        var parent = this.get_parentDesigner();
        var currentView = this._getCurrentView(parent);

        if (this._libraryRadio.checked) {
            if (this._selectedLibraryId) {
                currentView.ItemsParentId = this._selectedLibraryId;
                currentView.IncludeDescendantItems = this._includesChildLibraryItems;
            }
        }
        else {
            currentView.ItemsParentId = Telerik.Sitefinity.getEmptyGuid();
        }

        // construct sort expression and apply it to the selected master view
        var sortExpression = this.get_sortExpressionChoiceField().get_value();
        if (sortExpression != "custom") {
            currentView.SortExpression = sortExpression;
        }

        this._applyFilterChanges(currentView);
    },

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return this.get_controlData().MasterViewName;
    },

    // sets the content filter setting based on the radio button that selected the filter type
    _setContentFilter: SelectorDesignerView$setContentFilter,

    _refreshFilterUI: SelectorDesignerView$refreshFilterUI,

    _applyFilterChanges: SelectorDesignerView$applyFilterChanges,

    _btnNarrowSelectionClick: function () {
        jQuery(this._narrowSelection).toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    // gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        if (this._parentDesigner != value) {
            this._parentDesigner = value;
            this.raisePropertyChanged("parentDesigner");
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    // gets all the radio buttons in the container of this control with group name 'ContentSelection'
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find(':radio[name$=ContentSelection]'); // finds radio buttons with names ending with 'ContentSelection'
        }
        return this._radioChoices;
    },

    // gets the name of the currently selected master view of the content view control
    get_currentMasterViewName: function () {
        return (this._currentMasterViewName) ? this._currentMasterViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentMasterView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentMasterViewName()];
    },

    get_folderSelector: function () {
        return this._folderSelector;
    },

    set_folderSelector: function (value) {
        this._folderSelector = value;
    },

    get_selectedLibrary: function () {
        return this._selectedLibrary;
    },

    set_selectedLibrary: function (value) {
        if (this._selectedLibrary != value) {
            this._selectedLibrary = value;
            this.raisePropertyChanged("selectedLibrary");
        }
    },

    // Gets button that opens the upload screen
    get_uploadButton: function () {
        return this._uploadButton;
    },

    // Sets button that opens the upload screen
    set_uploadButton: function (value) {
        this._uploadButton = value;
    },

    // Gets the choice field with the different sorting options
    get_sortExpressionChoiceField: function () {
        return this._sortExpressionChoiceField;
    },

    // Sets the choice field with the different sorting options
    set_sortExpressionChoiceField: function (value) {
        this._sortExpressionChoiceField = value;
    },

    //gets refernce to the filter selector
    get_filterSelector: function () {
        return this._filterSelector;
    },

    //sets refernce to the filter selector
    set_filterSelector: function (value) {
        this._filterSelector = value;
    },

    get_btnNarrowSelection: function () {
        return this._btnNarrowSelection;
    },
    set_btnNarrowSelection: function (value) {
        this._btnNarrowSelection = value;
    },
    get_narrowSelection: function () {
        return this._narrowSelection;
    },
    set_narrowSelection: function (value) {
        this._narrowSelection = value;
    },

    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    },
    get_providersSelector: function () {
        return this._providersSelector;
    },
    set_providersSelector: function (value) {
        this._providersSelector = value;
    },

    get_noContentSelectedLabel: function () {
        return this._noContentSelectedLabel;
    },
    set_noContentSelectedLabel: function (value) {
        this._noContentSelectedLabel = value;
    },
    get_selectLibraryButtonWrapper: function () {
        return this._selectLibraryButtonWrapper;
    },
    set_selectLibraryButtonWrapper: function (value) {
        this._selectLibraryButtonWrapper = value;
    },

    get_includesChildLibraryItemsLabel: function () {
        return this._includesChildLibraryItemsLabel;
    },
    set_includesChildLibraryItemsLabel: function (value) {
        this._includesChildLibraryItemsLabel = value;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSelectorDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
