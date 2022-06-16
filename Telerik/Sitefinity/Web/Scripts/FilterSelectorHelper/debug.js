var _translateQueryItems = function (collection) {
    if (!collection || collection.length == 0 || collection.length > 2)
        return [];
    var result = {};
    for (var i = 0; i < collection.length; i++) {
        var item = collection[i];
        var operator = item.Condition.Operator;
        if (operator == ">") {
            result.From = item.Value;
        }
        else if (operator == "<") {
            result.To = item.Value;
        }
    }
    return [result];
};
var _buildQueryItems = function (queryData, collection) {
    if (!collection || collection.length != 1)
        return;
    var item = collection[0];
    var itemName;
    if (item.From) {
        itemName = String.format("{0}.{1}", this._group.Name, item.From);
        queryData.addChildToGroup(this._group, itemName, this._itemLogicalOperator, this._queryFieldName, this._queryFieldType, ">", item.From); //TODO
    }
    if (item.To) {
        itemName = String.format("{0}.{1}", this._group.Name, item.To);
        queryData.addChildToGroup(this._group, itemName, this._itemLogicalOperator, this._queryFieldName, this._queryFieldType, "<", item.To); //TODO
    }
};
// sets the content filter setting based on the radio button that selected the filter type
var SelectorDesignerView$setContentFilter = function (sender) {

    var radioID = sender.target.value;
    var controlData = this.get_controlData();
    var disabledFilter = true;
    switch (radioID) {
        case "contentSelect_AllItems":
            jQuery(this.get_element()).find('#selectorPanel').hide();
            if (!this._refreshing) {
                controlData.ContentViewDisplayMode = "Automatic";
            }
            break;
        case "contentSelect_OneItem":
            jQuery(this.get_element()).find('#selectorPanel').show();
            if (!this._refreshing) {
                controlData.ContentViewDisplayMode = "Detail";
            }
            break;
        case "contentSelect_SimpleFilter":
            disabledFilter = false;
            if (!this._refreshing) {
                controlData.ContentViewDisplayMode = "Automatic";
            }
            break;
        case "contentSelect_AdvancedFilter": break;
    }
    this._filterSelector.set_disabled(disabledFilter);
    dialogBase.resizeToContent();
}

var SelectorDesignerView$refreshFilterUI = function () {
    var controlData = this.get_controlData();
    var additionalFilter = this.get_currentMasterView().AdditionalFilter;
    if (additionalFilter)
        additionalFilter = Sys.Serialization.JavaScriptSerializer.deserialize(additionalFilter);
    this._filterSelector.set_queryData(additionalFilter);
    var disabledFilter = true;
    if (controlData.ContentViewDisplayMode != "Detail") {
        if (additionalFilter) {
            this.get_radioChoices()[1].click();
            disabledFilter = false;
        }
        else
            this.get_radioChoices()[0].click();
    }
    else {
        this.get_radioChoices()[1].click()
    }
    this._filterSelector.set_disabled(disabledFilter);
}

var SelectorDesignerView$applyFilterChanges = function (currentView) {
    var displayMode = this.get_controlData().ContentViewDisplayMode;
    currentView.AdditionalFilter = null;
    if (displayMode == "Automatic" || displayMode == "Master") {
        if (this.get_radioChoices().eq(1).attr("checked")) {
            this._filterSelector.applyChanges();
            var queryData = this._filterSelector.get_queryData();
            if (queryData.QueryItems && queryData.QueryItems.length > 0)
                queryData = Telerik.Sitefinity.JSON.stringify(queryData);
            else
                queryData = null;
            currentView.AdditionalFilter = queryData;
        }
    }
}