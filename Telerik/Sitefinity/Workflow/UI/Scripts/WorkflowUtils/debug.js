Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");
// ------------------------------------------------------------------------
//                          Global functions
// ------------------------------------------------------------------------

$workflow = Telerik.Sitefinity.Workflow.UI;

Telerik.Sitefinity.Workflow.UI.hasBeenAdded = function (currentScope, scopes) {
    /// <summary>Try compare newly added scope with already added.</summary>
    /// <param name="currentScope">added scope</param>
    /// <param name="scopes">array of scopes to compare</param>
    /// <returns>true/false</returns>

    return scopes.some(function (scope) {
        var cloneObj = Telerik.Sitefinity.cloneObject(scope);
        delete cloneObj.Id;
        if (cloneObj.Site)
            delete cloneObj.Site.Cultures;

        return Telerik.Sitefinity.compareObjects(cloneObj, currentScope);
    });
}

Telerik.Sitefinity.Workflow.UI.getPageNodeItem = function (items) {
    /// <summary>Try get page type from selected items.</summary>
    /// <param name="items">selected items</param>
    /// <returns>page node item/undefined</returns>
    if (!items)
        return;

    return Telerik.Sitefinity.find(items, function (node) {
        return node.ContentType === Telerik.Sitefinity.Workflow.UI.PAGE_NODE;
    });
}

Telerik.Sitefinity.Workflow.UI.getDataItem = function (ev, dataSource) {
    /// <summary>Try get data item from grid datasource.</summary>
    /// <param name="ev">event</param>
    /// <param name="dataSource">dataSource</param>
    /// <returns>dataItem/undefined</returns>
    var targetRow = jQuery(ev.target).closest("tr");
    var targetRowUid = targetRow.data("client-id");

    var dataItem = dataSource.getByUid(targetRowUid);

    return dataItem;
}

Telerik.Sitefinity.Workflow.UI.getActionUIName = function (action, labelManager) {
    /// <summary>Try get action name from workflow level.</summary>
    /// <param name="action">action</param>
    /// <param name="labelManager">label manager to get res</param>
    /// <returns>actionName/""</returns>
    var actionName = "";

    switch (action) {
        case $workflow.actionNames.approve:
            actionName = labelManager.getLabel("WorkflowResources", "Approval");
            break;
        case $workflow.actionNames.review:
            actionName = labelManager.getLabel("WorkflowResources", "Review");
            break;
        case $workflow.actionNames.publish:
            actionName = labelManager.getLabel("WorkflowResources", "Publishing");
            break;
    }

    return actionName;
},

Telerik.Sitefinity.Workflow.UI.PAGE_NODE = "Telerik.Sitefinity.Pages.Model.PageNode";

Telerik.Sitefinity.Workflow.UI.actionNames = {
    review: "Review",
    approve: "Approve",
    publish: "Publish"
};