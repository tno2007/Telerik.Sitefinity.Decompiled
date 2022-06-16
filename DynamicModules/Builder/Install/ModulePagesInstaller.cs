// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Install.ModulePagesInstaller
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Install
{
  internal class ModulePagesInstaller
  {
    private string transactionName = "Pages Installer";
    public const string DynamicModulePageAttribute = "IsDynamicModulePage";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Install.ModulePagesInstaller" /> class.
    /// </summary>
    public ModulePagesInstaller(string transactionName) => this.transactionName = transactionName;

    /// <summary>
    /// Creates a group page for the module and child pages for each type at a root level (each type with no parent type), in the content menu.
    /// </summary>
    /// <param name="module">The module.</param>
    public void CreateModulePages(DynamicModule module, PageManager pageManager)
    {
      if (module.PageId == Guid.Empty)
        module.PageId = pageManager.Provider.GetNewGuid();
      PageNode modulePage = this.CreateModulePage(module, pageManager);
      if (((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).Count<DynamicModuleType>() == 1)
      {
        bool showInNavigation = false;
        DynamicModuleType moduleType1 = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).First<DynamicModuleType>();
        if (moduleType1.PageId == Guid.Empty)
          moduleType1.PageId = pageManager.Provider.GetNewGuid();
        this.CreateContentTypePageInternal(moduleType1.PageId, module, moduleType1, modulePage, showInNavigation, pageManager);
        foreach (DynamicModuleType moduleType2 in (IEnumerable<DynamicModuleType>) ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId != Guid.Empty)).OrderBy<DynamicModuleType, string>((Func<DynamicModuleType, string>) (t => t.DisplayName)))
        {
          if (moduleType2.PageId == Guid.Empty)
            moduleType2.PageId = pageManager.Provider.GetNewGuid();
          this.CreateContentTypePageInternal(moduleType2.PageId, module, moduleType2, modulePage, false, pageManager);
        }
      }
      else
      {
        this.MoveGroupPageNodeUnderContent(modulePage, pageManager);
        foreach (DynamicModuleType moduleType in (IEnumerable<DynamicModuleType>) ((IEnumerable<DynamicModuleType>) module.Types).OrderBy<DynamicModuleType, string>((Func<DynamicModuleType, string>) (t => t.DisplayName)))
        {
          if (moduleType.PageId == Guid.Empty)
            moduleType.PageId = pageManager.Provider.GetNewGuid();
          int num;
          if (!(moduleType.ParentModuleTypeId == Guid.Empty))
          {
            Guid parentModuleTypeId = moduleType.ParentModuleTypeId;
            num = 0;
          }
          else
            num = 1;
          bool showInNavigation = num != 0;
          this.CreateContentTypePageInternal(moduleType.PageId, module, moduleType, modulePage, showInNavigation, pageManager);
        }
      }
    }

    /// <summary>Creates a group module page under 'Types of Content'</summary>
    /// <param name="module">The module.</param>
    public PageNode CreateModulePage(DynamicModule module, PageManager manager)
    {
      if (module.PageId == Guid.Empty)
        module.PageId = manager.Provider.GetNewGuid();
      PageNode childNode = manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == module.PageId)).SingleOrDefault<PageNode>();
      if (childNode == null)
      {
        childNode = manager.CreatePageNode(module.PageId);
        childNode.Name = module.Name;
        childNode.NodeType = NodeType.Group;
        childNode.ModuleName = module.Name;
        PageNode pageNode = manager.GetPageNode(SiteInitializer.ModulesNodeId);
        manager.ChangeParent(childNode, pageNode, false);
        if (!childNode.Attributes.ContainsKey("IsDynamicModulePage"))
          childNode.Attributes.Add("IsDynamicModulePage", "");
        childNode.Title = (Lstring) module.Title;
        childNode.UrlName = (Lstring) module.UrlName;
        childNode.Description = (Lstring) string.Format("This is the group page of the {0} module.", (object) module.Title);
        childNode.RenderAsLink = true;
      }
      return childNode;
    }

    /// <summary>
    /// Creates a content type page under the group page for the specified module in the content menu and moves the group page if necessary
    /// </summary>
    /// <param name="module">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="showInNavigation">Defines whether to show the new content type page or not</param>
    public PageNode CreateContentTypePage(
      Guid pageId,
      DynamicModule module,
      DynamicModuleType moduleType,
      bool showInNavigation)
    {
      PageManager manager = PageManager.GetManager(string.Empty, this.transactionName);
      PageNode groupPageNode = manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == module.PageId)).FirstOrDefault<PageNode>() ?? this.CreateModulePage(module, manager);
      if (moduleType.ParentModuleTypeId == Guid.Empty)
        this.MoveGroupPageNodeUnderContent(module.PageId, manager);
      return this.CreateContentTypePageInternal(pageId, module, moduleType, groupPageNode, showInNavigation, manager);
    }

    /// <summary>
    /// Deletes the module group page and all its child pages.
    /// </summary>
    /// <param name="module">The module.</param>
    public void DeleteModulePage(DynamicModule module)
    {
      PageManager manager = PageManager.GetManager(string.Empty, this.transactionName);
      IQueryable<PageNode> pageNodes1 = manager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate1 = (Expression<Func<PageNode, bool>>) (p => p.ParentId == module.PageId);
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes1.Where<PageNode>(predicate1))
        manager.Delete(pageNode);
      IQueryable<PageNode> pageNodes2 = manager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate2 = (Expression<Func<PageNode, bool>>) (p => p.Id == module.PageId);
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes2.Where<PageNode>(predicate2))
        manager.Delete(pageNode);
      Guid[] moduleTypePageIds = ((IEnumerable<DynamicModuleType>) module.Types).Select<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (t => t.PageId)).ToArray<Guid>();
      IQueryable<PageNode> pageNodes3 = manager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate3 = (Expression<Func<PageNode, bool>>) (p => moduleTypePageIds.Contains<Guid>(p.Id));
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes3.Where<PageNode>(predicate3))
        manager.Delete(pageNode);
    }

    /// <summary>Deletes the page for the specified module type.</summary>
    /// <param name="module">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public void DeleteModuleTypePage(DynamicModule module, DynamicModuleType moduleType)
    {
      PageManager manager = PageManager.GetManager(string.Empty, this.transactionName);
      PageNode pageNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == moduleType.PageId));
      if (pageNode == null)
        return;
      manager.Delete(pageNode);
    }

    /// <summary>
    /// Moves module group page under 'Types of content' if there is only one root content type
    /// </summary>
    /// <param name="module"></param>
    public void ValidateModuleGroupPagePosition(DynamicModule module)
    {
      PageManager manager = PageManager.GetManager(string.Empty, this.transactionName);
      int num = ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).Count<DynamicModuleType>();
      Guid pageId = module.PageId;
      if (num != 1)
        return;
      Guid rootPageId = ModulePagesInstaller.GetRootPageId(module);
      this.MoveGroupPageNodeUnderTypesOfContent(pageId, rootPageId, manager);
    }

    /// <summary>Updates module type page</summary>
    /// <param name="module">The module.</param>
    /// <param name="moduleType">Type of the module.</param>
    public void UpdateModuleTypePage(
      DynamicModule module,
      DynamicModuleType type,
      Guid rootTypeNodeId,
      string title,
      int rootTypesCount,
      bool noParent)
    {
      PageManager manager = PageManager.GetManager(string.Empty, this.transactionName);
      bool showInNavigation = false;
      if (rootTypesCount > 1)
      {
        this.MoveGroupPageNodeUnderContent(module.PageId, manager);
        showInNavigation = noParent;
      }
      else
        this.MoveGroupPageNodeUnderTypesOfContent(module.PageId, rootTypeNodeId, manager);
      PageNode pageNode;
      try
      {
        pageNode = manager.GetPageNode(type.PageId);
      }
      catch (ItemNotFoundException ex)
      {
        pageNode = this.CreateContentTypePage(type.PageId, module, type, showInNavigation);
      }
      pageNode.ShowInNavigation = showInNavigation;
      pageNode.Title = (Lstring) PluralsResolver.Instance.ToPlural(title);
      pageNode.Page.HtmlTitle = (Lstring) title;
      pageNode.Description = (Lstring) string.Format("This is the page of the {0} content type.", (object) title);
    }

    /// <summary>Returns the PageId of the first root module type</summary>
    public static Guid GetRootPageId(DynamicModule module) => ((IEnumerable<DynamicModuleType>) module.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == Guid.Empty)).First<DynamicModuleType>().PageId;

    /// <summary>
    /// Moves group page under 'Content' while creating module pages (used in import case)
    /// </summary>
    private void MoveGroupPageNodeUnderContent(PageNode groupPageNode, PageManager manager)
    {
      PageNode pageNode1 = manager.GetPageNode(SiteInitializer.ContentNodeId);
      manager.ChangeParent(groupPageNode, pageNode1, false);
      groupPageNode.RenderAsLink = false;
      PageNode pageNode2 = manager.GetPageNode(SiteInitializer.ModulesNodeId);
      manager.MovePageNode(groupPageNode, pageNode2, Place.After);
    }

    /// <summary>Moves group page under 'Content'</summary>
    private void MoveGroupPageNodeUnderContent(Guid groupPageId, PageManager manager)
    {
      try
      {
        PageNode pageNode1 = manager.GetPageNode(groupPageId);
        if (!(pageNode1.ParentId == SiteInitializer.ModulesNodeId))
          return;
        PageNode pageNode2 = manager.GetPageNode(SiteInitializer.ContentNodeId);
        manager.ChangeParent(pageNode1, pageNode2, false);
        PageNode pageNode3 = manager.GetPageNode(SiteInitializer.ModulesNodeId);
        manager.MovePageNode(pageNode1, pageNode3, Place.After);
        pageNode1.RenderAsLink = false;
        manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (n => n.ParentId == groupPageId)).OrderBy<PageNode, float>((Expression<Func<PageNode, float>>) (t => t.Ordinal)).First<PageNode>().ShowInNavigation = true;
      }
      catch (ItemNotFoundException ex)
      {
        throw new Exception("Error moving module group page under Content", (Exception) ex)
        {
          Data = {
            [(object) nameof (groupPageId)] = (object) groupPageId
          }
        };
      }
    }

    /// <summary>Moves group page under 'Types of Content'</summary>
    private void MoveGroupPageNodeUnderTypesOfContent(
      Guid groupPageId,
      Guid rootTypeNodeId,
      PageManager manager)
    {
      try
      {
        PageNode pageNode1 = manager.GetPageNode(groupPageId);
        if (pageNode1 != null && pageNode1.ParentId == SiteInitializer.ContentNodeId)
        {
          PageNode pageNode2 = manager.GetPageNode(SiteInitializer.ModulesNodeId);
          manager.ChangeParent(pageNode1, pageNode2, false);
          pageNode1.RenderAsLink = true;
        }
        PageNode pageNode3 = manager.GetPageNode(rootTypeNodeId);
        manager.MovePageNode(pageNode3, MoveTo.FirstInTheCurrentLevel);
        pageNode3.ShowInNavigation = false;
      }
      catch (ItemNotFoundException ex)
      {
        throw new Exception("Error moving module group page under Types of Content", (Exception) ex)
        {
          Data = {
            [(object) nameof (groupPageId)] = (object) groupPageId,
            [(object) nameof (rootTypeNodeId)] = (object) rootTypeNodeId
          }
        };
      }
    }

    /// <summary>
    /// Creates a content type page under the group page for the specified module in the content menu
    /// </summary>
    private PageNode CreateContentTypePageInternal(
      Guid pageId,
      DynamicModule module,
      DynamicModuleType moduleType,
      PageNode groupPageNode,
      bool showInNavigation,
      PageManager manager)
    {
      PageData pageData = this.GetPageData(manager, moduleType, module.Name);
      PageNode childNode = manager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == pageId)) ?? manager.CreatePageNode(pageId);
      childNode.IsBackend = true;
      childNode.ShowInNavigation = showInNavigation;
      childNode.Name = moduleType.DisplayName + "Dashboard";
      childNode.UrlName = (Lstring) ModulePagesInstaller.GetContentTypePageUrl(module, moduleType);
      childNode.Title = (Lstring) PluralsResolver.Instance.ToPlural(moduleType.DisplayName);
      childNode.Description = (Lstring) string.Format("This is the page of the {0} content type.", (object) moduleType.DisplayName);
      pageData.NavigationNode = childNode;
      pageData.HtmlTitle = (Lstring) moduleType.DisplayName;
      manager.ChangeParent(childNode, groupPageNode, false);
      if (!childNode.Attributes.ContainsKey("IsDynamicModulePage"))
        childNode.Attributes.Add("IsDynamicModulePage", "");
      return childNode;
    }

    private PageData GetPageData(
      PageManager pageManager,
      DynamicModuleType moduleType,
      string moduleName)
    {
      PageData pageData = pageManager.CreatePageData();
      pageData.Status = ContentLifecycleStatus.Live;
      pageData.Visible = true;
      pageData.Version = 1;
      pageData.Attributes["ModuleName"] = moduleName;
      pageData.IncludeScriptManager = true;
      LanguageData publishedLanguageData = pageManager.CreatePublishedLanguageData();
      pageData.LanguageData.Add(publishedLanguageData);
      PageTemplate pageTemplate = pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Name == "DefaultBackend")).SingleOrDefault<PageTemplate>();
      pageData.Template = pageTemplate;
      this.AddContentView(this.GetBackendDefinitionName(moduleType), pageData, pageManager, moduleName);
      return pageData;
    }

    private string GetBackendDefinitionName(DynamicModuleType moduleType) => moduleType != null ? ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName()) : throw new ArgumentNullException("module");

    private void AddContentView(
      string controlDefinition,
      PageData pageData,
      PageManager pageManager,
      string moduleName)
    {
      BackendContentView backendContentView = new BackendContentView();
      backendContentView.ModuleName = moduleName;
      backendContentView.ControlDefinitionName = controlDefinition;
      this.AddControl((Control) backendContentView, pageData, pageManager, true);
    }

    private void AddControl(
      Control control,
      PageData pageData,
      PageManager pageManager,
      bool isBackend = false)
    {
      PageControl control1 = pageManager.CreateControl<PageControl>(false);
      control1.IsLayoutControl = false;
      control1.IsBackendObject = isBackend;
      control1.ObjectType = control.GetType().FullName;
      control1.PlaceHolder = "Content";
      pageManager.ReadProperties((object) control, (ObjectData) control1);
      control1.SetDefaultPermissions((IControlManager) pageManager);
      pageData.Controls.Add(control1);
    }

    internal static string GetContentTypePageUrl(DynamicModule module, DynamicModuleType moduleType) => string.Format("{0}-{1}-{2}", (object) module.Name.Replace(" ", "-"), (object) moduleType.DisplayName.Replace(" ", "-"), (object) "dashboard");
  }
}
