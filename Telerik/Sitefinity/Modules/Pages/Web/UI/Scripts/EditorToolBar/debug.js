Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar = function () {
    this._cancelUrl = null;
    this._serviceUrl = null;
    this._baseItemServiceUrl = null;
    this._draftId = null;
    this._pageNodeId = null;
    this._mediaType = 0;
    this._clientLabelManager = null;
    this._clientManager = null;
    this._windowManager = null;
    this._takeOwnershipURL = null;
    this._zoneEditorId = null;
    this._leftToolbar = null;
    this._languageToolbar = null;
    this._mainToolbar = null;
    this._workflowMenu = null;
    this._workflowStatusField = null;
    this._warningsField = null;
    this._warnings = null;
    this._toolbarWrapper = null;
    this._toolbarLoadingPanel = null;
    this._windowCloseDelegate = null;
    this._windowClosedDelegate = null;
    this._noWorkflowActionsDelegate = null;
    this._noWorkflowActionsDialogDelegate = null;
    this._cannotModifyPageDialogId = null;
    this._isEditable = true;
    this._isContentEditable = true;
    this._localizationStrategy = null;
    this._hasChangesInOtherVariation = false;
    this._formMasterItemId = null;

    //If the theme is missing this property contains the name of the theme
    this._missingThemeName = null;

    this._dataItem = null;
    this._preventDeleteParentItem = null;

    this._hasStatus = null;
    this._statusLabelId = null;
    this._wasPublished = false;
    this._isPublished = false;
    this._currentLanguage = null;
    this._status = null;

    //Statuses - those map to the value of the Status property in EditorToolBar.cs
    this.STATUS_DRAFT = 'Draft';
    this.STATUS_PUBLISHED = 'Live';
    this.STATUS_DRAFT_NEWER = 'PublishedAndDraft';
    this.STATUS_UNPUBLISHED = 'Hidden';
    this.STATUS_INVISIBLE = 'Unpublished';

    // Messages
    this.UNSAVED_CHANGES_MESSAGE = "You have unsaved changes. Are you sure you want to exit the page?";

    //Holds the current status of the page (one of the STATUS_XXX values).
    this._currentStatus = 0;

    this._languageToolbar = null;

    //for permissions management:
    this._parentItemId = null;
    this._personalizationMasterId = null;
    this._pageTitle = null;

    this._messageControl = null;

    this._handlePageLoadDelegate = null;
    this._nonParallelRequestInvokedDelegate = null;
    this._nonParallelRequestCompletedDelegate = null;

    this._operationFailureDelegate = null;
    this._operationFailureQuietDelegate = null;
    this._takeOwnershipSuccessDelegate = null;
    this._publishDraftSuccessDelegate = null;
    this._unpublishPageSuccessDelegate = null;
    this._saveDraftSuccessDelegate = null;
    this._cancelDraftEditingSuccessDelegate = null;
    this._backToItemLabelTemplate = null;
    this._workflowMenuCommandDelegate = null;
    this._restoreTemplateSuccessDelegate = null;
    this._personalizationSegmentAddedDelegate = null;
    this._personalizationSegmentRemovedDelegate = null;
    this._personalizationSegmentChangedDelegate = null;
    this._navigateToVariationDelegate = null;

    this._moreActionsText = null;
    this._workflowItemId = null;
    this._workflowItemState = null;
    this._workflowItemType = null;
    this._pageUrl = null;

    this._templatesService = null;
    this._templatePagesCount = null;
    this._templateTemplatesCount = null;
    this._singleTemplateInUseDialogId = null;
    this._deleteConfirmationDialogId = null;

    this._personalizationSelector = null;
    this._editorToolbarSelectors = null;
    this._cancelDraftEditingAlternativeUrl = null;
    this._blockOnDiscardTemp = false;
    this._discardTempDelegate = null;
    Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar.initializeBase(this);
}
Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar.callBaseMethod(this, "initialize");

        editorToolBar = this;

        if (this.get_mediaType() == 1) {
            $("body").addClass("sfTemplateEditor");
        } else if (this.get_mediaType() == 2) {
            $("body").addClass("sfFormsEditor");
        } else if (this.get_mediaType() == 3 || this.get_mediaType() == 4) { //Newsletter campaign or Newsletter template
            $("body").addClass("sfNewsletterEditor");
        }

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);

        this._nonParallelRequestInvokedDelegate = Function.createDelegate(this, this._nonParallelRequestInvokedHandler);
        this._nonParallelRequestCompletedDelegate = Function.createDelegate(this, this._nonParallelRequestCompletedHandler);
        this.add_nonParallelRequestInvoked(this._nonParallelRequestInvokedDelegate);
        this.add_nonParallelRequestCompleted(this._nonParallelRequestCompletedDelegate);

        if (this._dataItem) {
            this._dataItem = Sys.Serialization.JavaScriptSerializer.deserialize(this._dataItem);
        }

        this._operationFailureDelegate = Function.createDelegate(this, this._operationFailure);
        this._operationFailureQuietDelegate = Function.createDelegate(this, this._operationFailureQuiet);
        this._takeOwnershipSuccessDelegate = Function.createDelegate(this, this._takeOwnershipSuccess);
        this._publishDraftSuccessDelegate = Function.createDelegate(this, this._publishDraftSuccess);
        this._unpublishPageSuccessDelegate = Function.createDelegate(this, this._unpublishPageSuccess);
        this._saveDraftSuccessDelegate = Function.createDelegate(this, this._saveDraftSuccess);
        this._cancelDraftEditingSuccessDelegate = Function.createDelegate(this, this.cancelDraftEditingSuccess);
        this._workflowMenuCommandDelegate = Function.createDelegate(this, this._workflowMenuCommandHandler);
        this._restoreTemplateSuccessDelegate = Function.createDelegate(this, this._restoreTemplateSuccess);
        this._deleteItemSuccessDelegate = Function.createDelegate(this, this._deleteItemSuccess);
        this._deleteItemPromptActionDelegate = Function.createDelegate(this, this._deleteItemPromptAction);
        this._personalizationSegmentAddedDelegate = Function.createDelegate(this, this._personalizationSegmentAdded);
        this._personalizationSegmentRemovedDelegate = Function.createDelegate(this, this._personalizationSegmentRemoved);
        this._personalizationSegmentChangedDelegate = Function.createDelegate(this, this._personalizationSegmentChanged);

        this._discardTempDelegate = Function.createDelegate(this, this._discardTempAction);

        this._noWorkflowActionsDelegate = Function.createDelegate(this, this._noWorkflowActionsHandler);
        this._workflowFailureDelegate = Function.createDelegate(this, this._workflowFailureHandler);
        if (this.get_workflowMenu()) {
            this.get_workflowMenu().add_onNoWorkflowActions(this._noWorkflowActionsDelegate);
            this.get_workflowMenu().add_onWorkflowFailure(this._workflowFailureDelegate);

        }
        this._noWorkflowActionsDialogDelegate = Function.createDelegate(this, this._noWorkflowActionsDialogHandler);

        this._windowCloseDelegate = Function.createDelegate(this, this._windowCloseHandler);
        this._windowClosedDelegate = Function.createDelegate(this, this._windowClosedHandler);
        window.onbeforeunload = this._windowCloseDelegate;
        //window.onunload = this._windowClosedDelegate;
        //Sys.Application.add_unload(this._windowClosedDelegate);
        $(window).on("unload", this._windowClosedDelegate);

        if (this.get_mainToolbar()) {
            var saveDraftItem = this.get_mainToolbar().findItemByValue("SaveDraft");
            if (saveDraftItem) {
                if (this._zoneEditorId && $find(this._zoneEditorId)) {
                    // DesignMediaType.Template is 2-nd in the mediaType enum and, thus, has id = 1
                    if (this.get_mediaType() === 1) {
                        if (!$find(this._zoneEditorId).get_isNewDraft()) {
                            $(saveDraftItem.get_linkElement()).removeClass('sfDisplayNoneImportant');
                        }
                    } else {
                        $(saveDraftItem.get_linkElement()).removeClass('sfDisplayNoneImportant');
                    }
                } else {
                    $(saveDraftItem.get_linkElement()).removeClass('sfDisplayNoneImportant');
                }
            }
        }

        //Fix for TeamPulse #35200: Personalization: Page is locked after 'Back to Pages' button
        //This code alternates the behaviour of PersonalizedPageSelector.
        //Originally it lists the segments, and upon clicking it just navigates to another url (cancelling the onbeforeunload to avoid user confirmation).
        //This code cancels the original behaviour and assigns new click handlers (still cancels the onbeforeunload)-
        //sends it first to cancel the edited draft, then sets the url to navigate to once the process is done.
        this._navigateToVariationDelegate = Function.createDelegate(editorToolBar, editorToolBar.navigateToVariation);
        if (this.get_personalizationSelector()) {
            var personalizationSelector = $(this.get_personalizationSelector()._element);
            personalizationSelector.ready(function () {
                var personalizationSelectorControl = editorToolBar.get_personalizationSelector();
                personalizationSelectorControl.add_createPersonalizedPageClicked(function (sender, e) {
                    var hasChanges = editorToolBar.get_hasChanges();
                    if (hasChanges && !confirm(editorToolBar.UNSAVED_CHANGES_MESSAGE)) {
                        return;
                    }

                    editorToolBar.openPersonalizationDialog();
                });

                personalizationSelectorControl.add_pageSegmentClicked(function (sender, e) {
                    var element = e.target;
                    window.onbeforeunload = null;
                    var segmentId = $(element).attr("data-id");
                    var url = '';
                    if (segmentId !== Telerik.Sitefinity.getEmptyGuid()) {
                        url = QueryStringManager.setValue(window.location.href, "segment", segmentId);
                    } else {
                        url = QueryStringManager.removeKey(window.location.href, "segment");
                    }

                    editorToolBar._navigateToVariationDelegate(url);
                });

                if (editorToolBar._zoneEditorId) {
                    var zoneEditor = $find(editorToolBar._zoneEditorId);
                    if (zoneEditor) {
                        zoneEditor.add_personalizationSegmentAdded(editorToolBar._personalizationSegmentAddedDelegate);
                        zoneEditor.add_personalizationSegmentRemoved(editorToolBar._personalizationSegmentRemovedDelegate);
                        zoneEditor.add_personalizationSegmentChanged(editorToolBar._personalizationSegmentChangedDelegate);
                        personalizationSelectorControl.add_widgetSegmentClicked(function (sender, e) {
                            window.onbeforeunload = null;
                            var element = e.target;
                            var segmentName = $(element).attr("data-name");
                            zoneEditor.selectSegment(segmentName);
                            personalizationSelectorControl.setCurrentWidgetSegment(segmentName);
                        });
                    }
                }
            });
        }

        var editorToolbarSelectors = this.get_editorToolbarSelectors();
        if (editorToolbarSelectors) {
            for (var i = 0; i < editorToolbarSelectors.length; i++) {
                var selectorControl = $find(editorToolbarSelectors[i]);
                if (!selectorControl) {
                    continue;
                }

                var selectorControlType = Object.getType(selectorControl);
                if (selectorControlType.implementsInterface(Telerik.Sitefinity.Modules.Pages.Web.UI.IEditorToolbarSelector)) {
                    var selectorControlElement = $(selectorControl._element);
                    selectorControlElement.ready(function () {
                        selectorControl.set_pageInfo(editorToolBar.get_pageNodeId(), editorToolBar.get_dataItem(), editorToolBar.get_currentLanguage());

                        selectorControl.set_navigateAction(editorToolBar._navigateToVariationDelegate);
                    });
                }
            }
        }

        this._hasChangesInOtherVariation = this.get_hasChangesFromQueryString();
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar.callBaseMethod(this, "dispose");
        editorToolBar = null;

        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }

        if (this._operationFailureDelegate) {
            delete this._operationFailureDelegate;
        }

        if (this._takeOwnershipSuccessDelegate) {
            delete this._takeOwnershipSuccessDelegate;
        }

        if (this._publishDraftSuccessDelegate) {
            delete this._publishDraftSuccessDelegate;
        }
        if (this._saveDraftSuccessDelegate) {
            delete this._saveDraftSuccessDelegate;
        }

        if (this._unpublishPageSuccessDelegate) {
            delete this._unpublishPageSuccessDelegate;
        }

        if (this._cancelDraftEditingSuccessDelegate) {
            delete this._cancelDraftEditingSuccessDelegate;
        }
        if (this._nonParallelRequestInvokedDelegate) {
            delete this._nonParallelRequestInvokedDelegate;
        }
        if (this._nonParallelRequestCompletedHandler) {
            delete this._nonParallelRequestCompletedHandler;
        }
        if (this._workflowMenuCommandDelegate) {
            delete this._workflowMenuCommandDelegate;
        }
        if (this._restoreTemplateSuccessDelegate) {
            delete this._restoreTemplateSuccessDelegate;
        }
        if (this._windowCloseDelegate) {
            window.onbeforeunload = null;
            delete this._windowCloseDelegate;
        }
        if (this._windowClosedDelegate) {
            window.onunload = null;
            delete this._windowClosedDelegate;
        }
        if (this._deleteItemSuccessDelegate) {
            delete this._deleteItemSuccessDelegate;
        }

        if (this._deleteItemPromptActionDelegate) {
            delete this._deleteItemPromptActionDelegate;
        }

        if (this._noWorkflowActionsDelegate) {
            delete this._noWorkflowActionsDelegate;
        }

        if (this._noWorkflowActionsDialogDelegate) {
            delete this._noWorkflowActionsDialogDelegate;
        }

        if (this._workflowFailureDelegate) {
            delete this._workflowFailureDelegate;
        }

        if (this._personalizationSegmentAddedDelegate) {
            delete this._personalizationSegmentAddedDelegate;
        }

        if (this._personalizationSegmentRemovedDelegate) {
            delete this._personalizationSegmentRemovedDelegate;
        }

        if (this._personalizationSegmentChangedDelegate) {
            delete this._personalizationSegmentChangedDelegate;
        }

        if (this._navigateToVariationDelegate) {
            delete this._navigateToVariationDelegate;
        }
    },

    getFormattedBackLabel: function (backText) {
        return String.format(this._backToItemLabelTemplate, backText);
    },

    /* --------------------  public methods ----------- */

    /* ==================== Take Ownership ============ */

    takeOwnership: function () {
        var url = this.get_serviceUrl();

        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        url += this._takeOwnershipURL;
        url += this.get_draftId();

        this.get_clientManager().InvokeGet(url, null, null, this._takeOwnershipSuccessDelegate, this._operationFailureDelegate);
    },

    /* ==================== End Take Ownership ============ */

    publishDraft: function () {
        var url = this.get_serviceUrl();

        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        var data = null;
        switch (this.get_mediaType()) {
            case 0:
            case 3: // newsletter campaign
            case 4: // newsletter template
                url += "Page/Publish/" + this.get_draftId() + "/";
                break;
            case 1:
                url += "Template/Publish/" + this.get_draftId() + "/";

                this.get_clientManager().set_uiCulture(this.get_currentLanguage());
                break;
            case 2:
                var settingsEditor = $find(this._zoneEditorId).getSettingsEditor();
                url += "Form/Publish/" + this.get_draftId() + "/";
                data = settingsEditor.getFormDescription();

                this.get_clientManager().set_uiCulture(this.get_currentLanguage());
                break;
        }
        this.raiseNonParallelRequestInvoked();
        this.get_clientManager().InvokePut(url, null, null, data, this._publishDraftSuccessDelegate, this._operationFailureDelegate);
    },

    unpublishItem: function () {
        switch (this.get_mediaType()) {
            case 0:
                this.unpublishPage();
                break;
            case 1:

                break;
            case 2:
                this.get_clientManager().set_uiCulture(this.get_currentLanguage());
                this.unpublishForm();
                break;
        }

        this._wasPublished = false;
    },

    restoreTemplateToDefault: function () {
        var url = this._getItemServiceUrl();
        url += 'Template/RestoreDefault/';
        var id = this.get_parentItemId();

        this.get_clientManager().InvokePut(url, null, null, id, this._restoreTemplateSuccessDelegate, this._operationFailureDelegate);
    },

    unpublishPage: function () {
        var url = this._getItemServiceUrl();
        url += 'UnpublishPage/';

        this.get_clientManager().InvokePut(url, null, null, this.get_pageNodeId(), this._unpublishPageSuccessDelegate, this._operationFailureDelegate);
    },

    unpublishForm: function () {
        var url = this._getItemServiceUrl();
        url += 'unpublish/';
        this.get_clientManager().InvokePut(url, null, null, this.get_parentItemId(), this._unpublishPageSuccessDelegate, this._operationFailureDelegate);
    },

    saveDraft: function (workflowOperation, contextBag) {
        var url = this.get_serviceUrl();
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }
        var successFunction = this._saveDraftSuccessDelegate;
        var urlParams = null;
        var data = null;
        if (this.get_mediaType() == 1) {
            url += "Template/Save/" + this.get_draftId() + "/";
        }
        else if (this.get_mediaType() == 0) {
            data = this.get_pageNodeId();
            url += "Page/Save/?workflowItemState=" + this.get_workflowMenu().get_workflowItemState();
            if (workflowOperation) {
                url += "&workflowOperation=" + workflowOperation;
                var wfItem = this.get_workflowMenu().get_dataItem();
                if (workflowOperation == 'Schedule') {
                    if (contextBag) {
                        wfItem.ExpirationDate = null;
                        for (i = 0; i < contextBag.length; i++) {
                            if (contextBag[i].key == "PublicationDate")
                                wfItem.PublicationDate = new Date(contextBag[i].value);
                            if (contextBag[i].key == "ExpirationDate")
                                wfItem.ExpirationDate = new Date(contextBag[i].value);
                        }
                    } 
                }

                if (contextBag) {
                    for (i = 0; i < contextBag.length; i++) {
                        if (contextBag[i].key == "Note")
                            wfItem.Note = contextBag[i].value;
                    }
                }

                data = wfItem;
                successFunction = this._publishDraftSuccessDelegate;
            }

        }
        else if (this.get_mediaType() == 2) {
            var settingsEditor = $find(this._zoneEditorId).getSettingsEditor();
            var draftId = this.get_draftId();
            data = settingsEditor.getFormDescription();
            url += "Form/Save/" + draftId + "/";

            this.get_clientManager().set_uiCulture(this.get_currentLanguage());
        }

        this.get_clientManager().InvokePut(url, urlParams, null, data, successFunction, this._operationFailureDelegate);
    },

    navigateToVariation: function (url) {
        if (url) {
            window.onbeforeunload = null;
            window.onunload = null;
            $(window).off('unload');

            var hasChanges = this.get_hasChanges();
            if (hasChanges) {
                url = QueryStringManager.setValue(url, 'hasChanges', hasChanges);
            } else {
                url = QueryStringManager.removeKey(url, 'hasChanges');
            }

            window.location = url;
        }
    },

    get_hasChangesFromQueryString: function () {
        var hasChangesKey = 'hasChanges';
        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        if (queryString.contains(hasChangesKey)) {
            var stringVal = queryString.get(hasChangesKey);
            if (stringVal === 'true') {
                return true
            }
        }

        return false;
    },

    get_hasChanges: function () {
        if (this._hasChangesInOtherVariation) {
            return true;
        }

        var zoneEditor = $find(this._zoneEditorId);
        if (zoneEditor) {
            var isChangeMade = $find(this._zoneEditorId).get_isChangeMade();
            if (isChangeMade) {
                return true;
            }
        }

        return false;
    },

    cancelDraftEditing: function () {
        var hasChanges = this.get_hasChanges();
        if (hasChanges) {
            if (!confirm(this.UNSAVED_CHANGES_MESSAGE)) {
                return;
            }
        }

        var url = this.get_serviceUrl();
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        //We use _cancelDraftEditingSuccessDelegate for both success and failure because we want to go back to the grid even if unlocking does not succeed.

        if (this.get_mediaType() == 1) {
            url += "Template/" + this.get_draftId();
            this.get_clientManager().InvokeDelete(url, null, null, this._cancelDraftEditingSuccessDelegate, this._cancelDraftEditingSuccessDelegate);
        }
        else if (this.get_mediaType() == 0) {
            url += "Page/" + this.get_draftId();
            this.get_clientManager().InvokeDelete(url, null, null, this._cancelDraftEditingSuccessDelegate, this._cancelDraftEditingSuccessDelegate);
        }
        else if (this.get_mediaType() == 2) {
            this.get_clientManager().set_uiCulture(this.get_currentLanguage());
            url += "Form/" + this.get_draftId();
            this.get_clientManager().InvokeDelete(url, null, null, this._cancelDraftEditingSuccessDelegate, this._cancelDraftEditingSuccessDelegate);
        }
        else if (this.get_mediaType() == 3 || this.get_mediaType() == 4) {
            window.onunload = null;
            window.onbeforeunload = null;

            url += "Page/" + this.get_draftId();
            this.get_clientManager().InvokeDelete(url, null, null, this._cancelDraftEditingSuccessDelegate, this._cancelDraftEditingSuccessDelegate);
        }
    },

    //Unlocks the current page
    unlockCurrentPage: function (successDelegate) {
        var url = this.get_serviceUrl();
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        url += "Page/" + this.get_draftId();
        this.get_clientManager().InvokeDelete(url, null, null, successDelegate, null);
        zoneEditor.set_isUnlockingDone(true); //not a secure code as the unlocking may fail
    },

    cancelDraftEditingSuccess: function (e) {
        var zoneEditor = $find(this._zoneEditorId);
        if (zoneEditor) {
            zoneEditor.set_isChangeMade(false);
            this._hasChangesInOtherVariation = false;
            zoneEditor.set_isUnlockingDone(true);
        }
        if (this._cancelDraftEditingAlternativeUrl) {
            var url = this._cancelDraftEditingAlternativeUrl;
            this._cancelDraftEditingAlternativeUrl = null;
            window.location = url;
        }
        else
            window.location = this.get_cancelUrl();
    },

    _deleteItemSuccess: function () {
        window.onunload = null;
        window.onbeforeunload = null;
        window.location = this.get_cancelUrl();
    },

    deleteItem: function () {
        var isTEmplate = this.get_mediaType() == 1;
        if (this._templatePagesCount) {
            var pagesCount = parseInt(this._templatePagesCount);
            var templatesCount = parseInt(this._templateTemplatesCount);

            if ((!isNaN(pagesCount)) && (!isNaN(templatesCount)) && ((pagesCount > 0) || (templatesCount > 0))) {
                var promptDialog = $find(this._singleTemplateInUseDialogId);
                if (promptDialog != null) {
                    var message = promptDialog.get_initialMessage();
                    message = String.format(message, '<strong>' + pagesCount + '</strong>', '<strong>' + templatesCount + '</strong>');
                    promptDialog.show_prompt(null, message);
                }
                return;
            }
        }
        var deleteConfirmationDialog = $find(this._deleteConfirmationDialogId);
        if (isTEmplate) {
            if (deleteConfirmationDialog != null) {
                var confirmation = deleteConfirmationDialog.show_prompt(null, null, this._deleteItemPromptActionDelegate, this);
            }
        }
    },

    _deleteItemPromptAction: function (sender, eventargs) {
        if (eventargs.get_commandName() == "ok") {
            this.get_clientManager().InvokePost(
                sender._lastContext._templatesService,
                { "language": sender._lastContext.get_currentLanguage() },
                ["batch"],
                [sender._lastContext._parentItemId],
                sender._lastContext._deleteItemSuccessDelegate,
                sender._lastContext._operationFailureDelegate,
                sender._lastContext);
        }
    },

    /* -------------------- Operate With Dialogs ------ */

    openPermissionsDialog: function () {
        var params = new Object();
        params.backLabelText = this.getFormattedBackLabel(this.get_pageTitle());
        this.openDialog('permissions', null, params, this.get_parentItemId());
    },

    editFormRules: function () {
        var controlId = this._formMasterItemId;
        var event = new CustomEvent('edit-form-rule', { detail: { controlId: controlId, culture: this.get_currentLanguage() } });
        document.dispatchEvent(event);
    },

    openHistoryDialog: function () {
        var item = new Object;
        item.Id = this.get_parentItemId();
        item.Title = this.get_pageTitle();
        item.IsEditable = this.get_isEditable();
        item.IsContentEditable = this.get_isContentEditable();
        item.LocalizationStrategy = this.get_localizationStrategy();
        var key = new Object;
        key.Id = this.get_parentItemId();
        var params = new Object;
        params.backItemTitle = item.Title;

        if (this.get_mediaType() === 0) {
            params["typeName"] = "Telerik.Sitefinity.Pages.Model.PageData";
        } else if (this.get_mediaType() === 1) {
            params["typeName"] = "Telerik.Sitefinity.Pages.Model.PageTemplate";
        }

        this.openDialog('history', item, params, key, { language: this._currentLanguage });
    },

    openPersonalizationDialog: function () {
        var item = new Object;
        item.Id = this.get_personalizationMasterId();
        item.Title = this.get_pageTitle();
        var key = new Object;
        key.Id = this.get_personalizationMasterId();
        var params = new Object;
        params.backItemTitle = item.Title;
        $find(this._zoneEditorId).set_isPageRefreshControlled(true);
        this.openDialog('personalize', item, params, key, { language: this._currentLanguage });
    },

    openDialog: function (commandName, dataItem, params, key, commandArgument) {

        var dialog = this.get_windowManager().getWindowByName(commandName);

        dialog.set_skin("Default");
        dialog.set_showContentDuringLoad(false);
        if (dialog) {
            //var commandArgument = null;
            if (commandName == "edit") {
                if (!this._isEditable) {
                    var promptDialog = $find(this._cannotModifyPageDialogId);
                    if (promptDialog != null) {
                        var message = promptDialog.get_initialMessage();
                        promptDialog.show_prompt(null, message);
                        return;
                    }
                }
                commandArgument = { language: this.get_currentLanguage(), languageMode: "edit" }
            }
            params.backLabelText = this.getFormattedBackLabel(this.get_pageTitle());
            dialog._sfArgs = new Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs(
                commandName, dataItem, this, dialog, params, key, commandArgument
                );

            if (!dialog._sfShowDialogExtension) {
                //var func = Function.createDelegate(this, this._showDialogExtension);
                dialog._sfShowDialogExtension = this._showDialogExtension;
                dialog.add_show(dialog._sfShowDialogExtension);
            }
            //check if the the dialog is set to reload on each showing.
            //If that's the case - the _loadDialogExtension handler should be reattached.
            if (!dialog._sfLoadDialogExtension || dialog.get_reloadOnShow()) {
                //var func = Function.createDelegate(this, this._loadDialogExtension);
                dialog._sfLoadDialogExtension = this._loadDialogExtension;
                dialog.add_pageLoad(dialog._sfLoadDialogExtension);
            }
            if (!dialog._sfCloseDialogExtension) {
                //var func = Function.createDelegate(this, this._closeDialogExtension);
                dialog._sfCloseDialogExtension = this._closeDialogExtension;
                dialog.add_close(dialog._sfCloseDialogExtension);
            }
        }
        dialog.show();

        Telerik.Sitefinity.centerWindowHorizontally(dialog);

        if (dialog.get_width() == 100 && dialog.get_height() == 100) {
            dialog.maximize();
        }
    },

    setCurrentStatus: function (status) {
        this._currentStatus = status;
        this._updateForNewPageStatus();
    },

    _updateForNewPageStatus: function () {
        if (this._hasStatus == true) {
            var status = this._currentStatus;
            if (status == this.STATUS_PUBLISHED) {
                this._showUnpublishMenuItem(true);
            }
            else if (status == this.STATUS_DRAFT) {
            }
            else if (status == this.STATUS_DRAFT_NEWER) {
            }
            else if (status == this.STATUS_UNPUBLISHED) {
                this._showUnpublishMenuItem(false);
            }
            this.setStatusText(this._get_resourceString(status));
        }
    },

    _restoreTemplateSuccess: function (e) {
        window.location = window.location;
    },

    _showUnpublishMenuItem: function (val) {
        var toolBar = this.get_leftToolbar();
        //This is incorrect - to get the drop-down item by text. However since it does not support Value
        //this appears to be the only way
        var dropDown = toolBar.findItemByText(this._moreActionsText);
        if (dropDown) {
            var button = null;
            var buttons = dropDown.get_buttons();
            for (var i = 0; i < buttons.get_count() ; i++) {
                var b = buttons.getButton(i);
                if (b.get_value() == 'unpublish') {
                    button = b;

                    if (val == true) {
                        b.show();
                        if (i == 0) {
                            buttons.getButton(i + 1).show();
                        }
                    }
                    else {
                        b.hide();
                        if (i == 0) {
                            buttons.getButton(i + 1).hide();
                        }
                    }
                    break;
                }
            }
        }
    },

    setStatusText: function (text) {
        var statusLabel = $("*[id$='" + this._statusLabelId + "']").get(0);
        if (statusLabel) {
            statusLabel.innerHTML = text;
        }
    },

    //Returns the service URL corresponding to the current media type, e.g. /../PagesService.svc for mediaType page.
    _getItemServiceUrl: function () {
        var url = this._baseItemServiceUrl;
        if (url.charAt(url.length - 1) != "/") {
            url += "/";
        }

        return url;
    },

    _handleDialogClosed: function (sender, originalArgs) {
        var dialogArgument = originalArgs.get_argument();
        if (dialogArgument) {
            if (dialogArgument == "CopyAsNew") {
                window.location = window.location;
            }
            if (dialogArgument == "takeOwnership") {
                window.location = this.get_cancelUrl();
            }
            else if (dialogArgument == "deleteHistoryItem") {
                this.openHistoryDialog();
            }
            else {
                if (dialogArgument != "ToMainScreen") {
                    var args = sender._sfArgs;
                    var dataItem = null;
                    if (typeof dialogArgument == "object") {
                        if (dialogArgument.DataItem) {
                            if (dialogArgument.Context && dialogArgument.Context.get_widgetCommandName && dialogArgument.Context.get_widgetCommandName() == "create") {
                                dataItem = dialogArgument.DataItem;
                            }
                        }
                        else {
                            dataItem = dialogArgument;
                        }
                    }

                    this.onDialogClosed(args.get_commandName(), dataItem, args.get_itemsList(), args.get_dialog(), args.get_params(), args.get_key());
                }
            }

        }
    },

    _loadDialogExtension: function (sender, e) {
        var args = sender._sfArgs;
        var dialog = args.get_dialog();
        var itemsList = args.get_itemsList();

        dialog.remove_pageLoad(dialog._sfLoadDialogExtension);
        dialog._sfShowDialogExtension(sender, args, true);
    },

    _closeDialogExtension: function (sender, originalArgs) {
        editorToolBar._handleDialogClosed(sender, originalArgs);
    },

    _showDialogExtension: function (sender, e, isLoad) {
        var args = sender._sfArgs;
        var itemsList = args.get_itemsList();
        var dataItem = args.get_dataItem();
        var commandName = args.get_commandName();
        var dialog = args.get_dialog();
        var params = args.get_params();
        var key = args.get_key();
        //        if (this._onDialogShowed(commandName, dataItem, itemsList, dialog, params, key).get_cancel() != true) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            //            if (itemsList.get_scrollOpenedDialogsToTop()) {
            frameHandle.scrollTo(0, 0);
            //            }
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog && (!dialog.get_reloadOnShow() || isLoad)) {
                frameHandle.createDialog(commandName, dataItem, itemsList, dialog, params, key, args.get_commandArgument());
            }
        }
        //        }
    },

    /* -------------------- events -------------------- */

    add_viewLoaded: function (handler) {
        this.get_events().addHandler('viewLoaded', handler);
    },

    remove_viewLoaded: function (handler) {
        this.get_events().removeHandler('viewLoaded', handler);
    },

    add_command: function (delegate) {
        /// <summary>Happens when a custom command is fired. Can be cancelled.</summary>
        this.get_events().addHandler('command', delegate);
    },

    remove_command: function (delegate) {
        /// <summary>Happens when a custom command was fired. Can be cancelled.</summary>
        this.get_events().removeHandler('command', delegate);
    },

    add_dialogClosed: function (delegate) {
        /// <summary>Happens when the dialog was closed. Can be cancelled.</summary>
        this.get_events().addHandler('dialogClosed', delegate);
    },

    remove_dialogClosed: function (delegate) {
        /// <summary>Happens when the dialog was closed. Can be cancelled.</summary>
        this.get_events().removeHandler('dialogClosed', delegate);
    },

    add_nonParallelRequestInvoked: function (delegate) {
        /// <summary>Happens when a requst that can't be executed with any other action is being invoked.</summary>
        this.get_events().addHandler("nonParallelRequestInvoked", delegate);
    },

    remove_nonParallelRequestInvoked: function (delegate) {
        /// <summary>Happens when a requst that can't be executed with any other action is being invoked.</summary>
        this.get_events().removeHandler("nonParallelRequestInvoked", delegate);
    },

    add_nonParallelRequestCompleted: function (delegate) {
        /// <summary>Happens when a requst that can't be executed with any other action is completed</summary>
        this.get_events().addHandler("nonParallelRequestCompleted", delegate);
    },

    remove_nonParallelRequestCompleted: function (delegate) {
        /// <summary>Happens when a requst that can't be executed with any other action is completed</summary>
        this.get_events().removeHandler("nonParallelRequestCompleted", delegate);
    },

    raiseNonParallelRequestInvoked: function () {
        var handler = this.get_events().getHandler("nonParallelRequestInvoked");
        if (handler)
            handler(this);
    },

    raiseNonParallelRequestCompleted: function () {
        var handler = this.get_events().getHandler("nonParallelRequestCompleted");
        if (handler)
            handler(this);
    },

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {
        var handler = this.get_events().getHandler('viewLoaded');
        if (handler) {
            handler(this);
        }
        if (this._workflowMenu) {
            //TODO: add item as last arg
            //Disabled per UX request.
            //this._workflowMenu.set_messageControl(this.get_messageControl());
            this._workflowMenu.set_contentCulture(this._currentLanguage);
            this._workflowMenu.bindWorkflowVisuals(this.get_workflowItemType(), null, this.get_pageNodeId(), null);
            this._workflowMenu.add_command(this._workflowMenuCommandDelegate);
            this._workflowMenu.set_dataItem(this.get_dataItem());
            this._workflowMenu.set_preventDeleteParentItem(this._preventDeleteParentItem);
            this._workflowMenu.set_contentWorkflowStatusInfoField(this._workflowStatusField);
            if (this._workflowStatusField) {
                this._workflowStatusField.set_wasPublished(this._wasPublished);
                if (this._dataItem && this._dataItem.PublicationDate) {
                    this._workflowStatusField.set_pagePublicationDate(this._dataItem.PublicationDate);
                }
            }
        }

        if (this._warningsField) {
            this._warningsField.set_dataItemContext({ Warnings: this._warnings });
        }
    },

    _nonParallelRequestInvokedHandler: function (sender, args) {
        this.get_toolbarLoadingPanel().show(this._toolbarWrapper.id);
    },

    _nonParallelRequestCompletedHandler: function (sender, args) {
        this.get_toolbarLoadingPanel().hide(this._toolbarWrapper.id);
    },
    _windowCloseHandler: function (sender) {
        var isChangeMade = $find(this._zoneEditorId).get_isChangeMade();
        var isUnloadControlled = $find(this._zoneEditorId).get_isPageRefreshControlled();
        if (!isUnloadControlled) {
            if (isChangeMade) {
                return this.get_clientLabelManager().getLabel("PageResources", "PromptNavigateFromEditorToolBar") +
                    this.get_clientLabelManager().getLabel("PageResources", "PromptLeavePageEditor");
            }
            if (!$find(this._zoneEditorId).get_isUnlockingDone()) {
                return this.get_clientLabelManager().getLabel("PageResources", "PromptNavigateFromEditorToolBarLocked") +
                    this.get_clientLabelManager().getLabel("PageResources", "PromptLeavePageEditor");
            }
        }
    },

    _windowClosedHandler: function (sender) {
        var zoneEditor = $find(this._zoneEditorId);
        if (zoneEditor != null && zoneEditor.get_isPageRefreshControlled()) {
            zoneEditor.set_isChangeMade(false);
            this._hasChangesInOtherVariation = false;
        }

        // callbacks do not work. always get state canceled, but in a request inspector like fiddler
        // their server returns 200 OK. Left only for reference. This works only because of the delay timeout.
        if (zoneEditorShared && !zoneEditorShared.get_isPageRefreshControlled()) {
            this._blockOnDiscardTemp = true;
            var timeout = new Date().getTime() + 1000;

            var url = this.get_serviceUrl();
            if (url.charAt(url.length - 1) != "/") {
                url += "/";
            }

            if (this.get_mediaType() == 1) {
                url += "Template/" + this.get_draftId();
                this.get_clientManager().InvokeDelete(url, null, null, this._discardTempDelegate, this._discardTempDelegate);
            }
            else if (this.get_mediaType() == 0) {
                url += "Page/" + this.get_draftId() + "/";
                this.get_clientManager().InvokeDelete(url, null, null, this._discardTempDelegate, this._discardTempDelegate);
            }
            //else if (this.get_mediaType() == 2) {
            //    this.get_clientManager().set_uiCulture(this.get_currentLanguage());
            //    url += "Form/" + this.get_draftId();
            //    this.get_clientManager().InvokeDelete(url, null, null, this._discardTempDelegate, this._discardTempDelegate);
            //}

            while (true) {
                if (!this._blockOnDiscardTemp)
                    break;

                var currentTime = new Date().getTime();
                if (currentTime > timeout)
                    break;
            }
        }
    },

    _discardTempAction: function () {
        this._blockOnDiscardTemp = false;
    },

    _noWorkflowActionsHandler: function (sender, eventArgs) {
        var prompt = $find(this.get_cannotModifyPageDialogId());
        if (prompt) {
            prompt.add_command(this._noWorkflowActionsDialogDelegate);
            prompt.show_prompt();
        }
    },

    _noWorkflowActionsDialogHandler: function (sender, eventArgs) {
        if (eventArgs.get_commandName().toLowerCase() == "ok") {
            this.cancelDraftEditing();
        }
    },

    _workflowFailureHandler: function (sender, args) {
        var handled = zoneEditor.get_lockingHandler().tryHandleError(args.get_sender());
        if (handled) {
            args.set_cancel(true);
        }
    },

    _personalizationSegmentAdded: function (sender, args) {
        if (this.get_personalizationSelector()) {
            this.get_personalizationSelector().addSegment(args.segmentName, args.mediaType);
        }
    },

    _personalizationSegmentRemoved: function (sender, args) {
        if (this.get_personalizationSelector()) {
            this.get_personalizationSelector().removeSegments(args.segmentNames, args.mediaType);
        }
    },

    _personalizationSegmentChanged: function (sender, args) {
        if (this.get_personalizationSelector()) {
            this.get_personalizationSelector().onWidgetSegmentChange(args);
        }
    },

    /* -------------------- private methods ----------- */

    _workflowMenuCommandHandler: function (sender, args, contextBag) {
        var contentCommandName = args.get_commandName();
        var workflowOperation = args.get_commandArgument();
        var isCommandGoingToSave = false;
        var isCommandGoingToUnlock = false;

        if (workflowOperation) {
            var commandId;
            for (commandId = 0; commandId < this.get_workflowMenu().get_workflowVisualElements().length; commandId++) {
                if (this.get_workflowMenu().get_workflowVisualElements()[commandId].OperationName === workflowOperation) {
                    isCommandGoingToSave = !this.get_workflowMenu().get_workflowVisualElements()[commandId].RunAsUICommand;
                    break;
                }
            }
            for (commandId = 0; commandId < this.get_workflowMenu().get_workflowVisualElements().length; commandId++) {
                if (this.get_workflowMenu().get_workflowVisualElements()[commandId].OperationName === workflowOperation) {
                    isCommandGoingToUnlock = this.get_workflowMenu().get_workflowVisualElements()[commandId].ClosesForm;
                    break;
                }
            }
        }

        switch (workflowOperation) {
            case 'Schedule':
                if (!contextBag && args.get_contextBag) {
                    contextBag = args.get_contextBag();
                }

                this.saveDraft(workflowOperation, contextBag);
                return;
                break;
            case 'SendForApproval':
            case 'SendForReview':
            case 'SendForPublishing':
                if (!contextBag && args.get_contextBag) {
                    contextBag = args.get_contextBag();
                }

                if (contextBag && contextBag.length && contextBag.length != 0) {
                    this.saveDraft(workflowOperation, contextBag);
                    return;
                }
        }

        var queryString;
        switch (contentCommandName) {
            case 'preview':
                var previewUrl = this.get_pageUrl() + "/Action/Preview";
                if (this.get_currentLanguage()) {
                    previewUrl += "/" + this.get_currentLanguage();
                }
                queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                previewUrl += queryString.toString(true);
                window.open(previewUrl, "_blank");
                break;
            case 'mobile-preview':
                var mobilePreviewUrl = this.get_pageUrl() + "/Action/MobilePreview";
                if (this.get_currentLanguage()) {
                    mobilePreviewUrl += "/" + this.get_currentLanguage();
                }
                queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                mobilePreviewUrl += queryString.toString(true);
                window.open(mobilePreviewUrl, "_blank");
                break;
            case 'save':
                this.saveDraft(workflowOperation);
                break;
        }

        var zoneEditor = $find(this._zoneEditorId);
        if (zoneEditor && isCommandGoingToSave) {
            zoneEditor.set_isChangeMade(false);
            this._hasChangesInOtherVariation = false;
            if (isCommandGoingToUnlock) {
                zoneEditor.set_isUnlockingDone(true);
            }
        }
    },

    _takeOwnershipSuccess: function () {

    },

    _publishDraftSuccess: function (e) {
        this._markNoPendingChanges();

        //We don't want to raise this event as this is the final event 
        //before the user should be redirected which takes time.
        //this.raiseNonParallelRequestCompleted();
        window.location = this.get_cancelUrl();
    },

    _markNoPendingChanges: function () {
        var zoneEditor = $find(this._zoneEditorId);
        if (zoneEditor) {
            zoneEditor.set_isChangeMade(false);
            this._hasChangesInOtherVariation = false;
            zoneEditor.set_isUnlockingDone(true);
        }
    },

    _operationFailure: function (e) {
        this.raiseNonParallelRequestCompleted();
        alert(e.Detail);
    },

    _operationFailureQuiet: function (e) {
        //shhhh
    },

    _get_resourceString: function (key) {
        return this.get_clientLabelManager().getLabel("PageResources", key);
    },

    _saveDraftSuccess: function (e) {
        if (this._workflowMenu) {
            // TODO: add item as last arg
            this._workflowMenu.set_contentCulture(this._currentLanguage);
            this._workflowMenu.bindWorkflowVisuals(this.get_workflowItemType(), null, this.get_pageNodeId(), null);
        }
        else {
            this.get_messageControl().showPositiveMessage(this.get_clientLabelManager().getLabel("PageResources", "DraftSuccessfullySaved"));

            var status = this._status;
            if (status == this.STATUS_DRAFT || status == this.STATUS_INVISIBLE) {
                this.setCurrentStatus(this.STATUS_DRAFT);
            }
            else {
                //Update UI
                this.setCurrentStatus(this._wasPublished ? this.STATUS_DRAFT_NEWER : this.STATUS_DRAFT);
            }
        }

        var zoneEditor = $find(this._zoneEditorId);
        if (zoneEditor) {
            zoneEditor.set_isChangeMade(false);
            this._hasChangesInOtherVariation = false;
            //zoneEditor.set_isUnlockingDone(true);
        }
    },

    _unpublishPageSuccess: function (e) {
        // Show confirmation message
        var message = "";

        if (this.get_mediaType() == 0)
            message = this.get_clientLabelManager().getLabel("PageResources", "PageSuccessfullyUnpublished");
        else if (this.get_mediaType() == 2)
            message = this.get_clientLabelManager().getLabel("FormsResources", "FormSuccessfullyUnpublished");

        this.get_messageControl().showPositiveMessage(message);

        // Update UI
        this.setCurrentStatus(this.STATUS_UNPUBLISHED);
    },

    _getRadWindow: function () {
        var oWindow = null;
        if (window.radWindow) {
            oWindow = window.radWindow;
        } else if (window.frameElement.radWindow) {
            oWindow = window.frameElement.radWindow;
        }
        return oWindow;
    },

    /* -------------------- event firing  ---------------- */

    onCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('command');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    onDialogClosed: function (commandName, dataItem, itemsList, dialog, params, key) {
        var eventArgs = new Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs(commandName, dataItem, itemsList, dialog, params, key);
        var handler = this.get_events().getHandler('dialogClosed');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    /* -------------------- properties ---------------- */


    get_currentLanguage: function () {
        return this._currentLanguage;
    },

    set_currentLanguage: function (value) {
        this._currentLanguage = value;
    },

    get_languageToolbar: function () {
        return this._languageToolbar;
    },

    set_languageToolbar: function (value) {
        this._languageToolbar = value;
        if (value) {
            value.set_editorToolbar(this);
        }
    },

    get_pageUrl: function () {
        return this._pageUrl;
    },
    set_pageUrl: function (value) {
        this._pageUrl = value;
    },

    get_dataItem: function () {
        return this._dataItem;
    },
    set_dataItem: function (value) {
        this._dataItem = value;
    },

    get_hasStatus: function () {
        return this._hasStatus;
    },

    setv: function (value) {
        this._hasStatus = value;
    },

    get_mainToolbar: function () {
        return this._mainToolbar;
    },
    set_mainToolbar: function (value) {
        this._mainToolbar = value;
    },

    get_workflowMenu: function () {
        return this._workflowMenu;
    },
    set_workflowMenu: function (value) {
        this._workflowMenu = value;
    },

    get_workflowStatusField: function () {
        return this._workflowStatusField;
    },
    set_workflowStatusField: function (value) {
        this._workflowStatusField = value;
    },

    get_warningsField: function () {
        return this._warningsField;
    },
    set_warningsField: function (value) {
        this._warningsField = value;
    },

    get_warnings: function () {
        return this._warnings;
    },
    set_warnings: function (value) {
        this._warnings = value;
    },

    get_leftToolbar: function () {
        return this._leftToolbar;
    },

    set_leftToolbar: function (value) {
        this._leftToolbar = value;
    },

    get_parentItemId: function () {
        return this._parentItemId;
    },

    set_parentItemId: function (value) {
        if (this._parentItemId != value)
            this._parentItemId = value;
    },

    get_personalizationMasterId: function () {
        return this._personalizationMasterId;
    },

    set_personalizationMasterId: function (value) {
        this._personalizationMasterId = value;
    },

    get_pageTitle: function () {
        return this._pageTitle;
    },

    get_isEditable: function () {
        return this._isEditable;
    },

    get_isContentEditable: function () {
        return this._isContentEditable;
    },

    get_localizationStrategy: function () {
        return this._localizationStrategy;
    },

    set_pageTitle: function (value) {
        if (this._pageTitle != value)
            this._pageTitle = value;
    },

    get_clientManager: function () {
        if (this._clientManager == null)
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();

        if (this._clientManager.get_culture() == null)
            this._clientManager.set_culture(this._currentLanguage);

        if (this._clientManager.get_uiCulture() == null)
            this._clientManager.set_uiCulture(this._currentLanguage);

        return this._clientManager;
    },

    get_cancelUrl: function () {
        return this._cancelUrl;
    },

    set_cancelUrl: function (value) {
        if (this._cancelUrl != value) {
            this._cancelUrl = value;
            this.raisePropertyChanged('cancelUrl');
        }
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },

    set_serviceUrl: function (value) {
        if (this._serviceUrl != value) {
            this._serviceUrl = value;
            this.raisePropertyChanged('serviceUrl');
        }
    },

    get_draftId: function () {
        return this._draftId;
    },

    set_draftId: function (value) {
        if (this._draftId != value) {
            this._draftId = value;
            this.raisePropertyChanged('draftId');
        }
    },

    get_pageNodeId: function () {
        return this._pageNodeId;
    },

    set_pageNodeId: function (value) {
        this._pageNodeId = value;
    },

    get_missingThemeName: function () {
        return this._missingThemeName;
    },

    set_missingThemeName: function (value) {
        this._missingThemeName = value;
    },

    get_mediaType: function () {
        return this._mediaType;
    },

    set_mediaType: function (value) {
        if (this._mediaType != value) {
            this._mediaType = value;
            this.raisePropertyChanged('mediaType');
        }
    },

    get_messageControl: function () {
        return this._messageControl;
    },

    set_messageControl: function (value) {
        if (this._messageControl != value) {
            this._messageControl = value;
            this.raisePropertyChanged('messageControl');
        }
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        if (this._clientLabelManager != value) {
            this._clientLabelManager = value;
            this.raisePropertyChanged('clientLabelManager');
        }
    },

    get_windowManager: function () {
        return this._windowManager;
    },

    set_windowManager: function (value) {
        if (this._windowManager != value) {
            this._windowManager = value;
            this.raisePropertyChanged('windowManager');
        }
    },

    get_baseItemServiceUrl: function () {
        return this._baseItemServiceUrl;
    },

    set_baseItemServiceUrl: function (value) {
        this._baseItemServiceUrl = value;
    },

    get_toolbarLoadingPanel: function () {
        return this._toolbarLoadingPanel;
    },

    set_toolbarLoadingPanel: function (value) {
        this._toolbarLoadingPanel = value;
    },

    get_toolbarWrapper: function () {
        return this._toolbarWrapper;
    },

    set_toolbarWrapper: function (value) {
        this._toolbarWrapper = value;
    },

    get_personalizationSelector: function () {
        return this._personalizationSelector;
    },

    set_personalizationSelector: function (value) {
        this._personalizationSelector = value;
    },

    get_editorToolbarSelectors: function () {
        return this._editorToolbarSelectors;
    },

    set_editorToolbarSelectors: function (value) {
        this._editorToolbarSelectors = value;
    },

    get_workflowItemId: function () {
        return this._workflowItemId;
    },
    set_workflowItemId: function (value) {
        this._workflowItemId = value;
    },
    get_workflowItemState: function () {
        return this._workflowItemState;
    },
    set_workflowItemState: function (value) {
        this._workflowItemState = value;
    },
    get_workflowItemType: function () {
        return this._workflowItemType;
    },
    set_workflowItemType: function (value) {
        this._workflowItemType = value;
    },
    get_cannotModifyPageDialogId: function () {
        return this._cannotModifyPageDialogId;
    },
    set_cannotModifyPageDialogId: function (value) {
        this._cannotModifyPageDialogId = value;
    }
};

Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.EditorToolBar', Sys.Component);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs = function (commandName, commandArgument) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.initializeBase(this);
    this._commandName = commandName;
    this._commandArgument = commandArgument;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.callBaseMethod(this, 'dispose');
    },

    get_commandName: function () {
        return this._commandName;
    },

    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs', Sys.CancelEventArgs);

// ------------------------------------------------------------------------
// Dialog Event Args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs =
function (
    commandName,
    dataItem,
    itemsList,
    dialog,
    params,
    key,
    commandArgument) {
    this._commandName = commandName;
    this._dataItem = dataItem;
    this._itemsList = itemsList;
    this._dialog = dialog;
    this._params = params;
    this._key = key;
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.CommandEventArgs.callBaseMethod(this, 'dispose');
    },

    get_commandName: function () {
        return this._commandName;
    },

    get_dataItem: function () {
        return this._dataItem;
    },

    get_itemsList: function () {
        return this._itemsList;
    },

    get_params: function () {
        return this._params;
    },

    get_key: function () {
        return this._key;
    },

    get_dialog: function () {
        return this._dialog;
    },

    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.DialogEventArgs', Sys.CancelEventArgs);
