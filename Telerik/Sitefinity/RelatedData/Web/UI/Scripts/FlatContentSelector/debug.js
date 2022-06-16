define(["ContentSelectorBase", "DetailViewEditingWindow", "text!FlatContentSelectorTemplate!strip"], function (ContentSelectorBase, DetailViewEditingWindow, html) {
    function FlatContentSelector(options) {
        ContentSelectorBase.call(this, options);
        this.template = html;
        this.options = options;

        this.viewModelObjExt = this.getViewModelObject();

        this.DetailViewEditingWindow = new DetailViewEditingWindow({
            closeEditingWindowHandler: jQuery.proxy(this.closeInlineEditingWindowHandler, this),
            serviceUrl: this.options.removeTempItemServiceUrl,
            culture: this.culture
        });

        this.dataItemTemplate = null;
        this.gridViewElement = null;
        this.providerSelectorElement = null;

        return (this);
    }

    FlatContentSelector.prototype = {

        init: function () {
            $(this.parentElement).html(this.template);
            this.gridViewElement = $(this.parentElement).find('#selectorGrid');
            this.providersSelectorElement = $(this.parentElement).find('#providersSelector');
            this.viewModelObjExt.contentDataSource = new kendo.data.DataSource(this.viewModelObjExt.contentDataSource);
            this.viewModelObjExt.providersDataSource = new kendo.data.DataSource(this.viewModelObjExt.providersDataSource);

            //If the template hasn't been changed, assign the default one
            if (!this.dataItemTemplate) {
                this.dataItemTemplate = $(this.parentElement).find('#dataItemTemplate').html();
            }

            ContentSelectorBase.prototype.init.call(this);

            this.initializeProviderSelector();
            this.initializeGridView();
            this.bindGridViewEvents();
            this.configureGridView();
        },

        initializeProviderSelector: function () {
            var that = this;
            if (that.options.enableProvidersSelector) {
                $(this.providersSelectorElement).kendoDropDownList({
                    dataTextField: "Title",
                    dataValueField: "Name",
                    dataSource: that.viewModel.providersDataSource,
                    index: 0,
                    change: that.viewModel.onProviderSelect,
                    dataBound: that.viewModel.onProvidersDataBound
                });
            }
        },

        initializeGridView: function () {
            this.validatedVisibleColumnKeys();
            var pagerConfig = this.options.pagerConfig;
            var rowTemplate = this.dataItemTemplate;
            var that = this;
            if (this.options.visibleColumnKeys) {
                var templateConfig =
                {
                    visibleColumns: {},
                    editItemLabel: function (data) {
                        return typeof data.IsTranslated !== "undefined" && !data.IsTranslated ? 'Add' : 'Edit';
                    },
                    editItemCommandCss: function (data) {
                        return typeof data.IsTranslated !== "undefined" && !data.IsTranslated ? 'sfAddBtn' : 'sfEditBtn';
                    },
                };
                $.each(this.options.visibleColumnKeys, function (i, element) {
                    var columnKey = element == that.options.relatedTypeIdentifierField ? 'MainIdentifierField' : element;
                    templateConfig.visibleColumns[columnKey] = true;
                });
                rowTemplate = $.proxy(kendo.template(this.dataItemTemplate), templateConfig);
            }

            $(this.gridViewElement).kendoGrid({
                dataSource: that.viewModel.contentDataSource,
                rowTemplate: rowTemplate,
                scrollable: that.options.scrollable ? that.options.scrollable : false,
                selectable: !that.options.allowMultipleSelection && that.options.dataSelectable,
                pageable: pagerConfig && pagerConfig.pageable ? pagerConfig.pageable : {
                    info: false
                },
                columns: that.options.columns || [
                    { field: "AllowMultiple", title: " ", template: "<input class='check_row' type='checkbox' />", filterable: false, sortable: false },
                    { field: "Title", title: "Title", filterable: false, sortable: true }
                ],
                autoBind: (that.options.parameterMap.ItemProvider != 'sf-any-site-provider') && (typeof (that.options.autoBind) === "boolean" ? that.options.autoBind : true),
                sortable: true,
                dataBound: that.viewModel.onDataBound,
                change: that.viewModel.onChange,
            }).delegate(".sfHeaderSelectItem", "click", function (e, s) {
                $(that).trigger('onHeaderSelect', this);
            })
                .delegate(".sfSelectDataItem", "click", function (e, s) {
                    $(that).trigger('onDataItemSelect', this);
                })
                .delegate(".sfEditBtn", "click", function (e, s) {
                    var anchor = e.target;
                    var dataItem = $(that.gridViewElement).data("kendoGrid").dataItem($(anchor).closest("tr"));
                    if (dataItem.IsEditable) {
                        var args = { Item: dataItem };
                        $(that).trigger('onItemEdit', args);
                    }
                })
                .delegate(".sfAddBtn", "click", function (e, s) {
                    var anchor = e.target;
                    var dataItem = $(that.gridViewElement).data("kendoGrid").dataItem($(anchor).closest("tr"));
                    var args = { Item: dataItem };
                    $(that).trigger('onItemAdd', args);
                })
                .delegate(".sfDelBtn", "click", function (e, s) {
                    var anchor = e.target;
                    var dataItem = $(that.gridViewElement).data("kendoGrid").dataItem($(anchor).closest("tr"));
                    var args = { Item: dataItem };
                    $(that).trigger('onItemRemove', args);
                });

            $(this.gridViewElement).show();
        },

        bindGridViewEvents: function () {
            var that = this;
            $(this).on('onHeaderSelect', function (event, data) {
                that.onHeaderSelect(data);
            });
            $(this).on('onDataItemSelect', function (event, data) {
                event.preventDefault();
                if (that.viewModel.allowMultipleSelection) {
                    that.onDataItemSelect(data);
                }
            });
            $(this).on('onItemEdit', function (event, data) {
                event.preventDefault();
                that.editItem(data.Item);
            });
            $(this).on('onItemAdd', function (event, data) {
                event.preventDefault();
                that.addItemTranslation(data.Item);
            });
            $(this).on('onItemRemove', function (event, data) {
                event.preventDefault();
                that.onRemove(data);
            });

            // FireFox is not initializing the grid with correct width of the Title column, so we need to refresh it once.
            if ($telerik.isFirefox) {
                var grid = this.gridViewElement.data("kendoGrid");
                grid.one("dataBound", function () { this.refresh(); });
            }
        },

        leafDataCells: function (container) {
            var rows = container.find(">tr:not(.k-filter-row)");

            var cells = $();
            if (rows.length > 1) {
                cells = rows.find("th:not(.k-group-cell,.k-hierarchy-cell)")
                    .filter(function () { return this.rowSpan > 1; });
            }

            cells = cells.add(rows.last().find("th:not(.k-group-cell,.k-hierarchy-cell)"));

            var indexAttr = kendo.attr("index");
            cells.sort(function (a, b) {
                a = parseInt($(a).attr(indexAttr), 10);
                b = parseInt($(b).attr(indexAttr), 10);
                return a > b ? 1 : (a < b ? -1 : 0);
            });
            return cells;
        },

        leafColumns: function (columns) {
            var result = [];
            for (var idx = 0; idx < columns.length; idx++) {
                if (!columns[idx].columns) {
                    result.push(columns[idx]);
                    continue;
                }
                result = result.concat(leafColumns(columns[idx].columns));
            }
            return result;
        },

        visibleColumns: function (columns) {
            var self = this;
            return $.grep(columns, function (column) {
                var result = !column.hidden;
                if (result && column.columns) {
                    result = self.visibleColumns(column.columns).length > 0;
                }
                return result;
            });
        },

        configureGridView: function () {
            var grid = this.gridViewElement.data("kendoGrid");
            if (this.options.hideGridHeaderRow) {
                $(this.gridViewElement).find(".k-grid-header").hide();
            }
            if (this.options.visibleColumnKeys) {
                var visibilityKeys = this.options.visibleColumnKeys;
                if (grid) {
                    var columns = grid.columns;
                    var that = this;
                    $(columns).each(function (index, column) {
                        if ($.inArray(column.field, visibilityKeys) == -1) {

                            //TODO: remove this workaround of bug related to Kendo Web v2014.3.1119 when calling hideColumn. There is no check if the cell is greater than zero
                            // inside hideColumn
                            //cell = leafDataCells(container).filter(":visible").eq(headerCellIndex);
                            //cell[0].style.display = "none";
                            var headerCellIndex = $.inArray(column, that.visibleColumns(that.leafColumns(grid.columns)));
                            var thead = grid.element.find('thead');
                            var visibleCells = that.leafDataCells(thead).filter(":visible").eq(headerCellIndex);
                            if (visibleCells.length > 0) {
                                grid.hideColumn(column.field);
                            }
                        }
                    });
                }
            }
            if (this.options.allowSorting) {
                this.configureSortableGrid();
            }
            if (this.options.hideGridWrapper) {
                $(this.gridViewElement).parent().removeClass("sfSelectorGridWrapper");
            }
        },

        configureSortableGrid: function () {
            var that = this;
            var grid = this.gridViewElement.data("kendoGrid");
            grid.table.kendoSortable({
                dataSource: grid.dataSource,
                filter: ">tbody >tr",
                hint: $.noop,
                cursor: "move",
                placeholder: function (element) {
                    return element.clone().addClass("sfIsBeingDragged");
                },
                container: that.gridViewElement,
                change: function (e) {
                    var skip = grid.dataSource.skip() || 0,
                        newIndex = e.newIndex + skip,
                        dataItem = grid.dataSource.getByUid(e.item.data("uid"));

                    grid.dataSource.remove(dataItem);
                    grid.dataSource.insert(newIndex, dataItem);

                    var prevItem = grid.dataSource.view()[e.newIndex - 1];
                    var prevOrdinal = prevItem ? prevItem.Ordinal : null;

                    var nextItem = grid.dataSource.view()[e.newIndex + 1];
                    var nextOrdinal = nextItem ? nextItem.Ordinal : null;

                    // Reload data from source when items are reordered to adjust their position
                    var data = {
                        item: dataItem, prevOrdinal: prevOrdinal, nextOrdinal: nextOrdinal, onSuccess: function () {
                            grid.dataSource.read();
                        }
                    };

                    that.onSort(data);
                },
                handler: '.sfGrippy'
            });
        },

        validatedVisibleColumnKeys: function () {
            var keys = this.options.visibleColumnKeys;
            if ($.inArray("AllowMultiple", keys) !== -1 && !this.options.allowMultipleSelection) {
                keys = keys.remove('AllowMultiple')
            }
            if (($.inArray("AllowSorting", keys) !== -1) &&
                (!this.options.allowSorting || this.options.isBackendReadMode)) {
                keys = keys.remove('AllowSorting');
                this.options.allowSorting = false;
            }
            if ($.inArray("Remove", keys) !== -1 && this.options.isBackendReadMode) {
                keys = keys.remove('Remove')
            }
            if ($.inArray("Edit", keys) !== -1 && this.options.isBackendReadMode) {
                keys = keys.remove('Edit')
            }
            this.options.visibleColumnKeys = keys;
        },

        getDataItem: function (uid) {
            var gridView = this.gridViewElement.data("kendoGrid");
            return gridView.dataSource.getByUid(uid);
        },

        getDataItems: function () {
            var gridView = this.gridViewElement.data("kendoGrid");
            return gridView.dataSource.data();
        },

        setDataItems: function (items) {
            var gridView = this.gridViewElement.data("kendoGrid");
            gridView.dataSource.data([]);
            for (var i = 0; i < items.length; i++) {
                var itemInDataSource = gridView.dataSource.get(items[i].Id);
                if (!itemInDataSource) {
                    gridView.dataSource.add(items[i]);
                }
            }
            this.selectedDataItems = items;
            this.selectItems(gridView);
        },

        selectDataItems: function (items) {
            if (this.preselectedDataItemIds.length > 0) {
                this.preselectedDataItemIds = [];
            }
            var gridView = this.gridViewElement.data("kendoGrid");
            this.selectedDataItems = items;
            this.selectItems(gridView);
        },

        removeDataItem: function (item) {
            var gridView = this.gridViewElement.data("kendoGrid");
            if (item && item.Id) {
                var itemInDataSource = gridView.dataSource.get(item.Id);
                if (itemInDataSource) {
                    gridView.dataSource.remove(itemInDataSource);
                }
            }
        },

        addDataItem: function (item) {
            var gridView = this.gridViewElement.data("kendoGrid");
            if (item && item.Id) {
                var itemInDataSource = gridView.dataSource.get(item.Id);
                if (!itemInDataSource) {
                    gridView.dataSource.add(item);
                }
            }
        },

        addSelectedDataItem: function (selectedItem) {
            var gridView = this.gridViewElement.data("kendoGrid");
            if (selectedItem && selectedItem.Id) {
                var itemInDataSource = gridView.dataSource.get(selectedItem.Id);
                if (!itemInDataSource) {
                    if (this.viewModel.allowMultipleSelection) {
                        this.addDataItemToSelected(selectedItem);
                        // adding manually an item to data source calls onDataBound
                        gridView.dataSource.add(selectedItem);
                    } else {
                        this.onSingleSelectionChange(selectedItem, gridView);
                    }
                }
            }
        },

        setPreselectedDataItems: function (items) {
            var preselectedIds = [];
            for (var i = 0; i < items.length; i++) {
                preselectedIds.push(items[i].id || items[i].Id);
            }
            this.setPreselectedDataItemIds(preselectedIds);
        },

        selectItems: function (gridView) {
            this.unselect(gridView);
            var items = this.selectedDataItems;

            // preselected items are used for marking items as selected, when field is in create mode, and server operations for temp are still not possible
            var preselectedIds = this.getPreselectedDataItemIds();
            if (preselectedIds.length !== 0) {
                for (var j = 0; j < preselectedIds.length; j++) {
                    var selectedItem = gridView.dataSource.get(preselectedIds[j]);
                    if (selectedItem && items.indexOf(selectedItem) == -1) {
                        items.push(selectedItem);
                    }
                }
            }

            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                this.selectDataItem(gridView, item);
            }
        },

        selectDataItem: function (gridView, item) {
            var dataItem = gridView.dataSource.get(item.id || item.Id);
            if (dataItem) {
                var itemElements = gridView.items();
                var dataItemElement = jQuery.grep(itemElements, function (element) {
                    return ($(element).data("uid") == dataItem.uid);
                });
                if (this.viewModel.allowMultipleSelection) {
                    var checkBox = $(dataItemElement).find("input:checkbox");
                    $(checkBox).attr("checked", "checked");
                }
                else {
                    $(dataItemElement).addClass('k-state-selected');
                }
            }
        },

        unselect: function (gridView) {
            var itemElements = gridView.items();
            if (this.viewModel.allowMultipleSelection) {
                var checkBoxes = itemElements.find("input:checkbox");
                $(checkBoxes).prop("checked", false);
            } else {
                $(itemElements).removeClass('k-state-selected');
            }
        },

        onDataItemSelect: function (checkBox) {
            var dataItemId = $(checkBox).val();
            var dataItem = this.viewModel.contentDataSource.get(dataItemId);
            var relation = { Changes: [] };
            var data = { Item: dataItem };
            if (checkBox.checked) {
                data.Action = "Add";
                if (this.options.isCreateMode) {
                    this.addDataItemToSelected(dataItem);
                }
            } else {
                data.Action = "Remove";
                if (this.options.isCreateMode) {
                    this.removeDataItemFromSelected(dataItem);
                }
                if (this.viewModel.removeFromDataOnUnselect) {
                    this.removeDataItem(dataItem);
                }
            }
            relation.Changes.push(data);
            $(this).trigger('onItemSelected', relation);
        },

        //TODO/6: select all items from current page
        onHeaderSelect: function (data) {
            var gridView = this.gridViewElement.data("kendoGrid");
            var rowsCount = gridView._data.length;
            //args.set_cancel(true);
            if (rowsCount > 0) {
                var headerCheckbox = args.get_commandArgument();
                var headerIsChecked = jQuery(headerCheckbox).is(':checked');
                var checkBoxes = jQuery(that.get_commentsGrid().get_grid()).find(".sfSelectComment");
                that._setToolbarButtonsEnabledState(headerIsChecked);
                checkBoxes.attr("checked", headerIsChecked).parent().parent().attr("aria-selected", headerIsChecked ? "true" : "false");
            }
        },

        getViewModelObject: function () {
            var that = this;
            return {
                contentDataSource: that.getContentDataSourceObj(),
                providersDataSource: that.getProvidersDataSourceObj(),
                enableSearch: this.options.enableSearch,
                enableProviderSelector: that.options.enableProvidersSelector,
                enableMultilingualSearch: this.options.enableMultilingualSearch,
                onDataBound: function (e) {
                    var pagerElement = that.gridViewElement.find("div[data-role = 'pager']");
                    pagerElement.hide();
                    var totalPages = this.dataSource.totalPages();
                    if (!isNaN(totalPages) && totalPages > 1 &&
                        !(that.options.scrollable && that.options.scrollable.virtual)) {
                        pagerElement.show();
                    }
                    that.selectItems(this);
                    that.showEmptyGridMessage(this);
                    if (!that.viewModel.searchValue) {
                        $(that).trigger('onItemsCountChange', { TotalCount: this.dataSource.total() });
                    }
                },
                onChange: function (e) {
                    if (!that.viewModel.allowMultipleSelection) {
                        var data = that.viewModel.contentDataSource.view();
                        var selectedDataItems = $.map(e.sender.select(), function (item) {
                            return data[$(item).index()];
                        });
                        that.onSingleSelectionChange(selectedDataItems[0]);
                    }
                },
                onProviderSelect: function (e) {
                    var providerName = e.sender.value()
                    that.setProviderName(providerName);
                },
                onProvidersDataBound: function (e) {
                    var providerName = e.sender.value()
                    that.setProviderName(providerName);

                    if (e.sender.dataSource.view().length < 2) {
                        that.viewModel.set('enableProviderSelector', false)
                    }
                },
                containerElement: this.element,
                createItem: function (e, s) {
                    e.preventDefault();
                    that.createItem(e, s);
                },
                emptyGridMessage: 'No items to display'
            };
        },

        onSingleSelectionChange: function (selectedItem, gridView) {
            // get previously selected item
            var deselectedItem = this.selectedDataItems.length > 0 ? this.selectedDataItems[0] : null;
            this.selectedDataItems = [selectedItem];
            if (gridView) {
                gridView.dataSource.add(selectedItem);
            }

            if (selectedItem && deselectedItem && selectedItem.Id === deselectedItem.Id)
                return;

            // trigger changed relations
            var relation = { Changes: [] };
            if (selectedItem) {
                relation.Changes.push({ Action: "Add", Item: selectedItem });
            }
            if (deselectedItem) {
                relation.Changes.push({ Action: "Remove", Item: deselectedItem });
            }
            if (relation.Changes.length > 0) {
                $(this).trigger('onItemSelected', relation);
            }
        },

        getContentDataSourceObj: function () {
            var that = this;
            var dataSource = {
                transport: {
                    read: {
                        type: "GET",
                        dataType: "json",
                        cache: false,
                        url: function (options) {
                            return that.resolveUrlFunc(options);
                        },
                        beforeSend: function (xhr) {
                            if (that.culture) {
                                xhr.setRequestHeader("SF_UI_CULTURE", that.culture);
                            }
                            xhr.setRequestHeader("IsBackendRequest", true);
                        }
                    },
                    parameterMap: function (options) {
                        var parameterMap = that.parameterMapFunc(options);
                        if (parameterMap.hasOwnProperty('ItemProvider') && that.providerName) {
                            parameterMap.ItemProvider = that.providerName;
                        }
                        return parameterMap;
                    }
                },
                schema: {
                    data: "Items",
                    total: "TotalCount",
                    model: {
                        id: "Id"
                    }
                },
                requestEnd: function (e) {
                    var items = e.response && e.response.Items ? e.response.Items : [];
                    that.markDataItemsTranslatedForCurrentCulture(items);
                    if (that.options.dataSelectable) {
                        that.markDataItemsAsSelected(items, that.options.isCreateMode);
                    }
                    that.onRequestEnd(e);
                },
                serverFiltering: true,
                serverSorting: true,
                group: that.options.group
            };

            var pagerConfig = that.options.pagerConfig;
            if (pagerConfig) {
                dataSource.serverPaging = pagerConfig.serverPaging || false;
                dataSource.page = pagerConfig.page;
                dataSource.pageSize = pagerConfig.pageSize;
            } else {
                dataSource.serverPaging = true;
                dataSource.page = 1;
                dataSource.pageSize = 20;
            }

            return dataSource;
        },

        getProvidersDataSourceObj: function () {
            var that = this;

            var dataSource = {
                transport: {
                    read: {
                        type: "GET",
                        dataType: "json",
                        cache: false,
                        url: function (options) {
                            return that.resolveDataSourceServiceUrlFunc({ type: that.itemType });
                        }
                    }
                },
                schema: {
                    data: "Items",
                    total: "TotalCount"
                }
            };
            
            return dataSource;
        },

        showEmptyGridMessage: function (gridView) {
            if (this.options.showEmptyGridMessage) {
                if (gridView.dataSource._view.length == 0) {
                    $(this.gridViewElement).find(".k-grid-header").hide();
                    $(gridView.element).find('tbody').append('<div>' + this.viewModel.emptyGridMessage + '</div>');
                } else if (!this.options.hideGridHeaderRow) {
                    $(this.gridViewElement).find(".k-grid-header").show();
                }
            }
        },

        editItem: function (data) {
            data.ShowMoreActionsWorkflowMenu = false;
            data.HideLanguageList = false;
            data.Culture = this.culture;
            this.DetailViewEditingWindow.open(data);
        },

        onRemove: function (data) { },

        onSort: function (data) { },

        createItem: function (event, sender) {
            var data = {
                ShowMoreActionsWorkflowMenu: false,
                HideLanguageList: false,
                Culture: this.culture,
                DetailsViewUrl: this.createItemUrl,
                ItemType: this.itemType,
                CreateItem: true,
                ProviderName: this.providerName || this.options.parameterMap.ItemProvider
            }

            this.DetailViewEditingWindow.open(data);
        },

        addItemTranslation: function (data) {
            data.ShowMoreActionsWorkflowMenu = false;
            data.HideLanguageList = false;
            data.Culture = this.culture;
            data.CommandArgument = { languageMode: "create", language: this.culture };
            this.DetailViewEditingWindow.open(data);
        },

        closeInlineEditingWindowHandler: function (sender, args) {
            var clearFrame = true;
            if (args.deleteTemp && sender.get_dataItem()) {
                // get data for temp item created when details view was opened
                var tempItemData = {
                    'ItemId': sender.get_dataItem().Item.Id,
                    'ItemType': sender.get_dataItem().ItemType,
                    'Provider': sender._providerName
                }
                clearFrame = false;
                this.deleteTemp(tempItemData);
            }
            if (args.isNew && args.lastModifiedDataItem) {

                var itemId = args.lastModifiedDataItem.Id;
                var data = { Id: itemId, Ordinal: undefined, ProviderName: sender._providerName };
                $(this).trigger('onItemCreated', data);

                if (this.options.isCreateMode) {
                    if (!this.preselectedDataItemIds) {
                        this.preselectedDataItemIds = [];
                    }
                    this.preselectedDataItemIds.push(args.lastModifiedDataItem.Id);
                }
            }
            if (args.workflowOperationWasExecuted) {
                clearFrame = false;
            }
            if (!this.options.isCreateMode) {
                this.rebind();
            }
            this.DetailViewEditingWindow.close(clearFrame);
        },

        deleteTemp: function (tempItemData) {
            var obj = { ItemId: tempItemData.ItemId, Provider: tempItemData.Provider, ItemType: tempItemData.ItemType };
            var that = this;
            this.DetailViewEditingWindow.deleteTemp(obj, function () {
                if (!that.options.isCreateMode) {
                    that.rebind();
                }
                that.DetailViewEditingWindow.clearFrame();
            });
        }

        //----------------------------------------------------
    };
    FlatContentSelector.prototype = $.extend(Object.create(ContentSelectorBase.prototype), FlatContentSelector.prototype);
    return (FlatContentSelector);
});

