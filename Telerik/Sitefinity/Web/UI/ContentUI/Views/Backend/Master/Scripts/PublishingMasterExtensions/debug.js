var sidebar;
var contextBar;
var filteredByLibrary = false;
var itemsGrid;
var actionCommandPrefix = "sf_binderCommand_";
var checkProgress = null;
var currentlyReindexedPoints = {};
var serviceBaseUrl;
var clientManager;
var itemToRowMapping = {}

// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    sidebar = sender.get_sidebar();
    contextBar = sender.get_contextBar();

    var binder = sender.get_binder();
    binder.add_onDataBound(handleBinderDataBound);
    binder.add_onItemDataBound(handleBinderItemDataBound);
    binder.add_onItemCommand(handleBinderItemCommand);
    binder.set_unescapeHtml(true);

    serviceBaseUrl = binder.get_serviceBaseUrl();
    var index = serviceBaseUrl.indexOf(".svc");
    serviceBaseUrl = serviceBaseUrl.substring(0, index + 4);

    clientManager = binder.get_manager();

    itemsGrid = sender.get_currentItemsList();
    itemsGrid.add_command(handleItemsGridCommand);

    startMonitoringReindexProgress();
}

function handleItemsGridCommand(sender, args) {
    switch (args.get_commandName()) {
        case "activeFeeds":
            itemsGrid.applyFilter("IsActive == true");
            break;
        case "inactiveFeeds":
            itemsGrid.applyFilter("IsActive == false");
            break;
        case "filterContent":
            var cArg = args.get_commandArgument();
            itemsGrid.applyFilter("ContentType == \"" + cArg.value + "\"");
            break;
        default:
            break;
    }
}

function handleBinderItemCommand(sender, args) {
    var commandName = args.get_commandName();
    var dataItem = args.get_dataItem();
    var itemElement = args.get_itemElement();

    switch (commandName) {
        case "startUpdating":
        case "stopUpdating":
            {
                var setActive = commandName === "startUpdating";
                serviceUrl = serviceBaseUrl + "/setactive/" + dataItem.Id + "/";
                var urlParams = {};
                urlParams["setActive"] = setActive;
                clientManager.InvokePut(serviceUrl, urlParams, {}, {}, successCallback, failureCallback, sender);
            }
            break;
        case "runPipes":

            var serviceUrl = serviceBaseUrl + "/runpipes/" + dataItem.Id +"/";
            clientManager.InvokePut(serviceUrl, {}, {}, {}, successCallback, failureCallback, sender);
            break;
        case "reindex":
            var serviceUrl = serviceBaseUrl + "/reindex/" + dataItem.Id + "/";            
            
            var dateRowId = itemElement.attributes["id"].nodeValue
            monitorReindexingProgress();

            var context = { itemID: dateRowId };
            clientManager.InvokePut(serviceUrl, {}, {}, {}, successCallback, failureCallback, sender, false, null, context);
            break;
        case "deactivate":
            var serviceUrl = serviceBaseUrl + "/setactive/" + dataItem.Id + "/?&setActive=false";
            var dateRowId = itemElement.attributes["id"].nodeValue;
            
            var context = { itemID: dateRowId };
            clientManager.InvokePut(serviceUrl, {}, {}, {}, successCallback, failureCallback, sender, false, null, context);
            break;
        default:
            break;
    }
}

function successCallback(caller, data, request, context) {
    itemsGrid.dataBind();
}

function failureCallback(error, caller, context) {
    alert(error.Detail);
}

