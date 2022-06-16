// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.MirrorTextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information needed to construct mirror text field.
  /// </summary>
  public class MirrorTextFieldDefinition : 
    TextFieldDefinition,
    IMirrorTextFieldDefinition,
    ITextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string regularExpressionFilter;
    private string readOnlyReplacement;
    private string replaceWith;
    private string mirroredControlId;
    private bool enableChangeButton;
    private string changeButtonText;
    private bool? toLower;
    private bool? trim;
    private string prefixText;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public MirrorTextFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public MirrorTextFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (MirrorTextField);

    /// <summary>
    /// Gets or sets the regular expression for filtering the value of the mirror text field.
    /// </summary>
    /// <value>
    /// The regular expression for filtering the value of the mirror text field.
    /// </value>
    public string RegularExpressionFilter
    {
      get => this.ResolveProperty<string>(nameof (RegularExpressionFilter), this.regularExpressionFilter);
      set => this.regularExpressionFilter = value;
    }

    /// <summary>
    /// Gets or sets the value that will be replaced for every Regular expression filter match.
    /// </summary>
    /// <value>The value to replace with.</value>
    public string ReplaceWith
    {
      get => this.ResolveProperty<string>(nameof (ReplaceWith), this.replaceWith);
      set => this.replaceWith = value;
    }

    /// <summary>Gets the pageId of the mirrored control pageId.</summary>
    /// <value>The pageId of the mirrored control pageId.</value>
    public string MirroredControlId
    {
      get => this.ResolveProperty<string>(nameof (MirroredControlId), this.mirroredControlId);
      set => this.mirroredControlId = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a button that must be clicked in order to change
    /// the value of the control.
    /// </summary>
    /// <value></value>
    public bool EnableChangeButton
    {
      get => this.ResolveProperty<bool>(nameof (EnableChangeButton), this.enableChangeButton);
      set => this.enableChangeButton = value;
    }

    /// <summary>Gets or sets the text of the change button.</summary>
    /// <value>The text of the change button.</value>
    public string ChangeButtonText
    {
      get => this.ResolveProperty<string>(nameof (ChangeButtonText), this.changeButtonText, Res.Get<Labels>().Change);
      set => this.changeButtonText = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to lower the
    /// the value of the control.
    /// </summary>
    public bool? ToLower
    {
      get => this.ResolveProperty<bool?>(nameof (ToLower), this.toLower);
      set => this.toLower = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to trim the value of this control.
    /// </summary>
    /// <value></value>
    public bool? Trim
    {
      get => this.ResolveProperty<bool?>(nameof (Trim), this.trim);
      set => this.trim = value;
    }

    /// <summary>
    /// Gets or sets the prefix that will be appended to the mirrored value.
    /// </summary>
    /// <value>The text that will be appended to the mirrored text.</value>
    public string PrefixText
    {
      get => this.ResolveProperty<string>(nameof (PrefixText), this.prefixText);
      set => this.prefixText = value;
    }

    /// <summary>Gets or sets the read only replacement.</summary>
    /// <value></value>
    public new string ReadOnlyReplacement
    {
      get => this.ResolveProperty<string>(nameof (ReadOnlyReplacement), this.readOnlyReplacement);
      set => this.readOnlyReplacement = value;
    }
  }
}
