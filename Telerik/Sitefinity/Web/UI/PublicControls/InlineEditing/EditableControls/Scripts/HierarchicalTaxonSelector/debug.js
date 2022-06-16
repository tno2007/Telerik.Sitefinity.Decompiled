define(["HierarchicalContentSelector", "text!HierarchicalTaxonSelectorDataTemplate!strip"], function (HierarchicalContentSelector, dataTemplate) {
    function HierarchicalTaxonSelector(options) {
        
        HierarchicalContentSelector.call(this, options);
        this.serviceUrl = "Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
        this.taxonomyId = null;
        return (this);
    }

    HierarchicalTaxonSelector.prototype = {

        init: function () {
            this.dataTemplate = dataTemplate;
            HierarchicalContentSelector.prototype.init.call(this);
        },
        parameterMapFunc: function (options) {
            var queryObj = {
                sortExpression: "Title ASC"
            };
            if (this.isSearchRequest()) {
                queryObj.take = 50;
                queryObj.filter = this.generateFilterExpression("Title", this.viewModel.searchValue);
                queryObj.hierarchyMode = false;
                queryObj.mode = "TitlePath";
            } else {
                //queryObj.mode = "Simple";
                queryObj.hierarchyMode = true;
            }

            return queryObj;
        },

        resolveUrlFunc: function (options) {
            if (options.Id) {
                return String.format("{0}/subtaxa/{1}/", this.getServiceUrl(), options.Id);
            }
            return String.format("{0}/{1}/", this.getServiceUrl(), this.taxonomyId);
        },
        validateTranslations: function (nodes, treeView) {
        }
    };
    HierarchicalTaxonSelector.prototype = $.extend(Object.create(HierarchicalContentSelector.prototype), HierarchicalTaxonSelector.prototype);
    return (HierarchicalTaxonSelector);
});