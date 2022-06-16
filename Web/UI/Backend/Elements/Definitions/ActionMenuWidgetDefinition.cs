// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ActionMenuWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// A class that provides all information needed to construct action menu widget.
  /// </summary>
  public class ActionMenuWidgetDefinition : 
    WidgetDefinition,
    IActionMenuWidgetDefinition,
    IWidgetDefinition,
    IDefinition,
    IActionMenuDefinition
  {
    private ICommandWidgetDefinition mainAction;
    private List<IWidgetDefinition> menuItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ActionMenuWidgetDefinition" /> class.
    /// </summary>
    public ActionMenuWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ActionMenuWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ActionMenuWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ActionMenuWidgetDefinition GetDefinition() => this;

    /// <summary>Gets or sets the main action.</summary>
    /// <value>The main action.</value>
    public ICommandWidgetDefinition MainAction
    {
      get => this.ResolveProperty<ICommandWidgetDefinition>(nameof (MainAction), this.mainAction);
      set => this.mainAction = value;
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition" /> objects.
    /// </summary>
    /// <value>The menu items.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IWidgetDefinition> MenuItems
    {
      get
      {
        if (this.menuItems == null)
          this.menuItems = new List<IWidgetDefinition>();
        return this.ResolveProperty<List<IWidgetDefinition>>(nameof (MenuItems), this.menuItems);
      }
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition" /> objects.
    /// </summary>
    /// <value>The menu items.</value>
    IEnumerable<IWidgetDefinition> IActionMenuDefinition.MenuItems => (IEnumerable<IWidgetDefinition>) this.MenuItems;
  }
}
