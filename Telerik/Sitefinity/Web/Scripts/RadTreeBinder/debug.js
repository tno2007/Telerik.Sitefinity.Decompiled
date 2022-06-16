﻿/* RadTreeBinder class */

Telerik.Sitefinity.Web.UI.RadTreeBinder = function () {
    this._treeView = null;
    this._template = null;
    this._lastExpandedNode = null;
    this._orginalServiceBaseUrl = null;
    this._serviceBaseUrl = null;
    this._serviceChildItemsBaseUrl = null;
    this._serviceTreeUrl = null;
    this._changeParentServiceUrl = null;
    this._loadingText = null;
    this._loadMoreText = null;
    this._loadingNodeSuffixValue = null;
    this._servicePredecessorBaseUrl = null;
    this._parentDataKeyName = null;
    this._allowMultipleSelection = null;
    this._rootTaxonID = null;

    //Binding indicators
    this._isTargetLibrary = false;

    this._isHierachicalBind = false;
    this._startBindingFromRoot = false;
    this._bindingMode = 0;

    this._FlatRootBind = 0;
    this._FlatChildBind = 1;
    this._HierarchicalPredecessorRootBind = 2;
    this._HierarchicalTreeRootBind = 3;

    //TODO Drag&Drop functionality should be extracted as some sort of behavior plug-in.
    this._enableDragAndDrop = null;

    this._destionationNode = null;
    this._sourceNodes = [];
    this._placePosition = null;

    //TODO Initial expanding functionality should be extracted as some sort of behavior plug-in.
    this._enableInitialExpanding = false;
    this._expandedNodesDictionary = {};
    this._nodeCollapsedDelegate = null;
    this._expandedNodesCookieName = null;

    this._initialExpandingNodesLimit = null;
    this._InitialExpandingNodesPerLevelLimit = null;
    this._initialExpandingNodesPerSubtreeLimit = null;
    this._initialExpandingSubtreesLimit = null;

    this._nodeExpandingDelegate = null;
    this._nodeCheckedDelegate = null;
    this._nodeDroppingDelegate = null;
    this._nodeDragStartDelegate = null;
    this._nodeDraggingDelegate = null;
    this._nodeDroppedtDelegate = null;
    this._parentChangeSuccessDelegate = null;
    this._parentChangeFailureDelegate = null;
    this._placeAfterSuccessDelegate = null;
    this._placeAfterFailureDelegate = null;
    this._placeBeforeSuccessDelegate = null;
    this._placeBeforeFailureDelegate = null;
    this._itemDomCreatedDelegate = null;
    this._moveSuccessDelegate = null;
    this._moveFailureDelegate = null;
    this._onDocumentMouseUpDelegate = null;
    this._hierarchicalTreeRootBindModeEnabled = null;
    this._nodeClickedAdded = false;
    this._pagingLinkCssClass = "sfLnk sfRequiredItalics sfBiggerTxt sfLightTxt";

    this._loadMoreTemplate = '<div class="sfFirstTreeTableColumn">{0}</div>';
    this._supportsTaxonomyChildPaging = null;
    this._taxonomyChildPagingSize = null;

    Telerik.Sitefinity.Web.UI.RadTreeBinder.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.RadTreeBinder.prototype = {
    // set up and tear down
    initialize: function () {
        this._treeView = $find(this._targetId);
        this._template = new Sys.UI.Template($get(this._clientTemplates[0]));
        this._serviceBaseUrl = this.get_serviceBaseUrl();
        this._orginalServiceBaseUrl = this._serviceBaseUrl;
        this._loadingNodeSuffixValue = "A417937C-EE90-4bbf-97F0-AABD13691B4D";

        if (this._parentChangeSuccessDelegate === null) {
            this._parentChangeSuccessDelegate = Function.createDelegate(this, this._parentChangeSuccess);
        }
        if (this._parentChangeFailureDelegate === null) {
            this._parentChangeFailureDelegate = Function.createDelegate(this, this._parentChangeFailure);
        }

        if (this._placeAfterSuccessDelegate === null) {
            this._placeAfterSuccessDelegate = Function.createDelegate(this, this._placeAfterSuccess);
        }
        if (this._placeAfterFailureDelegate === null) {
            this._placeAfterFailureDelegate = Function.createDelegate(this, this._placeAfterFailure);
        }

        if (this._placeBeforeSuccessDelegate === null) {
            this._placeBeforeSuccessDelegate = Function.createDelegate(this, this._placeBeforeSuccess);
        }
        if (this._placeBeforeFailureDelegate === null) {
            this._placeBeforeFailureDelegate = Function.createDelegate(this, this._placeBeforeFailure);
        }
        if (this._moveSuccessDelegate === null) {
            this._moveSuccessDelegate = Function.createDelegate(this, this._moveSuccess);
        }
        if (this._moveFailureDelegate === null) {
            this._moveFailureDelegate = Function.createDelegate(this, this._moveFailure);
        }
        if (this._itemDomCreatedDelegate === null) {
            this._itemDomCreatedDelegate = Function.createDelegate(this, this._itemDomCreatedHandler);
        }

        if (this._nodeCollapsedDelegate === null) {
            this._nodeCollapsedDelegate = Function.createDelegate(this, this._nodeCollapsedHandler);
        }

        this._nodeExpandingDelegate = Function.createDelegate(this, this._nodeExpanding);
        this._treeView.add_nodeExpanding(this._nodeExpandingDelegate);
        this._nodeCheckedDelegate = Function.createDelegate(this, this._nodeChecked);
        this._treeView.add_nodeChecked(this._nodeCheckedDelegate);

        this._pageLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._pageLoadDelegate);

        var loadingTemplate = '<div class="RadAjax"><div class="raDiv">{0}</div><div class="raColor"></div></div>';
        loadingTemplate = String.format(loadingTemplate, this._loadingText);
        $('form').eq(0).after(loadingTemplate);
        $('.RadAjax').hide();

        this._onDocumentMouseUpDelegate = Function.createDelegate(this, this._onDocumentMouseUpHandler);
        if ($telerik.isIE) {
            // there is an event handler in rad tree view for mouseup event registered for the document
            // but since the event handlers in IE are executed LIFO the handler here is executed after the handler in rad tree view
            // so we atach the event to document.body and take advantage from the fact that in IE events bubble, 
            // i.e. the body event handler is executed before that of its parent - the document
            $addHandler(document.body, "mouseup", this._onDocumentMouseUpDelegate);
        }
        else {
            $addHandler(document, "mouseup", this._onDocumentMouseUpDelegate);
        }

        Telerik.Sitefinity.Web.UI.RadTreeBinder.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Sys.Application.remove_load(this._pageLoadDelegate);
        delete this._pageLoadDelegate;

        if (this._treeView) {
            this._treeView.remove_nodeExpanding(this._nodeExpandingDelegate);
            this._treeView.remove_nodeChecked(this._nodeCheckedDelegate);
            this._removeDraggingEvents();
        }

        if ($telerik.isIE) {
            $removeHandler(document.body, "mouseup", this._onDocumentMouseUpDelegate);
        }
        else {
            $removeHandler(document, "mouseup", this._onDocumentMouseUpDelegate);
        }

        this._treeView = null;
        this._template = null;
        this._lastExpandedNode = null;
        delete this._nodeExpandingDelegate;
        delete this._nodeCheckedDelegate;
        delete this._nodeDroppingDelegate;
        delete this._nodeDraggingDelegate;
        delete this._nodeDragStartDelegate;
        delete this._parentChangeSuccessDelegate;
        delete this._parentChangeFailureDelegate;
        delete this._placeAfterSuccessDelegate;
        delete this._placeAfterFailureDelegate;
        delete this._placeBeforeSuccessDelegate;
        delete this._placeBeforeFailureDelegate;
        delete this._itemDomCreatedDelegate;
        delete this._nodeCollapsedDelegate;
        delete this._onDocumentMouseUpDelegate;

        Telerik.Sitefinity.Web.UI.RadTreeBinder.callBaseMethod(this, "dispose");
    },

    _onLoad: function () {
        this.add_onItemDomCreated(this._itemDomCreatedDelegate);
        //TODO Initial expanding functionality should be extracted as some sort of behavior plug-in.
        if (this._enableInitialExpanding) {
            this._treeView.add_nodeCollapsed(this._nodeCollapsedDelegate);
        }
        this._treeView.set_enableDragAndDrop(this._enableDragAndDrop);
        this._treeView.set_enableDragAndDropBetweenNodes(this._enableDragAndDrop);
        if (this._enableDragAndDrop) {
            this._subscribeToDraggingEvents();
        }
    },

    // Overridden method from the ClientBinder. This method binds data to the target control.
    // Depending on the specified parameters the a different portion of the tree will be binded.
    // Use the dataKey parameter if you want to bind all children of a given node. 
    // The dataKey must be se to the Id of the node which children will be bound.
    // If the context parameter is specified with a node id the binder will be bound to the sub tree of the nodes hierarchy
    // that ends at the specified node id.
    // If the context is an object that has as a 'nodeIdsToExpand' property containing a list of node ids 
    // the tree view will be bound to a sub tree that contains all of the specified nodes.
    DataBind: function (dataKey, context) {
        // hide the tree view here and show once it's bound
        this._showHideLoadingImage(true);

        Telerik.Sitefinity.Web.UI.RadTreeBinder.callBaseMethod(this, "DataBind", [null, context]);

        var expandedNodes = null;
        if (this._enableInitialExpanding && !dataKey) {
            // context is sometimes not an object (it's a string), so we cannot check for existing properties with "in".
            // instead we directly use context.[name of property], which would return undefined if not there
            if (context && context.nodeIdsToExpand) {
                expandedNodes = context.nodeIdsToExpand;
            }
            else {
                expandedNodes = this._getExpandingNodesCookieData();
            }
        }
        this._configureBindingMode(dataKey, context, expandedNodes);

        // binding mode should be configured before clientManager url params are set
        var clientManager = this.get_manager();
        this._setManagerParams(clientManager);

        var data = null;
        if (this._hierarchicalTreeRootBindModeEnabled == true
            && this._bindingMode == this._HierarchicalTreeRootBind) {
            data = expandedNodes;
        }

        clientManager.GetItemCollection(this, null, null, null, context, data);
    },

    // This method binds the form, with the specified sort expression. It is a shorthand to setting the 
    // sortExpression property and calling the bind.
    Sort: function (sortExpression) {
        this._sortExpression = sortExpression;
        this.DataBind();
    },

    // This method binds the form, with the specified filter expression. It is a shorthand to setting the 
    // filterExpression property and calling the bind.
    Filter: function (filterExpression) {
        this._filterExpression = filterExpression;
        this.DataBind();
    },

    // Overridden method from the ClientBinder. The ClientManager will call this method after it receives data
    // (collection of items) from the service and pass the CollectionContext (defined on the server) to it.
    BindCollection: function (data, context) {

        var dataKey = this._get_globalDataKey();

        // NOTE: We check the service's recieved ID if it matches the current selected node Id. 
        // If they are not equals we skip the treeview loading operation. This will happen when the number the server requests are greater then the recieved responses.
        // This will lead to incorrect binder reset and bug as well.
        if (dataKey !== undefined && context !== undefined && context !== null) {

            var id = context;
            if (typeof (context) !== 'string' && context[this._getItemKeyName()]) {

                id = context[this._getItemKeyName()];
            }
            if (id !== dataKey) {

                return;
            }
        }

        data = this.DeserializeData(data);
        this._dataBindingHandler(data);

        this._startProcessingHandler();

        if (this._startBindingFromRoot) {
            this._displayedItemsCount = data.Items.length;
        }
        else {
            this._displayedItemsCount += data.Items.length;
        }

        if (this._isHierachicalBind) {
            this._bindHierarchicalCollection(data, context);
        }
        else {
            this._bindFlatCollection(data);
        }
        this._endProcessingHandler();

        this._resetBindingMode();

        this._showHideLoadingImage(false);
    },

    // Clears all selected items; both in user interface and data.
    clearSelection: function () {
        if (this._allowMultipleSelection) {
            var checkedNodes = this._treeView.get_checkedNodes();
            var checkedNodesCount = checkedNodes.length;
            while (checkedNodesCount--) {
                checkedNodes[checkedNodesCount].uncheck();
            }
        }
        else {
            this._treeView.unselectAllNodes();
            if (this._treeView.uncheckAllNodes) {
                this._treeView.uncheckAllNodes();
            }
        }
    },

    //Selects/checks the given items. The items parameter must contain dataItems of the nodes to check.
    setSelectedItems: function (items, check, select) {
        if (!items)
            return;
        var treeTable = this._treeView;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            var itemKey = this.getItemKey(item);
            var node = treeTable.findNodeByValue(itemKey);
            if (node) {
                if (select) node.select();
                if (check) node.check();
            }
        }
    },

    //Selects/checks the nodes with the given values. 
    setSelectedValues: function (values, check, select) {
        if (!values)
            return;
        var treeTable = this._treeView;
        for (var i = 0; i < values.length; i++) {
            var node = treeTable.findNodeByValue(values[i]);
            if (node) {
                if (select) node.select();
                if (check) node.check();
            }
        }
    },

    selectByIds: function (ids) {
        this.setSelectedValues(ids, true, true);
    },

    _handleTreeDataBound: function (sender, args) {

        this._showHideLoadingImage(false);
    },

    _showHideLoadingImage: function (toShow) {
        var loadingElement = $('form').eq(0).next().filter(".RadAjax");
        // hide the tree view here and show once it's bound
        if (toShow) {
            loadingElement.show();
            if ($(".sfNewContentForm") && $(".sfNewContentForm").height() > 0) {
                //loadingElement.height($(".sfNewContentForm").height());
                var scrollTopHtml = jQuery("html").eq(0).scrollTop();
                var scrollTopBody = jQuery("body").eq(0).scrollTop();
                var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody);
                loadingElement.css('cssText', 'top: ' + scrollTop + 'px !important');
            }
        }
        else {
            if ($(".sfNewContentForm") && $(".sfNewContentForm").height() > 0)
                loadingElement.css('cssText', 'top: 0px !important');
            loadingElement.hide();
        }
    },

    // Binds the items when the tree gets populated after the initial request.
    _bindFlatCollection: function (data) {
        data = this.DeserializeData(data);

        if (!this._nodeClickedAdded) {
            var binder = this;
            this._treeView.add_nodeClicking(function (sender, args) {
                binder.invokeClickHandler(binder, sender, args)
            });
            this._nodeClickedAdded = true;
        }

        this._treeView.trackChanges();

        // clear all child nodes (the one that was set there as a hack)
        var parentNode = this._getParentNode();
        // before rebinding, clear the previous items from node
        parentNode.get_nodes().clear();
        var len = data.Items.length;

        if (len > 0 && (data.FoldersContext || data.TaxonomyContext)) {
            var levels = {};
            var hierarchyLevelContext;
            var beginLinkToAdd = null;
            var endLinkToAdd = null;

            var firstItem = data.Items[0];
            var levelKey = firstItem.ParentId != null ? firstItem.ParentId : "00000000-0000-0000-0000-000000000000";

            //check if taxonomy
            levelKey = levelKey == "00000000-0000-0000-0000-000000000000" && firstItem.ParentTaxonId != null ? firstItem.ParentTaxonId : levelKey;

            if (levels[levelKey]) {
                hierarchyLevelContext = levels[levelKey];
            }
            else {
                var context = data.FoldersContext || data.TaxonomyContext;
                hierarchyLevelContext = context.filter(function (obj) {
                    return obj.Key == levelKey;
                })[0];

                if (hierarchyLevelContext) {
                    levels[levelKey] = hierarchyLevelContext.Value;
                    if (levels[levelKey].Skip > 0) {
                        beginLinkToAdd = { key: levelKey, level: levels[levelKey] };
                    }
                    if (levels[levelKey].Take > 0 && (levels[levelKey].Skip + levels[levelKey].Take < levels[levelKey].Total)) {
                        endLinkToAdd = { key: levelKey, level: levels[levelKey] };
                    }
                }
            }
        }

        if (beginLinkToAdd) {
            var textNodeTitle = this._loadMoreText;
            var beginShowMoreNode = this._createTextNode(textNodeTitle, { hierarchyLevelContext: beginLinkToAdd.level, linkType: "begin" }, this._getLoadMoreCss());
            var parentNodeCollection = parentNode.get_nodes();
            parentNodeCollection.add(beginShowMoreNode);
        }

        for (var bc = 0; bc < len; bc++) {
            var dataItem = data.Items[bc];

            this._ensureDataItemIntegrity(dataItem);
            this._itemDataBindingHandler(dataItem);

            var parentNodeCollection = parentNode.get_nodes();
            var node = this._createNode(dataItem, bc, data.IsGeneric);
            parentNodeCollection.add(node);

            // TODO: check if the _displayedItemsCount is valid as the index as the last parameter

            var element = node.get_element();
            var key = node.get_value();

            //TODO: remove when support of compound keys is ready
            var compoundKey = [];
            compoundKey[this._getItemKeyName()] = key;

            this._raiseItemDomCreated(compoundKey, dataItem, bc, element);
            this._itemDataBoundHandler(compoundKey, dataItem, bc, element);
        }

        if (endLinkToAdd) {
            var textNodeTitle = String.format(this._loadMoreTemplate, this._loadMoreText);
            var nodeText = this._supportsTaxonomyChildPaging ? textNodeTitle : this._loadMoreText;
            var endShowMoreNode = this._createTextNode(nodeText, { hierarchyLevelContext: endLinkToAdd.level, linkType: "end" }, this._getLoadMoreCss());
            var parentNodeCollection = parentNode.get_nodes();
            endShowMoreNode.set_checkable(false);
            parentNodeCollection.add(endShowMoreNode);
        }

        this._treeView.commitChanges();
        this._dataBoundHandler(data);
    },

    _getLoadMoreCss: function () {
        if (this._supportsTaxonomyChildPaging) {
            return this._pagingLinkCssClass + ' sfTaxonomiesLoadMore';
        }
        return this._pagingLinkCssClass;
    },

    // Binds the items when the tree is already populated and an user action requires loading of new nodes (expanding, drag, drop and etc.)
    _bindHierarchicalCollection: function (data, context) {
        var len = data.Items.length;
        var itemIterator = len;
        var nodes = [];
        var levels = {};
        var parentKeyName = this.get_parentDataKeyName();
        if (!parentKeyName) {
            throw "ParentKeyName is not set.";
        }
        var keyName = this._getItemKeyName();

        this._treeView.trackChanges();

        this._treeView.get_nodes().clear();
        //TODO: support of compund keys

        if (!this._nodeClickedAdded) {
            var binder = this;
            this._treeView.add_nodeClicking(function (sender, args) {
                binder.invokeClickHandler(binder, sender, args)
            });
            this._nodeClickedAdded = true;
        }

        var levelCount = { level: null, count: 0 };

        while (itemIterator--) {

            var dataItem = data.Items[itemIterator];
            var parentPropertyName = (data.FoldersContext) ? 'ParentId' : 'ParentTaxonId';
            var levelKey = dataItem[parentPropertyName];
            var hierarchyLevelContext;
            var beginLinkToAdd = null;
            var endLinkToAdd = null;

            if (levelCount.level !== levelKey) {
                levelCount.count = 0;
                levelCount.level = levelKey;
            }

            var serviceContext = data.FoldersContext || data.TaxonomyContext;
            if (serviceContext) {

                if (levels[levelKey]) {
                    hierarchyLevelContext = levels[levelKey].context;
                }
                else {
                    hierarchyLevelContext = serviceContext
                        .filter(function (obj) {
                            return obj.Key === dataItem[parentPropertyName] ||
                                (obj.Key === "00000000-0000-0000-0000-000000000000" && dataItem[parentPropertyName] === null);
                        })[0];

                    if (hierarchyLevelContext) {
                        levels[levelKey] = { 'context': hierarchyLevelContext.Value };
                        if (levels[levelKey].context.Skip > 0) {
                            levels[levelKey].beginLinkToAdd = { key: levelKey, level: levels[levelKey].context };
                        }
                        if (levels[levelKey].context.Take > 0 && (levels[levelKey].context.Skip + levels[levelKey].context.Take < levels[levelKey].context.Total)) {
                            levels[levelKey].endLinkToAdd = { key: levelKey, level: levels[levelKey].context };
                        }
                    }
                }
            }

            if (levels[levelKey] && levelCount.count === 0 && levels[levelKey].endLinkToAdd) {

                var textNodeTitle = String.format(this._loadMoreTemplate, this._loadMoreText);
                var nodeText = this._supportsTaxonomyChildPaging ? textNodeTitle : this._loadMoreText;

                var endShowMoreNode = this._createTextNode(nodeText, { hierarchyLevelContext: levels[levelKey].endLinkToAdd.level, linkType: "end" }, this._getLoadMoreCss());
                endShowMoreNode.set_checkable(false);
                nodes.push({
                    'id': levels[levelKey].endLinkToAdd.key + 'endlink', 'data': {
                        'node': endShowMoreNode,
                        'dataItem': null,
                        'parentKey': dataItem[parentKeyName],
                        'key': dataItem[keyName]
                    }
                });
            }

            this._ensureDataItemIntegrity(dataItem);
            this._itemDataBindingHandler(dataItem);

            var nodeToAdd = this._createNode(dataItem, itemIterator, data.IsGeneric);
            var keyToAdd = nodeToAdd.get_value();

            nodes.push({
                'id': keyToAdd, 'data': {
                    'node': nodeToAdd,
                    'dataItem': dataItem
                }
            });

            levelCount.count++;

            if (levels[levelKey] && levelCount.count === levels[levelKey].context.Take && levels[levelKey].beginLinkToAdd) {
                var beginShowMoreNode = this._createTextNode(this._loadMoreText, { hierarchyLevelContext: levels[levelKey].beginLinkToAdd.level, linkType: "begin" }, this._getLoadMoreCss());
                nodes.push({
                    'id': levels[levelKey].beginLinkToAdd.key + 'beginlink', 'data': {
                        'node': beginShowMoreNode,
                        'dataItem': null,
                        'parentKey': dataItem[parentKeyName],
                        'key': dataItem[keyName]
                    }
                });
            }
        }

        var findNodeInNodes = function (nodesToIterate, id) {
            for (var j = 0; j < nodesToIterate.length; j++) {
                if (nodesToIterate[j].id === id)
                    return nodesToIterate[j];
            }
            return null;
        };

        nodes = nodes.reverse();

        for (var i = 0; i < nodes.length; i++) {

            var nodeData = nodes[i].data;
            var dataItem = nodeData.dataItem;

            var parentKey = null;
            var key = null;
            var node = nodeData.node;

            if (dataItem !== null) {
                parentKey = dataItem[parentKeyName];
                key = dataItem[keyName];
            } else {
                parentKey = nodeData.parentKey;
                key = nodeData.key;
            }

            //check if this is a root node.
            if (!parentKey || findNodeInNodes(nodes, parentKey) == null) {
                this._treeView.get_nodes().add(node);
            }
            else {
                var parentNodeData = findNodeInNodes(nodes, parentKey);
                var parentNodeCollection = parentNodeData.data.node.get_nodes();
                if (parentNodeCollection.get_count() == 1) {
                    //Hack to remove the loading node
                    if (parentNodeCollection.getNode(0).get_value() == this._get_loadingNodeValue(parentNodeData.data.node)) {
                        parentNodeCollection.clear();
                    }
                }
                parentNodeCollection.add(node);
                parentNodeData.data.node.set_expanded(true);
            }

            if (dataItem !== null) {
                var element = node.get_element();

                //TODO: remove when support of compound keys is ready
                var compoundKey = {};
                compoundKey[keyName] = key;

                this._raiseItemDomCreated(compoundKey, dataItem, i, element);
                this._itemDataBoundHandler(compoundKey, dataItem, i, element);
            }
        }

        if (!context || !(context.nodeIdsToExpand)) {
            this._syncExpandedNodes();
        }

        //this._treeView.commitChanges();
        this._dataBoundHandler(data);
    },

    // Overridden method from the ClientBinder. The ClientManager will call this method afer it receives data
    // (single item) from the service and pass the deserialized item to it.
    BindItem: function (data) {
        alert('BindItem function is not supported by RadTreeBinder. Please use Bind instead.');
    },
    // Overridden method from the ClientBinder. This function updates a set of items.
    UpdateItems: function (data, clearHandlers) {
        if (!data || !data.Items)
            return;
        var keyName = this._getItemKeyName();

        this._treeView.trackChanges();
        while (data.Items.length > 0) {
            var dataItem = data.Items.pop();
            this._itemDataBindingHandler(dataItem);
            var key = this._getItemKey(dataItem);
            var node = this._treeView.findNodeByValue(key);
            if (node._children !== undefined && node._children._array.length > 0) {
                for (var i = 0; i < node._children._array.length; i++) {
                    data.Items.push(node._children._array[i].get_dataItem());
                }
            }
            var container = document.createElement('div');
            var index = node.get_index();
            container.innerHTML = '';
            this.LoadItem(container, this._template, dataItem, data.IsGeneric, index);
            node._text = ""; //forces the node to use the innerHTML property of its textElement
            node.set_text(container.innerHTML);

            var element = node.get_element();
            //TODO: remove when support of compound keys is ready
            var compoundKey = {};
            compoundKey[keyName] = key;

            if (clearHandlers) {
                element = node.get_contentElement();
                $clearHandlers(element);
            }
            this._raiseItemDomCreated(compoundKey, dataItem, index, element);
            this._itemDataBoundHandler(compoundKey, dataItem, index, element);
        }
        this._treeView.commitChanges();
    },
    //Moves the selected nodes in the given direction
    // moveDirection can be 'up' or 'down'
    moveSelectedNodes: function (direction) {
        if (this._allowMultipleSelection) {
            var checkedNodes = this._treeView.get_checkedNodes();
            this._moveNodes(checkedNodes, direction);
        }
        else {
            var selectedNode = this._treeView.get_selectedNode();
            this._moveNodes([selectedNode], direction);
        }
    },

    // Moving nondes in a given direction by the nodes values.
    // direction can be 'up' or 'down'
    moveNodes: function (nodeValues, direction) {
        //TODO throw exception if the given nodes have different parents.
        var nodesToMove = this._getNodesFromNodeValues(nodeValues);
        this._moveNodes(nodesToMove, direction);
    },

    // Moving a single node in a given direction.
    // direction can be 'up' or 'down'
    moveNode: function (nodevalue, direction) {
        this.moveNodes([nodevalue], direction);
    },

    //Gets whether the currently selected nodes can be moved in the given direction or not.
    // direction can be 'up' or 'down'
    canMoveSelection: function (direction) {
        if (this._allowMultipleSelection) {
            var checkedNodes = this._treeView.get_checkedNodes();
            return this._canMoveNodes(checkedNodes, direction);
        }
        else {
            var selectedNode = this._treeView.get_selectedNode();
            return this._canMoveNodes([selectedNode], direction);
        }
    },

    //Gets whether the supplied nodes can be moved in the given direction or not.
    canMoveNodes: function (nodeValues, direction) {
        var nodes = this._getNodesFromNodeValues(nodeValues);
        return this._canMoveNodes(nodes, direction);
    },

    //Gets whether the supplied node can be moved in the given direction or not.
    canMoveNode: function (nodeValue, direction) {
        return this.canMoveNodes([nodeValue], direction);
    },

    //Gets whether the currently selected nodes are siblings or not.
    selectedNodesAreSiblings: function () {
        if (this._allowMultipleSelection) {
            var checkedNodes = this._treeView.get_checkedNodes();
            var checkedNodesLength = checkedNodes.length;
            var commonParent;
            if (checkedNodesLength > 0)
                commonParent = checkedNodes[0].get_parent();

            while (checkedNodesLength--) {
                var nodeToValidate = checkedNodes[checkedNodesLength];
                var parent = nodeToValidate.get_parent();
                if (parent !== commonParent) {
                    return false;
                }
            }
            return true;
        }
        else {
            return true;
        }
    },

    // Placing a node with the given value before or after the destination node.
    // placePosition: 'before' or 'after'
    placeNode: function (nodeValue, placePosition, destinationNodeValue) {
        this.placeNodes([nodeValue], placePosition, destinationNodeValue);
    },

    // Placing nodes with the given values before or after the destination node.
    // placePosition: 'before' or 'after'
    placeNodes: function (nodeValues, placePosition, destinationNodeValue) {
        var sourceNodes = this._getNodesFromNodeValues(nodeValues);
        var destinationNode = this._getNodeFromNodeValue(destinationNodeValue);
        this._placeNodes(sourceNodes, placePosition, destinationNode);
    },

    /* **************************** events **************************** */

    add_treeViewNodeDropping: function (delegate) {
        if (this._treeView) {
            this._treeView.add_nodeDropping(delegate);
        }
    },

    remove_treeViewNodeDropping: function (delegate) {
        if (this._treeView) {
            this._treeView.remove_nodeDropping(delegate);
        }
    },

    add_treeViewNodeDropped: function (delegate) {
        if (this._treeView) {
            this._treeView.add_nodeDropped(delegate);
        }
    },

    remove_treeViewNodeDropped: function (delegate) {
        if (this._treeView) {
            this._treeView.remove_nodeDropped(delegate);
        }
    },

    add_treeViewNodeDragStart: function (delegate) {
        if (this._treeView) {
            this._treeView.add_nodeDragStart(delegate);
        }
    },

    remove_treeViewNodeDragStart: function (delegate) {
        if (this._treeView) {
            this._treeView.remove_nodeDragStart(delegate);
        }
    },

    add_treeViewNodeDragging: function (delegate) {
        if (this._treeView) {
            this._treeView.add_nodeDragging(delegate);
        }
    },

    remove_treeViewNodeDragging: function (delegate) {
        if (this._treeView) {
            this._treeView.remove_nodeDragging(delegate);
        }
    },

    add_nodesPlacing: function (delegate) {
        this.get_events().addHandler("nodesPlacing", delegate);
    },

    remove_nodesPlacing: function (delegate) {
        this.get_events().removeHandler("nodesPlacing", delegate);
    },

    add_nodesPlaced: function (delegate) {
        this.get_events().addHandler("nodesPlaced", delegate);
    },

    remove_nodesPlaced: function (delegate) {
        this.get_events().removeHandler("nodesPlaced", delegate);
    },

    add_opertationFailed: function (delegate) {
        this.get_events().addHandler("operationFailed", delegate);
    },

    remove_opertationFailed: function (delegate) {
        this.get_events().removeHandler("operationFailed", delegate);
    },

    /* ********************* tree handling methods ********************* */

    _nodeExpanding: function (sender, args) {
        // cancel the default behavior of the tree
        args.set_cancel(true);

        var filterExpressionTemp = this._filterExpression;

        if (this.get_isTargetLibrary()) {
            this.set_filterExpression("");
        }

        var node = args.get_node();

        if (!node.get_enabled()) {
            return;
        }

        node.set_expanded(true);
        this._lastExpandedNode = node;

        if (this._enableInitialExpanding) {
            this._expandedNodesDictionary[node.get_value()] = node;
            this._setInitialExpandingCookie();
        }

        this.DataBind(node.get_value());

        this.set_filterExpression(filterExpressionTemp);
    },

    _nodeChecked: function (sender, eventArgs) {
    },

    _nodesPlacing: function (placePosition) {
        var args = this._getNodesPlacingEventArgs(this._sourceNodes, placePosition, this._destionationNode);
        var h = this.get_events().getHandler('nodesPlacing');
        if (h) h(this, args);
        return args;
    },

    _nodesPlaced: function () {
        var args = this._getNodesPlacingEventArgs(this._sourceNodes, this._placePosition, this._destionationNode);
        var h = this.get_events().getHandler('nodesPlaced');
        if (h) h(this, args);
        return args;
    },

    _operationFailed: function (operation) {
    },

    /* ********************* private methods ********************* */

    _getParentNode: function () {
        //Hack to determin the parent node of the subtree that was returned from the server.
        if (this._startBindingFromRoot) {
            return this._treeView;
        }
        var parentNode = this._treeView.findNodeByValue(this._get_globalDataKey());
        if (parentNode) {
            return parentNode;
        }
        return this._treeView;
    },

    _get_globalDataKey: function () {
        return this.get_globalDataKeys()[this._getItemKeyName()];
    },

    _createNode: function (dataItem, dataItemIndex, isGenericNode) {
        // create a new instance of RadTreeNode
        var node = this._createTreeNode(dataItem, dataItemIndex);
        // create the container; wrapper element
        // TODO: make, the container element tag configurable
        var container = document.createElement('div');
        container.innerHTML = '';

        this.LoadItem(container, this._template, dataItem, isGenericNode, dataItemIndex);
        node.set_text(container.innerHTML);

        var key = this._getItemKey(dataItem);
        node.set_value(key);

        // TODO: provide a way so that this can be checked from a property of the data item
        // NOTE: this is a hack, because RadTreeView does not allow setting "hasChildren" manually
        if (dataItem.HasChildren) {
            var loadingNode = new Telerik.Web.UI.RadTreeNode();
            loadingNode.set_text(this._loadingText);
            loadingNode.set_value(this._get_loadingNodeValue(node));
            loadingNode.set_checkable(false);
            node.get_nodes().add(loadingNode);
        }
        node.set_expandMode(Telerik.Web.UI.TreeNodeExpandMode.ClientSide);

        return node;
    },

    _createTextNode: function (text, value, cssClass) {
        var node = new Telerik.Sitefinity.Web.UI.RadTreeNode();
        node.set_value(value);
        node.set_text(text);
        node.set_expandMode(Telerik.Web.UI.TreeNodeExpandMode.ClientSide);
        if (cssClass) {
            node.set_cssClass(cssClass);
        }
        return node;
    },

    invokeClickHandler: function (binder, sender, args) {
        binder.showMore_onClick(sender, args);
    },

    showMore_onClick: function (sender, args) {
        var selectedNode = args.get_node();
        var parentNode = selectedNode._parent;
        var parentId = Telerik.Sitefinity.getEmptyGuid();
        if (selectedNode._parent._dataItem)
            parentId = selectedNode._parent._dataItem.Id;

        var serviceUrl = this.get_serviceBaseUrl();
        if (this._supportsTaxonomyChildPaging && parentId !== Telerik.Sitefinity.getEmptyGuid()) {
            serviceUrl = this._serviceChildItemsBaseUrl;
        }

        var serviceParamsPlaceHolder = '?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&hierarchyMode={hierarchyMode}';
        if (parentId !== Telerik.Sitefinity.getEmptyGuid()) {
            serviceUrl = serviceUrl + parentId + '/';
        }
        serviceUrl += serviceParamsPlaceHolder;

        var providerName = this._urlParams["provider"] ? this._urlParams["provider"] : '';
        var sortExpression = this._urlParams["sortExpression"] ? this._urlParams["sortExpression"] : 'Title';
        var hierarchyMode = this._urlParams["hierarchyMode"] ? this._urlParams["hierarchyMode"] : true;

        serviceUrl = serviceUrl.replace("{sortExpression}", sortExpression);
        serviceUrl = serviceUrl.replace("{filter}", '');
        serviceUrl = serviceUrl.replace("{providerName}", providerName);
        serviceUrl = serviceUrl.replace("{hierarchyMode}", hierarchyMode);

        var context = args.get_node().get_value();
        if (context.hierarchyLevelContext) {

            var skip = context.hierarchyLevelContext.Skip,
                take = context.hierarchyLevelContext.Take,
                total = context.hierarchyLevelContext.Total;

            if (context.linkType === 'begin') {
                skip -= take;
                if (skip < 0) skip = 0;
            } else {
                skip += take;
                if (skip > total) skip = total;
            }

            serviceUrl = serviceUrl.replace("{skip}", skip);
            serviceUrl = serviceUrl.replace("{take}", take);

            this._treeView.trackChanges();
            parentNode.get_nodes().remove(selectedNode);

            var loadingNode = this._getLoadMoreLoadingNode();
            parentNode.get_nodes().add(loadingNode);

            this._treeView.commitChanges();

            var that = this;
            $.ajax({
                type: "GET",
                url: serviceUrl,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function (xhr) {
                    if (that._uiCulture && that._supportsTaxonomyChildPaging) {
                        xhr.setRequestHeader("SF_UI_CULTURE", that._uiCulture);
                    }
                },
                success: function (data) {

                    var itemContext = data.FoldersContext || data.TaxonomyContext;
                    skip = itemContext[0].Value.Skip;
                    take = itemContext[0].Value.Take;
                    that._treeView.trackChanges();

                    parentNode.get_nodes().remove(loadingNode);

                    var parentId = null;
                    $.each(data.Items, function (i, v) {
                        parentId = v.ParentId;

                        var node = that._createNode(v, i, v.IsGeneric);
                        if (context.linkType === 'begin')
                            parentNode.get_nodes().insert(0 + i, node);
                        else
                            parentNode.get_nodes().add(node);

                        var element = node.get_element();
                        var compoundKey = [];
                        compoundKey[that._getItemKeyName()] = node.get_value();;

                        that._raiseItemDomCreated(compoundKey, v, i, element);
                        that._itemDataBoundHandler(compoundKey, v, i, element);
                    });

                    if (context.linkType === 'begin') {
                        if (skip > 0) {
                            var beginShowMoreNode = that._createTextNode(that._loadMoreText, { hierarchyLevelContext: { 'Skip': skip, 'Take': take, 'Total': total }, linkType: "begin" }, that._getLoadMoreCss());
                            parentNode.get_nodes().insert(0, beginShowMoreNode);
                        }
                    }

                    if (context.linkType === 'end') {
                        if (skip + take < total) {
                            var textNodeTitle = String.format(that._loadMoreTemplate, that._loadMoreText);
                            var nodeText = that._supportsTaxonomyChildPaging ? textNodeTitle : that._loadMoreText;
                            var endShowMoreNode = that._createTextNode(nodeText, { hierarchyLevelContext: { 'Skip': skip, 'Take': take, 'Total': total }, linkType: "end" }, that._getLoadMoreCss());
                            endShowMoreNode.set_checkable(false);
                            parentNode.get_nodes().add(endShowMoreNode);
                        }
                    }

                    that._treeView.commitChanges();
                    that._dataBoundHandler(data);
                },
                error: function (e) {
                    console.log(e);
                }
            });
        }
    },

    _getLoadMoreLoadingNode: function () {
        var loadingNode = new Telerik.Web.UI.RadTreeNode();
        loadingNode.set_text(String.format(this._loadMoreTemplate, this._loadingText));
        loadingNode.set_value({ linkType: "loading" });
        loadingNode.set_checkable(false);
        return loadingNode;
    },

    _createTreeNode: function (dataItem, dataItemIndex) {
        return new Telerik.Sitefinity.Web.UI.RadTreeNode(dataItem);
    },

    _get_loadingNodeValue: function (parentNode) {
        return parentNode.get_value() + this._loadingNodeSuffixValue;
    },

    getItemParentKey: function (dataItem) {
        return dataItem[this.get_parentDataKeyName()];
    },
    getItemKey: function (dataItem) {
        return this._getItemKey(dataItem);
    },

    _getItemKey: function (dataItem) {
        return dataItem[this._getItemKeyName()];
    },

    _getItemKeyName: function (dataItem) {
        if (this.get_dataKeyNames().length == 0) {
            alert('You must specify a data key name on the RadTreeBinder!');
            return;
        }
        if (this.get_dataKeyNames().length > 1) {
            alert('RadTreeBinder does not support composite keys at this moment.');
            return;
        }
        return this.get_dataKeyNames()[0];
    },

    _setManagerParams: function (clientManager) {
        var urlParams = this.get_urlParams();
        urlParams['hierarchyMode'] = true;
        if (this._supportsTaxonomyChildPaging) {
            urlParams['take'] = this._taxonomyChildPagingSize;
        }

        clientManager.set_urlParams(urlParams);
    },

    _getNodesPlacingEventArgs: function (sourceNodes, placePosition, destinationNode) {
        var nodesPlacingEventArgs = new Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs(sourceNodes, placePosition, destinationNode);
        return nodesPlacingEventArgs;
    },

    // TODO: rewrite DataBind method after beta, the mode should be passed as a parameter
    _configureBindingMode: function (dataKey, context, expandedNodes) {
        // This mode binds to a predecessor subtree of a given node (all nodes before a given node in the hirarchy + root nodes).
        if (context && !expandedNodes) {
            if (typeof (context) == 'string') {
                this._setPredecessorSubTreeMode(context);
            }
            else {
                this._setPredecessorSubTreeMode(context[this._getItemKeyName()]);
            }
        }
        else {
            if (dataKey) {
                // This mode binds to a node subtree (for example when expanding a node)
                this._setChildSubtreeMode(dataKey);
            }
            // This is the root binding mechanism
            else {
                if (expandedNodes && expandedNodes.length && expandedNodes.length > 0) {
                    this._setTreeMode();
                }
                else {
                    if (this._isRootTaxonSet()) {
                        this._setChildSubtreeMode(this.get_rootTaxonID());
                    }
                    else {
                        this._setRootMode();
                    }
                }
            }
        }
    },

    _ensureDataItemIntegrity: function (dataItem) {
        if (dataItem && dataItem.Title && !dataItem.Title.Value && !dataItem.HasTranslationSiblings) {
            dataItem.Title.Value = '';
            dataItem.HasTranslationSiblings = true;
        }
    },

    _setRootMode: function () {
        this._bindingMode = this._FlatRootBind;
        this._isHierachicalBind = false;
        this._startBindingFromRoot = true;
        if (!this._orginalServiceBaseUrl) {
            throw "OrginalServiceBaseUrl is not set.";
        }
        this.set_serviceBaseUrl(this._orginalServiceBaseUrl);
        this.set_globalDataKeys([]);
    },

    _setChildSubtreeMode: function (dataKey) {
        this._bindingMode = this._FlatChildBind;
        this._isHierachicalBind = false;
        this._startBindingFromRoot = false;
        if (!this._serviceChildItemsBaseUrl) {
            throw "ServiceChildItemsBaseUrl is not set.";
        }
        this.set_serviceBaseUrl(this._serviceChildItemsBaseUrl);
        this.get_globalDataKeys()[this._getItemKeyName()] = dataKey;
    },

    _setTreeMode: function () {
        this._bindingMode = this._HierarchicalTreeRootBind;
        this._isHierachicalBind = true;
        this._startBindingFromRoot = true;
        if (!this._serviceTreeUrl) {
            throw "ServiceTreeUrl is not set.";
        }
        this.set_serviceBaseUrl(this._serviceTreeUrl);
        this.set_globalDataKeys([]);
    },

    _setPredecessorSubTreeMode: function (dataKey) {
        this._bindingMode = this._HierarchicalPredecessorRootBind;
        this._isHierachicalBind = true;
        this._startBindingFromRoot = true;
        if (!this._servicePredecessorBaseUrl) {
            throw "ServicePredecessorBaseUrl is not set.";
        }
        this.set_serviceBaseUrl(this._servicePredecessorBaseUrl);
        this.get_globalDataKeys()[this._getItemKeyName()] = dataKey;
    },

    _resetBindingMode: function () {
        this._bindingMode = this._FlatRootBind;
        this._isHierachicalBind = false;
        this.set_serviceBaseUrl(this._orginalServiceBaseUrl);
        this.set_globalDataKeys([]);
    },

    _isRootTaxonSet: function () {
        return this.get_rootTaxonID() && this.get_rootTaxonID() !== Telerik.Sitefinity.getEmptyGuid();
    },

    _nodeDragStartHandler: function (sender, args) {
        jQuery("body").addClass("sfDraggingStart");
    },

    _nodeDroppingHandler: function (sender, args) {
        this._placeNodes(args.get_sourceNodes(), args.get_dropPosition(), args.get_destNode());
    },

    _moveNodes: function (sourceNodes, direction) {
        if (this._nodesPlacing(direction).get_cancel() !== true) {
            this._sourceNodes = sourceNodes;
            this._reorderSourceNodes(direction);
            var dataItemIdsForUpdate = this._constructDateItemIds(sourceNodes);

            var urlParams = this._getMovementUrlParams(direction);

            var serviceUrl = this._getBatchServiceUrl();
            serviceUrl.get_pathSections().push("move");

            this.get_manager().InvokePut(serviceUrl.toString(), urlParams, null, dataItemIdsForUpdate, this._moveSuccessDelegate, this._moveFailureDelegate, this);
        }
    },

    _placeNodes: function (sourceNodes, placePosition, destinationNode) {
        jQuery("body").removeClass("sfDraggingStart");
        jQuery('.sfDraggingAbove').removeClass('sfDraggingAbove');
        jQuery('.sfDraggingBelow').removeClass('sfDraggingBelow');

        if (destinationNode && sourceNodes.length > 0) {
            if (this._nodesPlacing(placePosition).get_cancel() !== true) {
                //TODO don't store those as fields but pass them as some kind of context of the call.
                this._destionationNode = destinationNode;
                this._sourceNodes = sourceNodes;
                this._placePosition = placePosition;

                switch (placePosition) {
                    case 'over':
                        this._startProcessingHandler();
                        this._changeParent(sourceNodes, destinationNode);
                        break;
                    case 'above':
                        this._startProcessingHandler();
                        this._placeBefore(sourceNodes, destinationNode);
                        break;
                    case 'below':
                        this._startProcessingHandler();
                        this._placeAfter(sourceNodes, destinationNode);
                        break;
                    default:
                        throw "Invalid place position: '" + placePosition + "'.";
                        break;
                }

                this._nodesPlacing(placePosition).set_cancel(true);
                return;
            }
        }
    },

    // Moves the page before the supplied target page.
    _placeBefore: function (sourceNodes, destinationNode) {
        this._reorderSourceNodes('before');
        var dataItemIdsForUpdate = this._constructDateItemIds(sourceNodes);

        var urlParams = this._getPlacementUrlParams(destinationNode, 'before');

        var serviceUrl = this._getBatchServiceUrl();
        serviceUrl.get_pathSections().push("place");

        this.get_manager().InvokePut(serviceUrl.toString(), urlParams, null, dataItemIdsForUpdate, this._placeBeforeSuccessDelegate, this._placeBeforeFailureDelegate, this);
    },

    _getPlacementUrlParams: function (destinationNode, placePosition) {
        var urlParams = this.get_urlParams();
        urlParams['destination'] = destinationNode.get_dataItem()[this._getItemKeyName()];
        urlParams['placePosition'] = placePosition;
        return urlParams;
    },

    _getMovementUrlParams: function (movingDirection) {
        var urlParams = this.get_urlParams();
        urlParams['direction'] = movingDirection
        return urlParams;
    },

    _constructDateItemIds: function (sourceNodes) {
        var sourceNodesLength = sourceNodes.length;
        var index = 0;
        var pageIdsForUpdate = [];
        while (index < sourceNodesLength) {
            pageIdsForUpdate.push(sourceNodes[index].get_dataItem()[this._getItemKeyName()]);
            index++;
        }
        return pageIdsForUpdate;
    },

    // Moves the page after the supplied target page.
    _placeAfter: function (sourceNodes, destinationNode) {
        this._reorderSourceNodes('after');
        var dataItemIdsForUpdate = this._constructDateItemIds(sourceNodes);

        this._getPlacementUrlParams(destinationNode, 'after');

        var urlParams = this._getPlacementUrlParams(destinationNode, 'after');

        var serviceUrl = this._getBatchServiceUrl();
        serviceUrl.get_pathSections().push("place");

        this.get_manager().InvokePut(serviceUrl.toString(), urlParams, null, dataItemIdsForUpdate, this._placeBeforeSuccessDelegate, this._placeBeforeFailureDelegate, this);
    },

    // Changes the page parent.
    _changeParent: function (sourceNodes, destinationNode) {
        this._reorderSourceNodes('over');
        var dataItemsToUpdate = this._constrcuctDataItemsForUpdate(sourceNodes, destinationNode, this._changeDataItemParent)

        var serviceUrl;
        if (this._changeParentServiceUrl)
            serviceUrl = this._changeParentServiceUrl;
        else
            serviceUrl = this._getBatchServiceUrl();

        this.get_manager().InvokePut(serviceUrl.toString(), null, null, dataItemsToUpdate, this._parentChangeSuccessDelegate, this._parentChangeFailureDelegate, this);
    },

    _constrcuctDataItemsForUpdate: function (sourceNodes, destinationNode, constructFunction) {
        var sourceNodesLength = sourceNodes.length;
        var dataItemsToUpdate = [];
        var destNodeDataItem = destinationNode.get_dataItem();
        while (sourceNodesLength--) {
            var sourceNode = sourceNodes[sourceNodesLength];
            var sourceNodeDataItem = sourceNode.get_dataItem();

            //Call to the passed as parameter function that should construct the data items for update.
            if (constructFunction) {
                if (typeof (constructFunction) !== 'function') {
                    throw "Invalid function argument 'constructFunction' was passed.";
                }
                constructFunction.apply(this, [sourceNodeDataItem, destNodeDataItem]);
            }

            dataItemsToUpdate.push(sourceNodeDataItem);
        }
        return dataItemsToUpdate;
    },

    _getBatchServiceUrl: function () {
        //Adding batch to service url path
        var serviceUrl = new Sys.Uri(this.get_serviceBaseUrl());
        //TODO make the "batch" sufix configurable
        serviceUrl.get_pathSections().push("batch");
        return serviceUrl;
    },

    _getNodesFromNodeValues: function (nodeValues) {
        var nodes = [];
        var nodeValuesLength = nodeValues.length;
        while (nodeValuesLength--) {
            nodes.push(this._getNodeFromNodeValue(nodeValues[nodeValuesLength]));
        }
        return nodes;
    },

    _getNodeFromNodeValue: function (nodeValue) {
        var node = this._treeView.findNodeByValue(nodeValue);
        if (node === null) {
            throw "No node with the value: " + nodeValue + "was found.";
        }
        return node;
    },

    _changeDataItemParent: function (sourceNodeDataItem, destNodeDataItem) {
        sourceNodeDataItem[this.get_parentDataKeyName()] = destNodeDataItem[this._getItemKeyName()];
    },

    // This will update the tree view nodes
    _reorderSourceNodes: function (placePosition) {
        var sourceNodesLength = this._sourceNodes.length;
        var index = 0;
        switch (placePosition) {
            case 'over':
                while (index < sourceNodesLength) {
                    var sourceNode = this._sourceNodes[index];
                    this._destionationNode.get_nodes().add(sourceNode);
                    index++;
                }
                break;
            case 'before':
                for (var i = 0; i < sourceNodesLength; ++i) {
                    //Removing the current source node from its parent before
                    //getting the index of the destination node, because the insert @ index
                    //will first remove the source node and then the index of the destination node could be changed.
                    var sourceNode = this._sourceNodes[i];
                    this._removeNodeFromItsParent(sourceNode);
                    var destinationParent = this._destionationNode.get_parent();
                    var destinationNodeIndex = this._getNodeIndex(this._destionationNode, destinationParent);
                    destinationParent.get_nodes().insert(destinationNodeIndex, sourceNode);
                }
                break;
            case 'after':
                while (index < sourceNodesLength) {
                    var sourceNode = this._sourceNodes[index];
                    //Removing the current source node from its parent before
                    //getting the index of the destination node, because the insert @ index
                    //will first remove the source node and then the index of the destination node could be changed.
                    this._removeNodeFromItsParent(sourceNode);
                    var destinationParent = this._destionationNode.get_parent();
                    var destinationNodeIndex = this._getNodeIndex(this._destionationNode, destinationParent);
                    destinationParent.get_nodes().insert(destinationNodeIndex + 1, sourceNode);

                    index++;
                }
                break;
            case 'up':
                while (index < sourceNodesLength) {
                    var sourceNode = this._sourceNodes[index];
                    var sourceNodeParent = sourceNode.get_parent();
                    var destinationNodeIndex = sourceNodeParent.get_nodes().indexOf(sourceNode);
                    if (destinationNodeIndex === 0) {
                        throw "Can't move first item up";
                    }
                    sourceNodeParent.get_nodes().insert(destinationNodeIndex - 1, sourceNode);

                    index++;
                }
                break;
            case 'down':
                while (sourceNodesLength--) {
                    var sourceNode = this._sourceNodes[sourceNodesLength];
                    var sourceNodeParent = sourceNode.get_parent();
                    var destinationNodeIndex = sourceNodeParent.get_nodes().indexOf(sourceNode);
                    if (destinationNodeIndex === sourceNodeParent.get_nodes().get_count() - 1) {
                        throw "Can't move last item down";
                    }
                    sourceNodeParent.get_nodes().insert(destinationNodeIndex + 1, sourceNode);
                }
                break;
            case 'top':
                while (index < sourceNodesLength) {
                    var sourceNode = this._sourceNodes[index];
                    var sourceNodeParent = sourceNode.get_parent();
                    var destinationNodeIndex = sourceNodeParent.get_nodes().indexOf(sourceNode);
                    if (destinationNodeIndex === 0) {
                        throw "Can't move first item to the top";
                    }
                    sourceNodeParent.get_nodes().insert(0, sourceNode);

                    index++;
                }
                break;
            case 'bottom':
                while (sourceNodesLength--) {
                    var sourceNode = this._sourceNodes[sourceNodesLength];
                    var sourceNodeParent = sourceNode.get_parent();
                    var destinationNodeIndex = sourceNodeParent.get_nodes().indexOf(sourceNode);
                    if (destinationNodeIndex === sourceNodeParent.get_nodes().get_count() - 1) {
                        throw "Can't move last item down";
                    }
                    sourceNodeParent.get_nodes().insert(sourceNodeParent.get_nodes().get_count() - 1, sourceNode);
                }
                break;
            default:
                throw 'Unknown placement position:' + placePosition;
                break;
        }
        this._raiseItemDomCreatedForAllChildNodes(this._sourceNodes);
    },

    _raiseItemDomCreatedForAllChildNodes: function (nodes) {
        var nodesLength = nodes.length;
        while (nodesLength--) {
            var node = nodes[nodesLength];
            this._raiseItemDomCreatedForNode(node, nodesLength);

            var childNodes = this._getAllChildNodes(node);
            var childNodesLength = childNodes.length;
            while (childNodesLength--) {
                this._raiseItemDomCreatedForNode(childNodes[childNodesLength], childNodesLength);
            }
        }
    },

    _getAllChildNodes: function (node) {
        var result = [];
        this._getChildNodesRecursive(node, result);
        return result;
    },

    _getChildNodesRecursive: function (node, nodesCollection) {
        var childNodes = node.get_nodes();
        var childNodesLength = childNodes.get_count();
        while (childNodesLength--) {
            var childNode = childNodes.getNode(childNodesLength);
            if (childNode.get_value() !== this._get_loadingNodeValue(node)) {
                this._getChildNodesRecursive(childNode, nodesCollection);
                nodesCollection.push(childNode);
            }
        }
    },

    _raiseItemDomCreatedForNode: function (node, nodesLength) {
        var dataItem = node.get_dataItem();
        //TODO: remove when support of compound keys is ready
        var compoundKey = {};
        var keyName = this._getItemKeyName();
        compoundKey[keyName] = dataItem[keyName];
        this._raiseItemDomCreated(compoundKey, dataItem, nodesLength, node.get_element());
    },

    _getNodeIndex: function (node, parentNode) {
        if (!parentNode) {
            parentNode = node.get_parent();
        }
        return parentNode.get_nodes().indexOf(node);
    },

    _removeNodeFromItsParent: function (sourceNode) {
        var sourceParent = sourceNode.get_parent();
        sourceParent.get_nodes().remove(sourceNode);
    },

    _parentChangeSuccess: function (sender, data) {
        this._endProcessingHandler();
        this._nodesPlaced();
        this.UpdateItems(data);
        this._clearReorderderingParams();
    },

    _parentChangeFailure: function (result) {
        this._endProcessingHandler();
        this._clearReorderderingParams();
        this.DataBind();
        alert(result.Detail);
    },

    _placeAfterSuccess: function (sender, data) {
        this._endProcessingHandler();
        this._nodesPlaced();
        this.UpdateItems(data);
        this._clearReorderderingParams();
    },

    _placeAfterFailure: function (result) {
        this._endProcessingHandler();
        this._clearReorderderingParams();
        this.DataBind();
        alert(result.Detail);
    },

    _placeBeforeSuccess: function (sender, data) {
        this._endProcessingHandler();
        this._nodesPlaced();
        this.UpdateItems(data);
        this._clearReorderderingParams();
    },

    _placeBeforeFailure: function (result) {
        this._endProcessingHandler();
        this._clearReorderderingParams();
        this.DataBind();
        alert(result.Detail);
    },

    _moveSuccess: function (sender, data) {
        this._clearReorderderingParams();
        this._endProcessingHandler();
        //TODO the nodes placed event should be raised here.
    },

    _moveFailure: function (result) {
        this._endProcessingHandler();
        this._clearReorderderingParams();
        this.DataBind();
        alert(result.Detail);
    },

    _clearReorderderingParams: function () {
        this._destionationNode = null;
        this._sourceNodes = [];
        this._placePosition = null;
        this.clearSelection();
    },

    _itemDomCreatedHandler: function (sender, eventArgs) {
        this.AssignCommands(eventArgs.get_itemElement(), eventArgs.get_dataItem(), eventArgs.get_key(), eventArgs.get_itemIndex());
        // enable actions menu for the element
        this.EnableActionMenus(eventArgs.get_itemElement());
    },

    _subscribeToDraggingEvents: function () {
        if (!this._nodeDroppingDelegate) {
            this._nodeDroppingDelegate = Function.createDelegate(this, this._nodeDroppingHandler);
        }
        this.add_treeViewNodeDropping(this._nodeDroppingDelegate);

        if (!this._nodeDragStartDelegate) {
            this._nodeDragStartDelegate = Function.createDelegate(this, this._nodeDragStartHandler);
        }
        this.add_treeViewNodeDragStart(this._nodeDragStartDelegate);

    },

    _removeDraggingEvents: function () {
        this._treeView.remove_nodeDropping(this._nodeDroppingDelegate);
        this._treeView.remove_nodeDragging(this._nodeDraggingDelegate);
        this._treeView.remove_nodeDragStart(this._nodeDragStartDelegate);
    },

    _canMoveNodes: function (nodes, direction) {
        var nodesLength = nodes.length;
        while (nodesLength--) {
            var nodeToValidate = nodes[nodesLength];
            var parent = nodeToValidate.get_parent();
            var nodeIndex = this._getNodeIndex(nodeToValidate, parent);
            switch (direction) {
                case "up":
                case "top":
                    if (nodeIndex === 0) {
                        return false;
                    }
                    break;
                case "down":
                case "bottom":
                    if (nodeIndex === parent.get_nodes().get_count() - 1) {
                        return false;
                    }
                    break;
                default:
                    throw 'Unknown move direction: ' + direction;
            }
        }
        return true;
    },

    _nodeCollapsedHandler: function (sender, eventArgs) {
        var node = eventArgs.get_node();
        this._removeCollapsedNodes(node);
    },

    // The ultimate hack for bug 76669
    _onDocumentMouseUpHandler: function (sender) {
        if (this._treeView._dragging) {
            var jBody = jQuery("body");
            if (jBody.hasClass("sfDraggingStart"))
                jBody.removeClass("sfDraggingStart");
        }
    },

    //Synchronising the list of nodes to expand in the cookie with the data bound nodes.
    _syncExpandedNodes: function () {
        if (this._bindingMode === this._HierarchicalTreeRootBind) {
            var expandedNodeValues = this._getExpandingNodesCookieData();
            if (expandedNodeValues) {
                var expandedNodeValuesLength = expandedNodeValues.length;
                while (expandedNodeValuesLength--) {
                    var cookieNode = this._treeView.findNodeByValue(expandedNodeValues[expandedNodeValuesLength]);
                    if (cookieNode && cookieNode.get_expanded()) {
                        this._expandedNodesDictionary[cookieNode.get_value()] = cookieNode;
                    }
                }
                this._setInitialExpandingCookie();
            }
        }
    },

    _removeCollapsedNodes: function (node) {
        this._removeChildNodes(node);

        delete this._expandedNodesDictionary[node.get_value()];
        this._setInitialExpandingCookie();
    },

    _removeChildNodes: function (node) {
        var childNodes = node.get_nodes();
        var childNodesLength = childNodes.get_count();
        while (childNodesLength--) {
            var childNode = childNodes.getNode(childNodesLength);
            this._removeChildNodes(childNode);
            var childNodeValue = childNode.get_value();
            if (childNodeValue in this._expandedNodesDictionary) {
                delete this._expandedNodesDictionary[childNodeValue];
            }
        }
    },

    _setInitialExpandingCookie: function () {
        var expandedNodesValues = [];
        for (var i in this._expandedNodesDictionary) {
            expandedNodesValues.push(i);
        }
        var cookieValue = Sys.Serialization.JavaScriptSerializer.serialize(expandedNodesValues)

        var expirationDate = new Date();
        expirationDate.setFullYear(expirationDate.getFullYear() + 1);

        jQuery.cookie(this.get_expandedNodesCookieName(), cookieValue, { expires: expirationDate });
    },

    _clearExpandingNodesCookie: function () {
        jQuery.cookie(this.get_expandedNodesCookieName(), null);
    },

    _getExpandingNodesCookie: function () {
        return jQuery.cookie(this.get_expandedNodesCookieName());
    },

    _getExpandingNodesCookieData: function () {
        var cookieData = null;
        var cookieValue = this._getExpandingNodesCookie();
        if (cookieValue && cookieValue !== "" && cookieValue !== "[]") {
            try {
                cookieData = Sys.Serialization.JavaScriptSerializer.deserialize(cookieValue);
            }
            catch (e) {
                cookieData = null;
                this._clearExpandingNodesCookie();
            }
        }
        return cookieData;
    },

    /* *************************** properties *************************** */

    get_serviceChildItemsBaseUrl: function () {
        return this._serviceChildItemsBaseUrl;
    },
    set_serviceChildItemsBaseUrl: function (value) {
        if (this._serviceChildItemsBaseUrl != value) {
            this._serviceChildItemsBaseUrl = value;
            this.raisePropertyChanged('serviceChildItemsBaseUrl');
        }
    },

    get_orginalServiceBaseUrl: function () {
        return this._orginalServiceBaseUrl;
    },
    set_orginalServiceBaseUrl: function (value) {
        if (this._orginalServiceBaseUrl != value) {
            this._orginalServiceBaseUrl = value;
            this.raisePropertyChanged('orginalServiceBaseUrl');
        }
    },

    get_servicePredecessorBaseUrl: function () {
        return this._servicePredecessorBaseUrl;
    },
    set_servicePredecessorBaseUrl: function (value) {
        if (this._servicePredecessorBaseUrl != value) {
            this._servicePredecessorBaseUrl = value;
            this.raisePropertyChanged("servicePredecessorBaseUrl");
        }
    },

    get_serviceTreeUrl: function () {
        return this._serviceTreeUrl;
    },
    set_serviceTreeUrl: function (value) {
        if (this._serviceTreeUrl != value) {
            this._serviceTreeUrl = value;
            this.raisePropertyChanged("serviceTreeUrl");
        }
    },

    get_changeParentServiceUrl: function () {
        return this._changeParentServiceUrl;
    },
    set_changeParentServiceUrl: function (value) {
        if (this._changeParentServiceUrl != value) {
            this._changeParentServiceUrl = value;
            this.raisePropertyChanged("_changeParentServiceUrl");
        }
    },

    get_parentDataKeyName: function () {
        return this._parentDataKeyName;
    },
    set_parentDataKeyName: function (value) {
        if (this._parentDataKeyName != value) {
            this._parentDataKeyName = value;
            this.raisePropertyChanged("parentDataKeyName");
        }
    },

    get_rootTaxonID: function () {
        return this._rootTaxonID;
    },
    set_rootTaxonID: function (value) {
        if (this._rootTaxonID != value) {
            this._rootTaxonID = value;
            this.raisePropertyChanged("rootTaxonID");
        }
    },

    get_selectedItemsCount: function () {
        var selectedItems = this.get_selectedItems();
        if (selectedItems == null) {
            return 0;
        }
        return selectedItems.length;
    },

    get_selectedItems: function () {
        if (this._allowMultipleSelection) {
            var checkedNodes = this._treeView.get_checkedNodes();
            var checkedNodesValues = [];
            for (var i = 0; i < checkedNodes.length; i++) {
                checkedNodesValues.push(checkedNodes[i].get_dataItem());
            }
            return checkedNodesValues;
        }
        else {
            var selectedNode = this._treeView.get_selectedNode();
            if (selectedNode != null) {
                return [selectedNode.get_dataItem()];
            }
            return null;
        }
    },

    get_enableDragAndDrop: function () {
        return this._enableDragAndDrop;
    },

    set_enableDragAndDrop: function (value) {
        if (this._enableDragAndDrop != value) {
            this._enableDragAndDrop = value;
            this.raisePropertyChanged('enableDragAndDrop');
        }
    },

    get_isTargetLibrary: function () {
        return this._isTargetLibrary;
    },

    set_isTargetLibrary: function (value) {
        this._isTargetLibrary = value;
    },

    //TODO Initial expanding functionality should be extracted as some sort of behavior plug-in.
    get_expandedNodesCookieName: function () {
        if (!this._expandedNodesCookieName) {
            this._expandedNodesCookieName = "expandedNodesCookie";
        }
        return this._expandedNodesCookieName;
    },
    set_expandedNodesCookieName: function (value) {
        if (this._expandedNodesCookieName != value) {
            this._expandedNodesCookieName = value;
            this.raisePropertyChanged('expandedNodesCookieName');
        }
    },
    get_enableInitialExpanding: function () {
        return this._enableInitialExpanding;
    },

    set_enableInitialExpanding: function (value) {
        if (this._enableInitialExpanding != value) {
            this._enableInitialExpanding = value;
            this.raisePropertyChanged('enableInitialExpanding');
        }
    }
}

