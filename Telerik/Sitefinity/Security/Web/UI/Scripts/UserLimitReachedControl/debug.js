﻿Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI");

Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl = function (element) {
    Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl.initializeBase(this, [element]);

    this._forceSomeoneToLogoutButton = null;
    this._forceSomeoneToLogoutButtonDelegate = null;
    this._forceLogoffContainer = null;
    this._userListContainer = null;
    this._cancelLogoffButton = null;
    this._cancelLogoffButtonDelegate = null;
}

Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl.callBaseMethod(this, "initialize");

        if (this.get_forceSomeoneToLogoutButton()) {
            this._forceSomeoneToLogoutButtonDelegate = Function.createDelegate(this, this._forceSomeoneToLogoutButtonClick);
            $addHandler(this.get_forceSomeoneToLogoutButton(), "click", this._forceSomeoneToLogoutButtonDelegate);
        }
        if (this.get_cancelLogoffButton()) {
            this._cancelLogoffButtonDelegate = Function.createDelegate(this, this._cancelLogoffButtonClick);
            $addHandler(this.get_cancelLogoffButton(), "click", this._cancelLogoffButtonDelegate);
        }
    },
    dispose: function () {
        if (this.get_forceSomeoneToLogoutButton())
            $removeHandler(this.get_forceSomeoneToLogoutButton(), "click", this._forceSomeoneToLogoutButtonDelegate);
        if (this._forceSomeoneToLogoutButtonDelegate)
            delete this._forceSomeoneToLogoutButtonDelegate;
        if (this.get_cancelLogoffButton())
            $removeHandler(this.get_cancelLogoffButton(), "click", this._cancelLogoffButtonDelegate);
        if (this._cancelLogoffButtonDelegate)
            delete this._cancelLogoffButtonDelegate;

        Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl.callBaseMethod(this, "dispose");
    },

    _forceSomeoneToLogoutButtonClick: function () {
        this.get_userListContainer().style.display = "none";
        this.get_forceLogoffContainer().style.display = "";
    },
    _cancelLogoffButtonClick: function () {
        this.get_forceLogoffContainer().style.display = "none";
        this.get_userListContainer().style.display = "";
    },

    get_forceSomeoneToLogoutButton: function () {
        return this._forceSomeoneToLogoutButton;
    },
    set_forceSomeoneToLogoutButton: function (value) {
        this._forceSomeoneToLogoutButton = value;
    },
    get_forceLogoffContainer: function () {
        return this._forceLogoffContainer;
    },
    set_forceLogoffContainer: function (value) {
        this._forceLogoffContainer = value;
    },
    get_userListContainer: function () {
        return this._userListContainer;
    },
    set_userListContainer: function (value) {
        this._userListContainer = value;
    },
    get_cancelLogoffButton: function () {
        return this._cancelLogoffButton;
    },
    set_cancelLogoffButton: function (value) {
        this._cancelLogoffButton = value;
    }
}
Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl.registerClass("Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl", Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();