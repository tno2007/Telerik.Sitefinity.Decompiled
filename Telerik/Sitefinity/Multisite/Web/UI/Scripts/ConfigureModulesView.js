Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");
Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView=function(element){Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView.initializeBase(this,[element]);
this._backLink=null;
this._dialogTitle=null;
this._clientLabelManager=null;
this._siteConfiguration=null;
this._webServiceUrl=null;
this._messageControl=null;
this._loadingCounter=null;
this._loadingView=null;
this._buttonsPanel=null;
this._cancelLink=null;
this._saveButton=null;
this._configureModulesViewWrapper=null;
this._dataSourcesTableBody=null;
this._dataSourceProviderSelectorDialog=null;
this._dataSourceProviderSelectorDialogCloseDelegate=null;
this._isCreateMode=null;
this._propertiesView=null;
this._allDataSourcesCheckbox=null;
this._allDataSourcesCheckboxClickDelegate=null;
this._ajaxFailDelegate=null;
this._ajaxCompletedDelegate=null;
this._saveDelegate=null;
this._backDelegate=null;
this._cancelDelegate=null;
this._startMonitoringSiteInitializationProgressDelegate=null;
this._monitorSiteInitializationProgressDelegate=null;
this._checkSiteInitializationStatusCallbackDelegate=null;
this._createButton=null;
this._createButtonClickDelegate=null;
this._checkProgress=null;
this._manuallyConfiguredModeContainer=null;
this._configureByDeploymentBtn=null;
this._configuredByDeploymentModeContainer=null;
this._configureManuallyBtn=null;
this.userGroupsSectionElement=null;
this.userSourcesGridElement=null;
this.usersDataSource=null;
this.usersDataSourceName="Telerik.Sitefinity.Security.UserManager";
};
Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView.prototype={initialize:function(){Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView.callBaseMethod(this,"initialize");
this._ajaxCompleteDelegate=Function.createDelegate(this,this._ajaxCompleteHandler);
this._ajaxFailDelegate=Function.createDelegate(this,this._ajaxFailHandler);
this._startMonitoringSiteInitializationProgressDelegate=Function.createDelegate(this,this._startMonitoringSiteInitializationProgress);
this._monitorSiteInitializationProgressDelegate=Function.createDelegate(this,this._monitorSiteInitializationProgress);
this._checkSiteInitializationStatusCallbackDelegate=Function.createDelegate(this,this._checkSiteInitializationStatusCallback);
this._saveDelegate=Function.createDelegate(this,this._save);
$addHandler(this.get_saveButton(),"click",this._saveDelegate);
this._dataSourceProviderSelectorDialogCloseDelegate=Function.createDelegate(this,this._dataSourceProviderSelectorDialogCloseHandler);
this.get_dataSourceProviderSelectorDialog().add_close(this._dataSourceProviderSelectorDialogCloseDelegate);
this._backDelegate=Function.createDelegate(this,this._back);
$addHandler(this.get_backLink(),"click",this._backDelegate);
this._allDataSourcesCheckboxClickDelegate=Function.createDelegate(this,this._allDataSourcesCheckboxClickHandler);
$addHandler(this.get_allDataSourcesCheckbox(),"click",this._allDataSourcesCheckboxClickDelegate);
this._createButtonClickDelegate=Function.createDelegate(this,this._createButtonClickHandler);
$addHandler(this.get_createButton(),"click",this._createButtonClickDelegate);
if(this.get_configureManuallyBtn()){this._configureManuallyClickDelegate=Function.createDelegate(this,this._configureManuallyClickHandler);
$addHandler(this.get_configureManuallyBtn(),"click",this._configureManuallyClickDelegate);
}if(this.get_configureByDeploymentBtn()){this._configureByDeploymentClickDelegate=Function.createDelegate(this,this._configureByDeploymentClickHandler);
$addHandler(this.get_configureByDeploymentBtn(),"click",this._configureByDeploymentClickDelegate);
}this._cancelDelegate=Function.createDelegate(this,this.cancel);
$addHandler(this.get_cancelLink(),"click",this._cancelDelegate);
this.initializeUserGroups();
},dispose:function(){if(this._ajaxCompleteDelegate){delete this._ajaxCompleteDelegate;
}if(this._ajaxFailDelegate){delete this._ajaxFailDelegate;
}if(this._startMonitoringSiteInitializationProgressDelegate){delete this._startMonitoringSiteInitializationProgressDelegate;
}if(this._checkSiteInitializationStatusCallbackDelegate){delete this._checkSiteInitializationStatusCallbackDelegate;
}if(this._monitorSiteInitializationProgressDelegate){delete this._monitorSiteInitializationProgressDelegate;
}if(this._saveDelegate){if(this.get_saveButton()){$removeHandler(this.get_saveButton(),"click",this._saveDelegate);
}delete this._saveDelegate;
}if(this._dataSourceProviderSelectorDialogCloseDelegate){if(this.get_dataSourceProviderSelectorDialog()){this.get_dataSourceProviderSelectorDialog().remove_close(this._dataSourceProviderSelectorDialogCloseDelegate);
}delete this._dataSourceProviderSelectorDialogCloseDelegate;
}if(this._backDelegate){if(this.get_backLink()){$removeHandler(this.get_backLink(),"click",this._backDelegate);
}delete this._backDelegate;
}if(this._cancelDelegate){if(this.get_cancelLink()){$removeHandler(this.get_cancelLink(),"click",this._cancelDelegate);
}delete this._cancelDelegate;
}if(this._allModulesCheckboxClickDelegate){if(this.get_allModulesCheckbox()){$removeHandler(this.get_allModulesCheckbox(),"click",this._allModulesCheckboxClickDelegate);
}delete this._allModulesCheckboxClickDelegate;
}if(this._createButtonClickDelegate){if(this.get_createButton()){$removeHandler(this.get_createButton(),"click",this._createButtonClickDelegate);
}delete this._createButtonClickDelegate;
}if(this._configureManuallyClickDelegate){delete this._configureManuallyClickDelegate;
}if(this._configureByDeploymentClickDelegate){delete this._configureByDeploymentClickDelegate;
}Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView.callBaseMethod(this,"dispose");
},show:function(siteId,siteConfiguration){jQuery(this._element).closest(".sfFormDialog").addClass("sfLoadingTransition");
this.reset();
jQuery(this.get_configureModulesViewWrapper()).show();
this._isCreateMode=(siteConfiguration)?true:false;
this._siteConfiguration=siteConfiguration;
if(this._siteConfiguration){var that=this;
var getSourcesSuccess=function(data){var dataSources=data.filter(function(e){return e.Name!=that.usersDataSourceName;
});
that.usersDataSource=data.find(function(e){return e.Name==that.usersDataSourceName;
});
if(that._siteConfiguration.DataSources&&that._siteConfiguration.DataSources.length===dataSources.length){that._updateSamples(dataSources);
}else{that._siteConfiguration.DataSources=dataSources;
}that._updateUi();
};
this._getDataSources(getSourcesSuccess);
}else{var id=this._siteConfiguration?this._siteConfiguration.Id:siteId;
this._rebind(id);
}},hide:function(){jQuery(this.get_configureModulesViewWrapper()).hide();
},close:function(sender,args){if(this._isCreateMode||this.get_propertiesView()){this.hide();
siteDetailView.get_propertiesView().showWrapper();
return;
}siteDetailView.close();
},cancel:function(){siteDetailView.close();
},reset:function(){this._siteConfiguration=null;
jQuery(this.get_dataSourcesTableBody()).html("");
this._loadingCounter=0;
this._setLoadingViewVisible(false);
this.get_messageControl().hide();
},arrangeUIForIntegrationMode:function(){jQuery(this.get_cancelLink()).hide();
jQuery(this.get_backLink()).hide();
jQuery(".k-window-actions").hide();
},_updateSamples:function(dataSources){for(var i=0;
i<dataSources.length;
i++){for(var j=0;
j<this._siteConfiguration.DataSources[i].Links.length;
j++){if(this._siteConfiguration.DataSources[i].Links[j].ProviderName===this._siteConfiguration.DataSources[i].SampleLink.ProviderName){this._siteConfiguration.DataSources[i].Links[j].ProviderName=dataSources[i].SampleLink.ProviderName;
this._siteConfiguration.DataSources[i].Links[j].ProviderTitle=dataSources[i].SampleLink.ProviderTitle;
}}this._siteConfiguration.DataSources[i].SampleLink=dataSources[i].SampleLink;
}},_rebind:function(siteId){var that=this;
if(!this._siteConfiguration){var getSiteConfigSuccess=function(data){that.usersDataSource=data.DataSources.find(function(e){return e.Name==that.usersDataSourceName;
});
data.DataSources=data.DataSources.filter(function(e){return e.Name!=that.usersDataSourceName;
});
that._siteConfiguration=data;
that._updateUi();
};
this._getSiteConfiguration(siteId,getSiteConfigSuccess);
}},_updateUi:function(){var labelKey;
if(this.usersDataSource){$(this.userSourcesGridElement).data("kendoGrid").setDataSource(new kendo.data.DataSource({data:this.usersDataSource.Links,sort:{field:"ProviderTitle",dir:"asc"}}));
labelKey="ConfigureModulesAndAccessFor";
this.userGroupsSectionElement.show();
}else{labelKey="ConfigureModulesFor";
this.userGroupsSectionElement.hide();
}if(this._isCreateMode){jQuery(this.get_backLink()).html(this.get_clientLabelManager().getLabel("MultisiteResources","BackToCreateSite"));
jQuery(this.get_createButton()).show();
jQuery(this.get_saveButton()).hide();
}else{if(this.get_propertiesView()){jQuery(this.get_backLink()).html(this.get_clientLabelManager().getLabel("MultisiteResources","BackToSiteProperties"));
}else{jQuery(this.get_backLink()).html(this.get_clientLabelManager().getLabel("MultisiteResources","BackToSites"));
}jQuery(this.get_createButton()).hide();
jQuery(this.get_saveButton()).show();
}jQuery(this.get_dialogTitle()).html(this.get_clientLabelManager().getLabel("MultisiteResources",labelKey)+" "+this._siteConfiguration.Name.htmlEncode());
var jModulesTableBody=jQuery(this.get_dataSourcesTableBody());
jModulesTableBody.html("");
for(var i=0;
i<this._siteConfiguration.DataSources.length;
i++){if(this._siteConfiguration.DataSources[i].Links){this._siteConfiguration.DataSources[i].Links.sort(function(a,b){var aVal=a.ProviderTitle?a.ProviderTitle:a.ProviderName;
var bVal=b.ProviderTitle?b.ProviderTitle:b.ProviderName;
if(aVal>bVal){return 1;
}else{if(aVal<bVal){return -1;
}else{return 0;
}}});
}jModulesTableBody.append(this._getRow(this._siteConfiguration.DataSources[i]));
}this.get_allDataSourcesCheckbox().checked=jQuery("input[id^='"+this._getDataSourceCheckboxIdPrefix()+"']:checked").length===this._siteConfiguration.DataSources.length;
if(!this._isCreateMode){var isReadMode=this._siteConfiguration.SiteConfigurationMode===Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureByDeployment;
this._switchDisplayMode(isReadMode);
}else{this._switchDisplayMode(false);
this._hideConfigurationModeContainers();
}jQuery(this._element).closest(".sfLoadingTransition").removeClass("sfLoadingTransition");
},_switchDisplayMode:function(isReadMode){if(isReadMode){jQuery(this._element).find("input").prop("disabled",true);
jQuery(this._element).find("table a").hide();
jQuery(this.get_manuallyConfiguredModeContainer()).hide();
jQuery(this.get_configuredByDeploymentModeContainer()).show();
}else{jQuery(this._element).find("input").prop("disabled",false);
jQuery(this._element).find("input:checked").next("a").show();
jQuery(this.get_manuallyConfiguredModeContainer()).show();
jQuery(this.get_configuredByDeploymentModeContainer()).hide();
}},_hideConfigurationModeContainers:function(){jQuery(this.get_manuallyConfiguredModeContainer()).hide();
jQuery(this.get_configuredByDeploymentModeContainer()).hide();
},_getRow:function(dataItem){var jRow=jQuery("<tr />");
jRow.append(this._getCheckboxCell(dataItem));
jRow.append(this._getLabelCell(dataItem));
jRow.append(this._getSiteDataSourceLinksCell(dataItem));
return jRow;
},_getCheckboxCell:function(dataItem){var jCell=jQuery("<td />");
var jCheckbox=jQuery('<input type="checkbox" />');
jCheckbox.attr("id",this._getDataSourceCheckboxId(dataItem.Name));
jCheckbox.attr("checked",dataItem.IsChecked);
var that=this;
var checkboxClick=function(sender,dataSourceName){return function(){dataItem.IsChecked=sender.is(":checked");
if(dataItem.DependantDataSources&&dataItem.DependantDataSources.length!==0){for(var i=0;
i<that._siteConfiguration.DataSources.length;
i++){var otherItem=that._siteConfiguration.DataSources[i];
for(var j=0;
j<dataItem.DependantDataSources.length;
j++){if(otherItem.Name===dataItem.DependantDataSources[j]){otherItem.IsChecked=sender.is(":checked");
}}}}that._updateUi();
};
}(jCheckbox,dataItem.Name);
jCheckbox.click(checkboxClick);
jCell.append(jCheckbox);
return jCell;
},_getLabelCell:function(dataItem){var jCell=jQuery("<td />");
jCell.append('<label class="sfItemTitle" for="'+this._getDataSourceCheckboxId(dataItem.Name)+'">'+(dataItem.Title?dataItem.Title:dataItem.Name)+"</label>");
return jCell;
},_getDataSourceCheckboxIdPrefix:function(){return this.get_id()+"_datasource_checkbox_";
},_getDataSourceCheckboxId:function(dataSourceName){return this._getDataSourceCheckboxIdPrefix()+dataSourceName.replace(/\.|\s/g,"_");
},_getSiteDataSourceLinksCell:function(dataItem){var jCell=jQuery("<td />");
jChangeAnchor=jQuery("<a />");
jChangeAnchor.attr("id",this._getChangeAnchorId(dataItem.Name));
jChangeAnchor.addClass("sfChangeLnk");
if(!dataItem.IsChecked){jChangeAnchor.hide();
}else{if(dataItem.Links.length===0&&dataItem.SampleLink){dataItem.Links=[dataItem.SampleLink];
}var siteDataSourceLinks=this._getTitles(dataItem.Links);
jCell.append(siteDataSourceLinks.join(", "));
}jChangeAnchor.html(this.get_clientLabelManager().getLabel("MultisiteResources","Change"));
var that=this;
jChangeAnchor.click(function(){that.get_dataSourceProviderSelectorDialog().show(that._siteConfiguration,dataItem);
});
jCell.append("&nbsp;");
jCell.append(jChangeAnchor);
return jCell;
},_getTitles:function(links){var result=[];
for(var i=0;
i<links.length;
i++){var title=links[i].ProviderTitle?links[i].ProviderTitle:links[i].ProviderName;
title=title.htmlEncode();
if(links[i].IsDefault){title="<span>"+title+"</span>";
}result.push(title);
}return result;
},_getChangeAnchorId:function(dataSourceName){return this.get_id()+"_change_"+dataSourceName.replace(/\./g,"_");
},_getSiteConfiguration:function(siteId,onSuccess){this._setLoadingViewVisible(true);
jQuery.ajax({type:"GET",url:this.get_webServiceUrl()+String.format("{0}/config/",siteId),contentType:"application/json",processData:false,success:onSuccess,error:this._ajaxFailDelegate,complete:this._ajaxCompleteDelegate});
},_getDataSources:function(onSuccess){this._setLoadingViewVisible(true);
jQuery.ajax({type:"GET",url:this.get_webServiceUrl()+"config/sources/",data:jQuery.param({siteName:this._siteConfiguration.Name}),contentType:"application/json",processData:false,success:onSuccess,error:this._ajaxFailDelegate,complete:this._ajaxCompleteDelegate});
},_ajaxCompleteHandler:function(jqXHR,textStatus){this._setLoadingViewVisible(false);
},_ajaxFailHandler:function(jqXHR,textStatus,errorThrown){this._stopMonitoringSiteInitializationProgress();
this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
},_setLoadingViewVisible:function(loading){if(loading){this._loadingCounter++;
}else{if(this._loadingCounter>0){this._loadingCounter--;
}}if(this._loadingCounter>0){jQuery(this.get_buttonsPanel()).hide();
jQuery(this.get_loadingView()).show();
}else{jQuery(this.get_loadingView()).hide();
if(!this._siteConfiguration||this._siteConfiguration.SiteConfigurationMode!==Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureByDeployment){jQuery(this.get_buttonsPanel()).show();
}}},_save:function(sender,args){this._saveSiteContentSources();
},_createButtonClickHandler:function(sender,args){this._createSite();
},_createSite:function(){this._setLoadingViewVisible(true);
jQuery.ajax({type:"PUT",url:this.get_webServiceUrl()+"/createSiteAsync/",contentType:"application/json",processData:false,data:this._getSiteConfigurationJsonModel(),success:this._startMonitoringSiteInitializationProgressDelegate,error:this._ajaxFailDelegate});
},_saveSiteContentSources:function(){this._setLoadingViewVisible(true);
var that=this;
jQuery.ajax({type:"PUT",url:this.get_webServiceUrl()+"sources/"+this._siteConfiguration.Id,contentType:"application/json",processData:false,data:this._getSiteConfigurationJsonModel(),success:function(){that.close();
},error:this._ajaxFailDelegate,complete:this._ajaxCompleteDelegate});
},_getSiteConfigurationJsonModel:function(){if(this.usersDataSource){this.usersDataSource.IsChecked=true;
this._siteConfiguration.DataSources.push(this.usersDataSource);
}return Telerik.Sitefinity.JSON.stringify(this._siteConfiguration);
},_startMonitoringSiteInitializationProgress:function(){this._monitorSiteInitializationProgress();
this._checkProgress=setInterval(this._monitorSiteInitializationProgressDelegate,2000);
},_stopMonitoringSiteInitializationProgress:function(){clearInterval(this._checkProgress);
this._checkProgress=null;
},_monitorSiteInitializationProgress:function(){jQuery.ajax({type:"GET",url:this.get_webServiceUrl()+"checkSiteInitializationStatus/",contentType:"application/json",processData:false,data:jQuery.param({siteName:this._siteConfiguration.Name}),success:this._checkSiteInitializationStatusCallbackDelegate,error:this._ajaxFailDelegate});
},_checkSiteInitializationStatusCallback:function(data){if(!data.IsInProgress){this._stopMonitoringSiteInitializationProgress();
if(data.ErrorMessage==""||data.ErrorMessage==null){siteDetailView.close("reload");
}else{this.get_messageControl().showNegativeMessage(data.ErrorMessage);
}this._setLoadingViewVisible(false);
}},_dataSourceProviderSelectorDialogCloseHandler:function(sender,args){var data=args.get_data();
if(!data){return;
}data.dataSource.Links=data.links;
this._updateUi();
},_getSiteDataSourceLinkIdx:function(moduleName,providerName){for(var i=0;
i<this._site.ContentSources.length;
i++){if(this._site.ContentSources[i].ModuleName===moduleName&&this._site.ContentSources[i].ProviderName===providerName){return i;
}}return -1;
},_back:function(sender,args){this.close();
},_allDataSourcesCheckboxClickHandler:function(sender,args){for(var i=0;
i<this._siteConfiguration.DataSources.length;
i++){this._siteConfiguration.DataSources[i].IsChecked=this.get_allDataSourcesCheckbox().checked;
}this._updateUi();
},_getDefaultSiteDataSourceLinkTitle:function(dataSourceTitle){return this._site.Name+" "+dataSourceTitle;
},_configureManuallyClickHandler:function(sender,args){this._changeConfigurationMode(Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureManually);
},_configureByDeploymentClickHandler:function(sender,args){this._changeConfigurationMode(Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.ConfigureByDeployment);
},_changeConfigurationMode:function(newMode){if(this._siteConfiguration.SiteConfigurationMode!==newMode){var that=this;
var onSuccess=function(data){that._siteConfiguration.SiteConfigurationMode=data.Mode;
that._updateUi();
};
var siteId=this._siteConfiguration.Id;
this._setLoadingViewVisible(true);
jQuery.ajax({type:"PUT",url:this.get_webServiceUrl()+String.format("{0}/config/mode",siteId),contentType:"application/json",processData:false,data:Telerik.Sitefinity.JSON.stringify({Mode:newMode}),success:onSuccess,error:this._ajaxFailDelegate,complete:this._ajaxCompleteDelegate});
}},initializeUserGroups:function(){var that=this;
this.userGroupsSectionElement=$("#userGroupsSection");
this.userSourcesGridElement=$("#userSourcesGrid");
$("#changeUserGroupsButton").click(function(){that.get_dataSourceProviderSelectorDialog().show(that._siteConfiguration,that.usersDataSource);
});
$(this.userSourcesGridElement).kendoGrid({columns:[{field:"ProviderTitle",title:" ",template:"#:ProviderTitle# #if(IsGlobalProvider) {#<span class='sfLightTxt'>"+that.get_clientLabelManager().getLabel("MultisiteResources","GlobalUserGroupNote")+"</span>#}#"},{template:"<span class='sfDeleteProfileOn'><i class='fa fa-trash-o' title='"+that.get_clientLabelManager().getLabel("MultisiteResources","RemoveFromThisSite")+"'></i></span>",attributes:{"class":"sfAlignCenter sfEditCol"}}],scrollable:false,dataBound:jQuery.proxy(this._usersDataSourceBound,this)});
$(this.userSourcesGridElement).on("click","span.sfDeleteProfileOn",function(){var row=$(this).closest("tr");
var grid=$(that.userSourcesGridElement).data("kendoGrid");
var dataItem=grid.dataItem(row);
grid.dataSource.remove(dataItem);
that.usersDataSource.Links=that.usersDataSource.Links.filter(function(item){return item.ProviderName!==dataItem.ProviderName;
});
});
},_usersDataSourceBound:function(arg){var that=this;
if(arg.sender._data.length==1){arg.sender.hideColumn(arg.sender.columns[1]);
}else{arg.sender.showColumn(arg.sender.columns[1]);
}},get_configureModulesViewWrapper:function(){return this._configureModulesViewWrapper;
},set_configureModulesViewWrapper:function(value){this._configureModulesViewWrapper=value;
},get_backLink:function(){return this._backLink;
},set_backLink:function(value){this._backLink=value;
},get_dialogTitle:function(){return this._dialogTitle;
},set_dialogTitle:function(value){this._dialogTitle=value;
},get_clientLabelManager:function(){return this._clientLabelManager;
},set_clientLabelManager:function(value){this._clientLabelManager=value;
},get_webServiceUrl:function(){return this._webServiceUrl;
},set_webServiceUrl:function(value){this._webServiceUrl=value;
},get_dataSourcesTableBody:function(){return this._dataSourcesTableBody;
},set_dataSourcesTableBody:function(value){this._dataSourcesTableBody=value;
},get_messageControl:function(){return this._messageControl;
},set_messageControl:function(value){this._messageControl=value;
},get_loadingView:function(){return this._loadingView;
},set_loadingView:function(value){this._loadingView=value;
},get_buttonsPanel:function(){return this._buttonsPanel;
},set_buttonsPanel:function(value){this._buttonsPanel=value;
},get_cancelLink:function(){return this._cancelLink;
},set_cancelLink:function(value){this._cancelLink=value;
},get_saveButton:function(){return this._saveButton;
},set_saveButton:function(value){this._saveButton=value;
},get_site:function(){return this._site;
},set_site:function(value){this._site=value;
},get_dataSourceProviderSelectorDialog:function(){return this._dataSourceProviderSelectorDialog;
},set_dataSourceProviderSelectorDialog:function(value){this._dataSourceProviderSelectorDialog=value;
},get_propertiesView:function(){return this._propertiesView;
},set_propertiesView:function(value){this._propertiesView=value;
},get_allDataSourcesCheckbox:function(){return this._allDataSourcesCheckbox;
},set_allDataSourcesCheckbox:function(value){this._allDataSourcesCheckbox=value;
},get_createButton:function(){return this._createButton;
},set_createButton:function(value){this._createButton=value;
},get_manuallyConfiguredModeContainer:function(){return this._manuallyConfiguredModeContainer;
},set_manuallyConfiguredModeContainer:function(value){this._manuallyConfiguredModeContainer=value;
},get_configureByDeploymentBtn:function(){return this._configureByDeploymentBtn;
},set_configureByDeploymentBtn:function(value){this._configureByDeploymentBtn=value;
},get_configuredByDeploymentModeContainer:function(){return this._configuredByDeploymentModeContainer;
},set_configuredByDeploymentModeContainer:function(value){this._configuredByDeploymentModeContainer=value;
},get_configureManuallyBtn:function(){return this._configureManuallyBtn;
},set_configureManuallyBtn:function(value){this._configureManuallyBtn=value;
}};
Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView.registerClass("Telerik.Sitefinity.Multisite.Web.UI.ConfigureModulesView",Sys.UI.Control);
