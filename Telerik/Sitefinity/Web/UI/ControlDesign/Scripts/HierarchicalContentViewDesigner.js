Type._registerScript("HierarchicalContentViewDesigner.js",["ControlDesignerBase.js"]);
Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design");
Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner=function(element){Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner.initializeBase(this,[element]);
this._viewModeDelegate=null;
this._rootContentType=null;
this._dynamicContentSelectorsViewsMap=null;
this._listSettingViewsMap=null;
this._templatesSelectorsViewsMap=null;
this._singleTypeTemplatesSelector=null;
this._typesSettings=null;
this._dialog=null;
this._selectorTag=null;
this._pageSelectButton=null;
this._removePageButton=null;
this._pagesSelector=null;
this._showPageSelectorDelegate=null;
this._pageSelectedDelegate=null;
this._selectedPageContainer=null;
this._selectPageText=null;
this._changePageText=null;
this._syncProvidersDelegate=null;
};
Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner.prototype={initialize:function(){Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner.callBaseMethod(this,"initialize");
this._syncProvidersDelegate=Function.createDelegate(this,this._syncProviders);
if(this._dynamicContentSelectorsViewsMap!=null){for(var i in this._dynamicContentSelectorsViewsMap){var dynamicContentSelector=$find(this._dynamicContentSelectorsViewsMap[i]);
if(dynamicContentSelector){dynamicContentSelector.set_parentDesigner(this);
}}}if(this._listSettingViewsMap!=null){for(var i in this._listSettingViewsMap){var listSetting=$find(this._listSettingViewsMap[i]);
if(listSetting){listSetting.set_parentDesigner(this);
}}}if(this._templatesSelectorsViewsMap!=null){for(var i in this._templatesSelectorsViewsMap){var templatesSelector=$find(this._templatesSelectorsViewsMap[i]);
if(templatesSelector){templatesSelector.set_parentDesigner(this);
}}}$(this).on("unload",function(e){jQuery.event.remove(this);
jQuery.removeData(this);
});
this._viewModeDelegate=Function.createDelegate(this,this._clickedViewMode);
this._setRadioClickHandler("ViewMode",this._viewModeDelegate);
this._showPageSelectorDelegate=Function.createDelegate(this,this._showPageSelector);
$addHandler(this._pageSelectButton,"click",this._showPageSelectorDelegate);
this._pageSelectedDelegate=Function.createDelegate(this,this._pageSelected);
this._pagesSelector.add_doneClientSelection(this._pageSelectedDelegate);
this._removePageDelegate=Function.createDelegate(this,this._pageRemoved);
$addHandler(this._removePageButton,"click",this._removePageDelegate);
this._dialog=jQuery(this._selectorTag).dialog({autoOpen:false,modal:false,width:355,closeOnEscape:true,resizable:false,draggable:false,classes:{"ui-dialog":"sfZIndexL"}});
},dispose:function(){Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner.callBaseMethod(this,"dispose");
this._viewModeDelegate=null;
if(this._dynamicContentSelectorsViewsMap!=null){for(var i in this._dynamicContentSelectorsViewsMap){var dynamicContentSelector=$find(this._dynamicContentSelectorsViewsMap[i]);
if(dynamicContentSelector&&dynamicContentSelector.get_providersSelector()){dynamicContentSelector.get_providersSelector().remove_onProviderSelected(this._syncProvidersDelegate);
}}}if(this._syncProvidersDelegate){delete this._syncProvidersDelegate;
}if(this._viewModeDelegate){delete this._viewModeDelegate;
}if(this._showPageSelectorDelegate){delete this._showPageSelectorDelegate;
}this._pagesSelector.remove_doneClientSelection(this._pageSelectedDelegate);
if(this._pageSelectedDelegate){delete this._pageSelectedDelegate;
}if(this._removePageDelegate){delete this._removePageDelegate;
}},refreshUI:function(){var data=this.get_controlData();
var currentMode=data.ViewMode;
this._clickRadioChoice("ViewMode",currentMode);
if(this._dynamicContentSelectorsViewsMap!=null){for(var i in this._dynamicContentSelectorsViewsMap){var dynamicContentSelectors=$find(this._dynamicContentSelectorsViewsMap[i]);
if(dynamicContentSelectors){dynamicContentSelectors.refreshUI();
}if(i!=currentMode){var validateParticularTypeSelected=false;
dynamicContentSelectors._filterSelector._queryData.QueryItems=[];
dynamicContentSelectors.resetCurrentViewValidating(validateParticularTypeSelected,data.ViewMode,this._rootContentType,i);
}}}var typesSettings=Sys.Serialization.JavaScriptSerializer.deserialize(this._typesSettings);
if(this._listSettingViewsMap!=null){for(var i in this._listSettingViewsMap){var listSettingsControl=$find(this._listSettingViewsMap[i]);
if(listSettingsControl){var currentTypeSettings=this._getTypeSettings(typesSettings,i);
this._setListSettingsToControl(listSettingsControl,currentTypeSettings);
listSettingsControl.refreshUI();
}}}if(this._templatesSelectorsViewsMap!=null){for(var i in this._templatesSelectorsViewsMap){var templatesSelector=$find(this._templatesSelectorsViewsMap[i]);
if(templatesSelector){var currentTypeSettings=this._getTypeSettings(typesSettings,i);
this._setTemplatesSettingsToControl(templatesSelector,currentTypeSettings);
templatesSelector.refreshUI();
}}}dialogBase.resizeToContent();
},applyChanges:function(){var data=this.get_controlData();
var selectedType=data.ViewMode;
if(selectedType==="Full"){selectedType=this._rootContentType;
this.get_masterView().DetailsPageId=null;
}if(this._dynamicContentSelectorsViewsMap!=null){for(var i in this._dynamicContentSelectorsViewsMap){var dynamicContentSelectors=$find(this._dynamicContentSelectorsViewsMap[i]);
if(i!=selectedType){var validateParticularTypeSelected=false;
dynamicContentSelectors._filterSelector._queryData.QueryItems=[];
dynamicContentSelectors.resetCurrentViewValidating(validateParticularTypeSelected,data.ViewMode,this._rootContentType,i);
}if(dynamicContentSelectors&&i===selectedType){dynamicContentSelectors.applyChanges();
}}}var typesSettings=Sys.Serialization.JavaScriptSerializer.deserialize(this._typesSettings);
if(this._listSettingViewsMap!=null){for(var i in this._listSettingViewsMap){var listSettingsControl=$find(this._listSettingViewsMap[i]);
if(listSettingsControl){var currentTypeSettings=this._getTypeSettings(typesSettings,i);
this._setListSettingsFromControl(listSettingsControl,currentTypeSettings);
}}}if(this._templatesSelectorsViewsMap!=null){for(var i in this._templatesSelectorsViewsMap){var templatesSelector=$find(this._templatesSelectorsViewsMap[i]);
if(templatesSelector){var currentTypeSettings=this._getTypeSettings(typesSettings,i);
this._setTemplatesSettingsFromControl(templatesSelector,currentTypeSettings);
}}}this._typesSettings=Sys.Serialization.JavaScriptSerializer.serialize(typesSettings);
data.TypesSettings=Telerik.Sitefinity.JSON.stringify(typesSettings);
},setFieldControlValueToControlData:function(fieldControl,controlData){var value=fieldControl.get_value();
var propertyPath=fieldControl.get_fieldName();
this.setValueToControlData(propertyPath,value);
},setValueToControlData:function(propertyPath,value,controlData){this._setValueByPath(propertyPath,controlData,value);
},executeCommand:function(argument){},_onLoad:function(){Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner.callBaseMethod(this,"_onLoad");
$("a.sfExpander").click(function(){var parent=$(this).parents(".sfExpandableSection:first");
parent.toggleClass("sfExpandedSection");
parent.find(".sfExpandableTarget:first").toggle();
parent.siblings(".sfExpandableSection").removeClass("sfExpandedSection").find(".sfExpandableTarget:first").hide();
dialogBase.resizeToContent();
});
jQuery("#countersDetailsLinkSingle").click(function(){jQuery("#countersDetailsSingle").toggle();
});
jQuery(".sfHierarchicalContentMode > li:last-child").addClass("sfLast");
if(jQuery(".sfHierarchicalContentMode > li").length==3){jQuery(".sfHierarchicalContentMode > .sfFullFunctionality").addClass("sfTwoTypes");
}else{if(jQuery(".sfHierarchicalContentMode > li").length==4){jQuery(".sfHierarchicalContentMode > .sfFullFunctionality").addClass("sfThreeTypes");
}else{if(jQuery(".sfHierarchicalContentMode > li").length==5){jQuery(".sfHierarchicalContentMode > .sfFullFunctionality").addClass("sfFourTypes");
}else{if(jQuery(".sfHierarchicalContentMode > li").length>5){jQuery(".sfHierarchicalContentMode > .sfFullFunctionality").addClass("sfManyTypes");
}}}}var data=this.get_controlData();
if(this._dynamicContentSelectorsViewsMap!=null){var selectedType=data.ViewMode;
if(selectedType==="Full"){selectedType=this._rootContentType;
}for(var i in this._dynamicContentSelectorsViewsMap){var dynamicContentSelector=$find(this._dynamicContentSelectorsViewsMap[i]);
if(dynamicContentSelector&&dynamicContentSelector.get_providersSelector()){dynamicContentSelector.get_providersSelector().add_onProviderSelected(this._syncProvidersDelegate);
jQuery(dynamicContentSelector.get_providersSelector().get_element()).addClass("sfSelectSpecificWrp sfMBottom10");
}}}dialogBase.resizeToContent();
},_setRadioClickHandler:function(groupName,delegate){jQuery(this.get_element()).find("input[name='"+groupName+"']").click(delegate);
},_clickRadioChoice:function(groupName,value){return jQuery(this.get_element()).find("input[name='"+groupName+"'][value='"+value+"']").click();
},_clickedViewMode:function(sender){var data=this.get_controlData();
this._showRelevantSettings(sender.target.value);
data.ViewMode=sender.target.value;
},_pageSelected:function(items){this._dialog.dialog("close");
jQuery("body > form").show();
dialogBase.resizeToContent();
if(items==null){return;
}var selectedItem=this.get_pagesSelector().getSelectedItems()[0];
if(selectedItem){var masterView=this.get_masterView();
this.get_selectedPageLabel().innerHTML=selectedItem.Title;
jQuery(this.get_selectedPageContainer()).show();
this.get_pageSelectButton().innerHTML=this._changePageText;
masterView.DetailsPageId=selectedItem.Id;
}},_pageRemoved:function(){this.get_masterView().DetailsPageId=null;
jQuery(this.get_selectedPageContainer()).hide();
this.get_pageSelectButton().innerHTML=this._selectPageText;
},_syncProviders:function(sender,args){var currentViewMode=this.get_controlData().ViewMode;
if(this._dynamicContentSelectorsViewsMap!=null){for(var i in this._dynamicContentSelectorsViewsMap){if(i!==currentViewMode){var dynamicContentSelector=$find(this._dynamicContentSelectorsViewsMap[i]);
if(dynamicContentSelector){if(dynamicContentSelector.get_providersSelector().get_selectedProvider()!==args.ProviderName){dynamicContentSelector.get_providersSelector().selectProvider(args.ProviderName,args.ProviderTitle,false);
dynamicContentSelector.resetCurrentView();
}}}}}},_showRelevantSettings:function(viewMode){var thisControl=this.get_element();
this._hideAllSettings(thisControl);
var option=viewMode;
if(option==="Full"){option=this._rootContentType;
jQuery(thisControl).find(".sfFullFunctionalityListSettings>.sfExpandableSection>h3").show().parent().removeClass("sfExpandedSection");
jQuery(thisControl).find(".sfFullFunctionalityTemplatesSelectors>.sfExpandableSection>h3").show().parent().removeClass("sfExpandedSection");
jQuery(thisControl).find(".sfExpandableSection .sfExpandableSection .sfRadioList, #detailsTemplateContainer").addClass("sfMLeft15");
}else{jQuery(thisControl).find(".sf"+option+"ListSettings").show();
jQuery(thisControl).find(".sf"+option+"TemplatesSelector").show();
jQuery(thisControl).find("#pageSelectorContainer").show();
jQuery(thisControl).find("#pageSelectorContainer .sf"+option+"PagesSelectorTitle").show();
}jQuery(this.get_element()).find("#groupSettingSelectSource div[id='"+option+"ContentSelector']").show();
dialogBase.resizeToContent();
},_hideAllSettings:function(thisControl){jQuery(thisControl).find(".sfExpandableSection .sfExpandableSection .sfRadioList, #detailsTemplateContainer").removeClass("sfMLeft15");
jQuery(thisControl).find(".sfFullFunctionalityListSettings>.sfExpandableSection>h3").hide();
jQuery(thisControl).find(".sfFullFunctionalityListSettings>.sfExpandableSection>.sfExpandableTarget").hide();
jQuery(thisControl).find(".sfFullFunctionalityTemplatesSelectors>.sfExpandableSection>h3").hide();
jQuery(thisControl).find(".sfFullFunctionalityTemplatesSelectors>.sfExpandableSection>.sfExpandableTarget").hide();
jQuery(thisControl).find("#groupSettingSelectSource div[id$='ContentSelector']").hide();
jQuery(thisControl).find("#pageSelectorContainer").hide();
jQuery(thisControl).find('#pageSelectorContainer div[class$="PagesSelectorTitle"]').hide();
},_setListSettingsToControl:function(listSettingsControl,typeSettings){var currentView=listSettingsControl.get_currentView();
currentView.AllowPaging=typeSettings.AllowPaging;
currentView.ItemsPerPage=typeSettings.ItemsPerPage;
currentView.SortExpression=typeSettings.SortExpression;
},_setListSettingsFromControl:function(listSettingsControl,typeSettings){listSettingsControl.applyChanges();
var currentView=listSettingsControl.get_currentView();
typeSettings.ItemsPerPage=currentView.ItemsPerPage;
typeSettings.AllowPaging=currentView.AllowPaging;
typeSettings.SortExpression=currentView.SortExpression;
},_setTemplatesSettingsToControl:function(templatesSettingsControl,typeSettings){templatesSettingsControl.set_detailTemplateId(typeSettings.DetailTemplateId);
templatesSettingsControl.set_masterTemplateId(typeSettings.MasterTemplateId);
},_setTemplatesSettingsFromControl:function(templatesSettingsControl,typeSettings){templatesSettingsControl.applyChanges();
typeSettings.DetailTemplateId=templatesSettingsControl.get_detailTemplateId();
typeSettings.MasterTemplateId=templatesSettingsControl.get_masterTemplateId();
},_getTypeSettings:function(typesSettings,typeName){var filteredSettings=typesSettings.filter(function(val){return val.Key===typeName;
});
if(filteredSettings&&filteredSettings.length>0){return filteredSettings[0].Value;
}},_pathToChunks:function(path){path=path.replace(/\[/g,".");
path=path.replace(/\]/g,"");
path=path.replace(/\"|\'/g,"");
return path.split(".");
},_setValueByPath:function(path,obj,value){var pathChunks;
if(path.constructor!=Array){pathChunks=this._pathToChunks(path);
}else{pathChunks=path;
}var currentChunk=pathChunks[0];
var depth=pathChunks.length;
var ready=false;
for(var memberName in obj){if(memberName==currentChunk){if(depth==1){obj[memberName]=value;
ready=true;
}else{ready=this._setValueByPath(pathChunks.slice(1),obj[memberName],value);
}}if(ready){return true;
}}return false;
},_showPageSelector:function(){this._dialog.dialog("open");
jQuery("body > form").hide();
dialogBase.resizeToContent();
},get_rootContentType:function(){return this._rootContentType;
},set_rootContentType:function(value){this._rootContentType=value;
},get_listSettingViewsMap:function(){return this._listSettingViewsMap;
},set_listSettingViewsMap:function(value){this._listSettingViewsMap=value;
},get_templatesSelectorsViewsMap:function(){return this._templatesSelectorsViewsMap;
},set_templatesSelectorsViewsMap:function(value){this._templatesSelectorsViewsMap=value;
},get_dynamicContentSelectorsViewsMap:function(){return this._dynamicContentSelectorsViewsMap;
},set_dynamicContentSelectorsViewsMap:function(value){this._dynamicContentSelectorsViewsMap=value;
},get_singleTypeTemplatesSelector:function(){return this._singleTypeTemplatesSelector;
},set_singleTypeTemplatesSelector:function(value){this._singleTypeTemplatesSelector=value;
},get_singleTypeTemplatesSelectorControl:function(){return $find(this._singleTypeTemplatesSelector);
},set_saveButtonText:function(text){jQuery(this.get_saveButton()).find(".sfLinkBtnIn").text(text);
},set_saveButtonEnabled:function(state){jQuery(this.get_saveButton())[state?"removeClass":"addClass"]("sfDisabledLinkBtn");
},get_saveButton:function(){return this.get_propertyEditor().get_saveButton();
},get_pageSelectButton:function(){return this._pageSelectButton;
},set_pageSelectButton:function(value){this._pageSelectButton=value;
},get_removePageButton:function(){return this._removePageButton;
},set_removePageButton:function(value){this._removePageButton=value;
},get_selectorTag:function(){return this._selectorTag;
},set_selectorTag:function(value){this._selectorTag=value;
},get_pagesSelector:function(){return this._pagesSelector;
},set_pagesSelector:function(value){this._pagesSelector=value;
},get_masterViewName:function(){return this.get_controlData().MasterViewName;
},get_masterView:function(){return this.get_controlData().ControlDefinition.Views[this.get_masterViewName()];
},get_selectedPageLabel:function(){return this._selectedPageLabel;
},set_selectedPageLabel:function(value){this._selectedPageLabel=value;
},get_selectedPageContainer:function(){return this._selectedPageContainer;
},set_selectedPageContainer:function(value){this._selectedPageContainer=value;
},get_selectPageText:function(){return this._selectPageText;
},set_selectPageText:function(value){this._selectPageText=value;
},get_changePageText:function(){return this._changePageText;
},set_changePageText:function(value){this._changePageText=value;
},get_typesSettings:function(){return this._typesSettings;
},set_typesSettings:function(value){this._typesSettings=value;
}};
Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner.registerClass("Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.Design.HierarchicalContentViewDesigner",Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if(typeof(Sys)!=="undefined"){Sys.Application.notifyScriptLoaded();
}