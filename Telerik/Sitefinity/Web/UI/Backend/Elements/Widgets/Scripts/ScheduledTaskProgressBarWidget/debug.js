Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget.initializeBase(this, [element]);

    this._taskId = null;
    this._serviceUrl = null;
    this._checkInterval = null;
    this._clientManager = null;
    this._name = null;

    this._wrapper = null;
    this._errorDetailsPanel = null;
    this._taskDescription = null;
    this._taskStatus = null;
    this._taskCommand = null;
    this._errorDetailsLink = null;
    this._errorDetailsMessage = null;
    this._taskProgressBar = null;
    this._taskProgressReport = null;
    this._intervalHandle = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget.callBaseMethod(this, 'initialize');

        this._initDelegates();
        this._addHandlers();
    },

    dispose: function () {

        this._removeHandlers();
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget.callBaseMethod(this, 'dispose');
    },

    /* Private Methods */

    _initDelegates: function () {

    },

    _addHandlers: function () {
        
    },

    _removeHandlers: function () {
        if (this._intervalHandle) {
            window.clearInterval(this._intervalHandle);
            this._intervalHandle = null;
        }
    },

    beginPolling: function() {
        var that = this;
        this._intervalHandle = window.setInterval(function () {
            that.refreshProgressBar();
        }, this.get_checkInterval());
    },

    /* Private Methods */

    refreshProgressBar: function () {
        if (this.get_taskId() == this.get_clientManager().GetEmptyGuid())
            return;

        var requestUrl = String.format("{0}/{1}/progress?time={2}", this.get_serviceUrl(), this.get_taskId(), new Date().getTime());
        
        this.get_clientManager().InvokeGet(requestUrl, [], [], this.onRefreshSuccess, null, this);
    },

    onRefreshSuccess: function(sender, taskData) {
        if (taskData && taskData.Id != sender.get_clientManager().GetEmptyGuid()) {
            $(sender.get_wrapper()).show();
            var description = taskData.Description;
            var taskDescription = sender.get_taskDescription();
            $(taskDescription).html(description);

            var taskStatus = sender.get_taskStatus();
            if ($(taskStatus).length > 0) {
                var statusMessage = Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.toString(taskData.Status);
                if (statusMessage == "Started") {
                    statusMessage = "In progress";
                }

                $(taskStatus).html(statusMessage);

                if (taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Failed || taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Stopped) {
                    $(taskStatus).removeClass("sfSuccessTxt");
                    $(taskStatus).addClass("sfWarningTxt");
                } else {
                    $(taskStatus).removeClass("sfWarningTxt");
                    $(taskStatus).addClass("sfSuccessTxt");
                }
            } else {
                // Preserve previous behavior for not updated templates
                var description = taskData.Description + " " + Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.toString(taskData.Status);
                $(taskDescription).html(description);
            }
            sender._onTaskProgressHandler({ Progress: taskData.ProgressStatus, Status: taskData.Status });
            if (taskData.ProgressStatus == 100) {
                $(sender.get_wrapper()).hide();
                sender._removeHandlers();
            }
            if (taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Failed) {
                sender.showTaskFailed(taskData);
            }
            else {
                sender.showTaskProgress(taskData);
                $(sender.get_errorDetailsPanel()).hide();
            }
        }
        else {
            $(sender.get_wrapper()).hide();
            sender._removeHandlers();
            sender._onTaskProgressHandler({ Progress: 100, Status: "Done" });
        }
    },

    showTaskProgress: function (taskData) {
        var progressReport = this.get_taskProgressReport();
        var progressBarEl = this.get_taskProgressBar();

        progressReport.innerHTML = taskData.ProgressStatus + "%";
        var colorCssClass = "sfProgressStarted";
        var command = Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Stop;

        if (taskData.Status == Telerik.Sitefinity.Scheduling.Web.UI.TaskStatuses.Stopped) {
            colorCssClass = "sfProgressStopped";
            command = Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Resume;
        }

        this.updateBinderCommandButton(command);
        this.updateProgressBar(progressBarEl, taskData.ProgressStatus, colorCssClass);
    },

    updateProgressBar: function (parentElement, percents, colorCssClass) {
        if (!colorCssClass) colorCssClass = "sfProgressStarted";

        if (parentElement.hasChildNodes()) {
            while (parentElement.childNodes.length >= 1) {
                parentElement.removeChild(parentElement.firstChild);
            }
        }
        $(parentElement).removeClass("sfProgressStarted sfProgressStopped");
        $(parentElement).addClass(colorCssClass);

        var innerParentDiv = document.createElement('div');
        $(innerParentDiv).addClass("sfProgressBar");

        var innerDiv = document.createElement('div');
        var minWidth = 3;
        var width = percents > 0 ? percents : minWidth;
        innerDiv.setAttribute('style', 'width : ' + width + '%;');
        $(innerDiv).addClass("sfProgressBarIn");

        innerParentDiv.appendChild(innerDiv);
        parentElement.appendChild(innerParentDiv);
    },

    showTaskFailed: function (taskData) {
        $(this.get_wrapper()).show();
        $(this.get_errorDetailsPanel()).show();
        var errorDetails = this.get_errorDetailsMessage(); 
        $(errorDetails).text(taskData.StatusMessage);
        var errorLink = this.get_errorDetailsLink();
        $(errorLink).unbind('click');
        $(errorLink).click(function () { $(errorDetails).toggle(); return false; });
        var progressBarEl = this.get_taskProgressBar();

        this.updateProgressBar(progressBarEl, taskData.ProgressStatus, "sfProgressStopped");
        this.updateBinderCommandButton(Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.Restart);
    },

    updateBinderCommandButton: function (command) {
        var commandButton = this.get_taskCommand();

        $(commandButton).removeAttr("href");
        $(commandButton).html(Telerik.Sitefinity.Scheduling.Web.UI.TaskCommands.toString(command));
        $(commandButton).unbind('click');
        var that = this;
        $(commandButton).click(function () {
            that.manageScheduledTask(command);
        });
    },

    manageScheduledTask: function (command) {
        var requestUrl = String.format("{0}/{1}/manage?command={2}", this.get_serviceUrl(), this.get_taskId(), command);

        this.get_clientManager().InvokePut(requestUrl, [], [], null, null, this);
    },

    /* Event Handlers */

    add_onTaskProgress: function (delegate) {
        this.get_events().addHandler('onTaskProgress', delegate);
    },
    remove_onTaskProgress: function (delegate) {
        this.get_events().removeHandler('onTaskProgress', delegate);
    },

    _onTaskProgressHandler: function (data) {
        var eventArgs = {
            get_data: function() {
                return data;
            }
        };

        var h = this.get_events().getHandler('onTaskProgress');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    /* Event Handlers */

    /* Properties */

    get_taskId: function () {
        return this._taskId;
    },
    set_taskId: function (value) {
        if (value && value != this.get_clientManager().GetEmptyGuid() && this._intervalHandle == null) {
            this._taskId = value;
            this.beginPolling();
        }
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_taskDescription: function () {
        return this._taskDescription;
    },
    set_taskDescription: function (value) {
        this._taskDescription = value;
    },

    get_taskStatus: function () {
        return this._taskStatus;
    },
    set_taskStatus: function (value) {
        this._taskStatus = value;
    },

    get_errorDetailsLink: function () {
        return this._errorDetailsLink;
    },
    set_errorDetailsLink: function (value) {
        this._errorDetailsLink = value;
    },

    get_errorDetailsMessage: function () {
        return this._errorDetailsMessage;
    },
    set_errorDetailsMessage: function (value) {
        this._errorDetailsMessage = value;
    },

    get_taskProgressBar: function () {
        return this._taskProgressBar;
    },
    set_taskProgressBar: function (value) {
        this._taskProgressBar = value;
    },

    get_taskProgressReport: function () {
        return this._taskProgressReport;
    },
    set_taskProgressReport: function (value) {
        this._taskProgressReport = value;
    },

    get_wrapper: function() {
        return this._wrapper;
    },
    set_wrapper: function(value) {
        this._wrapper = value;
    },
    
    get_errorDetailsPanel: function() {
        return this._errorDetailsPanel;
    },
    set_errorDetailsPanel: function(value) {
        this._errorDetailsPanel = value;
    },
    
    get_taskCommand: function() {
        return this._taskCommand;
    },
    set_taskCommand: function(value) {
        this._taskCommand = value;
    },

    get_checkInterval: function () {
        return this._checkInterval;
    },
    set_checkInterval: function (value) {
        this._checkInterval = value;
    },

    get_clientManager: function () {
        if (this._clientManager == null) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }
        
        return this._clientManager;
    },
    
    get_name: function() {
        return this._name;
    },
    set_name: function(value) {
        this._name = value;
    }

    /* Properties */
}
Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ScheduledTaskProgressBarWidget", Sys.UI.Control);
