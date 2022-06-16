// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.IGenericContentViewHost
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// Marker interface for hosts of GenericContent ViewMode controls
  /// </summary>
  public interface IGenericContentViewHost
  {
    /// <summary>
    /// Retrieves the type of content that should be edited by generic content-item views
    /// </summary>
    Type ContentItemType { get; }

    /// <summary>
    /// Retrieves the name of the content provider to use for data operations
    /// </summary>
    string ProviderName { get; }
  }
}
