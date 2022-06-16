// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.AdminSitemapNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Data;

namespace Telerik.Sitefinity.Web
{
  internal class AdminSitemapNodeFilter : ISitemapNodeFilter
  {
    private bool IsFilteringForSectionEnabled(string sectionName)
    {
      SitemapNodeFilterElement nodeFilterElement;
      SitemapNodeFilterParameterElement parameterElement;
      bool result;
      return !Config.Get<PagesConfig>().SitemapNodeFilters.TryGetValue("Admin", out nodeFilterElement) || !nodeFilterElement.Parameters.TryGetValue(sectionName, out parameterElement) || !bool.TryParse(parameterElement.ParamterValue, out result) || result;
    }

    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend)
        return false;
      Guid id = pageNode.Id;
      return this.IsFilterEnabled("Admin") && (this.IsUserManagementSectionDenied(id) || this.IsSettignsAndConfigurationSectionDenied(id) || this.IsSystemSectionDenied(id) || this.IsDesignSectionDenied(id) || this.IsTaxonomySectionDenied(id) || this.IsFrontendPageManagementDenied(pageNode) || this.IsToolsSectionDenied(id));
    }

    private bool IsFrontendPageManagementDenied(PageSiteNode node)
    {
      bool flag1 = false;
      if (this.IsFilteringForSectionEnabled("IsFrontendPageManagementFilteringEnabled"))
      {
        bool flag2 = node.Id == SiteInitializer.PagesNodeId;
        PageSiteNode rootNodeCore = (PageSiteNode) ((SiteMapBase) node.Provider).GetRootNodeCore(false);
        if ((flag2 ? 1 : (rootNodeCore == null ? 0 : (rootNodeCore.Id == SiteInitializer.FrontendRootNodeId ? 1 : 0))) == 0)
          return false;
        if (flag2 || SystemManager.IsDesignMode || SystemManager.IsPreviewMode)
        {
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          Guid rootNodeID = multisiteContext != null ? multisiteContext.CurrentSite.SiteMapRootNodeId : SiteInitializer.FrontendRootNodeId;
          PageManager manager = PageManager.GetManager();
          ISecuredObject rootNode;
          using (new ElevatedModeRegion((IManager) manager))
            rootNode = (ISecuredObject) manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == rootNodeID)).SingleOrDefault<PageNode>();
          flag1 = !((IEnumerable<string>) new string[7]
          {
            "ChangeOwner",
            "ChangePermissions",
            "Create",
            "CreateChildControls",
            "Delete",
            "EditContent",
            "Modify"
          }).Any<string>((Func<string, bool>) (action => rootNode.IsGranted("Pages", action)));
        }
      }
      return flag1;
    }

    private bool IsUserManagementSectionDenied(Guid id)
    {
      bool flag = false;
      if (this.IsFilteringForSectionEnabled("IsUserManagementSectionFilteringEnabled"))
      {
        if (id == SiteInitializer.UsersPageId)
          flag = !AppPermission.IsGranted(AppAction.ManageUsers);
        if (id == SiteInitializer.RolesPageId)
          flag = !AppPermission.IsGranted(AppAction.ManageRoles);
        if (id == SiteInitializer.PermissionsPageId)
          flag = !AppPermission.IsGranted(AppAction.ChangePermissions);
        if (id == SiteInitializer.ProfileTypesPageId)
          flag = !AppPermission.IsGranted(AppAction.ManageUserProfiles);
      }
      return flag;
    }

    private bool IsSettignsAndConfigurationSectionDenied(Guid id)
    {
      bool flag = false;
      if (this.IsFilteringForSectionEnabled("IsSettingsAndConfigurationFilteringEnabled"))
      {
        if (id == SiteInitializer.SettingsNodeId || id == SiteInitializer.BasicSettingsNodeId || id == SiteInitializer.AdvancedSettingsNodeId || id == SiteInitializer.ContinuousDeliveryPageId)
          flag = !AppPermission.IsGranted(AppAction.ChangeConfigurations);
        if (id == SiteInitializer.WorkflowPageId)
        {
          List<ISecuredObject> securityRoots = new List<ISecuredObject>();
          foreach (WorkflowDataProvider staticProvider in (Collection<WorkflowDataProvider>) WorkflowManager.GetManager().StaticProviders)
            securityRoots.Add(WorkflowManager.GetManager(staticProvider.Name).SecurityRoot);
          flag = !((IEnumerable<string>) new string[5]
          {
            "ChangeOwner",
            "ChangePermissions",
            "Create",
            "Delete",
            "Modify"
          }).Any<string>((Func<string, bool>) (action => securityRoots.Any<ISecuredObject>((Func<ISecuredObject, bool>) (r => r.IsGranted("WorkflowDefinition", action)))));
        }
      }
      return flag;
    }

    private bool IsSystemSectionDenied(Guid id)
    {
      bool flag = false;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      ISecuredObject securityRoot = WorkflowManager.GetManager().SecurityRoot;
      if (this.IsFilteringForSectionEnabled("IsSystemSectionFilteringEnabled"))
      {
        if (id == SiteInitializer.LabelsPageId)
          flag = !AppPermission.IsGranted(AppAction.ManageLabels);
        if (id == SiteInitializer.LicensePageId)
          flag = !AppPermission.IsGranted(AppAction.ManageLicenses);
        if (id == SiteInitializer.BackendPageModulesAndServicesNodeId)
          flag = !currentIdentity.IsUnrestricted;
        if (id == SiteInitializer.WorkflowPageId)
          flag = this.IsWorkflowDenied(securityRoot);
        if (id == SiteInitializer.BackendPagesWarningPageId || id == SiteInitializer.BackendPagesActualNodeId || id == SiteInitializer.BackendPagesNodeId)
          flag = !AppPermission.IsGranted(AppAction.ManageBackendPages);
        if (id == SiteInitializer.SystemNodeId)
        {
          int num;
          if (!AppPermission.IsGranted(AppAction.ManageLabels))
          {
            if (!AppPermission.IsGranted(AppAction.ManageLicenses))
            {
              if (!AppPermission.IsGranted(AppAction.ManageBackendPages))
              {
                num = this.IsWorkflowDenied(securityRoot) ? 1 : 0;
                goto label_17;
              }
            }
          }
          num = 0;
label_17:
          flag = num != 0;
        }
      }
      return flag;
    }

    private bool IsToolsSectionDenied(Guid id)
    {
      bool flag = false;
      if (id == SiteInitializer.FilesPageId)
        flag = !AppPermission.IsGranted(AppAction.ManageFiles);
      return flag;
    }

    private bool IsDesignSectionDenied(Guid id)
    {
      bool flag = false;
      if (this.IsFilteringForSectionEnabled("IsDesignSectionFilteringEnabled"))
      {
        string[] source = new string[5]
        {
          "ChangeOwner",
          "ChangePermissions",
          "Create",
          "Delete",
          "Modify"
        };
        if (id == SiteInitializer.PageTemplatesNodeId)
        {
          PageTemplate[] inMemTemplates = PageManager.GetManager().GetTemplates().ToArray<PageTemplate>();
          flag = !((IEnumerable<string>) source).Any<string>((Func<string, bool>) (action => ((IEnumerable<PageTemplate>) inMemTemplates).Any<PageTemplate>((Func<PageTemplate, bool>) (r => r.IsGranted("PageTemplates", action)))));
        }
      }
      return flag;
    }

    private bool IsTaxonomySectionDenied(Guid id)
    {
      bool flag = false;
      if (this.IsFilteringForSectionEnabled("IsTaxonomySectionFilteringEnabled") && (id == SiteInitializer.FlatTaxonomyPageId || id == SiteInitializer.HierarchicalTaxonomyPageId || id == SiteInitializer.NetworkTaxonomyPageId || id == SiteInitializer.FacetTaxonomyPageId || id == SiteInitializer.TaxonomiesNodeId || id == SiteInitializer.MarkedItemsPageId))
      {
        List<ISecuredObject> roots = new List<ISecuredObject>();
        foreach (TaxonomyDataProvider staticProvider in (Collection<TaxonomyDataProvider>) TaxonomyManager.GetManager().StaticProviders)
          roots.Add(TaxonomyManager.GetManager(staticProvider.Name).SecurityRoot);
        flag = !((IEnumerable<string>) new string[5]
        {
          "ChangeTaxonomyOwner",
          "ChangeTaxonomyPermissions",
          "CreateTaxonomy",
          "DeleteTaxonomy",
          "ModifyTaxonomyAndSubTaxons"
        }).Any<string>((Func<string, bool>) (action => roots.Any<ISecuredObject>((Func<ISecuredObject, bool>) (r => r.IsGranted("Taxonomies", action)))));
      }
      return flag;
    }

    private bool IsWorkflowDenied(ISecuredObject objectRoot)
    {
      if (!objectRoot.IsGranted("WorkflowDefinition", "Create"))
      {
        if (!objectRoot.IsGranted("WorkflowDefinition", "Modify"))
          return !objectRoot.IsGranted("WorkflowDefinition", "Delete");
      }
      return false;
    }

    public static class ConfigParamNames
    {
      public const string IsUserManagementSectionFilteringEnabled = "IsUserManagementSectionFilteringEnabled";
      public const string IsSystemSectionFilteringEnabled = "IsSystemSectionFilteringEnabled";
      public const string IsDesignSectionFilteringEnabled = "IsDesignSectionFilteringEnabled";
      public const string IsTaxonomySectionFilteringEnabled = "IsTaxonomySectionFilteringEnabled";
      public const string IsFrontendPageManagementFilteringEnabled = "IsFrontendPageManagementFilteringEnabled";
      public const string IsSettingsAndConfigurationFilteringEnabled = "IsSettingsAndConfigurationFilteringEnabled";
    }
  }
}
