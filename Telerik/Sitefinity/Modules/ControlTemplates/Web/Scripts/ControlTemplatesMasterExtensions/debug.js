// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    // the sender here is MasterGridView
    var itemsGrid = sender.get_itemsGrid();
    itemsGrid.add_command(handleItemsGridCommand);

    var dialogClosedHandler = function (sender, args) {
        if (!Telerik.Sitefinity.DialogClosedEventArgs.isInstanceOfType(args)) {
            var binder = itemsGrid.getBinder();
            var windowName = sender.get_name();

            switch (windowName) {
                case "shareWith":
                    if (args.IsUpdated) {
                        binder.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
    };

    itemsGrid.add_dialogClosed(dialogClosedHandler);

    if (sender.get_sidebar().getWidgetByName("ThisSiteTemplates")) {
        jQuery(sender.get_sidebar().getWidgetByName("AllTemplates").get_element()).find("a").removeClass("sfSel");
    }
}

function handleItemsGridCommand(sender, args) {
    var binder = sender.getBinder();
    switch (args.get_commandName()) {
        case 'showAllTemplates':
            binder.get_urlParams()['templateFilter'] = 'AllTemplates';
            if (binder.get_isFiltering()) {
                binder.set_filterExpression('');
            }
            else {
                binder.set_isFiltering(true);
            }
            binder.DataBind();
            break;
        case 'showThisSiteTemplates':
            binder.get_urlParams()['templateFilter'] = 'ThisSiteTemplates';
            binder.set_isFiltering(true);
            binder.DataBind();
            break;
        case 'showNotSharedTemplates':
            binder.get_urlParams()['templateFilter'] = 'NotSharedTemplates';
            binder.set_isFiltering(true);
            binder.DataBind();
            break;
        case 'showMyTemplates':
            binder.get_urlParams()['templateFilter'] = 'MyTemplates';
            binder.set_isFiltering(true);
            binder.DataBind();
            break;
        default:
            break;
    }
}
