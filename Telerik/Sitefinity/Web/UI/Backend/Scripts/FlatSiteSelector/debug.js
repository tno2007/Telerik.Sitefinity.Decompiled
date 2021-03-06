Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector = function (element) {
    //private properties (retrieved from server, inaccessible)
    this._currentSiteId = null;
    this._selectedSite = null;
    this._allSitesElementId = null;
    this._siteSelectorVisible = false;
    this._moreSitesMenuVisible = false;
    this._moreSitesMenu = null;
    this._rptSitesList = null;
    this._allSitesDoneSelectingDelegate = null;
    this._allSitesCancelSelectingDelegate = null;
    this._allSitesDoneSelectingButton = null;
    this._allSitesCancelSelectingButton = null;
    this._allSitesDialog = null;
    this._showAllSitesSelectorDelegate = null;
    this._dataBindingDelegate = null;
    this._allSitesLink = null;
    this._allSitesSelector = null;
    this._dataItems = {};

    //internal properties
    this._thisElement = null;

    Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------

    initialize: function () {
        /// <summary>Initialization handler.</summary>
        Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector.callBaseMethod(this, "initialize");

        this._thisElement = jQuery(this.get_element());

        if (this._allSitesLink) {
            this._showAllSitesSelectorDelegate = Function.createDelegate(this, this._showAllSitesSelector);
            $addHandler(this._allSitesLink, "click", this._showAllSitesSelectorDelegate);
        }

        if (this._allSitesDoneSelectingButton) {
            this._allSitesDoneSelectingDelegate = Function.createDelegate(this, this._allSitesDoneSelecting);
            $addHandler(this._allSitesDoneSelectingButton, "click", this._allSitesDoneSelectingDelegate);
        }

        if (this._allSitesCancelSelectingButton) {
            this._allSitesCancelSelectingDelegate = Function.createDelegate(this, this._allSitesCancelSelecting);
            $addHandler(this._allSitesCancelSelectingButton, "click", this._allSitesCancelSelectingDelegate);
        }

        this._allSitesDialog = jQuery(this._allSites).dialog({
            autoOpen: false,
            modal: true,
            width: 410,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexL"
            }
        });

        //register events
        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        // prevent memory leaks

        jQuery(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        /// <summary>Disposal handler.</summary>
        // Clean up events
        Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector.callBaseMethod(this, "dispose");
    },

    onload: function () {
        /// <summary>Fires when the selector is loaded.</summary>
        if (this._dataItems) {
            this._dataItems = JSON.parse(this._dataItems);
        }

        if (this._siteSelectorVisible) {
            jQuery("body").addClass("sfHasSiteSelector");
            jQuery(this._thisElement).find(".allSitesItem").attr("id", this._allSitesElementId);
            jQuery(this._thisElement).find(".allSitesItem .sfProviderOut").attr("name", this._allSitesElementId);
            this.selectSite(this._currentSiteId);

            this._initializeSiteSelect();

            if (this._moreSitesMenu && this._moreSitesMenuVisible) {
                jQuery(this._moreSitesMenu).clickMenu()
            }
        }
        else {
            jQuery("body").removeClass("sfHasProviderSelector");
        }

        this._onLoadHandler();
    },

    // ------------------------------------- Event handlers -------------------------------------

    add_onSiteSelected: function (delegate) {
        /// <summary>Adds a handler to when a provider is selected.</summary>
        /// <param name="delegate">The event handler to add.</param>
        this.get_events().addHandler('onSiteSelected', delegate);
    },

    remove_onSiteSelected: function (delegate) {
        /// <summary>Removes a handler to when a provider is selected.</summary>
        /// <param name="delegate">The event handler to remove.</param>
        this.get_events().removeHandler('onSiteSelected', delegate);
    },

    _siteSelectedHandler: function (site) {
        /// <summary>Event handler to when a provider is selected.</summary>
        /// <param name="providerItem">The selected provider item.</param>
        var h = this.get_events().getHandler('onSiteSelected');
        if (h) h(this, site);
    },

    add_onLoad: function (delegate) {
        /// <summary>Adds a handler to when a provider is selected.</summary>
        /// <param name="delegate">The event handler to add.</param>
        this.get_events().addHandler('onLoad', delegate);
    },

    remove_onLoad: function (delegate) {
        /// <summary>Removes a handler to when a provider is selected.</summary>
        /// <param name="delegate">The event handler to remove.</param>
        this.get_events().removeHandler('onLoad', delegate);
    },

    _onLoadHandler: function () {
        /// <summary>Event handler to when a provider is selected.</summary>
        /// <param name="providerItem">The selected provider item.</param>
        var h = this.get_events().getHandler('onLoad');
        if (h) h(this);
    },

    // ------------------------------------- Public methods -------------------------------------

    selectSite: function (siteId) {
        this._selectedSite = this._dataItems[siteId];

        this._thisElement.find('li.sfProvider').removeClass('sfSel');
        var selected = jQuery("#" + siteId + ".sfProvider");
        selected.addClass("sfSel");
        var last = this._thisElement.find('ul.sfProviderSelector > li').last();

        if (selected.length > 0) {
            if (selected.parents(".moreSitesMenuWrp").length > 0) {
                this._swapElements(selected[0], last[0]);
            }
        }
        else if (this._selectedSite){
            last.attr("id", siteId).addClass("sfSel");
            jQuery("a.sfProviderOut", last).attr("name", siteId);
            jQuery(".sfProviderIn", last).html(this._selectedSite.Name.htmlEncode());
        }

        this._siteSelectedHandler(this._selectedSite);
    },

    // ------------------------------------- Private methods -------------------------------------

    _initializeSiteSelect: function () {
        var that = this;
        this._thisElement.find('a.sfProviderOut').click(function (sender, args) {
            if (this.name != that._selectedSiteId) {
                that.selectSite(this.name);
            }
            return false;
        });
    },

    _swapElements: function (elm1, elm2) {
        var parent1, next1,
            parent2, next2;

        parent1 = elm1.parentNode;
        next1 = elm1.nextSibling;
        parent2 = elm2.parentNode;
        next2 = elm2.nextSibling;

        parent1.insertBefore(elm2, next1);
        parent2.insertBefore(elm1, next2);
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    _dataBinding: function (sender, args) {
        var that = this;
        if (args && args._dataItem && args._dataItem.Items) {
            args._dataItem.Items = args._dataItem.Items.filter(function (element, index, array) {
                return !that._isHidden(element['Name'])
            });
        }
    },
    _showAllSitesSelector: function () {
        this._allSitesDialog.dialog("open");
        jQuery(this._allSitesDialog).parent().find(".ui-dialog-titlebar").hide();
        this._dialogScrollToTop(this._allSitesDialog);
    },

    _allSitesDoneSelecting: function () {
        var selectedItems = this.get_allSitesSelector().get_selectedItems();
        if (selectedItems) {
            var selectedItem = selectedItems[0];
            if (selectedItem && selectedItem['Name']) {
                var siteId = selectedItem['Id'];
                var siteName = selectedItem['Name'];
                if (siteId) {
                    this.selectSite(siteId);
                }
            }
        }
        this._allSitesDialog.dialog("close");
    },

    _allSitesCancelSelecting: function () {
        this._allSitesDialog.dialog("close");
        this.get_allSitesSelector().cleanUp();
    },

    // ------------------------------------- Public accessors -------------------------------------
    get_rptSitesList: function () {
        return this._rptSitesList;
    },

    set_rptSitesList: function (value) {
        this._rptSitesList = value;
    },

    get_moreSitesMenu: function () {
        return this._moreSitesMenu;
    },

    set_moreSitesMenu: function (value) {
        if (this._moreSitesMenu != value) {
            this._moreSitesMenu = value;
        }
    },

    get_allSitesLink: function () {
        return this._allProvidersLink;
    },
    set_allSitesLink: function (value) {
        if (this._allSitesLink != value) {
            this._allSitesLink = value;
        }
    },

    get_allSites: function () {
        return this._allSites;
    },

    set_allSites: function (value) {
        if (this._allSites != value) {
            this._allSites = value;
        }
    },

    get_allSitesSelector: function () {
        return this._allSitesSelector;
    },
    set_allSitesSelector: function (value) {
        if (this._allSitesSelector != value) {
            this._allSitesSelector = value;
        }
    },

    get_allSitesDoneSelectingButton: function () {
        return this._allSitesDoneSelectingButton;
    },
    set_allSitesDoneSelectingButton: function (value) {
        if (this._allSitesDoneSelectingButton != value) {
            this._allSitesDoneSelectingButton = value;
        }
    },

    get_allSitesCancelSelectingButton: function () {
        return this._allSitesCancelSelectingButton;
    },
    set_allSitesCancelSelectingButton: function (value) {
        if (this._allSitesCancelSelectingButton != value) {
            this._allSitesCancelSelectingButton = value;
        }
    },

};
Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.FlatSiteSelector', Sys.UI.Control);