Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Security.Permissions");

var usersAndRolesDialog = null;

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog.initializeBase(this, [element]);

    this._usersAndRolesSelector = null;
    this._doneSelectingLink = null;
    this._cancelSelectingLink = null;
    this._backLink = null;
    this._headerLabel = null;

    this._doneSelectingDelegate = null;
    this._cancelSelectingDelegate = null;
}

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog.prototype = {

    // ------------------------------------- Initialization -------------------------------------

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog.callBaseMethod(this, "initialize");

        usersAndRolesDialog = this;

        //delegates
        this._doneSelectingDelegate = Function.createDelegate(this, this._doneSelecting);
        this._cancelSelectingDelegate = Function.createDelegate(this, this._cancelSelecting);
        this._onBackLinkClickDelegate = Function.createDelegate(this, this._onBackLinkClick);

        //ui events
        $addHandler(this._doneSelectingLink, "click", this._doneSelectingDelegate);
        $addHandler(this._cancelSelectingLink, "click", this._cancelSelectingDelegate);
        $addHandler(this._backLink, "click", this._onBackLinkClickDelegate);
    },

    dispose: function () {
        if (this._doneSelectingDelegate) {
            if (this._doneSelectingLink) {
                $removeHandler(this._doneSelectingLink, "click", this._doneSelectingDelegate);
            }
            delete this._doneSelectingDelegate;
        }

        if (this._onBackLinkClickDelegate) {
            if (this._backLink) {
                $removeHandler(this._backLink, "click", this._onBackLinkClickDelegate);
            }
            delete this._onBackLinkClickDelegate;
        }

        if (this._cancelSelectingDelegate) {
            if (this._cancelSelectingLink) {
                $removeHandler(this._cancelSelectingLink, "click", this._cancelSelectingDelegate);
            }
            delete this._cancelSelectingDelegate;
        }

        Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog.callBaseMethod(this, "dispose");
    },

    // ------------------------------------- Public methods -------------------------------------

    createDialog: function (selectedPrincipalsIds, headerLabel, doneBtnLabel, enableBackBtn) {
        this._usersAndRolesSelector.get_principalSelector().setSelectedKeys(selectedPrincipalsIds);

        this._setDialogHeader(headerLabel);
        this._setDoneBtnLabel(doneBtnLabel);

        this.resizeToContent();

        if (enableBackBtn) {
            $(this.get_backLink()).show();
        } else {
            $(this.get_backLink()).hide();
        }
    },

    // ------------------------------------- Private methods -------------------------------------

    _setDialogHeader: function (label) {
        if (!label) return;

        jQuery(this.get_headerLabel()).html(label);
    },

    _setDoneBtnLabel: function (label) {
        if (!label) return;

        jQuery(this.get_doneSelectingLink()).html(label);
    },

    // ------------------------------------- Event handlers -------------------------------------

    _doneSelecting: function (sender, args) {
        var isDirty = this._usersAndRolesSelector.get_isDirty();
        var selectedPrincipals = this._usersAndRolesSelector.getSelectedPrincipals();


        var argument = { SelectedPrincipals: selectedPrincipals, IsDirty: isDirty};
        this.close(argument);
    },

    _onBackLinkClick: function () {
        this.close({ command: "back" });
    },

    _cancelSelecting: function (sender, args) {
        this.close();
    },

    // ------------------------------------- Properties -------------------------------------

    get_usersAndRolesSelector: function () {
        return this._usersAndRolesSelector;
    },
    set_usersAndRolesSelector: function (value) {
        this._usersAndRolesSelector = value;
    },

    get_doneSelectingLink: function () {
        return this._doneSelectingLink;
    },
    set_doneSelectingLink: function (value) {
        this._doneSelectingLink = value;
    },

    get_cancelSelectingLink: function () {
        return this._cancelSelectingLink;
    },
    set_cancelSelectingLink: function (value) {
        this._cancelSelectingLink = value;
    },

    get_backLink: function () {
        return this._backLink;
    },
    set_backLink: function (value) {
        this._backLink = value;
    },

    get_headerLabel: function () {
        return this._headerLabel;
    },
    set_headerLabel: function (value) {
        this._headerLabel = value;
    },
};

Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog.registerClass('Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.SelectUsersAndRolesDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);