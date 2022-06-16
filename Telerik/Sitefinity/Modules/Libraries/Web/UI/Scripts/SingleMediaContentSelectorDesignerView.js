Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView=function(element){Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView.initializeBase(this,[element]);
this._parentDesigner=null;
this._libraryBinder=null;
this._libraryListBox=null;
this._selectedImageKey=null;
this._bindOnLoad=null;
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView.prototype={initialize:function(){Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView.callBaseMethod(this,"initialize");
if(this._pageLoadDelegate==null){this._pageLoadDelegate=Function.createDelegate(this,this._handlePageLoad);
}if(this._selectLibraryChangedDelegate==null){this._selectLibraryChangedDelegate=Function.createDelegate(this,this._selectedLibraryChanged);
}if(this._libraryListBox!=null){this._libraryListBox.add_selectedIndexChanged(this._selectLibraryChangedDelegate);
}if(this._libraryBinderDataBoundDelegate==null){this._libraryBinderDataBoundDelegate=Function.createDelegate(this,this._libraryBinder_DataBound);
}if(this._libraryBinder!=null){this._libraryBinder.add_onDataBound(this._libraryBinderDataBoundDelegate);
}Sys.Application.add_load(this._pageLoadDelegate);
$(this).on("unload",function(e){jQuery.event.remove(this);
jQuery.removeData(this);
});
},dispose:function(){Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView.callBaseMethod(this,"dispose");
Sys.Application.remove_load(this._pageLoadDelegate);
if(this._pageLoadDelegate){delete this._pageLoadDelegate;
}if(this._libraryListBox){this._libraryListBox.remove_selectedIndexChanged(this._selectLibraryChangedDelegate);
}if(this._selectLibraryChangedDelegate){delete this._selectLibraryChangedDelegate;
}if(this._libraryBinder){this._libraryBinder.remove_onDataBound(this._libraryBinderDataBoundDelegate);
}if(this._libraryBinderDataBoundDelegate){delete this._libraryBinderDataBoundDelegate;
}},refreshUI:function(){},applyChanges:function(){var controlData=this.get_controlData();
if(this._selectedImageKey!=null){controlData.ImageId=this._selectedImageKey.Id;
if(this._imageResizingOptionsControl.get_itemIsResized()){controlData.Width=this._imageResizingOptionsControl.get_resizedWidth();
controlData.OpenOriginalImageOnClick=this._imageResizingOptionsControl.get_resizedItemOpensOriginal();
}else{controlData.Width=0;
controlData.OpenOriginalImageOnClick=false;
}}},rebind:function(providerName){if(this._libraryBinder){this._libraryBinder._provider=providerName;
this._libraryBinder.get_urlParams()["providerName"]=providerName;
this._libraryBinder.DataBind();
}},_handlePageLoad:function(sender,args){this._libraryBinder.get_urlParams()["itemType"]="Telerik.Sitefinity.Libraries.Model.Album";
this._libraryBinder.DataBind();
},_selectedLibraryChanged:function(sender,args){},_libraryBinder_DataBound:function(sender,args){var libraryListBox=$find(sender._targetId);
if(libraryListBox!=null){var item=new Telerik.Web.UI.RadListBoxItem();
if(item){item.set_text(this._firstItemText);
item.set_value("");
}libraryListBox.get_items().insert(0,item);
item.set_selected(true);
}},get_parentDesigner:function(){return this._parentDesigner;
},set_parentDesigner:function(value){if(this._parentDesigner!=value){this._parentDesigner=value;
this.raisePropertyChanged("parentDesigner");
}},get_controlData:function(){return this.get_parentDesigner().get_propertyEditor().get_control();
},get_libraryBinder:function(){return this._libraryBinder;
},set_libraryBinder:function(value){this._libraryBinder=value;
},get_libraryListBox:function(){return this._libraryListBox;
},set_libraryListBox:function(value){this._libraryListBox=value;
},get_firstItemText:function(){return this._firstItemText;
},set_firstItemText:function(value){this._firstItemText=value;
},get_bindOnLoad:function(){return this._bindOnLoad;
},set_bindOnLoad:function(value){this._bindOnLoad=value;
}};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentSelectorDesignerView",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
