Type.registerNamespace("Telerik.Sitefinity.Web.UI.CommonDialogs");
Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog=function(element){Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog.initializeBase(this,[element]);
this._element=element;
this._intialSelectedItems=[];
this._itemSelector=null;
this._doneButton=null;
this._cancelButton=null;
this._binderDataBoundDelegate=null;
this._doneClientSelectionDelegate=null;
this._cancelClientsSelectionDelegate=null;
this._onLoadDelegate=null;
this._isFirstBind=true;
};
Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog.prototype={initialize:function(){this._onLoadDelegate=Function.createDelegate(this,this._onLoadHandler);
Sys.Application.add_load(this._onLoadDelegate);
if(this._doneButton){this._doneClientSelectionDelegate=Function.createDelegate(this,this._doneClientSelection);
$addHandler(this._doneButton,"click",this._doneClientSelectionDelegate);
}if(this._cancelButton){this._cancelClientsSelectionDelegate=Function.createDelegate(this,this._cancelClientsSelection);
$addHandler(this._cancelButton,"click",this._cancelClientsSelectionDelegate);
}this._binderDataBoundDelegate=Function.createDelegate(this,this._binderDataBoundHandler);
Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog.callBaseMethod(this,"dispose");
},_doneClientSelection:function(sender,args){var items=this.get_itemSelector().get_selectedItems();
this.close(items);
},_cancelClientsSelection:function(sender,args){this.close(null);
},_binderDataBoundHandler:function(sender,args){if(this._isFirstBind){this._isFirstBind=false;
sender.selectByIds(this._intialSelectedItems,false);
}},_onLoadHandler:function(sender,args){this.get_itemSelector().get_binder().add_onDataBound(this._binderDataBoundDelegate);
this.get_itemSelector().get_binder().set_clearSelectionOnRebind(false);
},get_itemSelector:function(){return this._itemSelector;
},set_itemSelector:function(value){this._itemSelector=value;
},get_doneButton:function(){return this._doneButton;
},set_doneButton:function(value){this._doneButton=value;
},get_cancelButton:function(){return this._cancelButton;
},set_cancelButton:function(value){this._cancelButton=value;
},set_intialSelectedItems:function(value){this._intialSelectedItems=value;
if(!this._isFirstBind){this.get_itemSelector().get_binder().selectByIds(this._intialSelectedItems,false);
}}};
Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog.registerClass("Telerik.Sitefinity.Web.UI.CommonDialogs.FlatSelectorDialog",Telerik.Sitefinity.Web.UI.AjaxDialogBase);
