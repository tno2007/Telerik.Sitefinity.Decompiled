Type.registerNamespace("Telerik.Sitefinity.Web.Scripts");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Scripts");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField.initializeBase(this, [element]);
    this._changed = false;
    this._value = null;
    this._initialValue = null;
    this._initialText = null;
    this._noContentSelectedText = null;
    this._selectContentButton = null;
    this._buttonSelectText = null;
    this._buttonChangeText = null;
    this._showItemSelectorDelegate = null;
    this._selectedContentTitle = null;
    this._selectorTag = null;
    this._mainFieldName = null;
    this._valueChangedDelegate = null;
    this._dataBoundHandlerDelegate = null;
    this._itemSelector = null;
    this._itemType = null;
    this._isSelectorReady = null;
    this._doneSelectingButton = null;
    this._doneSelectingDelegate = null;
    this._cancelSelectingButton = null;
    this._cancelSelectingDelegate = null;
    this._dialog = null;
    this._providerName = null;
    this._allowMultipleSelection = null;
}

Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField.prototype =
{
    /* --------------------  Set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField.callBaseMethod(this, "initialize");

        this._dataBoundHandlerDelegate = Function.createDelegate(this, this._dataBoundHandler);
        this.add_dataBound(this._dataBoundHandlerDelegate);

        if (this._selectContentButton) {
            this._showItemSelectorDelegate = Function.createDelegate(this, this._showContentSelector);
            $addHandler(this._selectContentButton, "click", this._showItemSelectorDelegate);
        }

        if (this._doneSelectingButton) {
            this._doneSelectingDelegate = Function.createDelegate(this, this._doneSelecting);
            $addHandler(this._doneSelectingButton, "click", this._doneSelectingDelegate);
        }

        if (this._cancelSelectingButton) {
            this._cancelSelectingDelegate = Function.createDelegate(this, this._cancelSelecting);
            $addHandler(this._cancelSelectingButton, "click", this._cancelSelectingDelegate);
        }

        if (this._initialText) {
            this.get_selectedContentTitle().innerHTML = this._initialText;
            jQuery(this.get_selectedContentTitle()).show();
        } else if (this.get_noContentSelectedText()) {
            this.get_selectedContentTitle().innerHTML = this.get_noContentSelectedText();
            jQuery(this.get_selectedContentTitle()).show();
        }

        if (this._itemSelector) {
            this.get_itemSelector().set_itemType(this._itemType);
        }

        this._dialog = jQuery(this._selectorTag).dialog({
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
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField.callBaseMethod(this, "dispose");

        if (this._dataBoundHandlerDelegate) {
            this.remove_dataBound(this._dataBoundHandlerDelegate);
            delete this._dataBoundHandlerDelegate;
        }

        if (this._showItemSelectorDelegate) {
            if (this._selectContentButton) {
                $removeHandler(this._selectContentButton, "click", this._showItemSelectorDelegate);
            }
            delete this._showItemSelectorDelegate;
        }

        if (this._doneSelectingDelegate) {
            if (this._doneSelectingButton) {
                $removeHandler(this._doneSelectingButton, "click", this._doneSelectingDelegate);
            }
            delete this._doneSelectingDelegate;
        }

        if (this._cancelSelectingDelegate) {
            if (this._cancelSelectingButton) {
                $removeHandler(this._cancelSelectingButton, "click", this._cancelSelectingDelegate);
            }
            delete this._cancelSelectingDelegate;
        }
    },

    /* -------------------- Public functions ------------ */

    resetSelectedContentTitle: function () {
        var selectedContentTitle = this.get_selectedContentTitle();
        selectedContentTitle.innerHTML = this.get_noContentSelectedText() || '';
        if (selectedContentTitle.innerHTML === '') {
            jQuery(this.get_selectedContentTitle()).hide();
        }
    },

    set_selectContentButtonText: function (value) {
        $(this._selectContentButton).find("span").text(value);
    },

    /* -------------------- Event handlers ------------ */

    _dataBoundHandler: function (sender) {
        if (this._initialValue && (!(this._value) || this._value === this._emptyGuid)) {
            this.set_value(this._initialValue);
        }
    },

    // handles the click event of the select content button
    _showContentSelector: function () {
        var itemSelector = this.get_itemSelector();
        if (itemSelector) {
            itemSelector.dataBind();
            var value = this.get_value();
            if (value) {
                if (Object.prototype.toString.call(value) === '[object Array]') {
                    itemSelector.set_selectedKeys(value);
                } else {
                    itemSelector.set_selectedKeys([value]);
                }
            }
        }

        this._dialog.dialog("open");
        this._dialogScrollToTop(this._dialog);
    },

    _selectContent: function (selectedItems) {
        this._dialog.dialog("close");
        if (selectedItems == null) return;

        var titles = [];
        var itemIds = [];

        for (var i = 0; i < selectedItems.length; i++) {
            this._processSelectedItem(selectedItems[i], titles, itemIds);
        }

        if (this._allowMultipleSelection) {
            this.set_value(itemIds);
        } else {
            if (selectedItems.length > 0) {
                this.set_value(itemIds[0]);
            }
        }

        var selectedContentTitle = this.get_selectedContentTitle();
        if (titles.length > 0) {
            selectedContentTitle.innerHTML = titles.join(", ");
            jQuery(this.get_selectedContentTitle()).show();
        } else {
            selectedContentTitle.innerHTML = this.get_noContentSelectedText() || '';
            if (selectedContentTitle.innerHTML === '') {
                jQuery(this.get_selectedContentTitle()).hide();
            }
        }
    },

    _processSelectedItem: function (item, titles, itemIds) {
        var selectedTitle = '';
        itemIds.push(item.Id);
        if (item.hasOwnProperty(this._mainFieldName)) {
            if (item[this._mainFieldName].hasOwnProperty('Value')) {
                selectedTitle = item[this._mainFieldName].Value;
            } else {
                selectedTitle = item[this._mainFieldName];
            }
        }
        else if (item.hasOwnProperty('Title')) {
            if (item.Title.hasOwnProperty('Value')) {
                selectedTitle = item.Title.Value;
            } else {
                selectedTitle = item.Title;
            }
        }

        titles.push(selectedTitle);
    },

    _doneSelecting: function () {
        var selectedItems = this.get_itemSelector().get_selectedItems();
        this._selectContent(selectedItems);
    },

    _cancelSelecting: function () {
        this._selectContent(null);
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    /* --------------------  IParentSelectorField methods ----------- */

    getSelectedParentId: function () {
        if (!this._allowMultipleSelection) {
            if (Object.prototype.toString.call(this._value) === '[object Array]' && this._value.length > 0) {
                return this._value[0];
            }
            return this._value;
        }
        return this._value;
    },

    isChanged: function () {
        return this._changed;
    },

    /* --------------------  Properties  ----------- */
    get_value: function () {
        if (!this._value || this._value === this._emptyGuid) {
            return "";
        }
        return this._value;
    },
    set_value: function (value) {
        if (this._value != value) {
            this._value = value;
            this._changed = value != this._initialValue;

            if (value && value.length > 0 && value != this._emptyGuid) {
                this.set_selectContentButtonText(this.get_buttonChangeText());
            } else {
                this.set_selectContentButtonText(this.get_buttonSelectText());
            }
        }
    },
    get_initialValue: function () {
        return this._initialValue;
    },
    set_initialValue: function (value) {
        this._initialValue = value;
    },
    get_initialText: function () {
        return this._initialText;
    },
    set_initialText: function (value) {
        this._initialText = value;
    },
    get_noContentSelectedText: function () {
        return this._noContentSelectedText;
    },
    set_noContentSelectedText: function (value) {
        this._noContentSelectedText = value;
    },
    get_selectContentButton: function () {
        return this._selectContentButton;
    },
    set_selectContentButton: function (value) {
        this._selectContentButton = value;
    },
    get_selectedContentTitle: function () {
        return this._selectedContentTitle;
    },
    set_selectedContentTitle: function (value) {
        this._selectedContentTitle = value;
    },
    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    },
    get_mainFieldName: function () {
        return this._mainFieldName;
    },
    set_mainFieldName: function (value) {
        this._mainFieldName = value;
    },
    get_itemSelector: function () {
        return this._itemSelector;
    },
    set_itemSelector: function (value) {
        this._itemSelector = value;
    },
    get_itemType: function () {
        return this._itemType;
    },
    set_itemType: function (value) {
        this._itemType = value;
        var itemSelector = this.get_itemSelector();
        if (itemSelector) {
            itemSelector.set_itemType(value);
        }
    },
    get_isSelectorReady: function () {
        return this._isSelectorReady;
    },
    set_isSelectorReady: function (value) {
        this._isSelectorReady = value;
    },
    get_doneSelectingButton: function () {
        return this._doneSelectingButton;
    },
    set_doneSelectingButton: function (value) {
        this._doneSelectingButton = value;
    },
    get_doneSelectingDelegate: function () {
        return this._doneSelectingDelegate;
    },
    set_doneSelectingDelegate: function (value) {
        this._doneSelectingDelegate = value;
    },
    get_cancelSelectingButton: function () {
        return this._cancelSelectingButton;
    },
    set_cancelSelectingButton: function (value) {
        this._cancelSelectingButton = value;
    },
    get_cancelSelectingDelegate: function () {
        return this._cancelSelectingDelegate;
    },
    set_cancelSelectingDelegate: function (value) {
        this._cancelSelectingDelegate = value;
    },
    // Passes the provider to the control
    set_providerName: function (value) {
        this._providerName = value;

        var itemSelector = this.get_itemSelector();
        if (itemSelector) {
            itemSelector.set_providerName(value);
        }
    },

    // Gets the provider from the control
    get_providerName: function () {
        return this._providerName;
    },
    //gets if multiple parents can be selected
    get_allowMultipleSelection: function () {
        return this._allowMultipleSelection;
    },

    //sets if multiple parents can be selected
    set_allowMultipleSelection: function (value) {
        this._allowMultipleSelection = value;
    },
    get_buttonSelectText: function () {
        return this._buttonSelectText;
    },
    set_buttonSelectText: function (value) {
        this._buttonSelectText = value;
    },
    get_buttonChangeText: function () {
        return this._buttonChangeText;
    },
    set_buttonChangeText: function (value) {
        this._buttonChangeText = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.IParentSelectorField, Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider);
