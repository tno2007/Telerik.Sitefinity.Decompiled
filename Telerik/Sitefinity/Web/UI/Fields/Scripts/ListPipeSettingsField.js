Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField=function(element){this._existingPipesBinder=null;
this._value=[];
this._windowManager=null;
this._dialogWindow=null;
this._dialogUrl=null;
this._currentPipeSettings=null;
this._currentPipeSettingsKey=null;
this._disableRemoving=false;
this._changePipeText="";
this._disableActivation=false;
this._showDefaultPipes=false;
this._publishingPointDefinitionFieldName=null;
this._publishingPointNameFieldName=null;
this._itemCommandHandlerDelegate=null;
this._dataBoundHandlerDelegate=null;
this._windowLoadHandlerDelegate=null;
this._dialogDoneHandlerDelegate=null;
this._addSettingsHandlerDelegate=null;
this._itemDataBoundHandlerDelegate=null;
this._pagePickerOpenedDelegate=null;
this._pagePickerClosedDelegate=null;
this._defaultPipes=null;
this._defaultCreatePipe=null;
this._addSettingsButton=null;
this._outBoundPipesMode=false;
this._fieldChangeNotifier=null;
this._backLinksPagePicker=null;
this._currentArgs=null;
this._showContentLocation=false;
Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField.callBaseMethod(this,"initialize");
if(this._itemCommandHandlerDelegate==null){this._itemCommandHandlerDelegate=Function.createDelegate(this,this._itemCommandHandler);
}if(this._dataBoundHandlerDelegate==null){this._dataBoundHandlerDelegate=Function.createDelegate(this,this._dataBoundHandler);
}if(this._windowLoadHandlerDelegate==null){this._windowLoadHandlerDelegate=Function.createDelegate(this,this._windowLoadHandler);
}if(this._dialogDoneHandlerDelegate==null){this._dialogDoneHandlerDelegate=Function.createDelegate(this,this._dialogDoneHandler);
}if(this._addSettingsHandlerDelegate==null){this._addSettingsHandlerDelegate=Function.createDelegate(this,this._addSettingsHandler);
}if(this._itemDataBoundHandlerDelegate==null){this._itemDataBoundHandlerDelegate=Function.createDelegate(this,this._itemDataBoundHandler);
}if(this._loadHandlerDelegate==null){this._loadHandlerDelegate=Function.createDelegate(this,this._loadHandler);
}this._getLatestPublishingPoitDefinitionDelegate=Function.createDelegate(this,this._getLatestPublishingPoitDefinition);
this._pagePickerOpenedDelegate=Function.createDelegate(this,this._pagePickerOpenedHandler);
this.get_backLinksPagePicker().add_selectorOpened(this._pagePickerOpenedDelegate);
this._pagePickerClosedDelegate=Function.createDelegate(this,this._pagePickerClosedHandler);
this.get_backLinksPagePicker().add_selectorClosed(this._pagePickerClosedDelegate);
$addHandler(this._addSettingsButton,"click",this._addSettingsHandlerDelegate);
this._existingPipesBinder.add_onItemCommand(this._itemCommandHandlerDelegate);
this._existingPipesBinder.add_onDataBound(this._dataBoundHandlerDelegate);
this._existingPipesBinder.add_onItemDataBound(this._itemDataBoundHandlerDelegate);
Sys.Application.add_load(this._loadHandlerDelegate);
if(this._showDefaultPipes){this._value=this._defaultPipes;
this._doExpandHandler();
this._existingPipesBinder.BindCollection({Items:this._value});
}$(this).on("unload",function(e){jQuery.event.remove(this);
jQuery.removeData(this);
});
},dispose:function(){Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField.callBaseMethod(this,"dispose");
$removeHandler(this._addSettingsButton,"click",this._addSettingsHandlerDelegate);
if(this._existingPipesBinder){this._existingPipesBinder.remove_onItemCommand(this._itemCommandHandlerDelegate);
this._existingPipesBinder.remove_onDataBound(this._dataBoundHandlerDelegate);
this._existingPipesBinder.remove_onItemDataBound(this._itemDataBoundHandlerDelegate);
}this._dialogWindow.remove_pageLoad(this._windowLoadHandlerDelegate);
Sys.Application.remove_load(this._loadHandlerDelegate);
if(this._itemCommandHandlerDelegate){delete this._itemCommandHandlerDelegate;
}if(this._windowLoadHandlerDelegate){delete this._windowLoadHandlerDelegate;
}if(this._dialogDoneHandlerDelegate){delete this._dialogDoneHandlerDelegate;
}if(this._dataBoundHandlerDelegate){delete this._dataBoundHandlerDelegate;
}if(this._itemDataBoundHandlerDelegate){delete this._itemDataBoundHandlerDelegate;
}if(this._addSettingsHandlerDelegate){delete this._addSettingsHandlerDelegate;
}if(this._loadHandlerDelegate){delete this._loadHandlerDelegate;
}delete this._getLatestPublishingPoitDefinitionDelegate;
if(this._pagePickerClosedDelegate){delete this._pagePickerClosedDelegate;
}},_loadHandler:function(){if(typeof GetDialogManager=="function"){GetDialogManager().blacklistWindowManager(this._windowManager);
}this._dialogWindow=this._windowManager.getWindowByName("settingsDialog");
this._dialogWindow.add_pageLoad(this._windowLoadHandlerDelegate);
},set_fieldChangeNotifier:function(notifier){this._fieldChangeNotifier=notifier;
},get_existingPipesBinder:function(){return this._existingPipesBinder;
},set_existingPipesBinder:function(value){this._existingPipesBinder=value;
},get_windowManager:function(){return this._windowManager;
},set_windowManager:function(value){this._windowManager=value;
},get_backLinksPagePicker:function(){return this._backLinksPagePicker;
},set_backLinksPagePicker:function(value){this._backLinksPagePicker=value;
},get_disableRemoving:function(){return this._disableRemoving;
},set_disableRemoving:function(value){this._disableRemoving=value;
},get_changePipeText:function(){return this._changePipeText;
},set_changePipeText:function(value){this._changePipeText=value;
},get_disableActivation:function(){return this._disableActivation;
},set_disableActivation:function(value){this._disableActivation=value;
},get_showDefaultPipes:function(){return this._showDefaultPipes;
},set_showDefaultPipes:function(value){this._showDefaultPipes=value;
},get_showContentLocation:function(){return this._showContentLocation;
},set_showContentLocation:function(value){this._showContentLocation=value;
},get_dialogUrl:function(){return this._dialogUrl;
},set_dialogUrl:function(value){this._dialogUrl=value;
},set_value:function(value){this._value=value;
if(!value&&this._showDefaultPipes){if(this._existingPipesBinder){this._value=this._defaultPipes;
}}else{this._value=jQuery.extend(true,[],value);
}if(this._value!=null&&this._value.length>0){this._doExpandHandler();
this._existingPipesBinder.BindCollection({Items:this._value});
}},get_value:function(){return this._value;
},get_defaultPipes:function(){return this._defaultPipes;
},set_defaultPipes:function(value){this._defaultPipes=value;
},get_defaultCreatePipe:function(){return this._defaultCreatePipe;
},set_defaultCreatePipe:function(value){this._defaultCreatePipe=value;
},get_addSettingsButton:function(){return this._addSettingsButton;
},set_addSettingsButton:function(value){this._addSettingsButton=value;
},validate:function(){return true;
},_getDefaultSettings:function(pipeName,inbound){var registered;
if(inbound){registered=this._fieldChangeNotifier.get_fieldValue("RegisteredInboundPipeSettings");
}else{registered=this._fieldChangeNotifier.get_fieldValue("RegisteredOutboundPipeSettings");
}var iter=registered.length;
while(iter--){var pipe=registered[iter];
if(pipe.PipeName==pipeName){return pipe;
}}return null;
},_itemDataBoundHandler:function(sender,args){if(this._disableRemoving){var btn=args.FindControl("removeButton");
jQuery(btn).hide();
}if(this._changePipeText){var elem=args.FindControl("changeButtonLabel");
elem.innerHTML=this._changePipeText;
}if(this._disableActivation){var chck=args.FindControl("activateCheckbox");
jQuery(chck).hide();
}var loc=args.FindControl("contentLocation");
var uidesc=args.FindControl("contentUIDescription");
var pipe=args.get_dataItem();
if(!this._showContentLocation){if(pipe.ContentLocationPageID==null){jQuery(loc).hide();
}}else{if(!this._pipeNeedsPageId(args.get_dataItem().PipeName)){jQuery(loc).hide();
jQuery(uidesc).hide();
}else{if(pipe.ContentLocationPageID==null){var removeLocBtn=args.FindControl("removeContentLocationButton");
jQuery(removeLocBtn).hide();
}}var changeSettingsButton=args.FindControl("changeSettingsButton");
jQuery(changeSettingsButton).hide();
}},_dataBoundHandler:function(sender,args){var target=args.get_target();
},_itemCommandHandler:function(sender,args){switch(args.get_commandName()){case"editSettings":this._onSettingsChanged(sender,args);
break;
case"removeSettings":this._onSettingsDelete(sender,args);
break;
case"activate":var input=args.FindControl("pipeIsActive");
args.get_dataItem().IsActive=input.checked;
return true;
break;
case"editLocation":this._currentArgs=args;
var pagePicker=this.get_backLinksPagePicker();
pagePicker.set_value(null);
pagePicker.openSelectorCommand();
case"removeLocation":this._removeContentLocation(args);
}},_onSettingsChanged:function(sender,args){this._currentPipeSettings=args.get_dataItem();
this._currentPipeSettingsKey=args.get_itemIndex();
var pipeName=args.get_dataItem().PipeName;
this._openSettingsDialog(pipeName);
},_onSettingsDelete:function(sender,args){var elem=args.get_itemElement();
var itemIndex=args.get_itemIndex();
elem.parentNode.removeChild(elem);
this._value.splice(itemIndex,1);
},_windowLoadHandler:function(sender,args){var frameHandle=sender.get_contentFrame().contentWindow;
if(typeof frameHandle.showSettings=="function"){var serializedData=null;
var data=null;
if(this._currentPipeSettings.Settings){serializedData=this._currentPipeSettings.Settings;
data=Sys.Serialization.JavaScriptSerializer.deserialize(serializedData);
}var additionalData=null;
if(this._currentPipeSettings.AdditionalSettings){serializedData=this._currentPipeSettings.AdditionalSettings;
additionalData=Sys.Serialization.JavaScriptSerializer.deserialize(serializedData);
}var settingsData={pipe:this._currentPipeSettings,settings:data,additionalSettings:additionalData,getDefinitionFunc:this._getLatestPublishingPoitDefinitionDelegate};
if(settingsData.pipe[this._publishingPointNameFieldName]==null){settingsData.pipe[this._publishingPointNameFieldName]=this._fieldChangeNotifier.get_fieldValue(this._publishingPointNameFieldName);
}frameHandle.showSettings(settingsData,this._dialogDoneHandlerDelegate,this._currentPipeSettingsKey);
}},_getLatestPublishingPoitDefinition:function(){var uiData=this._fieldChangeNotifier.get_fieldValuesFromUI(this._publishingPointDefinitionFieldName);
var definition;
if(typeof uiData!="undefined"&&uiData.length>0){definition=uiData[0];
}else{definition=this._fieldChangeNotifier.get_fieldValue(this._publishingPointDefinitionFieldName);
}return definition;
},_dialogDoneHandler:function(settingsData,settingsContext){settingsData=Telerik.Sitefinity.JSON.parse(settingsData);
jQuery.extend(this._currentPipeSettings,settingsData.pipe);
var data=Sys.Serialization.JavaScriptSerializer.serialize(settingsData.settings);
this._currentPipeSettings.Settings=data;
var additionalData=Sys.Serialization.JavaScriptSerializer.serialize(settingsData.additionalSettings);
this._currentPipeSettings.AdditionalSettings=additionalData;
this._value[settingsContext]=this._currentPipeSettings;
this._currentPipeSettings=null;
this._existingPipesBinder.BindCollection({Items:this._value});
},_addSettingsHandler:function(e){var dataItem=jQuery.extend(true,{},this._defaultCreatePipe);
this._currentPipeSettings=dataItem;
this._currentPipeSettingsKey=this._value.length;
var pipeName=dataItem.PipeName;
this._openSettingsDialog("");
},_openSettingsDialog:function(pipeName){var mode=this._outBoundPipesMode?"Outbound":"Inbound";
var oWnd=this._windowManager.open(String.format("{0}?SettingsName={1}&Mode={2}",this._dialogUrl,pipeName,mode),"settingsDialog");
oWnd.set_visibleStatusbar(false);
oWnd.set_visibleTitlebar(true);
oWnd.set_modal(true);
oWnd.set_cssClass("sfpeDesigner");
oWnd.setSize(425,250);
Telerik.Sitefinity.centerWindowHorizontally(oWnd);
},_pagePickerOpenedHandler:function(sender,args){if(this._currentArgs){var currentPipe=this._currentArgs.get_dataItem();
var currentPage=currentPipe.ContentLocationPageID;
if(currentPage){sender.get_pageSelector().set_selectedItemId(currentPage);
sender.get_pageSelector().dataBind();
}}},_pagePickerClosedHandler:function(sender,args){if(this._currentArgs){if(sender.get_value()){if(sender._selectedPage){var currentPipe=this._currentArgs.get_dataItem();
var currentLocationLabel=this._currentArgs.FindControl("locationLabel");
var currentLocationLink=this._currentArgs.FindControl("locationLink");
currentLocationLabel.innerHTML=sender._selectedPage.TitlesPath;
currentLocationLink.href=sender._selectedPage.FullUrl;
currentPipe.ContentLocationPageID=sender.get_value();
currentPipe.ContentLocation=currentLocationLabel.innerHTML;
var removeLocBtn=this._currentArgs.FindControl("removeContentLocationButton");
jQuery(removeLocBtn).show();
}else{this._removeContentLocation(this._currentArgs);
}}}},_removeContentLocation:function(args){var currentPipe=args.get_dataItem();
var currentLocationLabel=args.FindControl("locationLabel");
var currentLocationLink=args.FindControl("locationLink");
currentLocationLabel.innerHTML="";
currentLocationLink.href="";
currentPipe.ContentLocationPageID=null;
currentPipe.ContentLocation="";
var removeLocBtn=args.FindControl("removeContentLocationButton");
jQuery(removeLocBtn).hide();
},_pipeNeedsPageId:function(pipeName){return pipeName!="PagePipe"&&pipeName!="DocumentSearchInboundPipe";
}};
Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField",Telerik.Sitefinity.Web.UI.Fields.FieldControl,Telerik.Sitefinity.Web.UI.IDataItemAccessField);
