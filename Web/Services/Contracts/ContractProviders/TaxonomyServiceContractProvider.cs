// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.TaxonomyServiceContractProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services.Contracts;
using Telerik.Sitefinity.Web.Services.Contracts.Properties;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class TaxonomyServiceContractProvider : ITypeSettingsProvider
  {
    public const string TaxonomyUrl = "taxonomies";
    public const string FlatTaxonomyUrl = "flat-taxa";
    public const string HierarchyTaxonomyUrl = "hierarchy-taxa";
    public const string TaxaUrlPropertyName = "TaxaUrl";
    public const string TaxaDefaultTitlePropertyName = "DefaultTitle";
    public const string TaxaDefaultTaxonNamePropertyName = "DefaultTaxonName";
    public const string TaxonomySharedWithPropertyName = "TaxonomySharedWith";

    public IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      ITypeSettings taxonomyContract = this.GetTaxonomyContract();
      ITypeSettings typeSettings1 = ContractFactory.Instance.Create(typeof (FlatTaxon), "flat-taxa");
      ITypeSettings typeSettings2 = ContractFactory.Instance.Create(typeof (HierarchicalTaxon), "hierarchy-taxa");
      PersistentPropertyMappingProxy propertyMappingProxy1 = new PersistentPropertyMappingProxy();
      propertyMappingProxy1.Name = LinqHelper.MemberName<Taxon>((Expression<Func<Taxon, object>>) (x => (object) x.ParentId));
      propertyMappingProxy1.AllowFilter = true;
      PersistentPropertyMappingProxy propertyMappingProxy2 = propertyMappingProxy1;
      typeSettings2.Properties.Add((IPropertyMapping) propertyMappingProxy2);
      return (IDictionary<string, ITypeSettings>) new Dictionary<string, ITypeSettings>()
      {
        {
          taxonomyContract.ClrType,
          taxonomyContract
        },
        {
          typeSettings1.ClrType,
          typeSettings1
        },
        {
          typeSettings2.ClrType,
          typeSettings2
        }
      };
    }

    private ITypeSettings GetTaxonomyContract()
    {
      PersistentPropertyMappingProxy[] propertyMappingProxyArray1 = new PersistentPropertyMappingProxy[7];
      PersistentPropertyMappingProxy propertyMappingProxy1 = new PersistentPropertyMappingProxy();
      propertyMappingProxy1.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => (object) t.Id));
      propertyMappingProxy1.IsKey = true;
      propertyMappingProxy1.ReadOnly = true;
      propertyMappingProxy1.AllowFilter = true;
      propertyMappingProxyArray1[0] = propertyMappingProxy1;
      PersistentPropertyMappingProxy propertyMappingProxy2 = new PersistentPropertyMappingProxy();
      propertyMappingProxy2.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => (object) t.LastModified));
      propertyMappingProxyArray1[1] = propertyMappingProxy2;
      PersistentPropertyMappingProxy propertyMappingProxy3 = new PersistentPropertyMappingProxy();
      propertyMappingProxy3.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => t.Name));
      propertyMappingProxyArray1[2] = propertyMappingProxy3;
      PersistentPropertyMappingProxy propertyMappingProxy4 = new PersistentPropertyMappingProxy();
      propertyMappingProxy4.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => t.Title));
      propertyMappingProxyArray1[3] = propertyMappingProxy4;
      PersistentPropertyMappingProxy propertyMappingProxy5 = new PersistentPropertyMappingProxy();
      propertyMappingProxy5.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => t.Description));
      propertyMappingProxyArray1[4] = propertyMappingProxy5;
      PersistentPropertyMappingProxy propertyMappingProxy6 = new PersistentPropertyMappingProxy();
      propertyMappingProxy6.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => t.TaxonName));
      propertyMappingProxyArray1[5] = propertyMappingProxy6;
      PersistentPropertyMappingProxy propertyMappingProxy7 = new PersistentPropertyMappingProxy();
      propertyMappingProxy7.Name = LinqHelper.MemberName<Taxonomy>((Expression<Func<Taxonomy, object>>) (t => (object) t.RootTaxonomyId));
      propertyMappingProxyArray1[6] = propertyMappingProxy7;
      IEnumerable<IPropertyMapping> first = (IEnumerable<IPropertyMapping>) propertyMappingProxyArray1;
      CalculatedPropertyMappingProxy[] propertyMappingProxyArray2 = new CalculatedPropertyMappingProxy[5];
      CalculatedPropertyMappingProxy propertyMappingProxy8 = new CalculatedPropertyMappingProxy();
      propertyMappingProxy8.Name = "TaxaUrl";
      propertyMappingProxy8.ResolverType = typeof (TaxonomyUrlProperty).FullName;
      propertyMappingProxyArray2[0] = propertyMappingProxy8;
      ClassPropertyMappingProxy propertyMappingProxy9 = new ClassPropertyMappingProxy();
      propertyMappingProxy9.Name = "Type";
      propertyMappingProxy9.ResolverType = typeof (TaxonomyTypeProperty).FullName;
      propertyMappingProxy9.AllowFilter = true;
      propertyMappingProxyArray2[1] = (CalculatedPropertyMappingProxy) propertyMappingProxy9;
      ClassPropertyMappingProxy propertyMappingProxy10 = new ClassPropertyMappingProxy();
      propertyMappingProxy10.Name = "DefaultTitle";
      propertyMappingProxy10.ResolverType = typeof (TaxonomyDefaultTitleProperty).FullName;
      propertyMappingProxyArray2[2] = (CalculatedPropertyMappingProxy) propertyMappingProxy10;
      ClassPropertyMappingProxy propertyMappingProxy11 = new ClassPropertyMappingProxy();
      propertyMappingProxy11.Name = "DefaultTaxonName";
      propertyMappingProxy11.ResolverType = typeof (TaxonomyDefaultTaxonNameProperty).FullName;
      propertyMappingProxyArray2[3] = (CalculatedPropertyMappingProxy) propertyMappingProxy11;
      ClassPropertyMappingProxy propertyMappingProxy12 = new ClassPropertyMappingProxy();
      propertyMappingProxy12.Name = "TaxonomySharedWith";
      propertyMappingProxy12.ResolverType = typeof (TaxonomySharedWithProperty).FullName;
      propertyMappingProxyArray2[4] = (CalculatedPropertyMappingProxy) propertyMappingProxy12;
      IEnumerable<IPropertyMapping> second = (IEnumerable<IPropertyMapping>) propertyMappingProxyArray2;
      return (ITypeSettings) new TypeSettingsProxy()
      {
        ClrType = typeof (Taxonomy).FullName,
        Enabled = true,
        UrlName = "taxonomies",
        Properties = (IList<IPropertyMapping>) first.Union<IPropertyMapping>(second).ToList<IPropertyMapping>()
      };
    }
  }
}
