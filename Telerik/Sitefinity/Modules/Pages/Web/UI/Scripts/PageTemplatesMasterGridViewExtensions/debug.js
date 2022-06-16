﻿// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    var masterView = sender;
    masterView.get_binder().set_unescapeHtml(true);
    var extender = new Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension(masterView);
    extender.initialize();
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension = function (masterView) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension.initializeBase(this);

    // Main components
    this._masterView = masterView;
    this._binder = null;

    this._itemsGrid = {};

    // Event delegates
    this._dialogClosedDelegate = null;
    this._itemCommandDelegate = null;
    this._itemDataBoundDelegate = null;
    this._masterCommandDelegate = null;
    this._editTranslationDelegate = null;
    this._createTranslationDelegate = null;

    this._translationHandlers = null;

    this._actionCommandPrefix = "sf_binderCommand_";

    //HACK: hardcode these ids here
    this._defaultPageTemplatesIds =
        ["f669d9a7-009d-4d83-bbbb-000000000001",
        "f669d9a7-009d-4d83-bbbb-000000000002",
        "f669d9a7-009d-4d83-bbbb-000000000003",
        "f669d9a7-009d-4d83-bbbb-000000000004",
        "f669d9a7-009d-4d83-bbbb-000000000005",
        "f669d9a7-009d-4d83-bbbb-000000000006",
        "f669d9a7-009d-4d83-bbbb-000000000007",
        "f669d9a7-009d-4d83-bbbb-000000000008",
        "f669d9a7-009d-4d83-bbbb-000000000009",
        "f669d9a7-009d-4d83-cccc-000000000001",
        "45fef908-af79-4568-b950-1d76768177ce",
        "e2b5894c-f3f8-49e8-bca7-acdfd5352e74"];
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension.callBaseMethod(this, 'initialize');

        this._masterCommandDelegate = Function.createDelegate(this, this._masterCommandHandler);
        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
        this._itemDataBoundDelegate = Function.createDelegate(this, this._itemDataBoundHandler);
        this._editTranslationDelegate = Function.createDelegate(this, this._editTranslationHandler);
        this._createTranslationDelegate = Function.createDelegate(this, this._createTranslationHandler);

        this._translationHandlers = { create: this._createTranslationDelegate, edit: this._editTranslationDelegate };

        this._itemsGrid = this._masterView.get_itemsGrid();

        if (this._itemsGrid) {
            this._binder = this._itemsGrid.getBinder();
            this._itemsGrid.add_command(this._masterCommandDelegate);
            this._itemsGrid.add_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.set_translationHandlers(this._translationHandlers);
            this._itemsGrid.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
        }

        if (this._masterView.get_sidebar().getWidgetByName("ThisSiteTemplates")) {
            jQuery(this._masterView.get_sidebar().getWidgetByName("AllTemplates").get_element()).find("a").removeClass("sfSel");
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension.callBaseMethod(this, 'dispose');

        if (this._itemsGrid) {
            this._itemsGrid.remove_command(this._masterCommandDelegate);
            this._itemsGrid.remove_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.remove_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.set_translationHandlers(null);
            this._itemsGrid.getBinder().remove_onItemDataBound(this._itemDataBoundDelegate);
        }

        delete this._masterCommandDelegate;
        delete this._itemCommandDelegate;
        delete this._dialogClosedDelegate;
        delete this._itemDataBoundDelegate;
        delete this._editTranslationDelegate;
        delete this._createTranslationDelegate;

        delete this._translationHandlers;
    }
    ,
    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    // handles commands fired the master view
    _masterCommandHandler: function (sender, args) {
        var binder = this._binder;
        switch (args.get_commandName()) {
            case "groupDeleteTemplate":
                var selectedItems = sender.get_selectedItems();
                if (selectedItems && selectedItems.length > 0) {
                    var multiKey = sender._getKeyFromItems(selectedItems);
                    this._deleteItems(multiKey, selectedItems, sender);
                }

                args.set_cancel(true);
                break;
            case 'showAllTemplates':
                binder.get_urlParams()['pageFilter'] = 'AllTemplates';
                if (binder.get_isFiltering()) {
                    binder.set_filterExpression('');
                }
                else {
                    binder.set_isFiltering(true);
                }
                binder.DataBind();
                break;
            case 'showThisSiteTemplates':
                binder.get_urlParams()['pageFilter'] = 'ThisSiteTemplates';
                binder.set_isFiltering(true);
                binder.DataBind();
                break;
            case 'showNotSharedTemplates':
                binder.get_urlParams()['pageFilter'] = 'NotSharedTemplates';
                binder.set_isFiltering(true);
                binder.DataBind();
                break;
            case 'showMyTemplates':
                binder.get_urlParams()['pageFilter'] = 'MyTemplates';
                binder.set_isFiltering(true);
                binder.DataBind();
                break;
            default:
                if (args.get_commandName().substring(0, 'sf_status_filter'.length) == 'sf_status_filter') {
                    binder.get_urlParams()['pageFilter'] = args.get_commandName();
                    binder.set_isFiltering(true);
                    binder.DataBind();
                    args.set_cancel(true);
                }
                break;
        }
    },

    // handles the commands fired by a single item
    _itemCommandHandler: function (sender, args) {
        var template = args.get_commandArgument();
        var templateId = args.get_commandArgument().Id;
        var editUrl = null;
        var dialog = null;
        var url = null;

        switch (args.get_commandName()) {
            case 'editParent':
                args.set_cancel(true);
                editUrl = template.ParentTemplateUrl;
                this._editTemplate(editUrl);
                break;
            case 'editPageContent':
            case 'editTemplate':
                args.set_cancel(true);
                editUrl = template.EditUrl;
                var currentList = this._masterView.get_currentItemsList();
                var uiCulture = currentList.get_uiCulture();
                if (uiCulture)
                    editUrl += "/" + uiCulture;
                this._editTemplate(editUrl);
                break;
            case "deleteTemplate":
                args.set_cancel(true);
                this._deleteItems([templateId], [template], sender);
                break;
            case "templatePages":
                dialog = sender.getRadWindowManager().GetWindowByName("templatePages");
                if (dialog) {
                    url = dialog.get_navigateUrl();
                    var idx = url.indexOf("?");
                    if (idx > -1) {
                        url = url.substring(0, idx);
                    }
                    dialog.set_navigateUrl(url + "?id={{Id}}");
                }
                break;
            case 'unlockPage':
                template.LockedByUsername = template.PageLifecycleStatus.LockedByUsername;
                //this._unlockPage(sender, pageId);
                break;
            case 'publishDraft':
                this._publishDraft(sender, templateId, templateId);
                break;
        }
    },

    // Fires when a dialog is closed
    _dialogClosedHandler: function (sender, args) {
        if (!Telerik.Sitefinity.DialogClosedEventArgs.isInstanceOfType(args)) {
            var itemsGrid = this._masterView.get_currentItemsList();
            var binder = itemsGrid.getBinder();
            var windowName = sender.get_name();

            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            var serviceUrl = this._getBaseUrl(binder);
            switch (windowName) {
                case "changeTemplate":
                    this._changeTemplate(serviceUrl, args, itemsGrid, clientManager);
                    binder.get_urlParams()['language'] = binder.get_uiCulture();
                    binder.DataBind();
                    break;
                case "unlockPage":
                    //If the window is an unlockPage window - check if the unlock button was clicked 
                    if (args.Action && args.Action == 'unlock') {
                        //If yes - refresh grid (ToDo: better refresh just unlocked item?)
                        binder.DataBind();
                    }
                    break;
                case "shareWith":
                    if (args.IsUpdated) {
                        binder.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
        else {
            var context = args.get_context();
            if (args && args.get_isCreated()
            && context && context.get_widgetCommandName
            && context.get_widgetCommandName() == 'create') {
                this._navigateToEditPage(sender, args);
            }
        }
    },

    // Fired when item is bound
    _itemDataBoundHandler: function (sender, args) {
        var dataItem = args.get_dataItem();
        var itemElement = args.get_itemElement();
        var spanElement = jQuery(itemElement).find("span.sfBasedOn");
        if (dataItem.ParentTemplateUrl) {

            var anchorElement = jQuery(itemElement).find(".sfParentLnk");
            anchorElement.attr("href", "javascript:void(0)");
            anchorElement.text(dataItem.ParentTemplateTitle);

            anchorElement.click(function () {
                window.location = dataItem.ParentTemplateUrl;
            });

            anchorElement.on("unload",
                function (e) {
                    jQuery.event.remove(this);
                    jQuery.removeData(this);
                });
            spanElement.hide();
        }
        else if (dataItem.MasterPage) {
            spanElement.text(dataItem.MasterPage);
        }
        else if (dataItem.Framework == 1 && dataItem.Name) {
            this._configureBasedOnWithPackage(dataItem, itemElement);
        }
        if (dataItem.PagesCountString && dataItem.PagesCount == 0) {
            var pagesCountHtml = String.format("<span>{0}</span>", dataItem.PagesCountString);
            jQuery(itemElement).find(".sfPageCount div.sys-container").html(pagesCountHtml);
        }

        this._configureMoreActionsMenu(dataItem, itemElement);

        //set translation column values
        this._masterView.setTranslations(dataItem, this._masterView.get_currentItemsList(), args.get_key(), itemElement);

    },

    _editTranslationHandler: function (sender, args) {
        args.set_cancel(true);
        this._editTemplate(args.get_dataItem().EditUrl + "/" + args.get_language());
    },

    _createTranslationHandler: function (sender, args) {
        args.set_cancel(true);
        var list = args.get_list();
        var item = args.get_dataItem();
        list.executeItemCommand("create", item, args.get_key(), { language: args.get_language(), languageMode: args.get_commandName(), sourceObjectId: item.Id });
    },

    /* -------------------- private methods ----------- */

    _configureBasedOnWithPackage: function (dataItem, itemElement) {
        var spanElement = jQuery(itemElement).find("span.sfBasedOn");
        spanElement.hide();
        var fileNameElement = jQuery(itemElement).find("span.sfFileName");
        var dataItemName = dataItem.Name;
        var conventionPath = "\\Mvc\\Views\\Layouts\\";
        var fileExtension = ".cshtml";
        var separator = ".";
        var pathSeparator = "\\";
        var filePath;
        if (dataItemName.indexOf(separator) == -1) {
            filePath = conventionPath + dataItemName + fileExtension;
            fileNameElement.text(pathSeparator + dataItemName + fileExtension);
        }
        else {
            var packageNameElement = jQuery(itemElement).find("span.sfPackageName");
            var packageName = dataItemName.split(separator, 1);
            packageNameElement.text(packageName);
            packageNameElement.after(pathSeparator);
            var fileName = dataItemName.slice(dataItemName.indexOf(separator) + 1);
            fileNameElement.text(pathSeparator + fileName + fileExtension);
            filePath = "<span>" + "\\ResourcePackages\\" + "</span><span>" + packageName + "</span><span>" + conventionPath + "</span><span>" + fileName + fileExtension + "</span>";
        }

        var anchorElement = jQuery(itemElement).find(".sfFullPath");
        anchorElement.attr("href", "javascript:void(0)");
        anchorElement.text("...");

        anchorElement.click(function () {
            spanElement.html(filePath);
            jQuery(itemElement).find(".sfPackage").hide();
            spanElement.show();
        });
    },

    _navigateToEditPage: function (sender, args) {
        $('body').addClass('sfLoadingTransition');

        var dataItem = args.get_dataItem();
        var language = null;
        var dialog = args.get_context();
        if (dialog) {
            var commandArgument = dialog.get_commandArgument();
            // Take the language from the arguments with which the dialog was created.
            if (commandArgument) {
                if (commandArgument.languageMode == "create" || commandArgument.languageMode == "duplicate") {
                    language = commandArgument.language;
                }
            }
        }
        if (dataItem != null && dataItem.hasOwnProperty('EditUrl')) {
            window.location.href = this._getLocalizedEditUrl(dataItem, language);
            args.set_cancel(true);
        }
    },

    _getLocalizedEditUrl: function (dataItem, language) {
        var url = dataItem.EditUrl;

        if (language) {
            url += "/" + language;
        }
        else if (dataItem.Language) {
            url += "/" + dataItem.Language;
        }
        return url;
    },

    // publish draft
    _publishDraft: function (itemsListBase, templateId, context) {
        var thisView = this;

        var itemsGrid = this._masterView.get_currentItemsList();
        var binder = itemsGrid.getBinder();

        var serviceUrl = this._getBaseUrl(binder);
        serviceUrl += 'batchPublishDraft/';

        var urlParams = [];
        var keys = [];
        var data = [];

        var context = { Command: "publishDraft" };

        data.push(templateId);

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        clientManager.set_uiCulture(this._binder.get_uiCulture());
        clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._serviceSuccess, this._serviceFailure, itemsGrid, false, null, context);
    },

    // edit content
    _editTemplate: function (editUrl) {
        window.location = editUrl;
    },

    _configureMoreActionsMenu: function (dataItem, containerElement) {

        var itemsList = this._masterView.get_currentItemsList();

        if (dataItem.PageLifecycleStatus && dataItem.PageLifecycleStatus.IsLocked == true && dataItem.IsUnlockable) {
        } else {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "unlockPage"], containerElement);
        }

        if (dataItem.Status && dataItem.Status == "Published") {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "publishDraft"], containerElement);
        }

        if (jQuery.inArray(dataItem.Id, this._defaultPageTemplatesIds) > -1) {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "changeTemplate"], containerElement);
        }
    },

    _deleteItems: function (multikey, templates, sender) {
        /// <summary>Delete one or more items</summary>
        /// <param name="multikey">Key or keys of the items to delete.</param>
        /// <param name="templates">Template or templates of the items to delete.</param>
        /// <param name="sender">field control binder.</param>

        var f = function () {
            sender.deleteItems(multikey, false);
        };

        var pdFunction = function (sender, args) {
            if (args.get_commandName() == "ok") {
                f();
            }
        };

        //single item delete
        if (templates.length == 1) {
            var t = templates[0];
            var pagesCount = t.PagesCount;
            var childTemplatesCount = t.ChildTemplatesCount;

            if (pagesCount > 0 || childTemplatesCount > 0) {
                var dialog = this._itemsGrid.getPromptDialogByName("singleTemplateInUseDialog");

                var message = dialog.get_initialMessage();
                message = String.format(message, '<strong>' + pagesCount + '</strong>', '<strong>' + childTemplatesCount + '</strong>');
                dialog.show_prompt(null, message)
            }
            //deletion allowed
            else {
                //Now confirmation is handled in ItemsListBase
                //this._itemsGrid.showPromptDialogByName("templateNotUsedWarningDialog", null, null, pdFunction);
                sender.deleteItems(multikey, false, templates);
            }
        }
        //batch items delete
        else {
            //templates are in use from other page or template, not allowed to be deleted
            if (this._hasTemplateInUse(templates)) {
                this._itemsGrid.showPromptDialogByName("batchDeleteNotAllowedDialog", null);
            }
            //deletion allowed
            else {
                //Now confirmation is handled in ItemsListBase
                //this._itemsGrid.showPromptDialogByName("templateNotUsedWarningDialog", null, null, pdFunction);
                sender.deleteItems(multikey, false, templates);
            }
        }
        return true;
    },

    _changeTemplate: function (serviceUrl, args, itemsGrid, clientManager) {
        serviceUrl += 'changeTemplate/';

        var urlParams = [];

        var context = { Command: "changeTemplate" };
        var binder = itemsGrid.getBinder();
        var dataItem;
        if (args["Template"] !== undefined) {
            dataItem = args;
            urlParams["newTemplateId"] = dataItem.Template.Id;
        }
        var failureFunction = this._serviceFailure;
        var successFunction = this._serviceSuccess;

        var keys = [];
        keys.push(dataItem.Id);

        if (binder._isMultilingual) {
            clientManager.set_uiCulture(binder.get_uiCulture());
        }
        clientManager.InvokePut(serviceUrl, urlParams, keys, dataItem.Id, successFunction, failureFunction, itemsGrid, false, null, context);
    },

    _serviceFailure: function (sender, args) {
        alert(sender.Detail);
    },

    _serviceSuccess: function (caller, data, request, context) {
        if (context && context.Command) {
            if (context.Command == "changeTemplate") {
                if (data && data == "true") {//Succeeded
                    caller.showPromptDialogByName("templateChangedDialog");
                } else {//Failed
                    caller.showPromptDialogByName("templateChangeFailedDialog");
                }
            } 
            context = null;
        }
        if (context) {
            caller.dataBind(null, context);
        }
        else {
            caller.dataBind();
        }
    },

    // utility methods
    _getBaseUrl: function (itemsListBase) {
        var baseUrl = itemsListBase.get_serviceBaseUrl();
        var serviceUrl = baseUrl.toString().substr(0, baseUrl.toString().indexOf('?'));
        return serviceUrl;
    },

    _hasTemplateInUse: function (templates) {
        /// <summary>Checks if at least one of selected templates is used by other pages or templates.</summary>
        /// <param name="templates">The collection of templates to be checked.</param>
        var isInUse = false;
        for (var i = 0; i < templates.length; i++) {
            var t = templates[i];
            if (t.PagesCount > 0 || t.ChildTemplatesCount > 0) {
                isInUse = true;
                break;
            }
        }
        return isInUse;
    }

    /* -------------------- properties ---------------- */
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplatesMasterGridViewExtension', Sys.Component);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
