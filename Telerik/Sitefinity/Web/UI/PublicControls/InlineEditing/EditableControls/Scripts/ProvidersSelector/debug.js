define(["FlatContentSelector"], function (FlatContentSelector) {
    function ProvidersSelector(parentElement) {
        
        FlatContentSelector.call(this, parentElement);
        this.viewModelObjExt.contentDataSource.schema.model.id = "Name";
        this.serviceUrl = "Sitefinity/Services/DataSourceService/providers/";
        this.dataSourceName = "Telerik.Sitefinity.Modules.Libraries.LibrariesManager";
        return (this);
    }

    ProvidersSelector.prototype = {

        parameterMapFunc: function (options) {
            var queryObj = {
                sortExpression: "Title ASC",
                dataSourceName: this.dataSourceName,
                useCurrentSite: true
            };
            if (this.isSearchRequest()) {
                queryObj.skip = 0;
                queryObj.take = 20;
                queryObj.filter = String.format("Title.StartsWith(\"{0}\")", this.viewModel.searchValue);
            }
            else {
                queryObj.skip = options.skip;
                queryObj.take = options.take;
                queryObj.filter = "";
            }

            return queryObj;
        },

        resolveUrlFunc: function (options) {
            return this.getServiceUrl();
        }
    };
    ProvidersSelector.prototype = $.extend(Object.create(FlatContentSelector.prototype), ProvidersSelector.prototype);
    return (ProvidersSelector);
});