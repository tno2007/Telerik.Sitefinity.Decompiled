Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._modeRadioButtonClientIDs = [];
    this._pagingRadioButtonClientIDs = [];
    this._pagingTextFieldClientIDs = [];
    this._limitRadioButtonClientIDs = [];
    this._noLimitAndPagingRadioButtonsClientIDs = [];
    this._limitTextFieldClientIDs = [];
    this._selectThumbSizeClientIDs = [];
    this._selectBigItemSizeClientIDs = [];
    //this._multiPage = null;
    this._currentViewName = null;
    this._currentMode = null;
    this._pageLoadDelegate = null;
    this._multiPage = null;
    this._masterViewNameMap = [];
    this._radWindowManager = null;
    this._widgetEditorDialog = null;
    this._selectedMasterTemplateId = null;
    this._selectedDetailTemplateId = null;
    this._widgetEditorCloseDelegate = null;
    this._widgetEditorShowDelegate = null;
    this._availableThumbnails = null;
    this._originalSizeImageKey = null;
    this._selectedLibrary = null;
    this._thumbnailServiceUrl = null;
    this._libraryType = null;
    this._modifyWidgetTemplatePermission = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView.callBaseMethod(this, "initialize");

        var radioHandlerDelegate = Function.createDelegate(this, this._modeRadioButtonCheckedChanged);
        var modeRadioCount = this._modeRadioButtonClientIDs.length;
        for (var i = 0; i < modeRadioCount; i++) {
            var radio = $get(this._modeRadioButtonClientIDs[i]);
            $addHandler(radio, "click", radioHandlerDelegate);
        }
        if (this._widgetEditorCloseDelegate == null) {
            this._widgetEditorCloseDelegate = Function.createDelegate(this, this._widgetEditorClosed);
        }
        if (this._widgetEditorShowDelegate == null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._widgetEditorShown);
        }

        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);
        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView.callBaseMethod(this, "dispose");
        if (this._pageLoadDelegate) {
            Sys.Application.remove_load(this._pageLoadDelegate);
            delete this._pageLoadDelegate;
        }
        if (this._widgetEditorCloseDelegate) {
            delete this._widgetEditorCloseDelegate;
        }
        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
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

        var linkId = "#detailCounterPage" + parseInt(newMode + 1);
        var tooltipId = "#counterToolTipPage" + parseInt(newMode + 1);
        $(linkId).unbind('click').bind('click', function () {
            jQuery(tooltipId).toggleClass("sfDisplayNone");
        });

        if (currentView.AllowPaging) {
            this._displayPagingForMode(newMode, currentView.ItemsPerPage);
        }
        else {
            if (currentView.ItemsPerPage > 0) {
                this._displayLimitedListForMode(newMode, currentView.ItemsPerPage);
            }
            else {
                var noPagingRadio = $get(this._noLimitAndPagingRadioButtonsClientIDs[newMode + 1]);
                $(noPagingRadio).click();
            }
        }
    },

    _modeRadioButtonCheckedChanged: function (sender, args) {
        var clickedRadioID = sender.target.value;
        switch (clickedRadioID) {
            case "rbMode1":
                this._currentMode = 0;
                break;
            case "rbMode2":
                this._currentMode = 1;
                break;
            case "rbMode3":
                this._currentMode = 2;
                break;
            case "rbMode4":
                this._currentMode = 3;
                break;
            default:
                this._currentMode = 0;
                break;
        }
        this.refreshCustomUI(this._currentMode);
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

    // this fixes the data if there are some incompatible values set in advanced mode
    _adjustControlData: function (data) {
        var view = data.ControlDefinition.Views[this.get_currentViewName()];
        if (!view) {
            var views = data.ControlDefinition.Views;
            var viewName;
            for (var key in views) {
                if (views[key].IsMasterView) {
                    viewName = key;
                    break;
                }
            }
            data.MasterViewName = viewName;
        }
    },

    // updates the UI (pageView for the given mode) with new values for Paging-related properties
    _displayPagingForMode: function (mode, itemsPerPage) {
        var pagingRadio = $get(this._pagingRadioButtonClientIDs[mode + 1]);
        if (pagingRadio) {
            pagingRadio.checked = true;
        }
        var textField = $find(this._pagingTextFieldClientIDs[mode + 1]);
        if (textField) {
            textField.set_value(itemsPerPage);
        }
    },

    // updates the UI (pageView for the given mode) with new values for Limit-related properties
    _displayLimitedListForMode: function (mode, itemsPerPage) {
        var limitRadio = $get(this._limitRadioButtonClientIDs[mode]);
        limitRadio.checked = true;
        var textField = $find(this._limitTextFieldClientIDs[mode]);
        textField.set_value(itemsPerPage);
    },

    _resolvePropertyPath: function (property) {
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        var propertyPath = viewPath + "." + property;
        return propertyPath;
    },

    _setPagingOrLimitToControlData: function (mode) {
        var currentModePagingRadio = $get(this._pagingRadioButtonClientIDs[mode + 1]);
        var parentDesigner = this.get_parentDesigner();
        var textField = null;
        if (currentModePagingRadio && currentModePagingRadio.checked) {
            // paging should be used
            parentDesigner.setValueToControlData(this._resolvePropertyPath("AllowPaging"), true, this.get_controlData());
            textField = $find(this._pagingTextFieldClientIDs[mode + 1]);
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

    // ----------------------------------------------- Public functions -----------------------------------------------

    // forces the designer to refresh the UI from the control Data
    refreshUI: function () {
        this._refreshing = true;
        var control = this.get_controlData();
        this._adjustControlData(control);
        var currentView = this.get_currentView();

        switch (currentView.ViewName) {
            case this._masterViewNameMap[0]:
                this._currentMode = 0;
                break;
            case this._masterViewNameMap[1]:
                this._currentMode = 1;
                break;
            case this._masterViewNameMap[2]:
                this._currentMode = 2;
                break;
            case this._masterViewNameMap[3]:
                this._currentMode = 3;
                break;
            default:
                this._currentMode = 0;
                break;
        }

        if (currentView.ThumbnailsWidth) {
            var selectThumbSize = $find(this._selectThumbSizeClientIDs[this._currentMode]);
            if (selectThumbSize) {
                selectThumbSize.set_value(currentView.ThumbnailsWidth);
            }
        }

        if (currentView.SingleItemWidth) {
            var selectBigItemSize = $find(this._selectBigItemSizeClientIDs[this._currentMode]);
            if (selectBigItemSize) {
                selectBigItemSize.set_value(currentView.SingleItemWidth);
            }
        }

        this.bindThumbnailSizesChoiceFields();
        this.bindBigImageSizesChoiceFields();

        this.refreshCustomUI(this._currentMode);
        this._changeMode(this._currentMode);
        this._refreshing = false;
    },

    // forces the designer to apply the changes on UI to the control Data
    applyChanges: function () {
        var parent = this.get_parentDesigner();
        var control = this.get_controlData();
        var currentViewName = this._masterViewNameMap[this._currentMode];

        // set the current view
        parent.setValueToControlData("MasterViewName", currentViewName, control);

        if (this._selectedMasterTemplateId) {
            this.get_currentView().TemplateKey = this._selectedMasterTemplateId;
        }
        if (this._selectedDetailTemplateId) {
            this.get_currentDetailView().TemplateKey = this._selectedDetailTemplateId;
        }

        this._setPagingOrLimitToControlData(this._currentMode);

        //set smaller image size - if present for the current view
        var selectThumbSize = $find(this._selectThumbSizeClientIDs[this._currentMode]);
        if (selectThumbSize) {
            parent.setValueToControlData(this._resolvePropertyPath("ThumbnailsName"), selectThumbSize.get_value(), control);
        }

        //set bigger image size - if present for the current view
        var selectBigImageSize = $find(this._selectBigItemSizeClientIDs[this._currentMode]);
        if (selectBigImageSize) {
            parent.setValueToControlData(this._resolvePropertyPath("SingleItemThumbnailsName"), selectBigImageSize.get_value(), control);
        }
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

    refreshCustomUI: function (newMode) {

    },

    bindThumbnailSizesChoiceFields: function () {
        var that = this;

        this.getAvailableThumbnailProfilesAsync(function (profiles) {
            for (var i = 0; i < that._selectThumbSizeClientIDs.length; ++i) {
                var selectThumbSizeField = $find(that._selectThumbSizeClientIDs[i]);
                var selectThumbSize = jQuery(selectThumbSizeField._choiceElement);
                var selectedValue = selectThumbSize.val();

                var choices = that.getThumbnailChoices(profiles);
                that.bindChoiceField(selectThumbSize, choices);

                var currentView = that.getView(i);
                if (currentView) {
                    if (currentView.ThumbnailsName) {
                        selectThumbSizeField.set_value(currentView.ThumbnailsName);
                    }
                    else if (selectedValue != null && typeof selectedValue !== 'undefined') {
                        selectThumbSizeField.set_value(selectedValue);
                    }
                    else {
                        selectThumbSizeField.set_value("");
                    }
                }
            }
        });
    },

    bindBigImageSizesChoiceFields: function () {
        var that = this;
        this.getAvailableThumbnailProfilesAsync(function (profiles) {
            for (var i = 0; i < that._selectBigItemSizeClientIDs.length; ++i) {
                var selectBigImagebSizeField = $find(that._selectBigItemSizeClientIDs[i]);
                var selectBigImageSize = jQuery(selectBigImagebSizeField._choiceElement);
                var selectedValue = selectBigImageSize.val();

                var choices = that.getBigImageChoices(profiles);
                that.bindChoiceField(selectBigImageSize, choices);

                var currentView = that.getView(i);
                if (currentView) {
                    if (currentView.SingleItemThumbnailsName) {
                        selectBigImagebSizeField.set_value(currentView.SingleItemThumbnailsName);
                    }
                    else if (selectedValue != null && typeof selectedValue !== 'undefined') {
                        selectBigImagebSizeField.set_value(selectedValue);
                    }
                    else {
                        selectBigImagebSizeField.set_value("");
                    }
                }
            }
        });
        
    },

    getThumbnailChoices: function (profiles) {

        var choice = { key: "", value: this.get_clientLabelManager().getLabel("LibrariesResources", "SelectImageSize") };
        if (profiles[0] && profiles[0].key != choice.key && profiles[0].value != choice.value) {
            profiles.splice(0, 0, choice);
        } else if (profiles.length < 1) {
            profiles.push(choice);
        }

        return profiles;
    },

    getBigImageChoices: function (profiles) {
        
        var choice = { key: "", value: this.get_clientLabelManager().getLabel("LibrariesResources", "OriginalSize") };
        if (profiles[0] && profiles[0].key != choice.key && profiles[0].value != choice.value) {
            profiles.splice(0, 0, choice);
        } else if (profiles.length < 1) {
            profiles.push(choice);
        }
        
        return profiles;
    },

    getAvailableThumbnailProfilesAsync: function (callback) {
        var profiles = [];
        var requestUrl = null;
        var viewType = this.get_controlData().ViewType;

        if (this._selectedLibrary) {
            var itemId = this._selectedLibrary.Id;
            var itemProvider = this._selectedLibrary.ProviderName;
            requestUrl = String.format("{0}/{1}/profiles?provider={2}&viewType={3}", this._thumbnailServiceUrl, itemId, itemProvider, viewType);
        }
        else {
            if (this._libraryType) {
                requestUrl = String.format("{0}/thumbnail-profiles/?libraryType={1}&viewType={2}", this._thumbnailServiceUrl, this._libraryType, viewType);
            }
        }
        if (this._libraryType) {
            jQuery.ajax({
                type: 'GET',
                url: requestUrl,
                processData: false,
                contentType: "application/json",
                success: function (data) {
                    var result = [];
                    for (var i = 0; i < data.Items.length; i++) {
                        var name = data.Items[i].Id;
                        var title = data.Items[i].Title;
                        result.push({ key: name, value: title });
                    }
                    profiles = result;
                },
                complete: function () {
                    callback(profiles);
                }
            });
        }
    },

    setSelectedLibrary: function (value) {
        this._selectedLibrary = value;
    },

    bindChoiceField: function (choiceElement, choices) {
        choiceElement.empty();

        for (var i = 0; i < choices.length; i++) {
            choiceElement.append($('<option></option>').val(choices[i].key).html(choices[i].value));
        }
    },

    getView: function (mode) {
        var viewName = this._masterViewNameMap[mode];
        if (viewName)
            return this.get_controlData().ControlDefinition.Views[viewName];
    },

    // ------------------------------------------------- Properties ----------------------------------------------------

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentViewName()];
    },

    // gets the name of the currently selected detail view of the content view control
    get_currentDetailViewName: function () {
        return (this._currentDetailViewName) ? this._currentDetailViewName : this.get_controlData().DetailViewName;
    },

    // gets the client side representation of the currently selected detail view definition
    get_currentDetailView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentDetailViewName()];
    },

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

    get_currentMode: function () {
        return this._currentMode;
    },

    set_currentMode: function (value) {
        if (this._currentMode != value) {
            this._currentMode = value;
            this.raisePropertyChanged("_currentMode");
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
    },

    //gets the client label manager that manages the localization strings 
    get_clientLabelManager: function () { return this._clientLabelManager; },
    set_clientLabelManager: function (value) { this._clientLabelManager = value; },



    get_thumbnailServiceUrl: function () {
        return this._thumbnailServiceUrl;
    },
    set_thumbnailServiceUrl: function (value) {
        this._thumbnailServiceUrl = value;
    }

}


Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSettingsDesignerView", Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

