// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Statistics.WebActionEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Statistics
{
  /// <summary>
  /// Event arguments container for events raised by StatisticsWebCounterService
  /// </summary>
  /// <example>
  /// Can be used for example when notifying that a forum thread was visited - so the forum visit statistics can be updated
  /// The arguments can keep the forum thread id, the object type -'forumthread' and the type of action - 'visit'
  /// </example>
  internal class WebActionEventArgs : EventArgs
  {
    public WebActionEventArgs(string action, string objectType, string objectId)
    {
      this.Action = action;
      this.ObjectType = objectType;
      this.ObjectId = objectId;
    }

    public virtual string Action { get; protected set; }

    public virtual string ObjectType { get; protected set; }

    public virtual string ObjectId { get; protected set; }
  }
}
