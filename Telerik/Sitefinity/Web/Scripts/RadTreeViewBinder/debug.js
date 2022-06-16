Telerik.Sitefinity.Web.UI.RadTreeNode = function(dataItem) {
    this._nested = false;
    this._dataItem = dataItem;
    this._isLoading = false;
    this._parentText;
    this._populated = false;
    this._expanding = false;
    this._elements = [];
    this._index = 0;
    Telerik.Sitefinity.Web.UI.RadTreeNode.initializeBase(this);
}

// RadTreeNode wrapper.
Telerik.Sitefinity.Web.UI.RadTreeNode.prototype = {

    // set up and tear down
    initialize: function() {
        Telerik.Sitefinity.Web.UI.RadTreeNode.callBaseMethod(this, "_initialize");
    },

    // Gets the underlying data item.
    get_dataItem: function() {
        return this._dataItem;
    },

    set_hasChildren: function(value) {
        this._nested = value;
    },

    get_hasChildren: function() {
        return this._nested;
    },

    set_parentText: function(value) {
        this._parentText = value;
    },

    get_parentText: function() {
        return this._parentText;
    },

    set_isPopulated: function(value) {
        this._populated = value;
    },

    get_isPopulated: function() {
        return this._populated;
    },

    set_isLoading: function(value) {
        this._isLoading = value;
    },

    get_isLoading: function() {
        return this._isLoading;
    },

    set_expanding: function(value) {
        this._expanding = value;
    },

    get_expanding: function() {
        return this._expanding;
    },


    // appends a new child.
    add: function(childNode) {
        var nodes = this.get_nodes();

        if (nodes.get_count) {
            childNode.set_currentIndex(nodes.get_count());
        }
        else {
            childNode.set_currentIndex(0);
        }

        nodes.add(childNode);
    },
    // removes the child node and validates the index.
    remove: function(childNode) {

        this.get_nodes().remove(childNode);

        var nodes = this.get_nodes();

        for (i = 0; i < nodes.get_count(); i++) {
            nodes.getNode(i).set_currentIndex(i);
        }
    },

    get_elements: function() {
        return this._elements;
    },

    // Returns the current index for the node.
    get_currentIndex: function() {
        return this._index;
    },

    set_currentIndex: function(value) {
        this._index = value;
    }
}

Telerik.Sitefinity.Web.UI.RadTreeNode.registerClass('Telerik.Sitefinity.Web.UI.RadTreeNode', Telerik.Web.UI.RadTreeNode);

