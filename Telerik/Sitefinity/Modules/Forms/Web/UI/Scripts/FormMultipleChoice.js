Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");
Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice=function(element){Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.initializeBase(this,[element]);
this._addOtherChoiceItemValue=null;
this._customValueTextField=null;
this._enableAddOther=false;
this._valueChangedDelegate=null;
this._returnCustomValue=false;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.prototype={initialize:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.callBaseMethod(this,"initialize");
if(this._enableAddOther){this._valueChangedDelegate=Function.createDelegate(this,this._choiceValueChangedHandler);
this.add_valueChanged(this._valueChangedDelegate);
}},dispose:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.callBaseMethod(this,"dispose");
if(this._valueChangedDelegate){delete this._valueChangedDelegate;
}},_choiceValueChangedHandler:function(sender){var selectedChoiceIndex=$(this.get_selectedChoicesIndex()).get(0);
var choices=this.get_choices();
var selectedChoice=null;
if(selectedChoiceIndex!==undefined&&selectedChoiceIndex<choices.length){selectedChoice=choices[selectedChoiceIndex];
}if(selectedChoice&&selectedChoice.Value==this._addOtherChoiceItemValue){jQuery(this._customValueTextField.get_element()).show();
this._returnCustomValue=true;
}else{jQuery(this._customValueTextField.get_element()).hide();
this._returnCustomValue=false;
}},_clearAllSelectedItems:function(){if(this._renderChoiceAs==Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox){this._choiceElement.checked=false;
}else{var selector=this._get_selectedAttributeSelector();
jQuery(this._choiceElement).find(this._get_listItemSelector()).attr(selector,false);
}},reset:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.callBaseMethod(this,"reset");
if(this._enableAddOther){this._customValueTextField.set_value("");
}},get_value:function(){var baseValue=Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.callBaseMethod(this,"get_value");
if(this._enableAddOther&&this._returnCustomValue){return this.get_customValueTextField().get_value();
}else{return baseValue;
}},set_value:function(value){Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.callBaseMethod(this,"set_value",[value]);
var selectedChoiceIndex=$(this.get_selectedChoicesIndex()).get(0);
if(value&&this._enableAddOther&&selectedChoiceIndex===undefined){selectedChoiceIndex=this.get_choices().length-1;
this.set_selectedChoicesIndex(selectedChoiceIndex);
this._choiceValueChangedHandler();
this._customValueTextField.set_value(value);
}},get_customValueTextField:function(){return this._customValueTextField;
},set_customValueTextField:function(value){this._customValueTextField=value;
}};
Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormMultipleChoice",Telerik.Sitefinity.Web.UI.Fields.ChoiceField);
