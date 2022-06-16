// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Events.IMediaContentDownloadEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Events
{
  public interface IMediaContentDownloadEvent : IEvent
  {
    Guid FileId { get; set; }

    Guid LibraryId { get; set; }

    string ProviderName { get; set; }

    string BlobStorageProviderName { get; set; }

    string MimeType { get; set; }

    Type Type { get; set; }

    string Title { get; set; }

    string Url { get; set; }

    Guid UserId { get; set; }

    NameValueCollection Headers { get; set; }
  }
}
