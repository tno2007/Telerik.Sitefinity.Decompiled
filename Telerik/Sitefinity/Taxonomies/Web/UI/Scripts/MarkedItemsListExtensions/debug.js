function MarkedItemsListExtensions_ViewLoaded(masterGridView) {
    var itemsGrid;
    var statList;
    var toolbar;
    var selectedStat = null;
    var taxonId;
    var goFirst = true;
    var itemsRemovedDelegate = null;
    var totalCount = 0;
    var totalCountElem = null;
    var wcfTaxon;

    function Initialize() {        
        var customParams = masterGridView.get_customParameters();

        toolbar = masterGridView.get_toolbar();
        itemsGrid = masterGridView.get_itemsGrid();
        statList = $find(customParams.statsBinderClientId);
        taxonId = customParams.taxonId;
        totalCountElem = $get(customParams.totalCountClientId);
        wcfTaxon = Sys.Serialization.JavaScriptSerializer.deserialize(customParams.wcfTaxon);
        
        itemsGrid.add_command(OnListCommand);
        itemsGrid.getBinder().set_dataKeyNames(["taxonId", "itemType", "itemId"]);
        itemsGrid._dataItemProperty = "Content";
        itemsGrid.add_dialogClosed(OnDialogClosed);

        toolbar.add_command(OnToolbarCommand);

        statList.add_onItemSelectCommand(StatSelectCommand);
        statList.add_onItemDataBound(StatItemDataBound);
        statList.add_onDataBound(StatListDataBound);

        BindStatList();
    }

    function BindStatList() {
        itemsGrid.getBinder().Clear();
        totalCount = 0;
        statList.set_globalDataKeys([]);
        statList.get_globalDataKeys()["taxonId"] = taxonId;
        statList.DataBind();
    }

    function ChangeSelectedStat(stat) {
        selectedStat = stat;
        goFirst = false;
        itemsGrid.getBinder().set_globalDataKeys([]);
        //itemsGrid.getBinder().get_globalDataKeys()["taxonId"] = taxonId;
        itemsGrid.getBinder().get_urlParams()["itemType"] = selectedStat.ItemType;
        itemsGrid.getBinder().get_urlParams()["itemProvider"] = selectedStat.ItemProvider;
        itemsGrid.dataBind();
    }

    function StatSelectCommand(sender, args) {
        var dataItem = args.get_dataItem();
        SelectStatItem(dataItem, sender.get_target(), args.get_itemElement())
    }

    function StatItemDataBound(sender, args) {
        var dataItem = args.get_dataItem();
        if ((goFirst && args.get_itemIndex() == 0) || (selectedStat.ItemType == dataItem.ItemType)) {
            SelectStatItem(dataItem, sender.get_target(), args.get_itemElement())
        }
        totalCount = totalCount + dataItem.Count;
    }


    function StatListDataBound(sender, args) {
        $(totalCountElem).text(totalCount);
    }

    function OnListCommand(sender, args) {
        var commandName = args._commandName;
        if (commandName == "groupRemove") {
            args.set_cancel(true);
            UnmarkSelectedItems(sender.getBinder());
        }
        else if (commandName == "edit") {
            args.set_cancel(true);
        }
    }

    function OnToolbarCommand(sender, args) {
        var commandName = args.get_commandName();
        if (commandName == "edit") {
            args.set_cancel(true);
            var key = { "Id": taxonId };
            itemsGrid.openDialog(commandName, wcfTaxon, itemsGrid._getDialogParameters(commandName), key, { language: itemsGrid._uiCulture });           
        }
    }

    function OnDialogClosed(sender, args) {
        args.set_cancel(true);
        if (args.get_isUpdated()) {
            var refreshUrl = window.location.href;
            window.location.href = refreshUrl;
        }
    }

    function UnmarkSelectedItems(binder) {
        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {
            var ids = "";
            var itemsIter = selectedItems.length;
            goFirst = (selectedStat.Count == itemsIter)
            while (itemsIter--) {
                ids += selectedItems[itemsIter]["Id"] + ",";
            }

            var url = binder.get_serviceBaseUrl() + "?itemType=" + selectedStat.ItemType + "&itemProvider=" + selectedStat.ItemProvider;
            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            clientManager._invokeWebService(url, "DELETE", ids, binder, Function.createDelegate(this, OnItemsRemoved), null, null, this);
        }
    }

    function OnItemsRemoved(sender, args) {
        BindStatList();
    }

    function SelectStatItem(dataItem, container, itemElement) {
        ChangeSelectedStat(dataItem);
        $(container).find('.sfSel').each(function () {
            $(this).removeClass('sfSel');
        });
        $(itemElement).addClass('sfSel');
    }

    Initialize();
}