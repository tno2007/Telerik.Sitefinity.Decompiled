Type.registerNamespace("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI");
var emailTemplateEditor=null;
Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor=function(element){Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.initializeBase(this,[element]);
};
Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.prototype={initialize:function(){Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.callBaseMethod(this,"initialize");
emailTemplateEditor=this;
},dispose:function(){Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.callBaseMethod(this,"dispose");
},reset:function(){this._otherPropertiesContainerExpanded=false;
this._messageControl.hide();
},saveChanges:function(){this._isDirty=true;
this._binder.SaveChanges();
},_pageLoadHandler:function(sender,args){jQuery("body").addClass("sfFormDialog sfWidgetTmpDialog");
this._binder.set_fieldControlIds(this._fieldControlIds);
},_dataBindSuccess:function(sender,result){this.Caller._binder.BindItem(result);
}};
Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor.registerClass("Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.EmailTemplateEditor",Telerik.Sitefinity.Modules.ControlTemplates.Web.UI.ControlTemplateEditor);
