// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Cryptography.CryptographyManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Security.Cryptography
{
  /// <summary>Used to Encrypt and Decrypt data</summary>
  internal static class CryptographyManager
  {
    private static readonly Dictionary<string, ConcurrentProperty<TripleDESCryptoServiceProvider>> CryptoServiceProviders = new Dictionary<string, ConcurrentProperty<TripleDESCryptoServiceProvider>>();

    /// <summary>Encrypts the given data with the given key.</summary>
    /// <param name="data">The data to encrypt.</param>
    /// <param name="key">The encryption key to use.</param>
    /// <returns>The encrypted data as Base64 encoded string.</returns>
    public static string EncryptData(string data, string key) => Convert.ToBase64String(CryptographyManager.EncryptData(Encoding.Unicode.GetBytes(data), key));

    /// <summary>Decrypts the given data with the given key.</summary>
    /// <param name="data">The data to decrypt, in Base64 encoding.</param>
    /// <param name="key">The decryption key to use.</param>
    /// <returns>The decrypted data as plain text.</returns>
    public static string DecryptData(string data, string key) => Encoding.Unicode.GetString(CryptographyManager.DecryptData(Convert.FromBase64String(data), key));

    private static byte[] EncryptData(byte[] data, string key)
    {
      TripleDESCryptoServiceProvider cryptoServiceProvider = CryptographyManager.GetCryptoServiceProvider(key);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        byte[] randomByteKey = SecurityManager.GetRandomByteKey(8);
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(cryptoServiceProvider.Key, randomByteKey), CryptoStreamMode.Write))
        {
          cryptoStream.Write(data, 0, data.Length);
          cryptoStream.FlushFinalBlock();
          long length = memoryStream.Length;
          byte[] bytes = BitConverter.GetBytes(data.Length);
          byte[] destinationArray1 = new byte[length + 12L];
          Array.Copy((Array) memoryStream.ToArray(), (Array) destinationArray1, length);
          Array.Copy((Array) randomByteKey, 0L, (Array) destinationArray1, length, 8L);
          byte[] destinationArray2 = destinationArray1;
          long destinationIndex = length + 8L;
          Array.Copy((Array) bytes, 0L, (Array) destinationArray2, destinationIndex, 4L);
          return destinationArray1;
        }
      }
    }

    private static byte[] DecryptData(byte[] data, string key)
    {
      byte[] numArray = new byte[8];
      Array.Copy((Array) data, data.Length - 12, (Array) numArray, 0, 8);
      int int32 = BitConverter.ToInt32(data, data.Length - 4);
      byte[] buffer = new byte[int32];
      using (MemoryStream memoryStream = new MemoryStream(data, 0, data.Length - 12))
      {
        TripleDESCryptoServiceProvider cryptoServiceProvider = CryptographyManager.GetCryptoServiceProvider(key);
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateDecryptor(cryptoServiceProvider.Key, numArray), CryptoStreamMode.Read))
        {
          cryptoStream.Read(buffer, 0, int32);
          return buffer;
        }
      }
    }

    private static TripleDESCryptoServiceProvider GetCryptoServiceProvider(
      string key)
    {
      return CryptographyManager.CryptoServiceProviders.GetOrAdd<string, ConcurrentProperty<TripleDESCryptoServiceProvider>>(key, new Func<string, ConcurrentProperty<TripleDESCryptoServiceProvider>>(CryptographyManager.CreateTripleDESCryptoServiceProvider)).Value;
    }

    private static ConcurrentProperty<TripleDESCryptoServiceProvider> CreateTripleDESCryptoServiceProvider(
      string key)
    {
      return new ConcurrentProperty<TripleDESCryptoServiceProvider>(new Func<TripleDESCryptoServiceProvider>(new TripleDESCryptoServiceProviderFactory(key).Build));
    }
  }
}
