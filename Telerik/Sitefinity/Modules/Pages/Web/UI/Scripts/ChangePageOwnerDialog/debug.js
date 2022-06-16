Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog.initializeBase(this, [element]);
    this._doneSelectingButton = null;
    this._cancelButton = null;
    this._userSelector = null;
    this._pageTitlePlaceHolder = null;
    this._webServiceBaseUrl = null;
    this._selectionDoneDelegate = null;
    this._cancelDelegate = null;
    this._changeOwnerSuccessDelegate = null;
    this._changeOwnerFailureDelegate = null;
    this._itemSelectedDelegate = null;
    this._searchDelegate = null;

    this._pageDataItem = null;
    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog.prototype = {

    // set up and tear down
    initialize: function () {
        this._selectionDoneDelegate = Function.createDelegate(this, this._selectionDone);
        this._cancelDelegate = Function.createDelegate(this, this._cancel);
        this._itemSelectedDelegate = Function.createDelegate(this, this._itemSelected);
        this._searchDelegate = Function.createDelegate(this, this._search);

        this._changeOwnerSuccessDelegate = Function.createDelegate(this, this._changeOwnerServiceSuccess);
        this._changeOwnerFailureDelegate = Function.createDelegate(this, this._changeOwnerServiceFailure);

        $addHandler(this._doneSelectingButton, 'click', this._selectionDoneDelegate);
        $addHandler(this._cancelButton, 'click', this._cancelDelegate);
        this._userSelector.add_itemSelected(this._itemSelectedDelegate);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        if (this._selectionDoneDelegate) {
            delete this._selectionDoneDelegate;
        }
        if (this._cancelDelegate) {
            delete this._cancelDelegate;
        }
        if (this._changeOwnerSuccessDelegate) {
            delete this._changeOwnerSuccessDelegate;
        }
        if (this._changeOwnerFailureDelegate) {
            delete this._changeOwnerFailureDelegate;
        }
        if (this._itemSelectedDelegate) {
            delete this._itemSelectedDelegate;
        }
        if (this._searchDelegate) {
            delete this._searchDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog.callBaseMethod(this, 'dispose');
    },

    /* ------------------ Events --------------*/
    _onLoad: function () {
        dialogBase.resizeToContent();
    },

    // public methods

    setUp: function (pageDataItem) {
        this._userSelector._getSearchBox().get_searchBox().add_search(this._searchDelegate);
        jQuery(this._doneSelectingButton).addClass("sfDisabledLinkBtn");

        if (pageDataItem) {
            this._pageDataItem = pageDataItem;
            this.setTitle(this._pageDataItem.Title);
        }
        else {
            this._pageDataItem = null;
        }
    },

    setTitle: function (title) {
        if (title.hasOwnProperty('Value')) {
            jQuery(this._pageTitlePlaceHolder).text(title.Value);
        } else {
            jQuery(this._pageTitlePlaceHolder).text(title);
        }

        dialogBase.resizeToContent();
    },

    // event handlers

    _cancel: function () {
        dialogBase.close();
        this._userSelector.cleanUp();
    },

    _selectionDone: function () {
        var userId = this._userSelector.get_selectedKeys()[0];
        if (userId) {
            if (this._pageDataItem) {
                var serviceUrl = this._webServiceBaseUrl;
                serviceUrl += 'ChangeOwner/';
                var keys = [userId];
                var clientManager = new Telerik.Sitefinity.Data.ClientManager();
                clientManager.InvokePut(serviceUrl, null, keys, this._pageDataItem.Id, this._changeOwnerSuccessDelegate, this._changeOwnerFailureDelegate, this);
            }
            else {
                dialogBase.get_radWindow().close(userId); ;
                this._userSelector.cleanUp();
            }
        }
    },

    _changeOwnerServiceSuccess: function (caller, sender, args) {
        dialogBase.closeAndRebind();
        this._userSelector.cleanUp();
    },

    _changeOwnerServiceFailure: function (sender, args) {
        alert(sender.Detail);
        this._userSelector.cleanUp();
    },

    _itemSelected: function (sender, args) {
        jQuery(this._doneSelectingButton).removeClass("sfDisabledLinkBtn");
    },

    _search: function (sender, args) {
        jQuery(this._doneSelectingButton).addClass("sfDisabledLinkBtn");
    },

    // private methods

    // properties

    get_userSelector: function () {
        return this._userSelector;
    },
    set_userSelector: function (value) {
        this._userSelector = value;
    },

    get_doneSelectingButton: function () {
        return this._doneSelectingButton;
    },
    set_doneSelectingButton: function (value) {
        this._doneSelectingButton = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },

    get_pageTitlePlaceHolder: function () {
        return this._pageTitlePlaceHolder;
    },
    set_pageTitlePlaceHolder: function (value) {
        this._pageTitlePlaceHolder = value;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageOwnerDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);
