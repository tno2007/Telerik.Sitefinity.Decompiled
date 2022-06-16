Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.LinkField=function(element){Telerik.Sitefinity.Web.UI.Fields.LinkField.initializeBase(this,[element]);
this._element=element;
this._linkFieldButton=null;
this._value=null;
this._commandName=null;
};
Telerik.Sitefinity.Web.UI.Fields.LinkField.prototype={initialize:function(){if(this._linkFieldButton){this._onLinkClickedDelegate=Function.createDelegate(this,this._onLinkClicked);
$addHandler(this._linkFieldButton,"click",this._onLinkClickedDelegate);
}Telerik.Sitefinity.Web.UI.Fields.LinkField.callBaseMethod(this,"initialize");
},dispose:function(){if(this._onLinkClickedDelegate){$removeHandler(this._linkFieldButton,"click",this._onLinkClickedDelegate);
}this._linkFieldButton=null;
Telerik.Sitefinity.Web.UI.Fields.LinkField.callBaseMethod(this,"dispose");
},reset:function(){Telerik.Sitefinity.Web.UI.Fields.LinkField.callBaseMethod(this,"reset");
},get_value:function(){var val=this._value;
return val;
},set_value:function(value){this._value=value;
this.raisePropertyChanged("value");
this._valueChangedHandler();
},isChanged:function(){return false;
},add_command:function(handler){this.get_events().addHandler("command",handler);
},remove_command:function(handler){this.get_events().removeHandler("command",handler);
},get_linkFieldButton:function(){return this._linkFieldButton;
},set_linkFieldButton:function(value){this._linkFieldButton=value;
},_onLinkClicked:function(sender,args){var eventArgs=new Telerik.Sitefinity.CommandEventArgs(this._commandName);
this.onCommand(sender,eventArgs);
},onCommand:function(sender,args){var h=this.get_events().getHandler("command");
if(h){h(this,args);
}},get_commandName:function(){return this._commandName;
},set_commandName:function(value){this._commandName=value;
},set_tabIndex:function(value){this._tabIndex=value;
jQuery(this.get_linkFieldButton()).attr("tabindex",value);
},get_tabIndex:function(){return jQuery(this.get_linkFieldButton()).attr("tabindex");
},focus:function(){var input=this.get_linkFieldButton();
if(jQuery(input).is(":visible")&&jQuery(input).is(":enabled")){input.focus();
}},blur:function(){},set_defaultValue:function(value){if(value===null||value===undefined){value="";
}this._defaultValue=value;
},get_element:function(){return this._element;
},set_element:function(value){this._element=value;
}};
Telerik.Sitefinity.Web.UI.Fields.LinkField.registerClass("Telerik.Sitefinity.Web.UI.Fields.LinkField",Telerik.Sitefinity.Web.UI.Fields.FieldControl,Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext,Telerik.Sitefinity.Web.UI.Fields.ICommandField);