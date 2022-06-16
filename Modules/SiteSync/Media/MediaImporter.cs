// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.MediaImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Data;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Cmis.RestAtom;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.Json;

namespace Telerik.Sitefinity.SiteSync
{
  internal class MediaImporter : SiteSyncImporter
  {
    internal const string ThumbnailSeparator = "-Thumbnail-Data-Separator-";
    internal const string StorageProviderEnabledSyncKey = "siteSyncBlobTransferEnabled";
    private const int ContentStreamBufferSize = 4096;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.MediaImporter" /> class.
    /// </summary>
    public MediaImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.MediaImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public MediaImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    internal override void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      this.ImportItem(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
    }

    protected void ImportItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      if (itemType == typeof (TaxonomyStatistic))
        this.ImportTaxonomyStatistic(transactionName, itemId, (object) item, provider);
      else if (typeof (Stream).IsAssignableFrom(itemType))
        this.ImportStream(item);
      else if (itemType == typeof (ScheduledTaskData))
      {
        base.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
      }
      else
      {
        LibrariesManager manager = LibrariesManager.GetManager(provider, transactionName);
        if (itemType == typeof (Folder))
        {
          this.ImportFolder(item, itemId, manager);
        }
        else
        {
          bool flag1 = typeof (MediaContent).IsAssignableFrom(itemType);
          if (flag1)
          {
            this.CheckForOldBlobContent(itemType, itemId, provider, transactionName, item);
            item.SetProperty("AutoGenerateUniqueUrl", (object) false);
          }
          bool flag2 = typeof (Library).IsAssignableFrom(itemType);
          if (flag2)
          {
            if (importTransaction.Headers.ContainsKey("MultisiteMigrationTarget"))
            {
              this.MigrateLibrary(item, itemType, itemId, provider, manager, importTransaction, postProcessingAction);
              return;
            }
            string urlName;
            if (this.IsDefaultLibrary(item, out urlName))
            {
              ILocatable locatable = manager.GetItems(itemType, "", "DateCreated DESC", 0, 1).OfType<ILocatable>().FirstOrDefault<ILocatable>();
              if (locatable != null && locatable.Id != itemId && locatable.UrlName == (Lstring) urlName)
              {
                manager.DeleteItem((object) locatable);
                TransactionManager.FlushTransaction(transactionName);
              }
            }
          }
          base.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
          if (flag1)
          {
            this.ImportMediaContent(item, itemId, itemType, provider, manager, transactionName);
            if (!importTransaction.Headers.ContainsKey("MultisiteMigrationTarget"))
              return;
            MediaContent mediaContent = (MediaContent) manager.GetItem(itemType, itemId);
            manager.Provider.RecompileItemUrls<MediaContent>(mediaContent);
          }
          else
          {
            if (!flag2)
              return;
            this.UpdateLibrary(itemId, manager, transactionName);
          }
        }
      }
    }

    protected override void RecompileItemUrls(Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade fluent, IDataItem dataItem)
    {
      if (dataItem is MediaContent)
        return;
      base.RecompileItemUrls(fluent, dataItem);
    }

    internal bool IsDefaultLibrary(WrapperObject item, out string urlName)
    {
      urlName = Reader.GetLstringValue(item.GetProperty("UrlName"));
      if (urlName == null)
        return false;
      return urlName == "default-album" || urlName == "default-document-library" || urlName == "default-video-library";
    }

    private void ImportMediaContent(
      WrapperObject item,
      Guid itemId,
      Type itemType,
      string provider,
      LibrariesManager manager,
      string transactionName)
    {
      MediaContent mediaContent = (MediaContent) manager.GetItem(itemType, itemId);
      if (mediaContent.Status == ContentLifecycleStatus.Master)
        this.RetrieveThumbnails(mediaContent, item, manager);
      else if (mediaContent.Status == ContentLifecycleStatus.Live)
      {
        MediaContent master = (MediaContent) App.Prepare().SetContentProvider(provider).SetTransactionName(transactionName).WorkWith().AnyContentItem(itemType).Manager.GetMaster((Content) mediaContent);
        mediaContent.TotalSize = master.TotalSize;
        mediaContent.FileId = master.FileId;
        mediaContent.FilePath = master.FilePath;
        manager.CopyThumbnails(master, mediaContent);
      }
      this.UpdateMediaFileUrls(mediaContent, item, manager);
      if (item.HasProperty("ThumbnailsVersion"))
        mediaContent.ThumbnailsVersion = Convert.ToInt32(item.GetPropertyOrDefault<long>("ThumbnailsVersion"));
      if (mediaContent.NumberOfChunks > 0 && BlobStorageManager.GetManager(mediaContent.BlobStorageProvider).Provider is ChunksBlobStorageProvider provider1 && manager.Provider is OpenAccessLibrariesProvider provider2)
        mediaContent.NumberOfChunks = provider1.GetChunksCount((IBlobContentLocation) mediaContent, (object) provider2.GetContext());
      this.PrepareStatisticsToRemove(transactionName, itemType, itemId, provider);
    }

