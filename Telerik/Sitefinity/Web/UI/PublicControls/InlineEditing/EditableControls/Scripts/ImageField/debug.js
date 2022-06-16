define(["EditableWindowField", "DetailViewEditingWindow", "text!ImageFieldTemplate!strip", "ImageSelector"], function (EditableWindowField, DetailViewEditingWindow, html, ImageSelector) {
    function ImageField(options) {
        var that = this;
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.fieldType = "ImageField";
        this.fieldName = options.fieldName;
        this.editWindowContentTemplate = html;
        this.originalValue = null;
        this.detailsViewInfo = null;
        this.dateFormat = "dddd, MMM d, yyyy";
        this.imageSelector = new ImageSelector({ siteBaseUrl: this.siteBaseUrl, culture: this.contentRepository.culture });
        this.detailViewEditingWindow = new DetailViewEditingWindow({
            closeEditingWindowHandler: Function.createDelegate(that, this.closeInlineEditingWindowHandler)
        });
        this.editWindowViewModel = kendo.observable({
            titleText: '',
            dataItem: null,
            changeImage: function (e) {
                e.preventDefault();
                that.imageSelector.show();
            },
            openImageInBackend: function (e) {
                e.preventDefault();
                that.openImageInBackend(e);
            },
            dateString: function (e) {
                var wcfDate = this.dataItem.DateCreated;
                if (wcfDate.match) {
                    return new Date(wcfDate.match(/\d+/)[0] * 1).format(that.dateFormat);
                }
                return wcfDate.format(that.dateFormat);
            }
        });

        return (this);
    }

    ImageField.prototype = {

        success: function (data, textStatus, jqXHR) {
            
            this.editWindowViewModel.set('dataItem', data.Image);
            this.detailsViewInfo = data.DetailsViewInfo;
            this.originalValue = data.Image;
            if (!this.isInitialized) {
                kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
                this.dialog.viewModel.set('titleText', this.editWindowViewModel.titleText);
                this.imageSelector.siteBaseUrl = this.siteBaseUrl;
                $(this.imageSelector).on("doneSelected", jQuery.proxy(this.onImageSelected, this));
                this.isInitialized = true;
            }
            this.viewModel.set("showEditButton", true);
        },

        isChanged: function () {
            if (!this.value) {
                return false;
            }
            var isNewUploadedItem = this.value.Status == "Master";
            if (isNewUploadedItem) {
                return true;
            }
            var isNewSelectedItem = (this.originalValue.Id !== this.value.Id) ||
                (this.originalValue.ProviderName !== this.value.ProviderName);
            if (isNewSelectedItem) {
                return true;
            }

            var isCurrentItemChanged =  (this.originalValue.Title !== this.value.Title) ||
                (this.originalValue.AlternativeText !== this.value.AlternativeText);

            if (isCurrentItemChanged) {
                return true;
            }
            
            return false;
        },

        
        openImageInBackend: function (e) {
            var data =
                {
                    ShowMoreActionsWorkflowMenu: false,
                    HideLanguageList: true,
                    DetailsViewUrl: this.detailsViewInfo.DetailsViewUrl,
                    ItemId: this.detailsViewInfo.ItemId,
                    ItemType: this.detailsViewInfo.ItemType,
                    ProviderName: this.detailsViewInfo.ProviderName,
                    CommandName: this.detailsViewInfo.CommandName,
                    Params: {
                        IsEditable: true,
                        parentId: this.detailsViewInfo.ParentId,
                        parentType: this.detailsViewInfo.ParentType
                    },
                    Culture: this.contentRepository.culture
                }
            this.detailViewEditingWindow.open(data);
        },

        closeInlineEditingWindowHandler: function (sender, args) {
            if (args.workflowOperationWasExecuted) {
                var parentId = sender.get_dataItem().Item.Album.Id;
                var originalContentId = sender.get_dataItem().Item.OriginalContentId;
                var that = this;
                this.contentRepository.getLiveImageData(parentId, originalContentId, this.detailsViewInfo.ProviderName,
                    function (data, textStatus, jqXHR) {
                        that.onImageSelected(null, [data]);
                });
            }
            this.detailViewEditingWindow.close();
        },


        onImageSelected: function (event, files) {
            var selectedFile = files;
            if (files && files.length) {
                selectedFile = files[0];
            }
            
            if (selectedFile) {

                this.editWindowViewModel.dataItem.Id = selectedFile.LiveContentId;
                this.editWindowViewModel.dataItem.ProviderName = selectedFile.ProviderName;
                this.editWindowViewModel.set('dataItem.ThumbnailUrl', selectedFile.ThumbnailUrl);
                this.editWindowViewModel.set('dataItem.DateCreated', selectedFile.DateCreated);
                this.editWindowViewModel.set('dataItem.TotalSize', selectedFile.TotalSize);
                this.editWindowViewModel.set('dataItem.Extension', selectedFile.Extension);
                this.editWindowViewModel.set('dataItem.AlternativeText', selectedFile.AlternativeText);
                this.editWindowViewModel.set('dataItem.Title', selectedFile.Title);
                this.editWindowViewModel.set('dataItem.Status', "Master");
                this.value = jQuery.extend(true, {}, this.editWindowViewModel.dataItem);

                this.detailsViewInfo.ItemId = selectedFile.LiveContentId;
                this.detailsViewInfo.ParentId = selectedFile.ParentId;
                this.detailsViewInfo.ProviderName = selectedFile.ProviderName;
            }
        },

        onDone: function (event, sender) {
            var imageItem = [{
                Name: this.fieldName,
                Value: null,
                ComplexValue: [
                    { Name: "Title", Value: this.editWindowViewModel.dataItem.Title },
                    { Name: "AlternativeText", Value: this.editWindowViewModel.dataItem.AlternativeText },
                    { Name: "Id", Value: this.editWindowViewModel.dataItem.Id },
                    { Name: "ProviderName", Value: this.editWindowViewModel.dataItem.ProviderName }]
            }]
            
            this.value = jQuery.extend(true, {}, this.editWindowViewModel.dataItem);
            this.saveTempParams.data = imageItem;
            EditableWindowField.prototype.onDone.call(this, event, sender);
        },

        onClose: function (event, sender) {
            this.editWindowViewModel.set('dataItem', this.originalValue);
            EditableWindowField.prototype.onClose.call(this, event, sender);
        }

    };

    ImageField.prototype = $.extend(Object.create(EditableWindowField.prototype), ImageField.prototype);
    return (ImageField);
});

