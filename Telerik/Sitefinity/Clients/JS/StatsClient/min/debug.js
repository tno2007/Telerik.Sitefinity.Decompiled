// Statistics client for Sitefinity websites
StatsClient = {
    _canTrack: null,

    // Logs the page visit
    LogVisit: function (pageNodeId) {
        var that = this;
        var logVisit = function (pageNodeId) {
            if (!that._canTrack) return;

            var trackingCookieName = "sf-trckngckie"

            var trackingId = that.readCookie(trackingCookieName);
            if (!trackingId) {
                trackingId = that.generateGuid();
                that.createCookie(trackingCookieName, trackingId, 180);
            }

            var referrer = document.referrer.replace(/&/gi, "%26").replace(/\?/gi, "%3F").replace(/=/gi, "%3D");

            var xmlHttp = new XMLHttpRequest();
            xmlHttp.open("GET", that.getServiceUrl() + "?pageNodeId=" + pageNodeId + "&trackingId=" + trackingId + "&referrer=" + referrer + "&pageUrl=" + window.location.href);
            xmlHttp.send(null);
        }

        if (window.personalizationManager) {
            window.personalizationManager.addPersonalizedContentLoaded(function () {
                logVisit(pageNodeId);
            });
        } else {
            logVisit(pageNodeId);
        }
    },

    getServiceUrl: function () {
        // get the web service url
        var protocol = window.location.protocol;
        var port = window.location.port;
        var hostName = window.location.hostname;
        var sf_appPath = window.sf_appPath;
        if (sf_appPath) {
            return protocol + "//" + hostName + ":" + port + sf_appPath + "Sitefinity/Public/Services/Statistics/Log.svc/";
        }

        window.sf_appPath = "/";
        return protocol + "//" + hostName + ":" + port + window.sf_appPath + "Sitefinity/Public/Services/Statistics/Log.svc/";
    },

    createCookie: function (name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    },

    readCookie: function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    },

    updateState: function (canTrack) {
        if (!canTrack) { // delete cookie
            StatsClient.createCookie("sf-trckngckie", "", -1);
        }

        StatsClient._canTrack = canTrack;
    },

    generateGuid: function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
};

if (window.TrackingConsentManager) {
    TrackingConsentManager.addEventListener("ConsentChanged", StatsClient.updateState);
    StatsClient.updateState(TrackingConsentManager.canTrackCurrentUser());
}
else {
    StatsClient.updateState(true);
}