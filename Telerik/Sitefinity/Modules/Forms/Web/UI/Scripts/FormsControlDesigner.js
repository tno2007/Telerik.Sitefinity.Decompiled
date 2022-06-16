Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Type._registerScript("FormsControlDesigner.js",["IControlDesigner.js"]);
Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers");
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner=function(element){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner.initializeBase(this,[element]);
this._propertyEditor=null;
this._controlData=null;
this._contentSelector=null;
this._selectedFormId=null;
this._showMessageRadioButton=null;
this._redirectRadioButton=null;
this._successMessageTextField=null;
this._confirmationRadioButtons=null;
this._chkPerWidgetConfirmation=null;
this._defaultSuccessMessage="";
this._confirmationClickDelegate=null;
this._useCustomConfirmationClickDelegate=null;
this._submitActionsMap={};
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner.prototype={initialize:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner.callBaseMethod(this,"initialize");
$(this).on("unload",function(e){jQuery.event.remove(this);
jQuery.removeData(this);
});
this._showContentSelectorDelegate=Function.createDelegate(this,this._showContentSelector);
Sys.Application.add_load(Function.createDelegate(this,this._showContentSelectorDelegate));
this._selectContentDelegate=Function.createDelegate(this,this._selectContent);
this._contentSelector.add_doneClientSelection(this._selectContentDelegate);
this._showMessageRadioButton=$get(this._confirmationRadioButtons[0]);
this._redirectRadioButton=$get(this._confirmationRadioButtons[1]);
this._confirmationClickDelegate=Function.createDelegate(this,this._confirmationClickHandler);
$addHandler(this._showMessageRadioButton,"click",this._confirmationClickDelegate,true);
$addHandler(this._redirectRadioButton,"click",this._confirmationClickDelegate,true);
this._useCustomConfirmationClickDelegate=Function.createDelegate(this,this._useCustomConfirmationClickHandler);
$addHandler(this._chkPerWidgetConfirmation,"change",this._useCustomConfirmationClickDelegate,true);
this._submitActionsMap=new Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap(this._submitActionsMap);
this._updateConfirmationChoices();
},dispose:function(){Sys.Application.remove_load(this._showContentSelectorDelegate);
this._contentSelector.remove_doneClientSelection(this._selectContentDelegate);
if(this._selectContentDelegate){delete this._selectContentDelegate;
}if(this._showContentSelectorDelegate){delete this._showContentSelectorDelegate;
}if(this._confirmationClickDelegate){delete this._confirmationClickDelegate;
}if(this._useCustomConfirmationClickHandler){delete this._useCustomConfirmationClickHandler;
}delete this._submitActionsMap;
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner.callBaseMethod(this,"dispose");
},refreshUI:function(){var controlData=this.get_controlData();
if(!controlData){return;
}this._updateConfirmationChoices();
},applyChanges:function(){var selectedItems=this.get_contentSelector().getSelectedItems(),controlData=this.get_controlData();
if(selectedItems!=null){if(selectedItems.length>0){this._selectedFormId=selectedItems[0].Id;
}}if(this._selectedFormId){controlData.FormId=this._selectedFormId;
}controlData.UserCustomConfirmation=this._shoudUseCustomConfirmation();
if(jQuery(this._showMessageRadioButton).is(":checked")){controlData.SubmitAction=this._submitActionsMap.getValue("TextMessage");
controlData.SuccessMessage=this._successMessageTextField.get_value();
}else{controlData.SubmitAction=this._submitActionsMap.getValue("PageRedirect");
controlData.RedirectPageUrl=this._redirectUrlTextField.get_value();
}},_selectContent:function(items){if(items!=null){var selectedItems=this.get_contentSelector().getSelectedItems();
if(selectedItems!=null){if(selectedItems.length>0){this._selectedFormId=selectedItems[0].Id;
this.get_propertyEditor().saveEditorChanges();
}}}else{this.get_propertyEditor()._closeEditor();
}},_showContentSelector:function(){this.get_contentSelector().dataBind();
var controlData=this.get_controlData();
if(controlData&&controlData.FormId){var dataItemId=controlData.FormId;
if(dataItemId){this.get_contentSelector().set_selectedKeys([dataItemId]);
var itemSelector=this.get_contentSelector().get_itemSelector();
if(itemSelector&&(!itemSelector._selectedKeys||itemSelector._selectedKeys.length===0)){itemSelector._selectedKeys=[dataItemId];
}}}jQuery(this.get_element()).find("#selectorTag").show();
dialogBase.resizeToContent();
},_confirmationClickHandler:function(e){if(e.target==this._showMessageRadioButton){this._setSuccessMessageMode();
}else{this._setRedirectMode();
}dialogBase.resizeToContent();
},_useCustomConfirmationClickHandler:function(e){this._setConfirmationMode();
},_setSuccessMessageMode:function(){jQuery(this._successMessageTextField.get_element()).show();
if(!this._successMessageTextField.get_value()){this._successMessageTextField.set_value(this._defaultSuccessMessage);
}jQuery(this._redirectUrlTextField.get_element()).hide();
},_setRedirectMode:function(){jQuery(this._successMessageTextField.get_element()).hide();
jQuery(this._redirectUrlTextField.get_element()).show();
},_setConfirmationMode:function(){if(this._shoudUseCustomConfirmation()){jQuery("#panelConfirmation").show();
}else{jQuery("#panelConfirmation").hide();
}dialogBase.resizeToContent();
},_updateConfirmationChoices:function(){var controlData=this.get_controlData();
jQuery(this.get_chkPerWidgetConfirmation()).prop("checked",controlData.UserCustomConfirmation);
this._setConfirmationMode();
if(controlData.SubmitAction=="TextMessage"){jQuery(this._showMessageRadioButton).attr("checked",true);
this._successMessageTextField.set_value(controlData.SuccessMessage);
this._setSuccessMessageMode();
}else{jQuery(this._redirectRadioButton).attr("checked",true);
this._redirectUrlTextField.set_value(controlData.RedirectPageUrl);
this._setRedirectMode();
}},_shoudUseCustomConfirmation:function(){return jQuery(this.get_chkPerWidgetConfirmation()).prop("checked");
},get_controlData:function(){return this.get_propertyEditor().get_control();
},get_propertyEditor:function(){return this._propertyEditor;
},set_propertyEditor:function(value){this._propertyEditor=value;
},get_contentSelector:function(){return this._contentSelector;
},set_contentSelector:function(value){this._contentSelector=value;
},get_successMessageTextField:function(){return this._successMessageTextField;
},set_successMessageTextField:function(value){this._successMessageTextField=value;
},get_redirectUrlTextField:function(){return this._redirectUrlTextField;
},set_redirectUrlTextField:function(value){this._redirectUrlTextField=value;
},get_confirmationRadioButtons:function(){return this._confirmationRadioButtons;
},set_confirmationRadioButtons:function(value){this._confirmationRadioButtons=value;
},get_chkPerWidgetConfirmation:function(){return this._chkPerWidgetConfirmation;
},set_chkPerWidgetConfirmation:function(value){this._chkPerWidgetConfirmation=value;
}};
Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");
Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap=function(enumMap){this._enumMap=enumMap;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.EnumMap.prototype={getValue:function(name){return this._enumMap.NamesToValuesMap[name];
},getName:function(value){return this._enumMap.ValuesToNamesMap[value];
}};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormsControlDesigner",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IControlDesigner);