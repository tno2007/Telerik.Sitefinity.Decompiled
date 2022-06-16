/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl.initializeBase(this, [element]);
    this._rootPanel = null;
    this._linkHtmlPanel = null;
    this._customizePanel = null;
    this._customSizesPanel = null;
    this._linkTextField = null;
    this._embedTextField = null;
    this._customWidthTextField = null;
    this._customHeightTextField = null;
    this._sizesChoiceField = null;
    this._customizedEmbedTextField = null;
    this._customizeButton = null;
    this._closeButton = null;
    this._dataItem = null;
    this._dataItemContext = null;

    this._mediaUrl = "";
    this._embedUrl = "";
    this._mediaWidth = 0;
    this._mediaHeight = 0;
    this._originalMediaWidth = 0;
    this._originalMediaHeight = 0;
    this._alternativeText = "";
    this._mode = null;

    this._embedStringTemplate = null;
    this._defaultHtml5EmbedStringTemplate = null;

    this._closeButtonClickDelegate = null;
    this._customizeButtonClickDelegate = null;
    this._sizeChangedDelegate = null;
    this._customSizesChangedDelegate = null;
    this._textFieldElementClickDelegate = null;
    this._onLoadDelegate = null;
    this._domain = "http://" + document.domain;
    // matches string pattern like 100x300
    this._sizesRegEx = /(\d+)(?:x)(\d+)/i;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {

        this._closeButtonClickDelegate = Function.createDelegate(this, this._closeButtonClickHandler);
        $addHandler(this._closeButton, "click", this._closeButtonClickDelegate);

        this._customizeButtonClickDelegate = Function.createDelegate(this, this._customizeButtonClickHandler);
        $addHandler(this._customizeButton, "click", this._customizeButtonClickDelegate);

        this._sizeChangedDelegate = Function.createDelegate(this, this._sizeChangedHandler);
        this._sizesChoiceField.add_valueChanged(this._sizeChangedDelegate);

        this._customSizesChangedDelegate = Function.createDelegate(this, this._customSizesChangedHandler);
        $addHandler(this._customWidthTextField.get_textElement(), "blur", this._customSizesChangedDelegate);
        $addHandler(this._customHeightTextField.get_textElement(), "blur", this._customSizesChangedDelegate);

        this._textFieldElementClickDelegate = Function.createDelegate(this, this._textFieldElementClickHandler);
        $addHandler(this._linkTextField.get_textElement(), "click", this._textFieldElementClickDelegate);
        $addHandler(this._embedTextField.get_textElement(), "click", this._textFieldElementClickDelegate);
        $addHandler(this._customizedEmbedTextField.get_textElement(), "click", this._textFieldElementClickDelegate);

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._closeButtonClickDelegate) {
            $removeHandler(this._closeButton, "click", this._closeButtonClickDelegate);
            delete this._closeButtonClickDelegate;
        }

        if (this._customizeButtonClickDelegate) {
            $removeHandler(this._customizeButton, "click", this._customizeButtonClickDelegate);
        }

        if (this._sizesChoiceField) {
            this._sizesChoiceField.remove_valueChanged(this._sizeChangedDelegate);
            delete this._sizeChangedDelegate;
        }

        if (this._customSizesChangedDelegate) {
            delete this._customSizesChangedDelegate;
        }

        if (this._textFieldElementClickDelegate) {

            if (this._linkTextField) {
                $removeHandler(this._linkTextField.get_textElement(), "click", this._textFieldElementClickDelegate);
            }
            if (this._embedTextField) {
                $removeHandler(this._embedTextField.get_textElement(), "click", this._textFieldElementClickDelegate);
            }
            if (this._customizedEmbedTextField) {
                $removeHandler(this._customizedEmbedTextField.get_textElement(), "click", this._textFieldElementClickDelegate);
            }
            delete this._textFieldElementClickDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    _onLoad: function () {
        if (jQuery.browser.safari && !dialogBase._dialog.isMaximized())
            jQuery("body").addClass("sfOverflowHiddenX");
        dialogBase.resizeToContent();
    },

    reset: function () {
        this._customWidthTextField.reset();
        this._customHeightTextField.reset();
        this._sizesChoiceField.reset();

        jQuery(this._customSizesPanel).hide();
        jQuery(this._customizePanel).hide();
        jQuery(this._linkHtmlPanel).show();
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    // Handles the click on the close button
    _closeButtonClickHandler: function (e) {
        jQuery(this._customSizesPanel).hide();
        jQuery(this._customizePanel).hide();
        jQuery(this._linkHtmlPanel).show();
        dialogBase.resizeToContent();
    },

    // Handles the click on the customize button
    _customizeButtonClickHandler: function (e) {
        jQuery(this._linkHtmlPanel).hide();
        jQuery(this._customizePanel).show();
        dialogBase.resizeToContent();
    },

    // Handles the selection change on the sizes list
    _sizeChangedHandler: function (sender, args) {
        var value = sender.get_value();
        if (value == "custom") {
            jQuery(this._customSizesPanel).show();
            this._customWidthTextField.set_value(this._mediaWidth);
            this._customHeightTextField.set_value(this._mediaHeight);
        }
        else {
            var width = this._originalMediaWidth;
            var height = this._originalMediaHeight;
            jQuery(this._customSizesPanel).hide();
            if (value != "original") {
                var matches = this._sizesRegEx.exec(value);
                if (matches) {
                    width = matches[1];
                    height = matches[2];
                }
            }

            var embedHtml = this._generateEmbedHtml(this._embedUrl, width, height, this._alternativeText);
            this._customizedEmbedTextField.set_value(embedHtml);
        }
        dialogBase.resizeToContent();
    },

    // Handles the changes in the custo width/height fields
    _customSizesChangedHandler: function (sender, args) {
        var isValid = this._customWidthTextField.validate();
        isValid = this._customHeightTextField.validate() && isValid;
        if (isValid) {
            var width = this._customWidthTextField.get_value();
            var height = this._customHeightTextField.get_value();
            var embedHtml = this._generateEmbedHtml(this._embedUrl, width, height, this._alternativeText);
            this._customizedEmbedTextField.set_value(embedHtml);
        }
        dialogBase.resizeToContent();
    },

    /* -------------------- private methods ----------- */
    // Generates the html code for both link and embed and sets it to the text boxes
    _initializeHtmlOutput: function (url, width, height, altText) {
        var link = this._getAbsoluteUrl(url);
        this._linkTextField.set_value(link);

        var embedHtml = this._generateEmbedHtml(url, width, height, altText);
        this._embedTextField.set_value(embedHtml);
    },

    // Generates the html code for embedding of the media
    _generateEmbedHtml: function (url, width, height, altText) {
        //sample format: <img width=""{0}"" height=""{1}"" src=""{2}"" alt=""{3}""/>"
        var template = this._embedStringTemplate;

        //If there is predefined _embedScriptTemplate we will use it instead of the default ones
        if (!template && this.get_mode() === "videos" && this._dataItem) {
			template = this.get_defaultHtml5EmbedStringTemplate();
        }

        if (this._isVectorGraphics() && template ) {
            if (!width) {
                template = template.replace(/width=["']{[\d]*}["'] /g, "");
            }
            
            if (!height) {
                template = template.replace(/height=["']{[\d]*}["'] /g, "");
            }
        }

        if (url && template) {
            var link = this._getAbsoluteUrl(url);
            return String.format(template, width, height, link, altText);
        }

        return "";
    },

    _textFieldElementClickHandler: function (e) {
        jQuery(e.target).select();
    },

    _getAbsoluteUrl: function (url) {
        if (url.startsWith("http://") || url.startsWith("https://"))
            return url;
        return this._domain + url;
    },

    _removeQueryStringParam: function (url, paramName, paramValue) {
        var components = url.split('?');
        if (components.length == 1) {
            return url;
        }

        url = components[0];
        var params = components[1].split('&');

        for (var i = 0; i < params.length; i++) {
            var nameValue = params[i].split('=');
            if (nameValue[0].toLowerCase() !== paramName.toLowerCase()
                || (paramValue !== undefined && nameValue[1] !== paramValue))
            {
                var sep = (url.indexOf('?') === -1) ? '?' : '&';
                url += sep + params[i];
            }
        }

        return url;
    },

    _isVectorGraphics: function () {
        var status = false;
        if (this._dataItem) {
            status = this._dataItem && this._dataItem.IsVectorGraphics === true
        }
        if (status == false && this._dataItemContext) {
            var additionalInfo = this._dataItemContext.SfAdditionalInfo;
            if (additionalInfo) {
                status = this._findValueByKey(additionalInfo, "IsVectorGraphics") === true;
            }
        }

        return status;
    },

    _findValueByKey: function (dictionary, key) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key)
                return item.Value;
        }

        return null;
    },

    /* -------------------- properties ---------------- */

    get_defaultHtml5EmbedStringTemplate: function () {
        return this._defaultHtml5EmbedStringTemplate;
    },
    set_defaultHtml5EmbedStringTemplate: function (value) {
        this._defaultHtml5EmbedStringTemplate = value;
    },

    get_rootPanel: function () {
        return this._rootPanel;
    },
    set_rootPanel: function (value) {
        this._rootPanel = value;
    },

    get_mode: function () {
        return this._mode;
    },
    set_mode: function (value) {
        this._mode = value;
    },

    // Gets the panel (DOM element) that contains the link and embed fields
    get_linkHtmlPanel: function () {
        return this._linkHtmlPanel;
    },
    // Sets the panel (DOM element) that contains the link and embed fields    
    set_linkHtmlPanel: function (value) {
        this._linkHtmlPanel = value;
    },

    // Gets the panel (DOM element) that contains the custom sizes radio buttons
    get_customizePanel: function () {
        return this._customizePanel;
    },
    // Sets the panel (DOM element) that contains the custom sizes radio buttons
    set_customizePanel: function (value) {
        this._customizePanel = value;
    },

    // Gets the panel (DOM element) that contains the custom width and height fields
    get_customSizesPanel: function () {
        return this._customSizesPanel;
    },
    // Sets the panel (DOM element) that contains the custom width and height fields
    set_customSizesPanel: function (value) {
        this._customSizesPanel = value;
    },

    // Gets the text box (text field control) that displays the media url
    get_linkTextField: function () {
        return this._linkTextField;
    },
    // Sets the text box (text field control) that displays the media url
    set_linkTextField: function (value) {
        this._linkTextField = value;
    },

    // Gets the text box (text field control) that displays the embed html
    get_embedTextField: function () {
        return this._embedTextField;
    },
    // Sets the text box (text field control) that displays the embed html
    set_embedTextField: function (value) {
        this._embedTextField = value;
    },

    // Gets the text box (text field control) that displays custom width
    get_customWidthTextField: function () {
        return this._customWidthTextField;
    },
    // Sets the text box (text field control) that displays custom width
    set_customWidthTextField: function (value) {
        this._customWidthTextField = value;
    },

    // Gets the text box (text field control) that displays custom height
    get_customHeightTextField: function () {
        return this._customHeightTextField;
    },
    // Sets the text box (text field control) that displays custom height
    set_customHeightTextField: function (value) {
        this._customHeightTextField = value;
    },

    // Gets the radio button list (choice field control) that displays different sizes
    get_sizesChoiceField: function () {
        return this._sizesChoiceField;
    },
    // Sets the radio button list (choice field control) that displays different sizes
    set_sizesChoiceField: function (value) {
        this._sizesChoiceField = value;
    },

    // Gets the text box (text field control) that displays the customized embed html code
    get_customizedEmbedTextField: function () {
        return this._customizedEmbedTextField;
    },
    // Sets the text box (text field control) that displays the customized embed html code
    set_customizedEmbedTextField: function (value) {
        this._customizedEmbedTextField = value;
    },

    // Gets the link button (DOM element) that expands the customization panel
    get_customizeButton: function () {
        return this._customizeButton;
    },
    // Sets the link button (DOM element) that expands the customization panel
    set_customizeButton: function (value) {
        this._customizeButton = value;
    },

    // Gets the link button (DOM element) that closes the customization panel
    get_closeButton: function () {
        return this._closeButton;
    },
    // Sets the link button (DOM element) that closes the customization panel
    set_closeButton: function (value) {
        this._closeButton = value;
    },

    // inherited from IRequiresDataItemContext
    set_dataItemContext: function (value) {
        this._dataItemContext = value;
        this.set_dataItem(value.Item);
    },

    set_dataItem: function (value, setDefaultValues) {
        this.reset();
        if (setDefaultValues)
            return;

        this._dataItem = value;

        /* Commented because of regression - bug #126807.
        // show the control only for published media items
        if (this._dataItem && this._dataItem.ApprovalWorkflowState) {
        var isPublished = this._dataItem.ApprovalWorkflowState.Value == "Published";
        jQuery(this._rootPanel).toggle(isPublished);
        }
        */

        if (this._dataItem.MediaUrl) {
            this._mediaUrl = this._removeQueryStringParam(this._dataItem.MediaUrl, 'Status', 'Master');
        }
        else {
            this._mediaUrl = this._dataItem.Url;
        }

        if (this._dataItem.EmbedUrl) {
            this._embedUrl = this._removeQueryStringParam(this._dataItem.EmbedUrl, 'Status', 'Master');
        }
        else {
            this._embedUrl = this._dataItem.Url;
        }

        if (this._mediaUrl) {
            this._mediaWidth = this._dataItem.Width;
            this._mediaHeight = this._dataItem.Height;

            this._originalMediaWidth = this._dataItem.Width;
            this._originalMediaHeight = this._dataItem.Height;
            if (typeof this._dataItem.AlternativeText === "object" && this._dataItem.AlternativeText && this._dataItem.AlternativeText.Value) {
                this._alternativeText = this._dataItem.AlternativeText.Value;
            }
            else if (typeof this._dataItem.AlternativeText === "string" && this._dataItem.AlternativeText) {
                this._alternativeText = this._dataItem.AlternativeText;
            }
            else if (this._dataItem.Title) {
                this._alternativeText = this._dataItem.Title.Value || this._dataItem.Title;
            }
            this._initializeHtmlOutput(this._embedUrl, this._mediaWidth, this._mediaHeight, this._alternativeText);

            var radioButtonList = this._sizesChoiceField.get_choiceElement();
            var originalRadioButton = jQuery(radioButtonList).find("input[value=original]");
            var radioLabel = jQuery(radioButtonList).find("label[for=" + originalRadioButton[0].id + "]");

            var pos = radioLabel.text().indexOf("(");
            var text = radioLabel.text();
            if (pos > 0) {
                text = text.substring(0, pos - 1);
            }

            var embedHtml = "";
            if (this._isVectorGraphics()) {
               embedHtml = this._generateEmbedHtml(this._embedUrl, null, null, this._alternativeText);
            } else {
                text = String.format("{0} ({1} x {2})", text, this._mediaWidth, this._mediaHeight);
                embedHtml = this._generateEmbedHtml(this._embedUrl, this._mediaWidth, this._mediaHeight, this._alternativeText);
            }

            radioLabel.text(text);

            this._customizedEmbedTextField.set_value(embedHtml);
        }
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControl", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext);