// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.GenericData.Messages.DataItemMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Services.GenericData.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for retrieving available data items for RelatedDataSelector
  /// </summary>
  public class DataItemMessage
  {
    /// <summary>Gets or sets the item id</summary>
    public string ItemId { get; set; }

    /// <summary>The type of the content that can be related</summary>
    public string ItemType { get; set; }

    /// <summary>The provider of the content that can be related</summary>
    public string ItemProvider { get; set; }

    /// <summary>Gets or sets the related item id.</summary>
    public string RelatedItemId { get; set; }

    /// <summary>Gets or sets the type of the related item.</summary>
    public string RelatedItemType { get; set; }

    /// <summary>Gets or sets the related item provider.</summary>
    public string RelatedItemProvider { get; set; }

    /// <summary>
    /// Specifies the status availability of the relation.
    /// (Each relation can be available for Temp, Master and Live versions of the item.)
    /// </summary>
    public ContentLifecycleStatus? RelationStatus { get; set; }

    /// <summary>The name of the field that contains related data</summary>
    public string FieldName { set; get; }

    /// <summary>The filter expression</summary>
    public string Filter { get; set; }

    /// <summary>The order expression</summary>
    public string Order { get; set; }

    /// <summary>The number of items to skip</summary>
    public int Skip { get; set; }

    /// <summary>The number of items to take</summary>
    public int Take { get; set; }
  }
}
