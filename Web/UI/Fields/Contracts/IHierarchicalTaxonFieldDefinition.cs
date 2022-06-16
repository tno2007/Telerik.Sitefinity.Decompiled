// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IHierarchicalTaxonFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of hierarchical taxonfield.
  /// </summary>
  public interface IHierarchicalTaxonFieldDefinition : 
    ITaxonFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    IExpandableControlDefinition ExpandableDefinition { get; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow root selection.
    /// </summary>
    /// <value><c>true</c> if to allow root selection; otherwise, <c>false</c>.</value>
    bool? AllowRootSelection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done-selecting button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the done-selecting button; otherwise, <c>false</c>.
    /// </value>
    bool? ShowDoneSelectingButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the create-new-taxon button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the create-new-taxon button; otherwise, <c>false</c>.
    /// </value>
    bool? ShowCreateNewTaxonButton { get; set; }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    Guid RootTaxonID { get; set; }
  }
}
