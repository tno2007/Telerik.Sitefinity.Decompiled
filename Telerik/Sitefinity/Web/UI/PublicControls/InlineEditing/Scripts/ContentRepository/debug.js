define([], function () {
    function ContentRepository(options) {
        this.serviceUrl = options.serviceUrl;
        this.pageId = options.pageId;
        this.culture = options.currentUICulture;
        this.imageServiceUrl = options.imageServiceUrl;
        this.flatTaxonServiceUrl = options.flatTaxonServiceUrl;
        this.workflowOperation = {
            Publish: "Publish",
            SaveAsDraft: "SaveAsDraft"
        };
    }

    ContentRepository.prototype = {
        sitefinityAjax: function (settings) {
            var that = this;
            commonSettings = {
                contentType: "application/json; charset=utf-8",
                cache: false,
                beforeSend: function (xhr) {
                    if (that.culture) {
                        xhr.setRequestHeader("SF_UI_CULTURE", that.culture);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof settings.error === 'function') {
                        settings.error();
                    }
                    if ($(document.body).loading) {
                        $(document.body).loading(false);
                    }
                    var message;
                    try{
                        message = JSON.parse(jqXHR.responseText).ResponseStatus.Message;
                    } catch (e) {
                        message = $(jqXHR.responseText).text();
                    }
                    var content = "<div class='sfWindowBody'><h1>Error message</h1><p>" + message + "</p><div class='sfButtonArea'><a class='sfLinkBtn sfPrimary'>Close</a></div></div>",
                        $kendoWindow = $("<div></div>"),
                        kendoWindow = $kendoWindow.kendoWindow({
                            width: "500px",
                            resizable: false,
                            modal: true,
                            title: false
                        })
                        .data("kendoWindow")
                        .content(content)
                        .center()
                        .open();
                    kendoWindow.wrapper.addClass("sfInlineEditDlgWrp");
                    kendoWindow.wrapper.addClass("sfPreventClickOutside");
                    kendoWindow.wrapper.find(".k-window-action").css("visibility", "hidden");
                    kendoWindow.wrapper.find(".sfLinkBtn").click(function (e) {
                        kendoWindow.close();
                        kendoWindow.destroy();
                    })
                    content = null;
                    message = null;
                    $kendoWindow = null;
                }
            }
            
            var jqXHRSettings = $.extend({}, settings, commonSettings);
            $.ajax(jqXHRSettings);
        },

        render: function (container, field, onSuccess, onError) {
            var serviceUrl = this.serviceUrl + '/render?format=json',
                fieldName = null;
            if (field) {
                field.element.loading(true);
                fieldName = field.fieldName;
            } else {
                container.element.loading(false);
            }
            var controlId = container.controlId;
            //check to see if we are editing a control.
            if (container.controlId == container.itemId) {
                controlId = container.tempItemId;
            }
            this.sitefinityAjax({
                type: "POST",
                url: serviceUrl,
                dataType: "html",
                data: JSON.stringify({
                    pageId: this.pageId,
                    controlId: controlId,
                    dataItemId: container.tempItemId ? container.tempItemId : container.itemId,
                    fieldName: fieldName,
                    url: encodeURIComponent(window.location.href)
                }),
                success: function (data, textStatus, jqXHR) {
                    if (field) {
                        field.updateHtml(data);
                    } else {
                        container.element.html(data);
                    }

                    if (typeof onSuccess === 'function') {
                        onSuccess();
                    }
                    if (field) {
                        field.element.loading(false);
                    }
                    else {
                        container.element.loading(false);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
                        onError();
                    }
                    if (field) {
                        field.element.loading(false);
                    }
                    else {
                        container.element.loading(false);
                    }
                }
            });
        },

        getTemp: function (container, onSuccess, onError, isAsync) {
            // isEditable checks for permissions
            if (!container.isEditable) {
                return;
            }
            container.element.loading(true);
            if (isAsync === null) {
                //Make async call by default
                isAsync = true;
            }
            this.sitefinityAjax({
                type: "GET",
                url: this.serviceUrl + '/temp/' + container.itemId + '/?provider=' + (container.providerName || '') + '&itemType=' + container.itemType,
                async: isAsync,
                success: function (data, textStatus, jqXHR) {
                    container.tempItemId = data;
                    if (typeof onSuccess === 'function') {
                        onSuccess();
                    }
                    container.element.loading(false);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
                        onError();
                    }
                    container.element.loading(false);
                }
            });
        },

        getLiveImageData: function (parentId, originalContentId, providerName, onSuccess) {
            var params = {
                providerName: providerName,
                filter: "Visible == true AND Status == Live AND OriginalContentId == " + originalContentId + "",
                itemType: "Telerik.Sitefinity.Libraries.Model.Image",
                skip: 0,
                take: 1
            };
            this.sitefinityAjax({
                type: "GET",
                url: this.imageServiceUrl + "parent/" + parentId + "/?" + $.param(params),
                success: function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        if (data && data.TotalCount != 0) {
                            onSuccess(data.Items[0], textStatus, jqXHR);
                        }
                    }
                }
            });
        },

        saveTemp: function (container, field, onSuccess, onError) {
            if (field.isChanged()) {
                field.element.loading(true);
                var data = [{ Name: field.fieldName, Value: field.value }];
                var url = this.serviceUrl;
                var verb = "PUT";

                if (field.saveTempParams.data) {
                    data = field.saveTempParams.data;
                }

                this.sitefinityAjax({
                    type: verb,
                    url: url,
                    data: JSON.stringify({
                        fields: data,
                        itemId: container.tempItemId,
                        provider: (container.providerName || ''),
                        itemType: container.itemType
                    }),
                    success: function (data, textStatus, jqXHR) {
                        if (typeof onSuccess === 'function') {
                            onSuccess(container, field);
                        }
                    },
                    error: onError
                });
            }
        },

        executeWorkflowOperation: function (data, workflowOperation, onSuccess, onError) {
            if (data && data.length) {
                this.sitefinityAjax({
                    type: "POST",
                    url: this.serviceUrl +'/workflow',
                    data: JSON.stringify({
                        items: data,
                        pageId: this.pageId,
                        workflowOperation: workflowOperation
                    }),
                    success: onSuccess,
                    error: onError
                });
            }
        },

        cancelChanges: function (data, onSuccess, onError) {
            if (data && data.length > 0) {
                this.sitefinityAjax({
                    type: "DELETE",
                    url: this.serviceUrl + '/workflow',
                    data: JSON.stringify({
                        items: data,
                        pageId: this.pageId
                    }),
                    success: onSuccess,
                    error: onError
                });
            } else if (typeof onSuccess === 'function') {
                onSuccess();
            }
        },


        deleteTemp: function (container, onSuccess, onError) {
            this.sitefinityAjax({
                type: "DELETE",
                url: this.serviceUrl + '/temp/' + container.tempItemId + '/?provider=' + (container.providerName || '') + '&itemType=' + container.itemType,
                success: function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        onSuccess();
                    }

                    if (container.element != null) {
                        container.element.loading(false);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
                        onError();
                    }

                    if (container.element != null) {
                        container.element.loading(false);
                    }
                }
            });
        },

        loadFieldValues: function (container, success, error) {
            this.sitefinityAjax({
                type: "POST",
                url: this.serviceUrl + '/fieldValue',
                data: JSON.stringify({
                    dataItemId: container.itemId,
                    itemType: container.itemType,
                    fieldName: container.fieldName,
                    fieldType: container.fieldType,
                    provider: (container.providerName || '')
                }),
                success: function (data, textStatus, jqXHR) {
                    if (typeof success === 'function') {
                        success(data, textStatus, jqXHR);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof error === 'function') {
                        error(jqXHR, textStatus, errorThrown);
                    }
                }
            });
        },

        getEditableContainersAdditionalInfo: function (containers, pageId, onsuccess, onerror) {
            var that = this;
            var data = {
                PageId: pageId,
                PageTitle: "",
                ContainersInfo: []
            };
            $.each(containers, function (index, item) {
                data.ContainersInfo.push({
                    ItemId: item.itemId,
                    ItemType: item.itemType,
                    Provider: item.providerName,
                    Status: "",
                    Fields: that.getFieldNames(item.fields)
                });
            });

            this.sitefinityAjax({
                type: "POST",
                url: this.serviceUrl + '/containersInfo',
                data: JSON.stringify(data),
                success: onsuccess,
                error: onerror
            });
        },

        ensureFlatTaxon: function (taxonomyId, data, successCallback, errorCallback) {
            this.sitefinityAjax({
                type: 'PUT',
                url: String.format("{0}/{1}/ensure/", this.flatTaxonServiceUrl, taxonomyId),
                data: Telerik.Sitefinity.JSON.stringify(data),
                success: function (data) {
                    if (typeof successCallback === 'function') {
                        successCallback(data);
                    }
                },
                error: function (jqXHR) {
                    if (typeof errorCallback === 'function') {
                        errorCallback(jqXHR);
                    }
                }
            });
        },

        getFieldNames: function (fields) {
            var temp = [];
            $.each(fields, function (index, item) {
                temp.push({"Name" : $(item).data("sf-field")});
            });

            return temp;
        }
    };

    return (ContentRepository);
});