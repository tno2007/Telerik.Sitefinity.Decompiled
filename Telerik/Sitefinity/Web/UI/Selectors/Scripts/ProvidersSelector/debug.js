Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.ProvidersSelector = function (element) {
    Telerik.Sitefinity.Web.UI.ProvidersSelector.initializeBase(this, [element]);

    this._dataKeyName = null;
    this._dataTextField = null;
    this._selectedProvider = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._selectionChangedDelegate = null;
    this._doneSelectingDelegate = null;
    this._cancelDelegate = null;
    this._selectProviderButton = null;
    this._selectedProviderTitle = null;
    this._providersSelectorDialog = null;
    this._providersSelector = null;
    this._selectProviderDelegate = null;
    this._doneSelectingDelegate = null;
    this._cancelSelectingDelegate = null;
    this._providersDialog = null;
    this._providersMenu = null;
    this._moduleName = null;
    this._selectedProviderNameMaxLength = null;
}

Telerik.Sitefinity.Web.UI.ProvidersSelector.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ProvidersSelector.callBaseMethod(this, "initialize");

        if (this._selectProviderButton) {
            this._selectProviderDelegate = Function.createDelegate(this, this._showProvidersSelector);
            $addHandler(this._selectProviderButton, "click", this._selectProviderDelegate);
        }
        if (this._doneButton) {
            this._doneSelectingDelegate = Function.createDelegate(this, this._doneSelecting);
            $addHandler(this._doneButton, "click", this._doneSelectingDelegate);
        }
        if (this._cancelButton) {
            this._cancelSelectingDelegate = Function.createDelegate(this, this._cancelSelecting);
            $addHandler(this._cancelButton, "click", this._cancelSelectingDelegate);
        }

        if (this._providersMenu) {
            $(this._providersMenu).clickMenu();
            jQuery(".sfSelectorDialog").addClass("sfDlgWithSourceSelector");
        }

        this._initializeProviderClick();

        this._providersDialog = jQuery(this._providersSelectorDialog).dialog({
            autoOpen: false,
            modal: false,
            width: 355,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexL"
            }
        });

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        if (this._selectProviderDelegate) {
            if (this._selectProviderButton) {
                $removeHandler(this._selectProviderButton, "click", this._selectProviderDelegate);
            }
            delete this._selectProviderDelegate;
        }
        if (this._doneSelectingDelegate) {
            if (this._doneButton) {
                $removeHandler(this._doneButton, "click", this._doneSelectingDelegate);
            }
            delete this._doneSelectingDelegate;
        }
        if (this._cancelSelectingDelegate) {
            if (this._cancelButton) {
                $removeHandler(this._cancelButton, "click", this._cancelSelectingDelegate);
            }
            delete this._cancelSelectingDelegate;
        }
        Telerik.Sitefinity.Web.UI.ProvidersSelector.callBaseMethod(this, "dispose");
    },

    /* -------------------- events -------------------- */

    add_onProviderSelected: function (delegate) {
        this.get_events().addHandler('onProviderSelected', delegate);
    },

    remove_onProviderSelected: function (delegate) {
        this.get_events().removeHandler('onProviderSelected', delegate);
    },

    _providerSelectedHandler: function (args) {
        var h = this.get_events().getHandler('onProviderSelected');
        if (h) h(this, args);
    },

    /* -------------------- event handlers ------------ */

    _showProvidersSelector: function () {
        this._providersDialog.dialog("open");
        jQuery("body > form").hide();
        this._providersSelector.set_selectedKeys([this._selectedProvider]);
        dialogBase.resizeToContent();
    },

    _doneSelecting: function () {
        var selectedItems = this._providersSelector.get_selectedItems();
        if (selectedItems) {
            var selectedItem = selectedItems[0];
            if (selectedItem) {
                this._selectedProvider = selectedItem[this._dataKeyName];
                this._selectedProviderTitle.innerHTML = selectedItem[this._dataTextField];
                this._providerSelectedHandler({ 'ProviderName': this._selectedProvider, 'ProviderTitle': selectedItem[this._dataTextField] });
            }
        }
        this._providersDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    _cancelSelecting: function () {
        this._providersDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    /* -------------------- public methods ----------- */

    selectProvider: function (providerName, providerTitle, raiseEvent) {
        if (this._providersMenu) {
            $(this.get_element()).find('a.sfProviderLink[name="' + providerName + '"]').click();
        }
        else {
            this._selectedProvider = providerName;

            if (providerTitle == null) {
                var provider = this._findProviderByName(providerName);
                if (provider != null) {
                    providerTitle = provider.Title;
                }
            }

            this._selectedProviderTitle.innerHTML = this._getUIProviderTitle(providerTitle);
            if (raiseEvent) {
                this._providerSelectedHandler({ 'ProviderName': this._selectedProvider, 'ProviderTitle': providerTitle });
            }
        }
    },

    /* -------------------- private methods ----------- */
    _findProviderByName: function (providerName) {
        var providerItems = this.get_providersSelector()._grid.MasterTableView.get_dataItems();
        for (var i = 0; i < providerItems.length; i++) {
            var providerItem = providerItems[i].get_dataItem();
            if (providerItem.Name == providerName) {
                return providerItem;
            }
        }

        return null;
    },

    _getUIProviderTitle: function (source) {
        var result = source;
        if (!result) {
            return result;
        }

        if (this._moduleName) {
            //remove module name from source
            var regExp = new RegExp('\\s' + this._moduleName + '$');
            if (regExp.test(result)) {
                result = result.substring(0, result.length - this._moduleName.length);
                result = result.replace(/\s+$/g, '');
            }
        }

        if (result.length > this._selectedProviderNameMaxLength) {
            //truncate and add ...
            var ellipsis = '...';
            result = result.substring(0, this._selectedProviderNameMaxLength - ellipsis.length).concat(ellipsis);
        }
        return result;
    },

    _initializeProviderClick: function () {
        var that = this;
        $(this.get_element()).find('a.sfProviderLink').click(function (sender, args) {
            $(that.get_element()).find('a.sfProviderLink').removeClass('sfSel');
            $(this).addClass('sfSel');
            that._selectedProvider = this.name;
            var providerTitle = $(this).text();
            that._selectedProviderTitle.innerHTML = that._getUIProviderTitle(providerTitle);
            $(that._selectedProviderTitle).attr("Title", providerTitle);
            that._providerSelectedHandler({ 'ProviderName': that._selectedProvider, 'ProviderTitle': providerTitle });
        });
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    /* -------------------- properties ---------------- */
    get_dataKeyName: function () {
        return this._dataKeyName;
    },
    set_dataKeyName: function (value) {
        this._dataKeyName = value;
    },
    get_dataTextField: function () {
        return this._dataTextField;
    },
    set_dataTextField: function (value) {
        this._dataTextField = value;
    },
    get_selectedProvider: function () {
        return this._selectedProvider;
    },
    set_selectedProvider: function (value) {
        this._selectedProvider = value;
    },
    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },
    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },
    get_selectProviderButton: function () {
        return this._selectProviderButton;
    },
    set_selectProviderButton: function (value) {
        this._selectProviderButton = value;
    },
    get_selectedProviderTitle: function () {
        return this._selectedProviderTitle;
    },
    set_selectedProviderTitle: function (value) {
        this._selectedProviderTitle = value;
    },
    get_providersSelectorDialog: function () {
        return this._providersSelectorDialog;
    },
    set_providersSelectorDialog: function (value) {
        this._providersSelectorDialog = value;
    },
    get_providersSelector: function () {
        return this._providersSelector;
    },
    set_providersSelector: function (value) {
        this._providersSelector = value;
    },
    get_providersMenu: function () {
        return this._providersMenu;
    },
    set_providersMenu: function (value) {
        this._providersMenu = value;
    },
    get_moduleName: function () {
        return this._moduleName;
    },
    set_moduleName: function (value) {
        this._moduleName = value;
    },
    get_selectedProviderNameMaxLength: function () {
        return this._selectedProviderNameMaxLength;
    },
    set_selectedProviderNameMaxLength: function (value) {
        this._selectedProviderNameMaxLength = value;
    }
};

Telerik.Sitefinity.Web.UI.ProvidersSelector.registerClass("Telerik.Sitefinity.Web.UI.ProvidersSelector", Sys.UI.Control);
