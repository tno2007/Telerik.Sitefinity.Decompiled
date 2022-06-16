// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pop3.Pop3Manager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Utilities.OpenPOP.MIME;
using Telerik.Sitefinity.Utilities.OpenPOP.POP3;

namespace Telerik.Sitefinity.Publishing.Pop3
{
  /// <summary>Managing class over OpenPOP library</summary>
  public static class Pop3Manager
  {
    /// <summary>
    /// Retrieve all messages from a mail account and deletes them
    /// </summary>
    /// <param name="isSslEnabled">To use SSL connection or not</param>
    /// <param name="server">POP3 server address</param>
    /// <param name="port">POP3 port</param>
    /// <param name="user">User for authentication in the POP3 server</param>
    /// <param name="password">Password for authentication in the POP3 server</param>
    /// <param name="appendHostToUser">If <c>true</c> transforms the user to an email address using the host</param>
    /// <returns>All mails in MIME.Message format</returns>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.PopServerNotFoundException">When POP3 server not found</exception>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.PopServerNotAvailableException">When POP3 server not available</exception>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.InvalidLoginException">When POP3 server have not accepted user login</exception>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.PopServerLockException">When POP3 server is locked</exception>
    /// <exception cref="T:Telerik.Sitefinity.Utilities.OpenPOP.POP3.InvalidPasswordException">When POP3 server responded wrong user/password pair</exception>
    public static IEnumerable<Message> RetrieveAndDeleteMails(
      bool isSslEnabled,
      string server,
      int port,
      string user,
      string password,
      bool appendHostToUser = true)
    {
      using (TcpClient clientSocket = new TcpClient())
        return Pop3Manager.GetMails(clientSocket, server, port, user, password, isSslEnabled, appendHostToUser);
    }

    private static IEnumerable<Message> GetMails(
      TcpClient clientSocket,
      string server,
      int port,
      string user,
      string password,
      bool isSslEnabled,
      bool appendHostToUser)
    {
      string username = user;
      if (appendHostToUser && !username.Contains("@"))
        username = username + "@" + server;
      POPClient popClient = new POPClient();
      List<Message> mails = new List<Message>();
      try
      {
        if (popClient.Connected)
          popClient.Disconnect();
        popClient.Connect(clientSocket, server, port, isSslEnabled);
        popClient.Authenticate(username, password);
        int messageCount = popClient.GetMessageCount();
        int num1 = 0;
        int num2 = 0;
        for (int messageNumber = 1; messageNumber <= messageCount; ++messageNumber)
        {
          try
          {
            Message message = popClient.GetMessage(messageNumber);
            if (message != null)
            {
              ++num1;
              mails.Add(message);
              popClient.DeleteMessage(messageNumber);
            }
            else
              ++num2;
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw;
            else
              ++num2;
          }
        }
      }
      finally
      {
        popClient.Disconnect();
      }
      return (IEnumerable<Message>) mails;
    }
  }
}
