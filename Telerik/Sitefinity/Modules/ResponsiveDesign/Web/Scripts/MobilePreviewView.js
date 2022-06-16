Type.registerNamespace("Telerik.Sitefinity.Modules.ResponsiveDesign.Web");
Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView=function(element){Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView.initializeBase(this,[element]);
this._element=element;
this._devicesSettings=null;
this._inPortraitMode=true;
this._currentDeviceName=null;
this._previewPageUrl=null;
};
Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView.prototype={initialize:function(){var me=this;
this._devicesSettings=JSON.parse(this._devicesSettings);
this._fillDevicesDropdown(this._devicesSettings,me);
if(this._inPortraitMode){$(this._selectors.portraitButton).addClass("sfSel");
$(this._selectors.landscapeButton).removeClass("sfSel");
}else{$(this._selectors.portraitButton).removeClass("sfSel");
$(this._selectors.landscapeButton).addClass("sfSel");
}$(this._selectors.portraitButton).click(function(){me._inPortraitMode=true;
me._switchDevice(me._currentDeviceName);
$(me._selectors.portraitButton).addClass("sfSel");
$(me._selectors.landscapeButton).removeClass("sfSel");
});
$(this._selectors.landscapeButton).click(function(){me._inPortraitMode=false;
me._switchDevice(me._currentDeviceName);
$(me._selectors.portraitButton).removeClass("sfSel");
$(me._selectors.landscapeButton).addClass("sfSel");
});
$(this._selectors.deviceSelect).change(function(){me._switchDevice($(this).val());
});
this._switchDevice(this._devicesSettings[0].Name);
},dispose:function(){},_fillDevicesDropdown:function(devices,model){var categoryNames=new Array();
$(devices).each(function(deviceIndex,deviceItem){var deviceExists=$.grep(categoryNames,function(strItem,strIndex){return strItem==deviceItem.DeviceCategory;
}).length>0;
if(!deviceExists){categoryNames.push(deviceItem.DeviceCategory);
}});
$(categoryNames).each(function(categoryIndex,categoryItem){$(model._selectors.deviceSelect).append('<optgroup label="'+categoryItem+'"></optgroup>');
var allDevicesInThisCategory=$.grep(devices,function(deviceItem,deviceIndex){return deviceItem.DeviceCategory==categoryItem;
});
$(allDevicesInThisCategory).each(function(deviceIndex,deviceItem){$(model._selectors.deviceSelect).append('<option value="'+deviceItem.Name+'">'+deviceItem.Title+"</option>");
});
});
},_switchDevice:function(newDeviceName){var deviceSettings=this._getSettings(newDeviceName);
var devicesCount=this._devicesSettings.length;
while(devicesCount--){$(this._selectors.devicePreviewContainer).removeClass(this._devicesSettings[devicesCount].CssClass);
}$(this._selectors.devicePreviewContainer).removeClass();
$(this._selectors.devicePreviewContainer).addClass("sfDevicePreviewContainer");
if(this._inPortraitMode){$(this._selectors.devicePreviewContainer).width(deviceSettings.DeviceWidth);
$(this._selectors.devicePreviewContainer).height(deviceSettings.DeviceHeight);
$(this._selectors.viewport).width(deviceSettings.ViewportWidth);
$(this._selectors.viewport).height(deviceSettings.ViewportHeight);
$(this._selectors.viewport).css("left",deviceSettings.OffsetX+"px");
$(this._selectors.viewport).css("top",deviceSettings.OffsetY+"px");
$(this._selectors.devicePreviewContainer).addClass(deviceSettings.CssClass);
}else{$(this._selectors.devicePreviewContainer).height(deviceSettings.DeviceWidth);
$(this._selectors.devicePreviewContainer).width(deviceSettings.DeviceHeight);
$(this._selectors.viewport).width(deviceSettings.ViewportHeight);
$(this._selectors.viewport).height(deviceSettings.ViewportWidth);
$(this._selectors.viewport).css("left",deviceSettings.OffsetXLandscape+"px");
$(this._selectors.viewport).css("top",deviceSettings.OffsetYLandscape+"px");
$(this._selectors.devicePreviewContainer).addClass(deviceSettings.CssClass+"_landscape");
}this._currentDeviceName=newDeviceName;
var me=this;
if($(this._selectors.viewport).attr("src")==null){$(this._selectors.viewport).attr("src",this._previewPageUrl);
$(this._selectors.viewport).on("load",function(){var _wheelDelegate=Function.createDelegate(me,me._wheel);
var viewportFrame=$(me._selectors.viewport).get(0);
var frameWindow=viewportFrame.contentWindow;
if(frameWindow.addEventListener){frameWindow.addEventListener("DOMMouseScroll",_wheelDelegate,{passive:false});
}frameWindow.addEventListener("mousewheel",_wheelDelegate,{passive:false});
viewportFrame.contentDocument.addEventListener("mousewheel",_wheelDelegate,{passive:false});
});
}},_getSettings:function(deviceName){var devicesCount=this._devicesSettings.length;
while(devicesCount--){if(this._devicesSettings[devicesCount].Name==deviceName){return this._devicesSettings[devicesCount];
}}},_selectors:{devicePreviewContainer:"#device-preview-container",portraitButton:"#mobile-preview-portrait-button",landscapeButton:"#mobile-preview-landscape-button",viewport:"#device-preview-viewport",deviceSelect:"#deviceSelect"},_wheel:function(event){var delta=0;
if(!event){event=window.event;
}if(event.wheelDelta){delta=event.wheelDelta/120;
}else{if(event.detail){delta=-event.detail/3;
}}if(delta){this._handle(delta);
}if(event.preventDefault){event.preventDefault();
}event.returnValue=false;
},_handle:function(delta){$(this._selectors.viewport).contents().scrollTop($(this._selectors.viewport).contents().scrollTop()-(delta*10));
}};
Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView.registerClass("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MobilePreviewView",Sys.UI.Control);
