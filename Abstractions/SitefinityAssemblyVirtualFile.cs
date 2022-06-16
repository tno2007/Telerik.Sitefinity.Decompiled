// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.SitefinityAssemblyVirtualFile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Reflection;
using System.Web.Hosting;

namespace Telerik.Sitefinity.Abstractions
{
  public class SitefinityAssemblyVirtualFile : VirtualFile
  {
    private string normalized;

    public SitefinityAssemblyVirtualFile(string normalized, string original)
      : base(original)
    {
      this.normalized = normalized;
    }

    public override Stream Open() => Assembly.GetAssembly(this.GetType()).GetManifestResourceStream(this.normalized);
  }
}
