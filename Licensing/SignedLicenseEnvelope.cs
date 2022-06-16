// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.SignedLicenseEnvelope
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>
  /// Represents a signed license info , providers parsing and validation of saved licenses
  /// </summary>
  public class SignedLicenseEnvelope
  {
    private const string const_Envelope = "Envelope";
    private const string const_SignedInfo = "SignedInfo";
    private const string const_SignatureValue = "SignatureValue";

    public SignedLicenseEnvelope()
    {
    }

    public SignedLicenseEnvelope(string encryptedEnvelope) => this.LoadEncryptedEnvelope(encryptedEnvelope);

    /// <summary>Gets or sets the signed info.</summary>
    /// <value>The signed info.</value>
    public string SignedInfo { get; set; }

    /// <summary>Gets or sets the signature value.</summary>
    /// <value>The signature value.</value>
    public string SignatureValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the envelope is corrupted, e.g. unparsable or missing required elements
    /// </summary>
    public bool IsCorrupted { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether [signature is invalid].
    /// </summary>
    /// <value><c>true</c> if [signature is invalid]; otherwise, <c>false</c>.</value>
    public bool SignatureIsInvalid => !this.CheckSignature(this.SignatureKey);

    /// <summary>
    /// Gets or sets the signature RSA key represented as xml string persisted data
    /// </summary>
    /// <value>The encryption key.</value>
    public virtual string SignatureKey { get; set; }

    /// <summary>
    /// Gets or sets the DES 196 bit key in base64. This property should be set if the DES key used will not be derived from the RSA public key or for testing pruposes
    /// </summary>
    /// <value>The DES key in base64.</value>
    public virtual string DesKeyInBase64 { get; set; }

    /// <summary>
    /// Returns an xml envelope with the license info and the signature.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => this.ToXmlDocument().ToString(SaveOptions.DisableFormatting);

    /// <summary>Loads the data from encrypted envelope string.</summary>
    public void LoadEncryptedEnvelope(string encryptedEnv)
    {
      try
      {
        this.LoadEnvelopeXml(Encoding.UTF8.GetString(this.DecryptDataWithDes(Convert.FromBase64String(encryptedEnv), this.GetDesKey())));
      }
      catch
      {
        this.IsCorrupted = true;
      }
    }

    /// <summary>
    /// Load the envelop from unencrypted envelope xml string.
    /// </summary>
    /// <param name="xml">The XML.</param>
    public void LoadEnvelopeXml(string xml)
    {
      XElement element;
      try
      {
        element = XElement.Load(XmlReader.Create((TextReader) new StringReader(xml)));
      }
      catch
      {
        this.IsCorrupted = true;
        return;
      }
      this.LoadEnvelopeXmlElement(element);
    }

    /// <summary>
    /// Generates the license from current info as 3DES encrypted base64 string.
    /// </summary>
    /// <param name="info">a license info instance.</param>
    /// <returns></returns>
    public virtual string GetSignedLicense()
    {
      if (string.IsNullOrEmpty(this.SignatureValue))
        this.SignEnvelope();
      return this.EncryptData(this.ToString());
    }

    public static string GenerateSignedLicense(object licenseInfo, string key) => new SignedLicenseEnvelope()
    {
      SignedInfo = licenseInfo.ToString(),
      SignatureKey = key
    }.GetSignedLicense();

    /// <summary>Signs the license.</summary>
    /// <param name="info">The info.</param>
    protected internal virtual void SignEnvelope() => this.SignatureValue = this.GenerateSignedHash(this.SignedInfo);

    /// <summary>Generates the signed hash.</summary>
    /// <param name="dataString">The data string.</param>
    /// <returns></returns>
    protected internal virtual string GenerateSignedHash(string dataString)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(dataString);
      return Convert.ToBase64String(this.GetCryptoProvider((string) null).SignData(bytes, (object) new SHA1CryptoServiceProvider()));
    }

    private XDocument ToXmlDocument()
    {
      XElement xelement = new XElement((XName) "Envelope");
      xelement.Add((object) new XElement((XName) "SignedInfo", (object) new XCData(this.SignedInfo ?? "")));
      xelement.Add((object) new XElement((XName) "SignatureValue", (object) (this.SignatureValue ?? "")));
      return new XDocument(new object[1]
      {
        (object) xelement
      });
    }

    /// <summary>Loads the XML from a an element.</summary>
    /// <param name="element">The element.</param>
    private void LoadEnvelopeXmlElement(XElement element)
    {
      XElement xelement1 = element.Descendants((XName) "SignedInfo").FirstOrDefault<XElement>();
      if (xelement1 != null)
      {
        this.SignedInfo = xelement1.Value;
        XElement xelement2 = element.Descendants((XName) "SignatureValue").FirstOrDefault<XElement>();
        if (xelement2 != null)
          this.SignatureValue = xelement2.Value;
        else
          this.IsCorrupted = true;
      }
      else
        this.IsCorrupted = true;
    }

    /// <summary>Checks the signature.</summary>
    /// <returns></returns>
    public virtual bool CheckSignature(string signatureKey)
    {
      if (string.IsNullOrEmpty(this.SignatureValue))
        return false;
      byte[] bytes = Encoding.UTF8.GetBytes(this.SignedInfo);
      byte[] signature = Convert.FromBase64String(this.SignatureValue);
      return this.GetCryptoProvider(signatureKey).VerifyData(bytes, (object) new SHA1CryptoServiceProvider(), signature);
    }

    /// <summary>
    /// Gets the crypto provider with the currently assigned SignatureKey
    /// </summary>
    /// <returns></returns>
    protected internal virtual RSACryptoServiceProvider GetCryptoProvider() => this.GetCryptoProvider(this.SignatureKey);

    /// <summary>
    /// Gets the crypto provider. Abstracts how the key is imported into the crypto provider
    /// </summary>
    /// <returns></returns>
    protected internal virtual RSACryptoServiceProvider GetCryptoProvider(
      string signatureKey)
    {
      if (string.IsNullOrEmpty(signatureKey))
        throw new ApplicationException("No signature key is provided.");
      RSACryptoServiceProvider cryptoProvider = new RSACryptoServiceProvider();
      cryptoProvider.FromXmlString(signatureKey);
      return cryptoProvider;
    }

    /// <summary>
    /// Encrypts the data with hardcoded 3DES key for obfuscation purposes.
    /// </summary>
    /// <param name="licenseData">The license data.</param>
    /// <returns></returns>
    protected virtual string EncryptData(string licenseData) => Convert.ToBase64String(this.EncryptDataWithDES(Encoding.UTF8.GetBytes(licenseData), this.GetDesKey()));

    /// <summary>Decrypts the data with DES.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    protected internal virtual byte[] DecryptDataWithDes(byte[] data, byte[] key)
    {
      TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
      byte[] numArray = new byte[8];
      Array.Copy((Array) data, data.Length - 12, (Array) numArray, 0, 8);
      int int32 = BitConverter.ToInt32(data, data.Length - 4);
      byte[] buffer = new byte[int32];
      using (MemoryStream memoryStream = new MemoryStream(data, 0, data.Length - 12))
      {
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateDecryptor(key, numArray), CryptoStreamMode.Read))
        {
          cryptoStream.Read(buffer, 0, int32);
          return buffer;
        }
      }
    }

    /// <summary>Encrypts the data with DES.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    protected internal virtual byte[] EncryptDataWithDES(byte[] data, byte[] key)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
        byte[] randomByteKey = SignedLicenseEnvelope.GetRandomByteKey(8);
        using (CryptoStream cryptoStream = new CryptoStream((Stream) memoryStream, cryptoServiceProvider.CreateEncryptor(key, randomByteKey), CryptoStreamMode.Write))
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

    /// <summary>
    /// Gets the random byte key used with the DES encryption.
    /// </summary>
    /// <param name="byteLength">Length of the byte.</param>
    /// <returns></returns>
    private static byte[] GetRandomByteKey(int byteLength)
    {
      byte[] data = new byte[byteLength];
      new RNGCryptoServiceProvider().GetBytes(data);
      return data;
    }

    /// <summary>
    /// Gets the current DES key. The DES encyption is only for obfuscation purpose
    /// </summary>
    /// <returns></returns>
    protected internal virtual byte[] GetDesKey()
    {
      if (!string.IsNullOrEmpty(this.DesKeyInBase64))
        return Convert.FromBase64String(this.DesKeyInBase64);
      byte[] numArray = new byte[24];
      return Convert.FromBase64String(Config.Get<SecurityConfig>().DesKey);
    }
  }
}
