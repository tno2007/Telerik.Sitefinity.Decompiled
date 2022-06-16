Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");
var manageSubscribersDialog=null;
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog=function(element){this._serviceBaseUrl=null;
this._titleLabel=null;
this._doneLink=null;
this._labelManager=null;
this._subscribersSelector=null;
this._mailingListId=null;
this._subscribersSelectorBinded=false;
this._originallySelectedIds=[];
this._saveChangesDelegate=null;
this._binderDataBoundDelegate=null;
this._rowSelectedDelegate=null;
this._rowDeselectedDelegate=null;
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog.initializeBase(this,[element]);
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog.prototype={initialize:function(){manageSubscribersDialog=this;
if(this._saveChangesDelegate===null){this._saveChangesDelegate=Function.createDelegate(this,this._saveChanges);
}$addHandler(this._doneLink,"click",this._saveChangesDelegate);
if(this._binderDataBoundDelegate===null){this._binderDataBoundDelegate=Function.createDelegate(this,this._binderDataBound);
}this._subscribersSelector.add_binderDataBound(this._binderDataBoundDelegate);
if(this._rowSelectedDelegate===null){this._rowSelectedDelegate=Function.createDelegate(this,this._rowSelected);
}if(this._rowDeselectedDelegate===null){this._rowDeselectedDelegate=Function.createDelegate(this,this._rowDeselected);
}Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog.callBaseMethod(this,"initialize");
},dispose:function(){if(this._saveChangesDelegate){delete this._saveChangesDelegate;
}if(this._binderDataBoundDelegate){delete this._binderDataBoundDelegate;
}if(this._rowSelectedDelegate){delete this._rowSelectedDelegate;
}if(this._rowDeselectedDelegate){delete this._rowDeselectedDelegate;
}Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog.callBaseMethod(this,"dispose");
},loadDialog:function(listId,listTitle){this._mailingListId=listId;
this.reset();
var addExistingSubscribersText=this._labelManager.getLabel("NewslettersResources","AddExistingSubscribers");
this._titleLabel.innerHTML=String.format(addExistingSubscribersText,listTitle);
this._subscribersSelector._grid.add_rowSelected(this._rowSelectedDelegate);
this._subscribersSelector._grid.add_rowDeselected(this._rowDeselectedDelegate);
this._getSubscribersByMailingList();
dialogBase.resizeToContent();
},reset:function(){this._titleLabel.innerHTML="";
this._originallySelectedIds=[];
this._subscribersSelector._masterTable.clearSelectedItems();
},_getSubscribersByMailingList:function(){var webServiceUrl=this._serviceBaseUrl+"mailingList/"+this._mailingListId;
var clientManager=new Telerik.Sitefinity.Data.ClientManager();
var urlParams=[];
var keys=[];
clientManager.InvokeGet(webServiceUrl,urlParams,keys,this._getSubscribers_Success,this._getSubscribers_Failure,this);
},_getSubscribers_Success:function(sender,args){var items=args.Items;
for(var i=0;
i<items.length;
i++){sender._originallySelectedIds.push(items[i].Id);
}if(!sender._subscribersSelectorBinded){sender._subscribersSelectorBinded=true;
sender._subscribersSelector.dataBind();
}else{sender._subscribersSelector.selectItemsInternal(sender._originallySelectedIds);
}},_getSubscribers_Failure:function(sender,args){alert("Failure!");
},_saveChanges:function(sender,args){var webServiceUrl=this._serviceBaseUrl+"add/"+this._mailingListId;
var clientManager=new Telerik.Sitefinity.Data.ClientManager();
var urlParams=[];
var keys=[];
clientManager.InvokePost(webServiceUrl,urlParams,keys,this._originallySelectedIds,this._saveChanges_Success,this._saveChanges_Failure,this);
},_saveChanges_Success:function(sender,args){var context={ListId:sender._mailingListId};
dialogBase.close(context);
},_saveChanges_Failure:function(sender,args){alert("Failure!");
},_binderDataBound:function(sender,args){this._subscribersSelector.selectItemsInternal(this._originallySelectedIds);
},_rowSelected:function(sender,args){var e=args.get_domEvent();
var target=e.target?e.target:e.srcElement;
if(target){var subscriber=args.get_gridDataItem().get_dataItem();
if(this._originallySelectedIds.indexOf(subscriber.Id)==-1){this._originallySelectedIds.push(subscriber.Id);
}}},_rowDeselected:function(sender,args){var e=args.get_domEvent();
var target=e.target?e.target:e.srcElement;
if(target){var subscriber=args.get_gridDataItem().get_dataItem();
var idx=this._originallySelectedIds.indexOf(subscriber.Id);
if(idx>-1){this._originallySelectedIds.splice(idx,1);
}}},get_serviceBaseUrl:function(){return this._serviceBaseUrl;
},set_serviceBaseUrl:function(value){this._serviceBaseUrl=value;
},get_titleLabel:function(){return this._titleLabel;
},set_titleLabel:function(value){this._titleLabel=value;
},get_doneLink:function(){return this._doneLink;
},set_doneLink:function(value){this._doneLink=value;
},get_labelManager:function(){return this._labelManager;
},set_labelManager:function(value){this._labelManager=value;
},get_subscribersSelector:function(){return this._subscribersSelector;
},set_subscribersSelector:function(value){this._subscribersSelector=value;
}};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog.registerClass("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ManageSubscribersDialog",Telerik.Sitefinity.Web.UI.AjaxDialogBase);