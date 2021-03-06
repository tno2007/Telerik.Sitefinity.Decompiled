define(["ContentRepository", "DetailViewEditingWindow", "text!WorkflowPanelTemplate!strip"], function (ContentRepository, DetailViewEditingWindow, html) {
    function WorkflowPanel(options) {
        var that = this;

        this.editableContainers = [];
        this.cancelResultList = [];
        this.currentlyEditedContainer = null;
        this.areContainerStatusesVisible = false;
        this.contentRepository = options.contentRepository;
        this.detailViewEditingWindow = new DetailViewEditingWindow({
            closeEditingWindowHandler: Function.createDelegate(that, this.closeInlineEditingWindowHandler)
        });

        this.validator = options.validator;
        this.viewModel = kendo.observable({
            isMenuVisible: false,
            exitEditingAlertMessage: '',
            publish: function (e) {
                that.publish();
            },
            saveDraft: function (e) {
                that.saveDraft();
            },
            cancel: function (e) {
                that.exitEditing();
            },
            showActions: function (e) {
                that.showActions();
            },
            hideActions: function (e) {
                that.hideActions();
            },
            editInBackend: function (e) {
                alert('TODO: Edit in backend');
            },
            deleteItem: function (e) {
                alert('TODO: Delete item');
            },
            publishItem: function (e) {
                that.publishItem();
            },
            editAll: function () {
                if (that.currentlyEditedContainer) {
                    var argument =
                        {
                            data:
                                {
                                    DetailsViewUrl: that.currentlyEditedContainer.detailsViewUrl,
                                    ItemId: that.currentlyEditedContainer.itemId,
                                    ItemType: that.currentlyEditedContainer.itemType,
                                    ProviderName: that.currentlyEditedContainer.providerName
                                }
                        };
                    if (that.currentlyEditedContainer.detailViewEditingWindow) {
                        var data = argument.data;
                        data.ShowMoreActionsWorkflowMenu = false;
                        data.HideLanguageList = true;
                        data.Culture = that.contentRepository.culture;
                        that.currentlyEditedContainer.detailViewEditingWindow.open(data);
                    }
                }
            },
            openItemInBackend: function (e) {
                that.detailViewEditingWindow.setCloseEditingWindowHandler(Function.createDelegate(that, that.closeInlineEditingWindowHandler));
                that.openItemInBackend(e);
            },
            closeResultsWindow: function (e) {
                that.closeResultsWindow();
            },
            markNotPublishedContentClick: function (e) {
                that.markNotPublishedContentClick();
            },
            pageTitle: "",
            isContainerTitleVisible: false,
            containerTitle: "",
            itemStatus: "",
            isItemStatusVisible: false,
            isActionsPanelVisible: false,
            showPublish: false,
            showEditAll: false,
            resultList: [],
            resultsCount: "",
            isSingleResultLabelVisible: false,
            isMultipleResultLabelVisible: false,
            isStatusContainerVisible: false,
            editingText: "",
            markNotPublishedText: "",
            unmarkNotPublishedText: "",
            markText: "",
            contentNotEditableText: "",
            statusDescription: "",
            isItemTitleVisible: true,
            actionsVisible: true,
            workflowClass: function (e) {
                return "sfStatusIcn sf" + e.ItemStatus.WorkflowStatus.toLocaleLowerCase();
            }
        });

        this.element = $(html);
        $(document.body).append(this.element);
        kendo.bind(this.element, this.viewModel);
        this.viewModel.set('markText', this.viewModel.markNotPublishedText);
        return this;
    }

    String.prototype.htmlEncode = function () {
        var encoded = '',
            div = document.createElement('div');

        div.innerText = this;
        encoded = div.innerHTML;
        delete div;

        return encoded;
    };

    WorkflowPanel.prototype = {
        show: function () {
            var isStatusContainerVisible = false;
            $.each(this.editableContainers, function (index, container) {
                if (!container.status.IsPublished) {
                    isStatusContainerVisible = true;
                    return;
                }
            });

            this.viewModel.set('isStatusContainerVisible', isStatusContainerVisible);
            this.viewModel.set('isMenuVisible', true);
            $(document.body).addClass("sfWorkflowBtnsShown");
            $("#workflowResultsWindow").kendoWindow({
                actions: [],
                title: false,
                animation: false,
                modal: true,
                width: "600px",
                viewable: false
            });
        },

        hide: function () {
            this.viewModel.set('isMenuVisible', false);
            $("body").removeClass("sfWorkflowBtns");
        },

        exitEditing: function (tempItemsData) {
            var that = this, data = [], hasChanges = false;
            $(this).trigger('commandExecuting', { 'operation': 'Cancel' });

            if (tempItemsData) {
                data = tempItemsData;
            } else {
                $.each(this.editableContainers, function (index, container) {
                    if (container.tempItemId) {
                        data.push({
                            'ItemId': container.tempItemId,
                            'ItemType': container.itemType,
                            'Provider': container.providerName
                        });
                        $.each(container.editableFields, function (index, field) {
                            hasChanges |= field.isChanged();
                        });
                        hasChanges |= container.hasChanges;
                    }
                });
            }

            if (hasChanges) {
                var exitConfirmed = window.confirm(this.viewModel.exitEditingAlertMessage);
                if (!exitConfirmed) {
                    return;
                }
            }

            $(document.body).loading(true);

            this.contentRepository.cancelChanges(data,
                function () {
                    $.each(that.editableContainers, function (index, container) {
                        container.needsCleanUp = false;
                    });

                    that.triggerCommandExecuted('Cancel', true);
                }
            );
        },

        publish: function () {
            this.executeWorkflowOperation(this.contentRepository.workflowOperation.Publish, true, false);
        },

        saveDraft: function () {
            this.executeWorkflowOperation(this.contentRepository.workflowOperation.SaveAsDraft, false, false);
        },

        showActions: function () {
            $('.sfInlineEditingWorkflowMenu .actionsWrapper').toggle();
            $('.sfContentMenuListWrp').toggleClass('sfContentMenuOpened');
        },

        hideActions: function () {
            $('.sfInlineEditingWorkflowMenu .actionsWrapper').hide();
            $('.sfContentMenuListWrp').removeClass('sfContentMenuOpened');
        },

        publishItem: function () {
            $('.sfInlineEditingWorkflowMenu .actionsWrapper').toggle();
            $('.sfContentMenuListWrp').toggleClass('sfContentMenuOpened');
            var that = this;
            $(this).trigger('commandExecuting', { 'operation': this.contentRepository.workflowOperation.Publish });

            if (!this.currentlyEditedContainer) {
                alert("Currently edited container is not set!");
            }

            var data = this.getContainerData(this.currentlyEditedContainer, false, this.contentRepository.workflowOperation.Publish);
            if (this.currentlyEditedContainer.validate()) {
                if (data) {
                    this.currentlyEditedContainer.element.loading(true);
                    this.contentRepository.executeWorkflowOperation([data], this.contentRepository.workflowOperation.Publish,
                       function (data, textStatus, jqXHR) {
                           //TODO/6: render field and get new workflow status
                           that.currentlyEditedContainer.disableEditing();
                           that.currentlyEditedContainer.needsCleanUp = false;
                           that.clearPanelTitle();
                           that.currentlyEditedContainer.element.loading(false);
                           that.checkResultData(data, that.contentRepository.workflowOperation.Publish, false);
                       });
                }
                else {
                    this.triggerCommandExecuted(this.contentRepository.workflowOperation.Publish, false);
                }
            }
            else {
                $('[role="alert"]').css('display', 'block');
            }
        },

        executeWorkflowOperation: function (workflowOperation, reload) {
            var that = this,
                isValid = true;

            if (this.editableContainers == []) {
                alert("No editable containers set");
            }

            $.each(this.editableContainers, function (index, container) {
                if (!container.validate())
                    isValid = false;
            });

            var data = [];
            $.each(this.editableContainers, function (index, container) {
                // isEditable checks for permissions
                if (!container.isEditable) {
                    return;
                }

                var containerData = that.getContainerData(container, reload, workflowOperation);
                if (containerData != null) {
                    data.push(containerData);
                }
            });
            if (isValid) {
                if (data.length) {
                    $(this).trigger('commandExecuting', { 'operation': workflowOperation });
                    $(document.body).loading(true);
                    this.contentRepository.executeWorkflowOperation(data, workflowOperation,
                        function (data, textStatus, jqXHR) {
                            $.each(that.editableContainers, function (index, container) {
                                if (container.hasChanges && !reload) {
                                    $.each(container.editableFields, function (index, field) {
                                        if (field.isChanged()) {
                                            field.applyChanges();
                                        }
                                    });
                                    container.isPublished = false;
                                }
                                container.hasChanges = false;
                                container.needsCleanUp = !reload;
                            });
                            $(document.body).loading(false);

                            that.checkResultData(data, workflowOperation, reload);
                        }
                    );
                }
                else {
                    this.exitEditing();
                }
            }
            $('body [data-sf-temp-input]').remove();
        },

        triggerCommandExecuted: function (operation, reload) {
            $(this).trigger('commandExecuted', { 'operation': operation, 'reload': reload });
        },

        openItemInBackend: function (e) {
            var data = e.data;
            data.ShowMoreActionsWorkflowMenu = false;
            data.HideLanguageList = true;
            data.Culture = this.contentRepository.culture;
            this.detailViewEditingWindow.open(data);
        },

        closeInlineEditingWindowHandler: function (sender, args) {
            if (args.deleteTemp) {
                // get data for temp item created when details view was opened
                var tempItemData = {
                    'ItemId': sender.get_dataItem().Item.Id,
                    'ItemType': sender.get_dataItem().ItemType,
                    'Provider': sender._providerName
                }
                if (!this.containsItemWithId(this.cancelResultList, tempItemData.ItemId)) {
                    this.cancelResultList.push(tempItemData);
                }
            }

            var clearFrame = true;
            if (args.workflowOperationWasExecuted) {
                // remove item from resultList if workflow operation was executed
                var masterId = sender.get_dataKey().Id;
                var resultList = this.viewModel.get('resultList');
                var newList = this.removeItemByAttr(resultList, "ItemId", masterId);
                this.setResultsWindowData(newList);
                clearFrame = false;
            }

            this.detailViewEditingWindow.close(clearFrame);
        },

        containsItemWithId: function (arr, itemId) {
            var items = $.grep(arr,
                function (obj) {
                    return obj.ItemId == itemId;
                });
            return items.length > 0;
        },

        removeItemByAttr: function (arr, attr, value) {
            var i = arr.length;
            while (i--) {
                if (arr[i] && arr[i].hasOwnProperty(attr) && (arguments.length > 2 && arr[i][attr] === value)) {
                    arr.splice(i, 1);
                    break;
                }
            }
            return arr;
        },

        checkResultData: function (data, workflowOperation, reload) {
            if (data.length > 0) {
                this.openResultsWindow(data);
            } else {
                this.triggerCommandExecuted(workflowOperation, reload);
            }
        },

        openResultsWindow: function (data) {
            this.setResultsWindowData(data);
            $("#workflowResultsWindow").data("kendoWindow").center();
            $("#workflowResultsWindow").data("kendoWindow").open();
            $("#workflowResultsWindow").parent().addClass("sfInlineEditDlgWrp");
        },

        setResultsWindowData: function (data) {
            if (data.length == 0) {
                this.closeResultsWindow();
                return;
            }
            this.viewModel.set('resultList', data);
            this.viewModel.set('resultsCount', data.length);
            this.viewModel.set('isSingleResultLabelVisible', data.length == 1);
            this.viewModel.set('isMultipleResultLabelVisible', data.length > 1);
        },

        closeResultsWindow: function () {
            $("#workflowResultsWindow").data("kendoWindow").close();
            this.exitEditing(this.cancelResultList);
            this.viewModel.set('resultList', []);
            this.cancelResultList = [];
        },

        getContainerData: function (container, reload, workflowOperation) {
            var data = null,
                fields = container.getData(),
                hasChanges = container.hasChanges,
                hasEditedFields = (fields && fields.length),
                hasToBeReloaded = (!container.isPublished && reload),
                hasDraftNeedsPublishing = false;

            //If we have Draft item and publish all items is clicked
            if (workflowOperation && workflowOperation === this.contentRepository.workflowOperation.Publish) {
                try {
                    hasDraftNeedsPublishing = container.status.WorkflowStatus.toLowerCase().indexOf("Draft".toLowerCase()) != -1;
                }
                catch (e) {
                    hasDraftNeedsPublishing = false;
                }
            }

            if (hasChanges ||
                hasEditedFields ||
                hasToBeReloaded ||
                hasDraftNeedsPublishing) {

                //If we did not enter edit mode we will need to check out the item before publish it
                if (!container.tempItemId) {
                    this.contentRepository.getTemp(container,
                        function () { container.initializeEditableFields(false); },
                        function () { },
                        false);
                }

                data = {
                    'ItemId': container.tempItemId,
                    'ItemType': container.itemType,
                    'Provider': container.providerName,
                    'FieldValues': fields
                };

                container.hasChanges = true;
            }
            return data;
        },

        get_editableContainers: function () {
            return this.editableContainers;
        },

        add_editableContainer: function (editableContainer) {
            if (this.editableContainers.indexOf(editableContainer) < 0) {
                this.editableContainers.push(editableContainer);
            }
        },

        set_additionalInfo: function (data) {
            this.viewModel.set('pageTitle', data.PageTitle);
        },

        set_currentlyEditedContainer: function (container) {
            this.currentlyEditedContainer = container;

            if (container != null) {
                var status = container.getContainerStatus();
                this.viewModel.set('itemStatus', status);
                this.viewModel.set('isItemStatusVisible', status && status.length > 0);
                this.viewModel.set('isContainerTitleVisible', true);
                this.viewModel.set('isStatusContainerVisible', false);
                this.viewModel.set('showPublish', container.fields && container.fields.length > 0 && !container.isPageControl);
                this.viewModel.set('showEditAll', container.detailsViewUrl && container.detailsViewUrl.length > 0);

                if (typeof container.displayType === 'string') {
                    this.viewModel.set('containerTitle', container.displayType.htmlEncode());
                } else {
                    this.viewModel.set('containerTitle', container.displayType);
                }

                if (container.status.IsStatusEditable && !container.isLocked) {
                    this.viewModel.set('statusDescription', this.viewModel.editingText);
                    this.viewModel.set('isItemTitleVisible', true);
                    this.viewModel.set('actionsVisible', true);
                } else {
                    this.viewModel.set('statusDescription', this.viewModel.contentNotEditableText);
                    this.viewModel.set('isItemTitleVisible', false);
                    this.viewModel.set('actionsVisible', !container.status.IsStatusEditable);
                }

            } else {
                this.clearPanelTitle();
            }
        },

        stopEditingContainer: function (container) {
            if (this.currentlyEditedContainer == container) {
                this.set_currentlyEditedContainer(null);
            }
        },

        clearPanelTitle: function () {
            var isStatusContainerVisible = false;
            $.each(this.editableContainers, function (index, container) {
                if (!container.status.IsPublished) {
                    isStatusContainerVisible = true;
                    return;
                }
            });
            this.viewModel.set('isStatusContainerVisible', isStatusContainerVisible);
            this.viewModel.set('isContainerTitleVisible', false);
            this.viewModel.set('containerTitle', '');
            this.viewModel.set('itemStatus', '');
            this.viewModel.set('isItemStatusVisible', false);
        },

        markNotPublishedContentClick: function () {
            var that = this;
            that.areContainerStatusesVisible = !that.areContainerStatusesVisible;
            if (that.areContainerStatusesVisible) {
                this.viewModel.set('markText', this.viewModel.unmarkNotPublishedText);
            } else {
                this.viewModel.set('markText', this.viewModel.markNotPublishedText);
            }
            $.each(this.editableContainers, function (index, container) {
                container.setContainerStatusVisibility(that.areContainerStatusesVisible);
            });
        }
    }

    return (WorkflowPanel);
});