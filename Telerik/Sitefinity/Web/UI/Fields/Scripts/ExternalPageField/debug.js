/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ExternalPageField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ExternalPageField.initializeBase(this, [element]);

    this._element = element;
    this._clientLabelManager = null;
    this._currentExternalLinkLabel = null;
    this._isExternalPageChoiceField = null;
    this._externalPageControlsGroup = null;
    this._openDialogLink = null;
    this._viewExternalPageLink = null;
    this._dialogBoxDiv = null;
    this._dialog = null;
    this._selectedRadioIndex = 0;
    this._originalGridFilter = null;
    this._originalTreeFilter = null;
    this._originalFilterSet = false;

    this._dataItem = null;

    this._pageTypeSelector = null;
    this._optionsMultipage = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._internalPageSelector = null;
    this._internalPageFullUrl = null;
    this._externalPageSelector = null;

    this._isExternalPageChoiceFieldValueChangedDelegate = null;
    this._pageTypeSelectorValueChangedDelegate = null;
    this._radioClickedDelegate = null;
    this._doneSelectingDelegate = null;
    this._cancelDelegate = null;
    this._viewThisPageDelegate = null;
    this._siteRootPath = null;

    this.InternalPageID = "InternalPageChoice";
    this.ExternalPageID = "ExternalPageChoice";
    this.EmptyGuid = "00000000-0000-0000-0000-000000000000";
}

