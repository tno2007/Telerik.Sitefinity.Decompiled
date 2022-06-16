// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.IForbiddenPageAccessEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// An interface of the event notifying that the current authenticated user was denied to access the requested front-end page.
  /// </summary>
  public interface IForbiddenPageAccessEvent : IEvent
  {
    /// <summary>Gets the current HTTP context.</summary>
    /// <value>The HTTP context.</value>
    HttpContextBase HttpContext { get; }

    /// <summary>Gets the page node that the user is denied to access.</summary>
    /// <value>The page.</value>
    PageSiteNode Page { get; }
  }
}
