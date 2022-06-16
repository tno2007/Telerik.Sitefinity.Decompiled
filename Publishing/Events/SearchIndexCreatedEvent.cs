// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Search.SearchIndexCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services.Search
{
  internal class SearchIndexCreatedEvent : ISearchIndexCreatedEvent, ISearchIndexEvent, IEvent
  {
    public string Origin { get; set; }

    public string Name { get; set; }
  }
}
