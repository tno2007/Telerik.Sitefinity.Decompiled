﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField.initializeBase(this, [element]);

    this._element = element;

    this._noParent = null;
    this._selectParent = null;
    this._parentLibrarySelector = null;
     
    this._radioButtonsClickDelegate = null;
    this._providerName = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField.prototype = {
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField.callBaseMethod(this, "initialize");

        this._radioButtonsClickDelegate = Function.createDelegate(this, this._radioButtonsClick);
        $addHandler(this.get_noParent(), "click", this._radioButtonsClickDelegate);
        $addHandler(this.get_selectParent(), "click", this._radioButtonsClickDelegate);
    },

    dispose: function () {
        /*  this is the place to unbind/dispose the event handlers created in the initialize method */

        if (!this._radioButtonsClickDelegate) {
            if (this.get_noParent()) {
                $removeHandler(this.get_noParent(), "click", this._radioButtonsClickDelegate);
            }

            if (this.get_selectParent()) {
                $removeHandler(this.get_selectParent(), "click", this._radioButtonsClickDelegate);
            }

            delete this._radioButtonsClickDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    reset: function () {
        this.set_value(null);
        this.get_parentLibrarySelector().reset();
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField.callBaseMethod(this, "reset");
    },

    add_hasParentChanged: function (delegate) {
        this.get_events().addHandler('hasParentChanged', delegate);
    },
    remove_hasParentChanged: function (delegate) {
        this.get_events().removeHandler('hasParentChanged', delegate);
    },

    /* --------------------------------- event handlers ---------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    _radioButtonsClick: function () {
        this._updateVisibility();
        this._raiseHasParentChanged(this.get_selectParent().checked);
    },

    _updateVisibility: function () {
        jQuery(this.get_parentLibrarySelector().get_element()).toggle(this.get_selectParent().checked);
    },

    _raiseHasParentChanged: function (hasParent) {
        var handler = this.get_events().getHandler("hasParentChanged");
        var eventArgs = {
            get_hasParent: function () {
                return hasParent;
            }
        };
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    /* --------------------------------- properties -------------------------------------- */

    get_noParent: function () {
        return this._noParent;
    },
    set_noParent: function (value) {
        this._noParent = value;
    },

    get_selectParent: function () {
        return this._selectParent;
    },
    set_selectParent: function (value) {
        this._selectParent = value;
    },

    get_parentLibrarySelector: function () {
        return this._parentLibrarySelector;
    },
    set_parentLibrarySelector: function (value) {
        this._parentLibrarySelector = value;
    },

    get_value: function () {
        if ((this.get_selectParent()).checked) {
            return this.get_parentLibrarySelector().get_value();
        }
        else {
            return "00000000-0000-0000-0000-000000000000";
        }
    },
    set_value: function (value) {
        if (this.get_parentLibrarySelector()) {
            this.get_parentLibrarySelector().set_value(value);
            if (value == null || value == "" || value == "00000000-0000-0000-0000-000000000000") {
                this.get_noParent().checked = true;
            }
            else {
                this.get_selectParent().checked = true;
            }
            this._updateVisibility();
            this._raiseHasParentChanged(this.get_selectParent().checked);
        }

        this._value = value;
    },
    // Passes the provider to the control
    set_providerName: function (value) {
        this._providerName = value;

        if (this.get_parentLibrarySelector()) {
            this.get_parentLibrarySelector().rebind(value);
        }
    },

    // Gets the provider from the control
    get_providerName: function () {
        return this._providerName;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider);