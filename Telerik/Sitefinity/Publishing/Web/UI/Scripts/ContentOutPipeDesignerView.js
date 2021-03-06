Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");
Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView=function(element){Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView.initializeBase(this,[element]);
this._resources=null;
this._parentContentSelector=null;
this._buttonParents=null;
this._selectedParentContentTitle=null;
this._itemsParentId=null;
this._contentTypeName=null;
this._contentNotSelectedMessage="";
this._controlData=null;
this._selectParentContentDelegate=null;
this._showParentDelegate=null;
this._showSelectedContentSuccessDelegate=null;
};
Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView.prototype={initialize:function(){Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView.callBaseMethod(this,"initialize");
this._selectParentContentDelegate=Function.createDelegate(this,this._selectParentContent);
this._parentContentSelector.add_doneClientSelection(this._selectParentContentDelegate);
this._showParentDelegate=Function.createDelegate(this,this._showParentSelector);
$addHandler($get(this._buttonParents),"click",this._showParentDelegate);
this._showSelectedContentSuccessDelegate=Function.createDelegate(this,this._showSelectedContentSuccess);
},dispose:function(){Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView.callBaseMethod(this,"dispose");
if(this._selectParentContentDelegate){this._parentContentSelector.remove_doneClientSelection(this._selectParentContentDelegate);
delete this._selectParentContentDelegate;
}if(this._showParentDelegate){$removeHandler($get(this._buttonParents),"click",this._showParentDelegate);
delete this._showParentDelegate;
}if(this._showSelectedContentSuccessDelegate){delete this._showSelectedContentSuccessDelegate;
}},_selectParentContent:function(items){jQuery(this.get_element()).find("#parentSelectorTag").hide();
dialogBase.resizeToContent();
if(items==null){return;
}var selectedItems=this.get_parentContentSelector().getSelectedItems();
var title=null;
if(selectedItems!=null){if(selectedItems.length>0){this._itemsParentId=items[0];
title=selectedItems[0].Title;
}}this._selectedParentContentTitle.innerHTML=title;
jQuery(this._selectedParentContentTitle).show();
},_showParentSelector:function(){this._parentContentSelector.dataBind();
jQuery(this.get_element()).find("#parentSelectorTag").show();
dialogBase.resizeToContent();
},validate:function(){var isValid=true;
if(!this._itemsParentId||this._itemsParentId=="00000000-0000-0000-0000-000000000000"){alert(this._contentNotSelectedMessage);
isValid=false;
}return isValid;
},refreshUI:function(){this._refreshing=true;
var data=this.get_controlData();
if(data.settings.ImportedItemParentId){this._itemsParentId=data.settings.ImportedItemParentId;
this._showSelectedContent();
}this._refreshing=false;
},applyChanges:function(){var data=this.get_controlData();
if(this._itemsParentId){data.settings.ImportedItemParentId=this._itemsParentId;
}},_showSelectedContent:function(){var selector=this._parentContentSelector.get_itemSelector();
var binder=selector._binder;
var clientManager=binder.get_manager();
var urlParams={};
var settingsData=this.get_controlData();
urlParams.filter=String.format("Id = ({0})",settingsData.settings.ImportedItemParentId);
urlParams.itemType=settingsData.settings.ContentTypeName;
var keys={};
clientManager.InvokeGet(binder.get_serviceBaseUrl(),urlParams,keys,this._showSelectedContentSuccessDelegate,this._showSelectedContentFailure,this);
},_showSelectedContentSuccess:function(sender,result){if(result){var items=result.Items;
if(items&&items.length>0){sender.get_selectedParentContentTitle().innerHTML=items[0].Title;
}}},_showSelectedContentFailure:function(sender,result){},get_uiDescription:function(){var typeName=this._contentTypeName;
if(this._itemsParentId){typeName=typeName+": "+this.get_selectedParentContentTitle().innerHTML;
}var typePrefix=String.format("<strong>{0}</strong>",typeName);
return typePrefix;
},get_parentContentSelector:function(){return this._parentContentSelector;
},set_parentContentSelector:function(value){this._parentContentSelector=value;
},get_selectedParentContentTitle:function(){return this._selectedParentContentTitle;
},set_selectedParentContentTitle:function(value){this._selectedParentContentTitle=value;
},get_resources:function(){return this._resources;
},set_resources:function(value){this._resources=value;
},get_buttonParents:function(){return this._buttonParents;
},set_buttonParents:function(value){this._buttonParents=value;
},get_contentTypeName:function(){return this._contentTypeName;
},set_contentTypeName:function(value){this._contentTypeName=value;
},get_controlData:function(){return this._controlData;
},set_controlData:function(value){this._controlData=value;
}};
Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView.registerClass("Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentOutPipeDesignerView",Sys.UI.Control);
