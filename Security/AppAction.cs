// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.AppAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security
{
  /// <summary>Defines application wide security actions.</summary>
  public enum AppAction
  {
    /// <summary>User account view action.</summary>
    ViewUsers,
    /// <summary>User account management action.</summary>
    ManageUsers,
    /// <summary>Roles view action.</summary>
    ViewRoles,
    /// <summary>Roles management action.</summary>
    ManageRoles,
    /// <summary>ManageUserProfiles permissions</summary>
    ManageUserProfiles,
    /// <summary>View application permissions.</summary>
    ViewPermissions,
    /// <summary>Change application wide permissions.</summary>
    ChangePermissions,
    /// <summary>View configurations.</summary>
    ViewConfigurations,
    /// <summary>Configuration changes.</summary>
    ChangeConfigurations,
    /// <summary>Labels view action.</summary>
    ViewLabels,
    /// <summary>Labels management action.</summary>
    ManageLabels,
    /// <summary>View files from the file system.</summary>
    ViewFiles,
    /// <summary>Upload and delete files from the OS file system.</summary>
    ManageFiles,
    /// <summary>View Licenses.</summary>
    ViewLicenses,
    /// <summary>Licenses management.</summary>
    ManageLicenses,
    UseBrowseAndEdit,
    /// <summary>
    /// Security action required to edit backend pages via the UI
    /// </summary>
    ManageBackendPages,
    /// <summary>
    /// Security action that allows access to the backend pages for managing publishing points
    /// </summary>
    ManagePublishingSystem,
    /// <summary>
    /// Security action that allows access to the backend pages for managing search indices
    /// </summary>
    ManageSearchIndices,
    /// <summary>
    /// Security action that allows access to the backend pages for desiging widgets (public controls)
    /// </summary>
    ManageWidgets,
    /// <summary>
    /// Security action that allows access to the backend pages of the newsletters module
    /// </summary>
    ManageNewsletters,
    /// <summary>
    /// Security action that allows access to backend pages of the ecommerce modules
    /// </summary>
    ManageEcommerce,
    /// <summary>
    /// Security action that allows access to the backend pages for site synchronization
    /// </summary>
    SiteSynchronization,
    /// <summary>
    /// Security action that allows access to the backend pages for Multisite management
    /// </summary>
    ManageMultisiteManagement,
  }
}