Telerik.Sitefinity.Web.UI.RadTreeViewBinder = function() {
    this._isLoading = false;

    Telerik.Sitefinity.Web.UI.RadTreeViewBinder.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.RadTreeViewBinder.prototype = {

    // set up and tear down
    initialize: function() {
        Telerik.Sitefinity.Web.UI.RadTreeViewBinder.callBaseMethod(this, "initialize");
        this._serviceCallback = this.BindCollection;
    },
    dispose: function() {
        Telerik.Sitefinity.Web.UI.RadTreeViewBinder.callBaseMethod(this, "dispose");
    },

    InternalDataBind: function() {
        Telerik.Sitefinity.Web.UI.RadTreeViewBinder.callBaseMethod(this, "InternalDataBind");
        if (this.get_allChildBindersBound()) {
            this.ClearTarget();
            var clientManager = this.get_manager();
            if (this._dataKeys) {
                clientManager.GetItemCollection(this);
            }
        }
    },

    // onDataBound event is fired right after the client side ClientBinder has received data from the WCF service
    // and bound data to the user interface
    add_onClientNodeExpanding: function(delegate) {
        this.get_events().addHandler('onClientNodeExpanding', delegate);
    },
    remove_onClientNodeExpanding: function(delegate) {
        this.get_events().removeHandler('onClientNodeExpanding', delegate);
    },

    _clientNodeExpandingHandler: function(sender, args) {
        var h = this.get_events().getHandler('onClientNodeExpanding');
        if (h) h(sender, args);
    },

    // onDataBound event is fired right after the client side ClientBinder has received data from the WCF service
    // and bound data to the user interface
    add_onClientNodeItemBound: function(delegate) {
        this.get_events().addHandler('onClientNodeItemBound', delegate);
    },
    remove_onClientNodeItemBound: function(delegate) {
        this.get_events().removeHandler('onClientNodeItemBound', delegate);
    },

    _clientNodeBoundHandler: function(sender, args) {
        var h = this.get_events().getHandler('onClientNodeItemBound');
        if (h) h(sender, args);
    },

    add_onClientNodeClicked: function(delegate) {
        this.get_events().addHandler('onClientNodeClicked', delegate);
    },
    remove_onClientNodeClicked: function(delegate) {
        this.get_events().removeHandler('onClientNodeClicked', delegate);
    },

    _clientNodeClickedHandler: function(sender, args) {
        var h = this.get_events().getHandler('onClientNodeClicked');
        if (h) h(sender, args);
    },

    add_onClientNodeBoundComplete: function(delegate) {
        this.get_events().addHandler('onClientNodeBoundComplete', delegate);
    },
    remove_onClientNodeBoundComplete: function(delegate) {
        this.get_events().removeHandler('onClientNodeBoundComplete', delegate);
    },

    _nodeBoundCompleteHandler: function(sender, args) {
        var h = this.get_events().getHandler('onClientNodeBoundComplete');
        if (h) h(sender, args);
    },

    add_onClientNodeItemBoundComplete: function(delegate) {
        this.get_events().addHandler('onClientNodeItemBoundComplete', delegate);
    },
    remove_onClientNodeItemBoundComplete: function(delegate) {
        this.get_events().removeHandler('onClientNodeItemBoundComplete', delegate);
    },

    _nodeItemBoundCompleteHandler: function(sender, args) {
        var h = this.get_events().getHandler('onClientNodeItemBoundComplete');
        if (h) h(sender, args);
    },

    add_onBeforeLoading: function(delegate) {
        this.get_events().addHandler('onBeforeLoading', delegate);
    },
    remove_onBeforeLoading: function(delegate) {
        this.get_events().removeHandler('onBeforeLoading', delegate);
    },

    _beforeLoadingHandler: function(sender, args) {
        var h = this.get_events().getHandler('onBeforeLoading');
        if (h) h(sender, args);
    },

    // Overridden method from the ClientBinder. This method binds data to the target control.
    DataBind: function(node) {
        Telerik.Sitefinity.Web.UI.RadTreeViewBinder.callBaseMethod(this, "DataBind");

        if (node == null) {
            node = this.GetCurrentNode();
        }

        if (this._isLoading || (node.get_isLoading && node.get_isLoading())) {
            return;
        }
        this._isLoading = true;
        if (node.set_isLoading) {
            node.set_isLoading(true);
        }

        var treeView = $find(this._targetId);
        treeView.set_enabled(false);

        node.get_nodes().clear();
        if (node.showLoadingStatus) {
            node.showLoadingStatus("Loading...", Telerik.Web.UI.TreeViewLoadingStatusPosition.BelowNodeText);
        }
        this._beforeLoadingHandler(this, node);

        // raise the client logic for poupulating the selected node.

        // get all items.
        var clientManager = this.get_manager();
        clientManager.GetItemCollection(this);
        treeView.set_enabled(true);
        this._isLoading = false;
    },

    // This method binds the form, with the specified filter expression. It is a shorthand to setting the 
    // filterExpression property and calling the bind.
    Filter: function(filterExpression) {
        this._filterExpression = filterExpression;
        this.DataBind();
    },

    ClearNodes: function() {
        var treeView = $find(this._targetId);
        treeView.get_nodes().clear();
    },

    // Overridden method from the ClientBinder. The ClientManager will call this method after it receives data
    // (collection of items) from the service and pass the CollectionContext (defined on the server) to it.
    BindCollection: function(data) {
        data = this.DeserializeData(data);
        this._dataBindingHandler(data);
        
        // write the treeview binding logic here.
        var treeView = $find(this._targetId);

        var bindingNode = this.GetCurrentNode();
        if (data.Context != null && data.Context["nodekey"]) {
            bindingNode = treeView.findNodeByValue(data.Context["nodekey"]);
        }

        var template = new Sys.UI.Template($get(this._clientTemplates[0]));
        var container = $("<div/>")[0];

        if (bindingNode.hideLoadingStatus) {
            bindingNode.hideLoadingStatus();
        }
        bindingNode.get_nodes().clear();

        if (data.TotalCount && data.TotalCount > 0) {
            for (i = 0; i < data.TotalCount; i++) {
                this.Populate(treeView, bindingNode, container, template, data.Items[i]);
            }
            if (bindingNode.set_expanding) {
                if (bindingNode.get_expanding()) {
                    bindingNode.set_expanding(false);
                }
            }
        }
        else {
            if (bindingNode.set_hasChildren) {
                bindingNode.set_expanding(false)
                bindingNode.set_expanded(false);
                bindingNode.set_hasChildren(false);
            }
        }
        if (bindingNode.set_isPopulated) {
            bindingNode.set_isPopulated(true);
            if (bindingNode.get_selected())
                bindingNode.set_expanded(true);
        }
        treeView.set_enabled(true);

        this._nodeBoundCompleteHandler(treeView, bindingNode);
        if (bindingNode.set_isLoading)
            bindingNode.set_isLoading(false);

        this._dataBoundHandler();
    },

    Populate: function(treeView, current, container, template, dataItem) {
        var node = new Telerik.Sitefinity.Web.UI.RadTreeNode(dataItem);

        if (dataItem.IsGeneric) {
            node.set_text(dataItem);
        }
        else {

            container.innerHTML = '';

            template.instantiateIn(container, null, dataItem);

            $($(container).find("div").children()).each(function(i, element) {
                if (i == 0) {
                    node.set_text($(container).find(element.tagName).html());
                }
                else {
                    // add any additional nodes to the elements collection.
                    node.get_elements().push(element);
                }
            });

            // do it on your own.
            this._clientNodeBoundHandler(treeView, node);
        }

        // if the node is expading remove any temporary nodes.
        //        if (current.set_expanding) {
        //            if (current.get_expanding()) {
        //                current.get_nodes().clear();
        //                current.set_expanding(false);
        //            }
        //        }

        // set by client
        if (node.get_hasChildren()) {
            node.add(new Telerik.Sitefinity.Web.UI.RadTreeNode());
        }

        if (current.add) {
            current.add(node);
        }
        else {
            current.get_nodes().add(node);
        }

        // fire after population of each node.
        this._nodeItemBoundCompleteHandler(treeView, node);

    },

    NodeExpanding: function(sender, args) {
        var node = args.get_node();

        node.set_selected(true);

        if (!node.get_isPopulated() && node.get_hasChildren()) {
            this.DataBind(node);
        }
        this._clientNodeExpandingHandler($find(this._targetId), args);
        if (!node.get_expanded()) {
            node.set_expanded(true);
        }
    },

    NodeClicked: function(sender, args) {
        this._clientNodeClickedHandler($find(this._targetId), args);
        this.NodeExpanding(sender, args);
    },

    // Overridden method from the ClientBinder. The ClientManager will call this method afer it receives data
    // (single item) from the service and pass the deserialized item to it.
    BindItem: function(data) {
        alert('BindItem function is not supported by RadTreeViewBinder. Please use DataBind instead.');
    },

    // Gets the current selected node, return tree itself for root.
    GetCurrentNode: function() {
        var treeView = $find(this._targetId);
        var selectedNode = treeView.get_selectedNode();
        var current = treeView;

        if (selectedNode) {
            current = selectedNode;
        }
        return current;
    }
}

Telerik.Sitefinity.Web.UI.RadTreeViewBinder.registerClass('Telerik.Sitefinity.Web.UI.RadTreeViewBinder', Telerik.Sitefinity.Web.UI.ClientBinder);

