// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ABTesting.IABTestClientReportUrlBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.ABTesting
{
  /// <summary>
  /// Exposes an interface of AB tests client report url builder
  /// </summary>
  internal interface IABTestClientReportUrlBuilder
  {
    /// <summary>Gets the correct repost url for specified test</summary>
    /// <param name="testId">The test id</param>
    /// <returns>The report url</returns>
    string GetTestReportUrl(string testId);
  }
}
