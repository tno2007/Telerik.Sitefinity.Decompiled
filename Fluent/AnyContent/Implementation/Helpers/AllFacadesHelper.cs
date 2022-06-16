// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers.AllFacadesHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers
{
  public static class AllFacadesHelper
  {
    public static bool SaveChanges(Telerik.Sitefinity.Fluent.AppSettings settings) => AllFacadesHelper.SaveChanges(settings, (IManager) null);

    public static bool SaveChanges(Telerik.Sitefinity.Fluent.AppSettings settings, IManager manager)
    {
      AllFacadesHelper.RecompileUrls(settings, manager);
      settings.ClearTransactionItems();
      if (!settings.IsGlobalTransaction)
        TransactionManager.CommitTransaction(settings.TransactionName);
      settings.ClearTransactionItems();
      return true;
    }

    /// <summary>
    /// Recompiles the urls for items that were added through the fluent api and did not have their urls build
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="manager">The manager.</param>
    private static void RecompileUrls(Telerik.Sitefinity.Fluent.AppSettings settings, IManager manager)
    {
      if (manager == null)
        return;
      settings.TransactionItems[ItemTrackingStatus.Modified].ForEach((Action<IDataItem>) (item =>
      {
        if (!(item is Content))
          return;
        AllFacadesHelper.EnsureLocatableUrls(item as Content, manager);
      }));
    }

    /// <summary>Ensures the locatable items urls aer created.</summary>
    /// <param name="item">The item.</param>
    /// <param name="manager">The manager.</param>
    public static void EnsureLocatableUrls(Content item, IManager manager)
    {
      if (!(item is ILocatable locatable) || locatable.Urls.Count<UrlData>() != 0)
        return;
      CommonMethods.RecompileItemUrls((Content) locatable, manager);
    }

    public static bool CancelChanges(Telerik.Sitefinity.Fluent.AppSettings settings)
    {
      TransactionManager.RollbackTransaction(settings.TransactionName);
      settings.ClearTransactionItems();
      return true;
    }

    public static IAnyContentManager GetManager(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Type itemType)
    {
      IManager mappedManager = AllFacadesHelper.GetMappedManager(settings, itemType);
      IAnyContentManager manager = ObjectFactory.Resolve<IAnyContentManager>();
      manager.Initialize(mappedManager, itemType);
      return manager;
    }

    public static IManager GetMappedManager(Telerik.Sitefinity.Fluent.AppSettings settings, Type itemType) => ManagerBase.GetMappedManagerInTransaction(itemType, settings.ContentProviderName, settings.TransactionName);

    public static IManager GetManagerInTransaction(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Type managerTypeName,
      string providerName)
    {
      return ManagerBase.GetManagerInTransaction(managerTypeName, providerName, settings.TransactionName);
    }

    public static void CreateVersion(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      IDataItem item,
      Guid ID,
      ContentLifecycleStatus status)
    {
      if (!(item is IVersionSerializable))
        return;
      VersionManager.GetManager(settings.VersioningProviderName, settings.TransactionName).CreateVersion((object) item, ID, status == ContentLifecycleStatus.Live);
    }

    public static void CreateVersion(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Content item,
      ContentLifecycleStatus status)
    {
      if (item == null)
        return;
      Guid ID = item.OriginalContentId == Guid.Empty ? item.Id : item.OriginalContentId;
      AllFacadesHelper.CreateVersion(settings, (IDataItem) item, ID, status);
    }

    public static IAnyPublicFacade GetPublicFacade(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Content item,
      IAnyDraftFacade parentFacade)
    {
      IAnyPublicFacade publicFacade = ObjectFactory.Resolve<IAnyPublicFacade>();
      publicFacade.SetInitialState(settings, item.GetType(), item, parentFacade);
      return publicFacade;
    }

    public static IAnyTempFacade GetTempFacade(
      Telerik.Sitefinity.Fluent.AppSettings settings,
      Content item,
      IAnyDraftFacade parentFacade)
    {
      IAnyTempFacade tempFacade = ObjectFactory.Resolve<IAnyTempFacade>();
      tempFacade.SetInitialState(settings, item.GetType(), item, parentFacade);
      return tempFacade;
    }

    public static IAnyPropertyEditorFacade<TParentFacade> GetPropertyEditorFacade<TParentFacade>(
      TParentFacade parentFacade,
      Content item,
      IManager manager)
      where TParentFacade : class
    {
      Type c = typeof (TParentFacade);
      FacadeHelper.Assert(typeof (IAnyDraftFacade).IsAssignableFrom(c) || typeof (IAnyTempFacade).IsAssignableFrom(c) || typeof (IAnyPublicFacade).IsAssignableFrom(c), "Parent facade must be IAnyDraftFacade, IAnyTempFacade or IAnyPublicFacade");
      IAnyPropertyEditorFacade<TParentFacade> propertyEditorFacade = ObjectFactory.Resolve<IAnyPropertyEditorFacade<TParentFacade>>();
      propertyEditorFacade.SetInitialState(parentFacade, item, manager);
      return propertyEditorFacade;
    }

    /// <summary>
    /// Overrides the behavior for <see cref="T:Telerik.Sitefinity.Libraries.Model.MediaContent" /> objects when checking if they support content life cycle.
    /// If the instance is of type <c>MediaContent</c> the method returns <c>true</c>; otherwise returns the value of <c>SupportsContentLifecycle</c> property.
    /// </summary>
    /// <param name="content">The instance to be checked.</param>
    /// <returns><c>true</c>, if the instance is of type <c>MediaContent</c>; otherwise the value of <c>SupportsContentLifecycle</c> property.</returns>
    internal static bool SupportsContentLifecycle(Content content) => typeof (MediaContent).IsAssignableFrom(content.GetType()) || content.SupportsContentLifecycle;
  }
}
