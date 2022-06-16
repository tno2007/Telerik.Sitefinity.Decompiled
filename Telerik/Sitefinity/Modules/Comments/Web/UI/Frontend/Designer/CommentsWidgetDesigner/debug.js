Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner = function (element) {

    this._commentsTemplatesSelector = null;
    this._commentsListViewTemplateSelector = null;
    this._commentsSubmitFormTemplateSelectorControl = null;
    this._commentsSubmitFormTemplateKeyName = "ViewTemplateKey";
    this._commentsListViewTemplateKeyName = "ListViewTemplateKey";
    /* Calls the base constructor */
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        /* Here you can attach to events or do other initialization */
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.callBaseMethod(this, 'initialize');

    },
    dispose: function () {
        /* this is the place to unbind/dispose the event handlers created in the initialize method */
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    findElement: function (id) {
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        return result;
    },

    /* Called when the designer window gets opened and here is place to "bind" your designer to the control properties */
    refreshUI: function () {
        var controlData = this._propertyEditor.get_control(); /* JavaScript clone of your control - all the control properties will be properties of the controlData too */

        /* RefreshUI Message */
        this.get_commentsTemplatesSelector().refreshUI();
        this.get_commentsListViewTemplateSelector().refreshUI(this._commentsListViewTemplateKeyName);
        this.get_commentsSubmitFormTemplateSelectorControl().refreshUI(this._commentsSubmitFormTemplateKeyName);
    },

    /* Called when the "Save" button is clicked. Here you can transfer the settings from the designer to the control */
    applyChanges: function () {
        var controlData = this._propertyEditor.get_control();

        /* ApplyChanges Message */
        this.get_commentsTemplatesSelector().applyChanges();
        this.get_commentsListViewTemplateSelector().applyChanges(this._commentsListViewTemplateKeyName);
        this.get_commentsSubmitFormTemplateSelectorControl().applyChanges(this._commentsSubmitFormTemplateKeyName);
    },

    /* --------------------------------- properties -------------------------------------- */
    get_commentsTemplatesSelector: function () { return this._commentsTemplatesSelector; },
    set_commentsTemplatesSelector: function (value) { this._commentsTemplatesSelector = value; },
    get_commentsListViewTemplateSelector: function () { return this._commentsListViewTemplateSelector; },
    set_commentsListViewTemplateSelector: function (value) { this._commentsListViewTemplateSelector = value; },
    get_commentsSubmitFormTemplateSelectorControl: function () { return this._commentsSubmitFormTemplateSelectorControl; },
    set_commentsSubmitFormTemplateSelectorControl: function (value) { this._commentsSubmitFormTemplateSelectorControl = value; },


}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner.registerClass('Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.Designer.CommentsWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
