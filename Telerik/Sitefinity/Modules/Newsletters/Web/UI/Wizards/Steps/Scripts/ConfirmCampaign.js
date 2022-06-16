Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps");
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign=function(element){this._saveCampaignButton=null;
this._previewCampaignButton=null;
this._sendTestButton=null;
this._sendButton=null;
this._scheduleDeliveryButton=null;
this._scheduleCampaignDialog=null;
this._discardCampaignButton=null;
this._saveCampaignDelegate=null;
this._previewCampaignDelegate=null;
this._sendTestDelegate=null;
this._sendDelegate=null;
this._scheduleDeliveryDelegate=null;
this._discardCampaignDelegate=null;
this._sendTestCallbackDelegate=null;
this._sendTestPrompt=null;
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign.initializeBase(this,[element]);
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign.prototype={initialize:function(){if(this._saveCampaignDelegate===null){this._saveCampaignDelegate=Function.createDelegate(this,this._saveCampaignHandler);
}if(this._previewCampaignDelegate===null){this._previewCampaignDelegate=Function.createDelegate(this,this._previewCampaignHandler);
}if(this._sendTestDelegate===null){this._sendTestDelegate=Function.createDelegate(this,this._sendTestHandler);
}if(this._sendDelegate===null){this._sendDelegate=Function.createDelegate(this,this._sendHandler);
}if(this._sendTestCallbackDelegate===null){this._sendTestCallbackDelegate=Function.createDelegate(this,this._sendTestCallback);
}if(this._scheduleDeliveryDelegate===null){this._scheduleDeliveryDelegate=Function.createDelegate(this,this._scheduleDeliveryHandler);
}if(this._discardCampaignDelegate===null){this._discardCampaignDelegate=Function.createDelegate(this,this._discardCampaignHandler);
}$addHandler(this._saveCampaignButton,"click",this._saveCampaignDelegate);
$addHandler(this._previewCampaignButton,"click",this._previewCampaignDelegate);
$addHandler(this._sendTestButton,"click",this._sendTestDelegate);
$addHandler(this._sendButton,"click",this._sendDelegate);
$addHandler(this._scheduleDeliveryButton,"click",this._scheduleDeliveryDelegate);
$addHandler(this._discardCampaignButton,"click",this._discardCampaignDelegate);
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign.callBaseMethod(this,"initialize");
},dispose:function(){if(this._saveCampaignDelegate){delete this._saveCampaignDelegate;
}if(this._previewCampaignDelegate){delete this._previewCampaignDelegate;
}if(this._sendTestDelegate){delete this._sendTestDelegate;
}if(this._sendDelegate){delete this._sendDelegate;
}if(this._scheduleDeliveryDelegate){delete this._scheduleDeliveryDelegate;
}if(this._discardCampaignDelegate){delete this._discardCampaignDelegate;
}Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign.callBaseMethod(this,"dispose");
},add_command:function(delegate){this.get_events().addHandler("onCommand",delegate);
},remove_command:function(delegate){this.get_events().removeHandler("onCommand",delegate);
},_commandHandler:function(commandName,commandArgument){var eventArgs={CommandName:commandName,CommandArgument:commandArgument};
var h=this.get_events().getHandler("onCommand");
if(h){h(this,eventArgs);
}return eventArgs;
},_saveCampaignHandler:function(sender,args){this._commandHandler("saveCampaign");
},_previewCampaignHandler:function(sender,args){this._commandHandler("previewCampaign");
},_sendTestHandler:function(sender,args){this.get_sendTestPrompt().set_inputText("");
this.get_sendTestPrompt().show_prompt(null,null,this._sendTestCallbackDelegate);
},_sendTestCallback:function(sender,args){if(args.get_commandName()=="send"){this._commandHandler("sendTest",sender.get_inputText());
}},_sendHandler:function(sender,args){this._commandHandler("send");
},_scheduleDeliveryHandler:function(sender,args){this._scheduleCampaignDialog.add_pageLoad(Function.createDelegate(this,this._onScheduleCampaignDialogLoaded));
this._scheduleCampaignDialog.show();
this._scheduleCampaignDialog.add_close(Function.createDelegate(this,this._scheduleCampaignDialogCloses));
Telerik.Sitefinity.centerWindowHorizontally(this._scheduleCampaignDialog);
},_onScheduleCampaignDialogLoaded:function(sender,args){scheduleCampaignDialog.remove_pageLoad(onScheduleCampaignDialogLoaded);
scheduleCampaignDialog.add_show(Function.createDelegate(this,this._scheduleCampaignDialogShowed));
},_scheduleCampaignDialogShowed:function(sender,args){this._scheduleCampaignDialog.get_contentFrame().contentWindow.reset();
},_scheduleCampaignDialogCloses:function(sender,args){this._commandHandler("scheduleDelivery",args.get_argument());
},_discardCampaignHandler:function(sender,args){this._commandHandler("discardCampaign");
},get_saveCampaignButton:function(){return this._saveCampaignButton;
},set_saveCampaignButton:function(value){this._saveCampaignButton=value;
},get_previewCampaignButton:function(){return this._previewCampaignButton;
},set_previewCampaignButton:function(value){this._previewCampaignButton=value;
},get_sendTestButton:function(){return this._sendTestButton;
},set_sendTestButton:function(value){this._sendTestButton=value;
},get_sendButton:function(){return this._sendButton;
},set_sendButton:function(value){this._sendButton=value;
},get_scheduleDeliveryButton:function(){return this._scheduleDeliveryButton;
},set_scheduleDeliveryButton:function(value){this._scheduleDeliveryButton=value;
},get_discardCampaignButton:function(){return this._discardCampaignButton;
},set_discardCampaignButton:function(value){this._discardCampaignButton=value;
},get_sendTestPrompt:function(){return this._sendTestPrompt;
},set_sendTestPrompt:function(value){this._sendTestPrompt=value;
},get_scheduleCampaignDialog:function(){return this._scheduleCampaignDialog;
},set_scheduleCampaignDialog:function(value){this._scheduleCampaignDialog=value;
}};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign.registerClass("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.ConfirmCampaign",Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepControl);
