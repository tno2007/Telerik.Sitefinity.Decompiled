// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.ThumbnailUploadHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  /// <summary>
  /// Handler for uploading thumbnails through thumbnail generation for Sitefinity libraries module
  /// </summary>
  public class ThumbnailUploadHandler : IHttpHandler
  {
    private LibrariesManager manager;

    /// <summary>Gets Library manager.</summary>
    /// <value>The Library manager.</value>
    public LibrariesManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = ManagerBase.GetMappedManager(typeof (Video), this.ProviderName) as LibrariesManager;
        return this.manager;
      }
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the content id.</summary>
    /// <value>The content id.</value>
    public Guid ContentId { get; set; }

    /// <summary>
    /// Gets or sets the binary data that will be uploaded as a thumbnail.
    /// </summary>
    /// <value>The data.</value>
    public byte[] Data { get; set; }

    /// <summary>
    /// Gets or sets the id of the Sitefinity image that will be used as a thumbnail.
    /// </summary>
    /// <value>The thumbnail image id.</value>
    public Guid ThumbnailImageId { get; set; }

    /// <summary>
    /// Gets or sets whether a Sitefinity image will be used as a thumbnail.
    /// </summary>
    /// <value>The use sitefinity image.</value>
    public bool UseSitefinityImage { get; set; }

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      NameValueCollection headers = context.Request.Headers;
      int num = headers.Keys.Contains("SF_UI_CULTURE") ? 1 : 0;
      bool flag = headers.Keys.Contains("IsBackendRequest") && headers["IsBackendRequest"] == "true";
      CultureInfo culture = (CultureInfo) null;
      if (num == 0 || !flag)
        RouteHelper.ApplyThreadCulturesForCurrentUser();
      else
        culture = CultureInfo.GetCultureInfo(headers["SF_UI_CULTURE"]);
      using (new CultureRegion(culture))
      {
        ThumbnailUploadHandler.UploadResponse uploadResponse = this.SaveData(context);
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        context.Response.Write(scriptSerializer.Serialize((object) uploadResponse));
        context.Response.End();
      }
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
    public bool IsReusable => false;

    private ThumbnailUploadHandler.UploadResponse SaveData(
      HttpContextBase context)
    {
      ThumbnailUploadHandler.UploadResponse uploadResponse = new ThumbnailUploadHandler.UploadResponse()
      {
        ContentId = (string) null,
        ErrorMessage = "No thumbnail uploaded",
        UploadResult = false
      };
      try
      {
        this.InitializeParameters(context);
        byte[] data = this.Data;
        if (this.UseSitefinityImage)
        {
          using (MemoryStream destination = new MemoryStream())
          {
            this.Manager.Download(this.ThumbnailImageId).CopyTo((Stream) destination);
            data = destination.ToArray();
          }
        }
        this.Manager.UpdateThumbnail(this.ContentId, data);
        this.Manager.SaveChanges();
        uploadResponse.ContentId = this.ContentId.ToString();
        uploadResponse.UploadResult = true;
        uploadResponse.ErrorMessage = (string) null;
      }
      catch (Exception ex)
      {
        string str = string.Format("Cannot save the thumbnail: [{0}]", (object) ex.Message);
        uploadResponse.ContentId = (string) null;
        uploadResponse.UploadResult = false;
        uploadResponse.ErrorMessage = str;
      }
      return uploadResponse;
    }

    private void InitializeParameters(HttpContextBase context)
    {
      this.ContentId = new Guid(this.GetParamsValue(context, "contentId", true));
      this.ProviderName = this.GetParamsValue(context, "provider", false);
      string paramsValue = this.GetParamsValue(context, "useSFImage", false);
      if (!string.IsNullOrEmpty(paramsValue) && bool.Parse(paramsValue))
      {
        this.ThumbnailImageId = new Guid(this.GetParamsValue(context, "thumbnailImageId", false));
        this.UseSitefinityImage = true;
      }
      else
      {
        string s = this.GetParamsValue(context, "data", false);
        if (s != null)
          s = s.Replace("data:image/jpeg;base64,", "").Replace(' ', '+').Replace('-', '+').Replace('_', '/');
        this.Data = Convert.FromBase64String(s);
      }
    }

    private string GetParamsValue(HttpContextBase context, string paramName, bool required)
    {
      string paramsValue = context.Request.Params[paramName];
      if (!(string.IsNullOrEmpty(paramName) & required))
        return paramsValue;
      throw new ArgumentNullException(paramName);
    }

    internal class UploadResponse
    {
      public string ContentId { get; set; }

      public bool UploadResult { get; set; }

      public string ErrorMessage { get; set; }
    }
  }
}
