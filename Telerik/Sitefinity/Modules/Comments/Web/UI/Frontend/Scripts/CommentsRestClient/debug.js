Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend");

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient = function (serviceUrl) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient.initializeBase(this);

    if (serviceUrl.substr(serviceUrl.length - 1) != '/') {
        serviceUrl = serviceUrl + '/';
    }
    this._rootServiceUrl = serviceUrl;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient.callBaseMethod(this, 'initialize');

    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient.callBaseMethod(this, 'dispose');
    },

    //gets single comment by its key
    getComment: function (commentKey, onSuccess) {
        var requestUrl = (this.get_rootServiceUrl() + String.format("comments/{0}", commentKey));

        jQuery.ajax({
            type: 'GET',
            accepts: {
                text: "application/json"
            },
            url: requestUrl,
            dataType: "text",
            dataFilter: function (data) {
                return Sys.Serialization.JavaScriptSerializer.deserialize(data);
            },
            cache: false,
            processData: false,
            success: onSuccess
        });
    },

    
    //gets comments filtered by passed parameters
    getComments: function (threadKey, language, olderThan, newerThan, sortDescending, take, onSuccess, onError, onComplete) {
        var params = this._getQueryString(threadKey, language, olderThan, newerThan, sortDescending, take);

        var requestUrl = this.get_rootServiceUrl() + "comments/" + params;

        jQuery.ajax({
            type: 'GET',
            url: requestUrl,
            dataType: "text",
            dataFilter: function (data) {
                return Sys.Serialization.JavaScriptSerializer.deserialize(data);
            },
            accepts: {
                text: "application/json"
            },
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //gets public comment count for a thread
    getCommentsCount: function (threadKey, status, onSuccess) {
        var getCountUrl = this.get_rootServiceUrl() + String.format("comments/count?ThreadKey={0}", threadKey)
        if (status)
            getCountUrl += "&Status=" + status;

        jQuery.ajax({
            type: 'GET',
            url: getCountUrl,
            contentType: "application/json",
            cache: false,
            async: false,
            processData: false,
            success: onSuccess
        });
    },

    //creates new comment
    createComment: function (item, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'POST',
            url: this.get_rootServiceUrl() + "comments",
            contentType: "application/json",
            data: Telerik.Sitefinity.JSON.stringify(item),
            dataType: "text",
            dataFilter: function (data) {
                return Sys.Serialization.JavaScriptSerializer.deserialize(data);
            },
            accepts: {
                text: "application/json"
            },
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //updates existing comment.
    updateComment: function (item, onSuccess, onError, onComplete) {
        jQuery.ajax({
            type: 'PUT',
            url: this.get_rootServiceUrl() + "comments/",
            contentType: "application/json",
            accepts: {
                text: "application/json"
            },
            data: Telerik.Sitefinity.JSON.stringify(item),
            dataType: "text",
            dataFilter: function (data) {
                return Sys.Serialization.JavaScriptSerializer.deserialize(data);
            },
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //updates the status of multiple comments
    updateComments: function (commentKeys, status, onSuccess, onError, onComplete) {
        var requestUrl = this.get_rootServiceUrl() + "comments/list/";

        jQuery.ajax({
            type: 'PUT',
            url: requestUrl,
            contentType: "application/json",
            data: Telerik.Sitefinity.JSON.stringify({ Key: commentKeys, Status: status }),
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //deletes existing comment
    deleteComment: function (commentKey, onSuccess, onError, onComplete) {
        var requestUrl = this.get_rootServiceUrl() + "comments/" + commentKey;

        jQuery.ajax({
            type: 'DELETE',
            url: requestUrl,
            contentType: "application/json",
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //deletes multiple comments
    deleteComments: function (commentKeys, onSuccess, onError, onComplete) {
        var requestUrl = this.get_rootServiceUrl() + "comments/list/";

        jQuery.ajax({
            type: 'DELETE',
            url: requestUrl,
            contentType: "application/json",
            data: Telerik.Sitefinity.JSON.stringify({ Key: commentKeys }),
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //gets threads by filter
    getThreads: function (item, siteId, onSuccess, onError, onComplete) {
        var url = this.get_rootServiceUrl() + "threads/filter";

        if (siteId)
            url += "?sf_site=" + siteId;

        jQuery.ajax({
            type: 'POST',
            url: url,
            contentType: "application/json",
            data: Telerik.Sitefinity.JSON.stringify(item),
            dataType: "text",
            dataFilter: function (data) {
                return Sys.Serialization.JavaScriptSerializer.deserialize(data);
            },
            async: false,
            accepts: {
                text: "application/json"
            },
            cache: false,
            processData: false,
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //gets captcha data
    getCaptcha: function (onSuccess, onError, onComplete) {
        var requestUrl = this.get_rootServiceUrl();

        jQuery.ajax({
            type: 'GET',
            url: requestUrl + "captcha",
            contentType: "application/json",
            cache: false,
            processData: false,
            accepts: {
                text: "application/json"
            },
            success: onSuccess,
            error: onError,
            complete: onComplete
        });
    },

    //formats the parameters that would be send to the rest service for get comments method
    _getQueryString: function (threadKey, language, olderThan, newerThan, sortDescending, take) {
        var params = "?";
        if (threadKey)
            params += String.format("ThreadKey={0}&", threadKey);
        if (language)
            params += String.format("Language={0}&", language);
        if (olderThan)
            params += String.format("OlderThan={0}&", olderThan);
        if (newerThan)
            params += String.format("NewerThan={0}&", newerThan);
        if (sortDescending)
            params += String.format("SortDescending={0}&", sortDescending);
        if (take)
            params += String.format("Take={0}", take);

        var lastChar = params.charAt(params.length - 1);
        if (lastChar === '&' || lastChar === '?')
            params.substring(0, params.length - 1);

        return params;
    },

         
    get_rootServiceUrl: function () {
        return this._rootServiceUrl;
    },

    set_rootServiceUrl: function (value) {
        this._rootServiceUrl = value;
        if (this._rootServiceUrl.substr(-1) != '/') {
            this._rootServiceUrl = this._rootServiceUrl + '/';
        }
    }
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsRestClient", Sys.Component);