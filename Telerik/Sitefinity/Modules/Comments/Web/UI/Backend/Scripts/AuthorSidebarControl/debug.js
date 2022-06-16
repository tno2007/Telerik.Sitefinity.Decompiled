Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl.initializeBase(this, [element]);

    this._element = element;
    this._dataItem = null;

    this._clientLabelManager = null;
    this._commentStatusLabel = null;
    this._commentAgeLabel = null;
    this._authorPanel = null;
    this._authorLink = null;
    this._authorAvatarImage = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl.callBaseMethod(this, "initialize");
       // this._fillElements();
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl.callBaseMethod(this, "dispose");
    },


    /* --------------------  public methods ----------- */
    //reset all fields
    reset: function () {
        if (this.get_commentStatusLabel()) {
            this.get_commentStatusLabel().innerHTML = "";
        }
        if (this.get_commentAgeLabel()) {
            this.get_commentAgeLabel().innerHTML = "";
        }
        if (this.get_authorLink()) {
            this.get_authorLink().innerHTML = "";
        }
        if (this.get_authorAvatarImage()) {
            this.get_authorAvatarImage().src = "";
        }
    },

    /* -------------------- properties ---------------- */


    //fill all elements
    fillElements: function () {
        if (this.get_dataItem() && this.get_commentStatusLabel()) {
            if (this.get_dataItem().Status === "Spam") {
                this.get_commentStatusLabel().innerHTML = this.get_clientLabelManager().getLabel("CommentsResources", "Spam");
            }
            else if (this.get_dataItem().Status === "Published") {
                this.get_commentStatusLabel().innerHTML = this.get_clientLabelManager().getLabel("CommentsResources", "Published");
            }
            else if (this.get_dataItem().Status === "WaitingForApproval") {
                this.get_commentStatusLabel().innerHTML = this.get_clientLabelManager().getLabel("CommentsResources", "WaitingApproval");
            }
            else if (this.get_dataItem().Status === "Hidden") {
                this.get_commentStatusLabel().innerHTML = this.get_clientLabelManager().getLabel("CommentsResources", "Hidden");
            }
        }

        if (this.get_dataItem().DateCreated && this.get_commentAgeLabel()) {
            this.get_commentAgeLabel().innerHTML = this.get_age(this.get_dataItem().DateCreated, new Date() );
        }

        if (this.get_authorLink()) {
            this.get_authorLink().innerHTML = this.get_dataItem().Name;
        }

        if (this.get_authorAvatarImage()) {
            this.get_authorAvatarImage().src = this.get_dataItem().ProfilePictureThumbnailUrl;
        }

    },

    //calculate the time interval between two dates
    get_age: function (d1, d2) {
        var diff = d2 - new Date(d1),  milliseconds, seconds, minutes, hours, days;
        diff = (diff - (milliseconds = diff % 1000)) / 1000;
        diff = (diff - (seconds = diff % 60)) / 60;
        diff = (diff - (minutes = diff % 60)) / 60;
        days = (diff - (hours = diff % 24)) / 24;
        var age= "";
        if (days !== 0)
            age=  String.format( this.get_clientLabelManager().getLabel("CommentsResources", "PostedDaysAgo") , days);
        else if(hours!==0)
            age = String.format(this.get_clientLabelManager().getLabel("CommentsResources", "PostedHoursAgo"), hours);
        else if (minutes !== 0)
            age = String.format(this.get_clientLabelManager().getLabel("CommentsResources", "PostedMinutesAgo"), minutes);
        return age;
    },

    get_dataItem: function () {
        return this._dataItem;
    },

    set_dataItem: function (value) {
        this._dataItem = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_commentStatusLabel: function () {
        return this._commentStatusLabel;
    },

    set_commentStatusLabel: function (value) {
        this._commentStatusLabel = value;
    },

    get_commentAgeLabel: function () {
        return this._commentAgeLabel;
    },

    set_commentAgeLabel: function (value) {
        this._commentAgeLabel = value;
    },

    get_authorPanel: function () {
        return this._authorPanel;
    },

    set_authorPanel: function (value) {
        this._authorPanel = value;
    },

    get_authorLink: function () {
        return this._authorLink;
    },

    set_authorLink: function (value) {
        this._authorLink = value;
    },

    //get_postAuthorRoleLabel: function () {
    //    return this._postAuthorRoleLabel;
    //},

    //set_postAuthorRoleLabel: function (value) {
    //    this._postAuthorRoleLabel = value;
    //},

    get_authorAvatarImage: function () {
        return this._authorAvatarImage;
    },

    set_authorAvatarImage: function (value) {
        this._authorAvatarImage = value;
    }

    //get_postAuthorPostsLabel: function () {
    //    return this._postAuthorPostsLabel;
    //},

    //set_postAuthorPostsLabel: function (value) {
    //    this._postAuthorPostsLabel = value;
    //},

    //get_postAuthorRegistrationDateLabel: function () {
    //    return this._postAuthorRegistrationDateLabel;
    //},

    //set_postAuthorRegistrationDateLabel: function (value) {
    //    this._postAuthorRegistrationDateLabel = value;
    //}
};

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.AuthorSidebarControl", Sys.UI.Control);