Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.CommandPanel = function(element) {
    Telerik.Sitefinity.Web.UI.Backend.CommandPanel.initializeBase(this, [element]);
    this._name = null;
    this._selectedCssClass = null;
    this._selectedItem = null;
    this._items = null;
    this._selectedElement = null;
    this._commandElements = null;
    this._oldClass = null;
}

Telerik.Sitefinity.Web.UI.Backend.CommandPanel.prototype = {
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Backend.CommandPanel.callBaseMethod(this, "initialize");
        if (this._commandElements != null) {
            this._commandElements = this._commandElements.split(",");
        }
        if (this._items != null) {
            this._items = Sys.Serialization.JavaScriptSerializer.deserialize(this._items);
        }
        commandPanels[this._name] = this;
        if (this._selectedItem != -1) {
            this.execute(this._selectedItem);
        }
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.Backend.CommandPanel.callBaseMethod(this, "dispose");
    },
    set_name: function(value) {
        if (this._name !== value) {
            this._name = value;
            this.raisePropertyChanged("name");
        }
    },
    get_name: function() {
        return this._name;
    },
    set_selectedCssClass: function(value) {
        if (this._selectedCssClass !== value) {
            this._selectedCssClass = value;
            this.raisePropertyChanged("selectedCssClass");
        }
    },
    get_selectedCssClass: function() {
        return this._selectedCssClass;
    },
    set_selectedItem: function(value) {
        if (this._selectedItem !== value) {
            this._selectedItem = value;
            this.raisePropertyChanged("selectedItem");
        }
    },
    get_selectedItem: function() {
        return this._selectedItem;
    },
    set_items: function(value) {
        if (this._items !== value) {
            this._items = value;
            this.raisePropertyChanged("items");
        }
    },
    get_items: function() {
        return this._items;
    },
    set_commandElements: function(value) {
        if (this._commandElements !== value) {
            this._commandElements = value;
            this.raisePropertyChanged("commandElements");
        }
    },
    get_commandElements: function() {
        return this._commandElements;
    },
    execute: function(index) {
        this.selectElement(index);
        var cmd = this._items[index];
        eval(cmd.ClientFunction);
    },
    selectElement: function(index) {
        if (this._selectedElement != null)
            this._selectedElement.className = this._oldClass;

        this._selectedElement = document.getElementById(this._commandElements[index]);
        if (this._selectedElement.className != this._selectedCssClass) {
            this._oldClass = this._selectedElement.className;
        }
        this._selectedElement.className = this._selectedCssClass;
        this._selectedItem = index;
    }
};

Telerik.Sitefinity.Web.UI.Backend.CommandPanel.registerClass("Telerik.Sitefinity.Web.UI.Backend.CommandPanel", Telerik.Web.UI.RadWebControl);
var commandPanels = [];

