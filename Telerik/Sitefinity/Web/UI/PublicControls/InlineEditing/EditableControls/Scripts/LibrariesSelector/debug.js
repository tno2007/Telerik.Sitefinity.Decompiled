define(["HierarchicalContentSelector"], function (HierarchicalContentSelector) {
    function LibrariesSelector(parentElement) {
        var that = this;
        HierarchicalContentSelector.call(this, parentElement);
        this.serviceUrl = "Sitefinity/Services/Content/AlbumService.svc/folders/";
        return (this);
    }

    LibrariesSelector.prototype = {

        resolveUrlFunc: function (options) {
            if (options.Id) {
                return String.format("{0}/{1}/", this.getServiceUrl(), options.Id);
            }
            return this.getServiceUrl();
        },

        parameterMapFunc: function (options) {
            var that = this;
            var queryObj = {
                sortExpression: "Title ASC",
                provider: this.providerName
            };

            if (this.isSearchRequest()) {
                queryObj.skip = 0;
                queryObj.take = 20;
                queryObj.hierarchyMode = false;
                queryObj.filter = String.format("Title.StartsWith(\"{0}\")", that.viewModel.searchValue);
            } else {
                queryObj.hierarchyMode = true;
            }

            return queryObj;
        },
        validateTranslations: function (nodes, treeView) {
        }
    };

    LibrariesSelector.prototype = $.extend(Object.create(HierarchicalContentSelector.prototype), LibrariesSelector.prototype);
    return (LibrariesSelector);
});