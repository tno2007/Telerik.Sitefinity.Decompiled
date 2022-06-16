// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibrariesContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataProcessing;
using Telerik.Sitefinity.FileProcessors;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.SiteSync.Media;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Implements functionality for converting items from libraries content in transferable format.
  /// </summary>
  internal class LibrariesContentTransfer : StaticContentTransfer
  {
    private ProcessorFactory<LibrariesConfig, IFileProcessor> fileProcessorFactory;
    private IEnumerable<ExportType> supportedTypes;
    private const string AreaName = "Libraries";
    private const string AlbumTypeFullName = "Telerik.Sitefinity.Libraries.Model.Album";
    private const string VideoLibraryTypeFullName = "Telerik.Sitefinity.Libraries.Model.VideoLibrary";
    private const string DocumentLibraryTypeFullName = "Telerik.Sitefinity.Libraries.Model.DocumentLibrary";
    private const string ThumbnailSeparator = "-Thumbnail-Data-Separator-";
    private readonly Lazy<MediaContentImporter> itemsImporter = new Lazy<MediaContentImporter>((Func<MediaContentImporter>) (() =>
    {
      return new MediaContentImporter("Export/Import")
      {
        Serializer = (ISiteSyncSerializer) new SiteSyncSerializer("Export/Import")
      };
    }));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesContentTransfer" /> class.
    /// </summary>
    public LibrariesContentTransfer()
    {
      this.AddDependencies(typeof (Image).FullName, typeof (Album).FullName);
      this.AddDependencies(typeof (Video).FullName, typeof (VideoLibrary).FullName);
      this.AddDependencies(typeof (Document).FullName, typeof (DocumentLibrary).FullName);
    }

    /// <inheritdoc />
    public override void CreateItem(WrapperObject transferableObject, string transactionName)
    {
      Type type = TypeResolutionService.ResolveType(transferableObject.GetPropertyOrDefault<string>("objectTypeId"), false);
      string propertyOrDefault1 = transferableObject.GetPropertyOrDefault<string>("Provider");
      Guid propertyOrDefault2 = transferableObject.GetPropertyOrDefault<Guid>("objectId");
      if (typeof (Stream).IsAssignableFrom(type))
      {
        byte[] bytes = Convert.FromBase64String(transferableObject.GetProperty<string>("BlobStream"));
        byte[] buffer = this.SanitizeInput(transferableObject, bytes);
        transferableObject.SetOrAddProperty("BlobStream", (object) new MemoryStream(buffer));
      }
      this.ItemsImporter.ImportItemInternal(transactionName, type, propertyOrDefault2, transferableObject, propertyOrDefault1, (ISiteSyncImportTransaction) new SiteSyncImportTransaction(), new Action<IDataItem, WrapperObject, IManager>(((ContentTransferBase) this).OnItemImported));
      if (!typeof (Library).IsAssignableFrom(type))
        return;
      this.TryRecompileItemUrls(type, propertyOrDefault2, propertyOrDefault1, transactionName);
    }

    public override void Count(Stream fileStream, ScanOperation operation)
    {
      if (fileStream.Length <= 0L)
        return;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(fileStream);
      XmlElement documentElement = xmlDocument.DocumentElement;
      XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
      nsmgr.AddNamespace("cmisra", "http://docs.oasis-open.org/ns/cmis/restatom/200908/");
      nsmgr.AddNamespace("cmis", "http://docs.oasis-open.org/ns/cmis/core/200908/");
      foreach (ExportType supportTypes in this.GetSupportTypesList())
      {
        if (!(supportTypes.TypeName == typeof (Folder).FullName))
        {
          string xpath1 = string.Format("./cmisra:object[(./cmis:properties/cmis:propertyString[./cmis:value/text() = '{0}' and @propertyDefinitionId = 'cmis:objectTypeId']) and not(./cmis:properties/cmis:propertyId[@propertyDefinitionId='sf:Id']/cmis:value/text() = following::cmisra:object/cmis:properties[./cmis:propertyString[./cmis:value/text() = '{0}']]/cmis:propertyId[@propertyDefinitionId='sf:Id']/cmis:value/text())]", (object) supportTypes.TypeName);
          int count = documentElement.SelectNodes(xpath1, nsmgr).Count;
          if (count > 0)
          {
            if (supportTypes.TypeName == typeof (Album).FullName || supportTypes.TypeName == typeof (VideoLibrary).FullName || supportTypes.TypeName == typeof (DocumentLibrary).FullName)
            {
              string xpath2 = "./cmisra:object[(./cmis:properties/cmis:propertyString[./cmis:value/text() = 'Telerik.Sitefinity.Model.Folder' and @propertyDefinitionId = 'cmis:objectTypeId']) and not(./cmis:properties/cmis:propertyId[@propertyDefinitionId='sf:Id']/cmis:value/text() = following::cmisra:object/cmis:properties[./cmis:propertyString[./cmis:value/text() = 'Telerik.Sitefinity.Model.Folder']]/cmis:propertyId[@propertyDefinitionId='sf:Id']/cmis:value/text())]";
              XmlNodeList xmlNodeList = documentElement.SelectNodes(xpath2, nsmgr);
              count += xmlNodeList.Count;
            }
            AddOnEntry addOnEntry = this.GetAddOnEntry(supportTypes, count);
            operation.AddOnInfo.Entries.Add(addOnEntry);
          }
        }
      }
    }

    /// <inheritdoc />
    public override SiteSyncImporter ItemsImporter => (SiteSyncImporter) this.itemsImporter.Value;

    /// <inheritdoc />
    public override string Area => "Libraries";

    /// <inheritdoc />
    public override IEnumerable<ExportType> SupportedTypes
    {
      get
      {
        if (this.supportedTypes == null)
        {
          IList<ExportType> exportTypeList = (IList<ExportType>) new List<ExportType>();
          ExportType exportType = new ExportType("Libraries", "Libraries", false);
          exportTypeList.Add(exportType);
          this.AddExportType("Images", typeof (Album), exportType.ChildTypes);
          this.AddExportType("Videos", typeof (VideoLibrary), exportType.ChildTypes);
          this.AddExportType("Documents and files", typeof (DocumentLibrary), exportType.ChildTypes);
          this.supportedTypes = (IEnumerable<ExportType>) exportTypeList;
        }
        return this.supportedTypes;
      }
    }

    /// <inheritdoc />
    public override bool IsAvailableForCurrentSite() => SystemManager.IsModuleAccessible("Libraries");

    /// <inheritdoc />
    public override IEnumerable<WrapperObject> Export(
      ExportParams parameters)
    {
      LibrariesContentTransfer librariesContentTransfer = this;
      if (librariesContentTransfer.AllowToProcess(parameters.TypeName))
      {
        IEnumerable<string> languages = librariesContentTransfer.GetAvailableLanguages(parameters);
        foreach (IQueryable<object> query in librariesContentTransfer.GetItemsQueries(parameters))
        {
          int page = 0;
          while (true)
          {
            List<object> items = query.Skip<object>(page * parameters.BufferSize).Take<object>(parameters.BufferSize).ToList<object>();
            foreach (object item in items)
            {
              if (item is ILocalizable)
              {
                foreach (string str in languages)
                {
                  WrapperObject obj = librariesContentTransfer.PreProcessExportObject(item, parameters, str);
                  if (obj != null)
                  {
                    if (item is MediaContent content)
                    {
                      using (new CultureRegion(str))
                      {
                        ExtendedBlobContentProxy blobContentProxy = new ExtendedBlobContentProxy((IChunksBlobContent) content);
                        WrapperObject wrapperObject = new WrapperObject((object) blobContentProxy);
                        librariesContentTransfer.SetCommonProperties(wrapperObject, typeof (Stream).FullName, parameters.ProviderName, str);
                        librariesContentTransfer.SetMediaProperties(wrapperObject, (object) blobContentProxy, str);
                        yield return wrapperObject;
                      }
                    }
                    librariesContentTransfer.IgnoreProperties(obj);
                    yield return obj;
                  }
                  obj = (WrapperObject) null;
                }
              }
              else
              {
                WrapperObject mappedItem = librariesContentTransfer.PreProcessExportObject(item, parameters, string.Empty);
                if (mappedItem != null)
                {
                  librariesContentTransfer.IgnoreProperties(mappedItem);
                  yield return mappedItem;
                }
              }
            }
            if (items.Count<object>() >= parameters.BufferSize)
            {
              ++page;
              items = (List<object>) null;
            }
            else
              break;
          }
        }
      }
    }

    /// <inheritdoc />
    public override IEnumerable<IQueryable<object>> GetItemsQueries(
      ExportParams parameters)
    {
      LibrariesContentTransfer librariesContentTransfer = this;
      if (!librariesContentTransfer.AllowToProcess(parameters.TypeName))
      {
        yield return Enumerable.Empty<object>().AsQueryable<object>();
      }
      else
      {
        LibrariesManager manager = LibrariesManager.GetManager(parameters.ProviderName);
        string typeName = parameters.TypeName;
        if (!(typeName == "Telerik.Sitefinity.Libraries.Model.Album"))
        {
          if (!(typeName == "Telerik.Sitefinity.Libraries.Model.VideoLibrary"))
          {
            if (typeName == "Telerik.Sitefinity.Libraries.Model.DocumentLibrary")
            {
              foreach (IQueryable<object> itemsQuery in librariesContentTransfer.GetItemsQueries<DocumentLibrary, Document>(manager))
                yield return itemsQuery;
            }
          }
          else
          {
            foreach (IQueryable<object> itemsQuery in librariesContentTransfer.GetItemsQueries<VideoLibrary, Video>(manager))
              yield return itemsQuery;
          }
        }
        else
        {
          foreach (IQueryable<object> itemsQuery in librariesContentTransfer.GetItemsQueries<Album, Image>(manager))
            yield return itemsQuery;
        }
      }
    }

    /// <inheritdoc />
    public override void Delete(string sourceName) => this.DeleteImportedData(sourceName, typeof (LibrariesManager), (IList<Type>) new List<Type>()
    {
      typeof (Album),
      typeof (VideoLibrary),
      typeof (DocumentLibrary),
      typeof (Image),
      typeof (Video),
      typeof (Document),
      typeof (Folder)
    });

    /// <inheritdoc />
    public override IEnumerable<ExportType> GetSupportTypesList() => (IEnumerable<ExportType>) new List<ExportType>()
    {
      new ExportType("Images", typeof (Image).FullName),
      new ExportType("Videos", typeof (Video).FullName),
      new ExportType("Documents", typeof (Document).FullName),
      new ExportType("Image Libraries", typeof (Album).FullName),
      new ExportType("Video Libraries", typeof (VideoLibrary).FullName),
      new ExportType("Document Libraries", typeof (DocumentLibrary).FullName),
      new ExportType("Folders", typeof (Folder).FullName)
    };

    /// <inheritdoc />
    protected override void DeleteItems(
      Type managerTypeName,
      Type itemType,
      string provider,
      IList<Guid> itemIds)
    {
      itemIds.Remove(LibrariesModule.DefaultImagesLibraryId);
      itemIds.Remove(LibrariesModule.DefaultVideosLibraryId);
      itemIds.Remove(LibrariesModule.DefaultDocumentsLibraryId);
      LibrariesManager manager = LibrariesManager.GetManager(provider);
      if (typeof (Folder).IsAssignableFrom(itemType))
      {
        IQueryable<Folder> folders = manager.GetFolders();
        Expression<Func<Folder, bool>> predicate = (Expression<Func<Folder, bool>>) (m => itemIds.Contains(m.Id));
        foreach (Folder folder in (IEnumerable<Folder>) folders.Where<Folder>(predicate))
          manager.Delete((IFolder) folder);
      }
      else
      {
        foreach (Telerik.Sitefinity.GenericContent.Model.Content content in manager.GetItems(itemType, (string) null, (string) null, 0, 0).Cast<Telerik.Sitefinity.GenericContent.Model.Content>().Where<Telerik.Sitefinity.GenericContent.Model.Content>((Func<Telerik.Sitefinity.GenericContent.Model.Content, bool>) (m => itemIds.Contains(m.Id) && m.UrlName != (Lstring) "default-album" && m.UrlName != (Lstring) "default-document-library" && m.UrlName != (Lstring) "default-video-library")))
          manager.DeleteItem((object) content);
      }
      manager.SaveChanges();
    }

    /// <inheritdoc />
    protected override WrapperObject PreProcessExportObject(
      object item,
      ExportParams parameters,
      string language)
    {
      WrapperObject wrapperObject = base.PreProcessExportObject(item, parameters, language);
      this.SetMediaProperties(wrapperObject, item, language);
      if (item is Folder folder)
      {
        wrapperObject.AdditionalProperties["ParentId"] = (object) folder.ParentId;
        wrapperObject.AdditionalProperties["RootId"] = (object) folder.RootId;
        wrapperObject.AddProperty("ParentLibraryType", (object) parameters.TypeName);
      }
      return wrapperObject;
    }

    protected override void OnItemImported(IDataItem dataItem, WrapperObject obj, IManager manager)
    {
      base.OnItemImported(dataItem, obj, manager);
      if (!(dataItem is MediaContent mediaContent1))
        return;
      LibrariesManager librariesManager = manager as LibrariesManager;
      MediaContent mediaContent2 = (MediaContent) null;
      try
      {
        mediaContent2 = librariesManager.GetItem(mediaContent1.GetType(), mediaContent1.OriginalContentId) as MediaContent;
      }
      catch (ItemNotFoundException ex)
      {
      }
      if (mediaContent2 == null)
        return;
      string cultureName = obj.GetProperty<string>("LangId");
      CultureRegion cultureRegion = (CultureRegion) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (string.IsNullOrEmpty(cultureName))
          cultureName = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        cultureRegion = new CultureRegion(cultureName);
      }
      try
      {
        librariesManager.RegenerateThumbnails(mediaContent2);
      }
      finally
      {
        cultureRegion?.Dispose();
      }
    }

    private byte[] SanitizeInput(WrapperObject transferableObject, byte[] bytes)
    {
      IEnumerable<IFileProcessor> processors = this.GetFileProcessorFactory().GetProcessors();
      if (processors.Count<IFileProcessor>() > 0)
      {
        foreach (IFileProcessor fileProcessor in processors)
        {
          FileProcessorInput fileInput = new FileProcessorInput();
          fileInput.MimeType = transferableObject.GetPropertyOrDefault<string>("MimeType");
          fileInput.FileExtension = transferableObject.GetPropertyOrDefault<string>("Extension");
          fileInput.FileStream = (Stream) new MemoryStream(bytes);
          if (fileProcessor.CanProcessFile(fileInput))
          {
            using (Stream stream = fileProcessor.Process(fileInput))
            {
              using (MemoryStream destination = new MemoryStream())
              {
                stream.CopyTo((Stream) destination);
                bytes = destination.ToArray();
              }
            }
          }
        }
      }
      return bytes;
    }

    private ProcessorFactory<LibrariesConfig, IFileProcessor> GetFileProcessorFactory() => ProcessorFactory<LibrariesConfig, IFileProcessor>.Instance;

    private IEnumerable<IQueryable<object>> GetItemsQueries<LibraryType, ContentType>(
      LibrariesManager librariesManager)
      where LibraryType : Library
      where ContentType : MediaContent
    {
      IQueryable<Library> libraries = this.GetLibraries<LibraryType>(librariesManager);
      yield return (IQueryable<object>) libraries;
      foreach (IQueryable<object> folder in this.GetFolders(libraries, librariesManager))
        yield return folder;
      yield return this.GetItems<ContentType>(librariesManager);
    }

    private IQueryable<Library> GetLibraries<LibraryType>(
      LibrariesManager librariesManager)
      where LibraryType : Library
    {
      return (IQueryable<Library>) librariesManager.GetItems<LibraryType>();
    }

    private IEnumerable<IQueryable<object>> GetFolders(
      IQueryable<Library> libraries,
      LibrariesManager librariesManager)
    {
      IQueryable<Guid> libraryIds = libraries.Select<Library, Guid>((Expression<Func<Library, Guid>>) (a => a.Id));
      Queue<IQueryable<Folder>> queue = new Queue<IQueryable<Folder>>((IEnumerable<IQueryable<Folder>>) new IQueryable<Folder>[1]
      {
        this.GetChildFolders(librariesManager, new Guid?(), libraryIds)
      });
      while (queue.Any<IQueryable<Folder>>())
      {
        IQueryable<Folder> next = queue.Dequeue();
        yield return (IQueryable<object>) next;
        IQueryable<Folder> source = next;
        Expression<Func<Folder, Guid>> selector = (Expression<Func<Folder, Guid>>) (pn => pn.Id);
        foreach (Guid guid in (IEnumerable<Guid>) source.Select<Folder, Guid>(selector))
          queue.Enqueue(this.GetChildFolders(librariesManager, new Guid?(guid), (IQueryable<Guid>) null));
        next = (IQueryable<Folder>) null;
      }
    }

    private IQueryable<object> GetItems<ContentType>(LibrariesManager librariesManager) where ContentType : MediaContent => (IQueryable<object>) librariesManager.GetItems<ContentType>().Where<ContentType>((Expression<Func<ContentType, bool>>) (i => (int) i.Status == 2 && i.Visible));

    private IQueryable<Folder> GetChildFolders(
      LibrariesManager librariesManager,
      Guid? parentId,
      IQueryable<Guid> libraryIds)
    {
      IQueryable<Folder> source = librariesManager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (l => l.ParentId == parentId));
      if (libraryIds != null && libraryIds.Count<Guid>() > 0)
        source = source.Where<Folder>((Expression<Func<Folder, bool>>) (l => libraryIds.Contains<Guid>(l.RootId)));
      return source;
    }

    private void SetCommonProperties(
      WrapperObject item,
      string typeName,
      string providerName,
      string language)
    {
      item.Language = language;
      item.AddProperty("Provider", (object) providerName);
      item.AddProperty("objectTypeId", (object) typeName);
      item.AddProperty("LangId", (object) language);
    }

    private void SetMediaProperties(WrapperObject item, object dataItem, string lang)
    {
      CultureRegion cultureRegion = (CultureRegion) null;
      try
      {
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          cultureRegion = new CultureRegion(lang);
        if (!(dataItem is ExtendedBlobContentProxy contentItem))
          return;
        Stream mediaStream = this.GetMediaStream((IChunksBlobContent) contentItem, contentItem.BlobStorageProvider);
        item.AddProperty("objectId", (object) contentItem.FileId);
        item.AddProperty("BlobStream", (object) mediaStream);
        item.AddProperty("BlobTotalSize", (object) contentItem.TotalSize);
        item.AddProperty("Extension", (object) contentItem.Extension);
        item.AddProperty("MimeType", (object) contentItem.MimeType);
      }
      finally
      {
        cultureRegion?.Dispose();
      }
    }

    private List<string> GetThumbnails(IEnumerable<Thumbnail> thumbnails)
    {
      string str1 = "{0}-Thumbnail-Data-Separator-{1}-Thumbnail-Data-Separator-{2}-Thumbnail-Data-Separator-{3}-Thumbnail-Data-Separator-{4}";
      List<string> thumbnails1 = new List<string>();
      foreach (Thumbnail thumbnail in thumbnails)
      {
        byte[] inArray = this.ReadStreamAsArray(LibrariesManager.GetManager().Download(thumbnail));
        string str2 = str1.Arrange((object) Convert.ToBase64String(inArray), (object) thumbnail.Name, (object) thumbnail.Type, (object) thumbnail.Width, (object) thumbnail.Height);
        thumbnails1.Add(str2);
      }
      return thumbnails1;
    }

    private byte[] ReadStreamAsArray(Stream input)
    {
      byte[] buffer = new byte[4096];
      using (MemoryStream memoryStream = new MemoryStream())
      {
        int count;
        while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
          memoryStream.Write(buffer, 0, count);
        return memoryStream.ToArray();
      }
    }

    private Stream GetMediaStream(IChunksBlobContent contentItem, string providerName) => BlobStorageManager.GetManager(providerName).GetDownloadStream((IBlobContent) contentItem);

    private void AddExportType(string displayName, Type type, IList<ExportType> result)
    {
      ExportType exportType = new ExportType(displayName, type.FullName);
      result.Add(exportType);
    }

    private void TryRecompileItemUrls(
      Type itemType,
      Guid libraryId,
      string provider,
      string transactionName)
    {
      LibrariesManager manager = LibrariesManager.GetManager(provider, transactionName);
      Library library;
      try
      {
        library = manager.GetItem(itemType, libraryId) as Library;
      }
      catch (Exception ex)
      {
        library = (Library) null;
      }
      if (library == null)
        return;
      manager.RecompileItemUrls<Library>(library);
    }
  }
}
