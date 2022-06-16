// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.ColumnProviders.PageVersionHistoryColumnProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Versioning.ColumnProviders
{
  internal class PageVersionHistoryColumnProvider : IVersionHistoryColumnProvider
  {
    public IEnumerable<VersionHistoryColumn> GetColumns(
      Type itemType,
      object item)
    {
      List<VersionHistoryColumn> columns = new List<VersionHistoryColumn>();
      if (typeof (PageData).IsAssignableFrom(itemType))
      {
        if ((item as PageData).NavigationNode.LocalizationStrategy != LocalizationStrategy.Synced)
          return (IEnumerable<VersionHistoryColumn>) columns;
      }
      else
      {
        if (!typeof (PageTemplate).IsAssignableFrom(itemType))
          return (IEnumerable<VersionHistoryColumn>) columns;
        if (((IEnumerable<string>) (item as PageTemplate).AvailableLanguages).Where<string>((Func<string, bool>) (l => !string.IsNullOrEmpty(l))).Count<string>() < 1)
          return (IEnumerable<VersionHistoryColumn>) columns;
      }
      string str = "Metadata";
      columns.Add(new VersionHistoryColumn()
      {
        Field = str,
        Title = Res.Get<PageResources>().ExistingTranslations,
        Template = "<span>#= " + str + " #</span>",
        CssClass = "sfShort",
        HeaderCssClass = "sfShort",
        Ordinal = 3.5f
      });
      return (IEnumerable<VersionHistoryColumn>) columns;
    }
  }
}
