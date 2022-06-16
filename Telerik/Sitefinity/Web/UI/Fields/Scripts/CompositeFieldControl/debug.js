Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl = function(element) {
    this._title = null;
    this._example = null;
    this._description = null;
    this._titleElement = null;
    this._exampleElement = null;
    this._descriptionElement = null;
    this._fieldIds = [];
    this._displayMode = null;
    this._fieldName = null;

    Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.prototype =
 {
     initialize: function() {
         Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.callBaseMethod(this, "initialize");
     },

     dispose: function() {
         Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     /* -------------------- events -------------------- */

     /* -------------------- event handlers ------------ */

     /* -------------------- private methods ----------- */

     /* -------------------- properties ---------------- */

     // Gets the title text of the composite field control.
     get_title: function() {
         return this._title;
     },
     // Sets the title text of the composite field control.
     set_title: function(value) {
         this._title = value;
     },

     // Gets the text that is used as an example of the usage of the composite field control.
     get_example: function() {
         return this._example;
     },
     // Sets the text that is used as an example of the usage of the composite field control.
     set_example: function(value) {
         this._example = value;
     },

     // Gets the text that is used as a description of the composite field control.
     get_description: function() {
         return this._description;
     },
     // Sets the text that is used as a description of the composite field control.
     set_description: function(value) {
         this._description = value;
     },

     // Gets the reference to the DOM element used to display the title of the composite field control.
     get_titleElement: function() {
         return this.titleElement;
     },
     // Sets the reference to the DOM element used to displaye the title of the composite field control.
     set_titleElement: function(value) {
         this._titleElement = value;
     },

     // Gets the reference of the DOM element that is used to display the example.
     get_exampleElement: function() {
         return this._exampleElement;
     },
     // Sets the reference of the DOM element that is used to display the example.
     set_exampleElement: function(value) {
         this._exampleElement = value;
     },

     // Gets the reference of the DOM element that is used to display the description.
     get_descriptionElement: function() {
         return this._descriptionElement;
     },
     // Sets the reference of the DOM element that is used to display the description.
     set_descriptionElement: function(value) {
         this._descriptionElement = value;
     },
     // Gets the current display mode of the field control. One of the values of the
     // Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode enumeration.
     get_displayMode: function() {
         return this._displayMode;
     },
     // Sets the current display mode of the field control. One of the values of the
     // Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode enumeration.
     set_displayMode: function(value) {
         this._displayMode = value;
     },
     // Gets the field control ids.
     get_fieldIds: function() {
         return this._fieldIds;
     },
     // Sets the field control ids
     set_fieldIds: function(value) {
         this._fieldIds = value;
     },
     // Gets the name of the field
    get_fieldName: function () {	
        return this._fieldName;
    },
    // Sets the name of the field
    set_fieldName: function (value) {	
        this._fieldName = value;	
    }
 };

 Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl.registerClass("Telerik.Sitefinity.Web.UI.Fields.CompositeFieldControl", Sys.UI.Control, Telerik.Sitefinity.Web.UI.Fields.IField);