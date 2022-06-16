// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.CommentServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Text;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Services.Comments.DTO;

namespace Telerik.Sitefinity.Services.Comments
{
  /// <summary>
  /// Represents a ServiceStack plugin for the comments web service.
  /// </summary>
  public class CommentServiceStackPlugin : IPlugin
  {
    private const string CommentServiceRoute = "/comments-api";

    /// <summary>Adding the comment service routes</summary>
    /// <param name="appHost"></param>
    public void Register(IAppHost appHost)
    {
      JsConfig.AlwaysUseUtc = Telerik.Sitefinity.Configuration.Config.Get<CommentsModuleConfig>().AlwaysUseUTC;
      appHost.RegisterService(typeof (CommentWebService));
      appHost.Routes.Add<CommentsFilter>("/comments-api" + "/" + "comments", "GET").Add<CommentsCountGetRequest>("/comments-api" + "/" + "comments/count", "GET").Add<ReviewsStatisticsGetRequest>("/comments-api" + "/" + "comments/reviews_statistics", "GET").Add<CommentGetRequest>("/comments-api" + "/" + "comments/{key}", "GET").Add<CommentsFilterExtended>("/comments-api" + "/" + "comments/filter", "POST").Add<CommentCreateRequest>("/comments-api" + "/" + "comments", "POST").Add<CommentUpdateRequest>("/comments-api" + "/" + "comments", "PUT").Add<CommentDeleteRequest>("/comments-api" + "/" + "comments/{key}", "DELETE").Add<CommentsBatchUpdateRequest>("/comments-api" + "/" + "comments/list", "PUT").Add<CommentsBatchDeleteRequest>("/comments-api" + "/" + "comments/list", "DELETE").Add<ThreadsFilterExtended>("/comments-api" + "/" + "threads/filter", "POST").Add<ThreadUpdateRequest>("/comments-api" + "/" + "threads", "PUT").Add<NotificationStatusRequest>("/comments-api" + "/" + "notifications", "GET").Add<NotificationSubscribeRequest>("/comments-api" + "/" + "notifications/subscribe", "POST").Add<NotificationUnsubscribeRequest>("/comments-api" + "/" + "notifications/unsubscribe", "POST").Add<CaptchaRequest>("/comments-api" + "/" + "captcha", "GET").Add<CaptchaInfo>("/comments-api" + "/" + "captcha", "POST");
    }
  }
}
