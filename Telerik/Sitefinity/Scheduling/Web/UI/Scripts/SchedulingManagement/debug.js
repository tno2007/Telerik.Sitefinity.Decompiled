Type.registerNamespace("Telerik.Sitefinity.Scheduling.Web.UI");

Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement = function (element) {
    Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement.initializeBase(this, [element]);
    this._onReadyDelegate = null;
    this._webServiceUrl = null;
    this._gridDataBounDelegate = null;
    this._scheduledTasksGrid = null;
    this._deleteScheduledTaskPromptDialog = null;
    this._clientLabelManager = null;
    this._executeCommandDelegate = null;
    this._loadGridDataDelegate = null;
    this._loadFilterStatusDataDelegate = null;
    this._sortOption = Telerik.Sitefinity.Scheduling.Web.UI.OrderBy.ScheduledForDate;
    this._filterStatus = Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus.All;
    this._searchWidget = null;
    this._searchCommandDelegate = null;
    this._searchTerm = "";
}

Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement.callBaseMethod(this, "initialize");
        this._onReadyDelegate = Function.createDelegate(this, this._onLoadHandler);
        this._gridDataBounDelegate = Function.createDelegate(this, this._onGridDataBounHandler);
        this._executeCommandDelegate = Function.createDelegate(this, this._onExecuteCommand);
        this._loadGridDataDelegate = Function.createDelegate(this, this.loadGridData);
        this._loadFilterStatusDataDelegate = Function.createDelegate(this, this.loadFilterStatusData);

        this._searchCommandDelegate = Function.createDelegate(this, this._onSearchCommand);
        this.get_searchWidget().add_command(this._searchCommandDelegate);
        $(document).ready(this._onReadyDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement.callBaseMethod(this, "dispose");

        if (this._onReadyDelegate) {
            delete this._onReadyDelegate;
        }

        if (this._gridDataBounDelegate) {
            delete this._gridDataBounDelegate;
        }

        if (this._executeCommandDelegate) {
            delete this._executeCommandDelegate;
        }

        if (this._loadGridDataDelegate) {
            delete this._loadGridDataDelegate;
        }

        if (this._searchCommandDelegate) {
            delete this._searchCommandDelegate;
        }

        if (this._loadFilterStatusDataDelegate) {
            delete this._loadFilterStatusDataDelegate;
        }
    },
    _onSearchCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case sender._searchCommandName:
                var searchArg = args.get_commandArgument().get_query();

                // Set filter to All when search button is clicked.
                this._filterStatus = Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus.All;
                this._searchTerm = searchArg;
                break;
            case sender._closeSearchCommandName:
                this._searchTerm = "";
                break;
            default:
                break;
        }

        this.get_filterByStatusListElement().dataSource.read();
        this.get_kendoGridElement().dataSource.page(1);
    },
    _bindSortingDropdown: function () {
        var that = this;

        $("#scheduledTaskSortingList").on("change", function (e) {
            that._sortOption = this.value;
            that.get_filterByStatusListElement().dataSource.read();
            that.get_kendoGridElement().dataSource.page(1);
        }
        );
    },
    _bindFilterStatusList: function () {
        var that = this;

        var filterStatusDataSource = new kendo.data.DataSource({
            transport: {
                read: that._loadFilterStatusDataDelegate
            },
        });

        $(this.get_filterByStatusList()).kendoListView({
            dataSource: filterStatusDataSource,
            template: jQuery.proxy(kendo.template($("#filter-by-status-template").html()), this),
            contentElement: "ul",
            selectable: true,
            dataBound: function (e) {
                var listView = e.sender;
                var item = listView.content.children()[that._filterStatus]
                $(item).addClass("k-state-selected");
            },
            change: function (e) {
                var index = e.sender.select().index();
                var selectedElement = e.sender.dataSource.data()[index];
                that._filterStatus = selectedElement.Id;

                // Close the search bar on filter change
                if (that.get_searchWidget().get_commandName() === that.get_searchWidget()._searchCommandName) {
                    that.get_searchWidget().get_hideSearchBoxLink().click();
                }
                else {
                    that.get_filterByStatusListElement().dataSource.read();
                    that.get_kendoGridElement().dataSource.page(1);
                }
            }
        });

        $(this.get_filterByStatusList()).find("ul").addClass("sfListAutoWidthItems");
    },
    loadGridData: function (options) {
        var skip = 0;
        var take = 50;
        if (options && options.data) {
            skip = options.data.skip;
            take = options.data.take;
        }

        var requestUrl = this.get_webServiceUrl() + "/scheduled-tasks?format=json&orderBy=" + this._sortOption + "&skip=" + skip + "&take=" + take + "&searchTerm=" + this._searchTerm + "&filterBy=" + this._filterStatus;
        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            contentType: "application/json",
            processData: false,
            success: function (response) {
                options.success(response);
            },
            error: function (err) {
            }
        });
    },
    loadFilterStatusData: function (options) {
        var that = this;
        var requestUrl = this.get_webServiceUrl() + "/scheduled-task-statuses";

        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            contentType: "application/json",
            processData: false,
            beforeSend: function () {
                that.get_filterByStatusList().hide();
                that.get_loadingFilterByStatusSection().show();
            },
            success: function (response) {
                options.success(response);
            },
            error: function (err) {
            },
            complete: function () {
                that.get_loadingFilterByStatusSection().hide();
                that.get_filterByStatusList().show();
            }
        });
    },
    _onLoadHandler: function () {
        this._setLoadingDim(true);

        var that = this;
        jQuery("body").addClass("sfHasSidebar");

        // Load first the filter status in order to take the correct total count for the kendo grid.
        that._bindFilterStatusList();
        that._bindSortingDropdown();

        var dataSource = new kendo.data.DataSource({
            transport: {
                read: that._loadGridDataDelegate
            },
            schema: {
                data: function (response) {
                    return response.Items
                },
                total: "TotalCount",
                model: {
                    fields: {
                        Title: { type: "string" },
                        Name: { type: "string" },
                        Status: { type: "string" },
                        ScheduledForDate: { type: "date" },
                        LastExecutionTime: { type: "date" },
                        RecurringInterval: { type: "string" },
                        LastModified: { type: "date" },
                        ErrorMessage: { type: "string" }
                    }
                }
            },
            pageSize: 50,
            serverPaging: true,
            change: function (e) {
                // Kendo pager will be displayed even when there is only 1 page. (50 items) https://docs.telerik.com/kendo-ui/api/javascript/ui/grid/configuration/pageable.alwaysvisible
                // We need to show it only if there are more than 1 pages
                if (this.totalPages() <= 1) {
                    that.get_pagerElement().hide();
                }
                else {
                    that.get_pagerElement().show();
                }
            }
        });

        $(this.get_scheduledTasksGrid()).kendoGrid({
            dataSource: dataSource,
            pageable: {
                responsive: false,
            },
            rowTemplate: jQuery.proxy(kendo.template($("#row-template").html()), this),
            dataBound: this._gridDataBounDelegate,
            scrollable: false,
            noRecords: {
                template: jQuery.proxy(kendo.template($("#no-results-template").html()), this)
            },
            page: function () {
                window.scrollTo({ top: 0 });
            }
        })
            .delegate(".sfDeleteItm", "click", function (e) {
                var anchor = e.target;
                var kendoDataItem = that.get_kendoGridElement().dataItem(jQuery(anchor).closest("tr"));
                var promtDialog = that.get_deleteScheduledTaskPromptDialog();
                if (kendoDataItem.Status === "Stopped" || kendoDataItem.Status === "Started") {
                    var stoppedTaskDeletePromptMessage = that.get_clientLabelManager().getLabel("SchedulingResources", "DeleteStoppedScheduledTaskConfirmationMessage");
                    promtDialog.set_message(stoppedTaskDeletePromptMessage);
                }
                else {
                    promtDialog.set_message(promtDialog.get_initialMessage());
                }

                that.get_deleteScheduledTaskPromptDialog().show_prompt(null, null, function (sender, args) {
                    if (args.get_commandName() === "delete") {
                        var callback = function () {
                            var kendoGridDataSource = that.get_kendoGridElement().dataSource
                            var currentGridDataLength = kendoGridDataSource.data().length;

                            that.get_filterByStatusListElement().dataSource.read();
                            if (currentGridDataLength <= 1) {
                                kendoGridDataSource.page(1);
                            }
                            else {
                                kendoGridDataSource.read();
                            }
                        };

                        that._executeCommandDelegate(kendoDataItem.TaskId, Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Delete, kendoDataItem.LastModified, callback, callback);
                    }
                });
            })
            .delegate(".sfTooltip", "click", function (e) {
                var anchor = e.target;
                var toolTipTextElement = $(anchor).closest("td").find(".sfDetailsPopup");
                toolTipTextElement.toggle();
            })
            .delegate(".sfStartTask", "click", function (e) {
                var anchor = e.target;
                var kendoDataItem = that.get_kendoGridElement().dataItem(jQuery(anchor).closest("tr"));

                var callback = function () {
                    that.get_filterByStatusListElement().dataSource.read();
                    that.get_kendoGridElement().dataSource.read();
                };

                that._executeCommandDelegate(kendoDataItem.TaskId, Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Start, kendoDataItem.LastModified, callback, callback);
            })
            .delegate(".sfRetryTask", "click", function (e) {
                var anchor = e.target;
                var kendoDataItem = that.get_kendoGridElement().dataItem(jQuery(anchor).closest("tr"));

                var callback = function () {
                    that.get_filterByStatusListElement().dataSource.read();
                    that.get_kendoGridElement().dataSource.read();
                };

                that._executeCommandDelegate(kendoDataItem.TaskId, Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Restart, kendoDataItem.LastModified, callback, callback);
            });
    },
    _onExecuteCommand: function (taskId, command, lastModified, successCallback, errorCallBack) {
        var manageScheduledTaskUrl = this.get_webServiceUrl() + "/scheduled-tasks?" + "taskId=" + taskId + "&operation=" + command + "&lastmodified=" + lastModified.toISOString();
        var that = this;
        this._setLoadingDim(true);

        jQuery.ajax({
            type: 'PUT',
            url: manageScheduledTaskUrl,
            contentType: "application/json",
            processData: false,
            success: function () {
                that._setLoadingDim(false);
                successCallback();
            },
            error: function () {
                that._setLoadingDim(false);
                errorCallBack();
            }
        });
    },
    _onGridDataBounHandler: function () {
        this._setLoadingDim(false);
        var that = this;
        var grid = jQuery(that.get_scheduledTasksGrid());
        var filterByStatusData = that.get_filterByStatusListElement().dataSource.data();

        var totalTasks = filterByStatusData.length > 0 ? filterByStatusData[Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus.All].TasksCount : grid.data("kendoGrid").dataSource.total();
        if (totalTasks === 0 && !that._searchTerm) {
            // show empty grid state
            that.get_emptyStateWrapper().show();
            that.get_scheduledTaskGridWrapper().hide();
            that.get_toolbarElement().hide();
            that.get_SortWrapper().hide();
            that.hideFilterSideBar();
            return;
        } else if (totalTasks === 0 && that._searchTerm) {
            // there are no search results
            return;
        }

        var tasksOnCurrentPage = grid.data("kendoGrid").dataSource.data().length;
        if (tasksOnCurrentPage === 0 && totalTasks > 0) {

            if (!that._searchTerm) {
                var kendoGridDataSource = that.get_kendoGridElement().dataSource;
                var pageToLoad = kendoGridDataSource.page() - 1;

                // the last page was requested and there are not items on it - we load the previous page
                if (pageToLoad > 0) {
                    kendoGridDataSource.page(pageToLoad);
                    that.get_filterByStatusListElement().dataSource.read();
                    that.get_kendoGridElement().dataSource.read();
                    return;
                }
                else {
                    // Filter Status with no tasks is requested;
                    return;
                }
            }
            else {
                // When we search with not exisitng task name. Return there are no search results.
                return;
            }
        }

        // show the grid, the toolbar and hide the empty state
        that.get_emptyStateWrapper().hide();
        that.get_scheduledTaskGridWrapper().show();
        that.get_toolbarElement().show();
        that.get_SortWrapper().show();

        // bind actions column
        grid.find(".sfActionsMenu").kendoMenu({
            animation: false,
            openOnClick: {
                rootMenuItems: true
            }
        });

        grid.find("tr").each(function () {
            var dataItem = grid.data("kendoGrid").dataItem(this);
            var statusIconStyle = that.calculateStatusIcon(dataItem.Status);
            $(this).find(".status-column").addClass(statusIconStyle);

        });
    },
    calculateStatusIcon: function (status) {
        switch (status) {
            case 'Pending':
                return "sfProfilerStatusScheduled"
            case 'Started':
                return "sfProfilerStatusStarted"
            case 'Stopped':
                return "sfProfilerStatusStopped"
            case 'Failed':
                return "sfProfilerStatusFailed"
            default:
                return "sfProfilerStatusFailed"
        }
    },
    hideFilterSideBar: function () {
        this.get_modulesFilters().hide();
        this.get_pageBody().removeClass("sfHasSidebar");
        this.get_scheduledTasksTooltip().hide();
    },
    get_pageBody: function () {
        return jQuery("body");
    },
    get_scheduledTasksTooltip: function () {
        return jQuery("#scheduletasksfaqclassic");
    },
    _setLoadingDim: function (flag) {
        if (flag) {
            jQuery("#sf_scheduledTasksGridLoading").show();
        } else {
            jQuery("#sf_scheduledTasksGridLoading").hide();
        }
    },
    get_scheduledTaskGridWrapper: function () {
        return jQuery("#scheduledTasksManagementGridWrapper");
    },
    get_emptyStateWrapper: function () {
        return jQuery("#emptyGridState");
    },
    get_SortWrapper: function () {
        return jQuery(".sfQuickSort");
    },
    get_pagerElement: function () {
        return jQuery("div[data-role = 'pager']");
    },
    get_toolbarElement: function () {
        return jQuery("#toolbar");
    },
    get_modulesFilters: function () {
        return jQuery("#modulesFilters");
    },
    get_loadingFilterByStatusSection: function () {
        return jQuery("#loadingFilterByStatusSection");
    },
    get_filterByStatusList: function () {
        return jQuery("#filterByStatusList");
    },
    get_filterByStatusListElement: function () {
        return this.get_filterByStatusList().data("kendoListView");
    },
    get_kendoGridElement: function () {
        return jQuery(this.get_scheduledTasksGrid()).data("kendoGrid");
    },
    get_webServiceUrl: function () {
        return this._webServiceUrl;
    },
    set_webServiceUrl: function (value) {
        this._webServiceUrl = value;
    },
    get_scheduledTasksGrid: function () {
        return this._scheduledTasksGrid;
    },
    set_scheduledTasksGrid: function (value) {
        this._scheduledTasksGrid = value;
    },
    get_deleteScheduledTaskPromptDialog: function () {
        return this._deleteScheduledTaskPromptDialog;
    },
    set_deleteScheduledTaskPromptDialog: function (value) {
        this._deleteScheduledTaskPromptDialog = value;
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
    }
}

Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement.registerClass('Telerik.Sitefinity.Scheduling.Web.UI.SchedulingManagement', Sys.UI.Control);

// ------------------------------------------------------------------------
// Enum OrderBy
// ------------------------------------------------------------------------

Telerik.Sitefinity.Scheduling.Web.UI.OrderBy = function () {
};
Telerik.Sitefinity.Scheduling.Web.UI.OrderBy.prototype = {
    ScheduledForDate: 0,
    LastExecutionDate: 1,
    NameAsc: 2,
    NameDesc: 3,
    Status: 4
};
Telerik.Sitefinity.Scheduling.Web.UI.OrderBy.registerEnum("Telerik.Sitefinity.Scheduling.Web.UI.OrderBy");

// ------------------------------------------------------------------------
// Enum FilterByStatus
// ------------------------------------------------------------------------
Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus = function () {
};


Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus.prototype = {
    All: 0,
    Pending: 1,
    Started: 2,
    Stopped: 3,
    Failed: 4,
    Recurring: 5
};

Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus.registerEnum("Telerik.Sitefinity.Scheduling.Web.UI.FilterByStatus");
