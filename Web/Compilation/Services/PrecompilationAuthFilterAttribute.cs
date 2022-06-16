// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Compilation.Services.PrecompilationAuthFilterAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Web;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.ServiceStack.Filters;

namespace Telerik.Sitefinity.Web.Compilation.Services
{
  [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
  internal class PrecompilationAuthFilterAttribute : RequestBackendAuthenticationFilterAttribute
  {
    private const string KEY_AUTH_HEADER = "SF_PRECOMPILE_AUTH";

    public override void Execute(IRequest req, IResponse res, object requestDto)
    {
      string authKey = Config.Get<PagesConfig>().Compilation.AuthKey;
      string header = req.Headers["SF_PRECOMPILE_AUTH"];
      if (string.IsNullOrEmpty(header) || string.IsNullOrEmpty(authKey) || !string.Equals(header, authKey))
        base.Execute(req, res, requestDto);
      else
        SecurityManager.AuthenticateSystemRequest(SystemManager.CurrentHttpContext);
    }

    public PrecompilationAuthFilterAttribute()
      : base()
    {
    }
  }
}
