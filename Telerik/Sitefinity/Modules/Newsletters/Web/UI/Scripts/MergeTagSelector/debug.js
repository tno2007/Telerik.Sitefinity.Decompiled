Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

/* MergeTagSelecto class */

Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector = function (element) {
    this._mergeTagChoiceField = null;
    this._insertMergeTagButton = null;
    this._mustChooseMergeTagMessage = null;
    this._webServiceUrl = null;

    this._insertMergeTagButtonDelegate = null;

    Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector.prototype = {

    // set up 
    initialize: function () {

        if (this._insertMergeTagButtonDelegate === null) {
            this._insertMergeTagButtonDelegate = Function.createDelegate(this, this._insertMergeTagButtonHandler);
        }
        $addHandler(this._insertMergeTagButton, 'click', this._insertMergeTagButtonDelegate);

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function () {

        if (this._insertMergeTagButtonDelegate) {
            delete this._insertMergeTagButtonDelegate;
        }

        Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */
    add_tagSelected: function (delegate) {
        this.get_events().addHandler('tagSelected', delegate);
    },
    remove_tagSelected: function (delegate) {
        this.get_events().removeHandler('tagSelected', delegate);
    },

    setMergeTags: function (mergeTags) {
        this.get_mergeTagChoiceField().clearListItems();
        for (var i = 0; i < mergeTags.length; i++) {
            this.get_mergeTagChoiceField().addListItem(mergeTags[i].ComposedTag, mergeTags[i].Title);
        }
    },

    /* *************************** private methods *************************** */
    _insertMergeTagButtonHandler: function (sender, args) {
        var mergeTag = this._mergeTagChoiceField.get_value();
        if (mergeTag == '-') {
            alert(this._mustChooseMergeTagMessage);
        } else {
            this._tagSelectedHandler(mergeTag);
        }
    },

    _tagSelectedHandler: function (mergeTag) {
        var tagSelectedArgs = { 'MergeTag': mergeTag };
        var h = this.get_events().getHandler('tagSelected');
        if (h) h(this, tagSelectedArgs);
        return tagSelectedArgs;
    },

    /* *************************** properties *************************** */
    get_mergeTagChoiceField: function () {
        return this._mergeTagChoiceField;
    },
    set_mergeTagChoiceField: function (value) {
        this._mergeTagChoiceField = value;
    },
    get_insertMergeTagButton: function () {
        return this._insertMergeTagButton;
    },
    set_insertMergeTagButton: function (value) {
        this._insertMergeTagButton = value;
    }
};

Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.MergeTagSelector', Sys.UI.Control);