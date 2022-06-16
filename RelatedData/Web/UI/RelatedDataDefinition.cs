// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.Web.UI.RelatedDataDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.RelatedData.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to display related data items.
  /// </summary>
  public class RelatedDataDefinition
  {
    /// <summary>Gets or sets the parent item identifier.</summary>
    /// <value>The parent item identifier.</value>
    public Guid RelatedItemId { get; set; }

    /// <summary>Gets or sets the type of the parent item.</summary>
    /// <value>The type of the parent item.</value>
    public string RelatedItemType { get; set; }

    /// <summary>Gets or sets the name of the parent item provider.</summary>
    /// <value>The name of the parent item provider.</value>
    public string RelatedItemProviderName { get; set; }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    public string RelatedFieldName { get; set; }

    /// <summary>
    /// Gets or sets the relation type of the items that will be display - children or parent.
    /// </summary>
    /// <value>
    /// The relation type of the items that will be display - children or parent.
    /// </value>
    public RelationDirection RelationTypeToDisplay { get; set; }

    /// <summary>
    /// Gets or sets the sources type for resolving related item
    /// </summary>
    /// <value>The sources type for resolving related item.</value>
    public RelatedItemSource RelatedItemSource { get; set; }
  }
}
