Type.registerNamespace("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend");
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget = function (element) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget.initializeBase(this, [element]);
    //element properties
    this._actionsMenu = null;
    this._thread = null;
    this.lockedItem = null;
    this.webServiceUrl = null;
    this._clientLabelManager = null;

    this.allowCommentsText = null;
    this.closeCommentsText = null;
    this._numberOfCommentsSection = null;

    this._ratingSection = null;
    this._averageRating = null;
    this._ratingComponent = null;

    //delegates
    this._onLoadDelegate = null;
    this._actionCommandDelegate = null;
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget.prototype =
 {
     /* --------------------  set up and tear down ----------- */

     initialize: function () {
         Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget.callBaseMethod(this, "initialize");
         this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
         Sys.Application.add_load(this._onLoadDelegate);

         this._actionCommandDelegate = Function.createDelegate(this, this._actionCommandHandler);
         this.add_actionCommand(this._actionCommandDelegate);

         this.lockedItem = jQuery('span.closedIcon.sfLockedItem');

         if (this.get_thread()) {
             if (this.get_thread().IsClosed)
                 this.lockedItem.toggle();
         }
     },

     dispose: function () {
         Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget.callBaseMethod(this, "dispose");
         if (this._actionCommandDelegate) {
             this.remove_actionCommand(this._actionCommandDelegate);
             delete this._actionCommandDelegate;
         }

         delete this._actionCommandDelegate;

         if (this._onLoadDelegate) {
             Sys.Application.remove_load(this._onLoadDelegate);
             delete this._onLoadDelegate;
         }
     },

     /* --------------------  event methods ----------- */
     _onLoadHandler: function () {
         this._bindActions();
         jQuery(this.get_element()).find(".actionsMenu").clickMenu();
         this.updateRating(this.get_averageRating());
     },
     /* --------------------  public methods ----------- */

     //Updates comments count
     updateCommentsCount: function () {
         if (this.get_thread()) {
             var url = this.get_webServiceUrl() + "/comments/filter";
             var that = this;
             jQuery.ajax({
                 url: url,
                 dataType: 'json',
                 contentType: 'application/json; charset=utf-8',
                 type: "POST",
                 cache: false,
                 data: Telerik.Sitefinity.JSON.stringify({
                         ThreadKey: this.get_thread().Key,
                         Take: 1,
                         Skip: 0
                     }),
                 success: function (result) {                     
                     if (result && result.TotalCount > 1) {
                         jQuery(that.get_numberOfCommentsSection()).show();
                         jQuery(that.get_numberOfCommentsSection()).html(result.TotalCount + " " +
                             that.get_clientLabelManager().getLabel("CommentsResources", "CommentsPluralTypeName"));
                     }
                     else {
                         jQuery(that.get_numberOfCommentsSection()).hide();
                     }
                 }
             });
         }
     },

     //Updates average rating 
     updateAverageRating: function () {
         var that = this;

         if (this.get_thread()) {
             var getThreadDataUrl = String.format(this.get_webServiceUrl() + "/comments/reviews_statistics?ThreadKey={0}", this.get_thread().Key);
             jQuery.ajax({
                 type: 'GET',
                 url: getThreadDataUrl,
                 contentType: "application/json",
                 cache: false,
                 accepts: {
                     text: "application/json"
                 },
                 processData: false,
                 success: function (result) {
                     if (result.length > 0) {
                         that.updateRating(result[0].AverageRating);
                     }
                 }
             });
         }
     },

     updateRating: function (rating) {
         var jqRatingSection = jQuery(this.get_ratingSection());
         
         if (rating > 0) {
             if (this._ratingComponent) {
                 this._ratingComponent.set_value(rating);
             } else {
                 this._ratingComponent = jqRatingSection.rating({
                     value: rating,
                     readOnly: true,
                     displayMode: "ShortText",
                     label: this.get_clientLabelManager().getLabel("CommentsResources", "AverageRating")
                 });
             }
             jqRatingSection.show();
         } else {
             jqRatingSection.hide();
         }
     },

     //add action command Event for clicking on context bar links
     add_actionCommand: function (delegate) {
         /// <summary>Happens when a custom command was fired from action click. Can be canceled.</summary>
         this.get_events().addHandler('actionCommand', delegate);
     },
     //remove action command Event for clicking on context bar links
     remove_actionCommand: function (delegate) {
         /// <summary>Happens when a custom command was fired from action click. Can be canceled.</summary>
         this.get_events().removeHandler('actionCommand', delegate);
     },
     //raise action command Event for clicking on context bar links
     _raiseActionCommand: function (commandName, commandArgument, element) {
         var eventArgs = new Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs(commandName, commandArgument);
         eventArgs._element = element;
         var handler = this.get_events().getHandler('actionCommand');
         if (handler) handler(this, eventArgs);
         return eventArgs;
     },

     /* --------------------  private methods ----------- */
     //binding the click event by checking for "sf_threadCommand_" prefix in the class attribute of the link
     _bindActions: function () {
         var that = this;
         jQuery(this.get_actionsMenu()).find("a").each(
             function (index) {
                 if (jQuery(this).attr("class") && jQuery(this).attr("class").indexOf("sf_threadCommand_") != -1) {
                     jQuery(this).click(function () {
                         var commandName = jQuery(this).attr("class").substr(jQuery(this).attr("class").indexOf("sf_threadCommand_") + "sf_threadCommand_".length).split(' ')[0];
                         that._raiseActionCommand(commandName, that.get_thread().Key, this);
                     });
                 }
             });
     },

     _actionCommandHandler: function (sender, args) {
         var commandName = args.get_commandName();

         switch (commandName) {
             case "closeThread":
                 if (this.get_thread().IsClosed) {
                     this._threadAction("OpenThread", this.lockedItem);
                     this.get_thread().IsClosed = false;
                     jQuery(args._element).text(this.get_closeCommentsText());
                 }
                 else {
                     this._threadAction("CloseThread", this.lockedItem);
                     this.get_thread().IsClosed = true;
                     jQuery(args._element).text(this.get_allowCommentsText());
                 }
                 break;
         }
     },

     //Invoke thread status changes based on the called command
     _threadAction: function (method, element) {
         var isClosed;
         if (method == "CloseThread")
             isClosed = true;
         else if (method == "OpenThread")
             isClosed = false;
         jQuery.ajax({
             url: this.get_webServiceUrl() + "/threads/",
             contentType: "application/json",
             type: "PUT",
             data: Telerik.Sitefinity.JSON.stringify({ Key: this.get_thread().Key, IsClosed: isClosed }),
             success: function (result) {
                 element.toggle();
             }
         });
     },

     /* -------------------- properties ---------------- */
     get_actionsMenu: function () {
         return this._actionsMenu;
     },
     set_actionsMenu: function (value) {
         this._actionsMenu = value;
     },

     get_thread: function () {
         return this._thread;
     },
     set_thread: function (value) {
         this._thread = value;
     },

     get_webServiceUrl: function () {
         return this.webServiceUrl;
     },
     set_webServiceUrl: function (value) {
         this.webServiceUrl = value;
     },

     get_numberOfCommentsSection: function () {
         return this._numberOfCommentsSection;
     },
     set_numberOfCommentsSection: function (value) {
         this._numberOfCommentsSection = value;
     },

     get_ratingSection: function () {
         return this._ratingSection;
     },
     set_ratingSection: function (value) {
         this._ratingSection = value;
     },

     get_averageRating: function () {
         return this._averageRating;
     },
     set_averageRating: function (value) {
         this._averageRating = value;
     },

     get_allowCommentsText: function () {
         return this.allowCommentsText;
     },
     set_allowCommentsText: function (value) {
         this.allowCommentsText = value;
     },

     get_closeCommentsText: function () {
         return this.closeCommentsText;
     },
     set_closeCommentsText: function (value) {
         this.closeCommentsText = value;
     },
     
     get_clientLabelManager: function () {
         return this._clientLabelManager;
     },
     set_clientLabelManager: function (value) {
         this._clientLabelManager = value;
     }
 };

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget.registerClass("Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsThreadHeaderWidget", Sys.UI.Control);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs = function (commandName, commandArgument) {
    Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.initializeBase(this);
    if (commandArgument && commandArgument.get_commandName && commandArgument.get_commandArgument && commandArgument.get_cancel) {
        this._commandName = commandArgument.get_commandName();
        this._commandArgument = commandArgument.get_commandArgument();
        this.set_cancel(commandArgument.get_cancel());
    }
    else {
        this._commandName = commandName;
        this._commandArgument = commandArgument;
    }
}

Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    }   
};
Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs.registerClass('Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommandEventArgs', Sys.CancelEventArgs);