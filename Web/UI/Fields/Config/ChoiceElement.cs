// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a single choice item.
  /// </summary>
  public class ChoiceElement : 
    DefinitionConfigElement,
    IChoiceDefinition,
    IDefinition,
    IKeyLessElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ChoiceElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    internal ChoiceElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ChoiceDefinition((ConfigElement) this);

    /// <summary>Gets or sets the text of the choice.</summary>
    /// <value></value>
    [ConfigurationProperty("text")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TextDescription", Title = "TextTitle")]
    public string Text
    {
      get => (string) this["text"];
      set => this["text"] = (object) value;
    }

    /// <summary>Gets or sets the value of the choice.</summary>
    /// <value></value>
    [ConfigurationProperty("value")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ValueDescription", Title = "ValueTitle")]
    public string Value
    {
      get => (string) this["value"];
      set => this[nameof (value)] = (object) value;
    }

    /// <summary>Gets or sets the description of the choice.</summary>
    /// <value></value>
    [ConfigurationProperty("description")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DescriptionDescription", Title = "DescriptionTitle")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Gets a value which indicates whether the choice is enabled. If choice is enabled true; otherwise false.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("enabled", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnabledDescription", Title = "EnabledTitle")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets a value which indicates whether the choice is enabled. If choice is enabled true; otherwise false.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("selected", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SelectedDescription", Title = "SelectedTitle")]
    public bool Selected
    {
      get => (bool) this["selected"];
      set => this["selected"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the element.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Text will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public ConfigElement ConfigDefinition => throw new NotImplementedException();

    string IKeyLessElement.GetHash() => this.Value + "_" + this.Text;

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct FieldProps
    {
      public const string Text = "text";
      public const string Value = "value";
      public const string Description = "description";
      public const string Enabled = "enabled";
      public const string Selected = "selected";
      public const string ResourceClassId = "resourceClassId";
    }
  }
}
