// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Communication.IMessageReceiver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME.Header;

namespace Telerik.Sitefinity.Modules.Newsletters.Communication
{
  internal interface IMessageReceiver
  {
    /// <summary>Gets the total number of available messages.</summary>
    /// <returns>The total number of messages.</returns>
    int GetMessageCount();

    /// <summary>Gets the header of the message.</summary>
    /// <param name="messageNumber">Number of the message.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Utilities.OpenPOP.MIME.Header.MessageHeader" />.</returns>
    MessageHeader GetMessageHeader(int messageNumber);

    /// <summary>Gets the message.</summary>
    /// <param name="messageNumber">Number of the message.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Utilities.OpenPOP.MIME.Message" />.</returns>
    Message GetMessage(int messageNumber);

    /// <summary>Gets all the messages from the POP3 server.</summary>
    /// <returns>The messages</returns>
    IEnumerable<Message> GetAndDeleteMessages();
  }
}
