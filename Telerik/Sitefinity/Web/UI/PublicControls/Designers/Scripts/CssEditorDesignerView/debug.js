/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("CssEditorDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView.initializeBase(this, [element]);

    this._parentDesigner = null;
    this._customCssCodeTextField = null;
    this._mediaChoiceField = null;
    this._mediaTypesChoiceField = null;
    this._mediaChoiceFieldChangedDelegate = null;
    // keys is enum name, value is its int representation
    this._mediaTypesMap = {};
    this._codeMirror = null;
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView.callBaseMethod(this, 'initialize');

        this._mediaChoiceFieldChangedDelegate = Function.createDelegate(this, this._mediaChangeHandler);
        this._mediaChoiceField.add_valueChanged(this._mediaChoiceFieldChangedDelegate);
        this._codeMirror = CodeMirror.fromTextArea(this._customCssCodeTextField.get_textElement(), {
            mode: "css",
            lineNumbers: true,
            matchBrackets: true,
            tabMode: "classic"
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView.callBaseMethod(this, 'dispose');
        if (this._mediaChoiceFieldChangedDelegate) {
            if (this._mediaChoiceField) {
                this._mediaChoiceField.remove_valueChanged(this._mediaChoiceFieldChangedDelegate);
            }
            delete this._mediaChoiceFieldChangedDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        if (controlData.CustomCssCode) {
            var parentDesigner = this.get_parentDesigner();
            var tab = parentDesigner.get_menuTabStrip().findTabByValue("CssEditorDesignerView");
            tab.select();

            this.get_CodeMirror().setValue(controlData.CustomCssCode);

            if (controlData.MediaType == "all") {
                this.get_mediaChoiceField().set_value("all");
            }
            else {
                this.get_mediaChoiceField().set_value("selectMediaTypes");
                var selectedTypes = [];
                var typesArray = controlData.MediaType.split(",");
                for (var i = 0, length = typesArray.length; i < length; i++) {
                    var enumValue = this._mediaTypesMap[typesArray[i].trim()];
                    if (enumValue) {
                        selectedTypes.push(enumValue);
                    }
                }
                this.get_mediaTypesChoiceField().set_value(selectedTypes);
            }
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var parentDesigner = this.get_parentDesigner();
        var selectedTab = parentDesigner.get_menuTabStrip().get_selectedTab();
        var tabValue = selectedTab.get_value();

        // process only of the current view is the selected one
        if (tabValue == "CssEditorDesignerView") {
            var controlData = this.get_controlData();

            controlData.CustomCssCode = this.get_CodeMirror().getValue();
            controlData.Url = "";

            if (this.get_mediaChoiceField().get_value() == "all") {
                controlData.MediaType = 1; //all
            }
            else {
                var mediaType = this.get_mediaTypesChoiceField().get_value();
                if (Array.prototype.isPrototypeOf(mediaType)) {
                    // if multiple types are selected we convert it to a bit mask
                    var mask = 0;
                    for (var i = 0, length = mediaType.length; i < length; i++) {
                        var flag = parseInt(mediaType[i]);
                        mask = mask | flag;
                    }
                    controlData.MediaType = mask;
                }
                else {
                    controlData.MediaType = mediaType;
                }
            }

        }
    },
    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */
    // Handles selection change in the media choice field
    _mediaChangeHandler: function (sender, args) {
        var value = sender.get_value();
        if (value == "all") {
            jQuery(this._mediaTypesChoiceField.get_element()).hide();
        }
        else {
            jQuery(this._mediaTypesChoiceField.get_element()).show();
        }
        dialogBase.resizeToContent();
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    // Gets the text filed that holds the CSS styles
    get_customCssCodeTextField: function () {
        return this._customCssCodeTextField;
    },
    // Sets the text filed that holds the CSS styles
    set_customCssCodeTextField: function (value) {
        this._customCssCodeTextField = value;
    },

    // Gets the choices between all media types and specific ones
    get_mediaChoiceField: function () {
        return this._mediaChoiceField;
    },
    // Sets the choices between all media types and specific ones
    set_mediaChoiceField: function (value) {
        this._mediaChoiceField = value;
    },

    // Gets the media type choices
    get_mediaTypesChoiceField: function () {
        return this._mediaTypesChoiceField;
    },
    // Sets the media type choices
    set_mediaTypesChoiceField: function (value) {
        this._mediaTypesChoiceField = value;
    },

    // Gets the code mirror variable
    get_CodeMirror: function () {
        return this._codeMirror;
    },
    // Sets the code mirror variable
    set_CodeMirror: function (value) {
        this._codeMirror = value;
    }
};

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.CssEditorDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();