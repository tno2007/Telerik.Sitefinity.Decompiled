// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentBlockPageService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ContentBlockPageService : IContentBlockPageService
  {
    /// <summary>Gets the content blocks by pages.</summary>
    /// <param name="cultureName">Name of the culture.</param>
    /// <returns></returns>
    public CollectionContext<ContentBlockPageViewModel> GetContentBlocksByPages(
      string cultureName)
    {
      string empty1 = string.Empty;
      ContentManager manager = ContentManager.GetManager();
      PageNode[] pageNodeArray = string.IsNullOrWhiteSpace(cultureName) ? manager.GetPagesThatUseSharedContent(empty1) : manager.GetPagesThatUseSharedContent(empty1, Guid.Empty, new CultureInfo(cultureName), SharedContentStatisticsMode.ExcludeTemps);
      List<ContentBlockPageViewModel> items = new List<ContentBlockPageViewModel>();
      CultureInfo cultureInfo = cultureName == null ? (CultureInfo) null : new CultureInfo(cultureName);
      foreach (PageNode pageNode in pageNodeArray)
      {
        string empty2 = string.Empty;
        string fullTitlesPath = pageNode.Parent.GetFullTitlesPath(" > ");
        string str = string.IsNullOrEmpty(fullTitlesPath) ? "on top level" : string.Format("under {0}", (object) fullTitlesPath);
        Guid id = pageNode.GetPageData().Id;
        int num = string.IsNullOrWhiteSpace(cultureName) ? manager.GetContentByPage(id, empty1).Count<ContentItem>() : manager.GetContentByPage(id, empty1, cultureInfo).Count<ContentItem>();
        string title = (string) pageNode.Title;
        if (!string.IsNullOrWhiteSpace(cultureName) && pageNode.LocalizationStrategy == LocalizationStrategy.Synced && PageHelper.IsPageNodeForLanguage(pageNode, cultureInfo))
          title = pageNode.Title.GetString(cultureInfo, false);
        items.Add(new ContentBlockPageViewModel()
        {
          Id = pageNode.Id,
          PageTitle = title,
          PageBreadcrumb = str,
          BlocksCount = num
        });
      }
      return new CollectionContext<ContentBlockPageViewModel>((IEnumerable<ContentBlockPageViewModel>) items)
      {
        TotalCount = pageNodeArray.Length
      };
    }
  }
}
