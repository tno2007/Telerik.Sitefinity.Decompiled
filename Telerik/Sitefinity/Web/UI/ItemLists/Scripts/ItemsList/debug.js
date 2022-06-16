﻿/// <reference name="MicrosoftAjax.js" />

Type.registerNamespace("Telerik.Sitefinity.Web.UI.ItemLists");

// ------------------------------------------------------------------------
// Class ItemsList
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.ItemsList = function (element) {
    /// <summary>Creates a new instance of ItemsList</summary>

    this._pager = null;
    this._pagerId = null;

    Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.callBaseMethod(this, 'initialize');

    },

    dispose: function () {
        // clean-up
        if (this._pager) {
            if (this._pageChangedDelegate) {
                this._pager.remove_pageChanged(this._pageChangedDelegate);
                this._pageChangedDelegate = null;
            }

        }


        Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.callBaseMethod(this, 'dispose');
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.callBaseMethod(this, "_handlePageLoad", [sender, args]);

        this._pager = this.getPager();
        if (this._pager) {
            this._pageChangedDelegate = Function.createDelegate(this, this._pageChanged);
            this._pager.add_pageChanged(this._pageChangedDelegate);

            this._handleBinderDataBindingDelegate = Function.createDelegate(this, this._handleBinderDataBinding);
            this._binder.add_onDataBinding(this._handleBinderDataBindingDelegate);
        }
    },

    _handleBinderDataBinding: function (sender, args) {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.callBaseMethod(this, "_handleBinderDataBinding", [sender, args]);
        if (args.get_dataItem) {
            var data = args.get_dataItem();
            if (data && data.TotalCount != null) {
                this._pager.init(data.TotalCount, this._binder.get_take());
            }
        }
    },

    _pageChanged: function (sender, args) {
        this._binder.set_skip(args.get_skip());
        //this._binder.set_take(args.get_take());
        this._binder.ReBind();
    },

    getPager: function () {
        if (this._pager == null) {
            this._pager = $find(this._pagerId);
        }
        return this._pager;
    },

    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */
    get_pagerId: function () {
        return this._pagerId;
    },
    set_pagerId: function (value) {
        this._pagerId = value;
        this._pager = null;
    }

}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsList.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.ItemsList', Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase);


