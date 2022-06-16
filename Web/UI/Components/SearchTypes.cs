// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Components.SearchTypes
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Components
{
  /// <summary>
  /// Types of searches that can be performed with search box
  /// </summary>
  public enum SearchTypes
  {
    /// <summary>The search field must contain the expression</summary>
    Contains,
    /// <summary>The search field must start with the expression</summary>
    StartsWith,
    /// <summary>The search field must end with the expression</summary>
    EndsWith,
    /// <summary>The search field must be equal to the expression</summary>
    Equals,
  }
}
