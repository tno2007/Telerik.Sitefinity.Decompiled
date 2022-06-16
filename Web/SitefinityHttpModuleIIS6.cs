// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitefinityHttpModuleIIS6
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents HTTP module specific to IIS 6.x</summary>
  public class SitefinityHttpModuleIIS6 : SitefinityHttpModule
  {
    /// <summary>
    /// Matches the HTTP request to a route, retrieves the handler for that route, and sets the handler as the HTTP handler for the current request.
    /// </summary>
    /// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    public override void PostResolveRequestCache(HttpContextBase context)
    {
      HttpRequestBase request = context.Request;
      string path = request.Path;
      bool flag = false;
      if (path.EndsWith("/sf404.aspx", StringComparison.OrdinalIgnoreCase))
      {
        if (request.QueryString.Count == 0)
          return;
        string[] strArray = request.QueryString[0].Split(';');
        if (strArray.Length > 1)
        {
          string str = strArray[1];
          int num = str.IndexOf("://");
          if (num > 0)
            str = str.Substring(num + 3);
          int startIndex = str.IndexOf('/');
          if (startIndex > 0)
            str = str.Substring(startIndex);
          path = str;
          flag = true;
        }
      }
      else if (path.EndsWith("/default.aspx", StringComparison.OrdinalIgnoreCase))
      {
        int length = path.LastIndexOf('/');
        path = path.Substring(0, length);
        flag = true;
      }
      if (flag)
      {
        if (!path.EndsWith("/"))
          path += "/";
        context.RewritePath(path);
      }
      base.PostResolveRequestCache(context);
    }
  }
}
