// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.BinarySerializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Telerik.Sitefinity.Utilities
{
  internal static class BinarySerializer
  {
    /// <summary>Serializes given object to byte data</summary>
    /// <param name="data">The object data</param>
    /// <returns>The byte data</returns>
    public static byte[] Serialize(object data)
    {
      if (data == null)
        return new byte[0];
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) serializationStream, data);
        return serializationStream.ToArray();
      }
    }

    /// <summary>Deserializes given byte data to object</summary>
    /// <param name="data">The byte data</param>
    /// <returns>The object data</returns>
    public static object Deserialize(byte[] data)
    {
      if (data == null || data.Length == 0)
        return (object) null;
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      using (MemoryStream serializationStream = new MemoryStream(data, 0, data.Length))
      {
        serializationStream.Seek(0L, SeekOrigin.Begin);
        return binaryFormatter.Deserialize((Stream) serializationStream);
      }
    }
  }
}
