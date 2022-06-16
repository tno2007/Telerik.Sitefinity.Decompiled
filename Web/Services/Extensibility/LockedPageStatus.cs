// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.LockedPageStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.UserProfiles;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving page locked status</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class LockedPageStatus : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (ItemEventInfo);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (object key in items)
      {
        ItemEventInfo itemEventInfo = (ItemEventInfo) null;
        PageDataProxy currentPageDataItem = PropertyHelpers.GetSiteMapNode(key).CurrentPageDataItem;
        if (currentPageDataItem.LockedBy != Guid.Empty)
          itemEventInfo = new ItemEventInfo()
          {
            User = UserProfilesHelper.GetUserDisplayName(currentPageDataItem.LockedBy),
            Id = currentPageDataItem.LockedBy.ToString(),
            Date = currentPageDataItem.LastModified
          };
        values.Add(key, (object) itemEventInfo);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
