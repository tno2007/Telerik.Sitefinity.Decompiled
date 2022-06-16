// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.ICommandWidgetDefinitionSecured
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// An interface the provides the common members for all definitions that define a widget extended with two more properties for access restriction
  /// that can fire a command.
  /// </summary>
  public interface ICommandWidgetDefinitionSecured : 
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets the required permission set for the item to be show.
    /// </summary>
    string RequiredPermissionSet { get; set; }

    /// <summary>
    /// Gets or sets the required actions for the item to be shown.
    /// </summary>
    string RequiredActions { get; set; }
  }
}
