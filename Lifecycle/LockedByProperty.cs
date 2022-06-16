// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LockedStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>
  /// A locked by property for retrieving which item is locked.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class LockedStatus : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (ItemEventInfo);

    /// <inheritdoc />
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      List<ILifecycleDataItemGeneric> list1 = items.Cast<ILifecycleDataItemGeneric>().ToList<ILifecycleDataItemGeneric>();
      if (list1.Any<ILifecycleDataItemGeneric>())
      {
        IEnumerable<Guid> masterItemIds = list1.Select<ILifecycleDataItemGeneric, Guid>((Func<ILifecycleDataItemGeneric, Guid>) (x => x.Status == ContentLifecycleStatus.Master ? x.Id : x.OriginalContentId));
        List<ILifecycleDataItemGeneric> list2 = (manager.GetItems(list1.First<ILifecycleDataItemGeneric>().GetType(), (string) null, (string) null, 0, 0) as IQueryable<ILifecycleDataItemGeneric>).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => masterItemIds.Contains<Guid>(x.OriginalContentId) && ((int) x.Status == 1 || (int) x.Status == 8) && x.Owner != Guid.Empty)).Include<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, object>>) (x => x.LanguageData)).ToList<ILifecycleDataItemGeneric>();
        foreach (ILifecycleDataItemGeneric lifecycleDataItemGeneric1 in list1)
        {
          ILifecycleDataItemGeneric entry = lifecycleDataItemGeneric1;
          ItemEventInfo itemEventInfo = (ItemEventInfo) null;
          if (entry.Status == ContentLifecycleStatus.Master && list2.Any<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.OriginalContentId == entry.Id)) || entry.Status == ContentLifecycleStatus.Live && list2.Any<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.OriginalContentId == entry.OriginalContentId)))
          {
            CultureInfo culture = SystemManager.CurrentContext.Culture.GetSitefinityCulture();
            IEnumerable<ILifecycleDataItemGeneric> source = list2.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.OriginalContentId == (entry.Status == ContentLifecycleStatus.Master ? entry.Id : entry.OriginalContentId)));
            if (SystemManager.CurrentContext.AllowConcurrentEditing)
              source = source.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.GetLanguageData(culture) != null));
            ILifecycleDataItemGeneric lifecycleDataItemGeneric2 = source.FirstOrDefault<ILifecycleDataItemGeneric>();
            if (lifecycleDataItemGeneric2 != null)
              itemEventInfo = LockedStatus.GetLockedEventInfo(lifecycleDataItemGeneric2.Owner, lifecycleDataItemGeneric2.LastModified);
          }
          values.Add((object) entry, (object) itemEventInfo);
        }
      }
      return (IDictionary<object, object>) values;
    }

    internal static ItemEventInfo GetLockedEventInfo(Guid owner, DateTime lastModified)
    {
      UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(owner);
      string str = Res.Get<Labels>().UserNotFound;
      string userNotFound = Res.Get<Labels>().UserNotFound;
      if (cachedUserProfile != null)
      {
        str = cachedUserProfile.FirstName + " " + cachedUserProfile.LastName;
        userNotFound = cachedUserProfile.UserId.ToString();
      }
      return new ItemEventInfo()
      {
        User = str,
        Id = userNotFound,
        Date = lastModified
      };
    }
  }
}
