// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Responses.RelatedDataItemResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.RelatedData.Responses
{
  /// <summary>
  /// Represents the response returned for related data item.
  /// </summary>
  public class RelatedDataItemResponse : RelatedItemResponse
  {
    /// <summary>Gets or sets the preview URL.</summary>
    public string PreviewUrl { get; set; }

    /// <summary>Gets or sets the owner.</summary>
    public string Owner { get; set; }

    /// <summary>Gets or sets the last modified date.</summary>
    public DateTime LastModified { get; set; }

    /// <summary>
    /// Gets or sets the subtitle for the item (path to the item usually).
    /// </summary>
    public string SubTitle { get; set; }
  }
}
