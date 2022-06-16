
var masterGridView;
var sidebar;
var contextBar;
var toolbar;
var moreActionsMenu;
var queryParams;
var intervalListener = null;
var anyLibraryHasTaskFlag = false;
var clickedDataArgs = null;
var scheduledTaskPolledList = [];
var scheduledTaskInfoByDataItemId = {};
var currentActiveItemId = null;
// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    masterGridView = sender;
    sender.add_itemCommand(handleMasterGridViewItemCommand);

    sidebar = sender.get_sidebar();
    contextBar = sender.get_contextBar();
    toolbar = sender.get_toolbar();
    moreActionsMenu = getMoreActionsMenu();

    var binder = sender.get_binder();
    binder.add_onItemDataBound(handleBinderItemDataBound);
    binder.add_onDataBound(handleBinderDataBound);
    binder.add_onItemSelectCommand(itemSelectHandler);
    binder.set_unescapeHtml(true);

    var itemsGrid = sender.get_currentItemsList();
    itemsGrid.add_command(handleItemsGridCommand);
    itemsGrid.add_beforeCommand(handleItemsGridBeforeCommand);
    itemsGrid.add_itemCommand(handleItemsGridItemCommand);
    itemsGrid.add_dialogClosed(handleItemsGridDialogClosed);

    var editFolderDialog = $find(itemsGrid.get_radWindowManagerId()).getWindowByName("editFolder");
    if (editFolderDialog) {
        editFolderDialog.add_close(handleEditFolderDialogClose);
    }
    var editLibraryProperties = $find(itemsGrid.get_radWindowManagerId()).getWindowByName("editLibraryProperties");
    if (editLibraryProperties) {
        editLibraryProperties.add_close(handleEditFolderDialogClose);
    }

    var translationHandlers = { create: editTranslationHandler, edit: editTranslationHandler };
    itemsGrid.set_translationHandlers(translationHandlers);
    var notTranslatedItemsToHide = ".sfRegular,.sfMedium"; //hides "Actions" "File/Dim./Size" "Album/Categories/Tags" "Uploaded/Owner" columns
    itemsGrid.set_itemsToHideSelector(notTranslatedItemsToHide);
    var itemTitleSelector = ".sf_binderCommand_editMediaContentProperties";
    itemsGrid.set_itemTitleSelector(itemTitleSelector);

    queryParams = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();

    var folderId = queryParams.get("folderId", null);
    var widget = masterGridView.get_sidebar().getWidgetByName("FolderFilter");

    if (folderId && widget) {
        widget.set_selectedItemId(folderId);
        widget.getBinder().set_uiCulture(itemsGrid.getBinder().get_uiCulture());
        widget.getBinder().add_onDataBound(function () { widget.getBinder().setSelectedValues([folderId], true, true); });
    }
    else {
        var libraryId = masterGridView.get_parentId();
        if (libraryId && widget) {
            widget.set_selectedItemId(libraryId);
            widget.getBinder().add_onDataBound(function () { widget.getBinder().setSelectedValues([libraryId], true, true); });
        }
    }

    var displayMode = queryParams.get("displayMode", null);
    configureDisplayMode(displayMode);

    var parentActions = contextBar.getWidgetByName("FolderActionsWidget");
    if (parentActions) {
        var menuItem = null;
        if (!masterGridView.get_isParentEditable()) {
            menuItem = parentActions.get_menu().findItemByValue("editLibraryProperties");
            jQuery(menuItem.get_element()).hide();

            menuItem = parentActions.get_menu().findItemByValue("relocateLibrary");
            jQuery(menuItem.get_element()).hide();
        }
        if (!masterGridView.get_isParentDeletable()) {
            menuItem = parentActions.get_menu().findItemByValue("deleteLibrary");
            jQuery(menuItem.get_element()).hide();

        }
    }

    updateFolderProgressBar(itemsGrid._element);

    blacklistDialog("regenerateThumbnails");
    blacklistDialog("thumbnailSettings");
    blacklistDialog("relocateLibrary");
    blacklistDialog("transferLibrary");
    var thumbnailDialog = GetRadWindowManager().GetWindowByName("regenerateThumbnails");
    if (thumbnailDialog) {
        thumbnailDialog.add_close(handleRegenerateThumbnailsPromptDialogClose);
    }

    thumbnailDialog = GetRadWindowManager().GetWindowByName("thumbnailSettings");
    if (thumbnailDialog) {
        thumbnailDialog.add_close(handleThumbnailSettingsDialogClose);
    }
    var relocateDialog = GetRadWindowManager().GetWindowByName("relocateLibrary");
    if (relocateDialog) {
        relocateDialog.add_close(handleRelocateLibraryDialogClose);
    }
    var transferDialog = GetRadWindowManager().GetWindowByName("transferLibrary");
    if (transferDialog) {
        transferDialog.add_close(handleRelocateLibraryDialogClose);
    }

    var selectionWidget = contextBar.getWidgetByName("SelectionWidget");
    if (selectionWidget && masterGridView.get_currentItemsList() == masterGridView.get_itemsGrid()) {
        jQuery(selectionWidget.get_element()).hide();
    }
    clearIntervals();
}

