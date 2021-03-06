function OnMasterViewLoaded(sender, args) {
    var masterView = sender;
    masterView.get_binder().set_unescapeHtml(true);
    var extender = new Twitter.MasterViewExtension(masterView);
    extender.initialize();
    $("#registerNewTwitterAppBtn").click(function () { extender._itemsGrid.executeCommand("create", {}); return false; });
}

function printUsersContent(arr) {
    return $.map(arr, function (v) {
        return v.Key.Name
    }).join(", ");
}

Type.registerNamespace("Twitter");

Twitter.MasterViewExtension = function (masterView) {
    Twitter.MasterViewExtension.initializeBase(this);

    // Main components
    this._masterView = masterView;
    this._binder = null;

    this._itemsGrid = {};
    this._toolbar = {};

    // Event delegates
    this._masterCommandDelegate = null;
    this._itemCommandDelegate = null;
    this._dialogClosedDelegate = null;
    this._toolbarCommandDelegate = null;
    this._serviceSuccessDelegate = null;
    this._serviceFailureDelegate = null;
    this._associateSuccessDelegate = null;

    this._actionCommandPrefix = "sf_binderCommand_";

    this._clientManager = null;
    this._handleBinderDataBoundDelegate = null;
}

Twitter.MasterViewExtension.prototype = {
    initialize: function () {
        Twitter.MasterViewExtension.callBaseMethod(this, 'initialize');
        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        this._toolbarCommandDelegate = Function.createDelegate(this, this._toolbarCommandHandler);
        this._serviceSuccessDelegate = Function.createDelegate(this, this._serviceSuccess);
        this._serviceFailureDelegate = Function.createDelegate(this, this._serviceFailure);
        this._associateSuccessDelegate = Function.createDelegate(this, this._associateRedirect);
        this._handleBinderDataBoundDelegate = Function.createDelegate(this, this._handleBinderDataBound);
        this._itemsGrid = this._masterView.get_itemsGrid();
        this._toolbar = this._masterView.get_toolbar();

        this._itemsGrid._replacePropertyValues = function (dataItem, literal) {
            if (literal && dataItem) {
                var matches = literal.match(/{{[a-zA-Z0-9_\.]+}}/g);
                if (matches) {
                    var matchIndex = matches.length;
                    var current = null;
                    var propName = null;
                    var propValue = null;
                    var propArr = null;
                    while (matchIndex--) {
                        current = matches[matchIndex];
                        propName = current.slice(2, -2);
                        propArr = propName.split(".");
                        propValue = dataItem;
                        while (propArr.length > 0) {
                            propValue = propValue[propArr[0]];
                            propArr.shift();
                        }
                        literal = literal.replace(current, propValue);
                    }

                }
            }
            return literal;
        };

        var currentList = this._masterView.get_currentItemsList();
        var binder = currentList.getBinder();
        binder.DeleteItem = function (key) { alert(key); };
        binder.deleteItems = function (keys, callback, caller) {
            var clientManager = this.get_manager();
            var args = this._deletingHandler(keys);
            if (args.get_cancel() === false) {
                clientManager.deleteItems(this, keys, callback, caller);
            }
        };

        if (this._toolbar) {
            this._toolbar.add_command(this._toolbarCommandDelegate);
        }

        if (this._itemsGrid) {
            this._itemsGrid.add_itemCommand(this._itemCommandDelegate);
        }
        binder.add_onDataBound(this._handleBinderDataBoundDelegate);
    },

    dispose: function () {
        Twitter.MasterViewExtension.callBaseMethod(this, 'dispose');
        Twitter.MasterViewExtension.callBaseMethod(this, 'initialize');
        if (this._itemsGrid) {
            //            this._itemsGrid.remove_command(this._masterCommandDelegate);
            this._itemsGrid.remove_itemCommand(this._itemCommandDelegate);
            //            this._itemsGrid.remove_dialogClosed(this._dialogClosedDelegate);
        }

        //        delete this._masterCommandDelegate;
        delete this._itemCommandDelegate;
        //        delete this._dialogClosedDelegate;
        //        if (this._serviceSuccessDelegate) {
        //            delete this._serviceSuccessDelegate;
        //        }
        //        if (this._serviceFailureDelegate) {
        //            delete this._serviceFailureDelegate;
        //        }        
        var binder = this._masterView.get_currentItemsList().getBinder();
        binder.remove_onDataBound(this._handleBinderDataBoundDelegate);
        this._handleBinderDataBoundDelegate = null;
    },
    // handles the commands fired by a single item
    _itemCommandHandler: function (sender, args) {
        var dataPair = args.get_commandArgument();
        var appName = dataPair.key.Name;

        switch (args.get_commandName()) {
            case 'delete':
                args.set_cancel(true);
                if (confirm('Are you sure you want to delete this item?')) {
                    this._deleteApp(sender, appName, appName);
                }
                break;
            case 'associateUsr':
                this._associateUser(sender, appName, appName);
                break;
            default:
                break;
        }
    },

    _toolbarCommandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case 'associateUsr':
                //get the first
                var appName = this._itemsGrid.get_selectedItems()[0].key.Name;
                this._associateUser(this._itemsGrid, appName, appName);
                break;
            case 'getNewApiKey':
                window.open('http://dev.twitter.com/apps/new', 'Get A Twitter Api Key', 'width=800,height=600')
                break;
            default:
                break;
        }

    },

    _associateUser: function (itemsListBase, appName, context) {
        this._showGridLoadingPanel();
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'AssociateUser/';
        var clientManager = this._getClientManager();
        clientManager.InvokeGet(serviceUrl, { "appName": appName }, null, this._associateSuccessDelegate, this._serviceFailureDelegate, itemsListBase, null, null, context);
    },
    _deleteApp: function (itemsListBase, appName, context) {
        this._showGridLoadingPanel();
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'DeleteApp/';
        var clientManager = this._getClientManager();
        clientManager.InvokeDelete(serviceUrl, { "appName": appName }, null, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsListBase, null, null, context);
    },

    _serviceSuccess: function (caller, sender, args) {
        this._hideGridLoadingPanel();
        this._itemsGrid.dataBind();
    },

    _associateRedirect: function (caller, sender, args) {
        var newStr = unescape(sender).replace(/"/g, '');
        this._hideGridLoadingPanel();
        window.location = newStr;
    },

    _batchServiceSuccess: function (caller, sender, args) {
        var masterView = this.Context.MasterView;

        masterView._movedItemsCount++;
        if (masterView._movedItemsCount == masterView._itemsToMoveCount) {
            var key = this.Context.DataKey;
            this.Caller.dataBind(null, key);
            masterView._movedItemsCount = 0;
        }
    },

    _serviceFailure: function (sender, args) {
        this._hideGridLoadingPanel();
        alert(sender.Detail);
    },

    //TODO change the way the loading panel is constructed in the itemsGrid
    _hideGridLoadingPanel: function () {
        $('.RadAjax').hide();
    },

    _showGridLoadingPanel: function () {
        $('.RadAjax').show();
    },

    _getClientManager: function () {
        if (!this._clientManager) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }

        return this._clientManager;
    },

    _getBaseUrl: function (itemsListBase) {
        var baseUrl = itemsListBase.get_serviceBaseUrl();
        var serviceUrl = baseUrl.toString().substr(0, baseUrl.toString().indexOf('?'));
        return serviceUrl;
    },
    // undo the adding of "sfEmpty" class to the body in MasterGridView._handleBinderDataBound()
    _handleBinderDataBound: function (sender, args) {
        if (sender.get_hasNoData()) {
            //$("body").removeClass("sfEmpty");
            jQuery(".sfTwitterSettingsWrp [id~='registerNewTwitterAppBtn']").parents(".sfButtonArea").show();
        }
    }
    /* -------------------- properties ---------------- */

}

Twitter.MasterViewExtension.registerClass('Twitter.MasterViewExtension', Sys.Component);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();