function handleBinderItemDataBound(sender, args) {
    var dataItem = args.get_dataItem();
    var pipes = dataItem.OutputPipes;
    var elem = args.FindControl("publishedAs");
    var list = jQuery("<ul class='sfPipeContentList' />").appendTo(jQuery(elem));
    jQuery.each(pipes, function (i, pipe) {
        jQuery("<li />").appendTo(list).addClass("sfPipeTitle sfPipe" + pipe.PipeName).html(pipe.UIName);
    });
    var command = dataItem.IsActive ? "startUpdating" : "stopUpdating";
    itemsGrid.removeActionsMenuItems([actionCommandPrefix + command], args.get_itemElement());

    if (dataItem.IsBackend) {
        itemsGrid.removeActionsMenuItems([actionCommandPrefix + "delete"], args.get_itemElement());
        if (!dataItem.IsActive)
            itemsGrid.removeActionsMenuItems([actionCommandPrefix + "deactivate"], args.get_itemElement());
    } else {
        itemsGrid.removeActionsMenuItems([actionCommandPrefix + "deactivate"], args.get_itemElement());
    }

    $("#" + args._itemElement.id).find(".sfProgressTaskCommand").click(function () { hideProcessingLoader(args._itemElement.id); itemsGrid.dataBind(); });
    itemToRowMapping[dataItem.Id] = args._itemElement.id;
}

function handleBinderDataBound(sender, args) {
    monitorReindexingProgress();
}

function showProcessingLoader(itemID, progress, statusMessage) {
    var itemEl = jQuery("#" + itemID + " .sfDateAuthor");
    var actionsEl = jQuery("#" + itemID + " .sfMoreActions .actionsMenu");
    var progressStatus = progress < 0 ? itemEl.find(".resPending").html() : progress >= 100 ? itemEl.find(".resDone").html() : itemEl.find(".resInProgress").html();
    if (progress >= 100)
        statusMessage = itemEl.find(".resIndexingContent").html();

    itemEl.find(".sfLine").addClass("sfDisplayNoneImportant");

    var wrapper = itemEl.find(".sfProgress");
    wrapper.find(".sfProgressTaskDescription").html(statusMessage);
    wrapper.find(".sfMoveProgressWrp .sfProgressPercentage").html((progress < 0 ? 0 : progress) + "%");
    wrapper.find(".taskStatus").html(progressStatus);

    // Updating the progress bar
    var innerDiv = wrapper.find('.sfProgressBarIn');
    var width = progress > 0 ? progress : 3; // set min width
    innerDiv.attr('style', 'width : ' + width + '%;');

    var doneBtn = wrapper.find(".sfProgressTaskCommand");
    if (progress >= 100)
        doneBtn.show();
    else
        doneBtn.hide();

    actionsEl.hide();
    wrapper.show();
}

function hideProcessingLoader(itemID) {
    var itemEl = jQuery("#" + itemID + " .sfDateAuthor");
    var actionsEl = jQuery("#" + itemID + " .sfMoreActions .actionsMenu");
    
    itemEl.find(".sfLine").removeClass("sfDisplayNoneImportant");
    itemEl.find(".sfProgress").hide();
    actionsEl.show();
}

function startMonitoringReindexProgress() {
    monitorReindexingProgress();
    checkProgress = setInterval(monitorReindexingProgress, 2000);
}

function stopMonitoringReindexProgress() {
    clearInterval(checkProgress);
    checkProgress = null;
}

function monitorReindexingProgress() {
    var serviceUrl = serviceBaseUrl + "/getReindexStatus/";
    clientManager.InvokeGet(serviceUrl, {}, {}, reindexMonitorSuccessCallback);
}

function reindexMonitorSuccessCallback(caller, data) {
    if (!Array.isArray(data))
        return;

    var reindexedPointsToRemove = Object.keys(currentlyReindexedPoints);
    currentlyReindexedPoints = {};
    for (var i = 0; i < data.length; i++) {
        var item = data[i].Key;
        var status = data[i].Value;
        showProcessingLoader(itemToRowMapping[item], status.Progress, status.StatusMessage);
        currentlyReindexedPoints[item] = true;
    }

    for (var i = 0; i < reindexedPointsToRemove.length; i++) {
        var itemToRemove = reindexedPointsToRemove[i];
        if (!currentlyReindexedPoints.hasOwnProperty(itemToRemove)) {
            showProcessingLoader(itemToRowMapping[itemToRemove], 100);
            shouldRebind = true;
        }
    }
}