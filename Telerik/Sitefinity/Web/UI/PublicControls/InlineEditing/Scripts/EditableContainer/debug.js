define(["FieldsFactory", "DetailViewEditingWindow"], function (fieldsFactory, DetailViewEditingWindow) {

    function EditableContainer(options) {

        this.fieldType = '';
        this.fieldName = '';
        this.options;
        this.itemId = options.itemId;
        this.tempItemId = null;
        this.itemType = options.itemType;
        this.displayType = options.itemType;
        this.hasChanges = false;
        this.isPublished = true;
        this.onEditCallback = options.onEdit;
        this.onCancelEditCallback = options.onCancelEdit;
        this.onStopEditCallback = options.onStopEdit;
        this.element = options.element;
        this.controlId = null;
        this.providerName = options.providerName;
        this.editableFields = [];
        this.viewModel = null;
        this.fieldsInitialized = false;
        this.isPageLocked = eval(options.isPageLocked);
        this.isEditable = false;
        this.isLocked = false;
        this.status = {};
        this.needsCleanUp = false;
        this.contentRepository = options.contentRepository;
        this.siteBaseUrl = options.siteBaseUrl;
        this.detailsViewUrl = '';
        this.setControlInfo();
        this.validator = {};
        this.fields = [];
        this.htmlTemplate = '';
        this.wasWarningMessageClosed = false;
        this.detailViewEditingWindow = new DetailViewEditingWindow({
            closeEditingWindowHandler: Function.createDelegate(this, this.closeInlineEditingWindowHandler)
        });

        if (!this.itemId) {
            //if item id doesn't exists we are editing page control
            this.isLocked = this.isPageLocked;
            this.itemId = this.controlId;
        }
        //Set fields
        this.getFieldElements(this.element, this.fields);
        this.showWarningMessage();
    }

    EditableContainer.prototype = {

        initialize: function (editableContainerHtmlTemplate, data) {
            var that = this;

            this.displayType = data.DisplayType;
            this.isEditable = data.ItemStatus.IsEditable;
            this.isLocked = data.ItemStatus.IsLocked && !data.ItemStatus.IsLockedByMe;
            this.status = data.ItemStatus;
            this.isPageControl = data.IsPageControl;
            this.detailsViewUrl = data.DetailsViewUrl;
            this.htmlTemplate = editableContainerHtmlTemplate;

            this.element.find('a[data-sf-ftype=ShortText]').each(function (i, el) {
                $(el).removeAttr('href');                
            });

            this.validator = this.element.kendoValidator({
                rules: {
                    minlength: function (input) {
                        var minLength, valueLength, tempInputItemText = input.val() || input.text();
                        if ((input.filter('[data-sf-ftype=ShortText]').filter("[minlength]").length && tempInputItemText !== '' && input.attr("minlength").length > 0) ||
                            (input.filter('[data-sf-ftype=LongText]').filter("[minlength]").length && tempInputItemText !== '' && input.attr("minlength").length > 0)) {
                            minLength = parseInt(input.attr("minlength"));
                            if (minLength > 0) {
                                valueLength = tempInputItemText.length;
                                return valueLength >= minLength;
                            }
                        }

                        return true;
                    },
                    maxlength: function (input) {
                        var maxLength, valueLength, tempInputItemText = input.val() || input.text();
                        if ((input.filter('[data-sf-ftype=ShortText]').filter("[maxlength]").length && tempInputItemText !== '' && input.attr("maxlength").length > 0) ||
                            (input.filter('[data-sf-ftype=LongText]').filter("[maxlength]").length && tempInputItemText !== '' && input.attr("maxlength").length > 0)) {
                            maxLength = parseInt(input.attr("maxlength"));
                            if (maxLength > 0) {
                                valueLength = tempInputItemText.length;
                                return valueLength <= maxLength;
                            }
                        }

                        return true;
                    }
                }
            }).data('kendoValidator');

            if (!this.isEditable) {
                // the user doesn't have permissions to edit this item
                return;
            }

            this.viewModel = kendo.observable({
                status: data.ItemStatus.DisplayStatus,
                statusClass: 'sfStatusOfContent',
                isStatusVisible: false
            });
            that.element.append(editableContainerHtmlTemplate);
            kendo.bind(that.element.get(0), that.viewModel);

            if (this.status.IsStatusEditable && !this.isLocked) {
                $.each(this.fields, function (index, field) {
                    $(field).addClass('sfFieldEditable');
                });
            }

            if (this.isLocked) {
                this.viewModel.set('statusClass', 'sfLockedContent');
                this.viewModel.set('isStatusVisible', 'true');
            }

            this.enableEditing();
        },
        initializeEditableFields: function (enableEditing, sourceEvent) {
            var that = this;
            $.each(this.fields, function (index, item) {
                fieldType = $(item).data('sf-ftype');
                fieldName = $(item).data('sf-field');
                options = {
                    element: $(item),
                    fieldName: fieldName,
                    itemId: that.tempItemId,
                    itemType: that.itemType,
                    providerName: that.providerName,
                    contentRepository: that.contentRepository,
                    siteBaseUrl: that.siteBaseUrl
                }

                fieldsFactory.createField(fieldType, options, function (field) {
                    if (field) {
                        that.editableFields.push(field);
                        if (enableEditing) {
                            field.enableEditing();
                            if (field.element[0] == sourceEvent.target) {
                                field.focusAndShowEditMode();
                            } else {
                                var allChildren = field.element.find('*');
                                jQuery.each(allChildren, function (index, item) {
                                    if (item == sourceEvent.target) {
                                        field.focusAndShowEditMode();
                                    }
                                });
                            }
                        }
                        $(field).on('saveTempField', function (event, sender) {
                            that.contentRepository.saveTemp(that, sender, that.saveTempSuccess, that.saveTempError);
                        });
                    }
                });
            });
        },
        getFieldElements: function (element, fields) {
            if ($(element).attr('data-sf-ftype')) {
                fields.push(element);
            }
            var children = $(element).children();
            for (var i = 0; i < children.length; i++) {
                if ($(children[i]).attr('data-sf-provider')) {
                    continue;
                }
                this.getFieldElements(children[i], fields);
            }
        },
        setControlInfo: function () {
            var id, control = this.element;
            while (control && control.length && !id) {
                id = control.prevAll('script[data-sf-ctrlid]').data('sf-ctrlid');
                control = control.parent();
            }
            this.controlId = id;
        },
        enableEditing: function () {
            var that = this;
            this.element.unbind('click');
            this.element.bind('click', function (e) {
                if (!that.wasWarningMessageClosed) {
                    $(this).prev(".sfWarningMessage").show();
                }

                if (that.fields.length === 0 || $(e.target).data("sf-ftype") || $(e.target).parents('[data-sf-ftype]').length) {
                    if (!that.fieldsInitialized && !that.isLocked) {
                        if (!that.status.IsStatusEditable) {
                            that.fields = [];
                            that.viewModel.set('isStatusVisible', true);
                            that.fieldsInitialized = true;
                        }
                        else if (that.tempItemId) {
                            that.initializeEditableFields(true, e);
                            that.fieldsInitialized = true;
                        }
                        else {
                            that.contentRepository.getTemp(that, function () {
                                that.initializeEditableFields(true, e);
                                that.fieldsInitialized = true;
                                that.needsCleanUp = true;
                                if (that.onEditCallback && typeof (that.onEditCallback) === "function") {
                                    that.onEditCallback(that);
                                }
                            });
                            return;
                        }
                    }
                    if (that.onEditCallback && typeof (that.onEditCallback) === "function") {
                        that.onEditCallback(that);
                    }
                }
                else {
                    that.element.trigger("clickoutside", e.target);
                }
            });

            this.element.unbind('clickoutside');
            this.element.bind('clickoutside', function (e, target) {
                var clickedItem = $(e.target);
                if (target) {
                    clickedItem = $(target);
                }

                var warningMsg = that.element.prev('.sfWarningMessage')[0];
                if (warningMsg && $(warningMsg).is(":visible")) {
                    that.element.prev('.sfWarningMessage').hide();
                }

                if (clickedItem.hasClass('k-overlay') ||
                    clickedItem.hasClass('sfPreventClickOutside') || clickedItem.parents('.sfPreventClickOutside').length ||
                    clickedItem.hasClass('k-animation-container') || clickedItem.parents('.k-animation-container').length) {
                    return;
                }
                if (that.onStopEditCallback && typeof (that.onStopEditCallback) === "function") {
                    that.onStopEditCallback(that);
                }
            });

            this.viewModel.set('isInEditMode', true);
            this.viewModel.set('isEditButtonVisible', false);
        },
        disableEditing: function () {
            $.each(this.editableFields, function (index, field) {
                field.disableEditing();
                delete field;
            });
            this.editableFields = [];
            this.fieldsInitialized = false;
            this.tempItemId = null;
            this.hasChanges = false;
        },
        setContainerStatusVisibility: function (visible) {
            if ((!this.status.IsPublished) && this.viewModel.get('status')) {
                this.viewModel.set('isStatusVisible', visible);
            }
        },
        getContainerStatus: function () {
            return this.viewModel.get('status');
        },
        getData: function () {
            var data = [],
                editedFields = $.grep(this.editableFields, function (item, index) {
                    return item.isChanged();
                });

            if (editedFields.length > 0) {
                $.each(editedFields, function (index, item) {
                    data.push({ Name: item.fieldName, Value: item.value });
                });
            }

            return data;
        },
        cancelEditing: function (container, success, error) {
            $.each(container.editableFields, function (index, item) {
                item.cancelChanges();
            });
            this.contentRepository.deleteTemp(container, success, error);
        },
        saveTempSuccess: function (container, field, success, error) {
            var that = this;
            if (field !== null) {
                if (field.isChanged()) {
                    field.applyChanges();
                    if (field.needsReRender()) {
                        container.contentRepository.render(container, field, success, error);
                    }
                }
            } else {
                $.each(container.editableFields, function (index, item) {
                    if (item.isChanged()) {
                        item.applyChanges();
                        if (item.needsReRender()) {
                            that.contentRepository.render(container, item, success, error);
                        }
                    }
                });
            }
            container.hasChanges = true;
            container.element.loading(false);
        },
        saveTempError: function (jqXHR, textStatus, errorThrown) {
            var that = this;
            that.element.loading(false);
        },
        validate: function () {
            var that = this,
                isValid = true,
                tempValue,
                needsValidation = false;

            $.each(that.editableFields, function (index, field) {
                needsValidation = false;

                if (field.isChanged()) {
                    needsValidation = true;
                    tempValue = field.value;
                    if (fieldType && fieldType === "LongText") {
                        //This <br _moz_dirty="true"> is inserted and we need the clean value of the element in oreder to compare it.
                        //If the browser is mozilla the inserted element is only <br> 
                        //In Chrome <br class="k-br"> is inserted
                        tempValue = tempValue.replace('<br _moz_dirty="true">', '');
                        tempValue = tempValue.replace('<br>', '');
                        tempValue = tempValue.replace('<br class="k-br">', '');
                    }
                }
                else if (field.saveTempParams && field.saveTempParams.data) {
                    needsValidation = true;
                    if (field.saveTempParams.data.hasOwnProperty("ComplexValue")) {
                        tempValue = JSON.stringify(field.saveTempParams.data.ComplexValue);
                    } else {
                        tempValue = JSON.stringify(field.saveTempParams.data);
                    }
                }
                else if (field.dateChangedParams !== null && field.dateChangedParams !== undefined) {
                    needsValidation = true;
                    tempValue = field.dateChangedParams;
                }
                if (needsValidation) {
                    var attributes = field.element.prop("attributes"),
                        $input = $("<input data-sf-temp-input type='text' style='display:none;'/>");

                    //In IE7 and IE8 the attributes property is a collection from all possible attributes.
                    //We filter this on the properties with set status "specified	true	Boolean"
                    if ($.browser && $.browser.msie && $.browser.version <= 8.0) {
                        attributes = $.grep(attributes, function (n, i) {
                            return n.specified;
                        });
                    }
                    $.each(attributes, function () {
                        $input.attr(this.name, this.value);
                    });
                    $input.val(tempValue);
                    field.element.after($input);
                    $input = null;
                    attributes = null;
                }
            });



            if (!that.validator.validate())
                isValid = false;
            that.element.find('[data-sf-temp-input]').remove();

            return isValid;
        },
        editAll: function () {
            var data =
            {
                ShowMoreActionsWorkflowMenu: false,
                HideLanguageList: true,
                DetailsViewUrl: this.detailsViewUrl,
                ItemId: this.itemId,
                ItemType: this.itemType,
                ProviderName: this.providerName,
                Culture: this.contentRepository.culture
            };
            this.detailViewEditingWindow.open(data);
        },
        closeInlineEditingWindowHandler: function (sender, args) {
            var that = this;
            var clearFrame = true;
            if (args.workflowOperationWasExecuted) {
                clearFrame = false;
                if (!args.deleteTemp) {
                    that.tempItemId = undefined;
                }
                this.contentRepository.render(this, null, function () {
                    that.contentRepository.getEditableContainersAdditionalInfo(
                        [that],
                        that.contentRepository.pageId,
                        function (data, textStatus, jqXHR) {
                            that.fieldsInitialized = false;
                            that.needsCleanUp = false;
                            that.disableEditing();
                            that.fields = [];
                            that.getFieldElements(that.element, that.fields);
                            that.initialize(that.htmlTemplate, data.ContainersInfo[0]);
                            that.viewModel.set('isStatusVisible', that.viewModel.isStatusVisible);
                            that.detailViewEditingWindow.clearFrame();
                            if (that.onEditCallback && typeof (that.onEditCallback) === "function") {
                                that.onEditCallback();
                            }
                        }
                    );
                });
            }
            this.detailViewEditingWindow.close(clearFrame);
        },
        needsPublishing: function () {
            return !this.status.IsPublished;
        },
        showWarningMessage: function () {
            var warningMsg = this.element.attr("data-sf-warning-message");
            if (warningMsg && warningMsg.length > 0) {
                var warningMsgTemplate = '<div class="sfPreventClickOutside sfWarningMessage" style="display:none;">  \
                                      <a class="sfCloseMsg">close</a>\
                                      <div>' + warningMsg + ' </div> \
                                  </div>  ';
                var that = this;
                $(warningMsgTemplate).insertBefore(this.element).find(".sfCloseMsg").click(function () {
                    that.wasWarningMessageClosed = true;
                    $(this).parent().hide();
                    that.element.focus();
                });
            }
        }
    };

    return (EditableContainer);
});