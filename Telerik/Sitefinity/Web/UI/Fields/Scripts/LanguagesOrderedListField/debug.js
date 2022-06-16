﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField.initializeBase(this, [element]);
    this._value = null;
    this._windowManager = null;
    this._loadDelegate = null;
    this._openSelector = null;
    this._commandBar = null;
    this._selectorWrapper = null;
    this._dialogWindow = null;
    this._binder = null;
    this._itemsList = null;
    this._headerRow = null;
    this._clientLabelManager = null;
    this._languageListControlDelegate = null;
    this._commandBarCommandDelegate = null;
    this._openSelectorCommandDelegate = null;
    this._windowLoadHandlerDelegate = null;
    this._dialogDoneHandlerDelegate = null;
    this._itemCommandHandlerDelegate = null;
    this._sortStopDelegatge = null;
    this._sortStartDelegate = null;
    this._valueChangedDelegate = null;
    this._itemDataBoundDelegate = null;
    this._confirmationDialogPanel = null;
    this._removeLanguageConfirmationMessagesPanel = null;
    this._removeLanguageConfirmationButtonsPanel = null;
    this._cannotRemoveLangMessagesPanel = null;
    this._showConfirmationDialog = null;
    this._confirmationDialog = null;
    this._confirmRemoveLink = null;
    this._cancelRemoveLink = null;
    this._cancelOkayLink = null;
    this._confirmRemoveLinkClickDelegate = null;
    this._cancelRemoveLinkClickDelegate = null;
    this._cancelOkayLinkClickDelegate = null;
    this._cultureToBeRemoved = null;
    this._moreText = null;
    this._lessText = null;
    this._sitesUsingCultureLabel = null;
    this._cultureCanBeRemoved = null;
    this._confirmDialogMoreText = null;
    this._confirmDialogLessText = null;
}

Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField.callBaseMethod(this, "initialize");

        this._valueChangedDelegate = Function.createDelegate(this, this._valueChanged);
        this.add_valueChanged(this._valueChangedDelegate);

        if (this._openSelectorCommandDelegate == null)
            this._openSelectorCommandDelegate = Function.createDelegate(this, this.openSelectorCommand);
        if (this._openSelector) {
            $addHandler(this._openSelector, "click", this._openSelectorCommandDelegate);
        }
        this._itemCommandHandlerDelegate = Function.createDelegate(this, this._itemCommandHandler);
        this._binder.add_onItemCommand(this._itemCommandHandlerDelegate);
        this._windowLoadHandlerDelegate = Function.createDelegate(this, this._windowLoadHandler);
        this._dialogDoneHandlerDelegate = Function.createDelegate(this, this._dialogDoneHandler);
        this._loadDelegate = Function.createDelegate(this, this.loadHandler);
        Sys.Application.add_load(this._loadDelegate);

        this._sortStopDelegatge = Function.createDelegate(this, this._sortStopHandler);
        this._sortStartDelegate = Function.createDelegate(this, this._sortStartHandler);

        this._itemDataBoundDelegate = Function.createDelegate(this, this._itemDataBound);
        this._binder.add_onItemDataBound(this._itemDataBoundDelegate);

        if (this._confirmRemoveLink) {
            this._confirmRemoveLinkClickDelegate = Function.createDelegate(this, this._confirmRemoveLinkClick);
            $addHandler(this._confirmRemoveLink, "click", this._confirmRemoveLinkClickDelegate);
        }
        if (this._cancelRemoveLink) {
            this._cancelRemoveLinkClickDelegate = Function.createDelegate(this, this._cancelRemoveLinkClick);
            $addHandler(this._cancelRemoveLink, "click", this._cancelRemoveLinkClickDelegate);
        }
        if (this._cancelOkayLink) {
            this._cancelOkayLinkClickDelegate = Function.createDelegate(this, this._cancelRemoveLinkClick);
            $addHandler(this._cancelOkayLink, "click", this._cancelOkayLinkClickDelegate);
        }

        this.enableSortable();

        if (this._showConfirmationDialog && this._confirmationDialogPanel) {
            this._confirmationDialog = jQuery(this._confirmationDialogPanel).dialog({
                autoOpen: false,
                modal: true,
                width: 355,
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                classes: {
                    "ui-dialog": "sfZIndexL"
                }
            });
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField.callBaseMethod(this, "dispose");

        if (this._valueChangedDelegate) {
            this.remove_valueChanged(this._valueChangedDelegate);
            delete this._valueChangedDelegate;
        }

        if (this._openSelector) {
            $clearHandlers(this._openSelector);
        }

        if (this._openSelectorCommandDelegate) {
            delete this._openSelectorCommandDelegate;
        }

        if (this._loadDelegate) {
            delete this._loadDelegate;
        }

        if (this._itemCommandHandlerDelegate) {
            delete this._itemCommandHandlerDelegate;
        }

        if (this._dialogDoneHandlerDelegate) {
            delete this._dialogDoneHandlerDelegate;
        }

        if (this._windowLoadHandlerDelegate) {
            if (this._dialogWindow) {
                this._dialogWindow.remove_pageLoad(this._windowLoadHandlerDelegate);
            }
            delete this._windowLoadHandlerDelegate;
        }

        if (this._itemDataBoundDelegate) {
            this._binder.remove_onItemDataBound(this._itemDataBoundDelegate);
            delete this._itemDataBoundDelegate;
        }

        if (this._confirmRemoveLinkClickDelegate) {
            $removeHandler(this._confirmRemoveLink, "click", this._confirmRemoveLinkClickDelegate);
            delete this._confirmRemoveLinkClickDelegate;
        }

        if (this._cancelRemoveLinkClickDelegate) {
            $removeHandler(this._cancelRemoveLink, "click", this._cancelRemoveLinkClickDelegate);
            delete this._cancelRemoveLinkClickDelegate;
        }

        if (this._cancelOkayLinkClickDelegate) {
            $removeHandler(this._cancelOkayLink, "click", this._cancelOkayLinkClickDelegate);
            delete this._cancelOkayLinkClickDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    openSelectorCommand: function () {
        this._openSelectorDialog();
        return false;
    },

    enableSortable: function () {
        jQuery(this._itemsList).sortable(
            {
                update: this._sortStopDelegatge,
                start: this._sortStartDelegate,
                axis: 'y',
                forcePlaceholderSize: true
            }
        );
    },

    disableSortable: function () {
        jQuery(this._itemsList).sortable("disable");
    },

    //changing the culture of the caller's manager
    saveChanges: function (dataItem, successCallback, failureCallback, caller) {
        return;
    },

    //This should always return true in order for the saveChanges method to be executed. 
    isChanged: function () {
        return false;
    },
    setDefaultCulture: function (defaultCulture) {
        this._setDefaultCulture(defaultCulture);
    },
    getDefaultCulture: function () {
        var items = this._value;
        if (!items) return null;
        for (var i = 0, l = items.length; i < l; i++) {
            var item = items[i];
            if (item.IsDefault)
                return item.Key;
        }
        return null;
    },

    /* -------------------- events -------------------- */

    // implements Telerik.Sitefinity.Web.UI.Fields.ICommandField interface
    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    add_selectionDone: function (handler) {
        this.get_events().addHandler('selectionDone', handler);
    },

    remove_selectionDone: function (handler) {
        this.get_events().removeHandler('selectionDone', handler);
    },

    /* -------------------- event handlers ------------ */

    loadHandler: function () {
        this._dialogWindow = this._windowManager.getWindowByName("selectorDialog");
        this._dialogWindow.add_pageLoad(this._windowLoadHandlerDelegate);
    },

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField.callBaseMethod(this, "reset");
        this.set_value([]);
    },

    _itemCommandHandler: function (sender, args) {
        var item = args.get_dataItem();
        switch (args.get_commandName()) {
            case "setDefualt":
                this._setDefaultCulture(item.Key);
                break;
            case "removeCulture":
                this._removeCultureClick(item);
                break;
        }
    },

    _windowLoadHandler: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (typeof frameHandle.initDialog == "function") {
            frameHandle.initDialog(this._value, this._dialogDoneHandlerDelegate, null);
        }
    },

    _dialogDoneHandler: function (data, context) {
        var newItems = data;
        var items = this._value ? this._value : [];
        var error;
        if (newItems && newItems.length > 0) {
            var isDefaultAlreadySet = false;
            jQuery.each(items, function (k, el) {
                if (el.IsDefault) {
                    isDefaultAlreadySet = true;
                }
            });

            for (var i = 0, l = newItems.length; i < l; i++) {
                var keyFound = false;
                var item = newItems[i];

                if (isDefaultAlreadySet && item.IsDefault) {
                    item.IsDefault = false;
                }

                //Check if the new language is already present in the newsItems collection.
                jQuery.each(items, function (k, el) {
                    if (el.Key == item.Key) {
                        keyFound = true;
                        //break;
                        return false;
                    }
                });
                //Add only if the language is not already added.
                if (!keyFound) {
                    items.push(jQuery.extend(true, {}, item));
                }
            }

            var hasDefault = false;
            for (var i = 0; i < items.length; i++) {
                if (item.IsDefault) {
                    hasDefault = true;
                    break;
                }
            }

            if (!hasDefault && items.length > 0) {

                items[0].IsDefault = true;
            }

            this.set_value(items);
        }
        this._onSelectionDone(items, error);
    },

    _sortStopHandler: function (event, ui) {
        var el = ui.item;
        var newIndex = jQuery(this._itemsList).children().index(el);
        var oldIndex = jQuery(el).data("startIndex");

        var items = this._value;
        var dataItem = items[newIndex];
        items[newIndex] = items[oldIndex];
        items[oldIndex] = dataItem;
        this.set_value(items);
    },

    _sortStartHandler: function (event, ui) {
        var el = ui.item;
        var startIndex = jQuery(this._itemsList).children().index(el);
        jQuery(el).data("startIndex", startIndex);
    },

    _valueChanged: function (sender, args) {
        if (this._value) {
            this._binder.BindCollection({ 'Items': this._value });
            jQuery(this._itemsList).sortable((this._value.length > 1) ? "enable" : "disable");
            if (this._value.length > 1 && this._headerRow != null) {
                jQuery(this._headerRow).removeClass("sfDragNDropHeaderDisabled");
            } else {
                jQuery(this._headerRow).addClass("sfDragNDropHeaderDisabled");
            }
        }
    },

    _itemDataBound: function (sender, itemEventArgs) {
        var siteNames = [];
        for (var i = 0; i < itemEventArgs._dataItem.SitesNames.length; i++) {
            siteNames.push(itemEventArgs._dataItem.SitesNames[i].htmlEncode());
        }

        jQuery(itemEventArgs._itemElement).find('.sfExpandableLabel').expandableLabel({
            maxCharSize: 22,
            items: siteNames,
            moreText: this._moreText,
            lessText: this._lessText,
            wrapperClass: "sfLangSiteItem"
        });
    },

    _confirmRemoveLinkClick: function () {
        this._removeCulture(this._cultureToBeRemoved);
        this._cultureToBeRemoved = null;
        this._confirmationDialog.dialog("close");
    },

    _cancelRemoveLinkClick: function () {
        this._cultureToBeRemoved = null;
        this._confirmationDialog.dialog("close");
    },

    /* -------------------- private methods ----------- */

    _setDefaultCulture: function (defaultCulture) {
        if (!defaultCulture || typeof defaultCulture !== "string")
            return;
        var items = this._value, item;
        for (var i = 0, l = items.length; i < l; i++) {
            item = items[i];
            item.IsDefault = item.Key === defaultCulture;
        }
        this.set_value(items);
    },

    _removeCultureClick: function (item) {
        var culture = item.Key;
        if (this._showConfirmationDialog && this._confirmationDialog) {
            this._cultureToBeRemoved = culture;
            this._checkIfCultureCanBeRemoved(item.SitesUsingCultureAsDefault);
        }
        else {
            this._removeCulture(culture);
        }
    },

    _checkIfCultureCanBeRemoved: function (siteNamesUsingCulture) {
        if (siteNamesUsingCulture.length > 0) {
            this._configureDialogCannotRemoveCulture(siteNamesUsingCulture);
        }
        else {
            this._configureDialogCanRemoveCulture();
        }
        this._confirmationDialog.dialog("open");
        this._dialogScrollToTop(this._confirmationDialog);
    },

    _configureDialogCanRemoveCulture: function () {
        $(this._removeLanguageConfirmationMessagesPanel).show();
        $(this._removeLanguageConfirmationButtonsPanel).show();
        $(this._cannotRemoveLangMessagesPanel).hide();
        $(this._cancelOkayLink).hide();
    },

    _configureDialogCannotRemoveCulture: function (siteNames) {
        $(this._removeLanguageConfirmationMessagesPanel).hide();
        $(this._removeLanguageConfirmationButtonsPanel).hide();

        jQuery(this._confirmationDialog).find('.sfExpandableLabel').expandableLabel({
            maxCharSize: 50,
            items: siteNames,
            moreText: this._confirmDialogMoreText,
            lessText: this._confirmDialogLessText
        });

        $(this._cannotRemoveLangMessagesPanel).show();
        $(this._cancelOkayLink).show();
    },

    _removeCulture: function (culture) {
        var items = this._value;
        for (var i = 0, l = items.length; i < l; i++) {
            if (items[i].Key == culture) {
                items.splice(i, 1);
                break;
            }
        }
        this.set_value(items);
    },

    _clearItems: function () {
    },

    _openSelectorDialog: function () {
        var oWnd = this._windowManager.open(this._dialogUrl, "selectorDialog");
        if (jQuery("body > .k-window.sfMaximizedWindow").is(":visible")) {
            var baseZindex = jQuery("body .k-window.sfMaximizedWindow").css("z-index");
            jQuery("body > form > .RadWindow:visible").css({ "z-index": baseZindex + 2 });
            jQuery("body > form > .TelerikModalOverlay:visible").css({ "z-index": baseZindex + 1 });
        }
        oWnd.setSize(425, 250);
        oWnd.set_modal(true);
        Telerik.Sitefinity.centerWindowHorizontally(oWnd);
        oWnd.set_autoSizeBehaviors(5);
        oWnd.set_visibleStatusbar(false);
        oWnd.set_visibleTitlebar(true);
        oWnd.set_cssClass("sfpeDesigner");
    },

    _onSelectionDone: function (items, error) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Fields.SelectionDoneEventArgs(items, error);
        var h = this.get_events().getHandler('selectionDone');
        if (h) h(this, eventArgs);
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    /* -------------------- properties ---------------- */

    get_value: function () {
        return this._value;
    },
    set_value: function (value) {
        this._value = value;
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },
    get_openSelector: function () {
        return this._openSelector;
    },
    set_openSelector: function (value) {
        this._openSelector = value;
    },
    get_dialogUrl: function () {
        return this._dialogUrl;
    },
    set_dialogUrl: function (value) {
        this._dialogUrl = value;
    },
    get_windowManager: function () {
        return this._windowManager;
    },
    set_windowManager: function (value) {
        this._windowManager = value;
    },
    get_binder: function () {
        return this._binder;
    },
    set_binder: function (value) {
        this._binder = value;
    },
    get_itemsList: function () {
        return this._itemsList;
    },
    set_itemsList: function (value) {
        this._itemsList = value;
    },
    get_headerRow: function () {
        return this._headerRow;
    },
    set_headerRow: function (value) {
        this._headerRow = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_confirmationDialogPanel: function () {
        return this._confirmationDialogPanel;
    },
    set_confirmationDialogPanel: function (value) {
        this._confirmationDialogPanel = value;
    },
    get_removeLanguageConfirmationMessagesPanel: function () {
        return this._removeLanguageConfirmationMessagesPanel;
    },
    set_removeLanguageConfirmationMessagesPanel: function (value) {
        this._removeLanguageConfirmationMessagesPanel = value;
    },
    get_removeLanguageConfirmationButtonsPanel: function () {
        return this._removeLanguageConfirmationButtonsPanel;
    },
    set_removeLanguageConfirmationButtonsPanel: function (value) {
        this._removeLanguageConfirmationButtonsPanel = value;
    },
    get_cannotRemoveLangMessagesPanel: function () {
        return this._cannotRemoveLangMessagesPanel;
    },
    set_cannotRemoveLangMessagesPanel: function (value) {
        this._cannotRemoveLangMessagesPanel = value;
    },
    get_confirmRemoveLink: function () {
        return this._confirmRemoveLink;
    },
    set_confirmRemoveLink: function (value) {
        this._confirmRemoveLink = value;
    },
    get_cancelRemoveLink: function () {
        return this._cancelRemoveLink;
    },
    set_cancelRemoveLink: function (value) {
        this._cancelRemoveLink = value;
    },
    get_cancelOkayLink: function () {
        return this._cancelOkayLink;
    },
    set_cancelOkayLink: function (value) {
        this._cancelOkayLink = value;
    },
    get_showConfirmationDialog: function () {
        return this._showConfirmationDialog;
    },
    set_showConfirmationDialog: function (value) {
        this._showConfirmationDialog = value;
    },
    get_moreText: function () {
        return this._moreText;
    },
    set_moreText: function (value) {
        this._moreText = value;
    },
    get_lessText: function () {
        return this._lessText;
    },
    set_lessText: function (value) {
        this._lessText = value;
    },
    get_sitesUsingCultureLabel: function () {
        return this._sitesUsingCultureLabel;
    },
    set_sitesUsingCultureLabel: function (value) {
        this._sitesUsingCultureLabel = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField.registerClass("Telerik.Sitefinity.Web.UI.Fields.LanguagesOrderedListField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.ISelfExecutableField, Telerik.Sitefinity.Web.UI.Fields.ICommandField);

Telerik.Sitefinity.Web.UI.Fields.SelectionDoneEventArgs = function (items, error) {
    Telerik.Sitefinity.Web.UI.Fields.SelectionDoneEventArgs.initializeBase(this);
    this._items = items;
    this._error = error;
};
Telerik.Sitefinity.Web.UI.Fields.SelectionDoneEventArgs.prototype = {
    get_items: function () {
        return this._items;
    },
    get_error: function () {
        return this._error;
    }
};
Telerik.Sitefinity.Web.UI.Fields.SelectionDoneEventArgs.registerClass("Telerik.Sitefinity.Web.UI.Fields.SelectionDoneEventArgs", Sys.EventArgs);