function handleItemsGridBeforeCommand(sender, args) {
    switch (args.get_commandName()) {
        case "editFolder":
            var folderId = queryParams.get("folderId", null);
            var dataObject = { Id: folderId };
            var argument = {};
            if (sender.get_uiCulture())
                argument.language = sender.get_uiCulture();
            sender.openDialog('editFolder', dataObject, sender._getDialogParameters('editFolder'), dataObject, argument);
            args.set_cancel(true);
            break;
        case "changeLanguage":
            var widget = masterGridView.get_sidebar().getWidgetByName("FolderFilter");
            if (widget !== null) {
                widget.getBinder().set_uiCulture(args.get_commandArgument());
            }

            break;
        case "selectLibrary":
            var itemsGrid = sender;
            var selectedItems = itemsGrid.get_selectedItems();
            var showPrompt = false;
            for (i = 0; i < selectedItems.length; i++) {
                if (selectedItems[i].IsFolder) {
                    showPrompt = true;
                    break;
                }
            }

            if (!showPrompt)
                sender._dialogParameters['selectLibrary'] = sender._dialogParameters['selectLibrary'].replace("&showPrompt=True", "&showPrompt=");
            else {
                if (sender._dialogParameters['selectLibrary'].indexOf("&showPrompt=") != -1 && sender._dialogParameters['selectLibrary'].indexOf("&showPrompt=True") == -1)
                    sender._dialogParameters['selectLibrary'] = sender._dialogParameters['selectLibrary'].replace("&showPrompt=", "&showPrompt=True");
            }

            break;
        case "relocateLibrary":
            var parentId = queryParams.get("folderId", null);
            var isFolder;
            if (!parentId) {
                isFolder = false;
                parentId = masterGridView.get_parentId();
            }
            else {
                isFolder = true;
            }
            var dataItem = {
                Id: parentId,
                Url: masterGridView.get_parentUrlName(),
                LibraryType: masterGridView.get_libraryType(),
                ProviderName: masterGridView.get_provider(),
                IsFolder: isFolder
            };
            var params = {
                IsEditable: masterGridView.get_isParentEditable(),
                mode: "RelocateLibrary",
                parentId: dataItem.Id,
                parentType: masterGridView.get_libraryType(),
                providerName: masterGridView.get_provider()
            };
            var key = [];
            key.Id = dataItem.Id;

            sender.openDialog('relocateLibrary', dataItem, params, key);
            args.set_cancel(true);
            break;
        case "groupDelete":
            var shouldNotCancel;
            var selectedItems = masterGridView.get_selectedItems();
            if (selectedItems && selectedItems.length > 0) {
                shouldNotCancel = deleteItemsMonoLingual(sender, args, true, selectedItems);
            }
            args.set_cancel(!shouldNotCancel);
            break;
        default:
            break;
    }
}

function handleItemsGridCommand(sender, args) {
    switch (args.get_commandName()) {
        case "filterByStorage":
            var filterExpression = "BlobStorageProvider==\"" + args.get_commandArgument().get_dataItem().Name + "\"";
            sender.applyFilter(filterExpression);
            break;
        case "dynamicWidgetItemDataBound":
            var dataItem = args.get_commandArgument().get_dataItem();
            var element = args.get_commandArgument().get_itemElement();
            var jthumbImg = $(element).find(".thumbnailImg");
            if (dataItem.ThumbnailUrl != "") {
                jthumbImg.attr("src", dataItem.ThumbnailUrl);
                if (jthumbImg && jthumbImg.length > 0) {
                    resizeImage(jthumbImg, dataItem.Width, dataItem.Height, 32);
                }
            }
            else {
                jthumbImg.hide();
            }
            break;
        case "selectAll":
            var binder = $find(sender.get_binderId());
            if (binder.selectAll) {
                binder.selectAll();
            }
            break;
        case "deselectAll":
            var binder = $find(sender.get_binderId());
            if (binder.deselectAll) {
                binder.deselectAll();
            }
            break;
        case "showAllItems":
        case "showMyItems":
        case "showDraftItems":
        case "showPublishedItems":
        case "filter":
        case 'customDateFilterCommand':
            {
                var cmdArg = args.get_commandArgument();
                if (cmdArg.hasOwnProperty("propertyToFilter") && cmdArg.propertyToFilter == "LastModified") {
                    var filterExpression = '(LastModified>(' + cmdArg.dateFrom + ') OR Page.LastModified>(' + cmdArg.dateFrom + '))';
                    sender.applyFilter(args.get_commandArgument().filterExpression);
                }
                else {
                    sender.applyFilter(args.get_commandArgument().filterExpression);
                }
                args.set_cancel(true);
            }
            break;
        case 'reorder':
            sender.openDialog('reorder', args.get_commandArgument(), sender._getDialogParameters('reorder'), null);
            break;
        case 'libraryPermissions':
            var dataItem;
            if (args.get_commandArgument()) {
                dataItem = args.get_commandArgument()
            }
            else {
                dataItem = {
                    Id: sender.get_parent().get_parentId(),
                    Title: sender.get_titleText(),
                    ProviderName: sender.get_providerName()
                };
            }
            sender.openDialog('libraryPermissionsDialog', dataItem, sender._getDialogParameters('libraryPermissionsDialog'), null);
            break;
        case "editLibraryProperties":
            var dataItem = args.get_commandArgument();
            var dataObject;
            if (dataItem) {
                dataObject = { Id: dataItem.Id };
            }
            else {
                dataObject = { Id: sender.get_parent().get_parentId() };
            }
            sender.executeCommand("editLibraryProperties", dataObject, dataObject, null, null);
            args.set_cancel(true);
            break;
        case "uploadFile":
            var params = sender._getDialogParameters("upload");
            var folderId = queryParams.get("folderId", null);
            sender.openDialog("upload", { FolderId: folderId }, params, null, null);
            args.set_cancel(true);
            break;
        case "deleteLibrary":
            deleteItemsMonoLingual(sender, args, false);
            break;
        case "deleteFolder":
            var folderId = queryParams.get("folderId", null);
            if (folderId) {
                sender.deleteItems([folderId]);
            }
            break;
        default:
            break;
    }
}

