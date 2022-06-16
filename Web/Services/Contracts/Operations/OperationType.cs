// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.OperationType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations
{
  /// <summary>The target of the operation.</summary>
  public enum OperationType
  {
    /// <summary>
    /// When the operation is not coupled with an item or entity set.
    /// </summary>
    Unbound,
    /// <summary>When the operation targets specific item.</summary>
    PerItem,
    /// <summary>When operation targets given entity set.</summary>
    Collection,
  }
}
