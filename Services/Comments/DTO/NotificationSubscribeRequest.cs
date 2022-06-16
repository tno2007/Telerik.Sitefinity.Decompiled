// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.NotificationSubscribeRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>NotificationSubscribeRequest </c> Used when the user is subscribed to receive notifications.
  /// </summary>
  /// <remarks>
  /// Currently the user could be subscribed to single thread depending on the provided <see cref="P:Telerik.Sitefinity.Services.Comments.DTO.NotificationSubscribeRequest.ThreadKey" />.
  /// </remarks>
  public class NotificationSubscribeRequest
  {
    /// <summary>Gets or sets the thread key.</summary>
    /// <value>The thread key.</value>
    public string ThreadKey { set; get; }
  }
}
