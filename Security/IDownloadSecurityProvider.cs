// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.IDownloadSecurityProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// This is the interface to be implemented by custom security providers
  /// that protect downloads of files from libraries.
  /// </summary>
  public interface IDownloadSecurityProvider
  {
    /// <summary>
    /// Returns true if item can be downloaded; otherwise false.
    /// </summary>
    /// <param name="downloadItem">The item that is to be downloaded.</param>
    /// <returns>True if item can be downloaded; otherwise false.</returns>
    bool IsAllowed(MediaContent downloadItem);
  }
}
