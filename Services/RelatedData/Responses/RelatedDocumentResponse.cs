// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Responses.RelatedDocumentResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.RelatedData.Responses
{
  /// <summary>
  /// Represents the response returned for related document item.
  /// </summary>
  public class RelatedDocumentResponse : RelatedItemResponse
  {
    /// <summary>Gets or sets the document extension.</summary>
    /// <value>The extension.</value>
    public string Extension { get; set; }

    /// <summary>Gets or sets the document total size.</summary>
    /// <value>The total size.</value>
    public long TotalSize { get; set; }
  }
}