function deleteItemsMonoLingual(sender, args, isGroup, dataItems) {
    var key;
    var dataItem;
    var multyKeys = new Array();
    var isImage = true;
    if (!isGroup) {
        dataItem = args.get_commandArgument();
        if (dataItem) {
            key = dataItem.Id;
        }
        else {
            key = sender.get_parent().get_parentId();
        }
        if (dataItem.IsFolder || !(dataItem.LibraryType == undefined))
            isImage = false;
    }
    else {
        for (var i = 0; i < dataItems.length; i++) {
            key = dataItems[i].Id;
            multyKeys[i] = key;
            if (dataItems[i].IsFolder || !(dataItems[i].LibraryType == undefined))
                isImage = false;
        }
    }
    var deleteFunc = function (e, args) {
        var commandName = args.get_commandName();
        if (commandName == 'cancel') {
            return;
        }

        if (isGroup) {
            sender.deleteItems(multyKeys, true, dataItems, false);
        }
        else {
            sender.deleteItems([key], true, dataItem, false);
        }
    }

    if (!isImage) {
        var dialog = sender.getPromptDialogByName("confirmDeleteSingle");
        var multipleItemsDeleteCommandButtonId = dialog._commands[1].ButtonClientId;
        var multipleItemsDeleteButton = jQuery('#' + multipleItemsDeleteCommandButtonId);
        multipleItemsDeleteButton.attr("style", "display: none !important");
        dialog.show_prompt('', '', deleteFunc, sender);
    }
    return isImage;
}

function handleItemsGridItemCommand(sender, args) {
    var dataItem = args.get_commandArgument();
    switch (args.get_commandName()) {
        case "filter":
            sender.applyFilter("Parent.Id == " + dataItem.ParentId);
            break;
        case "thumbnailSettings":
            var dialog = GetRadWindowManager().GetWindowByName("thumbnailSettings");

            if (dialog) {

                var onShow = function (sender, args) {
                    if (sender.AjaxDialog) {
                        sender.AjaxDialog.set_libraryDataItem(dataItem);
                    }
                }
                var onLoad = function (sender, args) {
                    onShow(sender, args);
                }
                var onClose = function (sender, args) {
                    sender.remove_show(onShow);
                    sender.remove_pageLoad(onLoad);
                    sender.remove_close(onClose);
                }

                dialog.add_pageLoad(onLoad);
                dialog.add_show(onShow);
                dialog.set_showContentDuringLoad(false);
                dialog.set_autoSizeBehaviors(5);
                dialog.add_close(onClose);
                dialog.show();
                Telerik.Sitefinity.centerWindowHorizontally(dialog);
            }
            args.set_cancel(true);
            break;
        case "delete":
            var shouldNotCancel = deleteItemsMonoLingual(sender, args, false);
            if (!shouldNotCancel)
                args.set_cancel(!shouldNotCancel);
            break;
        case "moveToSingle":
            currentActiveItemId = args.get_commandArgument().Id;
            if (!dataItem.IsFolder)
                sender._dialogParameters['moveToSingle'] = sender._dialogParameters['moveToSingle'].replace("&showPrompt=True", "&showPrompt=");
            else
                if (sender._dialogParameters['moveToSingle'].indexOf("&showPrompt=") != -1 && sender._dialogParameters['moveToSingle'].indexOf("&showPrompt=True") == -1)
                    sender._dialogParameters['moveToSingle'] = sender._dialogParameters['moveToSingle'].replace("&showPrompt=", "&showPrompt=True");
            break;
        default:
            break;
    }
}

//_handleCommand: function (sender, args) {
//    var commandName = args.get_commandName();
//    if (commandName == "createAndUpload") {
//        var workflowOperationName = args.get_commandArgument();
//        this._detailFormView._multipleSaving = false;
//        this._detailFormView.saveChanges(workflowOperationName);
//        args.set_cancel(true);
//    }
//}

function handleItemsGridDialogClosed(sender, args) {
    var context = null;
    if (args && args.get_context) {
        context = args.get_context();
    }

    if (context && context.get_widgetCommandName && context.get_widgetCommandName() == "createAndUpload") {
        var key = null;
        var params = sender._getDialogParameters("upload", args.get_dataItem());
        params.LibraryId = args.get_dataItem().Id;
        if (args.get_dataItem().ParentId) {
            params.FolderId = args.get_dataItem().Id;
        }
        sender.openDialog("upload", args.get_dataItem(), params, key, null);
    }

    if (context && context.WindowName == "selectLibrary") {
        var itemsGrid = sender;
        var selectedItems = itemsGrid.get_selectedItems();
        var selectedKeys = [];
        var length = selectedItems.length;
        while (length--) {
            var dataItem = selectedItems[length];
            selectedKeys.push(dataItem.Id);
        }
        moveLibraryCommon(sender, context, selectedKeys);
    }
    else if (context && context.WindowName == "moveToSingle") {
        var itemsGrid = sender;
        var selectedKeys = [currentActiveItemId];

        moveLibraryCommon(sender, context, selectedKeys);
    }
    else if (context && context.WindowName == "moveToAll") {
        var folderId = queryParams.get("folderId", null);
        var parentKeys;
        var isLibrary;
        if (folderId === null) {
            parentKeys = [masterGridView.get_parentId()];
            isLibrary = true;
        }
        else {
            parentKeys = [folderId];
            isLibrary = false;
        }
        moveLibraryCommon(sender, context, parentKeys, isLibrary);
    }

    else {
        var itemsGrid = sender;
        updateFolderProgressBar(itemsGrid._element);
        //event canceled - return
        return false;
    }
}

