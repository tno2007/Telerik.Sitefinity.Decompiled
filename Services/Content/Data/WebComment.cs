// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Data.WebComment
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Services.Content.Data
{
  /// <summary>
  /// Used to pass information from the client when creating a new comment
  /// </summary>
  [DataContract(Name = "WebComment")]
  public class WebComment : IExtensibleDataObject
  {
    /// <summary>Gets or sets the structure that contains extra data.</summary>
    /// <value></value>
    /// <returns>An <see cref="T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
    public ExtensionDataObject ExtensionData { get; set; }

    /// <summary>Gets or sets the text for the new comment.</summary>
    /// <value>Text for the new comment.</value>
    [DataMember]
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the pageId of the content item to comment
    /// </summary>
    /// <value>ID of the content item to comment.</value>
    [DataMember]
    public Guid ContentId { get; set; }
  }
}
