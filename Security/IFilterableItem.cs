// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.IFilterableItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security
{
  /// <summary>Provides the ability to filter items</summary>
  public interface IFilterableItem
  {
    /// <summary>Gets the item type</summary>
    string ItemType { get; }

    /// <summary>Gets the item id</summary>
    string ItemId { get; }

    /// <summary>Gets the item provider</summary>
    string ItemProvider { get; }
  }
}
