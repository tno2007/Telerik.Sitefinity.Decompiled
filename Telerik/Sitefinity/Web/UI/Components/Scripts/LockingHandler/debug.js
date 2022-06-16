﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Components");

/* LockingHandler class */

Telerik.Sitefinity.Web.UI.Components.LockingHandler = function (element) {
	Telerik.Sitefinity.Web.UI.Components.LockingHandler.initializeBase(this, [element]);

	this._lockingDialog = null;

	this._dialogUrl = null;

	this._showUnlockButton = null;
	this._showViewButton = null;
	this._showCloseButton = null;
	this._title = null;
	this._viewUrl = null;
	this._closeUrl = null;
	this._unlockUrl = null;
	this._lockedBy = null;

	//Delegates
	this._lockingDialogClosedDelegate = null;
}
Telerik.Sitefinity.Web.UI.Components.LockingHandler.prototype = {

	// set up 
	initialize: function () {
		Telerik.Sitefinity.Web.UI.Components.LockingHandler.callBaseMethod(this, "initialize");


		this._lockingDialogClosedDelegate = Function.createDelegate(this, this._lockingDialogClosed);
		this.get_lockingDialog().add_close(this._lockingDialogClosedDelegate);

	},

	// tear down
	dispose: function () {
		Telerik.Sitefinity.Web.UI.Components.LockingHandler.callBaseMethod(this, "dispose");

		if (this._lockingDialogClosedDelegate) {
			this.get_lockingDialog().remove_close(this._lockingDialogClosedDelegate);
			delete this._lockingDialogClosedDelegate;
		}
	},

	/* ----------------------- public methods ----------------------- */

	showLockingDialog: function (settings) {
		//Build URL
		var url = this.get_dialogUrl();
		if (settings) {
			if (settings.Title) url = Telerik.Sitefinity.setUrlParameter(url, "Title", settings.Title);
			if (settings.LockedBy) url = Telerik.Sitefinity.setUrlParameter(url, "LockedBy", settings.LockedBy);
			if (settings.ViewUrl) url = Telerik.Sitefinity.setUrlParameter(url, "ViewUrl", settings.ViewUrl);
			if (settings.CloseUrl) url = Telerik.Sitefinity.setUrlParameter(url, "CloseUrl", settings.CloseUrl);
			if (settings.UnlockUrl) url = Telerik.Sitefinity.setUrlParameter(url, "UnlockUrl", settings.UnlockUrl);
			if (settings.ShowUnlock) url = Telerik.Sitefinity.setUrlParameter(url, "ShowUnlock", (settings.ShowUnlock ? "true" : "false"));
			if (settings.ShowView) url = Telerik.Sitefinity.setUrlParameter(url, "ShowView", (settings.ShowView ? "true" : "false"));
			if (settings.ShowClose) url = Telerik.Sitefinity.setUrlParameter(url, "ShowClose", (settings.ShowClose ? "true" : "false"));
		}

		var dialogWindow = this.get_lockingDialog();
		dialogWindow.SetUrl(url);
		dialogWindow.show();
		dialogWindow.maximize();
	},

	//This is only to get an idea of what settings are available. You must further adjust some of the properties of the result object to your needs
	getDefaultSettings: function () {
		return this.getSettings(null, null, null, null, null, true, true, true);
	},

	getCurrentSettings: function () {
		return this.getSettings(
			this.get_title(),
			this.get_lockedBy(),
			this.get_viewUrl(),
			this.get_unlockUrl(),
			this.get_closeUrl(),
			this.get_showUnlockButton(),
			this.get_showViewButton(),
			this.get_showCloseButton()
		);
	},

	//Returns a settings object to use with the showLockingDialog method.
	getSettings: function (title, lockedByGuid, viewUrl, unlockUrl, closeUrl, showUnlock, showView, showClose) {
		return {
			Title: title,
			LockedBy: lockedByGuid,
			ViewUrl: viewUrl,
			UnlockUrl: unlockUrl,
			CloseUrl: closeUrl,
			ShowUnlock: showUnlock,
			ShowView: showView,
			ShowClose: showClose
		};
	},

	tryHandleError: function (error) {

		if (error && error.ItemState && error.LockedBy && error.Operation) {
			//Ready = 0,
			//Locked = 1,
			//Deleted = 2
			var settings = this.getCurrentSettings();
			switch (error.ItemState) {
				case 1:
					settings.LockedBy = error.LockedBy;
					break;
				case 2:
					settings.ShowUnlock = false;
					break;
				default:
					return false;
					break;
			}
			this.showLockingDialog(settings);
			return true;
		}
		
		return false;
	},

	/* ----------------------- events ----------------------- */

	add_commandSelected: function (handler) {
		this.get_events().addHandler('commandSelected', handler);
	},
	remove_commandSelected: function (handler) {
		this.get_events().removeHandler('commandSelected', handler);
	},
	_raiseCommandSelected: function (args) {
		var handler = this.get_events().getHandler('commandSelected');
		if (handler) {
			handler(this, args);
		}
	},

	/* ----------------------- event handlers ----------------------- */

	_lockingDialogClosed: function (sender, args) {
		var arg = args.get_argument();
		var action = arg.Action;
		var newArg = { Action: action, Handled: false };
		this._raiseCommandSelected(newArg);

		//Stop handling if Handled property of the argument is true
		if (newArg.Handled == false) {
			switch (newArg.Action) {
				case "close":
					document.location.href = this.get_closeUrl();
					break;
				case "view":
					window.open(this.get_unlockUrl());
					break;
				case "unlock":
					document.location.href = this.get_closeUrl();
					break;
			}
		}
	},

	/* ----------------------- properties ----------------------- */

	get_title: function () {
		return this._title;
	},
	set_title: function (value) {
		this._title = value;
	},
	get_viewUrl: function () {
		return this._viewUrl;
	},
	set_viewUrl: function (value) {
		this._viewUrl = value;
	},
	get_closeUrl: function () {
		return this._closeUrl;
	},
	set_closeUrl: function (value) {
		this._closeUrl = value;
	},
	get_unlockUrl: function () {
		return this._unlockUrl;
	},
	set_unlockUrl: function (value) {
		this._unlockUrl = value;
	},
	get_lockedBy: function () {
		return this._lockedBy;
	},
	set_lockedBy: function (value) {
		this._lockedBy = value;
	},
	get_showCloseButton: function () {
		return this._showCloseButton;
	},
	set_showCloseButton: function (value) {
		this._showCloseButton = value;
	},
	get_showUnlockButton: function () {
		return this._showUnlockButton;
	},
	set_showUnlockButton: function (value) {
		this._showUnlockButton = value;
	},
	get_showViewButton: function () {
		return this._showViewButton;
	},
	set_showViewButton: function (value) {
		this._showViewButton = value;
	},


	get_lockingDialog: function () {
		return this._lockingDialog;
	},
	set_lockingDialog: function (value) {
		if (this._lockingDialog != value) {
			this._lockingDialog = value;
			this.raisePropertyChanged("lockingDialog");
		}
	},

	get_dialogUrl: function () {
		return this._dialogUrl;
	},
	set_dialogUrl: function (value) {
		if (this._dialogUrl != value) {
			this._dialogUrl = value;
			this.raisePropertyChanged('dialogUrl');
		}
	}
};

Telerik.Sitefinity.Web.UI.Components.LockingHandler.registerClass('Telerik.Sitefinity.Web.UI.Components.LockingHandler', Sys.UI.Control);