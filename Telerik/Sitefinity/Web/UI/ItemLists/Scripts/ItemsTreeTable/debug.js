/// <reference name="MicrosoftAjax.js" />

Type.registerNamespace("Telerik.Sitefinity.Web.UI.ItemLists");

// ------------------------------------------------------------------------
// Class ItemsTreeTable
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable = function (element) {
    /// <summary>Creates a new instance of ItemsTreeTable</summary>
    Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable.initializeBase(this, [element]);
    this._element = element;
    this._treeTable = null;
    this._emptyMessage = null;

    this._treeItemDomCreatedDelegate = null;
    this._treeItemNodeCheckedDelegate = null;
    this._treeItemNodeCheckingDelegate = null;
    this._handleBinderDataBoundDelegate = null;
    this._lastCheckedNode = null;
    this._elementsToDispose = [];
    this._callbacksToDispose = [];

}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable.callBaseMethod(this, 'initialize');
        this._treeItemDomCreatedDelegate = Function.createDelegate(this, this._treeItemDomCreatedHandler);
        this.getBinder().add_onItemDomCreated(this._treeItemDomCreatedDelegate);

        this._treeItemNodeCheckingDelegate = Function.createDelegate(this, this._treeItemNodeCheckingHandler);
        this._treeTable.add_nodeChecking(this._treeItemNodeCheckingDelegate);

        this._treeItemNodeCheckedDelegate = Function.createDelegate(this, this._treeItemNodeCheckedHandler);
        this._treeTable.add_nodeChecked(this._treeItemNodeCheckedDelegate);

        if (this.get_emptyMessage())
        {
            this._handleBinderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
            this.getBinder().add_onDataBound(this._handleBinderDataBoundDelegate);

            jQuery(this.get_emptyMessage()).hide();
        }       
    },
    dispose: function () {
        // clean-up

        if (this._treeItemDomCreatedDelegate) {
            if (this.getBinder()) {
                this.getBinder().remove_onItemDataBound(this._treeItemDomCreatedDelegate);
            }
            delete this._treeItemDomCreatedDelegate;
        }

        if (this._treeItemNodeCheckingDelegate) {
            if (this._treeTable) {
                this._treeTable.remove_nodeChecking(this._treeItemNodeCheckingDelegate);
            }
            delete this._treeItemNodeCheckingDelegate;
        }

        if (this._treeItemNodeCheckedDelegate) {
            if (this._treeTable) {
                this._treeTable.remove_nodeChecked(this._treeItemNodeCheckedDelegate);
            }
            delete this._treeItemNodeCheckedDelegate;
        }

        for (var i = 0, length = this._elementsToDispose.length; i < length; i++) {
            $clearHandlers(this._elementsToDispose[i]);
        }

        for (var i = 0, length = this._callbacksToDispose.length; i < length; i++) {
            delete this._callbacksToDispose[i];
        }

        Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable.callBaseMethod(this, 'dispose');
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */


    // Happens when selection of the pages is changing (node is checked or unchecked)
    add_selectionChanging: function (delegate) {
        this.get_events().addHandler("selectionChanging", delegate);
    },
    // Happens when selection of the pages is changing (node is checked or unchecked)
    remove_selectionChanging: function (delegate) {
        this.get_events().removeHandler("selectionChanging", delegate);
    },

    // raises the selectionChanging event
    _raiseSelectionChanging: function (node, tree, e, list) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs(node, tree, e, list);
        var handler = this.get_events().getHandler("selectionChanging");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable.callBaseMethod(this, "_handlePageLoad");
    },

    _treeItemNodeCheckedHandler: function (sender, args) {
        this._lastCheckedNode = args.get_node();
        this._lastCheckedNode.set_selected(this._lastCheckedNode.get_checked());
        this._raiseSelectionChanged(this.getBinder().get_selectedItems());
    },

    _treeItemNodeCheckingHandler: function (sender, args) {
        var args2 = this._raiseSelectionChanging(args.get_node(), this._treeTable, null, this);
        args.set_cancel(args2.get_cancel());
    },

    _binderDataBound: function (sender, args) {
        var totalItemCount = 0; 
        var dataItem = args && typeof (args.get_dataItem) == "function" ? args.get_dataItem() : null;
        if (dataItem && dataItem.Items && dataItem.Items.length > 0){
            totalItemCount = dataItem.Items.length;
        }

        var emptyMessage = jQuery(this.get_emptyMessage());
        if(totalItemCount > 0) {
            emptyMessage.hide();
        }
        else {
            emptyMessage.show();
        }
    },

    // Handles the clicks on the tree nodes
    _nodeClickedHandler: function (e, context) {

        var targetElement = e.target;
        var nodeName = targetElement.nodeName;
        var tree = context.self.get_treeTable();
        var node = context.node;
        var jTargetElement = jQuery(targetElement);

        // skip all links, checkboxes, buttons, +/-
        if (nodeName == "A"
            || nodeName == "INPUT"
            || jTargetElement.hasClass("rtPlus")
            || jTargetElement.hasClass("rtMinus")
            || jTargetElement.parent().hasClass('sfTaxonomiesLoadMore')
            || targetElement.parentNode.nodeName == "A"
            ) {
            return;
        }

        e.preventDefault();
        e.stopPropagation();

        var args = context.self._raiseSelectionChanging(node, tree, e, context.self);
        if (args.get_cancel())
            return;


        tree.trackChanges();
        if (e.ctrlKey) {
            var currentState = node.get_checked();
            node.set_checked(!currentState);
            node.set_selected(!currentState);
        }
        else if (e.shiftKey) {
            if (context.self._lastCheckedNode == null || context.self._lastCheckedNode == node) {
                node.set_checked(!node.get_checked);
            }
            else {
                var allNodes = tree.get_allNodes();
                var lastNodeIndex = 0;
                var currentNodeIndex = 0;
                for (var i = 0, length = allNodes.length; i < length; i++) {
                    var n = allNodes[i];
                    if (n == node) {
                        currentNodeIndex = i;
                    }
                    else if (n == context.self._lastCheckedNode) {
                        lastNodeIndex = i;
                    }
                }

                var from = 0; var to = 0;

                if (lastNodeIndex >= currentNodeIndex) {
                    from = currentNodeIndex;
                    to = lastNodeIndex;
                }
                else {
                    from = lastNodeIndex;
                    to = currentNodeIndex;
                }

                for (var i = from; i <= to; i++) {
                    allNodes[i].set_checked(true);
                }
            }
        }
        else {
            var checked = node.get_checked();
            //var checkedNodes = tree.get_checkedNodes();
            //if (checkedNodes.length > 1) {
            //    checked = true;
            //}
            //else {
            checked = !checked;
            //}

            var treeTable = context.self;
            if (!treeTable.get_treeTable().get_multipleSelect()) {
                treeTable._uncheckAllNodes();
            }

            node.set_checked(checked);
            node.set_selected(checked);
        }

        //tree.unselectAllNodes();
        var allNodes = tree.get_checkedNodes();
        var count = allNodes.length;
        while (count--) {
            var n = allNodes[count];
            n.set_selected(true);
        }

        tree.commitChanges();

        context.self._lastCheckedNode = node;

        context.self._raiseSelectionChanged(context.self.getBinder().get_selectedItems());
    },

    _selectNode: function (node) {
        jQuery(node.get_element()).addClass("sfSel");
    },

    _treeItemDomCreatedHandler: function (sender, args) {
        var dataItem = args.get_dataItem();
        var index = args.get_itemIndex();
        var key;
        var keyNames = sender.get_dataKeyNames();
        if (keyNames.length == 1) {
            key = args.get_key()[keyNames[0]];
        }
        else {
            throw "Support of compund keys not implemented for tree binder";
        }

        var itemElement = args.get_itemElement();
        var node = this.get_treeTable().findNodeByValue(key);
        node.get_attributes().setAttribute("Index", index);
        var context = {
            self: this,
            node: node
        };

        var nodeElementClickCallback = Function.createCallback(this._nodeClickedHandler, context);

        $addHandler(itemElement, "click", nodeElementClickCallback);
        this._elementsToDispose.push(itemElement);
        this._callbacksToDispose.push(nodeElementClickCallback);
    },


    /* -------------------- private methods ----------- */

    _uncheckAllNodes: function () {
        var checkedNodes = this._treeTable.get_checkedNodes();
        for (var i = 0, length = checkedNodes.length; i < length; i++) {
            checkedNodes[i].set_checked(false);
        }
    },

    /* -------------------- properties ---------------- */

    // sets the the RadTreeView
    get_treeTable: function () { return this._treeTable; },
    // gets the the RadTreeView
    set_treeTable: function (value) { this._treeTable = value; },

    // sets the the label
    get_emptyMessage: function () { return this._emptyMessage; },
    // gets the the label
    set_emptyMessage: function (value) { this._emptyMessage = value; }

}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable', Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase);


// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs = function (node, tree, event, list) {
    Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs.initializeBase(this);
    this._node = node;
    this._tree = tree;
    this._event = event;
    this._list = list;
}

Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs.callBaseMethod(this, 'dispose');
    },
    get_node: function () {
        return this._node;
    },
    get_tree: function () {
        return this._tree;
    },
    get_event: function () {
        return this._event;
    },
    get_list: function () {
        return this._list;
    }
};
Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.NodeEventArgs', Sys.CancelEventArgs);
