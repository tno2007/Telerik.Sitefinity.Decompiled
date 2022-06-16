// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.StatisticsHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Statistics;

namespace Telerik.Sitefinity.Web
{
  internal class StatisticsHttpHandler : IHttpHandler
  {
    private string ActionParam = "action";
    private string ObjectTypeParam = "objectType";
    private string ObjectId = "objectid";
    private string ImageExpireHoursParam = "imgExpire";
    private static readonly byte[] transparent1pixelGifImage = new byte[43]
    {
      (byte) 71,
      (byte) 73,
      (byte) 70,
      (byte) 56,
      (byte) 57,
      (byte) 97,
      (byte) 1,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 128,
      (byte) 0,
      (byte) 0,
      byte.MaxValue,
      byte.MaxValue,
      byte.MaxValue,
      byte.MaxValue,
      byte.MaxValue,
      byte.MaxValue,
      (byte) 33,
      (byte) 249,
      (byte) 4,
      (byte) 1,
      (byte) 10,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 44,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 1,
      (byte) 0,
      (byte) 0,
      (byte) 2,
      (byte) 2,
      (byte) 76,
      (byte) 1,
      (byte) 0,
      (byte) 59
    };

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public virtual void ProcessRequest(HttpContext context)
    {
      System.Web.HttpRequest request = context.Request;
      System.Web.HttpResponse response = context.Response;
      string actionName = request.Params[this.ActionParam];
      string objectType = request.Params[this.ObjectTypeParam];
      string objectId = request.Params[this.ObjectId];
      string s = request.Params[this.ImageExpireHoursParam];
      int num = 0;
      ref int local = ref num;
      int.TryParse(s, out local);
      response.Cache.SetCacheability(HttpCacheability.Private);
      DateTime date = DateTime.UtcNow;
      date = date.AddHours((double) num);
      response.Cache.SetExpires(date);
      response.ContentType = "image/GIF";
      response.OutputStream.Write(StatisticsHttpHandler.transparent1pixelGifImage, 0, StatisticsHttpHandler.transparent1pixelGifImage.Length);
      response.Output.Flush();
      response.Flush();
      SystemManager.CurrentHttpContext.ApplicationInstance.CompleteRequest();
      ObjectFactory.Resolve<IStatisticsWebCounterService>().NotifySubscribers(objectType, actionName, objectId);
    }
  }
}
