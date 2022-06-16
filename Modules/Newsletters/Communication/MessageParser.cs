// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Communication.MessageParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Communication
{
  /// <summary>
  /// This class provides functionality for parsing the received email messages.
  /// </summary>
  public class MessageParser
  {
    private const string statusHeaderKey = "Status";
    public const string campaignIdHeaderKey = "X-Sitefinity-Campaign";
    public const string subscriberIdHeaderKey = "X-Sitefinity-Subscriber";

    /// <summary>Gets the message status from the raw message.</summary>
    /// <param name="rawMessage">The content of the raw message.</param>
    /// <returns>One of the predefined <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Communication.MessageStatus" /> values.</returns>
    public static BounceStatus GetMessageStatus(string rawMessage)
    {
      string messageStatus = !string.IsNullOrEmpty(rawMessage) ? MessageParser.FindHeader(rawMessage, "Status") : throw new ArgumentNullException(nameof (rawMessage));
      return messageStatus != null && !messageStatus.StartsWith("2") ? MessageParser.GetBounceMessageStatus(messageStatus) : BounceStatus.Normal;
    }

    /// <summary>
    /// Gets the bounce type message status from the specified <paramref name="messageStatus" /> as string.
    /// </summary>
    /// <param name="messageStatus">The message status string from which to parse the bounce status.</param>
    /// <returns></returns>
    public static BounceStatus GetBounceMessageStatus(string messageStatus)
    {
      switch (messageStatus)
      {
        case "5.0.0":
        case "5.1.0":
        case "5.1.1":
        case "5.1.2":
        case "5.1.3":
        case "5.1.4":
        case "5.1.5":
        case "5.1.6":
        case "5.1.7":
        case "5.1.8":
        case "5.2.3":
        case "5.2.4":
        case "5.3.0":
        case "5.3.2":
        case "5.3.3":
        case "5.3.4":
        case "5.4.0":
        case "5.4.1":
        case "5.4.2":
        case "5.4.3":
        case "5.4.4":
        case "5.4.6":
        case "5.4.7":
        case "5.5.0":
        case "5.5.1":
        case "5.5.2":
        case "5.5.4":
        case "5.5.5":
        case "5.6.0":
        case "5.6.1":
        case "5.6.2":
        case "5.6.3":
        case "5.6.4":
        case "5.6.5":
        case "5.7.0":
        case "5.7.1":
        case "5.7.2":
        case "5.7.3":
        case "5.7.4":
        case "5.7.5":
        case "5.7.6":
        case "5.7.7":
        case "9.1.1":
          return BounceStatus.Hard;
        case "5.2.0":
        case "5.2.1":
        case "5.2.2":
        case "5.3.1":
        case "5.4.5":
        case "5.5.3":
          return BounceStatus.Soft;
        default:
          return BounceStatus.UnknownBounce;
      }
    }

    /// <summary>
    /// Gets the id of the campaign for which the message has been sent.
    /// </summary>
    /// <param name="rawMessage">The content of the raw message.</param>
    /// <returns>A GUID id of the campaign.</returns>
    public static Guid GetMessageCampaignId(string rawMessage)
    {
      string g = !string.IsNullOrEmpty(rawMessage) ? MessageParser.FindHeader(rawMessage, "X-Sitefinity-Campaign") : throw new ArgumentNullException(nameof (rawMessage));
      return string.IsNullOrEmpty(g) ? Guid.Empty : new Guid(g);
    }

    /// <summary>
    /// Gets the id of the subscriber to whom the message has been sent.
    /// </summary>
    /// <param name="rawMessage">The content of the raw message.</param>
    /// <returns>A GUID id of the subscriber.</returns>
    public static Guid GetMessageSubscriberId(string rawMessage)
    {
      string g = !string.IsNullOrEmpty(rawMessage) ? MessageParser.FindHeader(rawMessage, "X-Sitefinity-Subscriber") : throw new ArgumentNullException(nameof (rawMessage));
      return string.IsNullOrEmpty(g) ? Guid.Empty : new Guid(g);
    }

    private static string FindHeader(string rawMessage, string key)
    {
      StringReader stringReader = new StringReader(rawMessage);
      string empty = string.Empty;
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        if (str.StartsWith(key))
          return str.Split(':')[1].Trim();
      }
      return (string) null;
    }
  }
}
