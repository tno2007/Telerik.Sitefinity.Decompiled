// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IProvidersListWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Declares the contract for ProviderListWidgetDefinition and respective configuration element
  /// </summary>
  public interface IProvidersListWidgetDefinition : 
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the type of the data item.</summary>
    /// <value>The type of the data item.</value>
    string DataItemType { get; set; }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// <value>The type of the manager.</value>
    string ManagerType { get; set; }

    /// <summary>Gets or sets the select provider message.</summary>
    /// <value>The select provider message.</value>
    string SelectProviderMessage { get; set; }

    /// <summary>Gets or sets the select provider message CSS class.</summary>
    /// <value>The select provider message CSS class.</value>
    string SelectProviderMessageCssClass { get; set; }
  }
}
