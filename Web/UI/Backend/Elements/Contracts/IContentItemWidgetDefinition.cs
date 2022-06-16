// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IContentItemWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Defines the contract implemented by the configuration elements and definition classes for ContentItemWidget
  /// </summary>
  public interface IContentItemWidgetDefinition : IWidgetDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the type of the content item displayed by the content item widget
    /// </summary>
    /// <value>The type of the content item.</value>
    Type ContentItemType { get; set; }

    /// <summary>Gets or sets the service base URL.</summary>
    /// <value>The service base URL.</value>
    string ServiceBaseUrl { get; set; }
  }
}