function updateFolderProgressBar(itemsGrid) {
    if (typeof masterGridView.get_libraryServiceUrl === 'function') {

        var baseUrl = masterGridView.get_libraryServiceUrl();
        var parentId = masterGridView.get_parentId();
        if (!(baseUrl && parentId))
            return;

        var serviceUrl = baseUrl + parentId;
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();

        clientManager.InvokeGet(serviceUrl, [], [], function (sender, data) {

            var contextBar = masterGridView.get_contextBar();
            var scheduledTaskProgressBar = contextBar.getWidgetByName("LibraryRunningTaskProgressBar");

            if (scheduledTaskProgressBar != null) {
                if (data.Item.RunningTask != "00000000-0000-0000-0000-000000000000") {
                    scheduledTaskProgressBar.set_taskId(data.Item.RunningTask);

                    var librariesPropertiesSection = contextBar.getSectionById("librariesPropertiesSection");
                    var folderBreadcrumb = $(librariesPropertiesSection.getElementsByClassName("sfFolderBreadcrumbWrp")[0]);

                    function hideGridOverlay() {
                        itemsGrid.className.replace('sfRelative', '');
                        var gridOverlay = document.getElementById('gridOverlayID');
                        if (gridOverlay) {
                            gridOverlay.parentNode.removeChild(gridOverlay);
                        }
                        folderBreadcrumb.removeClass('sfDisplayNone');
                    }

                    function showGridOverlay() {
                        var gridOverlay = document.getElementById('gridOverlayID');
                        if (gridOverlay)
                            return;

                        gridOverlay = document.createElement("div");
                        gridOverlay.className = 'sfGridOverlay';
                        gridOverlay.id = 'gridOverlayID';
                        itemsGrid.className = 'sfRelative';
                        itemsGrid.appendChild(gridOverlay);
                        //Hiding the library breadcrumb 
                        folderBreadcrumb.addClass('sfDisplayNone');
                    }

                    scheduledTaskProgressBar.add_onTaskProgress(function (sender, args) {
                        var taskProgress = args.get_data().Progress;
                        if (taskProgress == 100) {
                            masterGridView.get_binder().DataBind();
                            hideGridOverlay();
                        }
                        else
                            showGridOverlay();

                        var taskStatus = args.get_data().Status;
                        if (taskStatus == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Failed ||
                            taskStatus == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Stopped) {
                            hideGridOverlay();
                        }

                    })
                }

            }

        }, null, this);
    }
}

function successCallback(caller, resultData) {
    // rebind the grid
    caller.dataBind();
    updateFolderProgressBar(caller._element);
}

function successCoverCallback(caller, resultData) {
    caller.get_messageControl().showPositiveMessage(this.Context.Title.htmlEncode() + " has been set as library cover");
}

function failureCallback(result) {
    alert(result.Detail);
}

function moveLibraryCommon(sender, context, selectedKeys, isLibrary) {
    var itemsGrid = sender;
    var binder = itemsGrid.getBinder();
    var clientManager = binder.get_manager();
    var serviceBaseUrl;
    if (isLibrary)
        serviceBaseUrl = masterGridView.get_libraryServiceUrl();
    else
        serviceBaseUrl = binder.get_serviceBaseUrl();
    var serviceUrl = null;
    var index = serviceBaseUrl.indexOf(".svc");
    if (index > -1) {
        serviceUrl = serviceBaseUrl.substring(0, index + 4) + "/batch/";

        var urlParams = [];
        urlParams["provider"] = binder.get_provider();
        urlParams["itemType"] = context.ItemType;
        urlParams["parentItemType"] = context.ParentItemType;
        var parentId = context.Id;
        var keys = [];
        keys.push(parentId);

        clientManager.InvokePut(serviceUrl, urlParams, keys, selectedKeys, successCallback, failureCallback, sender);
    }
}

function handleMasterGridViewItemCommand(sender, args) {
    clickedDataArgs = args;
    var commandName = args.get_commandName();
    var dataItem = args.get_dataItem();
    var mediaUrl = dataItem.MediaUrl;
    switch (commandName) {
        case "viewOriginalImage":
        case "viewOriginalDocument":
            window.open(mediaUrl, "_blank");
            break;
        case "download":
            var appender = "?";
            if (mediaUrl.indexOf('?') > 0) { appender = "&"; }
            document.location.href = mediaUrl.indexOf("download=true") === -1 ? mediaUrl + appender + "download=true" : mediaUrl;
            break;
        case "openLibrary":
            var query = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
            query.set("folderId", dataItem.Id);
            var uiCulture = sender.get_binder().get_manager().get_uiCulture();
            if (uiCulture) {
                query.set("lang", uiCulture);
            }
            location.search = query.toString(true);
            break;
        case "setAsCover":
            var binder = sender.get_binder();
            var clientManager = binder.get_manager();
            var serviceBaseUrl = binder.get_serviceBaseUrl();
            var serviceUrl = null;
            var index = serviceBaseUrl.indexOf(".svc");
            if (index > -1) {
                serviceUrl = serviceBaseUrl.substring(0, index + 4) + "/cover/";
                var urlParams = [];
                urlParams["provider"] = binder.get_provider();
                var keys = [];
                keys.push(dataItem.Id);

                clientManager.InvokePost(serviceUrl, urlParams, keys, [], successCoverCallback, failureCallback, sender, dataItem);
            }
            break;
    }
}

function manageScheduledTask(args, command) {

    var dataItem = args.get_dataItem();
    var requestUrl = masterGridView.get_baseUrl() + "Sitefinity/Services/SchedulingService.svc";
    requestUrl = String.format("{0}/{1}/manage?command={2}", requestUrl, dataItem.ScheduledTaskInfo.Id, command);

    jQuery.ajax({
        type: 'PUT',
        url: requestUrl,
        processData: false,
        contentType: "application/json",
        success: function () {
            if ((command == Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Restart) || (command == Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Resume)) {
                addScheduledTaskToPolledList(args, dataItem.Id);
            }
        },
        error: function (jqXHR) {
            alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
        }
    });
}

function manageScheduledTaskUI(args) {
    var dataItem = args.get_dataItem();
    var taskInfo = args.FindControl("taskInfo");
    $(taskInfo).hide();

    if (!dataItem.ScheduledTaskInfo && scheduledTaskInfoByDataItemId[dataItem.Id]) {
        dataItem.ScheduledTaskInfo = scheduledTaskInfoByDataItemId[dataItem.Id];
    }

    if (dataItem.ScheduledTaskInfo && dataItem.ScheduledTaskInfo.Description) {
        addScheduledTaskToPolledList(args, dataItem.Id);
    }
    else {
        var thumbnailsRegenError = args.FindControl("tmbRegenNeeded");

        if (dataItem.NeedThumbnailsRegeneration) {
            $(thumbnailsRegenError).show();
        }
    }
}

function addScheduledTaskToPolledList(args, itemId) {
    window.clearInterval(scheduledTaskPolledList[itemId]);
    scheduledTaskPolledList[itemId] = window.setInterval(function () { refreshProgressBar(args); }, 1000);
    var actions = ['edit', 'relocateLibrary', 'transferLibrary', 'regenerateThumbnails', 'thumbnailSettings', 'permissions'];
    manageActionsVisibility(actions, args, false);
}

