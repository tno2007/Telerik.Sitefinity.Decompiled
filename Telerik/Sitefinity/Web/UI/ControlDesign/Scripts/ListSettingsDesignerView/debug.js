/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("ListSettingsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");


Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView.initializeBase(this, [element]);
    this._dataFieldNameControlIdMap = null;
    this._controldataFieldNameMap = {};
    this._valueUpdatedDelegate = null;
    this._parentDesigner = null;
    this._refreshing = false;
    this._listLimitControl = null;
    this._currentViewName = null;
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
    this._radWindowManager = null;
    this._widgetEditorDialog = null;
    this._selectedTemplateId = null;
    this._editTemplateViewName = null;
    this._createTemplateViewName = null;
    this._modifyWidgetTemplatePermission = null;

    this._viewsList = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView.callBaseMethod(this, 'initialize');

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        if (this._onUnloadDelegate == null) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        }
        Sys.Application.add_load(this._onLoadDelegate);
        Sys.Application.add_unload(this._onUnloadDelegate);

        this._radioClickDelegate = Function.createDelegate(this, this._setMasterViewMode);
        this.get_radioChoices().click(this._radioClickDelegate);

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

        if (this._widgetEditorCloseDelegate == null) {
            this._widgetEditorCloseDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }
        jQuery('#countersDetailsLink').click(function () {
            jQuery('#countersDetails').toggleClass("sfDisplayNone");
        });
        // prevent memory leaks for jQuery
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        //Add custom dispose actions here
        Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView.callBaseMethod(this, 'dispose');
        if (this._valueUpdatedDelegate) {
            delete this._valueUpdatedDelegate;
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
        if (this._widgetEditorCloseDelegate) {
            delete this._widgetEditorCloseDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    refreshUI: function () {
        this._refreshing = true;
        var control = this.get_controlData();
        this._adjustControlData(control);
        var currentView = this.get_currentView();

        // if the view is set in the advanced mode it might be something that is not defined
        if (currentView.AllowPaging) {
            this._displayAllowPaging(currentView.ItemsPerPage);
        }
        else {
            if (currentView.ItemsPerPage > 0) {
                this._displayLimitedList(currentView.ItemsPerPage);
            }
            else {
                this._displayShowAllItems();
            }
        }

        var sortExpressionField = this._getFieldControl("CommonMasterDefinition.SortExpression");
        if (sortExpressionField._get_listItemByValue(currentView.SortExpression).length == 0) {
            sortExpressionField.set_value("custom");
        }
        else {
            sortExpressionField.set_value(currentView.SortExpression);
        }

        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);
        //        c.reset();
        //        c.clearListItems();

        //        var views = control.ControlDefinition.Views;
        //        for (var viewName in views) {
        //            var view = views[viewName];
        //            if (view && view.IsMasterView == true) {
        //                c.addListItem(view.ViewName, view.ViewName);
        //            }
        //        }

        if (this.get_currentView().TemplateKey) {
            c.set_value(this.get_currentView().TemplateKey);
        }
        else {
            var embeddedTemplateName = this.get_currentView().LayoutTemplateName;
            templateKey = this._embeddedTemplateMap[embeddedTemplateName];
            c.set_value(templateKey);
        }
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

    applyChanges: function () {
        var parent = this.get_parentDesigner();
        var control = this.get_controlData();
        var itemsPerPage = 0;
        var allowPaging = false;
        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var field = this._getFieldControl(dataFieldName);

            if (dataFieldName == "CommonMasterDefinition.ItemsPerPage") {
                if (field.get_value() > 0) {
                    itemsPerPage = field.get_value();
                    allowPaging = true;
                }
            }
            else if (dataFieldName == "ListLimit") {
                //this is work-around of having 2 fieldControls bound to the same field
                //we use ItemsPerPage - for both limiting top X items or having X items per page.
                if (field.get_value() > 0) {
                    itemsPerPage = field.get_value();
                }
            }
            else if (dataFieldName == "CommonMasterDefinition.SortExpression") {
                if (field.get_value() != "custom") {
                    parent.setValueToControlData(this._resolvePropertyPath(field), field.get_value(), control);
                }
            }
            else {
                parent.setValueToControlData(this._resolvePropertyPath(field), field.get_value(), control);
            }
        }
        this.get_currentView().ItemsPerPage = itemsPerPage;
        this.get_currentView().AllowPaging = allowPaging;
        this.get_currentView().TemplateKey = $find(this._dataFieldNameControlIdMap["TemplateKey"]).get_value();
    },

    /* --------------------------------- private methods --------------------------------- */

    // this method is executed when the page loads
    _onLoad: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).add_valueChanged(this._templateValueChangedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._widgetEditorCloseDelegate);
            //this._widgetEditorDialog.add_show(this._widgetEditorShowDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }

        //        this._valueUpdatedDelegate = Function.createDelegate(this, this._onControlValueUpdated);
        //        for (var dataFieldName in this._dataFieldNameControlIdMap) {
        //            var fieldControl = null;
        //            fieldControl = $find(this._dataFieldNameControlIdMap[dataFieldName]);
        //            if (fieldControl != null) {
        //                fieldControl.add_valueChanged(this._valueUpdatedDelegate);
        //                this._controldataFieldNameMap[fieldControl.get_id()] = dataFieldName;
        //            }
        //        }
    },

    _onUnload: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).remove_valueChanged(this._templateValueChangedDelegate);
        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            this._widgetEditorDialog.remove_close(this._widgetEditorCloseDelegate);
            //this._widgetEditorDialog.remove_show(this._widgetEditorShowDelegate);
            this._widgetEditorDialog.remove_pageLoad(this._widgetEditorShowDelegate);
        }
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
                    $(this.get_viewsList().get_element()).show();
                    dialogBase.resizeToContent();
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
                    ItemTypeFullNameField: "ClrType",
                    ItemTypeTitleField: "Title",
                    ItemTypeFullName: this._widgetEditorDialog._itemType,
                    ModuleName: "Products catalog"
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    // gets the reference to the field control by the field name that it edits
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataFieldNameControlIdMap[dataFieldName]);
    },

    _templateValueChanged: function (sender) {
        if (sender.get_value() == "") {
            jQuery(this.get_editTemplateLink()).hide();
        }
        else {
            jQuery(this.get_editTemplateLink()).show();
        }
        // TODO: Refactor this to set the embedded template path when template has not been changed
        this.get_currentView().TemplateKey = sender.get_value();
    },

    // sets the the Master view mode show all/paged/top n
    _setMasterViewMode: function (sender) {
        var radioID = sender.target.value;
        var currentView = this.get_currentView();
        var limitControl = this._getFieldControl('ListLimit');
        var itemsPerPageControl = this._getFieldControl('CommonMasterDefinition.ItemsPerPage');
        switch (radioID) {
            case "listRadio_showAll":
                if (!this._refreshing) {
                    currentView.ItemsPerPage = 0;
                    currentView.AllowPaging = false;
                }
                itemsPerPageControl.reset();
                this.disableElement(itemsPerPageControl.get_element());
                limitControl.reset();
                this.disableElement(limitControl.get_element());
                break;
            case "listRadio_showPaged":
                if (!this._refreshing) {
                    currentView.AllowPaging = true;
                }
                this.enableElement(itemsPerPageControl.get_element());
                limitControl.reset();
                this.disableElement(limitControl.get_element());

                break;
            case "listRadio_showLimited":
                if (!this._refreshing) {
                    currentView.AllowPaging = false;
                }
                itemsPerPageControl.reset();
                this.disableElement(itemsPerPageControl.get_element());
                this.enableElement(limitControl.get_element());
                break;
        }
        dialogBase.resizeToContent();
    },

    _onControlValueUpdated: function (sender, args) {
        if (this._refreshing)
            return;

        if (sender.get_dataFieldName() == 'ListLimit') {
            //this is work-around of having 2 fieldControls bound to the same field
            //we use ItemsPerPage - for both limiting top X items or having X items per page.
            this.get_currentView().ItemsPerPage = sender.get_value();
            return;
        }

        this.get_parentDesigner().setValueToControlData(this._resolvePropertyPath(sender), sender.get_value(), this.get_controlData());
    },

    // this function demonstrates on the user interface that paging is allowed
    _displayAllowPaging: function (itemsPerPage) {
        jQuery(this.get_element()).find("input[type|=radio][id$='listRadio_showPaged']").click();
        this._getFieldControl("CommonMasterDefinition.ItemsPerPage").set_value(itemsPerPage);
    },

    // this function demonstrated on the user interface that paging is not allowed, but that
    // the list should show display limited number of items
    _displayLimitedList: function (itemsPerPage) {
        this._getFieldControl("ListLimit").set_value(itemsPerPage);
        jQuery(this.get_element()).find("input[type|=radio][id$='listRadio_showLimited']").click();
    },

    // this function demonstrated on the user interface that all items should be shown
    _displayShowAllItems: function () {
        jQuery(this.get_element()).find("input[type|=radio][id$='listRadio_showAll']").click();
    },

    /* --------------------------------- properties --------------------------------- */

    // gets the reference to the control that sets the list limit settings
    get_listLimitControl: function () {
        return this._listLimitControl
    },

    // sets the reference to the control that sets the list limit settings
    set_listLimitControl: function (value) {
        this._listLimitControl = value;
    },

    // gets all the radio buttons in the container of this control
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find('input[type|=radio]');
        }
        return this._radioChoices;
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

    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
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

    get_viewsList: function () {
        return this._viewsList;
    },

    set_viewsList: function (value) {
        if (this._viewsList != value) {
            this._viewsList = value;
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

    // gets the name of the currently selected master view name of the content view control
    get_currentViewName: function () {
        return (this._currentViewName) ? this._currentViewName : this.get_controlData().MasterViewName;
    },

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        var currentViewName = this.get_currentViewName();
        var data = this.get_controlData();
        var views = data.ControlDefinition.Views;
        if (views.hasOwnProperty(currentViewName)) {
            return views[currentViewName];
        }
        else {
            views = data.ControlDefinition.Views;
            for (var v in views) {
                var current = views[v];
                if (current.IsMasterView) {
                    return current;
                }
            }
            return null;
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

    _resolvePropertyPath: function (fieldControl) {
        var dataFieldName = fieldControl.get_dataFieldName();
        var viewPath = "ControlDefinition.Views['" + this.get_currentViewName() + "']";
        var propertyPath = dataFieldName.replace('CommonMasterDefinition', viewPath);
        return propertyPath;
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

    enableElement: function (domElement) {
        $(domElement).find('input').each(function () {
            $(this).removeAttr('disabled');
        });
    },

    disableElement: function (domElement) {
        $(domElement).find('input').each(function () {
            $(this).attr('disabled', 'disabled');
        });
    }
}
Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
