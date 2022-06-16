// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.SiteSync.Media.MediaContentImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.SiteSync.Media
{
  internal class MediaContentImporter : MediaImporter
  {
    private const string AlbumType = "Telerik.Sitefinity.Libraries.Model.Album";
    private const string ImageType = "Telerik.Sitefinity.Libraries.Model.Image";
    private const string VideoLibraryType = "Telerik.Sitefinity.Libraries.Model.VideoLibrary";
    private const string VideoType = "Telerik.Sitefinity.Libraries.Model.Video";
    private const string DocumentLibraryType = "Telerik.Sitefinity.Libraries.Model.DocumentLibrary";
    private const string DocumentType = "Telerik.Sitefinity.Libraries.Model.Document";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.SiteSync.Media.MediaContentImporter" /> class.
    /// </summary>
    public MediaContentImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.SiteSync.Media.MediaContentImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public MediaContentImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    internal override void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      LibrariesManager manager = LibrariesManager.GetManager(provider, transactionName);
      if (itemType == typeof (Folder))
        this.SetCurrentDefaultLibraryIfNotExists(TypeResolutionService.ResolveType(item.GetProperty<string>("ParentLibraryType")), item, manager, "RootId");
      else if (typeof (Library).IsAssignableFrom(itemType))
      {
        string urlName;
        if (this.IsDefaultLibrary(item, out urlName) && this.GetLibrary(manager, urlName) != null)
          return;
      }
      else if (typeof (MediaContent).IsAssignableFrom(itemType))
        this.SetCurrentDefaultLibraryIfNotExists(itemType, item, manager);
      base.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
    }

    private void SetCurrentDefaultLibraryIfNotExists(
      Type itemType,
      WrapperObject item,
      LibrariesManager manager,
      string rootPropertyName = "ParentId")
    {
      Guid parentId = item.GetProperty<Guid>(rootPropertyName);
      if (manager.GetLibraries().Any<Library>((Expression<Func<Library, bool>>) (c => c.Id == parentId)))
        return;
      string defaultLibraryUrlName = this.GetDefaultLibraryUrlName(itemType.FullName);
      Library library = this.GetLibrary(manager, defaultLibraryUrlName);
      if (library == null)
        return;
      item.SetProperty(rootPropertyName, (object) library.Id);
    }

    private Library GetLibrary(LibrariesManager librariesManager, string urlName)
    {
      string predicate = string.Format("(UrlName.ToUpper().Equals(\"{0}\") OR UrlName[\"\"].ToUpper().Equals(\"{0}\"))", (object) urlName.ToUpper());
      return librariesManager.GetLibraries().Where<Library>(predicate).FirstOrDefault<Library>();
    }

    private string GetDefaultLibraryUrlName(string itemType)
    {
      if (itemType == "Telerik.Sitefinity.Libraries.Model.Image" || itemType == "Telerik.Sitefinity.Libraries.Model.Album")
        return "default-album";
      if (itemType == "Telerik.Sitefinity.Libraries.Model.Video" || itemType == "Telerik.Sitefinity.Libraries.Model.VideoLibrary")
        return "default-video-library";
      return itemType == "Telerik.Sitefinity.Libraries.Model.Document" || itemType == "Telerik.Sitefinity.Libraries.Model.DocumentLibrary" ? "default-document-library" : (string) null;
    }
  }
}
