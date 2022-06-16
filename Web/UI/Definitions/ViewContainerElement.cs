// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.ViewContainerElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ViewContainerDescription", Title = "ViewContainerTitle")]
  [RestartAppOnChange]
  internal class ViewContainerElement : 
    DefinitionConfigElement,
    IViewContainerDefinition,
    IDefinition
  {
    private ViewDefinitionDictionary views;

    public ViewContainerElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ViewContainerDefinition((ConfigElement) this);

    /// <summary>Gets or sets the name of the definition.</summary>
    /// <value>The name of the definition.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ViewContainerDefinitionNameDescription", Title = "ViewContainerDefinitionName")]
    [ConfigurationProperty("definitionName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    public string ControlDefinitionName
    {
      get => (string) this["definitionName"];
      set => this["definitionName"] = (object) value;
    }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    public virtual ViewDefinitionDictionary Views
    {
      get
      {
        if (this.views == null)
          this.views = new ViewDefinitionDictionary((IEnumerable<IViewDefinition>) this.ViewsConfig.Elements.Select<ViewDefinitionElement, ViewDefinition>((Func<ViewDefinitionElement, ViewDefinition>) (i => (ViewDefinition) i.GetDefinition())));
        return this.views;
      }
    }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    [ConfigurationProperty("views")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ViewContainerViewsDescription", Title = "ViewContainerViews")]
    [ConfigurationCollection(typeof (ViewDefinitionElement), AddItemName = "view")]
    [TypeConverter(typeof (GenericCollectionConverter))]
    public ConfigElementDictionary<string, ViewDefinitionElement> ViewsConfig => (ConfigElementDictionary<string, ViewDefinitionElement>) this["views"];

    /// <summary>Tries the get view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    public bool TryGetView(string viewName, out IViewDefinition view)
    {
      ViewDefinitionElement definitionElement;
      if (this.ViewsConfig.TryGetValue(viewName, out definitionElement))
      {
        view = (IViewDefinition) definitionElement;
        return true;
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
    public bool ContainsView(string viewName) => this.ViewsConfig.Keys.Contains(viewName);
  }
}
