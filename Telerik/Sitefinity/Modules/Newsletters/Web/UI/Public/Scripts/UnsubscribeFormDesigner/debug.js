﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public");

// ------------------------------------------------------------------------
// UnsubscribeFormDesigner class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner.initializeBase(this, [element]);

    this._htmlMergeTagSelector = null;
    this._htmlTextControl = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner.callBaseMethod(this, 'initialize');

        this._htmlMergeTagSelectedDelegate = Function.createDelegate(this, this._htmlMergeTagSelectedHandler);
        this.get_htmlMergeTagSelector().add_tagSelected(this._htmlMergeTagSelectedDelegate);
    },
    dispose: function () {
         if (this._htmlMergeTagSelectedDelegate) {
            if (this.get_htmlMergeTagSelector()) {
                this.get_htmlMergeTagSelector().remove_tagSelected(this._htmlMergeTagSelectedDelegate);
            }
            delete this._htmlMergeTagSelectedDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner.callBaseMethod(this, 'dispose');
    },
    /* ************************* public methods ************************* */
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.MessageBody = this.get_htmlTextControl().get_value();
    },

    refreshUI: function () {
        var controlData = this.get_controlData();
        this.get_htmlTextControl().set_value(controlData.MessageBody);
    },

    /* ************************* private methods ************************* */

    _htmlMergeTagSelectedHandler: function (sender, args) {
        this.get_htmlTextControl()._editControl.pasteHtml(args.MergeTag);
    },

    /* ************************* properties ************************* */

    // gets the reference to the merge tag selector
    get_htmlMergeTagSelector: function () {
        return this._htmlMergeTagSelector;
    },
    // sets the reference to the merge tag selector
    set_htmlMergeTagSelector: function (value) {
        this._htmlMergeTagSelector = value;
    },
    // gets the reference to the html merge control
    get_htmlTextControl: function () {
        return this._htmlTextControl;
    },
    //sets the reference to the html merge control
    set_htmlTextControl: function (value) {
        this._htmlTextControl = value;
    }
};
Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.UnsubscribeFormDesigner', Telerik.Sitefinity.Modules.Newsletters.Web.UI.Public.SubscriptionsDesignerBase);