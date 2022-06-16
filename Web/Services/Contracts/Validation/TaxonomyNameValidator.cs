// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Validation.TaxonomyNameValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts.Validation
{
  internal class TaxonomyNameValidator : StringValidator, IItemValidator
  {
    public object Item { get; set; }

    public IManager Manager { get; set; }

    internal override bool Validate(object value, out object errMsg)
    {
      if (!base.Validate(value, out errMsg))
        return false;
      if (!(this.Manager as TaxonomyManager).GetTaxonomies<Taxonomy>().Any<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == value)))
        return true;
      errMsg = (object) "TaxonomyNameDuplicate";
      return false;
    }
  }
}
