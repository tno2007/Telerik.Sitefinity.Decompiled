// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.Messages.ChildItemMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.RelatedData.Messages
{
  /// <summary>
  /// Class representing child item message in the related data service
  /// </summary>
  public class ChildItemMessage
  {
    /// <summary>The Id of the related item</summary>
    public string ChildItemId { get; set; }

    /// <summary>The type of the related item.</summary>
    public string ChildItemType { get; set; }

    /// <summary>The provider of the related item.</summary>
    public string ChildProviderName { get; set; }

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
