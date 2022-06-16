// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.GridColumnWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data
{
  /// <summary>
  /// This class is used by Sitefinity user interface to describe which columns
  /// ought to be included in the grid and in which order.
  /// </summary>
  [DataContract]
  internal class GridColumnWrapper
  {
    /// <summary>
    /// Gets or sets the name of the field that should be displayed in
    /// backend grid.
    /// </summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the position of the column in the grid.</summary>
    [DataMember]
    [Obsolete("Columns are ordered manually based on the UX requirements. This property is not applicable anymore.")]
    public int ColumnOrdinal { get; set; }

    /// <summary>Gets the unique identity of the data item.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title of the field.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the item should be displayed in the
    /// backend grid.
    /// </summary>
    [DataMember]
    public bool ShowInGrid { get; set; }

    /// <summary>
    /// Gets or sets the css class for the field used in the GridEditor.
    /// </summary>
    [DataMember]
    public string GridEditorCssClass { get; set; }

    /// <summary>
    /// Gets or sets the value indicating weather the column is a grouping one.
    /// </summary>
    [DataMember]
    public bool IsGrouping { get; set; }

    [DataMember]
    public string BoundPropertyName { get; set; }

    [DataMember]
    public IDictionary<string, object> FieldMetadata { get; set; }
  }
}
