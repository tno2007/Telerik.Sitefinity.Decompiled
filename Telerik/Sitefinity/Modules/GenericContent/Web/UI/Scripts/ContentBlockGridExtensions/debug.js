// called by the MasterGridView when it is loaded
var masterView;
function OnMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    masterView = sender;
    var sidebar = masterView.get_sidebar();
    sidebar.add_command(handleCustomContentBlockCommand);

    var itemsGrid = sender.get_itemsGrid();
    itemsGrid.add_itemCommand(handleItemCommand);
    addItemsGridDialogClosedHandling(itemsGrid);

    var binder = sender.get_binder();
    binder.add_onItemDataBound(handleItemDataBound);
    binder.set_unescapeHtml(true);
}

function handleCustomContentBlockCommand(sender, args) {
    switch (args.get_commandName()) {
        case "showNotUsedItems":
            var binder = masterView.get_binder();

            binder.set_isFiltering(true);
            var filter = "#ContentBlocksNotUsedOnAnyPage#";
            binder.Filter(filter);
            break;
        default:
            break;
    }
}

function unselectToolbarItems(masterView, names) {
    var sidebar = masterView.get_sidebar();
    for (var i = 0; i < names.length; i++) {
        var linkElem = sidebar.getWidgetByName(names[i]).get_linkElement();
        jQuery(linkElem).removeClass('sfSel');
    }
}

// handles the commands fired by a single item
function handleItemCommand(sender, args) {
    if (args != null) {
        switch (args.get_commandName()) {
            case "viewContentPages":
                var dataItem = args.get_commandArgument();
                showContentPagesDialog(sender, dataItem);
                break;
            default:
                break;
        }
    }
}

// handles the data bound fired by a single item
function handleItemDataBound(sender, args) {
    var dataItem = args.get_dataItem();
    if (dataItem) {
        if (dataItem["PagesCount"] == 0 && dataItem["PageTemplatesCount"] == 0) {
            var row = args.get_itemElement();
            if (row) {
                jQuery(row).find(".sf_binderCommand_viewContentPages").removeClass("sf_binderCommand_viewContentPages");
            }
        }
    }
}

function addItemsGridDialogClosedHandling(itemsGrid) {
    var currentDataItem;
    var currentLanguage;

    //handle showed to get the currentDataItem that is needed to show ContentPagesDialog in the closed event.
    var handleDialogShowed = function (sender, args) {
        var dataItem = args.get_dataItem();
        var command = args.get_commandName();
        if (dataItem && command == "edit") {
            currentDataItem = dataItem;
            if (args.get_commandArgument() && args.get_commandArgument().language) {
                currentLanguage = args.get_commandArgument().language;
            }
            else {
                currentLanguage = null;
            }
        }
    };
    itemsGrid.add_dialogShowed(handleDialogShowed);

    var handleDialogClosed = function (sender, args) {
        if (args._isUpdated == true && currentDataItem && (currentDataItem.PagesCount > 0 || currentDataItem.PageTemplatesCount > 0)) {
            if (!$telerik.isChrome) {
                showContentPagesDialog(sender, currentDataItem, true, currentLanguage);
            }
            else {
                window.setTimeout(function () { showContentPagesDialog(sender, currentDataItem, true, currentLanguage) }, 0);
            }
        }
    };
    itemsGrid.add_dialogClosed(handleDialogClosed);
}

function showContentPagesDialog(sender, dataItem, isSuccessfullyUpdatedDialog, language) {
    var dialog = sender.getRadWindowManager().GetWindowByName("viewContentPages");
    if (dialog) {
        // black list ContentPagesDialog
        var dialogManager = window.top.GetDialogManager();
        if (dialogManager) {
            dialogManager.blacklistWindow(dialog);
        }

        var url = dialog.get_navigateUrl();
        var idx = url.indexOf("?");
        if (idx > -1) {
            url = url.substring(0, idx);
        }
        var id = null;
        var providerName = null;
        if (dataItem) {
            id = dataItem.LiveContentId;
            providerName = dataItem.ProviderName;
        }
        var uiCulture;
        if (language) {
            uiCulture = language;
        }
        else {
            uiCulture = masterView.get_binder().get_uiCulture();
        }
        var query = String.format("?contentId={0}&contentProviderName={1}&uiCulture={2}", id, providerName, uiCulture);
        if (isSuccessfullyUpdatedDialog) {
            query += "&isSuccessfullyUpdatedDialog=true";
        }
        dialog.set_navigateUrl(url + query);
        dialog.show();
        Telerik.Sitefinity.centerWindowHorizontally(dialog);
    }
}
