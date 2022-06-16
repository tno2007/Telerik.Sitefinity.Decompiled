// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FilterRangeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the filter range</summary>
  public class FilterRangeDefinition : DefinitionBase, IFilterRangeDefinition, IDefinition
  {
    private string key;
    private string value;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DialogDefinition" /> class.
    /// </summary>
    public FilterRangeDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DialogDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FilterRangeDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public FilterRangeDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Key
    {
      get => this.ResolveProperty<string>(nameof (Key), this.key);
      set => this.key = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that will cause the dialog defined by this definition
    /// to be opened.
    /// </summary>
    /// <value>Name of the command.</value>
    public string Value
    {
      get => this.ResolveProperty<string>(nameof (Value), this.value);
      set => this.value = value;
    }
  }
}
