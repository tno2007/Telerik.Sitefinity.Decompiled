/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView.initializeBase(this, [element]);

    this._uploadServiceUrl = null;
    this._webServiceUrl = null;
    this._contentType = null;
    this._parentType = null;
    this._providerName = null;
    this._selectFileButtonText = null;
    this._jElementToInsert = null;
    this._selectLabel = null;
    this._bindOnLoad = null;

    this._fileNameTextBox = null;
    this._selectFileButton = null;
    this._settingsPanel = null;

    this._altTextField = null;
    this._resizingOptionsControl = null;
    this._titleTextField = null;
    this._targetLibraryId = null;
    this._emptyGuid = null;

    this._contentId = null;
    this._contentUrl = null;
    this._contentItem = null;
    this._ajaxUpload = null;

    this._fileSubmitDelegate = null;
    this._fileChangedDelegate = null;
    this._fileUploadedDelegate = null;
    this._cantUploadFilesErrorMessage = null;
    this._fileAllowedExtensions = null;

    this._librarySelectorTitle = null;
    this._parentLibrarySelector = null;

    this._applicationLoadDelegate = null;

    this._useTitleFieldValueAsMediaItemTitle;
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView.callBaseMethod(this, 'initialize');

        if (this._selectFileButton) {
            this._fileChangedDelegate = Function.createDelegate(this, this._fileChanged);
            this._fileSubmitDelegate = Function.createDelegate(this, this._fileSubmit);
            this._fileUploadedDelegate = Function.createDelegate(this, this._fileUploaded);
            this._ajaxUpload = new AjaxUpload(this._selectFileButton.id,
                                              { action: this._uploadServiceUrl,
                                                  autoSubmit: false,
                                                  // The type of data that you're expecting back from the server.
                                                  // HTML (text) and XML are detected automatically.
                                                  // Useful when you are using JSON data as a response, set to "json" in that case.
                                                  // Also set server response type to text/html, otherwise it will not work in IE6
                                                  responseType: false,
                                                  // Fired after the file is selected
                                                  // Useful when autoSubmit is disabled
                                                  // You can return false to cancel upload
                                                  // @param file basename of uploaded file
                                                  // @param extension of that file
                                                  onChange: this._fileChangedDelegate,
                                                  // Fired before the file is uploaded
                                                  // You can return false to cancel upload
                                                  // @param file basename of uploaded file
                                                  // @param extension of that file
                                                  onSubmit: this._fileSubmitDelegate,
                                                  // Fired when file upload is completed
                                                  // WARNING! DO NOT USE "FALSE" STRING AS A RESPONSE!
                                                  // @param file basename of uploaded file
                                                  // @param response server response
                                                  onComplete: this._fileUploadedDelegate
                                              });
        }
        else {
            throw "File upload element not found!";
        }

        this._applicationLoadDelegate = Function.createDelegate(this, this._applicationLoadHandler);
        Sys.Application.add_load(this._applicationLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView.callBaseMethod(this, 'dispose');

        if (this._fileChangedDelegate) {
            delete this._fileChangedDelegate;
        }
        if (this._fileSubmitDelegate) {
            delete this._fileSubmitDelegate;
        }
        if (this._fileUploadedDelegate) {
            delete this._fileUploadedDelegate;
        }
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        this._ajaxUpload = null;
        if (this._applicationLoadDelegate) {
            delete this._applicationLoadDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    rebind: function (providerName) {
        this._providerName = providerName;
        this.get_parentLibrarySelector().rebind(providerName);
    },

    /* -------------------- events -------------------- */

    add_onFileChanged: function (delegate) {
        this.get_events().addHandler('onFileChanged', delegate);
    },
    remove_onFileChanged: function (delegate) {
        this.get_events().removeHandler('onFileChanged', delegate);
    },

    add_onFileUploaded: function (delegate) {
        this.get_events().addHandler('onFileUploaded', delegate);
    },
    remove_onFileUploaded: function (delegate) {
        this.get_events().removeHandler('onFileUploaded', delegate);
    },

    /* --------------------------------- event handlers --------------------------------- */

    _applicationLoadHandler: function () {
        if (this._targetLibraryId != this._emptyGuid) {
            jQuery(this.get_parentLibrarySelector().get_element()).hide();
            jQuery(this.get_librarySelectorTitle()).hide();
        }
        else {
            if (this._bindOnLoad) {
                this.rebind(this._providerName);
            }
        }
    },

    _validateFileExtension: function (extension) {
        // if file extension is not specified => do not perform validation for file extension
        if (!this._fileAllowedExtensions)
            return true;
        
        // Allowed extensions could be an array in which case we pass it as-is
        var extensions = this._fileAllowedExtensions.toLowerCase ?
            this._fileAllowedExtensions.toLowerCase() :
            this._fileAllowedExtensions;
        return extensions.indexOf(extension.toLowerCase()) >= 0
    },

    // Fired when the user selects a file
    _fileChanged: function (file, extension) {
        if (!this._validateFileExtension(extension)) {
            var message = String.format(this._cantUploadFilesErrorMessage, "." + extension, this._fileAllowedExtensions);
            alert(message);

            // Return false to cancel upload
            return false;
        }

        jQuery(this._settingsPanel).show();
        jQuery(this._fileNameTextBox).val(file);

        if (this._titleTextField) {
            if (this._jElementToInsert && this._jElementToInsert.children().length > 0) {
                jQuery(this._titleTextField.get_element()).hide();
            }
            else {
                var filename = (this._jElementToInsert) ? this._jElementToInsert.html() : "";

                if (filename == "") {
                    filename = file;
                    var idx = file.indexOf(".");
                    if (idx > -1) {
                        filename = file.substring(0, idx);
                    }
                }

                this._titleTextField.set_value(filename);
            }
        }

        var jSelectButton = jQuery(this._selectFileButton);
        jSelectButton.find("strong.sfLinkBtnIn").html(this._selectFileButtonText);
        if (dialogBase && jSelectButton.parents(".ui-dialog").length == 0) { // resize only if not in jQuery dialog
            dialogBase.resizeToContent();
        }

        this._fileChangedHandler(file);
    },

    // Fired when the file is to be uploaded
    _fileSubmit: function (file, extension) {
        this._showLoadingSign();

        var libraryId = null;
        if (this._targetLibraryId != this._emptyGuid) {
            libraryId = this._targetLibraryId;
        }
        else {
            libraryId = this.get_parentLibrarySelector().get_value();
        }
        var uploadData = {
            ContentType: this._contentType,
            ProviderName: this._providerName,
            LibraryId: libraryId,
            RecompileItemUrls: 'true'
        };

        if (this._altTextField) {
            uploadData.AlternativeText = this._altTextField.get_value();
        }
        if (this._titleTextField) {
            uploadData.Title = this._titleTextField.get_value();
        }
        uploadData.UseTitleAsMediaItemTitle = this.get_useTitleFieldValueAsMediaItemTitle() ? "true" : "false";
        if (this._resizingOptionsControl) {
            if (this._resizingOptionsControl.get_itemIsResized()) {
                uploadData.Width = this._resizingOptionsControl.get_resizedWidth();
                uploadData.OpenOriginalImageOnClick = this._resizingOptionsControl.get_resizedItemOpensOriginal();
            }
            else {
                uploadData.Width = 0;
                uploadData.OpenOriginalImageOnClick = false;
            }
        }

        this._ajaxUpload.setData(uploadData);
    },

    // Fired when the file is uploaded
    _fileUploaded: function (file, response) {
        this._hideLoadingSign();
        var jSelectButton = jQuery(this._selectFileButton);
        jSelectButton.find("strong.sfLinkBtnIn").html(this._selectLabel);
        if (response) {
            try {
                var responseMessage = eval("(" + response + ")");
                if (!responseMessage.UploadResult) {
                    alert(responseMessage.ErrorMessage);
                }
                else {
                    this._contentId = responseMessage.ContentId;
                    this._contentUrl = responseMessage.ContentUrl;
                    this._contentItem = responseMessage.ContentItem;
                    var uploadedItem = { "Id": this._contentId, "MediaUrl": this._contentUrl };

                    if (responseMessage.ContentItem) {
                        uploadedItem = responseMessage.ContentItem;
                    }

                    this._fileUploadedHandler(uploadedItem);
                }
            }
            catch (e) {
                alert("Error '" + e + "': " + response);
            }
        }
    },

    _fileChangedHandler: function (file) {
        var eventArgs = eventArgs;
        var h = this.get_events().getHandler('onFileChanged');
        if (h) h(this, eventArgs);
    },

    _fileUploadedHandler: function (uploadedItem) {
        var eventArgs = uploadedItem;
        var h = this.get_events().getHandler('onFileUploaded');
        if (h) h(this, eventArgs);
    },

    _showLoadingSign: function () {
        var windowContent = this._getWindowContent();
        windowContent.addClass("rwLoading");
        windowContent.children().css({ visibility: "hidden" });
    },
    _hideLoadingSign: function () {
        var windowContent = this._getWindowContent();
        windowContent.removeClass("rwLoading");
        windowContent.children().css({ visibility: "" });
    },
    _getWindowContent: function () {
        if (this.get_parent()._hostedInRadWindow) {
            return jQuery(this.get_parent().get_radWindow()._contentCell);
        }
        else {
            return jQuery(this.get_parent()._element);
        }
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    get_settingsPanel: function () {
        return this._settingsPanel;
    },
    set_settingsPanel: function (value) {
        this._settingsPanel = value;
    },

    get_selectFileButton: function () {
        return this._selectFileButton;
    },
    set_selectFileButton: function (value) {
        this._selectFileButton = value;
    },

    get_fileNameTextBox: function () {
        return this._fileNameTextBox;
    },
    set_fileNameTextBox: function (value) {
        this._fileNameTextBox = value;
    },

    get_ajaxUpload: function () {
        return this._ajaxUpload;
    },
    set_ajaxUpload: function (value) {
        this._ajaxUpload = value;
    },

    get_altTextField: function () {
        return this._altTextField;
    },
    set_altTextField: function (value) {
        this._altTextField = value;
    },

    // Gets the control that represents the resizing options
    get_resizingOptionsControl: function () {
        return this._resizingOptionsControl;
    },
    // Sets the control that represents the resizing options
    set_resizingOptionsControl: function (value) {
        this._resizingOptionsControl = value;
    },

    get_titleTextField: function () {
        return this._titleTextField;
    },
    set_titleTextField: function (value) {
        this._titleTextField = value;
    },

    get_selectFileButtonText: function () {
        return this._selectFileButtonText;
    },
    set_selectFileButtonText: function (value) {
        this._selectFileButtonText = value;
    },

    get_cantUploadFilesErrorMessage: function () {
        return this._cantUploadFilesErrorMessage;
    },
    set_cantUploadFilesErrorMessage: function (value) {
        this._cantUploadFilesErrorMessage = value;
    },
    get_fileAllowedExtensions: function () {
        return this._fileAllowedExtensions;
    },
    set_fileAllowedExtensions: function (value) {
        this._fileAllowedExtensions = value;
    },

    get_jElementToInsert: function () {
        return this._jElementToInsert;
    },
    set_jElementToInsert: function (value) {
        this._jElementToInsert = value;
    },

    get_parentLibrarySelector: function () {
        return this._parentLibrarySelector;
    },
    set_parentLibrarySelector: function (value) {
        this._parentLibrarySelector = value;
    },
    get_librarySelectorTitle: function () {
        return this._librarySelectorTitle;
    },
    set_librarySelectorTitle: function (value) {
        this._librarySelectorTitle = value;
    },

    get_useTitleFieldValueAsMediaItemTitle: function () {
        return this._useTitleFieldValueAsMediaItemTitle;
    },
    set_useTitleFieldValueAsMediaItemTitle: function (value) {
        this._useTitleFieldValueAsMediaItemTitle = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentUploaderView', Sys.UI.Control);
