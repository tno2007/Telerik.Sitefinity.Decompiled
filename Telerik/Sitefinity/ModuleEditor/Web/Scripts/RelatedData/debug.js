//define(["ContentBase", "text!RelatedDataTemplate!strip"], function (EditableWindowField, html) {
define(["text!RelatedDataTemplate!strip"], function (html) {
    function RelatedData(options) {
        // Call the super constructor.
        //ContentBase.call(this, options);
        var that = this;
        this.modelType = {
            Dynamic: "0",
            Static: "1"
        };
        this.fieldType = "RelatedData";
        this.defaultWidgetTypeName = "Telerik.Sitefinity.Web.UI.Fields.RelatedDataField";
        this.defaultFrontEndWidgetNamesCollection = {
            "DynamicType": "Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentView",
            "Pages": "Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationControl",
            "News": "Telerik.Sitefinity.Modules.News.Web.UI.NewsView",
            "Blog Posts": "Telerik.Sitefinity.Modules.Blogs.Web.UI.BlogPostView",
            "Events": "Telerik.Sitefinity.Modules.Events.Web.UI.EventsView",
            "ProductType": "Telerik.Sitefinity.Modules.Ecommerce.Catalog.Web.UI.Views.LightProductsView"
        };
        this.selectors = {
            dropdownProviders: "#dataTypeProviders"
        };
        this.inlineFrontendControl = 'inline';
        this.selectedFieldType = {};
        this.template = html;
        this.originalDataModel = options.dataModel;
        this.siteBaseUrl = options.siteBaseUrl;
        this.dataModelType = this.originalDataModel.hasOwnProperty("Definition") ? this.modelType.Static : this.modelType.Dynamic;
        this.modifiedDataModel = {
            field: {}
        };

        $.extend(true, this.modifiedDataModel.field, options.field);

        this.viewModel = kendo.observable({
            field: this.modifiedDataModel.field,
            options: options,
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
            isWidgetTypeNameVisible: false,
            isFrontendWidgetTypeNameVisible: false,
            widgetTypeNameChanged: function (e, s) {
                var selectedValue = $(e.target).find(":selected").val();
                var isCustom = selectedValue !== $(e.target).find(":first").val();
                that.viewModel.set("isWidgetTypeNameVisible", isCustom);

                if (!isCustom) {
                    that.viewModel.set("field.WidgetTypeName", selectedValue);
                    $('#widgetSelectorVirtualPath').removeAttr('required');
                }
                else {
                    $('#widgetSelectorVirtualPath').attr('required', 'required');
                }

                that.resizeToContent();
            },
            frontendWidgetTypeNameChanged: function (e, s) {
                var selectedValue = $(e.target).find(":selected").val();
                var isCustom = selectedValue === $(e.target).find(":last").val();
                that.viewModel.set("isFrontendWidgetTypeNameVisible", isCustom);

                if (!isCustom) {
                    that.viewModel.set("field.FrontendWidgetTypeName", selectedValue);
                    $('#frontendWidgetSelectorVirtualPath').removeAttr('required');
                }
                else {
                    $('#frontendWidgetSelectorVirtualPath').attr('required', 'required');
                }

                that.resizeToContent();
            },
            tabActivate: function (e) {
                that.resizeToContent();
            },
            getDefaultWidgetType: function (field) {
            	return that.defaultWidgetTypeName;
            },
            getDefaultFrontendWidgetType: function () {
                return that.defaultFrontEndWidgetNamesCollection[this.options.moduleTypeName];
            }
        });

        // Return this object reference.
        return (this);
    }

    RelatedData.prototype = {
        initialize: function () {
            this.relatedDataServiceUrl = this.siteBaseUrl + "restapi/sitefinity/related-data/data-types";

            this.dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: this.relatedDataServiceUrl,
                        dataType: "json"
                    }
                },
                schema: {
                    data: function (response) {
                        for (var i = 0; i < response.length; i++) {
                            if (response[i].ClrType === options.field.RelatedDataType) {
                                var providers = response[i].Providers;
                                for (var p = 0; p < providers.length; p++) {
                                    if (providers[p].Name == "sf-site-default-provider" || providers[p].Name == "sf-any-site-provider") {
                                        providers[p].Type = "system";
                                    }
                                }
                                return response[i].Providers;
                            }
                        }
                    }
                },
                group: { field: "Type" }
            });
            this.dataProvidersDropdown = $(this.selectors.dropdownProviders).kendoDropDownList({
                animation: false,
                height: 150,
                dataTextField: "Title",
                dataValueField: "Name",
                fixedGroupTemplate: '',
                groupTemplate: ' ',
                dataSource: this.dataSource
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

        	var isFrontendWidgetTypeNameVisible = this.modifiedDataModel.field.FrontendWidgetTypeName !== this.viewModel.getDefaultFrontendWidgetType() &&
                              this.modifiedDataModel.field.FrontendWidgetTypeName !== 'default' &&
        	                  this.modifiedDataModel.field.FrontendWidgetTypeName !== this.inlineFrontendControl;
        	this.viewModel.set('isFrontendWidgetTypeNameVisible', isFrontendWidgetTypeNameVisible);
        },

        resizeToContent: function () {
            if (window.dialogBase) {
                dialogBase.resizeToContent();
            }
        },

        success: function (data, textStatus, jqXHR) {
        },

        done: function (event, sender) {
        	var that = this;

            this.viewModel.field.RelatedDataProvider = this.dataProvidersDropdown.value();

        	that.setWidgetTypes();

        	that.modifiedDataModel.field = that.viewModel.field.toJSON();

        	this.clearUI();
            //if (that.validate()) {
                $(this).trigger('onFieldDone', that);
            //}
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
            if (that.validators.generalValidator.validate()) {
                valid = true;
            }
            else {
                $($(that.validatorSelectors.genral).parent().find('ul.k-tabstrip-items li:first').get(0)).click();
                $(that.validatorSelectors.genral).find('span[role="alert"]').show();
            }

            return valid;
        },

        initializeValidators: function () {
            var that = this;

            if (that.validators.generalValidator === null) {
                that.validators.generalValidator = $(this.validatorSelectors.genral).kendoValidator().data("kendoValidator");
            }
        },
		
        setWidgetTypes: function () {
        	if (!this.viewModel.isFrontendWidgetTypeNameVisible && this.viewModel.field.FrontendWidgetTypeName === 'default') {
        		this.viewModel.field.FrontendWidgetTypeName = this.viewModel.getDefaultFrontendWidgetType();
        	}

        	if (!this.viewModel.isWidgetTypeNameVisible && this.viewModel.field.WidgetTypeName === 'default') {
        		this.viewModel.field.WidgetTypeName = this.viewModel.getDefaultWidgetType(this.viewModel.field);
        	}
        },

        clearUI: function () {
            kendo.destroy($(this.selectors.dropdownProviders));
        }
    };

    //kendo custom bindings --------------------------------------

    kendo.data.binders.relatedDataWidgetTypeName = kendo.data.Binder.extend({
        refresh: function () {
            var that = this,
                element = this.element,
                vmValue = that.bindings.relatedDataWidgetTypeName.get(),
                isAnySiteSource = false;

            var defaultType = '';
            var viewModel = that.bindings.relatedDataWidgetTypeName.source;

            $("#dataTypeProviders").bind('change', function (e) {
                viewModel.field.RelatedDataProvider = this.value;
                that.refresh();
                if (viewModel.field.RelatedDataProvider === 'sf-any-site-provider') {
                    viewModel.field.FrontendWidgetTypeName = "inline";
                }
            })

            if ($(element).data('sf-frontend')) {
                defaultType = viewModel.getDefaultFrontendWidgetType();
                if (viewModel.field.RelatedDataProvider === 'sf-any-site-provider') {
                    isAnySiteSource = true;
                }
            }
            else {
            	defaultType = viewModel.getDefaultWidgetType(viewModel.field);
            }

            if (vmValue !== defaultType && vmValue !== 'inline') {
                $(element).val("custom");
            } else {
                $(element).val(vmValue);
            }

            if (isAnySiteSource) {
                if ($(element).val() === defaultType) {
                    $(element).val('inline');
                }
                $(element).find('option[value="' + defaultType + '"]').hide();
            } else {
                $(element).find('option[value="' + defaultType + '"]').show();
            }
        }
    });

    //RelatedData.prototype = $.extend(Object.create(ContentBase.prototype), RelatedData.prototype);
    return (RelatedData);
});