// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewPlugInDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>Base interface for contentview plugins definition.</summary>
  public interface IContentViewPlugInDefinition : IDefinition
  {
    /// <summary>Gets or sets the name.</summary>
    /// <value>The ordinal.</value>
    string Name { get; set; }

    /// <summary>Gets or sets the ordinal.</summary>
    /// <value>The ordinal.</value>
    int? Ordinal { get; set; }

    /// <summary>Gets or sets the place holder pageId.</summary>
    /// <value>The place holder pageId.</value>
    string PlaceHolderId { get; set; }

    /// <summary>Gets or sets the virtual path.</summary>
    /// <value>The virtual path.</value>
    string VirtualPath { get; set; }

    /// <summary>Gets or sets the type of the plug in.</summary>
    /// <value>The type of the plug in.</value>
    Type PlugInType { get; set; }
  }
}
