Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.QueryBuilder=function(element){this._typeProperties=[];
this._queryData=null;
this._containerId=null;
this._queryLevels=[];
this._selectedItemPaths=[];
this._filterDelegate=null;
this._filterButton=null;
this._groupButton=null;
this._groupDelegate=null;
Telerik.Sitefinity.Web.UI.QueryBuilder.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.QueryBuilder.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.QueryBuilder.callBaseMethod(this,"initialize");
if(this._filterDelegate===null){this._filterDelegate=Function.createDelegate(this,this._filterHandler);
}this._filterButton=$get("filterButton");
$addHandler(this._filterButton,"click",this._filterDelegate);
if(this._queryData==null){this._queryData=new Telerik.Sitefinity.Web.UI.QueryData();
this._queryData.set_Title("TestQueryData");
this._queryData.set_Id("50CDB5A4-B2F9-46a8-B02F-6B91A6CC5FD2");
var testQueryItems=[this._queryData.createQueryDataItem(0,"_0",false,"AND",null)];
this._queryData.set_TypeProperties(this._typeProperties);
this._queryData.set_QueryItems(testQueryItems);
}this._buildQueryBuilderUI();
this._groupButton=$get("groupButton");
this._groupDelegate=Function.createDelegate(this,this.onConditionsGrouped);
$addHandler(this._groupButton,"click",this._groupDelegate);
},dispose:function(){Telerik.Sitefinity.Web.UI.QueryBuilder.callBaseMethod(this,"dispose");
if(this._filterDelegate){$removeHandler(this._filterButton,"click",this._filterDelegate);
delete this._filterDelegate;
}if(this._groupDelegate){$removeHandler(this._groupButton,"click",this._groupDelegate);
delete this._groupDelegate;
}},add_filter:function(handler){this.get_events().addHandler("filter",handler);
},remove_filter:function(handler){this.get_events().removeHandler("select",handler);
},_filterHandler:function(event){var h=this.get_events().getHandler("filter");
if(h){h(this._queryData);
}},_buildQueryBuilderUI:function(){this._queryData.sortQueryItems();
this._selectedItemPaths=[];
this._queryLevels=this._queryData.separateQueryInLevels();
$("#"+this._containerId).empty();
this._buildQuery(this._queryData,this._queryLevels);
},_buildQuery:function(queryData,queryLevels){var levelCount=queryLevels.length;
for(var levelIter=0;
levelIter<levelCount;
levelIter++){var itemCount=queryLevels[levelIter].length;
for(var itemIter=0;
itemIter<itemCount;
itemIter++){var parent=this._findParent(queryLevels[levelIter][itemIter].get_ItemPath());
if(parent.length!==0){var itemElementIndex=levelIter==0?queryLevels[levelIter][itemIter].get_Ordinal():queryLevels[levelIter][itemIter].get_Ordinal()+1;
if(queryLevels[levelIter][itemIter].get_IsGroup()){this._createItemOrGroup(queryLevels[levelIter][itemIter],parent.get(0),levelIter,itemElementIndex,true);
}else{this._createItemOrGroup(queryLevels[levelIter][itemIter],parent.get(0),levelIter,itemElementIndex,false);
}}}}},_findParent:function(itemPath){if(this._queryData.getItemLevel(itemPath)===0){var result=$("#"+this._containerId);
return result;
}else{var result=$(".sfG"+this._queryData.getParentItemPath(itemPath));
if(result.length==1){return result;
}else{throw"Couldn't find the group UL by class. Something went wrong when creating the group.";
return null;
}}},_createItemOrGroup:function(queryItem,parentDom,levelIndex,itemElementIndex,isGroup){var template=null;
var templateId=isGroup?"#sfGroupTemplate":"#sfBasicItemTemplate";
template=new Sys.UI.Template($(templateId).get(0));
var element=null;
if(isGroup){element=document.createElement("ul");
element.setAttribute("class","sfGroupUl");
$(parentDom).append(element);
var queryGroupControl=new Telerik.Sitefinity.Web.UI.QueryGroup(element,queryItem);
queryGroupControl.initialize();
queryGroupControl.add_ungroup(Function.createDelegate(this,this.onConditionsUngrouped));
}else{element=document.createElement("li");
$(parentDom).append(element);
var queryItemControl=new Telerik.Sitefinity.Web.UI.QueryItem(element,queryItem);
queryItemControl.initialize();
queryItemControl.remove_select(Function.createDelegate(this,this.onItemSelected));
queryItemControl.remove_conditionadd(Function.createDelegate(this,this.onConditionAdded));
queryItemControl.remove_conditionremove(Function.createDelegate(this,this.onConditionRemoved));
queryItemControl.remove_valuechanged(Function.createDelegate(this,this.onValueChanged));
queryItemControl.add_select(Function.createDelegate(this,this.onItemSelected));
queryItemControl.add_conditionadd(Function.createDelegate(this,this.onConditionAdded));
queryItemControl.add_conditionremove(Function.createDelegate(this,this.onConditionRemoved));
queryItemControl.add_valuechanged(Function.createDelegate(this,this.onValueChanged));
}this._populateJoinTypes(queryItem,element);
if(isGroup){$(element).addClass("sfG"+queryItem.get_ItemPath());
}else{if(!(levelIndex>0&&itemElementIndex==0)){this._populateOperators(queryItem,element);
this._populateProperties(queryItem,element);
$(element).find(".sfValue").get(0).value=queryItem.get_Value();
}}},_populateJoinTypes:function(queryItem,itemElement){this._appendOptionToSelect("AND","AND",$(itemElement).find(".sfJoin:first").get(0));
this._appendOptionToSelect("OR","OR",$(itemElement).find(".sfJoin:first").get(0));
var selectedIndex=queryItem.get_Join()==="AND"?0:1;
$(itemElement).find(".sfJoin:first").get(0).selectedIndex=selectedIndex;
},_appendOptionToSelect:function(text,value,element){var optionToAppend=document.createElement("option");
optionToAppend.setAttribute("text",text);
optionToAppend.setAttribute("value",value);
optionToAppend.innerHTML=text;
element.appendChild(optionToAppend);
},_populateOperators:function(queryItem,itemElement){if(queryItem.get_Condition().get_FieldType()==="string"){this._appendOptionToSelect("Contains","Contains",$(itemElement).find(".sfOperators:first").get(0));
}this._appendOptionToSelect("=","=",$(itemElement).find(".sfOperators:first").get(0));
this._appendOptionToSelect("&gt;","&gt;",$(itemElement).find(".sfOperators:first").get(0));
this._appendOptionToSelect("&lt;","&lt;",$(itemElement).find(".sfOperators:first").get(0));
this._appendOptionToSelect("&lt;&gt;","&lt;&gt;",$(itemElement).find(".sfOperators:first").get(0));
var operator=queryItem.get_Condition().get_Operator();
var selectedIndex=this._getOptionIndex($(itemElement).find(".sfOperators:first").get(0).options,operator);
$(itemElement).find(".sfOperators:first").get(0).selectedIndex=selectedIndex;
},_populateProperties:function(queryItem,itemElement){var properties=this._queryData.get_TypeProperties();
var propertiesCount=properties.length;
var selectedIndex=0;
for(var i=0;
i<propertiesCount;
i++){this._appendOptionToSelect(properties[i],properties[i],$(itemElement).find(".sfFields:first").get(0));
if(properties[i]===queryItem.get_Condition().get_FieldName()){selectedIndex=i;
}}$(itemElement).find(".sfFields:first").get(0).selectedIndex=selectedIndex;
},_getOptionIndex:function(optionArr,optionText){var optionCount=optionArr.length;
for(var optionIndex=0;
optionIndex<optionCount;
optionIndex++){if(optionArr[optionIndex].text===optionText){return optionIndex;
}}return -1;
},onItemSelected:function(itemPath,selected){if(selected){this._selectedItemPaths.push(itemPath);
}else{var indexOfItem=this._selectedItemPaths.indexOf(itemPath);
this._selectedItemPaths.splice(indexOfItem,1);
}},onConditionAdded:function(senderPath){this._queryData.conditionAdded(senderPath);
this._buildQueryBuilderUI();
},onConditionRemoved:function(senderPath){this._queryData.conditionRemoved(senderPath);
this._buildQueryBuilderUI();
},onConditionsUngrouped:function(senderPath){this._queryData.conditionsUngrouped(senderPath);
this._buildQueryBuilderUI();
},onConditionsGrouped:function(itemPaths){try{this._queryData.conditionsGrouped(this._selectedItemPaths);
this._buildQueryBuilderUI();
}catch(ex){alert(ex.message);
}},onValueChanged:function(senderPath,newValue){},get_typeProperties:function(){return this._typeProperties;
},set_typeProperties:function(value){if(this._typeProperties!=value){this._typeProperties=value;
this.raisePropertyChanged("typeProperties");
}},get_containerId:function(){return this._containerId;
},set_containerId:function(value){if(this._containerId!=value){this._containerId=value;
this.raisePropertyChanged("containerId");
}}};
Telerik.Sitefinity.Web.UI.QueryBuilder.registerClass("Telerik.Sitefinity.Web.UI.QueryBuilder",Sys.UI.Control);
