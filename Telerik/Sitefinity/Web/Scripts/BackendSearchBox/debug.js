Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* BindeSearchBox class */

Telerik.Sitefinity.Web.UI.BackendSearchBox = function (element) {
    Telerik.Sitefinity.Web.UI.BackendSearchBox.initializeBase(this, [element]);
    this._textBox = null;
    this._typingDelay = null;
    this._minCharacters = null;
    this._timerId = null;
    this._searchButtonId = null;
    this._searchBoxText = "";
    this._textBoxFocusBlurDelegate = null;
    this._searchDelay = null;
}
Telerik.Sitefinity.Web.UI.BackendSearchBox.prototype = {

    // set up 
    initialize: function() {
        Telerik.Sitefinity.Web.UI.BackendSearchBox.callBaseMethod(this, "initialize");

        this._textBoxFocusBlurDelegate = Function.createDelegate(this, this._onTextBoxFocusBlur);
        $addHandler(this._textBox, "blur", this._textBoxFocusBlurDelegate);
        $addHandler(this._textBox, "focus", this._textBoxFocusBlurDelegate);

        // if searchButtonId is null, search should be performed on key storkes
        this._textBoxChangedDelegate = Function.createDelegate(this, this._onTextBoxChanged);
        $addHandler(this._textBox, 'keyup', this._textBoxChangedDelegate);

        if (this._searchButtonId != null) {
            this._searchButtonClickedDelegate = Function.createDelegate(this, this._onSearchButtonClicked);
            this._searchButton = $get(this._searchButtonId);
            $addHandler(this._searchButton, 'click', this._searchButtonClickedDelegate);
        }

        this._searchDelegate = Function.createDelegate(this, this._raiseSearch);
    },

    // tear down
    dispose: function() {
        if (this._textBoxChangedDelegate) {
            delete this._textBoxChangedDelegate;
        }
        if (this._searchButtonClickedDelegate) {
            delete this._searchButtonClickedDelegate;
        }

        if (this._textBoxFocusBlurDelegate) {
            delete this._textBoxFocusBlurDelegate;
        }
        Telerik.Sitefinity.Web.UI.BackendSearchBox.callBaseMethod(this, "dispose");
    },

    clearSearchBox: function() {
        this._textBox.value = '';
    },

    focusSearchBox: function() {
        this._textBox.focus();
    },
    // event handler fired each time user enters something in the search box
    _onTextBoxChanged: function(e) {
        // if user pressed enter and search mode is with the button, start the search
        if (this._searchButtonId != null) {
            var characterCode;
            if (e && e.keyCode) {
                characterCode = e.keyCode;
            } else {
                characterCode = e.charCode;
            }

            if (characterCode == 13) {
                this._raiseSearch();
            }
        } else {
            //ignoring the default search box text
            if (this._textBox.value == this._searchBoxText) {
                return;
            }
            if (this._textBox.value.length >= this._minCharacters || this._textBox.value.length == 0) {
                if (this._timerId != null) {
                    clearTimeout(this._timerId);
                }
                this._timerId = setTimeout(this._searchDelegate, this._searchDelay);
            }
        }

    },

    // when foucsed the default text is cleared and when blured the default text is returned if the textbox is empty
    _onTextBoxFocusBlur: function() {
        if (this._textBox.value == this._searchBoxText) {
            this._textBox.value = '';
        }
        else if (this._textBox.value == '' && this._searchBoxText != null) {
            this._textBox.value = this._searchBoxText;
        }
    },

    // event fired when search button is clicked, if search box in this mode
    _onSearchButtonClicked: function() {
        this._raiseSearch();
    },

    /* ----------------------- events ----------------------- */
    add_search: function(delegate) {
        /// <summary>
        /// If automatic search is disabled, then subscribing to this event
        /// will enable you to do the search yourself
        /// </summary>
        this.get_events().addHandler("search", delegate);
    },
    remove_search: function(delegate) {
        this.get_events().removeHandler("search", delegate);
    },

    /* ----------------------- event raising ----------------------- */
    _raiseSearch: function() {
        var handler = this.get_events().getHandler("search");
        if (handler) {
            var args = new Telerik.Sitefinity.Web.UI.SearchEventArgs(this._textBox.value);
            handler(this, args);
        }
    },

    /* ----------------------- properties ----------------------- */

    get_textBox: function() {
        return this._textBox;
    },
    set_textBox: function(value) {
        if (this._textBox != value) {
            this._textBox = value;
            this.raisePropertyChanged("textBox");
        }
    }
};

Telerik.Sitefinity.Web.UI.BackendSearchBox.registerClass('Telerik.Sitefinity.Web.UI.BackendSearchBox', Sys.UI.Control);

//-----------------------------------------------------------------------------
// Event arguments
//-----------------------------------------------------------------------------

Telerik.Sitefinity.Web.UI.SearchEventArgs = function(query) {
    /// <summary>Event arguments for the SearchBox' search event</summary>
    Telerik.Sitefinity.Web.UI.SearchEventArgs.initializeBase(this);
    this._query = query;
}
Telerik.Sitefinity.Web.UI.SearchEventArgs.prototype = {
    get_query: function() { return this._query; }
}
Telerik.Sitefinity.Web.UI.SearchEventArgs.registerClass("Telerik.Sitefinity.Web.UI.SearchEventArgs", Sys.EventArgs);