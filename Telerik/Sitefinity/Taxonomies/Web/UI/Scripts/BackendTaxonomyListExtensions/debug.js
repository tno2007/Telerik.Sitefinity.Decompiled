﻿function BackendTaxonomyListExtensions_ViewLoaded(masterGridView) {
    var itemsGrid = masterGridView.get_itemsGrid();
    var binder = itemsGrid.getBinder();
    var sidebar = masterGridView.get_sidebar();
    var actionCommandPrefix = "sf_binderCommand_";
    var taxonomyNotUsedFilter = "[ShowNotUsedTaxonomies]";

    itemsGrid.add_dialogClosed(dialogClosedCallback);

    function SetTranslations(item, list, key, element) {
        var availableLangs = item.AvailableLanguages;
        if (availableLangs && availableLangs.length > 0) {
            list.setTranslations(item, key, availableLangs, element);
        }
    }

    function FilterTaxonomies(widgetBar, filterLink, taxonomyType) {
        binder.get_urlParams()['skipSiteContext'] = false;
        binder.get_urlParams()['taxonomyType'] = taxonomyType;

        itemsGrid.applyFilter('');

        jQuery(widgetBar.get_element()).find(".sfSel").removeClass('sfSel');
        jQuery(filterLink.get_element()).find("a").addClass("sfSel");
    }

    function dialogClosedCallback(sender, args) {
        if (args.get_context) {
            var context = args.get_context();

            if (args.get_isCreated && args.get_isCreated()
            && context && context.get_createFormCommandName
            && context.get_createFormCommandName() == "create") {
                navigateToLists(sender, args);
            }
            else {
                var grid = sender;
                $("body").removeClass("sfEmpty");
                grid.show();
            }
        }

        binder.DataBind();
    }

    function navigateToLists(sender, args) {
        var grid = sender;
        var dataItem = args.get_dataItem();

        $("body").addClass("sfEmpty");
        grid.hide();

        if (dataItem.EditUrl) {
            var parametersDelimeter = '?';

            if (dataItem.EditUrl.indexOf('?') > -1) {
                parametersDelimeter = '&';
            }

            window.location = dataItem.EditUrl + parametersDelimeter + 'status=created';
        }
    }

    var onItemsGridDataBound = function (sender, args) {
        var clientLabelManager = masterGridView.get_clientLabelManager();
        var dataItem = args.get_dataItem();
        var sharedWith = args.FindControl('showSites');

        if (sharedWith != null) {
            switch (dataItem.SharedSitesCount) {

                case 0:  // set 'shared with' text to 'not used' when the taxonomy is not used in any site
                    sharedWith.innerHTML = String.format("<span>{0}</span>", clientLabelManager.getLabel('TaxonomyResources', 'NotUsed'));
                    break;
                case 1:  // set 'shared with' text to 'this site only' when the taxonomy is used in only this site
                    sharedWith.innerHTML = String.format("<span>{0}</span>", clientLabelManager.getLabel('TaxonomyResources', 'ThisSiteOnly'));
                    break;
                default:
                    break;
            }
        }

        var itemElement = args.get_itemElement();

        _configureMoreActionsMenu(itemElement);

        var containsList = args.FindControl('containsList');
        var firstTwoTaxonsCount = dataItem.FirstTwoTaxons.length;
        if (firstTwoTaxonsCount > 0) {
            // add the first two taxons to the list
            for (var i = 0; i < firstTwoTaxonsCount; i++) {
                var taxonItem = document.createElement('li');
                taxonItem.className = "sfCatItem";
                taxonItem.innerText = dataItem.FirstTwoTaxons[i];
                containsList.appendChild(taxonItem);
            }
            // add the number of other items in the list
            var remainingItemsCount = dataItem.TotalTaxaCount - firstTwoTaxonsCount;
            if (remainingItemsCount > 0) {
                var moreItemsLabel = (remainingItemsCount == 1) ? clientLabelManager.getLabel('Labels', 'MoreItem') : clientLabelManager.getLabel('Labels', 'MoreItems');
                var remainingItem = document.createElement('li');
                remainingItem.innerHTML = remainingItemsCount + moreItemsLabel;
                containsList.appendChild(remainingItem);
            }
        } else {
            // taxonomy is empty
            var emptyItem = document.createElement('li');
            emptyItem.innerHTML = clientLabelManager.getLabel('Labels', 'NoItemsEmpty');
            containsList.appendChild(emptyItem);
        }

        if (dataItem.IsBuiltIn) {
            var actionsMenu = jQuery(args.get_itemElement()).find("ul.actionsMenu");
            actionsMenu.find(".sf_binderCommand_delete").parent().addClass("sfDisplayNone");
            actionsMenu.find(".sf_binderCommand_edit").parent().addClass("sfDisplayNone");
            actionsMenu.find(".sfSeparator").removeClass("sfSeparator").addClass("sfActionsSectionTitle");
        }

        SetTranslations(dataItem, itemsGrid, args.get_key(), args.get_itemElement());
    }

    var onWidgetBarCommand = function (sender, args) {
        switch (args.get_commandName()) {
            case "showFlatTaxonomies":
                FilterTaxonomies(sender, sender.getWidgetByCommandName("showFlatTaxonomies"), "FlatTaxonomy");
                break;
            case "showHierarchicalTaxonomies":
                FilterTaxonomies(sender, sender.getWidgetByCommandName("showHierarchicalTaxonomies"), "HierarchicalTaxonomy");
                break;
            case "showAllItems":
                FilterTaxonomies(sender, sender.getWidgetByCommandName("showAllItems"), null);
                break;
            case "showNotUsedTaxonomies":
                binder.get_urlParams()['taxonomyType'] = null;
                binder.get_urlParams()['skipSiteContext'] = true;
                itemsGrid.applyFilter(taxonomyNotUsedFilter);
                itemsGrid.dataBind();

                jQuery(sender.get_element()).find(".sfSel").removeClass('sfSel');
                jQuery(sender.getWidgetByCommandName("showNotUsedTaxonomies").get_element()).find("a").addClass("sfSel");
                break;
            default:
                break;
        }
    }

    var itemCommandHandler = function (sender, args) {
        var taxonomy = args.get_commandArgument();
        var taxonomyId = args.get_commandArgument().Id;
        var editUrl = null;
        var dialog = null;
        var url = null;

        switch (args.get_commandName()) {
            case 'showSharedSites': {
                dialog = sender.getRadWindowManager().GetWindowByName("showSharedSites");

                if (dialog) {
                    url = dialog.get_navigateUrl();
                    var idx = url.indexOf("?");
                    if (idx > -1) {
                        url = url.substring(0, idx);
                    }
                    dialog.set_navigateUrl(url + "?id={{Id}}");
                }
                break;
            }

            case 'delete':
                // Delete taxa only if grid is filtered by not used taxonomies
                binder.get_urlParams()['deleteTaxaOnly'] = (itemsGrid.get_filterExpression() == taxonomyNotUsedFilter);
                break;
        }
    }

    var _configureMoreActionsMenu = function (containerElement) {
        var itemsList = masterGridView.get_currentItemsList();

        if (itemsGrid.get_filterExpression() == taxonomyNotUsedFilter) {
            // Remove 'Set classification for this site' menu item if the grid is filtered by not used taxonomies
            // 'Use classification in' menu item remains visible
            itemsList.removeActionsMenuItems([actionCommandPrefix + "setTaxonomy"], containerElement);
        }
        else {
            // In all other cases the 'Use classification in' menu item must be removed
            itemsList.removeActionsMenuItems([actionCommandPrefix + "useClassificationIn"], containerElement);
        }
    }

    binder.add_onItemDataBound(onItemsGridDataBound);
    sidebar.add_command(onWidgetBarCommand);
    itemsGrid.add_itemCommand(itemCommandHandler);
}