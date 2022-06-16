// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information that is needed to construct a single choice item.
  /// </summary>
  [DataContract]
  public class ChoiceDefinition : DefinitionBase, IChoiceDefinition, IDefinition
  {
    private string text;
    private string value;
    private string description;
    private bool enabled;
    private bool selected;
    private string resourceClassId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceDefinition" /> class.
    /// </summary>
    public ChoiceDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ChoiceDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ChoiceDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the text of the choice.</summary>
    /// <value></value>
    [DataMember]
    public string Text
    {
      get => this.ResolveProperty<string>(nameof (Text), this.text);
      set => this.text = value;
    }

    /// <summary>Gets or sets the value of the choice.</summary>
    /// <value></value>
    [DataMember]
    public string Value
    {
      get => this.ResolveProperty<string>(nameof (Value), this.value);
      set => this.value = value;
    }

    /// <summary>Gets or sets the description of the choice.</summary>
    /// <value></value>
    [DataMember]
    public string Description
    {
      get => this.ResolveProperty<string>(nameof (Description), this.description);
      set => this.description = value;
    }

    /// <summary>
    /// Gets a value which indicates whether the choice is enabled. If choice is enabled true; otherwise false.
    /// </summary>
    /// <value></value>
    [DataMember]
    public bool Enabled
    {
      get => this.ResolveProperty<bool>(nameof (Enabled), this.enabled);
      set => this.enabled = value;
    }

    /// <summary>
    /// Gets a value which indicates whether the choice is enabled. If choice is enabled true; otherwise false.
    /// </summary>
    /// <value></value>
    [DataMember]
    public bool Selected
    {
      get => this.ResolveProperty<bool>(nameof (Selected), this.selected);
      set => this.selected = value;
    }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Text will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    public virtual string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }
  }
}
