// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.Extensions.IOwinContextExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Telerik.Sitefinity.Owin.Extensions
{
  /// <summary>Owin extensions for commonly used methods</summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public static class IOwinContextExtensions
  {
    /// <summary>Gets the HttpContextBase from the OWIN Context.</summary>
    /// <param name="context">The OWIN context.</param>
    /// <returns>An instance of <see cref="T:System.Web.HttpContextBase" /> or null if not found.</returns>
    public static HttpContextBase GetHttpContext(this IOwinContext context) => context.Get<HttpContextBase>(typeof (HttpContextBase).FullName);

    /// <summary>Gets the HttpContextBase from the OWIN Request.</summary>
    /// <param name="request">The OWIN request.</param>
    /// <returns>An instance of <see cref="T:System.Web.HttpContextBase" /> or null if not found.</returns>
    public static HttpContextBase GetHttpContext(this IOwinRequest request) => request.Get<HttpContextBase>(typeof (HttpContextBase).FullName);
  }
}
