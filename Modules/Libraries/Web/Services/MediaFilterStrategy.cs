// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.MediaFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Filters;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  internal class MediaFilterStrategy : IFilterStrategy
  {
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (typeof (MediaContent).IsAssignableFrom(itemType))
      {
        if (typeof (Document).IsAssignableFrom(itemType) && !this.IsDocumentFilteringByExtensionAllowed())
          return (IEnumerable<FilterItem>) filters;
        filters.Add(new FilterItem()
        {
          Name = "extensions",
          Title = string.Format(Res.Get<Labels>().FilterTitle, (object) Res.Get<LibrariesResources>().Extension),
          Parameters = new FilterParameters((IDictionary<string, object>) new Dictionary<string, object>()
          {
            {
              "Type",
              (object) "call"
            },
            {
              "ContentSingularName",
              (object) Res.Get<LibrariesResources>().Extension
            },
            {
              "ContentPluralName",
              (object) Res.Get<LibrariesResources>().Extensions
            }
          })
        });
      }
      return (IEnumerable<FilterItem>) filters;
    }

    public IEnumerable<Result> GetValues(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      int skip,
      int take,
      string search,
      ISet<string> parameters,
      ref int? totalCount)
    {
      if (!typeof (MediaContent).IsAssignableFrom(itemType))
        return Enumerable.Empty<Result>();
      bool flag = this.IsDocumentFilteringByExtensionAllowed();
      IEnumerable<string> source = Enumerable.Empty<string>();
      if (!typeof (Document).IsAssignableFrom(itemType) | flag)
        source = LibrariesManager.GetAllowedExtensions(itemType);
      return source.Distinct<string>().Select<string, Result>((Func<string, Result>) (x => new Result(x, x)));
    }

    public bool TryToFilterBy(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs)
    {
      filteredItemsIDs = Enumerable.Empty<Guid>();
      if (!typeof (MediaContent).IsAssignableFrom(itemType))
        return false;
      LibrariesManager manager = LibrariesManager.GetManager(providerName);
      filteredItemsIDs = (IEnumerable<Guid>) manager.GetMediaFileLinks().Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (mfl => parameters.Contains<string>(mfl.Extension) && mfl.Culture == AppSettings.CurrentSettings.GetCultureLcid(culture))).Select<MediaFileLink, Guid>((Expression<Func<MediaFileLink, Guid>>) (mfl => mfl.MediaContentId)).ToList<Guid>();
      return true;
    }

    public bool TryToFilterByQuery(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      IQueryable query,
      out IQueryable resultQuery)
    {
      resultQuery = (IQueryable) null;
      return false;
    }

    private bool IsDocumentFilteringByExtensionAllowed()
    {
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      return librariesConfig.Documents.AllowedExensions.HasValue && librariesConfig.Documents.AllowedExensions.Value;
    }
  }
}
