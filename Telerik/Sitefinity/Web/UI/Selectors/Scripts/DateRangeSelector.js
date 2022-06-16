Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.DateRangeSelector=function(element){Telerik.Sitefinity.Web.UI.DateRangeSelector.initializeBase(this,[element]);
this._customRangeValue=null;
this._dateRangesChoiceField=null;
this._fromDateField=null;
this._toDateField=null;
this._datesPanel=null;
this._dateRangesValueChangedDelegate=null;
this._onLoadDelegate=null;
};
Telerik.Sitefinity.Web.UI.DateRangeSelector.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.DateRangeSelector.callBaseMethod(this,"initialize");
this._dateRangesValueChangedDelegate=Function.createDelegate(this,this._dateRangesValueChangedHandler);
this._dateRangesChoiceField.add_valueChanged(this._dateRangesValueChangedDelegate);
jQuery(this._datesPanel).attr("disabled",true);
this._onLoadDelegate=Function.createDelegate(this,this._onLoadHandler);
Sys.Application.add_load(this._onLoadDelegate);
},dispose:function(){Telerik.Sitefinity.Web.UI.DateRangeSelector.callBaseMethod(this,"dispose");
if(this._dateRangesValueChangedDelegate){this._dateRangesChoiceField.remove_valueChanged(this._dateRangesValueChangedDelegate);
}},get_value:function(){var choiceVal=this._dateRangesChoiceField.get_value();
if(choiceVal==this._customRangeValue){var from=this._fromDateField.get_value();
var to=this._toDateField.get_value();
var fmt="dd MMM, yyyy HH:mm:ss";
var fromText="";
if(from){fromText=from.sitefinityLocaleFormat(fmt);
from=GetUserPreferences().sitefinityToUniversalDate(from).toUTCString();
}var toText="";
if(to){toText=to.sitefinityLocaleFormat(fmt);
to=GetUserPreferences().sitefinityToUniversalDate(to).toUTCString();
}var text="";
if(fromText&&toText){text=String.format("{0} - {1}",fromText,toText);
}else{if(fromText){text="From "+fromText;
}else{text="To "+toText;
}}return{From:from,To:to,Text:text};
}else{if(choiceVal==""){return null;
}else{var choiceField=this._dateRangesChoiceField;
var text=choiceField.get_choices()[choiceField.get_selectedChoicesIndex()].Text;
return{From:choiceVal,To:null,Text:text};
}}},set_value:function(value){if(value===undefined||value==null||value==""){this._dateRangesChoiceField.set_value("");
}else{if(value.From&&!value.To&&isNaN(Date.parse(value.From))){this._dateRangesChoiceField.set_value(value.From);
}else{this._dateRangesChoiceField.set_value(this._customRangeValue);
if(value.From){this._fromDateField.set_value(new Date(value.From));
}else{this._fromDateField.set_value(null);
}if(value.To){this._toDateField.set_value(new Date(value.To));
}else{this._toDateField.set_value(null);
}}}this._dateRangesValueChangedHandler();
},validate:function(){var choiceVal=this._dateRangesChoiceField.get_value();
if(choiceVal==this._customRangeValue){return(this._fromDateField.validate()&&this._toDateField.validate())&&(this._fromDateField.get_value()||this._toDateField.get_value());
}return true;
},_dateRangesValueChangedHandler:function(sender,args){if(this._dateRangesChoiceField.get_value()==this._customRangeValue){jQuery(this._datesPanel).removeAttr("disabled");
}else{jQuery(this._datesPanel).attr("disabled",true);
}},_onLoadHandler:function(e){this._dateRangesValueChangedHandler();
},get_dateRangesChoiceField:function(){return this._dateRangesChoiceField;
},set_dateRangesChoiceField:function(value){this._dateRangesChoiceField=value;
},get_fromDateField:function(){return this._fromDateField;
},set_fromDateField:function(value){this._fromDateField=value;
},get_toDateField:function(){return this._toDateField;
},set_toDateField:function(value){this._toDateField=value;
},get_datesPanel:function(){return this._datesPanel;
},set_datesPanel:function(value){this._datesPanel=value;
}};
Telerik.Sitefinity.Web.UI.DateRangeSelector.registerClass("Telerik.Sitefinity.Web.UI.DateRangeSelector",Sys.UI.Control);
