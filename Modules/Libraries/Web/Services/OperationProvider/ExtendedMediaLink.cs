// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider.ExtendedMediaLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services.OperationProvider
{
  internal class ExtendedMediaLink : MediaFileLink
  {
    internal ExtendedMediaLink(MediaFileLink link, IEnumerable<int> cultures)
    {
      using (new CultureRegion(link.Culture))
      {
        this.Title = (string) link.MediaContent.Title;
        this.ThumbnailUrl = link.MediaContent.ThumbnailUrl;
        this.Url = link.MediaContent.Url;
        this.MediaFileUrlName = (string) link.MediaContent.MediaFileUrlName;
        this.Cultures = new List<string>();
        foreach (int culture in cultures)
          this.Cultures.Add(Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(culture).Name);
        this.Id = link.FileId;
        this.Extension = link.Extension;
        this.Width = link.Width;
        this.Height = link.Height;
        this.MimeType = link.MimeType;
        this.Culture = link.Culture;
        this.FileId = link.FileId;
        this.MediaContentId = link.MediaContentId;
        this.TotalSize = link.TotalSize;
      }
    }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string ThumbnailUrl { get; set; }

    [DataMember]
    public string Url { get; set; }

    [DataMember]
    public string MediaFileUrlName { get; set; }

    [DataMember]
    public List<string> Cultures { get; set; }
  }
}
