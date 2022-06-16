// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Events.LinkEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Taxonomies.Events
{
  internal class LinkEventFactory
  {
    public static ILinkEvent CreateEvent(
      IDataItem dataItem,
      OperationStatus operation,
      Guid siteId,
      string action,
      string origin = null)
    {
      string providerName = DataEventFactory.GetProviderName(dataItem.Provider);
      if (operation == OperationStatus.LinkWithSite)
        return (ILinkEvent) new LinkedEvent(origin, dataItem.Id, dataItem.GetType(), providerName, siteId, action);
      if (operation == OperationStatus.UnlinkWithSite)
        return (ILinkEvent) new UnlinkedEvent(origin, dataItem.Id, dataItem.GetType(), providerName, siteId, action);
      throw new InvalidOperationException("Not supported operation. Only LinkWithSite and UnlinkWithSite operations allowed.");
    }
  }
}
