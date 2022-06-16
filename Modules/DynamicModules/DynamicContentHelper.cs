// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.DynamicModules.DynamicContentHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.DynamicModules
{
  internal static class DynamicContentHelper
  {
    public static void CopyTo(
      this IList<DynamicContentUrlData> sourceUrls,
      IList<DynamicContentUrlData> destinationUrls,
      DynamicContent parent)
    {
      foreach (DynamicContentUrlData sourceUrl in (IEnumerable<DynamicContentUrlData>) sourceUrls)
      {
        DynamicContentUrlData dynamicContentUrlData = new DynamicContentUrlData();
        dynamicContentUrlData.Id = parent.GetNewGuid();
        dynamicContentUrlData.Parent = (IDataItem) parent;
        dynamicContentUrlData.Url = sourceUrl.Url;
        dynamicContentUrlData.Culture = sourceUrl.Culture;
        dynamicContentUrlData.ApplicationName = sourceUrl.ApplicationName;
        dynamicContentUrlData.RedirectToDefault = sourceUrl.RedirectToDefault;
        dynamicContentUrlData.IsDefault = sourceUrl.IsDefault;
        dynamicContentUrlData.ItemType = sourceUrl.ItemType;
        destinationUrls.Add(dynamicContentUrlData);
      }
    }
  }
}
