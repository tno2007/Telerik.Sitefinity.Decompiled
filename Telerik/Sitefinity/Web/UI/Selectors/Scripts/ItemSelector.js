Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.ItemSelector=function(element){Telerik.Sitefinity.Web.UI.ItemSelector.initializeBase(this,[element]);
this._binderId=null;
this._selectorSearchBoxId=null;
this._numberOfSelectedItemsId=null;
this._selectedItemsFilterId=null;
this._binder=null;
this._selectorSearchBox=null;
this._selectedItemsFilter=null;
this._itemType=null;
this._itemSurrogateType=null;
this._providerName=null;
this._dataKeyNames=null;
this._returnMembers=null;
this._originallySelectedKeys=[];
this._selectedKeys=[];
this._selectedItems=[];
this._deselectedKeys=[];
this._deselectedItems=[];
this._selectedItemsCount=0;
this._selectorReadyDelegate=null;
this._selectorInitialized=false;
this._itemsFilter="";
this._showSelectedFilter=false;
this._itemSelected=null;
this._itemSelectedDelegate=null;
this._itemDeselected=null;
this._itemDeselectedDelegate=null;
this._bindOnLoad=null;
this._selectedItemsReceivedDelegate=null;
this._binderDataBoundDelegate=null;
this._errorHandlerDelegate=null;
this._mode=Telerik.Sitefinity.Web.UI.ItemSelectorMode.AllItems;
this._originalServiceBaseUrl=null;
this._originalUrlParams=[];
this._pickSelectedItemsDelegate=null;
this._constantFilter=null;
this._culture=null;
this._uiCulture=null;
};
Telerik.Sitefinity.Web.UI.ItemSelector.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.ItemSelector.callBaseMethod(this,"initialize");
if(this._selectorReadyDelegate===null){this._selectorReadyDelegate=Function.createDelegate(this,this._selectorReadyHandler);
}this._dataKeyNames=Sys.Serialization.JavaScriptSerializer.deserialize(this._dataKeyNames);
if(this.setUp==null){alert("The selector deriving from base class ItemSelector, must implement a setUp function.");
}if(this._itemSelectedDelegate==null){this._itemSelectedDelegate=Function.createDelegate(this,this._itemSelectedHandler);
}if(this._itemDeselectedDelegate==null){this._itemDeselectedDelegate=Function.createDelegate(this,this._itemDeselectedHandler);
}if(this._selectedItemsReceivedDelegate==null){this._selectedItemsReceivedDelegate=Function.createDelegate(this,this._selectedItemsReceived);
}if(this._errorHandlerDelegate==null){this._errorHandlerDelegate=Function.createDelegate(this,this._errorHandler);
}this._binderDataBoundDelegate=Function.createDelegate(this,this._binderDataBound);
this._pickSelectedItemsDelegate=Function.createDelegate(this,this._pickSelectedItems);
this._setUpDelegate=Function.createDelegate(this,this.setUp);
Sys.Application.add_load(this._setUpDelegate);
},setUp:function(){this._binder=$find(this._binderId);
this._originalBinder=this._binder;
this._originalServiceBaseUrl=this._binder.get_serviceBaseUrl();
this._originalUrlParams=this._binder.get_urlParams();
this._binder.add_onError(this._errorHandlerDelegate);
this._binder.add_onDataBound(this._binderDataBoundDelegate);
var iterator=null;
if(this._pendingBinderEvents){iterator=this._pendingBinderEvents.length;
while(iterator--){this._binder.add_onDataBound(this._pendingBinderEvents[iterator]);
}delete this._pendingBinderEvents;
}if(this._pendingBinderItemDataBoundEvents){iterator=this._pendingBinderItemDataBoundEvents.length;
while(iterator--){this._binder.add_onItemDataBound(this._pendingBinderItemDataBoundEvents[iterator]);
}delete this._pendingBinderItemDataBoundEvents;
}if(this._pendingBinderDataBindingEvents){iterator=this._pendingBinderDataBindingEvents.length;
while(iterator--){this._binder.add_onDataBinding(this._pendingBinderDataBindingEvents[iterator]);
}delete this._pendingBinderDataBindingEvents;
}this._selectorSearchBox=$find(this._selectorSearchBoxId);
if(this._showSelectedFilter){this._selectedItemsFilter=$find(this._selectedItemsFilterId);
this._selectedItemsFilterChangedDelegate=Function.createDelegate(this,this._selectedItemsFilterChanged);
this._selectedItemsFilter.add_tabSelected(this._selectedItemsFilterChangedDelegate);
}},cleanUp:function(){this._cleanUpWithoutRebind();
this._showAllItems();
},_cleanUpWithoutRebind:function(){this._originallySelectedKeys=[];
this._selectedKeys=[];
this._selectedItems=[];
this._deselectedKeys=[];
this._deselectedItems=[];
this._selectedItemsCount=0;
if(this._selectorSearchBox!=null){this._selectorSearchBox._searchBox.clearSearchBox();
}if(this._showSelectedFilter){this._selectedItemsFilter.get_tabs().getTab(0).set_selected(true);
}},dispose:function(){Telerik.Sitefinity.Web.UI.ItemSelector.callBaseMethod(this,"dispose");
if(this._selectorReadyDelegate){delete this._selectorReadyDelegate;
}if(this._selectedItemsFilterChangedDelegate){delete this._selectedItemsFilterChangedDelegate;
}if(this._selectedItemsReceivedDelegate){delete this._selectedItemsReceivedDelegate;
}if(this._errorHandlerDelegate){delete this._errorHandlerDelegate;
}if(this._itemSelectedHandler){delete this._itemSelectedHandler;
}if(this._itemDeselectedHandler){delete this._itemDeselectedHandler;
}if(this._binderDataBoundDelegate){delete this._binderDataBoundDelegate;
}if(this._pickSelectedItemsDelegate){delete this._pickSelectedItemsDelegate;
}},add_selectorReady:function(delegate){this.get_events().addHandler("onSelectorReady",delegate);
},remove_selectorReady:function(delegate){this.get_events().removeHandler("onSelectorReady",delegate);
},add_itemSelected:function(delegate){this.get_events().addHandler("itemSelected",delegate);
},remove_itemSelected:function(delegate){this.get_events().removeHandler("itemSelected",delegate);
},add_itemDeselected:function(delegate){this.get_events().addHandler("itemDeselected",delegate);
},remove_itemDeselected:function(delegate){this.get_events().removeHandler("itemDeselected",delegate);
},add_initialized:function(delegate){this.get_events().addHandler("initialized",delegate);
},remove_initialized:function(delegate){this.get_events().removeHandler("initialized",delegate);
},add_binderDataBound:function(delegate){if(this._binder!=null){this._binder.add_onDataBound(delegate);
}else{if(!this._pendingBinderEvents){this._pendingBinderEvents=[];
}this._pendingBinderEvents.push(delegate);
}},remove_binderDataBound:function(delegate){this._binder.remove_onDataBound(delegate);
},add_binderItemDataBound:function(delegate){if(this._binder!=null){this._binder.add_onItemDataBound(delegate);
}else{if(!this._pendingBinderItemDataBoundEvents){this._pendingBinderItemDataBoundEvents=[];
}this._pendingBinderItemDataBoundEvents.push(delegate);
}},remove_binderItemDataBound:function(delegate){this._binder.remove_onItemDataBound(delegate);
},add_binderDataBinding:function(delegate){if(this._binder!=null){this._binder.add_onDataBinding(delegate);
}else{if(!this._pendingBinderDataBindingEvents){this._pendingBinderDataBindingEvents=[];
}this._pendingBinderDataBindingEvents.push(delegate);
}},remove_binderDataBinding:function(delegate){this._binder.remove_onDataBinding(delegate);
},add_needsSelectedSeviceUrl:function(delegate){this.get_events().addHandler("needsSelectedSeviceUrl",delegate);
},remove_needsSelectedSeviceUrl:function(delegate){this.get_events().removeHandler("needsSelectedSeviceUrl",delegate);
},add_needsSelectedInPageServiceUrl:function(delegate){this.get_events().addHandler("needsSelectedInPageServiceUrl",delegate);
},remove_needsSelectedInPageServiceUrl:function(delegate){this.get_events().removeHandler("needsSelectedInPageServiceUrl",delegate);
},add_error:function(delegate){this.get_events().addHandler("error",delegate);
},remove_error:function(delegate){this.get_events().removeHandler("error",delegate);
},selectItem:function(key,dataItem){if(this._selectedKeys.indexOf(key)<0){this._selectedKeys.push(key);
this._selectedItems.push(dataItem);
var deselectedKeysIndex=this._deselectedKeys.indexOf(key);
if(deselectedKeysIndex>-1){this._deselectedItems.splice(deselectedKeysIndex,1);
this._deselectedKeys.splice(deselectedKeysIndex,1);
}this._updateSelectedCount(1);
this._itemSelectedHandler(dataItem);
}},deselectItem:function(key,dataItem){var itemIndex=this._selectedKeys.indexOf(key);
if(itemIndex>-1){this._selectedKeys.splice(itemIndex,1);
this._selectedItems.splice(itemIndex,1);
var originalKeysIndex=this._originallySelectedKeys.indexOf(key);
if(originalKeysIndex>-1){this._deselectedKeys.push(key);
this._deselectedItems.push(dataItem);
}this._updateSelectedCount(-1);
this._itemDeselectedHandler(dataItem);
}},bindSelector:function(){var urlParams=this._binder.get_urlParams();
urlParams.itemType=this._itemType;
if(this._itemSurrogateType!=null){urlParams.itemSurrogateType=this._itemSurrogateType;
}urlParams.allProviders=(this._providerName==""||this._providerName==null);
if(this.get_combinedFilter()){urlParams.filter=this.get_combinedFilter();
}this._binder.set_provider(this._providerName);
this._binder.DataBind();
},dataBind:function(){this.bindSelector();
},getSelectedKeys:function(){return this._selectedKeys;
},getSelectedItems:function(){return this._selectedItems;
},getAllItemsCheckbox:function(){return this._masterTable.HeaderRow.firstElementChild.firstChild;
},getDataItems:function(){return this._masterTable.get_dataItems();
},selectItemsInternal:function(originallySetKeys){alert('The "selectItemsInternal(originallySetKeys)" method must be implemented on the concrete implementation of ItemSelector.');
},_updateSelectedCount:function(addition){this._selectedItemsCount+=addition;
if(this._showSelectedFilter){$get(this._numberOfSelectedItemsId).innerHTML=this._selectedItemsCount;
}},_setSelectedCount:function(count){this._selectedItemsCount=count;
if(this._showSelectedFilter){$get(this._numberOfSelectedItemsId).innerHTML=this._selectedItemsCount;
}},_selectedItemsFilterChanged:function(sender,args){if(this._selectorSearchBox!=null){this._selectorSearchBox._searchBox.clearSearchBox();
}var tabValue=args.get_tab().get_value();
if(tabValue=="allItems"){this._mode=Telerik.Sitefinity.Web.UI.ItemSelectorMode.AllItems;
this._showAllItems();
}else{if(tabValue=="selectedItems"){this._mode=Telerik.Sitefinity.Web.UI.ItemSelectorMode.SelectedItems;
this._showSelectedItems();
}}},_showSelectedItems:function(){if(this._dataKeyNames.length>1){alert("composite data keys not yet supported.");
}var needsUrlArgs=this._needsSelectedSeviceUrlHandler();
if(needsUrlArgs==null){var dataKeyName=this._dataKeyNames[0];
var filter="";
for(var x=0;
x<this._selectedItemsCount;
x++){filter+=dataKeyName+' = "'+this._selectedKeys[x]+'" OR ';
}filter=filter.substring(0,filter.length-4);
if(this._selectedItemsCount>0){this._binder.set_filterExpression(filter);
this._binder.DataBind();
}else{this._binder.get_target().style.display="none";
}}else{this._callServiceWithNeedsServiceUrlEventArgs(needsUrlArgs,this._selectedItemsReceivedDelegate);
}},_callServiceWithNeedsServiceUrlEventArgs:function(needsUrlArgs,successDelegate){var mgr=this._binder.get_manager();
var method=needsUrlArgs.get_method().toUpperCase();
var binderUrl=needsUrlArgs.get_url();
if(binderUrl.charAt(binderUrl.length-1)!="/"){binderUrl+="/";
}var keysLen=needsUrlArgs.get_keys().length;
for(var i=0;
i<keysLen;
i++){var part=needsUrlArgs.get_keys()[i]+"";
binderUrl+=part+"/";
}this._binder.set_serviceBaseUrl(binderUrl);
mgr.set_urlParams(binderUrl);
if(method=="GET"){mgr.InvokeGet(needsUrlArgs.get_url(),needsUrlArgs.get_urlParams(),needsUrlArgs.get_keys(),successDelegate,this._errorHandlerDelegate,this,needsUrlArgs.get_context());
}else{if(method=="PUT"){mgr.InvokePut(needsUrlArgs.get_url(),needsUrlArgs.get_urlParams(),needsUrlArgs.get_keys(),needsUrlArgs.get_data(),successDelegate,this._errorHandlerDelegate,this,needsUrlArgs.get_disableValidation(),needsUrlArgs.get_validationGroup(),needsUrlArgs.get_context());
}else{if(method=="DELETE"){mgr.InvokeDelete(needsUrlArgs.get_url(),needsUrlArgs.get_urlParams(),needsUrlArgs.get_keys(),successDelegate,this._errorHandlerDelegate,this,needsUrlArgs.get_context());
}else{this._errorHandler("Cannot show selected items, because of unknown web method: "+method);
}}}},_selectedItemsReceived:function(caller,data,request){if(!data||typeof data.Items!=typeof[]){data=new Object();
data.Context=null;
data.IsGeneric=false;
data.Items=[];
data.TotalCount=0;
}if(caller._dataKeyNames.length!=1){caller._errorHandler("Only single data key name is supported.");
return;
}var dataKeyName=caller._dataKeyNames[0];
var contains=false;
var previouslySelectedIterator=caller._selectedItems.length;
var existingItemIterator=data.Items.length;
var previouslySelected=null;
while(previouslySelectedIterator--){previouslySelected=caller._selectedItems[previouslySelectedIterator];
while(existingItemIterator--){var key1=data.Items[existingItemIterator][dataKeyName];
var key2=previouslySelected[dataKeyName];
if(key1==key2){contains=true;
break;
}}if(!contains){data.Items.push(previouslySelected);
}existingItemIterator=data.Items.length;
contains=false;
}caller._binder.BindCollection(data);
existingItemIterator=data.Items.length;
var selectedKeys=[];
while(existingItemIterator--){var item=data.Items[existingItemIterator];
var key=item[dataKeyName];
selectedKeys.push(key);
caller.selectItem(key,item);
}caller.selectItemsInternal(selectedKeys);
},_showAllItems:function(){this._binder.set_serviceBaseUrl(this._originalServiceBaseUrl);
this._binder.set_urlParams(this._originalUrlParams);
if(this._binder.get_target().style){this._binder.get_target().style.display="";
}this._binder.set_filterExpression("");
this._binder.DataBind();
},_pickSelectedItems:function(caller,data,serviceContext){var iter=data.Items.length;
var dataKeyName=caller._dataKeyNames[0];
var pageItemsIter=caller._items.length;
while(iter--){var item=null;
var key=data.Items[iter];
while(pageItemsIter--){var key2=caller._items[pageItemsIter][dataKeyName];
if(key==key2){item=caller._items[pageItemsIter];
break;
}}pageItemsIter=caller._items.length;
caller.selectItem(key,item);
}caller.selectItemsInternal(data.Items);
caller._originallySelectedKeys=data.Items;
caller._setSelectedCount(data.TotalCount);
delete caller._items;
},_binderDataBound:function(sender,args){var items=args.get_dataItem().Items;
if(this._mode==Telerik.Sitefinity.Web.UI.ItemSelectorMode.AllItems){var args=this._needsSelectedInPageServiceUrlHandler();
if(args!=null){var iter=items.length;
var dataKeyName=this._dataKeyNames[0];
var allKeys=[];
while(iter--){allKeys.push(items[iter][dataKeyName]);
}this._items=items;
if(!args.get_data()){args.set_data(allKeys);
}this._callServiceWithNeedsServiceUrlEventArgs(args,this._pickSelectedItemsDelegate);
this._binder.set_serviceBaseUrl(this._originalServiceBaseUrl);
this._binder.set_urlParams(this._originalUrlParams);
}this.selectItemsInternal(this._originallySelectedKeys);
}else{var iter=items.length;
var dataKeyName=this._dataKeyNames[0];
var allKeys=[];
while(iter--){var item=items[iter];
var key=item[dataKeyName];
allKeys.push(key);
this.selectItem(key,item);
}this.selectItemsInternal(allKeys);
this._setSelectedCount(args.get_dataItem().TotalCount);
this._originallySelectedKeys=allKeys;
}},errorArgs:function(message){this.get_error=function(){return message;
};
},_selectorReadyHandler:function(){if(this._selectorInitialized==false){this._selectorInitialized=true;
var h=this.get_events().getHandler("onSelectorReady");
if(h){h(this,Sys.EventArgs.Empty);
}}},_itemSelectedHandler:function(dataItem,selectedElement){var h=this.get_events().getHandler("itemSelected");
if(h){h(this,dataItem,selectedElement);
}},_itemDeselectedHandler:function(dataItem,selectedElement){var h=this.get_events().getHandler("itemDeselected");
if(h){h(this,dataItem,selectedElement);
}},_initializedHandler:function(){var handler=this.get_events().getHandler("initialized");
if(handler){handler(this,Sys.EventArgs.Empty);
}},_needsSelectedSeviceUrlHandler:function(){var handler=this.get_events().getHandler("needsSelectedSeviceUrl");
if(handler){var args=new Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs();
handler(this,args);
return args;
}else{return null;
}},_needsSelectedInPageServiceUrlHandler:function(){var handler=this.get_events().getHandler("needsSelectedInPageServiceUrl");
if(handler){var args=new Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs();
handler(this,args);
return args;
}else{return null;
}},_errorHandler:function(message){var msg=message?message:"";
msg=msg.Detail?msg.Detail:msg;
var handler=this.get_events().getHandler("error");
if(handler){var args=new this.errorArgs(msg);
handler(this,args);
}else{alert(message);
}},get_selectedKeys:function(){return this._selectedKeys;
},set_selectedKeys:function(value){if(this._selectedKeys!=value){this._originallySelectedKeys=value;
this.raisePropertyChanged("selectedKeys");
}},get_selectedItems:function(){return this._selectedItems;
},get_deselectedItems:function(){return this._deselectedItems;
},get_deselectedKeys:function(){return this._deselectedKeys;
},get_providerName:function(){return this._providerName;
},set_providerName:function(value){if(this._providerName!=value){this._providerName=value;
this.raisePropertyChanged("providerName");
}},get_dataKeyNames:function(){return this._dataKeyNames;
},get_itemType:function(){return this._itemType;
},set_itemType:function(value){if(this._itemType!=value){this._itemType=value;
this.raisePropertyChanged("itemType");
}},get_itemsFilter:function(){return this._itemsFilter;
},set_itemsFilter:function(value){if(this._itemsFilter!=value){this._itemsFilter=value;
this.raisePropertyChanged("itemsFilter");
}},get_bindOnLoad:function(){return(String(this._bindOnLoad).toUpperCase()=="TRUE");
},set_bindOnLoad:function(value){if(this._bindOnLoad!=value){this._bindOnLoad=(String(value).toUpperCase()=="TRUE");
this.raisePropertyChanged("bindOnLoad");
}},get_constantFilter:function(){return this._constantFilter;
},set_constantFilter:function(val){if(this._constantFilter!=val){this._constantFilter=val;
this.raisePropertyChanged("constantFilter");
}},get_combinedFilter:function(){var filter="";
if(this._itemsFilter&&this._constantFilter&&this._itemsFilter!=this._constantFilter){filter="("+this._itemsFilter+") and ("+this._constantFilter+")";
}else{if(this._itemsFilter){filter=this._itemsFilter;
}else{if(this._constantFilter){filter=this._constantFilter;
}}}return filter;
},get_binder:function(){return this._binder;
},_setCultures:function(culture){this._uiCulture=culture;
this._culture=culture;
if(this._binder!=null){this._binder.set_culture(culture);
this._binder.set_uiCulture(culture);
this._binder.DataBind();
}},set_uiCulture:function(culture){this._uiCulture=culture;
if(this._binder!=null){this._binder.set_uiCulture(culture);
this._binder.DataBind();
}},get_uiCulture:function(){return this._uiCulture;
},set_culture:function(culture){this._culture=culture;
if(this._binder!=null){this._binder.set_culture(culture);
}},get_culture:function(){return this._culture;
}};
Telerik.Sitefinity.Web.UI.ItemSelector.registerClass("Telerik.Sitefinity.Web.UI.ItemSelector",Sys.UI.Control);
Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs=function(){this._method="GET";
this._url="";
this._urlParams=[];
this._keys=[];
this._data=null;
this._disableValidation=false;
this._validationGroup=null;
this._context=null;
Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs.initializeBase(this);
};
Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs.prototype={get_method:function(){return this._method;
},set_method:function(val){this._method=val;
},get_url:function(){return this._url;
},set_url:function(val){this._url=val;
},get_urlParams:function(){return this._getUrlParams;
},set_urlParams:function(val){this._getUrlParams=val;
},get_keys:function(){return this._keys;
},set_keys:function(val){this._keys=val;
},get_data:function(){return this._data;
},set_data:function(val){this._data=val;
},get_disableValidation:function(){return this._disableValidation;
},set_disableValidation:function(val){this._disableValidation=val;
},get_validationGroup:function(){return this._validationGroup;
},set_validationGroup:function(val){this._validationGrip=val;
},get_context:function(){return this._context;
},set_context:function(val){this._context=val;
}};
Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs.registerClass("Telerik.Sitefinity.Web.UI.NeedsServiceUrlEventArgs",Sys.EventArgs);
Telerik.Sitefinity.Web.UI.ItemSelectorMode=function(){};
Telerik.Sitefinity.Web.UI.ItemSelectorMode.prototype={AllItems:0,SelectedItems:1};
Telerik.Sitefinity.Web.UI.ItemSelectorMode.registerEnum("Telerik.Sitefinity.Web.UI.ItemSelectorMode");
