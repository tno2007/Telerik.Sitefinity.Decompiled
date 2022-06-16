// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.SitefinityFile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  public static class SitefinityFile
  {
    /// <summary>
    /// Returns the stream from the specified virtual path, if there is physical file on the disk it returns the physical file, otherwise tries to resolve through the virtual path provider
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns></returns>
    public static Stream Open(string virtualPath) => SitefinityFile.Open(virtualPath, true);

    /// <summary>
    /// Returns the stream from the specified virtual path, if there is physical file on the disk it returns the physical file, otherwise tries to resolve through the virtual path provider
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <param name="allowOnlyAppDataFolderAccess">Restrict physical file access only to the App_Data folder.</param>
    /// <returns></returns>
    public static Stream Open(string virtualPath, bool allowOnlyAppDataFolderAccess)
    {
      string path = SystemManager.CurrentHttpContext.Server.MapPath(virtualPath);
      if (File.Exists(path) && (LicenseState.Current.LicenseInfo.WorkflowFeaturesLevel >= 100 || !(virtualPath == WorkflowConfig.anyContentWorkflow) && !(virtualPath == WorkflowConfig.pagesWorkflow) && !(virtualPath == WorkflowConfig.anyMediaContentWorkflow)))
        return !allowOnlyAppDataFolderAccess || path.Contains("App_Data") ? (Stream) File.OpenRead(path) : throw new FileNotFoundException(virtualPath);
      return VirtualPathManager.FileExists(virtualPath) ? VirtualPathManager.OpenFile(virtualPath) : throw new FileNotFoundException(virtualPath);
    }
  }
}
