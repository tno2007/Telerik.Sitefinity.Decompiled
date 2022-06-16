// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Contracts.TaxonomyDefaultTaxonNameProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Contracts
{
  /// <summary>
  /// A calculated property for resolving <see cref="T:Telerik.Sitefinity.Taxonomies.Model.Taxonomy" /> taxon name in default culture.
  /// </summary>
  internal class TaxonomyDefaultTaxonNameProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (string);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      CultureInfo culture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      return (IDictionary<object, object>) items.Cast<Taxonomy>().ToDictionary<Taxonomy, object, object>((Func<Taxonomy, object>) (i => (object) i), (Func<Taxonomy, object>) (i => (object) i.TaxonName.GetString(culture, true)));
    }

    /// <inheritdoc />
    public override IEnumerable<VocabularyAnnotation> GetAnnotations() => (IEnumerable<VocabularyAnnotation>) new VocabularyAnnotation[1]
    {
      new VocabularyAnnotation("Org.OData.Core.V1", "Immutable", (object) true)
    };
  }
}
