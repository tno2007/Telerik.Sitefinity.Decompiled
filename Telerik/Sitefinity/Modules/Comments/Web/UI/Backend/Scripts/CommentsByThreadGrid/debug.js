﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend");
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid.initializeBase(this, [element]);
    this._grid = null;
    this._webServiceUrl = null;
    this._threadKey = null;
    this._threadTitle = null;

    this._selectors = null;
    this._dataSource = null;
    this._queryParams = null;
    this._culture = null;
    this._uiCulture = null;

    this._gridDataBoundDelegate = null;
    this._onLoadDelegate = null;
    this._commentsPerPage = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid.prototype = {
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid.callBaseMethod(this, "initialize");
        this.set_queryParams({ ThreadKey: this.get_threadKey() });
        this._selectors = {
            gridViewRowTemplate: "#commentsGridRowTemplate"
        };

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        if (this._gridDataBoundDelegate) {
            delete this._gridDataBoundDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    //Initializing the daasource of the kendo grid
    initializeDataSource: function () {
        var that = this;
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: this.get_webServiceUrl() + "/comments/filter",
                    contentType: 'application/json; charset=utf-8',
                    type: "POST",
                    cache: false,
                    dataType: "json"
                },
                parameterMap: function (options, type) {
                    that.get_queryParams().ThreadKey = that.get_threadKey();
                    var query = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                    that.get_queryParams().Language = query.get("lang", null);
                    that.get_queryParams().Take = options.take;
                    that.get_queryParams().Skip = options.skip;
                    return Telerik.Sitefinity.JSON.stringify(that.get_queryParams());
                }
            },
            serverPaging: true,
            pageSize: that.get_commentsPerPage(),
            schema: {
                data: "Items",
                total: "TotalCount"
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var errText;
                if (jqXHR.responseText) {
                    errText = Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail;
                }
                else {
                    errText = jqXHR.status;
                }
                alert(errText);
            },
            change: function (e) {
                var commentsData = e.sender.data()
                for (i = 0; i < commentsData.length; i++) {
                    commentsData[i].ThreadTitle = that.get_threadTitle();
                }

                if (this.totalPages() <= 1) {
                    jQuery("div[data-role = 'pager']").hide();
                }
                else {
                    jQuery("div[data-role = 'pager']").show();
                }
            }
        });
    },

    initializeGridView: function () {
        var that = this;
        jQuery(this.get_grid()).kendoGrid({
            dataSource: this._dataSource,
            rowTemplate: kendo.template(jQuery(this.getSelectors().gridViewRowTemplate).html()),
            scrollable: false,
            pageable: {
                info: false
            },
            autoBind: true,
            dataBound: this._gridDataBoundDelegate
        })
         .delegate(".sfHeaderSelectComment", "click", function () {
             that._raiseItemCommand("headerSelect", this);
         })
        .delegate(".sfSelectComment", "click", function () {
            that._raiseItemCommand("rowSelect", this);
        })
        .delegate(".sfEditBtn", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            that._raiseItemCommand("edit", kendoDataItem.Key);
        })
        .delegate(".sfUnpublish", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            that._raiseItemCommand("hide", kendoDataItem.Key);
        })
        .delegate(".sfPublish", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            that._raiseItemCommand("publish", kendoDataItem.Key);
        })
        .delegate(".sfDelBtn", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            that._raiseItemCommand("delete", kendoDataItem.Key);
        })
        .delegate(".sfMarkSpam", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            that._raiseItemCommand("markAsSpam", kendoDataItem.Key);
        });

        jQuery(this.get_grid()).show();
    },

    getSelectors: function () {
        return this._selectors;
    },

    fetchDataSource: function () {
        this._dataSource.read();
    },

    add_itemCommand: function (delegate) {
        /// <summary>Happens when a custom command was fired inside the container. Can be cancelled.</summary>
        this.get_events().addHandler('itemCommand', delegate);
    },
    remove_itemCommand: function (delegate) {
        /// <summary>Happens when a custom command was fired inside the container. Can be cancelled.</summary>
        this.get_events().removeHandler('itemCommand', delegate);
    },

    _raiseItemCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('itemCommand');
        if (handler)
            handler(this, eventArgs);
        return eventArgs;
    },

    ///* --------------------  events handlers ----------- */

    _onLoadHandler: function () {
        this.initializeDataSource();
        this.initializeGridView();
        jQuery("body").addClass("sfHasSidebar");
    },

    _gridDataBoundHandler: function (e) {
        jQuery(this.get_element()).find(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });
    },

    ///* --------------------  private methods ----------- */

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    },

    /* -------------------- properties ---------------- */
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

    get_commentsPerPage: function () {
        return this._commentsPerPage;
    },
    set_commentsPerPage: function (value) {
        this._commentsPerPage = value;
    },

    get_threadKey: function () {
        return this._threadKey;
    },
    set_threadKey: function (value) {
        this._threadKey = value;
    },

    get_threadTitle: function () {
        return this._threadTitle;
    },
    set_threadTitle: function (value) {
        this._threadTitle = value;
    },

    get_queryParams: function () {
        if (!this._queryParams) {
            this._queryParams = {};
        }
        this._queryParams.sortDescending = true;
        return this._queryParams;
    },
    set_queryParams: function (value) {
        this._queryParams = value;        
    }
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsByThreadGrid", Sys.UI.Control);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs = function (commandName, commandArgument) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.initializeBase(this);
    if (commandArgument && commandArgument.get_commandName && commandArgument.get_commandArgument && commandArgument.get_cancel) {
        this._commandName = commandArgument.get_commandName();
        this._commandArgument = commandArgument.get_commandArgument();
        this.set_cancel(commandArgument.get_cancel());
    }
    else {
        this._commandName = commandName;
        this._commandArgument = commandArgument;
    }
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.registerClass('Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs', Sys.CancelEventArgs);