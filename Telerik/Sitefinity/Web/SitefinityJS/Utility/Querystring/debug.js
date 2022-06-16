﻿/* Client-side access to querystring name=value pairs
Version 1.3
28 May 2008
	
License (Simplified BSD):
http://adamv.com/dev/javascript/qslicense.txt
*/

Type.registerNamespace("Telerik.Sitefinity.Web.SitefinityJS.Utility");

Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring = function (qs) { // optionally pass a querystring to parse
    this.params = {};
    this.keys = new Array();

    if (qs == null) qs = location.search.substring(1, location.search.length);
    if (qs.length == 0) return;

    // Turn <plus> back to <space>
    // See: http://www.w3.org/TR/REC-html40/interact/forms.html#h-17.13.4.1
    qs = qs.replace(/\+/g, ' ');
    var args = qs.split('&'); // parse out name/value pairs separated via &

    // split out each name=value pair
    for (var i = 0; i < args.length; i++) {
        var pair = args[i].split('=');
        var name = decodeURIComponent(pair[0]).toLowerCase();

        var value = (pair.length == 2)
			? decodeURIComponent(pair[1])
			: name;

        this.params[name] = value;
        this.keys[i] = name;
    }
};

Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring.prototype = {
    get: function (key, default_) {
        var value = this.params[key.toLowerCase()];
        return (value != null) ? value : default_;
    },

    contains: function (key) {
        var value = this.params[key.toLowerCase()];
        return (value != null);
    },

    set: function (key, value) {
        var lowerKey = key.toLowerCase();
        if (this.keys.indexOf(lowerKey) == -1) {
            this.keys.push(lowerKey);
        }
        this.params[lowerKey] = value;
    },

    toString: function (appendQuestionMark) {
        if (this.keys.length > 0) {
            var query = appendQuestionMark ? "?" : "";
            for (var i = 0; i < this.keys.length; i++) {
                if (i > 0)
                    query += "&";
                query += this.keys[i] + "=" + this.params[this.keys[i]];
            }
            return query;
        }
        return "";
    }
};