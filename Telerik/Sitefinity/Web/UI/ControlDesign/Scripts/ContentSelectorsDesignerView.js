Type._registerScript("ContentSelectorsDesignerView.js",["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");
Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView=function(element){Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView.initializeBase(this,[element]);
this._selectContentButton=null;
this._selectedContentTitle=null;
this._controlData=null;
this._contentSelector=null;
this._showContentSelectorDelegate=null;
this._selectContentDelegate=null;
this._parentDesigner=null;
this._radioChoices=null;
this._refreshing=false;
this._filterSelector=null;
this._currentDetailViewName=null;
this._currentMasterViewName=null;
this._btnNarrowSelection=null;
this._narrowSelection=null;
this._btnNarrowSelectionClickDelegate=null;
this._selectorTag=null;
this._dialog=null;
this._providersSelector=null;
this._providersSelectorClickedDelegate=null;
this._noSingleItemSelectedLabel=null;
this._selectContentButtonWrapper=null;
};
Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView.callBaseMethod(this,"initialize");
this._radioClickDelegate=Function.createDelegate(this,this._setContentFilter);
this.get_radioChoices().click(this._radioClickDelegate);
$(this).on("unload",function(e){jQuery.event.remove(this);
jQuery.removeData(this);
});
if(this._selectContentButton){this._showContentSelectorDelegate=Function.createDelegate(this,this._showContentSelector);
$addHandler($get(this._selectContentButton),"click",this._showContentSelectorDelegate);
}if(this._contentSelector){this._selectContentDelegate=Function.createDelegate(this,this._selectContent);
this._contentSelector.add_doneClientSelection(this._selectContentDelegate);
}if(this._btnNarrowSelection){this._btnNarrowSelectionClickDelegate=Function.createDelegate(this,this._btnNarrowSelectionClick);
$addHandler(this._btnNarrowSelection,"click",this._btnNarrowSelectionClickDelegate);
}if(this._providersSelector){this._providersSelectorClickedDelegate=Function.createDelegate(this,this._handleProvidersChanged);
this._providersSelector.add_onProviderSelected(this._providersSelectorClickedDelegate);
}this._dialog=jQuery(this._selectorTag).dialog({autoOpen:false,modal:false,width:355,closeOnEscape:true,resizable:false,draggable:false,classes:{"ui-dialog":"sfZIndexL"}});
},dispose:function(){if(this._selectContentButton){$removeHandler($get(this._selectContentButton),"click",this._showContentSelectorDelegate);
delete this._showContentSelectorDelegate;
}if(this._contentSelector){this._contentSelector.remove_doneClientSelection(this._selectContentDelegate);
delete this._selectContentDelegate;
}if(this._radioClickDelegate){this.get_radioChoices().unbind("click",this._radioClickDelegate);
delete this._radioClickDelegate;
}if(this._btnNarrowSelection){$removeHandler(this._btnNarrowSelection,"click",this._btnNarrowSelectionClickDelegate);
delete this._btnNarrowSelectionClickDelegate;
}if(this._providersSelector){this._providersSelector.remove_onProviderSelected(this._providersSelectorClickedDelegate);
delete this._providersSelectorClickedDelegate;
}Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView.callBaseMethod(this,"dispose");
},refreshUI:function(){this._refreshing=true;
var controlData=this.get_controlData();
if(!controlData){return;
}var additionalFilter=this.get_currentMasterView().AdditionalFilter;
if(additionalFilter){additionalFilter=Sys.Serialization.JavaScriptSerializer.deserialize(additionalFilter);
}this._filterSelector.set_queryData(additionalFilter);
var disabledFilter=true;
if(controlData.ContentViewDisplayMode!="Detail"){if(additionalFilter){this.get_radioChoices()[2].click();
disabledFilter=false;
}else{this.get_radioChoices()[0].click();
}}else{this.get_radioChoices()[1].click();
}this._filterSelector.set_disabled(disabledFilter);
dialogBase.resizeToContent();
this._refreshing=false;
},applyChanges:function(){var displayMode=this.get_controlData().ContentViewDisplayMode;
if(displayMode=="Automatic"){this.get_currentDetailView().DataItemId="00000000-0000-0000-0000-000000000000";
}this.get_currentMasterView().AdditionalFilter=null;
if(displayMode=="Automatic"||displayMode=="Master"){if(this.get_radioChoices().eq(2).attr("checked")){this._filterSelector.applyChanges();
var queryData=this._filterSelector.get_queryData();
if(queryData.QueryItems&&queryData.QueryItems.length>0){queryData=Telerik.Sitefinity.JSON.stringify(queryData);
}else{queryData=null;
}this.get_currentMasterView().AdditionalFilter=queryData;
}}},resetCurrentView:function(){this._resetViewOnProvidersChanged();
},resetCurrentViewValidating:function(validateTypeSelected,currentView,rootType,childView){this._resetDesignerFilters(validateTypeSelected,currentView,rootType,childView);
},_selectContent:function(items){this._dialog.dialog("close");
jQuery("body > form").show();
dialogBase.resizeToContent();
if(items==null){return;
}var contentSelector=this.get_contentSelector();
if(contentSelector){var selectedItems=contentSelector.getSelectedItems();
if(selectedItems!=null){if(selectedItems.length>0){var controlData=this.get_controlData();
this.get_currentDetailView().DataItemId=selectedItems[0].Id;
controlData.ContentViewDisplayMode="Detail";
var selectedContentTitle=this.get_selectedContentTitle();
if(selectedContentTitle){if(selectedItems[0].Title.hasOwnProperty("Value")){selectedContentTitle.innerText=selectedItems[0].Title.Value;
}else{selectedContentTitle.innerText=selectedItems[0].Title;
}jQuery(selectedContentTitle).show();
}}}}},_showContentSelector:function(){var contentSelector=this.get_contentSelector();
if(contentSelector){var controlData=this.get_controlData();
if(controlData.ControlDefinition.hasOwnProperty("ProviderName")){contentSelector.set_providerName(controlData.ControlDefinition.ProviderName);
}contentSelector.dataBind();
var dataItemId=this.get_currentDetailView().DataItemId;
if(dataItemId){contentSelector.set_selectedKeys([dataItemId]);
}}this._dialog.dialog("open");
jQuery("body > form").hide();
dialogBase.resizeToContent();
},_btnNarrowSelectionClick:function(){jQuery(this._narrowSelection).toggleClass("sfExpandedSection");
dialogBase.resizeToContent();
},_handleProvidersChanged:function(sender,args){var controlData=this.get_controlData();
if(controlData.ControlDefinition&&controlData.ControlDefinition.hasOwnProperty("ProviderName")){var oldProviderName=controlData.ControlDefinition.ProviderName;
controlData.ControlDefinition.ProviderName=args.ProviderName;
if(args.ProviderName!=oldProviderName){this._resetViewOnProvidersChanged();
}}if(this.get_contentSelector()!=null){this.get_contentSelector().set_providerName(args.ProviderName);
}dialogBase.resizeToContent();
},_hideContent:function(sender,args){this.get_radioChoices()[0].click();
this.get_radioChoices().eq(0).attr("checked",true);
this.get_selectedContentTitle().innerHTML=this._noSingleItemSelectedLabel;
this._filterSelector.set_disabled(true);
},_resetDesignerFilters:function(resetAll,currentView,rootType,childView){if(typeof currentView!=="undefined"){if(childView!=rootType){if(currentView=="Full"){this._hideContent();
}else{if(childView!=rootType&&childView!=currentView){if(currentView!="Full"){this._hideContent();
}}}}else{if(currentView!=rootType&&currentView!=childView){if(currentView!="Full"){this._hideContent();
}}}}if(typeof resetAll!=="undefined"){if(resetAll){this.get_currentDetailView().DataItemId=Telerik.Sitefinity.getEmptyGuid();
this.get_contentSelector().set_selectedKeys([]);
}}if(this._selectContentButtonWrapper){jQuery(this._selectContentButtonWrapper).show();
}if(this.get_parentDesigner()&&typeof(this.get_parentDesigner().get_message)==="function"){this.get_parentDesigner().get_message().hide();
}},_resetViewOnProvidersChanged:function(){this.get_radioChoices()[0].click();
this.get_radioChoices().eq(0).attr("checked",true);
this.get_selectedContentTitle().innerHTML=this._noSingleItemSelectedLabel;
this.get_currentDetailView().DataItemId=Telerik.Sitefinity.getEmptyGuid();
this.get_contentSelector().set_selectedKeys([]);
this._filterSelector.set_disabled(true);
if(this._selectContentButtonWrapper){jQuery(this._selectContentButtonWrapper).show();
}if(this.get_parentDesigner()&&typeof(this.get_parentDesigner().get_message)==="function"){this.get_parentDesigner().get_message().hide();
}},_setContentFilter:function(sender){var radioID=sender.target.value;
var controlData=this.get_controlData();
var disabledFilter=true;
switch(radioID){case"contentSelect_AllItems":jQuery(this.get_element()).find("#selectorPanel").hide();
if(!this._refreshing){controlData.ContentViewDisplayMode="Automatic";
}break;
case"contentSelect_OneItem":jQuery(this.get_element()).find("#selectorPanel").show();
if(!this._refreshing){controlData.ContentViewDisplayMode="Detail";
}break;
case"contentSelect_SimpleFilter":jQuery(this.get_element()).find("#selectorPanel").hide();
disabledFilter=false;
if(!this._refreshing){controlData.ContentViewDisplayMode="Automatic";
}break;
case"contentSelect_AdvancedFilter":break;
}this._filterSelector.set_disabled(disabledFilter);
dialogBase.resizeToContent();
},get_radioChoices:function(){if(!this._radioChoices){this._radioChoices=jQuery(this.get_element()).find(":radio[name$=ContentSelection]");
}return this._radioChoices;
},get_parentDesigner:function(){return this._parentDesigner;
},set_parentDesigner:function(value){this._parentDesigner=value;
},get_controlData:function(){return this.get_parentDesigner().get_propertyEditor().get_control();
},get_currentDetailViewName:function(){return(this._currentDetailViewName)?this._currentDetailViewName:this.get_controlData().DetailViewName;
},get_currentDetailView:function(){return this.get_controlData().ControlDefinition.Views[this.get_currentDetailViewName()];
},get_currentMasterViewName:function(){return(this._currentMasterViewName)?this._currentMasterViewName:this.get_controlData().MasterViewName;
},get_currentMasterView:function(){return this.get_controlData().ControlDefinition.Views[this.get_currentMasterViewName()];
},get_contentSelector:function(){return this._contentSelector;
},set_contentSelector:function(value){this._contentSelector=value;
},get_selectedContentTitle:function(){return this._selectedContentTitle;
},set_selectedContentTitle:function(value){this._selectedContentTitle=value;
},get_filterSelector:function(){return this._filterSelector;
},set_filterSelector:function(value){this._filterSelector=value;
},get_btnNarrowSelection:function(){return this._btnNarrowSelection;
},set_btnNarrowSelection:function(value){this._btnNarrowSelection=value;
},get_narrowSelection:function(){return this._narrowSelection;
},set_narrowSelection:function(value){this._narrowSelection=value;
},get_selectorTag:function(){return this._selectorTag;
},set_selectorTag:function(value){this._selectorTag=value;
},set_narrowSelection:function(value){this._narrowSelection=value;
},get_providersSelector:function(){return this._providersSelector;
},set_providersSelector:function(value){this._providersSelector=value;
},get_noSingleItemSelectedLabel:function(){return this._noSingleItemSelectedLabel;
},set_noSingleItemSelectedLabel:function(value){this._noSingleItemSelectedLabel=value;
},get_selectContentButtonWrapper:function(){return this._selectContentButtonWrapper;
},set_selectContentButtonWrapper:function(value){this._selectContentButtonWrapper=value;
}};
Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView.registerClass("Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
if(typeof(Sys)!=="undefined"){Sys.Application.notifyScriptLoaded();
}