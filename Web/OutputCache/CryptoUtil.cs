// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.CryptoUtil
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Security.Cryptography;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal static class CryptoUtil
  {
    /// <summary>Computes the SHA256 hash of a given input.</summary>
    /// <param name="input">The input over which to compute the hash.</param>
    /// <returns>The binary hash (32 bytes) of the input.</returns>
    public static byte[] ComputeHash(byte[] input) => CryptoUtil.ComputeHash(input, 0, input.Length);

    /// <summary>
    /// Computes the SHA256 hash of a given segment in a buffer.
    /// </summary>
    /// <param name="buffer">The buffer over which to compute the hash.</param>
    /// <param name="offset">The offset at which to begin computing the hash.</param>
    /// <param name="count">The number of bytes in the buffer to include in the hash.</param>
    /// <returns>The binary hash (32 bytes) of the buffer segment.</returns>
    public static byte[] ComputeHash(byte[] buffer, int offset, int count)
    {
      using (SHA256 shA256 = (SHA256) new SHA256Cng())
        return shA256.ComputeHash(buffer, offset, count);
    }
  }
}
