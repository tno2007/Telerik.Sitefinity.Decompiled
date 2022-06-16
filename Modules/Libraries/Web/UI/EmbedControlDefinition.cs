// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Contains all properties needed to construct an instance of the EmbedControl control
  /// </summary>
  public class EmbedControlDefinition : 
    FieldControlDefinition,
    IEmbedControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IChoiceFieldDefinition sizesChoiceFieldDefinition;
    private string embedStringTemplate;
    private string customizeButtonTitle;
    private bool? displayEmbedTextBox = new bool?(false);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControlDefinition" /> class.
    /// </summary>
    public EmbedControlDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.EmbedControlDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public EmbedControlDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets the definition for the choice field representig the diffrenet media sizes
    /// </summary>
    /// <value>The sizes choice field definition.</value>
    public IChoiceFieldDefinition SizesChoiceFieldDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (SizesChoiceFieldDefinition), this.sizesChoiceFieldDefinition);
      set => this.sizesChoiceFieldDefinition = value;
    }

    /// <summary>String template used to generate the embed html</summary>
    /// <value></value>
    /// <example>
    /// 	<img width="{0}" height="{1}" src="{2}" alt="{3}" />
    /// </example>
    public string EmbedStringTemplate
    {
      get => this.ResolveProperty<string>(nameof (EmbedStringTemplate), this.embedStringTemplate);
      set => this.embedStringTemplate = value;
    }

    /// <summary>The title of the customize button</summary>
    /// <value></value>
    public string CustomizeButtonTitle
    {
      get => this.ResolveProperty<string>(nameof (CustomizeButtonTitle), this.customizeButtonTitle);
      set => this.customizeButtonTitle = value;
    }

    /// <summary>
    /// If set will hide the text box with the code for embedding in a page
    /// </summary>
    /// <value></value>
    public bool? HideEmbedTextBox
    {
      get => this.ResolveProperty<bool?>(nameof (HideEmbedTextBox), this.displayEmbedTextBox);
      set => this.displayEmbedTextBox = value;
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (EmbedControl);
  }
}
