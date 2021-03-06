Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner = function (element) {

    //control IDs given from the server
    this._feedSelectionPanelID = null;
    this._feedSelectorID = null;
    this._noFeedSelectedLabelID = null;
    this._selectedFeedTitleID = null;
    this._selectFeedButtonID = null;
    this._doneSelectingFeedButtonID = null;
    this._cancelSelectingFeedButtonID = null;
    this._radioLinksInPageAndAddressBarID = null;
    this._radioLinkInTheBrowserAddressBarOnlyID = null;
    this._radioLinkInThePageOnlyID = null;
    this._textToDisplayID = null;
    this._radioBigIconID = null;
    this._radioSmallIconID = null;
    this._radioNoIconID = null;
    this._moreOptionsButtonID = null;
    this._tooltipID = null;
    this._cssClassTextBoxID = null;
    this._openInNewWindowID = null;
    this._moreOptionsPanelID = null;
    this._feedEmbedControlEnum = null;
    this._iconSizeEnum = null;
    this._selectAFeedText = null;
    this._changeSelectedFeedText = null;

    //private control references
    this._feedSelector = null;
    this._noFeedSelectedLabel = null;
    this._selectedFeedTitle = null;
    this._selectFeedButton = null;
    this._doneSelectingFeedButton = null;
    this._cancelSelectingFeedButton = null;
    this._radioLinksInPageAndAddressBar = null;
    this._radioLinkInTheBrowserAddressBarOnly = null;
    this._radioLinkInThePageOnly = null;
    this._textToDisplay = null;
    this._radioBigIcon = null;
    this._radioSmallIcon = null;
    this._radioNoIcon = null;
    this._moreOptionsButton = null;
    this._tooltip = null;
    this._cssClassTextBox = null;
    this._openInNewWindow = null;
    this._moreOptionsPanel = null;

    this._feedSelectionPanel = null;
    this._dialog = null;

    //private vars
    this._moreOptionsShown = false;
    this._currentFeedID = "00000000-0000-0000-0000-000000000000";
    this._currentFeedTitle = "";
    this._aFeedHasBeenSelected = false;

    Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner.prototype = {

    /* ****************************** set up / tear down methods ****************************** */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner.callBaseMethod(this, 'initialize');
        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        //populate private control references
        this._noFeedSelectedLabel = $get(this._noFeedSelectedLabelID);
        this._selectedFeedTitle = $get(this._selectedFeedTitleID);
        this._selectFeedButton = $get(this._selectFeedButtonID);
        this._doneSelectingFeedButton = $get(this._doneSelectingFeedButtonID);
        this._cancelSelectingFeedButton = $get(this._cancelSelectingFeedButtonID);
        this._radioLinksInPageAndAddressBar = $get(this._radioLinksInPageAndAddressBarID);
        this._radioLinkInTheBrowserAddressBarOnly = $get(this._radioLinkInTheBrowserAddressBarOnlyID);
        this._radioLinkInThePageOnly = $get(this._radioLinkInThePageOnlyID);
        this._textToDisplay = $get(this._textToDisplayID);
        this._radioBigIcon = $get(this._radioBigIconID);
        this._radioSmallIcon = $get(this._radioSmallIconID);
        this._radioNoIcon = $get(this._radioNoIconID);
        this._moreOptionsButton = $get(this._moreOptionsButtonID);
        this._tooltip = $get(this._tooltipID);
        this._cssClassTextBox = $get(this._cssClassTextBoxID);
        this._openInNewWindow = $get(this._openInNewWindowID);
        this._moreOptionsPanel = $get(this._moreOptionsPanelID);

        this._feedEmbedControlEnum = Sys.Serialization.JavaScriptSerializer.deserialize(this._feedEmbedControlEnum);
        this._iconSizeEnum = Sys.Serialization.JavaScriptSerializer.deserialize(this._iconSizeEnum);

        //event handler delegates
        this._selectFeedButton_ClickDelegate = Function.createDelegate(this, this._selectFeedButton_Click);
        this._doneSelectingFeedButton_ClickDelegate = Function.createDelegate(this, this._doneSelectingFeedButton_Click);
        this._cancelSelectingFeedButton_ClickDelegate = Function.createDelegate(this, this._cancelSelectingFeedButton_Click);
        this._moreOptionsButton_ClickDelegate = Function.createDelegate(this, this._moreOptionsButton_Click);

        //event handlers
        $addHandler(this._selectFeedButton, "click", this._selectFeedButton_ClickDelegate);
        $addHandler(this._doneSelectingFeedButton, "click", this._doneSelectingFeedButton_ClickDelegate);
        $addHandler(this._cancelSelectingFeedButton, "click", this._cancelSelectingFeedButton_ClickDelegate);
        $addHandler(this._moreOptionsButton, "click", this._moreOptionsButton_ClickDelegate);

        this._dialog = jQuery(this._feedSelectionPanel).dialog({
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

    // tear down
    dispose: function () {
        $removeHandler(this._selectFeedButton, "click", this._selectFeedButton_ClickDelegate);
        $removeHandler(this._doneSelectingFeedButton, "click", this._doneSelectingFeedButton_ClickDelegate);
        $removeHandler(this._cancelSelectingFeedButton, "click", this._cancelSelectingFeedButton_ClickDelegate);
        $removeHandler(this._moreOptionsButton, "click", this._moreOptionsButton_ClickDelegate);
    },

    onload: function () {
        this._feedSelector = $find(this._feedSelectorID);
        if (!this._aFeedHasBeenSelected) {
            this._textToDisplay.value = "";
            this._tooltip.value = "";
            this._cssClassTextBox.value = "";
        }
        dialogBase.resizeToContent();
    },

    _getSelector: function () {
        if (this._feedSelector == null)
            this._feedSelector = $find(this._feedSelectorID);

        return this._feedSelector;
    },

    _moreOptionsButton_Click: function () {
        jQuery(this._moreOptionsPanel).toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    _selectFeedButton_Click: function () {
        //this._toggleControlDisplay(this._feedSelectionPanel, true);
        this._dialog.dialog("open");
        jQuery("body > form").hide();
        this._getSelector().bindSelector();
        dialogBase.resizeToContent();
    },

    _doneSelectingFeedButton_Click: function () {
        //this._toggleControlDisplay(this._feedSelectionPanel, false);
        this._dialog.dialog("close");
        jQuery("body > form").show();
        if (this._getSelector().getSelectedItems().length > 0) {
            var selectedFeed = this._getSelector().getSelectedItems()[0];
            this._selectedFeed(selectedFeed.ID, selectedFeed.Title);
            if (this._textToDisplay.value == "")
                this._textToDisplay.value = selectedFeed.Title;
        }
        dialogBase.resizeToContent();
    },

    _cancelSelectingFeedButton_Click: function () {
        //this._toggleControlDisplay(this._feedSelectionPanel, false);
        this._dialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    _toggleControlDisplay: function (ctlElement, bIsDisplayed) {
        if (ctlElement != null)
            ctlElement.style.display = ((bIsDisplayed) ? "block" : "none");
    },

    _setLabelText: function (LabelElement, newText) {
        if (LabelElement != null) {
            if (typeof LabelElement.textContent != "undefined")
                LabelElement.textContent = newText;

            if (typeof LabelElement.innerText != "undefined")
                LabelElement.innerText = newText;
        }
    },

    _noSelectedFeed: function () {
        this._aFeedHasBeenSelected = false;
        $(this._noFeedSelectedLabel).removeClass("sfDisplayNoneImportant");
        $(this._selectedFeedTitle).addClass("sfDisplayNoneImportant");
        this._selectFeedButton.innerHTML = this._selectAFeedText;
        this._selectedFeedID = "00000000-0000-0000-0000-000000000000";
    },

    _selectedFeed: function (feedID, feedTitle) {
        this._aFeedHasBeenSelected = true;
        $(this._noFeedSelectedLabel).addClass("sfDisplayNoneImportant");
        $(this._selectedFeedTitle).removeClass("sfDisplayNoneImportant");
        this._setLabelText(this._selectedFeedTitle, feedTitle);
        this._selectFeedButton.innerHTML = this._changeSelectedFeedText;
        this._currentFeedID = feedID;
        this._currentFeedTitle = feedTitle;
    },

    // forces the designer to refresh the UI from the cotnrol Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        switch (controlData.FeedIconSize) {
            case this._iconSizeEnum.BigIcon:
                this._radioBigIcon.checked = true;
                break;

            case this._iconSizeEnum.SmallIcon:
                this._radioSmallIcon.checked = true;
                break;

            case this._iconSizeEnum.NoIcon:
                this._radioNoIcon.checked = true;
                break;

            default:
                this._radioBigIcon.checked = true;
                break;
        }

        switch (controlData.FeedInsertionMethod) {
            case this._feedEmbedControlEnum.PageAndAddressBar:
                this._radioLinksInPageAndAddressBar.checked = true;
                break;

            case this._feedEmbedControlEnum.AddressBarOnly:
                this._radioLinkInTheBrowserAddressBarOnly.checked = true;
                break;

            case this._feedEmbedControlEnum.PageOnly:
                this._radioLinkInThePageOnly.checked = true;
                break;

            default:
                this._radioLinksInPageAndAddressBar.checked = true;
                break;
        }

        this._textToDisplay.value = (controlData.TextToDisplay == null) ? "" : controlData.TextToDisplay;
        this._tooltip.value = (controlData.ToolTip == null) ? "" : controlData.ToolTip;
        this._cssClassTextBox.value = (controlData.CssClass == null) ? "" : controlData.CssClass;
        this._openInNewWindow.checked = ((controlData.OpenInNewWindow != null) && (String(controlData.OpenInNewWindow).toUpperCase() == "TRUE"));
        if ((controlData.FeedID == null) || (controlData.FeedID == "00000000-0000-0000-0000-000000000000"))
            this._noSelectedFeed();
        else
            this._selectedFeed(controlData.FeedID, controlData.Title);
    },

    // forces the designer to apply the changes on UI to the cotnrol Data
    applyChanges: function () {
        var controlData = this.get_controlData();

        if (this._radioBigIcon.checked)
            controlData.FeedIconSize = this._iconSizeEnum.BigIcon;
        else if (this._radioSmallIcon.checked)
            controlData.FeedIconSize = this._iconSizeEnum.SmallIcon;
        else if (this._radioNoIcon.checked)
            controlData.FeedIconSize = this._iconSizeEnum.NoIcon;

        if (this._radioLinksInPageAndAddressBar.checked)
            controlData.FeedInsertionMethod = this._feedEmbedControlEnum.PageAndAddressBar;
        else if (this._radioLinkInTheBrowserAddressBarOnly.checked)
            controlData.FeedInsertionMethod = this._feedEmbedControlEnum.AddressBarOnly;
        else if (this._radioLinkInThePageOnly.checked)
            controlData.FeedInsertionMethod = this._feedEmbedControlEnum.PageOnly;

        controlData.TextToDisplay = this._textToDisplay.value;
        controlData.ToolTip = this._tooltip.value;
        controlData.CssClass = this._cssClassTextBox.value;
        controlData.OpenInNewWindow = this._openInNewWindow.checked;

        controlData.FeedID = this._currentFeedID;
        controlData.Title = this._currentFeedTitle;
    },

    get_feedSelectionPanel: function () {
        return this._feedSelectionPanel;
    },
    set_feedSelectionPanel: function (value) {
        this._feedSelectionPanel = value;
    }
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
