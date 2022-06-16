// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class BlobStorageProvider : DataProviderBase
  {
    private const string CdnKey = "cdn";

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
    /// <param name="managerType">The type of the manger initialized this provider.
    /// The type is needed for retrieving lifetime mangers.</param>
    /// <param name="initializeDecorator">if set to <c>true</c> [initialize decorator].</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType,
      bool initializeDecorator)
    {
      base.Initialize(providerName, config, managerType, false);
      this.InitializeStorage(config);
      this.InitializeProvider(config);
    }

    private void InitializeProvider(NameValueCollection config)
    {
      string str1 = config["cdn"];
      if (!string.IsNullOrEmpty(str1))
        this.Cdn = str1;
      string str2 = config["customImageSizeAllowed"];
      bool result;
      if (!string.IsNullOrEmpty(str2) && bool.TryParse(str2, out result))
        this.CustomImageSizeAllowed = result;
      else
        this.CustomImageSizeAllowed = false;
    }

    /// <summary>Initializes the storage.</summary>
    /// <param name="config">The config.</param>
    protected abstract void InitializeStorage(NameValueCollection config);

    /// <summary>Gets the upload stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public abstract Stream GetUploadStream(IBlobContent content);

    /// <summary>Gets the download stream.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public abstract Stream GetDownloadStream(IBlobContent content);

    public virtual long Upload(IBlobContent content, Stream source, int bufferSize)
    {
      byte[] buffer = new byte[bufferSize];
      int length = buffer.Length;
      long num = 0;
      using (Stream uploadStream = this.GetUploadStream(content))
      {
        while (true)
        {
          int count = source.Read(buffer, 0, buffer.Length);
          if (count > 0)
          {
            uploadStream.Write(buffer, 0, count);
            num += (long) count;
          }
          else
            break;
        }
      }
      return num;
    }

    /// <summary>
    /// Deletes the blob item stored under the specified location
    /// </summary>
    /// <param name="fileId">The file id.</param>
    public abstract void Delete(IBlobContentLocation location);

    /// <summary>
    /// Determines whether a blob items under the specified location is exists.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns>True if the blob exists, otherwise - false</returns>
    public abstract bool BlobExists(IBlobContentLocation location);

    /// <summary>Tests the connection.</summary>
    /// <param name="error">The error.</param>
    /// <returns></returns>
    public virtual bool TestConnection(out Exception error)
    {
      try
      {
        Encoding utF8 = Encoding.UTF8;
        BlobContentProxy content = new BlobContentProxy()
        {
          Id = Guid.NewGuid(),
          FileId = Guid.NewGuid(),
          MimeType = "text/plain",
          FilePath = Guid.NewGuid().ToString()
        };
        try
        {
          string s = "Test";
          int count = 10;
          using (MemoryStream memoryStream = new MemoryStream(utF8.GetBytes(s)))
          {
            byte[] buffer = new byte[count];
            using (Stream uploadStream = this.GetUploadStream((IBlobContent) content))
            {
              while (count == buffer.Length)
              {
                count = memoryStream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                  uploadStream.Write(buffer, 0, count);
              }
            }
          }
          content.Uploaded = true;
          MemoryStream memoryStream1 = new MemoryStream();
          using (memoryStream1)
          {
            byte[] buffer = new byte[count];
            using (Stream downloadStream = this.GetDownloadStream((IBlobContent) content))
            {
              while (count == buffer.Length)
              {
                count = downloadStream.Read(buffer, 0, buffer.Length);
                if (count > 0)
                  memoryStream1.Write(buffer, 0, count);
              }
            }
            memoryStream1.Seek(0L, SeekOrigin.Begin);
            byte[] numArray = new byte[memoryStream1.Length];
            memoryStream1.Read(numArray, 0, numArray.Length);
            if (!utF8.GetString(numArray).Equals(s))
              throw new Exception("Test failed: Download does not return the correct uploaded data.");
          }
        }
        finally
        {
          try
          {
            this.Delete(this.ResolveBlobContentLocation((IBlobContent) content));
          }
          catch
          {
          }
        }
      }
      catch (Exception ex)
      {
        error = ex;
        return false;
      }
      error = (Exception) null;
      return true;
    }

    /// <summary>Resolves the BLOB content location.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual IBlobContentLocation ResolveBlobContentLocation(
      IBlobContent content)
    {
      return (IBlobContentLocation) new BlobContentLocation(content);
    }

    protected internal virtual string GetCdnUrl(
      string cdnUrl,
      string contentUrl,
      params char[] urlDelimiters)
    {
      return cdnUrl.TrimEnd(urlDelimiters) + "/" + contentUrl.Trim(urlDelimiters);
    }

    protected internal virtual long GetStreamingBufferSize() => 0;

    /// <summary>
    /// Determines whether the blob location of the current provider is the same as the location of the other provider.
    /// </summary>
    /// <param name="other">The provider which location will be compared to.</param>
    /// <returns>True if the location of the blob items is the same. Otherwise false.</returns>
    public virtual bool HasSameLocation(BlobStorageProvider other) => this.Equals((object) other);

    /// <summary>Tries to get the CDN URL for the given blob content.</summary>
    /// <param name="blobContent">Content of the BLOB.</param>
    /// <param name="cdn">The CDN url.</param>
    /// <returns></returns>
    protected internal virtual bool TryGetCdnUrl(IBlobContent blobContent, out string cdn)
    {
      cdn = (string) null;
      bool cdnUrl = false;
      if (!string.IsNullOrWhiteSpace(this.Cdn))
      {
        cdn = this.Cdn;
        cdnUrl = true;
      }
      return cdnUrl;
    }

    /// <summary>Tries to get the CDN URL for the given blob content.</summary>
    /// <param name="cdn">The CDN url.</param>
    /// <returns></returns>
    internal virtual bool TryGetCdnUrl(out string cdn)
    {
      cdn = (string) null;
      bool cdnUrl = false;
      if (!string.IsNullOrWhiteSpace(this.Cdn))
      {
        cdn = this.Cdn;
        cdnUrl = true;
      }
      return cdnUrl;
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>Array of Type-s that can be used in the generic IManager methods as arguments.</returns>
    public override Type[] GetKnownTypes() => new Type[0];

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (BlobStorageProvider);

    public override ISecuredObject GetSecurityRoot(bool create) => (ISecuredObject) null;

    public override ISecuredObject GetSecurityRoot() => (ISecuredObject) null;

    /// <summary>
    /// Gets a value indicating whether custom image size is allowed for current provider
    /// </summary>
    public bool CustomImageSizeAllowed { get; private set; }

    /// <summary>
    /// Gets or sets the base Url of CDN that could serve the blob item by reading it from Sitefinity.
    /// </summary>
    /// <value>The CDN base URL.</value>
    protected internal virtual string Cdn { get; set; }
  }
}
