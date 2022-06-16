// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ItemTrackingStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Status of item tracked during actions performed with the fuent api
  /// On commiting the changes done with fluent items, the fluent api can perform additional activites based on the status of the items like
  /// calling the publishing system, recompiling urls etc.
  /// </summary>
  public enum ItemTrackingStatus
  {
    /// <summary>
    /// 
    /// </summary>
    Modified,
    /// <summary>
    /// 
    /// </summary>
    Deleted,
    /// <summary>
    /// 
    /// </summary>
    DeletedWithAllTranslations,
    /// <summary>
    /// 
    /// </summary>
    Published,
    /// <summary>
    /// 
    /// </summary>
    Unpublished,
  }
}
