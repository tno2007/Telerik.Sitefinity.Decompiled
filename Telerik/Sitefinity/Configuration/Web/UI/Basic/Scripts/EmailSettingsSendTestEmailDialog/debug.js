Type.registerNamespace("Telerik.Sitefinity.Configuration.Web.UI.Basic");

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog = function (element) {
    Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog.initializeBase(this, [element]);

    this._window = null;
    this._windowBody = null;

    this._cancelButton = null;
    this._closeButton = null;
    this._closeButton2 = null;
    this._sendTestButton = null;
    this._emailAddress = null;
    this._serviceUrl = null;
    this._required = null;

    this._successCallback = null;
    this._failureCallback = null;

    this._cancelButtonClickDelegate = null;
    this._sendTestButtonClickDelegate = null;

    this._dataItem = null;
};

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog.prototype =
    {
        initialize: function () {
            Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog.callBaseMethod(this, "initialize");

            jQuery(document).ready(this.onReady.bind(this));

            this._sendTestButtonClickDelegate = Function.createDelegate(this, this._sendTestButtonClickHandler);
            $addHandler(this.get_sendTestButton(), "click", this._sendTestButtonClickDelegate);

            this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClickHandler);
            $addHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
            $addHandler(this.get_closeButton(), "click", this._cancelButtonClickDelegate);
            $addHandler(this.get_closeButton2(), "click", this._cancelButtonClickDelegate);
        },

        dispose: function () {
            Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog.callBaseMethod(this, "dispose");

            if (this._cancelButtonClickDelegate) {
                if (this.get_cancelButton()) {
                    $removeHandler(this.get_cancelButton(), "click", this._cancelButtonClickDelegate);
                }

                if (this.get_closeButton()) {
                    $removeHandler(this.get_closeButton(), "click", this._cancelButtonClickDelegate);
                }

                if (this.get_closeButton2()) {
                    $removeHandler(this.get_closeButton(), "click", this._cancelButtonClickDelegate);
                }

                delete this._cancelButtonClickDelegate;
            }

            if (this._sendTestButtonClickDelegate) {
                if (this.get_sendTestButton()) {
                    $removeHandler(this.get_sendTestButton(), "click", this._sendTestButtonClickDelegate);
                }
                delete this._sendTestButtonClickDelegate;
            }
        },

        onReady: function () {
            this._window = jQuery(this.get_windowBody()).kendoWindow({
                title: false,
                visible: false,
                animation: false,
                actions: [],
                modal: true,
                width: 510
            }).data("kendoWindow");

            jQuery(this.get_windowBody()).kendoValidator(
                {
                    errorTemplate: '<span class="sfError">#=message#</span>'
                });
        },
        _cancelButtonClickHandler: function () {
            this._window.close();
        },
        _sendTestButtonClickHandler: function () {
            if (!this._validate()) {
                return;
            }
            var jWindow = jQuery(this.get_windowBody());
            if (!jWindow.getKendoValidator().validate())
                return;

            var data = {
                'ReceiverEmailAddress': this.get_emailAddress().get_value(),
                'SenderEmailAddress' : this._dataItem.SenderEmailAddress,
                'SenderName' : this._dataItem.SenderName,
                'Subject' : this._dataItem.Subject,
                'BodyHtml' : this._dataItem.BodyHtml,
                'ModuleName' : this._dataItem.ModuleName,
                'PlaceholderFields' : this._dataItem.PlaceholderFields || [],
                'DynamicPlaceholderFields' : this._dataItem.DynamicPlaceholderFields || []
            };

            var that = this;
            jQuery.ajax({
                type: "POST",
                url: this.get_serviceUrl(),
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            })
                .done(function (data) {
                    if(that._successCallback) {
                        that._successCallback();
                    } else {
                        that._sendTestEmailSuccess(data);
                    }
                })
                .fail(function (err) {
                    if(that._failureCallback) {
                        that._failureCallback();
                    } else {
                        that._sendTestEmailFailure(err);
                    }
                })
                .always(function () {
                    that._window.close();
                });
        },
        open: function (dataItem, successCallback, failureCallback) {
            this._dataItem = dataItem || {};

            this.get_emailAddress()._clearTextBox();

            var offsetTop = null;

            if (this._dataItem.SenderVerified === true) {
                $(".sfSenderVerifiedSection").removeClass("sfDisplayNone");
                $(".sfSenderNotVerifiedSection").addClass("sfDisplayNone");
            } else if (this._dataItem.SenderVerified === false) {
                $(".sfSenderVerifiedSection").addClass("sfDisplayNone");
                $(".sfSenderNotVerifiedSection").removeClass("sfDisplayNone");
            }

            if (this._dataItem.offsetTop) {
                offsetTop = this._dataItem.offsetTop;
            } else {
                offsetTop = this._element.offsetTop;
            }

            this._successCallback = successCallback;
            this._failureCallback = failureCallback;
            this._window.center().open();
            this._window.setOptions({ position: { top: offsetTop } });
            var jWindow = jQuery(this.get_windowBody());
            jWindow.getKendoValidator().hideMessages();
        },

        initializeControls: function (data) {

        },
        _sendTestEmailSuccess: function (data) {
            if (basicSettingsView && data) {
                basicSettingsView.get_message().showPositiveMessage(data);
                basicSettingsView._scrollTop();
            }
        },
        _sendTestEmailFailure: function (err) {
            if (basicSettingsView && err && err.responseJSON && err.responseJSON.Detail) {
                basicSettingsView.get_message().showNegativeMessage(err.responseJSON.Detail);
                basicSettingsView._scrollTop();
            }
        },
        _validate: function() {
            var isValid = true;

            var required = this.get_emailAddress().get_validator().get_required();
            if (!required) {
                this.get_emailAddress().get_validator().set_required(this.get_required());
            }

            if (!this.get_emailAddress().validate()) {
                isValid = false;
            }
            
            if (!required) {
                this.get_emailAddress().get_validator().set_required(required);
            }

            return isValid;
        },
        get_windowBody: function () {
            return this._windowBody;
        },
        set_windowBody: function (value) {
            this._windowBody = value;
        },
        get_cancelButton: function () {
            return this._cancelButton;
        },
        set_cancelButton: function (value) {
            this._cancelButton = value;
        },
        get_closeButton: function () {
            return this._closeButton;
        },
        set_closeButton: function (value) {
            this._closeButton = value;
        },
        get_closeButton2: function () {
            return this._closeButton2;
        },
        set_closeButton2: function (value) {
            this._closeButton2 = value;
        },
        get_sendTestButton: function () {
            return this._sendTestButton;
        },
        set_sendTestButton: function (value) {
            this._sendTestButton = value;
        },
        get_emailAddress: function () {
            return this._emailAddress;
        },
        set_emailAddress: function (value) {
            this._emailAddress = value;
        },
        get_serviceUrl: function () {
            return this._serviceUrl;
        },
        set_serviceUrl: function (value) {
            this._serviceUrl = value;
        },
        get_required: function () {
            return this._required;
        },
        set_required: function (value) {
            this._required = value;
        }
    };

Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog.registerClass("Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsSendTestEmailDialog", Sys.UI.Control);