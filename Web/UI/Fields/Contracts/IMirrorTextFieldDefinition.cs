// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IMirrorTextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface the provides the common members for the definition of mirror text field element.
  /// </summary>
  public interface IMirrorTextFieldDefinition : 
    ITextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets the regular expression for filtering the value of the mirror text field.
    /// </summary>
    /// <value>The regular expression for filtering the value of the mirror text field.</value>
    string RegularExpressionFilter { get; set; }

    /// <summary>
    /// Gets or sets the value that will be replaced for every Regular expression filter match.
    /// </summary>
    /// <value>The value to replace with.</value>
    string ReplaceWith { get; set; }

    /// <summary>Gets the pageId of the mirrored control pageId.</summary>
    /// <value>The pageId of the mirrored control pageId.</value>
    string MirroredControlId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show a button that must be clicked in order to change
    /// the value of the control.
    /// </summary>
    bool EnableChangeButton { get; set; }

    /// <summary>Gets or sets the text of the change button.</summary>
    /// <value>The text of the change button.</value>
    string ChangeButtonText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to lower the
    /// the value of the control.
    /// </summary>
    bool? ToLower { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to trim the value of this control.
    /// </summary>
    bool? Trim { get; set; }

    /// <summary>
    /// Gets or sets the prefix that will be appended to the mirrored value.
    /// </summary>
    /// <value>The text that will be appended to the mirrored text.</value>
    string PrefixText { get; set; }
  }
}
