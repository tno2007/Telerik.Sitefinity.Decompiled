// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Enums.BackendSearchMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Enums
{
  /// <summary>
  /// Specifies the search mode used in backend master views
  /// </summary>
  public enum BackendSearchMode
  {
    /// <summary>
    /// Indicates that the search mode has not been set. Use this when you want the property
    /// to be set by other member of the chain or responsibility pattern.
    /// </summary>
    NotSet,
    /// <summary>No search</summary>
    None,
    /// <summary>Basic mode. This is just filtering using a textbox</summary>
    Basic,
    /// <summary>
    /// Advanced mode. Use the Query Builder to create a custom filter expression
    /// </summary>
    Advanced,
    /// <summary>
    /// Use both simple and advanced modes. They can be switched through the user interface
    /// </summary>
    Both,
  }
}
