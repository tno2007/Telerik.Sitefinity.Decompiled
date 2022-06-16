// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleFacadeBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>Abstract base facade for all module related facades.</summary>
  public abstract class ModuleFacadeBase<TActualFacade> where TActualFacade : class
  {
    private Type resourceClassType;

    /// <summary>
    /// Gets the type of global resource class used for localizing.
    /// </summary>
    protected Type ResourceClassType => this.resourceClassType;

    /// <summary>Sets the localization class for the facade.</summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for localizing.
    /// </typeparam>
    /// <returns>The current instance of the <see cref="!:TActualFacade" />.</returns>
    public TActualFacade LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.resourceClassType = typeof (TResourceClass);
      return this as TActualFacade;
    }

    /// <summary>Sets the localization class for the facade.</summary>
    /// <param name="resourceClassType">
    /// Type of the localization class to be used for localizing.
    /// </param>
    /// <returns>The current instance of the <see cref="!:TActualFacade" />.</returns>
    public TActualFacade LocalizeUsing(Type resourceClassType)
    {
      this.resourceClassType = resourceClassType;
      return this as TActualFacade;
    }

    /// <summary>
    /// Gets the instance of the page node based on the common node argument.
    /// </summary>
    /// <param name="pageManager">Page manager to be used to retrieve the page node.</param>
    /// <param name="commonNode">One of the common nodes.</param>
    /// <returns>The instance of the page node.</returns>
    protected PageNode GetPageNode(PageManager pageManager, CommonNode commonNode)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      switch (commonNode)
      {
        case CommonNode.Root:
          return pageManager.GetPageNode(SiteInitializer.SitefinityNodeId);
        case CommonNode.Dashboard:
          return pageManager.GetPageNode(SiteInitializer.DashboardPageNodeId);
        case CommonNode.Pages:
          return pageManager.GetPageNode(SiteInitializer.BackendPagesNodeId);
        case CommonNode.Content:
          return pageManager.GetPageNode(SiteInitializer.ContentNodeId);
        case CommonNode.TypesOfContent:
          return pageManager.GetPageNode(SiteInitializer.ModulesNodeId);
        case CommonNode.ClassificationsOfContent:
          return pageManager.GetPageNode(SiteInitializer.TaxonomiesNodeId);
        case CommonNode.Design:
          return pageManager.GetPageNode(SiteInitializer.DesignNodeId);
        case CommonNode.Administration:
          return pageManager.GetPageNode(SiteInitializer.AdministrationNodeId);
        case CommonNode.UserManagement:
          return pageManager.GetPageNode(SiteInitializer.UsersNodeId);
        case CommonNode.Settings:
          return pageManager.GetPageNode(SiteInitializer.SettingsAndConfigurationsNodeId);
        case CommonNode.AlternativePublishing:
          return pageManager.GetPageNode(SiteInitializer.AlternativePublishingNodeId);
        case CommonNode.System:
          return pageManager.GetPageNode(SiteInitializer.SystemNodeId);
        case CommonNode.Marketing:
          return pageManager.GetPageNode(SiteInitializer.MarketingNodeId);
        case CommonNode.MarketingTools:
          return pageManager.GetPageNode(SiteInitializer.MarketingToolsNodeId);
        case CommonNode.Tools:
          return pageManager.GetPageNode(SiteInitializer.ToolsNodeId);
        default:
          throw new NotSupportedException();
      }
    }
  }
}
