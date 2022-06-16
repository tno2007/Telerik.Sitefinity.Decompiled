Type.registerNamespace("Telerik");
Type.registerNamespace("Telerik.Sitefinity");
Type.registerNamespace("Telerik.Sitefinity.Web");
Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.RadBreadCrumb = function(element) {
    Telerik.Sitefinity.Web.UI.RadBreadCrumb.initializeBase(this, [element]);
    this._list = [];
}

Telerik.Sitefinity.Web.UI.RadBreadCrumb.prototype =
{
    //From the server the clientStateFieldID is supplied, so a fake prop is implemented to prevent exception
    get_clientStateFieldID: function() {
    },
    set_clientStateFieldID: function() {
    },

    // Bind and unbind to click event.
    add_click: function(handler) {
        this.get_events().addHandler("click", handler);
    },

    remove_click: function(handler) {
        this.get_events().removeHandler("click", handler);
    },

    _raiseClick: function(args) {
        var h = this.get_events().getHandler("click");
        if (h) h(this, args);
    },

    initialize: function() {
        Telerik.Sitefinity.Web.UI.RadBreadCrumb.callBaseMethod(this, 'initialize');

        //Attach to click of content area	   
        this._registerMouseHandlers(true);
    },

    _registerMouseHandlers: function(attachEvent) {
        var targetElement = this.get_element();
        if (true == attachEvent) {
            var handlers =
                {
                    click: this._onMouseClick
                };
            $addHandlers(targetElement, handlers, this);
        }
        else $clearHandlers(targetElement);
    },

    dispose: function() {
        this._clear();
        this._registerMouseHandlers(false);
        Telerik.Sitefinity.Web.UI.RadBreadCrumb.callBaseMethod(this, 'dispose');
    },

    _onMouseClick: function(e) {
        //Get clicked index
        var index = this._getClickedIndex(e);

        //Get correspoding object            
        var obj = this._list[index];

        //Raise event
        this._raiseClick(obj);

        //Cancel the event to avoid problems
        return $telerik.cancelRawEvent(e);
    },

    _getClickedIndex: function(e) {
        var target = e.target;
        if (!target || target.tagName != "A") return -1;
        //Determine which is the corresponding element in the array
        var links = this.get_element().getElementsByTagName("A");
        for (var i = 0; i < links.length; i++) {
            if (links[i] == target) return i;
        }
        return -1;
    },

    _clear: function() {
        this.get_element().innerHTML = "&nbsp;";
        this._list = [];
    },

    //The last item should not be a navigable link, but simple text
    _addBreadcrumb: function(text, isLast) {

        var parent = this.get_element();
        var domLink = null;
        if (isLast) {
            domLink = document.createElement("SPAN");
        }
        else {
            domLink = document.createElement("A");
            domLink.href = "javascript:void(0);";
        }

        //Set text and append to parent
        domLink.innerHTML = text;
        parent.appendChild(domLink);

        //Do not display this if it is the last element of the array
        if (!isLast) {
            var arrow = document.createElement("SPAN");
            arrow.innerHTML = "&nbsp;> ";
            arrow.className = "bcArrow";
            parent.appendChild(arrow);
        }
    },


    set_list: function(array) {
        //Clear old crumbs
        this._clear();
        //Set new data
        this._list = array;
        //Display new ones
        for (var i = 0; i < array.length; i++) {
            var item = array[i];
            this._addBreadcrumb(item.text, i >= array.length - 1); //isLast
        }
    }
};

//Not a RadWebControl - simpler - does not require Core.js
Telerik.Sitefinity.Web.UI.RadBreadCrumb.registerClass('Telerik.Sitefinity.Web.UI.RadBreadCrumb', Sys.UI.Control);