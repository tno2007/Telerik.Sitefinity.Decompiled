﻿// called by the DetailFormView when it is loaded

var historyExtender_widgetCommandDelegate = null;
var historyExtender_onDataBindDelegate = null;
var historyExtender_onBeforeDialogCloseDelegate = null;
var historyExtender_detailFormView = null;
var revertUrl = null;
var sitefinityMediaTypes = {
    image: "Telerik.Sitefinity.Libraries.Model.Image",
    video: "Telerik.Sitefinity.Libraries.Model.Video",
    document: "Telerik.Sitefinity.Libraries.Model.Document"
};
var videoPlayer = null;
var totalSizeControl = null;

function OnDetailViewLoaded(sender, args) {
    // the sender here is DetailFormView
    historyExtender_detailFormView = sender;

    if (historyExtender_widgetCommandDelegate == null) {
        historyExtender_widgetCommandDelegate = Function.createDelegate(this, HandleViewCommands);
        historyExtender_detailFormView.add_command(historyExtender_widgetCommandDelegate);
    }

    if (historyExtender_onDataBindDelegate == null) {
        historyExtender_onDataBindDelegate = Function.createDelegate(this, HandleDataBind);
        historyExtender_detailFormView.add_onDataBind(historyExtender_onDataBindDelegate);
    }

    for (var i = 0; i < historyExtender_detailFormView.get_fieldControlIds().length; i++) {
        var fieldControl = $find(historyExtender_detailFormView.get_fieldControlIds()[i]);
        var fieldControlType = Object.getType(fieldControl);        
        if (fieldControlType === Telerik.Sitefinity.Web.UI.Fields.MediaField
            && fieldControl._libraryContentType === sitefinityMediaTypes.video) {
            videoPlayer = fieldControl.get_mediaPlayer();
        
            if (historyExtender_onBeforeDialogCloseDelegate == null) {
                historyExtender_onBeforeDialogCloseDelegate = Function.createDelegate(this, HandleBeforeDialogClose);
                historyExtender_detailFormView.add_onBeforeDialogClose(historyExtender_onBeforeDialogCloseDelegate);
            }
        } else if (fieldControlType === Telerik.Sitefinity.Web.UI.Fields.TextField
                   && fieldControl._dataFieldName === "TotalSize") {
            totalSizeControl = fieldControl;
        }
    }

    revertUrl = sender._revertUrl;
}

function HandleDataBind(sender, args) {
    var title = sender.get_titleElement();
    var item = args;
    var isMediaType = Object.values(sitefinityMediaTypes).some(function(type) {
        return type === item.ItemType;
    });

    var tempTitle = "";
    if (item.Item.Title || item.Item.Name) {
        if (item.Item.Title)
            tempTitle = typeof (item.Item.Title.Value) === "string" ? item.Item.Title.Value : item.Item.Title;
        else
            tempTitle = typeof (item.Item.Name.Value) === "string" ? item.Item.Name.Value : item.Item.Name;
    }

    tempTitle = tempTitle.replaceAll('"', '&quot;'); // no need for attribute encoding as the element is properly enclosed in double quotation
    var titletodisplay = (tempTitle.length < 21) ? tempTitle : tempTitle.substring(0, 18).concat("...");

    var newtitle = titletodisplay.htmlEncode();
    var localization = sender.get_localization();

    if (item.VersionInfo != null) {
        if (localization.ItemVersionOfClientTemplate != null) {
            newtitle = String.format(localization.ItemVersionOfClientTemplate, item.VersionInfo.Version, tempTitle, titletodisplay);
        }

        if (item.IsPublished) {
            newtitle += " " + localization.PreviouslyPublished;
        }

        var prev_button = $(sender.get_previousButton());
        var next_button = $(sender.get_nextButton());

        if (item.VersionInfo.PreviousId)
            prev_button.removeClass("sfVisibilityHidden");
        else
            prev_button.addClass("sfVisibilityHidden");

        if (item.VersionInfo.NextId)
            next_button.removeClass("sfVisibilityHidden");
        else
            next_button.addClass("sfVisibilityHidden");
        
        $("body").addClass("sfVersionPreview sfItemPreview");

        if (isMediaType) {
            $("body").addClass("sfMediaVersionPreview");
        }
    }

    if (totalSizeControl) {
        var readablizedBytes = readablizeBytes(totalSizeControl.get_value());
        var readablizedBytesArray = readablizedBytes.split(" ")
        totalSizeControl.set_value(readablizedBytesArray[0]);
        totalSizeControl.set_description(readablizedBytesArray[1]);    
    }

    title.innerHTML = newtitle;
}

