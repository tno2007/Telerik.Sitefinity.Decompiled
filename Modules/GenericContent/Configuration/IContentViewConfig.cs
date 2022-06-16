// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.IContentViewConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>
  /// Common interface for content view controls configuration
  /// </summary>
  public interface IContentViewConfig
  {
    /// <summary>Gets a collection of data Content View Controls.</summary>
    /// <value>The content view controls.</value>
    ConfigElementDictionary<string, ContentViewControlElement> ContentViewControls { get; }
  }
}
