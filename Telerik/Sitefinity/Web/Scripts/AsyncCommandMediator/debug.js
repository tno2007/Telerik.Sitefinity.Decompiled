/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.ClientBinders");

// Connects sender and receiver events through a mediator
// The mediator listens to the command events fired from the senders
// and transfers them to the receiver's command handler
// The mediator also listens to the "endprocessing" event fired from the receivers
// and transfers and calls the handler for the end processing events on the sender
// Senders should implement the IAsyncCommandSender interface
// Receivers have to implement the IAsyncCommandReceiver interface

Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator = function () {
    Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.initializeBase(this);

    this._asyncCommandPairs = null;

    this._onCommandDelegate = null;
    this._onEndProcessingDelegate = null;
    this._onLoadDelegate = null;
    // array with all the created callbacks for the pairs sender/receiver
    // elements are objects of type { Sender: commandCallback, Receiver: endProcessingCallback };
    this._callbacks = [];

}

Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.prototype =
 {
     initialize: function () {
         this._onLoadDelegate = Function.createDelegate(this, this._on_load);
         Sys.Application.add_load(this._onLoadDelegate);

     },

     dispose: function () {
         Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.callBaseMethod(this, "dispose");

         for (var i = 0, length = this._asyncCommandPairs.length; i < length; i++) {
             var pair = this._asyncCommandPairs[i];
             var sender = $find(pair.CommandSenderClientId);
             var receiver = $find(pair.CommandReceiverClientId);
             var pairCallbacks = this._callbacks[i];

             //Unsubscribes from end processing of the reciever
             if (receiver) {
                 receiver.remove_onEndProcessing(pairCallbacks.Receiver);
                 //unsubscribe if in two way mode
                 if (pairCallbacks.TwoWay) {
                     receiver.remove_command(pairCallbacks.TwoWay);
                 }
             }

             //Unscubscribe from the commands that are comming from the sender
             if (sender) {
                 sender.remove_command(pairCallbacks.Sender);
             }

             if (pairCallbacks) {
                 delete pairCallbacks.Receiver;
                 delete pairCallbacks.Sender;
                 delete pairCallbacks.TwoWay;
             }
         }

     },

     /* --------------------  public methods ----------- */



     /* -------------------- events -------------------- */

     _commandHandler: function (sender, commandEventArgs) {
         var commandName = commandEventArgs.get_commandName();
         var commandArgs = commandEventArgs.get_commandArgument();

         // since this is a callback the last argument which is siltently passed is the context
         // in this case - the receiver
         if (arguments.length > 2) {
             var receiver = arguments[2];
             receiver._onCommand(commandName, commandArgs);
         }
         else {
             throw new Error("Callback context is not set, receiver is missing.");
         }

     },

     _endProcessingHandler: function (sender, args) {
         // since this is a callback the last argument which is siltently passed is the context
         // in this case - the sender
         if (arguments.length > 2) {
             var asyncSender = arguments[2];
             asyncSender._endProcessingHandler(sender, args);
         }
         else {
             throw new Error("Callback context is not set, sender is missing.");
         }

     },


     /* -------------------- event handlers ------------ */

     _on_load: function () {
         this._initAllPairs();
     },


     /* -------------------- private methods ----------- */

     // Connects the sender and the receiver in the pairs through the mediator     
     _initAllPairs: function () {
         this._onCommandDelegate = Function.createDelegate(this, this._commandHandler);

         for (var i = 0, length = this._asyncCommandPairs.length; i < length; i++) {
             var pair = this._asyncCommandPairs[i];
             var sender = this.get_senderFromPair(pair);
             var receiver = this.get_receiverFromPair(pair);

             if (sender == null) {
                 throw new Error("The Sender component is not available. Id:" + pair.CommandSenderClientId);
             }
             if (receiver == null) {
                 throw new Error("The Receiver component is not available. Id:" + pair.CommandReceiverClientId);
             }

             var commandCallback = null;
             var twoWayCommandCallback = null; // callback excecuted by the receiver and processed by the sender - reverse link
             if (pair.TwoWayCommunicationMode) {
                 commandCallback = Function.createCallback(this._commandHandler, receiver);
                 twoWayCommandCallback = Function.createCallback(this._commandHandler, sender);
                 //subbscribes to the sender's commands
                 sender.add_command(commandCallback);
                 receiver.add_command(twoWayCommandCallback);
             }
             var endProcessingCallback = Function.createDelegate(sender, sender._endProcessingHandler);
             //subscribes to the receiver end processing event
             receiver.add_onEndProcessing(endProcessingCallback);

             var pairCallbacks = { Sender: commandCallback, Receiver: endProcessingCallback, TwoWay: twoWayCommandCallback };
             this._callbacks.push(pairCallbacks);

         }

     },

     get_senderFromPair: function (pair) {
         return $find(pair.CommandSenderClientId);
     },

     get_receiverFromPair: function (pair) {
         return $find(pair.CommandReceiverClientId);
     },

     /* -------------------- properties ---------------- */


     get_asyncCommandPairs: function () {
         return this._asyncCommandPairs;
     },

     set_asyncCommandPairs: function (value) {
         this._asyncCommandPairs = value;
     }

 };

Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.registerClass("Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator", Sys.Component);