function removeScheduledTaskFromPolledList(args, itemId, actionsVisible) {
    window.clearInterval(scheduledTaskPolledList[itemId]);
    scheduledTaskInfoByDataItemId[itemId] = null;
    var actions = ['edit', 'relocateLibrary', 'transferLibrary', 'regenerateThumbnails', 'thumbnailSettings', 'permissions'];
    manageActionsVisibility(actions, args, actionsVisible);
}

function manageActionsVisibility(actions, args, visible) {
    var dataItem = args.get_dataItem();
    var hasPermissions = true;
    if ((typeof (dataItem.IsManageable) != "undefined") && (dataItem.IsManageable != null) && (!dataItem.IsManageable))
        hasPermissions = false;
    for (var i = 0; i < actions.length; i++) {
        var element = $(args.get_itemElement()).find(".sf_binderCommand_" + actions[i]);
        if (visible && hasPermissions) {
            element.show();
        } else {
            element.hide();
        }
    }
    var separators = $(args.get_itemElement()).find(".sfSeparator");
    if (visible && hasPermissions) {
        separators.show();
    } else {
        separators.hide();
    }
}


function refreshProgressBar(args) {
    var dataItem = args.get_dataItem();
    var taskDescription = args.FindControl("taskDescription");
    var taskStatus = args.FindControl("taskStatus");
    var taskInfo = args.FindControl("taskInfo");
    $(taskInfo).show();
    var requestUrl = masterGridView.get_baseUrl() + "Sitefinity/Services/SchedulingService.svc";
    //IE FIX
    var dummyParameter = new Date().getTime();
    requestUrl = String.format("{0}/{1}/progress?time={2}", requestUrl, dataItem.ScheduledTaskInfo.Id, dummyParameter);
    var emptyGuid = "00000000-0000-0000-0000-000000000000";
    jQuery.ajax({
        type: 'GET',
        url: requestUrl,
        contentType: "application/json",
        success: function (taskData) {
            var thumbnailsRegenError = args.FindControl("tmbRegenNeeded");
            $(thumbnailsRegenError).hide();
            if (taskData && taskData.Id != emptyGuid) {
                if ($(taskStatus).length > 0) {
                    $(taskDescription).html(taskData.Description);
                    var statusMessage = Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.toString(taskData.Status);
                    if (statusMessage == "Started") {
                        statusMessage = "In progress";
                    }

                    $(taskStatus).html(statusMessage);

                    if (taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Failed || taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Stopped) {
                        $(taskStatus).removeClass("sfSuccessTxt");
                        $(taskStatus).addClass("sfWarningTxt");
                    } else {
                        $(taskStatus).removeClass("sfWarningTxt");
                        $(taskStatus).addClass("sfSuccessTxt");
                    }
                } else {
                    // Preserve previous behavior for not updated templates
                    var description = taskData.Description + " " + Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.toString(taskData.Status);
                    $(taskDescription).html(description);
                }


                if (taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Failed) {
                    showTaskFailed(args, taskData);
                    removeScheduledTaskFromPolledList(args, dataItem.Id, true);
                }
                else if (taskData.ProgressStatus >= 0) {
                    showTaskProgress(args, taskData);
                    var errorPanel = args.FindControl("errorDetailsPanel");
                    $(errorPanel).hide();
                }
            }
            else {
                $(taskInfo).hide();
                removeScheduledTaskFromPolledList(args, dataItem.Id, true);

                if (dataItem.TaskInfo && dataItem.TaskInfo.TaskName == "MoveLibrary") {
                    masterGridView.get_currentItemsList().dataBind();
                }
            }
        }
    });
}

function showTaskProgress(args, taskData) {
    var dataItem = args.get_dataItem();
    var progressReport = args.FindControl("taskProgressReport");
    var progressBarEl = args.FindControl("taskProgressBar");

    progressReport.innerHTML = taskData.ProgressStatus + "%";
    var colorCssClass = "sfProgressStarted";
    if (taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Stopped) {
        colorCssClass = "sfProgressStopped";
        removeScheduledTaskFromPolledList(args, dataItem.Id, false);
        updateBinderCommandButton(args, Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Resume);
    }
    else {
        updateBinderCommandButton(args, Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Stop);
    }
    updateProgressBar(progressBarEl, taskData.ProgressStatus, colorCssClass);
}

function showTaskFailed(args, taskData) {
    var taskInfo = args.FindControl("taskInfo");
    $(taskInfo).show();
    var errorPanel = args.FindControl("errorDetailsPanel");
    $(errorPanel).show();
    var errorDetails = args.FindControl("errorDetailsMessage");
    $(errorDetails).text(taskData.StatusMessage);
    var errorLink = args.FindControl("errorDetailsLink");
    $(errorLink).unbind('click');
    $(errorLink).click(function () { $(errorDetails).toggle(); return false; });
    var progressBarEl = args.FindControl("taskProgressBar");
    updateProgressBar(progressBarEl, taskData.ProgressStatus, "sfProgressStopped");
    updateBinderCommandButton(args, Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Restart);
}

function updateBinderCommandButton(args, command) {
    var dataItem = args.get_dataItem();
    var commandButton = args.FindControl("taskCommand");
    if ((typeof (dataItem.IsManageable) != "undefined") && (dataItem.IsManageable != null) && (!dataItem.IsManageable)) {
        $(commandButton).hide();
    }
    else {
        $(commandButton).removeAttr("href");
        $(commandButton).html(Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.toString(command));
        $(commandButton).unbind('click');
        $(commandButton).click(function () {
            manageScheduledTask(args, command);
        });
    }
}

function clearIntervals() {
    for (var i = 0; i < scheduledTaskPolledList.length; i++) {
        window.clearInterval(scheduledTaskPolledList[i]);
    }
}

