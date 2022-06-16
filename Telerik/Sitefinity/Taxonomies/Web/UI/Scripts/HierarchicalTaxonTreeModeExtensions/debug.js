function HierarchicalTaxonTreeModeExtensions_ViewLoaded(masterGridView) {
    var context = this;

    this.itemsGrid = null;
    this.serviceBaseUrl = null;
    this.taxonomyId = null;
    this.expandedParentId = null;
    this.singleTaxonName = null;
    this.taxonomyName = null;
    this.taxonomyTitle = null;
    this.binder = null;
    this.moveItemUpServiceUrl = null;
    this.moveItemDownServiceUrl = null;
    this.batchMoveItemsUpServiceUrl = null;
    this.batchMoveItemsDownServiceUrl = null;
    this.masterGridView = masterGridView;
    this.clientLabelManager = null;

    this.initialize = function () {
        this._itemsGrid_ItemCommandDelegate = Function.createDelegate(this, this.itemsGrid_ItemCommand);
        this._itemsGrid_CommandDelegate = Function.createDelegate(this, this.itemsGrid_Command);
        this._itemsGrid_binder_itemDataBoundDelegate = Function.createDelegate(this, this.itemsGrid_binder_itemDataBound);
        this._moveTaxon_SuccessDelegate = Function.createDelegate(this, this.moveTaxon_Success);
        this._moveTaxon_FailureDelegate = Function.createDelegate(this, this.moveTaxon_Failure);
        this._moveTaxons_SuccessDelegate = Function.createDelegate(this, this.moveTaxons_Success);
        this._moveTaxons_FailureDelegate = Function.createDelegate(this, this.moveTaxons_Failure);
        this._selectionChangedDelegate = Function.createDelegate(this, this.selectionChanged);

        var customParams = masterGridView.get_customParameters();

        this.itemsGrid = masterGridView.get_itemsTreeTable();
        this.itemsGrid.add_itemCommand(this._itemsGrid_ItemCommandDelegate);
        this.itemsGrid.add_command(this._itemsGrid_CommandDelegate);
        this.itemsGrid.add_selectionChanged(this._selectionChangedDelegate);

        this.binder = this.itemsGrid.getBinder();
        this.binder.add_onItemDataBound(this._itemsGrid_binder_itemDataBoundDelegate);

        this.serviceBaseUrl = masterGridView._baseServiceUrl;
        this.taxonomyId = customParams.taxonomyId;
        this.singleTaxonName = customParams.singleTaxonName;
        this.taxonomyName = customParams.taxonomyName;
        this.taxonomyTitle = customParams.taxonomyTitle;

        this.moveItemUpServiceUrl = customParams.moveItemUpServiceUrl;
        this.moveItemDownServiceUrl = customParams.moveItemDownServiceUrl;
        this.batchMoveItemsUpServiceUrl = customParams.batchMoveItemsUpServiceUrl;
        this.batchMoveItemsDownServiceUrl = customParams.batchMoveItemsDownServiceUrl;

        this.clientLabelManager = masterGridView.get_clientLabelManager(); 

        var toolbar = this.masterGridView.get_toolbar();
        var createNewWidget = toolbar.getWidgetByName("CreateHierarchicalTaxon");
        if (createNewWidget) {
            var createText = createNewWidget.get_buttonText();
            createText = this.GetFormattedString(createText, this.singleTaxonName.toLowerCase());
            createNewWidget.set_buttonText(createText);
        }

        var moreActionsWidget = toolbar.getWidgetByName("Move");
        if (moreActionsWidget) {
            var moreActionElements = moreActionsWidget._menu.get_allItems();
            var moreActionsIter = moreActionElements.length;
            while (moreActionsIter--) {
                var action = moreActionElements[moreActionsIter];
                if (action.get_value() == "batchChangeParent") {
                    var txt = action.get_text();
                    txt = this.GetFormattedString(txt, this.singleTaxonName.htmlEncode());
                    action.set_text(txt);
                }
            }
        }
    }

    this.selectionChanged = function (sender, args) {
        masterGridView._setToolbarButtonsEnabledState();
    }

    this.itemsGrid_binder_itemDataBound = function (sender, args) {
        var container = args.get_itemElement();
        var dataItem = args.get_dataItem();
        var self = this;
        $(container).find('.sf_binderCommand_changeParent').each(function () {
            var html = $(this).html();
            html = String.format(html, self.singleTaxonName.toLowerCase());
            $(this).text(html);
        });

        var itemsCount = dataItem["ItemsCount"];
        if (itemsCount == 0) {
            var oThis = this;
            $(container).find('.sf_binderCommand_viewMarkedItems').each(function () {
                var div = $(this).parent();
                div.html(itemsCount + " " + oThis.clientLabelManager.getLabel("Labels", "Items"));
            });
        }
    };

    this.itemsGrid_Command = function(sender, args) {
        var commandName = args.get_commandName();
        switch (commandName) {
            case 'batchMoveUp':
                if (sender.getBinder().get_selectedItemsCount() > 0) {
                    args.set_cancel(true);
                    this.moveTaxons('up');
                }
                break;
            case 'batchMoveDown':
                if (sender.getBinder().get_selectedItemsCount() > 0) {
                    args.set_cancel(true);
                    this.moveTaxons('down');
                }
                break;
            case 'batchChangeParent':
                if (sender.getBinder().get_selectedItemsCount() > 0) {
                    args.set_cancel(true);
                    this.itemsGrid.openDialog('changeParent', this.itemsGrid.get_selectedItems(),
                    {
                        'TaxonomyId': this.taxonomyId,
                        "TaxonName": this.singleTaxonName,
                        "TaxonomyTitle": this.taxonomyName,
                        "UiCulture": this.itemsGrid.get_uiCulture()
                    });
                }
                break;
            default:
                args.set_cancel(false);
                break;
        }
    };

    this.itemsGrid_ItemCommand = function (sender, args) {
        var commandName = args.get_commandName();
        var dataItem = args.get_commandArgument();
        switch (commandName) {
            case 'changeParent':
                args.set_cancel(true);
                this.itemsGrid.openDialog('changeParent', [dataItem],
                {
                    'TaxonomyId': this.taxonomyId,
                    "TaxonName": this.singleTaxonName,
                    "TaxonomyTitle": this.taxonomyName,
                    "UiCulture": this.itemsGrid.get_uiCulture()
                });
                break;
//            case 'createChild':
//                args.set_cancel(true);
//                var argument = { 'createMode': 'createChild' };
//                this.itemsGrid.executeItemCommand('create', dataItem, null, argument);
//                break;
//            case 'createSiblingBefore':
//                args.set_cancel(true);
//                var argument = { 'createMode': 'createSiblingBefore' };
//                this.itemsGrid.executeItemCommand('create', dataItem, null, argument);
//                break;
//            case 'createSiblingAfter':
//                args.set_cancel(true);
//                var argument = { 'createMode': 'createSiblingAfter' };
//                this.itemsGrid.executeItemCommand('create', dataItem, null, argument);
//                break;
            case 'moveUp':
                args.set_cancel(true);
                this.moveTaxon('up', dataItem);
                break;
            case 'moveDown':
                args.set_cancel(true);
                this.moveTaxon('down', dataItem);
                break;
            default:
                args.set_cancel(false);
                break;
        }
    };

    /* **************** move taxon **************** */

    this.moveTaxon = function (direction, dataItem) {
        var baseUrl;
        if (direction == 'up') {
            baseUrl = this.moveItemUpServiceUrl;
        } else if (direction == 'down') {
            baseUrl = this.moveItemDownServiceUrl;
        } else {this._moveTaxon_FailureDelegate
            alert('Unknown direction for moving an item "' + direction + '". Supported are "up" and "down".');
            return;
        }

        var taxonId = dataItem.Id;
        this.expandedParentId = dataItem.ParentTaxonId;
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var urlParams = [];
        var keys = [this.taxonomyId, taxonId];
        clientManager.InvokePut(baseUrl, urlParams, keys, taxonId, this._moveTaxon_SuccessDelegate, this._moveTaxon_FailureDelegate);
    };

    this.moveTaxon_Success = function (result) {
        this.itemsGrid.getBinder().DataBind(this.expandedParentId);
    };

    this.moveTaxon_Failure = function (result) {
        alert(result.Detail);
    };

    /* **************** batch move taxons **************** */
    this.moveTaxons = function (direction) {
        var baseUrl;
        if (direction == 'up') {
            baseUrl = this.batchMoveItemsUpServiceUrl;
        } else if (direction == 'down') {
            baseUrl = this.batchMoveItemsDownServiceUrl;
        } else {
            alert('Unknown direction for moving items "' + direction + '". Supported are "up" and "down".');
            return;
        }

        var data = this.GetSelectedIds();
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var urlParams = [];
        var keys = [this.taxonomyId];
        clientManager.InvokePut(baseUrl, urlParams, keys, data, this._moveTaxons_SuccessDelegate, this._moveTaxons_FailureDelegate);
    };

    this.moveTaxons_Success = function (result) {
        this.itemsGrid.getBinder().DataBind();
    };

    this.moveTaxons_Failure = function (result) {
        alert(result.Detail);
    };

    /* **************** helper methods **************** */

    this.GetSelectedIds = function () {
        var selectedIds = [];
        var selectedItems = this.itemsGrid.get_selectedItems();
        for (var i = 0, selectedItemsLength = selectedItems.length; i < selectedItemsLength; i++) {
            selectedIds.push(selectedItems[i].Id);
        }
        return selectedIds
    };

    this.GetFormattedString = function (str, strToFormat) {
        if (str.indexOf('{0}') != -1) {
            return String.format(str, strToFormat);
        }
        return str;
    };

    this.initialize();
}