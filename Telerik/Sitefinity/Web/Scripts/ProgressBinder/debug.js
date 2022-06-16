Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.ProgressBinder = function (element) {
    Telerik.Sitefinity.Web.UI.ProgressBinder.initializeBase(this, [element]);

    this._onloadDelegate = null;
    this._progressServiceUrl = null;
    this._refreshInterval = null;
    this._updateCurrentStateSuccessDelegate = null;
    this._updateCurrentStateFailureDelegate = null;
    this._currentStepResource = null;
    this._completedResource = null;
    this._targetId = null;
}

Telerik.Sitefinity.Web.UI.ProgressBinder.prototype = {
    initialize: function () {
        if (this._onloadDelegate == null) {
            this._onloadDelegate = Function.createDelegate(this, this._onload);
        }

        Sys.Application.add_load(this._onloadDelegate);

        this._updateCurrentStateSuccessDelegate = Function.createDelegate(this, this._updateCurrentStateSuccess);
        this._updateCurrentStateFailureDelegate = Function.createDelegate(this, this._updateCurrentStateFailure);

        Telerik.Sitefinity.Web.UI.ProgressBinder.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        this._binderItemDataBoundDelegate = null;

        if (this._onloadDelegate) {
            Sys.Application.remove_load(this._onloadDelegate);
            delete this._onloadDelegate;
        }

        if (this._updateCurrentStateSuccessDelegate) {
            delete this._updateCurrentStateSuccessDelegate;
        }

        if (this._updateCurrentStateFailureDelegate) {
            delete this._updateCurrentStateFailureDelegate;
        }

        Telerik.Sitefinity.Web.UI.ProgressBinder.callBaseMethod(this, "dispose");
    },

    _onload: function () {
    },

    _updateCurrentState: function () {
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        clientManager.InvokeGet(this._progressServiceUrl, null, null,
            this._updateCurrentStateSuccessDelegate, this._updateCurrentStateFailureDelegate, this, null);
    },

    _updateCurrentStateSuccess: function (obj, result) {
        var targetSelector = $("#" + this._targetId);
        var that = this;
        if (targetSelector.find(".sfProgressMarker").length > 0) {
            if (result.ProgressPerItem != null && result.ProgressPerItem.length > 0) {
                var finishedResource = this._completedResource;
                var currentStepResource = this._currentStepResource;

                var maxWidth = targetSelector.find(".sfProgressIndicatorMarker").width();
                var progressPerItemDict = [];
                for (var j = 0; j < result.ProgressPerItem.length; j++) {
                    var currentObject = result.ProgressPerItem[j];
                    progressPerItemDict[currentObject.Key] = currentObject.Value;
                }

                this._onProgressStarted(currentObject.Value);

                targetSelector.find(".sfProgressMarker").each(function () {
                    var progressValue = $(this).find(".sfProgressValueMarker");
                    var dataIdAttr = this.attributes["data-Id"];
                    if (dataIdAttr != undefined) {
                        var dataId = dataIdAttr.value;
                        var progressPerItem = progressPerItemDict[dataId];
                        if (!isNaN(progressPerItem)) {
                            if (progressPerItem >= 100) {
                                $(this).hide();
                                var statusMarker = $(this.parentNode).find(".sfStatusMarker");
                                statusMarker.html(finishedResource);
                                that._onProgressCompleted(dataId, statusMarker);
                            } else {
                                $(this).show();
                                var widthForValueBar = (Math.round(progressPerItem) / 100) * maxWidth;
                                progressValue.width(widthForValueBar);
                                $(this).find(".sfProgressPercentMarker").html("(" + progressPerItem + "% " + currentStepResource + ")");
                            }
                        }
                    }
                });

                this._callDelayedUpdateCurrentState();
            }
            else {
                this._onProgressCompleted(null);
            }
        }
    },

    _updateCurrentStateFailure: function (obj, result) {
        this._callDelayedUpdateCurrentState();
    },

    _callDelayedUpdateCurrentState: function () {
        var that = this;
        var timer = 500;
        if (this._refreshInterval > 0) {
            timer = this.get_refreshInterval();
        }

        setTimeout(function () { that._updateCurrentState() }, timer);
    },

    StartProgress: function () {
        this._callDelayedUpdateCurrentState();
    },

    add_progressStarted: function (delegate) {
        this.get_events().addHandler('progressStarted', delegate);
    },

    _onProgressStarted: function (item) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs(item);
        var handler = this.get_events().getHandler('progressStarted');
        if (handler) handler(this, eventArgs);
    },

    add_progressCompleted: function (delegate) {
        this.get_events().addHandler('progressCompleted', delegate);
    },

    _onProgressCompleted: function (item, statusMarker) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs(item, statusMarker);
        var handler = this.get_events().getHandler('progressCompleted');
        if (handler) handler(this, eventArgs);
    },

    get_refreshInterval: function () {
        return this._refreshInterval;
    },
    set_refreshInterval: function (value) {
        this._refreshInterval = value;
    },
    get_progressServiceUrl: function () {
        return this._progressServiceUrl;
    },
    set_progressServiceUrl: function (value) {
        this._progressServiceUrl = value;
    },
    get_currentStepResource: function () {
        return this._currentStepResource;
    },
    set_currentStepResource: function (value) {
        this._currentStepResource = value;
    },
    get_completedResource: function () {
        return this._completedResource;
    },
    set_completedResource: function () {
        this._completedResource = value;
    }
}

Telerik.Sitefinity.Web.UI.ProgressBinder.registerClass('Telerik.Sitefinity.Web.UI.ProgressBinder', Sys.UI.Control);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs = function (item, statusMarker) {
    this._item = item;
    this._statusMarker = statusMarker;
    Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs.callBaseMethod(this, 'dispose');
    },
    get_item: function () {
        return this._item;
    },
    get_statusMarker: function () {
        return this._statusMarker;
    }
};
Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ProgressBinder.ProgressEventArgs', Sys.EventArgs);