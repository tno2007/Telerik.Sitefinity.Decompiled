Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");
Telerik.Sitefinity.Workflow.UI.WorkflowMenu=function(element){Telerik.Sitefinity.Workflow.UI.WorkflowMenu.initializeBase(this,[element]);
this._actionsContainerId=null;
this._actionsContainer=null;
this._secondaryActionsContainer=null;
this._otherActionsMenuId=null;
this._otherActionsMenu=null;
this._workflowServiceUrl=null;
this._workflowItemId=null;
this._workflowItemState=null;
this._workflowCommandButtonDelegate=null;
this._workflowCommandMenuDelegate=null;
this._workflowMenuMouseOverDelegate=null;
this._startProcessingDelegate=null;
this._endProcessingDelegate=null;
this._bindWorkflowVisualsFailureDelegate=null;
this._bindWorkflowVisualsSuccessDelegate=null;
this._messageWorkflowFailureDelegate=null;
this._messageWorkflowSuccessDelegate=null;
this._deleteItemsDelegate=null;
this._commandDelegate=null;
this._noWorkflowActionsDelegate=null;
this._workflowVisualElements=null;
this._dialogBaseUrl=null;
this._BUTTON=0;
this._LINK=1;
this._LABEL=2;
this._loadingView=null;
this._loadingViewClone=null;
this._returnUrl=null;
this._promptDialogNamesJson=null;
this._promptDialogCommandsJson=null;
this._promptDialogNames=null;
this._promptDialogCommands=null;
this._promptDialogs=[];
this._itemType=null;
this._providerName=null;
this._itemId=null;
this._workflowWindowCloseDialog=null;
this._itemContext=null;
this._workflowWindowOpenDialogDelegate=null;
this._dataItem=null;
this._preventDeleteParentItem=null;
this._closeOnSuccess=false;
this._messageControl=null;
this._initialBinding=true;
this._lastWorkflowItemId=null;
this._contentWorkflowStatusInfoField=null;
this._cancelLink=null;
this._widgetDomElements=new Array();
this._recycleBinEnabled=null;
this._sendToRecycleBinSingleConfirmationMessage=null;
this._isMultilingual=false;
this._showMoreActions=true;
this._backendUICulture=null;
this._contentCulture=null;
this._workflowWarningDelegate=null;
this._showMessage=null;
this._validationFunction=null;
this._noteValue=null;
this._validateItemKey=null;
this._validateItem=null;
};
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.prototype={initialize:function(){this._workflowCommandButtonDelegate=Function.createDelegate(this,this._handleWorkflowButtonCommand);
this._workflowCommandMenuDelegate=Function.createDelegate(this,this._handleWorkflowMenuCommand);
this._workflowMenuMouseOverDelegate=Function.createDelegate(this,this._handleWorkflowMenuMouseOver);
this._startProcessingDelegate=Function.createDelegate(this,this._startProcessingHandler);
this._endProcessingDelegate=Function.createDelegate(this,this._endProcessingHandler);
this._workflowWindowCloseDialog=Function.createDelegate(this,this._endWorkflowWindowClosed);
this._bindWorkflowVisualsFailureDelegate=Function.createDelegate(this,this._bindWorkflowVisuals_Failure);
this._bindWorkflowVisualsSuccessDelegate=Function.createDelegate(this,this._bindWorkflowVisuals_Success);
this._messageWorkflowFailureDelegate=Function.createDelegate(this,this._messageWorkflow_Failure);
if(!this._messageWorkflowSuccessDelegate){this._messageWorkflowSuccessDelegate=Function.createDelegate(this,this._messageWorkflow_Success);
}this._workflowWindowOpenDialogDelegate=Function.createDelegate(this,this._handleWorkflowWindowOpenDialog);
this._noWorkflowActionsDelegate=Function.createDelegate(this,this._handleNoWorkflowActions);
this._deleteItemsDelegate=Function.createDelegate(this,this._deleteItemsFinal);
this._workflowWarningDelegate=Function.createDelegate(this,this._workflowWarningHandler);
this.get_workflowWarningDialog().add_command(this._workflowWarningDelegate);
this._promptDialogNames=Sys.Serialization.JavaScriptSerializer.deserialize(this._promptDialogNamesJson);
this._promptDialogCommands=Sys.Serialization.JavaScriptSerializer.deserialize(this._promptDialogCommandsJson);
this.get_otherActionsMenu().add_itemClicked(this._workflowCommandMenuDelegate);
this.get_otherActionsMenu().add_mouseOver(this._workflowMenuMouseOverDelegate);
this._beforeRequest();
this._showMessage=true;
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.callBaseMethod(this,"initialize");
},dispose:function(){if(this._workflowCommandButtonDelegate){delete this._workflowCommandButtonDelegate;
}if(this._workflowMenuMouseOverDelegate){delete this._workflowMenuMouseOverDelegate;
}if(this._workflowCommandMenuDelegate){delete this._workflowCommandMenuDelegate;
}if(this._workflowWindowCloseDialog){delete this._workflowWindowCloseDialog;
}if(this._bindWorkflowVisualsFailureDelegate){delete this._bindWorkflowVisualsFailureDelegate;
}if(this._bindWorkflowVisualsSuccessDelegate){delete this._bindWorkflowVisualsSuccessDelegate;
}if(this._messageWorkflowFailureDelegate){delete this._messageWorkflowFailureDelegate;
}if(this._messageWorkflowSuccessDelegate){delete this._messageWorkflowSuccessDelegate;
}if(this._workflowWindowOpenDialogDelegate){delete this._workflowWindowOpenDialogDelegate;
}if(this._startProcessingDelegate){delete this._startProcessingDelegate;
}if(this._endProcessingDelegate){delete this._endProcessingDelegate;
}if(this._workflowWarningDelegate){if(this.get_workflowWarningDialog()){this.get_workflowWarningDialog().remove_command(this._workflowWarningDelegate);
}delete this._workflowWarningDelegate;
}if(this._noWorkflowActionsDelegate){delete this._noWorkflowActionsDelegate;
}Telerik.Sitefinity.Workflow.UI.WorkflowMenu.callBaseMethod(this,"dispose");
},add_command:function(delegate){this.get_events().addHandler("onCommand",delegate);
},remove_command:function(delegate){this.get_events().removeHandler("onCommand",delegate);
},add_onStartProcessing:function(delegate){this.get_events().addHandler("onStartProcessing",delegate);
},remove_onStartProcessing:function(delegate){this.get_events().removeHandler("onStartProcessing",delegate);
},add_onEndProcessing:function(delegate){this.get_events().addHandler("onEndProcessing",delegate);
},remove_onEndProcessing:function(delegate){this.get_events().removeHandler("onEndProcessing",delegate);
},add_onNoWorkflowActions:function(delegate){this.get_events().addHandler("onNoWorkflowActions",delegate);
},remove_onNoWorkflowActions:function(delegate){this.get_events().removeHandler("onNoWorkflowActions",delegate);
},add_onWorkflowFailure:function(delegate){this.get_events().addHandler("onWorkflowFailure",delegate);
},remove_onWorkflowFailure:function(delegate){this.get_events().removeHandler("onWorkflowFailure",delegate);
},getAllWidgets:function(){return null;
},getWidgetByName:function(name){return null;
},bindWorkflowVisuals:function(itemType,providerName,itemId,dataItem,validationFunction){if(!itemId&&dataItem){itemId=dataItem.OriginalContentId;
}this.set_itemType(itemType);
this.set_providerName(providerName);
this.set_itemId(itemId);
this.set_dataItem(dataItem);
if(this._lastWorkflowItemId){this._initialBinding=itemId!=this._lastWorkflowItemId;
}else{this._initialBinding=true;
}this._lastWorkflowItemId=itemId;
this._showMessage=false;
this._bindWorkflowVisuals();
this._validationFunction=validationFunction;
this._noteValue=null;
},operationClosesDialog:function(workflowOperationName){var elements=this._workflowVisualElements;
for(var i=0,length=elements.length;
i<length;
i++){var visualElement=elements[i];
if(visualElement.OperationName==workflowOperationName){return visualElement.ClosesForm;
}}return true;
},rebindWorkflowVisuals:function(lastModifiedItemContext){var lastModifiedItem=lastModifiedItemContext.Item;
this.set_workflowItemState(lastModifiedItem.ApprovalWorkflowState.Value);
this.set_itemId(lastModifiedItem.Id);
this.set_dataItem(lastModifiedItem);
this._showMessage=true;
this._bindWorkflowVisuals();
},hide:function(){jQuery(this.get_element()).hide();
},show:function(){jQuery(this.get_element()).show();
},cancelCommand:function(){this._afterRequest();
},getPromptDialogByCommandName:function(commandName){if(commandName in this._promptDialogCommands){return this.getPromptDialogByName(this._promptDialogCommands[commandName]);
}else{return null;
}},getPromptDialogByName:function(name){if(name in this._promptDialogNames){return this._getPromptDialogById(this._promptDialogNames[name]);
}else{return null;
}},showPromptDialogByName:function(name,title,message,handlerFunction){var dialog=this.getPromptDialogByName(name);
dialog.show_prompt(title,message,handlerFunction);
},_getPromptDialogById:function(id){if(!this._promptDialogs[id]){this._promptDialogs[id]=$find(id);
}return this._promptDialogs[id];
},_bindWorkflowVisuals:function(){this.show();
var clientManager=new Telerik.Sitefinity.Data.ClientManager();
var urlParams=[];
urlParams.itemType=this.get_itemType();
urlParams.providerName=this.get_providerName();
if(this._showMoreActions!==undefined){urlParams.showMoreActions=this._showMoreActions;
}if(this._isMultilingual){clientManager.set_uiCulture(this._backendUICulture);
urlParams.itemCulture=this.get_contentCulture();
}var keys=[this.get_itemId()];
this._beforeRequest();
clientManager.InvokeGet(this._workflowServiceUrl,urlParams,keys,this._bindWorkflowVisualsSuccessDelegate,this._bindWorkflowVisualsFailureDelegate,this);
},_sendMessageWorkflow:function(workflowOperation,contextBag){var clientManager=new Telerik.Sitefinity.Data.ClientManager();
var urlParams=[];
urlParams.itemType=this.get_itemType();
urlParams.providerName=this.get_providerName();
urlParams.workflowOperation=workflowOperation;
var keys=["MessageWorkflow",this.get_itemId()];
if(this._isMultilingual){clientManager.set_uiCulture(this.get_contentCulture());
}if(contextBag==null){contextBag=[];
}if(this._validateItem){contextBag.push({key:this._validateItemKey,value:"true"});
}this._beforeRequest(false,workflowOperation);
contextBag=Telerik.Sitefinity.fixArray(contextBag);
clientManager.InvokePut(this._workflowServiceUrl,urlParams,keys,contextBag,this._messageWorkflowSuccessDelegate,this._messageWorkflowFailureDelegate,this,null,null,workflowOperation);
},_buildMenu:function(items,workflowDefinition){this._workflowVisualElements=items;
this._resetVisuals();
var mainActions=[];
var secondaryActions=[];
var otherActions=[];
var hidePreviewAction=(this.get_itemId()=="00000000-0000-0000-0000-000000000000");
for(var i=0,length=items.length;
i<length;
i++){var visualElementDefinition=items[i];
if(hidePreviewAction&&visualElementDefinition.OperationName=="Preview"){continue;
}switch(visualElementDefinition.DecisionType){case"MainAction":mainActions.push(visualElementDefinition);
break;
case"SecondaryActions":secondaryActions.push(visualElementDefinition);
break;
case"OtherActions":otherActions.push(visualElementDefinition);
break;
default:alert("Visual element type not supported! "+visualElementDefinition.DecisionType);
}}if(mainActions.length>0){this._buildActions(mainActions,workflowDefinition);
}if(secondaryActions.length>0){this._buildActions(secondaryActions,workflowDefinition,this.get_secondaryActionsContainer());
}this._buildOtherActions(otherActions);
},_buildActions:function(actions,workflowDefinition,actionsContainer){if(actionsContainer==null){actionsContainer=this.get_actionsContainer();
}for(var i=0,length=actions.length;
i<length;
i++){var actionDefinition=actions[i];
var visualElement=this._generateVisualElement(actionDefinition);
if(workflowDefinition!=null){jQuery(visualElement).attr("Title",actionDefinition.Title+" ("+workflowDefinition.Title+")");
}else{jQuery(visualElement).attr("Title",actionDefinition.Title);
}actionsContainer.appendChild(visualElement);
this._widgetDomElements.push(visualElement);
this._calculateWorkflowButtonId(visualElement,actionDefinition.OperationName);
}},_buildOtherActions:function(actions){var rootItem=this.get_otherActionsMenu().get_allItems()[0];
for(var i=0,length=actions.length;
i<length;
i++){var actionDefinition=actions[i];
var childItem=new Telerik.Web.UI.RadMenuItem();
childItem.set_text(actionDefinition.Title);
childItem.set_value(actionDefinition.OperationName);
rootItem.get_items().add(childItem);
}if(rootItem.get_items().get_count()<=0){this.get_otherActionsMenu().set_visible(false);
}else{this.get_otherActionsMenu().set_visible(true);
}},_bindWorkflowVisuals_Success:function(caller,result){this._resetVisuals();
if(result!=null&&result.Items!=null){var items=result.Items;
var definition=result.WorkflowDefinition;
this._buildMenu(items,definition);
this._raiseCommandWithArgs(new Telerik.Sitefinity.CommandEventArgs("buildMenu",result));
if(result.Context){if(result.Context.CommandToExecute&&result.Context.OperationName){this._raiseCommand(result.Context.CommandToExecute,result.Context.OperationName);
}if(!caller._initialBinding&&this.get_messageControl()){if(this._showMessage){if(result.Context.PositiveMessage){this.get_messageControl().showPositiveMessage(result.Context.PositiveMessage);
}if(result.Context.NegativeMessage){this.get_messageControl().showNegativeMessage(result.Context.NegativeMessage);
}}else{this.get_messageControl().hide();
}}if(this.get_contentWorkflowStatusInfoField()){this.get_contentWorkflowStatusInfoField().set_dataItemContext(result.Context);
}}}else{this.get_otherActionsMenu().set_visible(false);
this._raiseNoWorkflowActions(null);
}this._afterRequest();
this._initialBinding=false;
},_resetVisuals:function(){for(var i=0,length=this._widgetDomElements.length;
i<length;
i++){$clearHandlers(this._widgetDomElements[i]);
this._widgetDomElements[i]=null;
}this._widgetDomElements.length=0;
jQuery(this.get_actionsContainer()).empty();
jQuery(this.get_secondaryActionsContainer()).empty();
var rootItem=this.get_otherActionsMenu().get_allItems()[0];
rootItem.get_items().clear();
},_bindWorkflowVisuals_Failure:function(sender,result){this._afterRequest();
alert(result.Detail);
},_messageWorkflow_Success:function(caller,data,request,context){this._afterRequest(false,context);
this._raiseCommand("reportDirty",null);
if(this._closeOnSuccess){if(this.get_returnUrl()){window.location=this.get_returnUrl();
}else{dialogBase.closeAndRebind();
}}else{this._showMessage=true;
this._bindWorkflowVisuals();
}},_messageWorkflow_Failure:function(sender,result){this._afterRequest();
var args=new Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs(sender,result);
this._raiseOnWorkflowFailure(args);
if(!args.get_cancel()){if(sender.Detail){alert(sender.Detail);
}else{alert("Failed workflow operation");
}}},_generateVisualElement:function(workflowVisualElement){var visualElement=null;
switch(workflowVisualElement.VisualType){case this._BUTTON:visualElement=document.createElement("input");
$(visualElement).addClass(workflowVisualElement.CssClass);
if(workflowVisualElement.OperationName!=null){$(visualElement).attr("rel",workflowVisualElement.OperationName);
}visualElement.setAttribute("type","button");
visualElement.setAttribute("value",workflowVisualElement.Title);
$addHandler(visualElement,"click",this._workflowCommandButtonDelegate,true);
break;
case this._LINK:visualElement=document.createElement("a");
$(visualElement).addClass(workflowVisualElement.CssClass);
if(workflowVisualElement.DecisionType==="MainAction"){$(visualElement).addClass("sfSave");
}visualElement.innerHTML='<span class="sfLinkBtnIn">'+workflowVisualElement.Title+"</span>";
if(workflowVisualElement.OperationName!=null){$(visualElement).attr("rel",workflowVisualElement.OperationName);
}$addHandler(visualElement,"click",this._workflowCommandButtonDelegate,true);
break;
case this._LABEL:visualElement=document.createElement("span");
visualElement.innerHTML=workflowVisualElement.Title;
break;
}return visualElement;
},_calculateWorkflowButtonId:function(element,idSuffix){if(idSuffix){jElement=jQuery(element);
var parentId="";
if(jElement.parent()&&jElement.parent().attr("id")){parentId=jElement.parent().attr("id")+"_";
}jElement.attr("id",parentId+idSuffix);
}},_handleWorkflowButtonCommand:function(sender,args){var relElement=jQuery(sender.target).attr("rel")?sender.target:sender.target.parentNode;
var operationName=jQuery(relElement).attr("rel");
var workflowVisualElement;
for(var i=0,length=this._workflowVisualElements.length;
i<length;
i++){if(this._workflowVisualElements[i].OperationName==operationName){workflowVisualElement=this._workflowVisualElements[i];
break;
}}this._excecuteWorkflowCommand(workflowVisualElement,operationName);
return false;
},_handleWorkflowMenuCommand:function(sender,args){var operationName=args.get_item().get_value();
if(!operationName){return;
}sender.close();
var workflowVisualElement;
for(var i=0,length=this._workflowVisualElements.length;
i<length;
i++){if(this._workflowVisualElements[i].OperationName==operationName){workflowVisualElement=this._workflowVisualElements[i];
break;
}}if(operationName=="Delete"){if(this._preventDeleteParentItem){var cannotDeleteParentDialog=this.getPromptDialogByName("cannotDeleteParentPageDialog");
if(cannotDeleteParentDialog){cannotDeleteParentDialog.show_prompt("","");
return;
}}var showAdvancedDialog=false;
var basicDialog=this.getPromptDialogByName("confirmDeleteSingle");
var multipleItemsDeleteCommandButtonId=basicDialog._commands[1].ButtonClientId;
var multipleItemsDeleteButton=jQuery("#"+multipleItemsDeleteCommandButtonId);
multipleItemsDeleteButton.attr("style","display: none !important");
if(this._isMultilingual){var dataItem=this.get_dataItem();
if(dataItem.AvailableLanguages){var availableLanguagesCount=dataItem.AvailableLanguages.length;
if(jQuery.inArray("",dataItem.AvailableLanguages)>-1){availableLanguagesCount--;
}showAdvancedDialog=(availableLanguagesCount>1);
}else{showAdvancedDialog=false;
}}var deleteMessage="";
if(this.get_recycleBinEnabled()){deleteMessage=this.get_sendToRecycleBinSingleConfirmationMessage();
}if(showAdvancedDialog==true){var dialog=this.getPromptDialogByName("confirmDelete");
var singleLanguageDeleteCommand=null;
for(var i=0;
i<dialog._commands.length;
i++){if(dialog._commands[i].CommandName==="language"){singleLanguageDeleteCommand=dialog._commands[i];
break;
}}var commandButtonId=singleLanguageDeleteCommand.ButtonClientId;
var button=$("#"+commandButtonId).get(0);
var textContainer=button.children[0];
if(!this._confirmDeleteMessage){this._confirmDeleteMessage=textContainer.innerHTML;
}textContainer.innerHTML=String.format(this._confirmDeleteMessage,this.get_contentCulture().toUpperCase());
dialog.show_prompt("",deleteMessage,this._deleteItemsDelegate,workflowVisualElement);
return true;
}else{var dialog=this.getPromptDialogByName("confirmDeleteSingle");
dialog.show_prompt("",deleteMessage,this._deleteItemsDelegate,workflowVisualElement);
}}else{this._excecuteWorkflowCommand(workflowVisualElement,operationName);
}return false;
},_workflowWarningHandler:function(sender,args){var commandName=args.get_commandName();
if(commandName=="cancel"){return;
}var commandArgs=args.get_commandArgument();
var workflowVisualElement=commandArgs.WorkflowVisualElement;
var operationName=commandArgs.OperationName;
this._messageWorkflow(workflowVisualElement,operationName);
},_excecuteWorkflowCommand:function(workflowVisualElement,operationName){if(workflowVisualElement.WarningMessage){this.get_workflowWarningDialog().show_prompt("",workflowVisualElement.WarningMessage,null,{WorkflowVisualElement:workflowVisualElement,OperationName:operationName});
}else{this._messageWorkflow(workflowVisualElement,operationName);
}},_deleteItemsFinal:function(sender,args){var commandName=args.get_commandName();
if(commandName=="cancel"){return;
}var workflowVisualElement=args.get_commandArgument();
var deleteCurrentLanguageOnly=(commandName=="language");
var lang=null;
if(deleteCurrentLanguageOnly==true){lang=this.get_contentCulture();
}var checkRelatingData=args.get_checkRelatingData();
this._messageWorkflow(workflowVisualElement,"Delete",lang,checkRelatingData);
},_handleWorkflowMenuMouseOver:function(sender,args){if(args.get_item().get_parent()==sender){sender.set_clicked(false);
}},_messageWorkflow:function(workflowVisualElement,operationName,language,checkRelatingData){this._closeOnSuccess=workflowVisualElement.ClosesForm;
if(workflowVisualElement.ArgumentDialogName!=null&&workflowVisualElement.ArgumentDialogName!="undefined"){if(workflowVisualElement.PersistOnDecision&&this._validationFunction&&!this._validationFunction()){return;
}var args="?operationName="+operationName+"&providerName="+this.get_providerName()+"&itemType="+this.get_itemType();
args+="&PersistOnDecision="+workflowVisualElement.PersistOnDecision;
if(workflowVisualElement.ContentCommandName){args+="&contentCommandName="+workflowVisualElement.ContentCommandName;
}if(this.get_itemId()!=Telerik.Sitefinity.getEmptyGuid()){args+="&itemId"+this.get_itemId();
}var dialogName=workflowVisualElement.ArgumentDialogName;
if(dialogName!=null&&dialogName!=""){var dlg=this._openDialog(this.get_dialogBaseUrl()+dialogName+args,dialogName);
if(dlg!=null){dlg._sfProperties=workflowVisualElement.Parameters;
dlg._sfNoteValue=this._noteValue;
dlg._sfValidationEnabled=typeof this._validationFunction!=="undefined";
dlg.add_close(this._workflowWindowCloseDialog);
dlg.add_show(this._workflowWindowOpenDialogDelegate);
dlg.add_pageLoad(this._workflowWindowOpenDialogDelegate);
}}}else{if(workflowVisualElement.RunAsUICommand){this._raiseCommand(workflowVisualElement.ContentCommandName,"");
}else{if(workflowVisualElement.ContentCommandName&&workflowVisualElement.PersistOnDecision){this._beforeRequest();
this._raiseCommand(workflowVisualElement.ContentCommandName,operationName);
}else{var contextBag=null;
if(language){contextBag=new Array({key:"Language",value:language});
}if(checkRelatingData){if(!contextBag){contextBag=new Array();
}contextBag.push({key:"CheckRelatingData",value:checkRelatingData});
}this._sendMessageWorkflow(operationName,contextBag);
}}}},getParameterByName:function(name){name=name.replace(/[\[]/,"\\[").replace(/[\]]/,"\\]");
var regexS="[\\?&]"+name+"=([^&#]*)";
var regex=new RegExp(regexS);
var results=regex.exec(window.location.search);
if(results==null){return"";
}else{return decodeURIComponent(results[1].replace(/\+/g," "));
}},_openDialog:function(url,dialogName){var dialog=radopen(url,dialogName);
if(dialog){dialog.set_modal(true);
Telerik.Sitefinity.centerWindowHorizontally(dialog);
var size=this._getDialogSize(dialogName);
dialog.set_width(size.Width);
dialog.set_height(size.Height);
dialog.set_visibleStatusbar(false);
dialog.set_autoSizeBehaviors(5);
dialog.set_showContentDuringLoad(false);
dialog.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
}return dialog;
},_getDialogSize:function(dialogName){var dialogSize={Height:300,Width:425};
if(dialogName=="WorkflowRejectDialog"){dialogSize.Height=400;
}if(dialogName=="WorkflowSendForApprovalDialog"){dialogSize.Height=400;
}return dialogSize;
},_raiseCommand:function(contentCommandName,operationName,contextBag){var eventArgs=new Telerik.Sitefinity.CommandEventArgs(contentCommandName,operationName,contextBag);
return this._raiseCommandWithArgs(eventArgs);
},_raiseCommandWithArgs:function(eventArgs){var h=this.get_events().getHandler("onCommand");
if(h){h(this,eventArgs);
}return eventArgs;
},_raiseNoWorkflowActions:function(eventArgs){var h=this.get_events().getHandler("onNoWorkflowActions");
if(h){h(this,eventArgs);
}},_raiseOnWorkflowFailure:function(eventArgs){var h=this.get_events().getHandler("onWorkflowFailure");
if(h){h(this,eventArgs);
}},_startProcessingHandler:function(sender,args){this._beforeRequest();
},_endProcessingHandler:function(sender,args){this._afterRequest();
},_endWorkflowWindowClosed:function(sender,args){if(sender!=null||sender!==undefined){sender.remove_close(this._workflowWindowCloseDialog);
}var args=args.get_argument();
var persist=sender.AjaxDialog.getQueryValue("PersistOnDecision",true)==="true";
if(args){var operationName=args.get_operationName();
var commandName=args.get_commandName();
var executeCommandOnClose=args.get_executeCommandOnClose();
if(typeof args.__workflowScheduleDialog!="undefined"&&args.__workflowScheduleDialog==true){this._dataItem.PublicationDate.constructor=Date;
if(this._dataItem.ExpirationDate!=null){this._dataItem.ExpirationDate.constructor=Date;
}}if(typeof args._noteValue!="undefined"&&args._noteValue){this._noteValue=args._noteValue;
}if(persist&&!commandName){commandName="save";
}var contextBag=args.get_contextBag();
if(executeCommandOnClose){this._raiseCommand(commandName,operationName,contextBag);
}else{if(operationName){this._sendMessageWorkflow(operationName,contextBag);
}else{alert("No content command name or workflow operation specified!");
}}}},_beforeRequest:function(preventRaise,workflowOperation){if(this._loadingViewClone!=null){return;
}this._loadingViewClone=this._loadingView.clone();
var jba=jQuery(this.get_element());
jba.append(this._loadingViewClone);
jba.children().hide();
this._loadingViewClone.show();
if(!preventRaise){this._raiseCommand("beforeWMRequest",workflowOperation);
}},_afterRequest:function(preventRaise,workflowOperation){if(this._loadingViewClone){this._loadingViewClone.remove();
this._loadingViewClone=null;
jQuery(this.get_element()).children().show();
}this._loadingView.hide();
if(!preventRaise){this._raiseCommand("afterWMRequest",workflowOperation);
}},_handleWorkflowWindowOpenDialog:function(sender,args){if(sender!=null||sender!==undefined){sender.remove_show(this._workflowWindowOpenDialogDelegate);
sender.remove_pageLoad(this._workflowWindowOpenDialogDelegate);
}if(sender.AjaxDialog){if(sender.AjaxDialog.set_dataItem){sender.AjaxDialog.set_dataItem(this.get_dataItem());
}}},_onCommand:function(commandName,commandArgument){if(commandName=="beforeWMRequest"){this._beforeRequest(true);
}else{if(commandName=="afterWMRequest"){this._afterRequest(true);
}else{if(commandName=="buildMenu"&&commandArgument){this._buildMenu(commandArgument.Items,commandArgument.WorkflowDefinition);
}}}},get_actionsContainerId:function(){return this._actionsContainerId;
},set_actionsContainerId:function(value){this._actionsContainerId=value;
},get_secondaryActionsContainer:function(){return this._secondaryActionsContainer;
},set_secondaryActionsContainer:function(value){this._secondaryActionsContainer=value;
},get_actionsContainer:function(){if(this._actionsContainer==null){this._actionsContainer=$find(this.get_actionsContainerId());
}return this._actionsContainer;
},set_actionsContainer:function(value){this._actionsContainer=value;
},get_otherActionsMenuId:function(){return this._otherActionsMenuId;
},set_otherActionsMenuId:function(value){this._otherActionsMenuId=value;
},get_otherActionsMenu:function(){if(this._otherActionsMenu==null){this._otherActionsMenu=$find(this.get_otherActionsMenuId());
}return this._otherActionsMenu;
},set_otherActionsMenu:function(value){this._otherActionsMenu=value;
},get_workflowServiceUrl:function(){return this._workflowServiceUrl;
},set_workflowServiceUrl:function(value){this._workflowServiceUrl=value;
},get_dialogBaseUrl:function(){return this._dialogBaseUrl;
},set_dialogBaseUrl:function(value){this._dialogBaseUrl=value;
},get_workflowItemId:function(){return this._workflowItemId;
},set_workflowItemId:function(value){this._workflowItemId=value;
},get_workflowItemState:function(){return this._workflowItemState;
},set_workflowItemState:function(value){this._workflowItemState=value;
},get_itemType:function(){return this._itemType;
},set_itemType:function(value){this._itemType=value;
},get_providerName:function(){return this._providerName;
},set_providerName:function(value){this._providerName=value;
},get_itemId:function(){return this._itemId;
},set_itemId:function(value){this._itemId=value;
},get_loadingView:function(){return this._loadingView;
},set_loadingView:function(value){this._loadingView=jQuery(value);
},set_dataItem:function(value){this._dataItem=value;
},get_dataItem:function(value){return this._dataItem;
},get_returnUrl:function(){return this._returnUrl;
},set_returnUrl:function(value){this._returnUrl=value;
},get_messageControl:function(){return this._messageControl;
},set_messageControl:function(value){this._messageControl=value;
},get_contentWorkflowStatusInfoField:function(){return this._contentWorkflowStatusInfoField;
},set_contentWorkflowStatusInfoField:function(value){this._contentWorkflowStatusInfoField=value;
},get_cancelLink:function(){return this._cancelLink;
},set_cancelLink:function(value){this._cancelLink=value;
},set_contentCulture:function(value){this._contentCulture=value;
},get_contentCulture:function(){return this._contentCulture;
},get_workflowVisualElements:function(){return this._workflowVisualElements;
},set_workflowWarningDialog:function(value){this._workflowWarningDialog=value;
},get_workflowWarningDialog:function(){return this._workflowWarningDialog;
},set_preventDeleteParentItem:function(value){this._preventDeleteParentItem=value;
},get_showMoreActions:function(){return this._showMoreActions;
},set_showMoreActions:function(value){this._showMoreActions=value;
},get_recycleBinEnabled:function(){return this._recycleBinEnabled;
},set_recycleBinEnabled:function(value){this._recycleBinEnabled=value;
},get_sendToRecycleBinSingleConfirmationMessage:function(){return this._sendToRecycleBinSingleConfirmationMessage;
},set_sendToRecycleBinSingleConfirmationMessage:function(value){this._sendToRecycleBinSingleConfirmationMessage=value;
},};
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.registerClass("Telerik.Sitefinity.Workflow.UI.WorkflowMenu",Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar);
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs=function(sender,result){this._sender=sender;
this._result=result;
this._cancel=false;
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs.initializeBase(this);
};
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs.prototype={initialize:function(){Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs.callBaseMethod(this,"dispose");
},get_sender:function(){return this._sender;
},get_result:function(){return this._result;
},get_cancel:function(){return this._cancel;
},set_cancel:function(value){this._cancel=value;
}};
Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs.registerClass("Telerik.Sitefinity.Workflow.UI.WorkflowMenu.WorkflowFailureEventArgs",Sys.CancelEventArgs);
