﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Kendo");

Telerik.Sitefinity.Web.UI.Kendo.KendoWindow = function (element) {
    Telerik.Sitefinity.Web.UI.Kendo.KendoWindow.initializeBase(this, [element]);

    this._documentReadyDelegate = null;

    this._outerDiv = null;
    this._kendoWindow = null;
}

Telerik.Sitefinity.Web.UI.Kendo.KendoWindow.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Kendo.KendoWindow.callBaseMethod(this, "initialize");

        this._documentReadyDelegate = Function.createDelegate(this, this._documentReadyHandler);

        jQuery(document).ready(this._documentReadyDelegate);
    },

    dispose: function () {
        if (this._documentReadyDelegate) {
            delete this._documentReadyDelegate;
        }

        Telerik.Sitefinity.Web.UI.Kendo.KendoWindow.callBaseMethod(this, "dispose");
    },

    close: function (data) {
        var args = this._raise_close(data);

        if (args.get_cancel() == false) {
            this.get_kendoWindow().close();
        }
    },

    add_close: function (delegate) {
        this.get_events().addHandler('close', delegate);
    },

    remove_close: function (delegate) {
        this.get_events().removeHandler('close', delegate);
    },

    _raise_close: function (data) {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('close');
            var args = new Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs(data);
            if (h) h(this, args);
            return args;
        }
    },

    show: function () {
        this.get_kendoWindow().center();
        if(this.get_kendoWindow().options && !this.get_kendoWindow().options["isMaximized"])
            jQuery(this.get_kendoWindow().element).parent().css({ "top": this._dialogScrollTop() });
        else
            jQuery(this.get_kendoWindow().element).parent().css({ "top": 0 });
        this.get_kendoWindow().open();
    },

    _documentReadyHandler: function () {
        var jOuterDiv = jQuery(this.get_outerDiv());
        this._kendoWindow = jOuterDiv.kendoWindow({
            width: this.get_width() + "px",
            height: ((this.get_height() && this.get_height() > 0) ? this.get_height() + "px": "auto"),
            resizable: this.get_isResizable(),
            modal: this.get_isModal(),
            animation: false
        }).data("kendoWindow");
        jOuterDiv.show();
    },

    /* calculates top position of kendo dialog */
    _dialogScrollTop: function () {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        return scrollTop;
    },

    get_outerDiv: function () {
        return this._outerDiv;
    },
    set_outerDiv: function (value) {
        this._outerDiv = value;
    },
    get_width: function () {
        return this._width;
    },
    set_width: function (value) {
        this._width = value;
    },
    get_height: function () {
        return this._height;
    },
    set_height: function (value) {
        this._height = value;
    },
    get_isResizable: function () {
        return this._isResizable;
    },
    set_isResizable: function (value) {
        this._isResizable = value;
    },
    get_isModal: function () {
        return this._isModal;
    },
    set_isModal: function (value) {
        this._isModal = value;
    },
    get_kendoWindow: function () {
        return this._kendoWindow;
    }
}

Telerik.Sitefinity.Web.UI.Kendo.KendoWindow.registerClass('Telerik.Sitefinity.Web.UI.Kendo.KendoWindow', Sys.UI.Control);


Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs = function (data) {
    Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs.initializeBase(this);

    this._data = data;
    this._cancel = false;
}

Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs.callBaseMethod(this, 'dispose');
    },

    get_data: function () {
        return this._data;
    },
    get_cancel: function () {
        return this._cancel;
    },
    set_cancel: function (value) {
        this._cancel = value;
    }
};
Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs.registerClass('Telerik.Sitefinity.Web.UI.Kendo.CloseEventArgs', Sys.EventArgs);
