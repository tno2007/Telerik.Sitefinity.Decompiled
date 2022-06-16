// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.TaxonomyRelationResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class TaxonomyRelationResolver : PropertyRelationResolverBase
  {
    public override IQueryable GetRelatedItems(object item) => (IQueryable) this.GetTaxa(item);

    public override object GetRelatedItem(object item, Guid relatedItemKey)
    {
      TaxonomyManager manager = this.GetManager();
      if (!((ICollection<Guid>) this.Descriptor.GetValue(item)).Contains(relatedItemKey))
        return (object) null;
      return (object) manager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (x => x.Id == relatedItemKey)).SingleOrDefault<Taxon>();
    }

    public override void CreateRelation(
      object item,
      Guid relatedItemKey,
      string relatedItemprovider,
      object persistentItem)
    {
      if (item is IOrganizable organizable)
      {
        organizable.Organizer.AddTaxa(this.Descriptor.Name, relatedItemKey);
      }
      else
      {
        IList<Guid> guidList = (IList<Guid>) this.Descriptor.GetValue(item);
        if (guidList.Contains(relatedItemKey))
          return;
        guidList.Add(relatedItemKey);
      }
    }

    public override void DeleteRelation(object item, Guid relatedItemKey)
    {
      if (item is IOrganizable)
      {
        ((IOrganizable) item).Organizer.RemoveTaxa(this.Descriptor.Name, relatedItemKey);
      }
      else
      {
        IList<Guid> guidList = (IList<Guid>) this.Descriptor.GetValue(item);
        if (!guidList.Contains(relatedItemKey))
          return;
        guidList.Remove(relatedItemKey);
      }
    }

    public override void DeleteAllRelations(object item)
    {
      if (item is IOrganizable)
        ((IOrganizable) item).Organizer.Clear(this.Descriptor.Name);
      else
        ((ICollection<Guid>) this.Descriptor.GetValue(item)).Clear();
    }

    private IQueryable<Taxon> GetTaxa(object item)
    {
      TaxonomyManager manager = this.GetManager();
      IList<Guid> taxGuids = (IList<Guid>) this.Descriptor.GetValue(item);
      return manager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (x => taxGuids.Contains(x.Id)));
    }

    private TaxonomyManager GetManager() => TaxonomyManager.GetManager((this.Descriptor as TaxonomyPropertyDescriptor).TaxonomyProvider);

    public override Type RelatedType
    {
      get
      {
        Type relatedType = typeof (FlatTaxon);
        if ((this.Descriptor as TaxonomyPropertyDescriptor).TaxonomyType == TaxonomyType.Hierarchical)
          relatedType = typeof (HierarchicalTaxon);
        return relatedType;
      }
    }

    public override bool IsMultipleRelation => true;
  }
}
