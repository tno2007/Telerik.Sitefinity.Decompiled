
var masterGridView;
function OnMasterViewLoaded(sender, args) {
    masterGridView = sender;
    var itemsGrid = sender.get_currentItemsList();
    $("#addThumbnailProfile").click(function () { itemsGrid.executeCommand("create", {}); return false; });
       
    blacklistDialogs();
    sender.add_itemCommand(handleMasterGridViewItemCommand);
    
    itemsGrid.add_itemCommand(handleItemsGridItemCommand);
    //
    var dialog = GetRadWindowManager().GetWindowByName("ThumbnailProfileDialog");
    if (dialog) {
        dialog.add_close(handleThumbnailProfileClosing);
    }
}

function handleThumbnailProfileClosing(sender, args) {
    if (args.get_argument() == 'submit') {
        
    }
}

function blacklistDialogs() {
    var dialogs = GetRadWindowManager().GetWindows();
    var dialogManager = window.top.GetDialogManager();
    for (var i = 0; i < dialogs.length; i++) {
        var currentDialog = dialogs[i];
        dialogManager.blacklistWindow(currentDialog);
    }
}

function handleItemsGridItemCommand(sender, args) {
    if (args.get_commandName() == "delete") {
        var libCount = args.get_commandArgument().LibrariesCount;
        if (libCount > 0) {
            sender._binder.get_urlParams().libraryType = "Telerik.Sitefinity.Libraries.Model.Album";
            var dialog = sender.getPromptDialogByName("deleteProfileForbiddenDialog");
            var message = dialog.get_initialMessage();
            message = String.format(message, libCount);
            dialog.show_prompt(null, message);
            args.set_cancel(true);
        }
    }
}

function handleMasterGridViewItemCommand(sender, args) {
    
}