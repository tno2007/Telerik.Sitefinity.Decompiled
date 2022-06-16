Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");


Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow.initializeBase(this, [element]);

    this._windowBody = null;
    this._doneButton = null;
    this._cancelButton = null;

    this._serviceUrl = null;
    this._newProfile = null;

    this._window = null;

    this._doneButtonClickDelegate = null;
    this._cancelButtonClickDelegate = null;

    this._profileName = null;
    this._dataItem = null;
    this._successCallback = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow.prototype =
 {
    initialize: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow.callBaseMethod(this, "initialize");

        jQuery(document).ready(this.onReady.bind(this));

        this._addTooltipLinksEventHandler();

        this._doneButtonClickDelegate = Function.createDelegate(this, this._doneButtonClickHandler);
        $addHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);

        this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClickHandler);
        $addHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
    },

    dispose: function () {
        if (this._cancelButtonClickDelegate) {
            $removeHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
            delete this._cancelButtonClickDelegate;
        }

        if (this._doneButtonClickDelegate) {
            $removeHandler(this.get_doneButton(), "click", this._doneButtonClickDelegate);
            delete this._doneButtonClickDelegate;
        }

        Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow.callBaseMethod(this, "dispose");
    },

    onReady: function () {
        this._window = jQuery(this.get_windowBody()).kendoWindow({
            title: false,
            visible: false,
            animation: false,
            actions: [],
            modal: true
        }).data("kendoWindow");

        jQuery(this.get_windowBody()).kendoValidator(
        {
            errorTemplate: '<span class="sfError">#=message#</span>'
        });
    },

    open: function (profileName, successCallback) {
        this._window.center().open();
        this._profileName = profileName;
        this._successCallback = successCallback;
        var jWindow = jQuery(this.get_windowBody());
        jWindow.getKendoValidator().hideMessages();
        jWindow.find('.create-only').toggle(!this._profileName);
        jWindow.find('.edit-only').toggle(!!this._profileName);
        if (profileName) {
            kendo.ui.progress(jWindow, true);
            jQuery.get(this.get_serviceUrl() + "/" + encodeURIComponent(profileName))
                .done(this._bind.bind(this))
                .always(function () {
                    kendo.ui.progress(jWindow, false);
                });
        }
        else {
            this._bind(this.get_newProfile());
        }
    },

    _bind: function (data) {
        var jWindow = jQuery(this.get_windowBody());
        this._dataItem = kendo.observable(jQuery.extend(true, this._baseViewModel(), data));
        this._dataItem.bind("change", this._viewModelChanged.bind(this));
        this._viewModelChanged();
        kendo.bind(jWindow, this._dataItem);
        this._window.center();
    },

    _viewModelChanged: function (args) {
        if (typeof args === "undefined" || args.field === "Location") {
            this._dataItem.set("noServerCache", this._dataItem.Location === "None" || this._dataItem.Location === "Client");
            this._dataItem.set("noClientCache", this._dataItem.Location === "None" || this._dataItem.Location === "Server");
            this._dataItem.set("noProxyCache", this._dataItem.Location !== "Any");
        }

        if (this._dataItem.BrowserMaxAge === null)
            this._dataItem.set("BrowserMaxAge", this._dataItem.MaxAge);

        if (typeof args === "undefined" || args.field === "Location" || args.field === "MaxAge" || args.field === "BrowserMaxAge" || args.field === "ProxyCdnMaxAge") {
            var httpHeaderValues = ["Cache-Control: "];
            var cacheability;
            switch (this._dataItem.Location) {
                case "Any":
                    cacheability = "public";
                    break;
                case "Client":
                case "ServerAndClient":
                    cacheability = "private";
                    break;
                default:
                    cacheability = "no-cache";
            }

            httpHeaderValues.push(cacheability);
            if (cacheability !== "no-cache") {
                if ((typeof args === "undefined" || args.field === "BrowserMaxAge") && !this._dataItem.browserMaxAgeSpecified)
                    this._dataItem.set("browserMaxAgeSpecified", this._dataItem.BrowserMaxAge !== this._dataItem.MaxAge);

                if (!this._dataItem.browserMaxAgeSpecified)
                    this._dataItem.set("BrowserMaxAge", this._dataItem.MaxAge);

                var maxAge = this._dataItem.BrowserMaxAge;
                if(maxAge > 0)
                    httpHeaderValues.push(", max-age=", maxAge);
            }

            if (cacheability === "public" && this._dataItem.ProxyCdnMaxAge !== null && this._dataItem.ProxyCdnMaxAge > 0)
                httpHeaderValues.push(", s-maxage=", this._dataItem.ProxyCdnMaxAge);

            this._dataItem.set("HttpHeader", httpHeaderValues.join(""));
        }

         if (typeof args === "undefined" || args.field === "isDefaultFor") {
             for (var i = 0; i < this._dataItem.AvailableItemTypes.length; i++) {
                 var type = this._dataItem.AvailableItemTypes[i];
                 var isDefault = this._dataItem.DefaultItemTypes.indexOf(type) > -1;

                 this._dataItem.set("isDefaultFor" + type, isDefault);
             }
         }

         if (typeof args !== "undefined" && args.field.indexOf("isDefaultFor") === 0) {
             var itemType = args.field.substr("isDefaultFor".length, args.field.length - "isDefaultFor".length);
             var index = this._dataItem.DefaultItemTypes.indexOf(itemType);

             if (this._dataItem[args.field] && index < 0)
                 this._dataItem.DefaultItemTypes.push(itemType);
             else if (!this._dataItem[args.field] && index >= 0)
                 this._dataItem.DefaultItemTypes.splice(index, 1);
         }
    },

    _baseViewModel: function () {
        return {
            noServerCache: false,
            noClientCache: true,
            noProxyCache: true,
            HttpHeader: "Cache-Control: no-cache",
            browserMaxAgeSpecified: false
        };
    },

    _doneButtonClickHandler: function () {
        var jWindow = jQuery(this.get_windowBody());
        if (!jWindow.getKendoValidator().validate())
            return;

        var method;
        var url = this.get_serviceUrl();
        if (this._profileName) {
            method = "PUT";
            url = url + "/" + encodeURIComponent(this._profileName);
        }
        else {
            method = "POST";
        }

        var that = this;
        kendo.ui.progress(jWindow, true);
        jQuery.ajax({
            type: method,
            url: url,
            data: JSON.stringify(this._dataItem),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        })
        .done(function (data) {
            if (data.Message)
                alert(data.Message);

            if (data.Success) {
                that._window.close();
                if (that._successCallback) {
                    that._successCallback();
                }
            }
        })
        .always(function () {
            kendo.ui.progress(jWindow, false);
        });
    },
    _cancelButtonClickHandler: function () {
        this._window.close();
    },

    _addTooltipLinksEventHandler: function () {
        jQuery('.tooltipLink')
            .unbind()
            .hover(function () {
                getTooltipDescriptionFromLink(this);
            });

        function getTooltipDescriptionFromLink(linkElement) {
            jQuery(linkElement)
                .parent()
                .parent()
                .find('.tooltip')
                .toggleClass('sfDisplayNone');
        };
    },

    get_windowBody: function () {
        return this._windowBody;
    },
    set_windowBody: function (value) {
        this._windowBody = value;
    },

    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_newProfile: function () {
        return this._newProfile;
    },
    set_newProfile: function (value) {
        this._newProfile = value;
    }
 };

Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfileDetailsWindow", Sys.UI.Control);
