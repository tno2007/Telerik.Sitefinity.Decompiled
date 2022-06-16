// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.EtagProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Cryptography;
using System.Text;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Provides API for generating ETag.</summary>
  internal class EtagProvider : IEtagProvider
  {
    /// <summary>Generates ETag by given key and content.</summary>
    /// <param name="key">Key used for generating the hash.</param>
    /// <param name="content">Content used for generating the hash.</param>
    /// <returns>The generated ETag.</returns>
    public string GetEtag(string key, byte[] content) => this.GenerateETag(this.Combine(Encoding.UTF8.GetBytes(key), content));

    private string GenerateETag(byte[] data)
    {
      using (SHA256 shA256 = SHA256.Create())
        return BitConverter.ToString(shA256.ComputeHash(data)).Replace("-", string.Empty);
    }

    private byte[] Combine(byte[] a, byte[] b)
    {
      byte[] dst = new byte[a.Length + b.Length];
      Buffer.BlockCopy((Array) a, 0, (Array) dst, 0, a.Length);
      Buffer.BlockCopy((Array) b, 0, (Array) dst, a.Length, b.Length);
      return dst;
    }
  }
}
