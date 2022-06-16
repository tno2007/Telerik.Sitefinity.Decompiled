Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");


Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView.initializeBase(this, [element]);
    
    this._pageCacheGrid = null;
    this._pageProfileUrl = null;

    this._mediaCacheGrid = null;
    this._mediaProfileUrl = null;

    this._clientLabelManager = null;

    this._createPageProfileButton = null;
    this._createPageProfileButtonClickDelegate = null;

    this._createMediaProfileButton = null;
    this._createMediaProfileButtonClickDelegate = null;

    this._pageProfilesWindow = null;
    this._mediaProfilesWindow = null;

    this._pageProfileDeleteConfirmationDialog = null;
    this._mediaProfileDeleteConfirmationDialog = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView.prototype =
 {
    initialize: function () {
        Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView.callBaseMethod(this, "initialize");

        jQuery(document).ready(this.onReady.bind(this));

        this._createPageProfileButtonClickDelegate = Function.createDelegate(this, this._createPageProfileButtonClickHandler);
        $addHandler(this.get_createPageProfileButton(), "click", this._createPageProfileButtonClickDelegate);

        this._createMediaProfileButtonClickDelegate = Function.createDelegate(this, this._createMediaProfileButtonClickHandler);
        $addHandler(this.get_createMediaProfileButton(), "click", this._createMediaProfileButtonClickDelegate);
    },

    dispose: function () {
        if (this._createMediaProfileButtonClickDelegate) {
            $removeHandler(this.get_createPageProfileButton(), "click", this._createMediaProfileButtonClickDelegate);
            delete this._createMediaProfileButtonClickDelegate;
        }

        if (this._createPageProfileButtonClickDelegate) {
            $removeHandler(this.get_createPageProfileButton(), "click", this._createPageProfileButtonClickDelegate);
            delete this._createPageProfileButtonClickDelegate;
        }

        Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView.callBaseMethod(this, "dispose");
    },

    onReady: function () {
        var columns = [
            {
                title: this.get_clientLabelManager().getLabel("ConfigDescriptions", "ItemNameCaption"),
                template: "<span class=\"sfLnk\">#= Name #</span>",
                attributes: {
                    "class": "sfMedium"
                },
                headerAttributes: {
                    "class": "sfMedium"
                }
            },
            {
                field: "Location",
                title: this.get_clientLabelManager().getLabel("ConfigDescriptions", "OutputCacheProfileLocationTitle"),
                attributes: {
                    "class": "sfMedium"
                },
                headerAttributes: {
                    "class": "sfMedium"
                }
            },
            {
                field: "HttpHeader",
                title: "Http-Header",
                attributes: {
                    "class": "sfCodeTxt sfMedium"
                },
                headerAttributes: {
                    "class": "sfMedium"
                }
            },
            {
                title: this.get_clientLabelManager().getLabel("ConfigDescriptions", "DefaultProfile"),
                template: "<em> #= DefaultProfile #</em>",
                attributes: {
                    "class": "sfShort"
                },
                headerAttributes: {
                    "class": "sfShort"
                }
            },
            {
                title: "",
                template: "# if (DefaultProfile) { # <span class=\"sfDeleteProfileOff\" data-profile=\"#= Name #\"><i class=\"fa fa-trash-o\"></i></span> # } else { # <span class=\"sfDeleteProfileOn\" data-profile=\"#= Name #\"><i class=\"fa fa-trash-o\"></i></span> # } #",
                attributes: {
                    "class": "sfEditCol sfAlignRight"
                },
                headerAttributes: {
                    "class": "sfEditCol"
                }
            }
        ];

        jQuery(this.get_pageCacheGrid()).kendoGrid({
            dataSource: {
                transport: {
                    read: this.get_pageProfileUrl(),
                    dataType: "json"
                }
            },
            columns: columns,
            scrollable: false,
            dataBound: jQuery.proxy(this._onPageDataBound, this)
        });

        jQuery(this.get_mediaCacheGrid()).kendoGrid({
            dataSource: {
                transport: {
                    read: this.get_mediaProfileUrl(),
                    dataType: "json"
                }
            },
            columns: columns,
            scrollable: false,
            dataBound: jQuery.proxy(this._onMediaDataBound, this)
        });
    },

    _onPageDataBound: function (arg) {
        var that = this;
        jQuery(arg.sender.element).find(".sfDeleteProfileOff").click(function (e) { e.stopPropagation(); });
        jQuery(arg.sender.element).find(".sfDeleteProfileOn").click(function (e) {
            e.stopPropagation();
            that.deleteProfileAction(this, that.get_pageProfileDeleteConfirmationDialog(), that.get_pageProfileUrl());
        });

        var pageGrid = jQuery(this.get_pageCacheGrid()).data("kendoGrid");
        jQuery.each(arg.sender.dataItems(), function (i, item) {
            var dataItem = item;
            jQuery(arg.sender.table).find('tr[data-uid=' + dataItem.uid + ']').click(function () {
                that.get_pageProfilesWindow().open(dataItem.Name, function () { pageGrid.dataSource.read(); });
            });
        });
    },

    _onMediaDataBound: function (arg) {
        var that = this;
        jQuery(arg.sender.element).find(".sfDeleteProfileOff").click(function (e) { e.stopPropagation(); });
        jQuery(arg.sender.element).find(".sfDeleteProfileOn").click(function (e) {
            e.stopPropagation();
            that.deleteProfileAction(this, that.get_mediaProfileDeleteConfirmationDialog(), that.get_mediaProfileUrl());
        });

        var mediaGrid = jQuery(this.get_mediaCacheGrid()).data("kendoGrid");
        jQuery.each(arg.sender.dataItems(), function (i, item) {
            var dataItem = item;
            jQuery(arg.sender.table).find('tr[data-uid=' + dataItem.uid + ']').click(function () {
                that.get_mediaProfilesWindow().open(dataItem.Name, function () { mediaGrid.dataSource.read(); });
            });
        });
    },

    deleteProfileAction: function (element, dialog, serviceUrl) {
        var promptCallback = function (sender, args) {
            var shouldDelete = args.get_commandName() === "delete";
            if (shouldDelete) {
                var profile = jQuery(element).attr("data-profile");
                var gridElement = jQuery(element).parents("div.k-widget");
                var grid = jQuery(element).parents("div.k-widget").data("kendoGrid");

                kendo.ui.progress(gridElement, true);
                jQuery.ajax({
                    url: serviceUrl + "/" + encodeURIComponent(profile),
                    method: "DELETE"
                })
                .done(function (data) {
                    if (data.Message)
                        alert(data.Message);

                    if (data.Success)
                        grid.dataSource.read();
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    if (errorThrown)
                        alert(errorThrown);
                })
                .always(function () {
                    kendo.ui.progress(gridElement, false);
                });
            }
        };

        dialog.show_prompt(null, null, promptCallback);
    },

    _createPageProfileButtonClickHandler: function () {
        var pageGrid = jQuery(this.get_pageCacheGrid()).data("kendoGrid");
        this.get_pageProfilesWindow().open(null, function () { pageGrid.dataSource.read(); });

        return false;
    },

    _createMediaProfileButtonClickHandler: function () {
        var mediaGrid = jQuery(this.get_mediaCacheGrid()).data("kendoGrid");
        this.get_mediaProfilesWindow().open(null, function () { mediaGrid.dataSource.read(); });

        return false;
    },

    get_pageCacheGrid: function () {
        return this._pageCacheGrid;
    },
    set_pageCacheGrid: function (value) {
        this._pageCacheGrid = value;
    },
    get_pageProfileUrl: function () {
        return this._pageProfileUrl;
    },
    set_pageProfileUrl: function (value) {
        this._pageProfileUrl = value;
    },

    get_mediaCacheGrid: function () {
        return this._mediaCacheGrid;
    },
    set_mediaCacheGrid: function (value) {
        this._mediaCacheGrid = value;
    },
    get_mediaProfileUrl: function () {
        return this._mediaProfileUrl;
    },
    set_mediaProfileUrl: function (value) {
        this._mediaProfileUrl = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_createPageProfileButton: function () {
        return this._createPageProfileButton;
    },
    set_createPageProfileButton: function (value) {
        this._createPageProfileButton = value;
    },

    get_createMediaProfileButton: function () {
        return this._createMediaProfileButton;
    },
    set_createMediaProfileButton: function (value) {
        this._createMediaProfileButton = value;
    },

    get_pageProfilesWindow: function () {
        return this._pageProfilesWindow;
    },
    set_pageProfilesWindow: function (value) {
        this._pageProfilesWindow = value;
    },

    get_mediaProfilesWindow: function () {
        return this._mediaProfilesWindow;
    },
    set_mediaProfilesWindow: function (value) {
        this._mediaProfilesWindow = value;
    },

    get_mediaProfileDeleteConfirmationDialog: function () {
        return this._mediaProfileDeleteConfirmationDialog;
    },
    set_mediaProfileDeleteConfirmationDialog: function (value) {
        this._mediaProfileDeleteConfirmationDialog = value;
    },

    get_pageProfileDeleteConfirmationDialog: function () {
        return this._pageProfileDeleteConfirmationDialog;
    },
    set_pageProfileDeleteConfirmationDialog: function (value) {
        this._pageProfileDeleteConfirmationDialog = value;
    }
 };

Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView", Sys.UI.Control);
