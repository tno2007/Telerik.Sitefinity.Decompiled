Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.SelectorResultView = function (element) {
    this._binder = null;
    this._openSelector = null;
    this._commandBar = null;
    this._selectorWindow = null;
    this._selector = null;
    this._selectorWrapper = null;
    this._dialog = null;

    this._onLoadDelegate = null;
    this._binderDataBoundDelegate = null;
    this._commandBarCommandDelegate = null;
    this._culture = null;

    this._selectorOpenerId = null;

    Telerik.Sitefinity.Web.UI.SelectorResultView.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.SelectorResultView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.SelectorResultView.callBaseMethod(this, "initialize");

        if (this._commandBarCommandDelegate == null)
            this._commandBarCommandDelegate = Function.createDelegate(this, this.commandBar_Command);
        if (this._commandBar)
            this._commandBar.add_command(this._commandBarCommandDelegate);

        if (this._openSelectorCommandDelegate == null)
            this._openSelectorCommandDelegate = Function.createDelegate(this, this.openSelectorCommand);
        if (this._openSelector) {
            $addHandler(this._openSelector, "click", this._openSelectorCommandDelegate);
        }

        this._onLoadDelegate = Function.createDelegate(this, this.onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        this._dialog = jQuery(this._selectorWrapper).dialog({
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
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.SelectorResultView.callBaseMethod(this, "dispose");

        if (this._onLoadDelegate)
            delete this._onLoadDelegate;
        if (this._binderDataBoundDelegate)
            delete this._binderDataBoundDelegate;

        if (this._commandBarCommandDelegate != null) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }
        if (this._openSelector)
            $clearHandlers(this._openSelector);
        if (this._openSelectorCommandDelegate != null)
            delete this._openSelectorCommandDelegate;

        this._commandBar = null;
    },
    /* --------------------------------- public methods ---------------------------------- */
    // TODO when the control is hidden do not refresh or call services (ipelovski)
    refreshUI: function () {
    },
    validate: function () {
        return true;
    },

    // Gets the selected items from the selector result view.
    get_selectedItems: function () {
        return [];
    },
    // Sets the selected items from the selector result view.
    set_selectedItems: function (value) {
    },

    // Gets the selected values from the selector result view. Used by the QuerySelectorItem to build a collection of QueryItem objects
    get_selectedValues: function () {
        return [];
    },
    // Sets the selected values from the selector result view. Used by the QuerySelectorItem 
    set_selectedValues: function (value) {
    },
    /* --------------------------------- event handlers ---------------------------------- */
    onLoad: function () {
    },
    doneClicked: function () {
    },
    cancelClicked: function () {
    },
    commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case "done":
                if (this.validate()) {
                    this.doneClicked();
                    this._close();
                }
                break;
            case "cancel":
                this.cancelClicked();
                this._close();
                break;
        }
    },
    openSelectorCommand: function () {
        this._bindSelector();
        var oWnd = this._selectorWindow;
        if (oWnd) {
            oWnd.show();
            oWnd.set_visibleTitlebar(true);
            Telerik.Sitefinity.centerWindowHorizontally(oWnd);
        }
        var wrapper = this._selectorWrapper;
        if (wrapper) {
            this._dialog.dialog("open");

            var openerId = this.get_selectorOpenerId();
            if (openerId) {
                jQuery("#" + openerId).hide();
            }
            else {
                jQuery("body > form").hide();
            }

            //jQuery(wrapper).show();
            dialogBase.resizeToContent();
            //jQuery("body").eq(0).addClass("sfOverflowHidden");
        }
    },
    /* --------------------------------- private methods --------------------------------- */
    _bindSelector: function () {
        var selector = this._selector;
        if (typeof selector.get_bindOnLoad === 'function' && !selector.get_bindOnLoad()) {
            if (this._culture) {
                var cultureIsDefined = typeof (selector.get_culture) != "undefined";
                var cultureIsChanged = cultureIsDefined && selector.get_culture() != this._culture;
                if (cultureIsChanged && typeof (selector._setCultures) != "undefined") {
                    selector._setCultures(this._culture);
                }
            }
            else {
                selector.dataBind();
            }
        }
    },
    _close: function (context) {
        var oWnd = this._selectorWindow;
        if (oWnd) {
            oWnd.close(context);
        }
        var wrapper = this._selectorWrapper;
        if (wrapper) {
            //jQuery(wrapper).hide();
            this._dialog.dialog("close");

            var openerId = this.get_selectorOpenerId();
            if (openerId) {
                jQuery("#" + openerId).show();
            }
            else {
                jQuery("body > form").show();
            }

            dialogBase.resizeToContent();
            //jQuery("body").eq(0).removeClass("sfOverflowHidden");
        }
    },
    /* --------------------------------- properties -------------------------------------- */
    get_binder: function () {
        return this._binder;
    },
    set_binder: function (value) {
        this._binder = value;
    },
    get_openSelector: function () {
        return this._openSelector;
    },
    set_openSelector: function (value) {
        this._openSelector = value;
    },
    get_selectorWindow: function () {
        return this._selectorWindow;
    },
    set_selectorWindow: function (value) {
        this._selectorWindow = value;
    },
    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },
    get_selector: function () {
        return this._selector;
    },
    set_selector: function (value) {
        this._selector = value;
    },
    get_selectorWrapper: function () {
        return this._selectorWrapper;
    },
    set_selectorWrapper: function (value) {
        this._selectorWrapper = value;
    },
    get_culture: function () {
        return this._culture;
    },
    set_culture: function (value) {
        this._culture = value;
    },
    get_selectorOpenerId: function () {
        return this._selectorOpenerId;
    },
    set_selectorOpenerId: function (value) {
        this._selectorOpenerId = value;
    }
}

Telerik.Sitefinity.Web.UI.SelectorResultView.registerClass('Telerik.Sitefinity.Web.UI.SelectorResultView', Sys.UI.Control);
