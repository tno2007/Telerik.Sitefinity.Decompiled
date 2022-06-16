// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.HierarchicalTaxonHasChildrenProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property that determine if a page has children</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class HierarchicalTaxonHasChildrenProperty : HasChildrenProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (bool);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      IEnumerable<Guid> guidIds = items.Cast<HierarchicalTaxon>().Select<HierarchicalTaxon, Guid>((Func<HierarchicalTaxon, Guid>) (x => x.Id));
      return (IDictionary<object, object>) ((TaxonomyManager) manager).GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (x => guidIds.Contains<Guid>(x.Id))).Select(taxon => new
      {
        Taxon = taxon,
        HasChildren = taxon.Subtaxa.Count > 0
      }).ToDictionary(x => (object) x.Taxon, y => (object) y.HasChildren);
    }
  }
}
