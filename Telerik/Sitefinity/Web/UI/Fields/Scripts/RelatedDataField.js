Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.RelatedDataField=function(element){Telerik.Sitefinity.Web.UI.Fields.RelatedDataField.initializeBase(this,[element]);
this._relatedDataService=null;
this._genericDataService=null;
this._dataSourceService=null;
this._resetDelegate=null;
this._updatedRelatedData=[];
this._duplicatedRelatedData=[];
this._relatedDataFieldViewModel=null;
this._relatedDataSelectorWindow=null;
this.RelatedDataSelector=null;
this.RelatedDataGrid=null;
this._pageSize=10;
this._siteBaseUrl=null;
this._allowMultipleSelection=null;
this._relatedDataType=null;
this._relatedDataProvider=null;
this._relatedTypeIdentifierField=null;
this._createRelatedItemUrl=null;
this._isBackendReadMode=null;
this._isMultilingual=null;
this._initializeControl=true;
this._isCreateMode=false;
this._isDuplicateMode=false;
this._relatedDataSupportsMultilingualSearch=true;
this._dataItemInfo=null;
this._gridParameterMap=null;
this._saveRelationChangesSuccessDelegate=null;
this._relatedDataSelectorTitle=null;
this._isChanged=false;
this._defaultRelatedItemsSortExpression=null;
this._minOrdinal=null;
this._siteId=null;
};
Telerik.Sitefinity.Web.UI.Fields.RelatedDataField.prototype={initialize:function(){if(!this._initializeControl){return false;
}Telerik.Sitefinity.Web.UI.Fields.RelatedDataField.callBaseMethod(this,"initialize");
this._resetDelegate=Function.createDelegate(this,this.reset);
this._saveRelationChangesSuccessDelegate=Function.createDelegate(this,this.saveRelationChangesSuccess);
var that=this;
requirejs.config({baseUrl:this._siteBaseUrl+"Res",paths:{RelatedDataSelector:"Telerik.Sitefinity.RelatedData.Web.UI.Scripts.RelatedDataSelector",RelatedDataGrid:"Telerik.Sitefinity.RelatedData.Web.UI.Scripts.RelatedDataGrid",RelatedDataSelectorTemplate:"Telerik.Sitefinity.RelatedData.Web.UI.Templates.RelatedDataSelector.sfhtml",RelatedDataGridTemplate:"Telerik.Sitefinity.RelatedData.Web.UI.Templates.RelatedDataGrid.sfhtml",ContentSelectorBase:"Telerik.Sitefinity.RelatedData.Web.UI.Scripts.ContentSelectorBase",FlatContentSelector:"Telerik.Sitefinity.RelatedData.Web.UI.Scripts.FlatContentSelector",FlatContentSelectorTemplate:"Telerik.Sitefinity.RelatedData.Web.UI.Templates.FlatContentSelector.sfhtml",DetailViewEditingWindow:"Telerik.Sitefinity.Web.RequireJSModules.Scripts.DetailViewEditingWindow",ContentRepository:"Telerik.Sitefinity.Web.UI.PublicControls.InlineEditing.Scripts.ContentRepository"},map:{"*":{text:this._siteBaseUrl+"ExtRes/Telerik.Sitefinity.Resources.Scripts.RequireJS.text.js",}},waitSeconds:0});
this._relatedDataFieldViewModel=kendo.observable({isBackendReadMode:this._isBackendReadMode,isDuplicateMode:this._isDuplicateMode,hideSelectButton:function(){return this.get("isDuplicateMode")||this.get("isBackendReadMode");
},openRelatedDataSelector:function(e){e.preventDefault();
require(["RelatedDataSelector"],function(RelatedDataSelector){if(that._relatedDataSelectorWindow==null){that._relatedDataSelectorWindow=$(that._element).find("#relatedDataSelectorWindow").kendoWindow({actions:[],title:"[Content type name] selector",resizable:false,modal:true,animation:false}).addClass("sfSelectorDialog").data("kendoWindow");
that._relatedDataSelectorWindow.content('<div id="windowContainer"></div>').center();
}that._relatedDataSelectorWindow.open();
var selectorParameterMap=that.getSelectorParameterMap();
var settings={parameterMap:selectorParameterMap,selectedItemsParameterMap:that._gridParameterMap,selectedItemsServiceUrl:that._relatedDataService+"/child-items",dataSourceServiceUrl:that._dataSourceService+"/providers",serviceUrl:that._genericDataService+"/data-items",enableSearch:true,enableProvidersSelector:that._relatedDataProvider==="sf-any-site-provider",enableMultilingualSearch:false,relatedDataSupportsMultilingualSearch:that._relatedDataSupportsMultilingualSearch,isMultilingual:that._isMultilingual,relatedTypeIdentifierField:that._relatedTypeIdentifierField,culture:that._uiCulture,siteBaseUrl:that._siteBaseUrl,removeTempItemServiceUrl:that._genericDataService,allowMultipleSelection:that._allowMultipleSelection,applyMasterItemFilter:that._correctMasterItemFilterFlag(true,that._relatedDataType),itemType:that._relatedDataType,createItemUrl:that._createRelatedItemUrl,container:$(that._relatedDataSelectorWindow.element).find("#windowContainer"),kendoWindow:that._relatedDataSelectorWindow,isCreateMode:that._isCreateMode,title:that._relatedDataSelectorTitle,siteId:that._siteId};
if(that._defaultRelatedItemsSortExpression){var sortExpressionArray=that._defaultRelatedItemsSortExpression.split(" ");
if(sortExpressionArray.length===2){settings.sort={field:sortExpressionArray[0],dir:sortExpressionArray[1]};
}}that.RelatedDataSelector=new RelatedDataSelector(settings);
$(that.RelatedDataSelector).on("onDone",function(event,data){if(that._isCreateMode){that.addItems(data.Items);
}else{that.RelatedDataGrid.rebind();
}});
$(that.RelatedDataSelector.RelatedDataGrid).on("onItemSelected",function(event,relation){var changes=relation.Changes;
for(var i=0;
i<changes.length;
i++){var data=changes[i];
switch(data.Action){case"Add":if(!that._isCreateMode){if(!that._allowMultipleSelection&&that.RelatedDataSelector.SelectedItemsGrid){var currentlySelectedItems=that.RelatedDataSelector.SelectedItemsGrid.getDataItems();
if(currentlySelectedItems.length>0){var deselectItem=currentlySelectedItems[0].toJSON();
var isMarkedToRemove=false;
for(var j=0;
j<changes.length;
j++){if(changes[j].Action=="Remove"&&changes[j].Item.Id==deselectItem.Id){isMarkedToRemove=true;
break;
}}if(!isMarkedToRemove){that.removeItem(deselectItem,function(data,status,xhr){if(that.RelatedDataSelector.SelectedItemsGrid){that.RelatedDataSelector.SelectedItemsGrid.rebind();
}});
}}}that.addItem(data.Item);
}else{if(that.RelatedDataSelector.SelectedItemsGrid){that.RelatedDataSelector.SelectedItemsGrid.addSelectedDataItem(data.Item);
}}break;
case"Remove":if(!that._isCreateMode){that.removeItem(data.Item);
}else{if(that.RelatedDataSelector.SelectedItemsGrid){that.RelatedDataSelector.SelectedItemsGrid.removeDataItem(data.Item);
}}break;
default:break;
}}});
$(that.RelatedDataSelector.SelectedItemsGrid).on("onItemSelected",function(event,relation){var changes=relation.Changes;
for(var i=0;
i<changes.length;
i++){var data=changes[i];
switch(data.Action){case"Add":if(!that._isCreateMode){that.addItem(data.Item,undefined,function(data,status,xhr){that.RelatedDataSelector.SelectedItemsGrid.rebind();
});
}else{}break;
case"Remove":if(!that._isCreateMode){that.removeItem(data.Item,function(data,status,xhr){that.RelatedDataSelector.SelectedItemsGrid.rebind();
});
}else{if(that.RelatedDataSelector.SelectedItemsGrid){that.RelatedDataSelector.SelectedItemsGrid.removeDataItem(data.Item);
}if(that.RelatedDataSelector.RelatedDataGrid){that.RelatedDataSelector.RelatedDataGrid.removeDataItemFromSelected(data.Item);
}}break;
default:break;
}}});
$(that.RelatedDataSelector.RelatedDataGrid).on("onItemCreated",function(event,item){if(!that._isCreateMode){if(!that._allowMultipleSelection&&that.RelatedDataSelector.SelectedItemsGrid){var currentlySelectedItems=that.RelatedDataSelector.SelectedItemsGrid.getDataItems();
if(currentlySelectedItems.length>0){var deselectItem=currentlySelectedItems[0].toJSON();
that.removeItem(deselectItem,function(data,status,xhr){});
}}that.addItem(item,undefined,function(data,status,xhr){var activeGrid=that.RelatedDataSelector.getActiveGridView();
activeGrid.rebind();
if(activeGrid!==that.RelatedDataSelector.SelectedItemsGrid){that.RelatedDataSelector.SelectedItemsGrid.rebind();
}});
}else{var params={ItemId:item.Id,ItemType:that._relatedDataType,ItemProvider:item.ProviderName};
if(that._dataItemInfo.ItemId){params.RelatedItemId=that._dataItemInfo.ItemId;
params.RelatedItemType=that._dataItemInfo.ItemType;
params.RelatedItemProvider=that._dataItemInfo.ItemProvider;
}$.ajax({type:"GET",url:that._genericDataService+"/data-items?"+$.param(params),contentType:"application/json",cache:false,beforeSend:function(xhr){if(that._uiCulture){xhr.setRequestHeader("SF_UI_CULTURE",that._uiCulture);
}xhr.setRequestHeader("IsBackendRequest",true);
},success:function(data,status,xhr){if(data.Items.length>0){var selectedItem=data.Items[0];
if(selectedItem&&that.RelatedDataSelector.SelectedItemsGrid){that.RelatedDataSelector.SelectedItemsGrid.addSelectedDataItem(selectedItem);
that.RelatedDataSelector.RelatedDataGrid.rebind();
}}},error:function(xhr,status,error){alert(xhr.responseText);
}});
}});
$(that.RelatedDataSelector).on("onCancel",function(event,data){});
var selectedItems=that.RelatedDataGrid&&that._isCreateMode?that.RelatedDataGrid.getDataItems():new kendo.data.ObservableArray([]);
that.RelatedDataSelector.open(selectedItems,true);
});
}});
var wrapper=$(this._element).find("#relatedDataFieldContainer");
kendo.bind(wrapper,this._relatedDataFieldViewModel);
},dispose:function(){Telerik.Sitefinity.Web.UI.Fields.RelatedDataField.callBaseMethod(this,"dispose");
if(this._resetDelegate){delete this._resetDelegate;
}if(this._saveRelationChangesSuccessDelegate){delete this._saveRelationChangesSuccessDelegate;
}this._updatedRelatedData=[];
delete this._updatedRelatedData;
},reset:function(){Telerik.Sitefinity.Web.UI.Fields.RelatedDataField.callBaseMethod(this,"reset");
this._updatedRelatedData=[];
this._isChanged=false;
this._isDuplicateMode=false;
if(this._relatedDataFieldViewModel){this._relatedDataFieldViewModel.set("isDuplicateMode",false);
}this._duplicatedRelatedData=[];
},dataBind:function(dataItem,dataItemType,providerName,propertyName,setDefault,duplicationData){if(!this._initializeControl||setDefault){return false;
}this.reset();
var that=this;
this._isCreateMode=false;
var parameterMap=this.getGridParameterMap(dataItem,dataItemType,providerName,propertyName,duplicationData.parentItemId);
if(!parameterMap.ParentItemId||parameterMap.ParentItemId===Telerik.Sitefinity.getEmptyGuid()){this._isCreateMode=true;
}if(dataItem.IsDuplicate){this._relatedDataFieldViewModel.set("isDuplicateMode",true);
this._isCreateMode=true;
this._isDuplicateMode=true;
this._isChanged=true;
if(duplicationData.relatedDataLinks){this._duplicatedRelatedData=duplicationData.relatedDataLinks;
}}var visibleColumnKeys=this.getVisibleColumnKeys(dataItem.IsDuplicate);
var autoBind=!this._isCreateMode||this._isDuplicateMode;
require(["RelatedDataGrid"],function(RelatedDataGrid){var gridOptions={parentElement:$(that._element).find("#relatedDataGridContainer"),culture:that._uiCulture,serviceUrl:that._relatedDataService+"/child-items",parameterMap:parameterMap,enableSearch:false,enableMultilingualSearch:false,relatedDataSupportsMultilingualSearch:that._relatedDataSupportsMultilingualSearch,relatedTypeIdentifierField:that._relatedTypeIdentifierField,isMultilingual:that._isMultilingual,autoBind:autoBind,isCreateMode:that._isCreateMode,dataSelectable:false,allowMultipleSelection:false,allowSorting:true,applyMasterItemFilter:false,removeTempItemServiceUrl:that._genericDataService,itemType:that._relatedDataType,createItemUrl:that._createRelatedItemUrl,visibleColumnKeys:visibleColumnKeys,isBackendReadMode:that._isBackendReadMode,hideGridHeaderRow:true,hideGridWrapper:true};
if(!that._isCreateMode||that._isDuplicateMode){gridOptions.pagerConfig={pageable:true,serverPaging:true,page:1,pageSize:30};
gridOptions.scrollable={virtual:true};
}else{gridOptions.pagerConfig={pageable:false};
}that.RelatedDataGrid=new RelatedDataGrid(gridOptions);
that.RelatedDataGrid.siteBaseUrl=that._siteBaseUrl;
$(that.RelatedDataGrid).on("onRelatedDataGridRemove",function(event,data){that.removeItem(data.Item,that._saveRelationChangesSuccessDelegate);
});
$(that.RelatedDataGrid).on("onRelatedDataGridSort",function(event,data){that._sortItems(data);
});
$(that._element).find("#relatedDataLoadingContainer").hide();
that.RelatedDataGrid.init();
});
},getVisibleColumnKeys:function(isDuplicateMode){if(isDuplicateMode){return[this._relatedTypeIdentifierField,"Preview"];
}else{return["AllowSorting",this._relatedTypeIdentifierField,"Edit","Preview","Remove"];
}},getSelectorParameterMap:function(){var parameterMap={ItemType:this._relatedDataType,ItemProvider:this._relatedDataProvider,FieldName:this._fieldName,RelatedItemId:this._dataItemInfo.ItemId,RelatedItemType:this._dataItemInfo.ItemType,RelatedItemProvider:this._dataItemInfo.ItemProvider};
return parameterMap;
},getGridParameterMap:function(dataItem,dataItemType,providerName,propertyName,parentItemId){var parentId;
if(dataItem.Id&&dataItem.Id!==Telerik.Sitefinity.getEmptyGuid()){parentId=dataItem.Id;
}var status=null;
if(dataItem.hasOwnProperty("Status")){if(dataItemType!=="Telerik.Sitefinity.Blogs.Model.Blog"){status=dataItem.Status;
if(dataItem.Status!==0){parentId=dataItem.OriginalContentId;
}}}if(!parentId||parentId===Telerik.Sitefinity.getEmptyGuid()){parentId=parentItemId;
}if(dataItemType==="Telerik.Sitefinity.Blogs.Model.Blog"){this._isMultilingual=false;
}this._dataItemInfo={ItemId:parentId,ItemType:dataItemType,ItemProvider:providerName};
this._gridParameterMap={ParentItemId:parentId,ParentItemType:dataItemType,ParentProviderName:providerName,FieldName:propertyName,Status:status};
return this._gridParameterMap;
},addItems:function(newItems){var newItemsIds=[];
$(newItems).each(function(i,item){newItemsIds.push(item.Id||item.id);
});
var currentItemIds=[];
var currentItems=this.RelatedDataGrid.getDataItems().toJSON();
$(currentItems).each(function(i,item){currentItemIds.push(item.Id||item.id);
});
var removedItems=[];
for(var j=0;
j<currentItems.length;
j++){var currentItemId=currentItems[j].id||currentItems[j].Id;
if($.inArray(currentItemId,newItemsIds)==-1){removedItems.push(currentItems[j]);
}}var items=this.RelatedDataGrid.getDataItems().toJSON();
var itemOrdinal=items.length>0?items[items.length-1].Ordinal+1:0;
for(var i=0;
i<newItems.length;
i++){var newItemId=newItems[i].id||newItems[i].Id;
if($.inArray(newItemId,currentItemIds)==-1){this.addItem(newItems[i],itemOrdinal);
itemOrdinal++;
}}for(var k=0;
k<removedItems.length;
k++){this.removeItem(removedItems[k]);
}},addItem:function(item,ordinal,onSuccess){var addedContent={ChildItemId:item.Id,ChildItemType:this._relatedDataType,ChildItemProviderName:item.ProviderName||this._relatedDataProvider};
if(!ordinal&&ordinal!==0){var items=this.RelatedDataGrid.getDataItems().toJSON();
if(items.length>0){ordinal=items[0].Ordinal-1;
}else{ordinal=0;
}if(this._minOrdinal!==null){if(this._minOrdinal<=ordinal){ordinal=this._minOrdinal-1;
}}this._minOrdinal=ordinal;
}addedContent.Ordinal=ordinal;
item.Ordinal=ordinal;
this.actionExecuted({Name:"Add",RelatedContentWrapper:addedContent,DataItem:item,OnSuccess:onSuccess});
},removeItem:function(item,onSuccess){var removedContent={ChildItemId:item.Id,ChildItemType:this._relatedDataType,ChildItemProviderName:item.ProviderName||this._relatedDataProvider,Ordinal:item.Ordinal};
this.actionExecuted({Name:"Remove",RelatedContentWrapper:removedContent,DataItem:item,OnSuccess:onSuccess});
},updateItem:function(item,newOrdinal,onSuccess){if(item.Ordinal!=newOrdinal){var movedContent={ChildItemId:item.Id,ChildItemType:this._relatedDataType,ChildItemProviderName:item.ProviderName||this._relatedDataProvider,Ordinal:newOrdinal};
var dataItem=this.RelatedDataGrid.getDataItem(item.uid);
if(dataItem){dataItem.Ordinal=newOrdinal;
}this.actionExecuted({Name:"Update",RelatedContentWrapper:movedContent,DataItem:dataItem,OnSuccess:onSuccess});
}},actionExecuted:function(args){switch(args.Name){case"Add":if(args.RelatedContentWrapper){this._addUpdatedContent(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Added,args.RelatedContentWrapper,args.OnSuccess);
if(this._isCreateMode){this.RelatedDataGrid.addDataItem(args.DataItem);
}}break;
case"Remove":if(args.RelatedContentWrapper){this._addUpdatedContent(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Removed,args.RelatedContentWrapper,args.OnSuccess);
if(this._isCreateMode){this.RelatedDataGrid.removeDataItem(args.DataItem);
}}break;
case"Update":if(args.RelatedContentWrapper){this._addUpdatedContent(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Updated,args.RelatedContentWrapper,args.OnSuccess);
}break;
default:break;
}},saveRelationChanges:function(newState,relatedContentWrapper,onSuccess){var data={RelationChanges:[{State:newState,ChildItemId:relatedContentWrapper.ChildItemId,ChildItemProviderName:relatedContentWrapper.ChildItemProviderName,ChildItemType:relatedContentWrapper.ChildItemType,ChildItemAdditionalInfo:relatedContentWrapper.ChildItemAdditionalInfo,Ordinal:relatedContentWrapper.Ordinal,ComponentPropertyName:this._fieldName}]};
data=$.extend(data,this._dataItemInfo);
var that=this;
$.ajax({type:"PUT",url:this._relatedDataService+"/relations",data:JSON.stringify(data),contentType:"application/json",cache:false,beforeSend:function(xhr){if(that._uiCulture){xhr.setRequestHeader("SF_UI_CULTURE",that._uiCulture);
}xhr.setRequestHeader("IsBackendRequest",true);
},success:function(data,status,xhr){if(typeof onSuccess==="function"){onSuccess(data,status,xhr);
}},error:function(xhr,status,error){alert(xhr.responseText);
}});
},saveRelationChangesSuccess:function(data,status,xhr){if(!this._isCreateMode){this.RelatedDataGrid.rebind();
}},_addUpdatedContent:function(newState,relatedContentWrapper,onSuccess){this._isChanged=true;
if(!this._isCreateMode){this.saveRelationChanges(newState,relatedContentWrapper,onSuccess);
return;
}var contentChildItemId=relatedContentWrapper.ChildItemId;
var contentChildItemProviderName=relatedContentWrapper.ChildItemProviderName;
var contentChildItemType=relatedContentWrapper.ChildItemType;
var alreadyAddedContent=$.grep(this._updatedRelatedData,function(obj){return obj.ChildItemId===contentChildItemId&&obj.ChildItemProviderName===contentChildItemProviderName&&obj.ChildItemType===contentChildItemType;
});
if(!(alreadyAddedContent.length>0)){this._updatedRelatedData.push({State:newState,ChildItemId:relatedContentWrapper.ChildItemId,ChildItemProviderName:relatedContentWrapper.ChildItemProviderName,ChildItemType:relatedContentWrapper.ChildItemType,ChildItemAdditionalInfo:relatedContentWrapper.ChildItemAdditionalInfo,Ordinal:relatedContentWrapper.Ordinal,ComponentPropertyName:this._fieldName});
}else{var addedContent=alreadyAddedContent[0];
var relatedState=Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState;
if((addedContent.State==relatedState.Removed&&newState==relatedState.Added)||(addedContent.State==relatedState.Added&&newState==relatedState.Removed)){for(var i=this._updatedRelatedData.length-1;
i>=0;
i--){if(this._updatedRelatedData[i].ChildItemId===contentChildItemId&&this._updatedRelatedData[i].ChildItemProviderName===contentChildItemProviderName&&this._updatedRelatedData[i].ChildItemType===contentChildItemType){this._updatedRelatedData.splice(i,1);
}}}if((addedContent.State==relatedState.Updated&&newState==relatedState.Removed)){addedContent.State=relatedState.Removed;
}if(newState==Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Updated){addedContent.Ordinal=relatedContentWrapper.Ordinal;
}}},_sortItems:function(data){var movedItem=data.item;
if(this._isNumber(data.prevOrdinal)&&this._isNumber(data.nextOrdinal)){newOrdinal=this._calculateOrdinalWithRounding(data.prevOrdinal,data.nextOrdinal);
this.updateItem(movedItem,newOrdinal,data.onSuccess);
}else{if(this._isNumber(data.prevOrdinal)){newOrdinal=this._calculateOrdinalWithRounding(data.prevOrdinal,data.prevOrdinal+2);
this.updateItem(movedItem,newOrdinal,data.onSuccess);
}else{if(this._isNumber(data.nextOrdinal)){newOrdinal=this._calculateOrdinalWithRounding(data.nextOrdinal-2,data.nextOrdinal);
this.updateItem(movedItem,newOrdinal,data.onSuccess);
}}}},_calculateOrdinalWithRounding:function(previousOrdinal,nextOrdinal){var average=(previousOrdinal+nextOrdinal)/2;
var roundedAverage=0;
var lastRoundedAverage=null;
var precisionToRound=0;
while(true){roundedAverage=parseFloat(average.toFixed(precisionToRound));
if(roundedAverage>previousOrdinal&&roundedAverage<nextOrdinal){return roundedAverage;
}if(lastRoundedAverage!=null&&lastRoundedAverage===roundedAverage){break;
}lastRoundedAverage=roundedAverage;
precisionToRound++;
if(precisionToRound==20){break;
}}return average;
},_correctMasterItemFilterFlag:function(initialFlag,relatedDataType){if(relatedDataType==="Telerik.Sitefinity.Pages.Model.PageNode"){return false;
}return initialFlag;
},_isNumber:function(n){return typeof n==="number"&&!isNaN(n);
},isChanged:function(){return this._isChanged;
},getChanges:function(){if(this._isDuplicateMode){var that=this;
var relatedData=$.grep(this._duplicatedRelatedData,function(element,index){return element.ComponentPropertyName===that._fieldName;
});
return relatedData;
}else{return this._updatedRelatedData;
}},get_value:function(){if(this.RelatedDataGrid){return this.RelatedDataGrid.getDataItems().toJSON();
}return null;
},set_value:function(value){return;
}};
Telerik.Sitefinity.Web.UI.Fields.RelatedDataField.registerClass("Telerik.Sitefinity.Web.UI.Fields.RelatedDataField",Telerik.Sitefinity.Web.UI.Fields.FieldControl,Telerik.Sitefinity.Web.UI.Fields.IRelatedDataField,Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);
