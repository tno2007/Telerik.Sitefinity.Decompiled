// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Messages.RelationChangeMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.RelatedData.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for applying relation changes.
  /// </summary>
  public class RelationChangeMessage
  {
    /// <summary>Gets or sets the item id.</summary>
    public string ItemId { get; set; }

    /// <summary>Gets or sets the type of the item.</summary>
    public string ItemType { get; set; }

    /// <summary>Gets or sets the provider.</summary>
    public string ItemProvider { get; set; }

    /// <summary>
    /// Represents the changes made to the related child items.
    /// </summary>
    public ContentLinkChange[] RelationChanges { get; set; }
  }
}
