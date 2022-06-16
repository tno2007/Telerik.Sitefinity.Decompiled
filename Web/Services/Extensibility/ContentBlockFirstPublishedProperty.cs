// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ContentBlockFirstPublishedProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.UserProfiles;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving first published status</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class ContentBlockFirstPublishedProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (ItemEventInfo);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (ContentItem key in items.Cast<ContentItem>())
      {
        ItemEventInfo itemEventInfo = new ItemEventInfo()
        {
          Date = key.DateCreated,
          User = UserProfilesHelper.GetUserDisplayName(key.Owner)
        };
        values.Add((object) key, (object) itemEventInfo);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
