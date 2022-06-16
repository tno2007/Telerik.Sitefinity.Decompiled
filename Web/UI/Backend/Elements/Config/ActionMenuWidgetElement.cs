// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ActionMenuWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents the configuration element of action menu widget.
  /// </summary>
  public class ActionMenuWidgetElement : 
    WidgetElement,
    IActionMenuWidgetDefinition,
    IWidgetDefinition,
    IDefinition,
    IActionMenuDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ActionMenuWidgetElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ActionMenuWidgetDefinition((ConfigElement) this);

    /// <summary>Gets or sets the main action configuration element.</summary>
    /// <value>The main action configuration element.</value>
    [ConfigurationProperty("mainAction")]
    public CommandWidgetElement MainAction
    {
      get => (CommandWidgetElement) this["mainAction"];
      set => this["mainAction"] = (object) value;
    }

    /// <summary>
    /// Defines a dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration elements.
    /// </summary>
    /// <value>The dictionary of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> configuration element.</value>
    [ConfigurationProperty("menuItems")]
    [ConfigurationCollection(typeof (WidgetElement), AddItemName = "item")]
    public ConfigElementList<WidgetElement> MenuItems => (ConfigElementList<WidgetElement>) this["menuItems"];

    /// <summary>Gets or sets the main action.</summary>
    /// <value>The main action.</value>
    ICommandWidgetDefinition IActionMenuDefinition.MainAction
    {
      get => (ICommandWidgetDefinition) this.MainAction;
      set => this.MainAction = (CommandWidgetElement) value;
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetDefinition" /> objects.
    /// </summary>
    /// <value>The menu items.</value>
    IEnumerable<IWidgetDefinition> IActionMenuDefinition.MenuItems => this.MenuItems.Cast<IWidgetDefinition>();
  }
}
