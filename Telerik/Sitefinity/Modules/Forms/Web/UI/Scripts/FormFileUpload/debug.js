﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload.initializeBase(this, [element]);

    this._handlePageLoadDelegate = null;
    this._documentServiceUrl = null;
    this._errorLabelId = null;
    this._rangeViolationMessage = null;
    this._cantUploadFilesFormat = null;
    this._maxFileSize = null;
    this._minFileSize = null;
    this._allowedExtensions = null;
    this._allowMultipleAttachments = null;
    this._selector = null;
    this._selectorDialog = null;
    this._selectorInsertDelegate = null;
    this._selectFileButton = null;
    this._selectedFilesList = null;
    this._selectedFiles = [];
    this._documentTypeFullName = null;
    this._dataItem = null;
    this._entriesTypeName = null;
    this._providerName = null;
    this._contentLinksApplicationName = null;
    this.thisElement = null;
    this._selectedFilesHiddenElement = null;

    this._radUpload = null;
    this._radUploadAddedDelegate = null;
    this._radUploadDeletingDelegate = null;

    this._removeButtonsSelector = "input[name='RemoveRow']";
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload.callBaseMethod(this, 'initialize');

        this.thisElement = jQuery(this._element);
        if (this._selector) {
            this._selectorDialog = jQuery(this._selector.get_element()).dialog({
                autoOpen: false,
                modal: true,
                width: 655,
                height: "auto",
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                classes: {
                    "ui-dialog": "sfSelectorDialog sfZIndexL"
                }
            });
            this._selectorInsertDelegate = Function.createDelegate(this, this._selectorInsertHandler);
            this._selector.set_customInsertDelegate(this._selectorInsertDelegate);
            this._selector.set_maxFileSize(this._maxFileSize);
            this._selector.set_allowedExtensions(this._allowedExtensions);
        }

        if (this.get_selectFileButton()) {
            var that = this;
            jQuery(this.get_selectFileButton()).click(function () {
                that._selectorDialog.dialog("open");
                that._dialogScrollToTop(that._selectorDialog);
            });
        }

        if (this.get_radUpload()) {
            this._radUploadAddedDelegate = Function.createDelegate(this, this._radUploadAddedHandler);
            this.get_radUpload().add_added(this._radUploadAddedDelegate);

            this._radUploadDeletingDelegate = Function.createDelegate(this, this._radUploadDeletingHandler);
            this.get_radUpload().add_deleting(this._radUploadDeletingDelegate);

            if (this.get_radUpload().getFileInputs().length <= 1) {
                jQuery(this.get_radUpload().get_element()).find(this._removeButtonsSelector).hide();
            }
        }

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload.callBaseMethod(this, 'dispose');

        if (this._selectorInsertDelegate) {
            delete this._selectorInsertDelegate
        }
        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }

        if (this.get_radUpload()) {
            if (this._radUploadAddedDelegate) {
                this.get_radUpload().remove_added(this._radUploadAddedDelegate);
                delete this._radUploadAddedDelegate;
            }

            if (this._radUploadDeletingDelegate) {
                this.get_radUpload().remove_deleting(this._radUploadDeletingDelegate);
                delete this._radUploadDeletingDelegate;
            }
        }
    },

    /* --------------------------------- public methods ---------------------------------- */

    validate: function () {
        var baseValid = Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload.callBaseMethod(this, 'validate');
        if (!baseValid)
            return false;
        var errorText = $('#' + this.get_errorLabelId());
        var that = this;
        var allValid = true;
        ////NOTE: this.get_radUpload() is only used in front end mode.
        if (window.File && window.Blob && this.get_radUpload()) {
            $.each(this.get_radUpload().getFileInputs(), function (i, uploadControl) {
                var valid = true;
                $.each(uploadControl.files, function (i, file) {
                    var extensionIndex = file.name.lastIndexOf('.');
                    var extension = file.name.substring(extensionIndex + 1, file.name.length).toLowerCase().trim();
                    if (that.get_allowedExtensions() !== null && $.inArray(extension, that.get_allowedExtensions()) === -1) {
                        var errorMessage = String.format(that.get_cantUploadFilesFormat(), '.' + extension, that.get_allowedExtensions().join(', .'));
                        errorText.html(errorMessage);
                        errorText.show();
                        valid = false;
                        return false;
                    }
                    if (that.get_minFileSize() > 0 && file.size < that.get_minFileSize() ||
                        that.get_maxFileSize() > 0 && file.size > that.get_maxFileSize()) {
                        errorText.html(that.get_rangeViolationMessage());
                        errorText.show();
                        valid = false;
                        return false;
                    }
                });
                if (!valid) {
                    allValid = false;
                    return false;
                }
            });
        }
        if (allValid)
            errorText.hide();
        return allValid;
    },

    reset: function () {
        this._selectedFiles.length = 0;
        $(this._selectedFilesList).empty();
        this._resetHandler();
    },
    /* --------------------------------- event handlers ---------------------------------- */
    _handlePageLoad: function () {
        if (this._selector) {
            //create library control is not visible, we don't need to validate it
            this._selector.get_uploaderView().get_parentLibrarySelector().get_createLibraryControl().get_createLibraryTxt().set_validator(undefined);
        }
    },

    _selectorInsertHandler: function (selectedItem) {
        if (!this._allowMultipleAttachments) {
            this._selectedFiles.length = 0;
            $(this._selectedFilesList).empty();
        }
        this._addDocument(selectedItem, this);

        this._selectedFiles.push(
		{
		    'ParentItemId': this._dataItem.Id ? this._dataItem.Id : this._emptyGuid,
		    'ParentItemProviderName': this._providerName,
		    'ParentItemType': this._entriesTypeName,
		    'ComponentPropertyName': this._dataFieldName,
		    'ChildItemId': selectedItem.Id,
		    'ChildItemType': this._documentTypeFullName,
		    'ChildItemProviderName': selectedItem.ProviderName,
		    'ChildItemAdditionalInfo': selectedItem.Title + selectedItem.Extension + '|' + selectedItem.MediaUrl,
		    'ApplicationName': this._contentLinksApplicationName,
		    'Ordinal': this._selectedFiles.length
		});
        this._selectorDialog.dialog("close");

        $(this._selectedFilesHiddenElement).val(JSON.stringify($sitefinity.fixObjectForSerialization(this._selectedFiles)));
    },

    _radUploadAddedHandler: function (sender, args) {
        if (sender.getFileInputs().length > 1) {
            jQuery(sender.get_element()).find(this._removeButtonsSelector).show();
        }
    },

    _radUploadDeletingHandler: function (sender, args) {
        //This is called prior to deletion so if there are 2 inputs, 
        //after the action is done there will be just one input left.
        if (sender.getFileInputs().length <= 2) {
            jQuery(sender.get_element()).find(this._removeButtonsSelector).hide();
        }
    },

    /* --------------------------------- private methods --------------------------------- */
    _loadItems: function (items) {
        $('#filesPlaceholder').html('');
        $('#selectedFiles').html('');
        if (items) {
            var that = this;
            for (var i = 0; i < items.length; i++) {
                var contentLink = items[i];
                $.ajax({
                    type: 'GET',
                    url: this._documentServiceUrl + "live/" + contentLink.ChildItemId + "/?provider=" + contentLink.ChildItemProviderName,
                    contentType: "application/json",
                    processData: false,
                    success: function (result, args) {
                        that._addDocument(result.Item, that);
                    }
                });
            }
        }
    },

    _addDocument: function (document, context) {
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            context._addWriteModeDocument(document);
        }
        else {
            context._addReadModeDocument(document);
        }
    },

    _addWriteModeDocument: function (document) {
        var title = (document.Title.PersistedValue ? document.Title.PersistedValue : document.Title) + document.Extension;
        var documentMarkup = '<li id="asset' + document.Id + '" class="sfAsset">';
        documentMarkup += '<a href="' + document.MediaUrl + '" target="_blank">' + title + '</a>';
        documentMarkup += ' <span class="sfNote">' + Math.round(document.TotalSize / 1024) + ' KB</span>';
        documentMarkup += ' <a data-command="delete" data-id="' + document.Id + '" class="sfDeleteItm">[DELETE]</a>';
        documentMarkup += '</li>'
        $(this._selectedFilesList).append(documentMarkup);
        this._bindDelete();
    },

    _addReadModeDocument: function (document) {
        var title = document.Title.PersistedValue + document.Extension;
        var documentMarkup = '<li id="asset' + document.Id + '" class="sfAsset">';
        documentMarkup += '<a href="' + document.MediaUrl + '" target="_blank">' + title + '</a>';
        documentMarkup += ' <span class="sfNote">' + Math.round(document.TotalSize / 1024) + ' KB</span>';
        documentMarkup += '</li>'
        $(this._selectedFilesList).append(documentMarkup);
    },

    _bindDelete: function () {
        var that = this;
        this.thisElement.find(".sfAsset").find("[data-command=delete]").unbind();
        this.thisElement.find(".sfAsset").find("[data-command=delete]").bind('click', function () {
            that._removeItem($(this).attr("data-id"));
        });
    },

    _removeItem: function (id) {
        this.thisElement.find("#asset" + id).remove();

        for (var i = this._selectedFiles.length - 1; i >= 0; i--) {
            if (this._selectedFiles[i].ChildItemId == id) {
                this._selectedFiles.splice(i, 1);
            }
        }

        $(this._selectedFilesHiddenElement).val(JSON.stringify($sitefinity.fixObjectForSerialization(this._selectedFiles)));
    },

    _dialogScrollToTop: function (dlg) {
        var scrollTopHtml = jQuery("html").eq(0).scrollTop();
        var scrollTopBody = jQuery("body").eq(0).scrollTop();
        var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
        jQuery(dlg).parent().css({ "top": scrollTop });
    },

    /* --------------------------------- properties -------------------------------------- */
    // Gets the value of the field control.
    get_value: function () {
        if (this.get_radUpload()) {
            return jQuery(this.get_radUpload().getFileInputs()).val();
        }
        else {
            return this._selectedFiles;
        }
    },
    // Sets the value of the field control. [Optional] sends the whole data item to the field control, in
    // case other properties of the data item are needed.
    set_value: function (value) {
        this._selectedFiles.length = 0;
        $(this._selectedFilesList).empty();
        if (value) {
            for (var i = 0; i < value.length; i++) {
                var currentItem = value[i];
                this._selectedFiles.push(
                    {
                        'ParentItemId': currentItem.ParentItemId,
                        'ParentItemProviderName': currentItem.ParentItemProviderName,
                        'ParentItemType': currentItem.ParentItemType,
                        'ParentItemAdditionalInfo': currentItem.ParentItemAdditionalInfo,
                        'ComponentPropertyName': currentItem.ComponentPropertyName,
                        'ChildItemId': currentItem.ChildItemId,
                        'ChildItemType': currentItem.ChildItemType,
                        'ChildItemProviderName': currentItem.ChildItemProviderName,
                        'ChildItemAdditionalInfo': currentItem.ChildItemAdditionalInfo,
                        'ApplicationName': currentItem.ApplicationName,
                        'Ordinal': i
                    }
                );
            }
            $(this._selectedFilesHiddenElement).val(JSON.stringify($sitefinity.fixObjectForSerialization(this._selectedFiles)));

            this._loadItems(value);
        }
    },
    get_rangeViolationMessage: function () {
        return this._rangeViolationMessage;
    },
    set_rangeViolationMessage: function (value) {
        this._rangeViolationMessage = value;
    },
    get_cantUploadFilesFormat: function () {
        return this._cantUploadFilesFormat;
    },
    set_cantUploadFilesFormat: function (value) {
        this._cantUploadFilesFormat = value;
    },
    get_errorLabelId: function () {
        return this._errorLabelId;
    },
    set_errorLabelId: function (value) {
        this._errorLabelId = value;
    },
    get_documentServiceUrl: function () {
        return this._documentServiceUrl;
    },
    set_documentServiceUrl: function (value) {
        this._documentServiceUrl = value;
    },
    get_selector: function () {
        return this._selector;
    },
    set_selector: function (value) {
        this._selector = value;
    },
    get_maxFileSize: function () {
        return this._maxFileSize;
    },
    set_maxFileSize: function (value) {
        this._maxFileSize = value;
    },
    get_minFileSize: function () {
        return this._minFileSize;
    },
    set_minFileSize: function (value) {
        this._minFileSize = value;
    },
    get_allowedExtensions: function () {
        return this._allowedExtensions;
    },
    set_allowedExtensions: function (value) {
        this._allowedExtensions = value;
    },
    get_allowMultipleAttachments: function () {
        return this._allowMultipleAttachments;
    },
    set_allowMultipleAttachments: function (value) {
        this._allowMultipleAttachments = value;
    },
    get_selectFileButton: function () {
        return this._selectFileButton;
    },
    set_selectFileButton: function (value) {
        this._selectFileButton = value;
    },
    get_selectedFilesList: function () {
        return this._selectedFilesList;
    },
    set_selectedFilesList: function (value) {
        this._selectedFilesList = value;
    },
    get_selectedFiles: function () {
        return this._selectedFiles;
    },
    set_selectedFiles: function (value) {
        this._selectedFiles = value;
    },
    get_documentTypeFullName: function () {
        return this._documentTypeFullName;
    },
    set_documentTypeFullName: function (value) {
        this._documentTypeFullName = value;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    set_dataItem: function (value) {
        this._dataItem = value;
    },
    get_selectedFilesHiddenElement: function () {
        return this._selectedFilesHiddenElement;
    },
    set_selectedFilesHiddenElement: function (value) {
        this._selectedFilesHiddenElement = value;
    },
    get_entriesTypeName: function () {
        return this._entriesTypeName;
    },
    set_entriesTypeName: function (value) {
        this._entriesTypeName = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },
    get_contentLinksApplicationName: function () {
        return this._contentLinksApplicationName;
    },
    set_contentLinksApplicationName: function (value) {
        this._contentLinksApplicationName = value;
    },
    get_radUpload: function () {
        return this._radUpload;
    },
    set_radUpload: function (value) {
        this._radUpload = value;
    }
}
Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormFileUpload', Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
