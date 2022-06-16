define(["FlatContentSelector"], function (FlatContentSelector) {
    function FlatTaxonSelector(options) {
        FlatContentSelector.call(this, options);
        this.serviceUrl = "Sitefinity/Services/Taxonomies/FlatTaxon.svc";
        this.taxonomyId = null;
        return (this);
    }

    FlatTaxonSelector.prototype = {
        
        parameterMapFunc: function (options) {
            var queryObj = {
                sortExpression: "Title ASC",
                mode: "Simple",
                take: options.take
            };
            if (this.isSearchRequest()) {
                if (this.viewModel.contentDataSource.page() > 1) {
                    queryObj.skip = options.skip;
                } else {
                    queryObj.skip = 0;
                }
                queryObj.filter = this.generateFilterExpression("Title", this.viewModel.searchValue);
            }
            else {
                queryObj.skip = options.skip;
                queryObj.take = options.take;
            }

            return queryObj;
        },

        resolveUrlFunc: function (options) {
            return String.format("{0}/{1}/", this.getServiceUrl(), this.taxonomyId);
        }
    };
    FlatTaxonSelector.prototype = $.extend(Object.create(FlatContentSelector.prototype), FlatTaxonSelector.prototype);
    return (FlatTaxonSelector);
});