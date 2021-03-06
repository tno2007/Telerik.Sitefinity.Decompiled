Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.SortDirection = function() { };
Telerik.Sitefinity.Web.UI.SortDirection.prototype =
{
    Ascending: 0, 
    Descending: 1    
}
Telerik.Sitefinity.Web.UI.SortDirection.registerEnum("Telerik.Sitefinity.Web.UI.SortDirection");

Telerik.Sitefinity.Web.UI.CommentStatus = function() { };
Telerik.Sitefinity.Web.UI.CommentStatus.prototype =
{
    Invisible: 0,
    Visible: 1,
    Accepted: 2,
    Spam: 3
}
Telerik.Sitefinity.Web.UI.CommentStatus.registerEnum("Telerik.Sitefinity.Web.UI.CommentStatus");

Telerik.Sitefinity.Web.UI.CommentsList = function(element) {
    Telerik.Sitefinity.Web.UI.CommentsList.initializeBase(this, [element]);

    this._serviceBaseUrl = null;
    this._contentType = null;
    this._contentBaseType = null;
    this._commentType = null;
    this._binderId = null;
    this._binder = null;
    this._manager = null;
    this._providerName = null;
    this._securityProviderName = null;
    this._sortDirection = null;
    this._defaultFilter = null;
    this._bindOnSuccess = true;
    this._editCommentLink = null;
    this._noTextMessage = null;
    this._commentedItemId = null;
    this._addNewCommentClientId = null;
    this._newCommentTextClientId = null;
    this._searchClientId = null;
    this._groupPublishClientId = null;
    this._groupHideAndSpamClientId = null;
    this._groupDeleteClientId = null;
    this._groupHideClientId = null;

    // delegates
    this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
    this._onItemCommandDelegate = Function.createDelegate(this, this._onItemCommand);
    this._onDataBoundDelegate = Function.createDelegate(this, this._onDataBound);
}
Telerik.Sitefinity.Web.UI.CommentsList.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function() {
        Telerik.Sitefinity.Web.UI.CommentsList.callBaseMethod(this, 'initialize');

        this._manager = new Telerik.Sitefinity.Data.ClientManager();
        Telerik.Sitefinity.Web.UI.CommentsList.Instance = this;

        Sys.Application.add_load(this._onLoadDelegate);
    },
    dispose: function() {
        this._onLoadDelegate = null;
        this._onItemCommandDelegate - null;
        this._onDataBoundDelegate = null;

        Telerik.Sitefinity.Web.UI.CommentsList.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // public functions
    // ------------------------------------------------------------------------

    dataBind: function() {
        this._binder.DataBind();
    },
    deleteComment: function(idFilter) {
        var params = this._getDefaultParameters();
        params["filter"] = idFilter;
        this._manager.InvokeDelete(this._serviceBaseUrl, params, null,
            this._onOperationSuccess, this._onOperationFailure, this);
    },
    changeStatus: function(idFilter, status) {
        var params = this._getDefaultParameters();
        params["idFilter"] = idFilter;
        this._manager.InvokePut(this._serviceBaseUrl + "/status/", params, null, status,
            this._onOperationSuccess, this._onOperationFailure, this);
    },
    makeFavourite: function(commentId) {
        var params = this._getDefaultParameters();
        this._manager.InvokePut(this._serviceBaseUrl + "/favourite/", params, null, commentId,
            this._onOperationSuccess, this._onOperationFailure, this);
    },
    blockUser: function(blockKind, whatToBlock) {
        var params = []; //this._getDefaultParameters();
        params["contentType"] = this._commentType;
        params["securityProvider"] = this._securityProviderName != null ? this._securityProviderName : "";
        this._manager.InvokePut(this._serviceBaseUrl + "/block/" + blockKind + "/",
            params, null, whatToBlock,
            this._onOperationSuccess, this._onOperationFailure, this);
    },
    unblockUser: function(blockKind, whatToBlock) {
        var params = [];
        params["contentType"] = this._commentType;
        params[blockKind] = whatToBlock;
        if (this._securityProviderName) {
            params["securityProvider"] = this._securityProviderName;
        }

        this._manager.InvokeDelete(this._serviceBaseUrl + "/block/" + blockKind + "/",
            params, null, this._onOperationSuccess, this._onOperationFailure, this);
    },
    createComment: function(newCommentText) {
        if (newCommentText && typeof (newCommentText) == typeof ("") && newCommentText.length > 0) {
            var params = this._getDefaultParameters();
            var data = new Object();
            data["Text"] = newCommentText;
            data["ContentId"] = this._commentedItemId;
            this._manager.InvokePut(this._serviceBaseUrl + "/new/", params, null, data,
            this._onOperationSuccess, this._onOperationFailure, this);
            this.set_newCommentText("");
        } else {
            alert(this._noTextMessage);
        }
    },
    updateComment: function(comment) {
        var params = [];
        if (this._providerName) {
            params["provider"] = this._providerName;
        }
        if (this._commentType) {
            params["itemType"] = this._commentType;
        }
        params[this._contentBaseType] = this._contentType;
        this._manager.InvokePut(this._serviceBaseUrl + "/update/", params, null, comment,
            this._onOperationSuccess, this._onOperationFailure, this);
    },

    // ------------------------------------------------------------------------
    // private functions
    // ------------------------------------------------------------------------
    _onLoad: function(sender, args) {
        this._binder = $find(this._binderId);
        this._binder.set_unescapeHtml(true);
        var params = this._getDefaultParameters(this._binder.get_urlParams());
        var filter = "";
        if (typeof (this._commentedItemId) != "undefined" &&
            this._commentedItemId != null &&
            this._commentedItemId != this._binder.GetEmptyGuid()) {
            filter = "CommentedItem.Id = " + this._commentedItemId;
        }
        if (this._defaultFilter) {
            filter += " And (" + this._defaultFilter + ")";
        }
        params["filter"] = filter;
        this._binder.set_urlParams(params);

        if (this._sortDirection == Telerik.Sitefinity.Web.UI.SortDirection.Ascending) {
            this._binder.set_sortExpression("DateCreated asc");
        }
        else {
            this._binder.set_sortExpression("DateCreated desc");
        }

        this._binder.add_onItemCommand(this._onItemCommandDelegate);
        this._binder.add_onDataBound(this._onDataBoundDelegate);

        var self = this;
        // bind to click events with jQuery
        $("#" + this._addNewCommentClientId).each(function() {
            var elem = $(this);
            elem.bind("click", self, self._onAddNewCommentClicked);
            elem.on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });
        $("#" + this._searchClientId).each(function() {
            var elem = $(this);
            elem.bind("click", self, self._onSearchClicked);
            elem.on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });
        $("#" + this._groupPublishClientId).each(function() {
            var elem = $(this);
            elem.bind("click", self, self._onGroupPublishClicked);
            elem.on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });
        $("#" + this._groupHideAndSpamClientId).each(function() {
            var elem = $(this);
            elem.bind("click", self, self._onGroupHideAndSpamClicked);
            elem.on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });
        $("#" + this._groupDeleteClientId).each(function() {
            var elem = $(this);
            elem.bind("click", self, self._onGroupDeleteClicked);
            elem.on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });
        $("#" + this._groupHideClientId).each(function() {
            var elem = $(this);
            elem.bind("click", self, self._onGroupHideClicked);
            elem.on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });

        this._binder.DataBind();
    },
    _onDataBound: function(sender, args) {
        $(".more").each(function() {
            $(this).click(function(jEvent) {
                jEvent.preventDefault();
                $(this).next("ul").toggle();
            });
            $(this).on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });
        });
    }
    ,
    _onItemCommand: function(sender, args) {
        var commandName = args.get_commandName();
        var dataItem = args.get_dataItem();
        switch (commandName) {
            case "delete":
                this.deleteComment("Id = " + dataItem.Comment.Id);
                break;
            case "hide":
                this.changeStatus("Id = " + dataItem.Comment.Id, "Invisible");
                break;
            case "spam":
                this.changeStatus("Id = " + dataItem.Comment.Id, "Spam");
                break;
            case "favourite":
                this.makeFavourite("Id = " + dataItem.Comment.Id);
                break;
            case "blockIp":
                this.blockUser("ip", dataItem.Comment.IpAddress);
                break;
            case "blockEmail":
                this.blockUser("id", dataItem.Comment.Owner);
                break;
            case "unblockEmail":
                this.unblockUser("id", dataItem.Comment.Owner);
                break;
            case "unblockIp":
                this.unblockUser("ip", dataItem.Comment.IpAddress);
                break;
            default:
                break;
        }
    },
    _getDefaultParameters: function(params) {
        if (!params) {
            params = [];
        }
        params["contentType"] = this._contentType != null ? this._contentType : "";
        params["provider"] = this._providerName != null ? this._providerName : "";
        params[this._contentBaseType] = this._contentType;
        if (this._commentType) {
            params["itemType"] = this._commentType;
        }
        return params;
    },
    _onOperationSuccess: function(commentsList, successData) {
        if (commentsList.get_bindOnSuccess() == true) {
            commentsList.get_binder().DataBind();
        }
    },
    _onOperationFailure: function(errorData) {
        if (typeof (errorData.Detail) != "undefined" && errorData.Detail.length > 0) {
            alert(errorData.Detail);
        } else {
            alert("" + errorData);
        }
    },
    _onAddNewCommentClicked: function(jEvent) {
        var commentsList = jEvent.data;
        var text = commentsList.get_newCommentText();
        commentsList.createComment(text);
    },
    _onSearchClicked: function(jEvent) {
        var commentsList = jEvent.data;
        alert("on search");
    },
    _onGroupPublishClicked: function(jEvent) {
        var commentsList = jEvent.data;
        var selectedItems = commentsList.get_binder().get_selectedItems();
        if (selectedItems.length > 0) {
            var idFilter = commentsList._getIdFilter(selectedItems);
            commentsList.changeStatus(idFilter, "Accepted");
        }
    },
    _onGroupHideAndSpamClicked: function(jEvent) {
        var commentsList = jEvent.data;
        var selectedItems = commentsList.get_binder().get_selectedItems();
        if (selectedItems.length > 0) {
            var idFilter = commentsList._getIdFilter(selectedItems);
            commentsList.changeStatus(idFilter, "Spam");
        }
    },
    _onGroupDeleteClicked: function(jEvent) {
        var commentsList = jEvent.data;
        var selectedItems = commentsList.get_binder().get_selectedItems();
        if (selectedItems.length > 0) {
            var idFilter = commentsList._getIdFilter(selectedItems);
            commentsList.deleteComment(idFilter);
        }
    },
    _onGroupHideClicked: function(jEvent) {
        var commentsList = jEvent.data;
        var selectedItems = commentsList.get_binder().get_selectedItems();
        if (selectedItems.length > 0) {
            var idFilter = commentsList._getIdFilter(selectedItems);
            commentsList.changeStatus(idFilter, "Invisible");
        }
    },
    _getIdFilter: function(listOfItems) {
        var idFilter = "";
        for (var i = 0, len = listOfItems.length; i < len; i++) {
            idFilter += "Id = " + listOfItems[i].Comment.Id;
            if (i + 1 < listOfItems.length) {
                idFilter += " Or ";
            }
        }
        return idFilter;
    },

    // ------------------------------------------------------------------------
    // properties
    // ------------------------------------------------------------------------
    get_newCommentText: function() {
        var radEditor = $find(this._newCommentTextClientId);
        if (radEditor) {
            var html = radEditor.get_html(true);
            return html;
        }
        else {
            return "";
        }
    },
    set_newCommentText: function(value) {
        var radEditor = $find(this._newCommentTextClientId);
        if (radEditor) {
            if (value == "")
                value = "&nbsp;";
            radEditor.set_html(value);
            radEditor.fire("SelectAll");
        }
    },
    get_providerName: function() {
        return this._providerName;
    },
    set_providerName: function(value) {
        this._providerName = value;
    },
    get_bindOnSuccess: function() {
        return this._bindOnSuccess;
    },
    set_bindOnSuccess: function(value) {
        this._bindOnSuccess = value;
    },
    get_serviceBaseUrl: function() {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function(value) {
        this._serviceBaseUrl = value;
    },
    get_contentType: function() {
        return this._contentType;
    },
    set_contentType: function(value) {
        this._contentType = value;
    },
    get_binderId: function() {
        return this._binderId;
    },
    set_binderId: function(value) {
        this._binderId = value;
    },
    get_binder: function() {
        return $find(this._binderId);
    },
    get_clientManager: function() {
        return this._clientManager;
    },
    get_addNewCommentClientId: function() {
        return this._addNewCommentClientId;
    },
    set_addNewCommentClientId: function(value) {
        this._addNewCommentClientId = value;
    },
    get_newCommentTextClientId: function() {
        return this._newCommentTextClientId;
    },
    set_newCommentTextClientId: function(value) {
        this._newCommentTextClientId = value;
    },
    get_searchClientId: function() {
        return this._searchClientId;
    },
    set_searchClientId: function(value) {
        this._searchClientId = value;
    },
    get_groupPublishClientId: function() {
        return this._groupPublishClientId;
    },
    set_groupPublishClientId: function(value) {
        this._groupPublishClientId = value;
    },
    get_groupHideAndSpamClientId: function() {
        return this._groupHideAndSpamClientId;
    },
    set_groupHideAndSpamClientId: function(value) {
        this._groupHideAndSpamClientId = value;
    },
    get_groupDeleteClientId: function() {
        return this._groupDeleteClientId;
    },
    set_groupDeleteClientId: function(value) {
        this._groupDeleteClientId = value;
    },
    get_groupHideClientId: function() {
        return this._groupHideClientId;
    },
    set_groupHideClientId: function(value) {
        this._groupHideClientId = value;
    },
    get_securityProviderName: function() {
        return this._securityProviderName;
    },
    set_securityProviderName: function(value) {
        this._securityProviderName = value;
    },
    get_commentedItemId: function() {
        return this._commentedItemId;
    },
    set_commentedItemId: function(value) {
        if (this._binder) {
            var params = this._getDefaultParameters(this._binder.get_urlParams());
            if (typeof (this._commentedItemId) != "undefined" &&
                this._commentedItemId != null &&
                this._commentedItemId != this._binder.GetEmptyGuid()) {
                params["filter"] = "CommentedItem.Id = " + _commentedItemId;
            }
        }
        this._commentedItemId = value;
    },
    get_sortDirection: function() {
        return this._sortDirection;
    },
    set_sortDirection: function(value) {
        this._sortDirection = value;
    },
    get_defaultFilter: function() {
        return this._defaultFilter;
    },
    set_defaultFilter: function(value) {
        this._defaultFilter = value;
    },
    get_editCommentLink: function() {
        return this._editCommentLink;
    },
    set_editCommentLink: function(value) {
        this._editCommentLink = value;
    },
    get_commentType: function() {
        return this._commentType;
    },
    set_commentType: function(value) {
        this._commentType = value;
    },
    get_contentBaseType: function() {
        return this._contentBaseType;
    },
    set_contentBaseType: function(value) {
        this._contentBaseType = value;
    }
}
Telerik.Sitefinity.Web.UI.CommentsList.registerClass('Telerik.Sitefinity.Web.UI.CommentsList', Sys.UI.Control);

function getCommentsList() {
    return Telerik.Sitefinity.Web.UI.CommentsList.Instance;
}
