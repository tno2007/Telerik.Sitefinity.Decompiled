define(["ContentSelectorBase", "text!HierarchicalContentSelectorTemplate!strip"], function (ContentSelectorBase, html) {
    function HierarchicalContentSelector(options) {
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
                        id: "Id",
                        hasChildren: "HasChildren"
                    }
                },
                requestEnd: function (e) {
                    that.markDataItemsTranslatedForCurrentCulture(e.response.Items);
                    that.setTemplate();
                },
                change: function (e) {
                    if (e.items.length == 1) {
                        var dataItem = e.items[0];
                        if (that.allowMultipleSelection) {
                            if (dataItem.checked) {
                                that.addDataItemToSelected(dataItem);
                            } else if (dataItem.checked !== undefined) {
                                that.removeDataItemFromSelected(dataItem);
                            }
                        }
                    }
                },
                serverFiltering: true
            },
            onDataBound: function (e) {
                var treeView = $(e.sender.element).data("kendoTreeView");
                if (typeof e.node === 'undefined') {
                    that.selectNodes(this.contentDataSource.view(), treeView);
                    that.validateTranslations(this.contentDataSource.view(), treeView);
                } else {
                    that.selectNodes([treeView.dataItem(e.node)], treeView);
                    that.validateTranslations(e.sender.dataItem(e.node).children.view(), treeView);
                }
            },
            onSelected: function (e) {
                if (!that.allowMultipleSelection) {
                    var treeView = $(e.sender.element).data("kendoTreeView");
                    var dataItem = treeView.dataItem(e.node);
                    that.selectedDataItems = [dataItem];
                }
            }
        };

        this.treeViewElement = null;
        this.timerId = null;
        this.inputDelay = 500;
        return (this);
    }

    HierarchicalContentSelector.prototype = {

        init: function () {
            $(this.parentElement).html(this.template);
            this.treeViewElement = $(this.parentElement).find('#contentTreeView');
            $(this.treeViewElement).attr('data-checkboxes', this.allowMultipleSelection);
            this.viewModelObjExt.contentDataSource = new kendo.data.HierarchicalDataSource(this.viewModelObjExt.contentDataSource);
            ContentSelectorBase.prototype.init.call(this);

            // Fixes an issue with the inline editing after the Kendo UI upgrade to version 2016.1.112 (2016 Q1).
            // The rebind function is called to force the binding of the kendoTreeView dataSource during the
            // HierarchicalContentSelector initialization.
            ContentSelectorBase.prototype.rebind.call(this);
        },

        setSelectedDataItems: function (value) {
            ContentSelectorBase.prototype.setSelectedDataItems.call(this, value);
            this.selectNodes(this.viewModel.contentDataSource.view(), this.treeViewElement.data('kendoTreeView'));
        },

        selectNodes: function (nodes, treeView) {
            if (this.allowMultipleSelection) {
                this.checkNodes(nodes, this.selectedDataItems);
            } else if (this.selectedDataItems[0]) {
                var nodeToSelect = this.getNodeById(nodes, this.selectedDataItems[0].Id);
                if (nodeToSelect) {
                    var nodeElement = treeView.findByUid(nodeToSelect.uid);
                    treeView.select(nodeElement);
                }
            }
        },

        validateTranslations: function (nodes, treeView) {
            if (this.isMultilingual()) {
                if (!treeView) {
                    treeView = $(this.treeViewElement).data("kendoTreeView")
                }

                for (var i = 0; i < nodes.length; i++) {
                    if (!nodes[i].IsTranslated) {
                        var nodeElement = treeView.findByUid(nodes[i].uid);
                        treeView.enable(nodeElement, false);
                    }
                }
            }
        },

        getNodeById: function (nodes, dataItemId) {
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].id == dataItemId) {
                    return nodes[i];
                }
            }
        },

        checkNodes: function (nodes, dataItems) {
            for (var i = 0; i < nodes.length; i++) {
                this.checkNode(nodes[i], dataItems);
            }
        },

        checkNode: function (node, dataItems) {
            var found = false;
            for (var i = 0; i < dataItems.length; i++) {
                if (node.id == dataItems[i].Id) {
                    node.set("checked", true);
                    found = true;
                    break;
                }
            }
            if (!found) {
                node.set("checked", false);
            }
            if (node.hasChildren) {
                this.checkNodes(node.children.view(), dataItems);
            }
        },

        setTemplate: function () {
            var treeView = this.treeViewElement.data('kendoTreeView');
            var template = '\
                    #= data.item.Title#\
                    #if(data.item.IsTranslated == false) {#\
                        <span>(@(Res.Get<LocalizationResources>().NotTranslated))</span>\
                    #}#';
            treeView.setOptions({ template: this.dataTemplate || template });
        }
    };
    HierarchicalContentSelector.prototype = $.extend(Object.create(ContentSelectorBase.prototype), HierarchicalContentSelector.prototype);
    return (HierarchicalContentSelector);
});