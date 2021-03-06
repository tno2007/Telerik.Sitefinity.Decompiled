/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView.initializeBase(this, [element]);

    this._uiCulture = null;
    this._contentType = null;
    this._parentType = null;
    this._providerName = null;
    this._fileUpload = null;
    this._parentId = null;
    this._workflowOperation = "Upload";
    this._contentWebServiceUrl = null;
    this._blankDataItem = null;
    this._filesSelectedDelegate = null;
    this._fileRemovedDelegate = null;
    this._fileUploadedDelegate = null;
    this._fileUploadFailedDelegate = null;
    this._fileUploadWrapper = null;
    this._mediaItemFieldsControl = null;
    this._cantUploadFilesErrorMessage = null;
    this._allowedExtensions = null;
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView.callBaseMethod(this, 'initialize');

        if (this.get_fileUpload()) {
            if (!this._filesSelectedDelegate) {
                this._filesSelectedDelegate = Function.createDelegate(this, this._filesSelected);
            }
            if (!this._fileRemovedDelegate) {
                this._fileRemovedDelegate = Function.createDelegate(this, this._fileRemoved);
            }
            if (!this._fileUploadedDelegate) {
                this._fileUploadedDelegate = Function.createDelegate(this, this._fileUploaded);
            }
            if (!this._fileUploadFailedDelegate) {
                this._fileUploadFailedDelegate = Function.createDelegate(this, this._fileUploadFailed);
            }

            this.get_fileUpload().add_filesSelected(this._filesSelectedDelegate);
            this.get_fileUpload().add_fileRemoved(this._fileRemovedDelegate);
            this.get_fileUpload().add_fileUploaded(this._fileUploadedDelegate);
            this.get_fileUpload().add_fileUploadFailed(this._fileUploadFailedDelegate);
        }

        if (this._blankDataItem) {
            this._blankDataItem = this._deserializeBlankDataItem(this._blankDataItem);
        }

        if (this.get_allowedExtensions()) {
            this.get_fileUpload()._allowedExtensions = this.get_allowedExtensions();
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView.callBaseMethod(this, 'dispose');

        if (this.get_fileUpload()) {
            if (this._filesSelectedDelegate) {
                this.get_fileUpload().remove_filesSelected(this._filesSelectedDelegate);
                delete this._filesSelectedDelegate;
            }
            if (this._fileRemovedDelegate) {
                this.get_fileUpload().remove_fileRemoved(this._fileRemovedDelegate);
                delete this._fileRemovedDelegate;
            }
            if (this._fileUploadedDelegate) {
                this.get_fileUpload().remove_fileUploaded(this._fileUploadedDelegate);
                delete this._fileUploadedDelegate;
            }
            if (this._fileUploadFailedDelegate) {
                this.get_fileUpload().remove_fileUploadFailed(this._fileUploadFailedDelegate);
                delete this._fileUploadFailedDelegate;
            }
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    rebind: function (providerName) {
        this._providerName = providerName;
        this.get_mediaItemFieldsControl().resetProviderControls();
        this.get_mediaItemFieldsControl().rebind(providerName);
    },
    
    allowCreateNewLibrary: function (allow) {
        if (this.get_mediaItemFieldsControl()) {
            this.get_mediaItemFieldsControl().allowCreateNewLibrary(allow);
        }        
    },

    reset: function () {
        this.get_fileUpload().reset();
        this.get_mediaItemFieldsControl().reset();
        jQuery(this.get_mediaItemFieldsControl().get_element()).hide();
    },

    /* -------------------- events -------------------- */

    add_onFileChanged: function (delegate) {
        this.get_events().addHandler('onFileChanged', delegate);
    },
    remove_onFileChanged: function (delegate) {
        this.get_events().removeHandler('onFileChanged', delegate);
    },

    add_onFileUploading: function (delegate) {
        this.get_events().addHandler('onFileUploading', delegate);
    },
    remove_onFileUploading: function (delegate) {
        this.get_events().removeHandler('onFileUploading', delegate);
    },

    add_onFileUploaded: function (delegate) {
        this.get_events().addHandler('onFileUploaded', delegate);
    },
    remove_onFileUploaded: function (delegate) {
        this.get_events().removeHandler('onFileUploaded', delegate);
    },

    /* --------------------------------- event handlers --------------------------------- */

    _fileChangedHandler: function (file) {
        var eventArgs = file;
        var h = this.get_events().getHandler('onFileChanged');
        if (h) h(this, eventArgs);
    },

    _fileUploadingHandler: function (item) {
        var eventArgs = item;
        var h = this.get_events().getHandler('onFileUploading');
        if (h) h(this, eventArgs);
    },

    _fileUploadedHandler: function (uploadedItem) {
        var eventArgs = uploadedItem;
        var h = this.get_events().getHandler('onFileUploaded');
        if (h) h(this, eventArgs);
    },

    _filesSelected: function (sender, args) {
        var selectedFile = null;
        if (args && args.files.length > 0) {
            selectedFile = args.files[0].name;
        }
     
        this._fileChangedHandler(args);

        if (!args.isDefaultPrevented()) {
            var dataObject = this.get_mediaItemFieldsControl().get_blankDataItem();
            dataObject = this._clone(dataObject);

            //TODO: send also the extension because the method is substracting it from the file name
            this._setDefaultProperties(dataObject, selectedFile);

            this.get_mediaItemFieldsControl().set_dataItem(dataObject);
            jQuery(this.get_fileUploadWrapper()).removeClass("sfLeftCol");
        }
    },

    _fileRemoved: function (sender, args) {
        this._fileChangedHandler(null);
    },

    _fileUploaded: function (sender, args) {
        this._fileUploadedHandler(args._file);
    },

    _fileUploadFailed: function (sender, fileUploadFailedArgs) {
        //TODO: gather exceptions like this (fileUploadFailedArgs._exception) and remove alert;
    },

    _contentItemSave_Success: function (caller, result, requestContext) {
        //get SF_UI_CULTURE from the headers of the request
        var language = requestContext._headers[Telerik.Sitefinity.Data.ClientManager.UiCultureHeaderKey];
        caller.get_fileUpload().uploadFiles([result.Item.Id], caller._workflowOperation, language, true);
    },

    _contentItemSave_Failure: function (result) {
        alert(result.Detail);
    },

    /* --------------------------------- private methods --------------------------------- */
    _saveMediaItems: function () {
        var selectedFiles = this.get_fileUpload().get_selectedFiles();
        var itemsToUploadCount = selectedFiles.length;
        if (itemsToUploadCount <= 0) {
            return;
        }

        var contentId = Telerik.Sitefinity.getEmptyGuid();
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this._contentWebServiceUrl;
        var urlParams = {
            itemType: this._contentType,
            provider: this._providerName,
            parentItemType: this._parentType,
        };
        
        if (this._uiCulture)
            clientManager.set_uiCulture(this._uiCulture);

        //var blankItem = this._createCommonMediaItem(this._blankDataItem);

        for (var filesIter = 0; filesIter < itemsToUploadCount; filesIter++) {            
            var mediaItem = this._createMediaItem(selectedFiles[filesIter]);

            var parentId = this.get_mediaItemFieldsControl().getSelectedParentId();

            // set the Library to null and ParentId to Empty GUID. Otherwise the default library is passed to the server
            // and the item could not be uploaded if a user hasn't been grated with appropriate permissions
            // The parentId is passed to the clientManager.InvokePut, so later on the server the parent is set properly.
            if (mediaItem.Item !== null && mediaItem.Item.Library !== undefined) {
                mediaItem.Item.Library = null;
            }
            else if (mediaItem.Item !== null && mediaItem.Item.Album !== undefined) {
                mediaItem.Item.Album = null;
            }
            mediaItem.Item.ParentId = Telerik.Sitefinity.getEmptyGuid();

            if (!this.get_mediaItemFieldsControl().validate()) {
                return false;
            }

            this._fileUploadingHandler(selectedFiles[filesIter]);

            urlParams.newParentId = parentId;
            this.get_fileUpload().set_libraryId(parentId);
            this.get_fileUpload().set_libraryProviderName(this._providerName);
            var keys = ['parent', parentId, contentId];

            var context = { uploadOrdinal: filesIter };
            clientManager.InvokePut(serviceUrl, urlParams, keys, mediaItem, this._contentItemSave_Success, this._contentItemSave_Failure, this, null, null, context);
        }
    },

    _createMediaItem: function (fileName, blankItem) {
        return this._clone(this.get_mediaItemFieldsControl().get_dataItem());
    },

    _createCommonMediaItem: function (blankItem) {
        var dataObject = this._clone(blankItem);
        dataObject.UIStatus = 0;
        return dataObject;
    },

    _deserializeBlankDataItem: function (blankDataItem) {
        blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(blankDataItem);
        if (blankDataItem.hasOwnProperty('Id')) {
            delete blankDataItem.Id;
        }
        return blankDataItem;
    },

    _setDefaultProperties: function (dataItem, fileName) {
        dataItem.Id = Telerik.Sitefinity.getEmptyGuid();
        var extension = this._getExtension(fileName);
        var extensionIndex = fileName.lastIndexOf('.');
        if (extensionIndex > 0) {
            fileName = fileName.slice(0, extensionIndex);
        }
        dataItem.Title.PersistedValue = fileName;
        dataItem.Title.Value = fileName;
        dataItem.Extension = extension;
    },

    _getExtension: function (fileName) {
        var extension = "";
        var extensionIndex = fileName.lastIndexOf('.');
        if (extensionIndex > 0) {
            extension = fileName.slice(extensionIndex, fileName.length);
        }
        return extension;
    },

    _clone: function (obj) {
        var clone = jQuery.extend(true, {}, obj);
        this._fixClone(clone, obj);
        return clone;
    },

    _fixClone: function (obj, original) {
        for (var property in obj) {
            var val = original[property];
            if (val && (typeof val === 'object')) {
                if (val.constructor === Date) {
                    obj[property] = val;
                }
                else {
                    this._fixClone(obj[property], val);
                }
            }
        }
    },

    /* --------------------------------- properties --------------------------------- */

    get_fileUpload: function () {
        return this._fileUpload;
    },
    set_fileUpload: function (value) {
        this._fileUpload = value;
    },
    get_parentId: function () {
        return this._parentId;
    },
    set_parentId: function (value) {
        this._parentId = value;
    },
    get_mediaItemFieldsControl: function () {
        return this._mediaItemFieldsControl;
    },
    set_mediaItemFieldsControl: function (value) {
        this._mediaItemFieldsControl = value;
    },

    get_fileUploadWrapper: function () {
        return this._fileUploadWrapper;
    },
    set_fileUploadWrapper: function (value) {
        this._fileUploadWrapper = value;
    },
    get_cantUploadFilesErrorMessage: function () {
        return this._cantUploadFilesErrorMessage;
    },
    set_cantUploadFilesErrorMessage: function (value) {
        this._cantUploadFilesErrorMessage = value;
    },
    get_allowedExtensions: function () {
        return this._allowedExtensions;
    },
    set_allowedExtensions: function (value) {
        this._allowedExtensions = value;
        this.get_fileUpload()._allowedExtensions = value;
    },

    get_uiCulture: function () {
        return this._uiCulture;
    },
    set_uiCulture: function (value) {
        this._uiCulture = value;
        if (this.get_mediaItemFieldsControl())
            this.get_mediaItemFieldsControl().set_uiCulture(value);
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemUploaderView', Sys.UI.Control);
