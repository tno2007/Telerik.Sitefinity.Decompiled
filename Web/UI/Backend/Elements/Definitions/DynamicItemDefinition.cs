// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DynamicItemDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  public class DynamicItemDefinition : DefinitionBase, IDynamicItemDefinition, IDefinition
  {
    private string title;
    private string value;
    private string resourceClassId;
    private NameValueCollection parameters;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public DynamicItemDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DynamicItemDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DynamicItemDefinition GetDefinition() => this;

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The value.</value>
    public string Value
    {
      get => this.ResolveProperty<string>(nameof (Value), this.value);
      set => this.value = value;
    }

    /// <summary>
    /// Gets or sets the resource class pageId for styling the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Gets or sets the parameters.</summary>
    /// <value>The parameters.</value>
    public NameValueCollection Parameters
    {
      get
      {
        if (this.parameters == null)
          this.parameters = new NameValueCollection();
        return this.parameters;
      }
    }
  }
}
