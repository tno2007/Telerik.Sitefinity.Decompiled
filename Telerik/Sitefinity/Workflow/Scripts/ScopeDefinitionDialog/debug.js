var scopeDefinitionDialog = null;

window.createDialog = function (context) {
    if (scopeDefinitionDialog) {
        scopeDefinitionDialog.createDialog(context);
    }
};

Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog.initializeBase(this, [element]);

    this._selectors = {
        contentTypeSelectorWrapper: "#contentTypeSelectorWrapper",
        langSelectorWrapper: "#langSelectorWrapper",
        siteSelectorWrapper: "#siteSelectorWrapper",
        contentPageActionContainer: ".contentPageActionContainer",
        applyToAllContentTypesWrapper: "#applyToAllContentTypesWrapper"
    };

    this._constants = {
        ALL_SITES: "ALL_SITES",
        ALL_LANGUAGES: "ALL_LANGUAGES"
    };

    this._dialogManager = null;
    this._langScopeValues = null;
    this._siteScopeValues = null;
    this._itemSelector = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._labelManager = null;
    this._errorMessageWrapper = null;
    this._errorMessageLabel = null;
    this._applyToAllContentTypes = null;
    this._workflowServiceUrl = null;
    this._multisiteServiceUrl = null;

    this._isMultisiteMode = null;
    this._isMultilingual = null;
    this._currentSite = null;

    this.isNew = false;
    this._isPopulated = false;
    this._sites = [];
    this._siteIds = [];
    this._languages = null;
    this._scope = null;
    this._scopes = null;
    this._selectedKeys = null;
    this._contentTypesKeys = [];
    this._registeredHandlers = [];

    this._getAllSitesDelegate = null;
    this._getAllLanguagesDelegate = null;
    this._doneClickDelegate = null;
    this._closeButtonDelegate = null;
    this._changeSitesDelegate = null;
    this._changeLanguageDelegate = null;
    this._binderDataBoundDelegate = null;
}
Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog.callBaseMethod(this, "initialize");

        scopeDefinitionDialog = this;
        jQuery("body").addClass("sfSelectorDialog");

        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBoundHandler);
        this._getAllLanguagesDelegate = Function.createDelegate(this, this._getAllLanguagesHandler);
        this._getAllSitesDelegate = Function.createDelegate(this, this._getAllSitesHandler);
        
        this._onPageLoadDelegate = Function.createDelegate(this, this._onPageLoadHandler);
        Sys.Application.add_load(this._onPageLoadDelegate);

        this._initializeEventHandlers();
    },

    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog.callBaseMethod(this, "dispose");

        if (this._binderDataBoundDelegate)
            delete this._binderDataBoundDelegate;

        this._registeredHandlers.forEach(function (handler) {
            $removeHandler(handler.element, handler.event, handler.delegate);
        });
    },

    /* **************** private methods **************** */

    _addHandler: function (element, handler, event) {
        var delegate = Function.createDelegate(this, handler);
        $addHandler(element, event, delegate);

        this._registeredHandlers.push({
            element: element,
            event: event,
            delegate: delegate
        });
    },

    /* **************** public methods **************** */

    createDialog: function (context) {
        this._reset();
        this._scopes = context.wfScopes;
        this._scope = context.wfScope;
        this.isNew = context.mode === "create";

        if (this.get_isMultisiteMode()) {
            this._getSites();
        } else {
            this._sites.push(this.get_currentSite());
        }

        if (this.get_isMultilingual()) {
            this._getLanguages(null);
        }

        this.get_itemSelector().bindSelector();
    },

    dropDownSelectOption: function (dropDown, value) {
        if (!value) return;

        for (var i = 0; i < dropDown.options.length; i++) {
            if (dropDown.options[i].value == value)
                dropDown.options[i].selected = true;
        }
    },

    dropDownAppendOptions: function (dropDown, value, text) {
        jQuery(dropDown).append($("<option></option>").attr("value", value).text(text));
    },

    /* **************** event handlers **************** */

    _initializeEventHandlers: function () {
        this._addHandler(this.get_doneButton(), this._doneClickHandler, "click");
        this._addHandler(this.get_cancelButton(), this._closeButtonClickHandler, "click");
        this._addHandler(this.get_siteScopeValues(), this._changeSitesHandler, "change");
    },

    _onPageLoadHandler: function () {
        this.get_itemSelector().add_binderDataBound(this._binderDataBoundDelegate);

        this._onRowSelectedDelegate = Function.createDelegate(this, this._onRowSelectedHandler);
        this._itemSelector._grid.add_rowSelected(this._onRowSelectedDelegate);

        this._onRowDeselectedDelegate = Function.createDelegate(this, this._onRowDeselectedHandler);
        this._itemSelector._grid.add_rowDeselected(this._onRowDeselectedDelegate);

        this.get_dialogManager().addHandler(this.get_pageSelectorDialog(), "close", this._onPageSelectorDialogClose.bind(this));
    },

    _doneClickHandler: function (sender, args) {
        var _currentScope = Telerik.Sitefinity.cloneObject(this._scope);

        var siteId = this.get_siteScopeValues().value;
        if (!this.get_isMultisiteMode() || siteId == this._constants.ALL_SITES) {
            _currentScope.Site = null;
        } else {
            var site = Telerik.Sitefinity.find(this._sites, function (site) {
                return site.Id === siteId;
            });

            _currentScope.Site = {
                SiteId: siteId,
                SiteMapRootNodeId: site.SiteMapRootNodeId,
                Name: site.Name
            };
        }

        var culture = this.get_langScopeValues().value;
        if (!this.get_isMultilingual() || culture == this._constants.ALL_LANGUAGES) {
            _currentScope.Language = [];
        } else {
            var language = Telerik.Sitefinity.find(this._languages, function(lang){
                return lang.Culture === culture;
            });

            _currentScope.Language = [language];
        }

        var contentTypes = this.get_itemSelector().get_selectedItems();
        if (contentTypes.length > 0) {
            var applyToAll = this.get_applyToAllContentTypes().checked;
            if (applyToAll) {
                contentTypes = [];
            }

            _currentScope.TypeScopes = contentTypes;
        } else {
            this.get_applyToAllContentTypesWrapper().hide();
            var errMsg = this.get_labelManager().getLabel('WorkflowResources', 'WorkflowSelectAtLeastOneType');
            this._showError(errMsg);
            return;
        }
        var hasBeenAdded = $workflow.hasBeenAdded(_currentScope, this._scopes);
        if (this.isNew && hasBeenAdded)
        {
            var errMsg = this.get_labelManager().getLabel('WorkflowResources', 'ScopeIsAdded');
            this._showError(errMsg);
            return;
        }

        this.close({
            scope: _currentScope,
            isNew: this.isNew
        });
    },

    _closeButtonClickHandler: function () {
        this.close();
    },

    _setVisibility: function (selector, visibility) {
        selector = jQuery(selector);
        selector.show();

        if (visibility) {
            selector.css("visibility", "visible");
        } else {
            selector.css("visibility", "hidden");
        }
    },

    _reset: function () {
        if (this.get_isMultilingual())
            this.get_langSelector().show();
        else
            this.get_langSelector().hide();
        
        if (this.get_isMultisiteMode())
            this.get_siteSelector().show();
        else
            this.get_siteSelector().hide();

        this._setVisibility(this.get_applyToAllContentTypesWrapper(), false);
        this._hideError();
    },

    _populateSitesSelector: function (dropDown, value, text) {
        var _this = this;
        jQuery.each(this._sites, function (key, val) {
            _this._siteIds.push(val.Id);
        });

        jQuery(dropDown).empty();
        this.dropDownAppendOptions(dropDown, this._constants.ALL_SITES, this.get_labelManager().getLabel("MultisiteResources", "SitesAll"));

        jQuery.each(this._sites, function (key, val) {
            jQuery(dropDown).append($("<option />").val(val[value]).text(val[text]))
        });

        if (this.isNew) return;

        var selectAll = this._scope.Site == null;

        if (selectAll) {
            this.dropDownSelectOption(dropDown, this._constants.ALL_SITES);
        }
        else {
            this.dropDownSelectOption(dropDown, this._scope.Site.SiteId);
            this._siteIds = [this._scope.Site.SiteId];
        }

        if (this.get_isMultilingual()) {
            this._getLanguages(this._siteIds);
        }
    },

    _populateLangSelector: function (dropDown, value, text) {
        jQuery(dropDown).empty();
        this.dropDownAppendOptions(dropDown, this._constants.ALL_LANGUAGES, this.get_labelManager().getLabel("Labels", "AllLanguages"));

        jQuery.each(this._languages, function (key, val) {
            jQuery(dropDown).append($("<option />").val(val[value]).text(val[text]))
        });

        if (this.isNew) return;

        var selectAll = !this._scope.Language || this._scope.Language.length == 0;

        if (selectAll)
            this.dropDownSelectOption(dropDown, this._constants.ALL_LANGUAGES, selectAll)
        else
            this.dropDownSelectOption(dropDown, this._scope.Language[0].Culture, selectAll)
    },

    _populateContentSelector: function () {
        if (this.isNew) return;

        if (this._selectContentItems()) {
            this.get_applyToAllContentTypes().checked = false;
            this._setVisibility(this.get_applyToAllContentTypesWrapper(), true);
        }

        var pageNodeItem = this._getPageNodeItem();
        if (pageNodeItem) {
            var pagesScope = null;

            if (this._isPopulated) {
                pagesScope = this._getPageNodeItem();
            } else {
                pagesScope = this._getPageNodeItem(this._scope.TypeScopes);
            }

            if (pagesScope) {
                pageNodeItem.ContentFilter = pagesScope.ContentFilter;
                pageNodeItem.IncludeChildren = pagesScope.IncludeChildren;
                this._showPageSelectorActionContainer();

                this._isPopulated = true;
            }
        }
    },

    _selectContentItems: function () {
        var items = this.get_itemSelector().getDataItems();
        var scopes = this.get_contentTypesKeys();
        if (!scopes.length) {
            var i = items.length;
            while (i--) {
                var item = items[i];
                item.set_selected(true);
            }

            this.get_applyToAllContentTypes().checked = true;
            this._setVisibility(this.get_applyToAllContentTypesWrapper(), true);
        }
        else {
            this.get_itemSelector().selectItemsInternal(scopes);
        }

        return scopes.length == items.length;
    },

    _getPageNodeItem: function (selectedItems) {
        selectedItems = selectedItems || this.get_itemSelector().get_selectedItems();

        return $workflow.getPageNodeItem(selectedItems);
    },

    _binderDataBoundHandler: function (sender, args) {
        this._populateContentSelector();
        var allChk = this.get_ItemSelectorAllChk();
        if (allChk)
            this._addHandler(allChk, this._onAllItemsCheck, "click");
    },

    _onAllItemsCheck: function () {
        this._hideError();
        var isChecked = this.get_ItemSelectorAllChk().checked;
        if (isChecked) {
            this.get_applyToAllContentTypesWrapper().show();
            this.resizeToContent();

            this.get_applyToAllContentTypes().checked = true;
            this._setVisibility(this.get_applyToAllContentTypesWrapper(), true);
        }
        else {
            this.get_applyToAllContentTypes().checked = false;
            this._setVisibility(this.get_applyToAllContentTypesWrapper(), false);
        }
    },
    
    _getLanguages: function (siteIds) {
        jQuery.ajax({
            type: "POST",
            url: this.get_workflowServiceUrl() + "/lang-scope/",
            data: JSON.stringify(siteIds),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: this._getAllLanguagesDelegate,
            error: function (e) {
                console.log(e);
            }
        });
    },

    _getSites: function () {
        jQuery.ajax({
            type: "GET",
            url: this.get_multisiteServiceUrl(),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: this._getAllSitesDelegate,
            error: function (e) {
                console.log(e);
            }
        });
    },

    _getAllLanguagesHandler: function (sender, args) {
        this._languages = sender.Items;
        this._populateLangSelector(this.get_langScopeValues(), "Culture", "DisplayName");
    },

    _getAllSitesHandler: function (sender, args) {
        this._sites = sender.Items;
        this._populateSitesSelector(this.get_siteScopeValues(), "Id", "Name");
    },

    _changeSitesHandler: function (sender, args) {
        this._resetPageNodeItemContext();

        var siteId = this.get_siteScopeValues().value;
        if (siteId === this._constants.ALL_SITES) {
            this._hidePageSelectorActionContainer();
            this._siteIds = null;
        } else {
            this._showPageSelectorActionContainer();
            this._siteIds = [siteId];
        }

        if (this.get_isMultilingual()) {
            this._getLanguages(this._siteIds);
        }            
    },

    _resetPageNodeItemContext: function (dataItem) {
        var pageNode = dataItem || this._getPageNodeItem();

        if (pageNode) {
            pageNode.ContentFilter = [];
            pageNode.IncludeChildren = true;
        }
    },

    _onRowSelectedHandler: function (sender, args) {
        var dataItem = args.get_gridDataItem().get_dataItem();

        if (dataItem.ContentType === $workflow.PAGE_NODE) {
            this._showPageSelectorActionContainer();
        }

        var selectedItems = this.get_itemSelector().get_selectedItems();
        var selectorItems = this.get_itemSelector().getDataItems();
        if (selectedItems.length == selectorItems.length)
            this._setVisibility(this.get_applyToAllContentTypesWrapper(), true);
        else
            this._setVisibility(this.get_applyToAllContentTypesWrapper(), false);
        
        this._hideError();
    },

    _onRowDeselectedHandler: function (sender, args) {
        var dataItem = args.get_gridDataItem().get_dataItem();

        if (dataItem.ContentType === $workflow.PAGE_NODE) {
            this._hidePageSelectorActionContainer();
            this._resetPageNodeItemContext(dataItem);
        }

        this._setVisibility(this.get_applyToAllContentTypesWrapper(), false);
        this.get_applyToAllContentTypes().checked = false;
        this._hideError();
    },

    _onPageSelectorBtnClick: function () {
        var self = this;

        var selectedSiteDefinition = Telerik.Sitefinity.find(this._sites, function (site) {
            return site.Id === self.get_siteScopeValues().value;
        }) || this._sites[0];

        var siteContext = {
            id: selectedSiteDefinition.Id,
            name: selectedSiteDefinition.Name,
            rootNodeId: selectedSiteDefinition.SiteMapRootNodeId
        };

        var pageNodeItem = this._getPageNodeItem();
        var cultureContext = this.get_selectedLanguage();

        var pageSelectorDialog = this.get_pageSelectorDialog();
        var dialogArgs = [pageNodeItem.ContentFilter, pageNodeItem.IncludeChildren, siteContext, cultureContext];

        this.get_dialogManager().openDialog(pageSelectorDialog, dialogArgs);
    },

    _onPageSelectorDialogClose: function (sender, args) {
        if (!args) return;

        var pageNodeItem = this._getPageNodeItem();

        pageNodeItem.ContentFilter = args.selectedPagesIds;
        pageNodeItem.IncludeChildren = args.applyToChildPages;

        this._showPageSelectorActionContainer();
    },

    _showPageSelectorActionContainer: function () {
        if (this._siteScopeValues.value === this._constants.ALL_SITES) return;

        var actionContainer = $(this._selectors.contentPageActionContainer);
        if (!actionContainer) return;

        var pageNodeItem = this._getPageNodeItem();
        if (!pageNodeItem) return;

        actionContainer.empty();

        var additionalLabel = $("<span/>").addClass("text-capitalize");
        var pageSelectorBtn = $("<a/>").addClass("sfMLeft10 sfMRight5");

        var onPageSelectorBtnClickDelegate = Function.createDelegate(this, this._onPageSelectorBtnClick);
        pageSelectorBtn.on("click", onPageSelectorBtnClickDelegate);

        var labelManager = this.get_labelManager();

        if (pageNodeItem.ContentFilter.length) {
            var pageLabel = labelManager.getLabel("PageResources", "Page");
            var pagesLabel = labelManager.getLabel("PageResources", "Pages");

            var pageSelectorBtnSuffix = pageNodeItem.ContentFilter.length === 1 ? pageLabel : pagesLabel;
            pageSelectorBtn.text(pageNodeItem.ContentFilter.length + " " + pageSelectorBtnSuffix);
            pageSelectorBtn.addClass("sfTextLowercase");

            actionContainer.append(pageSelectorBtn);

            if (pageNodeItem.IncludeChildren) {
                var singlePageChildPagesLabel = labelManager.getLabel("PageResources", "AndItsChildPages");
                var multiplePagesChildPagesLabel = labelManager.getLabel("PageResources", "AndTheirChildPages");
                var childPagesLabel = pageNodeItem.ContentFilter.length === 1 ? singlePageChildPagesLabel : multiplePagesChildPagesLabel;

                additionalLabel.text(childPagesLabel);
                additionalLabel.addClass("sfTextLowercase sfLightTxt sfSmallestTxt");

                actionContainer.append(additionalLabel);
            }
        } else {
            var allPagesLabel = labelManager.getLabel("PageResources", "AllPages");
            var changeLabel = labelManager.getLabel("Labels", "Change");

            additionalLabel.text(allPagesLabel);
            additionalLabel.addClass("text-capitalize");

            pageSelectorBtn.text(changeLabel);

            actionContainer.append(additionalLabel).append(pageSelectorBtn);
        }

        actionContainer.show();
    },

    _hidePageSelectorActionContainer: function () {
        var actionContainer = $(this._selectors.contentPageActionContainer);

        if (!actionContainer) return;

        actionContainer.empty();
        actionContainer.hide();
    },

    _showError: function (error) {
        $(this.get_errorMessageWrapper()).show();
        $(this.get_errorMessageLabel()).text(error);
        this.resizeToContent();
    },

    _hideError: function () {
        $(this.get_errorMessageWrapper()).hide();
        $(this.get_errorMessageLabel()).text("");
        this.resizeToContent();
    },

    get_contentTypesKeys: function () {
        var _this = this;

        var contentTypes = !this._scope ? null : this._scope.TypeScopes;
        if (!contentTypes) return [];

        this._contentTypesKeys = [];
        jQuery.each(contentTypes, function (key, val) {
            _this._contentTypesKeys.push(val.ContentType);
        });

        return this._contentTypesKeys;
    },

    get_selectedLanguage: function () {
        var langScopeValue = this.get_langScopeValues().value;

        if (langScopeValue === this._constants.ALL_LANGUAGES) {
            return null;
        }

        return langScopeValue;
    },

    get_pageSelectorDialog: function () {
        return this.get_dialogManager().getDialog("scopePageSelectorDialog");
    },

    get_ItemSelectorAllChk: function () {
        return this.get_itemSelector().getAllItemsCheckbox();
    },

    get_dialogManager: function () {
        return this._dialogManager;
    },
    set_dialogManager: function (value) {
        this._dialogManager = value;
    },

    get_siteScopeValues: function () {
        return this._siteScopeValues;
    },
    set_siteScopeValues: function (value) {
        this._siteScopeValues = value;
    },

    get_langScopeValues: function () {
        return this._langScopeValues;
    },
    set_langScopeValues: function (value) {
        this._langScopeValues = value;
    },

    get_itemSelector: function () {
        return this._itemSelector;
    },
    set_itemSelector: function (value) {
        this._itemSelector = value;
    },

    get_selectors: function () {
        return this._selectors;
    },

    get_contentTypeSelector: function () {
        return jQuery(this.get_selectors().contentTypeSelectorWrapper);
    },

    get_applyToAllContentTypesWrapper: function () {
        return jQuery(this.get_selectors().applyToAllContentTypesWrapper);
    },

    get_langSelector: function () {
        return jQuery(this.get_selectors().langSelectorWrapper);
    },

    get_siteSelector: function () {
        return jQuery(this.get_selectors().siteSelectorWrapper);
    },

    get_workflowServiceUrl: function () {
        return this._workflowServiceUrl;
    },
    set_workflowServiceUrl: function (value) {
        this._workflowServiceUrl = value;
    },

    get_multisiteServiceUrl: function () {
        return this._multisiteServiceUrl;
    },
    set_multisiteServiceUrl: function (value) {
        this._multisiteServiceUrl = value;
    },

    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },

    get_errorMessageLabel: function () {
        return this._errorMessageLabel;
    },
    set_errorMessageLabel: function (value) {
        this._errorMessageLabel = value;
    },

    get_errorMessageWrapper: function () {
        return this._errorMessageWrapper;
    },
    set_errorMessageWrapper: function (value) {
        this._errorMessageWrapper = value;
    },

    get_applyToAllContentTypes: function () {
        return this._applyToAllContentTypes;
    },
    set_applyToAllContentTypes: function (value) {
        this._applyToAllContentTypes = value;
    },

    get_selectedKeys: function () {
        return this._itemSelector.get_selectedKeys();
    },

    get_isMultisiteMode: function () {
        return this._isMultisiteMode;
    },
    set_isMultisiteMode: function (value) {
        this._isMultisiteMode = value;
    },

    get_isMultilingual: function () {
        return this._isMultilingual;
    },
    set_isMultilingual: function (value) {
        this._isMultilingual = value;
    },

    get_currentSite: function () {
        return this._currentSite;
    },
    set_currentSite: function (value) {
        this._currentSite = value;
    },
}

Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog.registerClass('Telerik.Sitefinity.Workflow.UI.ScopeDefinitionDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);