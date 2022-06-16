/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView.initializeBase(this, [element]);
    this._labelTextField = null;
    this._defaultRequiredMessage = null;
    this._metaFieldNameTextBox = null;

    this._requiredChoiceFieldValueChangedDelegate = null;
    this._defaultSelectedChoiceFieldValueChangedDelegate = null;
    this._pageLoadDelegate = null;
    //this._beforeSaveChangesDelegate = null;
    this._selectors = {
        choicesEditor: {
            choiceItem: ".sfChoiceItem",
            choiceList: "#typeEditor_multipleChoiceList",
            choiceItemTemplate: "#typeFieldEditor_choiceItemTemplate",
            addChoice: ".sfAddChoice",
            removeChoice: ".sfRemoveChoice",
            choiceLabelDetailsLink: "#choiceLabelDetailsLink",
            choiceLabelDetailsTooltip: "#choiceLabelDetailsTooltip",
            choiceValueDetailsLink: "#choiceValueDetailsLink",
            choiceValueDetailsTooltip: "#choiceValueDetailsTooltip",
        }
    };
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView.callBaseMethod(this, 'initialize');

        this.choiceEditorDesign();
        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);

        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView.callBaseMethod(this, 'dispose');
        Sys.Application.remove_load(this._pageLoadDelegate);
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        this._labelTextField.set_value(controlData.Title);
        if (controlData.ChoiceItemsTitles && controlData.ChoiceItemsTitles.length > 0) {
            var choices;
            if (typeof controlData.Choices === 'string') {
                choices = $.parseJSON(controlData.Choices);
            }
            else {
                choices = controlData.Choices
            }

            this.setChoices(choices);
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var that = this;
        if (this.validateChoices()) {
            var controlData = this.get_controlData();
            controlData.Title = this._labelTextField.get_value();

            var metaFieldName = this.get_metaFieldNameTextBox();
            if (metaFieldName && !metaFieldName.get_readOnly() && controlData.MetaField) {
                controlData.MetaField.FieldName = metaFieldName.get_value();
            }

            var xml = $('<choices></choices>');
            $(this._selectors.choicesEditor.choiceList).find(this._selectors.choicesEditor.choiceItem).each(function () {
                var value = $(this).find("input.sfVal").val();
                var text = $(this).find("input.sfTxt").val();
                var encodedText = that.encodeHTML(text);

                xml.append($('<choice />').attr('value', value).attr('text', encodedText));
            });
            choices = xml[0].outerHTML;

            controlData.ChoiceItemsTitles = xml;
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    // Handles the page load event
    _pageLoadHandler: function () {
        var that = this;

        $(this._selectors.choicesEditor.choiceLabelDetailsLink).click(function () {
            $(that._selectors.choicesEditor.choiceLabelDetailsTooltip).toggleClass("sfDisplayNone");
        });

        $(this._selectors.choicesEditor.choiceValueDetailsLink).click(function () {
            $(that._selectors.choicesEditor.choiceValueDetailsTooltip).toggleClass("sfDisplayNone");
        });
    },

    /* --------------------------------- private methods --------------------------------- */

    // create the design for the multiplechoice field
    choiceEditorDesign: function () {
        // clear all choices
        $(this._selectors.choicesEditor.choiceList).empty();
        // create three sample choices
        var choiceText;
        var choiceValue;
        for (var i = 1; i < 4; i++) {
            switch (i) {
                case 1:
                    choiceText = "First Choice";
                    choiceValue = "1"
                    break;
                case 2:
                    choiceText = "Second Choice";
                    choiceValue = "2"
                    break;
                case 3:
                    choiceText = "Third Choice";
                    choiceValue = "3"
                    break;
            }

            var newChoice;
            var newChoice = $(this._selectors.choicesEditor.choiceItemTemplate)
                .clone()
                .removeAttr("id")
                .show();
            newChoice.find('input.sfVal').val(choiceValue);

            newChoice.find('input.sfTxt').val(choiceText);

            this.wireUpChoiceItem(newChoice);
            var wrapperCheckbox = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
            $(this._selectors.choicesEditor.choiceList).append(wrapperCheckbox);
        }
        // enable sorting of the choices
        //$(this._selectors.choicesEditor.choiceList).sortable({ axis: 'y', containment: 'parent', cursor: 'crosshair', handle: '.sfSortHandle' });
    },

    wireUpChoiceItem: function (choiceItem) {
        var that = this;
        // wire up add choice buttons
        $(choiceItem).find(this._selectors.choicesEditor.addChoice).each(function () {
            $(this).click(function () {
                var newChoice = $(choiceItem).clone().removeAttr("id").show();
                newChoice.find("input.sfTxt").val('');
                newChoice.find("input.sfVal").val('');
                that.wireUpChoiceItem(newChoice);
                var wrapper = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
                $(wrapper).insertAfter($(choiceItem).closest(that._selectors.choicesEditor.choiceItem));
            });
        });
        // wire up remove choice buttons
        $(choiceItem).find(this._selectors.choicesEditor.removeChoice).each(function () {
            $(this).click(function () {
                var choiceCounter = 0;
                $(that._selectors.choicesEditor.choiceList).find(that._selectors.choicesEditor.choiceItem).each(function () {
                    $(that).find("input.sfTxt").each(function () {
                        choiceCounter++;
                    });
                });
                if (choiceCounter == 2) {
                    $(that._selectors.choicesEditor.choiceList).siblings(".sfError").remove();
                    errorMessage = document.createElement("div");
                    $(errorMessage).addClass("sfError");
                    $(errorMessage).html("You cannot have less than two options!");
                    $(that._selectors.choicesEditor.choiceList).after(errorMessage);
                    return;
                }
                $(choiceItem).parent().remove();
            });
        });
        //add validation 
        $(choiceItem).find("input.sfVal").attr('required', 'required').attr('pattern', '^[a-zA-Z_0-9]+\w*$');
    },

    // validate the multiple choice field
    validateChoices: function () {
        var values = [];
        var isValid = true;
        $(this._selectors.choicesEditor.choiceList).find(this._selectors.choicesEditor.choiceItem + ' input.sfVal').each(function () {
            var val = $(this).val();
            if (values.indexOf(val) < 0) {
                values.push(val);
            }
            else {
                errorMessage = document.createElement("div");
                $(errorMessage).addClass("sfError");
                $(errorMessage).html("You cannot have duplicate values!");
                $(this._selectors.choicesEditor.choiceList).after(errorMessage);
                isValid = false;
                return false;
            }
        });
        if (!isValid) {
            return false;
        }

        var choiceCounter = 0;
        $(this._selectors.choicesEditor.choiceList).find(this._selectors.choicesEditor.choiceItem).each(function () {
            $(this).find("input.sfTxt").each(function () {
                var choiceText = $.trim($(this).val());
                if (choiceText) {
                    choiceCounter++;
                }
            });
        });

        if (choiceCounter < 2) {
            errorMessage = document.createElement("div");
            $(errorMessage).addClass("sfError");
            $(errorMessage).html("You need at least 2 choices filled with text!");
            $(this._selectors.choicesEditor.choiceList).after(errorMessage);
            return false;
        }

        return true;
    },

    encodeHTML: function (value) {
        //create a in-memory div, set it's inner text(which jQuery automatically encodes)
        //then grab the encoded contents back out.  The div never exists on the page.
        var $div = $('<div/>');
        var encodedValue = $div.text(value).html();
        $div = null;
        return encodedValue;
    },

    decodeHTML: function (value) {
        var $div = $('<div/>');
        var decodedValue = $div.html(value).text();
        $div = null;
        return decodedValue;
    },

    setChoices: function (choices) {
        var that = this;
        // clear all choices
        $(this._selectors.choicesEditor.choiceList).empty();
        // get all values from the field

        for (var i = 0; i < choices.length; i++) {      
            var value = choices[i].Value;
            var text = this.decodeHTML(choices[i].Text);
            var newChoice = $(this._selectors.choicesEditor.choiceItemTemplate).clone().removeAttr("id").show();
            $(newChoice).find("input.sfTxt").val(text);
            $(newChoice).find("input.sfVal").val(value);
            that.wireUpChoiceItem(newChoice);
            var wrapper = $(document.createElement("li")).addClass("sfChoiceItem").append(newChoice);
            $(this._selectors.choicesEditor.choiceList).append(wrapper);
        }
        // enable sorting of the choices
        //$(this._selectors.choicesEditor.choiceList).sortable({ axis: 'y', containment: 'parent', cursor: 'crosshair', handle: '.sfSortHandle' });
    },

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () { return this._parentDesigner; },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) { this._parentDesigner = value; },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    // Gets the textfield for the label of the control
    get_labelTextField: function () { return this._labelTextField; },
    // Sets the textfield for the label of the control
    set_labelTextField: function (value) { this._labelTextField = value; },

    get_metaFieldNameTextBox: function () { return this._metaFieldNameTextBox; },
    set_metaFieldNameTextBox: function (value) { this._metaFieldNameTextBox = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoicesLabelView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);