kendo.data.binders.serverValue = kendo.data.Binder.extend({
    init: function (element, bindings, options) {
        kendo.data.Binder.fn.init.call(this, element, bindings, options);
        var binding = this.bindings['serverValue'];
        binding.source[binding.path] = $(this.element).val();
    },
    refresh: function () {
        var binding = this.bindings['serverValue'];
        $(this.element).val(binding.source[binding.path]);
    }
});
kendo.data.binders.keyUp = kendo.data.Binder.extend({
    init: function (element, bindings, options) {
        kendo.data.Binder.fn.init.call(this, element, bindings, options);
        var binding = this.bindings.keyUp;
        $(element).bind("keyup", function (e) {
            if (typeof binding.source[binding.path] === 'function') {
                binding.source[binding.path](this, e);
            }
        });
    },
    refresh: function () { }
});
kendo.data.binders.widget.keyUp = kendo.data.Binder.extend({
    init: function (element, bindings, options) {
        kendo.data.Binder.fn.init.call(this, element, bindings, options);
        var binding = this.bindings.keyUp;
        if (element.options.name == 'MultiSelect') {
            $(element.element).parent().find('input').bind("keyup", function (e) {
                binding.source.multiSelectKeyUp = e;
                binding.get();
            });
        }
    },
    refresh: function () { }
});

