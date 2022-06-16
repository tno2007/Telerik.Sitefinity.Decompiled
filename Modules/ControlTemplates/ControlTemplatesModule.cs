// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplatesModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.ControlTemplates.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.ControlTemplates
{
  /// <summary>Represents Control Templates module.</summary>
  public class ControlTemplatesModule : SecuredModuleBase, ITrackingReporter
  {
    /// <summary>The name of the module.</summary>
    public const string ModuleName = "ControlTemplates";
    /// <summary>
    /// Identity for the page group used by all pages in the Control Templates module
    /// </summary>
    public static readonly Guid ControlTemplatesPageGroupId = new Guid("8FBBB774-7844-406D-A773-D7DDB7B4EDAB");
    /// <summary>
    /// Identity of the home (landing) page for the Control Templates module
    /// </summary>
    public static readonly Guid HomePageId = new Guid("B470ADD8-E7F6-4E97-A927-FF54ED243687");
    /// <summary>
    /// Localization resources' class Id for Control Templates
    /// </summary>
    public static readonly string ResourceClassId = typeof (ControlTemplatesResources).Name;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => new Type[1]
    {
      typeof (PageManager)
    };

    /// <summary>Gets the landing page id for the module.</summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => ControlTemplatesModule.HomePageId;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      Res.RegisterResource<ControlTemplatesResources>();
      Telerik.Sitefinity.Configuration.Config.RegisterSection<ControlTemplatesConfig>();
      ObjectFactory.RegisterSitemapNodeFilter<ControlTemplatesSitemapNodeFilter>("ControlTemplates");
      EventHub.Subscribe<IDataEvent>(new SitefinityEventHandler<IDataEvent>(this.DataItemChanged));
      SystemManager.TypeRegistry.Register(typeof (ControlPresentation).FullName, new SitefinityType()
      {
        SingularTitle = Res.Get<ControlTemplatesResources>().ModuleTitle,
        PluralTitle = Res.Get<ControlTemplatesResources>().ModuleTitle,
        Parent = (string) null,
        Kind = SitefinityTypeKind.Type
      });
    }

    private void DataItemChanged(IDataEvent args)
    {
      if (!(args is DataEvent dataEvent) || dataEvent.ItemType != typeof (PageNode) || !dataEvent.Action.Equals(DataEventAction.Deleted))
        return;
      List<CacheDependencyKey> items1 = new List<CacheDependencyKey>();
      CacheDependencyKey cacheDependencyKey = new CacheDependencyKey();
      cacheDependencyKey.Type = typeof (CacheDependencyPageNodeStateChange);
      cacheDependencyKey.Key = dataEvent.ItemId.ToString() + (object) SystemManager.CurrentContext.Culture;
      items1.Add(cacheDependencyKey);
      CacheDependency.Notify((IList<CacheDependencyKey>) items1);
      List<CacheDependencyKey> items2 = new List<CacheDependencyKey>();
      cacheDependencyKey = new CacheDependencyKey();
      cacheDependencyKey.Type = typeof (CacheDependencyPageNodeStateChange);
      cacheDependencyKey.Key = dataEvent.ItemId.ToString();
      items2.Add(cacheDependencyKey);
      CacheDependency.Notify((IList<CacheDependencyKey>) items2);
    }

    /// <summary>Installs this module in Sitefinity system.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
      PageManager pageManager = initializer.PageManager;
      PageNode pageNode = pageManager.GetPageNode(SiteInitializer.TemplatesNodeId);
      Guid id = ControlTemplatesModule.ControlTemplatesPageGroupId;
      PageNode parentNode = pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == id)).SingleOrDefault<PageNode>();
      if (parentNode == null)
      {
        parentNode = initializer.CreatePageNode(id, pageNode, 1.1f, NodeType.Group);
        parentNode.Name = "ControlTemplates";
        parentNode.Title = (Lstring) Res.Expression("ControlTemplatesResources", "PageGroupNodeTitle");
        parentNode.ShowInNavigation = true;
        parentNode.ModuleName = "ControlTemplates";
        Res.SetLstring(parentNode.UrlName, ControlTemplatesModule.ResourceClassId, "PageGroupNodeUrlName");
        Res.SetLstring(parentNode.Description, ControlTemplatesModule.ResourceClassId, "PageGroupNodeDescription");
      }
      id = this.LandingPageId;
      if (pageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == id)) == null)
      {
        PageDataElement pageDataElement = new PageDataElement();
        pageDataElement.PageId = this.LandingPageId;
        pageDataElement.Name = "ControlTemplates";
        pageDataElement.MenuName = "ControlTemplatesTitle";
        pageDataElement.UrlName = "ControlTemplatesUrlName";
        pageDataElement.Description = "ControlTemplatesDescription";
        pageDataElement.HtmlTitle = "ControlTemplatesHtmlTitle";
        pageDataElement.ResourceClassId = ControlTemplatesModule.ResourceClassId;
        pageDataElement.IncludeScriptManager = true;
        pageDataElement.ShowInNavigation = false;
        pageDataElement.EnableViewState = false;
        pageDataElement.TemplateName = "DefaultBackend";
        PageDataElement pageInfo = pageDataElement;
        BackendContentView backendContentView1 = new BackendContentView();
        backendContentView1.ModuleName = "ControlTemplates";
        backendContentView1.ControlDefinitionName = ControlTemplatesDefinitions.BackendDefinitionName;
        BackendContentView backendContentView2 = backendContentView1;
        initializer.CreatePageFromConfiguration((PageElement) pageInfo, parentNode, (Control) backendContentView2).ModuleName = "ControlTemplates";
      }
      this.AddSortingExpression(initializer.Context.GetConfig<ContentViewConfig>(), typeof (PresentationData));
      this.CreateOrUpdateControlPresentationDefaultVersion();
    }

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Telerik.Sitefinity.Configuration.Config.Get<ControlTemplatesConfig>();

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      if (upgradeFrom.Build <= 1405)
      {
        PageNode pageNode = initializer.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (t => t.Id == ControlTemplatesModule.ControlTemplatesPageGroupId));
        if (pageNode != null)
          pageNode.Title = (Lstring) Res.Expression("ControlTemplatesResources", "PageGroupNodeTitle");
      }
      this.CreateOrUpdateControlPresentationDefaultVersion();
    }

    /// <inheritdoc />
    object ITrackingReporter.GetReport()
    {
      IQueryable<ControlPresentation> source = PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (c => c.FriendlyControlName.Contains("MVC")));
      int num1 = source.Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (c => c.LastModified > c.DateCreated)).Count<ControlPresentation>();
      DateTime lastYear = DateTime.UtcNow.AddYears(-1);
      int num2 = source.Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (c => c.LastModified > c.DateCreated && c.LastModified > lastYear)).Count<ControlPresentation>();
      return (object) new ControlTemplatesModuleReport()
      {
        ModuleName = "ControlTemplates",
        CountOfModifiedMVCWidgetTemplates = num1,
        CountOfMVCWidgetTemplatesModifiedLastYear = num2
      };
    }

    private void AddSortingExpression(ContentViewConfig config, Type contentType)
    {
      ConfigElementList<SortingExpressionElement> expressionSettings1 = config.SortingExpressionSettings;
      SortingExpressionElement element1 = new SortingExpressionElement((ConfigElement) config.SortingExpressionSettings);
      element1.ContentType = contentType.FullName;
      element1.SortingExpressionTitle = "NewCreatedOnTop";
      element1.SortingExpression = "DateCreated DESC";
      expressionSettings1.Add(element1);
      ConfigElementList<SortingExpressionElement> expressionSettings2 = config.SortingExpressionSettings;
      SortingExpressionElement element2 = new SortingExpressionElement((ConfigElement) config.SortingExpressionSettings);
      element2.ContentType = contentType.FullName;
      element2.SortingExpressionTitle = "NewModifiedOnTop";
      element2.SortingExpression = "LastModified DESC";
      expressionSettings2.Add(element2);
      ConfigElementList<SortingExpressionElement> expressionSettings3 = config.SortingExpressionSettings;
      SortingExpressionElement element3 = new SortingExpressionElement((ConfigElement) config.SortingExpressionSettings);
      element3.ContentType = contentType.FullName;
      element3.SortingExpressionTitle = "ByWidgets";
      element3.SortingExpression = "FriendlyControlName ASC";
      expressionSettings3.Add(element3);
      ConfigElementList<SortingExpressionElement> expressionSettings4 = config.SortingExpressionSettings;
      SortingExpressionElement element4 = new SortingExpressionElement((ConfigElement) config.SortingExpressionSettings);
      element4.ContentType = contentType.FullName;
      element4.SortingExpressionTitle = "CustomSorting";
      element4.SortingExpression = "Custom";
      element4.IsCustom = true;
      expressionSettings4.Add(element4);
    }

    private void CreateOrUpdateControlPresentationDefaultVersion()
    {
      PageManager manager1 = PageManager.GetManager();
      VersionManager manager2 = VersionManager.GetManager();
      foreach (ControlPresentation presentationItem in (IEnumerable<ControlPresentation>) manager1.GetPresentationItems<ControlPresentation>())
      {
        List<Change> list = manager2.GetItemVersionHistory(presentationItem.Id).ToList<Change>();
        if (!list.Any<Change>())
          manager2.CreateVersion((IDataItem) presentationItem, true);
        else
          list.First<Change>().Data = manager2.Provider.Serialize((object) presentationItem);
      }
      manager2.SaveChanges();
    }
  }
}