    private void ImportStream(WrapperObject item)
    {
      ExtendedBlobContentProxy blobContent = this.CreateBlobContent((object) item);
      bool flag1 = ((int) MediaImporter.GetStorageProviderEnabledSyncValue(blobContent.BlobStorageProvider) ?? 0) != 0;
      bool flag2 = BlobStorageManager.GetManager(blobContent.BlobStorageProvider).Provider is IExternalBlobStorageProvider;
      if (this.BlobDataExists(blobContent) && !(flag2 & flag1))
      {
        this.SiteSyncLogger.Write((object) ("Skipped uploading" + this.PrepareBlobMessage(blobContent)));
      }
      else
      {
        this.UploadStream(blobContent, item);
        this.SiteSyncLogger.Write((object) ("Successfully uploaded" + this.PrepareBlobMessage(blobContent)));
      }
    }

    private void ImportFolder(WrapperObject item, Guid itemId, LibrariesManager manager)
    {
      Folder folder = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == itemId)) ?? manager.CreateFolder(itemId);
      var args = new{ Manager = manager, Item = folder };
      this.Serializer.SetProperties((object) folder, (object) item, (object) args);
      manager.ValidateFolderUrl(folder);
    }

    private void MigrateLibrary(
      WrapperObject item,
      Type itemType,
      Guid itemId,
      string provider,
      LibrariesManager manager,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      string urlName;
      if (this.IsDefaultLibrary(item, out urlName))
      {
        Library library = manager.GetLibraries().ToList<Library>().Where<Library>((Func<Library, bool>) (l => l.UrlName == (Lstring) urlName)).FirstOrDefault<Library>();
        if (library != null)
        {
          importTransaction.Response.Mappings.AddMapping(typeof (Library).FullName, itemId.ToString(), library.Id.ToString());
          itemId = library.Id;
        }
      }
      base.ImportItemInternal(manager.TransactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
    }

    protected override void RemoveDataItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      string provider,
      string language)
    {
      if (itemType == typeof (Folder))
      {
        LibrariesManager manager = LibrariesManager.GetManager(provider, transactionName);
        Folder folder = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == itemId));
        if (folder == null)
          return;
        manager.Delete((IFolder) folder);
      }
      else
        base.RemoveDataItem(transactionName, itemType, itemId, provider, language);
    }

    private ExtendedBlobContentProxy CreateBlobContent(object component)
    {
      ExtendedBlobContentProxy destination = new ExtendedBlobContentProxy();
      this.Serializer.SetProperties((object) destination, component, (Func<PropertyDescriptor, PropertyDescriptor, bool>) null, (object) null);
      return destination;
    }

    private bool BlobDataExists(MediaContent mediaContent) => BlobStorageManager.GetManager(mediaContent.GetStorageProviderName()).BlobExists((IBlobContentLocation) mediaContent);

    private bool BlobDataExists(ExtendedBlobContentProxy blobContent) => BlobStorageManager.GetManager(blobContent.BlobStorageProvider).BlobExists((IBlobContentLocation) blobContent);

    private void UploadStream(ExtendedBlobContentProxy blobContent, WrapperObject obj)
    {
      Stream propertyOrDefault = obj.GetPropertyOrDefault<Stream>("BlobStream");
      if (propertyOrDefault == null)
        return;
      using (propertyOrDefault)
      {
        int bufferSize = blobContent.ChunkSize > 0 ? blobContent.ChunkSize : 4096;
        BlobStorageManager.GetManager(blobContent.BlobStorageProvider).Provider.Upload((IBlobContent) blobContent, propertyOrDefault, bufferSize);
      }
    }

    private void UpdateMediaFileUrls(
      MediaContent content,
      WrapperObject obj,
      LibrariesManager manager)
    {
      if (!obj.HasProperty("MediaFileUrlsProp"))
        return;
      foreach (MediaFileUrl mediaFileUrl in JsonUtility.FromJson<List<MediaFileUrl>>(obj.GetPropertyOrDefault<string>("MediaFileUrlsProp")))
        manager.Provider.AddMediaFileUrl(content, mediaFileUrl.Url, mediaFileUrl.IsDefault, mediaFileUrl.RedirectToDefault, new int?(Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture)));
    }

    private void RetrieveThumbnails(
      MediaContent content,
      WrapperObject obj,
      LibrariesManager manager)
    {
      if (!obj.HasProperty("ThumbnailsStream"))
        return;
      foreach (Thumbnail thumbnail in content.GetThumbnails().ToList<Thumbnail>())
      {
        manager.Provider.Delete(thumbnail);
        content.Thumbnails.Remove(thumbnail);
      }
      List<string> propertyOrDefault = obj.GetPropertyOrDefault<List<string>>("ThumbnailsStream");
      if (propertyOrDefault == null)
        return;
      foreach (string str1 in propertyOrDefault)
      {
        string[] strArray = str1.Split(new string[1]
        {
          "-Thumbnail-Data-Separator-"
        }, StringSplitOptions.RemoveEmptyEntries);
        byte[] buffer = Convert.FromBase64String(strArray[0]);
        string name = strArray[1];
        string str2 = strArray[2];
        string s1 = strArray[3];
        string s2 = strArray[4];
        string str3 = strArray[5];
        Thumbnail thumbnail = new Thumbnail(manager.Provider.GetNewGuid(), name);
        thumbnail.Parent = content;
        thumbnail.Type = (ThumbnailTypes) Enum.Parse(typeof (ThumbnailTypes), str2);
        thumbnail.Width = int.Parse(s1);
        thumbnail.Height = int.Parse(s2);
        if (!string.IsNullOrEmpty(str3))
          thumbnail.MimeType = str3;
        manager.Provider.UploadDataOnly(thumbnail, (Stream) new MemoryStream(buffer));
        thumbnail.Culture = SystemManager.CurrentContext.Culture.Name;
        content.Thumbnails.Add(thumbnail);
      }
    }

    private void CheckForOldBlobContent(
      Type itemType,
      Guid itemId,
      string provider,
      string transaction,
      WrapperObject obj)
    {
      FluentSitefinity fluent = App.Prepare().SetContentProvider(provider).SetTransactionName(transaction).WorkWith();
      Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = this.GetFacade(itemType, fluent, provider);
      if (!facade.Exists(itemId))
        return;
      MediaContent mediaContent = (MediaContent) facade.Load(itemId).Get();
      Guid propertyOrDefault1 = obj.GetPropertyOrDefault<Guid>("FileId");
      if (!(mediaContent.FileId != Guid.Empty) || !(propertyOrDefault1 != mediaContent.FileId))
        return;
      LibrariesManager manager1 = LibrariesManager.GetManager(provider, transaction);
      string storageProviderName = mediaContent.GetStorageProviderName();
      BlobStorageManager manager2 = BlobStorageManager.GetManager(storageProviderName);
      string propertyOrDefault2 = obj.GetPropertyOrDefault<string>("FilePath");
      if (manager2.Provider is IExternalBlobStorageProvider && propertyOrDefault2 == mediaContent.FilePath)
        return;
      bool includeExternalProviders = ((int) MediaImporter.GetStorageProviderEnabledSyncValue(storageProviderName) ?? 0) != 0;
      IBlobContentLocation blobToDelete = manager1.Provider.GetBlobToDelete(manager2, storageProviderName, (IBlobContent) mediaContent, includeExternalProviders);
      if (blobToDelete == null)
        return;
      manager1.Provider.DeleteBlob(blobToDelete, manager2);
    }

    protected override void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction transaction)
    {
      if (dataItem is MediaContent mediaContent && !this.BlobDataExists(mediaContent))
        throw new ApplicationException("There is no blob data for this media content");
      base.SetAdditionalValues(dataItem, provider, wrapperObject, fluent, transaction);
    }

    private string PrepareBlobMessage(ExtendedBlobContentProxy blobContent) => string.Format(" a blob with file id '{0}' and provider '{1}', belonging to media content with id '{2}' and provider '{3}'", (object) blobContent.FileId, (object) blobContent.BlobStorageProvider, (object) blobContent.Id, (object) blobContent.Provider);

    private void UpdateLibrary(Guid itemId, LibrariesManager manager, string transactionName)
    {
      TransactionManager.FlushTransaction(transactionName);
      Library library = manager.GetLibraries().FirstOrDefault<Library>((Expression<Func<Library, bool>>) (x => x.Id == itemId));
      if (library == null || !library.NeedThumbnailsRegeneration)
        return;
      library.NeedThumbnailsRegeneration = false;
    }

    internal static bool? GetStorageProviderEnabledSyncValue(string providerName)
    {
      bool? enabledSyncValue = new bool?();
      BlobStorageConfigElement blobStorage = Config.Get<LibrariesConfig>().BlobStorage;
      DataProviderSettings providerConfig;
      if (blobStorage.Providers.TryGetValue(providerName, out providerConfig))
      {
        string parameter1 = providerConfig.Parameters["siteSyncBlobTransferEnabled"];
        if (!string.IsNullOrWhiteSpace(parameter1))
        {
          enabledSyncValue = new bool?(bool.Parse(parameter1));
        }
        else
        {
          BlobStorageTypeConfigElement typeConfigElement = blobStorage.BlobStorageTypes.Elements.SingleOrDefault<BlobStorageTypeConfigElement>((Func<BlobStorageTypeConfigElement, bool>) (t => t.ProviderType == providerConfig.ProviderType));
          if (typeConfigElement != null)
          {
            string parameter2 = typeConfigElement.Parameters["siteSyncBlobTransferEnabled"];
            enabledSyncValue = string.IsNullOrWhiteSpace(parameter2) ? new bool?(!typeof (IExternalBlobStorageProvider).IsAssignableFrom(typeConfigElement.ProviderType)) : new bool?(bool.Parse(parameter2));
          }
        }
      }
      return enabledSyncValue;
    }
  }
}
