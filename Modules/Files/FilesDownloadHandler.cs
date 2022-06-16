// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Files.FilesDownloadHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Zip;

namespace Telerik.Sitefinity.Modules.Files
{
  /// <summary>Represents a handler for downloading files.</summary>
  public class FilesDownloadHandler : IHttpHandler
  {
    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    /// <exception cref="T:System.Web.HttpException">403;Request denied</exception>
    public void ProcessRequest(HttpContextBase context)
    {
      if (SystemManager.CurrentHttpContext != null)
      {
        if (SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated && SecurityManager.IsBackendUser())
        {
          if (AppPermission.IsGranted(AppAction.ManageFiles))
            goto label_4;
        }
        throw new HttpException(403, "Request denied");
      }
label_4:
      string[] allKeys = context.Request.QueryString.AllKeys;
      string[] paths = new string[allKeys.Length];
      int index = 0;
      foreach (string name in allKeys)
      {
        if (name.StartsWith("path"))
        {
          paths[index] = context.Request.QueryString[name];
          ++index;
        }
      }
      if (paths.Length == 0)
        return;
      string fileName = string.Empty;
      byte[] numArray = (byte[]) null;
      string path = context.Server.MapPath(paths[0]);
      if (paths.Length == 1 && File.Exists(path))
      {
        using (StreamReader streamReader = new StreamReader(path))
        {
          using (BinaryReader binaryReader = new BinaryReader(streamReader.BaseStream))
          {
            numArray = new byte[streamReader.BaseStream.Length];
            binaryReader.Read(numArray, 0, (int) streamReader.BaseStream.Length);
          }
          fileName = Path.GetFileName(path);
        }
      }
      else
        fileName = string.Format("archive-{0}.zip", (object) DateTime.UtcNow.ToString("yyyy-MM-dd-HHmmss"));
      this.WriteFile(numArray, paths, fileName, context);
    }

    /// <summary>Sends a file to the client.</summary>
    /// <param name="content">Binary file content</param>
    /// <param name="paths">The array with the paths of all files.</param>
    /// <param name="fileName">The name of the file to be sent to the client.</param>
    /// <param name="context">The HTTP context.</param>
    private void WriteFile(
      byte[] content,
      string[] paths,
      string fileName,
      HttpContextBase context)
    {
      HttpResponseBase response = context.Response;
      response.Buffer = true;
      response.Clear();
      response.AddHeader("content-disposition", "attachment;filename=\"" + fileName + "\"");
      if (content == null)
      {
        response.ContentType = "application/zip";
        using (ZipFile zipFile = new ZipFile())
        {
          foreach (string path in paths)
          {
            string fileOrDirectoryName = context.Server.MapPath(path);
            zipFile.AddItem(fileOrDirectoryName);
          }
          zipFile.Save(response.OutputStream);
        }
      }
      else if (content.Length != 0)
        response.BinaryWrite(content);
      response.End();
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => true;
  }
}
