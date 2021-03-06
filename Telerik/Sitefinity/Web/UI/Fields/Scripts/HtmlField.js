Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.HtmlField=function(element){this._element=element;
this._editControlId=null;
this._editControl=null;
this._editorLoadDelegate=null;
this._editorDialogsExtender=null;
this._viewControl=null;
this._originalValue=null;
this._editorKeyDownDelegate=null;
this._handleApplicationInitDelegate=null;
this._handlePageLoadDelegate=null;
this._focusDelegate=null;
this._hasChanged=false;
this._editorCommandExecutingDelegate=null;
this._editorCommandExecutedDelegate=null;
this._editorClientPasteHtmlDelegate=null;
this._toolbarMoreToolsLabel=null;
this._toolbarLessToolsLabel=null;
this._fixCursorIssue=null;
this._editorToolbarMode=null;
this._className=null;
this._handlersAdded=false;
this._enabled=true;
this._addDialogShowHandlerDelegate=null;
this._addDialogLoadHandlerDelegate=null;
this._removeDialogShowHandlerDelegate=null;
this._autoSizeEndHandlerDelegate=null;
this._culture=null;
this._uiCulture=null;
Telerik.Sitefinity.Web.UI.Fields.HtmlField.initializeBase(this,[element]);
};
Telerik.Sitefinity.Web.UI.Fields.HtmlField.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Fields.HtmlField.callBaseMethod(this,"initialize");
this._handleApplicationInitDelegate=Function.createDelegate(this,this._handleApplicationInit);
Sys.Application.add_init(this._handleApplicationInitDelegate);
this._handlePageLoadDelegate=Function.createDelegate(this,this._handlePageLoad);
this._focusDelegate=Function.createDelegate(this,this.focus);
$addHandler(this._element,"focus",this._focusDelegate);
Sys.Application.add_load(this._handlePageLoadDelegate);
this._editorCommandExecutingDelegate=Function.createDelegate(this,this._editorCommandExecutingHandler);
this._editorCommandExecutedDelegate=Function.createDelegate(this,this._editorCommandExecutedHandler);
this._editorClientPasteHtmlDelegate=Function.createDelegate(this,this._editorClientPasteHtmlHandler);
this._editorKeyDownDelegate=Function.createDelegate(this,this._editorKeyDownHandler);
this._addDialogShowHandlerDelegate=Function.createDelegate(this,this._addDialogShowHandler);
this._addDialogLoadHandlerDelegate=Function.createDelegate(this,this._addDialogLoadHandler);
this._removeDialogShowHandlerDelegate=Function.createDelegate(this,this._removeDialogShowHandler);
this._autoSizeEndHandlerDelegate=Function.createDelegate(this,this._autoSizeEndHandler);
},dispose:function(){if(this._handleApplicationInitDelegate){Sys.Application.remove_init(this._handleApplicationInitDelegate);
delete this._handleApplicationInitDelegate;
}if(this._handlePageLoadDelegate){Sys.Application.remove_load(this._handlePageLoadDelegate);
delete this._handlePageLoadDelegate;
}if(this._focusDelegate){$removeHandler(this._element,"focus",this._focusDelegate);
delete this._focusDelegate;
}if(this._editorKeyDownDelegate){if(this._editControl){this._editControl.detachEventHandler("onkeydown",this._editorKeyDownDelegate);
}delete this._editorKeyDownDelegate;
}if(this._editControl){this._editControl.remove_commandExecuting(this._editorCommandExecutingDelegate);
this._editControl.remove_commandExecuted(this._editorCommandExecutedDelegate);
this._editControl.remove_pasteHtml(this._editorClientPasteHtmlDelegate);
}if(this._editorCommandExecutingDelegate){delete this._editorCommandExecutingDelegate;
}if(this._editorCommandExecutedDelegate){delete this._editorCommandExecutedDelegate;
}if(this._editorClientPasteHtmlDelegate){delete this._editorClientPasteHtmlDelegate;
}if(this._addDialogShowHandlerDelegate){delete this._addDialogShowHandlerDelegate;
}if(this._addDialogLoadHandlerDelegate){delete this._addDialogLoadHandlerDelegate;
}if(this._autoSizeEndHandlerDelegate){delete this._autoSizeEndHandlerDelegate;
}if(this._removeDialogShowHandlerDelegate){delete this._removeDialogShowHandlerDelegate;
}if(this._editorCommandExecutingDelegate){delete this._editorCommandExecutingDelegate;
}if(this._editorLoadDelegate){delete this._editorLoadDelegate;
}Telerik.Sitefinity.Web.UI.Fields.HtmlField.callBaseMethod(this,"dispose");
},reset:function(){if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){var defaultValue=this.get_defaultValue();
if(defaultValue==undefined||defaultValue==null){defaultValue="";
}if(this._editControl!=null){this._editControl.set_html(defaultValue);
}}else{this._viewControl.innerHTML=defaultValue;
}Telerik.Sitefinity.Web.UI.Fields.HtmlField.callBaseMethod(this,"reset");
},focus:function(){if(this._editControl!=null&&this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){this._editControl.setFocus();
}},blur:function(){},_editorKeyDownHandler:function(e){this._hasChanged=true;
if(e.keyCode==Sys.UI.Key.tab){this._editControl.removeShortCut("InsertTab");
$telerik.cancelRawEvent(e);
this.focusControlByTabKey(e);
}},_editorCommandExecutedHandler:function(editor,args){var commandName=args.get_commandName();
if(commandName=="ToggleScreenMode"){editor.repaint();
}},_editorCommandExecutingHandler:function(editor,args){this._hasChanged=true;
var name=args.get_name();
var val=args.get_value();
var commandName=args.get_commandName();
this._className="re"+commandName;
if(commandName=="ToggleAdvancedToolbars"){this._toggleAdvancedToolbars(editor);
args.set_cancel(true);
}if(editor.get_dialogOpener()){if(editor.get_dialogOpener()._container&&!this._handlersAdded){editor.get_dialogOpener()._container.add_show(this._addDialogShowHandlerDelegate);
editor.get_dialogOpener()._container.add_pageLoad(this._addDialogLoadHandlerDelegate);
editor.get_dialogOpener()._container.add_close(this._removeDialogShowHandlerDelegate);
editor.get_dialogOpener()._container.add_autoSizeEnd(this._autoSizeEndHandlerDelegate);
this._handlersAdded=true;
}}if(name=="MergeTags"){editor.pasteHtml(val);
args.set_cancel(true);
}if(commandName=="ToggleScreenMode"){var containerWnd=this._getRadWindow();
if(!editor.isFullScreen()){$("body").addClass("sfFullScreenEditorWrp");
$(editor.get_element()).addClass("sfFullScreenEditor");
if(containerWnd!=null&&!containerWnd.isMaximized()){containerWnd.maximize();
$(containerWnd.get_element()).addClass("sfWndToBeRestored");
}editor.get_document().body.style.maxWidth="640px";
editor.get_document().body.style.marginLeft="auto";
editor.get_document().body.style.marginRight="auto";
}else{$("body").removeClass("sfFullScreenEditorWrp");
$(editor.get_element()).removeClass("sfFullScreenEditor");
if(containerWnd!=null&&$(containerWnd.get_element()).hasClass("sfWndToBeRestored")){$(containerWnd.get_element()).removeClass("sfWndToBeRestored");
containerWnd.restore();
}editor.get_document().body.style.maxWidth="100%";
}}},_editorClientPasteHtmlHandler:function(sender,args){var newContent=args.get_value();
newContent=newContent.replace(new RegExp("<b(\\s([^>])*?)?>","ig"),"<strong$1>");
newContent=newContent.replace(new RegExp("</b(\\s([^>])*?)?>","ig"),"</strong$1>");
newContent=newContent.replace(new RegExp("<i(\\s([^>])*?)?>","ig"),"<em$1>");
newContent=newContent.replace(new RegExp("</i(\\s([^>])*?)?>","ig"),"</em$1>");
args.set_value(newContent);
},_editorLoadHandler:function(sender,args){this._toggleAdvancedToolbars(sender);
},_handleApplicationInit:function(sender,args){this._editorLoadDelegate=Function.createDelegate(this,this._editorLoadHandler);
if(this._editControl!=null&&this.get_displayMode()===Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){this._editControl.add_load(this._editorLoadDelegate);
}},_handlePageLoad:function(sender,args){if(args.get_isPartialLoad()){return;
}if(this._editControl!=null&&this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){Telerik.Web.UI.RadEditor.prototype._initContentAreaHtml=this._setRadEditorContentAreaHtml;
this._setEditorHtml(this._originalValue);
var element=document.all?this._editControl.get_document().body:this._editControl.get_document();
$telerik.addExternalHandler(element,"blur",Function.createDelegate(this,this._handleEditorBlur));
this._editControl.attachEventHandler("onkeydown",this._editorKeyDownDelegate);
this._editControl.add_commandExecuting(this._editorCommandExecutingDelegate);
this._editControl.add_commandExecuted(this._editorCommandExecutedDelegate);
this._editControl.add_pasteHtml(this._editorClientPasteHtmlDelegate);
if(this._fixCursorIssue=="True"&&navigator.userAgent.indexOf("Firefox/")!=-1){var code="document.getElementById('"+this._editControl.get_contentAreaElement().id+"').blur();";
window.setTimeout(code,400);
}if(this._editorToolbarMode=="Custom"){this._toggleAdvancedToolbars(this._editControl);
}this._editControl.enableEditing(this._enabled);
this._editControl.set_editable(this._enabled);
this._editControl.get_dialogOpener()._container.set_keepInScreenBounds(false);
this._editControl.get_dialogOpener()._container.set_autoSizeBehaviors(4+1);
this._setUnlinkCommand();
}},_handleEditorBlur:function(sender,args){if(this._originalValue!=this._editControl.get_html(true)){this._valueChangedHandler();
}},_toggleAdvancedToolbars:function(editor){var $=$telerik.$;
var newMargin=0;
var editorElement=$(editor.get_element());
var toolbar=$("#"+editor.get_id()+" .reToolbar");
var toolbarWrapper=$("#"+editor.get_id()+" .reToolbarWrapper");
var toggleButton=$("#"+editor.get_id()+" .ToggleAdvancedToolbars");
toggleButton.parent().parent().css("display","inline");
toggleButton.parent().addClass("sfLinkBtn sfChange sfToggleMoreTools");
toggleButton.addClass("sfLinkBtnIn");
if(editorElement.hasClass("sfMoreTools")){editorElement.removeClass("sfMoreTools");
toggleButton.text(this._toolbarLessToolsLabel);
}else{editorElement.addClass("sfMoreTools");
toggleButton.text(this._toolbarMoreToolsLabel);
}if(toggleButton.parent().is(":visible")){newMargin=toggleButton.parent().width()+17;
toolbarWrapper.css("margin-right",newMargin+"px");
}else{newMargin=toggleButton.parent().clone().addClass("sfTobeRemoved").css({position:"absolute",top:"-1000px",left:"-1000px"}).appendTo("body").width()+17;
$(".sfTobeRemoved").remove();
toolbarWrapper.css("margin-right",newMargin+"px");
}toolbarWrapper.addClass("sfClearfix");
toolbar.each(function(index){if(index>0){$(this).css("display",$(this).css("display")=="block"?"none":"block");
}editor._updateEditorSize(editor.get_element().style.height);
});
},_addDialogShowHandler:function(sender,args){if(typeof dialogBase!="undefined"){var parentWindow=dialogBase.get_radWindow();
var parentHeightBefore=parentWindow.get_height();
var parentWidthBefore=parentWindow.get_width();
parentWindow.set_height(parentHeightBefore*2);
parentWindow.set_width(parentWidthBefore*2);
}sender.set_keepInScreenBounds(false);
sender.set_autoSizeBehaviors(5);
if(args&&args.isPageLoadEvent){if(typeof dialogBase!="undefined"&&!parentWindow.IsMaximized()){sender.autoSize();
sender.moveTo(50,50);
}}var popupElement=sender.get_popupElement();
Sys.UI.DomElement.addCssClass(popupElement,this._className+" reDlg");
setTimeout(function(){Telerik.Sitefinity.centerWindowHorizontally(sender);
},0);
if(typeof dialogBase!="undefined"){parentWindow.set_height(parentHeightBefore);
parentWindow.set_width(parentWidthBefore);
parentWindow.autoSize();
}sender.remove_close(this.onWindowClose);
sender.add_close(this.onWindowClose);
},_addDialogLoadHandler:function(sender,args){var that=this;
window.setTimeout(function(){if(!args){args={};
}args.isPageLoadEvent=true;
that._addDialogShowHandler(sender,args);
},0);
},onWindowClose:function(sender,eventArgs){if(typeof dialogBase!="undefined"){if(dialogBase){dialogBase.get_radWindow().autoSize();
}}sender.remove_close(this.onWindowClose);
},_removeDialogShowHandler:function(sender,args){var popupElement=sender.get_popupElement();
Sys.UI.DomElement.removeCssClass(popupElement,this._className);
},_autoSizeEndHandler:function(sender,args){},_getRadWindow:function(){var oWindow=null;
if(window.radWindow){oWindow=window.radWindow;
}else{if(window.frameElement&&window.frameElement.radWindow){oWindow=window.frameElement.radWindow;
}}return oWindow;
},_setEditorHtml:function(value){if(value=="undefined"||value==null){this._editControl.set_html("");
}else{this._editControl.set_html(value);
}},_setRadEditorContentAreaHtml:function(initContent,forceNewDocument){if(!initContent||initContent.trim().length==0){this._setEmptyMessage();
}var newContent=this.get_filtersManager().getDesignContent(initContent);
var oContent=null;
var $T=Telerik.Web.UI;
if(-1!=newContent.toLowerCase().indexOf("<html")){this.set_fullPage(true);
oContent=newContent;
this._doctypeString=this._extractDoctype(oContent);
}else{this.set_fullPage(false);
}if(this.get_fullPage()&&this.get_contentAreaMode()==$T.EditorContentAreaMode.Div){alert(this.get_localization()["SetToIframeForFullHTMLError"]);
newContent=Telerik.Web.UI.Editor.Utils.stripHtmlHeadBodyTags(newContent);
this.set_fullPage(false);
}newContent=this._markExistingEmptyParagraphs(newContent);
if(this.get_contentAreaMode()==$T.EditorContentAreaMode.Div){this.set_contentArea(this.get_contentAreaElement());
var contentArea=this.get_contentArea();
this.set_document(contentArea.ownerDocument);
this.set_contentWindow(window);
if(newContent.match(/<form[\s>]/i)){alert(this.get_localization()["FormElementsAreSupportedByIframeError"]);
newContent="";
}$T.Editor.Utils.setElementInnerHtml(contentArea,newContent);
this.get_filtersManager().getDesignContentDom(contentArea);
if(!this._divContentAreaInitialized){this._contentAreaCommonInitialization();
}}else{if(null!=oContent||true==forceNewDocument){if(!oContent){if($telerik.isSafari){oContent="<!DOCTYPE html><html><head><style></style></head><body>"+newContent+"</body></html>";
}else{var operaSpecific="";
if($telerik.isOpera){operaSpecific=" style='height:100%'";
}oContent='<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><head><style></style></head><body'+operaSpecific+">"+newContent+"</body>";
}}if($telerik.isFirefox){var oLoadFunction=Function.createDelegate(this,function(){if(this._execLoadFunctionSecondTime){this._contentFrameLoadFunction();
}});
$addHandler(this.get_contentAreaElement(),"load",oLoadFunction);
}try{var contentDocument=this.get_contentAreaElement().contentWindow.document;
contentDocument.open();
contentDocument.write(oContent);
contentDocument.close();
this._execLoadFunctionSecondTime=false;
if(contentDocument.body){this._contentFrameLoadFunction();
}else{this._execLoadFunctionSecondTime=true;
}}catch(e){}}else{$T.Editor.Utils.setElementInnerHtml(this.get_contentArea(),newContent);
this.get_filtersManager().getDesignContentDom(this.get_contentArea());
$(this.get_contentArea()).addClass("sfreContentArea");
}}var contentIsBroken=this._checkForBrokenParagraphs();
if(contentIsBroken){this._fixBrokenParagraphs(newContent);
}},_setUnlinkCommand:function(){var $T=Telerik.Web.UI;
var Editor=$T.Editor;
var utils=Editor.Utils;
Editor.UnlinkCommand=function(editor,settings,options){settings={tag:"a",altTags:[]};
Editor.UnlinkCommand.initializeBase(this,[editor,settings,options]);
};
Editor.UnlinkCommand.prototype={getState:function(wnd,editor,range){range=editor.getSelection().getRange();
var states=Editor.CommandStates;
var tag=this.settings.tag;
var hasLink=false;
if(range){hasLink=utils.someInRange(range,function(node){var isTag=utils.isTag(node,tag);
var parent=$(node).parent();
var isAnchorTag=!!parent.is(tag);
return isTag||isAnchorTag||!!$(node).find(tag+":first").length;
});
}hasLink=hasLink||$(editor.getSelectedElement()).is(tag);
return hasLink?states.Off:states.Disabled;
},isSameTagFormat:function(node){return utils.isTag(node,this.settings.tag);
},shouldRemoveFormatting:function(){return true;
},removeNodeFormatting:function(node){utils.removeNode(node);
}};
Editor.UnlinkCommand.registerClass("Telerik.Web.UI.Editor.UnlinkCommand",Editor.InlineCommand);
Editor.UpdateCommandsArray.Unlink=new Editor.UnlinkCommand();
Editor.CommandList.Unlink=function(commandName,editor,oTool){editor.setActive();
editor.setFocus();
var cmd=new Editor.UnlinkCommand(editor,undefined,{title:"Unlink"});
return editor.executeCommand(cmd);
};
},get_editControlId:function(){return this._editControlId;
},set_editControlId:function(value){this._editControlId=value;
},get_editControl:function(){return this._editControl;
},set_editControl:function(value){this._editControl=value;
},get_editorDialogsExtender:function(){if(!this._editorDialogsExtender){var editorCustomDialogsExtenders=Sys.UI.Behavior.getBehaviorsByType(this.get_editControl().get_element(),Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender);
if(!editorCustomDialogsExtenders||editorCustomDialogsExtenders.lenght<1){throw"No 'Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender' found";
}if(editorCustomDialogsExtenders.length>1){throw"There are more than one 'Telerik.Sitefinity.Web.UI.Extenders.RadEditorCustomDialogsExtender' for the editor.";
}this._editorDialogsExtender=editorCustomDialogsExtenders[0];
}return this._editorDialogsExtender;
},get_viewControl:function(){return this._viewControl;
},set_viewControl:function(value){this._viewControl=value;
},isChanged:function(){if(this._hasChanged==true){return true;
}else{return false;
}},get_value:function(){if(this._editControl!=null&&this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){if(this._editControl.get_mode()==Telerik.Web.UI.EditModes.Html){this._editControl.set_mode(Telerik.Web.UI.EditModes.Design);
this._editControl.set_mode(Telerik.Web.UI.EditModes.Html);
}return this._editControl.get_html(true);
}else{return this._viewControl.innerHTML;
}},set_value:function(value){this._originalValue=value;
this._hasChanged=false;
if(this.get_displayMode()==Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){if(this._editControl!=null){this._setEditorHtml(value);
}}else{if(this._viewControl!=null){this._viewControl.innerHTML=value;
}}this._value=value;
this.raisePropertyChanged("value");
this._valueChangedHandler();
},get_editorToolbarMode:function(){return this._editorToolbarMode;
},set_editorToolbarMode:function(value){this._editorToolbarMode=value;
},set_culture:function(culture){this._culture=culture;
},get_culture:function(){return this._culture;
},set_uiCulture:function(culture){this._uiCulture=culture;
if(this._editControl!=null&&this.get_isInitialized()&&this.get_displayMode()===Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write){this.get_editorDialogsExtender().set_uiCulture(culture);
}},get_uiCulture:function(){return this._uiCulture;
}};
Telerik.Sitefinity.Web.UI.Fields.HtmlField.registerClass("Telerik.Sitefinity.Web.UI.Fields.HtmlField",Telerik.Sitefinity.Web.UI.Fields.FieldControl,Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);
