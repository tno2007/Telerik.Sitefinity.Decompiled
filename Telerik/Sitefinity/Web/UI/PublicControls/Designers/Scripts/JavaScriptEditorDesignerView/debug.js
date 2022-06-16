/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("JavaScriptEditorDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView = function(element) {
    Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView.initializeBase(this, [element]);

    this._parentDesigner = null;
    this._customJavaScriptCodeTextField = null;
    this._scriptEmbedPositionChoiceField = null;
    this._codeMirror = null;
}

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView.callBaseMethod(this, 'initialize');
        this._codeMirror = CodeMirror.fromTextArea(this._customJavaScriptCodeTextField.get_textElement(), {
            mode: "javascript",
            lineNumbers: true,
            matchBrackets: true,
            tabMode: "classic"
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (controlData.CustomJavaScriptCode) {
            var parentDesigner = this.get_parentDesigner();
            var tab = parentDesigner.get_menuTabStrip().findTabByValue("JavaScriptEditorDesignerView");
            tab.select();
            
            //this.get_customJavaScriptCodeTextField().set_value(controlData.CustomJavaScriptCode)
            this.get_CodeMirror().setValue(controlData.CustomJavaScriptCode);

            if (controlData.ScriptEmbedPosition) {
                this.get_scriptEmbedPositionChoiceField().set_value(controlData.ScriptEmbedPosition);
            }
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var parentDesigner = this.get_parentDesigner();
        var selectedTab = parentDesigner.get_menuTabStrip().get_selectedTab();
        var tabValue = selectedTab.get_value();

        // process only of the current view is the selected one
        if (tabValue == "JavaScriptEditorDesignerView") {
            var controlData = this.get_controlData();

            controlData.CustomJavaScriptCode = this.get_CodeMirror().getValue();
            controlData.Url = "";
            controlData.ScriptEmbedPosition = this.get_scriptEmbedPositionChoiceField().get_value();
        }

    },
    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

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

    // Gets the code mirror variable
    get_CodeMirror: function () {
        return this._codeMirror;
    },
    // Sets the code mirror variable
    set_CodeMirror: function (value) {
        this._codeMirror = value;
    },

    get_customJavaScriptCodeTextField: function () { return this._customJavaScriptCodeTextField; },
    set_customJavaScriptCodeTextField: function (value) { this._customJavaScriptCodeTextField = value; },

    get_scriptEmbedPositionChoiceField: function () { return this._scriptEmbedPositionChoiceField; },
    set_scriptEmbedPositionChoiceField: function (value) { this._scriptEmbedPositionChoiceField = value; }

}

Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views.JavaScriptEditorDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();