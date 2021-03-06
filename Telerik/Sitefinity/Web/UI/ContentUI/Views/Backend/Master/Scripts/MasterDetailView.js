Type.registerNamespace("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend");
dialogBase=null;
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView=function(element){Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView.initializeBase(this,[element]);
this._detailView=null;
this._masterView=null;
this._currentMasterSelectedItem=null;
this._masterViewSelectionChangedDelegate=null;
this._handlePageLoadDelegate=null;
};
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView.prototype={initialize:function(){this._handlePageLoadDelegate=Function.createDelegate(this,this._pageLoad);
this._masterViewSelectionChangedDelegate=Function.createDelegate(this,this._masterViewSelectionChanged);
Sys.Application.add_load(this._handlePageLoadDelegate);
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView.callBaseMethod(this,"initialize");
},dispose:function(){if(this._masterView&&this._masterView.get_currentItemsList()){this._masterView.get_currentItemsList().remove_selectionChanged(this._masterViewSelectionChangedDelegate);
}delete this._masterViewSelectionChangedDelegate;
delete this._handleViewLoadedDelegate;
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView.callBaseMethod(this,"dispose");
},add_masterViewSelectionChanged:function(delegate){this.get_events().addHandler("masterViewSelectionChanged",delegate);
},remove_masterViewSelectionChanged:function(delegate){this.get_events().removeHandler("masterViewSelectionChanged",delegate);
},_raiseMasterViewSelectionChanged:function(selectedItems){var eventArgs=selectedItems;
var handler=this.get_events().getHandler("masterViewSelectionChanged");
if(handler){handler(this,eventArgs);
}return eventArgs;
},_pageLoad:function(sender,args){this._masterView.add_selectionChanged(this._masterViewSelectionChangedDelegate);
},_masterViewSelectionChanged:function(sender,eventArgs){var selectedItems=this._masterView.get_selectedItems();
this._raiseMasterViewSelectionChanged(selectedItems);
var selectedItemsCount=selectedItems.length;
if(selectedItemsCount>0){if(this._currentMasterSelectedItem!=selectedItems[0]){this._detailView.reset();
this._detailView.dataBind(selectedItems[0],[selectedItems[0].Id]);
this._currentMasterSelectedItem=selectedItems[0];
}}},get_detailView:function(){return this._detailView;
},set_detailView:function(value){if(value!==this._detailView){this._detailView=value;
}},get_masterView:function(){return this._masterView;
},set_masterView:function(value){if(value!==this._masterView){this._masterView=value;
}}};
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.MasterDetailView.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView",Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase);
