Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.FieldChangeNotifier=function(fieldsBinder,dataItem){this._binder=fieldsBinder;
this._dataItem=dataItem;
this._fieldNames=[];
this._fieldMap={};
var ctrls=this._binder._fieldControls;
var iter=ctrls.length;
var cur=null;
var dependantFieldControls;
for(var propName in dataItem){if(typeof dataItem[propName]!="function"){dependantFieldControls=[];
var obj={};
iter=ctrls.length;
while(iter--){cur=ctrls[iter];
if(cur.get_dataFieldName()==propName){dependantFieldControls.push(cur);
}}obj.dependantFieldControls=dependantFieldControls;
this._fieldMap[propName]=obj;
this._fieldNames.push(propName);
}}};
Telerik.Sitefinity.Web.UI.FieldChangeNotifier.prototype={get_fieldNames:function(){var clone=Array.clone(this._fieldNames);
return clone;
},get_fieldValuesFromUI:function(fieldName){var values=[];
if(this._fieldMap.hasOwnProperty(fieldName)&&typeof this._fieldMap[fieldName]!="function"){var dependentFields=this._fieldMap[fieldName].dependantFieldControls;
var len=dependentFields.length;
for(var idx=0;
idx<len;
idx++){values.push(dependentFields[idx].get_value());
}}return values;
},get_fieldValue:function(fieldName){var undef;
if(this.containsField(fieldName)){var val=this._dataItem[fieldName];
var clone=Telerik.Sitefinity.cloneObject(val);
return clone;
}else{return undef;
}},set_fieldValue:function(fieldName,value){if(this.notifyFieldChange(fieldName,value)){this._dataItem[fieldName]=value;
return true;
}else{return false;
}},containsField:function(fieldName){return this._dataItem.hasOwnProperty(fieldName)&&typeof this._dataItem[fieldName]!="function";
},ensureFieldExists:function(fieldName){if(!this.containsField(fieldName)){this._fieldMap[fieldName]={dependantFieldControls:[]};
this._dataItem[fieldName]=null;
}},notifyFieldChange:function(fieldName,value){if(this.containsField(fieldName)){var ctrls=this._fieldMap[fieldName].dependantFieldControls;
var idx=ctrls.length;
while(idx--){ctrls[idx].set_value(value);
}return true;
}else{return false;
}}};
Telerik.Sitefinity.Web.UI.FieldChangeNotifier.registerClass("Telerik.Sitefinity.Web.UI.FieldChangeNotifier");
