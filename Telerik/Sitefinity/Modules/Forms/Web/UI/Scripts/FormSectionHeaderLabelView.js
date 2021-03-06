Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView=function(element){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.initializeBase(this,[element]);
this._titleTextField=null;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.prototype={initialize:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.callBaseMethod(this,"dispose");
},refreshUI:function(){var controlData=this.get_controlData();
this._titleTextField.set_value(controlData.Title);
},applyChanges:function(){var controlData=this.get_controlData();
controlData.Title=this._titleTextField.get_value();
},get_controlData:function(){return this.get_parentDesigner().get_propertyEditor().get_control();
},get_parentDesigner:function(){return this._parentDesigner;
},set_parentDesigner:function(value){this._parentDesigner=value;
},get_propertyEditor:function(){if(this.get_parentDesigner()){return this.get_parentDesigner().get_propertyEditor();
}return null;
},get_titleTextField:function(){return this._titleTextField;
},set_titleTextField:function(value){this._titleTextField=value;
}};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
