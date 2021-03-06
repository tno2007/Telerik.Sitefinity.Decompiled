Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView.initializeBase(this, [element]);

    this._openSendTestEmailDialogBtn = null;
    this._emailSettingsSendTestEmailDialog = null;

    this._openSendTestMailDialogDelegate = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView.prototype =
    {
        /* --------------------  set up and tear down ----------- */
        initialize: function () {
            Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView.callBaseMethod(this, "initialize");

            this._openSendTestMailDialogDelegate = Function.createDelegate(this, this._openSendTestMailDialogHandler);
            $addHandler(this.get_openSendTestEmailDialogBtn(), "click", this._openSendTestMailDialogDelegate);

            this._positionOpenSendTestMailDialogBtn();
        },
        dispose: function () {

            if (this._openSendTestMailDialogDelegate) {
                $removeHandler(this.get_openSendTestEmailDialogBtn(), "click", this._openSendTestMailDialogDelegate);
                delete this._openSendTestMailDialogDelegate;
            }
        },
        _positionOpenSendTestMailDialogBtn: function () {
            // Hack in order to position the send test button
            // next to the save button
            var parent = this.get_parent();
            if (parent) {
                var buttonsArea = parent.get_buttonsArea();
                if (buttonsArea) {
                    buttonsArea.append(this.get_openSendTestEmailDialogBtn());
                }
            }
        },
        _openSendTestMailDialogHandler: function () {
            var offsetTop = this._currentScrollPosition() + 80;
            this.get_emailSettingsSendTestEmailDialog().open({offsetTop: offsetTop});
        },
        _currentScrollPosition: function () {
            if (window && window.pageYOffset) {
                return window.pageYOffset;
            }
            if (document && document.documentElement && document.documentElement.scrollTop) {
                return document.documentElement.scrollTop;
            }
            if (document && document.body && document.body.scrollTop) {
                return document.body.scrollTop;
            }

            return 0;
        },
        get_openSendTestEmailDialogBtn: function () {
            return this._openSendTestEmailDialogBtn;
        },
        set_openSendTestEmailDialogBtn: function (value) {
            this._openSendTestEmailDialogBtn = value;
        },
        get_emailSettingsSendTestEmailDialog: function () {
            return this._emailSettingsSendTestEmailDialog;
        },
        set_emailSettingsSendTestEmailDialog: function (value) {
            this._emailSettingsSendTestEmailDialog = value;
        },
    }

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView", Sys.UI.Control);