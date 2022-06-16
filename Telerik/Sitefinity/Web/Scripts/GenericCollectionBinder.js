Telerik.Sitefinity.Web.UI.GenericCollectionBinder=function(){this._selectedValue=null;
this._selectFirst=false;
this._selectLast=false;
Telerik.Sitefinity.Web.UI.GenericCollectionBinder.initializeBase(this);
};
Telerik.Sitefinity.Web.UI.GenericCollectionBinder.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.GenericCollectionBinder.callBaseMethod(this,"initialize");
this._serviceCallback=this.BindCollection;
},dispose:function(){Telerik.Sitefinity.Web.UI.GenericCollectionBinder.callBaseMethod(this,"dispose");
},DataBind:function(){Telerik.Sitefinity.Web.UI.GenericCollectionBinder.callBaseMethod(this,"DataBind");
var clientManager=this.get_manager();
clientManager.set_urlParams(this.get_urlParams());
clientManager.GetItemCollection(this);
},SaveChanges:function(){if($(document.forms[0]).validate().form()==false){return false;
}this._savingHandler();
return true;
},BindCollection:function(data){data=this.DeserializeData(data);
this._dataBindingHandler(data);
this.ClearTarget();
var target=this.GetTarget();
var template=new Sys.UI.Template($get(this._clientTemplates[0]));
for(var i=0;
i<data.Items.length;
i++){var dataItem=data.Items[i];
this.LoadDataItemKey(dataItem);
var key=this.GetItemKey(i);
this._itemDataBindingHandler(dataItem);
this.LoadItem(this.GetTarget(),template,dataItem,data.IsGeneric,i);
var itemElement;
itemElement=jQuery(target).children()[i];
$(itemElement).find("[for]").each(function(i1,element){var attrUnique=$(element).attr("for")+i;
$(element).attr("for",attrUnique);
});
this.BuildInputWrappers(itemElement);
this.AssignCommands(itemElement,dataItem,key,i);
this.ApplyValidationRules(itemElement,i);
this._itemDataBoundHandler(key,dataItem,i,itemElement);
}if(this._selectedValue){var selValue=this._selectedValue;
if($(target).hasClass("radioList")){$(target).find('input:[value="'+selValue+'"]').each(function(){$(this).attr("checked","checked");
});
}else{if($(target).hasClass("checkboxList")){$(target).find('input:[value="'+selValue+'"]').each(function(){$(this).attr("checked","checked");
});
}else{if($(target).hasClass("optionList")){$(target).find("option").each(function(){if($(this).val()==selValue){$(this).attr("selected","selected");
return false;
}});
}}}this._selectedValue=null;
}else{if(this._selectFirst){this.SelectFirstOrLast(target,true);
this._selectFirst=false;
}else{if(this._selectLast){this.SelectFirstOrLast(target,false);
this._selectLast=false;
}}}this._dataBoundHandler(data);
},SelectFirstOrLast:function(target,value){var attr=value?"first":"last";
if($(target).hasClass("radioList")||$(target).hasClass("checkboxList")){$(target).find("input[type='radio']:"+attr).each(function(){$(this).attr("checked","checked");
});
}else{if($(target).hasClass("optionList")){$(target).find("option:"+attr).each(function(){$(this).attr("selected","selected");
});
}}},BindItem:function(data){alert("write here why this does not work!");
},get_selectedValue:function(){return this._selectedValue;
},set_selectedValue:function(value){if(this._selectedValue!=value){this._selectedValue=value;
this.raisePropertyChanged(value);
}},SelectFirst:function(){this._selectFirst=true;
},SelectLast:function(){this._selectLast=true;
}};
Telerik.Sitefinity.Web.UI.GenericCollectionBinder.registerClass("Telerik.Sitefinity.Web.UI.GenericCollectionBinder",Telerik.Sitefinity.Web.UI.ClientBinder);