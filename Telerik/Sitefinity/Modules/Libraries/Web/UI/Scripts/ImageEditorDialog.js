Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images");
Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog=function(element){Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog.initializeBase(this,[element]);
this._cancelButton=null;
this._saveButton=null;
this._saveAsButton=null;
this._imageEditor=null;
this._imageId="";
this._isSave=false;
this._imageWidth=null;
this._imageHeight=null;
};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog.prototype={initialize:function(){Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog.callBaseMethod(this,"initialize");
this._attachHandlers(true);
jQuery(this.get_imageEditor().get_element()).find("li.rtbItem.rtbBtn:last").hide();
this.organizeToolGroups();
Telerik.Web.UI.ImageEditor.CommandList.SaveAs=Telerik.Web.UI.ImageEditor.CommandList.Crop;
document.body.className+=" sfSelectorDialog";
this._enableSaveButtons(false);
},dispose:function(){this._attachHandlers(false);
Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog.callBaseMethod(this,"dispose");
},organizeToolGroups:function(){var groups=this.get_imageEditor().get_toolGroups();
for(var i=0;
i<groups.length;
i++){var group=groups[i];
if(jQuery.grep(group.get_allItems(),this._isZoomCommand)){jQuery(group.get_element()).css({"float":"right"});
break;
}}},_isZoomCommand:function(item){return item.get_commandName().indexOf("Zoom")>-1;
},_attachHandlers:function(toAttach){this._attachButtonHandlers(toAttach);
this._attachImageEditorHandlers(toAttach);
},_attachImageEditorHandlers:function(toAttach){var imageEditor=this.get_imageEditor();
if(toAttach){this._imageChangedDelegate=Function.createDelegate(this,this._imageChanged);
imageEditor.add_imageChanging(this._imageChangedDelegate);
this._commandExecutingDelegate=Function.createDelegate(this,this._commandExecuting);
imageEditor.add_commandExecuting(this._commandExecutingDelegate);
var editableImage=imageEditor.getEditableImage();
this._savingDelegate=Function.createDelegate(this,this._saving);
this._fixImageOnSaveDelegate=Function.createDelegate(this,this._fixImageOnSave);
this._originalEditableImageFinishOp=editableImage._finishOperation;
this._saveSuccessDelegate=Function.createDelegate(this,this._saveSuccess);
jQuery.extend(editableImage,{_finishSave:this._savingDelegate,_finishOperation:this._fixImageOnSaveDelegate,set_serverUrl:this._saveSuccessDelegate});
}else{delete this._savingDelegate;
delete this._saveSuccessDelegate;
if(this._imageChangedDelegate){imageEditor.remove_imageChanging(this._imageChangedDelegate);
delete this._imageChangedDelegate;
}if(this._commandExecutingDelegate){imageEditor.remove_commandExecuting(this._commandExecutingDelegate);
delete this._commandExecutingDelegate;
}}},_attachButtonHandlers:function(toAttach){var cancelButton=this.get_cancelButton();
var saveButton=this.get_saveButton();
var saveAsButton=this.get_saveAsButton();
if(toAttach){this._cancelButtonClickDelegate=Function.createDelegate(this,this._cancelButtonClick);
$addHandler(cancelButton,"click",this._cancelButtonClickDelegate);
this._saveButtonClickDelegate=Function.createDelegate(this,this._saveButtonClick);
$addHandler(saveButton,"click",this._saveButtonClickDelegate);
if(saveAsButton){this._saveAsButtonClickDelegate=Function.createDelegate(this,this._saveAsButtonClick);
$addHandler(saveAsButton,"click",this._saveAsButtonClickDelegate);
}}else{if(this._cancelButtonClickDelegate){$removeHandler(cancelButton,"click",this._cancelButtonClickDelegate);
delete this._cancelButtonClickDelegate;
}if(this._saveButtonClickDelegate){$removeHandler(saveButton,"click",this._saveButtonClickDelegate);
delete this._saveButtonClickDelegate;
}if(this._saveAsButtonClickDelegate){$removeHandler(saveAsButton,"click",this._saveAsButtonClickDelegate);
delete this._saveAsButtonClickDelegate;
}}},_saveButtonClick:function(args){if(!jQuery(this.get_saveButton()).hasClass("sfDisabledLinkBtn")){this._imageWidth=this.get_imageEditor().getEditableImage().get_width();
this._imageHeight=this.get_imageEditor().getEditableImage().get_height();
this.get_imageEditor().saveImageOnServer("",true);
}},_saveAsButtonClick:function(args){if(!jQuery(this.get_saveAsButton()).hasClass("sfDisabledLinkBtn")){this.get_imageEditor().fire("SaveAs");
}},_cancelButtonClick:function(args){this._closeDialog("cancel");
},_saving:function(){this._isSave=true;
},_saveSuccess:function(guid,args){this.get_imageEditor().getEditableImage()._serverUrl=guid;
if(this._isSave){this.set_imageId(guid);
this._isSave=false;
var x;
if(args.indexOf("|")!==-1){x=args.split("|");
}else{if(args.indexOf("%7C")!==-1){x=args.split("%7C");
}else{if(args.indexOf("%7c")!==-1){x=args.split("%7c");
}else{x=[args];
}}}var newArgs={ProviderName:x[0],SaveArgument:(x.length>1?x[1]:"")};
this._closeDialog("save",newArgs);
}},_fixImageOnSave:function(serverData,context,callback){var editableImage=this.get_imageEditor().getEditableImage();
if(context.data.name=="Save"){var data=editableImage._readJson(serverData);
if(typeof(callback)=="function"){callback.call(editableImage,context.data);
}if(data.serverUrl){editableImage.set_serverUrl(data.serverUrl,data.args);
}editableImage._alertMessage(data.alertMessage);
}else{this._originalEditableImageFinishOp.apply(editableImage,arguments);
}},_commandExecuting:function(sender,args){var cmdName=args.get_commandName();
if(cmdName=="Crop"||cmdName=="SaveAs"||cmdName=="Resize"){var hiddenInput=$get(sender.get_clientStateFieldID());
var index=hiddenInput.value.lastIndexOf(',"clientOps');
var csValue=hiddenInput.value.substring(0,index);
hiddenInput.value=csValue+',"clientOps":"[]"}';
}},_imageChanged:function(sender,args){if(args.get_commandName()!="Zoom"){this._enableSaveButtons(args.get_commandName()!="Reset");
}},_enableSaveButtons:function(toEnable){if(toEnable){jQuery(this.get_saveButton()).removeClass("sfDisabledLinkBtn").addClass("sfSave");
jQuery(this.get_saveAsButton()).removeClass("sfDisabledLinkBtn");
}else{jQuery(this.get_saveButton()).addClass("sfDisabledLinkBtn").removeClass("sfSave");
jQuery(this.get_saveAsButton()).addClass("sfDisabledLinkBtn");
}},_closeDialog:function(value,params){if(value=="save"){this.close({CommandName:value,ImageId:this.get_imageId(),DisableHistory:true,ProviderName:params.ProviderName,SaveArgument:params.SaveArgument,Width:this._imageWidth,Height:this._imageHeight});
return;
}var editor=this.get_imageEditor();
var xmlHttpPanel=$find(editor.get_id()+"_eiXHPanel");
this._closeDialogEndedDelegate=Function.createDelegate(this,this._closeDialogEnded);
xmlHttpPanel.add_responseEnding(this._closeDialogEndedDelegate);
xmlHttpPanel.set_value(value);
},_closeDialogEnded:function(sender,args){sender.remove_responseEnding(this._closeDialogEndedDelegate);
delete this._closeDialogEndedDelegate;
this.close({CommandName:sender.get_value(),ImageId:this.get_imageId(),DisableHistory:true});
},get_imageEditor:function(){return this._imageEditor;
},set_imageEditor:function(value){this._imageEditor=value;
},get_saveButton:function(){return this._saveButton;
},set_saveButton:function(value){this._saveButton=value;
},get_saveAsButton:function(){return this._saveAsButton;
},set_saveAsButton:function(value){this._saveAsButton=value;
},get_cancelButton:function(){return this._cancelButton;
},set_cancelButton:function(value){this._cancelButton=value;
},get_imageId:function(){return this._imageId;
},set_imageId:function(value){this._imageId=value;
},raiseEvent:function(eventName,eventArgs){var handler=this.get_events().getHandler(eventName);
if(handler){if(!eventArgs){eventArgs=Sys.EventArgs.Empty;
}handler(this,eventArgs);
}}};
Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImageEditorDialog",Telerik.Sitefinity.Web.UI.AjaxDialogBase);
