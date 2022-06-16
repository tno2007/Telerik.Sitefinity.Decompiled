"use strict";

(function () {
    var LOAD_EVENT = "sf-load-tooltips";

    window.onload = function () {
        var event;
        if(typeof(Event) === 'function') {
            event = new Event(LOAD_EVENT);
        }else{
            event = document.createEvent('Event');
            event.initEvent(LOAD_EVENT, true, true);
        }

        window.dispatchEvent(event);
    };
}());