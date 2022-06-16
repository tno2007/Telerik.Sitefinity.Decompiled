// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.IWcfTaxon
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// Defines to common properties of all WCF taxon representations, regardless of the actual taxon type.
  /// </summary>
  public interface IWcfTaxon
  {
    /// <summary>Gets or sets the pageId of the taxon.</summary>
    Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the taxonomy to which the taxon belongs to.
    /// </summary>
    string TaxonomyName { get; set; }

    /// <summary>
    /// Gets or sets the root taxonomy to which the taxon belongs to in multi-site context.
    /// </summary>
    Guid RootTaxonomyId { get; set; }

    /// <summary>Gets or sets the title of the taxon.</summary>
    string Title { get; set; }

    /// <summary>Gets or sets the name of the taxon.</summary>
    string Name { get; set; }

    /// <summary>Gets or sets the url name of the taxon.</summary>
    string UrlName { get; set; }

    /// <summary>Gets or sets the description of the taxon.</summary>
    string Description { get; set; }

    /// <summary>
    /// Gets or sets the number of items that are marked with this taxon.
    /// </summary>
    uint ItemsCount { get; set; }

    /// <summary>Gets or sets the synonyms for this taxon.</summary>
    string Synonyms { get; set; }

    /// <summary>Gets or sets the path relative to taxonomy</summary>
    string UrlPath { get; set; }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    string[] AvailableLanguages { get; set; }

    /// <summary>Gets or sets the last modified.</summary>
    /// <value>The last modified.</value>
    DateTime LastModified { get; set; }

    /// <summary>
    /// Gets taxon attributes collection (can be used to store custom meta-data with the taxonomy)
    /// </summary>
    DictionaryObjectViewModel Attributes { get; set; }

    /// <summary>Gets or sets the additional stataus.</summary>
    /// <value>The status text.</value>
    Status AdditionalStatus { get; set; }
  }
}
