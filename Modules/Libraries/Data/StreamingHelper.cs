// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Data.StreamingHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;

namespace Telerik.Sitefinity.Modules.Libraries.Data
{
  public static class StreamingHelper
  {
    /// <summary>
    /// Helper method that will copy efficiently the entire stream. This overload lets us specify the chunk size.
    /// </summary>
    /// <param name="source">Source stream to copy from</param>
    /// <param name="target">Target stream to copy to</param>
    /// <param name="resetSource">True to reset the source position to its original value before the copying</param>
    /// <param name="chunkSize">Desired chunk size</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="source" /> or <paramref name="target" /> is <c>null</c></exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">When <paramref name="chunkSize" /> is non-positive.</exception>
    public static void CopyStream(Stream source, Stream target, bool resetSource, long chunkSize)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (chunkSize <= 0L)
        throw new ArgumentOutOfRangeException("Chunk size has to be non-negative");
      long num = 0;
      if (resetSource)
        num = source.Position;
      byte[] buffer = new byte[chunkSize];
      int count = buffer.Length;
      while (count == buffer.Length)
      {
        count = source.Read(buffer, 0, buffer.Length);
        if (count > 0)
          target.Write(buffer, 0, count);
      }
      if (!resetSource)
        return;
      source.Position = num;
    }

    /// <summary>
    /// Reads a stream into memory and returns a byte array. Use with caution! Will load the whole array in memory
    /// </summary>
    /// <param name="source">Stream to read into memory</param>
    /// <param name="resetSource">Reset source after reading to its original position.</param>
    /// <returns>Buffer of a byte array containing the stream's binary data.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="source" /> is <c>null</c>.</exception>
    public static byte[] ReadToEnd(Stream source, bool resetSource)
    {
      long num = source != null ? source.Position : throw new ArgumentNullException(nameof (source));
      byte[] buffer = new byte[source.Length];
      source.Read(buffer, 0, (int) source.Length);
      if (resetSource)
        source.Position = num;
      return buffer;
    }
  }
}
