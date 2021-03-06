define(["FlatContentSelector", "text!RelatingDataGridTemplate!strip"], function (FlatContentSelector, html) {
    function RelatingDataGrid(options) {
        FlatContentSelector.call(this, options, html);

        this.options = options;
        this.dataItemTemplate = html;
        this.serviceUrl = options.serviceUrl;
        this.parameterMap = options.parameterMap;
        this.viewModelObjExt.isMultilingual = options.isMultilingual;
        this.options.columns = this.getColumnsConfig();
        this.viewModelObjExt.contentDataSource.group = this.getGroupConfig(options.group);
        return (this);
    };

    RelatingDataGrid.prototype = {
        init: function () {
            FlatContentSelector.prototype.init.call(this);
        },

        parameterMapFunc: function (options) {
            var queryObj = {
                take: 0,
                skip: 0
            };
            $.extend(queryObj, this.parameterMap);

            return queryObj;
        },

        resolveUrlFunc: function (options) {
            return this.serviceUrl;
        },

        onRequestEnd: function (e) {
            if (this.options.onRequestEnd) {
                this.options.onRequestEnd(e);
            }
        },

        open: function () {
            $(this.options.container).show();
            if (this.options.kendoWindow) {
                this.options.kendoWindow.element.css({ "visibility": "visible" })
                    .parent().css({ "top": "50px", "left": "50%", "margin-left": "-212px" });
                this.options.kendoWindow.open();
            }

            dialogBase.resizeToContent();
        },

        getColumnsConfig: function () {
            return [
                    {
                        field: "ContentTypeName",
                        title: " ",
                        filterable: false,
                        sortable: false,
                        groupHeaderTemplate: "#= count # #: value #",
                        aggregates: ["count"]
                    },
                    { field: "Title", title: "Title", filterable: false, sortable: false },
                    { field: "Remove", title: " ", filterable: false, sortable: false }
            ];
        },

        getGroupConfig: function (group) {
            var aggregate = { field: "ContentTypeName", aggregate: "count" };
            if (!group) {
                return {
                    field: "ContentTypeName",
                    aggregates: [aggregate]
                };
            }
            else {
                if (!group.aggregates) {
                    group.aggregates = [aggregate];
                } else {
                    group.aggregates.push(aggregate);
                }

                return group;
            }
        }
    }

    RelatingDataGrid.prototype = $.extend(Object.create(FlatContentSelector.prototype), RelatingDataGrid.prototype);
    return (RelatingDataGrid);
});