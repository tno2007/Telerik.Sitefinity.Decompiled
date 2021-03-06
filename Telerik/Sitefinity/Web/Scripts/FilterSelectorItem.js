Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");
Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem=function(element){this._itemLogicalOperator=null;
this._groupLogicalOperator=null;
this._queryItems=[];
this._queryData=null;
this._queryDataName=null;
this._queryFieldName=null;
this._queryFieldType=null;
this._conditionOperator=null;
this._selectorResultView=null;
this._itemTranslatorDelegate=null;
this._collectionTranslatorDelegate=null;
this._itemBuilderDelegate=null;
this._collectionBuilderDelegate=null;
this._group=null;
this._selected=null;
this._container=null;
this._staticQueryItems=null;
this._onLoadDelegate=null;
Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.callBaseMethod(this,"initialize");
this._onLoadDelegate=Function.createDelegate(this,this.onLoad);
Sys.Application.add_load(this._onLoadDelegate);
},dispose:function(){Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.callBaseMethod(this,"dispose");
if(this._itemTranslatorDelegate!=null){delete this._itemTranslatorDelegate;
}if(this._collectionTranslatorDelegate!=null){delete this._collectionTranslatorDelegate;
}if(this._itemBuilderDelegate!=null){delete this._itemBuilderDelegate;
}if(this._collectionBuilderDelegate!=null){delete this._collectionBuilderDelegate;
}delete this._onLoadDelegate;
},refreshUI:function(){var itemsTranslator=this.get_collectionTranslatorDelegate();
var values=itemsTranslator.call(this,this._queryItems);
if(this._selectorResultView){this._selectorResultView.set_selectedValues(values);
}},applyChanges:function(){this.removeGroup();
if(!this._selected){return;
}if(this._selectorResultView){var items=this._selectorResultView.get_selectedItems();
if(items&&items.length>0){this._group=this._queryData.addGroup(this._queryDataName,this._groupLogicalOperator);
var itemsBuilder=this.get_collectionBuilderDelegate();
itemsBuilder.call(this,this._queryData,items);
}}else{if(this._staticQueryItems&&this._staticQueryItems.length>0){this._group=this._queryData.addGroup(this._queryDataName,this._groupLogicalOperator);
for(var i=0,length=this._staticQueryItems.length;
i<length;
i++){this._queryData.addQueryItemToGroup(this._group,this._staticQueryItems[i]);
}}}},displayContainer:function(show){if(show){jQuery("#"+this._container).css("display","");
}else{jQuery("#"+this._container).css("display","none");
}},removeGroup:function(){if(this._group){this._queryData.removeGroup(this._group);
}},onLoad:function(){},_translateQueryItems:function(collection){var result=[];
var itemTranslator=this.get_itemTranslatorDelegate();
for(var i=0,l=collection.length;
i<l;
i++){var translatedItem=itemTranslator.call(this,collection[i]);
result.push(translatedItem);
}return result;
},_translateQueryItem:function(item){if(item.Value){return item.Value;
}return item;
},_buildQueryItems:function(queryData,collection){var itemBuilder=this.get_itemBuilderDelegate();
for(var i=0,l=collection.length;
i<l;
i++){itemBuilder.call(this,queryData,collection[i]);
}},_buildQueryItem:function(queryData,item){queryData.addChildToGroup(this._group,item.Name,this._itemLogicalOperator,this._queryFieldName,this._queryFieldType,this._conditionOperator,item.Id);
},get_itemTranslatorDelegate:function(){if(!this._itemTranslatorDelegate){return this._translateQueryItem;
}return this._itemTranslatorDelegate;
},set_itemTranslatorDelegate:function(value){if(this._itemTranslatorDelegate!=null){delete this._itemTranslatorDelegate;
}if(!value||typeof value!="function"){return;
}this._itemTranslatorDelegate=value;
},get_collectionTranslatorDelegate:function(){if(!this._collectionTranslatorDelegate){return this._translateQueryItems;
}return this._collectionTranslatorDelegate;
},set_collectionTranslatorDelegate:function(value){if(this._collectionTranslatorDelegate!=null){delete this._collectionTranslatorDelegate;
}if(!value||typeof value!="function"){return;
}this._collectionTranslatorDelegate=value;
},get_itemBuilderDelegate:function(){if(!this._itemBuilderDelegate){return this._buildQueryItem;
}return this._itemBuilderDelegate;
},set_itemBuilderDelegate:function(value){if(this._itemBuilderDelegate!=null){delete this._itemBuilderDelegate;
}if(!value||typeof value!="function"){return;
}this._itemBuilderDelegate=value;
},get_collectionBuilderDelegate:function(){if(!this._collectionBuilderDelegate){return this._buildQueryItems;
}return this._collectionBuilderDelegate;
},set_collectionBuilderDelegate:function(value){if(this._collectionBuilderDelegate!=null){delete this._collectionBuilderDelegate;
}if(!value||typeof value!="function"){return;
}this._collectionBuilderDelegate=value;
},get_queryData:function(){return this._queryData;
},set_queryData:function(value){this._queryData=value;
this._group=this._queryData.getItemByName(this._queryDataName);
if(this._group!=null){this._queryItems=this._queryData.getChildren(this._group);
}this.refreshUI();
},get_queryDataName:function(){return this._queryDataName;
},set_queryDataName:function(value){this._queryDataName=value;
},get_queryFieldName:function(){return this._queryFieldName;
},set_queryFieldName:function(value){this._queryFieldName=value;
},get_queryFieldType:function(){return this._queryFieldType;
},set_queryFieldType:function(value){this._queryFieldType=value;
},get_conditionOperator:function(){return this._conditionOperator;
},set_conditionOperator:function(value){this._conditionOperator=value;
},get_selectorResultView:function(){return this._selectorResultView;
},set_selectorResultView:function(value){this._selectorResultView=value;
},get_groupLogicalOperator:function(){return this._groupLogicalOperator;
},set_groupLogicalOperator:function(value){this._groupLogicalOperator=value;
},get_container:function(){return this._container;
},set_container:function(value){this._container=value;
},get_itemLogicalOperator:function(){return this._itemLogicalOperator;
},set_itemLogicalOperator:function(value){this._itemLogicalOperator=value;
},get_selected:function(){return this._selected;
},set_selected:function(value){if(this._selected!=null){this.displayContainer(value);
}this._selected=value;
},get_staticQueryItems:function(){return this._staticQueryItems;
},set_staticQueryItems:function(value){this._staticQueryItems=value;
},get_queryItems:function(){this._group=this._queryData.getItemByName(this._queryDataName);
var result=[];
if(this._group!=null){result.push(this._group);
var children=this._queryData.getChildren(this._group);
for(var i=0,length=children.length;
i<length;
i++){result.push(children[i]);
}}return result;
}};
Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem.registerClass("Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem",Sys.UI.Control);
