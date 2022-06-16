// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.PageTemplateViewModelCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>
  /// Implementation of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.IPageTemplateViewModelCreatedEvent" />
  /// </summary>
  internal class PageTemplateViewModelCreatedEvent : 
    EventBase,
    IPageTemplateViewModelCreatedEvent,
    IPostProcessingEvent,
    IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.PageTemplateViewModelCreatedEvent" /> class.
    /// </summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="viewModel">The view model.</param>
    public PageTemplateViewModelCreatedEvent(
      PageTemplate pageTemplate,
      PageTemplateViewModel viewModel)
    {
      this.PageTemplate = pageTemplate;
      this.ViewModel = viewModel;
    }

    /// <summary>
    /// Gets the page template that is used to build the view model.
    /// </summary>
    /// <value>The page template that is used to build the view model.</value>
    public PageTemplate PageTemplate { get; private set; }

    /// <summary>Gets the page template view model.</summary>
    /// <value>The view model.</value>
    public PageTemplateViewModel ViewModel { get; private set; }
  }
}
