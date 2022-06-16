Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.EmailTextField=function(element){Telerik.Sitefinity.Web.UI.Fields.EmailTextField.initializeBase(this,[element]);
this._element=element;
this._labelElement=null;
this._textBoxElement=null;
this._expandableExtenderId=null;
this._expandableExtenderBehavior=null;
this._conditionalTemplatesContainerId=null;
this._templatesContainer=null;
this._hideIfValue=null;
this._suffix=null;
this._currentCondition=null;
this._textBoxId=null;
this._textLabelId=null;
this._conditionDictionary=[];
this._unit=null;
this._characterCounterElement=null;
this._recommendedCharactersCount=0;
this._trimSpaces=false;
this._maxChars=0;
this._allowNulls=false;
this._readOnlyReplacement=false;
this._isLocalizable=false;
this._readOnlyModeOriginalValue=null;
this._autocompleteServiceUrl=null;
};
Telerik.Sitefinity.Web.UI.Fields.EmailTextField.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Fields.EmailTextField.callBaseMethod(this,"initialize");
if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){this._handleKeyDownDelegate=Function.createDelegate(this,this._keyDownHandler);
$addHandler(this.get_textElement(),"keypress",this._handleKeyDownDelegate);
this._handleKeyUpDelegate=Function.createDelegate(this,this._keyUpHandler);
$addHandler(this.get_textElement(),"keyup",this._handleKeyUpDelegate);
this._handleInputDelegate=Function.createDelegate(this,this._inputHandler);
$addHandler(this.get_textElement(),"input",this._handleInputDelegate);
this._handleFocusDelegate=Function.createDelegate(this,this._focusHandler);
$addHandler(this.get_textElement(),"focus",this._handleFocusDelegate);
this._handleBlurDelegate=Function.createDelegate(this,this._blurHandler);
$addHandler(this.get_textElement(),"blur",this._handleBlurDelegate);
if(this._autocompleteServiceUrl!=null){$("#"+this.get_textElement().id).autocomplete({source:this._autocompleteServiceUrl,minLength:0}).focus(function(){$(this).autocomplete("search",$(this).val());
});
}}if(this._hideIfValue!=null){this.set_defaultValue(this._hideIfValue);
}this.calculateCharactersCount();
jQuery(this.get_characterCounterElement()).hide();
},dispose:function(){if(this._handleKeyDownDelegate){$removeHandler(this.get_textElement(),"keypress",this._handleKeyDownDelegate);
delete this._handleKeyDownDelegate;
}if(this._handleKeyUpDelegate){$removeHandler(this.get_textElement(),"keyup",this._handleKeyUpDelegate);
delete this._handleKeyUpDelegate;
}if(this._handleInputDelegate){$removeHandler(this.get_textElement(),"input",this._handleInputDelegate);
delete this._handleInputDelegate;
}if(this._handleFocusDelegate){$removeHandler(this.get_textElement(),"focus",this._handleFocusDelegate);
delete this._handleFocusDelegate;
}if(this._handleBlurDelegate){$removeHandler(this.get_textElement(),"blur",this._handleBlurDelegate);
delete this._handleBlurDelegate;
}this._labelElement=null;
this._textBoxElement=null;
Telerik.Sitefinity.Web.UI.Fields.EmailTextField.callBaseMethod(this,"dispose");
},reset:function(){var defaultValue=this.get_defaultValue();
if(defaultValue==undefined){defaultValue=null;
}this.set_value(defaultValue);
jQuery(this.get_characterCounterElement()).text("");
Telerik.Sitefinity.Web.UI.Fields.EmailTextField.callBaseMethod(this,"reset");
},get_value:function(){var val=this._get_textValue();
if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){if(val==""&&this._hideIfValue!=null){return this._hideIfValue;
}}if(val==""&&this.get_allowNulls()){return null;
}return val;
},set_value:function(value){if(this.get_trimSpaces()){value=jQuery.trim(value);
}if(this._hideIfValue!=null&&this._hideIfValue==value){if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){this._clearTextBox();
}else{this._clearLabel();
}}else{if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){this._set_writeModeValue(value);
}else{this._set_readModeValue(value);
}}this._value=value;
this.raisePropertyChanged("value");
this._valueChangedHandler();
},isChanged:function(){if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write&&this._value==null){this._value="";
}if(this._value==""&&this.get_allowNulls()){this._value=null;
}var notChanged=(this._replaceNewLines(this._value," ")==this._replaceNewLines(this.get_value()," "));
if(notChanged){return false;
}else{return true;
}},calculateCharactersCount:function(){if(this.get_textElement().value==undefined){return;
}var currentCount=this.get_textElement().value.length;
if(currentCount==0){jQuery(this.get_characterCounterElement()).text("");
}else{jQuery(this.get_characterCounterElement()).text(currentCount);
}if(this.get_recommendedCharactersCount()!=0){this.colorCharactersCount(currentCount,this.get_recommendedCharactersCount());
}jQuery(this.get_characterCounterElement()).show();
},colorCharactersCount:function(currentCount,recommendedCount){if(currentCount<=recommendedCount){jQuery(this.get_characterCounterElement()).css("color","Black");
}else{jQuery(this.get_characterCounterElement()).css("color","Red");
}},_keyDownHandler:function(e){this.focusControlByTabKey(e);
},_inputHandler:function(e){this._trimChars();
this.calculateCharactersCount();
},_keyUpHandler:function(e){this._trimChars();
this.calculateCharactersCount();
},_focusHandler:function(e){jQuery(this.get_characterCounterElement()).show();
this.calculateCharactersCount();
},_blurHandler:function(e){if(this.get_trimSpaces()){this.get_textElement().value=jQuery.trim(this.get_value());
this.calculateCharactersCount();
}var currentCount=this.get_textElement().value.length;
if(currentCount<=this.get_recommendedCharactersCount()){jQuery(this.get_characterCounterElement()).hide();
}},_getTemplate:function(templateCondition){var templatesContainer=this._getTemplatesContainer();
var template=templatesContainer.find("#"+this._conditionDictionary[templateCondition]);
return template;
},_switchToTemplate:function(templateCondition){var currentCondition=this._currentCondition;
if(currentCondition==templateCondition){return;
}this._currentCondition=templateCondition;
var currentElement=jQuery(this._element);
var templatesContainer=this._getTemplatesContainer();
var childsExceptContainer=currentElement.children().not("#"+this._conditionalTemplatesContainerId);
var hiddenContainer=this._getTemplate(currentCondition);
if(hiddenContainer.length==0){hiddenContainerID="hiddenContainer"+this._suffix;
hiddenContainer=jQuery.find("#"+hiddenContainerID);
if(hiddenContainer.length==0){hiddenContainer=jQuery('<div id="'+hiddenContainerID+'"></div>');
}templatesContainer.append(hiddenContainer);
}childsExceptContainer.each(function(index){hiddenContainer.append(this);
});
var newTemplate=this._getTemplate(templateCondition);
var cleanHtml=jQuery.trim(newTemplate.html());
newTemplate.children().each(function(index){jQuery(this).insertBefore(templatesContainer);
});
},_getTemplatesContainer:function(){if(!this._templatesContainer){this._templatesContainer=jQuery("#"+this._conditionalTemplatesContainerId);
}return this._templatesContainer;
},_getElementFromCurrentTemplate:function(selector){return jQuery(this._element).find(selector).not("#"+this._conditionalTemplatesContainerId+" "+selector)[0];
},_buildCondition:function(left,operator,right){return String.format("{0}-{1}-{2}",left,operator,right).toLowerCase();
},_clearLabel:function(){if(this._labelElement!=null){this._labelElement.innerHTML="";
}},_clearTextBox:function(){if(this._textBoxElement!=null){this._textBoxElement.value="";
this.calculateCharactersCount();
}},_set_writeModeValue:function(value){if(value===undefined||value==null){this._clearTextBox();
}else{if(this._textBoxElement!=null){this._textBoxElement.value=value;
if(value!=this.get_defaultValue()&&jQuery("body").hasClass("sfFormDialog")){this._doExpandHandler();
}}if(this._unit!=null&&this._unit.length>0){jQuery(this._element).find("span.sfUnit").remove();
jQuery(this._textBoxElement).after('<span class="sfUnit">'+this._unit+"</span>");
}}},_set_readModeValue:function(value){this._readOnlyModeOriginalValue=value;
if(value===undefined||value==null){this._clearLabel();
}else{if(this._labelElement!=null){var resultText=null;
if(this._readOnlyReplacement){var data=JSON.parse(this._readOnlyReplacement);
if(data&&data.Value&&data.Text){var replacementCondition=data.Value==value;
var replacementText=data.Text;
if(replacementCondition&&replacementText){resultText=replacementText;
}}}if(!resultText){resultText=value;
if(this._unit!=null&&this._unit.length>0){resultText=resultText.htmlEncode?resultText.htmlEncode():resultText;
resultText+=' <p class="sfDescription">'+this._unit+"</p>";
jQuery(this._labelElement).html(resultText);
return;
}}jQuery(this._labelElement).text(resultText);
}}},_get_writeModeValue:function(){if(this._textBoxElement){return this._textBoxElement.value;
}return null;
},_get_textValue:function(){if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){return this._get_writeModeValue();
}else{return this._readOnlyModeOriginalValue;
}},_get_ExpandableExtenderBehavior:function(){if(this._expandableExtenderBehavior){return this._expandableExtenderBehavior;
}this._expandableExtenderBehavior=Sys.UI.Behavior.getBehaviorByName(this._element,"ExpandableExtender");
return this._expandableExtenderBehavior;
},_trimChars:function(){if(this._maxChars&&this._maxChars>0){var value=this.get_value();
value=value.substring(0,this._maxChars);
this.set_value(value);
}},_replaceNewLines:function(value,replacedText){if(value==null){return value;
}return value.toString().replace(/\r\n/ig,replacedText).replace(/\n/ig,replacedText);
},get_allowNulls:function(){return this._allowNulls;
},set_allowNulls:function(value){this._allowNulls=value;
},get_hideIfValue:function(){return this._hideIfValue;
},set_hideIfValue:function(value){this._hideIfValue=value;
},get_textBoxElement:function(){return this._textBoxElement;
},set_textBoxElement:function(value){this._textBoxElement=value;
},get_labelElement:function(){return this._labelElement;
},set_labelElement:function(value){this._labelElement=value;
},get_textElement:function(){if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){return this._textBoxElement;
}else{return this._labelElement;
}},set_displayMode:function(mode){var currentValue=this.get_value();
if(typeof mode==="string"){mode=parseInt(mode);
}this._displayMode=mode;
if(this.get_isInitialized()){switch(mode){case Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read:var condition=this._buildCondition("DisplayMode","Equal","Read");
this._switchToTemplate(condition);
if(!this._labelElement){this._labelElement=this._getElementFromCurrentTemplate("[id*='"+this._textLabelId+this.get_suffix()+"']");
}if(!this._textBoxElement){this._textBoxElement=this._getElementFromCurrentTemplate("[id*='"+this._textBoxId+this.get_suffix()+"']");
}if(this._textBoxElement){currentValue=this._textBoxElement.value;
this.set_value(currentValue);
}jQuery(this._labelElement).text(currentValue);
break;
case Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write:var condition=this._buildCondition("DisplayMode","Equal","Write");
this._switchToTemplate(condition);
if(!this._textBoxElement){this._textBoxElement=this._getElementFromCurrentTemplate("[id*='"+this._textBoxId+this.get_suffix()+"']");
}this._textBoxElement.value=currentValue;
break;
}}},set_tabIndex:function(value){this._tabIndex=value;
jQuery(this.get_textElement()).attr("tabindex",value);
},get_tabIndex:function(){return jQuery(this.get_textElement()).attr("tabindex");
},get_suffix:function(){return this._suffix;
},set_suffix:function(value){this._suffix=value;
},focus:function(){var input=this.get_textElement();
if(jQuery(input).is(":visible")&&jQuery(input).is(":enabled")){input.focus();
}},blur:function(){var behavior=this._get_ExpandableExtenderBehavior();
if(behavior!=null&&!behavior.get_originalExpandedState()){var val=this.get_value();
if(val==""){behavior.collapse();
}}},set_defaultValue:function(value){if(value===null||value===undefined){value="";
}this._defaultValue=value;
},get_element:function(){return this._element;
},set_element:function(value){this._element=value;
},get_conditionDictionary:function(){return this._conditionDictionary;
},set_conditionDictionary:function(value){this._conditionDictionary=value;
},get_characterCounterElement:function(){return this._characterCounterElement;
},set_characterCounterElement:function(value){this._characterCounterElement=value;
},get_recommendedCharactersCount:function(){return this._recommendedCharactersCount;
},set_recommendedCharactersCount:function(value){this._recommendedCharactersCount=value;
},get_trimSpaces:function(){return this._trimSpaces;
},set_trimSpaces:function(value){this._trimSpaces=value;
},get_isLocalizable:function(){return this._isLocalizable;
},set_isLocalizable:function(value){this._isLocalizable=value;
}};
Telerik.Sitefinity.Web.UI.Fields.EmailTextField.registerClass("Telerik.Sitefinity.Web.UI.Fields.EmailTextField",Telerik.Sitefinity.Web.UI.Fields.FieldControl);