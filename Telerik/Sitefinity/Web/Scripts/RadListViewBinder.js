Telerik.Sitefinity.Web.UI.RadListViewBinder=function(){this._handleItemReordering=false;
this._targetLayoutContainerId=null;
this._targetLayoutContainer=null;
this._containerTag=null;
this._sortStopDelegatge=null;
this._itemIds=null;
this._contentType=null;
this._autoUpdateOrdinals=true;
this._orginalServiceBaseUrl=null;
this._serviceChildItemsBaseUrl=null;
this._parentDataKeyName=null;
this._selectedItems=[];
this._currentItems=[];
Telerik.Sitefinity.Web.UI.RadListViewBinder.initializeBase(this);
};
Telerik.Sitefinity.Web.UI.RadListViewBinder.prototype={initialize:function(){this._orginalServiceBaseUrl=this.get_serviceBaseUrl();
this._containerTag=(this._containerTag!=null)?this._containerTag.toLowerCase():"";
if(this._handleItemReordering){this._sortStopDelegatge=Function.createDelegate(this,this._sortStopHandler);
this._sortStartDelegate=Function.createDelegate(this,this._sortStartHandler);
var sortableElement=jQuery("#"+this._targetLayoutContainerId);
sortableElement.sortable({update:this._sortStopDelegatge,start:this._sortStartDelegate,placeholder:"ui-state-highlight"});
sortableElement.disableSelection();
if(this._itemIds!=null){for(var i=0,length=this._itemIds.length;
i<length;
i++){var el=sortableElement.children().eq(i);
el.data("id",this._itemIds[i]);
}}}$("#"+this.get_targetId()).after('<div class="RadAjax"><div class="raDiv">Loading...</div><div class="raColor"></div></div>');
$(".RadAjax").hide();
Telerik.Sitefinity.Web.UI.RadListViewBinder.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.RadListViewBinder.callBaseMethod(this,"dispose");
},ClearPager:function(){this.set_skip(0);
},DataBind:function(dataKey){Telerik.Sitefinity.Web.UI.RadListViewBinder.callBaseMethod(this,"DataBind",null);
var clientManager=this.get_manager();
clientManager.set_urlParams(this.get_urlParams());
if(dataKey instanceof Array){this.set_globalDataKeys($.extend([],dataKey));
}else{if(dataKey&&dataKey!=""){this._serviceBaseUrl=this._serviceChildItemsBaseUrl;
this.get_globalDataKeys()[this._getItemKeyName()]=dataKey;
}else{this._serviceBaseUrl=this._orginalServiceBaseUrl;
this.set_globalDataKeys([]);
}}if(this._skip==null||this._skip==0){var listView=$find(this.get_targetId());
var currentPageIndex=listView.get_currentPageIndex();
var pageSize=listView.get_pageSize();
this._skip=(currentPageIndex*pageSize);
if(this._skip==null){this._skip=0;
}if(!this._take){this._take=pageSize;
}}var jTarget=$("#"+this._targetId);
jTarget.next().filter(".RadAjax").show();
jTarget.hide();
clientManager.GetItemCollection(this);
},ReBind:function(){this.DataBind(this.get_globalDataKeys());
},SaveChanges:function(){if($(document.forms[0]).validate().form()==false){return false;
}this._savingHandler();
return true;
},BindCollection:function(data){data=this.DeserializeData(data);
this._dataBindingHandler(data);
this.ClearTargetLayoutContainer();
var target=this.GetTargetLayoutContainer();
var template=new Sys.UI.Template($get(this._clientTemplates[0]));
this._displayedItemsCount=data.Items.length;
this._selectedItems=[];
this._currentItems=data.Items;
for(var i=0;
i<data.Items.length;
i++){var dataItem=data.Items[i];
this.LoadDataItemKey(dataItem);
var key=this.GetItemKey(i);
this._itemDataBindingHandler(dataItem);
this.LoadItem(target,template,dataItem,data.IsGeneric,i);
var itemElement;
itemElement=jQuery(target).children()[i];
jQuery(itemElement).data("id",key.Id);
if(this._handleItemReordering){jQuery(itemElement).data("dataItem",dataItem);
}$(itemElement).find("[id]").each(function(i1,element){var attr=$(element).attr("id");
var attrUnique=attr.substring(0,attr.length-1)+i;
$(element).attr("id",attrUnique);
});
$(itemElement).find("[for]").each(function(i1,element){var attrUnique=$(element).attr("for")+i;
$(element).attr("for",attrUnique);
});
this.BuildInputWrappers(itemElement);
this.AssignCommands(itemElement,dataItem,key,i);
this.ApplyValidationRules(itemElement,i);
this._itemDataBoundHandler(key,dataItem,i,itemElement);
this.EnableActionMenus(itemElement);
this.EnsureTextLength(itemElement,21);
}this._dataBoundHandler();
$(".RadAjax").hide();
var jTarget=$("#"+this._targetId);
jTarget.show();
},GetTargetLayoutContainer:function(){if(this._targetLayoutContainer){return this._targetLayoutContainer;
}this._targetLayoutContainer=$get(this._targetLayoutContainerId);
return this._targetLayoutContainer;
},ClearTargetLayoutContainer:function(){var target=this.GetTargetLayoutContainer();
if(target!=null){if(target.hasChildNodes()){while(target.childNodes.length>=1){target.removeChild(target.firstChild);
}}}},add_onItemReordered:function(delegate){this.get_events().addHandler("onItemReordered",delegate);
},remove_onItemReordered:function(delegate){this.get_events().removeHandler("onItemReordered",delegate);
},get_currentItemElements:function(){return jQuery(this.GetTargetLayoutContainer()).children();
},get_currentItems:function(){return this._currentItems;
},selectAll:function(){this._selectedItems=[];
for(var selected=0;
selected<this.get_currentItems().length;
selected++){}for(var elementCount=0;
elementCount<this.get_currentItemElements().length;
elementCount++){var itemElement=this.get_currentItemElements()[elementCount];
$(itemElement).removeClass("sfSel");
var key=[];
key.Id=this.get_currentItems()[elementCount].Id;
this._itemSelectCommandHandler(key,this.get_currentItems()[elementCount],elementCount,this.get_currentItemElements()[elementCount]);
}},deselectAll:function(){for(var elementCount=0;
elementCount<this.get_currentItemElements().length;
elementCount++){var itemElement=this.get_currentItemElements()[elementCount];
$(itemElement).addClass("sfSel");
}for(var selected=0;
selected<this.get_currentItems().length;
selected++){var key=[];
key.Id=this.get_currentItems()[selected].Id;
this._itemSelectCommandHandler(key,this.get_currentItems()[selected],selected,this.get_currentItemElements()[selected]);
}this._selectedItems=[];
},selectByIds:function(ids){if(!ids||ids.length<=0){return;
}for(var elementCount=0;
elementCount<this.get_currentItemElements().length;
elementCount++){var itemElement=this.get_currentItemElements()[elementCount];
var currentItem=this.get_currentItems()[elementCount];
if(ids.indexOf(currentItem.OriginalContentId)>=0){$(itemElement).removeClass("sfSel");
var key=[];
key.Id=currentItem.Id;
this._itemSelectCommandHandler(key,this.get_currentItems()[elementCount],elementCount,this.get_currentItemElements()[elementCount]);
}}},_onItemReorderedHandler:function(sender,eventArgs){var h=this.get_events().getHandler("onItemReordered");
if(h){h(sender,eventArgs);
}return eventArgs;
},_sortStopHandler:function(event,ui){var el=ui.item;
var newIndex=jQuery("#"+this._targetLayoutContainerId).children().index(el);
var oldIndex=jQuery(el).data("startIndex");
var id=el.data("id");
if(this._autoUpdateOrdinals){this._updateOrdinal(id,oldIndex,newIndex);
}var args={dataItem:el.data("dataItem"),oldIndex:oldIndex,newIndex:newIndex};
this._onItemReorderedHandler(el,args);
},_sortStartHandler:function(event,ui){var el=ui.item;
var startIndex=jQuery("#"+this._targetLayoutContainerId).children().index(el);
jQuery(el).data("startIndex",startIndex);
},_onItemSelecting:function(key,dataItem,itemIndex,itemElement){var action="";
if($(itemElement).hasClass("sfSel")){$(itemElement).removeClass("sfSel");
$(itemElement).find(".selectCommand:checkbox").removeAttr("checked");
action="deselect";
}else{$(itemElement).addClass("sfSel");
$(itemElement).find(".selectCommand:checkbox").attr("checked","checked");
action="select";
}this._addRemoveSelectedItem(key,dataItem,action);
},_addRemoveSelectedItem:function(key,dataItem,action){var j=0;
if(action=="deselect"){while(j<this._selectedItems.length){var key1=Sys.Serialization.JavaScriptSerializer.serialize(this._selectedItems[j].Key);
var key2=Sys.Serialization.JavaScriptSerializer.serialize(key.Id?key.Id:key);
if(key1==key2){this._selectedItems.splice(j,1);
return;
}j++;
}}if(action=="select"){var item=[];
item.Key=key.Id?key.Id:key;
item.Item=dataItem;
this._selectedItems.push(item);
}},_updateOrdinal:function(itemId,oldPosition,newPosition){if(oldPosition==newPosition){return;
}var clientManager=this.get_manager();
var serviceUrl=this._serviceBaseUrl;
var urlParams=[];
if(this._providerName!=null){urlParams.provider=this._providerName;
}if(this._contentType!=null){urlParams.itemType=this._contentType;
}urlParams.oldPosition=oldPosition+"";
urlParams.newPosition=newPosition+"";
var keys=[];
var globalKeys=this.get_globalDataKeys();
var globalKeysLen=globalKeys.length;
for(var i=0;
i<globalKeysLen;
i++){keys.push(globalKeys[i]);
}serviceUrl+="/reorder/";
keys.push(itemId);
var data={Id:itemId,oldPosition:oldPosition,newPosition:newPosition};
clientManager.InvokePut(serviceUrl,urlParams,keys,data,this._saveChangesSuccess,this._saveChangesFailure,this);
keys.pop();
},_saveChangesSuccess:function(caller,result,webRequest){dialogBase.close();
},_saveChangesFailure:function(result){alert(result.Detail);
},_getItemKeyName:function(){if(this.get_dataKeyNames().length==0){alert("You must specify a data key name on the RadListViewBinder!");
return;
}if(this.get_dataKeyNames().length>1){alert("RadListViewBinder does not support composite keys at this moment.");
return;
}return this.get_dataKeyNames()[0];
},get_itemIds:function(){return this._itemIds;
},set_itemIds:function(value){this._itemIds=value;
},get_contentType:function(){return this._contentType;
},set_contentType:function(value){this._contentType=value;
},get_serviceChildItemsBaseUrl:function(){return this._serviceChildItemsBaseUrl;
},set_serviceChildItemsBaseUrl:function(value){if(this._serviceChildItemsBaseUrl!=value){this._serviceChildItemsBaseUrl=value;
this.raisePropertyChanged("serviceChildItemsBaseUrl");
}},set_serviceBaseUrl:function(value){Telerik.Sitefinity.Web.UI.RadListViewBinder.callBaseMethod(this,"set_serviceBaseUrl",[value]);
this._orginalServiceBaseUrl=value;
},get_parentDataKeyName:function(){return this._parentDataKeyName;
},set_parentDataKeyName:function(value){if(this._parentDataKeyName!=value){this._parentDataKeyName=value;
this.raisePropertyChanged("parentDataKeyName");
}},get_selectedItemsCount:function(){return this._selectedItems.length;
},get_selectedItems:function(){var dataItems=new Array();
for(var idx=0,lelLength=this._selectedItems.length;
idx<lelLength;
idx++){dataItems.push(this._selectedItems[idx].Item);
}return dataItems;
},get_pagerId:function(){return this._pagerId;
},set_pagerId:function(value){if(this._pagerId!=value){this._pagerId=value;
}}};
Telerik.Sitefinity.Web.UI.RadListViewBinder.registerClass("Telerik.Sitefinity.Web.UI.RadListViewBinder",Telerik.Sitefinity.Web.UI.ClientBinder);