function handleBinderDataBound() {
    if (typeof masterGridView != 'undefined') {
        var grid = $(masterGridView.get_element());
        var gridTable = $(grid).find(".rgMasterTable");
        var dlgSidebar = null;
        var dlgContent = null;
        var dlgMain = null;
        var wndWidth = null;
        if (gridTable.is(":visible")) {
            var gridWidth = gridTable.width();

            if (gridWidth + 80 > $(window).width() * 0.8) {
                //if the content is wider than the viewport
                dlgSidebar = $(".sfSidebar");
                dlgContent = $(".sfContent");
                dlgMain = $(".sfMain");

                dlgContent.width(gridWidth + 80);
                dlgSidebar.width(Math.floor(dlgContent.width() / 4));
                wndWidth = dlgSidebar.width() + dlgContent.width();
                dlgMain.width(wndWidth);
                $("body").width(wndWidth);
            }
        }
        if ($("body").width() < $(window).width()) {
            //if the window is resized from smaller to wider
            dlgSidebar = $(".sfSidebar");
            dlgContent = $(".sfContent");
            dlgMain = $(".sfMain");

            dlgContent.width("80%");
            dlgSidebar.width("20%");
            wndWidth = dlgSidebar.width() + dlgContent.width();
            dlgMain.width("auto");
            $("body").width("auto");
        }

        var contextActionMenu = getContextActionMenuWidget();
        if (contextActionMenu) {
            var folderId = queryParams.get("folderId", null);
            if (folderId) {
                jQuery(contextActionMenu.get_element()).find(".sf_NotForFolder").parent().hide();
            }
            else {
                jQuery(contextActionMenu.get_element()).find(".sf_ForFolder").parent().hide();
            }
        }

    }
}

function getContextActionMenuWidget() {
    var widget = null;
    if (contextBar) {
        widget = contextBar.getWidgetByName("FolderActionsWidget");
    }
    return widget;
}

function handleBinderItemDataBound(sender, args) {
    var dataItem = args.get_dataItem();
    var width = dataItem.Width;
    var height = dataItem.Height;

    // hide link "More translations.." from folders
    var commandLinks = $(args.get_itemElement()).find("[class*='sf_binderCommand']");
    for (var curCmd = 0; curCmd < commandLinks.length; curCmd++) {
        var cmdNameMatch = commandLinks[curCmd].className.match(/sf_binderCommand_(\S*)/i);
        if (cmdNameMatch && cmdNameMatch[1]) {
            var commandName = cmdNameMatch[1];
            if ((dataItem.IsFolder) && (commandName == "showMoreTranslations" || commandName == "hideMoreTranslations")) {
                commandLinks[curCmd].style.display = "none";
            }
        }
    }

    // resizing Images thumbnails
    var img = $(args.get_itemElement()).find(".sfImgTmb .sfSmallImgTmb img");
    if (img && img.length > 0 && width > 0 && height > 0) {
        resizeImage(img, width, height, 60);
    } else {
        if (img.attr("height"))
            img.removeAttr("height");
        if (img.attr("width"))
            img.removeAttr("width");
    }

    // remove width and height for svg in list view
    var img = $(args.get_itemElement()).find(".sfBigImgTmb .sfSvgImg");
    if (img && img.length > 0 && width === 0 && height === 0) {
        if (img.attr("height"))
            img.removeAttr("height");
        if (img.attr("width"))
            img.removeAttr("width");
    }

    // resizing Library thumbnails
    img = $(args.get_itemElement()).find(".sfImgTmb .sfSmallLibTmb img");
    if (img && img.length > 0) {
        img.on("load", function () {
            resizeImage($(this), this.offsetWidth, this.offsetHeight, 42);
        });
    }

    //hide video thumbnamil if snapshotUrl is not generated
    img = $(args.get_itemElement()).find(".sfVideoSelector img");
    if (img) {
        var snapshotUrl = dataItem.SnapshotUrl;
        if (!snapshotUrl || snapshotUrl == "") {
            img.hide();
        }
    }

    if (dataItem.LibraryType) {
        var noViewPermissions = typeof dataItem.IsViewable != "undefined" && dataItem.IsViewable != null && !dataItem.IsViewable;
        if (noViewPermissions) {
            //make the title and the album thumbnail unclickable
            $(args.get_itemElement()).find(".sfItemTitle").addClass("sfDisabled").removeClass("sf_binderCommand_viewItemsByParent").unbind("click").removeAttr("href");
            $(args.get_itemElement()).find(".sf_binderCommand_viewItemsByParent").removeClass("sf_binderCommand_viewItemsByParent").unbind("click").removeAttr("href");
        }
    }

    //tripple checking here explicitly because not all data items have the IsManageable property.
    if ((typeof (dataItem.IsManageable) != "undefined") && (dataItem.IsManageable != null) && (!dataItem.IsManageable)) {
        //------------------------------------ For libraries ------------------------------------
        if (dataItem.LibraryType) {

            //hide the upload command
            $(args.get_itemElement()).find(".sf_binderCommand_upload").parent().hide();

            //hide the edit properties command
            $(args.get_itemElement()).find(".sf_binderCommand_edit").parent().hide();

            //hide the "change library" url command
            $(args.get_itemElement()).find(".sf_binderCommand_relocateLibrary").parent().hide();

            //hide the "move to another storage" command
            $(args.get_itemElement()).find(".sf_binderCommand_transferLibrary").parent().hide();

            $(args.get_itemElement()).find(".sf_binderCommand_thumbnailSettings").parent().hide();
            $(args.get_itemElement()).find(".sf_binderCommand_regenerateThumbnails").parent().hide();
        }

            //------------------------------------ For individual media items ------------------------------------
        else {
            //hide the unpublish command
            $(args.get_itemElement()).find(".sf_binderCommand_unpublish").hide();

            //hide the publish command
            $(args.get_itemElement()).find(".sf_binderCommand_publish").hide();

            //hide the delete command
            $(args.get_itemElement()).find(".sf_binderCommand_delete").hide();

            //hide the edit properties command
            $(args.get_itemElement()).find("ul.actionsMenu li").children("a.sf_binderCommand_editMediaContentProperties").hide();
            $(args.get_itemElement()).find("a.sf_binderCommand_editMediaContentProperties").children("img").parent().remove("sf_binderCommand_editMediaContentProperties").unbind("click").removeAttr("href");
            $(args.get_itemElement()).find(".sf_binderCommand_editMediaContentProperties").removeClass("sf_binderCommand_editMediaContentProperties").unbind("click").removeAttr("href");

            //hide the revision history command
            $(args.get_itemElement()).find(".sf_binderCommand_historygrid").hide();
        }

    }
    //tripple checking here explicitly too because not sue all data items have the IsDeletable property.
    if ((typeof (dataItem.IsDeletable) != "undefined") && (dataItem.IsDeletable != null) && (!dataItem.IsDeletable)) {
        //hide the "delete item" command (and adjacent separator).
        $(args.get_itemElement()).find(".sfDeleteItm").parent().hide();
        $(args.get_itemElement()).find(".sfDeleteItm").parent().siblings(".sfSeparator").hide();
    }

    if (typeof (dataItem.IsFolder) != 'undefined' && dataItem.IsFolder) {
        jQuery(args.get_itemElement()).find(".sfTranslationCommands").hide();
        jQuery(args.get_itemElement()).find(".sf_NotForFolder").parent().hide();
    }
    else {
        jQuery(args.get_itemElement()).find(".sf_ForFolder").parent().hide();
    }

    //Remove "View all sizes" from "Action" menu for SVG images
    if (typeof (dataItem.Extension) != 'undefined' && dataItem.IsVectorGraphics === true) {
        jQuery(args.get_itemElement()).find(".sfViewAllSizes").parent().hide();
    }

    //TODOTMB: Modify this once there is support for videos and documents libraries.
    if (dataItem.LibraryType && dataItem.LibraryType != "Telerik.Sitefinity.Libraries.Model.Album") {
        $(args.get_itemElement()).find(".sf_binderCommand_regenerateThumbnails").parent().hide();
        $(args.get_itemElement()).find(".sf_binderCommand_thumbnailSettings").parent().hide();
    }

    // resizing Albums thumbnails
    img = $(args.get_itemElement()).find(".sfAlbumTmb img");
    if (img && img.length > 0) {
        var thumbnailUrl = dataItem.ThumbnailUrl;
        if (!thumbnailUrl || thumbnailUrl == "") {
            img.hide();
        }
        else {
            resizeImage(img, width, height, 80);
        }
    }

    manageScheduledTaskUI(args);

}

