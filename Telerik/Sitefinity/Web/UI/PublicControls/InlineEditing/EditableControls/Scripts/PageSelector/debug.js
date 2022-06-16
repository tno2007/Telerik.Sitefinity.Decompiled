﻿define(["HierarchicalContentSelector"], function (HierarchicalContentSelector) {
    function PageSelector(options) {
        HierarchicalContentSelector.call(this, options);

        this.siteBaseUrl = options.siteBaseUrl;
        this.isMultisite = options.isMultisite;
        this.serviceUrl = "Sitefinity/Services/Pages/PagesService.svc";
        this.multisiteDataUrl = "restapi/sitefinity/inlineediting/multisitedata";
        this.currentSiteRootNodeId = options.currentSiteRootNodeId;
        this.setTextToDisplay = options.setTextToDisplay;
        this.selectPageInfo = options.selectPageInfo;
        this.isOrderingHierarchy = false;

        return (this);
    }

    PageSelector.prototype = {

        init: function (options) {
            this.dataTemplate = '#= data.item.Title.PersistedValue #';
            if (this.isMultisite) {
                this.initWithSiteSelector();
            } else {
                this.initPageSelector();
            }
        },

        parameterMapFunc: function (options) {
            var queryObj = {
                sortExpression: "Title ASC",
                itemType: "Telerik.Sitefinity.Pages.Model.PageNode",
                hierarchyMode: true,
                sf_site: this.viewModel.selectedSite ? this.viewModel.selectedSite.SiteId : null
            };
            if (this.isSearchRequest()) {
                queryObj.take = 50,
                queryObj.filter = this.generateFilterExpression("Title", this.viewModel.searchValue),
                queryObj.hierarchyMode = false
            } else {
                queryObj.hierarchyMode = true;
            }

            return queryObj;
        },

        resolveUrlFunc: function (options) {
            var rootId = options.Id ? options.Id : this.currentSiteRootNodeId;
            if (this.selectPageInfo) {
                this.currentSiteRootNodeId = this.selectPageInfo.RootNodeId;
                return String.format("{0}/predecessor/{1}/", this.getServiceUrl(), this.selectPageInfo.PageNodeId);
            }
            return String.format("{0}/?root={1}", this.getServiceUrl(), rootId);
        },

        initWithSiteSelector: function () {
            var siteSelectorTemplate = ' \
                     <div class="sfSiteSelector" data-bind="visible: isSiteSelectorVisible"><label>Sites: </label>  \
                        <select data-bind="source: sitesDataSource, value: selectedSite"  data-value-field="SiteMapRootNodeId" data-text-field="Name" /> \
                     </div> \
                    ';
            this.template = siteSelectorTemplate + this.template;

            var that = this;
            $.ajax({
                type: "GET",
                contentType: "application/json;",
                cache: false,
                url: this.siteBaseUrl + this.multisiteDataUrl,
                success: function (data, textStatus, jqXHR) {
                    that.viewModelObjExt.sitesDataSource = data;
                    that.viewModelObjExt.selectedSite = that.getSelectedSite(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    that.viewModelObjExt.sitesDataSource = [];
                    that.viewModelObjExt.selectedSite = {};
                },
                complete: function (jqXHR, textStatus, errorThrown) {
                    that.initPageSelector();
                    if ($(that.parentElement).find("div.sfSiteSelector select").length > 0) {
                        $(that.parentElement).find("div.sfSiteSelector select").change(Function.createDelegate(that, that.siteChangeHandler));
                        $(that.parentElement).find("div.sfSiteSelector select").change();
                    }
                }
            });
        },

        siteChangeHandler: function (e) {
            e.preventDefault();
            if (this.viewModel.selectedSite && this.currentSiteRootNodeId != this.viewModel.selectedSite.SiteMapRootNodeId) {
                this.currentSiteRootNodeId = this.viewModel.selectedSite.SiteMapRootNodeId;
                this.viewModel.contentDataSource.read();
            }
        },

        getSelectedSite: function (data) {
            var site = null,
                requestedSiteId = this.selectPageInfo != null ? this.selectPageInfo.RootNodeId : this.currentSiteRootNodeId;

            for (var i = 0; i < data.length; i++) {
                if (data[i].SiteMapRootNodeId == requestedSiteId) {
                    site = data[i];
                    break;
                }
            }
            return site;
        },

        initPageSelector: function () {
            var that = this;
            this.viewModelObjExt.isSiteSelectorVisible = this.isMultisite && this.viewModelObjExt.sitesDataSource.length > 1;
            this.viewModelObjExt.onSelected = function (e) {
                if (!this.allowMultipleSelection) {
                    var treeView = $(e.sender.element).data("kendoTreeView");
                    var dataItem = treeView.dataItem(e.node);
                    that.selectedDataItems = [dataItem];
                    if (typeof that.setTextToDisplay === 'function') {
                        that.setTextToDisplay(dataItem.Title);
                    }
                }
                that.selectPageInfo = null;
            }

            this.viewModelObjExt.onDataBound = function (e) {
                if (that.isOrderingHierarchy) {
                    return;
                }

                var treeView = $(e.sender.element).data("kendoTreeView");
                var currentDataSourceView = null;

                if (typeof e.node === 'undefined') {
                    currentDataSourceView = this.contentDataSource.view();
                    that.selectNodes(currentDataSourceView, treeView);
                    that.validateTranslations(currentDataSourceView, treeView);
                } else {
                    var dataItem = treeView.dataItem(e.node);
                    that.selectNodes([dataItem], treeView);
                    currentDataSourceView = dataItem.children.view();
                    that.validateTranslations(currentDataSourceView, treeView);
                }

                if (currentDataSourceView != null && that.selectPageInfo != null) {
                    var selectedNode = that.getNodeById(currentDataSourceView, that.selectPageInfo.PageNodeId);
                    if (selectedNode) {
                        that.setSelectedDataItems([selectedNode]);
                        var nodeElement = treeView.findByUid(selectedNode.uid);
                        treeView.select(nodeElement);
                    }
                    that.orderHierarchy(currentDataSourceView);
                }
            }

            HierarchicalContentSelector.prototype.init.call(this);
            
            var tree = $(this.treeViewElement).data("kendoTreeView");
            tree.setOptions({ template: this.dataTemplate });
            tree.bind("expand", function (e) {
                if (that.isOrderingHierarchy) {
                    e.preventDefault();
                }
            });
        },

        orderHierarchy: function (currentDataSourceView) {
            this.isOrderingHierarchy = true;
            var treeView = $(this.treeViewElement).data("kendoTreeView");
            var itemLevels = [];
            var itemHierarchyInfo = [];
            var dataItem = null;
            for (var i = 0; i < this.viewModel.contentDataSource.view().length ; i++) {
                dataItem = this.viewModel.contentDataSource.view()[i];
                dataItem.loaded(true);
                itemLevels.push(-1);
                itemHierarchyInfo.push({
                    DataItemId: dataItem.Id,
                    DataItemParentId: dataItem.ParentId,
                    Uid: dataItem.uid
                });
            }
            var rootId = this.selectPageInfo ? this.selectPageInfo.RootNodeId  : this.currentSiteRootNodeId;
            var currentLevel = 0;
            var itemsLength = itemHierarchyInfo.length;

            while (this.hasLevelToOrder(itemLevels)) {
                // set root level first
                if (currentLevel == 0) {
                    for (var k = 0; k < itemsLength; k++) {
                        if (itemHierarchyInfo[k].DataItemParentId == rootId) {
                            itemLevels[k] = currentLevel;
                        }
                    }
                } else {
                    for (var childIdx = 0; childIdx < itemsLength; childIdx++) {
                        // node is not ordered and has level -1
                        if (itemLevels[childIdx] == -1) {
                            // check if has parent on previous level
                            var parentLvl = currentLevel - 1;
                            for (var parentIdx = 0; parentIdx < itemsLength; parentIdx++) {
                                // potential parent index
                                if (itemLevels[parentIdx] == parentLvl) {
                                    if (itemHierarchyInfo[parentIdx].DataItemId == itemHierarchyInfo[childIdx].DataItemParentId) {
                                        // we have a parent and should move child under it and mark it's level
                                        var childDataElement = treeView.findByUid(itemHierarchyInfo[childIdx].Uid);
                                        var parentDataElement = treeView.findByUid(itemHierarchyInfo[parentIdx].Uid);
                                        treeView.append(childDataElement, parentDataElement);
                                        itemLevels[childIdx] = currentLevel;
                                    }                                    
                                }
                            }
                        }
                    }
                }
                currentLevel++;

                // set this as a precaution if hierarchy could not be build
                if (currentLevel > 1000)
                    break;
            }
            this.selectPageInfo = null;
            this.isOrderingHierarchy = false;
        },

        hasLevelToOrder: function (levels) {
            for (var i = 0; i < levels.length; i++) {
                if (levels[i] == -1)
                    return true;
            }
            return false;
        }

    };
    PageSelector.prototype = $.extend(Object.create(HierarchicalContentSelector.prototype), PageSelector.prototype);
    return (PageSelector);
});