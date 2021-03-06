Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI");
Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField=function(element){this._element=element;
this._webServiceUrl=null;
this._rootNodeId=null;
this._rootNode=null;
this._provider=null;
this._initialNodesCount=0;
this._selectedNode=null;
this._selectedNodeOriginal=null;
this._changeSelectedNodeButton=null;
this._selectedNodePanel=null;
this._selectedNodeLabel=null;
this._nodeSelector=null;
this._dataItem=null;
this._loadDelegate=null;
this._selectionDoneDelegate=null;
this._changeSelectedNodeClickDelegate=null;
this._selectorDataBoundDelegate=null;
this._nodesRootChoiceChangedDelegate=null;
this._selectorItemDataBoundDelegate=null;
Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.initializeBase(this,[element]);
};
Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.prototype={initialize:function(){this._loadDelegate=Function.createDelegate(this,this._load);
Sys.Application.add_load(this._loadDelegate);
this._changeSelectedNodeClickDelegate=Function.createDelegate(this,this._changeSelectedNodeClick);
if(this._changeSelectedNodeButton){$addHandler(this._changeSelectedNodeButton,"click",this._changeSelectedNodeClickDelegate);
}this._selectionDoneDelegate=Function.createDelegate(this,this._handleSelectionDone);
this._nodesRootChoiceChangedDelegate=Function.createDelegate(this,this._handleNodesRootChoiceChanged);
if(this._nodeSelector){this._nodeSelector.add_selectionDone(this._selectionDoneDelegate);
this._nodeSelector.add_nodesRootChoiceChanged(this._nodesRootChoiceChangedDelegate);
}var fakeRootNode={Id:Telerik.Sitefinity.getEmptyGuid()};
fakeRootNode=Sys.Serialization.JavaScriptSerializer.serialize(fakeRootNode);
this._rootNode=fakeRootNode;
this._rootNodeId=Telerik.Sitefinity.getEmptyGuid();
Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.callBaseMethod(this,"initialize");
},dispose:function(){this._rootNodeId=null;
this._provider=null;
if(this._loadDelegate!=null){delete this._loadDelegate;
}if(this._changeSelectedNodeClickDelegate!=null){delete this._changeSelectedNodeClickDelegate;
}if(this._selectionDoneDelegate!=null){delete this._selectionDoneDelegate;
}if(this._nodesRootChoiceChangedDelegate!=null){delete this._nodesRootChoiceChangedDelegate;
}if(this._selectorDataBoundDelegate){delete this._selectorDataBoundDelegate;
}if(this._selectorItemDataBoundDelegate){delete this._selectorItemDataBoundHandler;
}Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.callBaseMethod(this,"dispose");
},_configureServiceUrls:function(taxonId){},get_provider:function(){return this._provider;
},set_provider:function(value){this._provider=value;
},get_selectedNode:function(){return this._selectedNode;
},get_value:function(){if(this._selectedNode!=null){return this._selectedNode.Id;
}return Telerik.Sitefinity.getEmptyGuid();
},set_value:function(value){},_setValueInternal:function(value){this._selectedNode=value;
if(this._selectedNode&&this._selectedNode.Id!=this._rootNodeId){$(this._selectedNodeLabel).text(this._selectedNode.Title);
$(this._selectedNodePanel).show();
this._nodeSelector.get_nodesRadio().checked=true;
}else{$(this._selectedNodeLabel).text("");
}this._valueChangedHandler();
},set_dataItem:function(value){this._hasChanged=false;
this._dataItem=value;
var parent=null;
if(value.Parent){parent=value.Parent;
}else{if(value.ParentTaxonId!=null&&value.ParentTaxonTitle!=null){parent={Id:value.ParentTaxonId,Title:value.ParentTaxonTitle};
}}this._selectedNodeOriginal=parent;
this._setValueInternal(this._selectedNodeOriginal);
this._doExpandHandler();
},reset:function(){Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.callBaseMethod(this,"reset");
this.clearSelection();
},clearSelection:function(){this.selectedNode=null;
$(this._selectedNodePanel).hide();
this._nodeSelector.hideTreePanel();
this._nodeSelector.get_rootRadio().checked=true;
},focus:function(){},blur:function(){var behavior=this._get_ExpandableExtenderBehavior();
var value=this.get_value();
if(value){this._nodeSelector.showTreePanel();
$(this._selectedNodePanel).hide();
behavior.reset();
}else{this._nodeSelector.hideTreePanel();
$(this._selectedNodePanel).show();
}},isChanged:function(){if(this._hasChanged==true){if(!this.get_value()){return false;
}if(!this._selectedNodeOriginal){return true;
}if(this.get_value().Id==this._selectedNodeOriginal.Id){return false;
}else{return true;
}}else{return false;
}},_load:function(){this._selectorDataBoundDelegate=Function.createDelegate(this,this._selectorDataBoundHandler);
this._selectorItemDataBoundDelegate=Function.createDelegate(this,this._selectorItemDataBoundHandler);
this.get_nodeSelector().get_treeBinder().add_onDataBound(this._selectorDataBoundDelegate);
this.get_nodeSelector().get_treeBinder().add_onItemDataBound(this._selectorItemDataBoundDelegate);
},_changeSelectedNodeClick:function(){this._nodeSelector.set_showBinder(true);
var context=null;
if(typeof this._selectedNode!="undefined"&&this._selectedNode!=null&&this._selectedNode.Id!=$sitefinity.getEmptyGuid()){context=this._selectedNode;
}this._nodeSelector.dataBind(null,context);
$(this._selectedNodePanel).hide();
},_handleSelectionDone:function(sender,args){this._hasChanged=true;
this._setValueInternal(args.selectedNode);
this._nodeSelector.hideTreePanel();
},_selectorDataBoundHandler:function(sender,args){if(this._dataItem&&this._dataItem.ParentTaxonId){var currentlySelectedNode=this._getSelectorNode(sender,this._dataItem.ParentTaxonId);
if(currentlySelectedNode){currentlySelectedNode.select();
}}},_selectorItemDataBoundHandler:function(sender,args){var dataItem=args.get_dataItem();
if(this._dataItem&&dataItem.Id===this._dataItem.Id){var nodeToDisable=this._getSelectorNode(sender,dataItem.Id);
if(nodeToDisable){nodeToDisable.set_enabled(false);
}}},_getSelectorNode:function(binder,value){var radTreeView=$find(binder.get_targetId());
return radTreeView.findNodeByValue(value);
},_handleNodesRootChoiceChanged:function(sender,args){this._hasChanged=true;
var selectedNode=this._nodeSelector.get_selectedNode();
if(selectedNode){this._setValueInternal(selectedNode);
}else{this._setValueInternal(Sys.Serialization.JavaScriptSerializer.deserialize(this._rootNode));
}$(this._selectedNodePanel).hide();
},_get_ExpandableExtenderBehavior:function(){if(this._expandableExtenderBehavior){return this._expandableExtenderBehavior;
}this._expandableExtenderBehavior=Sys.UI.Behavior.getBehaviorByName(this._element,"ExpandableExtender");
return this._expandableExtenderBehavior;
},get_changeSelectedNodeButton:function(){return this._changeSelectedNodeButton;
},set_changeSelectedNodeButton:function(value){this._changeSelectedNodeButton=value;
},get_selectedNodePanel:function(){return this._selectedNodePanel;
},set_selectedNodePanel:function(value){this._selectedNodePanel=value;
},get_selectedNodeLabel:function(){return this._selectedNodeLabel;
},set_selectedNodeLabel:function(value){this._selectedNodeLabel=value;
},get_nodeSelector:function(){return this._nodeSelector;
},set_nodeSelector:function(value){this._nodeSelector=value;
},set_uiCulture:function(culture){Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.callBaseMethod(this,"set_uiCulture",[culture]);
this.get_nodeSelector().set_uiCulture(culture);
}};
Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField.registerClass("Telerik.Sitefinity.Taxonomies.Web.UI.HierarchicalTaxonParentSelectorField",Telerik.Sitefinity.Web.UI.Fields.FieldControl,Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem,Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);
