Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.UI");

Telerik.Sitefinity.Multisite.Web.UI.SitesView = function (element) {
    Telerik.Sitefinity.Multisite.Web.UI.SitesView.initializeBase(this, [element]);
    this._grid = null;
    this._webServiceUrl = null;
    this._clientLabelManager = null;
    this._siteDetailView = null;
    this._searchWidget = null;
    this._createSiteLink = null;
    this._pageBehaviourField = null;
    this._stopConfirmationDialog = null;
    this._deleteConfirmationDialog = null;

    this._selectors = null;
    this._dataSource = null;
    this._lastQuery = null;
    this._pageSize = 50;

    this._gridDataBoundDelegate = null;
    this._searchCommandDelegate = null;
    this._createSiteLinkClickDelegate = null;
    this._onLoadDelegate = null;
    this._closeDelegate = null;

    this._permissionsDialog = null;
    this._permissionsDialogUrl = null;

    this._showSite = false;
    this._dashboardUrl = null;

    this._separateUsersPerSiteModeEnabled = null;
}

Telerik.Sitefinity.Multisite.Web.UI.SitesView.prototype = {

    /* ----------------------------- setup and teardown ----------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SitesView.callBaseMethod(this, 'initialize');

        this._selectors = {
            gridViewRowTemplate: "#sitesGridRowTemplate"
        };

        // prevent memory leaks
        jQuery(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);

        this._searchCommandDelegate = Function.createDelegate(this, this._onSearchCommand);
        this.get_searchWidget().add_command(this._searchCommandDelegate);

        if (this.get_createSiteLink()) {
            this._createSiteLinkClickDelegate = Function.createDelegate(this, this._createSiteLinkClickHandler);
            $addHandler(this.get_createSiteLink(), "click", this._createSiteLinkClickDelegate);
        }

        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);

        this._closeDelegate = Function.createDelegate(this, this._close);
        this._siteDetailView.add_close(this._closeDelegate);

        this.get_permissionsDialog().set_reloadOnShow(true);

        jQuery("body").addClass("sfNoSidebar");
    },

    dispose: function () {
        Telerik.Sitefinity.Multisite.Web.UI.SitesView.callBaseMethod(this, 'dispose');

        if (this._gridDataBoundDelegate) {
            delete this._gridDataBoundDelegate;
        }

        if (this._searchCommandDelegate) {
            if (this.get_searchWidget()) {
                this.get_searchWidget().remove_command(this._searchCommandDelegate);
            }
            delete this._searchCommandDelegate;
        }

        if (this._createSiteLinkClickDelegate) {
            if (this.get_createSiteLink()) {
                $removeHandler(this.get_createSiteLink(), "click", this._createSiteLinkClickDelegate);
            }
            delete this._createSiteLinkClickDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        if (this._closeDelegate) {
            if (this._siteDetailView) {
                this._siteDetailView.remove_close(this._closeDelegate);
            }
            delete this._closeDelegate;
        }
    },

    initializeDataSource: function () {
        var search = window.location.search;
        var queryStringParser = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(search.substring(1, search.length));
        var filter = queryStringParser.get("filter");
        var newQuery = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        if (filter) {
            newQuery.set("filter", encodeURIComponent(filter));
        }
        this._lastQuery = newQuery.toString(true);

        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: this.getSitesDataUrl(this._lastQuery),
                    contentType: 'application/json; charset=utf-8',
                    type: "GET",
                    dataType: "json"
                }
            },
            schema: {
                data: "Items",
                total: "TotalCount"
            },
            converters: {
                "text json": function (data) {
                    return Sys.Serialization.JavaScriptSerializer.deserialize(data);
                }
            },
            requestStart: function (e) {
                jQuery('body').addClass('sfLoadingTransition');
            },
            change: function (e) {
                jQuery('body').removeClass('sfLoadingTransition');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                jQuery('body').removeClass('sfLoadingTransition');
                var errText;
                if (jqXHR.responseText) {
                    errText = Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail;
                }
                else {
                    errText = jqXHR.status;
                }
                alert(errText);
            },
            pageSize: this._pageSize,
            serverPaging: true,
            serverFiltering: true
        });
    },

    initializeGridView: function () {
        jQuery(this.get_grid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: jQuery.proxy(kendo.template(jQuery(this.getSelectors().gridViewRowTemplate).html()), this),
            scrollable: false,
            pageable: true,
            autoBind: true,
            dataBound: this._gridDataBoundDelegate
        });
        jQuery(this.get_grid()).show();
    },

    getSelectors: function () {
        return this._selectors;
    },

    getSitesDataUrl: function (query) {
        var serviceUrl;

        if (this.get_webServiceUrl().indexOf("?") > -1) {
            serviceUrl = this.get_webServiceUrl() + "&sortExpression=Name";
        }
        else {
            serviceUrl = this.get_webServiceUrl() + "?sortExpression=Name";
        }

        if (query) {
            serviceUrl = serviceUrl + "&" + query;
        }

        return serviceUrl;
    },

    // ----------------------------------------- Event handlers ---------------------------------------

    _onLoadHandler: function () {
        this.initializeDataSource();
        this.initializeGridView();
    },

    _gridDataBoundHandler: function (e) {
        var data = jQuery(this.get_grid()).data("kendoGrid").dataSource.data();
        var that = this;
        for (var i = 0; i < data.length; i++) {

            var setClick = function (dataItem) {
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_Start_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Start);
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_Stop_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Stop);
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_Edit_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Edit);
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_Delete_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Delete);
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_SetPermissions_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.SetPermissions);
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_Configure_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Configure);
                that._bindAnchorClick(dataItem, that.get_id() + "_sitesGrid_Default_" + dataItem.Id, Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.SetDefault);

                var editAnchor = jQuery("#" + that.get_id() + "_Edit_" + dataItem.Id);
                editAnchor.click(function () {
                    that.get_siteDetailView().show(false, dataItem.Id);
                });

                var showAnchor = jQuery("#" + that.get_id() + "_Show_" + dataItem.Id);
                showAnchor.click(function () {
                    var queryString = String.format("?sf_site={0}", dataItem.Id);
                    var dashboardUrlPerSite = that.get_dashboardUrl() + queryString;
                    window.location.href = dashboardUrlPerSite;
                });
            }

            setClick(data[i]);

            var expandableLabelId = this.get_id() + "_expandableLabel_" + data[i].Id;
            jQuery("#" + expandableLabelId).expandableLabel({
                maxCharSize: 22,
                items: data[i].CultureDisplayNames,
                moreText: this.get_clientLabelManager().getLabel('Labels', 'MoreText'),
                lessText: this.get_clientLabelManager().getLabel('Labels', 'LessText')
            });
        }
        jQuery(".sfActionsMenu").kendoMenu({
            animation: false,
            openOnClick: {
                rootMenuItems: true
            }
        });

        //hides the pager if there is only one page
        if (e.sender.dataSource.total() <= this._pageSize) {
            jQuery(".k-pager-wrap").hide();
        }
        else {
            jQuery(".k-pager-wrap").show();
        }
    },

    _bindAnchorClick: function (dataItem, id, operation) {
        var anch = $("#" + id);
        if (anch) {
            var that = this;
            switch (operation) {
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Start:
                    anch.click(function () {
                        var site = {
                            "Id": dataItem.Id,
                            "IsOffline": false
                        };
                        that._setSiteIsOffline(site);
                    });
                    break;
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Stop:
                    anch.click(function () {
                        that.get_stopConfirmationDialog().set_title(that.get_clientLabelManager().getLabel('MultisiteResources', 'SetOfflineSiteTitle'));
                        that.get_stopConfirmationDialog().set_message(that.get_clientLabelManager().getLabel('MultisiteResources', 'SetOfflineSiteMessage'));
                        if (that.get_pageBehaviourField()) {
                            that.get_pageBehaviourField().reset(dataItem.SiteMapRootNodeId);
                        }

                        var promptCallback = function (sender, args) {
                            if (args.get_commandName() === "stop") {
                                var offlineMessage = null;
                                var offlinePageId = null;
                                if (that.get_pageBehaviourField()) {
                                    offlineMessage = that.get_pageBehaviourField().get_value().Message;
                                    offlinePageId = that.get_pageBehaviourField().get_value().PageId;
                                }

                                var site = {
                                    "Id": dataItem.Id,
                                    "IsOffline": true,
                                    "OfflineSiteMessage": offlineMessage,
                                    "OfflinePageToRedirect": offlinePageId
                                };
                                that._setSiteIsOffline(site);
                            }
                        };

                        that.get_stopConfirmationDialog().show_prompt(null, null, promptCallback, that);
                    });
                    break;
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Delete:
                    anch.click(function () {

                        that.get_deleteConfirmationDialog().set_title(that.get_clientLabelManager().getLabel('MultisiteResources', 'DeleteSiteTitle'));
                        that.get_deleteConfirmationDialog().set_message(that.get_clientLabelManager().getLabel('MultisiteResources', 'DeleteSiteMessage'));

                        var promptCallback = function (sender, args) {
                            if (args.get_commandName() === "delete") {
                                that._deleteSite(dataItem.Id);
                            }
                        };

                        that.get_deleteConfirmationDialog().show_prompt(null, null, promptCallback);
                    });
                    break;
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Configure:
                    anch.click(function () {
                        that.get_siteDetailView().show(false, dataItem.Id, true);
                    });
                    break;
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.SetPermissions:
                    anch.click(function () {
                        var url = String.format(that._permissionsDialogUrl, encodeURIComponent(dataItem.Name), dataItem.Id);
                        that.get_permissionsDialog().set_navigateUrl(url);
                        that.get_permissionsDialog().show();
                        that.get_permissionsDialog().center();
                        that.get_permissionsDialog().maximize();
                    });
                    break;
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.Edit:
                    anch.click(function () {
                        that.get_siteDetailView().show(false, dataItem.Id);
                    });
                    break;
                case Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.SetDefault:
                    anch.click(function () {
                        that._setDefaultSite(dataItem.Id);
                    });
                    break;
                default:
                    alert('Unsupported site operation: ' + operation);
                    break;
            }
        }
    },

    _createSiteLinkClickHandler: function (e) {
        var that = this;
        that.get_siteDetailView().show(true);
    },

    _deleteSite: function (siteId) {
        var that = this;
        jQuery.ajax({
            type: 'DELETE',
            url: this.get_webServiceUrl() + String.format('{0}/', siteId),
            contentType: "application/json",
            processData: false,
            success: function () { that.fetchDataSource(); }
        });
    },

    _setSiteIsOffline: function (site) {
        var that = this;
        jQuery.ajax({
            type: 'PUT',
            url: this.get_webServiceUrl() + 'offline/',
            data: Sys.Serialization.JavaScriptSerializer.serialize(site),
            contentType: "application/json",
            processData: false,
            success: function () { that.fetchDataSource(); }
        });
    },

    _setDefaultSite: function (siteId) {
        var that = this;
        jQuery.ajax({
            type: 'PUT',
            url: String.format(this.get_webServiceUrl() + 'default/{0}/', siteId),
            contentType: "application/json",
            processData: false,
            success: function () { that.fetchDataSource(); }
        });
    },

    fetchDataSource: function () {
        this._dataSource.fetch();
    },

    _onSearchCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case sender._searchCommandName:
                var searchArg = args.get_commandArgument().get_query();
                var query = 'filter=' + encodeURIComponent('Name.ToUpper().Contains("' + searchArg.toUpperCase() + '")');
                this.filterSites(query);
                break;
            case sender._closeSearchCommandName:
                this.filterSites("");
                break;
            default:
                break;
        }
    },

    filterSites: function (query) {
        var dataSource = jQuery(this.get_grid()).data().kendoGrid.dataSource;

        dataSource.transport.options.read.url = this.getSitesDataUrl(query);
        dataSource.page(1);
    },

    _close: function (sender, args) {
        if (args.get_data()) {
            this.fetchDataSource();

            if (args.get_data().IsCurrentSite) {
                var siteName = String.format(this.get_clientLabelManager().getLabel('Labels', 'BackToSite'), args.get_data().Name);
                jQuery(".sfHeader .sfBack").html(siteName.htmlEncode());
            }
        }

        jQuery(this.get_grid()).data('kendoGrid').dataSource.read();
        jQuery(this.get_grid()).data('kendoGrid').refresh();
    },

    // ----------------------------------------- Properties ---------------------------------------

    get_grid: function () {
        return this._grid;
    },
    set_grid: function (value) {
        this._grid = value;
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_siteDetailView: function () {
        return this._siteDetailView;
    },
    set_siteDetailView: function (value) {
        this._siteDetailView = value;
    },
    get_showSite: function () {
        return this._showSite;
    },
    set_showSite: function (value) {
        this._showSite = value;
    },
    get_dashboardUrl: function () {
        return this._dashboardUrl;
    },
    set_dashboardUrl: function (value) {
        this._dashboardUrl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_searchWidget: function () {
        return this._searchWidget;
    },
    set_searchWidget: function (value) {
        this._searchWidget = value;
    },
    get_createSiteLink: function () {
        return this._createSiteLink;
    },
    set_createSiteLink: function (value) {
        this._createSiteLink = value;
    },
    get_pageBehaviourField: function () {
        return this._pageBehaviourField;
    },
    set_pageBehaviourField: function (value) {
        this._pageBehaviourField = value;
    },
    get_stopConfirmationDialog: function () {
        return this._stopConfirmationDialog;
    },
    set_stopConfirmationDialog: function (value) {
        this._stopConfirmationDialog = value;
    },
    get_deleteConfirmationDialog: function () {
        return this._deleteConfirmationDialog;
    },
    set_deleteConfirmationDialog: function (value) {
        this._deleteConfirmationDialog = value;
    },
    get_permissionsDialog: function () {
        return this._permissionsDialog;
    },
    set_permissionsDialog: function (value) {
        this._permissionsDialog = value;
    },
    get_separateUsersPerSiteModeEnabled: function () {
        return this._separateUsersPerSiteModeEnabled;
    },
    set_separateUsersPerSiteModeEnabled: function (value) {
        this._separateUsersPerSiteModeEnabled = value;
    }
}

Telerik.Sitefinity.Multisite.Web.UI.SitesView.registerClass('Telerik.Sitefinity.Multisite.Web.UI.SitesView', Sys.UI.Control);

// ------------------------------------------------------------------------
// SiteOperation Enum 
// ------------------------------------------------------------------------
Type.registerNamespace("Telerik.Sitefinity.Multisite.Web.Services");

Telerik.Sitefinity.Multisite.Web.Services.SiteOperation = function () {
};
Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.prototype = {
    Start: 0,
    Stop: 1,
    Delete: 2,
    Configure: 3,
    SetPermissions: 4,
    Edit: 5,
    SetDefault: 6
};
Telerik.Sitefinity.Multisite.Web.Services.SiteOperation.registerEnum("Telerik.Sitefinity.Multisite.Web.Services.SiteOperation");

Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode = function () {
};
Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.prototype = {
    ConfigureManually: 0,
    ConfigureByDeployment: 1
};
Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode.registerEnum("Telerik.Sitefinity.Multisite.Web.Services.SiteConfigurationMode");
