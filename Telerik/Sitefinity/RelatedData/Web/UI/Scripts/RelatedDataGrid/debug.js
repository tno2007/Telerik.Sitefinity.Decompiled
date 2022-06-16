define(["FlatContentSelector", "text!RelatedDataGridTemplate!strip"], function (FlatContentSelector, html) {
    function RelatedDataGrid(options) {
        this.options = options;
        this.options.columns = this.getColumnsConfig();
        FlatContentSelector.call(this, options, html);

        this.serviceUrl = options.serviceUrl;
        this.dataSourceServiceUrl = options.dataSourceServiceUrl;
        this.parameterMap = options.parameterMap;
        this.dataItemTemplate = html;

        if (options.isCreateMode && options.useEmptyDataSource) {
            this.viewModelObjExt.contentDataSource = this.getEmptyDataSourceObj();
        }
        this.viewModelObjExt.isMultilingual = options.isMultilingual;
        return (this);
    };

    RelatedDataGrid.prototype = {

        init: function () {
            FlatContentSelector.prototype.init.call(this);
            this.options.parentElement.find("[data-role=pager]").hide();
        },

        parameterMapFunc: function (options) {
            var queryObj = {
                take: options.take
            };
            $.extend(queryObj, this.parameterMap);
            if (this.isSearchRequest()) {
                if (this.viewModel.contentDataSource.page() > 1) {
                    queryObj.skip = options.skip;
                } else {
                    queryObj.skip = 0;
                }
                var mainIdentifier = this.options.relatedTypeIdentifierField || "Title";
                queryObj.filter = this.generateFilterExpression(mainIdentifier, this.viewModel.searchValue);
            } else {
                queryObj.skip = options.skip;
                queryObj.take = options.take;
            }

            if (this.options.applyMasterItemFilter) {
                var filter = "STATUS = MASTER";
                if (queryObj.filter && queryObj.filter !== "") {
                    filter = queryObj.filter + " AND STATUS = MASTER"
                }
                queryObj.filter = filter;
            }

            if (options.sort && options.sort.length > 0) {
                queryObj.order = options.sort[0].field + " " + options.sort[0].dir;
            } else {
                if (this.options.sort) {
                    queryObj.order = this.options.sort.field + " " + this.options.sort.dir;
                }
            }                        

            return queryObj;
        },

        resolveUrlFunc: function (options) {
            return this.serviceUrl;
        },

        resolveDataSourceServiceUrlFunc: function (params) {
            return this.dataSourceServiceUrl + '/' + params.type + '/?siteId=' + this.options.siteId;
        },

        onRequestEnd: function (e) {
            if (this.options.loadingContainer) {
                $(this.options.loadingContainer).hide();
            }
        },

        onRemove: function (data) {
            $(this).trigger('onRelatedDataGridRemove', data);
        },

        onSort: function (data) {
            $(this).trigger('onRelatedDataGridSort', data);
        },

        getEmptyDataSourceObj: function () {
            return {
                data: [],
                schema: {
                    data: "Items",
                    total: "TotalCount",
                    model: {
                        id: "Id"
                    }
                }
            };
        },

        getColumnsConfig: function () {
            return [
                    { field: "AllowMultiple", title: " ", template: "<input class='check_row' type='checkbox' />", filterable: false, sortable: false },
                    { field: "AllowSorting", title: " ", filterable: false, sortable: false },
                    { field: this.options.relatedTypeIdentifierField, title: this.options.relatedTypeIdentifierField, filterable: false, sortable: true },
                    { field: "LastModified", title: "Modified on", filterable: false, sortable: true },
                    { field: "Owner", title: "Owner", filterable: false, sortable: true },
                    { field: "Edit", title: " ", filterable: false, sortable: false },
                    { field: "Preview", title: " ", filterable: false, sortable: false },
                    { field: "Remove", title: " ", filterable: false, sortable: false }
            ];
        }
    };

    RelatedDataGrid.prototype = $.extend(Object.create(FlatContentSelector.prototype), RelatedDataGrid.prototype);
    return (RelatedDataGrid);
});