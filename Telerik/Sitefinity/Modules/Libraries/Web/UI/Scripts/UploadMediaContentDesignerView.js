Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView=function(element){Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView.initializeBase(this,[element]);
this._parentDesigner=null;
this._uploadServiceUrl=null;
this._selectFileButton=null;
this._fileNameTextBox=null;
this._cantUploadFilesErrorMessage=null;
this._fileAllowedExtensions=null;
this._libraryItemType=null;
this._contentType=null;
this._providerName=null;
this._managerType=null;
this._contentId=null;
this._fileUploadedDelegate=null;
this._ajaxUpload=null;
this._beforeSaveChangesDelegate=null;
this._settingsPanel=null;
this._uploadTabName="uploadMediaDesignerView";
this._libraryNotSelectedErrorMessage=null;
this._bindOnLoad=null;
this._parentLibrarySelector=null;
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView.prototype={initialize:function(){Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView.callBaseMethod(this,"initialize");
if(this._libraryItemType==null){this._libraryItemType="Sitefinity.Libraries.Model.Library";
}if(this._selectFileButton){this._fileSubmitDelegate=Function.createDelegate(this,this._fileSubmitHandler);
this._fileChangedDelegate=Function.createDelegate(this,this._fileChangedHandler);
this._fileUploadedDelegate=Function.createDelegate(this,this._fileUploadedHandler);
this._ajaxUpload=new AjaxUpload(this._selectFileButton.id,{action:this._uploadServiceUrl,autoSubmit:false,responseType:false,onChange:this._fileChangedDelegate,onSubmit:this._fileSubmitDelegate,onComplete:this._fileUploadedDelegate});
}else{throw"File upload element not found!";
}this.attachEventHandlers();
},dispose:function(){Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView.callBaseMethod(this,"dispose");
Sys.Application.remove_load(this._pageLoadDelegate);
if(this._pageLoadDelegate){delete this._pageLoadDelegate;
}if(this._fileUploadedDelegate){delete this._fileUploadedDelegate;
}if(this._fileSubmitDelegate){delete this._fileSubmitDelegate;
}if(this.get_propertyEditor()){this.get_propertyEditor().remove_beforeSaveChanges(this._beforeSaveChangesDelegate);
}if(this._beforeSaveChangesDelegate){delete this._beforeSaveChangesDelegate;
}this._ajaxUpload=null;
},rebind:function(providerName){this._providerName=providerName;
this.get_parentLibrarySelector().rebind(providerName);
},refreshUI:function(){},applyChanges:function(){this._setControlData(this.get_controlData());
},get_controlData:function(){return this.get_parentDesigner().get_propertyEditor().get_control();
},isFileNameSet:function(){return !!this.get_fileNameTextBox().value;
},attachEventHandlers:function(){if(this._pageLoadDelegate==null){this._pageLoadDelegate=Function.createDelegate(this,this._pageLoadHandler);
}Sys.Application.add_load(this._pageLoadDelegate);
},detachEventHandlers:function(){Sys.Application.remove_load(this._pageLoadDelegate);
delete this._pageLoadDelegate;
},_fileUploadedHandler:function(file,response){this._hideLoadingSign();
var id=null;
try{var responseMessage=eval("("+response+")");
if(!responseMessage.UploadResult){alert(responseMessage.ErrorMessage);
jQuery(this._fileNameTextBox).val("");
}else{this._contentId=responseMessage.ContentId;
this._readUploadResponseData(responseMessage);
this.get_propertyEditor().saveEditorChanges();
}}catch(e){alert(response);
}},_fileSubmitHandler:function(file,extension){this._showLoadingSign();
var libraryId=this.get_parentLibrarySelector().get_value();
if(libraryId){var uploadData={ContentType:this._contentType,ProviderName:this._providerName,LibraryId:libraryId,RecompileItemUrls:"true"};
this._setAdditionalUploadData(uploadData);
this._ajaxUpload.setData(uploadData);
}},_validateFileExtension:function(extension){var doNotValidate=this._fileAllowedExtensions==null||this._fileAllowedExtensions=="";
return doNotValidate||this._fileAllowedExtensions.indexOf(extension.toLowerCase())!=-1;
},_fileChangedHandler:function(file,extension){if(!this._validateFileExtension(extension)){var message=String.format(this._cantUploadFilesErrorMessage,"."+extension,this._fileAllowedExtensions);
alert(message);
return false;
}jQuery(this.get_settingsPanel()).show();
jQuery(this._fileNameTextBox).val(file);
this.get_parentDesigner().set_saveButtonEnabled(this.isFileNameSet());
dialogBase.resizeToContent();
},_pageLoadHandler:function(){this._beforeSaveChangesDelegate=Function.createDelegate(this,this._beforeSaveChangesHandler);
this.get_propertyEditor().add_beforeSaveChanges(this._beforeSaveChangesDelegate);
},_beforeSaveChangesHandler:function(sender,cancelEventArgs){var parentDesigner=this.get_parentDesigner();
var selectedTab=parentDesigner.get_menuTabStrip().get_selectedTab();
var tabValue=selectedTab.get_value();
if(tabValue==this._uploadTabName){var libraryId=this.get_parentLibrarySelector().get_value();
if(!libraryId){alert(this._libraryNotSelectedErrorMessage);
cancelEventArgs.set_cancel(true);
return;
}var fileUploading=(this._ajaxUpload.get_input()&&this._ajaxUpload.get_input().value!="");
if(fileUploading){this._ajaxUpload.submit();
cancelEventArgs.set_cancel(true);
}}},_setAdditionalUploadData:function(uploadData){},_readUploadResponseData:function(response){},_setControlData:function(controlData){},_showLoadingSign:function(){var windowContent=this._getWindowContent();
windowContent.addClass("rwLoading");
windowContent.children().css({visibility:"hidden"});
},_hideLoadingSign:function(){var windowContent=this._getWindowContent();
windowContent.removeClass("rwLoading");
windowContent.children().css({visibility:""});
},_getWindowContent:function(){return jQuery(this.get_propertyEditor().get_radWindow()._contentCell);
},get_settingsPanel:function(){return this._settingsPanel;
},set_settingsPanel:function(value){this._settingsPanel=value;
},get_selectFileButton:function(){return this._selectFileButton;
},set_selectFileButton:function(value){this._selectFileButton=value;
},get_fileNameTextBox:function(){return this._fileNameTextBox;
},set_fileNameTextBox:function(value){this._fileNameTextBox=value;
},get_parentDesigner:function(){return this._parentDesigner;
},set_parentDesigner:function(value){this._parentDesigner=value;
},get_propertyEditor:function(){if(this.get_parentDesigner()){return this.get_parentDesigner().get_propertyEditor();
}return null;
},get_cantUploadFilesErrorMessage:function(){return this._cantUploadFilesErrorMessage;
},set_cantUploadFilesErrorMessage:function(value){this._cantUploadFilesErrorMessage=value;
},get_fileAllowedExtensions:function(){return this._fileAllowedExtensions;
},set_fileAllowedExtensions:function(value){this._fileAllowedExtensions=value;
},get_libraryNotSelectedErrorMessage:function(){return this._libraryNotSelectedErrorMessage;
},set_libraryNotSelectedErrorMessage:function(value){this._libraryNotSelectedErrorMessage=value;
},get_bindOnLoad:function(){return this._bindOnLoad;
},set_bindOnLoad:function(value){this._bindOnLoad=value;
},get_parentLibrarySelector:function(){return this._parentLibrarySelector;
},set_parentLibrarySelector:function(value){this._parentLibrarySelector=value;
}};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView",Sys.UI.Control,Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
if(typeof(Sys)!=="undefined"){Sys.Application.notifyScriptLoaded();
}