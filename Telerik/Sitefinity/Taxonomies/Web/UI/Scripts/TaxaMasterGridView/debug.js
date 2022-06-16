Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI");
Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView = function (element) {
    Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView.initializeBase(this, [element]);

    this._handleTaxonDialogClosed = null;
}

Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView.callBaseMethod(this, "initialize");

        this._handleTaxonDialogClosed = Function.createDelegate(this, this._dialogClosed);

        if(this._itemsTreeTable) {
            this._itemsTreeTable.add_dialogClosed(this._handleTaxonDialogClosed);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView.callBaseMethod(this, 'dispose');

        if (this._itemsTreeTable) {
            this._itemsTreeTable.remove_dialogClosed(this._handleTaxonDialogClosed);
        }

        delete this._handleTaxonDialogClosed;
    },

    _dialogClosed: function (sender, args) {
        if (args && !args._isCreated && args._isUpdated &&
            args._dataItem && args._context && args._context._dataItem &&
            this._isParentChanged(args._context._dataItem, args._dataItem) &&
            args._context._binder && this._itemsTreeTable && this._itemsTreeTable._binder &&
            args._context._binder._uiCulture === this._itemsTreeTable._binder._uiCulture) {

            //Update only currently edited node
            this._itemsTreeTable._binder.UpdateItems({ Items: [args._dataItem] }, true);
            args._cancel = true;
        }
    },

    _isParentChanged: function (originalItem, modifiedItem) {
        var originalParentId = originalItem.ParentTaxonId || Telerik.Sitefinity.getEmptyGuid(),
            modifiedParentId = modifiedItem.ParentTaxonId || Telerik.Sitefinity.getEmptyGuid();

        return originalParentId === modifiedParentId;
    }
}

Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView.registerClass('Telerik.Sitefinity.Taxonomies.Web.UI.TaxaMasterGridView', Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView);
