// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Resolvers.TaxonFieldResolverBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.InlineEditing.Resolvers
{
  internal abstract class TaxonFieldResolverBase : InlineEditingResolverBase, IInlineEditingResolver
  {
    public object GetFieldValue(Guid id, string itemType, string fieldName, string provider)
    {
      ILifecycleDataItem component = (ManagerBase.GetMappedManager(itemType, provider) as ILifecycleManager).GetItem(TypeResolutionService.ResolveType(itemType), id) as ILifecycleDataItem;
      IEnumerable<Guid> taxaIds = TypeDescriptor.GetProperties((object) component)[fieldName].GetValue((object) component) as IEnumerable<Guid>;
      FieldDefinitionElement fieldDefinition = this.GetFieldDefinition(component, fieldName);
      if (fieldDefinition == null)
        return (object) null;
      TaxonFieldDefinitionElement definitionElement = fieldDefinition as TaxonFieldDefinitionElement;
      Guid taxonomyId = definitionElement.TaxonomyId;
      Taxonomy taxonomy = TaxonomyManager.GetManager().GetTaxonomies<Taxonomy>().FirstOrDefault<Taxonomy>((Expression<Func<Taxonomy, bool>>) (x => x.Id == taxonomyId));
      List<TaxonModel> taxonModelList = new List<TaxonModel>();
      foreach (Taxon taxon in taxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (x => taxaIds.Contains<Guid>(x.Id))).OrderBy<Taxon, string>((Func<Taxon, string>) (x => x.Title.ToString())).ToList<Taxon>())
        taxonModelList.Add(new TaxonModel()
        {
          Id = taxon.Id,
          Title = taxon.Title.ToString(),
          AvailableLanguages = taxon.AvailableLanguages
        });
      return (object) new
      {
        TaxonomyId = taxonomy.Id,
        TaxonomyName = taxonomy.Name.LowerFirstLetter(),
        Taxons = taxonModelList,
        AllowMultipleSelection = definitionElement.AllowMultipleSelection
      };
    }

    public abstract string GetFieldType();
  }
}
