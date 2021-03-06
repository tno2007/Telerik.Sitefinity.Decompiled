Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl=function(element){this._title=null;
this._example=null;
this._description=null;
this._titleElement=null;
this._exampleElement=null;
this._descriptionElement=null;
this._fieldIds=[];
this._displayMode=null;
this._fieldName=null;
Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.callBaseMethod(this,"dispose");
},get_title:function(){return this._title;
},set_title:function(value){this._title=value;
},get_example:function(){return this._example;
},set_example:function(value){this._example=value;
},get_description:function(){return this._description;
},set_description:function(value){this._description=value;
},get_titleElement:function(){return this.titleElement;
},set_titleElement:function(value){this._titleElement=value;
},get_exampleElement:function(){return this._exampleElement;
},set_exampleElement:function(value){this._exampleElement=value;
},get_descriptionElement:function(){return this._descriptionElement;
},set_descriptionElement:function(value){this._descriptionElement=value;
},get_displayMode:function(){return this._displayMode;
},set_displayMode:function(value){this._displayMode=value;
},get_fieldIds:function(){return this._fieldIds;
},set_fieldIds:function(value){this._fieldIds=value;
},get_fieldName:function(){return this._fieldName;
},set_fieldName:function(value){this._fieldName=value;
}};
Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.registerClass("Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl",Sys.UI.Control,Telerik.Sitefinity.Web.UI.Fields.IField);
