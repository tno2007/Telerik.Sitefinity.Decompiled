Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ------------------------------------------------------------------------
// Class: ProvidersList
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ProvidersList = function(element) {
    Telerik.Sitefinity.Web.UI.ProvidersList.initializeBase(this, [element]);
    this._radComboId = null;
    this._radCombo = null;
    this._providersNamesList = null;
    this._selectedProviderName = null;
}
Telerik.Sitefinity.Web.UI.ProvidersList.prototype = {
    // ------------------------------------------------------------------------
    // Set-up and tear-down
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.ProvidersList.callBaseMethod(this, 'initialize');

        this._providersNamesList = Sys.Serialization.JavaScriptSerializer.deserialize(this._providersNamesList);

        Sys.Application.add_load(Function.createDelegate(this, this._handlePageLoad));
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.ProvidersList.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // Private functions
    // ------------------------------------------------------------------------
    _handlePageLoad: function(sender, args) {
        this._radCombo = $find(this._radComboId);
        this._bindCombo();
        this._radCombo.add_selectedIndexChanging(Function.createDelegate(this, this._handleIndexChanging));
        this._radCombo.add_selectedIndexChanged(Function.createDelegate(this, this._handleIndexChanged));
    },
    _bindCombo: function() {
        this._radCombo.trackChanges();
        var len = this._providersNamesList.length;
        for (var i = 0; i < len; i++) {
            var providerName = this._providersNamesList[i].ProviderName;
            var comboItem = new Telerik.Web.UI.RadComboBoxItem();
            comboItem.set_text(providerName);
            comboItem.set_value(providerName);
            this._radCombo.get_items().add(comboItem);
            if (i == 0) {
                comboItem.select();
                this._selectedProviderName = providerName;
            }
        }
        this._radCombo.commitChanges();
    },
    _handleIndexChanging: function(sender, args) {
        var providerName = args.get_item().get_value();
        if (this._onProviderNameChanging(this._selectedProviderName, providerName).get_cancel()) {
            args.set_cancel(true);
        }
    },
    _handleIndexChanged: function(sender, args) {
        var oldProviderName = this._selectedProviderName;
        this._selectedProviderName = args.get_item().get_value();
        this._onProviderNameChanged(oldProviderName, this._selectedProviderName);
    },
    _onProviderNameChanged: function(oldProviderName, newProviderName) {
        var args = new Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs(oldProviderName, newProviderName);
        var handler = this.get_events().getHandler("providerNameChanged");
        if (handler) {
            handler(this, args);
        }
        return args;
    },
    _onProviderNameChanging: function(oldProviderName, newProviderName) {
        var args = new Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs(oldProviderName, newProviderName);
        var handler = this.get_events().getHandler("providerNameChanging");
        if (handler) {
            handler(this, args);
        }
        return args;
    },

    // ------------------------------------------------------------------------
    // Events
    // ------------------------------------------------------------------------
    add_providerNameChanging: function(handler) {
        this.get_events().addHandler("providerNameChanging", handler);
    },
    remove_providerNameChanging: function(handler) {
        this.get_events().removeHandler("providerNameChanging", handler);
    },
    add_providerNameChanged: function(handler) {
        this.get_events().addHandler("providerNameChanged", handler);
    },
    remove_providerNameChanged: function(handler) {
        this.get_events().removeHandler("providerNameChanged", handler);
    }
}
Telerik.Sitefinity.Web.UI.ProvidersList.registerClass('Telerik.Sitefinity.Web.UI.ProvidersList', Sys.UI.Control);

// ------------------------------------------------------------------------
// Class: ProviderNameChangingEventArgs
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs = function(oldProviderName, newProviderName) {
    this._oldProviderName = oldProviderName;
    this._newProviderName = newProviderName;
    Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up and tear-down
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // Properties
    // ------------------------------------------------------------------------
    get_oldProviderName: function() {
        return this._oldProviderName;
    },
    get_newProviderName: function() {
        return this._newProviderName;
    }
};
Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ProviderNameChangingEventArgs', Sys.CancelEventArgs);

// ------------------------------------------------------------------------
// Class: ProviderNameChangedEventArgs
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs = function(oldProviderName, newProviderName) {
    this._oldProviderName = oldProviderName;
    this._newProviderName = newProviderName;
    Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up and tear-down
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // Properties
    // ------------------------------------------------------------------------
    get_oldProviderName: function() {
        return this._oldProviderName;
    },
    get_newProviderName: function() {
        return this._newProviderName;
    }
};
Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ProviderNameChangedEventArgs', Sys.EventArgs);