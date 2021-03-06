define(["ContentSelectorBase", "text!FlatContentSelectorTemplate!strip"], function (ContentSelectorBase, html) {
    function FlatContentSelector(options) {
        var that = this;
        ContentSelectorBase.call(this, options, html);
        this.viewModelObjExt = {
            contentDataSource: {
                transport: {
                    read: {
                        type: "GET",
                        dataType: "json",
                        url: function (options) {
                            return that.resolveUrlFunc(options);
                        },
                        beforeSend: function (xhr) {
                            if (that.culture) {
                                xhr.setRequestHeader("SF_UI_CULTURE", that.culture);
                            }
                        }
                    },

                    parameterMap: function (options) {
                        return that.parameterMapFunc(options);
                    }
                },
                schema: {
                    data: "Items",
                    total: "TotalCount",
                    model: {
                        id: "Id"
                    }
                },
                requestEnd: function(e) {
                    that.markDataItemsTranslatedForCurrentCulture(e.response.Items);
                },
                pageSize: 20,
                page: 1,
                serverFiltering: true,
                serverPaging: true
            },

            onDataBound: function(e) {
                var listView = $(e.sender.element).data("kendoListView");
                //select
                that.selectItems(listView);
                if (this.contentDataSource.totalPages() <= 1) {
                    that.parentElement.find("[data-role=pager]").hide();
                } else {
                    that.parentElement.find("[data-role=pager]").show();
                }
            },
            onDataItemSelect: function (e) {
                //only in multiple selection mode
                if (that.allowMultipleSelection) {
                    that.saveSelectedItem(e.target || e.srcElement);
                }
            },
            onSelected: function (e) {
                //only in single selection mode
                if (!that.allowMultipleSelection) {
                    var listView = $(e.sender.element).data("kendoListView");
                    var data = this.contentDataSource.view();
                    that.selectedDataItems = $.map(e.sender.select(), function (item) {
                        return data[$(item).index()];
                    });
                }
            }
        }
        this.dataTemplateSingle = null;
        this.dataTemplateMultiple = null;
        this.listViewElement = null;
        this.allowMultipleSelection = false;
        return (this);
    }

    FlatContentSelector.prototype = {

        init: function () {
            $(this.parentElement).html(this.template);
            this.listViewElement = $(this.parentElement).find('#contentListView');
            $(this.listViewElement).attr('data-selectable', !this.allowMultipleSelection);
            this.viewModelObjExt.contentDataSource = new kendo.data.DataSource(this.viewModelObjExt.contentDataSource);

            if (this.allowMultipleSelection) {
                $(this.listViewElement).attr('data-template', "dataTemplateMultiple");
                if (this.dataTemplateMultiple != null) {
                    $(this.parentElement).find('#dataTemplateMultiple').html(this.dataTemplateMultiple);
                }
            } else {
                $(this.listViewElement).attr('data-template', "dataTemplateSingle");
                if (this.dataTemplateSingle != null) {
                    $(this.parentElement).find('#dataTemplateSingle').html(this.dataTemplateSingle);
                }
            }

            ContentSelectorBase.prototype.init.call(this);
        },

        setSelectedDataItems: function(value) {
            ContentSelectorBase.prototype.setSelectedDataItems.call(this, value);
            this.selectItems(this.listViewElement.data("kendoListView"));
        },

        selectItems: function (listView) {
            this.unselect(listView);
            for (var i = 0; i < this.selectedDataItems.length; i++) {
                var dataItem = listView.dataSource.get(this.selectedDataItems[i].id || this.selectedDataItems[i].Id);
                if (dataItem) {
                    var itemElements = listView.items();
                    var dataItemElement = jQuery.grep(itemElements, function (element) {
                        return ($(element).data("uid") == dataItem.uid);
                    });
                    if (this.allowMultipleSelection) {
                        var checkBox = $(dataItemElement).find("input:checkbox");
                        $(checkBox).attr("checked", "checked");
                    }
                    else {
                        listView.select(dataItemElement);
                    }
                }
            }
        },

        unselect: function (listView) {
            if (this.allowMultipleSelection) {
                var itemElements = listView.items();
                var checkBoxes = itemElements.find("input:checkbox");
                $(checkBoxes).prop("checked", false);
            }
        },

        saveSelectedItem: function (checkBox) {
            var dataItemId = $(checkBox).val();
            var dataItem = this.viewModel.contentDataSource.get(dataItemId);
            if (checkBox.checked) {
                this.addDataItemToSelected(dataItem);
            } else {
                this.removeDataItemFromSelected(dataItem);
            }
        }
    };
    FlatContentSelector.prototype = $.extend(Object.create(ContentSelectorBase.prototype), FlatContentSelector.prototype);
    return (FlatContentSelector);
});