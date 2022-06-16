// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobStorage.BlobStorageManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.BlobStorage
{
  /// <summary>Manages Blob content persistence</summary>
  public class BlobStorageManager : ManagerBase<BlobStorageProvider>
  {
    private ConfigElementDictionary<string, DataProviderSettings> providerSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:PublishingManager" /> class.
    /// </summary>
    public BlobStorageManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public BlobStorageManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public BlobStorageManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the upload stream of the content item.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual Stream GetUploadStream(IBlobContent content) => this.Provider.GetUploadStream(content);

    /// <summary>
    /// Downloads the binary data of the specified media content from the storage.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual Stream GetDownloadStream(IBlobContent content) => this.Provider.GetDownloadStream(content);

    /// <summary>Resolves the BLOB content location.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual IBlobContentLocation ResolveBlobContentLocation(
      IBlobContent content)
    {
      return this.Provider.ResolveBlobContentLocation(content);
    }

    /// <summary>Deletes the specified content.</summary>
    /// <param name="content">The content.</param>
    public virtual void Delete(IBlobContentLocation location) => this.Provider.Delete(location);

    /// <summary>
    /// Determines whether a blob items under the specified location is exists.
    /// </summary>
    /// <param name="location">The location.</param>
    /// <returns>True if the blob exists, otherwise - false</returns>
    public virtual bool BlobExists(IBlobContentLocation location) => this.Provider.BlobExists(location);

    /// <summary>Tests the connection.</summary>
    /// <param name="err">The error with the details about the problem.</param>
    /// <returns></returns>
    public virtual bool TestConnection(out Exception error) => this.Provider.TestConnection(out error);

    /// <summary>Gets an instance for BlobStorageManager.</summary>
    /// <returns>An instance of UserManager.</returns>
    public static BlobStorageManager GetManager() => BlobStorageManager.GetManager((string) null);

    /// <summary>
    /// Gets an instance for BlobStorageManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of UserManager.</returns>
    public static BlobStorageManager GetManager(string providerName) => BlobStorageManager.GetManager(providerName, true);

    /// <summary>
    /// Gets an instance for BlobStorageManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of UserManager.</returns>
    public static BlobStorageManager GetManager(
      string providerName,
      bool throwExceptionIfNotFound)
    {
      return ManagerBase<BlobStorageProvider>.GetManager<BlobStorageManager>(providerName);
    }

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<LibrariesConfig>().BlobStorage.DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public override string ModuleName => "BlobStorage";

    /// <summary>Gets the providers settings.</summary>
    /// <value>The providers settings.</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
    {
      get
      {
        if (this.providerSettings == null)
          this.providerSettings = Config.Get<LibrariesConfig>().BlobStorage.Providers;
        return this.providerSettings;
      }
    }
  }
}
