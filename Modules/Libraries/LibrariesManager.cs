// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Thumbnails;
using Telerik.Sitefinity.Modules.Libraries.Versioning;
using Telerik.Sitefinity.Modules.Libraries.Web;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Manages the data layer for Libraries.</summary>
  public class LibrariesManager : 
    ContentManagerBase<LibrariesDataProvider>,
    IContentLifecycleManager<Telerik.Sitefinity.Libraries.Model.Image>,
    IContentLifecycleManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IContentLifecycleManager<Telerik.Sitefinity.Libraries.Model.Document>,
    IContentLifecycleManager<Telerik.Sitefinity.Libraries.Model.Video>,
    IContentLifecycleManager<Telerik.Sitefinity.Libraries.Model.Album>,
    IContentLifecycleManager<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>,
    IContentLifecycleManager<Telerik.Sitefinity.Libraries.Model.VideoLibrary>,
    ILifecycleManager,
    ILanguageDataManager,
    IFolderManager,
    ISupportRecyclingManager
  {
    private ILifecycleDecorator lifecycle;
    private static readonly string librariesTempFolder;
    private IRecycleBinStrategy recycleBin;
    /// <summary>
    /// The key of the additional info dictionary item that persists the information for item's additional file urls.
    /// </summary>
    public const string FileAdditionalUrlsKey = "FileAdditionalUrlsKey";
    /// <summary>
    /// The key of the additional info dictionary item that persists the information for item's allow multiple file urls.
    /// </summary>
    public const string AllowMultipleFileUrlsKey = "AllowMultipleFileUrls";
    /// <summary>
    /// The key of the additional info dictionary item that persists the information if the item's additional file urls redirect to default.
    /// </summary>
    public const string RedirectToDefaultKey = "RedirectToDefault";
    /// <summary>
    /// The key of the additional info dictionary item that persists the information if the item's default file url.
    /// </summary>
    public const string DefaultFileUrlKey = "DefaultFileUrl";
    /// <summary>
    /// The key of the additional info dictionary item that persists the base url of the media file url.
    /// </summary>
    public const string MediaFileBaseUrlKey = "MediaFileBaseUrl";
    /// <summary>
    /// The key of the additional info dictionary item that persists the number of translations available for an item.
    /// </summary>
    public const string NumberOfTranslationsUrlKey = "NumberOfTranslations";
    /// <summary>
    /// The key of the additional info dictionary item that persists the information for item's default.
    /// </summary>
    public const string DefaultFileNameKey = "DefaultFileName";
    /// <summary>
    /// The key of the additional info dictionary item that persists the information of the full media url.
    /// </summary>
    public const string FullMediaUrlKey = "fullUrl";
    /// <summary>
    /// The key of the additional info dictionary item that persists the information of the full media url.
    /// </summary>
    public const string IsVectorGraphicsKey = "IsVectorGraphics";

    static LibrariesManager()
    {
      if (AzureRuntime.IsRunning)
        LibrariesManager.librariesTempFolder = AzureRuntime.LibrariesTempFolderPath;
      if (string.IsNullOrEmpty(LibrariesManager.librariesTempFolder))
      {
        LibrariesManager.librariesTempFolder = Path.Combine(SystemManager.AppDataFolderPhysicalPath, "Sitefinity\\Temp\\Libraries");
        if (!Directory.Exists(LibrariesManager.librariesTempFolder))
          Directory.CreateDirectory(LibrariesManager.librariesTempFolder);
      }
      ManagerBase<LibrariesDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(LibrariesManager.Provider_Executing);
    }

    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      LibrariesDataProvider provider1 = sender as LibrariesDataProvider;
      if (!(provider1.GetTransaction() is SitefinityOAContext transaction) || transaction.ContextOptions.EnableDataSynchronization)
        return;
      foreach (Library library in provider1.GetDirtyItems().OfType<Library>())
      {
        SecurityConstants.TransactionActionType dirtyItemStatus = provider1.GetDirtyItemStatus((object) library);
        if (transaction != null && dirtyItemStatus == SecurityConstants.TransactionActionType.Updated)
        {
          List<string> stringList1 = new List<string>();
          List<string> stringList2 = new List<string>();
          IList<string> thumbnailProfiles = library.ThumbnailProfiles;
          IList<string> originalValue = provider1.GetOriginalValue<IList<string>>((object) library, "ThumbnailProfiles");
          if (originalValue != null)
          {
            foreach (string str in (IEnumerable<string>) originalValue)
            {
              if (!thumbnailProfiles.Contains(str))
                stringList1.Add(str);
            }
            foreach (string str in (IEnumerable<string>) thumbnailProfiles)
            {
              if (!originalValue.Contains(str))
                stringList2.Add(str);
            }
          }
          else
            stringList2.AddRange((IEnumerable<string>) thumbnailProfiles);
          if (stringList2.Count > 0 || stringList1.Count > 0)
            library.NeedThumbnailsRegeneration = true;
        }
      }
      foreach (Telerik.Sitefinity.Libraries.Model.Image image1 in provider1.GetDirtyItems().OfType<Telerik.Sitefinity.Libraries.Model.Image>().Where<Telerik.Sitefinity.Libraries.Model.Image>((Func<Telerik.Sitefinity.Libraries.Model.Image, bool>) (img => img.Status == ContentLifecycleStatus.Deleted)))
      {
        Telerik.Sitefinity.Libraries.Model.Image deletedImage = image1;
        IQueryable<Telerik.Sitefinity.Libraries.Model.Album> albums = provider1.GetAlbums();
        Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>> predicate1 = (Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (album => album.CoverId.HasValue && album.CoverId.Value == deletedImage.Id);
        foreach (Telerik.Sitefinity.Libraries.Model.Album album in (IEnumerable<Telerik.Sitefinity.Libraries.Model.Album>) albums.Where<Telerik.Sitefinity.Libraries.Model.Album>(predicate1))
        {
          Telerik.Sitefinity.Libraries.Model.Album library = album;
          Telerik.Sitefinity.Libraries.Model.Image image2 = provider1.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (img => img.Parent.Id == library.Id)).OrderBy<Telerik.Sitefinity.Libraries.Model.Image, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
          if (image2 != null)
            library.CoverId = new Guid?(image2.Id);
          else
            library.CoverId = new Guid?();
        }
        IQueryable<Folder> folders = ((IFolderOAProvider) provider1).GetFolders();
        Expression<Func<Folder, bool>> predicate2 = (Expression<Func<Folder, bool>>) (fld => fld.CoverId.HasValue && fld.CoverId.Value == deletedImage.Id);
        foreach (Folder folder1 in (IEnumerable<Folder>) folders.Where<Folder>(predicate2))
        {
          Folder folder = folder1;
          Telerik.Sitefinity.Libraries.Model.Image image3 = provider1.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (img => img.FolderId == (Guid?) folder.Id)).OrderBy<Telerik.Sitefinity.Libraries.Model.Image, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
          folder.CoverId = image3 == null ? new Guid?() : new Guid?(image3.Id);
        }
      }
      IEnumerable<Thumbnail> thumbnails = provider1.GetDirtyItems().OfType<Thumbnail>().Where<Thumbnail>((Func<Thumbnail, bool>) (t => t.Image != null));
      List<Thumbnail> source1 = new List<Thumbnail>();
      foreach (Thumbnail thumbnail1 in thumbnails)
      {
        Thumbnail thumbnail = thumbnail1;
        switch (provider1.GetDirtyItemStatus((object) thumbnail))
        {
          case SecurityConstants.TransactionActionType.New:
          case SecurityConstants.TransactionActionType.Updated:
            System.Drawing.Image image = (System.Drawing.Image) thumbnail.Image;
            Thumbnail thumbnail2 = source1.FirstOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.FileId == thumbnail.FileId));
            if (thumbnail2 != null)
            {
              thumbnail.TotalSize = thumbnail2.TotalSize;
              ((IChunksBlobContent) thumbnail).ChunkSize = ((IChunksBlobContent) thumbnail2).ChunkSize;
              ((IChunksBlobContent) thumbnail).NumberOfChunks = ((IChunksBlobContent) thumbnail2).NumberOfChunks;
              ((IChunksBlobContent) thumbnail).Uploaded = ((IChunksBlobContent) thumbnail2).Uploaded;
            }
            else
            {
              using (MemoryStream source2 = new MemoryStream())
              {
                ImagesHelper.SaveImageToStream(image, (Stream) source2, thumbnail.MimeType, true);
                BlobStorageProvider provider2 = ((LibrariesDataProvider) thumbnail.Parent.Provider).GetBlobStorageManager((IBlobContent) thumbnail).Provider;
                provider1.UploadToStorage(new BlobItemWrapper((IBlobContent) thumbnail), (Stream) source2, provider2);
              }
            }
            image.Dispose();
            thumbnail.Image = (object) null;
            source1.Add(thumbnail);
            continue;
          default:
            continue;
        }
      }
      LibrariesManager.UpdateMediaFilesAdditionalUrls(provider1);
    }

    /// <summary>
    /// Updates the additional library URLs cache table for the dirty items
    /// of the provider.
    /// </summary>
    private static void UpdateMediaFilesAdditionalUrls(LibrariesDataProvider provider)
    {
      if (!Config.Get<LibrariesConfig>().MediaFileAdditionalUrls.AdditionalUrlsToFiles)
        return;
      Dictionary<Guid, MediaContent> contentWithChangedUrls = LibrariesManager.GetMediaContentWithChangedUrls(provider);
      if (!contentWithChangedUrls.Any<KeyValuePair<Guid, MediaContent>>())
        return;
      MediaFileAdditionalUrlsManager relatedManager = provider.GetRelatedManager<MediaFileAdditionalUrlsManager>((string) null);
      Dictionary<string, MediaFileAdditionalUrl> source = new Dictionary<string, MediaFileAdditionalUrl>();
      Dictionary<Guid, Dictionary<string, MediaFileAdditionalUrl>> originalUrls = LibrariesManager.GetOriginalUrls(relatedManager, provider.Name, (IEnumerable<MediaContent>) contentWithChangedUrls.Values);
      Dictionary<Guid, Dictionary<string, bool>> newAdditionalUrls = LibrariesManager.GetNewAdditionalUrls((IEnumerable<MediaContent>) contentWithChangedUrls.Values, provider);
      foreach (MediaContent mediaContent in contentWithChangedUrls.Values)
      {
        Dictionary<string, MediaFileAdditionalUrl> dictionary1 = originalUrls[mediaContent.OriginalContentId != Guid.Empty ? mediaContent.OriginalContentId : mediaContent.Id];
        Dictionary<string, bool> dictionary2 = newAdditionalUrls[mediaContent.Id];
        foreach (KeyValuePair<string, MediaFileAdditionalUrl> keyValuePair in dictionary1)
        {
          if (!dictionary2.ContainsKey(keyValuePair.Key))
          {
            if (mediaContent.OriginalContentId == Guid.Empty && keyValuePair.Value.MasterHasIt)
              keyValuePair.Value.MasterHasIt = false;
            else if (mediaContent.Status == ContentLifecycleStatus.Live && keyValuePair.Value.LiveItemId != Guid.Empty)
              keyValuePair.Value.LiveItemId = Guid.Empty;
            if (!keyValuePair.Value.MasterHasIt && keyValuePair.Value.LiveItemId == Guid.Empty)
              source[keyValuePair.Key] = keyValuePair.Value;
          }
        }
      }
      foreach (MediaContent mediaContent in contentWithChangedUrls.Values)
      {
        if (!mediaContent.IsDeleted)
        {
          Dictionary<string, MediaFileAdditionalUrl> dictionary = originalUrls[mediaContent.OriginalContentId != Guid.Empty ? mediaContent.OriginalContentId : mediaContent.Id];
          foreach (KeyValuePair<string, bool> keyValuePair in newAdditionalUrls[mediaContent.Id])
          {
            if (!dictionary.ContainsKey(keyValuePair.Key))
            {
              MediaFileAdditionalUrl fileAdditionalUrl;
              if (source.ContainsKey(keyValuePair.Key))
              {
                fileAdditionalUrl = source[keyValuePair.Key];
                source.Remove(keyValuePair.Key);
              }
              else
                fileAdditionalUrl = relatedManager.CreateMediaFileAdditionalUrl(keyValuePair.Key, provider.Name, Guid.Empty, false, Guid.Empty, false);
              if (mediaContent.OriginalContentId == Guid.Empty)
              {
                fileAdditionalUrl.ItemId = mediaContent.Id;
                fileAdditionalUrl.LiveItemId = Guid.Empty;
                fileAdditionalUrl.RedirectToDefault = false;
                fileAdditionalUrl.MasterHasIt = true;
              }
              else
              {
                fileAdditionalUrl.ItemId = mediaContent.OriginalContentId;
                fileAdditionalUrl.LiveItemId = mediaContent.Id;
                fileAdditionalUrl.RedirectToDefault = keyValuePair.Value;
                fileAdditionalUrl.MasterHasIt = false;
              }
              dictionary[keyValuePair.Key] = fileAdditionalUrl;
            }
            else if (mediaContent.OriginalContentId == Guid.Empty && !dictionary[keyValuePair.Key].MasterHasIt)
              dictionary[keyValuePair.Key].MasterHasIt = true;
            else if (mediaContent.Status == ContentLifecycleStatus.Live && dictionary[keyValuePair.Key].LiveItemId == Guid.Empty)
            {
              dictionary[keyValuePair.Key].LiveItemId = mediaContent.Id;
              dictionary[keyValuePair.Key].RedirectToDefault = keyValuePair.Value;
            }
            else if (mediaContent.Status == ContentLifecycleStatus.Live && dictionary[keyValuePair.Key].RedirectToDefault != keyValuePair.Value)
              dictionary[keyValuePair.Key].RedirectToDefault = keyValuePair.Value;
          }
        }
      }
      if (!source.Any<KeyValuePair<string, MediaFileAdditionalUrl>>())
        return;
      relatedManager.Delete((IEnumerable<MediaFileAdditionalUrl>) source.Values);
    }

    /// <summary>
    /// Detects changed MediaContent items with changed URLs in the dirty items of the provider.
    /// </summary>
    /// <returns>Dictionary itemId =&gt; MediaContent</returns>
    private static Dictionary<Guid, MediaContent> GetMediaContentWithChangedUrls(
      LibrariesDataProvider provider)
    {
      IEnumerable<MediaContent> source1 = provider.GetDirtyItems().OfType<MediaContent>().Where<MediaContent>((Func<MediaContent, bool>) (mc => mc.Status == ContentLifecycleStatus.Master || mc.Status == ContentLifecycleStatus.Live || mc.Status == ContentLifecycleStatus.Deleted));
      IEnumerable<MediaContent> source2 = source1.Where<MediaContent>((Func<MediaContent, bool>) (mc => provider.IsFieldDirty((object) mc, "MediaFileLinks") || provider.IsFieldDirty((object) mc, "Status") || provider.IsFieldDirty((object) mc, "Visible")));
      List<MediaContent> list = provider.GetDirtyItems().OfType<MediaFileUrl>().Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (mfu => !mfu.IsDefault && mfu.MediaFileLink != null && mfu.MediaFileLink.MediaContent != null)).Select<MediaFileUrl, MediaContent>((Func<MediaFileUrl, MediaContent>) (mfu => mfu.MediaFileLink.MediaContent)).Where<MediaContent>((Func<MediaContent, bool>) (mc => mc.Status == ContentLifecycleStatus.Master || mc.Status == ContentLifecycleStatus.Live || mc.Status == ContentLifecycleStatus.Deleted)).ToList<MediaContent>();
      foreach (MediaFileUrl entity in provider.GetDirtyItems().OfType<MediaFileUrl>().Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (mfu => provider.GetDirtyItemStatus((object) mfu) == SecurityConstants.TransactionActionType.Deleted)))
      {
        MediaFileLink originalValue = provider.GetOriginalValue<MediaFileLink>((object) entity, "MediaFileLink");
        if (originalValue != null)
        {
          MediaContent mc = provider.GetOriginalValue<MediaContent>((object) originalValue, "MediaContent");
          if (mc != null && (mc.Status == ContentLifecycleStatus.Master || mc.Status == ContentLifecycleStatus.Live || mc.Status == ContentLifecycleStatus.Deleted) && !list.Any<MediaContent>((Func<MediaContent, bool>) (c => c.Id == mc.Id)))
            list.Add(mc);
        }
      }
      Dictionary<Guid, MediaContent> changedContentItems = new Dictionary<Guid, MediaContent>();
      if (!source2.Any<MediaContent>() && !list.Any<MediaContent>())
        return changedContentItems;
      foreach (MediaContent mediaContent in list)
        changedContentItems[mediaContent.Id] = mediaContent;
      foreach (MediaContent mediaContent in source2)
        changedContentItems[mediaContent.Id] = mediaContent;
      IEnumerable<MediaContent> mediaContents1 = source1.Where<MediaContent>((Func<MediaContent, bool>) (mc => changedContentItems.ContainsKey(mc.OriginalContentId)));
      HashSet<Guid> masterIds = new HashSet<Guid>(changedContentItems.Values.Select<MediaContent, Guid>((Func<MediaContent, Guid>) (mc => mc.OriginalContentId)).Where<Guid>((Func<Guid, bool>) (id => id != Guid.Empty)));
      IEnumerable<MediaContent> mediaContents2 = source1.Where<MediaContent>((Func<MediaContent, bool>) (mc => masterIds.Contains(mc.Id)));
      foreach (MediaContent mediaContent in mediaContents1)
        changedContentItems[mediaContent.Id] = mediaContent;
      foreach (MediaContent mediaContent in mediaContents2)
        changedContentItems[mediaContent.Id] = mediaContent;
      return changedContentItems;
    }

    private static Dictionary<Guid, Dictionary<string, bool>> GetNewAdditionalUrls(
      IEnumerable<MediaContent> items,
      LibrariesDataProvider provider)
    {
      Dictionary<Guid, Dictionary<string, bool>> newAdditionalUrls = new Dictionary<Guid, Dictionary<string, bool>>();
      foreach (MediaContent itemInTransaction in items)
      {
        Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
        newAdditionalUrls[itemInTransaction.Id] = dictionary;
        if (!itemInTransaction.IsDeleted && provider.GetDirtyItemStatus((object) itemInTransaction) != SecurityConstants.TransactionActionType.Deleted && (itemInTransaction.Status != ContentLifecycleStatus.Live || itemInTransaction.Visible))
        {
          MediaFileLink fileLink = itemInTransaction.GetFileLink();
          if (fileLink != null && fileLink.Urls != null && fileLink.Urls.Count != 0)
          {
            foreach (MediaFileUrl url in (IEnumerable<MediaFileUrl>) fileLink.Urls)
            {
              if (!url.IsDefault)
              {
                string str = LibrariesManager.CanonifyUrl(url.Url);
                if (!provider.IsStandardMediaContentUrl(str, itemInTransaction))
                  dictionary[str] = url.RedirectToDefault;
              }
            }
          }
        }
      }
      return newAdditionalUrls;
    }

    /// <summary>
    /// Searches the additional media files cache table for the additional URLs
    /// of a set of items.
    /// </summary>
    /// <returns>Dictionary itemId =&gt; (Dictionary URL =&gt; MediaFileAdditionalUrl).</returns>
    private static Dictionary<Guid, Dictionary<string, MediaFileAdditionalUrl>> GetOriginalUrls(
      MediaFileAdditionalUrlsManager manager,
      string providerName,
      IEnumerable<MediaContent> items)
    {
      IEnumerable<Guid> originalContentIds = items.Select<MediaContent, Guid>((Func<MediaContent, Guid>) (mc => !(mc.OriginalContentId != Guid.Empty) ? mc.Id : mc.OriginalContentId));
      IEnumerable<MediaFileAdditionalUrl> fileAdditionalUrls = (IEnumerable<MediaFileAdditionalUrl>) manager.GetMediaFileAdditionalUrls().Where<MediaFileAdditionalUrl>((Expression<Func<MediaFileAdditionalUrl, bool>>) (mfu => mfu.ProviderName == providerName && originalContentIds.Contains<Guid>(mfu.ItemId)));
      Dictionary<Guid, Dictionary<string, MediaFileAdditionalUrl>> originalUrls = new Dictionary<Guid, Dictionary<string, MediaFileAdditionalUrl>>();
      foreach (Guid key in originalContentIds)
        originalUrls[key] = new Dictionary<string, MediaFileAdditionalUrl>();
      foreach (MediaFileAdditionalUrl fileAdditionalUrl in fileAdditionalUrls)
        originalUrls[fileAdditionalUrl.ItemId][LibrariesManager.CanonifyUrl(fileAdditionalUrl.Url)] = fileAdditionalUrl;
      return originalUrls;
    }

    internal static string GenerateUrlName(string providerName) => providerName.Replace(' ', '-').ToLower().TrimEnd('/');

    internal static string CanonifyUrl(string url)
    {
      url = url.ToLower();
      if (url.StartsWith("~/"))
        return url;
      return url.StartsWith("/") ? "~" + url : "~/" + url;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesManager" /> class.
    /// </summary>
    public LibrariesManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public LibrariesManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public LibrariesManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the provider base url.</summary>
    /// <param name="item">The media content item. Used to distinguish whether the item is image, video or doc.</param>
    /// <returns>The constructed Url</returns>
    internal string GetMediaContentProviderUrl(MediaContent item) => this.Provider.CompileMediaFileUrl(item);

    /// <summary>
    /// Gets the thumbnail generator that will be used to create image thumbnails.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.IImageProcessor" />.
    /// </returns>
    internal static IImageProcessor GetThumbnailGenerator() => ObjectFactory.Resolve<IImageProcessor>();

    /// <summary>
    /// Moves the item and all its versions to the specified library.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="library">The library.</param>
    public void MoveItemToLibrary(MediaContent item, Library library) => this.ChangeItemParent((Content) item, (Content) library, true);

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<LibrariesConfig>().DefaultProvider);

    protected override bool TryGetProviderSettings(
      string providerName,
      string dataSourceName,
      out IDataProviderSettings settings)
    {
      if (!base.TryGetProviderSettings(providerName, dataSourceName, out settings))
        return false;
      if (settings is VirtualDataProviderSettings)
        settings.Parameters["urlName"] = LibrariesManager.GenerateUrlName(providerName);
      return true;
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "Libraries";

    /// <summary>Returns all providers from the configuration</summary>
    /// <value>Collection of providers</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<LibrariesConfig>().Providers;

    /// <summary>
    /// Get an instance of the libraries manager using the default provider
    /// </summary>
    /// <returns>Instance of libraries manager</returns>
    public static LibrariesManager GetManager() => ManagerBase<LibrariesDataProvider>.GetManager<LibrariesManager>();

    /// <summary>
    /// Get an instance of the libraries manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the libraries manager</returns>
    public static LibrariesManager GetManager(string providerName) => ManagerBase<LibrariesDataProvider>.GetManager<LibrariesManager>(providerName);

    /// <summary>
    /// Resolve libraries manager with specific provider (by name) that works in a named transaction
    /// </summary>
    /// <param name="providerName">Name of the libraries provider to resolve</param>
    /// <param name="transactionName">Named transaction name</param>
    /// <returns>Instance of LibrariesManager with properly resolved and initiated provider that works in a named transaction</returns>
    public static LibrariesManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<LibrariesDataProvider>.GetManager<LibrariesManager>(providerName, transactionName);
    }

    internal static string GetProviderNameFromUrl(string providerUrlName)
    {
      LibrariesDataProvider librariesDataProvider = ManagerBase<LibrariesDataProvider>.StaticProvidersCollection.Where<LibrariesDataProvider>((Func<LibrariesDataProvider, bool>) (p => p.UrlName.Equals(providerUrlName, StringComparison.OrdinalIgnoreCase))).SingleOrDefault<LibrariesDataProvider>();
      if (librariesDataProvider != null)
        return librariesDataProvider.Name;
      if (SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext)
      {
        string fullName = typeof (LibrariesManager).FullName;
        string keyValue = providerUrlName;
        ISiteDataSource siteDataSource = multisiteContext.GetDataSourcesByManagerAndKey(fullName, "UrlName", keyValue, (Func<ISiteDataSource, string>) (s => LibrariesManager.GenerateUrlName(s.Provider))).FirstOrDefault<ISiteDataSource>();
        if (siteDataSource != null)
          return siteDataSource.Provider;
      }
      return ManagerBase<LibrariesDataProvider>.GetDefaultProviderName();
    }

    internal static LibrariesManager GetSystemLibrariesManager()
    {
      if (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection == null)
        LibrariesManager.GetManager();
      return LibrariesManager.GetManager((ManagerBase<LibrariesDataProvider>.StaticProvidersCollection.Where<LibrariesDataProvider>((Func<LibrariesDataProvider, bool>) (p => p.ProviderGroup == "System")).FirstOrDefault<LibrariesDataProvider>() ?? throw new ArgumentNullException("Missing libraries provider with 'System' provider group. The library provider should be created with parameter key 'providerGroup' and value 'System'.")).Name);
    }

    protected internal override void DeleteLanguageVersion(object item, CultureInfo language)
    {
      if (item is MediaContent mediaContent && ((IEnumerable<CultureInfo>) mediaContent.AvailableCultures).Contains<CultureInfo>(language))
      {
        foreach (Thumbnail thumbnail in mediaContent.GetThumbnails().ToList<Thumbnail>())
          this.Provider.Delete(thumbnail);
        MediaFileLink fileLink = mediaContent.GetFileLink(cultureId: new int?(AppSettings.CurrentSettings.GetCultureLcid(language)));
        if (fileLink != null)
          this.Delete(fileLink);
        mediaContent.MediaFileUrlName.SetString(language, (string) null);
      }
      base.DeleteLanguageVersion(item, language);
    }

    /// <summary>Gets the library.</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    protected internal virtual Library GetLibrary(Guid id) => this.Provider.GetLibrary(id);

    internal Type GetLibraryType(Guid libraryOrFolderId)
    {
      IFolder folderById = this.FindFolderById(libraryOrFolderId);
      if (folderById is Folder folder && folder.RootId != Guid.Empty)
        folderById = this.FindFolderById(folder.RootId);
      return folderById?.GetType();
    }

    internal virtual IQueryable<Library> GetLibraryNeighbours(
      Guid id,
      string sortExpression)
    {
      this.GetLibraries();
      Type type = (this.GetFolder(id) ?? throw new ArgumentNullException("The id in the argument isn't valid library id.")).GetType();
      IQueryable<Library> source;
      if (type == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        source = (IQueryable<Library>) this.GetAlbums();
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
      {
        source = (IQueryable<Library>) this.GetVideoLibraries();
      }
      else
      {
        if (!(type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary)))
          throw new ArgumentException("The library with the given id has an unknown type!");
        source = (IQueryable<Library>) this.GetDocumentLibraries();
      }
      if (sortExpression != null)
      {
        using (new CultureRegion(CultureInfo.InvariantCulture))
          source = source.OrderBy<Library>(sortExpression);
      }
      return source;
    }

    /// <summary>Get a query for all media items</summary>
    /// <returns>Queryable object for all MediaItems</returns>
    protected internal virtual IQueryable<MediaContent> GetMediaItems() => this.Provider.GetMediaItems();

    /// <summary>Gets the libraries.</summary>
    protected internal virtual IQueryable<Library> GetLibraries() => this.Provider.GetLibraries();

    /// <summary>Gets a media item by its ID.</summary>
    /// <param name="id">The ID of the requested media item.</param>
    /// <returns>An instance of the media item.</returns>
    public virtual MediaContent GetMediaItem(Guid id) => this.Provider.GetMediaItem(id);

    /// <summary>Mark an library for removal</summary>
    /// <param name="libraryToDelete">The library to delete.</param>
    public virtual void DeleteLibrary(Library libraryToDelete) => this.Provider.Delete(libraryToDelete);

    internal Library GetDefaultLibrary(Type mediaContentType)
    {
      if (mediaContentType == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        return (Library) this.GetAlbums().OrderBy<Telerik.Sitefinity.Libraries.Model.Album, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, DateTime>>) (a => a.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Album>();
      if (mediaContentType == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        return (Library) this.GetDocumentLibraries().OrderBy<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, DateTime>>) (l => l.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
      if (!(mediaContentType == typeof (Telerik.Sitefinity.Libraries.Model.Video)))
        return (Library) null;
      return (Library) this.GetVideoLibraries().OrderBy<Telerik.Sitefinity.Libraries.Model.VideoLibrary, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, DateTime>>) (l => l.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.VideoLibrary>();
    }

    /// <summary>
    /// Create a new <c>Album</c> and choose a random identity
    /// </summary>
    /// <returns>Created <c>Album</c> instance</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Album CreateAlbum() => this.Provider.CreateAlbum();

    /// <summary>
    /// Create a new <c>Album</c> with a explicitly specified identity
    /// </summary>
    /// <param name="id"><c>Album</c> identity</param>
    /// <returns>Created <c>Album</c> instance</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Album CreateAlbum(Guid id) => this.Provider.CreateAlbum(id);

    /// <summary>Get a queryable object for all Albums</summary>
    /// <returns>Queryable object for all Albums</returns>
    public virtual IQueryable<Telerik.Sitefinity.Libraries.Model.Album> GetAlbums() => this.Provider.GetAlbums();

    /// <summary>Search for an Album by its identity</summary>
    /// <param name="id">Searched Album identity</param>
    /// <returns>Found Album.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Album GetAlbum(Guid id) => this.Provider.GetAlbum(id);

    /// <summary>Mark an Album for removal</summary>
    /// <param name="albumToDelete">The Album to delete.</param>
    public virtual void DeleteAlbum(Telerik.Sitefinity.Libraries.Model.Album albumToDelete) => this.Provider.Delete((Library) albumToDelete);

    /// <summary>Creates a new video library.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.VideoLibrary CreateVideoLibrary() => this.Provider.CreateVideoLibrary();

    /// <summary>
    /// Creates a new video library with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the video library to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.VideoLibrary CreateVideoLibrary(
      Guid id)
    {
      return this.Provider.CreateVideoLibrary(id);
    }

    /// <summary>Gets a queryable object for all video libraries.</summary>
    /// <returns>Queryable object for all video libraries.</returns>
    public virtual IQueryable<Telerik.Sitefinity.Libraries.Model.VideoLibrary> GetVideoLibraries() => this.Provider.GetVideoLibraries();

    /// <summary>Gets a video library by its identity.</summary>
    /// <param name="id">The identity of the video library to get.</param>
    /// <returns>
    /// The <see cref="T:Telerik.Sitefinity.Libraries.Model.VideoLibrary" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.VideoLibrary GetVideoLibrary(
      Guid id)
    {
      return this.Provider.GetVideoLibrary(id);
    }

    /// <summary>Deletes the video library.</summary>
    /// <param name="libraryToDelete">The library to delete.</param>
    public virtual void DeleteVideoLibrary(Telerik.Sitefinity.Libraries.Model.VideoLibrary libraryToDelete) => this.Provider.Delete((Library) libraryToDelete);

    /// <summary>Generates the video thumbnails.</summary>
    /// <param name="video">The video.</param>
    public void GenerateVideoThumbnails(Telerik.Sitefinity.Libraries.Model.Video video) => this.GenerateVideoThumbnails(video, new FileInfo(this.GetItemTemporaryFilePath((MediaContent) video, (string) video.UrlName + video.Extension)));

    /// <summary>Generates the video thumbnail.</summary>
    /// <param name="video">The video.</param>
    /// <param name="videofile">The video file.</param>
    public virtual void GenerateVideoThumbnails(Telerik.Sitefinity.Libraries.Model.Video video, FileInfo videofile)
    {
      try
      {
        this.Provider.GenerateVideoThumbnails(video, videofile);
      }
      catch (Exception ex)
      {
        Log.Write((object) ex, "Video");
      }
    }

    /// <summary>Creates a new document library.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.DocumentLibrary CreateDocumentLibrary() => this.Provider.CreateDocumentLibrary();

    /// <summary>
    /// Creates a new document library with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the document library to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.DocumentLibrary CreateDocumentLibrary(
      Guid id)
    {
      return this.Provider.CreateDocumentLibrary(id);
    }

    /// <summary>Gets a queryable object for all document libraries.</summary>
    /// <returns>Queryable object for all document libraries.</returns>
    public virtual IQueryable<Telerik.Sitefinity.Libraries.Model.DocumentLibrary> GetDocumentLibraries() => this.Provider.GetDocumentLibraries();

    /// <summary>Gets a document library by its identity.</summary>
    /// <param name="id">The identity of the document library to get.</param>
    /// <returns>
    /// The <see cref="T:Telerik.Sitefinity.Libraries.Model.DocumentLibrary" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.DocumentLibrary GetDocumentLibrary(
      Guid id)
    {
      return this.Provider.GetDocumentLibrary(id);
    }

    /// <summary>Deletes the document library.</summary>
    /// <param name="libraryToDelete">The library to delete.</param>
    public virtual void DeleteDocumentLibrary(Telerik.Sitefinity.Libraries.Model.DocumentLibrary libraryToDelete) => this.Provider.Delete((Library) libraryToDelete);

    /// <summary>Create a new image and choose a random identity</summary>
    /// <returns>Created Image instance.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Image CreateImage() => this.Provider.CreateImage();

    /// <summary>Create an image by explicitly set its identity</summary>
    /// <param name="id">Identity of the Image to create</param>
    /// <returns>Created Image instance.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Image CreateImage(Guid id) => this.Provider.CreateImage(id);

    /// <summary>Get a query for all images</summary>
    /// <returns>Queryable object for all images</returns>
    public virtual IQueryable<Telerik.Sitefinity.Libraries.Model.Image> GetImages() => (IQueryable<Telerik.Sitefinity.Libraries.Model.Image>) this.Provider.GetImages().OrderByDescending<Telerik.Sitefinity.Libraries.Model.Image, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, DateTime>>) (i => i.DateCreated));

    /// <summary>Search for an image by its identity</summary>
    /// <param name="id">Identity of the searched image</param>
    /// <returns>Found image.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Image GetImage(Guid id) => this.Provider.GetImage(id);

    /// <summary>Mark an image for removal</summary>
    /// <param name="imageToDelete">The image to delete.</param>
    public virtual void DeleteImage(Telerik.Sitefinity.Libraries.Model.Image imageToDelete) => this.Provider.Delete(imageToDelete);

    /// <summary>
    /// Checks in the content in the temp state. Content becomes draft after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image CheckIn(Telerik.Sitefinity.Libraries.Model.Image image) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.CheckIn((ILifecycleDataItem) image);

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image CheckOut(Telerik.Sitefinity.Libraries.Model.Image image) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.CheckOut((ILifecycleDataItem) image);

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image Edit(Telerik.Sitefinity.Libraries.Model.Image image) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.Edit((ILifecycleDataItem) image);

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image Publish(Telerik.Sitefinity.Libraries.Model.Image image) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.Publish((ILifecycleDataItem) image);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image Unpublish(Telerik.Sitefinity.Libraries.Model.Image cnt) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.Unpublish((ILifecycleDataItem) cnt);

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    [Obsolete("Use the Lifecycle property")]
    public Telerik.Sitefinity.Libraries.Model.Image Schedule(
      Telerik.Sitefinity.Libraries.Model.Image item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      return this.Provider.Schedule<Telerik.Sitefinity.Libraries.Model.Image>(item, publicationDate, expirationDate, new Action<Telerik.Sitefinity.Libraries.Model.Image, Telerik.Sitefinity.Libraries.Model.Image>(this.CopyImage), this.GetImages());
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all Images</param>
    /// <returns>ID of the user that checked out the item or Guid.Empty if the item is not checked out.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Guid GetCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Image item) => this.Lifecycle.GetCheckedOutBy((ILifecycleDataItem) item);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TItem" />-s</param>
    /// <returns>True if the item is checked out, false otherwise.</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOut(Telerik.Sitefinity.Libraries.Model.Image item) => this.Lifecycle.IsCheckedOut((ILifecycleDataItem) item);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TItem" />-s.</param>
    /// <returns>True if it was checked out by a user with the specified id, false otherwise</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Image item, Guid userId) => this.Lifecycle.IsCheckedOutBy((ILifecycleDataItem) item, userId);

    [Obsolete("Use the Copy method with the culture parameter")]
    private void CopyImage(Telerik.Sitefinity.Libraries.Model.Image source, Telerik.Sitefinity.Libraries.Model.Image destination)
    {
      this.CopyMediaContent((MediaContent) source, (MediaContent) destination);
      ((IHasParent) destination).Parent = (Content) source.Album;
      destination.Height = source.Height;
      destination.Width = source.Width;
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image GetLive(Telerik.Sitefinity.Libraries.Model.Image cnt) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.GetLive((ILifecycleDataItem) cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image GetTemp(Telerik.Sitefinity.Libraries.Model.Image cnt) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.GetTemp((ILifecycleDataItem) cnt);

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <typeparam name="TContent">Type of the content item</typeparam>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwise, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Image GetMaster(Telerik.Sitefinity.Libraries.Model.Image cnt) => (Telerik.Sitefinity.Libraries.Model.Image) this.Lifecycle.GetMaster((ILifecycleDataItem) cnt);

    /// <summary>
    /// Creates a thumbnail by finding the thumbnail profile from already registered profiles
    /// </summary>
    /// <param name="liveMediaContent"></param>
    /// <param name="thumbnailName"></param>
    /// <returns></returns>
    public Thumbnail CreateImageThumbnail(
      MediaContent liveMediaContent,
      string thumbnailName)
    {
      return LazyThumbnailGenerator.Instance.CreateThumbnail(liveMediaContent, thumbnailName);
    }

    /// <summary>
    /// Creates a thumbnail by finding the thumbnail profile by specifying image method and method parameters
    /// </summary>
    /// <param name="liveMediaContent"></param>
    /// <param name="thumbnailName"></param>
    /// <param name="imageMethod"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Thumbnail CreateImageThumbnail(
      MediaContent liveMediaContent,
      string imageMethod,
      NameValueCollection parameters)
    {
      return LazyThumbnailGenerator.Instance.CreateThumbnail(liveMediaContent, (string) null, imageMethod, parameters);
    }

    /// <summary>Generates the thumbnail of the media content item.</summary>
    /// <param name="mediaContent">The media content item.</param>
    internal void RegenerateThumbnails(MediaContent mediaContent, params string[] profilesFilter) => this.Provider.RegenerateThumbnails(mediaContent, profilesFilter);

    /// <summary>Creates a new video.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Video CreateVideo() => this.Provider.CreateVideo();

    /// <summary>
    /// Creates a new video with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the video to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Video CreateVideo(Guid id) => this.Provider.CreateVideo(id);

    /// <summary>Gets a query for all videos.</summary>
    /// <returns>Queryable object for all videos.</returns>
    public virtual IQueryable<Telerik.Sitefinity.Libraries.Model.Video> GetVideos() => this.Provider.GetVideos();

    /// <summary>Gets a video by its identity.</summary>
    /// <param name="id">The identity of the video to get.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.Video" /> instance.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Video GetVideo(Guid id) => this.Provider.GetVideo(id);

    /// <summary>Marks a video for removal.</summary>
    /// <param name="videoToDelete">The video to delete.</param>
    public virtual void DeleteVideo(Telerik.Sitefinity.Libraries.Model.Video videoToDelete) => this.Provider.Delete(videoToDelete);

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video CheckIn(Telerik.Sitefinity.Libraries.Model.Video video) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.CheckIn((ILifecycleDataItem) video);

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video CheckOut(Telerik.Sitefinity.Libraries.Model.Video video) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.CheckOut((ILifecycleDataItem) video);

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video Edit(Telerik.Sitefinity.Libraries.Model.Video video) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.Edit((ILifecycleDataItem) video);

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video Publish(Telerik.Sitefinity.Libraries.Model.Video video) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.Publish((ILifecycleDataItem) video);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video Unpublish(Telerik.Sitefinity.Libraries.Model.Video cnt) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.Unpublish((ILifecycleDataItem) cnt);

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    [Obsolete("Use the Lifecycle property")]
    public Telerik.Sitefinity.Libraries.Model.Video Schedule(
      Telerik.Sitefinity.Libraries.Model.Video item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      return this.Provider.Schedule<Telerik.Sitefinity.Libraries.Model.Video>(item, publicationDate, expirationDate, new Action<Telerik.Sitefinity.Libraries.Model.Video, Telerik.Sitefinity.Libraries.Model.Video>(this.CopyVideo), this.GetVideos());
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all Videos</param>
    /// <returns>ID of the user that checked out the item or Guid.Empty if the item is not checked out.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Guid GetCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Video item) => this.Lifecycle.GetCheckedOutBy((ILifecycleDataItem) item);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all Videos</param>
    /// <returns>True if the item is checked out, false otherwise.</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOut(Telerik.Sitefinity.Libraries.Model.Video item) => this.Lifecycle.IsCheckedOut((ILifecycleDataItem) item);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all Videos.</param>
    /// <returns>True if it was checked out by a user with the specified id, false otherwise</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Video item, Guid userId) => this.Lifecycle.IsCheckedOutBy((ILifecycleDataItem) item, userId);

    [Obsolete("Use the Copy method with the culture parameter")]
    private void CopyVideo(Telerik.Sitefinity.Libraries.Model.Video source, Telerik.Sitefinity.Libraries.Model.Video destination)
    {
      this.CopyMediaContent((MediaContent) source, (MediaContent) destination);
      destination.Height = source.Height;
      destination.Width = source.Width;
      ((IHasParent) destination).Parent = (Content) source.Library;
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video GetLive(Telerik.Sitefinity.Libraries.Model.Video cnt) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.GetLive((ILifecycleDataItem) cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video GetTemp(Telerik.Sitefinity.Libraries.Model.Video cnt) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.GetTemp((ILifecycleDataItem) cnt);

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <typeparam name="TContent">Type of the content item</typeparam>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// otherwise, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Video GetMaster(Telerik.Sitefinity.Libraries.Model.Video cnt) => (Telerik.Sitefinity.Libraries.Model.Video) this.Lifecycle.GetMaster((ILifecycleDataItem) cnt);

    /// <summary>Updates the thumbnails data.</summary>
    /// <param name="id">The id of the video to be updated.</param>
    /// <param name="data">The binary data of the new thumbnail.</param>
    public virtual void UpdateThumbnail(Guid id, byte[] data)
    {
      Telerik.Sitefinity.Libraries.Model.Video video = this.GetVideo(id);
      System.Drawing.Image image = ImagesHelper.ByteArrayToImage(data);
      Guid fileId = video.FileId;
      foreach (MediaFileLink mediaFileLink in video.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == fileId)))
      {
        using (new CultureRegion(mediaFileLink.Culture))
          this.Provider.UpdateThumbnail(video, image, MimeMapping.GetMimeMapping(".jpg"));
      }
    }

    /// <summary>Creates a new document.</summary>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Document CreateDocument() => this.Provider.CreateDocument();

    /// <summary>
    /// Creates a new document with an explicitly specified identity.
    /// </summary>
    /// <param name="id">The identity of the document to create.</param>
    /// <returns>
    /// The created <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /> instance.
    /// </returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Document CreateDocument(Guid id) => this.Provider.CreateDocument(id);

    /// <summary>Gets a query for all documents.</summary>
    /// <returns>Queryable object for all documents.</returns>
    public virtual IQueryable<Telerik.Sitefinity.Libraries.Model.Document> GetDocuments() => this.Provider.GetDocuments();

    /// <summary>Gets a document by its identity.</summary>
    /// <param name="id">The identity of the document to get.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Libraries.Model.Document" /> instance.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Document GetDocument(Guid id) => this.Provider.GetDocument(id);

    /// <summary>Marks a document for removal.</summary>
    /// <param name="documentToDelete">The document to delete.</param>
    public virtual void DeleteDocument(Telerik.Sitefinity.Libraries.Model.Document documentToDelete) => this.Provider.Delete(documentToDelete);

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document CheckIn(Telerik.Sitefinity.Libraries.Model.Document document) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.CheckIn((ILifecycleDataItem) document);

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document CheckOut(Telerik.Sitefinity.Libraries.Model.Document document) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.CheckOut((ILifecycleDataItem) document);

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document Edit(Telerik.Sitefinity.Libraries.Model.Document document) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.Edit((ILifecycleDataItem) document);

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document Publish(Telerik.Sitefinity.Libraries.Model.Document document) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.Publish((ILifecycleDataItem) document);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document Unpublish(Telerik.Sitefinity.Libraries.Model.Document cnt) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.Unpublish((ILifecycleDataItem) cnt);

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    [Obsolete("Use the Lifecycle property")]
    public Telerik.Sitefinity.Libraries.Model.Document Schedule(
      Telerik.Sitefinity.Libraries.Model.Document item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      return this.Provider.Schedule<Telerik.Sitefinity.Libraries.Model.Document>(item, publicationDate, expirationDate, new Action<Telerik.Sitefinity.Libraries.Model.Document, Telerik.Sitefinity.Libraries.Model.Document>(this.CopyDocument), this.GetDocuments());
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all Documents</param>
    /// <returns>ID of the user that checked out the item or Guid.Empty if the item is not checked out.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Guid GetCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Document item) => this.Lifecycle.GetCheckedOutBy((ILifecycleDataItem) item);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all Documents</param>
    /// <returns>True if the item is checked out, false otherwise.</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOut(Telerik.Sitefinity.Libraries.Model.Document item) => this.Lifecycle.IsCheckedOut((ILifecycleDataItem) item);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all Documents.</param>
    /// <returns>True if it was checked out by a user with the specified id, false otherwise</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Document item, Guid userId) => this.Lifecycle.IsCheckedOutBy((ILifecycleDataItem) item, userId);

    /// <summary>
    /// Copy one Document to another for the uses of content lifecycle management
    /// </summary>
    /// <param name="source">Document to copy from</param>
    /// <param name="destination">Document to copy to</param>
    [Obsolete("Use the Copy method with the culture parameter")]
    private void CopyDocument(Telerik.Sitefinity.Libraries.Model.Document source, Telerik.Sitefinity.Libraries.Model.Document destination)
    {
      this.CopyMediaContent((MediaContent) source, (MediaContent) destination);
      destination.Parts = source.Parts;
      ((IHasParent) destination).Parent = (Content) source.Library;
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document GetLive(Telerik.Sitefinity.Libraries.Model.Document cnt) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.GetLive((ILifecycleDataItem) cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document GetTemp(Telerik.Sitefinity.Libraries.Model.Document cnt) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.GetTemp((ILifecycleDataItem) cnt);

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <typeparam name="TContent">Type of the content item</typeparam>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwise, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual Telerik.Sitefinity.Libraries.Model.Document GetMaster(Telerik.Sitefinity.Libraries.Model.Document cnt) => (Telerik.Sitefinity.Libraries.Model.Document) this.Lifecycle.GetMaster((ILifecycleDataItem) cnt);

    /// <summary>Create a new media file link</summary>
    /// <returns>Created media file link instance.</returns>
    public virtual MediaFileLink CreateMediaFileLink() => this.Provider.CreateMediaFileLink();

    /// <summary>Get a query for all media file links</summary>
    /// <returns>Queryable object for all media file links</returns>
    public virtual IQueryable<MediaFileLink> GetMediaFileLinks() => this.Provider.GetMediaFileLinks();

    /// <summary>Mark an media file link for removal</summary>
    /// <param name="mediaFileLinkToDelete">The media file link to delete.</param>
    public virtual void Delete(MediaFileLink mediaFileLinkToDelete)
    {
      IBlobContentLocation blobContentLocation = (IBlobContentLocation) null;
      BlobStorageManager blobStorageManager = (BlobStorageManager) null;
      MediaContent mediaContent = mediaFileLinkToDelete.MediaContent;
      if (mediaContent != null)
      {
        blobStorageManager = BlobStorageManager.GetManager(mediaContent.BlobStorageProvider);
        using (new CultureRegion(mediaFileLinkToDelete.Culture))
        {
          if (!this.provider.IsFileUsedByOtherMediaContent(mediaFileLinkToDelete.FileId, mediaContent.BlobStorageProvider, mediaContent.Id))
            blobContentLocation = blobStorageManager.ResolveBlobContentLocation((IBlobContent) mediaContent);
        }
      }
      this.Provider.Delete(mediaFileLinkToDelete);
      if (blobContentLocation == null || blobStorageManager == null)
        return;
      this.Provider.DeleteBlob(blobContentLocation, blobStorageManager);
    }

    /// <summary>Create a new media file url</summary>
    /// <returns>Created media file url instance.</returns>
    public virtual MediaFileUrl CreateMediaFileUrl() => this.Provider.CreateMediaFileUrl();

    /// <summary>Get a query for all media file urls</summary>
    /// <returns>Queryable object for all media file urls</returns>
    public virtual IQueryable<MediaFileUrl> GetMediaFileUrls() => this.Provider.GetMediaFileUrls();

    /// <summary>Mark an media file url for removal</summary>
    /// <param name="mediaFileUrlToDelete">The media file url to delete.</param>
    public virtual void Delete(MediaFileUrl mediaFileUrlToDelete) => this.Provider.Delete(mediaFileUrlToDelete);

    /// <summary>
    /// Transfers the item blob data from the current storage to the specified storage.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="storageProvider">The storage provider.</param>
    public virtual void TransferItemStorage(MediaContent item, string storageProvider) => this.Provider.TransferItemStorage(item, storageProvider);

    /// <summary>Downloads the specified content from its storage.</summary>
    /// <param name="mediaItemId">The ID of the requested media item.</param>
    /// <returns>The content data stream.</returns>
    public virtual Stream Download(Guid mediaItemId) => this.Provider.Download(this.GetMediaItem(mediaItemId));

    /// <summary>Downloads the specified content from its storage.</summary>
    /// <param name="content">The content.</param>
    /// <returns>The content data stream.</returns>
    public virtual Stream Download(MediaContent content) => this.Provider.Download(content);

    public virtual Stream Download(Thumbnail thumbnail) => this.Provider.Download(thumbnail);

    public virtual Stream Download(IBlobContent item) => this.Provider.Download(item);

    /// <summary>Uploads the specified content.</summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="extension">The extension.</param>
    public virtual void Upload(MediaContent content, Stream source, string extension) => this.Upload(content, source, extension, false);

    /// <summary>Uploads the specified content.</summary>
    /// <param name="content">The content.</param>
    /// <param name="source">The source.</param>
    /// <param name="extension">The extension.</param>
    /// <param name="uploadAndReplace">Whether to replace the existing file with the uploaded one.</param>
    public virtual void Upload(
      MediaContent content,
      Stream source,
      string extension,
      bool uploadAndReplace)
    {
      this.Provider.Upload(content, source, extension, uploadAndReplace);
    }

    /// <summary>Determines whether a BLOB storage provider is used</summary>
    /// <param name="blobProviderName">Name of the BLOB provider.</param>
    public static bool IsBlobStorageProviderUsed(string blobProviderName)
    {
      IEnumerable<LibrariesDataProvider> librariesDataProviders = LibrariesManager.GetManager().GetAllProviders().Cast<LibrariesDataProvider>();
      long num = 0;
      foreach (LibrariesDataProvider librariesDataProvider in librariesDataProviders)
      {
        num += (long) librariesDataProvider.GetAlbums().Where<Telerik.Sitefinity.Libraries.Model.Album>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Album, bool>>) (lib => lib.BlobStorageProvider == blobProviderName)).Count<Telerik.Sitefinity.Libraries.Model.Album>();
        num += (long) librariesDataProvider.GetDocumentLibraries().Where<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.DocumentLibrary, bool>>) (lib => lib.BlobStorageProvider == blobProviderName)).Count<Telerik.Sitefinity.Libraries.Model.DocumentLibrary>();
        num += (long) librariesDataProvider.GetVideoLibraries().Where<Telerik.Sitefinity.Libraries.Model.VideoLibrary>((Expression<Func<Telerik.Sitefinity.Libraries.Model.VideoLibrary, bool>>) (lib => lib.BlobStorageProvider == blobProviderName)).Count<Telerik.Sitefinity.Libraries.Model.VideoLibrary>();
      }
      return num > 0L;
    }

    /// <summary>Starts the relocate library items task.</summary>
    /// <param name="libraryId">The library id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="taskTitle">The task title.</param>
    /// <returns>Task ID</returns>
    public static Guid StartRelocateLibraryItemsTask(
      Guid libraryId,
      string providerName = null,
      string taskTitle = "RelocateLibrary")
    {
      return LibrariesManager.StartRelocateLibraryItemsTaskInteral(libraryId, providerName, taskTitle);
    }

    /// <summary>
    /// Starts an asynchronous task to move a library under a different new parent.
    /// </summary>
    /// <param name="sourceId">The id of the library to move.</param>
    /// <param name="newParentId">The new parent id. If set to Guid.Empty then the source library will be moved to root level.</param>
    /// <param name="librariesProvider">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>The id of the newly created task.</returns>
    public static Guid StartLibraryMoveTask(
      Guid sourceId,
      Guid newParentId,
      string librariesProvider = null,
      string transactionName = null)
    {
      return LibrariesManager.StartLibraryMoveTask(new Guid[1]
      {
        sourceId
      }, newParentId, librariesProvider, transactionName);
    }

    internal static Guid StartRelocateLibraryItemsTaskInteral(
      Guid libraryId,
      string providerName = null,
      string taskTitle = "RelocateLibrary",
      string libraryTitle = null,
      string libraryType = null)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      Guid guid = Guid.NewGuid();
      if (string.IsNullOrEmpty(libraryType))
        libraryType = LibrariesManager.GetLibraryType(libraryId, providerName);
      LibraryRelocationTask libraryRelocationTask = new LibraryRelocationTask();
      libraryRelocationTask.Id = guid;
      libraryRelocationTask.LibraryId = libraryId;
      libraryRelocationTask.LibraryType = libraryType;
      libraryRelocationTask.LibraryProvider = providerName;
      libraryRelocationTask.Title = taskTitle;
      libraryRelocationTask.LibraryTitle = libraryTitle;
      LibraryRelocationTask task = libraryRelocationTask;
      task.ConcurrentTasksKey = libraryId.ToString();
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
      return guid;
    }

    internal static Guid StartLibraryMoveTask(
      Guid[] sourceIDs,
      Guid newParentId,
      string librariesProvider = null,
      string transactionName = null)
    {
      SchedulingManager manager = SchedulingManager.GetManager((string) null, transactionName);
      Guid guid = Guid.NewGuid();
      string libraryTitle = (string) null;
      string taskDescription = LibrariesManager.GetTaskDescription(sourceIDs, newParentId, librariesProvider, out libraryTitle, transactionName);
      LibraryMoveTask libraryMoveTask = new LibraryMoveTask();
      libraryMoveTask.Id = guid;
      libraryMoveTask.ItemId = ((IEnumerable<Guid>) sourceIDs).FirstOrDefault<Guid>();
      libraryMoveTask.ItemIDs = sourceIDs;
      libraryMoveTask.ParentId = newParentId;
      libraryMoveTask.LibraryProvider = librariesProvider;
      libraryMoveTask.Title = libraryTitle;
      libraryMoveTask.Description = taskDescription;
      libraryMoveTask.LibraryTitle = libraryTitle;
      libraryMoveTask.LibraryType = LibrariesManager.GetLibraryType(((IEnumerable<Guid>) sourceIDs).FirstOrDefault<Guid>(), librariesProvider);
      LibraryMoveTask task = libraryMoveTask;
      task.ConcurrentTasksKey = ((IEnumerable<Guid>) sourceIDs).FirstOrDefault<Guid>().ToString();
      manager.AddTask((ScheduledTask) task);
      if (transactionName.IsNullOrEmpty())
        manager.SaveChanges();
      else
        TransactionManager.TryAddPostCommitAction(transactionName, (Action) (() => SchedulingManager.RescheduleNextRun()));
      return guid;
    }

    private static string GetLibraryType(Guid folderOrLibraryId, string providerName)
    {
      Type libraryType = LibrariesManager.GetManager(providerName).GetLibraryType(folderOrLibraryId);
      return libraryType != (Type) null ? libraryType.FullName : (string) null;
    }

    private static string GetTaskDescription(
      Guid[] sourceIDs,
      Guid newParentId,
      string provider,
      out string libraryTitle,
      string transactionName = null)
    {
      LibrariesManager manager = LibrariesManager.GetManager(provider, transactionName);
      IEnumerable<string> strings = manager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => sourceIDs.Contains<Guid>(f.Id))).ToList<Folder>().Select<Folder, string>((Func<Folder, string>) (f => f.Title.ToString()));
      if (strings.Count<string>() == 0)
      {
        List<Library> list = manager.GetLibraries().Where<Library>((Expression<Func<Library, bool>>) (f => sourceIDs.Contains<Guid>(f.Id))).ToList<Library>();
        libraryTitle = list.Count != 1 ? (string) null : list.FirstOrDefault<Library>().Title.ToString();
      }
      else
        libraryTitle = string.Join(", ", strings);
      string str = "Root";
      if (newParentId != Guid.Empty)
        str = (string) manager.GetFolder(newParentId).Title;
      return Res.Get<LibrariesResources>().MovingLibrary.Arrange((object) string.Join(", ", strings), (object) str);
    }

    /// <summary>
    /// Starts the thumbnail regeneration task for the specified library.
    /// </summary>
    /// <param name="libraryId">The library's id.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <param name="taskTitle">The task's title.</param>
    public static Guid StartThumbnailRegenerationTask(
      Guid libraryId,
      string providerName = null,
      string[] profilesFilter = null,
      string taskTitle = "RegenerateThumbnails")
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      Guid guid = Guid.NewGuid();
      LibraryThumbnailsRegenerateTask thumbnailsRegenerateTask = new LibraryThumbnailsRegenerateTask();
      thumbnailsRegenerateTask.Id = guid;
      thumbnailsRegenerateTask.LibraryId = libraryId;
      thumbnailsRegenerateTask.LibraryProvider = providerName;
      thumbnailsRegenerateTask.Title = taskTitle;
      LibraryThumbnailsRegenerateTask task = thumbnailsRegenerateTask;
      if (profilesFilter != null)
        task.ProfilesFilter = profilesFilter;
      task.ConcurrentTasksKey = libraryId.ToString();
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
      return guid;
    }

    /// <summary>
    /// Starts the thumbnail regeneration task for the specified library.
    /// </summary>
    /// <param name="libraryId">The library's id.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <param name="taskTitle">The task's title.</param>
    public static Guid StartThumbnailDeleteTask(
      Guid libraryId,
      string providerName = null,
      string[] profilesFilter = null,
      string taskTitle = "DeleteThumbnails")
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      Guid guid = Guid.NewGuid();
      LibraryThumbnailsDeleteTask thumbnailsDeleteTask = new LibraryThumbnailsDeleteTask();
      thumbnailsDeleteTask.Id = guid;
      thumbnailsDeleteTask.LibraryId = libraryId;
      thumbnailsDeleteTask.LibraryProvider = providerName;
      thumbnailsDeleteTask.Title = taskTitle;
      LibraryThumbnailsDeleteTask task = thumbnailsDeleteTask;
      if (profilesFilter != null)
        task.ProfilesFilter = profilesFilter;
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
      return guid;
    }

    /// <summary>Gets libraries manager from the system provider .</summary>
    /// <returns></returns>
    internal static LibrariesManager GetSystemProviderLibrariesManager()
    {
      if (ManagerBase<LibrariesDataProvider>.StaticProvidersCollection == null)
        LibrariesManager.GetManager();
      return LibrariesManager.GetManager((ManagerBase<LibrariesDataProvider>.StaticProvidersCollection.Where<LibrariesDataProvider>((Func<LibrariesDataProvider, bool>) (p => p.ProviderGroup == "System")).FirstOrDefault<LibrariesDataProvider>() ?? throw new ArgumentNullException("Missing libraries provider with 'System' provider group. The library provider should be created with parameter key 'providerGroup' and value 'System'.")).Name);
    }

    /// <summary>Gets the temp file path.</summary>
    /// <param name="content">The content.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public string GetItemTemporaryFilePath(MediaContent content, string fileName) => Path.Combine(LibrariesManager.LibrariesTempFolder, string.Format("{0}_{1}_{2}", (object) this.Provider.Name, (object) content.Parent.UrlName, (object) fileName));

    public static string LibrariesTempFolder => LibrariesManager.librariesTempFolder;

    /// <summary>Copies one media content item over another</summary>
    /// <param name="source">Source media content</param>
    /// <param name="destination">Destination media content</param>
    public void CopyMediaContent(MediaContent source, MediaContent destination)
    {
      this.CopyUrls<MediaUrlData>((Content) source, (Content) destination, source.Urls, destination.Urls);
      bool flag = SystemManager.CurrentHttpContext.Request.QueryString["uploadAndReplace"] == "true";
      CopyOptions copyOptions = flag ? CopyOptions.AllFields : (this.Lifecycle as IExtendedLifecycleDecorator).GetCopyOptions((ILifecycleDataItem) source, (ILifecycleDataItem) destination);
      if (copyOptions == CopyOptions.AllFields)
      {
        Lstring.CopyValues(source.MediaFileUrlName, destination.MediaFileUrlName);
        if (!((IEnumerable<CultureInfo>) destination.MediaFileUrlName.GetAvailableLanguages()).Any<CultureInfo>())
          Lstring.CopyValues(destination.UrlName, destination.MediaFileUrlName);
      }
      else
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        string stringDefaultFallback = source.MediaFileUrlName.GetStringDefaultFallback(culture);
        destination.MediaFileUrlName.SetString(culture, stringDefaultFallback);
      }
      if (copyOptions != CopyOptions.LocalazibleFields)
      {
        destination.Parent = source.Parent;
        destination.Ordinal = source.Ordinal;
        destination.FolderId = source.FolderId;
      }
      this.CopyBlobContent((IChunksBlobContent) source, (IChunksBlobContent) destination);
      this.CopyThumbnails(source, destination, copyOptions: copyOptions, fileReplaced: flag);
      this.CopyBlobLocation(source, destination, flag);
      this.CopyFileLinks(destination, source, copyOptions);
      destination.GetFileLink(true);
    }

    private void CopyFileLinks(
      MediaContent destination,
      MediaContent source,
      CopyOptions copyOptions)
    {
      int cultureLcid = AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
      List<int> unusedLinkCultures = destination.MediaFileLinks.Select<MediaFileLink, int>((Func<MediaFileLink, int>) (l => l.Culture)).ToList<int>();
      foreach (MediaFileLink mediaFileLink in (IEnumerable<MediaFileLink>) source.MediaFileLinks)
      {
        int culture = mediaFileLink.Culture;
        if (copyOptions == CopyOptions.AllFields || culture == cultureLcid)
        {
          MediaFileLink fileLink = destination.GetFileLink(true, new int?(culture));
          unusedLinkCultures.Remove(fileLink.Culture);
          mediaFileLink.CopyTo(fileLink);
          this.Provider.CopyMediaFileUrls(mediaFileLink, fileLink);
        }
      }
      if (copyOptions != CopyOptions.AllFields)
        return;
      foreach (MediaFileLink mediaFileLinkToDelete in destination.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => unusedLinkCultures.Contains(l.Culture))).ToList<MediaFileLink>())
      {
        destination.MediaFileLinks.Remove(mediaFileLinkToDelete);
        this.Delete(mediaFileLinkToDelete);
      }
    }

    internal void CopyThumbnails(
      MediaContent source,
      MediaContent destination,
      bool copyMetaDataOnly = false,
      CopyOptions copyOptions = CopyOptions.AllFields,
      bool fileReplaced = false)
    {
      CultureInfo cultureInfo = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      List<Thumbnail> source1 = new List<Thumbnail>((IEnumerable<Thumbnail>) destination.Thumbnails);
      foreach (Thumbnail thumbnail1 in (IEnumerable<Thumbnail>) source.Thumbnails)
      {
        Thumbnail srcThumb = thumbnail1;
        Thumbnail thumbnail2 = source1.FirstOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.Name == srcThumb.Name && t.Culture == srcThumb.Culture));
        if (copyOptions != CopyOptions.AllFields && srcThumb.Culture != cultureInfo.Name)
        {
          if (thumbnail2 != null)
            source1.Remove(thumbnail2);
        }
        else
        {
          if (thumbnail2 == null)
          {
            thumbnail2 = new Thumbnail();
            thumbnail2.Id = this.provider.GetNewGuid();
            thumbnail2.Parent = destination;
            thumbnail2.Name = srcThumb.Name;
            destination.Thumbnails.Add(thumbnail2);
          }
          else
            source1.Remove(thumbnail2);
          thumbnail2.Width = srcThumb.Width;
          thumbnail2.Height = srcThumb.Height;
          thumbnail2.Image = srcThumb.Image;
          this.CopyBlobContent((IChunksBlobContent) srcThumb, (IChunksBlobContent) thumbnail2);
          if (srcThumb.FileId == Guid.Empty)
          {
            byte[] destinationArray = new byte[srcThumb.Data.Length];
            Array.Copy((Array) srcThumb.Data, (Array) destinationArray, srcThumb.Data.Length);
            thumbnail2.Data = destinationArray;
          }
          else
          {
            if (thumbnail2.FileId == Guid.Empty)
              thumbnail2.Data = (byte[]) null;
            if (copyMetaDataOnly)
              thumbnail2.FileId = srcThumb.FileId;
            else
              this.CopyBlobLocationInternal(new BlobItemWrapper((IBlobContent) srcThumb), new BlobItemWrapper((IBlobContent) thumbnail2), fileReplaced);
          }
          thumbnail2.Type = srcThumb.Type;
          thumbnail2.Culture = srcThumb.Culture;
        }
      }
      foreach (Thumbnail thumbnail in source1)
      {
        this.Provider.Delete(thumbnail);
        destination.Thumbnails.Remove(thumbnail);
      }
      destination.UseLagacyThumbnailsStorage = source.UseLagacyThumbnailsStorage;
      destination.ThumbnailsVersion = source.ThumbnailsVersion;
    }

    internal void CopyBlobContent(IChunksBlobContent source, IChunksBlobContent destination)
    {
      destination.TotalSize = source.TotalSize;
      destination.MimeType = source.MimeType;
      destination.Extension = source.Extension;
      destination.NumberOfChunks = source.NumberOfChunks;
      destination.Uploaded = source.Uploaded;
      destination.ChunkSize = source.ChunkSize;
    }

    /// <summary>Copies the BLOB location.</summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    protected virtual void CopyBlobLocation(MediaContent source, MediaContent destination) => this.CopyBlobLocationInternal(new BlobItemWrapper((IBlobContent) source), new BlobItemWrapper((IBlobContent) destination), false);

    /// <summary>Copies the BLOB location.</summary>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="replacedFile">The replaced file.</param>
    protected virtual void CopyBlobLocation(
      MediaContent source,
      MediaContent destination,
      bool replacedFile)
    {
      this.CopyBlobLocationInternal(new BlobItemWrapper((IBlobContent) source), new BlobItemWrapper((IBlobContent) destination), replacedFile);
    }

    private void CopyBlobLocationInternal(
      BlobItemWrapper source,
      BlobItemWrapper destination,
      bool replacedFile)
    {
      Guid fileId = destination.FileId;
      string blobStorageProvider = destination.BlobStorageProvider;
      BlobStorageManager blobStorageManager = this.Provider.GetBlobStorageManager(destination.Content);
      bool flag1 = replacedFile || fileId != Guid.Empty && (source.FileId != fileId || !source.BlobStorageProvider.Equals(blobStorageProvider));
      string str = source.FilePath;
      bool flag2 = destination.Status == ContentLifecycleStatus.Live && source.Status == ContentLifecycleStatus.Master;
      bool flag3 = destination.Status == ContentLifecycleStatus.Master && source.Status == ContentLifecycleStatus.Temp;
      MediaContent content1 = destination.Content as MediaContent;
      Thumbnail content2 = destination.Content as Thumbnail;
      IBlobContentLocation blobContentLocation1 = (IBlobContentLocation) null;
      if (flag1)
      {
        bool flag4 = false;
        if (((content1 == null ? 1 : (this.Provider.FindAnotherMediaContentForFile(content1.FileId, content1.BlobStorageProvider, content1.Id, true, (CultureInfo) null) == null ? 1 : 0)) & (content2 == null ? (true ? 1 : 0) : (this.Provider.FindAnotherThumbnailForFile(content2.FileId, content2.Parent.BlobStorageProvider, content2.Id, content2.Culture) == null ? 1 : 0))) != 0)
          flag4 = true;
        if (flag4)
          blobContentLocation1 = blobStorageManager.ResolveBlobContentLocation(destination.Content);
      }
      if (destination.FileId != Guid.Empty | flag1)
      {
        if (blobStorageManager.Provider is IExternalBlobStorageProvider provider && !string.IsNullOrEmpty(destination.FilePath) | flag1)
        {
          bool flag5 = SystemManager.CurrentContext.AppSettings.Multilingual && content2 != null && content2.Culture != SystemManager.CurrentContext.Culture.Name;
          if (flag2 && !flag5)
          {
            IBlobContentLocation blobContentLocation2 = (IBlobContentLocation) null;
            IBlobContentLocation destination1 = (IBlobContentLocation) null;
            if (flag1)
            {
              blobContentLocation2 = (IBlobContentLocation) source.Content;
              destination1 = (IBlobContentLocation) destination.Content;
            }
            if (!(provider is IMediaContentFilePathResolver filePathResolver))
              filePathResolver = (IMediaContentFilePathResolver) this.Provider;
            str = source.GenerateLivePath(filePathResolver);
            IBlobContentLocation blob;
            if (!str.Equals(destination.FilePath))
            {
              if (blobContentLocation2 == null)
                blobContentLocation2 = blobStorageManager.ResolveBlobContentLocation(destination.Content);
              BlobContentProxy from = BlobContentProxy.CreateFrom(destination.Content);
              blob = blobStorageManager.ResolveBlobContentLocation((IBlobContent) from);
              from.FilePath = str;
              destination1 = blobStorageManager.ResolveBlobContentLocation((IBlobContent) from);
            }
            else
            {
              blob = destination1;
              blobContentLocation1 = (IBlobContentLocation) null;
            }
            if (blobContentLocation2 != null && destination1 != null && !blobContentLocation2.FilePath.Equals(destination1.FilePath))
            {
              if (flag1)
              {
                if (this.Provider.GetRelatedManager<VersionManager>((string) null).DependencyExists(blob.FileId.ToString(), typeof (BlobContentCleanerTask)))
                {
                  this.Provider.ArchiveBlob(blob, provider, blobContentLocation1 == null);
                  if (blobContentLocation1 != null && blobContentLocation1.FileId == blob.FileId)
                    blobContentLocation1 = (IBlobContentLocation) null;
                }
                if (blobStorageProvider == source.BlobStorageProvider)
                {
                  provider.Copy(blobContentLocation2, destination1);
                  this.Provider.DeleteBlob(blobContentLocation2, blobStorageManager);
                }
              }
              else
                provider.Move(blobContentLocation2, destination1);
            }
            else if (blob != null && blob is IDependentItem)
              blobContentLocation1 = blob;
            source.FilePath = str;
          }
          else if (flag3 && !string.IsNullOrEmpty(destination.FilePath) && destination.FilePath.Equals(source.FilePath))
            blobContentLocation1 = (IBlobContentLocation) null;
        }
        if (blobContentLocation1 != null)
          this.Provider.DeleteBlob(blobContentLocation1, blobStorageManager);
      }
      destination.FileId = source.FileId;
      destination.FilePath = str;
      destination.BlobStorageProvider = source.BlobStorageProvider;
    }

    /// <summary>
    /// Copies an already uploaded content from another translation to the current one
    /// </summary>
    /// <param name="item">The media item</param>
    /// <param name="sourceCulture">The culture to copy from</param>
    /// <param name="regenerateThumbnails">Indicates if the thumbnails for the current translation should be regenerated</param>
    public virtual void CopyUploadedContent(
      MediaContent item,
      CultureInfo sourceCulture,
      bool regenerateThumbnails = false)
    {
      MediaFileLink fileLink = item.GetFileLink(true);
      int sourceCultureLcid = AppSettings.CurrentSettings.GetCultureLcid(sourceCulture);
      MediaFileLink source = item.MediaFileLinks.FirstOrDefault<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.Culture == sourceCultureLcid));
      if (source != null)
      {
        item.MediaFileUrlName = (Lstring) item.MediaFileUrlName[sourceCulture];
        source.CopyTo(fileLink);
        this.Provider.CopyMediaFileUrls(source, fileLink);
      }
      if (!regenerateThumbnails)
        return;
      this.RegenerateThumbnails(item);
    }

    /// <summary>
    /// Called from the Lifecycle when the items is unpublished
    /// </summary>
    /// <param name="liveItem">The live item.</param>
    /// <param name="masterItem">The master item.</param>
    /// <param name="culture">The culture.</param>
    protected virtual void OnUnpublishItem(
      ILifecycleDataItem liveItem,
      ILifecycleDataItem masterItem,
      CultureInfo culture = null)
    {
      MediaContent liveMediaItem = liveItem as MediaContent;
      MediaFileLink[] array = liveMediaItem.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == liveMediaItem.FileId)).ToArray<MediaFileLink>();
      if (((IEnumerable<MediaFileLink>) array).Any<MediaFileLink>((Func<MediaFileLink, bool>) (l => LifecycleExtensions.IsPublishedInCulture(liveMediaItem, AppSettings.CurrentSettings.GetCultureByLcid(l.Culture)))))
        return;
      this.MoveExternalBlobToTempPath(masterItem, liveMediaItem, (IEnumerable<MediaFileLink>) array);
    }

    private void MoveExternalBlobToTempPath(
      ILifecycleDataItem masterItem,
      MediaContent liveMediaItem,
      IEnumerable<MediaFileLink> liveLinks)
    {
      BlobStorageManager blobStorageManager = this.Provider.GetBlobStorageManager(liveMediaItem);
      if (!(blobStorageManager.Provider is IExternalBlobStorageProvider provider))
        return;
      MediaContent destination1 = masterItem as MediaContent;
      IEnumerable<MediaFileLink> source = destination1.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == liveMediaItem.FileId));
      if (source.Any<MediaFileLink>())
      {
        string tempPath = this.Provider.GenerateTempPath(liveMediaItem);
        foreach (MediaFileLink mediaFileLink in source)
          mediaFileLink.FilePath = tempPath;
        foreach (Thumbnail thumbnail in liveMediaItem.GetThumbnails())
        {
          Thumbnail thumb = thumbnail;
          Thumbnail destination2 = destination1.GetThumbnails().SingleOrDefault<Thumbnail>((Func<Thumbnail, bool>) (t => t.Name == thumb.Name));
          if (destination2 != null)
            provider.Move((IBlobContentLocation) thumb, (IBlobContentLocation) destination2);
        }
        provider.Move((IBlobContentLocation) liveMediaItem, (IBlobContentLocation) destination1);
        foreach (MediaFileLink liveLink in liveLinks)
          liveLink.FilePath = tempPath;
      }
      else
        this.Provider.DeleteBlob((IBlobContentLocation) liveMediaItem, blobStorageManager);
    }

    internal void RefreshItem(MediaContent item) => this.Provider.RefreshItem(item);

    internal static IEnumerable<string> GetAllowedExtensions(Type type)
    {
      string str = "";
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      if (type == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        str = librariesConfig.Images.AllowedExensionsSettings;
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        str = librariesConfig.Videos.AllowedExensionsSettings;
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        str = librariesConfig.Documents.AllowedExensionsSettings;
      if (str == null)
        return (IEnumerable<string>) new string[0];
      return ((IEnumerable<string>) str.Split(',')).Select<string, string>((Func<string, string>) (e => e.Trim()));
    }

    internal static int GetMaximumAllowedSize(Type type)
    {
      int maximumAllowedSize = 0;
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      if (type == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        maximumAllowedSize = librariesConfig.Images.AllowedMaxImageSize;
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        maximumAllowedSize = librariesConfig.Videos.AllowedMaxVideoSize;
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        maximumAllowedSize = librariesConfig.Documents.AllowedMaxDocumentSize;
      return maximumAllowedSize;
    }

    public override IQueryable<TItem> GetItems<TItem>()
    {
      if (typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(typeof (TItem)))
        return this.GetImages() as IQueryable<TItem>;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(typeof (TItem)))
        return this.GetVideos() as IQueryable<TItem>;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(typeof (TItem)))
        return this.GetDocuments() as IQueryable<TItem>;
      if (typeof (Telerik.Sitefinity.Libraries.Model.Album).IsAssignableFrom(typeof (TItem)))
        return this.GetAlbums() as IQueryable<TItem>;
      if (typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).IsAssignableFrom(typeof (TItem)))
        return this.GetDocumentLibraries() as IQueryable<TItem>;
      if (typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).IsAssignableFrom(typeof (TItem)))
        return this.GetVideoLibraries() as IQueryable<TItem>;
      throw new ArgumentException("Unable to cast TItem to Image or Video or Document or Album or DocumentLibrary or VideoLibrary");
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.Album Unpublish(Telerik.Sitefinity.Libraries.Model.Album cnt) => throw new NotSupportedException();

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.DocumentLibrary Unpublish(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary cnt)
    {
      throw new NotSupportedException();
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public virtual Telerik.Sitefinity.Libraries.Model.VideoLibrary Unpublish(
      Telerik.Sitefinity.Libraries.Model.VideoLibrary cnt)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.Album CheckIn(Telerik.Sitefinity.Libraries.Model.Album item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.Album CheckOut(Telerik.Sitefinity.Libraries.Model.Album item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.Album Edit(Telerik.Sitefinity.Libraries.Model.Album item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.Album Publish(Telerik.Sitefinity.Libraries.Model.Album item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public Guid GetCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Album item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public bool IsCheckedOut(Telerik.Sitefinity.Libraries.Model.Album item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Albums don't support content lifecycle management.</exception>
    public bool IsCheckedOutBy(Telerik.Sitefinity.Libraries.Model.Album item, Guid userId) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public Library CheckIn(Library item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public Library CheckOut(Library item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public Library Edit(Library item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public Library Publish(Library item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public Guid GetCheckedOutBy(Library item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public bool IsCheckedOut(Library item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Libraries don't support content lifecycle management.</exception>
    public bool IsCheckedOutBy(Library item, Guid userId) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Library is not supported</exception>
    public Library GetLive(Library cnt) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Library is not supported</exception>
    public Library GetTemp(Library cnt) => throw new NotImplementedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Library is not supported</exception>
    public Library GetMaster(Library cnt) => throw new NotImplementedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.Album GetLive(Telerik.Sitefinity.Libraries.Model.Album cnt) => throw new NotImplementedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.Album GetTemp(Telerik.Sitefinity.Libraries.Model.Album cnt) => throw new NotImplementedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.Album GetMaster(Telerik.Sitefinity.Libraries.Model.Album cnt) => throw new NotImplementedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary CheckIn(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary item)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary CheckOut(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary item)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary Edit(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary item)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary Publish(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary item)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Guid GetCheckedOutBy(Telerik.Sitefinity.Libraries.Model.DocumentLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public bool IsCheckedOut(Telerik.Sitefinity.Libraries.Model.DocumentLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used</param>
    /// <param name="userId">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public bool IsCheckedOutBy(Telerik.Sitefinity.Libraries.Model.DocumentLibrary item, Guid userId) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary GetLive(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary cnt)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary GetTemp(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary cnt)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary GetMaster(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary cnt)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary GetLive(Telerik.Sitefinity.Libraries.Model.VideoLibrary cnt) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary GetTemp(Telerik.Sitefinity.Libraries.Model.VideoLibrary cnt) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="cnt">Not used</param>
    /// <returns>Throws Exception</returns>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary GetMaster(Telerik.Sitefinity.Libraries.Model.VideoLibrary cnt) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary CheckIn(Telerik.Sitefinity.Libraries.Model.VideoLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary CheckOut(Telerik.Sitefinity.Libraries.Model.VideoLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary Edit(Telerik.Sitefinity.Libraries.Model.VideoLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary Publish(Telerik.Sitefinity.Libraries.Model.VideoLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public Guid GetCheckedOutBy(Telerik.Sitefinity.Libraries.Model.VideoLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public bool IsCheckedOut(Telerik.Sitefinity.Libraries.Model.VideoLibrary item) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="itemsQuery">Not used.</param>
    /// <returns>Nothing. Throws exception.</returns>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrarys don't support content lifecycle management.</exception>
    public bool IsCheckedOutBy(Telerik.Sitefinity.Libraries.Model.VideoLibrary item, Guid userId) => throw new NotSupportedException();

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="publicationDate">Not used</param>
    /// <param name="expirationDate">Not used</param>
    /// <exception cref="T:System.NotSupportedException">Always. Album is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.Album Schedule(
      Telerik.Sitefinity.Libraries.Model.Album item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="publicationDate">Not used</param>
    /// <param name="expirationDate">Not used</param>
    /// <exception cref="T:System.NotSupportedException">Always. Library is not supported</exception>
    public Library Schedule(
      Library item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="publicationDate">Not used</param>
    /// <param name="expirationDate">Not used</param>
    /// <exception cref="T:System.NotSupportedException">Always. DocumentLibrary is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.DocumentLibrary Schedule(
      Telerik.Sitefinity.Libraries.Model.DocumentLibrary item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      throw new NotSupportedException();
    }

    /// <summary>Not supported</summary>
    /// <param name="item">Not used</param>
    /// <param name="publicationDate">Not used</param>
    /// <param name="expirationDate">Not used</param>
    /// <exception cref="T:System.NotSupportedException">Always. VideoLibrary is not supported</exception>
    public Telerik.Sitefinity.Libraries.Model.VideoLibrary Schedule(
      Telerik.Sitefinity.Libraries.Model.VideoLibrary item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      throw new NotSupportedException();
    }

    protected override bool EnsureItemParent(Content item, Content parent, bool recompileUrls)
    {
      Library library1 = (Library) null;
      MediaContent mediaContent = item as MediaContent;
      if (parent is Library library2 && mediaContent != null)
        library1 = mediaContent.Parent;
      bool flag = base.EnsureItemParent(item, parent, recompileUrls);
      if (recompileUrls && mediaContent != null)
        this.UpdateMediaContentForAllTranslations(mediaContent, (Action) (() => this.RecompileMediaFileUrls(mediaContent)));
      if (mediaContent != null)
      {
        if (library2 != null && mediaContent.Uploaded && library1 != null)
        {
          List<string> removedProfiles = new List<string>();
          List<string> addedProfiles = new List<string>();
          IList<string> thumbnailProfiles1 = library2.ThumbnailProfiles;
          IList<string> thumbnailProfiles2 = library1.ThumbnailProfiles;
          if (thumbnailProfiles2 != null && thumbnailProfiles2.Count<string>() > 0)
          {
            foreach (string str in (IEnumerable<string>) thumbnailProfiles2)
            {
              if (!thumbnailProfiles1.Contains(str))
                removedProfiles.Add(str);
            }
            foreach (string str in (IEnumerable<string>) thumbnailProfiles1)
            {
              if (!thumbnailProfiles2.Contains(str))
                addedProfiles.Add(str);
            }
          }
          else
            addedProfiles.AddRange((IEnumerable<string>) thumbnailProfiles1);
          if (removedProfiles.Count > 0)
          {
            foreach (Thumbnail thumbnail in new List<Thumbnail>(mediaContent.Thumbnails.Where<Thumbnail>((Func<Thumbnail, bool>) (t => t.Type == ThumbnailTypes.Autogenerated && removedProfiles.Contains(t.Name)))))
              this.Provider.Delete(thumbnail);
          }
          this.TransferItemStorage(mediaContent, library2.BlobStorageProvider);
          if (addedProfiles.Count > 0)
          {
            if (mediaContent.Status == ContentLifecycleStatus.Master)
            {
              this.UpdateMediaContentForAllTranslations(mediaContent, (Action) (() => this.RegenerateThumbnails(mediaContent, addedProfiles.ToArray())));
            }
            else
            {
              MediaContent master = this.Lifecycle.GetMaster((ILifecycleDataItem) mediaContent) as MediaContent;
              if (mediaContent.Parent == master.Parent && mediaContent.FileId == master.FileId)
                this.CopyThumbnails(master, mediaContent);
              else
                this.UpdateMediaContentForAllTranslations(mediaContent, (Action) (() => this.RegenerateThumbnails(mediaContent, addedProfiles.ToArray())));
            }
          }
        }
        Guid? nullable1 = mediaContent.FolderId;
        if (nullable1.HasValue)
        {
          Folder folder = this.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => (Guid?) f.Id == mediaContent.FolderId));
          if (folder != null && parent != null && folder.RootId != parent.Id)
          {
            MediaContent mediaContent1 = mediaContent;
            nullable1 = new Guid?();
            Guid? nullable2 = nullable1;
            mediaContent1.FolderId = nullable2;
          }
        }
      }
      return flag;
    }

    internal void UpdateMediaContentForAllTranslations(MediaContent item, Action action)
    {
      foreach (MediaFileLink mediaFileLink in (IEnumerable<MediaFileLink>) item.MediaFileLinks)
      {
        using (new CultureRegion(mediaFileLink.Culture))
          action();
      }
    }

    internal void ChangeItemParent(
      MediaContent item,
      Content newParent,
      Guid? folderId,
      bool recompileUrls)
    {
      ILifecycleManager lifecycleManager = (ILifecycleManager) this;
      if (lifecycleManager != null && item.Status != ContentLifecycleStatus.Temp)
      {
        ILifecycleDecorator lifecycle = lifecycleManager.Lifecycle;
        if (lifecycle.GetMaster((ILifecycleDataItem) item) is MediaContent master)
        {
          master.FolderId = folderId;
          this.EnsureItemParent((Content) master, newParent, recompileUrls);
        }
        if (lifecycle.GetTemp((ILifecycleDataItem) item) is MediaContent temp)
        {
          temp.FolderId = folderId;
          this.EnsureItemParent((Content) temp, newParent, recompileUrls);
        }
        if (!(lifecycle.GetLive((ILifecycleDataItem) item) is MediaContent live))
          return;
        live.FolderId = folderId;
        this.EnsureItemParent((Content) live, newParent, recompileUrls);
      }
      else
      {
        item.FolderId = folderId;
        this.EnsureItemParent((Content) item, newParent, recompileUrls);
      }
    }

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    public Content CheckIn(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.CheckIn((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.CheckIn((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.CheckIn((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.CheckIn((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return (Content) this.CheckIn((Library) item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.CheckIn((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.CheckIn((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    public Content CheckOut(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.CheckOut((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.CheckOut((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.CheckOut((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.CheckOut((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return (Content) this.CheckOut((Library) item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.CheckOut((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.CheckOut((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    public Content Edit(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.Edit((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.Edit((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.Edit((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.Edit((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return (Content) this.Edit((Library) item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.Edit((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.Edit((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <returns>Published content item</returns>
    public Content Publish(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.Publish((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.Publish((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.Publish((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.Publish((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return (Content) this.Publish((Library) item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.Publish((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.Publish((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    public Content Unpublish(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.Unpublish((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.Unpublish((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.Unpublish((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.Unpublish((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return this.Unpublish(item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.Unpublish((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.Unpublish((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    /// <returns>Scheduled content item</returns>
    public Content Schedule(
      Content item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.Schedule((Telerik.Sitefinity.Libraries.Model.Image) item, publicationDate, expirationDate);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.Schedule((Telerik.Sitefinity.Libraries.Model.Document) item, publicationDate, expirationDate);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.Schedule((Telerik.Sitefinity.Libraries.Model.Video) item, publicationDate, expirationDate);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.Schedule((Telerik.Sitefinity.Libraries.Model.Album) item, publicationDate, expirationDate);
        case Library _:
          return (Content) this.Schedule((Library) item, publicationDate, expirationDate);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.Schedule((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item, publicationDate, expirationDate);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.Schedule((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item, publicationDate, expirationDate);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>
    /// ID of the user that checked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    public Guid GetCheckedOutBy(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return this.GetCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return this.GetCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return this.GetCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return this.GetCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return this.GetCheckedOutBy((Library) item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return this.GetCheckedOutBy((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return this.GetCheckedOutBy((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>True if the item is checked out, false otherwise.</returns>
    public bool IsCheckedOut(Content item)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return this.IsCheckedOut((Telerik.Sitefinity.Libraries.Model.Image) item);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return this.IsCheckedOut((Telerik.Sitefinity.Libraries.Model.Document) item);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return this.IsCheckedOut((Telerik.Sitefinity.Libraries.Model.Video) item);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return this.IsCheckedOut((Telerik.Sitefinity.Libraries.Model.Album) item);
        case Library _:
          return this.IsCheckedOut((Library) item);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return this.IsCheckedOut((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return this.IsCheckedOut((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s.</param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwise
    /// </returns>
    public bool IsCheckedOutBy(Content item, Guid userId)
    {
      switch (item)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return this.IsCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Image) item, userId);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return this.IsCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Document) item, userId);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return this.IsCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Video) item, userId);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return this.IsCheckedOutBy((Telerik.Sitefinity.Libraries.Model.Album) item, userId);
        case Library _:
          return this.IsCheckedOutBy((Library) item, userId);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return this.IsCheckedOutBy((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) item, userId);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return this.IsCheckedOutBy((Telerik.Sitefinity.Libraries.Model.VideoLibrary) item, userId);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
      }
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetLive(Content cnt)
    {
      switch (cnt)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.GetLive((Telerik.Sitefinity.Libraries.Model.Image) cnt);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.GetLive((Telerik.Sitefinity.Libraries.Model.Document) cnt);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.GetLive((Telerik.Sitefinity.Libraries.Model.Video) cnt);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.GetLive((Telerik.Sitefinity.Libraries.Model.Album) cnt);
        case Library _:
          return (Content) this.GetLive((Library) cnt);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.GetLive((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) cnt);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.GetLive((Telerik.Sitefinity.Libraries.Model.VideoLibrary) cnt);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));
      }
    }

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>
    /// Temp version of <paramref name="cnt" />, if it exists.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetTemp(Content cnt)
    {
      switch (cnt)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.GetTemp((Telerik.Sitefinity.Libraries.Model.Image) cnt);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.GetTemp((Telerik.Sitefinity.Libraries.Model.Document) cnt);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.GetTemp((Telerik.Sitefinity.Libraries.Model.Video) cnt);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.GetTemp((Telerik.Sitefinity.Libraries.Model.Album) cnt);
        case Library _:
          return (Content) this.GetTemp((Library) cnt);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.GetTemp((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) cnt);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.GetTemp((Telerik.Sitefinity.Libraries.Model.VideoLibrary) cnt);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));
      }
    }

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwise, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetMaster(Content cnt)
    {
      switch (cnt)
      {
        case Telerik.Sitefinity.Libraries.Model.Image _:
          return (Content) this.GetMaster((Telerik.Sitefinity.Libraries.Model.Image) cnt);
        case Telerik.Sitefinity.Libraries.Model.Document _:
          return (Content) this.GetMaster((Telerik.Sitefinity.Libraries.Model.Document) cnt);
        case Telerik.Sitefinity.Libraries.Model.Video _:
          return (Content) this.GetMaster((Telerik.Sitefinity.Libraries.Model.Video) cnt);
        case Telerik.Sitefinity.Libraries.Model.Album _:
          return (Content) this.GetMaster((Telerik.Sitefinity.Libraries.Model.Album) cnt);
        case Library _:
          return (Content) this.GetMaster((Library) cnt);
        case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
          return (Content) this.GetMaster((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) cnt);
        case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
          return (Content) this.GetMaster((Telerik.Sitefinity.Libraries.Model.VideoLibrary) cnt);
        default:
          throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));
      }
    }

    /// <summary>Gets the lifecycle decorator</summary>
    /// <value>The lifecycle.</value>
    public ILifecycleDecorator Lifecycle
    {
      get
      {
        if (this.lifecycle == null)
        {
          this.lifecycle = LifecycleFactory.CreateLifecycle<MediaContent>((ILifecycleManager) this, new Action<Content, Content>(this.Copy));
          if (this.lifecycle is LifecycleDecorator lifecycle)
            lifecycle.UnpublishItemDelegate = new LifecycleUnpublishItemDelegate(this.OnUnpublishItem);
        }
        return this.lifecycle;
      }
    }

    /// <summary>Creates a language data instance</summary>
    /// <returns></returns>
    public LanguageData CreateLanguageData() => this.Provider.CreateLanguageData();

    /// <summary>Creates a language data instance</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public LanguageData CreateLanguageData(Guid id) => this.Provider.CreateLanguageData(id);

    /// <summary>Gets language data instance by its Id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public LanguageData GetLanguageData(Guid id) => this.Provider.GetLanguageData(id);

    public void Copy(Telerik.Sitefinity.Libraries.Model.DocumentLibrary source, Telerik.Sitefinity.Libraries.Model.DocumentLibrary destination)
    {
      this.CopyLibrary((Library) source, (Library) destination);
      this.Provider.CopyContent((Content) source, (Content) destination);
    }

    public void Copy(Telerik.Sitefinity.Libraries.Model.VideoLibrary source, Telerik.Sitefinity.Libraries.Model.VideoLibrary destination)
    {
      this.CopyLibrary((Library) source, (Library) destination);
      this.Provider.CopyContent((Content) source, (Content) destination);
    }

    public void Copy(Telerik.Sitefinity.Libraries.Model.Album source, Telerik.Sitefinity.Libraries.Model.Album destination)
    {
      destination.ResizeOnUpload = source.ResizeOnUpload;
      destination.NewSize = source.NewSize;
      this.CopyLibrary((Library) source, (Library) destination);
      this.Provider.CopyContent((Content) source, (Content) destination);
    }

    public void Copy(Telerik.Sitefinity.Libraries.Model.Video source, Telerik.Sitefinity.Libraries.Model.Video destination) => this.CopyVideo(source, destination);

    public void Copy(Telerik.Sitefinity.Libraries.Model.Document source, Telerik.Sitefinity.Libraries.Model.Document destination) => this.CopyDocument(source, destination);

    public void Copy(Telerik.Sitefinity.Libraries.Model.Image source, Telerik.Sitefinity.Libraries.Model.Image destination) => this.CopyImage(source, destination);

    public virtual void Copy(Content source, Content destination)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      Type type;
      if ((type = source.GetType()) != destination.GetType())
        throw new ArgumentException("source and destination must be of the same type");
      if (type == typeof (Telerik.Sitefinity.Libraries.Model.Image))
        this.Copy((Telerik.Sitefinity.Libraries.Model.Image) source, (Telerik.Sitefinity.Libraries.Model.Image) destination);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Document))
        this.Copy((Telerik.Sitefinity.Libraries.Model.Document) source, (Telerik.Sitefinity.Libraries.Model.Document) destination);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Video))
        this.Copy((Telerik.Sitefinity.Libraries.Model.Video) source, (Telerik.Sitefinity.Libraries.Model.Video) destination);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        this.Copy((Telerik.Sitefinity.Libraries.Model.Album) source, (Telerik.Sitefinity.Libraries.Model.Album) destination);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        this.Copy((Telerik.Sitefinity.Libraries.Model.DocumentLibrary) source, (Telerik.Sitefinity.Libraries.Model.DocumentLibrary) destination);
      else if (type == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        this.Copy((Telerik.Sitefinity.Libraries.Model.VideoLibrary) source, (Telerik.Sitefinity.Libraries.Model.VideoLibrary) destination);
      else
        throw new NotSupportedException("Type {0} is not supported".Arrange((object) type));
    }

    private void CopyLibrary(Library source, Library destination)
    {
      destination.MaxSize = source.MaxSize;
      destination.MaxItemSize = source.MaxItemSize;
      destination.BlobStorageProvider = source.BlobStorageProvider;
      destination.DownloadSecurityProviderName = source.DownloadSecurityProviderName;
      destination.OutputCacheProfile = source.OutputCacheProfile;
      destination.ClientCacheProfile = source.ClientCacheProfile;
      destination.PrevClientCacheProfile = source.PrevClientCacheProfile;
      destination.OutputCacheDuration = source.OutputCacheDuration;
      destination.ClientCacheDuration = source.ClientCacheDuration;
      destination.OutputSlidingExpiration = source.OutputSlidingExpiration;
      destination.OutputCacheMaxSize = source.OutputCacheMaxSize;
      destination.EnableOutputCache = source.EnableOutputCache;
      destination.EnableClientCache = source.EnableClientCache;
      destination.UseDefaultSettingsForOutputCaching = source.UseDefaultSettingsForOutputCaching;
      destination.UseDefaultSettingsForClientCaching = source.UseDefaultSettingsForClientCaching;
      destination.ParentId = source.ParentId;
      destination.NeedThumbnailsRegeneration = source.NeedThumbnailsRegeneration;
    }

    /// <summary>
    /// Gets the strategy that encapsulates the Recycle Bin operations
    /// like moving an item to, and restoring from the Recycle Bin.
    /// </summary>
    public IRecycleBinStrategy RecycleBin
    {
      get
      {
        if (this.recycleBin == null)
          this.recycleBin = RecycleBinStrategyFactory.CreateRecycleBin((IManager) this);
        return this.recycleBin;
      }
    }

    /// <inheritdoc />
    protected override Type GetConfigType() => typeof (LibrariesConfig);

    /// <inheritdoc />
    public override string ProviderNameDefaultPrefix => "librariesProvider";

    protected override void ApplyProviderDefaultSettings(
      IModuleConfig config,
      DataProviderSettings providerSettings)
    {
      base.ApplyProviderDefaultSettings(config, providerSettings);
      providerSettings.Parameters["urlName"] = LibrariesManager.GenerateUrlName(providerSettings.Name);
    }

    /// <summary>Creates a new folder under a specified parent.</summary>
    /// <param name="parentFolder">The parent folder.</param>
    /// <returns>The newly created folder.</returns>
    public IFolder CreateFolder(IFolder parentFolder) => this.CreateFolder(Guid.NewGuid(), parentFolder);

    /// <summary>Creates a new folder under a specified parent.</summary>
    /// <param name="id">The id of the new folder.</param>
    /// <param name="parentFolder">The parent folder.</param>
    /// <returns>The newly created folder.</returns>
    public IFolder CreateFolder(Guid id, IFolder parentFolder)
    {
      Folder folder1 = this.CreateFolder(id);
      if (!(parentFolder is Folder folder2))
      {
        folder1.RootId = parentFolder.Id;
        folder1.ParentId = new Guid?();
      }
      else
      {
        folder1.RootId = folder2.RootId;
        folder1.ParentId = new Guid?(parentFolder.Id);
      }
      return (IFolder) folder1;
    }

    /// <summary>Deletes the specified folder.</summary>
    /// <param name="folder">The folder.</param>
    /// <exception cref="T:System.ArgumentNullException">folder</exception>
    public void Delete(IFolder folder)
    {
      switch (folder)
      {
        case null:
          throw new ArgumentNullException(nameof (folder));
        case Folder _:
          FolderExtensions.Delete(this, folder as Folder);
          break;
        case Library _:
          this.DeleteLibrary(folder as Library);
          break;
      }
    }

    /// <summary>Finds the folder by id.</summary>
    /// <param name="id">The id.</param>
    /// <returns>The folder with the specified id or null if no folder was found.</returns>
    public IFolder FindFolderById(Guid id)
    {
      Folder folderById = this.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == id));
      if (folderById != null)
        return (IFolder) folderById;
      return (IFolder) this.Provider.GetLibraries().FirstOrDefault<Library>((Expression<Func<Library, bool>>) (l => l.Id == id)) ?? (IFolder) null;
    }

    /// <summary>Gets the folder with the specified id.</summary>
    /// <param name="id">The id.</param>
    /// <returns>The folder.</returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException"></exception>
    public IFolder GetFolder(Guid id) => this.FindFolderById(id) ?? throw new ItemNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) typeof (IFolder).Name, (object) id));

    /// <summary>
    /// Gets the child folders of the specified parent folder.
    /// </summary>
    /// <param name="parentFolder">The parent folder.</param>
    /// <exception cref="T:System.ArgumentNullException">parentFolder</exception>
    public IQueryable<IFolder> GetChildFolders(IFolder parentFolder)
    {
      if (parentFolder == null)
        throw new ArgumentNullException(nameof (parentFolder));
      if (parentFolder is Folder)
        return (IQueryable<IFolder>) this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) parentFolder.Id));
      return (IQueryable<IFolder>) this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.RootId == parentFolder.Id && f.ParentId == new Guid?()));
    }

    /// <summary>
    /// Gets the child items of a specified parent folder as IQuerryable.
    /// </summary>
    /// <param name="parentFolder">The parent folder.</param>
    /// <exception cref="T:System.ArgumentNullException">parentFolder</exception>
    public IQueryable<MediaContent> GetChildItems(IFolder parentFolder)
    {
      if (parentFolder == null)
        throw new ArgumentNullException(nameof (parentFolder));
      if (parentFolder is Folder)
        return this.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (m => m.FolderId == (Guid?) parentFolder.Id));
      return this.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (m => m.Parent == parentFolder));
    }

    /// <summary>Gets the folder of a specified item.</summary>
    /// <param name="item">The item.</param>
    public IFolder GetItemFolder(MediaContent item) => item.FolderId.HasValue ? this.GetFolder(item.FolderId.Value) : (IFolder) item.Parent;

    /// <summary>Changes the item folder.</summary>
    /// <param name="item">The item.</param>
    /// <param name="newParent">The new parent.</param>
    /// <exception cref="T:System.ArgumentNullException">newParent</exception>
    public void ChangeItemFolder(MediaContent item, IFolder newParent)
    {
      if (newParent == null)
        throw new ArgumentNullException(nameof (newParent));
      Guid? folderId1 = item.FolderId;
      Library newParent1;
      if (newParent is Folder folder)
      {
        item.FolderId = new Guid?(folder.Id);
        newParent1 = this.GetLibrary(folder.RootId);
      }
      else
        newParent1 = (Library) newParent;
      if (folderId1.HasValue)
      {
        Guid? nullable = folderId1;
        Guid? folderId2 = item.FolderId;
        if ((nullable.HasValue == folderId2.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != folderId2.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
        {
          this.EnsureItemParent((Content) item, (Content) newParent1, false);
          this.RecompileAndValidateUrls<MediaContent>(item);
          return;
        }
      }
      this.EnsureItemParent((Content) item, (Content) newParent1, true);
    }

    /// <summary>Changes the folder parent.</summary>
    /// <param name="folder">The folder.</param>
    /// <param name="newParent">The new parent.</param>
    public void ChangeFolderParent(IFolder folder, IFolder newParent)
    {
      if (folder == null)
        throw new ArgumentNullException(nameof (folder));
      if (newParent == null)
        throw new ArgumentNullException(nameof (newParent));
      if (!(folder is Folder folder1))
        throw new InvalidOperationException("Folders on the root level (Libraries) cannot have parents.");
      MediaContent[] array;
      bool flag;
      Library newParent1;
      if (newParent is Folder folder2)
      {
        if (folder1.RootId == folder2.RootId)
        {
          Guid? parentId = folder1.ParentId;
          Guid id = folder2.Id;
          if ((parentId.HasValue ? (parentId.HasValue ? (parentId.GetValueOrDefault() == id ? 1 : 0) : 1) : 0) != 0)
            return;
        }
        array = this.GetDescendants(folder).ToArray<MediaContent>();
        flag = folder1.RootId != folder2.RootId;
        newParent1 = flag ? this.GetLibrary(folder2.RootId) : (Library) null;
        folder1.RootId = folder2.RootId;
        folder1.ParentId = new Guid?(folder2.Id);
      }
      else
      {
        if (folder1.RootId == newParent.Id && !folder1.ParentId.HasValue)
          return;
        array = this.GetDescendants(folder).ToArray<MediaContent>();
        flag = folder1.RootId != newParent.Id;
        newParent1 = flag ? (Library) newParent : (Library) null;
        folder1.RootId = newParent.Id;
        folder1.ParentId = new Guid?();
      }
      this.ValidateFolderUrl(folder1);
      foreach (MediaContent content in array)
      {
        if (flag)
          this.ChangeItemParent((Content) content, (Content) newParent1, false);
        this.RecompileAndValidateUrls<MediaContent>(content);
      }
    }

    /// <summary>Gets the descendants of the specified folder.</summary>
    /// <param name="parentFolder">The parent folder.</param>
    public IQueryable<MediaContent> GetDescendants(IFolder parentFolder)
    {
      Folder strongFolder = parentFolder as Folder;
      if (strongFolder != null)
        return this.GetMediaItems().Join((IEnumerable<Folder>) this.GetFolders(), (Expression<Func<MediaContent, Guid?>>) (m => m.FolderId), (Expression<Func<Folder, Guid?>>) (f => (Guid?) f.Id), (m, f) => new
        {
          MediaItem = m,
          Folder = f
        }).Where(j => j.Folder.Path.StartsWith(strongFolder.Path)).Select(j => j.MediaItem);
      return this.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (m => m.Parent == parentFolder));
    }

    /// <summary>Gets the descendants of the specified folders.</summary>
    /// <param name="query">The query.</param>
    /// <param name="parentFolder">The parent folder.</param>
    internal IQueryable<T> GetDescendantsFromQuery<T>(
      IQueryable<T> query,
      IFolder parentFolder)
      where T : MediaContent
    {
      Folder strongFolder = parentFolder as Folder;
      if (strongFolder != null)
        return query.Join((IEnumerable<Folder>) this.GetFolders(), (Expression<Func<T, Guid?>>) (p => p.FolderId), (Expression<Func<Folder, Guid?>>) (f => (Guid?) f.Id), (p, f) => new
        {
          Media = p,
          Folder = f
        }).Where(r => r.Folder.RootId == strongFolder.RootId && r.Folder.Path.StartsWith(strongFolder.Path)).Select(r => r.Media);
      return query.Where<T>((Expression<Func<T, bool>>) (m => m.Parent == parentFolder));
    }

    /// <summary>Gets all folders under the specified parent folder.</summary>
    /// <param name="parentFolder">The parent folder.</param>
    public IQueryable<IFolder> GetAllFolders(IFolder parentFolder)
    {
      Folder strongFolder = parentFolder as Folder;
      IQueryable<Folder> allFolders;
      if (strongFolder != null)
        allFolders = this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.Path.StartsWith(strongFolder.Path)));
      else
        allFolders = this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.RootId == parentFolder.Id));
      return (IQueryable<IFolder>) allFolders;
    }

    /// <summary>Gets all folders under the specified parent folder.</summary>
    /// <param name="folderId">The parent id.</param>
    /// <param name="parentFolder">The parent folder.</param>
    internal IQueryable<Folder> GetAllFolders(Guid? folderId = null, IFolder parentFolder = null)
    {
      if (!folderId.HasValue)
        return this.GetFolders();
      Folder strongFolder = parentFolder as Folder;
      IQueryable<Folder> allFolders;
      if (strongFolder != null)
        allFolders = this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.Path.StartsWith(strongFolder.Path)));
      else
        allFolders = this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.RootId == parentFolder.Id));
      return allFolders;
    }

    internal virtual IQueryable<IFolder> GetFolderNeighbours(
      Guid folderId,
      string sortExpression)
    {
      Folder folder = (Folder) this.FindFolderById(folderId);
      if (folder == null)
        throw new ArgumentException("The folderId passed as an argument is invalid!");
      IQueryable<IFolder> source = (IQueryable<IFolder>) this.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (l => folder.ParentId != new Guid?() ? l.ParentId == folder.ParentId : l.RootId == folder.RootId && l.ParentId == new Guid?()));
      if (sortExpression != null)
      {
        using (new CultureRegion(CultureInfo.InvariantCulture))
          source = source.OrderBy<IFolder>(sortExpression);
      }
      return source;
    }

    /// <summary>
    /// Validates the new URL of a folder when its parent is changed. Throws DuplicateUrlException if conflict is found.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="newParentId">The new parent id.</param>
    private void ValidateFolderNewUrl(IFolder folder, Guid newParentId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LibrariesManager.\u003C\u003Ec__DisplayClass249_0 displayClass2490 = new LibrariesManager.\u003C\u003Ec__DisplayClass249_0();
      // ISSUE: reference to a compiler-generated field
      displayClass2490.urlName = folder.UrlName.ToLower();
      if (newParentId != Guid.Empty)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        LibrariesManager.\u003C\u003Ec__DisplayClass249_1 displayClass2491 = new LibrariesManager.\u003C\u003Ec__DisplayClass249_1();
        // ISSUE: reference to a compiler-generated field
        displayClass2491.CS\u0024\u003C\u003E8__locals1 = displayClass2490;
        IFolder folder1 = this.GetFolder(newParentId);
        if (folder1 is Folder)
        {
          Folder folder2 = (Folder) folder1;
          // ISSUE: reference to a compiler-generated field
          displayClass2491.rootId = folder2.RootId;
          // ISSUE: reference to a compiler-generated field
          displayClass2491.parentId = new Guid?(folder2.Id);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          displayClass2491.rootId = folder1.Id;
          // ISSUE: reference to a compiler-generated field
          displayClass2491.parentId = new Guid?();
        }
        ParameterExpression parameterExpression;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: field reference
        if (this.GetFolders().Any<Folder>(Expression.Lambda<Func<Folder, bool>>((Expression) Expression.AndAlso(f.RootId == displayClass2491.rootId && f.ParentId == displayClass2491.parentId, (Expression) Expression.Equal((Expression) Expression.Call((Expression) Expression.Call(f.UrlName, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) displayClass2491, typeof (LibrariesManager.\u003C\u003Ec__DisplayClass249_1)), FieldInfo.GetFieldFromHandle(__fieldref (LibrariesManager.\u003C\u003Ec__DisplayClass249_1.CS\u0024\u003C\u003E8__locals1))), FieldInfo.GetFieldFromHandle(__fieldref (LibrariesManager.\u003C\u003Ec__DisplayClass249_0.urlName))))), parameterExpression)))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ErrorMessages>().DuplicateUrlException, (object) folder.UrlName.Value), (Exception) null);
      }
      else
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        if (this.GetLibraries().Any<Library>(Expression.Lambda<Func<Library, bool>>((Expression) Expression.Equal((Expression) Expression.Call((Expression) Expression.Call(l.UrlName, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) displayClass2490, typeof (LibrariesManager.\u003C\u003Ec__DisplayClass249_0)), FieldInfo.GetFieldFromHandle(__fieldref (LibrariesManager.\u003C\u003Ec__DisplayClass249_0.urlName)))), parameterExpression)))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ErrorMessages>().DuplicateUrlException, (object) folder.UrlName.Value), (Exception) null);
      }
    }

    /// <summary>
    /// Validates that folder is parent of the newParent. If it is then that would be a circular
    /// reference and InvalidOperationException is thrown, indicating that moving cannot be done.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="newParentId">The new parent id.</param>
    private void ValidateFolderCircularReference(IFolder folder, Guid newParentId)
    {
      if (newParentId == Guid.Empty)
        return;
      IFolder folder1 = this.GetFolder(newParentId);
      if (newParentId == folder.Id)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().CannotChangeParentBecauseOfRecursiveRelation.Arrange((object) folder.Title, (object) folder1.Title));
      if (!(folder1 is Folder))
        return;
      Folder folder2 = (Folder) folder1;
      if (folder is Folder)
      {
        if (folder2.Path.StartsWith(((Folder) folder).Path))
          throw new InvalidOperationException(Res.Get<ErrorMessages>().CannotChangeParentBecauseOfRecursiveRelation.Arrange((object) folder.Title, (object) folder1.Title));
      }
      else if (folder2.RootId == folder.Id)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().CannotChangeParentBecauseOfRecursiveRelation.Arrange((object) folder.Title, (object) folder1.Title));
    }

    /// <summary>Validates the folder.</summary>
    /// <param name="folder">The folder.</param>
    /// <param name="newParentId">The new parent id.</param>
    internal void ValidateFolder(IFolder folder, Guid newParentId)
    {
      this.ValidateFolderNewUrl(folder, newParentId);
      this.ValidateFolderCircularReference(folder, newParentId);
    }

    /// <summary>
    /// Adds the media file URL for the given media content item.
    /// </summary>
    /// <param name="mediaContentItem">The media content item.</param>
    /// <param name="fileUrl">The file URL.</param>
    /// <param name="isDefault">The is default.</param>
    /// <param name="redirectToDefault">The redirect to default.</param>
    /// <param name="cultureId">The culture id. If it is not specified it will be added for the current culture.</param>
    public void AddMediaFileUrl(
      MediaContent mediaContentItem,
      string fileUrl,
      bool isDefault = false,
      bool redirectToDefault = false,
      int? cultureId = null)
    {
      this.Provider.AddMediaFileUrl(mediaContentItem, fileUrl, isDefault, redirectToDefault, cultureId);
    }

    /// <summary>Gets the file link from the given URL.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="published">If the item is published.</param>
    /// <param name="redirectUrl">Returns the redirect URL (if any).</param>
    /// <param name="resolvedCultureId">The culture id of the item link.</param>
    /// <returns>The media content item for the given url.</returns>
    public MediaFileLink GetFileFromUrl(
      string url,
      bool published,
      out string redirectUrl,
      out int resolvedCultureId)
    {
      return this.Provider.GetFileFromUrl(url, published, out redirectUrl, out resolvedCultureId);
    }

    /// <summary>Gets the file link from the given URL.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="published">If the item is published.</param>
    /// <param name="cultureId">The culture id of the item link.</param>
    /// <param name="redirectUrl">Returns the redirect URL (if any).</param>
    /// <returns>The media content item for the given url and culture.</returns>
    public MediaFileLink GetFileFromUrl(
      string url,
      bool published,
      int cultureId,
      out string redirectUrl)
    {
      return this.Provider.GetFileFromUrl(url, published, cultureId, out redirectUrl);
    }

    /// <summary>
    /// Creates/Updates the file urls of the given media content item based on the provided url information in the dictionary.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="item">The item.</param>
    /// <param name="additionalInfo">The additional info.</param>
    public void RecompileMediaFileUrls(
      MediaContent mediaContentItem,
      Dictionary<string, object> additionalInfo = null)
    {
      this.Provider.RecompileMediaFileUrls(mediaContentItem, additionalInfo);
    }

    /// <summary>
    /// Loads the media content additional into the given dictionary.
    /// </summary>
    /// <param name="mediaContentItem">The media content item.</param>
    /// <param name="mediaContentAdditionalInfo">The media file link urls info.</param>
    public void LoadMediaContentAdditionalInfo(
      MediaContent mediaContentItem,
      Dictionary<string, object> mediaContentAdditionalInfo)
    {
      if (mediaContentAdditionalInfo == null)
        throw new ArgumentNullException(nameof (mediaContentAdditionalInfo));
      MediaFileLink fileLink = mediaContentItem.GetFileLink();
      if (fileLink != null)
      {
        mediaContentAdditionalInfo["FileAdditionalUrlsKey"] = (object) fileLink.Urls.Where<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => !u.IsDefault)).Select<MediaFileUrl, string>((Func<MediaFileUrl, string>) (fileUrl => fileUrl.Url)).Cast<object>().ToArray<object>();
        mediaContentAdditionalInfo["AllowMultipleFileUrls"] = (object) fileLink.Urls.Any<MediaFileUrl>((Func<MediaFileUrl, bool>) (u => !u.IsDefault));
        MediaFileUrl mediaFileUrl = fileLink.Urls.FirstOrDefault<MediaFileUrl>((Func<MediaFileUrl, bool>) (fileUrl => !fileUrl.IsDefault));
        mediaContentAdditionalInfo["RedirectToDefault"] = (object) (bool) (mediaFileUrl == null ? 0 : (mediaFileUrl.RedirectToDefault ? 1 : 0));
      }
      this.AddDefaultFileNameToTheAdditionalInfo(mediaContentItem, mediaContentAdditionalInfo);
      this.IncludeItemFullUrlAsAdditionalInfo(mediaContentItem, mediaContentAdditionalInfo);
      this.IncludeTranslationsInfo(mediaContentItem, mediaContentAdditionalInfo);
      this.IncludeIsVectorGraphicsInfo(mediaContentItem, mediaContentAdditionalInfo);
    }

    private void AddDefaultFileNameToTheAdditionalInfo(
      MediaContent mediaContent,
      Dictionary<string, object> additionalInfo)
    {
      MediaFileLink mediaFileLink = mediaContent.GetFileLink();
      if (mediaFileLink == null)
        return;
      if (mediaFileLink.FileId == Guid.Empty)
        mediaFileLink = mediaContent.MediaFileLinks.First<MediaFileLink>();
      using (new CultureRegion(mediaFileLink.Culture))
        additionalInfo.Add("DefaultFileName", (object) ((string) mediaContent.MediaFileUrlName + mediaFileLink.Extension));
    }

    private void IncludeItemFullUrlAsAdditionalInfo(
      MediaContent mediaContent,
      Dictionary<string, object> additionalInfo)
    {
      Lstring[] lstringArray = new Lstring[2]
      {
        mediaContent.MediaFileUrlName,
        mediaContent.UrlName
      };
      additionalInfo.Add("fullUrl", (object) string.Empty);
      foreach (Lstring lstring in lstringArray)
      {
        string str1 = (string) lstring + mediaContent.Extension;
        string str2 = mediaContent.ResolveMediaUrl(true, (CultureInfo) null);
        int length = str2.IndexOf(str1);
        if (length > 0)
        {
          string str3 = str2.Substring(0, length);
          additionalInfo["fullUrl"] = (object) str3;
          break;
        }
      }
    }

    private void IncludeTranslationsInfo(
      MediaContent mediaContentItem,
      Dictionary<string, object> mediaFileLinkUrlsInfo)
    {
      Guid fileId = mediaContentItem.FileId;
      int num = SystemManager.CurrentContext.AppSettings.Current.Multilingual ? mediaContentItem.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (mfl => mfl.FileId == fileId)).Count<MediaFileLink>() - 1 : 0;
      mediaFileLinkUrlsInfo.Add("NumberOfTranslations", (object) num);
    }

    private void IncludeIsVectorGraphicsInfo(
      MediaContent mediaContentItem,
      Dictionary<string, object> additionalInfo)
    {
      additionalInfo.Add("IsVectorGraphics", (object) mediaContentItem.IsVectorGraphics());
    }
  }
}
