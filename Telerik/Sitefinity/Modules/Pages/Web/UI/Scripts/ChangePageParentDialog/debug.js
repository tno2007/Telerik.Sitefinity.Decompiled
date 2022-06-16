Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog.initializeBase(this, [element]);
    this._pageSelector = null;
    this._doneSelectingDelegate = null;
    this._webServiceBaseUrl = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog.prototype = {

    // set up and tear down
    initialize: function () {

        if (this._doneSelectingDelegate == null) {
            this._doneSelectingDelegate = Function.createDelegate(this, this._pageSelector_doneSelecting);
        }
        this._pageSelector.add_doneClientSelection(this._doneSelectingDelegate);

        Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog.callBaseMethod(this, 'initialize');
    },

    dispose: function () {

        if (this._doneSelectingDelegate != null) {
            this._pageSelector.remove_doneClientSelection(this._doneSelectingDelegate);
            delete this._doneSelectingDelegate;
        }

        Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog.callBaseMethod(this, 'dispose');
    },

    // event handlers
    _pageSelector_doneSelecting: function (newParentPage) {
        if (newParentPage == null) {
            dialogBase.close();
        } else {
            this._changePageParent(dialogPageId, newParentPage.Id);
        }
    },

    // private methods
    _changePageParent: function (pageId, newParentPageId) {
        var serviceUrl = this._webServiceBaseUrl;
        serviceUrl += 'ChangeParent/';
        var keys = [newParentPageId];
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        clientManager.InvokePut(serviceUrl, null, keys, dialogPageId, this._serviceSuccess, this._serviceFailure, this);
    },

    _serviceSuccess: function (caller, sender, args) {
        dialogBase.closeAndRebind();
    },

    _serviceFailure: function (sender, args) {
        alert('Problem!');
    },

    // properties
    get_pageSelector: function () {
        return this._pageSelector;
    },
    set_pageSelector: function (value) {
        this._pageSelector = value;
    }
}

Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.ChangePageParentDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);