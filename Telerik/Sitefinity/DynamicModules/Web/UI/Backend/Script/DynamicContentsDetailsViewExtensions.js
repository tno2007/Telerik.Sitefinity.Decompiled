function OnDetailViewLoaded(sender,args){var currentForm=sender;
Sys.Application.add_init(function(){$create(Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension,{_detailFormView:currentForm},{},{},null);
});
}Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script");
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension=function(masterView){Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension.initializeBase(this);
this._detailFormView={};
this._binder=null;
this._commandDelegate=null;
};
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension.prototype={initialize:function(){Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension.callBaseMethod(this,"initialize");
this._commandDelegate=Function.createDelegate(this,this._commandHandler);
this._detailFormView.add_onDataBind(this._commandDelegate);
},dispose:function(){Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension.callBaseMethod(this,"dispose");
if(this._commandDelegate){this._detailFormView.remove_command(this._commandDelegate);
delete this._commandDelegate;
}},_commandHandler:function(sender,args){var dataItem=args;
this._detailFormView.get_topWorkflowMenu().set_preventDeleteParentItem(dataItem.Item.HasChildren);
this._detailFormView.get_bottomWorkflowMenu().set_preventDeleteParentItem(dataItem.Item.HasChildren);
}};
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension.registerClass("Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DetailFormViewExtension",Sys.Component);
