// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.Services.VersionHistoryService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.ColumnProviders;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Versioning.Web.Services
{
  /// <summary>
  /// Web service that provides methods for working with version history data of the modules.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class VersionHistoryService : IVersionHistoryService
  {
    /// <summary>Gets the item version history.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of module changes.</returns>
    public CollectionContext<WcfChange> GetItemVersionHistory(
      string itemType,
      string itemId,
      int skip,
      int take)
    {
      VersionManager manager = VersionManager.GetManager();
      IQueryable<Change> queryable = manager.GetChanges();
      Guid parsedItemId = Guid.Parse(itemId);
      if (manager.Provider.ShouldApplyCultureFilter(parsedItemId))
        queryable = manager.Provider.ApplyCultureFilter(queryable, CultureHelpers.GetRequestCultureOrDefault());
      IQueryable<Change> source1 = (IQueryable<Change>) queryable.Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent != default (object) && c.Parent.Id == parsedItemId)).OrderByDescending<Change, int>((Expression<Func<Change, int>>) (o => o.Version));
      if (!string.IsNullOrEmpty(itemType))
      {
        Type type = TypeResolutionService.ResolveType(itemType, false);
        if (type != (Type) null)
        {
          if (type.Equals(typeof (PageNode)) || type.Equals(typeof (PageData)))
            itemType = typeof (PageDraft).FullName;
          else if (type.Equals(typeof (ControlPresentation)))
            itemType = typeof (ControlPresentation).FullName;
          else if (type.Equals(typeof (PageTemplate)))
            itemType = typeof (TemplateDraft).FullName;
        }
        source1 = source1.Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.TypeName == itemType));
      }
      IQueryable<Change> source2 = source1;
      if (take > 0 && skip >= 0)
        source2 = source2.Skip<Change>(skip).Take<Change>(take);
      List<WcfChange> items = new List<WcfChange>();
      foreach (Change change in (IEnumerable<Change>) source2)
      {
        WcfChange wcfChange = new WcfChange(change);
        items.Add(wcfChange);
      }
      CollectionContext<WcfChange> itemVersionHistory = new CollectionContext<WcfChange>((IEnumerable<WcfChange>) items);
      itemVersionHistory.TotalCount = source1.Count<Change>();
      ServiceUtility.DisableCache();
      return itemVersionHistory;
    }

    /// <summary>Deletes the change.</summary>
    /// <param name="changeId">The change id.</param>
    /// <returns>True on success, false on failure</returns>
    public bool DeleteChange(string changeId)
    {
      VersionManager manager = VersionManager.GetManager();
      manager.DeleteChange(new Guid(changeId));
      manager.SaveChanges();
      return true;
    }

    /// <summary>Saves the change comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <param name="changeId">The change id.</param>
    /// <returns>True on success, false on failure</returns>
    public bool SaveChangeComment(string comment, string changeId)
    {
      VersionManager manager = VersionManager.GetManager();
      (manager.GetItem(typeof (Change), new Guid(changeId)) as Change).Comment = comment.Trim();
      manager.SaveChanges();
      return true;
    }

    /// <summary>
    /// Get the columns for the revision history dialog for the given type
    /// </summary>
    /// <returns>Revision history dialog grid columns</returns>
    public IEnumerable<VersionHistoryColumn> GetColumns(
      string itemType,
      string itemId,
      string provider)
    {
      Type type = TypeResolutionService.ResolveType(itemType, false);
      object obj = (object) null;
      if (type != (Type) null && !string.IsNullOrEmpty(itemId) && this.CheckTypeHasVersioning(type))
      {
        Guid id = Guid.Parse(itemId);
        if (id != Guid.Empty)
          obj = ManagerBase.GetMappedManager(itemType, provider).GetItem(type, id);
      }
      List<VersionHistoryColumn> source = new List<VersionHistoryColumn>();
      if (obj != null)
      {
        foreach (IVersionHistoryColumnProvider historyColumnProvider in ObjectFactory.Container.ResolveAll(typeof (IVersionHistoryColumnProvider)).OfType<IVersionHistoryColumnProvider>())
          source.AddRange(historyColumnProvider.GetColumns(type, obj));
      }
      return (IEnumerable<VersionHistoryColumn>) source.OrderBy<VersionHistoryColumn, float>((Func<VersionHistoryColumn, float>) (x => x.Ordinal)).ToList<VersionHistoryColumn>();
    }

    private bool CheckTypeHasVersioning(Type type) => typeof (IVersionSerializable).IsAssignableFrom(type) || typeof (PageNode).IsAssignableFrom(type) || typeof (PageTemplate).IsAssignableFrom(type);
  }
}
