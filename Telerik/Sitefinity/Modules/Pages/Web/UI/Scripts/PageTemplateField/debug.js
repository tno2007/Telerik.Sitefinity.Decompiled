﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields");

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField.initializeBase(this, [element]);
    this._templateIcon = null;
    this._templateLabel = null;
    this._frameworkLabel = null;
    this._templateSelectorDialog = null;
    this._openTemplateDialogButton = null;
    this._selectTemplatePanel = null;
    this._templatePreviewPanel = null;
    this._templateSelectorDialogUrl = null;
    this._emptyGuid = "00000000-0000-0000-0000-000000000000";
    this._showEmptyTemplate = null;
    this._notCreateTemplateForMasterPage = null;
    this._isBackendTemplate = null;
    this._rootTaxonType = null;
    this._language = null;
    this._manager = null;

    this._useDefaultTemplate = null;

    this._currentUITemplate = null;
    this._defaultTemplateRequested = false;
    this._baseBackendUrl = null;

    this._selectedTemplate = null;
    this._lastClickedRadio = null;
    this._currentTemplateId = null;

    this._openTemplateClickDelegate = null;
    this._templateSelectorDialogCloseDelegate = null;
    this._templateSelectorDialogLoadDelegate = null;
    this._templateRadioClickDelegate = null;

    this._templateDlgContainerOverflow = "auto";

    this._emptyIconCache = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField.callBaseMethod(this, "initialize");

        if (this._openTemplateDialogButton) {
            this._openTemplateClickDelegate = Function.createDelegate(this, this._openTemplateClickHandler);
            $addHandler(this._openTemplateDialogButton, "click", this._openTemplateClickDelegate);
        }

        if (this._templateSelectorDialogCloseDelegate == null) {
            this._templateSelectorDialogCloseDelegate = Function.createDelegate(this, this._templateSelectorDialogCloseHandler);
        }
        if (this._templateSelectorDialogLoadDelegate == null) {
            this._templateSelectorDialogLoadDelegate = Function.createDelegate(this, this._templateSelectorDialogLoadHandler);
        }

        if (this._templateSelectorDialog) {
            this._templateSelectorDialog.add_close(this._templateSelectorDialogCloseDelegate);
        }
    },

    dispose: function () {

        if (this._openTemplateClickDelegate) {
            if (this._openTemplateDialogButton) {
                $removeHandler(this._openTemplateDialogButton, "click", this._openTemplateClickDelegate);
            }
            delete this._openTemplateClickDelegate;
        }

        if (this._templateSelectorDialogCloseDelegate) {
            if (this._templateSelectorDialog) {
                this._templateSelectorDialog.remove_close(this._templateSelectorDialogCloseDelegate);
            }
            delete this._templateSelectorDialogCloseDelegate;
        }

        if (this._templateRadioClickDelegate) {
            delete this._templateRadioClickDelegate;
        }

        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this._defaultTemplateRequested = false;
        this._currentUITemplate = null;
        this.set_value(null);
        this._setValueInternal(null);
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField.callBaseMethod(this, "reset");
    },

    // Gets the value of the field control.
    get_value: function () {

        var isSelectedTemplate = this._selectedTemplate && this._selectedTemplate.Id != this._emptyGuid;
        if (isSelectedTemplate && this._selectedTemplate) {
            this._selectedTemplate.RootTaxonType = this._rootTaxonType;
            return this._selectedTemplate;
        }
        else {
            // null is skipped by the field controls binder because of the reference types
            return { Id: this._emptyGuid, RootTaxonType: this._rootTaxonType };
        }
    },

    // Sets the value of the text field control depending on DisplayMode.
    // NOTE: When the value contains empty guid, this means that the field is from a "Create Page" dialog, not "Edit".
    //       If the value is null, this means that "No template" is selected for the page.
    set_value: function (value) {
        this._value = value;

        if (value == null || (value.Id == this._emptyGuid && !value.TemplateIconUrl)) {
            if (this._currentUITemplate == null && this.get_useDefaultTemplate() == true) {
                this._loadDefaultTemplate(this.get_isBackendTemplate());
            }
        }

        this._setValueInternal(value);
    },

    isChanged: function () {
        if (this.get_value().Id == this._emptyGuid) {

            return false;
        } else {
            if (this._value == this.get_value()) {
                return false;
            } else {
                return true;
            }
        }

    },

    _setValueInternal: function (value) {
        this._value = value;
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._applyTemplate(value);
        }
        else {
            //throw "ReadMode not implemented";
            return;
        }
        this._lastClickedRadio = null;

        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    _loadDefaultTemplate: function (isBackendTemplate) {
        if (this._defaultTemplateRequested == false) {
            this._defaultTemplateRequested = true;
            var serviceUrl = this._baseBackendUrl;
            if (isBackendTemplate) {
                serviceUrl += 'Services/Pages/PagesService.svc/Template/GetDefaultBackend/';
            }
            else {
                serviceUrl += 'Services/Pages/PagesService.svc/Template/GetDefault/';
            }

            var clientManager = this.get_manager();
            clientManager.InvokeGet(serviceUrl, [], null, this._getDefaultTemplateSuccess, this._getDefaultTemplateFailure, this);
        }
    },
    _getDefaultTemplateSuccess: function (caller, result) {
        //        var templateField = caller._binder.getFieldControlByDataFieldName('Template');
        //        templateField.set_value(result);
        if (caller._value && caller._value.Id == caller._emptyGuid) {
            caller.set_value(result);
        } else {
            caller._currentUITemplate = result;
        }

        caller._applyPreselectedTemplate();
    },
    _getDefaultTemplateFailure: function (result) {

    },
    selectCurrentUITemplate: function () {
        this.set_value(this._currentUITemplate);
    },

    /* -------------------- events -------------------- */

    add_templateChanged: function (handler) {
        this.get_events().addHandler('templateChanged', handler);
    },

    remove_templateChanged: function (handler) {
        this.get_events().removeHandler('templateChanged', handler);
    },

    raiseTemplateChanged: function (args) {
        var handler = this.get_events().getHandler('templateChanged');
        if (handler) {
            handler(this, args);
        }
    },

    /* -------------------- event handlers ------------ */
    _openTemplateClickHandler: function (e) {
        this._templateDlgContainerOverflow = jQuery("body").css("overflow");
        jQuery("body").css("overflow", "auto");
        if (this._selectedTemplate) {
            var url;
            url = Telerik.Sitefinity.setUrlParameter(this._templateSelectorDialogUrl, "templateId", this._selectedTemplate.Id);
            url = Telerik.Sitefinity.setUrlParameter(url, "showEmptyTemplate", this.get_showEmptyTemplate());
            url = Telerik.Sitefinity.setUrlParameter(url, "notCreateTemplateForMasterPage", this.get_notCreateTemplateForMasterPage());
            url = Telerik.Sitefinity.setUrlParameter(url, "rootTaxonType", this.get_rootTaxonType());
            var clientManager = this.get_manager();
            var siteId = clientManager.getSiteId();
            if (siteId) {
                url = Telerik.Sitefinity.setUrlParameter(url, clientManager.getSiteIdKey(), siteId);
            }

            if (this.get_language()) url = Telerik.Sitefinity.setUrlParameter(url, "language", this.get_language());

            this._templateSelectorDialog.SetUrl(url);
        }
        //page load event handler should be reattached each time the dialog is shown as it is set to reload on each show.
        this._templateSelectorDialog.add_pageLoad(this._templateSelectorDialogLoadDelegate);
        this._templateSelectorDialog.show();
        Telerik.Sitefinity.centerWindowHorizontally(this._templateSelectorDialog);
    },

    _templateSelectorDialogLoadHandler: function (sender, args) {
        //page load event handler should be reattached each time the dialog is shown as it is set to reload on each show.
        sender.remove_pageLoad(this._templateSelectorDialogLoadDelegate);
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle && frameHandle.createDialog) {
            var dataItem = null;
            if (this._currentTemplateId) {
                dataItem = { "Template": this._selectedTemplate, "Id": this._currentTemplateId };
            }
            else {
                dataItem = { "Template": this._selectedTemplate };
            }
            frameHandle.createDialog("", dataItem, sender, this._templateSelectorDialog, null, null)
        }
    },

    _templateSelectorDialogCloseHandler: function (sender, args) {
        jQuery("body").css("overflow", this._templateDlgContainerOverflow);
        var dataItem = args.get_argument();
        if (dataItem && dataItem.Template) {
            this._applyTemplate(dataItem.Template);
        }
    },

    _applyPreselectedTemplate: function () {
        if (this._selectedTemplate == null && this._currentUITemplate != null) {
            this._applyTemplate(this._currentUITemplate);
        }
    },

    /* -------------------- private methods ----------- */

    _applyTemplate: function (template) {
        jQuery(this._frameworkLabel).attr("class","");
        this._selectedTemplate = template;
        var selectedTemplateCondition = template && ((template.Id != this._emptyGuid && template.Id !== undefined) || (template.TemplateIconUrl != null & template.TemplateIconUrl !== undefined));
        if (selectedTemplateCondition) {
            this._templateIcon.src = template.TemplateIconUrl;
            jQuery(this._templateIcon).show();
            jQuery(this._templateLabel).html(template.Title);
            jQuery(this._frameworkLabel).attr("class","sfFramework" + template.Framework);
            jQuery(this._templatePreviewPanel).show();
            jQuery(this._selectTemplatePanel).show();
        }
        else if (template && template.MasterPage != null && template.MasterPage !== undefined) {
            //If we set src to empty string, then a request to the current page is made by the browser, which is not what we want
            jQuery(this._templateIcon).hide();
            //this._templateIcon.src = "";

            var name = template.MasterPage;
            var idx = name.lastIndexOf("/");
            if (idx > -1) {
                name = name.substring(idx + 1, name.length);
            }
            jQuery(this._templateLabel).text(name);
        }
        else {
            //If we set src to empty string, then a request to the current page is made by the browser, which is not what we want
            jQuery(this._templateLabel).text("");
            if (template && template.Id == this._emptyGuid) {
                jQuery(this._templateLabel).text("No template");
                this._templateIcon.src = this._emptyIconCache;
            }
        }

        this.raiseTemplateChanged({ Template: this._selectedTemplate });
    },

    /* -------------------- properties ---------------- */
    get_showEmptyTemplate: function () {
        return this._showEmptyTemplate;
    },
    set_showEmptyTemplate: function (value) {
        this._showEmptyTemplate = value;
    },

    get_notCreateTemplateForMasterPage: function () {
        return this._notCreateTemplateForMasterPage;
    },
    set_notCreateTemplateForMasterPage: function (value) {
        this._notCreateTemplateForMasterPage = value;
    },

    get_useDefaultTemplate: function () {
        return this._useDefaultTemplate;
    },
    set_useDefaultTemplate: function (value) {
        this._useDefaultTemplate = value;
    },

    get_isBackendTemplate: function () {
        return this._isBackendTemplate;
    },
    set_isBackendTemplate: function (value) {
        this._isBackendTemplate = value;
    },

    get_rootTaxonType: function () {
        return this._rootTaxonType;
    },
    set_rootTaxonType: function (value) {
        this._rootTaxonType = value;
    },

    get_language: function () {
        return this._language;
    },
    set_language: function (value) {
        this._language = value;
    },

    // Gets the reference to the DOM element used to display the text box of the text field control.
    get_templateIcon: function () { return this._templateIcon; },
    // Sets the reference to the DOM element used to display the text box of the text field control.
    set_templateIcon: function (value) { this._templateIcon = value; },

    // Gets the reference to the DOM element used to display the label box of the text field control.
    get_templateLabel: function () { return this._templateLabel; },
    // Sets the reference to the DOM element used to display the label of the text field control.
    set_templateLabel: function (value) { this._templateLabel = value; },

    // Gets the reference to the DOM element used to display the label box of the template framework.
    get_frameworkLabel: function () { return this._frameworkLabel; },
    // Sets the reference to the DOM element used to display the label of the template framework.
    set_frameworkLabel: function (value) { this._frameworkLabel = value; },

    get_selectedTemplate: function () { return this._selectedTemplate; },
    set_selectedTemplate: function (value) { this._selectedTemplate = value; },

    get_templateSelectorDialog: function () { return this._templateSelectorDialog; },
    set_templateSelectorDialog: function (value) { this._templateSelectorDialog = value; },

    get_openTemplateDialogButton: function () { return this._openTemplateDialogButton; },
    set_openTemplateDialogButton: function (value) { this._openTemplateDialogButton = value; },

    get_selectTemplatePanel: function () { return this._selectTemplatePanel; },
    set_selectTemplatePanel: function (value) { this._selectTemplatePanel = value; },

    get_templatePreviewPanel: function () { return this._templatePreviewPanel; },
    set_templatePreviewPanel: function (value) { this._templatePreviewPanel = value; },

    get_currentTemplateId: function () {
        return this._currentTemplateId;
    },
    set_currentTemplateId: function (value) {
        this._currentTemplateId = value;
    },

    get_manager: function () {
        if (!this._manager) {
            this._manager = new Telerik.Sitefinity.Data.ClientManager();
        }
        return this._manager;
    }
};
Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageTemplateField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);