// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.SitefinityVirtualFile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Web.Hosting;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Represents a file object in a virtual file or resource space.
  /// </summary>
  public class SitefinityVirtualFile : VirtualFile
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.SitefinityVirtualFile" /> class.
    /// </summary>
    /// <param name="virtualPaht">The virtual paht.</param>
    public SitefinityVirtualFile(string virtualPaht)
      : base(virtualPaht)
    {
    }

    /// <summary>
    /// When overridden in a derived class, returns a read-only stream to the virtual resource.
    /// </summary>
    /// <returns>A read-only stream to the virtual file.</returns>
    public override Stream Open() => VirtualPathManager.OpenFile(this.VirtualPath);
  }
}
