// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.PageDataContainerResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal class PageDataContainerResolver : IContainerResolver
  {
    private PageManager pageManager;

    public PageDataContainerResolver(PageSiteNode siteNode)
    {
      this.SiteNode = siteNode;
      this.pageManager = PageManager.GetManager();
    }

    public PageSiteNode SiteNode { get; private set; }

    public IControlsContainer GetContainer(bool editVersion, string version)
    {
      PageData pageData = this.GetPageData(this.pageManager, this.SiteNode);
      IControlsContainer targetObject;
      if (editVersion)
      {
        using (new ElevatedModeRegion((IManager) this.pageManager))
        {
          targetObject = (IControlsContainer) pageData.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => x.IsTempDraft));
          if (targetObject == null)
          {
            targetObject = (IControlsContainer) this.pageManager.EditPage(pageData.Id);
            pageData.LockedBy = Guid.Empty;
            this.pageManager.SaveChanges();
          }
          if (!string.IsNullOrEmpty(version))
            VersionManager.GetManager().GetSpecificVersionByChangeId((object) targetObject, Guid.Parse(version));
        }
      }
      else
        targetObject = (IControlsContainer) pageData;
      return targetObject;
    }

    public IControlManager GetManager() => (IControlManager) this.pageManager;

    private PageData GetPageData(PageManager pageManager, PageSiteNode siteNode)
    {
      Guid? variationId = this.GetVariation(siteNode);
      PageData pageData;
      if (variationId.HasValue)
        pageData = pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == siteNode.PageId && p.PersonalizationSegmentId == variationId.Value)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Drafts)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Template)).FirstOrDefault<PageData>();
      else
        pageData = pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Id == siteNode.PageId)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Drafts)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Template)).FirstOrDefault<PageData>();
      return pageData;
    }

    private Guid? GetVariation(PageSiteNode siteNode)
    {
      string variationKey = siteNode.GetVariationKey();
      return !string.IsNullOrEmpty(variationKey) ? new Guid?(Guid.Parse(variationKey)) : new Guid?();
    }
  }
}
