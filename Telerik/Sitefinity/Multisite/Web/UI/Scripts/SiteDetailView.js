Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");
var siteDetailView=null;
Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView=function(element){Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.initializeBase(this,[element]);
this._propertiesView=null;
this._configureModulesView=null;
this._backLinkUrl=null;
this._isInStandaloneMode=false;
this._isSingleSiteMode=false;
this._isIntegratedApp=false;
this._kendoLoadedHandler=null;
};
Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.prototype={initialize:function(){Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.callBaseMethod(this,"initialize");
siteDetailView=this;
},dispose:function(){Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.callBaseMethod(this,"dispose");
},reset:function(){this.get_propertiesView().hide();
this.get_configureModulesView().hide();
},show:function(isCreateMode,siteId,configureModules,propertiesView){this.reset();
if(configureModules){this.get_configureModulesView().set_propertiesView(propertiesView);
this.get_configureModulesView().show(siteId);
if(this.get_isInStandaloneMode()){this.get_configureModulesView().arrangeUIForIntegrationMode();
}}else{this._isIntegratedApp=jQuery("html").hasClass("-sf-integrated-app")||this._isIntegratedApp;
if(this._isIntegratedApp){jQuery("html").removeClass("-sf-integrated-app");
}this.get_propertiesView().set_siteDetailView(this);
this.get_propertiesView().set_isSingleSiteMode(this.get_isSingleSiteMode());
this.get_propertiesView().show(isCreateMode,siteId);
if(this.get_isInStandaloneMode()){this.get_propertiesView().arrangeUIForIntegrationMode();
}}Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.callBaseMethod(this,"show");
if(!this.get_isInStandaloneMode()){this.get_kendoWindow().element.parent().addClass("sfMaximizedWindow");
this.get_kendoWindow().maximize();
}else{this.get_kendoWindow().element.parent().addClass("sfWindowAsPartOfPage");
}jQuery("html").addClass("sfLegacyDialogOpened");
},close:function(data){if(this.get_backLinkUrl()){window.location.href=this.get_backLinkUrl();
}else{if(this._isIntegratedApp){jQuery("html").addClass("-sf-integrated-app");
this._isIntegratedApp=false;
}jQuery("html").removeClass("sfLegacyDialogOpened");
Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.callBaseMethod(this,"close");
}},get_propertiesView:function(){return this._propertiesView;
},set_propertiesView:function(value){this._propertiesView=value;
},get_configureModulesView:function(){return this._configureModulesView;
},set_configureModulesView:function(value){this._configureModulesView=value;
},get_backLinkUrl:function(){return this._backLinkUrl;
},set_backLinkUrl:function(value){this._backLinkUrl=value;
},get_isInStandaloneMode:function(){return this._isInStandaloneMode;
},set_isInStandaloneMode:function(value){this._isInStandaloneMode=value;
},get_isSingleSiteMode:function(){return this._isSingleSiteMode;
},set_isSingleSiteMode:function(value){this._isSingleSiteMode=value;
},set_kendoLoadedHandler:function(value){this._kendoLoadedHandler=value;
},_documentReadyHandler:function(){var jOuterDiv=jQuery(this.get_outerDiv());
this._kendoWindow=jOuterDiv.kendoWindow({width:this.get_width()+"px",height:((this.get_height()&&this.get_height()>0)?this.get_height()+"px":"auto"),resizable:this.get_isResizable(),modal:this.get_isModal(),animation:false,autoFocus:false}).data("kendoWindow");
jOuterDiv.show();
if(this._kendoLoadedHandler){this._kendoLoadedHandler();
}},};
Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView.registerClass("Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView",Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);
