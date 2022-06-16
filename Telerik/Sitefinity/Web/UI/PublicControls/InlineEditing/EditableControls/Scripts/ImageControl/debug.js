﻿define(["EditableWindowField", "DetailViewEditingWindow", "text!ImageControlTemplate!strip", "ImageSelector"], function (EditableWindowField, DetailViewEditingWindow, html, ImageSelector) {
    function ImageControl(options) {
        var that = this;
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.fieldType = "ImageControl";
        this.fieldName = "";
        this.editWindowContentTemplate = html;
        this.originalValue = null;
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
                var wcfDate = this.dataItem.Image.DateCreated;
                var jsDate = new Date(wcfDate.match(/\d+/)[0] * 1);
               
                return jsDate.format(that.dateFormat);
            }
        });

        return (this);
    }

    ImageControl.prototype = {

        success: function (data, textStatus, jqXHR) {
            this.isInEdit = true;
            this.originalValue = data;
            this.editWindowViewModel.set('dataItem', data);
            
            kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
            this.dialog.viewModel.set('titleText', this.editWindowViewModel.titleText);
            this.imageSelector.siteBaseUrl = this.siteBaseUrl;
            $(this.imageSelector).on("doneSelected", jQuery.proxy(this.onImageSelected, this));
            
            this.viewModel.set("showEditButton", true);

            this.isInitialized = true;
        },

        openImageInBackend: function (e) {
            var detailsViewInfo = this.editWindowViewModel.dataItem.DetailsViewInfo;
            var data =
                {
                    ShowMoreActionsWorkflowMenu: false,
                    HideLanguageList: true,
                    DetailsViewUrl: detailsViewInfo.DetailsViewUrl,
                    ItemId: detailsViewInfo.ItemId,
                    ItemType: detailsViewInfo.ItemType,
                    ProviderName: detailsViewInfo.ProviderName,
                    CommandName: detailsViewInfo.CommandName,
                    Params: {
                        IsEditable: true,
                        parentId: detailsViewInfo.ParentId,
                        parentType: detailsViewInfo.ParentType,
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
                this.contentRepository.getLiveImageData(parentId, originalContentId, this.editWindowViewModel.dataItem.DetailsViewInfo.ProviderName,
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
                this.editWindowViewModel.dataItem.ControlData.ImageId = selectedFile.LiveContentId;
                this.editWindowViewModel.dataItem.ControlData.ProviderName = selectedFile.ProviderName;
                this.editWindowViewModel.set('dataItem.Image.ThumbnailUrl', selectedFile.ThumbnailUrl);
                this.editWindowViewModel.set('dataItem.Image.DateCreated', selectedFile.DateCreated);
                this.editWindowViewModel.set('dataItem.Image.TotalSize', selectedFile.TotalSize);
                this.editWindowViewModel.set('dataItem.Image.Extension', selectedFile.Extension);

                this.editWindowViewModel.dataItem.DetailsViewInfo.ItemId = selectedFile.LiveContentId;
                this.editWindowViewModel.dataItem.DetailsViewInfo.ParentId = selectedFile.ParentId;
                this.editWindowViewModel.dataItem.DetailsViewInfo.ProviderName = selectedFile.ProviderName;
            }
        },

        isChanged: function () {
            if (this.value) {
                return this.isDataItemChanged() /*|| this.isControlDataChanged()*/;
            }
            return false;
        },
        applyChanges: function() {
            this.value = this.editWindowViewModel.dataItem.toJSON();
            this.originalValue = this.value;
        },
        onDone: function (event, sender) {
            this.value = this.editWindowViewModel.dataItem.toJSON();
            this.complexValue = this.getComplexValue();
            this.saveTempParams.data = this.complexValue;
            EditableWindowField.prototype.onDone.call(this, event, sender);
            this.isInEdit = false;
        },

        onClose: function (event, sender) {
            this.editWindowViewModel.set('dataItem', this.originalValue);
            EditableWindowField.prototype.onClose.call(this, event, sender);
            this.isInEdit = false;
        },

        getComplexValue: function () {
            var imageItem = [
                   { Name: "ImageId", Value: this.editWindowViewModel.dataItem.ControlData.ImageId },
                   { Name: "ProviderName", Value: this.editWindowViewModel.dataItem.ControlData.ProviderName }
            ]
            return imageItem;
        },
        isDataItemChanged: function() {
            return JSON.stringify(this.value.Image) !== JSON.stringify(this.originalValue.Image);
        }
    };

    ImageControl.prototype = $.extend(Object.create(EditableWindowField.prototype), ImageControl.prototype);
    return (ImageControl);
});

