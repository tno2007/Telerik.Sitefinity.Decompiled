// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ChoiceItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a single choice in the choice field control. This could be a checkbox, radiobutton or list item
  /// This class holds all the information needed for a single choice
  /// </summary>
  public class ChoiceItem
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ChoiceItem" /> class.
    /// </summary>
    public ChoiceItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ChoiceItem" /> class.
    /// </summary>
    /// <param name="definition">The definition.</param>
    public ChoiceItem(IChoiceDefinition definition) => this.Configure(definition);

    /// <summary>
    /// Configures the specified choice item with the given definition
    /// </summary>
    /// <param name="definition">The definition.</param>
    public void Configure(IChoiceDefinition definition)
    {
      this.Text = Res.ResolveLocalizedValue(definition.ResourceClassId, definition.Text);
      this.Value = definition.Value;
      this.Description = Res.ResolveLocalizedValue(definition.ResourceClassId, definition.Description);
      this.Enabled = definition.Enabled;
      this.Selected = definition.Selected;
    }

    /// <summary>Gets or sets the text of the choice.</summary>
    /// <value></value>
    public string Text { get; set; }

    /// <summary>Gets or sets the value of the choice.</summary>
    /// <value></value>
    public string Value { get; set; }

    /// <summary>Gets or sets the description of the choice.</summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Gets a value which indicates whether the choice is enabled. If choice is enabled true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool Enabled { get; set; }

    /// <summary>Indicates if the choice item is selected/checked</summary>
    public bool Selected { get; set; }
  }
}
