﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

var templateSelectorDialog;

Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog.initializeBase(this, [element]);

    this._bottomCommandBar = null;
    this._masterPageSelectorDialog = null;
    this._showEmptyTemplate = true;
    this._notCreateTemplateForMasterPage = true;
    this._webServiceUrl = null;
    this._useAsDefaultTemplateCheckboxId = null;

    // map key is elementID and value corresponding template
    this._templateItemElements = {};

    this._availableFrameworks = 0;

    this._newTemplate = null;
    this._selectedTemplate = null;
    this._emptyTemplateElement = null;

    this._warningMessageControl = null;

    this._makeTemplateFromMasterFileServiceUrl = null;
    this._getTemplateByPageDataServiceUrl = null;
    this._clientManager = null;

    this._templateClickDelegate = null;
    this._commandBarCommandDelegate = null;
    this._masterPageSelectorDialogCloseDelegate = null;
    this._masterPageSelectorDialogShowDelegate = null;
    this._createTemplateServiceSuccessDelegate = null;
    this._getTemplateServiceSuccessDelegate = null;
    this._loadDelegate = null;
    this._dialogShowDelegate = null;
    this._callBacks = [];
    this._elementsToDispose = [];
    this._emptyGuid = "00000000-0000-0000-0000-000000000000";
    // if true will return args suitable for itemslistbase dialog closed event
    this._dataItem = null;

};

Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog.callBaseMethod(this, "initialize");

        templateSelectorDialog = this;

        var selectedTemplateId = dialogBase.getQueryValue('templateId', true);
        this.set_showEmptyTemplate(dialogBase.getQueryValue('showEmptyTemplate', true) === "true");
        this.set_notCreateTemplateForMasterPage(dialogBase.getQueryValue('notCreateTemplateForMasterPage', true) === "true");

        this._createTemplateServiceSuccessDelegate = Function.createDelegate(this, this._createTemplateServiceSuccess);
        this._getTemplateServiceSuccessDelegate = Function.createDelegate(this, this._getTemplateServiceSuccess);
        this._commandBarCommandDelegate = Function.createDelegate(this, this._commandBarCommandHandler);
        this._bottomCommandBar.add_command(this._commandBarCommandDelegate);

        this._masterPageSelectorDialogCloseDelegate = Function.createDelegate(this, this._masterPageSelectorDialogCloseHandler);
        this._masterPageSelectorDialog.add_close(this._masterPageSelectorDialogCloseDelegate);

        this._masterPageSelectorDialogShowDelegate = Function.createDelegate(this, this._masterPageSelectorDialogShowHandler);
        this._masterPageSelectorDialog.add_show(this._masterPageSelectorDialogShowDelegate);
        this._masterPageSelectorDialog.add_beforeShow(this._masterPageSelectorDialogShowDelegate);

        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        var componentElement = this.get_element();
        for (var elementId in this._templateItemElements) {
            var element = $get(elementId, componentElement);
            var template = this._templateItemElements[elementId];
            var callBack = Function.createCallback(this._templateClickHandler, { self: this, template: template });
            $addHandler(element, "click", callBack);
            this._callBacks.push(callBack);
            this._elementsToDispose.push(element);

            if (template && (selectedTemplateId == template.Id)) {
                this._selectedTemplate = template;
            }

            if (template.Id == this._emptyGuid) {
                this._emptyTemplateElement = element;
                jQuery(this._emptyTemplateElement).toggle(this._showEmptyTemplate);
            }
        }
    },

    dispose: function () {
        if (this._commandBarCommandDelegate) {
            if (this._bottomCommandBar) {
                this._bottomCommandBar.remove_command(this._commandBarCommandDelegate);
            }
            delete this._commandBarCommandDelegate;
        }

        if (this._callBacks) {
            delete this._callBacks;
        }

        for (var i = 0, length = this._elementsToDispose.length; i < length; i++) {
            $clearHandlers(this._elementsToDispose[i]);
        }

        if (this._masterPageSelectorDialogCloseDelegate && this._masterPageSelectorDialogShowDelegate) {
            if (this._masterPageSelectorDialog) {
                this._masterPageSelectorDialog.remove_close(this._masterPageSelectorDialogCloseDelegate);
                this._masterPageSelectorDialog.remove_show(this._masterPageSelectorDialogShowHandler);
                this._masterPageSelectorDialog.remove_beforeShow(this._masterPageSelectorDialogShowHandler);
            }

            delete this._masterPageSelectorDialogCloseDelegate;
            delete this._masterPageSelectorDialogShowDelegate;
        }

        if (this._createTemplateServiceSuccessDelegate) {
            delete this._createTemplateServiceSuccessDelegate;
        }

        if (this._getTemplateServiceSuccessDelegate) {
            delete this._getTemplateServiceSuccessDelegate;
        }

        if (this._dialogShowDelegate) {
            if (this.get_radWindow()) {
                this.get_radWindow().remove_show(this._dialogShowDelegate);
            }
            Sys.Application.remove_load(this._dialogShowDelegate);
            delete this._dialogShowDelegate;
        }

        Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    setData: function (dataItem) {
        this._deselectAll();
        this._dataItem = dataItem;

        if (dataItem && dataItem.Template && dataItem.Template.Id != Telerik.Sitefinity.getEmptyGuid()) {
            this._selectTemplate(dataItem.Template);
        }
        else if (dataItem && this._dataItem.PageDataId) {
            this.get_clientManager().InvokeGet(this._getTemplateByPageDataServiceUrl + this._dataItem.PageDataId + "/", [], {}, this._getTemplateServiceSuccessDelegate, this._serviceFailure);
        }

        if (this._shouldHideCurrentTemplate(dataItem)) {
            this._hideCurrentTemplate(dataItem.Id);
        }
    },

    showHideWarningMessage: function (val) {
        if (val) {
            jQuery(this.get_warningMessageControl()).show();
        } else {
            jQuery(this.get_warningMessageControl()).hide();
        }
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    // Handles the clicks on the command bar buttons
    _commandBarCommandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case "saveChanges":
                this._saveChanges();
                break;
            case "cancel":
                dialogBase.close();
                break;
            case "selectMasterFile":
                this._masterPageSelectorDialog.show();
                break;
        }
    },
    // Handles clicks on templates
    _templateClickHandler: function (e, args) {
        args.self._selectedTemplate = args.template;
        args.self._selectElement(this);
    },

    // Handles the closing event of the master page selector dialog
    _masterPageSelectorDialogCloseHandler: function (sender, args) {
        //This is the path to the master page but we need Id
        var masterPagePath = args.get_argument();
        var notCreateTemplateForMasterPage = this.get_notCreateTemplateForMasterPage();

        if (notCreateTemplateForMasterPage) {
            //do not create template for master page
            var dummyTemplate =
            {
                NotCreateTemplateForMasterPage: true,
                MasterPage: masterPagePath
            };

            this._selectedTemplate = dummyTemplate;
            this._closeForm();
        }
        else if (masterPagePath) {
            this.get_clientManager().InvokePut(this._makeTemplateFromMasterFileServiceUrl, [], {}, masterPagePath, this._createTemplateServiceSuccessDelegate, this._serviceFailure);
        }
    },

    _masterPageSelectorDialogShowHandler: function (sender, args) {
        sender.maximize();
    },

    /* -------------------- private methods ----------- */

    _load: function () {
        this.set_showEmptyTemplate(dialogBase.getQueryValue('showEmptyTemplate', true) === "true");
        this.set_notCreateTemplateForMasterPage(dialogBase.getQueryValue('notCreateTemplateForMasterPage', true) === "true");
        if (this._availableFrameworks == 2) {
            this._toggleUseMastertTemplateVisibility(true);
        } else {
            this._toggleUseMastertTemplateVisibility(false);
        }
    },

    _saveChanges: function () {
        var setAsDefaultCheckBox = this._getSetAsDefaultCheckbox();
        if (setAsDefaultCheckBox && setAsDefaultCheckBox.checked == true) {
            this._setDefaultTemplateId(this._selectedTemplate.Id);
        }

        this._closeForm();
    },

    _getSetAsDefaultCheckbox: function () {
        var setAsDefaultCheckBox = $('#' + this._useAsDefaultTemplateCheckboxId).get(0);
        return setAsDefaultCheckBox;
    },
    _closeForm: function () {
        var args;
        if (this._dataItem) {
            this._dataItem.Template = this._selectedTemplate;
            args = this._dataItem;
        }
        else {
            args = this._selectedTemplate;
        }

        this._selectedTemplate = null;
        this._dataItem = null;
        dialogBase.close(args);
    },

    _setDefaultTemplateId: function (templateId) {
        //Make request to change the default template
        var serviceUrl = this._webServiceUrl;
        serviceUrl += 'Template/SetDefault/';
        this.get_clientManager().InvokePut(serviceUrl, null, null, templateId, this._serviceSuccess, this._serviceFailure, this);
    },

    _findTemplateElement: function (templateId) {
        var templateElementId;
        for (var elementId in this._templateItemElements) {
            var template = this._templateItemElements[elementId];
            if (template.Id == templateId) {
                templateElementId = elementId;
                break;
            }
        }
        return templateElementId;
    },

    _selectTemplateById: function (templateId) {
        var templateElementId = this._findTemplateElement(templateId);

        if (templateElementId) {
            var element = $get(templateElementId, this.get_element());
            if (element) {
                this._selectElement(element);
            }
            else {
                throw "Unable to find element representing template with Id: " + templateId;
            }
        }
    },

    _selectTemplate: function (template) {
        if (template) {
            this._selectedTemplate = template;
            this._selectTemplateById(this._selectedTemplate.Id);
        }
        else if (this._showEmptyTemplate) {
            this._selectedTemplate = null;
            this._selectTemplateById(this._emptyGuid);
        }
    },

    _shouldHideCurrentTemplate: function (dataItem) {
        return dataItem && dataItem.HideCurrentTemplate !== undefined;
    },

    //when selecting parent template of the current template hide current template
    _hideCurrentTemplate: function (templateId) {
        var templateElementId = this._findTemplateElement(templateId);

        if (templateElementId) {
            var element = $get(templateElementId, this.get_element());
            jQuery(element).hide();
        }
    },

    _selectElement: function (element) {
        this._deselectAll();
        jQuery(element).addClass("sfSel sfSimpleTemplate");
        if (jQuery(element)[0]) {
            var currentTop = jQuery(element)[0].offsetTop;
            var scrollableContainer = element.parentNode.parentNode.parentNode;
            if (scrollableContainer)
                scrollableContainer.scrollTop = currentTop - 98;
        }
    },

    _deselectAll: function () {
        var componentElement = jQuery(this.get_element());
        for (var elementId in this._templateItemElements) {
            var el = componentElement.find("#" + elementId);
            el.removeClass("sfSel");
        }
    },

    _serviceFailure: function (sender, args) {
        alert(sender.Detail);
    },
    _serviceSuccess: function (caller, sender, args) {

    },
    _createTemplateServiceSuccess: function (sender, data) {
        this._selectedTemplate = data;
        this._closeForm();
    },

    _getTemplateServiceSuccess: function (sender, data) {
        this._selectedTemplate = data;
        this._selectTemplate(data);
    },

    _toggleUseMastertTemplateVisibility: function (hide) {
        if (hide) {
            jQuery(".sfModeSwitcher").hide();
        } else {
            jQuery(".sfModeSwitcher").show();
        }
    },

    /* -------------------- properties ---------------- */

    get_warningMessageControl: function () {
        return this._warningMessageControl;
    },
    set_warningMessageControl: function (value) {
        this._warningMessageControl = value;
    },

    // Gets the command bar component
    get_bottomCommandBar: function () {
        return this._bottomCommandBar;
    },
    // Sets the command bar component
    set_bottomCommandBar: function (value) {
        this._bottomCommandBar = value;
    },

    // Gets the dialog for selection a master page for templates
    get_masterPageSelectorDialog: function () {
        return this._masterPageSelectorDialog;
    },
    // Sets the dialog for selection a master page for templates
    set_masterPageSelectorDialog: function (value) {
        this._masterPageSelectorDialog = value;
    },

    // Gets the newly created template from a master page if such
    get_newTemplate: function () {
        return this._newTemplate;
    },
    // Sets the newly created template from a master page if such
    set_newTemplate: function (value) {
        this._newTemplate = value;
    },

    // Determines whether to show the empty template selection
    get_showEmptyTemplate: function () {
        return this._showEmptyTemplate;
    },
    set_showEmptyTemplate: function (value) {
        this._showEmptyTemplate = value;
        if (this.get_isInitialized()) {
            jQuery(this._emptyTemplateElement).toggle(value);
        }
    },
    // Determines whether to create a template for master page
    //If true will not create a template for selected master page file.
    get_notCreateTemplateForMasterPage: function () {
        return this._notCreateTemplateForMasterPage;
    },
    set_notCreateTemplateForMasterPage: function (value) {
        this._notCreateTemplateForMasterPage = value;

    },

    // Specifies the culture that will be used on the server as UICulture when processing a request
    set_uiCulture: function (culture) {
        this.get_clientManager().set_uiCulture(culture);
    },
    // Gets the culture that will be used on the server as UICulture when processing a request
    get_uiCulture: function () {
        return this.get_clientManager().get_uiCulture();
    },

    get_clientManager: function () {
        if (!this._clientManager) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }
        return this._clientManager;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
