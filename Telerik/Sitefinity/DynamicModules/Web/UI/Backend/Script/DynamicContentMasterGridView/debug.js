Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Web.UI.Backend");
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentMasterGridView = function (element) {
    Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentMasterGridView.initializeBase(this, [element]);

    this._mainShortTextFieldName = null;
    this._restrictedItemCommands = null;
}

Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentMasterGridView.prototype = {

    _interceptListItemCommandAndLaunchAlternative: function (sender, args) {

        var originalCommandName = args.get_commandName();
        var dataItem = args.get_commandArgument();

        if (!dataItem) {
            return;
        }

        if ((originalCommandName == "edit") ||
            ((this._clientMappedCommnadNames != null) && (this._clientMappedCommnadNames.hasOwnProperty("edit")) &&
             (this._clientMappedCommnadNames["edit"] == originalCommandName))) {

            if (!this._isEditable(dataItem)) {
                args.set_cancel(true);

                this._nonEditableItemToView = dataItem;
                this._nonEditableItemKey = [];
                this._nonEditableItemKey[sender.get_keys()[0]] = dataItem[sender.get_keys()[0]];
                this._nonEditableItemElement = sender.get_element();

                var oPDialog = $find(this._promptWindowId);
                var itemTitle = dataItem.Title;
                if ((typeof (dataItem.Title.Value) != typeof (undefined)) && (dataItem.Title.Value != null) && (dataItem.Title.Value != "")) {
                    itemTitle = dataItem.Title.Value;
                }
                oPDialog.set_title(String.format(this._noEditPermissionsViewDialogTitle, itemTitle));

                var previewCommand = "viewProperties";
                var dialogText = this._noEditPermissionsPreviewOnlyConfirmationNoViewOption;
                if ((this._clientMappedCommnadNames != null) && (this._clientMappedCommnadNames.hasOwnProperty("preview"))) {
                    previewCommand = this._clientMappedCommnadNames["preview"];
                }
                var previewWin = window.GetRadWindowManager().getWindowByName(previewCommand);

                if (previewWin != null && this._isViewable(dataItem)) {

                    dialogText = this._noEditPermissionsConfirmationMessage;
                    oPDialog.setButtonDisplay("view", true);
                }
                else {
                    oPDialog.setButtonDisplay("view", false);
                }
                oPDialog.set_textFieldTitle(dialogText);
                oPDialog.show_prompt(null, dialogText, Function.createDelegate(this, this._handleUnEditableObject));
            }
            else {

                if (dataItem.Lifecycle && this._isLockedBySomebodyElse(dataItem)) {
                    args.set_cancel(true);
                    this._lockedItemToView = dataItem;
                    this._lockedItemKey = [];
                    this._lockedItemKey["Id"] = dataItem.Id;

                    var oLDialog = $find(this._lockWindowId);

                    oLDialog.setButtonDisplay("unlock", dataItem.IsUnlockable);

                    var editCommand = "edit";
                    if ((this._clientMappedCommnadNames != null) && (this._clientMappedCommnadNames.hasOwnProperty("edit"))) {
                        editCommand = this._clientMappedCommnadNames["edit"];
                    }

                    var mainShortTextFieldValue = dataItem[this._mainShortTextFieldName];
                    var dataItemTitle = mainShortTextFieldValue.hasOwnProperty("PersistedValue") ? mainShortTextFieldValue.PersistedValue : mainShortTextFieldValue;
                    oLDialog.set_textFieldTitle(String.format(this._hasBeenLockedForEditingBySince, dataItemTitle, dataItem.Lifecycle.LockedByUsername, dataItem.Lifecycle.LockedSince.sitefinityLocaleFormat("f")));
                    //oLDialog.setButtonDisplay("view", true);

                    oLDialog.show_prompt(null, null, Function.createDelegate(this, this._handleLockedObject));
                }
            }
        }
    },

    _isLockedBySomebodyElse: function (item) {

        if (item.Lifecycle && item.Lifecycle.IsLocked == true && item.Lifecycle.IsLockedByMe == false) {
            return true;
        }
        else {
            return false;
        }
    },

    _handleBinderItemDataBound: function (sender, args) {
        var commandLinks = $(args.get_itemElement()).find("[class*='sf_binderCommand']");
        var item = args.get_dataItem();
        var itemIsEditable = item.IsEditable;
        var isPublishable = false;
        var isUnpublishable = false;
        var list = this.get_currentItemsList();

        if (item.WorkflowOperations) {
            for (var curOp = 0; curOp < item.WorkflowOperations.length; curOp++) {
                if (item.WorkflowOperations[curOp].OperationName == "Publish") {
                    isPublishable = true;
                }
                if (item.WorkflowOperations[curOp].OperationName == "Unpublish") {
                    isUnpublishable = true;
                }
            }
        } else {
            isPublishable = true;
            isUnpublishable = true;
        }

        //fix for 69684: hide "create post" command links if the item is not editable
        for (var curCmd = 0; curCmd < commandLinks.length; curCmd++) {
            var cmdNameMatch = commandLinks[curCmd].className.match(/sf_binderCommand_(\S*)/i);

            if (cmdNameMatch && cmdNameMatch[1]) {
                var commandName = cmdNameMatch[1];
                var hideItemCommand = this._hideRestrictedItemsAnchors(commandName);

                if ((!itemIsEditable) && (commandName === "createPost")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!isPublishable) && (commandName === "publish")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!isPublishable) && (commandName === "publishDraft")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!isUnpublishable) && (commandName === "unpublish")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!itemIsEditable) && (commandName === "setAsHomepage")) {
                    commandLinks[curCmd].style.display = "none";
                }
                
                if (!hideItemCommand) {
                    if (cmdNameMatch[0] === "sf_binderCommand_viewChildItems") {
                        commandLinks[curCmd].className += " sfDisabled";
                    }
                    else {
                        commandLinks[curCmd].parentNode.style.display = "none";
                    }
                }
            }
        }
        if (item.Lifecycle) {
            var isPublished = item.Lifecycle.IsPublished;
            var commands;
            if (isPublished)
                commands = ["sf_binderCommand_publish"];
            else
                commands = ["sf_binderCommand_unpublish"];
            list.removeActionsMenuItems(commands, args.get_itemElement());
        }

        //handle additional links (edit, view child items)
        this.setAdditionalLinksTranslations(item, args.get_key(), item.AvailableLanguages, args.get_itemElement());

        this.setTranslations(item, list, args.get_key(), args.get_itemElement());
    },

    setAdditionalLinksTranslations: function (dataItem, key, availableLangs, containerElement, handlers) {
        if (this.get_itemsGrid()._supportsMultilingual && availableLangs && availableLangs.length > 0) {
            var uiCulture = this.get_itemsGrid().get_uiCulture(),
                hasTranslation = jQuery.inArray(uiCulture, availableLangs) > -1,
                jcntElem = $(containerElement),
                editLink = jcntElem.find(".sfTitleCol").find(".sf_binderCommand_edit"),
                childrenList = jcntElem.find(".sfTitleCol").find(".sfChildrenList");

            childrenList.toggleClass("sfVisibilityHidden", !hasTranslation);
            editLink.parent().toggleClass("sfVisibilityHidden", !hasTranslation && childrenList.length > 0);
        }
    },

    _hideRestrictedItemsAnchors: function (commandName) {
        var restrictedItemCommands = this.get_restrictedItemCommands();

        if (restrictedItemCommands.indexOf(commandName) > -1) {
            return false;
        }

        return true;
    },

    /* -------------------- properties -------------------- */
    get_mainShortTextFieldName: function () {
        return this._mainShortTextFieldName;
    },

    set_mainShortTextFieldName: function (value) {
        this._mainShortTextFieldName = value;
    },

    get_restrictedItemCommands: function () {
        return this._restrictedItemCommands;
    },

    set_restrictedItemCommands: function (value) {
        this._restrictedItemCommands = value;
    }
}

Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentMasterGridView.registerClass('Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentMasterGridView', Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView);