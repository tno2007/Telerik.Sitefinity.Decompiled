Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView.initializeBase(this, [element]);
    //properties
    this._showAllCommentsCommandName = null;
    this._showOnlyCommentsCommandName = null;
    this._showReviewsCommandName = null;
    this._showMyCommentsCommandName = null;
    this._showWaitingApprovalItemsCommandName = null;
    this._showPublishedItemsCommandName = null;
    this._showHiddenItemsCommandName = null;
    this._showMarkedAsSpamItemsCommandName = null;
    this._filterCommandName = null;
    this._currentUserId = null;
    this._selectedItemFilterCssClass = null;
    this._showInapropriateItemsCommandName = null;
    this._previousFilterWidget = null;
    this._commentsUrl = null;
    this._blackListedWindows = null;
    this._dialogIds = null;
    this._decisionScreens = null;
    this._openedDecisionScreen = null;
    this._settingsLink = null;
    this._selectedLanguage = null;
    this._currentSelectedLanguage = null;
    this._multiLingual = null;
    this.isFilterCommand = false;
    this.waitingForApprovalElement;
    this._selectedItems = null;
    this._singleItemDeleteMsg;
    this._multipleItemsDeleteMsg;

    //components
    this._sidebar = null;
    this._toolbar = null;
    this._commentsGrid = null;
    this.backToComments = null;
    this._windowManager = null;
    this._contextBar = null;
    this._commentsRestClient = null;
    this._deleteConfirmationDialog = null;
    this._showSectionsExceptAndResetFilterCommandName = null;

    //delegates
    this._handleWidgetCommandDelegate = null;
    this._itemCommandDelegate = null;
    this._actionCommandDelegate = null;
    this._showDialogDelegate = null;
    this._loadDialogDelegate = null;
    this._closeDialogDelegate = null;
    this._handleGridDataBoundDelegate = null;
    this._handleDecisionScreenCommandDelegate = null;

    this._waitingForApprovalStatus = "WaitingForApproval";
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView.callBaseMethod(this, "initialize");

        jQuery("body").addClass("sfEmpty");
        jQuery('.sfMain.sfClearfix').show();
        this.setBackButton();
        if (this.get_decisionScreens()) {
            this.set_decisionScreens(Sys.Serialization.JavaScriptSerializer.deserialize(this.get_decisionScreens()));
        }

        this._handleWidgetCommandDelegate = Function.createDelegate(this, this._handleWidgetBarCommand);
        if (this.get_sidebar())
            this.get_sidebar().add_command(this._handleWidgetCommandDelegate);

        if (this.get_toolbar())
            this.get_toolbar().add_command(this._handleWidgetCommandDelegate);

        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        if (this.get_commentsGrid()) {
            this.get_commentsGrid().add_itemCommand(this._itemCommandDelegate);
        }

        this._actionCommandDelegate = Function.createDelegate(this, this._actionCommandHandler);
        if (this.get_contextBar()) {
            this.get_contextBar().add_actionCommand(this._actionCommandDelegate);
        }

        this._showDialogDelegate = Function.createDelegate(this, this._showDialogHandler);
        this._loadDialogDelegate = Function.createDelegate(this, this._loadDialogHandler);
        this._closeDialogDelegate = Function.createDelegate(this, this._closeDialogHandler);

        if (this.get_commentsGrid()) {
            if (this.get_multiLingual()) {
                this.get_commentsGrid().get_queryParams().language = this.get_selectedLanguage();
            }
            this._currentSelectedLanguage = this.get_selectedLanguage();
            this.set_commentsRestClient(new Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient(this.get_commentsGrid().get_webServiceUrl()));
        }        

        this._handleGridDataBoundDelegate = Function.createDelegate(this, this._handleGridDataBound);
        this._handleDecisionScreenCommandDelegate = Function.createDelegate(this, this._handleDecisionScreenCommand);
        this._setToolbarButtonsEnabledState(false);
        Sys.Application.add_load(Function.createDelegate(this, this._loadHandler));
    },

    dispose: function () {
        if (this._handleWidgetCommandDelegate) {
            if (this.get_sidebar()) {
                this._sidebar.remove_command(this._handleWidgetCommandDelegate);
            }

            delete this._handleWidgetCommandDelegate;
        }

        if (this.get_toolbar()) {
            this.get_toolbar().remove_command(this._handleWidgetCommandDelegate);
        }

        if (this._handleGridDataBoundDelegate) {
            delete this._handleGridDataBoundDelegate;
        }

        if (this._handleDecisionScreenCommandDelegate) {
            delete this._handleDecisionScreenCommandDelegate;
        }

        if (this._itemCommandDelegate) {
            if (this.get_commentsGrid()) {
                this.get_commentsGrid().remove_itemCommand(this._itemCommandDelegate);
            }

            delete this._itemCommandDelegate;
        }

        if (this._actionCommandDelegate) {
            if (this.get_contextBar()) {
                this.get_contextBar().remove_actionCommand(this._actionCommandDelegate);
            }

            delete this._actionCommandDelegate;
        }

        var that = this;
        jQuery.each(Telerik.Sitefinity.JSON.parse(this.get_dialogIds()), function (i, dialogId) {
            var dialog = that.get_windowManager().getWindowByName(dialogId);
            if (dialog) {
                if (that._showDialogDelegate) {
                    dialog.remove_show(that._showDialogDelegate);
                }
                if (that._loadDialogDelegate) {
                    dialog.remove_pageLoad(that._loadDialogDelegate);
                }
                if (that._closeDialogDelegate) {
                    dialog.remove_close(that._closeDialogDelegate);
                }
            }
        });
        if (that._showDialogDelegate) {
            delete that._showDialogDelegate;
        }
        if (that._loadDialogDelegate) {
            delete that._loadDialogDelegate;
        }

        this._decisionScreens = null;

        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView.callBaseMethod(this, "dispose");
    },

    /* --------------------  event handlers ----------- */

    //Handles grid databound and apply the logic for show/hide Decision Screen
    _handleGridDataBound: function (sender, args) {
        var gridDataSource = sender.sender.dataSource;

        //go to prevous page if current one is empty
        if (gridDataSource.view().length == 0) {
            var currentPage = gridDataSource.page();
            if (currentPage > 1) {
                gridDataSource.page(currentPage - 1);
                return;
            }
        }

        var queryParams = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var threadKey = queryParams.get("targetKey", null);
        var threadType = queryParams.get("threadType", null);
        if (!this.isFilterCommand && sender.sender._data && !sender.sender._data.length && !threadKey && !threadType) {
            this._openedDecisionScreen = this.findDecisionScreen("NoItemsExist");
            if (this._openedDecisionScreen != null) {
                this._openedDecisionScreen.show();
                jQuery(this.get_element()).show();
                jQuery("body").addClass("sfEmpty");
                this._openedDecisionScreen.add_actionCommand(this._handleDecisionScreenCommandDelegate);
            }
            else {
                jQuery("body").removeClass("sfEmpty");
            }
        }
        else {
            jQuery("body").removeClass("sfEmpty");
        }
    },

    //Handle Decision Screen commands 
    _handleDecisionScreenCommand: function (sender, args) {
        var commandName = args.get_commandName();

        switch (commandName) {
            case "settings":
                args.set_cancel(true);
                window.location = this.get_settingsLink();
                break;
        }
    },

    //Handles the commands from sidebar filtering widgets
    _handleWidgetBarCommand: function (sender, args) {
        var that = this;
        var rebindFunc = function () {
            jQuery(that.get_commentsGrid().get_grid()).find(".sfHeaderSelectComment, .sfSelectComment").attr("checked", false);            
            that._setToolbarButtonsEnabledState(false);
            that.get_commentsGrid().fetchDataSource();
            that._setWaitingFroApprovalCount(that.waitingForApprovalElement);
            //update comments count on delete
            if (commandName === "groupDelete") {
                if (that.get_contextBar()) {
                    that.get_contextBar().updateCommentsCount();
                }
            }
            //update average ratings always not only on delete
            if (that.get_contextBar()) {
                that.get_contextBar().updateAverageRating();
            }
        };

        var handleCommand = true;
        var commandName = args.get_commandName();
        var widget;

        if (args.get_cancel() === false) {
            switch (commandName) {
                case this.get_showAllCommentsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ status: "" });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case this.get_showOnlyCommentsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ Behavior: [null] });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case this.get_showReviewsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ Behavior: "review" });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case this.get_showWaitingApprovalItemsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ status: this._waitingForApprovalStatus });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case this.get_showPublishedItemsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ status: 'Published' });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case this.get_showHiddenItemsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ status: 'Hidden' });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case this.get_showMarkedAsSpamItemsCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ status: 'Spam' });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case "settings":
                    handleCommand = false;
                    args.set_cancel(true);
                    window.location = this.get_settingsLink();
                    break;
                case this.get_filterCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        if (args.get_commandArgument().dateTo) {
                            this.get_commentsGrid().set_queryParams({ newerThan: args.get_commandArgument().dateFrom, olderThan: args.get_commandArgument().dateTo });
                        }
                        else if (args.get_commandArgument().dateFrom) {
                            this.get_commentsGrid().set_queryParams({ newerThan: args.get_commandArgument().dateFrom });
                        }
                        else if (args.get_commandArgument().contentType) {
                            this.get_commentsGrid().set_queryParams({ threadType: args.get_commandArgument().contentType });
                        }

                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
                case "groupDelete":
                    args.set_cancel(true);
                    this.set_selectedItems(this._getSelectedComments());
                    if (this.get_selectedItems() && this.get_selectedItems().length > 0) {                        
                        var singleItemDeleteCommandButtonId = that.get_deleteConfirmationDialog()._commands[0].ButtonClientId;
                        var singleItemDeleteButton = jQuery('#' + singleItemDeleteCommandButtonId);
                        var multipleItemsDeleteCommandButtonId = that.get_deleteConfirmationDialog()._commands[1].ButtonClientId;
                        var multipleItemsDeleteButton = jQuery('#' + multipleItemsDeleteCommandButtonId);

                        var promptCallback = function (sender, promptArgs) {
                            if (promptArgs.get_commandName() === "delete") {
                                that.get_commentsRestClient().deleteComments(that.get_selectedItems(), rebindFunc);
                            }
                        };
                        
                        if (this.get_selectedItems().length > 1) {
                            that.get_deleteConfirmationDialog().show_prompt(null, String.format(that.get_multipleItemsDeleteMsg(), this.get_selectedItems().length), promptCallback);
                            singleItemDeleteButton.hide();
                            multipleItemsDeleteButton.show();
                        }
                        else {
                            multipleItemsDeleteButton.hide();
                            singleItemDeleteButton.show();
                            that.get_deleteConfirmationDialog().show_prompt(null, that.get_singleItemDeleteMsg(), promptCallback);
                        }
                    }

                    break;
                case "groupPublish":
                    args.set_cancel(true);
                    this.set_selectedItems(this._getSelectedComments());
                    if (this.get_selectedItems() && this.get_selectedItems().length > 0) {
                        this.get_commentsRestClient().updateComments(this.get_selectedItems(), "Published", rebindFunc);
                    }

                    break;
                case "groupHide":
                    args.set_cancel(true);
                    this.set_selectedItems(this._getSelectedComments());
                    if (this.get_selectedItems() && this.get_selectedItems().length > 0) {
                        this.get_commentsRestClient().updateComments(this.get_selectedItems(), "Hidden", rebindFunc);
                    }

                    break;
                case "groupMarkSpam":
                    args.set_cancel(true);
                    this.set_selectedItems(this._getSelectedComments());
                    if (this.get_selectedItems() && this.get_selectedItems().length > 0) {
                        this.get_commentsRestClient().updateComments(this.get_selectedItems(), "Spam", rebindFunc);
                    }

                    break;
                case this.get_showSectionsExceptAndResetFilterCommandName():
                    this.isFilterCommand = true;
                    if (this.get_commentsGrid()) {
                        this.get_commentsGrid().set_queryParams({ status: "" });
                        if (this.get_multiLingual()) {
                            this.get_commentsGrid().get_queryParams().language = this._currentSelectedLanguage;
                        }
                    }
                    break;
            }
        }

        if (this.isFilterCommand == true) {
            widget = widget || args.Widget;
            if (args.get_commandArgument && (cmdArg = args.get_commandArgument())) {
                if (cmdArg.get_itemElement) {
                    var linkElm = null;
                    var elm = cmdArg.get_itemElement();
                    linkElm = jQuery(elm).find(".sf_binderCommand_" + commandName).get(0);
                    widget = { LinkElement: linkElm };
                }
            }

            if (widget && this._selectedItemFilterCssClass) {
                //Unselect previous widget
                if (this._previousFilterWidget) {
                    this._markItemSelected(this._previousFilterWidget, false);
                }
                else {
                    if (sender.getAllWidgets) {
                        var widgets = sender.getAllWidgets();
                        for (var i = 0; i < widgets.length; i++) {
                            var w = widgets[i];
                            this._markItemSelected(w, false);
                        }
                    }
                }

                //Select current widget
                this._markItemSelected(widget, true);

                //Update previous widget with current
                this._previousFilterWidget = widget;
            }
        }

        if (handleCommand) {
            //If the command is changeLanguage, we need to change the UiCulture of all lists so that the language selection is persisted
            var argument;
            if (commandName == "changeLanguage") {
                argument = args.get_commandArgument();
                this.isFilterCommand = true;
                if (this.get_commentsGrid()) {
                    this.get_commentsGrid().set_queryParams({ language: argument });
                }
                this._currentSelectedLanguage = argument;
            }

            if (args.get_cancel() == false) {
                if (this.get_commentsGrid()) {
                    //this.get_commentsGrid().initializeDataSource();
                    this.get_commentsGrid().fetchDataSource();
                    this._setWaitingFroApprovalCount(this.waitingForApprovalElement, argument);
                }
            }
        }
    },

    //Handle commands from items in the grid
    _itemCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();

        var that = this;
        var item = {};
        var rebindFunc = function () {
            that.get_commentsGrid().fetchDataSource();
            that._setWaitingFroApprovalCount(that.waitingForApprovalElement);
            //update comments only on delete
            if (commandName === "delete") {
                if (that.get_contextBar()) {
                    that.get_contextBar().updateCommentsCount();
                }
            }
            //update average rating always
            if (that.get_contextBar()) {
                that.get_contextBar().updateAverageRating();
            }
        };
        
        switch (commandName) {
            case "edit":
                this._tryOpenDialog(commandName, args.get_commandArgument(), rebindFunc);
                break;
            case "markAsSpam":                
                item.Key = args.get_commandArgument();
                item.Status = "Spam";
                this.get_commentsRestClient().updateComment(item, rebindFunc);
                break;
            case "publish":                
                item.Key = args.get_commandArgument();
                item.Status = "Published";
                this.get_commentsRestClient().updateComment(item, rebindFunc);
                break;
            case "hide":                
                item.Key = args.get_commandArgument();
                item.Status = "Hidden";
                this.get_commentsRestClient().updateComment(item, rebindFunc);
                break;
            case "delete":
                var singleItemDeleteCommandButtonId = that.get_deleteConfirmationDialog()._commands[0].ButtonClientId;
                var singleItemDeleteButton = jQuery('#' + singleItemDeleteCommandButtonId);
                var multipleItemsDeleteCommandButtonId = that.get_deleteConfirmationDialog()._commands[1].ButtonClientId;
                var multipleItemsDeleteButton = jQuery('#' + multipleItemsDeleteCommandButtonId);
                multipleItemsDeleteButton.hide();
                singleItemDeleteButton.show();

                var promptCallback = function (sender, promptArgs) {                    
                    if (promptArgs.get_commandName() === "delete") {
                        that.get_commentsRestClient().deleteComment(args.get_commandArgument(), rebindFunc);
                    }
                };

                that.get_deleteConfirmationDialog().show_prompt(null, that.get_singleItemDeleteMsg(), promptCallback);
                break;
            case "rowSelect":
                args.set_cancel(true);
                var rowCheckbox = args.get_commandArgument();
                var selectedRowsCount = jQuery(that.get_commentsGrid().get_grid()).find(".sfSelectComment:checked").length;
                if (jQuery(rowCheckbox).is(':checked')) {
                    jQuery(rowCheckbox).parent().parent().attr("aria-selected", "true");
                    that._setToolbarButtonsEnabledState(true);
                    var totalCount = jQuery(that.get_commentsGrid().get_grid()).find(".sfSelectComment").length;
                    var selectedRowsCount = jQuery(that.get_commentsGrid().get_grid()).find(".sfSelectComment:checked").length;
                    jQuery(that.get_commentsGrid().get_grid()).find(".sfHeaderSelectComment").attr("checked", totalCount == selectedRowsCount);
                }
                else {
                    jQuery(rowCheckbox).parent().parent().attr("aria-selected", "false");
                    jQuery(that.get_commentsGrid().get_grid()).find(".sfHeaderSelectComment").attr("checked", false);
                    that._setToolbarButtonsEnabledState(selectedRowsCount > 0);
                }

                break;
            case "headerSelect":
                var rowsCount = jQuery(that.get_commentsGrid().get_grid()).data("kendoGrid")._data.length;
                args.set_cancel(true);
                if (rowsCount > 0) {
                    var headerCheckbox = args.get_commandArgument();
                    var headerIsChecked = jQuery(headerCheckbox).is(':checked');
                    var checkBoxes = jQuery(that.get_commentsGrid().get_grid()).find(".sfSelectComment");
                    that._setToolbarButtonsEnabledState(headerIsChecked);
                    checkBoxes.attr("checked", headerIsChecked).parent().parent().attr("aria-selected", headerIsChecked ? "true" : "false");
                }
                break;
        }
    },

    //Handle action commands in context bar
    _actionCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();

        switch (commandName) {
            case "closeThread":
                break;
            case "requireApproval":
                break;
            case "requireAuthentication":
                break;
        }
    },

    //Sets the initial values and configurations for comments master view controls
    _loadHandler: function () {
        var that = this;
        var query = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        jQuery.each(Telerik.Sitefinity.JSON.parse(this.get_dialogIds()), function (i, dialogId) {
            var dialog = that.get_windowManager().getWindowByName(dialogId);
            if (dialog) {
                dialog.add_show(that._showDialogDelegate);
                dialog.add_pageLoad(that._loadDialogDelegate);
                dialog.add_close(that._closeDialogDelegate);
            }
        });

        //Get the number of the waiting approval comments
        if (this.get_sidebar()._widgets) {
            var threadType = query.get("threadType", null);
            for (var i = 0; i < this.get_sidebar()._widgets.length; i++) {
                if (this.get_sidebar()._widgets[i]._commandName == this.get_showWaitingApprovalItemsCommandName()) {
                    this.waitingForApprovalElement = this.get_sidebar()._widgets[i]
                    this._setWaitingFroApprovalCount(this.waitingForApprovalElement);
                }

                if (threadType){
                    if (this.get_sidebar()._widgets[i]._name == "FilterByContent") {
                        jQuery(this.get_sidebar()._widgets[i]._element).toggle(false)
                    }
                }
                else {
                    if (this.get_sidebar()._widgets[i]._name == "WithRating" ||
                        this.get_sidebar()._widgets[i]._name == "WithoutRating") {  // Hide the rating filters if no threadType constraint
                        jQuery(this.get_sidebar()._widgets[i]._element).toggle(false)
                    }
                }
            }
        }
        //attach to grid datasource change event
        jQuery(this.get_commentsGrid().get_grid()).data("kendoGrid").bind("dataBound", this._handleGridDataBoundDelegate);
    },

    /* --------------------  public methods ----------- */

    //Setup header back link button behavior
    setBackButton: function () {
        var queryParams = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var threadKey = queryParams.get("targetKey", null);
        var threadType = queryParams.get("threadType", null);
        if (this.get_backToComments()) {
            if (threadKey || threadType) {
                jQuery(this.get_backToComments()).attr("href", this.get_commentsUrl());
            }
            else {
                jQuery(this.get_backToComments()).toggle(false);
            }
        }
    },

    //Returns the correct Decision Screen by given key
    findDecisionScreen: function (key) {
        if (this.get_decisionScreens()) {
            var dsID = this.get_decisionScreens()[key];
            if (dsID) {
                return $find(dsID);
            }
        }
        return null;
    },

    /* --------------------  private methods ----------- */

    _getSelectedComments: function () {
        var selectedItems = [];
        var that = this;
        jQuery(this.get_commentsGrid().get_grid()).find("[aria-selected = true]").each(function () {
            selectedItems.push(jQuery(that.get_commentsGrid().get_grid()).data("kendoGrid").dataItem(this).Key);
        });
        return selectedItems;
    },

    //Handles dialog load event
    _loadDialogHandler: function (sender, e) {
        this._showDialogHandler(sender, e);
    },

    //Handles show dialog event
    _showDialogHandler: function (sender, e) {
        var args = sender._sfArgs;

        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            if (frameHandle.detailsView && frameHandle.detailsView.loadForm && args) {
                frameHandle.detailsView.loadForm(args.itemId);
            }
        }
    },

    //Handles close dialog event
    _closeDialogHandler: function (sender, args) {
        if (args.get_argument() == "rebind") {
            this.get_commentsGrid().fetchDataSource();
        }
    },

    //Open edit dialog by dialog name and pass the itemId 
    _tryOpenDialog: function (dialogName, itemId, rebindFunc) {
        var dialog = this.get_windowManager().getWindowByName(dialogName);
        if (dialog) {
            if (this.get_blackListedWindows().indexOf(dialogName) > -1) {
                var dialogManager = window.top.GetDialogManager();
                if (dialogManager) {
                    dialogManager.blacklistWindow(dialog);
                }
            }

            dialog.set_skin("Default");
            dialog.set_showContentDuringLoad(false);

            if ((dialog.get_width() == 100 && dialog.get_height() == 100) || (dialog._sfShouldMaximize)) {
                jQuery("body").addClass("sfLoadingTransition");
                dialog._sfShouldMaximize = true;
            }

            dialog._sfArgs = {
                itemId: itemId
            };

            dialog.show();
            Telerik.Sitefinity.centerWindowHorizontally(dialog);

            if (dialog._sfShouldMaximize) {
                dialog.maximize();
                jQuery("body").removeClass("sfLoadingTransition");
            }

            dialog.add_close(rebindFunc);
        }
    },

    //Set the count of the waiting approval items
    _setWaitingFroApprovalCount: function (widget, language) {
        if (widget && widget._buttonTextElem) {
            var that = this;
            var queryParams = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
            
            var requestData = null;

            if (this.get_commentsGrid()) {
                var parenThreadKey = queryParams.get("targetKey", null);
                if (parenThreadKey) {
                    requestData = Telerik.Sitefinity.JSON.stringify({ status: this._waitingForApprovalStatus, threadKey: parenThreadKey, take:1 });
                }
                else {
                    if (this.get_multiLingual()) {
                        requestData = Telerik.Sitefinity.JSON.stringify({ status: this._waitingForApprovalStatus, language: this._currentSelectedLanguage, take: 1 });
                    }
                    else{
                        requestData = Telerik.Sitefinity.JSON.stringify({ status: this._waitingForApprovalStatus, take: 1 });
                    }
                }
            }
            
            var requestUrl = this.get_commentsGrid().get_webServiceUrl() + "/comments/filter";

            var onSuccess = function (result) {
                var title = "";
                if (result.TotalCount && result.TotalCount > 0) {
                    if (widget.get_buttonText().indexOf('(') == -1) {
                        title = widget.get_buttonText() + " (" + result.TotalCount + ")";
                        widget.set_buttonText("");
                        widget.set_buttonText(title);
                    }
                    else {
                        title = widget.get_buttonText().substr(0, widget.get_buttonText().indexOf('('));
                        title += "(" + result.TotalCount + ")";
                        widget.set_buttonText("");
                        widget.set_buttonText(title);
                    }
                }
                else {
                    if (widget.get_buttonText().indexOf('(') != -1) {
                        title = widget.get_buttonText().substr(0, widget.get_buttonText().indexOf('('));
                        widget.set_buttonText("");
                        widget.set_buttonText(title);
                    }
                }
            }

            jQuery.ajax({
                type: 'POST',
                url: requestUrl,
                contentType: "application/json",
                data: requestData,
                cache: false,
                processData: false,
                success: onSuccess
            });

        }
    },

    // Mark items that are selected in the sidebar
    _markItemSelected: function (widget, value) {
        if (widget) {
            var element = null;
            if (widget.LinkElement) {
                element = widget.LinkElement;
            }
            else if (widget.get_linkElement) {
                element = widget.get_linkElement();
            }
            if (element) {
                if (value == true) {
                    jQuery(element).addClass(this._selectedItemFilterCssClass);
                }
                else {
                    jQuery(element).removeClass(this._selectedItemFilterCssClass);
                }
            }
        }
    },

    _setToolbarButtonsEnabledState: function (toEnable) {
        jQuery('.sfGroupBtn').each(function () {
            var elements = this.getElementsByTagName('a');
            for (var i = 0; i < elements.length; i++) {
                var el = elements[i];
                if (toEnable) {
                    $telerik.removeCssClasses(el, ['sfDisabledLinkBtn']);
                    //restore the removed postback when link disabled
                    jQuery(el).attr("href", " javascript: __doPostBack('" + el.id + "', '')");
                }
                else {
                    $telerik.addCssClasses(el, ['sfDisabledLinkBtn']);
                    //TODO: this fix removes the link with postback error at FF, but still a  new window is opened when "Middle" mouse button is pressed.
                    //Better aproach would be link to be replaced with span with appropriate styles.
                    //$(el).replaceWith("<span class='" + $(el).css() +  "'>" + $(el).text() + "<span/>");
                    jQuery(el).attr("href", "");
                }
            }
        });
        // Disables Actions menu in the toolbar when there are no selected items
        jQuery('.sfActionsDDL').each(function () {
            var clientId = this.id;
            var menu = $find(clientId);
            if (toEnable || jQuery(this).hasClass('sfAlwaysOn') || jQuery(this).parent().parent().hasClass('sfAlwaysOn')) {
                if (menu) {
                    menu.enable();
                    jQuery(this).find("a.rmDisabled").removeClass("sfDisabledLinkBtn");
                }
            }
            else {
                if (menu) {
                    menu.close();
                    menu.disable();
                    jQuery(this).find("a.rmDisabled").addClass("sfDisabledLinkBtn");
                }
            }
        });
    },

    /* -------------------- properties ---------------- */
    get_sidebar: function () {
        return this._sidebar;
    },
    set_sidebar: function (value) {
        this._sidebar = value;
    },

    get_toolbar: function () {
        return this._toolbar;
    },
    set_toolbar: function (value) {
        this._toolbar = value;
    },

    get_selectedItems: function () {
        return this._selectedItems;
    },
    set_selectedItems: function (value) {
        this._selectedItems = value;
    },

    get_contextBar: function () {
        return this._contextBar;
    },
    set_contextBar: function (value) {
        this._contextBar = value;
    },

    get_singleItemDeleteMsg: function () {
        return this._singleItemDeleteMsg;
    },
    set_singleItemDeleteMsg: function (value) {
        this._singleItemDeleteMsg = value;
    },    

    get_multipleItemsDeleteMsg: function () {
        return this._multipleItemsDeleteMsg;
    },
    set_multipleItemsDeleteMsg: function (value) {
        this._multipleItemsDeleteMsg = value;
    },

    get_showAllCommentsCommandName: function () {
        return this._showAllCommentsCommandName;
    },
    set_showAllCommentsCommandName: function (value) {
        this._showAllCommentsCommandName = value;
    },

    get_showOnlyCommentsCommandName: function () {
        return this._showOnlyCommentsCommandName;
    },
    set_showOnlyCommentsCommandName: function (value) {
        this._showOnlyCommentsCommandName = value;
    },

    get_showReviewsCommandName: function () {
        return this._showReviewsCommandName;
    },
    set_showReviewsCommandName: function (value) {
        this._showReviewsCommandName = value;
    },

    get_showMyCommentsCommandName: function () {
        return this._showMyCommentsCommandName;
    },
    set_showMyCommentsCommandName: function (value) {
        this._showMyCommentsCommandName = value;
    },

    get_showWaitingApprovalItemsCommandName: function () {
        return this._showWaitingApprovalItemsCommandName;
    },
    set_showWaitingApprovalItemsCommandName: function (value) {
        this._showWaitingApprovalItemsCommandName = value;
    },

    get_showPublishedItemsCommandName: function () {
        return this._showPublishedItemsCommandName;
    },
    set_showPublishedItemsCommandName: function (value) {
        this._showPublishedItemsCommandName = value;
    },

    get_showHiddenItemsCommandName: function () {
        return this._showHiddenItemsCommandName;
    },
    set_showHiddenItemsCommandName: function (value) {
        this._showHiddenItemsCommandName = value;
    },

    get_showMarkedAsSpamItemsCommandName: function () {
        return this._showMarkedAsSpamItemsCommandName;
    },
    set_showMarkedAsSpamItemsCommandName: function (value) {
        this._showMarkedAsSpamItemsCommandName = value;
    },

    get_showInapropriateItemsCommandName: function () {
        return this.showInapropriateItemsCommandName;
    },
    set_showInapropriateItemsCommandName: function (value) {
        this.showInapropriateItemsCommandName = value;
    },

    get_commentsUrl: function () {
        return this._commentsUrl;
    },
    set_commentsUrl: function (value) {
        this._commentsUrl = value;
    },
    get_filterCommandName: function () {
        return this._filterCommandName;
    },
    set_filterCommandName: function (value) {
        this._filterCommandName = value;
    },

    get_commentsGrid: function () {
        return this._commentsGrid;
    },
    set_commentsGrid: function (value) {
        this._commentsGrid = value;
    },

    get_backToComments: function () {
        return this._backToComments;
    },
    set_backToComments: function (value) {
        this._backToComments = value;
    },

    get_windowManager: function () {
        return this._windowManager;
    },
    set_windowManager: function (value) {
        this._windowManager = value;
    },

    get_blackListedWindows: function () {
        return this._blackListedWindows;
    },
    set_blackListedWindows: function (value) {
        this._blackListedWindows = value;
    },

    get_dialogIds: function () {
        return this._dialogIds;
    },
    set_dialogIds: function (value) {
        this._dialogIds = value;
    },

    get_decisionScreens: function () {
        return this._decisionScreens;
    },

    set_decisionScreens: function (value) {
        this._decisionScreens = value;
    },

    get_settingsLink: function () {
        return this._settingsLink;
    },
    set_settingsLink: function (value) {
        this._settingsLink = value;
    },

    get_commentsRestClient: function () {
        return this._commentsRestClient;
    },
    set_commentsRestClient: function (value) {
        this._commentsRestClient = value;
    },

    get_selectedLanguage: function () {
        return this._selectedLanguage;
    },
    set_selectedLanguage: function (value) {
        this._selectedLanguage = value;
    },

    get_multiLingual: function () {
        return this._multiLingual;
    },
    set_multiLingual: function (value) {
        this._multiLingual = value;
    },

    get_deleteConfirmationDialog: function () {
        return this._deleteConfirmationDialog;
    },
    set_deleteConfirmationDialog: function (value) {
        this._deleteConfirmationDialog = value;
    },

    get_showSectionsExceptAndResetFilterCommandName: function () {
        return this._showSectionsExceptAndResetFilterCommandName;
    },
    set_showSectionsExceptAndResetFilterCommandName: function (value) {
        this._showSectionsExceptAndResetFilterCommandName = value;
    }
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsMasterView", Sys.UI.Control);