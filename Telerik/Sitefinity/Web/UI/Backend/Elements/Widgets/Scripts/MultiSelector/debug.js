Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

// ------------------------------------------------------------------------
// Class MultiSelector
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector = function(element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector.initializeBase(this, [element]);

    // IWidget support fields
    this._name = null;
    this._cssClass = null;
    this._isSeparator = null;
    this._wrapperTagId = null;
    this._wrapperTagName = null;

    // own fields
    this._selectors = null;
    this._commandName = null;
    this._pageLoadDelegate = Function.createDelegate(this, this._pageLoad);
    this._itemToggledDelegate = Function.createDelegate(this, this._itemToggled);
    this._itemTogglingDelegate = Function.createDelegate(this, this._itemToggling);
    this._invalidateItemsDelegate = Function.createDelegate(this, this._invalidateItems);
    this._itemSelectionChangedDelegate = Function.createDelegate(this, this._itemSelectionChanged);
    this._itemSelectorInitializedDelegate = Function.createDelegate(this, this._itemSelectorInitialized);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector.prototype = {
    // ------------------------------------------------------------------------
    // Initialize & dispose
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector.callBaseMethod(this, "initialize");

        this._selectors = Sys.Serialization.JavaScriptSerializer.deserialize(this._selectors);
        var index = 0;
        for (var selectorName in this._selectors) {
            if (this._selectors.hasOwnProperty(selectorName)) {
                var selector = this._selectors[selectorName];
                var item = new Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem(selector);

                item.add_toggled(this._itemToggledDelegate);
                item.add_toggling(this._itemTogglingDelegate);
                item.add_otherItemsInvalidated(this._invalidateItemsDelegate);
                item.add_selectionChanged(this._itemSelectionChangedDelegate);
                item.add_selectorInitialized(this._itemSelectorInitializedDelegate);

                item.set_index(index);
                item.set_visible(index == 0);

                this._selectors[selectorName] = item;
                index++;
            }
        }

        Sys.Application.add_load(this._pageLoadDelegate);
    },
    _pageLoad: function() {
        for (var selectorName in this._selectors) {
            if (this._selectors.hasOwnProperty(selectorName)) {
                // hook-up internal events
                this._selectors[selectorName].initialize();
            }
        }
    },
    dispose: function() {
        for (var selectorName in this._selectors) {
            if (!this._selectors.hasOwnProperty(selectorName))
                continue;
            var item = this._selectors[selectorName];
            item.remove_toggled(this._itemToggledDelegate);
            item.remove_toggling(this._itemTogglingDelegate);
            item.remove_otherItemsInvalidated(this._invalidateItemsDelegate);
            item.remove_selectionChanged(this._itemSelectionChangedDelegate);
            item.remove_selectorInitialized(this._itemSelectorInitializedDelegate);
            this._selectors[selectorName].dispose();
        }
        Sys.Application.remove_load(this._pageLoadDelegate);
        delete this._selectors;
        delete this._pageLoadDelegate;
        delete this._itemToggledDelegate;
        delete this._itemTogglingDelegate;
        delete this._invalidateItemsDelegate;
        delete this._itemSelectionChangedDelegate;
        delete this._itemSelectorInitializedDelegate;

        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector.callBaseMethod(this, "dispose");
    },

    // ------------------------------------------------------------------------
    // Events
    // ------------------------------------------------------------------------
    add_command: function(delegate) {
        /// <summary>Raised when a selector has changed its selection.</summary>
        Sys.Observer.addEventHandler(this, "command", delegate);
    },
    remove_command: function(delegate) {
        Sys.Observer.removeEventHandler(this, "command", delegate);
    },
    add_activated: function(delegate) {
        /// <summary>Raised when a selector has been made visible (activated).</summary>
        Sys.Observer.addEventHandler(this, "activated", delegate);
    },
    remove_activated: function(delegate) {
        Sys.Observer.removeEventHandler(this, "activated", delegate);
    },
    add_deactivated: function(delegate) {
        /// <summary>Raised to indicated that a selecto</summary>
        Sys.Observer.addEventHandler(this, "deactivated", delegate);
    },
    remove_deactivated: function(delegate) {
        Sys.Observer.removeEventHandler(this, "deactivated", delegate);
    },
    add_selectorInitialized: function(delegate) {
        /// <summary>Happens the very first time a selector is visible.</summary>
        /// <remarks>Event args is SelectorItem</remarks>
        Sys.Observer.addEventHandler(this, "selectorInitialized", delegate);
    },
    remove_selectorInitialized: function(delegate) {
        Sys.Observer.removeEventHandler(this, "selectorInitialized", delegate);
    },


    // ------------------------------------------------------------------------
    // Public functions
    // ------------------------------------------------------------------------
    findSelectorByName: function(name) {
        /// <summary>Find selector by name</summary>
        /// <param name="name">Name of the selector to search for</param>
        /// <returns>Selector component or null.</returns>
        if (this._selectors.hasOwnProperty(name)) {
            return this._selectors[name].get_selector();
        }
        else {
            return null;
        }
    },
    getSelectorItemByName: function(name) {
    /// <summary>Get one of the listed selector item</summary>
    /// <param name="name">Name of the selector to search for</param>
    /// <returns>Selector component or null.</returns>    
        if (this._selectors.hasOwnProperty(name)) {
            return this._selectors[name];
        }
        else {
            return null;
        }
    },
    
    activeSelector: function() {
        /// <summary>Get active selector or null if none is selected.</summary>
        /// <returns>SelectorItem or null.</returns>
        for (var selectorName in this._selectors) {
            if (this._selectors.hasOwnProperty(selectorName)) {
                var selector = this._selectors[selectorName];
                if (selector.get_visible()) {
                    return selector;
                }
            }
        }
        return null;
    },

    getSelectedValuesFromAllSelectors: function() {
        var selections = new Array();
        for (var selectorName in this._selectors) {
            if (this._selectors.hasOwnProperty(selectorName)) {
                var selector = this._selectors[selectorName];
                selections = selections.concat(selector.get_selector().getSelectedItems());
            }
        }
        return selections;
    },

    setSelectedKeys: function(selectedKeys) {
        for (var selectorName in this._selectors) {
            if (this._selectors.hasOwnProperty(selectorName)) {
                var selector = this._selectors[selectorName];
                selector.set_selectedKeys(selectedKeys);
            }
        }
    },

    resetSelectorsPageIndexes: function () {
        for (var selectorName in this._selectors) {
            if (this._selectors.hasOwnProperty(selectorName)) {
                var selector = this._selectors[selectorName].get_selector();
                selector._grid.get_masterTableView().set_currentPageIndex(0);
            }
        }
    },

    // ------------------------------------------------------------------------
    // Handlers
    // ------------------------------------------------------------------------
    _itemToggling: function(sender, args) {
        // won't hide the item if it is visible, it should be hidden if other invisible item is clicked
        if (sender.get_visible()) {
            args.set_cancel(true);
        }
    },
    _itemToggled: function(sender, args) {
        // we come here only if sender was previously hidden

        // notify sender that is now visible
        Sys.Observer.raiseEvent(this, "activated", this._getActivationArgs(sender));

        // find the previously visible item and notify it that it is now hidden
        var myName = sender.get_name();
        for (var itemName in this._selectors) {
            if (!this._selectors.hasOwnProperty(itemName))
                continue;
            var item = this._selectors[itemName];
            if (item.get_name() != myName && item.get_visible()) {
                item.hide();
                Sys.Observer.raiseEvent(this, "deactivated", this._getActivationArgs(item));
                break;
            }
        }
    },
    _invalidateItems: function(sender, args) {
        var namesToInvalidate = args.get_invalidatedItemNames();
        var iter = namesToInvalidate.length;

        while (iter--) {
            var name = namesToInvalidate[iter];
            if (this._selectors.hasOwnProperty(name)) {
                var itemToInvalidate = this._selectors[name];
                itemToInvalidate.invalidate();
            }
        }
    },
    _itemSelectionChanged: function(sender, args) {
        args._selectorItems = this._selectors;
        var commandArgs = new Telerik.Sitefinity.CommandEventArgs(this._commandName, args);
        Sys.Observer.raiseEvent(this, "command", commandArgs);
    },
    _itemSelectorInitialized: function(sender, args) {
        Sys.Observer.raiseEvent(this, "selectorInitialized", sender);
    },

    // ------------------------------------------------------------------------
    // Utility functions
    // ------------------------------------------------------------------------
    _raiseEvent: function(eventName, eventArgs) {
        Sys.Observer.raiseEvent(this, eventArgs);
        return eventArgs;
    },
    // if called with 2 arguments: propertyName, value
    // if called with 3 arguments: propertyName, fieldName, value
    _setPropertyIfChanged: function() {
        var propertyName = arguments[0];
        var value = arguments.length == 3 ? arguments[2] : arguments[1];
        var fieldName = arguments.length == 3 ? arguments[1] : null;

        if (!propertyName || !this.hasOwnProperty(propertyName)) return;
        if (!fieldName)
            fieldName = "_" + propertyName[0] + propertyName.slice(1);

        var currentValue = this[fieldName];
        if (currentValue != value) {
            this[fieldName] = value;
            this.raisePropertyChanged(propertyName);
        }
    },
    _getActivationArgs: function(item) {
        return new Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActivationEventArgs(item);
    },

    // ------------------------------------------------------------------------
    // Inherited properties from IWidget
    // ------------------------------------------------------------------------
    get_name: function() { return this._name; },
    set_name: function(value) { this._setPropertyIfChanged("name", value); },
    get_cssClass: function() { return this._cssClass; },
    set_cssClass: function(value) { this._setPropertyIfChanged("cssClass", value); },
    get_isSeparator: function() { return this._isSeparator; },
    set_isSeparator: function(value) { this._setPropertyIfChanged("isSeparator", value); },
    get_wrapperTagId: function() { return this._wrapperTagId; },
    set_wrapperTagId: function(value) { this._setPropertyIfChanged("wrapperTagId", value); },
    get_wrapperTagName: function() { return this._wrapperTagName; },
    set_wrapperTagName: function(value) { this._setPropertyIfChanged("wrapperTagName", value); },
    // ------------------------------------------------------------------------
    // Own Properties
    // ------------------------------------------------------------------------
    get_commandName: function() { return this._commandName; },
    set_commandName: function(value) { this._setPropertyIfChanged("commandName", value); }
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.MultiSelector", Sys.UI.Control, Telerik.Sitefinity.UI.IWidget, Telerik.Sitefinity.UI.ICommandWidget);


// ------------------------------------------------------------------------
// --                                                                    --
// ------------------------------------------------------------------------


// ------------------------------------------------------------------------
// Class SelectorItem
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem = function(data) {
    this._name = data.Name;
    this._section = data.SectionClientId ? $get(data.SectionClientId) : null;
    this._title = data.TitleClientId ? $get(data.TitleClientId) : null;
    this._body = data.BodyClientId ? $get(data.BodyClientId) : null;
    this._selectorId = data.SelectorComponentId;
    this._selectedClass = data.SelectedCssClass;
    this._notSelectedClass = data.NotSelectedCssClass;
    this._invalidatedItemNames = data.InvalidatesNames || [];

    this._dataLoaded = false;
    this._isSelectorInitialized = false;
    this._selectedKeys = null;

    this._titleClickedDelegate = Function.createDelegate(this, this._titleClicked);
    this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChanged);
    this._itemDeselectedDelegate = Function.createDelegate(this, this._itemDeselected);
    this._selectorInitializedDelegate = Function.createDelegate(this, this._selectorInitialized);
    this._selectorBinderDataBoundDelegate = Function.createDelegate(this, this._selectorBinderDataBound);

    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem.prototype = {
    // ------------------------------------------------------------------------
    // Set-up and tear-down
    // ------------------------------------------------------------------------
    initialize: function() {
        this._selector = $find(this._selectorId);
        delete this._selectorId;
        
        var binder = $find(this._selector._binderId);
        if (binder) {
            binder.set_clearSelectionOnRebind(false);
        }

        this._selector.add_initialized(this._selectorInitializedDelegate);
        this._selector.add_itemSelected(this._selectionChangedDelegate);
        this._selector.add_itemDeselected(this._itemDeselectedDelegate);

        $addHandler(this._title, "click", this._titleClickedDelegate);

        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem.callBaseMethod(this, "initialize");
    },
    dispose: function() {
        if (this._section) {
            $clearHandlers(this._section);
            delete this._section;
        }
        if (this._title) {
            $clearHandlers(this._title);
            delete this._title;
        }
        if (this._body) {
            $clearHandlers(this._body);
            delete this._body;
        }
        if (this._selector) {
            this._selector.dispose();
            delete this._selector;
        }
        delete this._titleClickedDelegate;
        delete this._selectionChangedDelegate;
        delete this._selectorInitializedDelegate;
        delete this._selectorBinderDataBoundDelegate;
        delete this._itemDeselectedDelegate;

        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem.callBaseMethod(this, "dispose");
    },

    // ------------------------------------------------------------------------
    // Events
    // ------------------------------------------------------------------------
    add_toggling: function(delegate) {
        /// <summary>Gives the user an option to cancel the nextcomming toggled event.</summary>
        Sys.Observer.addEventHandler(this, "toggling", delegate);
    },
    remove_toggling: function(delegate) {
        Sys.Observer.removeEventHandler(this, "toggling", delegate);
    },
    add_toggled: function(delegate) {
        /// <summary>Indicates that the item has been toggled.</summary>
        Sys.Observer.addEventHandler(this, "toggled", delegate);
    },
    remove_toggled: function(delegate) {
        Sys.Observer.removeEventHandler(this, "toggled", delegate);
    },
    add_otherItemsInvalidated: function(delegate) {
        /// <summary>Indicates that the selection has changed and the user has requested the invalidation of other items (selectors)</summary>
        Sys.Observer.addEventHandler(this, "otherItemsInvalidated", delegate);
    },
    remove_otherItemsInvalidated: function(delegate) {
        Sys.Observer.removeEventHandler(this, "otherItemsInvalidated", delegate);
    },
    add_selectionChanged: function(delegate) {
        /// <summary>Bubbles up the event from the wrapped selector</summary>
        Sys.Observer.addEventHandler(this, "selectionChanged", delegate);
    },
    remove_selectionChanged: function(delegate) {
        Sys.Observer.removeEventHandler(this, "selectionChanged", delegate);
    },
    add_itemDeselected: function (delegate) {
        /// <summary>Bubbles up the event from the wrapped selector</summary>
        Sys.Observer.addEventHandler(this, "itemDeselected", delegate);
    },
    remove_itemDeselected: function (delegate) {
        Sys.Observer.removeEventHandler(this, "itemDeselected", delegate);
    },
    add_selectorInitialized: function(delegate) {
        /// <summary>Happens the very first time a selector is visible.</summary>
        Sys.Observer.addEventHandler(this, "selectorInitialized", delegate);
    },
    remove_selectorInitialized: function(delegate) {
        Sys.Observer.removeEventHandler(this, "selectorInitialized", delegate);
    },

    // ------------------------------------------------------------------------
    // Public functions
    // ------------------------------------------------------------------------
    hide: function() {
        /// <summary>Hide body. Does not raise events.</summary>  
        /// <remarks>Use this only if you have a good reason not to use <c>toggle</c></remarks>      
        this._changeClass(this._title, this._selectedClass, this._notSelectedClass);
        Sys.UI.DomElement.setVisible(this._body, false);
    },
    show: function() {
        /// <summary>Show body. Does not raise events.</summary>
        /// <remarks>Use this only if you have a good reason not to use <c>toggle</c></remarks>
        if (!this._dataLoaded && this._isSelectorInitialized) {
            this._selector.bindSelector();
            this._dataLoaded = true;
        }

        this._changeClass(this._title, this._notSelectedClass, this._selectedClass);
        Sys.UI.DomElement.setVisible(this._body, true);
    },
    toggle: function() {
        /// <summary>Toggle visibility. Raises events.</summary>     
        /// <remarks>Use this instead of <c>show</c> or <c>hide</c> unless you know what you are doing.</remarks>   
        var args = new Sys.CancelEventArgs();
        Sys.Observer.raiseEvent(this, "toggling", args);
        if (args.get_cancel() == false) {
            if (this.get_visible()) {
                this.hide();
            }
            else {
                this.show();
            }
            Sys.Observer.raiseEvent(this, "toggled");
        }
    },
    invalidate: function() {
        ///<summary>Call this to invalidate the selector item.</summary>
        if (this.get_visible()) {
            this._selector.bindSelector();
        }
        else {
            this._dataLoaded = false;
        }
    },

    // ------------------------------------------------------------------------
    // Utilities
    // ------------------------------------------------------------------------
    _escapeHtml: function(text) {
        var div = document.createElement('div');
        var text = document.createTextNode(str);
        div.appendChild(text);
        return div.innerHTML;
    },
    _removeClassIfExisting: function(element, className) {
        if (element && className && Sys.UI.DomElement.containsCssClass(element, className)) {
            Sys.UI.DomElement.removeCssClass(element, className);
        }
    },
    _addClassIfNotExisting: function(element, className) {
        if (element && className && !Sys.UI.DomElement.containsCssClass(element, className)) {
            Sys.UI.DomElement.addCssClass(element, className);
        }
    },
    _changeClass: function(element, oldClass, newClass) {
        this._removeClassIfExisting(element, oldClass);
        this._addClassIfNotExisting(element, newClass);
    },

    // ------------------------------------------------------------------------
    // Handlers
    // ------------------------------------------------------------------------
    _titleClicked: function() {
        this.toggle();
    },
    _selectionChanged: function(sender, args) {
        var selection = args ? (args.length ? args : [args]) : [];

        // bubble up selection event
        var bubbleArgs = new Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionChangedEventArgs(this, selection);
        Sys.Observer.raiseEvent(this, "selectionChanged", bubbleArgs);

        // if by selecting items in this selector we have invalidated others, inform the MultiSelector container
        if (this._invalidatedItemNames.length > 0) {
            var invalidationArgs = new Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.InvalidatingEventArgs(this, selection);
            Sys.Observer.raiseEvent(this, "otherItemsInvalidated", invalidationArgs);
        }
    },
    _itemDeselected: function (sender, args) {
        Sys.Observer.raiseEvent(this, "itemDeselected", args);
    },
    _selectorInitialized: function() {
    this._isSelectorInitialized = true;
        this._selector.add_binderDataBound(this._selectorBinderDataBoundDelegate);
        if (!this._dataLoaded && this.get_visible()) {
            this._selector.bindSelector();
            this._dataLoaded = true;
            Sys.Observer.raiseEvent(this, "selectorInitialized");
        }
    },

    _selectorBinderDataBound: function(sender, args) {
        if (this._selectedKeys != null) {
            this.get_selector().set_selectedKeys(this._selectedKeys);
            this._selectedKeys = null;
        }
    },


    // Properties
    // ------------------------------------------------------------------------
    get_titleText: function() {
        return this._title.innerText;
    },
    set_titleText: function(value) {
        this._title.innerHTML = this._escapeHtml(value);
    },
    get_index: function() { return this._index; },
    set_index: function(value) { this._index = value; },
    get_hidden: function() { return !this._get_visible(); },
    set_hidden: function(value) { this.set_visible(value); },
    get_visible: function() { return Sys.UI.DomElement.getVisible(this._body); },
    set_visible: function(value) {
        /// <summary>Sets visibility. Does not raise events.</summary>
        if (value)
            this.show();
        else
            this.hide();
    },
    get_name: function() { return this._name; },
    get_section: function() { return this._section; },
    get_title: function() { return this._title; },
    get_body: function() { return this._body; },
    get_selector: function() { return this._selector; },
    get_invalidatedItemNames: function() { return this._invalidatedItemNames; },
    get_dataLoaded: function() { return this._dataLoaded; },
    set_selectedKeys: function(keys) {
        this._selectedKeys = keys;
        this.get_selector().cleanUp();

        if (this._dataLoaded) {
            this.get_selector().set_selectedKeys(this._selectedKeys);
            this._selectedKeys = null;
        }
    }
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem", Sys.Component);




// ------------------------------------------------------------------------
// Class ActivationEventArgs
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActivationEventArgs = function(item) {
    this._selector = item.get_selector();
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActivationEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActivationEventArgs.prototype = {
    get_selector: function() { return this._selector; }
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActivationEventArgs.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActivationEventArgs", Sys.EventArgs);

// ------------------------------------------------------------------------
// Class InvalidatingEventArgs
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.InvalidatingEventArgs = function(item, selection) {
    this._invalidatedItemNames = item.get_invalidatedItemNames();
    this._selection = selection || [];
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.InvalidatingEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.InvalidatingEventArgs.prototype = {
    get_invalidatedItemNames: function() { return this._invalidatedItemNames; },
    get_selection: function() { return this._selection; }
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.InvalidatingEventArgs.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.InvalidatingEventArgs", Sys.EventArgs);

// ------------------------------------------------------------------------
// Class SelectionChangedEventArgs
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionChangedEventArgs = function(item, selection, allSelectors) {
    this._selectorItems = allSelectors;
    this._source = item;
    this._selection = selection || [];
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionChangedEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionChangedEventArgs.prototype = {
    get_selectorItems: function() {
        /// <summary>Optionally, can contain all selectors in the MultiSelector, including the source.</summary>
        return this._selectorItems; 
    },
    get_selection: function() {
        /// <summary>Get an array of selected items. Guaranteed to be not null.</summary>
        return this._selection; 
    },
    get_source: function() {
        /// <summary>Selector item that originally bubbled the selector event</summary>
        return this._source; 
    }
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionChangedEventArgs.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionChangedEventArgs", Sys.EventArgs);