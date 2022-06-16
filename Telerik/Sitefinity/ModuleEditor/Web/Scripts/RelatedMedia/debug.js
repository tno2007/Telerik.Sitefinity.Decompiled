﻿//define(["ContentBase", "text!RelatedMediaTemplate!strip"], function (EditableWindowField, html) {
define(["text!RelatedMediaTemplate!strip"], function (html) {
    function RelatedMedia(options) {
        //ContentBase.call(this, options);
        var that = this;
       
        this.fieldType = "RelatedMedia";
        this.inlineFrontendControl = 'inline';
        this.selectedFieldType = {};
        this.template = html;
        this.siteBaseUrl = options.siteBaseUrl;
        this.originalDataModel = options.dataModel;
        this.validatorSelectors = {
            genral: "#typeFieldEditor_Tabstrip-1",
            limitations: "#typeFieldEditor_Tabstrip-2"
        };
        this.selectors = {
            dropdownProviders: "#mediaDataProviders"
        };
        this.validators = {
            generalValidator: null,
            limitationsValidator: null
        };

        this.modifiedDataModel = {
            field: {}
        };

        $.extend(true, this.modifiedDataModel.field, options.field);
        
        this.viewModel = kendo.observable({
            currentValue: "RelatedMedia",
            options: options,
            field: this.modifiedDataModel.field,
            allTypeAllowed: (this.modifiedDataModel.field.FileExtensions === "" || this.modifiedDataModel.field.FileExtensions === null) && (options.mediaType.isFile),
            selectAllTypes: false,
            isWidgetTypeNameVisible: false,
            isFrontendWidgetTypeNameVisible: false,
            imageGalleryWidgetName: 'Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImagesView',
            done: function (e, s) {
                e.preventDefault();
                that.done(e, s);
            },
            cancel: function (e, s) {
                e.preventDefault();
                that.cancel(e, s);
            },
            back: function (e, s) {
                e.preventDefault();
                that.back(e, s);
            },
            widgetTypeNameChanged: function (e, s) {
                var selectedValue = $(e.target).val();
                var isCustom = e.target.selectedIndex !== 0;
                that.viewModel.set("isWidgetTypeNameVisible", isCustom);

                if (!isCustom) {
                    $('#widgetSelectorVirtualPath').removeAttr('required');
                }
                else {
                    if (selectedValue === 'custom') {
                        selectedValue = '';
                    }
                    $('#widgetSelectorVirtualPath').attr('required', 'required');
                }
                that.viewModel.set("field.WidgetTypeName", selectedValue);

                that.resizeToContent();
            },
            frontendWidgetTypeNameChanged: function (e, s) {
                var selectedValue = $(e.target).find(":selected").val();
                var isCustom = selectedValue === $(e.target).find(":last").val();
                that.viewModel.set("isFrontendWidgetTypeNameVisible", isCustom);

                if (!isCustom) {
                    $('#frontendWidgetSelectorVirtualPath').removeAttr('required');
                }
                else {
                    if (selectedValue === 'custom') {
                        selectedValue = '';
                    }
                    $('#frontendWidgetSelectorVirtualPath').attr('required', 'required');
                }
                that.viewModel.set("field.FrontendWidgetTypeName", selectedValue);

                that.resizeToContent();
            },
            allowMultipleChanged: function (e) {
                if (!that.viewModel.get("isFrontendWidgetTypeNameVisible")) {
                    that.viewModel.set("field.FrontendWidgetTypeName", that.inlineFrontendControl);
                }
            },
            tabActivate: function (e) {
                that.resizeToContent();
            },
            allTypesChanged: function (e) {
                that.viewModel.set("selectAllTypes", that.viewModel.allTypeAllowed !== "true");
                that.resizeToContent();
            },
            getDefaultWidgetType: function (field) {
                return 'Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField';
            },
            getDefaultFrontendWidgetType: function (field) {
                var widgetType = ''
                switch (field.MediaType) {
                    case 'image':
                        if (field.AllowMultipleImages === "true" || field.AllowMultipleImages === true) {
                            widgetType = 'Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.ImagesView';
                        }
                        else {
                            widgetType = 'Telerik.Sitefinity.Web.UI.PublicControls.ImageControl';
                        }
                        break;
                    case 'video':
                        if (field.AllowMultipleVideos === "true" || field.AllowMultipleVideos === true) {
                            widgetType = 'Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.VideosView';
                        }
                        else {
                            widgetType = 'Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos.MediaPlayerControl';
                        }
                        break;
                    case 'file':
                        if (field.AllowMultipleFiles === "true" || field.AllowMultipleFiles === true) {
                            widgetType = 'Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.DownloadListView';
                        }
                        else {
                            widgetType = 'Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.DocumentLink';
                        }
                        break;
                }
                return widgetType;
            }
        });
        that.viewModel.set("selectAllTypes", (!that.viewModel.allTypeAllowed) && (options.mediaType.isFile));
        that.resizeToContent();
        // Return this object reference.
        return (this);
    };

    RelatedMedia.prototype = {
        initialize: function () {
            var that = this;
            var getMediaProvidersDataSource = function () {
                return that.siteBaseUrl + "Sitefinity/Services/DataSourceService/providers/?siteId=00000000-0000-0000-0000-000000000000&dataSourceName=Telerik.Sitefinity.Modules.Libraries.LibrariesManager&addDefaultSiteProvider=true";
            }

            var mediaProvidersDataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: getMediaProvidersDataSource(),
                        dataType: "json"
                    }
                },
                schema: {
                    data: function (response) {
                        that.addRelatedMediaSources(response.Items);
                        var providers = response.Items;
                        for (var p = 0; p < providers.length; p++) {
                            if (providers[p].Name == "sf-site-default-provider" || providers[p].Name == "sf-any-site-provider") {
                                providers[p].Type = "system";
                            }
                        }
                        return response.Items;
                    }
                },
                group: { field: "Type" }
            });

            this.dataProvidersDropdown = $(this.selectors.dropdownProviders).kendoDropDownList({
                animation: false,
                dataTextField: "Title",
                dataValueField: "Name",
                fixedGroupTemplate: '',
                groupTemplate: ' ',
                dataSource: mediaProvidersDataSource
            }).data("kendoDropDownList");

            this.dataProvidersDropdown.list[0].classList.add("sfKendoDropdownSeparator");

            if (!options.isEditMode) {
                this.dataProvidersDropdown.select(0);
            }
            else {
                this.dataProvidersDropdown.value(this.viewModel.field.RelatedDataProvider);
            }

            var isWidgetTypeNameVisible = this.modifiedDataModel.field.WidgetTypeName !== this.viewModel.getDefaultWidgetType(this.modifiedDataModel.field) &&
                                          this.modifiedDataModel.field.WidgetTypeName !== 'default';
            this.viewModel.set('isWidgetTypeNameVisible', isWidgetTypeNameVisible);

            var isFrontendWidgetTypeNameVisible = this.modifiedDataModel.field.FrontendWidgetTypeName !== this.viewModel.getDefaultFrontendWidgetType(this.modifiedDataModel.field) &&
				this.modifiedDataModel.field.FrontendWidgetTypeName !== this.viewModel.imageGalleryWidgetName &&
				this.modifiedDataModel.field.FrontendWidgetTypeName !== 'default' &&
                this.modifiedDataModel.field.FrontendWidgetTypeName !== this.inlineFrontendControl;
            this.viewModel.set('isFrontendWidgetTypeNameVisible', isFrontendWidgetTypeNameVisible);
        },

        resizeToContent: function(){
            if (window.dialogBase) {
                dialogBase.resizeToContent();
            }
        },

        done: function (event, sender) {
            var that = this;
            this.viewModel.field.RelatedDataProvider = this.dataProvidersDropdown.value();

            if (that.viewModel.allTypeAllowed === "true") {
                that.viewModel.field.FileExtensions = "";
            }

            that.setWidgetTypes();
            that.modifiedDataModel.field = that.viewModel.field.toJSON();

            this.clearUI();
            if (that.validate()) {
                $(this).trigger('onFieldDone', that);
            }
        },

        cancel: function (event, sender) {
            var that = this;
            this.clearUI();
            $(this).trigger('onFieldCancel', that);
        },

        back: function (event, sender) {
            var that = this;
            this.clearUI();
            $(this).trigger('onFieldBack', that);
        },

        validate: function () {
            var that = this,
                valid = false;

            that.initializeValidators();
            $(that.validatorSelectors.genral).find('span[role="alert"]').remove();
            $(that.validatorSelectors.limitations).find('span[role="alert"]').remove();
            if (that.validators.generalValidator.validate()) {
                if (that.validators.limitationsValidator.validate()) {
                    valid = true;
                }
                else {
                    $($(that.validatorSelectors.limitations).parent().find('ul.k-tabstrip-items li:last').get(0)).click();
                    that.prepareValidator(that.validatorSelectors.limitations);
                }
            }
            else {
                $($(that.validatorSelectors.genral).parent().find('ul.k-tabstrip-items li:first').get(0)).click();
                that.prepareValidator(that.validatorSelectors.genral);
            }

            return valid;
        },

        initializeValidators: function () {
            var that = this;

            if (that.validators.generalValidator === null){
                that.validators.generalValidator = $(this.validatorSelectors.genral).kendoValidator().data("kendoValidator");
            }

            if (that.validators.limitationsValidator === null){
                that.validators.limitationsValidator = $(this.validatorSelectors.limitations).kendoValidator().data("kendoValidator");
            }
        },

        prepareValidator: function (selector) {
            $(selector).find('span[role="alert"]').removeAttr('style');
            $(selector).find('span[role="alert"]').removeClass('k-tooltip');
            $(selector).find('span[role="alert"]').addClass('sfError');
        },

        setWidgetTypes: function () {
            if (!this.viewModel.isFrontendWidgetTypeNameVisible && this.viewModel.field.FrontendWidgetTypeName === 'default') {
                this.viewModel.field.FrontendWidgetTypeName = this.viewModel.getDefaultFrontendWidgetType(this.viewModel.field);
            }

            if (!this.viewModel.isWidgetTypeNameVisible && this.viewModel.field.WidgetTypeName === 'default') {
                this.viewModel.field.WidgetTypeName = this.viewModel.getDefaultWidgetType(this.viewModel.field);
            }
        },

        addRelatedMediaSources: function (items) {
            var that = this;
            var addProvider = function (items, name, title) {
                var exists = false;
                for (var i = 0; i < items.length; i++) {
                    var current = items[i];
                    if (current.Name == name) {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                    items.push({ Name: name, Title: title });
            }
           
            addProvider(items, 'sf-any-site-provider', 'All sources for the current site');           
        },

        clearUI: function () {
            kendo.destroy($(this.selectors.dropdownProviders));
        }
    };

    //kendo custom bindings --------------------------------------

    kendo.data.binders.fileExtensions = kendo.data.Binder.extend({
        init: function (element, bindings, options) {
            //call the base constructor
            kendo.data.Binder.fn.init.call(this, element, bindings, options);

            var that = this;
            //listen for the change event of the element
            $(that.element).on("change", function () {
                that.change(); //call the change function
            });
        },
        refresh: function () {
            var that = this,
                value = that.bindings["fileExtensions"].get(),
                checkAll = false;

            if (value !== null && value !== 'undefined') {
                if ($(that.element).data('is-visible') && value === "") {
                    checkAll = true;
                }

                var currentValueArray = value !== null ? value.split(",") : [],
                    allValuesArray = value.split(","),
                    allCheckBoxes = $(that.element).find('input[data-value]'),
                    textarea = $(that.element).find('textarea'),
                    allCheckBoxesValues = [];

                $.each(allCheckBoxes, function (index, item) {
                    allCheckBoxesValues.push($(item).data("value"));
                });

                allCheckBoxesValues = allCheckBoxesValues.join(",").split(",");

                var otherValues = $.grep(currentValueArray, function (item, index) {
                    return (allCheckBoxesValues.indexOf(item) === -1);
                });
                
                $.each(allCheckBoxes, function (index, item) {
                    var currentItemName = $(item).data('value').split(",")[0];
                    if (allValuesArray.indexOf(currentItemName) !== -1 || checkAll) {
                        $(item).attr('checked', true);
                    }
                    else {
                        $(item).attr('checked', false);
                    }
                });

                textarea.val(otherValues);

                if (checkAll) {
                    this.bindings["fileExtensions"].set(allCheckBoxesValues.join(","));
                }
            }
        },
        change: function (e, s) {
            var that = this,
                element = this.element,
                values = [],
                value = "",
                currentValue = this.bindings["fileExtensions"].get(),
                currentValueArray = currentValue !== null ? currentValue.split(",") : [],
                allCheckBoxes = $(that.element).find('input[data-value]'),
                allCheckBoxesValues = [],
                textarea = $(that.element).find('textarea'),
                otherValues = textarea.val().replace(/\s/g, '');

            $.each(allCheckBoxes, function (index, item) {
                var name = $(item).data("value");
                allCheckBoxesValues.push(name);
                if (otherValues.indexOf(name) !== -1) {
                    $(item).attr('checked', true);
                    otherValues = $.grep(otherValues.split(","), function (item, index) {
                        return (item !== name);
                    }).join(",");
                }
                name = null;
            });

            $.each(allCheckBoxes, function (index, item) {
                if ($(item).prop('checked')) {
                    values.push($(item).data("value"));
                }
            });

            value = values.concat(otherValues).join(",");

            this.bindings["fileExtensions"].set(value);
        }
    });

    kendo.data.binders.relatedMediaWidgetTypeName = kendo.data.Binder.extend({
        refresh: function () {
            var that = this,
                element = this.element,
                vmValue = that.bindings.relatedMediaWidgetTypeName.get(),
                defaultType = '',
                isAnySiteSource = false;

            var defaultType = '';
            var viewModel = that.bindings.relatedMediaWidgetTypeName.source;

            $("#mediaDataProviders").bind('change', function (e) {
                viewModel.field.RelatedDataProvider = this.value;
                that.refresh();
                if (viewModel.field.RelatedDataProvider === 'sf-any-site-provider') {
                    viewModel.field.FrontendWidgetTypeName = "inline";
                }
            })

            if ($(element).data('sf-frontend')) {
                defaultType = viewModel.getDefaultFrontendWidgetType(viewModel.field);
                if (viewModel.field.RelatedDataProvider === 'sf-any-site-provider') {
                    isAnySiteSource = true;
                }
            }
            else {
                defaultType = viewModel.getDefaultWidgetType(viewModel.field);
            }

            if (vmValue === defaultType) {
                $(element).val("default");
            } else if (vmValue !== "default" && vmValue !== "inline") {
                if (vmValue === viewModel.imageGalleryWidgetName) {
                    $(element).val(viewModel.imageGalleryWidgetName);
                } else {
                    $(element).val("custom");
                }
            } else {
                $(element).val(vmValue);
            }

            if (isAnySiteSource) {
                if ($(element).val() === 'default') {
                    $(element).val('inline');
                }
                $(element).find('option[value="default"]').hide();
            } else {
                $(element).find('option[value="default"]').show();
            }
        }
    });

    //RelatedMedia.prototype = $.extend(Object.create(ContentBase.prototype), RelatedMedia.prototype);
    return (RelatedMedia);
});