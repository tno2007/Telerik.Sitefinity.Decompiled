Type.registerNamespace("Telerik.Sitefinity.Web.UI.Extenders");
Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender=function(element){this._expanded=null;
this._expandElement=null;
this._expandText=null;
this._expandTarget=null;
this._fieldControl=null;
this._originalExpandedState=false;
this._expandDelegate=null;
this._resetDelegate=null;
this._toggleDelegate=null;
this._resetSectionDelegate=null;
Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender.prototype={initialize:function(){this._originalExpandedState=this._expanded;
this._expandDelegate=Function.createDelegate(this,this.expand);
this._resetDelegate=Function.createDelegate(this,this.reset);
this._toggleDelegate=Function.createDelegate(this,this.toggle);
this._resetSectionDelegate=Function.createDelegate(this,this.resetSection);
if(this._expandElement!=null){$addHandler(this._expandElement,"click",this._expandDelegate);
$addHandler(this._expandElement,"focus",this._expandDelegate);
}if(this._fieldControl!=null){if(this.isSection()){if(this._expandElement!=null){$addHandler(this._expandElement,"click",this._toggleDelegate);
}this._fieldControl.add_reset(this._resetSectionDelegate);
}else{this._fieldControl.add_reset(this._resetDelegate);
this._fieldControl.add_doExpand(this._expandDelegate);
}}Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender.callBaseMethod(this,"initialize");
},dispose:function(){if(this._expandElement){$clearHandlers(this._expandElement);
}if(this._fieldControl){if(this.isSection()){if(this._toggleDelegate){this._fieldControl.remove_doToggle(this._toggleDelegate);
}if(this._resetSectionDelegate){this._fieldControl.remove_reset(this._resetSectionDelegate);
}}else{if(this._resetDelegate){this._fieldControl.remove_reset(this._resetDelegate);
}if(this._expandDelegate){this._fieldControl.remove_doExpand(this._expandDelegate);
}}}if(this._expandDelegate){delete this._expandDelegate;
}if(this._resetDelegate){delete this._resetDelegate;
}if(this._resetSectionDelegate){delete this._resetSectionDelegate;
}if(this._toggleDelegate){delete this._toggleDelegate;
}Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender.callBaseMethod(this,"dispose");
},isSection:function(){if(Object.getTypeName(this._fieldControl)=="Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl"){return true;
}return false;
},inheritsFieldControl:function(){if(Object.getTypeName(this._fieldControl)=="Telerik.Sitefinity.Web.UI.Fields.FieldControl"){return true;
}return false;
},expandSection:function(){var wrapper=$(this._fieldControl.get_wrapperElement());
var toggleTarget=$(this._expandTarget);
wrapper.addClass("sfExpandedForm");
toggleTarget.addClass("sfExpandedTarget");
toggleTarget.removeClass("sfCollapsedTarget");
this._fieldControl.toggle({Action:"expand"});
},collapseSection:function(){var wrapper=$(this._fieldControl.get_wrapperElement());
var toggleTarget=$(this._expandTarget);
wrapper.removeClass("sfExpandedForm");
toggleTarget.removeClass("sfExpandedTarget");
toggleTarget.addClass("sfCollapsedTarget");
this._fieldControl.toggle({Action:"collapse"});
},toggle:function(){var wrapper=$(this._fieldControl.get_wrapperElement());
wrapper.toggleClass("sfExpandedForm");
if(!wrapper.hasClass("sfExpandedForm")){this.collapseSection();
}else{this.expandSection();
}},expand:function(){this.get_expandElement().style.display="none";
this.get_expandTarget().style.display="";
if(this._fieldControl.focus){this._fieldControl.focus();
}},collapse:function(){this.get_expandElement().style.display="";
this.get_expandTarget().style.display="none";
},reset:function(){if(this._originalExpandedState){this.expand();
}else{this.collapse();
}},resetSection:function(){if(this._originalExpandedState){this.expandSection();
}else{this.collapseSection();
}},get_expanded:function(){return this._expanded;
},set_expanded:function(value){this._expanded=value;
},get_expandElement:function(){return this._expandElement;
},set_expandElement:function(value){this._expandElement=value;
},get_expandText:function(){return this._expandText;
},set_expandText:function(value){this._expandText=value;
},get_expandTarget:function(){return this._expandTarget;
},set_expandTarget:function(value){this._expandTarget=value;
},get_fieldControl:function(){return this._fieldControl;
},set_fieldControl:function(value){this._fieldControl=value;
},get_originalExpandedState:function(){return this._originalExpandedState;
}};
Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender.registerClass("Telerik.Sitefinity.Web.UI.Extenders.ExpandableExtender",Sys.UI.Behavior);
