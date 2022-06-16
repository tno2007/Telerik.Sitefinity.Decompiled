// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.IPageTemplateViewModelCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// Event raised when a page template view model is created.
  /// </summary>
  public interface IPageTemplateViewModelCreatedEvent : IPostProcessingEvent, IEvent
  {
    /// <summary>
    /// Gets the page template that is used to build the view model.
    /// </summary>
    /// <value>The page template that is used to build the view model.</value>
    PageTemplate PageTemplate { get; }

    /// <summary>Gets the page template view model.</summary>
    /// <value>The view model.</value>
    PageTemplateViewModel ViewModel { get; }
  }
}
