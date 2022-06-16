// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Responses.RelatedMediaResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.RelatedData.Responses
{
  /// <summary>
  /// Represents the response returned for related media item.
  /// </summary>
  public class RelatedMediaResponse : RelatedItemResponse
  {
    /// <summary>Gets or sets the thumbnail URL.</summary>
    /// <value>The thumbnail URL.</value>
    public string ThumbnailUrl { get; set; }

    /// <summary>Gets or sets the media URL.</summary>
    /// <value>The media URL.</value>
    public string MediaUrl { get; set; }
  }
}
