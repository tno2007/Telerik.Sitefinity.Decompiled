﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner");

Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner = function (element) {
    this._showPageSelectorDelegate = null;
    this._pageSelectedDelegate = null;
    this._elementsCache = new Object();
    this._pageSelector = null;

    this._dataFieldNameControlIdMap = null;
    this._controldataFieldNameMap = {};
    this._valueUpdatedDelegate = null;
    this._refreshing = false;
    this._templateTitleElement = null;
    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._embeddedTemplateMap = [];

    this._templateValueChangedDelegate = null;
    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;
    this._widgetEditorDialogUrl = null;
    this._createTemplateLinkClickDelegate = null;
    this._editTemplateLinkClickDelegate = null;
    this._widgetEditorShowDelegate = null;
    this._widgetEditorCloseDelegate = null;
    this._modifyWidgetTemplatePermission = null;

    this._radWindowManager = null;
    this._widgetEditorDialog = null;
    this._selectedTemplateId = null;
    this._editTemplateViewName = null;
    this._createTemplateViewName = null;

    this._viewsList = null;
    this._currentViewName = null;
    this._defaultLayoutTemplateName = null;

    this._selectorTag = null;
    this._dialog = null;
    this._clientLabelManager = null;

    Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner.callBaseMethod(this, 'initialize');

        //hook Page selector
        this._showPageSelectorDelegate = Function.createDelegate(this, this._showPageSelector);
        $addHandler(this.get_pageSelectButton(), "click", this._showPageSelectorDelegate);

        //hook Page selector
        this._pageSelectedDelegate = Function.createDelegate(this, this._pageSelected);
        this.get_pageSelector().add_doneClientSelection(this._pageSelectedDelegate);

        // region edit/create templates
        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        }
        Sys.Application.add_load(this._onLoadDelegate);

        if (this._templateValueChangedDelegate == null) {
            this._templateValueChangedDelegate = Function.createDelegate(this, this._templateValueChanged);
        }
        if (this._createTemplateLinkClickDelegate == null) {
            this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        }
        $addHandler(this.get_createTemplateLink(), "click", this._createTemplateLinkClickDelegate, true);
        if (this._editTemplateLinkClickDelegate == null) {
            this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        }
        $addHandler(this.get_editTemplateLink(), "click", this._editTemplateLinkClickDelegate, true);

        if (this._widgetEditorShowDelegate == null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        }

        if (this._widgetEditorCloseDelegate == null) {
            this._widgetEditorCloseDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }

        //endregion

        // prevent memory leaks for jQuery
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        //initialize jquery UI dialod which chooses from which page the breadcrumb path will start from
        this._dialog = jQuery(this._selectorTag).dialog({
            autoOpen: false,
            modal: true,
            width: 395,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexL"
            }
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner.callBaseMethod(this, 'dispose');

        this._showPageSelectorDelegate = null;
        this._pageSelectedDelegate = null;
        this._templateValueChangedDelegate = null;
        this._onLoadDelegate = null;
        this._onUnloadDelegate = null;
        this._widgetEditorDialogUrl = null;
        this._createTemplateLinkClickDelegate = null;
        this._editTemplateLinkClickDelegate = null;
        this._widgetEditorShowDelegate = null;
        this._widgetEditorCloseDelegate = null;
    },

    refreshUI: function () {
        var controlData = this._propertyEditor.get_control();

        var that = this;
        // bind widget properites to designer
        jQuery(this.get_element()).delegate("#rbSelectFullPath", "click", function (e) {
            var $pageSelectorElement = jQuery(that.get_element()).find(".pageSelectZone");
            $pageSelectorElement.hide();
        });

        jQuery(this.get_element()).delegate("#rbSelectSpecificPath", "click", function (e) {
            var $pageSelectorElement = jQuery(that.get_element()).find(".pageSelectZone");
            $pageSelectorElement.show();
        });

        if (controlData.ShowFullPath) {
            var $showFullPathElement = jQuery(this.get_element()).find("#rbSelectFullPath");
            $showFullPathElement.click();
        }
        else {
            var $showSpecificPathElement = jQuery(this.get_element()).find("#rbSelectSpecificPath");
            $showSpecificPathElement.click();
        }

        if (this.get_controlData().SelectedPageTitle != null && this.get_controlData().SelectedPageTitle.length > 0) {
            this.get_selectedPageLabel().innerHTML = controlData.SelectedPageTitle;
            jQuery(this.get_selectedPageLabel()).show();
        }

        var showHomePageElement = jQuery(this.get_element()).find("#cbShowHomePage").get(0);
        showHomePageElement.checked = controlData.ShowHomePage;

        var showCurrentPageElement = jQuery(this.get_element()).find("#cbShowCurrentPage").get(0);
        showCurrentPageElement.checked = controlData.ShowCurrentPage;

        var showGroupPagesElement = jQuery(this.get_element()).find("#cbShowGroupPages").get(0);
        showGroupPagesElement.checked = controlData.ShowGroupPages;

        var breadcrumbLabelElement = jQuery(this.get_element()).find("#tbBreadcrumbLabel").get(0);
        breadcrumbLabelElement.value = controlData.BreadcrumbLabelText;

        // region edit/create templates
        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);

        if (controlData.TemplateKey) {
            c.set_value(controlData.TemplateKey);
        }
        else {

            templateKey = this._embeddedTemplateMap[this._defaultLayoutTemplateName];
            c.set_value(templateKey);
        }

        if (c.get_value() == "")
            jQuery(this.get_editTemplateLink()).hide();
        else {
            jQuery(this.get_editTemplateLink()).show();
        }

        // endregion
    },
    applyChanges: function () {

        var controlData = this._propertyEditor.get_control();

        // bind designer properties back to widget
        var selectFullPathElement = jQuery(this.get_element()).find("#rbSelectFullPath").get(0);
        controlData.ShowFullPath = selectFullPathElement.checked;

        var showHomePageElement = jQuery(this.get_element()).find("#cbShowHomePage").get(0)
        controlData.ShowHomePage = showHomePageElement.checked;

        var showCurrentPageElement = jQuery(this.get_element()).find("#cbShowCurrentPage").get(0);
        controlData.ShowCurrentPage = showCurrentPageElement.checked;

        var showGroupPagesElement = jQuery(this.get_element()).find("#cbShowGroupPages").get(0);
        controlData.ShowGroupPages = showGroupPagesElement.checked;

        var breadcrumbLabelElement = jQuery(this.get_element()).find("#tbBreadcrumbLabel").get(0);
        controlData.BreadcrumbLabelText = breadcrumbLabelElement.value;

        // region edit/create templates
        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);

        var option = $(c._choiceElement).find("option:selected").get(0);
        if (option) {
            controlData.TemplateKey = option.value;
        }
        // endregion
    },

    // shows the page selector
    _showPageSelector: function () {
        var selectedItem = this.get_controlData().StartingNodeId;
        if (selectedItem && selectedItem != Telerik.Sitefinity.getEmptyGuid()) {
            this.get_pageSelector().setSelectedItems([{ Id: selectedItem }]);
        }
        //jQuery(this.get_selectorTag()).show();
        this._dialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    // this is an event handler for page selected event    
    _pageSelected: function (items) {
        //jQuery(this.get_selectorTag()).hide();
        this._dialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        if (items == null)
            return;

        var selectedItem = this.get_pageSelector().getSelectedItems()[0];
        if (selectedItem) {
            this.get_selectedPageLabel().innerHTML = selectedItem.Title;
            jQuery(this.get_selectedPageLabel()).show();
            this.get_pageSelectButton().innerHTML = this.get_clientLabelManager().getLabel("Labels", "ChangePageButton");
            this.get_controlData().StartingNodeId = selectedItem.Id;
        }
    },

    _templateValueChanged: function (sender) {
        var option = $(sender._choiceElement).find("option:selected").get(0);
        if (option) {
            jQuery(this.get_editTemplateLink()).show();
            this.get_currentView().TemplateKey = option.value;
        }
    },

    _createTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = null;
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                $("body").removeClass("sfSelectorDialog");
                dialogBase.get_radWindow().maximize();
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();                
            }
        } else {
            alert("You don't have the permissions to create new widgets templates.");
        }
    },

    _editTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);
            var option = $(c._choiceElement).find("option:selected").get(0);
            this._selectedTemplateId = option.value;
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                $("body").removeClass("sfSelectorDialog");
                dialogBase.get_radWindow().maximize();
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
            }
        } else {
            alert("You don't have the permissions to edit widgets templates.");
        }
    },

    _onWidgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog) {
                var params =
                {
                    TemplateId: this._selectedTemplateId,
                    ControlType: "Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Breadcrumb",
                    //   ItemTypeFullName: "Telerik.Sitefinity.Blogs.Model.Blog",
                    BlackListControlTemplateEditor: true
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
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
                    }
                }
            }
        }
    },

    // this method is executed when the page loads
    _onLoadHandler: function () {

        $find(this._dataFieldNameControlIdMap["TemplateKey"]).add_valueChanged(this._templateValueChangedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    //get the page selector component
    get_pageSelector: function () {
        return this._pageSelector;
    },
    //sets the page selector component
    set_pageSelector: function (val) {
        this._pageSelector = val;
    },

    //get the page selector open button
    get_pageSelectButton: function () {
        if (this._pageSelectButton == null) {
            this._pageSelectButton = this.findElement("pageSelectButton");
        }
        return this._pageSelectButton;
    },

    //gets the page selector wrapping element(use to hide and show the selector)
    get_selectorTag: function () {
        if (this._selectorTag == null)
            this._selectorTag = this.findElement("selectorTag");
        return this._selectorTag;

    },

    //gets the selected page label
    get_selectedPageLabel: function () {
        if (this._selectedPageLabel == null) {
            this._selectedPageLabel = this.findElement("selectedPageLabel");
        }
        return this._selectedPageLabel;
    },

    findElement: function (id) {

        if (typeof (this._elementsCache[id]) !== 'undefined')
            return this._elementsCache[id];
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        this._elementsCache[id] = result;
        return result;
    },

    get_currentView: function () {
        return "Breadcrumb"
    },

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {

        return "Breadcrumb";
    },

    // gets the object that represents the client side representation of the control 
    // being edited
    get_controlData: function () {
        return this._propertyEditor.get_control();
    },

    _disableAndClearTxtFields: function () {
        jQuery(this._txtMonths).attr("disabled", "disabled");
        jQuery(this._txtPosts).attr("disabled", "disabled");

    },

    _disableAndClearTxtMonths: function () {
        jQuery(this._txtMonths).attr("disabled", "disabled");
        jQuery(this._txtPosts).removeAttr("disabled");
    },

    _disableAndClearTxtPosts: function () {
        jQuery(this._txtPosts).attr("disabled", "disabled");
        jQuery(this._txtMonths).removeAttr("disabled");
    },

    /* --------------------------------- properties -------------------------------------- */

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

    get_txtPosts: function () {
        return this._txtPosts;
    },

    set_txtPosts: function (value) {
        if (this._txtPosts != value)
            this._txtPosts = value;
    },

    get_radWindowManager: function () {
        return this._radWindowManager;
    },
    // sets the reference to the rad window manager used by this designer
    set_radWindowManager: function (value) {
        this._radWindowManager = value;
    },
    get_viewsList: function () {
        return this._viewsList;
    },

    set_viewsList: function (value) {
        if (this._viewsList != value) {
            this._viewsList = value;
        }
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

    //gets and sets html wrapper of page selector from which the breadcrumb path will start from
    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner.registerClass('Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.BreadcrumbDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

