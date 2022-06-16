﻿function OnMasterViewLoaded(sender, args) {
    $("#AddStorageProviderBtn").click(function () { sender.get_itemsGrid().executeCommand("create", {}); return false; });

    masterGridView = sender;
    sender.add_itemCommand(handleMasterGridViewItemCommand);
    var itemsGrid = sender.get_currentItemsList();
    itemsGrid.add_command(handleItemsGridCommand);
    itemsGrid.add_itemCommand(handleItemsGridItemCommand);

}

function handleItemsGridCommand(sender, args) {
}

function handleItemsGridItemCommand(sender, args) {
}

function handleMasterGridViewItemCommand(sender, args) {
    if (args.get_commandName() == 'setdefault') {
        var itemsGrid = sender;
        var binder = itemsGrid.get_binder();
        var clientManager = binder.get_manager();
        var serviceBaseUrl = binder.get_serviceBaseUrl();
        var serviceUrl = null;
        var urlParams = [];
        var keys = [];
        var index = serviceBaseUrl.indexOf(".svc");
        if (index > -1) {
            serviceUrl = serviceBaseUrl.substring(0, index + 4) + "/setdefault/";
            var selectedKeys = args.get_dataItem().Name;
            clientManager.InvokePut(serviceUrl, urlParams, keys, selectedKeys, successCallback, failureCallback, itemsGrid);
        }


    }

    function successCallback(caller, resultData) {
        // rebind the grid
        caller.get_binder().DataBind();
    }

    function failureCallback(result) {
        alert(result.Detail);
    }

}
