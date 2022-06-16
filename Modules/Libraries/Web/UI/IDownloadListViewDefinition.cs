// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IDownloadListViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>A base contract for download list definition</summary>
  public interface IDownloadListViewDefinition
  {
    /// <summary>Displays a download link below the description</summary>
    bool? ShowDownloadLinkBelowDescription { get; set; }

    /// <summary>Displays a download link above the description</summary>
    bool? ShowDownloadLinkAboveDescription { get; set; }

    /// <summary>
    /// Sets the size of the thumbnail - None - no thumbnail, Big and Small
    /// /// </summary>
    ThumbnailType ThumbnailType { get; set; }
  }
}
