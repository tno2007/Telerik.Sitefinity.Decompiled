Type.registerNamespace("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail");

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl = function (element) {
    Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl.initializeBase(this, [element]);

    this._element = element;
    this._wrapperElement = null;
    this._fieldControlClientIDs = [];
    this._expandableExtenderBehavior = null;
    this._name = null;
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl.prototype =
 {
     initialize: function () {
         Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl.callBaseMethod(this, "initialize");         
     },

     dispose: function () {
         Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl.callBaseMethod(this, "dispose");
     },

     // This function allows other objects to subscribe to the doToggle event of the field control.
     add_doToggle: function (delegate) {
         this.get_events().addHandler('doToggle', delegate);
     },
     // This function allows other objects to unsubscribe from the doToggle event of the field control.
     remove_doToggle: function (delegate) {
         this.get_events().removeHandler('doToggle', delegate);
     },
     // This function allows other objects to subscribe to the reset event of the field control.
     add_reset: function (delegate) {
         this.get_events().addHandler('reset', delegate);
     },
     // This function allows other objects to unsubscribe from the reset event of the field control.
     remove_reset: function (delegate) {
         this.get_events().removeHandler('reset', delegate);
     },

     /* --------------------  public methods --------------------  */

     configureExpandableSection: function () {
         var behavior = this._get_ExpandableExtenderBehavior();
         var expandSection = false;
         for (var i = 0, length = this._fieldControlClientIDs.length; i < length; i++) {
             var control = $find(this._fieldControlClientIDs[i]);
             
             if (control) {
                 if (Object.getType(control).implementsInterface(Telerik.Sitefinity.Web.UI.Fields.IRelatedDataField)) {
                     //TODO/6: configure separate sections for related data fields
                     expandSection = false;
                 }
                 else {
                     var value = control.get_value();
                     var defaultValue = control.get_defaultValue();
                     var isArray = Array.prototype.isPrototypeOf(value);

                     if (isArray) {
                         if (defaultValue != null && value.compareArrays(defaultValue) == false) {
                             expandSection = true;
                         }
                     }
                     else {
                         if (value != defaultValue) {
                             expandSection = true;
                         }
                     }

                     if (expandSection) {
                         break;
                     }
                 }
             }
         } // end for

         if (expandSection) {
             behavior.expandSection();
         }
         else {
             // Check if section is collapsed by default and collapse it
             if (!behavior.get_originalExpandedState()) {
                 behavior.collapseSection();
             }
         }


     },


     reset: function () {
         this._resetSectionHandler();
     },

     toggle: function (args) {
         this._doToggleHandler(args);
         //fixes an issue with #259996 in TWU 2014 Q1 SP1 
         for (var i = 0, length = this._fieldControlClientIDs.length; i < length; i++) {
             var control = $find(this._fieldControlClientIDs[i]);
             if (Object.getTypeName(control) == "Telerik.Sitefinity.Web.UI.Fields.HtmlField" && control._editControl && jQuery.browser.chrome) {
                 try {
                     control._editControl.repaint();
                 } catch (e) {
                     console.log("Could not execute repaint() of the RadEditor control." + e.message);
                 }
               
             }
         }
     },

     /* -------------------- events -------------------- */

     // This function will rise the doToggle event.
     _doToggleHandler: function (args) {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('doToggle');
             if (h) h(this, args);
             return args;
         }
     },

     // This function will rise the reset event.
     _resetSectionHandler: function () {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('reset');
             if (h) h(this, Sys.EventArgs.Empty);
             return Sys.EventArgs.Empty;
         }
     },
     
     /* -------------------- event handlers -------------------- */

     /* -------------------- private methods ----------- */

     _get_ExpandableExtenderBehavior: function () {
         if (this._expandableExtenderBehavior) {
             return this._expandableExtenderBehavior;
         }
         this._expandableExtenderBehavior = Sys.UI.Behavior.getBehaviorByName(this._element, "ExpandableExtender");
         return this._expandableExtenderBehavior;
     },

     /* -------------------- properties ---------------- */

     get_fieldControlIds: function () {
         return this._fieldControlClientIDs;
     },

     set_fieldControlIds: function (value) {
         this._fieldControlClientIDs = value;
     },
     get_wrapperElement: function () {
         return this._wrapperElement;
     },

     set_wrapperElement: function (value) {
         this._wrapperElement = value;
     },

     get_name: function () {
         return this._name;
     },
     set_name: function (value) {
         this._name = value;
     }
 };

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.SectionControl", Sys.UI.Control);
