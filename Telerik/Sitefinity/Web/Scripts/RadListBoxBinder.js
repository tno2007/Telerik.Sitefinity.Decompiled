Telerik.Sitefinity.Web.UI.RadListBoxBinder=function(){this._itemsBoundCount=null;
this._itemsToBindCount=null;
this._virtualCount;
this._batchSave;
Telerik.Sitefinity.Web.UI.RadListBoxBinder.initializeBase(this);
};
Telerik.Sitefinity.Web.UI.RadListBoxBinder.prototype={initialize:function(){this._serviceCallback=this.DataBind;
Telerik.Sitefinity.Web.UI.RadListBoxBinder.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.RadListBoxBinder.callBaseMethod(this,"dispose");
},DataBind:function(){Telerik.Sitefinity.Web.UI.RadListBoxBinder.callBaseMethod(this,"DataBind");
var clientManager=this.get_manager();
clientManager.set_urlParams(this.get_urlParams());
clientManager.GetItemCollection(this);
},SaveChanges:function(){if($(document.forms[0]).validate().form()==false){return false;
}var list=$find(this._targetId);
var dataItems=list.get_Items();
var clientManager=this.get_manager();
clientManager.set_urlParams(this.get_urlParams());
this._savingHandler(propertyBag);
var properties=[];
var batchKeys;
for(dataItemIndex=0,dataItemsLen=masterTableview.get_virtualItemCount();
dataItemIndex<dataItemsLen;
dataItemIndex++){var dataItem=dataItems[dataItemIndex]._dataItem;
if(dataItem!=null){if($(document.forms[0]).validate().form()==false){return false;
}var key=this.GetItemKey(dataItemIndex);
var propertyBag=this.GetData(dataItems[dataItemIndex]._element,dataItemIndex);
var args=this._itemSavingHandler(key,dataItem,dataItemIndex,dataItems[dataItemIndex].get_element(),propertyBag);
if(args.get_cancel()==false){if(this._batchSave){if(dataItemIndex==0){batchKeys=key;
}properties.push(args.get_propertyBag()[0]);
properties.push(args.get_propertyBag()[1]);
}else{clientManager.SaveItem(this,key,args.get_propertyBag(),null);
}}}}if(this._batchSave){clientManager.SaveItems(this,batchKeys,properties,null);
}return true;
},SaveItem:function(dataItemIndex){if($(document.forms[0]).validate().form()==false){return false;
}var list=$find(this._targetId);
var dataItems=list.get_Items();
var clientManager=this.get_manager();
clientManager.set_urlParams(this.get_urlParams());
var dataItem=dataItems[dataItemIndex]._dataItem;
if(dataItem!=null){if($(document.forms[0]).validate().form()==false){return false;
}var key=this.GetItemKey(dataItemIndex);
var propertyBag=this.GetData(dataItems[dataItemIndex]._element,dataItemIndex);
this._savingHandler(propertyBag);
var args=this._itemSavingHandler(key,dataItem,dataItemIndex,dataItems[dataItemIndex].get_element(),propertyBag);
if(args.get_cancel()==false){clientManager.SaveItem(this,key,args.get_propertyBag(),null);
}}return true;
},BindCollection:function(data){this._dataBindingHandler(data);
var list=$find(this._targetId);
list.get_items().clear();
var container=$("<div/>")[0];
var template=new Sys.UI.Template($get(this._clientTemplates[0]));
for(i=0;
i<data.TotalCount;
i++){var dataItem=data.Items[i];
var listItem=new Telerik.Web.UI.RadListBoxItem();
if(data.IsGeneric){listItem.set_text(dataItem);
}else{container.innerHTML="";
template.instantiateIn(container,null,dataItem);
$($(container).find("div").children()).each(function(i,element){if(i==0){listItem.set_text($(container).find(element.tagName).html());
}});
}if(dataItem[this.get_dataKeyNames()]!=null){listItem.set_value(dataItem[this.get_dataKeyNames()]);
}list.get_items().add(listItem);
}this._virtualCount=data.TotalCount;
this._dataBoundHandler();
},BindItem:function(data){alert("BindItem function is not supported by RadListBoxBinder. Please use Bind instead.");
},get_itemCount:function(){return this._virtualCount;
},set_batchSave:function(value){this._batchSave=value;
}};
Telerik.Sitefinity.Web.UI.RadListBoxBinder.registerClass("Telerik.Sitefinity.Web.UI.RadListBoxBinder",Telerik.Sitefinity.Web.UI.ClientBinder);