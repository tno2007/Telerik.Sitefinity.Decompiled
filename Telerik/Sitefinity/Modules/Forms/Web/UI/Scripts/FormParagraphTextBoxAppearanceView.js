Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView=function(element){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView.initializeBase(this,[element]);
this._textBoxSizeChoiceField=null;
this._cssClassTextField=null;
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView.prototype={initialize:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView.callBaseMethod(this,"dispose");
},refreshUI:function(){var controlData=this.get_controlData();
this._cssClassTextField.set_value(controlData.CssClass);
this._textBoxSizeChoiceField.set_value(controlData.ParagraphTextBoxSize);
},applyChanges:function(){var controlData=this.get_controlData();
controlData.CssClass=this._cssClassTextField.get_value();
controlData.ParagraphTextBoxSize=this._textBoxSizeChoiceField.get_value();
},get_controlData:function(){return this.get_parentDesigner().get_propertyEditor().get_control();
},get_parentDesigner:function(){return this._parentDesigner;
},set_parentDesigner:function(value){this._parentDesigner=value;
},get_propertyEditor:function(){if(this.get_parentDesigner()){return this.get_parentDesigner().get_propertyEditor();
}return null;
},get_textBoxSizeChoiceField:function(){return this._textBoxSizeChoiceField;
},set_textBoxSizeChoiceField:function(value){this._textBoxSizeChoiceField=value;
},get_cssClassTextField:function(){return this._cssClassTextField;
},set_cssClassTextField:function(value){this._cssClassTextField=value;
}};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView.registerClass("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);