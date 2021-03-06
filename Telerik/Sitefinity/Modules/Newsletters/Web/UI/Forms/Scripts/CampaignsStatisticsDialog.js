Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms");
var campaignsStatisticsDialog=null;
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog=function(element){Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog.initializeBase(this,[element]);
this._campaignStatsTitle=null;
this._statsGrid=null;
this._statsBinder=null;
this._labelManager=null;
this._statsBinderDataBoundDelegate=null;
this._currentlyLoadedCampaign=null;
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog.prototype={initialize:function(){Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog.callBaseMethod(this,"initialize");
campaignsStatisticsDialog=this;
if(this._statsBinderDataBoundDelegate==null){this._statsBinderDataBoundDelegate=Function.createDelegate(this,this._statsBinderDataBound);
}if(this._statsBinder!=null){this._statsBinder.add_onDataBound(this._statsBinderDataBoundDelegate);
}this._onLoadDelegate=Function.createDelegate(this,this._onLoad);
Sys.Application.add_load(this._onLoadDelegate);
},dispose:function(){Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog.callBaseMethod(this,"dispose");
if(this._statsBinderDataBoundDelegate){if(this._statsBinder!=null){this._statsBinder.remove_onDataBound(this._statsBinderDataBoundDelegate);
}delete this._statsBinderDataBoundDelegate;
}if(this._onLoadDelegate){Sys.Application.remove_load(this._onLoadDelegate);
delete this._onLoadDelegate;
}},_onLoad:function(){jQuery("body").addClass("sfSelectorDialog");
dialogBase.resizeToContent();
},BindCampaignsByMailingListId:function(listId){if(this._currentlyLoadedCampaign!=listId){var urlParams={filter:'List.Id == "'+listId+'" AND CampaignState != ABTest'};
this._statsBinder.set_urlParams(urlParams);
this._statsBinder.DataBind();
this._currentlyLoadedCampaign=listId;
}},_statsBinderDataBound:function(sender,args){var titleMessageTemplate=this._labelManager.getLabel("NewslettersResources","NumberOfUsingCampaigns");
var titleMessageForOneItem=this._labelManager.getLabel("NewslettersResources","OneUsingCampaigns");
var totalCampaigns=args.get_dataItem().TotalCount;
if(totalCampaigns==1){this._campaignStatsTitle.innerHTML=titleMessageForOneItem;
}else{this._campaignStatsTitle.innerHTML=String.format(titleMessageTemplate,totalCampaigns);
}dialogBase.resizeToContent();
},get_campaignStatsTitle:function(){return this._campaignStatsTitle;
},set_campaignStatsTitle:function(value){this._campaignStatsTitle=value;
},get_statsGrid:function(){return this._statsGrid;
},set_statsGrid:function(value){this._statsGrid=value;
},get_statsBinder:function(){return this._statsBinder;
},set_statsBinder:function(value){this._statsBinder=value;
},get_labelManager:function(){return this._labelManager;
},set_labelManager:function(value){this._labelManager=value;
}};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog.registerClass("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.CampaignsStatisticsDialog",Telerik.Sitefinity.Web.UI.AjaxDialogBase);
