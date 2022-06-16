/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("DynamicTypeTemplatesSelectorDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView.initializeBase(this, [element]);
    //common fields
    this._parentDesigner = null;
    this._dataFieldNameControlIdMap = null;
    this._refreshing = false;
    this._widgetEditorDialogUrl = null;
    this._onLoadDelegate = null;
    this._onUnloadDelegate = null;
    this._radWindowManager = null;
    this._widgetEditorShowDelegate = null;
    this._onWidgetEditorClosedDelegate = null;
    this._editTemplateViewName = null;
    this._createTemplateViewName = null;
    this._widgetEditorDialog = null;
    this._currentEditedTemplateType = null;
    this._modifyWidgetTemplatePermission = null;

    //detail templates
    this._detailViewsList = null;
    this._detailTemplateTitleElement = null;
    this._editDetailTemplateLink = null;
    this._createDetailTemplateLink = null;
    this._createDetailTemplateLinkClickDelegate = null;
    this._editDetailTemplateLinkClickDelegate = null;
    this._detailValueUpdatedDelegate = null;
    this._selectedDetailTemplateId = null;
    this._detailTemplateId = null;

    //master templates
    this._masterViewsList = null;
    this._masterTemplateTitleElement = null;
    this._editMasterTemplateLink = null;
    this._createMasterTemplateLink = null;
    this._createMasterTemplateLinkClickDelegate = null;
    this._editMasterTemplateLinkClickDelegate = null;
    this._masterValueUpdatedDelegate = null;
    this._selectedMasterTemplateId = null;
    this._masterTemplateId = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView.callBaseMethod(this, 'initialize');

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        Sys.Application.add_load(this._onLoadDelegate);

        if (this._onUnloadDelegate == null) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        }
        Sys.Application.add_unload(this._onUnloadDelegate);

        if (this._createDetailTemplateLinkClickDelegate == null) {
            this._createDetailTemplateLinkClickDelegate = Function.createDelegate(this, this._createDetailTemplateLinkClicked);
        }
        $addHandler(this.get_createDetailTemplateLink(), "click", this._createDetailTemplateLinkClickDelegate);

        if (this._createMasterTemplateLinkClickDelegate == null) {
            this._createMasterTemplateLinkClickDelegate = Function.createDelegate(this, this._createMasterTemplateLinkClicked);
        }
        $addHandler(this.get_createMasterTemplateLink(), "click", this._createMasterTemplateLinkClickDelegate);

        if (this._editDetailTemplateLinkClickDelegate == null) {
            this._editDetailTemplateLinkClickDelegate = Function.createDelegate(this, this._editDetailTemplateLinkClicked);
        }
        $addHandler(this.get_editDetailTemplateLink(), "click", this._editDetailTemplateLinkClickDelegate);

        if (this._editMasterTemplateLinkClickDelegate == null) {
            this._editMasterTemplateLinkClickDelegate = Function.createDelegate(this, this._editMasterTemplateLinkClicked);
        }
        $addHandler(this.get_editMasterTemplateLink(), "click", this._editMasterTemplateLinkClickDelegate);

        this._detailValueUpdatedDelegate = Function.createDelegate(this, this._onDetailControlValueUpdated);
        this._masterValueUpdatedDelegate = Function.createDelegate(this, this._onMasterControlValueUpdated);

        if (this._widgetEditorShowDelegate == null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        }

        if (this._onWidgetEditorClosedDelegate == null) {
            this._onWidgetEditorClosedDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }

        this._detailTemplateId = this.get_detailView().TemplateKey;
        this._masterTemplateId = this.get_masterView().TemplateKey;

        // prevent memory leaks for jQuery
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },
    dispose: function () {
        //Add custom dispose actions here
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }
        if (this._onUnloadDelegate) {
            delete this._onUnloadDelegate;
        }
        $removeHandler(this._editDetailTemplateLink, "click", this._createDetailTemplateLinkClickDelegate);
        if (this._createDetailTemplateLinkClickDelegate) {
            delete this._createDetailTemplateLinkClickDelegate;
        }
        $removeHandler(this._editMasterTemplateLink, "click", this._createMasterTemplateLinkClickDelegate);
        if (this._createMasterTemplateLinkClickDelegate) {
            delete this._createMasterTemplateLinkClickDelegate;
        }

        $removeHandler(this._editDetailTemplateLink, "click", this._editDetailTemplateLink);
        $removeHandler(this._editMasterTemplateLink, "click", this._editMasterTemplateLink);

        if (this._editDetailTemplateLinkClickDelegate) {
            delete this._editDetailTemplateLinkClickDelegate;
        }
        if (this._editMasterTemplateLinkClickDelegate) {
            delete this._editMasterTemplateLinkClickDelegate;
        }

        if (this._detailValueUpdatedDelegate) {
            delete this._detailValueUpdatedDelegate;
        }
        if (this._masterValueUpdatedDelegate) {
            delete this._masterValueUpdatedDelegate;
        }
        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }
        if (this._onWidgetEditorClosedDelegate) {
            delete this._onWidgetEditorClosedDelegate;
        }
    },
    // this method is executed when the page is ready and all client components have been initialized
    _onLoad: function () {
        $find(this._dataFieldNameControlIdMap["DetailTemplateKey"]).add_valueChanged(this._detailValueUpdatedDelegate);
        $find(this._dataFieldNameControlIdMap["MasterTemplateKey"]).add_valueChanged(this._masterValueUpdatedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._onWidgetEditorClosedDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },
    _onUnload: function () {
        $find(this._dataFieldNameControlIdMap["DetailTemplateKey"]).remove_valueChanged(this._detailValueUpdatedDelegate);
        $find(this._dataFieldNameControlIdMap["MasterTemplateKey"]).remove_valueChanged(this._masterValueUpdatedDelegate);

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            this._widgetEditorDialog.remove_close(this._onWidgetEditorClosedDelegate);
            this._widgetEditorDialog.remove_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    /* --------------------------------- public functions --------------------------------- */

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        this.get_detailView().TemplateKey = this._detailTemplateId;
        this.get_masterView().TemplateKey = this._masterTemplateId;
    },
    // forces the designer to refresh the UI from the control Data
    refreshUI: function () {
        this._refreshing = true;
        var controlData = this.get_controlData();
        this._adjustControlData(controlData);

        //set detail templates
        var detailControl = $find(this._dataFieldNameControlIdMap["DetailTemplateKey"]);
        detailControl.set_value(this._detailTemplateId);
        var option = $(detailControl._choiceElement).find("option:selected").get(0);
        if (option) {
            this._detailTemplateTitleElement.innerHTML = option.text;
        }
        if (detailControl.get_value() == "")
            jQuery(this.get_editDetailTemplateLink()).hide();
        else {
            jQuery(this.get_editDetailTemplateLink()).show();
            jQuery(this.get_detailTemplateTitleElement()).show();
            jQuery(this.get_detailTemplateTitleElement()).attr("title", detailControl.get_value());
        }

        //set master templates
        var masterControl = $find(this._dataFieldNameControlIdMap["MasterTemplateKey"]);
        masterControl.set_value(this._masterTemplateId);
        var option = $(masterControl._choiceElement).find("option:selected").get(0);
        if (option) {
            this._masterTemplateTitleElement.innerHTML = option.text;
        }
        if (masterControl.get_value() == "")
            jQuery(this.get_editMasterTemplateLink()).hide();
        else {
            jQuery(this.get_editMasterTemplateLink()).show();
            jQuery(this.get_masterTemplateTitleElement()).show();
            jQuery(this.get_masterTemplateTitleElement()).attr("title", masterControl.get_value());
        }

        this._refreshing = false;
    },

    /* --------------------------------- event handlers --------------------------------- */

    // this is an event handler that handles the value updated event of the detail field control
    _onDetailControlValueUpdated: function (sender, args) {
        if (this._refreshing) {
            return;
        }
        var value = sender.get_value();
        if (value) {
            jQuery(this.get_editDetailTemplateLink()).show();
        }
        else {
            jQuery(this.get_editDetailTemplateLink()).hide();
        }
        this._detailTemplateId = value;
    },
    // this is an event handler that handles the value updated event of the master field control
    _onMasterControlValueUpdated: function (sender, args) {
        if (this._refreshing) {
            return;
        }
        var value = sender.get_value();
        if (value) {
            jQuery(this.get_editMasterTemplateLink()).show();
        }
        else {
            jQuery(this.get_editMasterTemplateLink()).hide();
        }
        this._masterTemplateId = value;
    },
    // this is an event handler that handles the widget editor close event
    _onWidgetEditorClosed: function (sender, args) {
        dialogBase.get_radWindow().Restore();
        $("body").addClass("sfSelectorDialog");

        if (args && args.get_argument) {
            var arg = args.get_argument();
            if (arg) {
                if (arg.IsCreated) {
                    //we have created a new widget template - add it to the dropdown list
                    var widgetName = arg.DataItem.Name;
                    var widgetId = arg.DataItem.Id;
                    if (this._currentEditedTemplateType == 'Detail') {
                        this.get_detailViewsList().addListItem(widgetId, widgetName);
                        $(this.get_detailViewsList().get_element()).show();
                    }
                    else {
                        this.get_masterViewsList().addListItem(widgetId, widgetName);
                        $(this.get_masterViewsList().get_element()).show();
                    }
                }
                else if (arg.IsUpdated) {
                    //we have edited a widget template - update the dropdown list
                    if (this._currentEditedTemplateType == 'Detail') {
                        var selectedChoices = this.get_detailViewsList()._get_selectedListItemsElements();
                        if (selectedChoices) {
                            var selectedChoice = selectedChoices[0];
                            var newName = arg.DataItem.Name;
                            selectedChoice.text = newName;
                            this._detailTemplateTitleElement.innerHTML = newName;
                            jQuery(this.get_detailTemplateTitleElement()).attr("title", newName);
                        }
                    }
                    else {
                        var selectedChoices = this.get_masterViewsList()._get_selectedListItemsElements();
                        if (selectedChoices) {
                            var selectedChoice = selectedChoices[0];
                            var newName = arg.DataItem.Name;
                            selectedChoice.text = newName;
                            this._masterTemplateTitleElement.innerHTML = newName;
                            jQuery(this.get_masterTemplateTitleElement()).attr("title", newName);
                        }
                    }
                }
            }
        }
        this._currentEditedTemplateType = null;
    },
    // this is an event handler that handles the widget editor show event
    _onWidgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog) {
                var data = this.get_controlData();
                if (this._currentEditedTemplateType == 'Detail') {
                    var params =
					{
					    TemplateId: this._selectedDetailTemplateId,
					    ControlType: this.get_detailView().ViewType,
					    BlackListControlTemplateEditor: true,
					    ModuleName: data.ModuleName
					};
                }
                else {
                    var params =
					{
					    TemplateId: this._selectedMasterTemplateId,
					    ControlType: this.get_masterView().ViewType,
					    BlackListControlTemplateEditor: true,
					    ModuleName: data.ModuleName
					};
                }
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },
    // this is an event handler that handles the create detail template link click event
    _createDetailTemplateLinkClicked: function (sender, args) {
        this._selectedDetailTemplateId = null;
        if (this._widgetEditorDialog) {
            this._currentEditedTemplateType = 'Detail';
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
            this._initAndShowEditorDialog(dialogUrl);
        }
    },
    // this is an event handler that handles the create master template link click event
    _createMasterTemplateLinkClicked: function (sender, args) {
        this._selectedMasterTemplateId = null;
        if (this._widgetEditorDialog) {
            this._currentEditedTemplateType = 'Master';
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
            this._initAndShowEditorDialog(dialogUrl);
        }
    },
    // this is an event handler that handles the edit detail template link click event
    _editDetailTemplateLinkClicked: function (sender, args) {
        var c = $find(this._dataFieldNameControlIdMap["DetailTemplateKey"]);
        var option = $(c._choiceElement).find("option:selected").get(0);
        this._selectedDetailTemplateId = option.value;
        if (this._widgetEditorDialog) {
            this._currentEditedTemplateType = 'Detail';
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
            this._initAndShowEditorDialog(dialogUrl);
        }
    },
    // this is an event handler that handles the edit master template link click event
    _editMasterTemplateLinkClicked: function (sender, args) {
        var c = $find(this._dataFieldNameControlIdMap["MasterTemplateKey"]);
        var option = $(c._choiceElement).find("option:selected").get(0);
        this._selectedMasterTemplateId = option.value;
        if (this._widgetEditorDialog) {
            this._currentEditedTemplateType = 'Master';
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
            this._initAndShowEditorDialog(dialogUrl);
        }
    },

    /* --------------------------------- private functions --------------------------------- */

    //initialize and display edit template dialog
    _initAndShowEditorDialog: function (dialogUrl) {
        if (this._modifyWidgetTemplatePermission) {
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            dialogBase.get_radWindow().maximize();
            this._widgetEditorDialog.show();
            this._widgetEditorDialog.maximize();
            $("body").removeClass("sfSelectorDialog");
        } else {
            alert("You don't have the permissions to create and modify widgets templates.");
        }
    },
    // this fixes the data if there are some incompatible values set in advanced mode 
    _adjustControlData: function (data) {
        var detailView = data.ControlDefinition.Views[this.get_detailViewName()];
        if (!detailView) {
            var views = data.ControlDefinition.Views;
            var detailViewName;
            for (var key in views) {
                if (!views[key].IsMasterView) {
                    detailViewName = key;
                    break;
                }
            }
            data.DetailViewName = detailViewName;
        }

        var masterView = data.ControlDefinition.Views[this.get_masterViewName()];
        if (!masterView) {
            var views = data.ControlDefinition.Views;
            var masterViewName;
            for (var key in views) {
                if (views[key].IsMasterView) {
                    masterViewName = key;
                    break;
                }
            }
            data.MasterViewName = masterViewName;
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
    get_detailViewsList: function () {
        return this._detailViewsList;
    },
    set_detailViewsList: function (value) {
        if (this._detailViewsList != value) {
            this._detailViewsList = value;
        }
    },
    get_masterViewsList: function () {
        return this._masterViewsList;
    },
    set_masterViewsList: function (value) {
        if (this._masterViewsList != value) {
            this._masterViewsList = value;
        }
    },
    get_detailTemplateTitleElement: function () {
        return this._detailTemplateTitleElement;
    },
    set_detailTemplateTitleElement: function (value) {
        if (this._detailTemplateTitleElement != value) {
            this._detailTemplateTitleElement = value;
        }
    },
    get_masterTemplateTitleElement: function () {
        return this._masterTemplateTitleElement;
    },
    set_masterTemplateTitleElement: function (value) {
        if (this._masterTemplateTitleElement != value) {
            this._masterTemplateTitleElement = value;
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
    get_editMasterTemplateLink: function () {
        return this._editMasterTemplateLink;
    },
    set_editMasterTemplateLink: function (value) {
        if (this._editMasterTemplateLink != value) {
            this._editMasterTemplateLink = value;
        }
    },
    get_createDetailTemplateLink: function () {
        return this._createDetailTemplateLink;
    },
    set_createDetailTemplateLink: function (value) {
        if (this._createDetailTemplateLink != value) {
            this._createDetailTemplateLink = value;
        }
    },
    get_createMasterTemplateLink: function () {
        return this._createMasterTemplateLink;
    },
    set_createMasterTemplateLink: function (value) {
        if (this._createMasterTemplateLink != value) {
            this._createMasterTemplateLink = value;
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
    get_detailViewName: function () {
        return this.get_controlData().DetailViewName;
    },
    get_masterViewName: function () {
        return this.get_controlData().MasterViewName;
    },
    get_detailView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_detailViewName()];
    },
    get_masterView: function () {
        return this.get_controlData().ControlDefinition.Views[this.get_masterViewName()];
    },
    get_detailTemplateId: function () {
        return this._detailTemplateId;
    },
    set_detailTemplateId: function (value) {
        this._detailTemplateId = value;
    },
    get_masterTemplateId: function () {
        return this._masterTemplateId;
    },
    set_masterTemplateId: function (value) {
        this._masterTemplateId = value;
    }
}
Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.DynamicTypeTemplatesSelectorDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();