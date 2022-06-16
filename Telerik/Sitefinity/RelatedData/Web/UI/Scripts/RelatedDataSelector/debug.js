﻿define(["RelatedDataGrid", "text!RelatedDataSelectorTemplate!strip"], function (RelatedDataGrid, html) {
    function RelatedDataSelector(options) {
        var that = this;
        this.template = html;
        this.options = options;

        $(this.options.container).html(this.template);
        this.viewModel = kendo.observable({
            showCreateBtn: function (e, s) {
                // disable explicitly creation of new items for Pages and Blog Posts
                if (options.itemType === "Telerik.Sitefinity.Pages.Model.PageNode" ||
                    options.itemType === "Telerik.Sitefinity.Blogs.Model.BlogPost")
                    return false;

                return true;
            },
            done: function (e, s) {
                e.preventDefault();
                that.done(e, s);
            },
            cancel: function (e, s) {
                e.preventDefault();
                that.cancel(e, s);
            },
            showAllItems: function (e, s) {
                e.preventDefault();
                that.showAllItems(e, s);
            },
            showSelected: function (e, s) {
                e.preventDefault();
                that.showSelected(e, s);
            },
            createItem: function (e, s) {
                e.preventDefault();
                that.createItem(e, s);
            },
            allItemsCount: 0,
            selectedItemsCount: 0,
            selectorTitle: this.options.title
        });
        kendo.bind($(this.options.container), this.viewModel);

        this.RelatedDataGrid = null;
        this.SelectedItemsGrid = null;
        this.activeGridView = null;

        this.relatedDataGridContainer = $(this.options.container).find("#itemSelectorContainer");
        this.selectedDataGridContainer = $(this.options.container).find("#selectedItemsContainer");

        this.initializeRelatedDataGrid();
        this.initializeSelectedItemsGrid();
    };
    RelatedDataSelector.prototype = {

        initialize: function () {

        },

        open: function (selectedItems, isEditMode) {

            $(this.options.container).show();
            if (this.options.kendoWindow) {
                this.options.kendoWindow.element.css({ "visibility": "visible" })
                    .parent().css({ "top": this._dialogScrollToTop() + "px", "left": "50%", "margin-left": "-212px" });
            }
            dialogBase.resizeToContent();

            if (this.RelatedDataGrid) {
                this.RelatedDataGrid.setPreselectedDataItems(selectedItems.toJSON());
            }
            if (this.SelectedItemsGrid) {
                this.SelectedItemsGrid.setDataItems(selectedItems.toJSON());
            }
        },

        cancel: function (event, sender) {
            if (this.options && this.options.kendoWindow) {
                $(this).trigger('onCancel', this);
                this.options.kendoWindow.close();
            }
        },

        done: function (event, sender) {
            var data = { Items: this.SelectedItemsGrid.getDataItems().toJSON() };
            $(this).trigger('onDone', data);

            if (this.options.kendoWindow) {
                this.options.kendoWindow.close();
            }
        },

        createItem: function (event, sender) {
            this.RelatedDataGrid.createItem(event, sender);
        },

        initializeRelatedDataGrid: function () {
            var relatedDataGridOptions = $.extend({}, this.options);
            relatedDataGridOptions.parentElement = this.relatedDataGridContainer;
            relatedDataGridOptions.dataSelectable = true;
            relatedDataGridOptions.autoBind = true;
            relatedDataGridOptions.showEmptyGridMessage = true;
            relatedDataGridOptions.enableMultilingualSearch = this.options.relatedDataSupportsMultilingualSearch;
            relatedDataGridOptions.visibleColumnKeys = ["AllowMultiple", this.options.relatedTypeIdentifierField, "LastModified", "Owner", "Edit", "Preview"];

            this.RelatedDataGrid = new RelatedDataGrid(relatedDataGridOptions);

            var that = this;
            $(this.RelatedDataGrid).on('onItemsCountChange', function (event, data) {
                that.viewModel.set('allItemsCount', data.TotalCount);
            });
            $(this.RelatedDataGrid).on('onItemSelected', $.proxy(this._onItemSelected, this));

            this.RelatedDataGrid.siteBaseUrl = this.options.siteBaseUrl;
            this.RelatedDataGrid.init();
            this.activeGridView = this.RelatedDataGrid;
        },

        initializeSelectedItemsGrid: function () {
            var selectedItemsGridOptions = $.extend({}, this.options);
            selectedItemsGridOptions.parentElement = this.selectedDataGridContainer;
            selectedItemsGridOptions.serviceUrl = this.options.selectedItemsServiceUrl;
            selectedItemsGridOptions.dataSourceServiceUrl = this.options.dataSourceServiceUrl;
            selectedItemsGridOptions.parameterMap = this.options.selectedItemsParameterMap;
            selectedItemsGridOptions.enableSearch = !this.options.isCreateMode;
            selectedItemsGridOptions.enableMultilingualSearch = this.options.relatedDataSupportsMultilingualSearch;
            selectedItemsGridOptions.dataSelectable = true;
            selectedItemsGridOptions.removeFromDataOnUnselect = true;
            selectedItemsGridOptions.autoBind = !this.options.isCreateMode;
            selectedItemsGridOptions.useEmptyDataSource = true;
            selectedItemsGridOptions.showEmptyGridMessage = true;
            selectedItemsGridOptions.visibleColumnKeys = ["AllowMultiple", this.options.relatedTypeIdentifierField, "LastModified", "Owner", "Edit", "Preview"];
            selectedItemsGridOptions.sort = undefined;
            selectedItemsGridOptions.enableProvidersSelector = false;
            selectedItemsGridOptions.siteId = this.options.siteId;

            this.SelectedItemsGrid = new RelatedDataGrid(selectedItemsGridOptions);
            var that = this;
            $(this.SelectedItemsGrid).on('onItemsCountChange', function (event, data) {
                that.viewModel.set('selectedItemsCount', data.TotalCount);
            });

            this.SelectedItemsGrid.siteBaseUrl = this.options.siteBaseUrl;
            this.SelectedItemsGrid.init();
        },

        showAllItems: function (event, sender) {
            this._setSelectState(event.currentTarget);
            this.selectedDataGridContainer.hide();
            this.relatedDataGridContainer.show();
            if (this.options.isCreateMode) {
                var currentItems = this.SelectedItemsGrid.getDataItems().toJSON();
                if (!currentItems) currentItems = [];
                this.RelatedDataGrid.setPreselectedDataItems(currentItems);
                this.RelatedDataGrid.setSelectedDataItems([]);
            }
            this.RelatedDataGrid.rebind();
            this.activeGridView = this.RelatedDataGrid;
        },

        showSelected: function (event, sender) {
            this._setSelectState(event.currentTarget);
            this.selectedDataGridContainer.show();
            this.relatedDataGridContainer.hide();
            if (!this.options.isCreateMode) {
                this.SelectedItemsGrid.rebind();
            }
            this.activeGridView = this.SelectedItemsGrid;
        },

        _onItemSelected: function (event, relation) {
            var changes = relation.Changes;
            var currentCount = this.viewModel.get('selectedItemsCount');
            for (var i = 0; i < changes.length; i++) {
                var data = changes[i];

                switch (data.Action) {
                    case "Add":
                        currentCount = currentCount + 1;
                        break;
                    case "Remove":
                        var deselectedCount = currentCount - 1;
                        currentCount = deselectedCount >= 0 ? deselectedCount : 0;
                    default:
                        break;
                }
            }
            this.viewModel.set('selectedItemsCount', currentCount);
        },

        _setSelectState: function (clickedItem) {
            jQuery(clickedItem).closest(".sfTabstrip").find("a").removeClass("sfSel");
            jQuery(clickedItem).addClass("sfSel");
        },

        getActiveGridView: function () {
            return this.activeGridView;
        },

        _dialogScrollToTop: function () {
            var scrollTopHtml = jQuery("html").eq(0).scrollTop();
            var scrollTopBody = jQuery("body").eq(0).scrollTop();
            var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
            return scrollTop;
        }
    };

    return (RelatedDataSelector);
});