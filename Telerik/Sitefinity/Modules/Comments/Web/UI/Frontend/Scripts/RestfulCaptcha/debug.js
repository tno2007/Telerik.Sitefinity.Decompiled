﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha.initializeBase(this, [element]);

    this._commentsRestClient = null;

    this._captchaImage = null;
    this._captchaInput = null;
    this._refreshLink = null;

    this._key = null;
    this._iv = null;
    this._correctAnswer = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha.callBaseMethod(this, 'initialize');

        var $this = jQuery(this.get_element());
        this._captchaImage = $this.find("img.sfRestfulCaptchaImage");
        this._captchaInput = $this.find("input.sfRestfulCapthaInput");
        this._refreshLink = $this.find("a.sfRestfulCaptchaRefresh");

        var that = this;
        this._refreshLink.click(function () {
            that.refresh();
        });
    },

    dispose: function () {

        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha.callBaseMethod(this, 'dispose');
    },

    refresh: function () {
        var that = this;
        var onSuccess = function (data) {
            if (data) {
                that.get_captchaImage().attr("src", "data:image/png;base64," + data.Image);
                that._iv = data.InitializationVector;
                that._correctAnswer = data.CorrectAnswer;
                that._key = data.Key;
                that.get_captchaInput().val("");
                that.get_captchaInput().show();
            }
        };

        this.get_captchaImage().attr("src", "");
        this.get_captchaInput().hide();

        this.get_commentsRestClient().getCaptcha(onSuccess);
    },

    get_commentsRestClient: function () {
        return this._commentsRestClient;
    },
    set_commentsRestClient: function (value) {
        this._commentsRestClient = value;
        this.refresh();
    },

    get_captchaImage: function () {
        return this._captchaImage;
    },
    get_captchaInput: function () {
        return this._captchaInput;
    },
    get_refreshLink: function () {
        return this._refreshLink;
    },

    get_iv: function () {
        return this._iv;
    },
    get_key: function () {
        return this._key;
    },
    get_correctAnswer: function () {
        return this._correctAnswer;
    },
    get_answer: function () {
        return this.get_captchaInput().val();
    }
};
Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.RestfulCaptcha", Sys.UI.Control);