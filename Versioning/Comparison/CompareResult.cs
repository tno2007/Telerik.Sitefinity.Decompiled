// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.CompareResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>The class representing the</summary>
  public class CompareResult
  {
    /// <summary>The name of the compared property</summary>
    public string PropertyName { get; set; }

    /// <summary>The old value of the property</summary>
    public string OldValue { get; set; }

    /// <summary>The new value of the property</summary>
    public string NewValue { get; set; }

    /// <summary>
    /// The difference between two objects represented as HTML
    /// </summary>
    public string DiffHtml { get; set; }

    /// <summary>
    /// If True the two compare values are different. Otherwise - False
    /// </summary>
    public bool AreDifferent { get; set; }

    internal bool IsHtmlEnchancedField { get; set; }

    /// <summary>Gets or sets the type of the compare.</summary>
    /// <value>The type of the compare.</value>
    public string CompareType { get; set; }

    /// <summary>Gets or sets the old value related properties.</summary>
    /// <value>The old value related properties.</value>
    public Dictionary<string, object> OldValueRelatedProperties { get; set; }

    /// <summary>Gets or sets the new value related properties.</summary>
    /// <value>The new value related properties.</value>
    public Dictionary<string, object> NewValueRelatedProperties { get; set; }
  }
}
