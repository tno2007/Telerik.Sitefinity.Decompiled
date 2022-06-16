// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.ILinkDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>Base interface for LinkDefinition</summary>
  public interface ILinkDefinition : IDefinition
  {
    /// <summary>
    /// Gets or sets the URL to link to when the Link item is clicked.
    /// </summary>
    string NavigateUrl { get; set; }

    /// <summary>Gets or sets the command name of the Link item</summary>
    string CommandName { get; set; }

    /// <summary>Gets or sets the name for the Link Item</summary>
    string Name { get; set; }
  }
}
