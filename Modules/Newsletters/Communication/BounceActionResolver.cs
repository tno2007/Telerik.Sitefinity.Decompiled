// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Communication.BounceActionResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Newsletters.Configuration;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Communication
{
  /// <summary>
  /// A component responsible for resolving and performing <see cref="T:Telerik.Sitefinity.Modules.Newsletters.BounceAction" />.
  /// </summary>
  /// <remarks>
  /// This implementation can handle only <c>BounceAction.DeleteUser</c> and <c>BounceAction.SuspendUser</c> actions.
  /// </remarks>
  public class BounceActionResolver
  {
    /// <summary>
    /// Resolves the bounce action that should be performed based on the
    /// specified <paramref name="bounceStatus" />.
    /// </summary>
    /// <param name="bounceStatus">The bounce status.</param>
    /// <returns>The bounce action to perform.</returns>
    public virtual BounceAction ResolveAction(BounceStatus bounceStatus)
    {
      BounceAction bounceAction = BounceAction.DoNothing;
      switch (bounceStatus)
      {
        case BounceStatus.Soft:
          bounceAction = Config.Get<NewslettersConfig>().SoftBounceAction;
          break;
        case BounceStatus.Hard:
          bounceAction = Config.Get<NewslettersConfig>().HardBounceAction;
          break;
      }
      return bounceAction;
    }

    /// <summary>
    /// Performs the specified <paramref name="bounceAction" /> using the <paramref name="newsletterManager" />.
    /// </summary>
    /// <param name="newslettersManager">The newsletters manager that will be used to handle the specified bounceAction.</param>
    /// <param name="subscriberId">The id of the subscriber that caused the bounced message.</param>
    /// <param name="bounceAction">The bounce action to perform.</param>
    /// <param name="commitChanages">Specified whether to commit the performed changes (if any).</param>
    /// <returns><c>true</c> if the the bounce action was performed, otherwise <c>false</c>.</returns>
    public virtual bool PerformAction(
      NewslettersManager newslettersManager,
      Guid subscriberId,
      BounceAction bounceAction,
      bool commitChanages = false)
    {
      bool flag = false;
      switch (bounceAction)
      {
        case BounceAction.SuspendUser:
          Subscriber subscriber1 = newslettersManager.GetSubscribers().SingleOrDefault<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Id == subscriberId));
          if (subscriber1 != null)
          {
            flag = true;
            subscriber1.IsSuspended = true;
            break;
          }
          break;
        case BounceAction.DeleteUser:
          Subscriber subscriber2 = newslettersManager.GetSubscribers().SingleOrDefault<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Id == subscriberId));
          if (subscriber2 != null)
          {
            newslettersManager.DeleteSubscriber(subscriber2);
            flag = true;
            break;
          }
          break;
      }
      if (commitChanages & flag)
        newslettersManager.SaveChanges();
      return flag;
    }
  }
}
