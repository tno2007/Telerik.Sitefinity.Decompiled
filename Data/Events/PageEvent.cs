// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.PageEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Data.Events
{
  internal class PageEvent : DataEvent
  {
    public PageEvent(PageNode node)
    {
      this.RootNodeId = node.RootNodeId;
      this.IsBackend = node.IsBackend;
      this.Attributes = node.Attributes;
      this.LocalizationStrategy = node.LocalizationStrategy;
      this.AvailableCultures = node.AvailableCultures;
      this.HasPageData = node.PageDataList.Any<PageData>();
    }

    public Guid RootNodeId { get; set; }

    public bool IsBackend { get; set; }

    public IDictionary<string, string> Attributes { get; set; }

    public LocalizationStrategy LocalizationStrategy { get; set; }

    public CultureInfo[] AvailableCultures { get; set; }

    public bool HasPageData { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the PageData object was modified in the context of Sitefinity
    /// </summary>
    public bool HasPageDataChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the PageNode object was modified in the context of Sitefinity
    /// </summary>
    public bool HasPageNodeChanged { get; set; }

    /// <summary>
    /// Gets or sets the actual data item type that was modified and triggered the event. Needed for cases when
    /// the original data item object which triggers the event is substituted by its parent node when the event is fired.
    /// </summary>
    public Type TriggeredByType { get; set; }
  }
}
