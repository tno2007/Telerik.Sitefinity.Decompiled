Telerik.Sitefinity.Web.UI.QueryData = function (queryData) {
    Telerik.Sitefinity.Web.UI.QueryData.initializeBase(this);
    // define variables
    this.Title = null;
    this.Id = Telerik.Sitefinity.getEmptyGuid();
    this.QueryItems = [];
    this.TypeProperties = [];
    this._itemPathSeparator = '_';

    // initializes the current instance with the WCF queryData object
    if (queryData) {
        this.copyFromObject(queryData);
    }

}
Telerik.Sitefinity.Web.UI.QueryData.prototype = {

    // set up
    initialize: function () {
        Telerik.Sitefinity.Web.UI.QueryData.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.QueryData.callBaseMethod(this, "dispose");
    },

    // functions
    getItemByPath: function (itemPath) {
        // TODO: Probably not the best way to do this
        var itemCount = this.get_QueryItems().length;
        for (var itemIndex = 0; itemIndex < itemCount; itemIndex++) {
            if (this.get_QueryItems()[itemIndex].get_ItemPath() === itemPath) {
                return this.get_QueryItems()[itemIndex];
            }
        }
        return null;
    },

    getItemLevel: function (itemPath) {
        var level = itemPath.split(this._itemPathSeparator).length;
        if (level < 0) {
            return 0;
        }
        return level - 2;
    },

    sortQueryItems: function () {
        this.QueryItems.sort(function (item1, item2) {
            var item1Segments = item1.get_PathSegments();
            var item2Segments = item2.get_PathSegments();

            var item1SegmentsCount = item1Segments.length;
            for (var sIter = 0; sIter < item1SegmentsCount; sIter++) {
                // if item2 has shorter path than item 1 and they are the same up to the length 
                // of item2's path, then the shorter comes before the longer
                if (item2Segments[sIter] == null) {
                    return 1;
                }

                // item 1 segment smaller than item 2 segment, return -1
                if (Number(item1Segments[sIter]) < Number(item2Segments[sIter])) {
                    return -1;
                }
                // else if item 1 segment larger than item 2 segment, return 1
                if (Number(item1Segments[sIter]) > Number(item2Segments[sIter])) {
                    return 1;
                }
            }

            // if the logic came to this point, it meant that the item2 had a longer path
            // than item1, but it was same up to the point where item1 had segments. Having
            // in mind that children (longer path) always come after the parent, we can
            // always return -1;
            return -1;
        });
    },

    separateQueryInLevels: function () {

        var levels = [];

        var itemCount = this.QueryItems.length;
        for (var i = 0; i < itemCount; i++) {
            var itemPath = this.QueryItems[i].get_ItemPath();
            var itemLevel = this.getItemLevel(itemPath);

            if (levels[itemLevel] === undefined) {
                levels[itemLevel] = [];
            }
            levels[itemLevel].push(this.QueryItems[i]);
        }
        return levels;
    },

    getParentItemPath: function (itemPath) {

        var lastSeparatorIndex = itemPath.lastIndexOf(this._itemPathSeparator);
        if (lastSeparatorIndex < 0) {
            return null;
        }
        else {
            var parentItemPath = itemPath.slice(0, lastSeparatorIndex);
            var parentUlClass = parentItemPath.substring(0, lastSeparatorIndex);
            return parentUlClass;
        }
    },

    getChildrenByParentPath: function (parentPath) {
        var children = [];
        var itemCount = this.QueryItems.length;
        for (var itemIndex = 0; itemIndex < itemCount; itemIndex++) {
            var itemPath = this.QueryItems[itemIndex].get_ItemPath();
            if (itemPath.slice(0, parentPath.length) === parentPath
                && itemPath.length > parentPath.length) {
                children.push(this.QueryItems[itemIndex]);
            }
        }
        return children;
    },

    getImmediateChildren: function (parentPath) {
        var children = [];
        var itemCount = this.QueryItems.length;
        for (var itemIndex = 0; itemIndex < itemCount; itemIndex++) {
            var itemPath = this.QueryItems[itemIndex].get_ItemPath();
            if (itemPath.slice(0, parentPath.length) === parentPath
                && this.getItemLevel(itemPath) - this.getItemLevel(parentPath) === 1) {
                children.push(this.QueryItems[itemIndex]);
            }
        }
        return children;
    },

    getSiblingsBelow: function (itemPath) {
        // GETS only the sibling below the itemPath (same level, higher ordinal)
        //var item = this.getItemByPath(itemPath);
        var itemOrdinal = Number(itemPath[itemPath.length - 1]);
        var siblings = [];
        var itemCount = this.QueryItems.length;
        for (var itemIndex = 0; itemIndex < itemCount; itemIndex++) {
            var currentItem = this.QueryItems[itemIndex];
            if (this.getParentItemPath(itemPath) === this.getParentItemPath(currentItem.get_ItemPath())) {
                if (currentItem.get_Ordinal() > itemOrdinal) {
                    siblings.push(currentItem);
                }
            }
        }
        return siblings;
    },

    createQueryDataItem: function (ordinal, path, isGroup, join, value) {
        var result = new Telerik.Sitefinity.Web.UI.QueryDataItem();
        result.set_IsGroup(isGroup);
        result.set_Ordinal(ordinal);
        result.set_Join(join);
        result.set_ItemPath(path);
        result.set_Value(value);

        var condition = new Telerik.Sitefinity.Web.UI.Condition();
        condition.set_Operator('=');
        condition.set_FieldName('FieldName');
        condition.set_FieldType('System.String');

        result.set_Condition(condition);
        result.set_Name('Test Group Name');
        return result;
    },

    updateSiblingOrdinalsAndPaths: function (senderPath, difference) {
        var siblings = this.getSiblingsBelow(senderPath);
        var siblingCount = siblings.length;
        if (siblingCount > 0) {
            for (var siblingIndex = siblingCount - 1; siblingIndex >= 0; siblingIndex--) {
                // get old partial path = item.get_path();
                var oldPath = siblings[siblingIndex].get_ItemPath();
                // decrease ordinal for the sibling
                var children = this.getChildrenByParentPath(oldPath);
                var childCount = children.length;

                siblings[siblingIndex].updateOrdinal(difference);
                // get new partial path = item.get_path();
                var newPath = siblings[siblingIndex].get_ItemPath();
                // for each sibling get all children
                for (var childIndex = 0; childIndex < childCount; childIndex++) {
                    children[childIndex].updatePathPartial(oldPath, newPath);
                }
            }
        }
    },

    conditionAdded: function (senderPath) {
        // get every sibling below the prevSiblingItem
        var siblings = this.getSiblingsBelow(senderPath);
        if (siblings.length > 0) {
            //var newItemPath = siblings[0].get_ItemPath();
            var siblingCount = siblings.length;
            for (var siblingIndex = siblingCount - 1; siblingIndex >= 0; siblingIndex--) {
                // get old partial path = item.get_path();
                var oldPath = siblings[siblingIndex].get_ItemPath();
                // increase ordinal for the sibling
                var children = this.getChildrenByParentPath(oldPath);
                var childCount = children.length;

                siblings[siblingIndex].increaseOrdinal();
                // get new partial path = item.get_path();
                var newPath = siblings[siblingIndex].get_ItemPath();
                // for each sibling get all children
                for (var childIndex = 0; childIndex < childCount; childIndex++) {
                    children[childIndex].updatePathPartial(oldPath, newPath);
                }
            }
        }
        // add item to items collection
        var newOrdinal = this.getItemByPath(senderPath).get_Ordinal();
        // create the new item with same ordinal and path as previous, and then increase
        var newItem = this.createQueryDataItem(newOrdinal, senderPath, false, 'AND', null);
        newItem.increaseOrdinal();
        this.QueryItems.push(newItem);
    },

    conditionRemoved: function (senderPath) {
        // get every sibling below the prevSiblingItem
        var siblings = this.getSiblingsBelow(senderPath);
        var siblingCount = siblings.length;

        // remove item
        var itemToRemove = this.getItemByPath(senderPath);
        var indexOfItemToRemove = this.QueryItems.indexOf(itemToRemove);
        this.QueryItems.splice(indexOfItemToRemove, 1);

        for (var siblingIndex = 0; siblingIndex < siblingCount; siblingIndex++) {
            // get old partial path = item.get_path();
            var oldPath = siblings[siblingIndex].get_ItemPath();
            // decrease ordinal for the sibling
            var children = this.getChildrenByParentPath(oldPath);
            var childCount = children.length;

            siblings[siblingIndex].decreaseOrdinal();
            // get new partial path = item.get_path();
            var newPath = siblings[siblingIndex].get_ItemPath();
            // for each sibling get all children
            for (var childIndex = 0; childIndex < childCount; childIndex++) {
                children[childIndex].updatePathPartial(oldPath, newPath);
            }
        }
    },

    conditionsGrouped: function (itemPaths) {
        // verify that the conditions selected can be grouped
        // (should be adjacent items on the same level and be more than one)
        if (itemPaths.length < 2) {
            throw { name: 'GroupError', message: 'Cannot group items, because less than two items were selected.' };
        }
        var selectedItems = [];
        var itemCount = itemPaths.length;
        var commonParentPath = null;
        for (var pathIndex = 0; pathIndex < itemCount; pathIndex++) {
            if (commonParentPath === null) {
                commonParentPath = this.getParentItemPath(itemPaths[pathIndex]);
            }
            if (commonParentPath !== this.getParentItemPath(itemPaths[pathIndex])) {
                throw { name: 'GroupError', message: 'Cannot group items, because they do not have the same parent.' };
            }
            selectedItems.push(this.getItemByPath(itemPaths[pathIndex]));
        }

        selectedItems.sort(function (item1, item2) {
            if (item1.get_Ordinal() > item2.get_Ordinal()) {
                return 1;
            }
            if (item1.get_Ordinal() < item2.get_Ordinal()) {
                return -1;
            }
            return 0;
        });

        var currentOrdinal = selectedItems[0].get_Ordinal();
        for (var itemIndex = 1; itemIndex < itemCount; itemIndex++) {
            if (selectedItems[itemIndex].get_Ordinal() - currentOrdinal !== 1) {
                throw { name: 'GroupError', message: 'Cannot group items because they are not adjacent' };
            }
            currentOrdinal = selectedItems[itemIndex].get_Ordinal();
        }

        // enough verification

        // create an item for the new parent (group item)
        var firstSelectedItem = selectedItems[0];
        var newParent = this.createQueryDataItem(firstSelectedItem.get_Ordinal(), firstSelectedItem.get_ItemPath(), true,
            firstSelectedItem.get_Join(), null);
        // increase level for grouped items
        for (var itemIndex = 0; itemIndex < itemCount; itemIndex++) {
            var oldPath = selectedItems[itemIndex].get_ItemPath();
            var chidlren = this.getChildrenByParentPath(oldPath);
            var childCount = chidlren.length;
            if (childCount > 0) {
                // just a verification - theoretically this should never happen
                throw { name: 'GroupError', message: 'Cannot group items because some of the selected items have children' };
            }
            selectedItems[itemIndex].increaseLevel(newParent.get_ItemPath(), itemIndex);
            //var newPath = selectedItems[itemIndex].get_ItemPath();
            //for (var childIndex = 0; childIndex < childCount; childIndex++) {
            //    children[childIndex].updatePathPartial(oldPath, newPath);
            //}
        }

        // modify ordinals for siblings below new item
        this.updateSiblingOrdinalsAndPaths(newParent.get_ItemPath(), -itemCount);
        this.QueryItems.push(newParent);
    },

    conditionsUngrouped: function (senderPath) {
        // when we ungroup, we are in fact removing a group item and promoting its children one level up.
        // There is one subtle difference with remove, though.

        // when we remove an item, we don't care about its children, and the only thing to do is
        // DECREMENT the ordinals of the siblings below it (to fill the gap)

        // when we ungroup, we are promoting children, so we should INCREMENT the ordinals of the
        // siblings below, with the amount of the immediate children of
        // the group item (the number of immediate items or groups inside the group we remove)

        // we should also INCREMENT those ordinals BEFORE we promote the children to avoid duplicating paths
        // in theory, we should increment ordinals with at least 1, because there is no case of a group
        // with less than 2 items

        var immediateChildren = this.getImmediateChildren(senderPath);
        this.updateSiblingOrdinalsAndPaths(senderPath, immediateChildren.length - 1);

        var itemToRemove = this.getItemByPath(senderPath);
        var indexOfItemToRemove = this.QueryItems.indexOf(itemToRemove);
        this.QueryItems.splice(indexOfItemToRemove, 1);

        var childCount = immediateChildren.length;
        // for the immediate children, we need to update the ordinals by adding the parent's ordinal
        // and then change the path to get them one level up
        for (var childIndex = 0; childIndex < childCount; childIndex++) {
            var oldPath = immediateChildren[childIndex].get_ItemPath();
            var oldOrdinal = immediateChildren[childIndex].get_Ordinal();
            var children = this.getChildrenByParentPath(oldPath);
            immediateChildren[childIndex].decreaseLevel(senderPath, itemToRemove.get_Ordinal());
            var newPath = immediateChildren[childIndex].get_ItemPath();
            var nestedChildCount = children.length;
            // for the deeper children of each immediate child, we need to only replace the path
            // with the new path of the parent (i.e. skip the level decrease)
            for (var nestedIndex = 0; nestedIndex < nestedChildCount; nestedIndex++) {
                children[nestedIndex].updatePathPartial(oldPath, newPath);
            }
        }
    },

    toLinq: function () {
        ///return a dynamic linq analog of the query data
        return this._translateToLinq()
    },


    addGroup: function (name, join, parentGroup) {
        //adds a new group to the query data
        //parameters:
        //  name: the name of the group
        //  join: the logical operator that will be used when the group is joined to the other groups. Possible values: 'AND', 'OR'
        //  [optional]parentGroup: the group that will be the parent of the new group
        //returns: the new group
        var ordinal = 0;
        var path = "";
        var levels = this.separateQueryInLevels();
        if (parentGroup) {
            var parentPath = parentGroup.get_ItemPath();
            var directChildren = this.getDirectChildren(parentGroup);
            ordinal = directChildren.length;
            path += parentPath;
        }
        else {
            if (levels.length > 0)
                ordinal = levels[0].length;
        }
        path += this._itemPathSeparator + ordinal;
        var group = this.createFullQueryDataItem(name, ordinal, path, true, join, null);
        this.QueryItems.push(group);
        return group;
    },

    removeGroup: function (group) {
        //removes the specified group from the query data
        //parameters:
        //  group: a Telerik.Sitefinity.Web.UI.QueryDataItem instance representing a group that will be removed

        if (!this._checkIfGroupItemExists(group.Name))
            return;

        var children = this.getChildren(group);
        var queryItemsCount = this.QueryItems.length;
        while (queryItemsCount--) {
            var item = this.QueryItems[queryItemsCount];
            if (jQuery.inArray(item, children) > -1 || item == group)
                this.QueryItems.splice(queryItemsCount, 1);
        }
        var level = group.ItemPath.split(this._itemPathSeparator).length - 2; // the first element is empty
        this.updateItemsOrdinalsAndPaths(level, group.Ordinal, -1);
    },

    hasGroup: function (name) {
        //checks whether the specified group exists in the query data
        //parameters:
        //  name: the name of the group to be checked
        //returns: true if the the query data contains a group with that name; otherwise false
        var group = this.getItemByName(name);
        return group != null && group.IsGroup;
    },

    addChildToGroup: function (group, name, join, fieldName, fieldType, operator, value) {
        //adds a child to the given group
        //parameters:
        //  group: a Telerik.Sitefinity.Web.UI.QueryDataItem instance
        //  name: the name of the child
        //  join: the logical operator that will be used when the child is joined to the other children. Possible values: 'AND', 'OR'
        //  fieldName: the name of the data member that will participate in the expression
        //  fieldType: the type of the data member that will participate in the expression
        //  operator: the operator of the query item
        //  value: the value of the query item
        //returns: the new child
        var directChildren = this.getDirectChildren(group);
        var ordinal = directChildren.length;
        var path = group.ItemPath + this._itemPathSeparator + ordinal;
        var condition = new Telerik.Sitefinity.Web.UI.Condition();
        condition.FieldName = fieldName;
        condition.FieldType = fieldType;
        condition.Operator = operator;
        var child = this.createFullQueryDataItem(name, ordinal, path, false, join, value);
        child.Condition = condition;
        this.QueryItems.push(child);
        return child;
    },

    // Adds a query item to the specified group. The ordinal and path of the item will be fixed to fit the specified group
    addQueryItemToGroup: function (group, queryItem) {

        if (!Telerik.Sitefinity.Web.UI.QueryDataItem.isInstanceOfType(queryItem)) {
            // If a JSON object convert it to QueryDataItem
            var item = new Telerik.Sitefinity.Web.UI.QueryDataItem();
            queryItem = item.copyFromObject(queryItem);
        }
        var directChildren = this.getDirectChildren(group);
        var ordinal = directChildren.length;
        var path = group.ItemPath + this._itemPathSeparator + ordinal;
        queryItem.Ordinal = ordinal;
        queryItem.ItemPath = path;
        this.QueryItems.push(queryItem);
        return queryItem;
    },

    // returns the direct children of the given group
    getDirectChildren: function (group) {
        return this.getImmediateChildren(group.ItemPath);
    },
    // returns all children in depth of the given group
    getChildren: function (group) {
        return this.getChildrenByParentPath(group.ItemPath);
    },
    // searches for an item by its name
    getItemByName: function (name) {
        for (var i = 0, l = this.QueryItems.length; i < l; i++) {
            if (this.QueryItems[i].Name == name)
                return this.QueryItems[i];
        }
        return null;
    },
    // fills the properties of the current instance with the properties of the passed object
    copyFromObject: function (obj) {
        this.Title = obj.Title;
        this.Id = obj.Id;
        this.TypeProperties = obj.TypeProperties;
        var queryItems = [];
        if (obj.QueryItems) {
            for (var i = 0, l = obj.QueryItems.length; i < l; i++) {
                var queryItem = new Telerik.Sitefinity.Web.UI.QueryDataItem();
                queryItem = queryItem.copyFromObject(obj.QueryItems[i]);
                queryItems.push(queryItem);
            }
        }
        this.QueryItems = queryItems;
        return this;
    },
    // creates a new Telerik.Sitefinity.Web.UI.QueryDataItem instance with the given properties
    createFullQueryDataItem: function (name, ordinal, itemPath, isGroup, join, value) {
        var item = new Telerik.Sitefinity.Web.UI.QueryDataItem();
        item.Name = name;
        item.Ordinal = ordinal;
        item.ItemPath = itemPath;
        item.IsGroup = isGroup;
        item.Join = join;
        item.Value = value;
        return item;
    },

    updateItemsOrdinalsAndPaths: function (level, startOrdinal, difference) {
        var separator = this._itemPathSeparator;
        var levels = this.separateQueryInLevels();
        if (levels.length == 0)
            return;
        var levelItems = levels[level];

        for (var j = startOrdinal, l = levelItems.length; j < l; j++) {
            var item = levelItems[j];
            var children = this.getChildrenByParentPath(item.ItemPath);
            for (var k = 0, m = children.length; k < m; k++) {
                var child = children[k];
                this._updateItemPath(child, level, difference);
            }
            this._updateItemPath(item, level, difference);
            item.Ordinal += difference;
        }
    },

    _updateItemPath: function (item, level, difference) {
        var separator = this._itemPathSeparator;
        var pathSegments = item.ItemPath.split(separator);
        pathSegments.shift(); // removes the first empty element
        pathSegments[level] = parseInt(pathSegments[level]) + difference;
        item.ItemPath = separator + pathSegments.join(separator);
    },

    _translateToLinq: function (queryItems) {
        ///dynamic linq translation that can process a list of query items being either simple items or groups(nested queryitems)
        var firstItem = true;
        var resultLinq = "";
        if (queryItems == undefined || queryItems == null) {
            queryItems = this.getImmediateChildren("");
        }

        for (var i = 0, length = queryItems.length; i < length; i++) {
            var item = queryItems[i];
            if (!firstItem) {
                resultLinq += " " + item.Join + " ";
            }
            if (!item.IsGroup) {
                resultLinq += this._translateQueryItem(item);
            }
            else {
                resultLinq += "(";
                var groupItems = this.getImmediateChildren(item.get_ItemPath())
                resultLinq += this._translateToLinq(groupItems);
                resultLinq += ")";
            }
            firstItem = false;
        }        
        return resultLinq;
    },

    _translateQueryItem: function (queryItem) {
        ///this translates a single simple query item into dynamic linq expression 
        ///the algorythm considers the condition operator and the field type to build the proper expression
        var condition = queryItem.get_Condition();
        var linq = "";
        linq += condition.FieldName;
        var operator = condition.get_Operator();
        var value = queryItem.get_Value();
        var wrappedValue = this._wrapQueryLinqValue(condition.get_FieldType(), value);

        switch (operator) {
            case "=":
            case "<":
            case ">":
            case ">=":
            case "<=":
                linq += operator;
                linq += wrappedValue;
                break;
            case "<>":
                linq += "!=";
                linq += wrappedValue;
            case "Contains":
            case "StartsWith":
            case "EndsWith":
                linq += "." + operator + "(" + wrappedValue + ")";
        }
        return linq;
    },

    _wrapQueryLinqValue: function (valueType, value) {
        ///escapes the query value depending on the field type, e.g. puts quotes for strings etc.
        switch (valueType) {
            case "Telerik.Sitefinity.Model.Lstring":
            case "System.String":
                return "\"" + value + "\"";
            case "System.DateTime":
                return "(" + value + ")";
            case "System.Guid":
                return "(" + value + ")";
            case "System.Double":
                return value + "d";
            case "System.Single":
                return value + "f";
            case "System.Decimal":
                return value + "m";
            case "System.Int64":
                return value + "L";
            default:
                return value;
        }
    },

    _checkIfGroupItemExists: function (groupName) {
        var queryItems = this.QueryItems;
        for (var i = 0; i < queryItems.length; i++) {
            var item = queryItems[i];
            if(item.IsGroup && item.Name == groupName)
                return true;
        }

        return false;
    },

    // properties
    get_Title: function () {
        return this.Title;
    },
    set_Title: function (value) {
        if (this.Title != value) {
            this.Title = value;
            this.raisePropertyChanged('Title');
        }
    },

    get_Id: function () {
        return this.Id;
    },
    set_Id: function (value) {
        if (this.Id != value) {
            this.Id = value;
            this.raisePropertyChanged('Id');
        }
    },

    get_TypeProperties: function () {
        return this.TypeProperties;
    },
    set_TypeProperties: function (value) {
        if (this.TypeProperties != value) {
            this.TypeProperties = value;
            this.raisePropertyChanged('TypeProperties');
        }
    },

    get_QueryItems: function () {
        return this.QueryItems;
    },
    set_QueryItems: function (value) {
        if (this.QueryItems != value) {
            this.QueryItems = value;
            this.raisePropertyChanged('QueryItems');
        }
    }
};

//static function deserialize
//creates a typed QueryData object from untyped json array
Telerik.Sitefinity.Web.UI.QueryData.deserialize = function (data) {
    var dataObj;
    if (typeof data == 'string') {
        dataObj = Sys.Serialization.JavaScriptSerializer.deserialize(data)
    }
    else {
        dataObj = data;
    }
    var result = $create(Telerik.Sitefinity.Web.UI.QueryData, dataObj);
    var items = new Array();
    for (var i = 0; i < dataObj.QueryItems.length; i++) {
        var item = dataObj.QueryItems[i];
        var q_item = $create(Telerik.Sitefinity.Web.UI.QueryDataItem, item, null, null, null);
        var q_condition = $create(Telerik.Sitefinity.Web.UI.Condition, item.Condition, null, null, null);
        q_item.set_Condition(q_condition);
        items.push(q_item)
    }
    result.set_QueryItems(items);
    return result;
}


Telerik.Sitefinity.Web.UI.QueryData.registerClass('Telerik.Sitefinity.Web.UI.QueryData', Sys.Component);
