﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend");

Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector = function (element) {
    //private properties (retrieved from server, inaccessible)
    this._providersListPanelID = null;
    this._showAllProvidersLink = null;
    this._listedProviders = null;

    //internal properties
    this._listedProviderUniqueId = 0;
    this._providersPanel = null;
    this._providerTemplate = null;
    this._selectedProviderCurrent = null;
    this._defaultProvider = null;
    this._selectedProvider = null;

    //publics
    this._autoHideIfLessThanTwoProviders = null;
    this._hiddenProviderNames = null;

    // event delegates
    this._providerSelectedDelegate = null;

    this._thisElement = null;
    this._moreProvidersMenu = null;
    this._allProvidersLink = null;
    this._allProvidersDoneSelectingButton = null;
    this._allProvidersCancelSelectingButton = null;
    this._allProvidersDialog = null;
    this._showAllProvidersSelectorDelegate = null;
    this._allProvidersDoneSelectingDelegate = null;
    this._allProvidersCancelSelectingDelegate = null;
    this._mainMenuProvidersCount = null;
    this._allProvidersSelector = null;
    this._dataBindingDelegate = null;

    Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector.prototype = {

    // ------------------------------------- Initialization -------------------------------------

    initialize: function () {
        /// <summary>Initialization handler.</summary>
        Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector.callBaseMethod(this, "initialize");
        this._showAllProvidersLink = (this._showAllProvidersLink.toUpperCase() == "TRUE");
        this._listedProviderUniqueId = 0;
        this._listedProviders = Sys.Serialization.JavaScriptSerializer.deserialize(this._listedProviders);
        this._hiddenProviderNames = Sys.Serialization.JavaScriptSerializer.deserialize(this._hiddenProviderNames);

        //register events
        if (this._providerSelectedDelegate === null) {
            this._providerSelectedDelegate = Function.createDelegate(this, this._providerSelectedHandler);
        }

        this._thisElement = $(this.get_element());

        this._initializeProviderClick();

        if (this._moreProvidersMenu) {
            $(this._moreProvidersMenu).clickMenu()
        }

        if (this._allProvidersLink) {
            this._showAllProvidersSelectorDelegate = Function.createDelegate(this, this._showAllProvidersSelector);
            $addHandler(this._allProvidersLink, "click", this._showAllProvidersSelectorDelegate);
        }

        if (this._allProvidersDoneSelectingButton) {
            this._allProvidersDoneSelectingDelegate = Function.createDelegate(this, this._allProvidersDoneSelecting);
            $addHandler(this._allProvidersDoneSelectingButton, "click", this._allProvidersDoneSelectingDelegate);
        }

        if (this._allProvidersCancelSelectingButton) {
            this._allProvidersCancelSelectingDelegate = Function.createDelegate(this, this._allProvidersCancelSelecting);
            $addHandler(this._allProvidersCancelSelectingButton, "click", this._allProvidersCancelSelectingDelegate);
        }

        this._allProvidersDialog = jQuery(this._allProviders).dialog({
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

        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        /// <summary>Disposal handler.</summary>
        // Clean up events
        if (this._providerSelectedDelegate) {
            delete this._providerSelectedDelegate;
        }

        if (this._showAllProvidersSelectorDelegate) {
            if (this._allProvidersLink) {
                $removeHandler(this._allProvidersLink, "click", this._showAllProvidersSelectorDelegate);
            }
            delete this._showAllProvidersSelectorDelegate;
        }

        if (this._allProvidersDoneSelectingDelegate) {
            if (this._allProvidersDoneSelectingButton) {
                $removeHandler(this._allProvidersDoneSelectingButton, "click", this._allProvidersDoneSelectingDelegate);
            }
            delete this._allProvidersDoneSelectingDelegate;
        }

        if (this._allProvidersCancelSelectingDelegate) {
            if (this._allProvidersCancelSelectingButton) {
                $removeHandler(this._allProvidersCancelSelectingButton, "click", this._allProvidersCancelSelectingDelegate);
            }
            delete this._allProvidersCancelSelectingDelegate;
        }

        if (this._dataBindingDelegate) {
            if (this._allProvidersSelector) {
                this._allProvidersSelector.remove_binderDataBinding(this._dataBindingDelegate);
            }
            delete this._dataBindingDelegate;
        }

        Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector.callBaseMethod(this, "dispose");
    },

    onload: function () {
        /// <summary>Fires when the selector is loaded.</summary>
        this._providersPanel = $get(this._providersListPanelID).getElementsByTagName("ul")[0];
        this._providerTemplate = new Sys.UI.Template($('#providerItemTemplate').get(0));
        this.dataBind();

        if (this._selectedProvider) {
            this.selectProviderByName(this._selectedProvider);
        } else {
            // Select the first provider if there are any
            if (this._listedProviders.length > 0) {
                this.selectProviderByName(this._listedProviders[0].ProviderName);
            }
            else {
                this.highlightProviderByName(this._defaultProvider);
            }
        }

        if (this._allProvidersSelector) {
            this._dataBindingDelegate = Function.createDelegate(this, this._dataBinding);
            this._allProvidersSelector.add_binderDataBinding(this._dataBindingDelegate);
        }
    },

    // ------------------------------------- Event handlers -------------------------------------

    add_onProviderSelected: function (delegate) {
        /// <summary>Adds a handler to when a provider is selected.</summary>
        /// <param name="delegate">The event handler to add.</param>
        this.get_events().addHandler('onProviderSelected', delegate);
    },

    remove_onProviderSelected: function (delegate) {
        /// <summary>Removes a handler to when a provider is selected.</summary>
        /// <param name="delegate">The event handler to remove.</param>
        this.get_events().removeHandler('onProviderSelected', delegate);
    },

    _providerSelectedHandler: function (providerItem) {
        /// <summary>Event handler to when a provider is selected.</summary>
        /// <param name="providerItem">The selected provider item.</param>
        var h = this.get_events().getHandler('onProviderSelected');
        if (h) h(this, providerItem);
    },

    // ------------------------------------- Public methods -------------------------------------

    dataBind: function () {
        /// <summary>Binds the listed providers to the list retrieved from the server.</summary>
        for (var curProvider = 0; curProvider < this._listedProviders.length && curProvider < this._mainMenuProvidersCount; curProvider++) {
            if (!this._isHidden(this._listedProviders[curProvider].ProviderName))
                this._listedProviders[curProvider].CliendID = this.addProvider(this._listedProviders[curProvider].ProviderName, this._listedProviders[curProvider].ProviderTitle, this._listedProviders[curProvider].ProviderID);
        }
        for (var curProvider = this._mainMenuProvidersCount; curProvider < this._listedProviders.length; curProvider++) {
            if (!this._isHidden(this._listedProviders[curProvider].ProviderName)) {
                this._listedProviders[curProvider].CliendID = this._thisElement.find('a.sfProviderLink[name="' + this._listedProviders[curProvider].ProviderName + '"]').attr('id');
            }
        }
        var showMe = true;
        if (((this._autoHideIfLessThanTwoProviders) &&
            ((this._showAllProvidersLink) && (this._listedProviders.length < 3)) ||
            ((!this._showAllProvidersLink) && (this._listedProviders.length < 2))))
            showMe = false;
        this.setSelectorDisplay(showMe);
    },

    setSelectorDisplay: function (bShow) {
        if (bShow)
            $("body").addClass("sfHasProviderSelector");
        else
            $("body").removeClass("sfHasProviderSelector");
        this._toggleDisplay(this._element, bShow);
    },

    addProvider: function (providerName, providerTitle, providerID) {
        /// <summary>Appends a provider to the displayed list.</summary>
        /// <param name="providerName">The name of the provider.</param>
        /// <param name="providerTitle">The title of the provider.</param>
        /// <param name="providerID">The ID of the provider.</param>
        this._clickDelegate = Function.createDelegate(this, this._clickProvider);
        var providerInstance = this._providerTemplate.instantiateIn(this._providersPanel, null, null, this._listedProviderUniqueId);
        var providerTitleLabel = $(this._providersPanel).find("#providerTitle" + providerInstance.index).get(0);
        this._setLabelText(providerTitleLabel, providerTitle);
        var instantiatedElement = providerInstance.get("li");
        $addHandler(instantiatedElement, "click", this._clickDelegate);
        this._listedProviderUniqueId++;
        return instantiatedElement.id;
    },

    selectProviderById: function (providerId) {
        /// <summary>Selects one of the listed providers by ID.</summary>
        /// <param name="providerId">The ID of the provider to select.</param>
        for (var curProvider = 0; curProvider < this._listedProviders.length; curProvider++) {
            var curProviderItem = this._listedProviders[curProvider];
            if (curProviderItem.ProviderID == providerId) {
                this._selectedProviderCurrent = curProviderItem;
                this._highlightProvider($get(curProviderItem.CliendID), true);
            }
            else
                this._highlightProvider($get(curProviderItem.CliendID), false);
        }
        this._providerSelectedHandler(this._selectedProviderCurrent);
    },

    selectProviderByName: function (providerName) {
        /// <summary>Selects one of the listed providers by name.</summary>
        /// <param name="providerId">The name of the provider to select.</param>
        for (var curProvider = 0; curProvider < this._listedProviders.length; curProvider++) {
            var curProviderItem = this._listedProviders[curProvider];
            if (curProviderItem.ProviderName == providerName) {
                this._selectedProviderCurrent = curProviderItem;
                this._highlightProvider($get(curProviderItem.CliendID), true);
            }
            else
                this._highlightProvider($get(curProviderItem.CliendID), false);
        }
        this._providerSelectedHandler(this._selectedProviderCurrent);
    },

    getProviderItems: function () {
        /// <summary>Returns all the listed provider items.</summary>
        return this._listedProviders;
    },

    getNumOfListedProvider: function () {
        /// <summary>Returns the number of all the listed provider items.</summary>
        return this._listedProviders.length;
    },

    getSelectedProviderItem: function () {
        /// <summary>Retrieves the currently selected provider object.</summary>
        if ((this._selectedProviderCurrent == null) && (this._listedProviders.length > 0))
            this._selectedProviderCurrent = this._listedProviders[0];
        return this._selectedProviderCurrent;
    },

    highlightProviderByName: function (providerName) {
        for (var curProvider = 0; curProvider < this._listedProviders.length; curProvider++) {
            var curProviderItem = this._listedProviders[curProvider];
            if (curProviderItem.ProviderName == providerName) {
                this._selectedProviderCurrent = curProviderItem;
                this._highlightProvider($get(curProviderItem.CliendID), true);
            }
            else
                this._highlightProvider($get(curProviderItem.CliendID), false);
        }
    },

    setProviderTitle: function (providerName, newTitle) {
        /// <summary>Sets a new title to a specific provider.</summary>
        /// <param name="providerName">The name of the provider to set the title for.</param>
        /// <param name="newTitle">The new title to set.</param>
        for (var curProvider = 0; curProvider < this._listedProviders.length; curProvider++) {
            var curProviderItem = this._listedProviders[curProvider];
            if (curProviderItem.CliendID != null) {
                if (curProviderItem.ProviderName == providerName) {
                    if (($get(curProviderItem.CliendID) != null) && ($($get(curProviderItem.CliendID)).find("label").length > 0))
                        this._setLabelText($($get(curProviderItem.CliendID)).find("label").get(0), newTitle);
                    curProviderItem.providerTitle = newTitle;
                }
            }
        }
    },

    // ------------------------------------- Private methods -------------------------------------

    _isHidden: function (providerName) {
        var hide = false;
        for (var i = 0; i < this._hiddenProviderNames.length; i++) {
            if (providerName == this._hiddenProviderNames[i]) {
                hide = true;
                break;
            }
        }
        return hide;
    },

    _highlightProvider: function (providerElement, bHighlight) {
        /// <summary>Highlights or dehighlights a specific provider in the list.</summary>
        /// <param name="providerElement">The HTML element representing the provider</param>
        /// <param name="bHighlight">Boolean- whether to highlight or dehighlight the provider</param>
        if (bHighlight)
            $(providerElement).addClass("sfSel");
        else
            $(providerElement).removeClass("sfSel");
    },

    _toggleDisplay: function (element, bShow) {
        /// <summary>Shows or hides an element.</summary>
        /// <param name="element">The element to show or hide</param>
        /// <param name="bShow">Boolean- whether to show or hide the control</param>
        element.style.display = ((bShow) ? "inline-block" : "none");
    },

    _clickProvider: function (e) {
        /// <summary>Handles clicking a slecific provider.</summary>
        /// <param name="e">The click event</param>
        var curNode = e.target;
        while ((curNode != null) && (curNode.nodeName.toUpperCase() != "LI"))
            curNode = curNode.parentNode;

        if (curNode != null) {
            var provider = null;
            for (var curProvider = 0; curProvider < this._listedProviders.length; curProvider++) {
                if (this._listedProviders[curProvider].CliendID == curNode.id) {
                    provider = this._listedProviders[curProvider];
                }
            }
            if ((provider != null) && (provider.ProviderName != this._selectedProviderCurrent.ProviderName))
                this.selectProviderByName(provider.ProviderName);
        }
    },

    _setLabelText: function (LabelElement, newText) {
        /// <summary>Sets a text to a label element.</summary>
        /// <param name="LabelElement">The label element.</param>
        /// <param name="newText">The new text to set.</param>
        if (typeof LabelElement.textContent != "undefined")
            LabelElement.textContent = newText;

        if (typeof LabelElement.innerText != "undefined")
            LabelElement.innerText = newText;
    },

    _showAllProvidersSelector: function () {
        this._allProvidersDialog.dialog("open");
        jQuery(this._allProvidersDialog).parent().find(".ui-dialog-titlebar").hide();
        this._dialogScrollToTop(this._allProvidersDialog);
    },

    _allProvidersDoneSelecting: function () {
        var selectedItems = this.get_allProvidersSelector().get_selectedItems();
        if (selectedItems) {
            var selectedItem = selectedItems[0];
            if (selectedItem && selectedItem['Name']) {
                var providerName = selectedItem['Name'];
                if (providerName != this._selectedProviderCurrent.ProviderName) {
                    this.selectProviderByName(providerName);
                }
            }
        }
        this._allProvidersDialog.dialog("close");
    },

    _allProvidersCancelSelecting: function () {
        this._allProvidersDialog.dialog("close");
        this.get_allProvidersSelector().cleanUp();
    },

    _initializeProviderClick: function () {
        var that = this;
        this._thisElement.find('a.sfProviderLink').click(function (sender, args) {
            if (this.name != that._selectedProviderCurrent.ProviderName) {
                that.selectProviderByName(this.name);
            }
            return false;
        });
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

    // ------------------------------------- Public accessors -------------------------------------

    get_autoHideIfLessThanTwoProviders: function () {
        /// <summary>Gets the autoHideIfLessThanTwoProviders value, indicating whether to hide the selector if less than 2 providers are selected.</summary>
        return this._autoHideIfLessThanTwoProviders;
    },

    set_autoHideIfLessThanTwoProviders: function (value) {
        /// <summary>Sets value to the autoHideIfLessThanTwoProviders value, indicating whether to hide the selector if less than 2 providers are selected.</summary>
        /// <param name="value">The new boolean value</param>
        if (this._autoHideIfLessThanTwoProviders != value) {
            this._autoHideIfLessThanTwoProviders = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('autoHideIfLessThanTwoProviders');
        }
    },


    get_selectedProvider: function () {
        return this._selectedProvider;
    },

    set_selectedProvider: function (value) {
        if (this._selectedProvider != value) {
            this._selectedProvider = value;
        }
    },

    get_hiddenProviderNames: function () {
        /// <summary>Gets an array of providers which should not be displayed.</summary>
        return this._hiddenProviderNames;
    },

    set_hiddenProviderNames: function (value) {
        /// <summary>Sets an array of providers which should not be displayed.</summary>
        /// <param name="value">Array of providers which should not be displayed</param>
        if (this._hiddenProviderNames != value) {
            this._hiddenProviderNames = value;
            this.raisePropertyChanged('hiddenProviderNames');
        }
    },

    get_moreProvidersMenu: function () {
        return this._moreProvidersMenu;
    },
    set_moreProvidersMenu: function (value) {
        if (this._moreProvidersMenu != value) {
            this._moreProvidersMenu = value;
        }
    },

    get_allProvidersLink: function () {
        return this._allProvidersLink;
    },
    set_allProvidersLink: function (value) {
        if (this._allProvidersLink != value) {
            this._allProvidersLink = value;
        }
    },

    get_allProviders: function () {
        return this._allProviders;
    },
    set_allProviders: function (value) {
        if (this._allProviders != value) {
            this._allProviders = value;
        }
    },

    get_allProvidersSelector: function () {
        return this._allProvidersSelector;
    },
    set_allProvidersSelector: function (value) {
        if (this._allProvidersSelector != value) {
            this._allProvidersSelector = value;
        }
    },

    get_allProvidersDoneSelectingButton: function () {
        return this._allProvidersDoneSelectingButton;
    },
    set_allProvidersDoneSelectingButton: function (value) {
        if (this._allProvidersDoneSelectingButton != value) {
            this._allProvidersDoneSelectingButton = value;
        }
    },

    get_allProvidersCancelSelectingButton: function () {
        return this._allProvidersCancelSelectingButton;
    },
    set_allProvidersCancelSelectingButton: function (value) {
        if (this._allProvidersCancelSelectingButton != value) {
            this._allProvidersCancelSelectingButton = value;
        }
    },

    get_mainMenuProvidersCount: function () {
        return this._mainMenuProvidersCount;
    },
    set_mainMenuProvidersCount: function (value) {
        if (this._mainMenuProvidersCount != value) {
            this._mainMenuProvidersCount = value;
        }
    },
    enableProviderLinks: function() {
        $(this.get_element()).removeClass("sfDisabled");
    },
    disableProviderLinks: function() {
        $(this.get_element()).addClass("sfDisabled");
    }
};
Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector.registerClass('Telerik.Sitefinity.Web.UI.Backend.FlatProviderSelector', Sys.UI.Control);