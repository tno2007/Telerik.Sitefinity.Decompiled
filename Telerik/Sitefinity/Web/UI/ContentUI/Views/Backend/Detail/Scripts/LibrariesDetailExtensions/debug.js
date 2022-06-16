// called by the DetailFormView when it is loaded
function OnDetailViewLoaded(sender, args) {
    // the sender here is DetailFormView
    var detailFormView = sender;

    Sys.Application.add_init(function () {
        $create(Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension,
        { _detailFormView: detailFormView },
        {},
        {},
        null);
    });
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension = function () {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension.initializeBase(this);
    // Main components
    this._detailFormView = {};
    this._binder = null;
    this._emptyGuid = "00000000-0000-0000-0000-000000000000";
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension.callBaseMethod(this, "initialize");
        this._binder = this._detailFormView.get_binder();

        this._closeDialogDelegate = Function.createDelegate(this, this._closeDialog);
        if (dialogBase.get_radWindow()) {
            dialogBase.get_radWindow().add_beforeClose(this._closeDialogDelegate);
        }
        var imageUploadField = null;
        var fieldIds = this._detailFormView._fieldControlIds;
        for (var i = 0, length = fieldIds.length; i < length; i++) {
            imageUploadField = $find(fieldIds[i]);
            if (Object.getTypeName(imageUploadField) == "Telerik.Sitefinity.Web.UI.Fields.ImageUploadField") {
                this._imageUploadField = imageUploadField;
                break;
            }
        }
        if (this._imageUploadField) {
            this._imageSavedDelegate = Function.createDelegate(this, this._imageSaved);
            this._imageUploadField.add_imageSaved(this._imageSavedDelegate);            
        }

        if (this._detailFormView) {
            this._detailFormViewDataBindDelegate = Function.createDelegate(this, this._detailFormViewDataBind);
            this._detailFormView.add_onDataBind(this._detailFormViewDataBindDelegate);
        }

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        if (this._imageUploadField && this._imageSavedDelegate) {
            this._imageUploadField.remove_imageSaved(this._imageSavedDelegate);
            delete this._imageSavedDelegate;
        }

        if (this._closeDialogDelegate) {
            if (dialogBase.get_radWindow()) {
                dialogBase.get_radWindow().remove_beforeClose(this._closeDialogDelegate);
            }
            delete this._closeDialogDelegate;
        }

        if (this._detailFormView && this.detailFormViewDataBindDelegate) {
            this._detailFormView.remove_onDataBind(this._detailFormViewDataBindDelegate);
            delete this._detailFormViewDataBindDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
    _imageSaved: function (sender, args) {
        if (args.SaveArgument == "saveAs") {
            var detailFormView = this._detailFormView;
            detailFormView._callerDataItem = args.dataItem;
            detailFormView._dataKey = args.Key;
            detailFormView._callParams = { skipLoadingItemFromServer: false };
            detailFormView.rebind();
        }
    },

    _detailFormViewDataBind: function (sender, args)
    {
        this._includeOrUpdateFullUrlPrefix(args);
    },

    _includeOrUpdateFullUrlPrefix: function (args) {
        var prependedSpanId = 'mirroredFiedlPrependedSpan';
        var appendedSpanId = 'mirroredFiedlAppendedSpan';
        var prependedSpan = $('#' + prependedSpanId);


        var mediaUrlLabelControl = this._binder.getFieldControlByFieldName("MediaFileUrlName");
        var UrlNameLabelControl = this._binder.getFieldControlByFieldName("UrlName");
        var elementToPrependAndAppendTo = mediaUrlLabelControl || UrlNameLabelControl;

        var additionalInfo = args.SfAdditionalInfo;
        var baseUrl = this._findValueByKey(additionalInfo, 'fullUrl');

        if (prependedSpan.length === 0) {
            prependedSpan = $('<span id="' + prependedSpanId + '" class="sfUrlPath sfFlLeft"></span>');
            prependedSpan.insertBefore(elementToPrependAndAppendTo.get_textBoxElement());
        }

        var appendedSpan = $('#' + appendedSpanId);
        if (appendedSpan.length === 0) {
            appendedSpan = $('<span id="' + appendedSpanId + '"></span>');
            appendedSpan.insertAfter(elementToPrependAndAppendTo.get_mirroredValueLabel());
        }

        if (mediaUrlLabelControl) {
            $addHandler(mediaUrlLabelControl.get_mirroredValueLabel(), "mouseenter", this._setBackgroundOfUrl.bind(this, ["MediaFileUrlName"]));
            $addHandler(mediaUrlLabelControl.get_mirroredValueLabel(), "mouseleave", this._removeBackgroundOfUrl.bind(this, ["MediaFileUrlName"]));
            $addHandler(mediaUrlLabelControl.get_changeControl(), "mouseenter", this._setBackgroundOfUrl.bind(this, ["MediaFileUrlName"]));
            $addHandler(mediaUrlLabelControl.get_changeControl(), "mouseleave", this._removeBackgroundOfUrl.bind(this, ["MediaFileUrlName"]));
        }
        

        if (UrlNameLabelControl) {
            $addHandler(UrlNameLabelControl.get_mirroredValueLabel(), "mouseenter", this._setBackgroundOfUrl.bind(this, ["UrlName"]));
            $addHandler(UrlNameLabelControl.get_mirroredValueLabel(), "mouseleave", this._removeBackgroundOfUrl.bind(this, ["UrlName"]));
            $addHandler(UrlNameLabelControl.get_changeControl(), "mouseenter", this._setBackgroundOfUrl.bind(this, ["UrlName"]));
            $addHandler(UrlNameLabelControl.get_changeControl(), "mouseleave", this._removeBackgroundOfUrl.bind(this, ["UrlName"]));
        }

        prependedSpan.text(baseUrl);
        appendedSpan.text(args.Item.Extension);
    },

    _setBackgroundOfUrl: function (params) {
        this._binder.getFieldControlByFieldName(params[0]).get_mirroredValueLabel().style.backgroundColor = "#ffff99";
    },

    _removeBackgroundOfUrl: function (params) {
        this._binder.getFieldControlByFieldName(params[0]).get_mirroredValueLabel().style.backgroundColor = "";
    },

    /* -------------------- private methods ----------- */
    _getFieldControlByDataFieldName: function (dataFieldName) {
        return this._binder.getFieldControlByDataFieldName(dataFieldName);
    },

    _closeDialog: function () {
        var mediaField = this._getFieldControlByDataFieldName("MediaUrl");

        if (mediaField && Object.getTypeName(mediaField) == "Telerik.Sitefinity.Web.UI.Fields.MediaField")
            mediaField._clearMediaItem();
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

    /* -------------------- properties ---------------- */

}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrariesDetailExtension", Sys.Component, Sys.IDisposable);


