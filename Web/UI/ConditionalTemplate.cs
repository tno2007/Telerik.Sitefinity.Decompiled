// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ConditionalTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Class that represents a conditional template;</summary>
  public class ConditionalTemplate : Control, ITemplate
  {
    /// <summary>Gets or sets the left part of the comparison.</summary>
    /// <value>The left part of the comparison.</value>
    public string Left { get; set; }

    /// <summary>
    /// Gets or sets the operator of the of conditional template.
    /// </summary>
    /// <value>The operator.</value>
    public ConditionOperators Operator { get; set; }

    /// <summary>Gets or sets the right part of the comparison.</summary>
    /// <value>The right part of the comparison.</value>
    public string Right { get; set; }

    public void InstantiateIn(Control container)
    {
      this.EnsureChildControls();
      container.Controls.Add((Control) this);
    }
  }
}
