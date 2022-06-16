﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

/* Newsletters content block designer class */

Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner = function (element) {
    this._htmlMergeTagSelector = null;
    this._contentMergeTagSelector = null;
    this._tagSelectedDelegate = null;
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner.prototype = {

    // set up 
    initialize: function () {

        this._tagSelectedDelegate = Function.createDelegate(this, this._handleTagSelected);
        this._htmlMergeTagSelector.add_tagSelected(this._tagSelectedDelegate);
        if (this._contentMergeTagSelector) {
            this._contentMergeTagSelector.add_tagSelected(this._tagSelectedDelegate);
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._tagSelectedDelegate) {
            if (this._htmlMergeTagSelector) {
                this._htmlMergeTagSelector.remove_tagSelected(this._tagSelectedDelegate);
            }
            if (this._contentMergeTagSelector) {
                this._contentMergeTagSelector.remove_tagSelected(this._contentMergeTagSelector);
            }
            delete this._tagSelectedDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    /* *************************** private methods *************************** */

    _handleTagSelected: function (sender, args) {
        this.get_htmlEditor()._editControl.pasteHtml(args.MergeTag);
    },

    /* *************************** properties *************************** */
    get_htmlMergeTagSelector: function () {
        return this._htmlMergeTagSelector;
    },
    set_htmlMergeTagSelector: function (value) {
        this._htmlMergeTagSelector = value;
    },
    get_contentMergeTagSelector: function () {
        return this._contentMergeTagSelector;
    },
    set_contentMergeTagSelector: function (value) {
        this._contentMergeTagSelector = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersContentBlockDesigner', Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerBase);