Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls");
Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField=function(element){Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.initializeBase(this,[element]);
this._element=element;
this._addNewRuleButton=null;
this._mediaQueryRuleDialog=null;
this._deviceTypesDropDown=null;
this._defaultRules=[];
this._rules=[];
this._editedRuleIndex=-1;
this._rulesDataSource=null;
};
Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.prototype={initialize:function(){Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.callBaseMethod(this,"initialize");
this._defaultRules=Telerik.Sitefinity.JSON.parse(this._defaultRules);
$(this.get_mediaQueryRuleDialog()).kendoWindow({resizable:false,width:"425px",height:"auto",title:"Modal Window",modal:true,visible:false,animation:false,actions:["Refresh","Maximize","Minimize","Close"]});
var me=this;
$(this._selectors.designer.ruleWidthType).change(function(){me._setWidthTypeInterface();
});
$(this._selectors.designer.ruleHeightType).change(function(){me._setHeightTypeInterface();
});
$(this.get_addNewRuleButton()).click(function(){me._openRuleEditor();
});
$(this._selectors.designer.doneButton).click(function(){if(me._saveRule()){me._closeDialog();
me._bindRules();
}});
$(this._selectors.designer.cancelButton).click(function(){me._closeDialog();
});
$(this._selectors.designer.mediaQueryAffectingElement).change(function(){me._refreshMediaQuery();
});
$(this._selectors.designer.editQueryButton).click(function(){me._switchToEditMode();
});
$(this._selectors.designer.resetQueryButton).click(function(){me._resetMediaQuery();
});
$(this.get_deviceTypesDropDown()).change(function(){me._loadDefaultRules();
});
this._rulesDataSource=new kendo.data.DataSource({data:this.get_rules()});
$(this._selectors.rulesGrid).kendoGrid({rowTemplate:kendo.template($(me._selectors.ruleRowTemplate).html()),scrollable:false,dataSource:me._rulesDataSource,dataBound:function(e){$('a[data-sf-command="'+me._constants.editMediaQueryRuleCommand+'"]').click(function(){var description=$(this).attr("data-sf-description");
var rIndex=me._findRuleIndexByDescription(description);
me._editedRuleIndex=rIndex;
me._openRuleEditor();
});
$('a[data-sf-command="'+me._constants.deleteMediaQueryRuleCommand+'"]').click(function(){var description=$(this).attr("data-sf-description");
var rIndex=me._findRuleIndexByDescription(description);
me._rules.splice(rIndex,1);
me._bindRules();
});
}});
},dispose:function(){Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.callBaseMethod(this,"dispose");
},reset:function(){this._rules.length=0;
this._editedRuleIndex=-1;
this._bindRules();
this._loadDefaultRules();
},get_value:function(){return this._rules;
},set_value:function(value){if(value!=null){this._rules.length=0;
for(var rIter=0;
rIter<value.length;
rIter++){this._rules.push(value[rIter]);
}this._bindRules();
}},isChanged:function(){},_saveRule:function(){if(this._validateRuleForm()){if(this._editedRuleIndex>-1){this.get_rules().splice(this._editedRuleIndex,1,this._getCurrentRuleObject());
}else{this.get_rules().push(this._getCurrentRuleObject());
}return true;
}return false;
},_validateRuleForm:function(){var isValid=true;
if($(this._selectors.designer.description).val().length==0){isValid=false;
$(this._selectors.designer.validation.descriptionEmpty).show();
}else{$(this._selectors.designer.validation.descriptionEmpty).hide();
}var widthType=$(this._selectors.designer.ruleWidthType).val();
var exactWidth=$(this._selectors.designer.exactWidth).val();
if((widthType==0||widthType==2)&&exactWidth.length>0){if(!(this._isInteger(exactWidth))){isValid=false;
$(this._selectors.designer.validation.exactWidthNotNumber).show();
}else{$(this._selectors.designer.validation.exactWidthNotNumber).hide();
}}else{$(this._selectors.designer.validation.exactWidthNotNumber).hide();
}var minWidth=$(this._selectors.designer.minWidth).val();
if((widthType==1||widthType==3)&&minWidth.length>0){if(!(this._isInteger(minWidth))){isValid=false;
$(this._selectors.designer.validation.minWidthNotNumber).show();
}else{$(this._selectors.designer.validation.minWidthNotNumber).hide();
}}else{$(this._selectors.designer.validation.minWidthNotNumber).hide();
}var maxWidth=$(this._selectors.designer.maxWidth).val();
if((widthType==1||widthType==3)&&maxWidth.length>0){if(!(this._isInteger(maxWidth))){isValid=false;
$(this._selectors.designer.validation.maxWidthNotNumber).show();
}else{$(this._selectors.designer.validation.maxWidthNotNumber).hide();
}}else{$(this._selectors.designer.validation.maxWidthNotNumber).hide();
}var heightType=$(this._selectors.designer.ruleHeightType).val();
var exactHeight=$(this._selectors.designer.exactHeight).val();
if((heightType==0||heightType==2)&&exactHeight.length>0){if(!(this._isInteger(exactHeight))){isValid=false;
$(this._selectors.designer.validation.exactHeightNotNumber).show();
}else{$(this._selectors.designer.validation.exactHeightNotNumber).hide();
}}else{$(this._selectors.designer.validation.exactHeightNotNumber).hide();
}var minHeight=$(this._selectors.designer.minHeight).val();
if((heightType==1||heightType==3)&&minHeight.length>0){if(!(this._isInteger(minHeight))){isValid=false;
$(this._selectors.designer.validation.minHeightNotNumber).show();
}else{$(this._selectors.designer.validation.minHeightNotNumber).hide();
}}else{$(this._selectors.designer.validation.minHeightNotNumber).hide();
}var maxHeight=$(this._selectors.designer.maxHeight).val();
if((heightType==1||heightType==3)&&maxHeight.length>0){if(!(this._isInteger(maxHeight))){isValid=false;
$(this._selectors.designer.validation.maxHeightNotNumber).show();
}else{$(this._selectors.designer.validation.maxHeightNotNumber).hide();
}}else{$(this._selectors.designer.validation.maxHeightNotNumber).hide();
}var resolution=$(this._selectors.designer.resolution).val();
if(resolution.length>0){if(!(this._isInteger(resolution))){isValid=false;
$(this._selectors.designer.validation.resolutionNotNumber).show();
}else{$(this._selectors.designer.validation.resolutionNotNumber).hide();
}}else{$(this._selectors.designer.validation.resolutionNotNumber).hide();
}return isValid;
},_isInteger:function(val){var isIntegretRegex=/^\s*\d+\s*$/;
return String(val).search(isIntegretRegex)!=-1;
},_bindRules:function(){var grid=$(this._selectors.rulesGrid).data("kendoGrid");
grid.dataSource.read();
$(this._selectors.rulesGrid).find(".sfOrLabel:last").hide();
},_loadDefaultRules:function(){this.get_rules().length=0;
var deviceType=$(this.get_deviceTypesDropDown()).val();
for(var i=0;
i<this._defaultRules.length;
i++){if(this._defaultRules[i].DeviceTypeName==deviceType){this.get_rules().push(this._defaultRules[i]);
}}this._bindRules();
},_openRuleEditor:function(){if(this._editedRuleIndex>-1){this._loadRuleInTheForm(this.get_rules()[this._editedRuleIndex]);
}else{this._resetDialogForm();
}var window=$(this.get_mediaQueryRuleDialog()).data("kendoWindow");
window.center();
jQuery(window.element).parent().css({top:this._dialogScrollTop()});
window.open();
},_dialogScrollTop:function(){var scrollTopHtml=jQuery("html").eq(0).scrollTop();
var scrollTopBody=jQuery("body").eq(0).scrollTop();
var scrollTop=((scrollTopHtml>scrollTopBody)?scrollTopHtml:scrollTopBody)+50;
return scrollTop;
},_switchToEditMode:function(){var _css=this._generateMediaQuery();
$(this._selectors.designer.mediaQueryReadElement).hide();
$(this._selectors.designer.mediaQueryEditElement).show();
$(this._selectors.designer.mediaQueryEditElement).val(_css);
$(this._selectors.designer.editQueryButton).hide();
$(this._selectors.designer.resetQueryButton).show();
$(this._selectors.designer.mediaQueryAffectingElement).attr("disabled","disabled");
},_resetMediaQuery:function(){var _css=this._generateMediaQuery();
$(this._selectors.designer.mediaQueryEditElement).hide();
$(this._selectors.designer.mediaQueryReadElement).show();
$(this._selectors.designer.mediaQueryReadElement).val(_css);
$(this._selectors.designer.resetQueryButton).hide();
$(this._selectors.designer.editQueryButton).show();
$(this._selectors.designer.mediaQueryAffectingElement).removeAttr("disabled");
},_refreshMediaQuery:function(){$(this._selectors.designer.mediaQueryReadElement).html(this._generateMediaQuery());
},_generateMediaQuery:function(){var css="@media all ";
var _widthType=$(this._selectors.designer.ruleWidthType).val();
var _exactWidth=$(this._selectors.designer.exactWidth).val();
var _minWidth=$(this._selectors.designer.minWidth).val();
var _maxWidth=$(this._selectors.designer.maxWidth).val();
var _heightType=$(this._selectors.designer.ruleHeightType).val();
var _exactHeight=$(this._selectors.designer.exactHeight).val();
var _minHeight=$(this._selectors.designer.minHeight).val();
var _maxHeight=$(this._selectors.designer.maxHeight).val();
var _isPortraitOrientation=$(this._selectors.designer.portraitOrientation).is(":checked");
var _isLandscapeOrientation=$(this._selectors.designer.landscapeOrientation).is(":checked");
var _aspectRatio=$(this._selectors.designer.aspectRatio).val();
var _resolution=$(this._selectors.designer.resolution).val();
var _isMonochrome=$(this._selectors.designer.isMonochrome).is(":checked");
var _isGrid=$(this._selectors.designer.isGrid).is(":checked");
if(_widthType=="0"&&_exactWidth.length>0){css+="and (width:"+_exactWidth+"px) ";
}if(_widthType=="1"){if(_minWidth.length>0){css+="and (min-width:"+_minWidth+"px) ";
}if(_maxWidth.length>0){css+="and (max-width:"+_maxWidth+"px) ";
}}if(_widthType=="2"&&_exactWidth.length>0){css+="and (device-width:"+_exactWidth+"px) ";
}if(_widthType=="3"){if(_minWidth.length>0){css+="and (min-device-width:"+_minWidth+"px) ";
}if(_maxWidth.length>0){css+="and (max-device-width:"+_maxWidth+"px) ";
}}if(_heightType=="0"&&_exactHeight.length>0){css+="and (height:"+_exactHeight+"px) ";
}if(_heightType=="1"){if(_minHeight.length>0){css+="and (min-height:"+_minHeight+"px) ";
}if(_maxHeight.length>0){css+="and (max-height:"+_maxHeight+"px) ";
}}if(_heightType=="2"&&_exactHeight.length>0){css+="and (device-height:"+_exactHeight+"px) ";
}if(_heightType=="3"){if(_minHeight.length>0){css+="and (min-device-height:"+_minHeight+"px) ";
}if(_maxHeight.length>0){css+="and (max-device-height:"+_maxHeight+"px) ";
}}if(_isPortraitOrientation){css+="and (orientation:portrait) ";
}else{if(_isLandscapeOrientation){css+="and (orientation:landscape) ";
}}if(_aspectRatio.length>0){css+="and (aspect-ratio:"+_aspectRatio+") ";
}if(_resolution.length>0){css+="and (resolution:"+_resolution+") ";
}if(_isMonochrome){css+="and (monochrome) ";
}if(_isGrid){css+="and (grid) ";
}return css;
},_getCurrentRuleObject:function(){var rule=new Object();
rule.Description=$(this._selectors.designer.description).val();
rule.WidthType=$(this._selectors.designer.ruleWidthType).val();
rule.ExactWidth=$(this._selectors.designer.exactWidth).val();
rule.MinWidth=$(this._selectors.designer.minWidth).val();
rule.MaxWidth=$(this._selectors.designer.maxWidth).val();
rule.HeightType=$(this._selectors.designer.ruleHeightType).val();
rule.ExactHeight=$(this._selectors.designer.exactHeight).val();
rule.MinHeight=$(this._selectors.designer.minHeight).val();
rule.MaxHeight=$(this._selectors.designer.maxHeight).val();
rule.Orientation=0;
if($(this._selectors.designer.portraitOrientation).is(":checked")){rule.Orientation=1;
}else{if($(this._selectors.designer.landscapeOrientation).is(":checked")){rule.Orientation=2;
}}rule.AspectRatio=$(this._selectors.designer.aspectRatio).val();
rule.Resolution=$(this._selectors.designer.resolution).val();
rule.IsMonochrome=$(this._selectors.designer.isMonochrome).is(":checked");
rule.IsGrid=$(this._selectors.designer.isGrid).is(":checked");
rule.MediaQueryRule=$(this._selectors.designer.mediaQueryEditElement).is(":visible")?$(this._selectors.designer.mediaQueryEditElement).val():$(this._selectors.designer.mediaQueryReadElement).html();
return rule;
},_closeDialog:function(){this._resetDialogForm();
var window=$(this.get_mediaQueryRuleDialog()).data("kendoWindow");
window.close();
},_loadRuleInTheForm:function(rule){this._resetMediaQuery();
$(this._selectors.designer.description).val(rule.Description);
$(this._selectors.designer.ruleWidthType).val(rule.WidthType);
$(this._selectors.designer.exactWidth).val(rule.ExactWidth);
$(this._selectors.designer.minWidth).val(rule.MinWidth);
$(this._selectors.designer.maxWidth).val(rule.MaxWidth);
$(this._selectors.designer.ruleHeightType).val(rule.HeightType);
$(this._selectors.designer.exactHeight).val(rule.ExactHeight);
$(this._selectors.designer.minHeight).val(rule.MinHeight);
$(this._selectors.designer.maxHeight).val(rule.MaxHeight);
switch(rule.Orientation){case 0:$(this._selectors.designer.bothOrientations).attr("checked","checked");
break;
case 1:$(this._selectors.designer.portraitOrientation).attr("checked","checked");
break;
case 2:$(this._selectors.designer.landscapeOrientation).attr("checked","checked");
break;
}$(this._selectors.designer.aspectRatio).val(rule.AspectRatio);
$(this._selectors.designer.resolution).val(rule.Resolution);
if(rule.IsMonochrome){$(this._selectors.designer.isMonochrome).attr("checked","checked");
}else{$(this._selectors.designer.isMonochrome).prop("checked",false);
}if(rule.IsGrid){$(this._selectors.designer.isGrid).attr("checked","checked");
}else{$(this._selectors.designer.isGrid).prop("checked",false);
}if(rule.IsManualMediaQuery){this._switchToEditMode();
}$(this._selectors.designer.mediaQueryEditElement).val(rule.MediaQueryRule);
$(this._selectors.designer.mediaQueryReadElement).html(rule.MediaQueryRule);
this._setWidthTypeInterface();
this._setHeightTypeInterface();
},_resetDialogForm:function(){this._resetMediaQuery();
$(this._selectors.designer.description).val("");
$(this._selectors.designer.ruleWidthType).val($(this._selectors.designer.ruleWidthType+" option:first").val());
$(this._selectors.designer.exactWidth).val("");
$(this._selectors.designer.minWidth).val("");
$(this._selectors.designer.maxWidth).val("");
$(this._selectors.designer.ruleHeightType).val($(this._selectors.designer.ruleHeightType+" option:first").val());
$(this._selectors.designer.exactHeight).val("");
$(this._selectors.designer.minHeight).val("");
$(this._selectors.designer.maxHeight).val("");
$(this._selectors.designer.bothOrientations).attr("checked","checked");
$(this._selectors.designer.aspectRatio).val("");
$(this._selectors.designer.resolution).val("");
$(this._selectors.designer.isMonochrome).prop("checked",false);
$(this._selectors.designer.isGrid).prop("checked",false);
$(this._selectors.designer.mediaQueryEditElement).val("");
$(this._selectors.designer.mediaQueryReadElement).html("");
this._editedRuleIndex=-1;
$(this._selectors.designer.validation.allMessages).hide();
},_setWidthTypeInterface:function(){var val=$(this._selectors.designer.ruleWidthType).val();
if(val=="0"||val=="2"){$(this._selectors.designer.exactWidthsPanel).show();
$(this._selectors.designer.rangeWidthsPanel).hide();
}else{$(this._selectors.designer.exactWidthsPanel).hide();
$(this._selectors.designer.rangeWidthsPanel).show();
}},_setHeightTypeInterface:function(){var val=$(this._selectors.designer.ruleHeightType).val();
if(val=="0"||val=="2"){$(this._selectors.designer.exactHeightsPanel).show();
$(this._selectors.designer.rangeHeightsPanel).hide();
}else{$(this._selectors.designer.exactHeightsPanel).hide();
$(this._selectors.designer.rangeHeightsPanel).show();
}},_findRuleIndexByDescription:function(description){for(var count=0;
count<this._rules.length;
count++){if(this._rules[count].Description==description){return count;
}}return -1;
},_selectors:{rulesGrid:"#media-query-rules-grid",ruleRowTemplate:"#media-query-rule-row-template",designer:{description:"#media-query-rule-description",mediaQueryAffectingElement:".sf_affectsMediaQuery",doneButton:"#media-query-rule-done-button",cancelButton:"#media-query-rule-cancel-button",ruleWidthType:"#media-query-rule-width-type",ruleHeightType:"#media-query-rule-height-type",exactWidthsPanel:".exact-widths-panel",exactHeightsPanel:".exact-heights-panel",rangeWidthsPanel:".range-widths-panel",rangeHeightsPanel:".range-heights-panel",exactWidth:"#media-query-rule-exact-width",minWidth:"#media-query-rule-min-width",maxWidth:"#media-query-rule-max-width",exactHeight:"#media-query-rule-exact-height",minHeight:"#media-query-rule-min-height",maxHeight:"#media-query-rule-max-height",mediaQueryReadElement:"#media-query-rule-css-read",mediaQueryEditElement:"#media-query-rule-css-edit",portraitOrientation:"#media-query-rule-orientation-portrait",landscapeOrientation:"#media-query-rule-orientation-landscape",bothOrientations:"#media-query-rule-orientation-both",aspectRatio:"#media-query-rule-aspect-ratio",resolution:"#media-query-rule-resolution",isMonochrome:"#media-query-rule-is-monochrome",isGrid:"#media-query-rule-is-grid",editQueryButton:"#media-query-rule-edit-css-button",resetQueryButton:"#media-query-rule-reset-css-button",validation:{allMessages:".sfError",descriptionEmpty:"#media-query-rule-description-required",exactWidthNotNumber:"#media-query-rule-exact-width-not-number",exactHeightNotNumber:"#media-query-rule-exact-height-not-number",minWidthNotNumber:"#media-query-rule-min-width-not-number",maxWidthNotNumber:"#media-query-rule-max-width-not-number",minHeightNotNumber:"#media-query-rule-min-height-not-number",maxHeightNotNumber:"#media-query-rule-max-height-not-number",resolutionNotNumber:"#media-query-rule-resolution-not-number"}}},_constants:{editMediaQueryRuleCommand:"edit-media-query-rule",deleteMediaQueryRuleCommand:"delete-media-query-rule"},get_element:function(){return this._element;
},set_element:function(value){this._element=value;
},get_addNewRuleButton:function(){return this._addNewRuleButton;
},set_addNewRuleButton:function(value){this._addNewRuleButton=value;
},get_mediaQueryRuleDialog:function(){return this._mediaQueryRuleDialog;
},set_mediaQueryRuleDialog:function(value){this._mediaQueryRuleDialog=value;
},get_deviceTypesDropDown:function(){return this._deviceTypesDropDown;
},set_deviceTypesDropDown:function(value){this._deviceTypesDropDown=value;
},get_rules:function(){return this._rules;
}};
Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.MediaQueryRuleField",Telerik.Sitefinity.Web.UI.Fields.FieldControl);
