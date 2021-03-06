Type.registerNamespace("Telerik.Sitefinity.Web.UI");
window.console=window.console||{};
if(typeof(window.console.log)==="undefined"){window.console.log=window.alert;
}Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog=function(element){this._imageData;
this._serviceBase;
this._itemType;
this._parentItemType;
this._saveLink=null;
this._cancelLink=null;
this._librarySelectorField=null;
this._extendedDetailsPanel=null;
this._closeDialogCancelDelegate=null;
this._closeDialogSaveDelegate=null;
this._loadDelegate=null;
this._bodyCssClass=null;
this._updateImagePropertiesDelegate=null;
Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog.callBaseMethod(this,"initialize");
this._closeDialogCancelDelegate=Function.createDelegate(this,this._closeDialogCancel);
this._closeDialogSaveDelegate=Function.createDelegate(this,this._closeDialogSave);
this._loadDelegate=Function.createDelegate(this,this._load);
if(this._bodyCssClass){jQuery("body").addClass(_bodyCssClass);
}Sys.Application.add_load(this._loadDelegate);
},dispose:function(){if(this._closeDialogCancelDelegate){delete this._closeDialogCancelDelegate;
}if(this._closeDialogSaveDelegate){delete this._closeDialogSaveDelegate;
}if(this._loadDelegate){Sys.Application.remove_load(this._loadDelegate);
delete this._loadDelegate;
}if(this._librarySelectorField){delete this._librarySelectorField;
}Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog.callBaseMethod(this,"dispose");
},setImageProperties:function(image){this._displayFileName(image.FileName);
this._displayFileSize(image.FileSize);
this._displayUploadedDate(image.UploadedDate);
this.get_librarySelectorField().control.reset();
this._displayTitle(image.Title);
this._displayAltText(image.AlternativeText.Value);
this._displayImageSize(image.ImageDimensions);
this._displayThumbnail(image);
this._getImageData(image);
},_load:function(){this._initializeHandlers();
},_closeDialogCancel:function(){if(this._imageData){this._unlockImage(this._imageData.Item.Id);
}this.close();
},_closeDialogSave:function(sender,args){if(this._updateImagePropertiesDelegate){this._updateImagePropertiesDelegate(null);
}if(this._imageData){this._saveImageData(this._imageData);
}this.close();
},_getImageData:function(image){var urlFormat=this.get_serviceBase()+"/parent/{0}/{1}/?itemType={2}&parentItemType={3}&checkOut=true";
var itemType=this.get_itemType();
var parentItemType=this.get_parentItemType();
var url=String.format(urlFormat,image.AlbumId,image.Id,itemType,parentItemType);
var succeeded=function(sender,result){sender._imageData=result;
image.Title=result.Item.Title.Value;
image.AlternativeText=result.Item.AlternativeText;
image.FileName=result.Item.UrlName.Value+result.Item.Extension;
image.Album=result.Item.Album.Title.Value;
sender._displayFileName(image.FileName);
sender._displayAlbum(image.Album);
sender._displayTitle(image.Title);
sender._displayAltText(image.AlternativeText.Value);
sender._displayImageSize(image.ImageDimensions);
};
var failed=function(error,caller){console.log(error);
};
this.get_clientManager().InvokeGet(url,[],[],succeeded,failed,this);
},_saveImageData:function(imageData){var urlFormat=this.get_serviceBase()+"/parent/{0}/{1}/?itemType={2}&parentItemType={3}&workflowOperation=Publish";
var itemType=this.get_itemType();
var parentItemType=this.get_parentItemType();
var url=String.format(urlFormat,imageData.Item.Album.Id,this.get_clientManager().GetEmptyGuid(),itemType,parentItemType);
var idToUnlock=imageData.Item.Id;
var unlockImage=Function.createDelegate(this,this._unlockImage);
var succeeded=function(sender,result){unlockImage(idToUnlock);
};
var failed=function(error,caller){unlockImage(idToUnlock);
console.log(error);
};
imageData.Item.Title=this._getTitle();
imageData.Item.AlternativeText=this._getAltText();
if(this.get_librarySelectorField().control.isChanged()){var albumId=this.get_librarySelectorField().control.getSelectedParentId();
url=url+"&newParentId="+albumId;
}delete imageData.LastApprovalTrackingRecord;
delete imageData.LifecycleStatus;
delete imageData.PublicationSettings;
delete imageData.VersionInfo;
this.get_clientManager().InvokePut(url,[],[],imageData,succeeded,failed,this);
},_unlockImage:function(idToUnlock){var urlFormat=this.get_serviceBase()+"/temp/{0}/";
var url=String.format(urlFormat,idToUnlock);
var succeeded=function(sender,result){};
var failed=function(error,caller){console.log(error);
};
this.get_clientManager().InvokeDelete(url,[],[],succeeded,failed,this);
},_displayFileName:function(value){this.get_imageOverviewPanel().find(".sfEditImagePropsFileName").html(value);
},_displayFileSize:function(value){this.get_imageOverviewPanel().find(".sfEditImagePropsFileSize").html(value);
},_displayUploadedDate:function(value){this.get_imageOverviewPanel().find(".sfEditImagePropsUploadedDate").html(value);
},_displayAlbum:function(value){this.get_extendedDetailsPanel().find(".sfEditImagePropsAlbum").html(value);
this.get_librarySelectorField().control.reset();
this.get_librarySelectorField().control.get_selectElement().blur();
this.get_librarySelectorField().control.set_value({Title:{Value:value}});
},_displayTitle:function(value){this.get_extendedDetailsPanel().find(".sfEditImagePropsTitle").val(value);
},_displayAltText:function(value){this.get_extendedDetailsPanel().find(".sfEditImagePropsAltText").val(value);
},_displayImageSize:function(value){this.get_extendedDetailsPanel().find(".sfEditImagePropsSize").html(value);
},_displayThumbnail:function(image){if((!image.ThumbnailWidth)&&(!image.ThumbnailHeight)){image.ThumbnailWidth=100;
image.ThumbnailHeight=100;
}this.get_imageOverviewPanel().find(".sfEditImagePropsThumbnail").attr("src",image.Url+".tmb").attr("alt",image.AlternativeText.Value).attr("width",""+image.ThumbnailWidth).attr("height",""+image.ThumbnailHeight);
},_initializeHandlers:function(){jQuery(this.get_cancelLink()).click(this._closeDialogCancelDelegate);
jQuery(this.get_saveLink()).click(this._closeDialogSaveDelegate);
},_getTitle:function(){var title=this.get_extendedDetailsPanel().find(".sfEditImagePropsTitle").val();
return{PersistedValue:title,Value:title};
},_getAltText:function(){var alternativeText=this.get_extendedDetailsPanel().find(".sfEditImagePropsAltText").val();
return{PersistedValue:alternativeText,Value:alternativeText};
},get_clientManager:function(){if(this._clientManager==null){this._clientManager=new Telerik.Sitefinity.Data.ClientManager();
}return this._clientManager;
},set_saveImagePropertiesDelegate:function(editDelegate){this._updateImagePropertiesDelegate=editDelegate;
},get_serviceBase:function(){return this._serviceBase;
},set_serviceBase:function(value){this._serviceBase=value;
},get_itemType:function(){return this._itemType;
},set_itemType:function(value){this._itemType=value;
},get_parentItemType:function(){return this._parentItemType;
},set_parentItemType:function(value){this._parentItemType=value;
},get_imageOverviewPanel:function(){if(!this._imageOverviewPanel){this._imageOverviewPanel=jQuery(".sfImageOverviewPanel");
}return this._imageOverviewPanel;
},get_extendedDetailsPanel:function(){return jQuery(this._extendedDetailsPanel);
},set_extendedDetailsPanel:function(value){this._extendedDetailsPanel=value;
},get_saveLink:function(){return this._saveLink;
},set_saveLink:function(value){this._saveLink=value;
},get_cancelLink:function(){return this._cancelLink;
},set_cancelLink:function(value){this._cancelLink=value;
},get_librarySelectorField:function(){return this._librarySelectorField;
},set_librarySelectorField:function(value){this._librarySelectorField=value;
}};
Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog.registerClass("Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog",Telerik.Sitefinity.Web.UI.AjaxDialogBase);
