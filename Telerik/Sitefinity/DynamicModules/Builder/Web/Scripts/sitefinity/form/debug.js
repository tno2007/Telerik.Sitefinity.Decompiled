/**
* @name Sitefinity Form
*
* @description
* This form enhances any HTML element into a Sitefinity form.
* 
*/
function sitefinityForm(element, options) {

    this.formElement = $(element);
    this.validation = new sitefinityValidation();

    // set default options for Sitefinity Window Form
    if (!options) {
        options = {};
    }
    options.draggable = true;
    options.resizable = true;
    options.modal = true;
    options.visible = false;
    options.actions = [];

    this.windowForm = this.formElement.kendoWindow(options);
}

sitefinityForm.prototype = {

    /* initializes the form */
    initialize: function () {

        /* find all the child elements that have attributes that has 
        * an attribute data-sf-field, and convert them to 
        * Sitefinity fields (title, description, example).
        */
        $(this.formElement).find("[data-field]").each(function () {

            /* create a title element */
            if ($(this).attr("data-sf-title")) {
                var titleElement = document.createElement("label");
                titleElement.setAttribute("for", $(this).attr("id"));
                titleElement.setAttribute("class", "sfTxtLbl");
                $(titleElement).html($(this).attr("data-sf-title"));
                $(this).before(titleElement);
            }

            /* create a desription element */
            if ($(this).attr("data-sf-description")) {
                var descriptionElement = document.createElement("div");
                $(descriptionElement).addClass("sfExample");
                $(descriptionElement).html($(this).attr("data-sf-description"));
                $(this).after(descriptionElement);
            }

            /* create an example element */
            if ($(this).attr("data-sf-example")) {
                var exampleElement = document.createElement("div");
                $(exampleElement).addClass("sfExample");
                $(exampleElement).html($(this).attr("data-sf-example"));
                $(this).after(exampleElement);
            }

        });
    },

    create: function (options) {
        var window = $(this.windowForm).data("kendoWindow");
        window.open();

        if (options == null) {
            options = { fullScreen: true };
        }

        if (options.fullScreen === null || options.fullScreen === true) {
            window.maximize();
        }
    },

    /* this function puts the form in the edit mode */
    edit: function (entry) {
        $(this.formElement).find("[data-field]").each(function () {
            var property = $(this).attr("data-field");
            $(this).val(entry[property]);
        });

        this.formElement.data("entry", entry);

        var window = $(this.windowForm).data("kendoWindow");
        window.open();
        window.maximize();
    },

    /* This function clears the values on all the elements that have a data-field attribute. */
    reset: function (close) {
        $(this).find("[data-field]").each(function () {
            $(this).val("");
        });

        this.formElement.data("entry", null);

        if (close) {
            this.windowForm.data("kendoWindow").close();
        }
    },

    /* This function validates the form and returns true if form is valid. Otherwise
    * false will be returned.
    */
    isValid: function () {
        return this.validation.validateContainer(this.formElement);
    },

    /* This function validates the specified array of elements and returns true if all elements
    *  are valid; otherwise false will be returned.
    */
    areElementsValid: function (elements) {

        var areValid = true;

        if (elements == null) {
            return;
        }

        var elementsCount = elements.length;
        /* go through all the elements */
        while (elementsCount--) {
            var definitions = this.validation.getElementDefinitions(elements[elementsCount]);
            /* go through all the definitions */
            var definitionsCount = definitions.length;
            while (definitionsCount--) {
                var isValid = this.validation.validateElement(definitions[definitionsCount]);
                if (isValid == false) {
                    this.validation.displayErrorMessage(definitions[definitionsCount]);
                    areValid = false;
                } else {
                    this.validation.removeDisplayErrorMessages(definitions[definitionsCount]);
                }
            }
        }

        return areValid;
    }
};