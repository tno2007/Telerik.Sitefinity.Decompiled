// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceImagesHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Http handler that uses reads images from embedded resources
  /// </summary>
  public class ResourceImagesHttpHandler : IHttpHandler
  {
    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      HttpResponseBase response = context.Response;
      HttpRequestBase request = context.Request;
      string assemblyName = context.Items[(object) "assemblyName"] as string;
      string str = context.Items[(object) "resourceName"] as string;
      string mimeType;
      Assembly assembly = this.ValidateRequest(context, assemblyName, str, out mimeType);
      DateTime? nullable = new DateTime?();
      string s = request.Headers["If-Modified-Since"];
      if (!string.IsNullOrEmpty(s))
      {
        int length = s.IndexOf(";");
        if (length > 0)
          s = s.Substring(0, length);
        DateTime result = DateTime.MaxValue;
        if (DateTime.TryParse(s, out result))
          result = result.ToUniversalTime();
        ref DateTime? local = ref nullable;
        DateTime assemblyBuildTime = this.GetAssemblyBuildTime(assembly);
        DateTime universalTime = assemblyBuildTime.ToUniversalTime();
        local = new DateTime?(universalTime);
        assemblyBuildTime = nullable.Value;
        if (assemblyBuildTime.AddSeconds(-1.0) <= result)
        {
          response.StatusCode = 304;
          response.StatusDescription = "Not Modified";
          response.End();
          return;
        }
      }
      if (!str.StartsWith(assemblyName, StringComparison.OrdinalIgnoreCase))
        str = assemblyName + "." + str;
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(str))
      {
        DateTime dateTime1;
        if (!nullable.HasValue)
        {
          dateTime1 = this.GetAssemblyBuildTime(assembly);
          nullable = new DateTime?(dateTime1.ToUniversalTime());
        }
        response.AddHeader("content-disposition", "inline; filename={0}".Arrange((object) str));
        response.AddHeader("Content-Length", manifestResourceStream.Length.ToString());
        response.Cache.SetCacheability(HttpCacheability.Public);
        response.Cache.SetLastModified(nullable.Value);
        DateTime date1 = nullable.Value + TimeSpan.FromDays(7.0);
        DateTime dateTime2 = date1;
        dateTime1 = DateTime.Now;
        DateTime universalTime = dateTime1.ToUniversalTime();
        if (dateTime2 <= universalTime)
        {
          HttpCachePolicyBase cache = response.Cache;
          dateTime1 = DateTime.UtcNow;
          DateTime date2 = dateTime1.AddDays(7.0);
          cache.SetExpires(date2);
        }
        else
          response.Cache.SetExpires(date1);
        response.ContentType = mimeType;
        response.StatusCode = 200;
        response.StatusDescription = "OK";
        response.Buffer = false;
        byte[] buffer = new byte[100000];
        while (true)
        {
          int count = manifestResourceStream.Read(buffer, 0, buffer.Length);
          if (count != 0)
            response.OutputStream.Write(buffer, 0, count);
          else
            break;
        }
        context.ApplicationInstance.CompleteRequest();
      }
    }

    /// <summary>Gets the is reusable.</summary>
    /// <value>The is reusable.</value>
    public bool IsReusable => false;

    /// <summary>
    /// Validates the request - checks the assembly name, resournce name, resource extension
    /// </summary>
    protected virtual Assembly ValidateRequest(
      HttpContext context,
      string assemblyName,
      string resourceName,
      out string mimeType)
    {
      return this.ValidateRequest((HttpContextBase) new HttpContextWrapper(context), assemblyName, resourceName, out mimeType);
    }

    protected virtual Assembly ValidateRequest(
      HttpContextBase context,
      string assemblyName,
      string resourceName,
      out string mimeType)
    {
      if (string.IsNullOrEmpty(assemblyName))
        throw new HttpException(404, "Assembly name is not specified.");
      if (string.IsNullOrEmpty(resourceName))
        throw new HttpException(404, "Resource name is not specified.");
      Assembly assembly;
      try
      {
        assembly = Assembly.Load(assemblyName);
      }
      catch (Exception ex)
      {
        throw new HttpException(404, "Failed to load assembly: {0}".Arrange((object) ex.Message));
      }
      if (!resourceName.StartsWith(assemblyName, StringComparison.OrdinalIgnoreCase))
        resourceName = assemblyName + "." + resourceName;
      int startIndex = ((IEnumerable<string>) assembly.GetManifestResourceNames()).Contains<string>(resourceName) ? resourceName.LastIndexOf(".") : throw new HttpException(404, "Unable to find resource with the specified name: {0}".Arrange((object) resourceName));
      mimeType = "image/jpeg";
      string extension = resourceName.Substring(startIndex);
      switch (extension.ToLower())
      {
        case ".bmp":
        case ".gif":
        case ".ico":
        case ".jfif":
        case ".jpe":
        case ".jpeg":
        case ".jpg":
        case ".png":
        case ".tif":
        case ".tiff":
          mimeType = Telerik.Sitefinity.Modules.Libraries.Web.MimeMapping.GetMimeMapping(extension);
          return assembly;
        default:
          throw new HttpException(404, "Unsupported image format: {0}".Arrange((object) extension));
      }
    }

    protected DateTime GetAssemblyBuildTime(Assembly assembly) => new FileInfo(assembly.Location).LastWriteTime;
  }
}
