/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.initializeBase(this, [element]);

    this._relatedDataService = null;
    this._dataBindSuccessDelegate = null;
    this._dataBindErrorDelegate = null;
    this._actionExecutedDelegate = null;
    this._resetDelegate = null;
    this._updatedAssets = [];
    this._parentItemId = null;
    this._dataItemType = null;
    this._providerName = null;
    this._propertyName = null;
    this._status = null;
    this._step = 8;
    this._skip = 0;
    this._take = this._step;
    this._totalCount = 0;
    this._isUpdate = false;
    this._isMultipleMode = false;
    this._initializeControl = true;
    this._relatedDataProvider = null;
    this._childItemType = null;
    this._isDuplicateMode = null;
}

Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.prototype =
{
    initialize: function () {
        if (!this._initializeControl) {
            return false;
        }

        Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "initialize");

        this._dataBindSuccessDelegate = Function.createDelegate(this, this._dataBindSuccessHandler);
        this._dataBindErrorDelegate = Function.createDelegate(this, this._dataBindErrorHandler);

        this._actionExecutedDelegate = Function.createDelegate(this, this._actionExecutedHandler);
        this.add_actionExecuted(this._actionExecutedDelegate);

        this._resetDelegate = Function.createDelegate(this, this.reset);

        switch (this._assetsWorkMode) {
            case this.SingleImage:
            case this.MultipleImages:
                this._childItemType = 'Telerik.Sitefinity.Libraries.Model.Image';
                break;
            case this.SingleDocument:
            case this.MultipleDocuments:
                this._childItemType = 'Telerik.Sitefinity.Libraries.Model.Document';
                break;
            case this.SingleVideo:
            case this.MultipleVideos:
                this._childItemType = 'Telerik.Sitefinity.Libraries.Model.Video';
                break;
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "dispose");

        if (this._dataBindSuccessDelegate) {
            delete this._dataBindSuccessDelegate;
        }
        if (this._dataBindErrorDelegate) {
            delete this._dataBindErrorDelegate;
        }
        if (this._actionExecutedDelegate) {
            this.remove_actionExecuted(this._actionExecutedDelegate);
            delete this._actionExecutedDelegate;
        }
        if (this._resetDelegate) {
            delete this._resetDelegate;
        }
        this._updatedAssets = [];
        delete this._updatedAssets;
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "reset");
        this._updatedAssets = [];
        this._skip = 0;
        this._take = this._step;
        this._isDuplicateMode = false;
    },

    dataBind: function (dataItem, dataItemType, providerName, propertyName, setDefault, duplicationData) {
        if (!this._initializeControl) {
            return false;
        }

        this.reset();

        //Blogs does not support ML
        if (dataItemType === 'Telerik.Sitefinity.Blogs.Model.Blog') {
            this.set_isMultilingual(false);
        }

        if (dataItem.IsDuplicate) {
            this._isDuplicateMode = true;

            var that = this;

            if (duplicationData.relatedDataLinks) {
                var relatedData = $.grep(duplicationData.relatedDataLinks, function (element, index) {
                    return element.ComponentPropertyName === that._fieldName;
                });

                this._updatedAssets = relatedData;
            }
            else {
                this._updatedAssets = [];
            }
        }

        // default value for item is null, there is no need to load them for blank item
        if (typeof setDefault === "undefined" || (typeof setDefault !== "undefined" && !setDefault)) {
            var parentItemId;

            //If the related data has been duplicated, the dataItem won't has Id, so we will use the provided parentItemId
            if (duplicationData.parentItemId && duplicationData.parentItemId !== Telerik.Sitefinity.getEmptyGuid()) {
                parentItemId = duplicationData.parentItemId;
            }

            if (dataItem.Id && dataItem.Id !== Telerik.Sitefinity.getEmptyGuid()) {
                parentItemId = dataItem.Id;
                var status = null;
                if (dataItem.hasOwnProperty('Status') && dataItem.hasOwnProperty('OriginalContentId')) {
                    status = dataItem.Status
                    if (dataItem.Status !== 0) {
                        parentItemId = dataItem.OriginalContentId;
                    }
                }
            }

            if (parentItemId && parentItemId !== Telerik.Sitefinity.getEmptyGuid()) {
                this._parentItemId = parentItemId;
                this._dataItemType = dataItemType;
                this._providerName = providerName;
                this._propertyName = propertyName;
                this._status = status;

                this._loadItems();
            }
        }
    },

    _loadItems: function () {
        var uiCulture = this.get_uiCulture();
        var params = {
            ParentItemId: this._parentItemId,
            ParentItemType: this._dataItemType,
            ParentProviderName: this._providerName,
            FieldName: this._propertyName,
            skip: this._skip,
            take: this._take,
            Status: this._status,
            ChildItemType: this._childItemType,
            ChildProviderName: this._relatedDataProvider
        };
        $.ajax({
            type: 'GET',
            url: this._relatedDataService + "/child-items?" + $.param(params),
            contentType: "application/json",
            cache: false,
            success: this._dataBindSuccessDelegate,
            error: this._dataBindErrorDelegate,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("IsBackendRequest", true);
                if (uiCulture) {
                    xhr.setRequestHeader("SF_UI_CULTURE", uiCulture);
                }
            }
        });
    },

    _dataBindSuccessHandler: function (data, status, xhr) {
        var that = this,
            items = data.Items;

        this._totalCount = data.TotalCount;

        if (items) {
            this._bindFields(items);
        }

        if (!this._isUpdate && this._isMultipleMode && this._totalCount > this._skip + this._take) {
            this._isUpdate = true;
            $(this.thisElement.find('ul.ui-sortable')).after($('<div id="btnShowMoreWrp" class="sfMBottom15 sfSmallerTxt"><a id="btnShowMore">Show more</a></div>'));
            $(this.thisElement.find('#btnShowMore')).on('click', function () {
                that._showMore();
            });
        }
        else if (this._totalCount <= this._skip + this._take) {
            $(this.thisElement.find('#btnShowMoreWrp')).hide();
        }
        else {
            $(this.thisElement.find('#btnShowMoreWrp')).show();
        }
    },

    _showMore: function () {
        if (this._totalCount > this._skip + this._take) {
            this._skip += this._step;
            this._loadItems();
        }
    },

    _bindFields: function (items) {
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            var isTranslated = this.isDataItemTranslatedForCurrentCulture(item);

            switch (this._assetsWorkMode) {
                case this.SingleImage:
                    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "addListElement", [item.Id, "sfAsset sfInlineBlock"]);                   
                    this.addImage(item.Id, item.ThumbnailUrl, item.MediaUrl, true, isTranslated);
                    break;
                case this.MultipleImages:
                    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "addListElement", [item.Id, "sfAsset sfInlineBlock"]);
                    this.addImage(item.Id, item.ThumbnailUrl, item.MediaUrl, false, isTranslated);
                    this._isMultipleMode = true;
                    break;
                case this.SingleDocument:
                    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "addListElement", [item.Id, "sfAsset"]);
                    this.addDocument(item.Id, item.Title, item.Extension, Math.round(item.TotalSize / 1024), true, isTranslated);
                    break;
                case this.MultipleDocuments:
                    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "addListElement", [item.Id, "sfAsset"]);
                    this.addDocument(item.Id, item.Title, item.Extension, Math.round(item.TotalSize / 1024), false, isTranslated);
                    this._isMultipleMode = true;
                    break;
                case this.SingleVideo:
                    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "addListElement", [item.Id, "sfAsset sfInlineBlock"]);
                    this.addVideo(item.Id, item.ThumbnailUrl, true, isTranslated, item.Title);
                    break;
                case this.MultipleVideos:
                    Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "addListElement", [item.Id, "sfAsset sfInlineBlock"]);
                    this.addVideo(item.Id, item.ThumbnailUrl, false, isTranslated, item.Title);
                    this._isMultipleMode = true;
                    break;
            }

            var itemWorkflowStatusCssClass = "sfStatus sf" + item.LifecycleStatus.WorkflowStatus.toLowerCase();
            this.thisElement.find("#asset" + item.Id).prepend("<span class='" + itemWorkflowStatusCssClass + "'></span>");

            var selectedAsset = {
                "ChildItemId": item.Id,
                "ChildItemType": this._childItemType,
                "ChildItemProviderName": item.ProviderName,
                "Ordinal": item.Ordinal,
                "ContainerId": item.Id
            };
            this._selectedAssets.push(selectedAsset);
        }
    },

    _dataBindErrorHandler: function (xhr, status, error) {
        alert(xhr.responseText);
    },

    _selectorInsertHandler: function (selectedItem) {
        var containerId = selectedItem.Id;

        //Don't allow adding same image twice
        if (this.thisElement.find("#asset" + containerId).length > 0) {
            return;
        }

        Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "_selectorInsertHandler", [selectedItem, containerId])

        var itemWorkflowStatusCssClass = "sfStatus sf" + selectedItem.Status.toLowerCase();
        this.thisElement.find("#asset" + containerId).prepend("<span class='" + itemWorkflowStatusCssClass + "'></span>");
    },

    addListElement: function (id, cssClass) {
        var imageMarkup = '<li id="asset' + id + '" class="' + cssClass + '"></li>';
        $(this.get_selectedAssetsList()).prepend(imageMarkup);
    },

    getOrdinal: function () {
        //Get the minimum Ordinal in collection. In case there are no other items
        //the ordinal of the item will be set to 0
        var minOrdinal = -1;
        if (this._selectedAssets.length > 0) {
            minOrdinal = this._selectedAssets[0].Ordinal;
            for (var i = 1, len = this._selectedAssets.length; i < len; i++) {
                if (this._selectedAssets[i].Ordinal < minOrdinal) {
                    minOrdinal = this._selectedAssets[i].Ordinal;
                }
            }
        }

        return minOrdinal - 1;
    },

    getChildItemId: function (item) {
        var id = item.Id;
        if (item.OriginalContentId && item.OriginalContentId !== Telerik.Sitefinity.getEmptyGuid()) {
            id = item.OriginalContentId;
        }
        return id;
    },

    _actionExecutedHandler: function (sender, args) {
        switch (args.Name) {
            case "Add":
                //if we have single mode and another asset is selected, remove the old one
                if (args.SingleModeRemovedAsset) {
                    this._addUpdatedAsset(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Removed, args.SingleModeRemovedAsset);
                }
                if (args.Asset) {
                    this._addUpdatedAsset(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Added, args.Asset);
                }
                break;
            case "Remove":
                if (args.Asset) {
                    this._addUpdatedAsset(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Removed, args.Asset);
                }
                break;
            case "Update":
                if (args.Asset) {
                    this._addUpdatedAsset(Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Updated, args.Asset);
                }
                break;
            default:
                break;
        }
    },

    _addUpdatedAsset: function (state, asset) {
        var assetChildItemId = asset.ChildItemId;
        var assetChildItemProviderName = asset.ChildItemProviderName;
        var assetChildItemType = asset.ChildItemType;

        var alreadyAddedAsset = $.grep(this._updatedAssets, function (obj) {
            return obj.ChildItemId === assetChildItemId &&
                 obj.ChildItemProviderName === assetChildItemProviderName &&
                 obj.ChildItemType === assetChildItemType;
        });

        if (!(alreadyAddedAsset.length > 0)) {
            this._updatedAssets.push({
                State: state,
                ChildItemId: asset.ChildItemId,
                ChildItemProviderName: asset.ChildItemProviderName,
                ChildItemType: asset.ChildItemType,
                ChildItemAdditionalInfo: asset.ChildItemAdditionalInfo,
                Ordinal: asset.Ordinal,
                ComponentPropertyName: this._fieldName
            });
        } else {
            //check if has opposite states and remove it
            var updatedAsset = alreadyAddedAsset[0];
            var relatedState = Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState;
            if ((updatedAsset.State == relatedState.Removed && state == relatedState.Added) || (updatedAsset.State == relatedState.Added && state == relatedState.Removed)) {
                for (var i = this._updatedAssets.length - 1; i >= 0; i--) {
                    if (this._updatedAssets[i].ChildItemId === assetChildItemId &&
                            this._updatedAssets[i].ChildItemProviderName === assetChildItemProviderName &&
                            this._updatedAssets[i].ChildItemType === assetChildItemType) {
                        this._updatedAssets.splice(i, 1);
                    }
                }
            }

            if (state == Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.Updated) {
                updatedAsset.Ordinal = asset.Ordinal;
            }
        }
    },

    _sortItems: function (event, data) {

        //Create associative array for faster access
        var selectedAssetsQuickAccess = {};
        for (var i = 0, len = this._selectedAssets.length; i < len; i++) {
            selectedAssetsQuickAccess[this._selectedAssets[i].ContainerId] = this._selectedAssets[i];
        }

        var movedListItem = $(data.item);
        var movedListItemIndex = 0;
        var movedItemId = $(movedListItem).attr("id").substr(5);
        var movedItem = selectedAssetsQuickAccess[movedItemId];

        //Get moved item's new index in the DOM list
        var allListItems = $(this.get_selectedAssetsList()).children("li");
        for (var i = 0; i < allListItems.length; i++) {
            if ($(allListItems[i]).attr("id") == movedListItem.attr("id")) {
                movedListItemIndex = i;
                break;
            }
        }

        var prevItemId = null;
        var prevItem = null;
        var prevOrdinal = null;
        var nextItemId = null;
        var nextItem = null;
        var nextOrdinal = null;
        var newOrdinal = null;

        if (allListItems[movedListItemIndex - 1]) {
            prevItemId = $(allListItems[movedListItemIndex - 1]).attr("id").substr(5);
            prevItem = selectedAssetsQuickAccess[prevItemId];
            prevOrdinal = parseFloat(prevItem.Ordinal);
        }
        if (allListItems[movedListItemIndex + 1]) {
            nextItemId = $(allListItems[movedListItemIndex + 1]).attr("id").substr(5);
            nextItem = selectedAssetsQuickAccess[nextItemId];
            nextOrdinal = parseFloat(nextItem.Ordinal);
        }

        //Determine moved item's new Ordinal
        if (prevItem && nextItem) {
            //The item is placed between two other items
            newOrdinal = this._calculateOrdinalWithRounding(prevOrdinal, nextOrdinal);
            this._updateOrdinal(movedItem, newOrdinal);
        }
        else if (prevItem) {
            //The item is placed last
            newOrdinal = this._calculateOrdinalWithRounding(prevOrdinal, prevOrdinal + 2);
            this._updateOrdinal(movedItem, newOrdinal);
        }
        else if (nextItem) {
            //The item is placed first
            newOrdinal = this._calculateOrdinalWithRounding(nextOrdinal - 2, nextOrdinal);
            this._updateOrdinal(movedItem, newOrdinal);
        }
    },

    _calculateOrdinalWithRounding: function (previousOrdinal, nextOrdinal) {
        var average = (previousOrdinal + nextOrdinal) / 2;

        var roundedAverage = 0;
        var lastRoundedAverage = null;

        var precisionToRound = 0;
        while (true) {
            roundedAverage = parseFloat(average.toFixed(precisionToRound));

            if (roundedAverage > previousOrdinal && roundedAverage < nextOrdinal) {
                return roundedAverage;
            }
            if (lastRoundedAverage && lastRoundedAverage === roundedAverage) {
                // this means that the maximum precision of float has been reached
                //TODO handle this
                break;
            }
            lastRoundedAverage = roundedAverage;
            precisionToRound++;
        }

        return average;
    },

    _updateOrdinal: function (item, newOrdinal) {
        if (item.Ordinal != newOrdinal) {
            item.Ordinal = newOrdinal;
            this.raiseEvent("actionExecuted", { Name: "Update", Asset: item });
        }
    },

    isChanged: function () {
        return (this._updatedAssets.length > 0 || this._isDuplicateMode);
    },

    getChanges: function () {
        return this._updatedAssets;
    },

    //Fields implementing Telerik.Sitefinity.Web.UI.Fields.IRelatedDataField should not use get_value and set_value, but dataBind and getChanges methods instead. 
    //Get value is used only for validation.
    get_value: function () {
        return Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.callBaseMethod(this, "get_value");
    },

    set_value: function (value) {
        return;
    }
};

Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField.registerClass("Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField", Telerik.Sitefinity.Web.UI.Fields.AssetsField, Telerik.Sitefinity.Web.UI.Fields.IRelatedDataField);
