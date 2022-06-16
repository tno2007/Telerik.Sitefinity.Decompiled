Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField = function (element) {
    this._element = element;

    this._webServiceUrl = null;
    this._rootNodeId = null;
    this._rootNode = null;
    this._provider = null;
    this._initialNodesCount = 0;
    this._selectedNode = null;
    this._selectedNodeOriginal = null;

    this._changeSelectedNodeButton = null;
    this._selectedNodePanel = null;
    this._selectedNodeLabel = null;
    this._nodeSelector = null;
    this._dataItem = null;

    this._loadDelegate = null;
    this._selectionDoneDelegate = null;
    this._changeSelectedNodeClickDelegate = null;
    this._nodesRootChoiceChangedDelegate = null
    this._selectorDataBoundDelegate = null
    this._selectorItemDataBoundDelegate = null
    this._clientManager = null;
    this._selectedNodeDataFieldName = null;

    Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField.prototype =
{
    initialize: function () {

        this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        if (this._loadDelegate == null) {
            this._loadDelegate = Function.createDelegate(this, this._load)
        }
        Sys.Application.add_load(this._loadDelegate);

        if (this._changeSelectedNodeClickDelegate == null) {
            this._changeSelectedNodeClickDelegate = Function.createDelegate(this, this._changeSelectedNodeClick);
        }
        if (this._changeSelectedNodeButton) {
            $addHandler(this._changeSelectedNodeButton, 'click', this._changeSelectedNodeClickDelegate);
        }

        if (this._selectionDoneDelegate == null) {
            this._selectionDoneDelegate = Function.createDelegate(this, this._handleSelectionDone);
        }
        if (this._nodesRootChoiceChangedDelegate == null) {
            this._nodesRootChoiceChangedDelegate = Function.createDelegate(this, this._handleNodesRootChoiceChanged);
        }
        if (this._nodeSelector) {
            this._nodeSelector.add_selectionDone(this._selectionDoneDelegate);
            this._nodeSelector.add_nodesRootChoiceChanged(this._nodesRootChoiceChangedDelegate);
        }

        Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        this._webServiceUrl = null;
        this._rootNodeId = null;
        this._provider = null;

        if (this._loadDelegate != null) {
            delete this._loadDelegate;
        }

        if (this._changeSelectedNodeClickDelegate != null) {
            delete this._changeSelectedNodeClickDelegate;
        }

        if (this._selectionDoneDelegate != null) {
            this._nodeSelector.remove_selectionDone(this._selectionDoneDelegate);
            delete this._selectionDoneDelegate;
        }

        if (this._nodesRootChoiceChangedDelegate != null) {
            this._nodeSelector.remove_nodesRootChoiceChanged(this._nodesRootChoiceChangedDelegate);
            delete this._nodesRootChoiceChangedDelegate;
        }

        if (this._selectorDataBoundDelegate) {
            delete this._selectorDataBoundDelegate;
        }

        if (this._selectorItemDataBoundDelegate) {
            delete this._selectorItemDataBoundHandler;
        }
        Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField.callBaseMethod(this, "dispose");
    },

    /* -------------------- events -------------------- */

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

    // Gets the url of the webservice that is used to manage taxonomies asynchronously.
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },

    // Sets the url of the webservice that is used to manage taxonomies asynchronously.
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },

    // Gets the id of the taxonomy associated with this taxon field.
    get_rootNodeId: function () {
        return this._rootNodeId;
    },

    // Sets the id of the taxonomy associated with this taxon field.     
    set_rootNodeId: function (value) {
        this._rootNodeId = value;
    },
    // Gets the taxonomy associated with this taxon field.
    get_rootNode: function () {
        return this._rootNode;
    },

    // Sets the taxonomy associated with this taxon field.     
    set_rootNode: function (value) {
        this._rootNode = value;
    },

    // Gets the name of the taxonomy provider associated with this taxon field.
    get_provider: function () {
        return this._provider;
    },

    set_provider: function (value) {
        this._provider = value;
    },

    get_selectedNode: function () {
        return this._selectedNode;
    },

    // Gets the value of the taxon field.
    get_value: function () {
        if (this._selectedNode != null) {
            return this._selectedNode;
        }
        return Sys.Serialization.JavaScriptSerializer.deserialize(this._rootNode);
    },

    // Sets the value of the taxon field.
    set_value: function (value) {

    },

    _setValueInternal: function (value) {
        this._selectedNode = value;
        if (this._dataItem) {
            this._dataItem.Parent = this._selectedNode;
            if (this._selectedNode) {
                this._dataItem.ParentId = this._selectedNode.Id;
            }
            else {
                this._dataItem.ParentId = '00000000-0000-0000-0000-000000000000';
            }
        }
        if (this._selectedNode && this._selectedNode.Id != this._rootNodeId) {
            if (this._selectedNode.hasOwnProperty(this._selectedNodeDataFieldName)) {
                $(this._selectedNodeLabel).text(this._selectedNode[this._selectedNodeDataFieldName]);
            }
            else {
                $(this._selectedNodeLabel).text(this._selectedNode.Title);
            }
            $(this._selectedNodePanel).show();
            this._nodeSelector.get_nodesRadio().checked = true;
        }
        else {
            $(this._selectedNodeLabel).text("");
        }

        this._valueChangedHandler();
    },

    /* --------------------  public methods ----------- */

    //IRequiresDataItem inteface implementation
    set_dataItem: function (value) {
        this._hasChanged = false;
        this._dataItem = value;
        this._selectedNodeOriginal = value.Parent;
        this._setValueInternal(this._selectedNodeOriginal);
        this._doExpandHandler();
    },

    //TODO: this code needs review
    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField.callBaseMethod(this, "reset");
        this.clearSelection();
    },

    clearSelection: function () {
        this.selectedNode = null;
        $(this._selectedNodePanel).hide();
        this._nodeSelector.hideTreePanel();
        this._nodeSelector.get_rootRadio().checked = true;
    },

    focus: function () {
    },

    blur: function () {
        var behavior = this._get_ExpandableExtenderBehavior();
        var value = this.get_value();
        if (value) {
            this._nodeSelector.showTreePanel();
            $(this._selectedNodePanel).hide();
            behavior.reset();
        }
        else {
            this._nodeSelector.hideTreePanel();
            $(this._selectedNodePanel).show();
        }
    },

    // Returns true if the value of the field is changed
    isChanged: function () {
        if (this._hasChanged == true) {
            if (!this.get_value()) return false;
            if (!this._selectedNodeOriginal) return true;
            if (this.get_value().Id == this._selectedNodeOriginal.Id) {
                return false;
            }
            else {
                return true;
            }
        } else {
            return false;
        }

    },
    /* -------------------- events --------------------- */

    /* -------------------- event handlers ------------- */

    // Handles the application load event.
    _load: function () {
        this._selectorDataBoundDelegate = Function.createDelegate(this, this._selectorDataBoundHandler);
        this._selectorItemDataBoundDelegate = Function.createDelegate(this, this._selectorItemDataBoundHandler);
        this.get_nodeSelector().get_treeBinder().add_onDataBound(this._selectorDataBoundDelegate);
        this.get_nodeSelector().get_treeBinder().add_onItemDataBound(this._selectorItemDataBoundDelegate);
    },

    // Handles the click event of the change selected taxa button.
    _changeSelectedNodeClick: function () {
        this._nodeSelector.set_showBinder(true);
        var context = this._dataItem.Parent;
        if (context.ParentId == "00000000-0000-0000-0000-000000000000") {
            context = null;
        }
        this._nodeSelector.dataBind(null, context);
        $(this._selectedNodePanel).hide();
    },

    // Handles the selection done event of the taxa selector.
    _handleSelectionDone: function (sender, args) {
        this._hasChanged = true;
        this._setValueInternal(args.selectedNode);

        this._nodeSelector.hideTreePanel();
        $(this._selectedNodePanel).show();
    },

    _selectorDataBoundHandler: function (sender, args) {
        if (this._dataItem && this._dataItem.Parent && this._dataItem.Parent.Id) {
            var currentlySelectedNode = this._getSelectorNode(sender, this._dataItem.Parent.Id);
            if (currentlySelectedNode) {
                currentlySelectedNode.select();
            }
        }
    },

    _selectorItemDataBoundHandler: function (sender, args) {
        var dataItem = args.get_dataItem();
        if (this._dataItem && dataItem.Id === this._dataItem.Id && !this._dataItem.IsDuplicate) {
            var nodeToDisable = this._getSelectorNode(sender, dataItem.Id);
            if (nodeToDisable)
                nodeToDisable.set_enabled(false);
        }
    },

    _getSelectorNode: function (binder, value) {
        var radTreeView = $find(binder.get_targetId());
        return radTreeView.findNodeByValue(value);
    },

    // Handles the radio clicked event of the taxa selector.
    _handleNodesRootChoiceChanged: function (sender, args) {
        this._hasChanged = true;

        if (args && args.commandName == 'create') {
            if (args.selectedNode) {
                this._setValueInternal(args.selectedNode);
            }
            else {
                this._setValueInternal(Sys.Serialization.JavaScriptSerializer.deserialize(this._rootNode));
            }
            $(this._selectedNodePanel).hide();
        }
    },

    /* -------------------- private methods ----------- */

    _get_ExpandableExtenderBehavior: function () {
        if (this._expandableExtenderBehavior) {
            return this._expandableExtenderBehavior;
        }
        this._expandableExtenderBehavior = Sys.UI.Behavior.getBehaviorByName(this._element, "ExpandableExtender");
        return this._expandableExtenderBehavior;
    },

    /* -------------------- properties ---------------- */

    // Gets the reference to the button/element which is used to change the selected taxa.
    get_changeSelectedNodeButton: function () {
        return this._changeSelectedNodeButton;
    },

    // Sets the reference to the button/element which is used to change the selected taxa.
    set_changeSelectedNodeButton: function (value) {
        this._changeSelectedNodeButton = value;
    },

    // Gets the reference to the element (container) which is displayed when user has selected a taxa.
    get_selectedNodePanel: function () {
        return this._selectedNodePanel;
    },
    // Sets the reference to the element (container) which is displayed when user has selected a taxa.
    set_selectedNodePanel: function (value) {
        this._selectedNodePanel = value;
    },

    // Gets the reference to the element (container) which is displayed when user has selected a taxa.
    get_selectedNodeLabel: function () {
        return this._selectedNodeLabel;
    },
    // Sets the reference to the element (container) which is displayed when user has selected a taxa.
    set_selectedNodeLabel: function (value) {
        this._selectedNodeLabel = value;
    },

    // Gets the reference to hierarchical selector component used to select hierarchical taxa.
    get_nodeSelector: function () {
        return this._nodeSelector;
    },

    // Sets the reference to hierarchical selector component used to select hierarchical taxa.
    set_nodeSelector: function (value) {
        this._nodeSelector = value;
    },

    get_selectedNodeDataFieldName: function() {
        return this._selectedNodeDataFieldName;
    },
    set_selectedNodeDataFieldName: function(value) {
        this._selectedNodeDataFieldName = value;
    }

};

 Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField.registerClass("Telerik.Sitefinity.Web.UI.Fields.GenericHierarchicalField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);