Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView=function(element){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.initializeBase(this,[element]);
this._labelTextField=null;
this._addOtherChoiceField=null;
this._choiceItemsBuilder=null;
this._defaultSelectedChoiceField=null;
this._otherTitleTextField=null;
this._requiredChoiceField=null;
this._errorMessageTextField=null;
this._defaultRequiredMessage=null;
this._sortChoicesAlphabeticallyChoiceField=null;
this._metaFieldNameTextBox=null;
this._requiredChoiceFieldValueChangedDelegate=null;
this._addOtherChoiceFieldValueChangedDelegate=null;
this._defaultSelectedChoiceFieldValueChangedDelegate=null;
this._pageLoadDelegate=null;
this._beforeSaveChangesDelegate=null;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.prototype={initialize:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.callBaseMethod(this,"initialize");
if(this._addOtherChoiceField){this._addOtherChoiceFieldValueChangedDelegate=Function.createDelegate(this,this._addOtherChoiceFieldValueChangedHandler);
this._addOtherChoiceField.add_valueChanged(this._addOtherChoiceFieldValueChangedDelegate);
}if(this._requiredChoiceField){this._requiredChoiceFieldValueChangedDelegate=Function.createDelegate(this,this._requiredChoiceFieldValueChangedHandler);
this._requiredChoiceField.add_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
}if(this._defaultSelectedChoiceField){this._defaultSelectedChoiceFieldValueChangedDelegate=Function.createDelegate(this,this._defaultSelectedChoiceFieldValueChangedHandler);
this._defaultSelectedChoiceField.add_valueChanged(this._defaultSelectedChoiceFieldValueChangedDelegate);
}this._pageLoadDelegate=Function.createDelegate(this,this._pageLoadHandler);
Sys.Application.add_load(this._pageLoadDelegate);
},dispose:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.callBaseMethod(this,"dispose");
Sys.Application.remove_load(this._pageLoadDelegate);
if(this._addOtherChoiceFieldValueChangedDelegate){if(this._addOtherChoiceField){this._addOtherChoiceField.remove_valueChanged(this._addOtherChoiceFieldValueChangedDelegate);
}}if(this._requiredChoiceFieldValueChangedDelegate){if(this._requiredChoiceField){this._requiredChoiceField.remove_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
}}},refreshUI:function(){var controlData=this.get_controlData();
this._labelTextField.set_value(controlData.Title);
if(controlData.ChoiceItemsTitles&&controlData.ChoiceItemsTitles.length>0){if(controlData.DefaultSelectedTitle!==undefined){this._choiceItemsBuilder.set_choiceItemsTitles(controlData.ChoiceItemsTitles,controlData.DefaultSelectedTitle);
}else{this._choiceItemsBuilder.set_choiceItemsTitles(controlData.ChoiceItemsTitles,null);
}}if(this._sortChoicesAlphabeticallyChoiceField){this._sortChoicesAlphabeticallyChoiceField.set_value(controlData.SortAlphabetically);
}if(this._requiredChoiceField){if(controlData.ValidatorDefinition==null){controlData.ValidatorDefinition={Required:false};
}if(controlData.ValidatorDefinition.Required){this._requiredChoiceField.set_value(true);
if(this._errorMessageTextField){jQuery(this._errorMessageTextField.get_element()).show();
this._errorMessageTextField.set_value(controlData.ValidatorDefinition.RequiredViolationMessage);
}}else{this._requiredChoiceField.set_value(false);
if(this._errorMessageTextField){this._errorMessageTextField.set_value(this._defaultRequiredMessage);
jQuery(this._errorMessageTextField.get_element()).hide();
}}}if(this._defaultSelectedChoiceField){if(controlData.FirstItemIsSelected){this.enable_requiredChoiceField(false);
this._defaultSelectedChoiceField.set_value("First");
}else{this.enable_requiredChoiceField(true);
this._defaultSelectedChoiceField.set_value("None");
}}if(this._otherTitleTextField&&this._otherTitleTextField){if(controlData.EnableAddOther){jQuery(this._otherTitleTextField.get_element()).show();
this._addOtherChoiceField.set_value(true);
}else{jQuery(this._otherTitleTextField.get_element()).hide();
this._addOtherChoiceField.set_value(false);
}this._otherTitleTextField.set_value(controlData.OtherTitleText);
}},applyChanges:function(){var controlData=this.get_controlData();
controlData.Title=this._labelTextField.get_value();
var metaFieldName=this.get_metaFieldNameTextBox();
if(metaFieldName&&!metaFieldName.get_readOnly()&&controlData.MetaField){controlData.MetaField.FieldName=metaFieldName.get_value();
}if(controlData.DefaultSelectedTitle!==undefined){controlData.DefaultSelectedTitle=this._choiceItemsBuilder.get_defaultSelectedTitle();
}controlData.ChoiceItemsTitles=this._choiceItemsBuilder.get_choiceItemsTitles();
if(this._sortChoicesAlphabeticallyChoiceField){if(this._sortChoicesAlphabeticallyChoiceField.get_value()=="true"){controlData.SortAlphabetically=true;
}else{controlData.SortAlphabetically=false;
}}if(this._requiredChoiceField){if(controlData.ValidatorDefinition===null){controlData.ValidatorDefinition={Required:false};
}if(this._requiredChoiceField.get_value()==="true"){controlData.ValidatorDefinition.Required=true;
controlData.ValidatorDefinition.RequiredViolationMessage=this._errorMessageTextField.get_value();
}else{controlData.ValidatorDefinition.Required=false;
}}if(this._defaultSelectedChoiceField){if(this._defaultSelectedChoiceField.get_value()=="First"){this.enable_requiredChoiceField(false);
controlData.FirstItemIsSelected=true;
}else{this.enable_requiredChoiceField(true);
controlData.FirstItemIsSelected=false;
}}if(this._otherTitleTextField&&this._otherTitleTextField){if(this._addOtherChoiceField.get_value()==="true"){controlData.EnableAddOther=true;
controlData.OtherTitleText=this._otherTitleTextField.get_value();
}else{controlData.EnableAddOther=false;
}}},get_controlData:function(){return this.get_parentDesigner().get_propertyEditor().get_control();
},enable_requiredChoiceField:function(enable){if(enable){jQuery(this._requiredChoiceField._element).find("input").removeAttr("disabled");
jQuery(this._requiredChoiceField._element).find("label").removeClass("sfDisabledLinkBtn");
}else{jQuery(this._requiredChoiceField._element).find("input").attr("disabled","disabled");
jQuery(this._requiredChoiceField._element).find("label").addClass("sfDisabledLinkBtn");
}},_requiredChoiceFieldValueChangedHandler:function(sender){if(this._requiredChoiceField.get_value()==="true"){jQuery(this._errorMessageTextField.get_element()).show();
}else{jQuery(this._errorMessageTextField.get_element()).hide();
}dialogBase.resizeToContent();
},_defaultSelectedChoiceFieldValueChangedHandler:function(sender){if(this.get_defaultSelectedChoiceField().get_value()=="None"){this.enable_requiredChoiceField(true);
}else{this.enable_requiredChoiceField(false);
this._requiredChoiceField.set_value(false);
if(this._errorMessageTextField){jQuery(this._errorMessageTextField.get_element()).hide();
}}},_addOtherChoiceFieldValueChangedHandler:function(sender){if(this._addOtherChoiceField.get_value()==="true"){jQuery(this._otherTitleTextField.get_element()).show();
}else{jQuery(this._otherTitleTextField.get_element()).hide();
}dialogBase.resizeToContent();
},_pageLoadHandler:function(){this._beforeSaveChangesDelegate=Function.createDelegate(this,this._beforeSaveChangesHandler);
this.get_propertyEditor().add_beforeSaveChanges(this._beforeSaveChangesDelegate);
},_beforeSaveChangesHandler:function(sender,cancelEventArgs){cancelEventArgs.set_cancel(!this._choiceItemsBuilder.validate());
},get_parentDesigner:function(){return this._parentDesigner;
},set_parentDesigner:function(value){this._parentDesigner=value;
},get_propertyEditor:function(){if(this.get_parentDesigner()){return this.get_parentDesigner().get_propertyEditor();
}return null;
},get_labelTextField:function(){return this._labelTextField;
},set_labelTextField:function(value){this._labelTextField=value;
},get_addOtherChoiceField:function(){return this._addOtherChoiceField;
},set_addOtherChoiceField:function(value){this._addOtherChoiceField=value;
},get_choiceItemsBuilder:function(){return this._choiceItemsBuilder;
},set_choiceItemsBuilder:function(value){this._choiceItemsBuilder=value;
},get_defaultSelectedChoiceField:function(){return this._defaultSelectedChoiceField;
},set_defaultSelectedChoiceField:function(value){this._defaultSelectedChoiceField=value;
},get_otherTitleTextField:function(){return this._otherTitleTextField;
},set_otherTitleTextField:function(value){this._otherTitleTextField=value;
},get_requiredChoiceField:function(){return this._requiredChoiceField;
},set_requiredChoiceField:function(value){this._requiredChoiceField=value;
},get_sortChoicesAlphabeticallyChoiceField:function(){return this._sortChoicesAlphabeticallyChoiceField;
},set_sortChoicesAlphabeticallyChoiceField:function(value){this._sortChoicesAlphabeticallyChoiceField=value;
},get_metaFieldNameTextBox:function(){return this._metaFieldNameTextBox;
},set_metaFieldNameTextBox:function(value){this._metaFieldNameTextBox=value;
},get_errorMessageTextField:function(){return this._errorMessageTextField;
},set_errorMessageTextField:function(value){this._errorMessageTextField=value;
}};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
