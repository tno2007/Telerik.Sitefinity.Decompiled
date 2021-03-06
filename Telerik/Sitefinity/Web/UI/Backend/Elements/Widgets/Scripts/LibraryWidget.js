Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget=function(element){Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget.initializeBase(this,[element]);
this._showActionMenu=null;
this._itemName=null;
this._itemsName=null;
this._libraryName=null;
this._supportsReordering=null;
this._name=null;
this._cssClass=null;
this._isSeparator=null;
this._wrapperTagId=null;
this._wrapperTagName=null;
};
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget.callBaseMethod(this,"initialize");
Sys.Application.add_load(Function.createDelegate(this,this._loadHandler));
},dispose:function(){Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget.callBaseMethod(this,"dispose");
},_loadHandler:function(sender,args){this.get_binder().add_onItemDataBound(Function.createDelegate(this,this._binderItemDataBoundHandler));
this.get_binder().add_onItemCommand(Function.createDelegate(this,this._binderItemCommandHandler));
},_binderItemDataBoundHandler:function(sender,args){var jItemElement=$(args.get_itemElement());
var itemsText=jItemElement.find(".sfLibItemsText").get(0);
if(args.get_dataItem().ItemsCount==1){itemsText.text=this._itemName;
itemsText.innerHTML=this._itemName;
}else{itemsText.text=this._itemsName;
itemsText.innerHTML=this._itemsName;
}var mainActionMenuLink=jItemElement.find("a[menu='actions']").get(0);
mainActionMenuLink.innerHTML=String.format(mainActionMenuLink.innerHTML,this._libraryName);
var otherActionMenuItems=jItemElement.find("ul.actionsMenu ul li a").get();
var supportsReordering=this._supportsReordering;
var itemsName=this._itemsName;
var libraryName=this._libraryName;
jQuery.each(otherActionMenuItems,function(){if(jQuery(this).hasClass("sf_binderCommand_reorder")){if(supportsReordering){this.innerHTML+=itemsName;
}else{jQuery(this).hide();
}}else{this.innerHTML=String.format(this.innerHTML,libraryName);
}});
$(args.get_itemElement()).find(".actionsMenu").clickMenu();
},_binderItemCommandHandler:function(sender,args){var commandEventArgs=new Telerik.Sitefinity.UI.CommandEventArgs(args.get_commandName(),args.get_dataItem());
var h=this.get_events().getHandler("command");
if(h){h(this,commandEventArgs);
}},add_command:function(handler){this.get_events().addHandler("command",handler);
},remove_command:function(handler){this.get_events().removeHandler("command",handler);
},get_name:function(){return this._name;
},set_name:function(value){if(this._name!=value){this._name=value;
this.raisePropertyChanged("name");
}},get_cssClass:function(){return this._cssClass;
},set_cssClass:function(value){if(this._cssClass!=value){this._cssClass=value;
this.raisePropertyChanged("cssClass");
}},get_isSeparator:function(){return this._isSeparator;
},set_isSeparator:function(value){if(this._isSeparator!=value){this._isSeparator=value;
this.raisePropertyChanged("isSeparator");
}},get_wrapperTagId:function(){return this._wrapperTagId;
},set_wrapperTagId:function(value){if(this._wrapperTagId!=value){this._wrapperTagId=value;
this.raisePropertyChanged("wrapperTagId");
}},get_wrapperTagName:function(){return this._wrapperTagName;
},set_wrapperTagName:function(value){if(this._wrapperTagName!=value){this._wrapperTagName=value;
this.raisePropertyChanged("wrapperTagName");
}},get_supportsReordering:function(){return this._supportsReordering;
},set_supportsReordering:function(value){this._supportsReordering=value;
}};
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget",Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget,Telerik.Sitefinity.UI.ICommandWidget);
