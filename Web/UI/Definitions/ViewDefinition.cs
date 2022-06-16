// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.ViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  internal class ViewDefinition : DefinitionBase, IViewDefinition, IDefinition
  {
    private Type viewType;
    private string viewName;
    private string controlId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Definitions.ViewDefinition" /> class.
    /// </summary>
    public ViewDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ContentViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public virtual string ViewName
    {
      get => this.ResolveProperty<string>(nameof (ViewName), this.viewName);
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    public virtual Type ViewType
    {
      get => this.ResolveProperty<Type>(nameof (ViewType), this.viewType);
      set => this.viewType = value;
    }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    public virtual string ControlId
    {
      get => this.ResolveProperty<string>(nameof (ControlId), this.controlId);
      set => this.controlId = value;
    }
  }
}
