/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ChoiceField = function (element) {
    this._element = element;
    this._choices = [];
    this._mutuallyExclusive = null;
    this._renderChoicesAs = null;
    this._selectedChoicesIndex = [];
    this._choiceElement = null;
    this._readModeLabel = null;
    // map - list item - choice value used when in checkbox mode
    this._listItemValueMap = null;
    this._valueChangedHandlerDelegate;
    this._returnValuesAlwaysInArray = null;

    Telerik.Sitefinity.Web.UI.Fields.ChoiceField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.ChoiceField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ChoiceField.callBaseMethod(this, "initialize");

        //add value attribute to all ListItems when in CheckBoxes mode
        if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.CheckBoxes) {
            this._setCheckBoxesValueMapping();
        }

        this._valueChangedHandlerDelegate = Function.createDelegate(this, this._valueChangedHandler);
        // attaching the event handlers
        this._subscribeForValueChanged();
        this._value = this.get_value();
        jQuery(this._choiceElement).on("unload",
            function (e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
    },

    dispose: function () {
        this.choices = null;
        this._choiceElement = null;
        if (this._valueChangedHandlerDelegate) {
            delete this._valueChangedHandlerDelegate;
        }
        Telerik.Sitefinity.Web.UI.Fields.ChoiceField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // TODO: Text always goes first, then value. 
    addListItem: function (value, caption) {
        if (this._choiceElement) {
            var selector = this._get_listItemSelector();
            jQuery(this._choiceElement).append("<" + selector + " value='" + value + "'>" + caption + "</" + selector + ">");
        }
    },

    clearListItems: function () {
        if (this._choiceElement) {
            jQuery(this._choiceElement).children().remove();
        }
    },

    reset: function () {
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this.set_value(null);
        }
        else {
            this.set_value("");
        }
        Telerik.Sitefinity.Web.UI.Fields.ChoiceField.callBaseMethod(this, "reset");
    },

    selectListItemsByValue: function (value) {
        this._selectListItemsByValue(value);
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */

    _subscribeForValueChanged: function () {
        switch (this._renderChoicesAs) {
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.CheckBoxes:
                if (this._get_choiceListItems()) {
                    this._get_choiceListItems().click(this._valueChangedHandlerDelegate);
                }
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown:
                if (this._choiceElement) {
                    jQuery(this._choiceElement).change(this._valueChangedHandlerDelegate);
                }
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.ListBox:
                if (this._choiceElement) {
                    jQuery(this._choiceElement).change(this._valueChangedHandlerDelegate);
                }
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.HorizontalRadioButtons:
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons:
                if (this._get_choiceListItems()) {
                    this._get_choiceListItems().click(this._valueChangedHandlerDelegate);
                }
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox:
                if (this._choiceElement) {
                    jQuery(this._choiceElement).click(this._valueChangedHandlerDelegate);
                }
                break;
        }
    },

    _unsubscribeForValueChanged: function () {
        switch (this._renderChoicesAs) {
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.CheckBoxes:
                if (this._get_choiceListItems()) {
                    this._get_choiceListItems().unbind("click", this._valueChangedHandlerDelegate);
                }
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown:
                if (this._choiceElement) {
                    jQuery(this._choiceElement).unbind("change", this._valueChangedHandlerDelegate);
                }
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.ListBox:
                if (this._choiceElement) {
                    jQuery(this._choiceElement).unbind("change", this._valueChangedHandlerDelegate);
                }
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.HorizontalRadioButtons:
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons:
                if (this._get_choiceListItems()) {
                    this._get_choiceListItems().unbind("click", this._valueChangedHandlerDelegate);
                }
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox:
                if (this._choiceElement) {
                    jQuery(this._choiceElement).unbind("click", this._valueChangedHandlerDelegate);
                }
                break;
        }
    },

    // Returns the jQuery selector that specfies the attribute which holds the selected/checed state
    _get_selectedAttributeSelector: function () {
        switch (this._renderChoicesAs) {
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.CheckBoxes:
                return "checked";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown:
                return "selected";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.ListBox:
                return "selected";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.HorizontalRadioButtons:
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons:
                return "checked";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox:
                return "checked";
        }
    },

    // Returns the jQuery selector that selects the list items for the specific choice control
    _get_listItemSelector: function () {
        switch (this._renderChoicesAs) {
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox:
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.CheckBoxes:
                return "input:checkbox";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown:
                return "option";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.ListBox:
                return "option";
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.HorizontalRadioButtons:
            case Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons:
                return "input:radio";
        }
    },

    // Returns the values of the selected list items
    _get_selectedItemsValues: function () {
        var result;
        if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox) {
            if (this._choiceElement.checked) {
                result = ["true"];
            }
            else {
                result = ["false"];
            }
        }
        else {
            var selected = this._get_selectedListItemsElements();
            result = [];
            if (selected && selected.length > 0) {
                for (var i = 0, length = selected.length; i < length; i++) {
                    result.push(jQuery(selected[i]).attr("value"));
                }
            }
        }
        return result;
    },

    // Returns the selected list item's DOM elements
    _get_selectedListItemsElements: function () {
        var result = this._get_choiceListItems(this._get_selectedAttributeSelector());
        return result;
    },

    // Finds the DOM list item element by its value attribute
    _get_listItemByValue: function (value) {
        return jQuery(this._choiceElement).find(this._get_listItemSelector()).filter(function () {
            return jQuery(this).val() == value;
        });
    },

    // Finds the DOM list item element by its text
    _get_listItemByText: function (text) {
        var selector = this._get_listItemSelector() + "[text='" + text + "']"
        return jQuery(this._choiceElement).find(selector);
    },

    // Gets the list items for the specific choice control
    _get_choiceListItems: function (attribute) {

        if (attribute) {
            attribute = ":" + attribute
        }
        else {
            attribute = "";
        }

        var itemSelector = this._get_listItemSelector();
        return jQuery(this._choiceElement).find(itemSelector + attribute);

    },

    // Clears all selected elements
    _clearAllSelectedItems: function () {
        if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox) {
            this._choiceElement.checked = false;
        }
        else {
            var selector = this._get_selectedAttributeSelector();
            jQuery(this._choiceElement).find(this._get_listItemSelector()).prop(selector, false);

            if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.RadioButtons
                || this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.HorizontalRadioButtons
                || this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.DropDown) {
                this._selectFirstChoice();
            }
        }
    },

    // Selects/checks a specific list item by the specified value
    _selectListItemsByValue: function (value) {
        var selectedAttribute = this._get_selectedAttributeSelector();

        if (Array.prototype.isPrototypeOf(value)) {
            for (var i = 0, length = value.length; i < length; i++) {
                var item = this._get_listItemByValue(value[i]);
                if (item) {
                    jQuery(item).prop(selectedAttribute, true);
                }
            }
        }
        else {
            var item;
            if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox) {
                // in this mode there's only one item that can be returned and its value depends on the checked attribute,
                // so the _get_listItemByValue() doesn't work
                item = jQuery(this._choiceElement);
                if (item.length) {
                    item.attr(selectedAttribute, (value === true || value === "true"));
                }
            }
            else {
                item = this._get_listItemByValue(value);
                if (item.length) {
                    jQuery(item).prop(selectedAttribute, true);
                }
            }
        }
    },

    _selectFirstChoice: function () {
        var listItems = jQuery(this._choiceElement).find(this._get_listItemSelector());
        if (listItems && listItems.length > 0) {
            var firstItem = listItems.eq(0);
            if (firstItem)
                firstItem.prop(this._get_selectedAttributeSelector(), true);
        }
    },

    _setCheckBoxesValueMapping: function () {
        for (var listItemId in this._listItemValueMap) {
            var value = this._listItemValueMap[listItemId];
            jQuery($get(listItemId, this._element)).attr("value", value);
        }
    },

    _get_choiceByValue: function (value) {
        var choices = this.get_choices();
        if (!choices)
            return null;
        
        for (var i = 0; i < choices.length; i++) {
            if (choices[i].Value == value)
                return choices[i];
        }

        return null;
    },

    /* -------------------- properties ---------------- */
    enable: function (cssClass) {
        var listItems = jQuery("#" + this._choiceElement.parentNode.id).find(this._get_listItemSelector());
        for (var i = 0; i < listItems.length; i++) {
            jQuery(listItems[i]).removeAttr('disabled');
        }
        if (cssClass != null) {
            jQuery(this._element).removeClass(cssClass);
        }
        else {
            jQuery(this._element).removeClass('sfDisabledFieldCtrl');
        }
    },
    disable: function (cssClass) {
        var listItems = jQuery("#" + this._choiceElement.parentNode.id).find(this._get_listItemSelector());
        for (var i = 0; i < listItems.length; i++) {
            jQuery(listItems[i]).attr('disabled', 'disabled');
        }
        if (cssClass != null) {
            jQuery(this._element).addClass(cssClass);
        }
        else {
            jQuery(this._element).addClass('sfDisabledFieldCtrl');
        }
    },
    // Gets the choices (objects of type Choice) that are available to be chosen from.
    get_choices: function () {
        return this._choices;
    },

    // Sets the choices (objects of type Choice) that are available to be chosen from.
    set_choices: function (value) {

        // important!!!: if in checboxes mode add a value attribute to each new item
        // checkboxes does not render a value attribute
        // check the initialize method for more details

        // TODO: This is not working. Implement it.
        this._choices = value;
    },

    // Gets the value indicating whether only one choice can be made or more. If only one choice can be
    // made true; otherwise false.
    get_mutuallyExclusive: function () {
        return this._mutuallyExclusive;
    },

    // Sets the value indicating whether only one choice can be made or more. If only one choice can be
    // made true; otherwise false.
    set_mutuallyExclusive: function (value) {
        this._mutuallyExclusive = value;
    },
    // Gets one of the values of the RenderChoicesAs enumeration that determines how choices should be
    // rendered.
    // 0 - CheckBoxes
    // 1 - DropDown
    // 2 - ListBox
    // 3 - RadioButtons
    // 4 - SingleCheckBox
    // 5 - HorizontalRadioButtons
    get_renderChoicesAs: function () {
        return this._renderChoicesAs;
    },

    // Sets one of the values of the RenderChoicesAs enumeration that determines how choices should be
    // rendered.
    // 0 - CheckBoxes
    // 1 - DropDown
    // 2 - ListBox
    // 3 - RadioButtons
    // 4 - SingleCheckBox
    // 5 - HorizontalRadioButtons
    set_renderChoicesAs: function (value) {
        this._renderChoicesAs = value;
    },

    // Gets the array of indexes that represent the selected choices.
    get_selectedChoicesIndex: function () {

        var items = this._get_choiceListItems();
        var selector = this._get_selectedAttributeSelector();
        var result = [];
        for (var i = 0, length = items.length; i < length; i++) {
            if (jQuery(items[i]).prop(selector) == true) {
                result.push(i);
            }
        }
        if (result.length == 0) {
            return null;
        }

        return result;
    },

    // Sets the array of indexes that represent the selected choices.
    set_selectedChoicesIndex: function (value) {
        this._selectedChoicesIndex = value;

        if (value !== undefined && value != null) {

            var itemSelector = this._get_listItemSelector();
            var selectedSelector = this._get_selectedAttributeSelector();

            if (Array.prototype.isPrototypeOf(value)) {
                for (var i = 0, length = value.length; i < length; i++) {
                    var index = value[i];
                    jQuery(this._choiceElement).find(itemSelector).eq(index).prop(selectedSelector, true);
                }
            }
            else {
                jQuery(this._choiceElement).find(itemSelector).eq(value).prop(selectedSelector, true);
            }
        }

        this.raisePropertyChanged("selectedChoicesIndex");
    },

    // Gets a reference to the DOM element
    // that contains the control representing the choices - CheckBoxes, DropDown, ListBox or RadioButtons
    get_choiceElement: function () {
        return this._choiceElement;
    },

    // Sets a reference to the DOM element
    // that contains the control representing the choices - CheckBoxes, DropDown, ListBox or RadioButtons
    set_choiceElement: function (value) {
        this._choiceElement = value;
    },

    // gets a map between the list item element and it's choice value - used when renderChoiceAs is Checkboxes
    // because CheckBoxList control is not holding and rendering its vales
    get_listItemValueMap: function () {
        return this._listItemValueMap;
    },

    // Sets a map between the list item element and it's choice value - used when renderChoiceAs is Checkboxes
    // because CheckBoxList control is not holding and rendering its vales     
    set_listItemValueMap: function (value) {
        this._listItemValueMap = value;
    },

    // gets the read mode label
    get_readModeLabel: function () {
        return this._readModeLabel;
    },

    // Sets the read mode label  
    set_readModeLabel: function (value) {
        this._readModeLabel = value;
    },

    // Returns true if the value of the field is changed
    isChanged: function () {

        var currValue = this.get_value();
        if (this.get_renderChoicesAs() === Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox) {
            if (currValue === "true") currValue = true;
            if (currValue === "false") currValue = false;
            if (currValue === "1") currValue = 1;
            if (currValue === "0") currValue = 0;
        }

        return this.hasValueChanged(currValue);
    },

    hasValueChanged: function (currValue) {
        // While comparing the present value in the UI and the one previously set 
        // one should note that due to the nature of rendering of the ChoiceField as
        // DropDown (1), RadioButtons (3), and HorizontalRadioButtons (5) the letter always have a selected value in the UI
        // therefore the get_value() function can not return null no matter if set_value(null) was previously called

        var hasChanged = false;
        // if the value is an array, we need to compare the values of the array
        if (Object.prototype.toString.call(this._value) === '[object Array]') {
            // if the currValue is not an array, value has changed
            if (Object.prototype.toString.call(currValue) !== '[object Array]') {
                hasChanged = true;
            } else if (this._value.length != currValue.length) { // lengths are different
                hasChanged = true;
            } else { // compare all elements of the array
                for (var valIter = 0; valIter < this._value.length; valIter++) {
                    if (this._value[valIter] != currValue[valIter]) {
                        hasChanged = true;
                        break;
                    }
                }
            }
        } else {
            hasChanged = !(this._value == currValue || (this.isValueNullOrUndefined(this._value) && (this.get_renderChoicesAs() === 1 || this.get_renderChoicesAs() === 3 || this.get_renderChoicesAs() === 5)));
        }

        return hasChanged;
    },

    isValueNullOrUndefined: function (value) {
        return value === null || value === undefined;
    },

    // Gets the value of the field control.
    // If a single item is selected returns the value, otherwise of more that one is selected
    // returns an arrawy with the selected values
    get_value: function () {

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            var result = this._get_selectedItemsValues();
            if (this._returnValuesAlwaysInArray) {
                return result;
            }

            // this needs to be made obsolete and removed. You should
            // never dynamically change the return type of any function!!!
            if (Array.prototype.isPrototypeOf(result)) {
                if (result.length == 1) {
                    result = result[0];
                }
                else if (result.length > 1) {
                    return result;
                }
            }
            return result;
        }
        return this._value;
    },

    // Sets the value of the field control.
    set_value: function (value) {
        // when you pass non string (e.g. bool, int) object for value should be converted to string
        if (value !== undefined && value !== null &&
            this._renderChoicesAs != Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox &&
            Object.prototype.toString.call(value) !== '[object Array]' &&
            typeof value != "string") {
            value = value.toString();
        }

        this._value = value;

        // if not initiazlied we don't have mapping between the checkboxes and their values
        if (!this.get_isInitialized() && this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.CheckBoxes) {
            this._setCheckBoxesValueMapping();
        }
        if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox && value == null) {
            // if we are resetting the ChoiceField, we have to uncheck in case of a single checkbox
            jQuery(this._choiceElement).attr("checked", false);
        }

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._clearAllSelectedItems();
            if (value !== undefined && value !== null) {
                this._selectListItemsByValue(value);
            }
            else if(this.get_defaultValue()) {
                this._selectListItemsByValue(this.get_defaultValue());
            }
        }
        else {
            if (this._renderChoicesAs == Telerik.Sitefinity.Web.UI.Fields.RenderChoicesAs.SingleCheckBox ||
                String.isInstanceOfType(value) || (typeof value) === (typeof "")) {
                var choice = this._get_choiceByValue(value);
                if (choice) {
                    value = choice.Text;
                }

                jQuery(this._readModeLabel).text(value.toString());
            } else {
                var compositeValue = "";
                if (value) {
                    if (typeof (value) === "string") {
                        compositeValue = value;
                    }
                    else {
                        for (var cIter = 0; cIter < value.length; cIter++) {
                            compositeValue += (value[cIter] + ", ");
                        }
                        if (compositeValue.length > 2) {
                            compositeValue = compositeValue.substr(0, compositeValue.length - 2);
                        }
                    }
                }
                jQuery(this._readModeLabel).html(compositeValue);
            }
        }

        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    get_selectedName: function () {
        var selectedIndices = this.get_selectedChoicesIndex();
        if (!selectedIndices || selectedIndices.length === 0) {
            return null;
        }

        var choices = this.get_choices(), names;
        if (selectedIndices.length === 1) {
            names = choices[selectedIndices[0]].Text;
        } else {
            names = [];
            for (var i = 0, len = choices.length; i < len; i++) {
                names.push(choices[selectedIndices[i]].Text);
            }
        }
        return names;
    },

    // Sets the default value of the field control.
    // overriden from FieldControl
    set_defaultValue: function (value) {
        // converting it to string to be comptible with the results returned by get_value
        this._defaultValue = value + "";
    },

    get_element: function () {
        return this._element;
    },
    set_element: function (value) {
        this._element = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.ChoiceField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ChoiceField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);
