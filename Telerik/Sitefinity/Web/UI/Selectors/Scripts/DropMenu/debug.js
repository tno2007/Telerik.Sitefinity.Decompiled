Type.registerNamespace("Telerik.Sitefinity.Web.UI.Selectors");

/* DropMenu class */
Telerik.Sitefinity.Web.UI.Selectors.DropMenu = function(element) {
    this._selectedTextElementId = null;
    this._selectedValueElementId = null;
    this._optionsMenuElementId = null;
    this._selectedItemChangedDelegate = null;
    this._element = element;
    this._selectedText = null;
    this._selectedValue = null;
    this._selectedItemCssClass = null;
    Telerik.Sitefinity.Web.UI.Selectors.DropMenu.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Selectors.DropMenu.prototype = {
    add_onSelectedItemChanged: function(delegate) {
        this.get_events().addHandler('onSelectedItemChanged', delegate);
    },
    remove_onSelectedItemChanged: function(delegate) {
        this.get_events().removeHandler('onSelectedItemChanged', delegate);
    },
    initialize: function() {
        if (this._selectedItemChangedDelegate === null) {
            this._selectedItemChangedDelegate = Function.createDelegate(this, this._selectedItemChangedHandler);
        }

        Telerik.Sitefinity.Web.UI.Selectors.DropMenu.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        if (this._selectedItemChangedDelegate) {
            delete this._selectedItemChangedDelegate;
        }
        Telerik.Sitefinity.Web.UI.Selectors.DropMenu.callBaseMethod(this, 'dispose');
    },
    _selectedItemChangedHandler: function(text, value) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs(text, value);
        var h = this.get_events().getHandler('onSelectedItemChanged');
        if (h) h(this, eventArgs);
    },
    OnMenuItemClicking: function(sender, args) {
        var item = args.get_item();
        if (item.get_level() > 0) {
            this.SelectNewValue(item);
            sender.close();
            args.set_cancel(true);
        }
    },
    SelectNewValue: function(item) {
        var cssClass = this._selectedItemCssClass;
        $(this._element).find('.' + cssClass).each(function() {
            $(this).removeClass(cssClass);
        });
        $(item.get_element()).addClass(cssClass);
        this._selectedText = item.get_text();
        this._selectedValue = item.get_value();
        $('#' + this._selectedTextElementId).html(this._selectedText);
        this._selectedItemChangedHandler(this._selectedText, this._selectedValue);
    },
    GetSelectedText: function() {
        return $(this._selectedTextElementId).text();
    },
    GetSelectedValue: function() {
        return $(this._selectedValueElementId).val();
    },
    get_selectedTextElementId: function() {
        return this._selectedTextElementId;
    },
    set_selectedTextElementId: function(value) {
        if (this._selectedTextElementId != value) {
            this._selectedTextElementId = value;
            this.raisePropertyChanged('selectedTextElementId');
        }
    },
    get_selectedValueElementId: function() {
        return this._selectedValueElementId;
    },
    set_selectedValueElementId: function(value) {
        if (this._selectedValueElementId != value) {
            this._selectedValueElementId = value;
            this.raisePropertyChanged('selectedValueElementId');
        }
    },
    get_optionsMenuElementId: function() {
        return this._optionsMenuElementId;
    },
    set_optionsMenuElementId: function(value) {
        if (this._optionsMenuElementId != value) {
            this._optionsMenuElementId = value;
            this.raisePropertyChanged('optionsMenuElementId');
        }
    },
    get_selectedText: function() {
        return this._selectedText;
    },
    set_selectedText: function(value) {
        if (this._selectedText != value) {
            this._selectedText = value;
            this.raisePropertyChanged('selectedText');
        }
    },
    get_selectedValue: function() {
        return this._selectedValue;
    },
    set_selectedValue: function(value) {
        if (this._selectedValue != value) {
            this._selectedValue = value;
            this.raisePropertyChanged('selectedValue');
        }
    },
    get_selectedItemCssClass: function() {
        return this._selectedItemCssClass;
    },
    set_selectedItemCssClass: function(value) {
        if (this._selectedItemCssClass != value) {
            this._selectedItemCssClass = value;
            this.raisePropertyChanged('selectedItemCssClass');
        }
    }
};

Telerik.Sitefinity.Web.UI.Selectors.DropMenu.registerClass('Telerik.Sitefinity.Web.UI.Selectors.DropMenu', Sys.UI.Control);

/* Selector item event args class */
Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs = function(text, value) {
    this._text = text;
    this._value = value;
    Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs.prototype = {
    // set up and tear down
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs.callBaseMethod(this, 'dispose');
    },
    get_text: function() {
        return this._text;
    },
    get_value: function() {
        return this._value;
    }
};

Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs.registerClass('Telerik.Sitefinity.Web.UI.Selectors.ItemEventArgs', Sys.Component);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();