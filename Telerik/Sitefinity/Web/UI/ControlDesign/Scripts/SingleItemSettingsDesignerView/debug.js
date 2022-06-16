/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("SingleItemSettingsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView.initializeBase(this, [element]);
    this._parentDesigner = null;
    this._dataFieldNameControlIdMap = null;
    this._pageSelectButton = null;
    this._showPageSelectorDelegate = null;
    this._pagesSelector = null;
    this._pageSelectedDelegate = null;
    this._radioClickDelegate = null;
    this._radioChoices = null;
    this._selectedPageLabel = null;
    this._refreshing = false;
    this._templateTitleElement = null;
    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._widgetEditorDialogUrl = null;

    this._templateValueChangedDelegate = null;
    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;
    this._radWindowManager = null;
    this._createTemplateLinkClickDelegate = null;
    this._editTemplateLinkClickDelegate = null;
    this._widgetEditorShowDelegate = null;
    this._onWidgetEditorClosedDelegate = null;
    this._selectedTemplateId = null;
    this._editTemplateViewName = null;
    this._createTemplateViewName = null;
    this._modifyWidgetTemplatePermission = null;

    this._selectorTag = null;
    this._dialog = null;

    this._viewsList = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {

        Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView.callBaseMethod(this, 'initialize');

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        if (this._onUnloadDelegate == null) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        }

        Sys.Application.add_load(this._onLoadDelegate);
        Sys.Application.add_unload(this._onUnloadDelegate);

        this._valueUpdatedDelegate = Function.createDelegate(this, this._onControlValueUpdated);

        this._showPageSelectorDelegate = Function.createDelegate(this, this._showPageSelector);
        $addHandler(this._pageSelectButton, "click", this._showPageSelectorDelegate);

        this._radioClickDelegate = Function.createDelegate(this, this._setDetailPageMode);
        this.get_radioChoices().click(this._radioClickDelegate);

        this._pageSelectedDelegate = Function.createDelegate(this, this._pageSelected);
        this._pagesSelector.add_doneClientSelection(this._pageSelectedDelegate);

        if (this._templateValueChangedDelegate == null) {
            this._templateValueChangedDelegate = Function.createDelegate(this, this._templateValueChanged);
        }

        if (this._createTemplateLinkClickDelegate == null) {
            this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        }
        $addHandler(this.get_createTemplateLink(), "click", this._createTemplateLinkClickDelegate);
        if (this._editTemplateLinkClickDelegate == null) {
            this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        }
        $addHandler(this.get_editTemplateLink(), "click", this._editTemplateLinkClickDelegate);

        if (this._widgetEditorShowDelegate == null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        }

        if (this._onWidgetEditorClosedDelegate == null) {
            this._onWidgetEditorClosedDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }

        jQuery('#countersDetailsLinkSingle').click(function () {
            jQuery('#countersDetailsSingle').toggleClass("sfDisplayNone");
        });

        // prevent memory leaks for jQuery
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

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
        //Add custom dispose actions here
        Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView.callBaseMethod(this, 'dispose');
        if (this._valueUpdatedDelegate) {
            delete this._valueUpdatedDelegate;
        }
        if (this._showPageSelectorDelegate) {
            delete this._showPageSelectorDelegate;
        }
        this._pagesSelector.remove_doneClientSelection(this._pageSelectedDelegate);
        if (this._pageSelectedDelegate) {
            delete this._pageSelectedDelegate;
        }
        if (this._radioClickDelegate) {
            delete this._radioClickDelegate;
        }
        if (this._templateValueChangedDelegate) {
            delete this._templateValueChangedDelegate;
        }
        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }
        if (this._onUnloadDelegate) {
            delete this._onUnloadDelegate;
        }
        $removeHandler(this._editTemplateLink, "click", this._createTemplateLinkClickDelegate);
        if (this._createTemplateLinkClickDelegate) {
            delete this._createTemplateLinkClickDelegate;
        }
        $removeHandler(this._editTemplateLink, "click", this._editTemplateLink);
        if (this._editTemplateLinkClickDelegate) {
            delete this._editTemplateLinkClickDelegate;
        }
        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }
    },

    /* --------------------------------- public functions --------------------------------- */

    applyChanges: function () {
        // TODO: implement
        this.get_currentView().TemplateKey = $find(this._dataFieldNameControlIdMap["TemplateKey"]).get_value();
    },

    refreshUI: function () {
        this._refreshing = true;
        var controlData = this.get_controlData();
        this._adjustControlData(controlData);
        var masterView = this.getMasterView();
        var currentView = this.get_currentView();

        if (masterView.RenderLinksInMasterView == false && this.get_radioChoices().length == 3) {
            this.get_radioChoices()[2].click();
        }
        else if (masterView.DetailsPageId == null || masterView.DetailsPageId == '00000000-0000-0000-0000-000000000000') {
            this.get_radioChoices()[0].click();
        }
        else {
            this.get_radioChoices()[1].click();
        }

        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);
        //        c.reset();
        //        c.clearListItems();

        //        var views = controlData.ControlDefinition.Views;
        //        for (var viewName in views) {
        //            var view = views[viewName];
        //            if (view && view.IsMasterView == false) {
        //                c.addListItem(view.ViewName, view.ViewName);
        //            }
        //        }

        c.set_value(currentView.TemplateKey);
        var option = $(c._choiceElement).find("option:selected").get(0);
        if (option) {
            this._templateTitleElement.innerHTML = option.text;
        }
        if (c.get_value() == "")
            jQuery(this.get_editTemplateLink()).hide();
        else {
            jQuery(this.get_editTemplateLink()).show();
            jQuery(this.get_templateTitleElement()).show();
            jQuery(this.get_templateTitleElement()).attr("title", c.get_value());
        }

        this._refreshing = false;
    },

    /* --------------------------------- event handlers --------------------------------- */

    // this is an event handler for page selected event    
    _pageSelected: function (items) {

        //jQuery(this.get_element()).find('#selectorTag').hide();
        this._dialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        if (items == null)
            return;

        var selectedItem = this.get_pagesSelector().getSelectedItems()[0];
        if (selectedItem) {
            var masterView = this.getMasterView();
            this.get_selectedPageLabel().innerText = selectedItem.Title;
            jQuery(this.get_selectedPageLabel()).show();
            this.get_pageSelectButton().innerHTML = 'Change page';
            masterView.DetailsPageId = selectedItem.Id;
        }
    },

    // this is an event handler that handles the value updated event of the field control
    _onControlValueUpdated: function (sender, args) {
        if (this._refreshing) {
            return;
        }

        var dataFieldName = sender.get_dataFieldName();
        var value = sender.get_value();

        switch (dataFieldName) {
            case "TemplateKey":
                this.get_controlData().TemplateKey = value;
                break;
        }

        if (sender.get_value() == "") {
            jQuery(this.get_editTemplateLink()).hide();
        }
        else {
            jQuery(this.get_editTemplateLink()).show();
        }
        // TODO: Refactor this to set the embedded template path when template has not been changed
        this.get_currentView().TemplateKey = sender.get_value();
    },

    /* --------------------------------- private functions --------------------------------- */

    // this method is executed when the page is ready and all client components have been initialized
    _onLoad: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).add_valueChanged(this._valueUpdatedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._onWidgetEditorClosedDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _onUnload: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).remove_valueChanged(this._valueUpdatedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            this._widgetEditorDialog.remove_close(this._onWidgetEditorClosedDelegate);
            this._widgetEditorDialog.remove_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    // shows the page selector
    _showPageSelector: function () {
        var masterView = this.getMasterView();
        if (masterView && masterView.DetailsPageId != Telerik.Sitefinity.getEmptyGuid()) {
            this.get_pagesSelector().setSelectedItems([{ Id: masterView.DetailsPageId }]);
        }
        //jQuery(this.get_element()).find('#selectorTag').show();
        this._dialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    // sets the content filter setting based on the radio button that selected the
    // filter type
    _setDetailPageMode: function (sender) {
        var radioID = sender.target.value;
        var controlData = this.get_controlData();
        var currentView = this.getMasterView();
        //var currentView = this.get_currentView();
        jQuery(this.get_element()).find('#detailsTemplateContainer').show();
        switch (radioID) {
            case "itemPageSetting_currentPage":
                jQuery(this.get_element()).find('#pageSelectGroup').hide();
                currentView.DetailsPageId = '00000000-0000-0000-0000-000000000000';
                currentView.RenderLinksInMasterView = true;
                break;
            case "itemPageSetting_newPage":
                jQuery(this.get_element()).find('#pageSelectGroup').show();
                jQuery(this.get_element()).find('#detailsTemplateContainer').hide();
                currentView.RenderLinksInMasterView = true;
                break;
            case "itemPageSettings_noPage":
                jQuery(this.get_element()).find('#pageSelectGroup').hide();
                currentView.DetailsPageId = '00000000-0000-0000-0000-000000000000';
                currentView.RenderLinksInMasterView = false;
                break;
        }
        dialogBase.resizeToContent();
    },

    _createTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            //var dialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
            this._selectedTemplateId = null;
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                dialogBase.get_radWindow().maximize();
                //dialog.add_close(this._onWidgetEditorClosed);
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
                //Telerik.Sitefinity.centerWindowHorizontally(dialog);
                $("body").removeClass("sfSelectorDialog");
            }
        } else {
            alert("You don't have the permissions to create new widgets templates.");
        }
    },

    _editTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            //var dialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
            var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);
            var option = $(c._choiceElement).find("option:selected").get(0);
            this._selectedTemplateId = option.value;
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                dialogBase.get_radWindow().maximize();
                //dialog.add_close(this._onWidgetEditorClosed);
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
                $("body").removeClass("sfSelectorDialog");
            }
        } else {
            alert("You don't have the permissions to edit widgets templates.");
        }
    },

    _onWidgetEditorClosed: function (sender, args) {
        dialogBase.get_radWindow().Restore();
        $("body").addClass("sfSelectorDialog");

        if (args && args.get_argument) {
            var arg = args.get_argument();
            if (arg) {
                if (arg.IsCreated) {
                    var widgetName = arg.DataItem.Name;
                    var widgetId = arg.DataItem.Id;

                    this.get_viewsList().addListItem(widgetId, widgetName);
                } else if (arg.IsUpdated) {
                    var selectedChoices = this.get_viewsList()._get_selectedListItemsElements();
                    if (selectedChoices) {
                        var selectedChoice = selectedChoices[0];
                        var newName = arg.DataItem.Name;

                        selectedChoice.text = newName;
                        this._templateTitleElement.innerHTML = newName;
                        jQuery(this.get_templateTitleElement()).attr("title", newName);
                    }
                }
            }
        }
    },

    _onWidgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //            if (itemsList.get_scrollOpenedDialogsToTop()) {
            //                frameHandle.scrollTo(0, 0);
            //            }
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog) {
                var data = this.get_controlData();
                var params =
                {
                    TemplateId: this._selectedTemplateId,
                    ControlType: this.get_currentView().ViewType,
                    BlackListControlTemplateEditor: true,
                    ModuleName: data.ModuleName
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    /* --------------------------------- properties functions --------------------------------- */

    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // gets the object which represents the map of field properties and respective controls
    // that are used to edit them
    set_dataFieldNameControlIdMap: function (value) {
        this._dataFieldNameControlIdMap = value;
    },

    // sets the object which represents the map of field properties and respective controls
    // that are used to edit them
    get_dataFieldNameControlIdMap: function () {
        return this._dataFieldNameControlIdMap;
    },

    // gets the reference to the button that opens page selector
    get_pageSelectButton: function () {
        return this._pageSelectButton;
    },

    // sets the reference to the button that opens page selector
    set_pageSelectButton: function (value) {
        this._pageSelectButton = value;
    },

    // gets the reference to the pages selector used to choose a page for showing the detail mode
    get_pagesSelector: function () {
        return this._pagesSelector;
    },

    // sets the reference to the pages selector used to choose a page for showing the detail mode
    set_pagesSelector: function (value) {
        this._pagesSelector = value;
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

    //gets the label control to show the selcted page title (or url)
    get_selectedPageLabel: function () {
        return this._selectedPageLabel;
    },
    //sets the label control to show the selcted page title (or url)
    set_selectedPageLabel: function (value) {
        this._selectedPageLabel = value;
    },

    get_viewsList: function () {
        return this._viewsList;
    },

    set_viewsList: function (value) {
        if (this._viewsList != value) {
            this._viewsList = value;
        }
    },

    get_templateTitleElement: function () {
        return this._templateTitleElement;
    },

    set_templateTitleElement: function (value) {
        if (this._templateTitleElement != value) {
            this._templateTitleElement = value;
        }
    },

    get_editTemplateLink: function () {
        return this._editTemplateLink;
    },

    set_editTemplateLink: function (value) {
        if (this._editTemplateLink != value) {
            this._editTemplateLink = value;
        }
    },

    get_createTemplateLink: function () {
        return this._createTemplateLink;
    },

    set_createTemplateLink: function (value) {
        if (this._createTemplateLink != value) {
            this._createTemplateLink = value;
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

    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    },

    // gets the name of the currently selected detail view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().DetailViewName;
    },

    // gets the client side representation of the currently selected detail view definition
    getMasterView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_controlData().MasterViewName];
    },

    //
    get_currentView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_currentViewName()];
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
            data.DetailViewName = viewName;
        }
    },

    // gets all the radio buttons in the container of this control
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find('input[type|=radio]');
        }
        return this._radioChoices;
    }
}
Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.SingleItemSettingsDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
