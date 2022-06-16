// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientBinders.SenderReceiverPair
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ClientBinders
{
  /// <summary>
  /// Reprecesents a pair of sender - receiver clientIds that are used by the AsyncCommandMediator
  /// </summary>
  public class SenderReceiverPair
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ClientBinders.SenderReceiverPair" /> class.
    /// </summary>
    public SenderReceiverPair() => this.TwoWayCommunicationMode = false;

    /// <summary>
    /// Gets or sets the client pageId of the component which sends the commands - buttonbar, etc
    /// On the client side should implement the IAsyncCommandSender interface
    /// </summary>
    [IDReferenceProperty(typeof (IScriptControl))]
    public string CommandSenderClientId { get; set; }

    /// <summary>
    /// Gets or sets the client if of the compoment that accepts the comands
    /// On the Client side should implement the IAsyncCommandReceiver interface
    /// </summary>
    [IDReferenceProperty(typeof (IScriptControl))]
    public string CommandReceiverClientId { get; set; }

    /// <summary>
    /// If set to true the mediator will try to fire all the commands comming from the sender
    /// on the receiver using the _onCommandHandler
    /// By default the mediator will process only the endProcessing Events comming from the receiver
    /// </summary>
    public bool TwoWayCommunicationMode { get; set; }
  }
}
