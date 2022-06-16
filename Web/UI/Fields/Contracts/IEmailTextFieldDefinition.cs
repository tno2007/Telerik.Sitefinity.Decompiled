// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IEmailTextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of text field element.
  /// </summary>
  public interface IEmailTextFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether the text field is nullable.
    /// Empty text field will return null value instead of empty string.
    /// </summary>
    bool AllowNulls { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    bool IsLocalizable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to trim spaces.
    /// </summary>
    bool TrimSpaces { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    bool ToolTipVisible { get; set; }

    /// <summary>Gets or sets the text of the tooltip target.</summary>
    string ToolTipText { get; set; }

    /// <summary>Gets or sets the title of the tooltip.</summary>
    string ToolTipTitle { get; set; }

    /// <summary>Gets or sets the content of the tooltip.</summary>
    string ToolTipContent { get; set; }

    /// <summary>Gets or sets the css class of the tooltip.</summary>
    string ToolTipCssClass { get; set; }

    /// <summary>Gets or sets the css class of the tooltip target.</summary>
    string ToolTipTargetCssClass { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client side component should be initialized in Read mode.
    /// </summary>
    bool ServerSideOnly { get; set; }
  }
}
