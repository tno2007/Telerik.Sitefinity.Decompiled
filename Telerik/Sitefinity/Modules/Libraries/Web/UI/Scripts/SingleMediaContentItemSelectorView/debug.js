Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.initializeBase(this, [element]);

    this._enableSelectedFolderSearch = false;

    this._mediaContentItemsList = null;
    this._mediaContentBinder = null;
    this._searchBox = null;
    this._mediaContentBreadcrumb = null;

    this._parentType = null;
    this._contentType = null;
    this._provider;
    this._mediaContentBinderServiceUrl = null;
    this._mediaContentBinderServiceChildItemsUrl = null;

    this._mediaContentBinderItemSelectDelegate = null;
    this._mediaContentBinderItemDataBoundDelegate = null;
    this._loadDelegate = null;
    this._searchDelegate = null;

    this._lastSelectedElement = null;
    this._bindOnLoad = null;
    this._currentUserId = null;
    this._preSelectedItems = null;
    this._sourceLibraryId = null;

    this._breadcrumbItemClickDelegate = null;
    this._breadcrumbRootItemClickDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.callBaseMethod(this, "initialize");

        this._mediaContentBinderItemSelectDelegate = Function.createDelegate(this, this._mediaContentBinderItemSelect);
        this._mediaContentBinderItemDataBoundDelegate = Function.createDelegate(this, this._mediaContentBinderItemDataBound);
        this._mediaContentBinderDataBoundDelegate = Function.createDelegate(this, this._mediaContentBinderDataBound);

        this._loadDelegate = Function.createDelegate(this, this._load);
        Sys.Application.add_load(this._loadDelegate);

        if (this._searchBox) {
            this._searchDelegate = Function.createDelegate(this, this._search);
            this.get_searchBox().add_manualSearch(this._searchDelegate);
            this.get_searchBox().set_enableManualSearch(true);
        }

        if (this.get_mediaContentBreadcrumb()) {
            this._breadcrumbItemClickDelegate = Function.createDelegate(this, this._breadcrumbItemClickHandler);
            this.get_mediaContentBreadcrumb().add_onItemSelectCommand(this._breadcrumbItemClickDelegate);

            this._breadcrumbRootItemClickDelegate = Function.createDelegate(this, this._breadcrumbRootItemClickHandler);
            this.get_mediaContentBreadcrumb().add_onRootItemSelectCommand(this._breadcrumbRootItemClickDelegate);
        }

        this.get_mediaContentItemsList().set_bindOnSuccess(true);

        this._setupTooltips();

    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.callBaseMethod(this, "dispose");

        if (this._mediaContentBinderItemSelectDelegate) {
            if (this.get_mediaContentBinder()) {
                this.get_mediaContentBinder().remove_onItemSelectCommand(this._mediaContentBinderItemSelectDelegate);
            }
            delete this._mediaContentBinderItemSelectDelegate;
        }
        if (this._mediaContentBinderItemDataBoundDelegate) {
            if (this.get_mediaContentBinder()) {
                this.get_mediaContentBinder().remove_onItemDataBound(this._mediaContentBinderItemDataBoundDelegate);
            }
            delete this._mediaContentBinderItemDataBoundDelegate;
        }
        if (this._mediaContentBinderDataBoundDelegate) {
            if (this.get_mediaContentBinder()) {
                this.get_mediaContentBinder().remove_onDataBound(this._mediaContentBinderDataBoundDelegate);
            }
            delete this._mediaContentBinderDataBoundDelegate;
        }
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._searchDelegate) {
            if (this.get_searchBox()) {
                this.get_searchBox().remove_manualSearch(this._searchDelegate);
            }
            delete this._searchDelegate;
        }
        if (this._breadcrumbItemClickDelegate) {
            if (this.get_mediaContentBreadcrumb()) {
                this.get_mediaContentBreadcrumb().remove_onItemSelectCommand(this._breadcrumbItemClickDelegate);
            }
            delete this._breadcrumbItemClickDelegate;
        }

        this._removeTooltips();
    },

    rebind: function () {
        this._initializeLists();
    },

    // ----------------------------------------------- Public functions -----------------------------------------------

    /* -------------------- events -------------------- */

    add_onItemSelectCommand: function (delegate) {
        this.get_events().addHandler('onItemSelectCommand', delegate);
    },
    remove_onItemSelectCommand: function (delegate) {
        this.get_events().removeHandler('onItemSelectCommand', delegate);
    },

    /* -------------------- event handlers -------------------- */

    _load: function () {
        this._initializeHandlers();
        if (this._bindOnLoad) {
            this._initializeLists();
        }
    },

    _mediaContentBinderItemSelect: function (sender, args) {
        this._manageHighlight(args.get_itemElement());
        this._itemSelectCommandHandler(args);
    },

    _itemSelectCommandHandler: function (dataItem) {
        var itemEventArgs = dataItem;
        var h = this.get_events().getHandler('onItemSelectCommand');
        if (h) h(this, itemEventArgs);
    },

    _mediaContentBinderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();

        if (dataItem.IsFolder) {
            var that = this;

            jQuery(args.get_itemElement()).find(".selectCommand").addClass("sfSmallLibTmb");
            jQuery(args.get_itemElement()).find(".selectCommand").click(function () {
                that._folderClickHandler(dataItem.Id);
            });
        }

        var img = jQuery(args.get_itemElement()).find("img");
        if (img) {
            var width = dataItem.SnapshotWidth ? dataItem.SnapshotWidth : dataItem.Width;
            var height = dataItem.SnapshotHeight ? dataItem.SnapshotHeight : dataItem.Height;
            this._resizeImage(img, width, height, 71);

            if (width == 0 && height == 0)
                jQuery(img).addClass("sfDefaultTmb");
        }

        //HACK: If MediaPlayerMode is set to HTML5, a thumbnail is not generated on video upload
        //and video's width and hight are not set (see VideoThumbnailGenerator.cs, method CreateThumbnail)
        //so we hide "Dimensions: 0x0" from the video tooltip.
        //If CreateHtml5VideoThumbnails method is implemented delete this hack
        var descriptions = jQuery(args.get_itemElement()).find("li");
        if (descriptions) {
            for (var i = 0; i < descriptions.length; i++) {
                var currentDescription = descriptions.eq(i).text();
                if (currentDescription == "Dimensions: 0x0") {
                    descriptions.eq(i).hide();
                    break;
                }
            }
        }
    },

    _mediaContentBinderDataBound: function (sender, args) {
        this._itemsListTooltipManager.repairEdgeElements();
        this._preSelectItems();

        this.get_mediaContentBinder().get_urlParams()["includeSubFoldersItems"] = false;
        this.get_mediaContentBinder().get_urlParams()["excludeFolders"] = false;
    },

    _search: function (sender, args) {
        if (this.get_mediaContentBreadcrumb() && (!this._enableSelectedFolderSearch || !this._selectedFolder)) {
            jQuery(this.get_mediaContentBreadcrumb().get_wrapper()).hide();
        }

        var query = args.get_query(),
            originalQuery = args.get_query();
        var filter = this.get_mediaContentItemsList().get_constantFilter();

        if (filter) {
            if (query != null && query != "") {
                query = String.format("({0}) AND {1}", filter, query);
                if (this._selectedFolder) {
                    var parentFilter;
                    if (this.get_mediaContentItemsList().get_dataItem().FolderTitle != null) {
                        parentFilter = String.format("FolderId == {0}", this._selectedFolder);
                    }
                    else {
                        parentFilter = String.format("Parent.Id == {0}", this._selectedFolder);
                    }
                    query = String.format("{0} AND {1}", query, parentFilter);
                }
            }
            else {
                query = filter;
            }
        }

        this.get_mediaContentBinder().set_filterExpression(query);
        this.get_mediaContentBinder().set_isFiltering(true);
        if (this._selectedFolder && this._enableSelectedFolderSearch && originalQuery && originalQuery.length > 0) {
            this.get_mediaContentBinder().get_urlParams()["includeSubFoldersItems"] = true;
            this.get_mediaContentBinder().get_urlParams()["excludeFolders"] = true;
            this.get_mediaContentBinder().DataBind(this._selectedFolder);
        }
        else {
            this.get_mediaContentBinder().get_urlParams()["includeSubFoldersItems"] = false;
            this.get_mediaContentBinder().get_urlParams()["excludeFolders"] = false;
            this.get_mediaContentBinder().DataBind();
        }
    },

    _selectionChanged: function (sender, args) {
        if (typeof (args.get_commandName) != "undefined") {
            if (this.get_mediaContentBreadcrumb()) {
                this._selectedFolder = "";
                jQuery(this.get_mediaContentBreadcrumb().get_wrapper()).hide();
            }
            switch (args.get_commandName()) {
                case "showRecent":
                    this.get_mediaContentBinder().set_filterExpression('[ShowRecentLiveItems]');
                    this.get_mediaContentBinder().DataBind();
                    break;
                case "showMy":
                    this.get_mediaContentItemsList().applyFilter("Owner == (" + this._currentUserId + ")");
                    break;
                case "showAll":
                    var mediaContentBinderFilter = this.get_mediaContentItemsList().get_constantFilter();
                    this.get_mediaContentBinder().set_filterExpression(mediaContentBinderFilter);
                    this.get_mediaContentBinder().DataBind();
                    break;
            }
        }
        else {
            var selectedItem = args;
            var globalDataKeys = [];
            var binder = this.get_mediaContentBinder();
            binder.set_globalDataKeys(globalDataKeys);
            var itemsList = this.get_mediaContentItemsList();
            var filter = itemsList.get_constantFilter();
            binder.set_filterExpression(filter);
            binder.set_isFiltering(true);
            itemsList.getPager().init(0, binder.get_take());
            itemsList.getPager().set_currentPage(1);

            if (selectedItem != null) {
                this._selectedFolder = selectedItem[0].Id;  
                this._bindWithSpecificItemId(selectedItem[0].Id, binder);
            }
            else {
                binder.DataBind();
            }
        }
        jQuery(this.get_searchBox().get_textBox()).val("");
    },

    _bindWithSpecificItemId: function (itemId, binder) {
        var globalDataKeys = [];
        globalDataKeys["Id"] = itemId;
        binder.set_globalDataKeys(globalDataKeys);
        binder.set_skip(0);
        binder.DataBind(itemId);
        if (this.get_mediaContentBreadcrumb()) {
            jQuery(this.get_mediaContentBreadcrumb().get_wrapper()).show();
            this.get_mediaContentBreadcrumb().set_providerName(this._provider);
            this.get_mediaContentBreadcrumb().dataBind(itemId);
        }
    },

    _commandBarCommandHandler: function (sender, args) {
        //jQuery(this.get_mediaContentBreadcrumb().get_wrapper()).hide();
        //this.get_librarySelector().clearSelection();
        jQuery($get(sender.get_id())).children().removeClass("sfSelected");
        jQuery($get(sender.findItemByCommandName(args.get_commandName()).ButtonClientId)).addClass("sfSelected");
        switch (args.get_commandName()) {
            case "showRecent":
                this.get_mediaContentBinder().set_filterExpression('[ShowRecentLiveItems]');
                this.get_mediaContentBinder().DataBind();
                break;
            case "showMy":
                this.get_mediaContentItemsList().applyFilter("Owner == (" + this._currentUserId + ")");
                break;
            case "showAll":
                var mediaContentBinderFilter = this.get_mediaContentItemsList().get_constantFilter();
                this.get_mediaContentBinder().set_filterExpression(mediaContentBinderFilter);
                this.get_mediaContentBinder().DataBind();
                break;
        }
    },

    _breadcrumbItemClickHandler: function (sender, args) {
        var selectedItem = args;
        var globalDataKeys = [];
        var binder = this.get_mediaContentBinder();
        binder.set_globalDataKeys(globalDataKeys);
        var itemsList = this.get_mediaContentItemsList();
        var filter = itemsList.get_constantFilter();
        binder.set_filterExpression(filter);
        binder.set_isFiltering(true);
        itemsList.getPager().init(0, binder.get_take());
        itemsList.getPager().set_currentPage(1);

        if (selectedItem != null) {
            var key = selectedItem.Id;
            globalDataKeys["Id"] = key;
            binder.set_globalDataKeys(globalDataKeys);
            binder.set_skip(0);
            binder.DataBind(key);
            if (this.get_mediaContentBreadcrumb()) {
                this.get_mediaContentBreadcrumb().set_providerName(this._provider);
                this.get_mediaContentBreadcrumb().dataBind(key);
            }
        }
        else {
            binder.DataBind();
        }
    },

    _breadcrumbRootItemClickHandler: function (sender, args) {
        if (this.get_mediaContentBreadcrumb()) {
            jQuery(this.get_mediaContentBreadcrumb().get_wrapper()).hide();
        }
        var mediaContentBinderFilter = this.get_mediaContentItemsList().get_constantFilter();
        this.get_mediaContentBinder().set_filterExpression(mediaContentBinderFilter);
        this.get_mediaContentBinder().DataBind();
    },

    _folderClickHandler: function (itemId) {
        var globalDataKeys = [];
        var binder = this.get_mediaContentBinder();
        var itemsList = this.get_mediaContentItemsList();
        var filter = itemsList.get_constantFilter();
        binder.set_filterExpression(filter);
        binder.set_isFiltering(true);
        itemsList.getPager().init(0, binder.get_take());
        itemsList.getPager().set_currentPage(1);

        var key = itemId;
        this._selectedFolder = itemId;
        globalDataKeys["Id"] = key;
        binder.set_globalDataKeys(globalDataKeys);
        binder.set_skip(0);
        binder.DataBind(key);
        if (this.get_mediaContentBreadcrumb()) {
            this.get_mediaContentBreadcrumb().set_providerName(this._provider);
            this.get_mediaContentBreadcrumb().dataBind(key);
        }
        //this.get_librarySelector().set_selectedItemId(key);
        //this.get_librarySelector()._updateTreeSelection();
    },

    setPreSelectedItems: function (ids) {
        this._preSelectedItems = ids;
    },

    /* -------------------- private methods -------------------- */

    _initializeHandlers: function () {
        var binder = this.get_mediaContentBinder();
        binder.add_onItemSelectCommand(this._mediaContentBinderItemSelectDelegate);
        binder.add_onItemDataBound(this._mediaContentBinderItemDataBoundDelegate);
        binder.add_onDataBound(this._mediaContentBinderDataBoundDelegate);
    },

    _initializeLists: function () {
        var urlParams = []

        var binder = this.get_mediaContentBinder();

        if (binder) {
            binder.set_serviceBaseUrl(this._mediaContentBinderServiceUrl);
            binder.set_serviceChildItemsBaseUrl(this._mediaContentBinderServiceChildItemsUrl);
            urlParams["itemType"] = this._contentType;
            urlParams["provider"] = this._provider;
            binder.set_provider(this._provider);
            binder.set_urlParams(urlParams);

            if (this.get_sourceLibraryId() && this.get_sourceLibraryId() !== Telerik.Sitefinity.getEmptyGuid()) {
                var itemsList = this.get_mediaContentItemsList();
                var filter = itemsList.get_constantFilter();
                binder.set_filterExpression(filter);
                binder.set_isFiltering(true);
                binder.set_sortExpression('LastModified');

                this._bindWithSpecificItemId(this.get_sourceLibraryId(), binder);
            }
            else {
                binder.set_filterExpression('[ShowRecentLiveItems]');
                binder.DataBind();
            }
        }

        this._setDocumentsClass();

        if (this.get_mediaContentBreadcrumb()) {
            // hide the breadcrumb
            this.get_mediaContentBreadcrumb().dataBind();
        }
    },

    _setDocumentsClass: function () {
        if (this._mediaContentBinder && this._contentType == "Telerik.Sitefinity.Libraries.Model.Document") {
            var container = document.getElementById(this.get_mediaContentBinder()._targetLayoutContainerId);

            if (container != null) {
                jQuery(container).addClass("sfDocItemsListMode")
            }
        } else if (this._mediaContentBinder && this._contentType == "Telerik.Sitefinity.Libraries.Model.Video") {
            var container = document.getElementById(this.get_mediaContentBinder()._targetLayoutContainerId);

            if (container != null) {
                jQuery(container).addClass("sfVideoItemsListMode")
            }
        }
    },

    _resizeImage: function (img, w, h, size) {
        if (h > size || w > size) {
            if (h == w) {
                img.attr("height", size);
                img.attr("width", size);
            }
            else if (h > w) {
                //ie fix
                img.removeAttr("height");
                img.attr("width", size);
            }
            else {
                //ie fix
                img.removeAttr("width");
                img.attr("height", size);
            }
        }
    },

    //This method deals with highlighting issues - there should be only one selected item at a time
    _manageHighlight: function (selectedElement) {
        if (this._lastSelectedElement) {
            if (this._lastSelectedElement != selectedElement) {
                $(this._lastSelectedElement).removeClass("sfSel");
            } else {
                $(this._lastSelectedElement).addClass("sfSel");
            }
        }

        if (selectedElement) {
            this._lastSelectedElement = selectedElement;
        }
    },

    _setupTooltips: function () {
        this._removeTooltips();
        this._itemsListTooltipManager = new Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.extendItemsList(this.get_mediaContentItemsList());
    },
    _removeTooltips: function () {
        if (this._itemsListTooltipManager)
            this._itemsListTooltipManager.dispose();
    },

    _preSelectItems: function () {
        if (this._preSelectedItems) {
            this.get_mediaContentBinder().deselectAll();
            this.get_mediaContentBinder().selectByIds(this._preSelectedItems);
        }
    },

    // ------------------------------------------------- Properties ----------------------------------------------------    

    get_mediaContentItemsList: function () {
        return this._mediaContentItemsList;
    },
    set_mediaContentItemsList: function (value) {
        this._mediaContentItemsList = value;
    },

    get_mediaContentBinder: function () {
        return this._mediaContentBinder;
    },
    set_mediaContentBinder: function (value) {
        this._mediaContentBinder = value;
    },

    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        this._searchBox = value;
    },

    get_mediaContentBreadcrumb: function () {
        return this._mediaContentBreadcrumb;
    },
    set_mediaContentBreadcrumb: function (value) {
        this._mediaContentBreadcrumb = value;
    },

    get_parentType: function () {
        return this._parentType;
    },
    set_parentType: function (value) {
        this._parentType = value;
    },

    get_contentType: function () {
        return this._contentType;
    },
    set_contentType: function (value) {
        this._contentType = value;
    },

    get_mediaContentBinderServiceUrl: function () {
        return this._mediaContentBinderServiceUrl;
    },
    set_mediaContentBinderServiceUrl: function (value) {
        this._mediaContentBinderServiceUrl = value;
    },

    get_mediaContentBinderServiceChildItemsUrl: function () {
        return this._mediaContentBinderServiceChildItemsUrl;
    },
    set_mediaContentBinderServiceChildItemsUrl: function (value) {
        this._mediaContentBinderServiceChildItemsUrl = value;
    },


    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this.get_mediaContentBinder().set_culture(culture);
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this.get_mediaContentBinder().get_culture();
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        return this.get_mediaContentBinder().set_uiCulture(culture);
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this.get_mediaContentBinder().get_uiCulture();
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },
    set_bindOnLoad: function (value) {
        this._bindOnLoad = value;
    },
    get_currentUserId: function () {
        return this._currentUserId;
    },
    set_currentUserId: function (value) {
        this._currentUserId = value;
    },

    get_sourceLibraryId: function () {
        return this._sourceLibraryId;
    },
    set_sourceLibraryId: function (value) {
        this._sourceLibraryId = value;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView", Sys.UI.Control);

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.extendItemsList = function (itemsList) {
    this.options = {
        tooltipElementsSelector: "a.selectCommand",
        tooltipSelector: "div.sfTooltipWrp",
        imageContainerSelector: "li.sys-container",
        extraTooltipCssClass: "sfTooltipRAlign",
        rightAlignPerCount: 7
    };

    this._itemsList = itemsList;
    this._container = jQuery(this._itemsList.get_element());
    this._tooltipedParents = this._container.find(this.options.imageContainerSelector);
    this._tooltipedElements = this._tooltipedParents.find(this.options.tooltipElementsSelector);
    this._tooltips = this._getTooltipByElement(this._tooltipedParents);

    this.attachEventHandlers();
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.extendItemsList.prototype = {
    attachEventHandlers: function () {

        jQuery(document).on("mouseover", this.options.tooltipElementsSelector, jQuery.proxy(this._waithToShowTooltip, this));
        jQuery(document).on("mouseout", this.options.tooltipElementsSelector, jQuery.proxy(this._waitToHideTooltip, this));

        jQuery(document).on("mouseover", this.options.tooltipSelector, jQuery.proxy(this._preventHide, this));
        jQuery(document).on("mouseout", this.options.tooltipSelector, jQuery.proxy(this._allowHide, this));

    },
    detachEventHandlers: function () {
        this._tooltipedElements.die("mouseover mouseout");
        this._tooltips.die("mouseover mouseout");
    },
    dispose: function () {
        this.detachEventHandlers();
        delete this._tooltipedElements;
        delete this._tooltipedParents;
        delete this._tooltips;
    },
    repairEdgeElements: function () {
        this._getTooltipedParents();
        var tooltips = this._getTooltipByElement(this._tooltipedParents.filter(":nth-child(" + this.options.rightAlignPerCount + "n), :nth-child(" + this.options.rightAlignPerCount + "n - 1)"));
        tooltips.addClass(this.options.extraTooltipCssClass);
    },
    _getTooltipedParents: function () {
        this._tooltipedParents = this._container.find(this.options.imageContainerSelector);
    },
    _waithToShowTooltip: function (event) {
        this._waitForEventHandler(event, "_showTooltip");
    },
    _waitToHideTooltip: function (event) {
        this._waitForEventHandler(event, "_hideTooltip");
    },
    _showTooltip: function (container) {
        this._hideAllTooltips();
        this._getTooltipByElement(container).show();
        jQuery(container).addClass("sfTooltipShown");
    },
    _hideTooltip: function (container) {
        if (!this._skipHide)
            this._getTooltipByElement(container).hide();
    },
    _hideAllTooltips: function () {
        this._getVisibleTooltips().hide();
        jQuery(this._container).find(".sfTooltipShown").removeClass("sfTooltipShown");
    },
    _getTooltipFromEvent: function (event) {
        return this._getTooltipByElement();
    },
    _preventHide: function () { this._skipHide = true; },
    _allowHide: function () { this._skipHide = false; },
    _getTooltipByElement: function (element) {
        return jQuery(element).find(this.options.tooltipSelector);
    },
    _getVisibleTooltips: function () {
        return jQuery(this._itemsList.get_element()).find(this.options.tooltipSelector + ":visible");
    },
    _waitForEventHandler: function (event, methodName) {
        var container = event.currentTarget.parentNode;

        setTimeout(jQuery.proxy(function () {
            this[methodName](container);
        }, this), 100);

        event.stopPropagation();
    }    
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.extendItemsList.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorView.extendItemsList");
