/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ImageUploadField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ImageUploadField.initializeBase(this, [element]);
    this._imageControl = null;
    this._mediaContentBinderServiceUrl = null;
    this._copyMediaFileLinkServiceUrl = null;
    this._mediaContentItemsList = null;
    this._mediaContentBinder = null;
    this._originalSizeUrl = null;
    this._size = null;
    this._dataItem = null;
    this._dataItemContext = null;
	this._windowManager = null;
	this._menuList = null;
	this._clientLabelManager = null;	
	this._uploadNewImageButton = null;
	this._mediaContainer = null;
	this._radioButtonList = null;
	this._moreTranslationsLbl = null;
	this._clientLabelManager = null;
	this._menuListClickedDelegate = null;
	this._defaultFileId = null;	
    //The name of the timestamp paremeter used to prevent caching GET requests after image is replaced.
	this._urlVersionQueryParam = "sfvrsn";
}

Telerik.Sitefinity.Web.UI.Fields.ImageUploadField.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ImageUploadField.callBaseMethod(this, "initialize");

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {            
            $("#cancelUploadContainer_write").hide();
            this._menuListClickedDelegate = Function.createDelegate(this, this._menuList_clicked)
            this._menuList.add_itemClicked(this._menuListClickedDelegate);
        }

        if (window.hasOwnProperty("detailFormView") && detailFormView) {
            this._detailFormViewDataBindDelegate = Function.createDelegate(this, this._detailFormViewDataBind);
            detailFormView.add_onDataBind(this._detailFormViewDataBindDelegate);
        }

        if (this._urlVersionQueryParam == null || this._urlVersionQueryParam == "")
            this._urlVersionQueryParam = "tmstmp";
    },

    _detailFormViewDataBind: function (sender, args) {
        if (this._menuList) {
            this._menuList.trackChanges();

            var additionalInfo = args.SfAdditionalInfo;
            if (additionalInfo) {
                var isVectorGraphics = this._findValueByKey(additionalInfo, "IsVectorGraphics") === true;
                this._setMenuListItemVisibility("editImage", isVectorGraphics === false);
                this._setMenuListItemVisibility("allSizes", isVectorGraphics === false);
            }

            if (this._isMultilingual) {
                this._setMenuListItemVisibility("replace", true);
            }

            this._menuList.commitChanges();
        }
    },

    dispose: function () {

        if (this._detailFormView && this.detailFormViewDataBindDelegate) {
            this._detailFormView.remove_onDataBind(this._detailFormViewDataBindDelegate);
            delete this._detailFormViewDataBindDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.ImageUploadField.callBaseMethod(this, "dispose");
    },    
   
    /* --------------------  public methods ----------- */

    reset: function () {
        this.set_value(null);
        Telerik.Sitefinity.Web.UI.Fields.ImageUploadField.callBaseMethod(this, "reset");
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    _openThumbnailListDialog: function () {
        var params = {};
        params.backLabelText = "BackToItem";        
        params.backLabelResourceClassId = "ImagesResources";
        params.provider = this.get_libraryProviderName();
        var baseListUiCulture = detailFormView.get_baseList().get_uiCulture();

        try {            
            //We need to set the culture of the ItemsList control in order to get the correct thumbnail.        
            detailFormView.get_baseList().set_uiCulture(detailFormView.get_binder().get_uiCulture());
            detailFormView._openDialog(this.get_thumbnailListDialogCommand(), detailFormView.get_dataItem().Item, params);
        }
        finally {
            //_uiCulture is used to filter the grid by language. That is why we return the initial UI culture.
            detailFormView.get_baseList().set_uiCulture(baseListUiCulture);
        }
    },

    _getAppender: function (value) {
        var appender = "?";
        if (value.indexOf('?') > 0) { appender = "&"; }
        return appender;
    },

    _appendTimeStampParam: function (value) {
        var appender = this._getAppender(value);
        value += appender + this._urlVersionQueryParam + "=" + new Date().getTime();
        return value;
    },    
    
    _menuList_clicked: function (sender, args) {
        var menuItem = args.get_item();
        var command = menuItem.get_attributes().getAttribute("command");
        switch (command) {
            default:
                break;
            case "originalImage":
                this.viewOriginalFile();
                this._clearMenuSelection();
                break;
            case "allSizes":
                this._openThumbnailListDialog();
                this._clearMenuSelection();
                break;
            case "editImage":
                this._openDialog();
                this._clearMenuSelection();
                break;
        }
    },

    _openDialog: function () {
        var dialog = this.get_windowManager().getWindowByName("imageEditor");
        if (dialog) {            
            dialog.set_skin("Default");
            dialog.set_showContentDuringLoad(false);

            var dataItem = this.get_dataItem();
            var url = dialog.get_navigateUrl();
            url = Telerik.Sitefinity.setUrlParameter(url, "Id", dataItem.Id);
            url = Telerik.Sitefinity.setUrlParameter(url, "Status", "Master");
            url = Telerik.Sitefinity.setUrlParameter(url, "provider", this.get_libraryProviderName());

            var uiCulture = detailFormView.get_binder()._uiCulture;
            if (uiCulture)
                url = Telerik.Sitefinity.setUrlParameter(url, "SF_UI_CULTURE", uiCulture);
                
            dialog.set_navigateUrl(url);

            if (!this._closeDialogDelegate) {
                this._closeDialogDelegate = Function.createDelegate(this, this._closeDialogHandler);
            }
            dialog.add_close(this._closeDialogDelegate);

            dialog.show();
        }
    },

    _closeDialogHandler: function (sender, args) {
        var argument = args.get_argument();
        sender.remove_close(this._closeDialogDelegate);
        delete this._closeDialogDelegate;
        if (argument.CommandName == "save") {
            if (this._imageControl) {
                if (argument.Width) {
                    this._resizeImage(this._imageControl, argument.Width, argument.Height, this.get_size());
                }
                this._imageControl.src = this.appendRnd(this._imageControl.src);
            }

			var key = [];
			key.Id = argument.ImageId;
			this.raiseEvent("imageSaved", { dataItem: { ProviderName: argument.ProviderName }, Key: key, SaveArgument: argument.SaveArgument });
        }
    },

    _setMenuListItemVisibility: function (command, visible) {
        var item = this._menuList.findItemByAttribute("command", command);
        if (item) {
            item.set_visible(visible);
        }
    },

    appendRnd: function (value) {
        var qIndex = value.lastIndexOf("?");
        if (qIndex > 0) {
            var query = value.substring(qIndex + 1);
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(query);
            if (queryString.contains(this._urlVersionQueryParam)) {
                queryString.set(this._urlVersionQueryParam, Math.random());
                return (value.substring(0, qIndex)) + queryString.toString(true);
            }
        }
        return value + "?" + this._urlVersionQueryParam + "=" + Math.random();
    },

    /* -------------------- properties ---------------- */

    get_size: function () {
        if (this._size == null)
            this._size = 538;
        return this._size;
    },

    set_size: function (value) {
        this._size = value;
    },

    // Gets the value(media URL) of the image control.
    get_value: function () {
        if (this._imageControl) {
            return this._imageControl.src;
        }
        return "";
    },

    //Sets the value (media URL) of the image control.
    set_value: function (value) {
        this._originalSizeUrl = value;
        if (this._imageControl) {
            if (value) {
                var dataItem = this.get_dataItem();
                if (dataItem)
                    if (dataItem.Width && dataItem.Height) {
                        this._resizeImage(this._imageControl, dataItem.Width, dataItem.Height, this.get_size());
                    } else if (this._isVectorGraphics() === true) {
                        var img = $(this._imageControl);
                        img.attr("height", this.get_size());
                        img.attr("width", this.get_size());
                    }
                this._imageControl.src = value;
            }
            else {
                this._imageControl.src = "";
            }
        }
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    _resizeImage: function (imgElement, w, h, size) {
        var img = $(imgElement);
        if (h > size || w > size) {
            if (h == w) {
                img.attr("height", size);
                img.attr("width", size);
            }
            else if (h < w) {
                img.attr("width", size);
                // IE fix
                img.removeAttr("height");
            }
            else {
                img.attr("height", size);
                // IE fix
                img.removeAttr("width");
            }
        } else {
            if (img.attr("height"))
                img.removeAttr("height");
            if (img.attr("width"))
                img.removeAttr("width");
                
        }
    },

    //override function from base class
    viewOriginalFile: function () {
        if (this._originalSizeUrl) {
            window.open(this._originalSizeUrl, "_blank");
        }
    },

    _findValueByKey: function (dictionary, key) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key)
                return item.Value;
        }

        return null;
    },

    _setValueByKey: function (dictionary, key, value) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key) {
                item.Value = value;
                return;
            }
        }

        dictionary.push({ Key: key, Value: value });
    },

    _isVectorGraphics: function () {
        var status = false;

        if (this.get_dataItemContext()) {
            var sfAdditionalInfo = this.get_dataItemContext().SfAdditionalInfo;
            if (sfAdditionalInfo) {
                status = this._findValueByKey(sfAdditionalInfo, "IsVectorGraphics") === true;
            }
        }

        return status;
    },

    // Gets the reference to the image control that displays the media url
    get_imageControl: function () { return this._imageControl; },

    // Sets the reference to the image control that displays the media url
    set_imageControl: function (value) { this._imageControl = value; },   

    get_mediaContentItemsList: function () {
        return this._mediaContentItemsList;
    },
    set_mediaContentItemsList: function (value) {
        this._mediaContentItemsList = value;
    },

    get_mediaContentBinder: function () {
        return this._mediaContentBinder;
    },
    set_mediaContentBinder: function (value) {
        this._mediaContentBinder = value;
    },

    get_mediaContentBinderServiceUrl: function () {
        return this._mediaContentBinderServiceUrl;
    },
    set_mediaContentBinderServiceUrl: function (value) {
        this._mediaContentBinderServiceUrl = value;
    },

    get_copyMediaFileLinkServiceUrl: function () {
        return this._copyMediaFileLinkServiceUrl;
    },
    set_copyMediaFileLinkServiceUrl: function (value) {
        this._copyMediaFileLinkServiceUrl = value;
    },

    get_menuList: function () {
        return this._menuList;
    },

    set_menuList: function(value) {
        this._menuList = value;
    },   

    get_uploadNewItemButton: function () {
        return this._uploadNewItemButton;
    },

    set_uploadNewItemButton: function(value) {
        this._uploadNewItemButton=value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_mediaContainer: function() {
        return this._mediaContainer;
    },

    set_mediaContainer: function(value) {
        this._mediaContainer = value;
    },

    get_radioButtonList: function(){
        return this._radioButtonList;
    },

    set_radioButtonList: function(value) {
        this._radioButtonList=value;
    },

    get_moreTranslationsLbl: function() {
        return this._moreTranslationsLbl;
    },

    set_moreTranslationsLbl:function(value) {
        this._moreTranslationsLbl=value;
    },

    get_windowManager: function () { return this._windowManager; },
    set_windowManager: function (value) { this._windowManager = value; },

    set_dataItem: function (value) {
        this._dataItem = value;
    },
    get_dataItem: function () { return this._dataItem; },

    get_thumbnailListDialogCommand: function () {
        return this._thumbnailListDialogCommand;
    },

	//Events START
	raiseEvent: function (eventName, eventArgs)
	{
		var handler = this.get_events().getHandler(eventName);
		if (handler)
		{
			if (!eventArgs)
			{
				eventArgs = Sys.EventArgs.Empty;
			}
			handler(this, eventArgs);
		}
	},
	add_imageSaved: function (handler)
	{
		this.get_events().addHandler("imageSaved", handler);
	},
	remove_imageSaved: function (handler)
	{
		this.get_events().removeHandler("imageSaved", handler);
	},
    //Events END

    get_dataItemContext: function () {
        return this._dataItemContext;
    },
    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        this._dataItemContext = value;
        this.set_dataItem(value.Item);
        this._detailFormViewDataBind(null, value);
	}
};
Telerik.Sitefinity.Web.UI.Fields.ImageUploadField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ImageUploadField", Telerik.Sitefinity.Web.UI.Fields.FileField, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext);