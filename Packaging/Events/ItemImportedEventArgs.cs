// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Events.ItemImportedEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Packaging.Events
{
  /// <summary>Item imported event args</summary>
  /// <seealso cref="T:System.EventArgs" />
  public class ItemImportedEventArgs : EventArgs
  {
    /// <summary>Gets or sets the item identifier.</summary>
    /// <value>The item identifier.</value>
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public string ItemType { get; set; }

    /// <summary>Gets or sets the item provider.</summary>
    /// <value>The item provider.</value>
    public string ItemProvider { get; set; }

    /// <summary>Gets or sets the name of the transaction.</summary>
    /// <value>The name of the transaction.</value>
    public string TransactionName { get; set; }

    /// <summary>Gets or sets the additional information.</summary>
    /// <value>The additional information.</value>
    public string AdditionalInfo { get; set; }
  }
}
