Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.SiteSelector = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.SiteSelector.initializeBase(this, [element]);

    this._thisElement = null;
    this._currentSiteLabel = null;
    this._sitesMenu = null;
    this._removeSiteContextLink = null;
    this._allSitesLink = null;
    this._customizeLink = null;
    this._showAllSitesSelectorDelegate = null;
    this._showCustomizeSitesSelectorDelegate = null;
    this._baseServiceUrl = null;
    this._mainMenuSites = null;
    this._siteIdParamKey = null;
    this._selectedSite = null;

    this._allSites = null;
    this._allSitesDialog = null;
    this._allSitesSelector = null;
    this._allSitesDoneSelectingButton = null;
    this._allSitesCancelSelectingButton = null;
    this._allSitesDoneSelectingDelegate = null;
    this._allSitesCancelSelectingDelegate = null;

    this._customizeSites = null;
    this._customizeSitesDialog = null;
    this._customizeSitesSelector = null;
    this._customizeSitesDoneSelectingButton = null;
    this._customizeSitesCancelSelectingButton = null;
    this._customizeSitesDoneSelectingDelegate = null;
    this._customizeSitesCancelSelectingDelegate = null;
    this._siteSelectedDelegate = null;
    this._preventDefaultBehavior = null;
    this._sitesDropDown = null;
}

