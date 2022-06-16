Telerik.Sitefinity.Web.UI.QueryDataItem = function () {
    // define variables
    this.IsGroup = null;
    this.Ordinal = null;
    this.Join = null;
    this.ItemPath = null;
    this.Value = null;
    this.Condition = null;
    this.Name = null;
    this._itemPathSeparator = '_';
    Telerik.Sitefinity.Web.UI.QueryDataItem.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.QueryDataItem.prototype = {

    // set up
    initialize: function () {
        Telerik.Sitefinity.Web.UI.QueryDataItem.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.QueryDataItem.callBaseMethod(this, "dispose");
    },

    _getParentItemPath: function (itemPath) {
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

    increaseOrdinal: function () {
        // increases ordinal and modifies path only for item itself
        //this.set_Ordinal(this.get_Ordinal() + 1);
        //this.set_ItemPath(this._getParentItemPath(this.get_ItemPath()) + this._itemPathSeparator + this.get_Ordinal());

        this.updateOrdinal(1);
    },

    decreaseOrdinal: function () {
        // decreases ordinal and modifies path only for item itself
        //this.set_Ordinal(this.get_Ordinal() - 1);
        //this.set_ItemPath(this._getParentItemPath(this.get_ItemPath()) + this._itemPathSeparator + this.get_Ordinal());

        this.updateOrdinal(-1);
    },

    updateOrdinal: function (difference) {
        // either decreases or increases the ordinal by adding the difference
        // if the difference is negative it means we are decreasing
        var newOrdinal = this.get_Ordinal() + difference;
        if (newOrdinal < 0) {
            throw "Incorrect update of ordinal - new ordinal is negative";
        }
        this.set_Ordinal(newOrdinal);
        this.set_ItemPath(this._getParentItemPath(this.get_ItemPath()) + this._itemPathSeparator + this.get_Ordinal());
    },

    increaseLevel: function (parentPath, ordinal) {
        // TODO: This has to take parameters for the level of the parent
        // ONLY FOR THE ITEM ITEMSELF
        var newPath = parentPath + this._itemPathSeparator + ordinal;
        this.set_ItemPath(newPath);
        this.set_Ordinal(ordinal);
    },

    decreaseLevel: function (oldParentPath, oldParentOrdinal) {
        // TODO: This has to take parameters
        // ONLY FOR THE ITEM ITEMSELF
        var newPath = oldParentPath.slice(0, oldParentPath.length - 1);
        newPath += (oldParentOrdinal + this.get_Ordinal());
        this.set_ItemPath(newPath);
        this.set_Ordinal(oldParentOrdinal + this.get_Ordinal());
    },

    updatePathPartial: function (oldPartialPath, newPartialPath) {
        // TODO: string replace from 0 old partial path with new partial path
        // replace only the first occurence of the OldPath with the newPath
        var newItemPath = this.ItemPath.replace(oldPartialPath, newPartialPath);
        this.set_ItemPath(newItemPath);
    },
    // fills the properties of the current instance with the properties of the passed object
    copyFromObject: function (obj) {
        this.IsGroup = obj.IsGroup;
        this.Ordinal = obj.Ordinal;
        this.Join = obj.Join;
        this.ItemPath = obj.ItemPath;
        this.Value = obj.Value;
        this.Name = obj.Name;
        //this.Id = obj.Id;
        var condition = new Telerik.Sitefinity.Web.UI.Condition();
        if (obj.Condition) {
            condition.copyFromObject(obj.Condition);
        }
        this.Condition = condition;
        return this;
    },

    // properties
    get_IsGroup: function () {
        return this.IsGroup;
    },
    set_IsGroup: function (value) {
        if (this.IsGroup != value) {
            this.IsGroup = value;
            this.raisePropertyChanged('IsGroup');
        }
    },

    get_Ordinal: function () {
        return this.Ordinal;
    },
    set_Ordinal: function (value) {
        if (this.Ordinal != value) {
            this.Ordinal = value;
            this.raisePropertyChanged('Ordinal');
        }
    },

    get_Join: function () {
        return this.Join;
    },
    set_Join: function (value) {
        if (this.Join != value) {
            this.Join = value;
            this.raisePropertyChanged('Join');
        }
    },

    get_ItemPath: function () {
        return this.ItemPath;
    },
    set_ItemPath: function (value) {
        if (this.ItemPath != value) {
            this.ItemPath = value;
            this.raisePropertyChanged('ItemPath');
        }
    },

    get_PathSegments: function () {
        var segments = this.get_ItemPath().split('_');
        // remove the first empty string
        return segments.slice(1, segments.length);
    },

    get_Value: function () {
        return this.Value;
    },
    set_Value: function (value) {
        if (this.Value != value) {
            this.Value = value;
            this.raisePropertyChanged('Value');
        }
    },

    get_Condition: function () {
        return this.Condition;
    },
    set_Condition: function (value) {
        if (this.Condition != value) {
            this.Condition = value;
            this.raisePropertyChanged('Condition');
        }
    },

    get_Name: function () {
        return this.Name;
    },
    set_Name: function (value) {
        if (this.Name != value) {
            this.Name = value;
            this.raisePropertyChanged('Name');
        }
    }
};

Telerik.Sitefinity.Web.UI.QueryDataItem.registerClass('Telerik.Sitefinity.Web.UI.QueryDataItem', Sys.Component);

/* Condition class */
Telerik.Sitefinity.Web.UI.Condition = function () {
    // define variables
    this.FieldName = null;
    this.FieldType = null;
    this.Operator = null;
    Telerik.Sitefinity.Web.UI.Condition.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Condition.prototype = {

    // set up
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Condition.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Condition.callBaseMethod(this, "dispose");
    },

    // fills the properties of the current instance with the properties of the passed object
    copyFromObject: function (obj) {        
        this.FieldName = obj.FieldName;
        this.FieldType = obj.FieldType;
        this.Operator = obj.Operator;
        return this;
    },
    
    get_FieldName: function () {
        return this.FieldName;
    },
    set_FieldName: function (value) {
        if (this.FieldName != value) {
            this.FieldName = value;
            this.raisePropertyChanged('FieldName');
        }
    },

    get_FieldType: function () {
        return this.FieldType;
    },
    set_FieldType: function (value) {
        if (this.FieldType != value) {
            this.FieldType = value;
            this.raisePropertyChanged('FieldType');
        }
    },

    get_Operator: function () {
        return this.Operator;
    },
    set_Operator: function (value) {
        if (this.Operator != value) {
            this.Operator = value;
            this.raisePropertyChanged('Operator');
        }
    }
};

Telerik.Sitefinity.Web.UI.Condition.registerClass('Telerik.Sitefinity.Web.UI.Condition', Sys.Component);
