﻿define(["text!ImageSelectorTemplate!strip", "DialogBase", "LibrariesSelector", "ProvidersSelector"], function (html, DialogBase, LibrariesSelector, ProvidersSelector) {
    function ImageSelector(options) {
        var that = this;
      
        this.template = html;
        this.originalValue = null;
        this.dialog = new DialogBase();
        this.initialized = false;
        this.maxFilesCount = 1;
        this.selectedFilesCount = 0;
        this.uploadedFiles = [];
        this.selectedFiles = [];
        this.closeCallback = null;
        this.uploadWidget = null;
        this.librariesDialog = new DialogBase();
        this.providersDialog = new DialogBase();
        this.librariesSelector = new LibrariesSelector({ parentElement: this.librariesDialog.getContentPlaceHolder()});
        this.providersSelector = new ProvidersSelector({ parentElement: this.providersDialog.getContentPlaceHolder()});
        this.albumServiceBaseUrl = "Sitefinity/Services/Content/AlbumService.svc/";
        this.imageServiceBaseUrl = "Sitefinity/Services/Content/ImageService.svc/parent/";
        this.providersServiceBaseUrl = "Sitefinity/Services/DataSourceService/providers/";
        this.dataSourceUrl = null;
        this.queryParams = null;
        this.emptyGuid = "00000000-0000-0000-0000-000000000000";
        this.rootFolder = { Id: this.emptyGuid, Title: "Root" };
        this.timerId = null;
        this.inputDelay = 500;
        this.culture = options.culture;
        this.siteBaseUrl = options.siteBaseUrl;

        this.viewModel = kendo.observable({
            foldersDataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        type: "GET",
                        dataType: "json",
                        url: function (options) {
                            return that.siteBaseUrl + that.dataSourceUrl;
                        },
                        beforeSend: function (xhr) {
                            if (that.culture) {
                                xhr.setRequestHeader("SF_UI_CULTURE", that.culture);
                            }
                        }
                    },
                    parameterMap: function (options) {
                        if (that.isSearchRequest()) {
                            if (that.viewModel.foldersDataSource.page() > 1) {
                                that.queryParams.skip = options.skip;
                            } else {
                                that.queryParams.skip = 0;
                            }
                            
                            that.queryParams.take = 20;
                            that.queryParams.filter = String.format('((Visible == true AND Status == Live) AND (Title.Contains("{0}")))', that.viewModel.searchValue);
                        } else {
                            that.queryParams.skip = options.skip;
                            that.queryParams.take = options.take;
                            if (that.isOnRootLevel()) {
                                that.queryParams.filter = '';
                            } else {
                                that.queryParams.filter = '(Visible == true AND Status == Live)';
                            }
                        }
                        return that.queryParams;
                    }
                },
                schema: {
                    data: "Items",
                    total: "TotalCount",
                    model: {
                        id: "Id"
                    }
                },
                pageSize: 20,
                page: 1,
                serverFiltering: true,
                serverPaging: true
            }),
            onDataBound: function(e) {
                if (this.foldersDataSource.totalPages() <= 1) {
                    that.dialog.getContentPlaceHolder().find("[data-role=pager]").hide();
                } else {
                    that.dialog.getContentPlaceHolder().find("[data-role=pager]").show();
                }
            },
            providersDataSource: new kendo.data.DataSource({
                transport: {
                    read: {
                        type: "GET",
                        dataType: "json",
                        url: function (options) {
                            return that.siteBaseUrl + that.providersServiceBaseUrl;
                        }
                    },
                    parameterMap: function (options) {
                        var queryObj = {
                            sortExpression: "Title ASC",
                            dataSourceName: "Telerik.Sitefinity.Modules.Libraries.LibrariesManager",
                            take: 5,
                            siteId: that.viewModel.siteId
                        };
                        return queryObj;
                    }
                },
                schema: {
                    data: "Items",
                    total: "TotalCount",
                    model: {
                        id: "Name"
                    }
                },
                serverFiltering: true
            }),
            onProvidersDropDownClick: function(e) {
                this.set('providersDropDownVisible', !this.providersDropDownVisible);
            },
            onProviderDropDownSelected: function (e) {
                var data = e.sender.dataSource.view();
                var selectedItems = $.map(e.sender.select(), function (item) {
                    return data[$(item).index()];
                });
                var dataItem = selectedItems[0];
                if (dataItem !== this.selectedProvider) {
                    this.set('selectedProvider', dataItem);
                    that.rebind();
                }
                this.set('providersDropDownVisible', false);
            },
            onProviderDropDownDataBound: function(e) {
                this.set('providersClickerVisible', e.sender.dataSource.view().length > 1);
            },
            onImagePreUpload: function (e) {
                e.data = {
                    ContentType: "Telerik.Sitefinity.Libraries.Model.Image",
                    ProviderName: this.selectedProvider.Name,
                    LibraryId: this.selectedLibrary.Id,
                    Culture: that.culture,
                    Workflow: "Upload"
                }
            },
            onImageUploadSuccess: function (e) {
                for (var i = 0; i < e.response.length; i++) {
                    that.uploadedFiles.push(e.response[i].ContentItem);
                }
                $(that).trigger("doneSelected", that.uploadedFiles);
                that.dialog.close();
            },
            onImageUploadSelected: function (e) {
                if (e.files.length + that.selectedFilesCount > that.maxFilesCount) {
                    e.preventDefault();
                }
                else {
                    that.selectedFilesCount += e.files.length;
                }
                if (that.selectedFilesCount > 0) {
                    e.sender.element.data("kendoUpload").wrapper.removeClass("sfNoFilesSelected").addClass("sfFilesSelected");
                }
            },
            onImageUploadComplete: function (e) {
                that.resetUploader($(e.sender.element).data("kendoUpload"));
            },
            onImageUploadFail: function(e) {
                //TODO/6
            },
            onImageUploadRemove: function (e) {
                that.selectedFilesCount -= e.files.length;
                if (that.selectedFilesCount == 0) {
                    that.uploadWidget.wrapper.removeClass("sfFilesSelected").addClass("sfNoFilesSelected");
                }
            },
            
            refreshView: function (e) {
                var sender = e.srcElement || e.target;
                if (sender.value == "selectImageView") {
                    this.set("selectImageViewVisible", true);
                    this.set("uploadImageViewVisible", false);
                    that.dialog.viewModel.set('doneButtonText', "Done");
                }
                else if (sender.value == "uploadImageView") {
                    this.set("selectImageViewVisible", false);
                    this.set("uploadImageViewVisible", true);
                    that.dialog.viewModel.set('doneButtonText', this.uploadAndPublishText);
                }
            },
            breadCrumb: [],
            onBreadCrumbSelected: function(e) {
                var data = e.sender.dataSource.view();
                var selectedItems = $.map(e.sender.select(), function (item) {
                    return data[$(item).index()];
                });
                var dataItem = selectedItems[0];
                var breadCrumbIndex = that.getBreadCrumbIndex(dataItem);
                var lastItem = this.breadCrumb[this.breadCrumb.length - 1];
                this.breadCrumb.splice(breadCrumbIndex + 1, this.breadCrumb.length - breadCrumbIndex);
                //check if the last folder in the hierarchy was clicked
                if (dataItem.Id != lastItem.Id) {
                    this.set('searchValue', '');
                    //check for the root folder
                    if (dataItem.Id == that.emptyGuid) {
                        that.configureDataSourceForRoot();
                        this.set('breadCrumb', []);
                    } else {
                        that.configureDataSourceForChildren(dataItem);
                    }
                    this.foldersDataSource.page(1);
                }
            },
            folderSelected: function (e) {
                var data = this.foldersDataSource.view();
                var selectedItems = $.map(e.sender.select(), function (item) {
                    return data[$(item).index()];
                });
                var dataItem = selectedItems[0];
                that.selectedFiles = selectedItems;
                that.currentFolder = dataItem;
                if (dataItem.LibraryType || dataItem.IsFolder) {
                    that.configureDataSourceForChildren(dataItem);
                    if (this.breadCrumb.length === 0) {
                        this.set('breadCrumb', [that.rootFolder]);
                    }
                    this.breadCrumb.push(dataItem);
                    this.foldersDataSource.page(1);
                }

                // After kendo update when you push a new item to a collection binded to a view. The view is updated only for the last item
                // With this setter we trigger an update to the whole collection.
                this.set('breadCrumb', this.breadCrumb.toJSON());
            },
            searchValue: null,
            onSearch: function (e) {
                var that = this;
                if (that._timerId != null) {
                    clearTimeout(that._timerId);
                }
                that._timerId = setTimeout(function () {
                    that.foldersDataSource.page(1);
                }, that.inputDelay);
            },
            searchEnabled: function() {
                return !that.isOnRootLevel();
            },
            selectImageViewVisible: true,
            uploadImageViewVisible: false,
            providersDropDownVisible: false,
            providersClickerVisible: false,
            openProvidersLinkVisible: function () {
                return (this.get('providersDataSource').total() > 5);
            },
            onSelectLibrary: function (e) {
                that.librariesDialog.open();
            },
            openProvidersDialog: function (e) {
                that.providersDialog.open();
            },
            selectedLibrary: null,
            selectedProvider: {
                Name: "",
                Title: function () { return that.viewModel.defaultSourceText; }
            },
            isMultilingual: "False",
            uploadAndPublishText: "",
            sourceText: "",
            imageLibraryText: "",
            selectImageText: "",
            allProvidersText: "",
            defaultSourceText: "",
            allLibrariesText: "",
            selectText: "",
            changeText: "",
            siteId: null,
            selectLibraryText: function() {
                if (this.selectedLibrary) {
                    return this.changeText;
                }
                return this.selectText;
            },
            allProvidersTextFormatted: function () {
                return String.format(this.allProvidersText, this.get('providersDataSource').total());
            }
        });
    }

    ImageSelector.prototype = {

        show: function () {
            this.initialize();
            this.dialog.open();
            $(this.dialog.kendoWindow.wrapper).width("650px");
            this.dialog.center();
        },

        initialize: function () {
            this.uploadedFiles = [];
            
            this.selectedFilesCount = 0;
            if (!this.initialized) {
                this.configureDataSourceForRoot();
                this.dialog.getContentPlaceHolder().html(this.template);
                kendo.bind(this.dialog.getContentPlaceHolder(), this.viewModel);
                this.rootFolder.Title = this.viewModel.allLibrariesText;
                this.uploadWidget = this.dialog.getContentPlaceHolder().find("#kendoUpload").data("kendoUpload");
                this.uploadWidget.options.async.saveUrl = this.siteBaseUrl + "Telerik.Sitefinity.Html5UploadHandler.ashx";
                this.uploadWidget.wrapper.addClass("sfLibUploadWrp sfNoFilesSelected");
                this.librariesSelector.siteBaseUrl = this.siteBaseUrl;
                this.providersSelector.siteBaseUrl = this.siteBaseUrl;
                this.librariesSelector.init();
                this.providersSelector.init();
                this.librariesDialog.viewModel.set('titleText', this.viewModel.imageLibraryText);
                this.providersDialog.viewModel.set('titleText', this.viewModel.sourceText);
                $(this.dialog).on("doneSelected", jQuery.proxy(this.onDoneSelected, this));
                $(this.dialog).on('closeSelected', jQuery.proxy(this.onCloseSelected, this));
                $(this.librariesDialog).on('doneSelected', jQuery.proxy(this.onLibrarySelected, this));
                $(this.providersDialog).on('doneSelected', jQuery.proxy(this.onProviderSelected, this));
                $(this.librariesDialog).on('closeSelected', jQuery.proxy(this.onCloseSelected, this));
                $(this.providersDialog).on('closeSelected', jQuery.proxy(this.onCloseSelected, this));

                this.dialog.viewModel.set('titleText', this.viewModel.selectImageText);
                this.initialized = true;
            }

        },
        onLibrarySelected: function (event, sender) {
            var selectedLibrary = this.librariesSelector.getSelectedDataItems()[0];
            if (selectedLibrary) {
                this.viewModel.set('selectedLibrary', selectedLibrary);
            }
            sender.close();
        },
        onProviderSelected: function(event, sender) {
            var selectedProvider = this.providersSelector.getSelectedDataItems()[0];
            if (selectedProvider) {
                this.viewModel.set('selectedProvider', selectedProvider);
                this.rebind();
            }
            this.viewModel.set('providersDropDownVisible', false);
            sender.close();
        },

        onCloseSelected: function (event, sender) {
            sender.close();
        },

        onDoneSelected: function (event, sender) {
            if (this.viewModel.uploadImageViewVisible) {
                if (this.viewModel.selectedLibrary == null) {
                    alert('Select a library');
                } else {
                    this.uploadWidget._module.onSaveSelected();
                }
            } else {
                $(this).trigger("doneSelected", this.selectedFiles);
                sender.close();
            }
            
        },

        rebind: function () {
            this.viewModel.set('breadCrumb', [this.rootFolder]);
            this.viewModel.set('searchValue', '');
            this.configureDataSourceForRoot();
            this.librariesSelector.setProviderName(this.viewModel.selectedProvider.Name);
            this.viewModel.foldersDataSource.read();
            this.viewModel.set('selectedLibrary', null);
        },

        resetUploader: function (kendoUpload) {
            kendoUpload.wrapper.find('ul').remove();
            kendoUpload.wrapper.removeClass("sfFilesSelected").addClass("sfNoFilesSelected");
            kendoUpload.wrapper.find("span.sfFilesSelectedLbl").show();
        },

        isSearchRequest: function () {
            var searchValue = this.viewModel.searchValue;
            if (searchValue && searchValue != "") {
                return true;
            }
            return false;
        },

        isOnRootLevel: function() {
            return (this.viewModel.get('breadCrumb').length <= 1);
        },

        configureDataSourceForRoot: function () {
            this.dataSourceUrl = this.albumServiceBaseUrl;
            this.queryParams = {
                provider: this.viewModel.selectedProvider.Name,
                sortExpression: "Title ASC",
                itemType: "Telerik.Sitefinity.Libraries.Model.Album"
            }
        },

        configureDataSourceForChildren: function (dataItem) {
            this.dataSourceUrl = this.imageServiceBaseUrl + dataItem.Id + "/";
            this.queryParams = {
                provider: this.viewModel.selectedProvider.Name,
                itemType: "Telerik.Sitefinity.Libraries.Model.Image",
                filter: '(Visible == true AND Status == Live)'
            }
        },

        getBreadCrumbIndex: function (dataItem) {
            var completeBreadCrumb = this.viewModel.breadCrumb;
            for (var i = 0; i < completeBreadCrumb.length; i++) {
                if (completeBreadCrumb[i].Id == dataItem.Id) {
                    return i;
                }
            }
            return -1;
        },

        isMultilingual: function () {
            return Boolean.parse(this.viewModel.isMultilingual);
        }
    };

    return (ImageSelector);
});
