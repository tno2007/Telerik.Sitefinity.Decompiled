// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Events.MediaContentDownloadingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Events
{
  public class MediaContentDownloadingEvent : 
    IMediaContentDownloadingEvent,
    IMediaContentDownloadEvent,
    IEvent
  {
    public Guid LibraryId { get; set; }

    public Guid FileId { get; set; }

    public string ProviderName { get; set; }

    public string BlobStorageProviderName { get; set; }

    public string MimeType { get; set; }

    public Type Type { get; set; }

    public string Title { get; set; }

    public string Url { get; set; }

    public Guid UserId { get; set; }

    public NameValueCollection Headers { get; set; }

    public string Origin { get; set; }
  }
}
