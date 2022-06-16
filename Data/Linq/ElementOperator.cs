// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.ElementOperator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// Defines the supported element operators for Linq to Sitefinity.
  /// </summary>
  public enum ElementOperator
  {
    /// <summary>No element operator.</summary>
    None,
    /// <summary>First element operator.</summary>
    First,
    /// <summary>First or default element operator.</summary>
    FirstOrDefault,
    /// <summary>Any element operator</summary>
    Any,
    /// <summary>Distinct element operator.</summary>
    Distinct,
    /// <summary>Element at element operator.</summary>
    ElementAt,
    /// <summary>Element at or default element operator.</summary>
    ElementAtOrDefault,
    /// <summary>Last element operator.</summary>
    Last,
    /// <summary>Last or default element operator.</summary>
    LastOrDefault,
    /// <summary>Single element operator.</summary>
    Single,
    /// <summary>Single or default element operator.</summary>
    SingleOrDefault,
  }
}
