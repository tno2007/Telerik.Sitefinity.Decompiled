Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector = function (element) {
    this._queryData = null;
    this._allowMultipleSelection = false;
    this._items = [];
    this._itemButtons = [];
    this._disabledTextClass = null;
    this._disabled = null;

    this._onLoadDelegate = null;
    this._buttonClickedDelegate = null;
    this._uiCulture = null;
    this._culture = null;

    Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.callBaseMethod(this, "initialize");

        this._onLoadDelegate = Function.createDelegate(this, this.onLoad);
        this._buttonClickedDelegate = Function.createDelegate(this, this.buttonClicked);
        for (var i = 0, l = this._itemButtons.length; i < l; i++) {
            $addHandler($get(this._itemButtons[i]), "click", this._buttonClickedDelegate);
        }

        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.callBaseMethod(this, "dispose");

        Sys.Application.remove_load(this._onLoadDelegate);
        delete this._onLoadDelegate;
        for (var i = 0, l = this._itemButtons.length; i < l; i++) {
            $clearHandlers($get(this._itemButtons[i]));
        }
        delete this._buttonClickedDelegate;
    },

    /* --------------------------------- public methods ---------------------------------- */
    refreshUI: function () {
        var hasSelectedItem = false;
        for (var i = 0, l = this._items.length; i < l; i++) {
            var item = $find(this._items[i]);
            var groupName = item.get_queryDataName();
            var hasGroup = this._queryData.hasGroup(groupName);

            item.set_selected(hasGroup);
            if (hasGroup) {
                hasSelectedItem = true;
            }
            jQuery("#" + this._itemButtons[i]).prop("checked", hasGroup);
            item.set_queryData(this._queryData);
        }

        // if nothing is selected and is rendered as radio buttons we selected the first option
        if (!hasSelectedItem && !this._allowMultipleSelection) {
            $find(this._items[0]).set_selected(true);
            jQuery("#" + this._itemButtons[0]).prop("checked", true);
        }
    },

    applyChanges: function () {
        var operation = this._disabled ? "removeGroup" : "applyChanges";
        for (var i = 0, l = this._items.length; i < l; i++) {
            var item = $find(this._items[i]);
            item[operation]();
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    onLoad: function () {

    },

    buttonClicked: function (e) {
        var btnId = e.target.id;
        var k = jQuery.inArray(btnId, this._itemButtons);
        if (k > -1) {
            if (this._allowMultipleSelection) {
                var item = $find(this._items[k]);
                item.set_selected(!item.get_selected());
            }
            else {
                for (var i = 0, length = this._items.length; i < length; i++) {
                    var item = $find(this._items[i]);
                    item.set_selected(i == k);
                }
            }
            dialogBase.resizeToContent();
        }
    },

    /* --------------------------------- private methods --------------------------------- */
    _disable: function (disabled) {
        for (var i = 0, l = this._itemButtons.length; i < l; i++) {
            var btnId = this._itemButtons[i];
            if (disabled) {
                jQuery("#" + btnId).attr("disabled", true);
                jQuery(this.element).find("label[for=" + btnId + "]").addClass(this._disabledTextClass);
            }
            else {
                jQuery("#" + btnId).removeAttr("disabled");
                jQuery(this.element).find("label[for=" + btnId + "]").removeClass(this._disabledTextClass);
            }
        }
        for (var i = 0, l = this._items.length; i < l; i++) {
            var item = $find(this._items[i]);
            if (disabled) {
                item.displayContainer(false, true);
            }
            else if (item.get_selected()) {
                item.displayContainer(true, true);
            }
        }
    },

    _setCulture: function (culture) {
        if (culture) {
            for (var i = 0, l = this._items.length; i < l; i++) {
                var item = $find(this._items[i]);
                if (item && item._selectorResultView) {
                    item._selectorResultView.set_culture(culture);
                }
            }
        }
    },

    /* --------------------------------- properties -------------------------------------- */

    get_items: function () {
        return this._items;
    },
    set_items: function (value) {
        this._items = value;
    },

    get_itemButtons: function () {
        return this._itemButtons;
    },
    set_itemButtons: function (value) {
        this._itemButtons = value;
    },

    get_queryData: function () {
        return this._queryData;
    },
    set_queryData: function (value) {
        var queryData;
        if (value) {
            if (Telerik.Sitefinity.Web.UI.QueryData.isInstanceOfType(value)) {
                queryData = value;
            }
            else {
                queryData = new Telerik.Sitefinity.Web.UI.QueryData(value);
            }
        }
        if (!queryData)
            queryData = new Telerik.Sitefinity.Web.UI.QueryData();
        this._queryData = queryData;
        this.refreshUI();
    },

    get_allowMultipleSelection: function () {
        return this._allowMultipleSelection;
    },
    set_allowMultipleSelection: function (value) {
        this._allowMultipleSelection = value;
    },

    get_disabledTextClass: function () {
        return this._disabledTextClass;
    },
    set_disabledTextClass: function (value) {
        this._disabledTextClass = value;
    },

    get_disabled: function () {
        return this._disabled;
    },
    set_disabled: function (value) {
        this._disabled = value;
        //TODO this method is called lots of time - during control init and during dialog setup (ipelovski)
        this._disable(value);
    },

    // Returns the selected filter selector items
    get_selectedItems: function () {
        var result = [];
        for (var i = 0, length = this._items.length; i < length; i++) {
            var item = $find(this._items[i]);
            if (item.get_selected()) {
                result.push(item);
            }
        }
        return result;
    },

    // Specifies the ui culture to be used by the control
    set_uiCulture: function (value) {
        this._uiCulture = value;
    },

    // Gets the ui culture used by the control
    get_uiCulture: function () {
        return this._uiCulture;
    },

    // Sets the culture culture to be used by the control
    set_culture: function (value) {
        this._culture = value;
        this._setCulture(value);
    },

    // Gets the culture used by the control
    get_culture: function () {
        return this._culture;
    }

}
Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelector', Sys.UI.Control);
