Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView.initializeBase(this, [element]);

    this._trackBouncedMessagesField = null;
    this._performPop3TestBtn = null;
    this._serviceUrl = null;
    this._pop3HostField = null;
    this._pop3PortField = null;
    this._pop3Username = null;
    this._pop3Password = null;
    this._pop3UseSmtpSSLField = null;

    this._trackBouncedChangedDelegate = null;
    this._performPop3TestDelegate = null;
}

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView.prototype =
    {
        /* --------------------  set up and tear down ----------- */
        initialize: function () {
            Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView.callBaseMethod(this, "initialize");

            this._trackBouncedChangedDelegate = Function.createDelegate(this, this._trackBouncedChanged);
            this.get_trackBouncedMessagesField().add_valueChanged(this._trackBouncedChangedDelegate);

            this._performPop3TestDelegate = Function.createDelegate(this, this._performPop3TestHandler);
            $addHandler(this.get_performPop3TestBtn(), "click", this._performPop3TestDelegate);
        },
        dispose: function () {
            if (this._trackBouncedChangedDelegate) {
                this.get_trackBouncedMessagesField().remove_valueChanged(this._trackBouncedChangedDelegate);
                delete this._trackBouncedChangedDelegate;
            }

            if (this._performPop3TestDelegate) {
                $removeHandler(this.get_performPop3TestBtn(), "click", this._performPop3TestDelegate);
                delete this._performPop3TestDelegate;
            }
        },
        _trackBouncedChanged: function () {
            if (this.get_trackBouncedMessagesField().get_value() == 'true') {
                $('.sfTrackBounce').show();
            } else {
                $('.sfTrackBounce').hide();
            }
        },
        _performPop3TestHandler: function () {
            var that = this;
            jQuery.ajax({
                type: "POST",
                url: this.get_serviceUrl(),
                data: JSON.stringify({
                    'NotificationsSmtpProfile': 'Default',
                    'Host': that.get_pop3HostField().get_value(),
                    'Port': that.get_pop3PortField().get_value(),
                    'Username': that.get_pop3Username().get_value(),
                    'Password': that.get_pop3Password().get_value(),
                    'UseSSL': that.get_pop3UseSmtpSSLField().get_value()
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            })
                .done(function (data) {
                    that._pop3TestSuccess(data);
                })
                .fail(function (err) {
                    that._pop3TestFailure(err);
                });
        },
        _pop3TestSuccess: function (data) {
            if (basicSettingsView && data) {
                basicSettingsView.get_message().showPositiveMessage(data);
                basicSettingsView._scrollTop();
            }
        },
        _pop3TestFailure: function (err) {
            if (basicSettingsView && err && err.responseJSON && err.responseJSON.Detail) {
                basicSettingsView.get_message().showNegativeMessage(err.responseJSON.Detail);
                basicSettingsView._scrollTop();
            }
        },
        get_trackBouncedMessagesField: function (err) {
            return this._trackBouncedMessagesField;
        },
        set_trackBouncedMessagesField: function (value) {
            this._trackBouncedMessagesField = value;
        },
        get_performPop3TestBtn: function () {
            return this._performPop3TestBtn;
        },
        set_performPop3TestBtn: function (value) {
            this._performPop3TestBtn = value;
        },
        get_serviceUrl: function () {
            return this._serviceUrl;
        },
        set_serviceUrl: function (value) {
            this._serviceUrl = value;
        },
        get_pop3HostField: function () {
            return this._pop3HostField;
        },
        set_pop3HostField: function (value) {
            this._pop3HostField = value;
        },
        get_pop3PortField: function () {
            return this._pop3PortField;
        },
        set_pop3PortField: function (value) {
            this._pop3PortField = value;
        },
        get_pop3Username: function () {
            return this._pop3Username;
        },
        set_pop3Username: function (value) {
            this._pop3Username = value;
        },
        get_pop3Password: function () {
            return this._pop3Password;
        },
        set_pop3Password: function (value) {
            this._pop3Password = value;
        },
        get_pop3UseSmtpSSLField: function () {
            return this._pop3UseSmtpSSLField;
        },
        set_pop3UseSmtpSSLField: function (value) {
            this._pop3UseSmtpSSLField = value;
        },
    }

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView", Sys.UI.Control);