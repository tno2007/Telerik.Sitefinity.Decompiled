// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ICustomOutputCacheVariationInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Interface for objects that register custom output cache variations for pages
  /// </summary>
  internal interface ICustomOutputCacheVariationInitializer
  {
    /// <summary>Registers the custom output cache variations.</summary>
    /// <param name="pageNode">The page node.</param>
    void RegisterCustomOutputCacheVariations(PageSiteNode pageNode);
  }
}
