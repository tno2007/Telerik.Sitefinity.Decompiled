// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IItemInfoDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Base interface for ItemInfoDefinition. This definition is used to describe a general content element by ProviderName and ItemId.
  /// </summary>
  public interface IItemInfoDefinition : IDefinition
  {
    /// <summary>
    /// Gets or sets the name of the provider to retrieve the content element.
    /// </summary>
    string ProviderName { get; set; }

    /// <summary>Gets or sets the ID of the content element.</summary>
    Guid ItemId { get; set; }

    /// <summary>Gets or sets the title of the content element.</summary>
    string Title { get; set; }
  }
}