if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length;

        var from = Number(arguments[1]) || 0;
        from = (from < 0) ? Math.ceil(from) : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
                this[from] === elt)
                return from;
        }
        return -1;
    };
}
if (!Array.prototype.compare) {
    Array.prototype.compare = function (array) {
        if (!array) {
            return false;
        }

        if (this.length != array.length) {
            return false;
        }

        for (var i = 0; i < this.length; i++) {
            if (this[i] instanceof Array && array[i] instanceof Array) {
                if (!this[i].compare(array[i])) {
                    return false;
                }
            }
            else if (this[i] != array[i]) {
                var hasSameProps = true;
                for (var property in this[i]) {
                    if (this[i][property] != array[i][property]) {
                        hasSameProps = false;
                        break;
                    }
                }
                if (!hasSameProps) {
                    return false;
                }
            }
        }
        return true;
    }
}
if (!Object.create) {
    Object.create = (function () {
        function F() { }
        return function (o) {
            if (arguments.length != 1) {
                throw new Error('Object.create implementation only accepts one parameter.');
            }
            F.prototype = o
            return new F();
        }
    })()
}
if (!Array.prototype.remove) {
    Array.prototype.remove = function () {
        var what, a = arguments, L = a.length, ax;
        while (L && this.length) {
            what = a[--L];
            while ((ax = this.indexOf(what)) != -1) {
                this.splice(ax, 1);
            }
        }
        return this;
    }
}