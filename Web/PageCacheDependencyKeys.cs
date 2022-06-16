// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageCacheDependencyKeys
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.InteropServices;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// The struct contains keys to retrieve different cache dependency strategies.
  /// </summary>
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct PageCacheDependencyKeys
  {
    /// <summary>
    /// The key to retrieve page cache dependency that will invalidate cached page with specified page id.
    /// </summary>
    public const string PageData = "PageDataCacheDependencyName";
    /// <summary>
    /// The key to retrieve cache dependency that will invalidate all cached instances of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />.
    /// </summary>
    public const string PageNodes = "PageNodesCacheDependencyName";
    /// <summary>
    /// The key to retrieve page cache dependency that will invalidate cached pages when a content type is changed
    /// e.g publish news should invalidate page that contain archive control with list news.
    /// </summary>
    public const string DataItemType = "DataItemTypeCacheDependencyName";
  }
}
