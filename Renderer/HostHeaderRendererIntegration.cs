// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.HostHeaderRendererIntegration
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Renderer.Diagnostics;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Renderer
{
  internal class HostHeaderRendererIntegration : IRendererIntegration
  {
    public IEnumerable<DiagnosticsDto> Run(RendererData data)
    {
      DiagnosticsDto diagnosticsDto1 = new DiagnosticsDto()
      {
        Name = "HostHeader"
      };
      HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
      if (!string.IsNullOrEmpty(data.HostHeaderName) && !string.Equals(data.HostHeaderName, "Host", StringComparison.OrdinalIgnoreCase))
      {
        string header1 = request.Headers[data.HostHeaderName];
        string header2 = request.Headers["Host"];
        if (!string.Equals(header1, header2, StringComparison.OrdinalIgnoreCase))
        {
          diagnosticsDto1.Message = "The sent host header value(" + data.HostHeaderName + ") - '" + header1 + "' is not rewritten to the value of the Host Header - '" + header2 + "'. Check your url rewriter settings in the web.config file.";
          diagnosticsDto1.HelpLink = "https://www.progress.com/documentation/sitefinity-cms/host-the-asp.net-core-rendered-application#configure-host-headers-when-using-azure-app-services";
        }
      }
      DiagnosticsDto diagnosticsDto2 = new DiagnosticsDto()
      {
        Name = "License"
      };
      Uri uri = request.Url;
      if (!LicenseState.Current.LicenseInfo.AllLicensedDomains.Any<string>((Func<string, bool>) (x => string.Equals(x, uri.Host, StringComparison.OrdinalIgnoreCase))))
      {
        diagnosticsDto2.Message = "The domain '" + uri.Host + "' is not present in the license file. This will cause issues when accessing the Sitefinity administration";
        diagnosticsDto2.HelpLink = "https://www.progress.com/documentation/sitefinity-cms/setup-the-asp.net-core-renderer#overview";
      }
      return (IEnumerable<DiagnosticsDto>) new DiagnosticsDto[2]
      {
        diagnosticsDto1,
        diagnosticsDto2
      };
    }
  }
}
