Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.Designers.Views");
Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView=function(element){Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.initializeBase(this,[element]);
this._viewsSelector=null;
};
Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.callBaseMethod(this,"dispose");
},refreshUI:function(){var selectedViews=this.get_controlData().VisibleViews;
var hidden=this.get_controlData().Hidden;
if(selectedViews&&selectedViews.length>0){this.get_viewsSelector().setSelectedViews(selectedViews);
}else{if(hidden){this.get_viewsSelector().setSelectedViews("nowhere");
}else{this.get_viewsSelector().setSelectedViews("allViews");
}}Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.callBaseMethod(this,"refreshUI");
},applyChanges:function(){var selectedViews=this.get_viewsSelector().getSelectedViews();
this.get_controlData().VisibleViews=selectedViews;
if(selectedViews==="allViews"){this.get_controlData().Hidden="false";
}else{if(selectedViews==="nowhere"){this.get_controlData().Hidden="true";
}}Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.callBaseMethod(this,"applyChanges");
},get_viewsSelector:function(){return this._viewsSelector;
},set_viewsSelector:function(value){this._viewsSelector=value;
}};
Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView.registerClass("Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ChoiceFieldAppearanceView",Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView);