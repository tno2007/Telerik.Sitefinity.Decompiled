/// <reference name="MicrosoftAjax.js" />

Type.registerNamespace("Telerik.Sitefinity.Web.UI.ItemLists");

// ------------------------------------------------------------------------
// Class ItemsGrid
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid = function (element) {
    /// <summary>Creates a new instance of ItemsGrid</summary>
    Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid.initializeBase(this, [element]);
    this._grid = null;
    this._gridId = null;

    this._rowSelectedDelegate = null;
}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid.callBaseMethod(this, 'initialize');

        this._rowSelectedDelegate = Function.createDelegate(this, this._rowSelectedHandler);
    },

    dispose: function () {
        // clean-up
        if (this._rowSelectedDelegate) {
            delete this._rowSelectedDelegate;
        }
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid.callBaseMethod(this, 'dispose');
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid.callBaseMethod(this, "_handlePageLoad");

        var grid = this.get_grid();

        grid.add_rowSelected(this._rowSelectedDelegate);
        grid.add_rowDeselected(this._rowSelectedDelegate);
    },

    /* -------------------- private methods ----------- */

    _rowSelectedHandler: function (sender, args) {
        this._raiseSelectionChanged(this.getBinder().get_selectedItems());
    },

    /* -------------------- properties ---------------- */

    // Gets the the grid   
    get_grid: function () {
        if (this._grid == null) {
            this._grid = $find(this._gridId);
        }
        return this._grid;
    }
}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.ItemsGrid', Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase);


