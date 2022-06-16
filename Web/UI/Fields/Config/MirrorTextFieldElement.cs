// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.MirrorTextFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for mirror text fields.</summary>
  public class MirrorTextFieldElement : 
    TextFieldDefinitionElement,
    IMirrorTextFieldDefinition,
    ITextFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public MirrorTextFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new MirrorTextFieldDefinition((ConfigElement) this);

    /// <summary>Called when properties are initialized.</summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.propertyResolver = (PropertyResolverBase) new MirrorTextFieldPropertyResolver();
    }

    /// <summary>
    /// Gets or sets the expression for filtering the value of the mirror text field.
    /// </summary>
    /// <value>The filter expression of the mirror text field.</value>
    [ConfigurationProperty("regularExpressionFilter")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RegularExpressionFilterDescription", Title = "RegularExpressionFilterTitle")]
    public string RegularExpressionFilter
    {
      get => (string) this["regularExpressionFilter"];
      set => this["regularExpressionFilter"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value that will be replaced for every Regular expression filter match.
    /// </summary>
    /// <value>The value to replace with.</value>
    [ConfigurationProperty("replaceWith")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ReplaceWithDescription", Title = "ReplaceWithTitle")]
    public string ReplaceWith
    {
      get => (string) this["replaceWith"];
      set => this["replaceWith"] = (object) value;
    }

    /// <summary>Gets the pageId of the mirrored control.</summary>
    /// <value>The pageId of the mirrored control.</value>
    [ConfigurationProperty("mirroredControlId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MirroredControlIdDescription", Title = "MirroredControlIdTitle")]
    public string MirroredControlId
    {
      get => (string) this["mirroredControlId"];
      set => this["mirroredControlId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a button that must be clicked in order to change
    /// the value of the control.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("enableChangeButton", DefaultValue = true, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableChangeButtonDescription", Title = "EnableChangeButtonTitle")]
    public bool EnableChangeButton
    {
      get => (bool) this["enableChangeButton"];
      set => this["enableChangeButton"] = (object) value;
    }

    /// <summary>Gets or sets the text of the change button.</summary>
    /// <value>The text of the change button.</value>
    [ConfigurationProperty("changeButtonText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChangeButtonTextDescription", Title = "ChangeButtonTextTitle")]
    public string ChangeButtonText
    {
      get => (string) this["changeButtonText"];
      set => this["changeButtonText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to lower the
    /// the value of the control.
    /// </summary>
    [ConfigurationProperty("toLower", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToLowerDescription", Title = "ToLowerTitle")]
    public bool? ToLower
    {
      get => (bool?) this["toLower"];
      set => this["toLower"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to trim the value of this control.
    /// </summary>
    [ConfigurationProperty("trim", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrimDescription", Title = "TrimTitle")]
    public bool? Trim
    {
      get => (bool?) this["trim"];
      set => this["trim"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the prefix that will be appended to the mirrored value.
    /// </summary>
    /// <value>The text that will be appended to the mirrored text.</value>
    [ConfigurationProperty("prefixText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PrefixTextDescription", Title = "PrefixTextTitle")]
    public string PrefixText
    {
      get => (string) this["prefixText"];
      set => this["prefixText"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (MirrorTextField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct PropertyNames
    {
      public const string RegularExpressionFilter = "regularExpressionFilter";
      public const string ReplaceWith = "replaceWith";
      public const string MirroredControlId = "mirroredControlId";
      public const string EnableChangeButton = "enableChangeButton";
      public const string ChangeButtonText = "changeButtonText";
      public const string ToLower = "toLower";
      public const string Trim = "trim";
      public const string PrefixText = "prefixText";
    }
  }
}
