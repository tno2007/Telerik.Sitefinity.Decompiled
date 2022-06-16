Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner = function (element) {
    this._widgetTypeChoiceField = null;
    this._usernameTextField = null;
    this._titleTextField = null;
    this._subtitleTextField = null;
    this._searchTextField = null;
    this._timingChoiceField = null;
    this._loadEveryChoiceField = null;
    this._displayTweetsTextField = null;
    this._includeChoiceField = null;
    this._sizeChoiceField = null;
    this._colorChoiceField = null;
    this._widthTextField = null;
    this._heightTextField = null;
    this._shellBackgroundPicker = null;
    this._shellTextColorPicker = null;
    this._tweetBackgroundColorPicker = null;
    this._tweetTextColorColorPicker = null;
    this._linksColorPicker = null;
    this._listOfTextField = null;
    this._userNameLabel = null;
    this._includeScrollBarChoiceField = null;
    this._radTabStrip = null;
    this._valueOfUsernameLabel = null;

    Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner.callBaseMethod(this, 'initialize');

        this._widgetTypeSelectorValueChangedDelegate = Function.createDelegate(this, this._widgetTypeSelectorValueChanged);
        this.get_widgetTypeChoiceField().add_valueChanged(this._widgetTypeSelectorValueChangedDelegate);

        this._timingChoiceSelectorValueChangedDelegate = Function.createDelegate(this, this._timingChoiceSelectorValueChanged);
        this.get_timingChoiceField().add_valueChanged(this._timingChoiceSelectorValueChangedDelegate);

        this._sizeChoiceSelectorValueChangedDelegate = Function.createDelegate(this, this._sizeChoiceSelectorValueChanged);
        this.get_sizeChoiceField().add_valueChanged(this._sizeChoiceSelectorValueChangedDelegate);

        this._colorChoiceSelectorValueChangedDelegate = Function.createDelegate(this, this._colorChoiceSelectorValueChanged);
        this.get_colorChoiceField().add_valueChanged(this._colorChoiceSelectorValueChangedDelegate);

        this._usernameTextFieldValueChangedDelegate = Function.createDelegate(this, this._usernameTextFieldValueChanged);
        $addHandler(this.get_usernameTextField().get_textBoxElement(), "blur", this._usernameTextFieldValueChangedDelegate);

        this._radTabStripTabSelectedDelegate = Function.createDelegate(this, this._radTabStripTabSelected);
        this.get_radTabStrip().add_tabSelected(this._radTabStripTabSelectedDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner.callBaseMethod(this, 'dispose');
        if (this._widgetTypeSelectorValueChangedDelegate) {
            this.get_widgetTypeChoiceField().remove_valueChanged(this._widgetTypeSelectorValueChangedDelegate);
            delete this._widgetTypeSelectorValueChangedDelegate;
        }

        if (this._timingChoiceSelectorValueChangedDelegate) {
            this.get_timingChoiceField().remove_valueChanged(this._timingChoiceSelectorValueChangedDelegate);
            delete this._timingChoiceSelectorValueChangedDelegate;
        }

        if (this._sizeChoiceSelectorValueChangedDelegate) {
            this.get_sizeChoiceField().remove_valueChanged(this._sizeChoiceSelectorValueChangedDelegate);
            delete this._sizeChoiceSelectorValueChangedDelegate;
        }

        if (this._colorChoiceSelectorValueChangedDelegate) {
            this.get_radTabStrip().remove_tabSelected(this._colorChoiceSelectorValueChangedDelegate);
            delete this._colorChoiceSelectorValueChangedDelegate;
        }

        if (this._radTabStripTabSelectedDelegate) {
            this.get_colorChoiceField().remove_valueChanged(this._radTabStripTabSelectedDelegate);
            delete this._radTabStripTabSelectedDelegate;
        }

        if (this._usernameTextFieldValueChangedDelegate) {
            this.get_usernameTextField().remove_valueChanged(this._usernameTextFieldValueChangedDelegate);
            delete this._usernameTextFieldValueChangedDelegate;
        }

        if (this._usernameTextFieldValueChangedDelegate) {
            $removeHandler(this.get_usernameTextField().get_textBoxElement(), "blur", this._usernameTextFieldValueChangedDelegate);
            delete this._usernameTextFieldValueChangedDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control();
        if (this.get_usernameTextField()) {
            this.get_usernameTextField().set_value(controlData.Username);
        }

        if (this.get_widgetTypeChoiceField()) {
            this.get_widgetTypeChoiceField().set_value(controlData.FeedType);
        }
        if (this.get_timingChoiceField()) {
            this.get_timingChoiceField().set_value(controlData.TweetLoadBehavior);
        }
        if (this.get_loadEveryChoiceField()) {
            this.get_loadEveryChoiceField().set_value(controlData.TweetInterval);
        }

        if (this.get_includeChoiceField()) {
            if (controlData.ShowTimestamps) {
                this.get_includeChoiceField().set_selectedChoicesIndex(0);
            }
            if (controlData.ShowAvatars) {
                this.get_includeChoiceField().set_selectedChoicesIndex(1);
            }
            if (controlData.ShowHashtags) {
                this.get_includeChoiceField().set_selectedChoicesIndex(2);
            }
        }

        if (this.get_sizeChoiceField()) {
            if (controlData.AutoWidth) {
                this.get_sizeChoiceField().set_selectedChoicesIndex(0);
                this._setDefaultSizeScheme();
            }
            else {
                this.get_sizeChoiceField().set_selectedChoicesIndex(1);
                this._sizeChoiceSelectorValueChanged();
            }
        }

        if (this.get_colorChoiceField()) {
            if (controlData.AutoColor) {
                this.get_colorChoiceField().set_selectedChoicesIndex(0);
                this._setDefaultColorScheme();
            }
            else {
                this.get_colorChoiceField().set_selectedChoicesIndex(1);
                this._colorChoiceSelectorValueChanged();
            }
        }

        if (this.get_listOfTextField()) {
            this.get_listOfTextField().set_value(controlData.ListName);
        }

        if (this.get_titleTextField()) {
            this.get_titleTextField().set_value(controlData.Title);
        }
        if (this.get_subtitleTextField()) {
            this.get_subtitleTextField().set_value(controlData.Caption);
        }
        if (this.get_searchTextField()) {
            this.get_searchTextField().set_value(controlData.SearchQuery);
        }
        if (this.get_displayTweetsTextField()) {
            this.get_displayTweetsTextField().set_value(controlData.TweetsNumber);
        }
        if (this.get_widthTextField()) {
            this.get_widthTextField().set_value(controlData.Width);
        }
        if (this.get_heightTextField()) {
            this.get_heightTextField().set_value(controlData.Height);
        }
        if (this.get_shellBackgroundPicker()) {
            this.get_shellBackgroundPicker().set_selectedColor(controlData.ShellBackgroundColor);
        }
        if (this.get_shellTextColorPicker()) {
            this.get_shellTextColorPicker().set_selectedColor(controlData.ShellTextColor);
        }
        if (this.get_tweetBackgroundColorPicker()) {
            this.get_tweetBackgroundColorPicker().set_selectedColor(controlData.TweetBackgroundColor);
        }
        if (this.get_tweetTextColorColorPicker()) {
            this.get_tweetTextColorColorPicker().set_selectedColor(controlData.TweetTextColor);
        }
        if (this.get_linksColorPicker()) {
            this.get_linksColorPicker().set_selectedColor(controlData.LinksColor);
        }

        if (this.get_includeScrollBarChoiceField() && controlData.IncludeScrollbar) {
            this.get_includeScrollBarChoiceField().set_selectedChoicesIndex(0);
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        if (this.get_usernameTextField()) {
            controlData.Username = this.get_usernameTextField().get_value();
        }

        if (this.get_widgetTypeChoiceField()) {
            controlData.FeedType = this.get_widgetTypeChoiceField().get_value();
        }

        if (this.get_timingChoiceField()) {
            controlData.TweetLoadBehavior = this.get_timingChoiceField().get_value();
        }

        if (this.get_loadEveryChoiceField()) {
            controlData.TweetInterval = this.get_loadEveryChoiceField().get_value();
        }

        if (this.get_includeChoiceField()) {

            controlData.ShowTimestamps = false;
            controlData.ShowAvatars = false;
            controlData.ShowHashtags = false;

            var selectedChoices = this.get_includeChoiceField().get_selectedChoicesIndex();
            if (selectedChoices) {
                for (var i = 0; i < selectedChoices.length; i++) {

                    if (selectedChoices[i] == 0) {
                        controlData.ShowTimestamps = true;
                    }
                    else if (selectedChoices[i] == 1) {
                        controlData.ShowAvatars = true;
                    }
                    else {
                        controlData.ShowHashtags = true;
                    }
                }
            }
        }

        if (this.get_listOfTextField()) {
            controlData.ListName = this.get_listOfTextField().get_value();
        }

        if (this.get_sizeChoiceField()) {
            if (this.get_sizeChoiceField().get_value() == 1) {
                this._setDefaultSizeScheme();
                controlData.AutoWidth = true;
            }
            else {
                controlData.AutoWidth = false;
            }
        }

        if (this.get_colorChoiceField()) {
            if (this.get_colorChoiceField().get_value() == 1) {
                controlData.AutoColor = true;
            }
            else {
                controlData.AutoColor = false;
            }
        }

        if (this.get_titleTextField()) {
            controlData.Title = this.get_titleTextField().get_value();
        }

        if (this.get_subtitleTextField()) {
            controlData.Caption = this.get_subtitleTextField().get_value();
        }

        if (this.get_searchTextField()) {
            controlData.SearchQuery = this.get_searchTextField().get_value();
        }

        if (this.get_displayTweetsTextField()) {
            controlData.TweetsNumber = this.get_displayTweetsTextField().get_value();
        }

        if (this.get_widthTextField()) {
            controlData.Width = this.get_widthTextField().get_value();
        }

        if (this.get_heightTextField()) {
            controlData.Height = this.get_heightTextField().get_value();
        }

        if (this.get_shellBackgroundPicker()) {
            controlData.ShellBackgroundColor = this.get_shellBackgroundPicker().get_selectedColor();
        }

        if (this.get_shellTextColorPicker()) {
            controlData.ShellTextColor = this.get_shellTextColorPicker().get_selectedColor();
        }

        if (this.get_tweetBackgroundColorPicker()) {
            controlData.TweetBackgroundColor = this.get_tweetBackgroundColorPicker().get_selectedColor();
        }

        if (this.get_tweetTextColorColorPicker()) {
            controlData.TweetTextColor = this.get_tweetTextColorColorPicker().get_selectedColor();
        }

        if (this.get_linksColorPicker()) {
            controlData.LinksColor = this.get_linksColorPicker().get_selectedColor();
        }

        if (this.get_includeScrollBarChoiceField()) {
            controlData.IncludeScrollbar = (this.get_includeScrollBarChoiceField().get_selectedChoicesIndex() == 0);
        }
    },

    /* --------------------------------- private methods --------------------------------- */

    _setColorsToAllPickers: function (shellBackground, shellText, tweetBackground, tweetText, linksColor) {
        this.get_shellBackgroundPicker().set_selectedColor(shellBackground);
        this.get_shellTextColorPicker().set_selectedColor(shellText);
        this.get_tweetBackgroundColorPicker().set_selectedColor(tweetBackground);
        this.get_tweetTextColorColorPicker().set_selectedColor(tweetText);
        this.get_linksColorPicker().set_selectedColor(linksColor);
    },

    _setDefaultSizeScheme: function () {
        if (this.get_widthTextField()) {
            this.get_widthTextField().set_value(250);
        }
        if (this.get_heightTextField()) {
            this.get_heightTextField().set_value(300);
        }
    },
    _setDefaultColorScheme: function () {
        var selectedChoiceIndex = parseInt(this.get_widgetTypeChoiceField().get_value(), 10);
        switch (selectedChoiceIndex) {
            case 1:
                {
                    this._setColorsToAllPickers("#333333", "#ffffff", "#000000", "#ffffff", "#4aed05");
                }
                break;
            case 2:
                {
                    this._setColorsToAllPickers("#8ec1da", "#ffffff", "#ffffff", "#444444", "#1985b5");
                }
                break;
            case 3:
                {
                    this._setColorsToAllPickers("#43c43f", "#ffffff", "#ffffff", "#444444", "#43c43f");
                }
                break;
            case 4:
                {
                    this._setColorsToAllPickers("#ff96e7", "#ffffff", "#ffffff", "#444444", "#b740c2");
                }
                break;
            default:
                break;
        }
    },

    _radTabStripTabSelected: function () {
        dialogBase.resizeToContent();
    },

    _widgetTypeSelectorValueChanged: function () {
        var selectedChoiceIndex = parseInt(this.get_widgetTypeChoiceField().get_value(), 10);
        switch (selectedChoiceIndex) {
            case 1:
                {
                    jQuery("#titleDiv").hide();
                    jQuery("#searchInputDiv").hide();
                    jQuery("#usernameDiv").show();
                    jQuery("#twitterListDiv").hide();
                }
                break;
            case 2:
                {
                    jQuery("#titleDiv").show();
                    jQuery("#searchInputDiv").show();
                    jQuery("#usernameDiv").hide();
                    jQuery("#twitterListDiv").hide();
                }
                break;
            case 3:
                {
                    jQuery("#titleDiv").show();
                    jQuery("#searchInputDiv").hide();
                    jQuery("#usernameDiv").show();
                    jQuery("#twitterListDiv").hide();
                }
                break;
            case 4:
                {
                    jQuery("#titleDiv").show();
                    jQuery("#searchInputDiv").hide();
                    jQuery("#usernameDiv").show();
                    jQuery("#twitterListDiv").show();
                    this._changeUserNameLabelText();
                }
                break;
            default:
                break;
        }

        this.get_colorChoiceField().set_selectedChoicesIndex(0);
        this._setDefaultColorScheme();
        this._setDefaultSizeScheme();
        jQuery("#colorPickersDiv").hide();
        dialogBase.resizeToContent();
    },

    _timingChoiceSelectorValueChanged: function () {
        var selectedChoiceIndex = parseInt(this.get_timingChoiceField().get_value(), 10);
        switch (selectedChoiceIndex) {
            case 1:
                {
                    jQuery("#loadEveryDiv").hide();
                }
                break;
            case 2:
                {
                    jQuery("#loadEveryDiv").show();
                }
                break;
            default:
                break;
        }
        dialogBase.resizeToContent();
    },

    _sizeChoiceSelectorValueChanged: function () {
        var selectedChoiceIndex = parseInt(this.get_sizeChoiceField().get_value(), 10);
        switch (selectedChoiceIndex) {
            case 1:
                {
                    jQuery("#sizeDiv").hide();
                }
                break;
            case 2:
                {
                    jQuery("#sizeDiv").show();
                }
                break;
            default:
                break;
        }
        dialogBase.resizeToContent();
    },

    _colorChoiceSelectorValueChanged: function () {
        var selectedChoiceIndex = parseInt(this.get_colorChoiceField().get_value(), 10);
        switch (selectedChoiceIndex) {
            case 1:
                {
                    jQuery("#colorPickersDiv").hide();
                }
                break;
            case 2:
                {
                    jQuery("#colorPickersDiv").show();
                }
                break;
            default:
                break;
        }
        dialogBase.resizeToContent();
    },

    _changeUserNameLabelText: function () {
        var username = this.get_usernameTextField().get_value().replace(/\"/gi, "&quot;");
        this.get_userNameLabel().innerHTML = this.get_valueOfUsernameLabel() + username;
    },

    _usernameTextFieldValueChanged: function () {
        var selectedChoiceIndex = parseInt(this.get_widgetTypeChoiceField().get_value(), 10);
        if (selectedChoiceIndex == 4) {
            this._changeUserNameLabelText();
        }
    },

    /* --------------------------------- properties --------------------------------- */

    get_widgetTypeChoiceField: function () {
        return this._widgetTypeChoiceField;
    },

    set_widgetTypeChoiceField: function (value) {
        if (this._widgetTypeChoiceField != value) {
            this._widgetTypeChoiceField = value;
        }
    },

    get_usernameTextField: function () {
        return this._usernameTextField;
    },

    set_usernameTextField: function (value) {
        if (this._usernameTextField != value) {
            this._usernameTextField = value;
        }
    },

    get_titleTextField: function () {
        return this._titleTextField;
    },

    set_titleTextField: function (value) {
        if (this._titleTextField != value) {
            this._titleTextField = value;
        }
    },

    get_subtitleTextField: function () {
        return this._subtitleTextField;
    },

    set_subtitleTextField: function (value) {
        if (this._subtitleTextField != value) {
            this._subtitleTextField = value;
        }
    },

    get_searchTextField: function () {
        return this._searchTextField;
    },

    set_searchTextField: function (value) {
        if (this._searchTextField != value) {
            this._searchTextField = value;
        }
    },

    get_timingChoiceField: function () {
        return this._timingChoiceField;
    },

    set_timingChoiceField: function (value) {
        if (this._timingChoiceField != value) {
            this._timingChoiceField = value;
        }
    },

    get_loadEveryChoiceField: function () {
        return this._loadEveryChoiceField;
    },

    set_loadEveryChoiceField: function (value) {
        if (this._loadEveryChoiceField != value) {
            this._loadEveryChoiceField = value;
        }
    },

    get_displayTweetsTextField: function () {
        return this._displayTweetsTextField;
    },

    set_displayTweetsTextField: function (value) {
        if (this._displayTweetsTextField != value) {
            this._displayTweetsTextField = value;
        }
    },

    get_includeChoiceField: function () {
        return this._includeChoiceField;
    },

    set_includeChoiceField: function (value) {
        if (this._includeChoiceField != value) {
            this._includeChoiceField = value;
        }
    },

    get_sizeChoiceField: function () {
        return this._sizeChoiceField;
    },

    set_sizeChoiceField: function (value) {
        if (this._sizeChoiceField != value) {
            this._sizeChoiceField = value;
        }
    },

    get_colorChoiceField: function () {
        return this._colorChoiceField;
    },

    set_colorChoiceField: function (value) {
        if (this._colorChoiceField != value) {
            this._colorChoiceField = value;
        }
    },

    get_widthTextField: function () {
        return this._widthTextField;
    },

    set_widthTextField: function (value) {
        if (this._widthTextField != value) {
            this._widthTextField = value;
        }
    },

    get_heightTextField: function () {
        return this._heightTextField;
    },

    set_heightTextField: function (value) {
        if (this._heightTextField != value) {
            this._heightTextField = value;
        }
    },

    get_shellBackgroundPicker: function () {
        return this._shellBackgroundPicker;
    },

    set_shellBackgroundPicker: function (value) {
        if (this._shellBackgroundPicker != value) {
            this._shellBackgroundPicker = value;
        }
    },

    get_shellTextColorPicker: function () {
        return this._shellTextColorPicker;
    },

    set_shellTextColorPicker: function (value) {
        if (this._shellTextColorPicker != value) {
            this._shellTextColorPicker = value;
        }
    },

    get_tweetBackgroundColorPicker: function () {
        return this._tweetBackgroundColorPicker;
    },

    set_tweetBackgroundColorPicker: function (value) {
        if (this._tweetBackgroundColorPicker != value) {
            this._tweetBackgroundColorPicker = value;
        }
    },

    get_tweetTextColorColorPicker: function () {
        return this._tweetTextColorColorPicker;
    },

    set_tweetTextColorColorPicker: function (value) {
        if (this._tweetTextColorColorPicker != value) {
            this._tweetTextColorColorPicker = value;
        }
    },

    get_listOfTextField: function () {
        return this._listOfTextField;
    },

    set_listOfTextField: function (value) {
        if (this._listOfTextField != value) {
            this._listOfTextField = value;
        }
    },

    get_linksColorPicker: function () {
        return this._linksColorPicker;
    },

    set_linksColorPicker: function () {
        if (this._linksColorPicker != value) {
            this._linksColorPicker = value;
        }
    },

    get_userNameLabel: function () {
        return this._userNameLabel;
    },

    set_userNameLabel: function (value) {
        if (this._userNameLabel != value) {
            this._userNameLabel = value;
        }
    },

    get_includeScrollBarChoiceField: function () {
        return this._includeScrollBarChoiceField;
    },

    set_includeScrollBarChoiceField: function (value) {
        if (this._includeScrollBarChoiceField != value) {
            this._includeScrollBarChoiceField = value;
        }
    },

    get_radTabStrip: function () {
        return this._radTabStrip;
    },

    set_radTabStrip: function (value) {
        if (this._radTabStrip != value) {
            this._radTabStrip = value;
        }
    },

    get_valueOfUsernameLabel: function () {
        return this._valueOfUsernameLabel;
    },

    set_valueOfUsernameLabel: function (value) {
        if (this._valueOfUsernameLabel != value) {
            this._valueOfUsernameLabel = value;
        }
    },

    get_userNameLabel: function () {
        return this._userNameLabel;
    },

    set_linksColorPicker: function (value) {
        if (this._linksColorPicker != value) {
            this._linksColorPicker = value;
        }
    }
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.TwitterFeedDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);