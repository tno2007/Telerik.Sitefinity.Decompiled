// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.PreviewUrlProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.ContentLocations.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A calculated property for retrieving Comments of items.
  /// </summary>
  public class PreviewUrlProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (string);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      List<ILifecycleDataItemGeneric> list = items.Cast<ILifecycleDataItemGeneric>().ToList<ILifecycleDataItemGeneric>();
      IList<ILifecycleDataItemGeneric> tempItems = (IList<ILifecycleDataItemGeneric>) null;
      this.GetTempItems((IList<ILifecycleDataItemGeneric>) list, manager, out tempItems);
      foreach (ILifecycleDataItemGeneric lifecycleDataItemGeneric1 in items)
      {
        ILifecycleDataItemGeneric entry = lifecycleDataItemGeneric1;
        if (entry.GetLanguageData(SystemManager.CurrentContext.Culture) != null)
        {
          string empty = string.Empty;
          ILifecycleDataItemGeneric lifecycleDataItemGeneric2 = tempItems.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.OriginalContentId == entry.Id)).FirstOrDefault<ILifecycleDataItemGeneric>();
          string str = lifecycleDataItemGeneric2 == null ? this.GetPreviewUrl(entry) : this.GetPreviewUrl(lifecycleDataItemGeneric2);
          values.Add((object) entry, (object) str);
        }
      }
      return (IDictionary<object, object>) values;
    }

    private string GetPreviewUrl(ILifecycleDataItemGeneric item) => VirtualPathUtility.ToAbsolute("~/" + ContentLocationRoute.path) + "?item_id=" + item.Id.ToString() + "&item_type=" + item.GetType().FullName + "&item_provider=" + (item.Provider as IDataProviderBase).Name + "&item_culture=" + SystemManager.CurrentContext.Culture.Name;

    private void GetTempItems(
      IList<ILifecycleDataItemGeneric> items,
      IManager manager,
      out IList<ILifecycleDataItemGeneric> tempItems)
    {
      tempItems = (IList<ILifecycleDataItemGeneric>) null;
      if (items.Count == 0)
        return;
      Type type = items.First<ILifecycleDataItemGeneric>().GetType();
      List<Guid> masterIds = items.Select<ILifecycleDataItemGeneric, Guid>((Func<ILifecycleDataItemGeneric, Guid>) (x => !(x.OriginalContentId == Guid.Empty) ? x.OriginalContentId : x.Id)).ToList<Guid>();
      tempItems = (IList<ILifecycleDataItemGeneric>) (manager.GetItems(type, (string) null, (string) null, 0, 0) as IQueryable<ILifecycleDataItemGeneric>).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => ((int) x.Status == 1 || (int) x.Status == 8) && masterIds.Contains(x.OriginalContentId))).ToList<ILifecycleDataItemGeneric>();
    }
  }
}
