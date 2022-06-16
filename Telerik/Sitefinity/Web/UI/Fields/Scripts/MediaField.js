Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
Telerik.Sitefinity.Web.UI.Fields.MediaField=function(element){Telerik.Sitefinity.Web.UI.Fields.MediaField.initializeBase(this,[element]);
this._mediaPlayer=null;
};
Telerik.Sitefinity.Web.UI.Fields.MediaField.prototype={initialize:function(){Telerik.Sitefinity.Web.UI.Fields.MediaField.callBaseMethod(this,"initialize");
},dispose:function(){Telerik.Sitefinity.Web.UI.Fields.MediaField.callBaseMethod(this,"dispose");
},reset:function(){this._clearMediaItem();
Telerik.Sitefinity.Web.UI.Fields.MediaField.callBaseMethod(this,"reset");
},_clearMediaItem:function(){if(this._mediaPlayer){this._mediaPlayer._clearMediaItem();
}},get_value:function(){if(this._mediaPlayer){return this._mediaPlayer.get_url();
}return"";
},set_value:function(value){if(this._mediaPlayer){var data={url:this._appendRnd(value),title:"",description:""};
this._mediaPlayer.setMediaParams(data);
}this.raisePropertyChanged("value");
this._valueChangedHandler();
},get_mediaPlayer:function(){return this._mediaPlayer;
},set_mediaPlayer:function(value){this._mediaPlayer=value;
}};
Telerik.Sitefinity.Web.UI.Fields.MediaField.registerClass("Telerik.Sitefinity.Web.UI.Fields.MediaField",Telerik.Sitefinity.Web.UI.Fields.FileField);