function resizeImage(img, w, h, size) {
    if (h > size || w > size) {
        if (h == w) {
            img.attr("height", size);
            img.attr("width", size);
        }
        else if (h > w) {
            img.attr("width", size);
            // IE fix
            img.removeAttr("height");
        }
        else {
            img.attr("height", size);
            // IE fix
            img.removeAttr("width");
        }
    }
}

function getToolbarUploadWidget() {
    var widget = null;
    if (toolbar) {
        widget = toolbar.getWidgetByCommandName("upload");
    }
    return widget;
}

function getMoreActionsMenu() {
    var widget = null;
    if (toolbar) {
        widget = toolbar.getWidgetByName("MoreActionsWidget");
    }
    return widget;
}

function editTranslationHandler(sender, args) {
    args.set_cancel(true);
    var list = args.get_list();
    list.executeItemCommand("editMediaContentProperties", args.get_dataItem(), args.get_key(), { language: args.get_language(), languageMode: args.get_commandName() });
}

function updateProgressBar(parentElement, percents, colorCssClass) {

    if (!colorCssClass) colorCssClass = "sfProgressStarted";

    if (parentElement.hasChildNodes()) {
        while (parentElement.childNodes.length >= 1) {
            parentElement.removeChild(parentElement.firstChild);
        }
    }
    $(parentElement).removeClass("sfProgressStarted sfProgressStopped");
    $(parentElement).addClass(colorCssClass);

    var innerParentDiv = document.createElement('div');
    $(innerParentDiv).addClass("sfProgressBar");

    var innerDiv = document.createElement('div');
    var minWidth = 3;
    var width = percents > 0 ? percents : minWidth;
    innerDiv.setAttribute('style', 'width : ' + width + '%;');
    $(innerDiv).addClass("sfProgressBarIn");

    innerParentDiv.appendChild(innerDiv);
    parentElement.appendChild(innerParentDiv);
}

function itemSelectHandler(sender, args) {
    if (moreActionsMenu) {
        var hasMediaItemsSelected = false;
        var binder = sender;
        for (var i = 0; i < binder.get_selectedItems().length; i++) {
            var dataItem = binder.get_selectedItems()[i];
            if (dataItem && !dataItem.IsFolder) {
                hasMediaItemsSelected = true;
                break;
            }
        }

        var publishItem = moreActionsMenu.get_menu().findItemByValue("groupPublish");
        var unpublishItem = moreActionsMenu.get_menu().findItemByValue("groupUnpublish");
        var bulkEditItem = moreActionsMenu.get_menu().findItemByValue("bulkEdit");

        if (hasMediaItemsSelected) {
            if (publishItem)
                publishItem.enable();
            if (unpublishItem)
                unpublishItem.enable();
            if (bulkEditItem)
                bulkEditItem.enable();
        }
        else {
            if (publishItem) {
                publishItem.disable();
                publishItem.set_disabledCssClass("sfDisabledLinkBtn");
            }
            if (unpublishItem) {
                unpublishItem.disable();
                unpublishItem.set_disabledCssClass("sfDisabledLinkBtn");
            }
            if (bulkEditItem) {
                bulkEditItem.disable();
                bulkEditItem.set_disabledCssClass("sfDisabledLinkBtn");
            }
        }
    }
}

