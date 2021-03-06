Type.registerNamespace("Telerik.Sitefinity.DynamicModules.Builder.Web.UI");

Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView = function (element) {
    Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView.initializeBase(this, [element]);

    this._moduleTitle = null;
    this._moduleName = null;
    this._moduleId = null;
    this._webServiceUrl = null;

    this._typeEditorTitle = null;
    this._moduleType = null;

    this._typeEditor = null;
    this._addContentTypeButton = null;
    this._emptyGuid = '00000000-0000-0000-0000-000000000000';
}

Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView.prototype = {

    // ------------------------------------- Initialization -------------------------------------
    initialize: function () {
        Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView.callBaseMethod(this, "initialize");

        this._initTreeView();

        var oThis = this;
        $(document).ready(function () { oThis._documentReady(); });
    },

    dispose: function () {
        Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView.callBaseMethod(this, "dispose");
    },

    _initTreeView: function () {

        $(".sfActionsMenu").kendoMenu({ animation: false, openOnClick: true });

        var rootUrl = this._webServiceUrl + this._moduleId + "/types/";

        var oThis = this;
        var treeView = $("#contentTypesTree ul:first").kendoTreeView({
            select: function (e) {
                e.preventDefault();
                var contentTypeId = $(e.node).attr("data-id");
                $.ajax({
                    type: 'GET',
                    url: rootUrl + contentTypeId + "/",
                    contentType: "application/json",
                    processData: false,
                    success: function (result, args) {
                        oThis.set_moduleType(result);
                        oThis.set_moduleId(result.ModuleId);
                        oThis._loadTypeEditorData(result);
                        $("#typeEditorWindow").show().sf().form().create();
                    }
                });
            }
        }).data("kendoTreeView");
        treeView.expand(".k-item");
    },

    // fired when document is ready 
    _documentReady: function () {

        jQuery("body").addClass("sfNoSidebar");
        /* type editor */
        $("#typeEditorWindow").sf().form({ animation: false, autoFocus: false }).formElement.parent().addClass("sfMaximizedWindow");
        $("#itemFieldWindow").sf().form({ animation: false }).formElement.parent().css({ "top": "50px", "left": "50%", "margin-left": "-212px" });

        var getAvailableParents = this._getAvailableParents;
        var moduleId = this._moduleId;
        var webServiceUrl = this._webServiceUrl;
        $(this._addContentTypeButton).click(function () {
            var parents = getAvailableParents(webServiceUrl, moduleId, { 'ContentTypeId': '00000000-0000-0000-0000-000000000000' });
            $(this._typeEditor).moduleBuilderTypeEditor("reset");
            $(this._typeEditor).moduleBuilderTypeEditor("addDefaultTitleField");
            $(this._typeEditor).moduleBuilderTypeEditor("loadParents", parents);
            $(this._typeEditor).moduleBuilderTypeEditor("setParentType", "00000000-0000-0000-0000-000000000000");

            $("#typeEditorWindow").show().sf().form().create();
            return false;
        });

        // initialize          
        var oThis = this;
        $(this._typeEditor).moduleBuilderTypeEditor({
            moduleName: this._moduleTitle,
            editMode: true,
            typeTitle: "",
            typeName: "",
            fields: [],
            contentTypes: [],
            onCancel: function () {
                $("#typeEditorWindow").sf().form().reset(true);
                oThis._resetModuleType();
            },
            onDone: function () {
                oThis._saveChangesClick();
            },
            onDelete: function () {
                oThis._deleteModuleType();
                oThis._resetModuleType();
            }
        });

        $(this._typeEditor).moduleBuilderTypeEditor("addDefaultTitleField");

        /* this button is not part of the editor, but rather the dialog */
        $("#typeEditorBackButton").click(function () {
            $("#typeEditorWindow").sf().form().reset(true);
            oThis._resetModuleType();
            $("#typeEditorWindow").data("kendoWindow").close();
        });
    },

    /* type editor */
    _saveChangesClick: function () {
        if (this._validateData()) {
            if (this._moduleType == null) {
                this._saveChanges(false);
            }
            else {
                this._showConfirmationMessage();
            }
        }
    },

    _showConfirmationMessage: function () {
        var newParentId = $(this._typeEditor).moduleBuilderTypeEditor("parentType");
        var confirmationId;
        var oldParentId = this._moduleType.ParentContentTypeId;
        if (newParentId != oldParentId) {
            if (newParentId != this._emptyGuid && oldParentId == this._emptyGuid) {
                //add existing type to hierarchy
                confirmationId = 'add-type-to-hierarchy-confirmation';
            }
            else {
                //change type hierarchy
                confirmationId = 'change-hierarchy-confirmation';
            }
        }
        else {
            confirmationId = 'widget-update-confirmation';
        }

        if (confirmationId) {
            var oThis = this;
            var kendoWindow = $("<div />").kendoWindow({
                title: '',
                resizable: false,
                modal: true,
                animation: false
            });
            kendoWindow.addClass("sfSelectorDialog");

            var template = kendo.template($('#' + confirmationId).html());
            kendoWindow.data("kendoWindow")
                            .content(template(this._moduleType))
                            .center().open();

            kendoWindow.find(".update-confirm,.update-cancel")
                            .click(function () {
                                if ($(this).hasClass("update-confirm")) {
                                    var checkbox = kendoWindow.find('#updateWidgetTemplates');
                                    var updateWidgetTemplates = false;
                                    if (checkbox.length) {
                                        updateWidgetTemplates = checkbox.is(':checked');
                                    }

                                    oThis._saveChanges(updateWidgetTemplates);
                                }
                                kendoWindow.data("kendoWindow").close();
                            })
                        .end();
            kendoWindow.css({ "visibility": "visible" }).parent().css({ "top": "50px", "left": "50%", "margin-left": "-212px" });
        }
    },

    _validateData: function () {
        $(".sfError").remove();
        $(this._typeEditor).moduleBuilderTypeEditor("hideAllValidationMessages");

        var isContentTypeWindowValid = $("#typeEditorWindow").sf().form().isValid();

        if ($(this._typeEditor).moduleBuilderTypeEditor("typeTitleObj").siblings(".sfError").length > 1) {
            $(this._typeEditor).moduleBuilderTypeEditor("typeTitleObj").siblings(".sfError:first").remove();
        }

        if ($(this._typeEditor).moduleBuilderTypeEditor("typeNameObj").siblings(".sfError").length > 1) {
            $(this._typeEditor).moduleBuilderTypeEditor("typeNameObj").siblings(".sfError:first").remove();
        }

        var isFormValid = false;
        if (isContentTypeWindowValid)
            isFormValid = $(this._typeEditor).moduleBuilderTypeEditor("validate");

        return isFormValid && isContentTypeWindowValid;
    },

    _saveChanges: function (regenerateTemplates) {
        var _data;
        var oThis = this;
        if (this._moduleType != null) {

            this._showLoadingSection();
            _data = {
                "ContentTypeName": this._moduleType.ContentTypeName,
                "ContentTypeDescription": this._moduleType.ContentTypeDescription,
                "ContentTypeId": this._moduleType.ContentTypeId,
                "ParentContentTypeId": $(this._typeEditor).moduleBuilderTypeEditor("parentType"),
                "ModuleId": this._moduleId,
                "ContentTypeItemTitle": $(this._typeEditor).moduleBuilderTypeEditor("typeTitle").trim(),
                "ContentTypeItemName": $(this._typeEditor).moduleBuilderTypeEditor("typeName").trim(),
                "Fields": $(this._typeEditor).moduleBuilderTypeEditor("fields"),
                "MainShortTextFieldName": $(this._typeEditor).moduleBuilderTypeEditor("mainField"),
                "IsSelfReferencing": $(this._typeEditor).moduleBuilderTypeEditor("isSelfReferencing"),
                "CheckFieldPermissions": $(this._typeEditor).moduleBuilderTypeEditor("checkFieldPermissions")
            };

            $.ajax({
                type: 'PUT',
                url: this._webServiceUrl + this._moduleId + "/?updateWidgetTemplates=" + regenerateTemplates,
                contentType: "application/json",
                processData: false,
                data: JSON.stringify(_data),
                success: function (result, args) {
                    oThis._hideLoadingSection();
                    window.location.reload(true);

                },
                error: function (result, args) {
                    oThis._hideAllErrorDivsOnPage();
                    oThis._hideLoadingSection();
                    $("#typeEditorWindow").data("kendoWindow").close();
                    $("#errorUpdatingModule").show();
                    $("#errorUpdatingModule").text(JSON.parse(result.responseText).Detail);
                }
            });
        }
        else {
            this._showLoadingSection();
            _data = {
                "ContentTypeName": this._moduleName,
                "ModuleId": this._moduleId,
                "ContentTypeItemTitle": $(this._typeEditor).moduleBuilderTypeEditor("typeTitle").trim(),
                "ContentTypeItemName": $(this._typeEditor).moduleBuilderTypeEditor("typeName").trim(),
                "ContentTypeTitle": this._moduleTitle,
                "ParentContentTypeId": $(this._typeEditor).moduleBuilderTypeEditor("parentType"),
                "Fields": $(this._typeEditor).moduleBuilderTypeEditor("fields"),
                "MainShortTextFieldName": $(this._typeEditor).moduleBuilderTypeEditor("mainField"),
                "IsSelfReferencing": $(this._typeEditor).moduleBuilderTypeEditor("isSelfReferencing"),
                "CheckFieldPermissions": $(this._typeEditor).moduleBuilderTypeEditor("checkFieldPermissions")
            };

            $.ajax({
                type: 'PUT',
                url: this._webServiceUrl + this._moduleId + "/?updateWidgetTemplates=" + true,
                contentType: "application/json",
                processData: false,
                data: JSON.stringify(_data),
                success: function (result, args) {
                    oThis._hideLoadingSection();
                    window.location.reload(true);
                },
                error: function (result, args) {
                    oThis._hideAllErrorDivsOnPage();
                    oThis._hideLoadingSection();
                    $("#typeEditorWindow").data("kendoWindow").close();
                    $("#errorUpdatingModule").show();
                    $("#errorUpdatingModule").text(JSON.parse(result.responseText).Detail);
                }
            });
        }
    },

    _deleteModuleType: function () {
        var dynamicContentType = this.get_moduleType();
        var oThis = this;
        oThis._showLoadingSection();

        $.ajax({
            type: 'DELETE',
            url: this._webServiceUrl + "content-type/" + dynamicContentType.ContentTypeId + "/?moduleId=" + this._moduleId,
            contentType: "application/json",
            processData: false,
            success: function (result, args) {
                if (result["DeleteContentTypeStatus"] == 1) { //delete module succeeded     
                    oThis._hideLoadingSection();
                    window.location.reload(true);
                }
                else {
                    oThis._hideAllErrorDivsOnPage();
                    oThis._hideLoadingSection();
                    $("#typeEditorWindow").data("kendoWindow").close();
                    $("#errorDeletingContentType").show();
                }
            },
            error: function (result, args) {
                oThis._hideAllErrorDivsOnPage();
                oThis._hideLoadingSection();
                $("#typeEditorWindow").data("kendoWindow").close();
                $("#errorUpdatingModule").show();
                $("#errorUpdatingModule").text(JSON.parse(result.responseText).Detail);
            }
        });
    },

    _resetModuleType: function () {
        this._moduleType = null;
    },

    _loadTypeEditorData: function (moduleType) {
        var parents = this._getAvailableParents(this._webServiceUrl, this._moduleId, moduleType);
        $(this._typeEditor).moduleBuilderTypeEditor("setEditorTitle", this._typeEditorTitle, moduleType.ContentTypeItemTitle);
        $(this._typeEditor).moduleBuilderTypeEditor("loadData", moduleType, moduleType.Fields);
        $(this._typeEditor).moduleBuilderTypeEditor("setMainField", moduleType.MainShortTextFieldName);
        $(this._typeEditor).moduleBuilderTypeEditor("loadParents", parents);
        $(this._typeEditor).moduleBuilderTypeEditor("setParentType", moduleType.ParentContentTypeId);
    },

    _getAvailableParents: function (webServiceUrl, moduleId, moduleType) {
        var rootUrl = webServiceUrl + moduleId + "/types/" + moduleType.ContentTypeId + '/availableparents/';
        var parents = [];
        $.ajax({
            type: 'GET',
            url: rootUrl,
            contentType: "application/json",
            processData: false,
            async: false,
            success: function (result, args) {
                parents = result["Items"];
            }
        });

        return parents;
    },

    _showLoadingSection: function () {
        $("#finishSection").hide();
        $("#loadingSection").show();
    },

    _hideLoadingSection: function () {
        $("#finishSection").show();
        $("#loadingSection").hide();
    },

    _hideAllErrorDivsOnPage: function () {
        $("errorUpdatingModule").hide();
        $("#errorUpdatingModuleFields").hide();
        $("#errorUpdatingModuleNameAndDescription").hide();
        $("#errorActivatingModule").hide();
        $("#errorDeactivatingModule").hide();
        $("#errorDeletingContentType").hide();
    },

    /* properties */
    get_typeEditor: function () {
        return this._typeEditor;
    },

    set_typeEditor: function (value) {
        this._typeEditor = value;
    },

    get_addContentTypeButton: function () {
        return this._addContentTypeButton;
    },

    set_addContentTypeButton: function (value) {
        this._addContentTypeButton = value;
    },

    get_moduleType: function () {
        return this._moduleType;
    },

    set_moduleType: function (value) {
        this._moduleType = value;
    },

    get_moduleId: function () {
        return this._moduleId;
    },

    set_moduleId: function (value) {
        this._moduleId = value;
    },

    get_typeEditorTitle: function () {
        return this._typeEditorTitle;
    },

    set_typeEditorTitle: function (value) {
        this._typeEditorTitle = value;
    }
};

Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView.registerClass("Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypesMasterView", Sys.UI.Control);
