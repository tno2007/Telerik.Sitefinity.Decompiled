Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm.initializeBase(this, [element]);
    this._serviceUrl = null;
    this._requireAuthentication = null;
    this._enableRatings = null;
    this._threadType = null;
    this._threadBehavior = null;
    this._threadTitle = null;
    this._groupKey = null;
    this._dataSource = null;
    this._createThread = null;
    this.showAuthenticatedForm;
    this.showUnAuthenticatedForm;
    this._loginLink = true;
    this._commentEditor = null;
    this._nameTextField = null;
    this._emailTextField = null;
    this._submitCommentButton = null;
    this._submitCommentClickDelegate = null;
    this._item = null;
    this._loadingView = null;
    this._messageControl = null;
    this._validationMessage = null;
    this._validationMessageRating = null;
    this._clientLabelManager = null;
    this._threadKey = null;
    this._language = null;
    this._restfulCaptcha = null;
    this._captchaError = null;
    this._requireCaptcha = null;
    this._submitCommentForm = null;
    this._ajaxCaptchaFailDelegate = null;
    this._loginDelegate = null;
    this._leaveCommentLabel = null;
    this._loginText = null;
    this._ratingsLabel = null;
    this._ratingSection = null;
    this._ratingComponent = null;

    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm.callBaseMethod(this, 'initialize');

        var commentsRestClientCommon = new Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient(this.get_serviceUrl());
        this.set_commentsRestClient(commentsRestClientCommon);

        jQuery(this.get_nameTextField().get_textBoxElement()).watermarkField({
            defaultText: this.get_clientLabelManager().getLabel("CommentsResources", "NameWatermark"),
            isInline: true
        });
        jQuery(this.get_emailTextField().get_textBoxElement()).watermarkField({
            defaultText: this.get_clientLabelManager().getLabel("CommentsResources", "EmailWatermark"),
            isInline: true
        });

        jQuery(this.get_validationMessage()).hide();
        jQuery(this.get_validationMessageRating()).hide();

        this._configureKendoEditor();

        this._submitCommentClickDelegate = Function.createDelegate(this, this._submitCommentClickHandler);
        $addHandler(this.get_submitCommentButton(), "click", this._submitCommentClickDelegate);

        this._ajaxCompleteDelegate = Function.createDelegate(this, this._ajaxCompleteHandler);
        this._ajaxFailDelegate = Function.createDelegate(this, this._ajaxFailHandler);
        this._ajaxCaptchaFailDelegate = Function.createDelegate(this, this._ajaxCaptchaFailHandles);
        this._loginDelegate = Function.createDelegate(this, this._loginHandler);

        if (this._onLoadDelegate == null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }

        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        this._reset();
        if (this._submitCommentClickDelegate) {
            if (this.get_submitCommentButton()) {
                $removeHandler(this.get_submitCommentButton(), "click", this._submitCommentClickDelegate);
            }
            delete this._submitCommentClickDelegate;
        }

        if (this._ajaxCompleteDelegate) {
            delete this._ajaxCompleteDelegate;
        }

        if (this._ajaxFailDelegate) {
            delete this._ajaxFailDelegate;
        }

        if (this._ajaxCaptchaFailDelegate) {
            delete this._ajaxCaptchaFailDelegate;
        }

        if (this._loginDelegate) {
            delete this._loginDelegate;
        }

        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm.callBaseMethod(this, 'dispose');
    },

    //configure Kendo Editor
    _configureKendoEditor: function () {
        var that = this;
        jQuery(this.get_commentEditor()).kendoEditor({
            paste: function (ev) {
                ev.html = jQuery(ev.html).text();
            }
        });
        var editor = jQuery(this.get_commentEditor()).data("kendoEditor");
        editor.toolbar.element.hide();
        if (editor) {
            jQuery(editor.window).blur(function () {
                var currentValue = editor.value();
                if (!currentValue || currentValue == null)
                    jQuery(that.get_leaveCommentLabel()).toggleClass("sfFocused", false);
            })
            $(editor.window).focus(function () {
                jQuery(that.get_leaveCommentLabel()).toggleClass("sfFocused", true);
            })
        }
    },

    //Set the proper comments submit form based on comments module security settings
    setFormViewMode: function (isAuthenticated) {
        jQuery(this.get_messageControl().get_element()).hide();
        if (!isAuthenticated) {
            if (this.get_requireAuthentication()) {
                this.showAuthenticatedForm = false;
                this.showUnAuthenticatedForm = false;
                jQuery(this.get_submitCommentForm()).hide();
                jQuery(this.get_loginLink()).show();
            }
            else {
                this.showAuthenticatedForm = false;
                this.showUnAuthenticatedForm = true;
                jQuery(this.get_submitCommentForm()).show();

                var editor = jQuery(this.get_commentEditor()).data("kendoEditor");
                if (editor) {
                    editor.bind("select", jQuery.proxy(this.editor_select, this));
                }

                jQuery(this.get_loginLink()).hide();
            }
        }
        else {
            this.showAuthenticatedForm = true;
            this.showUnAuthenticatedForm = false;
            jQuery(this.get_submitCommentForm()).show();
            jQuery(this.get_nameTextField().get_element()).hide();
            jQuery(this.get_emailTextField().get_element()).hide();
            jQuery(this.get_loginLink()).hide();
        }
    },

    setCaptchaViewMode: function (requireCaptcha) {
        if (!this.get_requireAuthentication()) {
            if (requireCaptcha == true) {
                this.set_requireCaptcha(requireCaptcha);
                jQuery(this.get_restfulCaptcha().get_element()).show();
                this.initializeRestfulCaptchaService();
            }
            else {
                this.set_requireCaptcha(false);
                jQuery(this.get_restfulCaptcha().get_element()).hide();
            }
        }
    },

    initializeRestfulCaptchaService: function () {
        this.get_restfulCaptcha().set_commentsRestClient(this.get_commentsRestClient());
    },

    //handles click of the submit button
    _submitCommentClickHandler: function (sender, args) {
        if (this._isValid()) {
            var that = this;
            var onSuccess = function (data, textStatus, jqXHR) {
                jQuery(that.get_messageControl().get_element()).hide();
                jQuery(that.get_captchaError()).hide();
                if (that.get_enableRatings() == true) {
                    that._hideForm();
                }

                that._item.Key = data.Key;
                that._item.Status = data.Status;
                that._item.ProfilePictureThumbnailUrl = data.ProfilePictureThumbnailUrl;
                that._item.Name = data.Name;
                that._item.Email = data.Email;
                that._item.DateCreated = data.DateCreated;
                that._item.Message = data.Message;
                that._item.Parent = data.Parent;
                that._item.ThreadKey = that.get_threadKey();
                that.raise_submitSuccess(that._item);
                that.set_createThread(false);
                that._reset();
            };
            this._saveItem(onSuccess);
        }
    },

    add_submitSuccess: function (delegate) {
        this.get_events().addHandler('submitSuccess', delegate);
    },
    remove_submitSuccess: function (delegate) {
        this.get_events().removeHandler('submitSuccess', delegate);
    },

    raise_submitSuccess: function (args) {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('submitSuccess');
            if (h) h(this, args);
            return args;
        }
    },

    //makes visible fields for Name and Email
    editor_select: function (sender, args) {
        jQuery(this.get_nameTextField().get_element()).show();
        jQuery(this.get_emailTextField().get_element()).show();
    },

    //reset all of the fields in the submit form 
    _reset: function () {
        this._item = null;
        jQuery(this.get_commentEditor()).data("kendoEditor").value("");

        this._updateRating(0);

        this.get_nameTextField().reset();
        this.get_emailTextField().reset();
    },

    _updateRating: function (rating) {
        if (this.get_enableRatings() == false) {
            return;
        }

        if (rating >= 0) {
            if (this._ratingComponent) {
                this._ratingComponent.set_value(rating);
            } else {
                var jqRatingSection = jQuery(this.get_ratingSection());
                this._ratingComponent = jqRatingSection.rating({
                    value: rating,
                    label: this.get_ratingsLabel(),
                    readOnly: false,
                    displayMode: "IconOnly"
                });
            }
        }
    },

    //creates the object that will be send to the server
    _updateCurrentItem: function () {
        if (this._item == null) {
            this._item = {};
        }

        if (this.showUnAuthenticatedForm) {
            this._item.Name = this.get_nameTextField().get_value();
            this._item.Email = this.get_emailTextField().get_value();
        }

        this._item.Message = jQuery(this.get_commentEditor()).data("kendoEditor").value();
        this._item.ThreadKey = this.get_threadKey();

        if (this.get_enableRatings() == true) {
            var selectedRating = this._ratingComponent.get_value();
            if (selectedRating > 0) {
                this._item.Rating = selectedRating;
            }
        }

        if (this.get_createThread()) {
            if (this._item.Thread == null) {
                this._item.Thread = {};
                this._item.Thread.Group = {};
            }

            this._item.Thread.GroupKey = this.get_groupKey();
            this._item.Thread.Type = this.get_threadType();
            this._item.Thread.Behavior = this.get_threadBehavior();
            this._item.Thread.Title = this.get_threadTitle();
            this._item.Thread.DataSource = this.get_dataSource();
            this._item.Thread.Key = this.get_threadKey();
            this._item.Thread.Language = this.get_language();

            this._item.Thread.Group.Key = this.get_groupKey();
        }
    },

    //shows loading message
    _setLoadingViewVisible: function (loading) {
        if (loading)
            jQuery(this.get_loadingView()).show();
        else
            jQuery(this.get_loadingView()).hide();
    },

    //hides Submit button when loading message is shown
    _setSubmitButtonVisible: function (loading) {
        if (loading)
            jQuery(this.get_submitCommentButton()).show();
        else
            jQuery(this.get_submitCommentButton()).hide();
    },

    //check if all required fields are filled
    _isValid: function () {
        var isValid = true;
        if ($.trim($(jQuery(this.get_commentEditor()).data("kendoEditor").body).text()) == "") {
            isValid = false;
            jQuery(this.get_validationMessage()).show();
        } else {
            jQuery(this.get_validationMessage()).hide();
        }

        if (this.get_enableRatings() == true) {
            var selectedRating = this._ratingComponent.get_value();
            if (selectedRating > 0) {
                jQuery(this.get_validationMessageRating()).hide();
            } else {
                isValid = false;
                jQuery(this.get_validationMessageRating()).show();
            }
        }

        if (this.showUnAuthenticatedForm) {
            if (this.get_nameTextField().validate() == false) {
                isValid = false;
            }
            if (this.get_emailTextField().get_value() !== "" && this.get_emailTextField().validate() == false) {
                isValid = false;
            }
        }

        return isValid;
    },

    _isVisible: function () {
        return jQuery(this.get_submitCommentForm()).is(":visible");
    },

    _onLoad: function () {
        this._updateRating(0);
    },

    //performs the Post request to the server to save current comment
    _saveItem: function (onSuccess) {
        this._setLoadingViewVisible(true);
        this._setSubmitButtonVisible(false);

        if (this.get_requireCaptcha()) {
            jQuery(this.get_restfulCaptcha().get_element()).hide();
        }

        this._updateCurrentItem();

        if (this.showUnAuthenticatedForm && this.get_requireCaptcha()) {
            this._item.Captcha = {};
            this._item.Captcha.Answer = this.get_restfulCaptcha().get_answer();
            this._item.Captcha.CorrectAnswer = this.get_restfulCaptcha().get_correctAnswer();
            this._item.Captcha.InitializationVector = this.get_restfulCaptcha().get_iv()
            this._item.Captcha.Key = this.get_restfulCaptcha().get_key();

            this.get_commentsRestClient().createComment(this._item, onSuccess, this._ajaxCaptchaFailDelegate, this._ajaxCompleteDelegate);

            this.get_restfulCaptcha().refresh();
        }
        else {
            this.get_commentsRestClient().createComment(this._item, onSuccess, this._ajaxFailDelegate, this._ajaxCompleteDelegate);
        }
    },

    _ajaxCompleteHandler: function (jqXHR, textStatus) {
        this._setLoadingViewVisible(false);
        this._setSubmitButtonVisible(true);
        if (this.get_requireCaptcha()) {
            jQuery(this.get_restfulCaptcha().get_element()).show();
        }
        jQuery(this.get_validationMessage()).hide();
        jQuery(this.get_validationMessageRating()).hide();
    },

    _ajaxFailHandler: function (jqXHR, textStatus, errorThrown) {
        this._setLoadingViewVisible(false);
        this._setSubmitButtonVisible(true);
        if (jqXHR.responseText) {
            var responseText = jqXHR.responseText;
            try {
                var responseAsJson = Telerik.Sitefinity.JSON.parse(jqXHR.responseText);
                responseText = responseAsJson.Detail;

                // When an exception is thrown (e.g. ArgumentException) the error message is in responseAsJson.ResponseStatus.Message
                if (!responseText && responseAsJson.ResponseStatus && responseAsJson.ResponseStatus.Message) {
                    responseText = responseAsJson.ResponseStatus.Message;
                }
            }
            catch (err) {
                console.log(err);
            }

            var anchorElement;
            if (responseText) {
                var authError = jqXHR.getResponseHeader("X-Authentication-Error");
                var returnUrl = "/"; //home
                if (401 == jqXHR.status || (403 == jqXHR.status && authError == "SessionExpired")) {
                    var issuer = jqXHR.getResponseHeader("Issuer");
                    var realm = jqXHR.getResponseHeader("Realm");
                    if (issuer && realm) {
                        loginUrl = issuer + "?UserLoggingReason=SessionExpired&deflate=true&realm=" + encodeURIComponent(realm) + "&redirect_uri=" + returnUrl;
                        anchorElement = this._generateAnchorElement(this.get_loginText(), this._loginDelegate, loginUrl);
                    }
                }
                else if (403 == jqXHR.status) {
                    var selfLogoutUrl = "/Sitefinity/SignOut/selflogout?ReturnUrl=" + returnUrl;
                    anchorElement = this._generateAnchorElement(this.get_loginText(), this._loginDelegate, selfLogoutUrl);
                }

                this.get_messageControl().showNegativeMessage(responseText);
                var messageControlElement = this.get_messageControl().get_element();
                if (anchorElement)
                    $(messageControlElement).append(anchorElement);
            }
        }
        else
            this.get_messageControl().showNegativeMessage("unknown error");
    },

    _loginHandler: function () {
        this.get_messageControl().hide();
    },

    _generateAnchorElement: function (title, commandDelegate, location, target) {
        var anchorElement = document.createElement('a');
        anchorElement.innerHTML = '<span>' + title + '</span>';
        if (!location)
            location = "javascript:void(0)";
        $(anchorElement).attr("href", location);

        if (!target)
            target = "blank";
        $(anchorElement).attr("target", target);

        $addHandler(anchorElement, 'click', commandDelegate);

        return anchorElement;
    },

    _ajaxCaptchaFailHandles: function (jqXHR, textStatus, errorThrown) {
        this._setLoadingViewVisible(false);
        this._setSubmitButtonVisible(true);
        if (jqXHR.responseText) {
            var responseObj = Telerik.Sitefinity.JSON.parse(jqXHR.responseText);
            if (responseObj && responseObj.ResponseStatus &&
                responseObj.ResponseStatus.Message == this.get_clientLabelManager().getLabel("CommentsResources", "CaptchavalidationError")) {
                jQuery(this.get_captchaError()).toggle(true);
            }
            else {
                this.get_messageControl().showNegativeMessage(Telerik.Sitefinity.JSON.parse(jqXHR.responseText).Detail);
            }
        }
        else {
            this.get_messageControl().showNegativeMessage("unknown error");
        }
    },

    _hideForm: function () {
        jQuery(this.get_submitCommentForm()).hide();
    },


    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },

    get_submitUnAuthenticatedCommentForm: function () {
        return this._submitUnAuthenticatedCommentForm;
    },
    set_submitUnAuthenticatedCommentForm: function (value) {
        this._submitUnAuthenticatedCommentForm = value;
    },

    get_submitAuthenticatedCommentForm: function () {
        return this._submitAuthenticatedCommentForm;
    },
    set_submitAuthenticatedCommentForm: function (value) {
        this._submitAuthenticatedCommentForm = value;
    },

    get_commentsRestClient: function () {
        return this._commentsRestClient;
    },
    set_commentsRestClient: function (value) {
        this._commentsRestClient = value;
    },

    get_loginLink: function () {
        return this._loginLink;
    },
    set_loginLink: function (value) {
        this._loginLink = value;
    },

    get_threadType: function () {
        return this._threadType;
    },
    set_threadType: function (value) {
        this._threadType = value;
    },

    get_threadBehavior: function () {
        return this._threadBehavior;
    },
    set_threadBehavior: function (value) {
        this._threadBehavior = value;
    },

    get_threadTitle: function () {
        return this._threadTitle;
    },
    set_threadTitle: function (value) {
        this._threadTitle = value;
    },

    get_groupKey: function () {
        return this._groupKey;
    },
    set_groupKey: function (value) {
        this._groupKey = value;
    },

    get_createThread: function () {
        return this._createThread;
    },
    set_createThread: function (value) {
        this._createThread = value;
    },

    get_requireAuthentication: function () {
        return this._requireAuthentication;
    },
    set_requireAuthentication: function (value) {
        this._requireAuthentication = value;
    },

    get_enableRatings: function () {
        return this._enableRatings;
    },
    set_enableRatings: function (value) {
        this._enableRatings = value;
    },

    get_commentEditor: function () {
        return this._commentEditor;
    },
    set_commentEditor: function (value) {
        this._commentEditor = value;
    },

    get_nameTextField: function () {
        return this._nameTextField;
    },
    set_nameTextField: function (value) {
        this._nameTextField = value;
    },

    get_emailTextField: function () {
        return this._emailTextField;
    },
    set_emailTextField: function (value) {
        this._emailTextField = value;
    },

    get_submitCommentButton: function () {
        return this._submitCommentButton;
    },
    set_submitCommentButton: function (value) {
        this._submitCommentButton = value;
    },

    get_loadingView: function () {
        return this._loadingView;
    },
    set_loadingView: function (value) {
        this._loadingView = value;
    },

    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },

    get_validationMessage: function () {
        return this._validationMessage;
    },
    set_validationMessage: function (value) {
        this._validationMessage = value;
    },

    get_validationMessageRating: function () {
        return this._validationMessageRating;
    },
    set_validationMessageRating: function (value) {
        this._validationMessageRating = value;
    },

    get_threadKey: function () {
        return this._threadKey;
    },
    set_threadKey: function (value) {
        this._threadKey = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_language: function () {
        return this._language;
    },
    set_language: function (value) {
        this._language = value;
    },

    get_captchaError: function () {
        return this._captchaError;
    },
    set_captchaError: function (value) {
        this._captchaError = value;
    },

    get_requireCaptcha: function () {
        return this._requireCaptcha;
    },
    set_requireCaptcha: function (value) {
        this._requireCaptcha = value;
    },

    get_restfulCaptcha: function () {
        return this._restfulCaptcha;
    },
    set_restfulCaptcha: function (value) {
        this._restfulCaptcha = value;
    },

    get_submitCommentForm: function () {
        return this._submitCommentForm;
    },
    set_submitCommentForm: function (value) {
        this._submitCommentForm = value;
    },

    get_leaveCommentLabel: function () {
        return this._leaveCommentLabel;
    },
    set_leaveCommentLabel: function (value) {
        this._leaveCommentLabel = value;
    },

    get_dataSource: function () {
        return this._dataSource;
    },
    set_dataSource: function (value) {
        this._dataSource = value;
    },

    get_loginText: function () {
        return this._loginText;
    },
    set_loginText: function (value) {
        this._loginText = value;
    },

    get_ratingsLabel: function () {
        return this._ratingsLabel;
    },
    set_ratingsLabel: function (value) {
        this._ratingsLabel = value;
    },

    get_ratingSection: function () {
        return this._ratingSection;
    },
    set_ratingSection: function (value) {
        this._ratingSection = value;
    }
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsSubmitForm", Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();