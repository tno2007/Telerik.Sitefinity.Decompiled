// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.IRelatedDataHolder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RelatedData
{
  /// <summary>Interface for types having related data fields</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Model.IDataItem" />
  public interface IRelatedDataHolder : IDataItem
  {
    /// <summary>Gets or sets the full name of the type.</summary>
    /// <value>The full name of the type.</value>
    string FullTypeName { get; set; }

    /// <summary>Gets or sets the item identifier.</summary>
    /// <value>The item identifier.</value>
    Guid ItemId { get; set; }

    /// <summary>Gets or sets the provider name of the item.</summary>
    /// <value>The provider name of the item.</value>
    string ProviderName { get; set; }

    /// <summary>Gets or sets the lifecycle status.</summary>
    /// <value>The lifecycle status.</value>
    ContentLifecycleStatus Status { get; set; }
  }
}
