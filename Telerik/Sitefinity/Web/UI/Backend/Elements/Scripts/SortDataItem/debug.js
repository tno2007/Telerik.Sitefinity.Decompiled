Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");


/* SortDataItem class - data needed for construction of one sorting condition */
Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem = function() {
   
    this._ordinal = null;
    this._sortBy = null;
    this._sortType = null;

    Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem.prototype = {
    /* --------------------  set up and tear down ----------- */
    
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem.callBaseMethod(this, "initialize");
     
    },

    dispose: function() {
        Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem.callBaseMethod(this, "dispose");
    },

    /* -------------------- events -------------------- */


    /* -------------------- event handlers ------------ */


    /* -------------------- private methods ----------- */
 
    /* -------------------- properties ---------------- */

    get_ordinal: function() {
        return this._ordinal;
    },
    set_ordinal: function(value) {
        if (this._ordinal != value) {
            this._ordinal = value;
            this.raisePropertyChanged('ordinal');
        }
    },
    get_sortBy: function() {
        return this._sortBy;
    },
    set_sortBy: function(value) {
        if (this._sortBy != value) {
            this._sortBy = value;
            this.raisePropertyChanged('sortBy');
        }
    },
    get_sortType: function() {
        return this._sortType;
    },
    set_sortType: function(value) {
        if (this._sortType != value) {
            this._sortType = value;
            this.raisePropertyChanged('sortType');
        }
    }
};

Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem.registerClass('Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem', Sys.Component);