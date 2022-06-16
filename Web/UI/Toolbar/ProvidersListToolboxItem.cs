// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ProvidersListToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Representas a toolbox item that will show all available providers
  /// </summary>
  public class ProvidersListToolboxItem : ToolboxItemBase, ICommandButton
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.ProvidersListButton.ascx");
    private GenericContainer container;
    private ITemplate itemTemplate;

    /// <summary>Gets or sets the item template for the toolbox item</summary>
    public override ITemplate ItemTemplate
    {
      get
      {
        if (this.itemTemplate == null)
          this.itemTemplate = ControlUtilities.GetTemplate(ProvidersListToolboxItem.UiPath, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.itemTemplate;
      }
      set => this.itemTemplate = value;
    }

    private GenericContainer Container
    {
      get
      {
        if (this.container == null)
        {
          this.container = new GenericContainer();
          this.ItemTemplate.InstantiateIn((Control) this.container);
        }
        return this.container;
      }
    }

    /// <summary>Gets or sets the name of the command.</summary>
    public string CommandName { get; set; }

    /// <summary>Gets the client pageId of the button control</summary>
    public string ButtonClientId => this.Providers.ClientID;

    /// <summary>
    /// Type name of the manager to whose provers we want to bind
    /// </summary>
    public string ManagerTypeName { get; set; }

    /// <summary>Type name of the item whose</summary>
    public string ItemTypeName { get; set; }

    /// <summary>
    /// Message to display next to the combo box that says something like "Select a provider"
    /// </summary>
    public string SelectProviderMessage { get; set; }

    /// <summary>
    /// Css class to apply to the element encompassing the "Select a provider" message
    /// </summary>
    public string SelectProviderMessageCssClass { get; set; }

    public override string ItemType => nameof (ProvidersListToolboxItem);

    /// <summary>
    /// Generates the command item and returns instantiated control.
    /// </summary>
    /// <returns>Instance of the command button</returns>
    public Control GenerateCommandItem()
    {
      this.Providers.ItemTypeName = this.ItemTypeName;
      this.Providers.ManagerTypeName = this.ManagerTypeName;
      this.Providers.SelectProviderMessage = this.SelectProviderMessage;
      this.Providers.SelectProviderMessageCssClass = this.SelectProviderMessageCssClass;
      return (Control) this.Container;
    }

    /// <summary>
    /// Reference to the providers list control in the toolbox item template
    /// </summary>
    protected virtual ProvidersList Providers => this.Container.GetControl<ProvidersList>("providersList", true);
  }
}