Telerik.Sitefinity.Web.UI.Backend.SiteSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.SiteSelector.callBaseMethod(this, "initialize");

        this._thisElement = $(this.get_element());

        this._initializeSiteClick();

        if (this._sitesMenu) {
            jQuery(this._sitesMenu).clickMenu();
        }

        if (this._allSitesLink) {
            this._showAllSitesSelectorDelegate = Function.createDelegate(this, this._showAllSitesSelector);
            $addHandler(this._allSitesLink, "click", this._showAllSitesSelectorDelegate);
        }

        if (this._customizeLink) {
            this._showCustomizeSitesSelectorDelegate = Function.createDelegate(this, this._showCustomizeSitesSelector);
            $addHandler(this._customizeLink, "click", this._showCustomizeSitesSelectorDelegate);
        }

        if (this._allSitesDoneSelectingButton) {
            this._allSitesDoneSelectingDelegate = Function.createDelegate(this, this._allSitesDoneSelecting);
            $addHandler(this._allSitesDoneSelectingButton, "click", this._allSitesDoneSelectingDelegate);
        }

        if (this._allSitesCancelSelectingButton) {
            this._allSitesCancelSelectingDelegate = Function.createDelegate(this, this._allSitesCancelSelecting);
            $addHandler(this._allSitesCancelSelectingButton, "click", this._allSitesCancelSelectingDelegate);
        }

        if (this._customizeSitesDoneSelectingButton) {
            this._customizeSitesDoneSelectingDelegate = Function.createDelegate(this, this._customizeSitesDoneSelecting);
            $addHandler(this._customizeSitesDoneSelectingButton, "click", this._customizeSitesDoneSelectingDelegate);
        }

        if (this._customizeSitesCancelSelectingButton) {
            this._customizeSitesCancelSelectingDelegate = Function.createDelegate(this, this._customizeSitesCancelSelecting);
            $addHandler(this._customizeSitesCancelSelectingButton, "click", this._customizeSitesCancelSelectingDelegate);
        }

        if (!this._preventDefaultBehavior) {
            this._siteSelectedDelegate = Function.createDelegate(this, this._changeSite);
            this.add_onSiteSelected(this._siteSelectedDelegate);
        }

        if (this._sitesDropDown) {
            var that = this;
            $(this._sitesDropDown).change(function () {
                var rootId = $(this).val();
                var siteId = $('option:selected', this).attr('SiteId');
                that._siteSelectedHandler({ 'RootNodeId': rootId, 'SiteId': siteId });
            });
        }

        this._initializeDialogs();

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.SiteSelector.callBaseMethod(this, "dispose");

        if (this._showAllSitesSelectorDelegate) {
            if (this._allSitesLink) {
                $removeHandler(this._allSitesLink, "click", this._showAllSitesSelectorDelegate);
            }
            delete this._showAllSitesSelectorDelegate;
        }

        if (this._showCustomizeSitesSelectorDelegate) {
            if (this._customizeLink) {
                $removeHandler(this._customizeLink, "click", this._showCustomizeSitesSelectorDelegate);
            }
            delete this._showCustomizeSitesSelectorDelegate;
        }

        if (this._allSitesDoneSelectingDelegate) {
            if (this._allSitesDoneSelectingButton) {
                $removeHandler(this._allSitesDoneSelectingButton, "click", this._allSitesDoneSelectingDelegate);
            }
            delete this._allSitesDoneSelectingDelegate;
        }

        if (this._allSitesCancelSelectingButton) {
            if (this._allSitesCancelSelectingButton) {
                $removeHandler(this._allSitesCancelSelectingButton, "click", this._allSitesCancelSelectingButton);
            }
            delete this._allSitesCancelSelectingButton;
        }

        if (this._customizeSitesDoneSelectingDelegate) {
            if (this._customizeSitesDoneSelectingButton) {
                $removeHandler(this._customizeSitesDoneSelectingButton, "click", this._customizeSitesDoneSelectingDelegate);
            }
            delete this._customizeSitesDoneSelectingDelegate;
        }

        if (this._customizeSitesCancelSelectingDelegate) {
            if (this._customizeSitesCancelSelectingButton) {
                $removeHandler(this._customizeSitesCancelSelectingButton, "click", this._customizeSitesCancelSelectingDelegate);
            }
            delete this._customizeSitesCancelSelectingDelegate;
        }

        if (this._siteSelectedDelegate) {
            this.remove_onSiteSelected(this._siteSelectedDelegate);
            delete this._siteSelectedDelegate;
        }
    },

    setSelectedSiteInMenu: function (siteId) {
        var that = this;
        this._thisElement.find('a.sfSiteLink').removeClass("sfSel");
        if (siteId) {
            this._thisElement.find('a.sfSiteLink[name="' + siteId + '"]').addClass("sfSel");
        } else {
            jQuery(this._removeSiteContextLink).addClass("sfSel");
        }

        // close the menu
    },

    toggleSitesDropdownMenu: function () {
        jQuery(this._element).find(".sfSitesMenuToggle").click();
    },

    changeSelectedSite: function (siteId, siteName) {
        this._selectedSite = siteId;
        if (this._currentSiteLabel) {
            $(this._currentSiteLabel).text(siteName);
        }
    },

    // ------------------------------------- Event handlers -------------------------------------

    add_onSiteSelected: function (delegate) {
        this.get_events().addHandler('onSiteSelected', delegate);
    },

    remove_onSiteSelected: function (delegate) {
        this.get_events().removeHandler('onSiteSelected', delegate);
    },

    _siteSelectedHandler: function (args) {
        var h = this.get_events().getHandler('onSiteSelected');
        if (h) h(this, args);
    },

    // ------------------------------------- Private methods -------------------------------------

    _initializeSiteClick: function () {
        var that = this;
        this._thisElement.find('a.sfSiteLink').click(function (sender, args) {
            that.changeSelectedSite(sender.target.name, $(sender.target).text());
            that._siteSelectedHandler({ 'SiteId': that._selectedSite });
            return false;
        });
    },

    _initializeDialogs: function () {
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
        this._customizeSitesDialog = jQuery(this._customizeSites).dialog({
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
    },

    _showAllSitesSelector: function () {
        this.get_allSitesSelector().dataBind();
        this._allSitesDialog.dialog("open");
        jQuery(this._allSitesDialog).parent().find(".ui-dialog-titlebar").hide();
        this._dialogScrollToTop(this._allSitesDialog);
    },

    _showCustomizeSitesSelector: function () {
        this.get_customizeSitesSelector().dataBind();
        this._customizeSitesDialog.dialog("open");
        jQuery(this._customizeSitesDialog).parent().find(".ui-dialog-titlebar").hide();
        this._customizeSitesSelector.selectItemsInternal(this._mainMenuSites);
        this._dialogScrollToTop(this._customizeSitesDialog);
    },

    _allSitesDoneSelecting: function () {
        var selectedItems = this.get_allSitesSelector().get_selectedItems();
        if (selectedItems) {
            var selectedItem = selectedItems[0];
            if (selectedItem) {
                this._selectedSite = selectedItem['Id'];
                if (this._currentSiteLabel) {
                    $(this._currentSiteLabel).text(selectedItem['Name']);
                }
                this._siteSelectedHandler({ 'SiteId': this._selectedSite });
            }
        }
        this._allSitesDialog.dialog("close");
    },

    _allSitesCancelSelecting: function () {
        this._allSitesDialog.dialog("close");
        this.get_allSitesSelector().cleanUp();
    },

    _customizeSitesDoneSelecting: function () {
        var selectedItems = this.get_customizeSitesSelector().get_selectedItems();
        if (selectedItems) {
            var data = [];
            for (i = 0; i < selectedItems.length; i++) {
                data.push(selectedItems[i]['Id'])
            }
            $.ajax({
                type: 'PUT',
                url: this._baseServiceUrl + "locateinmainmenu/",
                contentType: "application/json",
                processData: false,
                data: JSON.stringify(data),
                success: function (result, args) {
                    window.location.reload(true);
                }
            });
        }
        this._customizeSitesDialog.dialog("close");
    },

    _customizeSitesCancelSelecting: function () {
        this._customizeSitesDialog.dialog("close");
        this.get_customizeSitesSelector().cleanUp();
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    _changeSite: function (sender, args) {
        var siteId = args.SiteId;

        var anchor = '#';
        var urlhash = '';

        //add url param
        var url = window.location.href;

        // Get URL to '#' if any and append it.
        // TODO: there is a URL component that can handle such operations.
        var anchorIndex = url.indexOf(anchor);
        if (anchorIndex > 0) {
            urlhash = url.substring(anchorIndex, url.length);
            url = url.substring(0, anchorIndex);
        }

        var qIndex = url.lastIndexOf("?");
        if (qIndex > 0) {
            var query = url.substring(qIndex + 1);
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(query);

            //clear provider parameter from the URL
            var index = queryString.keys.indexOf('provider');
            if (index > -1) {
                queryString.keys.splice(index, 1);
            }

            //clear lang parameter from the URL
            index = queryString.keys.indexOf('lang');
            if (index > -1) {
                queryString.keys.splice(index, 1);
            }

            var baseUrl = url.substring(0, qIndex);
            //clear returnUrl parameter from the URL
            index = queryString.keys.indexOf('returnurl');
            if (index > -1) {
                var parser = document.createElement('a');
                parser.href = url;
                baseUrl = parser.protocol + "//" + parser.host + "/" + queryString.params['returnurl'];
                queryString.keys.splice(index, 1);
            }

            if (!siteId) {
                index = queryString.keys.indexOf(this._siteIdParamKey);
                queryString.keys.splice(index, 1);
            }
            else {
                queryString.set(this._siteIdParamKey, siteId);
            }
            
            window.location.href = baseUrl + queryString.toString(true) + urlhash;
        }
        else {
            if (!siteId) {
                window.location.href = url + urlhash;
            }
            else {
                window.location.href = String.format('{0}?{1}={2}', url, this._siteIdParamKey, siteId) + urlhash;
            }
        }
    },

    // ------------------------------------- Properties -------------------------------------

    get_currentSiteLabel: function () {
        return this._currentSiteLabel;
    },
    set_currentSiteLabel: function (value) {
        this._currentSiteLabel = value;
    },
    get_thisElement: function () {
        return this._thisElement;
    },
    set_thisElement: function (value) {
        this._thisElement = value;
    },
    get_sitesMenu: function () {
        return this._sitesMenu;
    },
    set_sitesMenu: function (value) {
        this._sitesMenu = value;
    },
    get_removeSiteContextLink: function () {
        return this._removeSiteContextLink;
    },
    set_removeSiteContextLink: function (value) {
        this._removeSiteContextLink = value;
    },
    get_allSitesLink: function () {
        return this._allSitesLink;
    },
    set_allSitesLink: function (value) {
        this._allSitesLink = value;
    },
    get_customizeLink: function () {
        return this._customizeLink;
    },
    set_customizeLink: function (value) {
        this._customizeLink = value;
    },
    get_allSites: function () {
        return this._allSites;
    },
    set_allSites: function (value) {
        this._allSites = value;
    },
    get_allSitesDoneSelectingButton: function () {
        return this._allSitesDoneSelectingButton;
    },
    set_allSitesDoneSelectingButton: function (value) {
        this._allSitesDoneSelectingButton = value;
    },
    get_allSitesCancelSelectingButton: function () {
        return this._allSitesCancelSelectingButton;
    },
    set_allSitesCancelSelectingButton: function (value) {
        this._allSitesCancelSelectingButton = value;
    },
    get_allSitesSelector: function () {
        return this._allSitesSelector;
    },
    set_allSitesSelector: function (value) {
        this._allSitesSelector = value;
    },
    get_customizeSites: function () {
        return this._customizeSites;
    },
    set_customizeSites: function (value) {
        this._customizeSites = value;
    },
    get_customizeSitesDoneSelectingButton: function () {
        return this._customizeSitesDoneSelectingButton;
    },
    set_customizeSitesDoneSelectingButton: function (value) {
        this._customizeSitesDoneSelectingButton = value;
    },
    get_customizeSitesCancelSelectingButton: function () {
        return this._customizeSitesCancelSelectingButton;
    },
    set_customizeSitesCancelSelectingButton: function (value) {
        this._customizeSitesCancelSelectingButton = value;
    },
    get_customizeSitesSelector: function () {
        return this._customizeSitesSelector;
    },
    set_customizeSitesSelector: function (value) {
        this._customizeSitesSelector = value;
    },
    get_baseServiceUrl: function () {
        return this._baseServiceUrl;
    },
    set_baseServiceUrl: function (value) {
        this._baseServiceUrl = value;
    },
    get_mainMenuSites: function () {
        return this._mainMenuSites;
    },
    set_mainMenuSites: function (value) {
        this._mainMenuSites = value;
    },
    get_siteIdParamKey: function () {
        return this._siteIdParamKey;
    },
    set_siteIdParamKey: function (value) {
        this._siteIdParamKey = value;
    },
    get_selectedSite: function () {
        return this._selectedSite;
    },
    set_selectedSite: function (value) {
        this._selectedSite = value;
    },
    get_preventDefaultBehavior: function () {
        return this._preventDefaultBehavior;
    },
    set_preventDefaultBehavior: function (value) {
        this._preventDefaultBehavior = value;
    },
    get_sitesDropDown: function () {
        return this._sitesDropDown;
    },
    set_sitesDropDown: function (value) {
        this._sitesDropDown = value;
    }
};
Telerik.Sitefinity.Web.UI.Backend.SiteSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.SiteSelector', Sys.UI.Control);