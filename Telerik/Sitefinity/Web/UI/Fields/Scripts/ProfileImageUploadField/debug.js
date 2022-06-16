﻿///

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField = function (element) {
	Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField.initializeBase(this, [element]);

	this._imageUrl = null;
	this._imageControl = null;

	//The name of the timestamp paremeter used to prevent caching GET requests after image is replaced.
	this._timestampParameterName = "unv_tstmp_prm";
}

Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField.prototype =
{
    initialize: function () {
    	Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField.callBaseMethod(this, "initialize");
		
		if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
			if (this._commandBarCommandDelegate == null) {
				this._commandBarCommandDelegate = Function.createDelegate(this, this._handleCommandBarCommand);
			}

			this._commandBar.add_command(this._commandBarCommandDelegate);
		}

		this._createUploadField();
        $(this._commandsContainer).show();
        $(this._uploaderContainer).hide();
    },

    dispose: function () {
    	if (this._commandBarCommandDelegate != null) {
    		this._commandBar.remove_command(this._commandBarCommandDelegate);
    		delete this._commandBarCommandDelegate;
    	}

        Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField.callBaseMethod(this, "dispose");
    },

	/* --------------------  public methods ----------- */

	/* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

	_handleCommandBarCommand: function (sender, args) {
       	switch (args.get_commandName()) {
       		case "replacePhoto":
				$(this._commandsContainer).hide();
				$(this._uploaderContainer).show();
				break;
			case "deletePhoto":
				alert("Are you sure you want to remove this photo?");
				break;
		}
	},

    /* -------------------- private methods ----------- */

	_createUploadField: function() {
		$(this._uploaderContainer).html();
	},

	_getAppender: function (value) {
		var appender = "?";
		if (value.indexOf('?') > 0) { appender = "&"; }
		return appender;
	},

	_appendTimeStampParam: function (value) {
		var appender = this._getAppender(value);
		value += appender + this._timestampParameterName + "=" + new Date().getTime();
		return value;
	},

	/* -------------------- properties ---------------- */

	// Gets the value(media url) of the image control.
    get_value: function () {
        if (this._imageControl) {
            return this._imageControl.src;
        }
        return "";
    },

    //Sets the value (media url) of the image control.
    set_value: function (value) {
    	this._imageUrl = value;

        if (this._imageControl) {
            if (value) {
                var appender = this._getAppender(value);
                var imageUrl = value + appender + "size=" + this.get_size();
                this._imageControl.src = this._appendTimeStampParam(imageUrl);
            }
            else {
                this._imageControl.src = "";
            }
        }
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

	// Gets the reference to the image control that displays the media url
    get_imageControl: function () { return this._imageControl; },

    // Sets the reference to the image control that displays the media url
    set_imageControl: function (value) { this._imageControl = value; }

};
Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ProfileImageUploadField", Telerik.Sitefinity.Web.UI.Fields.FileField);