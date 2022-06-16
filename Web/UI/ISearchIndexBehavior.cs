// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ISearchIndexBehavior
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This interface provides capabilities for defining widget search behavior.
  /// </summary>
  public interface ISearchIndexBehavior
  {
    /// <summary>
    /// Gets or sets a value indicating whether to exclude the content from search index.
    /// </summary>
    /// <value>
    /// <c>true</c> if should exclude content from search index; otherwise, <c>false</c>.
    /// </value>
    bool ExcludeFromSearchIndex { get; set; }
  }
}
