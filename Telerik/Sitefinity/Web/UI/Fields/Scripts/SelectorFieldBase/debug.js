Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase.initializeBase(this, [element]);

    this._element = element;
    
    this._selectedValues = null;
    
    this._allValues = null;
    this._selectedValuesContainer = null;
    this._openSelectorButton = null;
    this._selectorButtonText = null;
    this._selectorDialog = null;
    this._clientLabelManager = null;

    this._isFirstOpen = true;

    this._onAllValuesClickDelegate = null;
    this._onSelectedValuesClickDelegate = null;
    this._onOpenSelectorButtonClickDelegate = null;

    this._selectorDialogShowDelegate = null;
    this._selectorDialogLoadDelegate = null;
    this._selectorDialogCloseDelegate = null;

    this._binderDataBoundDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase.callBaseMethod(this, "initialize");
     
        this._onAllValuesClickDelegate = Function.createDelegate(this, this._onAllValuesClickHandler);
        $addHandler(this.get_allValues(), 'click', this._onAllValuesClickDelegate);

        this._onSelectedValuesClickDelegate = Function.createDelegate(this, this._onSelectedValuesClickHandler);
        $addHandler(this.get_selectedValues(), 'click', this._onSelectedValuesClickDelegate);

        this._onOpenSelectorButtonClickDelegate = Function.createDelegate(this, this._onOpenSelectorButtonClickHandler);
        $addHandler(this.get_openSelectorButton(), 'click', this._onOpenSelectorButtonClickDelegate);

        this._selectorDialogLoadDelegate = Function.createDelegate(this, this._selectorDialogLoadHandler);
        this.get_selectorDialog().add_pageLoad(this._selectorDialogLoadDelegate);

        this._selectorDialogShowDelegate = Function.createDelegate(this, this._selectorDialogShowHandler);
        this.get_selectorDialog().add_show(this._selectorDialogShowDelegate);

        this._selectorDialogCloseDelegate = Function.createDelegate(this, this._selectorDialogCloseHandler);
        this.get_selectorDialog().add_close(this._selectorDialogCloseDelegate);

        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBoundHandler);
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */

        if (this._onAllValuesClickDelegate) {
            if (this.get_allValues()) {
                $removeHandler(this.get_allValues(), "click", this._onAllValuesClickDelegate);
            }
            delete this._onAllValuesClickDelegate;
        }

        if (this._onSelectedValuesClickDelegate) {
            if (this.get_selectedValues()) {
                $removeHandler(this.get_selectedValues(), "click", this._onSelectedValuesClickDelegate);
            }
            delete this._onSelectedValuesClickDelegate;
        }

        if (this._onOpenSelectorButtonClickDelegate) {
            if (this.get_openSelectorButton()) {
                $removeHandler(this.get_openSelectorButton(), "click", this._onOpenSelectorButtonClickDelegate);
            }
            delete this._onOpenSelectorButtonClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase.callBaseMethod(this, "dispose");
    },

    set_value: function (value) {
        this._value = value;

        if (value && value.length > 0) {
            jQuery(this.get_selectedValues()).attr('checked', true);
            this._showUI();
        } else {
            jQuery(this.get_allValues()).attr('checked', true);
            this._hideUI();
        }

        this._updateSelectorButtonText();
        this.raisePropertyChanged("_value");
    },

    get_value: function () {
        if (jQuery(this.get_allValues()).attr('checked') == 'checked' || this._value == null) {
            return [];
        }
        return [].concat(this._value);
    },

    reset: function () {
        this.set_value(null);
    },

    _hideUI: function() {
        jQuery(this.get_openSelectorButton()).hide();
        jQuery(this.get_selectedValuesContainer()).hide();
    },

    _showUI: function() {
        jQuery(this.get_openSelectorButton()).show();
        this._updateValueNamesUI();
    },

    _updateValueNamesUI: function () {
        var names = [];
        if (this._value != null && this._value.length > 0) {
            for (var i = 0; i < this._value.length; i++) {
                names.push(this._value[i].DisplayName);
            }

            jQuery(this.get_selectedValuesContainer()).html(names.join(', '));
            jQuery(this.get_selectedValuesContainer()).show();
        } else {
            jQuery(this.get_selectedValuesContainer()).hide();
        }
    },

    _updateSelectorButtonText : function() {
        var text = "";
        var manager = this.get_clientLabelManager();

        if (manager != null) {

            if (this._value != null && this._value.length > 0) {
                text = manager.getLabel("Labels", "Change");
            } else {
                text = manager.getLabel("Labels", "Select");
            }

            jQuery(this.get_selectorButtonText()).html(text);
        }
    },
    
    _onAllValuesClickHandler: function(sender, args) {
        this._hideUI();
    },

    _onSelectedValuesClickHandler: function (sender, args) {
        this._showUI();
    },

    _onOpenSelectorButtonClickHandler: function (sender, args) {
        this.get_selectorDialog().show();
    },

    _selectorDialogShowHandler: function (sender, args) {
        var dialog = this.get_selectorDialog().AjaxDialog;
        if (dialog) {
            var selector = dialog.get_itemSelector();
            var binder = selector.get_binder();
            if (this._isFirstOpen) {
                this._isFirstOpen = false;
                binder.set_postData({ "siteIds": [] });
                selector.bindSelector();
            }

            var items = [];
            if (this._value != null)
                items = this._value;
            var keys = [];
            for (var i = 0; i < items.length; i++) {
                keys.push(items[i]["ShortName"]);
            }
            dialog.set_intialSelectedItems(keys);
            dialog.resizeToContent();
        }
    },

    _selectorDialogLoadHandler: function (sender, args) {
        this._selectorDialogShowHandler(sender, args);
    },

    _selectorDialogCloseHandler: function (sender, args) {
        var items = args.get_argument();
        if (items != null) {
            this.set_value([].concat(items));
        }
    },

    get_selectedValues: function () {
        return this._selectedValues;
    },
    set_selectedValues: function (value) {
        this._selectedValues = value;
    },

    get_allValues: function () {
        return this._allValues;
    },
    set_allValues: function (value) {
        this._allValues = value;
    },

    get_selectedValuesContainer: function () {
        return this._selectedValuesContainer;
    },
    set_selectedValuesContainer: function (value) {
        this._selectedValuesContainer = value;
    },

    get_openSelectorButton: function () {
        return this._openSelectorButton;
    },
    set_openSelectorButton: function (value) {
        this._openSelectorButton = value;
    },

    get_selectorButtonText: function () {
        return this._selectorButtonText;
    },
    set_selectorButtonText: function (value) {
        this._selectorButtonText = value;
    },

    get_selectorDialog: function () {
        return this._selectorDialog;
    },
    set_selectorDialog: function (value) {
        this._selectorDialog = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    set_siteIds: function (value) {
        this._siteIds = value;
    }

};

Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase.registerClass("Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase", Telerik.Sitefinity.Web.UI.Fields.FieldControl);