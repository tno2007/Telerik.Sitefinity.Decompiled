// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.RecycleBinLibrariesStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RecycleBin.Conflicts;
using Telerik.Sitefinity.RecycleBin.Security;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Recycle Bin strategy for <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibrariesManager" /> managers.
  /// Responsible for <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> recycling.
  /// </summary>
  public class RecycleBinLibrariesStrategy : RecycleBinLifecycleStrategy
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.RecycleBinLibrariesStrategy" /> class.
    /// </summary>
    public RecycleBinLibrariesStrategy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.RecycleBinLibrariesStrategy" /> class.
    /// </summary>
    /// <param name="manager">The decorated manager of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.</param>
    /// <param name="eventRegistry">A component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.</param>
    /// <param name="urlValidator">URL validator for the dynamic content items.</param>
    /// <param name="permissionsAuthorizer">The component responsible for authorizing Recycle Bin related actions.</param>
    /// <exception cref="T:System.Exception">The supported recycling manager should be an instance of DynamicModuleManager.</exception>
    [InjectionConstructor]
    public RecycleBinLibrariesStrategy(
      ISupportRecyclingManager manager,
      IRecycleBinEventRegistry eventRegistry,
      IRecycleBinUrlValidator<ILifecycleDataItem> urlValidator,
      IRecycleBinActionsAuthorizer permissionsAuthorizer)
      : base(manager, eventRegistry, permissionsAuthorizer, urlValidator)
    {
    }

    /// <summary>
    /// A template method used for execution of custom additional logic in sub class when moving to recycle bin
    /// </summary>
    /// <param name="dataItem">A lifecycle data item</param>
    protected override void OnMovedToRecycleBin(ILifecycleDataItem dataItem)
    {
      if (!(dataItem is MediaContent content))
        return;
      LibrariesDataProvider provider1 = (LibrariesDataProvider) dataItem.Provider;
      BlobStorageProvider provider2 = provider1.GetBlobStorageManager(content).Provider;
      if (!(provider2 is IExternalBlobStorageProvider))
        return;
      foreach (MediaFileLink mediaFileLink in (IEnumerable<MediaFileLink>) content.MediaFileLinks)
        provider1.MoveBlobToRecycleBin(mediaFileLink, provider2);
      foreach (Thumbnail thumbnail in (IEnumerable<Thumbnail>) content.Thumbnails)
        provider1.MoveBlobToRecycleBin((IBlobContentLocation) thumbnail, provider2);
    }

    /// <summary>
    /// A template method used for execution of custom additional logic in sub class when moving to recycle bin
    /// </summary>
    /// <param name="dataItem">A lifecycle data item</param>
    protected override void OnRestoredFromRecycleBin(ILifecycleDataItem dataItem)
    {
      if (!(dataItem is MediaContent content))
        return;
      LibrariesDataProvider provider1 = (LibrariesDataProvider) dataItem.Provider;
      BlobStorageProvider provider2 = provider1.GetBlobStorageManager(content).Provider;
      if (!(provider2 is IExternalBlobStorageProvider))
        return;
      foreach (MediaFileLink mediaFileLink in (IEnumerable<MediaFileLink>) content.MediaFileLinks)
        provider1.RestoreBlobFromRecycleBin(mediaFileLink, provider2);
      foreach (Thumbnail thumbnail in (IEnumerable<Thumbnail>) content.Thumbnails)
        provider1.RestoreBlobFromRecycleBin((IBlobContentLocation) thumbnail, provider2);
    }
  }
}
