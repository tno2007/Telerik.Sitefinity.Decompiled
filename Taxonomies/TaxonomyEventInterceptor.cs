// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyEventInterceptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Taxonomies.Events;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.ScheduledTasks;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Handles taxonomy related <see cref="!:IEvent" />s for which Sitefinity needs to subscribe and the logic about processing those events.
  /// </summary>
  public class TaxonomyEventInterceptor : ITaxonomyEventInterceptor, IEventInterceptor
  {
    /// <summary>
    /// Subscribes this instance for <see cref="!:IEvent" />s.
    /// </summary>
    public void Subscribe() => EventHub.Subscribe<IUnlinkedEvent>(new SitefinityEventHandler<IUnlinkedEvent>(this.HandleUnlinkedEvent));

    private void HandleUnlinkedEvent(IUnlinkedEvent unlinkedEvent)
    {
      if (!typeof (Taxonomy).IsAssignableFrom(((ILinkEvent) unlinkedEvent).ItemType))
        return;
      ITaxonomy taxonomy = TaxonomyManager.GetManager(unlinkedEvent.ItemProviderName).GetTaxonomy(((ILinkEvent) unlinkedEvent).ItemId);
      ObjectFactory.Resolve<ITaxonomyMultisiteTaskService>().CreateTaxonomyUnlinkFromSiteCleanTask(unlinkedEvent.SiteId, taxonomy);
    }
  }
}
