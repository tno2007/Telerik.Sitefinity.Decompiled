Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.FieldChangeNotifier = function (fieldsBinder, dataItem) {
    /// <summary>Provides access to the whole data item of a fields control binder to a field control</summary>
    /// <param name="fieldsBinder">Instance of Telerik.Sitefinity.Web.UI.FieldControls.FieldControlsBinder</param>
    /// <param name="dataItem"><paramref name="fieldsBinder" />'s data item</param>
    this._binder = fieldsBinder;
    this._dataItem = dataItem;

    this._fieldNames = [];
    this._fieldMap = {};

    var ctrls = this._binder._fieldControls;
    var iter = ctrls.length;
    var cur = null;
    var dependantFieldControls;

    for (var propName in dataItem) {
        if (typeof dataItem[propName] != "function") {
            dependantFieldControls = [];
            var obj = {};
            iter = ctrls.length;
            while (iter--) {
                cur = ctrls[iter];
                if (cur.get_dataFieldName() == propName) {
                    dependantFieldControls.push(cur);
                }
            }
            obj.dependantFieldControls = dependantFieldControls;
            this._fieldMap[propName] = obj;
            this._fieldNames.push(propName);
        }
    }
}

Telerik.Sitefinity.Web.UI.FieldChangeNotifier.prototype = {
    get_fieldNames: function () {
        /// <summary>Get the names of all fields in the data item</summary>
        /// <returns>Array of strings</returns>

        // so that it can't be modified
        var clone = Array.clone(this._fieldNames);
        return clone;
    },

    get_fieldValuesFromUI: function (fieldName) {
        /// <summary>Get value of a field by quering the UI instead of the data item itself</summary>
        /// <param name="">Data item field name</param>
        /// <returns>Array of the values returned by all field controls bound to this data item field</returns>

        var values = [];
        if (this._fieldMap.hasOwnProperty(fieldName) && typeof this._fieldMap[fieldName] != "function") {
            var dependentFields = this._fieldMap[fieldName].dependantFieldControls;
            var len = dependentFields.length;
            for (var idx = 0; idx < len; idx++) {
                values.push(dependentFields[idx].get_value());
            }
        }
        return values;
    },

    get_fieldValue: function (fieldName) {
        /// <summary>Get the value of a field in the data item</summary>
        /// <parm name="fieldName">Name of the data item field to get the value of</param>        
        /// <returns>Deep copy of the value</returns>

        // so that it can't be modified
        var undef;
        if (this.containsField(fieldName)) {
            var val = this._dataItem[fieldName];
            var clone = Telerik.Sitefinity.cloneObject(val);
            return clone;
        }
        else {
            return undef;
        }
    },

    set_fieldValue: function (fieldName, value) {
        /// <summary>Sets the value of a field in the data item only if it already exits. 
        /// Performs notification before the actual modification.</summary>
        /// <param name="fieldName">Name of the field to set value to with notification</param>
        /// <param name="value">New value</param>
        /// <returns>true if value was set (field exists), false otherwize.</returns>
        if (this.notifyFieldChange(fieldName, value)) {
            this._dataItem[fieldName] = value;
            return true;
        }
        else {
            return false;
        }
    },

    containsField: function (fieldName) {
        /// <summary>Chekcs if the data item has a member with a certain name</summary>
        /// <param name="fieldName">Name of the field to check for existence</param>
        /// <remarks>If this is the name of a function, false will be returned.</remarks>
        /// <returns>true if the data item has a member with that name which is not a function, false otherwize</returns>
        return this._dataItem.hasOwnProperty(fieldName) && typeof this._dataItem[fieldName] != "function";
    },

    ensureFieldExists: function (fieldName) {
        /// <summary>Checks if the data item has a field with a certain name, and creates it if it doesn't. 
        /// Does not perform notification.</summary>
        /// <param name="fieldName">Name of the field</param>
        if (!this.containsField(fieldName)) {
            this._fieldMap[fieldName] = { dependantFieldControls: [] };
            this._dataItem[fieldName] = null;
        }
    },

    notifyFieldChange: function (fieldName, value) {
        /// <summary>Notify all field controls bound to a certain field that its value has been changed, 
        /// without actually changing the value </summary>
        /// <param name="fieldName">Name of the field that has been changed</param>
        /// <param name="value">New value of the field</param>
        /// <returns>true if there is such a field, false if there is not, and nothing was done</returns>
        if (this.containsField(fieldName)) {
            var ctrls = this._fieldMap[fieldName].dependantFieldControls;
            var idx = ctrls.length;
            while (idx--) {
                ctrls[idx].set_value(value);
            }
            return true;
        }
        else {
            return false;
        }
    }
};

Telerik.Sitefinity.Web.UI.FieldChangeNotifier.registerClass("Telerik.Sitefinity.Web.UI.FieldChangeNotifier");
