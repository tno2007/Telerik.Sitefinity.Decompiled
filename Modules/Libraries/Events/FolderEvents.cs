// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Events.FolderEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Libraries.Events
{
  internal class FolderEvent : IFolderEvent, IDataEvent, IEvent, IHasTitle, IPropertyChangeDataEvent
  {
    private IDictionary<string, PropertyChange> changedPropertyNames;

    public string Action { get; set; }

    public Type ItemType { get; set; }

    public Guid ItemId { get; set; }

    public string ProviderName { get; set; }

    public string Origin { get; set; }

    public Guid RootId { get; set; }

    internal string Title { get; set; }

    /// <inheritdoc />
    string IHasTitle.GetTitle(CultureInfo culture) => this.Title;

    /// <inheritdoc />
    public IDictionary<string, PropertyChange> ChangedProperties
    {
      get
      {
        if (this.changedPropertyNames == null)
          this.changedPropertyNames = (IDictionary<string, PropertyChange>) new Dictionary<string, PropertyChange>();
        return this.changedPropertyNames;
      }
    }
  }
}
