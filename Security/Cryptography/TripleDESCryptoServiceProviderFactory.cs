// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Cryptography.TripleDESCryptoServiceProviderFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Cryptography;

namespace Telerik.Sitefinity.Security.Cryptography
{
  internal class TripleDESCryptoServiceProviderFactory
  {
    private readonly string encryptionKey;

    public TripleDESCryptoServiceProviderFactory(string key) => this.encryptionKey = !key.IsNullOrEmpty() ? key : throw new ArgumentException("Key must be specified.");

    public TripleDESCryptoServiceProvider Build()
    {
      TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
      int length = cryptoServiceProvider.Key.Length;
      string encryptionKey = this.encryptionKey;
      while (encryptionKey.Length / 2 < length)
        encryptionKey += encryptionKey;
      cryptoServiceProvider.Key = SecurityManager.HexToByte(encryptionKey.Left(length * 2));
      return cryptoServiceProvider;
    }
  }
}
