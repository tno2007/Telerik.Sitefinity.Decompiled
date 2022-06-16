// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.DynamicModules.IHtmlFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.DynamicModules
{
  /// <summary>
  /// Determines method which apply filter on an html content.
  /// </summary>
  internal interface IHtmlFilter
  {
    /// <summary>
    /// Applies specific filter to the specified html content.
    /// </summary>
    /// <param name="html">The HTML content.</param>
    string Apply(string html);
  }
}
