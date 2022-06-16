// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenStringGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Access token string generator class</summary>
  internal static class AccessTokenStringGenerator
  {
    /// <summary>Generates a new token string.</summary>
    /// <returns>The new access token.</returns>
    public static string GenerateTokenString() => AccessTokenStringGenerator.GenerateTokenInternal();

    /// <summary>
    /// Determines whether the specified token is valid token string.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>Value indicating if token is valid token string</returns>
    public static bool IsValidTokenString(string token)
    {
      try
      {
        if (!token.IsNullOrWhitespace())
        {
          byte[] source = Convert.FromBase64String(token);
          ((IEnumerable<byte>) source).Take<byte>(8).ToArray<byte>();
          ((IEnumerable<byte>) source).Skip<byte>(8).Take<byte>(16).ToArray<byte>();
          return true;
        }
      }
      catch
      {
      }
      return false;
    }

    private static string GenerateTokenInternal()
    {
      byte[] bytes = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
      byte[] byteArray = Guid.NewGuid().ToByteArray();
      byte[] numArray = new byte[bytes.Length + byteArray.Length];
      Buffer.BlockCopy((Array) bytes, 0, (Array) numArray, 0, bytes.Length);
      Buffer.BlockCopy((Array) byteArray, 0, (Array) numArray, bytes.Length, byteArray.Length);
      return Convert.ToBase64String(((IEnumerable<byte>) numArray).ToArray<byte>());
    }
  }
}
