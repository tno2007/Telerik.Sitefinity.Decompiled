/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField.initializeBase(this, [element]);
    this._element = element;
    this.EMPTY_GUID = "00000000-0000-0000-0000-000000000000";
    this._originalServiceUrl = null;
    this._providerName = null;
    this._binderItemCommandDelegate = null;
    this._binderDataBoundDelegate = null;
    this._locationsBinder = null;
    this._toggleLocationsListDelegate = null;
    this._locationsListExpander = null;
    this._locationsListWrapper = null;
    this._fieldWrapper = null;
    this._dataItem = null;
    this._refreshButton = null;
    this._refreshLocationsDelegate = null;
}

Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField.callBaseMethod(this, "initialize");

        if (this._binderItemCommandDelegate == null) {
            this._binderItemCommandDelegate = Function.createDelegate(this, this._binderItemCommand);
        }
        this._locationsBinder.add_onItemCommand(this._binderItemCommandDelegate);

        if (this._binderDataBoundDelegate == null) {
            this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        }
        this._locationsBinder.add_onDataBound(this._binderDataBoundDelegate);

        if (this._locationsListExpander) {
            this._toggleLocationsListDelegate = Function.createDelegate(this, this._toggleLocationsList);
            $addHandler(this._locationsListExpander, "click", this._toggleLocationsListDelegate);
        }
        if (this._refreshButton) {
            this._refreshLocationsDelegate = Function.createDelegate(this, this._refreshLocations);
            $addHandler(this._refreshButton, "click", this._refreshLocationsDelegate);
        }

    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField.callBaseMethod(this, "dispose");

        if (this._binderItemCommandDelegate != null) {
            this._locationsBinder.remove_onItemCommand(this._binderItemCommandDelegate);
            delete this._binderItemCommandDelegate;
        }

        if (this._binderDataBoundDelegate != null) {
            this._locationsBinder.remove_onDataBound(this._binderDataBoundDelegate);
            delete this._binderDataBoundDelegate;
        }

        if (this._toggleLocationsListDelegate) {
            if (this._locationsListExpander) {
                $removeHandler(this._locationsListExpander, "click", this._toggleDefaultFieldsDelegate);
            }
            delete this._toggleLocationsListDelegate;
        }

        if (this._refreshLocationsDelegate) {
            if (this._refreshButton) {
                $removeHandler(this._locationsListExpander, "click", this._refreshLocationsDelegate);
            }
            delete this._refreshLocationsDelegate;
        }
    },


    /* --------------------  public methods ----------- */

    reset: function () {
        jQuery(this.get_locationsListWrapper()).hide();
        this._dataItem = null;
    },

    // Gets the culture to use when visualizing a content.
    get_culture: function () {
        return this._locationsBinder.get_culture();
    },
    // Sets the culture to use when visualizing a content.
    set_culture: function (culture) {
        this._locationsBinder.set_culture(culture)
    },

    // Gets the UI culture to use when visualizing a content.
    get_uiCulture: function () {
        return this._locationsBinder.get_uiCulture();
    },

    // Sets the UI culture to use when visualizing a content.
    set_uiCulture: function (culture) {
        this._locationsBinder.set_uiCulture(culture);
        this.dataBind();
    },

    /* -------------------- events -------------------- */

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    /* -------------------- event handlers ------------ */

    // Handles the item command of the existing taxa binder.
    _binderItemCommand: function (sender, eventArgs) {
    },

    // Handles the data bound event of the existing taxa binder.
    _binderDataBound: function (sender, eventArgs) {
        var binderTarget = jQuery(this.get_locationsBinder().GetTarget());
        var noItemsPanel = jQuery(this.get_locationsListWrapper()).children(".sfNoItemsPanel").get(0);
        var dataItem = eventArgs.get_dataItem();
        if (dataItem.TotalCount > 0) {
            binderTarget.show();
            if (noItemsPanel)
                jQuery(noItemsPanel).hide();
        }
        else {
            binderTarget.hide();
            if (noItemsPanel)
                jQuery(noItemsPanel).show();
        }
    },

    _toggleLocationsList: function (sender, args) {
        var jQueryElement = jQuery(this.get_locationsListWrapper());
        var state = jQueryElement.css("display");
        if (state == "none") {
            this.dataBind();
            jQueryElement.show();
            jQuery(this._fieldWrapper).addClass("sfExpandedForm")
        }
        else {
            jQueryElement.hide();
            jQuery(this._fieldWrapper).removeClass("sfExpandedForm")
        }
        //IE FIX: Explorer calls window.onbeforeunload when hyperlink (<a>)  with href="javascript:..." is clicked
        //Returning false solves the issue.
        return false;
    },

    _refreshLocations: function (sender, args) {
        this.dataBind();
        //IE FIX: Explorer calls window.onbeforeunload when hyperlink (<a>)  with href="javascript:..." is clicked
        //Returning false solves the issue.
        return false;
    },

    /* -------------------- public methods ----------- */
    dataBind: function () {
        if (this._dataItem) {
            var actualItem = this._dataItem;
            if (this._dataItem.hasOwnProperty('Item')) {
                var context = this._dataItem;
                actualItem = this._dataItem.Item;
                var itemType = this._dataItem.ItemType;
                
                var itemId = null;
                if (actualItem.Id && actualItem.Id !== this.EMPTY_GUID) {
                    itemId = actualItem.Id;
                }
                else if (context.ItemId) {
                    //The item will not has id when it is duplicated.
                    //In this case the original item's id will be stored in the context.
                    itemId = context.ItemId;
                }
                else {
                    itemId = this.EMPTY_GUID;
                }

                var itemUrl = String.format("{0}/{1}", this.get_originalServiceUrl(), itemId);
                this.get_locationsBinder().set_serviceBaseUrl(itemUrl);
                this._locationsBinder.get_urlParams()["itemType"] = itemType;
                this._locationsBinder.get_urlParams()["provider"] = this.get_providerName();

                this._locationsBinder.DataBind();
            }
        }
    },


    /* -------------------- properties ---------------- */

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        if (value.Item.Id && value.Item.Id != this.EMPTY_GUID) {
            jQuery(this.get_fieldWrapper()).show();
        }
        else {
            jQuery(this.get_fieldWrapper()).hide();
        }
        this._dataItem = value;
    },

    get_locationsBinder: function () {
        return this._locationsBinder;
    },

    set_locationsBinder: function (value) {
        this._locationsBinder = value;
    },

    get_locationsListExpander: function () {
        return this._locationsListExpander;
    },
    set_locationsListExpander: function (value) {
        this._locationsListExpander = value;
    },

    get_locationsListWrapper: function () {
        return this._locationsListWrapper;
    },
    set_locationsListWrapper: function (value) {
        this._locationsListWrapper = value;
    },
    get_refreshButton: function () {
        return this._locationsListWrapper;
    },
    set_refreshButton: function (value) {
        this._refreshButton = value;
    },
    
    get_originalServiceUrl: function() {
        return this._originalServiceUrl;
    },
    set_originalServiceUrl: function(value) {
        this._originalServiceUrl = value;
    },

    set_providerName: function (value) {
        this._providerName = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    
    get_fieldWrapper: function() {
        return this._fieldWrapper;
    },
    set_fieldWrapper: function(value) {
        this._fieldWrapper = value;
    }

};

Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ContentLocationInfoField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext, Telerik.Sitefinity.Web.UI.Fields.ICommandField, Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl, Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider);