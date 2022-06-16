/// <reference name="MicrosoftAjax.js"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl = function (element) {

    Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl.initializeBase(this, [element]);

    this._handlePageLoadDelegate = null;
    this._formFieldsMetadata = null;
    this._formSubmitButtonsMetadata = null;
    this._usePostbackOnSubmit = null;
    this._formsSubmitUrl = null;
    this._successMessage = null;
    this._successMessageBlockId = null;
    this._formControlsContainerId = null;
    this._formDescriptionFound = null;
    this._formId = null;
    this._formName = null;
    this._formRedirectUrl = null;
    this._formSubmitAction = null;
    this._errorsPanelContainerId = null;
    this._hasFileUploadField = null;
    this._validationGroup = null;
    this._clrTypeDelimiter = null;
    this._successMessageAfterFormUpdate = null;
    this._submitActionAfterFormUpdate = null;
    this._redirectPageUrlAfterFormUpdate = null;
    this._currentMode = null;
    this._formEditContext = null;
    this._formsQueryDataElement = null;
};

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl.callBaseMethod(this, 'initialize');

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl.callBaseMethod(this, 'dispose');

        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */



    /* --------------------------------- event handlers --------------------------------- */

    _handlePageLoad: function () {

        var fieldsMetadata = JSON.parse(this.get_formFieldsMetadata());
        var formSubmitButtonsMetadata = JSON.parse(this.get_formSubmitButtonsMetadata());

        var oldBrowsers = false;
        if (!window.FormData || !window.XMLHttpRequest)
            oldBrowsers = true;

        var that = this;
        $.each(formSubmitButtonsMetadata, function (i, button) {
            $("#" + button.Id).on("click", function (e) {

                if (oldBrowsers || that.get_usePostbackOnSubmit())
                    return Telerik.Sitefinity.Web.UI.Fields.FormManager.validateGroup(that.get_validationGroup());

                e.preventDefault();

                var formData = new FormData();
                var isInputValid = Telerik.Sitefinity.Web.UI.Fields.FormManager.validateGroup(that.get_validationGroup());

                if (isInputValid) {
                    $.each(fieldsMetadata, function (i, field) {
                        var fieldControl = $find(field.Id);
                        if (!fieldControl) {
                            return;
                        }
                        var fieldValue = fieldControl.get_value();
                        if (typeof (fieldControl.get_radUpload) === 'function' && fieldControl.get_radUpload()) {
                            $.each(fieldControl.get_radUpload().getFileInputs(), function (i, uploadControl) {
                                $.each(uploadControl.files, function (i, file) {
                                    formData.append(field.FieldName, file, file.name);
                                });
                            });
                        } else {
                            if (field.ClrType) {
                                var serializeData = Sys.Serialization.JavaScriptSerializer.serialize(fieldValue);

                                formData.append(field.FieldName, serializeData + that._clrTypeDelimiter + field.ClrType);
                            }
                            else {
                                formData.append(field.FieldName, fieldValue);
                            }
                        }
                    });
                    var spinner = $('#loadingPanel_' + button.Id.split("_").pop());
                    var xhr = new XMLHttpRequest();
                    var url = String.format(that.get_formsSubmitUrl() + '/?name={0}&formId={1}&currentMode={2}', that.get_formName(), that.get_formId(), that._currentMode);
                    var editContext = that.get_formEditContext();

                    if (editContext && editContext.IsValidUpdateRequest) {
                        url = String.format(url + '&data={0}', editContext.QueryData);

                        formData.append("_formsQueryDataElement", that.get_formsQueryDataElement().value);
                    }

                    xhr.open('POST', url, true);
                    xhr.onload = function () {
                        spinner.hide();
                        $("#" + button.Id).attr("disabled", false);
                        if (xhr.status === 200) {
                            that.processFormSubmitAction();
                        } else if (xhr.status === 409) {
                            //someone has thrown CancelationException, we shouldn't display anything.
                        } else {
                            var responseJson = JSON.parse(xhr.response);
                            if ($('#' + that.get_errorsPanelContainerId()).length > 0) {
                                $('#' + that.get_errorsPanelContainerId()).text(responseJson.error);
                                $('#' + that.get_errorsPanelContainerId()).show();
                            }
                        }
                    };
                    $("#" + button.Id).attr("disabled", true);
                    spinner.show();
                    xhr.send(formData);
                }
            });
        });
    },

    /* --------------------------------- private methods --------------------------------- */
    processFormSubmitAction: function () {

        var submitAction = this._getSubmitAction();

        var pageUrl = this._getSubmitPageUrl();

        var successMessage = this._getSubmitSuccessMessage();

        switch (submitAction) {
            case 'PageRedirect': {
                if (pageUrl != null) {
                    window.location.replace(pageUrl);
                }
            } break;
            case 'TextMessage': {
                if (successMessage != null) {
                    $('#' + this.get_formControlsContainerId()).hide();
                    $('#' + this.get_successMessageBlockId()).text(successMessage);
                    $('#' + this.get_successMessageBlockId()).show();
                }
            } break;
            default: {
                throw new Error('Not implemented form submit action - ' + submitAction);
            }
        }
    },

    _getSubmitAction: function () {
        return this.get_isInEditMode() ? this._submitActionAfterFormUpdate : this.get_formSubmitAction();
    },

    _getSubmitSuccessMessage: function () {
        return this.get_isInEditMode() ? this._successMessageAfterFormUpdate : this.get_successMessage();
    },

    _getSubmitPageUrl: function () {
        return this.get_isInEditMode() ? this._redirectPageUrlAfterFormUpdate : this.get_formRedirectUrl();
    },

    /* --------------------------------- properties --------------------------------- */

    get_validationGroup: function () {
        return this._validationGroup;
    },
    set_validationGroup: function (value) {
        this._validationGroup = value;
    },
    get_hasFileUploadField: function () {
        return this._hasFileUploadField;
    },
    set_hasFileUploadField: function (value) {
        this._hasFileUploadField = value;
    },
    get_errorsPanelContainerId: function () {
        return this._errorsPanelContainerId;
    },
    set_errorsPanelContainerId: function (value) {
        this._errorsPanelContainerId = value;
    },
    get_formRedirectUrl: function () {
        return this._formRedirectUrl;
    },
    set_formRedirectUrl: function (value) {
        this._formRedirectUrl = value;
    },
    get_formSubmitAction: function () {
        return this._formSubmitAction;
    },
    set_formSubmitAction: function (value) {
        this._formSubmitAction = value;
    },
    get_formId: function () {
        return this._formId;
    },
    set_formId: function (value) {
        this._formId = value;
    },
    get_formName: function () {
        return this._formName;
    },
    set_formName: function (value) {
        this._formName = value;
    },

    get_formDescriptionFound: function () {
        return this._formDescriptionFound;
    },
    set_formDescriptionFound: function (value) {
        this._formDescriptionFound = value;
    },

    get_successMessage: function () {
        return this._successMessage;
    },
    set_successMessage: function (value) {
        this._successMessage = value;
    },
    get_successMessageBlockId: function () {
        return this._successMessageBlockId;
    },
    set_successMessageBlockId: function (value) {
        this._successMessageBlockId = value;
    },
    get_formControlsContainerId: function () {
        return this._formControlsContainerId;
    },
    set_formControlsContainerId: function (value) {
        this._formControlsContainerId = value;
    },
    get_formFieldsMetadata: function () {
        return this._formFieldsMetadata;
    },
    set_formFieldsMetadata: function (value) {
        this._formFieldsMetadata = value;
    },
    get_formSubmitButtonsMetadata: function () {
        return this._formSubmitButtonsMetadata;
    },
    set_formSubmitButtonsMetadata: function (value) {
        this._formSubmitButtonsMetadata = value;
    },
    get_usePostbackOnSubmit: function () {
        return this._usePostbackOnSubmit;
    },
    set_usePostbackOnSubmit: function (value) {
        this._usePostbackOnSubmit = value;
    },
    get_formsSubmitUrl: function () {
        return this._formsSubmitUrl;
    },
    set_formsSubmitUrl: function (value) {
        this._formsSubmitUrl = value;
    },

    get_isInEditMode: function () {
        var editContext = this.get_formEditContext();

        if (editContext) {
            return editContext.IsValidUpdateRequest;
        }
        return false;
    },

    get_formEditContext: function () {
        if (this._formEditContext) {
            return JSON.parse(this._formEditContext);
        }
        return null;
    },

    get_formsQueryDataElement: function () {
        return this._formsQueryDataElement;
    },

    set_formsQueryDataElement: function (value) {
        this._formsQueryDataElement = value;
    },
};

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl', Sys.UI.Control);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();