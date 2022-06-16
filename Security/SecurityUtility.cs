// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecurityUtility
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Security
{
  public static class SecurityUtility
  {
    public static string GetPasswordLengthHint(int minRequiredPasswordLength)
    {
      Labels labels = Res.Get<Labels>();
      return string.Format(labels.PasswordLengthHint, (object) minRequiredPasswordLength, minRequiredPasswordLength > 1 ? (object) labels.CharacterPlural : (object) labels.CharacterSingular);
    }

    public static string GetPasswordAlphaNumCharactersHint(int minRequiredNonAlphanumericCharacters)
    {
      Labels labels = Res.Get<Labels>();
      return string.Format(labels.PasswordAlphaNumCharactersHint, (object) minRequiredNonAlphanumericCharacters, minRequiredNonAlphanumericCharacters > 1 ? (object) labels.CharacterPlural : (object) labels.CharacterSingular);
    }

    public static string GetPasswordRequirementsText(UserManager manager) => SecurityUtility.GetPasswordRequirementsText(manager.MinRequiredPasswordLength, manager.MinRequiredNonAlphanumericCharacters);

    public static string GetPasswordRequirementsText(MembershipDataProvider provider) => SecurityUtility.GetPasswordRequirementsText(provider.MinRequiredPasswordLength, provider.MinRequiredNonAlphanumericCharacters);

    public static string GetPasswordRequirementsText(
      int minRequiredPasswordLength,
      int minRequiredNonAlphanumericCharacters)
    {
      Labels labels = Res.Get<Labels>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(labels.ThePasswordMustBeAtLeastLong, (object) minRequiredPasswordLength, minRequiredPasswordLength > 1 ? (object) labels.CharacterPlural : (object) labels.CharacterSingular);
      if (minRequiredNonAlphanumericCharacters > 0)
      {
        stringBuilder.Append(" ");
        stringBuilder.AppendFormat(labels.AndMustContainNoLessThanNonAlphanumeric, (object) minRequiredNonAlphanumericCharacters, minRequiredNonAlphanumericCharacters > 1 ? (object) labels.CharacterPlural : (object) labels.CharacterSingular);
      }
      stringBuilder.Append(".");
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets an instance of a secured object by manager, object type and ID.
    /// </summary>
    /// <param name="managerInstance">Instance of the relevant manager.</param>
    /// <param name="SecuredObjectTypeString">The secured object type string.</param>
    /// <param name="securedObjectId">The secured object id.</param>
    /// <returns>Instance of a secured object.</returns>
    internal static ISecuredObject GetSecuredObject(
      IManager managerInstance,
      string securedObjectTypeString,
      Guid securedObjectId,
      string dynamicDataProviderName = null)
    {
      Type itemType = TypeResolutionService.ResolveType(WcfHelper.DecodeWcfString(securedObjectTypeString), false, true);
      ISecuredObject mainSecuredObject = (ISecuredObject) managerInstance.GetItem(itemType, securedObjectId);
      return managerInstance is IDynamicModuleSecurityManager && !itemType.Equals(typeof (SecurityRoot)) ? DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) managerInstance, mainSecuredObject, dynamicDataProviderName) : mainSecuredObject;
    }

    /// <summary>
    /// Encrypts the specified data using a symmetric encryption algorithm. Uses the DecryptionKey from Sitefinity's security config.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="initialVector">The initial vector. It is needed for decryption.</param>
    /// <returns>Encrypted data.</returns>
    internal static byte[] Encrypt(string data, ref byte[] initialVector)
    {
      byte[] key = SecurityManager.HexToByte(EnvironmentVariables.Current.GetDecryptionKey());
      return SecurityUtility.Encrypt(data, key, ref initialVector);
    }

    /// <summary>
    /// Encrypts the specified data using a symmetric encryption algorithm.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="initialVector">The initial vector. It is needed for decryption.</param>
    /// <returns>Encrypted data.</returns>
    internal static byte[] Encrypt(string data, byte[] key, ref byte[] initialVector)
    {
      using (SecurityUtility.CryptoProvider cryptoProvider = new SecurityUtility.CryptoProvider(key))
      {
        if (initialVector == null)
          initialVector = cryptoProvider.InitializationVector;
        else
          cryptoProvider.InitializationVector = initialVector;
        ICryptoTransform encryptor = cryptoProvider.GetEncryptor();
        using (MemoryStream memoryStream = new MemoryStream())
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) cryptoStream))
              streamWriter.Write(data);
            return memoryStream.ToArray();
          }
        }
      }
    }

    /// <summary>
    /// Decrypts the specified encrypted data using a symmetric decryption algorithm. Uses the DecryptionKey from Sitefinity's security config.
    /// </summary>
    /// <param name="encryptedData">The encrypted data.</param>
    /// <param name="initialVector">The initial vector. Encryption method should provides.</param>
    /// <returns>Decrypted data.</returns>
    internal static string Decrypt(byte[] encryptedData, byte[] initialVector)
    {
      byte[] key = SecurityManager.HexToByte(EnvironmentVariables.Current.GetDecryptionKey());
      return SecurityUtility.Decrypt(encryptedData, key, initialVector);
    }

    /// <summary>
    /// Decrypts the specified encrypted data using a symmetric decryption algorithm.
    /// </summary>
    /// <param name="encryptedData">The encrypted data.</param>
    /// <param name="key">The key.</param>
    /// <param name="initialVector">The initial vector. Encryption method should provides.</param>
    /// <returns>Decrypted data.</returns>
    internal static string Decrypt(byte[] encryptedData, byte[] key, byte[] initialVector)
    {
      using (SecurityUtility.CryptoProvider cryptoProvider = new SecurityUtility.CryptoProvider(key))
      {
        cryptoProvider.InitializationVector = initialVector;
        ICryptoTransform decryptor = cryptoProvider.GetDecryptor();
        using (MemoryStream memoryStream = new MemoryStream(encryptedData))
        {
          using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream) cryptoStream))
              return streamReader.ReadToEnd();
          }
        }
      }
    }

    /// <summary>Gets the keyed hash of the specified data.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    /// <returns>The hash.</returns>
    internal static byte[] GetKeyedHash(string data, byte[] key) => SecurityUtility.GetKeyedHash(Encoding.UTF8.GetBytes(data), key);

    /// <summary>Gets the keyed hash of the specified data.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    /// <returns>The hash.</returns>
    internal static byte[] GetKeyedHash(byte[] data, byte[] key)
    {
      using (KeyedHashAlgorithm keyedHashAlgorithm = KeyedHashAlgorithm.Create())
      {
        keyedHashAlgorithm.Key = key;
        return keyedHashAlgorithm.ComputeHash(data);
      }
    }

    private sealed class CryptoProvider : IDisposable
    {
      private ICryptoTransform encryptor;
      private ICryptoTransform decryptor;
      private AesCryptoServiceProvider aesAlg;

      public CryptoProvider(byte[] key)
      {
        this.aesAlg = new AesCryptoServiceProvider();
        this.aesAlg.KeySize = 256;
        this.aesAlg.BlockSize = 128;
        this.aesAlg.Padding = PaddingMode.PKCS7;
        this.aesAlg.Mode = CipherMode.CBC;
        this.aesAlg.Key = key;
      }

      public byte[] InitializationVector
      {
        get => this.aesAlg.IV;
        set => this.aesAlg.IV = value;
      }

      public ICryptoTransform GetEncryptor()
      {
        if (this.encryptor == null)
          this.encryptor = this.aesAlg.CreateEncryptor();
        return this.encryptor;
      }

      public ICryptoTransform GetDecryptor()
      {
        if (this.decryptor == null)
          this.decryptor = this.aesAlg.CreateDecryptor();
        return this.decryptor;
      }

      public void Dispose()
      {
        if (this.encryptor != null)
          this.encryptor.Dispose();
        if (this.decryptor != null)
          this.decryptor.Dispose();
        if (this.aesAlg == null)
          return;
        this.aesAlg.Dispose();
      }
    }
  }
}
