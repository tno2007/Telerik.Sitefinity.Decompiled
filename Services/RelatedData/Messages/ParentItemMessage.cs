// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Messages.ParentItemMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Services.RelatedData.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for retrieving related data info for specific item
  /// </summary>
  public class ParentItemMessage
  {
    /// <summary>The Id of the parent item of the related item</summary>
    public string ParentItemId { get; set; }

    /// <summary>The Type of the parent item</summary>
    public string ParentItemType { get; set; }

    /// <summary>The Provider of the parent item</summary>
    public string ParentProviderName { get; set; }

    /// <summary>The name of the field that contains related data</summary>
    public string FieldName { set; get; }

    /// <summary>The status of the items</summary>
    public ContentLifecycleStatus? Status { get; set; }

    /// <summary>The filter expression</summary>
    public string Filter { get; set; }

    /// <summary>The order expression</summary>
    public string Order { get; set; }

    /// <summary>The number of items to skip</summary>
    public int Skip { get; set; }

    /// <summary>The number of items to take</summary>
    public int Take { get; set; }

    /// <summary>The Type of the child item</summary>
    public string ChildItemType { get; set; }

    /// <summary>The Provider of the parent item</summary>
    public string ChildProviderName { get; set; }
  }
}
