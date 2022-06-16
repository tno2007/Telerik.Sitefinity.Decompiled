// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Enums.ContentViewDisplayMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ContentUI.Enums
{
  /// <summary>
  /// Defines the possible display modes of ContentView control.
  /// </summary>
  public enum ContentViewDisplayMode
  {
    /// <summary>
    /// Control automatically determines in which mode it will be. If not url parameters that determine the name of the
    /// view are present, control will assume it is in the master mode; otherwise the view specified through the url
    /// parameter will be loaded and control will be switched in the appropriate mode depending if the specified view
    /// implements <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewMasterDefinition" /> (Master mode) or <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDetailDefinition" />
    /// (Detail mode).
    /// </summary>
    Automatic,
    /// <summary>
    /// Control will be displaying the view defined through the MasterViewName property
    /// or the first view from the collection of view definitions that implements <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewMasterDefinition" />
    /// interface.
    /// </summary>
    Master,
    /// <summary>
    /// Control will be displaying the view defined through the DetailViewName property
    /// or the first view from the collection of view definitions that implements <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDetailDefinition" />
    /// interface.
    /// </summary>
    Detail,
  }
}
