Type.registerNamespace("Telerik.Sitefinity.Web.UI.ClientBinders");
Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator=function(){Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.initializeBase(this);
this._asyncCommandPairs=null;
this._onCommandDelegate=null;
this._onEndProcessingDelegate=null;
this._onLoadDelegate=null;
this._callbacks=[];
};
Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.prototype={initialize:function(){this._onLoadDelegate=Function.createDelegate(this,this._on_load);
Sys.Application.add_load(this._onLoadDelegate);
},dispose:function(){Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.callBaseMethod(this,"dispose");
for(var i=0,length=this._asyncCommandPairs.length;
i<length;
i++){var pair=this._asyncCommandPairs[i];
var sender=$find(pair.CommandSenderClientId);
var receiver=$find(pair.CommandReceiverClientId);
var pairCallbacks=this._callbacks[i];
if(receiver){receiver.remove_onEndProcessing(pairCallbacks.Receiver);
if(pairCallbacks.TwoWay){receiver.remove_command(pairCallbacks.TwoWay);
}}if(sender){sender.remove_command(pairCallbacks.Sender);
}if(pairCallbacks){delete pairCallbacks.Receiver;
delete pairCallbacks.Sender;
delete pairCallbacks.TwoWay;
}}},_commandHandler:function(sender,commandEventArgs){var commandName=commandEventArgs.get_commandName();
var commandArgs=commandEventArgs.get_commandArgument();
if(arguments.length>2){var receiver=arguments[2];
receiver._onCommand(commandName,commandArgs);
}else{throw new Error("Callback context is not set, receiver is missing.");
}},_endProcessingHandler:function(sender,args){if(arguments.length>2){var asyncSender=arguments[2];
asyncSender._endProcessingHandler(sender,args);
}else{throw new Error("Callback context is not set, sender is missing.");
}},_on_load:function(){this._initAllPairs();
},_initAllPairs:function(){this._onCommandDelegate=Function.createDelegate(this,this._commandHandler);
for(var i=0,length=this._asyncCommandPairs.length;
i<length;
i++){var pair=this._asyncCommandPairs[i];
var sender=this.get_senderFromPair(pair);
var receiver=this.get_receiverFromPair(pair);
if(sender==null){throw new Error("The Sender component is not available. Id:"+pair.CommandSenderClientId);
}if(receiver==null){throw new Error("The Receiver component is not available. Id:"+pair.CommandReceiverClientId);
}var commandCallback=null;
var twoWayCommandCallback=null;
if(pair.TwoWayCommunicationMode){commandCallback=Function.createCallback(this._commandHandler,receiver);
twoWayCommandCallback=Function.createCallback(this._commandHandler,sender);
sender.add_command(commandCallback);
receiver.add_command(twoWayCommandCallback);
}var endProcessingCallback=Function.createDelegate(sender,sender._endProcessingHandler);
receiver.add_onEndProcessing(endProcessingCallback);
var pairCallbacks={Sender:commandCallback,Receiver:endProcessingCallback,TwoWay:twoWayCommandCallback};
this._callbacks.push(pairCallbacks);
}},get_senderFromPair:function(pair){return $find(pair.CommandSenderClientId);
},get_receiverFromPair:function(pair){return $find(pair.CommandReceiverClientId);
},get_asyncCommandPairs:function(){return this._asyncCommandPairs;
},set_asyncCommandPairs:function(value){this._asyncCommandPairs=value;
}};
Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator.registerClass("Telerik.Sitefinity.Web.UI.ClientBinders.AsyncCommandMediator",Sys.Component);
