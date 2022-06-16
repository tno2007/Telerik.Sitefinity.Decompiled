// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Data.HierarchicalTaxonomyDataItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Data
{
  public class HierarchicalTaxonomyDataItem : ITaxon, IDataItem, IOrderedItem, IHasUrlName
  {
    public HierarchicalTaxonomyDataItem(ITaxon source)
    {
      this.Name = source.Name;
      this.UrlName = new Lstring();
      this.UrlName.CopyFrom(source.UrlName);
      this.Title = new Lstring();
      this.Title.CopyFrom(source.Title);
      this.Description = new Lstring();
      this.Description.CopyFrom(source.Description);
      this.ShowInNavigation = source.ShowInNavigation;
      this.RenderAsLink = source.RenderAsLink;
      this.Taxonomy = source.Taxonomy;
      this.Transaction = source.Transaction;
      this.Provider = source.Provider;
      this.LastModified = source.LastModified;
      this.ApplicationName = source.ApplicationName;
      this.Ordinal = source.Ordinal;
      this.Status = source.Status;
    }

    public Guid Id { get; set; }

    public Guid ParentId { get; set; }

    public bool Expanded { get; set; }

    public bool Checked { get; set; }

    public string Name { get; set; }

    public Lstring UrlName { get; set; }

    public Lstring Title { get; set; }

    public Lstring Description { get; set; }

    public bool ShowInNavigation { get; set; }

    public bool RenderAsLink { get; set; }

    public ITaxonomy Taxonomy { get; set; }

    public object Transaction { get; set; }

    public object Provider { get; set; }

    public DateTime LastModified { get; set; }

    public string ApplicationName { get; set; }

    public float Ordinal { get; set; }

    public TaxonStatus Status { get; set; }
  }
}
