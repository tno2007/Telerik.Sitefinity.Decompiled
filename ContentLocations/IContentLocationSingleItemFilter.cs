// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocationSingleItemFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides interface for checking if a single item matches particular filter.
  /// It guarantees that the location with such filter is able to show one particular item without specifying which.
  /// </summary>
  internal interface IContentLocationSingleItemFilter : 
    IContentLocationMatchingFilter,
    IContentLocationFilter
  {
  }
}
