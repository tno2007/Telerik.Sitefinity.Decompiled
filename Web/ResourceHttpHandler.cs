// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Returns resources from the VirtualPathManager</summary>
  public class ResourceHttpHandler : IHttpHandler
  {
    private IResourceProvider resourceFileProvider;
    private IResourceProvider razorTemplatesResourceProvider;

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
    public bool IsReusable => true;

    /// <summary>
    /// Gets or sets the resource file provider that will be used to fetch files that are not handled by more specific file providers.
    /// </summary>
    /// <value>The resource file provider instance.</value>
    public IResourceProvider ResourceFileProvider
    {
      get
      {
        if (this.resourceFileProvider == null)
          this.resourceFileProvider = (IResourceProvider) new Telerik.Sitefinity.Web.ResourceFileProvider();
        return this.resourceFileProvider;
      }
      set => this.resourceFileProvider = value;
    }

    /// <summary>
    /// Gets or sets the Razor templates file provider. It will be used to fetch razor template files.
    /// </summary>
    /// <value>The templates provider.</value>
    public virtual IResourceProvider RazorTemplatesProvider
    {
      get
      {
        if (this.razorTemplatesResourceProvider == null)
          this.razorTemplatesResourceProvider = (IResourceProvider) new RazorTemplatesResourceProvider(this.ResourceFileProvider);
        return this.razorTemplatesResourceProvider;
      }
      set => this.razorTemplatesResourceProvider = value;
    }

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The HttpContextBase.</param>
    public virtual void ProcessRequest(HttpContextBase context)
    {
      LocalizationBehavior.ApplyLocalizationBehaviorFromCurrentRequest();
      HttpResponseBase response = context.Response;
      string resourceName = this.GetResourceName(context);
      string resourceContent;
      int num = this.RazorTemplatesProvider.TryGetResourceContent(resourceName, out resourceContent) ? 1 : 0;
      if (num != 0 && !this.CurrentUserIsBackendUser())
        throw new UnauthorizedAccessException();
      if (num == 0)
      {
        resourceContent = this.ResourceFileProvider.GetResourceContent(resourceName);
        if (resourceName.EndsWith(".js"))
          response.ContentType = "text/javascript";
      }
      response.AddHeader("content-disposition", SitefinityExtensions.RemoveCRLF("inline; filename={0}".Arrange((object) resourceName)));
      string str = resourceContent.Length.ToString();
      if (response.ContentEncoding != null)
        str = response.ContentEncoding.GetByteCount(resourceContent).ToString();
      response.AddHeader("Content-Length", str);
      response.StatusCode = 200;
      response.StatusDescription = "OK";
      response.Write(resourceContent);
      context.ApplicationInstance.CompleteRequest();
    }

    internal virtual bool CurrentUserIsBackendUser() => SecurityManager.IsBackendUser();

    /// <summary>Gets the name of the resource.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    internal virtual string GetResourceName(HttpContextBase context) => context.Items[(object) "resourceName"] as string;
  }
}
