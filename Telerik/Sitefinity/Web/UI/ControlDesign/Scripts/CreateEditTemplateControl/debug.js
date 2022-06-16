/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl.initializeBase(this, [element]);
    this._refreshing = false;
    this._dataFieldNameControlIdMap = null;
    this._controldataFieldNameMap = {};
    this._valueUpdatedDelegate = null;
    this._parentDesigner = null;
    this._refreshing = false;
    this._listLimitControl = null;
    this._templateTitleElement = null;
    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._embeddedTemplateMap = [];
    this._modifyWidgetTemplatePermission = null;

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
    this._currentView = null;

    this._viewsList = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl.callBaseMethod(this, 'initialize');

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }
        if (this._onUnloadDelegate == null) {
            this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        }
        Sys.Application.add_load(this._onLoadDelegate);
        Sys.Application.add_unload(this._onUnloadDelegate);

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
    },

    dispose: function () {
        //Add custom dispose actions here
        Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl.callBaseMethod(this, 'dispose');
        if (this._valueUpdatedDelegate) {
            delete this._valueUpdatedDelegate;
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
        var currentView = this.get_currentView();

        var c = $find(this._dataFieldNameControlIdMap["TemplateKey"]);

        if (currentView.TemplateKey) {
            c.set_value(currentView.TemplateKey);
        }
        else {
            var embeddedTemplateName = currentView.LayoutTemplateName;
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


    /* --------------------------------- private methods --------------------------------- */

    // this method is executed when the page loads
    _onLoad: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).add_valueChanged(this._templateValueChangedDelegate);

        this._widgetEditorDialog = this._radWindowManager.GetWindowByName("widgetEditorDialog");
    },

    _onUnload: function () {
        $find(this._dataFieldNameControlIdMap["TemplateKey"]).remove_valueChanged(this._templateValueChangedDelegate);
        this._unbindWindowEvents();
    },

    _createTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = null;
            if (this._widgetEditorDialog) {
                this._bindWindowEvents();
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
                this._bindWindowEvents();
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

    _bindWindowEvents: function () {
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },
    _unbindWindowEvents: function () {
        this._widgetEditorDialog = this._radWindowManager.GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            this._widgetEditorDialog.remove_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.remove_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _onWidgetEditorClosed: function (sender, args) {
        dialogBase.get_radWindow().Restore();
        $("body").addClass("sfSelectorDialog");

        this._unbindWindowEvents();
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
            if (frameHandle.createDialog) {
                var params =
                {
                    TemplateId: this._selectedTemplateId,
                    ControlType: this.get_currentView().ViewType,
                    BlackListControlTemplateEditor: true
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
        this.get_currentView().TemplateKey = sender.get_value();
    },


    /* --------------------------------- properties --------------------------------- */

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

    // gets the client side representation of the currently selected master view definition
    get_currentView: function () {
        return this._currentView;
    },
    set_currentView: function (value) {
        this._currentView = value;
    }
}
Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.CreateEditTemplateControl', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
