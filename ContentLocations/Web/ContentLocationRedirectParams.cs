// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.ContentLocationRedirectParams
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations.Web
{
  /// <summary>
  /// Defines the parameters that will be passed in order to open content item in preview mode
  /// </summary>
  internal class ContentLocationRedirectParams
  {
    /// <summary>
    /// The name of the query string parameter that should initiate a specific operation for the given item.
    /// </summary>
    public const string ActionKey = "sf-content-action";
    /// <summary>
    /// Specifies the preview action that should show the item.
    /// </summary>
    public const string ActionPreviewValue = "preview";
    /// <summary>
    /// Provides the key of the query string parameter that is used to specify the specific item status
    /// that should be loaded for preview
    /// </summary>
    public const string ItemLifeCycleStatusKey = "sf-lc-status";
    /// <summary>
    /// The name of the query string parameter that is used to specify the specific item id that should be loaded for preview.
    /// </summary>
    public const string ItemId = "sf-itemId";
  }
}
