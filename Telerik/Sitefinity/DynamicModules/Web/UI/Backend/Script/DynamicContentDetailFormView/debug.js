Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Web.UI.Backend");
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView = function (element) {
    Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView.initializeBase(this, [element]);
}
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView.prototype =
{
    _prepareCommandArgument: function () {
        var lang, cmdArg;
        if (this._isMultilingual) {
            lang = this._binder.get_uiCulture();
            cmdArg = { language: lang };
            this._commandArgument.language = lang;
        }
        this._dialogContext = this._commandArgument;
        return cmdArg;
    },
    _hideDynamicContentInstructionalText: function (commandName) {
        if (commandName == 'preview') {
            // hide instructional text in preview dialog
            $('.sfDescription').hide();
        }
        else if (commandName == 'showTranslationWrapper') {
            // hide instructional text in compare translations dialog
            $('.sfTranslateFromWrp').find('.sfDescription').hide();
        }
    },
    _fieldCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        var commandArgument = args.get_commandArgument();

        switch (commandName) {
            case this._historyCommandName:
                if (!this._verifyChangesOnExit()) break;
                var controlParams = new Object;
                controlParams["backLabelText"] = this._backLabelText;
                controlParams["parentId"] = this._callParams.parentId;
                var cmdArg = this._prepareCommandArgument();
                this._openDialog("historygrid", this._doNotUseContentItemContext ? this.get_dataItem() : this.get_dataItem().Item, controlParams, cmdArg);
                break;
            case this._saveTempAndOpenLinkCommandName:
                this._handleSaveTempAndOpenLinkCommand(commandArgument);
                break;
            default:
                this._commandArgument.language = commandArgument.language;
                this._setupDialogMode(commandName, this._callerDataItem, this._dataKey, commandArgument);
                break;
        }
    },
    _enforceContentLifecycle: function (dataItem) {
        /// <summary>Makes sure the item can not be edited if it is locked and the currently logged in user cannot unlock it.</summary>
        /// <param name="dataItem">Item the form is bound to</param>
        /// <remarks>If there is an error, the form will be closed.</remarks>        

        if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write && dataItem.Item && dataItem.Item.Lifecycle) {
            var mustCloseWindow = false;
            if (dataItem.Item.Lifecycle.ErrorMessage) {
                // can't use this._messageControl.showNegativeMessage(), because it is not modal and 
                // we need the user to see the message
                this._formError();
                alert(dataItem.Item.Lifecycle.ErrorMessage);
                mustCloseWindow = true;
            }
            else if (dataItem.Item.Lifecycle.IsLocked && !dataItem.Item.Lifecycle.IsLockedByMe) {
                this._formError();
                this._handleLockedItem(dataItem.Item.Id, dataItem.Item.IsUnlockable, dataItem.Item.Lifecycle);
                mustCloseWindow = true;
            }

            if (mustCloseWindow) {
                if (dialogBase)
                    dialogBase.closeAndRebind();
                else
                    window.close();
            }
        }
    },

    unlockContent: function (isCancel) {
        /// <summary>Unlocks the content and closes the window</summary>   
        if (this._disableUnlocking) return;
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._dataItem != null && this._dataItem.Item.Lifecycle
                && this._unlockMode
                && this._dataItem
                && this._dataKey
                && this._dataKey.Id) {

                //try to unlock with item id becouse of the case when unlock a blog post dataKey contains the id of the parent blog;

                var realDataItem;
                if (this._doNotUseContentItemContext) {
                    realDataItem = this._dataItem;
                }
                else {
                    realDataItem = this._dataItem.Item;
                }
                if (realDataItem && realDataItem.Id) {
                    this.sendUnlockRequest(realDataItem.Id);
                }
                else {
                    this.sendUnlockRequest(this._dataKey.Id);
                }
            }
            if (this._unlockMode && this._bulkEditFieldControlIds.length > 0) {
                for (var i = 0, length = this._bulkEditFieldControlIds.length; i < length; i++) {
                    var fieldControl = $find(this._bulkEditFieldControlIds[i]);
                    if (fieldControl) {
                        var items = fieldControl.getDataItems();
                        var provider = "";
                        if (typeof (fieldControl.get_provider) === 'function') {
                            provider = fieldControl.get_provider();
                        }
                        var j = items.length;
                        while (j--) {
                            var item = this._doNotUseContentItemContext ? items[j] : items[j].Item;
                            this.sendUnlockRequest(item.Id, provider);
                        }

                    }
                }
            }
        }
    },

    getUnlockParameters: function (providerName) {
        var params = Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView.callBaseMethod(this, 'getUnlockParameters', [providerName]);
        params["item_type"] = this._contentType;
        return params;
    }
};
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView.registerClass("Telerik.Sitefinity.DynamicModules.Web.UI.Backend.DynamicContentDetailFormView", Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView);