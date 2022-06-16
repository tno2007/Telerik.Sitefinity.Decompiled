﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.EmbedControlDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Providers the configuration element for EmbedControl control
  /// </summary>
  public class EmbedControlDefinitionElement : 
    FieldControlDefinitionElement,
    IEmbedControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.EmbedControlDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public EmbedControlDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new EmbedControlDefinition((ConfigElement) this);

    /// <summary>
    /// Gets the definition for the choice field representig the diffrenet media sizes
    /// </summary>
    /// <value>The sizes choice field definition.</value>
    [ConfigurationProperty("sizesChoiceFieldDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChoiceFieldElementSizesChoiceFieldDefinitionDescription", Title = "ChoiceFieldElementSizesChoiceFieldDefinitionCaption")]
    public ChoiceFieldElement SizesChoiceFieldDefinition
    {
      get => (ChoiceFieldElement) this["sizesChoiceFieldDefinition"];
      set => this["sizesChoiceFieldDefinition"] = (object) value;
    }

    /// <summary>String template used to generate the embed html</summary>
    /// <value></value>
    /// <example>
    /// 	<img width="{0}" height="{1}" src="{2}" alt="{3}" />
    /// </example>
    [ConfigurationProperty("embedStringTemplate")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EmbedStringTemplateDescription", Title = "EmbedStringTemplateCaption")]
    public string EmbedStringTemplate
    {
      get => (string) this["embedStringTemplate"];
      set => this["embedStringTemplate"] = (object) value;
    }

    /// <summary>The title of the customize button</summary>
    /// <value></value>
    [ConfigurationProperty("customizeButtonTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CustomizeButtonTitleDescription", Title = "CustomizeButtonTitleCaption")]
    public string CustomizeButtonTitle
    {
      get => (string) this["customizeButtonTitle"];
      set => this["customizeButtonTitle"] = (object) value;
    }

    /// <summary>
    /// If set will hide the text box with the code for embedding in a page
    /// </summary>
    [ConfigurationProperty("hideEmbedTextBox", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HideEmbedTextBoxDescription", Title = "HideEmbedTextBoxCaption")]
    public bool? HideEmbedTextBox
    {
      get => (bool?) this["hideEmbedTextBox"];
      set => this["hideEmbedTextBox"] = (object) value;
    }

    /// <summary>
    /// Gets the definition for the choice field representig the diffrenet media sizes
    /// </summary>
    /// <value>The sizes choice field definition.</value>
    IChoiceFieldDefinition IEmbedControlDefinition.SizesChoiceFieldDefinition => (IChoiceFieldDefinition) this.SizesChoiceFieldDefinition;

    /// <summary>String template used to generate the embed html</summary>
    /// <value></value>
    /// <example>
    /// 	<img width="{0}" height="{1}" src="{2}" alt="{3}" />
    /// </example>
    string IEmbedControlDefinition.EmbedStringTemplate => this.EmbedStringTemplate;

    /// <summary>The title of the customize button</summary>
    /// <value></value>
    string IEmbedControlDefinition.CustomizeButtonTitle => this.CustomizeButtonTitle;

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (EmbedControl);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string SizesChoiceFieldDefinition = "sizesChoiceFieldDefinition";
      public const string EmbedStringTemplate = "embedStringTemplate";
      public const string CustomizeButtonTitle = "customizeButtonTitle";
      public const string HideEmbedTextBox = "hideEmbedTextBox";
    }
  }
}
