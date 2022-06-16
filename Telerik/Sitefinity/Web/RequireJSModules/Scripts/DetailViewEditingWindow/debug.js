define([], function () {
    function DetailViewEditingWindow(options) {
        this.kendoWindow = null;
        this.closeEditingWindowHandler = options.closeEditingWindowHandler;
        this.windowOptions =
            {
                actions: [],
                title: false,
                animation: false,
                modal: false
            }
        this.culture = options.culture || null;
        this.serviceUrl = options.serviceUrl || null;

        return (this);
    }

    DetailViewEditingWindow.prototype = {

        open: function (data) {
            if (data.ItemType == "Telerik.Sitefinity.Pages.Model.PageNode" || data.ItemType == "Telerik.Sitefinity.Pages.Model.PageDraftControl") {
                var win = window.open(data.DetailsViewUrl, '_blank');
                win.focus();
            } else {
                var template = "<iframe id=\"sfEditingWindowFrame\" style=\"width:100%; height:100%;\" frameBorder=\"0\" border=\"0\" src=\"about:blank\"></iframe>";
                this.kendoWindow = jQuery('<div>')
                                   .append(template)
                                   .kendoWindow(this.windowOptions)
                                   .data("kendoWindow");
                this.contentFrame = $(this.kendoWindow.wrapper).find('#sfEditingWindowFrame');
                $(this.kendoWindow.wrapper).addClass("sfMaximizedWindowWithIframe sfLoadingTransition");

                this.contentFrame.prop("src", function () {
                    var src = data.DetailsViewUrl;
                    return src;
                });

                this.kendoWindow.maximize();
                this.kendoWindow.open();

                var that = this;
                this.contentFrame.one('load', function () {
                    $(that.kendoWindow.wrapper).removeClass("sfLoadingTransition");
                    var frameHandle = that.contentFrame.get(0).contentWindow;
                    if (frameHandle) {
                        var showMoreActionsWorkflowMenu = data.ShowMoreActionsWorkflowMenu !== undefined ? data.ShowMoreActionsWorkflowMenu : true;
                        var hideLanguageList = data.HideLanguageList !== undefined ? data.HideLanguageList : false;
                        var commandName, params, key, commandArgument, dataItem;
                        var isEditMode = data.CreateItem ? false : true;
                        commandName = that.getCommandName(data, isEditMode);
                        params = that.getParams(data, isEditMode);
                        key = that.getKey(data, isEditMode);
                        commandArgument = that.getCommandArgument(data, isEditMode);
                        dataItem = that.getDataItem(data, isEditMode);

                        if (frameHandle.createEditingWindow) {
                            frameHandle.createEditingWindow(commandName, dataItem, params, key, commandArgument, that.closeEditingWindowHandler, showMoreActionsWorkflowMenu, hideLanguageList);
                        }
                    }
                });
            }
        },

        deleteTemp: function (data, onSuccess, onError) {
            this.sitefinityAjax({
                type: "DELETE",
                url: this.serviceUrl + '/temp-items/?itemId=' + data.ItemId + '&itemProvider=' + (data.Provider || '') + '&itemType=' + data.ItemType,
                success: function (data, textStatus, jqXHR) {
                    if (typeof onSuccess === 'function') {
                        onSuccess();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof onError === 'function') {
                        onError();
                    }
                }
            });
        },

        sitefinityAjax: function (settings) {
            var that = this;
            commonSettings = {
                contentType: "application/json; charset=utf-8",
                cache: false,
                beforeSend: function (xhr) {
                    if (that.culture) {
                        xhr.setRequestHeader("SF_UI_CULTURE", that.culture);
                    }
                    xhr.setRequestHeader("IsBackendRequest", true);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (typeof settings.error === 'function') {
                        settings.error();
                    }
                    if ($(document.body).loading) {
                        $(document.body).loading(false);
                    }
                    var message;
                    try {
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

        getCommandName: function (data, isEditMode) {
            return data.CommandName ? data.CommandName :
                (isEditMode ? "edit" : "create");
        },

        setCulture: function (culture) {
            this.culture = culture;
        },

        getParams: function (data, isEditMode) {
            return data.Params ? data.Params :
                (isEditMode ? { IsEditable: true } : { providerName: data.ProviderName });
        },

        getKey: function (data, isEditMode) {
            return data.Key ? data.Key :
                (isEditMode ? { Id: data.ItemId || data.Id } : null);
        },

        getCommandArgument: function (data, isEditMode) {
            return data.CommandArgument ? data.CommandArgument :
                { languageMode: (isEditMode ? "edit" : "create"), language: data.Culture || this.culture };
        },

        getDataItem: function (data, isEditMode) {
            if (isEditMode) {
                return {
                    Id: data.ItemId || data.Id,
                    ProviderName: data.ProviderName
                };
            }
            return null;
        },

        setCloseEditingWindowHandler: function (handler) {
            this.closeEditingWindowHandler = handler;
        },

        close: function (clearFrame) {
            if (this.contentFrame) {
                this.contentFrame.prop("src", "about:blank");
            }
            if (clearFrame && this.kendoWindow && this.kendoWindow.wrapper) {
                $(this.kendoWindow.wrapper).remove();
            }
            if (this.kendoWindow) {
                this.kendoWindow.close();
            }

        },

        // Frame's content should be cleared explicitly if there are any requests made when closing it.
        clearFrame: function () {
            if (this.kendoWindow && this.kendoWindow.wrapper) {
                $(this.kendoWindow.wrapper).remove();
            }
        },

        maximize: function () {
            this.kendoWindow.maximize();
        },

        center: function () {
            this.kendoWindow.center();
        },

        getKendoWindow: function () {
            return this.kendoWindow;
        }
    };

    return (DetailViewEditingWindow);
});