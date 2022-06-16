Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");
Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor=function(element){Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor.initializeBase(this,[element]);
this._confirmationRadioButtons=null;
this._restrictionsChoiceField=null;
this._successMessageTextField=null;
this._labelPlacementChoiceField=null;
this._redirectUrlTextField=null;
this._form=null;
this._currentLanguage=null;
this._serviceUrl=null;
this._showMessageRadioButton=null;
this._redirectRadioButton=null;
this._zoneEditorId=null;
this._notificationsCheckboxControl=null;
this._emailListTextField=null;
this._currentUserEmail=null;
this._userEmail=null;
this._formConfirmationMessageTemplateSettings=null;
this._newFormResponseMessageTemplateSettings=null;
this._defaultSuccessMessage="";
this._verifiedEmails=[];
this._submitActionsMap={};
this._formLabelPlacementMap={};
this._submitRestrictionMap={};
this._confirmationClickDelegate=null;
this._labelPlacementValueChangedDelegate=null;
this._notificationsValueChangedDelegate=null;
this._emailNotificationsControlId=null;
this._emailTemplateEditor=null;
this._clientLabelManager=null;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor.prototype={initialize:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor.callBaseMethod(this,"initialize");
this._showMessageRadioButton=$get(this._confirmationRadioButtons[0]);
this._redirectRadioButton=$get(this._confirmationRadioButtons[1]);
this._confirmationClickDelegate=Function.createDelegate(this,this._confirmationClickHandler);
$addHandler(this._showMessageRadioButton,"click",this._confirmationClickDelegate,true);
$addHandler(this._redirectRadioButton,"click",this._confirmationClickDelegate,true);
var emailNotificationsControl=$find(this._emailNotificationsControlId);
if(this._labelPlacementChoiceField){this._labelPlacementValueChangedDelegate=Function.createDelegate(this,this._labelPlacementValueChangedHandler);
this._labelPlacementChoiceField.add_valueChanged(this._labelPlacementValueChangedDelegate);
}this._notificationsValueChangedDelegate=Function.createDelegate(this,emailNotificationsControl._notificationsValueChangedHandler);
this._notificationsCheckboxControl.addEventListener("change",this._notificationsValueChangedDelegate);
this._submitActionsMap=new Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap(this._submitActionsMap);
this._formLabelPlacementMap=new Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap(this._formLabelPlacementMap);
this._submitRestrictionMap=new Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap(this._submitRestrictionMap);
this._newFormResponseMessageTemplateSettings=jQuery(this.get_element()).find("#newFormResponseMessageTemplateSettings");
this._formConfirmationMessageTemplateSettings=jQuery(this.get_element()).find("#formConfirmationMessageTemplateSettings");
var that=this;
document.getElementById(this._zoneEditorId).control.add_commandSuccess(function(dock,args){if((args.CommandName==="new"||args.CommandName==="delete")&&that.get_sendConfirmationEmailCheckBox().attr("checked")){that.updateWarningMessageUI();
}});
this._notificationsCheckboxControl.addEventListener("change",function(e){if(e.target.checked){that._newFormResponseMessageTemplateSettings.show();
}else{that._newFormResponseMessageTemplateSettings.hide();
}});
this.get_sendConfirmationEmailCheckBox().change(function(){if(this.checked){that._formConfirmationMessageTemplateSettings.show();
}else{that._formConfirmationMessageTemplateSettings.hide();
}that.updateWarningMessageUI();
});
jQuery(this.get_element()).find("button[template-target]").click(function(){that.openEmailTemplateEditor(this.getAttribute("template-target"));
});
var wrapper=jQuery("#sfPageContainer .sfPublicWrapper");
if(wrapper.length===1){wrapper.addClass("sfTopLbls");
}this._refreshUI();
},dispose:function(){if(this._confirmationClickDelegate){delete this._confirmationClickDelegate;
}if(this._labelPlacementValueChangedDelegate){if(this._labelPlacementChoiceField){this._labelPlacementChoiceField.remove_valueChanged(this._labelPlacementValueChangedDelegate);
}delete this._labelPlacementValueChangedDelegate;
}if(this._notificationsValueChangedDelegate){if(this._notificationsCheckboxControl){this._notificationsCheckboxControl.removeEventListener("change",this._notificationsValueChangedDelegate);
}delete this._notificationsValueChangedDelegate;
}delete this._submitActionsMap;
delete this._formLabelPlacementMap;
delete this._submitRestrictionMap;
Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor.callBaseMethod(this,"dispose");
},getFormDescription:function(){this._applyChanges();
return this._form;
},_labelPlacementValueChangedHandler:function(sender,args){var wrapper=jQuery("#sfPageContainer .sfPublicWrapper");
if(wrapper.length===1){var placement=this._labelPlacementChoiceField.get_value();
switch(placement){case"TopAligned":wrapper.addClass("sfTopLbls");
wrapper.removeClass("sfLeftLbls");
wrapper.removeClass("sfRightLbls");
break;
case"LeftAligned":wrapper.removeClass("sfTopLbls");
wrapper.addClass("sfLeftLbls");
wrapper.removeClass("sfRightLbls");
break;
case"RightAligned":wrapper.removeClass("sfTopLbls");
wrapper.removeClass("sfLeftLbls");
wrapper.addClass("sfRightLbls");
break;
}}},_confirmationClickHandler:function(e){if(e.target===this._showMessageRadioButton){this._setSuccessMessageMode();
}else{this._setRedirectMode();
}},_setSuccessMessageMode:function(){jQuery(this._successMessageTextField.get_element()).show();
if(!this._successMessageTextField.get_value()){this._successMessageTextField.set_value(this._defaultSuccessMessage);
}jQuery(this._redirectUrlTextField.get_element()).hide();
},_setRedirectMode:function(){jQuery(this._successMessageTextField.get_element()).hide();
jQuery(this._redirectUrlTextField.get_element()).show();
},_loadServersFailedDelegate:function(error,result){alert(error.Detail);
},_applyChanges:function(){var form=this._form;
form.SubmitRestriction=this._submitRestrictionMap.getValue(this._restrictionsChoiceField.get_value());
if(this.get_labelPlacementChoiceField()){form.FormLabelPlacement=this._formLabelPlacementMap.getValue(this.get_labelPlacementChoiceField().get_value());
}if(jQuery(this._showMessageRadioButton).is(":checked")){form.SubmitAction=this._submitActionsMap.getValue("TextMessage");
form.SuccessMessage=this._successMessageTextField.get_value();
}else{form.SubmitAction=this._submitActionsMap.getValue("PageRedirect");
form.RedirectPageUrl=this._redirectUrlTextField.get_value();
}var emailNotificationsControl=$find(this._emailNotificationsControlId);
if(this._notificationsCheckboxControl.checked){var emailsToSubscribe=emailNotificationsControl._configureEmails(this._emailListTextField.get_value());
var validEmails=emailNotificationsControl._getValidEmails(emailNotificationsControl._configureEmails(this._emailListTextField.get_value()));
if(typeof emailsToSubscribe!=="undefined"){if(emailsToSubscribe.length>0){if(validEmails.length>0){form.SubscribedEmails=validEmails;
}else{form.SubscribedEmails=emailsToSubscribe;
}}else{if(form.SubscribedEmails&&form.SubscribedEmails.length>0){form.SubscribedEmails=emailsToSubscribe;
}}}}else{form.SubscribedEmails=null;
}if(this.get_sendConfirmationEmailCheckBox().attr("checked")&&!$("#sendConfirmationEmailValidationMessage").is(":visible")){form.SendConfirmationEmail=true;
}else{form.SendConfirmationEmail=false;
}},_refreshUI:function(){var form=this._form;
var emailNotificationsControl=$find(this._emailNotificationsControlId);
var showNewFormResponseMessageTemplateSettings=false;
var formatEmails=emailNotificationsControl._formatEmails(form.SubscribedEmails);
if(formatEmails!==""){this._emailListTextField.set_value(formatEmails);
this._notificationsCheckboxControl.checked=true;
showNewFormResponseMessageTemplateSettings=true;
}else{this._emailListTextField.set_visible(false);
}if(this._notificationsCheckboxControl.checked){showNewFormResponseMessageTemplateSettings=true;
}if(!showNewFormResponseMessageTemplateSettings){this._newFormResponseMessageTemplateSettings.hide();
}this._restrictionsChoiceField.set_value(this._submitRestrictionMap.getName(form.SubmitRestriction));
if(this.get_labelPlacementChoiceField()){this.get_labelPlacementChoiceField().set_value(this._formLabelPlacementMap.getName(form.FormLabelPlacement));
}if(this._submitActionsMap.getName(form.SubmitAction)==="TextMessage"){jQuery(this._showMessageRadioButton).attr("checked",true);
this._successMessageTextField.set_value(form.SuccessMessage);
this._setSuccessMessageMode();
}else{jQuery(this._redirectRadioButton).attr("checked",true);
this._redirectUrlTextField.set_value(form.RedirectPageUrl);
this._setRedirectMode();
}if(form.SendConfirmationEmail){this.get_sendConfirmationEmailCheckBox().attr("checked",true);
this.updateWarningMessageUI();
}else{this._formConfirmationMessageTemplateSettings.hide();
}this.refreshTemplatesCustomizationDateLabel();
},updateWarningMessageUI:function(){var sendConfirmationEmailValidationMessage=$("#sendConfirmationEmailValidationMessage");
var sendConfirmationEmail=this.get_sendConfirmationEmailCheckBox().attr("checked");
if(sendConfirmationEmail&&$("div[behaviourobjecttype*='Email']:visible").length===0){sendConfirmationEmailValidationMessage.show();
this._formConfirmationMessageTemplateSettings.hide();
}else{sendConfirmationEmailValidationMessage.hide();
if(sendConfirmationEmail){this._formConfirmationMessageTemplateSettings.show();
}}},refreshTemplatesCustomizationDateLabel:function(){var that=this;
jQuery(this.get_element()).find("button[template-target]").each(function(){var button=this;
that.getFormMessageTemplate(button.getAttribute("template-target"),function(dataItem){var customizedDateText=jQuery(button).next();
if(dataItem!==null&&dataItem.IsModified){customizedDateText.find("span").remove();
customizedDateText.append("<span> "+dataItem.LastModified+"</span>");
customizedDateText.show();
}else{customizedDateText.hide();
}});
});
},getFormMessageTemplate:function(template,callback){$.get(this._serviceUrl+"templates/?formId="+this._form.Id+"&language="+this._currentLanguage+"&template="+template,function(data){callback(data);
});
},openEmailTemplateEditor:function(template){var that=this;
var currentLanguage="";
if(this._currentLanguage){currentLanguage=" ("+this._currentLanguage.toUpperCase()+")";
}var options={removeVariationOnRestore:true,labels:{back:this.get_clientLabelManager().getLabel("FormsResources","TemplateEditorBackButtonTitle"),title:this.get_clientLabelManager().getLabel("FormsResources","TemplateEditorTitle")+currentLanguage,restoreDescription:this.get_clientLabelManager().getLabel("FormsResources","TemplateEditorRestoreDescription")}};
this.getFormMessageTemplate(template,function(dataItem){that.get_emailTemplateEditor().open(dataItem,function(){that.refreshTemplatesCustomizationDateLabel();
},options);
});
},get_confirmationRadioButtons:function(){return this._confirmationRadioButtons;
},set_confirmationRadioButtons:function(value){this._confirmationRadioButtons=value;
},get_restrictionsChoiceField:function(){return this._restrictionsChoiceField;
},set_restrictionsChoiceField:function(value){this._restrictionsChoiceField=value;
},get_successMessageTextField:function(){return this._successMessageTextField;
},set_successMessageTextField:function(value){this._successMessageTextField=value;
},get_labelPlacementChoiceField:function(){return this._labelPlacementChoiceField;
},set_labelPlacementChoiceField:function(value){this._labelPlacementChoiceField=value;
},get_redirectUrlTextField:function(){return this._redirectUrlTextField;
},set_redirectUrlTextField:function(value){this._redirectUrlTextField=value;
},get_notificationsCheckboxControl:function(){return this._notificationsCheckboxControl;
},set_notificationsCheckboxControl:function(value){this._notificationsCheckboxControl=value;
},get_emailListTextField:function(){return this._emailListTextField;
},set_emailListTextField:function(value){this._emailListTextField=value;
},get_currentUserEmail:function(){return this._currentUserEmail;
},set_currentUserEmail:function(value){this._currentUserEmail=value;
},get_form:function(){return this._form;
},set_form:function(value){this._form=value;
},get_sendConfirmationEmailCheckBox:function(){return jQuery(this.get_element()).find(":checkbox[id$=sendConfirmationEmail]");
},get_emailTemplateEditor:function(){return this._emailTemplateEditor;
},set_emailTemplateEditor:function(value){this._emailTemplateEditor=value;
},get_clientLabelManager:function(){return this._clientLabelManager;
},set_clientLabelManager:function(value){this._clientLabelManager=value;
}};
Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");
Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap=function(enumMap){this._enumMap=enumMap;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap.prototype={getValue:function(name){return this._enumMap.NamesToValuesMap[name];
},getName:function(value){return this._enumMap.ValuesToNamesMap[value];
}};
Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.FormSettingsEditor",Sys.UI.Control);
