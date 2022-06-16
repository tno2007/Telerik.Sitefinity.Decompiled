// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ModuleExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Services
{
  public static class ModuleExtensions
  {
    /// <summary>
    /// Gets the landing page url of the modules that implements <see cref="T:Telerik.Sitefinity.Services.IModule" /> interface.
    /// </summary>
    /// <param name="throws">Throws ArgumentException if LandingPageId property at module instance that implements <see cref="T:Telerik.Sitefinity.Services.IModule" /> interface is not set.</param>
    /// <returns>The landing page url for the module specified.</returns>
    public static string GetLandingPageUrl(this IModule module)
    {
      if (module.LandingPageId == Guid.Empty)
        throw new ArgumentException("LandingPageId is not specified at module :" + module.Name);
      SiteMapNode siteMapNodeFromKey = BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(module.LandingPageId.ToString());
      return siteMapNodeFromKey != null ? siteMapNodeFromKey.Url : string.Empty;
    }

    /// <summary>Installs taxonomy for specific type</summary>
    /// <param name="initializer">The initializer.</param>
    /// <param name="itemType">Type of the item.</param>
    public static void InstallTaxonomy(SiteInitializer initializer, Type itemType)
    {
      TaxonomyManager taxonomyManager = initializer.TaxonomyManager;
      HierarchicalTaxonomy taxonomy1 = ModuleExtensions.GetOrCreateTaxonomy<HierarchicalTaxonomy>(initializer, "Categories", TaxonomyManager.CategoriesTaxonomyId, Res.Get<ContentResources>().Category);
      FlatTaxonomy taxonomy2 = ModuleExtensions.GetOrCreateTaxonomy<FlatTaxonomy>(initializer, "Tags", TaxonomyManager.TagsTaxonomyId, Res.Get<ContentResources>().Tag);
      MetadataManager metadataManager = initializer.MetadataManager;
      MetaType metaType = metadataManager.GetMetaType(itemType) ?? metadataManager.CreateMetaType(itemType);
      MetaField metaField1 = metaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.FieldName == TaxonomyManager.CategoriesMetafieldName));
      PropertyInfo property1 = itemType.GetProperty(TaxonomyManager.CategoriesMetafieldName);
      if (metaField1 == null && property1 == (PropertyInfo) null)
      {
        MetaField metafield = metadataManager.CreateMetafield(TaxonomyManager.CategoriesMetafieldName);
        metafield.TaxonomyProvider = taxonomyManager.Provider.Name;
        metafield.TaxonomyId = taxonomy1.Id;
        metafield.IsSingleTaxon = false;
        string sitefinityTextResource = ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Templates.Fields.FrontendHierarchicalTaxonFieldTag.htm");
        MetaFieldAttribute metaFieldAttribute1 = new MetaFieldAttribute();
        metaFieldAttribute1.Name = DynamicAttributeNames.ControlTag;
        metaFieldAttribute1.Value = sitefinityTextResource;
        MetaFieldAttribute metaFieldAttribute2 = metaFieldAttribute1;
        metafield.MetaAttributes.Add(metaFieldAttribute2);
        IList<MetaFieldAttribute> metaAttributes1 = metafield.MetaAttributes;
        MetaFieldAttribute metaFieldAttribute3 = new MetaFieldAttribute();
        metaFieldAttribute3.Name = DynamicAttributeNames.IsCommonProperty;
        metaFieldAttribute3.Value = "true";
        metaAttributes1.Add(metaFieldAttribute3);
        IList<MetaFieldAttribute> metaAttributes2 = metafield.MetaAttributes;
        MetaFieldAttribute metaFieldAttribute4 = new MetaFieldAttribute();
        metaFieldAttribute4.Name = DynamicAttributeNames.IsBuiltIn;
        metaFieldAttribute4.Value = "true";
        metaAttributes2.Add(metaFieldAttribute4);
        metaType.Fields.Add(metafield);
      }
      MetaField metaField2 = metaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.FieldName == TaxonomyManager.TagsMetafieldName));
      PropertyInfo property2 = itemType.GetProperty(TaxonomyManager.TagsMetafieldName);
      if (metaField2 == null && property2 == (PropertyInfo) null)
      {
        MetaField metafield = metadataManager.CreateMetafield(TaxonomyManager.TagsMetafieldName);
        metafield.TaxonomyProvider = taxonomyManager.Provider.Name;
        metafield.TaxonomyId = taxonomy2.Id;
        metafield.IsSingleTaxon = false;
        string sitefinityTextResource = ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Templates.Fields.FrontendFlatTaxonFieldTag.htm");
        MetaFieldAttribute metaFieldAttribute5 = new MetaFieldAttribute();
        metaFieldAttribute5.Name = DynamicAttributeNames.ControlTag;
        metaFieldAttribute5.Value = sitefinityTextResource;
        MetaFieldAttribute metaFieldAttribute6 = metaFieldAttribute5;
        metafield.MetaAttributes.Add(metaFieldAttribute6);
        IList<MetaFieldAttribute> metaAttributes3 = metafield.MetaAttributes;
        MetaFieldAttribute metaFieldAttribute7 = new MetaFieldAttribute();
        metaFieldAttribute7.Name = DynamicAttributeNames.IsCommonProperty;
        metaFieldAttribute7.Value = "true";
        metaAttributes3.Add(metaFieldAttribute7);
        IList<MetaFieldAttribute> metaAttributes4 = metafield.MetaAttributes;
        MetaFieldAttribute metaFieldAttribute8 = new MetaFieldAttribute();
        metaFieldAttribute8.Name = DynamicAttributeNames.IsBuiltIn;
        metaFieldAttribute8.Value = "true";
        metaAttributes4.Add(metaFieldAttribute8);
        metaType.Fields.Add(metafield);
      }
      ToolboxesConfig config = initializer.Context.GetConfig<ToolboxesConfig>();
      ModuleExtensions.AddTaxonomyControlToToolbox(config, (Taxonomy) taxonomy1, "sfHierarchicalTaxonIcn", "Category");
      ModuleExtensions.AddTaxonomyControlToToolbox(config, (Taxonomy) taxonomy2, "sfFlatTaxonIcn", "Tags");
    }

    private static void AddTaxonomyControlToToolbox(
      ToolboxesConfig toolboxConfig,
      Taxonomy taxonomy,
      string toolboxCssClass,
      string metaFieldName)
    {
      ModuleExtensions.EnsureClassificationsSection(toolboxConfig);
      ToolboxSection toolboxSection = toolboxConfig.Toolboxes["PageControls"].Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == "Classifications")).SingleOrDefault<ToolboxSection>();
      foreach (ToolboxItem toolboxItem in toolboxSection.Tools.Where<ToolboxItem>((Func<ToolboxItem, bool>) (t => t.ControlType == typeof (TaxonomyControl).FullName)))
      {
        if (toolboxItem.Parameters["TaxonomyId"] == taxonomy.Id.ToString())
          return;
      }
      ToolboxItem element = new ToolboxItem((ConfigElement) toolboxSection.Tools)
      {
        ControlType = typeof (TaxonomyControl).FullName,
        Name = taxonomy.Name,
        Title = (string) taxonomy.Title,
        Description = (string) taxonomy.Description,
        Enabled = true,
        CssClass = toolboxCssClass,
        Parameters = new NameValueCollection()
        {
          {
            "TaxonomyId",
            taxonomy.Id.ToString()
          },
          {
            "FieldName",
            metaFieldName
          }
        }
      };
      toolboxSection.Tools.Add(element);
    }

    private static TTaxonomy GetOrCreateTaxonomy<TTaxonomy>(
      SiteInitializer initializer,
      string taxonomyName,
      Guid taxonomyId,
      string taxonName)
      where TTaxonomy : class, ITaxonomy
    {
      TTaxonomy taxonomy = initializer.Context.GetSharedObject<TTaxonomy>(taxonomyName);
      if ((object) taxonomy == null)
      {
        TaxonomyManager taxonomyManager = initializer.TaxonomyManager;
        taxonomy = taxonomyManager.GetTaxonomies<TTaxonomy>().FirstOrDefault<TTaxonomy>((Expression<Func<TTaxonomy, bool>>) (t => t.Name == taxonomyName));
        if ((object) taxonomy == null)
        {
          taxonomy = taxonomyManager.CreateTaxonomy<TTaxonomy>(taxonomyId);
          taxonomy.Name = taxonomyName;
          taxonomy.Title = (Lstring) taxonomyName;
          taxonomy.TaxonName = (Lstring) taxonName;
          ((ISecuredObject) (object) taxonomy).CanInheritPermissions = true;
          ((ISecuredObject) (object) taxonomy).InheritsPermissions = true;
          ((ISecuredObject) (object) taxonomy).SupportedPermissionSets = new string[1]
          {
            "Taxonomies"
          };
        }
        initializer.Context.SetSharedObject(taxonomyName, (object) taxonomy);
      }
      return taxonomy;
    }

    private static void EnsureClassificationsSection(ToolboxesConfig toolboxConfig)
    {
      Toolbox toolbox = toolboxConfig.Toolboxes["PageControls"];
      string classificationSectionName = "Classifications";
      if (toolbox.Sections.Where<ToolboxSection>((Func<ToolboxSection, bool>) (s => s.Name == classificationSectionName)).SingleOrDefault<ToolboxSection>() != null)
        return;
      ToolboxSection element = new ToolboxSection((ConfigElement) toolbox.Sections)
      {
        Name = "Classifications",
        Title = classificationSectionName
      };
      toolbox.Sections.Add(element);
    }

    /// <summary>Registers the data sources.</summary>
    /// <param name="dataSourceRegistry">The data source registry.</param>
    internal static void RegisterDataSources(this ModuleBase module)
    {
      Type[] managers = module.Managers;
      if (managers == null)
        return;
      foreach (Type type in managers)
      {
        if (typeof (IDataSource).IsAssignableFrom(type) && typeof (IManager).IsAssignableFrom(type))
          SystemManager.DataSourceRegistry.RegisterDataSource((IDataSource) new DataSourceProxy(module.Name, type));
      }
    }

    internal static void RegisterStatisticSupport(this ContentModuleBase module)
    {
      Type[] managers = module.Managers;
      if (managers == null)
        return;
      foreach (Type c in managers)
      {
        if (typeof (IContentManager).IsAssignableFrom(c))
          StatisticCache.Current.RegisterStatisticSupport<ContentModuleStatisticSupport>(module.Name, new object[1]
          {
            (object) c
          });
      }
    }

    internal static ISet<Type> GetKnownTypes(this ModuleBase module)
    {
      Type[] managers = module.Managers;
      HashSet<Type> knownTypes1 = new HashSet<Type>();
      if (managers != null)
      {
        foreach (Type managerType in managers)
        {
          foreach (DataProviderBase staticProvider in ManagerBase.GetManager(managerType).StaticProviders)
          {
            Type[] knownTypes2 = staticProvider.GetKnownTypes();
            if (knownTypes2 != null)
            {
              foreach (Type c in knownTypes2)
              {
                if (typeof (IDynamicFieldsContainer).IsAssignableFrom(c))
                  knownTypes1.Add(c);
              }
            }
          }
        }
      }
      return (ISet<Type>) knownTypes1;
    }

    internal static void WithUpgradeTraceLog(this ModuleBase module, Action action, string message)
    {
      try
      {
        action();
        Log.Write((object) "PASSED: {0}".Arrange((object) message), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) "FAILED : {0} - {1}".Arrange((object) message, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    internal static IEnumerable<T> GetConfigsOfType<T>(this ModuleBase module) where T : class
    {
      Type[] configTypes = module.GetConfigTypes();
      return configTypes != null ? ((IEnumerable<Type>) configTypes).Where<Type>((Func<Type, bool>) (t => typeof (T).IsAssignableFrom(t))).Select<Type, T>((Func<Type, T>) (t => Config.Get(t) as T)) : Enumerable.Empty<T>();
    }
  }
}
