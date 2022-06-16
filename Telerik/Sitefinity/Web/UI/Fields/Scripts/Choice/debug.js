Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.Choice = function() {
    this._text = null;
    this._value = null;
    this._description = null;
    this._enabled = null;
    Telerik.Sitefinity.Web.UI.Fields.Choice.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Fields.Choice.prototype =
 {
     initialize: function() {
         Telerik.Sitefinity.Web.UI.Fields.Choice.callBaseMethod(this, "initialize");
     },

     dispose: function() {
         Telerik.Sitefinity.Web.UI.Fields.Choice.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     /* -------------------- events -------------------- */

     /* -------------------- event handlers ------------ */

     /* -------------------- private methods ----------- */

     /* -------------------- properties ---------------- */

     // Gets the value that represents the text of the choice.
     get_text: function() {
         return this._text;
     },

     // Sets the value that represents the text of the choice.
     set_text: function(value) {
         this._text = value;
     },

     // Gets the value that represents the value of the choice.
     get_value: function() {
         return this._value;
     },

     // Sets the value that represents the value of the choice.
     set_value: function(value) {
         this._value = value;
     },

     // Gets the value that represents the descriptions of the choice.
     get_description: function() {
         return this._description;
     },

     // Sets the value that represents the description of the choice.
     set_description: function(value) {
         this._description = value;
     },

     // Gets the value that determines whether the choice is enabled or not. If enabled true;
     // otherwise false.
     get_enabled: function() {
         return this._enabled;
     },

     // Sets the value that determines whether the choice is enabled or not. If enabled true;
     // otherwise false.
     set_enabled: function(value) {
         this._enabled = value;
     }
 };
 
Telerik.Sitefinity.Web.UI.Fields.Choice.registerClass("Telerik.Sitefinity.Web.UI.Fields.Choice", Sys.Component);