// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentModuleBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.Services.Contracts;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>Represents generic content module.</summary>
  public abstract class ContentModuleBase : SecuredModuleBase, ITypeSettingsProvider
  {
    /// <inheritdoc />
    public override void Load()
    {
      base.Load();
      if (this.Name != "Forms")
        this.RegisterDataSources();
      this.RegisterStatisticSupport();
    }

    /// <summary>Installs taxonomy for specific type</summary>
    /// <param name="initializer">The initializer.</param>
    /// <param name="itemType">Type of the item.</param>
    protected void InstallTaxonomy(SiteInitializer initializer, Type itemType) => ModuleExtensions.InstallTaxonomy(initializer, itemType);

    protected internal virtual IDictionary<Type, Guid> GetTypeLandingPagesMapping() => (IDictionary<Type, Guid>) null;

    /// <inheritdoc />
    public override void Install(SiteInitializer initializer)
    {
      this.InstallPages(initializer);
      this.InstallConfiguration(initializer);
      this.InstallTaxonomies(initializer);
    }

    [Obsolete("Use Res.Expression method instead.")]
    public static string FormatResourceValue(string resourceId, string resourceKey) => "$Resources: " + resourceId + "," + resourceKey;

    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      base.Upgrade(initializer, upgradeFrom);
      if (upgradeFrom.Build < SitefinityVersion.Sitefinity6_2.Build)
      {
        CommentsUtilities.UpgradeComments(this.Managers, this.Name);
        if (initializer.Context.GetConfig(this.ModuleConfig.GetType()) is ModuleConfigBase config)
        {
          foreach (DataProviderSettings providerSettings in (IEnumerable<DataProviderSettings>) config.Providers.Values)
            providerSettings.Parameters.Add("enableCommentsBackwardCompatibility", "True");
        }
      }
      if (upgradeFrom.Build >= SitefinityVersion.Sitefinity7_0.Build)
        return;
      foreach (Type knownType in (IEnumerable<Type>) this.GetKnownTypes())
        this.UpgradeTaxonomies(initializer, knownType);
    }

    private void UpgradeTaxonomies(SiteInitializer initializer, Type itemType)
    {
      MetaType metaType = initializer.MetadataManager.GetMetaType(itemType);
      if (metaType == null)
        return;
      MetaField metaField1 = metaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.FieldName == TaxonomyManager.CategoriesMetafieldName));
      if (metaField1 != null && metaField1.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (x => x.Name == DynamicAttributeNames.IsBuiltIn)) == null)
      {
        IList<MetaFieldAttribute> metaAttributes = metaField1.MetaAttributes;
        MetaFieldAttribute metaFieldAttribute = new MetaFieldAttribute();
        metaFieldAttribute.Name = DynamicAttributeNames.IsBuiltIn;
        metaFieldAttribute.Value = "true";
        metaAttributes.Add(metaFieldAttribute);
      }
      MetaField metaField2 = metaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.FieldName == TaxonomyManager.TagsMetafieldName));
      if (metaField2 == null || metaField2.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (x => x.Name == DynamicAttributeNames.IsBuiltIn)) != null)
        return;
      IList<MetaFieldAttribute> metaAttributes1 = metaField2.MetaAttributes;
      MetaFieldAttribute metaFieldAttribute1 = new MetaFieldAttribute();
      metaFieldAttribute1.Name = DynamicAttributeNames.IsBuiltIn;
      metaFieldAttribute1.Value = "true";
      metaAttributes1.Add(metaFieldAttribute1);
    }

    /// <summary>Installs the pages.</summary>
    /// <param name="initializer">The initializer.</param>
    protected abstract void InstallPages(SiteInitializer initializer);

    /// <summary>Installs the taxonomies.</summary>
    /// <param name="initializer">The initializer.</param>
    protected abstract void InstallTaxonomies(SiteInitializer initializer);

    /// <summary>Installs module's toolbox configuration.</summary>
    /// <param name="initializer">The initializer.</param>
    protected abstract void InstallConfiguration(SiteInitializer initializer);

    IDictionary<string, ITypeSettings> ITypeSettingsProvider.GetTypeSettings() => this.GetContractsInternal();

    internal IDictionary<string, ITypeSettings> GetContractsInternal()
    {
      Type[] managers = this.Managers;
      if (managers == null || !((IEnumerable<Type>) managers).Any<Type>())
        return (IDictionary<string, ITypeSettings>) null;
      Dictionary<string, ITypeSettings> contractsInternal = new Dictionary<string, ITypeSettings>();
      foreach (Type managerType in managers)
      {
        IManager manager = ManagerBase.GetManager(managerType);
        if (!(manager is IControlManager))
        {
          foreach (DataProviderBase staticProvider in manager.StaticProviders)
          {
            Type[] knownTypes = staticProvider.GetKnownTypes();
            if (knownTypes != null)
            {
              foreach (Type clrType in knownTypes)
              {
                if (!contractsInternal.ContainsKey(clrType.FullName))
                {
                  string plural = PluralsResolver.Instance.ToPlural(clrType.Name.ToLowerInvariant());
                  ITypeSettings typeSettings = ContractFactory.Instance.Create(clrType, plural);
                  if (typeSettings != null)
                    contractsInternal.Add(typeSettings.ClrType, typeSettings);
                }
              }
            }
          }
        }
      }
      return (IDictionary<string, ITypeSettings>) contractsInternal;
    }
  }
}
