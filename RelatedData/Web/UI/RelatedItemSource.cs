// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.Web.UI.RelatedItemSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.RelatedData.Web.UI
{
  /// <summary>
  /// Defines different types of sources for resolving related item
  /// </summary>
  public enum RelatedItemSource
  {
    /// <summary>
    /// Related item is resolved from the current DataItemContainer.
    /// </summary>
    DataItemContainer,
    /// <summary>Related item is resolved from the URL.</summary>
    Url,
    /// <summary>
    /// Related item is no automatically resolved. RelatedItemId and RelatedItemProviderName should be set manually.
    /// </summary>
    NoAutomaticBinding,
  }
}