Telerik.Sitefinity.Web.UI.Fields.ExternalPageField.prototype =
{
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ExternalPageField.callBaseMethod(this, 'initialize');

        this._dialog = jQuery(this._dialogBoxDiv).dialog({
            autoOpen: false,
            modal: true,
            width: 440,
            height: "auto",
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfSelectorDialog sfZIndexL"
            }
        });

        this._isExternalPageChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._isExternalPageChoiceFieldValueChangedHandler);
        this._isExternalPageChoiceField.add_valueChanged(this._isExternalPageChoiceFieldValueChangedDelegate);

        this._openPopupDelegate = Function.createDelegate(this, this._onOpenPopupClick);
        $addHandler(this._openDialogLink, "click", this._openPopupDelegate);

        this._pageTypeSelectorValueChangedDelegate = Function.createDelegate(this, this._pageTypeSelectorValueChangedHandler);
        this._pageTypeSelector.add_valueChanged(this._pageTypeSelectorValueChangedDelegate);

        this._doneSelectingDelegate = Function.createDelegate(this, this._onDoneSelecting);
        $addHandler(this._doneButton, "click", this._doneSelectingDelegate);

        this._cancelDelegate = Function.createDelegate(this, this._onCancel);
        $addHandler(this._cancelButton, "click", this._cancelDelegate);

        this._viewThisPageDelegate = Function.createDelegate(this, this._onViewThisPage);
        $addHandler(this._viewExternalPageLink, "click", this._viewThisPageDelegate);

        this._internalPageSelector.set_bindOnLoad(false);
    },

    dispose: function () {
        if (this._isExternalPageChoiceFieldValueChangedDelegate) {
            if (this._isExternalPageChoiceField) {
                this._isExternalPageChoiceField.remove_valueChanged(this._isExternalPageChoiceFieldValueChangedDelegate);
            }
            delete this._isExternalPageChoiceFieldValueChangedDelegate;
        }

        if (this._openPopupDelegate) {
            $removeHandler(this._openDialogLink, "click", this._openPopupDelegate);
            delete this._openPopupDelegate;
        }

        if (this._pageTypeSelectorValueChangedDelegate) {
            if (this._pageTypeSelector) {
                this._pageTypeSelector.remove_valueChanged(this._pageTypeSelectorValueChangedDelegate);
            }
            delete this._pageTypeSelectorValueChangedDelegate;
        }

        if (this._doneSelectingDelegate) {
            $removeHandler(this.get_doneButton(), "click", this._doneSelectingDelegate);
            delete this._doneSelectingDelegate;
        }

        if (this._cancelDelegate) {
            $removeHandler(this.get_cancelButton(), "click", this._cancelDelegate);
            delete this._cancelDelegate;
        }

        if (this._viewThisPageDelegate) {
            $removeHandler(this.get_viewExternalPageLink(), "click", this._viewThisPageDelegate);
            delete this._viewThisPageDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.ExternalPageField.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    //IRequiresDataItem inteface implementation
    set_dataItem: function (value, isDefault) {
        // if this is the default value then ignore it. a non default value should always be passed
        if (isDefault)
            return;
        this._dialog.dialog("close");
        this._dataItem = value;
        if (value.IsExternal) {
            this._isExternalPageChoiceField.set_value("true");
            jQuery(this._openDialogLink).html(this._clientLabelManager.getLabel("PageResources", "EditPageToRedirectTo"));
            jQuery(this._viewExternalPageLink).show();

            if (value.RedirectUrl != "") {
                this._selectedRadioIndex = 1;
                jQuery(this._currentExternalLinkLabel).text(value.RedirectUrl);
            }
            else {
                this._selectedRadioIndex = 0;
                this._internalPageFullUrl = value.LinkedNodeFullUrl;
                if (value.LinkedNodeTitle == null || value.LinkedNodeTitle == "") {
                    var encoded = this._clientLabelManager.getLabel("PageResources", "NotSetPageTitle").htmlEncode();
                    jQuery(this._currentExternalLinkLabel).html("<i>" + encoded + "</i>");
                }
                else {
                    jQuery(this._currentExternalLinkLabel).text(value.LinkedNodeTitle);
                }
            }
        }
        else {
            this._dataItem.LinkedNodeId = this.EmptyGuid;
            this._dataItem.RedirectUrl = "";

            this._isExternalPageChoiceField.set_value("");
            jQuery(this._currentExternalLinkLabel).text("");
            jQuery(this._openDialogLink).html(this._clientLabelManager.getLabel("PageResources", "SetPageToRedirectTo"));
            jQuery(this._viewExternalPageLink).hide();
            this._externalPageSelector.get_urlTextBox().set_value("http://");
            this._externalPageSelector.get_openInNewWindowChoiceField().set_value("");
        }

        this._setCultures();
        this._setFilter();
    },

    validate: function () {
        var isValid = true;
        if (this._isExternalPageChoiceField.get_value() == "true") {
            if (this._selectedRadioIndex == 0) {
                if (this._dataItem.LinkedNodeId == "" || this._dataItem.LinkedNodeId == this.EmptyGuid)
                    isValid = false;
            }
            else {
                if (this._dataItem.RedirectUrl == "" || this._dataItem.RedirectUrl == null)
                    isValid = false;
            }
        }

        var addWrappingErrorCssClass = this._controlErrorCssClass.length > 0;
        var violationMessageElement = this._validator.get_violationMessageElement();
        jQuery(violationMessageElement).html(this._validator.get_requiredViolationMessage());
        if (isValid) {
            if (addWrappingErrorCssClass) {
                jQuery(violationMessageElement).removeClass(this._controlErrorCssClass);
            }

            violationMessageElement.style.display = 'none';
        }
        else {
            // We add the wrapping css class if set                 
            if (addWrappingErrorCssClass) {
                if (!jQuery(violationMessageElement).hasClass(this._controlErrorCssClass)) {
                    jQuery(violationMessageElement).addClass(this._controlErrorCssClass);
                }
            }
            this._showViolationMessageElement(violationMessageElement);
        }

        return isValid;
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _isExternalPageChoiceFieldValueChangedHandler: function (sender, args) {
        if (sender.get_value() == "true") {
            this._dataItem.IsExternal = true;
            jQuery(this._externalPageControlsGroup).show();
        }
        else {
            this._dataItem.IsExternal = false;
            jQuery(this._externalPageControlsGroup).hide();
        }
    },

    _pageTypeSelectorValueChangedHandler: function (sender, args) {
        if (sender.get_value() == this.InternalPageID) {
            this._optionsMultipage.set_selectedIndex(0);
        }
        else {
            this._optionsMultipage.set_selectedIndex(1);

        }
    },

    _onOpenPopupClick: function () {
        if (this._selectedRadioIndex == 0) {
            this._pageTypeSelector.set_value(this.InternalPageID);
            //this._internalPageSelector.get_itemsTree().set_selectedItems([ this._dataItem.LinkedNodeId ]);
        }
        else {
            this._pageTypeSelector.set_value(this.ExternalPageID);
            this._externalPageSelector.get_urlTextBox().set_value(this._dataItem.RedirectUrl);
            this._externalPageSelector.get_openInNewWindowChoiceField().set_value(this._dataItem.OpenNewWindow);
        }

        this._optionsMultipage.set_selectedIndex(this._selectedRadioIndex);

        this._dialog.dialog("open");
        // Hide the dialog's titlebar
        $(".ui-dialog-titlebar").hide();

        return false;
    },

    _clearValidationErrors: function () {
        if (!this._externalPageSelector.get_urlTextBox().validate()) {
            this._externalPageSelector.get_urlTextBox().set_value("http://");
            jQuery(this.get_dialog()).find('.sfError').hide();
        }
    },

    _onDoneSelecting: function () {
        var valid = false;
        var newTitle = "";

        if (this._pageTypeSelector.get_value() == this.InternalPageID) {
            if (this._internalPageSelector.get_selectedItems() && this._internalPageSelector.get_selectedItems().length == 1) {
                var pageItem = this._internalPageSelector.get_selectedItem();
                newTitle = pageItem.Title.Value || pageItem.Title;
                this._internalPageFullUrl = pageItem.FullUrl;
                this._dataItem.LinkedNodeId = pageItem.Id;
                this._dataItem.LinkedNodeProvider = this._internalPageSelector.get_provider();
                this._dataItem.RedirectUrl = "";
                this._dataItem.OpenNewWindow = false;
                this._dataItem.IsExternal = true;

                this._selectedRadioIndex = 0;
                valid = true;
            }
        }
        else {
            if (this._externalPageSelector.get_urlTextBox().validate()) {
                newTitle = this._externalPageSelector.get_urlTextBox().get_value();
                this._dataItem.LinkedNodeId = this.EmptyGuid;
                this._dataItem.LinkedNodeProvider = "";
                this._dataItem.RedirectUrl = newTitle;
                this._dataItem.OpenNewWindow = this._externalPageSelector.get_openInNewWindowChoiceField().get_value();
                this._dataItem.IsExternal = true;

                this._selectedRadioIndex = 1;
                valid = true;
            }
        }

        if (valid) {
            jQuery(this._currentExternalLinkLabel).text(newTitle);
            jQuery(this._openDialogLink).html(this._clientLabelManager.getLabel("PageResources", "EditPageToRedirectTo"));
            jQuery(this._viewExternalPageLink).show();
            this._dialog.dialog("close");
            this.validate();
        }
    },

    _onCancel: function () {
        this._clearValidationErrors();
        this._dialog.dialog("close");
    },

    _onViewThisPage: function () {
        if (this._selectedRadioIndex == 0) {
            window.open(this._internalPageFullUrl, "_blank");
        } else {
            window.open(this._dataItem.RedirectUrl.replace("~/", this._siteRootPath + "/"), "_blank");
        }
    },

    /* --------------------------------- private methods --------------------------------- */

    _setCultures: function () {
        var pageSelector = this._internalPageSelector;
        pageSelector.set_culture(this._culture);
        var uiCulture = this._uiCulture;
        pageSelector._uiCulture = uiCulture;
        pageSelector.get_gridBinder().set_uiCulture(uiCulture);
        pageSelector.get_treeBinder().set_uiCulture(uiCulture);
    },

    _setFilter: function () {
        var itemId = this._dataItem.Id;
        //This is a hack. When the detail form view is opened from the page editor in order to add a new language version
        //then the data item id is Guid.Empty and we need to get the item id from a different place
        try {
            if (!itemId || itemId == Telerik.Sitefinity.getEmptyGuid())
                itemId = dialogBase.get_radWindow()._sfArgs.get_commandArgument().sourceObjectId;
        }
        catch (err) { }
        var pageSelector = this._internalPageSelector;
        if (itemId) {
            var grid = pageSelector.get_itemsGrid();
            var tree = pageSelector.get_itemsTree();
            pageSelector.set_unselectableItems([itemId]);
            var filter = "Id != (" + itemId + ")";
            if (!this._originalFilterSet) {
                this._originalGridFilter = grid.get_constantFilter();
                this._originalTreeFilter = tree.get_constantFilter();
                this._originalFilterSet = true;
            }
            this._setListFilter(grid, filter, this._originalGridFilter, false);
        }
        else {
            pageSelector.set_unselectableItems([]);
            pageSelector.clearSelection();
        }
        pageSelector.dataBind();
    },

    _setListFilter: function (list, filter, constantFilter, bindList) {
        if (constantFilter)
            constantFilter = String.format("({0}) AND ({1})", constantFilter, filter);
        else
            constantFilter = filter;
        list.set_constantFilter(constantFilter);
        if (bindList)
            list.applyFilter();
    },

    /* --------------------------------- properties -------------------------------------- */

    get_currentExternalLinkLabel: function () {
        return this._currentExternalLinkLabel;
    },

    set_currentExternalLinkLabel: function (value) {
        this._currentExternalLinkLabel = value;
    },
    get_isExternalPageChoiceField: function () {
        return this._isExternalPageChoiceField;
    },
    set_isExternalPageChoiceField: function (value) {
        this._isExternalPageChoiceField = value;
    },
    get_externalPageControlsGroup: function () {
        return this._externalPageControlsGroup;
    },
    set_externalPageControlsGroup: function (value) {
        this._externalPageControlsGroup = value;
    },
    get_openDialogLink: function () {
        return this._openDialogLink;
    },
    set_openDialogLink: function (value) {
        this._openDialogLink = value;
    },
    get_viewExternalPageLink: function () {
        return this._viewExternalPageLink;
    },
    set_viewExternalPageLink: function (value) {
        this._viewExternalPageLink = value;
    },
    get_dialogBoxDiv: function () {
        return this._dialogBoxDiv;
    },
    set_dialogBoxDiv: function (value) {
        this._dialogBoxDiv = value;
    },

    get_dialog: function () {
        return this._dialog;
    },
    set_dialog: function (value) {
        this._dialog = value;
    },

    get_selectedRadioIndex: function () {
        return this._selectedRadioIndex;
    },
    set_selectedRadioIndex: function (value) {
        this._selectedRadioIndex = value;
    },
    get_pageTypeSelector: function () {
        return this._pageTypeSelector;
    },
    set_pageTypeSelector: function (value) {
        this._pageTypeSelector = value;
    },
    get_optionsMultipage: function () {
        return this._optionsMultipage;
    },
    set_optionsMultipage: function (value) {
        this._optionsMultipage = value;
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
    },
    get_internalPageSelector: function () {
        return this._internalPageSelector;
    },
    set_internalPageSelector: function (value) {
        this._internalPageSelector = value;
    },
    get_externalPageSelector: function () {
        return this._externalPageSelector;
    },
    set_externalPageSelector: function (value) {
        this._externalPageSelector = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        if (this._clientLabelManager != value) {
            this._clientLabelManager = value;
            this.raisePropertyChanged('clientLabelManager');
        }
    }
};

Telerik.Sitefinity.Web.UI.Fields.ExternalPageField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ExternalPageField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem, Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);
