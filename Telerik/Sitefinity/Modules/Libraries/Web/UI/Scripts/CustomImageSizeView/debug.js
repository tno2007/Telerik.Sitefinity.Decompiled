Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView.initializeBase(this, [element]);
    this._customImgSizeWrp = null;
    this._processingMethodSetFromOutside = false;
    this._selectImageProcessingMethod = null;
    this._processingMethodParamsWrp = null;
    this._methodFieldControls = null;
    this._methodControlsProperties = null;
    this._thumbnailServiceUrl = null;
    this._imageProcessingMethods = null;
    this._methodMoreDetailsLink = null;
    this._methodDetailsPopupControl = null;
    this._requiredFieldMessage = null;

    this._onLoadDelegate = null;
    this._methodMoreDetailsLinkClickDelegate = null;
    this._processingMethodChangeDelegate = null;
    this._processingMethodFieldsGeneratedDelegate = null;

    this._isVectorGraphics = false;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView.callBaseMethod(this, "initialize");
        this._attachHandlers(true);
        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        this._attachHandlers(false);
        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView.callBaseMethod(this, "dispose");
    },
    _onLoad: function () {
        if (!this._processingMethodSetFromOutside)
            this._populateMethodRelatedData();
    },
    _attachHandlers: function (toAttach) {
        this._attachImageProcessingMethodHandlers(toAttach);
    },
    _attachImageProcessingMethodHandlers: function (toAttach) {
        if (toAttach) {
            this._methodMoreDetailsLinkClickDelegate = Function.createDelegate(this, this._methodMoreDetailsLinkClickHandler);
            $addHandler(this.get_methodMoreDetailsLink(), "click", this._methodMoreDetailsLinkClickDelegate);

            this._processingMethodChangeDelegate = Function.createDelegate(this, this._processingMethodChangeHandler);
            this.get_selectImageProcessingMethod().add_valueChanged(this._processingMethodChangeDelegate);

            this._processingMethodFieldsGeneratedDelegate = Function.createDelegate(this, this._processingMethodFieldsGeneratedHandler);
            this.add_processingMethodFieldsGenerated(this._processingMethodFieldsGeneratedDelegate);
            
        } else {
            if (this._methodMoreDetailsLinkClickDelegate) {
                $removeHandler(this.get_methodMoreDetailsLink(), "click", this._methodMoreDetailsLinkClickDelegate);
                delete this._methodMoreDetailsLinkClickDelegate;
            }

            if (this._processingMethodChangeDelegate) {
                this.get_selectImageProcessingMethod().remove_valueChanged(this._processingMethodChangeDelegate);
                delete this._processingMethodChangeDelegate;
            }

            if (this._processingMethodFieldsGeneratedDelegate) {
                this.remove_processingMethodFieldsGenerated(this._processingMethodFieldsGeneratedDelegate);
                delete this._processingMethodFieldsGeneratedDelegate;
            }
        }
    },

    _methodMoreDetailsLinkClickHandler: function () {
        jQuery(this.get_methodDetailsPopupControl()).toggle();
        dialogBase.resizeToContent();
    },

    _processingMethodChangeHandler: function (sender) {        
        this._populateMethodRelatedData();
    },    

    _processingMethodFieldsGeneratedHandler: function () {
        if (this._methodControlsProperties && this._methodFieldControls) {
            var methodPropertiesDict = Sys.Serialization.JavaScriptSerializer.deserialize(this._methodControlsProperties);

            var methodFieldControl;
            for (var i = 0; i < this._methodFieldControls.length; i++) {
                methodFieldControl = jQuery("#" + this._methodFieldControls[i]);
                if (methodPropertiesDict[methodFieldControl.attr("name")] !== undefined) {
                    if (methodFieldControl.is(':checkbox'))
                        methodFieldControl.prop('checked', methodPropertiesDict[methodFieldControl.attr("name")]);
                    else
                        methodFieldControl.val(methodPropertiesDict[methodFieldControl.attr("name")]);
                }
            }
        }
    },

    //Properties START
    
    //gets the DIV containing the custom size controls.
    get_customImgSizeWrp: function () { return this._customImgSizeWrp; },
    set_customImgSizeWrp: function (value) { this._customImgSizeWrp = value; },

    //gets the choice field used for choosing the image processing method as part of the Custom size...
    get_selectImageProcessingMethod: function () { return this._selectImageProcessingMethod; },
    set_selectImageProcessingMethod: function (value) { this._selectImageProcessingMethod = value; },

    //gets the id of the wrapper div containing the processing method parameter fields
    get_processingMethodParamsWrp: function () { return this._processingMethodParamsWrp; },
    set_processingMethodParamsWrp: function (value) { this._processingMethodParamsWrp = value; },
    
    get_thumbnailServiceUrl: function () {
        return this._thumbnailServiceUrl;
    },
    set_thumbnailServiceUrl: function (value) {
        this._thumbnailServiceUrl = value;
    },

    get_imageProcessingMethods: function () {
        return this._imageProcessingMethods;
    },
    set_imageProcessingMethods: function (value) {
        this._imageProcessingMethods = value;
    },

    get_methodMoreDetailsLink: function () {
        return this._methodMoreDetailsLink;
    },
    set_methodMoreDetailsLink: function (value) {
        this._methodMoreDetailsLink = value;
    },

    get_methodDetailsPopupControl: function () {
        return this._methodDetailsPopupControl;
    },
    set_methodDetailsPopupControl: function (value) {
        this._methodDetailsPopupControl = value;
    },

    get_requiredFieldMessage: function () {
        return this._requiredFieldMessage;
    },
    set_requiredFieldMessage: function (value) {
        this._requiredFieldMessage = value;
    },

    get_isVectorGraphics: function () {
        return this._isVectorGraphics;
    },
    set_isVectorGraphics: function (value) {
        this._isVectorGraphics = value;
    },

    //Properties END

    //Methods START

    setImageProcessingMethod: function (value) {
        this._processingMethodSetFromOutside = true; // Used to populate the method related data only if needed
        this.get_selectImageProcessingMethod().set_value(value);
    },

    setMethodControlsProperties: function (methodControlsProperties) {
        this._methodControlsProperties = methodControlsProperties;
    },

    getCustomSizeMethodProperties: function () {
        var customSizeMethodProperties = "";
        var customSizeMethodPropertiesDictionary = {};
        if (this._methodFieldControls) {
            var methodFieldControl;
            for (var i = 0; i < this._methodFieldControls.length; i++) {
                methodFieldControl = jQuery("#" + this._methodFieldControls[i]);
                customSizeMethodPropertiesDictionary[methodFieldControl.attr("name")] = this._getMethodPropertyFieldValue(methodFieldControl);
            }
            customSizeMethodProperties = Sys.Serialization.JavaScriptSerializer.serialize(customSizeMethodPropertiesDictionary);
        }

        return {
            Method: this.get_selectImageProcessingMethod().get_value(),
            CustomSizeMethodProperties: customSizeMethodProperties,
            CustomSizeMethodPropertiesDictionary: customSizeMethodPropertiesDictionary
        };
    },

    validateCustomSizeMethodProperties: function () {
        var that = this;

        $(".sf_processing_method_error").remove();

        var formIsValid = true;
        jQuery(".processingMethodPropertyField").each(function () {
            var methodPropertyFieldJQ = jQuery(this);
            var methodPropertyFieldValue = that._getMethodPropertyFieldValue(methodPropertyFieldJQ);
            if (methodPropertyFieldJQ.attr('isRequired') === "true" && !methodPropertyFieldValue) {
                methodPropertyFieldJQ.after("<div class='sfError sf_processing_method_error'> " + that.get_requiredFieldMessage() + " </div>");
                formIsValid = false;
            }

            if (methodPropertyFieldJQ.attr('regularExpression') && methodPropertyFieldValue) {
                var regex = new RegExp(methodPropertyFieldJQ.attr('regularExpression'));
                if (!regex.test(methodPropertyFieldValue)) {
                    methodPropertyFieldJQ.after("<div class='sfError sf_processing_method_error'> " + methodPropertyFieldJQ.attr('regularExpressionValidationMessage') + " </div>");
                    formIsValid = false;
                }
            }
        });

        if (formIsValid) {
            var that = this;
            var customSizeMethodPropertiesData = this.getCustomSizeMethodProperties();
            method = customSizeMethodPropertiesData.Method;
            customSizeMethodProperties = customSizeMethodPropertiesData.CustomSizeMethodProperties;
            customSizeMethodPropertiesDictionary = customSizeMethodPropertiesData.CustomSizeMethodPropertiesDictionary;

            var requestUrl = String.format(
                "{0}/custom-image-thumbnail/checkParameters?methodName={1}&parameters={2}",
                this.get_thumbnailServiceUrl(),
                customSizeMethodPropertiesData.Method,
                customSizeMethodProperties);

            jQuery.ajax({
                type: 'GET',
                url: requestUrl,
                processData: false,
                async: false,
                contentType: "application/json",
                success: function (returnMessage) {
                    if (returnMessage) {
                        formIsValid = false;

                        var processingMethodParamsWrp = jQuery(that.get_processingMethodParamsWrp());
                        processingMethodParamsWrp.prepend("<div class='sfError sf_processing_method_error'> " + returnMessage + " </div>");
                    }
                }
            });
        }

        return formIsValid;
    },

    _getMethodPropertyFieldValue: function (methodFieldControl) {
        if (methodFieldControl.is(':checkbox'))
            return methodFieldControl.is(":checked");
        else
            return methodFieldControl.val();
    },

    _generateMethodSpecificFields: function (methodProperties) {
        this._clearMethodFieldControl();

        var newControl;
        for (var i = 0; i < methodProperties.Items.length; i++) {
            var property = methodProperties.Items[i];

            if (property.PropertyType === "Boolean") { // Boolean field - we create checkbox
                newControl = jQuery("<input type=\"checkbox\" />");
                newControl.attr('name', property.Name);
                newControl.addClass("processingMethodPropertyField");
                this._addMethodFieldControl(newControl, property.Title, true);

            } else if (property.PropertyType === "Int32") { // Integer field - we create text field
                newControl = jQuery("<input type=\"text\" />");
                newControl.attr('name', property.Name);
                newControl.attr('regularExpression', property.RegularExpression);
                newControl.attr('regularExpressionValidationMessage', property.RegularExpressionValidationMessage);
                newControl.attr('isRequired', property.IsRequired);
                newControl.addClass("sfTxt processingMethodPropertyField");
                this._addMethodFieldControl(newControl, property.Title);

            } else if (property.PropertyBaseType === "Enum") { // Enum field - we create a dropdown
                newControl = jQuery("<select />");
                newControl.attr('name', property.Name);
                newControl.attr('isRequired', property.IsRequired);
                newControl.addClass("processingMethodPropertyField");

                for (var j = 0; j < property.Choices.length; j++) {
                    var choiceText = property.Choices[j];
                    optionControl = jQuery("<option />");
                    optionControl.text(choiceText);
                    optionControl.val(choiceText);
                    newControl.append(optionControl);
                }
                this._addMethodFieldControl(newControl, property.Title);

            } else { // Other field - we create text  field
                newControl = jQuery("<input type=\"text\" />");
                newControl.attr('name', property.Name);
                newControl.attr('regularExpression', property.RegularExpression);
                newControl.attr('regularExpressionValidationMessage', property.RegularExpressionValidationMessage);
                newControl.attr('isRequired', property.IsRequired);
                newControl.addClass("sfTxt processingMethodPropertyField");
                this._addMethodFieldControl(newControl, property.Title);
            }
        }
    },

    _clearMethodFieldControl: function (control) {
        this._methodFieldControls = null;
        jQuery(this.get_processingMethodParamsWrp()).html("");
    },

    _addMethodFieldControl: function (control, label) {
        if (!this._methodFieldControls)
            this._methodFieldControls = [];

        var controlJQ = jQuery(control);
        var controlId = this.get_processingMethodParamsWrp().id + "_" + this._methodFieldControls.length;
        controlJQ.attr("id", controlId);

        // Add the new control Id to the _methodFieldControls array
        this._methodFieldControls.push(controlId);

        var processingMethodParamsWrpJQ = jQuery(this.get_processingMethodParamsWrp());

        // Add field wrapper
        var fieldWrapperJQ = jQuery("<div />");
        fieldWrapperJQ.addClass("sfFormCtrl");

        var controlTagName = controlJQ.get(0).tagName;
        var controlInputType = controlJQ.attr('type');
        if (controlTagName === "INPUT" && controlInputType == "checkbox") {
            fieldWrapperJQ.addClass("sfCheckBox");
        } else if (controlTagName === "INPUT" && controlInputType == "text") {
            fieldWrapperJQ.addClass("sfShortField80");

        } else if (controlTagName === "SELECT") {
            fieldWrapperJQ.addClass("sfDropdownList");
        }
        processingMethodParamsWrpJQ.append(fieldWrapperJQ);

        // Add the new control as a child of the processingMethodParamsWrp
        fieldWrapperJQ.append(controlJQ);
        
        // Add label for the control
        if (label) {
            var labelJQ = jQuery("<label />");
            labelJQ.attr("for", controlId);
            labelJQ.text(label);

            if (controlInputType === "checkbox")
                fieldWrapperJQ.append(labelJQ);
            else {
                labelJQ.insertBefore(controlJQ);
                labelJQ.addClass("sfTxtLbl");
            }
        }
    },

    _populateMethodRelatedData: function () {
        var that = this;
        var requestUrl = String.format(
            "{0}/processing-method/properties?methodName={1}",
            this.get_thumbnailServiceUrl(),
            this.get_selectImageProcessingMethod().get_value());

        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            processData: false,
            contentType: "application/json",
            success: function (methodPropertiesData) {
                that._generateMethodSpecificFields(methodPropertiesData);
                that._populateMethodMoreDetails(that.get_selectImageProcessingMethod().get_value());
                that.raiseEvent("processingMethodFieldsGenerated");
            }
        });

        this._populateMethodMoreDetails(this.get_selectImageProcessingMethod().get_value());
    },

    _populateMethodMoreDetails: function (method) {
        var imageProcessingMethods = this.get_imageProcessingMethods();

        for (var i = 0; i < imageProcessingMethods.length; i++) {
            var imageProcessingMethod = imageProcessingMethods[i];
            if (imageProcessingMethod.MethodKey === method) {
                if (!imageProcessingMethod.DescriptionText && !imageProcessingMethod.DescriptionImageUrl) {
                    jQuery(this.get_methodMoreDetailsLink()).hide();
                    jQuery(this.get_methodDetailsPopupControl()).hide();
                } else {
                    jQuery(this.get_methodMoreDetailsLink()).show();

                    // Set method title
                    jQuery(this.get_methodDetailsPopupControl()).children('h2').html(imageProcessingMethod.Title);

                    // Set method description
                    var methodDescription = jQuery(this.get_methodDetailsPopupControl()).children('p');
                    if (imageProcessingMethod.DescriptionText) {
                        methodDescription.html(imageProcessingMethod.DescriptionText);
                        methodDescription.show();
                    } else {
                        methodDescription.hide();
                    }

                    // Set method description image
                    var methodDescriptionImage = jQuery(this.get_methodDetailsPopupControl()).children('img');
                    if (imageProcessingMethod.DescriptionImageUrl) {
                        methodDescriptionImage.attr('src', imageProcessingMethod.DescriptionImageUrl);
                        methodDescriptionImage.show();
                    } else {
                        methodDescriptionImage.hide();
                    }
                }

                return;
            }
        }
    },

    //Methods END

    //Events START
    raiseEvent: function (eventName, eventArgs) {
        var handler = this.get_events().getHandler(eventName);
        if (handler) {
            if (!eventArgs) {
                eventArgs = Sys.EventArgs.Empty;
            }
            handler(this, eventArgs);
        }
    },
    add_processingMethodFieldsGenerated: function (handler) {
        this.get_events().addHandler("processingMethodFieldsGenerated", handler);
    },
    remove_processingMethodFieldsGenerated: function (handler) {
        this.get_events().removeHandler("processingMethodFieldsGenerated", handler);
    }
    //Events END
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView", Sys.UI.Control);
