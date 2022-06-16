// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Analytics.IAnalyticsApiAccessManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Analytics
{
  /// <summary>
  /// An interface that exposes analytics functionality to Sitefinity.
  /// </summary>
  public interface IAnalyticsApiAccessManager
  {
    /// <summary>Gets Analytics Module Id</summary>
    Guid AnalyticsModuleId { get; }

    string GetTopContentReportPath(string pageUrl, Guid pageNodeId);

    bool IsConfigured { get; }

    /// <summary>Gets the analytics link for an item.</summary>
    /// <param name="itemTitle">The item title.</param>
    /// <param name="itemId">The item Id.</param>
    /// <param name="itemType">The item type.</param>
    /// <param name="providerName">The provider name.</param>
    /// <returns>The analytics link.</returns>
    string GetLinkForItem(string itemTitle, string itemId, Type itemType, string providerName);

    /// <summary>Gets if the passed item type supports analytics.</summary>
    /// <param name="type">The item type.</param>
    /// <returns>True if the Item supports analytics.</returns>
    bool DoesItemSupportAnalytics(Type type);
  }
}