Telerik.Sitefinity.Web.UI.RadTreeBinder.registerClass('Telerik.Sitefinity.Web.UI.RadTreeBinder', Telerik.Sitefinity.Web.UI.ClientBinder);

Telerik.Sitefinity.Web.UI.RadTreeNode = function (dataItem) {
    this._dataItem = dataItem;
    Telerik.Sitefinity.Web.UI.RadTreeNode.initializeBase(this);
}

// RadTreeNode wrapper.
Telerik.Sitefinity.Web.UI.RadTreeNode.prototype = {

    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.RadTreeNode.callBaseMethod(this, "_initialize");
    },

    // Gets the underlying data item.
    get_dataItem: function () {
        return this._dataItem;
    }
}

Telerik.Sitefinity.Web.UI.RadTreeNode.registerClass('Telerik.Sitefinity.Web.UI.RadTreeNode', Telerik.Web.UI.RadTreeNode);

/* A class describing a node placement action */
Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs = function (sourceNodes, placePosition, destinationNode) {
    Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs.initializeBase(this);
    this._sourceNodes = sourceNodes;
    this._placePosition = placePosition;
    this._destinationNode = destinationNode;
};
Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs.callBaseMethod(this, 'dispose');
    },

    get_sourceNodes: function () {
        return this._sourceNodes;
    },
    set_sourceNodes: function (value) {
        this._sourceNodes = value;
    },

    get_placePosition: function () {
        return this._placePosition;
    },
    set_placePosition: function (value) {
        this._placePosition = value;
    },

    get_destinationNode: function () {
        return this._destinationNode;
    },
    set_destinationNode: function (value) {
        this._destinationNode = value;
    }
};

Telerik.Sitefinity.Web.UI.RadTreeBinder.registerClass("Telerik.Sitefinity.Web.UI.RadTreeBinder", Telerik.Sitefinity.Web.UI.ClientBinder);

Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs.registerClass('Telerik.Sitefinity.Web.UI.NodesPlacingEventArgs', Sys.CancelEventArgs);