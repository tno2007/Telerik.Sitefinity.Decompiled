﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");
/// js Component for the PageSelector Control =>used for selecting a page from the sitemap tree.
/// the implemnetation uses treeview and preloads all pages
/// </summary>

Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector = function(element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector.initializeBase(this, [element]);
    this._siteMapTree = null;
    this._doneButton = null;
    this._checkboxes = null;
    this._cancelButton = null;
    this._doneSelectingDelegate = null;
    this._cancelDelegate = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector.callBaseMethod(this, 'initialize');

        this._doneSelectingDelegate = Function.createDelegate(this, this._onDoneSelecting);
        $addHandler(this.get_doneButton(), "click", this._doneSelectingDelegate);

        this._cancelDelegate = Function.createDelegate(this, this._onCancel);
        $addHandler(this.get_cancelButton(), "click", this._cancelDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector.callBaseMethod(this, 'dispose');

        if (this._doneSelectingDelegate) {
            delete this._doneSelectingDelegate;
        }
        if (this._cancelDelegate) {
            delete this._cancelDelegate;
        }
    },

    /* Event handlers  */
    _onDoneSelecting: function () {
        var h = this.get_events().getHandler('doneClientSelection');
        if (h) h(this.get_selectedPage());
    },
    _onCancel: function () {
        var checkedNodes = this._siteMapTree.get_checkedNodes();
        for (var nodeIndex = 0; nodeIndex < checkedNodes.length; nodeIndex++) {
            checkedNodes[nodeIndex].set_checked(false);
        }
        this._siteMapTree.unselectAllNodes();
        this._onDoneSelecting();
    },

    /* events binding and unbinding */
    add_doneClientSelection: function (delegate) {
        this.get_events().addHandler('doneClientSelection', delegate);
    },
    remove_doneClientSelection: function (delegate) {
        this.get_events().removeHandler('doneClientSelection', delegate);
    },
    ///returns the sitemap tree component used to select the page
    get_siteMapTree: function () {
        return this._siteMapTree;
    },
    ///sets the sitemap tree component used to select the page
    set_siteMapTree: function (value) {
        this._siteMapTree = value;
    },

    set_selectedPageId: function (value) {
        var node = this._siteMapTree.findNodeByValue(value);
        if (node != null)
        { node.select(); }
    },
    get_selectedPageId: function () {
        var page = this.get_selectedPage()
        if (page != null) return page.Id;
        return null;
    },

    get_selectedPage: function () {
        var checkedNodes = this._siteMapTree.get_checkedNodes();
        if (checkedNodes != null && checkedNodes.length > 0) {
            return checkedNodes;
        }
        var node = this._siteMapTree.get_selectedNode();
        if (node != null) {
            var page = new Object;
            page.Id = node.get_value();
            page.Title = node.get_text();
            return page;
        }
        return null;
    },
    get_checkboxes: function () {
        return this._checkboxes;
    },
    set_checkboxes: function (value) {

        this._checkboxes = value;
    },
    get_doneButton: function () {

        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },
    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {

        this._cancelButton = value;
    },
    get_nodesByIds: function (pageIds) {

        var result = new Array();
        if (pageIds == null) return result;
        var pageIds = pageIds.split(";");
        var nodes = this.get_siteMapTree().get_nodes();
        for (var nodeIndex = 0; nodeIndex < nodes.get_count(); nodeIndex++) {
            this._get_childNodesByIds(pageIds, result, nodes.getNode(nodeIndex));
        }
        return result;
    },
    _get_childNodesByIds: function (pageIds, result, node) {
        for (var pageIndex = 0; pageIndex < pageIds.length; pageIndex++) {
            if (node.get_value() == pageIds[pageIndex]) {
                result.push(node);
                break;
            }
        }
        var nodes = node.get_nodes();
        for (var nodeIndex = 0; nodeIndex < nodes.get_count(); nodeIndex++) {
            this._get_childNodesByIds(pageIds, result, nodes.getNode(nodeIndex));
        }
    },

    set_checkedNodes: function (pageIds) {
        var checkedNodes = this.get_nodesByIds(pageIds);
        for (var nodeIndex = 0; nodeIndex < checkedNodes.length; nodeIndex++) {
            checkedNodes[nodeIndex].set_checked(true);
        }
        //        if (pageIds != null) {
        //            var pageIds = pageIds.split(";");
        //            var nodes = this.get_siteMapTree().get_nodes();
        //            for (var nodeIndex = 0; nodeIndex < nodes.get_count(); nodeIndex++) {
        //                this._set_childNodesCheckState(nodes.getNode(nodeIndex), pageIds);
        //            }
        //        }
    },
    //    _set_childNodesCheckState: function(node, pageIds) {
    //        node.set_checked(false);
    //        for (var pageIndex = 0; pageIndex < pageIds.length; pageIndex++) {
    //            if (node.get_value() == pageIds[pageIndex]) {
    //                node.set_checked(true);
    //                break;
    //            }
    //        }
    //        var nodes = node.get_nodes();
    //        for (var nodeIndex = 0; nodeIndex < nodes.get_count(); nodeIndex++) {
    //            this._set_childNodesCheckState(nodes.getNode(nodeIndex), pageIds);
    //        }

    //    },
    set_selecotor_keys: function (keys) {
        if (this.get_checkboxes() == true)
            set_checkedNodes(keys);
        else set_selectedPage(keys);
    },
    get_selectedItems: function () {
        get_selectedPage();
    },
    set_selectedPage: function (pageId) {
        var nodes = this.get_siteMapTree().get_nodes();
        for (var nodeIndex = 0; nodeIndex < nodes.get_count(); nodeIndex++) {
            this._set_selectedPage(nodes.getNode(nodeIndex), pageId);
        }
    },
    _set_selectedPage: function (node, pageId) {
        node.set_selected(false);
        if (node.get_value() == pageId) {
            node.set_selected(true);
            return true;
        }
        var nodes = node.get_nodes();
        for (var nodeIndex = 0; nodeIndex < nodes.get_count(); nodeIndex++) {
            if (this._set_selectedPage(nodes.getNode(nodeIndex), pageId)) {
                return true;
            }
        }
    }


}

Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.PageSelector', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
