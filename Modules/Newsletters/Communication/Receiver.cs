// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Communication.Receiver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Net.Sockets;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Publishing.Pop3;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME.Header;
using Telerik.Sitefinity.Utilities.OpenPOP.POP3;

namespace Telerik.Sitefinity.Modules.Newsletters.Communication
{
  /// <summary>
  /// This class is used to receive messages related to the Newsletter module.
  /// </summary>
  public sealed class Receiver : IMessageReceiver
  {
    /// <summary>Gets the total number of available messages.</summary>
    /// <returns>The total number of messages.</returns>
    public int GetMessageCount()
    {
      using (TcpClient socket = new TcpClient())
        return this.GetPopClient(socket).GetMessageCount();
    }

    /// <summary>Gets the header of the message.</summary>
    /// <param name="messageNumber">Number of the message.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Utilities.OpenPOP.MIME.Header.MessageHeader" />.</returns>
    public MessageHeader GetMessageHeader(int messageNumber)
    {
      using (TcpClient socket = new TcpClient())
        return this.GetPopClient(socket).GetMessageHeaders(messageNumber);
    }

    /// <summary>Gets the message.</summary>
    /// <param name="messageNumber">Number of the message.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Utilities.OpenPOP.MIME.Message" />.</returns>
    public Message GetMessage(int messageNumber)
    {
      using (TcpClient socket = new TcpClient())
        return this.GetPopClient(socket).GetMessage(messageNumber);
    }

    /// <summary>Gets all the messages from the POP3 server.</summary>
    /// <returns>The messages</returns>
    public IEnumerable<Message> GetAndDeleteMessages() => Pop3Manager.RetrieveAndDeleteMails(this.GetModuleConfig().Pop3UsesSSL, this.GetModuleConfig().Pop3Server, this.GetModuleConfig().Pop3Port, this.GetModuleConfig().Pop3Username, this.GetModuleConfig().Pop3Password, false);

    /// <summary>
    /// Gets the configured and connected instance of the <see cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.POPClient" />.
    /// </summary>
    /// <param name="socket">An instance of the <see cref="T:System.Net.Sockets.TcpClient" /> used as a socket.</param>
    /// <returns>A configured and connected instance of the <see cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.POPClient" />.</returns>
    protected POPClient GetPopClient(TcpClient socket)
    {
      POPClient popClient = new POPClient();
      if (popClient.Connected)
        popClient.Disconnect();
      popClient.Connect(socket, this.GetModuleConfig().Pop3Server, this.GetModuleConfig().Pop3Port, this.GetModuleConfig().Pop3UsesSSL);
      popClient.Authenticate(this.GetModuleConfig().Pop3Username, this.GetModuleConfig().Pop3Password);
      return popClient;
    }

    /// <summary>Gets the configuration of the Newsletter module.</summary>
    /// <returns>An instance of the <see cref="!:NewsletterConfig" />.</returns>
    protected NewslettersConfig GetModuleConfig() => Config.Get<NewslettersConfig>();
  }
}
