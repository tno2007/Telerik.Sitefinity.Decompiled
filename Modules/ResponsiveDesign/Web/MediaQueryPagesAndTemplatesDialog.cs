// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.MediaQueryPagesAndTemplatesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web
{
  /// <summary>
  /// A dialog listing all pages containing a given media query.
  /// </summary>
  public class MediaQueryPagesAndTemplatesDialog : PagesAndTemplatesDialog
  {
    public static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ResponsiveDesign.PagesAndTemplatesDialog.ascx");
    private PageManager pageManager;
    private ResponsiveDesignManager responsiveDesignManager;
    private string responsiveDesignProviderName;

    /// <summary>
    /// Gets or sets the name of the responsive design provider.
    /// </summary>
    /// <value>The name of the responsive design.</value>
    public string ResponsiveDesignProviderName
    {
      get
      {
        if (this.responsiveDesignProviderName == null)
          this.responsiveDesignProviderName = SystemManager.CurrentHttpContext.Request.QueryString["responsiveDesignProviderName"];
        return this.responsiveDesignProviderName;
      }
      set => this.responsiveDesignProviderName = value;
    }

    public Guid MediaQueryId { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string g = this.Page.Request.QueryString["mediaQueryId"];
      if (!string.IsNullOrEmpty(g) && Utility.IsGuid(g))
      {
        this.MediaQueryId = new Guid(g);
        this.responsiveDesignManager = ResponsiveDesignManager.GetManager(this.ResponsiveDesignProviderName);
        this.pageManager = PageManager.GetManager();
      }
      this.DescriptionLabel.Visible = false;
      base.InitializeControls(dialogContainer);
    }

    internal override IList<NavigationNode> GetNavNodes()
    {
      IEnumerable<PageData> pageDataList = this.GetPageDataList();
      List<NavigationNode> navNodes = new List<NavigationNode>();
      CultureInfo culture = (CultureInfo) null;
      foreach (PageData pageData in pageDataList)
      {
        if (pageData.Culture != null)
          culture = CultureInfo.GetCultureInfo(pageData.Culture);
        navNodes.Add(PageTemplateViewModel.GetNavigationNode(pageData.NavigationNode, pageData.NavigationNode.IsPublished(), culture));
      }
      return (IList<NavigationNode>) navNodes;
    }

    protected override void SetPagesCountLiteral()
    {
      string statisticsText = ContentViewModel.GetStatisticsText(this.PagesCount, this.TemplatesCount, " " + Res.Get<Labels>().And + " ");
      if (!this.IsSuccessfullyUpdatedDialog)
        this.PagesCountLiteral.Text = this.PagesCount + this.TemplatesCount != 1 ? statisticsText + Res.Get<ContentResources>().UseThisGroupMultiple : statisticsText + Res.Get<ContentResources>().UseThisGroupSingle;
      else
        this.PagesCountLiteral.Text = Res.Get<ResponsiveDesignResources>().SuccessfullyUpdatedMediaQuery.Arrange((object) statisticsText);
    }

    [Obsolete("Use GetPageDataList instead")]
    protected override IEnumerable<PageNode> GetPages() => this.MediaQueryId != Guid.Empty ? this.responsiveDesignManager.GetPageNodesForQuery(this.MediaQueryId, this.pageManager) : (IEnumerable<PageNode>) new PageNode[0];

    protected override IEnumerable<PageData> GetPageDataList() => this.MediaQueryId != Guid.Empty ? (IEnumerable<PageData>) this.responsiveDesignManager.GetPageDataForQuery(this.MediaQueryId, this.pageManager) : (IEnumerable<PageData>) new PageData[0];

    protected override IEnumerable<PageTemplate> GetTemplates() => this.MediaQueryId != Guid.Empty ? this.responsiveDesignManager.GetTemplatesForQuery(this.MediaQueryId, this.pageManager) : (IEnumerable<PageTemplate>) new PageTemplate[0];

    protected override string GetTemplatePath() => MediaQueryPagesAndTemplatesDialog.DialogTemplatePath;
  }
}
