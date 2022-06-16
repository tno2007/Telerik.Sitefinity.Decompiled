// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Contracts.IHierarchicalTaxonParentSelectorFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Contracts
{
  /// <summary>
  /// Stores information used by <see cref="!:Telerik.Sitefinity.Web.UI.HierarchicalTaxonParentSelectorField" />
  /// </summary>
  internal interface IHierarchicalTaxonParentSelectorFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the taxonomy id.</summary>
    /// <value>The taxonomy pageId.</value>
    Guid TaxonomyId { get; set; }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    string Provider { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    string WebServiceBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind on the server.
    /// </summary>
    bool BindOnServer { get; set; }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    IExpandableControlDefinition ExpandableDefinition { get; }
  }
}
