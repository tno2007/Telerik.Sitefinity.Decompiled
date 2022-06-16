Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ------------------------------------------------------------------------
// Decision screen class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.DecisionScreen = function (element) {
    this._actionItems = null;
    this._element = element;
    this._messageControl = null;

    Telerik.Sitefinity.Web.UI.DecisionScreen.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.DecisionScreen.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        this._actionItems = (this._actionItems == null) ? null : Sys.Serialization.JavaScriptSerializer.deserialize(this._actionItems);
        this._handleActionCommandDelegate = Function.createDelegate(this, this._handleActionCommand);
        var actionItemCount = (this._actionItems == null) ? 0 : this._actionItems.length;
        while (actionItemCount--) {
            var actionLink = $get(this._actionItems[actionItemCount].LinkClientId);
            if (actionItemCount == 0) {
                $(actionLink).append($('<span></span>').addClass("sfDecisionIcon"));
            }
            $addHandler(actionLink, 'click', this._handleActionCommandDelegate);
        }

        Telerik.Sitefinity.Web.UI.DecisionScreen.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        var actionItemCount = (this._actionItems == null) ? 0 : this._actionItems.length;
        while (actionItemCount--) {
            var actionLink = $get(this._actionItems[actionItemCount].LinkClientId);
            $removeHandler(actionLink, 'click', this._handleActionCommandDelegate);
        }
        this._actionItems = null;
        this._handleActionCommandDelegate = null;
        Telerik.Sitefinity.Web.UI.DecisionScreen.callBaseMethod(this, 'dispose');
    },

    /* ************************* public methods ************************* */
    show: function () {
        $(this._element).show();
    },

    hide: function () {
        $(this._element).hide();
    },

    /* ************************* events ************************* */
    add_actionCommand: function (delegate) {
        this.get_events().addHandler('actionCommand', delegate);
    },
    remove_actionCommand: function (delegate) {
        this.get_events().removeHandler('actionCommand', delegate);
    },

    /* ************************* event handlers ************************* */

    _handleActionCommand: function (sender) {
        var commandLink = this._getEventLink(sender);
        if (commandLink != null) {
            var actionItem = this._getActionItem(commandLink.id);
            if (this._onActionCommand(actionItem.CommandName, actionItem).get_cancel() == false) {
                if (actionItem.NavigateUrl) {
                    window.location = actionItem.NavigateUrl;
                }
            }
        }
    },

    //fix for #61421, Alon: if you don't click the actual link (<a>), but an internal node (e.g. <strong>), you can't get the right link properties.
    //this function helps to find the real link.
    _getEventLink: function (eventObject) {
        var link = eventObject.target;
        if (link.nodeName.toUpperCase() != "A") {
            var parentLink = $(link).parent("A");
            if (parentLink.length > 0) {
                link = parentLink.get(0);
            }
            else
                link = null;
        }
        return link;
    },

    /* ************************* event firing ************************* */
    _onActionCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('actionCommand');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    /* ************************* private methods ************************* */
    _getActionItem: function (actionLinkId) {
        var actionItemCount = (this._actionItems == null) ? 0 : this._actionItems.length;
        while (actionItemCount--) {
            if (this._actionItems[actionItemCount].LinkClientId == actionLinkId) {
                return this._actionItems[actionItemCount];
            }
        }
        return null;
    },

    /* ************************* properties ************************* */

    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    }
};
Telerik.Sitefinity.Web.UI.DecisionScreen.registerClass('Telerik.Sitefinity.Web.UI.DecisionScreen', Sys.UI.Control);