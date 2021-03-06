// called by the MasterGridView when it is loaded
var masterView;
function OnMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    masterView = sender;

    var itemsGrid = sender.get_itemsGrid();
    itemsGrid.add_itemCommand(handleItemCommand);
    addItemsGridDialogClosedHandling(itemsGrid);

    var binder = sender.get_binder();
    binder.add_onItemDataBound(handleItemDataBound);
    binder.set_unescapeHtml(true);

    jQuery("#sfToMainContent").after(masterView.get_clientLabelManager().getLabel("ResponsiveDesignResources", "ResponsiveAndMobileDesignCompatibilityWarning"));
}

// handles the commands fired by a single item
function handleItemCommand(sender, args) {
    if (args != null) {
        switch (args.get_commandName()) {
            case "PagesAndTemplatesDialog":
                var dataItem = args.get_commandArgument();
                if (dataItem["PagesCount"] > 0 || dataItem["PageTemplatesCount"] > 0) {
                    showContentPagesDialog(sender, dataItem);
                }
                break;
            case "delete":
                var dataItem = args.get_commandArgument();
                if (dataItem["PagesCount"] > 0 || dataItem["PageTemplatesCount"] > 0) {
                    args.set_cancel(true);
                    var dialog = masterGridView.get_itemsGrid().getPromptDialogByName("singleRuleInUseDialog");
                    var message = dialog.get_initialMessage();
                    message = String.format(message, '<strong>' + dataItem["PagesCount"] + '</strong>', '<strong>' + dataItem["PageTemplatesCount"] + '</strong>');
                    dialog.show_prompt(null, message);
                }
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
        var row = args.get_itemElement();
        if (row && dataItem["PagesCount"] == 0 && dataItem["PageTemplatesCount"] == 0) {
            jQuery(row).find(".sf_binderCommand_PagesAndTemplatesDialog").removeClass("sf_binderCommand_PagesAndTemplatesDialog");
        } else {
            jQuery(row).find(".sf_binderCommand_PagesAndTemplatesDialog").addClass("sfLnk");
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
    var dialog = sender.getRadWindowManager().GetWindowByName("PagesAndTemplatesDialog");
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
            id = dataItem.Id;
            providerName = dataItem.ProviderName;
        }
        var uiCulture;
        if (language) {
            uiCulture = language;
        }
        else {
            uiCulture = masterView.get_binder().get_uiCulture();
        }
        var query = String.format("?mediaQueryId={0}&responsiveDesignProviderName={1}&uiCulture={2}", id, providerName, uiCulture);
        if (isSuccessfullyUpdatedDialog) {
            query += "&isSuccessfullyUpdatedDialog=true";
        }
        dialog.set_navigateUrl(url + query);
        dialog.show();
        Telerik.Sitefinity.centerWindowHorizontally(dialog);
    }
}
