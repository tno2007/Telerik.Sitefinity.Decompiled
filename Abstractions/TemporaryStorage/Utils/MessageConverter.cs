// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.TemporaryStorage.CacheMessagingService.Utils.MessageConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using Telerik.Sitefinity.Abstractions.TemporaryStorage.Models;
using Telerik.Sitefinity.LoadBalancing;

namespace Telerik.Sitefinity.Abstractions.TemporaryStorage.CacheMessagingService.Utils
{
  internal class MessageConverter
  {
    private const string Separator = "__";

    public SyncMessage Convert(SystemMessageBase message)
    {
      string[] strArray = message.MessageData.Split(new string[1]
      {
        "__"
      }, StringSplitOptions.None);
      string key = MessageConverter.Base64Decode(strArray[0]);
      SyncOperation syncOperation = (SyncOperation) Enum.Parse(typeof (SyncOperation), strArray[1]);
      string str = MessageConverter.Base64Decode(strArray[2]);
      DateTime dateTime = DateTime.FromBinary(long.Parse(strArray[3]));
      string data = str;
      int operation = (int) syncOperation;
      DateTime expiresAtUtc = dateTime;
      return new SyncMessage(key, data, (SyncOperation) operation, expiresAtUtc);
    }

    public SystemMessageBase Convert(SyncMessage message) => new SystemMessageBase()
    {
      Key = "TemporaryStorageMessageKey",
      MessageData = MessageConverter.Base64Encode(message.Key) + "__" + (object) message.Operation + "__" + MessageConverter.Base64Encode(message.Data) + "__" + (object) message.ExpiresAtUtc.ToBinary()
    };

    private static string Base64Encode(string plainText) => System.Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

    private static string Base64Decode(string base64EncodedData) => Encoding.UTF8.GetString(System.Convert.FromBase64String(base64EncodedData));
  }
}
