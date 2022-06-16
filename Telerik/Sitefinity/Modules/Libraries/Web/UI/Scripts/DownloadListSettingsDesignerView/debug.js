Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");


Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ViewNames = function () { };
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ViewNames.prototype =
{
    MasterListView: 0,
    MasterTableView: 1,
    MasterListDetailView: 2,
    MasterTableDetailView: 3
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ViewNames.registerEnum("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ViewNames");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._modeRadioButtonClientIDs = [];
    this._pagingRadioButtonClientIDs = [];
    this._pagingTextFieldClientIDs = [];
    this._limitRadioButtonClientIDs = [];
    this._noLimitAndPagingRadioButtonsClientIDs = [];
    this._limitTextFieldClientIDs = [];
    //this._multiPageClientID = null;
    this._currentViewName = null;
    this._currentDetailViewName = null;
    this._currentMode = null;
    this._pageLoadDelegate = null;
    this._multiPage = null;
    this._masterViewNameMap = [];

    this._displayIconsCheckBoxIDs = [];
    this._iconSizesRadioButtonsClientIDs = [];
    this._downloadLinkPositionChoiceFieldIDs = [];

    this._modeRadioButtonCheckedDelegate = null;
    this._bigIconsRadioButton = 0;
    this._smallIconsRadioButton = 1;
    this._noIconsRadioButton = 2;
    this._widgetEditorDialog = null;
    this._radWindowManager = null;
    this._selectedMasterTemplateId = null;
    this._selectedDetailTemplateId = null;

    this._editListTemplateLink = null;
    this._editTableTemplateLink = null;
    this._editListDetailTemplateLink = null;
    this._editTableDetailTemplateLink = null;
    this._editDetailTemplateLink = null;
    this._editDetailTemplateLink2 = null;
    this._modifyWidgetTemplatePermission = null;

    this._editListTemplateDelegate = null;
    this._editTableTemplateDelegate = null;
    this._editListDetailTemplateDelegate = null;
    this._editTableDetailTemplateDelegate = null;
    this._editDetailTemplateDelegate = null;
    this._editDetailTemplateLink2Delegate = null;
    this._templateLinkIdMap = [];

    this._widgetEditorCloseDelegate = null;
    this._widgetEditorShowDelegate = null;

    $views = Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ViewNames;

}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView.callBaseMethod(this, "initialize");
        this._modeRadioButtonCheckedDelegate = Function.createDelegate(this, this._modeRadioButtonCheckedChanged);
        var modeRadioCount = this._modeRadioButtonClientIDs.length;
        for (var i = 0; i < modeRadioCount; i++) {
            var radio = $get(this._modeRadioButtonClientIDs[i]);
            $addHandler(radio, "click", this._modeRadioButtonCheckedDelegate);
        }
        if (this._widgetEditorCloseDelegate == null) {
            this._widgetEditorCloseDelegate = Function.createDelegate(this, this._widgetEditorClosed);
        }
        if (this._widgetEditorShowDelegate == null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._widgetEditorShown);
        }

        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);

        this._bigIconsRadioButton = $get(this._iconSizesRadioButtonsClientIDs[0]);
        this._smallIconsRadioButton = $get(this._iconSizesRadioButtonsClientIDs[1]);
        this._noIconsRadioButton = $get(this._iconSizesRadioButtonsClientIDs[2]);

        if (this._editListTemplateDelegate == null) {
            this._editListTemplateDelegate = Function.createDelegate(this, this._editListTemplateClicked);
        }
        if (this._editTableTemplateDelegate == null) {
            this._editTableTemplateDelegate = Function.createDelegate(this, this._editTableTemplateClicked);
        }
        if (this._editListDetailTemplateDelegate == null) {
            this._editListDetailTemplateDelegate = Function.createDelegate(this, this._editListDetailTemplateClicked);
        }
        if (this._editTableDetailTemplateDelegate == null) {
            this._editTableDetailTemplateDelegate = Function.createDelegate(this, this._editTableDetailTemplateClicked);
        }
        if (this._editDetailTemplateDelegate == null) {
            this._editDetailTemplateDelegate = Function.createDelegate(this, this._editDetailTemplateClicked);
        }
        if (this._editDetailTemplateLink2Delegate == null) {
            this._editDetailTemplateLink2Delegate = Function.createDelegate(this, this._editDetailTemplateLink2Clicked);
        }
        $addHandler(this._editListTemplateLink, "click", this._editListTemplateDelegate);
        $addHandler(this._editTableTemplateLink, "click", this._editTableTemplateDelegate);
        $addHandler(this._editListDetailTemplateLink, "click", this._editListDetailTemplateDelegate);
        $addHandler(this._editTableDetailTemplateLink, "click", this._editTableDetailTemplateDelegate);
        $addHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateDelegate);
        $addHandler(this._editDetailTemplateLink2, "click", this._editDetailTemplateLink2Delegate);

        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView.callBaseMethod(this, "dispose");
        Sys.Application.remove_load(this._pageLoadDelegate);

        for (var i = 0, length = this._modeRadioButtonClientIDs.length; i < length; i++) {
            var radio = $get(this._modeRadioButtonClientIDs[i]);
            $removeHandler(radio, "click", this._modeRadioButtonCheckedDelegate);
        }
        delete this._modeRadioButtonCheckedDelegate;
        if (this._widgetEditorCloseDelegate) {
            delete this._widgetEditorCloseDelegate;
        }
        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }
        $removeHandler(this._editListTemplateLink, "click", this._editListTemplateDelegate);
        $removeHandler(this._editTableTemplateLink, "click", this._editTableTemplateDelegate);
        $removeHandler(this._editListDetailTemplateLink, "click", this._editListDetailTemplateDelegate);
        $removeHandler(this._editTableDetailTemplateLink, "click", this._editTableDetailTemplateDelegate);
        $removeHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateDelegate);
        $removeHandler(this._editDetailTemplateLink2, "click", this._editDetailTemplateLink2Delegate);

        if (this._editListTemplateDelegate) {
            delete this._editListTemplateDelegate;
        }
        if (this._editTableTemplateDelegate) {
            delete this._editTableTemplateDelegate;
        }
        if (this._editListDetailTemplateDelegate) {
            delete this._editListDetailTemplateDelegate;
        }
        if (this._editTableDetailTemplateDelegate) {
            delete this._editTableDetailTemplateDelegate;
        }
        if (this._editDetailTemplateDelegate) {
            delete this._editDetailTemplateDelegate;
        }
        if (this._editDetailTemplateLink2Delegate) {
            delete this._editDetailTemplateLink2Delegate;
        }
    },

    // ----------------------------------------------- Private functions ----------------------------------------------

    _changeMode: function (newMode) {
        this.get_multiPage().get_pageViews().getPageView(newMode).set_selected(true);
        this._currentViewName = this._masterViewNameMap[newMode];
        var currentView = this.get_currentView();
        for (var counter = 0; counter < this._modeRadioButtonClientIDs.length; counter++) {
            var modeRadio = $get(this._modeRadioButtonClientIDs[counter]);
            if (counter === newMode) {
                modeRadio.checked = true;
            }
            else {
                modeRadio.checked = false;
            }
        }
        if (currentView.AllowPaging) {
            this._displayPagingForMode(newMode, currentView.ItemsPerPage);
        }
        else {
            if (currentView.ItemsPerPage > 0) {
                this._displayLimitedListForMode(newMode, currentView.ItemsPerPage);
            }
            else {
                var noPagingRadio = $get(this._noLimitAndPagingRadioButtonsClientIDs[newMode]);
                $(noPagingRadio).click();
            }
        }
    },

    _modeRadioButtonCheckedChanged: function (sender, args) {
        var clickedRadioID = sender.target.value;
        switch (clickedRadioID) {
            case "rbMode1": //List
                this._currentMode = $views.MasterListView;
                this._refreshThumbnailType(this._currentMode);
                break;
            case "rbMode2": //Table
                this._currentMode = $views.MasterTableView;
                this._refreshThumbnailType(this._currentMode);
                break;
            case "rbMode3": //List + detail
                this._currentMode = $views.MasterListDetailView;
                this._refreshDownloadLinkPosition(this._currentMode);
                break;
            case "rbMode4": //table + detail
                this._currentMode = $views.MasterTableDetailView;
                this._refreshThumbnailType(this._currentMode);
                this._refreshDownloadLinkPosition(this._currentMode);
                break;
            default:
                this._currentMode = $views.MasterListView;
                break;
        }
        this._changeMode(this._currentMode);
        dialogBase.resizeToContent();
    },

    _pageLoadHandler: function (sender, args) {
        //this._multiPage = $find(this._multiPageClientID);
        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._widgetEditorCloseDelegate);
            //this._widgetEditorDialog.add_show(this._widgetEditorShowDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _editListTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editListTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editTableTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editTableTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editListDetailTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editListDetailTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editTableDetailTemplateClicked: function (sender, args) {
        this._selectedMasterTemplateId = this._templateLinkIdMap[this.get_editTableDetailTemplateLink().id];
        this._selectedDetailTemplateId = null;
        this._showWidgetEditor();
    },

    _editDetailTemplateClicked: function (sender, args) {
        this._selectedDetailTemplateId = this._templateLinkIdMap[this.get_editDetailTemplateLink().id];
        this._selectedMasterTemplateId = null;
        this._showWidgetEditor();
    },

    _editDetailTemplateLink2Clicked: function (sender, args) {
        this._selectedDetailTemplateId = this._templateLinkIdMap[this.get_editDetailTemplateLink2().id];
        this._selectedMasterTemplateId = null;
        this._showWidgetEditor();
    },

    _showWidgetEditor: function () {
        if (this._modifyWidgetTemplatePermission) {
            if (this._widgetEditorDialog) {
                var dialogUrl = this._widgetEditorDialogUrl;
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                dialogBase.get_radWindow().maximize();
                //dialog.add_close(this._onWidgetEditorClosed);
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
                $("body").removeClass("sfSelectorDialog");
            }
        } else {
            alert("You don't have the permissions to create and edit widgets templates.");
        }
    },

    _widgetEditorClosed: function (sender, args) {
        dialogBase.get_radWindow().Restore();
        $("body").addClass("sfSelectorDialog");
    },

    _widgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //            if (itemsList.get_scrollOpenedDialogsToTop()) {
            //                frameHandle.scrollTo(0, 0);
            //            }
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog) {
                var templateId = this._selectedMasterTemplateId ? this._selectedMasterTemplateId : this._selectedDetailTemplateId;
                var params = { TemplateId: templateId };
                frameHandle.createDialog(null, null, null, dialogBase, params, null, null);
            }
        }
    },

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().MasterViewName;
    },

    get_currentDetailViewName: function () {
        return (this._currentDetailViewName) ? this._currentDetailViewName : this.get_controlData().DetailViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentViewName()];
    },

    get_currentDetailView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentDetailViewName()];
    },

    // updates the UI (pageView for the given mode) with new values for Paging-related properties
    _displayPagingForMode: function (mode, itemsPerPage) {
        var pagingRadio = $get(this._pagingRadioButtonClientIDs[mode]);
        $(pagingRadio).click();
        var textField = $find(this._pagingTextFieldClientIDs[mode]);
        textField.set_value(itemsPerPage);
    },

    // updates the UI (pageView for the given mode) with new values for Limit-related properties
    _displayLimitedListForMode: function (mode, itemsPerPage) {
        var limitRadio = $get(this._limitRadioButtonClientIDs[mode]);
        $(limitRadio).click();
        var textField = $find(this._limitTextFieldClientIDs[mode]);
        textField.set_value(itemsPerPage);
    },

    _resolvePropertyPath: function (property) {
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        var propertyPath = viewPath + "." + property;
        return propertyPath;
    },

    _setPagingOrLimitToControlData: function (mode) {
        var currentModePagingRadio = $get(this._pagingRadioButtonClientIDs[mode]);
        var parentDesigner = this.get_parentDesigner();
        var textField = null;
        if (currentModePagingRadio.checked) {
            // paging should be used
            parentDesigner.setValueToControlData(this._resolvePropertyPath("AllowPaging"), true, this.get_controlData());
            textField = $find(this._pagingTextFieldClientIDs[mode]);
            parentDesigner.setValueToControlData(this._resolvePropertyPath("ItemsPerPage"), textField.get_value(), this.get_controlData());
        }
        else {
            var currentModeLimitRadio = $get(this._limitRadioButtonClientIDs[mode]);
            if (currentModeLimitRadio.checked) {
                // limit should be used
                parentDesigner.setValueToControlData(this._resolvePropertyPath("AllowPaging"), false, this.get_controlData());
                textField = $find(this._limitTextFieldClientIDs[mode]);
                parentDesigner.setValueToControlData(this._resolvePropertyPath("ItemsPerPage"), textField.get_value(), this.get_controlData());
            }
            else {
                // no paging or limit
                parentDesigner.setValueToControlData(this._resolvePropertyPath("AllowPaging"), false, this.get_controlData());
                parentDesigner.setValueToControlData(this._resolvePropertyPath("ItemsPerPage"), 0, this.get_controlData());
            }
        }
    },

    // Sets the download link position
    _setDownloadLinkPosition: function (mode) {
        var choiceField = null;

        switch (mode) {
            case $views.MasterListDetailView:
                choiceField = $find(this._downloadLinkPositionChoiceFieldIDs[0]);
                break;
            case $views.MasterTableDetailView:
                choiceField = $find(this._downloadLinkPositionChoiceFieldIDs[1]);
                break;
        }

        if (!choiceField) {
            return;
        }

        var parent = this.get_parentDesigner();
        var value = choiceField.get_value();

        var isArray = Array.prototype.isPrototypeOf(value);
        var data = this.get_controlData();

        var linkBelow = !isArray && value == "below" || isArray && Array.contains(value, "below");
        parent.setValueToControlData(this._resolvePropertyPath("ShowDownloadLinkBelowDescription"), linkBelow, data);

        var linkAbove = !isArray && value == "above" || isArray && Array.contains(value, "above");
        parent.setValueToControlData(this._resolvePropertyPath("ShowDownloadLinkAboveDescription"), linkAbove, data);
    },

    // sets tthe thumbnail type if applicable
    _setThumbnailType: function (mode) {
        var thumbnailType;
        switch (mode) {
            case $views.MasterListView: // List
                for (var i = 0, length = this._iconSizesRadioButtonsClientIDs.length; i < length; i++) {
                    var radio = $get(this._iconSizesRadioButtonsClientIDs[i]);
                    if (jQuery(radio).is(":checked")) {
                        switch (i) {
                            case 0: thumbnailType = "BigIcons"; break;
                            case 1: thumbnailType = "SmallIcons"; break;
                            case 2: thumbnailType = "NoIcons"; break;
                        }
                        break;
                    }
                }
                break;
            case $views.MasterTableView: //List
                var displayIconsCheckBox = $get(this._displayIconsCheckBoxIDs[0]);
                if (jQuery(displayIconsCheckBox).is(":checked")) {
                    thumbnailType = "SmallIcons";
                }
                else {
                    thumbnailType = "NoIcons";
                }
                break;
            case $views.MasterTableDetailView: //Table + detail
                var displayIconsCheckBox = $get(this._displayIconsCheckBoxIDs[1]);
                if (jQuery(displayIconsCheckBox).is(":checked")) {
                    thumbnailType = "SmallIcons";
                }
                else {
                    thumbnailType = "NoIcons";
                }
                break;
        }
        if (thumbnailType) {
            var parent = this.get_parentDesigner();
            parent.setValueToControlData(this._resolvePropertyPath("ThumbnailType"), thumbnailType, this.get_controlData());
        }
    },

    // sets the UI download list postition according to the value in the view
    _refreshDownloadLinkPosition: function (mode) {
        this._currentViewName = this._masterViewNameMap[mode];
        var currentView = this.get_currentView();
        var choiceField;
        switch (mode) {
            case $views.MasterListDetailView:
                choiceField = $find(this._downloadLinkPositionChoiceFieldIDs[0]);
                break;
            case $views.MasterTableDetailView:
                choiceField = $find(this._downloadLinkPositionChoiceFieldIDs[1]);
                break;
        }

        var choiceValues = [];
        if (currentView.ShowDownloadLinkBelowDescription) {
            choiceValues.push("below");
        }
        if (currentView.ShowDownloadLinkAboveDescription) {
            choiceValues.push("above");
        }

        if (choiceValues.length > 0) {
            choiceField.set_value(choiceValues);
        }
    },

    // sets the UI thumbnail type/display icons according to the value in the view
    _refreshThumbnailType: function (mode) {
        this._currentViewName = this._masterViewNameMap[mode];
        var currentView = this.get_currentView();
        switch (mode) {
            case $views.MasterListView:
                switch (currentView.ThumbnailType) {
                    case "NoIcons":
                        jQuery(this._noIconsRadioButton).attr('checked', true);
                        break;
                    case "SmallIcons":
                        jQuery(this._smallIconsRadioButton).attr('checked', true);
                        break;
                    case "BigIcons":
                        jQuery(this._bigIconsRadioButton).attr('checked', true);
                        break;
                    default:
                        jQuery(this._bigIconsRadioButton).attr('checked', true);
                        break;
                }
                break;
            case $views.MasterTableView:
                var displayIconsCheckBox = $get(this._displayIconsCheckBoxIDs[0]);
                if (currentView.ThumbnailType != "NoIcons" && currentView.ThumbnailType != "NoSet") {
                    jQuery(displayIconsCheckBox).attr('checked', true);
                }
                else {
                    jQuery(displayIconsCheckBox).attr('checked', false);
                }
                break;
            case $views.MasterListDetailView:
                break;
            case $views.MasterTableDetailView:
                var displayIconsCheckBox = $get(this._displayIconsCheckBoxIDs[1]);
                if (currentView.ThumbnailType != "NoIcons" && currentView.ThumbnailType != "NoSet") {
                    jQuery(displayIconsCheckBox).attr('checked', true);
                }
                else {
                    jQuery(displayIconsCheckBox).attr('checked', false);
                }
                break;
        }
    },

    // ----------------------------------------------- Public functions -----------------------------------------------
    refreshUI: function () {
        this._refreshing = true;
        var control = this.get_controlData();
        var currentView = this.get_currentView();

        switch (currentView.ViewName) {
            case this._masterViewNameMap[$views.MasterListView]:
                this._currentMode = $views.MasterListView; // List
                this._refreshThumbnailType(this._currentMode);
                break;
            case this._masterViewNameMap[$views.MasterTableView]:
                this._currentMode = $views.MasterTableView; // Table
                this._refreshThumbnailType(this._currentMode);
                break;
            case this._masterViewNameMap[$views.MasterListDetailView]:
                this._currentMode = $views.MasterListDetailView; // List + detail
                this._refreshDownloadLinkPosition(this._currentMode);
                break;
            case this._masterViewNameMap[$views.MasterTableDetailView]:
                this._currentMode = $views.MasterTableDetailView; // Table + detail
                this._refreshThumbnailType(this._currentMode);
                this._refreshDownloadLinkPosition(this._currentMode);
                break;
            default:
                this._currentMode = $views.MasterListView; // List
                break;
        }
        this._changeMode(this._currentMode);
        this._refreshing = false;
    },

    applyChanges: function () {
        var parent = this.get_parentDesigner();
        var currentViewName = this._masterViewNameMap[this._currentMode];

        if (this._selectedMasterTemplateId) {
            this.get_currentView().TemplateKey = this._selectedMasterTemplateId;
        }
        if (this._selectedDetailTemplateId) {
            this.get_currentDetailView().TemplateKey = this._selectedDetailTemplateId;
        }
        // set the current view                
        parent.setValueToControlData("MasterViewName", currentViewName, this.get_controlData());

        this._setPagingOrLimitToControlData(this._currentMode)
        this._setThumbnailType(this._currentMode);
        this._setDownloadLinkPosition(this._currentMode);
    },

    // gets the object that represents the client side representation of the control 
    // being edited
    get_controlData: function () {
        var parent = this.get_parentDesigner();
        if (parent) {
            var pe = parent.get_propertyEditor();
            if (pe) {
                return pe.get_control();
            }
        }
        alert('Control designer cannot find the control properties object!');
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

    get_editListTemplateLink: function () {
        return this._editListTemplateLink;
    },

    set_editListTemplateLink: function (value) {
        if (this._editListTemplateLink != value) {
            this._editListTemplateLink = value;
        }
    },

    get_editTableTemplateLink: function () {
        return this._editTableTemplateLink;
    },

    set_editTableTemplateLink: function (value) {
        if (this._editTableTemplateLink != value) {
            this._editTableTemplateLink = value;
        }
    },

    get_editListDetailTemplateLink: function () {
        return this._editListDetailTemplateLink;
    },

    set_editListDetailTemplateLink: function (value) {
        if (this._editListDetailTemplateLink != value) {
            this._editListDetailTemplateLink = value;
        }
    },

    get_editTableDetailTemplateLink: function () {
        return this._editTableDetailTemplateLink;
    },

    set_editTableDetailTemplateLink: function (value) {
        if (this._editTableDetailTemplateLink != value) {
            this._editTableDetailTemplateLink = value;
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

    get_editDetailTemplateLink2: function () {
        return this._editDetailTemplateLink2;
    },

    set_editDetailTemplateLink2: function (value) {
        if (this._editDetailTemplateLink2 != value) {
            this._editDetailTemplateLink2 = value;
        }
    },

    get_radWindowManager: function () {
        return this._radWindowManager;
    },

    set_radWindowManager: function (value) {
        if (this._radWindowManager != value) {
            this._radWindowManager = value;
        }
    },

    get_multiPage: function () {
        return this._multiPage;
    },

    set_multiPage: function (value) {
        if (this._multiPage != value) {
            this._multiPage = value;
        }
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListSettingsDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);



if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();