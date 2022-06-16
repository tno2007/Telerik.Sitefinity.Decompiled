
Type.registerNamespace("Telerik.Sitefinity.Modules.GenericContent.Web.UI");

// ------------------------------------------------------------------------
// ContentBlockDesignerBase class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase = function (element) {
    this._propertyEditor = null;
    this._htmlEditor = null;
    Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    applyChanges: function () {
        var editor = this._htmlEditor._editControl;
        //trigger switch from "html" to "design" in order filters to be applied.
        if (editor) {
            editor.set_mode(1);
        }

        this.get_propertyEditor().get_control().Html = this._htmlEditor.get_value();
    },

    refreshUI: function () {
        var html = this.get_propertyEditor().get_control().Html;
        if (html) {
            this._htmlEditor.set_value(html);
        }
    },

    /* ************************* properties ************************* */
    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },
    // gets the reference to the rad editor control used to edit the
    // html content of the ContentBlock control
    get_htmlEditor: function () {
        return this._htmlEditor;
    },
    // gets the reference to the rad editor control used to edit the
    // html content of the ContentBlock control
    set_htmlEditor: function (value) {
        this._htmlEditor = value;
    }
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase.registerClass('Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);