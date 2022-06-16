// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ProvidersListWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>The definition for ProvidersListWidget</summary>
  public class ProvidersListWidgetDefinition : 
    CommandWidgetDefinition,
    IProvidersListWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private string dataItemType;
    private string managerType;
    private string selectProviderMessage;
    private string selectProviderMessageCssClass;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public ProvidersListWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ProvidersListWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ProvidersListWidgetDefinition GetDefinition() => this;

    /// <summary>Gets or sets the type of the data item.</summary>
    /// <value>The type of the data item.</value>
    public string DataItemType
    {
      get => this.ResolveProperty<string>(nameof (DataItemType), this.dataItemType);
      set => this.dataItemType = value;
    }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// <value>The type of the manager.</value>
    public string ManagerType
    {
      get => this.ResolveProperty<string>(nameof (ManagerType), this.managerType);
      set => this.managerType = value;
    }

    /// <summary>Gets or sets the select provider message.</summary>
    /// <value>The select provider message.</value>
    public string SelectProviderMessage
    {
      get => this.ResolveProperty<string>(nameof (SelectProviderMessage), this.selectProviderMessage);
      set => this.selectProviderMessage = value;
    }

    /// <summary>Gets or sets the select provider message CSS class.</summary>
    /// <value>The select provider message CSS class.</value>
    public string SelectProviderMessageCssClass
    {
      get => this.ResolveProperty<string>(nameof (SelectProviderMessageCssClass), this.selectProviderMessageCssClass);
      set => this.selectProviderMessageCssClass = value;
    }
  }
}
