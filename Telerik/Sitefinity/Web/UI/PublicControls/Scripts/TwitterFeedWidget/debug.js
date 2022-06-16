Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls");

Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget = function (element) {
    Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget.initializeBase(this, [element]);

    this._serviceUrl = null;
    this._refreshInterval = null;
    this._itemsControlId = null;

    this._handlePageLoadDelegate = null;
    this._bindDelegate = null;

}

Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget.callBaseMethod(this, 'initialize');

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        this._bindDelegate = Function.createDelegate(this, this._handleBindD);
        Sys.Application.add_load(this._handlePageLoadDelegate);
    },

    dispose: function () {
        
        Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget.callBaseMethod(this, 'dispose');

        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }


    },


    _handlePageLoad: function (sender, args) {
        var func = this._handlePageLoadDelegate;
        window.setTimeout(func, this.get_refreshInterval()*1000);
        this.loadData();
    },

    _handleBindD: function (sender, args) {
        $("#" + this.get_itemsControlId()).empty();
        var data = sender;
        for (var itemIndex = 0; itemIndex < data.Items.length; itemIndex++) {

            var currentTemplate = template;
            for (var propIndex in data.Items[itemIndex].dynamicProperties) {
                var propertyName = data.Items[itemIndex].dynamicProperties[propIndex].Key;
                var propertyValue = data.Items[itemIndex].dynamicProperties[propIndex].Value;
                if (propertyValue != null) {
                    if (propertyValue.indexOf('Date') != -1) {
                        propertyValue = new Date(parseInt(propertyValue.substr(6)));

                        propertyValue = propertyValue.toLocaleDateString() + "  " + propertyValue.toLocaleTimeString();
                    }

                    currentTemplate = currentTemplate.replace("#" + propertyName, propertyValue);
                }
            }
            $("#" + this.get_itemsControlId()).append(currentTemplate);
        }
    },

    loadData: function (sender, args) {
        $.ajax({
            dataType: "json",
            url: this.get_serviceUrl()
        })
       .done(this._bindDelegate);

    },

    
    get_refreshInterval: function () {
        return this._refreshInterval;
    },
    set_refreshInterval: function (value) {
        this._refreshInterval = value;
    },

    get_itemsControlId: function () {
        return this._itemsControlId;
    },
    set_itemsControlId: function (value) {
        this._itemsControlId = value;
    },

    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    }


    
}

Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeedWidget', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();