/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js" assembly="Telerik.Sitefinity"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.FieldControl = function (element) {
    this._element = element;
    this._dataFieldName = null;
    this._dataFormatString = null;
    this._description = null;
    this._descriptionElement = null;
    this._displayMode = null;
    this._example = null;
    this._exampleElement = null;
    this._title = null;
    this._titleElement = null;
    this._validatorDefinition = null;
    this._value = null;
    this._defaultValue = null;
    this._isBinding = false;
    this._fieldName = null;
    this._validator = null;

    this._valueChangedDelegate = null;

    this._isViolationElementPositioned = false;
    this._controlErrorCssClass = "";
    this._uiCulture = null;
    this._culture = null;

    this._emptyGuid = "00000000-0000-0000-0000-000000000000";
    this._dataContext = null;

    Telerik.Sitefinity.Web.UI.Fields.FieldControl.isValidationMessagedFocused = false;
    Telerik.Sitefinity.Web.UI.Fields.FieldControl.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.FieldControl.prototype =
 {
     /* --------------------  set up and tear down ----------- */

     initialize: function () {
         if (this._valueChangedDelegate == null) {
             this._valueChangedDelegate = Function.createDelegate(this, this._valueChangedHandler);
         }
         if (this._validatorDefinition) {
             this._validatorDefinition = Sys.Serialization.JavaScriptSerializer.deserialize(this._validatorDefinition);
             this._validator = new Telerik.Sitefinity.Web.UI.Validation.Validator(this._validatorDefinition);
         }
         if (this._controlErrorCssClass == null) {
             this._controlErrorCssClass = "";
         }


         Telerik.Sitefinity.Web.UI.Fields.FieldControl.callBaseMethod(this, "initialize");
     },

     dispose: function () {
         if (this._valueChangedDelegate != null) {
             delete this._valueChangedDelegate;
         }

         Telerik.Sitefinity.Web.UI.Fields.FieldControl.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     // This function allows other objects to subscribe to the valueChanged event of the field control.
     add_valueChanged: function (delegate) {
         this.get_events().addHandler('valueChanged', delegate);
     },
     // This function allows other objects to unsubscribe from the valueChanged event of the field control.
     remove_valueChanged: function (delegate) {
         this.get_events().removeHandler('valueChanged', delegate);
     },
     // This function allows other objects to subscribe to the reset event of the field control.
     add_reset: function (delegate) {
         this.get_events().addHandler('reset', delegate);
     },
     // This function allows other objects to unsubscribe from the reset event of the field control.
     remove_reset: function (delegate) {
         this.get_events().removeHandler('reset', delegate);
     },
     // This function allows other objects to subscribe to the doExpand event of the field control.
     add_doExpand: function (delegate) {
         this.get_events().addHandler('doExpand', delegate);
     },
     // This function allows other objects to unsubscribe from the doExpand event of the field control.
     remove_doExpand: function (delegate) {
         this.get_events().removeHandler('doExpand', delegate);
     },
     // This function allows other objects to subscribe to the doCollapse event of the field control.
     add_doCollapse: function (delegate) {
         this.get_events().addHandler('doCollapse', delegate);
     },
     // This function allows other objects to unsubscribe from the doCollapse event of the field control.
     remove_doCollapse: function (delegate) {
         this.get_events().removeHandler('doCollapse', delegate);
     },
     // This function allows other objects to subscribe to the dataBound event of the field control.
     add_dataBound: function (delegate) {
         this.get_events().addHandler('dataBound', delegate);
     },
     // This function allows other objects to unsubscribe from the dataBound event of the field control.
     remove_dataBound: function (delegate) {
         this.get_events().removeHandler('dataBound', delegate);
     },
     // Resets the field control in its initial states and clears the value.
     reset: function () {
         // remove all validation messages
         this._clearViolationMessage();
         // just raise the event - let the derivates implement this
         this._resetHandler();
     },

     // Validates the value of this control.
     validate: function () {
         if (this._validator && this._isToValidate()) {
             var isValid = this._validator.validate(this.get_value());
             this._refreshViolationMessage(isValid);

             return isValid;
         }

         this._refreshViolationMessage(true);
         return true;
     },
     // Raises dataBound event when the item is bound
     raise_dataBound: function () {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('dataBound');
             if (h) h(this, Sys.EventArgs.Empty);
             return Sys.EventArgs.Empty;
         }
     },
     // Focuses the a control depending on tab or shif-tab key pressed
     focusControlByTabKey: function (keyDownEventArgs) {

         if (keyDownEventArgs.keyCode == Sys.UI.Key.tab) {

             var currentTabIndex = parseInt(this.get_tabIndex());
             var direction = 1;
             // Shift + tab moves back
             var element = null;
             if (!currentTabIndex) {
                 return;
             }
             if (keyDownEventArgs.shiftKey) {
                 direction = -1;
                 element = this.findPreviousElementByTabIndex(currentTabIndex);
             }
             else {
                 element = this.findNextElementByTabIndex(currentTabIndex);
             }

             if (element) {
                 element.focus();
                 if (keyDownEventArgs.stopPropagation) {
                     keyDownEventArgs.stopPropagation();
                 }

                 if (keyDownEventArgs.preventDefault) {
                     keyDownEventArgs.preventDefault();
                 }
             }
         }
     },

     // Returns the dom element by the specified tabIndex, used in keoyboard navigation
     findElementByTabIndex: function (tabIndex) {
         if (tabIndex > 0) {
             var element = jQuery(this._element).parents().find("[tabIndex='" + tabIndex + "']:visible");
             if (element.length > 0) {
                 return element[element.length - 1];
             }
         }
         return null;
     },
     findNextElementByTabIndex: function (tabIndex) {

         var closestTabIndex = Infinity;

         var elements = jQuery(this._element).parents().find(":visible").filter(function () {
             var ti = $(this).attr("tabIndex");
             if (ti) {
                 ti = parseInt(ti);
                 if (ti > tabIndex && ti <= closestTabIndex) {
                     closestTabIndex = ti;
                     return true;
                 }
             }
             return false;

         });
         if (elements && elements.length > 0) {
             // Return the "closest" one, the first found
             return elements[0];
         }
         return null;
     },
     findPreviousElementByTabIndex: function (tabIndex) {
         var closestTabIndex = -Infinity;
         var elements = jQuery(this._element).parents().find(":visible").filter(function () {
             var ti = $(this).attr("tabIndex");
             if (ti) {
                 ti = parseInt(ti);
                 if (ti < tabIndex && ti >= closestTabIndex && ti >= 1) {
                     closestTabIndex = ti;
                     return true;
                 }
             }
             return false;
         });
         if (elements && elements.length > 0) {
             //return the last one, which is the last one before the current one
             return elements[elements.length - 1];
         }
         return null;
     },

     /* -------------------- events -------------------- */

     // This function will rise valueChanged event.
     _valueChangedHandler: function () {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('valueChanged');
             if (h) h(this, Sys.EventArgs.Empty);
             return Sys.EventArgs.Empty;
         }
     },

     // This function will rise the reset event.
     _resetHandler: function () {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('reset');
             if (h) h(this, Sys.EventArgs.Empty);
             return Sys.EventArgs.Empty;
         }
     },

     // This function will rise the doExpand event.
     _doExpandHandler: function () {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('doExpand');
             if (h) h(this, Sys.EventArgs.Empty);
             return Sys.EventArgs.Empty;
         }
     },

     // This function will rise the doCollapse event.
     _doCollapseHandler: function () {
         if (typeof this.get_events == 'function') {
             var h = this.get_events().getHandler('doCollapse');
             if (h) h(this, Sys.EventArgs.Empty);
             return Sys.EventArgs.Empty;
         }
     },

     /* -------------------- event handlers ------------ */

     /* -------------------- private methods ----------- */
     _clearViolationMessage: function () {
         var element = jQuery(this.get_element()).find("." + this._getViolationMessageClassName());
         if (element.length > 0) {
             //element.remove();
             element.hide();
         }
     },

     _showViolationMessageElement: function (element) {
         if (!this._isViolationElementPositioned) {
             this.get_element().appendChild(element);
             this._isViolationElementPositioned = true;
         }
         element.style.display = '';
         jQuery(element).addClass(this._getViolationMessageClassName());

         var fieldControl = Telerik.Sitefinity.Web.UI.Fields.FieldControl;

         //Check to see if this is the first control that has an error and give focus to it
         if (!fieldControl.isValidationMessagedFocused) {
             fieldControl.isValidationMessagedFocused = true;
             jQuery("html, body").scrollTop(element.offsetTop);
         }
     },

     _refreshViolationMessage: function (isValid) {
         if (this._validator) {
             var addWrappingErrorCssClass = this._controlErrorCssClass.length > 0;
             var violationMessageElement = this._validator.get_violationMessageElement();
             if (isValid) {
                 if (addWrappingErrorCssClass) {
                     jQuery(this._element).removeClass(this._controlErrorCssClass);
                 }

                 violationMessageElement.style.display = 'none';
             }
             else {
                 // We add the wrapping css class if set                 
                 if (addWrappingErrorCssClass) {
                     if (!jQuery(this._element).hasClass(this._controlErrorCssClass)) {
                         jQuery(this._element).addClass(this._controlErrorCssClass);
                     }
                 }

                 this._expandParentSections(this._element);
                 this._showViolationMessageElement(violationMessageElement);
             }
         }
     },

     _getViolationMessageClassName: function () {
         return "violationMessage" + this.get_id();
     },

     _isToValidate: function () {
         if ((!this._validator._required && !this.get_value())) {
             return false
         }
         else {
             return (this._validator.get_validateIfInvisible() || $(this._element).is(":visible")) &&
                this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write;
         }
     },

     //Expand all parent sections so that the error message can be made visible.
     _expandParentSections: function (element) {

         var elementParent = jQuery(element).parent();

         while (elementParent.length != 0) {

             if (jQuery(elementParent).hasClass("sfExpandableForm")) {
                 if (!jQuery(elementParent).hasClass("sfExpandedForm")) {
                     jQuery(elementParent).addClass("sfExpandedForm");
                 }

                 var collapsedElement = jQuery(elementParent).find(".sfCollapsedTarget");
                 if (collapsedElement.length != 0) {
                     collapsedElement.removeClass("sfCollapsedTarget");
                     collapsedElement.addClass("sfExpandedTarget");
                 }
             }
             elementParent = elementParent.parent();
         }
     },
     /* -------------------- properties ---------------- */

     // Returns true if the value of the field is changed
     isChanged: function () {
         var notChanged = (this._value == this.get_value());
         if (notChanged) {
             return false;
         } else {
             return true;
         }

     },
     hasAttribute: function (value) {
         var attr = jQuery(this._element).attr(value);
         if (typeof attr !== 'undefined' && attr !== false) {
             return true;
         }
         return false;
     },
     getAttributeValue: function (value) {
         var result = jQuery(this._element).attr(value);
         if (result) {
             return result;
         }
         return '';
    },

     // Gets the name of the date field to which the field is bound to.
     get_dataFieldName: function () {
         return this._dataFieldName;
     },
     // Sets the name of the data field to which the component is bound to.
     set_dataFieldName: function (value) {
         this._dataFieldName = value;
     },
     // Gets the data format string that should be applied to the value in read mode.
     get_dataFormatString: function () {
         return this._dataFormatString;
     },
     // Sets the data format string that should be applied to the value in read mode.
     set_dataFormatString: function (value) {
         this._dataFormatString = value;
     },
     // Gets the text that is used as a description of the field control.
     get_description: function () {
         return this._description;
     },
     // Sets the text that is used as a description of the field control.
     set_description: function (value) {
         this._description = value;
         if (this._descriptionElement) {
             this._descriptionElement.innerHTML = this._description;
         }
     },
     // Gets the reference of the DOM element that is used to display the description.
     get_descriptionElement: function () {
         return this._descriptionElement;
     },
     // Sets the reference of the DOM element that is used to display the description.
     set_descriptionElement: function (value) {
         this._descriptionElement = value;
     },
     // Gets the current display mode of the field control. One of the values of the
     // Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode enumeration.
     get_displayMode: function () {
         return this._displayMode;
     },
     // Sets the current display mode of the field control. One of the values of the
     // Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode enumeration.
     set_displayMode: function (value) {
         this._displayMode = value;
     },
     // Gets the text that is used as an example of the usage of the field control.
     get_example: function () {
         return this._example;
     },
     // Sets the text that is used as an example of the usage of the field control.
     set_example: function (value) {
         this._example = value;
         if (this._exampleElement) {
             this._exampleElement.innerHTML = this._example;
         }
     },
     // Gets the reference of the DOM element that is used to display the example.
     get_exampleElement: function () {
         return this._exampleElement;
     },
     // Sets the reference of the DOM element that is used to display the example.
     set_exampleElement: function (value) {
         this._exampleElement = value;
     },
     // Gets the title text of the field control.
     get_title: function () {
         return this._title;
     },
     // Sets the title text of the field control.
     set_title: function (value) {
         this._title = value;
         if (this._titleElement) {
             this._titleElement.innerHTML = this._title;
         }
     },
     // Gets the reference to the DOM element used to display the title of the field control.
     get_titleElement: function () {
         return this._titleElement;
     },
     // Sets the reference to the DOM element used to displaye the title of the field control.
     set_titleElement: function (value) {
         this._titleElement = value;
     },
     // Gets an instance of client side validation definition object used to instantiate client side validation component in order to validate
     // the value of the field control.
     get_validatorDefinition: function () {
         return this._validatorDefinition;
     },
     // Sets an instance of client side validation definition object used to instantiate client side validation component in order to validate
     // the value of the field control.
     set_validatorDefinition: function (value) {
         this._validatorDefinition = value;
     },
     // Gets the validator object in order to manipulate validations on the client side
     get_validator: function () {
         return this._validator;
     },
     // Sets the validator object in order to manipulate validations on the client side
     set_validator: function (value) {
         this._validator = value;
     },
     // Gets the isBinding flag
     get_isBinding: function () {
         return this._isBinding;
     },
     // Sets the isBinding flag
     set_isBinding: function (value) {
         this._isBinding = value;
     },
     // Gets the value of the field control.
     get_value: function () {
         return this._value;
     },
     // Sets the value of the field control. [Optional] sends the whole data item to the field control, in
     // case other properties of the data item are needed.
     set_value: function (value) {
         this._value = value;
     },
     // Gets the default value of the field control.
     get_defaultValue: function () {
         return this._defaultValue;
     },
     // Sets the default value of the field control.
     set_defaultValue: function (value) {
         this._defaultValue = value;
     },

     // Gets the position in the tabbing order
     get_tabIndex: function () {
         var result = jQuery(this._element).attr("tabIndex");
         if (result) {
             return result;
         }
         return -1; // tabIndex not set

     },
     // Sets the position in the tabbing order
     // Should be overridden from the ancestors in order to set it to the proper input element
     set_tabIndex: function (value) {
         jQuery(this._element).attr("tabIndex", value);
     },
     // Default blur behaviour for the control - if expandable - collapse if no value, etc.
     blur: function () { },
     // Default forcus beheviour for the control - should put the focus on the default element
     focus: function () { },

     // Resets the validation messages
     clearViolationMessage: function () {
         this._clearViolationMessage();
     },

     // Returns the violation messages generated upon validation
     get_violationMessages: function () {
         return this._validator.get_violationMessages();
     },

     // Returns the validator used by this field control
     get_validator: function () {
         return this._validator;
     },

     // Gets the CssClass that is added to the whole control on error
     get_controlErrorCssClass: function () { return this._controlErrorCssClass; },
     // Sets the CssClass that is added to the whole control on error
     set_controlErrorCssClass: function (value) { this._controlErrorCssClass = value; },

     // Specifies the ui culture to be used by the control
     set_uiCulture: function (culture) {
         this._uiCulture = culture;
     },

     // Gets the ui culture used by the control
     get_uiCulture: function () {
         return this._uiCulture;
     },

     // Sets the culture culture to be used by the control
     set_culture: function (culture) {
         this._culture = culture;
     },
     // Gets the culture used by the control
     get_culture: function () {
         return this._culture;
     },
     // Gets the name of the field
     get_fieldName: function () {
         return this._fieldName;
     },
     // Sets the name of the field
     set_fieldName: function (value) {
         this._fieldName = value;
     },
     get_dataContext: function () {
         return this._dataContext;
     },
     set_dataContext: function (value) {
         this._dataContext = value;
    },

    get_element: function () {
        return this._element;
    }
 };

Telerik.Sitefinity.Web.UI.Fields.FieldControl.registerClass("Telerik.Sitefinity.Web.UI.Fields.FieldControl", Sys.UI.Control, Telerik.Sitefinity.Web.UI.Fields.IField);