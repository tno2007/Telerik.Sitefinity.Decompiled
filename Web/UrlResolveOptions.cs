// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlResolveOptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Specifies how an URL should be resolved.</summary>
  [Flags]
  public enum UrlResolveOptions
  {
    /// <summary>No resolution.</summary>
    None = 0,
    /// <summary>To an application-relative path starting with "~/".</summary>
    ApplicationRelative = 1,
    /// <summary>Relative to the current request.</summary>
    CurrentRequestRelative = 2,
    /// <summary>
    /// Starts with the appliaiton root: "/MyApplicaton/VirtualPath"
    /// </summary>
    Rooted = 4,
    /// <summary>To absolute URL: "http://MyDomain.com/VirtualPath"</summary>
    Absolute = 8,
    /// <summary>
    /// Appends the literal slash mark (/) to the end of the virtual path, if one does not already exist.
    /// </summary>
    AppendTrailingSlash = 16, // 0x00000010
    /// <summary>
    /// Removes a trailing slash mark (/) from a virtual path.
    /// </summary>
    RemoveTrailingSlash = 32, // 0x00000020
  }
}