function configureDisplayMode(displayMode) {
    if (displayMode) {
        var reorderWidget = toolbar.getWidgetByCommandName("reorder");
        var mainSection = sidebar.getSectionById("mainSection");
        var showAllMediaItemsSection = sidebar.getSectionById("showAllMediaItemsSection");
        var librariesPropertiesSection = contextBar.getSectionById("librariesPropertiesSection");

        if (reorderWidget) {
            jQuery(reorderWidget.get_element()).hide();
        }
        if (mainSection) {
            jQuery(mainSection).hide();
        }
        if (showAllMediaItemsSection) {
            jQuery(showAllMediaItemsSection).hide();
        }
        if (librariesPropertiesSection) {
            jQuery(librariesPropertiesSection).hide();
        }

        var clientLabelManager = masterGridView.get_clientLabelManager();
        var moduleTitle;
        if (displayMode == "allImages") {
            moduleTitle = clientLabelManager.getLabel("ImagesResources", "AllImages")
        }
        else if (displayMode == "allDocuments") {
            moduleTitle = clientLabelManager.getLabel("DocumentsResources", "AllDocumentsAndFiles")
        }
        else {
            moduleTitle = clientLabelManager.getLabel("VideosResources", "AllVideos")
        }

        jQuery("#" + masterGridView.get_titleID()).html(moduleTitle);
    }
    else {
        var showAllLibrariesSection = sidebar.getSectionById("showAllLibrariesSection");
        var categoryFilterSection = sidebar.getSectionById("categoryFilterSection");
        var tagFilterSection = sidebar.getSectionById("tagFilterSection");
        var dateFilterSection = sidebar.getSectionById("dateFilterSection");
        var moreFiltersSection = sidebar.getSectionById("moreFiltersSection");

        if (showAllLibrariesSection) {
            jQuery(showAllLibrariesSection).hide();
        }
        if (categoryFilterSection) {
            jQuery(categoryFilterSection).hide();
        }
        if (tagFilterSection) {
            jQuery(tagFilterSection).hide();
        }
        if (dateFilterSection) {
            jQuery(dateFilterSection).hide();
        }
        if (moreFiltersSection) {
            jQuery(moreFiltersSection).hide();
        }
    }
}

function handleEditFolderDialogClose(sender, args) {
    if (sender.DetailFormView.get_binder().get_lastModifiedData()) {
        var folder = sender.DetailFormView.get_binder().get_lastModifiedData().Item;
        // check if we are editing folder properties from Images by library screen
        var doNotUpdateTitle =
            folder && folder.ParentId &&
            jQuery(masterGridView.get_contextBar().get_element()).find(".sfFolderBreadcrumbWrp .sfSep").length == 1;

        if (folder && folder.Title && !doNotUpdateTitle) {
            jQuery("#" + masterGridView.get_titleID() + " i").html(folder.Title.PersistedValue);
            jQuery(masterGridView.get_contextBar().get_element()).find(".sfFolderBreadcrumbWrp span").html(folder.Title.PersistedValue);
            if (masterGridView.get_currentItemsList()) {
                masterGridView.get_currentItemsList().set_titleText(folder.Title.PersistedValue);
            }
        }
    }
}

function handleRegenerateThumbnailsPromptDialogClose(sender, args) {

    var dataItem = clickedDataArgs.get_dataItem();
    var context = args.get_argument();
    if (context.Command == "submit" && dataItem != null) {
        var requestUrl = String.format("{0}?{1}={2}&{3}={4}&{5}={6}", masterGridView.get_baseUrl() + "Sitefinity/Services/ThumbnailService.svc/regenerate",
                "libraryId", dataItem.Id,
                "libraryProvider", dataItem.ProviderName,
                "libraryType", dataItem.LibraryType);

        jQuery.ajax({
            type: 'PUT',
            url: requestUrl,
            processData: false,
            contentType: "application/json",
            success: function (taskId) {
                clickedDataArgs.get_dataItem().ScheduledTaskInfo = { Id: taskId };
                addScheduledTaskToPolledList(clickedDataArgs, dataItem.Id);
            },
            error: function (jqXHR) {
                alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
            }
        });
    }
}

function handleThumbnailSettingsDialogClose(sender, args) {
    var context = args.get_argument();
    if (context && context.Command && context.Command == "submit" && context.Data) {
        var dataItem = clickedDataArgs.get_dataItem();

        var requestUrl = String.format("{0}?{1}={2}&{3}={4}&{5}={6}", masterGridView.get_baseUrl() + "Sitefinity/Services/ThumbnailService.svc/regenerate",
                "libraryId", dataItem.Id,
                "libraryProvider", dataItem.ProviderName,
                "libraryType", dataItem.LibraryType);

        jQuery.ajax({
            type: 'PUT',
            url: requestUrl,
            data: Telerik.Sitefinity.JSON.stringify(context.Data),
            contentType: "application/json",
            success: function (taskId) {
                clickedDataArgs.get_dataItem().ScheduledTaskInfo = { Id: taskId };
                addScheduledTaskToPolledList(clickedDataArgs, dataItem.Id);
            },
            error: function (jqXHR) {
                alert(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
            }
        });
    }
}

// 1. This function is called right after closing the "Change library URL" dialog
// 2. context.Data is the taskId returned from the RelocateLibrary method in LibraryRelocationService.cs
// 3. When we are changing the URL of a folder inside some library we are calling RelocateFolder, wchich is a void method, so context.Data is empty
// 4. masterGridView.get_binder().DataBind(); should always be called even if context.Data is empty
function handleRelocateLibraryDialogClose(sender, args) {
    var context = args.get_argument();
    if (context && context.Command && context.Command == "submit" && context.Data) {
        var dataItem = clickedDataArgs.get_dataItem();
        scheduledTaskInfoByDataItemId[dataItem.Id] = {
            Id: context.Data,
            Description: "Move to another storage"
        };
    }

    var millisecondsToWait = 500;
    setTimeout(function () {
        masterGridView.get_binder().DataBind();
    }, millisecondsToWait);
}

function blacklistDialog(dialogName) {
    var dialog = GetRadWindowManager().GetWindowByName(dialogName);
    if (dialog) {
        var dialogManager = window.top.GetDialogManager();
        dialogManager.blacklistWindow(dialog);
    }
}


$(window).bind('resize', handleBinderDataBound);