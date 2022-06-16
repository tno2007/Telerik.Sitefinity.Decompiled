// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Serialization.ISerializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;

namespace Telerik.Sitefinity.Packaging.Serialization
{
  /// <summary>
  /// Provides methods to Serialize and Deserialize structures.
  /// </summary>
  internal interface ISerializer
  {
    /// <summary>
    /// Serialize object to stream. The process will not close the stream.
    /// </summary>
    /// <param name="source">The object</param>
    /// <param name="output">The stream</param>
    void Serialize(object source, Stream output);

    /// <summary>
    /// Deserialize object from stream. The process will not close the stream.
    /// </summary>
    /// <param name="input">The stream</param>
    /// <param name="type">The type of the object</param>
    /// <returns>The deserialized object</returns>
    object Deserialize(Stream input, Type type);

    /// <summary>
    /// Deserialize object from stream. The process will not close the stream.
    /// </summary>
    /// <param name="input">The stream</param>
    /// <typeparam name="T">Type of the object</typeparam>
    /// <returns>The deserialized object</returns>
    T Deserialize<T>(Stream input);
  }
}
