var baseUrl;

// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    var itemsGrid = sender.get_itemsGrid();
    var binder = sender.get_binder();

    sender.add_itemCommand(masterGridViewItemCommand);
    itemsGrid.add_command(itemsGridCommand);
    binder.add_onItemDataBound(binderItemDataBound);

    baseUrl = sender.get_baseUrl();
    binder.set_unescapeHtml(true);
}

function itemsGridCommand(sender, args) {
    switch (args.get_commandName()) {
        case "ShowAllComments":
            sender.applyFilter('');
            break;
        case "ShowTodayComments":
            var date = new Date();
            var month = date.getMonth() + 1;
            var filterExpression = 'DateCreated >= (' + date.getFullYear() + '-' + month + '-' + date.getDate() + ' 00:00:00)';
            filterExpression += ' AND DateCreated <= (' + date.getFullYear() + '-' + month + '-' + date.getDate() + ' 23:59:59)';
            sender.applyFilter(filterExpression);
            break;
        case "ShowHiddenComments":
            sender.applyFilter("CommentStatus == Hidden");
            break;
        case "ShowPublishedComments":
            sender.applyFilter("CommentStatus == Published");
            break;
        case "ShowSpamComments":
            sender.applyFilter("CommentStatus == Spam");
            break;
        case "GroupHideComments":
            var selectedItems = sender.getBinder().get_selectedItems();
            if (selectedItems && selectedItems.length > 0) {
                var multikey = sender._getKeyFromItems(selectedItems);
                changeCommentStatus(baseUrl, selectedItems, multikey, sender.getBinder(), sender, 'hide');
            }
            break;
        case "GroupSpamComments":
            var selectedItems = sender.getBinder().get_selectedItems();
            if (selectedItems && selectedItems.length > 0) {
                var multikey = sender._getKeyFromItems(selectedItems);
                changeCommentStatus(baseUrl, selectedItems, multikey, sender.getBinder(), sender, 'spam');
            }
            break;
        case "GroupPublishComments":
            var selectedItems = sender.getBinder().get_selectedItems();
            if (selectedItems && selectedItems.length > 0) {
                var multikey = sender._getKeyFromItems(selectedItems);
                changeCommentStatus(baseUrl, selectedItems, multikey, sender.getBinder(), sender, 'publish');
            }
            break;
        case "view":
            var url = window.location.href;

            var index = url.toLowerCase().indexOf("comments");
            var moduleUrl = "";
            if (index > -1) {
                moduleUrl = url.substring(0, index - 1);
            }
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
            var returnUrl = "";
            var provider = "";
            var appender = "";

            if (queryString.contains("ReturnUrl")) {
                returnUrl = queryString.get("ReturnUrl");
            }

            var link = moduleUrl + returnUrl;
            if (queryString.contains("provider") && returnUrl.indexOf('provider') == -1) {
                appender = returnUrl[returnUrl.length - 1] !== "/" ? "/" : "";
                appender += link.indexOf("?") === -1 ? "?" : "&";
                provider = appender + "provider=" + queryString.get("provider");
            }

            window.location.href = link + provider;
            break;            
    }
}

function binderItemDataBound(sender, args) {
    var dataItem = args.get_dataItem();
    var element = args.get_itemElement();
    if (dataItem && dataItem.IsAuthorComment) {
        jQuery(element).addClass("sfcommentOfTheAuthor");
    }

    var editElement = _getCommandElement(element, "editComment");
    var hideElement = _getCommandElement(element, "hideComment");
    var spamElement = _getCommandElement(element, "spamComment");
    var deleteElement = _getCommandElement(element, "delete");

    if ((editElement != null) && (!dataItem.IsEditable))
        editElement.style.display = "none";
    if ((hideElement != null) && (!dataItem.IsEditable))
        hideElement.style.display = "none";
    if ((spamElement != null) && (!dataItem.IsEditable))
        spamElement.style.display = "none";
    if((deleteElement != null) && (!dataItem.IsDeletable))
        deleteElement.style.display = "none";

}

function _getCommandElement(binderItemElement, command)
{
    var commandElements = $(binderItemElement).find('[class="sf_binderCommand_' + command + '"]');
    if(commandElements.length > 0)
    {
        return commandElements.get(0);
    }
    else
        return null;
}

function changeCommentStatus(baseUrl, selectedItems, multikey, binder, itemsGrid, newStatus) {
    var serviceUrl = baseUrl + "Sitefinity/Services/Common/Comments.svc/" + newStatus;
    var urlParams = [];
    urlParams['provider'] = binder.get_provider();
    urlParams['managerType'] = itemsGrid.get_managerType();
    for (var i = selectedItems.length-1; i >= 0; i--) {
        if (!selectedItems[i].IsEditable) {
            for (j = multikey.length - 1; j >= 0; j--) {
                if (multikey[j].Id == selectedItems[i].Id) {
                    multikey.splice(j, 1);
                }
            }
            selectedItems.splice(i, 1);
        }
    }
    var keysCount = multikey.length;
    while (keysCount--) {
        if(selectedItems[keysCount].IsEditable)
            binder.get_manager().InvokePut(serviceUrl, urlParams, multikey[keysCount], selectedItems[keysCount], SuccessCallback, FailureCallback, itemsGrid);
    }
}

function masterGridViewItemCommand(sender, args) {
    var commandName = args.get_commandName();
    var dataItem = args.get_dataItem();
    var binder = sender.get_binder();
    var serviceUrl = sender.get_baseUrl() + "Sitefinity/Services/Common/Comments.svc/";
    var urlParams = [];
    var keys = [];
    switch (commandName) {
        case "editComment":
            break;
        case "hideComment":
            serviceUrl += "hide/";
            clientManager = binder.get_manager();
            urlParams['provider'] = binder.get_provider();
            urlParams['managerType'] = sender._itemsGrid.get_managerType();
            keys.push(dataItem.Id);
            clientManager.InvokePut(serviceUrl, urlParams, keys, dataItem, SuccessCallback, FailureCallback, sender.get_itemsGrid());
            break;
        case "spamComment":
            serviceUrl += "spam/";
            clientManager = binder.get_manager();
            urlParams['provider'] = binder.get_provider();
            urlParams['managerType'] = sender._itemsGrid.get_managerType();
            keys.push(dataItem.Id);
            clientManager.InvokePut(serviceUrl, urlParams, keys, dataItem, SuccessCallback, FailureCallback, sender.get_itemsGrid());
            break;
        case "editItem":
            // Handle the case for blog posts with ReturnUrl
            var url = new Sys.Uri(location.href);
            var returnUrl = url.get_query()["ReturnUrl"];

            var queryStringParams = "?command=edit&provider=" + dataItem.ProviderName + "&contentId=" + dataItem.CommentedItemID + "&backLabelText=" + unescape(sender.get_clientLabelManager().getLabel("Labels", "BackToComments"));
            var redirectUrl;
            if (returnUrl) {
                redirectUrl = location.href.substr(0, location.href.lastIndexOf("/Comments/")) + returnUrl + queryStringParams;
            }
            else {
                redirectUrl = location.href.substr(0, location.href.lastIndexOf("/")) + "/" + queryStringParams;
            }
            location.href = redirectUrl;
            break;
        case "blockIp":
            alert("In process of implementation.");
            break;
        case "blockEmail":
            alert("In process of implementation.");
            break;
        case "favoriteComment":
            alert("In process of implementation.");
            break;
    }
    
}

function SuccessCallback(caller, result) {
    caller.dataBind();
}

function FailureCallback(result) {
    alert(result.Detail);
}