function HandleViewCommands(sender, args) {
    var commandName = args.get_commandName();
    var view = historyExtender_detailFormView;
    var localization = historyExtender_detailFormView.get_localization();
    var culture = view.get_binder().get_uiCulture();

    switch (commandName) {
        case "publish":
            //alert(commandName + ' ' + view.get_dataItem().Id);
            args.set_cancel(true);
            break;
        case "delete":
            if (view.get_dataItem().VersionInfo.IsLastPublishedVersion) {
                alert(localization.CannotDeleteLastPublishedVersion);
                args.set_cancel(true);
            }

            break;
        case "restoreVersionAsNew":
            var list = historyExtender_detailFormView.get_baseList();
            var item = historyExtender_detailFormView.get_dataItem();
            var callerItem = historyExtender_detailFormView.get_callerDataItem(); // this ensures the item has the needed info for the detail view to load properly
            var key = historyExtender_detailFormView.get_dataKey();
            var params = view.get_callParameters();
            var editCommand = "edit";

            if (params.editCommandName) {
                editCommand = view.get_callParameters().editCommandName;
            }            
            
            if (args.VersionId != undefined) {
                params.VersionId = args.VersionId;
            } else {
                params.VersionId = item.VersionInfo.Id;
            }

            if (list && list.executeCommand) {
                params["backLabelText"] = list.getFormattedBackLabel($(historyExtender_detailFormView.get_titleElement()).text());
                list.executeCommand(editCommand, callerItem, key, null, params);
            } else if (revertUrl) {
                revertUrl += '&sf_version=' + params.VersionId;
                window.top.location.href = revertUrl;
            } else {
                var dialogManager = window.top.GetDialogManager();
                var dialog = dialogManager.getDialogByName(editCommand);
                var context = { commandName: editCommand, dataItem: callerItem, itemsList: null, dialog: dialog, params: params, key: key };
                dialogManager.openDialog(editCommand, this, context);
            }

            args.set_cancel(true);
            break;
        case "navigatePrevious":
            args.set_cancel(true);
            view._itemHistoryVersion = view.get_dataItem().VersionInfo.PreviousId;
            view.rebind(culture);
            break;
        case "navigateNext":
            args.set_cancel(true);
            view._itemHistoryVersion = view.get_dataItem().VersionInfo.NextId;
            view.rebind(culture);
            break;
    }
}

function HandleBeforeDialogClose(sender, args) {
    if (videoPlayer) {
        videoPlayer.stopMedia();
    }
} 

//NOTE: This method is copied from FileField.js - it can be extracted in a common utilities file.
function readablizeBytes(bytes) {
    var result = "";
    if (bytes == "0") {
        result = "0 KB";
    } else {
        var s = ['bytes', 'KB', 'MB', 'GB', 'TB', 'PB'];
        var range = Math.floor(Math.log(bytes) / Math.log(1024));

        //when range is in bytes present in KB and round to nearest integer.
        if (range == 0) {
            range++;
            result = Math.ceil((bytes / Math.pow(1024, Math.floor(range)))) + " " + s[range];
        } else {
            result = (bytes / Math.pow(1024, Math.floor(range))).toFixed(2) + " " + s[range];
        }
    }

    return result;
}