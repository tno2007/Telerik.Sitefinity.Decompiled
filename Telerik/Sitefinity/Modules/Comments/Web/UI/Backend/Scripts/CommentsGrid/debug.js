Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend");
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid.initializeBase(this, [element]);
    this._grid = null;
    this._webServiceUrl = null;
    this._commentsByThreadUrl = null;

    this._selectors = null;
    this._dataSource = null;
    this._queryParams = "";
    this._culture = null;
    this._uiCulture = null;

    this._gridDataBoundDelegate = null;
    this._onLoadDelegate = null;
    this._getThreadsFilterSuccessDelegate = null;
    this._getThreadsCountSuccessDelegate = null;

    this._commentsDataSource;
    this._commentsRestClient = null;
    this._commentsPerPage = null;
    this._siteId = null;
    this._threadsFilterResponse;
    this._distinctThreadKeys;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid.callBaseMethod(this, "initialize");
        this._selectors = {
            gridViewRowTemplate: "#commentsGridRowTemplate"
        };

        this.set_commentsRestClient(new Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient(this.get_webServiceUrl()));

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBoundHandler);
        this._getThreadsFilterSuccessDelegate = Function.createDelegate(this, this._getThreadsFilterSuccess);
        this._getThreadsCountSuccessDelegate = Function.createDelegate(this, this._getThreadsCountSuccess);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        if (this._gridDataBoundDelegate)
            delete this._gridDataBoundDelegate;

        if (this._getThreadsFilterSuccessDelegate) 
            delete this._getThreadsFilterSuccessDelegate;

        if (this._getThreadsCountSuccessDelegate) 
            delete this._getThreadsCountSuccessDelegate;

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    //Initializing the datasource of the kendo grid
    initializeDataSource: function () {
        var that = this;
        var url = this.get_webServiceUrl() + "/comments/filter";
        if (that.get_siteId())
            url += "?sf_site="+ this.get_siteId();
        this._dataSource = new kendo.data.DataSource({
            transport: {
                read: {
                    url: url,
                    contentType: 'application/json; charset=utf-8',
                    type: "POST",
                    cache: false,
                    dataType: "json"
                },
                parameterMap: function (options, type) {
                    that.get_queryParams().take = options.take;
                    that.get_queryParams().skip = options.skip;
                    var query = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                    if (!that.get_queryParams().threadType)
                        that.get_queryParams().threadType = query.get("threadType", null);

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
                that._commentsDataSource = e;
                that._buildKendoGridDataSource();
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
        .delegate(".sfCommentThreadView", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            if (that.get_queryParams()) {
                var lang = that.get_queryParams().language ? that.get_queryParams().language : "";
                window.location = that.get_commentsByThreadUrl() + "&targetKey=" + encodeURIComponent(kendoDataItem.ThreadKey) + "&lang=" + lang;
            }
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
        .delegate(".sfEditBtn", "click", function (e) {
            var anchor = e.target;
            var kendoDataItem = jQuery(that.get_grid()).data("kendoGrid").dataItem(jQuery(anchor).closest("tr"));
            that._raiseItemCommand("edit", kendoDataItem.Key);
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

    _buildKendoGridDataSource: function () {
        if (this._commentsDataSource.items.length > 0) {
            this._distinctThreadKeys = this.getThreadKeysDistinct();

            this.get_commentsRestClient().getThreads(this._distinctThreadKeys, this.get_siteId(), this._getThreadsFilterSuccessDelegate);            
        }
    },

    getThreadKeysDistinct: function () {
        var filter = {};
        filter.ThreadKey = [];

        for (var i = 0; i < this._commentsDataSource.items.length; i++) {
            if (!this.collectionContains(this._commentsDataSource.items[i], filter.ThreadKey)) {
                filter.ThreadKey.push(this._commentsDataSource.items[i].ThreadKey);
            }
        }
        return filter;
    },

    collectionContains: function (item, keys) {
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] === item.ThreadKey) return true;
        }
        return false;
    },

    _getThreadsFilterSuccess: function (data) {
        if (data && data.Items) {
            this._threadsFilterResponse = data;
            var status = ["Published", "WaitingForApproval", "Hidden", "Spam"];
            this.get_commentsRestClient().getCommentsCount(this._distinctThreadKeys.ThreadKey, status, this._getThreadsCountSuccessDelegate);
        }
    },

    _getThreadsCountSuccess: function (data) {
        if (this._threadsFilterResponse && data && data.Items) {
            var commentsData = this._commentsDataSource.sender.data()
            for (i = 0; i < commentsData.length; i++) {
                for (j = 0; j < this._threadsFilterResponse.Items.length; j++) {
                    if (commentsData[i].ThreadKey === this._threadsFilterResponse.Items[j].Key) {
                        commentsData[i].ThreadTitle = this._threadsFilterResponse.Items[j].Title;
                        commentsData[i].ThredType = this._threadsFilterResponse.Items[j].Type;
                        commentsData[i].Language = this._threadsFilterResponse.Items[j].Language;
                        commentsData[i].CommentedItemUrl = this._threadsFilterResponse.Items[j].ItemUrl;
                    }

                    if (commentsData[i].ThreadKey === data.Items[j].Key)
                        commentsData[i].CommentsCountByThread = data.Items[j].Count;
                }
            }
        }
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
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    ///* --------------------  events handlers ----------- */

    _onLoadHandler: function () {
        this.initializeDataSource();
        this.initializeGridView();
        jQuery("body").addClass("sfHasSidebar");
    },

    _gridDataBoundHandler: function (e) {
        jQuery(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });
    },

    /* -------------------- properties ---------------- */

    set_uiCulture: function (culture) {
        this._uiCulture = culture;
    },
    get_uiCulture: function () {
        return this._uiCulture;
    },
    get_commentsRestClient: function () {
        return this._commentsRestClient;
    },
    set_commentsRestClient: function (value) {
        this._commentsRestClient = value;
    },
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

    get_commentsByThreadUrl: function () {
        return this._commentsByThreadUrl;
    },
    set_commentsByThreadUrl: function (value) {
        this._commentsByThreadUrl = value;
    },

    get_commentsPerPage: function () {
        return this._commentsPerPage;
    },
    set_commentsPerPage: function (value) {
        this._commentsPerPage = value;
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
    },

    get_siteId: function () {
        return this._siteId;
    },
    set_siteId: function (value) {
        this._siteId = value;
    }
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGrid", Sys.UI.Control);

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