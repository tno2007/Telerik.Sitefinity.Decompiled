// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Enums.DecisionType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Enums
{
  /// <summary>
  /// Provides the types of decisions that decision screen offers depending
  /// on the context in which it was displayed.
  /// </summary>
  public enum DecisionType
  {
    /// <summary>
    /// Decision type has not been set. Used generally when the
    /// decision type will be determined in some other part of the
    /// chain of responsility pattern.
    /// </summary>
    NotSet,
    /// <summary>
    /// New item has been created and decision screen offers possibilities
    /// of next actions that can be taken.
    /// </summary>
    NewItemCreated,
    /// <summary>
    /// No items have been displayed, perhaps as a result of search or filtering.
    /// </summary>
    NoItemsDisplayed,
    /// <summary>
    /// There are no items, regardless of the search or filtering.
    /// </summary>
    NoItemsExist,
  }
}
