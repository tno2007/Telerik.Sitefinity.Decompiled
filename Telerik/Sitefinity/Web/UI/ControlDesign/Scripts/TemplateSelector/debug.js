﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("TemplateSelector.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign.Selectors");

Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector.initializeBase(this, [element]);

    this._valueUpdatedDelegate = null;
    this._parentDesigner = null;
    this._listLimitControl = null;
    this._currentViewName = null;
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

Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector.callBaseMethod(this, 'initialize');

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        this._onUnloadDelegate = Function.createDelegate(this, this._onUnload);
        Sys.Application.add_unload(this._onUnloadDelegate);

        this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        $addHandler(this.get_createTemplateLink(), "click", this._createTemplateLinkClickDelegate);

        this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        $addHandler(this.get_editTemplateLink(), "click", this._editTemplateLinkClickDelegate);

        this._templateValueChangedDelegate = Function.createDelegate(this, this._templateValueChanged);
        this.get_viewsList().add_valueChanged(this._templateValueChangedDelegate);

        this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        this._widgetEditorCloseDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);

        // prevent memory leaks for jQuery
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }
        if (this._onUnloadDelegate) {
            delete this._onUnloadDelegate;
        }

        if (this._createTemplateLinkClickDelegate) {
            if (this.get_createTemplateLink())
                $removeHandler(this.get_createTemplateLink(), "click", this._createTemplateLinkClickDelegate);
            delete this._createTemplateLinkClickDelegate;
        }

        if (this._editTemplateLinkClickDelegate) {
            if (this.get_editTemplateLink())
                $removeHandler(this.get_editTemplateLink(), "click", this._editTemplateLinkClickDelegate);
            delete this._editTemplateLinkClickDelegate;
        }

        if (this._templateValueChangedDelegate) {
            if (this.get_viewsList())
                this.get_viewsList().remove_valueChanged(this._templateValueChangedDelegate);
            delete this._templateValueChangedDelegate;
        }

        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }
        if (this._widgetEditorCloseDelegate) {
            delete this._widgetEditorCloseDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    refreshUI: function (keyName) {
        var control = this.get_controlData(); 
        if (!keyName && control.TemplateKey) {
            this.get_viewsList().set_value(control.TemplateKey);
        } else if (keyName) {
            this.get_viewsList().set_value(control[keyName]);
        } else {
            var embeddedTemplateName = control.LayoutTemplateName;
            templateKey = this._embeddedTemplateMap[embeddedTemplateName];
            this.get_viewsList().set_value(templateKey);
        }

        if (this.get_viewsList().get_value() == "")
            jQuery(this.get_editTemplateLink()).hide();
        else {
            jQuery(this.get_editTemplateLink()).show();
        }
    },

    applyChanges: function (keyName) {
        var control = this.get_controlData();
        var key = this.get_viewsList().get_value();

        if (!keyName) {
            control.TemplateKey = key;
            return;
        }

        control[keyName] = key;
    },

    /* --------------------------------- private methods --------------------------------- */

    // this method is executed when the page loads
    _onLoad: function () {
        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _onUnload: function () {
        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            this._widgetEditorDialog.remove_close(this._widgetEditorCloseDelegate);
            this._widgetEditorDialog.remove_pageLoad(this._widgetEditorShowDelegate);
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
            var option = $(this.get_viewsList()._choiceElement).find("option:selected").get(0);
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
                    this.get_viewsList().set_value(widgetId);
                    $(this.get_viewsList().get_element()).show();
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

    _onWidgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog) {
                var data = this.get_controlData();
                var params =
                {
                    TemplateId: this._selectedTemplateId,
                    ControlType: data.ControlType,
                    BlackListControlTemplateEditor: true,
                    ModuleName: data.ModuleName
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    _templateValueChanged: function (sender) {
        if (sender.get_value() == "") {
            jQuery(this.get_editTemplateLink()).hide();
        }
        else {
            jQuery(this.get_editTemplateLink()).show();
        }
        // TODO: Refactor this to set the embedded template path when template has not been changed
        this.get_controlData().TemplateKey = sender.get_value();
    },

    /* --------------------------------- properties --------------------------------- */
    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
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

    // gets the object that represents the client side representation of the control 
    // being edited
    get_controlData: function () {
        var parent = this.get_parent();
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
Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.Selectors.TemplateSelector', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
