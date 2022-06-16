Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// global dialog variable
if (typeof dialogBase === "undefined") {
    dialogBase = null;
}
if (typeof pageLoad == "undefined") {
    pageLoad = function (sender, args) {
        if (dialogBase && !dialogBase._pageIsLoaded) {
            dialogBase._pageIsLoaded = true;
            if (dialogBase._needsResizing) {
                dialogBase.resizeToContent();
            }
        }
    }
}

// ------------------------------------------------------------------------
// DialogBase class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.AjaxDialogBase = function (element) {
    Telerik.Sitefinity.Web.UI.AjaxDialogBase.initializeBase(this, [element]);
    this._dialog = null;
    this._hostedInRadWindow = true;
    this._needsResizing = false;
    this._pageIsLoaded = false;
}
Telerik.Sitefinity.Web.UI.AjaxDialogBase.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.AjaxDialogBase.callBaseMethod(this, 'initialize');
        this._queryStringParts = this._splitQueryString(location.search);
        // when using a dialog not in rad window this will preserve the current dialog from overriding
        if (this._hostedInRadWindow) {
            dialogBase = this;
        }
        if (this._hostedInRadWindow && this.get_radWindow()) {
            // Adding the current dialog as a property of the window
            this.get_radWindow().AjaxDialog = this;
        }

        // call this, in order to avoid lazy validation
        jQuery(document.forms[0]).validate();
    },
    // Release resources before control is disposed.
    dispose: function () {
        var radWindow = this.get_radWindow();
        if (radWindow != null && typeof radWindow !== 'undefined') {
            //TP : 194869 IE throws an error when executing the bellow line
            //The surrounding try catch block fixes te problem in IE
            try {
                radWindow.remove_close(this.onWindowClose);
            } catch (e) { }
        }

        Telerik.Sitefinity.Web.UI.AjaxDialogBase.callBaseMethod(this, 'dispose');
    },
    // ------------------------------------------------------------------------
    // Public functions
    // ------------------------------------------------------------------------
    close: function (context) {
        /// <summary>Closes the dialog</summary>
        /// <param name="context">Optional argument that can give arbitary result to the dialog owner</param>
        if (this._hostedInRadWindow) {
            var wnd = this.get_radWindow();
            if (typeof (context) != "undefined") {
               wnd.close(context);
            }
            else {
                wnd.close();
            }
        }
        // if it is a jQuery dialog
        else {
            this._closeJDialog();
        }
    },

    closeWithMessage: function (message, success) {
        if (this._hostedInRadWindow) {
            var arg = { 'Message': message, 'Success': success };
            var wnd = this.get_radWindow();
            wnd.close(arg);
        }
        // if it is a jQuery dialog
        else {
            this._closeJDialog();
        }
    },

    closeAndRebind: function () {
        if (this._hostedInRadWindow) {
            var wnd = this.get_radWindow();
            wnd.close("rebind");
        }
        // if it is a jQuery dialog
        else {
            this._closeJDialog();
        }
    },

    // TODO: Maybe unite these using a command name instead?
    closeCreated: function (dataItem, context) {
        /// <summary>Closes and sets up context</summary>
        /// <param name="dataItem">Data item that was created</param>
        /// <param name="context">Context to pass to the data binder</param>
        var arg = { 'IsCreated': true, 'IsUpdated': false, "IsDuplicated": false, 'DataItem': dataItem, 'Context': context };
        this.closeDialog(arg);
    },
    closeUpdated: function (dataItem, context) {
        /// <summary>Closes and sets up context</summary>
        /// <param name="dataItem">Data item that was updated</param>
        /// <param name="context">Context to pass to the data binder</param>
        var arg = { 'IsCreated': false, 'IsUpdated': true, "IsDuplicated": false, 'DataItem': dataItem, 'Context': context };
        this.closeDialog(arg);
    },
    closeDuplicated: function (dataItem, context) {
        /// <summary>Closes and sets up context</summary>
        /// <param name="dataItem">Data item that was duplicated</param>
        /// <param name="context">Context to pass to the data binder</param>

        // is duplicating item in a different site
        // context._blankDataItem.RootId -> current site id
        // dataItem.RootId -> target site id
        var doNotRebind = context._blankDataItem.RootId !== dataItem.RootId

        var arg = { 'IsCreated': true, 'IsUpdated': false, "IsDuplicated": true, 'DoNotRebind': doNotRebind, 'DataItem': dataItem, 'Context': context };
        this.closeDialog(arg);
    },
    closeDialog: function (arg) {
        if (this._hostedInRadWindow) {
            var radWindow = this.get_radWindow();
            if (radWindow) {
                radWindow.close(arg);
            }
        }
        else {
            this._closeJDialog();
        }
    },

    getQueryValue: function (key, doNotThrow) {
        if (this._queryStringParts[key] != null) {
            return unescape(this._queryStringParts[key]);
        }
        else {
            if (doNotThrow) {
                return null;
            }
            else {
                throw Error.argumentUndefined("key");
            }
        }
    },
    //Resizes the dialog window to match the inner content size
    resizeToContent: function (oWnd) {
        if (!this._hostedInRadWindow)
            return;

        if (!oWnd) {
            oWnd = this.get_radWindow();
        }

        if (!oWnd)
            return;

        if (oWnd.isMaximized()) {
            return;
        }

        //oWnd.autoSize();
        this.resizeSelfAndParentWindow(oWnd);
    },
    resizeSelfAndParentWindow: function (oWnd) {
        if (oWnd) {
            var childWindow = oWnd;
            var parentWindow = this.getParentWindow(oWnd);

            if (!parentWindow) {
                childWindow.autoSize();
                return;
            }

            if (childWindow == parentWindow) {
                childWindow.autoSize();
                return;
            }

            //Explicitly set childWindow's modal property to false so that the parent window can resize itself properly
            //Set in ImportSubscribers.ascx

            var parentHeightBefore = parentWindow.get_height();
            var parentWidthBefore = parentWindow.get_width();

            //Browser optimization: We multiply by two here so that the parent window can resize itself
            //to be more than the expanded child window.
            parentWindow.set_height(parentHeightBefore * 2);
            parentWindow.set_width(parentWidthBefore * 2);

            childWindow.autoSize();
            parentWindow.set_height(parentHeightBefore);
            parentWindow.set_width(parentWidthBefore);
            parentWindow.autoSize();

            childWindow.remove_close(this.onWindowClose);
            childWindow.add_close(this.onWindowClose);
        }
    },
    getParentWindow: function (oWnd) {
        var browserWindowParent = oWnd.get_browserWindow().parent;
        if (typeof (browserWindowParent.GetRadWindowManager) === "function") {
            var parentWindowManager = browserWindowParent.GetRadWindowManager();
            if (parentWindowManager != null && typeof parentWindowManager !== 'undefined') {
                var parentWindow = parentWindowManager.GetActiveWindow();
                if (parentWindow != null && typeof parentWindow !== 'undefined') {
                    return parentWindow;
                }
            }
        }
        return null;
    },
    onWindowClose: function (oWnd, eventArgs) {
        var parentWindow = oWnd.AjaxDialog.getParentWindow(oWnd);
        if (parentWindow) {
            parentWindow.autoSize();
        }
        oWnd.remove_close(oWnd.AjaxDialog.onWindowClose);
    },

    //Resizes the dialog window to match the inner content size
    setWndWidth: function (newWidth) {
        var oWnd = this.get_radWindow();
        if (!oWnd) {
            var jQueryDialog = this.get_jQueryDialog();
            if (jQueryDialog) {
                jQueryDialog.dialog({ width: newWidth });
            }
        }
        else {
            oWnd.set_width(newWidth);
        }
    },

    //Set dialog height
    setWndHeight: function (newHeight) {
        var oWnd = this.get_radWindow();
        if (!oWnd) {
            var jQueryDialog = this.get_jQueryDialog();
            if (jQueryDialog) {
                jQueryDialog.dialog({ height: newHeight });
            }
        }
        else {
            oWnd.set_height(newHeight);
        }
    },

    // ------------------------------------------------------------------------
    // Private functions
    // ------------------------------------------------------------------------
    _splitQueryString: function (queryString) {
        var queryStringParts = [];
        if (queryString && queryString.length > 0) {
            if (queryString.indexOf("?") == 0) {
                queryString = queryString.substring(1);
            }
            if (queryString) {
                var pairs = queryString.split("&");
                var i = pairs.length;
                var keyValuePair = null;
                while (i--) {
                    keyValuePair = pairs[i].split("=");
                    queryStringParts[keyValuePair[0]] = keyValuePair[1];
                }
            }
        }
        return queryStringParts;
    },

    _closeJDialog: function () {
        var jQueryDialog = this.get_jQueryDialog();
        if (jQueryDialog) {
            jQueryDialog.dialog("close");
        }
    },

    // ------------------------------------------------------------------------
    // Properties
    // ------------------------------------------------------------------------
    get_radWindow: function () {
        if (!this._dialog) {
            if (typeof window.radWindow !== "undefined") {
                this._dialog = window.radWindow;
            }
            else if (window.frameElement != null && typeof window.frameElement.radWindow !== "undefined") {
                this._dialog = window.frameElement.radWindow;
            }
        }
        return this._dialog;
    },
    get_jQueryDialog: function () {
        var jElement = jQuery(this._element);
        if (typeof jElement.dialog === "function") {
            return jElement;
        }
        return null;
    }

};
Telerik.Sitefinity.Web.UI.AjaxDialogBase.registerClass('Telerik.Sitefinity.Web.UI.AjaxDialogBase', Sys.UI.Control);
