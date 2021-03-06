// called by the MasterGridView when it is loaded
function OnModuleMasterViewLoaded(sender, args) {
    var masterView = sender;
    var extender = new Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions(masterView);
    extender.initialize();
}

Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script");

Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions = function (masterView) {
    Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.initializeBase(this);

    // Main components
    this._masterView = masterView;
    this._binder = null;

    this._itemsTreeTable = {};

    this._masterCommandDelegate = null;
    this._itemCommandDelegate = null;
}

Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.SelectionType = { None: 0, Single: 1, Multiple: 2 };
Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.prototype = {
    initialize: function () {
        Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.callBaseMethod(this, 'initialize');

        this._itemsTreeTable = this._masterView.get_itemsTreeTable();

        this._masterBeforeCommandDelegate = Function.createDelegate(this, this._masterBeforeCommandHandler);
        this._itemsTreeTable.add_beforeCommand(this._masterBeforeCommandDelegate);

        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        this._itemsTreeTable.add_itemCommand(this._itemCommandDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.callBaseMethod(this, 'dispose');

        this._itemsTreeTable.remove_command(this._masterBeforeCommandDelegate);
        this._itemsTreeTable.remove_itemCommand(this._itemCommandDelegate);

        delete this._itemCommandDelegate;
        delete this._masterBeforeCommandDelegate;
    },

    _masterBeforeCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        var binder = this._masterView.get_currentItemsList().getBinder();
        var selectedItems = binder.get_selectedItems();
        var selectedItem = null;

        if (commandName == "groupDelete") {
            for (var i = 0; i < selectedItems.length; i++) {
                selectedItem = selectedItems[i];
                this._processDelete(selectedItem, args);
            }
        }
    },

    _itemCommandHandler: function (sender, args) {
        var item = args.get_commandArgument();

        if (args.get_commandName() == 'delete' && args.get_cancel() == false) {
            this._processDelete(item, args);
        }
    },

    _processDelete: function (item, args) {
        if (item.HasChildren) {
            args.set_cancel(true);
            this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteParentItemDialog");
            return;
        }
    }
}

Telerik.Sitefinity.DynamicModules.Web.UI.Backend.Script.DynamicContentsMasterGridViewExtensions.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.DynamicContentsMasterGridViewExtensions', Sys.Component);


