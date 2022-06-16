// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.ViewContainerDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  internal class ViewContainerDefinition : DefinitionBase, IViewContainerDefinition, IDefinition
  {
    private string controlDefinitionName;
    private ViewDefinitionDictionary views;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Definitions.ViewContainerDefinition" /> class.
    /// </summary>
    public ViewContainerDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Definitions.ViewContainerDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ViewContainerDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the name of the definition.</summary>
    /// <value>The name of the definition.</value>
    public virtual string ControlDefinitionName
    {
      get => this.ResolveProperty<string>(nameof (ControlDefinitionName), this.controlDefinitionName);
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    public virtual ViewDefinitionDictionary Views
    {
      get
      {
        if (this.views == null)
        {
          ConfigElementDictionary<string, ViewDefinitionElement> viewsConfigPrivate = this.GetViewsConfig_private();
          this.views = viewsConfigPrivate == null ? new ViewDefinitionDictionary() : new ViewDefinitionDictionary(viewsConfigPrivate.Elements.Select<ViewDefinitionElement, IViewDefinition>((Func<ViewDefinitionElement, IViewDefinition>) (i => (IViewDefinition) i.GetDefinition())));
        }
        return this.views;
      }
    }

    /// <summary>Tries the get view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public bool TryGetView(string viewName, out IViewDefinition view)
    {
      if (this.Views != null)
      {
        if (this.Views.Contains(viewName))
        {
          view = this.Views[viewName];
          return true;
        }
      }
      else
      {
        IViewDefinition view1;
        if (((ViewContainerElement) this.ConfigDefinition).TryGetView(viewName, out view1))
        {
          view = (IViewDefinition) view1.GetDefinition();
          return true;
        }
      }
      view = (IViewDefinition) null;
      return false;
    }

    /// <summary>
    /// Determines whether the views collection contains view with the specified name.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns>
    /// 	<c>true</c> if the view exists; otherwise, <c>false</c>.
    /// </returns>
    public bool ContainsView(string viewName) => this.views != null ? this.views.Contains(viewName) : ((ViewContainerElement) this.ConfigDefinition).ContainsView(viewName);

    protected internal virtual ConfigElementDictionary<string, ViewDefinitionElement> GetViewsConfig_private() => this.ConfigDefinition != null ? ((ViewContainerElement) this.ConfigDefinition).ViewsConfig : (ConfigElementDictionary<string, ViewDefinitionElement>) null;
  }
}
